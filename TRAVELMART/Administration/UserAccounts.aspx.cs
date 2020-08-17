using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Net;

namespace TRAVELMART.Administration
{
    public partial class UserAccounts : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Modified By: Charlene Remotigue
        /// Date Modified:11/04/2012
        /// Description: Move page to a new master page
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  18/04/2012
        /// Description:    Move checking of uoHiddenFieldIsRefreshUserList and uoHiddenFieldPopupUser outside !IsPostBack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "User Maintenance Viewed";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetRoles();
                GetUsers("", uoTextBoxSearchParam.Text);                
                uoHiddenFieldIsRefreshUserList.Value = "0";
                uoHiddenFieldPopupUser.Value = "0";
                uoHyperLinkUserAdd.HRef = "~/Administration/CreateUserAccount.aspx";
            }
            if (uoHiddenFieldIsRefreshUserList.Value == "1")
            {
                RefreshUserList();
            }
            if (uoHiddenFieldPopupUser.Value == "1")
            {
                RefreshUserList();
            }
        }

        protected void uoDropDownUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string UserRoleString = "";
            UserRoleString = uoDropDownUserType.SelectedItem.Text;
            if (uoDropDownUserType.SelectedIndex == 0)
            {
                UserRoleString = "";
            }
            GetUsers(UserRoleString, uoTextBoxSearchParam.Text);
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            RefreshUserList();
        }

        /// <summary>
        /// Modified By: Charlene Remotigue
        /// Date Modified: 03/03/2012
        /// Description: add unlock user and reset password
        /// --------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  03/03/2012
        /// Description:    Reset users last activity date when Reset and Unlock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoUserList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                UserAccountBLL.DeleteUser(e.CommandArgument.ToString());
               

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                string sUserNameDeleted = e.CommandArgument.ToString();

                string sUser = sUserNameDeleted;
                string[] sUserArr = sUser.Split("::".ToCharArray());

                if (sUserArr.Count() > 0)
                {
                    MUser.DeactivateUserInLDAP(GlobalCode.Field2String(sUserArr[2]));
                }

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "User account deleted. (flagged as inactive) " + sUserNameDeleted;
                strFunction = "uoUserList_ItemCommand";


                BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

                GetUsers("", "");
            }

            else if (e.CommandName == "Unlock")
            {
                MembershipUser mUser = Membership.GetUser(e.CommandArgument.ToString());
                mUser.UnlockUser();
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);
                AlertMessage("User " + e.CommandArgument.ToString() + " successfully unlocked.");
            }
            else if (e.CommandName == "Reset")
            {
                Label userRole = new Label();

                userRole = (Label)e.Item.FindControl("Label2");
               
                MembershipUser mUser = Membership.GetUser(e.CommandArgument.ToString());
                if (mUser.IsLockedOut)
                {
                    mUser.UnlockUser();
                }
               
                string str = mUser.ResetPassword();
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);


                MUser.ChangePasswordInLDAP (mUser.UserName, str);

                string sEmail =  mUser.Email.ToString();
                HiddenField AlterNateEmail = (HiddenField)e.Item.FindControl("uoHiddenFieldAlternateEmail");


                if (userRole.Text.ToString() == TravelMartVariable.RoleImmigration)
                {
                    if (AlterNateEmail.Value != "")
                    { 
                        sEmail = AlterNateEmail.Value.ToString();
                    } }


                SendEmail(e.CommandArgument.ToString(), sEmail, str);
                //AlertMessage("User password successfully reset. New password will be emailed to user.");
                AlertMessage("New password has been sent to " + sEmail.ToString() + ".");
            }
        }

        protected void uoUserList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {
        }

        protected void uoUserListPager_PreRender(object sender, EventArgs e)
        {
            string UserRoleString = "";
            UserRoleString = uoDropDownUserType.SelectedItem.Text;
            if (uoDropDownUserType.SelectedIndex == 0)
            {
                UserRoleString = "";
            }
            GetUsers(UserRoleString, uoTextBoxSearchParam.Text);
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: Initialize Session Values (user, role)
        /// </summary>
        protected void InitializeValues()
        {
            string UserRolePrimary = GlobalCode.Field2String(Session["UserRole"]);
            string Name = GlobalCode.Field2String(Session["UserName"]);
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
                Name = GlobalCode.Field2String(Session["UserName"]);
            }

            MembershipUser UserName = Membership.GetUser(Name);
            if (UserName == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (!UserName.IsOnline)
                { 
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(Name);
                Session["UserRole"] = UserRolePrimary;
            }

            uoHiddenFieldUser.Value = Name;
            uoHiddenFieldRole.Value = UserRolePrimary;
            Session["strPrevPage"] = Request.RawUrl;
            if (HideHeaderElement())
            {
                ListView2.DataSource = null;
                ListView2.DataBind();
            }
            else
            {
                ListView1.DataSource = null;
                ListView1.DataBind();
            }
        }

        /// <summary>
        /// Date Created:   19/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get role list    
        /// -----------------------------------
        /// Date Modified:  22/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change GetUserRoles to GetUserRolesAccess
        /// </summary>
        private void GetRoles()
        {
            //DataTable dtRole = UserAccountBLL.GetUserRoles();
            DataTable dtRole = UserAccountBLL.GetUserRolesAccess(uoHiddenFieldRole.Value);
            try
            {
                uoDropDownUserType.Items.Clear();
                uoDropDownUserType.DataSource = dtRole;
                uoDropDownUserType.DataBind();
                uoDropDownUserType.Items.Insert(0, new ListItem("--Select Role--", "0"));
                //CommonFunctions.ChangeToUpperCase(uoDropDownUserType);
            }
            catch
            {
            }
            finally
            {
                if (dtRole != null)
                {
                    dtRole.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   19/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get user list  
        /// ------------------------------------
        /// Date Modified:   14/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Hide edit and delete header if not administrator   
        /// </summary>
        private void GetUsers(string role, string name)
        {
            DataTable dtUsers = UserAccountBLL.GetUsers(role, name, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, uoHiddenFieldOrderBy.Value);
            try
            {
                uoUserList.DataSource = dtUsers;
                uoUserList.DataBind();

                HtmlControl EditTH = (HtmlControl)uoUserList.FindControl("EditTH");
                HtmlControl DeleteTH = (HtmlControl)uoUserList.FindControl("DeleteTH");
                HtmlControl UnlockTH = (HtmlControl)uoUserList.FindControl("UnlockTH");
                HtmlControl ResetTH = (HtmlControl)uoUserList.FindControl("ResetTH");

                if (EditTH != null)
                {
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator))
                    {
                        //EditTH.Style.Add("display", "none");
                        DeleteTH.Style.Add("display", "none");
                        UnlockTH.Style.Add("display", "none");
                        ResetTH.Style.Add("display", "none");
                    }
                    else
                    {
                        //EditTH.Style.Add("display", "display");
                        DeleteTH.Style.Add("display", "display");
                        UnlockTH.Style.Add("display", "display");
                        ResetTH.Style.Add("display", "display");
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (dtUsers != null)
                {
                    dtUsers.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   14/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Hide Element if not administrator
        /// </summary>
        protected string HideElemet()
        {           
            return "";
        }

        /// <summary>
        /// Date Created:   22/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Hide Delete element
        /// </summary>
        protected string HideDeleteElement()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator))
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Date Created:   17/04/2012
        /// Created By:     Charlene Remotigue
        /// (description)   Hide Header element
        /// </summary>
        protected bool HideHeaderElement()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void SendEmail(string userId, string userEmail, string Password)
        {
            try
            {
                string sSubject = "Travelmart Password Recovery";
                string sBody = "";
                //sBody = "<table><tr><td colspan=\"2\">To log on to the site, use the following credentials:</td></tr>";
                sBody = "<table style=\"font-family:Tahoma; font-size:11px\"><tr><td colspan=\"2\">Your Password has been reset. To log on to <a href=\"https://rclcrew.com\">Travelmart System</a>, use the following credentials:</td></tr>";
                sBody += "<tr><td colspan=\"2\"></td></tr>";
                sBody += "<tr><td><br/><b>Username: </b></td>";
                sBody += "<td><br/>" + userId + "</td></tr>";
                sBody += "<tr><td><b>Password: </b></td>";
                sBody += "<td>" + Password + "</td></tr>";
                sBody += "<tr><td colspan=\"2\"></td></tr>";
                sBody += "<tr><td colspan=\"2\"><br/>If you have any questions or encounter any problems" +
                    " logging in, please contact a site administrator.</td></tr></table>";

                //CommonFunctions sendmail = new CommonFunctions();
                //sendmail.SendAsyncEmail(userEmail, sSubject, sBody);
                CommonFunctions.SendEmail("", userEmail, sSubject, sBody);

                if (userEmail != "")
                {
                    EPortalBLL PortalBLL = new EPortalBLL();
                    string sModuleID = GlobalCode.Field2Int(PortalBLL.GetPortalModuleID("USL")).ToString();

                    WSConnection.PortalInboxSave(userEmail, uoHiddenFieldUser.Value, sSubject, sBody,
                                userEmail, sModuleID);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }

        private void RefreshUserList()
        {
            string UserRoleString = "";
            UserRoleString = uoDropDownUserType.SelectedItem.Text;
            if (uoDropDownUserType.SelectedIndex == 0)
            {
                UserRoleString = "";
            }
            GetUsers(UserRoleString, uoTextBoxSearchParam.Text);
        }

        /// <summary>
        /// Date Created:   03/03/2012
        /// Created By:     Charlene Remotigue
        /// (description)   Alert message 
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", sScript, false);
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   29/Oct/2015
        /// Description:    Call API to reset password in LDAP
        /// </summary>
        /// <returns></returns>
        //private void ChangePasswordInLDAP(string sUsername,string sNewPassword)
        //{
        //    try
        //    {
        //        using (System.Net.WebClient client = new System.Net.WebClient())
        //        {
        //            string sAPI = MUser.GetLDAP();
        //            client.Headers.Add("content-type", "application/json");
        //            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

        //            string sAPI_URL = sAPI + "resetpass";
        //            string sAPI_param = "user=" + sUsername;
        //            sAPI_param = sAPI_param + "&pass=" + sNewPassword;

        //            string sResult = client.UploadString(sAPI_URL, sAPI_param);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string sMsg = "LDAP Error: " + ex.Message;
        //        AlertMessage(sMsg);
        //    }
        //}
        #endregion

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Security;

namespace TRAVELMART.Administration
{
    public partial class UserAccounts_old : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");
                uclabelStatus.Visible = false;

                string userName = GlobalCode.Field2String(Session["UserName"]);
                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {                   
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldUser.Value = userName;
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
                GetRoles();
                GetUsers("", uoTextBoxSearchParam.Text);
               
                //if (User.IsInRole("administrator"))
                //{
                //    uoHyperLinkUserAdd.Visible = true;
                //}
                //else
                //{
                //    uoHyperLinkUserAdd.Visible = false;
                //}
                uoHyperLinkUserAdd.HRef = "~/Administration/CreateUserAccount.aspx";
            }

            if (uoHiddenFieldIsRefreshUserList.Value == "1")
            {
                RefreshUserList();
            }
            if (uoHiddenFieldPopupUser.Value == "1")
            {
                RefreshUserList();
                //string UserRoleString = "";
                //UserRoleString = uoDropDownUserType.SelectedItem.Text;
                //if (uoDropDownUserType.SelectedIndex == 0)
                //{
                //    UserRoleString = "";
                //}
                //GetUsers(UserRoleString, uoTextBoxSearchParam.Text);
            }
            uoHiddenFieldIsRefreshUserList.Value = "0";
            uoHiddenFieldPopupUser.Value = "0";
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
                AlertMessage("User " + e.CommandArgument.ToString() + " successfully unlocked.");
            }
            else if (e.CommandName == "Reset")
            { 
                MembershipUser mUser = Membership.GetUser(e.CommandArgument.ToString());
                string str = mUser.ResetPassword();
                string sEmail = mUser.Email.ToString();
                SendEmail(e.CommandArgument.ToString(), sEmail, str);
                //AlertMessage("User password successfully reset. New password will be emailed to user.");
                AlertMessage("New password has been sent to " + mUser.Email + ".");
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

        #endregion
        #region Functions
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
            DataTable dtUsers = UserAccountBLL.GetUsers(role, name, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,"");
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
            //if (!User.IsInRole(TravelMartVariable.RoleAdministrator) && 
            //    !User.IsInRole(TravelMartVariable.Role24x7))
            //{
            //    return "hideElement";
            //}
            //else
            //{
            //    return "";
            //}
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

        protected void SendEmail(string userId, string userEmail, string Password)
        {
            try
            {
                string sSubject = "Travelmart Password Recovery";
                string sBody = "";
                //sBody = "<table><tr><td colspan=\"2\">To log on to the site, use the following credentials:</td></tr>";
                sBody = "<table style=\"font-family:Tahoma; font-size:smaller\"><tr><td colspan=\"2\">Your Password has been reset. To log on to <a href=\"http://travelmart.ptc.com.ph\">Travelmart System</a>, use the following credentials:</td></tr>";
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
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Net;
using System.Configuration;

namespace TRAVELMART
{
    public partial class LoginResetLDAP : System.Web.UI.Page
    {
        #region Events
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            MembershipUser mUser = Membership.GetUser(User.Identity.Name);
            if (mUser == null)
            {
                mUser = Membership.GetUser();
            }
            if (mUser == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (mUser.IsOnline == false || User.Identity.Name == "")
            {
                    Response.Redirect("Login.aspx", false);
            }
            //if (!mUser.CreationDate.Equals(mUser.LastPasswordChangedDate))
            //{
                //Label emailLabel = (Label)(ChangePassword1.ChangePasswordTemplateContainer.FindControl("uoTableChangePassword")).FindControl("uoLabelEmail");
                //emailLabel.Visible = false;
                //TextBox email = (TextBox)(ChangePassword1.ChangePasswordTemplateContainer.FindControl("uoTableChangePassword")).FindControl("uoTextBoxEmail");
                //email.Visible = false;
            //}
            //Label userName = (Label)(ChangePassword1.ChangePasswordTemplateContainer.FindControl("uoTableChangePassword")).FindControl("uoLabelUserName");
            //userName.Text = "Username " + User.Identity.Name;
            //ChangePassword1.NewPassword;
            //ChangePassword1.ConfirmNewPassword;
            //ChangePassword1.CurrentPassword;


            string sUserName = GlobalCode.Field2String(Session["UserName"]);
            string sLDAPSid = UserAccountBLL.GetUserSessionID_LDAP(sUserName);
            bool isAuthenticated = MUser.IsLDAPSessionValid(sUserName, sLDAPSid);
            if (!isAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (MUser.GetLDAPLoginIsON() == "1")
                {
                    if (!IsUserWithAlternateEmail(User.Identity.Name))
                    {
                        string sLDAPURL = ConfigurationSettings.AppSettings["LDAP-URL-Main"].ToString();
                        sLDAPURL = sLDAPURL + "change_password";
                        Response.Redirect(sLDAPURL);
                    }                   
                }
            }
        }

        /// <summary>
        /// Date Modified:  11/Nov/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Add LDAP change password
        /// ===================================
        /// </summary>        
        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strNewPass = NewPassword.Text;
                string strDummyPassword;
                string sMsg = "";

                if (!strNewPass.Any(char.IsUpper))
                {
                    sMsg = "Password must have uppercase ";
                }
                if (!strNewPass.Any(char.IsLower))
                {
                    if (sMsg.Trim() == "")
                    {
                        sMsg = "Password must have lowercase letter. ";
                    }
                    else
                    {
                        sMsg = sMsg + " and lowercase letter. ";
                    }
                }
                else
                {
                    if (sMsg != "")
                    {
                        sMsg = sMsg + "letter. ";
                    }
                }

                if (sMsg != "")
                {
                    AlertMessage(sMsg);
                }
                else
                {

                    MembershipUser mUser = Membership.GetUser(User.Identity.Name);
                    strDummyPassword = mUser.ResetPassword();

                    if (mUser.ChangePassword(strDummyPassword, strNewPass))
                    {
                        MUser.ChangePasswordInLDAP(mUser.UserName, strNewPass);
                        SelectSpecialistViewType();
                        AlertMessage("Password successfully changed.");
                    }
                    else
                    {
                        AlertMessage("Password change failed. Please re-enter your password and try again.");
                    }
                }                
            }
            catch (Exception ex)
            {
                if (ex.Message == "Non alpha numeric characters in 'newPassword' needs to be greater than or equal to '1'.")
                {
                    AlertMessage("Password must be at least 8 characters long and a combination of Alpha-Numeric and Special Characters.");
                }
                else
                {
                    AlertMessage(ex.Message);
                }                
            }
        }
        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            SelectSpecialistViewType();
        }

        //protected void ChangePassword1_ContinueButtonClick(object sender, EventArgs e)
        //{
        //    SelectSpecialistViewType();
        //}

        //protected void ChangePassword1_CancelButtonClick(object sender, EventArgs e)
        //{
        //    SelectSpecialistViewType();
        //}

        #endregion

        #region Functions
        /// <summary> 
        /// Date Created:   08/07/2011
        /// Created By:     Marco Abejar
        /// (description)   role management
        /// ===================================
        /// Date Modified:  18/07/2011
        /// Modified By:    Josphine Gad
        /// (description)   Change ONSIGNING to ON, OFFSIGNING to OFF, default page of all is GeneralListView.aspx
        /// ===================================
        /// Date Modified:  14/10/2011
        /// Modified By:    Josphine Gad
        /// (description)   Remove hardcoded roles
        /// ===================================
        /// Date Modified:  29/03/2012
        /// Modified By:    Josphine Gad
        /// (description)   Set authentication timeout to 90 mins
        /// ===================================
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Set UserBranchId upon Login
        /// ===================================
        /// Date Modified:  22/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   set user vendor id and Branch Name,
        ///                 set redirect for Hotel Vendors
        /// ===================================
        /// Date Modified:  31/08/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add dfferent role's page
        /// ===================================
        /// Date Modified:  25/Apr/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change Hotel Vendor's Page to HotelConfirmManifest.aspx Page
        /// ===================================
        /// Date Modified:  11/Aug/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add default page of Crew Medical Role
        /// ===================================
        /// </summary>         
        private void SelectSpecialistViewType()
        {
            string strLogDescription;
            string strFunction;

            MembershipUser mUser = Membership.GetUser(User.Identity.Name);
            //if (mUser != null)
            //{
                string[] uRoles = Roles.GetRolesForUser(mUser.UserName);
                if (uRoles[0] != "")//== "Administrator" || uRoles[0] == "24x7" || uRoles[0] == "Crew Admin")
                {
                    string sUser = mUser.UserName;
                    List<UserAccountList> userAccount = UserAccountBLL.GetUserInfoListByName(sUser);
                    Session["UserAccountList"] = userAccount;

                    List<UserPrimaryDetails> userDetails = (List<UserPrimaryDetails>)Session["UserPrimaryDetails"];

                    var vRole = (from a in userAccount
                                 where a.bIsPrimary == true
                                 select new
                                 {
                                     sRole = a.sRole
                                 }).ToList();//UserAccountBLL.GetUserPrimaryRole(sUser);
                    string PrimaryRole = vRole[0].sRole;
                    string strUserFName = userDetails[0].sFirstName + " as " + PrimaryRole;
                    //UserAccountBLL.GetUserFirstname(mUser.ToString()) + " as " + PrimaryRole;

                    DateTime dDateFrom = CommonFunctions.GetCurrentDateTime();
                    Session["UserName"] = sUser;
                    Session["UserRole"] = PrimaryRole;
                    Session["DateFrom"] = dDateFrom.ToString("MM/dd/yyyy");

                    //add UserBranchId Upon LogIn
                    Session["UserBranchID"] = userDetails[0].iBranchID;//UserAccountBLL.GetUserBranchId(sUser, PrimaryRole);
                    Session["BranchName"] = userDetails[0].sBranchName;//UserAccountBLL.GetUserBranchName(sUser, PrimaryRole);
                    Session["VendorID"] = userDetails[0].iVendorID; // UserAccountBLL.GetUserVendorId(sUser, PrimaryRole);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        sUser, dDateFrom, dDateFrom.AddMinutes(90), false, PrimaryRole, FormsAuthentication.FormsCookiePath);

                    //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                    strLogDescription = "User logged as " + PrimaryRole + ".";
                    strFunction = "SelectSpecialistViewType";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, sUser);

                    if (PrimaryRole == TravelMartVariable.RoleHotelSpecialist)
                    {
                        //Response.Redirect("~/HotelDashboardRoomType.aspx?ufn=" + strUserFName);
                        Response.Redirect("~/HotelDashboardRoomType5.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RolePortSpecialist)
                    {
                        Response.Redirect("~/PortAgent/PortAgentDashboard.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleMeetGreet)
                    {
                        Response.Redirect("~/MeetAndGreet/MeetAndGreet.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewAdmin)
                    {
                        Response.Redirect("~/CrewAdmin/CrewAdmin.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleHotelVendor)
                    {
                        //Response.Redirect("~/HotelDashboard3.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy") +
                        //    "&branchid=" + Session["UserBranchID"].ToString() + "&brandId=" + Session["VendorID"].ToString() + "&branchName=" + Session["BranchName"].ToString()); //gelo

                        //Response.Redirect("~/Hotel/HotelVendor.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                        Response.Redirect("~/Hotel/HotelConfirmManifest.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleSystemAnalyst)
                    {
                        Response.Redirect("~/SystemAnalyst/ExceptionPNR.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleFinance)
                    {
                        //Response.Redirect("~/Hotel/HotelConfirmManifest.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                        Response.Redirect("~/ManifestSearchFilterPage.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleVehicleVendor)
                    {
                        Response.Redirect("~/Vehicle/VehicleManifestByVendor.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleImmigration)
                    {
                        Response.Redirect("~/Immigration/CrewVerification.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewAssist)
                    {
                        Response.Redirect("~/CrewAssist/CrewAssist.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewAssistTeamLead)
                    {
                        Response.Redirect("~/CrewAssist/CrewAssist.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                        //Response.Redirect("~/CrewAssist/CrewAssistNew.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewMedical)
                    {
                        Response.Redirect("~/ManifestSearchFilterPage.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        Response.Redirect("~/Manifest.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy")); //gelo
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            //}
        }
        private void AlertMessage(string s)
        {
            s = s.Replace("'", " ");
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
        }
        /// <summary>
        /// Date Modified:  11/Nov/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Verify if User is Immigartion with Alternate email
        /// ===================================
        /// </summary>
        private bool IsUserWithAlternateEmail(string sUser)
        {
            bool bReturn = false;
            DateTime dtFrom = GlobalCode.Field2DateTime( "1/1/2000");
            DateTime dtTo = DateTime.Now.AddDays(100);

            List<UserList_LDAP> list = UserAccountBLL.GetUserList(dtFrom, dtTo, TravelMartVariable.RoleImmigration, true, true, sUser);
            if (list.Count > 0)
            {
                foreach (UserList_LDAP item in list)
                {
                    if (sUser == item.UserName)
                    {
                        bReturn = true;
                        break;
                    }
                }
            }
            return bReturn;
        }       
        #endregion
    }
}

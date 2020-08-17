using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class LoginProcess : System.Web.UI.Page
    {
        /// <summary>
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------        
        /// Date Modified:  15/08/2012
        /// Modified By:    Josphine Gad
        /// (description)   Add different role's page
        ///----------------------------------------------
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Add HotelVendor page
        ///                 Set UserBranchId upon Login
        ///----------------------------------------------
        /// Date Modified:  23/10/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change UserAccountBLL.GetUserPrimaryRole(sUser) to list
        ///----------------------------------------------
        /// Date Modified:  27/Nov/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add   List<UserPrimaryDetails> instead of calling different proc in DB
        ///----------------------------------------------
        /// Date Modified:  25/Apr/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change Hotel Vendor's Page to HotelConfirmManifest.aspx Page
        ///----------------------------------------------     
        /// Date Modified:  7/May/2013
        /// Modified By:    Marco Abejar
        /// (description)   Change Crew Assist Page to HotelDashboardRoomType5.aspx Page
        ///----------------------------------------------     
        /// Date Modified:  11/Aug/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add default page of Crew Medical Role
        /// ===================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            AlertMessage("Logged already");
            AlertMessage(GlobalCode.Field2String(User.Identity.Name));



            //UserVendorBLL bll = new UserVendorBLL();
            if (GlobalCode.Field2Int(UserVendorBLL.GetActiveUserVendor(User.Identity.Name)) == 0)
            {

                Session["ActiveUserVendor"] = 1;
                Response.Redirect("Login.aspx");

                //AlertMessage("There is no active contract connected in your account.");
            }

            if (User.Identity.Name == "")
            {
                Response.Redirect("Login.aspx", false);
            }
            MembershipUser mUser = Membership.GetUser(User.Identity.Name);
            mUser.UnlockUser();
            //if (mUser.CreationDate.Equals(mUser.LastPasswordChangedDate))
            //{
            //    Response.Redirect("~/LoginReset.aspx", false);
            //}
            //else
            {
                string strLogDescription;
                string strFunction;

                string[] uRoles = Roles.GetRolesForUser(mUser.UserName);

                if (uRoles[0] != "")
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
                    strFunction = "Page_Load";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, sUser);

                    if (PrimaryRole == TravelMartVariable.RoleAdministrator)
                    {
                        Response.Redirect("~/HotelDashboardRoomType5.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleHotelSpecialist )
                    {
                        //Response.Redirect("~/HotelDashboardRoomType.aspx?ufn=" + strUserFName);
                        Response.Redirect("~/HotelDashboardRoomType5.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RolePortSpecialist)
                    {
                        Response.Redirect("~/PortAgent/PortAgentDashboardNew.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));


                   
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleMeetGreet )
                    {
                        Response.Redirect("~/MeetAndGreet/MeetAndGreet.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewAdmin)
                    {
                        Response.Redirect("~/CrewAdmin/CrewAdmin.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleHotelVendor)
                    {
                        //Response.Redirect("~/Hotel/HotelVendor.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                        Response.Redirect("~/Hotel/HotelConfirmManifest.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleSystemAnalyst)
                    {
                        Response.Redirect("~/SystemAnalyst/ExceptionPNR.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleFinance)
                    {
                        Response.Redirect("~/ManifestSearchFilterPage.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleImmigration)
                    {
                        Response.Redirect("~/Immigration/CrewVerification.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewAssist)
                    {

                        Response.Redirect("~/CrewAssist/CrewAssist.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                        //Response.Redirect("~/CrewAssist/CrewAssistNew.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));                        
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewAssistTeamLead)
                    {
                        Response.Redirect("~/CrewAssist/CrewAssist.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                        //Response.Redirect("~/Medical/Medical.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleVehicleVendor)
                    {
                        Response.Redirect("~/Vehicle/VehicleManifestByVendor.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleCrewMedical)
                    {
                        Response.Redirect("~/ManifestSearchFilterPage.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }

                    else if (PrimaryRole == TravelMartVariable.RoleCrewMedical)
                    {
                        Response.Redirect("~/ManifestSearchFilterPage.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleDriver)
                    {
                        Response.Redirect("~/Vehicle/VehicleManifestByVendor.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    else if (PrimaryRole == TravelMartVariable.RoleGreeter)
                    {
                        Response.Redirect("~/Vehicle/VehicleManifestByVendor.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy"));
                    }
                    //else
                    //{
                    //    Response.Redirect("~/Manifest.aspx?ufn=" + strUserFName + "&dt=" + DateTime.Now.ToString("MM/dd/yyyy")); //gelo
                    //}
                }
                else
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }
            //}
            //catch (Exception ex)
            //{ 
            //    ExceptionBLL.InsertException(ex.Message, "LoginProcess.aspx", CommonFunctions.GetCurrentDateTime(), Session["UserName"].ToString());
            //}
        }


        /// Author:         Josephine Gad
        /// Date Created:   31/08/2012
        /// Description:    pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        } 

    }
}

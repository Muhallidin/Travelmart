using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.Security;
using System.Configuration;


namespace TRAVELMART
{
    public partial class TravelMartMaintenanceMaster2 : System.Web.UI.MasterPage
    {
        #region METHODS
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  31/08/2012
        /// Description:    Force logout if there is new session id for this user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            InitializeValues();
            
            string sUserName = GlobalCode.Field2String(Session["UserName"]);
            string sLDAPSid = UserAccountBLL.GetUserSessionID_LDAP(sUserName);
            bool isAuthenticated = MUser.IsLDAPSessionValid(sUserName, sLDAPSid);
            if (!isAuthenticated)
            {
                try
                {
                    Response.Redirect("/Login.aspx", false);
                }
                catch
                {
                    Response.Redirect("../Login.aspx");
                }
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["ufn"] != null)
                {
                    string sUserSessionID = UserAccountBLL.GetUserSessionID(GlobalCode.Field2String(Session["UserName"]));
                    if (sUserSessionID != "" && sUserSessionID != Session.SessionID)
                    {
                        var rCookie = Request.Cookies["asp.net_sessionid"];
                        if (rCookie != null)
                        {
                            HttpCookie respCookie = new HttpCookie("asp.net_sessionid", sUserSessionID);
                            respCookie.Expires = DateTime.Now.AddMinutes(-1);
                            Session["ForceLogout"] = "1";
                            Response.Redirect("/Login.aspx", false);
                        }
                    }
                    else
                    {
                        Session["ForceLogout"] = "0";
                    }
                    SetDefaultValues();
                    
                    SetValues();

                    string sUserRole = GlobalCode.Field2String(Session["UserRole"]); ;
                    if (sUserRole == "" && GlobalCode.Field2String(Session["HotelNameToSearch"]) == "")
                    {
                        GetUserBranchInfo();
                    }
                    else
                    {
                        if (sUserRole == TravelMartVariable.RolePortSpecialist ||
                            sUserRole == TravelMartVariable.RoleHotelVendor ||
                            sUserRole == TravelMartVariable.RoleVehicleVendor)
                        {
                            uoLabelBranchName.Text = GlobalCode.Field2String(Session["HotelNameToSearch"]);
                        }
                        else
                        {
                            uoLabelBranchName.Text = "";
                        }
                    }
                    uoHiddenFieldRole.Value = sUserRole;

                }

                ucLabelFName.Text = Request.QueryString["ufn"];
            }
           
        }
        /// <summary>
        /// Date Modified:  04/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Reset membership time
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinueWorking_Click(object sender, EventArgs e)
        {
            //Do nothing. But the Session will be refreshed as a result of 
            //this method being called, which is its entire purpose.
            string sUser = GlobalCode.Field2String(MUser.GetUserName());
            if (sUser != "")
            {
                MembershipUser mUser = Membership.GetUser(sUser);
                if (mUser != null)
                {
                    mUser.LastActivityDate = DateTime.Now;
                    Membership.UpdateUser(mUser);
                }
            }
        }      
        protected void btnExitWorking_Click(object sender, EventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (mUser != null)
            {
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);                
                Membership.UpdateUser(mUser);
            }
            FormsAuthentication.SignOut();
            try
            {
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                try
                {
                    Response.Redirect("../Login.aspx", false);

                }
                catch
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }
        /// <summary>
        /// ------------------------------
        /// Date Modified:  03/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Set  mUser.LastActivityDate to -Membership.UserIsOnlineTimeWindow         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(MUser.GetUserName()));
            if (mUser != null)
            {
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);
            }
            FormsAuthentication.SignOut();
            try
            {
                MUser.LogoutUserInLDAP(GlobalCode.Field2String(Session["UserName"]));
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                try
                {
                    Response.Redirect("../Login.aspx", false);

                }
                catch
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }
        protected void uoLinkButtonGoBackLDAP_Click(object sender, EventArgs e)
        {
            string sLDAPURL = ConfigurationSettings.AppSettings["LDAP-URL-Main"].ToString();
            sLDAPURL = sLDAPURL + "index";
            Response.Redirect(sLDAPURL);
        }
        #endregion 

        #region EVENTS
        protected void InitializeValues()
        {
            string sUserName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                try
                {
                    Response.Redirect("/Login.aspx", true);
                }
                catch
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    try
                    {
                        Response.Redirect("/Login.aspx", false);
                    }
                    catch
                    {
                        Response.Redirect("../Login.aspx");
                    }
                }
            }
            uoHiddenFieldPrevPage.Value = GlobalCode.Field2String(Session["strPrevPage"]);
        }

        protected void SetDefaultValues()
        {
            DateTime dt = DateTime.Now;
            string branchId = "0";
            string branchName = "";
            if (Request.QueryString["branchId"] != null)
            {
                branchId = Request.QueryString["branchId"];
            }
            if (Request.QueryString["branchName"] != null)
            {
                branchName = Request.QueryString["branchName"];
            }

            if (uoHiddenFieldPopupCalendar.Value != "1")
            {
                if (Session["DateFrom"] == null)
                {
                    if (Request.QueryString["dt"] != "")
                    {
                        Session["DateFrom"] = Request.QueryString["dt"];
                    }
                    else
                    {
                        Session["DateFrom"] = dt.ToString();
                    }
                }
                else
                {
                    dt = DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"]));
                }
            }
            else
            {

                dt = DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"]));
                uoHiddenFieldPopupCalendar.Value = "0";
                //Response.Redirect(strURL + "?ufn=" + Request.QueryString["ufn"] + "&dt=" + dt.ToString());
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }
        }

        public void SetValues()
        {
            if (Session["Region"] == null)
            {
                Session["Region"] = "0";
            }
            if (Session["Country"] == null)
            {
                Session["Country"] = "0";
            }
            if (Session["City"] == null)
            {
                Session["City"] = "0";
            }
            if (Session["Port"] == null)
            {
                Session["Port"] = "0";
            }
            if (Session["Hotel"] == null)
            {
                Session["Hotel"] = "0";
            }
            if (Session["ViewRegion"] == null)
            {
                Session["ViewRegion"] = "1";
            }
            if (Session["ViewCountry"] == null)
            {
                Session["ViewCountry"] = "1";
            }
            if (Session["ViewCity"] == null)
            {
                Session["ViewCity"] = "1";
            }
            if (Session["ViewHotel"] == null)
            {
                Session["ViewHotel"] = "1";
            }
            if (Session["ViewPort"] == null)
            {
                Session["ViewPort"] = "1";
            }
            if (Session["ViewLegend"] == null)
            {
                Session["ViewLegend"] = "1";
            }
            if (Session["ViewFilter"] == null)
            {
                Session["ViewFilter"] = "0";
            }
            if (Session["ViewDashboard"] == null)
            {
                Session["ViewDashboard"] = "0";
            }
            if (Session["ViewDashboard2"] == null)
            {
                Session["ViewDashboard2"] = "0";
            }
            if (Session["UserBranchID"] == null)
            {
                Session["UserBranchID"] = "";
            }
        }
        /// <summary>
        /// Modified by:    Josephine Gad
        /// Date Modified:  15/08/2012
        /// Description:    Get UserDateRange from UserAccountList
        /// </summary>
        protected void GetUserBranchInfo()
        {
            string sUserName = GlobalCode.Field2String(Session["UserName"]);//MUser.GetUserName();
            string sUserRole = GlobalCode.Field2String(Session["UserRole"]);
            string sDateFrom = GlobalCode.Field2String(Session["DateFrom"]);
            if (sUserName == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }
            if (sUserRole == "")
            {
                sUserRole = UserAccountBLL.GetUserPrimaryRole(sUserName);
                Session["UserRole"] = sUserRole;
            }

            List<UserAccountList> listUser = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
            int UserDateRange = listUser[0].iDayNo;

            DateTime dt = DateTime.Parse(sDateFrom).AddDays(UserDateRange);
            Session["DateTo"] = dt.ToString();

            if (sUserRole == TravelMartVariable.RolePortSpecialist ||
                sUserRole == TravelMartVariable.RoleHotelVendor ||
                sUserRole == TravelMartVariable.RoleVehicleVendor)
            {
                IDataReader dr = null;
                try
                {
                    dr = UserAccountBLL.GetUserBranchDetails(GlobalCode.Field2String(Session["UserName"]), sUserRole);
                    if (dr.Read())
                    {
                        uoLabelBranchName.Text = dr["BranchName"].ToString().ToUpper();
                        Session["HotelNameToSearch"] = uoLabelBranchName.Text;
                        Session["UserRoleKey"] = dr["RoleID"].ToString();
                        Session["UserBranchID"] = dr["BranchID"].ToString();
                        Session["UserCountry"] = dr["CountryID"].ToString();
                        Session["UserCity"] = dr["CityID"].ToString();
                        Session["UserVendor"] = dr["VendorID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Get user details using session
        /// </summary>
        /// <returns></returns>
        private List<UserAccountList> GetUserAccountList(string sUserName)
        {
            List<UserAccountList> list = new List<UserAccountList>();

            if (Session["UserAccountList"] != null)
            {
                list = (List<UserAccountList>)Session["UserAccountList"];
            }
            else
            {
                list = UserAccountBLL.GetUserInfoListByName("sUserName");
                Session["UserAccountList"] = list;
            }
            return list;
        }
        #endregion
    }
}

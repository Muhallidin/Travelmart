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
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Configuration;

namespace TRAVELMART
{
    public partial class TravelmartMasterPage : System.Web.UI.MasterPage
    {


        #region DEFINITIONS
        #endregion

        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 06/03/2012
        /// ----------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  31/08/2012
        /// Description:    Force logout if there is new session id for this user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();

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
                    ucLabelFName.Text = Request.QueryString["ufn"];
                    string sUserRole = GlobalCode.Field2String(Session["UserRole"]); ;

                }

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
        /// <summary>
        /// Date Modified:  04/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Set  mUser.LastActivityDate to -Membership.UserIsOnlineTimeWindow          
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Author: Charlene Remotigue
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
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
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

        #region METHODS
        /// <summary>
        /// Modified By: Charlene Remotigue
        /// Date MOdified: 13/04/2012
        /// Description: save prev page url to hiddenfield
        /// </summary>
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
                    Response.Redirect("/Login.aspx", false);
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
                list = UserAccountBLL.GetUserInfoListByName(sUserName);
                Session["UserAccountList"] = list;
            }
            return list;
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   14/Oct/2015
        /// Description:    Call API to logout user in LDAP
        /// </summary>
        /// <returns></returns>
        //private void LogoutUserInLDAP()
        //{
        //    try
        //    {

        //        using (System.Net.WebClient client = new System.Net.WebClient())
        //        {
        //            string sAPI = MUser.GetLDAP();
        //            client.Headers.Add("content-type", "application/json");
        //            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

        //            string sAPI_URL = sAPI + "signout";
        //            string sAPI_param = "user=" + GlobalCode.Field2String(Session["UserName"]);
        //            sAPI_param = sAPI_param + "&company=" + MUser.GetLDAPCompany();

        //            string sResult = client.UploadString(sAPI_URL, sAPI_param);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string sMsg = "LDAP Error: " + ex.Message;
        //        throw ex;
        //    }
        //}
        #endregion

    }
}

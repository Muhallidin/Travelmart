using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TRAVELMART.Common;
using System.Collections.Generic;
using TRAVELMART.BLL;
using System.Collections.ObjectModel;
using System.Text;
using System.Net;
using System.Xml;
using System.Reflection;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.IO;


namespace TRAVELMART
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  03/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   set mUser.LastActivityDate to -Membership.UserIsOnlineTimeWindow 
        /// -------------------------------------------        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ResetPassword();
            
            //LoadHotelTimeZone();
            string str = ConfigurationSettings.AppSettings["UnderMaintenance"].ToString();           

            if (ConfigurationSettings.AppSettings["IsTest"].ToString() == "1")
            {
                if (GlobalCode.Field2String(Request.QueryString["IsTest"]) == "1")
                {
                    //login to TM if IsTest = 1 
                    //if (str == "1") //Go to Undermaintenance if undermaintenance is 1
                    //{
                    //    Response.Redirect("UnderMaintenance.html");
                    //}
                    //else if (str == "2")
                    //{
                    //    Response.Redirect("UnderMaintenanceWithTime.html");
                    //}
                    //else if (MUser.GetLDAPLoginIsON() == "1")
                    //{
                    //    //Go to LDAP Page
                    //    RedirectToLDAP();
                    //}
                }                
                else if (str == "1") //Go to Undermaintenance if undermaintenance is 1
                {
                    Response.Redirect("UnderMaintenance.html");
                }                    
                else if (str == "2") //Go to Undermaintenance if undermaintenance is 1
                {
                    Response.Redirect("UnderMaintenanceWithTime.html");
                }
                else
                {
                    if (MUser.GetLDAPLoginIsON() == "1")
                    {
                        RedirectToLDAP();
                    }
                }               
               
            }

            else if (str == "1")
            {
                Response.Redirect("UnderMaintenance.html");
            }
            else if (str == "2")
            {
                Response.Redirect("UnderMaintenanceWithTime.html");
            }
           
            else if (MUser.GetLDAPLoginIsON() == "1")
            {
                RedirectToLDAP();
            }
           
            
           

            //if (GlobalCode.Field2String(Session["ActiveUserVendor"]) == "1")
            //{
            //    Session["ActiveUserVendor"] = 0;

            //    AlertMessage("There is no active contract connected in your account.");

            //}

            if (!IsPostBack)
            {
                //TextBox1.Text = Request.Headers["X-API-User"];                
                //TextBox2.Text = Request.Headers["X-API-SID"];                                             

                if (Session["ForceLogout"] != null && GlobalCode.Field2String(Session["ForceLogout"]) != ""
                    && GlobalCode.Field2String(Session["ForceLogout"]) != "0")
                {
                    AlertMessage("Another user logged in to this account.");
                }
                else
                {

                    CommonFunctions.ClearVariables();
                    ClearApplicationCache();
                    //Cache.Remove("UserMenu");

                    Session.Abandon();
                    Session.Clear();
                    if (Session.Count > 0)
                        Session.RemoveAll();

                    Session["UserRole"] = "";
                    Session["UserRoleKey"] = "";

                    HttpCookie cookie = Request.Cookies["loginDetails"];

                    if (cookie != null)
                    {
                        // Response.Cookies["loginDetails"].Expires = DateTime.Now.AddDays(-1);//force epire password
                        Login1.UserName = Server.HtmlEncode(Request.Cookies["loginDetails"].Values["login"]).ToString();
                        TextBox Password = Login1.FindControl("Password") as TextBox;
                        Password.Attributes["value"] = Password != null ? Server.HtmlEncode(Request.Cookies["loginDetails"].Values["pass"]).ToString() : "";
                    }

                    //enable for debugging
                    //MembershipUser mUser = Membership.GetUser("hotelCha");
                    //mUser.LastActivityDate = DateTime.Now.AddMinutes(-180);
                    //Membership.UpdateUser(mUser);
                    //mUser = Membership.GetUser("hotel");
                    //mUser.LastActivityDate = DateTime.Now.AddMinutes(-15);
                    //Membership.UpdateUser(mUser);

                    //FormsAuthentication.SignOut();
                    MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(MUser.GetUserName()));
                    if (mUser != null)
                    {
                        mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                        Membership.UpdateUser(mUser);
                    }

                    
                }
                FormsAuthentication.SignOut();


                //if (Request.QueryString["EPortalToken"] != null)
                //{
                //    if (GlobalCode.Field2String(Request.QueryString["EPortalToken"]) != "")
                //    {
                //        if (IsSessionIDValid())
                //        {
                //            Response.Redirect("LoginProcess.aspx",false);
                //        }
                //    }

                //    if (Request.QueryString["EPortalToken"].Trim() == "")
                //    {
                //        Panel2.Visible = true;
                //        LabelLoginErrorDetails.Text = "Token from E-Portal required!";
                //    }
                //}
                //AuthenticateLDAP();
                if (MUser.GetLDAPLoginIsON() == "1")
                {
                    AuthenticateLDAP();
                }
            }
        }

        protected void Login1_LoginError(object sender, EventArgs e)
        {
            MembershipUser userInfo = Membership.GetUser(Login1.UserName);
            
            if (userInfo == null)
            {
                Panel2.Visible = true;
                LabelLoginErrorDetails.Text = "There is no user in the database with the username " + Login1.UserName;
            }
            else if (userInfo.IsLockedOut == true)
            { 
                Panel2.Visible = true;
                    LabelLoginErrorDetails.Text = Login1.UserName + " has been locked out. Please contact the administrator to " +
                        "have your account unclocked.";
            }
            else
            {
                Panel2.Visible = true;
                LabelLoginErrorDetails.Text = "The username or password you entered is incorrect.";
            }
        }
 

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            CheckBox rm = (CheckBox)Login1.FindControl("RememberMe");

            //UserAccountBLL bll = new UserAccountBLL();
            //if (GlobalCode.Field2Int(bll.GetActiveUserVendor(Login1.UserName)) == 0)
            //{

            //    HttpCookie myCookie = new HttpCookie("ActiveUserVendor");
            //    Session["ActiveUserVendor"] = "1";

            //    Response.Redirect("Login.aspx");

            //}
            
                if (rm.Checked)
                {
                    HttpCookie myCookie = new HttpCookie("loginDetails");
                    Response.Cookies.Remove("loginDetails");
                    Response.Cookies.Add(myCookie);
                    myCookie.Values.Add("login", Login1.UserName.ToString());
                    myCookie.Values.Add("pass", Login1.Password.ToString());
                    DateTime dtExpiry = DateTime.Now.AddDays(15);
                    Response.Cookies["loginDetails"].Expires = dtExpiry;
                }

            
           

           
        }
        /// <summary>
        /// Date Created:   08/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Clear Cache
        /// </summary>
        private void ClearApplicationCache()
        {            
            List<string> keys = new List<string>();

            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();


            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }


            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                Cache.Remove(keys[i]);
            }
        }

        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            LoginUser();
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
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
       
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   13/Nov/2014
        /// Description:    Move Login1_LoggingIn content to this function to Login user to TM
        /// </summary>
        private void LoginUser()
        {
            MembershipUser mUser = Membership.GetUser(Login1.UserName);            
            DateTime dt = DateTime.Now;
            
            //#if !DEBUG
            if (mUser != null)
            {
                UserAccountBLL.UpdateUserSessionID_LDAP(mUser.UserName, "");
                UserAccountBLL.UpdateUserSessionID(mUser.UserName, Session.SessionID);
                //if (mUser.IsOnline)
                //{

                //var timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["myTimeoutConfigSetting"]);

                //foreach (var cookey in Request.Cookies.AllKeys)
                //{
                //    if (cookey == FormsAuthentication.FormsCookieName || cookey.ToLower() == "asp.net_sessionid")
                //    {
                //        var reqCookie = Request.Cookies[cookey];

                //        if (reqCookie != null)
                //        {
                //            HttpCookie respCookie = new HttpCookie(reqCookie.Name, reqCookie.Value);
                //            respCookie.Expires = DateTime.Now.AddMinutes(timeout);

                //            Response.Cookies.Set(respCookie);
                //        }
                //    }
                //}
                //if(may session pa)

                //FormsAuthentication.SignOut();
                //ClearApplicationCache();
                //mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                //Membership.UpdateUser(mUser);

                //if (mUser.LastActivityDate.CompareTo(dt) > 0)
                //{
                //    e.Cancel = true;
                //    Panel2.Visible = true;
                //    LabelLoginErrorDetails.Text = Login1.UserName + " is already logged in. Multiple user logins not allowed.";
                //}
                //else
                //{
                //    if (dt.Subtract(mUser.LastActivityDate).Minutes <= Membership.UserIsOnlineTimeWindow)
                //    {
                //        e.Cancel = true;
                //        Panel2.Visible = true;
                //        LabelLoginErrorDetails.Text = Login1.UserName + " is already logged in. Multiple user logins not allowed.";
                //    }
                //}
                //}
            }
            //#endif
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   13/Nov/2014
        /// Description:    Login User using E-Portal Session ID
        /// </summary>
        private bool IsSessionIDValid()
        {
            string sSesssionID = GlobalCode.Field2String(Request.QueryString["EPortalToken"]);
            bool bReturn = false;

            List<UserList> list = new List<UserList>();
            list = EPortalBLL.GetUserBySessionID(sSesssionID);
            if (list.Count > 0)
            {
                Login1.UserName = list[0].sUserName;
                LoginUser();
                FormsAuthentication.SetAuthCookie(Login1.UserName, true);
                //FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);

                MembershipUser mUser = Membership.GetUser(Login1.UserName);
                DateTime dt = DateTime.Now;

                mUser.LastLoginDate = dt;
                mUser.LastActivityDate = dt;
                Membership.UpdateUser(mUser);

                bReturn = true;                
            }
            else
            {
                Panel2.Visible = true;
                LabelLoginErrorDetails.Text = "Invalid Token from E-Portal!";
            }
            return bReturn;
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   09/Jul/2015
        /// Description:    One time process to insert
        /// </summary>
        private void LoadHotelTimeZone()
        {
            MasterfileBLL BLL = new MasterfileBLL();

            string sTimezoneID;
            string sDisplayName;
            string sStandardName;
          

            DateTime baseUTC = new DateTime(2000, 1, 1);
            TimeSpan tOffset;// =  localZone.GetUtcOffset(localTime);

            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo timeZoneRow in timeZones)
            {
                sTimezoneID = timeZoneRow.Id;
                sDisplayName = timeZoneRow.DisplayName;
                sStandardName = timeZoneRow.StandardName;
                tOffset = timeZoneRow.GetUtcOffset(baseUTC);

                DateTime dtNow2 = DateTime.UtcNow;
               
                TimeZoneInfo estTimeZone = TimeZoneInfo.FindSystemTimeZoneById(sTimezoneID);
                DateTime estTimeZone2 = TimeZoneInfo.ConvertTimeFromUtc(dtNow2, timeZoneRow);
                TimeSpan tsDiff = estTimeZone2 - dtNow2;

                BLL.TimezoneInsert(sDisplayName, sStandardName, tsDiff.TotalHours, "");              
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   12/Oct/2015
        /// Description:    Call webservice to verify if the user is authenticated through LDAP
        /// </summary>
        /// <returns></returns>
        private void AuthenticateLDAP()
        {        
            string sLDAPUser = "";
            string sLDAPID = "";
            string sLDAPValidated = "";
            
            try
            {
               

                //if (GlobalCode.Field2String( Request.Headers["X-API-SID"]) != "")
                //{
                //    sLDAPID = GlobalCode.Field2String(Request.Headers["X-API-SID"]);
                //}
                //if (GlobalCode.Field2String(Request.Headers["X-API-User"]) != "")
                //{
                //    sLDAPUser = GlobalCode.Field2String(Request.Headers["X-API-User"]);
                //}

                if (GlobalCode.Field2String(Request.QueryString["API-SID"]) != "")
                {
                    sLDAPID = GlobalCode.Field2String(Request.QueryString["API-SID"]);
                }
                if (GlobalCode.Field2String(Request.QueryString["API-User"]) != "")
                {
                    sLDAPUser = GlobalCode.Field2String(Request.QueryString["API-User"]);
                }

                //lblLDAPUser.Text = "User: " + sLDAPUser;
                //lblLDAPSID.Text = "SID: " + sLDAPID;

                //test
                //if (sLDAPUser != "")
                {                    

                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        string sAPI = MUser.GetLDAP();
                        client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers

                       ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                        //verify if the username exist in LDAP
                        string sResult = client.DownloadString(sAPI + "search?user=" + sLDAPUser);

                        string[] sResultArray;
                        sResultArray = sResult.Split(",{".ToCharArray());

                        string sUserName = "";
                        string[] sUserNameArray;
                        for (int i = 0; i < sResultArray.Count(); i++)
                        {
                            if (sResultArray[i].Contains("\"uid\""))
                            {
                                sUserNameArray = sResultArray[i].Split(":".ToCharArray());
                                sUserName = sUserNameArray[1].Replace("\"", "");
                                sUserName = sUserName.Replace("}", "");
                            }
                        }

                        //test

                        //string sResultTest = client.DownloadString(sAPI + "search?user=" + "jc.gad08@gmail132.com");
                        
                        //LDAP_search testSer = new LDAP_search();
                        //testSer = Parse.JsonHelper.JsonDeserialize<LDAP_search>(sResultTest);

                        //string sResultTestRemove ="{\"status\":true,\"statuscode\":200,\"result\":{\"entries\":{\"uid\":\"jc.gad08@gmail132.com\",\"cns\":[\"travel_mart\",\"mstr\",\"ctrac_employee\"],\"rwid\":\"80223030\",\"mail\":\"jc.gad08@gmail132.com\",\"sn\":\"TMLastName\",\"givenname\":\"TM-New User 4 M TMLastName\",\"is_migrated\":\"1\"},\"found\":1,\"member\":[\"travel_mart\",\"mstr\",\"ctrac_employee\"]},\"message\":\"Lists results found.\"}";
                        //string sResultValid = sResult.Replace("\n", "");
                        //LDAP_search testSer2 = new JavaScriptSerializer().Deserialize<LDAP_search>(sResult);

                        //string jsonString = Parse.JsonHelper.JsonSerializer<LDAP_search>(testSer);

                        

                        //verify if the sid is valid for the user
                        string sAPI_sid = sAPI + "sid";
                        string sAPI_param = "sid=" + sLDAPID;
                        string sSID_User = "";
                        string sSID_Message = "";
                        string sSID_Company = "";

                        string[] sSID_MessageArray;


                        //verify if the sid is valid in LDAP
                        client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        sResult = client.UploadString(sAPI_sid, sAPI_param);
                        sResultArray = sResult.Split(",{".ToCharArray());

                        for (int i = 0; i < sResultArray.Count(); i++)
                        {
                            if (sResultArray[i].Contains("\"message\""))
                            {
                                sSID_MessageArray = sResultArray[i].Split(":".ToCharArray());
                                sSID_Message = sSID_MessageArray[1].Replace("\"", "");
                                sSID_Message = sSID_Message.Replace("}", "");
                            }

                            if (sResultArray[i].Contains("\"user\""))
                            {
                                sSID_MessageArray = sResultArray[i].Split(":".ToCharArray());
                                sSID_User = sSID_MessageArray[1].Replace("\"", "");
                                sSID_User = sSID_User.Replace("}", "");
                            }
                            if (sResultArray[i].Contains("\"cn\""))
                            {
                                sSID_MessageArray = sResultArray[i].Split(":".ToCharArray());
                                sSID_Company = sSID_MessageArray[1].Replace("\"", "");
                                sSID_Company = sSID_Company.Replace("}", "");
                            }
                        }                        
                        if (sSID_Message != "Session is valid.")
                        {
                            Panel2.Visible = true;
                            LabelLoginErrorDetails.Text = "LDAP Authentication: Invalid Session ID!";
                            return;
                        }
                        if (sSID_User.ToLower() != sUserName.ToLower())
                        {
                            Panel2.Visible = true;
                            LabelLoginErrorDetails.Text = "LDAP Authentication: Username is not valid!";
                            return;
                        }

                        string sAPICompany = MUser.GetLDAPCompany();
                        if (sSID_Company.ToLower() != sAPICompany.ToLower())
                        {
                            Panel2.Visible = true;
                            LabelLoginErrorDetails.Text = "LDAP Authentication: Invalid company!";
                            return;
                        }

                        sLDAPValidated = "Validated: Yes";

                        if (Membership.GetUser(sUserName) != null)
                        {
                            
                            Login1.UserName = sUserName;
                            LoginUser();

                            UserAccountBLL.UpdateUserSessionID_LDAP(sUserName, sLDAPID);
                            FormsAuthentication.SetAuthCookie(Login1.UserName, true);
                            //FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);

                            MembershipUser mUser = Membership.GetUser(Login1.UserName);
                            DateTime dt = DateTime.Now;

                            mUser.LastLoginDate = dt;
                            mUser.LastActivityDate = dt;
                            Membership.UpdateUser(mUser);

                           // lblLDAPRedirectPage.Text = "Page to redirect: LoginProcess.aspx";
                            Response.Redirect("LoginProcess.aspx", false);
                            //Response.Redirect("UnderMaintenance.html", false);
                        }
                        else
                        {
                            Panel2.Visible = true;
                            LabelLoginErrorDetails.Text = "TM Authentication: Username is not valid!";
                        }                      
                    }
                }
                
            }
            catch (Exception ex)
            {
                string sMsg = "LDAP Error: " + ex.Message;
                sMsg = sMsg + "\n SID:" + sLDAPID ;
                sMsg = sMsg + "\n Username:" + sLDAPUser;
                sMsg = sMsg + "\n Validated:" + sLDAPValidated;

                AlertMessage(sMsg);
            }
        }
        private void RedirectToLDAP()
        {
            if (GlobalCode.Field2String(Request.QueryString["API-SID"]) == "" ||
               GlobalCode.Field2String(Request.QueryString["API-User"]) == "")
            {
                string sLDAP_Login = MUser.GetLDAPLogin();
                Response.Redirect(sLDAP_Login);
            }
        }
        //For dev use only
        private void ResetPassword()
        {
            MembershipUser mUser = Membership.GetUser("wali.finance");          
            if (mUser.IsLockedOut)
            {
                mUser.UnlockUser();
            }
            string str = mUser.ResetPassword();
            mUser.ChangePassword(str,"@muhallidin");
            mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
            Membership.UpdateUser(mUser);
        }
    }     
}

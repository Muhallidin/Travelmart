using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Collections;

namespace TRAVELMART.Common
{
    public static class MUser
    {
        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// Description: Get login username        
        /// </summary>
        public static string GetUserName()
        {
            string _username = HttpContext.Current.User.Identity.Name;
            return _username;
        }
        /// <summary>
        /// Date Created:    18/08/2011
        /// Created By:      Josephine Gad
        /// Description:     Get login role   
        /// --------------------------------
        /// Date Modified:   27/10/2011
        /// Modified By:     Josephine Gad
        /// Description:     get the primary role of the user 
        /// </summary>
        public static string GetUserRole()
        {
            //return Roles.GetRolesForUser(GetUserName())[0];            
            return GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]);

        }

        public static string GetLDAP()
        {
            return ConfigurationSettings.AppSettings["LDAP-API"].ToString();
        }

        public static string GetLDAPCompany()
        {
            return ConfigurationSettings.AppSettings["LDAP-Company"].ToString();
        }
        public static string GetLDAPLogin()
        {
            return ConfigurationSettings.AppSettings["LDAP-Login"].ToString();
        }
        public static string GetLDAPLoginIsON()
        {
            return ConfigurationSettings.AppSettings["LDAP-Login-Is-On"].ToString();
        }
        public static string GetLDAPUrl()
        {
            return ConfigurationSettings.AppSettings["LDAP-URL-Main"].ToString();
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   14/Oct/2015
        /// Description:    Call API to update password in LDAP
        /// </summary>
        /// <returns></returns>
        public static void DeactivateUserInLDAP(string sUsername)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = MUser.GetLDAP();
                    client.Headers.Add("content-type", "application/json");
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    string sAPI_URL = sAPI + "deactivate";
                    string sAPI_param = "user=" + sUsername;
                    sAPI_param = sAPI_param + "&company=" + MUser.GetLDAPCompany();

                    string sResult = client.UploadString(sAPI_URL, sAPI_param);
                }
            }
            catch (Exception ex)
            {
                string sMsg = "LDAP Error: " + ex.Message;
            }
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   14/Oct/2015
        /// Description:    Call API to update password in LDAP
        /// </summary>
        /// <returns></returns>
        public static void ChangePasswordInLDAP(string sUsername, string sNewPassword)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = MUser.GetLDAP();
                    client.Headers.Add("content-type", "application/json");
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    string sAPI_URL = sAPI + "resetpass";
                    string sAPI_param = "user=" + sUsername;
                    sAPI_param = sAPI_param + "&pass=" + sNewPassword;

                    string sResult = client.UploadString(sAPI_URL, sAPI_param);
                }
            }
            catch (Exception ex)
            {
                string sMsg = "LDAP Error: " + ex.Message;
            }
        }
        /// <summary>
        /// Date Created:   28/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Add/Edit user from LDAP
        /// </summary>
        public static string AddEditUserFromLDAP(string strUsername, string strFName,
            string strLName, string strEmail, string strPWD, string sType, string strEmailNew)
        {
            string sReturn = "";
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = MUser.GetLDAP();


                    client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers


                    //verify if the username exist in LDAP
                    string sResult = client.DownloadString(sAPI + "search?user=" + strEmail);

                    //JavaScriptSerializer jResult = new JavaScriptSerializer();
                    //object oResult = jResult.DeserializeObject(sResult);
                    


                    Dictionary<string, object> ArrayResult = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(sResult);
                    Dictionary<string, object> ArrayResultChild;


                    string[] sResultArray;
                    sResultArray = sResult.Split(",{".ToCharArray());

                    string sUserName = "";
                    string sCompany = "";
                    //bool IsExistinLDAP = false;

                    foreach (var arrayValue in ArrayResult)
                    {
                        var key = arrayValue.Key;
                        var value = arrayValue.Value;

                        if (key == "status")
                        {
                            if (GlobalCode.Field2String(value).ToLower() == "true")
                            {
                                sUserName = strUsername;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (key == "result")
                        {
                            if (value != null)
                            {
                                ArrayResultChild = (Dictionary<string, object>)value;
                                foreach (var ResultChild in ArrayResultChild)
                                {
                                    var resultChildKey = ResultChild.Key;
                                    var resultChildValue = ResultChild.Value;

                                    //if (resultChildKey == "entries")
                                    //{
                                    //    if (resultChildValue != null)
                                    //    {
                                    //        ArrayResultEntries = (Dictionary<string, object>)resultChildValue;
                                    //        foreach (var resultEntries in ArrayResultEntries)
                                    //        {
                                    //            if (resultEntries.Key == "uid")
                                    //                sUserName = GlobalCode.Field2String(resultEntries.Value);
                                    //        }
                                    //    }
                                    //}
                                    if (resultChildKey == "member")
                                    {
                                        if (resultChildValue != null)
                                        {
                                            var arrCompanyValue = (ArrayList)resultChildValue;//(List<string>)resultChildValue;

                                            for (var i = 0; i < arrCompanyValue.Count; i++)
                                            {
                                                if (GlobalCode.Field2String(arrCompanyValue[i]) == MUser.GetLDAPCompany())
                                                {
                                                    sCompany = GlobalCode.Field2String(GlobalCode.Field2String(arrCompanyValue[i]));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                    client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";


                    //if user exist in LDAP TM, do not do anything
                    if (strUsername == sUserName && sCompany == MUser.GetLDAPCompany())
                    {
                        //Update user
                        string sEditUser = sAPI + "modify";
                        string sParameter = "user=" + strUsername;
                        //sParameter = sParameter + "&firstname=" + strFName;
                        //sParameter = sParameter + "&middlename=" + "";
                        //sParameter = sParameter + "&lastname=" + strLName;
                        //sParameter = sParameter + "&description=EditedFromTravelmartSite";
                        //sParameter = sParameter + "&active=1";

                        //sResult = client.UploadString(sEditUser, sParameter);

                        //Update Email
                        sEditUser = sAPI + "changemail";
                        sParameter = "user=" + strUsername;
                        sParameter = sParameter + "&email=" + strEmailNew;

                        client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        sResult = client.UploadString(sEditUser, sParameter);

                        sResultArray = sResult.Split(",{".ToCharArray());
                        string[] sSuccessArray;
                        string sSuccessfull;

                        for (int i = 0; i < sResultArray.Count(); i++)
                        {
                            if (sResultArray[i].Contains("\"message\""))
                            {
                                sSuccessArray = sResultArray[i].Split(":".ToCharArray());
                                sSuccessfull = sSuccessArray[1].Replace("\"", "");
                                sReturn = sSuccessfull.Replace("}", "");
                            }
                        }

                        sReturn = " User Edit: " + sReturn;
                    }

                    //if user exist but no TM app, password is blank
                    else if (strUsername == sUserName && sCompany != MUser.GetLDAPCompany())
                    {
                        string sAddUser = sAPI + "add";
                        string sParameter = "user=" + strUsername;
                        sParameter = sParameter + "&pass=";
                        sParameter = sParameter + "&email=" + strEmail;
                        sParameter = sParameter + "&firstname=" + strFName;
                        sParameter = sParameter + "&middlename=" + "";
                        sParameter = sParameter + "&lastname=" + strLName;
                        sParameter = sParameter + "&description=AddedFromTravelmartSite";
                        sParameter = sParameter + "&company=" + MUser.GetLDAPCompany();


                        sResult = client.UploadString(sAddUser, sParameter);
                        sResultArray = sResult.Split(",{".ToCharArray());
                        string[] sSuccessArray;
                        string sSuccessfull;

                        for (int i = 0; i < sResultArray.Count(); i++)
                        {
                            if (sResultArray[i].Contains("\"message\""))
                            {
                                sSuccessArray = sResultArray[i].Split(":".ToCharArray());
                                sSuccessfull = sSuccessArray[1].Replace("\"", "");
                                sReturn = sSuccessfull.Replace("}", "");
                            }
                        }
                        sReturn = " User Add with Other App: " + sReturn;
                    }
                    else
                    {
                        if (sType == "Edit")
                        {
                            MembershipUser mUser = Membership.GetUser(strUsername);
                            string sPassword = DateTime.Now.ToLongTimeString().Replace(" ", "").Replace(":", "");
                            strPWD = sPassword;
                        }

                        string sAddUser = sAPI + "add";
                        string sParameter = "user=" + strUsername;
                        sParameter = sParameter + "&pass=" + strPWD;
                        sParameter = sParameter + "&email=" + strEmail;
                        sParameter = sParameter + "&firstname=" + strFName;
                        sParameter = sParameter + "&middlename=" + "";
                        sParameter = sParameter + "&lastname=" + strLName;
                        sParameter = sParameter + "&description=FirstAddedFromTravelmartSite";
                        sParameter = sParameter + "&company=" + MUser.GetLDAPCompany();


                        sResult = client.UploadString(sAddUser, sParameter);
                        sResultArray = sResult.Split(",{".ToCharArray());
                        string[] sSuccessArray;
                        string sSuccessfull;

                        for (int i = 0; i < sResultArray.Count(); i++)
                        {
                            if (sResultArray[i].Contains("\"message\""))
                            {
                                sSuccessArray = sResultArray[i].Split(":".ToCharArray());
                                sSuccessfull = sSuccessArray[1].Replace("\"", "");
                                sReturn = sSuccessfull.Replace("}", "");
                            }
                        }
                        sReturn = " User Add New User: " + sReturn;
                    }
                    return sReturn;
                }
            }
            catch (Exception ex)
            {
                string sMsg = sReturn + " - " + ex.Message;
                throw ex;
            }
            finally
            {
                sReturn = "";
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   30/Oct/2015
        /// Description:    Check if Session valid in LDAP
        /// </summary>
        public static bool IsLDAPSessionValid(string sLDAPUser, string sLDAPSid)
        {
            bool bReturn = false;
            try
            {
                if (sLDAPSid == "")
                {
                    return true;
                }
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = MUser.GetLDAP();
                    client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers


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

                    //verify if the sid is valid for the user
                    string sAPI_sid = sAPI + "sid";
                    string sAPI_param = "sid=" + sLDAPSid;
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
                        //LabelLoginErrorDetails.Text = "LDAP Authentication: Invalid Session ID!";
                        return false;
                    }
                    if (sSID_User.ToLower() != sUserName.ToLower())
                    {
                        //LabelLoginErrorDetails.Text = "LDAP Authentication: Username is not valid!";
                        return false;
                    }

                    string sAPICompany = MUser.GetLDAPCompany();
                    if (sSID_Company.ToLower() != sAPICompany.ToLower())
                    {
                        //LabelLoginErrorDetails.Text = "LDAP Authentication: Invalid company!";
                        return false;
                    }
                    bReturn = true;
                }
                return bReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<UserList_LDAP> GetLDAPUser(string sEmail)
        {
            List<UserList_LDAP> list = new List<UserList_LDAP>();

            string sCompany;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                string sAPI = MUser.GetLDAP();


                client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers


                //verify if the username exist in LDAP
                string sResult = client.DownloadString(sAPI + "search?user=" + sEmail);

                Dictionary<string, object> ArrayResult = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(sResult);
                Dictionary<string, object> ArrayResultChild;


                string[] sResultArray;
                sResultArray = sResult.Split(",{".ToCharArray());


                foreach (var arrayValue in ArrayResult)
                {
                    var key = arrayValue.Key;
                    var value = arrayValue.Value;

                    if (key == "status")
                    {
                        if (GlobalCode.Field2String(value).ToLower() == "true")
                        {

                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (key == "result")
                    {
                        if (value != null)
                        {
                            ArrayResultChild = (Dictionary<string, object>)value;
                            foreach (var ResultChild in ArrayResultChild)
                            {
                                var resultChildKey = ResultChild.Key;
                                var resultChildValue = ResultChild.Value;

                                //if (resultChildKey == "entries")
                                //{
                                //    if (resultChildValue != null)
                                //    {
                                //        ArrayResultEntries = (Dictionary<string, object>)resultChildValue;
                                //        foreach (var resultEntries in ArrayResultEntries)
                                //        {
                                //            if (resultEntries.Key == "uid")
                                //                sUserName = GlobalCode.Field2String(resultEntries.Value);
                                //        }
                                //    }
                                //}
                                if (resultChildKey == "member")
                                {
                                    if (resultChildValue != null)
                                    {
                                        var arrCompanyValue = (ArrayList)resultChildValue;//(List<string>)resultChildValue;

                                        for (var i = 0; i < arrCompanyValue.Count; i++)
                                        {
                                            if (GlobalCode.Field2String(arrCompanyValue[i]) == MUser.GetLDAPCompany())
                                            {
                                                sCompany = GlobalCode.Field2String(GlobalCode.Field2String(arrCompanyValue[i]));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }

            return list;
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   30/Oct/2015
        /// Description:    Check if Session valid in LDAP
        /// </summary>
        public static string EmailUserLDAP(string sUser, string sPurpose)
        {
            string sReturn = "";
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = MUser.GetLDAPUrl();
                    client.Headers.Add("content-type", "application/json");//set your header here, you can add multiple headers
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    

                    string sParam = "username=" + sUser + "&company=" + MUser.GetLDAPCompany() + "&purpose=" + sPurpose;
                    //string sResult = client.DownloadString(sAPI + "emailcenter.php" + sParam);
                    string sResult = client.UploadString(sAPI + "emailcenter.php", sParam);

                    string[] sResultArray;
                    sResultArray = sResult.Split(",{".ToCharArray());

                    string sStatus = "";
                    string[] sStatusArray;
                    for (int i = 0; i < sResultArray.Count(); i++)
                    {
                        if (sResultArray[i].Contains("\"statuscode\""))
                        {
                            sStatusArray = sResultArray[i].Split(":".ToCharArray());
                            sStatus = sStatusArray[1].Replace("\"", "");
                            sStatus = sStatus.Replace("}", "");
                        }
                    }

                    ////verify if the sid is valid for the user
                    //string sAPI_sid = sAPI + "sid";
                    //string sAPI_param = "sid=" + sLDAPSid;
                    //string sSID_User = "";
                    //string sSID_Message = "";
                    //string sSID_Company = "";

                    //string[] sSID_MessageArray;


                    ////verify if the sid is valid in LDAP
                    //client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    //sResult = client.UploadString(sAPI_sid, sAPI_param);
                    //sResultArray = sResult.Split(",{".ToCharArray());

                    //for (int i = 0; i < sResultArray.Count(); i++)
                    //{
                    //    if (sResultArray[i].Contains("\"message\""))
                    //    {
                    //        sSID_MessageArray = sResultArray[i].Split(":".ToCharArray());
                    //        sSID_Message = sSID_MessageArray[1].Replace("\"", "");
                    //        sSID_Message = sSID_Message.Replace("}", "");
                    //    }

                    //    if (sResultArray[i].Contains("\"user\""))
                    //    {
                    //        sSID_MessageArray = sResultArray[i].Split(":".ToCharArray());
                    //        sSID_User = sSID_MessageArray[1].Replace("\"", "");
                    //        sSID_User = sSID_User.Replace("}", "");
                    //    }
                    //    if (sResultArray[i].Contains("\"cn\""))
                    //    {
                    //        sSID_MessageArray = sResultArray[i].Split(":".ToCharArray());
                    //        sSID_Company = sSID_MessageArray[1].Replace("\"", "");
                    //        sSID_Company = sSID_Company.Replace("}", "");
                    //    }
                    //}

                    sReturn = sStatus;
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   15/Dec/2015
        /// Description:    Call API to logout user in LDAP
        /// </summary>
        /// <returns></returns>
        public static void LogoutUserInLDAP(string sUser)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = MUser.GetLDAP();
                    client.Headers.Add("content-type", "application/json");
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    string sAPI_URL = sAPI + "signout";
                    string sAPI_param = "user=" + sUser;//GlobalCode.Field2String(Session["UserName"]);
                    sAPI_param = sAPI_param + "&company=" + MUser.GetLDAPCompany();

                    string sResult = client.UploadString(sAPI_URL, sAPI_param);
                }
            }
            catch (Exception ex)
            {
                string sMsg = "LDAP Error: " + ex.Message;
                throw ex;
            }
        }
    }
     
    /// <summary>
    /// Date Created:    26/03/2012
    /// Created By:      Josephine Gad
    /// Description:     Set User's variable for session
    /// --------------------------------
    /// </summary>
    public class UserClass
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }

        public DateTime? DateFrom { get; set; }

        [System.ComponentModel.DefaultValue("0")]
        public string Region { get; set; }

        [System.ComponentModel.DefaultValue("0")]
        public string Country { get; set; }

        [System.ComponentModel.DefaultValue("0")]
        public string City { get; set; }

        [System.ComponentModel.DefaultValue("0")]
        public string Port { get; set; }

        [System.ComponentModel.DefaultValue("0")]
        public string Hotel { get; set; }

        [System.ComponentModel.DefaultValue("0")]
        public string ViewRegion { get; set; }
    }
    /// <summary>
    /// Date Created:    15/08/2012
    /// Created By:      Josephine Gad
    /// Description:     User's List
    /// --------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class UserAccountList
    {
        public string sRole { get; set; }
        public bool bIsPrimary { get; set; }
        public int iDayNo { get; set; }
        public bool bIsAirportMeetGreet { get; set; }
        public bool bIsSeaportMeetGreet { get; set; }
    }
    [Serializable]
    public class UserPrimaryDetails
    {
        public string sFirstName { get; set; }
        public int iBranchID { get; set; }
        public string sBranchName { get; set; }
        public int iVendorID { get; set; }
    }
    public class UserAccountGenericClass
    {
        public List<UserAccountList> UserAccountList { get; set; }
        public List<UserPrimaryDetails> UserPrimaryDetails { get; set; }
    }
    
    public class ActiveUserEmail
    {
        public string sEmail { get; set; }
    }
    /// <summary>
    /// Date Created:    11/Nov/2014
    /// Created By:      Josephine Monteza
    /// Description:     User's List
    /// --------------------------------
    /// </summary>
    public class UserList
    {
        public Guid sUserID { get; set; }

        public string sEnc { get; set; }
        public string sDec { get; set; }
        

        public string sUserName { get; set; }
        public string sUserEmail { get; set; }
        public string sLastName { get; set; }
        public string sFirstName { get; set; }
        public DateTime dDateCreated { get; set; }
    }
     /// <summary>
    /// Date Created:    03/Nov/2015
    /// Created By:      Josephine Monteza
    /// Description:     User's List for LDAP
    /// --------------------------------
    /// </summary>
    public class UserList_LDAP
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Group { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Role { get; set; }
        public string AlternateEmail { get; set; }
    }

    //public class LDAP_search
    //{
    //    public string status;
    //    public string statuscode;
    //    public object result;
    //    public string entries;
    //    public string found;
    //    public string member;
    //    public string message;
    //}
    //public class result
    //{ 
    
    //}

    //public class LDAP_search_result
    //{
    //    public string uid;
    //    public string cns;
    //    public string rwid;
    //    public string mail;
    //    public string sn;
    //    public string givenname;
    //    public string is_migrated;
       
    //}

    //public class LDAP_search
    //{
    //    public string status { get; set; }
    //    public string statuscode { get; set; }
    //    public LDAP_search_result result { get; set; }        
    //    public string message { get; set; }
    //}
    //public class LDAP_search_result
    //{

    //    public int found { get; set; }
    //    public LDAP_search_member member { get; set; }
    //    public LDAP_search_result_entries entries { get; set; }
    //}
    //public class LDAP_search_member
    //{
    //    public string company { get; set; }
    //}
    //public class LDAP_search_result_entries
    //{
    //    public string uid { get; set; }
    //    public string cns { get; set; }
    //    public string rwid { get; set; }
    //    public string mail { get; set; }
    //    public string sn { get; set; }
    //    public string givenname { get; set; }
    //    public string is_migrated { get; set; }
    //}    
}

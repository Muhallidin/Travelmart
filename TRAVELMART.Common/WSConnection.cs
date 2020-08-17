using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;

namespace TRAVELMART.Common
{
    public class WSConnection
    {
        public static string GetPortalAPI()
        {
            return ConfigurationSettings.AppSettings["Portal-API"].ToString();
        }
        /// Author:         Josephine Monteza
        /// Date Created:   29/Jan/2016
        /// Description:    Call API to save email through LDAP API
        /// </summary>
        /// <returns></returns>
        public static void PortalInboxSave(string user_id, string sender_id, string subject,
            string message, string email, string module_id)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    string sAPI = WSConnection.GetPortalAPI();
                    client.Headers.Add("content-type", "application/json");
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                    string sAPI_URL = sAPI + "inboxcenter.php";
                    string sAPI_param = "user_id=" + user_id;
                    sAPI_param = sAPI_param + "&sender_id=" + sender_id;
                    sAPI_param = sAPI_param + "&subject=" + subject;
                    sAPI_param = sAPI_param + "&message=" + message;
                    sAPI_param = sAPI_param + "&email=" + email;
                    sAPI_param = sAPI_param + "&module_id=" + module_id;

                    string sResult = client.UploadString(sAPI_URL, sAPI_param);
                }
            }
            catch (Exception ex)
            {
                string sMsg = "Portal Error: " + ex.Message;
            }
        }
        /// <summary>
        /// Date Modified:  03/Jan/2018
        /// Modified By:    JMonteza
        /// (description)   Save photo in Directory and call TM-API to save it in Panda
        /// </summary>
        /// <param name="sIDName"></param>
        /// <param name="sIDValue"></param>
        /// <param name="sFileExt"></param>
        /// <param name="sEntityType"></param>
        /// <returns></returns>
        public static bool SaveToMediaServer(string sIDName, string sIDValue, string sFileExt, string sEntityType)
        {
            bool bReturn = false;

            try
            {

                using (WebClient wc = new WebClient())
                {
                    string sURL = ConfigurationManager.AppSettings["TM-API"].ToString();
                    sURL += "/AddEditPhotoFromDir";

                    wc.QueryString.Add("IDName", sIDName);
                    wc.QueryString.Add("IDValue", sIDValue);
                    wc.QueryString.Add("FileExtension", sFileExt);
                    wc.QueryString.Add("EntityType", sEntityType);

                    var data = wc.UploadValues(sURL, "POST", wc.QueryString);

                    // data here is optional, in case we recieve any string data back from the POST request.
                    var responseString = UnicodeEncoding.UTF8.GetString(data);
                    bReturn = true;
                    return bReturn;
                }
            }
            catch (Exception ex)
            {
                string sMsg = "TM Error: " + ex.Message;
                //AlertMessage(sMsg, true);
                return bReturn;
            }

        }
    }
}

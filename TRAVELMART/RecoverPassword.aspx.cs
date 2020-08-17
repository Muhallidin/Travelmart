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

namespace TRAVELMART
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
        #region "EVENTS"
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        ///===================================================
        /// <summary>            
        /// Modified Created: 03/11/2015
        /// Created By: Muhallidin Wali
        /// (description) Get alternate email list of users                        
        ///===================================================
        protected void Submit_Click(object sender, EventArgs e)
        {

            string alternate = "";
            alternate = UserAccountBLL.GetUserInfoAlterEmail(PasswordRecovery1.UserName);
            MembershipUser mUser = Membership.GetUser(PasswordRecovery1.UserName);
           
            if (mUser == null)
            {
                Panel2.Visible = true;
            }
            else
            {                
                mUser.UnlockUser();
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);

                Panel2.Visible = false;
                string Password = mUser.ResetPassword();
                string sEmail = mUser.Email.ToString();
              
                MUser.ChangePasswordInLDAP(mUser.UserName, Password);
                if (alternate != "") sEmail = alternate;

                SendEmail(sEmail, Password);
                //Server.Transfer("Login.aspx");
            }
        }
        protected void RecoverPassword_Click(object sender, EventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(PasswordRecovery1.UserName);

            if (mUser == null)
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
                string Password = Membership.GetUser("hotel").ResetPassword();

                //Membership.EnablePasswordRetrieval;
                //string userEmail = mUser.Email.ToString();
                //string Password
                //SendEmail(mUser.Email, Password);
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void PasswordRecovery1_SendMailError(object sender, SendMailErrorEventArgs e)
        {
            AlertMessage(e.Exception.ToString());
        }

       
        #endregion

        #region "FUNCTIONS"
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  10/04/2012
        /// Description:    Change SendAsyncEmail to CommonFunctions.SendEmail
        ///                 Add style to the message, add email add of user in Alert
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="Password"></param>
        protected void SendEmail(string userEmail, string Password)
        {
            try
            {
                string sSubject = "Travelmart Password Recovery";
                string sBody = "";
                sBody = "<table style=\"font-family:Tahoma; font-size:11px\"><tr><td colspan=\"2\">To log on to <a href=\"https://rclcrew.com\">Travelmart System</a>, use the following credentials:</td></tr>";
                sBody += "<tr><td colspan=\"2\"></td></tr>";
                sBody += "<tr><td><br/><b>Username: </b></td>";
                sBody += "<td><br/>" + PasswordRecovery1.UserName + "</td></tr>";
                sBody += "<tr><td><b>Password: </b></td>";
                sBody += "<td>" + Password + "</td></tr>";
                sBody += "<tr><td colspan=\"2\"></td></tr>";
                sBody += "<tr><td colspan=\"2\"><br/>If you have any questions or encounter any problems" +
                    " logging in, please contact a site administrator.</td></tr></table>";

                //CommonFunctions sendmail = new CommonFunctions();
                //sendmail.SendAsyncEmail(userEmail, sSubject, sBody);
                CommonFunctions.SendEmail("",userEmail, sSubject, sBody);

                AlertMessage("Your password has been sent to your email address - " + userEmail + ".");
                //Response.Redirect("Login.aspx");                
            }
            catch (Exception ex)
            {               
                AlertMessage(ex.Message);
                //Response.Redirect("Login.aspx");                
            }
        }
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "window.location = 'Default.aspx'";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
        }
        #endregion
    }
}

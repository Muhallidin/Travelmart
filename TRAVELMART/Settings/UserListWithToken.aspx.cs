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

namespace TRAVELMART.Settings
{
    public partial class UserListWithToken : System.Web.UI.Page
    {
        #region EVENTS       
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "User List with Token";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                //GetRoles();   
                //ListView1.DataSource = null;
                //ListView1.DataBind();
            }
           
        }
        protected void uoButtonUserList_Click(object sender, EventArgs e)
        {
            List<UserList> list = new List<UserList>();
            list = EPortalBLL.GetUserList();
            uoGridViewUsers.DataSource = list;
            uoGridViewUsers.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Mar/2013
        /// Description:    Email Notification when the system is under maintenance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void uoButtonEmail_Click(object sender, EventArgs e)
        //{
        //    List<ActiveUserEmail> list = new List<ActiveUserEmail>();
        //    list = EmailBLL.GetActiveUserEmail();
    
        //    List<ActiveUserEmail> listEmailed = new List<ActiveUserEmail>();
        //    ActiveUserEmail item;

        //    if (list.Count > 0)
        //    {
                
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            if (SendEmail_UnderMaintenance(list[i].sEmail))
        //            {
        //                item = new ActiveUserEmail();
        //                item.sEmail = list[i].sEmail;
        //                listEmailed.Add(item);
        //            }
        //        }
        //        if (listEmailed.Count > 0)
        //        {

        //            ListView1.DataSource = null;
        //            ListView1.DataBind();

        //            uoUserList.DataSource = listEmailed;
        //            uoUserList.DataBind();
        //        }
        //    }
        //}
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Mar/2013
        /// Description:    Email Notification when the system resumes
        /// </summary>
        //protected void uoButtonResume_Click(object sender, EventArgs e)
        //{
        //    List<ActiveUserEmail> list = new List<ActiveUserEmail>();
        //    list = EmailBLL.GetActiveUserEmail();

        //    List<ActiveUserEmail> listEmailed = new List<ActiveUserEmail>();
        //    ActiveUserEmail item;

        //    if (list.Count > 0)
        //    {

        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            if (SendEmail_Resume(list[i].sEmail))
        //            {
        //                item = new ActiveUserEmail();
        //                item.sEmail = list[i].sEmail;
        //                listEmailed.Add(item);
        //            }
        //        }
        //        if (listEmailed.Count > 0)
        //        {

        //            ListView1.DataSource = null;
        //            ListView1.DataBind();

        //            uoUserList.DataSource = listEmailed;
        //            uoUserList.DataBind();
        //        }
        //    }
        //}      
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
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Mar/2013
        /// Description:    Email Notification: Under Maintenance
        /// </summary>
        /// <param name="userEmail"></param>
        //protected bool SendEmail_UnderMaintenance(string userEmail)
        //{
        //    try
        //    {
        //        string sSubject = "Travelmart Under Maintenance Advisory";
        //        string sBody = "";

        //        sBody = "<table style=\"font-family:Tahoma; font-size:12px; width:100%;text-align:left \" ><tr><td>Dear TM User,</td></tr>";
        //        sBody += "<tr><td><br/><br/>Travelmart will be taken down for maintenance next hour. ";
        //        sBody += "The site cannot be used for a while.</td></tr>";
        //        sBody += "<tr><td><br/>Email notification will be sent out once the service resumed.</td></tr>";
        //        sBody += "<tr><td><br/><br/>Thank you,</td></tr>";
        //        sBody += "<tr><td><br/>TM Development Team</td></tr>";
        //        sBody += "</table>";

        //        CommonFunctions.SendEmail("", userEmail, sSubject, sBody);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMessage(ex.Message);
        //        return false;
        //    }
            
        //}
        ///// <summary>
        ///// Author:         Josephine Gad
        ///// Date Created:   04/Mar/2013
        ///// Description:    Email Notification: Under Maintenance
        ///// </summary>
        ///// <param name="userEmail"></param>
        //protected bool SendEmail_Resume(string userEmail)
        //{
        //    try
        //    {
        //        string sSubject = "Travelmart Advisory";
        //        string sBody = "";

        //        sBody = "<table style=\"font-family:Tahoma; font-size:12px; width:100%;text-align:left \" ><tr><td>Dear TM User,</td></tr>";
        //        sBody += "<tr><td><br/><br/>Travelmart is now up and running. You may now access the system.";                                
        //        sBody += "<tr><td><br/><br/>Thank you,</td></tr>";
        //        sBody += "<tr><td><br/>TM Development Team</td></tr>";
        //        sBody += "</table>";

        //        CommonFunctions.SendEmail("", userEmail, sSubject, sBody);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMessage(ex.Message);
        //        return false;
        //    }

        //}        
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", sScript, false);
        }
        //protected void uoButtonDecrypt_Click(object sender, EventArgs e)
        //{
        //  uoLabelDecrypt.Text = CommonFunctions.DecryptString(uoTextBoxExncrypted.Text, uoTextBoxUserName.Text);
        //}
        #endregion

        

      
    }
}

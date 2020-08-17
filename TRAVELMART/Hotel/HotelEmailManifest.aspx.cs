using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART.Hotel
{
    public partial class HotelEmailManifest : System.Web.UI.Page
    {
        string BranchId = "";

        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) 'HotelEmailManifest' page load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DefaultValues();                
                BranchId = Request.QueryString["bId"];
                LoadEmailAddress(BranchId);
                uoButtonSaveAndSend.CommandArgument = BranchId;
            }
        }

        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Assign e-mail address to variable(s)        
        /// </summary>
        protected void uoButtonSend_Click(object sender, EventArgs e)
        {
            string EmailTo = uoTextBoxTo.Text;
            string EmailCc = uoTextBoxCc.Text;

            if (EmailTo != "")
            {
                Session.Add("EmailTo", EmailTo);
                Session.Add("EmailCc", EmailCc);

                CloseFancyBoxAndLoadParentPage();                
            }
            else
            {
                AlertMessage("Please specify at least one recipient.");
            }
        }

        /// <summary>
        /// Date Created: 28/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Save e-mail address and assign to variable(s)        
        /// </summary>
        protected void uoButtonSaveAndSend_OnCommand(object sender, CommandEventArgs e)
        {
            string EmailTo = uoTextBoxTo.Text;
            string EmailCc = uoTextBoxCc.Text;            

            if (e.CommandName == "SaveAndSend")
            {
                if (EmailTo != "")
                {
                    string bId = GlobalCode.Field2String(e.CommandArgument);
                    SaveEmailAddress(bId, EmailTo, EmailCc);

                    Session.Add("EmailTo", EmailTo);
                    Session.Add("EmailCc", EmailCc);

                    CloseFancyBoxAndLoadParentPage();
                }
                else
                {
                    AlertMessage("Please specify at least one recipient.");
                }
            }            
        }

        //protected void uoButtonSaveAndSend_Click(object sender, EventArgs e)
        //{
        //    string EmailTo = uoTextBoxTo.Text;
        //    string EmailCc = uoTextBoxCc.Text;
            
        //    if (EmailTo != "")
        //    {
        //        SaveEmailAddress(EmailTo, EmailCc);

        //        Session.Add("EmailTo", EmailTo);
        //        Session.Add("EmailCc", EmailCc);

        //        CloseFancyBoxAndLoadParentPage();
        //    }
        //    else
        //    {
        //        AlertMessage("Please specify at least one recipient.");
        //    }
        //}

        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Load default values
        /// </summary>
        protected void DefaultValues()
        {
            uoTextBoxTo.Text = "";
            uoTextBoxCc.Text = "";
        }

        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Load e-mail address
        /// </summary>
        protected void LoadEmailAddress(string BranchId)
        {
            IDataReader dr = null;
            try
            {
                dr = EmailBLL.LoadEmailAddress(BranchId);

                if (dr.Read())
                {
                    uoTextBoxTo.Text = dr["colEmailToVarchar"].ToString();
                    uoTextBoxCc.Text = dr["colEmailCcVarchar"].ToString();
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

        /// <summary>
        /// Date Created: 28/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Save e-mail address
        /// </summary>
        private void SaveEmailAddress(string bId, string EmailTo, string EmailCc)
        {            
            try
            {
                EmailBLL.SaveEmailAddress(bId, EmailTo, EmailCc);                                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Close this page and load parent page
        /// </summary>
        private void CloseFancyBoxAndLoadParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupSendEmail\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSend, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Show pop up alert message         
        /// </summary>
        private void AlertMessage(string s)
        {
            string aScript = "<script language='JavaScript'>";
            aScript += "alert('" + s + "');";
            aScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", aScript, false);
        }        
    }
}

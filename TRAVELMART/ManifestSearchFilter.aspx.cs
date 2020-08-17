using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART
{
    public partial class ManifestSearchFilter : System.Web.UI.Page
    {
        #region "Events"
       /// <summary>
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ufn"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }                
            }
        }

        /// <summary>
        /// Date Created:   09/02/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Assign filter value(s) to global variable(s) and redirect search result view to parent page
        /// 
        /// </summary>
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            Session["strSeafarerID"] = uoTextBoxSeafarerID.Text;
            Session["strSeafarerLN"] = uoTextBoxSeafarerLN.Text;
            Session["strSeafarerFN"] = uoTextBoxSeafarerFN.Text;
            Session["strRecLoc"] = uoTextBoxRecLoc.Text;

            Session["strVesselCode"] = uoTextBoxVesselCode.Text;
            Session["strVesselName"] = uoTextBoxVesselName.Text;

            //string strLogDescription = "";
            //string strFunction = "";
            
            //strLogDescription = "Search button for manifest list clicked.";
            //strFunction = "uoButtonSearch_Click";

            //DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

            if (uoTextBoxSeafarerID.Text != "" || uoTextBoxSeafarerLN.Text != "" || uoTextBoxSeafarerFN.Text != "" || uoTextBoxRecLoc.Text != "" || uoTextBoxVesselCode.Text != "" || uoTextBoxVesselName.Text != "")
            {
                RedirectUrlToParentPage();
            }
            else
            {
                //AlertMessage("Please enter filter value(s) of your choice.");
            }            
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   10/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Close this page and redirect url to parent page            
        /// </summary>
        private void RedirectUrlToParentPage()
        {            
            string sURL = "../ManifestSearchView2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_uoHiddenFieldSearch\").val(\"1\"); ";
            sScript += " window.parent.$(\"#ctl00_uoHiddenFieldRedirectURL\").val(\"" + sURL + "\"); ";            
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSearch, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created: 11/02/2012
        /// Created By:   Gelo Oquialda
        /// (description) Show pop up alert message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string aScript = "<script language='JavaScript'>";
            aScript += "alert('" + s + "');";
            aScript += "</script>";            
            ScriptManager.RegisterClientScriptBlock(uoButtonSearch, this.GetType(), "scr", aScript, false);
        }
        #endregion
    }
}

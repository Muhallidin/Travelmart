using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART.Hotel
{
    public partial class AddConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                uoTxtConfirmation.Focus();
                string tType = Convert.ToString(Request.QueryString["tType"]);
                uoTxtConfirmation.Text = tType;
            }
        }

        protected void uoBtnSave_Click(object sender, EventArgs e)
        {
            string BigIntId = Convert.ToString(Request.QueryString["BigID"]);
            string TravelIntId = Convert.ToString(Request.QueryString["TravelID"]);
            string BranchIntId = Convert.ToString(Request.QueryString["BranchId"]);                        
            AddUpdateConfirmation(BigIntId, TravelIntId, BranchIntId, uoTxtConfirmation.Text.Trim());
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   14/Jun/2013
        /// description     Add Fields for Audit Trail Use
        /// </summary>    
        private void AddUpdateConfirmation(string sIdBigint, string sTRId, string sBranch, string sConfirmation)
        {
            string strLogDescription;
            string sConfirmationNo = GlobalCode.Field2String((Request.QueryString["tType"]));
            if (sConfirmation.Trim() == "")
            {
                strLogDescription = "Hotel Confirmation No. Added";
            }
            else
            {
                strLogDescription = "Hotel Confirmation No. Edited";
            }
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            SeafarerTravelBLL.UpdateConfirmation(sIdBigint, sTRId, GlobalCode.Field2String(Session["UserName"]),
                GlobalCode.Field2String(Session["UserRole"]), sBranch, sConfirmation, strLogDescription,
                "AddUpdateConfirmation", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                CommonFunctions.GetDateTimeGMT(dateNow), dateNow);

            OpenParent();
            Session["ConfirmationTag"] = "1";
        }
        private void OpenParent()
        {           
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupConfirmation\").val(\"1\"); ";

            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoBtnSave, this.GetType(), "scr", sScript, false);

        }

    }
}

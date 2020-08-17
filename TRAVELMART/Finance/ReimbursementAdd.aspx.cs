using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.Finance
{
    public partial class ReimbursementAdd : System.Web.UI.Page
    {
        #region Declarations
        FinanceBLL fBLL = new FinanceBLL();
        public string PageTitle = "";
        #endregion


        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotgiue
        /// Date Created: 27/10/2011
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldCurrencyId.Value = Request.QueryString["cId"];
                uoTextBoxCurrency.Text = Request.QueryString["cName"];
                if (Request.QueryString["rId"] == "0")
                {
                    PageTitle = "Add Reimbursement";                    
                }
                else
                {
                    PageTitle = "Edit reimbursement";
                    SetDetails();
                }                
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            fBLL.SaveSeafarerReimbursement(Request.QueryString["rId"], uoTextBoxReimbursementName.Text, Request.QueryString["sfId"],
                Request.QueryString["mReqId"], Request.QueryString["tReqId"], uoTextBoxAmount.Text, uoHiddenFieldCurrencyId.Value,
                uoTextBoxRemarks.Text, GlobalCode.Field2String(Session["UserName"]));
            OpenParentPage();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/10/2011
        /// Description: close pop up and open parent page
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupReimbursement\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 28/10/2011
        /// Description: set default details upon edit
        /// </summary>
        private void SetDetails()
        {

            uoHiddenFieldCurrencyId.Value = Request.QueryString["cId"];
            uoTextBoxCurrency.Text = Request.QueryString["cName"];
            using (IDataReader dr = fBLL.LoadSeafarerReimbursementDetails(Request.QueryString["sfId"], Request.QueryString["rId"]))
            {
                if (dr.Read())
                {
                    uoTextBoxAmount.Text = dr["colAmountMoney"].ToString().Substring(0, (dr["colAmountMoney"].ToString().Length - 2));
                    uoTextBoxReimbursementName.Text = dr["colReimbursementNameVarchar"].ToString();
                    uoTextBoxRemarks.Text = dr["colRemarksVarchar"].ToString();
                }
                
            }
            
           
        }
        #endregion

    }
}

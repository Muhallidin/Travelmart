using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class PortContractAdd : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                OpenParentPage();
            }
            if (!IsPostBack)
            {
                CurrencyLoad();
                PortCompanyLoad();
                if (Request.QueryString["cmId"] != null)
                    GetContractDetails();
            }
        }

        protected void uobtnSave_Click(object sender, EventArgs e)
        {
            Int32 iContractID = (Request.QueryString["cmId"] == null) ? 0 : Convert.ToInt32(Request.QueryString["cmId"].ToString());
            AddSavePortContract(iContractID,
                                Convert.ToInt32(uoDropDownListPort.SelectedValue),
                                uotextboxContractTitle.Text,
                                uotextboxRemarks.Text,
                                uotextboxStartDate.Text,
                                uotextboxEndDate.Text,
                                uotextboxRateperHead.Text,
                                uoTextBoxTax.Text,
                                uoDropDownListCurrency.SelectedValue,
                                uoCheckBoxTaxInclusive.Checked,
                                uotextboxRCCLRep.Text,
                                uotextboxVendorRep.Text);

            OpenParentPage();
        }

        #endregion

        #region Function

        private void CurrencyLoad()
        {
            /// <summary>
            /// Date Created: 25/08/2011
            /// Created By: Marco Abejar
            /// (description) Select all currency  
            /// </summary>

            uoDropDownListCurrency.DataSource = ContractBLL.CurrencyLoad();
            uoDropDownListCurrency.DataTextField = "colCurrencyCodeVarchar";
            uoDropDownListCurrency.DataValueField = "colCurrencyIDInt";
            uoDropDownListCurrency.DataBind();
        }

        private void AddSavePortContract(Int32 PortContractID, Int32 PortCompanyID, string vContract, string Remarks,
            string dtStart, string dtEnd, string RateperHead, string TaxRate, string Currency, bool TaxInclusive, string RCCLRep, string vRep)
        {
            /// <summary>
            /// Date Created: 24/08/2011
            /// Created By: Marco Abejar
            /// (description) Add / save port contract          
            /// </summary>
            /// 

            //BLL.ContractBLL.AddSavePortContract(PortContractID, PortCompanyID,
            //    vContract, Remarks, dtStart, dtEnd, RateperHead,
            //    TaxRate, Currency, TaxInclusive, RCCLRep, vRep);
        }
        private void GetContractDetails()
        {
            /// <summary>
            /// Date Created: 24/08/2011
            /// Created By: Marco Abejar
            /// (description) Get port contract info
            /// </summary>
            /// 

            Int32 iContractID = (Request.QueryString["cmId"] == null) ? 0 : Convert.ToInt32(Request.QueryString["cmId"].ToString());
            DataTable dtContractInfo = null;//BLL.ContractBLL.GetPortContractDetails(iContractID);

            try
            {
                if (dtContractInfo.Rows.Count > 0)
                {
                    uoDropDownListPort.SelectedValue = dtContractInfo.Rows[0]["colPortAgentCompanyIdInt"].ToString();
                    uotextboxContractTitle.Text = dtContractInfo.Rows[0]["colContractNameVarchar"].ToString();
                    uotextboxRemarks.Text = dtContractInfo.Rows[0]["colRemarksVarchar"].ToString();
                    uotextboxStartDate.Text = dtContractInfo.Rows[0]["colContractDateStartedDate"].ToString();
                    uotextboxEndDate.Text = dtContractInfo.Rows[0]["colContractDateEndDate"].ToString();
                    uotextboxRateperHead.Text = string.Format("{0:0.00}", Double.Parse(dtContractInfo.Rows[0]["colContractRatePerHeadMoney"].ToString())); ;
                    uoTextBoxTax.Text = dtContractInfo.Rows[0]["colContractTaxDecimal"].ToString();
                    uoDropDownListCurrency.SelectedValue = dtContractInfo.Rows[0]["colCurrencyIDInt"].ToString();
                    uoCheckBoxTaxInclusive.Checked = Convert.ToBoolean(dtContractInfo.Rows[0]["colTaxInclusiveBit"].ToString());
                    uotextboxRCCLRep.Text = dtContractInfo.Rows[0]["colRCCLPersonnel"].ToString();
                    uotextboxVendorRep.Text = dtContractInfo.Rows[0]["colVendorPersonnel"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtContractInfo != null)
                {
                    dtContractInfo.Dispose();
                }
            }
        }
        private void PortCompanyLoad()
        {
            /// <summary>
            /// Date Created: 24/08/2011
            /// Created By: Marco Abejar
            /// (description) Select all port company
            /// </summary>

            uoDropDownListPort.DataSource = PortBLL.GetPortCompanyList();
            uoDropDownListPort.DataTextField = "colCompanyNameVarchar";
            uoDropDownListPort.DataValueField = "colPortAgentCompanyIdInt";
            uoDropDownListPort.DataBind();
        }

        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Close this page and update parent page            
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupPort\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uobtnSave, this.GetType(), "scr", sScript, false);
        }
        #endregion
    }
}

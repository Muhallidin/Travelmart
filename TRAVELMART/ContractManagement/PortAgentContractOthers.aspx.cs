using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.IO;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentContractOthers : System.Web.UI.Page
    {
        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCurrency();
                if (Request.QueryString["ServiceId"] != "0")
                {
                    LoadDetails();
                }
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            ContractBLL.SaveContractPortAgentOther(Request.QueryString["ServiceId"], Request.QueryString["pId"], Request.QueryString["vType"],
                uoTextBoxRate.Text, GlobalCode.Field2String(Session["UserName"]), Request.QueryString["cId"], uoTextBoxRemarks.Text, uoHiddenFieldDetailId.Value,
                uoDropDownListCurrency.SelectedValue);

            if (Request.QueryString["ServiceId"] == "0" || Request.QueryString["ServiceId"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract other added.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract other updated.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }            

            OpenParentPage();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Date Created: 20/10/2011
        /// Created By: Charlene Remotigue
        /// Description: loads currencies
        /// </summary>
        private void LoadCurrency()
        {
            DataTable CurrencyDataTable = null;
            try
            {
                //CurrencyDataTable = ContractBLL.GetCurrencyByCountry(uoHiddenFieldCountryId.Value);
                CurrencyDataTable = ContractBLL.CurrencyLoad();
                ListItem item = new ListItem("--Select Currency--", "0");
                uoDropDownListCurrency.Items.Clear();
                uoDropDownListCurrency.Items.Add(item);
                uoDropDownListCurrency.DataSource = CurrencyDataTable;
                uoDropDownListCurrency.DataTextField = "colCurrencyNameVarchar";
                uoDropDownListCurrency.DataValueField = "colCurrencyIDInt";
                uoDropDownListCurrency.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CurrencyDataTable != null)
                { CurrencyDataTable.Dispose(); }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: close this page and open parent page
        /// </summary>
        private void OpenParentPage()
        {

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldServicePopup\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += " window.parent.history.go(0); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        protected void LoadDetails()
        {
            IDataReader dr = null;
            try
            {
                dr=ContractBLL.LoadPortAgentContractOthers(Request.QueryString["ServiceId"]);
                if (dr.Read())
                {
                    uoTextBoxRemarks.Text = dr["colRemarksVarchar"].ToString();
                    uoTextBoxRate.Text = dr["colServiceRateMoney"].ToString().Remove(dr["colServiceRateMoney"].ToString().Length - 2);
                    uoDropDownListCurrency.SelectedValue = dr["colCurrencyInt"].ToString();
                    uoHiddenFieldDetailId.Value = dr["colContractPortAgentServiceDetailIdInt"].ToString();
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
                    dr.Dispose();
                }
            }
            
        }
        #endregion

        
    }
}

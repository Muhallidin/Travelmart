using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART.ContractManagement
{
    public partial class PortContractMedical : System.Web.UI.Page
    {
        public string uoLabelTitle = "";
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/11/2011        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCurrency();
                if (Request.QueryString["ServiceId"] == "0")
                {
                    uoLabelTitle = "Add Medical Accomodations";
                }
                else
                {
                    uoLabelTitle = "Edit Medical Accomodations";
                    LoadMedicalAccomodations();
                    
                    LoadMedicalTransfers();
                }
            }

        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            ContractBLL.SaveContractPortAgentMedicalDetails(Request.QueryString["ServiceId"], Request.QueryString["pId"],
                Request.QueryString["vType"], uoTextBoxRate.Text, GlobalCode.Field2String(Session["UserName"]), Request.QueryString["cId"], 
                uoTextBoxDays.Text, uoHiddenFieldDetailId.Value);
            OpenParentPage();
        }

        protected void uoButtonSaveTransfer_Click(object sender, EventArgs e)
        {
            string Origin = uoDropDownListOrigin.SelectedValue;
            string Destination = uoDropDownListDestination.SelectedValue;
            if (uoDropDownListOrigin.SelectedValue == "Others")
            {
                Origin = uoTextBoxOtherOrigin.Text;
            }
            if (uoDropDownListOrigin.SelectedValue == "Others")
            {
                Destination = uoTextBoxOtherDestination.Text;
            }
            //ContractBLL.SaveContractPortAgentMedicalSpecifications(Request.QueryString["ServiceId"], uoDropDownListCurrency.SelectedValue,
            //    uoTextBoxTrnsferRate.Text, Origin, Destination, uoTextBoxRemarks.Text, GlobalCode.Field2String(Session["UserName"]));

            LoadMedicalTransfers();
        }

        protected void uoDropDownListOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListOrigin.SelectedValue == "Others")
            {
                rfvVehicleOrigin.ControlToValidate = "uoTextBoxOtherOrigin";
                uoTextBoxOtherOrigin.Visible = true;
            }
            else
            {
                rfvVehicleOrigin.ControlToValidate = "uoDropDownListOrigin";
                uoTextBoxOtherOrigin.Visible = false;
            }
        }

        protected void uoDropDownListDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListDestination.SelectedValue == "Others")
            {
                rfvVehicleDestination.ControlToValidate = "uoTextBoxOtherDestination";
                uoTextBoxOtherDestination.Visible = true;
            }
            else
            {
                rfvVehicleDestination.ControlToValidate = "uoDropDownListDestination";
                uoTextBoxOtherDestination.Visible = false;
            }
        }

        protected void uoTransferList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                //ContractBLL.DeletePortContractSpecifications(e.CommandArgument.ToString(), GlobalCode.Field2String(Session["UserName"]));

            }
        }

        protected void uoTransferList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created:04/11/2011
        /// Description: Load Medical Accomodation details
        /// </summary>
        protected void LoadMedicalAccomodations()
        {
            IDataReader dr = null;
            try
            {
                dr = null;//ContractBLL.LoadPortAgentMedicalServices(Request.QueryString["ServiceId"]);
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        uoTextBoxDays.Text = dr["colAccomodationDays"].ToString();
                        uoTextBoxRate.Text = dr["colServiceRateMoney"].ToString().Remove(dr["colServiceRateMoney"].ToString().Length - 2);
                        uoHiddenFieldDetailId.Value = dr["colContractPortAgentServiceDetailIdInt"].ToString();
                    }
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

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Dexcription: close this page and open parent page
        /// </summary>
        private void OpenParentPage()
        {

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldService\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += " window.parent.history.go(0); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider medical specifications
        /// </summary>
        protected void LoadMedicalTransfers()
        {
            DataTable transferDataTable = null;//ContractBLL.LoadPortAgentMedicalSpecifications(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
            uoTransferList.Items.Clear();
            uoTransferList.DataSource = transferDataTable;
            uoTransferList.DataBind();
        }

        /// <summary>
        /// Date Created: 10/11/2011
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
        #endregion

    }
}

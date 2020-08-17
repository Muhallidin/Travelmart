using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;

using System.IO;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentContractVehicleAdd : System.Web.UI.Page
    {
        #region DECLARATIONS
        public string AddEditLabel = "";
        #endregion

        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ServiceId"] == "0")
            {
                AddEditLabel = "Add Service";
            }
            else
            {
                AddEditLabel = "Edit Service";
            }

            if (!IsPostBack)
            {
                LoadVehicleBrand();

                if (Request.QueryString["ServiceId"] != "0")
                {
                    LoadValues();
                }
            }
        }

        protected void uoDropDownListVehicleBrandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVehicleBranch();
        }

        protected void uoDropDownListVehicleBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVehicles();
        }

        protected void uoDropDownListOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListOrigin.SelectedValue == "Others")
            {
                uoTextBoxOtherOrigin.Visible = true;
            }
            else
            {
                uoTextBoxOtherOrigin.Visible = false;
            }
        }

        protected void uoDropDownListDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListDestination.SelectedValue == "Others")
            {
                uoTextBoxOtherDestination.Visible = true;
            }
            else
            {
                uoTextBoxOtherDestination.Visible = false;
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            ContractBLL.SaveContractPortAgentVendorVehicle(Request.QueryString["ServiceId"], uoDropDownListVehicleBrandName.SelectedValue,
                    uoDropDownListVehicleBranch.SelectedValue, Request.QueryString["pId"], Request.QueryString["vType"],
                    GlobalCode.Field2String(Session["UserName"]), Request.QueryString["cId"]);

            if (Request.QueryString["ServiceId"] == "0" || Request.QueryString["ServiceId"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract vehicle added.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract vehicle updated.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            OpenParentPage();
        }

        protected void uoButtonSaveVehicle_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            string Origin = uoDropDownListOrigin.SelectedValue;
            string Destination = uoDropDownListDestination.SelectedValue;
            if (uoDropDownListOrigin.SelectedValue == "Others")
            {
                Origin = uoTextBoxOtherOrigin.Text;
            }
            if (uoDropDownListDestination.SelectedValue == "Others")
            {
                Destination = uoTextBoxOtherDestination.Text;
            }
            ContractBLL.SaveContractPortAgentVehicleSpecifications(Request.QueryString["ServiceId"], uoTextBoxDateFrom.Text,
                uoTextBoxDateTo.Text, uoDropDownListVehicleCurrency.SelectedValue, uoTextBoxRate.Text,
                Origin, Destination, uoDropDownListVehicleType.SelectedValue,
                uoTextBoxCapacity.Text, GlobalCode.Field2String(Session["UserName"]));

            if (Request.QueryString["ServiceId"] == "0" || Request.QueryString["ServiceId"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract specification added.";
                strFunction = "uoButtonSaveVehicle_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            LoadVehicles();
            ClearFields();
        }

        protected void uoDropDownListVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable vTypeDataTable = (DataTable)ViewState["vTypeDataTable"];
            foreach (DataRow dr in vTypeDataTable.Rows)
            {
                if (dr["colVehicleIdBigint"].ToString() == uoDropDownListVehicleType.SelectedValue)
                {
                    uoTextBoxCapacity.Text = dr["colVehicleCapacityInt"].ToString();
                    return;
                }
            }

        }

        protected void uoVehicleList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                //ContractBLL.DeletePortContractSpecifications(e.CommandArgument.ToString(), GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract specification deleted. (flagged as inactive)";
                strFunction = "uoVehicleList_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                LoadVehicles();
            }
        }

        protected void uoVehicleList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        #endregion

        #region METHODS
        /// <summary>
        /// author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// Description: load vehicle brands with no contract
        /// </summary>
        private void LoadVehicleBrand()
        {
            DataTable VendorBrandDatatable = null;
            ListItem item;
            try
            {
                VendorBrandDatatable = PortBLL.getVendorBrandByPort(Request.QueryString["pId"], Request.QueryString["vType"], Request.QueryString["cId"],
                    Request.QueryString["ServiceId"]);


                item = new ListItem("--Select Vehicle Brand--", "0");
                uoDropDownListVehicleBrandName.Items.Clear();
                uoDropDownListVehicleBrandName.Items.Add(item);
                uoDropDownListVehicleBrandName.DataSource = VendorBrandDatatable;
                uoDropDownListVehicleBrandName.DataTextField = "colVendorNameVarchar";
                uoDropDownListVehicleBrandName.DataValueField = "colVendorIdInt";
                uoDropDownListVehicleBrandName.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VendorBrandDatatable != null)
                {
                    VendorBrandDatatable.Dispose();
                }
            }

        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: load vehicle branch
        /// </summary>
        protected void LoadVehicleBranch()
        {
            DataTable vendorBranchDatatable = null;
            ListItem item;
            try
            {
                vendorBranchDatatable = PortBLL.getVendorBranchbyVendorBrand(uoDropDownListVehicleBrandName.SelectedValue, Request.QueryString["vType"],
                    Request.QueryString["ServiceId"], Request.QueryString["cId"]);

                item = new ListItem("--Select Vehicle Branch--", "0");
                uoDropDownListVehicleBranch.Items.Clear();
                uoDropDownListVehicleBranch.Items.Add(item);
                uoDropDownListVehicleBranch.DataSource = vendorBranchDatatable;
                uoDropDownListVehicleBranch.DataTextField = "colVendorBranchNameVarchar";
                uoDropDownListVehicleBranch.DataValueField = "colBranchIDInt";
                uoDropDownListVehicleBranch.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (vendorBranchDatatable != null)
                {
                    vendorBranchDatatable.Dispose();
                }
            }
        }

        protected void SetVehicles()
        {
            ClearFields();
            if (uoDropDownListVehicleCurrency.Items.Count == 0)
            {
                LoadCurrency();
            }
            if (uoDropDownListVehicleBranch.SelectedValue != "0")
            {
                LoadVehicleTypes();
                LoadVehicles();
                uoTRVehicle.Visible = true;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: load all currencies
        /// </summary>
        protected void LoadCurrency()
        {
            DataTable CurrencyDataTable = ContractBLL.CurrencyLoad();            
            ListItem item = new ListItem("--Select Currency--", "0");
            uoDropDownListVehicleCurrency.Items.Clear();
            uoDropDownListVehicleCurrency.Items.Add(item);
            uoDropDownListVehicleCurrency.DataSource = CurrencyDataTable;
            uoDropDownListVehicleCurrency.DataTextField = "colCurrencyNameVarchar";
            uoDropDownListVehicleCurrency.DataValueField = "colCurrencyIDInt";
            uoDropDownListVehicleCurrency.DataBind();
        }

        protected void LoadVehicleTypes()
        {
            DataTable vTypeDataTable = VehicleBLL.vehicleGetTypeBrandMake(Int32.Parse(uoDropDownListVehicleBranch.SelectedValue));
            ListItem item = new ListItem("--Select Vehicle Type--", "0");
            uoDropDownListVehicleType.Items.Clear();
            uoDropDownListVehicleType.Items.Add(item);
            uoDropDownListVehicleType.DataSource = vTypeDataTable;
            uoDropDownListVehicleType.DataTextField = "vehicleType";
            uoDropDownListVehicleType.DataValueField = "colVehicleIdBigint";
            uoDropDownListVehicleType.DataBind();
            ViewState["vTypeDataTable"] = vTypeDataTable;
        }

        protected void LoadVehicles()
        {
            DataTable vehicleDataTable = null;// ContractBLL.LoadPortAgentContractVehicles(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
            uoVehicleList.Items.Clear();
            uoVehicleList.DataSource = vehicleDataTable;
            uoVehicleList.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: Clear fields after adding vehicles
        /// </summary>
        protected void ClearFields()
        {
            uoTextBoxDateFrom.Text = "";
            uoTextBoxDateTo.Text = "";
            uoTextBoxRate.Text = "";
            uoTextBoxOtherOrigin.Text = "";
            uoTextBoxOtherOrigin.Visible = false;
            uoTextBoxOtherDestination.Text = "";
            uoTextBoxOtherDestination.Visible = false;
            uoTextBoxCapacity.Text = "";
            uoDropDownListVehicleCurrency.SelectedValue = "0";
            uoDropDownListVehicleType.SelectedValue = "0";
            uoDropDownListOrigin.SelectedValue = "0";
            uoDropDownListDestination.SelectedValue = "0";
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

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load values on edit
        /// ---------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        protected void LoadValues()
        {
            IDataReader dr = null;
            try
            {
                dr = null;// ContractBLL.LoadPortContractVehicleDetails(Request.QueryString["ServiceId"]);
                if (dr.Read())
                {
                    uoDropDownListVehicleBrandName.SelectedValue = dr["colVendorBrandIdInt"].ToString();
                    LoadVehicleBranch();
                    uoDropDownListVehicleBranch.SelectedValue = dr["colVendorBranchIdInt"].ToString();
                    
                }
                SetVehicles();
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

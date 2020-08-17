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
    public partial class PortAgentContractHotel : System.Web.UI.Page
    {
        #region DECLARATIONS
        public string AddEditLabel = "";
        #endregion

        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// ------------------------------
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
                LoadHotelBrand();
               
                if (Request.QueryString["ServiceId"] != "0")
                {                
                    LoadValues();                   
                }
            }
        }

        protected void uoDropDownListHotelBrandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHotelBranch();
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            //ContractBLL.SaveContractPortAgentVendorHotel(Request.QueryString["ServiceId"], uoDropDownListHotelBrandName.SelectedValue,
            //        uoDropDownListHotelBranch.SelectedValue, Request.QueryString["pId"], 1, uoTextBoxMealRate.Text, uoTextBoxMealTax.Text,
            //        uoCheckBoxTaxInclusive.Checked, uoCheckBoxBreakFast.Checked, uoCheckBoxLunch.Checked, uoCHeckBoxDinner.Checked,
            //        uoCheckBoxLunchOrDinner.Checked, uoCheckBoxWithShuttle.Checked, GlobalCode.Field2String(Session["UserName"]), Request.QueryString["cId"], 
            //        uoHiddenFieldservicedetailId.Value);

            if (Request.QueryString["ServiceId"] == "0" || Request.QueryString["ServiceId"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract hotel added.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider contract hotel updated.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            OpenParentPage();
        }

        protected void uoButtonSaveRoom_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            //ContractBLL.SaveContractPortAgentHotelSpecifications(Request.QueryString["ServiceId"], uoTextBoxDateFrom.Text,
            //    uoTextBoxDateTo.Text, uoDropDownListCurrency.SelectedValue, uoTextBoxRoomRate.Text, uoTextBoxRoomTax.Text,
            //    uoCheckBoxRoomTaxInclusive.Checked, uoDropDownListRoomType.SelectedValue, uoTextBoxMonday.Text,
            //    uoTextBoxTuesday.Text, uoTextBoxWednesday.Text, uoTextBoxThursday.Text, uoTextBoxFriday.Text,
            //    uoTextBoxSaturday.Text, uoTextBoxSunday.Text, GlobalCode.Field2String(Session["UserName"]));

            //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
            strLogDescription = "Port contract specification added.";
            strFunction = "uoButtonSaveRoom_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

            LoadRooms();
        }

        protected void uoDropDownListHotelBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRooms();
        }

        protected void uoRoomsList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                //ContractBLL.DeletePortContractSpecifications(e.CommandArgument.ToString(), GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Port contract specification deleted. (flagged as inactive)";
                strFunction = "uoRoomsList_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                LoadRooms();
                ClearFields();
            }
        }

        protected void uoRoomsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion

        #region METHODS
        /// <summary>
        /// author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// Description: load hotel brands with no contract
        /// </summary>
        private void LoadHotelBrand()
        { 
            DataTable VendorBrandDatatable = null;
            ListItem item;
            try
            {
                VendorBrandDatatable = PortBLL.getVendorBrandByPort(Request.QueryString["pId"], Request.QueryString["vType"], Request.QueryString["cId"],
                    Request.QueryString["ServiceId"]);


                item = new ListItem("--Select Hotel Brand--", "0");
                uoDropDownListHotelBrandName.Items.Clear();
                uoDropDownListHotelBrandName.Items.Add(item);
                uoDropDownListHotelBrandName.DataSource = VendorBrandDatatable;
                uoDropDownListHotelBrandName.DataTextField = "colVendorNameVarchar";
                uoDropDownListHotelBrandName.DataValueField = "colVendorIdInt";
                uoDropDownListHotelBrandName.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                if(VendorBrandDatatable != null)
                {
                    VendorBrandDatatable.Dispose();
                }
            }
                
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// Description: loads values on edit
        /// ----------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        protected void LoadValues()
        {
            IDataReader dr = null;
            try
            {
                dr = null; //ContractBLL.LoadPortContractHotelDetails(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        uoDropDownListHotelBrandName.SelectedValue = dr["colVendorBrandIdInt"].ToString();
                        uoDropDownListHotelBrandName.Enabled = false;
                        LoadHotelBranch();
                        uoDropDownListHotelBranch.SelectedValue = dr["colVendorBranchIdInt"].ToString();
                        uoDropDownListHotelBranch.Enabled = false;
                        uoTextBoxMealRate.Text = dr["colServiceRateMoney"].ToString().Remove(dr["colServiceRateMoney"].ToString().Length - 2);
                        uoTextBoxMealTax.Text = (Int32.Parse(dr["colMealRateTaxDecimal"].ToString()) * 100).ToString();
                        uoCheckBoxTaxInclusive.Checked = bool.Parse(dr["colMealTaxInclusiveBit"].ToString());
                        uoCheckBoxBreakFast.Checked = bool.Parse(dr["colBreakfastBit"].ToString());
                        uoCheckBoxLunch.Checked = bool.Parse(dr["colLunchBit"].ToString());
                        uoCHeckBoxDinner.Checked = bool.Parse(dr["colDinnerBit"].ToString());
                        uoCheckBoxLunchOrDinner.Checked = bool.Parse(dr["colLunchOrDinnerBit"].ToString());
                        uoCheckBoxWithShuttle.Checked = bool.Parse(dr["colWithShuttleBit"].ToString());
                        uoHiddenFieldservicedetailId.Value = dr["colContractPortAgentServiceDetailIdInt"].ToString();
                        SetRooms();
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
        /// Date Created: 03/11/2011
        /// Description: load hotel branch
        /// </summary>
        protected void LoadHotelBranch()
        {
            DataTable vendorBranchDatatable = null;
            ListItem item;
            try
            {
                vendorBranchDatatable = PortBLL.getVendorBranchbyVendorBrand(uoDropDownListHotelBrandName.SelectedValue, Request.QueryString["vType"],
                    Request.QueryString["ServiceId"], Request.QueryString["cId"]);
                
                item = new ListItem("--Select Hotel Branch--", "0");
                uoDropDownListHotelBranch.Items.Clear();
                uoDropDownListHotelBranch.Items.Add(item);
                uoDropDownListHotelBranch.DataSource = vendorBranchDatatable;
                uoDropDownListHotelBranch.DataTextField = "colVendorBranchNameVarchar";
                uoDropDownListHotelBranch.DataValueField = "colBranchIDInt";
                uoDropDownListHotelBranch.DataBind();
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

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
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
        /// Date Created: 09/11/2011
        /// Description: load added rooms
        /// </summary>
        protected void LoadRooms()
        {
            DataTable roomsDataTable = null; // ContractBLL.LoadPortContractHotelRooms(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
            uoRoomsList.Items.Clear();
            uoRoomsList.DataSource = roomsDataTable;
            uoRoomsList.DataBind();
        }

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   09/11/2011
        /// Description:    load room types per branch
        /// -------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  24/02/2012
        /// Description:    Change DataTable to List
        /// </summary>
        protected void LoadRoomTypes()
        {
            List<HotelBranchRoomType> list = null;
            try
            {
                //DataTable roomTypeDataTable = HotelBLL.HotelRoomTypeGetDetailsByBranch(uoDropDownListHotelBranch.SelectedValue);
                list = HotelBLL.HotelRoomTypeGetDetailsByBranch(uoDropDownListHotelBranch.SelectedValue);
                ListItem item = new ListItem("Select Room Type--", "0");
                uoDropDownListRoomType.Items.Clear();
                uoDropDownListRoomType.Items.Add(item);
                uoDropDownListRoomType.DataSource = list;
                uoDropDownListRoomType.DataTextField = "colRoomNameVarchar";
                uoDropDownListRoomType.DataValueField = "colRoomTypeID";
                uoDropDownListRoomType.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
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
            uoDropDownListCurrency.Items.Clear();
            uoDropDownListCurrency.Items.Add(item);
            uoDropDownListCurrency.DataSource = CurrencyDataTable;
            uoDropDownListCurrency.DataTextField = "colCurrencyNameVarchar";
            uoDropDownListCurrency.DataValueField = "colCurrencyIDInt";
            uoDropDownListCurrency.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: set all room details
        /// </summary>
        protected void SetRooms()
        {
            ClearFields();
            if (uoDropDownListCurrency.Items.Count == 0)
            {
                LoadCurrency();
            }
            if (uoDropDownListHotelBranch.SelectedValue != "0")
            {                
                LoadRoomTypes();
                LoadRooms();
                uoTRRooms.Visible = true;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: Clear fields after adding rooms
        /// </summary>
        protected void ClearFields()
        {
            uoTextBoxDateFrom.Text = "";
            uoTextBoxDateTo.Text = "";
            uoTextBoxFriday.Text = "";
            uoTextBoxMonday.Text = "";
            uoTextBoxRoomRate.Text = "";
            uoTextBoxRoomTax.Text = "";
            uoTextBoxSaturday.Text = "";
            uoTextBoxSunday.Text = "";
            uoTextBoxThursday.Text = "";
            uoTextBoxTuesday.Text = "";
            uoTextBoxWednesday.Text = "";
            uoDropDownListCurrency.SelectedValue = "0";
            uoDropDownListRoomType.SelectedValue = "0";
            uoCheckBoxRoomTaxInclusive.Checked = false;
        }
        #endregion

       

    }
}

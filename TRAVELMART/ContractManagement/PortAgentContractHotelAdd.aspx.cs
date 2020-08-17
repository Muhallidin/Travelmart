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
    public partial class PortAgentContractHotelAdd : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/12/2011
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
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadHotelBrand();
                if (Request.QueryString["contractBranchId"] != "0")
                {
                    LoadDetails();
                    LoadHotelRooms();
                }
            }
        }

        protected void uoDropDownListHotelBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListHotelBrand.SelectedValue != "0")
            {
                LoadHotelBranch();
                CurrencyLoad();
            }
        }
        protected void uoDropDownListHotelBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelGetRoomType();
        }

        protected void uoBtnSaveRoom_Click(object sender, EventArgs e)
        {
            DataTable RoomDataTable = null;
            if (ViewState["Rooms"] == null)
            {
                _Create_DataTable_GridViewDataSource("0", uoDropDownListRoomType.SelectedItem.Text, uoDropDownListRoomType.SelectedValue,
                    uoTextBoxDateFrom.Text, uoTextBoxDateTo.Text, uoTextBoxCurrency.Text, uoHiddenFieldCurrencyId.Value, uoTextBoxRoomRate.Text,
                    uoTextBoxRoomTax.Text, uoCheckBoxRoomTaxInclusive.Checked, uoTextBoxMon.Text, uoTextBoxTues.Text, uoTextBoxWed.Text, uoTextBoxThur.Text,
                    uoTextBoxFri.Text, uoTextBoxSat.Text, uoTextBoxSun.Text);
            }
            else
            {
                RoomDataTable = (DataTable)ViewState["Rooms"];
                DataRow row = RoomDataTable.NewRow();
                
                RoomDataTable.Rows.Add(_Create_DataRow_GridViewDataSource(row, "0", uoDropDownListRoomType.SelectedItem.Text, uoDropDownListRoomType.SelectedValue,
                    uoTextBoxDateFrom.Text, uoTextBoxDateTo.Text, uoTextBoxCurrency.Text, uoHiddenFieldCurrencyId.Value, uoTextBoxRoomRate.Text,
                    uoTextBoxRoomTax.Text, uoCheckBoxRoomTaxInclusive.Checked, uoTextBoxMon.Text, uoTextBoxTues.Text, uoTextBoxWed.Text, uoTextBoxThur.Text,
                    uoTextBoxFri.Text, uoTextBoxSat.Text, uoTextBoxSun.Text));
                uoRoomList.DataSource = RoomDataTable;
                ViewState["Rooms"] = RoomDataTable;
            }

            uoRoomList.DataBind();
            //ViewState["Rooms"] = RoomDataTable;

            uoRoomList.Focus();
        }

        protected void uoRoomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = (DataTable)ViewState["Rooms"];
            Int32 contractDetailId = 0;
            contractDetailId = Int32.Parse(dt.Rows[this.uoRoomList.SelectedIndex]["colContractDetailIdInt"].ToString());
            if (contractDetailId != 0)
            {
                if (ViewState["deleteRooms"] == null)
                {
                    _Create_DataTable_DeletedSource(contractDetailId.ToString());
                }
                else
                {
                    dt2 = (DataTable)ViewState["deleteRooms"];
                    DataRow row = dt2.NewRow();
                    dt2.Rows.Add(_Create_DataRow_DeletedSource(row, contractDetailId.ToString()));
                    ViewState["deleteRooms"] = dt2;
                }
            }
            
            dt.Rows.RemoveAt(this.uoRoomList.SelectedIndex);
            uoRoomList.DataSource = dt;
            uoRoomList.DataBind();
            ViewState["Rooms"] = dt;

            
        }

        protected void uoRoomList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }
        protected void uoRoomList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoBtnSave_Click(object sender, EventArgs e)
        {
            ////save hotel branch details
            //Int32 pID = ContractBLL.SaveContractPortAgentVendorHotel(Request.QueryString["contractBranchId"],
            //    uoDropDownListHotelBrand.SelectedValue, uoDropDownListHotelBranch.SelectedValue,
            //    Request.QueryString["pId"], uoTextBoxMealRate.Text,
            //    uoTextBoxMealTaxRate.Text, uoCheckBoxTaxInclusive.Checked,
            //    uoCheckBoxBfast.Checked, uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked,
            //    uoCheckBoxLunchOrDinner.Checked, uoCheckBoxShuttle.Checked,
            //    GlobalCode.Field2String(Session["UserName"]), Request.QueryString["cId"]);

            ////save hotel room details
            //if (ViewState["Rooms"] != null)
            //{
            //    SaveRoomList(pID);
            //}
            ////delete hotel room
            //if (ViewState["deleteRooms"] != null)
            //{
            //    DeleteRoom();
            //}
            Session["hasChanges"] = "true";
        }
        #endregion

        #region METHODS
        void _Create_DataTable_GridViewDataSource(string contractDetailId, string roomType, string roomTypeId, string DateFrom,
            string DateTo, string Currency, string CurrencyId, string roomRate, string roomTax, bool taxInclusive, string Mon,
            string Tue, string Wed, string Thu, string Fri, string Sat, string Sun)
        {
            DataTable RoomDataTable = null;
            RoomDataTable = new DataTable();
            RoomDataTable.Columns.Add("colContractDetailIdInt");
            RoomDataTable.Columns.Add("RoomType");
            RoomDataTable.Columns.Add("RoomTypeId");
            RoomDataTable.Columns.Add("DateFrom");
            RoomDataTable.Columns.Add("DateTo");
            RoomDataTable.Columns.Add("Currency");
            RoomDataTable.Columns.Add("CurrencyId");
            RoomDataTable.Columns.Add("RoomRate");
            RoomDataTable.Columns.Add("RoomTax");
            RoomDataTable.Columns.Add("RoomTaxInclusive");
            RoomDataTable.Columns.Add("Mon");
            RoomDataTable.Columns.Add("Tue");
            RoomDataTable.Columns.Add("Wed");
            RoomDataTable.Columns.Add("Thu");
            RoomDataTable.Columns.Add("Fri");
            RoomDataTable.Columns.Add("Sat");
            RoomDataTable.Columns.Add("Sun");
            

            DataRow dr;
            dr = RoomDataTable.NewRow();
            RoomDataTable.Rows.Add(this._Create_DataRow_GridViewDataSource(dr, contractDetailId, roomType, roomTypeId, DateFrom,
            DateTo, Currency, CurrencyId, roomRate, roomTax, taxInclusive, Mon, Tue, Wed, Thu, Fri, Sat, Sun));

            ViewState["Rooms"] = RoomDataTable;
            this.uoRoomList.DataSource = RoomDataTable;
        }

        DataRow _Create_DataRow_GridViewDataSource(DataRow row, string contractDetailId, string roomType, string roomTypeId, string DateFrom,
            string DateTo, string Currency, string CurrencyId, string roomRate, string roomTax, bool taxInclusive, string Mon,
            string Tue, string Wed, string Thu, string Fri, string Sat, string Sun)
        {
            row["colContractDetailIdInt"] = contractDetailId;
            row["RoomType"] = roomType;
            row["RoomTypeId"] = roomTypeId;
            row["DateFrom"] = DateFrom;
            row["DateTo"] = DateTo;
            row["Currency"] = Currency;
            row["CurrencyId"] = CurrencyId;
            row["RoomRate"] = roomRate;
            row["RoomTax"] = roomTax;
            row["RoomTaxInclusive"] = taxInclusive;
            row["Mon"] = Mon;
            row["Tue"] = Tue;
            row["Wed"] = Wed;
            row["Thu"] = Thu;
            row["Fri"] = Fri;
            row["Sat"] = Sat;
            row["Sun"] = Sun;

            return row;
        }

        void _Create_DataTable_DeletedSource(string contractDetailId)
        {
            DataTable DeletedTable = null;
            DeletedTable = new DataTable();
            DeletedTable.Columns.Add("colContractDetailIdInt");

            DataRow dr;
            dr = DeletedTable.NewRow();
            DeletedTable.Rows.Add(this._Create_DataRow_DeletedSource(dr, contractDetailId));

            ViewState["deleteRooms"] = DeletedTable;
            this.uoRoomList.DataSource = DeletedTable;
        }

        DataRow _Create_DataRow_DeletedSource(DataRow row, string contractDetailId)
        {
            row["colContractDetailIdInt"] = contractDetailId;
            return row;
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/12/2011
        /// Description: Load hotel vendor with no contracts
        /// </summary>
        private void LoadHotelBrand()
        {
            DataTable VendorBrandDatatable = null;
            ListItem item;
            try
            {
                VendorBrandDatatable = PortBLL.getVendorBrandByPort(Request.QueryString["pId"], Request.QueryString["vTypeCode"], Request.QueryString["cId"],
                    Request.QueryString["contractBranchId"]);

                item = new ListItem("--Select Hotel Brand--", "0");
                uoDropDownListHotelBrand.Items.Clear();
                uoDropDownListHotelBrand.Items.Add(item);
                uoDropDownListHotelBrand.DataSource = VendorBrandDatatable;
                uoDropDownListHotelBrand.DataTextField = "colVendorNameVarchar";
                uoDropDownListHotelBrand.DataValueField = "colVendorIdInt";
                uoDropDownListHotelBrand.DataBind();
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
        /// Date Created: 07/12/2011
        /// Description: Load hotel branch with no contracts
        /// </summary>
        protected void LoadHotelBranch()
        {
            DataTable vendorBranchDatatable = null;
            ListItem item;
            try
            {
                vendorBranchDatatable = PortBLL.getVendorBranchbyVendorBrand(uoDropDownListHotelBrand.SelectedValue, Request.QueryString["vTypeCode"],
                    Request.QueryString["contractBranchId"], Request.QueryString["cId"]);

                item = new ListItem("--Select Hotel Branch--", "0");
                uoDropDownListHotelBranch.Items.Clear();
                uoDropDownListHotelBranch.Items.Add(item);
                uoDropDownListHotelBranch.DataSource = vendorBranchDatatable;
                uoDropDownListHotelBranch.DataTextField = "colVendorBranchNameVarchar";
                uoDropDownListHotelBranch.DataValueField = "colBranchIDInt";
                uoDropDownListHotelBranch.DataBind();
                uoHiddenFieldCountryId.Value = vendorBranchDatatable.Rows[0]["colCountryIDInt"].ToString();
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
        /// Date Created: 07/12/2011
        /// Description: Load currency for the given branch
        /// </summary>
        private void CurrencyLoad()
        {
            IDataReader drCurrency = null;
            try
            {
                drCurrency = ContractBLL.GetCurrencyByCountry(uoHiddenFieldCountryId.Value);
                if (drCurrency.Read())
                {
                    uoTextBoxCurrency.Text = drCurrency["colCurrencyNameVarchar"].ToString();
                    uoHiddenFieldCurrencyId.Value = drCurrency["colCurrencyIDInt"].ToString();
                }
                else
                {
                    uoTextBoxCurrency.Text = "No available currency.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drCurrency != null)
                {
                    drCurrency.Close();
                    drCurrency.Dispose();
                }
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created:   07/12/2011
        /// Description:    Load room types for the given branch
        /// -------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  24/02/2012
        /// Description:    Change DataTable to List
        /// </summary>
        private void HotelGetRoomType()
        {
             List<HotelBranchRoomType> list = null;
            try
            {
                list = HotelBLL.HotelRoomTypeGetDetailsByBranch(uoDropDownListHotelBranch.SelectedValue);
                uoDropDownListRoomType.Items.Clear();
                if (list.Count > 0)
                {
                    uoDropDownListRoomType.DataSource = list;
                    uoDropDownListRoomType.DataTextField = "colRoomNameVarchar";
                    uoDropDownListRoomType.DataValueField = "colRoomTypeID";

                }
                uoDropDownListRoomType.Items.Insert(0, new ListItem("--Select Room Type--", "0"));
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
        /// Date Created: 08/12/2011
        /// Description: save hotel rooms
        /// </summary>
        /// <param name="ContractBranchId"></param>
        protected void SaveRoomList(int ContractBranchId)
        {
            DataTable roomList = (DataTable)ViewState["Rooms"];

            ContractBLL.SaveContractPortAgentHotelRooms(roomList, ContractBranchId,
                Int32.Parse(Request.QueryString["cId"]));
        }

        protected void DeleteRoom()
        {
            DataTable roomList = (DataTable)ViewState["deleteRooms"];

            //ContractBLL.DeleteContractPortAgentHotelRooms(roomList, GlobalCode.Field2String(Session["UserName"]), "Hotel Room deleted.");
        }

        protected void LoadDetails()
        { }

        protected void LoadHotelRooms()
        { }
        #endregion

       

       



    }
}

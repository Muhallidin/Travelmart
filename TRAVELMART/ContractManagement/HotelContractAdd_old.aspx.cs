using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace TRAVELMART.ContractManagement
{
    public partial class HotelContractAdd_old : System.Web.UI.Page
    {
        #region Declaration

        private string _FileName;
        private string _FileType;
        private Int32 _FileSize = 0;
        private Byte[] _FileData;
        private DateTime _Now = DateTime.Now;

        #endregion

        #region Event

        #region Page Load
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Ryan Bautista
        /// (description)   Load all details      
        /// </summary>
        /// 
        protected void Page_Load(object sender, EventArgs e)
        {        
            //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //SFStatus.Visible = false;
            if (!IsPostBack)
            {
                HotelContractLogAuditTrail();
                HotelLoad();
                //HotelBranchLoad();
                HotelBranchLoadByVendorID();
                //ChangeToUpperCase(uoDropDownListVendor);
                CountryListByVendorID();
                //ChangeToUpperCase(uoDropDownCountry);
                CityListByVendorID(); 
                //ChangeToUpperCase(uoDropDownListCity);
                HotelRoomTypeLoad();
                //ChangeToUpperCase(uoDropDownListRoomType);
                CurrencyLoad();
                //ChangeToUpperCase(uoDropDownListCurrency);
                ViewState["Add"] = "False";
                ////ViewState["AddBranch"] = "False";
                ContractLoadByBranch();
            }
        }
        #endregion

        #region Button Save

        /// <summary>
        /// Date Created: 23/08/2011
        /// Created By:   Ryan Bautista
        /// (description) Save/Update Hotel contract  
        /// </summary>
        protected void uobtnSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;
            object sqlTransaction;

            bool isValidTest = false;
            Validate("Header");
            isValidTest = IsValid;
            Validate("branch"); 
            isValidTest &= IsValid;
            //Validate("Room");
            //isValidTest &= IsValid;
            if (!isValidTest)
            {
                return;
            }
   
            if (uoTextBoxMealRate.Text == "")
            {
                uoTextBoxMealRate.Text = "0";
            }
            if (uoTextBoxMealRateTax.Text == "")
            {
                uoTextBoxMealRateTax.Text = "0";
            }
            byte[] imageBytes = null;
            string Getdate;
            if (uoFileUploadContract.HasFile)
            {
                imageBytes = new byte[uoFileUploadContract.PostedFile.InputStream.Length + 1];
                uoFileUploadContract.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                uoHiddenFieldFileName.Value = uoFileUploadContract.FileName;
                uoHiddenFieldFileType.Value = uoFileUploadContract.PostedFile.ContentType;
                Getdate = Convert.ToString(_Now);
            }
            else
            {
                Getdate = "";
            }

            //if (uoGridViewBranch.Rows.Count > 0)
            //{
                Int32 pID = 0;
                //for (int i = 0; i < uoGridViewBranch.Rows.Count; i++) //Save the values one by one
                //{
                //    DataTable dt = new DataTable(); ;
                //    dt = (DataTable)ViewState["TableBranch"];
                    
                    pID = AddSaveHotelContract(
                                //dt.Rows[i]["Hotel Branch"].ToString(),
                                uoDropDownListVendor.SelectedItem.Text,
                                uotextboxContractTitle.Text,
                                uotextboxRemarks.Text,
                                uotextboxStartDate.Text,
                                uotextboxEndDate.Text,
                                uotextboxRCCLRep.Text,
                                uotextboxVendorRep.Text,
                                uoTextBoxRCCLDateAccepted.Text,
                                uoTextBoxVendorDateAccepted.Text,

                                //dt.Rows[i]["Country"].ToString(),
                                uoDropDownCountry.SelectedItem.Text,
                                //dt.Rows[i]["City"].ToString(),
                                uoDropDownListCity.SelectedItem.Text,

                                MUser.GetUserName(),
                                uoTextBoxMealRate.Text,
                                uoTextBoxMealRateTax.Text,
                                uoCheckBoxMealRateTaxInclusive.Checked,
                                uoCheckBoxBreakfast.Checked,
                                uoCheckBoxLunch.Checked,
                                uoCheckBoxDinner.Checked,
                                uoCheckBoxLunchDinner.Checked,
                                uoCheckBoxShuttle.Checked,

                                //_FileName,
                                uoHiddenFieldFileName.Value,
                                uoHiddenFieldFileType.Value,
                                //System.Text.Encoding.ASCII.GetBytes(uoHiddenFieldFileData.Value),
                                imageBytes,
                                Getdate,
                                uoHiddenFieldContractID.Value
                                //out sqlTransaction
                                );      
                //}
             SaveGridItems(pID);
            //}

             //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
             strLogDescription = "Hotel contract added.";
             strFunction = "uobtnSave_Click";

             DateTime currentDate = CommonFunctions.GetCurrentDateTime();

             BLL.AuditTrailBLL.InsertLogAuditTrail(pID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                   CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, MUser.GetUserName());

             Response.Redirect("~/ContractManagement/HotelContractListView.aspx?vmId=" + Request.QueryString["vmId"] + "&cID=" + Request.QueryString["cID"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
        #endregion

        #region uoDropDownListVendor_SelectedIndexChanged
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) select country and city after selecting a vendor branch
        /// </summary>
        protected void uoDropDownListVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountryListByVendorID();
            CityListByVendorID();
            ChangeToUpperCase(uoDropDownCountry);
        }
        #endregion

        #region uoDropDownCountry_SelectedIndexChanged
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) select city after select vendor branch 
        /// </summary>
        protected void uoDropDownCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            CityListByVendorID();
            ChangeToUpperCase(uoDropDownListCity);
        }
        #endregion

        #region uobtnAddRoom_Click
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) Add hotel branch in the grid
        /// </summary>
        protected void uobtnAddRoom_Click(object sender, EventArgs e)
        {
            CreateDatatable();
            Clear();

            //DateTime tmpMin = Convert.ToDateTime(uotextboxStartDate.Text);
            //DateTime tmpMax = Convert.ToDateTime(uotextboxEndDate.Text);
            //MaskedEditValidator1.MinimumValue = tmpMin.ToString("MM/dd/yyyy");
            //MaskedEditValidator1.MaximumValue = tmpMax.ToString("MM/dd/yyyy");

            //Page.Validate();
            //if (Page.IsValid)
            //{
                //GenerateGridView();
                //AddDataToDataTable(uoDropDownListRoomType.SelectedItem.Text, uotextboxRoomDateFrom.Text, uotextboxRoomDateTo.Text,
                //                   uoDropDownListCurrency.SelectedItem.Text, uotextboxRoomRate.Text, uoCheckBoxTaxInclusive.Checked, uoTextBoxTax.Text,
                //                   uotextboxMondayRoom.Text, uotextboxTuesdayRoom.Text, uotextboxWednesdayRoom.Text, uotextboxThursdayRoom.Text,
                //                   uotextboxFridayRoom.Text, uotextboxSaturdayRoom.Text, uotextboxSundayRoom.Text, uoDropDownListRoomType.SelectedValue,
                //                   uoDropDownListCurrency.SelectedValue,
                //                    (DataTable)ViewState["dt"]);
            //}
        }
        #endregion

        #region uoGridViewRooms_RowDeleting
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) remove item row in the grid view room
        /// </summary>
        protected void uoGridViewRooms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DataTable dt;
            //dt = CreateDatatable(); 
            //dt.Rows.RemoveAt(e.RowIndex);
            //uoGridViewRooms.DataSource = dt;
            //uoGridViewRooms.DataBind(); 
        }
        #endregion

        #region Remove items in grid
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) remove item row in the grid view room
        /// </summary>
        protected void uoGridViewRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["Table"];
            dt.Rows.RemoveAt(this.uoGridViewRooms.SelectedIndex);
            uoGridViewRooms.DataSource = dt;
            uoGridViewRooms.DataBind();
        }
        #endregion

        #region uoCheckBoxLunchDinner_CheckedChanged
        protected void uoCheckBoxLunchDinner_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxLunchDinner.Checked)
            {
                uoCheckBoxLunchDinner.Checked = true;
                uoCheckBoxLunch.Checked = false;
                uoCheckBoxDinner.Checked = false;
            }
        }
        #endregion

        #region uoCheckBoxLunch_CheckedChanged
        protected void uoCheckBoxLunch_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxLunch.Checked)
            {
                uoCheckBoxLunch.Checked = true;
                uoCheckBoxLunchDinner.Checked = false;
            }
        }
        #endregion

        #region uoCheckBoxDinner_CheckedChanged
        protected void uoCheckBoxDinner_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxDinner.Checked)
            {
                uoCheckBoxDinner.Checked = true;
                uoCheckBoxLunchDinner.Checked = false;
            }
        }
        #endregion

        #region uoCheckBoxBreakfast_CheckedChanged
        protected void uoCheckBoxBreakfast_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region uoButtonBranch_Click
        protected void uoButtonBranch_Click(object sender, EventArgs e)
        {
            CreateDatatableHotelBranch();
            if (uoGridViewBranch.Rows.Count == 0)
            {
                uoDropDownListVendorMain.Enabled = true;
            }
            else
            {
                uoDropDownListVendorMain.Enabled = false;
            }
        }
        #endregion

        #region Remove items in grid view hotel branch
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) remove item row in the grid view branch
        /// </summary>
        protected void uoGridViewBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["TableBranch"];
            dt.Rows.RemoveAt(this.uoGridViewBranch.SelectedIndex);
            uoGridViewBranch.DataSource = dt;
            uoGridViewBranch.DataBind();
            if (dt.Rows.Count == 0)
            {
                uoDropDownListVendorMain.Enabled = true;
            }
        }
        #endregion

        #region uoDropDownListVendorMain_SelectedIndexChanged
        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) select vendor branch, country and city after selecting a vendor 
        /// </summary>
        protected void uoDropDownListVendorMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelBranchLoadByVendorID();
            CountryListByVendorID();
            CityListByVendorID();
            ChangeToUpperCase(uoDropDownCountry);
        }
        #endregion

        #region uoButtonUpload_Click
        protected void uoButtonUpload_Click(object sender, EventArgs e)
        {
            if (uoFileUploadContract.PostedFile == null || string.IsNullOrEmpty(uoFileUploadContract.PostedFile.FileName) || uoFileUploadContract.PostedFile.InputStream == null)
            {
                Label1.Text = "<br />Error - unable to upload file. Please try again.<br />";
            }
            else
            {
                Label1.Visible = false;
                byte[] imageBytes = new byte[uoFileUploadContract.PostedFile.InputStream.Length + 1];
                uoFileUploadContract.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                uoHiddenFieldFileName.Value = uoFileUploadContract.FileName;
                uoHiddenFieldFileType.Value = uoFileUploadContract.PostedFile.ContentType;
                //_FileSize = imageBytes.Length;
                //uoHiddenFieldFileData.Value = System.Text.Encoding.ASCII.GetString(imageBytes);

                //ucLabelAttached.Text = "Attached: " + uoFileUploadContract.FileName + " (" + imageBytes.Length + " KB)";
                //ucLabelAttached.Visible = true;
                //uoLinkButtonRemove.Visible = true;
           
                //ContractBLL.InsertAttachHotelContract(uoFileUploadContract.FileName, imageBytes, imageBytes.Length, uoFileUploadContract.PostedFile.ContentType);
            }
        }
        #endregion

        #region uoLinkButtonRemove_Click
        protected void uoLinkButtonRemove_Click(object sender, EventArgs e)
        {
            ucLabelAttached.Text = "";
            ucLabelAttached.Visible = false;
            uoLinkButtonRemove.Visible = false;
            Label1.Visible = false;
        }
        #endregion

        #endregion

        #region Function

        #region SaveGridItems
        private void SaveGridItems(Int32 pID)
        {
            //Page.Validate("Room");
            //if (Page.IsValid)
            //{
            Int32 ContractDetailID = 0;

                if ((DataTable)ViewState["Table"] != null)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["Table"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < uoGridViewRooms.Rows.Count; i++) //Save the values one by one
                        {
                            string RoomType = dt.Rows[i]["Room Type"].ToString();
                            string dtFrom = dt.Rows[i]["Date From"].ToString();
                            string dtTo = dt.Rows[i]["Date To"].ToString();
                            string Currency = dt.Rows[i]["Currency"].ToString();
                            string RoomRate = dt.Rows[i]["Room Rate"].ToString();
                            bool TaxInclusive = Convert.ToBoolean(dt.Rows[i]["Tax Inclusive"].ToString());
                            string Tax = dt.Rows[i]["Tax(%)"].ToString();

                            Int32 Mon = Convert.ToInt32(dt.Rows[i]["Monday"].ToString());
                            Int32 Tues = Convert.ToInt32(dt.Rows[i]["Tuesday"].ToString());
                            Int32 Wed = Convert.ToInt32(dt.Rows[i]["Wednesday"].ToString());
                            Int32 Thurs = Convert.ToInt32(dt.Rows[i]["Thursday"].ToString());
                            Int32 Fri = Convert.ToInt32(dt.Rows[i]["Friday"].ToString());
                            Int32 Sat = Convert.ToInt32(dt.Rows[i]["Saturday"].ToString());
                            Int32 Sun = Convert.ToInt32(dt.Rows[i]["Sunday"].ToString());

                            //ContractDetailID = ContractBLL.AddSaveHotelDetailContract(pID, "", dtFrom, dtTo, RoomRate, RoomType, Mon, Tues, Wed, Thurs, Fri, Sat, Sun,
                            //                                       MUser.GetUserName(), TaxInclusive, Tax, Currency);

                            if (ContractDetailID != 0)
                            {
                                string strLogDescription;
                                string strFunction;

                                //Insert log audit trail (Gabriel Oquialda - 19/03/2012)
                                strLogDescription = "Hotel contract detail added.";
                                strFunction = "SaveGridItems";

                                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                                BLL.AuditTrailBLL.InsertLogAuditTrail(ContractDetailID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                            }
                        }
                    }
                }
            //}
        }
        #endregion

        #region Clear
        private void Clear()
        {
            uotextboxMondayRoom.Text = "";
            uotextboxTuesdayRoom.Text = ""; 
            uotextboxWednesdayRoom.Text = "";
            uotextboxThursdayRoom.Text = "";
            uotextboxFridayRoom.Text = ""; 
            uotextboxSaturdayRoom.Text = "";
            uotextboxSundayRoom.Text = "";
            uotextboxRoomRate.Text = "";
            uoTextBoxTax.Text = "";
        }
        #endregion

        #region DefaultValue
        private void DefaultValue()
        {
            if (uoTextBoxTax.Text == "")
            {
                uoTextBoxTax.Text = "0";
            }
            if (uotextboxMondayRoom.Text == "")
            {
                uotextboxMondayRoom.Text = "0";
            }
            if (uotextboxTuesdayRoom.Text == "")
            {
                uotextboxTuesdayRoom.Text = "0";
            }
            if (uotextboxWednesdayRoom.Text == "")
            {
                uotextboxWednesdayRoom.Text = "0";
            }
            if (uotextboxThursdayRoom.Text == "")
            {
                uotextboxThursdayRoom.Text = "0";
            }
            if (uotextboxFridayRoom.Text == "")
            {
                uotextboxFridayRoom.Text = "0";
            }
            if (uotextboxSaturdayRoom.Text == "")
            {
                uotextboxSaturdayRoom.Text = "0";
            }
            if (uotextboxSundayRoom.Text == "")
            {
                uotextboxSundayRoom.Text = "0";
            }
        }
        #endregion

        #region CreateDatatable for hotel branch
        private void CreateDatatableHotelBranch()
        {
            if (ViewState["AddBranch"].ToString() == "True")
            {
                DataTable dt = new DataTable();
                
                dt = (DataTable)ViewState["TableBranch"];
                int i_TotalRows = dt.Rows.Count;
                DataRow dr;
                dr = dt.NewRow();
                int Exists = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //DataRow row = dt.Rows.Find(uoDropDownListVendor.SelectedItem.Text);
                    if (dt.Rows[i]["Hotel Branch"].ToString() == uoDropDownListVendor.SelectedItem.Text)
                    {
                        Exists += 1;
                    }
                }
                if (Exists == 0)
                {
                    dt.Rows.Add(this._Create_DataRow_GridViewDataSourceHotelBranch(dr, uoDropDownListVendor.SelectedItem.Text,
                              uoDropDownCountry.SelectedItem.Text, uoDropDownListCity.SelectedItem.Text));
                    uoGridViewBranch.DataSource = dt;
                    uoGridViewBranch.DataBind();
                    ViewState["TableBranch"] = dt;
                }
            }
            else
            {
                _Create_DataTable_GridViewDataSourceHotelBranch(uoDropDownListVendor.SelectedItem.Text, uoDropDownCountry.SelectedItem.Text,
                                                     uoDropDownListCity.SelectedItem.Text);
                ViewState["AddBranch"] = true;
                uoGridViewBranch.Enabled = true;
                uoGridViewBranch.DataBind();
                
            }
        }
        #endregion

        #region _Create_DataTable_GridViewDataSource Hotel Branch
        void _Create_DataTable_GridViewDataSourceHotelBranch(string HotelBranch, string Country, string City)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Hotel Branch");
            dt.Columns.Add("Country");
            dt.Columns.Add("City");
            dt.Columns.Add("Delete");

            DataRow dr;
            dr = dt.NewRow();
            dt.Rows.Add(this._Create_DataRow_GridViewDataSourceHotelBranch(dr, HotelBranch, Country, City));

            ViewState["TableBranch"] = dt;
            this.uoGridViewBranch.DataSource = dt;
        }
        #endregion

        #region _Create_DataRow_GridViewDataSource Hotel Branch
        DataRow _Create_DataRow_GridViewDataSourceHotelBranch(DataRow dr, string HotelBranch, string Country, string City)
        {
            dr["Hotel Branch"] = HotelBranch;
            dr["Country"] = Country;
            dr["City"] = City;

            return dr;
        }
        #endregion

        #region CreateDatatable for hotel room type
        private void  CreateDatatable()
        {
            DefaultValue();
            if (ViewState["Add"].ToString() == "True")
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Table"];
                int i_TotalRows = dt.Rows.Count;
                DataRow dr;
                dr = dt.NewRow();

                dt.Rows.Add(this._Create_DataRow_GridViewDataSource(dr, uoDropDownListCurrency.SelectedItem.Text, uoDropDownListRoomType.SelectedItem.Text,
                                                                    uotextboxRoomDateFrom.Text, uotextboxRoomDateTo.Text, uotextboxRoomRate.Text,
                                                                    Convert.ToBoolean(uoCheckBoxTaxInclusive.Checked), uoTextBoxTax.Text,
                                                                     uotextboxMondayRoom.Text, uotextboxTuesdayRoom.Text, uotextboxWednesdayRoom.Text, uotextboxThursdayRoom.Text,
                                                                     uotextboxFridayRoom.Text, uotextboxSaturdayRoom.Text, uotextboxSundayRoom.Text));
                uoGridViewRooms.DataSource = dt;
                uoGridViewRooms.DataBind();
                ViewState["Table"] = dt;
            }
            else
            {
                _Create_DataTable_GridViewDataSource(uoDropDownListCurrency.SelectedItem.Text, uoDropDownListRoomType.SelectedItem.Text,
                                                     uotextboxRoomDateFrom.Text, uotextboxRoomDateTo.Text, uotextboxRoomRate.Text, 
                                                     Convert.ToBoolean(uoCheckBoxTaxInclusive.Checked), uoTextBoxTax.Text,
                                                     uotextboxMondayRoom.Text, uotextboxTuesdayRoom.Text, uotextboxWednesdayRoom.Text, uotextboxThursdayRoom.Text,
                                                     uotextboxFridayRoom.Text, uotextboxSaturdayRoom.Text, uotextboxSundayRoom.Text);
                ViewState["Add"] = true;
                uoGridViewRooms.Enabled = true;
                uoGridViewRooms.DataBind();
            }
        }
        #endregion

        #region _Create_DataTable_GridViewDataSource
        void _Create_DataTable_GridViewDataSource(string currency, string RoomType, string dtFrom, string dtTo, string RoomRate, bool TaxInclusive, string Tax,
                                                  string Mon, string Tues, string Wed, string Thurs, string Fri, string Sat, string Sun)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Room Type");
            dt.Columns.Add("Date From");
            dt.Columns.Add("Date To");
            dt.Columns.Add("Currency");
            dt.Columns.Add("Room Rate");
            dt.Columns.Add("Tax Inclusive");
            dt.Columns.Add("Tax(%)");
            dt.Columns.Add("Delete");

            dt.Columns.Add("Monday");
            dt.Columns.Add("Tuesday");
            dt.Columns.Add("Wednesday");
            dt.Columns.Add("Thursday");
            dt.Columns.Add("Friday");
            dt.Columns.Add("Saturday");
            dt.Columns.Add("Sunday");

            DataRow dr;
            dr = dt.NewRow();
            dt.Rows.Add(this._Create_DataRow_GridViewDataSource(dr, currency, RoomType, dtFrom, dtTo, RoomRate, TaxInclusive, Tax,
                                                                Mon, Tues, Wed, Thurs, Fri, Sat, Sun));

            ViewState["Table"] = dt;
            this.uoGridViewRooms.DataSource = dt;
        }
        #endregion

        #region _Create_DataRow_GridViewDataSource
        DataRow _Create_DataRow_GridViewDataSource(DataRow dr, string currency, string RoomType, string dtFrom, string dtTo, string RoomRate, bool TaxInclusive, string Tax,
                                                   string Mon, string Tues, string Wed, string Thurs, string Fri, string Sat, string Sun)
        {
            dr["Currency"] = currency;
            dr["Room Type"] = RoomType;
            dr["Date From"] = dtFrom;
            dr["Date To"] = dtTo;
            dr["Room Rate"] = RoomRate;
            dr["Tax Inclusive"] = TaxInclusive;
            dr["Tax(%)"] = Tax;

            dr["Monday"] = Mon;
            dr["Tuesday"] = Tues;
            dr["Wednesday"] = Wed;
            dr["Thursday"] = Thurs;
            dr["Friday"] = Fri;
            dr["Saturday"] = Sat;
            dr["Sunday"] = Sun;
            
            return dr;
        }
        #endregion

        #region AddDataToDataTable
        //private void AddDataToDataTable(string RoomType, string DtFrom, string DtTo, string Currency, string RoomRate, bool TaxInclusive, string Tax,
        //                                string Mon, string Tues, string Wed, string Thurs, string Fri, string Sat, string Sun, string RoomTypeID, string CurrencyID,
        //                                DataTable myTable)
        //{
            //DataRow row;
            //row = myTable.NewRow();
            //row["Room Type"] = RoomType;
            //row["Date From"] = DtFrom;
            //row["Date To"] = DtTo;
            //row["Currency"] = Currency;
            //row["Room Rate"] = RoomRate;
            //row["Tax Inclusive"] = TaxInclusive;
            //row["Tax(%)"] = Tax;

            //row["Monday"] = Mon;
            //row["Tuesday"] = Tues;
            //row["Wednesday"] = Wed;
            //row["Thursday"] = Thurs;
            //row["Friday"] = Fri;
            //row["Saturday"] = Sat;
            //row["Sunday"] = Sun;

            //row["RoomTypeID"] = RoomTypeID;

            //myTable.Rows.Add(row);

            //uoGridViewRooms.DataSource = myTable;
            //uoGridViewRooms.DataBind();
        //}
        #endregion

        #region GenerateGridView
        //private void GenerateGridView()
        //{
        //    BoundField RoomType = new BoundField();
        //    BoundField dtFrom = new BoundField();
        //    BoundField dtTo = new BoundField();
        //    BoundField Currency = new BoundField();
        //    BoundField RoomRate = new BoundField();
        //    BoundField TaxInclusive = new BoundField();
        //    BoundField Tax = new BoundField();

        //    ButtonField btnDelete = new ButtonField();

        //    //i_SalesOrderDetailID.DataField = "i_SalesOrderDetailID";

        //    RoomType.DataField = "Room Type";
        //    RoomType.HeaderText = "Room Type";

        //    dtFrom.DataField = "Date From";
        //    dtFrom.HeaderText = "Date From";

        //    dtTo.DataField = "Date To";
        //    dtTo.HeaderText = "Date To";

        //    Currency.DataField = "Currency";
        //    Currency.HeaderText = "Currency";

        //    RoomRate.DataField = "Room Rate";
        //    RoomRate.HeaderText = "Room Rate";

        //    TaxInclusive.DataField = "Tax Inclusive";
        //    TaxInclusive.HeaderText = "Tax Inclusive";

        //    //Tax.DataField = "Tax(%)";
        //    //Tax.HeaderText = "Tax(%)";

        //    btnDelete.ButtonType = ButtonType.Link;
        //    btnDelete.Text = "Delete";
        //    btnDelete.CommandName = "Select";
        //    btnDelete.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

        //    this.uoGridViewRooms.Columns.Add(RoomType);
        //    this.uoGridViewRooms.Columns.Add(dtFrom);
        //    this.uoGridViewRooms.Columns.Add(dtTo);
        //    this.uoGridViewRooms.Columns.Add(Currency);
        //    this.uoGridViewRooms.Columns.Add(RoomRate);
        //    this.uoGridViewRooms.Columns.Add(TaxInclusive);
        //}
        #endregion

        #region Room Currency Load

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all currency  
        /// </summary>
        private void CurrencyLoad()
        {            
            DataTable dt = null;
            try
            {
                dt = ContractBLL.CurrencyLoad();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCurrency.DataSource = dt;
                    uoDropDownListCurrency.DataTextField = "colCurrencyNameVarchar"; // "colCurrencyCodeVarchar";
                    uoDropDownListCurrency.DataValueField = "colCurrencyIDInt";
                    uoDropDownListCurrency.DataBind();
                }
                else
                {
                    uoDropDownListCurrency.DataBind();
                }
                uoDropDownListCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region Room Type

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all hotel room type   
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// </summary>

        private void HotelRoomTypeLoad()
        {           
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelRoomTypeGetDetails();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListRoomType.DataSource = dt;
                    uoDropDownListRoomType.DataTextField = "colRoomNameVarchar";
                    uoDropDownListRoomType.DataValueField = "colRoomTypeID";
                    uoDropDownListRoomType.DataBind();
                }
                else
                {
                    uoDropDownListRoomType.DataBind();
                }
                uoDropDownListRoomType.Items.Insert(0, new ListItem("--Select Room Type--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region Country List

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select country list by vendor ID   
        /// </summary>
        private void CountryListByVendorID()
        {            
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListVendor.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVendor.SelectedValue);
                }

                dt = HotelBLL.CountryByVendorBranchID(VendorID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownCountry.DataSource = dt;
                    uoDropDownCountry.DataTextField = "colCountryNameVarchar";
                    uoDropDownCountry.DataValueField = "colCountryIDInt";
                    uoDropDownCountry.DataBind();
                    uoDropDownCountry.SelectedValue = dt.Rows[0]["colCountryIDInt"].ToString();
                    uoDropDownCountry.Enabled = false;
                }
                else
                {
                    uoDropDownCountry.DataBind();
                    uoDropDownCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region City List

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select city list by vendor ID   
        /// </summary>

        private void CityListByVendorID()
        {            
            DataTable dt = null;
            try
            {
                int CountryID = 0;
                if (uoDropDownCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(uoDropDownCountry.SelectedValue);
                }
                int VendorID = 0;
                if (uoDropDownListVendor.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVendor.SelectedValue);
                }

                dt = HotelBLL.CityByVendorBranchIDCountryID(VendorID, CountryID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataSource = dt;
                    uoDropDownListCity.DataTextField = "colCityNameVarchar";
                    uoDropDownListCity.DataValueField = "colCityIDInt";
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.SelectedValue = dt.Rows[0]["colCityIDInt"].ToString();
                    uoDropDownListCity.Enabled = false;
                }
                else
                {
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select City--", "0"));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region Save Contract Hotel Header

        /// <summary>
        /// Date Created: 19/08/2011
        /// Created By: Ryan Bautista
        /// (description) Add / save hotel contract          
        /// </summary>
        /// 
        private Int32 AddSaveHotelContract(string VendorID, string vContract, string Remarks, string dtStart, string dtEnd, string RCCLRep,
                                            string vRep, string dtRCCLAccepted, string dtVendorAccepted, string CountryID, string CityID, string Username,
                                            string MealRate, string MealRateTax, bool TaxInclusive, bool Breakfast, bool Lunch, bool Dinner,
                                            bool LunchDinner, bool Shuttle, string Filename, string FileType, byte[] FileData, string DateUploaded,
                                            string ContractID)
        {
            return 0;
            //return BLL.ContractBLL.AddSaveHotelContract(VendorID, vContract, Remarks, dtStart, dtEnd, RCCLRep, vRep, dtRCCLAccepted, dtVendorAccepted,
            //                                        CountryID, CityID, Username, MealRate, MealRateTax, TaxInclusive, Breakfast, Lunch, Dinner, LunchDinner,
            //                                        Shuttle, Filename, FileType, FileData, DateUploaded, ContractID, out sqlTransaction);
        }
        #endregion

        #region Save Contract Hotel Detail

        /// <summary>
        /// Date Created: 19/08/2011
        /// Created By: Ryan Bautista
        /// (description) Add / save hotel contract detail          
        /// </summary>
        /// 
        private void AddSaveHotelContractDetail(Int32 ContractID, string currency, string RoomType, string dtFrom, string dtTo, string RoomRate, bool TaxInclusive, string Tax,
                                                   string Mon, string Tues, string Wed, string Thurs, string Fri, string Sat, string Sun)
        {            
            //BLL.ContractBLL.AddSaveHotelContract(VendorID, vContract, Remarks, dtStart, dtEnd, RCCLRep, vRep, dtRCCLAccepted, dtVendorAccepted,
            //                                        CountryID, CityID);
        }
        #endregion

        #region ChangeToUpperCase

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Format data to uppercase        
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {            
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }
        #endregion

        #region Hotel Load
        ///// <summary>
        ///// Date Created: 23/08/2011
        ///// Created By: Ryan Bautista
        ///// (description) Select all hotel details by region 
        ///// </summary>
        private void HotelLoad()
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelVendorGetDetailsByRegion(MUser.GetUserName());
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendorMain.DataSource = dt;
                    uoDropDownListVendorMain.DataTextField = "colVendorNameVarchar";
                    uoDropDownListVendorMain.DataValueField = "colVendorIdInt";
                    uoDropDownListVendorMain.DataBind();
                    if (Request.QueryString["hvID"] != null)
                    {
                        uoDropDownListVendorMain.SelectedValue = Request.QueryString["hvID"];
                        uoDropDownListVendorMain.Enabled = false;

                        if (uotextboxContractTitle.Text == "")
                        {
                            uotextboxContractTitle.Text = uoDropDownListVendorMain.SelectedItem.Text;
                        }
                    }
                }
                else
                {
                    uoDropDownListVendorMain.DataBind();
                }
                uoDropDownListVendorMain.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region Hotel Branch Load
        /// <summary>
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Select all hotel branch details  
        /// </summary>
        private void HotelBranchLoad()
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelBranchList();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendor.DataSource = dt;
                    uoDropDownListVendor.DataTextField = "colVendorBranchNameVarchar";
                    uoDropDownListVendor.DataValueField = "colBranchIDInt";
                    uoDropDownListVendor.DataBind();

                }
                else
                {
                    uoDropDownListVendor.DataBind();
                }
                uoDropDownListVendor.Items.Insert(0, new ListItem("--Select Hotel Branch--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region Hotel Branch Load by VendorID
        /// <summary>
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Select all hotel branch details  
        /// </summary>
        private void HotelBranchLoadByVendorID()
        {
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListVendorMain.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVendorMain.SelectedValue);
                }
                //dt = HotelBLL.HotelBranchList();
                dt = HotelBLL.HotelBranchListByVendorID(VendorID, MUser.GetUserName());
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendor.DataSource = dt;
                    uoDropDownListVendor.DataTextField = "colVendorBranchNameVarchar";
                    uoDropDownListVendor.DataValueField = "colBranchIDInt";
                    uoDropDownListVendor.DataBind();
                    if (Request.QueryString["vmId"] != null)
                    {
                        uoDropDownListVendor.SelectedValue = Request.QueryString["vmId"];
                        uoDropDownListVendor.Enabled = false;
                    }
                    else
                    {
                        uoDropDownListVendor.SelectedValue = dt.Rows[0]["colBranchIDInt"].ToString();
                    }
                }
                else
                {
                    uoDropDownListVendor.DataBind();
                    uoDropDownListVendor.Items.Insert(0, new ListItem("--Select Hotel Branch--", "0"));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion
                
        #region Contract load info
        ///// <summary>
        ///// Date Created: 23/08/2011
        ///// Created By: Ryan Bautista
        ///// (description) Select contract by branch
        ///// </summary>
        private void ContractLoadByBranch()
        {
            DataTable dt = null;
            try
            {
                //dt = HotelBLL.HotelVendorGetDetailsByRegion(MUser.GetUserName());
                if (Request.QueryString["vmId"] != "" && Request.QueryString["cID"] != null)
                {
                    dt = BLL.ContractBLL.GetVendorHotelContractByContractID(Request.QueryString["cID"], Request.QueryString["vmId"]);
                    if (dt.Rows.Count > 0)
                    {
                        //uoTextBoxVendorCode.Text = dt.Rows[0]["colVendorCodeVarchar"].ToString();
                        //uoTextBoxVendorName.Text = dt.Rows[0]["colVendorBranchNameVarchar"].ToString();
                        uoHiddenFieldContractID.Value = dt.Rows[0]["colContractIdInt"].ToString();
                        uotextboxContractTitle.Text = dt.Rows[0]["colContractNameVarchar"].ToString();
                        uotextboxRemarks.Text = dt.Rows[0]["colRemarksVarchar"].ToString();
                        uotextboxRCCLRep.Text = dt.Rows[0]["colRCCLPersonnel"].ToString();
                        uotextboxVendorRep.Text = dt.Rows[0]["colVendorPersonnel"].ToString();
                        uoCheckBoxShuttle.Checked = Convert.ToBoolean(dt.Rows[0]["colWithShuttleBit"].ToString());
                        uoCheckBoxBreakfast.Checked = Convert.ToBoolean(dt.Rows[0]["colBreakfastBit"].ToString());
                        uoCheckBoxLunch.Checked = Convert.ToBoolean(dt.Rows[0]["colLunchBit"].ToString());
                        uoCheckBoxDinner.Checked = Convert.ToBoolean(dt.Rows[0]["colDinnertBit"].ToString());
                        uoCheckBoxLunchDinner.Checked = Convert.ToBoolean(dt.Rows[0]["colLunchOrDinnerBit"].ToString());

                        string dtStart = (dt.Rows[0]["colContractDateStartedDate"].ToString().Length > 0)
                            ? dt.Rows[0]["colContractDateStartedDate"].ToString()
                            : "";
                        string dtEnd = (dt.Rows[0]["colContractDateEndDate"].ToString().Length > 0)
                           ? dt.Rows[0]["colContractDateEndDate"].ToString()
                           : "";
                        uotextboxStartDate.Text = Convert.ToDateTime(dtStart).ToShortDateString();
                        uotextboxEndDate.Text = Convert.ToDateTime(dtEnd).ToShortDateString();

                        string dtRCCLAccepted = (dt.Rows[0]["colRCCLAcceptedDate"].ToString().Length > 0)
                            ? Convert.ToDateTime(dt.Rows[0]["colRCCLAcceptedDate"].ToString()).ToShortDateString()
                            : "";
                        string dtVendorAccepted = (dt.Rows[0]["colVendorAcceptedDate"].ToString().Length > 0)
                           ? Convert.ToDateTime(dt.Rows[0]["colVendorAcceptedDate"].ToString()).ToShortDateString()
                           : "";

                        uoTextBoxRCCLDateAccepted.Text = dtRCCLAccepted;
                        uoTextBoxVendorDateAccepted.Text = dtVendorAccepted;

                        uoTextBoxMealRate.Text = dt.Rows[0]["colMealRateMoney"].ToString();
                        uoTextBoxMealRateTax.Text = dt.Rows[0]["colMealRateTaxDecimal"].ToString();
                        uoCheckBoxMealRateTaxInclusive.Checked = Convert.ToBoolean(dt.Rows[0]["colMealRateTaxInclusiveBit"].ToString());

                        ViewState["Add"] = true;
                        ViewState["Table"] = dt;
                        uoGridViewRooms.DataSource = dt;
                        uoGridViewRooms.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelContractLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"] != "" && Request.QueryString["cID"] != null)
            {
                strLogDescription = "Ammend linkbutton for hotel contract editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for hotel contract editor clicked.";
            }

            strFunction = "HotelContractLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, MUser.GetUserName());
        }
        #endregion

    }
}

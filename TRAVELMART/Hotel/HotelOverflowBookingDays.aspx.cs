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
using AjaxControlToolkit;
using System.Configuration;
using System.Web.Security;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.Collections;

namespace TRAVELMART.Hotel
{
    public partial class HotelOverflowBookingDays : System.Web.UI.Page
    {
        #region DEFINITION
        OverFlowBookingBLL overflowBLL = new OverFlowBookingBLL();
        HotelBookingsBLL bookingsBLL = new HotelBookingsBLL();
        #endregion
        #region EVENTS
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime currDate = GlobalCode.GetClientTime();
            InitializeValues();

            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;

                HtmlTable uoTblDate = (HtmlTable)Master.FindControl("uoTblDate");
                uoTblDate.Style.Add("display", "none");
            }
            else
            {
                if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                }
                else
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"];
                }
            }
            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {

                Session["strPrevPage"] = Request.RawUrl;
                SetDefaults();
                GetOverflow();
                GetHotels();

                //uoPanelhotels.Visible = true;
                uoHiddenFieldRoomType.Value = "";
                BindRegionList();
                BindPortList();

            }
            if (uoHiddenFieldPopupHotel.Value == "1")
            {
                GetOverflow();
                GetRoomBlocks(GlobalCode.Field2Int(uoHiddenFieldBranch.Value), false);
            }
            uoHiddenFieldPopupHotel.Value = "0";
        }

        

        protected void uoButtonCancel_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");
                cpe.ClientState = "true";
                cpe.Collapsed = true;
            }
        }
        /// <summary>
        /// Modified by:    Josephine Gad
        /// Date MOdified:  28/09/2012
        /// Description:    Get value of no of nites from uoHiddenFieldNoOfNites
        /// -----------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveOverFlow();
        }


        void SaveOverFlow()
        {

            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");
                TextBox txtCheckIn = (TextBox)item.FindControl("uoTxtBoxCheckIn");
                TextBox txtDays = (TextBox)item.FindControl("uoTxtBoxDays");
                DropDownList RoomType = (DropDownList)item.FindControl("uoDropDownListRoomType");
                RadioButtonList Source = (RadioButtonList)item.FindControl("uoRBtnAllocations");

                if (lnkBtn.CommandArgument.ToString() == uoHiddenFieldBranch.Value)
                {
                    int numOfPeople = GetSeafarersToBook();
                    DateTime CheckinDt = GlobalCode.Field2DateTime(txtCheckIn.Text);
                    uoHIddenFieldSFCount.Value = numOfPeople.ToString();

                    txtDays.Text = uoHiddenFieldNoOfNites.Value;
                    uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(txtCheckIn.Text).AddDays(GlobalCode.Field2Int(txtDays.Text)).ToShortDateString();
                  

                    CheckRoomValidity(CheckinDt, lnkBtn.Text, GlobalCode.Field2Int(uoHiddenFieldBranch.Value),
                        RoomType.SelectedItem.Text, GlobalCode.Field2Int(Source.SelectedValue));
                    return;
                }
            }
        
        
        
        }








        /// <summary>
        /// Modified By: Charlene Remotigue
        /// Date Modified: 03/04/2012
        /// Description: get total and avaliable room values
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  26/07/2012
        /// Description:    Change HotelTransactionOverflow.Hotels to GetHotelList()
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void uoRepeaterHotels_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                uoHiddenFieldBranch.Value = e.CommandArgument.ToString();

                GetRoomBlocks(Int32.Parse(e.CommandArgument.ToString()), true);
                
                List<Hotels> list = new List<Hotels>();
                list = GetHotelList();
                var hotel = (from a in list//HotelTransactionOverflow.Hotels
                             where a.BranchId == GlobalCode.Field2Int(e.CommandArgument.ToString())
                             select new
                             {
                                 HotelName = a.BranchName,
                                 IsAccredited = a.isAccredited,
                                 withContract = a.withContract,
                                 VendorId = a.VendorId,
                                 CityId = a.CityId,
                                 CountryId = a.CountryId,
                                 ContractId = a.ContractId,
                             }).ToList();
                if (hotel.Count > 0)
                {
                    ViewState["VendorId"] = hotel[0].VendorId;
                    ViewState["CountryId"] = hotel[0].CountryId;
                    ViewState["CityId"] = hotel[0].CityId;
                    ViewState["Contract"] = hotel[0].ContractId;
                }
                
            }
        }

        protected void uoBtnCheckRooms_Click(object sender, EventArgs e)
        {
            GetRoomBlocks(GlobalCode.Field2Int(uoHiddenFieldBranch.Value), false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 12/04/2012
        /// Description: export overflow list 
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  26/07/2012
        /// Description:    Change HotelTransactionOverflow.OverflowBooking2 to GetOverflowList()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                List<OverflowBooking2> list = new List<OverflowBooking2>();
                list = GetOverflowList();
                if (list.Count > 0)
                {
                    dt = overflowBLL.GetOverflowExtract(uoHiddenFieldUser.Value);
                    CreateFile(dt);
                }
                else
                {
                    AlertMessage("There are no overflow bookings to export.");
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
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session.Remove("Port");
            BindPortList();
            SetDefaults();
            GetOverflow();
            GetHotels();
        }
        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            SetDefaults();
            GetOverflow();
            GetHotels();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Feb/2013
        /// Description:    Get the order to be used
        /// -------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            GetOverflow();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
        /// Modified by:        Josephine Gad
        /// Modified Created:   25/05/2012
        /// Description:        Use session to get region list; use RegionListByUser if session is null
        /// </summary>
        private void BindRegionList()
        {
            List<RegionList> list = new List<RegionList>();
            try
            {
                if (Session["HotelDashboardDTO_RegionList"] != null)
                {
                    //Session["HotelDashboardDTO_RegionList"] = HotelDashboardDTO.RegionList;
                    list = (List<RegionList>)Session["HotelDashboardDTO_RegionList"];
                }
                else
                {
                    list = CountryBLL.RegionListByUser(uoHiddenFieldUser.Value);
                    Session["HotelDashboardDTO_RegionList"] = list;
                }
                if (list.Count > 0)
                {
                    uoDropDownListRegion.Items.Clear();
                    uoDropDownListRegion.DataSource = list;
                    uoDropDownListRegion.DataTextField = "RegionName";
                    uoDropDownListRegion.DataValueField = "RegionId";
                    uoDropDownListRegion.DataBind();
                }
                uoDropDownListRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));

                string sRegion = GlobalCode.Field2String(Session["Region"]);
                if (sRegion != "")
                {
                    if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
                    {
                        uoDropDownListRegion.SelectedValue = sRegion;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/04/2012
        /// Description: set date
        /// </summary>
        string lastDate = null;
        public string setDate()
        {
            string currentDate = Eval("colDate").ToString();

            //GlobalCode.Field2String(Session["UserVendor"]) = Eval("colBrandId").ToString();
            //GlobalCode.Field2String(Session["UserBranchID"]) = Eval("colBranchId").ToString();

            if (currentDate.Length == 0)
            {
                currentDate = "";
            }

            if (lastDate != currentDate)
            {
                lastDate = currentDate;
                DateTime dt = GlobalCode.Field2DateTime(currentDate);
                return String.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/04/2012
        /// Description: get branch room blocks
        /// -----------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  25/07/2012
        /// Description:    Use GetRoomBlocksList instead of HotelBookings.RoomBlocks
        /// -----------------------------------------
        /// Modified by:    Josephine Gad
        /// Date MOdified:  28/09/2012
        /// Description:    Get value of no of nites from uoHiddenFieldNoOfNites
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="isCollapsed"></param>
        protected void GetRoomBlocks(int BranchId, bool isCollapsed)
        {
            List<RoomBlocks> roomList = new List<RoomBlocks>();
            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");
                TextBox txtCheckIn = (TextBox)item.FindControl("uoTxtBoxCheckIn");
                TextBox txtDays = (TextBox)item.FindControl("uoTxtBoxDays");
                txtDays.Text = uoHiddenFieldNoOfNites.Value;

                if (txtCheckIn.Text == "")
                {
                    txtCheckIn.Text = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
                }

                uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(txtCheckIn.Text).AddDays(GlobalCode.Field2Int(txtDays.Text)).ToShortDateString();
                CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");

                if (lnkBtn.CommandArgument.ToString() == BranchId.ToString())
                {
                    //isCollapsed = cpe.Collapsed;
                    //cpe.Collapsed = !isCollapsed;
                    //cpe.ClientState = (!isCollapsed).ToString();
                    if (!isCollapsed || cpe.Collapsed)
                    {
                        int numOfPeople = GetSeafarersToBook();
                        //bookingsBLL.getRemainingRooms(GlobalCode.Field2DateTime(txtCheckIn.Text),
                        //    GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value),
                        //    numOfPeople, GlobalCode.Field2Int(uoHiddenFieldRoomType.Value), BranchId);
                        Session["HotelBookingsRoomBlocks"] = null;
                        roomList = GetRoomBlocksList(GlobalCode.Field2DateTime(txtCheckIn.Text), BranchId);

                        ListView l1 = (ListView)item.FindControl("uoRoomList");
                        l1.DataSource = roomList;//HotelBookings.RoomBlocks;                        
                        l1.DataBind();
                    }
                }
                else
                {
                    cpe.Collapsed = true;
                    cpe.ClientState = "true";
                }
            }
        }

        /// <summary>
        /// Modified by:    Josephine Gad
        /// Date MOdified:  27/03/2012
        /// Description:    Initialize Date from in Page_load
        /// </summary>
        protected void InitializeValues()
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                if (GlobalCode.Field2String(User.Identity.Name) == "")
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    Session["UserName"] = GlobalCode.Field2String(User.Identity.Name);
                }
            }
            if (!Membership.GetUser(GlobalCode.Field2String(Session["UserName"])).IsOnline)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
            {
                uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            }
            else
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
            }

            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: set alternate color
        /// </summary>
        /// <returns></returns> 
        string lastDataFieldValue = null;
        string lastClass = "alternateBg";
        public string OverflowChangeRowColor()
        {

            string currentDataFieldValue = Eval("SeafarerId").ToString();
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                if (lastClass == "")
                {
                    lastClass = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
                else
                {
                    lastClass = "";
                    return "<tr>";
                }
            }
            else
            {
                if (lastClass == "")
                {
                    lastClass = "";
                    return "<tr>";
                }
                else
                {
                    lastClass = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
            }
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Apr/2014
        /// Description:    load default values
        /// ----------------------------------        
        /// </summary>
        protected void SetDefaults()
        {
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldStartDate.Value = uoHiddenFieldDate.Value;
            int iRegionID = GlobalCode.Field2Int(Session["Region"]);
            int iPortID = GlobalCode.Field2Int(Session["Port"]);
            overflowBLL.LoadHotelOverflowPageDays(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0, iRegionID, iPortID);            
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/03/2012
        /// Description: get all hotel exceptions
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  26/07/2012
        /// Description:    Change HotelTransactionOverflow.OverflowBooking2 to GetOverflowList()
        /// </summary>
        protected void GetOverflow()
        {
            try
            {
                List<OverflowBooking2> list = new List<OverflowBooking2>();
                list = GetOverflowList();
                
                uoOverflowList.DataSource = list;
                uoOverflowList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/03/2012
        /// Description: get hotels for specialist
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  26/07/2012
        /// Description:    Change HotelTransactionOverflow.Hotels to GetHotelList()
        /// </summary>
        protected void GetHotels()
        {
            try
            {
                List<Hotels> list = new List<Hotels>();
                list = GetHotelList();
                uoRepeaterHotels.DataSource = list;//HotelTransactionOverflow.Hotels;
                uoRepeaterHotels.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: highlight entry with no remaining room blocks
        /// </summary>
        /// <returns></returns>
        public string Valid()
        {
            bool valid = Convert.ToBoolean(Eval("valid"));

            if (!valid)
            {
                //uoHiddenFieldValid.Value = "false";
                return "font-weight: bold; color: #FF0000; padding: 0px; border: thin solid #FF0000";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: count seafarers to be booked
        /// ---------------------------------------
        /// Modified By:    Gabriel Oquialda
        /// Date Modified:  21/03/2012
        /// Description:    Add uoHiddenFieldRoomType, IsSingleRoom and IsDoubleRoom for room type alert
        /// </summary>
        /// <returns></returns>
        protected int GetSeafarersToBook()
        {
            int Count = 0;
            bool IsSingleRoom = false;
            bool IsDoubleRoom = false;

            foreach (ListViewItem item in uoOverflowList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                HiddenField hfRoomType = (HiddenField)item.FindControl("hfRoomType");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    Count += 1;
                    if (hfRoomType.Value == "1")
                    {
                        IsSingleRoom = true;
                    }
                    if (hfRoomType.Value == "2")
                    {
                        IsDoubleRoom = true;
                    }
                }
            }
            if (IsSingleRoom && !IsDoubleRoom)
            {
                uoHiddenFieldRoomType.Value = "1";
            }
            else if (!IsSingleRoom && IsDoubleRoom)
            {
                uoHiddenFieldRoomType.Value = "2";
            }
            else if (IsSingleRoom && IsDoubleRoom)
            {
                uoHiddenFieldRoomType.Value = "3";
            }
            else
            {
                uoHiddenFieldRoomType.Value = "";
            }
            return Count;
        }



        /// <summary>
        /// Author: Muhallidin G wali
        /// Date Created: 07/03/2012
        /// Description: count seafarers to be booked 
        /// ---------------------------------------
        /// </summary>
        /// <returns></returns>
        private double GetSeafarersCountToBook()
        {
            double Count = 0;

            foreach (ListViewItem item in uoOverflowList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                HiddenField hfRoomType = (HiddenField)item.FindControl("hfRoomType");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                   
                    if (hfRoomType.Value == "1")
                    {
                        Count += 1.0;
                    }
                    if (hfRoomType.Value == "2")
                    {
                        Count += 0.5;
                    }
                }
            }
          
            return Count;
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   10/04/2012
        /// Description:    get branch room blocks
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="isCollapsed"></param>
        protected int GetDaysNumber(int BranchId)
        {
            int DayNumber = 0;
            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");
                if (BranchId == GlobalCode.Field2Int(lnkBtn.Text))
                {
                    TextBox txtDays = (TextBox)item.FindControl("uoTxtBoxDays");
                    txtDays.Text = uoHiddenFieldNoOfNites.Value;
                    DayNumber = GlobalCode.Field2Int(txtDays.Text);
                }
            }

            return DayNumber;
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   10/04/2012
        /// Description:    get branch room blocks
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="isCollapsed"></param>
        protected int GetRoomTypeID(int BranchId)
        {
            int RoomID = 1;
            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");
                if (BranchId == GlobalCode.Field2Int(lnkBtn.CommandArgument.ToString()))
                {
                    DropDownList RoomType = (DropDownList)item.FindControl("uoDropDownListRoomType");
                    RoomID = GlobalCode.Field2Int(RoomType.SelectedValue);
                }
            }

            return RoomID;
        }



        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: check if there are room blocks available per hotel per room type per day
        /// ---------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  22/03/2012
        /// Description:    Get message from ConfigurationSettings if there is no available room blocks
        /// ----------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date Modified: 10/04/2012
        /// Description: Change checking and booking process
        /// ----------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  24/07/2012
        /// Description:    Use GetRoomBlocksList instead of HotelBookings.RoomBlocks
        ///                 Add CheckinDt parameter
        /// ----------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  28/09/2012
        /// Description:    Change the way of getting valid contract, override and emrgency room because of date range
        ///                 add checkout date in selection
        /// ----------------------------------------
        /// </summary>
        /// <param name="Branch"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="source"></param>
        protected void  CheckRoomValidity(DateTime CheckinDt, string Branch, int BranchId, string RoomType, int source)
        {
            bool isBooked = false;
            int RoomId = GetRoomTypeID(BranchId);
            int? contractID = null;

            bool isemergency = false;
            decimal? RemainContract = null;
            bool isvalid = false;

            int VendorID = 0;
            DateTime CheckoutDt =  GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value).AddDays(-1);

            double SeafarerCount = GetSeafarersCountToBook();
            List<RemainRoomBlocks> list = new List<RemainRoomBlocks>();
            list = bookingsBLL.getRemainingRooms(CheckinDt, CheckoutDt, RoomId, BranchId);


            if (source != 2)
            {
                var roomContract = (from a in list
                                    where a.RemainVacantRoom >= (decimal?)SeafarerCount
                                    && a.RoomType == (int?)RoomId
                                    select new
                                    {
                                        ContractID = a.ContractID,
                                        validRemaining = a.RemainVacantRoom,
                                        RemainContract = a.RemainContractRoom,
                                        validOverride = a.OverrideRoomBlock,
                                        RoomTypeId = a.RoomType
                                    }).ToList();
                if (roomContract.Count > 0)
                {
                    contractID = roomContract[0].ContractID;
                    RemainContract = roomContract[0].RemainContract;
                    if (roomContract.Count >= GlobalCode.Field2Int(uoHiddenFieldNoOfNites.Value)) isvalid = true;
                }
           }
           

           if (source == 2)
           {

                var validEmergency = (from a in list
                                where a.EmergencyRoomBlock >= (decimal?)SeafarerCount
                                && a.RoomType == (int?)RoomId
                                select new
                                {
                                    validEmergency = a.EmergencyRoomBlock,
                                    RoomTypeId = a.RoomType,
                                    RemainContract = 0
                                }).ToList();

                if (validEmergency.Count > 0)
                {
                    isemergency = true;
                    contractID = null;
                    RemainContract = 0;
                    isvalid = true;
                }
            }
            if (isvalid == true)
            {
                BookSeafarer(RemainContract, BranchId, isemergency, RoomId, contractID, VendorID, out isBooked);

                string sMessage = "Hotel Bookings Approved";

                if (isBooked)
                {
                    Session["HotelTransactionOverflowOverflowBooking"] = null;
                    AlertMessage(sMessage);
                }
                else
                {
                    AlertMessage("Seafarer cannot be booked on the same date.");
                }

                SetDefaults();
                GetOverflow();
                GetRoomBlocks(BranchId, false);
            }
            else
            {
                string sMessage;
                if (source == 1)
                {
                    sMessage = ConfigurationSettings.AppSettings["MessageRoomBlocksOverride"].ToString();
                }
                else if (source == 1)
                {
                    sMessage = ConfigurationSettings.AppSettings["MessageRoomBlocksEmergency"].ToString();
                }
                else
                {
                    sMessage = ConfigurationSettings.AppSettings["MessageRoomBlocksContract"].ToString();
                }

                sMessage = sMessage.Replace("(COUNT)", uoHIddenFieldSFCount.Value);
                sMessage = sMessage.Replace("(HOTELNAME)", Branch);

                AlertMessage(sMessage);
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: book selected seafarers
        /// --------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 10/04/2012
        /// Description: loop through repeater for booking details
        /// --------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  25/07/2012
        /// Description:    Add ViewState["idBigint"] and ViewState["seqNo"]
        /// -----------------------------------------
        /// Modified by:    Josephine Gad
        /// Date MOdified:  28/09/2012
        /// Description:    Get value of no of nites from uoHiddenFieldNoOfNites
        /// -----------------------------------------
        /// </summary>
        private void BookSeafarer(int BranchId, int Source, int RoomTypeId,  out bool Valid)
        {
            DateTime currDate = CommonFunctions.GetCurrentDateTime();
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
            string sMessage = "";
            Valid = false;
            //if (uoRBtnAllocations.Visible)
            //{
            //    Source = uoRBtnAllocations.SelectedIndex;
            //}

            DateTime? vCheckIn = null;
            DateTime? vTime = null;
            string vConfirmation = null;
            string vRemarks = null;

            string vHotelStatus = null;
            bool? vShuttle = null;

            GetSeafarerDetails();
            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");

                CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");

                if (lnkBtn.CommandArgument.ToString() == BranchId.ToString())
                {
                    TextBox txtCheckIn =(TextBox)item.FindControl("uoTxtBoxCheckIn");
                    vCheckIn = GlobalCode.Field2DateTime(txtCheckIn.Text);

                    TextBox txtTime = (TextBox)item.FindControl("uoTxtBoxTime");
                    vTime = GlobalCode.Field2DateTime(txtTime.Text);

                    TextBox txtConfirmation = (TextBox)item.FindControl("uoTxtBoxConfirmation");
                    vConfirmation = txtConfirmation.Text.ToString();

                    TextBox txtRemarks = (TextBox)item.FindControl("uoTxtBoxRemarks");
                    vRemarks = txtRemarks.Text.ToString();

                    DropDownList HotelStatus = (DropDownList)item.FindControl("uoDropDownListStatus");
                    vHotelStatus = HotelStatus.Text.ToString();

                    CheckBox chkShuttle = (CheckBox)item.FindControl("uoCheckBoxShuttle");
                    vShuttle = chkShuttle.Checked;

                    if (txtCheckIn.Text == "")
                    {
                        txtCheckIn.Text = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
                    }

                    //uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(txtCheckIn.Text).AddDays(GlobalCode.Field2Int(txtDays.Text)).ToShortDateString();

                    Valid = bookingsBLL.BookSeafarer(ViewState["idBigint"].ToString(), ViewState["seqNo"].ToString(),
                            ViewState["trId"].ToString(), ViewState["mReqId"].ToString(), ViewState["recLoc"].ToString(), Source,
                           GlobalCode.Field2Int(ViewState["Contract"].ToString()), GlobalCode.Field2Int(ViewState["VendorId"]),
                           BranchId, RoomTypeId, GlobalCode.Field2DateTime(txtCheckIn.Text),
                           GlobalCode.Field2DateTime(txtTime.Text), GlobalCode.Field2Int(uoHiddenFieldDateTo.Value), txtConfirmation.Text, HotelStatus.SelectedValue,
                           uoHiddenFieldUser.Value, GlobalCode.Field2String(ViewState["SFStatus"]), GlobalCode.Field2Int(ViewState["CityId"].ToString()),
                           GlobalCode.Field2Int(ViewState["CountryId"]), txtRemarks.Text, GlobalCode.Field2String(ViewState["sfStripe"]),
                            ViewState["hotelCity"].ToString(), ViewState["isPort"].ToString(),
                           chkShuttle.Checked,
                           "Book Seafarers", "BookSeafarer", Path.GetFileName(Request.Path), strTimeZone,
                           CommonFunctions.GetDateTimeGMT(currDate), DateTime.Now);
                    return;
                }
            }
        }


        private void BookSeafarer(decimal? contractedRoom,int BranchId,bool isEmergency,int RoomTypeId , int? contractID, int vendorID, out bool Valid)
        {
            DateTime currDate = GlobalCode.Field2DateTime(CommonFunctions.GetCurrentDateTime());
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            Valid = false;
            DateTime? vCheckIn = null;
            DateTime? vTime = null;
            string vConfirmation = null;
            string vRemarks = null;

            string vHotelStatus = null;
            bool? vShuttle = null;
            string userName = uoHiddenFieldUser.Value.ToString();

            foreach (RepeaterItem item in uoRepeaterHotels.Items)
            {
                LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");

                CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");

                if (lnkBtn.CommandArgument.ToString() == BranchId.ToString())
                {
                    TextBox txtCheckIn = (TextBox)item.FindControl("uoTxtBoxCheckIn");
                    vCheckIn = GlobalCode.Field2DateTime(txtCheckIn.Text);

                    TextBox txtTime = (TextBox)item.FindControl("uoTxtBoxTime");
                    vTime = GlobalCode.Field2DateTime(txtTime.Text);

                    TextBox txtConfirmation = (TextBox)item.FindControl("uoTxtBoxConfirmation");
                    vConfirmation = txtConfirmation.Text.ToString();

                    TextBox txtRemarks = (TextBox)item.FindControl("uoTxtBoxRemarks");
                    vRemarks = txtRemarks.Text.ToString();

                    DropDownList HotelStatus = (DropDownList)item.FindControl("uoDropDownListStatus");
                    vHotelStatus = HotelStatus.Text.ToString();

                    CheckBox chkShuttle = (CheckBox)item.FindControl("uoCheckBoxShuttle");
                    vShuttle = chkShuttle.Checked;

                    if (txtCheckIn.Text == "")
                    {
                        txtCheckIn.Text = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
                    }
                    goto next;
                }
            }
        
            next:

            double useRoom = 0.5;
            bool CanAdd = false;
            double RoomCounter = 0.0;

            List<HotelTransactionOverFlowBooking> BookHotel = new List<HotelTransactionOverFlowBooking>();
            foreach (ListViewItem item in uoOverflowList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    HyperLink SeafarerName = (HyperLink)item.FindControl("uoHyperLinkName");
                    Label SeafarerId = (Label)item.FindControl("uoLblSeafarerID");
                    Label Stripes = (Label)item.FindControl("uoLblStripe");
                    Label Status = (Label)item.FindControl("uoLblStatus");
                    HiddenField RoomCount = (HiddenField)item.FindControl("hfRoomType");
                    Label RLoc = (Label)item.FindControl("uoLblRecLoc");
                    HiddenField tReq = (HiddenField)item.FindControl("hfReqId");

                    HiddenField idBigint = (HiddenField)item.FindControl("hfIdBigint");
                    HiddenField seqNo = (HiddenField)item.FindControl("hfSeqNo");
                    HiddenField HotelOverflowID = (HiddenField)item.FindControl("hfHotelOverflowID");

                    if (RoomCount.Value.Equals("2"))
                        useRoom = 0.5;
                    else
                        useRoom = 1.0;

                    Label uoLabelHotelCity = (Label)item.FindControl("uoLabelHotelCity");
                    HiddenField uoHiddenFieldIsByPort = (HiddenField)item.FindControl("uoHiddenFieldIsByPort");

                    RoomCounter += useRoom;

                    if ( GlobalCode.Field2Decimal(RoomCounter) > contractedRoom)
                    {
                        contractID = null;
                    }


                    BookHotel.Add(new HotelTransactionOverFlowBooking
                    {
                        TravelReqId = GlobalCode.Field2Long(tReq.Value),
                        SeafarerId = GlobalCode.Field2Long(SeafarerId.Text),
                        Stripes = GlobalCode.Field2Decimal(Stripes.Text),
                        RecordLocator = GlobalCode.Field2String(RLoc.Text),
                        SfStatus = GlobalCode.Field2String(Status.Text),
                        IDBigint = GlobalCode.Field2Long(idBigint.Value),
                        SeqNo = GlobalCode.Field2Int(seqNo.Value),
                        HotelCity = uoLabelHotelCity.Text.ToString(),
                        IsPort = GlobalCode.Field2Bool(uoHiddenFieldIsByPort.Value),
                        CheckInDate = vCheckIn.Value,
                        Duration = GlobalCode.Field2Int(uoHiddenFieldNoOfNites.Value),
                        ConfirmationNum = vConfirmation,
                        HotelStatus = vHotelStatus,
                        UserId = userName,
                        Remarks = vRemarks,
                        ShuttleBit = vShuttle.Value,
                        BranchId = BranchId,
                        VendorId = GlobalCode.Field2Int(ViewState["VendorId"]),
                        ContractId = contractID,
                        SeafarerCount = GlobalCode.Field2Float(useRoom),
                        RoomType = RoomTypeId,
                        Description = "Manual Book Seafarers from Overflow",
                        Function = "BookSeafarer",
                        FileName = Path.GetFileName(Request.Path),
                        GMTDATE = CommonFunctions.GetDateTimeGMT(currDate),
                        Timezone = strTimeZone,
                        CreateDate = currDate,
                        CheckInTime = vCheckIn.Value,                        
                        IsEmergency = isEmergency,
                        HotelOverflowID = GlobalCode.Field2Long(HotelOverflowID.Value)
                        
                    });
                    CanAdd = true;
                }
            }

            if (CanAdd == true)
            { 
                 Valid = bookingsBLL.BookSeafarer(BookHotel, 0);
            }
            return;
        }


        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: get checked seafarer details for booking
        /// --------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  25/07/2012
        /// Description:    Add ViewState["idBigint"] and ViewState["seqNo"]
        /// </summary>
        protected void GetSeafarerDetails()
        {
            string sfName = "";
            string sfId = "";
            string sfStripe = "";
            string SFStatus = "";
            string recLoc = "";
            string trId = "";
            string mReqId = "";

            string sIdBigint = "";
            string sSeqNo = "";
            int Count = 0;
            
            string HotelCity = "";
            string IsByPort = "";

            foreach (ListViewItem item in uoOverflowList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    Count += 1;
                    HyperLink SeafarerName = (HyperLink)item.FindControl("uoHyperLinkName");
                    Label SeafarerId = (Label)item.FindControl("uoLblSeafarerID");
                    Label Stripes = (Label)item.FindControl("uoLblStripe");
                    Label Status = (Label)item.FindControl("uoLblStatus");
                    Label RLoc = (Label)item.FindControl("uoLblRecLoc");
                    HiddenField tReq = (HiddenField)item.FindControl("hfReqId");
                    
                    HiddenField idBigint = (HiddenField)item.FindControl("hfIdBigint");
                    HiddenField seqNo = (HiddenField)item.FindControl("hfSeqNo");

                    Label uoLabelHotelCity = (Label)item.FindControl("uoLabelHotelCity");
                    HiddenField uoHiddenFieldIsByPort = (HiddenField)item.FindControl("uoHiddenFieldIsByPort");

                    sfName = sfName + SeafarerName.Text + "|";
                    sfId = sfId + SeafarerId.Text + "|";
                    sfStripe = sfStripe + Stripes.Text + "|";
                    SFStatus += Status.Text + '|';
                    recLoc += RLoc.Text + "|";
                    trId += tReq.Value + "|";
                    mReqId += "0" + "|";

                    sIdBigint += idBigint.Value + "|";
                    sSeqNo += seqNo.Value + "|";

                    HotelCity += uoLabelHotelCity.Text + "|";
                    IsByPort += uoHiddenFieldIsByPort.Value + "|";
                }
            }

            ViewState["sfStripe"] = sfStripe.TrimEnd('|');
            ViewState["SFStatus"] = SFStatus.TrimEnd('|');
            ViewState["recLoc"] = recLoc.TrimEnd('|');
            ViewState["trId"] = trId.TrimEnd('|');
            ViewState["mReqId"] = mReqId.TrimEnd('|');

            ViewState["idBigint"] = sIdBigint.TrimEnd('|');
            ViewState["seqNo"] = sSeqNo.TrimEnd('|');

            ViewState["hotelCity"] = HotelCity.TrimEnd('|');
            ViewState["isPort"] = IsByPort.TrimEnd('|');
        }


        public bool SetContractVisibility()
        {
            return Convert.ToBoolean(Eval("withContract").ToString());
        }

        public bool SetEventVisibility()
        {
            return Convert.ToBoolean(Eval("withEvent").ToString());
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: get Exceptions to be exported
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  26/07/2012
        /// Description:    Change HotelTransactionOverflow.OverflowBooking2 to GetOverflowList()
        /// </summary>
        //private void ExportOverlfow( List<OverflowBooking2> list)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        //List<OverflowBooking2> list = new List<OverflowBooking2>();
        //        //list = GetOverflowList();
        //        var e = (from a in list//HotelTransactionOverflow.OverflowBooking2
        //                 select new
        //                 {
        //                     HotelName = a.HotelName,
        //                     E1TravelReqId = a.E1TravelReqId,
        //                     EmployeeId = a.SeafarerId,
        //                     EmployeeName = a.SeafarerName,
        //                     PortName = a.PortName,
        //                     Status = a.SFStatus,
        //                     OnOffDate = string.Format("{0:dd-MMM-yyyy}", a.OnOffDate),
        //                     ArvlDeptDate = string.Format("{0:dd-MMM-yyyy}", a.ArrivalDepartureDatetime),
        //                     ArvlDeptTime = string.Format("{0:hh:mm:ss}", a.ArrivalDepartureDatetime),
        //                     Carrier = GlobalCode.Field2String(a.Carrier),
        //                     FlightNo = GlobalCode.Field2String(a.FlightNo),
        //                     FromCity = GlobalCode.Field2String(a.FromCity),
        //                     ToCity = GlobalCode.Field2String(a.ToCity),
        //                     RankName = GlobalCode.Field2String(a.RankName),
        //                     Stripes = GlobalCode.Field2String(a.Stripes),
        //                     RecordLocator = GlobalCode.Field2String(a.RecordLocator),
        //                     Gender = GlobalCode.Field2String(a.Gender),
        //                     Nationality = GlobalCode.Field2String(a.Nationality),
        //                     SingleDouble = GlobalCode.Field2String((a.RoomType == "Single") ? 1 : 0.5),
        //                     ReasonCode = GlobalCode.Field2String(a.ReasonCode),
        //                     Ship = GlobalCode.Field2String(a.VesselName),
        //                 }).ToList();

        //        dt = getDataTable(e);
        //        CreateFile(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Chralene Remotigue
        /// Date Created: 11/04/2012
        /// Description: convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/OverflowList/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "ExceptionList_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName);
                OpenExcelFile(FileName, strFileName);
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

        public static void CreateExcel(DataTable dtSource, string strFileName)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = 21;
                    //Format the output file for reading easier
                    xtwWriter.Formatting = Formatting.Indented;

                    // <?xml version="1.0"?>
                    xtwWriter.WriteStartDocument();

                    // <?mso-application progid="Excel.Sheet"?>
                    xtwWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

                    // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
                    xtwWriter.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

                    //Write definition of namespace
                    xtwWriter.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
                    xtwWriter.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
                    xtwWriter.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
                    xtwWriter.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

                    // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                    xtwWriter.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

                    // Write document properties
                    xtwWriter.WriteElementString("Author", "Travelmart");
                    xtwWriter.WriteElementString("LastAuthor", Environment.UserName);
                    xtwWriter.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
                    xtwWriter.WriteElementString("Company", "RCCL");
                    xtwWriter.WriteElementString("Version", "1");

                    // </DocumentProperties>
                    xtwWriter.WriteEndElement();

                    // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of workbook
                    xtwWriter.WriteElementString("WindowHeight", "13170");
                    xtwWriter.WriteElementString("WindowWidth", "17580");
                    xtwWriter.WriteElementString("WindowTopX", "120");
                    xtwWriter.WriteElementString("WindowTopY", "60");
                    xtwWriter.WriteElementString("ProtectStructure", "False");
                    xtwWriter.WriteElementString("ProtectWindows", "False");

                    // </ExcelWorkbook>
                    xtwWriter.WriteEndElement();

                    // <Styles>
                    xtwWriter.WriteStartElement("Styles");

                    // <Style ss:ID="Default" ss:Name="Normal">
                    xtwWriter.WriteStartElement("Style");
                    xtwWriter.WriteAttributeString("ss", "ID", null, "Default");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Normal");

                    // <Alignment ss:Vertical="Bottom"/>
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();

                    // Write null on the other properties
                    xtwWriter.WriteElementString("Borders", null);
                    xtwWriter.WriteElementString("Font", null);
                    xtwWriter.WriteElementString("Interior", null);
                    xtwWriter.WriteElementString("NumberFormat", null);
                    xtwWriter.WriteElementString("Protection", null);
                    // </Style>
                    xtwWriter.WriteEndElement();

                    //Style for header
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s62">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s62");
                    xtwWriter.WriteStartElement("Font");
                    // <Font ss:Bold="1"/>
                    xtwWriter.WriteAttributeString("ss", "Bold", null, "1");
                    //end of font
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    //Style for total summary numbers
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s64");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Right");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    // </Styles>
                    xtwWriter.WriteEndElement();

                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Hotel Overflow Bookings");

                    // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                    xtwWriter.WriteStartElement("Table");

                    int iRow = dtSource.Rows.Count + 15;

                    xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                    xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                    xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                    xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                    xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                    //Header
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                    int i = 0;
                    foreach (DataColumn Header in dtSource.Columns)
                    {
                        if (i <= iColCount)
                        {
                            if (i > 0)
                            {
                                xtwWriter.WriteStartElement("Cell");
                                // xxx
                                xtwWriter.WriteStartElement("Data");
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                // Write content of cell
                                xtwWriter.WriteValue(Header.ColumnName);
                                xtwWriter.WriteEndElement();
                                xtwWriter.WriteEndElement();
                            }
                        }
                        i++;
                    }
                    xtwWriter.WriteEndElement();


                    // Run through all rows of data source
                    foreach (DataRow row in dtSource.Rows)
                    {
                        // <Row>
                        xtwWriter.WriteStartElement("Row");

                        i = 0;
                        // Run through all cell of current rows
                        foreach (object cellValue in row.ItemArray)
                        {
                            if (i <= iColCount)
                            {
                                if (i > 0)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    // Write content of cell
                                    xtwWriter.WriteValue(cellValue);

                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
                                }
                            }
                            i++;
                        }
                        // </Row>
                        xtwWriter.WriteEndElement();

                    }

                    // </Table>
                    xtwWriter.WriteEndElement();

                    // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of page
                    xtwWriter.WriteStartElement("PageSetup");
                    xtwWriter.WriteStartElement("Header");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("Footer");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("PageMargins");
                    xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                    xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();

                    // <Selected/>
                    xtwWriter.WriteElementString("Selected", null);

                    // <Panes>
                    xtwWriter.WriteStartElement("Panes");

                    // <Pane>
                    xtwWriter.WriteStartElement("Pane");

                    // Write settings of active field
                    xtwWriter.WriteElementString("Number", "1");
                    xtwWriter.WriteElementString("ActiveRow", "1");
                    xtwWriter.WriteElementString("ActiveCol", "1");

                    // </Pane>
                    xtwWriter.WriteEndElement();

                    // </Panes>
                    xtwWriter.WriteEndElement();

                    // <ProtectObjects>False</ProtectObjects>
                    xtwWriter.WriteElementString("ProtectObjects", "False");

                    // <ProtectScenarios>False</ProtectScenarios>
                    xtwWriter.WriteElementString("ProtectScenarios", "False");

                    // </WorksheetOptions>
                    xtwWriter.WriteEndElement();

                    // </Worksheet>
                    xtwWriter.WriteEndElement();

                    // </Workbook>
                    xtwWriter.WriteEndElement();

                    // Write file on hard disk
                    xtwWriter.Flush();
                    xtwWriter.Close();

                    //FileInfo FileName = new FileInfo(strFileName);
                    //FileStream fs = new FileStream(FileName.FullName, FileMode.Create);                
                    //fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSource != null)
                {
                    dtSource.Dispose();
                }
            }
        }

        /// <summary>
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  12/04/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {

            //Response.Redirect("~/Extract/ExceptionList/" + strFileName, false);

            string strScript = "CloseModal('../Extract/OverflowList/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// Date Created:   06/07/2012
        /// Created By:     Josephine Gad
        /// (description)   Load Port List using session
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");
               
                uoDropDownListPortPerRegion.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListPortPerRegion.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPortPerRegion.DataSource = list;
                    uoDropDownListPortPerRegion.DataTextField = "PORTName";
                    uoDropDownListPortPerRegion.DataValueField = "PORTID";
                    uoDropDownListPortPerRegion.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListPortPerRegion.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListPortPerRegion.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/07/2012
        /// Description:    Use Session to retrieve room block list
        /// </summary>
        /// <param name="CheckinDt"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        private List<RoomBlocks> GetRoomBlocksList(DateTime CheckinDt, int BranchId)
        {
            try
            {
                List<RoomBlocks> list = new List<RoomBlocks>();

                if (Session["HotelBookingsRoomBlocks"] != null)
                {
                    list = (List<RoomBlocks>)Session["HotelBookingsRoomBlocks"];
                }
                else
                {
                    int numOfPeople = GetSeafarersToBook();
                    list = bookingsBLL.getRemainingRooms(CheckinDt,
                        GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value),
                        numOfPeople, GlobalCode.Field2Int(uoHiddenFieldRoomType.Value), BranchId);
                    
                    Session["HotelBookingsRoomBlocks"] = list;
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/07/2012
        /// Description:    Retrieve OverflowBooking2 list using session
        /// --------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  14/Feb/2013
        /// Description:    Add sorting
        /// </summary>
        /// <returns></returns>
        private List<OverflowBooking2> GetOverflowList()
        {
            try
            {
                List<OverflowBooking2> list = new List<OverflowBooking2>();

                if (Session["HotelTransactionOverflowOverflowBooking"] != null)
                {
                    list = (List<OverflowBooking2>)Session["HotelTransactionOverflowOverflowBooking"];
                }
                else
                {
                    int iRegionID = GlobalCode.Field2Int(Session["Region"]);
                    int iPortID = GlobalCode.Field2Int(Session["Port"]);
                    //overflowBLL.LoadHotelOverflowPage(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0, iRegionID, iPortID);

                    list = (List<OverflowBooking2>)Session["HotelTransactionOverflowOverflowBooking"];                   
                }
                if (uoHiddenFieldSortBy.Value != "")
                {
                    if (uoHiddenFieldSortBy.Value == "SortByHotelName")
                    {
                        list = list.OrderBy(a => a.HotelName).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByHotelCity")
                    {
                        list = list.OrderBy(a => a.HotelCity).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByRecLoc")
                    {
                        list = list.OrderBy(a => a.RecordLocator).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByEmpID")
                    {
                        list = list.OrderBy(a => a.SeafarerId).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByName")
                    {
                        list = list.OrderBy(a => a.SeafarerName).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByStatus")
                    {
                        list = list.OrderBy(a => a.SFStatus).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByOnOffDate")
                    {
                        list = list.OrderBy(a => a.OnOffDate).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByCheckDate")
                    {
                        list = list.OrderBy(a => a.CheckInDate).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByGender")
                    {
                        list = list.OrderBy(a => a.Gender).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByRank")
                    {
                        list = list.OrderBy(a => a.RankName).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByStripe")
                    {
                        list = list.OrderBy(a => a.Stripes).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByRoomType")
                    {
                        list = list.OrderBy(a => a.RoomType).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByNationality")
                    {
                        list = list.OrderBy(a => a.Nationality).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByPort")
                    {
                        list = list.OrderBy(a => a.PortName).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByShip")
                    {
                        list = list.OrderBy(a => a.VesselName).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByArrDepDate")
                    {
                        list = list.OrderBy(a => a.ArrivalDepartureDatetime).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByArrDepTime")
                    {
                        list = list.OrderBy(a => a.ArrivalDepartureDatetime).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByFlightNo")
                    {
                        list = list.OrderBy(a => a.FlightNo).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByCarrier")
                    {
                        list = list.OrderBy(a => a.Carrier).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByFromCity")
                    {
                        list = list.OrderBy(a => a.FromCity).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByToCity")
                    {
                        list = list.OrderBy(a => a.ToCity).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByReasonCode")
                    {
                        list = list.OrderBy(a => a.ReasonCode).ToList();
                    }
                    else if (uoHiddenFieldSortBy.Value == "SortByRemarks")
                    {
                        list = list.OrderBy(a => a.BookingRemarks).ToList();
                    }
                }
                Session["HotelTransactionOverflowOverflowBooking"] = list;

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/07/2012
        /// Description:    Retrieve OverflowBooking2 list using session
        /// </summary>
        /// <returns></returns>
        private List<Hotels> GetHotelList()
        {
            try
            {
                List<Hotels> list = new List<Hotels>();

                if (Session["HotelTransactionOverflowHotels"] != null)
                {
                    list = (List<Hotels>)Session["HotelTransactionOverflowHotels"];
                }
                else
                {
                    int iRegionID = GlobalCode.Field2Int(Session["Region"]);
                    int iPortID = GlobalCode.Field2Int(Session["Port"]);
                    //overflowBLL.LoadHotelOverflowPage(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0, iRegionID, iPortID);

                    list = (List<Hotels>)Session["HotelTransactionOverflowHotels"];
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

       
    }
}

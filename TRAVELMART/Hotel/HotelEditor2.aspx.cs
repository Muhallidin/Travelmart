using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Data;
using System.IO;
using System.Threading;
using System.Web.UI.HtmlControls;

namespace TRAVELMART.Hotel
{
    public partial class HotelEditor2 : System.Web.UI.Page
    {
        #region DECLARATIONS
        HotelBookingsBLL bookingsBLL = new HotelBookingsBLL();
        #endregion
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// -----------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// -----------------------------
        /// Modified by:    Josephine Gad
        /// Date MOdified:  06/Jun/2013
        /// Description:    Update room allocations if there is update in room override
        /// </summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        
        private AsyncTaskDelegate _dlgtBranch;
        
        // Create delegate. 
        protected delegate void AsyncTaskDelegate();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GlobalCode.Field2String(Session["strOnOffDate"]) != "")
                {
                    if (GlobalCode.Field2DateTime(Session["strOnOffDate"]) < DateTime.Now)
                    {
                        uoTxtBoxCheckIn.Text = GlobalCode.Field2String(Session["strOnOffDate"]);
                    }
                    else
                    {
                        uoTxtBoxCheckIn.Text = GlobalCode.Field2String(Session["DateFrom"]);
                    }
                }
                else
                {
                    uoTxtBoxCheckIn.Text = GlobalCode.Field2String(Session["DateFrom"]);
                }

                LoadSeafarers();
                //SetDefaults();

                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);

            }
            else
            {
                if (uoHiddenFieldOverride.Value == "1")
                {
                    uoHiddenFieldOverride.Value = "0";
                    BindRoomAlloactions();
                }
            }
        }


        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtBranch = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtBranch.BeginInvoke(cb, extraData);
            return result;
        }
        public void OnEndExceptions(IAsyncResult ar)
        {
            _dlgtBranch.EndInvoke(ar);
            SetDefaults();
        }







        //protected void uoChkAccredited_CheckedChanged(object sender, EventArgs e)
        //{
        //    LoadHotelBranch(!uoChkAccredited.Checked, 0, 1);

        //    if (uoChkAccredited.Checked)
        //    {
        //        uoLblAllocations.Visible = true;
        //        uoRBtnAllocations.Visible = true;
        //        uoBtnCheckRooms.Visible = true;

        //    }
        //    else
        //    {
        //        uoLblAllocations.Visible = false;
        //        uoRBtnAllocations.Visible = false;
        //        uoBtnCheckRooms.Visible = false;
        //        uoListRoomBlocks.Visible = false;
        //    }
        //}
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Change HotelBookings.BranchList to GetBranchList()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoDropDownListBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoLinkButtonEvent.Text = "";
            if (uoDropDownListBranch.SelectedIndex != 0)
            {
                List<BranchList> list = GetBranchList();
                var branchList = (from a in list//HotelBookings.BranchList
                                  where a.BranchId == GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue)
                                  select new
                                  {
                                      CurrencyName = a.Currency,
                                      withShuttle = a.withShuttle,
                                      ContractId = a.ContractId,
                                      VendorId = a.VendorId,
                                      CityId = a.CityId,
                                      CountryId = a.CountryId,
                                      EventCount = a.EventCount
                                  }).ToList();

                uoTxtBoxCurrency.ReadOnly = false;
                uoTxtBoxCurrency.Text = branchList[0].CurrencyName;
                uoChkWithShuttle.Checked = Convert.ToBoolean(branchList[0].withShuttle);
                ViewState["Contract"] = branchList[0].ContractId.ToString();
                ViewState["CityId"] = branchList[0].CityId.ToString();
                uoHiddenFieldCity.Value = branchList[0].CityId.ToString();
                ViewState["VendorId"] = branchList[0].VendorId.ToString();
                ViewState["CountryId"] = branchList[0].CountryId.ToString();
                uoBtnCheckRooms.Enabled = true;
                uoButtonSave.Enabled = true;

                if (branchList[0].EventCount > 0)
                {
                    SetEvent(branchList[0].EventCount);
                }
                else
                {
                    HideLink("$('a').hide();");
                }

                Session["HotelBookingsRoomAllocations"] = null;
                GetRoomAllocationList();
                //bookingsBLL.getRoomBlocks(GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text), GlobalCode.Field2Int(uoTxtBoxDays.Text), uoRepeaterSFName.Items.Count,
                //    GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue), GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue),
                //    GlobalCode.Field2Int(ViewState["Contract"].ToString()));

                getContract();

                if (uoChkAccredited.Checked)
                {
                    uoLblAllocations.Visible = true;
                    uoRBtnAllocations.Visible = true;
                    uoBtnCheckRooms.Visible = true;
                }
                else
                {
                    uoLblAllocations.Visible = false;
                    uoRBtnAllocations.Visible = false;
                    uoBtnCheckRooms.Visible = false;
                }
                CheckRoomAllocations();
            }
            else
            {
                uoLinkButtonContract.Enabled = false;
                uoBtnCheckRooms.Enabled = false;
                uoButtonSave.Enabled = false;
                HideLink("$('a').hide();");
            }
            uoTxtBoxCurrency.ReadOnly = true;
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            if (TravelMartVariable.RoleCrewAssist == GlobalCode.Field2String(Session["UserRole"]))
            {
                if (uoTxtBoxRemarks.Text.Equals(""))
                {
                    AlertMessage("Remark is required");
                    return;
                }
            }

            if (uoHiddenFieldDaysChanged.Value == "true" || uoHiddenFieldRoomChecked.Value == "false")
            {
                //bookingsBLL.getRoomBlocks(GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text), GlobalCode.Field2Int(uoTxtBoxDays.Text), uoRepeaterSFName.Items.Count,
                //    GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue), GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue),
                //    GlobalCode.Field2Int(ViewState["Contract"].ToString()));
                uoHiddenFieldRoomChecked.Value = "true";
            }
            uoHiddenFieldDaysChanged.Value = "false";
            CheckRoomValidity();

        }

        protected void uoBtnCheckRooms_Click(object sender, EventArgs e)
        {
            BindRoomAlloactions();
        }

        private void BindRoomAlloactions()
        {
            uoListRoomBlocks.Visible = true;
            uoHiddenFieldRoomChecked.Value = "true";
            uoHiddenFieldValid.Value = "true";
            Session["HotelBookingsRoomAllocations"] = null;
            GetRoomAllocationList();

            CheckRoomAllocations();
            uoHiddenFieldDaysChanged.Value = "false";
            uoHiddenFieldDateChanged.Value = "0";
            uoHiddenFieldRoomTypeChanged.Value = "0";
        }

        protected string HideEdit()
        {
            HtmlControl EditTH = (HtmlControl)uoListRoomBlocks.FindControl("EditTH");
            if (User.IsInRole(TravelMartVariable.RoleCrewAssist))
            {
                EditTH.Style.Add("display", "none");
                return "hideElement";
            }
            else
            {
                EditTH.Style.Add("display", "display");
                return "";
            }



        }





        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/02/2012
        /// Description: display if there is an event
        /// </summary>
        /// <param name="EventCount"></param>
        protected void SetEvent(int? EventCount)
        {
            uoLinkButtonEvent.Text = EventCount.ToString() + " Event/s.";

            string scriptEventString = "return OpenEventsList('" + uoDropDownListBranch.SelectedValue + "', '" + uoHiddenFieldCity.Value + "', '" + uoTxtBoxCheckIn.Text + "');";
            uoLinkButtonEvent.Attributes.Add("OnClick", scriptEventString);

            HideLink("$('a').show();");
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/02/2012
        /// Description: Set default values
        /// -----------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  27/07/2012
        /// Description:    Remove HotelBookings.SeafarerDetailsList and replace with GetSeafarerList()
        /// </summary>
        protected void SetDefaults()
        {
            Object sender = uoDropDownListBranch;
            EventArgs e = new EventArgs();

            int BranchId = 0;
            bool Accredited = false;
            int HotelTransId = 0;

            if (Request.QueryString["HotelTransId"] != null)
            {
                HotelTransId = GlobalCode.Field2Int(Request.QueryString["HotelTransId"]);
            }

            //HotelBookings.BranchList
            //HotelBookings.RegionList
            //HotelBookings.CountryList
            //HotelBookings.CityList
            //HotelBookings.SeafarerDetailsList           

            bookingsBLL.LoadUserBranchDetails(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text), HotelTransId);

            List<BranchList> branchList = GetBranchList();
            List<RegionList> regionList = GetRegionList();
            List<SeafarerDetails> seafarerList = GetSeafarerList();

            LoadRegion(regionList);

            if (Request.QueryString["HotelTransId"] != null)
            {
                var tempList = (from a in seafarerList//HotelBookings.SeafarerDetailsList
                                select new
                                {
                                    BranchId = a.BranchId,
                                    IsAccredited = a.IsAccredited,
                                    RoomTypeId = a.RoomTypeId,
                                    HotelStatus = a.HotelStatus,
                                    CheckInDate = a.CheckInDate,
                                    CheckInTime = a.CheckInTime,
                                    Duration = a.Duration,
                                    RoomSource = a.RoomSource,
                                    ConfirmationNum = a.ConfirmationNum,
                                    WithShuttle = a.WithShuttle,
                                    Remarks = a.Remarks,
                                }).ToList();

                BranchId = tempList[0].BranchId;
                Accredited = tempList[0].IsAccredited;
                uoDropDownListRoomType.SelectedValue = tempList[0].RoomTypeId.ToString();
                uoDropDownListStatus.SelectedValue = tempList[0].HotelStatus;
                uoTxtBoxCheckIn.Text = string.Format("{0:MM/dd/yyyy}", tempList[0].CheckInDate);
                uoTxtBoxTime.Text = string.Format("{0:HH:mm}", tempList[0].CheckInTime);
                uoTxtBoxDays.Text = tempList[0].Duration.ToString();
                uoRBtnAllocations.SelectedValue = GlobalCode.Field2TinyInt(tempList[0].RoomSource).ToString();
                uoTxtBoxConfirmation.Text = tempList[0].ConfirmationNum;
                uoChkWithShuttle.Checked = tempList[0].WithShuttle;
                uoTxtBoxRemarks.Text = tempList[0].Remarks;
                uoButtonSave.Enabled = true;
                uoBtnCheckRooms.Enabled = true;

                string RegionID = "0";

                LoadHotelBranch(Accredited, BranchId, 0,out RegionID);

                uoDropDownListRegion.SelectedValue = RegionID.ToString();

            }

            else if (Request.QueryString["branchId"] != null)
            {
                BranchId = GlobalCode.Field2Int(Request.QueryString["branchId"]);
                if (BranchId == 0)
                {
                    LoadHotelBranch(!uoChkAccredited.Checked, BranchId, 0);
                }
                else
                {
                    var tempList = (from a in branchList//HotelBookings.BranchList
                                    where a.BranchId == BranchId
                                    select new
                                    {
                                        Accredited = a.isAccredited
                                    }).ToList();

                    Accredited = tempList[0].Accredited;
                    BranchId = GlobalCode.Field2Int(Request.QueryString["branchId"]);
                    LoadHotelBranch(Accredited, BranchId, 0);
                }
            }
            else
            {
                LoadHotelBranch(!uoChkAccredited.Checked, BranchId, 0);
            }

            if (uoChkAccredited.Checked)
            {
                uoLblAllocations.Visible = true;
                uoRBtnAllocations.Visible = true;
                uoBtnCheckRooms.Visible = true;

            }
            else
            {
                uoLblAllocations.Visible = false;
                uoRBtnAllocations.Visible = false;
                uoBtnCheckRooms.Visible = false;
            }

            uoDropDownListBranch_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: load seafarers selected to book
        /// </summary>
        protected void LoadSeafarers()
        {
            string[] sfName = Request.QueryString["sfName"].Split('|');
            string[] sfID = Request.QueryString["sfID"].Split('|');

            uoRepeaterSFName.DataSource = sfName;
            uoRepeaterSFName.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: load user hotel branch
        /// --------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  27/07/2012
        /// Description:    Replace HotelBookings.BranchList with GetBranchList
        /// </summary>
        protected void LoadHotelBranch(bool isAccredited, int BranchId, int LoadType)
        {
            ListItem item = new ListItem("--Select Hotel Branch--", "0");
            List<BranchList> list = GetBranchList();

            var branchList = (from a in list//HotelBookings.BranchList 
                              where a.isAccredited == isAccredited

                              select new
                              {
                                  RegionId = a.RegionId,
                                  CountryId = a.CountryId,
                                  CityId = a.CityId,
                                  BranchId = a.BranchId,
                                  BranchName = a.BranchName,
                              }).ToList();

           

            switch (uoDropDownListRegion.SelectedValue)
            {
                case "0": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.RegionId == GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue)).ToList(); break;
            }

            switch (uoDropDownListCountry.SelectedValue)
            {
                case "0": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.CountryId == GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue)).ToList(); break;
            }

            switch (uoDropDownListCity.SelectedValue)
            {
                case "0": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.CityId == GlobalCode.Field2Int(uoDropDownListCity.SelectedValue)).ToList(); break;
            }

            switch (uoTxtBoxHotelName.Text)
            {
                case "": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.BranchName.ToLower().Contains(uoTxtBoxHotelName.Text.ToLower())).ToList(); break;
            }
            branchList = branchList.Distinct().ToList();
            uoDropDownListBranch.Items.Clear();
            uoDropDownListBranch.Items.Add(item);
            uoDropDownListBranch.DataSource = branchList;
            uoDropDownListBranch.DataTextField = "BranchName";
            uoDropDownListBranch.DataValueField = "BranchId";
            uoDropDownListBranch.DataBind();

            if (LoadType == 0)
            {
                uoDropDownListBranch.SelectedValue = BranchId.ToString();
                uoChkAccredited.Checked = !isAccredited;
            }
        }

        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 13/02/2012
        /// Description: load user hotel branch
        /// --------------------------------------
        /// </summary>
        protected void LoadHotelBranch(bool isAccredited, int BranchId, int LoadType,out string regionID)
        {
            ListItem item = new ListItem("--Select Hotel Branch--", "0");
            List<BranchList> list = GetBranchList();
           
            var branchList = (from a in list//HotelBookings.BranchList 
                              where a.isAccredited == isAccredited 

                              select new 
                              {
                                  RegionId = a.RegionId,
                                  CountryId = a.CountryId,
                                  CityId = a.CityId,
                                  BranchId = a.BranchId,
                                  BranchName = a.BranchName,
                              }).ToList();





            switch (uoDropDownListRegion.SelectedValue)
            {
                case "0": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.RegionId == GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue)).ToList(); break;
            }

            switch (uoDropDownListCountry.SelectedValue)
            {
                case "0": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.CountryId == GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue)).ToList(); break;
            }

            switch (uoDropDownListCity.SelectedValue)
            {
                case "0": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.CityId == GlobalCode.Field2Int(uoDropDownListCity.SelectedValue)).ToList(); break;
            }

            switch (uoTxtBoxHotelName.Text)
            {
                case "": branchList = branchList.ToList(); break;
                default: branchList = branchList.Where(x => x.BranchName.ToLower().Contains(uoTxtBoxHotelName.Text.ToLower())).ToList(); break;
            }
            string myregion = "";
            

            branchList = branchList.Distinct().ToList();
            uoDropDownListBranch.Items.Clear();
            uoDropDownListBranch.Items.Add(item);
            uoDropDownListBranch.DataSource = branchList;
            uoDropDownListBranch.DataTextField = "BranchName";
            uoDropDownListBranch.DataValueField = "BranchId";
            uoDropDownListBranch.DataBind();

            if (LoadType == 0)
            {
                uoDropDownListBranch.SelectedValue = BranchId.ToString();
                uoChkAccredited.Checked = !isAccredited;

                branchList = branchList.Where(x => x.BranchName.ToLower().Contains(uoDropDownListBranch.SelectedItem.Text.ToLower())).ToList();
                if (branchList.Count > 0) myregion = branchList[0].RegionId.ToString();
                
            }

            regionID = myregion;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: check room allocations
        /// </summary>
        protected void CheckRoomAllocations()
        {
            try
            {
                List<SelectedRoomAllocations> rooms = new List<SelectedRoomAllocations>();
                rooms = getRoomAllocations();

                uoListRoomBlocks.Items.Clear();
                uoListRoomBlocks.DataSource = rooms;
                uoListRoomBlocks.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: highlight entry with no remaining room blocks
        /// </summary>
        /// <returns></returns>
        public string Valid()
        {
            bool valid = Convert.ToBoolean(Eval("valid"));

            if (!valid)
            {
                uoHiddenFieldValid.Value = "false";
                return "font-weight: bold; color: #FF0000; padding: 0px; border: thin solid #FF0000";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   13/02/2012
        /// Descriptiion:   CheckRoomValidity
        /// ----------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  06/Feb/2013
        /// Descriptiion:   Add iContractID and pass it to the BookSeafarer function
        /// </summary>
        protected void CheckRoomValidity()
        {

            //List<RemainRoomBlocks> list = new List<RemainRoomBlocks>();
            //list = bookingsBLL.getRemainingRooms(CheckinDt, CheckoutDt, RoomId, BranchId);



            string stripe = Request.QueryString["Stripe"].ToString();

            List<SelectedRoomAllocations> rooms = new List<SelectedRoomAllocations>();
            rooms = getRoomAllocations();
            var roomAllocations = (from a in rooms
                                   where a.remaining >=  a.RoomCount
                                   select new
                                   {
                                       coldate = a.coldate,
                                       colNumOfPeople = a.colNumOfPeople,
                                       remaining = a.remaining,
                                       valid = a.valid,
                                       roomcount = a.RoomCount,
                                       remaincontract = a.remainContrat,
                                       contractID = a.contractId
                                   }).ToList();
             //////if (roomAllocations.Count > 0 && uoChkAccredited.Checked)
            int? contractID = null;
            decimal roomCount = decimal.Parse("0.5");

            if (rooms.Count > 0) contractID = (rooms[0].remainContrat <= 0 ? null : (int?)GlobalCode.Field2Int(rooms[0].contractId));
            if (roomAllocations.Count > 0) roomCount = GlobalCode.Field2Decimal(roomAllocations[0].roomcount);
            

            if (Request.QueryString["HotelTransId"] != null)
            {
                List<SeafarerDetails> CrewDetail = new List<SeafarerDetails>(); 
                CrewDetail = (List<SeafarerDetails>)Session["HotelBookingsSeafarerDetailsList"];
                var newCrewDetail = (from a in CrewDetail
                                     select new
                                     {
                                       BranchID = a.BranchId,
                                       CheckInDate = a.CheckInDate,
                                       RoomType = a.RoomTypeId

                                     }).ToList();



                if (newCrewDetail.Count == 0) return;

                if (newCrewDetail[0].BranchID != GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue)
                    || GlobalCode.Field2DateTime(newCrewDetail[0].CheckInDate).Date != GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text).Date
                    || newCrewDetail[0].RoomType != GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue)
                    )
                {
                    if (GlobalCode.Field2Int(uoTxtBoxDays.Text) != roomAllocations.Count )// && uoChkAccredited.Checked)
                    {
                        AlertMessage("Room blocks not enough to book " + uoRepeaterSFName.Items.Count +
                        " Seafarers to " + uoDropDownListBranch.SelectedItem.Text + ".");
                        return;
                    }
                }

            }
            else
            {
                if (GlobalCode.Field2Int(uoTxtBoxDays.Text) != roomAllocations.Count)// && uoChkAccredited.Checked)
                {
                    AlertMessage("Room blocks not enough to book " + uoRepeaterSFName.Items.Count +
                    " Seafarers to " + uoDropDownListBranch.SelectedItem.Text + ".");
                    return;
                }
            }

            BookSeafarer(contractID, roomCount);

        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Secription: alert user on success and errors
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
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/02/2012
        /// Description: hide link if no events
        /// </summary>
        /// <param name="s"></param>
        private void HideLink(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += s;
            sScript += "$('#<%=uoLinkButtonContract.ClientID %>').show();";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoDropDownListBranch, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: Book Seafarer
        /// -------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  06/Feb/2013
        /// Description:    Add parameter iContractID and get the correct source it from Contract, Override or Emergency
        /// </summary>
        private void BookSeafarer(int? iContractID,decimal roomcount )
        {
            try
            {
                DateTime currDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                string sMessage = "";
                int Source = 1;
                bool isNew = true;
                bool Valid = true;

                if (uoRBtnAllocations.Visible)
                {
                    Source = GlobalCode.Field2TinyInt(uoRBtnAllocations.SelectedValue);
                }


                ViewState["Contract"] = 0;
                if (Source == 0)
                {
                    ViewState["Contract"] = iContractID;
                }

                if (Request.QueryString["HotelTransId"] == null)
                {
                    Valid = bookingsBLL.BookSeafarer(
                            Request.QueryString["idBgint"],
                            Request.QueryString["seqNo"],
                            Request.QueryString["trId"],
                            Request.QueryString["mReqId"],
                            Request.QueryString["RecLoc"],
                            Source,
                            iContractID,
                            GlobalCode.Field2Int(ViewState["VendorId"].ToString()),
                            GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue),
                            GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue),
                            GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text),
                            GlobalCode.Field2DateTime(uoTxtBoxTime.Text),
                            GlobalCode.Field2Int(uoTxtBoxDays.Text),
                            uoTxtBoxConfirmation.Text,
                            uoDropDownListStatus.SelectedValue,
                            GlobalCode.Field2String(Session["UserName"]),
                            Request.QueryString["Status"],
                            GlobalCode.Field2Int(ViewState["CityId"].ToString()),
                            GlobalCode.Field2Int(ViewState["CountryId"]),
                            uoTxtBoxRemarks.Text,
                            Request.QueryString["Stripe"],
                            "",
                            "",
                            uoChkWithShuttle.Checked,
                            "Approve Hotel Bookings",
                            "BookSeafarer",
                            Path.GetFileName(Request.Path),
                            strTimeZone,
                            CommonFunctions.GetDateTimeGMT(currDate),
                            DateTime.Now,
                            roomcount,
                            uoRBtnAllocations.SelectedValue.Equals("2") ? true : false
                        );
                    isNew = true;
                    sMessage = "Hotel booking successfully added";
                }
                else
                {

                    string Mydate = string.Empty;
                    int i = 0;
                    bool CanUpdate = true;
                    List<RemainRoomBlocksWithHotelID> rooms = new List<RemainRoomBlocksWithHotelID>();
                    rooms = CheckRoomAllocationsForUpdate(GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text)
                            , GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text).AddDays(GlobalCode.Field2Int(uoTxtBoxDays.Text))
                            , GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue)
                            , GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue)
                            , GlobalCode.Field2Int(Request.QueryString["HotelTransId"]));
                    if (rooms.Count > 0)
                    {
                        for (i = 0; i < Convert.ToInt32(rooms.Count); i++)
                        {
                            if (roomcount > rooms[i].RemainVacantRoom)
                            {
                                CanUpdate = false;
                                Mydate = Mydate + "  " + GlobalCode.Field2Date(rooms[i].Date);
                            }
                        }
                    }

                    if (CanUpdate == false)
                    {
                        AlertMessage("Cannot update... There is no available room for the date of " + Mydate);
                        return;
                    }


                    bookingsBLL.UpdateSeafarerBookings(
                            Source,
                            iContractID,
                            GlobalCode.Field2Int(ViewState["VendorId"].ToString()),
                            GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue),
                            GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue),
                            GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text),
                            GlobalCode.Field2DateTime(uoTxtBoxTime.Text),
                            GlobalCode.Field2Int(uoTxtBoxDays.Text),
                            uoTxtBoxConfirmation.Text,
                            uoDropDownListStatus.SelectedValue,
                            GlobalCode.Field2String(Session["UserName"]),
                            GlobalCode.Field2Int(ViewState["CityId"].ToString()),
                            GlobalCode.Field2Int(ViewState["CountryId"]),
                            uoTxtBoxRemarks.Text,
                            uoChkWithShuttle.Checked,
                            "Update Hotel Bookings",
                            "BookSeafarer",
                            Path.GetFileName(Request.Path),
                            strTimeZone,
                            CommonFunctions.GetDateTimeGMT(currDate),
                            DateTime.Now,
                            Request.QueryString["Stripe"],
                            uoRBtnAllocations.SelectedValue.Equals("2") ? true : false,
                            GlobalCode.Field2Int(Request.QueryString["HotelTransId"])
                        );
                    isNew = true;
                    sMessage = "Hotel booking successfully updated.";
                }

                if (Valid)
                {
                    //Thread threadSendEmail;

                    //threadSendEmail = new Thread(delegate()
                    //{
                    //SendEmail(isNew);
                    //});

                    //threadSendEmail.IsBackground = true;
                    //threadSendEmail.Start();

                    ClosePage(sMessage);

                }
                else
                {
                    AlertMessage("Seafarer cannot be booked on the same date.");
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 13/02/2012
        /// Description: check room allocations
        /// </summary>
        private List<RemainRoomBlocksWithHotelID> CheckRoomAllocationsForUpdate(DateTime DateFrom, DateTime DateTo, int BranchId, int RoomType, long TransHotelID)
        {
            try
            {
                return bookingsBLL.getRemainingRooms(DateFrom, DateTo, BranchId, RoomType, TransHotelID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/02/2012
        /// Description: Send async email after approval
        /// </summary>
        private void SendEmail(bool isNew)
        {
            string SeafarerDetails = "";
            string Message = "";
            string sBody = "";
            string sSubject = "Seafarer bookings";
            string[] sfName = Request.QueryString["sfName"].Split('|');
            string[] sfID = Request.QueryString["sfID"].Split('|');

            for (int i = 0; i < sfName.Length; i++)
            {
                SeafarerDetails += "<b>" + sfName[i] + "</b> with E1 ID <b>" + sfID[i] + "</b><br/>";
            }

            if (isNew)
            {
                Message = "Booking of Seafarers: <br/>" + SeafarerDetails + "has been approved. <br/><br/>";
            }
            else
            {
                Message = "Booking of Seafarers: <br/>" + SeafarerDetails + "has been updated. <br/><br/>";
            }

            Message += "Hotel Branch: " + uoDropDownListBranch.SelectedItem.Text + "<br/>";
            Message += "Check-In Date: " + uoTxtBoxCheckIn.Text + "<br/><br/>";
            Message += "Thank You.<br/><br/> <i>Auto generated email</i>";

            sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
            sBody += "Dear " + TravelMartVariable.RoleHotelVendor + ", <br/><br/> " + Message;
            sBody += "</TD></TR></TABLE>";

            CommonFunctions cFunctions = new CommonFunctions();

            //string[] sTo = new string[SendTo.Count()];
            //for(int i=0; i<sTo.Count; i++)
            //cFunctions.SendApprovalEmail("", sSubject, sBody, TravelMartVariable.RoleHotelVendor);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage(string s)
        {

            string sScript = "<script language='JavaScript'>";
            sScript += "var msg = '" + s + "';";
            sScript += "alert( msg );";
            
            sScript += " window.opener.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopEditor\").val(\"1\"); ";
            sScript += " window.opener.RefreshPageFromPopup();  ";
            
            sScript += " self.close(); ";
            //sScript += "function closeWindow(){";
            //sScript += "closeWindow();";
            //sScript += "}";
            sScript += "</script>";
            //ScriptManager.re
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 17/02/2012
        /// Description: get Room allocations
        /// ---------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Change HotelBookings.SeafarerDetailsList to seafarerList
        ///                 Change HotelBookings.RoomAllocations to roomList
        /// ---------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  06/02/2013
        /// Description:    Add ContractID and Source in Return list
        /// ---------------------------------------
        /// </summary>
        /// <returns></returns>
        protected List<SelectedRoomAllocations> getRoomAllocations()
        {
            DateTime CheckInDate = GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text);
            DateTime CheckOutDate = CheckInDate.AddDays(GlobalCode.Field2Int(uoTxtBoxDays.Text));

            List<SeafarerDetails> seafarerList = GetSeafarerList();

            decimal addRoom = 0;

            if (Request.QueryString["HotelTransId"] != null)
            {
                if (seafarerList[0].RoomSource == GlobalCode.Field2TinyInt(uoRBtnAllocations.SelectedValue) &&
                    seafarerList[0].RoomTypeId == GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue) &&
                    seafarerList[0].BranchId == GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue))
                {
                    //CheckInDate = seafarerList[0].CheckInDate; ;
                    ////CheckOutDate = CheckInDate.AddDays(seafarerList[0].Duration);
                    //CheckOutDate = CheckInDate.AddDays(seafarerList[0].Duration);
                    if (seafarerList[0].RoomTypeId == 2)
                    {
                        addRoom += GlobalCode.Field2Decimal("0.5");
                    }
                    else
                    {
                        addRoom += GlobalCode.Field2Decimal("1");
                    }
                }
            }

            List<RoomAllocations> roomList = GetRoomAllocationList();
            var roomAllocations = (from a in roomList
                                   select new SelectedRoomAllocations
                                   {
                                       coldate = a.coldate,
                                       colNumOfPeople = a.colNumOfPeople,
                                       remaining = a.remaining,
                                       valid = a.valid,
                                       contractId = a.contractId,
                                       sourceAllocation = a.sourceAllocation,
                                       BranchId = a.BranchId,
                                       RoomTypeId = a.RoomTypeId,
                                       ReservedOverride = a.ReservedOverride,
                                       RoomCount = a.RoomCount,
                                       EmergencyVacant = a.EmergencyVacant,
                                       remainContrat = a.remainContrat

                                   }).ToList();
            return roomAllocations;
        
        }

        protected void getContract()
        {
            uoLinkButtonContract.Enabled = false;
            if (ViewState["Contract"] != null)
            {
                if (ViewState["Contract"].ToString() != "0" || ViewState["Contract"].ToString() != "")
                {
                    uoLinkButtonContract.Enabled = true;
                    string scriptContractString = "return OpenContract('" + uoDropDownListBranch.SelectedValue + "');";
                    uoLinkButtonContract.Attributes.Add("OnClick", scriptContractString);
                }
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Add regionList parameter
        ///                 Remove HotelBookings.RegionList
        /// </summary>
        protected void LoadRegion(List<RegionList> regionList)
        {
            ListItem item = new ListItem("--Select Region--", "0");
            uoDropDownListRegion.Items.Clear();
            uoDropDownListRegion.Items.Add(item);
            uoDropDownListRegion.DataSource = regionList;//HotelBookings.RegionList;
            uoDropDownListRegion.DataTextField = "RegionName";
            uoDropDownListRegion.DataValueField = "RegionId";
            uoDropDownListRegion.DataBind();
        }
        #endregion

        protected void uoButtonFilter_Click(object sender, EventArgs e)
        {
            uoButtonClear.Enabled = true;
            LoadHotelBranch(!uoChkAccredited.Checked, 0, 1);

            if (uoChkAccredited.Checked)
            {
                uoLblAllocations.Visible = true;
                uoRBtnAllocations.Visible = true;
                uoBtnCheckRooms.Visible = true;
                uoListRoomBlocks.Visible = true;
            }
            else
            {
                uoLblAllocations.Visible = false;
                uoRBtnAllocations.Visible = false;
                uoBtnCheckRooms.Visible = false;
                uoListRoomBlocks.Visible = false;
            }
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadCountry();
            LoadHotelBranch(false, 0, GlobalCode.Field2Int( uoDropDownListRegion.SelectedValue));

        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Change HotelBookings.CountryList to GetCountryList()
        /// </summary>
        protected void LoadCountry()
        {
            ListItem item = new ListItem("--Select Country--", "0");
            uoDropDownListCountry.Items.Clear();
            uoDropDownListCountry.Items.Add(item);

            List<CountryList> list = GetCountryList();
            var Country = (from a in list//HotelBookings.CountryList
                           where a.RegionId == GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue)
                           orderby a.CountryName
                           select new
                           {
                               CountryId = a.CountryId,
                               CountryName = a.CountryName,
                           }).ToList();

            uoDropDownListCountry.DataSource = Country;
            uoDropDownListCountry.DataTextField = "CountryName";
            uoDropDownListCountry.DataValueField = "CountryId";
            uoDropDownListCountry.DataBind();
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Change HotelBookings.CityList to GetCityList()
        /// </summary>
        protected void LoadCity()
        {
            ListItem item = new ListItem("--Select City--", "0");
            uoDropDownListCity.Items.Clear();
            uoDropDownListCity.Items.Add(item);

            List<CityList> list = GetCityList();

            var City = (from a in list
                        where a.CountryId == GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue)
                            //&& a.CityName.ToLower().Contains(uoTxtBoxCityName.Text.ToLower())
                           && a.CityName.ToLower().StartsWith(uoTxtBoxCityName.Text.ToLower())
                        orderby a.CityName
                        select new
                        {
                            CityId = a.CityId,
                            CityName = a.CityName,
                        }).ToList();

            uoDropDownListCity.DataSource = City;
            uoDropDownListCity.DataTextField = "CityName";
            uoDropDownListCity.DataValueField = "CityId";
            uoDropDownListCity.DataBind();
        }


        protected void uoBtnSearchCity_Click(object sender, EventArgs e)
        {
            LoadCity();
        }
        protected void uoButtonClear_Click(object sender, EventArgs e)
        {
            uoDropDownListRegion.SelectedIndex = 0;
            ListItem item = new ListItem("--Select Country--", "0");
            uoDropDownListCountry.Items.Clear();
            uoDropDownListCountry.Items.Add(item);
            item = new ListItem("--Select City--", "0");
            uoDropDownListCity.Items.Clear();
            uoDropDownListCity.Items.Add(item);
            uoTxtBoxCityName.Text = "";
            uoChkAccredited.Checked = true;
            uoTxtBoxHotelName.Text = "";
            uoLblAllocations.Visible = true;
            uoRBtnAllocations.Visible = true;
            uoBtnCheckRooms.Visible = true;

            LoadHotelBranch(!uoChkAccredited.Checked, 0, 1);

        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Retrieve Branch list using session
        /// </summary>
        /// <returns></returns>
        private List<BranchList> GetBranchList()
        {
            List<BranchList> list = new List<BranchList>();
            if (Session["HotelBookingsBranchList"] != null)
            {
                list = (List<BranchList>)Session["HotelBookingsBranchList"];

            }
            return list;
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Retrieve Region list using session
        /// </summary>
        /// <returns></returns>
        private List<RegionList> GetRegionList()
        {
            List<RegionList> list = new List<RegionList>();
            if (Session["HotelBookingsRegionList"] != null)
            {
                list = (List<RegionList>)Session["HotelBookingsRegionList"];

            }
            return list;
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Retrieve Country list using session
        /// </summary>
        /// <returns></returns>
        private List<CountryList> GetCountryList()
        {
            List<CountryList> list = new List<CountryList>();
            if (Session["HotelBookingsCountryList"] != null)
            {
                list = (List<CountryList>)Session["HotelBookingsCountryList"];

            }
            return list;
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Retrieve City list using session
        /// </summary>
        /// <returns></returns>
        private List<CityList> GetCityList()
        {
            List<CityList> list = new List<CityList>();
            if (Session["HotelBookingsCityList"] != null)
            {
                list = (List<CityList>)Session["HotelBookingsCityList"];
            }
            return list;
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Retrieve Seafarer list using session
        /// </summary>
        /// <returns></returns>
        private List<SeafarerDetails> GetSeafarerList()
        {
            List<SeafarerDetails> list = new List<SeafarerDetails>();
            if (Session["HotelBookingsSeafarerDetailsList"] != null)
            {
                list = (List<SeafarerDetails>)Session["HotelBookingsSeafarerDetailsList"];
            }
            return list;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   27/07/2012
        /// Description:    Get Room Allocation list using session
        /// </summary>
        /// <returns></returns>
        private List<RoomAllocations> GetRoomAllocationList()
        {
            List<RoomAllocations> list = new List<RoomAllocations>();
            if (Session["HotelBookingsRoomAllocations"] != null)
            {
                list = (List<RoomAllocations>)Session["HotelBookingsRoomAllocations"];
            }
            else
            {
                list = bookingsBLL.getRoomBlocks(GlobalCode.Field2DateTime(uoTxtBoxCheckIn.Text), GlobalCode.Field2Int(uoTxtBoxDays.Text), uoRepeaterSFName.Items.Count,
                    GlobalCode.Field2Int(uoDropDownListRoomType.SelectedValue), GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue),
                    0, GlobalCode.Field2Decimal( Request.QueryString["Stripe"].ToString()));
            }
            Session["HotelBookingsRoomAllocations"] = list;
            return list;
        }

    }
}

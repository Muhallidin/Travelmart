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

namespace TRAVELMART
{
    public partial class HotelRequest : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //uoTextNites.Text = uoHiddenFieldNoOfNites.Value;
            if (!IsPostBack)
            {
                uoHiddenFieldHotelRequestID.Value = Request.QueryString["hrID"].ToString();
                uoHiddenFieldTravelRequestID.Value = Request.QueryString["trID"].ToString();
                uoHiddenFieldHotelRequestApp.Value = Request.QueryString["App"].ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                if (GlobalCode.Field2String(Request.QueryString["dt"]) != "")
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                    uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                }
                else
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
                }
                //LoadDefaults(0);
                GetGender();
                GetVessel();
                GetRank();
                GetCurrency();
                GetSFInfo();

                if (uoHiddenFieldHotelRequestID.Value != "0" && uoHiddenFieldHotelRequestID.Value != "")
                    GetSFCompanion();
                if (uoDropDownListHotel.SelectedIndex > 0)
                    GetHotelRoomAmount();

                tblConfirm.Visible = (uoHiddenFieldHotelRequestApp.Value == "2") ? true : false;
            }

            //fire room availability check
            //if (Request["__EVENTARGUMENT"] == "uoTextBoxCheckinDate_TextChanged")
            //{
            //    BindRoom();
            //}
            uoCheckBoxAddCompanion.Enabled = (uoHiddenFieldHotelRequestID.Value != "0" && uoHiddenFieldHotelRequestID.Value != "");
            //only show to authorized approver
            
            uoListViewCompanion.DataSource = null;
            uoListViewCompanion.DataBind();
            //if crew exist
            if (uoTextBoxRequestNo.Text.Length > 0)
            {
                divSFNotExist.Visible = false;
                divSFExist.Visible = true;
            }
            else
            {
                divSFNotExist.Visible = true;
                divSFExist.Visible = false;
            }
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            uoHiddenFieldRegion.Value = uoDropDownListRegion.SelectedValue;
            uoHiddenFieldPort.Value = "0";

            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = "";
            Session.Remove("Port");
            LoadDefaults(1);
            GetAirport();
            GetHotelFilter();
        }
        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPort.SelectedValue;
            uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;
//            LoadDefaults(1);
            GetAirport();
            GetHotelFilter();
        }
        protected void uoDropDownListAirport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Airport"] = uoDropDownListAirport.SelectedValue;
            uoHiddenFieldAirPort.Value = uoDropDownListAirport.SelectedValue;
//            LoadDefaults(1);           
            GetHotelFilter();
        }
        protected void uoDropDownListRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCostCenter();
        }
        /// <summary>
        /// Date Modified:   21/May/2013
        /// Modified by:     Josephine Gad
        /// Description:     Add dtNow sDateNow
        /// </summary>      
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {

            string sDate = DateTime.Now.ToShortDateString();
            DateTime dt = Convert.ToDateTime(sDate);
            
            if (Convert.ToDateTime(uoTextBoxCheckoutDate.Text) < dt || Convert.ToDateTime(uoTextBoxCheckinDate.Text) < dt)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Past date is invalid!');</script>", false);
                uoTextBoxCheckinDate.Focus();
            }
            else if (Convert.ToDateTime(uoTextBoxCheckoutDate.Text) < Convert.ToDateTime(uoTextBoxCheckinDate.Text))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Invalid Date range!');</script>", false);
                uoTextBoxCheckinDate.Focus();
            }
            else
            {
                SaveRequest();
                tblConfirm.Visible = true;
            }
        }
        protected void uoButtonAddComp_Click(object sender, EventArgs e)
        {
            AddHotelRequestCompanion();
        }
        protected void uoListViewCompanionList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uoListViewCompanionList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        protected void uoListViewCompanionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if (e.CommandName == "edit")
            {
                string[] arg = new string[5];
                char[] splitter = { ':' };
                arg = e.CommandArgument.ToString().Split(splitter);

                uoHiddenFieldHotelRequestDetailID.Value = arg[0].ToString();
                uoTextBoxCompLastname.Text = arg[2].ToString();
                uoTextBoxCompFirstname.Text = arg[3].ToString();
                uoTextBoxCompRelationship.Text = arg[4].ToString();
                uoDropDownListCompGender.SelectedValue = arg[5].ToString();
                uoCheckBoxAddCompanion.Checked = true;
            }
            if (e.CommandName == "remove")
            {
                uoHiddenFieldHotelRequestDetailID.Value = e.CommandArgument.ToString();
                //RemoveRequestCompanion();
            }
        }

        //protected void uoDropdownRoomOccupancy_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //GetHotelRoomAmount();
        //}
        //protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    uoHiddenFieldRoomAmount.Value = "0";
        //    if (uoDropDownListHotel.SelectedIndex > 0)
        //        uoHiddenFieldContractID.Value =
        //            BLL.ContractBLL.GetApprovedVendorHotelBranchContractByBranchID(Convert.ToInt32(uoDropDownListHotel.SelectedValue)).ToString();
        //    //BindRoom();
        //    GetHotelRoomAmount();
        //}

        protected void uoButtonSubmit_Click(object sender, EventArgs e)
        {
            if (uoTextBoxRemarks.Text.ToString() != string.Empty)
            {
                SubmitRequest();
            }  
            
        }

        #endregion
        #region Functions

        /// <summary>
        /// Date Created:   03/25/2013
        /// Created by:     Marco Abejar
        /// Description:    Load defaults
        /// </summary>
        public void LoadDefaults(short LoadType)
        {
            if (LoadType == 0)
            {
                BindRegionList();
            }
            BindPortList();
        }
        private void BindRegionList()
        {
            List<RegionList> list = new List<RegionList>();
            try
            {
                if (Session["HotelDashboardDTO_RegionList"] != null)
                {
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
                uoDropDownListRegion.Items.Insert(0, new ListItem("--SELECT REGION--", "0"));

                string sRegion = GlobalCode.Field2String(Session["Region"]);
                if (sRegion != "")
                {
                    if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
                    {
                        uoDropDownListRegion.SelectedValue = sRegion;
                    }
                }
                uoHiddenFieldRegion.Value = uoDropDownListRegion.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:   03/25/2013
        /// Created by:     Marco Abejar
        /// Description:    For Filtering based on port per region 
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

                uoDropDownListPort.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListPort.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPort.DataSource = list;
                    uoDropDownListPort.DataTextField = "PORTName";
                    uoDropDownListPort.DataValueField = "PORTID";
                    uoDropDownListPort.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListPort.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                        }
                    }
                }
                uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row            
            //string GroupTextString = "Hotel Branch";
            string GroupValueString = "HotelBranchName";
            string currentDataFieldValue = Eval(GroupValueString).ToString();

            string sURl = "HotelDashboard3.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName");

            string sEvent = "";
            if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
            {
                //sEvent = "<td class=\"tdEvent\"><a class=\"rightAligned\" title=\"View Event(s)\" href=\"#\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"View Event(s)\" border=\"0\"/></a></td>";
                sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"View Event(s)\" href=\"#\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"View Event(s)\" border=\"0\"/></a>";
            }
            else
            {
                //sEvent = "<td class=\"tdEvent\"><a class=\"rightAligned\" title=\"No Event(s)\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"No Event(s)\" border=\"0\"/></a></td>";
                //sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"No Event(s)\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"No Event(s)\" border=\"0\"/></a>";
                sEvent = "";
            }

            string sContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"View Contract\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"View Contract\" border=\"0\"/></a> " + sEvent + "</td>";
            //string sNoContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"No Contract\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"No Contract\" border=\"0\"/></a> " + sEvent + "</td>";
            string sNoContract = "<td class=\"tdEvent\">" + sEvent + "</td>";

            string sReturn;
            if (Eval("IsWithContract").ToString() == "True")
            {
                sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sContract + "</tr>", currentDataFieldValue);
                //return string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sContract + "" + sEvent + "</tr>", currentDataFieldValue);
            }
            else
            {
                sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sNoContract + "</tr>", currentDataFieldValue);
                //return string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sNoContract + "" + sEvent + "</tr>", currentDataFieldValue);
            }
            return sReturn;
        }

        private void BindRoom()
        {
            try
            {
                DataTable dt = HotelBLL.GetAvailHotelRoomType(uoDropDownListHotel.SelectedValue, uoTextBoxCheckinDate.Text.Trim());

                ListItem item = new ListItem("--SELECT ROOM--", "0");
                uoDropdownRoomOccupancy.Items.Add(item);
                uoDropdownRoomOccupancy.Items.Clear();
                uoDropdownRoomOccupancy.DataSource = dt;
                uoDropdownRoomOccupancy.DataTextField = "RoomType";
                uoDropdownRoomOccupancy.DataValueField = "RoomTypeID";
                uoDropdownRoomOccupancy.DataBind();

                if (dt.Rows.Count < 1)
                {
                    uoDropdownRoomOccupancy.Items.Clear();
                    item = new ListItem("NO AVAILABLE ROOM", "0");
                    uoDropdownRoomOccupancy.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// ----------------------------------------------
        /// Modified By:    Marco Abejar
        /// Date Modified:  26/03/2013
        /// Description:    Get seafarer's info
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  30/May/2013
        /// Description:    Add Shuttle, LunchDinner, and Tax
        /// ----------------------------------------------
        /// </summary>

        private void GetSFInfo()
        {

            IDataReader dtSFInfo = null;
            try
            {
                dtSFInfo = GetSfInfoDataTable();
                if (dtSFInfo.Read())
                {
                    ClearDropdown();

                    uoHiddenFieldHotelRequestID.Value = dtSFInfo["RequestId"].ToString();
                    uoHiddenFieldSFStatus.Value = dtSFInfo["STATUS"].ToString();
                    uoTextBoxE1ID.Text = dtSFInfo["colSeafarerIdInt"].ToString();
                    uoTextBoxRequestNo.Text = dtSFInfo["RequestNo"].ToString();
                    uoTextBoxLastname.Text = dtSFInfo["Lastname"].ToString();
                    uoTextBoxFirstname.Text = dtSFInfo["Firstname"].ToString();
                    string st = dtSFInfo["Gender"].ToString();
                    if (dtSFInfo["Gender"].ToString().Length > 0)
                    {
                        uoDropDownListGender.Items.FindByValue(dtSFInfo["Gender"].ToString()).Selected = true;
                    }

                    if (dtSFInfo["Vessel"].ToString().Length > 0)
                    {
                        uoDropDownListVessel.SelectedIndex = -1;
                        uoDropDownListVessel.Items.FindByValue(dtSFInfo["Vessel"].ToString()).Selected = true;
                    }
                    if (dtSFInfo["Rank"].ToString().Length > 0)
                    {
                        uoDropDownListRank.Items.FindByValue(dtSFInfo["Rank"].ToString()).Selected = true;
                        GetCostCenter();
                    }
                    uoTextBoxCostCenter.Text = dtSFInfo["CostCenter"].ToString();
                    uoTextBoxCheckinDate.Text = dtSFInfo["Checkin"].ToString();
                    uoTextBoxCheckoutDate.Text = dtSFInfo["Checkout"].ToString();
                    uoCheckboxBreakfast.Checked = Convert.ToBoolean(dtSFInfo["Breakfast"].ToString());
                    uoCheckboxLunch.Checked = Convert.ToBoolean(dtSFInfo["Lunch"].ToString());
                    uoCheckboxDinner.Checked = Convert.ToBoolean(dtSFInfo["Dinner"].ToString());
                    uoCheckBoxLunchDinner.Checked = GlobalCode.Field2Bool(dtSFInfo["LUNCHDINNER"]);
                    uoCheckBoxIsWithShuttle.Checked = GlobalCode.Field2Bool(dtSFInfo["SHUTTLE"]);


                    uoTextNites.Text = dtSFInfo["Nites"].ToString();
                    uoHiddenFieldNoOfNites.Value = uoTextNites.Text;
                    uoTextBoxRemarks.Text = dtSFInfo["Comment"].ToString();
                    if (dtSFInfo["CheckInTime"] != null && dtSFInfo["CheckInTime"].ToString() != "")
                    {
                        DateTime CheckInTime = DateTime.Parse(dtSFInfo["CheckInTime"].ToString());
                        string CInTime = String.Format("{0:HH:mm}", CheckInTime);
                        uoTxtBoxTimeIn.Text = CInTime;
                    }
                    if (dtSFInfo["CheckOutTime"] != null && dtSFInfo["CheckOutTime"].ToString() != "")
                    {
                        DateTime CheckOutTime = DateTime.Parse(dtSFInfo["CheckOutTime"].ToString());
                        string COutTime = String.Format("{0:HH:mm}", CheckOutTime);
                        uoTxtBoxTimeOut.Text = COutTime;
                    }
                    if (dtSFInfo["RoomAmount"] != null && dtSFInfo["RoomAmount"].ToString() != "")
                    {
                        Decimal fAmount = GlobalCode.Field2Decimal(dtSFInfo["RoomAmount"]);
                        uoTextBoxAmount.Text = fAmount.ToString("0.00");
                    }
                    uoHiddenFieldRoomAmount.Value = dtSFInfo["RoomAmount"].ToString();

                    uoTextBoxTaxPercent.Text =  GlobalCode.Field2Double(dtSFInfo["colRoomRateTaxPercentage"]).ToString();
                    uoCheckContractBoxTaxInclusive.Checked = GlobalCode.Field2Bool(dtSFInfo["colRoomRateTaxInclusive"].ToString());
                                        
                    if (dtSFInfo["Currency"].ToString().Length > 0)
                    {
                        uoDropDownListCurrency.SelectedIndex = -1;
                        uoDropDownListCurrency.Items.FindByValue(dtSFInfo["Currency"].ToString()).Selected = true;
                    }
                    BindRegionList();
                    if (dtSFInfo["Region"].ToString().Length > 0)
                    {
                        uoDropDownListRegion.SelectedIndex = -1;
                        uoDropDownListRegion.Items.FindByValue(dtSFInfo["Region"].ToString()).Selected = true;
                    }
                    BindPortList();
                    if (dtSFInfo["Port"].ToString().Length > 0 && dtSFInfo["Region"].ToString().Length > 0)
                    {
                        if (uoDropDownListPort.Items.FindByValue(dtSFInfo["Port"].ToString()) != null)
                        {
                            uoDropDownListPort.SelectedValue = dtSFInfo["Port"].ToString();
                        }
                    }
                    GetAirport();
                    if (dtSFInfo["AIRPORT"].ToString().Length > 0 && dtSFInfo["Region"].ToString().Length > 0)
                    {
                        if (uoDropDownListAirport.Items.FindByValue(dtSFInfo["AIRPORT"].ToString()) != null)
                        {
                            uoDropDownListAirport.SelectedValue = dtSFInfo["AIRPORT"].ToString();
                        }
                    } 
                    GetHotelFilter();
                    if (dtSFInfo["Hotel"].ToString() != "0")
                    {
                        //uoDropDownListHotel.SelectedIndex = -1;
                        //GetHotelFilter();
                        if (uoDropDownListHotel.Items.FindByValue(dtSFInfo["Hotel"].ToString()) != null)
                        {
                            uoDropDownListHotel.SelectedValue = dtSFInfo["Hotel"].ToString();
                        }
                        uoLabelMessage.Visible = false;
                    }
                    else
                    {
                        uoLabelMessage.Visible = true;
                    }
                    if (dtSFInfo["RoomType"].ToString().Length > 0)
                    {
                        //BindRoom();
                        uoDropdownRoomOccupancy.Items.FindByValue(dtSFInfo["RoomType"].ToString()).Selected = true;
                    }
                    uoHiddenFieldContractID.Value = GlobalCode.Field2String(dtSFInfo["colContractIdInt"]);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dtSFInfo != null)
                {
                    dtSFInfo.Close();                    
                    dtSFInfo.Dispose();
                }
            }
        }
        /// ----------------------------------------------
        /// Modified By:    Marco Abejar
        /// Date Modified:  26/03/2013
        /// Description:    Get seafarer's info
        /// ----------------------------------------------
        /// </summary>

        private void GetSFCompanion()
        {
            DataTable dtCompanion = new DataTable();
            try
            {
                uoListViewCompanion.DataSource = null;
                uoListViewCompanion.DataBind();
                uoListViewCompanion.Visible = true;
                dtCompanion = GetSfCompanionDataTable();
                uoListViewCompanionList.DataSource = dtCompanion;
                uoListViewCompanionList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtCompanion != null)
                {
                    dtCompanion.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                MasterfileBLL bll = new MasterfileBLL();
                VesselDataTable = bll.SailMasterMaintenanceVessel("", true, 200, 0);
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Ship--", "0");
                uoDropDownListVessel.Items.Add(item);
                uoDropDownListVessel.DataSource = VesselDataTable;
                uoDropDownListVessel.DataTextField = "colVesselNameVarchar";
                uoDropDownListVessel.DataValueField = "colVesselIdInt";
                uoDropDownListVessel.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VesselDataTable != null)
                {
                    VesselDataTable.Dispose();
                }
            }
        }

        private void ClearDropdown()
        {
            uoDropDownListRegion.ClearSelection();
            uoDropDownListPort.ClearSelection();
            uoDropDownListVessel.ClearSelection();
            uoDropdownRoomOccupancy.ClearSelection();
            uoDropDownListHotel.ClearSelection();
            uoDropDownListRank.ClearSelection();
            uoDropDownListGender.ClearSelection();
            uoDropDownListCompGender.ClearSelection();
        }
        /// <summary>
        /// Date Created:   27/103/2013
        /// Created By:     Marco Abejar
        /// (description)   Get list of gender
        /// </summary>
        private void GetGender()
        {
            DataTable dt = null;
            try
            {
                dt = MasterfileBLL.GetReference("Gender");
                ListItem item = new ListItem("--Select Gender--", "0");
                uoDropDownListGender.Items.Clear();
                uoDropDownListGender.Items.Add(item);
                uoDropDownListGender.DataSource = dt;
                uoDropDownListGender.DataTextField = "RefName";
                uoDropDownListGender.DataValueField = "RefID";
                uoDropDownListGender.DataBind();

                uoDropDownListCompGender.Items.Clear();
                uoDropDownListCompGender.Items.Add(item);
                uoDropDownListCompGender.DataSource = dt;
                uoDropDownListCompGender.DataTextField = "RefName";
                uoDropDownListCompGender.DataValueField = "RefID";
                uoDropDownListCompGender.DataBind();

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
        /// <summary>
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of rank
        /// </summary>
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetRankByVessel(uoDropDownListVessel.SelectedValue);
                ListItem item = new ListItem("--Select Rank--", "0");
                uoDropDownListRank.Items.Clear();
                uoDropDownListRank.Items.Add(item);
                uoDropDownListRank.DataSource = dt;
                uoDropDownListRank.DataTextField = "RankName";
                uoDropDownListRank.DataValueField = "RankID";
                uoDropDownListRank.DataBind();
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
        private IDataReader GetSfInfoDataTable()
        {
            IDataReader dtSFInfo = null;
            try
            {
                dtSFInfo = SeafarerBLL.SeafarerGetRequestDetails(Request.QueryString["sfID"].ToString(),
                   uoHiddenFieldTravelRequestID.Value, uoHiddenFieldHotelRequestID.Value, uoHiddenFieldHotelRequestApp.Value);
                return dtSFInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable GetSfCompanionDataTable()
        {
            DataTable dtSFInfo = null;
            try
            {
                dtSFInfo = SeafarerBLL.SeafarerGetCompanionDetails(uoHiddenFieldHotelRequestID.Value);
                return dtSFInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  02/10/2012
        /// Description:    Add option "Select ALL Hotel" ,"-1" if there is selected Region
        /// ----------------------------------------------
        /// </summary>
        private void GetHotelFilter()
        {
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, uoDropDownListRegion.SelectedValue,
                    uoDropDownListPort.SelectedValue, "0", uoDropDownListAirport.SelectedValue);

                int iRowCount = list.Count;
                if (iRowCount > 0)
                {
                    uoDropDownListHotel.Items.Clear();
                    uoDropDownListHotel.DataSource = list;
                    uoDropDownListHotel.DataTextField = "HotelNameString";
                    uoDropDownListHotel.DataValueField = "HotelIDString";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));

                    //if (uoDropDownListHotel.Items.FindByValue("-1") != null)
                    //{
                    //    uoDropDownListHotel.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
                    //}

                    //RemoveDuplicateItems(uoDropDownListHotel);
                    uoDropDownListHotel.Enabled = true;
                }
                else
                {
                    uoDropDownListHotel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveDuplicateItems(DropDownList ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ddl.SelectedIndex = i;
                string str = ddl.SelectedItem.ToString();
                for (int counter = i + 1; counter < ddl.Items.Count; counter++)
                {
                    ddl.SelectedIndex = counter;
                    string compareStr = ddl.SelectedItem.ToString();
                    if (str == compareStr)
                    {
                        ddl.Items.RemoveAt(counter);
                        counter = counter - 1;
                    }
                }
            }
            ddl.SelectedIndex = 0;
        }
        /// <summary>
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of rank
        /// </summary>
        private void GetCostCenter()
        {
            try
            {
                string[] arg = new string[2];
                char[] splitter = { '-' };
                arg = SeafarerTravelBLL.GetCostCenterByRank(uoDropDownListRank.SelectedValue).Split(splitter);
                uoTextBoxCostCenter.Text = arg[1].ToString();
                uoHiddenFieldCostCenterID.Value = arg[0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   27/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Save Hotel Request
        /// ---------------------------------------------------------------------------
        /// Date Modified:  30/May/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add Shuttle, Tax, Lunch Dinner, Airport and fields for Audit Trail
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void SaveRequest()
        {
            try
            {
                string strLogDescription;
                if (uoHiddenFieldHotelRequestApp.Value == "1")
                {
                    strLogDescription = "Add Hotel Request";
                }
                else
                {
                    strLogDescription = "Edit Hotel Request";                
                }
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                string sHRID = SeafarerBLL.SeafarerSaveRequest(uoTextBoxRequestNo.Text
                                , uoTextBoxE1ID.Text
                                , uoTextBoxLastname.Text
                                , uoTextBoxFirstname.Text
                                , uoDropDownListGender.SelectedValue
                                , uoDropDownListRegion.SelectedValue
                                , uoDropDownListPort.SelectedValue
                                , uoDropDownListAirport.SelectedValue
                                , uoDropDownListHotel.SelectedValue
                                , uoTextBoxCheckinDate.Text
                                , uoTextBoxCheckoutDate.Text
                                , uoHiddenFieldNoOfNites.Value
                                , uoDropdownRoomOccupancy.SelectedValue
                                , uoCheckboxBreakfast.Checked
                                , uoCheckboxLunch.Checked
                                , uoCheckboxDinner.Checked
                                , uoCheckBoxLunchDinner.Checked
                                , uoCheckBoxIsWithShuttle.Checked

                                , uoDropDownListRank.SelectedValue
                                , uoDropDownListVessel.SelectedValue
                                , uoHiddenFieldCostCenterID.Value
                                , uoTextBoxRemarks.Text
                                , uoHiddenFieldSFStatus.Value
                                , uoTxtBoxTimeIn.Text
                                , uoTxtBoxTimeOut.Text
                                , uoTextBoxAmount.Text
                                , uoCheckContractBoxTaxInclusive.Checked
                                , uoTextBoxTaxPercent.Text

                                , uoHiddenFieldUser.Value
                                , uoHiddenFieldTravelRequestID.Value
                                , uoDropDownListCurrency.SelectedValue

                                , strLogDescription, "SaveRequest"
                                , Path.GetFileName(Request.UrlReferrer.AbsolutePath)
                                , CommonFunctions.GetDateTimeGMT(dateNow), dateNow
                            );

                uoHiddenFieldHotelRequestID.Value = (sHRID.Length > 0)
                        ? sHRID : uoHiddenFieldHotelRequestID.Value;

                if (uoListViewCompanionList.Items.Count > 0)
                    OpenParent();
                else
                {
                    uoCheckBoxAddCompanion.Checked = true;
                    uoTextBoxCompLastname.Focus();
                    uoHiddenFieldHotelRequestApp.Value = "1";
                    GetSFInfo();
                }
                uoListViewCompanion.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Successful", "alert('Updates have been saved successfully.');", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }
        /// <summary>
        /// Date Created:   04/02/2013
        /// Created By:     Marco Abejar
        /// (description)   Submit Hotel Request
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void SubmitRequest()
        {
            try
            {
                SeafarerBLL.SeafarerSubmitRequest(uoHiddenFieldHotelRequestID.Value, 
                        uoHiddenFieldUser.Value, txtContactName.Text.Trim(),
                        txtContactNo.Text.Trim(), "Submit Hotel Request", "SubmitRequest", Path.GetFileName(Request.Path));
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Successful", "alert('Request has been booked successfully.');", true);
                OpenParent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }
        /// <summary>
        /// Date Created:   27/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Save Hotel Request Companion
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void AddHotelRequestCompanion()
        {
            try
            {
                SeafarerBLL.SeafarerSaveComapnionRequest(
                    uoHiddenFieldHotelRequestDetailID.Value,
                    uoHiddenFieldHotelRequestID.Value,
                    uoTextBoxCompLastname.Text,
                    uoTextBoxCompFirstname.Text,
                    uoTextBoxCompRelationship.Text,
                    uoDropDownListCompGender.SelectedValue,
                    uoHiddenFieldUser.Value
                    );
                uoCheckBoxAddCompanion.Checked = false;
                uoTextBoxCompFirstname.Text = "";
                uoTextBoxCompLastname.Text = "";
                uoTextBoxCompRelationship.Text = "";
                uoDropDownListCompGender.SelectedIndex = 0;
                uoHiddenFieldHotelRequestDetailID.Value = "0";
                GetSFCompanion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }
        /// <summary>
        /// Date Created:   01/04/2013
        /// Created By:     Marco Abejar
        /// (description)   Remove Companion
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void RemoveRequestCompanion()
        {
            try
            {
                //SeafarerBLL.RemoveRequestCompanion( uoHiddenFieldHotelRequestDetailID.Value);
                GetSFCompanion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }
        /// <summary>
        /// Date Created:   04/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Get Room Amount
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetHotelRoomAmount()
        {
            List<HotelRoomBlocksDTO> listHotelRoomBlocks = null;
            try
            {
                string strRoomType = (uoDropdownRoomOccupancy.SelectedValue == "Single") ? "1" : "2";
                listHotelRoomBlocks = HotelBLL.GetHotelRoomOverrideByBranch(uoDropDownListHotel.SelectedValue,
                    strRoomType, uoHiddenFieldDate.Value);
                if (listHotelRoomBlocks.Count > 0)
                {
                    if (uoHiddenFieldRoomAmount.Value == "0")
                    {
                        Decimal fAmount = GlobalCode.Field2Decimal(listHotelRoomBlocks[0].OverrideRate);//GlobalCode.Field2Decimal(dr["EmergencyRate"].ToString());
                        uoTextBoxAmount.Text = fAmount.ToString("0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (listHotelRoomBlocks != null)
                {
                    listHotelRoomBlocks = null;
                }
            }
        }
        private void GetCurrency()
        {
            DataTable dt = null;
            try
            {
                dt = ContractBLL.CurrencyLoad();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCurrency.DataSource = dt;
                    uoDropDownListCurrency.DataTextField = "colCurrencyNameVarchar";
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
        private void OpenParent()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupHotel\").val(\"1\"); ";

            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);

        }
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        /// <summary>
        /// Date Created:   28/May/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport by Seaport
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetAirport()
        {
            List<Airport> list = new List<Airport>();
            list = MaintenanceViewBLL.GetAirportListByRegionBySeaport(uoHiddenFieldUser.Value,
               "", GlobalCode.Field2Int(uoDropDownListPort.SelectedValue), GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue));
            uoDropDownListAirport.Items.Clear();
            uoDropDownListAirport.Items.Insert(0, new ListItem("--Select Airport--", "0"));
            uoDropDownListAirport.DataSource = list;
            uoDropDownListAirport.DataTextField = "AirportCodeName";
            uoDropDownListAirport.DataValueField = "AirportID";
            
            uoDropDownListAirport.DataBind();
            if (list.Count == 1)
            {
                if (uoDropDownListAirport.Items.FindByValue(GlobalCode.Field2String(list[0].AirportID)) != null)
                {
                    uoDropDownListAirport.SelectedValue = GlobalCode.Field2String(list[0].AirportID);
                }
            }

            uoHiddenFieldAirPort.Value = uoDropDownListAirport.SelectedValue;
        }
        #endregion
    }
}

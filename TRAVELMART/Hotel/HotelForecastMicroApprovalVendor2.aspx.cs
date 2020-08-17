using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Web.Security;

namespace TRAVELMART.Hotel
{
    public partial class HotelForecastMicroApprovalVendor2 : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   26/Jan/2015
        /// Description:    Load page connected to Micro Forecasting Report with Approval
        /// --------------------------------------------------------------      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); //gelo     
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                HtmlTable uoTblDate = (HtmlTable)Master.FindControl("uoTblDate");
                uoTblDate.Style.Add("display", "none");
                //uoTblDate.Visible = false;


                SetDefaultValues();
                //GetNationality();
                //GetGender();
                //GetRank();
                //GetVessel();
                //GetHotelFilter();

                //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                //{
                //    uoTRVessel.Visible = false;
                //}
                //else
                //{
                //    uoTRVessel.Visible = true;
                //}

                //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                //    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                //    uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist)
                //{
                //    //uoButtonLock.Visible = true;
                //}
                //else
                //{
                //    //uoButtonLock.Visible = false;
                //}
                DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                //TimeSpan timeDiff = dDateFrom - dDateCurrent;
                //int dDays = timeDiff.Days;

                string sHotel = GlobalCode.Field2String(Session["Hotel"]);

                string sBranchID = GlobalCode.Field2Int(Session["UserBranchID"]).ToString();
                string sBranchName = GlobalCode.Field2String(Session["BranchName"]);

                uoLabelHotelVendor.Text = sBranchName;
                uoHiddenFieldHotelID.Value = sBranchID;

                uoHiddenFieldFrom.Value = dDateCurrent.ToString("");
                uoHiddenFieldTo.Value = uoHiddenFieldFrom.Value;

                BindDayTo();
                LoadTravelDetails();
                BindContractDetails();

                //uoListViewHeader.DataSource = null;
                //uoListViewHeader.DataBind();
            }
            else
            {
                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

                if (uoHiddenFieldPopupCalendar.Value == "1" || sChangeDate == "1")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                    DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                    //TimeSpan timeDiff = dDateFrom - dDateCurrent;
                    //int dDays = timeDiff.Days;

                    GetSFHotelTravelDetails();
                }
            }
        }
        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Region"] = uoDropDownListRegion.SelectedValue;
        //    Session["Hotel"] = "";
        //    Session["HotelNameToSearch"] = "";
        //    Session.Remove("Port"); // remove the current selected Port 05/07/2012
        //    LoadDefaults(1);
        //    Session["Forecast_Hotel"] = null;
        //    GetHotelFilter();
        //}

        //protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
        //    //LoadDefaults(1);
        //    Session["Forecast_Hotel"] = null;
        //    GetHotelFilter();
        //}
        //protected void uoButtonView_Click(object sender, EventArgs e)
        //{
        //    uoHiddenFieldLoadType.Value = "1";
        //    //Session["Hotel"] = uoDropDownListHotel.SelectedValue;
        //    GetSFHotelTravelDetails();
        //}
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            ApproveHotelForecast();
        }
        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            //ButtonLockSettings();
            BindNoOfDays();
            BindCurrency();
        }
        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sBranchName"] = uoLabelHotelVendor.Text;

            e.InputParameters["sDateFrom"] = uoHiddenFieldFrom.Value;
            e.InputParameters["sDateTo"] = uoHiddenFieldTo.Value;

            e.InputParameters["sVesselCode"] = "";
            e.InputParameters["sPortID"] = "0";

            e.InputParameters["sUser"] = uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldRole.Value;

            e.InputParameters["LoadType"] = uoHiddenFieldLoadType.Value;
            e.InputParameters["bShowAll"] = uoCheckBoxShowAll.Checked;
        }
        protected void uoDropDownListDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NoOfDays_Forecast_Vendor"] = uoDropDownListDays.SelectedValue;
            uoHiddenFieldNoOfDays.Value = uoDropDownListDays.SelectedValue;

            uoHiddenFieldTo.Value = GlobalCode.Field2DateTime(
                uoHiddenFieldFrom.Value).AddDays(GlobalCode.Field2Int(uoDropDownListDays.SelectedValue)).ToString();
            uoHiddenFieldLoadType.Value = "1";
            GetSFHotelTravelDetails();
        }
        protected void uoCheckBoxShowAll_CheckedChanged(object sender, EventArgs e)
        {
            Session["NoOfDays_Forecast_Vendor"] = uoDropDownListDays.SelectedValue;
            uoHiddenFieldNoOfDays.Value = uoDropDownListDays.SelectedValue;

            uoHiddenFieldTo.Value = GlobalCode.Field2DateTime(
                uoHiddenFieldFrom.Value).AddDays(GlobalCode.Field2Int(uoDropDownListDays.SelectedValue)).ToString();
            uoHiddenFieldLoadType.Value = "1";
            GetSFHotelTravelDetails();
        }
        protected void uolistviewHotelInfo_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DropDownList listDropDownListAction = (DropDownList)e.Item.FindControl("uoDropDownListAction");
                TextBox listTextBoxDBL = (TextBox)e.Item.FindControl("uoTextBoxDBL");
                TextBox listTextBoxSGL = (TextBox)e.Item.FindControl("uoTextBoxSGL");
                TextBox listTextBoxRoomToDropDBL = (TextBox)e.Item.FindControl("uoTextBoxRoomToDropDBL");
                TextBox listTextBoxRoomToDropSGL = (TextBox)e.Item.FindControl("uoTextBoxRoomToDropSGL");

                int iDbl = GlobalCode.Field2Int(listTextBoxDBL.Text);
                int iSgl = GlobalCode.Field2Int(listTextBoxSGL.Text);

                int iDBLToDrop = GlobalCode.Field2Int(listTextBoxRoomToDropDBL.Text);
                int iSGLToDrop = GlobalCode.Field2Int(listTextBoxRoomToDropSGL.Text);

                if (iDbl > 0 || iSgl > 0 || iDBLToDrop > 0 || iSGLToDrop > 0)
                {
                    listDropDownListAction.Enabled = true;
                    if (iDbl > 0 || iSgl > 0)
                    {
                        BindDDLAction(false, listDropDownListAction);
                    }
                    else if (iDBLToDrop > 0 || iSGLToDrop > 0)
                    {

                        BindDDLAction(true, listDropDownListAction);
                    }
                }
                else
                {
                    listDropDownListAction.Enabled = false;
                }
            }
        }
        
        #endregion

        #region Function
        
        protected void InitializeValues()
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            
            string sUserName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }
            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            Session["strPrevPage"] = Request.RawUrl;

            //uoListViewHeader.DataSource = null;
            //uoListViewHeader.DataBind();
        }
        /// <summary>
        /// Modified By:    Josephine gad
        /// Date Modified:  03/10/2012
        /// Description:    User BindRegionList to bind Region List
        /// </summary>
        //public void LoadDefaults(short LoadType)
        //{
        //    if (LoadType == 0)
        //    {
        //        BindRegionList();
        //    }
        //    BindPortList();
        //}
        ///// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
        /// </summary>
        //private void BindRegionList()
        //{
        //    List<RegionList> list = new List<RegionList>();
        //    try
        //    {
        //        if (Session["HotelDashboardDTO_RegionList"] != null)
        //        {
        //            //Session["HotelDashboardDTO_RegionList"] = HotelDashboardDTO.RegionList;
        //            list = (List<RegionList>)Session["HotelDashboardDTO_RegionList"];
        //        }
        //        else
        //        {
        //            list = CountryBLL.RegionListByUser(uoHiddenFieldUser.Value);
        //            Session["HotelDashboardDTO_RegionList"] = list;
        //        }
        //        if (list.Count > 0)
        //        {
        //            uoDropDownListRegion.Items.Clear();
        //            uoDropDownListRegion.DataSource = list;
        //            uoDropDownListRegion.DataTextField = "RegionName";
        //            uoDropDownListRegion.DataValueField = "RegionId";
        //            uoDropDownListRegion.DataBind();
        //        }
        //        uoDropDownListRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));

        //        string sRegion = GlobalCode.Field2String(Session["Region"]);
        //        if (sRegion != "")
        //        {
        //            if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
        //            {
        //                uoDropDownListRegion.SelectedValue = sRegion;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// Date Created:   05/07/2012
        /// Created by:     Jefferson Bermundo
        /// Description:    For Filtering based on port per region
        /// ------------------------
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        //private void BindPortList()
        //{
        //    List<PortList> list = new List<PortList>();
        //    try
        //    {
        //        list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

        //        uoDropDownListPortPerRegion.Items.Clear();
        //        ListItem item = new ListItem("--SELECT PORT--", "0");
        //        uoDropDownListPortPerRegion.Items.Add(item);
        //        if (list.Count > 0)
        //        {
        //            uoDropDownListPortPerRegion.DataSource = list;
        //            uoDropDownListPortPerRegion.DataTextField = "PORTName";
        //            uoDropDownListPortPerRegion.DataValueField = "PORTID";
        //            uoDropDownListPortPerRegion.DataBind();

        //            if (GlobalCode.Field2String(Session["Port"]) != "")
        //            {
        //                if (uoDropDownListPortPerRegion.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
        //                {
        //                    uoDropDownListPortPerRegion.SelectedValue = GlobalCode.Field2String(Session["Port"]);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void SetDefaultValues()
        {
            if (GlobalCode.Field2String(Session["Region"]) == "")
            {
                Session["strPendingFilter"] = "0";
                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["City"] = "0";
                Session["Port"] = "0";
                Session["Hotel"] = "0";
                Session["Vehicle"] = "0";
            }
            //ManifestBLL BLL = new ManifestBLL();
            //BLL.ForecastGetFilters(uoHiddenFieldUser.Value,
            //    GlobalCode.Field2Int(Session["Region"]),
            //    GlobalCode.Field2Int(Session["Port"]), 0);
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  09/07/2012
        /// Description:    Change DataTable to List
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  02/10/2012
        /// Description:    Add option "Select ALL Hotel" ,"-1" if there is selected Region
        /// ----------------------------------------------
        /// </summary>
        //private void GetHotelFilter()
        //{
        //    List<HotelDTO> list = new List<HotelDTO>();
        //    try
        //    {
        //        int iRowCount = 0;
        //        if (Session["Forecast_Hotel"] != null)
        //        {
        //            list = (List<HotelDTO>)Session["Forecast_Hotel"];
        //        }
        //        else
        //        {
        //            list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, Session["Region"].ToString(),
        //                Session["Port"] == null ? "0" : Session["Port"].ToString(), "0", "0");
        //        }
        //        Session["Forecast_Hotel"] = list;


        //        iRowCount = list.Count;
        //        if (iRowCount == 1)
        //        {
        //            Session["Hotel"] = list[0].HotelIDString;//dt.Rows[0]["BranchID"].ToString();
        //        }

        //        if (iRowCount > 0)
        //        {
        //            uoDropDownListHotel.Items.Clear();
        //            uoDropDownListHotel.DataSource = list;
        //            uoDropDownListHotel.DataTextField = "HotelNameString";
        //            uoDropDownListHotel.DataValueField = "HotelIDString";
        //            uoDropDownListHotel.DataBind();
        //            uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
        //            uoDropDownListHotel.SelectedValue = "0";

        //            //if (GlobalCode.Field2Int(Session["Region"]) > 0)
        //            //{
        //            //    if (uoDropDownListHotel.Items.FindByValue("-1") == null)
        //            //    {
        //            //        uoDropDownListHotel.Items.Insert(1, new ListItem("--Select ALL Hotel--", "-1"));
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    if (uoDropDownListHotel.Items.FindByValue("-1") != null)
        //            //    {
        //            //        uoDropDownListHotel.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
        //            //    }
        //            //}
        //            RemoveDuplicateItems(uoDropDownListHotel);
        //            uoDropDownListHotel.Enabled = true;

        //            if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
        //            {
        //                uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
        //            }
        //            LoadTravelDetails();
        //        }
        //        else
        //        {
        //            uoDropDownListHotel.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
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
        private void GetSFHotelTravelDetails()
        {
            try
            {
                uolistviewHotelInfo.DataSource = null;
                uolistviewHotelInfo.DataSourceID = "uoObjectDataSourceManifest";
                uolistviewHotelInfo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadTravelDetails()
        {
            //Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            //Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;
            ////if (uoDropDownListHotel.SelectedValue == "0")
            ////{
            ////    uoBtnExportList.Enabled = false;
            ////}
            ////else
            ////{
            ////    uoBtnExportList.Enabled = true;
            ////}

            //if (uoDropDownListHotel.SelectedValue == "-1")
            //{
            //    uoDropDownListPortPerRegion.SelectedValue = "0";
            //    Session["Port"] = "0";
            //    uoDropDownListPortPerRegion.Enabled = false;
            //}
            //else
            //{
            //    uoDropDownListPortPerRegion.Enabled = true;
            //}

            //uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
            //DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            //DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
            //TimeSpan timeDiff = dDateFrom - dDateCurrent;
            //int dDays = timeDiff.Days;

            GetSFHotelTravelDetails();
            //Session.Remove("TentativeManifestCalendarDashboard");
        }
        /// <summary>
        /// Date Created:   22/02/2011
        /// Created By:     Josephine Gad
        /// (description)   disable lock button if past dates and if there is no data
        /// -------------------------------------------------------
        /// Date Modified:  23/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   disable lock button  if already locked
        /// -------------------------------------------------------
        /// Date Modified:  02/10/2012
        /// Modified By:    Josephine Gad
        /// (description)   disable lock button  if "ALL Hotel" is selected
        /// -------------------------------------------------------
        /// </summary>
        //private void ButtonLockSettings()
        //{
        //    if (uolistviewHotelInfo.Items.Count > 0)
        //    {
        //        //uoBtnExportList.Enabled = true;
        //        if (uoDropDownListHotel.SelectedValue == "-1")
        //        {
        //            //uoButtonLock.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        //uoBtnExportList.Enabled = false;
        //    }

        //    string sDate = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
        //    if (GlobalCode.Field2DateTime((sDate)) < DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")))
        //    {
        //        //uoButtonLock.Enabled = false;
        //    }
        //    //bool IsAlreadyLocked = HotelManifestBLL.IsHotelHasLockedManifest(sDate, uoDropDownListHotel.SelectedValue, uoDropDownListHours.SelectedValue);
        //    //if (IsAlreadyLocked)
        //    //{
        //    //    uoButtonLock.Enabled = false;
        //    //}
        //}
        /// <summary>
        /// Date Created:   05/May/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save the Additional Room to Hotel Room Blocks 
        /// -------------------------------------------------------
        /// </summary>
        private void ApproveHotelForecast()
        {
            DataTable dt = null;
            try
            {

                HotelForecastBLL BLL = new HotelForecastBLL();
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                DateTime dateOnly = GlobalCode.Field2DateTime(dateNow.ToString("MM/dd/yyyy"));

                Label listLabelDate;

                HiddenField listHiddenFieldToAddDBL;
                HiddenField listHiddenFieldToAddSGL;

                TextBox listTextBoxDBL;
                TextBox listTextBoxSGL;

                TextBox listTextBoxRoomToDropDBL;
                TextBox listTextBoxRoomToDropSGL;

                TextBox listTextBoxRatePerDayMoneyDBL;
                TextBox listTextBoxRatePerDayMoneySGL;
                TextBox listTextBoxRoomRateTaxPercentage;
                CheckBox listCheckBoxTaxInclusive;
                
                DropDownList listDropDownListAction;

                int iCount = uolistviewHotelInfo.Items.Count;
                int iHotelID = GlobalCode.Field2Int(uoHiddenFieldHotelID.Value);

                DataColumn colBranchIDInt = new DataColumn("colBranchIDInt", typeof(Int64));
                DataColumn colDate = new DataColumn("colDate", typeof(DateTime));
                DataColumn colToAddDBL = new DataColumn("colToAddDBL", typeof(int));
                DataColumn colToAddSGL = new DataColumn("colToAddSGL", typeof(int));
                DataColumn colAction = new DataColumn("colAction", typeof(string));

                DataColumn colRoomToDropDBL = new DataColumn("colRoomToDropDBL", typeof(int));
                DataColumn colRoomToDropSGL = new DataColumn("colRoomToDropSGL", typeof(int));

                DataColumn colRatePerDayMoneySGL = new DataColumn("colRatePerDayMoneySGL", typeof(float));
                DataColumn colRatePerDayMoneyDBL = new DataColumn("colRatePerDayMoneyDBL", typeof(float));
                DataColumn colCurrencyIDInt = new DataColumn("colCurrencyIDInt", typeof(int));
                DataColumn colRoomRateTaxPercentage = new DataColumn("colRoomRateTaxPercentage", typeof(float));
                DataColumn colRoomRateIsTaxInclusive = new DataColumn("colRoomRateIsTaxInclusive", typeof(float));
                
                dt = new DataTable();
                dt.Columns.Add(colBranchIDInt);
                dt.Columns.Add(colDate);
                dt.Columns.Add(colToAddDBL);
                dt.Columns.Add(colToAddSGL);
                dt.Columns.Add(colAction);

                dt.Columns.Add(colRoomToDropDBL);
                dt.Columns.Add(colRoomToDropSGL);

                dt.Columns.Add(colRatePerDayMoneySGL);
                dt.Columns.Add(colRatePerDayMoneyDBL);
                dt.Columns.Add(colCurrencyIDInt);
                dt.Columns.Add(colRoomRateTaxPercentage);
                dt.Columns.Add(colRoomRateIsTaxInclusive);

                DataRow r;
                DateTime rowDate;

                //CheckBox lvuoCheckBoxSelect;

                for (int i = 0; i < iCount; i++)
                {
                    listDropDownListAction = (DropDownList)uolistviewHotelInfo.Items[i].FindControl("uoDropDownListAction");
                    //lvuoCheckBoxSelect = (CheckBox)uolistviewHotelInfo.Items[i].FindControl("uoCheckBoxSelect");
                    //if (lvuoCheckBoxSelect.Checked)
                    if (listDropDownListAction.SelectedValue == "Accept" 
                        || listDropDownListAction.SelectedValue == "Decline" 
                        || listDropDownListAction.SelectedValue == "Edit"
                        || listDropDownListAction.SelectedValue == "Drop")
                    {
                        listLabelDate = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLabelDate");
                        rowDate = GlobalCode.Field2DateTime(listLabelDate.Text);

                        if (rowDate >= dateOnly)
                        {
                            listHiddenFieldToAddDBL = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldToAddDBL");
                            listHiddenFieldToAddSGL = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldToAddSGL");

                            listTextBoxDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxDBL");
                            listTextBoxSGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxSGL");

                            listTextBoxRoomToDropDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomToDropDBL");
                            listTextBoxRoomToDropSGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomToDropSGL");

                        listTextBoxRatePerDayMoneyDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRatePerDayMoneyDBL");
                listTextBoxRatePerDayMoneySGL = (TextBox )uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRatePerDayMoneySGL");

               listTextBoxRoomRateTaxPercentage=(TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomRateTaxPercentage"); ;
               listCheckBoxTaxInclusive=(CheckBox)uolistviewHotelInfo.Items[i].FindControl("uoCheckBoxTaxInclusive");

                            int iToAddDBLOld = GlobalCode.Field2Int(listHiddenFieldToAddDBL.Value);
                            int iToAddSGLOld = GlobalCode.Field2Int(listHiddenFieldToAddSGL.Value);

                            int iToAddDBLNew = GlobalCode.Field2Int(listTextBoxDBL.Text);
                            int iToAddSGLNew = GlobalCode.Field2Int(listTextBoxSGL.Text);

                            int iToDropDBL = GlobalCode.Field2Int(listTextBoxRoomToDropDBL.Text);
                            int iToDropSGL = GlobalCode.Field2Int(listTextBoxRoomToDropSGL.Text);

                            float fRatePerDayMoneyDBL = GlobalCode.Field2Float(listTextBoxRatePerDayMoneyDBL.Text);
                            float fRatePerDayMoneySGL =  GlobalCode.Field2Float(listTextBoxRatePerDayMoneySGL.Text);
                            int iCurrency = GlobalCode.Field2Int(uoHiddenFieldCurrency.Value);
                            float fTaxPercent = GlobalCode.Field2Float(listTextBoxRoomRateTaxPercentage.Text);
                            bool bIsTaxInclusiveSingle = listCheckBoxTaxInclusive.Checked;

                            //if (iToAddDBLOld != iToAddDBLNew ||
                            //    iToAddSGLOld != iToAddSGLNew)
                            //{
                            r = dt.NewRow();
                            r["colBranchIDInt"] = iHotelID;
                            r["colDate"] = rowDate;
                            r["colToAddDBL"] = iToAddDBLNew;
                            r["colToAddSGL"] = iToAddSGLNew;
                            r["colAction"] = listDropDownListAction.SelectedValue;

                            r["colRoomToDropDBL"] = iToDropDBL;
                            r["colRoomToDropSGL"] = iToDropSGL;

                            r["colRatePerDayMoneySGL"] = fRatePerDayMoneySGL;
                            r["colRatePerDayMoneyDBL"] = fRatePerDayMoneyDBL;
                            r["colCurrencyIDInt"] = iCurrency;
                            r["colRoomRateTaxPercentage"] = fTaxPercent;
                            r["colRoomRateIsTaxInclusive"] = bIsTaxInclusiveSingle;
                          
                            dt.Rows.Add(r);
                            //}
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    BLL.ApproveForecastManifest(GlobalCode.Field2Int(uoHiddenFieldHotelID.Value),

                        //GlobalCode.Field2Float(uoTextBoxRateSingle.Text),
                        //GlobalCode.Field2Int(uoDropDownListCurrencySingle.SelectedValue),
                        //GlobalCode.Field2Float(uoTextBoxTaxSingle.Text),
                        //uoCheckBoxTaxInclusiveSingle.Checked,

                        //GlobalCode.Field2Float(uoTextBoxRateDouble.Text),
                        //GlobalCode.Field2Int(uoDropDownListCurrencyDouble.SelectedValue),
                        //GlobalCode.Field2Float(uoTextBoxTaxDouble.Text),
                        //uoCheckBoxTaxInclusiveDouble.Checked,

                        uoHiddenFieldUser.Value, "Add Override Forecast by Vendor", "SaveHotelForecast",
                        Path.GetFileName(Request.Path),
                        CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, dt);

                    GetSFHotelTravelDetails();
                    AlertMessage("Record successfully saved!");
                }
                else
                {
                    AlertMessage("No record changed!");
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
        /// Date Created:   4/Feb/2014
        /// Created By:     Josephine Monteza
        /// Description:    Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Monteza
        /// Date Created:   06/Feb/2015
        /// Descrption:     Bind No. Of Days
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindNoOfDays()
        {
            if (GlobalCode.Field2String(uoDropDownListDays.SelectedValue) == "")
            {

                uoDropDownListDays.Items.Clear();
                uoDropDownListDays.Items.Add(new ListItem("--Select No. of Days--", "0"));

                int iNoOfdays = TMSettings.NoOfDaysForecastVendor;
                ListItem item;
                for (int i = 1; i <= iNoOfdays; i++)
                {
                    item = new ListItem(i.ToString(), i.ToString());
                    uoDropDownListDays.Items.Add(item);
                }

                uoDropDownListDays.DataBind();
                if (Session["NoOfDays_Forecast_Vendor"] != null)
                {
                    string sDay = GlobalCode.Field2Int(Session["NoOfDays_Forecast_Vendor"]).ToString();
                    if (uoDropDownListDays.Items.FindByValue(sDay) != null)
                    {
                        uoDropDownListDays.SelectedValue = sDay;
                    }
                }
                else
                {
                    uoDropDownListDays.SelectedValue = GlobalCode.Field2String(iNoOfdays);
                }
                Session["NoOfDays_Forecast_Vendor"] = uoDropDownListDays.SelectedValue;
                uoHiddenFieldNoOfDays.Value = uoDropDownListDays.SelectedValue;
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   8/May/2015
        /// Description:    Bind Days
        /// </summary>
        private void BindDayTo()
        {
            if (Session["NoOfDays_Forecast_Vendor"] != null)
            {
                int i = GlobalCode.Field2Int(Session["NoOfDays_Forecast_Vendor"]);
                uoHiddenFieldTo.Value = GlobalCode.Field2DateTime(
                    uoHiddenFieldFrom.Value).AddDays(i).ToString();
            }
            else
            {
                int i = TMSettings.NoOfDaysForecast;
                uoHiddenFieldTo.Value = GlobalCode.Field2DateTime(
                    uoHiddenFieldFrom.Value).AddDays(i).ToString();
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   4/May/2015
        /// Description:    Bind Currency
        /// </summary>
        //private void BindCurrency()
        //{
        //    List<Currency> listCurrency = new List<Currency>();

        //    if (Session["HotelForecastMicroApproval_Currency"] != null)
        //    {
        //        listCurrency = (List<Currency>)Session["HotelForecastMicroApproval_Currency"];
        //        List<HotelForecastCurrency> listCurrencySelected = new List<HotelForecastCurrency>();
        //        listCurrencySelected = (List<HotelForecastCurrency>)Session["HotelForecastMicroApproval_CurrencySelected"];

        //        uoDropDownListCurrencySingle.Items.Clear();
        //        uoDropDownListCurrencySingle.Items.Add(new ListItem("--Select Currency--", "0"));

        //        uoDropDownListCurrencyDouble.Items.Clear();
        //        uoDropDownListCurrencyDouble.Items.Add(new ListItem("--Select Currency--", "0"));

        //        if (Session["HotelForecastMicroApproval_Currency"] != null)
        //        {
        //            listCurrency = (List<Currency>)Session["HotelForecastMicroApproval_Currency"];

        //            uoDropDownListCurrencySingle.DataSource = listCurrency;
        //            uoDropDownListCurrencySingle.DataTextField = "CurrencyName";
        //            uoDropDownListCurrencySingle.DataValueField = "CurrencyID";

        //            uoDropDownListCurrencyDouble.DataSource = listCurrency;
        //            uoDropDownListCurrencyDouble.DataTextField = "CurrencyName";
        //            uoDropDownListCurrencyDouble.DataValueField = "CurrencyID";

        //            uoTextBoxRateSingle.Text = listCurrencySelected[0].RateMoney.ToString("0.00");
        //            uoTextBoxRateDouble.Text = listCurrencySelected[1].RateMoney.ToString("0.00");

        //            uoCheckBoxTaxInclusiveSingle.Checked = listCurrencySelected[0].IsTaxInclusive;
        //            uoCheckBoxTaxInclusiveDouble.Checked = listCurrencySelected[1].IsTaxInclusive;

        //            uoTextBoxTaxSingle.Text = listCurrencySelected[0].Tax.ToString("0.##");
        //            uoTextBoxTaxDouble.Text = listCurrencySelected[1].Tax.ToString("0.##");
        //        }
        //        uoDropDownListCurrencySingle.DataBind();
        //        uoDropDownListCurrencyDouble.DataBind();

        //        if (uoDropDownListCurrencySingle.Items.FindByValue(listCurrencySelected[0].CurrencyID.ToString()) != null)
        //        {
        //            uoDropDownListCurrencySingle.SelectedValue = listCurrencySelected[0].CurrencyID.ToString();
        //        }

        //        if (uoDropDownListCurrencyDouble.Items.FindByValue(listCurrencySelected[1].CurrencyID.ToString()) != null)
        //        {
        //            uoDropDownListCurrencyDouble.SelectedValue = listCurrencySelected[1].CurrencyID.ToString();
        //        }
        //    }
        //}
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   4/May/2015
        /// Description:    Bind Currency
        /// </summary>
        private void BindCurrency()
        {
            List<HotelForecastCurrency> listCurrency = new List<HotelForecastCurrency>();
            if (Session["HotelForecastMicroApproval_CurrencySelected"] != null)
            {
                listCurrency = (List<HotelForecastCurrency>)Session["HotelForecastMicroApproval_CurrencySelected"];
                var list = (from a in listCurrency
                            where a.CurrencyID > 0
                            select new
                            {
                                CurrencyID = a.CurrencyID,
                                CurrencyName = a.CurrencyName,
                            }).ToList().Take(1);

                uoListViewHeader.DataSource = list;
                uoListViewHeader.DataBind();

                uoHiddenFieldCurrency.Value = GlobalCode.Field2Int(list.ToList()[0].CurrencyID).ToString();
               
            }
            else
            {
                uoListViewHeader.DataSource = listCurrency;
                uoListViewHeader.DataBind();
            }

           

            //if (uoListViewHeader.Items.Count > 0)
            //{
            //    Label listLabelCurrency = (Label)uoListViewHeader.Items[0].FindControl("uoLabelCurrency");
            //    if (listLabelCurrency != null)
            //    {
            //        listLabelCurrency.Text = sCurrency;
            //    }
            //}

            //if (Session["HotelForecastMicroApproval_Currency"] != null)
            //{
            //    listCurrency = (List<Currency>)Session["HotelForecastMicroApproval_Currency"];
            //    List<HotelForecastCurrency> listCurrencySelected = new List<HotelForecastCurrency>();
            //    listCurrencySelected = (List<HotelForecastCurrency>)Session["HotelForecastMicroApproval_CurrencySelected"];

            //    uoDropDownListCurrencySingle.Items.Clear();
            //    uoDropDownListCurrencySingle.Items.Add(new ListItem("--Select Currency--", "0"));

            //    uoDropDownListCurrencyDouble.Items.Clear();
            //    uoDropDownListCurrencyDouble.Items.Add(new ListItem("--Select Currency--", "0"));

            //    if (Session["HotelForecastMicroApproval_Currency"] != null)
            //    {
            //        listCurrency = (List<Currency>)Session["HotelForecastMicroApproval_Currency"];

            //        uoDropDownListCurrencySingle.DataSource = listCurrency;
            //        uoDropDownListCurrencySingle.DataTextField = "CurrencyName";
            //        uoDropDownListCurrencySingle.DataValueField = "CurrencyID";

            //        uoDropDownListCurrencyDouble.DataSource = listCurrency;
            //        uoDropDownListCurrencyDouble.DataTextField = "CurrencyName";
            //        uoDropDownListCurrencyDouble.DataValueField = "CurrencyID";

            //        uoTextBoxRateSingle.Text = listCurrencySelected[0].RateMoney.ToString("0.00");
            //        uoTextBoxRateDouble.Text = listCurrencySelected[1].RateMoney.ToString("0.00");

            //        uoCheckBoxTaxInclusiveSingle.Checked = listCurrencySelected[0].IsTaxInclusive;
            //        uoCheckBoxTaxInclusiveDouble.Checked = listCurrencySelected[1].IsTaxInclusive;

            //        uoTextBoxTaxSingle.Text = listCurrencySelected[0].Tax.ToString("0.##");
            //        uoTextBoxTaxDouble.Text = listCurrencySelected[1].Tax.ToString("0.##");
            //    }
            //    uoDropDownListCurrencySingle.DataBind();
            //    uoDropDownListCurrencyDouble.DataBind();

            //    if (uoDropDownListCurrencySingle.Items.FindByValue(listCurrencySelected[0].CurrencyID.ToString()) != null)
            //    {
            //        uoDropDownListCurrencySingle.SelectedValue = listCurrencySelected[0].CurrencyID.ToString();
            //    }

            //    if (uoDropDownListCurrencyDouble.Items.FindByValue(listCurrencySelected[1].CurrencyID.ToString()) != null)
            //    {
            //        uoDropDownListCurrencyDouble.SelectedValue = listCurrencySelected[1].CurrencyID.ToString();
            //    }
            //}
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   24/sept/2015
        /// Description:    Bind Contract
        /// </summary>
        private void BindContractDetails()
        {
            uoLinkButtonViewContract.Visible = false;
            List<ContractHotel> list = new List<ContractHotel>();
            if (Session["HotelForecastMicroApproval_ContractHotel"] != null)
            {
                list = (List<ContractHotel>)Session["HotelForecastMicroApproval_ContractHotel"];

                if (list.Count > 0)
                {
                    uoLinkButtonViewContract.Visible = true;
                    uoHiddenFieldContractID.Value = list[0].contractID.ToString();
                }
            }
        }
        /// <summary>
        /// Modified By:    Josephine Monteza
        /// Date Modified:  23/Sept/2015
        /// Description:    Bind uoDropDownListAction
        /// </summary>
        private void BindDDLAction(bool IsDrop, DropDownList list)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("--Select--", "Action"));
            if (IsDrop)
            {
                list.Items.Add(new ListItem("Drop", "Drop"));
            }
            else
            {
                list.Items.Add(new ListItem("Accept", "Accept"));
                list.Items.Add(new ListItem("Decline", "Decline"));
                list.Items.Add(new ListItem("Edit", "Edit"));
            }
            list.DataBind();
        }
        #endregion     

     

        
    }
}

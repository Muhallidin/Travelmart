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
    public partial class HotelForecastMicroApproval2 : System.Web.UI.Page
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
                GetHotelFilter();

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
                //DateTime dDateCurrent = DateTime.Now;
              
                string sHotel = GlobalCode.Field2String(Session["Hotel"]);
                if (sHotel != "" && sHotel != "0")
                {
                    if (uoDropDownListHotel.Items.FindByValue(sHotel) != null)
                    {
                        uoDropDownListHotel.SelectedValue = sHotel;
                    }
                }
                LoadDefaults(0);

                //SetCalendarLink();
                //uoHyperLinkCalendar.NavigateUrl = "../CalendarPopUpForecast.aspx?dt=" + uoHiddenFieldDate.Value + "&bID=0";
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

                if (uoHiddenFieldPopupRequestToOther.Value == "1")
                {
                    RequestRoom();
                    uoHiddenFieldPopupRequestToOther.Value = "0";
                }
                if (uoHiddenFieldSave.Value == "1")
                {
                    SaveHotelForecast();
                    uoHiddenFieldSave.Value = "0";
                }

            }
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = "";
            Session.Remove("Port"); // remove the current selected Port 05/07/2012
            LoadDefaults(1);
            Session["Forecast_Hotel"] = null;
            GetHotelFilter();
        }

        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            LoadDefaults(1);
            Session["Forecast_Hotel"] = null;
            GetHotelFilter();
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
            BindContractDetails();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveHotelForecast();
        }
        protected void uoButtonApprove_Click(object sender, EventArgs e)
        {
            ApproveHotelForecast();
        }
        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            ButtonLockSettings();

            if (uoHiddenFieldLoadType.Value == "0")
            {
                DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));

                uoTextBoxFrom.Text = dDateFrom.ToString(TravelMartVariable.DateFormat);
                uoTextBoxTo.Text = dDateFrom.AddDays(TMSettings.NoOfDaysForecast).ToString(TravelMartVariable.DateFormat);
            }
        }
        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (uoDropDownListHotel.SelectedValue == "0")
            {
                e.InputParameters["sBranchName"] = "";
            }
            else
            {
                e.InputParameters["sBranchName"] = uoDropDownListHotel.SelectedItem.Text;
            }

            e.InputParameters["sDateFrom"] = uoTextBoxFrom.Text;
            e.InputParameters["sDateTo"] = uoTextBoxTo.Text;

            e.InputParameters["sVesselCode"] = "";
            e.InputParameters["sPortID"] = "0";

            e.InputParameters["sUser"] = uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldRole.Value;

            e.InputParameters["LoadType"] = uoHiddenFieldLoadType.Value;
            e.InputParameters["bShowAll"] = uoCheckBoxShowAll.Checked;

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

            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        /// <summary>
        /// Modified By:    Josephine gad
        /// Date Modified:  03/10/2012
        /// Description:    User BindRegionList to bind Region List
        /// </summary>
        public void LoadDefaults(short LoadType)
        {
            if (LoadType == 0)
            {
                BindRegionList();
            }
            BindPortList();
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
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
        /// Date Created:   05/07/2012
        /// Created by:     Jefferson Bermundo
        /// Description:    For Filtering based on port per region
        /// ------------------------
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
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
        private void GetHotelFilter()
        {
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                int iRowCount = 0;
                if (Session["Forecast_Hotel"] != null)
                {
                    list = (List<HotelDTO>)Session["Forecast_Hotel"];
                }
                else
                {
                    list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, Session["Region"].ToString(),
                        Session["Port"] == null ? "0" : Session["Port"].ToString(), "0", "0");
                }
                Session["Forecast_Hotel"] = list;


                iRowCount = list.Count;
                if (iRowCount == 1)
                {
                    Session["Hotel"] = list[0].HotelIDString;//dt.Rows[0]["BranchID"].ToString();
                }

                if (iRowCount > 0)
                {
                    uoDropDownListHotel.Items.Clear();
                    uoDropDownListHotel.DataSource = list;
                    uoDropDownListHotel.DataTextField = "HotelNameString";
                    uoDropDownListHotel.DataValueField = "HotelIDString";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    uoDropDownListHotel.SelectedValue = "0";

                    //uoDropDownListHotelToRequestDBL.Items.Clear();
                    //uoDropDownListHotelToRequestDBL.DataSource = list;
                    //uoDropDownListHotelToRequestDBL.DataTextField = "HotelNameString";
                    //uoDropDownListHotelToRequestDBL.DataValueField = "HotelIDString";
                    //uoDropDownListHotelToRequestDBL.DataBind();
                    //uoDropDownListHotelToRequestDBL.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    //uoDropDownListHotelToRequestDBL.SelectedValue = "0";

                    //uoDropDownListHotelToRequestSGL.Items.Clear();
                    //uoDropDownListHotelToRequestSGL.DataSource = list;
                    //uoDropDownListHotelToRequestSGL.DataTextField = "HotelNameString";
                    //uoDropDownListHotelToRequestSGL.DataValueField = "HotelIDString";
                    //uoDropDownListHotelToRequestSGL.DataBind();
                    //uoDropDownListHotelToRequestSGL.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    //uoDropDownListHotelToRequestSGL.SelectedValue = "0";

                    RemoveDuplicateItems(uoDropDownListHotel);
                    uoDropDownListHotel.Enabled = true;

                    if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                    {
                        uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                    }
                    LoadTravelDetails();
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
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;
            //if (uoDropDownListHotel.SelectedValue == "0")
            //{
            //    uoBtnExportList.Enabled = false;
            //}
            //else
            //{
            //    uoBtnExportList.Enabled = true;
            //}

            if (uoDropDownListHotel.SelectedValue == "-1")
            {
                uoDropDownListPortPerRegion.SelectedValue = "0";
                Session["Port"] = "0";
                uoDropDownListPortPerRegion.Enabled = false;
            }
            else
            {
                uoDropDownListPortPerRegion.Enabled = true;
            }

            uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
            DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
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
        private void ButtonLockSettings()
        {
            if (uolistviewHotelInfo.Items.Count > 0)
            {
                //uoBtnExportList.Enabled = true;
                if (uoDropDownListHotel.SelectedValue == "-1")
                {
                    //uoButtonLock.Enabled = false;
                }
            }
            else
            {
                //uoBtnExportList.Enabled = false;
            }

            string sDate = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
            if (GlobalCode.Field2DateTime((sDate)) < DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")))
            {
                //uoButtonLock.Enabled = false;
            }
            //bool IsAlreadyLocked = HotelManifestBLL.IsHotelHasLockedManifest(sDate, uoDropDownListHotel.SelectedValue, uoDropDownListHours.SelectedValue);
            //if (IsAlreadyLocked)
            //{
            //    uoButtonLock.Enabled = false;
            //}
        }
        /// <summary>
        /// Date Created:   04/Feb/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save the "ToBeAdded" column for Vendor's approval
        /// -------------------------------------------------------
        /// </summary>
        private void SaveHotelForecast()
        {
            DataTable dt = null;
            try
            {
                HotelForecastBLL BLL = new HotelForecastBLL();               
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                DateTime dateOnly = GlobalCode.Field2DateTime(dateNow.ToString("MM/dd/yyyy"));

                Label listLabelDate;

                //HiddenField listHiddenFieldToAddDBL;
                //HiddenField listHiddenFieldToAddSGL;

                TextBox listTextBoxDBL;
                TextBox listTextBoxSGL;
                TextBox listTextBoxRemarks;
                TextBox listTextBoxRoomToDropDBL;
                TextBox listTextBoxRoomToDropSGL;

                CheckBox listCheckBoxSelect;

                int iCount = uolistviewHotelInfo.Items.Count;
                int iHotelID = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue);

                DataColumn colBranchIDInt = new DataColumn("colBranchIDInt", typeof(Int64));
                DataColumn colDate = new DataColumn("colDate", typeof(DateTime));
                DataColumn colForecastDBLAdj = new DataColumn("colForecastDBLAdj", typeof(int));
                DataColumn colForecastSGLAdj = new DataColumn("colForecastSGLAdj", typeof(int));
                DataColumn colRemarksVarchar = new DataColumn("colRemarksVarchar", typeof(string));
                DataColumn colRoomToDropDBL = new DataColumn("colRoomToDropDBL", typeof(int));
                DataColumn colRoomToDropSGL = new DataColumn("colRoomToDropSGL", typeof(int));

                dt = new DataTable();
                dt.Columns.Add(colBranchIDInt);
                dt.Columns.Add(colDate);
                dt.Columns.Add(colForecastDBLAdj);
                dt.Columns.Add(colForecastSGLAdj);
                dt.Columns.Add(colRemarksVarchar);
                dt.Columns.Add(colRoomToDropDBL);
                dt.Columns.Add(colRoomToDropSGL);

                DataRow r;
                DateTime rowDate;
                
                for (int i = 0; i < iCount; i++)
                {
                    listLabelDate = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLabelDate");
                    rowDate = GlobalCode.Field2DateTime(listLabelDate.Text);

                    if (rowDate >= dateOnly)
                    {

                        listCheckBoxSelect = (CheckBox)uolistviewHotelInfo.Items[i].FindControl("uoCheckBoxSelect");
                        if (listCheckBoxSelect.Checked)
                        {
                            //listHiddenFieldToAddDBL = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldToAddDBL");
                            //listHiddenFieldToAddSGL = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldToAddSGL");

                            listTextBoxDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxDBLAdj");
                            listTextBoxSGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxSGLAdj");
                            listTextBoxRemarks = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRemarks");

                            listTextBoxRoomToDropDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomToDropDBL");
                            listTextBoxRoomToDropSGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomToDropSGL");


                            int iToAddDBLAdj = GlobalCode.Field2Int(listTextBoxDBL.Text);
                            int iToAddSGLAdj = GlobalCode.Field2Int(listTextBoxSGL.Text);

                            int iRoomToDropDBL = GlobalCode.Field2Int(listTextBoxRoomToDropDBL.Text);
                            int iRoomToDropSGL = GlobalCode.Field2Int(listTextBoxRoomToDropSGL.Text);
                            
                            r = dt.NewRow();
                            r["colBranchIDInt"] = iHotelID;
                            r["colDate"] = rowDate;
                            r["colForecastDBLAdj"] = iToAddDBLAdj;
                            r["colForecastSGLAdj"] = iToAddSGLAdj;
                            r["colRemarksVarchar"] = listTextBoxRemarks.Text;

                            r["colRoomToDropDBL"] = iRoomToDropDBL;
                            r["colRoomToDropSGL"] = iRoomToDropSGL;
                            dt.Rows.Add(r);
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    BLL.UpdateForecastManifest(GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue),
                        uoHiddenFieldUser.Value, "Adjust Forecast", "SaveHotelForecast",
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
        /// <summary>
        /// Date Created:   07/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save the Additional Room to Hotel Room Blocks by RCCL
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

                //HiddenField listHiddenFieldToAddDBL;
                //HiddenField listHiddenFieldToAddSGL;

                //TextBox listTextBoxDBL;
                //TextBox listTextBoxSGL;

                TextBox listTextBoxRoomToDropDBL;
                TextBox listTextBoxRoomToDropSGL;

                //TextBox listTextBoxRatePerDayMoneyDBL;
                //TextBox listTextBoxRatePerDayMoneySGL;
                //TextBox listTextBoxRoomRateTaxPercentage;
                //CheckBox listCheckBoxTaxInclusive;

                Label listLabelRoomApprovedSGL;
                Label listLabelRoomApprovedDBL;
                
                CheckBox listCheckBoxApprove;
                HiddenField listHiddenFieldAction;

                int iCount = uolistviewHotelInfo.Items.Count;
                int iHotelID = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue);

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

                    listCheckBoxApprove = (CheckBox)uolistviewHotelInfo.Items[i].FindControl("uoCheckBoxApprove");
                    if (listCheckBoxApprove.Checked)
                    {
                        listHiddenFieldAction = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldAction");
                        listLabelDate = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLabelDate");
                        rowDate = GlobalCode.Field2DateTime(listLabelDate.Text);

                        if (rowDate >= dateOnly)
                        {
                            //listHiddenFieldToAddDBL = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldToAddDBL");
                            //listHiddenFieldToAddSGL = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldToAddSGL");

                            listLabelRoomApprovedDBL = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLabelRoomApprovedDBL");
                            listLabelRoomApprovedSGL = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLabelRoomApprovedSGL");
                                                        
                            //listTextBoxDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxToAddDBL");
                            //listTextBoxSGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxToAddSGL");

                            listTextBoxRoomToDropDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomToDropDBL");
                            listTextBoxRoomToDropSGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomToDropSGL");

                            //listTextBoxRatePerDayMoneyDBL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRatePerDayMoneyDBL");
                            //listTextBoxRatePerDayMoneySGL = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRatePerDayMoneySGL");

                            //listTextBoxRoomRateTaxPercentage = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRoomRateTaxPercentage"); ;
                            //listCheckBoxTaxInclusive = (CheckBox)uolistviewHotelInfo.Items[i].FindControl("uoCheckBoxTaxInclusive");

                            //int iToAddDBLOld = GlobalCode.Field2Int(listHiddenFieldToAddDBL.Value);
                            //int iToAddSGLOld = GlobalCode.Field2Int(listHiddenFieldToAddSGL.Value);

                            int iToAddDBLNew = GlobalCode.Field2Int(listLabelRoomApprovedDBL.Text);
                            int iToAddSGLNew = GlobalCode.Field2Int(listLabelRoomApprovedSGL.Text);

                            int iToDropDBL = GlobalCode.Field2Int(listTextBoxRoomToDropDBL.Text);
                            int iToDropSGL = GlobalCode.Field2Int(listTextBoxRoomToDropSGL.Text);

                            //float fRatePerDayMoneyDBL = GlobalCode.Field2Float(listTextBoxRatePerDayMoneyDBL.Text);
                            //float fRatePerDayMoneySGL = GlobalCode.Field2Float(listTextBoxRatePerDayMoneySGL.Text);
                            //int iCurrency = GlobalCode.Field2Int(uoHiddenFieldCurrency.Value);
                            //float fTaxPercent = GlobalCode.Field2Float(listTextBoxRoomRateTaxPercentage.Text);
                            //bool bIsTaxInclusiveSingle = listCheckBoxTaxInclusive.Checked;

                            //if (iToAddDBLOld != iToAddDBLNew ||
                            //    iToAddSGLOld != iToAddSGLNew)
                            //{
                            r = dt.NewRow();
                            r["colBranchIDInt"] = iHotelID;
                            r["colDate"] = rowDate;
                            r["colToAddDBL"] = iToAddDBLNew;
                            r["colToAddSGL"] = iToAddSGLNew;
                            r["colAction"] = listHiddenFieldAction.Value;

                            r["colRoomToDropDBL"] = iToDropDBL;
                            r["colRoomToDropSGL"] = iToDropSGL;

                            //r["colRatePerDayMoneySGL"] = fRatePerDayMoneySGL;
                            //r["colRatePerDayMoneyDBL"] = fRatePerDayMoneyDBL;
                            //r["colCurrencyIDInt"] = iCurrency;
                            //r["colRoomRateTaxPercentage"] = fTaxPercent;
                            //r["colRoomRateIsTaxInclusive"] = bIsTaxInclusiveSingle;

                            dt.Rows.Add(r);
                            //}
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    BLL.ApproveForecastManifestByRCCL(GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue),

                        uoHiddenFieldUser.Value, "Add Override Forecast by RCCl Approval", "ApproveHotelForecast",
                        Path.GetFileName(Request.Path),
                        CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, dt);

                    GetSFHotelTravelDetails();
                    AlertMessage("Record successfully approved!");
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
        /// <summary>
        /// Date Created:   29/Jul/2015
        /// Created By:     Josephine Monteza
        /// Description:    Request Room to Other Hotel
        /// </summary>
        private void RequestRoom()
        {
            try
            {
                HotelForecastBLL BLL = new HotelForecastBLL();               

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                DateTime dtDate = GlobalCode.Field2DateTime(uoHiddenFieldDateSelected.Value);
                Int32 iBranchIDSGL = GlobalCode.Field2Int(uoHiddenFieldHotelSGL.Value);
                Int32 iBranchIDDBL = GlobalCode.Field2Int(uoHiddenFieldHotelDBL.Value);
                Int32 iBranchID = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue);

                int iRoomCountSGL = GlobalCode.Field2Int(uoHiddenFieldRoomCountSGL.Value);
                int iRoomCountDBL = GlobalCode.Field2Int(uoHiddenFieldRoomCountDBL.Value);

                BLL.RequestHotelRoom(dtDate, uoCheckBoxSGL.Checked, uoCheckBoxDBL.Checked,
                    iBranchID, iBranchIDSGL, iBranchIDDBL, iRoomCountSGL, iRoomCountDBL,
                    uoHiddenFieldUser.Value, "Adjust Forecast through Request", "RequestRoom",
                   Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(dateNow));

                GetSFHotelTravelDetails();
                AlertMessage("Record successfully saved!");
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// Author:         Josephine Monteza
        /// Date Created:   24/sept/2015
        /// Description:    Bind Contract
        /// </summary>
        private void BindContractDetails()
        {
            uoLinkButtonViewContract.Attributes.Remove("onclick");
            uoLinkButtonViewContract.Visible = false;

            List<ContractHotel> list = new List<ContractHotel>();
            if (Session["HotelForecastMicroApproval_ContractHotel"] != null)
            {
                list = (List<ContractHotel>)Session["HotelForecastMicroApproval_ContractHotel"];

                if (list.Count > 0)
                {
                    uoLinkButtonViewContract.Visible = true;
                    uoHiddenFieldContractID.Value = list[0].contractID.ToString();

                    uoLinkButtonViewContract.Attributes.Add("onclick", "OpenContract('"+ uoDropDownListHotel.SelectedValue +"','"+ uoHiddenFieldContractID.Value +"');");
                }
            }            
        }
        #endregion       
    }
}

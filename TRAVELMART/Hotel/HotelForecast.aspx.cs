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
    public partial class HotelForecast : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   23/03/2012
        /// Description:    move tentative manifest to new page and new master
        /// --------------------------------------------------------------
        /// Date Modified:  23/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Refresh list view, Manifest Hrs Drop down, 
        ///                 selected date and default hotel if new date is selected
        /// -------------------------------------------
        /// Date Modified:  06/07/2012
        /// Modified By:    Jefferson Bermundo
        /// Description:    Add Region and Port filter,
        ///                 adjustment on the report view, if the hotel list for a port had only one
        ///                 Hotel, select the hotel and shows the report.
        /// -------------------------------------------
        /// Date Modified:  27/09/2012
        /// Modified By:    Josephine Gad
        /// Description:    Add 8 days and 16 days to the manifest hours selection
        /// -------------------------------------------
        /// Date Modified:  05/10/2012
        /// Modified By:    Josephine Gad
        /// Description:    Remove 8 and 16 days
        /// -------------------------------------------
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
                uoTblDate.Style.Add("display","none");
                //uoTblDate.Visible = false;

                SetDefaultValues();
                GetNationality();
                GetGender();
                GetRank();
                GetVessel();
                GetHotelFilter();

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist)
                {
                    //uoButtonLock.Visible = true;
                }
                else
                {
                    //uoButtonLock.Visible = false;
                }
                DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                TimeSpan timeDiff = dDateFrom - dDateCurrent;
                int dDays = timeDiff.Days;

                string sHotel = GlobalCode.Field2String(Session["Hotel"]);
                if (sHotel != "" && sHotel != "0")
                {
                    if (uoDropDownListHotel.Items.FindByValue(sHotel) != null)
                    {
                        uoDropDownListHotel.SelectedValue = sHotel;
                    }
                }
                LoadDefaults(0);

                SetCalendarLink();
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
                    TimeSpan timeDiff = dDateFrom - dDateCurrent;
                    int dDays = timeDiff.Days;

                    GetSFHotelTravelDetails();
                }
            }
        }

        //protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadTravelDetails();
        //}

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
            SetCalendarLink();
        }
       
        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFromString"] = uoTextBoxFrom.Text;
            e.InputParameters["DateToString"] = uoTextBoxTo.Text;

            e.InputParameters["strUser"] = uoHiddenFieldUser.Value;

            e.InputParameters["DateFilter"] = "1";
            e.InputParameters["ByNameOrID"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["filterNameOrID"] = uoTextBoxFilter.Text.Trim();

            e.InputParameters["Nationality"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["Rank"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;

            e.InputParameters["Region"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["Country"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["City"] = GlobalCode.Field2String(Session["City"]);

            e.InputParameters["Port"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["Hotel"] = uoDropDownListHotel.SelectedValue;
            e.InputParameters["Vessel"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["UserRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["LoadType"] = uoHiddenFieldLoadType.Value;
        }

        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            ButtonLockSettings();
        }

        //protected void uoButtonLock_Click(object sender, EventArgs e)
        //{
            //string strLogDescription;
            //string strFunction;

            //if (uoDropDownListHotel.SelectedValue != "")
            //{
            //    if (uoDropDownListHotel.SelectedValue != "0")
            //    {
            //        Int32 ID = HotelManifestBLL.InsertHotelManifestLockHeader(uoDropDownListHours.SelectedValue, uoDropDownListHotel.SelectedValue,
            //                                                                    uoHiddenFieldUser.Value, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString());

            //        //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            //        strLogDescription = "Hotel tentative manifest locked.";
            //        strFunction = "uoButtonLock_Click";

            //        DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //        AuditTrailBLL.InsertLogAuditTrail(ID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

            //        uoButtonLock.Enabled = false;
            //        AlertMessage("Manifest has been locked.");
            //    }
            //    else
            //    {
            //        AlertMessage("Select hotel.");
            //    }
            //}
            //else
            //{
            //    AlertMessage("Select hotel.");
            //}
        //}

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 23/03/2012
        /// Description: Clears the filters and displays the tentative manifest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonClear_Click(object sender, EventArgs e)
        {
            uoDropDownListVessel.SelectedIndex = 0;
            uoDropDownListFilterBy.SelectedIndex = 0;
            uoDropDownListNationality.SelectedIndex = 0;
            uoDropDownListGender.SelectedIndex = 0;
            uoDropDownListRank.SelectedIndex = 0;
            uoDropDownListStatus.SelectedIndex = 0;
            uoTextBoxFilter.Text = "";

            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
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

            SetCalendarLink();
        }

        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            LoadDefaults(1);
            Session["Forecast_Hotel"] = null;
            GetHotelFilter();

            SetCalendarLink();
        }
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            ManifestBLL MBLL = new ManifestBLL();
            List<TentativeManifestExport> TentativeExport = new List<TentativeManifestExport>();
            TentativeExport = MBLL.GetForecastExportList(GlobalCode.Field2DateTime(uoTextBoxFrom.Text), 
                GlobalCode.Field2DateTime(uoTextBoxTo.Text),
                GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue));
            DataTable dt = null;
            try
            {
                if (TentativeExport.Count > 0)
                {
                    try
                    {
                        dt = getDataTable(TentativeExport);
                        CreateFile(dt);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {

                    }
                }
                else
                {
                    AlertMessage("There is no record to export.");
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
        /// Modified By:    Josephine gad
        /// Date Modified:  01/Feb/2013
        /// Description:    Export Count Summary
        /// --------------------------------------
        /// Modified By:    Josephine Monteza
        /// Date Modified:  14/Jan/2015
        /// Description:    Changed GetCalendarRoomNeeded to GetCalendarRoomNeeded_Forecast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoLinkButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                CalendarBLL calendar = new CalendarBLL();
                dt = calendar.GetCalendarRoomNeeded_Forecast(GlobalCode.Field2String(Session["UserName"]), 
                    GlobalCode.Field2DateTime(uoTextBoxFrom.Text),
                    uoTextBoxTo.Text, GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue), 
                    GlobalCode.Field2Int(uoDropDownListPortPerRegion.SelectedValue), 
                    GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue));
                if(dt.Rows.Count > 0)
                {
                    CreateFileCount(dt);
                }
                else
                {
                    AlertMessage("No Record.");
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


        #region EVENTS
        protected void InitializeValues()
        {
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

        private void GetSFHotelTravelDetails()
        {
            try
            {
                uolistviewHotelInfo.DataSource = null;
                uolistviewHotelInfo.DataSourceID = "uoObjectDataSourceManifest";
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
            ManifestBLL BLL= new ManifestBLL();
            BLL.ForecastGetFilters(uoHiddenFieldUser.Value, 
                GlobalCode.Field2Int(Session["Region"]),
                GlobalCode.Field2Int(Session["Port"]), 0);
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  26/Nov/2012
        /// Description:    Use Session["Forecast_Nationality"] to get nationality
        /// ----------------------------------------------
        /// </summary>
        private void GetNationality()
        {
            DataTable dt = null;
            try
            {
                ListItem item = new ListItem("--Select Nationality--", "0");
                uoDropDownListNationality.Items.Clear();
                uoDropDownListNationality.Items.Add(item);
                              
                if (Session["Forecast_Nationality"] != null)
                {
                    List<NationalityList> list = new List<NationalityList>();
                    list = (List<NationalityList>)Session["Forecast_Nationality"];
                    uoDropDownListNationality.DataSource = list;
                    uoDropDownListNationality.DataTextField = "Nationality";
                    uoDropDownListNationality.DataValueField = "NationalityID";
                }
                else
                {
                    dt = MasterfileBLL.GetReference("Nationality");
                    
                    uoDropDownListNationality.DataSource = dt;
                    uoDropDownListNationality.DataTextField = "RefName";
                    uoDropDownListNationality.DataValueField = "RefID";
                }
                uoDropDownListNationality.DataBind();
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
        /// Modified By:    Josephine Gad
        /// Date Modified:  26/Nov/2012
        /// Description:    Use Session["Forecast_Gender"] to get gender
        /// ----------------------------------------------
        /// </summary>
        private void GetGender()
        {
            DataTable dt = null;
            try
            {  
                ListItem item = new ListItem("--Select Gender--", "0");
                uoDropDownListGender.Items.Clear();
                uoDropDownListGender.Items.Add(item);

                if (Session["Forecast_Gender"] != null)
                {
                    List<GenderList> list = new List<GenderList>();
                    list = (List<GenderList>)Session["Forecast_Gender"];
                    uoDropDownListGender.DataSource = list;
                    uoDropDownListGender.DataTextField = "Gender";
                    uoDropDownListGender.DataValueField = "GenderID";
                }
                else
                {
                    dt = MasterfileBLL.GetReference("Gender");
                    uoDropDownListGender.DataSource = dt;
                    uoDropDownListGender.DataTextField = "RefName";
                    uoDropDownListGender.DataValueField = "RefID";
                }
                uoDropDownListGender.DataBind();
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
        /// Modified By:    Josephine Gad
        /// Date Modified:  26/Nov/2012
        /// Description:    Use Session["Forecast_Rank"] to get rank
        /// ----------------------------------------------
        /// </summary>
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                ListItem item = new ListItem("--Select Rank--", "0");
                uoDropDownListRank.Items.Clear();
                uoDropDownListRank.Items.Add(item);
                if (Session["Forecast_Rank"] != null)
                {
                    List<RankList> list = new List<RankList>();
                    list = (List<RankList>)Session["Forecast_Rank"];
                    uoDropDownListRank.DataSource = list;
                    uoDropDownListRank.DataTextField = "Rank";
                    uoDropDownListRank.DataValueField = "RankID";
                }
                else
                {
                    dt = SeafarerTravelBLL.GetRankByVessel("0");

                    uoDropDownListRank.DataSource = dt;
                    uoDropDownListRank.DataTextField = "RankName";
                    uoDropDownListRank.DataValueField = "RankID";
                }
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
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  26/Nov/2012
        /// Description:    Use Session["Forecast_Vessel"] to get Vessel
        /// ----------------------------------------------
        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Ship--", "0");
                uoDropDownListVessel.Items.Add(item);

                if (Session["Forecast_Vessel"] != null)
                {
                    List<VesselList> list = new List<VesselList>();
                    list = (List<VesselList>)Session["Forecast_Vessel"];
                    uoDropDownListVessel.DataSource = list;
                    uoDropDownListVessel.DataTextField = "VesselName";
                    uoDropDownListVessel.DataValueField = "VesselID";
                }
                else
                {
                    VesselDataTable = VesselBLL.GetVessel(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(),
                        GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                        GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value);

                    uoDropDownListVessel.DataSource = VesselDataTable;
                    uoDropDownListVessel.DataTextField = "VesselName";
                    uoDropDownListVessel.DataValueField = "VesselID";
                }
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


                uoDropDownListHotel.Items.Clear();
                uoDropDownListHotel.DataSource = list;
                uoDropDownListHotel.DataTextField = "HotelNameString";
                uoDropDownListHotel.DataValueField = "HotelIDString";
                uoDropDownListHotel.DataBind();
                uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                uoDropDownListHotel.SelectedValue = "0";

                if (iRowCount > 0)
                {
                    

                    if (GlobalCode.Field2Int(Session["Region"]) > 0)
                    {
                        if (uoDropDownListHotel.Items.FindByValue("-1") == null)
                        {
                            uoDropDownListHotel.Items.Insert(1, new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                    else
                    {
                        if (uoDropDownListHotel.Items.FindByValue("-1") != null)
                        {
                            uoDropDownListHotel.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
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
                //uoButtonLock.Enabled = true;
                uoBtnExportList.Enabled = true;
                if (uoDropDownListHotel.SelectedValue == "-1")
                {
                    //uoButtonLock.Enabled = false;
                }
            }
            else
            {
                //uoButtonLock.Enabled = false;
                uoBtnExportList.Enabled = false;
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

        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel"; //"Check In";
            string GroupValueString = "HotelBranch"; //"colTimeSpanStartDate";

            if (Eval(GroupValueString) != null)
            {
                string currentDataFieldValue = Eval(GroupValueString).ToString();

                //Specify name to display if dataFieldValue is a database NULL
                if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
                {
                    currentDataFieldValue = "";
                }
                //See if there's been a change in value
                if (lastDataFieldValue != currentDataFieldValue) //Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy")
                {
                    //There's been a change! Record the change and emit the table row
                    lastDataFieldValue = currentDataFieldValue; //Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy")
                    return string.Format("<tr><td class=\"group\" colspan=\"37\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
                }
                else
                {
                    //No change, return an empty string
                    return string.Empty;
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "scr", sScript, false);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        #endregion
       
        /// <summary>
        /// Author: Chralene Remotigue
        /// Date Created: 17/04/2012
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
        /// Date Created: 17/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
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
                string FilePath = Server.MapPath("~/Extract/TentativeManifest/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "TentativeManifest_" + sDate + ".xls";
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
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 30;
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
                xtwWriter.WriteAttributeString("ss", "Name", null, "Tentative Manifest");

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
                int i = 1;
                foreach (DataColumn Header in dtSource.Columns)
                {
                    if (i <= iColCount)
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
                    i++;
                }
                xtwWriter.WriteEndElement();


                // Run through all rows of data source
                foreach (DataRow row in dtSource.Rows)
                {
                    // <Row>
                    xtwWriter.WriteStartElement("Row");

                    i = 1;
                    // Run through all cell of current rows
                    foreach (object cellValue in row.ItemArray)
                    {
                        if (i <= iColCount)
                        {
                            // <Cell>
                            xtwWriter.WriteStartElement("Cell");

                            // <Data ss:Type="String">xxx</Data>
                            xtwWriter.WriteStartElement("Data");

                            if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNIGHTS" ||
                                dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                            {
                                xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                            }
                            else
                            {
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            }
                            // Write content of cell
                            xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));

                            // </Data>
                            xtwWriter.WriteEndElement();

                            // </Cell>
                            xtwWriter.WriteEndElement();
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

        /// <summary>
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  12/04/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/TentativeManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
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

        static DataTable ConvertListToDataTable(List<HotelDTO> list)
        {
            DataTable table = null;
            try
            {
                // New table.
                table = new DataTable();
                table.Columns.Add("BranchId");
                table.Columns.Add("BranchName");
                // Add rows.
                foreach (var array in list)
                {
                    table.Rows.Add(array.HotelIDString, array.HotelNameString);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (table != null)
                {
                    table.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   July 6, 2012
        /// Created by:     Jefferson S. Bermundo
        /// Description:    Created a class for loading the Hotel Details,
        ///                 since loading can be called by multiple events
        /// ----------------------------------------------------------------
        /// Date Modified:  27/09/2012
        /// Modified By:    Josephine Gad
        /// Description:    Add 8 days and 16 days to the manifest hours selection
        /// -------------------------------------------
        /// Date Modified:  05/10/2012
        /// Modified By:    Josephine Gad
        /// Description:    Remove 8 and 16 days
        /// -------------------------------------------
        /// </summary>
        private void LoadTravelDetails()
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;
            if (uoDropDownListHotel.SelectedValue == "0")
            {
                //uoButtonLock.Enabled = false;
                uoBtnExportList.Enabled = false;
            }
            else
            {
                //uoButtonLock.Enabled = true;
                uoBtnExportList.Enabled = true;
            }

            if (uoDropDownListHotel.SelectedValue == "-1")
            {
                uoDropDownListPortPerRegion.SelectedValue = "0";
                Session["Port"] = "0";
                uoDropDownListPortPerRegion.Enabled = false;
                //uoButtonLock.Enabled = false;
            }
            else
            {
                uoDropDownListPortPerRegion.Enabled = true;
            }

            uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
            DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
            TimeSpan timeDiff = dDateFrom - dDateCurrent;
            int dDays = timeDiff.Days;
            
            GetSFHotelTravelDetails();
            Session.Remove("TentativeManifestCalendarDashboard");
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
        /// Author:             Josephine Gad
        /// Date Created:       08/Nov/2012
        /// Description:        Set the calendar link's URL
        /// ------------------------------------
        /// </summary>
        private void SetCalendarLink()
        {
            string sURL = "../CalendarPopUpForecast.aspx?dt=" + uoHiddenFieldDate.Value + "&bID=" + uoDropDownListHotel.SelectedValue;
            sURL += "&b=" + uoDropDownListHotel.SelectedItem.Text;
            sURL += "&p=" + uoDropDownListPortPerRegion.SelectedItem.Text;
            sURL += "&r=" + uoDropDownListRegion.SelectedItem.Text;
            uoHyperLinkCalendar.NavigateUrl = sURL;
        }
        /// <summary>
        /// Date Created:   08/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Calendar Room Count using session
        /// -------------------------------------------
        /// </summary>
        /// <returns></returns>
        protected List<CalendarRoomNeeded> GetCalendarRoomCount()
        {
            List<CalendarRoomNeeded> list = new List<CalendarRoomNeeded>();
            CalendarBLL calendar = new CalendarBLL();

            try
            {
                Session.Remove("CalendarRoomNeeded");

                list = calendar.GetCalendarRoomNeeded(GlobalCode.Field2String(Session["UserName"]), 
                    GlobalCode.Field2DateTime(uoTextBoxFrom.Text),
                    uoTextBoxTo.Text, GlobalCode.Field2Int(Session["Region"]), GlobalCode.Field2Int(Session["Port"]), 
                    GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue)
                    );
                Session["CalendarRoomNeeded"] = list;
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/Feb/2013
        /// Description:    Create Excel File for Count Summary
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFileCount(DataTable dt)
        {
            try
            {
                string sCaption = "";
                string sBranch;
                string sPort;
                string sRegion;

                sBranch = uoDropDownListHotel.SelectedItem.Text;
                sPort = uoDropDownListPortPerRegion.SelectedItem.Text;
                sRegion = uoDropDownListRegion.SelectedItem.Text;

                sBranch = (sBranch.ToLower().Contains("select") == true ? "" : sBranch);
                sPort = (sPort.ToLower().Contains("select") == true ? "" : sPort);
                sRegion = (sRegion.ToLower().Contains("select") == true ? "" : sRegion);

                if (sBranch != "")
                {
                    sCaption = sBranch;
                }
                if (sPort != "")
                {
                    sCaption += (sCaption == "" ? "" : " | ") + sPort;
                }
                if (sRegion != "")
                {
                    sCaption += (sCaption == "" ? "" : " | ") + sRegion;
                }

                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/Calendar/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "RoomCount_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcelCount(dt, strFileName, sCaption);
                OpenExcelFileCount(FileName, strFileName);
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
        /// Author:         Josephine Gad
        /// Date Created:   01/Feb/2013
        /// Description:    Create Excel File
        /// ---------------------------------------------
        /// Date Modifed:   04/Feb/2013
        /// Modifed By:     Josephine Gad
        /// (description)   Add fields override count, emergency and contracted room counts       
        /// -------------------------------------------
        /// Date Modifed:   14/Jan/2015
        /// Modifed By:     Josephine Monteza
        /// (description)   Set no. of col base from dtSource column count
        ///                 Get header name from column name
        /// -------------------------------------------
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strFileName"></param>
        /// <param name="sCaption"></param>
        public static void CreateExcelCount(DataTable dtSource, string strFileName, string sCaption)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = dtSource.Columns.Count;
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
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Room Needed");

                    // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                    xtwWriter.WriteStartElement("Table");

                    int iRow = dtSource.Rows.Count + 15;

                    xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                    xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                    xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                    xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                    xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "100");

                    //Title caption
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");


                    xtwWriter.WriteStartElement("Cell");
                    //xtwWriter.WriteAttributeString("ss", "MergeAcross", null, "2");
                    //xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                    // xxx
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    // Write content of cell
                    xtwWriter.WriteValue(sCaption);

                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();

                    xtwWriter.WriteEndElement();


                    //Header
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                    int i = 1;
                    string sColName;
                    foreach (DataColumn Header in dtSource.Columns)
                    {
                        if (i <= iColCount && i != 1)
                        {
                            xtwWriter.WriteStartElement("Cell");
                            // xxx
                            xtwWriter.WriteStartElement("Data");
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                            sColName = Header.ColumnName.Trim();
                            if (sColName.EndsWith("1"))
                            {
                                sColName = sColName.Replace("1", "").Trim();
                            }
                                                        
                            xtwWriter.WriteValue(sColName);

                            // Write content of cell
                            //if (Header.ColumnName == "sDate")
                            //{
                            //    xtwWriter.WriteValue("Date");
                            //}
                            //else
                            //{ 
                                
                            //}
                            //else if (Header.ColumnName == "SingleCount")
                            //{
                            //    xtwWriter.WriteValue("SGL Needed");
                            //}
                            //else if (Header.ColumnName == "DoubleCount")
                            //{
                            //    xtwWriter.WriteValue("DBL Needed");
                            //}
                            //else if (Header.ColumnName == "TotalNeededRoom")
                            //{
                            //    xtwWriter.WriteValue("Total Room Needed");
                            //}
                           
                            //else if (Header.ColumnName == "TotalContractRoom")
                            //{
                            //    xtwWriter.WriteValue("Total Contracted Room");
                            //}
                           
                            //else if (Header.ColumnName == "TotalOverrideRoom")
                            //{
                            //    xtwWriter.WriteValue("Total Override Room");
                            //}
                          
                            //else if (Header.ColumnName == "TotalRoom")
                            //{
                            //    xtwWriter.WriteValue("Total Room Block (Contracted + Override)");
                            //}
                           
                            xtwWriter.WriteEndElement();
                            xtwWriter.WriteEndElement();
                        }
                        i++;
                    }
                    xtwWriter.WriteEndElement();


                    // Run through all rows of data source
                    foreach (DataRow row in dtSource.Rows)
                    {
                        // <Row>
                        xtwWriter.WriteStartElement("Row");

                        i = 1;
                        // Run through all cell of current rows
                        foreach (object cellValue in row.ItemArray)
                        {
                            if (i <= iColCount && i != 1)
                            {
                                // <Cell>
                                xtwWriter.WriteStartElement("Cell");

                                // <Data ss:Type="String">xxx</Data>
                                xtwWriter.WriteStartElement("Data");

                                if (dtSource.Columns[i - 1].Caption.ToUpper() == "DATE")
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                }
                                else
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                // Write content of cell
                                xtwWriter.WriteValue(cellValue);

                                // </Data>
                                xtwWriter.WriteEndElement();

                                // </Cell>
                                xtwWriter.WriteEndElement();
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
        /// Modified By:    Josephine Gad
        /// Date Modified:  08/Nov/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFileCount(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/Calendar/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoLinkButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
    }
}

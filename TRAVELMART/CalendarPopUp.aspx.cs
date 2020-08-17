using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Text;

namespace TRAVELMART
{
    public partial class CalendarPopUp : System.Web.UI.Page
    {
        #region DEFINITIONS
        CalendarBLL calendar = new CalendarBLL();
        #endregion
        #region EVENTS
        /// <summary>
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// Date Modified:  07/Jan/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Add int iNoOfdays = TMSettings.NoOfDays instead of using 10
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime sDateFrom = new DateTime();
                //if (GlobalCode.Field2String(Session["DateFrom"]) == "")
                //{
                if (GlobalCode.Field2String(Request.QueryString["dt"]) == "")
                {
                    sDateFrom = GlobalCode.Field2DateTime(Session["DateFrom"]);
                }
                else
                {
                    sDateFrom = GlobalCode.Field2DateTime(Request.QueryString["dt"]);
                }

                Session["DateFrom"] = sDateFrom.ToShortDateString();
                //}

                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                CalendarSettings();

                uoCalendarDashboard.SelectedDate = sDateFrom;
                uoCalendarDashboard.VisibleDate = sDateFrom;

                int iNoOfdays = TMSettings.NoOfDays;
                DateTime dtRange = DateTime.Now.AddDays(iNoOfdays);
                uoHiddenFielddateRange.Value = dtRange.ToShortDateString();
            }
            Session.Remove("TentativeManifestCalendarDashboard");
            Session.Remove("OnOffCalendarDashboard");
            uoCalendarDashboard.Visible = true;

        }
        /// <summary>
        /// Date Modified:  19/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:  01/Jul/2013
        /// Modified By:    Josephine Gad
        /// (description)   Disable cell if date is more than 10 days from now for Hotel Vendor only
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoCalendarDashboard_DayRender(object sender, DayRenderEventArgs e)
        {
            //DataTable dt = null;
            List<ManifestOnOffCalendar> list = new List<ManifestOnOffCalendar>();
            List<ManifestCalendar> calList = new List<ManifestCalendar>();

            try
            {
                DateTime day = e.Day.Date;
                if (
                    //GlobalCode.Field2String(Session["strPrevPage"]).Contains("HotelManifest") ||
                    GlobalCode.Field2String(Session["strPrevPage"]).Contains("HotelTentativeManifest"))
                {
                    calList = GetManifest();
                    var filteredRow = (from a in calList
                                       where a.colDate.Equals(day) //a.Field<DateTime>("colDate").Equals(day)
                                       select a).ToList();

                    //Recover the resulting datatable
                    //dtCurrentDate = filteredRow.CopyToDataTable();

                    //if (dtCurrentDate.Rows[0]["TotalCount"].ToString() != "0")
                    //if (filteredRow[0]["TotalCount"].ToString() != "0")
                    if (filteredRow[0].TotalCount.ToString() != "0")
                    {
                        Label lblTotalCount = new Label();
                        lblTotalCount.Text = "<br/>Count:" + filteredRow[0].TotalCount.ToString() + "<br/>";
                        lblTotalCount.ForeColor = Color.Red;
                        lblTotalCount.Font.Size = 8;
                        e.Cell.Controls.Add(lblTotalCount);
                        e.Cell.ID = day.ToString("MM_dd_yyyy");
                    }
                }
                else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("HotelExceptionBookingsDays"))
                {

                    list = GetOnOffCalendar();
                    
                    //Filter Rows on collection
                    var filteredRow = (from a in list
                                       where a.colDate.Equals(day) //a.Field<DateTime>("colDate").Equals(day)
                                       select a).ToList();

                    //For Crew Assist Only
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist)
                    {
                        DisableDate(e, filteredRow);
                    }
                }
                else
                {

                    if (uoHiddenFieldRole.Value != TravelMartVariable.RoleCrewAssist)
                    {
                        list = GetOnOffCalendar();
                        
                        //Filter Rows on collection
                        var filteredRow = (from a in list
                                           where a.colDate.Equals(day) //a.Field<DateTime>("colDate").Equals(day)
                                           select a).ToList();

                                                
                        if (filteredRow.Count > 0)
                        {
                            //For Hotel Vendor Only
                            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                            {
                                DisableDate(e, filteredRow);
                            }
                            //For other Roles
                            else 
                            {
                                // Count Settings START
                                #region CountSettings
                                if (filteredRow[0].ONCount.ToString() != "0" || filteredRow[0].OffCount.ToString() != "0")
                                {
                                    Label lblOnCount = new Label();
                                    Label lblOffCount = new Label();
                                    if (filteredRow[0].ONCount.ToString() != "0")
                                    {
                                        lblOnCount.Text = "<br/>ON:" + filteredRow[0].ONCount.ToString();
                                        lblOnCount.ForeColor = Color.Green;
                                        lblOnCount.Font.Size = 8;
                                        e.Cell.Controls.Add(lblOnCount);
                                    }
                                    else
                                    {
                                        lblOnCount.Text = "<br/>";
                                        lblOnCount.Font.Size = 8;
                                        e.Cell.Controls.Add(lblOnCount);
                                    }
                                    if (filteredRow[0].OffCount.ToString() != "0")
                                    {
                                        lblOffCount.Text = "</br>OFF:" + filteredRow[0].OffCount.ToString();
                                        lblOffCount.ForeColor = Color.Red;
                                        lblOffCount.Font.Size = 8;
                                        e.Cell.Controls.Add(lblOffCount);
                                    }
                                    else
                                    {
                                        lblOffCount.Text = "</br>";
                                        lblOffCount.Font.Size = 8;
                                        e.Cell.Controls.Add(lblOffCount);
                                    }
                                    e.Cell.ID = day.ToString("MM_dd_yyyy");
                                }
                                #endregion CountSettings
                                // Count Settings END
                            }
                        }
                    }
                }
                e.Cell.Width = 50;
                e.Cell.Height = 50;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DisableDate(DayRenderEventArgs e, List<ManifestOnOffCalendar> filteredRow)
        {
            DateTime day = e.Day.Date;

            string sDateRange = uoHiddenFielddateRange.Value;
            string sDateCell = day.ToShortDateString();

            DateTime dtRange = GlobalCode.Field2DateTime(sDateRange);
            DateTime dtCell = GlobalCode.Field2DateTime(sDateCell);

            if (dtCell <= dtRange)
            {
                // Count Settings START
                #region CountSettings
                if (filteredRow[0].ONCount.ToString() != "0" || filteredRow[0].OffCount.ToString() != "0")
                {
                    Label lblOnCount = new Label();
                    Label lblOffCount = new Label();
                    if (filteredRow[0].ONCount.ToString() != "0")
                    {
                        lblOnCount.Text = "<br/>ON:" + filteredRow[0].ONCount.ToString();
                        lblOnCount.ForeColor = Color.Green;
                        lblOnCount.Font.Size = 8;
                        e.Cell.Controls.Add(lblOnCount);
                    }
                    else
                    {
                        lblOnCount.Text = "<br/>";
                        lblOnCount.Font.Size = 8;
                        e.Cell.Controls.Add(lblOnCount);
                    }
                    if (filteredRow[0].OffCount.ToString() != "0")
                    {
                        lblOffCount.Text = "</br>OFF:" + filteredRow[0].OffCount.ToString();
                        lblOffCount.ForeColor = Color.Red;
                        lblOffCount.Font.Size = 8;
                        e.Cell.Controls.Add(lblOffCount);
                    }
                    else
                    {
                        lblOffCount.Text = "</br>";
                        lblOffCount.Font.Size = 8;
                        e.Cell.Controls.Add(lblOffCount);
                    }
                    e.Cell.ID = day.ToString("MM_dd_yyyy");
                }
                #endregion CountSettings
                // Count Settings END
            }
            else
            {
                e.Cell.Enabled = false;
                e.Day.IsSelectable = false;
            }
        }

        /// <summary>
        /// Date Modified:  23/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add  uoCalendarDashboard.VisibleDate = dtSelected to avoid error "out of index"
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoCalendarDashboard_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dtSelected = uoCalendarDashboard.SelectedDate;
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            {
                DateTime dtRange = GlobalCode.Field2DateTime(uoHiddenFielddateRange.Value);
                if (dtSelected >= dtRange)
                {
                    return;
                }
            }

            Session["DateFrom"] = dtSelected.ToShortDateString();
            uoCalendarDashboard.VisibleDate = dtSelected;

            if (uoTableFilters.Visible == true)
            {
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator)
                {
                    Session["Port"] = uoDropDownListPort.SelectedValue;
                    Session["Region"] = uoDropDownListRegion.SelectedValue;
                }
            }
            //Session.Remove("OnOffCalendarDashboard");
            OpenParentPage();
        }

        protected void uoCalendarDashboard_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            Session["DateFrom"] = e.NewDate.ToString("MM/dd/yyyy");

            if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Manifest"))
            {
                GetManifest();
            }
            //else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Exceptions.aspx"))
            //{ 
            //    Session.Remove("ExceptionCalendarDashboard");
            //    GetExceptions();
            //}
            else
            {
                Session.Remove("OnOffCalendarDashboard");
                GetOnOffCalendar();
            }
        }
        protected void uoDropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (uoDropDownListType.SelectedValue == "1") //Crew Admin
            {
                ucLabelVesselOrHotel.Text = "Ship: ";
                BindVessel();
            }
            else if (uoDropDownListType.SelectedValue == "2") //Hotel Vendor
            {
                ucLabelVesselOrHotel.Text = "Hotel Branch: ";
                BindHotel();
            }
            else
            {
                ucLabelVesselOrHotel.Visible = false;
            }
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPortList();
            if (uoDropDownListType.SelectedValue == "1") //Crew Admin
            {
                BindVessel();
            }
            else if (uoDropDownListType.SelectedValue == "2") //Hotel Vendor
            {
                BindHotel();
            }
            

        }
        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListType.SelectedValue == "1") //Crew Admin
            {
                BindVessel();
            }
            else if (uoDropDownListType.SelectedValue == "2") //Hotel Vendor
            {
                BindHotel();
            }
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Date Create:    19/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   Export list to excel
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                List<ManifestOnOffCalendar> list = new List<ManifestOnOffCalendar>();
                list = GetOnOffCalendarExport();

                if (list.Count > 0)
                {
                    dt = getDataTable(list);
                    CreateFile(dt);
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

        #region METHODS
        /// <summary>
        /// Date Modified:  19/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        /// -------------------------------------------
        /// </summary>
        /// <returns></returns>
        protected List<ManifestOnOffCalendar> GetOnOffCalendar()
        {
            List<ManifestOnOffCalendar> list = new List<ManifestOnOffCalendar>();

            try
            {
                int RegionID = 0;
                int PortID = 0;
                int VesselID = 0;
                int HotelID = 0;

                if (uoTableFilters.Visible == true)
                {
                    if (uoDropDownListType.SelectedValue == "1")
                    {
                        VesselID = GlobalCode.Field2Int(uoDropDownListVesselOrHotel.SelectedValue);
                    }
                    else if (uoDropDownListType.SelectedValue == "2")
                    {
                        HotelID = GlobalCode.Field2Int(uoDropDownListVesselOrHotel.SelectedValue);
                    }

                    RegionID = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue);
                    PortID = GlobalCode.Field2Int(uoDropDownListPort.SelectedValue);
                }
                else
                {
                    RegionID = GlobalCode.Field2Int(Session["Region"]);
                    PortID = GlobalCode.Field2Int(Session["Port"]);
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                    {
                        HotelID = GlobalCode.Field2Int(Session["UserBranchID"]);
                    }
                    else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Immigration/Immigration.aspx"))
                    {
                        HotelID = GlobalCode.Field2Int(Session["SeaportID"]);
                    }

                    else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("PortAgent/PortAgentVehicleManifest.aspx"))
                    {
                        HotelID = GlobalCode.Field2Int(Session["PortAgentID"]);
                    }
                    else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("PortAgent/PortAgentHotelManifest.aspx"))
                    {
                        HotelID = GlobalCode.Field2Int(Session["PortAgentID"]);
                    }

                    else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("PortAgent/PortAgentRequest.aspx"))
                    {
                        HotelID = GlobalCode.Field2Int(Session["PortAgentID"]);
                    }

                    else if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                    {
                        HotelID = GlobalCode.Field2Int(Session["VehicleID"]);
                    }                    
                    else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Vehicle/VehicleManifest.aspx"))
                    {
                        HotelID = GlobalCode.Field2Int(Session["VehicleID"]);
                    }
                    else if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Vehicle/VehicleManifestByVendor.aspx"))
                    {
                        HotelID = GlobalCode.Field2Int(Session["VehicleID"]);
                    }                                       

                    else
                    {
                        HotelID = GlobalCode.Field2Int(Session["Hotel"]);
                    }
                }

                if (Session["OnOffCalendarDashboard"] == null)
                {
                    list = calendar.LoadOnOffCalendar(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])),
                       RegionID, PortID,
                        VesselID, HotelID,
                        GlobalCode.Field2String(Session["strPrevPage"]), 
                        GlobalCode.Field2TinyInt(uoDropDownListType.SelectedValue)
                        );
                    Session.Remove("OnOffCalendarDashboard");
                    Session["OnOffCalendarDashboard"] = list;
                    return list;
                }
                else
                {
                    list = (List<ManifestOnOffCalendar>)Session["OnOffCalendarDashboard"];
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  19/Dec/2012
        /// Created By:    Josephine Gad
        /// (description)  Get the list to export
        /// -------------------------------------------
        /// </summary>
        /// <returns></returns>
        protected List<ManifestOnOffCalendar> GetOnOffCalendarExport()
        {
            List<ManifestOnOffCalendar> list = new List<ManifestOnOffCalendar>();

            if (Session["OnOffCalendarDashboard"] != null)
            {
                list = (List<ManifestOnOffCalendar>)Session["OnOffCalendarDashboard"];
            }
            else
            {
                list = calendar.LoadOnOffCalendarExport(GlobalCode.Field2String(Session["UserName"]));
                Session.Remove("OnOffCalendarDashboard");
                Session["OnOffCalendarDashboard"] = list;
            }
            return list;
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 22/03/2012
        /// Description: get manifest count
        /// </summary>
        /// <returns></returns>
        private List<ManifestCalendar> GetManifest()
        {
            List<ManifestCalendar> calList = new List<ManifestCalendar>();
            if (Session["TentativeManifestCalendarDashboard"] == null)
            {
                DateTime day = GlobalCode.Field2DateTime(Session["DateFrom"]);
                string prevPage = GlobalCode.Field2String(Session["strPrevPage"]);
                if (prevPage.Contains("HotelManifest3"))
                {
                    LockedManifestBLL lockedBLL = new LockedManifestBLL();
                    calList = lockedBLL.LoadLockedManifestCalendar(GlobalCode.Field2String(Session["UserName"]), day, GlobalCode.Field2Int(Session["ManifestHrs"]),
                        GlobalCode.Field2Int(Session["Hotel"]));
                }
                else
                {
                    calList = HotelManifestBLL.GetTentativeManifestDashboard(day.ToString("MM/dd/yyyy"),
                               "", GlobalCode.Field2String(Session["UserName"]), "1",
                               "1", "", "0",
                               "0", "0", "0",
                              GlobalCode.Field2Int(Session["Region"]).ToString(), GlobalCode.Field2Int(Session["Country"]).ToString(),
                              GlobalCode.Field2Int(Session["City"]).ToString(), GlobalCode.Field2Int(Session["Port"]).ToString(),
                              GlobalCode.Field2Int(Session["Hotel"]).ToString(), "0",
                              GlobalCode.Field2String(Session["UserRole"]), GlobalCode.Field2Int(Session["ManifestHrs"]).ToString());
                }
                Session.Remove("TentativeManifestCalendarDashboard");
                Session["TentativeManifestCalendarDashboard"] = calList;
                return calList;
            }
            else
            {
                calList = (List<ManifestCalendar>)Session["TentativeManifestCalendarDashboard"];
                return calList;
            }
        }
        private void OpenParentPage()
        {

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_uoHiddenFieldPopupCalendar\").val(\"1\"); ";
            sScript += " window.parent.RefreshPageFromPopup(); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Nov/2012
        /// Description:    Set the controls of Crew Admin
        /// </summary>
        private void CrewAdminViewTypeSettings()
        {
            ucLabelVesselOrHotel.Visible = true;
            uoDropDownListVesselOrHotel.Visible = true;
            ucLabelVesselOrHotel.Text = "Ship: ";

        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Nov/2012
        /// Description:    Set calendar settings
        /// </summary>
        private void CalendarSettings()
        {
            string sURL = GlobalCode.Field2String(Session["strPrevPage"]);

            //if (sURL.Contains("HotelManifest") ||
            //   sURL.Contains("HotelTentativeManifest") ||
            //   sURL.Contains("HotelLockedManifest"))
            //{
            //    uoTableFilters.Visible = false;
            //}
            if (sURL.Contains("HotelDashboardRoomType4"))
            {
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator)
                {

                    uoTableFilters.Visible = true;
                    BindRegionList();
                    BindPortList();
                    uoDropDownListType.SelectedValue = "1"; //Crew Admin as default view
                    ucLabelVesselOrHotel.Text = "Ship: ";
                    BindVessel();
                }
                else
                {
                    uoTableFilters.Visible = false;                
                }
            }
            else
            {
                uoTableFilters.Visible = false;
            }
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       14/Nov/2012
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
        /// Date Created:   14/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Load Port List using session
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

                uoDropDownListPort.Items.Clear();
                //ListItem item = new ListItem("--SELECT PORT--", "0");
                //uoDropDownListPort.Items.Add(item);
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
                uoDropDownListPort.Items.Insert(0, new ListItem("--Select PORT--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  14/Nov/2012
        /// Description:    Get Hotel Branches
        /// ----------------------------------------------
        /// </summary>
        private void BindHotel()
        {
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, uoDropDownListRegion.SelectedValue,
                    uoDropDownListPort.SelectedValue, "0", "0");

                int iRowCount = list.Count;
                if (iRowCount == 1)
                {
                    Session["Hotel"] = list[0].HotelIDString;//dt.Rows[0]["BranchID"].ToString();
                }
                if (iRowCount > 0)
                {
                    uoDropDownListVesselOrHotel.Items.Clear();
                    uoDropDownListVesselOrHotel.DataSource = list;
                    uoDropDownListVesselOrHotel.DataTextField = "HotelNameString";
                    uoDropDownListVesselOrHotel.DataValueField = "HotelIDString";
                    uoDropDownListVesselOrHotel.DataBind();
                    uoDropDownListVesselOrHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    uoDropDownListVesselOrHotel.SelectedValue = "0";

                    //if (GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue) > 0)
                    //{
                    //    if (uoDropDownListVesselOrHotel.Items.FindByValue("-1") == null)
                    //    {
                    //        uoDropDownListVesselOrHotel.Items.Insert(1, new ListItem("--Select ALL Hotel--", "-1"));
                    //    }
                    //}
                    //else
                    //{
                    //    if (uoDropDownListVesselOrHotel.Items.FindByValue("-1") != null)
                    //    {
                    //        uoDropDownListVesselOrHotel.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
                    //    }
                    //}
                    uoDropDownListVesselOrHotel.Enabled = true;

                    if (uoDropDownListVesselOrHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                    {
                        uoDropDownListVesselOrHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                    }
                }
                else
                {
                    uoDropDownListVesselOrHotel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  14/Nov/2012
        /// Description:   Get Vessel by Port by Date
        /// ----------------------------------------------
        /// </summary>
        private void BindVessel()
        {
            List<VesselDTO> list = new List<VesselDTO>();
            list = VesselBLL.GetVesselList(uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["DateFrom"]),
                    "", uoDropDownListRegion.SelectedValue, "0",
                    "0", uoDropDownListPort.SelectedValue, uoHiddenFieldRole.Value, true);

            uoDropDownListVesselOrHotel.Items.Clear();
            ListItem item = new ListItem("--Select Ship--", "0");
            uoDropDownListVesselOrHotel.Items.Add(item);
            uoDropDownListVesselOrHotel.DataSource = list;
            uoDropDownListVesselOrHotel.DataTextField = "VesselNameString";
            uoDropDownListVesselOrHotel.DataValueField = "VesselIDString";
            uoDropDownListVesselOrHotel.DataBind();
        }
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
        /// Created By:    Josephine Gad
        /// Date Created:  19/Dec/2012
        /// Description:   Create the excel file
        /// ----------------------------------------------
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/Calendar/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "OnOFFCount_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                string sCaption = "";
                if (GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue) > 0)
                {
                    sCaption = uoDropDownListRegion.SelectedItem.Text;
                }
                if (GlobalCode.Field2Int(uoDropDownListPort.SelectedValue) > 0)
                {
                    if (sCaption != "")
                    {
                        sCaption += " - " + uoDropDownListPort.SelectedItem.Text;
                    }
                    else
                    {
                        sCaption = uoDropDownListPort.SelectedItem.Text;
                    }
                }
                if (GlobalCode.Field2Int(uoDropDownListVesselOrHotel.SelectedValue) > 0)
                {
                    if (sCaption != "")
                    {
                        sCaption += " - " + uoDropDownListVesselOrHotel.SelectedItem.Text;
                    }
                    else
                    {
                        sCaption = uoDropDownListVesselOrHotel.SelectedItem.Text;
                    }
                }

                CreateExcel(dt, strFileName, sCaption);
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
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  19/Dec/2012
        /// Description:   Create the excel file
        /// ----------------------------------------------
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strFileName"></param>
        /// <param name="sCaption"></param>
        public static void CreateExcel(DataTable dtSource, string strFileName, string sCaption)
        {
            try
            {  // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = 3;
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
                    xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

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
                    foreach (DataColumn Header in dtSource.Columns)
                    {
                        if (i <= iColCount)
                        {
                            xtwWriter.WriteStartElement("Cell");
                            // xxx
                            xtwWriter.WriteStartElement("Data");
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            // Write content of cell
                            if (Header.ColumnName == "sDate")
                            {
                                xtwWriter.WriteValue("Date");
                            }
                            else if (Header.ColumnName == "ONCount")
                            {
                                xtwWriter.WriteValue("ON");
                            }
                            else if (Header.ColumnName == "OffCount")
                            {
                                xtwWriter.WriteValue("OFF");
                            }
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

                                if (dtSource.Columns[i - 1].Caption.ToUpper() == "ONCount")
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                else if (dtSource.Columns[i - 1].Caption.ToUpper() == "OffCount")
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                else
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
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
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/Calendar/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
        #endregion      
    }
}

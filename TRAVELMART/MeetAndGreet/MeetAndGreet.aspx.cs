using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.Security;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;

namespace TRAVELMART
{
    public partial class MeetAndGreet : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  15/08/2012
        /// Description:    Default page for role Meet & Greet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "";
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleMeetGreet)
                {
                    strLogDescription = "Meet & Greet Page Viewed";
                }
                else if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    strLogDescription = "Service Provider Page Viewed";
                }
                else
                {
                    strLogDescription = "Meet & Greet or Service Provider Page Viewed";                
                }

                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));


                Session["strPrevPage"] = Request.RawUrl;
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;

                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {
                    Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                if (Session["ViewType"] != null)
                {
                    uoRadioButtonListView.SelectedValue = GlobalCode.Field2String(Session["ViewType"]);
                }
                else
                {
                    uoRadioButtonListView.SelectedValue = "3";
                }
                ControlSettings();

                BindPort();
                SetSelectedPort();
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

                if (uoHiddenFieldAirpordID.Value != "0")
                {
                    ListViewHeaderAir.DataSource = null;
                    ListViewHeaderAir.DataBind();
                }
                else
                {
                    ListViewHeaderSea.DataSource = null;
                    ListViewHeaderSea.DataBind();
                }
                uoHiddenFieldLoadType.Value = "1";

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
                if (uoHiddenFieldPopupCalendar.Value == "1")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                    SetSelectedPort();
                }
            }
        }
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  15/08/2012
        /// Description:   Refresh List View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "2";
            SetSelectedPort();
        }
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  16/08/2012
        /// Description:   Refresh Port List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoRadioButtonListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "2";
            BindPort();
            SetSelectedPort();
        }
        protected void uoObjectDataSourceTR_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["LoadType"] = uoHiddenFieldLoadType.Value;
            e.InputParameters["sUser"] = uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["sPortID"] = uoHiddenFieldSeaportID.Value;
            e.InputParameters["sAirportID"] = uoHiddenFieldAirpordID.Value;
            e.InputParameters["dDate"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);

            e.InputParameters["VesselID"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["FilterByName"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["SeafarerID"] = uoTextBoxFilter.Text;

            e.InputParameters["NationalityID"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["RankID"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;

            e.InputParameters["iViewType"] = GlobalCode.Field2TinyInt(uoRadioButtonListView.SelectedValue);
            e.InputParameters["SortBy"] = uoHiddenFieldSortBy.Value;
        }
        protected void uoListViewPagerAir_PreRender(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   16/08/2012
        /// Description:    ListView command - tag seafarer if arrived
        /// -----------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  19/Jul/2013
        /// Description:    Add ucLabelReclocLV, ucLabelE1IDLV, uoHiddenFieldStatusLV
        /// -----------------------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoListViewTR_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Tag")
            {
                uoHiddenFieldLoadType.Value = "2";
                string arg = e.CommandArgument.ToString();
                string[] args = arg.Split(":".ToCharArray());

                Label ucLabelReclocLV = (Label)e.Item.FindControl("ucLabelRecloc");
                Label ucLabelE1IDLV = (Label)e.Item.FindControl("ucLabelE1ID");
                HiddenField uoHiddenFieldStatusLV = (HiddenField)e.Item.FindControl("uoHiddenFieldStatus");

                if (args.Count() > 1)
                {
                    TagSeafarer(args[0].ToString(), args[1].ToString(), args[2].ToString(), args[3].ToString(),
                        ucLabelReclocLV.Text, ucLabelE1IDLV.Text, uoHiddenFieldStatusLV.Value);
                }
            }
        }
        protected void uoRadioButtonListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ViewType"] = uoRadioButtonListView.SelectedValue;
            uoHiddenFieldLoadType.Value = "2";
            SetSelectedPort();
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            SetSelectedPort();
        }
        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                dt = TravelRequestBLL.GetMeetGreetTravelRequestExport(0, uoHiddenFieldUser.Value,
                    uoHiddenFieldRole.Value, uoHiddenFieldSeaportID.Value, uoHiddenFieldAirpordID.Value, uoHiddenFieldSortBy.Value);
               
                if (dt.Rows.Count > 0)
                {
                    CreateFile(dt);
                }
                else
                {
                    AlertMessage("No Record to Export!");
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

        #region "Functions"
        
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  16/08/2012
        /// Description:   Tag Seafarer
        /// ---------------------------------
        /// Modified By:    Mabejar 
        /// Date Modified:  10/04/2013
        /// Description:    Add TagTime
        /// ---------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  19/Jul/2013
        /// Description:    Add Rec Loc, E1ID, Status On/Off and Audit Trail data
        /// </summary>
        /// <param name="sIdBigint"></param>
        /// <param name="sTRId"></param>
        /// <param name="sAirport"></param>
        /// <param name="sPort"></param>
        private void TagSeafarer(string sIdBigint, string sTRId, string sAirport, string sPort, string sRecLoc, string sE1Id, string sStatusOnOff)
        {
            string sUser = uoHiddenFieldUser.Value;
            string sRole = uoHiddenFieldRole.Value;
            string sTagTime = uoHiddenFieldTagTime.Value;

            string strLogDescription = "Tag Seafarer by Meet & Greet";
            string strFunction = "TagSeafarer";
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();
            
            SeafarerTravelBLL.InsertTag(sIdBigint, sTRId, sUser, sRole, sAirport, sPort, "0", sTagTime,                
                sRecLoc, sE1Id, sStatusOnOff, strLogDescription, strFunction, Path.GetFileName(Request.Path),
                 TimeZone.CurrentTimeZone.StandardName.ToString(), CommonFunctions.GetDateTimeGMT(dateNow));
            if (sAirport != "0")
            {
                BindAirportManifest();
            }
            else if (sPort != "0")
            {
                BindSeaportManifest();
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Redirect to Login page if invalid user
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
            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }
            //ListView1.DataSource = null;
            //ListView1.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Setup control if meet & greet for Airport or Seaport
        ///                 Or Port Agent
        /// </summary>
        private void ControlSettings()
        {
            bool bIsAirport = false;
            bool bIsSeaport = false;

            uoHiddenFieldIsSeaport.Value = "0";
            uoHiddenFieldIsAirport.Value = "0";

            //List<UserAccountList> list = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
            if (User.IsInRole(TravelMartVariable.RolePortSpecialist))
            {
                bIsSeaport = true;
                uoHiddenFieldIsSeaport.Value = "1";
            }
            if (User.IsInRole(TravelMartVariable.RoleMeetGreet))
            {
                bIsAirport = true;
                uoHiddenFieldIsAirport.Value = "1";
            }

            BindPort();
            if (bIsAirport && bIsSeaport)
            {
                uoRadioButtonListPort.Visible = true;
                ucLabelPort.Visible = false;

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleMeetGreet)
                {
                    uoLabelTitle.Text = "Meet & Greet";
                    uoRadioButtonListPort.SelectedValue = "1";
                    uoTRStatus.Visible = false;
                    BindAirportManifest();
                }
                else // (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    uoLabelTitle.Text = "Port Agent";
                    uoRadioButtonListPort.SelectedValue = "2";
                    uoTRStatus.Visible = true;
                    BindSeaportManifest();
                }
            }
            else if (bIsAirport)
            {
                uoRadioButtonListPort.Visible = false;
                ucLabelPort.Visible = true;
                ucLabelPort.Text = "Airport: ";

                uoLabelTitle.Text = "Meet & Greet";
                uoTRStatus.Visible = false;
                BindAirportManifest();
            }
            else if (bIsSeaport)
            {
                uoRadioButtonListPort.Visible = false;
                ucLabelPort.Visible = true;
                ucLabelPort.Text = "Seaport: ";

                uoLabelTitle.Text = "Port Agent";
                uoTRStatus.Visible = true;
                BindSeaportManifest();
            }
        }
        ///
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Bind Port in dropdown depends if Airport or Seaport
        /// </summary>
        private void BindPort()
        {
            if (uoHiddenFieldIsAirport.Value == "1" && uoRadioButtonListPort.SelectedValue == "1")
            {
                BindAirport();
                uoHiddenFieldAirpordID.Value = GlobalCode.Field2String(Session["Airport"]);
                uoHiddenFieldSeaportID.Value = "0";
            }
            else if (uoHiddenFieldIsSeaport.Value == "1" && uoRadioButtonListPort.SelectedValue == "2")
            {
                BindSeaport();
                uoHiddenFieldAirpordID.Value = "0";
                uoHiddenFieldSeaportID.Value = GlobalCode.Field2String(Session["Port"]);
            }
            else if (uoHiddenFieldIsAirport.Value == "1" && uoHiddenFieldIsSeaport.Value == "0")
            {
                BindAirport();
                uoHiddenFieldAirpordID.Value = GlobalCode.Field2String(Session["Airport"]);
                uoHiddenFieldSeaportID.Value = "0";
            }
            else if (uoHiddenFieldIsAirport.Value == "0" && uoHiddenFieldIsSeaport.Value == "1")
            {
                BindSeaport();
                uoHiddenFieldAirpordID.Value = "0";
                uoHiddenFieldSeaportID.Value = GlobalCode.Field2String(Session["Port"]);
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Set hidden field value based from selected port
        /// </summary>
        private void SetSelectedPort()
        {
            if (uoHiddenFieldIsAirport.Value == "1" && uoRadioButtonListPort.SelectedValue == "1")
            {
                uoHiddenFieldAirpordID.Value = uoDropDownListPort.SelectedValue;
                Session["Airport"] = uoHiddenFieldAirpordID.Value;
                uoHiddenFieldSeaportID.Value = "0";
                BindAirportManifest();
            }
            else if (uoHiddenFieldIsSeaport.Value == "1" && uoRadioButtonListPort.SelectedValue == "2")
            {
                uoHiddenFieldAirpordID.Value = "0";
                uoHiddenFieldSeaportID.Value = uoDropDownListPort.SelectedValue;
                Session["Port"] = uoHiddenFieldSeaportID.Value;
                BindSeaportManifest();
            }
            else if (uoHiddenFieldIsAirport.Value == "1" && uoHiddenFieldIsSeaport.Value == "0")
            {
                uoHiddenFieldAirpordID.Value = uoDropDownListPort.SelectedValue;
                Session["Airport"] = uoHiddenFieldAirpordID.Value;
                uoHiddenFieldSeaportID.Value = "0";
                BindAirportManifest();
            }
            else if (uoHiddenFieldIsAirport.Value == "0" && uoHiddenFieldIsSeaport.Value == "1")
            {
                uoHiddenFieldAirpordID.Value = "0";
                uoHiddenFieldSeaportID.Value = uoDropDownListPort.SelectedValue;
                Session["Port"] = uoHiddenFieldSeaportID.Value;
                BindSeaportManifest();
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Bind Seaport 
        /// </summary>
        private void BindSeaport()
        {
            List<PortList> port = new List<PortList>();
            port = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), "0", "0", uoHiddenFieldRole.Value);

            uoDropDownListPort.Items.Clear();
            if (port.Count > 0)
            {
                uoDropDownListPort.DataSource = port;
                uoDropDownListPort.DataTextField = "PortName";
                uoDropDownListPort.DataValueField = "PortId";
            }
            uoDropDownListPort.DataBind();
            if (port.Count == 1)
            {
                if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(port[0].PortId)) != null)
                {
                    uoDropDownListPort.SelectedValue = GlobalCode.Field2String(port[0].PortId);
                }
            }
            if (GlobalCode.Field2String(Session["Port"]) != "")
            {
                if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                {
                    uoDropDownListPort.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                }
            }
            uoDropDownListPort.Items.Insert(0, new ListItem("--Select Seaport--", "0"));
            Session["Port"] = uoDropDownListPort.SelectedValue;

        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/08/2012
        /// Description:    Bind Airport
        /// </summary>
        private void BindAirport()
        {
            List<Airport> air = new List<Airport>();
            air = MaintenanceViewBLL.GetAirportList(uoHiddenFieldUser.Value,
                uoHiddenFieldRole.Value, 0, 0, true);

            uoDropDownListPort.Items.Clear();
            if (air.Count > 0)
            {
                uoDropDownListPort.DataSource = air;
                uoDropDownListPort.DataTextField = "AirportName";
                uoDropDownListPort.DataValueField = "AirportID";
            }
            uoDropDownListPort.DataBind();
            if (air.Count == 1)
            {
                if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(air[0].AirportID)) != null)
                {
                    uoDropDownListPort.SelectedValue = GlobalCode.Field2String(air[0].AirportID);
                }
            }
            if (GlobalCode.Field2String(Session["Airport"]) != "")
            {
                if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(Session["Airport"])) != null)
                {
                    uoDropDownListPort.SelectedValue = GlobalCode.Field2String(Session["Airport"]);
                }
            }
            uoDropDownListPort.Items.Insert(0, new ListItem("--Select Airport--", "0"));
            Session["Airport"] = uoDropDownListPort.SelectedValue;
        }
        /// <summary>
        /// Author:         Marco Abejar
        /// Created Date:   22/03/2012
        /// (description):  Add Sorting
        /// </summary>
        protected void ListViewHeaderAir_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            BindAirportManifest();
        }
        protected void ListViewHeaderSea_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            BindSeaportManifest();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   16/08/2012
        /// (description):  Refresh listview
        /// </summary>
        private void BindAirportManifest()
        {
            uoDivAirport.Visible = true;
            uoDivSeaport.Visible = false;

            uoListViewTRAir.DataSource = null;
            uoListViewTRSea.DataSource = null;
            List<MeetGreetTravelRequestGenericClass> list = new List<MeetGreetTravelRequestGenericClass>();

            if (uoHiddenFieldLoadType.Value == "0" || uoHiddenFieldLoadType.Value == "2")
            {
                TravelRequestBLL bll = new TravelRequestBLL();
                list = bll.GetMeetGreetTravelRequestPage(GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value), uoHiddenFieldUser.Value,
                    uoHiddenFieldRole.Value, "0", uoHiddenFieldAirpordID.Value, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                    0, DataPager1.PageSize, "0", "1", "", "0", "0", "0", "", GlobalCode.Field2TinyInt(uoRadioButtonListView.SelectedValue)
                    ,uoHiddenFieldSortBy.Value);
                              
            }

            if (uoHiddenFieldLoadType.Value == "0")
            {
                BindControls(list);
            }
            else if (uoHiddenFieldLoadType.Value == "2")
            {
                BindCount(list);
            }
            uoListViewTRAir.DataSourceID = "uoObjectDataSourceTR";

            ListViewHeaderAir.DataSource = null;
            ListViewHeaderAir.DataBind();
        }
       
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   17/08/2012
        /// (description):  Refresh listview
        /// </summary>
        private void BindSeaportManifest()
        {
            uoDivAirport.Visible = false;
            uoDivSeaport.Visible = true;

            uoListViewTRAir.DataSource = null;
            uoListViewTRSea.DataSource = null;
            List<MeetGreetTravelRequestGenericClass> list = new List<MeetGreetTravelRequestGenericClass>();

            if (uoHiddenFieldLoadType.Value == "0" || uoHiddenFieldLoadType.Value == "2")
            {
                TravelRequestBLL bll = new TravelRequestBLL();
                list = bll.GetMeetGreetTravelRequestPage(GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value), uoHiddenFieldUser.Value, 
                    uoHiddenFieldRole.Value, uoHiddenFieldSeaportID.Value, "0", GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                    0, DataPager1.PageSize, "0", "1", "", "0", "0", "0", "", GlobalCode.Field2TinyInt(uoRadioButtonListView.SelectedValue)
                    ,uoHiddenFieldSortBy.Value);
            }
            if (uoHiddenFieldLoadType.Value == "0")
            {
                BindControls(list);
            }
            else if (uoHiddenFieldLoadType.Value == "2")
            {
                BindCount(list);
            }
            uoListViewTRSea.DataSourceID = "uoObjectDataSourceTR";

            ListViewHeaderSea.DataSource = null;
            ListViewHeaderSea.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   27/08/2012
        /// Description:    Bind filters and counts
        /// </summary>
        /// <param name="list"></param>
        private void BindControls(List<MeetGreetTravelRequestGenericClass> list)
        {
            uoDropDownListVessel.Items.Clear();
            uoDropDownListVessel.DataSource = list[0].VesselList;
            uoDropDownListVessel.DataTextField = "VesselNameString";
            uoDropDownListVessel.DataValueField = "VesselIDString";
            uoDropDownListVessel.DataBind();
            uoDropDownListVessel.Items.Insert(0, new ListItem("--Select Vessel--", "0"));
        
            uoDropDownListNationality.Items.Clear();
            uoDropDownListNationality.DataSource = list[0].NationalityList;
            uoDropDownListNationality.DataTextField = "Nationality";
            uoDropDownListNationality.DataValueField = "NationalityID";
            uoDropDownListNationality.DataBind();
            uoDropDownListNationality.Items.Insert(0, new ListItem("--Select Nationality--", "0"));

            uoDropDownListRank.Items.Clear();
            uoDropDownListRank.DataSource = list[0].RankList;
            uoDropDownListRank.DataTextField = "Rank";
            uoDropDownListRank.DataValueField = "RankID";
            uoDropDownListRank.DataBind();
            uoDropDownListRank.Items.Insert(0, new ListItem("--Select Rank--", "0"));

            uoDropDownListGender.Items.Clear();
            uoDropDownListGender.DataSource = list[0].GenderList;
            uoDropDownListGender.DataTextField = "Gender";
            uoDropDownListGender.DataValueField = "GenderID";
            uoDropDownListGender.DataBind();
            uoDropDownListGender.Items.Insert(0, new ListItem("--Select Gender--", "0"));

            uoLabelTaggedCount.Text = GlobalCode.Field2String(list[0].CountTagged);
            uoLabelUntaggedCount.Text = GlobalCode.Field2String(list[0].CountUntagged);
            uoLabelTotalCount.Text = GlobalCode.Field2String(list[0].CountAll);
        }
        private void BindCount(List<MeetGreetTravelRequestGenericClass> list)
        {
            if (list.Count > 0)
            {
                uoLabelTaggedCount.Text = GlobalCode.Field2String(list[0].CountTagged);
                uoLabelUntaggedCount.Text = GlobalCode.Field2String(list[0].CountUntagged);
                uoLabelTotalCount.Text = GlobalCode.Field2String(list[0].CountAll);
            }
        }
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/MeetGreetAndPortAgent/");
                string sDateNow = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDate = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");
                string FileName = "";
                string[] sPortArr = uoDropDownListPort.SelectedItem.Text.Split("-".ToCharArray());

                if (uoHiddenFieldIsAirport.Value == "1")
                {
                    FileName = "MeetAndGreet";
                }
                else
                {
                    FileName = "PortAgent";
                }
                FileName += "Manifest_" + sDate + "_" + sDateNow + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName, sPortArr[0].ToString().TrimEnd());
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
        /// Modified By:    Josephine Gad
        /// Date Modified:  29/Aug/2013
        /// Description:    Add groupings using lines
        ///                 Use blank E1 ID if not the 1st line of the group
        /// ------------------------------------------------
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName, string sPort)
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


                    //Style for for group with border
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s63">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s63");

                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement(); //End of Alignment
                    xtwWriter.WriteStartElement("Borders");
                    xtwWriter.WriteStartElement("Border");
                    xtwWriter.WriteAttributeString("ss", "Position", null, "Top");
                    xtwWriter.WriteAttributeString("ss", "LineStyle", null, "Continuous");
                    xtwWriter.WriteAttributeString("ss", "Weight", null, "1");
                    xtwWriter.WriteEndElement(); //End of Borders
                    xtwWriter.WriteEndElement(); // End of Border

                    //End Style for group with border
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

                    //Style for Rows
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s65");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    // </Styles>
                    xtwWriter.WriteEndElement();
                    // </Styles>
                    xtwWriter.WriteEndElement();

                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Port_" + sPort);

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
                            if (Header.ColumnName != "xGroupNo")
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

                    Int16 iGroup = 0;
                    // Run through all rows of data source
                    foreach (DataRow row in dtSource.Rows)
                    {
                        iGroup = GlobalCode.Field2TinyInt(row["xGroupNo"]);
                        // <Row>
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s65");

                        i = 1;
                        // Run through all cell of current rows
                        foreach (object cellValue in row.ItemArray)
                        {
                            if (dtSource.Columns[i - 1].Caption.ToUpper() != "XGROUPNO")
                            {
                                if (i <= iColCount)
                                {
                                    if (iGroup == 1)
                                    {
                                        // <Cell>
                                        xtwWriter.WriteStartElement("Cell");
                                        //Border
                                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s63");
                                        // <Data ss:Type="String">xxx</Data>
                                        xtwWriter.WriteStartElement("Data");
                                        if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEE ID" ||
                                                dtSource.Columns[i - 1].Caption.ToUpper() == "HOTEL NITES" ||
                                                dtSource.Columns[i - 1].Caption.ToUpper() == "SEQ NO" ||
                                                dtSource.Columns[i - 1].Caption.ToUpper() == "COST CENTER")
                                        {
                                            if (GlobalCode.Field2Int(cellValue) > 0)
                                            {
                                                xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                            }
                                            //else if (GlobalCode.Field2String(cellValue) != "")
                                            //{
                                            //    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                            //}                                       
                                            else
                                            {
                                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                            }
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
                                    else
                                    {
                                        // <Cell>
                                        xtwWriter.WriteStartElement("Cell");

                                        // <Data ss:Type="String">xxx</Data>
                                        xtwWriter.WriteStartElement("Data");
                                        if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEE ID" ||
                                                dtSource.Columns[i - 1].Caption.ToUpper() == "HOTEL NITES" ||
                                                dtSource.Columns[i - 1].Caption.ToUpper() == "SEQ NO" ||
                                                dtSource.Columns[i - 1].Caption.ToUpper() == "COST CENTER")
                                        {
                                            //if (GlobalCode.Field2String(cellValue) != "")
                                            //{
                                            //    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                            //}
                                            if (GlobalCode.Field2Int(cellValue) > 0)
                                            {
                                                xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                            }
                                            else
                                            {
                                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                            }
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

        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/MeetGreetAndPortAgent/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/Jan/2013
        /// Description:    pop up alert message
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
        /// Author:         Josephine Gad
        /// Date Created:   29/Aug/2013
        /// Description:    Tag Visible show/hide Tag Label settings
        /// </summary>
        protected bool IsTaggedLabelVisible(object IsTaggedByUser, object IsFirstPartition)
        {
            if (GlobalCode.Field2Bool(IsTaggedByUser) == true && GlobalCode.Field2Bool(IsFirstPartition) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/Aug/2013
        /// Description:    Tag Visible show/hide Tag Linkbutton settings
        /// </summary>
        protected bool IsTaggedLinkVisible(object IsTaggedByUser, object IsFirstPartition)
        {
            if (GlobalCode.Field2Bool(IsTaggedByUser) == false && GlobalCode.Field2Bool(IsFirstPartition) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion  
    }
}

using System;
using System.Web.Security;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;

namespace TRAVELMART
{
    public partial class TravelMart : System.Web.UI.MasterPage
    {
        #region Declarations

        string specialistPageString;
        string SFStatus;
        string SFDate;
        string SFDateRange;
        string UFName;
        string DateFromString;
        string DateToString;      

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (Request.QueryString["p"] != null)
            {
                specialistPageString = Request.QueryString["p"];
            }
            if (Request.QueryString["dt"] != null)
            {
                SFDate = Request.QueryString["dt"].ToString();
            }
            if (Request.QueryString["st"] != null)
            {
                SFStatus = Request.QueryString["st"];
            }
            if (Request.QueryString["ufn"] != null)
            {
                UFName = Request.QueryString["ufn"];
            }

            if (SFDate == "ByDate")
            {               
                uoDivByDate.Visible = true;

                if (uoTextBoxFrom.Text != "" && uoTextBoxTo.Text  != "")
                {

                    DateFromString = uoTextBoxFrom.Text;
                    uoHiddenFieldDateFrom.Value = DateFromString;
                    //uoTextBoxFrom.Text = DateFromString;

                    DateToString = uoTextBoxTo.Text;
                    uoHiddenFieldDateTo.Value = DateToString;
                    //uoTextBoxTo.Text = DateToString;
                    
                }
                else
                {
                    if (Request.QueryString["from"] != null)
                    {
                        DateFromString = Request.QueryString["from"];
                        uoHiddenFieldDateFrom.Value = DateFromString;
                        uoTextBoxFrom.Text = DateFromString;
                    }
                    if (Request.QueryString["to"] != null)
                    {
                        DateToString = Request.QueryString["to"];
                        uoHiddenFieldDateTo.Value = DateToString;
                        uoTextBoxTo.Text = DateToString;
                    }
                }
            }
            else
            {
                uoDivByDate.Visible = false;
            }
            
            

            String URLString = "";
            
            if (SFDate.Equals("0-3"))
            {
                SFDateRange = "0 - 3 Days";
            }
            else if (SFDate.Equals("4-7"))
            {
                SFDateRange = "4 - 7 Days";
            }
            else if (SFDate.Equals("8-15"))
            {
                SFDateRange = "8 - 15 Days";
            }
            else if (SFDate.Equals("16-30"))
            {
                SFDateRange = "16 - 30 Days";
            }
            else if (SFDate.Equals("31-100"))
            {
                SFDateRange = "31 Above Days";
            }
            else if (SFDate.Equals("ByDate"))
            {
                SFDateRange = "From: " + DateTime.Parse(DateFromString).ToString("dd-MMM-yyyy") + " To: " + DateTime.Parse(DateToString).ToString("dd-MMM-yyyy");
                URLString = "&from=" + uoHiddenFieldDateFrom.Value + "&to=" + uoHiddenFieldDateTo.Value;
            }           

            if (Request.QueryString["fBy"] != null)
            {
                uoHiddenFieldFilterBy.Value = Request.QueryString["fBy"];
            }
            
            loadSeafarerCount();
            loadSFFlightStatusCounter();
            loadSFPendingCounter();  

            if (SFStatus == "ON")
            {
                uoLinkButtonArrivalDate.Visible = true;
                uoLinkButtonOnsigningDate.Visible = true;
                uoLinkButtonDepartureDate.Visible = false;
                uoLinkButtonOffsigningDate.Visible = false;
                
                if (uoHiddenFieldFilterBy.Value == "")
                {
                    uoHiddenFieldFilterBy.Value = "OnDt";
                }

            }
            else
            {
                uoLinkButtonArrivalDate.Visible = false;
                uoLinkButtonOnsigningDate.Visible = false;
                uoLinkButtonDepartureDate.Visible = true;
                uoLinkButtonOffsigningDate.Visible = true;

                if (uoHiddenFieldFilterBy.Value == "")
                {
                    uoHiddenFieldFilterBy.Value = "OffDt";
                }
            }

            string FilterByString;
            switch (uoHiddenFieldFilterBy.Value)
            {
                case "OnDt" :
                    FilterByString = "Onsigning Date";
                    break;
                case "OffDt":
                    FilterByString = "Offsigning Date";
                    break;
                case "ArrDt":
                    FilterByString = "Arrival Date";
                    break;
                case "DepDt":
                    FilterByString = "Departure Date";
                    break;
                default:
                    FilterByString = "Onsigning Date";
                    break;
            }

            uclabelStatus.Text = (SFStatus == "ON")
                   ? "ONSIGNING ( " + SFDateRange + " )" 
                   : "OFFSIGNING ( " + SFDateRange + " )";
            uclabelStatus.Text += " Based on " + FilterByString;

            uclabelFName.Text = UFName;


            ucHyperLinkHome.NavigateUrl = "~/GeneralListView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=GeneralListView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;
            ucHyperLinkHotel.NavigateUrl = "~/Hotel/HotelView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=Hotel/HotelView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;
            ucHyperLinkVehicle.NavigateUrl = "~/Vehicle/VehicleView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=Vehicle/VehicleView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;
            ucHyperLinkPort.NavigateUrl = "~/PortAgent/PortView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=PortAgent/PortView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;
            ucHyperLinkAir.NavigateUrl = "~/Air/AirView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=Air/AirView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;
            //ucHyperLinkMaintenance.NavigateUrl = "~/Maintenance/MaintenanceView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=GeneralListView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString;
            ucHyperLinkUser.NavigateUrl = "~/Administration/UserAccounts.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=Administration/UserAccounts&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;
            //uohiddenSFDate.Value = (uohiddenSFDate.Value.Length > 0) ? uohiddenSFDate.Value : "0-3";
            ocHyperLinkContract.NavigateUrl = "~/ContractManagement/ContractList.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=ContractManagement/ContractList&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString + "&fBy=" + uoHiddenFieldFilterBy.Value;

            uohiddenSFDate.Value = SFDate;
            //uohiddenSFDate.Value = uohiddenSFDate.Value.Replace("Days", "").Trim();
            ucalinkcreate.InnerText = "| Create User";


            if (Page.User.IsInRole("administrator"))
            {
                ucalinkcreate.Visible = true;
                uoSpanMaintenance.Visible = true;
                ucHyperLinkMaintenance.NavigateUrl = "~/Maintenance/MaintenanceView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=GeneralListView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString;
            }
            else
            {
                ucalinkcreate.Visible = false;
                uoSpanHotel.Visible = false;
                uoSpanVehicle.Visible = false;
                uoSpanPort.Visible = false;
                uoSpanAir.Visible = false;


                if (Page.User.IsInRole("Hotel Specialist"))
                {
                    uoSpanHotel.Visible = true;
                    uoSpanMaintenance.Visible = true;
                    ucHyperLinkMaintenance.NavigateUrl = "~/Maintenance/HotelMaintenanceView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=GeneralListView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString;
                }
                else if (Page.User.IsInRole("Vehicle Specialist"))
                {
                    uoSpanVehicle.Visible = true;
                    uoSpanMaintenance.Visible = true;
                    ucHyperLinkMaintenance.NavigateUrl = "~/Maintenance/VehicleMaintenanceView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=GeneralListView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString;
                }
                else if (Page.User.IsInRole("Port Specialist"))
                {
                    uoSpanPort.Visible = true;
                    uoSpanMaintenance.Visible = true;
                    ucHyperLinkMaintenance.NavigateUrl = "~/Maintenance/PortMaintenanceView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=GeneralListView&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + URLString;
                }
                else if (Page.User.IsInRole("Air Specialist"))
                {
                    uoSpanAir.Visible = true;
                    uoSpanMaintenance.Visible = false;
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            ucalinkcreate.Visible = false;
            if (!IsPostBack)
            {
                uohiddenSFStatus.Value = SFStatus;
                loadDataTypeFilter();
            }
        }
        //protected void uobuttonSignOn_Click(object sender, EventArgs e)
        //{
            
        //}
        //protected void uobuttonSignOff_Click(object sender, EventArgs e)
        //{            
            
        //}
        protected void uobuttonSignOn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            clickSignOn();
        }
        protected void uoLinkButtonOn_Click(object sender, EventArgs e)
        {
            clickSignOn();
        }

        protected void uobuttonSignOff_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            clickSignOff();
        }
        protected void uoLinkButtonOff_Click(object sender, EventArgs e)
        {
            clickSignOff();
        }
        protected void uolinkbutton0_3Days_Click(object sender, EventArgs e)
        {
            uoDivByDate.Visible = false;
            uohiddenSFDate.Value = "0-3";
            TravelMartViewPage(specialistPageString, SFStatus, "0-3", uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uolinkbutton4_7Days_Click(object sender, EventArgs e)
        {
            uoDivByDate.Visible = false;
            uohiddenSFDate.Value = "4-7";
            TravelMartViewPage(specialistPageString, SFStatus, "4-7", uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uolinkbutton8_15Days_Click(object sender, EventArgs e)
        {
            uoDivByDate.Visible = false;
            uohiddenSFDate.Value = "8-15";
            TravelMartViewPage(specialistPageString, SFStatus, "8-15", uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uolinkbutton16_30Days_Click(object sender, EventArgs e)
        {
            uoDivByDate.Visible = false;
            uohiddenSFDate.Value = "16-30";
            TravelMartViewPage(specialistPageString, SFStatus, "16-30", uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uolinkbutton31_100Days_Click(object sender, EventArgs e)
        {
            uoDivByDate.Visible = false;
            uohiddenSFDate.Value = "31-100";
            TravelMartViewPage(specialistPageString, SFStatus, "31-100", uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoLinkButtonDateRange_Click(object sender, EventArgs e)
        {
            uoDivByDate.Visible = true;
        }
        protected void uoButtonDateRange_Click(object sender, EventArgs e)
        {
            uohiddenSFDate.Value = "ByDate";
            uoHiddenFieldDateFrom.Value = uoTextBoxFrom.Text;
            uoHiddenFieldDateTo.Value = uoTextBoxTo.Text;
            TravelMartViewPage(specialistPageString, SFStatus, "ByDate", uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());

        }
        //protected void ContentPlaceHolder1_Init(object sender, EventArgs e)
        //{
        //    uohiddenSFStatus.Value = "ON";
        //}
        protected void uoFlownFlight_Click(object sender, EventArgs e)
        {
            uohiddenFlightStatusFilter.Value = "Flown";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoDelayedFlight_Click(object sender, EventArgs e)
        {
            uohiddenFlightStatusFilter.Value = "Delayed";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoCancelledFlight_Click(object sender, EventArgs e)
        {
            uohiddenFlightStatusFilter.Value = "Cancelled";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoArrivedFlight_Click(object sender, EventArgs e)
        {
            uohiddenFlightStatusFilter.Value = "Arrived";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoPendingAirTrans_Click(object sender, EventArgs e)
        {
            uohiddenPendingFilter.Value = "air";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoPendingVehicleTrans_Click(object sender, EventArgs e)
        {
            uohiddenPendingFilter.Value = "vehicle";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoPendingHotelTrans_Click(object sender, EventArgs e)
        {
            uohiddenPendingFilter.Value = "hotel";
            uohiddenSFDate.Value = Request.QueryString["dt"].ToString();
            TravelMartViewPage("GeneralListView", SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoLinkButtonArrivalDate_Click(object sender, EventArgs e)
        {
            uoHiddenFieldFilterBy.Value = "ArrDt";
            TravelMartViewPage(specialistPageString, SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoLinkButtonDepartureDate_Click(object sender, EventArgs e)
        {
            uoHiddenFieldFilterBy.Value = "DepDt";
            TravelMartViewPage(specialistPageString, SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoLinkButtonOnsigningDate_Click(object sender, EventArgs e)
        {
            uoHiddenFieldFilterBy.Value = "OnDt";
            TravelMartViewPage(specialistPageString, SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoLinkButtonOffsigningDate_Click(object sender, EventArgs e)
        {
            uoHiddenFieldFilterBy.Value = "OffDt";
            TravelMartViewPage(specialistPageString, SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        protected void uoDropDownListDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldFilterBy.Value = uoDropDownListDateType.SelectedValue;
            TravelMartViewPage(specialistPageString, SFStatus, uohiddenSFDate.Value.Trim(), uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        #endregion        

        #region Functions
        private void TravelMartViewPage(string pageString, string statusString, string dateRangeString, string pendingfilter, string airStatusFilter)
        {
            /// <summary>        
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Page redirection
            /// </summary>

            string URLString = "~/" + pageString + ".aspx?st=" + statusString + "&dt=" + dateRangeString + "&p=" + pageString + "&ufn=" + UFName + "&pf=" + pendingfilter + "&af=" + airStatusFilter;
            URLString += "&fBy=" + uoHiddenFieldFilterBy.Value;
            if (dateRangeString == "ByDate")
            {
                uoHiddenFieldDateFrom.Value = (uoHiddenFieldDateFrom.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateFrom.Value);
                uoHiddenFieldDateTo.Value = (uoHiddenFieldDateTo.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateTo.Value);
                URLString += "&from=" + uoHiddenFieldDateFrom.Value + "&to=" + uoHiddenFieldDateTo.Value;
            }           
            Response.Redirect(URLString);
        }
        private void loadSeafarerCount()
        {
            /// <summary>        
            /// Date Created: 08/08/2011
            /// Created By: Marco Abejar
            /// (description) quick link for seafarer count
            /// </summary>

            Session["strSFStatus"] = Request.QueryString["st"];            
            uohl0_3Count.Text = SeafarerTravelBLL.GetSeafarerCountByDateRange(GlobalCode.Field2String(Session["strSFStatus"]),"0-3",uoHiddenFieldFilterBy.Value);
            uohl4_7Count.Text = SeafarerTravelBLL.GetSeafarerCountByDateRange(GlobalCode.Field2String(Session["strSFStatus"]), "4-7", uoHiddenFieldFilterBy.Value);
            uohl8_15Count.Text = SeafarerTravelBLL.GetSeafarerCountByDateRange(GlobalCode.Field2String(Session["strSFStatus"]), "8-15", uoHiddenFieldFilterBy.Value);
            uohl16_30Count.Text = SeafarerTravelBLL.GetSeafarerCountByDateRange(GlobalCode.Field2String(Session["strSFStatus"]), "16-30", uoHiddenFieldFilterBy.Value);
            uohl31_100Count.Text = SeafarerTravelBLL.GetSeafarerCountByDateRange(GlobalCode.Field2String(Session["strSFStatus"]), "31-100", uoHiddenFieldFilterBy.Value);
           
        }
        private void loadSFFlightStatusCounter()
        {
            /// <summary>        
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) quick link for flight status count
            /// </summary>

            Session["strSFStatus"] = Request.QueryString["st"];
            Session["strSFFlightDateRange"] = Request.QueryString["dt"];
            uoFlownFlightCount.Text = SeafarerTravelBLL.GetAirStatusCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Flown", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
            uoArrivedFlightCount.Text = SeafarerTravelBLL.GetAirStatusCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Arrived", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
            uoDelayedFlightCount.Text = SeafarerTravelBLL.GetAirStatusCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Delayed", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
            uoCancelledFlightCount.Text = SeafarerTravelBLL.GetAirStatusCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Cancelled", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
        }
        private void loadSFPendingCounter()
        {
            /// <summary>        
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) quick link for pending transactions
            /// </summary>

            Session["strSFStatus"] = Request.QueryString["st"];
            Session["strSFFlightDateRange"] = Request.QueryString["dt"];
            uoPendingAirTrans.Text = SeafarerTravelBLL.GetPendingTransactionCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Air", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
            uoPendingHotelTrans.Text = SeafarerTravelBLL.GetPendingTransactionCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Hotel", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
            uoPendingVehicleTrans.Text = SeafarerTravelBLL.GetPendingTransactionCount(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), "Vehicle", uoHiddenFieldDateFrom.Value, uoHiddenFieldDateTo.Value, uoHiddenFieldFilterBy.Value);
         }
        private void loadDataTypeFilter()
        {
            uoDropDownListDateType.Items.Clear();
            if (uohiddenSFStatus.Value == "OFF")
            {
                ListItem item = new ListItem("Departure Date", "DepDt");
                uoDropDownListDateType.Items.Add(item);
                item = new ListItem("Offsigning Date", "OffDt");
                uoDropDownListDateType.Items.Add(item);
                uoDropDownListDateType.DataBind();                
            }
            else
            {
                ListItem item = new ListItem("Arrival Date", "ArrDt");
                uoDropDownListDateType.Items.Add(item);
                item = new ListItem("Onsigning Date", "OnDt");
                uoDropDownListDateType.Items.Add(item);
                uoDropDownListDateType.DataBind();
            }

            if (uoHiddenFieldFilterBy.Value != "")
            {
                uoDropDownListDateType.SelectedValue = uoHiddenFieldFilterBy.Value;
            }
        }
        private void clickSignOn()
        {
            uohiddenSFStatus.Value = "ON";
            uoHiddenFieldFilterBy.Value = "OnDt";
            uohiddenSFDate.Value = "0-3";
            loadDataTypeFilter();
            TravelMartViewPage(specialistPageString, uohiddenSFStatus.Value, uohiddenSFDate.Value, uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());
        }
        private void clickSignOff()
        {
            uohiddenSFStatus.Value = "OFF";
            uoHiddenFieldFilterBy.Value = "OffDt";
            uohiddenSFDate.Value = "0-3";
            loadDataTypeFilter();
            TravelMartViewPage(specialistPageString, uohiddenSFStatus.Value, uohiddenSFDate.Value, uohiddenPendingFilter.Value.Trim(), uohiddenFlightStatusFilter.Value.Trim());

        }
        #endregion                                 
        
    }
}

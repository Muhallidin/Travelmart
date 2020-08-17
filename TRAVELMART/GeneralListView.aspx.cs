using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class GeneralListView : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// ========================================================        
        /// Date Modified:  16/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["p"] == null || Request.QueryString["st"] == null || Request.QueryString["dt"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["p"] == "" || Request.QueryString["st"] == "" || Request.QueryString["dt"] == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["from"] != null)
            {
                uoHiddenFieldDateFrom.Value = Request.QueryString["from"];
            }
            if (Request.QueryString["to"] != null)
            {
                uoHiddenFieldDateTo.Value = Request.QueryString["to"];
            }
            if(Request.QueryString["fBy"] != null)
            {
                uoHiddenFieldFilterBy.Value = Request.QueryString["fBy"];
            }

            if (Session["strSFStatus"] == "" && uoHiddenFieldFilterBy.Value == "")
            {
                uoHiddenFieldFilterBy.Value = "OnDt";
            }
            if (Session["strSFStatus"] == "ON" && uoHiddenFieldFilterBy.Value == "")
            {
                uoHiddenFieldFilterBy.Value = "OnDt";
            }
            if (Session["strSFStatus"] == "OFF" && uoHiddenFieldFilterBy.Value == "")
            {
                uoHiddenFieldFilterBy.Value = "OffDt";
            }


            if (!IsPostBack)
            {               
                if (Request.QueryString["pf"] != null)
                    Session["strPendingFilter"] = Request.QueryString["pf"];
                if (Request.QueryString["af"] != null)
                    Session["strAirStatusFilter"] = Request.QueryString["af"];

                Session["strSFStatus"] = Request.QueryString["st"];
                BindDropDownGroup();                
                BindRegion();
                Session["strSFFlightDateRange"] = Request.QueryString["dt"];
                
                //if (GlobalCode.Field2String(Session["strSFFlightDateRange"]) == "ByDate")
                //{
                //    uoTRDateRange.Visible = true;
                //}
                //else
                //{
                //    uoTRDateRange.Visible = false;
                //}

                GetSFTravelList(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), GlobalCode.Field2String(Session["strPendingFilter"]), GlobalCode.Field2String(Session["strAirStatusFilter"]));
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetSFTravelList(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), GlobalCode.Field2String(Session["strPendingFilter"]), GlobalCode.Field2String(Session["strAirStatusFilter"]));
        }
        protected void uoDropDownListGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSFTravelList(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), GlobalCode.Field2String(Session["strPendingFilter"]), GlobalCode.Field2String(Session["strAirStatusFilter"]));
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSFTravelList(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), GlobalCode.Field2String(Session["strPendingFilter"]), GlobalCode.Field2String(Session["strAirStatusFilter"]));
        }
        protected void uoListViewGeneralListPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region Functions
        private void GetSFTravelList(string strSFStatus, string strFlightDateRange, string strPendingFilter, string strAirStatusFilter)
        {
            /// <summary> 
            /// Date Created: 08/07/2011
            /// Created By: Marco Abejar
            /// (description) Get general view of seafarers list with regards to flight sched
            /// ---------------------------------------------------------------------------------------------------
            /// Date Modified: 20/07/2011
            /// Modifed By: Josephine Gad
            /// (description) Change the GridView uogridviewGeneralListInfo to ListView uoListViewGeneralList
            /// ---------------------------------------------------------------------------------------------------
            /// Date Modified: 01/08/2011
            /// Modified By: Josephine Gad
            /// (description) Add seafarer name parameter
            /// ---------------------------------------------------------------------------------------------------
            /// Date Modified: 05/08/2011
            /// Modifed By: Josephine Gad
            /// (description) Add Date Range parameter, Add Try and Catch, Hide Vessel Column if Group By is Vessel
            /// </summary>            

            DataView GeneralListDataView = null;

            try
            {
                string DateFromString = (uoHiddenFieldDateFrom.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateFrom.Value);
                string DateToString = (uoHiddenFieldDateTo.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateTo.Value);

                GeneralListDataView = SeafarerTravelBLL.GetSFTravelList(strSFStatus, strFlightDateRange, strPendingFilter, strAirStatusFilter, uoTextBoxName.Text,
                   DateFromString, DateToString, GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, uoHiddenFieldFilterBy.Value).DefaultView;
                GeneralListDataView.Sort = uoDropDownListGroupBy.SelectedValue + ", Name ";
                uoListViewGeneralList.DataSource = GeneralListDataView;
                uoListViewGeneralList.DataBind();

                Label HeaderLabel;
                Label HeaderFlightLabel;
                HeaderLabel = (Label)uoListViewGeneralList.FindControl("ucLabelDate");
                HeaderFlightLabel = (Label)uoListViewGeneralList.FindControl("ucLabelFlightDate");
                if (HeaderLabel != null)
                {
                    if (strSFStatus == "ON")
                    {
                        HeaderLabel.Text = "Onsigning";
                        HeaderFlightLabel.Text = "Arrival Date";
                    }
                    else
                    {
                        HeaderLabel.Text = "Offsigning";
                        HeaderFlightLabel.Text = "Departure Date";
                    }
                }

                HtmlControl VesselHeader = (HtmlControl)uoListViewGeneralList.FindControl("ucTHVessel");
                HtmlControl OnOffHeader = (HtmlControl)uoListViewGeneralList.FindControl("ucTHOnOffDate");
                HtmlControl FlightHeader = (HtmlControl)uoListViewGeneralList.FindControl("ucTHFlightDate");
                HtmlControl Port = (HtmlControl)uoListViewGeneralList.FindControl("ucTHPort");

                if (VesselHeader != null)
                {
                    if (uoDropDownListGroupBy.SelectedValue == "Vessel")
                    {
                        VesselHeader.Visible = false;
                        OnOffHeader.Visible = true;
                        FlightHeader.Visible = true;
                        Port.Visible = true;
                    }
                    //else
                    //{
                    //    VesselHeader.Visible = true;
                    //    OnOffHeader.Visible = false;
                    //    FlightHeader.Visible = false;
                    //}
                    
                    else if(uoDropDownListGroupBy.SelectedValue == "OnOffDate")
                    {
                        VesselHeader.Visible = true;
                        OnOffHeader.Visible = false;
                        FlightHeader.Visible = true;
                        Port.Visible = true;
                    }
                    else if (uoDropDownListGroupBy.SelectedValue == "colArrivalDate")
                    {
                        VesselHeader.Visible = true;
                        OnOffHeader.Visible = true;
                        FlightHeader.Visible = false;
                        Port.Visible = true;
                    }
                    else if (uoDropDownListGroupBy.SelectedValue == "colPortNameVarchar")
                    {
                        VesselHeader.Visible = true;
                        OnOffHeader.Visible = true;
                        FlightHeader.Visible = true;
                        Port.Visible = false;
                    }
                    else
                    {
                        VesselHeader.Visible = true;
                        OnOffHeader.Visible = false;
                        FlightHeader.Visible = false;
                        Port.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (GeneralListDataView != null)
                {
                    GeneralListDataView.Dispose();                    
                }
            }
        }
        protected string GetStatusImage(object StatusObj)
        {
            /// <summary>
            /// Date Created: 20/07/2011
            /// Created By: Josephine Gad
            /// (description) Get the directory of image status            
            /// <summary>
            
            string ImageString = "~/Images/";
            string StatusString = StatusObj.ToString();
            StatusString = StatusString.ToLower();

            switch (StatusString)
            {
                case "ok":
                    ImageString += "positive.png";
                    break;
                case "notok":
                    ImageString += "positive-red.png";
                    break;
                case "none":
                    ImageString += "neutral.png";
                    break;
                default:
                    ImageString += "neutral.png";
                    break;
            }
            return ImageString;
        }
        private void BindDropDownGroup()
        {
            /// <summary>
            /// Date Created: 20/07/2011
            /// Created By: Josephine Gad
            /// (description) List for grouping            
            /// <summary>

            ListItem item = new ListItem("Ship", "Vessel");
            uoDropDownListGroupBy.Items.Clear();
            uoDropDownListGroupBy.Items.Add(item);

            item = new ListItem("Arrival Date", "colArrivalDate");
            uoDropDownListGroupBy.Items.Add(item);

            if (Session["strSFStatus"] == "ON")
            {
                item = new ListItem("Onsigning Date", "OnOffDate");
            }
            else
            {
                item = new ListItem("Offsigning Date", "OnOffDate");
            }
            uoDropDownListGroupBy.Items.Add(item);

            item = new ListItem("Port", "colPortNameVarchar");
            uoDropDownListGroupBy.Items.Add(item);

            uoDropDownListGroupBy.DataBind();
            uoDropDownListGroupBy.Items.FindByValue("Vessel").Selected = true;

            //CommonFunctions.ChangeToUpperCase(uoDropDownListGroupBy);
        }       
        private void BindRegion()
        {
            DataTable RegionDataTable = null;
            try
            {
                RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListRegion.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListRegion.Items.Add(item);
                uoDropDownListRegion.DataSource = RegionDataTable;
                uoDropDownListRegion.DataTextField = "colMapNameVarchar";
                uoDropDownListRegion.DataValueField = "colMapIDInt";
                uoDropDownListRegion.DataBind();
                
                //CommonFunctions.ChangeToUpperCase(uoDropDownListRegion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }            

        string lastDataFieldValue = null;
        protected string TravelListAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = uoDropDownListGroupBy.SelectedItem.Text;
            string GroupValueString = uoDropDownListGroupBy.SelectedValue;

            string currentDataFieldValue = Eval(uoDropDownListGroupBy.SelectedValue).ToString();
            if (GroupTextString.Contains("Date"))
            {
                currentDataFieldValue = string.Format("{0:dd-MMM-yyyy}", Eval(GroupValueString));
            }
            
            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                return string.Format("<tr><td class=\"group\" colspan=\"9\">{0}: <strong>{1}</strong></td></tr>", uoDropDownListGroupBy.SelectedItem.Text, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        protected string HideVessel()
        {
            if (uoDropDownListGroupBy.SelectedValue == "Vessel")
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        protected string HideOnOffDate()
        {
            if (uoDropDownListGroupBy.SelectedValue == "OnOffDate")
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        protected string HideFlightDate()
        {
            if (uoDropDownListGroupBy.SelectedValue == "colArrivalDate")
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        protected string HidePort()
        {
            if (uoDropDownListGroupBy.SelectedValue == "colPortNameVarchar")
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        #endregion 

       

       

       
    }
}

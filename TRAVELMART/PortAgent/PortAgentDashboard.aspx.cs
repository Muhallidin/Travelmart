using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class PortAgentDashboard : System.Web.UI.Page
    {
        PortAgentBLL BLL = new PortAgentBLL();

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("Login.aspx");
            }  
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); 
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                uoHiddenFieldPortAgentID.Value = GlobalCode.Field2Int(Session["PortAgentID"]).ToString();

                BindSeaport();
                BindPortAgent();
                BindNoOfDays();

                BindList(0);
                BindSeaport();
                BindPortAgent();
                uoHiddenFieldPortAgentID.Value = GlobalCode.Field2Int(Session["PortAgentID"]).ToString();
                BindNoOfDays();
                BindDashboard();

                if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    uoDropDownListSeaport.Visible = false;
                }

                LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                uoLinkExport.Visible = false;

                HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");
                ucSpanExportALL.Visible = false;

                LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");

                uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                ucSpanViewWeek.Visible = false;
            }
            else
            {
                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

                if (uoHiddenFieldPopupCalendar.Value == "1" || (sChangeDate == "1" ))
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                    uoHiddenFieldPortAgentID.Value = GlobalCode.Field2Int(Session["PortAgentID"]).ToString();
                    BindList(0);
                    BindSeaport();
                    BindPortAgent();                    
                    BindDashboard();

                    //uoHiddenFieldPopupCalendar.Value = "0";
                    string sURL = "PortAgentDashboard.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
                    Context.RewritePath(sURL);
                }                
            }

           // uoHyperLinkDashboard.NavigateUrl = "~/Hotel/HotelExceptionBookingsDashboard.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];

            uoHyperLinkDashboard.NavigateUrl = "/Hotel/HotelNonTurnPorts2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];

        }

        protected void uoDropDownListPortAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldPortAgentID.Value = uoDropDownListPortAgent.SelectedValue;
            Session["PortAgentID"] = uoHiddenFieldPortAgentID.Value;

            BindList(1);
            BindDashboard();
        }
        protected void uoDropDownListSeaport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListSeaport.SelectedValue;
            Session["PortAgentDTO"] = null;
            BindPortAgent();
            BindList(1);                         
            BindDashboard();            
        }
        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
        }
        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }
        protected void uoDropDownListDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NoOfDays"] = uoDropDownListDays.SelectedValue;
            //Session["PortAgentDTO"] = null;
            BindList(0);
            BindSeaport();
            BindPortAgent();
            BindDashboard();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/Mar/2014
        /// Description:    Get the order to be used
        /// -------------------------------------  
        protected void uoListViewHotelHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            //uolistviewHotelInfoPager.SetPageProperties(0, uolistviewHotelInfoPager.PageSize, false);            
        }
        protected void uoDropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //uoHiddenFieldManifestStatus.Value = uoDropDownListStatus.SelectedValue;
            //Session["PortAgentDashboardStatus"] = uoDropDownListStatus.SelectedValue;
            //BindListwithStatus(1);
            //BindSeaport();
            //BindPortAgent();
            //BindDashboard();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Get List of Service Provider, Hotel Manifest and Vehicle Manifest
        /// -------------------------------------------------------------------
        /// </summary>        
        private void BindList(Int16 iLoadType)
        {
            Int32 iPortID = GlobalCode.Field2Int(Session["Port"]);

            BLL.PortAgentManifestGet(GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue),
                uoHiddenFieldDate.Value, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, 
                uoHiddenFieldOrder.Value, iPortID, iLoadType, GlobalCode.Field2Int(uoDropDownListDays.SelectedValue));

        }
        private void BindListwithStatus(Int16 iLoadType)
        {
            Int32 iPortID = GlobalCode.Field2Int(Session["Port"]);
            //BLL.PortAgentManifestGetwithStatus(GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue),
            //    uoHiddenFieldDate.Value, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
            //    uoHiddenFieldOrder.Value, iPortID, iLoadType, GlobalCode.Field2Int(uoDropDownListDays.SelectedValue), uoHiddenFieldManifestStatus.Value);

        }
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Bind Service Provider in DropDownList
        /// -------------------------------------------------------------------
        private void BindPortAgent()
        {
            List<PortAgentDTO> list = new List<PortAgentDTO>();

            uoDropDownListPortAgent.Items.Clear();
            uoDropDownListPortAgent.Items.Add(new ListItem("--Select Service Provider--", "0"));

            if (Session["PortAgentDTO"] != null)
            {
                list = (List<PortAgentDTO>)Session["PortAgentDTO"];                
            }
            else
            {
                list = BLL.GetPortAgentListByPortId(GlobalCode.Field2Int(uoDropDownListSeaport.SelectedValue), 
                    uoHiddenFieldUser.Value, uoHiddenFieldRole.Value);
                Session["PortAgentDTO"] = list;
            }
            uoDropDownListPortAgent.DataSource = list;
            uoDropDownListPortAgent.DataTextField = "PortAgentName";
            uoDropDownListPortAgent.DataValueField = "PortAgentID";
            uoDropDownListPortAgent.SelectedValue = "0";
            uoDropDownListPortAgent.DataBind();


            if (list.Count == 1)
            {
                if (uoDropDownListPortAgent.Items.FindByValue(list[0].PortAgentID) != null)
                {
                    uoDropDownListPortAgent.SelectedValue = list[0].PortAgentID;
                    uoHiddenFieldPortAgentID.Value = list[0].PortAgentID;
                    Session["PortAgentID"] = list[0].PortAgentID;
                }
            }
            //else
            //{
            //    if (uoDropDownListPortAgent.Items.FindByValue(uoHiddenFieldPortAgentID.Value) != null)
            //    {
            //        uoDropDownListPortAgent.SelectedValue = uoHiddenFieldPortAgentID.Value;
            //    }
            //}
            //Session["PortAgentID"] = uoDropDownListPortAgent.SelectedValue;

            string sPortAgentID = GlobalCode.Field2Int(Session["PortAgentID"]).ToString();
            if (uoDropDownListPortAgent.Items.FindByValue(sPortAgentID) != null)
            {
                uoDropDownListPortAgent.SelectedValue = sPortAgentID;
            }
            uoHiddenFieldPortAgentID.Value = uoDropDownListPortAgent.SelectedValue;
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Bind dashboard count summary
        /// -------------------------------------------------------------------
        /// Modified By:    Michael Brian C. Evangelista
        /// Date Modified:  11/Aug/2014
        /// Description:    Added Dashboard count for luggage,visa
        /// -------------------------------------------------------------------
        /// </summary>        
        private void BindDashboard()
        {
            List<PortAgentServicesCount> listHotelCount = new List<PortAgentServicesCount>();
            List<PortAgentServicesCount> listVehicleCount = new List<PortAgentServicesCount>();
            List<PortAgentServicesCount> listLuggageCount = new List<PortAgentServicesCount>();
            List<PortAgentServicesCount> listVisaCount = new List<PortAgentServicesCount>();
            List<PortAgentServicesCount> listSafeguardCount = new List<PortAgentServicesCount>();
            List<PortAgentServicesCount> listMAGCount = new List<PortAgentServicesCount>();

            listHotelCount = (List<PortAgentServicesCount>)Session["PortAgentHotelCount"];
            listVehicleCount = (List<PortAgentServicesCount>)Session["PortAgentVehicleCount"];
            listLuggageCount = (List<PortAgentServicesCount>)Session["PortAgentLuggageCount"];
            listVisaCount = (List<PortAgentServicesCount>)Session["PortAgentVisaCount"];
            listSafeguardCount = (List<PortAgentServicesCount>)Session["PortAgentSafeGuardCount"];
            listMAGCount = (List<PortAgentServicesCount>)Session["PortAgentMAGCount"];

            int iPortAgentID = GlobalCode.Field2Int(uoHiddenFieldPortAgentID.Value);

            if (iPortAgentID > 0)
            {
                var list = (from a in listHotelCount
                            where a.PortAgentID == iPortAgentID
                            select new PortAgentServicesCount
                            {
                                iRow = a.iRow,
                                TotalCount = a.TotalCount,
                                PortAgentID = a.PortAgentID,
                                PortAgentName = a.PortAgentName,

                                PendingVendor = a.PendingVendor,
                                PendingRCCL = a.PendingRCCL,
                                Approved = a.Approved,
                                Cancelled = a.Cancelled,
                                
                                PendingColor = a.PendingColor,
                                CancelledColor = a.CancelledColor,
                                PendingRCCLColor = a.PendingRCCLColor,
                                PendingRCCLCostColor = a.PendingRCCLCostColor,

                                PendingRCCLCost = a.PendingRCCLCost,
                                ApprovedColor = a.ApprovedColor
                                
                            }).ToList();
                uoListViewHotelCount.Items.Clear();
                uoListViewHotelCount.DataSource = list;



                var listVehicle = (from a in listVehicleCount
                            where a.PortAgentID == iPortAgentID
                            select new PortAgentServicesCount
                            {
                                iRow = a.iRow,
                                TotalCount = a.TotalCount,
                                PortAgentID = a.PortAgentID,
                                PortAgentName = a.PortAgentName,

                                PendingVendor = a.PendingVendor,
                                PendingRCCL = a.PendingRCCL,
                                Approved = a.Approved,
                                Cancelled = a.Cancelled,

                                PendingColor = a.PendingColor,
                                CancelledColor = a.CancelledColor,
                                PendingRCCLColor = a.PendingRCCLColor,
                                PendingRCCLCostColor = a.PendingRCCLCostColor,

                                PendingRCCLCost = a.PendingRCCLCost,
                                ApprovedColor = a.ApprovedColor
                                
                            }).ToList();
                uoListViewVehicleCount.Items.Clear();
                uoListViewVehicleCount.DataSource = listVehicle;

                //for luggage
                var listLuggage = (from a in listLuggageCount
                                   where a.PortAgentID == iPortAgentID
                                   select new PortAgentServicesCount
                                   {
                                       iRow = a.iRow,
                                       TotalCount = a.TotalCount,
                                       PortAgentID = a.PortAgentID,
                                       PortAgentName = a.PortAgentName,

                                       PendingVendor = a.PendingVendor,
                                       PendingRCCL = a.PendingRCCL,
                                       Approved = a.Approved,
                                       Cancelled = a.Cancelled,

                                       PendingColor = a.PendingColor,
                                       CancelledColor = a.CancelledColor,
                                       PendingRCCLColor = a.PendingRCCLColor,
                                       PendingRCCLCostColor = a.PendingRCCLCostColor,

                                       PendingRCCLCost = a.PendingRCCLCost,
                                       ApprovedColor = a.ApprovedColor

                                   }).ToList();
                //Commented for now
                //uoListViewLuggageCount.Items.Clear();
                //uoListViewLuggageCount.DataSource = listLuggage;
                
                //for visa
                var listVisa = (from a in listVisaCount
                                   where a.PortAgentID == iPortAgentID
                                   select new PortAgentServicesCount
                                   {
                                       iRow = a.iRow,
                                       TotalCount = a.TotalCount,
                                       PortAgentID = a.PortAgentID,
                                       PortAgentName = a.PortAgentName,

                                       PendingVendor = a.PendingVendor,
                                       PendingRCCL = a.PendingRCCL,
                                       Approved = a.Approved,
                                       Cancelled = a.Cancelled,

                                       PendingColor = a.PendingColor,
                                       CancelledColor = a.CancelledColor,
                                       PendingRCCLColor = a.PendingRCCLColor,
                                       PendingRCCLCostColor = a.PendingRCCLCostColor,

                                       PendingRCCLCost = a.PendingRCCLCost,
                                       ApprovedColor = a.ApprovedColor

                                   }).ToList();
                //Commented for now
                //uoListViewVisaCount.Items.Clear();
                //uoListViewVisaCount.DataSource = listLuggage;

                //for safeguard
                var listSafe = (from a in listSafeguardCount
                                where a.PortAgentID == iPortAgentID
                                select new PortAgentServicesCount
                                {
                                    iRow = a.iRow,
                                    TotalCount = a.TotalCount,
                                    PortAgentID = a.PortAgentID,
                                    PortAgentName = a.PortAgentName,

                                    PendingVendor = a.PendingVendor,
                                    PendingRCCL = a.PendingRCCL,
                                    Approved = a.Approved,
                                    Cancelled = a.Cancelled,

                                    PendingColor = a.PendingColor,
                                    CancelledColor = a.CancelledColor,
                                    PendingRCCLColor = a.PendingRCCLColor,
                                    PendingRCCLCostColor = a.PendingRCCLCostColor,

                                    PendingRCCLCost = a.PendingRCCLCost,
                                    ApprovedColor = a.ApprovedColor

                                }).ToList();
                //Commented for now
                //uoListViewSafeGuardCount.Items.Clear();
                //uoListViewSafeGuardCount.DataSource = listLuggage;

                //for safeguard
                var listMAG = (from a in listMAGCount
                                where a.PortAgentID == iPortAgentID
                                select new PortAgentServicesCount
                                {
                                    iRow = a.iRow,
                                    TotalCount = a.TotalCount,
                                    PortAgentID = a.PortAgentID,
                                    PortAgentName = a.PortAgentName,

                                    PendingVendor = a.PendingVendor,
                                    PendingRCCL = a.PendingRCCL,
                                    Approved = a.Approved,
                                    Cancelled = a.Cancelled,

                                    PendingColor = a.PendingColor,
                                    CancelledColor = a.CancelledColor,
                                    PendingRCCLColor = a.PendingRCCLColor,
                                    PendingRCCLCostColor = a.PendingRCCLCostColor,

                                    PendingRCCLCost = a.PendingRCCLCost,
                                    ApprovedColor = a.ApprovedColor

                                }).ToList();
                //Commented for now
                //uoListViewMAGCount.Items.Clear();
                //uoListViewMAGCount.DataSource = listLuggage;


            }
            else
            {
                uoListViewHotelCount.Items.Clear();
                uoListViewHotelCount.DataSource = listHotelCount;

                uoListViewVehicleCount.Items.Clear();
                uoListViewVehicleCount.DataSource = listVehicleCount;

                //Commented for now
                //uoListViewLuggageCount.Items.Clear();
                //uoListViewLuggageCount.DataSource = listLuggageCount;

                //uoListViewVisaCount.Items.Clear();
                //uoListViewVisaCount.DataSource = listVisaCount;
                //uoListViewSafeGuardCount.Items.Clear();
                //uoListViewSafeGuardCount.DataSource = listSafeguardCount;
                //uoListViewMAGCount.Items.Clear();
                //uoListViewMAGCount.DataSource = listMAGCount;
            }           

            uoListViewHotelCount.DataBind();            
            uoListViewVehicleCount.DataBind();

            //Commented for now
            //uoListViewLuggageCount.DataBind();
            //uoListViewVisaCount.DataBind();
            //uoListViewSafeGuardCount.DataBind();
            //uoListViewMAGCount.DataBind();            
        }

        protected string GetRowDivision(object iRow)
        {
            int i = GlobalCode.Field2Int(iRow);
            if (i != 1)
            { 
                return "cssLine";
            }
            else
            {
                return "cssNoLine";
            }
        }
        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Service Provider"; //"Check In";
            string GroupValueString = "PortAgentName"; //"colTimeSpanStartDate";

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
                    return string.Format("<tr><td class=\"group\" colspan=\"36\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   13/Mar/2014
        /// Descrption:     Bind Seaport
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindSeaport()
        {
            List<SeaportDTO> listSeaport = new List<SeaportDTO>();
            if (Session["PortAgentSeaport"] != null)
            { 
                listSeaport = (List<SeaportDTO>)Session["PortAgentSeaport"];
            }
            uoDropDownListSeaport.Items.Clear();
            uoDropDownListSeaport.Items.Add(new ListItem("--Select Seaport--", "0"));

            uoDropDownListSeaport.DataSource = listSeaport;

            uoDropDownListSeaport.DataTextField = "SeaportNameString";
            uoDropDownListSeaport.DataValueField = "SeaportIDString";
            uoDropDownListSeaport.SelectedValue = "0";
            uoDropDownListSeaport.DataBind();

            string sPortID = GlobalCode.Field2Int(Session["Port"]).ToString();
            if (uoDropDownListSeaport.Items.FindByValue(sPortID) != null)
            {
                uoDropDownListSeaport.SelectedValue = sPortID;
            }
        }
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   13/Mar/2014
        /// Descrption:     Bind Colors
        /// -------------------------------------------------------------------
        protected string GetPendingColor(object oColor)
        {
            string sStyle = "";
            string sColor = GlobalCode.Field2String(oColor);
            if (sColor.Trim() != "")
            {
                sStyle = "style=\" color:"+ sColor +"\" ";
            }

            return sStyle;
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   27/Mar/2014
        /// Descrption:     Bind No. Of Days
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindNoOfDays()
        {
            uoDropDownListDays.Items.Clear();
            uoDropDownListDays.Items.Add(new ListItem("--Select No. of Days--","0"));

            int iNoOfdays = TMSettings.NoOfDays;
            ListItem item;
            for(int i = 1; i<= iNoOfdays; i++)
            {
                item = new ListItem(i.ToString(), i.ToString());
                uoDropDownListDays.Items.Add(item);
            }

            uoDropDownListDays.DataBind();
            if (Session["NoOfDays"] != null)
            {
                string sDay = GlobalCode.Field2Int(Session["NoOfDays"]).ToString();
                if (uoDropDownListDays.Items.FindByValue(sDay) != null)
                {
                    uoDropDownListDays.SelectedValue = sDay;
                }
            }
            Session["NoOfDays"] = uoDropDownListDays.SelectedValue;
        }
        /// <summary>
        /// Date Created: 13/Aug/2014
        /// Created By: Michael Brian C. Evangelista
        /// Description: Added Status dropdownlist
        /// </summary>
        #endregion        

      
    }
}

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
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class HotelDashboardRoomType3 : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Modified:   02/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                  to avoid error in date conversion
        /// -----------------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date MOdified:  27/03/2012
        /// Description:    Add uoHiddenFieldPopupCalendar from Calendar popup  to refresh page        
        /// -------------------------------------------
        /// Date Modified:  15/08/2012
        /// Modified By:    Josephine Gad
        /// (description)   Get uoHiddenFieldDateRange.Value from UserAccountList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("Login.aspx");                           
            }
            Session["strPrevPage"] = Request.RawUrl; //gelo   
            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;
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
            }
            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {                
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                List<UserAccountList> listUser = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
                uoHiddenFieldDateRange.Value = GlobalCode.Field2String(listUser[0].iDayNo); 
                
                BindRegion();
                BindCountry();
                BindCity();
                BindPort();

                LoadDefaults(0);
            }            
            if (uoHiddenFieldPopupHotel.Value == "1" || TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserName"]))
            {
                GetExceptionNoTravelRequest();
                GetDashboard();
            }
            uoHiddenFieldPopupHotel.Value = "0";
          
        }

        protected void uoDataPagerDashboard_PreRender(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
        }

        protected void uoObjectDataSourceDashboard_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["iRegionID"] = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue); //GlobalCode.Field2Int(GlobalCode.Field2String(Session["Region"]));
            e.InputParameters["iCountryID"] = GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue); //GlobalCode.Field2Int( GlobalCode.Field2String(Session["Country"]));
            e.InputParameters["iCityID"] = GlobalCode.Field2Int(uoDropDownListCity.SelectedValue); //GlobalCode.Field2Int(GlobalCode.Field2String(Session["City"]));
            e.InputParameters["iPortID"] = GlobalCode.Field2Int(uoDropDownListPort.SelectedValue); //GlobalCode.Field2Int(GlobalCode.Field2String(Session["Port"]));

            e.InputParameters["sUserName"] = uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["iBranchID"] = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
            e.InputParameters["dFrom"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
            e.InputParameters["dTo"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value));
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            {
                e.InputParameters["sBranchName"] = "";
            }
            else
            {
                e.InputParameters["sBranchName"] = uoTextBoxSearch.Text.Trim();
            }
            e.InputParameters["iLoadType"] = GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value);
            e.InputParameters["FromDefaultView"] = GlobalCode.Field2TinyInt(uoHiddenFieldFromDefaultView.Value);
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = uoTextBoxSearch.Text.Trim();
            uoHiddenFieldLoadType.Value = "1";
            uoHiddenFieldFromDefaultView.Value = "0";
            GetDashboard();
        }
        protected void uoButtonClear_Click(object sender, EventArgs e)
        {
            uoDropDownListRegion.SelectedValue = "0";
            uoDropDownListCountry.SelectedValue = "0";
            uoTextBoxFilterCity.Text = "";
            uoDropDownListCity.SelectedValue = "0";
            uoDropDownListPort.SelectedValue = "0";
            uoTextBoxSearch.Text = "";
            GetDashboard();
        }
        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRoom")
            {
                Response.Redirect("HotelDashboard.aspx?ufn=" + Request.QueryString["ufn"].ToString());
            }
            else
            {
                string [] str = e.CommandArgument.ToString().Split('|');
                Session["DateFrom"] = GlobalCode.Field2DateTime(str[0]).ToShortDateString();
                string branchId = str[1];
                string brandId = str[2];
                string HotelName = str[3];
                Response.Redirect("HotelDashboard3.aspx?ufn=" + 
                    Request.QueryString["ufn"].ToString() + "&dt=" + Session["DateFrom"] + 
                    "&branchId=" + branchId + "&brandId=" + brandId + "&branchName=" + HotelName);
                //string sURl = "" String.Format("{0:MM-dd-yyyy}", Eval("colDate"))Eval("BranchID") + 
                //Eval("BrandId") Eval("HotelBranchName");
            }
        }
        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }
        protected void uoDataPagerExceptionNoTravelRequest_PreRender(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
        }
        protected void uoObjectDataSourceExceptionNoTravelRequest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["iRegionID"] = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue); //GlobalCode.Field2Int(GlobalCode.Field2String(Session["Region"]));
            e.InputParameters["iCountryID"] = GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue); //GlobalCode.Field2Int( GlobalCode.Field2String(Session["Country"]));
            e.InputParameters["iCityID"] = GlobalCode.Field2Int(uoDropDownListCity.SelectedValue); //GlobalCode.Field2Int(GlobalCode.Field2String(Session["City"]));
            e.InputParameters["iPortID"] = GlobalCode.Field2Int(uoDropDownListPort.SelectedValue); //GlobalCode.Field2Int(GlobalCode.Field2String(Session["Port"]));

            e.InputParameters["sUserName"] = uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["iBranchID"] = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
            e.InputParameters["dFrom"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
            e.InputParameters["dTo"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value));
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            {
                e.InputParameters["sBranchName"] = "";
            }
            else
            {
                e.InputParameters["sBranchName"] = uoTextBoxSearch.Text.Trim();
            }
            e.InputParameters["iLoadType"] = GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value);
        }
        protected void uoListViewExceptionNoTravelRequest_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Exception")
            {
                Session["DateFrom"] = e.CommandArgument.ToString();
                Response.Redirect("/Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "ArrDep")
            {
                Session["DateFrom"] = e.CommandArgument.ToString();
                Response.Redirect("/ArrivalDepartureSameDate.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + e.CommandArgument.ToString());
            }
            else
            {
                Session["DateFrom"] = e.CommandArgument.ToString();
                Response.Redirect("NoTravelRequest2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + e.CommandArgument.ToString());
            }
        }
        protected void uoListViewExceptionNoTravelRequest_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }

        /// <summary>
        /// Date Created: 16/03/2012
        /// Created By: Gabriel Oquialda
        /// (description) Select region
        /// </summary>
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountry();
        }

        /// <summary>
        /// Date Created: 16/03/2012
        /// Created By: Gabriel Oquialda
        /// (description) Select country
        /// </summary>
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity();
            BindPort();
        }

        /// <summary>
        /// Date Created: 16/03/2012
        /// Created By: Gabriel Oquialda
        /// (description) Select city
        /// </summary
        protected void uoButtonViewCity_Click(object sender, EventArgs e)
        {
            BindCity();
        }
        #endregion
             
        #region Functions
        /// <summary>
        /// Date Created:   24/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Bind Dashboard
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetDashboard()
        {
            try
            {
                uoListViewDashboard.DataSource = null;
                uoListViewDashboard.DataSourceID = "uoObjectDataSourceDashboard";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   16/03/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Bind Exception and No Travel Request
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetExceptionNoTravelRequest()
        {
            try
            {
                uoListViewExceptionNoTravelRequest.DataSource = null;
                uoListViewExceptionNoTravelRequest.DataSourceID = "ObjectDataSourceExceptionNoTravelRequest";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   24/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set Dashboard groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel Branch";
            string GroupValueString = "HotelBranchName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

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
                //return string.Format("<tr><td class=\"group\" colspan=\"17\">{0}: <strong><a class=\"groupLink\" href=\"Maintenance/HotelMaintenanceBranch.aspx?vmId=" + Eval("BranchID") + "&ufn=" + Request.QueryString["ufn"].ToString() + "\">{1}<a/></strong></td></tr>", GroupTextString, currentDataFieldValue);
                if (Eval("IsWithContract").ToString() == "True")
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"5\">{0}: <strong><a class=\"groupLink\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\">{1}<a/></strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
                else
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"5\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        string lastDateFieldValue = null;
        protected bool DashboardAddDateRow()
        {
            //string RowValueString = Eval("colDate").ToString();
            //string RowTextString = string.Format("{0:dd-MM-yyyy}", RowValueString);
            string RowTextString = string.Format("{0:dd-MMM-yyyy}", Eval("colDate"));
            string currentDataFieldValue = RowTextString;

            //See if there's been a change in value
            if (lastDateFieldValue != currentDataFieldValue)
            {                
                //There's been a change! Record the change and emit the table row
                lastDateFieldValue = currentDataFieldValue;
                //string sURl = "HotelDashboard3.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName");
                ////string sReturn = string.Format("<td class=\"leftAligned\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></td> <td  class=\"leftAligned\">" + Eval("colDateName"), RowTextString);
                //string sReturn = "<td class=\"leftAligned\"><a class=\"leftAligned\" href=\"" + sURl + "\">" + RowTextString + "<a/>";
                //if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
                //{
                //    //sReturn += "&nbsp; <a id=\"uoLinkButtonEvents\" href=\"#\" class=\"EventNotification\" title=\"With Event(s)\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\">*</a>";
                //    sReturn += "&nbsp; <a id=\"uoLinkButtonEvents\" href=\"#\" class=\"EventNotification\" title=\"With Event(s)\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"View Event(s)\" border=\"0\"/></a>";
                //    //<asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" Visible='<%# Eval("IsWithEvent") %>' ToolTip="With Event(s)"></asp:LinkButton>
                //}
                //sReturn += "<td  class=\"leftAligned\">" + Eval("colDateName") + "</td>";
                return true;
            }
            else
            {
                //No change, return an empty string
                return false;//"<td></td><td></td>";
            }
        }
         string lastDateFieldValue2 = null;
         string lastClass = "alternateBg";
         protected string DashboardChangeRowColor()
         {
             string RowTextString = string.Format("{0:dd-MMM-yyyy}", Eval("colDate"));
             string currentDataFieldValue = RowTextString;
             //See if there's been a change in value
             if (lastDateFieldValue2 != currentDataFieldValue)
             {
                 //There's been a change! Record the change and emit the table row
                 lastDateFieldValue2 = currentDataFieldValue;
                 if (lastClass == "")
                 {
                     lastClass = "alternateBg";
                     return "<tr class=\"alternateBg\">";
                 }
                 else
                 {
                     lastClass = "";
                     return "<tr>";
                 }
             }
             else
             {
                 if (lastClass == "")
                 {
                     lastClass = "";
                     return "<tr>";
                 }
                 else
                 {
                     lastClass = "alternateBg";
                     return "<tr class=\"alternateBg\">";
                 } 
             }            
         }
         /// <summary>
         /// Date Created:   13/02/2012
         /// Created By:     Josephine Gad
         /// (description)   get default values
         /// ---------------------------------------
         /// Date Modified:  27/03/2012
         /// Modified By:    Josephine Gad
         /// (description)   Change dt or URLs from QueryString to uoHiddenFieldDate.value
         /// ---------------------------------------
         /// Date Modified:  17/10/2012
         /// Modified By:    Josephine Gad
         /// (description)   Change HotelDashboardDTO.HotelExceptionNoTravelRequestList to Session
         ///                 Remove GetHotelDashboardList
         /// </summary>
         /// <param name="LoadType"></param>
         protected void LoadDefaults(Int16 LoadType)
         {
             ViewState["InvalidRequest"] = "";
             string sBranchName;
             Int32 iBranchID;
             if (LoadType == 0)
             {
                 iBranchID = 0;
             }
             else
             {
                 iBranchID = GlobalCode.Field2Int(Session["Hotel"]);                
             }
             if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
             {
                 sBranchName = "";
             }
             else
             {
                 sBranchName = uoTextBoxSearch.Text.Trim();
             }
             uoHiddenFieldBranchID.Value = iBranchID.ToString();

             HotelDashboardBLL BLL = new HotelDashboardBLL();
             BLL.LoadHotelDashboardList(LoadType, GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue),
                 GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
                 GlobalCode.Field2Int(uoDropDownListCity.SelectedValue),
                 GlobalCode.Field2Int(uoDropDownListPort.SelectedValue),
                 uoHiddenFieldUser.Value,
                 uoHiddenFieldRole.Value,
                 GlobalCode.Field2Int(uoHiddenFieldBranchID.Value),
                 GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                 GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value)),
                 "",0,50,0);

             uoListViewDashboard.DataSource = null;
             uoListViewDashboard.DataSourceID = "uoObjectDataSourceDashboard";
             //uoListViewDashboard.DataBind();

             List<HotelExceptionNoTravelRequestList> NoTRReqList = (List<HotelExceptionNoTravelRequestList>)Session["HotelDashboardDTO_HotelExceptionNoTravelRequestList"];
             uoListViewExceptionNoTravelRequest.DataSource = NoTRReqList;//HotelDashboardDTO.HotelExceptionNoTravelRequestList;
             uoListViewExceptionNoTravelRequest.DataBind();
                         
         }
         /// <summary>
         /// Author:        Charlene Remotigue
         /// Date Created:  14/02/2012
         /// Description:   Book Seafarer to a hotel
         /// -----------------------------------------
         /// Modified By:   Josephine Gad
         /// Date Modifief: 06/03/2012
         /// Description:   Change Page.ClientScript to  ScriptManager
         ///                Set BranchID to 0 instead of Request.QueryString["branchId"]
         /// </summary>
         protected void BookSeafarer()
         {
                
         }

         public string ValidateRequest()
         {
             Boolean Invalid = false;

             if (ViewState["InvalidRequest"].ToString().Contains(Eval("TravelReqId").ToString()))
             {
                 Invalid = true;
             }
             else
             {
                 Invalid = false;
             }

             if (Invalid)
             {
                 return string.Format("background-color: #FFCC66; border: thin solid #000000; font-weight: bold");
             }
             else
             {
                 return string.Format("");
             }
         }

         /// <summary>
         /// Date Created:   28/09/2011
         /// Created By:     Josephine Gad
         /// (description)   Get regions
         /// ---------------------------------------------------------------------------
         /// Date Modified : 26/01/2012
         /// Modified By:    Gelo Oquialda
         /// (description)   change MapRefrence to Region
         /// </summary>
         private void BindRegion()
         {
             DataTable RegionDataTable = null;
             try
             {
                 RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                 uoDropDownListRegion.Items.Clear();
                 ListItem item = new ListItem("--SELECT REGION--", "0");
                 uoDropDownListRegion.Items.Add(item);
                 uoDropDownListRegion.DataSource = RegionDataTable;
                 uoDropDownListRegion.DataTextField = "colRegionNameVarchar";
                 uoDropDownListRegion.DataValueField = "colRegionIDInt";
                 uoDropDownListRegion.DataBind();

                 if (GlobalCode.Field2String(Session["Region"]) != "")
                 {
                     if (uoDropDownListRegion.Items.FindByValue(GlobalCode.Field2String(Session["Region"])) != null)
                     {
                         uoDropDownListRegion.SelectedValue = GlobalCode.Field2String(Session["Region"]);
                     }
                 }
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

         /// <summary>
         /// Date Created:   28/09/2011
         /// Created By:     Josephine Gad
         /// (description)   Get country by region id
         /// ---------------------------------------------------------------------------
         /// </summary>
         private void BindCountry()
         {
             DataTable CountryDataTable = null;
             try
             {
                 CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, ""); //uoTextBoxCountryName.Text                 
                 uoDropDownListCountry.Items.Clear();
                 ListItem item = new ListItem("--SELECT COUNTRY--", "0");
                 uoDropDownListCountry.Items.Add(item);
                 uoDropDownListCountry.DataSource = CountryDataTable;
                 uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                 uoDropDownListCountry.DataValueField = "colCountryIDInt";
                 uoDropDownListCountry.DataBind();

                 if (GlobalCode.Field2String(Session["Country"]) != "")
                 {
                     if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(Session["Country"])) != null)
                     {
                         uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(Session["Country"]);
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 if (CountryDataTable != null)
                 {
                     CountryDataTable.Dispose();
                 }
             }
         }

         /// <summary>
         /// Date Created:   28/09/2011
         /// Created By:     Josephine Gad
         /// (description)   Get city by country id
         /// ---------------------------------------------------------------------------
         /// </summary>

         private void BindCity()
         {
             DataTable CityDataTable = null;
             try
             {
                 CityDataTable = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxFilterCity.Text.Trim(), "0");
                 uoDropDownListCity.Items.Clear();
                 ListItem item = new ListItem("--SELECT CITY--", "0");
                 uoDropDownListCity.Items.Add(item);
                 uoDropDownListCity.DataSource = CityDataTable;
                 uoDropDownListCity.DataTextField = "colCityNameVarchar";
                 uoDropDownListCity.DataValueField = "colCityIDInt";
                 uoDropDownListCity.DataBind();

                 if (GlobalCode.Field2String(Session["City"]) != "")
                 {
                     if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(Session["City"])) != null)
                     {
                         uoDropDownListCity.SelectedValue = GlobalCode.Field2String(Session["City"]);
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             finally
             {
                 if (CityDataTable != null)
                 {
                     CityDataTable.Dispose();
                 }
             }
         }
         /// <summary>
         /// Date Created:   28/09/2011
         /// Created By:     Josephine Gad
         /// (description)   Get port by user and city
         /// ---------------------------------------------------------------------------         
         /// Date Modified:   06/07/2012
         /// Modified By:     Josephine Gad
         /// (description)    Change DataTable to List and use session
         /// </summary>
         private void BindPort()
         {             
             try
             {
                 List<PortList> list = new List<PortList>();
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
             }
             catch (Exception ex)
             {
                 throw ex;
             }             
         }

         /// <summary>
         /// Date Created:   16/03/2012
         /// Created By:     Gabriel Oquialda
         /// (description)   Set exception and no travel request groupings
         /// <summary>
         string lastDataFieldValue2 = null;
         protected string ExceptionNoTravelRequestAddGroup()
         {
             //Get the data field value of interest for this row                         
             string GroupValueString = "Title";

             string currentDataFieldValue = Eval(GroupValueString).ToString();

             //Specify name to display if dataFieldValue is a database NULL
             if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
             {
                 currentDataFieldValue = "";
             }
             //See if there's been a change in value
             if (lastDataFieldValue2 != currentDataFieldValue)
             {
                 //There's been a change! Record the change and emit the table row
                 lastDataFieldValue2 = currentDataFieldValue;
                 return string.Format("<tr><td class=\"group\" colspan=\"2\"><strong>{0}</strong></td></tr>", currentDataFieldValue);                 
             }
             else
             {
                 //No change, return an empty string
                 return string.Empty;
             }
         }
         
         /// <summary>
         /// Author:         Josephine Gad
         /// Date Created:   15/08/2012
         /// Description:    Get user details using session
         /// </summary>
         /// <returns></returns>
         private List<UserAccountList> GetUserAccountList(string sUserName)
         {
             List<UserAccountList> list = new List<UserAccountList>();

             if (Session["UserAccountList"] != null)
             {
                 list = (List<UserAccountList>)Session["UserAccountList"];
             }
             else
             {
                 list = UserAccountBLL.GetUserInfoListByName("sUserName");
                 Session["UserAccountList"] = list;
             }
             return list;
         }
        #endregion        
    }
}

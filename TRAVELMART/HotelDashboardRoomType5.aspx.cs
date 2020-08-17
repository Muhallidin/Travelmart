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
    public partial class HotelDashboardRoomType5 : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Modified:  02/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                 to avoid error in date conversion
        /// ---------------------------------------------------------------------------------------------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// -----------------------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date MOdified:  27/03/2012
        /// Description:    Add uoHiddenFieldPopupCalendar from Calendar popup to refresh Links
        /// 
        /// Modified by:    Jefferson Bermundo
        /// Date Modified:  05/07/2012
        /// Description:    Add uoDropDownListPortPerRegion to filter Port per Region
        /// -------------------------------------------
        /// Date Modified:  15/08/2012
        /// Modified By:    Josephine Gad
        /// (description)   Get uoHiddenFieldDateRange.Value from UserAccountList
        /// -------------------------------------------
        /// Date Modified:  07/05/2013
        /// Modified By:    Marco Abejar
        /// (description)   Set overflow and exception visibility for crew assist (jquery)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "Hotel dashboard information viewed.";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                //uoRadioButtonListContract_SelectedIndexChanged(uoRadioButtonListContract, null);


            }


            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            Session["strPrevPage"] = Request.RawUrl;
            string sDateFrom = GlobalCode.Field2String(Session["DateFrom"]);

            DateTime dt = GlobalCode.Field2DateTime(Request.QueryString["dt"]);
            DateTime dt2 = GlobalCode.Field2DateTime(sDateFrom);

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1" || uoHiddenFieldPopupHotel.Value == "1")
            {
                uoHiddenFieldDate.Value = dt2.ToShortDateString(); //gelo
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");


                //if (GlobalCode.Field2Int(Session["Region"]) == 0)
                //{
                //    Session["Region"] = 1;
                //}

                List<UserAccountList> listUser = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
                uoHiddenFieldDateRange.Value = GlobalCode.Field2String(listUser[0].iDayNo);

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    TRSearch.Visible = false;
                }

                if (uoRadioButtonListContract.SelectedValue == "1")
                {
                    LoadDefaults(0);
                }

            }
            //if ( || TravelMartVariable.RoleHotelVendor == uoHiddenFieldRole.Value )
            //{
            //    GetDashboard();
            //}
            uoHiddenFieldPopupHotel.Value = "0";
        }

        protected void uoDataPagerDashboard_PreRender(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
        }

        protected void uoObjectDataSourceDashboard_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["iRegionID"] = GlobalCode.Field2Int(Session["Region"]);
            e.InputParameters["iCountryID"] = GlobalCode.Field2Int(Session["Country"]);
            e.InputParameters["iCityID"] = GlobalCode.Field2Int(Session["City"]);
            e.InputParameters["iPortID"] = GlobalCode.Field2Int(Session["Port"]);

            e.InputParameters["sUserName"] = uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["iBranchID"] = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
            e.InputParameters["dFrom"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
            e.InputParameters["dTo"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);//.AddDays(double.Parse(uoHiddenFieldDateRange.Value));
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

        protected void uoObjectDataSourceException_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sUserName"] = uoHiddenFieldUser.Value;
            e.InputParameters["dFrom"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);

            e.InputParameters["iLoadType"] = GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value);
        }

        protected void uoObjectDataSourceOverflow_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sUserName"] = uoHiddenFieldUser.Value;
            e.InputParameters["dFrom"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);

            e.InputParameters["iLoadType"] = GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value);
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = uoTextBoxSearch.Text.Trim();
            uoHiddenFieldLoadType.Value = "1";
            GetDashboard();
            GetException();
            GetOverflow();
        }
        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRoom")
            {
                Response.Redirect("HotelDashboard3.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName"));
            }
        }
        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session.Remove("Port"); // remove the current selected Port 05/07/2012
            LoadDefaults(1);
        }

        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (uoRadioButtonListContract.SelectedValue == "1")
            {

                //uoDropDownListPortPerRegion.DataSource = null;
                //uoDropDownListPortPerRegion.Items.Clear();

                //lblOverflow.Visible = true;
                //lblException.Visible = true;
                //lblNoHotelRequest.Visible = true;
                //lblArrDepSameDate.Visible = true;
                //lblNoHotelContract.Visible = true;


                uoListViewDashboardNonContract.Visible = false;
                uoListViewDashboard.Visible = true;


                uoDropDownListRegion.Visible = true;
                uoLabelRegion.Visible = true;

                Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
                LoadDefaults(1);
            }
            else
            {

                //GetNotTurnPort
                //uoDropDownListPortPerRegion.DataSource = null;
                //uoDropDownListPortPerRegion.Items.Clear();

                //uoHyperLinkOverflow.Visible =
                //    //uoHyperLinkOverflow.Text = "Overflow(0)";

                //uoHyperLinkException.Visible = false;
                ////uoHyperLinkException.Text = "Exception(0)";

                //uoHyperLinkNoHotelRequest.Visible = false;
                ////uoHyperLinkNoHotelRequest.Text = "No Hotel request(0)";

                //uoHyperLinkArrDepSameDate.Visible = false;
                //uoHyperLinkNoHotelContract.Visible = true;
                //uoHyperLinkRestrictedNationality.Visible = false;


                //lblOverflow.Visible = false;
                //lblException.Visible = false;
                //lblNoHotelRequest.Visible = false;
                //lblArrDepSameDate.Visible = false;
                //lblNoHotelContract.Visible = false;

                uoListViewDashboardNonContract.Visible = true;
                uoListViewDashboard.Visible = false;

                uoDropDownListRegion.Visible = false;
                uoLabelRegion.Visible = false;

                LoadSeaport(1, GlobalCode.Field2Int(uoDropDownListPortPerRegion.SelectedValue), GlobalCode.Field2String(Session["UserName"]));
            }









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
        /// Date Created:   08/03/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Bind exception
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetException()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   08/03/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Bind overflow
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetOverflow()
        {
            try
            {
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
            //string GroupTextString = "Hotel Branch";
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
                string sEditOverrideLink = "";
                string sEditEmergencyLink = "";

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist
                    )
                {
                    sEditEmergencyLink = "&nbsp&nbsp <a class= \"clsEmergency\" style=\"font-size:x-small\" href=\"HotelRoomEmergencyEdit2.aspx?bID=" + Eval("BranchId") + "&rID=1&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "\">Emergency Room</a>";
                    sEditOverrideLink = sEditOverrideLink = "&nbsp&nbsp<a class=\"clsOverride\" style=\"font-size:x-small\" href=\"HotelRoomOverrideEdit2.aspx?bID=" + Eval("BranchId") + "&rID=1&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "\">Override Room</a>";

                    sEditOverrideLink += sEditEmergencyLink;
                }
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                string sURl = "HotelDashboard3.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName");

                //string sContract = "<td class=\"tdContract\"><a class=\"rightAligned\" title=\"View Contract\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"View Contract\" border=\"0\"/></a></td>";
                //string sNoContract = "<td class=\"tdContract\"><a class=\"rightAligned\" title=\"No Contract\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"No Contract\" border=\"0\"/></a></td>";                

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
                    sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong>" + sEditOverrideLink + "</td>" + sContract + "</tr>", currentDataFieldValue);
                    //return string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sContract + "" + sEvent + "</tr>", currentDataFieldValue);
                }
                else
                {
                    sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLinkExpired\" href=\"" + sURl + "\">{0}<a/></strong>" + sEditOverrideLink + "</td>" + sNoContract + "</tr>", currentDataFieldValue);
                    //sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLinkExpired\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong>" + sEditOverrideLink + "</td>" + sNoContract + "</tr>", currentDataFieldValue);
                    //return string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sNoContract + "" + sEvent + "</tr>", currentDataFieldValue);
                }
                return sReturn;
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        string lastDateFieldValue = null;
        protected string DashboardAddDateRow()
        {
            //string RowValueString = Eval("colDate").ToString();
            //string RowTextString = string.Format("{0:dd-MM-yyyy}", RowValueString);
            string RowTextString = string.Format("{0:dd-MMM-yyyy}", Eval("colDate"));
            string currentDataFieldValue = RowTextString;

            //See if there's been a change in value
            //if (lastDateFieldValue != currentDataFieldValue)
            //{                
            //There's been a change! Record the change and emit the table row
            lastDateFieldValue = currentDataFieldValue;
            //string sURl = "HotelDashboard2.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName");                

            string sReturn = "<tr><td class=\"leftAligned\"><label id=\"Label12\">Date:</label></td><td class=\"leftAligned\">" + RowTextString + "</td></tr>";

            if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
            {
                sReturn += "&nbsp; <a id=\"uoLinkButtonEvents\" href=\"#\" class=\"EventNotification\" title=\"With Event(s)\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\">*</a>";
                //<asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" Visible='<%# Eval("IsWithEvent") %>' ToolTip="With Event(s)"></asp:LinkButton>
            }
            sReturn += "<tr><td class=\"leftAligned\"><label id=\"Label12\">Day of the Week:</label></td><td class=\"leftAligned\">" + Eval("colDateName") + "</td></tr>";
            return sReturn;
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
        /// Date Modified:  22/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add binding of uoDropDownListRegion on first load
        /// ---------------------------------------
        /// Date Modified:  23/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Remove  iBranchID = GlobalCode.Field2Int(Session["Hotel"]), because there is no filter for branch in this design
        /// ---------------------------------------
        /// Modified By:    Josephine gad
        /// Date Modified:  03/10/2012
        /// Description:    Change HotelDashboardDTO.RegionList to List<RegionList> listRegion
        ///                 Change HotelDashboardDTO.HotelExceptionCount to Session["HotelDashboardDTO_HotelExceptionCount"]
        ///                 Change HotelDashboardDTO.HotelOverflowCount to Session["HotelDashboardDTO_HotelOverflowCount"]
        ///                 Change HotelDashboardDTO.NoTravelRequestCount to Session["HotelDashboardDTO_NoTravelRequestCount"]
        ///                 Change HotelDashboardDTO.ArrDeptSameOnOffDateCount to Session["HotelDashboardDTO_ArrDeptSameOnOffDateCount"]
        /// </summary>
        /// <param name="LoadType"></param>
        protected void LoadDefaults(Int16 LoadType)
        {
            string sBranchName;
            Int32 iBranchID = 0;
            if (LoadType == 0)
            {
                iBranchID = 0;
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

            if (LoadType == 0)
            {
                uoHiddenFieldFromDefaultView.Value = "0";
            }
            else
            {
                uoHiddenFieldFromDefaultView.Value = "1";
            }

            HotelDashboardBLL bll = new HotelDashboardBLL();

            string sBranch;
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            {
                sBranch = "";
            }
            else
            {
                sBranch = uoTextBoxSearch.Text.Trim();
            }

            bll.LoadHotelDashboardList2(GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value),
                GlobalCode.Field2Int(Session["Region"]),
                GlobalCode.Field2Int(Session["Country"]),
                GlobalCode.Field2Int(Session["City"]),
                GlobalCode.Field2Int(Session["Port"]),
                uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                GlobalCode.Field2Int(uoHiddenFieldBranchID.Value),
                GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                sBranch, 0, 50);

            //uoListViewDashboard.DataSource = null;
            uoListViewDashboard.DataSourceID = "uoObjectDataSourceDashboard";
            //uoListViewDashboard.DataBind();


            Int32 ExceptionCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_HotelExceptionCount"]);//(Int32)HotelDashboardDTO.HotelExceptionCount;
            if (ExceptionCount >= 0)
            {
                uoHyperLinkException.Visible = true;
                uoHyperLinkException.Text = "Exception(" + ExceptionCount.ToString() + ")";
                uoHyperLinkException.NavigateUrl = "/Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            }
            else
            {
                uoHyperLinkException.Visible = false;
            }

            Int32 OverflowCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_HotelOverflowCount"]);//(Int32)HotelDashboardDTO.HotelOverflowCount;
            if (OverflowCount >= 0)
            {
                uoHyperLinkOverflow.Visible = true;
                uoHyperLinkOverflow.Text = "Overflow(" + OverflowCount.ToString() + ")";
                uoHyperLinkOverflow.NavigateUrl = "/Hotel/HotelOverflowBooking3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            }
            else
            {
                uoHyperLinkOverflow.Visible = false;
            }

            Int32 NoTravelRequestCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_NoTravelRequestCount"]);//GlobalCode.Field2Int(HotelDashboardDTO.NoTravelRequestCount);
            if (NoTravelRequestCount >= 0)
            {
                uoHyperLinkNoHotelRequest.Visible = true;
                uoHyperLinkNoHotelRequest.Text = "No Travel Request(" + NoTravelRequestCount.ToString() + ")";
                uoHyperLinkNoHotelRequest.NavigateUrl = "NoTravelRequest2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            }
            else
            {
                uoHyperLinkNoHotelRequest.Visible = false;
            }

            Int32 ArrDeptSameOnOffDateCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_ArrDeptSameOnOffDateCount"]);//GlobalCode.Field2Int(HotelDashboardDTO.ArrDeptSameOnOffDateCount);
            if (ArrDeptSameOnOffDateCount >= 0)
            {
                uoHyperLinkArrDepSameDate.Visible = true;
                uoHyperLinkArrDepSameDate.Text = "Same Day Arr/Dep(" + ArrDeptSameOnOffDateCount.ToString() + ")";
                uoHyperLinkArrDepSameDate.NavigateUrl = "ArrivalDepartureSameDate.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            }
            else
            {
                uoHyperLinkArrDepSameDate.Visible = false;
            }

            Int32 NoHotelContractCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_NoContractCount"]);//GlobalCode.Field2Int(HotelDashboardDTO.NoContractCount);
            if (NoHotelContractCount >= 0)
            {
                uoHyperLinkNoHotelContract.Visible = true;
                uoHyperLinkNoHotelContract.Text = "Non Turn Ports(" + NoHotelContractCount.ToString() + ")";
                uoHyperLinkNoHotelContract.NavigateUrl = "/Hotel/HotelNonTurnPortsPA.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value + "&ex=" + "11";
            }
            else
            {
                uoHyperLinkNoHotelContract.Visible = false;
            }
            Int32 RestrictedNationalityCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_RestrictedNationalityCount"]);
            if (RestrictedNationalityCount >= 0)
            {
                uoHyperLinkRestrictedNationality.Visible = true;
                uoHyperLinkRestrictedNationality.Text = "Restricted Nationalities(" + RestrictedNationalityCount.ToString() + ")";
                uoHyperLinkRestrictedNationality.NavigateUrl = "RestrictedNationality.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            }

            if (LoadType == 0)
            {
                List<RegionList> listRegion = new List<RegionList>();
                listRegion = (List<RegionList>)Session["HotelDashboardDTO_RegionList"];

                uoDropDownListRegion.Items.Clear();
                uoDropDownListRegion.DataSource = listRegion;
                uoDropDownListRegion.DataTextField = "RegionName";
                uoDropDownListRegion.DataValueField = "RegionId";
                uoDropDownListRegion.DataBind();
                uoDropDownListRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));

                DateTime dtE1CHProcess = TMSettings.E1CHLastProcessedDate;
                uoLabelE1Processed.Text = "E1 Crew History Last Processed: " + dtE1CHProcess.ToString("MMM/dd/yyyy HH:mm tt");
            }
            string sRegion = GlobalCode.Field2String(Session["Region"]);
            if (sRegion != "")
            {
                if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
                {
                    uoDropDownListRegion.SelectedValue = sRegion;
                }
            }
            BindPort();

            
        }

        /// <summary>
        /// Date Created:   08/03/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Set exception title
        /// <summary>         
        protected string ExceptionTitle()
        {
            string Title = "EXCEPTION";
            string sURl = "~/Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            return string.Format("<tr><td class=\"group\" colspan=\"4\"><strong><a class=\"groupLink\"><a style=\"color: Red\" class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td></tr>", Title);
        }

        /// <summary>
        /// Date Created:   08/03/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Set overflow title
        /// <summary>         
        protected string OverflowTitle()
        {
            string Title = "OVERFLOW";
            string sURl = "~/Hotel/HotelOverflowBooking3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            return string.Format("<tr><td class=\"group\" colspan=\"4\"><strong><a class=\"groupLink\"><a style=\"color: Red\" class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td></tr>", Title);
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
        private void BindPort()
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




        protected void uoRadioButtonListContract_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButtonList snder = (RadioButtonList)sender;

                switch (snder.SelectedValue)
                {
                    case "1":

                        uoDropDownListPortPerRegion.DataSource = null;
                        uoDropDownListPortPerRegion.Items.Clear();

                        //lblOverflow.Visible = true;
                        //lblException.Visible = true;
                        //lblNoHotelRequest.Visible = true;
                        //lblArrDepSameDate.Visible = true;
                        //lblNoHotelContract.Visible = true;


                        LoadDefaults(0);

                        uoListViewDashboardNonContract.Visible = false;
                        uoListViewDashboard.Visible = true;


                        uoDropDownListRegion.Visible = true;
                        uoLabelRegion.Visible = true;
                        break;
                    case "2":

                        //GetNotTurnPort
                        uoDropDownListPortPerRegion.DataSource = null;
                        uoDropDownListPortPerRegion.Items.Clear();

                        uoHyperLinkOverflow.Visible =
                            //uoHyperLinkOverflow.Text = "Overflow(0)";

                       // uoHyperLinkException.Visible = false;
                            // //uoHyperLinkException.Text = "Exception(0)";

                       // uoHyperLinkNoHotelRequest.Visible = false;
                            // //uoHyperLinkNoHotelRequest.Text = "No Hotel request(0)";

                       // uoHyperLinkArrDepSameDate.Visible = false;
                            // uoHyperLinkNoHotelContract.Visible = true;
                            // uoHyperLinkRestrictedNationality.Visible = false;


                       //lblOverflow.Visible = false;
                            //lblException.Visible = false;
                            //lblNoHotelRequest.Visible = false;
                            //lblArrDepSameDate.Visible = false;
                            //lblNoHotelContract.Visible = false;

                       uoListViewDashboardNonContract.Visible = true;
                        uoListViewDashboard.Visible = false;

                        uoDropDownListRegion.Visible = false;
                        uoLabelRegion.Visible = false;


                        LoadSeaport(GlobalCode.GetselectedValue(uoDropDownListPortPerRegion, uoDropDownListPortPerRegion.SelectedValue) > 0 ? (short)1 : (short)0,
                                    GlobalCode.GetselectedValue(uoDropDownListPortPerRegion, uoDropDownListPortPerRegion.SelectedValue),
                                    GlobalCode.Field2String(Session["UserName"]));


                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void LoadSeaport(short LoadType, int PortID, string UserID)
        {
            try
            {

                HotelDashboardBLL bll = new HotelDashboardBLL();

                List<HotelDashBoardPAGenericClass> HotelDashBoard = new List<HotelDashBoardPAGenericClass>();

                HotelDashBoard = bll.GetNotTurnPort(LoadType, PortID, UserID, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value));

                if (LoadType == 0)
                {
                    uoDropDownListPortPerRegion.DataSource = null;
                    uoDropDownListPortPerRegion.Items.Clear();

                    ListItem item = new ListItem("--SELECT PORT--", "0");
                    uoDropDownListPortPerRegion.Items.Add(item);

                    uoDropDownListPortPerRegion.DataSource = HotelDashBoard[0].PortList;
                    uoDropDownListPortPerRegion.DataBind();
                }
                else if (LoadType == 1)
                {
                    uoDropDownListPortPerRegion.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPortPerRegion, PortID);
                }

                Session["PortAgentDTO"] = (from a in HotelDashBoard[0].HotelDashBoardPortAgentClass
                                           select new PortAgentDTO
                                           {
                                               PortAgentID = a.PortAgentID.ToString(),
                                               PortAgentName = a.PortAgentName
                                           }).ToList();


                uoListViewDashboardNonContract.DataSource = HotelDashBoard[0].HotelDashBoardPortAgentClass;
                uoListViewDashboardNonContract.DataBind();




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
        string lastDataFieldValuePortAgent = null;
        protected string DashboardAddGroupPortAgent()
        {
            //Get the data field value of interest for this row            
            //string GroupTextString = "Hotel Branch";
            //string GroupValueString = "HotelBranchName";
            //string currentDataFieldValue = Eval(GroupValueString).ToString();

            ////Specify name to display if dataFieldValue is a database NULL
            //if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            //{
            //    currentDataFieldValue = "";
            //}

            ////See if there's been a change in value
            //string sEditOverrideLink = "";
            //string sEditEmergencyLink = "";

            //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
            //    uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist
            //    )
            //{
            //    sEditEmergencyLink = "&nbsp&nbsp <a class= \"clsEmergency\" style=\"font-size:x-small\" href=\"HotelRoomEmergencyEdit2.aspx?bID=" + Eval("BranchId") + "&rID=1&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "\">Emergency Room</a>";
            //    sEditOverrideLink = sEditOverrideLink = "&nbsp&nbsp<a class=\"clsOverride\" style=\"font-size:x-small\" href=\"HotelRoomOverrideEdit2.aspx?bID=" + Eval("BranchId") + "&rID=1&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "\">Override Room</a>";

            //    sEditOverrideLink += sEditEmergencyLink;
            //}
            ////There's been a change! Record the change and emit the table row
            //lastDataFieldValue = currentDataFieldValue;
            //string sURl = "HotelDashboard3.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName");

            ////string sContract = "<td class=\"tdContract\"><a class=\"rightAligned\" title=\"View Contract\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"View Contract\" border=\"0\"/></a></td>";
            ////string sNoContract = "<td class=\"tdContract\"><a class=\"rightAligned\" title=\"No Contract\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"No Contract\" border=\"0\"/></a></td>";                

            //string sEvent = "";
            //if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
            //{
            //    //sEvent = "<td class=\"tdEvent\"><a class=\"rightAligned\" title=\"View Event(s)\" href=\"#\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"View Event(s)\" border=\"0\"/></a></td>";
            //    sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"View Event(s)\" href=\"#\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"View Event(s)\" border=\"0\"/></a>";
            //}
            //else
            //{
            //    //sEvent = "<td class=\"tdEvent\"><a class=\"rightAligned\" title=\"No Event(s)\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"No Event(s)\" border=\"0\"/></a></td>";
            //    //sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"No Event(s)\"><img ID=\"uoImageEvent\" src=\"Images/calendar1.png\" Width=\"20px\" alt=\"No Event(s)\" border=\"0\"/></a>";
            //    sEvent = "";
            //}

            //string sContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"View Contract\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"View Contract\" border=\"0\"/></a> " + sEvent + "</td>";
            ////string sNoContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"No Contract\"><img ID=\"uoImageContract\" src=\"Images/contract.jpg\" Width=\"20px\" alt=\"No Contract\" border=\"0\"/></a> " + sEvent + "</td>";
            //string sNoContract = "<td class=\"tdEvent\">" + sEvent + "</td>";

            //string sReturn;
            //if (Eval("IsWithContract").ToString() == "True")
            //{
            //    sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong>" + sEditOverrideLink + "</td>" + sContract + "</tr>", currentDataFieldValue);
            //    //return string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sContract + "" + sEvent + "</tr>", currentDataFieldValue);
            //}
            //else
            //{
            //    //return string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td>" + sNoContract + "" + sEvent + "</tr>", currentDataFieldValue);
            //}


            string sReturn = "";
            //sReturn = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong>" + sEditOverrideLink + "</td>" + sNoContract + "</tr>", currentDataFieldValue);



            return sReturn;














        }



        #endregion
    }
}


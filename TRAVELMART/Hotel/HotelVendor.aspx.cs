using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Text;

namespace TRAVELMART.Hotel
{
    public partial class HotelVendor : System.Web.UI.Page
    {
        #region Events
        DashboardBLL BLL = new DashboardBLL();
        public string lastStatus = null;

        /// <summary>
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
        /// Date Modified:  11/10/2012
        /// Modified By:    Josephine Gad
        /// (description)   Set uoListViewDashboard datasource to null and bind it to view the header on first load
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            Session["strPrevPage"] = Request.RawUrl;
            string sDateFrom = GlobalCode.Field2String(Session["DateFrom"]);

            DateTime dt = GlobalCode.Field2DateTime(Request.QueryString["dt"]);
            DateTime dt2 = GlobalCode.Field2DateTime(sDateFrom);

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "Hotel Vendor Page Viewed";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {
                uoHiddenFieldDate.Value = dt2.ToShortDateString();
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                List<UserAccountList> listUser = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
                uoHiddenFieldDateRange.Value = GlobalCode.Field2String(listUser[0].iDayNo);

                //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                //{
                //    TRSearch.Visible = false;
                //}
                uoListViewDashboard.DataSource = null;
                uoListViewDashboard.DataBind();
                LoadDefaults(0);

                LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");
                HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");

                uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                uoLinkExport.Visible = false;
                ucSpanViewWeek.Visible = false;
                ucSpanExportALL.Visible = false;
            }
            uoHiddenFieldPopupHotel.Value = "0";

            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {
                SetDefaults(0);
            }
            uoHiddenFieldConfirmation.Value = (Session["ConfirmationTag"] != null) ? Session["ConfirmationTag"].ToString() : "0";
            if (uoHiddenFieldConfirmation.Value == "1")
            {
                SetDefaults(0);
                uoHiddenFieldConfirmation.Value = "0";
                Session["ConfirmationTag"] = "0";
            }
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
            e.InputParameters["iBranchID"] = GlobalCode.Field2Int(uoHiddenFieldBranchId.Value);
            e.InputParameters["dFrom"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
            e.InputParameters["dTo"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);//.AddDays(double.Parse(uoHiddenFieldDateRange.Value));
            e.InputParameters["sBranchName"] = "";

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
            Session["HotelNameToSearch"] = "";// uoTextBoxSearch.Text.Trim();
            uoHiddenFieldLoadType.Value = "1";
            GetDashboard();
            //GetException();
            //GetOverflow();
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
        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Session["Region"] = uoDropDownListRegion.SelectedValue;
        //    Session.Remove("Port");
        //    LoadDefaults(1);
        //}

        //protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
        //    LoadDefaults(1);
        //}
        protected void uoListViewTR_ItemCommand(object sender, ListViewCommandEventArgs e)
        
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            if (e.CommandName == "Tag")
            {
                string arg = e.CommandArgument.ToString();
                string[] args = arg.Split(":".ToCharArray());
                if (args.Count() > 1)
                {
                    TagSeafarer(args[0].ToString(), args[1].ToString(), args[2].ToString());
                }
            }
        }
        protected void uoDashBoardListDetailsPager_PreRender(object sender, EventArgs e)
        {
            uoHiddenFieldFromDefaultView.Value = "1";
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
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
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
        /// <summary>
        string lastDataFieldValue = null;
        protected string DashboardAddGroup()
        {
            string GroupValueString = "HotelBranchName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();
            if (currentDataFieldValue != "")
            {
                uoHiddenFieldHotelName.Value = currentDataFieldValue;
            }

            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            if (lastDataFieldValue != currentDataFieldValue)
            {
                lastDataFieldValue = currentDataFieldValue;

                string sEvent = "";
                if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
                {
                    sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"View Event(s)\" href=\"#\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\"><img ID=\"uoImageEvent\" src=\"../Images/calendar1.png\" Width=\"20px\" alt=\"View Event(s)\" border=\"0\"/></a>";
                }
                else
                {
                    //sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"No Event(s)\"><img ID=\"uoImageEvent\" src=\"../Images/calendar1.png\" visible=\"false\" Width=\"20px\" alt=\"No Event(s)\" border=\"0\"/></a>";
                    sEvent = "<a id=\"uoEvent\" class=\"rightAligned\" title=\"No Event(s)\"></a>";
                }

                string sContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"View Contract\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\"><img ID=\"uoImageContract\" src=\"../Images/contract.jpg\" Width=\"20px\" alt=\"View Contract\" border=\"0\"/></a> " + sEvent + "</td>";
                //string sNoContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"No Contract\"><img ID=\"uoImageContract\"  visible=\"false\" src=\"../Images/contract.jpg\" Width=\"20px\" alt=\"No Contract\" border=\"0\"/></a> " + sEvent + "</td>";
                string sNoContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"No Contract\"></a> " + sEvent + "</td>";

                string sResult = "";
                if (Eval("IsWithContract").ToString() == "True")
                {
                    sResult = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><span class=\"leftAligned\">{0}</span></strong></td>" + sContract + "</tr>", currentDataFieldValue);
                }
                else
                {
                    sResult = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"#\">{0}<a/></strong></td>" + sNoContract + "</tr>", currentDataFieldValue);
                }
                return sResult;
            }
            else
            {
                return string.Empty;
            }
        }
        string lastDateFieldValue = null;

        protected string DashboardAddDateRow()
        {
            string RowTextString = string.Format("{0:dd-MMM-yyyy}", Eval("colDate"));
            string currentDataFieldValue = RowTextString;

            lastDateFieldValue = currentDataFieldValue;
            string sReturn = "<tr><td class=\"leftAligned\"><label id=\"Label12\">Date:</label></td><td class=\"leftAligned\">" + RowTextString + "</td></tr>";

            if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
            {
                sReturn += "&nbsp; <a id=\"uoLinkButtonEvents\" href=\"#\" class=\"EventNotification\" title=\"With Event(s)\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\">*</a>";
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
            if (lastDateFieldValue2 != currentDataFieldValue)
            {
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
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  03/10/2012
        /// Description:    Change HotelDashboardDTO.RegionList to List<RegionList> listRegion
        ///                 Change HotelDashboardDTO.HotelExceptionCount to Session["HotelDashboardDTO_HotelExceptionCount"]
        ///                 Change HotelDashboardDTO.HotelOverflowCount to Session["HotelDashboardDTO_HotelOverflowCount"]
        ///                 Change HotelDashboardDTO.NoTravelRequestCount to Session["HotelDashboardDTO_NoTravelRequestCount"]
        ///                 Change HotelDashboardDTO.ArrDeptSameOnOffDateCount to Session["HotelDashboardDTO_ArrDeptSameOnOffDateCount"]
        /// -------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  11/10/2012
        /// Description:    Remove uoDropDownListRegion
        /// -------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  28/Nov/2012
        /// Description:    Add LoadHotelDashboardList2 for the default value
        ///                 Remove unnecessary links
        /// -------------------------------------------
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

            if (Session["UserBranchId"] != null)
            {
                int.TryParse(Session["UserBranchId"].ToString(), out iBranchID);
                uoHiddenFieldBranchId.Value = Session["UserBranchId"].ToString();
            }
            uoHiddenFieldBranchId.Value = iBranchID.ToString();

            HotelDashboardBLL bll = new HotelDashboardBLL();
            bll.LoadHotelDashboardList2(GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value),
              GlobalCode.Field2Int(Session["Region"]),
              GlobalCode.Field2Int(Session["Country"]),
              GlobalCode.Field2Int(Session["City"]),
              GlobalCode.Field2Int(Session["Port"]),
              uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
              iBranchID,
              GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
              GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
              "", 0, 50);

            //uoListViewDashboard.DataSource = null;
            uoListViewDashboard.DataSourceID = "uoObjectDataSourceDashboard";
            //uoListViewDashboard.DataBind();

            //Int32 ExceptionCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_HotelExceptionCount"]);//(Int32)HotelDashboardDTO.HotelExceptionCount;
            //if (ExceptionCount >= 0)
            //{
            //    uoHyperLinkException.Visible = true;
            //    uoHyperLinkException.Text = "Exception(" + ExceptionCount.ToString() + ")";
            //    uoHyperLinkException.NavigateUrl = "/Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            //}
            //else
            //{
            //    uoHyperLinkException.Visible = false;
            //}

            //Int32 OverflowCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_HotelOverflowCount"]);//(Int32)HotelDashboardDTO.HotelOverflowCount;
            //if (OverflowCount >= 0)
            //{
            //    uoHyperLinkOverflow.Visible = true;
            //    uoHyperLinkOverflow.Text = "Overflow(" + OverflowCount.ToString() + ")";
            //    uoHyperLinkOverflow.NavigateUrl = "/Hotel/HotelOverflowBooking3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            //}
            //else
            //{
            //    uoHyperLinkOverflow.Visible = false;
            //}

            //Int32 NoTravelRequestCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_NoTravelRequestCount"]);//GlobalCode.Field2Int(HotelDashboardDTO.NoTravelRequestCount);
            //if (NoTravelRequestCount >= 0)
            //{
            //    uoHyperLinkNoHotelRequest.Visible = true;
            //    uoHyperLinkNoHotelRequest.Text = "No Travel Request(" + NoTravelRequestCount.ToString() + ")";
            //    uoHyperLinkNoHotelRequest.NavigateUrl = "NoTravelRequest2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            //}
            //else
            //{
            //    uoHyperLinkNoHotelRequest.Visible = false;
            //}

            //Int32 ArrDeptSameOnOffDateCount = GlobalCode.Field2Int(Session["HotelDashboardDTO_ArrDeptSameOnOffDateCount"]);//GlobalCode.Field2Int(HotelDashboardDTO.ArrDeptSameOnOffDateCount);
            //if (ArrDeptSameOnOffDateCount >= 0)
            //{
            //    uoHyperLinkArrDepSameDate.Visible = true;
            //    uoHyperLinkArrDepSameDate.Text = "Same Day Arr/Dep(" + ArrDeptSameOnOffDateCount.ToString() + ")";
            //    uoHyperLinkArrDepSameDate.NavigateUrl = "ArrivalDepartureSameDate.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            //}
            //else
            //{
            //    uoHyperLinkArrDepSameDate.Visible = false;
            //}
        }

        /// <summary>
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
        /// <summary>         
        protected string ExceptionTitle()
        {
            string Title = "EXCEPTION";
            string sURl = "~/Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            return string.Format("<tr><td class=\"group\" colspan=\"4\"><strong><a class=\"groupLink\"><a style=\"color: Red\" class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td></tr>", Title);
        }

        /// <summary>
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
        /// <summary>         
        protected string OverflowTitle()
        {
            string Title = "OVERFLOW";
            string sURl = "~/Hotel/HotelOverflowBooking3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            return string.Format("<tr><td class=\"group\" colspan=\"4\"><strong><a class=\"groupLink\"><a style=\"color: Red\" class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></strong></td></tr>", Title);
        }

        /// <summary>
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Clean Copy of HotelView4
        /// -------------------------------------------
        /// </summary>
        //private void BindPort()
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
        /// <summary>
        /// Date Modified:  16/08/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   remove apostrophe on sUserName list 
        /// -------------------------------------------
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
                list = UserAccountBLL.GetUserInfoListByName(sUserName);
                Session["UserAccountList"] = list;
            }
            return list;
        }

        protected void SetDefaults(Int16 LoadType)
        {
            ViewState["InvalidRequest"] = "";
            Session["strPrevPage"] = Request.RawUrl;
            Session["ViewLeftMenu"] = "0";

            uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            uoHiddenFieldBranchId.Value = Session["UserBranchId"].ToString();
            //JHO--for hotel specialist only
            BLL.LoadAllHotelDashboard2Tables2(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), GlobalCode.Field2Int(uoHiddenFieldBranchId.Value),
                LoadType, uoHiddenFieldUser.Value, 0, 20);
            uoHiddenFieldFromDefaultView.Value = "0";
            LoadConfirmedBookings();

        }
        /// <summary>
        /// Date Modified: 19/10/2012
        /// Modified By:   Josephine Gad
        /// (description)  Remove GetConfirmBooking and assign the datasource directly
        /// ------------------------------------------- 
        /// </summary>
        protected void LoadConfirmedBookings()
        {
            //ObjectDataSource1.TypeName = "TRAVELMART.Common.HotelDashboardClass";
            //ObjectDataSource1.SelectCountMethod = "GetConfirmBookingCount";
            //ObjectDataSource1.SelectMethod = "GetConfirmBooking";

            uoDashboardListDetails.DataSourceID = ObjectDataSource1.UniqueID;
            uoUpdatePanelDetails.Update();
        }

        //protected string SetStatus()
        //{
        //    string StatusTitle = "Status :";
        //    string currentStatus = Eval("HotelStatus").ToString();

        //    if (currentStatus.Length == 0)
        //    {
        //        currentStatus = "";
        //    }

        //    if (lastStatus != currentStatus)
        //    {
        //        lastStatus = currentStatus;
        //        return string.Format("<tr><td class=\"group\" colspan=\"24\">{0} <strong>{1}</strong></td></tr> ", StatusTitle, currentStatus);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        private void TagSeafarer(string sIdBigint, string sTRId, string sBranch)
        {
            string sUser = uoHiddenFieldUser.Value;
            string sRole = uoHiddenFieldRole.Value;
            string sTagTime = uoHiddenFieldTagTime.Value;

            //SeafarerTravelBLL.InsertTag(sIdBigint, sTRId, sUser, sRole, "0", "0", sBranch, sTagTime);
            //if (sBranch != "0")
            //{
            //    SetDefaults(0);
            //}
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 04/10/2012
        /// Description: pop up alert message
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
        /// ===============================================
        /// Author: Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: export Exception Bookings to excel
        /// ===============================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  07/Jan/2013
        /// Description:    Use table instead of list using DashboardBLL.LoadHotelDashboardConfirmedExport
        /// ===============================================
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            uoHiddenFieldFromDefaultView.Value = "0";
            DataTable dt = null;
            try
            {
                //List<ConfirmBooking> list = new List<ConfirmBooking>();
                //list = (List<ConfirmBooking>)Session["HotelDashboardClass_ConfirmBooking"];

                //if (list.Count > 0)
                //{
                //    if (list.Count > 20)
                //        ExportException(list);
                //    else
                //        ExportException(list);
                //}
                dt = DashboardBLL.LoadHotelDashboardConfirmedExport(uoHiddenFieldUser.Value,GlobalCode.Field2Int(uoHiddenFieldBranchId.Value));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        CreateFile(dt);
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

        private void ExportException(List<ConfirmBooking> list)
        {
            try
            {
                DataTable exception = getDataTable(list);
                foreach (DataRow dr in exception.Rows)
                {
                    if (dr["HotelRequest"].ToString() == "False")
                        dr["HotelRequest"] = "No";
                    else
                        dr["HotelRequest"] = "Yes";

                    if (dr["WithSailMaster"].ToString() == "False")
                        dr["WithSailMaster"] = "No";
                    else
                        dr["WithSailMaster"] = "Yes";

                    if (dr["WithSailMaster"].ToString() == "False")
                        dr["WithSailMaster"] = "No";
                    else
                        dr["WithSailMaster"] = "Yes";

                }

                CreateFile(exception);
                if (exception != null)
                {
                    exception.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/HotelManifest/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value).ToString("MMM_dd_yyy");
            string FileName = "HotelVendolList_" + sDateManifest + "_" + sDate + ".xls";
            strFileName = FilePath + FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            CreateExcel(dt, strFileName, uoHiddenFieldHotelName.Value);
            OpenExcelFile(FileName, strFileName);
        }



        public static void CreateExcel(DataTable dtSource, string strFileName, string sHotelName)
        {
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 32;
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
                xtwWriter.WriteAttributeString("ss", "Name", null, sHotelName);

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
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
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

            string strScript = "CloseModal('../Extract/HotelManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
        }


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: 
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
        /// Author:         Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        #endregion


    }
}

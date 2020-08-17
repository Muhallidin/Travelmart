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
    public partial class HotelDashboardRoomType2 : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                    to avoid error in date conversion
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
             Session["strPrevPage"]  = Request.RawUrl; //gelo   
            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); //gelo
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;

                List<UserAccountList> listUser = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
                uoHiddenFieldDateRange.Value = GlobalCode.Field2String(listUser[0].iDayNo); 
                //uoTextBoxSearch.Text = GlobalCode.Field2String(Session["HotelNameToSearch"]);                                             

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    TRSearch.Visible = false;
                }                
                Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");                
                uclabelStatus.Visible = false;

                HtmlControl uoRowDateTo = (HtmlControl)Master.FindControl("uoRowDateTo");
                uoRowDateTo.Visible = false;

                LoadDefaults(0);
                //uoHiddenFieldLoadType.Value = "0";                                                
            }            
            if (uoHiddenFieldPopupHotel.Value == "1" || TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserRole"]))
            {
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
            e.InputParameters["iRegionID"] = GlobalCode.Field2Int( Session["Region"] .ToString());
            e.InputParameters["iCountryID"] = GlobalCode.Field2Int( Session["Country"] .ToString());
            e.InputParameters["iCityID"] = GlobalCode.Field2Int( Session["City"] .ToString());
            e.InputParameters["iPortID"] = GlobalCode.Field2Int( Session["Port"] .ToString());

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
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
             Session["Hotel"] = null;
            Session["HotelNameToSearch"] = uoTextBoxSearch.Text.Trim();
            uoHiddenFieldLoadType.Value = "1";
            GetDashboard();
        }
        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRoom")
            {
                Response.Redirect("HotelDashboard.aspx?ufn=" + Request.QueryString["ufn"].ToString());
            }
        }
        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (e.Item.ItemType == ListViewItemType.DataItem)
            //{
            //    LinkButton uoLinkButtonEvents = (LinkButton)e.Item.FindControl("uoLinkButtonEvents");
            //    HiddenField uoHiddenFieldBranchID = (HiddenField)e.Item.FindControl("uoHiddenFieldBranchID");

            //    string[] sBranchArr = uoHiddenFieldBranchID.Value.Split("_".ToCharArray());
            //    string sBranch = sBranchArr[0].ToString();
            //    string sCity = sBranchArr[1].ToString();
            //    string sDate = sBranchArr[2].ToString();

            //    string scriptEventString = "return OpenEventsList('" + sBranch + "', '" + sCity + "', '" + sDate + "');";
            //    uoLinkButtonEvents.Attributes.Add("OnClick", scriptEventString);
            //}
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
                    return string.Format("<tr><td class=\"group\" colspan=\"17\">{0}: <strong><a class=\"groupLink\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\")'\">{1}<a/></strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
                else
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"17\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
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
            if (lastDateFieldValue != currentDataFieldValue)
            {                
                //There's been a change! Record the change and emit the table row
                lastDateFieldValue = currentDataFieldValue;
                string sURl = "HotelDashboard2.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName");
                //string sReturn = string.Format("<td class=\"leftAligned\"><a class=\"leftAligned\" href=\"" + sURl + "\">{0}<a/></td> <td  class=\"leftAligned\">" + Eval("colDateName"), RowTextString);
                string sReturn = "<td class=\"leftAligned\"><a class=\"leftAligned\" href=\"" + sURl + "\">" + RowTextString + "<a/>";
                if (GlobalCode.Field2Bool(Eval("IsWithEvent")))
                {
                    sReturn += "&nbsp; <a id=\"uoLinkButtonEvents\" href=\"#\" class=\"EventNotification\" title=\"With Event(s)\" OnClick=\"return OpenEventsList('" + Eval("BranchID") + "', '0', '" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "');\">*</a>";
                    //<asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" Visible='<%# Eval("IsWithEvent") %>' ToolTip="With Event(s)"></asp:LinkButton>
                }
                sReturn += "<td  class=\"leftAligned\">" + Eval("colDateName") + "</td>";
                return sReturn;
            }
            else
            {
                //No change, return an empty string
                return "<td></td><td></td>";
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
        /// </summary>
        /// <param name="LoadType"></param>
         protected void LoadDefaults(Int16 LoadType)
         {                                      
             string sBranchName;
             Int32 iBranchID;
             if (LoadType == 0)
             {
                 iBranchID = 0;
             }
             else
             {
                 iBranchID = GlobalCode.Field2Int(Session["Hotel"].ToString());                
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
             //BLL.GetHotelDashboardList(LoadType, GlobalCode.Field2Int( Session["Region"] .ToString()),
             //GlobalCode.Field2Int( Session["Country"] .ToString()),
             //GlobalCode.Field2Int( Session["City"] .ToString()),
             //GlobalCode.Field2Int( Session["Port"] .ToString()),
             //uoHiddenFieldUser.Value,
             //uoHiddenFieldRole.Value,
             //iBranchID,
             //GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
             //GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value)),
             //sBranchName, 0, 20);

             //Int32 ExceptionCount = (Int32)HotelDashboardDTO.HotelExceptionCount;
             //if (ExceptionCount > 0)
             //{
             //    uoHyperLinkException.Visible = true;
             //    uoHyperLinkException.Text = "(" + ExceptionCount.ToString() + ") Exception Bookings";
             //    uoHyperLinkException.NavigateUrl = "Hotel/HotelOverflowBooking2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];
             //}
             //else
             //{
             //    uoHyperLinkException.Visible = false;
             //}

             uoObjectDataSourceDashboard.TypeName = "TRAVELMART.Common.HotelDashboardDTO";
             uoObjectDataSourceDashboard.SelectMethod = "GetHotelDashboardList";
             uoObjectDataSourceDashboard.SelectCountMethod = "GetHotelDashboardListCount";
             
             uoListViewDashboard.DataSource = null;
             uoListViewDashboard.DataSourceID = "uoObjectDataSourceDashboard";
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

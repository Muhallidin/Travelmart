using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.Security;
using System.IO;
using AjaxControlToolkit;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Text;

namespace TRAVELMART.Hotel
{
    public partial class HotelExceptionBooking : System.Web.UI.Page
    {


        #region DECLARATIONS
        ExceptionBLL ExceptionBLL = new ExceptionBLL();
        HotelDashboardBLL dashboardBLL = new HotelDashboardBLL();
        HotelBookingsBLL bookingsBLL = new HotelBookingsBLL();
        
        private AsyncTaskDelegate dlgtException;

        protected delegate void AsyncTaskDelegate();

        #endregion

        #region EVENT
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

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

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                PageAsyncTask TaskExcp = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async", true);
                Page.RegisterAsyncTask(TaskExcp);


            }
        }
        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            dlgtException = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = dlgtException.BeginInvoke(cb, extraData);
            return result;
        }
        public void OnEndExceptions(IAsyncResult ar)
        {
            try
            {
                List<ExceptionPageData> ExceptionPageData = new List<ExceptionPageData>();
                
                uoListViewExceptionList.DataSource = null;
                uoListViewExceptionList.DataBind();

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
                if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
                {
                    Session["strPrevPage"] = Request.RawUrl;

                    uoHiddenFieldRoomType.Value = "";

                  ExceptionPageData =  ExceptionBLL.GetExceptionPageData(0, uoHiddenFieldUser.Value.ToString()
                        , GlobalCode.Field2DateTime(uoHiddenFieldDate.Value)
                        , GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue)
                        , GlobalCode.Field2Int(uoDropDownListPortPerRegion.SelectedValue)
                        , 0, "");
                



                }

                if (ExceptionPageData.Count > 0) 
                {

                    uoDropDownListPortPerRegion.DataSource = ExceptionPageData[0].PortList;
                    uoDropDownListPortPerRegion.DataTextField = "PortName";
                    uoDropDownListPortPerRegion.DataValueField = "PortId";
                    uoDropDownListPortPerRegion.DataBind();
                    uoDropDownListPortPerRegion.Items.Insert(0, new ListItem("--SELECT PORT--", "0"));


                    uoDropDownListRegion.DataSource = ExceptionPageData[0].RegionList;
                    uoDropDownListRegion.DataTextField = "RegionName";
                    uoDropDownListRegion.DataValueField = "RegionId";
                    uoDropDownListRegion.DataBind();
                    uoDropDownListRegion.Items.Insert(0, new ListItem("--SELECT PORT--", "0"));

                    uoListViewExceptionList.DataSource = ExceptionPageData[0].ExceptionBooking;
                    uoListViewExceptionList.DataBind();

                    uoRepeaterHotels.DataSource = ExceptionPageData[0].Hotels;
                    uoRepeaterHotels.DataBind();
                }
      




                if (uoHiddenFieldPopupHotel.Value == "1")
                {
                    // GetRoomBlocks(GlobalCode.Field2Int(uoHiddenFieldBranch.Value), false);
                }
                if (uoHiddenFieldRemoveFromList.Value == "1")
                {
                    Session["HotelTransactionExceptionExceptionBooking"] = null;

                }
                //uoHiddenFieldPopupHotel.Value = "0";
                uoHiddenFieldRemoveFromList.Value = "0";
            }
            catch { }
        }

        #endregion



        #region METHOD


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

            if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
            {
                uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            }
            else
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
            }

            ListViewExceptionHeader.DataSource = null;
            ListViewExceptionHeader.DataBind();
        }



        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/04/2012
        /// Description: set date
        /// </summary>
        string lastDate = null;
        public string setDate()
        {
            string currentDate = Eval("colDate").ToString();

            //GlobalCode.Field2String(Session["UserVendor"]) = Eval("colBrandId").ToString();
            //GlobalCode.Field2String(Session["UserBranchID"]) = Eval("colBranchId").ToString();

            if (currentDate.Length == 0)
            {
                currentDate = "";
            }

            if (lastDate != currentDate)
            {
                lastDate = currentDate;
                DateTime dt = GlobalCode.Field2DateTime(currentDate);
                return String.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return "";
            }
        }




        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: set alternate color
        /// </summary>
        /// <returns></returns> 
        string lastDataFieldValue = null;
        string lastClass = "alternateBg";
        public string OverflowChangeRowColor()
        {
            string currentDataFieldValue = Eval("SeafarerId").ToString();
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
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

        public bool SetContractVisibility()
        {
            return Convert.ToBoolean(Eval("withContract").ToString());
        }

        public bool SetEventVisibility()
        {
            return Convert.ToBoolean(Eval("withEvent").ToString());
        }

        #endregion
       
    }
}



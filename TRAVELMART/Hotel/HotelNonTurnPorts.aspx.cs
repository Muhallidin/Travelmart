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
using Excel = Microsoft.Office.Interop.Excel;

//using System.Threading;
namespace TRAVELMART.Hotel
{
    public partial class HotelNonTurnPorts : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        #region DECLARATIONS
        NoHotelContractBLL ExceptionBLL = new NoHotelContractBLL();
        HotelDashboardBLL dashboardBLL = new HotelDashboardBLL();
        HotelBookingsBLL bookingsBLL = new HotelBookingsBLL();
        #endregion

        #region EVENTS


        private AsyncTaskDelegate _dlgt;
        private AsyncTaskDelegate _dlgtHotel;
        private AsyncTaskDelegate _dlgtException;
        // Create delegate. 
        protected delegate void AsyncTaskDelegate();

        /// <summary>
        /// Modified by:    Muhallidin G wali
        /// Date MOdified:  15/03/2012
        /// Description:    Change TravelMartVariable to Session
        /// ====================================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void Page_Load(object sender, EventArgs e)
        {
            //InitializeValues();
            ListView1.DataSource = null;
            ListView1.DataBind();

            uoListViewHeaderConfirmed.DataSource = null;
            uoListViewHeaderConfirmed.DataBind();

            uoListViewHeaderCancelled.DataSource = null;
            uoListViewHeaderCancelled.DataBind();

            if (!IsPostBack)
            {
                InitializeValues();
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;

                string strLogDescription = "Non Turn Port Manifest Page Viewed.";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(uoDropDownListPort.SelectedValue), 
                    "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

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
                Session["strPrevPage"] = Request.RawUrl;
                SetDefaults();

               
                //GetHotels();
                
                uoHiddenFieldRoomType.Value = "";
                

                PageAsyncTask TaskPort = new PageAsyncTask(OnBegin, OnEnd, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort);

                //BindGridViewNew();
            }
            if (uoHiddenFieldSendEmail.Value == "1")
            {
                SendEmailButton(false);
            }
            if (uoHiddenFieldSendSaveEmail.Value == "1")
            {
                SendEmailButton(true);
            }
            uoHiddenFieldSendEmail.Value = "0";
            uoHiddenFieldSendSaveEmail.Value = "0";
            //if (uoHiddenFieldPopupHotel.Value == "1")
            //{
            //    GetRoomBlocks(GlobalCode.Field2Int(uoHiddenFieldBranch.Value), false);
            //}
            //uoHiddenFieldPopupHotel.Value = "0";

        }


        public void ExecuteAsyncTask() { }

        
        public IAsyncResult OnBegin(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgt = new AsyncTaskDelegate(ExecuteAsyncTask);
            IAsyncResult result = _dlgt.BeginInvoke(cb, extraData);
            return result;
        }

      
        public void OnEnd(IAsyncResult ar)
        {
            //_taskprogress += "AsyncTask completed at: " + DateTime.Now;
            _dlgt.EndInvoke(ar);
            //BindRegionList();
            BindPortList();
            GetExceptions();
        }

        public void OnEndPort(IAsyncResult ar)
        {
            _dlgt.EndInvoke(ar);
            BindPortList();
        }

        public void OnTimeout(IAsyncResult ar)
        {
            //_taskprogress += "AsyncTask failed to complete " + "because it exceeded the AsyncTimeout parameter.";
        }


        //protected void uoButtonCancel_Click(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in uoRepeaterHotels.Items)
        //    {
        //        CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");
        //        cpe.ClientState = "true";
        //        cpe.Collapsed = true;
        //    }
        //}

        //protected void uoButtonSave_Click(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in uoRepeaterHotels.Items)
        //    {
        //        LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");
        //        TextBox txtCheckIn = (TextBox)item.FindControl("uoTxtBoxCheckIn");
        //        TextBox txtDays = (TextBox)item.FindControl("uoTxtBoxDays");
        //        DropDownList RoomType = (DropDownList)item.FindControl("uoDropDownListRoomType");
        //        RadioButtonList Source = (RadioButtonList)item.FindControl("uoRBtnAllocations");

        //        if (lnkBtn.CommandArgument.ToString() == uoHiddenFieldBranch.Value)
        //        {
        //            int numOfPeople = GetSeafarersToBook();
        //            DateTime CheckinDt = GlobalCode.Field2DateTime(txtCheckIn.Text);

        //            uoHIddenFieldSFCount.Value = numOfPeople.ToString();
        //            txtDays.Text = uoHiddenFieldNoOfNites.Value;
        //            uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(txtCheckIn.Text).AddDays(GlobalCode.Field2Int(txtDays.Text)).ToShortDateString();

        //            Session["HotelBookingsRoomBlocks"] = null;
        //            List<RoomBlocks> list = new List<RoomBlocks>();
        //            list = bookingsBLL.getRemainingRooms(CheckinDt,
        //                GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value),
        //                numOfPeople, GlobalCode.Field2Int(RoomType.SelectedValue), GlobalCode.Field2Int(uoHiddenFieldBranch.Value));

        //            Session["HotelBookingsRoomBlocks"] = list;

        //            CheckRoomValidity(CheckinDt, lnkBtn.Text, GlobalCode.Field2Int(uoHiddenFieldBranch.Value),
        //                RoomType.SelectedItem.Text, GlobalCode.Field2Int(Source.SelectedValue));
        //            return;
        //        }

        //    }

        //}

        //protected void uoRepeaterHotels_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    List<Hotels> list = new List<Hotels>();
        //    list = GetHotelList();
        //    if (e.CommandName == "Select")
        //    {
        //        uoHiddenFieldBranch.Value = e.CommandArgument.ToString();

        //        GetRoomBlocks(Int32.Parse(e.CommandArgument.ToString()), true);

        //        var hotel = (from a in list
        //                     where a.BranchId == GlobalCode.Field2Int(e.CommandArgument.ToString())
        //                     select new
        //                     {
        //                         HotelName = a.BranchName,
        //                         IsAccredited = a.isAccredited,
        //                         withContract = a.withContract,
        //                         VendorId = a.VendorId,
        //                         CityId = a.CityId,
        //                         CountryId = a.CountryId,
        //                         ContractId = a.ContractId,
        //                     }).ToList();

        //        ViewState["VendorId"] = hotel[0].VendorId;
        //        ViewState["CountryId"] = hotel[0].CountryId;
        //        ViewState["CityId"] = hotel[0].CityId;
        //        ViewState["Contract"] = hotel[0].ContractId;

        //    }
        //}

        //protected void uoBtnCheckRooms_Click(object sender, EventArgs e)
        //{
        //    GetRoomBlocks(GlobalCode.Field2Int(uoHiddenFieldBranch.Value), false);
        //}

        /// <summary>
        /// ===============================================
        /// Author: Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: export Exception Bookings to excel
        /// ===============================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtCancelled = null;
            DataSet ds = null;
            try
            {
                //List<NonTurnPortsList> list = new List<NonTurnPortsList>();
                //List<NonTurnPortsList> listCancelled = new List<NonTurnPortsList>();
                //list = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListConfirmed"];
                //listCancelled = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListCancelled"];

                //if (list.Count > 0 || listCancelled.Count > 0)
                //{
                //    //ExportException(list, listCancelled);
                //    dt = GetConfirmedDataTable(list);
                //    dtCancelled = GetCancelledDataTable(listCancelled);
                //    CreateFile(dt, dtCancelled);
                //}
                //else
                //{
                //    AlertMessage("There is no exception record to export.");
                //}
                int iRegionID = GlobalCode.Field2Int(Session["Region"]);
                int iPortID = GlobalCode.Field2Int(Session["Port"]);

                ds = NoHotelContractBLL.GetNonTurnPortsExport(GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                        uoHiddenFieldUser.Value, iRegionID, iPortID, "");
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    dtCancelled = ds.Tables[1];
                    CreateFile(dt, dtCancelled);
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        
        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Region"] = "0";
        //    Session["Port"] = uoDropDownListPort.SelectedValue;

        //    PageAsyncTask TaskPort = new PageAsyncTask(OnBegin, OnEndPort, null, "Async1", true);
        //    Page.RegisterAsyncTask(TaskPort);

        //    SetDefaults();

        //    PageAsyncTask TaskPort2 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async2", true);
        //    Page.RegisterAsyncTask(TaskPort2);

        //    PageAsyncTask TaskPort3 = new PageAsyncTask(OnBeginHotels, OnEndHotels, null, "Async3", true);
        //    Page.RegisterAsyncTask(TaskPort3);

        //    //GetExceptions();
        //    //GetHotels();
        //}


        //public void ExecuteAsyncTaskHotel() { }
        //public IAsyncResult OnBeginHotels(object sender, EventArgs e, AsyncCallback cb, object extraData)
        //{
        //    _dlgtHotel = new AsyncTaskDelegate(ExecuteAsyncTaskHotel);
        //    IAsyncResult result = _dlgtHotel.BeginInvoke(cb, extraData);
        //    return result;
        //}
        //public void OnEndHotels(IAsyncResult ar)
        //{
        //    _dlgtHotel.EndInvoke(ar);
        //    //GetHotels();
        //}

        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtException = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtException.BeginInvoke(cb, extraData);
            return result;
        }
        public void OnEndExceptions(IAsyncResult ar)
        {
            _dlgtException.EndInvoke(ar);
             GetExceptions();
        }

        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPort.SelectedValue;            

            SetDefaults();

            PageAsyncTask TaskPort2 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async2", true);
            Page.RegisterAsyncTask(TaskPort2);

            //PageAsyncTask TaskPort3 = new PageAsyncTask(OnBeginHotels, OnEndHotels, null, "Async3", true);
            //Page.RegisterAsyncTask(TaskPort3);

            //GetExceptions();
            //GetHotels();

        }
        

        /// <summary>
        /// Date Created: 17/Mar/2013
        /// Created By:   Josephine Gad
        /// (description) Save/Send Email and confirm manifest  
        /// </summary>
        //protected void uoButtonSend_Click(object sender, EventArgs e)
        private void SendEmailButton(bool bIsSave)
        {
            DataTable dt = null;
            DataTable dtCancelled = null;

            try
            {
                string EmailTo = uoHiddenFieldTo.Value;
                string EmailCc = uoHiddenFieldCc.Value;

                if (EmailTo != "")
                {
                    ConfirmManifest(bIsSave, EmailTo, EmailCc);
                    string sConfirmed = GlobalCode.Field2String(Session["NonTurnPortDateConfirmed"]);

                    List<NonTurnPortsList> list = new List<NonTurnPortsList>();
                    List<NonTurnPortsList> listCancelled = new List<NonTurnPortsList>();
                    list = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListConfirmed"];
                    listCancelled = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListCancelled"];

                    if (list.Count > 0 || listCancelled.Count > 0)
                    {
                        dt = GetConfirmedDataTable(list);
                        dtCancelled = GetCancelledDataTable(listCancelled);
                                              
                        CreateEmail(dt, dtCancelled, EmailTo, EmailCc);
                        GetExceptions();
                        AlertMessage("Email Sent!");
                    }
                    else
                    {
                        AlertMessage("There is no manifest to email.");
                    }
                }
                else
                {
                    AlertMessage("Please specify at least one recipient.");
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }

       
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Muhallidin G Wali
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
        /// Author:         Muhallidin G Wali
        /// Date Created:   04/04/2012
        /// Description:    
        /// ========================================
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="isCollapsed"></param>
        //protected void GetRoomBlocks(int BranchId, bool isCollapsed)
        //{
        //    List<RoomBlocks> roomList = new List<RoomBlocks>();
        //    foreach (RepeaterItem item in uoRepeaterHotels.Items)
        //    {
        //        LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");
        //        TextBox txtCheckIn = (TextBox)item.FindControl("uoTxtBoxCheckIn");
        //        TextBox txtDays = (TextBox)item.FindControl("uoTxtBoxDays");
        //        txtDays.Text = uoHiddenFieldNoOfNites.Value;

        //        if (txtCheckIn.Text == "")
        //        {
        //            DateTime dt = DateTime.Now;
        //            if (GlobalCode.Field2DateTime(Session["DateFrom"]) < GlobalCode.Field2DateTime(dt.ToShortDateString()))
        //            {
        //                txtCheckIn.Text = GlobalCode.Field2DateTime(dt.ToShortDateString()).ToShortDateString();
        //            }
        //            else
        //            {
        //                txtCheckIn.Text = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
        //            }
        //        }
        //        uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(txtCheckIn.Text).AddDays(GlobalCode.Field2Int(txtDays.Text)).ToShortDateString();
        //        CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");

        //        if (lnkBtn.CommandArgument.ToString() == BranchId.ToString())
        //        {
        //            if (!isCollapsed || cpe.Collapsed)
        //            {
        //                int numOfPeople = GetSeafarersToBook();
        //                Session["HotelBookingsRoomBlocks"] = null;
        //                roomList = GetRoomBlocksList(GlobalCode.Field2DateTime(txtCheckIn.Text), BranchId);
        //                ListView l1 = (ListView)item.FindControl("uoRoomList");
        //                l1.DataSource = roomList;//HotelBookings.RoomBlocks; //
        //                l1.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            cpe.Collapsed = true;
        //            cpe.ClientState = "true";
        //        }
        //    }
        //}

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
        }

        protected void SetDefaults()
        {
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldStartDate.Value = uoHiddenFieldDate.Value;
            int iRegionID = GlobalCode.Field2Int(Session["Region"]);
            int iPortID = GlobalCode.Field2Int(Session["Port"]);
            ExceptionBLL.LoadNonTurnPortsExceptionPage(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0, iRegionID, iPortID);
        }
        /// <summary>
        /// Modified By:     Josephine Gad
        /// Date Modified:   17/Mar/2013
        /// Description:     Add link for Sending Email To Service Provider
        ///                  Bind Confirmed and Cancelled Manifest
        /// </summary>
        protected void GetExceptions()
        {
            try
            {
                List<NonTurnPortsList> list = new List<NonTurnPortsList>();
                List<NonTurnPortsList> listConfirm = new List<NonTurnPortsList>();
                List<NonTurnPortsList> listCancel = new List<NonTurnPortsList>();

                //if (GlobalCode.Field2Int(Session["Region"]) != 0 || GlobalCode.Field2Int(Session["Port"]) != 0)
                //    list = GetHotelExceptionList();
                //uoListViewNonTurnPort.DataSource = list;//HotelTransactionException.ExceptionBooking;
                //uoListViewNonTurnPort.DataBind();

                list = GetHotelExceptionList();

                uoListViewNonTurnPort.DataSource = list;//HotelTransactionException.ExceptionBooking;
                uoListViewNonTurnPort.DataBind();


                listConfirm = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListConfirmed"];
                uoListViewConfirmed.DataSource = listConfirm;
                uoListViewConfirmed.DataBind();

                listCancel = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListCancelled"];
                uoListViewCancelled.DataSource = listCancel;
                uoListViewCancelled.DataBind();

                if (uoDropDownListPort.SelectedValue != "0" && uoDropDownListPort.SelectedValue != "" )
                {
                    List<PortAgentEmail> PortEmailList = new List<PortAgentEmail>();
                    PortEmailList = (List<PortAgentEmail>)Session["PortAgentEmail"];

                    uoHyperLinkSendEmails.Visible = true;
                    if (PortEmailList.Count > 0)
                    {
                        uoTextBoxTo.Text = PortEmailList[0].EmailTo;
                        uoTextBoxCc.Text = PortEmailList[0].EmailCc;
                        uoHiddenFieldTo.Value = PortEmailList[0].EmailTo;
                        uoHiddenFieldCc.Value = PortEmailList[0].EmailCc;
                    }
                    else
                    {
                        uoTextBoxTo.Text = "";
                        uoTextBoxCc.Text = "";
                        uoHiddenFieldTo.Value = "";
                        uoHiddenFieldCc.Value = "";
                    }
                }
                else
                {
                    uoHyperLinkSendEmails.Visible = false;
                    uoTextBoxTo.Text = "";
                    uoTextBoxCc.Text = "";
                    uoHiddenFieldTo.Value = "";
                    uoHiddenFieldCc.Value = "";
                }

                if (listConfirm.Count > 0 || listCancel.Count > 0)
                {
                    uoBtnExportList.Enabled = true;
                }
                else
                {
                    uoBtnExportList.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>

        /// </summary>
        //protected void GetHotels()
        //{
        //    try
        //    {
        //        List<Hotels> list = new List<Hotels>();


        //        if (GlobalCode.Field2Int(Session["Region"]) != 0 || GlobalCode.Field2Int(Session["Port"]) != 0)
        //            list = GetHotelList();

        //        uoRepeaterHotels.DataSource = list; //HotelTransactionException.Hotels;
        //        uoRepeaterHotels.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>

        /// </summary>
        /// <returns></returns>
        private List<Hotels> GetHotelList()
        {
            try
            {
                List<Hotels> list = new List<Hotels>();

                if (Session["HotelTransactionExceptionHotels"] != null)
                {
                    list = (List<Hotels>)Session["HotelTransactionExceptionHotels"];
                }
                else
                {
                    int iRegionID = GlobalCode.Field2Int(Session["Region"]);
                    int iPortID = GlobalCode.Field2Int(Session["Port"]);
                    ExceptionBLL.LoadNonTurnPortsExceptionPage(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0, iRegionID, iPortID);

                    list = (List<Hotels>)Session["HotelTransactionExceptionHotels"];
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>

        /// </summary>
        /// <returns></returns>
        private List<NonTurnPortsList> GetHotelExceptionList()
        {
            try
            {
                List<NonTurnPortsList> list = new List<NonTurnPortsList>();

                if (GlobalCode.Field2Int(Session["Port"]) != GlobalCode.Field2Int(uoDropDownListPort.SelectedValue) || 
                    Session["HotelNonTurnPortsListExceptionBooking"] == null)
                {
                    int iRegionID = 0;//GlobalCode.Field2Int(Session["Region"]);
                    int iPortID = GlobalCode.Field2Int(Session["Port"]);

                    ExceptionBLL.LoadNonTurnPortsExceptionPage(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0, iRegionID, iPortID);
                    list = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListExceptionBooking"];
                }
                else
                {
                    list = (List<NonTurnPortsList>)Session["HotelNonTurnPortsListExceptionBooking"];
                }

                
  
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   04/10/2012
        /// Description:    
        /// </summary>
        /// <param name="CheckinDt"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        //private List<RoomBlocks> GetRoomBlocksList(DateTime CheckinDt, int BranchId)
        //{
        //    try
        //    {
        //        List<RoomBlocks> list = new List<RoomBlocks>();

        //        if (Session["HotelBookingsRoomBlocks"] != null)
        //        {
        //            list = (List<RoomBlocks>)Session["HotelBookingsRoomBlocks"];
        //        }
        //        else
        //        {
        //            int numOfPeople = GetSeafarersToBook();
        //            list = bookingsBLL.getRemainingRooms(CheckinDt,
        //                GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value),
        //                numOfPeople, GlobalCode.Field2Int(uoHiddenFieldRoomType.Value), BranchId);

        //            Session["HotelBookingsRoomBlocks"] = list;
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 04/10/2012
        /// Description: 
        /// </summary>
        /// <returns></returns>
        public string Valid()
        {
            bool valid = Convert.ToBoolean(Eval("valid"));

            if (!valid)
            {
                //uoHiddenFieldValid.Value = "false";
                return "font-weight: bold; color: #FF0000; padding: 0px; border: thin solid #FF0000";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   04/10/2012
        /// Description:    
        /// ---------------------------------------
        /// </summary>
        /// <returns></returns>
        protected int GetSeafarersToBook()
        {
            int Count = 0;
            bool IsSingleRoom = false;
            bool IsDoubleRoom = false;

            foreach (ListViewItem item in uoListViewNonTurnPort.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                HiddenField hfRoomType = (HiddenField)item.FindControl("hfRoomType");
                bool IsApprovedBool = false;
                if (CheckSelect.Visible == true)
                {
                    IsApprovedBool = CheckSelect.Checked;
                    if (IsApprovedBool)
                    {
                        Count += 1;
                        if (hfRoomType.Value == "1")
                        {
                            IsSingleRoom = true;
                        }
                        if (hfRoomType.Value == "2")
                        {
                            IsDoubleRoom = true;
                        }
                    }
                }
                
            }
            if (IsSingleRoom && !IsDoubleRoom)
            {
                uoHiddenFieldRoomType.Value = "1";
            }
            else if (!IsSingleRoom && IsDoubleRoom)
            {
                uoHiddenFieldRoomType.Value = "2";
            }
            else if (IsSingleRoom && IsDoubleRoom)
            {
                uoHiddenFieldRoomType.Value = "3";
            }
            else
            {
                uoHiddenFieldRoomType.Value = "";
            }
            return Count;
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 04/10/2012
        /// </summary>
        /// <param name="Branch"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="source"></param>
        //protected void CheckRoomValidity(DateTime CheckinDt, string Branch, int BranchId, string RoomType, int source)
        //{
        //    List<RoomBlocks> list = new List<RoomBlocks>();
        //    list = GetRoomBlocksList(CheckinDt, BranchId);
        //    bool isValid = false;
        //    bool isBooked = false;
        //    int RoomId = 0;
        //    DateTime CheckoutDt = GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value);

        //    var room = (from a in list//HotelBookings.RoomBlocks
        //                where a.BranchId == BranchId && a.Room == RoomType
        //                select new
        //                {
        //                    validContract = a.validContract,
        //                    validOverride = a.validOverride,
        //                    validEmergency = a.validEmergency,
        //                    ContractId = a.ContractId,
        //                    RoomTypeId = a.RoomTypeId,
        //                    colDate = a.colDate
        //                }).ToList();

        //    if (source == 0)
        //    {
        //        var roomContract = (from a in room
        //                            where a.colDate >= CheckinDt && a.colDate <= CheckoutDt
        //                            && a.validContract == false
        //                            select new
        //                            {
        //                                validOverride = a.validOverride,
        //                                RoomTypeId = a.RoomTypeId
        //                            }).ToList();
        //        if (roomContract.Count == 0)
        //        {
        //            RoomId = room[0].RoomTypeId;
        //            ViewState["Contract"] = room[0].ContractId;
        //            isValid = true;
        //        }
        //    }
        //    if (source == 1)
        //    {
        //        var roomOverride = (from a in room
        //                            where a.colDate >= CheckinDt && a.colDate <= CheckoutDt
        //                            && a.validOverride == false
        //                            select new
        //                            {
        //                                validOverride = a.validOverride,
        //                                RoomTypeId = a.RoomTypeId
        //                            }).ToList();
        //        if (roomOverride.Count == 0)
        //        {
        //            RoomId = room[0].RoomTypeId;
        //            isValid = true;
        //        }
        //    }
        //    if (source == 2)
        //    {
        //        var roomEmergency = (from a in room
        //                             where a.colDate >= CheckinDt && a.colDate <= CheckoutDt
        //                             && a.validEmergency == false
        //                             select new
        //                             {
        //                                 validEmergency = a.validEmergency,
        //                                 RoomTypeId = a.RoomTypeId
        //                             }).ToList();
        //        if (roomEmergency.Count == 0)
        //        {
        //            RoomId = room[0].RoomTypeId;
        //            isValid = true;
        //        }
        //    }

           
        //    if (isValid)
        //    {
        //        BookSeafarer(BranchId, source, RoomId, out isBooked);
        //        string sMessage = "Hotel Bookings Approved";

        //        if (isBooked)
        //        {
        //            AlertMessage(sMessage);
        //        }
        //        else
        //        {
        //            AlertMessage("Seafarer cannot be booked on the same date.");
        //        }

        //        SetDefaults();
        //        GetExceptions();
        //        GetRoomBlocks(BranchId, false);
        //    }
        //    else
        //    {
        //        string sMessage;
        //        if (source == 1)
        //        {
        //            sMessage = ConfigurationSettings.AppSettings["MessageRoomBlocksOverride"].ToString();
        //        }
        //        else if (source == 1)
        //        {
        //            sMessage = ConfigurationSettings.AppSettings["MessageRoomBlocksEmergency"].ToString();
        //        }
        //        else
        //        {
        //            sMessage = ConfigurationSettings.AppSettings["MessageRoomBlocksContract"].ToString();
        //        }

        //        sMessage = sMessage.Replace("(COUNT)", uoHIddenFieldSFCount.Value);
        //        sMessage = sMessage.Replace("(HOTELNAME)", Branch);

        //        AlertMessage(sMessage);
        //    }

        //}

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
        /// Author:         Muhallidin G Wali
        /// Date Created: 04/10/2012
        /// Description: 
        /// --------------------------------------
        /// </summary>
        //private bool BookSeafarer(int BranchId, int Source, int RoomTypeId, out bool Valid)
        //{
        //    DateTime currDate = CommonFunctions.GetCurrentDateTime();
        //    string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
        //    //string sMessage = "";
        //    Valid = false;
           

        //    GetSeafarerDetails();

        //    foreach (RepeaterItem item in uoRepeaterHotels.Items)
        //    {
        //        LinkButton lnkBtn = (LinkButton)item.FindControl("uoLinkBranch");

        //        CollapsiblePanelExtender cpe = (CollapsiblePanelExtender)item.FindControl("cpe");

        //        if (lnkBtn.CommandArgument.ToString() == BranchId.ToString())
        //        {
        //            TextBox txtCheckIn = (TextBox)item.FindControl("uoTxtBoxCheckIn");
        //            TextBox txtDays = (TextBox)item.FindControl("uoTxtBoxDays");
        //            TextBox txtTime = (TextBox)item.FindControl("uoTxtBoxTime");
        //            TextBox txtConfirmation = (TextBox)item.FindControl("uoTxtBoxConfirmation");
        //            TextBox txtRemarks = (TextBox)item.FindControl("uoTxtBoxRemarks");
        //            DropDownList HotelStatus = (DropDownList)item.FindControl("uoDropDownListStatus");
        //            CheckBox chkShuttle = (CheckBox)item.FindControl("uoCheckBoxShuttle");
        //            if (txtCheckIn.Text == "")
        //            {
        //                txtCheckIn.Text = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
        //            }
        //            txtDays.Text = uoHiddenFieldNoOfNites.Value;
        //            uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(txtCheckIn.Text).AddDays(GlobalCode.Field2Int(txtDays.Text)).ToShortDateString();

        //            Valid = bookingsBLL.BookSeafarer(ViewState["idBigint"].ToString(), ViewState["seqNo"].ToString(),
        //                   ViewState["trId"].ToString(), ViewState["mReqId"].ToString(), ViewState["recLoc"].ToString(), Source,
        //                   GlobalCode.Field2Int(ViewState["Contract"].ToString()), GlobalCode.Field2Int(ViewState["VendorId"]),
        //                   BranchId, RoomTypeId, GlobalCode.Field2DateTime(txtCheckIn.Text),
        //                   GlobalCode.Field2DateTime(txtTime.Text), GlobalCode.Field2Int(txtDays.Text), txtConfirmation.Text, HotelStatus.SelectedValue,
        //                   uoHiddenFieldUser.Value, GlobalCode.Field2String(ViewState["SFStatus"]), GlobalCode.Field2Int(ViewState["CityId"].ToString()),
        //                   GlobalCode.Field2Int(ViewState["CountryId"]), txtRemarks.Text, GlobalCode.Field2String(ViewState["sfStripe"]),
        //                   "", "",
        //                   chkShuttle.Checked,
        //                   "Book Seafarers", "BookSeafarer", Path.GetFileName(Request.Path), strTimeZone,
        //                   CommonFunctions.GetDateTimeGMT(currDate), DateTime.Now);
        //            return Valid;
        //        }
        //    }
        //    return Valid;
        //}

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 04/10/2012
        /// Description: 
        /// --------------------------------------
        /// </summary>
        protected void GetSeafarerDetails()
        {
            string sfName = "";
            string sfId = "";
            string sfStripe = "";
            string SFStatus = "";
            string recLoc = "";
            string trId = "";
            string mReqId = "";

            string sIdBigint = "";
            string sSeqNo = "";
            int Count = 0;

            foreach (ListViewItem item in uoListViewNonTurnPort.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    Count += 1;
                    HyperLink SeafarerName = (HyperLink)item.FindControl("uoHyperLinkName");
                    Label SeafarerId = (Label)item.FindControl("uoLblSeafarerID");
                    Label Stripes = (Label)item.FindControl("uoLblStripe");
                    HiddenField Status = (HiddenField)item.FindControl("uoLblStatus");
                    Label RLoc = (Label)item.FindControl("uoLblRecLoc");
                    HiddenField tReq = (HiddenField)item.FindControl("hfReqId");

                    HiddenField idBigint = (HiddenField)item.FindControl("hfIdBigint");
                    HiddenField seqNo = (HiddenField)item.FindControl("hfSeqNo");

                    sfName = sfName + SeafarerName.Text + "|";
                    sfId = sfId + SeafarerId.Text + "|";
                    sfStripe = sfStripe + Stripes.Text + "|";
                    SFStatus += Status.Value + '|';
                    recLoc += RLoc.Text + "|";
                    trId += tReq.Value + "|";
                    mReqId += "0" + "|";

                    sIdBigint += idBigint.Value + "|";
                    sSeqNo += seqNo.Value + "|";
                }
            }


            ViewState["sfStripe"] = sfStripe.TrimEnd('|');
            ViewState["SFStatus"] = SFStatus.TrimEnd('|');
            ViewState["recLoc"] = recLoc.TrimEnd('|');
            ViewState["trId"] = trId.TrimEnd('|');
            ViewState["mReqId"] = mReqId.TrimEnd('|');

            ViewState["idBigint"] = sIdBigint.TrimEnd('|');
            ViewState["seqNo"] = sSeqNo.TrimEnd('|');
        }


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/10/2012
        /// Description:  set alternate color
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
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   11/04/2012
        /// Description: 
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/Mar/2013
        /// Description:    Change void to DataTable
        ///                 Remove CreateFile
        /// ---------------------------------------------
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable GetConfirmedDataTable(List<NonTurnPortsList> list)
        {
            DataTable dt = null;
          
            try
            {
                var e = (from a in list
                         select new
                         {
                             AirSequence = a.AirSequence,
                             HotelCity = a.HotelCity,
                             ServiceRequested = a.ServiceRequested,
                             ServiceRequestDate = a.ServiceRequestedDate,
                             Remarks = a.Remarks,
                             Checkin = a.Checkin,
                             CheckOut = a.CheckOut,
                             HotelNite = a.HotelNite,
                             LastName = a.LastName,
                             FirstName = a.FirstName,
                             Employee = a.Employee,
                             Gender = a.Gender,
                             SingleDouble = a.SingleDouble,
                             Couple = a.Couple,
                             Title = a.Title,
                             Ship = a.Ship,
                             Costcenter = a.Costcenter,
                             Nationality = a.Nationality,
                             HotelRequest = a.HotelRequest,
                             RecLoc = a.RecLoc,
                             DeptCity = a.deptCity,
                             ArvlCity = a.ArvlCity,
                             Arvldate = a.Arvldate,
                             ArvlTime = a.ArvlTime,
                             Carrier = a.Carrier,
                             FlightNo = a.FlightNo,
                             Voucher = a.Voucher,
                             PassportNo = a.PassportNo,
                             PassportExp = a.PassportExp,
                             PassportIssued = a.PassportIssued,
                             Birthday = a.Birthday,
                             HotelBranch = a.HotelBranch,
                             BookingRemarks = a.Bookingremark,

                             ConfirmedBy = a.ConfirmedBy,
                             ConfirmedDate = a.ConfirmedDate,

                             GroupNo = a.GroupNo,

                         }).ToList();             
                dt = getDataTable(e);
                return dt;
               // CreateFile(dt, dtCancelled);
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
        /// Date Modified:  17/Mar/2013
        /// Description:   Convert cancelled list to DataTable
        /// ---------------------------------------------
        /// </summary>
        /// <param name="listCancelled"></param>
        /// <returns></returns>
        private DataTable GetCancelledDataTable(List<NonTurnPortsList> listCancelled)
        {
            DataTable dtCancelled = null;
            try
            {
                var cancel = (from a in listCancelled
                              select new
                              {
                                  AirSequence = a.AirSequence,

                                  HotelCity = a.HotelCity,
                                  Checkin = a.Checkin,
                                  CheckOut = a.CheckOut,
                                  HotelNite = a.HotelNite,
                                  LastName = a.LastName,
                                  FirstName = a.FirstName,
                                  Employee = a.Employee,
                                  Gender = a.Gender,
                                  SingleDouble = a.SingleDouble,
                                  Couple = a.Couple,
                                  Title = a.Title,
                                  Ship = a.Ship,
                                  Costcenter = a.Costcenter,
                                  Nationality = a.Nationality,
                                  HotelRequest = a.HotelRequest,
                                  RecLoc = a.RecLoc,
                                  RecLocID = a.RecLocID,
                                  DeptCity = a.deptCity,
                                  ArvlCity = a.ArvlCity,
                                  Arvldate = a.Arvldate,
                                  ArvlTime = a.ArvlTime,
                                  Carrier = a.Carrier,
                                  FlightNo = a.FlightNo,
                                  Voucher = a.Voucher,
                                  PassportNo = a.PassportNo,
                                  PassportExp = a.PassportExp,
                                  PassportIssued = a.PassportIssued,
                                  Birthday = a.Birthday,
                                  HotelBranch = a.HotelBranch,
                                  //Booking = a.Booking,
                                  BookingRemarks = a.Bookingremark,

                                  ConfirmedBy = a.ConfirmedBy,
                                  ConfirmedDate = a.ConfirmedDate,

                                  GroupNo = a.GroupNo,

                              }).ToList();
                dtCancelled = getDataTable(cancel);

                return dtCancelled;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   11/04/2012
        /// Description: 
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/Mar/2013
        /// Description:    Add Cancelled List in Excel
        /// ---------------------------------------------
        /// </summary>
        //private void ExportException(List<NonTurnPortsList> list, List<NonTurnPortsList> listCancelled)
        //{
        //    DataTable dt = null;
        //    DataTable dtCancelled = null;

        //    try
        //    {
        //        var e = (from a in list
        //                 select new
        //                 {
        //                     HotelCity = a.HotelCity,
        //                     Checkin = a.Checkin,
        //                     CheckOut = a.CheckOut,
        //                     HotelNite = a.HotelNite,
        //                     LastName = a.LastName,
        //                     FirstName = a.FirstName,
        //                     Employee = a.Employee,
        //                     Gender = a.Gender,
        //                     SingleDouble = a.SingleDouble,
        //                     Couple = a.Couple,
        //                     Title = a.Title,
        //                     Ship = a.Ship,
        //                     Costcenter = a.Costcenter,
        //                     Natioality = a.Natioality,
        //                     HotelRequest = a.HotelRequest,
        //                     RecLoc = a.RecLoc,
        //                     //RecLocID = a.RecLocID,
        //                     AirSequence = a.AirSequence,
        //                     deptCity = a.deptCity,
        //                     ArvlCity = a.ArvlCity,
        //                     Arvldate = a.Arvldate,
        //                     ArvlTime = a.ArvlTime,
        //                     Carrier = a.Carrier,
        //                     FlightNo = a.FlightNo,
        //                     Voucher = a.Voucher,
        //                     PassportNo = a.PassportNo,
        //                     PassportExp = a.PassportExp,
        //                     PassportIssued = a.PassportIssued,
        //                     HotelBranch = a.HotelBranch,
        //                     //Booking = a.Booking,
        //                     Bookingremark = a.Bookingremark,

        //                     ConfirmedBy = a.ConfirmedBy,
        //                     ConfirmedDate = a.ConfirmedDate
        //                 }).ToList();


        //        var cancel = (from a in listCancelled
        //                 select new
        //                 {
        //                     HotelCity = a.HotelCity,
        //                     Checkin = a.Checkin,
        //                     CheckOut = a.CheckOut,
        //                     HotelNite = a.HotelNite,
        //                     LastName = a.LastName,
        //                     FirstName = a.FirstName,
        //                     Employee = a.Employee,
        //                     Gender = a.Gender,
        //                     SingleDouble = a.SingleDouble,
        //                     Couple = a.Couple,
        //                     Title = a.Title,
        //                     Ship = a.Ship,
        //                     Costcenter = a.Costcenter,
        //                     Natioality = a.Natioality,
        //                     HotelRequest = a.HotelRequest,
        //                     RecLoc = a.RecLoc,
        //                     RecLocID = a.RecLocID,
        //                     AirSequence = a.AirSequence,
        //                     deptCity = a.deptCity,
        //                     ArvlCity = a.ArvlCity,
        //                     Arvldate = a.Arvldate,
        //                     ArvlTime = a.ArvlTime,
        //                     Carrier = a.Carrier,
        //                     FlightNo = a.FlightNo,
        //                     Voucher = a.Voucher,
        //                     PassportNo = a.PassportNo,
        //                     PassportExp = a.PassportExp,
        //                     PassportIssued = a.PassportIssued,
        //                     HotelBranch = a.HotelBranch,
        //                     //Booking = a.Booking,
        //                     Bookingremark = a.Bookingremark,

        //                     ConfirmedBy = a.ConfirmedBy,
        //                     ConfirmedDate = a.ConfirmedDate
        //                 }).ToList();

        //        dt = getDataTable(e);
        //        dtCancelled = getDataTable(cancel);

        //        CreateFile(dt, dtCancelled);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //        if (dtCancelled != null)
        //        {
        //            dtCancelled.Dispose();
        //        }
        //    }
        //}

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

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   11/04/2012
        /// Description:    create the file to be exported
        /// ------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/Mar/2013
        /// Description:    Add Cancelled List in Excel
        /// ---------------------------------------------
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt, DataTable dtCancelled)
        {
            try
            {
                string sPort = uoDropDownListPort.SelectedItem.Text;
                string[] sPortArr = sPort.Split("-".ToCharArray());
                string[] sPortArrText = sPortArr[1].Split(":".ToCharArray());



                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/ExceptionList/");
                string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                //string FileName = "NTP_" + sPortArr[0].ToString().TrimEnd() + "-" + sPortArrText[0].ToString().TrimEnd() + "_" + sDateManifest + "_" + sDate + ".xls";            
                string FileName = "NonTurnPortManifest_" + sPortArr[0].ToString().TrimEnd() + "_" + sDateManifest + "_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                //ExportToExcel(dt, strFileName, dtCancelled);
                //string sPort = uoDropDownListPort.SelectedItem.Text;
                //string[] sPortArr = sPort.Split("-".ToCharArray());

                CreateExcel(dt, strFileName, dtCancelled, sPortArr[0].ToString());
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   17/Mar/2013
        /// Description:    Create the excel file and send email
        /// ------------------------------------------------
        /// </summary>
        private void CreateEmail(DataTable dt, DataTable dtCancelled, string sEmailTo, string sEmailCc)
        {
            string sPort = uoDropDownListPort.SelectedItem.Text;
            string[] sPortArr = sPort.Split("-".ToCharArray());

            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/ExceptionList/");
            string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");
            string sDateOnly = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM dd, yyyy");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string FileName = "NTP_" + sPortArr[0].ToString().TrimEnd() + "-" + sPortArr[1].ToString().TrimEnd() + "_" + sDateManifest + "_" + sDate + ".xls";
            strFileName = FilePath + FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }

            //ExportToExcel(dt, strFileName, dtCancelled);
            //CreateExcel(dt, strFileName, dtCancelled, sPortArr[0].ToString());
            CreateExcel(dt, strFileName, dtCancelled, sPort.ToString());
          
            //string sSubject = "Travelmart: " + sPortArr[0].ToString() + " Manifest";
            string sSubject = "Travelmart: " + sPort.ToString() + " Manifest";
            string sMsg = "Please find attached " + sPort.ToString() + " manifest for " +
                     sDateOnly + ".<br/><br/>Please send us confirmation and any questions to  HRPortLogistics@rccl.com.<br/><br/>Thank you.";
            EmailManifest(sSubject, sMsg, strFileName, "",
             sEmailTo, sEmailCc, (strFileName + ";").TrimEnd(';'));
        }  
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/Mar/2013
        /// Description:    Create the excel file 
        ///                 Add dtCancelled, sPort
        /// ------------------------------------------------
        /// Author:         Marco Abejar Gad
        /// Modifed Date:   26/April/2013
        /// Description:    Included full port name in filenames at tabs
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName, DataTable dtCancelled, string sPort)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = (dtSource.Columns.Count) - 2;
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


                    // </Styles>
                    xtwWriter.WriteEndElement();

                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    //xtwWriter.WriteAttributeString("ss", "Name", null, sPort.TrimEnd() + " NonTurn Port Manifest");
                    xtwWriter.WriteAttributeString("ss", "Name", null, sPort.TrimEnd() + "_Manifest");

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
                        if (i <= iColCount && i > 2)
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

                    //Int16 iGroup = 0;
                    // Run through all rows of data source
                    foreach (DataRow row in dtSource.Rows)
                    {
                       // iGroup = GlobalCode.Field2TinyInt(row["GroupNo"]);

                        // <Row>
                        xtwWriter.WriteStartElement("Row");
                        i = 1;

                        //sEmployeeName = row["Name"].ToString();

                        // Run through all cell of current rows

                        foreach (object cellValue in row.ItemArray)
                        {
                            //if (iGroup == 1)
                            //{
                            //    if (i <= iColCount)
                            //    {

                            //        // <Cell>
                            //        xtwWriter.WriteStartElement("Cell");
                            //        //Border
                            //        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s63");
                            //        // <Data ss:Type="String">xxx</Data>
                            //        xtwWriter.WriteStartElement("Data");

                            //        if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEE" ||
                            //           dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNITE" ||
                            //           dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                            //           dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER" ||
                            //           dtSource.Columns[i - 1].Caption.ToUpper().Contains("AIRSEQUENCE") == true)
                            //        {
                            //            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                            //        }
                            //        else
                            //        {
                            //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            //        }

                            //        // Write content of cell
                            //        xtwWriter.WriteValue(cellValue);

                            //        // </Data>
                            //        xtwWriter.WriteEndElement();

                            //        // </Cell>
                            //        xtwWriter.WriteEndElement();
                            //    }
                            //}
                            //else
                            {
                                if (i <= iColCount && i > 2)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtSource.Columns[i - 1].Caption.ToUpper().Contains("AIRSEQUENCE") == true ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEE" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNITE" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" 
                                        )
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }

                                    //if (dtSource.Columns[i - 1].Caption.ToUpper().Contains("AIRSEQUENCE") == true ||
                                    //    dtSource.Columns[i - 1].Caption.ToUpper() == "DEPTCITY" ||
                                    //    dtSource.Columns[i - 1].Caption.ToUpper() == "ARVLCITY" ||
                                    //    dtSource.Columns[i - 1].Caption.ToUpper() == "ARVLDATE" ||
                                    //    dtSource.Columns[i - 1].Caption.ToUpper() == "ARVLTIME" ||
                                    //    dtSource.Columns[i - 1].Caption.ToUpper() == "CARRIER" ||
                                    //    dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHTNO")
                                    //{
                                    //    // Write content of cell
                                    //    xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                                    //}
                                    //else
                                    //{
                                        xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                                    //}
                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
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

                    if (dtCancelled.Rows.Count > 0)
                    {
                        //=======================================CANCELLED SHEET===============================================
                        #region COMPARED SHEET

                        iColCount = (dtCancelled.Columns.Count) - 1;
                        iRow = dtCancelled.Rows.Count + 15;

                        // <Worksheet ss:Name="xxx">
                        xtwWriter.WriteStartElement("Worksheet");
                        xtwWriter.WriteAttributeString("ss", "Name", null, "Cancelled Manifest");

                        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                        xtwWriter.WriteStartElement("Table");

                        //iRow = dtCancelled.Rows.Count + 15;

                        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                        //Header
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                        i = 1;
                        foreach (DataColumn Header in dtCancelled.Columns)
                        {
                            if (i <= iColCount && i > 2)
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
                        foreach (DataRow row in dtCancelled.Rows)
                        {
                            //iGroup = GlobalCode.Field2TinyInt(row["GroupNo"]);

                            // <Row>
                            xtwWriter.WriteStartElement("Row");
                            i = 1;

                            //sEmployeeName = row["Name"].ToString();

                            // Run through all cell of current rows
                            foreach (object cellValue in row.ItemArray)
                            {
                                //if (iGroup == 1)
                                //{
                                //    if (i <= iColCount)
                                //    {
                                //        // <Cell>
                                //        xtwWriter.WriteStartElement("Cell");
                                //        //Border
                                //        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s63");

                                //        // <Data ss:Type="String">xxx</Data>
                                //        xtwWriter.WriteStartElement("Data");

                                //        if (dtCancelled.Columns[i - 1].Caption.ToUpper() == "EMPLOYEE" ||
                                //            dtCancelled.Columns[i - 1].Caption.ToUpper() == "HOTELNITE" ||
                                //            dtCancelled.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                //            dtCancelled.Columns[i - 1].Caption.ToUpper() == "VOUCHER" ||
                                //            dtCancelled.Columns[i - 1].Caption.ToUpper() == "AIRSEQUENCE")
                                //        {
                                //            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                //        }
                                //        else
                                //        {
                                //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                //        }

                                //        // Write content of cell
                                //        xtwWriter.WriteValue(cellValue);

                                //        // </Data>
                                //        xtwWriter.WriteEndElement();

                                //        // </Cell>
                                //        xtwWriter.WriteEndElement();
                                //    }
                                //}
                                //else
                                {
                                    if (i <= iColCount && i > 2)
                                    {
                                        // <Cell>
                                        xtwWriter.WriteStartElement("Cell");

                                        // <Data ss:Type="String">xxx</Data>
                                        xtwWriter.WriteStartElement("Data");

                                        if (dtCancelled.Columns[i - 1].Caption.ToUpper() == "AIRSEQUENCE" ||
                                            dtCancelled.Columns[i - 1].Caption.ToUpper() == "EMPLOYEE" ||
                                            dtCancelled.Columns[i - 1].Caption.ToUpper() == "HOTELNITE" ||
                                            dtCancelled.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" 
                                            )
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                        }

                                       // if (dtSource.Columns[i - 1].Caption.ToUpper() == "AIRSEQUENCE" ||
                                       //dtSource.Columns[i - 1].Caption.ToUpper() == "DEPTCITY" ||
                                       //dtSource.Columns[i - 1].Caption.ToUpper() == "ARVLCITY" ||
                                       //dtSource.Columns[i - 1].Caption.ToUpper() == "ARVLDATE" ||
                                       //dtSource.Columns[i - 1].Caption.ToUpper() == "ARVLTIME" ||
                                       //dtSource.Columns[i - 1].Caption.ToUpper() == "CARRIER" ||
                                       //dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHTNO")
                                       // {
                                       //     // Write content of cell
                                       //     xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                                       // }
                                       // else
                                       // {
                                            xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                                        //}

                                        // </Data>
                                        xtwWriter.WriteEndElement();

                                        // </Cell>
                                        xtwWriter.WriteEndElement();
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

                        #endregion
                        //=======================================CANCELLED SHEET===============================================
                    }
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
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

            //Response.Redirect("~/Extract/ExceptionList/" + strFileName, false);

            string strScript = "CloseModal('../Extract/ExceptionList/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   06/07/2012
        /// description   Load Port List 
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                PortBLL.GetPortForNonTurn("", "", "0", GlobalCode.Field2DateTime(uoHiddenFieldDate.Value));

                int i = GlobalCode.Field2Int(Session["NonTurnPortCount"]);
                list = (List<PortList>)Session["PortForNonTurn"];

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
                AlertMessage(ex.Message);
            }         
        }
        string lastDateFieldValue2 = null;
        string lastClassColor = "alternateBg";
        protected string DashboardChangeRowColor()
        {
            string RowTextString = Eval("IDBigInt").ToString();
            string currentDataFieldValue = RowTextString;
            //See if there's been a change in value
            if (lastDateFieldValue2 != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDateFieldValue2 = currentDataFieldValue;
                if (lastClassColor == "")
                {
                    lastClassColor = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
                else
                {
                    lastClassColor = "";
                    return "<tr>";
                }
            }
            else
            {
                if (lastClassColor == "")
                {
                    lastClassColor = "";
                    return "<tr>";
                }
                else
                {
                    lastClassColor = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
            }
        }
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel"; //"Check In";
            string GroupValueString = "HotelBranch"; //"colTimeSpanStartDate";

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
                    return string.Format("<tr><td class=\"group\" colspan=\"33\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
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
        /// Created By:     Josephine Gad
        /// Date Created:   16/Mar/2013
        /// description     Confirm Manifest
        /// </summary>
        private void ConfirmManifest(bool bIsSave, string sEmailTo, string sEmailCc)
        {
           string strLogDescription ;
           if (bIsSave)
           {
               strLogDescription = "Save Service Provider Email,Confirm Non Turn Port Manifest";
           }
           else
           {
               strLogDescription = "Confirm Non Turn Port Manifest";
           
           }
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            NoHotelContractBLL.ConfirmNonTurnPortList(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value),
                GlobalCode.Field2Int(uoDropDownListPort.SelectedValue), 
                bIsSave, sEmailTo, sEmailCc,
                strLogDescription, "ConfirmManifest",
                Path.GetFileName(Request.UrlReferrer.AbsolutePath), CommonFunctions.GetDateTimeGMT(dateNow), dateNow);

            BindPortList();
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   17/Mar/2013
        /// description     Email Manifest
        /// </summary>
        private void EmailManifest(string sSubject, string sMessage, string attachment1, string attachment2,
            string EmailVendor, string EmailCc, string file)
        {
            string sBody;
            try
            {
                string sPort = uoDropDownListPort.SelectedItem.Text;
                string[] sPortArr = sPort.Split("-".ToCharArray());

                sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                sBody += "Dear Service Provider,  <br/><br/> " + sMessage;
                sBody += "</TR></TD></TABLE>";

                if (EmailVendor != "")
                {
                    string attachment = attachment1 + ";" + attachment2;
                    CommonFunctions.SendEmailWithAttachment("", EmailVendor, EmailCc, sSubject, sBody, attachment.TrimEnd(';'));
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   16/Jan/2014
        /// description     Bind the Non Port List newly added records
        /// </summary>
        //private void BindGridViewNew()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        if (Session["HotelNonTurnPortsListTableNew"] != null)
        //        {
        //            dt = (DataTable)Session["HotelNonTurnPortsListTableNew"];
        //            uoGridViewNew.DataSource = dt;
        //            uoGridViewNew.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}
      
        #endregion
    }
}


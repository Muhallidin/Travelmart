using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
//using System.Collections;


namespace TRAVELMART
{
    public partial class CrewAssist : System.Web.UI.Page
    {
        #region Events

        //wali test
        private AsyncTaskDelegate _dlgtSeafarer;

        // Create delegate. 
        protected delegate void AsyncTaskDelegate();
        CrewAssistBLL SF = new CrewAssistBLL();


        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 08/06/2013
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
        /// Author: Muhallidin G Wali
        /// Date Created: 08/06/2013
        /// Description: pop up alert message
        /// </summary>
        /// <param name="s"></param>

        private void ExceptionMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterStartupScript(Page, GetType(), "key", sScript, true);


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 08/06/2013
        /// Description: pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void ConfirmMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += " if (confirm('" + s + "') == true) ";
            sScript += "  document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldIsFinish').value = '1'; ";
            sScript += "    else  document.getElementById('ctl00_NaviPlaceHolder_uoHiddenFieldIsFinish').value = '0'; ";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                GlobalCode gc = new GlobalCode();
                string userID = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldUser.Value = userID;


                uoLabelUserID.Text = uoHiddenFieldUser.Value;

                //gc.SetAppSetting(uoHiddenFieldUser.Value);
                BindListViewTravel();

                uoHiddenFieldSaveHotel.Value = "0";
                uoHiddenFieldSaveTrans.Value = "0";
                uoHiddenFieldSaveMAG.Value = "0";
                uoHiddenFieldSavePA.Value = "0";
                uoHiddenFieldSaveSG.Value = "0";
            }

            if (Session["CurrentDate"] != null && Session["CurrentDate"].ToString() == "")
            {
                Session["CurrentDate"] = DateTime.Now.Date;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["hrID"] != null) uoHiddenFieldHotelRequestID.Value = Request.QueryString["hrID"].ToString();
                if (Request.QueryString["trID"] != null) uoHiddenFieldTravelRequestID.Value = Request.QueryString["trID"].ToString();
                if (Request.QueryString["SfID"] != null) uoTextBoxEmployeeID.Text = Request.QueryString["SfID"].ToString();
                if (Request.QueryString["as"] != null) uoHiddenFieldSeqNo.Value = Request.QueryString["as"].ToString();
                if (Request.QueryString["trp"] != null) uoHiddenFieldTransVendorID.Value = Request.QueryString["trp"].ToString();
                if (Request.QueryString["magp"] != null) uoHiddenFieldMeetAndGreetID.Value = Request.QueryString["magp"].ToString();

                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);


                if (uoHiddenFieldHotelRequestID.Value == "0" || uoTextBoxEmployeeID.Text.ToString() == "") ClearHotelObject(1);
            }
            else
            {

                if (uoListviewAir.Items.Count <= 0)
                {
                    uoListviewAir.DataSource = null;
                    uoListviewAir.DataBind();
                }

                if (uoListviewTravel.Items.Count <= 0)
                {
                    uoListviewTravel.DataSource = null;
                    uoListviewTravel.DataBind();
                }
                if (uoListViewHotelBook.Items.Count <= 0)
                {
                    uoListViewHotelBook.DataSource = null;
                    uoListViewHotelBook.DataBind();
                }

                if (uoListViewTransportation.Items.Count <= 0)
                {
                    uoListViewTransportation.DataSource = null;
                    uoListViewTransportation.DataBind();
                }

                if (uoListViewCompanionList.Items.Count <= 0)
                {
                    uoListViewCompanionList.DataSource = null;
                    uoListViewCompanionList.DataBind();
                }

            }



            centerdiv.Visible = false;
            uoLabelContractRate.Text = "Was the contracted rate of $0.0 Confimed";



            if (uoDropDownListHotel.Items.Count > 0)
            {
                if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
                {
                    rowPortAgent.Visible = true;
                }
                else
                {
                    rowPortAgent.Visible = false;
                }
            }
            else
            {
                rowPortAgent.Visible = false;
            }


            if (HiddenFieldHideCenter.Value == "1")
            {

                AddApproveHotel();
                refreshPage();
                HiddenFieldHideCenter.Value = "0";
            }

            if (uoHiddenFieldCancelHotel.Value == "1")
            {
                uoHiddenFieldCancelHotel.Value = "0";



                LoadHotelCancellation();

            }


            uoButtonAddHotel.Visible = uoCheckBoxMedical.Checked;
            uoButtonAddTranspo.Visible = uoCheckBoxMedical.Checked;

            CheckBoxCopycrewassist.ToolTip = uoHiddenFieldCrewAssist.Value.ToString();
            CheckBoxCopycrewhotels.ToolTip = uoHiddenFieldEmailHotel.Value.ToString();
            CheckBoxCopyShip.ToolTip = uoHiddenFieldCopyShip.Value.ToString();
            CheckBoxScheduler.ToolTip = uoHiddenFieldScheduler.Value.ToString();

            uoTableCompanion.Visible = uoCheckBoxAddCompanion.Checked;


        }

        protected void uoButtonAddHotel_Click(object sender, EventArgs e)
        {
            try
            {
                uoHiddenFieldHotelRequestID.Value = "0";
                uoHiddenFieldTransHotelOtherID.Value = "0";
                uoHiddenFieldSaveHotel.Value = "1";

                uoListViewCompanionList.DataSource = null;
                uoListViewCompanionList.DataBind();

                EnableHotelControl(true);
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }


        protected void uoButtonAddTranspo_Click(object sender, EventArgs e)
        {
            try
            {
                //uoHiddenFieldHotelRequestID.Value = "0";
                uoHiddenFieldSaveTrans.Value = "1";
                EnableTransporationControl(true);

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }


        protected void btnClearControl_Click(object sender, EventArgs e)
        {
            ClearHotelObject(1);
            ClearMeetAndGreet();
            ClearObjectPortAgent(false);
            ClearObjectSafeguard();
            ClearObjectTrans();
            ClearObjectVisa();

            uoDropDownListHotel.SelectedIndex = -1;
            EnableHotelControl(true);
            EnableTransporationControl(true);
            EnablePortAgentControl(true);

            uoListViewCompanionList.DataSource = null;
            uoListViewCompanionList.DataBind();

            EnableMeetAndGreetControl(true);

            uoDropDownListPort.Enabled = true;
            uoDropDownListExpenseType.Enabled = true;
            uoCheckBoxShow.Enabled = true;
            uoCheckBoxMedical.Enabled = true;
            uoButtonAddHotel.Enabled = true;
            uoListViewHotelBook.Enabled = true;





        }




        #endregion


        #region Functions


        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }
        public void OnEndExceptions(IAsyncResult ar)
        {
            _dlgtSeafarer.EndInvoke(ar);


            List<CrewAssisVehicleCost> VehicleCost = new List<CrewAssisVehicleCost>();
            uoListViewTranspoCost.DataSource = VehicleCost;
            uoListViewTranspoCost.DataBind();

            List<CrewAssisRemark> Lst = new List<CrewAssisRemark>();
            uoListViewRemarkPopup.DataSource = Lst;
            uoListViewRemarkPopup.DataBind();



            List<VehicleTransactionPortAgent> list1 = new List<VehicleTransactionPortAgent>();

            uoListViewTranportationRoute.DataSource = list1;
            uoListViewTranportationRoute.DataBind();

            GlobalCode gc = new GlobalCode();
            DataTable dt = new DataTable();
            dt = gc.getDataTable(list1);
            if (list1.Count > 0)
            {
                uoListViewTranportationRoute.DataSource = list1;
                uoListViewTranportationRoute.DataBind();

            }
            else
            {
                uoListViewTranportationRoute.DataSource = dt;
                uoListViewTranportationRoute.DataBind();
            }

            LoadAllCombo();
            //GetRequestSource();



            uoListViewRemark.DataSource = Lst;
            uoListViewRemark.DataBind();


            if (GlobalCode.Field2Long(uoTextBoxEmployeeID.Text.ToString()) > 0)
            {
                LoadHotelRequestPost();
                //LoadHotelRequestDetail();
                //LoadTransprtationRequestDetail();


                List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
                GetCrewTransaction(ref cres, 1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text),
                                  GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                                  GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                                  GlobalCode.Field2Long(uoHiddenFieldSeqNo.Value),
                                  GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value),
                                  GlobalCode.Field2String(uoHiddenFieldPortCode.Value),
                                  GlobalCode.Field2String(uoHiddenFieldArrCode.Value),
                                  false, uoHiddenFieldUser.Value.ToString());
                if (cres.Count > 0)
                {
                    LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking);
                    LoadMeetAndGreetDetail(cres[0].CrewAssistMeetAndGreet);
                    LoadPortAgentDetail(cres[0].CrewAssistPortAgentRequest);
                    LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                    LoadSafeguardRequestDetail(cres[0].CrewAssistSafeguardRequest);


                    if (GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0)
                    {
                        uoCheckBoxPAMAG.Checked = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : false;
                        uoCheckBoxPATrans.Checked = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : false;
                        uoCheckBoxPAHotel.Checked = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : false;
                        uoCheckBoxPASafeguard.Checked = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : false;

                        uoCheckBoxPAMAG.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : true;
                        uoCheckBoxPATrans.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : true;
                        uoCheckBoxPAHotel.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true;
                        uoCheckBoxPASafeguard.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : true;
                    }

                    uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                    uoListViewTransportation.DataBind();
                }
                else
                {
                    ClearHotelObject(1);
                    ClearMeetAndGreet();
                    ClearObjectPortAgent(false);
                    ClearObjectTrans();
                    ClearObjectSafeguard();

                    uoListViewTransportation.DataSource = null;
                    uoListViewTransportation.DataBind();

                }

            }
            else
            {
                ClearHotelObject(1);
                ClearMeetAndGreet();
                ClearObjectPortAgent(false);
                ClearObjectSafeguard();
                ClearObjectTrans();
                ClearObjectVisa();
            }
        }


        void LoadHotelRequestPost()
        {
            LoadSeafarer(1, uoTextBoxEmployeeID.Text.ToString());


            if (Session["SeafarerDetailList"] != null)
            {
                var DetailList = (List<SeafarerDetailHeader>)Session["SeafarerDetailList"];

                if (DetailList != null || DetailList.Count > 0)
                {

                    var MyList = DetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                    if (MyList.Count > 0)
                    {

                        uoHiddenFieldPortCode.Value = MyList[0].PortCode;
                        uoDropDownListPort.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPort, MyList[0].PortID);

                        List<CrewAssistHotelList> list = new List<CrewAssistHotelList>();
                        SF = new CrewAssistBLL();
                        list = SF.GetGetHotelPortExpendTypeList(1, uoHiddenFieldUser.Value, "0", uoDropDownListPort.SelectedValue.ToString());
                        uoDropDownListHotel.Items.Clear();
                        if (list.Count > 0)
                        {
                            uoDropDownListHotel.DataSource = list;
                            uoDropDownListHotel.DataTextField = "HotelName";
                            uoDropDownListHotel.DataValueField = "HotelID";
                            uoDropDownListHotel.DataBind();
                            uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                        }

                        uoListviewAir.DataSource = MyList[0].CrewAssistAirTransaction;
                        uoListviewAir.DataBind();
                    }

                    var res = MyList.Count == 0 ? null : MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();

                    if (res != null)
                    {
                        uoListViewHotelBook.DataSource = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                        uoListViewHotelBook.DataBind();
                    }

                }

            }

            CheckGridCheckBox();
            GetComboVendor(uoHiddenFieldPortCode.Value.ToString(), true);
            //if (Session["CrewAssistSafeguardVendor"] != null)
            //{
            //    uoDropDownListSafeguard.DataSource = null;
            //    uoDropDownListSafeguard.Items.Clear();
            //}
        }



        void LoadHotelCancellation()
        {

            //GetComboVendor(portcode, false);

            uoListViewTransportation.DataSource = null;
            uoListViewTransportation.DataBind();

            uoListViewHotelBook.DataSource = null;
            uoListViewHotelBook.DataBind();


            var DetailList = (List<SeafarerDetailHeader>)Session["SeafarerDetailList"];
            if (DetailList == null) return;

            if (DetailList.Count > 0)
            {
                var MyList = DetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                if (MyList.Count > 0)
                {

                    uoListviewAir.DataSource = MyList[0].CrewAssistAirTransaction;
                    uoListviewAir.DataBind();

                    if (MyList[0].CrewAssistAirTransaction.Count > 0)
                    {
                        foreach (ListViewDataItem item in uoListviewAir.Items)
                        {
                            DropDownList GetDDLList = (DropDownList)item.FindControl("uoDropDownListStatus");
                            HiddenField lblStatus = (HiddenField)item.FindControl("uoHiddenFieldStatus");
                            var ArList = MyList[0].CrewAssistAirTransaction.Where(a => a.Status == lblStatus.Value).ToList();
                            GetDDLList.SelectedIndex = GlobalCode.GetselectedIndex(GetDDLList, ArList[0].StatusID);
                            goto exit_Loop;
                        }
                    }


                exit_Loop:

                    string port = uoHiddenFieldPortCode.Value;
                    if (GlobalCode.Field2String(uoHiddenFieldSaveType.Value) == "true")
                        port = uoTextBoxStatus.Text.ToString() == "OFF" ? uoHiddenFieldPortCode.Value : uoHiddenFieldArrCode.Value;

                    var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID &&
                              n.IDBigInt == GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value) &&
                              n.SeqNo == GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)).ToList();

                    if (res != null)
                    {
                        uoListViewHotelBook.DataSource = res;
                        uoListViewHotelBook.DataBind();
                    }

                    GetAirSequence(uoTextBoxStatus.Text.ToString());

                    List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
                    GetCrewTransaction(ref cres, 1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text),
                                      GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                                      GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                                      GlobalCode.Field2Long(uoHiddenFieldSeqNo.Value),
                                      GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value),
                                      GlobalCode.Field2String(uoHiddenFieldPortCode.Value),
                                      GlobalCode.Field2String(uoHiddenFieldArrCode.Value),
                                      false, uoHiddenFieldUser.Value.ToString());

                    if (cres.Count > 0)
                    {

                        if (GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0)
                        {
                            uoCheckBoxPAMAG.Checked = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : false;
                            uoCheckBoxPATrans.Checked = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : false;
                            uoCheckBoxPAHotel.Checked = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : false;
                            uoCheckBoxPASafeguard.Checked = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : false;

                            uoCheckBoxPAMAG.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : true;
                            uoCheckBoxPATrans.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : true;
                            uoCheckBoxPAHotel.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true;
                            uoCheckBoxPASafeguard.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : true;
                        }


                        LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking);
                        LoadMeetAndGreetDetail(cres[0].CrewAssistMeetAndGreet);
                        LoadPortAgentDetail(cres[0].CrewAssistPortAgentRequest);
                        LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                        LoadSafeguardRequestDetail(cres[0].CrewAssistSafeguardRequest);

                        uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                        uoListViewTransportation.DataBind();
                    }
                    else
                    {
                        ClearHotelObject(1);
                        ClearMeetAndGreet();
                        ClearObjectPortAgent(false);
                        ClearObjectTrans();
                        ClearObjectSafeguard();
                        uoListViewTransportation.DataSource = null;
                        uoListViewTransportation.DataBind();

                    }

                }
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);

        }









        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   25/Nov/2013
        /// description     Remove if (Session["CrewAssistSafeguardVendor"] != null){}
        ///                 Safeguard was cleared hence error occurs because the selected value was removed
        /// </summary>
        //void loadHotelRequest()
        //{
        //    LoadSeafarer(1, uoTextBoxEmployeeID.Text.ToString());
        //    uoButtonLoadAir_click(null, null);

        //    //ListViewItem itemRow in this.loggerlistView.Items


        //    CheckGridCheckBox();

        //    //if (Session["CrewAssistSafeguardVendor"] != null)
        //    //{
        //    //    uoDropDownListSafeguard.DataSource = null;
        //    //    uoDropDownListSafeguard.Items.Clear();
        //    //}                        
        //}


        //private void GetGridCheckBoxChecked(string date, string time, string PortCode)
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   25/Nov/2013
        /// description     Change all  AlertMessage(ex.ToString()) to  AlertMessage(ex.Message)
        /// </summary>   
        void GetGridCheckBoxChecked(ref string date, ref string time, string port)
        {
            try
            {
                Label lblArrTime;
                Label lblArrivalDateTimeDate;

                Label lblDepartureDateTime;
                Label lblDeptTime;
                CheckBox uoSelectAirCheckBoxs;
                foreach (ListViewItem rowitem in uoListviewAir.Items)
                {
                    //item.SubItems[0].
                    lblArrivalDateTimeDate = (Label)rowitem.FindControl("lblArrivalDateTimeDate");
                    lblDepartureDateTime = (Label)rowitem.FindControl("lblDepartureDateTime");

                    lblArrTime = (Label)rowitem.FindControl("lblArrTime");
                    lblDeptTime = (Label)rowitem.FindControl("lblDeptTime");

                    uoSelectAirCheckBoxs = (CheckBox)rowitem.FindControl("uoSelectAirCheckBoxs");

                    if (uoSelectAirCheckBoxs.Checked == true)
                    {
                        date = GlobalCode.Field2Date(uoTextBoxStatus.Text.ToString() == "ON" ? lblArrivalDateTimeDate.Text : lblDepartureDateTime.Text);
                        time = GlobalCode.Field2String(uoTextBoxStatus.Text.ToString() == "ON" ? lblArrTime.Text : lblDeptTime.Text).Substring(0, 5);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }

        void CheckGridCheckBox()
        {
            try
            {
                Label myLabel;
                CheckBox myCheckBox;
                Label myPort;
                string ports = "";
                HiddenField myIDHidden;

                foreach (ListViewItem rowitem in uoListviewAir.Items)
                {
                    //item.SubItems[0].
                    myLabel = (Label)rowitem.FindControl("lblSeqNo");
                    myCheckBox = (CheckBox)rowitem.FindControl("uoSelectAirCheckBoxs");
                    myIDHidden = (HiddenField)rowitem.FindControl("hfIDBigInt");
                    myPort = (Label)rowitem.FindControl("lblDepartureAirportLocationCode");
                    if (myLabel.Text.ToString() == uoHiddenFieldSeqNo.Value)
                    {

                        myCheckBox.Checked = true;
                        uoHiddenFieldIDBigint.Value = myIDHidden.Value;
                        ports = myPort.Text.ToString();
                    }
                }
                //item.ID = ParentCheckBox.IsChecked.GetValueOrDefault();

                if (uoListviewAir.Items.Count == 0)
                {

                    HiddenField TXHiddenField;
                    //ListViewItem itemRow in this.loggerlistView.Items
                    foreach (ListViewItem rowitem in uoListviewTravel.Items)
                    {
                        //item.SubItems[0].
                        TXHiddenField = (HiddenField)rowitem.FindControl("hfTravelRequestID");
                        myCheckBox = (CheckBox)rowitem.FindControl("uoSelectCheckBoxs");

                        myIDHidden = (HiddenField)rowitem.FindControl("uoHiddenFieldTRIDBint");
                        myPort = (Label)rowitem.FindControl("lblPort");

                        if (TXHiddenField.Value.ToString() == uoHiddenFieldTravelRequestID.Value)
                        {
                            myCheckBox.Checked = true;

                            //uoHiddenFieldIDBigint
                            uoHiddenFieldIDBigint.Value = myIDHidden.Value;
                            ports = myPort.Text.ToString();

                        }
                    }
                }


                //List<CrewAssistSafeguardVendor> lst = new List<CrewAssistSafeguardVendor>();

                //lst = (List<CrewAssistSafeguardVendor>)Session["CrewAssistSafeguardVendor"];
                //var Safresult = (from dbo in lst
                //                 where dbo.PortCode == ports
                //                 select new
                //                 {
                //                     SafeguardName = dbo.SafeguardName,
                //                     SafeguardVendorID = dbo.SafeguardVendorID
                //                 })
                //        .ToList().Distinct();

                //uoDropDownListSafeguard.Items.Clear();
                //uoDropDownListSafeguard.Items.Add(new ListItem("--Select Safeguard--", "0"));
                //uoDropDownListSafeguard.DataSource = Safresult;
                //uoDropDownListSafeguard.DataTextField = "SafeguardName";
                //uoDropDownListSafeguard.DataValueField = "SafeguardVendorID";
                //uoDropDownListSafeguard.DataBind();                

                //uoDropDownListSafeguard.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }


        void LoadSeafarer(short loadtype, string Seafarer)
        {

            SF = new CrewAssistBLL();

            List<SeafarerList> SList = new List<SeafarerList>();


            SList = SF.SeafarerList(loadtype, Seafarer, uoHiddenFieldUser.Value.ToString());
            uoDropDownListName.DataTextField = "Name";
            uoDropDownListName.DataValueField = "SeafarerID";
            uoDropDownListName.DataSource = SList;
            uoDropDownListName.DataBind();

            uoListViewRemark.DataSource = null;
            uoListViewRemark.DataBind();

            uoListViewRemarkPopup.DataSource = null;
            uoListViewRemarkPopup.DataBind();


            if (SList.Count >= 1)
            {
                uoDropDownListName_SelectedIndexChanged(null, null);
            }
            else
            {

                uoDropDownListName.Items.Insert(0, new ListItem("--Select Crew--", "0"));
                uoListviewAir.DataSource = null;
                uoListviewAir.DataBind();
                uoListviewTravel.DataSource = null;
                uoListviewTravel.DataBind();
                ClearControl();

                AlertMessage("Crew Member not active in TM");

            }

            HiddenFieldSearch.Value = "";
        }

        void ClearControl()
        {

            uoTextBoxCostCenter.Text = "";
            uoTextBoxShip.Text = "";
            uoTextBoxRank.Text = "";
            uoTextBoxStatus.Text = "";
            uoTextBoxNationality.Text = "";
            uoTextBoxGender.Text = "";
            uoTextBoxBrand.Text = "";
            uoHiddenFieldGenderID.Value = "";
            uoHiddenFieldCostCenterID.Value = "";
            uoHiddenFieldRank.Value = "";
            uoHiddenFieldVesselID.Value = "";
            uoHiddenFieldBrandID.Value = "";

            uoTextBoxReasonCode.Text = "";

            uoTextBoxLastName.Text = "";
            uoTextBoxEmployeeID.Text = "";
            uoTextBoxFirstName.Text = "";
            uoHiddenFieldHotelRequestDetailID.Value = "";

            uoDropDownListHotel.SelectedIndex = -1;
            uoTextBoxMealVoucher.Text = "";

        }

        List<SeafarerDetailHeader> _SDetailList = new List<SeafarerDetailHeader>();
        protected void uoDropDownListName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SeafarerDetailList"] = null;

            uoListviewAir.DataSource = null;
            uoListviewAir.DataBind();

            SF = new CrewAssistBLL();
            _SDetailList = new List<SeafarerDetailHeader>();
            _SDetailList = SF.SeafarerDetailList(0, GlobalCode.Field2Long(uoDropDownListName.SelectedValue), uoHiddenFieldUser.Value.ToString());

            uoTextBoxCostCenter.Text = "";
            uoTextBoxShip.Text = "";
            uoTextBoxRank.Text = "";
            uoTextBoxStatus.Text = "";
            uoTextBoxNationality.Text = "";
            uoTextBoxGender.Text = "";
            uoTextBoxBrand.Text = "";
            uoHiddenFieldGenderID.Value = "";
            uoHiddenFieldCostCenterID.Value = "";
            uoHiddenFieldRank.Value = "";
            uoHiddenFieldVesselID.Value = "";
            uoHiddenFieldBrandID.Value = "";

            uoTextBoxLastName.Text = "";

            uoTextBoxEmployeeID.Text = "";

            uoTextBoxFirstName.Text = "";
            uoHiddenFieldHotelRequestDetailID.Value = "";

            ClearObjectTrans();
            ClearObjectVisa();
            ClearObjectPortAgent(false);
            ClearHotelObject(1);

            uoListViewRemark.DataSource = null;
            uoListViewRemark.DataBind();

            uoListViewRemarkPopup.DataSource = null;
            uoListViewRemarkPopup.DataBind();

            if (_SDetailList.Count > 0)
            {

                uoListviewTravel.DataSource = _SDetailList[0].SeafarerDetailList;
                uoListviewTravel.DataBind();

                uoHiddenFieldHotelRequestDetailID.Value = _SDetailList[0].TravelRequetID.ToString();
                uoTextBoxRank.Text = _SDetailList[0].RankCode.ToString() + '-' + _SDetailList[0].RankName.ToString();

                uoTextBoxNationality.Text = _SDetailList[0].NationalityCode.ToString() + '-' + _SDetailList[0].Nationality.ToString();

                uoHiddenFieldNationality.Value = _SDetailList[0].NationalityID.ToString();

                uoTextBoxGender.Text = _SDetailList[0].Gender.ToString();
                uoHiddenFieldBrandID.Value = _SDetailList[0].BrandID.ToString();

                uoTextBoxVNationality.Text = _SDetailList[0].Nationality.ToString();
                uoTextBoxReasonCode.Text = GlobalCode.Field2String(_SDetailList[0].ReasonCode).ToString();


                uoTextBoxLastName.Text = _SDetailList[0].LastName.ToString();
                uoTextBoxEmployeeID.Text = _SDetailList[0].SeafarerID.ToString();
                uoHiddenFieldGenderID.Value = _SDetailList[0].GenderID.ToString();
                uoTextBoxFirstName.Text = _SDetailList[0].FirstName.ToString();

                uoHiddenFieldCostCenterID.Value = _SDetailList[0].CostCenterID.ToString();
                uoHiddenFieldRank.Value = _SDetailList[0].RankID.ToString();
                uoTextBoxComment.Text = _SDetailList[0].HotelComments.ToString();

                LoadRemark(_SDetailList[0].Remark);

                if (_SDetailList[0].SeafarerDetailList.Count > 0)
                {
                    uoTextBoxCostCenter.Text = _SDetailList[0].SeafarerDetailList[0].CostCenterCode.ToString() + '-' + _SDetailList[0].CostCenterName.ToString();
                    uoTextBoxBrand.Text = _SDetailList[0].BrandCode.ToString() + '-' + _SDetailList[0].Brand.ToString();
                    uoHiddenFieldVesselID.Value = _SDetailList[0].SeafarerDetailList[0].VesselID.ToString();
                    uoTextBoxShip.Text = _SDetailList[0].SeafarerDetailList[0].VesselCode.ToString() + '-' + _SDetailList[0].Vessel.ToString();
                    uoTextBoxStatus.Text = _SDetailList[0].SeafarerDetailList[0].Status.ToString();

                }

                if (_SDetailList[0].CrewAssistShipEmail.Count > 0)
                {
                    uoHiddenFieldVesselEmail.Value = _SDetailList[0].CrewAssistShipEmail[0].Email;

                    CheckBoxCopyShip.ToolTip = uoHiddenFieldVesselEmail.Value.ToString();
                }

                if (_SDetailList[0].CopyEmail.Count > 0)
                {
                    var crewAssEmail = (from s in _SDetailList[0].CopyEmail
                                        where s.EmailName == "Crew Assist"
                                        select new CopyEmail
                                        {
                                            Email = s.Email,
                                            EmailName = s.EmailName,
                                            EmailType = s.EmailType,
                                        }).ToList();

                    if (crewAssEmail.Count > 0)
                    {
                        uoHiddenFieldCrewAssist.Value = crewAssEmail[0].Email;
                        CheckBoxCopycrewassist.ToolTip = uoHiddenFieldCrewAssist.Value;
                    }


                    var Scheduler = (from i in _SDetailList[0].CopyEmail
                                     where i.EmailName == "Scheduler"
                                     select new CopyEmail
                                     {
                                         Email = i.Email,
                                         EmailName = i.EmailName,
                                         EmailType = i.EmailType,
                                     }).ToList();

                    if (Scheduler.Count > 0)
                    {
                        uoHiddenFieldScheduler.Value = Scheduler[0].Email;
                        CheckBoxScheduler.ToolTip = uoHiddenFieldScheduler.Value;
                    }

                    var VendorEmail = (from n in _SDetailList[0].CopyEmail
                                       where n.EmailName == "Hotel Specialist"
                                       select new CopyEmail
                                       {
                                           Email = n.Email,
                                           EmailName = n.EmailName,
                                           EmailType = n.EmailType,
                                       }).ToList();

                    if (VendorEmail.Count > 0)
                    {
                        uoHiddenFieldEmailHotel.Value = VendorEmail[0].Email;
                        CheckBoxCopycrewhotels.ToolTip = uoHiddenFieldEmailHotel.Value;
                    }
                }
            }

            Session["SeafarerDetailList"] = _SDetailList;
        }


        private void LoadRemark(List<CrewAssisRemark> Lst)
        {
            try
            {

                Lst = _SDetailList[0].Remark;

                uoListViewRemarkPopup.DataSource = Lst;
                uoListViewRemarkPopup.DataBind();

                uoListViewRemark.DataSource = Lst;
                uoListViewRemark.DataBind();

                uoTextBoxRemTransTime.Text = "";
                uoTextBoxRemTransdate.Text = "";

            }
            catch
            {
                uoListViewRemark.DataSource = null;
                uoListViewRemark.DataBind();

                uoListViewRemarkPopup.DataSource = null;
                uoListViewRemarkPopup.DataBind();

            }

        }
        protected void uoDropDownListVCountryVisiting_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SF = new CrewAssistBLL();
                uoTextBoxVRequired.Text = SF.GetNationalityVisa(0, GlobalCode.Field2Int(uoHiddenFieldNationality.Value), GlobalCode.Field2Int(uoDropDownListVCountryVisiting.SelectedValue));
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }

        /// <summary>
        /// Modified By:		Josephine Gad
        /// Create date:		25/Nov/2013
        /// Description:		Get Room Type of Crew
        /// ================================================= 
        /// </summary>
        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //uoButtonApprove.Visible = false;
            //uoButtonCancelHotel.Visible = false;
            uoHiddenFieldContractStart.Value = "";
            uoHiddenFieldContractEnd.Value = "";




            if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
            {
                GetCrewAssistPAHotelInformation(GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue));
            }
            else if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -")
            {
                GetCrewAssistHotelInformation();
            }
            else
            {
                ClearHotelObject(0);
            }

        }


        void GetCrewAssistHotelInformation()
        {
            SF = new CrewAssistBLL();
            Session["ContactPerson"] = null;
            Session["ContractRate"] = null;
            Session["HotelEmailTo"] = null;

            DateTime starDate = DateTime.Now;
            DateTime enddate = DateTime.Now;

            List<CrewAssistHotelInformation> _HotelInformationList = new List<CrewAssistHotelInformation>();


            _HotelInformationList = SF.CrewAssistHotelInformation(GlobalCode.Field2Int(uoHiddenFieldTravelRequestID.Value),
                    GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue),
                    uoHiddenFieldPortCode.Value, uoHiddenFieldArrCode.Value, starDate, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text));
            uoLabelAdress.Text = "";
            uoLabelTelephone.Text = "";
            uoTextContractedRate.Text = "";
            uoTextBoxMealVoucher.Text = "";
            uoTextBoxComfirmRate.Text = "";
            CheckBoxEmail.Checked = false;
            uoTextBoxEmail.Text = "";
            //CheckBoxFax.Checked = false;
            //CheckBoxFax.Text = "Fax : ";
            CheckBoxCopycrewassist.Checked = false;
            CheckBoxCopycrewhotels.Checked = false;



            if (uoTextBoxStatus.Text.ToString() == "ON")
            {
                if (uoHiddenFieldRequestDate.Value == "")
                {
                    starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
                    enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                }
                else
                {
                    starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(-1));
                    enddate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
                }
            }
            else
            {
                if (uoHiddenFieldRequestDate.Value == "")
                {
                    starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
                    enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                }
                else
                {
                    enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                    starDate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
                }

            }
            uoTextBoxCheckinDate.Text = GlobalCode.Field2Date(starDate);
            uoTextBoxCheckoutDate.Text = GlobalCode.Field2Date(enddate);

            uoTextBoxDuration.Text = "1";

            uoTxtBoxTimeOut.Text = DateTime.Now.Hour.ToString() + ":00";
            uoTxtBoxTimeIn.Text = DateTime.Now.Hour.ToString() + ":00";

            uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, _HotelInformationList[0].CurrencyID);

            if (_HotelInformationList.Count > 0)
            {
                uoLabelAdress.Text = _HotelInformationList[0].Address;
                uoLabelTelephone.Text = _HotelInformationList[0].ContactNo;
                uoHiddenFieldCityCode.Value = _HotelInformationList[0].CityCode;

                uoTextContractedRate.Text = _HotelInformationList[0].ContractedRate;

                uoTextBoxMealVoucher.Text = _HotelInformationList[0].MealVoucher;
                uoTextBoxComfirmRate.Text = _HotelInformationList[0].ContractRoomRateTaxPercentage;

                uoTextBoxEmail.Text = _HotelInformationList[0].EmailTo;
                //CheckBoxFax.Text =  "Fax : " + _HotelInformationList[0].FaxNo;

                TextBoxWhoConfirm.Text = _HotelInformationList[0].ContactPerson;

                if (uoTextBoxEmail.Text.ToString().Length > 0)
                {
                    CheckBoxEmail.Checked = true;
                    uoTextBoxEmail.Enabled = true;
                }

                uoCheckboxBreakfast.Checked = _HotelInformationList[0].IsBreakfast;
                uoCheckboxLunch.Checked = _HotelInformationList[0].IsLunch;
                uoCheckboxDinner.Checked = _HotelInformationList[0].IsDinner;

                uoCheckBoxIsWithShuttle.Checked = _HotelInformationList[0].IsWithShuttle;


                uoHiddenFieldHotelEmail.Value = "";

                if (_HotelInformationList[0].ATTEMail.Count > 0)
                {
                    uoHiddenFieldHotelEmail.Value = _HotelInformationList[0].ATTEMail[0].Email;
                    Session["HotelEmailTo"] = _HotelInformationList[0].ATTEMail;
                }

                Session["ContactPerson"] = _HotelInformationList[0].ContactPerson;
                Session["ContractRate"] = _HotelInformationList[0].ContractedRate;
                Session["HotelEmailTo"] = _HotelInformationList[0].EmailTo;


                uoHiddenFieldContractStart.Value = _HotelInformationList[0].ContractDateStarted;
                uoHiddenFieldContractEnd.Value = _HotelInformationList[0].ContractDateEnd;



                uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(_HotelInformationList[0].RoomTypeID);

            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);


        }

        void GetCrewAssistPAHotelInformation(long ID)
        {
            try
            {
                uoHiddenFieldContractStart.Value = "";
                uoHiddenFieldContractEnd.Value = "";

                SF = new CrewAssistBLL();
                Session["ContactPerson"] = null;
                Session["ContractRate"] = null;
                Session["HotelEmailTo"] = null;

                DateTime starDate = DateTime.Now;
                DateTime enddate = DateTime.Now;

                List<CrewAssistHotelInformation> _HotelInformationList = new List<CrewAssistHotelInformation>();

                _HotelInformationList = SF.GetPortAgentHotelVendor(0, ID,
                        GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                        GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value), uoHiddenFieldPortCode.Value);

                uoLabelAdress.Text = "";
                uoLabelTelephone.Text = "";
                uoTextContractedRate.Text = "";
                uoTextBoxMealVoucher.Text = "";
                uoTextBoxComfirmRate.Text = "";
                CheckBoxEmail.Checked = false;
                uoTextBoxEmail.Text = "";
                //CheckBoxFax.Checked = false;
                //CheckBoxFax.Text = "Fax : ";
                CheckBoxCopycrewassist.Checked = false;
                CheckBoxCopycrewhotels.Checked = false;

                if (uoTextBoxStatus.Text.ToString() == "ON")
                {
                    if (uoHiddenFieldRequestDate.Value == "")
                    {
                        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
                        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                    }
                    else
                    {
                        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(-1));
                        enddate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
                    }
                }
                else
                {
                    if (uoHiddenFieldRequestDate.Value == "")
                    {
                        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
                        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                    }
                    else
                    {
                        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                        starDate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
                    }

                }
                uoTextBoxCheckinDate.Text = GlobalCode.Field2Date(starDate);
                uoTextBoxCheckoutDate.Text = GlobalCode.Field2Date(enddate);

                uoTextBoxDuration.Text = "1";

                uoTxtBoxTimeOut.Text = DateTime.Now.Hour.ToString() + ":00";
                uoTxtBoxTimeIn.Text = DateTime.Now.Hour.ToString() + ":00";

                if (_HotelInformationList.Count > 0)
                {
                    uoLabelAdress.Text = _HotelInformationList[0].Address;
                    uoLabelTelephone.Text = _HotelInformationList[0].ContactNo;
                    uoHiddenFieldCityCode.Value = _HotelInformationList[0].CityCode;
                    uoTextContractedRate.Text = _HotelInformationList[0].ContractedRate;


                    uoTextBoxMealVoucher.Text = _HotelInformationList[0].MealVoucher;


                    uoTextBoxComfirmRate.Text = _HotelInformationList[0].ContractRoomRateTaxPercentage;

                    uoTextBoxEmail.Text = _HotelInformationList[0].EmailTo;
                    //CheckBoxFax.Text =  "Fax : " + _HotelInformationList[0].FaxNo;

                    TextBoxWhoConfirm.Text = _HotelInformationList[0].ContactPerson;

                    if (uoTextBoxEmail.Text.ToString().Length > 0)
                    {
                        CheckBoxEmail.Checked = true;
                        uoTextBoxEmail.Enabled = true;
                    }

                    uoCheckboxBreakfast.Checked = _HotelInformationList[0].IsBreakfast;
                    uoCheckboxLunch.Checked = _HotelInformationList[0].IsLunch;
                    uoCheckboxDinner.Checked = _HotelInformationList[0].IsDinner;

                    uoCheckBoxIsWithShuttle.Checked = _HotelInformationList[0].IsWithShuttle;

                    uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, _HotelInformationList[0].CurrencyID);

                    uoHiddenFieldHotelEmail.Value = "";

                    if (_HotelInformationList[0].ATTEMail.Count > 0)
                    {
                        uoHiddenFieldHotelEmail.Value = _HotelInformationList[0].ATTEMail[0].Email;
                        Session["HotelEmailTo"] = _HotelInformationList[0].ATTEMail;
                    }

                    Session["ContactPerson"] = _HotelInformationList[0].ContactPerson;
                    Session["ContractRate"] = _HotelInformationList[0].ContractedRate;
                    Session["HotelEmailTo"] = _HotelInformationList[0].EmailTo;


                    uoHiddenFieldContractStart.Value = _HotelInformationList[0].ContractDateStarted;
                    uoHiddenFieldContractEnd.Value = _HotelInformationList[0].ContractDateEnd;

                    uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(_HotelInformationList[0].RoomTypeID);
                }

                ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);

            }
            catch (Exception ex)
            {
                AlertMessage("DownListPort: " + ex.Message);
            }
        }



        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (uoDropDownListPort.SelectedIndex == 0)
                {
                    LoadAllCombo();
                    return;
                }
                List<CrewAssistHotelList> list = new List<CrewAssistHotelList>();
                SF = new CrewAssistBLL();
                list = SF.GetGetHotelPortExpendTypeList(1, uoHiddenFieldUser.Value, "0", uoDropDownListPort.SelectedValue.ToString());
                uoDropDownListHotel.Items.Clear();
                int iRowCount = list.Count;
                if (iRowCount > 0)
                {
                    uoHiddenFieldCityCode.Value = list[0].Portcode;
                    uoDropDownListHotel.DataSource = list;
                    uoDropDownListHotel.DataTextField = "HotelName";
                    uoDropDownListHotel.DataValueField = "HotelID";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));

                }
                if (uoDropDownListPort.SelectedIndex > 0)
                {
                    uoHiddenFieldPortCode.Value = GetSeaport(uoDropDownListPort.SelectedItem.Text.ToString());
                }


                BindVendors(uoHiddenFieldUser.Value, GlobalCode.Field2Int(uoDropDownListPort.SelectedValue), 0);

            }
            catch (Exception ex)
            {
                AlertMessage("DownListPort: " + ex.Message);
            }
        }
        private string GetSeaport(string value)
        {
            string PortCode = "";
            try
            {

                char delimiterChars = '-';
                string[] val = value.Split(delimiterChars);



                foreach (string s in val)
                {
                    PortCode = s.Replace(" ", "");
                    break;
                }

                return PortCode;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Date Modified: 25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Clear Air List, Hotel List and Vehicle List
        /// </summary>        
        protected void uoButtonSearch_click(object sender, EventArgs e)
        {
            try
            {

                uoHiddenFieldTravelRequestID.Value = "";
                uoHiddenFieldIDBigint.Value = "";

                if (uoLabelUserID.Text.ToString() == "") Response.Redirect("~/login.aspx");

                switch (HiddenFieldSearch.Value.ToString())
                {
                    case "1":
                        LoadSeafarer(1, uoTextBoxEmployeeID.Text.ToString());
                        break;
                    case "2":
                        LoadSeafarer(2, uoTextBoxFirstName.Text.ToString());
                        break;
                    case "3":
                        LoadSeafarer(3, uoTextBoxLastName.Text.ToString());
                        break;
                    default:
                        if (GlobalCode.Field2Long(uoTextBoxEmployeeID.Text) > 0)
                            LoadSeafarer(1, uoTextBoxEmployeeID.Text.ToString());
                        else if (uoTextBoxFirstName.Text.ToString() != "")
                            LoadSeafarer(1, uoTextBoxEmployeeID.Text.ToString());
                        else if (uoTextBoxLastName.Text.ToString() != "")
                            LoadSeafarer(3, uoTextBoxLastName.Text.ToString());
                        else
                        {
                            uoListviewAir.DataSource = null;
                            uoListviewAir.DataBind();
                            uoListviewTravel.DataSource = null;
                            uoListviewTravel.DataBind();
                        }
                        break;
                }
                CheckGridCheckBox();
                uoListViewCompanionList.DataSource = null;
                uoListViewCompanionList.DataBind();

                uoListviewAir.DataSource = null;
                uoListviewAir.DataBind();

                uoListViewHotelBook.DataSource = null;
                uoListViewHotelBook.DataBind();

                uoListViewTransportation.DataSource = null;
                uoListViewTransportation.DataBind();


                //uoListViewPATranpo.DataSource = null;
                //uoListViewPATranpo.DataBind();


                uoListViewTranspoCost.DataSource = null;
                uoListViewTranspoCost.DataBind();

                List<VehicleTransactionPortAgent> list1 = new List<VehicleTransactionPortAgent>();


                uoListViewTranportationRoute.DataSource = list1;
                uoListViewTranportationRoute.DataBind();


                EnableHotelControl(true);
                EnableTransporationControl(true);
                EnablePortAgentControl(true);
                EnableCrewScheduleControl(true);


                txtRemark.Text = "";
                uoHiddenFieldRemarkID.Value = "";
                txtSummaryCall.Text = "";

                uoTextBoxRemTransdate.Text = null;
                uoTextBoxRemTransTime.Text = null;

                cboRemarkType.SelectedIndex = 0;
                cboRemarkStatus.SelectedIndex = 0;
                cboRemarkRequestor.SelectedIndex = 0;


                ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);


            }
            catch (Exception ex)
            {
                AlertMessage("ButtonSearch: " + ex.Message);
            }
        }


        /// ----------------------------------------------
        /// Modified By:    Muhallidin G Wali
        /// Date Modified:  07/02/2012
        /// Description:    Add option "Select ALL Hotel" ,"-1" if there is selected Region
        /// ----------------------------------------------
        /// </summary>
        private void LoadAllCombo()
        {
            List<CrewAssistGenericClass> list = new List<CrewAssistGenericClass>();
            try
            {
                SF = new CrewAssistBLL();
                list = SF.GetGetHotelPortExpendTypeList(uoHiddenFieldUser.Value, "0", "0");
                uoDropDownListHotel.Items.Clear();
                uoDropDownListPort.Items.Clear();
                uoDropDownListExpenseType.Items.Clear();

                Session["CrewAssistVehicleVendor"] = null;

                int iRowCount = list.Count;
                if (iRowCount > 0)
                {
                    Session["HotelNameList"] = list[0].CrewAssistHotelList;
                    uoDropDownListHotel.DataSource = list[0].CrewAssistHotelList;
                    uoDropDownListHotel.DataTextField = "HotelName";
                    uoDropDownListHotel.DataValueField = "HotelID";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));


                    //if (list[0].CrewAssistHotelList.Count() > 1)
                    //{
                    //    for (int i = 0; i < list[0].CrewAssistHotelList.Count; i++)
                    //    {
                    //        if (list[0].CrewAssistHotelList[i].IsPortAgent == true)
                    //            uoDropDownListHotel.Items[i + 1].Attributes.Add("style", "background-color:#00FFFF");
                    //        else
                    //            uoDropDownListHotel.Items[i + 1].Attributes.Add("style", "background-color:#C0C0C0");

                    //    }

                    //}




                    Session["CrewAssitPortList"] = list[0].CrewAssitPortList;
                    uoDropDownListPort.DataSource = list[0].CrewAssitPortList;
                    uoDropDownListPort.DataTextField = "PORTName";
                    uoDropDownListPort.DataValueField = "PORTID";
                    uoDropDownListPort.DataBind();
                    uoDropDownListPort.Items.Insert(0, new ListItem("--Select Port--", "0"));

                    uoDropDownListExpenseType.DataSource = list[0].CrewAssitExpendTypeList;
                    uoDropDownListExpenseType.DataTextField = "ExpendType";
                    uoDropDownListExpenseType.DataValueField = "ExpendTypeID";
                    uoDropDownListExpenseType.DataBind();
                    //uoDropDownListExpenseType.Items.FindByText("Hotel").Selected = true;

                    if (list[0].CrewAssitExpendTypeList.Count > 0)
                    {
                        uoDropDownListExpenseType.Items.FindByValue("1").Selected = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);
                    }

                    uoDropDownListCurrency.DataSource = list[0].CrewAssistCurrency;
                    uoDropDownListCurrency.DataTextField = "CurrencyName";
                    uoDropDownListCurrency.DataValueField = "CurrencyID";
                    uoDropDownListCurrency.DataBind();


                    //uoDropDownListPACurrentcy.DataSource = list[0].CrewAssistCurrency;
                    //uoDropDownListPACurrentcy.DataTextField = "CurrencyName";
                    //uoDropDownListPACurrentcy.DataValueField = "CurrencyID";
                    //uoDropDownListPACurrentcy.DataBind();

                    uoDropDownListRouteFrom.Items.Clear();
                    uoDropDownListRouteFrom.Items.Add(new ListItem("--Select Route From--", "0"));
                    uoDropDownListRouteFrom.DataSource = list[0].CrewAssistRout;
                    uoDropDownListRouteFrom.DataTextField = "RoutName";
                    uoDropDownListRouteFrom.DataValueField = "RoutId";
                    uoDropDownListRouteFrom.DataBind();
                    //uoDropDownListRoute.Items.Insert(0, new ListItem("--Select Route--", "0"));

                    //uoDropDownListRouteTO.Items.Clear();
                    //uoDropDownListRouteTO.Items.Add(new ListItem("--Select Route To--", "0"));
                    //uoDropDownListRouteTO.DataSource = list[0].CrewAssistRout;
                    //uoDropDownListRouteTO.DataTextField = "RoutName";
                    //uoDropDownListRouteTO.DataValueField = "RoutId";
                    //uoDropDownListRouteTO.DataBind();
                    //uoDropDownListRouteTO.Items.Insert(0, new ListItem("--Select Route--", "0"));

                    uoDropDownListMAGTo.Items.Clear();
                    uoDropDownListMAGTo.Items.Add(new ListItem("--Select To--", "0"));
                    uoDropDownListMAGTo.DataSource = list[0].CrewAssistRout;
                    uoDropDownListMAGTo.DataTextField = "RoutName";
                    uoDropDownListMAGTo.DataValueField = "RoutId";
                    uoDropDownListMAGTo.DataBind();


                    //uoDropDownListPAFrom.Items.Clear();
                    //uoDropDownListPAFrom.Items.Add(new ListItem("--Select To--", "0"));
                    //uoDropDownListPAFrom.DataSource = list[0].CrewAssistRout;
                    //uoDropDownListPAFrom.DataTextField = "RoutName";
                    //uoDropDownListPAFrom.DataValueField = "RoutId";
                    //uoDropDownListPAFrom.DataBind();

                    //uoDropDownListPATo.Items.Clear();
                    //uoDropDownListPATo.Items.Add(new ListItem("--Select To--", "0"));
                    //uoDropDownListPATo.DataSource = list[0].CrewAssistRout;
                    //uoDropDownListPATo.DataTextField = "RoutName";
                    //uoDropDownListPATo.DataValueField = "RoutId";
                    //uoDropDownListPATo.DataBind();

                    ListView mylist = ((ListView)uoListViewTranportationRoute);
                    ListViewItem lvi = null;
                    if (mylist.Controls.Count == 1)
                        lvi = mylist.Controls[0] as ListViewItem;



                    //ListView mylist = ((ListView)uoListViewTranportationRoute) 


                    //if (lvi == null || lvi.ItemType != ListViewItemType.EmptyItem)
                    //    return;

                    //Literal literal1 = (Literal)lvi.FindControl("Literal1");
                    //if (literal1 != null)
                    //    literal1.Text = "No items to display";





                    Session["CrewAssistVehicleVendor"] = list[0].VehicleVendor;
                    var Vehresult = (from dbo in list[0].VehicleVendor
                                     select new
                                     {
                                         Vehicle = dbo.Vehicle,
                                         VehicleID = dbo.VehicleID,
                                         IsPortAgent = dbo.IsPortAgent
                                     }).Distinct();

                    uoDropDownListVehicleVendor.DataSource = null;
                    uoDropDownListVehicleVendor.Items.Clear();

                    uoDropDownListVehicleVendor.DataSource = Vehresult;
                    uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                    uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                    uoDropDownListVehicleVendor.DataBind();
                    uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));


                    uoDropDownListVCountryVisiting.DataSource = list[0].CrewAssistNationality;
                    uoDropDownListVCountryVisiting.DataTextField = "Nationality";
                    uoDropDownListVCountryVisiting.DataValueField = "NatioalityID";
                    uoDropDownListVCountryVisiting.DataBind();
                    uoDropDownListVCountryVisiting.Items.Insert(0, new ListItem("--Select Country Visit--", "0"));


                    Session["MeetAndGreetVendor"] = list[0].CrewAssistMeetAndGreetVendor;
                    var result = (from dbo in list[0].CrewAssistMeetAndGreetVendor
                                  select new
                                  {
                                      MeetAndGreetVendor = dbo.MeetAndGreetVendor,
                                      MeetAndGreetVendorID = dbo.MeetAndGreetVendorID
                                  })
                                  .ToList().Distinct();

                    //var rr = list[0].CrewAssistMeetAndGreetVendor.Select(e => new { e.MeetAndGreetVendor, e.MeetAndGreetVendorID }).Distinct();
                    uoDropDownListMAndGVendor.DataSource = null;
                    uoDropDownListMAndGVendor.Items.Clear();

                    uoDropDownListMAndGVendor.DataSource = result;
                    uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                    uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                    uoDropDownListMAndGVendor.DataBind();
                    uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));

                    Session["PortAgentVendor"] = list[0].CrewAssistVendorPortAgent;
                    var PAsult = (from dbo in list[0].CrewAssistVendorPortAgent
                                  select new
                                  {
                                      PortAgentVendorName = dbo.PortAgentVendorName,
                                      PortAgentVendorID = dbo.PortAgentVendorID
                                  })
                                .ToList().Distinct();


                    uoDropDownListPortAgent.DataSource = PAsult;
                    uoDropDownListPortAgent.DataTextField = "PortAgentVendorName";
                    uoDropDownListPortAgent.DataValueField = "PortAgentVendorID";
                    uoDropDownListPortAgent.DataBind();
                    uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider--", "0"));




                    var Safresult = (from dbo in list[0].CrewAssistSafeguardVendor
                                     select new
                                     {
                                         SafeguardName = dbo.SafeguardName,
                                         SafeguardVendorID = dbo.SafeguardVendorID
                                     })
                                  .ToList().Distinct();

                    Session["CrewAssistSafeguardVendor"] = list[0].CrewAssistSafeguardVendor;

                    uoDropDownListSafeguard.Items.Clear();
                    uoDropDownListSafeguard.Items.Add(new ListItem("--Select Safeguard--", "0"));
                    uoDropDownListSafeguard.DataSource = Safresult;
                    uoDropDownListSafeguard.DataTextField = "SafeguardName";
                    uoDropDownListSafeguard.DataValueField = "SafeguardVendorID";
                    uoDropDownListSafeguard.DataBind();

                    //uoDropDownListSafeguard.SelectedValue = "0";



                    cboAirportList.DataSource = list[0].CrewAssistAirport;
                    cboAirportList.DataTextField = "PortName";
                    cboAirportList.DataValueField = "PortCode";
                    cboAirportList.DataBind();

                    cboRemarkType.Items.Clear();
                    cboRemarkType.Items.Add(new ListItem("--Select Type--", "0"));
                    cboRemarkType.DataSource = list[0].RemarkType.OrderBy(r => r.RemarkType);
                    cboRemarkType.DataTextField = "RemarkType";
                    cboRemarkType.DataValueField = "RemarkTypeID";
                    cboRemarkType.DataBind();
 
                    foreach (CRRemarkType c in list[0].RemarkType.OrderBy(r => r.RemarkType))
                    {
                        TreeNode ParentNode = new TreeNode();
                        ParentNode.Text = c.RemarkType;
                        ParentNode.Value = c.RemarkTypeID.ToString();
                        ParentNode.SelectAction =TreeNodeSelectAction.None;
                        Remarktreeview.Nodes.Add(ParentNode);
                        foreach(RemarkTypeDetail n in c.RemarkTypeDetail.OrderBy(r => r.RemarkTypeDet))
                        {
                            TreeNode ChildNode = new TreeNode();
                            ChildNode.Text = n.RemarkTypeDet;
                            ChildNode.Value = n.RemarkTypeDetID.ToString();
                            ParentNode.ChildNodes.Add(ChildNode);
                        }
                    }

                


                    cboRemarkStatus.Items.Clear();
                    cboRemarkStatus.Items.Add(new ListItem("--Select Remark Status--", "0"));
                    cboRemarkStatus.DataSource = list[0].RemarkStatus.OrderBy(r => r.RemarkType);
                    cboRemarkStatus.DataTextField = "RemarkType";
                    cboRemarkStatus.DataValueField = "RemarkTypeID";
                    cboRemarkStatus.DataBind();

                    cboRemarkRequestor.Items.Clear();
                    cboRemarkRequestor.Items.Add(new ListItem("--Select Remark Requestor--", "0"));
                    cboRemarkRequestor.DataSource = list[0].RemarkRequestor.OrderBy(r => r.RemarkType);
                    cboRemarkRequestor.DataTextField = "RemarkType";
                    cboRemarkRequestor.DataValueField = "RemarkTypeID";
                    cboRemarkRequestor.DataBind();

                }

            }
            catch (Exception ex)
            {
                AlertMessage("LoadAllCombo: " + ex.Message);
            }
        }




        private void BindListViewTravel()
        {
            uoListviewAir.DataSource = null;
            uoListviewTravel.DataSource = null;

            uoListviewAir.DataBind();
            uoListviewTravel.DataBind();

            uoListViewHotelBook.DataBind();
            uoListViewHotelBook.DataBind();

            uoListViewTransportation.DataBind();
            uoListViewTransportation.DataBind();

        }

        protected void uoButtonAddComp_Click(object sender, EventArgs e)
        {
            //if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
            //{
            //    AddHotelRequestCompanion();
            //}
            //else
            //{
            //    AlertMessage("Hotel Request is required to be able to add companion ");
            //}

            if (uoDropDownListHotel.SelectedIndex > 0)
            {

                AddHotelRequestCompanion(GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0 ? true : false);
                uoHiddenFieldSaveHotel.Value = "1";
            }

        }


        protected void uoButtonSendComp_Click(object sender, EventArgs e)
        {
            if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
            {

                string[] Eseparators = { ",", ";", " " };
                string recipient = "";
                string EmailAdd = uoTextBoxEmail.Text.ToString();

                string[] separators = { ",", ";", " " };

                string[] words = EmailAdd.Split(Eseparators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    recipient += word + "; ";
                }

                string cssEmail = "";

                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                }

                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                }

                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                }
                if (uoTextBoxEmailOther.Text != "")
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }

                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {
                    ccrecipient += word + "; ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }

                if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value.ToString()) > 0 && CheckBoxEmail.Checked == true && recipient.Length > 0)
                {
                    SF = new CrewAssistBLL();
                    SF.SendCompanionHotelTransaction(GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value), uoHiddenFieldUser.Value, recipient, cssEmail, "", false);
                }

            }
            else
            {
                AlertMessage("Add hotel request first enable to add companion... ");
            }
        }



        protected void uoButtonLoadAllCrew_click(object sender, EventArgs e)
        {
            BindListViewTravel();

            PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginLoadStaffs, OnEndLoadStaffs, null, "Async1", true);
            Page.RegisterAsyncTask(TaskPort1);
        }

        public IAsyncResult OnBeginLoadStaffs(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }

        public void OnEndLoadStaffs(IAsyncResult ar)
        {
            _dlgtSeafarer.EndInvoke(ar);
            LoadSeafarer(0, "");
        }

        /// <summary>
        /// Date Created:   27/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Save Hotel Request Companion
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void AddHotelRequestCompanion(bool IsExist)
        {
            try
            {


                List<HotelRequestCompanion> HRC = new List<HotelRequestCompanion>();

                ListView lst = (ListView)uoListViewCompanionList;
                if (lst.Items.Count > 0)
                {


                    foreach (ListViewItem ls in lst.Items)
                    {
                        HRC.Add(new HotelRequestCompanion
                        {

                            FIRSTNAME = ((Label)ls.FindControl("uoLabelFirstname")).Text.ToString(),
                            LASTNAME = ((Label)ls.FindControl("uoLabelLastname")).Text.ToString(),
                            RELATIONSHIP = ((Label)ls.FindControl("uoLabelRelationship")).Text.ToString(),
                            GENDER = ((Label)ls.FindControl("uoLabelGender")).Text.ToString(),
                            GENDERID = GlobalCode.Field2Int(((HiddenField)ls.FindControl("uoHiddenFieldGENDERID")).Value),

                            REQUESTID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldComReqID")).Value),
                            DETAILID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldComReqDetID")).Value),


                            TRAVELREQID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldTRID")).Value),
                            IDBIGINT = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldIDBIGNT")).Value),
                            SEQNO = GlobalCode.Field2Int(((HiddenField)ls.FindControl("uoHiddenFieldSeqNo")).Value),
                            IsPortAgent = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("uoHiddenFieldPA")).Value),
                            IsMedical = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("uoHiddenFieldComIsMedical")).Value),
                        });
                    }
                }

                bool IsPortAgent = uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -" ? false : true;

                HRC.Add(new HotelRequestCompanion
                {

                    FIRSTNAME = uoTextBoxCompFirstname.Text.ToString(),
                    LASTNAME = uoTextBoxCompLastname.Text.ToString(),
                    RELATIONSHIP = uoTextBoxCompRelationship.Text.ToString(),
                    GENDER = GlobalCode.Field2String(uoDropDownListCompGender.SelectedItem.Text),
                    GENDERID = GlobalCode.Field2Int(uoDropDownListCompGender.SelectedItem.Value),
                    REQUESTID = GlobalCode.Field2Int(uoHiddenFieldHotelRequestDetailID.Value),

                    TRAVELREQID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                    IDBIGINT = GlobalCode.Field2Int(uoHiddenFieldIDBigint.Value),
                    SEQNO = GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value),

                    IsPortAgent = uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -" ? false : true,
                    IsMedical = uoCheckBoxAddCompanion.Checked

                });

                uoListViewCompanionList.DataSource = null;
                uoListViewCompanionList.DataBind();
                uoListViewCompanionList.DataSource = HRC;
                uoListViewCompanionList.DataBind();

                uoCheckBoxAddCompanion.Checked = true;
                uoTextBoxCompFirstname.Text = "";
                uoTextBoxCompLastname.Text = "";
                uoTextBoxCompRelationship.Text = "";
                uoDropDownListCompGender.SelectedIndex = 0;
                uoHiddenFieldHotelRequestDetailID.Value = "0";



                //if (IsExist == true)
                //{
                //    DataTable dt = new DataTable();
                //    SF = new CrewAssistBLL();
                //    SF.SeafarerSaveComapnionRequest(ref dt,
                //        uoHiddenFieldHotelRequestDetailID.Value,
                //        uoHiddenFieldHotelRequestID.Value,
                //        uoTextBoxCompLastname.Text,
                //        uoTextBoxCompFirstname.Text,
                //        uoTextBoxCompRelationship.Text,
                //        uoDropDownListCompGender.SelectedValue,
                //        uoHiddenFieldUser.Value,
                //         GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                //         GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                //         GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)
                //        );
                //    uoCheckBoxAddCompanion.Checked = true;
                //    uoTextBoxCompFirstname.Text = "";
                //    uoTextBoxCompLastname.Text = "";
                //    uoTextBoxCompRelationship.Text = "";
                //    uoDropDownListCompGender.SelectedIndex = 0;
                //    uoHiddenFieldHotelRequestDetailID.Value = "0";

                //    uoListViewCompanionList.DataSource = null;
                //    uoListViewCompanionList.DataBind();
                //    uoListViewCompanionList.DataSource = dt;
                //    uoListViewCompanionList.DataBind();

                //}
                //else
                //{ 


                //}



            }
            catch (Exception ex)
            {
                AlertMessage("AddHotelRequestCompanion: " + ex.Message);
            }
        }

        /// ----------------------------------------------
        /// Modified By:    Muhallidin G Wali
        /// Date Modified:  08/06/2013
        /// Description:    Bind Request Companion
        /// ----------------------------------------------
        /// </summary>
        private void GetSFCompanion()
        {
            DataTable dtCompanion = new DataTable();
            try
            {

                uoListViewCompanionList.DataSource = null;
                uoListViewCompanionList.DataBind();
                dtCompanion = GetSfCompanionDataTable();
                uoListViewCompanionList.DataSource = dtCompanion;
                uoListViewCompanionList.DataBind();

                //if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
                //{ 
                //    uoButtonSendComp.Visible = true; 
                //}
                //else 
                //{ 
                //    uoButtonSendComp.Visible = false;  
                //}

            }
            catch (Exception ex)
            {
                AlertMessage("GetSFCompanion: " + ex.Message);
            }
            finally
            {
                if (dtCompanion != null)
                {
                    dtCompanion.Dispose();
                }
            }
        }

        /// <summary>
        /// Modified By:     Josephine Gad
        /// Date Modified:   25/Nov/2013
        /// description      Dispose DataTable
        /// </summary>
        private DataTable GetSfCompanionDataTable()
        {
            DataTable dtSFInfo = null;
            try
            {
                dtSFInfo = SeafarerBLL.SeafarerGetCompanionDetails(uoHiddenFieldHotelRequestID.Value);
                return dtSFInfo;
            }
            catch (Exception ex)
            {
                AlertMessage("GetSfCompanionDataTable: " + ex.Message);
                return dtSFInfo;
            }
            finally
            {
                if (dtSFInfo != null)
                {
                    dtSFInfo.Dispose();
                }
            }
        }

        //protected void uoCheckBoxAddCompanion_OnCheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //GetSFCompanion();

        //        uoTableCompanion.Visible = uoCheckBoxAddCompanion.Checked; 

        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMessage("CheckBoxAddCompanion: " + ex.Message);
        //    }
        //}


        protected void uoCheckBoxsHotelTransaction_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {

                CheckBox obj = (CheckBox)sender;

                if (obj.Checked == false) return;

                List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
                cres = (List<CrewAssistTransaction>)Session["CrewAssistTransaction"];

                if (cres.Count > 0 && cres != null)
                {

                    //LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking.Where(a => a.TransHotelID == GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value)).ToList());

                    List<CrewAssistHotelBooking> CrewAssist = new List<CrewAssistHotelBooking>();
                    CrewAssist = cres[0].CrewAssistHotelBooking.Where(a => a.TransHotelID == GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value)).ToList();

                    //LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                    //uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                    //uoListViewTransportation.DataBind();

                    if (CrewAssist.Count > 0 && CrewAssist != null)
                    {
                        uoHiddenFieldTransVendorID.Value = CrewAssist[0].TransVehicleIDBigint.ToString();

                        uoHiddenFieldTransHotelOtherID.Value = CrewAssist[0].TransHotelID.ToString();
                        uoHiddenFieldHotelRequestID.Value = CrewAssist[0].RequestID.ToString();

                        uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);

                        if (uoDropDownListHotel.SelectedIndex <= 0)
                        {
                            uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                            uoDropDownListHotel.Items.Insert(1, new ListItem((CrewAssist[0].IsPortAgent.ToString() == "True" ? "PA - " : "HT - ") + CrewAssist[0].Branch.ToString(), CrewAssist[0].HotelIDInt.ToString()));
                            uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);
                        }

                        uoTextBoxCheckinDate.Text = CrewAssist[0].CheckinDate.ToString();
                        uoTextBoxCheckoutDate.Text = CrewAssist[0].CheckoutDate.ToString();

                        DateTime oldDate = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text);
                        DateTime newDate = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text);
                        TimeSpan ts = newDate - oldDate;

                        uoTextBoxDuration.Text = CrewAssist[0].NoNitesInt.ToString();

                        uoDropDownListRoomeType.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListRoomeType, CrewAssist[0].RoomTypeID);

                        uoCheckboxBreakfast.Checked = CrewAssist[0].MealBreakfastBit;
                        uoCheckboxLunch.Checked = CrewAssist[0].MealLunchBit;
                        uoCheckboxDinner.Checked = CrewAssist[0].MealDinnerBit;
                        uoCheckBoxLunchDinner.Checked = CrewAssist[0].MealLunchDinnerBit;
                        uoCheckBoxIsWithShuttle.Checked = CrewAssist[0].WithShuttleBit;
                        DateTime CheckInTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeIn);
                        string CInTime = String.Format("{0:HH:mm}", CheckInTime);
                        uoTxtBoxTimeIn.Text = CInTime;

                        DateTime CheckOutTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeOut);
                        string COutTime = String.Format("{0:HH:mm}", CheckOutTime);
                        uoTxtBoxTimeOut.Text = COutTime;

                        Decimal RoomTax = GlobalCode.Field2Decimal(CrewAssist[0].ContractedRateMoney);
                        Decimal fAmount = GlobalCode.Field2Decimal(CrewAssist[0].ConfirmRateMoney);

                        uoTextContractedRate.Text = RoomTax.ToString("N2");

                        uoTextBoxComfirmRate.Text = fAmount.ToString("N2");
                        uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, CrewAssist[0].Currency);

                        Decimal MealVoucher = GlobalCode.Field2Decimal(CrewAssist[0].MealVoucherMoney);
                        uoTextBoxMealVoucher.Text = MealVoucher.ToString("N2");

                        uoTextBoxEmail.Text = CrewAssist[0].HotelEmail;
                        CheckBoxEmail.Checked = true;

                        uoTextBoxComment.Text = CrewAssist[0].HotelComment;

                        uoHiddenFieldHSourceRequest.Value = CrewAssist[0].RequestSourceID.ToString();

                        uoTextBoxPortAgentConfirm.Text = CrewAssist[0].ConfirmBy;

                        uoHiddenFieldPAAirportHotel.Value = CrewAssist[0].IsAutoAirportToHotel.ToString();
                        uoHiddenFieldPAHotelShip.Value = CrewAssist[0].IsAutoHotelToShip.ToString();

                        uoTextBoxReasonCode.Text = CrewAssist[0].ReasonCode;

                        uoHotelListViewRemark.DataSource = CrewAssist[0].HotelRemark;
                        uoHotelListViewRemark.DataBind();
                        uoCheckBoxMedical.Checked = GlobalCode.Field2Bool(CrewAssist[0].IsMedical);

                        uoListViewCompanionList.DataSource = null;
                        uoListViewCompanionList.DataBind();

                        List<HotelRequestCompanion> CrewAssistCom = new List<HotelRequestCompanion>();
                        CrewAssistCom = cres[0].HotelRequestCompanion.Where(a => a.REQUESTID == GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) && a.IsPortAgent == GlobalCode.Field2Bool(CrewAssist[0].IsPortAgent)).ToList();

                        uoListViewCompanionList.DataSource = CrewAssistCom;
                        uoListViewCompanionList.DataBind();

                        uoHiddenFieldContractStart.Value = CrewAssist[0].ContractDateStarted;
                        uoHiddenFieldContractEnd.Value = CrewAssist[0].ContractDateEnd;


                        //EnableHotelControl( CrewAssist[0].StatusID == 4 ? false : true);

                        EnableHotelControl(true);
                    }

                }

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }


        private string AddCheckHotel()
        {
            try
            {

                SF = new CrewAssistBLL();
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                Session["CrewAssistRequestID"] = null;
                Session["HotelEmailTo"] = uoTextBoxEmail.Text.ToString();
                uoHiddenFieldSaveHotel.Value = "0";

                bool IsAir = false;

                if (GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value) > 0)
                {
                    IsAir = true;
                }

                return SF.SeafarerSaveRequestHotelOverflow(uoHiddenFieldHotelRequestID.Value
                       , uoTextBoxEmployeeID.Text
                       , uoTextBoxLastName.Text
                       , uoTextBoxFirstName.Text
                       , uoHiddenFieldGenderID.Value
                      , "0"
                       , IsAir == false ? uoDropDownListPort.SelectedValue : "0"
                       , IsAir == true ? uoDropDownListPort.SelectedValue : "0"
                       , uoDropDownListHotel.SelectedValue
                       , uoTextBoxCheckinDate.Text
                       , uoTextBoxCheckoutDate.Text
                       , GlobalCode.GetdateDiff(GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text), GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text)).ToString()
                       , uoDropDownListRoomeType.SelectedValue
                       , uoCheckboxBreakfast.Checked
                       , uoCheckboxLunch.Checked
                       , uoCheckboxDinner.Checked
                       , uoCheckBoxLunchDinner.Checked
                       , uoCheckBoxIsWithShuttle.Checked
                       , GlobalCode.Field2Int(uoHiddenFieldRank.Value).ToString()
                       , GlobalCode.Field2Int(uoHiddenFieldVesselID.Value).ToString()
                       , GlobalCode.Field2Int(uoHiddenFieldCostCenterID.Value).ToString()
                       , uoTextBoxComment.Text.ToString() // remark
                       , uoTextBoxStatus.Text
                       , uoTxtBoxTimeIn.Text
                       , uoTxtBoxTimeOut.Text
                       , uoTextContractedRate.Text
                       , uoCheckContractBoxTaxInclusive.Checked
                       , uoTextBoxComfirmRate.Text
                       , uoHiddenFieldUser.Value
                       , uoHiddenFieldTravelRequestID.Value
                       , uoDropDownListCurrency.SelectedValue
                       , ""
                       , "SaveRequest"
                       , Path.GetFileName(Request.UrlReferrer.AbsolutePath)
                       , CommonFunctions.GetDateTimeGMT(dateNow)
                       , dateNow
                       , IsAir
                       , GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)
                       , GlobalCode.Field2Int(uoHiddenFieldIDBigint.Value)
                       , GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue)
                       , GlobalCode.Field2Double(uoTextBoxMealVoucher.Text)
                       , GlobalCode.Field2Double(uoTextContractedRate.Text)
                       , GlobalCode.Field2Double(uoTextBoxComfirmRate.Text)
                       , uoTextBoxEmail.Text.ToString()
                       , uoHiddenFieldCityCode.Value);




            }
            catch
            {
                return "";
            }

        }


        /// <summary>
        /// ===============================================
        /// Author:       Muhallidin G Wali
        /// Date Created: 08/06/2013
        /// Description:  Insert Request
        /// ===============================================      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonFinish_click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value) == 0)
                {
                    if (uoListviewAir.Items.Count > 0)
                    {
                        //System.Windows.Forms.MessageBox.Show("Select a Crew schedule or Air detail! ", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        AlertMessage("Select a Crew schedule or Air detail! ");
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("Select a Crew schedule! ", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        AlertMessage("Select a Crew schedule! ");
                    }
                    return;
                }

                SF = new CrewAssistBLL();
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                Session["CrewAssistRequestID"] = null;
                Session["HotelEmailTo"] = uoTextBoxEmail.Text.ToString();

                string[] separators = { "-" };

                if (uoTextBoxEmployeeID.Text.ToString() != "")
                {

                    bool? IsAir = false;
                    if (GlobalCode.Field2String(uoHiddenFieldSaveType.Value) == "true")
                    {
                        IsAir = true;
                    }
                    else
                    {
                        uoHiddenFieldSeqNo.Value = uoHiddenFieldTRSequenceNo.Value;
                    }

                    if (uoHiddenFieldSaveHotel.Value == "1")
                    {
                        SaveHotelTransaction(IsAir);
                    }

                    if (uoHiddenFieldSaveTrans.Value == "1")
                    {
                        SaveVehicleTransaction(IsAir);

                    }

                    if (GlobalCode.Field2Long(uoDropDownListMAndGVendor.SelectedValue) > 0 &&
                        GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) == 0 && IsAir == true)
                    {
                        SaveMeetAndGreet(IsAir);
                    }

                    if (uoHiddenFieldSavePA.Value == "1")
                    {
                        SavePortAgent(IsAir);
                    }

                    if (GlobalCode.Field2Long(uoDropDownListSafeguard.SelectedValue) > 0 &&
                         GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) == 0)
                    {
                        SaveSafeGuard(IsAir);
                    }

                }
                else
                {

                    if (GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue) <= 0)
                    {
                        ExceptionMessage("Vendor branch required");
                    }
                    else if (uoTextBoxEmployeeID.Text.ToString() != "")
                    {
                        ExceptionMessage("Enter Staff");
                    }
                    else if (GlobalCode.Field2Int(GlobalCode.GetdateDiff(GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text), GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text)).ToString()) < 0)
                    {
                        ExceptionMessage("Checkout date must be greater than checkin date");
                    }
                    HiddenFieldHideCenter.Value = "0";

                }

            }
            catch (Exception ex)
            {
                AlertMessage("ButtonFinish: " + ex.Message);
            }
        }



        //protected void uoButtonApprovedHotel_click(object sender, EventArgs e)
        void AddApproveHotel()
        {

            try
            {

                if (GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value) == 0)
                {
                    if (uoListviewAir.Items.Count > 0)
                    {
                        //System.Windows.Forms.MessageBox.Show("Select a Crew schedule or Air detail! ", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        AlertMessage("Select a Crew schedule or Air detail! ");
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show("Select a Crew schedule! ", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        AlertMessage("Select a Crew schedule! ");
                    }
                    return;
                }

                SF = new CrewAssistBLL();
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                Session["CrewAssistRequestID"] = null;
                Session["HotelEmailTo"] = uoTextBoxEmail.Text.ToString();

                string[] separators = { "-" };

                if (uoTextBoxEmployeeID.Text.ToString() != "")
                {

                    bool? IsAir = false;
                    if (GlobalCode.Field2String(uoHiddenFieldSaveType.Value) == "true")
                    {
                        IsAir = true;
                    }
                    else
                    {
                        uoHiddenFieldSeqNo.Value = uoHiddenFieldTRSequenceNo.Value;
                    }

                    if (uoHiddenFieldSaveHotel.Value == "1")
                    {

                        SaveHotelTransaction(IsAir);

                    }

                    if (uoHiddenFieldSaveTrans.Value == "1")
                    {
                        SaveVehicleTransaction(IsAir);

                    }

                    if (GlobalCode.Field2Long(uoDropDownListMAndGVendor.SelectedValue) > 0 &&
                        GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) == 0 && IsAir == true)
                    {
                        SaveMeetAndGreet(IsAir);
                    }

                    if (uoHiddenFieldSavePA.Value == "1")
                    {
                        SavePortAgent(IsAir);
                    }

                    if (GlobalCode.Field2Long(uoDropDownListSafeguard.SelectedValue) > 0 &&
                         GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) == 0)
                    {
                        SaveSafeGuard(IsAir);
                    }
                    //refreshPage();
                }
                else
                {

                    if (GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue) <= 0)
                    {
                        ExceptionMessage("Vendor branch required");
                    }
                    else if (uoTextBoxEmployeeID.Text.ToString() != "")
                    {
                        ExceptionMessage("Enter Staff");
                    }
                    else if (GlobalCode.Field2Int(GlobalCode.GetdateDiff(GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text), GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text)).ToString()) < 0)
                    {
                        ExceptionMessage("Checkout date must be greater than checkin date");
                    }
                    HiddenFieldHideCenter.Value = "0";

                }
                //refreshPage();
            }
            catch (Exception ex)
            {
                AlertMessage("ButtonFinish: " + ex.Message);
            }

        }





        void SaveHotelTransaction(bool? IsAir)
        {
            try
            {
                string[] separators = { "-" };

                SF = new CrewAssistBLL();
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //if (uoTextBoxComment.Text.ToString() == "")
                //{
                //    ExceptionMessage("Comment Required \n and Request source...");
                //    return;
                //}


                //if (GlobalCode.Field2TinyInt(uoHiddenFieldHSourceRequest.Value) == 0)
                //{
                //    ExceptionMessage("Request source Required!!! \n double click comment to select the request source...");
                //    //ScriptManager.RegisterStartupScript(Page, GetType(), "key", "OverflowCofirmation();", true);
                //    return;
                //}

                ListView lst = (ListView)uoListViewCompanionList;
                List<HotelRequestCompanion> HRC = new List<HotelRequestCompanion>();


                if (uoCheckBoxAddCompanion.Checked == true)
                {
                    if (lst.Items.Count > 0)
                    {

                        foreach (ListViewItem ls in lst.Items)
                        {
                            HRC.Add(new HotelRequestCompanion
                            {


                                FIRSTNAME = ((Label)ls.FindControl("uoLabelFirstname")).Text.ToString(),
                                LASTNAME = ((Label)ls.FindControl("uoLabelLastname")).Text.ToString(),
                                RELATIONSHIP = ((Label)ls.FindControl("uoLabelRelationship")).Text.ToString(),
                                GENDER = ((Label)ls.FindControl("uoLabelGender")).Text.ToString(),
                                GENDERID = GlobalCode.Field2Int(((HiddenField)ls.FindControl("uoHiddenFieldGENDERID")).Value),
                                REQUESTID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldComReqID")).Value),
                                DETAILID = GlobalCode.Field2Int(((HiddenField)ls.FindControl("uoHiddenFieldComReqDetID")).Value),

                                TRAVELREQID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                                IDBIGINT = GlobalCode.Field2Int(uoHiddenFieldIDBigint.Value),
                                SEQNO = GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value),

                                IsPortAgent = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("uoHiddenFieldPA")).Value),
                                IsMedical = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("uoHiddenFieldComIsMedical")).Value),

                            });
                        }
                    }
                }

                if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -")
                {
                    //if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0
                    //    && uoTextBoxCheckinDate.Text.ToString() != ""
                    //    && uoTextBoxCheckoutDate.Text.ToString() != ""
                    //    && ((GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) == 0 || uoCheckBoxMedical.Checked == true) || GlobalCode.Field2Long( uoHiddenFieldHotelRequestID.Value) > 0  )
                    //    )
                    if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0
                         && uoTextBoxCheckinDate.Text.ToString() != ""
                         && uoTextBoxCheckoutDate.Text.ToString() != ""
                        //&& ((GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) == 0 || uoCheckBoxMedical.Checked == true) || GlobalCode.Field2Long( uoHiddenFieldHotelRequestID.Value) > 0  )
                         )
                    {


                        string recipient = "";
                        string EmailAdd = uoTextBoxEmail.Text.ToString();

                        string[] Eseparators = { ",", ";", " " };

                        string[] words = EmailAdd.Split(Eseparators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            recipient += word + "; ";
                        }

                        string cssEmail = "";

                        if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                        {
                            cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                        }

                        if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                        {
                            cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                        }

                        if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                        {
                            cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                        }
                        if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                        {
                            cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                        }
                        if (uoTextBoxEmailOther.Text != "")
                        {
                            cssEmail += uoTextBoxEmailOther.Text.ToString();
                        }

                        string ccrecipient = "";
                        string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in ccmail)
                        {

                            ccrecipient += word + "; ";
                        }

                        if (ccrecipient != "")
                        {
                            ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                        }

                        string sHRID = SF.SeafarerSaveRequest(uoHiddenFieldHotelRequestID.Value
                                       , uoTextBoxEmployeeID.Text
                                       , uoTextBoxLastName.Text
                                       , uoTextBoxFirstName.Text
                                       , uoHiddenFieldGenderID.Value
                                       , "0"
                                       , IsAir == false ? uoDropDownListPort.SelectedValue : "0"
                                       , IsAir == true ? uoDropDownListPort.SelectedValue : "0"
                                       , uoDropDownListHotel.SelectedValue
                                       , uoTextBoxCheckinDate.Text
                                       , uoTextBoxCheckoutDate.Text
                                       , GlobalCode.GetdateDiff(GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text), GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text)).ToString()
                                       , uoDropDownListRoomeType.SelectedValue
                                       , uoCheckboxBreakfast.Checked
                                       , uoCheckboxLunch.Checked
                                       , uoCheckboxDinner.Checked
                                       , uoCheckBoxLunchDinner.Checked
                                       , uoCheckBoxIsWithShuttle.Checked
                                       , GlobalCode.Field2Int(uoHiddenFieldRank.Value).ToString()
                                       , GlobalCode.Field2Int(uoHiddenFieldVesselID.Value).ToString()
                                       , GlobalCode.Field2Int(uoHiddenFieldCostCenterID.Value).ToString()
                                       , uoTextBoxComment.Text.ToString() // remark
                                       , uoTextBoxStatus.Text
                                       , uoTxtBoxTimeIn.Text
                                       , uoTxtBoxTimeOut.Text
                                       , uoTextContractedRate.Text
                                       , uoCheckContractBoxTaxInclusive.Checked
                                       , uoTextBoxComfirmRate.Text
                                       , uoHiddenFieldUser.Value
                                       , uoHiddenFieldTravelRequestID.Value
                                       , uoDropDownListCurrency.SelectedValue
                                       , ""
                                       , "SaveRequest"
                                       , Path.GetFileName(Request.UrlReferrer.AbsolutePath)
                                       , CommonFunctions.GetDateTimeGMT(dateNow)
                                       , dateNow
                                       , IsAir == true ? true : false
                                       , GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)
                                       , GlobalCode.Field2Int(uoHiddenFieldIDBigint.Value)
                                       , GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue)
                                       , GlobalCode.Field2Double(uoTextBoxMealVoucher.Text)
                                       , GlobalCode.Field2Double(uoTextBoxComfirmRate.Text)
                                       , GlobalCode.Field2Double(uoTextContractedRate.Text)
                                       , uoTextBoxEmail.Text.ToString()
                                       , uoHiddenFieldCityCode.Value
                                       , GlobalCode.Field2TinyInt(uoHiddenFieldHSourceRequest.Value)

                                       , uoHiddenFieldHotelConfirmBy.Value
                                       , ""
                                       , recipient.Substring(0, recipient.Length - 2).ToString()
                                       , ccrecipient
                                       , ""
                                       , GlobalCode.Field2Double(uoTextBoxComfirmRate.Text)
                                       , uoCheckBoxMedical.Checked
                                       , GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value)
                                       , HRC);

                        if (GlobalCode.Field2Long(sHRID) > 0)
                        {
                            uoHiddenFieldHotelRequestID.Value = sHRID;
                            if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
                            {
                                HiddenFieldHideCenter.Value = "1";
                            }
                        }
                        else
                        {

                            string[] res = sHRID.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                            string mes = "";
                            if (res.Length > 1)
                            {
                                mes = res[1].ToString();
                            }
                            else if (res.Length == 1)
                            {
                                mes = res[0].ToString();
                            }

                            if (mes == "is in over flow list of hotel" || mes == "Exist in the same day")
                            {

                                string sMsg1 = uoTextBoxLastName.Text + ' ' + uoTextBoxFirstName.Text + ' ' + mes + ' ' +
                                              "\ndo u want to add hotel request in " + uoDropDownListHotel.Text;

                                uoHiddenFieldOverflowMessage.Value = uoTextBoxLastName.Text + ' ' + uoTextBoxFirstName.Text + ' ' + mes + ' ' +
                                                                     res[0].ToString() + "\ndo u want to add hotel request in " + uoDropDownListHotel.Text + "?";

                                HiddenFieldHideCenter.Value = "0";

                                //ScriptManager.RegisterStartupScript(Page, GetType(), "key", "OverflowCofirmation();", true);

                            }
                            else if (mes == "Approve Request")
                            {
                                HiddenFieldHideCenter.Value = "0";
                            }

                            else
                            {

                                AlertMessage(sHRID.ToString());
                                HiddenFieldHideCenter.Value = "0";
                            }
                        }


                    }
                    else
                    {
                        HiddenFieldHideCenter.Value = "0";
                    }

                }

                else if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
                {
                    HiddenFieldHideCenter.Value = "0";

                    if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0
                        && uoTextBoxCheckinDate.Text.ToString() != ""
                        && uoTextBoxCheckoutDate.Text.ToString() != ""
                        )
                    {


                        uoHiddenFieldHotelRequestID.Value = SF.SavePortAgentHotelRequest(GetHotelTranPortAgent(IsAir == true ? true : false), GlobalCode.Field2String(uoHiddenFieldUser.Value), HRC);
                        if (uoHiddenFieldHotelRequestID.Value == "Crew has already a port agent request")
                        {
                            AlertMessage(uoHiddenFieldHotelRequestID.Value.ToString());
                            uoHiddenFieldHotelRequestID.Value = "0";
                        }

                        uoHiddenFieldSaveHotel.Value = "0";

                        if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "key", "PortAgentHotelCofirmation();", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessage("SaveHotelTransaction: " + ex.Message);
            }
        }

        void SaveVehicleTransaction(bool? IsAir)
        {
            try
            {
                string FromTrans = GetPortCode(); ;
                string ToTrans = GetPortCode(); ;
                string[] separators = { "-" };


                SF = new CrewAssistBLL();

                if (uoTextBoxTranpComfirmby.Text.ToString() == "")
                {

                    ExceptionMessage("Required Transportation! \n Confirm By");

                }


                if (uoTextBoxTransComment.Text.ToString() == "")
                {
                    ExceptionMessage("Comment Required \n and Request source...");
                    return;
                }


                if (GlobalCode.Field2TinyInt(uoHiddenFieldTSourceRequest.Value) == 0)
                {
                    ExceptionMessage("Request source Required!!! \n double click transpo. comment to select the request source...");
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "key", "OverflowCofirmation();", true);
                    return;
                }

                if (GlobalCode.Field2TinyInt(uoHiddenFieldTSourceRequest.Value) == 0)
                {
                    ExceptionMessage("Request source Required!!! \n double click transpo. comment to select the request source...");
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "key", "OverflowCofirmation();", true);
                    return;
                }


                bool TransSave = true;
                //if (GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0)
                //{
                //    TransSave = GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) == 0 ? true : false;
                //}


                string Hotelname = "";

                HiddenField HIdBigint;
                HiddenField HTravelReqIDInt;
                HiddenField HSeqNoInt;
                Label lblHotelBook;


                foreach (ListViewItem item in uoListViewHotelBook.Items)
                {


                    HIdBigint = (HiddenField)item.FindControl("uoHiddenFieldHIDBigint");
                    HSeqNoInt = (HiddenField)item.FindControl("uoHiddenFieldSeqNo");
                    HTravelReqIDInt = (HiddenField)item.FindControl("uoHiddenFieldHTravelRequestID");
                    lblHotelBook = (Label)item.FindControl("lblHotelBook");
                    if (HIdBigint.Value == uoHiddenFieldIDBigint.Value && HSeqNoInt.Value == uoHiddenFieldSeqNo.Value &&
                        HTravelReqIDInt.Value == uoHiddenFieldTravelRequestID.Value) Hotelname = lblHotelBook.Text.ToString();
                }





                List<CrewAssistTranspo> Transpo = new List<CrewAssistTranspo>();
                if (GlobalCode.Field2Long(uoDropDownListVehicleVendor.SelectedValue) > 0 && TransSave == true)
                {

                    ListView ListViewControl = uoListViewTranportationRoute;
                    List<VehicleTransactionPortAgent> list = new List<VehicleTransactionPortAgent>();

                    HiddenField FromID;
                    HiddenField ToID;
                    HiddenField TransVendorID;
                    HiddenField ReqTransVendorID;
                    Label RouteFrom;
                    Label RouteTo;
                    Label PickUpDate;
                    Label PickUpTime;
                    Label TransCost;

                    foreach (ListViewItem item in ListViewControl.Items)
                    {

                        TransVendorID = (HiddenField)item.FindControl("uoHiddenFieldTransID");
                        ReqTransVendorID = (HiddenField)item.FindControl("uoHiddenFieldReqTransID");



                        FromID = (HiddenField)item.FindControl("HiddenFieldRouteIDFromInt");
                        ToID = (HiddenField)item.FindControl("HiddenFieldRouteIDToInt");

                        RouteFrom = (Label)item.FindControl("uoLabelRouteFrom");
                        RouteTo = (Label)item.FindControl("uoLabelRouteTo");

                        //if (FromID.Value == "2")
                        //{


                        //}


                        PickUpDate = (Label)item.FindControl("uoLabelPickUpDate");
                        PickUpTime = (Label)item.FindControl("uoLabelPickUpTime");
                        TransCost = (Label)item.FindControl("uoLabelCost");

                        Transpo.Add(new CrewAssistTranspo
                        {
                            ReqVehicleIDBigint = GlobalCode.Field2Long(ReqTransVendorID.Value),
                            VehicleTransID = GlobalCode.Field2Long(TransVendorID.Value),

                            IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                            TravelReqIDInt = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                            SeqNoInt = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
                            RecordLocatorVarchar = uoHiddenFieldRecordLocator.Value,
                            VehicleVendorIDInt = GlobalCode.Field2Long(uoDropDownListVehicleVendor.SelectedValue),
                            VehiclePlateNoVarchar = GlobalCode.Field2String(""),
                            PickUpDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                            PickUpTime = GlobalCode.Field2Time(PickUpTime.Text),
                            DropOffDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                            DropOffTime = GlobalCode.Field2Time(PickUpTime.Text),
                            ConfirmationNoVarchar = GlobalCode.Field2String(""),
                            VehicleStatusVarchar = GlobalCode.Field2String(""),
                            VehicleTypeIdInt = GlobalCode.Field2Int(uoHiddenFieldVTypeID.Value),
                            SFStatus = GlobalCode.Field2String(uoTextBoxStatus.Text),
                            RouteIDFromInt = GlobalCode.Field2Int(FromID.Value),
                            RouteIDToInt = GlobalCode.Field2Int(ToID.Value),

                            FromVarchar = FromTrans,
                            ToVarchar = ToTrans,

                            UserID = GlobalCode.Field2String(uoHiddenFieldUser.Value),
                            Comment = GlobalCode.Field2String(uoTextBoxTransComment.Text),

                            RouteFrom = GlobalCode.Field2String(RouteFrom.Text),
                            RouteTo = GlobalCode.Field2String(RouteTo.Text),

                            IsAir = IsAir,
                            HotelID = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue),
                            ConfirmBy = uoTextBoxTranpComfirmby.Text.ToString(),
                            IsPortAgent = uoDropDownListVehicleVendor.SelectedItem.Text.Substring(0, 2) == "VH" ? false : true,
                            ConfirmRate = GlobalCode.Field2Double(TransCost.Text.ToString()),
                            ReqSourceID = GlobalCode.Field2TinyInt(uoHiddenFieldTSourceRequest.Value),
                            Email = GlobalCode.Field2String(uoTextBoxEmailTrans.Text),
                            //IsMedical = GlobalCode.Field2Bool(uoCheckBoxMedical.Checked),
                        });
                    }
                }

                string sVRID = "";
                if (Transpo.Count > 0)
                {

                    sVRID = SF.SaveTransportationeReques(Transpo);

                    if (GlobalCode.Field2Long(sVRID) > 0)
                    {
                        uoHiddenFieldTransVendorID.Value = sVRID;
                    }
                    else
                    {
                        string[] res = sVRID.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        string mes = "";

                        if (res.Length > 1)
                        {
                            mes = res[1].ToString();
                        }

                        if (mes == "Crew has vehicle booking already")
                        {
                            string sMsg = uoTextBoxLastName.Text + " " + uoTextBoxFirstName.Text + " " + mes + " " +
                                     "\ndo u want to add vehicle request in " + uoDropDownListHotel.Text + "?";
                            ExceptionMessage(sMsg);
                            uoHiddenFieldSaveTrans.Value = "0";
                        }
                        else
                        {
                            AlertMessage(sVRID);
                        }
                    }
                    if (GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "key", "TransportationCofirmation();", true);
                    }
                    uoHiddenFieldSaveTrans.Value = "0";
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SaveVehicleTransaction: " + ex.Message);
            }

        }

        void SaveMeetAndGreet(bool? IsAir)
        {
            try
            {
                List<CrewAssistMeetAndGreet> MGreet = new List<CrewAssistMeetAndGreet>();
                MGreet.Add(new CrewAssistMeetAndGreet
                {
                    ReqMeetAndGreetID = GlobalCode.Field2Long(uoHiddenFieldMeetAndGreetID.Value),
                    IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                    TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                    SeqNo = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
                    RecordLocator = GlobalCode.Field2String(""),
                    MeetAndGreetVendorID = GlobalCode.Field2Int(uoDropDownListMAndGVendor.SelectedValue),
                    MeetAndGreetVendor = GlobalCode.Field2String(uoDropDownListMAndGVendor.Text),
                    ConfirmationNo = GlobalCode.Field2String(""),
                    MeetAndGreetStatus = "Open",
                    AirportCode = uoTextBoxAirlineCode.Text,

                    ArrTime = GlobalCode.Field2DateTime(uoHiddenFieldArrivalTime.Value),
                    DeptTime = GlobalCode.Field2DateTime(uoHiddenFieldDepartureTime.Value),

                    AirportID = GlobalCode.Field2Int(uoDropDownListMAndGAirport.SelectedValue),
                    FligthNo = uoTextBoxMAndGFligthInfo.Text.ToString(),
                    ServiceDate = GlobalCode.Field2DateTime(uoTextBoxMAndGServiceDate.Text),
                    Rate = GlobalCode.Field2Double(uoTextBoxMAndGRate.Text),
                    SFStatus = GlobalCode.Field2String(uoTextBoxStatus.Text),
                    Comment = GlobalCode.Field2String(uoTextBoxMAGComment.Text),
                    UserID = GlobalCode.Field2String(uoHiddenFieldUser.Value),
                    IsAir = IsAir,
                    ConfirmBy = uoTextBoxMAGConfirm.Text.ToString()
                });

                if (MGreet.Count > 0) uoHiddenFieldMeetAndGreetID.Value = SF.SaveMeetAndGreetRequest(MGreet);


                if (GlobalCode.Field2Long(uoHiddenFieldMeetAndGreetID.Value) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "key", "MeetAndGreetCofirmation();", true);
                }


            }
            catch (Exception ex)
            {
                AlertMessage("SaveMeetAndGreet: " + ex.Message);
            }
        }

        void SavePortAgent(bool? IsAir)
        {
            try
            {
                // Hotel Port Agent
                if (GlobalCode.Field2Long(uoDropDownListPortAgent.SelectedValue) > 0 &&
                    //GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0 &&
                uoHiddenFieldSavePA.Value == "1")
                {

                    if (
                        uoCheckBoxPAMAG.Checked == false && uoCheckBoxPATrans.Checked == false &&
                        uoCheckBoxPAHotel.Checked == false && uoCheckBoxPALuggage.Checked == false &&
                        uoCheckBoxPALuggage.Checked == false && uoCheckBoxPASafeguard.Checked == false &&
                        uoCheckBoxPAVisa.Checked == false && uoCheckBoxPAOther.Checked == false &&
                        GlobalCode.Field2Long(uoHiddenFieldPortAgentID.Value) == 0
                        )
                    {
                        AlertMessage("Select Services for Service Provider!");
                    }
                    else
                    {

                        List<CrewAssistPortAgentRequest> PAgent = new List<CrewAssistPortAgentRequest>();
                        PAgent.Add(new CrewAssistPortAgentRequest
                        {
                            ReqPortAgentID = GlobalCode.Field2Long(uoHiddenFieldPortAgentID.Value),
                            IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                            TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                            SeqNo = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
                            RecordLocator = "",
                            PortAgentVendorID = GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue),
                            PortCodeID = GlobalCode.Field2Int(uoDropDownListPort.SelectedValue),
                            PortCode = GlobalCode.Field2String(uoTextBoxPAPort.Text),
                            AirportID = 0,
                            AirportCode = GetPortCode(),
                            FligthNo = GlobalCode.Field2String(uoHiddenFieldFligthNo.Value),
                            ServiceDatetime = GlobalCode.Field2DateTime(uoTextBoxPARequestDate.Text),
                            SFStatus = GlobalCode.Field2String(uoTextBoxStatus.Text),
                            Comment = GlobalCode.Field2String(uoTextBoxPAComment.Text),
                            IsMAG = uoCheckBoxPAMAG.Checked,
                            IsTrans = uoCheckBoxPATrans.Checked,
                            IsHotel = uoCheckBoxPAHotel.Checked,
                            IsLuggage = uoCheckBoxPALuggage.Checked,
                            IsSafeguard = uoCheckBoxPASafeguard.Checked,
                            IsVisa = uoCheckBoxPAVisa.Checked,
                            IsOther = uoCheckBoxPAOther.Checked,
                            UserID = GlobalCode.Field2String(uoHiddenFieldUser.Value),
                            IsAir = IsAir,
                            ConfirmBy = GlobalCode.Field2String(uoTextBoxPAConfirm.Text),
                            IsPhoneCard = uoCheckBoxPhoneCards.Checked,
                            PhoneCard = GlobalCode.Field2Double(uoTextBoxPhonecards.Text),
                            IsLaundry = uoCheckBoxLaundry.Checked,
                            Laundry = GlobalCode.Field2Double(uoTextBoxLaundry.Text),
                            IsGiftCard = uoCheckBoxGiftCard.Checked,
                            GiftCard = GlobalCode.Field2Double(uoTextBoxGiftCard.Text),



                        });
                        if (PAgent.Count > 0)
                        {
                            uoHiddenFieldPortAgentID.Value = SF.SavePortAgentRequest(PAgent, GetVehicleTranPortAgent(), GetHotelTranPortAgent(IsAir));
                            if (uoHiddenFieldPortAgentID.Value == "Crew has already a port agent request")
                            {
                                AlertMessage(uoHiddenFieldPortAgentID.Value.ToString());
                                uoHiddenFieldPortAgentID.Value = "0";
                            }

                            uoHiddenFieldSavePA.Value = "0";

                            if (GlobalCode.Field2Long(uoHiddenFieldPortAgentID.Value) > 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "key", "PortAgentCofirmation();", true);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SaveHotelTransaction: " + ex.Message);
            }

        }

        void SaveSafeGuard(bool? IsAir)
        {
            try
            {
                List<CrewAssistSafeguardRequest> SafeguardReq = new List<CrewAssistSafeguardRequest>();

                SafeguardReq.Add(new CrewAssistSafeguardRequest
                {

                    ReqSafeguardID = GlobalCode.Field2Long(uoHiddenFieldSafeguardRequestID.Value),

                    IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                    TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                    SeqNo = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
                    RecordLocator = GlobalCode.Field2String(""),

                    SafeguardVendorID = GlobalCode.Field2Int(uoDropDownListSafeguard.SelectedValue),
                    TypeID = GlobalCode.Field2Int(uoDropDownListServiceRender.SelectedValue),

                    ContractId = GlobalCode.Field2Int(uoHiddenFieldSGContractID.Value),
                    ContractServiceTypeID = GlobalCode.Field2Int(uoHiddenFieldSGContSerTypeID.Value),
                    SFStatus = GlobalCode.Field2String(uoTextBoxStatus.Text),
                    Comments = GlobalCode.Field2String(uoTextBoxSafeguardComment.Text),
                    IsAirBit = IsAir == true ? true : false,
                    TransactionDate = GlobalCode.Field2DateTime(uoTextBoxSafeguarDate.Text),
                    TransactionTime = GlobalCode.Field2DateTime(uoTextBoxSafeguarDate.Text + " " + uoTextBoxSafeguardTime.Text),
                    UserID = GlobalCode.Field2String(uoHiddenFieldUser.Value),
                    ConfirmBy = upTextBoxSComfirm.Text.ToString()
                });

                if (SafeguardReq.Count > 0) uoHiddenFieldSafeguardRequestID.Value = SF.SaveSafeguardRequest(SafeguardReq);

                //if (SafeguardReq.Count > 0) uoHiddenFieldSafeguardRequestID.Value = SF.SaveSafeguardRequest(SafeguardReq);

                if (GlobalCode.Field2Long(uoHiddenFieldSafeguardRequestID.Value) > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "key", "SafeguardCofirmation();", true);
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SaveSafeGuard: " + ex.Message);
            }
        }

        private List<VehicleTransactionPortAgent> GetVehicleTranPortAgent()
        {

            //if (uoTextBoxPAPickupdate.Text.ToString() != null &&
            //    uoDropDownListPAFrom.SelectedIndex > 0 && uoDropDownListPATo.SelectedIndex > 0)
            //{
            //    List.Add(new VehicleTransactionPortAgent
            //    {

            //        IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
            //        TravelReqIDInt = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
            //        SeqNo = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
            //        RecordLocator = uoHiddenFieldRecordLocator.Value,
            //        PortAgentVendorID = GlobalCode.Field2Long(GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue)),
            //        VehiclePlateNo = GlobalCode.Field2String(""),
            //        PickUpDate = GlobalCode.Field2DateTime(uoTextBoxPAPickupdate.Text),
            //        PickUpTime = GlobalCode.Field2Time(uoTextBoxPATime.Text),
            //        DropOffDate = GlobalCode.Field2DateTime(uoTextBoxPickupDate.Text),
            //        DropOffTime = GlobalCode.Field2Time(uoTextBoxPATime.Text),
            //        ConfirmationNo = GlobalCode.Field2String(""),
            //        VehicleStatus = GlobalCode.Field2String(""),
            //        VehicleTypeID = GlobalCode.Field2Int(0),
            //        RouteIDFromInt = GlobalCode.Field2Int(uoDropDownListPAFrom.SelectedValue),
            //        RouteIDToInt = GlobalCode.Field2Int(uoDropDownListPATo.SelectedValue),

            //        Comments = GlobalCode.Field2String(uoTextBoxTransComment.Text),
            //        RouteFrom = GlobalCode.Field2String(uoTextBoxPAFrom.Text),
            //        RouteTo = GlobalCode.Field2String(uoTextBoxPATo.Text),
            //        ConfirmBy = uoTextBoxPAConfirm.Text.ToString()

            //    });
            //}

            List<VehicleTransactionPortAgent> List = new List<VehicleTransactionPortAgent>();
            if (uoHiddenFieldShowTable.Value == "")
            {

                //var Grid = uoListviewPATranspoCost;
                HtmlTableRow row = new HtmlTableRow();
                string[] stringSeparators = new string[] { "|" };
                string mytabel = uoHiddenFieldShowTable.Value;
                string[] words = mytabel.Split('|');

                int n = words.Count();
                for (var a = 0; a < n - 1; a++)
                {
                    string[] myWords = words[a].Split('#');
                    List.Add(new VehicleTransactionPortAgent
                    {
                        IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                        TravelReqIDInt = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                        SeqNo = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
                        RecordLocator = uoHiddenFieldRecordLocator.Value,
                        PortAgentVendorID = GlobalCode.Field2Long(GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue)),
                        VehiclePlateNo = GlobalCode.Field2String(""),
                        PickUpDate = GlobalCode.Field2DateTime(myWords[5].ToString().Replace("~", "")),
                        PickUpTime = GlobalCode.Field2Time(myWords[6].ToString().Replace("~", "")),
                        DropOffDate = GlobalCode.Field2DateTime(myWords[5].ToString().Replace("~", "")),
                        DropOffTime = GlobalCode.Field2Time(myWords[6].ToString().Replace("~", "")),
                        ConfirmationNo = GlobalCode.Field2String(""),
                        VehicleStatus = GlobalCode.Field2String(""),
                        VehicleTypeID = GlobalCode.Field2Int(0),
                        RouteIDFromInt = GlobalCode.GetselectedValue(uoDropDownListRouteFrom, myWords[1].ToString().Replace("~", "")),
                        RouteIDToInt = GlobalCode.GetselectedValue(uoDropDownListRouteFrom, myWords[2].ToString().Replace("~", "")),
                        Comments = GlobalCode.Field2String(uoTextBoxTransComment.Text),
                        RouteFrom = myWords[3].ToString().Replace("~", ""),
                        RouteTo = myWords[4].ToString().Replace("~", ""),
                        ConfirmBy = uoTextBoxPAConfirm.Text.ToString()
                    });
                }
            }
            return List;
        }

        private List<HotelTransactionPortAgent> GetHotelTranPortAgent(bool? IsAir)
        {
            List<HotelTransactionPortAgent> List = new List<HotelTransactionPortAgent>();
            if (uoTextBoxCheckinDate.Text.ToString() != "" && uoTextBoxCheckoutDate.Text.ToString() != "" && GlobalCode.Field2Int(uoTextBoxDuration.Text) > 0)
            {
                List.Add(new HotelTransactionPortAgent
                {
                    HotelTransID = GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                    TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                    IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                    RecordLocator = uoHiddenFieldRecordLocator.Value,
                    SeqNo = GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value),
                    PortAgentVendorID = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue),
                    RoomTypeID = GlobalCode.Field2Int(uoDropDownListRoomeType.SelectedValue),
                    ReserveUnderName = "",
                    TimeSpanStartDate = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text),
                    TimeSpanStartTime = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text),
                    TimeSpanEndDate = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text),
                    TimeSpanEndTime = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text),
                    TimeSpanDurationInt = GlobalCode.Field2Int(uoTextBoxDuration.Text),
                    ConfirmationNo = "",
                    HotelStatus = "Open",
                    IsBilledToCrew = false,
                    Breakfast = uoCheckboxBreakfast.Checked,
                    Lunch = uoCheckboxLunch.Checked,
                    Dinner = uoCheckboxDinner.Checked,
                    LunchOrDinner = uoCheckBoxLunchDinner.Checked,
                    BreakfastID = 0,
                    LunchID = 0,
                    DinnerID = 0,
                    LunchOrDinnerID = 0,
                    VoucherAmount = GlobalCode.Field2Double(uoTextBoxMealVoucher.Text),
                    ContractID = 0,
                    ApprovedBy = null,
                    ApprovedDate = null,
                    ContractFrom = null,
                    RemarksForAudit = null,
                    HotelCity = GetPortCode(),
                    RoomCount = GlobalCode.Field2Decimal(GlobalCode.Field2Int(uoDropDownListRoomeType.SelectedValue) == 2 ? 0.5 : 1.0),
                    HotelName = uoDropDownListHotel.SelectedItem.Text,
                    ContractRate = uoTextContractedRate.Text.ToString(),
                    ConfirmRate = uoTextBoxComfirmRate.Text.ToString(),
                    IsAir = IsAir,
                    EmailTo = uoTextBoxEmail.Text.ToString(),
                    Comment = uoTextBoxComment.Text.ToString(),
                    Currency = GlobalCode.Field2Int(uoDropDownListCurrency.SelectedValue),
                    ConfirmBy = uoTextBoxPortAgentConfirm.Text.ToString(),
                    ReqSource = GlobalCode.Field2TinyInt(uoHiddenFieldHSourceRequest.Value),
                    IsMedical = GlobalCode.Field2Bool(uoCheckBoxMedical.Checked)

                });
            }
            return List;
        }

        string GetPortCode()
        {
            return uoTextBoxStatus.Text.ToString() == "ON" ? uoHiddenFieldArrCode.Value == "" ? uoHiddenFieldPortCode.Value : uoHiddenFieldArrCode.Value : uoHiddenFieldPortCode.Value;
        }

        protected void uoButtonOverflow_Click(object sender, EventArgs e)
        {
            try
            {
                if (uoHiddenFieldSaveConfirmation.Value == "Overflow")
                {
                    uoHiddenFieldHotelRequestID.Value = AddCheckHotel();
                    if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
                    {
                        HiddenFieldHideCenter.Value = "1";
                    }
                    else
                    {
                        HiddenFieldHideCenter.Value = "0";
                    }

                }

                if (uoHiddenFieldSaveConfirmationTrans.Value == "Transporation")
                {
                    InsertTransToSend();
                    uoHiddenFieldSaveConfirmationTrans.Value = "";
                }

                if (uoHiddenFieldSaveConfirmation.Value == "MeetAndGreet")
                {

                    InsertMeetAndGreetToSend();

                }

                if (uoHiddenFieldSaveConfirmationPA.Value == "PortAgent")
                {

                    insertPortAgentToSend();
                    uoHiddenFieldSaveConfirmationPA.Value = "";

                }

                if (uoHiddenFieldSaveConfirmationPA.Value == "PortAgentHotel")
                {
                    SendRequestPortAgentHotel();
                    uoHiddenFieldSaveConfirmationPA.Value = "";
                }


                if (uoHiddenFieldSaveConfirmationSG.Value == "Safeguard")
                {
                    //insertPortAgentToSend();
                    uoHiddenFieldSaveConfirmationSG.Value = "";

                }

            }
            catch (Exception ex)
            {
                AlertMessage("ButtonOverflow: " + ex.Message);
            }
        }

        protected void CheckBoxNo_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBoxNo.Checked == true)
                {
                    CheckBoxYes.Checked = false;
                }
                else
                {
                    CheckBoxYes.Checked = true;
                }
                TextBoxConfirmrate.Enabled = CheckBoxNo.Checked;
            }
            catch (Exception ex)
            {
                AlertMessage("CheckBoxNo: " + ex.Message);
            }
        }

        protected void uoButtonLoadAir_click(object sender, EventArgs e)
        {
            try
            {
                uoListviewAir.DataSource = null;
                uoListviewAir.DataBind();

                uoListViewHotelBook.DataSource = null;
                uoListViewHotelBook.DataBind();

                uoListViewTransportation.DataSource = null;
                uoListViewTransportation.DataBind();

                uoListViewCompanionList.DataSource = null;
                uoListViewTransportation.DataBind();

                //uoListViewCompanionList.DataSource = null;
                //uoListViewCompanionList.DataBind();




                string test = uoHiddenFieldRequestDate.Value;
                ClearHotelObject(1);

                Session["CrewAssistTransaction"] = null;
                Isdeleted(true);

                if (Session["SeafarerDetailList"] != null)
                {
                    var DetailList = (List<SeafarerDetailHeader>)Session["SeafarerDetailList"];

                    if (DetailList != null || DetailList.Count > 0)
                    {

                        var MyList = DetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                        if (MyList.Count > 0)
                        {
                            //uoHiddenFieldIDBigint.ClientID

                            var AirList = MyList[0].CrewAssistAirTransaction.Where(a => a.IdBigint == GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value)).ToList();

                            uoListviewAir.DataSource = AirList;
                            uoListviewAir.DataBind();



                            if (AirList.Count > 0)
                            {
                                //ListView  item = (ListView)uoListviewAir;


                                foreach (ListViewDataItem item in uoListviewAir.Items)
                                {
                                    DropDownList GetDDLList = (DropDownList)item.FindControl("uoDropDownListStatus");
                                    GetDDLList.SelectedIndex = GlobalCode.GetselectedIndex(GetDDLList, AirList[0].StatusID);
                                }


                            }





                            if (MyList[0].CrewAssistAirTransaction.Count > 0)
                            {
                                var air = MyList[0].CrewAssistAirTransaction;
                                if (uoTextBoxStatus.Text.ToString() == "ON")
                                {
                                    uoHiddenFieldArrCode.Value = air[air.Count - 1].ArrivalAirportLocationCode;
                                }
                                else
                                {
                                    uoHiddenFieldArrCode.Value = air[0].DepartureAirportLocationCode;
                                }
                            }

                            var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                            if (res != null)
                            {
                                uoListViewHotelBook.DataSource = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                                uoListViewHotelBook.DataBind();
                            }

                            GetAirSequence(uoHiddenFieldAirStatus.Value);
                            GetComboVendor(uoHiddenFieldPortCode.Value.ToString(), true);
                            uoDropDownListPort.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPort, uoHiddenFieldPortCode.Value.ToString());
                            uoDropDownListPort_SelectedIndexChanged(null, null);

                            uoHiddenFieldSeqNo.Value = uoHiddenFieldTRSequenceNo.Value;




                            List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
                            GetCrewTransaction(ref cres, 1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text),
                                              GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                                              GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                                              GlobalCode.Field2Long(uoHiddenFieldTRSequenceNo.Value),
                                              GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value),
                                              GlobalCode.Field2String(uoHiddenFieldPortCode.Value),
                                              GlobalCode.Field2String(uoHiddenFieldArrCode.Value),
                                              false, uoHiddenFieldUser.Value.ToString());


                            if (cres.Count > 0)
                            {

                                if (GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0)
                                {
                                    uoCheckBoxPAMAG.Checked = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : false;
                                    uoCheckBoxPATrans.Checked = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : false;
                                    uoCheckBoxPAHotel.Checked = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : false;
                                    uoCheckBoxPASafeguard.Checked = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : false;

                                    uoCheckBoxPAMAG.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : true;
                                    uoCheckBoxPATrans.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : true;
                                    uoCheckBoxPAHotel.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true;
                                    uoCheckBoxPASafeguard.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : true;
                                }

                                LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking);
                                LoadMeetAndGreetDetail(cres[0].CrewAssistMeetAndGreet);
                                LoadPortAgentDetail(cres[0].CrewAssistPortAgentRequest);
                                LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                                LoadSafeguardRequestDetail(cres[0].CrewAssistSafeguardRequest);

                                uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                                uoListViewTransportation.DataBind();



                                uoListViewCompanionList.DataSource = cres[0].HotelRequestCompanion.Where(n => n.REQUESTID == GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value)).ToList();
                                uoListViewCompanionList.DataBind();


                            }
                            else
                            {
                                ClearHotelObject(1);
                                ClearMeetAndGreet();
                                ClearObjectPortAgent(false);
                                ClearObjectTrans();
                                ClearObjectSafeguard();

                                uoListViewTransportation.DataSource = null;
                                uoListViewTransportation.DataBind();

                                uoListViewCompanionList.DataSource = null;
                                uoListViewCompanionList.DataBind();

                            }


                            var resIsdelete = MyList[0].SeafarerDetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                            if (resIsdelete.Count > 0)
                            {
                                if (GlobalCode.Field2Bool(resIsdelete[0].IsDeleted) == true)
                                {
                                    Isdeleted(!GlobalCode.Field2Bool(resIsdelete[0].IsDeleted));
                                }

                            }
                        }
                        else
                        {
                            LoadComboVehicle();
                        }
                    }

                    List<SeafarerDetailHeader> List = new List<SeafarerDetailHeader>();
                    List = (List<SeafarerDetailHeader>)Session["SeafarerDetailList"];
                    var Detailres = (from n in List[0].SeafarerDetailList
                                     where n.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)
                                     select new
                                     {
                                         Status = n.Status,
                                         ShipID = n.VesselID,
                                         ship = n.VesselCode + "-" + n.Vessel,
                                         Brand = n.BrandCode + "-" + n.Brand,
                                         ReasonCode = n.ReasonCode
                                     }
                               ).ToList();
                    if (Detailres.Count > 0)
                    {
                        uoHiddenFieldVesselID.Value = Detailres[0].ShipID.ToString();
                        uoTextBoxBrand.Text = Detailres[0].Brand.ToString();
                        uoTextBoxShip.Text = Detailres[0].ship.ToString();
                        uoTextBoxStatus.Text = Detailres[0].Status.ToString();
                        uoTextBoxReasonCode.Text = Detailres[0].ReasonCode;

                    }
                }

                ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);
            }
            catch (Exception ex)
            {
                AlertMessage("ButtonLoadAir: " + ex.Message);
            }
        }


        void Isdeleted(bool value)
        {

            EnablePortAgentControl(value);
            EnableHotelControl(value);
            EnableTransporationControl(value);
            EnableMeetAndGreetControl(value);
            EnableCrewScheduleControl(value);

        }
        void EnableCrewScheduleControl(bool value)
        {

            uoDropDownListPort.Enabled = value;
            uoDropDownListExpenseType.Enabled = value;
            uoCheckBoxShow.Enabled = value;
            uoCheckBoxMedical.Enabled = value;
            uoButtonAddHotel.Enabled = value;
            uoListViewHotelBook.Enabled = value;
            uoListViewTransportation.Enabled = value;
        }



        void GetAirSequence(string Status)
        {
            try
            {
                uoHiddenFieldTRSequenceNo.Value = "0";


                uoHiddenFieldDepartureTime.Value = "";
                uoHiddenFieldArrivalTime.Value = "";

                foreach (ListViewItem rowitem in uoListviewAir.Items)
                {
                    uoHiddenFieldTRSequenceNo.Value = GlobalCode.Field2Int(((Label)rowitem.FindControl("lblSeqNo")).Text).ToString();
                    uoHiddenFieldDepartureTime.Value = GlobalCode.Field2String(((Label)rowitem.FindControl("lblDeptTime")).Text);
                    uoHiddenFieldArrivalTime.Value = GlobalCode.Field2String(((Label)rowitem.FindControl("lblArrTime")).Text);

                    if (Status == "OFF") return;
                }
            }
            catch (Exception ex)
            {
                AlertMessage("GetAirSequence: " + ex.Message);
            }

        }

        void GetCrewTransaction(short LoadType, long SeafarerID, long TravelRequestID, long IDBigInt, long SeqNo, DateTime Startdate, string DepCode,
             string ArrCode, bool IsAir, string UserID)
        {
            try
            {
                SF = new CrewAssistBLL();
                List<CrewAssistTransaction> res = new List<CrewAssistTransaction>();

                Session["CrewAssistTransaction"] = null;

                uoHiddenFieldTransHotelOtherID.Value = "0";
                uoHiddenFieldTransTransapotationID.Value = "0";
                uoHiddenFieldTransMeetAndGreetID.Value = "0";
                uoHiddenFieldTransPortAgentID.Value = "0";
                uoHiddenFieldTransSafeguardID.Value = "0";

                uoCheckBoxPAMAG.Checked = false;
                uoCheckBoxPATrans.Checked = false;
                uoCheckBoxPAHotel.Checked = false;
                uoCheckBoxPALuggage.Checked = false;
                uoCheckBoxPASafeguard.Checked = false;
                uoCheckBoxPAVisa.Checked = false;
                uoCheckBoxPAOther.Checked = false;

                bool isApproved = true;

                res = SF.GetCrewTransaction(LoadType, SeafarerID, TravelRequestID, IDBigInt, SeqNo, Startdate, DepCode, ArrCode, IsAir, UserID);
                if (res.Count > 0)
                {
                    uoHiddenFieldTransHotelOtherID.Value = res[0].TransHotelOtherID.ToString();
                    uoHiddenFieldTransTransapotationID.Value = res[0].TransTransapotationID.ToString();
                    uoHiddenFieldTransMeetAndGreetID.Value = res[0].TransMeetAndGreetID.ToString();
                    uoHiddenFieldTransPortAgentID.Value = res[0].TransPortAgentID.ToString();
                    uoHiddenFieldTransSafeguardID.Value = res[0].TransSafeguardID.ToString();

                    uoHiddenFieldMeetAndGreetID.Value = res[0].ReqMeetAndGreet.ToString();
                    uoHiddenFieldPortAgentID.Value = res[0].ReqPortAgentID.ToString();
                    uoHiddenFieldTransVendorID.Value = res[0].ReqTransapotati.ToString();
                    uoHiddenFieldSafeguardRequestID.Value = res[0].ReqSafeguardID.ToString();
                    uoHiddenFieldHotelRequestID.Value = res[0].ReqHotelOtherID.ToString();


                    uoCheckBoxPAMAG.Checked = res[0].TransMeetAndGreetID > 0 ? false : false;
                    uoCheckBoxPATrans.Checked = res[0].TransTransapotationID > 0 ? false : false;
                    uoCheckBoxPAHotel.Checked = res[0].TransHotelOtherID > 0 ? false : false;
                    uoCheckBoxPASafeguard.Checked = res[0].TransSafeguardID > 0 ? false : false;


                    uoCheckBoxPAMAG.Enabled = res[0].TransMeetAndGreetID > 0 ? false : true;
                    uoCheckBoxPATrans.Enabled = res[0].TransTransapotationID > 0 ? false : true;
                    uoCheckBoxPAHotel.Enabled = res[0].TransHotelOtherID > 0 ? false : true;
                    uoCheckBoxPASafeguard.Enabled = res[0].TransSafeguardID > 0 ? false : true;


                    if (res[0].CrewAssistHotelBooking.Count > 0)
                    {
                        isApproved = res[0].CrewAssistHotelBooking[0].IsApproved == true ? true : false;
                    }

                }

                Session["CrewAssistTransaction"] = res;

                //EnableHotelControl(GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true);

                EnableHotelControl(GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? isApproved : true);

                //EnableHotelControl(GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true);
                EnableTransporationControl(GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 && GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) > 0 ? false : true);
                EnablePortAgentControl(GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) > 0 ? false : true);



            }
            catch (Exception ex)
            {
                AlertMessage("GetCrewTransaction: " + ex.Message);
            }

        }

        void GetCrewTransaction(ref List<CrewAssistTransaction> res, short LoadType, long SeafarerID, long TravelRequestID, long IDBigInt, long SeqNo, DateTime Startdate, string DepCode, string ArrCode, bool IsAir, string UserID)
        {
            try
            {
                SF = new CrewAssistBLL();
                res = new List<CrewAssistTransaction>();

                Session["CrewAssistTransaction"] = null;


                uoHiddenFieldTransHotelOtherID.Value = "0";
                uoHiddenFieldTransTransapotationID.Value = "0";
                uoHiddenFieldTransMeetAndGreetID.Value = "0";
                uoHiddenFieldTransPortAgentID.Value = "0";
                uoHiddenFieldTransSafeguardID.Value = "0";


                uoCheckBoxPAMAG.Checked = false;
                uoCheckBoxPATrans.Checked = false;
                uoCheckBoxPAHotel.Checked = false;
                uoCheckBoxPALuggage.Checked = false;
                uoCheckBoxPASafeguard.Checked = false;
                uoCheckBoxPAVisa.Checked = false;
                uoCheckBoxPAOther.Checked = false;

                bool isApproved = true;
                res = SF.GetCrewTransaction(LoadType, SeafarerID, TravelRequestID, IDBigInt, SeqNo, Startdate, DepCode, ArrCode, IsAir, UserID);
                if (res.Count > 0)
                {
                    uoHiddenFieldTransHotelOtherID.Value = res[0].TransHotelOtherID.ToString();
                    uoHiddenFieldTransTransapotationID.Value = res[0].TransTransapotationID.ToString();
                    uoHiddenFieldTransMeetAndGreetID.Value = res[0].TransMeetAndGreetID.ToString();
                    uoHiddenFieldTransPortAgentID.Value = res[0].TransPortAgentID.ToString();
                    uoHiddenFieldTransSafeguardID.Value = res[0].TransSafeguardID.ToString();

                    uoHiddenFieldMeetAndGreetID.Value = res[0].ReqMeetAndGreet.ToString();
                    uoHiddenFieldPortAgentID.Value = res[0].ReqPortAgentID.ToString();
                    uoHiddenFieldTransVendorID.Value = res[0].ReqTransapotati.ToString();
                    uoHiddenFieldSafeguardRequestID.Value = res[0].ReqSafeguardID.ToString();
                    uoHiddenFieldHotelRequestID.Value = res[0].ReqHotelOtherID.ToString();

                    uoCheckBoxPAMAG.Checked = res[0].TransMeetAndGreetID > 0 ? false : false;
                    uoCheckBoxPATrans.Checked = res[0].TransTransapotationID > 0 ? false : false;
                    uoCheckBoxPAHotel.Checked = res[0].TransHotelOtherID > 0 ? false : false;
                    uoCheckBoxPASafeguard.Checked = res[0].TransSafeguardID > 0 ? false : false;

                    uoCheckBoxPAMAG.Enabled = res[0].TransMeetAndGreetID > 0 ? false : true;
                    uoCheckBoxPATrans.Enabled = res[0].TransTransapotationID > 0 ? false : true;
                    uoCheckBoxPAHotel.Enabled = res[0].TransHotelOtherID > 0 ? false : true;
                    uoCheckBoxPASafeguard.Enabled = res[0].TransSafeguardID > 0 ? false : true;


                    if (res[0].CopyEmail.Count > 0)
                    {
                        var crewass = (from c in res[0].CopyEmail
                                       where c.EmailType == 1 // CREWASSIST
                                       select new
                                       {
                                           Email = c.Email
                                       }
                                        ).ToList();


                        if (crewass.Count > 0) uoHiddenFieldCrewAssist.Value = crewass[0].Email;

                        var hotel = (from c in res[0].CopyEmail
                                     where c.EmailType == 2 // Hotel
                                     select new
                                     {
                                         Email = c.Email
                                     }
                                        ).ToList();


                        if (hotel.Count > 0) uoHiddenFieldEmailHotel.Value = hotel[0].Email;

                        var Ship = (from c in res[0].CopyEmail
                                    where c.EmailType == 3 // Ship
                                    select new
                                    {
                                        Email = c.Email
                                    }
                                        ).ToList();


                        if (Ship.Count > 0) uoHiddenFieldCopyShip.Value = Ship[0].Email;

                        var scheduler = (from c in res[0].CopyEmail
                                         where c.EmailType == 4 // scheduler
                                         select new
                                         {
                                             Email = c.Email
                                         }
                                        ).ToList();


                        if (scheduler.Count > 0) uoHiddenFieldScheduler.Value = scheduler[0].Email;

                    }

                    CheckBoxCopycrewassist.ToolTip = uoHiddenFieldCrewAssist.Value.ToString();
                    CheckBoxCopycrewhotels.ToolTip = uoHiddenFieldEmailHotel.Value.ToString();
                    CheckBoxCopyShip.ToolTip = uoHiddenFieldCopyShip.Value.ToString();
                    CheckBoxScheduler.ToolTip = uoHiddenFieldScheduler.Value.ToString();


                    if (res[0].CrewAssistHotelBooking.Count > 0)
                    {
                        isApproved = res[0].CrewAssistHotelBooking[0].IsApproved == true ? true : false;
                    }

                }

                Session["CrewAssistTransaction"] = res;



                //EnableHotelControl(GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true);
                EnableHotelControl(GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? isApproved : true);

                EnableTransporationControl(GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 && GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) > 0 ? false : true);
                EnablePortAgentControl(GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) > 0 ? false : true);
            }
            catch (Exception ex)
            {
                AlertMessage("GetCrewTransaction: " + ex.Message);
            }

        }



        void GetComboVendor(string portcode, bool isPort)
        {
            try
            {
                if (Session["CrewAssistSafeguardVendor"] != null)
                {
                    List<CrewAssistSafeguardVendor> lst = new List<CrewAssistSafeguardVendor>();

                    lst = (List<CrewAssistSafeguardVendor>)Session["CrewAssistSafeguardVendor"];
                    uoDropDownListSafeguard.DataSource = null;
                    uoDropDownListSafeguard.Items.Clear();
                    if (lst != null)
                    {
                        var Safresult = (from dbo in lst
                                         where dbo.PortCode == uoHiddenFieldPortCode.Value.ToString() ||
                                               dbo.PortCode == uoHiddenFieldArrCode.Value.ToString()
                                         select new
                                         {
                                             SafeguardName = dbo.SafeguardName,
                                             SafeguardVendorID = dbo.SafeguardVendorID
                                         })
                               .ToList().Distinct();

                        uoDropDownListSafeguard.DataSource = Safresult;
                        uoDropDownListSafeguard.DataTextField = "SafeguardName";
                        uoDropDownListSafeguard.DataValueField = "SafeguardVendorID";
                        uoDropDownListSafeguard.DataBind();
                        uoDropDownListSafeguard.Items.Insert(0, new ListItem("--Select Safeguard--", "0"));

                    }

                }

                uoDropDownListPortAgent.DataSource = null;
                uoDropDownListPortAgent.Items.Clear();
                if (Session["PortAgentVendor"] != null)
                {
                    List<CrewAssistVendorPortAgent> palst = new List<CrewAssistVendorPortAgent>();
                    palst = (List<CrewAssistVendorPortAgent>)Session["PortAgentVendor"];

                    uoDropDownListPortAgent.DataSource = null;
                    uoDropDownListPortAgent.Items.Clear();

                    if (palst != null)
                    {
                        var pasult = (from dbo in palst
                                      where (
                                                dbo.PortCode == uoHiddenFieldPortCode.Value.ToString() ||
                                                dbo.PortCode == uoHiddenFieldArrCode.Value.ToString()
                                            ) && dbo.IsAir == false
                                      select new
                                      {
                                          PortAgentVendorName = dbo.PortAgentVendorName,
                                          PortAgentVendorID = dbo.PortAgentVendorID
                                      }).ToList().Distinct();

                        uoDropDownListPortAgent.DataSource = pasult;
                        uoDropDownListPortAgent.DataTextField = "PortAgentVendorName";
                        uoDropDownListPortAgent.DataValueField = "PortAgentVendorID";
                        uoDropDownListPortAgent.DataBind();
                        uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider--", "0"));

                    }
                }

                if (Session["CrewAssistVehicleVendor"] != null)
                {
                    uoDropDownListVehicleVendor.DataSource = null; ; ;
                    uoDropDownListVehicleVendor.Items.Clear();
                    List<VehicleVendor> vhList = new List<VehicleVendor>();
                    vhList = (List<VehicleVendor>)Session["CrewAssistVehicleVendor"];
                    var Vehresult = (from dbo in vhList
                                     where dbo.PortCode == uoHiddenFieldPortCode.Value.ToString() ||
                                           dbo.PortCode == uoHiddenFieldArrCode.Value.ToString()
                                     select new
                                     {
                                         Vehicle = dbo.Vehicle,
                                         VehicleID = dbo.VehicleID,
                                         isPortAgent = dbo.IsPortAgent
                                     })
                                     .ToList().Distinct();

                    uoDropDownListVehicleVendor.DataSource = Vehresult;
                    uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                    uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                    uoDropDownListVehicleVendor.DataBind();
                    uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));






                }

                if (Session["MeetAndGreetVendor"] != null)
                {

                    List<CrewAssistMeetAndGreetVendor> mList = new List<CrewAssistMeetAndGreetVendor>();
                    mList = (List<CrewAssistMeetAndGreetVendor>)Session["MeetAndGreetVendor"];

                    var result = (from dbo in mList
                                  where dbo.AirportCode == uoHiddenFieldPortCode.Value.ToString() ||
                                        dbo.AirportCode == uoHiddenFieldArrCode.Value.ToString()
                                  select new
                                  {
                                      MeetAndGreetVendor = dbo.MeetAndGreetVendor,
                                      MeetAndGreetVendorID = dbo.MeetAndGreetVendorID
                                  })
                                  .ToList().Distinct();
                    uoDropDownListMAndGVendor.DataSource = null;
                    uoDropDownListMAndGVendor.Items.Clear();

                    uoDropDownListMAndGVendor.DataSource = result;
                    uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                    uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                    uoDropDownListMAndGVendor.DataBind();
                    uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));

                }

            }
            catch (Exception ex)
            {
                AlertMessage("GetComboVendor: " + ex.Message);
            }
        }

        protected string GetRequestColor()
        {

            var ColorCode = Eval("ColorCode");
            var ForCode = Eval("ForeColor");

            if (ColorCode.ToString() != "" && ForCode.ToString() != "")
            {
                ColorCode = ColorCode + "; color:" + ForCode + ";\"";

                //return ColorCode = ColorCode + ";" + "";
                return "<tr style=\" background-color:" + ColorCode + ">";
            }
            else if (ColorCode.ToString() != "" && ForCode.ToString() == "")
            {
                ColorCode = ColorCode + ";\" ";

                //return ColorCode = ColorCode + ";" + "";
                return "<tr style=\" background-color:" + ColorCode + ">";
            }
            else
            {
                //return "<tr style=\" background-color:" + ColorCode + ">";
                return "<tr>";
            }


        }


        void LoadComboPort(short LoadType, string userID)
        {
            try
            {

                List<CrewAssitPortList> Ports = new List<CrewAssitPortList>();
                SF = new CrewAssistBLL();

                Ports = SF.GetPort(0, userID);

                uoDropDownListPort.DataSource = Ports;
                uoDropDownListPort.DataTextField = "PORTName";
                uoDropDownListPort.DataValueField = "PORTID";
                uoDropDownListPort.DataBind();
                uoDropDownListPort.Items.Insert(0, new ListItem("--Select Port--", "0"));

            }
            catch (Exception ex)
            {
                AlertMessage("LoadComboPort" + ex.Message);
            }
        }

        protected void uoListViewCompanionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if (e.CommandName == "edit")
            {

                string[] arg = new string[5];
                char[] splitter = { ':' };
                arg = e.CommandArgument.ToString().Split(splitter);

                uoHiddenFieldHotelRequestDetailID.Value = arg[0].ToString();
                uoTextBoxCompLastname.Text = arg[2].ToString();
                uoTextBoxCompFirstname.Text = arg[3].ToString();
                uoTextBoxCompRelationship.Text = arg[4].ToString();
                uoDropDownListCompGender.SelectedValue = arg[5].ToString();
                uoCheckBoxAddCompanion.Checked = true;

            }
            if (e.CommandName == "remove")
            {

                //uoHiddenFieldHotelRequestDetailID.Value = e.CommandArgument.ToString();



                RemoveRequestCompanion(GlobalCode.Field2Long(e.CommandArgument.ToString()), GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value), GlobalCode.Field2Int(e.Item.ClientID.Substring(50, e.Item.ClientID.Length - 50)));

            }

        }

        /// <summary>
        /// Date Created:   01/04/2013
        /// Created By:     Muhallidin G Wali
        /// (description)   Remove Companion
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void RemoveRequestCompanion(long RequestDetailID, long RequestID, int ctr)
        {
            try
            {

                int counter = 0;
                List<HotelRequestCompanion> HRC = new List<HotelRequestCompanion>();

                ListView lst = (ListView)uoListViewCompanionList;
                if (lst.Items.Count > 0)
                {


                    foreach (ListViewItem ls in lst.Items)
                    {
                        if (counter != ctr)
                        {


                            HRC.Add(new HotelRequestCompanion
                             {

                                 FIRSTNAME = ((Label)ls.FindControl("uoLabelFirstname")).Text.ToString(),
                                 LASTNAME = ((Label)ls.FindControl("uoLabelLastname")).Text.ToString(),
                                 RELATIONSHIP = ((Label)ls.FindControl("uoLabelRelationship")).Text.ToString(),
                                 GENDER = ((Label)ls.FindControl("uoLabelGender")).Text.ToString(),
                                 REQUESTID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldComReqID")).Value),
                                 DETAILID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldComReqDetID")).Value),


                                 TRAVELREQID = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldTRID")).Value),
                                 IDBIGINT = GlobalCode.Field2Long(((HiddenField)ls.FindControl("uoHiddenFieldIDBIGNT")).Value),
                                 SEQNO = GlobalCode.Field2Int(((HiddenField)ls.FindControl("uoHiddenFieldSeqNo")).Value),
                                 IsPortAgent = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("uoHiddenFieldPA")).Value),

                             });

                        }
                        counter = counter + 1;
                    }
                }

                uoListViewCompanionList.DataSource = null;
                uoListViewCompanionList.DataBind();
                if (RequestDetailID > 0)
                {
                    DataTable dt = SeafarerBLL.RemoveRequestCompanion(RequestDetailID, RequestID);
                }

                uoListViewCompanionList.DataSource = HRC;
                uoListViewCompanionList.DataBind();

                //SeafarerBLL.RemoveRequestCompanion(uoHiddenFieldHotelRequestDetailID.Value);
                //GetSFCompanion();
            }
            catch (Exception ex)
            {
                AlertMessage("RemoveRequestCompanion: " + ex.Message);
            }
        }

        protected void uoButtonChangePort_click(object sender, EventArgs e)
        {
            try
            {
                string test = uoHiddenFieldRequestDate.Value;

                PageAsyncTask TaskBranch = new PageAsyncTask(OnBeginLoadBranch, OnEndLoadBranch, null, "Async1", true);
                Page.RegisterAsyncTask(TaskBranch);
            }
            catch (Exception ex)
            {
                AlertMessage("ButtonChangePort: " + ex.Message);
            }
        }

        public IAsyncResult OnBeginLoadBranch(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }

        public void OnEndLoadBranch(IAsyncResult ar)
        {
            _dlgtSeafarer.EndInvoke(ar);

            List<CrewAssistGenericClass> list = new List<CrewAssistGenericClass>();
            SF = new CrewAssistBLL();



            string portcode = GlobalCode.Field2String(uoTextBoxStatus.Text) == "ON" ? uoHiddenFieldArrCode.Value.ToString() : uoHiddenFieldPortCode.Value.ToString();
            list = SF.GetComboGeneric(0, portcode, portcode, 0, "uspGetAirportHotel");

            uoDropDownListHotel.Items.Clear();
            int iRowCount = list.Count;
            if (iRowCount > 0)
            {
                uoDropDownListHotel.DataSource = list[0].CrewAssistHotelList;
                uoDropDownListHotel.DataTextField = "HotelName";
                uoDropDownListHotel.DataValueField = "HotelID";
                uoDropDownListHotel.DataBind();
                uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));


                //Session["CrewAssistVehicleVendor"] = list[0].VehicleVendor;
                uoDropDownListVehicleVendor.DataSource = null;
                uoDropDownListVehicleVendor.Items.Clear();

                uoDropDownListVehicleVendor.DataSource = list[0].VehicleVendor;
                uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                uoDropDownListVehicleVendor.DataBind();
                uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle Vendor--", "0"));


                //Session["MeetAndGreetVendor"] = list[0].CrewAssistMeetAndGreetVendor;
                uoDropDownListMAndGVendor.DataSource = null;
                uoDropDownListMAndGVendor.Items.Clear();

                uoDropDownListMAndGVendor.DataSource = list[0].CrewAssistMeetAndGreetVendor; ;
                uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                uoDropDownListMAndGVendor.DataBind();
                uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));

                uoDropDownListPortAgent.DataSource = null;
                uoDropDownListPortAgent.Items.Clear();
                uoDropDownListPortAgent.DataSource = list[0].CrewAssistVendorPortAgent;
                uoDropDownListPortAgent.DataTextField = "PortAgentVendorName";
                uoDropDownListPortAgent.DataValueField = "PortAgentVendorID";
                uoDropDownListPortAgent.DataBind();
                uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider Vendor--", "0"));

                Session["CrewAssistSafeguardVendor"] = list[0].CrewAssistSafeguardVendor;

                uoDropDownListSafeguard.DataSource = null;
                uoDropDownListSafeguard.Items.Clear();
                uoDropDownListSafeguard.DataSource = list[0].CrewAssistSafeguardVendor; ;
                uoDropDownListSafeguard.DataTextField = "SafeguardName";
                uoDropDownListSafeguard.DataValueField = "SafeguardVendorID";
                uoDropDownListSafeguard.DataBind();
                uoDropDownListSafeguard.Items.Add(new ListItem("--Select Safeguard--", "0"));


            }

            //GetComboVendor(portcode, false);

            uoListViewTransportation.DataSource = null;
            uoListViewTransportation.DataBind();

            uoListViewHotelBook.DataSource = null;
            uoListViewHotelBook.DataBind();


            var DetailList = (List<SeafarerDetailHeader>)Session["SeafarerDetailList"];
            if (DetailList != null || DetailList.Count > 0)
            {
                var MyList = DetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                if (MyList.Count > 0)
                {

                    string port = uoHiddenFieldPortCode.Value;
                    if (GlobalCode.Field2String(uoHiddenFieldSaveType.Value) == "true")
                        port = uoTextBoxStatus.Text.ToString() == "OFF" ? uoHiddenFieldPortCode.Value : uoHiddenFieldArrCode.Value;

                    var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID &&
                              n.IDBigInt == GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value) &&
                              n.SeqNo == GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)).ToList();

                    if (res != null)
                    {
                        uoListViewHotelBook.DataSource = res;
                        uoListViewHotelBook.DataBind();
                    }

                    GetAirSequence(uoTextBoxStatus.Text.ToString());

                    List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
                    GetCrewTransaction(ref cres, 1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text),
                                      GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                                      GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                                      GlobalCode.Field2Long(uoHiddenFieldSeqNo.Value),
                                      GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value),
                                      GlobalCode.Field2String(uoHiddenFieldPortCode.Value),
                                      GlobalCode.Field2String(uoHiddenFieldArrCode.Value),
                                      false, uoHiddenFieldUser.Value.ToString());

                    if (cres.Count > 0)
                    {

                        if (GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0)
                        {
                            uoCheckBoxPAMAG.Checked = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : false;
                            uoCheckBoxPATrans.Checked = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : false;
                            uoCheckBoxPAHotel.Checked = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : false;
                            uoCheckBoxPASafeguard.Checked = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : false;

                            uoCheckBoxPAMAG.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : true;
                            uoCheckBoxPATrans.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : true;
                            uoCheckBoxPAHotel.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true;
                            uoCheckBoxPASafeguard.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : true;
                        }


                        LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking);
                        LoadMeetAndGreetDetail(cres[0].CrewAssistMeetAndGreet);
                        LoadPortAgentDetail(cres[0].CrewAssistPortAgentRequest);
                        LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                        LoadSafeguardRequestDetail(cres[0].CrewAssistSafeguardRequest);

                        uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                        uoListViewTransportation.DataBind();



                        uoListViewCompanionList.DataSource = cres[0].HotelRequestCompanion.Where(n => n.REQUESTID == GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value)).ToList();
                        uoListViewCompanionList.DataBind();

                    }
                    else
                    {
                        ClearHotelObject(1);
                        ClearMeetAndGreet();
                        ClearObjectPortAgent(false);
                        ClearObjectTrans();
                        ClearObjectSafeguard();
                        uoListViewTransportation.DataSource = null;
                        uoListViewTransportation.DataBind();

                    }

                }
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);

        }

        void LoadPortAgentDetail(List<CrewAssistPortAgentRequest> list)
        {
            if (list.Count > 0)
            {

                var PA = list[0];
                //uoDropDownListPortAgent.SelectedIndex = -1;
                List<CrewAssistVendorPortAgent> PAList = new List<CrewAssistVendorPortAgent>();
                PAList = (List<CrewAssistVendorPortAgent>)Session["PortAgentVendor"];


                //if (Session["PortAgentVendor"] != null)
                //{
                //    List<CrewAssistVendorPortAgent> palst = new List<CrewAssistVendorPortAgent>();
                //    palst = (List<CrewAssistVendorPortAgent>)Session["PortAgentVendor"];
                //    uoDropDownListPortAgent.DataSource = null; ; ;
                //    uoDropDownListPortAgent.Items.Clear();
                //    if (palst != null)
                //    {
                //        var pasult = (from dbo in palst
                //                      where dbo.PortCode == uoHiddenFieldPortCode.Value.ToString() &&
                //                            dbo.IsAir == true
                //                      select new
                //                      {
                //                          PortAgentVendorName = dbo.PortAgentVendorName,
                //                          PortAgentVendorID = dbo.PortAgentVendorID
                //                      }).ToList().Distinct();

                //        uoDropDownListPortAgent.DataSource = pasult;
                //        uoDropDownListPortAgent.DataTextField = "PortAgentVendorName";
                //        uoDropDownListPortAgent.DataValueField = "PortAgentVendorID";
                //        uoDropDownListPortAgent.DataBind();
                //        uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Port Agent--", "0"));

                //    }
                //}


                uoTextBoxPAAddress.Text = PA.Address;
                uoTextBoxPATelephone.Text = PA.ContactNo;
                uoTextBoxPAPort.Text = PA.PortCode;
                uoTextBoxPARequestDate.Text = GlobalCode.Field2Date(PA.ServiceDatetime);

                uoCheckBoxPAMAG.Checked = (bool)PA.IsMAG;
                uoCheckBoxPATrans.Checked = (bool)PA.IsTrans;
                uoCheckBoxPAHotel.Checked = (bool)PA.IsHotel;
                uoCheckBoxPALuggage.Checked = (bool)PA.IsLuggage;
                uoCheckBoxPASafeguard.Checked = (bool)PA.IsSafeguard;
                uoCheckBoxPAVisa.Checked = (bool)PA.IsVisa;
                uoCheckBoxPAOther.Checked = (bool)PA.IsOther;
                uoTextBoxPAEmail.Text = PA.EmailTo;
                uoTextBoxPAComment.Text = PA.Comment;

                uoHiddenFieldPortAgentID.Value = PA.ReqPortAgentID.ToString();
                uoDropDownListPortAgent.ClearSelection();

                uoCheckBoxPhoneCards.Checked= (bool)PA.IsPhoneCard;
                uoTextBoxPhonecards.Text = GlobalCode.Field2String(PA.PhoneCard);

                uoCheckBoxLaundry.Checked = (bool)PA.IsLaundry;
                uoTextBoxLaundry.Text = GlobalCode.Field2String(PA.Laundry);

                uoCheckBoxGiftCard.Checked = (bool)PA.IsGiftCard;
                uoTextBoxGiftCard.Text = GlobalCode.Field2String(PA.GiftCard);
                               

                if (PA.VehicleTransactionPortAgent.Count > 0)
                {

                    var vt = PA.VehicleTransactionPortAgent[0];

                    //uoDropDownListPAFrom.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPAFrom, vt.RouteIDFromInt);
                    //uoDropDownListPATo.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPATo, vt.RouteIDToInt);

                    //uoTextBoxPAFrom.Text = vt.RouteFrom;
                    //uoTextBoxPATo.Text = vt.RouteTo;
                    //uoTextBoxPAPickupdate.Text = GlobalCode.Field2Date(vt.PickUpDate).ToString();
                    //uoTextBoxPATime.Text = GlobalCode.Field2DateTime(vt.PickUpTime).ToShortTimeString().Substring(0, 5);

                }

                //if (PA.HotelTransactionPortAgent.Count > 0)
                //{

                //    var vt = PA.HotelTransactionPortAgent[0];
                //    uoDropDownListPARoomType.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPARoomType, vt.RoomTypeID);

                //    uoTextBoxPACheckIn.Text = GlobalCode.Field2DateTime(vt.TimeSpanStartDate).ToShortDateString();
                //    uoTextBoxPACheckOut.Text = GlobalCode.Field2DateTime(vt.TimeSpanEndDate).ToShortDateString();
                //    uoTextBoxPADuration.Text=  GlobalCode.Field2String(vt.TimeSpanDurationInt);

                //    uoCheckBoxPAAddMeal.Checked  = GlobalCode.Field2Bool(vt.LunchOrDinner);
                //    uoTextBoxPAMealVoucher.Text= GlobalCode.Field2Double(vt.VoucherAmount).ToString("N2");
                //    uoTextBoxPAHotelName.Text = vt.HotelName;

                //    uoTextBoxPAContRateInclTax.Text = GlobalCode.Field2Double(vt.ContractRate).ToString("N2");
                //    uoTextBoxPAComfirmInclTax.Text = GlobalCode.Field2Double(vt.ConfirmRate).ToString("N2");


                //}



                //uoListViewPATranpo.DataSource = PA.PortAgentVehicle;
                //uoListViewPATranpo.DataBind();

                uoDropDownListPortAgent.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPortAgent, PA.PortAgentVendorID);
            }
            else
            {
                ClearObjectPortAgent(false);
            }
        }


        void LoadMeetAndGreetDetail(List<CrewAssistMeetAndGreet> list)
        {
            if (list.Count > 0)
            {
                uoDropDownListMAndGVendor.DataSource = null;
                uoDropDownListMAndGVendor.Items.Clear();
                var Mlist = (List<CrewAssistMeetAndGreetVendor>)Session["MeetAndGreetVendor"];
                var result = (from dbo in Mlist
                              select new
                              {
                                  MeetAndGreetVendor = dbo.MeetAndGreetVendor,
                                  MeetAndGreetVendorID = dbo.MeetAndGreetVendorID
                              })
                              .ToList().Distinct();

                uoDropDownListMAndGVendor.DataSource = result;
                uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                uoDropDownListMAndGVendor.DataBind();
                uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));

                if (Mlist.Count > 0)
                {

                    uoDropDownListMAndGVendor.ClearSelection();
                    uoDropDownListMAndGVendor.Items.FindByValue(list[0].MeetAndGreetVendorID.ToString()).Selected = true;

                    if (uoDropDownListMAndGVendor.Items.Count > 1)
                    {
                        uoDropDownListMAndGAirport.ClearSelection();
                        uoDropDownListMAndGVendor_SelectedIndexChanged(null, null);
                        uoDropDownListMAndGAirport.SelectedIndex = -1;
                        //uoDropDownListMAndGAirport.Items.FindByValue(list[0].AirportID.ToString()).Selected = true;

                        uoDropDownListMAndGAirport.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListMAndGAirport, list[0].AirportID);


                    }
                    else
                    {
                        uoDropDownListMAndGAirport.ClearSelection();
                        uoDropDownListMAndGAirport.SelectedIndex = -1;
                    }

                }



                uoTextBoxMAndGFligthInfo.Text = list[0].FligthNo;
                uoTextBoxMAndGRate.Text = list[0].Rate.ToString();
                uoTextBoxMAGComment.Text = list[0].Comment;
                uoTextBoxMAGEmail.Text = list[0].Email;
                uoTextBoxAirlineCode.Text = uoHiddenFieldAirlineCode.Value;


            }
            else
            {
                ClearMeetAndGreet();
            }

        }



        void ClearMeetAndGreet()
        {
            if (Session["MeetAndGreetVendor"] != null)
            {
                uoDropDownListMAndGVendor.DataSource = null;
                uoDropDownListMAndGVendor.Items.Clear();
                var Mlist = (List<CrewAssistMeetAndGreetVendor>)Session["MeetAndGreetVendor"];
                var result = (from dbo in Mlist
                              select new
                              {
                                  MeetAndGreetVendor = dbo.MeetAndGreetVendor,
                                  MeetAndGreetVendorID = dbo.MeetAndGreetVendorID
                              })
                              .ToList().Distinct();

                uoDropDownListMAndGVendor.DataSource = result;
                uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                uoDropDownListMAndGVendor.DataBind();
                uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet & Greet--", "0"));

            }


            uoDropDownListMAndGAirport.DataSource = null;
            uoDropDownListMAndGAirport.Items.Clear();
            uoDropDownListMAndGAirport.ClearSelection();
            uoDropDownListMAndGAirport.SelectedIndex = -1;



            uoTextBoxMAndGFligthInfo.Text = "";
            uoTextBoxMAndGRate.Text = "";
            uoTextBoxMAGComment.Text = "";

            uoCheckBoxMAGEmail.Checked = false;
            uoTextBoxMAndGAddress.Text = "";
            uoTextBoxMAndGTelephone.Text = "";
            uoTextBoxMAndGServiceDate.Text = "";
            uoTextBoxMAGEmail.Text = "";

        }


        void LoadComboVehicle()
        {

            if (Session["CrewAssistVehicleVendor"] != null)
            {
                List<VehicleVendor> Vehicle = new List<VehicleVendor>();

                Vehicle = (List<VehicleVendor>)Session["CrewAssistVehicleVendor"];

                //VehicleVendor Vehicle  = new VehicleVendor();

                //Vehicle = (VehicleVendor)Session["CrewAssistVehicleVendor"];
                uoDropDownListVehicleVendor.DataSource = null;
                uoDropDownListVehicleVendor.Items.Clear();


                var res = (from n in Vehicle
                           where n.PortCode == GlobalCode.Field2String(uoHiddenFieldArrCode.Value)
                           select new
                           {
                               Vehicle = n.Vehicle,
                               VehicleID = n.VehicleID,
                           }).ToList();


                uoDropDownListVehicleVendor.DataSource = res;
                uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                uoDropDownListVehicleVendor.DataBind();
                uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

            }

        }


        void LoadComboMeetAndGreet()
        {


            if (Session["MeetAndGreetVendor"] != null)
            {
                uoDropDownListMAndGVendor.DataSource = null;
                uoDropDownListMAndGVendor.Items.Clear();

                List<CrewAssistMeetAndGreetVendor> CrewAssistMeetAndGreet = new List<CrewAssistMeetAndGreetVendor>();

                CrewAssistMeetAndGreet = (List<CrewAssistMeetAndGreetVendor>)Session["MeetAndGreetVendor"];

                var res = (from n in CrewAssistMeetAndGreet
                           where n.AirportCode == GlobalCode.Field2String(uoHiddenFieldArrCode.Value)
                           select new
                           {
                               MeetAndGreetVendor = n.MeetAndGreetVendor,
                               MeetAndGreetVendorID = n.MeetAndGreetVendorID,
                           }).ToList();

                uoDropDownListMAndGVendor.DataSource = res;
                uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                uoDropDownListMAndGVendor.DataBind();
                uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("No Meet And Greet Contract in the Air Port " + GlobalCode.Field2String(uoHiddenFieldArrCode.Value), "0"));


            }

        }

        /// <summary>
        // Date Modified:  25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add uoDropDownListHotel.SelectedIndex = -1;; to clear hotel
        /// </summary>
        void ClearHotelObject(int val)
        {
            if (val == 1) uoDropDownListHotel.SelectedIndex = -1;

            uoTextBoxEmail.Text = "";
            uoTextBoxCheckinDate.Text = "";
            uoTextBoxCheckoutDate.Text = "";
            uoTxtBoxTimeIn.Text = "";
            uoTxtBoxTimeOut.Text = "";
            uoTextContractedRate.Text = "";
            uoTextBoxMealVoucher.Text = "";
            uoTextBoxComfirmRate.Text = "";
            uoTextBoxComment.Text = "";
            uoTextBoxDuration.Text = "";

            uoTextBoxPortAgentConfirm.Text = "";

            uoDropDownListCurrency.SelectedIndex = 0;
            uoCheckContractBoxTaxInclusive.Checked = false;
            uoCheckBoxIsWithShuttle.Checked = false;
            uoCheckboxBreakfast.Checked = false;
            uoCheckboxLunch.Checked = false;
            uoCheckboxDinner.Checked = false;
            uoCheckBoxLunchDinner.Checked = false;
            CheckBoxCopycrewassist.Checked = false;
            CheckBoxCopycrewhotels.Checked = false;
            //CheckBoxFax.Checked = false;
            CheckBoxEmail.Checked = false;

            //EnableHotelControl(true);

            //uoButtonCancelHotel.Visible = false;
            //uoButtonApprove.Visible = false;

            //uoListViewHotelBook.DataSource = null;
            //uoListViewHotelBook.DataBind();


            //uoHotelListViewRemark.DataSource = null;
            //uoHotelListViewRemark.DataBind();


        }

        void EnablePortAgentControl(bool val)
        {

            uoDropDownListPortAgent.Enabled = val;
            uoTextBoxPAAddress.Enabled = val;
            uoTextBoxPATelephone.Enabled = val;
            uoTextBoxPAPort.Enabled = val;
            uoTextBoxPARequestDate.Enabled = val;

            uoCheckBoxPAMAG.Enabled = val;
            uoCheckBoxPATrans.Enabled = val;
            uoCheckBoxPAHotel.Enabled = val;
            uoCheckBoxPALuggage.Enabled = val;
            uoCheckBoxPASafeguard.Enabled = val;
            uoCheckBoxPAVisa.Enabled = val;
            uoCheckBoxPAOther.Enabled = val;
            uoCheckBoxPAEmail.Enabled = val;
            uoTextBoxPAEmail.Enabled = val;
            uoTextBoxPAComment.Enabled = val;
            uoTextBoxPAConfirm.Enabled = val;
            uoTextBoxPAEmail.Enabled = val;



            uoCheckBoxPhoneCards.Enabled = val;
            uoTextBoxPhonecards.Enabled = val;

            uoCheckBoxLaundry.Enabled = val;
            uoTextBoxLaundry.Enabled = val;

            uoCheckBoxGiftCard.Enabled = val;
            uoTextBoxGiftCard.Enabled = val;
                               




        }
        void EnableHotelControl(bool val)
        {
            uoDropDownListHotel.Enabled = val;
            uoLabelAdress.Enabled = val;
            uoTextBoxCheckinDate.Enabled = val;
            uoTextBoxCheckoutDate.Enabled = val;
            uoTxtBoxTimeIn.Enabled = val;
            uoTextBoxDuration.Enabled = val;
            uoDropDownListCurrency.Enabled = val;
            uoTextContractedRate.Enabled = val;
            uoTextBoxMealVoucher.Enabled = val;
            uoTextBoxComfirmRate.Enabled = val;
            uoDropDownListRoomeType.Enabled = val;
            uoCheckboxBreakfast.Enabled = val;
            uoCheckboxLunch.Enabled = val;
            uoCheckboxDinner.Enabled = val;
            uoCheckBoxLunchDinner.Enabled = val;
            uoCheckBoxIsWithShuttle.Enabled = val;
            CheckBoxEmail.Enabled = val;
            uoTextBoxEmail.Enabled = val;
            uoTextBoxComment.Enabled = val;

        }

        void EnableMeetAndGreetControl(bool val)
        {

            uoDropDownListMAndGVendor.Enabled = val;
            uoTextBoxMAndGAddress.Enabled = val;
            uoTextBoxMAndGTelephone.Enabled = val;
            uoDropDownListMAndGAirport.Enabled = val;
            uoTextBoxMAndGServiceDate.Enabled = val;
            uoTextBoxServiceTime.Enabled = val;
            uoDropDownListMAGTo.Enabled = val;
            uoTextBoxMAndGRate.Enabled = val;
            uoTextBoxMAndGFligthInfo.Enabled = val;
            uoTextBoxAirlineCode.Enabled = val;
            uoCheckBoxMAGEmail.Enabled = val;
            uoTextBoxMAGEmail.Enabled = val;
            uoTextBoxMAGComment.Enabled = val;
            uoTextBoxMAGConfirm.Enabled = val;

        }



        void EnableTransporationControl(bool val)
        {

            uoDropDownListVehicleVendor.Enabled = val;
            uoDropDownListRouteFrom.Enabled = val;
            //uoTextBoxRouteFrom.Enabled = val;
            //uoDropDownListRouteTO.Enabled = val;
            //uoTextBoxRouteTO.Enabled = val;
            //uoTextBoxPickupDate.Enabled = val;
            //uoTextBoxTime.Enabled = val;
            uoCheckBoxEmailTrans.Enabled = val;
            uoTextBoxEmailTrans.Enabled = val;

            uoTextBoxTransComment.Enabled = val;
            uoTextBoxTranpComfirmby.Enabled = val;
        }
        void ClearObjectVisa()
        {

            uoTextBoxVNationality.Text = "";
            uoTextBoxVRequired.Text = "";
            uoDropDownListVCountryVisiting.SelectedIndex = -1;
        }

        void ClearObjectTrans()
        {
            uoTextBoxTransComment.Text = "";
            //uoTextBoxPickupDate.Text = "";

            //uoTextBoxTime.Text = "";
            uoDropDownListRouteFrom.SelectedIndex = -1;
            //uoDropDownListRouteTO.SelectedIndex = -1;
            uoDropDownListVehicleVendor.SelectedIndex = -1;
            uoCheckBoxEmailTrans.Checked = false;

            uoTextBoxEmailTrans.Text = ""; ;
            uoTextBoxEmailTrans.Enabled = true;
            //uoTextBoxRouteFrom.Text = "";
            //uoTextBoxRouteTO.Text = "";
            uoTextBoxTranpComfirmby.Text = "";
            //EnableTransporationControl(true);

            uoLabelVehicleAddress.Text = "";
            uoLabelVehicleTelephone.Text = "";

            uoListViewTranportationRoute.DataSource = null;
            uoListViewTranportationRoute.DataBind();

            uoListViewTransportation.DataSource = null;
            uoListViewTransportation.DataBind();

        }

        /// <summary>
        // Date Modified:  25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add uoDropDownListSafeguard.SelectedIndex = -1;; to clear safeguard
        /// </summary>
        void ClearObjectSafeguard()
        {
            uoDropDownListSafeguard.SelectedIndex = -1;
            uoTextBoxSAddress.Text = "";
            uoTextBoxSafeguardEmail.Text = "";
            uoTextBoxSafeguardTelephone.Text = "";
            uoTextBoxSafeguardComment.Text = "";
            uoTextBoxServiceTime.Text = "";
            uoTextBoxSafeguardRate.Text = "";
            uoHiddenFieldSGContractID.Value = "0";
            uoHiddenFieldSGContSerTypeID.Value = "0";

        }

        /// <summary>
        /// Date Modified:  25/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   use uoDropDownListRoomeType.SelectedValue instead of uoDropDownListRoomeType.SelectedIndex
        /// </summary>
        void LoadHotelRequestDetail()
        {
            try
            {
                ClearHotelObject(1);
                List<crewassistrequest> CrewAssist = new List<crewassistrequest>();
                SF = new CrewAssistBLL();
                if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) <= 0) return;

                CrewAssist = SF.GetHotelRequest(0, GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value));

                if (CrewAssist.Count > 0)
                {

                    //uoDropDownListRoomeType.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListRoomeType, CrewAssist[0].RoomTypeID);
                    string sRoomType = GlobalCode.Field2String(CrewAssist[0].RoomTypeID);
                    if (uoDropDownListRoomeType.Items.FindByValue(sRoomType) != null)
                    {
                        uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(CrewAssist[0].RoomTypeID);
                    }

                    uoHiddenFieldTransVendorID.Value = CrewAssist[0].TransVehicleIDBigint.ToString();
                    uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);
                    uoTextBoxCheckinDate.Text = CrewAssist[0].CheckinDate.ToString();
                    uoTextBoxCheckoutDate.Text = CrewAssist[0].CheckoutDate.ToString();
                    DateTime oldDate = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text);
                    DateTime newDate = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text);
                    TimeSpan ts = newDate - oldDate;
                    uoTextBoxDuration.Text = ts.Days.ToString();
                    uoCheckboxBreakfast.Checked = CrewAssist[0].MealBreakfastBit;
                    uoCheckboxLunch.Checked = CrewAssist[0].MealLunchBit;
                    uoCheckboxDinner.Checked = CrewAssist[0].MealDinnerBit;
                    uoCheckBoxLunchDinner.Checked = CrewAssist[0].MealLunchDinnerBit;
                    uoCheckBoxIsWithShuttle.Checked = CrewAssist[0].WithShuttleBit;
                    DateTime CheckInTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeIn);
                    string CInTime = String.Format("{0:HH:mm}", CheckInTime);
                    uoTxtBoxTimeIn.Text = CInTime;

                    DateTime CheckOutTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeOut);
                    string COutTime = String.Format("{0:HH:mm}", CheckOutTime);
                    uoTxtBoxTimeOut.Text = COutTime;

                    Decimal fAmount = GlobalCode.Field2Decimal(CrewAssist[0].ConfirmRateMoney);
                    uoTextContractedRate.Text = fAmount.ToString("N2");

                    Decimal RoomTax = GlobalCode.Field2Decimal(CrewAssist[0].ContractedRateMoney);
                    uoTextBoxComfirmRate.Text = RoomTax.ToString("N2");

                    Decimal MealVoucher = GlobalCode.Field2Decimal(CrewAssist[0].MealVoucherMoney);
                    uoTextBoxMealVoucher.Text = MealVoucher.ToString("N2");

                    uoTextBoxEmail.Text = CrewAssist[0].HotelEmail;

                    uoTextBoxComment.Text = CrewAssist[0].HotelComment;

                    //uoHiddenFieldHotelComment.Value = uoTextBoxComment.Text;
                    //uoButtonApprove.Visible = CrewAssist[0].StatusID == 1 ? false : true;


                }

            }
            catch (Exception ex)
            {
                AlertMessage("LoadHotelRequestDetail: " + ex.Message);
            }
        }

        void LoadHotelRequestDetail(List<CrewAssistHotelBooking> CrewAssist)
        {
            try
            {
                if (CrewAssist.Count > 0)
                {

                    uoHiddenFieldTransHotelOtherID.Value = CrewAssist[0].TransHotelID.ToString();
                    uoHiddenFieldHotelRequestID.Value = CrewAssist[0].RequestID.ToString();

                    uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);

                    if (uoDropDownListHotel.SelectedIndex <= 0)
                    {
                        uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                        uoDropDownListHotel.Items.Insert(1, new ListItem((CrewAssist[0].IsPortAgent.ToString() == "True" ? "PA - " : "HT - ") + CrewAssist[0].Branch.ToString(), CrewAssist[0].HotelIDInt.ToString()));
                        uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);
                    }

                    uoTextBoxCheckinDate.Text = CrewAssist[0].CheckinDate.ToString();
                    uoTextBoxCheckoutDate.Text = CrewAssist[0].CheckoutDate.ToString();

                    DateTime oldDate = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text);
                    DateTime newDate = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text);
                    TimeSpan ts = newDate - oldDate;

                    uoTextBoxDuration.Text = CrewAssist[0].NoNitesInt.ToString(); // ts.Days.ToString();

                    uoDropDownListRoomeType.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListRoomeType, CrewAssist[0].RoomTypeID);

                    uoCheckboxBreakfast.Checked = CrewAssist[0].MealBreakfastBit;
                    uoCheckboxLunch.Checked = CrewAssist[0].MealLunchBit;
                    uoCheckboxDinner.Checked = CrewAssist[0].MealDinnerBit;
                    uoCheckBoxLunchDinner.Checked = CrewAssist[0].MealLunchDinnerBit;
                    uoCheckBoxIsWithShuttle.Checked = CrewAssist[0].WithShuttleBit;
                    DateTime CheckInTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeIn);
                    string CInTime = String.Format("{0:HH:mm}", CheckInTime);
                    uoTxtBoxTimeIn.Text = CInTime;

                    DateTime CheckOutTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeOut);
                    string COutTime = String.Format("{0:HH:mm}", CheckOutTime);
                    uoTxtBoxTimeOut.Text = COutTime;

                    Decimal RoomTax = GlobalCode.Field2Decimal(CrewAssist[0].ContractedRateMoney);
                    Decimal fAmount = GlobalCode.Field2Decimal(CrewAssist[0].ConfirmRateMoney);

                    uoTextContractedRate.Text = RoomTax.ToString("N2");

                    uoTextBoxComfirmRate.Text = fAmount.ToString("N2");
                    uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, CrewAssist[0].Currency);

                    Decimal MealVoucher = GlobalCode.Field2Decimal(CrewAssist[0].MealVoucherMoney);
                    uoTextBoxMealVoucher.Text = MealVoucher.ToString("N2");

                    uoTextBoxEmail.Text = CrewAssist[0].HotelEmail;
                    CheckBoxEmail.Checked = true;

                    uoTextBoxComment.Text = CrewAssist[0].HotelComment;

                    uoHiddenFieldHSourceRequest.Value = CrewAssist[0].RequestSourceID.ToString();

                    uoTextBoxPortAgentConfirm.Text = CrewAssist[0].ConfirmBy;

                    uoHiddenFieldPAAirportHotel.Value = CrewAssist[0].IsAutoAirportToHotel.ToString();
                    uoHiddenFieldPAHotelShip.Value = CrewAssist[0].IsAutoHotelToShip.ToString();

                    uoTextBoxReasonCode.Text = CrewAssist[0].ReasonCode;

                    uoHotelListViewRemark.DataSource = CrewAssist[0].HotelRemark;
                    uoHotelListViewRemark.DataBind();

                    uoHiddenFieldContractStart.Value = CrewAssist[0].ContractDateStarted;
                    uoHiddenFieldContractEnd.Value = CrewAssist[0].ContractDateEnd;
                    EnableHotelControl(CrewAssist[0].StatusID == 4 ? false : true);

                }
                else
                {
                    ClearHotelObject(1);
                }

            }
            catch (Exception ex)
            {
                AlertMessage("LoadHotelRequestDetail: " + ex.Message);
            }
        }

        /// <summary>
        /// Date Modiefied:  25/Nov/2013
        /// Modified By:     Josephine Gad
        /// (description)    check if vendor exists in uoDropDownListVehicleVendor before assigning value to avoid error
        /// </summary>
        void LoadTransprtationRequestDetail(List<CrewAssistTranspo> list)
        {
            try
            {
                ClearObjectTrans();
                uoListViewTranspoCost.DataSource = null;
                uoListViewTranspoCost.DataBind();

                if (list.Count == 0)
                {
                    EnableTransporationControl(true);
                    return;
                }


                if (list.Count > 0)
                {
                    uoTextBoxTransComment.Text = list[0].Comment;

                    uoLabelVehicleAddress.Text = GlobalCode.Field2String(list[0].Address).ToString();
                    uoLabelVehicleTelephone.Text = GlobalCode.Field2String(list[0].Telephone).ToString();

                    uoDropDownListRouteFrom.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListRouteFrom, list[0].RouteIDFromInt);
                    uoDropDownListVehicleVendor.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListVehicleVendor, GlobalCode.Field2Int(list[0].VehicleVendorIDInt));




                    uoCheckBoxEmailTrans.Checked = true;
                    uoTextBoxEmailTrans.Text = list[0].Email;

                    uoTextBoxTranpComfirmby.Text = list[0].ConfirmBy;
                    uoListViewTranportationRoute.DataSource = list;
                    uoListViewTranportationRoute.DataBind();

                    uoListViewTranspoCost.DataSource = list[0].VehicleContract;
                    uoListViewTranspoCost.DataBind();


                    uoListViewVehicleRemark.DataSource = list[0].VehicleRemark;
                    uoListViewVehicleRemark.DataBind();

                    uoHiddenFieldTransVendorID.Value = list[0].ReqVehicleIDBigint.ToString();
                    uoHiddenFieldTransTransapotationID.Value = list[0].VehicleTransID.ToString();
                    uoHiddenFieldTSourceRequest.Value = list[0].StatusID.ToString();

                    EnableTransporationControl(GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 && GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) > 0 ? list[0].StatusID >= 4 ? false : true : true);
                }
                else
                {
                    ClearObjectTrans();
                }
            }
            catch (Exception ex)
            {
                AlertMessage("LoadTransprtationRequestDetail: " + ex.Message);
            }
        }






        void LoadSafeguardRequestDetail(List<CrewAssistSafeguardRequest> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    uoHiddenFieldSGContractID.Value = GlobalCode.Field2String(list[0].ContractId);
                    uoHiddenFieldSGContSerTypeID.Value = GlobalCode.Field2String(list[0].ContractServiceTypeID);
                    uoTextBoxSafeguardComment.Text = GlobalCode.Field2String(list[0].Comments);
                    uoTextBoxSafeguarDate.Text = GlobalCode.Field2DateTime(list[0].TransactionDate).ToString();
                    uoTextBoxSafeguardTime.Text = GlobalCode.Field2DateTime(list[0].TransactionTime).ToString();
                    uoTextBoxSafeguardRate.Text = GlobalCode.Field2String(list[0].SGRate).ToString();

                    uoTextBoxSAddress.Text = GlobalCode.Field2String(list[0].Address).ToString();
                    uoTextBoxSafeguardTelephone.Text = GlobalCode.Field2String(list[0].ContactNo).ToString();
                    uoTextBoxSafeguardEmail.Text = GlobalCode.Field2String(list[0].EmailTo).ToString();
                }
                else
                {
                    ClearObjectSafeguard();
                }
            }
            catch (Exception ex)
            {
                AlertMessage("LoadSafeguardRequestDetail: " + ex.Message);
            }


        }
        /// <summary>
        /// Date Modiefied:  25/Nov/2013
        /// Modified By:     Josephine Gad
        /// (description)    check if vendor exists in uoDropDownListVehicleVendor before assigning value to avoid error
        /// </summary>
        void LoadTransprtationRequestDetail()
        {
            try
            {

                ClearObjectTrans();

                if (GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) <= 0) return;
                List<CrewAssistTranspo> list = new List<CrewAssistTranspo>();
                SF = new CrewAssistBLL();
                list = SF.getVehicleRequest(0, GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value));
                if (list.Count > 0)
                {
                    uoTextBoxTransComment.Text = list[0].Comment;
                    //uoTextBoxPickupDate.Text = GlobalCode.Field2Date(list[0].PickUpDate).ToString();

                    //uoTextBoxTime.Text = GlobalCode.Field2DateTime(list[0].PickUpTime).ToShortTimeString().Substring(0, 5);
                    uoDropDownListRouteFrom.ClearSelection();

                    uoDropDownListRouteFrom.Items.FindByValue(list[0].RouteIDFromInt.ToString()).Selected = true;
                    //uoDropDownListRouteTO.ClearSelection();
                    //uoDropDownListRouteTO.Items.FindByValue(list[0].RouteIDToInt.ToString()).Selected = true;

                    if (uoDropDownListVehicleVendor.Items.FindByValue(list[0].VehicleVendorIDInt.ToString()) != null)
                    {
                        uoDropDownListVehicleVendor.ClearSelection();
                        uoDropDownListVehicleVendor.Items.FindByValue(list[0].VehicleVendorIDInt.ToString()).Selected = true;
                    }

                    uoCheckBoxEmailTrans.Checked = true;
                    uoTextBoxEmailTrans.Enabled = true;
                    //if (list[0].Email != "")
                    //    uoCheckBoxEmailTrans.Checked = true;

                    uoTextBoxEmailTrans.Text = list[0].Email;
                }
            }
            catch (Exception ex)
            {
                AlertMessage("LoadTransprtationRequestDetail: " + ex.Message);
            }
        }



        protected void uoListviewTravel_DataBound(object sender, EventArgs e)
        {
            string jsSelectionScript = "<script type='text/javascript'>\r\n";
            jsSelectionScript += "function chkListOperation(op) {\r\n";
            HiddenField chkItem;

            foreach (ListViewDataItem item in uoListviewTravel.Items)
            {
                chkItem = (HiddenField)item.FindControl("hfTravelRequestID");
                jsSelectionScript += "\tdocument.getElementById('" + chkItem.UniqueID.Replace("$", "_") + "').value= op;" + "\r\n";
            }
            jsSelectionScript += "\treturn false;\r\n";
            jsSelectionScript += "}\r\n";
            jsSelectionScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "AllSelectionScript", jsSelectionScript);
        }


        protected void uoDropDownListExpenseType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }

        protected void uoDropDownListRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SF = new CrewAssistBLL();
                List<ComboGenericClass> lst = new List<ComboGenericClass>();
                DropDownList control = new DropDownList();
                control = (DropDownList)sender;


                string route = control.SelectedItem.Text;

                string port = uoHiddenFieldPortCode.Value;
                if (GlobalCode.Field2String(uoHiddenFieldSaveType.Value) == "true")
                    port = uoTextBoxStatus.Text.ToString() == "OFF" ? uoHiddenFieldPortCode.Value : uoHiddenFieldArrCode.Value;

                //if (uoListviewAir.Items.Count == 0)
                //{
                //    route = "SHIP";
                //}

                lst = SF.GetTransportationRoute(route, 0, GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                               GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                               GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value), port,
                               GlobalCode.Field2Int(uoDropDownListPort.SelectedValue));

                switch (control.SelectedItem.Text.ToUpper())
                {
                    case "SHIP":
                        if (lst.Count == 0 && uoHiddenFieldPortCode.Value.ToString() == "")
                        {
                            AlertMessage("Port Required");
                        }
                        else
                        {
                            uoHiddenFieldBrandID.Value = lst.Count == 0 ? uoDropDownListHotel.SelectedValue : lst[0].ID.ToString();
                            //if (control.ID.ToString() == "uoDropDownListRouteFrom")
                            //{
                            //    uoTextBoxRouteFrom.Text = lst.Count == 0 ? uoHiddenFieldPortCode.Value.ToString() : lst[0].Name;
                            //}
                            //else
                            //{
                            //    uoTextBoxRouteTO.Text = lst.Count == 0 ? uoHiddenFieldPortCode.Value.ToString() : lst[0].Name;
                            //}
                        }
                        break;
                    case "HOTEL":
                        if (lst.Count == 0 && GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue) < 1)
                        {
                            uoHiddenFieldBrandID.Value = "0";
                            AlertMessage("Hotel Required");
                        }
                        else
                        {
                            uoHiddenFieldBrandID.Value = lst.Count == 0 ? uoDropDownListHotel.SelectedValue : lst[0].ID.ToString();
                            //if (control.ID.ToString() == "uoDropDownListRouteFrom")
                            //{
                            //    uoTextBoxRouteFrom.Text = lst.Count == 0 ? uoDropDownListHotel.SelectedItem.Text : lst[0].Name;

                            //}
                            //else
                            //{
                            //    uoTextBoxRouteTO.Text = lst.Count == 0 ? uoDropDownListHotel.SelectedItem.Text : lst[0].Name;
                            //}
                        }
                        break;
                    case "AIRPORT":
                        if (lst.Count == 0 && uoHiddenFieldPortCode.Value.ToString() == "")
                        {
                            AlertMessage("Air Detail reqired");
                        }
                        else
                        {
                            uoHiddenFieldBrandID.Value = lst.Count == 0 ? uoDropDownListHotel.SelectedValue : lst[0].ID.ToString();
                            //if (control.ID.ToString() == "uoDropDownListRouteFrom")
                            //{
                            //    uoTextBoxRouteFrom.Text = lst.Count == 0 ? uoHiddenFieldPortCode.Value.ToString() : lst[0].Name;

                            //}
                            //else
                            //{
                            //    uoTextBoxRouteTO.Text = lst.Count == 0 ? uoHiddenFieldPortCode.Value.ToString() : lst[0].Name;
                            //}
                        }

                        break;

                    case "OFFICE":
                        break;

                }
            }
            catch (Exception ex)
            {
                AlertMessage("DropDownListRoute: " + ex.Message);
            }

        }

        protected void uoDropDownListVehicleCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList obj = (DropDownList)sender;
                string str = obj.SelectedItem.Text;

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }


        protected void uoDropDownListVehicleVendor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                short Loadtyepe = GlobalCode.Field2TinyInt(uoDropDownListVehicleVendor.SelectedItem.Text.Substring(0, 2) == "VH" ? 0 : 1);

                CrewAssistBLL CA = new CrewAssistBLL();
                List<VehicleVendor> VehicleVendor = new List<VehicleVendor>();

                VehicleVendor = CA.GetVendorVehicleDetail(Loadtyepe, GlobalCode.Field2Long(uoDropDownListVehicleVendor.SelectedValue), uoHiddenFieldPortCode.Value.ToString());

                uoTextBoxTransComment.Text = "";
                //uoTextBoxPickupDate.Text = "";

                //uoTextBoxTime.Text = "";
                uoDropDownListRouteFrom.SelectedIndex = -1;
                //uoDropDownListRouteTO.SelectedIndex = -1;
                uoCheckBoxEmailTrans.Checked = false;

                uoTextBoxEmailTrans.Text = ""; ;
                uoTextBoxEmailTrans.Enabled = false;

                //uoTextBoxTranpComfirmby.Text = ""; 

                //uoTextBoxRouteFrom.Text = "";
                //uoTextBoxRouteTO.Text = "";

                uoTextBoxTranpComfirmby.Text = "";
                uoLabelVehicleAddress.Text = "";
                uoLabelVehicleTelephone.Text = "";

                uoListViewTranspoCost.DataSource = null;
                uoListViewTranspoCost.DataBind();

                if (VehicleVendor.Count > 0)
                {

                    uoCheckBoxEmailTrans.Checked = true;
                    uoTextBoxEmailTrans.Enabled = true;

                    uoTextBoxEmailTrans.Text = VehicleVendor[0].Email;
                    uoTextBoxTranpComfirmby.Text = "";// VehicleVendor[0].VenConfirm;

                    uoLabelVehicleAddress.Text = VehicleVendor[0].Address;
                    uoLabelVehicleTelephone.Text = VehicleVendor[0].Telephone;

                    uoListViewTranspoCost.DataSource = VehicleVendor[0].VehicleCost;
                    uoListViewTranspoCost.DataBind();

                    uoListViewTranportationRoute.DataSource = null;
                    uoListViewTranportationRoute.DataBind();


                }

                //if (uoDropDownListHotel.SelectedIndex > 0)
                //{

                //    if (uoTextBoxStatus.Text == "ON")
                //    {
                //        uoTextBoxPickupDate.Text = uoTextBoxCheckoutDate.Text;
                //    }
                //    else
                //    {
                //        uoTextBoxPickupDate.Text = uoTextBoxCheckinDate.Text;
                //    }

                //}
                //else
                //{
                //    if (uoHiddenFieldRequestDate.Value != "")
                //    {
                //        uoTextBoxPickupDate.Text = GlobalCode.Field2Date(uoHiddenFieldRequestDate.Value);
                //    }
                //}

                //if (uoTextBoxStatus.Text == "OFF")
                //{
                //    uoTextBoxTime.Text = uoHiddenFieldDepartureTime.Value;
                //}
                //else
                //{
                //    uoTextBoxTime.Text = uoHiddenFieldArrivalTime.Value;
                //}



            }
            catch (Exception ex)
            {
                AlertMessage("DropDownListVehicleVendor: " + ex.Message);
            }
        }

        protected void uoDropDownListMAndGVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (uoListviewAir.Items.Count > 0)
                {
                    CrewAssistBLL CA = new CrewAssistBLL();
                    List<MeetAndGreetGenericClass> GenericClass = new List<MeetAndGreetGenericClass>();
                    GenericClass = CA.GetMeetAndGreetAirport(0, GlobalCode.Field2Int(uoDropDownListMAndGVendor.SelectedValue));
                    uoDropDownListMAndGAirport.DataSource = null;

                    if (GenericClass.Count > 0)
                    {


                        uoTextBoxMAndGAddress.Text = GenericClass[0].CrewAssistMeetAndGreet[0].Address;
                        uoTextBoxMAndGTelephone.Text = GenericClass[0].CrewAssistMeetAndGreet[0].ContactNo;
                        uoTextBoxMAndGServiceDate.Text = GenericClass[0].CrewAssistMeetAndGreet[0].ServiceDate.ToShortDateString();
                        uoTextBoxMAndGFligthInfo.Text = uoHiddenFieldFligthNo.Value; // GenericClass[0].CrewAssistMeetAndGreet[0].FlightInfo;
                        uoTextBoxMAGEmail.Text = GenericClass[0].CrewAssistMeetAndGreet[0].EmailTo;
                        uoTextBoxAirlineCode.Text = uoHiddenFieldAirlineCode.Value;
                        uoTextBoxAirlineCode.Text = uoHiddenFieldAirlineCode.Value;


                        string date = "";
                        string time = "";

                        string PortCode = uoTextBoxStatus.Text.ToString().ToUpper() == "ON" ? uoHiddenFieldArrCode.Value : uoHiddenFieldPortCode.Value.ToString();

                        GetGridCheckBoxChecked(ref date, ref time, PortCode);

                        uoTextBoxMAndGServiceDate.Text = date;
                        uoTextBoxServiceTime.Text = time;

                        if (GenericClass[0].ComboGenericClass.Count > 0)
                        {
                            var airLis = GenericClass[0].ComboGenericClass.Where(a => a.NameCode == PortCode).ToList();

                            uoDropDownListMAndGAirport.ClearSelection();
                            if (airLis.Count > 1)
                            {
                                uoDropDownListMAndGAirport.DataSource = airLis;
                                uoDropDownListMAndGAirport.DataTextField = "Name";
                                uoDropDownListMAndGAirport.DataValueField = "ID";
                                uoDropDownListMAndGAirport.DataBind();
                                uoDropDownListMAndGAirport.Items.Insert(0, new ListItem("--Select Aiport--", "0"));
                            }
                            else if (airLis.Count == 1)
                            {
                                uoDropDownListMAndGAirport.DataSource = airLis;
                                uoDropDownListMAndGAirport.DataTextField = "Name";
                                uoDropDownListMAndGAirport.DataValueField = "ID";
                                uoDropDownListMAndGAirport.DataBind();
                                uoDropDownListMAndGAirport.SelectedIndex = 0;
                            }
                            else
                            {
                                uoDropDownListMAndGAirport.DataSource = GenericClass[0].ComboGenericClass;
                                uoDropDownListMAndGAirport.DataTextField = "Name";
                                uoDropDownListMAndGAirport.DataValueField = "ID";
                                uoDropDownListMAndGAirport.DataBind();
                                uoDropDownListMAndGAirport.SelectedIndex = -1;
                            }
                        }
                    }
                }
                else
                {
                    ClearMeetAndGreet();
                    AlertMessage("Air info required... ");

                }
            }
            catch (Exception ex)
            {
                AlertMessage("DropDownListMAndGVendor: " + ex.Message);
            }

        }

        protected void uoButtonCopyEmail_click(object sender, EventArgs e)
        {
            try
            {
                //    CrewAssistBLL CA = new CrewAssistBLL();
                //    List<CopyEmail> CopyEmail = new List<CopyEmail>();

                //    //if (uoHiddenFieldCheckBoxLoaded.Value == "True")
                //    //{ 
                //    //    CopyEmail = CA.GetTblEmail(0,0,GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue),
                //    //                GlobalCode.Field2Long(uoHiddenFieldVesselID.Value), GlobalCode.Field2Long(uoHiddenFieldVesselID.Value), GlobalCode.Field2Long(uoHiddenFieldVesselID.Value));
                //    //}

                //    CopyEmail = CA.GetTblEmail(0, 0, GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue),
                //    GlobalCode.Field2Long(uoHiddenFieldVesselID.Value), GlobalCode.Field2Long(uoHiddenFieldVesselID.Value), GlobalCode.Field2Long(uoHiddenFieldVesselID.Value));

                //    string NAME = uoHiddenFieldCheckBox.Value.ToString().Remove(0, 22);
                //    uoHiddenFieldCheckBoxLoaded.Value = "False";

                //    if (CopyEmail.Count > 0)
                //    {
                //        if (NAME == "uoCheckBoxCopyAll")
                //        {
                //            if (uoCheckBoxCopyAll.Checked == false)
                //            {
                //                uoTextBoxEmailCCHotel.Text = "";
                //                uoTextBoxEmailCCCrewassist.Text = "";
                //                uoTextBoxCopyShip.Text = "";
                //                uoTextBoxCopyScheduler.Text = "";
                //            }

                //            else
                //            {
                //                for (int i = 0; i < CopyEmail.Count; i++)
                //                {
                //                    if (CopyEmail[i].EmailType == 1)
                //                    {
                //                        uoTextBoxEmailCCHotel.Text = CopyEmail[i].Email;
                //                    }
                //                    else if (CopyEmail[i].EmailType == 2)
                //                    {
                //                        uoTextBoxEmailCCCrewassist.Text = CopyEmail[i].Email;
                //                    }
                //                    else if (CopyEmail[i].EmailType == 3)
                //                    {
                //                        uoTextBoxCopyShip.Text = CopyEmail[i].Email;
                //                    }
                //                    else if (CopyEmail[i].EmailType == 4)
                //                    {
                //                        uoTextBoxCopyScheduler.Text = CopyEmail[i].Email;
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {

                //            for (int i = 0; i < CopyEmail.Count; i++)
                //            {
                //                if (NAME == "CheckBoxCopycrewhotels" && CopyEmail[i].EmailType == 1)
                //                {
                //                    if (CheckBoxCopycrewhotels.Checked == true)
                //                    {
                //                        uoTextBoxEmailCCHotel.Text = CopyEmail[i].Email;
                //                    }
                //                    else
                //                    {
                //                        uoTextBoxEmailCCHotel.Text = "";
                //                    }
                //                }
                //                else if (NAME == "CheckBoxCopycrewassist" && CopyEmail[i].EmailType == 2)
                //                {
                //                    //uoTextBoxEmailCCCrewassist.Text = CopyEmail[i].Email;
                //                    if (CheckBoxCopycrewassist.Checked == true)
                //                    {
                //                        uoTextBoxEmailCCCrewassist.Text = CopyEmail[i].Email;
                //                    }
                //                    else
                //                    {
                //                        uoTextBoxEmailCCCrewassist.Text = "";
                //                    }
                //                }
                //                else if (NAME == "CheckBoxCopyShip" && CopyEmail[i].EmailType == 3)
                //                {
                //                    if (CheckBoxCopyShip.Checked == true)
                //                    {
                //                        uoTextBoxCopyShip.Text = CopyEmail[i].Email;
                //                    }
                //                    else
                //                    {
                //                        uoTextBoxCopyShip.Text = "";
                //                    }
                //                }
                //                else if (NAME == "CheckBoxScheduler" && CopyEmail[i].EmailType == 4)
                //                {
                //                    if (CheckBoxScheduler.Checked == true)
                //                    {
                //                        uoTextBoxCopyScheduler.Text = CopyEmail[i].Email;
                //                    }
                //                    else
                //                    {
                //                        uoTextBoxCopyScheduler.Text = "";
                //                    }
                //                }
                //            }
                //        }

                //    }


                //    uoTextBoxEmailCCHotel.Enabled = CheckBoxCopycrewhotels.Checked;
                //    uoTextBoxEmailCCCrewassist.Enabled = CheckBoxCopycrewassist.Checked;
                //    uoTextBoxCopyShip.Enabled = CheckBoxCopyShip.Checked;
                //    uoTextBoxCopyScheduler.Enabled = CheckBoxScheduler.Checked;
                //    uoTextBoxEmail.Enabled = CheckBoxEmail.Checked;
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }


        }

        protected void uoDropDownListPortAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ClearObjectPortAgent(true);

                CrewAssistBLL CA = new CrewAssistBLL();

                List<CrewAssistVendorPortAgent> crList = new List<CrewAssistVendorPortAgent>();

                crList = CA.GetVendorPortAgent(0, GlobalCode.Field2Long(uoDropDownListPortAgent.SelectedValue));
                if (crList.Count > 0)
                {

                    uoTextBoxPAAddress.Text = GlobalCode.Field2String(crList[0].Address);
                    uoTextBoxPATelephone.Text = GlobalCode.Field2String(crList[0].ContactNo);
                    uoTextBoxPAPort.Text = uoHiddenFieldPortCode.Value; // GlobalCode.Field2String(crList[0].PortCode);
                    uoTextBoxPARequestDate.Text = GlobalCode.Field2Date(uoHiddenFieldRequestDate.Value);

                    uoCheckBoxPAMAG.Checked = false;
                    uoCheckBoxPATrans.Checked = false;
                    uoCheckBoxPAHotel.Checked = false;
                    uoCheckBoxPALuggage.Checked = false;
                    uoCheckBoxPASafeguard.Checked = false;
                    uoCheckBoxPAVisa.Checked = false;
                    uoCheckBoxPAOther.Checked = false;
                    uoCheckBoxPAEmail.Checked = false;

                    uoTextBoxPAEmail.Text = GlobalCode.Field2String(crList[0].EmailTo);
                    uoTextBoxPAComment.Text = GlobalCode.Field2String(crList[0].Comment);

                    //uoListViewPATranpo.DataSource = crList[0].PortAgentVehicle;
                    //uoListViewPATranpo.DataBind();

                    List<VehicleTransactionPortAgent> list1 = new List<VehicleTransactionPortAgent>();

                    uoListViewTranportationRoute.DataSource = list1;
                    uoListViewTranportationRoute.DataBind();


                }

            }
            catch (Exception ex)
            {
                AlertMessage("DropDownListPortAgent: " + ex.Message);
            }

        }

        /// <summary>
        /// Date Modified: 25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add uoDropDownListPortAgent.SelectedIndex = -1;; to clear Port Agent
        /// </summary>
        void ClearObjectPortAgent(bool IsFromPortIndexChange)
        {
            if (!IsFromPortIndexChange)
            {
                uoDropDownListPortAgent.SelectedIndex = -1;
            }
            uoTextBoxPAAddress.Text = "";
            uoTextBoxPATelephone.Text = "";
            uoTextBoxPAPort.Text = "";
            uoTextBoxPARequestDate.Text = "";
            uoCheckBoxPAMAG.Checked = false;
            uoCheckBoxPATrans.Checked = false;
            uoCheckBoxPAHotel.Checked = false;
            uoCheckBoxPALuggage.Checked = false;
            uoCheckBoxPASafeguard.Checked = false;
            uoCheckBoxPAVisa.Checked = false;
            uoCheckBoxPAOther.Checked = false;
            uoCheckBoxPAEmail.Checked = false;

            uoTextBoxPAEmail.Text = "";
            uoTextBoxPAComment.Text = "";

            //uoDropDownListPAFrom.SelectedIndex = - 1;
            //uoDropDownListPATo.SelectedIndex = -1;

            //uoTextBoxPAFrom.Text =  "";
            //uoTextBoxPATo.Text =  "";
            //uoTextBoxPAPickupdate.Text = GlobalCode.Field2Date(DateTime.Now).ToString();
            //uoTextBoxPATime.Text = GlobalCode.Field2DateTime(DateTime.Now).ToShortTimeString().Substring(0, 5);


            List<CrewAssisVehicleCost> list = new List<CrewAssisVehicleCost>();

            //uoListViewPATranpo.DataSource = list;
            //uoListViewPATranpo.DataBind();

            List<VehicleTransactionPortAgent> list1 = new List<VehicleTransactionPortAgent>();

            uoListViewTranportationRoute.DataSource = list1;
            uoListViewTranportationRoute.DataBind();


        }

        protected void uoDropDownListSafeguard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ClearObjectSafeguard();
                if (uoDropDownListSafeguard.SelectedValue == "0") return;
                if (GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value) == 0)
                {
                    AlertMessage("Select Crew Schedule or Air Detail !!! ");
                    return;
                }
                if (GlobalCode.Field2String(uoTextBoxStatus.Text) != "OFF")
                {
                    AlertMessage("Sign off status required!!!");
                    //AlertMessage("Sign off status required!!!", "Required", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }

                CrewAssistBLL CA = new CrewAssistBLL();

                List<CrewAssistSafeguardVendor> crList = new List<CrewAssistSafeguardVendor>();

                crList = CA.GetVendorSafeguard(0, GlobalCode.Field2Long(uoDropDownListSafeguard.SelectedValue));

                if (crList.Count > 0)
                {
                    uoTextBoxSAddress.Text = crList[0].Address.ToString();
                    uoTextBoxSafeguardEmail.Text = crList[0].EmailTo.ToString();
                    uoTextBoxSafeguardTelephone.Text = crList[0].ContactNo.ToString();

                    uoTextBoxServiceTime.Text = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).Hour.ToString() + ":" + GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).Minute.ToString();
                    uoTextBoxSafeguarDate.Text = GlobalCode.Field2Date(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));

                    uoDropDownListServiceRender.DataSource = null;
                    uoDropDownListServiceRender.Items.Clear();
                    Session["CrewAssistSafeguardServiceType"] = crList[0].CrewAssistSafeguardServiceType;

                    if (crList[0].CrewAssistSafeguardServiceType.Count > 0)
                    {

                        Session["CrewAssistSafeguardServiceType"] =
                        uoDropDownListServiceRender.DataSource = crList[0].CrewAssistSafeguardServiceType;
                        uoDropDownListServiceRender.DataTextField = "ServiceDisplay";
                        uoDropDownListServiceRender.DataValueField = "TypeID";
                        uoDropDownListServiceRender.DataBind();
                        uoDropDownListServiceRender.Items.Insert(0, new ListItem("--Select Render Services--", "0"));
                        uoDropDownListServiceRender.SelectedIndex = 0;

                    }

                }
            }

            catch (Exception ex)
            {
                AlertMessage("DropDownListSafeguard: " + ex.Message);
            }


        }

        protected void uoDropDownListServiceRender_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<CrewAssistSafeguardServiceType> lst = new List<CrewAssistSafeguardServiceType>();
                lst = (List<CrewAssistSafeguardServiceType>)Session["CrewAssistSafeguardServiceType"];
                var res = (from n in lst
                           where n.TypeID == GlobalCode.Field2Int(uoDropDownListServiceRender.SelectedValue)
                           select new
                           {
                               RateAmount = n.RateAmount,
                               contactID = n.contractID,
                               conServiceTypeID = n.ContractServiceTypeID
                           }).ToList();
                uoTextBoxSafeguardRate.Text = "0.00";

                uoHiddenFieldSGContractID.Value = "0";
                uoHiddenFieldSGContSerTypeID.Value = "0";
                if (res.Count > 0)
                {
                    uoTextBoxSafeguardRate.Text = res[0].RateAmount;
                    uoHiddenFieldSGContractID.Value = res[0].contactID.ToString();
                    uoHiddenFieldSGContSerTypeID.Value = res[0].conServiceTypeID.ToString();
                }


            }
            catch (Exception ex)
            {
                AlertMessage("DropDownListServiceRender: " + ex.Message);
            }
        }

        protected void uoButtonCancelHotel_Click(object sender, EventArgs e)
        {
            if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) <= 0) return;
            if (uoTextBoxComment.Text.ToString() == "")
            {
                AlertMessage("Comment required...");
                return;
            }

            CrewAssistBLL CA = new CrewAssistBLL();
            ImageButton button = (ImageButton)sender;

            string recipient = "";
            string EmailAdd = uoTextBoxEmail.Text.ToString();

            string[] separators = { ",", ";", " " };
            string[] words = EmailAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                recipient += word + "; ";
            }


            string cssEmail = "";

            if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
            {
                cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
            }

            if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
            {
                cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
            }

            if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
            {
                cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
            }
            if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
            {
                cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
            }
            if (uoTextBoxEmailOther.Text != "")
            {
                cssEmail += uoTextBoxEmailOther.Text.ToString();
            }

            string ccrecipient = "";
            string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in ccmail)
            {

                ccrecipient += word + "; ";
            }

            if (ccrecipient != "")
            {
                ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
            }

            List<CrewAssistEmailDetail> List = new List<CrewAssistEmailDetail>();


            if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -")
            {

                switch (button.ID)
                {
                    case "uoImageButtonCancel":
                        //uoHiddenFieldTravelRequestID.Value = CA.SaveCancelPortAgentHotelRequest(0,
                        //    GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                        //    uoHiddenFieldUser.Value, recipient.Substring(0, recipient.Length - 2).ToString(),
                        //    ccrecipient, "", uoTextBoxComment.Text.ToString());

                        HyperLink hp = new HyperLink();
                        hp.NavigateUrl = String.Format("CrewAssistCancelHotel.aspx");


                        break;

                }

            }
            else
            {
                switch (button.ID)
                {
                    case "uoImageButtonCancel":
                        uoHiddenFieldTravelRequestID.Value = CA.SaveCancelPortAgentHotelRequest(0,
                            GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                            uoHiddenFieldUser.Value, recipient.Substring(0, recipient.Length - 2).ToString(),
                            ccrecipient, "", uoTextBoxComment.Text.ToString());

                        break;
                    case "uoImageButtonApproved":
                        uoHiddenFieldTravelRequestID.Value = CA.SaveCancelPortAgentHotelRequest(1,
                           GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                           uoHiddenFieldUser.Value, recipient.Substring(0, recipient.Length - 2).ToString(),
                           ccrecipient, "", uoTextBoxComment.Text.ToString());

                        break;
                }
            }







            Session["SeafarerDetailList"] = null;
            SF = new CrewAssistBLL();
            _SDetailList = new List<SeafarerDetailHeader>();
            _SDetailList = SF.SeafarerDetailList(0, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text), uoHiddenFieldUser.Value.ToString());
            Session["SeafarerDetailList"] = _SDetailList;

            uoListViewRemark.DataSource = null;
            uoListViewRemark.DataBind();
            if (_SDetailList != null || _SDetailList.Count > 0)
            {
                var MyList = _SDetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                if (MyList.Count > 0)
                {
                    var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                    if (res != null)
                    {
                        uoListViewHotelBook.DataSource = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                        uoListViewHotelBook.DataBind();
                    }
                }

                LoadRemark(_SDetailList[0].Remark);
                uoButtonLoadAir_click(null, null);

            }

        }




        protected void uoButtonDeleteTranspo_Click(object sender, EventArgs e)
        {
            if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) <= 0) return;
            if (uoTextBoxComment.Text.ToString() == "")
            {
                AlertMessage("Comment required...");
                return;
            }

            CrewAssistBLL CA = new CrewAssistBLL();
            ImageButton button = (ImageButton)sender;

            string recipient = "";
            string EmailAdd = uoTextBoxEmail.Text.ToString();

            string[] separators = { ",", ";", " " };
            string[] words = EmailAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                recipient += word + "; ";
            }


            string cssEmail = "";

            if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
            {
                cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
            }

            if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
            {
                cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
            }

            if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
            {
                cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
            }
            if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
            {
                cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
            }
            if (uoTextBoxEmailOther.Text != "")
            {
                cssEmail += uoTextBoxEmailOther.Text.ToString();
            }

            string ccrecipient = "";
            string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in ccmail)
            {

                ccrecipient += word + "; ";
            }

            if (ccrecipient != "")
            {
                ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
            }

            List<CrewAssistEmailDetail> List = new List<CrewAssistEmailDetail>();






            switch (button.ID)
            {
                case "uoImageButtontCancel":
                    //uoHiddenFieldTravelRequestID.Value = CA.SaveCancelPortAgentHotelRequest(0,
                    //    GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                    //    uoHiddenFieldUser.Value, recipient.Substring(0, recipient.Length - 2).ToString(),
                    //    ccrecipient, "", uoTextBoxComment.Text.ToString());


                    //BindTransforation(CA.DeleteTransportationRequest(0, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text)
                    //    , GlobalCode.Field2Long(HiddenFieldTranslID.Value)
                    //    , GlobalCode.Field2Long(HiddenFieldTransTravelRequestID.Value)
                    //    , GlobalCode.Field2Long(HiddenFieldTIDBigint.Value)
                    //    , GlobalCode.Field2Long(HiddenFieldTTravelRequestID.Value)
                    //    , GlobalCode.Field2Int(HiddenFieldTSeqNo.Value), uoHiddenFieldUser.Value.ToString()
                    //    , "crewassist@rccl.com", uoTextBoxEmailTrans.Text.ToString(), ""
                    //    , uoTextBoxTransComment.Text.ToString()));


                    break;
                case "uoImageButtonApproved":
                    uoHiddenFieldTravelRequestID.Value = CA.SaveCancelPortAgentHotelRequest(1,
                       GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                       uoHiddenFieldUser.Value, recipient.Substring(0, recipient.Length - 2).ToString(),
                       ccrecipient, "", uoTextBoxComment.Text.ToString());

                    break;
            }


            Session["SeafarerDetailList"] = null;
            SF = new CrewAssistBLL();
            _SDetailList = new List<SeafarerDetailHeader>();
            _SDetailList = SF.SeafarerDetailList(0, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text), uoHiddenFieldUser.Value.ToString());
            Session["SeafarerDetailList"] = _SDetailList;

            uoListViewRemark.DataSource = null;
            uoListViewRemark.DataBind();
            if (_SDetailList != null || _SDetailList.Count > 0)
            {
                var MyList = _SDetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                if (MyList.Count > 0)
                {
                    var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                    if (res != null)
                    {
                        uoListViewHotelBook.DataSource = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                        uoListViewHotelBook.DataBind();
                    }
                }

                LoadRemark(_SDetailList[0].Remark);
                uoButtonLoadAir_click(null, null);

            }

        }



        protected void uoButtonFromTo_Click(object sender, EventArgs e)
        {
            try
            {
                string[] n = uoHiddenFieldFromTo.Value.Split('|');
                short LT = GlobalCode.Field2TinyInt(uoDropDownListVehicleVendor.SelectedItem.Text.Substring(0, 2) == "VH" ? 0 : 1);

                CrewAssistBLL BL = new CrewAssistBLL();

                //if (n.Length == 1) return;
                List<CrewAssisVehicleCost> VehicleCost = new List<CrewAssisVehicleCost>();

                VehicleCost = BL.GetCrewAssistTransportationCost(LT, GlobalCode.Field2Int(uoDropDownListVehicleVendor.SelectedValue)
                        , GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value), 0, GlobalCode.Field2Int(n[0]), GlobalCode.Field2Int(n[1])
                        , "", uoHiddenFieldUser.Value);

                uoListViewTranspoCost.DataSource = VehicleCost;
                uoListViewTranspoCost.DataBind();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        protected void btnAddTranpoRoute_click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }


        }

        public DataTable GetDataRoute()
        {
            List<CrewAssistRout> list = new List<CrewAssistRout>();
            list.Add(new CrewAssistRout { RoutId = 0, RoutName = "-- Select Route --" });
            list.Add(new CrewAssistRout { RoutId = 1, RoutName = "Ship" });
            list.Add(new CrewAssistRout { RoutId = 2, RoutName = "Hotel" });
            list.Add(new CrewAssistRout { RoutId = 3, RoutName = "Airport" });
            list.Add(new CrewAssistRout { RoutId = 4, RoutName = "Office" });
            list.Add(new CrewAssistRout { RoutId = 5, RoutName = "Other" });

            GlobalCode gc = new GlobalCode();
            return gc.getDataTable(list);


        }


        //public void GetRequestSource()
        //{

        //    List<ComboGenericClass> list = new List<ComboGenericClass>();
        //    list.Add(new ComboGenericClass { ID = 1, Name = "Via Email", NameCode = "VIAE" });
        //    list.Add(new ComboGenericClass { ID = 2, Name = "Via Call", NameCode = "VIAC" });
        //    list.Add(new ComboGenericClass { ID = 3, Name = "Via Live Chat", NameCode = "VIALC" });
        //    list.Add(new ComboGenericClass { ID = 3, Name = "Via Live Chat", NameCode = "VIALC" });


        //    uoRadioButtonListComment.DataSource = list;
        //    uoRadioButtonListComment.DataTextField = "Name";
        //    uoRadioButtonListComment.DataValueField = "ID";
        //    uoRadioButtonListComment.DataBind();


        //}


        protected void uoListViewTranportationRoute_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if (e.CommandName == "Insert")
            {

            }
            if (e.CommandName == "Edit")
            {

            }
            else if (e.CommandName == "Update")
            {

            }
            else if (e.CommandName == "Delete")
            {

            }
        }


        protected void uoListViewTranportationBooking_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete") { }
            else if (e.CommandName == "Udpate") { }

        }

        protected void uoListViewTranportationBooking_ItemDeleting(Object sender, ListViewDeleteEventArgs e)
        {

            try
            {

                ListView lst = (ListView)sender;

                HiddenField HiddenFieldTranslID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTranslID");
                HiddenField HiddenFieldTIDBigint = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTIDBigint");
                HiddenField HiddenFieldTTravelRequestID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTTravelRequestID");
                HiddenField HiddenFieldTransTravelRequestID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTransactionID");

                HiddenField HiddenFieldTSeqNo = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTSeqNo");


                CrewAssistBLL BLL = new CrewAssistBLL();
                BindTransforation(BLL.DeleteTransportationRequest(0, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text)
                    , GlobalCode.Field2Long(HiddenFieldTranslID.Value)
                    , GlobalCode.Field2Long(HiddenFieldTransTravelRequestID.Value)
                    , GlobalCode.Field2Long(HiddenFieldTIDBigint.Value)
                    , GlobalCode.Field2Long(HiddenFieldTTravelRequestID.Value)
                    , GlobalCode.Field2Int(HiddenFieldTSeqNo.Value), uoHiddenFieldUser.Value.ToString()
                    , "crewassist@rccl.com", uoTextBoxEmailTrans.Text.ToString(), ""
                    , uoTextBoxTransComment.Text.ToString()));


            }
            catch (Exception ex)
            {
                AlertMessage(ex.ToString());
            }

        }


        protected void uoListViewTranportationBooking_ItemUpdating(Object sender, ListViewUpdateEventArgs e)
        {

            try
            {

                ListView lst = (ListView)sender;

                HiddenField HiddenFieldTranslID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTranslID");
                HiddenField HiddenFieldTIDBigint = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTIDBigint");
                HiddenField HiddenFieldTTravelRequestID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTTravelRequestID");
                HiddenField HiddenFieldTransTravelRequestID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTransactionID");

                HiddenField HiddenFieldTSeqNo = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTSeqNo");

                CrewAssistBLL BLL = new CrewAssistBLL();
                BindTransforation(BLL.DeleteTransportationRequest(1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text)
                    , GlobalCode.Field2Long(HiddenFieldTranslID.Value)
                    , GlobalCode.Field2Long(HiddenFieldTransTravelRequestID.Value)
                    , GlobalCode.Field2Long(HiddenFieldTIDBigint.Value)
                    , GlobalCode.Field2Long(HiddenFieldTTravelRequestID.Value)
                    , GlobalCode.Field2Int(HiddenFieldTSeqNo.Value), uoHiddenFieldUser.Value.ToString()
                    , "crewassist@rccl.com", uoTextBoxEmailTrans.Text.ToString(), ""
                    , uoTextBoxTransComment.Text.ToString()));
            }
            catch (Exception ex)
            {
                AlertMessage(ex.ToString());
            }

        }


        public void BindTransforation(List<CrewAssistTranspo> list)
        {
            try
            {
                ClearObjectTrans();
                uoListViewTranspoCost.DataSource = null;
                uoListViewTranspoCost.DataBind();

                if (list.Count == 0)
                {
                    EnableTransporationControl(true);
                    return;
                }


                if (list.Count > 0)
                {
                    uoTextBoxTransComment.Text = list[0].Comment;

                    uoLabelVehicleAddress.Text = GlobalCode.Field2String(list[0].Address).ToString();
                    uoLabelVehicleTelephone.Text = GlobalCode.Field2String(list[0].Telephone).ToString();
                    uoDropDownListRouteFrom.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListRouteFrom, list[0].RouteIDFromInt);

                    if (uoDropDownListVehicleVendor.Items.FindByValue(list[0].VehicleVendorIDInt.ToString()) != null)
                    {
                        uoDropDownListVehicleVendor.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListVehicleVendor, GlobalCode.Field2Int(list[0].VehicleVendorIDInt));

                    }


                    uoCheckBoxEmailTrans.Checked = true;
                    uoTextBoxEmailTrans.Text = list[0].Email;

                    uoTextBoxTranpComfirmby.Text = list[0].ConfirmBy;
                    uoListViewTranportationRoute.DataSource = list;
                    uoListViewTranportationRoute.DataBind();

                    uoListViewTranspoCost.DataSource = list[0].VehicleContract;
                    uoListViewTranspoCost.DataBind();


                    uoListViewVehicleRemark.DataSource = list[0].VehicleRemark;
                    uoListViewVehicleRemark.DataBind();

                    uoListViewTransportation.DataSource = list;
                    uoListViewTransportation.DataBind();

                    EnableTransporationControl(list[0].StatusID >= 4 ? false : true);
                    //EnableTransporationControl(GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 && GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value) > 0 ? list[0].StatusID >= 4 ? false : true : true);


                }
                else
                {
                    ClearObjectTrans();
                }
            }
            catch (Exception ex)
            {
                AlertMessage("LoadTransprtationRequestDetail: " + ex.Message);
            }

        }

        protected void uoListViewTranportationRoute_ItemDeleting(Object sender, ListViewDeleteEventArgs e)
        {
            ListView lst = (ListView)sender;
            DropDownList uoDropDownListTFrom = (DropDownList)lst.Items[e.ItemIndex].FindControl("uoDropDownListTFrom");
            DropDownList uoDropDownListTTo = (DropDownList)lst.Items[e.ItemIndex].FindControl("uoDropDownListTTo");
            Label uoTextBoxTFrom = (Label)lst.Items[e.ItemIndex].FindControl("uoLabelRouteFrom");
            Label uoTextBoxTTo = (Label)lst.Items[e.ItemIndex].FindControl("uoLabelRouteTo");
            Label uoTextBoxTPickupdate = (Label)lst.Items[e.ItemIndex].FindControl("uoLabelPickUpDate");
            Label uoTextBoxTTime = (Label)lst.Items[e.ItemIndex].FindControl("uoLabelPickUpTime");
            Label uoTextBoxCost = (Label)lst.Items[e.ItemIndex].FindControl("uoLabelCost");
            HiddenField uoHiddenFieldTransID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldTransID");
            HiddenField uoHiddenFieldReqTransID = (HiddenField)lst.Items[e.ItemIndex].FindControl("uoHiddenFieldReqTransID");

            HiddenField uoHiddenFieldRouteIDFromInt = (HiddenField)lst.Items[e.ItemIndex].FindControl("HiddenFieldRouteIDFromInt");
            HiddenField uoHiddenFieldRouteIDToInt = (HiddenField)lst.Items[e.ItemIndex].FindControl("HiddenFieldRouteIDToInt");


            ListView ListViewControl = (ListView)sender;
            List<VehicleTransactionPortAgent> list = new List<VehicleTransactionPortAgent>();

            Label From;
            Label TO;
            Label RouteFrom;
            Label RouteTo;
            Label PickUpDate;
            Label PickUpTime;

            Label TransCost;

            HiddenField FromID;
            HiddenField ToID;


            HiddenField HiddenFieldTransID;
            HiddenField HiddenFieldReqTransID;


            foreach (ListViewItem item in ListViewControl.Items)
            {

                FromID = (HiddenField)item.FindControl("HiddenFieldRouteIDFromInt");
                ToID = (HiddenField)item.FindControl("HiddenFieldRouteIDToInt");

                From = (Label)item.FindControl("uoLabelOrigin");
                TO = (Label)item.FindControl("uoLabelTO");
                RouteFrom = (Label)item.FindControl("uoLabelRouteFrom");
                RouteTo = (Label)item.FindControl("uoLabelRouteTo");
                PickUpDate = (Label)item.FindControl("uoLabelPickUpDate");
                PickUpTime = (Label)item.FindControl("uoLabelPickUpTime");
                TransCost = (Label)item.FindControl("uoLabelCost");

                HiddenFieldTransID = (HiddenField)item.FindControl("uoHiddenFieldTransID");
                HiddenFieldReqTransID = (HiddenField)item.FindControl("uoHiddenFieldReqTransID");
                //PickUpDate.Textuo != TextBoxTPickupdate.Text
                if ((FromID.Value != uoHiddenFieldRouteIDFromInt.Value &&
                     ToID.Value != uoHiddenFieldRouteIDToInt.Value) || GlobalCode.Field2Long(HiddenFieldReqTransID.Value) > 0)
                {
                    list.Add(new VehicleTransactionPortAgent
                    {
                        RouteIDFromInt = GlobalCode.Field2Int(FromID.Value),
                        RouteFrom = RouteFrom.Text.ToString(),
                        RouteIDToInt = GlobalCode.Field2Int(ToID.Value),
                        RouteTo = RouteTo.Text.ToString(),
                        PickUpDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                        PickUpTime = GlobalCode.Field2DateTime(PickUpTime.Text),
                        FromVarchar = GlobalCode.Field2String(From.Text),
                        ToVarchar = GlobalCode.Field2String(TO.Text),
                        TranspoCost = GlobalCode.Field2Double(TransCost.Text),

                        VehicleTransID = GlobalCode.Field2Long(HiddenFieldTransID.Value),
                        ReqVehicleIDBigint = GlobalCode.Field2Long(HiddenFieldReqTransID.Value)

                    });

                }



            }


            GetTranportationRouteItem(list);
        }


        protected void uoListViewTranportationRoute_ItemInserting(Object sender, ListViewInsertEventArgs e)
        {

            DropDownList uoDropDownListTFrom = (DropDownList)e.Item.FindControl("uoDropDownListTFrom");
            DropDownList uoDropDownListTTo = (DropDownList)e.Item.FindControl("uoDropDownListTTo");
            TextBox uoTextBoxTFrom = (TextBox)e.Item.FindControl("uoTextBoxTFrom");
            TextBox uoTextBoxTTo = (TextBox)e.Item.FindControl("uoTextBoxTTo");
            TextBox uoTextBoxTPickupdate = (TextBox)e.Item.FindControl("uoTextBoxTPickupdate");
            TextBox uoTextBoxTTime = (TextBox)e.Item.FindControl("uoTextBoxTTime");
            TextBox uoTextBoxCost = (TextBox)e.Item.FindControl("uoTextBoxCost");


            ListView ListViewControl = (ListView)sender;
            List<VehicleTransactionPortAgent> list = new List<VehicleTransactionPortAgent>();

            Label From;
            Label TO;
            Label RouteFrom;
            Label RouteTo;
            Label PickUpDate;
            Label PickUpTime;

            Label TransCost;

            HiddenField FromID;
            HiddenField ToID;
            HiddenField HiddenFieldTransID;
            HiddenField HiddenFieldReqTransID;
            foreach (ListViewItem item in ListViewControl.Items)
            {

                FromID = (HiddenField)item.FindControl("HiddenFieldRouteIDFromInt");
                ToID = (HiddenField)item.FindControl("HiddenFieldRouteIDToInt");

                From = (Label)item.FindControl("uoLabelOrigin");
                TO = (Label)item.FindControl("uoLabelTO");
                RouteFrom = (Label)item.FindControl("uoLabelRouteFrom");
                RouteTo = (Label)item.FindControl("uoLabelRouteTo");
                PickUpDate = (Label)item.FindControl("uoLabelPickUpDate");
                PickUpTime = (Label)item.FindControl("uoLabelPickUpTime");
                TransCost = (Label)item.FindControl("uoLabelCost");
                HiddenFieldTransID = (HiddenField)item.FindControl("uoHiddenFieldTransID");
                HiddenFieldReqTransID = (HiddenField)item.FindControl("uoHiddenFieldReqTransID");

                list.Add(new VehicleTransactionPortAgent
                {
                    RouteIDFromInt = GlobalCode.Field2Int(FromID.Value),
                    RouteFrom = RouteFrom.Text.ToString(),
                    RouteIDToInt = GlobalCode.Field2Int(ToID.Value),
                    RouteTo = RouteTo.Text.ToString(),
                    PickUpDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                    PickUpTime = GlobalCode.Field2DateTime(PickUpTime.Text),
                    FromVarchar = GlobalCode.Field2String(From.Text),
                    ToVarchar = GlobalCode.Field2String(TO.Text),
                    TranspoCost = GlobalCode.Field2Double(TransCost.Text),
                    VehicleTransID = GlobalCode.Field2Long(HiddenFieldTransID.Value),
                    ReqVehicleIDBigint = GlobalCode.Field2Long(HiddenFieldReqTransID.Value)
                });
            }

            if (GlobalCode.Field2Int(uoDropDownListTFrom.SelectedValue) > 0 && GlobalCode.Field2Int(uoDropDownListTTo.SelectedValue) > 0)
            {


                string Hotelname = "";
                HiddenField HIdBigint;
                HiddenField HTravelReqIDInt;
                HiddenField HSeqNoInt;
                Label lblHotelBook;

                foreach (ListViewItem item in uoListViewHotelBook.Items)
                {

                    HIdBigint = (HiddenField)item.FindControl("uoHiddenFieldHIDBigint");
                    HSeqNoInt = (HiddenField)item.FindControl("uoHiddenFieldSeqNo");
                    HTravelReqIDInt = (HiddenField)item.FindControl("uoHiddenFieldHTravelRequestID");
                    lblHotelBook = (Label)item.FindControl("lblHotelBook");
                    if (HIdBigint.Value == uoHiddenFieldIDBigint.Value && HSeqNoInt.Value == uoHiddenFieldSeqNo.Value &&
                        HTravelReqIDInt.Value == uoHiddenFieldTravelRequestID.Value) Hotelname = lblHotelBook.Text.ToString();
                }


                if (GlobalCode.Field2Int(uoDropDownListTFrom.SelectedValue) == 2 && uoTextBoxTFrom.Text != "")
                {
                    string SEP = uoTextBoxTFrom.Text != "" ? "(" + uoTextBoxTFrom.Text.Substring(1) : "";
                    string[] separators = { SEP };
                    var h = Hotelname.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    uoTextBoxTFrom.Text = h.Length > 0 ? h[0].ToString() : uoTextBoxTFrom.Text;
                }
                if (GlobalCode.Field2Int(uoDropDownListTTo.SelectedValue) == 2 && uoTextBoxTTo.Text != "")
                {
                    string SEP = uoTextBoxTFrom.Text != "" ? "(" + uoTextBoxTTo.Text.Substring(1) + ")" : "";
                    string[] separators = { SEP };
                    var h = Hotelname.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    uoTextBoxTTo.Text = h.Length > 0 ? h[0].ToString() : uoTextBoxTTo.Text;
                }

                list.Add(new VehicleTransactionPortAgent
                {
                    RouteIDFromInt = GlobalCode.Field2Int(uoDropDownListTFrom.SelectedValue),
                    RouteFrom = uoTextBoxTFrom.Text.ToString(),
                    RouteIDToInt = GlobalCode.Field2Int(uoDropDownListTTo.SelectedValue),
                    RouteTo = uoTextBoxTTo.Text.ToString(),

                    PickUpDate = GlobalCode.Field2DateTime(uoTextBoxTPickupdate.Text),
                    PickUpTime = GlobalCode.Field2DateTime(uoTextBoxTTime.Text),
                    FromVarchar = GlobalCode.Field2String(uoDropDownListTFrom.SelectedItem.Text),
                    ToVarchar = GlobalCode.Field2String(uoDropDownListTTo.SelectedItem.Text),
                    TranspoCost = GlobalCode.Field2Double(uoTextBoxCost.Text),

                    VehicleTransID = GlobalCode.Field2Long(0),
                    ReqVehicleIDBigint = GlobalCode.Field2Long(0)
                });
            }
            else
            {
                AlertMessage("Route Required... ");
            }

            GetTranportationRouteItem(list);
        }

        void GetTranportationRouteItem(List<VehicleTransactionPortAgent> listValue)
        {

            List<VehicleTransactionPortAgent> list = new List<VehicleTransactionPortAgent>();

            uoListViewTranportationRoute.DataSource = listValue;
            uoListViewTranportationRoute.DataBind();

        }

        #endregion

        /// Date Created:   16/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vendors of Port Selected
        /// </summary>
        public void BindVendors(string sUsername, int iSeaPortID, int iAirPortID)
        {
            try
            {
                List<VehicleVendor> listVehicle = new List<VehicleVendor>();
                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<MeetAndGreetList> listMeetGreet = new List<MeetAndGreetList>();
                List<VendorSafeguardList> listSafeguard = new List<VendorSafeguardList>();

                CrewAssistBLL CA = new CrewAssistBLL();
                CA.GetVendors(0, sUsername, iSeaPortID, iAirPortID);

                if (Session["CrewAssist_VehicleList"] != null)
                {
                    listVehicle = (List<VehicleVendor>)Session["CrewAssist_VehicleList"];
                }
                if (Session["CrewAssist_PortAgentList"] != null)
                {
                    listPortAgent = (List<PortAgentDTO>)Session["CrewAssist_PortAgentList"];
                }
                if (Session["CrewAssist_MeetGreet"] != null)
                {
                    listMeetGreet = (List<MeetAndGreetList>)Session["CrewAssist_MeetGreet"];
                }
                if (Session["CrewAssist_Safeguard"] != null)
                {
                    listSafeguard = (List<VendorSafeguardList>)Session["CrewAssist_Safeguard"];
                }

                uoDropDownListVehicleVendor.Items.Clear();
                uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle Vendor--", "0"));
                uoDropDownListVehicleVendor.DataSource = listVehicle;
                uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                uoDropDownListVehicleVendor.DataBind();

                uoDropDownListPortAgent.Items.Clear();
                uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider Vendor--", "0"));

                var listPort = (from a in listPortAgent
                                select new
                                {
                                    PortAgentName = a.PortAgentName,
                                    PortAgentID = a.PortAgentID
                                }).ToList();
                uoDropDownListPortAgent.DataSource = listPort;
                uoDropDownListPortAgent.DataTextField = "PortAgentName";
                uoDropDownListPortAgent.DataValueField = "PortAgentID";
                uoDropDownListPortAgent.DataBind();


                uoDropDownListMAndGVendor.Items.Clear();
                uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet & Greet Vendor--", "0"));
                uoDropDownListMAndGVendor.DataSource = listMeetGreet;
                uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendorName";
                uoDropDownListMAndGVendor.DataValueField = "MeetandGreetVendorId";
                uoDropDownListMAndGVendor.DataBind();


                uoDropDownListSafeguard.Items.Clear();
                uoDropDownListSafeguard.Items.Add(new ListItem("--Select Safeguard Vendor--", "0"));
                uoDropDownListSafeguard.DataSource = listSafeguard;
                uoDropDownListSafeguard.DataTextField = "VendorName";
                uoDropDownListSafeguard.DataValueField = "SafeguardID";
                uoDropDownListSafeguard.DataBind();

            }
            catch (Exception ex)
            {
                AlertMessage("BindVendors: " + ex.Message);
            }
        }


        protected void btnSaveRemark_click(object sender, EventArgs e)
        {

            List<CrewAssisRemark> Lst = new List<CrewAssisRemark>();

            CrewAssistBLL CA = new CrewAssistBLL();
            if (GlobalCode.Field2String(uoLabelUserID.Text) == "") Response.Redirect("~/login.aspx");


            string PortCode = "";
            if (uoDropDownListPort.SelectedIndex > 0)
            {
                PortCode = uoDropDownListPort.SelectedItem.Text.Substring(0, 3);
            }

            string TransTime = "1/1/1753 " + uoTextBoxRemTransTime.Text;

            Lst = CA.InsertCrewAssistRemark(GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value)
                        , GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)
                        , txtRemark.Text.ToString()
                        , uoLabelUserID.Text.ToString()
                        , GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value)
                        , "Crew Assist"
                        , GlobalCode.Field2TinyInt(uoHiddenFieldRSourceRequest.Value)
                        , GlobalCode.Field2Long(uoTextBoxEmployeeID.Text)
                        //, GlobalCode.Field2Int(cboRemarkType.SelectedItem.Value)
                        , GlobalCode.Field2Int(treeViewSearchInputID.Value)
                        , GlobalCode.Field2TinyInt(cboRemarkStatus.SelectedItem.Value)
                        , GlobalCode.Field2String(txtSummaryCall.Text)
                        , GlobalCode.Field2Int(cboRemarkRequestor.SelectedItem.Value)
                        , GlobalCode.Field2DateTime(uoTextBoxRemTransdate.Text)
                        , GlobalCode.Field2DateTime(TransTime)
                        , PortCode
                        , GlobalCode.Field2Bool(uoCheckBoxIR.Checked));

            uoListViewRemarkPopup.DataSource = Lst;
            uoListViewRemarkPopup.DataBind();

            uoListViewRemark.DataSource = Lst;
            uoListViewRemark.DataBind();

            txtRemark.Text = "";
            uoHiddenFieldRemarkID.Value = "";
            txtSummaryCall.Text = "";

            uoTextBoxRemTransdate.Text = null;
            uoTextBoxRemTransTime.Text = null;


            treeViewSearchInputID.Value  = "";
            treeViewSearchInput.Value = "";

            cboRemarkType.SelectedIndex = 0;
            cboRemarkStatus.SelectedIndex = 0;
            cboRemarkRequestor.SelectedIndex = 0;

        }

        protected void ButtonDeleteRemark_click(object sender, EventArgs e)
        {
            //uoHiddenFieldTravelRequestID
            List<CrewAssisRemark> Lst = new List<CrewAssisRemark>();
            if (GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value) == 0) return;

            uoListViewRemarkPopup.DataSource = null;
            uoListViewRemarkPopup.DataBind();

            uoListViewRemark.DataSource = null;
            uoListViewRemark.DataBind();



            CrewAssistBLL CA = new CrewAssistBLL();
            Lst = CA.DeleteCrewAssistRemarks(GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value),
                     GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value), txtRemark.Text.ToString(), uoHiddenFieldUser.Value.ToString());

            uoListViewRemarkPopup.DataSource = Lst;
            uoListViewRemarkPopup.DataBind();

            uoListViewRemark.DataSource = Lst;
            uoListViewRemark.DataBind();

            txtRemark.Text = "";

        }



        protected void uoCheckBoxShow_click(object sender, EventArgs e)
        {
            try
            {
                CheckBox ch = (CheckBox)sender;

                Session["SeafarerDetailList"] = null;
                uoListviewAir.DataSource = null;
                uoListviewAir.DataBind();

                SF = new CrewAssistBLL();
                _SDetailList = new List<SeafarerDetailHeader>();

                _SDetailList = SF.GetAllPastTravelRequest(GlobalCode.Field2TinyInt(ch.Checked == true ? 0 : 1), GlobalCode.Field2Long(uoTextBoxEmployeeID.Text), uoHiddenFieldUser.Value.ToString());

                uoTextBoxCostCenter.Text = "";
                uoTextBoxShip.Text = "";
                uoTextBoxRank.Text = "";
                uoTextBoxStatus.Text = "";
                uoTextBoxNationality.Text = "";
                uoTextBoxGender.Text = "";
                uoTextBoxBrand.Text = "";
                uoHiddenFieldGenderID.Value = "";
                uoHiddenFieldCostCenterID.Value = "";
                uoHiddenFieldRank.Value = "";
                uoHiddenFieldVesselID.Value = "";
                uoHiddenFieldBrandID.Value = "";

                uoTextBoxLastName.Text = "";

                uoTextBoxEmployeeID.Text = "";

                uoTextBoxFirstName.Text = "";
                uoHiddenFieldHotelRequestDetailID.Value = "";

                ClearObjectTrans();
                ClearObjectVisa();
                ClearObjectPortAgent(false);
                ClearHotelObject(1);

                uoListViewRemark.DataSource = null;
                uoListViewRemark.DataBind();

                uoListViewRemarkPopup.DataSource = null;
                uoListViewRemarkPopup.DataBind();

                if (_SDetailList.Count > 0)
                {

                    uoListviewTravel.DataSource = _SDetailList[0].SeafarerDetailList;
                    uoListviewTravel.DataBind();

                    //uoHiddenFieldHotelRequestDetailID.Value = _SDetailList[0].TravelRequetID.ToString();
                    uoHiddenFieldTravelRequestID.Value = _SDetailList[0].TravelRequetID.ToString();

                    uoTextBoxRank.Text = _SDetailList[0].RankCode.ToString() + '-' + _SDetailList[0].RankName.ToString();

                    uoTextBoxNationality.Text = _SDetailList[0].NationalityCode.ToString() + '-' + _SDetailList[0].Nationality.ToString();

                    uoHiddenFieldNationality.Value = _SDetailList[0].NationalityID.ToString();

                    uoTextBoxGender.Text = _SDetailList[0].Gender.ToString();
                    uoHiddenFieldBrandID.Value = _SDetailList[0].BrandID.ToString();

                    uoTextBoxVNationality.Text = _SDetailList[0].Nationality.ToString();
                    uoTextBoxReasonCode.Text = _SDetailList[0].ReasonCode.ToString();


                    uoTextBoxLastName.Text = _SDetailList[0].LastName.ToString();
                    uoTextBoxEmployeeID.Text = _SDetailList[0].SeafarerID.ToString();
                    uoHiddenFieldGenderID.Value = _SDetailList[0].GenderID.ToString();
                    uoTextBoxFirstName.Text = _SDetailList[0].FirstName.ToString();

                    uoHiddenFieldCostCenterID.Value = _SDetailList[0].CostCenterID.ToString();
                    uoHiddenFieldRank.Value = _SDetailList[0].RankID.ToString();
                    uoTextBoxComment.Text = _SDetailList[0].HotelComments.ToString();

                    LoadRemark(_SDetailList[0].Remark);

                    if (_SDetailList[0].SeafarerDetailList.Count > 0)
                    {
                        uoTextBoxCostCenter.Text = _SDetailList[0].SeafarerDetailList[0].CostCenterCode.ToString() + '-' + _SDetailList[0].CostCenterName.ToString();
                        uoTextBoxBrand.Text = _SDetailList[0].BrandCode.ToString() + '-' + _SDetailList[0].Brand.ToString();
                        uoHiddenFieldVesselID.Value = _SDetailList[0].SeafarerDetailList[0].VesselID.ToString();
                        uoTextBoxShip.Text = _SDetailList[0].SeafarerDetailList[0].VesselCode.ToString() + '-' + _SDetailList[0].Vessel.ToString();
                        uoTextBoxStatus.Text = _SDetailList[0].SeafarerDetailList[0].Status.ToString();

                    }

                    if (_SDetailList[0].CrewAssistShipEmail.Count > 0)
                    {
                        uoHiddenFieldVesselEmail.Value = _SDetailList[0].CrewAssistShipEmail[0].Email;

                        CheckBoxCopyShip.ToolTip = uoHiddenFieldVesselEmail.Value.ToString();
                    }

                    if (_SDetailList[0].CopyEmail.Count > 0)
                    {
                        var crewAssEmail = (from s in _SDetailList[0].CopyEmail
                                            where s.EmailName == "Crew Assist"
                                            select new CopyEmail
                                            {
                                                Email = s.Email,
                                                EmailName = s.EmailName,
                                                EmailType = s.EmailType,
                                            }).ToList();

                        if (crewAssEmail.Count > 0)
                        {
                            uoHiddenFieldCrewAssist.Value = crewAssEmail[0].Email;
                            CheckBoxCopycrewassist.ToolTip = uoHiddenFieldCrewAssist.Value;
                        }


                        var Scheduler = (from i in _SDetailList[0].CopyEmail
                                         where i.EmailName == "Scheduler"
                                         select new CopyEmail
                                         {
                                             Email = i.Email,
                                             EmailName = i.EmailName,
                                             EmailType = i.EmailType,
                                         }).ToList();

                        if (Scheduler.Count > 0)
                        {
                            uoHiddenFieldScheduler.Value = Scheduler[0].Email;
                            CheckBoxScheduler.ToolTip = uoHiddenFieldScheduler.Value;
                        }

                        var VendorEmail = (from n in _SDetailList[0].CopyEmail
                                           //where n.EmailType == GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue)
                                           //   && n.EmailName == "Hotel"
                                           where n.EmailName == "Hotel"
                                           select new CopyEmail
                                           {
                                               Email = n.Email,
                                               EmailName = n.EmailName,
                                               EmailType = n.EmailType,
                                           }).ToList();

                        if (VendorEmail.Count > 0)
                        {
                            uoHiddenFieldEmailHotel.Value = Scheduler[0].Email;
                            CheckBoxCopycrewhotels.ToolTip = uoHiddenFieldEmailHotel.Value;
                        }
                    }
                }

                Session["SeafarerDetailList"] = _SDetailList;

                EnableHotelControl(true);

                //GlobalCode.Field2TinyInt( ch.Checked == true ?   EnableHotelControl(!ch.Checked) : 1)

            }
            catch (Exception ex)
            {
                AlertMessage("CheckBoxAddCompanion: " + ex.Message);
            }
        }



        protected void uoDropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string strLanguage = Request.UserLanguages[0];
                System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(strLanguage);
                string dateformat = currentCulture.DateTimeFormat.ShortDatePattern;

                if (uoHiddenFieldFlightStatus.Value == "false") return;

                SF = new CrewAssistBLL();
                DropDownList dList = (DropDownList)sender;

                string val = dList.SelectedValue;


                DropDownList ddlListFind = (DropDownList)sender;
                ListViewItem item1 = (ListViewItem)ddlListFind.NamingContainer;

                DropDownList GetDDLList = (DropDownList)item1.FindControl("uoDropDownListStatus");

                Label lblSeqNo = (Label)item1.FindControl("lblSeqNo");
                HiddenField hfIDBigInt = (HiddenField)item1.FindControl("hfIDBigInt");
                HiddenField OldStatus = (HiddenField)item1.FindControl("uoHiddenFieldStatus");

                foreach (ListViewDataItem item in uoListviewAir.Items)
                {
                    Label lblMessage = (Label)item1.FindControl("lblStatus");
                    lblMessage.Text = ddlListFind.SelectedItem.Text;

                }



                _SDetailList = SF.InsertAirTransactionStatus(GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)
                        , GlobalCode.Field2Long(hfIDBigInt.Value), GlobalCode.Field2Int(lblSeqNo.Text)
                        , GlobalCode.Field2Int(ddlListFind.SelectedItem.Value)
                        , GlobalCode.Field2String(OldStatus.Value)
                        , GlobalCode.Field2String(uoLabelUserID.Text));

                var MyList = _SDetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                if (MyList.Count > 0)
                {

                    var AirList = MyList[0].CrewAssistAirTransaction.Where(a => a.IdBigint == GlobalCode.Field2Long(hfIDBigInt.Value)).ToList();
                    uoListviewAir.DataSource = AirList;
                    uoListviewAir.DataBind();


                    if (AirList.Count > 0)
                    {
                        //ListView  item = (ListView)uoListviewAir;


                        foreach (ListViewDataItem item in uoListviewAir.Items)
                        {
                            DropDownList GetDDLList1 = (DropDownList)item.FindControl("uoDropDownListStatus");
                            GetDDLList1.SelectedIndex = GlobalCode.GetselectedIndex(GetDDLList, AirList[0].StatusID);
                        }

                    }

                    if (dList.SelectedValue == "4" || dList.SelectedValue == "1")
                    {
                        var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                        if (res != null)
                        {
                            uoListViewHotelBook.DataSource = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                            uoListViewHotelBook.DataBind();
                        }
                        uoListViewTransportation.DataSource = null;
                        uoListViewTransportation.DataBind();
                    }


                }


                Session["SeafarerDetailList"] = _SDetailList;



            }
            catch (Exception ex)
            {
                AlertMessage(ex.ToString());
            }
        }





        #region SendEmail

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonSend_click(object sender, EventArgs e)
        {
            if (uoTextBoxEmail.Text.ToString() != "")
            {
                if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -")
                {
                    SendRequest();
                }
                else
                {
                    SendRequestPortAgentHotel();
                }
            }
            else
            {
                AlertMessage("Enter Email Address ");
            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);
        }

        /// <summary>
        /// Date Created:   04/02/2013
        /// Created By:     Muhallidin G Wali
        /// (description)   Submit Hotel Request
        /// ---------------------------------------------------------------------------      

        /// </summary>
        private void SendRequestPortAgentHotel()
        {
            try
            {
                string recipient = "";
                string EmailAdd = uoTextBoxEmail.Text.ToString();

                string[] separators = { ",", ";", " " };
                string[] words = EmailAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    recipient += word + "; ";
                }


                string cssEmail = "";

                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                }

                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                }

                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                }
                if (uoTextBoxEmailOther.Text != "")
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }


                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {

                    ccrecipient += word + "; ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }

                List<CrewAssistEmailDetail> List = new List<CrewAssistEmailDetail>();

                string mesage = "";

                if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value.ToString()) > 0 && CheckBoxEmail.Checked == true && recipient.Length > 0)
                {
                    SF = new CrewAssistBLL();
                    mesage = SF.SendHotelTransactionPortAgentRequest(GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value), GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value),
                        uoHiddenFieldUser.Value, TextBoxWhoConfirm.Text.Trim(),
                        uoTextBoxEmail.Text.ToString(), "Submit Hotel Request", "SubmitRequest",
                        Path.GetFileName(Request.Path), recipient.Substring(0, recipient.Length - 2).ToString(), ccrecipient, "");
                }

                if (mesage != "" && CheckBoxEmail.Checked == true && recipient.Length > 0)
                {
                    SendEmail("RCL Confirmation – Hotel Accommodation Request", mesage, "", recipient.Substring(0, recipient.Length - 2).ToString(), ccrecipient, "Hotel", TextBoxWhoConfirm.Text.ToString());
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Successful", "alert('Request has been booked successfully.');", true);
                }
                if (mesage == "") ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Successful", "alert('Hotel has been Requested successfully.');", true);

                refreshPage();
                HiddenFieldHideCenter.Value = "0";
            }
            catch (Exception ex)
            {

                HiddenFieldHideCenter.Value = "0";
                AlertMessage("SendRequest: " + ex.Message);
            }
        }


        /// <summary>
        /// Date Created:   04/02/2013
        /// Created By:     Muhallidin G Wali
        /// (description)   Submit Hotel Request
        /// ---------------------------------------------------------------------------      
        /// Date Modified:   25/Nov/2013
        /// Modified By:     Josehine Gad
        /// (description)    Refresh the page after sending Hotel Request
        /// </summary>
        private void SendRequest()
        {
            try
            {

                string recipient = "";
                string EmailAdd = uoTextBoxEmail.Text.ToString();

                string[] separators = { ",", ";", " " };
                string[] words = EmailAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    recipient += word + "; ";
                }

                string cssEmail = "";

                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                }

                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                }

                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                }
                if (uoTextBoxEmailOther.Text != "")
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }

                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {

                    ccrecipient += word + "; ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }

                List<CrewAssistEmailDetail> List = new List<CrewAssistEmailDetail>();

                string mesage = "";

                if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value.ToString()) > 0 && CheckBoxEmail.Checked == true && recipient.Length > 0)
                {
                    SF = new CrewAssistBLL();
                    mesage = SF.SendHotelTransactionOtherRequest(uoHiddenFieldHotelRequestID.Value,
                        uoHiddenFieldUser.Value, TextBoxWhoConfirm.Text.Trim(),
                        uoTextBoxEmail.Text.ToString(), "Submit Hotel Request", "SubmitRequest",
                        Path.GetFileName(Request.Path), recipient.Substring(0, recipient.Length - 2).ToString(),
                        ccrecipient, "", GlobalCode.Field2Double(TextBoxConfirmrate.Text));
                }

                if (mesage != "" && CheckBoxEmail.Checked == true && recipient.Length > 0)
                {
                    SendEmail("RCL Confirmation – Hotel Accommodation Request", mesage, "", recipient.Substring(0, recipient.Length - 2).ToString(), ccrecipient, "Hotel", TextBoxWhoConfirm.Text.ToString());
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Successful", "alert('Request has been booked successfully.');", true);
                }

                if (mesage == "") ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Successful", "alert('Hotel has been requested successfully.');", true);

                refreshPage();

                uoHiddenFieldSaveHotel.Value = "0";

            }
            catch (Exception ex)
            {

                HiddenFieldHideCenter.Value = "0";
                AlertMessage("SendRequest: " + ex.Message);
            }
        }


        void refreshPage()
        {

            Session["SeafarerDetailList"] = null;
            SF = new CrewAssistBLL();
            _SDetailList = new List<SeafarerDetailHeader>();
            _SDetailList = SF.SeafarerDetailList(0, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text), uoHiddenFieldUser.Value.ToString());
            Session["SeafarerDetailList"] = _SDetailList;


            uoListViewRemark.DataSource = null;
            uoListViewRemark.DataBind();




            if (_SDetailList != null || _SDetailList.Count > 0)
            {
                var MyList = _SDetailList.Where(a => a.TravelRequetID == GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)).ToList();
                if (MyList.Count > 0)
                {
                    var res = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                    if (res != null)
                    {
                        //uoPanelMaster.UpdateMode ;
                        uoPanelMaster.Update();
                        uoListViewHotelBook.DataSource = MyList[0].CrewAssistHotelBooking.Where(n => n.TravelReqID == MyList[0].TravelRequetID).ToList();
                        uoListViewHotelBook.DataBind();

                    }
                }

                LoadRemark(_SDetailList[0].Remark);


            }
            //    PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginRefreshPage, OnEndRefreshPage, null, "Async1", true);
            //    Page.RegisterAsyncTask(TaskPort1);


            List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
            GetCrewTransaction(ref cres, 1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text),
                              GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                              GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                              GlobalCode.Field2Long(uoHiddenFieldSeqNo.Value),
                              GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value),
                              GlobalCode.Field2String(uoHiddenFieldPortCode.Value),
                              GlobalCode.Field2String(uoHiddenFieldArrCode.Value),
                              false, uoHiddenFieldUser.Value.ToString());
            if (cres.Count > 0)
            {

                LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking);
                LoadMeetAndGreetDetail(cres[0].CrewAssistMeetAndGreet);
                LoadPortAgentDetail(cres[0].CrewAssistPortAgentRequest);
                LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                LoadSafeguardRequestDetail(cres[0].CrewAssistSafeguardRequest);

                if (GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0)
                {
                    uoCheckBoxPAMAG.Checked = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : false;
                    uoCheckBoxPATrans.Checked = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : false;
                    uoCheckBoxPAHotel.Checked = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : false;
                    uoCheckBoxPASafeguard.Checked = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : false;

                    uoCheckBoxPAMAG.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : true;
                    uoCheckBoxPATrans.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : true;
                    uoCheckBoxPAHotel.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true;
                    uoCheckBoxPASafeguard.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : true;
                }
                uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                uoListViewTransportation.DataBind();


                uoListViewCompanionList.DataSource = cres[0].HotelRequestCompanion.Where(n => n.REQUESTID == GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value)).ToList();
                uoListViewCompanionList.DataBind();


            }


        }


        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    Page.LoadComplete += Page_LoadComplete;
        //}

        //protected virtual void Page_LoadComplete(object sender, EventArgs e)
        //{
        //    Page.LoadComplete -= Page_LoadComplete;
        //}

        public IAsyncResult OnBeginRefreshPage(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }

        public void OnEndRefreshPage(IAsyncResult ar)
        {
            _dlgtSeafarer.EndInvoke(ar);

            List<CrewAssistTransaction> cres = new List<CrewAssistTransaction>();
            GetCrewTransaction(ref cres, 1, GlobalCode.Field2Long(uoTextBoxEmployeeID.Text),
                              GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                              GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                              GlobalCode.Field2Long(uoHiddenFieldSeqNo.Value),
                              GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value),
                              GlobalCode.Field2String(uoHiddenFieldPortCode.Value),
                              GlobalCode.Field2String(uoHiddenFieldArrCode.Value),
                              false, uoHiddenFieldUser.Value.ToString());
            if (cres.Count > 0)
            {

                LoadHotelRequestDetail(cres[0].CrewAssistHotelBooking);
                LoadMeetAndGreetDetail(cres[0].CrewAssistMeetAndGreet);
                LoadPortAgentDetail(cres[0].CrewAssistPortAgentRequest);
                LoadTransprtationRequestDetail(cres[0].CrewAssistTranspo);
                LoadSafeguardRequestDetail(cres[0].CrewAssistSafeguardRequest);

                if (GlobalCode.Field2Long(uoHiddenFieldTransPortAgentID.Value) == 0)
                {
                    uoCheckBoxPAMAG.Checked = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : false;
                    uoCheckBoxPATrans.Checked = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : false;
                    uoCheckBoxPAHotel.Checked = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : false;
                    uoCheckBoxPASafeguard.Checked = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : false;

                    uoCheckBoxPAMAG.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransMeetAndGreetID.Value) > 0 ? false : true;
                    uoCheckBoxPATrans.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransTransapotationID.Value) > 0 ? false : true;
                    uoCheckBoxPAHotel.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransHotelOtherID.Value) > 0 ? false : true;
                    uoCheckBoxPASafeguard.Enabled = GlobalCode.Field2Long(uoHiddenFieldTransSafeguardID.Value) > 0 ? false : true;
                }
                uoListViewTransportation.DataSource = cres[0].CrewAssistTranspoApprove;
                uoListViewTransportation.DataBind();
            }
        }


        void InsertTransToSend()
        {
            try
            {

                string TransRecipient = "";
                string EmailTransAdd = uoTextBoxEmailTrans.Text.ToString();
                string[] separators = { ",", ";", " " };

                string[] TransAdd = EmailTransAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in TransAdd)
                {
                    TransRecipient += word + "; ";
                }



                if (EmailTransAdd == string.Empty)
                {
                    ExceptionMessage("Transportation email required!");
                    return;
                }


                string cssEmail = "";

                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                }

                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                }

                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                }
                if (uoTextBoxEmailOther.Text != "")
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }


                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {

                    ccrecipient += word + "; ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }

                string mes = "";
                List<CrewAssistTranspo> Trans = new List<CrewAssistTranspo>();
                if (GlobalCode.Field2Long(uoHiddenFieldTransVendorID.Value.ToString()) > 0 && TransRecipient.Length > 0 && uoCheckBoxEmailTrans.Checked == true)
                {
                    mes = SF.SendVehicleTransactionRequest(uoHiddenFieldTransVendorID.Value,
                    uoHiddenFieldUser.Value, TextBoxWhoConfirm.Text.Trim(),
                    uoTextBoxEmail.Text.ToString(), "Submit Hotel Request", "SubmitRequest", Path.GetFileName(Request.Path),
                    TransRecipient.Substring(0, TransRecipient.Length - 2).ToString(), ccrecipient, "",
                    uoDropDownListVehicleVendor.SelectedItem.Text.Substring(0, 2) == "VH" ? false : true);
                }

                if (mes != "" && TransRecipient.Length > 0 && uoCheckBoxEmailTrans.Checked == true)
                {
                    SendTransEmail("RCL Confirmation – Transportation Service Request", mes, "", TransRecipient.Substring(0, TransRecipient.Length - 2).ToString(), ccrecipient, TextBoxWhoConfirm.Text.ToString());
                    ExceptionMessage("Transportation Request has been sent successfully!");
                }
                if (mes == "")
                {
                    ExceptionMessage("Transportation Request has been sent successfully!");
                }
                refreshPage();
            }
            catch (Exception ex)
            {
                AlertMessage("InsertTransToSend: " + ex.Message);
            }
        }

        public void SendTransEmail(string sSubject, string sMessage, string attachment1, string EmailVendor, string EmailCc, string ComfirmBy)
        {
            CrewAssistEmail SF = new CrewAssistEmail();
            if (EmailVendor != "")
            {
                SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sMessage);
            }
        }

        void InsertMeetAndGreetToSend()
        {
            try
            {

                string MAGRecipient = "";

                string EmailMeetAndGreetAdd = uoTextBoxMAGEmail.Text.ToString();

                string[] separators = { ",", ";", " " };
                string[] MAGAdd = EmailMeetAndGreetAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in MAGAdd)
                {
                    MAGRecipient += word + "; ";
                }

                string cssEmail = "";
                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                }

                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                }

                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                }
                if (uoTextBoxEmailOther.Text != "")
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }


                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {

                    ccrecipient += word + "; ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }


                List<CrewAssistMeetAndGreet> magList = new List<CrewAssistMeetAndGreet>();
                if (GlobalCode.Field2Long(uoHiddenFieldMeetAndGreetID.Value) > 0 && MAGRecipient.Length > 0 && uoCheckBoxMAGEmail.Checked == true)
                {
                    SF = new CrewAssistBLL();
                    magList = SF.SendMeetAndGreetTransactionRequest(GlobalCode.Field2Long(uoHiddenFieldMeetAndGreetID.Value), uoHiddenFieldUser.Value);
                }

                if (magList.Count > 0 && MAGRecipient.Length > 0 && uoCheckBoxMAGEmail.Checked == true)
                {
                    SendMeetAndGreetEmail("RCL Confirmation – Meet and Greet Service Request", magList, "", MAGRecipient.Substring(0, MAGRecipient.Length - 2).ToString(), ccrecipient);
                    AlertMessage(" Meet and Greet Request has been sent successfully!");
                }

            }
            catch (Exception ex)
            {
                AlertMessage("InsertMeetAndGreetToSend: " + ex.Message);
            }
        }

        void insertPortAgentToSend()
        {
            try
            {

                string PARecipient = "";


                string EmaiPAAdd = uoTextBoxPAEmail.Text.ToString();

                string[] separators = { ",", ";", " " };
                string[] PAAdd = EmaiPAAdd.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in PAAdd)
                {
                    PARecipient += word + "; ";
                }


                string cssEmail = "";

                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value == "")
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + "; ";
                }

                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value == "")
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + "; ";
                }

                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value == "")
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + "; ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value == "")
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + "; ";
                }
                if (uoTextBoxEmailOther.Text != "")
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }


                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {

                    ccrecipient += word + "; ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }

                List<CrewAssistPortAgentRequest> PAList = new List<CrewAssistPortAgentRequest>();
                string mes = "";
                if (GlobalCode.Field2Long(uoHiddenFieldPortAgentID.Value) > 0 && PARecipient.Length > 0 && uoCheckBoxPAEmail.Checked == true)
                {

                    SF = new CrewAssistBLL();
                    mes = SF.SendPortAgentTransactionRequest(GlobalCode.Field2Long(uoHiddenFieldPortAgentID.Value), uoHiddenFieldUser.Value,
                        PARecipient.Substring(0, PARecipient.Length - 2).ToString(), ccrecipient, "");
                }

                if (mes != "" && PARecipient.Length > 0 && uoCheckBoxPAEmail.Checked == true)
                {
                    SendPortAgenEmail("RCL Confirmation – Port Agent Service Request", mes, "", PARecipient.Substring(0, PARecipient.Length - 2).ToString(), ccrecipient);
                    AlertMessage("Port Agent Request has been sent successfully!");
                }
                if (mes == "")
                {
                    AlertMessage("Port Agent Request has been sent successfully!");
                }
                //refreshPage();
            }
            catch (Exception ex)
            {
                AlertMessage("insertPortAgentToSend: " + ex.Message);
            }
        }


        public void SendPortAgenEmail(string sSubject, string sMessage, string attachment1, string EmailVendor, string EmailCc)
        {
            CrewAssistEmail SF = new CrewAssistEmail();
            if (EmailVendor != "")
            {
                SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sMessage);
            }
        }


        public void SendEmail(string sSubject, string sMessage, string attachment1, string EmailVendor, string EmailCc, string Hotel, string ComfirmBy)
        {
            CrewAssistEmail SF = new CrewAssistEmail();
            if (EmailVendor != "")
            {
                SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sMessage);
            }
        }


        public void SendEmail(string sSubject, List<CrewAssistEmailDetail> sMessage, string attachment1, string EmailVendor, string EmailCc, string Hotel, string ComfirmBy)
        {
            DataTable dt = null;
            try
            {

                string AirDetail = "";

                if (sMessage[0].CrewAssistEmailAirDetail.Count > 0)
                {
                    foreach (var list in sMessage[0].CrewAssistEmailAirDetail)
                    {
                        AirDetail += list.AirDetail + "<br>";
                    }
                }

                //<font color=\"#0288D8\" face=\"Eras Medium ITC\">
                string Header = "https://www.rclcrew.com/images/CAEmailHearder.jpg";
                string Footer = "https://www.rclcrew.com/images/CAEmailFooter.jpg";

                string sBody = "<html><body style=\"width:100%;\" ><table style=\"width:100%;\" >";
                sBody += "<tr style=\"width:100%;\" ><td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailHearder.jpg\" style=\"width:100%; height:100px;\" /></td></tr>";
                sBody += "<tr><td colspan=\"4\">Greetings &nbsp;" + sMessage[0].VendorBranch + "! </td></tr>";
                sBody += "<tr><td colspan=\"4\">We are pleased to confirm our request for transportation service as indicated below: <br/><br/><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td  style=\"width:175px;\"><font color=\"#0288D8\" > <b>Employee Information</b>&nbsp;</td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"1\" style=\"width:70px\">Name :";
                //sBody += sMessage[0].FirstName + " " + sMessage[0].LastName + "</td>";
                sBody += "<td colspan=\"3\">" + sMessage[0].FirstName + " " + sMessage[0].LastName + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Employee ID :</td>";
                sBody += "<td>" + sMessage[0].SeafarerID + "</td>";
                sBody += "<td style=\"width:70px\">Position :</td>";
                sBody += "<td>" + sMessage[0].RankName + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Gender :</td>";
                sBody += "<td>" + sMessage[0].GenderDiscription + "</td>";
                sBody += "<td style=\"width:70px\">Cost Center :</td>";
                sBody += "<td>" + sMessage[0].CostCenterCode + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Nationality :</td>";
                sBody += "<td>" + sMessage[0].Nationality + "</td>";
                sBody += "<td style=\"width:70px\">Ship :</td>";
                sBody += "<td>" + sMessage[0].VesselName + "</td></td>";
                sBody += "</tr>";

                sBody += "<tr><td><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td style=\"width:155px;\"><font color=\"#0288D8\"><b>Hotel/Room Details&nbsp;</b></font></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Location :</td>";
                sBody += "<td >Hotel Location</td>";
                sBody += "<td style=\"width:70px\">Check In :</td>";
                sBody += "<td>" + sMessage[0].TimeSpanStartDate + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Number of nights :</td>";
                sBody += "<td>" + sMessage[0].NoOfNite + "</td>";
                sBody += "<td style=\"width:70px\">Check Out :</td>";
                sBody += "<td>" + sMessage[0].TimeSpanEndDate + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Room Occupancy :</td>";
                sBody += "<td>" + sMessage[0].RoomDesc + "</td>";
                sBody += "<td style=\"width:70px\">Meal Vouche :</td>";
                sBody += "<td>" + sMessage[0].Mealvoucheramount + "</td></td>";
                sBody += "</tr>";

                sBody += "<tr><td><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td style=\"width:80px;\" ><font color=\"#0288D8\" ><b>Comment&nbsp;</b></font></td>";
                //sBody += "<td style=\"width:155px;\"><font color=\"#0288D8\"><b>Hotel/Room Details&nbsp;</b></font></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr><td style=\"height:50px;\" colspan=\"4\">" + sMessage[0].Comment + "</td></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td style=\"width:105px;\" ><label style=\"width:105px; font-size:larger;\">&nbsp;Flight Details&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"2\" style=\"border:solid thin Black;\">" + sMessage[0].RecordLocator + "<br/><br/>" + AirDetail + "</td>";
                sBody += "<td colspan=\"2\" style=\"background-color:#0288D8; color:White;\">If you have any questions or need additional <br/> information, please contact: <br/> <br/><b>CrewAssist</b> <br/>Phone:<font color=\"White\">  1-877-414-2739 </font> <br/> Email:<font color=\"black\">  CrewAssist@rccl.com.</font> </td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Confirmed by :&nbsp;" + sMessage[0].ConfirmedbyRCCL + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Thank you! <br/> <br/> <b>Note:</b>&nbsp; This service request confirmation may be used for billing purposes. </td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailFooter_Logo.jpg\" style=\"width:100%; height:100px;\"/></td>";

                sBody += "</html></body></table>";



                CrewAssistEmail SF = new CrewAssistEmail();

                if (EmailVendor != "")
                {
                    SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sBody);
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SendEmail: " + ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        public void SendTransEmail(string sSubject, List<CrewAssistTranspo> sMessage, string attachment1, string EmailVendor, string EmailCc, string ComfirmBy)
        {
            DataTable dt = null;
            try
            {
                string Header = "https://www.rclcrew.com/images/CAEmailHearder.jpg";
                string Footer = "https://www.rclcrew.com/images/CAEmailFooter.jpg";


                string sBody = "<html><body style=\"width:100%;\" ><table style=\"width:100%;\" >";
                sBody += "<tr style=\"width:100%;\" ><td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailHearder.jpg\" style=\"width:100%; height:100px;\" /></td></tr>";
                sBody += "<tr><td colspan=\"4\">Greetings &nbsp;" + sMessage[0].VehicleVendor + "! </td></tr>";
                sBody += "<tr><td colspan=\"4\">We are pleased to confirm our request for transportation service as indicated below: <br/><br/><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td  style=\"width:175px;\"><label style=\"background-color:#0288D8; width:175px; font-size:larger;\">&nbsp;Employee Information&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Name :</td>";
                sBody += "<td colspan=\"3\">" + sMessage[0].FirstName + " " + sMessage[0].FirstName + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Employee ID :</td>";
                sBody += "<td>" + sMessage[0].SeaparerID + "</td>";
                sBody += "<td style=\"width:70px\">Position :</td>";
                sBody += "<td>" + sMessage[0].RankName + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Gender :</td>";
                sBody += "<td>" + sMessage[0].Gender + "</td>";
                sBody += "<td style=\"width:70px\">Cost Center :</td>";
                sBody += "<td>" + sMessage[0].CostCenter + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Nationality :</td>";
                sBody += "<td>" + sMessage[0].NationalityName + "</td>";
                sBody += "<td style=\"width:70px\">Ship :</td>";
                sBody += "<td>" + sMessage[0].Ship + "</td></td>";
                sBody += "</tr>";

                sBody += "<tr><td><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td style=\"width:155px;\"><label style=\"background-color:#0288D8; width:155px; font-size:larger;\">&nbsp;Transport Details&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";
                //String.Format("{0:dd MMMM yyyy}", GlobalCode.Field2DateTime(a["TimeSpanStartDate"])),

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Pick-up Date :</td>";
                sBody += "<td>" + String.Format("{0:dd MMMM yyyy}", sMessage[0].PickUpDate) + "</td>";
                sBody += "<td style=\"width:70px\">From :</td>";
                sBody += "<td>" + sMessage[0].RouteFrom + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Pick-up time :</td>";
                sBody += "<td>" + String.Format("{0:hh:mm:ss tt}", sMessage[0].PickUpTime) + "</td>";
                sBody += "<td style=\"width:70px\">To :</td>";
                sBody += "<td>" + sMessage[0].RouteTo + "</td>";
                sBody += "</tr>";

                sBody += "<tr><td><br/></td></tr>";


                sBody += "<tr>";
                sBody += "<td style=\"height:1px;\" colspan=\"1\">";
                sBody += "<label style=\"background-color:#0288D8; font-size:larger;\">&nbsp;Comment&nbsp;</label>";
                sBody += "&nbsp;&nbsp;<div style=\"background-color:#0288D8; width:100%; height:2px;\"/>";
                sBody += "</td>";
                //sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                //sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"2\" style=\"border:solid thin Black;\">" + sMessage[0].Comment + "</td>";
                sBody += "<td colspan=\"2\" style=\"background-color:#0288D8;  color:White;\">If you have any questions or need additional <br/> information, please contact: <br/> <br/><b>CrewAssist</b> <br/>Phone: <font color=\"black\">  1-877-414-2739 </font>  <br/> Email:  <font color=\"black\">  CrewAssist@rccl.com. </font></label></td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Comfirm by :&nbsp;" + sMessage[0].ConfirmBy + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Thank you! <br/> <br/> <b>Note:</b>&nbsp; This service request confirmation may be used for billing purposes. </td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailFooter_Logo.jpg\" style=\"width:100%; height:100px;\"/></td>";
                sBody += "</tr>";

                sBody += "</html></body></table>";


                CrewAssistEmail SF = new CrewAssistEmail();
                if (EmailVendor != "")
                {
                    SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sBody);
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SendTransEmail: " + ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }


        public void SendMeetAndGreetEmail(string sSubject, List<CrewAssistMeetAndGreet> sMessage, string attachment1, string EmailVendor, string EmailCc)
        {
            DataTable dt = null;
            try
            {

                string sBody = "<html><body style=\"width:100%;\" ><table style=\"width:100%;\" >";
                sBody += "<tr style=\"width:100%;\" ><td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailHearder.jpg\" style=\"width:100%; height:100px;\" /></td></tr>";
                sBody += "<tr><td colspan=\"4\">Greetings &nbsp;" + sMessage[0].MeetAndGreetVendor + "! </td></tr>";
                sBody += "<tr><td colspan=\"4\">We are pleased to confirm our request for transportation service as indicated below: <br/><br/><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td  style=\"width:175px;\"><label style=\"background-color:#0288D8; width:175px; font-size:larger;\">&nbsp;Employee Information&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Name :</td>";
                sBody += "<td colspan=\"3\">" + sMessage[0].FirstName + " " + sMessage[0].LastName + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Employee ID :</td>";
                sBody += "<td>" + sMessage[0].SeaparerID + "</td>";
                sBody += "<td style=\"width:70px\">Position :</td>";
                sBody += "<td>" + sMessage[0].RankName + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Gender :</td>";
                sBody += "<td>" + sMessage[0].Gender + "</td>";
                sBody += "<td style=\"width:70px\">Cost Center :</td>";
                sBody += "<td>" + sMessage[0].CostCenter + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Nationality :</td>";
                sBody += "<td>" + sMessage[0].NationalityName + "</td>";
                sBody += "<td style=\"width:70px\">Ship :</td>";
                sBody += "<td>" + sMessage[0].Ship + "</td></td>";
                sBody += "</tr>";

                sBody += "<tr><td><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td style=\"width:200px;\"><label style=\"background-color:#0288D8; width:155px; font-size:larger;\">&nbsp;Meet And Greet Details&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td>Airport :&nbsp;" + sMessage[0].AirportCode + "</td>";
                sBody += "<td>Service Date :&nbsp;" + GlobalCode.Field2Date(sMessage[0].ServiceDate) + "</td>";
                sBody += "<td colspan=\"2;\">Rate :&nbsp;" + sMessage[0].Rate + "</td>";
                sBody += "</tr>";


                sBody += "<tr><td><br/></td></tr>";


                sBody += "<tr>";
                sBody += "<td style=\"height:1px;\" colspan=\"1\">";
                sBody += "<label style=\"background-color:#0288D8; font-size:larger;\">&nbsp;Comment&nbsp;</label>";
                sBody += "&nbsp;&nbsp;<div style=\"background-color:#0288D8; width:100%; height:2px;\"/>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"2\" style=\"border:solid thin Black;\">" + sMessage[0].Comment + "</td>";
                sBody += "<td colspan=\"2\" style=\"background-color:#0288D8;  color:White;\">If you have any questions or need additional <br/> information, please contact: <br/> <br/><b>CrewAssist</b> <br/>Phone: <font color=\"black\">  1-877-414-2739 </font>  <br/> Email:  <font color=\"black\">  CrewAssist@rccl.com. </font></label></td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Comfirm by :&nbsp;" + sMessage[0].ConfirmBy + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Thank you! <br/> <br/> <b>Note:</b>&nbsp; This service request confirmation may be used for billing purposes. </td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailFooter_Logo.jpg\" style=\"width:100%; height:100px;\"/></td>";
                sBody += "</tr>";

                sBody += "</html></body></table>";


                CrewAssistEmail SF = new CrewAssistEmail();
                if (EmailVendor != "")
                {
                    SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sBody);
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SendMeetAndGreetEmail: " + ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }


        public void SendPortAgenEmail(string sSubject, List<CrewAssistPortAgentRequest> sMessage, string attachment1, string EmailVendor, string EmailCc)
        {
            //string sBody;
            DataTable dt = null;
            try
            {

                //string MAGService = sMessage[0].IsMAG == true ? "Checked=\"true\"" : "Checked=\"false\"";
                //string HotelService = sMessage[0].IsHotel == true ? "Checked=\"true\"" : "Checked=\"false\"";
                //string TransService = sMessage[0].IsTrans == true ? "Checked=\"true\"" : "Checked=\"false\"";
                //string LuggageService = sMessage[0].IsLuggage == true ? "Checked=\"true\"" : "Checked=\"false\"";

                //string SafeguardService = sMessage[0].IsSafeguard == true ? "Checked=\"true\"" : "Checked=\"false\"";
                //string VisaService = sMessage[0].IsVisa == true ? "Checked=\"true\"" : "Checked=\"false\"";
                //string OtherService = sMessage[0].IsOther == true ? "Checked=\"true\"" : "Checked=\"false\"";

                string PAYes = "<img src=\"https://rclcrew.com/images/positive.png\" style=\"width:20px; height:20px;\"/>";
                string PANo = "<img src=\"https://rclcrew.com/images/positive_circle.png\" style=\"width:20px; height:20px;\"/>";

                string ServiceMAG = sMessage[0].IsMAG == true ? PAYes : PANo;
                string ServiceHotel = sMessage[0].IsHotel == true ? PAYes : PANo;
                string ServiceTrans = sMessage[0].IsTrans == true ? PAYes : PANo;
                string ServiceLuggage = sMessage[0].IsLuggage == true ? PAYes : PANo;

                string ServiceSafeguard = sMessage[0].IsSafeguard == true ? PAYes : PANo;
                string ServiceVisa = sMessage[0].IsVisa == true ? PAYes : PANo;
                string ServiceOther = sMessage[0].IsOther == true ? PAYes : PANo;





                string sBody = "<html><body style=\"width:100%;\" ><table style=\"width:100%;\" >";
                sBody += "<tr style=\"width:100%;\" ><td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailHearder.jpg\" style=\"width:100%; height:100px;\" /></td></tr>";
                sBody += "<tr><td colspan=\"4\">Greetings &nbsp;" + sMessage[0].PortAgentVendorName + "! </td></tr>";
                sBody += "<tr><td colspan=\"4\">We are pleased to confirm our request for transportation service as indicated below: <br/><br/><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td  ><label style=\"background-color:#0288D8; width:178px; font-size:larger;\">&nbsp;Employee Information&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\" colspan=\"3\">Name:&nbsp" + uoTextBoxFirstName.Text.ToString() + " " + uoTextBoxLastName.Text.ToString() + "</td>";
                //sBody += "<td colspan=\"3\">" + uoTextBoxFirstName.Text.ToString() + " " + uoTextBoxLastName.Text.ToString() + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\" colspan=\"2\">Employee ID:&nbsp" + uoTextBoxEmployeeID.Text.ToString() + " </td>";
                sBody += "<td style=\"width:70px\" colspan=\"2\">Position:&nbsp" + uoTextBoxRank.Text.ToString() + "</td>";
                sBody += "</tr>";




                sBody += "<tr>";
                sBody += "<td style=\"width:70px\" colspan=\"2\">Gender:&nbsp" + uoTextBoxGender.Text.ToString() + "</td>";
                sBody += "<td style=\"width:70px\" colspan=\"2\">Cost Center:&nbsp</td>";
                sBody += "<td> </td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td style=\"width:70px\">Nationality:&nbsp" + uoTextBoxNationality.Text.ToString() + "</td>";
                sBody += "<td style=\"width:70px\">Ship:" + uoTextBoxShip.Text.ToString() + "</td>";
                sBody += "</tr>";

                sBody += "<tr><td><br/></td></tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">";
                sBody += "<table style=\"width:100%;\"><tr style=\"width:100%;\">";
                sBody += "<td ><label style=\"background-color:#0288D8; font-size:larger;\">&nbsp;Services&nbsp;</label></td>";
                sBody += "<td  colspan=\"3\" align=\"center\"><div style=\"background-color:#0288D8; width:100%; height:2px;\"/></td>";
                sBody += "</tr></table>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\" >";
                sBody += "<table>";

                sBody += "<tr>";
                sBody += "<td>" + ServiceMAG + "&nbsp Meet And Greet &nbsp&nbsp&nbsp" + "</td>";
                sBody += "<td>" + ServiceHotel + "&nbsp Hotel&nbsp&nbsp&nbsp" + "</td>";
                sBody += "<td>" + ServiceTrans + "&nbsp Transportation&nbsp&nbsp&nbsp" + "</td>";
                sBody += "<td>" + ServiceLuggage + "&nbsp Luggage&nbsp&nbsp&nbsp" + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td>" + ServiceSafeguard + "&nbsp Safeguard&nbsp&nbsp&nbsp" + "</td>";
                sBody += "<td>" + ServiceVisa + "&nbsp Visa&nbsp&nbsp&nbsp" + "</td>";
                sBody += "<td>" + ServiceOther + "&nbsp Other&nbsp&nbsp&nbsp" + "</td>";
                sBody += "</tr>";

                sBody += "</table>";
                sBody += "</td>";
                sBody += "</tr>";


                sBody += "<tr><td><br/></td></tr>";


                sBody += "<tr>";
                sBody += "<td style=\"height:1px;\" colspan=\"4\">";
                sBody += "<label style=\"background-color:#0288D8; font-size:larger;\">&nbsp;Comment&nbsp;</label>";
                sBody += "&nbsp;<div style=\"background-color:#0288D8; width:100%; height:2px;\"/>";
                sBody += "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"2\" style=\"border:solid thin Black;\">" + sMessage[0].Comment + "</td>";
                sBody += "<td colspan=\"2\" style=\"background-color:#0288D8;  color:White;\">If you have any questions or need additional <br/> information, please contact: <br/> <br/><b>CrewAssist</b> <br/>Phone: <font color=\"black\">  1-877-414-2739 </font>  <br/> Email:  <font color=\"black\">  CrewAssist@rccl.com. </font></label></td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Comfirm by :&nbsp;" + sMessage[0].ConfirmBy + "</td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\">Thank you! <br/> <br/> <b>Note:</b>&nbsp; This service request confirmation may be used for billing purposes. </td>";
                sBody += "</tr>";

                sBody += "<tr>";
                sBody += "<td colspan=\"4\" style=\"width:100%;\" ><img src=\"https://rclcrew.com/images/CAEmailFooter_Logo.jpg\" style=\"width:100%; height:100px;\"/></td>";
                sBody += "</tr>";

                sBody += "</html></body></table>";


                CrewAssistEmail SF = new CrewAssistEmail();
                if (EmailVendor != "")
                {
                    SF.SendEmail("RCLCrewTravelmart@gmail.com", EmailVendor, EmailCc, sSubject, sBody);
                }

            }
            catch (Exception ex)
            {
                AlertMessage("SendPortAgenEmail: " + ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Closed_Click(object sender, EventArgs e)
        {
            HiddenFieldHideCenter.Value = "0";
        }

        #endregion

    }     

}

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

namespace TRAVELMART
{
    public partial class CrewAssistMedical : System.Web.UI.Page
    {

        //wali test
        private AsyncTaskDelegate _dlgtSeafarer;

        // Create delegate. 
        protected delegate void AsyncTaskDelegate();
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
            //ScriptManager.RegisterStartupScript(Page, GetType(), "key", sScript, true);


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["strPrevPage"] = Request.RawUrl;

            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                uoTextBoxCheckinDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");

                uoTextBoxCheckinDate.Text = DateTime.Now.AddDays(1).Date.ToString("MM/dd/yyyy");
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
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                //BindListViewTravel();

                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);

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

                if (uoListViewTranspoCost.Items.Count <= 0)
                {
                    uoListViewTranspoCost.DataSource = null;
                    uoListViewTranspoCost.DataBind();
                }

            }

            //if (uoDropDownListHotel.Items.Count > 0)
            //{
            //    if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
            //    {
            //        rowPortAgent.Visible = true;
            //    }
            //    else
            //    {
            //        rowPortAgent.Visible = false;
            //    }
            //}
            //else
            //{
            //    rowPortAgent.Visible = false;
            //}
        }


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

            ClearHotelObject(1);
            ClearMeetAndGreet();
            ClearObjectPortAgent(false);

            ClearObjectTrans();
            BindListViewTravel();

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

                CrewAssistBLL SF = new CrewAssistBLL();
                list = SF.GetGetHotelPortExpendTypeList(uoHiddenFieldUser.Value, "0", "0");

                uoDropDownListHotel.Items.Clear();
                uoDropDownListPort.Items.Clear();
                uoDropDownListExpenseType.Items.Clear();

                Session["CrewAssistVehicleVendor"] = null;

                int iRowCount = list.Count;
                if (iRowCount > 0)
                {

                    uoDropDownListHotel.DataSource = list[0].CrewAssistHotelList;
                    uoDropDownListHotel.DataTextField = "HotelName";
                    uoDropDownListHotel.DataValueField = "HotelID";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));


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

                    //if (list[0].CrewAssitExpendTypeList.Count > 0)
                    //{
                    //    uoDropDownListExpenseType.Items.FindByValue("1").Selected = true;
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);
                    //}

                    uoDropDownListCurrency.DataSource = list[0].CrewAssistCurrency;
                    uoDropDownListCurrency.DataTextField = "CurrencyName";
                    uoDropDownListCurrency.DataValueField = "CurrencyID";
                    uoDropDownListCurrency.DataBind();



                    uoDropDownListMAGTo.Items.Clear();
                    uoDropDownListMAGTo.Items.Add(new ListItem("--Select To--", "0"));
                    uoDropDownListMAGTo.DataSource = list[0].CrewAssistRout;
                    uoDropDownListMAGTo.DataTextField = "RoutName";
                    uoDropDownListMAGTo.DataValueField = "RoutId";
                    uoDropDownListMAGTo.DataBind();


                    ListView mylist = ((ListView)uoListViewTranportationRoute);
                    ListViewItem lvi = null;
                    if (mylist.Controls.Count == 1)
                        lvi = mylist.Controls[0] as ListViewItem;

                    uoDropDownListVehicleVendor.DataSource = null;
                    uoDropDownListVehicleVendor.Items.Clear();

                    uoDropDownListVehicleVendor.DataSource = list[0].VehicleVendor;
                    uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                    uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                    uoDropDownListVehicleVendor.DataBind();
                    uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

                    uoDropDownListMAndGVendor.DataSource = null;
                    uoDropDownListMAndGVendor.Items.Clear();

                    uoDropDownListMAndGVendor.DataSource = list[0].CrewAssistMeetAndGreetVendor;
                    uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                    uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                    uoDropDownListMAndGVendor.DataBind();
                    uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));


                    uoDropDownListPortAgent.DataSource = list[0].CrewAssistVendorPortAgent;
                    uoDropDownListPortAgent.DataTextField = "PortAgentVendorName";
                    uoDropDownListPortAgent.DataValueField = "PortAgentVendorID";
                    uoDropDownListPortAgent.DataBind();
                    uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider--", "0"));


                    cboRemarkType.Items.Clear();
                    cboRemarkType.Items.Add(new ListItem("--Select Type--", "0"));
                    cboRemarkType.DataSource = list[0].RemarkType;
                    cboRemarkType.DataTextField = "RemarkType";
                    cboRemarkType.DataValueField = "RemarkTypeID";
                    cboRemarkType.DataBind();

                    cboRemarkStatus.Items.Clear();
                    cboRemarkStatus.Items.Add(new ListItem("--Select Remark Status--", "0"));
                    cboRemarkStatus.DataSource = list[0].RemarkStatus;
                    cboRemarkStatus.DataTextField = "RemarkType";
                    cboRemarkStatus.DataValueField = "RemarkTypeID";
                    cboRemarkStatus.DataBind();

                    cboRemarkRequestor.Items.Clear();
                    cboRemarkRequestor.Items.Add(new ListItem("--Select Remark Requestor--", "0"));
                    cboRemarkRequestor.DataSource = list[0].RemarkRequestor;
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




        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CrewAssistsBLL BLL = new CrewAssistsBLL();
                if (uoDropDownListPort.SelectedIndex == 0)
                {
                    LoadAllCombo();
                    return;
                }
                GetGenericVendors(BLL.GetGenericVendors(0, uoHiddenFieldUser.Value, GlobalCode.Field2Int(uoDropDownListPort.SelectedValue), GlobalCode.Field2Int(uoDropDownListPort.SelectedValue)));
            }
            catch (Exception ex)
            {
                AlertMessage("DownListPort: " + ex.Message);
            }
        }

        void GetGenericVendors(List<CrewAssistGenericVendor> list)
        {
            try
            {
                uoDropDownListHotel.DataSource = null;
                uoDropDownListHotel.Items.Clear();

                uoDropDownListHotel.DataSource = list[0].listHotel;
                uoDropDownListHotel.DataTextField = "HotelName";
                uoDropDownListHotel.DataValueField = "HotelID";
                uoDropDownListHotel.DataBind();
                uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));

                uoDropDownListVehicleVendor.DataSource = null;
                uoDropDownListVehicleVendor.Items.Clear();

                uoDropDownListVehicleVendor.DataSource = list[0].listVehicle;
                uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                uoDropDownListVehicleVendor.DataBind();
                uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));

                uoDropDownListMAndGVendor.DataSource = null;
                uoDropDownListMAndGVendor.Items.Clear();

                uoDropDownListMAndGVendor.DataSource = list[0].listMeetGreet;
                uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendorName";
                uoDropDownListMAndGVendor.DataValueField = "MeetandGreetVendorId";
                uoDropDownListMAndGVendor.DataBind();
                uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));


                uoDropDownListPortAgent.DataSource = null;
                uoDropDownListPortAgent.Items.Clear();

                uoDropDownListPortAgent.DataSource = list[0].listPortAgent;
                uoDropDownListPortAgent.DataTextField = "PortAgentName";
                uoDropDownListPortAgent.DataValueField = "PortAgentID";
                uoDropDownListPortAgent.DataBind();
                uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider--", "0"));



            }
            catch (Exception ex)
            {
                AlertMessage("DownListPort: " + ex.Message);
            }

        }



        void ClearObjectTrans()
        {
            uoTextBoxTransComment.Text = "";
            uoDropDownListVehicleVendor.SelectedIndex = -1;
            uoCheckBoxEmailTrans.Checked = false;

            uoTextBoxEmailTrans.Text = ""; ;
            uoTextBoxEmailTrans.Enabled = true;
            uoTextBoxTranpComfirmby.Text = "";

            uoLabelVehicleAddress.Text = "";
            uoLabelVehicleTelephone.Text = "";

            uoListViewTranportationRoute.DataSource = null;
            uoListViewTranportationRoute.DataBind();

            uoListViewTransportation.DataSource = null;
            uoListViewTransportation.DataBind();

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




        }


        /// <summary>
        // Date Modified:  25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add uoDropDownListHotel.SelectedIndex = -1;; to clear hotel
        /// </summary>
        void ClearHotelObject(int val)
        {
            if (val == 1) uoDropDownListHotel.SelectedIndex = -1;

            uoHiddenFieldHotelTransID.Value = "";
            uoTextBoxEmail.Text = "";
            uoTextBoxCheckinDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            uoTextBoxCheckoutDate.Text = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
            uoTxtBoxTimeIn.Text = ""; ;
            uoTxtBoxTimeOut.Text = ""; ;
            uoTextContractedRate.Text = "";
            uoTextBoxMealVoucher.Text = "";
            uoTextBoxComfirmRate.Text = "";
            uoTextBoxComment.Text = "";
            uoTextBoxDuration.Text = "";

            uoLabelAdress.Text = "";
            uoLabelTelephone.Text = "";


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

        private void BindListViewTravel()
        {



            uoListviewTravel.DataSource = null;
            uoListviewTravel.DataBind();

            uoListviewAir.DataSource = null;
            uoListviewAir.DataBind();

            uoListViewHotelBook.DataSource = null;
            uoListViewHotelBook.DataBind();

            uoListViewTransportation.DataBind();
            uoListViewTransportation.DataBind();

            uoListViewTranportationRoute.DataSource = null;
            uoListViewTranportationRoute.DataBind();


            uoListViewTranspoCost.DataSource = null;
            uoListViewTranspoCost.DataBind();


            uoListViewCompanionList.DataSource = null;
            uoListViewCompanionList.DataBind();




        }

        protected void uoButtonSearch_click(object sender, EventArgs e)
        {
            SearchCrewMember(GlobalCode.Field2Long(txtEmployeeID.Text));
        }
        private void SearchCrewMember(long SeafarerID)
        {
            try
            { 
                if (SeafarerID == 0) return;
                MedicalBLL BLL = new MedicalBLL();
                List<CrewMemberInformation> lst = new List<CrewMemberInformation>();
                lst = BLL.GetHotelTransactionMedical(0, SeafarerID, uoHiddenFieldUser.Value.ToString());
                if (lst.Count > 0)
                {

                    txtEmployeeID.Text = lst[0].SeafarerID.ToString();
                    txtName.Text = lst[0].Name;
                    txtNationality.Text = lst[0].Nationality;
                    txtGender.Text = lst[0].Gender;

                    uoListviewTravel.DataSource = lst[0].CrewSchedule;
                    uoListviewTravel.DataBind();


                    uoListViewRemark.DataSource = lst[0].Remark;
                    uoListViewRemark.DataBind();

                    uoListViewRemarkPopup.DataSource = lst[0].Remark;
                    uoListViewRemarkPopup.DataBind();

                    List<CrewAssistCMTransaction> CrewAssistCM = new List<CrewAssistCMTransaction>();
                    ProcessCrewAssistCMTransaction(CrewAssistCM);

                    uoListViewHotelBook.DataSource = lst[0].HotelTransactionMedical;
                    uoListViewHotelBook.DataBind();

                    uoListViewTransportation.DataSource = lst[0].VehicleTransactionMedical;
                    uoListViewTransportation.DataBind();

                }
            }
            catch(Exception ex) 
            {
                    throw ex;
            }
        }


        //protected void ButtonDeleteRemark_click(object sender, EventArgs e)
        //{
        //    //uoHiddenFieldTravelRequestID
        //    List<CrewAssisRemark> Lst = new List<CrewAssisRemark>();

        //    //if (GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value) == 0) return;

        //    //uoListViewRemarkPopup.DataSource = null;
        //    //uoListViewRemarkPopup.DataBind();

        //    //uoListViewRemark.DataSource = null;
        //    //uoListViewRemark.DataBind();



        //    //CrewAssistBLL CA = new CrewAssistBLL();
        //    //Lst = CA.DeleteCrewAssistRemarks(GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value),
        //    //         GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value), txtRemark.Text.ToString(), uoHiddenFieldUser.Value.ToString());

        //    uoListViewRemarkPopup.DataSource = Lst;
        //    uoListViewRemarkPopup.DataBind();

        //    uoListViewRemark.DataSource = Lst;
        //    uoListViewRemark.DataBind();

        //    txtRemark.Text = "";

        //}




        protected void uoDropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string strLanguage = Request.UserLanguages[0];
                System.Globalization.CultureInfo currentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(strLanguage);
                string dateformat = currentCulture.DateTimeFormat.ShortDatePattern;

                if (uoHiddenFieldFlightStatus.Value == "false") return;

                CrewAssistsBLL SF = new CrewAssistsBLL();
                DropDownList dList = (DropDownList)sender;

                string val = dList.SelectedValue;


                DropDownList ddlListFind = (DropDownList)sender;
                ListViewItem item1 = (ListViewItem)ddlListFind.NamingContainer;

                DropDownList GetDDLList = (DropDownList)item1.FindControl("uoDropDownListStatus");

                Label lblSeqNo = (Label)item1.FindControl("lblSeqNo");
                HiddenField hfIDBigInt = (HiddenField)item1.FindControl("hfIDBigInt");
                HiddenField OldStatus = (HiddenField)item1.FindControl("uoHiddenFieldStatus");
                HiddenField uoHiddenFieldTravelReqID = (HiddenField)item1.FindControl("uoHiddenFieldTravelReqID");

                foreach (ListViewDataItem item in uoListviewAir.Items)
                {
                    Label lblMessage = (Label)item1.FindControl("lblStatus");
                    lblMessage.Text = ddlListFind.SelectedItem.Text;

                }

                ProcessCrewAssistCMTransaction(SF.InsertAirTransactionStatus(GlobalCode.Field2Long(uoHiddenFieldTravelReqID.Value)
                        , GlobalCode.Field2Long(hfIDBigInt.Value), GlobalCode.Field2Int(lblSeqNo.Text)
                        , GlobalCode.Field2Int(ddlListFind.SelectedItem.Value)
                        , GlobalCode.Field2String(OldStatus.Value)
                        , GlobalCode.Field2String(uoHiddenFieldUser.Value)));


            }
            catch (Exception ex)
            {
                AlertMessage(ex.ToString());
            }
        }



        protected void uoSelectCheckBoxs_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            ListViewItem item = (ListViewItem)cb.NamingContainer;
            ListViewDataItem dataItem = (ListViewDataItem)item;

            CrewAssistsBLL BLL = new CrewAssistsBLL();

            HiddenField hfTravelRequestID = (HiddenField)item.FindControl("hfTravelRequestID");
            HiddenField HIdBigint = (HiddenField)item.FindControl("uoHiddenFieldHIDBigint");
            HiddenField HSeqNoInt = (HiddenField)item.FindControl("uoHiddenFieldSeqNo");

            HiddenField uoHiddenFieldLongShip = (HiddenField)item.FindControl("uoHiddenFieldLongShip");
            HiddenField uoHiddenFieldRank = (HiddenField)item.FindControl("uoHiddenFieldRank");
            HiddenField uoHiddenFieldBrand = (HiddenField)item.FindControl("uoHiddenFieldBrand");





            Label lblRequestDate = (Label)item.FindControl("lblRequestDate");
            Label lblPort = (Label)item.FindControl("lblPort");
            HiddenField hfPortID = (HiddenField)item.FindControl("hfPortID");
            uoDropDownListPort.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPort, GlobalCode.Field2Int(hfPortID.Value));
            Label lblStatus = (Label)item.FindControl("lblStatus");
            Label lblLabelReasoneCode = (Label)item.FindControl("lblLabelReasoneCode");

            txtBrand.Text = uoHiddenFieldBrand.Value;
            txtRank.Text = uoHiddenFieldRank.Value;
            txtShip.Text = uoHiddenFieldLongShip.Value;

            txtReasonCode.Text = lblLabelReasoneCode.Text;
            txtStatus.Text = lblStatus.Text;



            List<CrewAssistCMTransaction> CrewAssistCMTransaction = new List<CrewAssistCMTransaction>();
            CrewAssistCMTransaction = BLL.GetCrewMemberTransaction(0, GlobalCode.Field2Long(txtEmployeeID.Text),
                GlobalCode.Field2Long(hfTravelRequestID.Value), GlobalCode.Field2Long(HIdBigint.Value)
                , GlobalCode.Field2Int(HSeqNoInt.Value), GlobalCode.Field2DateTime(lblRequestDate.Text), lblPort.Text, lblPort.Text, false, uoHiddenFieldUser.Value);

            ProcessCrewAssistCMTransaction(CrewAssistCMTransaction);
            uoHiddenFieldTravelRequestID.Value = hfTravelRequestID.Value;

            foreach (ListViewItem items in uoListviewTravel.Items)
            {
                CheckBox uoSelectCheckBoxs = (CheckBox)items.FindControl("uoSelectCheckBoxs");
                if (items.ID != item.ID)
                    uoSelectCheckBoxs.Checked = false;

            }
        }

        private void ProcessCrewAssistCMTransaction(List<CrewAssistCMTransaction> CrewAssistCMTransaction)
        {

            if (CrewAssistCMTransaction.Count > 0)
            {


                if (CrewAssistCMTransaction[0].CrewAssistGenericVendor.Count > 0)
                {
                    GetGenericVendors(CrewAssistCMTransaction[0].CrewAssistGenericVendor);
                }

                uoListViewHotelBook.DataSource = CrewAssistCMTransaction[0].CrewAssistHotelBooking;
                uoListViewHotelBook.DataBind();

                //LoadHotelRequestDetail(CrewAssistCMTransaction[0].CrewAssistHotelBooking);
                LoadTransprtationRequestDetail(CrewAssistCMTransaction[0].CrewAssistTranspo);



                uoListViewTransportation.DataSource = CrewAssistCMTransaction[0].CrewAssistTranspo;
                uoListViewTransportation.DataBind();

                uoListViewTranportationRoute.DataSource = CrewAssistCMTransaction[0].CrewAssistTranspo;
                uoListViewTranportationRoute.DataBind();

                if (CrewAssistCMTransaction[0].CrewAssistTranspo.Count > 0)
                {
                    uoListViewTranspoCost.DataSource = CrewAssistCMTransaction[0].CrewAssistTranspo[0].VehicleContract;
                    uoListViewTranspoCost.DataBind();
                }
                else
                {
                    uoListViewTranspoCost.DataSource = null;
                    uoListViewTranspoCost.DataBind();

                }

                uoListviewAir.DataSource = CrewAssistCMTransaction[0].CrewAssistAirTransaction;
                uoListviewAir.DataBind();

                if (uoListviewAir.Items.Count > 0)
                {
                    foreach (ListViewDataItem item in uoListviewAir.Items)
                    {
                        HiddenField OldStatus = (HiddenField)item.FindControl("uoHiddenFieldStatus");
                        DropDownList GetDDLList = (DropDownList)item.FindControl("uoDropDownListStatus");
                        GetDDLList.SelectedIndex = GlobalCode.GetselectedIndexText(GetDDLList, OldStatus.Value);
                    }
                }

            }
            else
            {

                ClearHotelObject(1);
                ClearMeetAndGreet();
                ClearObjectPortAgent(false);

                ClearObjectTrans();

                uoListviewAir.DataSource = null;
                uoListviewAir.DataBind();

                uoListViewHotelBook.DataSource = null;
                uoListViewHotelBook.DataBind();

                uoListViewTransportation.DataBind();
                uoListViewTransportation.DataBind();

                uoListViewTranportationRoute.DataSource = null;
                uoListViewTranportationRoute.DataBind();


                uoListViewTranspoCost.DataSource = null;
                uoListViewTranspoCost.DataBind();


            }

        }


        /// <summary>
        /// Date Modified:  25/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   use uoDropDownListRoomeType.SelectedValue instead of uoDropDownListRoomeType.SelectedIndex
        /// </summary>
        void LoadHotelRequestDetail(List<HotelTransactionMedical> HotelMedical)
        {
            try
            {
                ClearHotelObject(0);

                if (HotelMedical.Count > 0)
                {

                    string sRoomType = GlobalCode.Field2String(HotelMedical[0].RoomTypeID);
                    if (uoDropDownListRoomeType.Items.FindByValue(sRoomType) != null)
                    {
                        uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(HotelMedical[0].RoomTypeID);
                    }
                    uoHiddenFieldHotelTransID.Value = HotelMedical[0].TransHotelID.ToString();
                    uoLabelAdress.Text = HotelMedical[0].Address;
                    uoLabelTelephone.Text = HotelMedical[0].ContactNo;
                    //uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);
                    uoTextBoxCheckinDate.Text = HotelMedical[0].TimeSpanStartDate.ToString();
                    uoTextBoxCheckoutDate.Text = HotelMedical[0].TimeSpanEndDate .ToString();
                    uoTextBoxDuration.Text = HotelMedical[0].TimeSpanDuration.ToString();
                    uoCheckboxBreakfast.Checked = (bool)HotelMedical[0].Breakfast;
                    uoCheckboxLunch.Checked = (bool)HotelMedical[0].Lunch;
                    uoCheckboxDinner.Checked = (bool)HotelMedical[0].Dinner;
                    uoCheckBoxLunchDinner.Checked = (bool)HotelMedical[0].LunchOrDinner;
                    uoCheckBoxIsWithShuttle.Checked = (bool)HotelMedical[0].WithShuttle;

                    DateTime CheckInTime = GlobalCode.Field2DateTime(HotelMedical[0].TimeSpanStartTime);
                    string CInTime = String.Format("{0:HH:mm}", CheckInTime);
                    uoTxtBoxTimeIn.Text = CInTime;

                    DateTime CheckOutTime = GlobalCode.Field2DateTime(HotelMedical[0].TimeSpanEndTime);
                    string COutTime = String.Format("{0:HH:mm}", CheckOutTime);
                    uoTxtBoxTimeOut.Text = COutTime;

                    Decimal fAmount = GlobalCode.Field2Decimal(HotelMedical[0].ConfirmRateMoney);
                    uoTextContractedRate.Text = fAmount.ToString("N2");

                    Decimal RoomTax = GlobalCode.Field2Decimal(HotelMedical[0].ContractedRateMoney);
                    uoTextBoxComfirmRate.Text = RoomTax.ToString("N2");

                    Decimal MealVoucher = GlobalCode.Field2Decimal(HotelMedical[0].VoucherAmount);
                    uoTextBoxMealVoucher.Text = MealVoucher.ToString("N2");

                    uoTextBoxEmail.Text = HotelMedical[0].EmailTo;
                    uoTextBoxComment.Text = HotelMedical[0].Comment;

                    ListViewItem items = uoListViewHotelBook.Items[0];
                    CheckBox uoSelectCheckBoxs = (CheckBox)items.FindControl("uoCheckBoxsHotelTransaction");
                    uoSelectCheckBoxs.Checked = true;


                    //uoListViewHotelBook.DataSource = HotelMedical;
                    //uoListViewHotelBook.DataBind();



                    //uoDropDownListRoomeType.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListRoomeType, CrewAssist[0].RoomTypeID);
                    //string sRoomType = GlobalCode.Field2String(CrewAssist[0].RoomTypeID);
                    //if (uoDropDownListRoomeType.Items.FindByValue(sRoomType) != null)
                    //{
                    //    uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(CrewAssist[0].RoomTypeID);
                    //}

                    //uoHiddenFieldHotelTransID.Value = CrewAssist[0].TransHotelID.ToString();
                    //uoLabelAdress.Text = CrewAssist[0].Address;
                    //uoLabelTelephone.Text = CrewAssist[0].ContactNo;

                    //uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListHotel, CrewAssist[0].HotelIDInt);
                    //uoTextBoxCheckinDate.Text = CrewAssist[0].CheckinDate.ToString();
                    //uoTextBoxCheckoutDate.Text = CrewAssist[0].CheckoutDate.ToString();
                    //DateTime oldDate = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text);
                    //DateTime newDate = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text);
                    //TimeSpan ts = newDate - oldDate;
                    //uoTextBoxDuration.Text = ts.Days.ToString();

                    //uoCheckboxBreakfast.Checked = CrewAssist[0].MealBreakfastBit;
                    //uoCheckboxLunch.Checked = CrewAssist[0].MealLunchBit;
                    //uoCheckboxDinner.Checked = CrewAssist[0].MealDinnerBit;
                    //uoCheckBoxLunchDinner.Checked = CrewAssist[0].MealLunchDinnerBit;
                    //uoCheckBoxIsWithShuttle.Checked = CrewAssist[0].WithShuttleBit;

                    //DateTime CheckInTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeIn);
                    //string CInTime = String.Format("{0:HH:mm}", CheckInTime);
                    //uoTxtBoxTimeIn.Text = CInTime;

                    //DateTime CheckOutTime = GlobalCode.Field2DateTime(CrewAssist[0].TimeOut);
                    //string COutTime = String.Format("{0:HH:mm}", CheckOutTime);
                    //uoTxtBoxTimeOut.Text = COutTime;

                    //Decimal fAmount = GlobalCode.Field2Decimal(CrewAssist[0].ConfirmRateMoney);
                    //uoTextContractedRate.Text = fAmount.ToString("N2");

                    //Decimal RoomTax = GlobalCode.Field2Decimal(CrewAssist[0].ContractedRateMoney);
                    //uoTextBoxComfirmRate.Text = RoomTax.ToString("N2");

                    //Decimal MealVoucher = GlobalCode.Field2Decimal(CrewAssist[0].MealVoucherMoney);
                    //uoTextBoxMealVoucher.Text = MealVoucher.ToString("N2");

                    //uoTextBoxEmail.Text = CrewAssist[0].HotelEmail;
                    //uoTextBoxComment.Text = CrewAssist[0].HotelComment;

                    //ListViewItem items = uoListViewHotelBook.Items[0];
                    //CheckBox uoSelectCheckBoxs = (CheckBox)items.FindControl("uoCheckBoxsHotelTransaction");
                    //uoSelectCheckBoxs.Checked = true;

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

                    uoLabelVehicleAddress.Text = list[0].Address;
                    uoLabelVehicleTelephone.Text = list[0].Telephone;

                    uoDropDownListVehicleVendor.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListVehicleVendor, GlobalCode.Field2Int(list[0].VehicleVendorIDInt));


                    uoCheckBoxEmailTrans.Checked = true;
                    uoTextBoxEmailTrans.Text = list[0].Email;

                    uoTextBoxTranpComfirmby.Text = list[0].ConfirmBy;
                    uoListViewTranportationRoute.DataSource = list;
                    uoListViewTranportationRoute.DataBind();

                    uoListViewTranspoCost.DataSource = list[0].VehicleContract;
                    uoListViewTranspoCost.DataBind();


                    //uoListViewVehicleRemark.DataSource = list[0].VehicleRemark;
                    //uoListViewVehicleRemark.DataBind();

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



        void EnableTransporationControl(bool val)
        {

            uoDropDownListVehicleVendor.Enabled = val;
            uoCheckBoxEmailTrans.Enabled = val;
            uoTextBoxEmailTrans.Enabled = val;

            uoTextBoxTransComment.Enabled = val;
            uoTextBoxTranpComfirmby.Enabled = val;
        }


        protected string GetRequestColor()
        {

            var ColorCode = Eval("ColorCode");
            var ForCode = Eval("ForeColor");


            if (ColorCode == null && ForCode == null) return "<tr>";

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


        protected void ButtonSend_OnClick(object sender, EventArgs e)
        {
            try
            {

                if (uoHiddenFieldExpendType.Value == "0")
                {
                    SaveHotelRequest();
                }
                else if (uoHiddenFieldExpendType.Value == "1")
                {
                    //AlertMessage("ButtonSend_OnClick: Test");

                    SaveVehicleTransaction(false);

                }
                else if (uoHiddenFieldExpendType.Value == "2")
                {
                    AlertMessage("ButtonSend_OnClick: Test");
                }
                else if (uoHiddenFieldExpendType.Value == "3")
                {
                    AlertMessage("ButtonSend_OnClick: Test");
                }
                else
                {
                    AlertMessage("ButtonSend_OnClick: Test w");
                }

            }
            catch (Exception ex)
            {
                AlertMessage("LoadTransprtationRequestDetail: " + ex.Message);
            }
        }

        void SaveHotelRequest()
        {
            try
            {
                string[] separators = { "-" };

                MedicalBLL SF = new MedicalBLL();
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                ListView lst = (ListView)uoListViewCompanionList;

                List<HotelRequestCompanion> HRC = new List<HotelRequestCompanion>();
                List<CrewAssistHotelBooking> HotelBooking = new List<CrewAssistHotelBooking>();


                List<HotelTransactionMedical> Medical = new List<HotelTransactionMedical>();



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
                 
                    if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0 && 
                        uoTextBoxCheckinDate.Text.ToString() != "" && uoTextBoxCheckoutDate.Text.ToString() != "")
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

                        if (CheckBoxCopycrewassist.Checked == true && CheckBoxCopycrewassist.ToolTip.ToString() == "")
                        {
                            cssEmail = CheckBoxCopycrewassist.ToolTip.ToString() + "; ";
                        }

                        if (CheckBoxCopycrewhotels.Checked == true && CheckBoxCopycrewhotels.ToolTip.ToString() == "")
                        {
                            cssEmail += CheckBoxCopycrewhotels.ToolTip.ToString() + "; ";
                        }

                        if (CheckBoxCopyShip.Checked == true && CheckBoxCopyShip.ToolTip.ToString() == "")
                        {
                            cssEmail += CheckBoxCopyShip.ToolTip.ToString() + "; ";
                        }
                        if (CheckBoxScheduler.Checked == true && CheckBoxScheduler.ToolTip.ToString() == "")
                        {
                            cssEmail += CheckBoxScheduler.ToolTip.ToString() + "; ";
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

                        //HotelBooking = SF.InsertHotelTransRequest(uoHiddenFieldHotelTransID.Value
                        //               , txtEmployeeID.Text
                        //               , ""
                        //              , ""
                        //               , "0"
                        //               , "0"
                        //               , uoDropDownListPort.SelectedValue
                        //               , "0"
                        //               , uoDropDownListHotel.SelectedValue
                        //               , uoTextBoxCheckinDate.Text
                        //               , uoTextBoxCheckoutDate.Text
                        //               , GlobalCode.GetdateDiff(GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text), GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text)).ToString()
                        //               , uoDropDownListRoomeType.SelectedValue
                        //               , uoCheckboxBreakfast.Checked
                        //               , uoCheckboxLunch.Checked
                        //               , uoCheckboxDinner.Checked
                        //               , uoCheckBoxLunchDinner.Checked
                        //               , uoCheckBoxIsWithShuttle.Checked
                        //               , "0"
                        //               , "0"
                        //               , "0"
                        //               , uoTextBoxComment.Text.ToString() // remark
                        //               , txtStatus.Text
                        //               , uoTxtBoxTimeIn.Text
                        //               , uoTxtBoxTimeOut.Text
                        //               , uoTextContractedRate.Text
                        //               , uoCheckContractBoxTaxInclusive.Checked
                        //               , uoTextBoxComfirmRate.Text
                        //               , uoHiddenFieldUser.Value
                        //               , uoHiddenFieldTravelRequestID.Value
                        //               , uoDropDownListCurrency.SelectedValue
                        //               , ""
                        //               , "SaveRequest"
                        //               , Path.GetFileName(Request.UrlReferrer.AbsolutePath)
                        //               , CommonFunctions.GetDateTimeGMT(dateNow)
                        //               , dateNow
                        //               , IsAir == true ? true : false
                        //               , GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)
                        //               , GlobalCode.Field2Int(uoHiddenFieldIDBigint.Value)
                        //               , GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue)
                        //               , GlobalCode.Field2Double(uoTextBoxMealVoucher.Text)
                        //               , GlobalCode.Field2Double(uoTextBoxComfirmRate.Text)
                        //               , GlobalCode.Field2Double(uoTextContractedRate.Text)
                        //               , uoTextBoxEmail.Text.ToString()

                        //               , uoHiddenFieldCityCode.Value

                        //               , GlobalCode.Field2TinyInt(uoHiddenFieldHSourceRequest.Value)
                        //               , txtWhoConfirm.Text
                        //               , ""
                        //               , recipient.Substring(0, recipient.Length - 2).ToString()
                        //               , ccrecipient
                        //               , ""
                        //               , GlobalCode.Field2Double(uoTextBoxComfirmRate.Text)
                        //               , uoCheckBoxMedical.Checked
                        //               , GlobalCode.Field2Long(uoHiddenFieldHotelTransID.Value)
                        //               , HRC);


                        Medical.Add(new HotelTransactionMedical { 
                                TransHotelID = GlobalCode.Field2Long( uoHiddenFieldHotelTransID.Value),
                                SeafarerID = GlobalCode.Field2Long( txtEmployeeID.Text),
                                FullName = GlobalCode.Field2String(txtName.Text),
                                TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                                IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                                RecordLocator = GlobalCode.Field2String(""),
                                SeqNo = GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value),
                                PortAgentVendorID = GlobalCode.Field2Long(uoDropDownListHotel.SelectedItem.Value),
                                RoomTypeID = GlobalCode.Field2Int(uoDropDownListRoomeType .SelectedItem.Value),
                                ReserveUnderName = "" ,
                                TimeSpanStartDate = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text),
                                TimeSpanStartTime = GlobalCode.Field2DateTime(uoTextBoxCheckinDate.Text),
                                TimeSpanEndDate = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text),
                                TimeSpanEndTime = GlobalCode.Field2DateTime(uoTextBoxCheckoutDate.Text),
                                TimeSpanDuration = GlobalCode.Field2Int(uoTextBoxDuration.Text),
                                ConfirmationNo  = "",
                                HotelStatus = "Open",
                                DateCreatedDatetime  = DateTime.Now,
                                CreatedBy = GlobalCode.Field2String(uoHiddenFieldUser.Value),
                                IsActive  = true,
                                VoucherAmount = GlobalCode.Field2Decimal(uoTextBoxMealVoucher.Text),
                                ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value),
                                ApprovedBy = "",
                                ApprovedDate = GlobalCode.Field2DateTimeNull( DateTime.Now),
                                ContractFrom = "",  
                                RemarksForAudit  = "Manual Hotel booking",
                                HotelCity = uoDropDownListHotel.SelectedItem.Text.Substring(0,3),
                                RoomCount = GlobalCode.Field2Float( uoDropDownListRoomeType.SelectedItem.Text == "Single" ? 1.0 : 0.5), 
                                HotelName = uoDropDownListHotel.SelectedItem.Text.Remove(0,5),
                                ConfirmRateMoney = GlobalCode.Field2Decimal ( uoTextBoxComfirmRate.Text),
                                ContractedRateMoney =  GlobalCode.Field2Decimal( uoTextContractedRate .Text),
                                EmailTo = GlobalCode.Field2String(uoTextBoxEmail.Text),
                                Comment   = GlobalCode.Field2String(uoTextBoxComment.Text),
                                CurrencyID = GlobalCode.Field2Int(uoDropDownListCurrency.SelectedItem.Value),
                                ConfirmBy = GlobalCode.Field2String(uoTextBoxPortAgentConfirm.Text),
                                StatusID = 1,
                                IsPortAgent = (bool)(uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -" ? false : true)

                        });
                     
                        var value =   SF.InsertHotelTransactionMedical(Medical);
                        LoadHotelRequestDetail(value);
                        uoListViewHotelBook.DataSource = value;
                        uoListViewHotelBook.DataBind();
                        txtWhoConfirm.Text = "";
                        txtConfirmrate.Text = "";

                    }
                //}

                //else if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
                //{

                //    if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0 && uoTextBoxCheckinDate.Text.ToString() != "" && uoTextBoxCheckoutDate.Text.ToString() != "")
                //    {

                //        //HotelBooking = SF.SavePortAgentHotelRequest(GetHotelTranPortAgent(IsAir == true ? true : false), GlobalCode.Field2String(uoHiddenFieldUser.Value), HRC);
                //        //LoadHotelRequestDetail(HotelBooking);
                //        //uoListViewHotelBook.DataSource = HotelBooking;
                //        //uoListViewHotelBook.DataBind();

                //    }
                //}




            }
            catch (Exception ex)
            {
                AlertMessage("SaveHotelTransaction: " + ex.Message);
            }

        }

        private List<HotelTransactionPortAgent> GetHotelTranPortAgent(bool? IsAir)
        {
            List<HotelTransactionPortAgent> List = new List<HotelTransactionPortAgent>();
            if (uoTextBoxCheckinDate.Text.ToString() != "" && uoTextBoxCheckoutDate.Text.ToString() != "" && GlobalCode.Field2Int(uoTextBoxDuration.Text) > 0)
            {
                List.Add(new HotelTransactionPortAgent
                {
                    HotelTransID = GlobalCode.Field2Long(uoHiddenFieldHotelTransID.Value),
                    TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                    IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                    RecordLocator = "",
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
                    HotelCity = "",
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


        /// <summary>
        /// Modified By:		Josephine Gad
        /// Create date:		25/Nov/2013
        /// Description:		Get Room Type of Crew
        /// ================================================= 
        /// </summary>
        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
            {
                GetCrewAssistHotelInformation(0, GlobalCode.Field2Int(uoDropDownListHotel.SelectedItem.Value));
            }
            else if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "HT -")
            {
                GetCrewAssistHotelInformation(1, GlobalCode.Field2Int(uoDropDownListHotel.SelectedItem.Value));
            }
            else
            {
                ClearHotelObject(0);
            }
        }

        void GetCrewAssistHotelInformation(short LoadType, int ID)
        {
            try
            {
                uoHiddenFieldContractStart.Value = "";
                uoHiddenFieldContractEnd.Value = "";

                CrewAssistsBLL SF = new CrewAssistsBLL();
                Session["ContactPerson"] = null;
                Session["ContractRate"] = null;
                Session["HotelEmailTo"] = null;

                DateTime starDate = DateTime.Now;
                DateTime enddate = DateTime.Now.AddDays(1);

                List<CrewAssistHotelInformation> _HotelInformationList = new List<CrewAssistHotelInformation>();

                _HotelInformationList = SF.GetCrewAssistHotelVendor(LoadType, ID,
                        GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                        GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value)
                        , uoHiddenFieldPortCode.Value);

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


                    if (_HotelInformationList[0].ATTEMail.Count > 0)
                    {
                        CheckBoxCopycrewhotels.ToolTip = _HotelInformationList[0].ATTEMail[0].Email;
                    }

                    txtWhoConfirm.Text = _HotelInformationList[0].ContactPerson;
                    txtConfirmrate.Text = _HotelInformationList[0].ContractedRate;

                    uoHiddenFieldContractStart.Value = _HotelInformationList[0].ContractDateStarted;
                    uoHiddenFieldContractEnd.Value = _HotelInformationList[0].ContractDateEnd;

                    uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(_HotelInformationList[0].RoomTypeID);
                } 

            }
            catch (Exception ex)
            {
                AlertMessage("DownListPort: " + ex.Message);
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


                uoCheckBoxEmailTrans.Checked = false;
                uoTextBoxEmailTrans.Text = ""; ;
                uoTextBoxEmailTrans.Enabled = false;

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
                    uoTextBoxTranpComfirmby.Text = "";

                    uoLabelVehicleAddress.Text = VehicleVendor[0].Address;
                    uoLabelVehicleTelephone.Text = VehicleVendor[0].Telephone;

                    uoListViewTranspoCost.DataSource = VehicleVendor[0].VehicleCost;
                    uoListViewTranspoCost.DataBind();

                    uoListViewTranportationRoute.DataSource = null;
                    uoListViewTranportationRoute.DataBind();

                }

            }
            catch (Exception ex)
            {
                AlertMessage("DropDownListVehicleVendor: " + ex.Message);
            }
        }






        void SaveHotelTransaction(bool? IsAir)
        {
            try
            {
                string[] separators = { "-" };

                CrewAssistBLL SF = new CrewAssistBLL();

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

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

                    if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0 && uoTextBoxCheckinDate.Text.ToString() != "" && uoTextBoxCheckoutDate.Text.ToString() != "")
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

                        if (CheckBoxCopycrewassist.Checked == true)
                        {
                            cssEmail = CheckBoxCopycrewassist.ToolTip.ToString();
                        }

                        if (CheckBoxCopycrewhotels.Checked == true)
                        {
                            cssEmail += CheckBoxCopycrewhotels.ToolTip.ToString();
                        }

                        if (CheckBoxCopyShip.Checked == true)
                        {
                            cssEmail += CheckBoxCopyShip.ToolTip.ToString();
                        }
                        if (CheckBoxScheduler.Checked == true)
                        {
                            cssEmail += CheckBoxScheduler.ToolTip.ToString();
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
                           , txtEmployeeID.Text
                           , ""
                           , ""
                           , "0"
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
                           , ""
                           , ""
                           , ""
                           , uoTextBoxComment.Text.ToString() // remark
                           , txtStatus.Text
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

                           , txtWhoConfirm.Text
                           , ""
                           , recipient.Substring(0, recipient.Length - 2).ToString()
                           , ccrecipient
                           , ""
                           , GlobalCode.Field2Double(uoTextBoxComfirmRate.Text)
                           , uoCheckBoxMedical.Checked
                           , GlobalCode.Field2Long(uoHiddenFieldHotelTransID.Value)
                           , HRC);

                    }
                }

                else if (uoDropDownListHotel.SelectedItem.Text.Substring(0, 4) == "PA -")
                {

                    if (GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue) > 0
                        && uoTextBoxCheckinDate.Text.ToString() != ""
                        && uoTextBoxCheckoutDate.Text.ToString() != ""
                        )
                    {

                        //uoHiddenFieldHotelRequestID.Value = SF.SavePortAgentHotelRequest(GetHotelTranPortAgent(IsAir == true ? true : false), GlobalCode.Field2String(uoHiddenFieldUser.Value), HRC);
                        //if (uoHiddenFieldHotelRequestID.Value == "Crew has already a port agent request")
                        //{
                        //    AlertMessage(uoHiddenFieldHotelRequestID.Value.ToString());
                        //    uoHiddenFieldHotelRequestID.Value = "0";
                        //}

                        //uoHiddenFieldSaveHotel.Value = "0";

                        //if (GlobalCode.Field2Long(uoHiddenFieldHotelRequestID.Value) > 0)
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "key", "PortAgentHotelCofirmation();", true);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessage("SaveHotelTransaction: " + ex.Message);
            }
        }

        protected void uoButtonLoadHotel_OnClick(object sender, EventArgs e)
        {
            try
            {
                ClearHotelObject(0);


                ListView lst = (ListView)uoListViewHotelBook;

                foreach (ListViewItem ls in lst.Items)
                {
                    if (((CheckBox)ls.FindControl("uoCheckBoxsHotelTransaction")).Checked == true)
                    {



                        uoHiddenFieldHotelTransID.Value = ((HiddenField)ls.FindControl("uoHiddenFieldHotelID")).Value;

                        uoLabelAdress.Text = ((HiddenField)ls.FindControl("hoAddress")).Value;
                        uoLabelTelephone.Text = ((HiddenField)ls.FindControl("hoTelephone")).Value;

                        uoDropDownListHotel.SelectedIndex = GlobalCode.GetselectedIndexText(uoDropDownListHotel, ((Label)ls.FindControl("lblHotelBook")).Text);
                        uoDropDownListRoomeType.SelectedIndex = GlobalCode.GetselectedIndexText(uoDropDownListRoomeType, ((Label)ls.FindControl("lblRoomTypeID")).Text);

                        uoTextBoxCheckinDate.Text = ((Label)ls.FindControl("lblCheckInDate")).Text;
                        uoTextBoxCheckoutDate.Text = ((Label)ls.FindControl("lblCheckOutDate")).Text;
                        uoTextBoxDuration.Text = ((Label)ls.FindControl("lblHotelNite")).Text;

                        uoCheckboxBreakfast.Checked = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("hoIsBreakfast")).Value);
                        uoCheckboxLunch.Checked = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("hoIsLunch")).Value);
                        uoCheckboxDinner.Checked = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("hoIsDinner")).Value);
                        uoCheckBoxLunchDinner.Checked = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("hoIsAddMeal")).Value);
                        uoCheckBoxIsWithShuttle.Checked = GlobalCode.Field2Bool(((HiddenField)ls.FindControl("hoIsWithShuttle")).Value);


                        uoTextContractedRate.Text = GlobalCode.Field2Decimal(((HiddenField)ls.FindControl("hoContractRate")).Value).ToString("N2");
                        uoTextBoxComfirmRate.Text = GlobalCode.Field2Decimal(((HiddenField)ls.FindControl("hoConfirmRate")).Value).ToString("N2");
                        uoTextBoxMealVoucher.Text = GlobalCode.Field2Decimal(((HiddenField)ls.FindControl("uoMealVoucher")).Value).ToString("N2");
                        uoTextBoxEmail.Text = ((HiddenField)ls.FindControl("hoEmail")).Value;
                        uoTextBoxComment.Text = ((HiddenField)ls.FindControl("hoComment")).Value;


                    }
                }

            }
            catch (Exception ex)
            {
                AlertMessage("LoadTransprtationRequestDetail: " + ex.Message);
            }
        }

        protected void btnSaveRemark_click(object sender, EventArgs e)
        {

            List<CrewAssisRemark> Lst = new List<CrewAssisRemark>();

            CrewAssistBLL CA = new CrewAssistBLL();
            if (GlobalCode.Field2String(uoHiddenFieldUser.Value ) == "") Response.Redirect("~/login.aspx");

            //CrewAssisRemark RemarkList =  new CrewAssisRemark(); 
            //RemarkList.RemarkID =  GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value);
            //RemarkList.TravelRequestID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value);
            //RemarkList.Remark = txtRemark.Text.ToString();
            //RemarkList.UserID = uoLabelUserID.Text.ToString();
            //RemarkList.IDBigInt = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value);
            //RemarkList.UserID 

            //,, "Crew Assist"
            //,GlobalCode.Field2TinyInt(uoHiddenFieldRSourceRequest.Value)
            //,GlobalCode.Field2Long(uoTextBoxEmployeeID.Text)
            //,GlobalCode.Field2Int(cboRemarkType.SelectedItem.Value)
            //,GlobalCode.Field2TinyInt(cboRemarkStatus.SelectedItem.Value)
            //,GlobalCode.Field2String(txtSummaryCall.Text)
            //,GlobalCode.Field2Int(cboRemarkRequestor.SelectedItem.Value)

            string PortCode = "";
            if (uoDropDownListPort.SelectedIndex > 0)
            {
                PortCode = uoDropDownListPort.SelectedItem.Text.Substring(0, 3);
            }

            string TransTime = "1/1/1753 " + uoTextBoxRemTransTime.Text;

            Lst = CA.InsertCrewAssistRemark(GlobalCode.Field2Long(uoHiddenFieldRemarkID.Value)
                        , GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)
                        , txtRemark.Text.ToString()
                        , uoHiddenFieldUser.Value
                        , GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value)
                        , "Crew Assist"
                        , GlobalCode.Field2TinyInt(uoHiddenFieldRSourceRequest.Value)
                        , GlobalCode.Field2Long(txtEmployeeID.Text)
                        , GlobalCode.Field2Int(cboRemarkType.SelectedItem.Value)
                        , GlobalCode.Field2TinyInt(cboRemarkStatus.SelectedItem.Value)
                        , GlobalCode.Field2String(txtSummaryCall.Text)
                        , GlobalCode.Field2Int(cboRemarkRequestor.SelectedItem.Value)
                        , GlobalCode.Field2DateTime(uoTextBoxRemTransdate.Text)
                        , GlobalCode.Field2DateTime(TransTime)
                        , PortCode
                        , false);

            uoListViewRemarkPopup.DataSource = Lst;
            uoListViewRemarkPopup.DataBind();

            uoListViewRemark.DataSource = Lst;
            uoListViewRemark.DataBind();

            txtRemark.Text = "";
            uoHiddenFieldRemarkID.Value = "";
            txtSummaryCall.Text = "";

            uoTextBoxRemTransdate.Text = null;
            uoTextBoxRemTransTime.Text = null;

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
            List<VehicleTransactionMedical> list = new List<VehicleTransactionMedical>();

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
                    list.Add(new VehicleTransactionMedical
                    {
                        RouteIDFrom = GlobalCode.Field2Int(FromID.Value),
                        RouteFrom = RouteFrom.Text.ToString(),
                        RouteIDTo = GlobalCode.Field2Int(ToID.Value),
                        RouteTo = RouteTo.Text.ToString(),
                        PickUpDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                        PickUpTime = GlobalCode.Field2DateTime(PickUpTime.Text),
                        From = GlobalCode.Field2String(From.Text),
                        To = GlobalCode.Field2String(TO.Text),
                        ConfirmRateMoney = GlobalCode.Field2Double(TransCost.Text),
                        TransVehicleID = GlobalCode.Field2Long(HiddenFieldTransID.Value),
                        //ReqVehicleIDBigint = GlobalCode.Field2Long(HiddenFieldReqTransID.Value)

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
            List<VehicleTransactionMedical> list = new List<VehicleTransactionMedical>();

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

                list.Add(new VehicleTransactionMedical
                {
                    RouteIDFrom = GlobalCode.Field2Int(FromID.Value),
                    RouteFrom = RouteFrom.Text.ToString(),
                    RouteIDTo = GlobalCode.Field2Int(ToID.Value),
                    RouteTo = RouteTo.Text.ToString(),
                    PickUpDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                    PickUpTime = GlobalCode.Field2DateTime(PickUpTime.Text),
                    From = GlobalCode.Field2String(From.Text),
                    To = GlobalCode.Field2String(TO.Text),
                    StatusID = 1,
                    ConfirmRateMoney = GlobalCode.Field2Double(TransCost.Text),
                    TransVehicleID  = GlobalCode.Field2Long(HiddenFieldTransID.Value),
                    //ReqVehicleIDBigint = GlobalCode.Field2Long(HiddenFieldReqTransID.Value)
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

                list.Add(new VehicleTransactionMedical
                {
                    RouteIDFrom  = GlobalCode.Field2Int(uoDropDownListTFrom.SelectedValue),
                    RouteFrom = uoTextBoxTFrom.Text.ToString(),
                    RouteIDTo = GlobalCode.Field2Int(uoDropDownListTTo.SelectedValue),
                    RouteTo = uoTextBoxTTo.Text.ToString(),

                    PickUpDate = GlobalCode.Field2DateTime(uoTextBoxTPickupdate.Text),
                    PickUpTime = GlobalCode.Field2DateTime(uoTextBoxTTime.Text),
                    From = GlobalCode.Field2String(uoDropDownListTFrom.SelectedItem.Text),
                    To = GlobalCode.Field2String(uoDropDownListTTo.SelectedItem.Text),
                    ConfirmRateMoney = GlobalCode.Field2Double(uoTextBoxCost.Text),
                    StatusID = 1,
                    TransVehicleID = GlobalCode.Field2Long(0),
                    //ReqVehicleIDBigint = GlobalCode.Field2Long(0)
                });
            }
            else
            {
                AlertMessage("Route Required... ");
            }

            GetTranportationRouteItem(list);
        }

        void GetTranportationRouteItem(List<VehicleTransactionMedical> listValue)
        {
            try
            {
                List<VehicleTransactionMedical> list = new List<VehicleTransactionMedical>();
                uoListViewTranportationRoute.DataSource = listValue;
                uoListViewTranportationRoute.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        string GetPortCode()
        {
            return txtStatus.Text.ToString() == "ON" ? uoHiddenFieldArrCode.Value == "" ? uoHiddenFieldPortCode.Value : uoHiddenFieldArrCode.Value : uoHiddenFieldPortCode.Value;
        }
        void SaveVehicleTransaction(bool? IsAir)
        {
            try
            {
                      string FromTrans = GetPortCode(); ;
                string ToTrans = GetPortCode(); ;
                string[] separators = { "-" };


                MedicalBLL SF = new MedicalBLL();

                if (uoTextBoxTranpComfirmby.Text.ToString() == "")
                {

                    ExceptionMessage("Required Transportation! \n Confirm By");

                }


                if (uoTextBoxTransComment.Text.ToString() == "")
                {
                    ExceptionMessage("Comment Required \n and Request source...");
                    return;
                }


                

                bool TransSave = true;
              

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





                List<VehicleTransactionMedical> Transpo = new List<VehicleTransactionMedical>();
                if (GlobalCode.Field2Long(uoDropDownListVehicleVendor.SelectedValue) > 0 && TransSave == true)
                {

                    ListView ListViewControl = uoListViewTranportationRoute;
                    //List<VehicleTransactionMedical> list = new List<VehicleTransactionMedical>();

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

                     
                        PickUpDate = (Label)item.FindControl("uoLabelPickUpDate");
                        PickUpTime = (Label)item.FindControl("uoLabelPickUpTime");
                        TransCost = (Label)item.FindControl("uoLabelCost");

                        Transpo.Add(new VehicleTransactionMedical
                        {

                            TransVehicleID = GlobalCode.Field2Long(TransVendorID.Value),
                            SeafarerID = GlobalCode.Field2Long(txtEmployeeID.Text),
                            IdBigint = GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value),
                            TravelReqID = GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value),
                            RecordLocator = GlobalCode.Field2String(uoHiddenFieldRecordLocator.Value),
                            TranspoVendorID = GlobalCode.Field2Long(uoDropDownListVehicleVendor.SelectedItem.Value),
                            VehiclePlateNo = GlobalCode.Field2String(""),
                            PickUpDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                            PickUpTime = GlobalCode.Field2DateTime(PickUpTime.Text),
                            DropOffDate = GlobalCode.Field2DateTime(PickUpDate.Text),
                            DropOffTime = GlobalCode.Field2DateTime(PickUpTime.Text),
                            ConfirmationNo = GlobalCode.Field2String(""),
                            VehicleStatus = GlobalCode.Field2String("Open"),
                            VehicleTypeId = GlobalCode.Field2Int(0),
                            SFStatus = GlobalCode.Field2String(txtStatus.Text),
                            RouteIDFrom = GlobalCode.Field2Int(FromID.Value),
                            RouteIDTo = GlobalCode.Field2Int(ToID.Value),

                            From = FromTrans,
                            To  = ToTrans,

                             
                            DateCreated = DateTime.Now,
                            DateModified =  null,// GlobalCode.Field2Long(TransVendorID.Value) == 0 ? null : DateTime.Now ,// GlobalCode.Field2Long(TransVendorID.Value) == 0 ? null : DateTime.Now,
                            CreatedBy = uoHiddenFieldUser.Value.ToString(),
                            Modifiedby = GlobalCode.Field2Long(TransVendorID.Value) == 0 ? "" : uoHiddenFieldUser.Value.ToString(),
                            IsActive = true, 
                            RemarksForAudit = "", 
                            HotelID = null, 
                            IsVisible = true,
                            ContractId = 0, 
                            IsSeaport = true,
                            SeqNo =GlobalCode.Field2Int( uoHiddenFieldSeqNo.Value), 
                            Driver = "", 
                            VehicleDispatchTime = null,
                             
                            RouteFrom = GlobalCode.Field2String(RouteFrom.Text),
                            RouteTo = GlobalCode.Field2String(RouteTo.Text),

                            VehicleUnset = null, 
                            VehicleUnsetBy = null, 
                            VehicleUnsetDate = null,
                            ConfirmBy = GlobalCode.Field2String(uoTextBoxTranpComfirmby.Text),
                            Comments = GlobalCode.Field2String(uoTextBoxTransComment.Text), 
                            VehicleVendor = GlobalCode.Field2String(uoDropDownListVehicleVendor.SelectedItem.Text), 
                            ContractedRateMoney = null,//GlobalCode.Field2String(),
                            ConfirmRateMoney = GlobalCode.Field2Double( TransCost.Text), //GlobalCode.Field2String(), 
                            CurrencyInt = 0, //GlobalCode.Field2String(), 
                            StatusID =1,
                            ApprovedBy = GlobalCode.Field2String(uoTextBoxTranpComfirmby.Text),
                            ApprovedDate = GlobalCode.Field2DateTime(DateTime.Now),
                            EmailTo = GlobalCode.Field2String(uoTextBoxEmailTrans.Text),
                            RequestSourceID = 0, 
                            TransportationDetails = "", 
                            IsPortAgent =  uoDropDownListVehicleVendor.SelectedItem.Text.Substring(0, 2) == "VH" ? false : true,

                             
                        });
                    }
                }

                if (Transpo.Count > 0)
                {
                    var result = SF.InsertVehicleTransactionMedical(Transpo);

                    uoListViewTranportationRoute.DataSource = result;
                    uoListViewTranportationRoute.DataBind();

                    uoListViewTransportation.DataSource = result;
                    uoListViewTransportation.DataBind();

                    SearchCrewMember(GlobalCode.Field2Long(txtEmployeeID.Text));

                }

            }
            catch (Exception ex)
            {
                AlertMessage("SaveVehicleTransaction: " + ex.Message);
            }

        }





















    }
}

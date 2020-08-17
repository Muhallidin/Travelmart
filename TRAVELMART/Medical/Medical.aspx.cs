using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TRAVELMART.Medical
{
    public partial class Medical : System.Web.UI.Page
    {


        private AsyncTaskDelegate _dlgtSeafarer;

        // Create delegate. 
        protected delegate void AsyncTaskDelegate();


        MedicalBLL BLL = new MedicalBLL();
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



        protected void Page_Load(object sender, EventArgs e)
        {
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                GlobalCode gc = new GlobalCode();
                string userID = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldUser.Value = userID;

                //uoFileLoadExcel.Attributes["onchange"] = "UploadFile(this)";
            }

            if (Session["CurrentDate"] != null && Session["CurrentDate"].ToString() == "")
            {
                Session["CurrentDate"] = DateTime.Now.Date;
            }

            if (uoFileLoadExcel.HasFile)
            {
                Session["FileLoadExcel"] = uoFileLoadExcel;
            }
          

        }



        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);
            }
            else
            {

                if (uoListviewRequest.Items.Count <= 0)
                {
                    uoListviewRequest.DataSource = null;
                    uoListviewRequest.DataBind();
                }
                 

            }

             

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



            //IMSClassList IMSClass = new IMSClassList();
            //IMSClass = BLL.GetMedicalClassList(0, 0);

            //lstVendor.DataSource = null;
            //lstVendor.Items.Clear();

            //lstVendor.DataSource = IMSClass.Vendor;
            //lstVendor.DataTextField = "BranchName";
            //lstVendor.DataValueField = "BranchID";
            //lstVendor.DataBind();


            //uoDropDownListPort.DataSource = null;
            //uoDropDownListPort.Items.Clear();

            //uoDropDownListPort.DataSource = IMSClass.Port;
            //uoDropDownListPort.DataTextField = "PortName";
            //uoDropDownListPort.DataValueField = "PortID";
            //uoDropDownListPort.DataBind();
             


            List<CrewAssisRemark> Lst = new List<CrewAssisRemark>();
            uoListViewRemark.DataSource = Lst;
            uoListViewRemark.DataBind();

            List<Medical> Medical = new List<Medical>();
            uoListviewRequest.DataSource = Medical;
            uoListviewRequest.DataBind();



            LoadAllCombo();



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

                lstHotel.DataSource = null;
                lstHotel.Items.Clear();

                uoDropDownListPort.DataSource = null;
                uoDropDownListPort.Items.Clear();

                uoDropDownListHotel.DataSource = null;
                uoDropDownListHotel.Items.Clear();


                Session["CrewAssistVehicleVendor"] = null;

                int iRowCount = list.Count;
                if (iRowCount > 0)
                {



                    Session["HotelNameList"] = list[0].CrewAssistHotelList;





                    lstHotel.DataSource = list[0].CrewAssistHotelList;
                    lstHotel.DataTextField = "HotelName";
                    lstHotel.DataValueField = "HotelID";
                    lstHotel.DataBind();


                     

                    uoDropDownListPort.DataSource = null;
                    uoDropDownListPort.Items.Clear();
 
                   




                    Session["CrewAssitPortList"] = list[0].CrewAssitPortList;
                    uoDropDownListPort.DataSource = list[0].CrewAssitPortList;
                    uoDropDownListPort.DataTextField = "PORTName";
                    uoDropDownListPort.DataValueField = "PORTID";
                    uoDropDownListPort.DataBind();
                    uoDropDownListPort.Items.Insert(0, new ListItem("--Select Port--", "0"));

                    //lstPort.DataSource = list[0].CrewAssitPortList;
                    //lstPort.DataTextField = "PORTName";
                    //lstPort.DataValueField = "PORTID";
                    //lstPort.DataBind();
                



                    uoDropDownListCurrency.DataSource = list[0].CrewAssistCurrency;
                    uoDropDownListCurrency.DataTextField = "CurrencyName";
                    uoDropDownListCurrency.DataValueField = "CurrencyID";
                    uoDropDownListCurrency.DataBind();


                  
                    //uoDropDownListRouteFrom.Items.Clear();
                    //uoDropDownListRouteFrom.Items.Add(new ListItem("--Select Route From--", "0"));
                    //uoDropDownListRouteFrom.DataSource = list[0].CrewAssistRout;
                    //uoDropDownListRouteFrom.DataTextField = "RoutName";
                    //uoDropDownListRouteFrom.DataValueField = "RoutId";
                    //uoDropDownListRouteFrom.DataBind();
                 
                    //uoDropDownListMAGTo.Items.Clear();
                    //uoDropDownListMAGTo.Items.Add(new ListItem("--Select To--", "0"));
                    //uoDropDownListMAGTo.DataSource = list[0].CrewAssistRout;
                    //uoDropDownListMAGTo.DataTextField = "RoutName";
                    //uoDropDownListMAGTo.DataValueField = "RoutId";
                    //uoDropDownListMAGTo.DataBind();

 
                    //ListView mylist = ((ListView)uoListViewTranportationRoute);
                    //ListViewItem lvi = null;
                    //if (mylist.Controls.Count == 1)
                    //    lvi = mylist.Controls[0] as ListViewItem;



                    //ListView mylist = ((ListView)uoListViewTranportationRoute) 


                    //if (lvi == null || lvi.ItemType != ListViewItemType.EmptyItem)
                    //    return;

                    //Literal literal1 = (Literal)lvi.FindControl("Literal1");
                    //if (literal1 != null)
                    //    literal1.Text = "No items to display";





                    //Session["CrewAssistVehicleVendor"] = list[0].VehicleVendor;
                    //var Vehresult = (from dbo in list[0].VehicleVendor
                    //                 select new
                    //                 {
                    //                     Vehicle = dbo.Vehicle,
                    //                     VehicleID = dbo.VehicleID,
                    //                     IsPortAgent = dbo.IsPortAgent
                    //                 }).Distinct();

                    //uoDropDownListVehicleVendor.DataSource = null;
                    //uoDropDownListVehicleVendor.Items.Clear();

                    //uoDropDownListVehicleVendor.DataSource = Vehresult;
                    //uoDropDownListVehicleVendor.DataTextField = "Vehicle";
                    //uoDropDownListVehicleVendor.DataValueField = "VehicleID";
                    //uoDropDownListVehicleVendor.DataBind();
                    //uoDropDownListVehicleVendor.Items.Insert(0, new ListItem("--Select Vehicle--", "0"));


                    //uoDropDownListVCountryVisiting.DataSource = list[0].CrewAssistNationality;
                    //uoDropDownListVCountryVisiting.DataTextField = "Nationality";
                    //uoDropDownListVCountryVisiting.DataValueField = "NatioalityID";
                    //uoDropDownListVCountryVisiting.DataBind();
                    //uoDropDownListVCountryVisiting.Items.Insert(0, new ListItem("--Select Country Visit--", "0"));


                    //Session["MeetAndGreetVendor"] = list[0].CrewAssistMeetAndGreetVendor;
                    //var result = (from dbo in list[0].CrewAssistMeetAndGreetVendor
                    //              select new
                    //              {
                    //                  MeetAndGreetVendor = dbo.MeetAndGreetVendor,
                    //                  MeetAndGreetVendorID = dbo.MeetAndGreetVendorID
                    //              })
                    //              .ToList().Distinct();

                    ////var rr = list[0].CrewAssistMeetAndGreetVendor.Select(e => new { e.MeetAndGreetVendor, e.MeetAndGreetVendorID }).Distinct();
                    //uoDropDownListMAndGVendor.DataSource = null;
                    //uoDropDownListMAndGVendor.Items.Clear();

                    //uoDropDownListMAndGVendor.DataSource = result;
                    //uoDropDownListMAndGVendor.DataTextField = "MeetAndGreetVendor";
                    //uoDropDownListMAndGVendor.DataValueField = "MeetAndGreetVendorID";
                    //uoDropDownListMAndGVendor.DataBind();
                    //uoDropDownListMAndGVendor.Items.Insert(0, new ListItem("--Select Meet And Greet--", "0"));

                    //Session["PortAgentVendor"] = list[0].CrewAssistVendorPortAgent;
                    //var PAsult = (from dbo in list[0].CrewAssistVendorPortAgent
                    //              select new
                    //              {
                    //                  PortAgentVendorName = dbo.PortAgentVendorName,
                    //                  PortAgentVendorID = dbo.PortAgentVendorID
                    //              })
                    //            .ToList().Distinct();


                    //uoDropDownListPortAgent.DataSource = PAsult;
                    //uoDropDownListPortAgent.DataTextField = "PortAgentVendorName";
                    //uoDropDownListPortAgent.DataValueField = "PortAgentVendorID";
                    //uoDropDownListPortAgent.DataBind();
                    //uoDropDownListPortAgent.Items.Insert(0, new ListItem("--Select Service Provider--", "0"));




                    //var Safresult = (from dbo in list[0].CrewAssistSafeguardVendor
                    //                 select new
                    //                 {
                    //                     SafeguardName = dbo.SafeguardName,
                    //                     SafeguardVendorID = dbo.SafeguardVendorID
                    //                 })
                    //              .ToList().Distinct();

                    //Session["CrewAssistSafeguardVendor"] = list[0].CrewAssistSafeguardVendor;

                    //uoDropDownListSafeguard.Items.Clear();
                    //uoDropDownListSafeguard.Items.Add(new ListItem("--Select Safeguard--", "0"));
                    //uoDropDownListSafeguard.DataSource = Safresult;
                    //uoDropDownListSafeguard.DataTextField = "SafeguardName";
                    //uoDropDownListSafeguard.DataValueField = "SafeguardVendorID";
                    //uoDropDownListSafeguard.DataBind();

                    ////uoDropDownListSafeguard.SelectedValue = "0";



                    //cboAirportList.DataSource = list[0].CrewAssistAirport;
                    //cboAirportList.DataTextField = "PortName";
                    //cboAirportList.DataValueField = "PortCode";
                    //cboAirportList.DataBind();

                    //cboRemarkType.Items.Clear();
                    //cboRemarkType.Items.Add(new ListItem("--Select Type--", "0"));
                    //cboRemarkType.DataSource = list[0].RemarkType;
                    //cboRemarkType.DataTextField = "RemarkType";
                    //cboRemarkType.DataValueField = "RemarkTypeID";
                    //cboRemarkType.DataBind();

                    //cboRemarkStatus.Items.Clear();
                    //cboRemarkStatus.Items.Add(new ListItem("--Select Remark Status--", "0"));
                    //cboRemarkStatus.DataSource = list[0].RemarkStatus;
                    //cboRemarkStatus.DataTextField = "RemarkType";
                    //cboRemarkStatus.DataValueField = "RemarkTypeID";
                    //cboRemarkStatus.DataBind();

                    //cboRemarkRequestor.Items.Clear();
                    //cboRemarkRequestor.Items.Add(new ListItem("--Select Remark Requestor--", "0"));
                    //cboRemarkRequestor.DataSource = list[0].RemarkRequestor;
                    //cboRemarkRequestor.DataTextField = "RemarkType";
                    //cboRemarkRequestor.DataValueField = "RemarkTypeID";
                    //cboRemarkRequestor.DataBind();

                }

            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }







        protected void 
            uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
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
                lstHotel.Items.Clear();
                int iRowCount = list.Count;
                if (iRowCount > 0)
                {

                    lstHotel.DataSource = list;
                    lstHotel.DataTextField = "HotelName";
                    lstHotel.DataValueField = "HotelID";
                    lstHotel.DataBind();

                }
               

                //BindVendors(uoHiddenFieldUser.Value, GlobalCode.Field2Int(uoDropDownListPort.SelectedValue), 0);

            }
            catch (Exception ex)
            {
                AlertMessage("DownListPort: " + ex.Message);
            }
        }

        protected void lstHotel_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            System.Web.UI.WebControls.ListBox obj = (System.Web.UI.WebControls.ListBox)sender;
            ClearHotelObject(0);

            if (obj.SelectedItem.Text.Substring(0, 4) == "PA -")
            {
                GetCrewAssistPAHotelInformation(GlobalCode.Field2Long(obj.SelectedValue));
            }
            else if (obj.SelectedItem.Text.Substring(0, 4) == "HT -")
            {
                GetCrewAssistHotelInformation(obj);
            }
            else
            {
                //ClearHotelObject(0);
            }

        
        }

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
            //CheckBoxCopycrewassist.Checked = false;
            //CheckBoxCopycrewhotels.Checked = false;
            //CheckBoxFax.Checked = false;
            CheckBoxEmail.Checked = false;

             


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
                        0,
                        0, "");

               
                uoTextContractedRate.Text = "";
                uoTextBoxMealVoucher.Text = "";
                uoTextBoxComfirmRate.Text = "";
                CheckBoxEmail.Checked = false;
                uoTextBoxEmail.Text = "";
                //CheckBoxFax.Checked = false;
                //CheckBoxFax.Text = "Fax : ";
                //CheckBoxCopycrewassist.Checked = false;
                //CheckBoxCopycrewhotels.Checked = false;

                //if (uoTextBoxStatus.Text.ToString() == "ON")
                //{
                //    if (uoHiddenFieldRequestDate.Value == "")
                //    {
                //        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
                //        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                //    }
                //    else
                //    {
                //        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(-1));
                //        enddate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
                //    }
                //}
                //else
                //{
                //    if (uoHiddenFieldRequestDate.Value == "")
                //    {
                //        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
                //        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                //    }
                //    else
                //    {
                //        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
                //        starDate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
                //    }

                //}
                uoTextBoxCheckinDate.Text = GlobalCode.Field2Date(starDate);
                uoTextBoxCheckoutDate.Text = GlobalCode.Field2Date(enddate);

                uoTextBoxDuration.Text = "1";

                uoTxtBoxTimeOut.Text = DateTime.Now.Hour.ToString() + ":00";
                uoTxtBoxTimeIn.Text = DateTime.Now.Hour.ToString() + ":00";

                if (_HotelInformationList.Count > 0)
                {
                    //uoLabelAdress.Text = _HotelInformationList[0].Address;
                    //uoLabelTelephone.Text = _HotelInformationList[0].ContactNo;
                    uoHiddenFieldCityCode.Value = _HotelInformationList[0].CityCode;
                    uoTextContractedRate.Text = _HotelInformationList[0].ContractedRate;


                    uoTextBoxMealVoucher.Text = _HotelInformationList[0].MealVoucher;


                    uoTextBoxComfirmRate.Text = _HotelInformationList[0].ContractRoomRateTaxPercentage;

                    uoTextBoxEmail.Text = _HotelInformationList[0].EmailTo;
                    //CheckBoxFax.Text =  "Fax : " + _HotelInformationList[0].FaxNo;

                    //TextBoxWhoConfirm.Text = _HotelInformationList[0].ContactPerson;

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

                    //uoHiddenFieldHotelEmail.Value = "";

                    //if (_HotelInformationList[0].ATTEMail.Count > 0)
                    //{
                    //    uoHiddenFieldHotelEmail.Value = _HotelInformationList[0].ATTEMail[0].Email;
                    //    Session["HotelEmailTo"] = _HotelInformationList[0].ATTEMail;
                    //}

                    Session["ContactPerson"] = _HotelInformationList[0].ContactPerson;
                    Session["ContractRate"] = _HotelInformationList[0].ContractedRate;
                    Session["HotelEmailTo"] = _HotelInformationList[0].EmailTo;


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



        void GetCrewAssistHotelInformation(object obj)
        {
            SF = new CrewAssistBLL();
            Session["ContactPerson"] = null;
            Session["ContractRate"] = null;
            Session["HotelEmailTo"] = null;

            DateTime starDate = DateTime.Now;
            DateTime enddate = DateTime.Now.AddDays(1);

            List<CrewAssistHotelInformation> _HotelInformationList = new List<CrewAssistHotelInformation>();


            _HotelInformationList = SF.CrewAssistHotelInformation(0,
                    GlobalCode.Field2Int(((System.Web.UI.WebControls.ListBox)obj).SelectedValue),
                    "", "", starDate, 0);
            

            uoTextContractedRate.Text = "";
            uoTextBoxMealVoucher.Text = "";
            uoTextBoxComfirmRate.Text = "";
            CheckBoxEmail.Checked = false;
            uoTextBoxEmail.Text = "";
            //CheckBoxFax.Checked = false;
            //CheckBoxFax.Text = "Fax : ";

            //CheckBoxCopycrewassist.Checked = false;
            //CheckBoxCopycrewhotels.Checked = false;



            //if (uoTextBoxStatus.Text.ToString() == "ON")
            //{
            //    if (uoHiddenFieldRequestDate.Value == "")
            //    {
            //        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
            //        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
            //    }
            //    else
            //    {
            //        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(-1));
            //        enddate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
            //    }
            //}
            //else
            //{
            //    if (uoHiddenFieldRequestDate.Value == "")
            //    {
            //        starDate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value));
            //        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
            //    }
            //    else
            //    {
            //        enddate = GlobalCode.Field2DateTime(GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value).AddDays(+1));
            //        starDate = GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value);
            //    }

            //}
            uoTextBoxCheckinDate.Text = GlobalCode.Field2Date(starDate);
            uoTextBoxCheckoutDate.Text = GlobalCode.Field2Date(enddate);

            uoTextBoxDuration.Text = "1";

            uoTxtBoxTimeOut.Text = DateTime.Now.Hour.ToString() + ":00";
            uoTxtBoxTimeIn.Text = DateTime.Now.Hour.ToString() + ":00";

            uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, _HotelInformationList[0].CurrencyID);

            if (_HotelInformationList.Count > 0)
            {
                //uoLabelAdress.Text = _HotelInformationList[0].Address;
                //uoLabelTelephone.Text = _HotelInformationList[0].ContactNo;
                uoHiddenFieldCityCode.Value = _HotelInformationList[0].CityCode;

                uoTextContractedRate.Text = _HotelInformationList[0].ContractedRate;

                uoTextBoxMealVoucher.Text = _HotelInformationList[0].MealVoucher;
                uoTextBoxComfirmRate.Text = _HotelInformationList[0].ContractRoomRateTaxPercentage;

                uoTextBoxEmail.Text = _HotelInformationList[0].EmailTo;
                //CheckBoxFax.Text =  "Fax : " + _HotelInformationList[0].FaxNo;

                //TextBoxWhoConfirm.Text = _HotelInformationList[0].ContactPerson;

                if (uoTextBoxEmail.Text.ToString().Length > 0)
                {
                    CheckBoxEmail.Checked = true;
                    uoTextBoxEmail.Enabled = true;
                }

                uoCheckboxBreakfast.Checked = _HotelInformationList[0].IsBreakfast;
                uoCheckboxLunch.Checked = _HotelInformationList[0].IsLunch;
                uoCheckboxDinner.Checked = _HotelInformationList[0].IsDinner;

                uoCheckBoxIsWithShuttle.Checked = _HotelInformationList[0].IsWithShuttle;

 

                //if (_HotelInformationList[0].ATTEMail.Count > 0)
                //{
                //    uoHiddenFieldHotelEmail.Value = _HotelInformationList[0].ATTEMail[0].Email;
                //}

                Session["ContactPerson"] = _HotelInformationList[0].ContactPerson;
                Session["ContractRate"] = _HotelInformationList[0].ContractedRate;
                Session["HotelEmailTo"] = _HotelInformationList[0].EmailTo;


                uoHiddenFieldContractStart.Value = _HotelInformationList[0].ContractDateStarted;
                uoHiddenFieldContractEnd.Value = _HotelInformationList[0].ContractDateEnd;



                uoDropDownListRoomeType.SelectedValue = GlobalCode.Field2String(_HotelInformationList[0].RoomTypeID);

            }

            //ScriptManager.RegisterStartupScript(Page, GetType(), "key", "HighLightTab();", true);


        }

        protected void uoButtonLoadExcel_click(object sender, EventArgs e)
        {

            //OpenFileDialog openfileDialog1 = new OpenFileDialog();
            //openfileDialog1.ShowDialog();
            //string Test = Server.MapPath();

            string filepath="";

            if (this.uoFileLoadExcel.HasFile)
            {
                this.uoFileLoadExcel.SaveAs(Server.MapPath(uoFileLoadExcel.FileName));
            }

            string[] FileFormat = { ".xls" };
            string FileExt = System.IO.Path.GetExtension(uoFileLoadExcel.FileName);

            // HasFile check to make sure a file selected or not    
            if (uoFileLoadExcel.HasFile)
            {
                // checking file extesion
                if (FileFormat.Contains(FileExt))
                {
                    // save the file to  server path
                    filepath = "~/ExcelFile/" + uoFileLoadExcel.FileName;
                    filepath = MapPath(filepath);
                    uoFileLoadExcel.SaveAs(filepath);

                    ImportDataExcelTOSQL(filepath);

                    //   Label1.Text = "File Uploaded Successfully";
                }
                else
                {
                     AlertMessage( "Invalid File Format");
                }
            }
        
        
        }

        private void ImportDataExcelTOSQL(string filepth)
        {
            string Excelstr="";
            //SqlConnection cnn = new SqlConnection();
            //SqlCommand cmd;
            //string connectionString = "Data Source=(local) ;Initial Catalog=Test;Integrated Security=True";
            //cnn.ConnectionString = connectionString;
            //cnn.Open();
            string strsql = "select * from Studmarks";
           
            //Excelstr = WebConfigurationManager.ConnectionStrings["ExcelCnn"].ConnectionString;

            Excelstr = string.Format(Excelstr,filepth, "Yes"); 
            OleDbConnection Excelcon=new OleDbConnection(Excelstr);
            OleDbCommand Excelcmd=new OleDbCommand();
            OleDbDataAdapter excelda=new OleDbDataAdapter();
            DataTable exceldt=new DataTable();

            Excelcon.Open();
            DataTable excelschema;
            excelschema = Excelcon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string Sheetnm = excelschema.Rows[0]["Table_Name"].ToString();
            Excelcon.Close();

            Excelcon.Open();
            Excelcmd.Connection = Excelcon;
            Excelcmd.CommandText="SELECT * FROM [" + Sheetnm +"]";
            excelda.SelectCommand=Excelcmd;
            excelda.Fill(exceldt);
            Excelcon.Close();

            int k,rwcount;

            rwcount=exceldt.Rows.Count;

            String  Name=""; int slno,Marks;

            for(k=0;k<rwcount;k++)
            {
         
            slno=Convert.ToInt32(exceldt.Rows[k][0].ToString());
            Name=exceldt.Rows[k][1].ToString();
            Marks=Convert.ToInt32(exceldt.Rows[k][2].ToString());

            //strsql = "INSERT INTO Studmarks([idx],[name],[marks]) Values(" + slno +",'"+ Name +"'," + Marks +")";
            //cmd = new SqlCommand(strsql,cnn);
            //cmd.ExecuteNonQuery();

            //}
            //cnn.Close();

            //Label1.Text="Data Imported Successfully...!";
           
          }

}



        protected void Upload(object sender, EventArgs e)
        {
            //uoFileLoadExcel.SaveAs(Server.MapPath("~/Extract/" + Path.GetFileName(uoFileLoadExcel.FileName)));
            //uoFileLoadExcel.FileName = uoHiddenFieldExcelFile.Value;
            try
            {
                if (Session["FileLoadExcel"] != null)
                {
                    FileUpload obj = (FileUpload)Session["FileLoadExcel"];
                    if (obj.HasFile)
                    {
                        //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                        //create the path to save the file to
                        string fileName = Path.Combine(Server.MapPath("~/Extract"), Path.GetFileName(obj.FileName));
                        //save the file to our local path
                        obj.SaveAs(fileName);
                        string FileName = Path.GetFileName(obj.PostedFile.FileName);
                        string Extension = Path.GetExtension(obj.PostedFile.FileName);
                        string FolderPath = Request.PhysicalApplicationPath + "Extract\\" + Path.GetFileName(obj.FileName);

                        switch (Extension)
                        {
                            case ".xls": //Excel 97-03
                            case ".xlsx": //Excel 07
                                Import_To_Grid(FolderPath, Extension, "Yes");
                                break;
                        }
                    }

                }
                Session["FileLoadExcel"] = null;

            }
            catch (Exception ex) { 
                
            }
           
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }



                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                BindImportExcel(FormatDataTable(dt));


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable FormatDataTable(DataTable dt)
        {
            DataTable tables = new DataTable();
            DataRow dr;
            tables.Columns.Add("SeafarerID", typeof(string));
            tables.Columns.Add("Name", typeof(string));
            tables.Columns.Add("Gender", typeof(string));
            tables.Columns.Add("Ship", typeof(string));
            tables.Columns.Add("Location", typeof(string));
            tables.Columns.Add("PickUpTime", typeof(string));
            tables.Columns.Add("DropOffTime", typeof(string));
            tables.Columns.Add("AppntTime", typeof(string));
            tables.Columns.Add("DoctorAppntTypeAdd", typeof(string));
            tables.Columns.Add("Coordinator", typeof(string));
            tables.Columns.Add("RCCLCaseManager", typeof(string));
            if (dt.Rows.Count > 0) {
                foreach (DataRow drow in dt.Rows)
                {
                    if (GlobalCode.Field2Long(drow[1].ToString()) > 0)
                    { 
                        dr = tables.NewRow();
                        dr["SeafarerID"] = drow[1].ToString();
                        dr["Name"] = drow[0].ToString();
                        dr["Gender"] = "";
                        dr["Ship"] =  drow[3].ToString();
                        dr["Location"] = drow[4].ToString();
                        dr["PickUpTime"] = drow[5].ToString();
                        dr["DropOffTime"] = drow[6].ToString();
                        dr["AppntTime"] = drow[7].ToString();
                        dr["DoctorAppntTypeAdd"] = drow[8].ToString();
                        dr["Coordinator"] = drow[9].ToString();
                        dr["RCCLCaseManager"] = drow[10].ToString();
                        tables.Rows.Add(dr);
                    }
                    
                
                } 
            }
                                      

            return tables;
        }


        void BindImportExcel(DataTable dt) {
            try
            {
                uoListviewRequest.DataSource = null;
                uoListviewRequest.DataBind();

                uoListviewRequest.DataSource = dt;
                uoListviewRequest.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }


    }
}

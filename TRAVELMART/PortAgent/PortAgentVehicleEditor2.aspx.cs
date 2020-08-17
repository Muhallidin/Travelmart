using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.Reflection;
using System.IO;

namespace TRAVELMART
{
    public partial class PortAgentVehicleEditor2 : System.Web.UI.Page
    {
        PortAgentBLL BLL = new PortAgentBLL();

        #region "Events"
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Page for the removed records from Exception List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
                if (User.IsInRole(TravelMartVariable.RolePortSpecialist) &&
                    sAction == "")
                {
                    ControlSettings(false);
                    uoLabelTitle.Text = "Vehicle Request: Confirm/Cancel Request";
                    //uoButtonEmail.Text = "Confirm";
                }
                if (sAction == "Add")
                {
                    ControlSettingsEditAmount();
                    uoLabelTitle.Text = "Create Vehicle Request";
                    //uoButtonEmail.Text = "Create";
                    uoHiddenFieldPortID.Value = GlobalCode.Field2Int(Session["Port"]).ToString();
                }
                else
                {
                    uoLabelTitle.Text = "Vehicle Request";
                    ControlSettings(true);
                }

                GetVehicleToConfirm();

                //if role is Service Provider
                if (User.IsInRole(TravelMartVariable.RolePortSpecialist))
                {
                    uoLabelSource.Visible = false;
                    uoDropDownListRequestSource.Visible = false;

                    uoLabelTranspoDetails.Visible = true;
                    uoTextBoxTranspoDetails.Visible = true;
                }
                else
                {
                    uoLabelSource.Visible = true;
                    uoDropDownListRequestSource.Visible = true;

                    uoLabelTranspoDetails.Visible = false;
                    uoTextBoxTranspoDetails.Visible = false;


                }
            }
            else
            {

                if (uoHiddenFieldIsKeyEnter.Value == "1")
                {
                    Label lblRoutFrom;
                    Label lblRoutTo;
                    HiddenField uoHiddenFieldRouteFrom;
                    HiddenField uoHiddenFieldRouteTo;
                    foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
                    {

                        lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                        lblRoutTo = (Label)item.FindControl("lblRouteTo");

                        uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                        uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");
                        lblRoutFrom.Text = uoHiddenFieldRouteFrom.Value;
                        lblRoutTo.Text = uoHiddenFieldRouteTo.Value;
                    }

                    uoHiddenFieldIsKeyEnter.Value = "0";
                
                }

                



            }

            uoListViewVehicleHeader.DataSource = null;
            uoListViewVehicleHeader.DataBind();

           
        }
        protected void uoButtonEmail_Click(object sender, EventArgs e)
        {

            Button uoEmail = (Button)sender;
           
            
            RadioButtonList r = (RadioButtonList)uoVendorSelectionList;
            insertTransportationRequest(r.Text == "Transportation" ? "uspInsNonTurnVehicleRequestBooking" : "uspInsNonTurnPAVehicleRequestBooking", uoEmail.Text);
            //ClosePage("Email successfully sent!");

            //if (uoEmail.Text != "Cancel")
            //{

       

            //}
            //else
            //{
            //    insertTransportationCancel(r.Text == "Transportation" ? "uspInsNonTurnVehicleRequestBooking" : "uspInsNonTurnPAVehicleCancelBooking");
            //    ClosePage("Email successfully sent!");

            
            //}




        }

        void insertTransportationRequest(string spName, string Status)
        {
            short statusID = GlobalCode.Field2TinyInt(Request.QueryString["st"]);
            Label uoLblSfID;
            Label uoLabelLast;
            LinkButton SeafarerLinkButton;
            Label uoLblCheckOut;
            Label uoLblNites;
            Label uoLblRoom;
            Label uoLblVessel;
            Label uoLblRank;
            Label uoLblCostCenter;
            Label uoLblRecLoc;
            HiddenField RecLocID;
            HiddenField TravelReqID;
            HiddenField SeqNo;
            Label lblSFStatus;
            Label lblRoutFrom;
            Label lblRoutTo;

            HiddenField uoHiddenFieldRouteFrom;
            HiddenField uoHiddenFieldRouteTo;
            HiddenField TransVehicleID;

            TextBox TextBoxRateConfirmedPerSeafarer;

            List<NonTurnTransportationRequest> PAH = new List<NonTurnTransportationRequest>();

            foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
            {

                uoLblSfID = (Label)item.FindControl("uoLblSfID");
                uoLabelLast = (Label)item.FindControl("uoLabelLast");
                SeafarerLinkButton = (LinkButton)item.FindControl("SeafarerLinkButton");
                uoLblCheckOut = (Label)item.FindControl("uoLblCheckOut");
                uoLblNites = (Label)item.FindControl("uoLblNites");
                uoLblRoom = (Label)item.FindControl("uoLblRoom");
                lblSFStatus = (Label)item.FindControl("lblStatus");
                uoLblVessel = (Label)item.FindControl("uoLblVessel");
                uoLblRank = (Label)item.FindControl("uoLblRank");
                uoLblCostCenter = (Label)item.FindControl("Label1");
                uoLblRecLoc = (Label)item.FindControl("uoLblRecLoc");
                RecLocID = (HiddenField)item.FindControl("uoHiddenFieldListRecLocID");
                TravelReqID = (HiddenField)item.FindControl("uoHiddenFieldListTRID");
                SeqNo = (HiddenField)item.FindControl("uoHiddenFieldSeqNo");
                lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                lblRoutTo = (Label)item.FindControl("lblRouteTo");
                uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");
                lblRoutFrom.Text = lblRoutFrom.Text == "" ? uoHiddenFieldRouteFrom.Value : lblRoutFrom.Text;
                lblRoutTo.Text = lblRoutFrom.Text == "" ? uoHiddenFieldRouteTo.Value : lblRoutTo.Text;
                TransVehicleID = (HiddenField)item.FindControl("uoHiddenFieldTransID");
                TextBoxRateConfirmedPerSeafarer = (TextBox)item.FindControl("uoTextBoxRateConfirmedPerSeafarer");
                PAH.Add(new NonTurnTransportationRequest
                {
                    TransVehicleID = GlobalCode.Field2Long(TransVehicleID.Value),
                    TravelReqID = GlobalCode.Field2Long(TravelReqID.Value),
                    SeafarerID = GlobalCode.Field2Long(uoLblSfID.Text),
                    IdBigint = GlobalCode.Field2Long(RecLocID.Value),
                    RecordLocator = GlobalCode.Field2String(uoLblRecLoc.Text),

                    SeqNoInt = GlobalCode.Field2Int(SeqNo.Value),
                    VehicleVendorID = GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value),
                    VehiclePlateNo = "",
                    PickUpDate = GlobalCode.Field2DateTime(uoTextBoxPickupDate.Text )  ,
                    PickUpTime = GlobalCode.Field2DateTime(uoTextBoxPickupTime.Text), 
                    DropOffDate = GlobalCode.Field2DateTime(uoTextBoxPickupDate.Text )  ,
                    DropOffTime = GlobalCode.Field2DateTime(uoTextBoxPickupTime.Text),
                    ConfirmationNo  = GlobalCode.Field2String(uoTextBoxConfirmation.Text),
                    VehicleStatus = "Open",
                    VehicleTypeId = GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedItem.Value),
                    RouteIDFrom = GlobalCode.Field2Int(uoDropDownListTFrom.SelectedItem.Value),
                    RouteIDTo   = GlobalCode.Field2Int(uoDropDownListTTo.SelectedItem.Value),

                    RouteFromVarchar = GlobalCode.Field2String(lblRoutFrom.Text),
                    RouteToVarchar = GlobalCode.Field2String(lblRoutTo.Text),

                    ContractId = GlobalCode.Field2Int(    uoHiddenFieldContractID.Value),
                    DriverID = 0,
                    SFStatus = GlobalCode.Field2String(lblSFStatus.Text),
                    
                    ConfirmBy  = uoTextBoxConfirmedBy.Text,   
                    Comments = uoTextBoxComment.Text  ,
                    StatusID = statusID, //(short)(Status == "Cancel" ? 5 : statusID),

                    ConfirmedRateMoney = GlobalCode.Field2Double(TextBoxRateConfirmedPerSeafarer.Text)  

                });
            }


            GlobalCode gc = new GlobalCode();
            DataTable dt = new DataTable();

            dt = gc.getDataTable(PAH);

            PortAgentBLL PortBLL = new PortAgentBLL();
            PortBLL.InsertNonTurnTransactionRequestBooking(dt, spName, uoHiddenFieldUser.Value , uoTextBoxEmailAdd.Text , uoTextBoxCopy.Text   );

        

        }



        void insertTransportationCancel(string spName)
        {

            Label uoLblSfID;
            Label uoLabelLast;
            LinkButton SeafarerLinkButton;
            Label uoLblCheckOut;
            Label uoLblNites;
            Label uoLblRoom;
            Label uoLblVessel;
            Label uoLblRank;
            Label uoLblCostCenter;
            Label uoLblRecLoc;
            HiddenField RecLocID;
            HiddenField TravelReqID;
            HiddenField SeqNo;
            Label lblSFStatus;
            Label lblRoutFrom;
            Label lblRoutTo;
            HiddenField TransID;
            HiddenField uoHiddenFieldRouteFrom;
            HiddenField uoHiddenFieldRouteTo;

            List<NonTurnTransportationCancelRequest> PAH = new List<NonTurnTransportationCancelRequest>();

            foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
            {
                TransID = (HiddenField)item.FindControl("uoHiddenFieldTransID");
                uoLblSfID = (Label)item.FindControl("uoLblSfID");
                uoLabelLast = (Label)item.FindControl("uoLabelLast");
                SeafarerLinkButton = (LinkButton)item.FindControl("SeafarerLinkButton");
                uoLblCheckOut = (Label)item.FindControl("uoLblCheckOut");
                uoLblNites = (Label)item.FindControl("uoLblNites");
                uoLblRoom = (Label)item.FindControl("uoLblRoom");
                lblSFStatus = (Label)item.FindControl("lblStatus");
                uoLblVessel = (Label)item.FindControl("uoLblVessel");
                uoLblRank = (Label)item.FindControl("uoLblRank");
                uoLblCostCenter = (Label)item.FindControl("Label1");
                uoLblRecLoc = (Label)item.FindControl("uoLblRecLoc");
                RecLocID = (HiddenField)item.FindControl("uoHiddenFieldListRecLocID");
                TravelReqID = (HiddenField)item.FindControl("uoHiddenFieldListTRID");
                SeqNo = (HiddenField)item.FindControl("uoHiddenFieldSeqNo");

                lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                lblRoutTo = (Label)item.FindControl("lblRouteTo");

                uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");

                lblRoutFrom.Text = lblRoutFrom.Text == "" ? uoHiddenFieldRouteFrom.Value : lblRoutFrom.Text;
                lblRoutTo.Text = lblRoutFrom.Text == "" ? uoHiddenFieldRouteTo.Value : lblRoutTo.Text;


                PAH.Add(new NonTurnTransportationCancelRequest
                {
                    TransVehicleID = GlobalCode.Field2Long(TransID.Value),
                    TravelReqID = GlobalCode.Field2Long(TravelReqID.Value),
                    SeafarerID = GlobalCode.Field2Long(uoLblSfID.Text),
                    IdBigint = GlobalCode.Field2Long(RecLocID.Value),
                    RecordLocator = GlobalCode.Field2String(uoLblRecLoc.Text),
                    SeqNoInt = GlobalCode.Field2Int(SeqNo.Value),
                    VehicleVendorID = GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value),
                    PickUpDate = GlobalCode.Field2DateTime(uoTextBoxPickupDate.Text),
                    PickUpTime = GlobalCode.Field2DateTime(uoTextBoxPickupTime.Text),
                    DropOffDate = GlobalCode.Field2DateTime(uoTextBoxPickupDate.Text),
                    DropOffTime = GlobalCode.Field2DateTime(uoTextBoxPickupTime.Text),
                    ConfirmationNo = GlobalCode.Field2String(uoTextBoxConfirmation.Text),
                    VehicleStatus = "Cancel",
                    VehicleTypeId = GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedItem.Value),
                    RouteIDFrom = GlobalCode.Field2Int(uoDropDownListTFrom.SelectedItem.Value),
                    RouteIDTo = GlobalCode.Field2Int(uoDropDownListTTo.SelectedItem.Value),
                    RouteFromVarchar = GlobalCode.Field2String(lblRoutFrom.Text),
                    RouteToVarchar = GlobalCode.Field2String(lblRoutTo.Text),
                    ContractId = GlobalCode.Field2Int(uoHiddenFieldContractID.Value),
                    SFStatus = GlobalCode.Field2String(lblSFStatus.Text),
                    ConfirmBy = uoTextBoxConfirmedBy.Text,
                    Comments = uoTextBoxComment.Text
                });
            }


            GlobalCode gc = new GlobalCode();
            DataTable dt = new DataTable();

            dt = gc.getDataTable(PAH);

            PortAgentBLL PortBLL = new PortAgentBLL();
            PortBLL.InsertNonTurnTransactionRequestBooking(dt, spName, uoHiddenFieldUser.Value, uoTextBoxEmailAdd.Text, uoTextBoxCopy.Text);



        }







        protected void uoVendorDropDownType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //uoTextBoxHotelname.Text = "";
                //uoTextBoxEmailAdd.Text = "";
                //uoTextBoxRateContract.Text = "";
                //uoTextBoxRateConfirmed.Text = "";
                //uoTextBoxCheckInDate.Text = "";
                //uoDropDownListCurrency.SelectedIndex = 0;
                //uoHiddenFieldContractID.Value = "";
                //uoHiddenFieldVoucher.Value = "";

                DropDownList ddl = (DropDownList)sender; 
                if (ViewState["NonTurnTransportation"] != null)
                {
                    List<NonTurnTransportation> ls = (List<NonTurnTransportation>)ViewState["NonTurnTransportation"];

                    var lst = ls.Where(n => n.VendorID == GlobalCode.Field2Int(ddl.SelectedItem.Value)).ToList();

                    if (lst.Count > 0)
                    {

                        DropDownList cbo = (DropDownList)sender;

                        uoTextBoxEmailAdd.Text = lst[0].Email;

                        uoTextBoxRateContract.Text = lst[0].Rate.ToString() ;
                        uoTextBoxRateConfirmed.Text = lst[0].Rate.ToString();
                       
                        
                        uoTextBoxPickupDate.Text = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToString() ;
                        uoTextBoxPickupTime.Text = "07:00";

                        RadioButtonList r = (RadioButtonList)RadioButtonList1;

                        Label lblRoutFrom;
                        Label lblRoutTo;
                        HiddenField uoHiddenFieldRouteFrom;
                        HiddenField uoHiddenFieldRouteTo;

                        HiddenField uoHiddenFieldAiportArr;
                        HiddenField uoHiddenFieldAiportDept;

                        HiddenField uoHiddenFieldHotel;
                        HiddenField uoHiddenFieldSeaport;


                        if (r.Text == "On")
                        {
                            uoDropDownListTFrom.SelectedIndex = 3;
                            uoDropDownListTTo.SelectedIndex = 2;

                            


                            foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
                            {


                                lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                                lblRoutTo = (Label)item.FindControl("lblRouteTo");

                                uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                                uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");

                                uoHiddenFieldAiportArr = (HiddenField)item.FindControl("uoHiddenFieldAiportArr");
                                uoHiddenFieldHotel = (HiddenField)item.FindControl("uoHiddenFieldHotel");


                                lblRoutFrom.Text = uoHiddenFieldAiportArr.Value;
                                lblRoutTo.Text = uoHiddenFieldHotel.Value;

                                uoHiddenFieldRouteFrom.Value = uoHiddenFieldAiportArr.Value;
                                uoHiddenFieldRouteTo.Value = uoHiddenFieldHotel.Value;


                                 


                            }




                        }
                        else if (r.Text == "Off")
                        {
                            uoDropDownListTFrom.SelectedIndex = 1;
                            uoDropDownListTTo.SelectedIndex = 2;


                            foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
                            {


                                lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                                lblRoutTo = (Label)item.FindControl("lblRouteTo");
                                uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                                uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");


                                uoHiddenFieldHotel = (HiddenField)item.FindControl("uoHiddenFieldHotel");
                                uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");


                                lblRoutFrom.Text = uoHiddenFieldSeaport.Value;
                                lblRoutTo.Text = uoHiddenFieldHotel.Value;

                                uoHiddenFieldRouteFrom.Value = uoHiddenFieldSeaport.Value;
                                uoHiddenFieldRouteTo.Value = uoHiddenFieldHotel.Value;





                            }






                        }



                       

                        uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, lst[0].CurrentcyID);
                        uoHiddenFieldContractID.Value = lst[0].ContractID.ToString();
                         

                    }

                }

              




            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }



















        protected void uoListViewVehicleHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrder.Value = e.CommandName;
            //BindVehicleManifestWithOrder();
            //uoListviewVehicleInfoPager.SetPageProperties(0, uoListviewVehicleInfoPager.PageSize, false);            
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    set alternate color
        /// </summary>
        /// <returns></returns> 
        string lastDataFieldValue = null;
        string lastClass = "alternateBg";


        public string OverflowChangeRowColor()
        {

            string currentDataFieldValue = Eval("SeafarerIdInt").ToString();
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
        
       
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   12/27/2012
        /// Description:    close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            //sScript += "var msg = '" + s + "';";
            //sScript += "alert( msg );";

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldNonTurnPageRoute\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            //sScript += " self.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/Dec/2013
        /// Description:    Alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {

            string sScript = "<script language='JavaScript'>";
            sScript += "var msg = '" + s + "';";
            sScript += "alert( msg );";

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldNonTurnPageRoute\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            //sScript += " window.parent.RefreshPageFromPopupVehicle(); ";
            sScript += " self.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   11/Mar/2014
        /// Description:    Alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertErrorMessage(string s)
        {

            string sScript = "<script language='JavaScript'>";
            s = s.Replace("'", " ");
            sScript += "var msg = '" + s + "';";
            sScript += "alert( msg );";            
            sScript += "</script>";

            //ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2015
        /// Description:    Alert message without assigning value to uoHiddenFieldVehicleConfirm from parent form
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessageNoRefresh(string s)
        {

            string sScript = "<script language='JavaScript'>";
            sScript += "var msg = '" + s + "';";
            sScript += "alert( msg );";

            sScript += " parent.$.fancybox.close(); ";
            sScript += " self.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   08/Mar/2014
        /// Description:    Bind List of vehicle manifest to confirm
        /// </summary>
        private void GetVehicleToConfirm()
        {
            //string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
            //if (GlobalCode.Field2String(Request.QueryString["AddCancel"]) == "")
            //{
            //    AlertMessage("No record to process!");
            //}
            //else if (sAction == "Add")
            //{
            //     GetVehicleManifestToAdd();
            //}
            //else
            //{
            //    GetVehicleManifestToConfirm();
            //}

            GetVehicleManifestToAdd();


        }
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

        public static Type GetCoreType(Type t)
        {
            return t;
        }


        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        private DataTable CreateTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();



            //table.Columns.Add("IDBigint", typeof(int));
            //table.Columns.Add("TravelReqID", typeof(string));
            //table.Columns.Add("SeqNoInt", typeof(string));

            //table.Columns.Add("ReqDate", typeof(DateTime));
            //table.Columns.Add("PortID", typeof(int));
            //table.Columns.Add("UserID", typeof(string));

            table.Columns.Add("TransVehicleID", typeof(long));
            table.Columns.Add("IDBigint", typeof(long));
            table.Columns.Add("TravelReqID", typeof(long));
            table.Columns.Add("SeqNo", typeof(int));
            table.Columns.Add("ReqDate", typeof(DateTime));
            table.Columns.Add("PortID", typeof(int));
            table.Columns.Add("UserID", typeof(string));




            return table;
        }


        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        private DataTable CreateTableCancelled()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();


            table.Columns.Add("TransVehicleID", typeof(long));
            table.Columns.Add("IDBigint", typeof(int));
            table.Columns.Add("TravelReqID", typeof(long));
            table.Columns.Add("SeqNoInt", typeof(int));

            table.Columns.Add("ReqDate", typeof(DateTime));
            table.Columns.Add("PortID", typeof(int));
            table.Columns.Add("UserID", typeof(string));
             

            return table;
        }



        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   12/Apr/2015
        /// Description:    Get the hotel to confirm from Non Turn Port Page
        /// </summary>
        private void GetVehicleManifestToAdd()
        {
            try
            {

                //ViewState["NonTurnTransportation"] = null;

                DataTable table = new DataTable();

                table = CreateTable();

                string dt = GlobalCode.Field2String(Request.QueryString["dt"]);
                var qRow = Request.QueryString["qTravelReqID"];
                int PortID = GlobalCode.Field2Int(Request.QueryString["pID"]);
                int PortAgentID = GlobalCode.Field2Int(Request.QueryString["paID"]);
                int st = GlobalCode.Field2Int(Request.QueryString["st"]);
                int row = GlobalCode.Field2Int(Request.QueryString["pRow"]);

                //document.getEl6ementById("uoNonTurnPageRoute").href = "../PortAgent/PortAgentVehicleEditor2.aspx?Action=Add&AddCancel=Add" + 
                //    "&dt=" + chdate + "&qTravelReqID=" + mydata + "&pID=" + 0 + "&pRow=" + n + "&paID=" + getQuerystringData("PA") + "&st=" + getQuerystringData("st");
                //document.getElementById("uoNonTurnPageRoute").click();


                string userID = uoHiddenFieldUser.Value;
                string[] TableRow = null;

                if (qRow != null)
                {
                    TableRow = qRow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                DataRow dr;
                int c1 = 0; int c2 = 1; int c3 = 2;  int c4 = 3;
              

                if (TableRow != null)
                {
                    for (var i = 0; i < row; i++)
                    {
                        dr = table.NewRow();


                        dr["TransVehicleID"] = TableRow[c4];
                        dr["IDBigint"] = TableRow[c1];

                        dr["TravelReqID"] = TableRow[c2];
                        dr["SeqNo"] = TableRow[c3];
                        dr["ReqDate"] = GlobalCode.Field2Time(dt);
                        dr["PortID"] = PortID;
                        dr["UserID"] = userID;




                        table.Rows.Add(dr);

                        c1 = c1 + 4;
                        c2 = c2 + 4;
                        c3 = c3 + 4;
                        c4 = c4 + 4;


                    }
                }
                

                PortAgentBLL PortBLL = new PortAgentBLL();
                List<NonTurnportGenericList> lst = new List<NonTurnportGenericList>();
                lst = PortBLL.GetPortNonTurnTransportationRequest(0, GlobalCode.Field2Time(dt), PortID, PortAgentID, userID, table);


                //=======================

                uoVendorDropDownList.DataSource = lst[0].NonTurnTransportation;
                uoVendorDropDownList.DataTextField = "VendorName";
                uoVendorDropDownList.DataValueField = "VendorID";
                uoVendorDropDownList.DataBind();
                uoVendorDropDownList.Items.Insert(0, new ListItem("--Select Vendor--", "0"));


                uoVendorDropDownList.SelectedIndex = GlobalCode.GetselectedIndex(uoVendorDropDownList, PortAgentID);



                if (lst[0].NonTurnTransportation.Count > 0)
                {
                    var res = lst[0].NonTurnTransportation.Where(e => e.VendorID == PortAgentID).ToList();
                    if (res.Count > 0)
                    {
                        uoTextBoxEmailAdd.Text = res[0].Email.ToString();
                    }
                }




                uoDropDownListCurrency.DataSource = lst[0].Currency;
                uoDropDownListCurrency.DataTextField = "Name";
                uoDropDownListCurrency.DataValueField = "ID";
                uoDropDownListCurrency.DataBind();
                uoDropDownListCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));



                uoDropDownListVehicleType.DataSource = lst[0].VehicleType;
                uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
                uoDropDownListVehicleType.DataValueField = "VehicleTypeID";
                uoDropDownListVehicleType.DataBind();
                uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));



                uoDropDownListRequestSource.DataSource = lst[0].Requestor;
                uoDropDownListRequestSource.DataTextField = "Name";
                uoDropDownListRequestSource.DataValueField = "ID";
                uoDropDownListRequestSource.DataBind();
                uoDropDownListRequestSource.Items.Insert(0, new ListItem("--Select Request Source--", "0"));
                uoDropDownListRequestSource.SelectedIndex = 2;

                ViewState["NonTurnTransportation"] = lst[0].NonTurnTransportation;

                uoListviewVehicleInfo.DataSource = lst[0].PortAgentVehicleManifestList;
                uoListviewVehicleInfo.DataBind();
               
                RadioButtonList1.Visible = false;

                if (lst[0].PortAgentVehicleManifestList.Count > 0)
                {
                    uoTextBoxPickupDate.Text = GlobalCode.Field2Date(lst[0].PortAgentVehicleManifestList[0].PickupDate);
                    uoTextBoxPickupTime.Text = GlobalCode.Field2String(lst[0].PortAgentVehicleManifestList[0].PickupTime);
                    uoTextBoxRateContract.Text = GlobalCode.Field2String(lst[0].PortAgentVehicleManifestList[0].RateContracted);

                    uoDropDownListTFrom.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListTFrom, lst[0].PortAgentVehicleManifestList[0].RouteFromID);
                    uoDropDownListTTo.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListTTo, lst[0].PortAgentVehicleManifestList[0].RouteToID);
                    uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, lst[0].PortAgentVehicleManifestList[0].CurrencyID);
                }














                
                //ViewState["NonTurnTransportation"] = lst[0].NonTurnTransportation;
                uoListviewVehicleInfo.DataSource = lst[0].PortAgentVehicleManifestList;

                uoListviewVehicleInfo.DataBind();


            }
            catch
            {


            }
             
        }



        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   12/Apr/2015
        /// Description:    Get the hotel to confirm from Non Turn Port Page
        /// </summary>
        private void GetVehicleManifestToConfirm()
        {
            try
            {

                ViewState["NonTurnTransportation"] = null;

                DataTable table = new DataTable();

                table = CreateTableCancelled();

                string dt = GlobalCode.Field2String(Request.QueryString["dt"]);

                var qRecLocRow = Request.QueryString["RecLoc"];
                var qTReqIDRow = Request.QueryString["TReqID"];
                var qTransIDRow = Request.QueryString["TransID"];

                int PortID = GlobalCode.Field2Int(Request.QueryString["pID"]);
                int row = GlobalCode.Field2Int(Request.QueryString["pRow"]);
                int PortAgentID = GlobalCode.Field2Int(Request.QueryString["pPAID"]);

                string userID = uoHiddenFieldUser.Value;
                string[] TRTableRow = null;
                string[] ReclocTableRow = null;
                string[] TransTableRow = null;

                if (qTReqIDRow != null)
                {
                    TRTableRow = qTReqIDRow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }

                if (qRecLocRow != null)
                {
                    ReclocTableRow = qRecLocRow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (qTransIDRow != null)
                {
                    TransTableRow = qTransIDRow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }


                DataRow dr;
               

                //if (TRTableRow != null)
                //{
                    for (var i = 0; i < row; i++)
                    {
                        dr = table.NewRow();

                        dr["TransVehicleID"] = TransTableRow[i] == null ? 0 : GlobalCode.Field2Long(TransTableRow[i]);
                        dr["IDBigint"] = ReclocTableRow[i] == null ? 0 : GlobalCode.Field2Long(ReclocTableRow[i]);
                        dr["TravelReqID"] = TRTableRow[i] == null ? 0 : GlobalCode.Field2Long(TRTableRow[i]);


                        dr["SeqNoInt"] = 0; 

                        dr["ReqDate"] = GlobalCode.Field2Time(dt);
                        dr["PortID"] = PortID;
                        dr["UserID"] = userID;
                        table.Rows.Add(dr);
 

                    }
                 


                PortAgentBLL PortBLL = new PortAgentBLL();
                List<NonTurnportGenericList> lst = new List<NonTurnportGenericList>();
                lst = PortBLL.GetPortNonTurnTransportationRequest(2, GlobalCode.Field2Time(dt), PortID, userID, table);

                uoVendorDropDownList.DataSource = lst[0].NonTurnTransportation;
                uoVendorDropDownList.DataTextField = "VendorName";
                uoVendorDropDownList.DataValueField = "VendorID";
                uoVendorDropDownList.DataBind();
                uoVendorDropDownList.Items.Insert(0, new ListItem("--Select Vendor--", "0"));


                uoVendorDropDownList.SelectedIndex = GlobalCode.GetselectedIndex(uoVendorDropDownList, PortAgentID);



                if (lst[0].NonTurnTransportation.Count > 0) {

                    var res = lst[0].NonTurnTransportation.Where(e => e.VendorID == PortAgentID).ToList();


                    if (res.Count > 0) {
                        uoTextBoxEmailAdd.Text = res[0].Email.ToString();
                    }
                }




                uoDropDownListCurrency.DataSource = lst[0].Currency;
                uoDropDownListCurrency.DataTextField = "Name";
                uoDropDownListCurrency.DataValueField = "ID";
                uoDropDownListCurrency.DataBind();
                uoDropDownListCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));



                uoDropDownListVehicleType.DataSource = lst[0].VehicleType;
                uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
                uoDropDownListVehicleType.DataValueField = "VehicleTypeID";
                uoDropDownListVehicleType.DataBind();
                uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));



                uoDropDownListRequestSource.DataSource = lst[0].Requestor;
                uoDropDownListRequestSource.DataTextField = "Name";
                uoDropDownListRequestSource.DataValueField = "ID";
                uoDropDownListRequestSource.DataBind();
                uoDropDownListRequestSource.Items.Insert(0, new ListItem("--Select Request Source--", "0"));
                uoDropDownListRequestSource.SelectedIndex = 2;

                ViewState["NonTurnTransportation"] = lst[0].NonTurnTransportation;

                uoListviewVehicleInfo.DataSource = lst[0].PortAgentVehicleManifestList;
                uoListviewVehicleInfo.DataBind();
                uoButtonEmail.Text = "Cancel";

                RadioButtonList1.Visible = false;

                if (lst[0].PortAgentVehicleManifestList.Count > 0) {
                    uoTextBoxPickupDate.Text = GlobalCode.Field2Date(lst[0].PortAgentVehicleManifestList[0].PickupDate);
                    uoTextBoxPickupTime.Text = GlobalCode.Field2String(lst[0].PortAgentVehicleManifestList[0].PickupTime);
                    uoTextBoxRateContract .Text = GlobalCode.Field2String(lst[0].PortAgentVehicleManifestList[0].RateContracted);


                    uoDropDownListTFrom.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListTFrom, lst[0].PortAgentVehicleManifestList[0].RouteFromID);
                    uoDropDownListTTo.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListTTo, lst[0].PortAgentVehicleManifestList[0].RouteToID);
                    uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, lst[0].PortAgentVehicleManifestList[0].CurrencyID); 
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }



         
        protected void uoVendorType_Click(object sender, EventArgs e)
        {

            ViewState["NonTurnTransportation"] = null;
            PortAgentBLL  PortBLL = new PortAgentBLL();
            List<NonTurnTransportation> lst = new List<NonTurnTransportation>();
            RadioButtonList r = (RadioButtonList)sender;
            if (r.Text == "Transportation")
            {
                lst = PortBLL.GetNonTurnporTransportionVendor(1, GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value), 0);
            }
            else if (r.Text == "Service Provider")
            {
                lst = PortBLL.GetNonTurnporTransportionVendor(0, GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value), 0);
            }

            if (lst.Count > 0)
            {
                uoVendorDropDownList.DataSource = lst;
                uoVendorDropDownList.DataTextField = "VendorName";
                uoVendorDropDownList.DataValueField = "VendorID";
                uoVendorDropDownList.DataBind();
                uoVendorDropDownList.Items.Insert(0, new ListItem("--Select Vendor--", "0"));
            }

             Label lblRoutFrom;
             Label lblRoutTo;
             HiddenField uoHiddenFieldRouteFrom;
             HiddenField uoHiddenFieldRouteTo;

             HiddenField uoHiddenFieldAiportArr;

             HiddenField uoHiddenFieldHotel;
             HiddenField uoHiddenFieldSeaport;

             if (r.Text == "On")
             {
                 uoDropDownListTFrom.SelectedIndex = 3;
                 uoDropDownListTTo.SelectedIndex = 2;
                 foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
                 {
                     lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                     lblRoutTo = (Label)item.FindControl("lblRouteTo");

                     uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                     uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");

                     uoHiddenFieldAiportArr = (HiddenField)item.FindControl("uoHiddenFieldAiportArr");
                     uoHiddenFieldHotel = (HiddenField)item.FindControl("uoHiddenFieldHotel");


                     lblRoutFrom.Text = uoHiddenFieldAiportArr.Value;
                     lblRoutTo.Text = uoHiddenFieldHotel.Value;

                     uoHiddenFieldRouteFrom.Value = uoHiddenFieldAiportArr.Value;
                     uoHiddenFieldRouteTo.Value = uoHiddenFieldHotel.Value;
                 }
             }
             else if (r.Text == "Off")
             {
                 uoDropDownListTFrom.SelectedIndex = 1;
                 uoDropDownListTTo.SelectedIndex = 2;
                 foreach (ListViewDataItem item in uoListviewVehicleInfo.Items)
                 {
                     lblRoutFrom = (Label)item.FindControl("lblRouteFrom");
                     lblRoutTo = (Label)item.FindControl("lblRouteTo");
                     uoHiddenFieldRouteFrom = (HiddenField)item.FindControl("uoHiddenFieldRouteFrom");
                     uoHiddenFieldRouteTo = (HiddenField)item.FindControl("uoHiddenFieldRouteTo");
                     uoHiddenFieldHotel = (HiddenField)item.FindControl("uoHiddenFieldHotel");
                     uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");
                     lblRoutFrom.Text = uoHiddenFieldSeaport.Value;
                     lblRoutTo.Text = uoHiddenFieldHotel.Value;
                     uoHiddenFieldRouteFrom.Value = uoHiddenFieldSeaport.Value;
                     uoHiddenFieldRouteTo.Value = uoHiddenFieldHotel.Value;
                 }
             }
            ViewState["NonTurnTransportation"] = lst;
        } 

        private void ControlSettings(bool bIsReadOnly)
        {
            //uoTextBoxHotelname.ReadOnly = bIsReadOnly;
            uoTextBoxConfirmation.ReadOnly = bIsReadOnly;
            uoTextBoxRateConfirmed.ReadOnly = bIsReadOnly;
            
            uoDropDownListCurrency.Enabled = !bIsReadOnly;
            uoDropDownListVehicleType.Enabled = !bIsReadOnly;


            uoTextBoxDriver.ReadOnly = bIsReadOnly;
            uoTextBoxPlateNo.ReadOnly = bIsReadOnly;

            if (bIsReadOnly)
            {
                //uoTextBoxHotelname.CssClass = "ReadOnly";
                uoTextBoxConfirmation.CssClass = "ReadOnly";
                uoTextBoxRateConfirmed.CssClass = "ReadOnly";

                uoTextBoxDriver.CssClass = "ReadOnly";
                uoTextBoxPlateNo.CssClass = "ReadOnly";
            }
        }
        private void ControlSettingsEditAmount()
        {
            //uoTextBoxHotelname.ReadOnly = true;
            uoTextBoxConfirmation.ReadOnly = true;
            uoTextBoxDriver.ReadOnly = true;
            uoTextBoxPlateNo.ReadOnly = true;
            uoDropDownListVehicleType.Enabled = true;

            //uoTextBoxHotelname.CssClass = "ReadOnly";
            uoTextBoxConfirmation.CssClass = "ReadOnly";
            uoTextBoxDriver.CssClass = "ReadOnly";
            uoTextBoxPlateNo.CssClass = "ReadOnly";

        }
       
        
        #endregion
    }
}

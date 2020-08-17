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
    public partial class HotelEditor5 : System.Web.UI.Page
    {

        PortAgentBLL PortBLL = new PortAgentBLL();
        HotelBLL HBLL = new HotelBLL();

        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
           

                string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
                 
                 if (sAction == "Add")
                {
                    ControlSettingsEditAmount();
                    uoLabelTitle.Text = "Approve Hotel Request";
                    uoButtonEmail.Text = "Approve";
                    uoHiddenFieldPortID.Value = GlobalCode.Field2Int(Session["Port"]).ToString();
                }
                 else if (sAction == "Cancel")
                 {
                     ControlSettingsEditAmount();
                     uoLabelTitle.Text = "Cancel Hotel Request";
                     uoButtonEmail.Text = "Cancel";
                     uoHiddenFieldPortID.Value = GlobalCode.Field2Int(Session["Port"]).ToString();
                 }
                 else
                 {
                     uoLabelTitle.Text = "Hotel Request";
                     ControlSettings(true);
                 }

                GetHotelToConfirm();

                //if role is Service Provider
                if (uoHiddenFieldRole.Value != TravelMartVariable.RolePortSpecialist ||
                    uoHiddenFieldRole.Value != TravelMartVariable.RoleVehicleVendor)
                {
                    uoLabelSource.Visible = true;
                    uoDropDownListRequestSource.Visible = true;
                    BindRequestSource();
                }
                else
                {
                    uoLabelSource.Visible = false;
                    uoDropDownListRequestSource.Visible = false;
                }
            }
            uoListViewHotelHeader.DataSource = null;
            uoListViewHotelHeader.DataBind();

            uoListViewCancelHeader.DataSource = null;
            uoListViewCancelHeader.DataBind();

        }

        protected void uoVendorDropDownType_Click(object sender, EventArgs e) 
        {
            DataTable dt = null;
            try
            {
                DropDownList dd = (DropDownList)sender;
                if (dd.ID == "uoDropDownListRequestSource")
                {
                    return; 
                }

                uoTextBoxHotelname.Text = "";
                uoTextBoxEmailAdd.Text = "";
                uoTextBoxRateContract.Text ="";
                uoTextBoxRateConfirmed.Text = "";
                uoTextBoxCheckInDate.Text = "";
                uoDropDownListCurrency.SelectedIndex = 0;
                uoHiddenFieldContractID.Value =  "";
                uoHiddenFieldVoucher.Value =  "";

                DropDownList ddl = (DropDownList)sender; 
                if (ViewState["PortAgenRequestVendor"] != null)
                { 
                    
                    List<PortAgenRequestVendor> ls = (List<PortAgenRequestVendor>)ViewState["PortAgenRequestVendor"];
                    var lst = ls.Where(n => n.VendorID == GlobalCode.Field2Int(ddl.SelectedItem.Value)).ToList();
                    if (lst.Count > 0)
                     { 
                     
                         DropDownList cbo = (DropDownList)sender;
                         uoTextBoxHotelname.Text = cbo.SelectedItem.Text;
                         uoTextBoxEmailAdd.Text = lst[0].EmailTo;
                         uoTextBoxRateContract.Text = lst[0].DoubleRate.ToString();
                         uoTextBoxRateConfirmed.Text = lst[0].DoubleRate.ToString() ;
                         uoTextBoxCheckInDate.Text =  GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToString();
                         uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex( uoDropDownListCurrency, lst[0].CurrentID);
                         uoHiddenFieldContractID.Value = lst[0].VendorContractID.ToString();
                         uoHiddenFieldVoucher.Value = lst[0].Voucher.ToString() ;
                     }
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

        protected void uoVendorType_Click(object sender, EventArgs e)
        {

            ViewState["PortAgenRequestVendor"] = null ;
            
            PortBLL = new PortAgentBLL();

            List<PortAgenRequestVendor> lst = new List<PortAgenRequestVendor>();
            RadioButtonList r = (RadioButtonList)sender;

            if (r.Text  == "Hotel")
            {
                lst =  PortBLL.GetNonTurnporHotelProviderVendor(1, GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value) , 0  );
            }
            else if (r.Text == "Service Provider")
            {
                lst = PortBLL.GetNonTurnporHotelProviderVendor(0, GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value), 0);
            }

            if (lst.Count > 0)
            { 
               
                uoVendorDropDownList.DataSource = lst;
                uoVendorDropDownList.DataTextField = "VendorName";
                uoVendorDropDownList.DataValueField = "VendorID";

                uoVendorDropDownList.DataBind();
                uoVendorDropDownList.Items.Insert(0, new ListItem("--Select Vendor--", "0"));
            
            }

            ViewState["PortAgenRequestVendor"] = lst;

        }
        protected void uoButtonEmail_Click(object sender, EventArgs e)
        {

            RadioButtonList r = (RadioButtonList)uoVendorSelectionList;


            //insertHotelrequest(r.Text == "Hotel" ? "uspInsNonTurnHotelTransactionOther" : "uspInsNonTurnHotelTransPortAgent");
            //insertHotelrequest(r.Text != "Hotel" ? "uspInsNonTurnPortApproveHotelTrans" : "uspInsNonTurnHotelTransPortAgent", uoButtonEmail.Text);
            insertHotelrequest(r.Text != "Hotel" ? "uspInsNonTurnPortApproveHotelTrans" : "uspInsNonTurnPortApproveHotelTrans", uoButtonEmail.Text);

            ClosePage("Email successfully sent!");
        }

        void insertHotelrequest(string spName, string status)
        {

            
            int? StatusID = null;

            List<NonTurnRequestBooking> PAH = new List<NonTurnRequestBooking>();
            foreach (ListViewDataItem item in uolistviewHotelInfo.Items)
            {

                  if (status =="Cancel")
                        StatusID = 5;
                  else
                       StatusID =  GlobalCode.Field2Int(((HiddenField)item.FindControl("uoHiddenFieldStatusID")).Value);
            

                PAH.Add(new NonTurnRequestBooking
                {
                    TransHotelID = GlobalCode.Field2Long(((HiddenField)item.FindControl("uoHiddenFieldTransHotelID")).Value),
                    TravelReqID = GlobalCode.Field2Long(((HiddenField)item.FindControl("uoHiddenFieldListTRID")).Value),
                    SeafarerID = GlobalCode.Field2Long(((Label)item.FindControl("uoLblSfID")).Text),
                    IdBigint = GlobalCode.Field2Long(((HiddenField)item.FindControl("uoHiddenFieldListRecLocID")).Value),
                    RecordLocator = GlobalCode.Field2String(((Label)item.FindControl("uoLblRecLoc")).Text),
                    SeqNo = GlobalCode.Field2Int(((HiddenField)item.FindControl("uoHiddenFieldSeqNo")).Value),
                    VendorID = GlobalCode.Field2Int(uoVendorDropDownList.SelectedItem.Value),
                    RoomTypeID = ((Label)item.FindControl("uoLblRoom")).Text == "double" ? 2 : 1,
                    CheckIn = GlobalCode.Field2DateTime(uoTextBoxCheckInDate.Text),
                    CheckOut = GlobalCode.Field2DateTime(((Label)item.FindControl("uoLblCheckOut")).Text),
                    Duration = GlobalCode.Field2Int(((Label)item.FindControl("uoLblNites")).Text) <= 0 ? 1 : GlobalCode.Field2Int(((Label)item.FindControl("uoLblNites")).Text),
                    VoucherAmount = GlobalCode.Field2Double(uoHiddenFieldVoucher.Value),
                    ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value),
                    ApprovedBy = GlobalCode.Field2String(uoTextBoxConfirmedBy.Text),
                    ApprovedDate = DateTime.Now.Date,
                    RoomCount = GlobalCode.Field2Float((((Label)item.FindControl("uoLblRoom"))).Text == "double" ? 0.5 : 1.0),
                    HotelName = GlobalCode.Field2String(uoTextBoxHotelname.Text),
                    //ConfirmRateMoney = GlobalCode.Field2Double(uoTextBoxRateConfirmed.Text),
                    //ContractedRateMoney = GlobalCode.Field2Double(uoTextBoxRateContract.Text),

                    ConfirmRateMoney = GlobalCode.Field2Double(((TextBox)item.FindControl("txtContractedRate")).Text),
                    ContractedRateMoney= GlobalCode.Field2Double(((Label)item.FindControl("lblContractedRate")).Text),
                    
                    EmailTo = GlobalCode.Field2String(uoTextBoxEmailAdd.Text),
                    EmailCC = GlobalCode.Field2String(uoTextBoxCopy.Text),
                    Comment = GlobalCode.Field2String(uoTextBoxComment.Text),
                    Currency = GlobalCode.Field2Int(uoDropDownListCurrency.SelectedItem.Value),
                    ConfirmBy = GlobalCode.Field2String(uoTextBoxConfirmedBy.Text),
                    StatusID = StatusID,//GlobalCode.Field2Int(((HiddenField)item.FindControl("uoHiddenFieldStatusID")).Value),// == 1 ? 2 : 4 ,
                    IsMedical = GlobalCode.Field2Bool(((HiddenField)item.FindControl("uoHiddenFieldIsMedical")).Value) ,
                    UserID = uoHiddenFieldUser.Value

                }); 
            }

            GlobalCode gc = new GlobalCode();
            DataTable dt = new DataTable();

            dt = gc.getDataTable(PAH);

            PortBLL = new PortAgentBLL();
            PortBLL.InsertNonTurnTransactionRequestBooking(dt, spName, uoHiddenFieldUser.Value, uoTextBoxEmailAdd.Text, uoTextBoxCopy.Text);
    
        }
         
        protected void uoListViewHotelHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrder.Value = e.CommandName;
            //BindVehicleManifestWithOrder();
        }

        #endregion

        #region "Functions"
        string lastDataFieldValue = null;
        string lastClass = "alternateBg";

        public string OverflowChangeRowColor()
        {

            string currentDataFieldValue = Eval("Employee").ToString();
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
            s = s.Replace("'", " ");

            string sScript = "<script language='JavaScript'>";
            sScript += "var msg = '" + s + "';";
            sScript += "alert( msg );";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2015
        /// Description:    Alert message without assigning value to uoHiddenFieldHotelConfirm from parent form
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
        /// Author:         Muhallidin
        /// Date Created:   14/oct/2015
        /// Description:    Bind List of hotel manifest to confirm
        /// </summary>
        private void GetHotelToConfirm()
        {
            string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);


            if (GlobalCode.Field2String(Request.QueryString["AddCancel"]) == "")
            {
                AlertMessage("No record to process!");
            }
            else if (sAction == "Add")
            {
                GetHotelManifestToAdd();
            }
            else
            {
                //GetHotelManifestToConfirm();
                GetHotelManifestToAdd();
            }
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
       
        private void ControlSettings(bool bIsReadOnly)
        {
            uoTextBoxHotelname.ReadOnly = bIsReadOnly;
            uoTextBoxConfirmation.ReadOnly = bIsReadOnly;
            uoTextBoxRateConfirmed.ReadOnly = bIsReadOnly;

            if (bIsReadOnly)
            {
                uoTextBoxHotelname.CssClass = "ReadOnly";
                uoTextBoxConfirmation.CssClass = "ReadOnly";
                uoTextBoxRateConfirmed.CssClass = "ReadOnly";
            }
        }
        private void ControlSettingsEditAmount()
        {
            uoTextBoxHotelname.ReadOnly = true;
            uoTextBoxConfirmation.ReadOnly = true;


            uoTextBoxHotelname.CssClass = "ReadOnly";
            uoTextBoxConfirmation.CssClass = "ReadOnly";

        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   25/Mar/2014
        /// Description:    Bind Request Source in Drop Down List
        /// </summary>
        private void BindRequestSource()
        {
            List<RequestSource> list = new List<RequestSource>();
            if (Session["PortAgentRequestSource"] != null)
            {
                list = (List<RequestSource>)Session["PortAgentRequestSource"];
            }
            uoDropDownListRequestSource.Items.Clear();
            uoDropDownListRequestSource.Items.Add(new ListItem("--Select Request Source--", "0"));
            if (list.Count > 0)
            {
                uoDropDownListRequestSource.DataSource = list;
                uoDropDownListRequestSource.DataTextField = "RequestSourceName";
                uoDropDownListRequestSource.DataValueField = "RequestSourceID";
            }
            uoDropDownListRequestSource.DataBind();
        }
        
        private void BindRoomType()
        {
            uoDropDownListRoom.Items.Clear();
            uoDropDownListRoom.Items.Add(new ListItem("ALL", "0"));
            if (TMSettings.RoomType.Count > 0)
            {
                uoDropDownListRoom.DataSource = TMSettings.RoomType;
                uoDropDownListRoom.DataTextField = "RoomName";
                uoDropDownListRoom.DataValueField = "RoomID";
            }
            uoDropDownListRoom.DataBind();
        }
        
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2014
        /// Description:    Get the hotel to confirm using selected seafarers
        /// </summary>
        private void GetHotelManifestToConfirm()
        {
            DataTable dt = null;
            try
            {
                string[] sAddCancelArr = Request.QueryString["AddCancel"].Split(",".ToCharArray());
                string[] sIDBigintArr = Request.QueryString["RecLoc"].Split(",".ToCharArray());
                string[] sIDTReqArr = Request.QueryString["TReqID"].Split(",".ToCharArray());
                string[] sIDTrans = Request.QueryString["TransID"].Split(",".ToCharArray());

                int i = 0;
                int iTotal = sAddCancelArr.Count();
                List<HotelManifestToConfirm> list = new List<HotelManifestToConfirm>();
                HotelManifestToConfirm item = new HotelManifestToConfirm();

                if (iTotal > 0)
                {

                    for (i = 0; i < iTotal; i++)
                    {
                        item = new HotelManifestToConfirm();
                        item.AddCancel = GlobalCode.Field2String(sAddCancelArr[i]);
                        item.IDBigint = GlobalCode.Field2Int(sIDBigintArr[i]);
                        item.TReqID = GlobalCode.Field2Int(sIDTReqArr[i]);
                        item.TransID = GlobalCode.Field2Int(sIDTrans[i]);

                        list.Add(item);
                        //}
                    }
                    if (list.Count > 0)
                    {

                        dt = getDataTable(list);
                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        PortBLL = new PortAgentBLL();
                        PortBLL.PortAgentManifestGetConfirmHotelToAdd(dt, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                                "Get Service Provider Hotel Manifest to confirm", "GetHotelToConfirm",
                                Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);

                       
                        BindListViewManifest();
                        BindRoomType();
                    }
                    else
                    {
                        AlertMessage("No record to process!");
                    }
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

        /// <summary>
        /// This example method generates a DataTable.
        /// </summary>
        private  DataTable CreateTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();


            table.Columns.Add("TransHotelID", typeof(long));
            table.Columns.Add("IDBigint", typeof(long));
            table.Columns.Add("TravelReqID", typeof(string));
            table.Columns.Add("SeqNoInt", typeof(string));

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
        private void GetHotelManifestToAdd()
        {
            try
            {
                ViewState["PortAgenRequestVendor"] = null;
                DataTable table = new DataTable();

                table = CreateTable();

                string dt = GlobalCode.Field2String(Request.QueryString["dt"]);
                var qRow = Request.QueryString["qTravelReqID"];
                int PortID = GlobalCode.Field2Int(Request.QueryString["pID"]);
                int row = GlobalCode.Field2Int(Request.QueryString["pRow"]);
                int PAID = GlobalCode.Field2Int(Request.QueryString["paID"]);
                string userID = uoHiddenFieldUser.Value;
                string[] TableRow = null  ;

                if (qRow != null){
                     TableRow = qRow.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
                DataRow dr;
                int c1 = 0; int c2 = 1; int c3 = 2; int c4 = 3;

                if (TableRow != null)
                { 
                     for (var i = 0; i < row; i++)
                    {
                       dr = table.NewRow();

                       dr["TransHotelID"] = TableRow[c4];
                       dr["IDBigint"] = TableRow[c1];
                       dr["TravelReqID"] = TableRow[c2];
                       dr["SeqNoInt"] = TableRow[c3];

                       dr["ReqDate"] = GlobalCode.Field2Time(dt)  ;
                       dr["PortID"] = PortID;
                       dr["UserID"] = userID;
                       table.Rows.Add(dr);

                       c1 = c1 + 4;
                       c2 = c2 + 4;
                       c3 = c3 + 4;
                       c4 = c4 + 4;


                    }
                }
                uoHiddenFieldStatusID.Value =Request.QueryString["st"];

                PortBLL = new PortAgentBLL();
                List<NonTurnportGenericList> lst = new List<NonTurnportGenericList>();
                lst = PortBLL.GetPortNonTurnHotelReques(0, GlobalCode.Field2Time(dt), PortID, GlobalCode.Field2Int(Request.QueryString["paID"]), userID, table);

                  List<PortAgenRequestVendor> PAList = new List<PortAgenRequestVendor>();
                if (uoHiddenFieldStatusID.Value == "1" && uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist )
                {
                    PAList = lst[0].PortAgenRequestVendor.Where(n => n.VendorID == GlobalCode.Field2Int(Request.QueryString["paID"])).ToList();
                }
                else

                {

                    PAList = lst[0].PortAgenRequestVendor;;
                }



                uoVendorDropDownList.DataSource = PAList;

                uoVendorDropDownList.DataTextField = "VendorName";
                uoVendorDropDownList.DataValueField = "VendorID";

                uoVendorDropDownList.DataBind();
                uoVendorDropDownList.Items.Insert(0, new ListItem("--Select Vendor--", "0"));


                uoVendorDropDownList.SelectedIndex = GlobalCode.GetselectedIndex(uoVendorDropDownList, GlobalCode.Field2Int( Request.QueryString["paID"]));


                uoDropDownListCurrency.DataSource = lst[0].Currency;


                
                uoDropDownListCurrency.DataTextField = "Name";
                uoDropDownListCurrency.DataValueField = "ID";

                uoDropDownListCurrency.DataBind();
                uoDropDownListCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));

                uoDropDownListRequestSource.DataSource = lst[0].Requestor;
                uoDropDownListRequestSource.DataTextField = "Name";
                uoDropDownListRequestSource.DataValueField = "ID";
                uoDropDownListRequestSource.DataBind();
                uoDropDownListRequestSource.Items.Insert(0, new ListItem("--Select Requestor--", "0"));

                uoDropDownListRequestSource.SelectedIndex = 2;

                ViewState["PortAgenRequestVendor"] =  PAList;//lst[0].PortAgenRequestVendor;
                uolistviewHotelInfo.DataSource =lst[0].NonTurnPortsLists;
                uolistviewHotelInfo.DataBind();

                var PortAgenRequest = PAList; // lst[0].PortAgenRequestVendor.Where(n => n.VendorID == GlobalCode.Field2Int(Request.QueryString["paID"])).ToList();
                if (PortAgenRequest.Count > 0)
                {
                    uoTextBoxHotelname.Text = uoVendorDropDownList.SelectedItem.Text;
                    uoTextBoxEmailAdd.Text = PortAgenRequest[0].EmailTo;


                    uoTextBoxRateContract.Text = PortAgenRequest[0].DoubleRate.ToString();

                    uoTextBoxRateContractSingle.Text = PortAgenRequest[0].SingleRate.ToString();

                    uoTextBoxRateConfirmed.Text = PortAgenRequest[0].DoubleRate.ToString();
                    uoTextBoxSingleRateConfirmed.Text = PortAgenRequest[0].SingleRate.ToString();

                    uoTextBoxCheckInDate.Text = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToString();
                    uoDropDownListCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListCurrency, PortAgenRequest[0].CurrentID);
                    uoHiddenFieldContractID.Value = PortAgenRequest[0].VendorContractID.ToString();
                    uoHiddenFieldVoucher.Value = PortAgenRequest[0].Voucher.ToString();
                }

            }
            catch(Exception e)
            {
                throw e;
            }
             
        }
        ///// <summary>
        ///// Author:         Josephine Gad
        ///// Date Created:   10/Apr/2014
        ///// Description:    Bind Currency
        ///// </summary>
        //private void BindCurrency()
        //{
        //    uoDropDownListCurrency.Items.Clear();
        //    uoDropDownListCurrency.Items.Add(new ListItem("--Select Currency--", "0"));
        //    List<Currency> listCurrency = new List<Currency>();
        //    if (Session["PortAgentCurrency"] != null)
        //    {
        //        listCurrency = (List<Currency>)Session["PortAgentCurrency"];
        //        uoDropDownListCurrency.DataSource = listCurrency;
        //        uoDropDownListCurrency.DataTextField = "CurrencyName";
        //        uoDropDownListCurrency.DataValueField = "CurrencyID";
        //    }
        //    uoDropDownListCurrency.DataBind();
        //}
        /// <summary>
        /// Author:         Michael Evangelista
        /// Date Modified:   01/Oct/2015
        /// Description:    Bind LisView Manifest, Manifest to Cancel, vendor name and other details
        /// </summary>
        private void BindListViewManifest()
        {
            //Bind New Hotel Request 
            List<PortAgentVendorManifestListName> listPortAgent = new List<PortAgentVendorManifestListName>();
            if (Session["PortAgentHotelListToConfirmToAdd"] != null)
            {
                listPortAgent = (List<PortAgentVendorManifestListName>)Session["PortAgentHotelListToConfirmToAdd"];
            }
            List<PortAgentHotelManifestListName> listHotel = new List<PortAgentHotelManifestListName>();
            if (Session["HotelListToConfirmToAdd"] != null)
            {
                listHotel = (List<PortAgentHotelManifestListName>)Session["HotelListToConfirmToAdd"];
            }
            

            string selectedValue = uoVendorSelectionList.SelectedValue;
            if (selectedValue == "Service Provider")
            {
                uoVendorDropDownList.DataSource = listPortAgent;
                uoVendorDropDownList.DataTextField = "PortAgentName";
                uoVendorDropDownList.DataValueField = "PortAgentID";
                uoVendorDropDownList.DataBind();
            }
            else
            {
                uoVendorDropDownList.DataSource = listHotel;
                uoVendorDropDownList.DataTextField = "HotelVendorName";
                uoVendorDropDownList.DataValueField = "HotelID";
                uoVendorDropDownList.DataBind();
            }

            uoTextBoxCheckInDate.Text = "";
          

            uoTextBoxEmailAdd.Text = GlobalCode.Field2String(Session["PortAgentEmailVendor"]);
        }

        

        #endregion




    }
}

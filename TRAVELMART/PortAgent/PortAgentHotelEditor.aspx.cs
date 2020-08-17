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
    public partial class PortAgentHotelEditor : System.Web.UI.Page
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
                    uoLabelTitle.Text = "Hotel Request: Confirm/Cancel Request";
                    uoButtonEmail.Text = "Confirm";
                }
                else if (sAction == "EditAmount")
                {
                    ControlSettingsEditAmount();
                    uoLabelTitle.Text = "Hotel Request: Edit Amount";
                    uoButtonEmail.Text = "Edit Amount";
                }
                else if (sAction == "Approve")
                {
                    ControlSettings(true);
                    uoLabelTitle.Text = "Hotel Request: Approval";
                    uoButtonEmail.Text = "Approve";

                }
                else if (sAction == "Cancel")
                {
                    ControlSettings(true);
                    uoLabelTitle.Text = "Hotel Request: Cancellation";
                    uoButtonEmail.Text = "Cancel";
                }
                else if (sAction == "CancelByRCCL")
                {
                    ControlSettings(true);
                    uoLabelTitle.Text = "Hotel Request: Cancellation";
                    uoButtonEmail.Text = "Cancel";
                }
                else if (sAction == "Add")
                {
                    ControlSettingsEditAmount();
                    uoLabelTitle.Text = "Create Hotel Request";
                    uoButtonEmail.Text = "Create";
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
        protected void uoButtonEmail_Click(object sender, EventArgs e)
        {
            ConfirmHotel();
            ClosePage("Email successfully sent!");
        }
        protected void uoListViewHotelHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrder.Value = e.CommandName;
            BindVehicleManifestWithOrder();
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

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldHotelConfirm\").val(\"1\"); ";
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

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldHotelConfirm\").val(\"1\"); ";
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
        /// Date Created:   11/Mar/2014
        /// Description:    Alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertContractMessages(string s)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert(document.getElementById('ctl00_ContentPlaceHolder1_uoHiddenfieldEndOfContract').value);", true);

           // Page.RegisterStartupScript("ClickScript", "<script language='javascript'>document.getElementById('" + btnAlertMe.ClientID + "').click();</script>");
            //Page.RegisterStartupScript("ClickScript", "<script language='javascript'>alert('" + s + "'); document.getElementById('" + btnAlertMe.ClientID + "').click();</script>"); 
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
        /// Author:         Josephine Gad
        /// Date Created:   08/Mar/2014
        /// Description:    Bind List of hotel manifest to confirm
        /// </summary>
        private void GetHotelToConfirm()
        {     
            string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
            if (GlobalCode.Field2String(Request.QueryString["AddCancel"]) == "")
            {
                AlertMessage("No record to process!");
            }
            else if(sAction == "Add")
            {
                GetHotelManifestToAdd();
            }
            else
            {
                GetHotelManifestToConfirm();                
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
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   10/Mar/2014
        /// Description:    Confirm Service Provider Hotel Manifest
        /// </summary>
        private void ConfirmHotel()
        {
            DataTable dt = null;
            try
            {
                int iCurrency = GlobalCode.Field2Int(uoDropDownListCurrency.SelectedValue);
                float fRate = GlobalCode.Field2Float(uoTextBoxRateConfirmed.Text);

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
                if (User.IsInRole(TravelMartVariable.RolePortSpecialist) &&
                    (sAction == ""))
                {
                    dt = getManifestAmount();
                    BLL.PortAgentManifestConfirmHotel(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                      uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                      iCurrency, fRate, uoTextBoxConfirmation.Text.Trim(),
                      uoTextBoxHotelname.Text.Trim(), uoTextBoxComment.Text,
                      uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, "Confirmed Hotel by Service Provider Vendor",
                      "ConfirmHotel", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                      CommonFunctions.GetDateTimeGMT(dateNow), dateNow, dt);
                }
                else if (sAction == "EditAmount")
                {
                    dt = getManifestAmount();
                    BLL.PortAgentManifestConfirmHotelAmount(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    iCurrency, fRate, uoTextBoxConfirmation.Text.Trim(),
                    uoTextBoxHotelname.Text.Trim(), uoTextBoxComment.Text,
                    uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, "Changed Amount of Hotel by RCCL",
                    "ConfirmHotel", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow, dt);
                }
                else if (sAction == "Approve")
                {
                    BLL.PortAgentManifestConfirmHotelApprove(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    uoTextBoxComment.Text, uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, "Approve Service Provider Hotel Manifest by RCCL",
                    "ConfirmHotel", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow);
                }
                else if (sAction == "CancelByRCCL" || sAction == "Cancel")
                {
                    string sDescription = sAction + ": Cancel Service Provider Hotel Manifest";

                    BLL.PortAgentManifestConfirmHotelCancel(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                   uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                   uoTextBoxComment.Text, uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, sDescription,
                   "ConfirmHotel", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                   CommonFunctions.GetDateTimeGMT(dateNow), dateNow);
                }
                else if (sAction == "Add")
                {                     
                    dt = getManifestAmount();
                  
                    BLL.PortAgentManifestConfirmHotelAdd(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    iCurrency, fRate, uoTextBoxConfirmation.Text.Trim(),
                    uoTextBoxHotelname.Text.Trim(), uoTextBoxComment.Text,
                    uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, "Create Hotel Request by RCCL",
                    "ConfirmHotel", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow, dt);
                }
            }
            catch (Exception ex)
            {
                AlertErrorMessage("Confirm Hotel: " + ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        private void ControlSettings(bool bIsReadOnly)
        {
            uoTextBoxHotelname.ReadOnly = bIsReadOnly;
            uoTextBoxConfirmation.ReadOnly = bIsReadOnly;
            uoTextBoxRateConfirmed.ReadOnly = bIsReadOnly;
            uoDropDownListCurrency.Enabled = !bIsReadOnly;

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
        /// <summary>       
        /// Author:         Josephine Gad
        /// Date Created:   31/Mar/2014
        /// Description:    Bind Vehicle Request with order by
        /// </summary>
        private void BindVehicleManifestWithOrder()
        {
            BLL.PortAgentManifestGetConfirmHotelToAddWithOrder(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                uoHiddenFieldOrder.Value);

            //Bind New Hotel Request
            List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
            if (Session["PortAgentHotelManifestListToConfirmToAdd"] != null)
            {
                listHotel = (List<PortAgentHotelManifestList>)Session["PortAgentHotelManifestListToConfirmToAdd"];
            }
            uolistviewHotelInfo.DataSource = listHotel;
            uolistviewHotelInfo.DataBind();

            //Bind Cancelled Hotel
            List<PortAgentHotelManifestList> listHotelCancelled = new List<PortAgentHotelManifestList>();
            if (Session["PortAgentHotelManifestListToConfirmToCancel"] != null)
            {
                listHotelCancelled = (List<PortAgentHotelManifestList>)Session["PortAgentHotelManifestListToConfirmToCancel"];
            }
            uoListViewCancelDetails.DataSource = listHotelCancelled;
            uoListViewCancelDetails.DataBind();
        }
        private void BindRoomType()
        { 
            uoDropDownListRoom.Items.Clear();
            uoDropDownListRoom.Items.Add(new ListItem("ALL","0"));
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
        /// Date Created:   01/Apr/2014
        /// Description:    get the amount to be saved
        /// </summary>
        /// <returns></returns>
        private DataTable getManifestAmount()
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                DataColumn dtCol;

                dtCol = new DataColumn("Rate", typeof(float));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("IDBigint", typeof(Int64));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("TRID", typeof(Int64));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("TransID", typeof(Int64));
                dt.Columns.Add(dtCol);



                DataRow dtRow;

                HiddenField hiddenRecLocID;
                HiddenField hiddenTRID;
                HiddenField hiddenTransID;
                TextBox txtRate;

                int iTotal = uolistviewHotelInfo.Items.Count();
                for (int i = 0; i < iTotal; i++)
                {
                    hiddenRecLocID = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldListRecLocID");
                    hiddenTRID = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldListTRID");
                    hiddenTransID = (HiddenField)uolistviewHotelInfo.Items[i].FindControl("uoHiddenFieldTransID");
                    txtRate = (TextBox)uolistviewHotelInfo.Items[i].FindControl("uoTextBoxRateConfirmedPerSeafarer");

                    dtRow = dt.NewRow();

                    dtRow["Rate"] = txtRate.Text;
                    dtRow["IDBigint"] = hiddenRecLocID.Value;
                    dtRow["TRID"] = hiddenTRID.Value;
                    dtRow["TransID"] = hiddenTransID.Value;


                    dt.Rows.Add(dtRow);
                }
                return dt;
            }
            catch (Exception ex)
            {
                AlertErrorMessage(ex.Message);
                throw ex;                
            }
            finally
            {
                dt.Dispose();
            }
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

                        BLL = new PortAgentBLL();
                        BLL.PortAgentManifestGetConfirmHotelToAdd(dt, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                                "Get Service Provider Hotel Manifest to confirm", "GetHotelToConfirm",
                                Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);

                        BindCurrency();
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
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2014
        /// Description:    Get the hotel to confirm from Non Turn Port Page
        /// </summary>
        private void GetHotelManifestToAdd()
        {
            bool IsWithError = false;
            DateTime currentDate = CommonFunctions.GetCurrentDateTime();
            int iPortID = GlobalCode.Field2Int(uoHiddenFieldPortID.Value);

            BLL = new PortAgentBLL();
            BLL.PortAgentManifestGetConfirmHotelToAddFromNonTurn(iPortID, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    "Get Service Provider Hotel Manifest to confirm", "GetHotelToConfirm",
                    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);
            List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
            if (GlobalCode.Field2String(Session["PortAgentEmailVendor"]) == "")
            {
                IsWithError = true;
                AlertMessageNoRefresh("No assigned active Service Provider Vendor for Hotel Service!");
            }
            if (Session["PortAgentHotelManifestListToConfirmToAdd"] != null)
            {
                listHotel = (List<PortAgentHotelManifestList>)Session["PortAgentHotelManifestListToConfirmToAdd"];
            }
            if (listHotel.Count == 0)
            {
                IsWithError = true;
                AlertMessageNoRefresh("No record to process!");                
            }
            if (!IsWithError)
            {
                BindCurrency();
                BindListViewManifest();
                BindRoomType();
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2014
        /// Description:    Bind Currency
        /// </summary>
        private void BindCurrency()
        {
            uoDropDownListCurrency.Items.Clear();
            uoDropDownListCurrency.Items.Add(new ListItem("--Select Currency--", "0"));
            List<Currency> listCurrency = new List<Currency>();
            if (Session["PortAgentCurrency"] != null)
            {
                listCurrency = (List<Currency>)Session["PortAgentCurrency"];
                uoDropDownListCurrency.DataSource = listCurrency;
                uoDropDownListCurrency.DataTextField = "CurrencyName";
                uoDropDownListCurrency.DataValueField = "CurrencyID";
            }
            uoDropDownListCurrency.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2014
        /// Description:    Bind LisView Manifest, Manifest to Cancel, vendor name and other details
        /// </summary>
        private void BindListViewManifest()
        {
            //Bind New Hotel Request
            List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
            if (Session["PortAgentHotelManifestListToConfirmToAdd"] != null)
            {
                listHotel = (List<PortAgentHotelManifestList>)Session["PortAgentHotelManifestListToConfirmToAdd"];
            }
            uolistviewHotelInfo.DataSource = listHotel;
            uolistviewHotelInfo.DataBind();

            //Bind Cancelled Hotel
            List<PortAgentHotelManifestList> listHotelCancel = new List<PortAgentHotelManifestList>();
            if (Session["PortAgentHotelManifestListToConfirmToCancel"] != null)
            {
                listHotelCancel = (List<PortAgentHotelManifestList>)Session["PortAgentHotelManifestListToConfirmToCancel"];
            }
            uoListViewCancelDetails.DataSource = listHotelCancel;
            uoListViewCancelDetails.DataBind();


            uoHiddenFieldCountAdd.Value = GlobalCode.Field2String(listHotel.Count);
            uoHiddenFieldCountDelete.Value = GlobalCode.Field2String(listHotelCancel.Count);


            uoTextBoxCheckInDate.Text = "";
            string sCurrency = "0";
            System.DateTime? checkindate = null;
            System.DateTime? dtEndcontract = null;
            System.DateTime? dtBegincontract = null;

            if (listHotel.Count > 0)
            {
                uoTextBoxCheckInDate.Text = string.Format("{0:dd-MMM-yyyy}", listHotel[0].Checkin);

                checkindate = listHotel[0].Checkin;

                if (listHotel[0].RateContracted > 0)
                {
                    uoTextBoxRateContract.Text = string.Format("{0:#,##0.00}", listHotel[0].RateContracted);
                }
                if (listHotel[0].RateConfirmed > 0)
                {
                    uoTextBoxRateConfirmed.Text = string.Format("{0:#,##0.00}", listHotel[0].RateConfirmed);
                }
                uoTextBoxConfirmation.Text = GlobalCode.Field2String(listHotel[0].ConfirmationNo).Trim();
                uoTextBoxHotelname.Text = listHotel[0].HotelName;

                sCurrency = GlobalCode.Field2Int(listHotel[0].CurrencyID).ToString();
            }
            else 
            {
                if (listHotelCancel.Count > 0)
                {
                    uoTextBoxCheckInDate.Text = string.Format("{0:dd-MMM-yyyy}", listHotelCancel[0].Checkin);
                    checkindate = listHotelCancel[0].Checkin;
                }
            }

            if (uoDropDownListCurrency.Items.FindByValue(sCurrency) != null)
            {
                uoDropDownListCurrency.SelectedValue = sCurrency;
            }

            uoTextBoxVendor.Text = "";
            List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
            if (Session["PortAgentDetails"] != null)
            {
                listPortAgent = (List<PortAgentDTO>)Session["PortAgentDetails"];
                if (listPortAgent.Count > 0)
                {
                    uoTextBoxVendor.Text = listPortAgent[0].PortAgentName;
                    uoHiddenfieldEndOfContract.Value = listPortAgent[0].EndOfContract.ToString();
                    dtEndcontract = listPortAgent[0].EndOfContract;
                    dtBegincontract = listPortAgent[0].BeginOfContract;
                }
            }
            uoTextBoxEmailAdd.Text = GlobalCode.Field2String(Session["PortAgentEmailVendor"]);


            System.TimeSpan? diffResult = dtEndcontract - checkindate;

            uoButtonEmail.Enabled = true;

            if (listHotel.Count > 0)
            {
                if (GlobalCode.Field2Long(diffResult.Value.Days) < 0)
                {
                    uoHiddenfieldEndOfContract.Value = "Contract not valid for requested date!!! \n valid date between " + dtBegincontract.Value.Month + "/" + dtBegincontract.Value.Day + "/" + dtBegincontract.Value.Year + " and " + dtEndcontract.Value.Month + "/" + dtEndcontract.Value.Day + "/" + dtEndcontract.Value.Year;// dtEndcontract.Value.Date.ToString();
                    AlertContractMessages("Contract not valid for requested date!!! \n valid date between " + dtBegincontract.ToString() + " and " + dtEndcontract.ToString());

                    uoButtonEmail.Enabled = false;
                }
            }
        }
        #endregion

    }
}

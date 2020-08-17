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
    public partial class PortAgentVehicleEditor : System.Web.UI.Page
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
                    uoButtonEmail.Text = "Confirm";
                }
                else if (sAction == "EditAmount")
                {
                    ControlSettingsEditAmount();
                    uoLabelTitle.Text = "Vehicle Request: Edit Amount";
                    uoButtonEmail.Text = "Edit Amount";
                }
                else if (sAction == "Approve")
                {
                    ControlSettings(true);
                    uoLabelTitle.Text = "Vehicle Request: Approval";
                    uoButtonEmail.Text = "Approve";

                }
                else if (sAction == "Cancel")
                {
                    ControlSettings(true);
                    uoLabelTitle.Text = "Vehicle Request: Cancellation";
                    uoButtonEmail.Text = "Cancel";
                }
                else if (sAction == "CancelByRCCL")
                {
                    ControlSettings(true);
                    uoLabelTitle.Text = "Vehicle Request: Cancellation";
                    uoButtonEmail.Text = "Cancel";
                }
                else if (sAction == "Add")
                {
                    ControlSettingsEditAmount();
                    uoLabelTitle.Text = "Create Vehicle Request";
                    uoButtonEmail.Text = "Create";
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

                    BindRequestSource();
                }                
            }
            uoListViewVehicleHeader.DataSource = null;
            uoListViewVehicleHeader.DataBind();

            uoListViewCancelHeader.DataSource = null;
            uoListViewCancelHeader.DataBind();
           
        }
        protected void uoButtonEmail_Click(object sender, EventArgs e)
        {
            ConfirmVehicle();
            ClosePage("Email successfully sent!");
        }
        protected void uoListViewVehicleHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrder.Value = e.CommandName;
            BindVehicleManifestWithOrder();
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

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldVehicleConfirm\").val(\"1\"); ";
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

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldVehicleConfirm\").val(\"1\"); ";
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
            string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
            if (GlobalCode.Field2String(Request.QueryString["AddCancel"]) == "")
            {
                AlertMessage("No record to process!");
            }
            else if (sAction == "Add")
            {
                GetVehicleManifestToAdd();
            }
            else
            {
                GetVehicleManifestToConfirm();
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
        /// Description:    Confirm Service Provider Vehicle Manifest
        /// </summary>
        private void ConfirmVehicle()
        {
            Int32 iContractIDInt = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
            Int16 iVehicleTypeId = GlobalCode.Field2TinyInt(uoDropDownListVehicleType.SelectedValue);
            DataTable dt = null;
            try
            {
                int iCurrency = GlobalCode.Field2Int(uoDropDownListCurrency.SelectedValue);
                float fRate = GlobalCode.Field2Float(uoTextBoxRateConfirmed.Text);

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                string sTimeSpan = "";
                if (uoTextBoxPickupTime.Text.Trim() != "")
                {
                    sTimeSpan = GlobalCode.Field2DateTimeWithTime(uoTextBoxPickupTime.Text).ToString();
                }
                Int16 iVehicleTypeID = GlobalCode.Field2TinyInt(uoDropDownListVehicleType.SelectedValue);

                string sAction = GlobalCode.Field2String(Request.QueryString["Action"]);
                if (User.IsInRole(TravelMartVariable.RolePortSpecialist) &&
                    (sAction == ""))
                {

                    dt = getManifestAmountWithContract();
                    BLL.PortAgentManifestConfirmVehicle(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                        uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(), sTimeSpan,
                        iCurrency, fRate, uoTextBoxConfirmation.Text.Trim(),
                        "", uoTextBoxDriver.Text, uoTextBoxPlateNo.Text,
                        iVehicleTypeID, uoTextBoxComment.Text, uoTextBoxConfirmedBy.Text, iContractIDInt, "Confirmed Vehicle by Service Provider Vendor",
                        "ConfirmVehicle", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                        CommonFunctions.GetDateTimeGMT(dateNow), dateNow, dt, uoTextBoxTranspoDetails.Text.Trim());
                }
                else if (sAction == "EditAmount")
                {
                    dt = getManifestAmount();
                    BLL.PortAgentManifestConfirmVehicleAmount(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    iCurrency, fRate, uoTextBoxConfirmation.Text.Trim(),
                    uoTextBoxComment.Text,
                    uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, "Changed Amount of Vehicle by RCCL",
                    "ConfirmVehicle", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow, dt);
                }
                else if (sAction == "Approve")
                {
                    BLL.PortAgentManifestConfirmVehicleApprove(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    uoTextBoxComment.Text, uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue,
                    "Approve Service Provider Vehicle Manifest by RCCL",
                    "ConfirmVehicle", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow);
                }
                else if (sAction == "CancelByRCCL" || sAction == "Cancel")
                {
                    string sDescription = sAction + ": Cancel Service Provider Vehicle Manifest";

                    BLL.PortAgentManifestConfirmVehicleCancel(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    uoTextBoxComment.Text, uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, sDescription,
                    "ConfirmVehicle", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow);
                }
                else if (sAction == "Add")
                {
                    dt = getManifestAmountWithContract();
                    BLL.PortAgentManifestConfirmVehicleAdd(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    uoTextBoxEmailAdd.Text.Trim(), uoTextBoxCopy.Text.Trim(),
                    iCurrency, fRate, uoTextBoxConfirmation.Text.Trim(),
                    uoTextBoxComment.Text,
                    uoTextBoxConfirmedBy.Text, uoDropDownListRequestSource.SelectedValue, iContractIDInt, 
                    iVehicleTypeId, "Create Vehicle Request by RCCL",
                    "ConfirmVehicle", Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                    CommonFunctions.GetDateTimeGMT(dateNow), dateNow, dt);
                }
            }
            catch (Exception ex)
            {
                AlertErrorMessage("Confirm Vehicle: " + ex.Message);
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
            BLL.PortAgentManifestGetConfirmVehicleToAddWithOrder(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, 
                uoHiddenFieldOrder.Value);

            //Bind New Vehicle Request
            List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();
            if (Session["PortAgentVehicleManifestListToConfirmToAdd"] != null)
            {
                listVehicle = (List<PortAgentVehicleManifestList>)Session["PortAgentVehicleManifestListToConfirmToAdd"];
            }
            uoListviewVehicleInfo.DataSource = listVehicle;
            uoListviewVehicleInfo.DataBind();

            //Bind Cancelled Vehicle
            List<PortAgentVehicleManifestList> listVehicleCancel = new List<PortAgentVehicleManifestList>();
            if (Session["PortAgentVehicleManifestListToConfirmToCancel"] != null)
            {
                listVehicleCancel = (List<PortAgentVehicleManifestList>)Session["PortAgentVehicleManifestListToConfirmToCancel"];
            }
            uoListViewCancelDetails.DataSource = listVehicleCancel;
            uoListViewCancelDetails.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   31/Mar/2014
        /// Description:    Bind Route From and To
        /// </summary>
        private void BindRouteFromTo()
        { 
            List<RouteFromTo> listRouteFromTo = new List<RouteFromTo>();
            listRouteFromTo = (List<RouteFromTo>)Session["PortAgentRouteFromTo"];

            uoDropDownListRoute.Items.Clear();
            uoDropDownListRoute.Items.Add(new ListItem("ALL", "0"));
            if (listRouteFromTo.Count > 0)
            {
                uoDropDownListRoute.DataSource = listRouteFromTo;
                uoDropDownListRoute.DataTextField = "RouteName";
                uoDropDownListRoute.DataValueField = "RouteID";

            }
            uoDropDownListRoute.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   31/Mar/2014
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

                int iTotal = uoListviewVehicleInfo.Items.Count();
                for (int i = 0; i < iTotal; i++)
                {
                    hiddenRecLocID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldListRecLocID");
                    hiddenTRID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldListTRID");
                    hiddenTransID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldTransID");
                    txtRate = (TextBox)uoListviewVehicleInfo.Items[i].FindControl("uoTextBoxRateConfirmedPerSeafarer");

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
        /// Date Created:   14/Apr/2014
        /// Description:    get the amount to be saved with contracted rate
        /// </summary>
        /// <returns></returns>
        private DataTable getManifestAmountWithContract()
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                DataColumn dtCol;

                dtCol = new DataColumn("ContractRate", typeof(float));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("Rate", typeof(float));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("IDBigint", typeof(Int64));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("TRID", typeof(Int64));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("TransID", typeof(Int64));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("SeqNo", typeof(Int32));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("RouteFromID", typeof(Int16));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("RouteToID", typeof(Int16));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("RouteFromCity", typeof(string));
                dt.Columns.Add(dtCol);

                dtCol = new DataColumn("RouteToCity", typeof(string));
                dt.Columns.Add(dtCol);


                DataRow dtRow;

                HiddenField hiddenRecLocID;
                HiddenField hiddenTRID;
                HiddenField hiddenTransID;
                TextBox txtRate;
                Label lblContractRate;
                HiddenField hdnContractRate;
                Label lblSeqNo;
                Label lblRouteFromCity;
                Label lblRouteToCity;
                HiddenField hdnRouteFromID;
                HiddenField hdnRouteToID;


                int iTotal = uoListviewVehicleInfo.Items.Count();
                for (int i = 0; i < iTotal; i++)
                {
                    hiddenRecLocID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldListRecLocID");
                    hiddenTRID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldListTRID");
                    hiddenTransID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldTransID");
                    txtRate = (TextBox)uoListviewVehicleInfo.Items[i].FindControl("uoTextBoxRateConfirmedPerSeafarer");
                    lblContractRate = (Label)uoListviewVehicleInfo.Items[i].FindControl("uoLabelContractedRate");
                    hdnContractRate = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldContractedRate");
                    lblSeqNo = (Label)uoListviewVehicleInfo.Items[i].FindControl("uoLabelSeqNo");
                    lblRouteFromCity = (Label)uoListviewVehicleInfo.Items[i].FindControl("uoLabelRouteFromCity");
                    lblRouteToCity = (Label)uoListviewVehicleInfo.Items[i].FindControl("uoLabelRouteToCity");
                    hdnRouteFromID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldRouteFromID");
                    hdnRouteToID = (HiddenField)uoListviewVehicleInfo.Items[i].FindControl("uoHiddenFieldRouteToID");

                    dtRow = dt.NewRow();

                    dtRow["ContractRate"] = GlobalCode.Field2Float(hdnContractRate.Value);
                    dtRow["Rate"] = GlobalCode.Field2Float(txtRate.Text);
                    dtRow["IDBigint"] = GlobalCode.Field2Int(hiddenRecLocID.Value);
                    dtRow["TRID"] = GlobalCode.Field2Int(hiddenTRID.Value);
                    dtRow["TransID"] = GlobalCode.Field2Int(hiddenTransID.Value);
                    dtRow["SeqNo"] =  GlobalCode.Field2Int(lblSeqNo.Text);
                    dtRow["RouteFromID"] = GlobalCode.Field2TinyInt(hdnRouteFromID.Value);
                    dtRow["RouteToID"] =  GlobalCode.Field2TinyInt(hdnRouteToID.Value);
                    dtRow["RouteFromCity"] = lblRouteFromCity.Text;
                    dtRow["RouteToCity"] = lblRouteToCity.Text;

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
        /// Date Created:   11/Apr/2014
        /// Description:    Get the vehicle to confirm using selected seafarers
        /// </summary>
        private void GetVehicleManifestToConfirm()
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
                List<VehicleManifestToConfirm> list = new List<VehicleManifestToConfirm>();
                VehicleManifestToConfirm item = new VehicleManifestToConfirm();

                if (iTotal > 0)
                {

                    for (i = 0; i < iTotal; i++)
                    {
                        //if (sAddCancelArr[i] == "Add")
                        //{
                        item = new VehicleManifestToConfirm();
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
                        BLL.PortAgentManifestGetConfirmVehicleToAdd(dt, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                                "Get Service Provider Vehicle Manifest to confirm",
                                "GetVehicleToConfirm", Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);


                        BindCurrency();
                        BindListViewManifest();
                        BindRouteFromTo();
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
        /// Date Created:   11/Apr/2014
        /// Description:    Get the vehicle to confirm from Non Turn Port Page
        /// =====================================================================
        /// Modified by:    Josephine Gad
        /// Date Modified:  14/May/2014
        /// Description:    Use Session["PortAgentDetails"] instead of Session["PortAgentEmailVendor"]
        ///                 in validating Service Provider
        /// </summary>
        private void GetVehicleManifestToAdd()
        {
            bool IsWithError = false;
            DateTime currentDate = CommonFunctions.GetCurrentDateTime();
            int iPortID = GlobalCode.Field2Int(uoHiddenFieldPortID.Value);

            BLL = new PortAgentBLL();
            BLL.PortAgentManifestGetConfirmVehicleToAddFromNonTurn(iPortID, uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,
                    "Get Service Provider Vehicle Request to add", "GetVehicleManifestToAdd",
                    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);

            List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();

            List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();

            if (Session["PortAgentDetails"] != null)
            {
                listPortAgent = (List<PortAgentDTO>)Session["PortAgentDetails"];                
            }
            if (listPortAgent.Count == 0)
            {
                IsWithError = true;
                AlertMessageNoRefresh("No assigned active Service Provider Vendor for Vehicle Service!");
            }

            if (Session["PortAgentVehicleManifestListToConfirmToAdd"] != null)
            {
                listVehicle = (List<PortAgentVehicleManifestList>)Session["PortAgentVehicleManifestListToConfirmToAdd"];
            }
            if (listVehicle.Count == 0)
            {
                IsWithError = true;
                AlertMessageNoRefresh("No record to process!");
            }
            if (!IsWithError)
            {
                BindCurrency();
                BindListViewManifest();
                BindRouteFromTo();
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   11/Apr/2014
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
        /// Date Created:   11/Apr/2014
        /// Description:    Bind LisView Manifest, Manifest to Cancel, vendor name and other details
        /// </summary>
        private void BindListViewManifest()
        {
            //Bind New Vehicle Request
            List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();
            if (Session["PortAgentVehicleManifestListToConfirmToAdd"] != null)
            {
                listVehicle = (List<PortAgentVehicleManifestList>)Session["PortAgentVehicleManifestListToConfirmToAdd"];
            }
            uoListviewVehicleInfo.DataSource = listVehicle;
            uoListviewVehicleInfo.DataBind();

            //Bind Cancelled Vehicle
            List<PortAgentVehicleManifestList> listVehicleCancel = new List<PortAgentVehicleManifestList>();
            if (Session["PortAgentVehicleManifestListToConfirmToCancel"] != null)
            {
                listVehicleCancel = (List<PortAgentVehicleManifestList>)Session["PortAgentVehicleManifestListToConfirmToCancel"];
            }
            uoListViewCancelDetails.DataSource = listVehicleCancel;
            uoListViewCancelDetails.DataBind();


            uoHiddenFieldCountAdd.Value = GlobalCode.Field2String(listVehicle.Count);
            uoHiddenFieldCountDelete.Value = GlobalCode.Field2String(listVehicleCancel.Count);


            uoTextBoxPickupDate.Text = "";
            uoTextBoxPickupTime.Text = "";
            string sCurrency = "0";

            if (listVehicle.Count > 0)
            {
                uoTextBoxPickupDate.Text = string.Format("{0:dd-MMM-yyyy}", listVehicle[0].PickupDate);
                if (listVehicle[0].PickupTime != null)
                {
                    uoTextBoxPickupTime.Text = string.Format("{0:hh:mm}", listVehicle[0].PickupTime);
                }


                uoDropDownListVehicleType.Items.Clear();
                uoDropDownListVehicleType.Items.Add(new ListItem("--Select Vehicle Type--", "0"));
                List<VehicleType> listVehicleType = new List<VehicleType>();
                if (Session["PortAgentVehicleType"] != null)
                {
                    listVehicleType = (List<VehicleType>)Session["PortAgentVehicleType"];
                }
                uoDropDownListVehicleType.DataSource = listVehicleType;
                uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
                uoDropDownListVehicleType.DataValueField = "VehicleTypeID";
                uoDropDownListVehicleType.DataBind();

                string sVehicleTypeID = GlobalCode.Field2Int(listVehicle[0].VehicleTypeID).ToString();
                if (uoDropDownListVehicleType.Items.FindByValue(sVehicleTypeID) != null)
                {
                    uoDropDownListVehicleType.SelectedValue = sVehicleTypeID;
                }

                if (listVehicle[0].RateContracted > 0)
                {
                    uoTextBoxRateContract.Text = string.Format("{0:#,##0.00}", listVehicle[0].RateContracted);
                }
                if (listVehicle[0].RateConfirmed > 0)
                {
                    uoTextBoxRateConfirmed.Text = string.Format("{0:#,##0.00}", listVehicle[0].RateConfirmed);
                }
                uoTextBoxConfirmation.Text = GlobalCode.Field2String(listVehicle[0].ConfirmationNo).Trim();

                sCurrency = GlobalCode.Field2Int(listVehicle[0].CurrencyID).ToString();
                uoHiddenFieldPortAgentID.Value = listVehicle[0].PortAgentID.ToString();
                uoHiddenFieldContractID.Value = listVehicle[0].ContractID.ToString();
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
                }
            }
            uoTextBoxEmailAdd.Text = GlobalCode.Field2String(Session["PortAgentEmailVendor"]);
        }
        #endregion
    }
}

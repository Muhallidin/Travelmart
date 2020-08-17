using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Reflection;

namespace TRAVELMART.ContractManagement
{
    public partial class VehicleContractAdd : System.Web.UI.Page
    {
        #region Declarations

        private string _FileName;
        private string _FileType;
        private Int32 _FileSize = 0;
        private Byte[] _FileData;
        private DateTime _Now = DateTime.Now;

        #endregion

        #region Events
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Load all details    
        /// ------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// ------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  25/Sept/2013
        /// Description:    Use ContractBLL.GetVendorVehicleContractByContractID to bind details
        /// ------------------------------------
        /// Modified by:    Michael Evangelista
        /// Date Modified:  10/June/2014
        /// Description:    Added uoCheckBoxAirportToHotel uoCheckBoxAirportToHotel uoCheckBoxHotelToShip
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //SFStatus.Visible = false;
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                Session.Remove("VehicleRoute");
                Session.Remove("ContractVehicleDetailsAmt");
                Session.Remove("ContractVendorVehicleTypeCapacity");

                uoHiddenFieldContractID.Value = GlobalCode.Field2Int(Request.QueryString["cID"]).ToString();
                uoHiddenFieldVendorId.Value = Request.QueryString["vmId"];
                uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);

                int iContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                if (iContractID > 0)
                {
                    ContractBLL.GetVendorVehicleContractByContractID(uoHiddenFieldContractID.Value, uoHiddenFieldVendorId.Value, 0);
                }
                BindCurrency();
                BindVehicleBranch(iContractID);
                if (iContractID == 0)
                {
                    BindAirportSeaport(0, 0);
                    BindVehicleType(0, 0);
                }
                else
                {
                    BindAirportSeaport(iContractID, 0);
                    BindVehicleType(iContractID, 0);
                    BindAttachmentListView(iContractID, 0);
                }
                textChangeToUpperCase(uoDropDownListVehicleType);
                
                BindVehicleTypeCapacity();
                BindRoute();
                BindContractVehicleDetailsList();
            }
            Session["UserName"] = uoHiddenFieldUserName.Value;
            uoButtonAddVehicle.Text = uoHiddenFieldDetail.Value == "0" ? "Add" : "Save";
        }
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update vendor vehicle contract        
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveContract();
        }
        /// <summary>
        /// Date Created: 18/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Upload vehicle contract attachment       
        /// ---------------------------------------------------------
        /// Date Modified:  2013/08/23
        /// Modified By:    Josephine Gad
        /// (description)   Upload file in listview
        /// </summary>
        protected void uoButtonUpload_Click(object sender, EventArgs e)
        {
            if (uoFileUploadContract.PostedFile == null || string.IsNullOrEmpty(uoFileUploadContract.PostedFile.FileName) || uoFileUploadContract.PostedFile.InputStream == null)
            {
                Label1.Text = "<br />Error - unable to upload file. Please try again.<br />";
                uoFileUploadContract.Focus();
            }
            else
            {
                ContractVehicleAttachment item = new ContractVehicleAttachment();
                int fileSize = uoFileUploadContract.PostedFile.ContentLength;
                if (fileSize > (10000 * 1024)) //10000 means 10MB
                {
                    Label1.Text = "Filesize is too large. Maximum file size permitted is 10MB";
                    uoFileUploadContract.Focus();
                    return;
                }
                //Label1.Visible = false;
                byte[] imageBytes = new byte[uoFileUploadContract.PostedFile.InputStream.Length + 1];
                uoFileUploadContract.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                uoHiddenFieldFileName.Value = uoFileUploadContract.FileName;
                uoHiddenFieldFileType.Value = uoFileUploadContract.PostedFile.ContentType;

                //uoFileUploadContract.Visible = false;
                //ucLabelAttached.Visible = true;

                //ucLabelAttached.Text = uoHiddenFieldFileName.Value;

                //ViewState["imageByte"] = imageBytes;
                //uoLinkAttach.Visible = false;
                //uoLinkButtonRemove.Visible = true;
                uoButtonSave.Focus();

                List<ContractVehicleAttachment> list = new List<ContractVehicleAttachment>();
                list = GetAttachmentList();

                item.FileName = uoFileUploadContract.FileName;
                item.FileType = uoFileUploadContract.PostedFile.ContentType;
                item.UploadedDate = DateTime.Now;
                item.uploadedFile = imageBytes;
                list.Add(item);
                Session["ContractVehicleAttachment"] = list;
                BindAttachmentListView(0, 0);
            }
        }

        protected void uoButtonAirportAdd_Click(object sender, EventArgs e)
        {
            AirportListViewAdd(GlobalCode.Field2Int(uoDropDownListAirport.SelectedValue), uoDropDownListAirport.SelectedItem.Text);
        }
        protected void uoButtonSeaportAdd_Click(object sender, EventArgs e)
        {
            SeaportListViewAdd(GlobalCode.Field2Int(uoDropDownListSeaport.SelectedValue), uoDropDownListSeaport.SelectedItem.Text);
        }
        protected void uoButtonAirportFilter_Click(object sender, EventArgs e)
        {
            BindAirportDroDown(GlobalCode.Field2Int(uoHiddenFieldContractID.Value), 1);
        }
        protected void uoButtonSeaportFilter_Click(object sender, EventArgs e)
        {
            BindSeaportDroDown(GlobalCode.Field2Int(uoHiddenFieldContractID.Value), 1);
        }
        protected void uoButtonVehicleTypeCapacityAdd_Click(object sender, EventArgs e)
        {
            VehicleTypeCapacityAdd(GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue),
                uoDropDownListVehicleType.SelectedItem.Text);
        }
        protected void uoListViewVehicleTypeCapacity_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label uoLabelVehicleTypeName = (Label)uoListViewVehicleTypeCapacity.Items[e.ItemIndex].FindControl("uoLabelVehicleTypeName");
            HiddenField uoHiddenFieldVehicleTypeID = (HiddenField)uoListViewVehicleTypeCapacity.Items[e.ItemIndex].FindControl("uoHiddenFieldVehicleTypeID");

            int i = GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value);
            VehicleTypeCapacityRemove(i, uoLabelVehicleTypeName.Text);
        }
        protected void uoListViewAirport_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label uoLabelAirport = (Label)uoListViewAirport.Items[e.ItemIndex].FindControl("uoLabelAirport");
            HiddenField uoHiddenFieldAirportID = (HiddenField)uoListViewAirport.Items[e.ItemIndex].FindControl("uoHiddenFieldAirportID");

            int i = GlobalCode.Field2Int(uoHiddenFieldAirportID.Value);
            AirportListViewRemove(i, uoLabelAirport.Text);
        }
        protected void uoListViewSeaport_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label uoLabelSeaport = (Label)uoListViewSeaport.Items[e.ItemIndex].FindControl("uoLabelSeaport");
            HiddenField uoHiddenFieldSeaportID = (HiddenField)uoListViewSeaport.Items[e.ItemIndex].FindControl("uoHiddenFieldSeaportID");

            int i = GlobalCode.Field2Int(uoHiddenFieldSeaportID.Value);
            SeaportListViewRemove(i, uoLabelSeaport.Text);
        }
        protected void uoListViewAttachment_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            LinkButton uoLinkButtonFileName = (LinkButton)uoListViewAttachment.Items[e.ItemIndex].FindControl("uoLinkButtonFileName");
            AttachmentListRemove(uoLinkButtonFileName.Text);
        }
        protected void ucLabelAttached_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            LinkButton obj = (LinkButton)sender;
            string sFilename = obj.CommandArgument;

            List<ContractVehicleAttachment> list = new List<ContractVehicleAttachment>();
            list = GetAttachmentList();

            byte[] bytes = (byte[])list.AsEnumerable().Where(
                    data => data.FileName == sFilename).Select(data => data.uploadedFile).FirstOrDefault();

            string fPath = System.IO.Path.GetTempPath();
            string fName = fPath + System.IO.Path.GetTempFileName();

            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = list.AsEnumerable().Where(
                    data => data.FileName == sFilename).Select(data => data.FileType).FirstOrDefault().Trim(); ;

            Response.AddHeader("content-length", bytes.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=" + obj.Text);

            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        protected void uoDropDownListRouteFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindOrigin(uoDropDownListRouteFrom.SelectedItem.Text);
            //BindDestination(uoDropDownListRouteFrom.SelectedItem.Text);
        }
        protected void uoDropDownListRouteTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindOrigin(uoDropDownListRouteTo.SelectedItem.Text);
            BindDestination(uoDropDownListRouteTo.SelectedItem.Text);
        }
        protected void uoButtonAddVehicle_Click(object sender, EventArgs e)
        {
            ContractVehicleDetailsAdd();
        }
        protected void uoGridViewVehicle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iID = GlobalCode.Field2Int(uoGridViewVehicle.Rows[e.RowIndex].Cells[0].Text);
            ContractVehicleDetailsRemove(iID);
        }
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   Edit Details/Amount of contract
        /// ---------------------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoGridViewVehicle_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int iID = GlobalCode.Field2Int(uoGridViewVehicle.Rows[e.NewSelectedIndex].Cells[0].Text);

            List<ContractVehicleDetailsAmt> list = new List<ContractVehicleDetailsAmt>();
            //ContractVehicleDetailsAmt item = new ContractVehicleDetailsAmt();

            list = GetContractVehicleDetailsList();

            uoHiddenFieldDetail.Value = iID.ToString();
            var item = list.Where(data => data.ContractDetailID == iID).ToList();
            if (uoDropDownListVehicleTypeCapacity.Items.FindByValue(item[0].ContractVehicleCapacityID.ToString()) != null)
            {
                //uoDropDownListVehicleTypeCapacity.Items.FindByValue(item[0].ContractVehicleCapacityID.ToString()).Selected = true;
                uoDropDownListVehicleTypeCapacity.SelectedValue = item[0].ContractVehicleCapacityID.ToString();
            }
            if (uoDropDownListRouteFrom.Items.FindByValue(item[0].RouteIDFrom.ToString()) != null)
            {
                //uoDropDownListRouteFrom.Items.FindByValue(item[0].RouteID.ToString()).Selected = true;            
                uoDropDownListRouteFrom.SelectedValue = item[0].RouteIDFrom.ToString();
            }
            BindOrigin(uoDropDownListRouteFrom.SelectedItem.Text);
            BindDestination(uoDropDownListRouteFrom.SelectedItem.Text);

            if (uoDropDownListRouteTo.Items.FindByValue(item[0].RouteIDTo.ToString()) != null)
            {
                //uoDropDownListRouteFrom.Items.FindByValue(item[0].RouteID.ToString()).Selected = true;            
                uoDropDownListRouteTo.SelectedValue = item[0].RouteIDTo.ToString();
            }
            //BindOrigin(uoDropDownListRouteTo.SelectedItem.Text);
            //BindDestination(uoDropDownListRouteTo.SelectedItem.Text);

            if (uoDropDownListOrigin.Items.FindByValue(item[0].Origin) != null)
            {
                //uoDropDownListOrigin.Items.FindByValue(item[0].Origin).Selected = true;
                uoDropDownListOrigin.SelectedValue = item[0].Origin;
            }
            if (uoDropDownListDestination.Items.FindByValue(item[0].Destination) != null)
            {
                //uoDropDownListDestination.Items.FindByValue(item[0].Destination).Selected = true;
                uoDropDownListDestination.SelectedValue = item[0].Destination;
            }
            uoTextBoxVehicleRate.Text = item[0].RateAmount.ToString("#.00");
            uoTextBoxTax.Text = item[0].Tax.ToString("#.00");
            uoDropDownListVehicleTypeCapacity.Focus();
            //ContractVehicleDetailsAdd();
        }

        protected void uoButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("VehicleContractListView.aspx?vmId=" + uoHiddenFieldVendorId.Value + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);           
        }
        #endregion

        #region Functions
        /// <summary> 
        /// Date Created: 01/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load vehicle branch to dropdownlist
        /// -------------------------------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter VendorID
        ///                 Set the default value if there is only 1 item
        /// -------------------------------------------------------
        /// Date Modified:  12/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Bind all details in 1 function 
        ///                 use list instead of DataTable
        /// 
        /// 
        /// </summary>
        private void BindVehicleBranch(int iContractID)
        {
            //DataTable dt = new DataTable();
            try
            {
                if (iContractID == 0)
                {
                    VendorMaintenanceBLL.VehicleVendorsGetByID(GlobalCode.Field2Int(Request.QueryString["vmId"]), 0);
                    List<VendorVehicleDetails> listVehicleDetails = new List<VendorVehicleDetails>();

                    if (Session["VehicleVendorDetails"] != null)
                    {
                        listVehicleDetails = (List<VendorVehicleDetails>)Session["VehicleVendorDetails"];
                        if (listVehicleDetails.Count > 0)
                        {
                            uoTextBoxContractTitle.Text = listVehicleDetails[0].VendorName + " Contract";
                            uoTextBoxVendorBranch.Text = listVehicleDetails[0].VendorName;

                            uoTextBoxCountry.Text = listVehicleDetails[0].CountryName;
                            uoTextBoxCity.Text = listVehicleDetails[0].CityName;
                        }
                    }
                }
                else
                {
                    List<ContractVehicleDetails> list = new List<ContractVehicleDetails>();
                    list = (List<ContractVehicleDetails>)Session["ContractVehicleDetails"];
                    if (list.Count > 0)
                    {
                        uoTextBoxContractTitle.Text = list[0].ContractName;
                        uoTextBoxVendorBranch.Text = list[0].VehicleName;

                        uoTextBoxCountry.Text = list[0].CountryName;
                        uoTextBoxCity.Text = list[0].CityName;

                        uoTextBoxRemarks.Text = list[0].Remarks;

                        if (list[0].ContractDateStart != null)
                        {
                            uoTextBoxContractStartDate.Text = list[0].ContractDateStart.Value.ToString("MM/dd/yyyy");
                        }
                        if (list[0].ContractDateEnd != null)
                        {
                            uoTextBoxContractEndDate.Text = list[0].ContractDateEnd.Value.ToString("MM/dd/yyyy");
                        }
                        if (list[0].RCCLAcceptedDate != null)
                        {
                            uoTextBoxRCCLDateAccepted.Text = list[0].RCCLAcceptedDate.Value.ToString("MM/dd/yyyy");
                        }
                        if (list[0].VendorAcceptedDate != null)
                        {
                            uoTextBoxVendorDateAccepted.Text = list[0].VendorAcceptedDate.Value.ToString("MM/dd/yyyy");
                        }

                        uotextboxRCCLRep.Text = list[0].RCCLPersonnel;
                        uotextboxVendorRep.Text = list[0].VendorPersonnel;
                        uoDropDownListCurrency.SelectedValue = GlobalCode.Field2String(list[0].CurrencyID);
                        uoCheckBoxAirportToHotel.Checked = list[0].IsAirportToHotel;
                        uoCheckBoxHotelToShip.Checked = list[0].IsHotelToShip;

                        uoCheckBoxListBrand.Items[0].Selected = list[0].IsRCI;
                        uoCheckBoxListBrand.Items[1].Selected = list[0].IsAZA;
                        uoCheckBoxListBrand.Items[2].Selected = list[0].IsCEL;
                        uoCheckBoxListBrand.Items[3].Selected = list[0].IsPUL;
                        uoCheckBoxListBrand.Items[4].Selected = list[0].IsSKS;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created: 24/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Close this page and update parent page            
        /// </summary>
        private void parentPageRefresh()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicleContract\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Format data to uppercase        
        /// </summary>
        private void textChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }
        private void Clear()
        {
            //uoTextBoxVehicleCapacity.Text = "";          
            uoTextBoxVehicleRate.Text = "";
            uoDropDownListVehicleType.SelectedIndex = 0;
            //uoDropDownListOrigin.SelectedIndex = 0;
            //uoDropDownListDestination.SelectedIndex = 0;
            //uoDropDownListCurrency.SelectedIndex = 0;
            uoDropDownListVehicleTypeCapacity.SelectedValue = "0";
            uoDropDownListRouteFrom.SelectedValue = "0";
            uoDropDownListRouteTo.SelectedValue = "0";
            uoDropDownListOrigin.SelectedValue = "0";
            uoDropDownListDestination.SelectedValue = "0";

        }
        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void VehicleContractLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"] != "" && Request.QueryString["cID"] != null)
            {
                strLogDescription = "Ammend linkbutton for vehicle contract editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for vehicle contract editor clicked.";
            }

            strFunction = "VehicleContractLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport and Seaport
        private void BindAirportSeaport(int iContractID, Int16 iLoadType)
        {
            if (iContractID == 0)
            {
                VendorMaintenanceBLL.VehicleVendorsAirportGet(0,
                    GlobalCode.Field2TinyInt(uoDropDownListAirportFilter.SelectedValue),
                    uoTextBoxAirportFilter.Text.Trim(),
                    false, 0);

                VendorMaintenanceBLL.VehicleVendorsSeaportGet(0,
                    GlobalCode.Field2TinyInt(uoDropDownListSeaportFilter.SelectedValue),
                    uoTextBoxSeaportFilter.Text.Trim(),
                    false, 0);

            }
            BindAirportDroDown(iContractID, iLoadType);
            BindAirportListView(iContractID, iLoadType);

            BindSeaportDroDown(iContractID, iLoadType);
            BindSeaportListView(iContractID, iLoadType);
        }
        /// <summary>
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport in Drop Down List
        ///                 List of Airport to be added in Contract
        /// </summary>        
        private void BindAirportDroDown(int iContractID, Int16 iLoadType)
        {
            List<Airport> list = new List<Airport>();
            uoDropDownListAirport.Items.Clear();

            if (iLoadType == 0)
            {
                if (Session["VendorAirportNOTExists"] != null)
                {
                    list = (List<Airport>)Session["VendorAirportNOTExists"];
                    uoDropDownListAirport.DataSource = list;
                    uoDropDownListAirport.DataTextField = "AirportName";
                    uoDropDownListAirport.DataValueField = "AirportID";
                    uoDropDownListAirport.DataBind();
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsAirportGet(0,
                   GlobalCode.Field2TinyInt(uoDropDownListAirportFilter.SelectedValue),
                   uoTextBoxAirportFilter.Text.Trim(), false, 1);

                if (Session["VendorAirportNOTExists"] != null)
                {
                    list = (List<Airport>)Session["VendorAirportNOTExists"];
                    uoDropDownListAirport.DataSource = list;
                    uoDropDownListAirport.DataTextField = "AirportName";
                    uoDropDownListAirport.DataValueField = "AirportID";
                    uoDropDownListAirport.DataBind();
                }
            }
            if (list.Count == 1)
            {
                if (uoDropDownListAirport.Items.FindByValue(list[0].AirportID.ToString()) != null)
                {
                    uoDropDownListAirport.SelectedValue = list[0].AirportID.ToString();
                }
            }
            uoDropDownListAirport.Items.Insert(0, new ListItem("--Select Airport--", "0"));
        }
        /// <summary>
        /// Date Created:   13/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport in ListView
        ///                 List of Airport in Contract
        /// </summary>        
        private void BindAirportListView(int iContractID, Int16 iLoadType)
        {
            List<Airport> list = new List<Airport>();
            uoListViewAirport.DataSource = null;

            if (iLoadType == 0)
            {
                if (Session["VendorAirportExists"] != null)
                {
                    list = (List<Airport>)Session["VendorAirportExists"];
                    uoListViewAirport.DataSource = list;
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsAirportGet(0,
                   GlobalCode.Field2TinyInt(uoDropDownListAirportFilter.SelectedValue),
                   uoTextBoxAirportFilter.Text.Trim(), true, 1);

                if (Session["VendorAirportExists"] != null)
                {
                    list = (List<Airport>)Session["VendorAirportExists"];
                    uoListViewAirport.DataSource = list;
                }
            }
            uoListViewAirport.DataBind();
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List from Session or ListView
        /// </summary> 
        private List<Airport> GetAirportList()
        {
            List<Airport> list = new List<Airport>();
            Airport airItem = null;
            if (Session["VendorAirportExists"] != null)
            {
                list = (List<Airport>)Session["VendorAirportExists"];
            }
            else
            {
                HiddenField lvHiddenFieldAirportID;
                HiddenField lvHiddenFieldContractAirportID;
                Label lvLabelAirport;


                foreach (ListViewItem item in uoListViewAirport.Items)
                {
                    lvHiddenFieldContractAirportID = (HiddenField)item.FindControl("uoHiddenFieldContractAirportID");
                    lvHiddenFieldAirportID = (HiddenField)item.FindControl("uoHiddenFieldAirportID");
                    lvLabelAirport = (Label)item.FindControl("uoLabelAirport");

                    airItem = new Airport();

                    airItem.AirportSeaportID = GlobalCode.Field2Int(lvHiddenFieldContractAirportID.Value);
                    airItem.AirportID = GlobalCode.Field2Int(lvHiddenFieldAirportID.Value);
                    airItem.AirportName = lvLabelAirport.Text;

                    list.Add(airItem);
                }
            }
            return list;
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Add Airport in ListView and remove from Drop Down
        /// </summary> 
        private void AirportListViewAdd(int iAirportID, string sAirportName)
        {
            uoListViewAirport.DataSource = null;
            try
            {
                List<Airport> list = new List<Airport>();
                Airport airItem = null;

                list = GetAirportList();

                airItem = new Airport();
                airItem.AirportSeaportID = 0;
                airItem.AirportID = iAirportID;
                airItem.AirportName = sAirportName;
                list.Add(airItem);

                list = list.OrderBy(a => a.AirportName).ToList();

                Session["VendorAirportExists"] = list;
                BindAirportListView(0, 0);

                //==================================================================
                //Remove selected from DropDownList
                //==================================================================
                List<Airport> listDDL = new List<Airport>();
                if (Session["VendorAirportNOTExists"] != null)
                {
                    listDDL = (List<Airport>)Session["VendorAirportNOTExists"];
                }
                else
                {
                    airItem = new Airport();
                    airItem.AirportSeaportID = 0;
                    airItem.AirportID = iAirportID;
                    airItem.AirportName = sAirportName;
                    listDDL.Add(airItem);
                }
                listDDL.RemoveAll(a => listDDL.Exists(b => a.AirportID == iAirportID));
                listDDL = listDDL.OrderBy(a => a.AirportName).ToList();
                Session["VendorAirportNOTExists"] = listDDL;
                BindAirportDroDown(0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   26/Sept/2013
        /// Created By:     Josephine Gad
        /// (description)   Remove Airport in ListView and Add in Drop Down
        /// </summary> 
        private void AirportListViewRemove(int iAirportID, string sAirportName)
        {
            List<Airport> list = new List<Airport>();
            list = GetAirportList();

            list.RemoveAll(a => list.Exists(b => a.AirportID == iAirportID));
            list = list.OrderBy(a => a.AirportName).ToList();

            Session["VendorAirportExists"] = list;
            BindAirportListView(0, 0);


            List<Airport> listDDL = new List<Airport>();
            if (Session["VendorAirportNOTExists"] != null)
            {
                listDDL = (List<Airport>)Session["VendorAirportNOTExists"];
            }

            Airport airItem = null;
            airItem = new Airport();
            airItem.AirportSeaportID = 0;
            airItem.AirportID = iAirportID;
            airItem.AirportName = sAirportName;
            listDDL.Add(airItem);

            listDDL = listDDL.OrderBy(a => a.AirportName).ToList();
            Session["VendorAirportNOTExists"] = listDDL;
            BindAirportDroDown(0, 0);
        }
        /// <summary>
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Seaport in Drop Down List
        ///                 List of Seaport to be added in Contract
        /// </summary>        
        private void BindSeaportDroDown(int iContractID, Int16 iLoadType)
        {
            List<Seaport> list = new List<Seaport>();
            uoDropDownListSeaport.Items.Clear();

            if (iLoadType == 0)
            {
                if (Session["VendorSeaportNOTExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportNOTExists"];
                    uoDropDownListSeaport.DataSource = list;
                    uoDropDownListSeaport.DataTextField = "SeaportName";
                    uoDropDownListSeaport.DataValueField = "SeaportID";
                    uoDropDownListSeaport.DataBind();
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsSeaportGet(0,
                   GlobalCode.Field2TinyInt(uoDropDownListSeaportFilter.SelectedValue),
                   uoTextBoxSeaportFilter.Text.Trim(), false, 1);

                if (Session["VendorSeaportNOTExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportNOTExists"];
                    uoDropDownListSeaport.DataSource = list;
                    uoDropDownListSeaport.DataTextField = "SeaportName";
                    uoDropDownListSeaport.DataValueField = "SeaportID";
                    uoDropDownListSeaport.DataBind();
                }
            }
            if (list.Count == 1)
            {
                if (uoDropDownListSeaport.Items.FindByValue(list[0].SeaportID.ToString()) != null)
                {
                    uoDropDownListSeaport.SelectedValue = list[0].SeaportID.ToString();
                }
            }
            uoDropDownListSeaport.Items.Insert(0, new ListItem("--Select Seaport--", "0"));
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Seaport in ListView
        ///                 List of Seaport in Contract
        /// </summary>        
        private void BindSeaportListView(int iContractID, Int16 iLoadType)
        {
            List<Seaport> list = new List<Seaport>();
            uoListViewSeaport.DataSource = null;

            if (iLoadType == 0)
            {
                if (Session["VendorSeaportExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportExists"];
                    uoListViewSeaport.DataSource = list;
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsSeaportGet(0,
                   GlobalCode.Field2TinyInt(uoDropDownListSeaportFilter.SelectedValue),
                   uoTextBoxSeaportFilter.Text.Trim(), true, 1);

                if (Session["VendorSeaportExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportExists"];
                    uoListViewSeaport.DataSource = list;
                }
            }
            uoListViewSeaport.DataBind();
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Add Seaport in ListView and remove from Drop Down
        /// </summary> 
        private void SeaportListViewAdd(int iSeaportID, string sSeaportName)
        {
            uoListViewSeaport.DataSource = null;

            try
            {
                List<Seaport> list = new List<Seaport>();
                Seaport seaItem = null;

                list = GetSeaportList();

                seaItem = new Seaport();
                seaItem.ID = 0;
                seaItem.SeaportID = iSeaportID;
                seaItem.SeaportName = sSeaportName;
                list.Add(seaItem);

                list = list.OrderBy(a => a.SeaportName).ToList();

                Session["VendorSeaportExists"] = list;
                BindSeaportListView(0, 0);

                //==================================================================
                //Remove selected from DropDownList
                //==================================================================
                List<Seaport> listDDL = new List<Seaport>();
                if (Session["VendorSeaportNOTExists"] != null)
                {
                    listDDL = (List<Seaport>)Session["VendorSeaportNOTExists"];
                }
                else
                {
                    seaItem = new Seaport();
                    seaItem.ID = 0;
                    seaItem.SeaportID = iSeaportID;
                    seaItem.SeaportName = sSeaportName;
                    listDDL.Add(seaItem);
                }
                listDDL.RemoveAll(a => listDDL.Exists(b => a.SeaportID == iSeaportID));
                listDDL = listDDL.OrderBy(a => a.SeaportName).ToList();
                Session["VendorSeaportNOTExists"] = listDDL;
                BindSeaportDroDown(0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine Gad
        /// (description)   Remove Seaport in ListView and add in Drop Down
        /// </summary> 
        private void SeaportListViewRemove(int iSeaportID, string sSeaportName)
        {
            List<Seaport> list = new List<Seaport>();
            list = GetSeaportList();

            list.RemoveAll(a => list.Exists(b => a.SeaportID == iSeaportID));
            list = list.OrderBy(a => a.SeaportName).ToList();

            Session["VendorSeaportExists"] = list;
            BindSeaportListView(0, 0);


            List<Seaport> listDDL = new List<Seaport>();
            if (Session["VendorSeaportNOTExists"] != null)
            {
                listDDL = (List<Seaport>)Session["VendorSeaportNOTExists"];
            }
            Seaport seaItem = null;
            seaItem = new Seaport();
            seaItem.ID = 0;
            seaItem.SeaportID = iSeaportID;
            seaItem.SeaportName = sSeaportName;
            listDDL.Add(seaItem);
            listDDL = listDDL.OrderBy(a => a.SeaportName).ToList();

            Session["VendorSeaportNOTExists"] = listDDL;
            BindSeaportDroDown(0, 0);
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List from Session or ListView
        /// </summary> 
        private List<Seaport> GetSeaportList()
        {
            List<Seaport> list = new List<Seaport>();
            Seaport seaItem = null;
            if (Session["VendorSeaportExists"] != null)
            {
                list = (List<Seaport>)Session["VendorSeaportExists"];
            }
            else
            {
                HiddenField lvHiddenFieldSeaportID;
                HiddenField lvHiddenFieldContractSeaportID;
                Label lvLabelSeaport;


                foreach (ListViewItem item in uoListViewSeaport.Items)
                {
                    lvHiddenFieldContractSeaportID = (HiddenField)item.FindControl("uoHiddenFieldContractSeaportID");
                    lvHiddenFieldSeaportID = (HiddenField)item.FindControl("uoHiddenFieldSeaportID");
                    lvLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                    seaItem = new Seaport();

                    seaItem.ID = GlobalCode.Field2Int(lvHiddenFieldContractSeaportID.Value);
                    seaItem.SeaportID = GlobalCode.Field2Int(lvHiddenFieldSeaportID.Value);
                    seaItem.SeaportName = lvLabelSeaport.Text;

                    list.Add(seaItem);
                }
            }
            return list;
        }
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport and Seaport
        private void BindVehicleType(int iContractID, Int16 iLoadType)
        {
            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();

            if (iLoadType == 0)
            {
                VendorMaintenanceBLL.VehicleVendorsVehicleTypeGet(iContractID, GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                false, 0);
            }
            BindVehicleTypeDropDown(0, iLoadType);
            BindVehicleTypeCapacityListView(0, iLoadType);
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Vehicle Type Capacity in ListView
        /// </summary>
        private void BindVehicleTypeCapacityListView(int iContractID, Int16 iLoadType)
        {
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();
            if (iLoadType == 0)
            {
                if (Session["ContractVendorVehicleTypeCapacity"] != null)
                {
                    listVehicleTypeCapacity = (List<ContractVendorVehicleTypeCapacity>)Session["ContractVendorVehicleTypeCapacity"];
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsVehicleTypeGet(iContractID, GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                 true, 1);

                if (Session["ContractVendorVehicleTypeCapacity"] != null)
                {
                    listVehicleTypeCapacity = (List<ContractVendorVehicleTypeCapacity>)Session["ContractVendorVehicleTypeCapacity"];
                }
            }
            uoListViewVehicleTypeCapacity.DataSource = listVehicleTypeCapacity;
            uoListViewVehicleTypeCapacity.DataBind();
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle type capacity List from Session or ListView
        /// </summary>
        /// <returns></returns>
        private List<ContractVendorVehicleTypeCapacity> GetVehicleTypeCapacityList()
        {
            List<ContractVendorVehicleTypeCapacity> list = new List<ContractVendorVehicleTypeCapacity>();
            ContractVendorVehicleTypeCapacity itemCapacity = null;
            if (Session["ContractVendorVehicleTypeCapacity"] != null)
            {
                list = (List<ContractVendorVehicleTypeCapacity>)Session["ContractVendorVehicleTypeCapacity"];
            }
            else
            {
                HiddenField lvHiddenFieldContractVehicleCapacityID;
                HiddenField lvHiddenFieldVehicleTypeID;
                Label lvLabelVehicleTypeName;
                Label lvLabelMinCapacity;
                Label lvLabelMaxCapacity;

                foreach (ListViewItem item in uoListViewSeaport.Items)
                {
                    lvHiddenFieldContractVehicleCapacityID = (HiddenField)item.FindControl("uoHiddenFieldContractVehicleCapacityID");
                    lvHiddenFieldVehicleTypeID = (HiddenField)item.FindControl("uoHiddenFieldVehicleTypeID");

                    lvLabelVehicleTypeName = (Label)item.FindControl("uoLabelVehicleTypeName");
                    lvLabelMinCapacity = (Label)item.FindControl("uoLabelMinCapacity");
                    lvLabelMaxCapacity = (Label)item.FindControl("uoLabelMaxCapacity");

                    itemCapacity = new ContractVendorVehicleTypeCapacity();

                    itemCapacity.ContractVehicleCapacityIDInt = GlobalCode.Field2Int(lvHiddenFieldContractVehicleCapacityID.Value);
                    itemCapacity.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                    itemCapacity.VehicleTypeID = GlobalCode.Field2Int(lvHiddenFieldVehicleTypeID.Value);

                    itemCapacity.VehicleType = lvLabelVehicleTypeName.Text;
                    itemCapacity.MinCapacity = GlobalCode.Field2Int(lvLabelMinCapacity.Text);
                    itemCapacity.MaxCapacity = GlobalCode.Field2Int(lvLabelMaxCapacity.Text);

                    list.Add(itemCapacity);
                }
            }
            return list;
        }

        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Vehicle Type Capacity in ListView
        /// </summary>
        private void BindVehicleTypeDropDown(int iContractID, Int16 iLoadType)
        {
            List<VehicleType> listVehicleType = new List<VehicleType>();

            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();
            if (Session["ContractVendorVehicleTypeCapacity"] != null)
            {
                listVehicleTypeCapacity = (List<ContractVendorVehicleTypeCapacity>)Session["ContractVendorVehicleTypeCapacity"];
            }

            if (iLoadType == 0)
            {
                if (Session["VehicleType"] != null)
                {
                    listVehicleType = (List<VehicleType>)Session["VehicleType"];
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsVehicleTypeGet(iContractID, GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                 false, 1);

                if (Session["VehicleType"] != null)
                {
                    listVehicleType = (List<VehicleType>)Session["VehicleType"];
                }
            }

            // listVehicleType.RemoveAll(a => listVehicleType.Exists(b => a.VehicleTypeID == listVehicleTypeCapacity[]));
            if (listVehicleTypeCapacity.Count > 0)
            {
                for (int i = 0; i < listVehicleTypeCapacity.Count; i++)
                {
                    listVehicleType.RemoveAll(a => listVehicleType.Exists(b => a.VehicleTypeID == listVehicleTypeCapacity[i].VehicleTypeID));
                }
            }
            listVehicleType = listVehicleType.OrderBy(a => a.VehicleTypeName).ToList();
            uoDropDownListVehicleType.Items.Clear();
            uoDropDownListVehicleType.DataSource = listVehicleType;
            uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
            uoDropDownListVehicleType.DataValueField = "VehicleTypeID";
            uoDropDownListVehicleType.DataBind();
            uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Add Vehicle in ListView with capacity
        /// </summary> 
        private void VehicleTypeCapacityAdd(int iVehicleType, string sVehicleType)
        {
            uoListViewVehicleTypeCapacity.DataSource = null;

            try
            {
                List<ContractVendorVehicleTypeCapacity> list = new List<ContractVendorVehicleTypeCapacity>();
                ContractVendorVehicleTypeCapacity itemCapacity = null;

                list = GetVehicleTypeCapacityList();

                itemCapacity = new ContractVendorVehicleTypeCapacity();
                itemCapacity.ContractVehicleCapacityIDInt = 0;
                itemCapacity.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                itemCapacity.VehicleTypeID = iVehicleType;

                itemCapacity.VehicleType = sVehicleType;
                itemCapacity.MinCapacity = GlobalCode.Field2Int(uoTextBoxMin.Text);
                itemCapacity.MaxCapacity = GlobalCode.Field2Int(uoTextBoxMax.Text);

                list.Add(itemCapacity);
                list = list.OrderBy(a => a.VehicleType).ToList();

                Session["ContractVendorVehicleTypeCapacity"] = list;
                BindVehicleTypeCapacityListView(0, 0);
                BindVehicleTypeDropDown(0, 0);
                BindVehicleTypeCapacity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   25/Sept/2013
        /// Created By:     Josephine Gad
        /// (description)   Remove Vehicle in ListView with capacity
        /// </summary> 
        private void VehicleTypeCapacityRemove(int iVehicleType, string sVehicleType)
        {
            List<ContractVendorVehicleTypeCapacity> list = new List<ContractVendorVehicleTypeCapacity>();
            if (Session["ContractVendorVehicleTypeCapacity"] != null)
            {
                list = (List<ContractVendorVehicleTypeCapacity>)Session["ContractVendorVehicleTypeCapacity"];
                list.RemoveAll(a => list.Exists(b => a.VehicleTypeID == iVehicleType));
                list = list.OrderBy(a => a.VehicleType).ToList();
            }
            Session["ContractVendorVehicleTypeCapacity"] = list;

            List<VehicleType> listVehicleType = new List<VehicleType>();
            VehicleType item = new VehicleType();
            if (Session["VehicleType"] != null)
            {
                listVehicleType = (List<VehicleType>)Session["VehicleType"];
            }
            item.VehicleTypeID = iVehicleType;
            item.VehicleTypeName = sVehicleType;

            listVehicleType.Add(item);

            Session["VehicleType"] = listVehicleType;

            BindVehicleTypeCapacityListView(0, 0);
            BindVehicleTypeDropDown(0, 0);
            BindVehicleTypeCapacity();
        }
        /// <summary>
        /// Date Created:   23/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Attachments
        /// </summary>        
        private void BindAttachmentListView(int iContractID, Int16 iLoadType)
        {
            List<ContractVehicleAttachment> list = new List<ContractVehicleAttachment>();
            list = GetAttachmentList();
            uoListViewAttachment.DataSource = list;
            uoListViewAttachment.DataBind();
        }
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Attachment List from Session or ListView
        /// </summary> 
        private List<ContractVehicleAttachment> GetAttachmentList()
        {
            List<ContractVehicleAttachment> list = new List<ContractVehicleAttachment>();
            if (Session["ContractVehicleAttachment"] != null)
            {
                list = (List<ContractVehicleAttachment>)Session["ContractVehicleAttachment"];
            }
            else
            {
                ContractVehicleAttachment itemList;
                //HiddenField uoHiddenFieldID;
                HiddenField uoLabelVehicleTypeName;
                foreach (ListViewItem item in uoListViewAttachment.Items)
                {
                    itemList = new ContractVehicleAttachment();
                    //uoHiddenFieldID = (HiddenField)item.FindControl("uoHiddenFieldID");
                    uoLabelVehicleTypeName = (HiddenField)item.FindControl("uoLabelVehicleTypeName");

                    itemList.FileName = uoLabelVehicleTypeName.Value;
                    list.Add(itemList);
                }
            }
            return list;
        }
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine Gad
        /// (description)   Remove Attachment from List
        /// </summary> 
        private void AttachmentListRemove(string sFileName)
        {
            List<ContractVehicleAttachment> list = new List<ContractVehicleAttachment>();
            ContractVehicleAttachment item = new ContractVehicleAttachment();
            list = GetAttachmentList();

            list.RemoveAll(a => list.Exists(b => a.FileName == sFileName));
            list = list.OrderBy(a => a.FileName).ToList();

            Session["ContractVehicleAttachment"] = list;
            BindAttachmentListView(0, 0);
        }
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update vendor vehicle contract        
        /// -----------------------------------------------
        /// Date Created:   15/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Save all data in 1 SP including the Airport, Seaport, 
        ///                 Vehicle Capacity, Attachment, Audit Trail
        /// -----------------------------------------------
        /// Date Modified:  3/Oct/2013
        /// Created By:     Marco ABejar
        /// (description)   Route (lDetails)
        /// -----------------------------------------------
        /// </summary>
        private void SaveContract()
        {
            DataTable dtAirport = null;
            DataTable dtSeaport = null;
            DataTable dtVehicleTypeCapacity = null;
            DataTable dtAttachment = null;
            DataTable dtDetails = null;

            try
            {
                string strLogDescription;

                if (GlobalCode.Field2Int(uoHiddenFieldContractID.Value) == 0)
                {
                    strLogDescription = "Add vehicle contract";
                }
                else
                {
                    strLogDescription = "Ammend vehicle contract";
                }
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                List<Airport> listAirport = GetAirportList();
                List<Seaport> listSeaport = GetSeaportList();
                List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = GetVehicleTypeCapacityList();
                List<ContractVehicleAttachment> listAttachment = GetAttachmentList();
                List<ContractVehicleDetailsAmt> listDetails = GetContractVehicleDetailsList();

                var lAirport = (from a in listAirport
                                select new
                                {
                                    AirportID = a.AirportID
                                }).ToList();


                var lSeaport = (from a in listSeaport
                                select new
                                {
                                    SeaportID = a.SeaportID
                                }).ToList();

                var lVehicleTypeCapacity = (from a in listVehicleTypeCapacity
                                            select new
                                            {
                                                VehicleVendorID = GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                                                VehicleTypeID = GlobalCode.Field2Int(a.VehicleTypeID),
                                                MinCapacity = GlobalCode.Field2Int(a.MinCapacity),
                                                MaxCapacity = GlobalCode.Field2Int(a.MaxCapacity),
                                            }).ToList();

                var lAttachment = (from a in listAttachment
                                   select new
                                   {
                                       Filename = a.FileName,
                                       FileType = a.FileType,
                                       uploadedFile = a.uploadedFile,
                                       UploadedDate = a.UploadedDate
                                   }).ToList();

                var lDetails = (from a in listDetails
                                select new
                                {
                                    ContractID = a.ContractID,
                                    BranchID = a.BranchID,
                                    ContractVehicleCapacityID = a.ContractVehicleCapacityID,
                                    RouteIDFrom = a.RouteIDFrom,
                                    RouteIDTo = a.RouteIDTo,
                                    RateAmount = a.RateAmount,
                                    Tax = a.Tax,
                                    Origin = a.Origin,
                                    Destination = a.Destination
                                }).ToList();

                //var lDetails = (from a in listDetails
                //                select new
                //                {
                //                    ContractID = a.ContractID,
                //                    BranchID = a.BranchID,
                //                    ContractVehicleCapacityID = a.ContractVehicleCapacityID,
                //                    RouteID = a.RouteID,
                //                    RateAmount = a.RateAmount,
                //                    Tax = a.Tax,
                //                    Origin = a.Origin,
                //                    Destination = a.Destination
                //                }).ToList();

                dtAirport = getDataTable(lAirport);
                dtSeaport = getDataTable(lSeaport);
                dtVehicleTypeCapacity = getDataTable(lVehicleTypeCapacity);
                dtAttachment = getDataTable(lAttachment);
                dtDetails = getDataTable(lDetails);

  
      
                BLL.ContractBLL.vehicleInsertContract(0,
                    GlobalCode.Field2Int(uoHiddenFieldVendorId.Value), uoTextBoxContractTitle.Text,
                    uoTextBoxRemarks.Text, uoTextBoxContractStartDate.Text, uoTextBoxContractEndDate.Text,
                    uotextboxRCCLRep.Text, uoTextBoxRCCLDateAccepted.Text, uotextboxVendorRep.Text,
                    uoTextBoxVendorDateAccepted.Text, GlobalCode.Field2Int(uoDropDownListCurrency.SelectedValue),
                     uoCheckBoxAirportToHotel.Checked, uoCheckBoxHotelToShip.Checked,
                    uoHiddenFieldUserName.Value, strLogDescription, "SaveContract", Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(currentDate).ToString(), 
                    uoCheckBoxListBrand.Items[0].Selected,
                    uoCheckBoxListBrand.Items[1].Selected,
                    uoCheckBoxListBrand.Items[2].Selected,
                    uoCheckBoxListBrand.Items[3].Selected,
                    uoCheckBoxListBrand.Items[4].Selected,
                    dtAirport, dtSeaport, dtVehicleTypeCapacity,
                    dtAttachment, dtDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                }
                if (dtVehicleTypeCapacity != null)
                {
                    dtVehicleTypeCapacity.Dispose();
                }
                if (dtAttachment != null)
                {
                    dtAttachment.Dispose();
                }
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                }
            }
            Response.Redirect("~/ContractManagement/VehicleContractListView.aspx?vmId=" + Request.QueryString["vmId"] + "&cId=" + Request.QueryString["cID"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/Aug/2013
        /// Description:    convert list to datatable
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
        /// Author:         Josephine Gad
        /// Date Created:   15/Aug/2013
        /// Description:    get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   25/Sep/2013
        /// Description:    Bind Currency
        /// </summary>
        private void BindCurrency()
        {
            List<Currency> listCurrency = new List<Currency>();
            if (Session["ContractCurrency"] != null)
            {
                listCurrency = (List<Currency>)Session["ContractCurrency"];
            }
            else
            {
                DataTable dt = null;
                DataView dv = null;
                try
                {
                    dt = ContractBLL.CurrencyLoad();
                    dv = dt.DefaultView;
                    dv.Sort = "colCurrencyNameVarchar";
                    dt = dv.ToTable();

                    listCurrency = (from a in dt.AsEnumerable()
                                    select new Currency
                                    {
                                        CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                        CurrencyName = GlobalCode.Field2String(a["colCurrencyNameVarchar"])
                                    }).ToList();
                    Session["ContractCurrency"] = listCurrency;

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
                    if (dv != null)
                    {
                        dv.Dispose();
                    }
                }
            }

            uoDropDownListCurrency.Items.Clear();
            uoDropDownListCurrency.DataSource = listCurrency;

            uoDropDownListCurrency.DataTextField = "CurrencyName";
            uoDropDownListCurrency.DataValueField = "CurrencyID";

            uoDropDownListCurrency.DataBind();
            uoDropDownListCurrency.Items.Insert(0, (new ListItem("--Select Currency--", "0")));
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/Sep/2013
        /// Description     Get the contract's vehicle type with capacity
        /// </summary>
        private void BindVehicleTypeCapacity()
        {

            List<ContractVendorVehicleTypeCapacity> list = GetVehicleTypeCapacityList();
            uoDropDownListVehicleTypeCapacity.Items.Clear();
            uoDropDownListVehicleTypeCapacity.Items.Add(new ListItem("--Select Vehicle Type--", "0"));

            if (list.Count > 0)
            {
                uoDropDownListVehicleTypeCapacity.DataSource = list;
                uoDropDownListVehicleTypeCapacity.DataTextField = "VehicleType";
                uoDropDownListVehicleTypeCapacity.DataValueField = "VehicleTypeID";
            }
            uoDropDownListVehicleTypeCapacity.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/Sep/2013
        /// Description     Bind Vehicle's Route   
        /// -------------------------------------
        /// Modified By:    Marco Abejar
        /// Date Created:   2/Oct/2013
        /// Description     Modify sOrigin 
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   06/Nov/2013
        /// Description     Use VendorMaintenanceBLL.VehicleVendorsRouteGet() if Session["VehicleRoute"] is null
        /// </summary>
        private void BindRoute()
        {            
            List<VehicleRoute> list = new List<VehicleRoute>();
            if (Session["VehicleRoute"] != null)
            {
                list = (List<VehicleRoute>)Session["VehicleRoute"];
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsRouteGet();
                list = (List<VehicleRoute>)Session["VehicleRoute"];
            }
            int iCount = list.Count;
            uoDropDownListRouteFrom.Items.Clear();
            uoDropDownListRouteFrom.Items.Add(new ListItem("--Select Route--", "0"));
            uoDropDownListRouteTo.Items.Clear();
            uoDropDownListRouteTo.Items.Add(new ListItem("--Select Route--", "0"));            
            if (iCount > 0)
            {
                uoDropDownListRouteFrom.DataSource = list;
                uoDropDownListRouteFrom.DataTextField = "RouteDesc";
                uoDropDownListRouteFrom.DataValueField = "RouteID";

                uoDropDownListRouteTo.DataSource = list;
                uoDropDownListRouteTo.DataTextField = "RouteDesc";
                uoDropDownListRouteTo.DataValueField = "RouteID";

            }
            uoDropDownListRouteFrom.DataBind();
            uoDropDownListRouteTo.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/Sep/2013
        /// Description     Bind Origin based from selected Route
        /// ------------------------------------------
        /// Modified By:    Marco Abejar
        /// Date Created:   2/Oct/2013
        /// Description     Modify sOrigin 
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   01/Mar/2014
        /// Description     Add RequiredFieldValidatorOrigin settings if not not Ship, Hotel and Airport
        /// </summary>
        private void BindOrigin(string sOrigin)
        {
            string[] sArr = sOrigin.Split('-');
            string[] sItemArr;
            string sText;
            string sValue = "";

            uoDropDownListOrigin.Items.Clear();
            uoDropDownListOrigin.Items.Add(new ListItem("--Select Origin--", "0"));
            RequiredFieldValidatorOrigin.Enabled = true;

            if (sOrigin == "Airport" || sOrigin == "Hotel")
            {
                List<Airport> list = new List<Airport>();
                list = GetAirportList();

                ListItem item;
                int iCount = list.Count;

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        sItemArr = list[i].AirportName.Split('-');
                        if (sItemArr.Count() > 1)
                        {
                            sValue = GlobalCode.Field2String(sItemArr[0]);
                            sText = list[i].AirportName;
                            item = new ListItem(sText, sValue);
                            uoDropDownListOrigin.Items.Add(item);
                        }
                    }
                }
                uoDropDownListOrigin.DataBind();
                if (list.Count == 1)
                {
                    if (uoDropDownListOrigin.Items.FindByValue(sValue) != null)
                    {
                        uoDropDownListOrigin.SelectedValue = sValue;
                    }
                }
            }
            else if (sOrigin == "Ship")
            {
                List<Seaport> list = new List<Seaport>();
                list = GetSeaportList();

                ListItem item;
                int iCount = list.Count;

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        sItemArr = list[i].SeaportName.Split('-');
                        if (sItemArr.Count() > 1)
                        {
                            sValue = GlobalCode.Field2String(sItemArr[0]);
                            sText = list[i].SeaportName;
                            item = new ListItem(sText, sValue);
                            uoDropDownListOrigin.Items.Add(item);
                        }
                    }
                }
                uoDropDownListOrigin.DataBind();
                if (list.Count == 1)
                {
                    if (uoDropDownListOrigin.Items.FindByValue(sValue) != null)
                    {
                        uoDropDownListOrigin.SelectedValue = sValue;
                    }
                }
            }
            else
            {
                RequiredFieldValidatorOrigin.Enabled = false;
            }
        }       
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/Sep/2013
        /// Description     Bind Destination based from selected Route
        /// ------------------------------------------
        /// Modified By:    Marco Abejar
        /// Date Created:   2/Oct/2013
        /// Description     Modify sOrigin 
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   01/Mar/2014
        /// Description     Add RequiredFieldValidatorDestination settings if not not Ship, Hotel and Airport
        /// </summary>
        private void BindDestination(string sDestination)
        {
            string[] sArr = sDestination.Split('-');
            string[] sItemArr;
            string sText;
            string sValue = "";

            uoDropDownListDestination.Items.Clear();
            uoDropDownListDestination.Items.Add(new ListItem("--Select Destinaton--", "0"));
            RequiredFieldValidatorDestination.Enabled = true;

            if (sDestination == "Airport" || sDestination == "Hotel")
            {
                List<Airport> list = new List<Airport>();
                list = GetAirportList();

                ListItem item;
                int iCount = list.Count;

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        sItemArr = list[i].AirportName.Split('-');
                        if (sItemArr.Count() > 1)
                        {
                            sValue = GlobalCode.Field2String(sItemArr[0]);
                            sText = list[i].AirportName;
                            item = new ListItem(sText, sValue);
                            uoDropDownListDestination.Items.Add(item);
                        }
                    }
                }
                uoDropDownListDestination.DataBind();
                if (list.Count == 1)
                {
                    if (uoDropDownListDestination.Items.FindByValue(sValue) != null)
                    {
                        uoDropDownListDestination.SelectedValue = sValue;
                    }
                }
            }
            else if (sDestination == "Ship")
            {
                List<Seaport> list = new List<Seaport>();
                list = GetSeaportList();

                ListItem item;
                int iCount = list.Count;

                if (iCount > 0)
                {
                    for (int i = 0; i < iCount; i++)
                    {
                        sItemArr = list[i].SeaportName.Split('-');
                        if (sItemArr.Count() > 1)
                        {
                            sValue = GlobalCode.Field2String(sItemArr[0]);
                            sText = list[i].SeaportName;
                            item = new ListItem(sText, sValue);
                            uoDropDownListDestination.Items.Add(item);
                        }
                    }
                }
                uoDropDownListDestination.DataBind();
                if (list.Count == 1)
                {
                    if (uoDropDownListDestination.Items.FindByValue(sValue) != null)
                    {
                        uoDropDownListDestination.SelectedValue = sValue;
                    }
                }
            }
            else
            {
                RequiredFieldValidatorDestination.Enabled = false;
            }
        }
        //private void BindDestination(string sDestination)
        //{
        //    string[] sArr = sDestination.Split('-');
        //    string[] sItemArr;
        //    string sText;
        //    string sValue;

        //    uoDropDownListDestination.Items.Clear();
        //    uoDropDownListDestination.Items.Add(new ListItem("--Select Destinaton--", "0"));
        //    if (sArr[0] == "Airport" || sArr[0] == "Hotel")
        //    {
        //        List<Airport> list = new List<Airport>();
        //        list = GetAirportList();

        //        ListItem item;
        //        int iCount = list.Count;

        //        if (iCount > 0)
        //        {
        //            for (int i = 0; i < iCount; i++)
        //            {
        //                sItemArr = list[i].AirportName.Split('-');
        //                if (sItemArr.Count() > 1)
        //                {
        //                    sValue = GlobalCode.Field2String(sItemArr[0]);
        //                    sText = list[i].AirportName;
        //                    item = new ListItem(sText, sValue);
        //                    uoDropDownListDestination.Items.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    else if (sArr[0] == "Pier")
        //    {
        //        List<Seaport> list = new List<Seaport>();
        //        list = GetSeaportList();

        //        ListItem item;
        //        int iCount = list.Count;

        //        if (iCount > 0)
        //        {
        //            for (int i = 0; i < iCount; i++)
        //            {
        //                sItemArr = list[i].SeaportName.Split('-');
        //                if (sItemArr.Count() > 1)
        //                {
        //                    sValue = GlobalCode.Field2String(sItemArr[0]);
        //                    sText = list[i].SeaportName;
        //                    item = new ListItem(sText, sValue);
        //                    uoDropDownListDestination.Items.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    uoDropDownListDestination.DataBind();
        //}
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   Add contract vehicle details to session
        /// -----------------------------------------------------------
        /// Date Modified:   03/Oct/2013
        /// Created By:     Marco Abejar
        /// (description)   Modify route fields (from/to)
        /// </summary> 
        private void ContractVehicleDetailsAdd()
        {
            uoGridViewVehicle.DataSource = null;
            try
            {
                List<ContractVehicleDetailsAmt> list = new List<ContractVehicleDetailsAmt>();
                ContractVehicleDetailsAmt item = new ContractVehicleDetailsAmt();

                list = GetContractVehicleDetailsList();

                item.BranchID = GlobalCode.Field2Int(uoHiddenFieldVendorId.Value);
                item.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                //item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVehicleTypeCapacity.SelectedValue);
                item.ContractVehicleCapacityID = GlobalCode.Field2Int(uoDropDownListVehicleTypeCapacity.SelectedValue);
                item.VehicleType = uoDropDownListVehicleTypeCapacity.SelectedItem.Text;
                item.RouteIDFrom = GlobalCode.Field2Int(uoDropDownListRouteFrom.SelectedValue);
                item.RouteIDTo = GlobalCode.Field2Int(uoDropDownListRouteTo.SelectedValue);
                item.RouteFrom = uoDropDownListRouteFrom.SelectedItem.Text;
                item.RouteTo = uoDropDownListRouteTo.SelectedItem.Text;
                item.Origin = uoDropDownListOrigin.SelectedValue;
                item.Destination = uoDropDownListDestination.SelectedValue;
                item.RateAmount = GlobalCode.Field2Float(uoTextBoxVehicleRate.Text);
                item.Tax = GlobalCode.Field2Float(uoTextBoxTax.Text);

                int iIdentity = 1;
                if (uoHiddenFieldDetail.Value == "0")
                {

                    if (list.Count > 0)
                    {
                        iIdentity = GlobalCode.Field2Int(list[(list.Count - 1)].ContractDetailID) + 1;
                    }
                    item.ContractDetailID = iIdentity;
                    list.Add(item);

                }
                else
                {
                    iIdentity = GlobalCode.Field2Int(uoHiddenFieldDetail.Value);
                    list.RemoveAll(a => list.Exists(b => a.ContractDetailID == iIdentity));
                    item.ContractDetailID = iIdentity;
                    list.Add(item);
                }

                uoHiddenFieldDetail.Value = "0";
                list = list.OrderBy(a => a.RouteFrom).ThenBy(a => a.VehicleType).ToList();

                Session["ContractVehicleDetailsAmt"] = list;
                BindContractVehicleDetailsList();
            }
            //try
            //{
            //    List<ContractVehicleDetailsAmt> list = new List<ContractVehicleDetailsAmt>();
            //    ContractVehicleDetailsAmt item = new ContractVehicleDetailsAmt();

            //    list = GetContractVehicleDetailsList();

            //    item.BranchID = GlobalCode.Field2Int(uoHiddenFieldVendorId.Value);
            //    item.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
            //    //item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVehicleTypeCapacity.SelectedValue);
            //    item.ContractVehicleCapacityID = GlobalCode.Field2Int(uoDropDownListVehicleTypeCapacity.SelectedValue);
            //    item.VehicleType = uoDropDownListVehicleTypeCapacity.SelectedItem.Text;
            //    item.RouteID = GlobalCode.Field2Int(uoDropDownListRoute.SelectedValue);
            //    item.Route = uoDropDownListRoute.SelectedItem.Text;
            //    item.Origin = uoDropDownListOrigin.SelectedValue;
            //    item.Destination = uoDropDownListDestination.SelectedValue;
            //    item.RateAmount = GlobalCode.Field2Float(uoTextBoxVehicleRate.Text);
            //    item.Tax = GlobalCode.Field2Float(uoTextBoxTax.Text);

            //    int iIdentity = 1;
            //    if (uoHiddenFieldDetail.Value == "0")
            //    {

            //        if (list.Count > 0)
            //        {
            //            iIdentity = GlobalCode.Field2Int(list[(list.Count - 1)].ContractDetailID) + 1;
            //        }
            //        item.ContractDetailID = iIdentity;
            //        list.Add(item);
            //    }
            //    else
            //    {
            //        iIdentity = GlobalCode.Field2Int(uoHiddenFieldDetail.Value);
            //        list.RemoveAll(a => list.Exists(b => a.ContractDetailID == iIdentity));
            //        item.ContractDetailID = iIdentity;
            //        list.Add(item);
            //    }

            //    uoHiddenFieldDetail.Value = "0";
            //    list = list.OrderBy(a => a.Route).ThenBy(a => a.VehicleType).ToList();

            //    Session["ContractVehicleDetailsAmt"] = list;
            //    BindContractVehicleDetailsList();
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   Add contract vehicle details to session
        /// </summary> 
        private void ContractVehicleDetailsRemove(int iContractDetailID)
        {
            uoGridViewVehicle.DataSource = null;
            try
            {
                List<ContractVehicleDetailsAmt> list = new List<ContractVehicleDetailsAmt>();
                list = GetContractVehicleDetailsList();

                list.RemoveAll(a => list.Exists(b => a.ContractDetailID == iContractDetailID));
                list = list.OrderBy(a => a.RouteFrom).ThenBy(a => a.VehicleType).ToList();

                Session["ContractVehicleDetailsAmt"] = list;
                BindContractVehicleDetailsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   
        /// </summary> 
        private List<ContractVehicleDetailsAmt> GetContractVehicleDetailsList()
        {
            List<ContractVehicleDetailsAmt> list = new List<ContractVehicleDetailsAmt>();
            //ContractVehicleDetailsAmt item = null;
            if (Session["ContractVehicleDetailsAmt"] != null)
            {
                list = (List<ContractVehicleDetailsAmt>)Session["ContractVehicleDetailsAmt"];
            }
            else
            {
                //HiddenField lvHiddenFieldAirportID;
                //HiddenField lvHiddenFieldContractAirportID;
                //Label lvLabelAirport;


                //foreach (ListViewItem item in uoListViewAirport.Items)
                //{
                //    lvHiddenFieldContractAirportID = (HiddenField)item.FindControl("uoHiddenFieldContractAirportID");
                //    lvHiddenFieldAirportID = (HiddenField)item.FindControl("uoHiddenFieldAirportID");
                //    lvLabelAirport = (Label)item.FindControl("uoLabelAirport");

                //    airItem = new Airport();

                //    airItem.AirportSeaportID = GlobalCode.Field2Int(lvHiddenFieldContractAirportID.Value);
                //    airItem.AirportID = GlobalCode.Field2Int(lvHiddenFieldAirportID.Value);
                //    airItem.AirportName = lvLabelAirport.Text;

                //    list.Add(airItem);
                //}
            }
            return list;
        }
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport in ListView
        ///                 List of Airport in Contract
        /// </summary>        
        private void BindContractVehicleDetailsList()
        {
            List<ContractVehicleDetailsAmt> list = new List<ContractVehicleDetailsAmt>();
            uoGridViewVehicle.DataSource = null;

            if (Session["ContractVehicleDetailsAmt"] != null)
            {
                list = (List<ContractVehicleDetailsAmt>)Session["ContractVehicleDetailsAmt"];
                uoGridViewVehicle.DataSource = list;
            }
            else
            {
                //VendorMaintenanceBLL.VehicleVendorsAirportGet(0,
                //   GlobalCode.Field2TinyInt(uoDropDownListAirportFilter.SelectedValue),
                //   uoTextBoxAirportFilter.Text.Trim(), true, 1);

                //if (Session["ContractVehicleDetailsAmt"] != null)
                //{
                //    list = (List<ContractVehicleDetailsAmt>)Session["ContractVehicleDetailsAmt"];
                //    uoGridViewVehicle.DataSource = list;
                //}
            }
            uoGridViewVehicle.Columns[0].Visible = true;
            uoGridViewVehicle.Columns[1].Visible = true;
            uoGridViewVehicle.Columns[2].Visible = true;
            uoGridViewVehicle.DataBind();
            uoGridViewVehicle.Columns[0].Visible = false;
            uoGridViewVehicle.Columns[1].Visible = false;
            uoGridViewVehicle.Columns[2].Visible = false;
        }
        #endregion
    }
}

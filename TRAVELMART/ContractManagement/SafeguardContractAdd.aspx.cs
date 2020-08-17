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
    public partial class SafeguardContractAdd : System.Web.UI.Page
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
        /// Created By:     Marco Abejar
        /// (description)   Load all details  
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
                uoHiddenFieldContractID.Value = GlobalCode.Field2Int(Request.QueryString["cID"]).ToString();
                uoHiddenFieldVendorId.Value = Request.QueryString["vmId"];
                uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);

                int iContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                if (iContractID > 0)
                {
                    SafeguardBLL.GetVendorSafeguardContractByContractID(uoHiddenFieldContractID.Value, uoHiddenFieldVendorId.Value, 0);
                }
                else
                {
                    Session["ContractServiceDetailsAmt"] = null;
                    Session["ContractSafeguardAttachment"] = null;
                }
                BindCurrency();
                BindSafeguardBranch(iContractID);
                if (iContractID == 0)
                {
                    BindServiceType(0, 0);
                }
                else
                {
                    BindServiceType(iContractID, 0);
                    BindAttachmentListView(iContractID, 0);
                }
                textChangeToUpperCase(uoDropDownListServiceType);

                BindServiceTypeDuration();
                BindContractServiceDetailsList();
            }
            Session["UserName"] = uoHiddenFieldUserName.Value;
            uoButtonAddService.Text = uoHiddenFieldDetail.Value == "0" ? "Add" : "Save";
        }
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport and Seaport
        private void BindServiceType(int iContractID, Int16 iLoadType)
        {
            List<ServiceType> listServiceType = new List<ServiceType>();
            //List<ContractSafeguardTypeDuration> listVehicleTypeCapacity = new List<ContractSafeguardTypeDuration>();

            if (iLoadType == 0)
            {
                SafeguardBLL.ServiceTypeGet(iContractID, GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                false, 0);
            }
            BindSafeguardTypeDropDown(0, iLoadType);
            BindServiceTypeDurationListView(0, iLoadType);
        }

        /// Date Modified:  12/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Bind all details in 1 function 
        ///                 use list instead of DataTable
        /// 
        /// 
        /// </summary>
        private void BindSafeguardBranch(int iContractID)
        {
            //DataTable dt = new DataTable();
            try
            {
                if (iContractID == 0)
                {
                    SafeguardBLL.SafeguardVendorsGetByID(GlobalCode.Field2Int(Request.QueryString["vmId"]), 0);
                    List<VendorSafeguardDetails> listVehicleDetails = new List<VendorSafeguardDetails>();

                    if (Session["SafeguardVendorDetails"] != null)
                    {
                        listVehicleDetails = (List<VendorSafeguardDetails>)Session["SafeguardVendorDetails"];
                        if (listVehicleDetails.Count > 0)
                        {
                            uoTextBoxContractTitle.Text = listVehicleDetails[0].VendorName + " Contract";
                            //uoTextBoxVendorBranch.Text = listVehicleDetails[0].VendorName;

                            //uoTextBoxCountry.Text = listVehicleDetails[0].VendorSafeguardDetails;
                            //uoTextBoxCity.Text = listVehicleDetails[0].CityName;
                        }
                    }
                }
                else
                {
                    List<ContractSafeguardDetails> list = new List<ContractSafeguardDetails>();
                    list = (List<ContractSafeguardDetails>)Session["SafeguardVendorDetails"];
                    if (list.Count > 0)
                    {
                        uoTextBoxContractTitle.Text = list[0].ContractName;
                        //uoTextBoxVendorBranch.Text = list[0].VehicleName;

                        //uoTextBoxCountry.Text = list[0].CountryName;
                        //uoTextBoxCity.Text = list[0].CityName;

                        uoTextBoxRemarks.Text = list[0].Remarks;

                        if (list[0].ContractDateStart != null)
                        {
                            uoTextBoxContractStartDate.Text = list[0].ContractDateStart;
                        }
                        if (list[0].ContractDateEnd != null)
                        {
                            uoTextBoxContractEndDate.Text = list[0].ContractDateEnd;
                        }
                        if (list[0].RCCLAcceptedDate != null)
                        {
                            uoTextBoxRCCLDateAccepted.Text = list[0].RCCLAcceptedDate;
                        }
                        if (list[0].VendorAcceptedDate != null)
                        {
                            uoTextBoxVendorDateAccepted.Text = list[0].VendorAcceptedDate;
                        }

                        uotextboxRCCLRep.Text = list[0].RCCLPersonnel;
                        uotextboxVendorRep.Text = list[0].VendorPersonnel;
                        uoDropDownListCurrency.SelectedValue = GlobalCode.Field2String(list[0].CurrencyID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/Sep/2013
        /// Description     Get the contract's vehicle type with capacity
        /// </summary>
        private void BindServiceTypeDuration()
        {

            List<ContractServiceTypeDuration> list = GetServiceTypeDurationList();
            uoDropDownListServiceTypeDuration.Items.Clear();
            uoDropDownListServiceTypeDuration.Items.Add(new ListItem("--Select Service Type", "0"));

            if (list.Count > 0)
            {
                uoDropDownListServiceTypeDuration.DataSource = list;
                uoDropDownListServiceTypeDuration.DataTextField = "ServiceType";
                uoDropDownListServiceTypeDuration.DataValueField = "ContractSafeguardDurationIDInt";             
            }
            //uoDropDownListServiceTypeDuration.Items.Add(new ListItem("--Select Service Type", "0"));
            uoDropDownListServiceTypeDuration.DataBind();
          

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
                ContractSafeguardAttachment item = new ContractSafeguardAttachment();
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

                List<ContractSafeguardAttachment> list = new List<ContractSafeguardAttachment>();
                list = GetAttachmentList();

                item.FileName = uoFileUploadContract.FileName;
                item.FileType = uoFileUploadContract.PostedFile.ContentType;
                item.UploadedDate = DateTime.Now;
                item.uploadedFile = imageBytes;
                list.Add(item);
                Session["ContractSafeguardAttachment"] = list;
                BindAttachmentListView(0, 0);
            }
        }

        protected void uoButtonServiceTypeDurationAdd_Click(object sender, EventArgs e)
        {
            ServiceTypeDurationAdd(GlobalCode.Field2Int(uoDropDownListServiceType.SelectedValue),
                uoDropDownListServiceType.SelectedItem.Text);
        }
        protected void uoListViewServiceTypeDuration_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label uoLabelServiceTypeName = (Label)uoListViewServiceTypeDuration.Items[e.ItemIndex].FindControl("uoLabelServiceTypeName");
            HiddenField uoHiddenFieldServiceTypeID = (HiddenField)uoListViewServiceTypeDuration.Items[e.ItemIndex].FindControl("uoHiddenFieldServiceTypeID");

            int i = GlobalCode.Field2Int(uoHiddenFieldServiceTypeID.Value);
            ServiceTypeDurationRemove(i, uoLabelServiceTypeName.Text);
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

            List<ContractSafeguardAttachment> list = new List<ContractSafeguardAttachment>();
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

        protected void uoButtonAddService_Click(object sender, EventArgs e)
        {
            ContractServiceDetailsAdd(GlobalCode.Field2Int(uoDropDownListServiceTypeDuration.SelectedValue),
                uoDropDownListServiceTypeDuration.SelectedItem.Text);
        }
        protected void uoGridViewService_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iID = GlobalCode.Field2Int(uoGridViewService.Rows[e.RowIndex].Cells[0].Text);
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
        protected void uoGridViewService_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int iID = GlobalCode.Field2Int(uoGridViewService.Rows[e.NewSelectedIndex].Cells[0].Text);

            List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
            //ContractServiceDetailsAmt item = new ContractServiceDetailsAmt();

            list = GetContractServiceDetailsList();

            uoHiddenFieldDetail.Value = iID.ToString();
            var item = list.Where(data => data.ContractDetailID == iID).ToList();
            if (uoDropDownListServiceTypeDuration.Items.FindByValue(item[0].ContractServiceDurationID.ToString()) != null)
            {
                //uoDropDownListServiceTypeDuration.Items.FindByValue(item[0].ContractVehicleCapacityID.ToString()).Selected = true;
                uoDropDownListServiceTypeDuration.SelectedValue = item[0].ContractServiceDurationID.ToString();
            }
            uoTextBoxVehicleRate.Text = item[0].RateAmount.ToString("#.00");
            uoTextBoxTax.Text = item[0].Tax.ToString("#.00");
            uoDropDownListServiceTypeDuration.Focus();
            //ContractServiceDetailsAdd();
            uoButtonAddService.Text = uoHiddenFieldDetail.Value == "0" ? "Add" : "Save";
        }
        #endregion

        #region Functions

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
            uoDropDownListServiceType.SelectedIndex = 0;
            //uoDropDownListOrigin.SelectedIndex = 0;
            //uoDropDownListDestination.SelectedIndex = 0;
            //uoDropDownListCurrency.SelectedIndex = 0;
            uoDropDownListServiceTypeDuration.SelectedIndex = 0;
        }
        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void SafeguardContractLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"] != "" && Request.QueryString["cID"] != null)
            {
                strLogDescription = "Ammend linkbutton for safeguard contract editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for safeguard contract editor clicked.";
            }

            strFunction = "SafeguardContractLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }


        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Vehicle Type Capacity in ListView
        /// </summary>
        private void BindServiceTypeDurationListView(int iContractID, Int16 iLoadType)
        {
            List<ContractServiceTypeDuration> listServiceTypeDuration = new List<ContractServiceTypeDuration>();
            if (iLoadType == 0)
            {
                if (Session["ContractSafeguardTypeDuration"] != null)
                {
                    listServiceTypeDuration = (List<ContractServiceTypeDuration>)Session["ContractSafeguardTypeDuration"];
                }
            }
            else
            {
                SafeguardBLL.ServiceTypeGet(iContractID, GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                 true, 1);

                if (Session["ContractSafeguardTypeDuration"] != null)
                {
                    listServiceTypeDuration = (List<ContractServiceTypeDuration>)Session["ContractSafeguardTypeDuration"];
                }
            }            
           
            uoListViewServiceTypeDuration.DataSource = listServiceTypeDuration;
            uoListViewServiceTypeDuration.DataBind();        
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle type capacity List from Session or ListView
        /// </summary>
        /// <returns></returns>
        private List<ContractServiceTypeDuration> GetServiceTypeDurationList()
        {
            List<ContractServiceTypeDuration> list = new List<ContractServiceTypeDuration>();
            ContractServiceTypeDuration itemCapacity = null;
            if (Session["ContractSafeguardTypeDuration"] != null)
            {
                list = (List<ContractServiceTypeDuration>)Session["ContractSafeguardTypeDuration"];
            }
            else
            {
                HiddenField lvHiddenFieldContractServiceDurationID;
                HiddenField lvHiddenFieldServiceTypeID;
                Label lvLabelServiceTypeName;
                Label lvLabelFrom;
                Label lvLabelTo;

                foreach (ListViewItem item in uoListViewServiceTypeDuration.Items)
                {
                    lvHiddenFieldContractServiceDurationID = (HiddenField)item.FindControl("uoHiddenFieldContractServiceDurationID");
                    lvHiddenFieldServiceTypeID = (HiddenField)item.FindControl("uoHiddenFieldServiceTypeID");

                    lvLabelServiceTypeName = (Label)item.FindControl("uoLabelServiceTypeName");
                    lvLabelFrom = (Label)item.FindControl("uoLabelMinCapacity");
                    lvLabelTo = (Label)item.FindControl("uoLabelMaxCapacity");

                    itemCapacity = new ContractServiceTypeDuration();

                    itemCapacity.ContractSafeguardDurationIDInt = GlobalCode.Field2Int(lvHiddenFieldContractServiceDurationID.Value);
                    itemCapacity.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                    itemCapacity.ServiceTypeID = GlobalCode.Field2Int(lvHiddenFieldServiceTypeID.Value);

                    itemCapacity.ServiceType = lvLabelServiceTypeName.Text;
                    itemCapacity.From = GlobalCode.Field2Int(lvLabelFrom.Text);
                    itemCapacity.To = GlobalCode.Field2Int(lvLabelTo.Text);

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
        private void BindSafeguardTypeDropDown(int iContractID, Int16 iLoadType)
        {
            List<ServiceType> listServiceType = new List<ServiceType>();

            //List<ContractServiceTypeDuration> listServiceTypeDuration = new List<ContractServiceTypeDuration>();
            //if (Session["ContractSafeguardTypeDuration"] != null)
            //{
            //    listServiceTypeDuration = (List<ContractServiceTypeDuration>)Session["ContractSafeguardTypeDuration"];
            //}

            if (iLoadType == 0)
            {
                if (Session["ServiceType"] != null)
                {
                    listServiceType = (List<ServiceType>)Session["ServiceType"];
                }
            }
            else
            {
                SafeguardBLL.ServiceTypeGet(iContractID, GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                 false, 1);

                if (Session["ServiceType"] != null)
                {
                    listServiceType = (List<ServiceType>)Session["ServiceType"];
                }
            }

            //listServiceType.RemoveAll(a => listServiceType.Exists(b => a.ServiceTypeName == listServiceTypeDuration[]));
            //if (listServiceType.Count > 0)
            //{
            //    for (int i = 0; i < listServiceTypeDuration.Count; i++)
            //    {
            //        listServiceTypeDuration.RemoveAll(a => listServiceTypeDuration.Exists(b => a.ServiceType == listServiceType[i].ServiceTypeName));
            //    }
            //}


            listServiceType = listServiceType.OrderBy(a => a.ServiceTypeName).ToList();
            uoDropDownListServiceType.Items.Clear();
            uoDropDownListServiceType.DataSource = listServiceType;
            uoDropDownListServiceType.DataTextField = "ServiceTypeName";
            uoDropDownListServiceType.DataValueField = "ServiceTypeID";
            uoDropDownListServiceType.DataBind();
            uoDropDownListServiceType.Items.Insert(0, new ListItem("--Select Service Type--", "0"));
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Add Vehicle in ListView with capacity
        /// </summary> 
        private void ServiceTypeDurationAdd(int iServiceType, string sServiceType)
        {
            uoListViewServiceTypeDuration.DataSource = null;

            try
            {
                List<ContractServiceTypeDuration> list = new List<ContractServiceTypeDuration>();
                ContractServiceTypeDuration itemCapacity = null;

                list = GetServiceTypeDurationList();

                int ilist = list.Count();

                itemCapacity = new ContractServiceTypeDuration();
                itemCapacity.ContractSafeguardDurationIDInt = ilist + 1;
                itemCapacity.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                itemCapacity.ServiceTypeID = iServiceType;

                if (uoTextBoxMin.Text.Equals("0") && !uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("HOURLY"))
                    sServiceType = uoTextBoxMax.Text + " Hour/s";
                else if (!uoTextBoxMin.Text.Equals("0") && !uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("HOURLY"))
                    sServiceType = uoTextBoxMin.Text + " - " + uoTextBoxMax.Text + " Hour/s";
                else if (!uoTextBoxMin.Text.Equals("0") && uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("HOURLY"))
                    sServiceType = uoTextBoxMin.Text + "+ Hours";
                else if (uoTextBoxMin.Text.Equals("0") && uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("HOURLY"))
                    sServiceType = "Hourly";
                else if (uoTextBoxMin.Text.Equals("0") && !uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("DAILY"))
                    sServiceType = uoTextBoxMax.Text + " Day/s";
                else if (!uoTextBoxMin.Text.Equals("0") && !uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("DAILY"))
                    sServiceType = uoTextBoxMin.Text + " - " + uoTextBoxMax.Text + " Day/s";
                else if (!uoTextBoxMin.Text.Equals("0") && uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("DAILY"))
                    sServiceType = uoTextBoxMin.Text + "+ Days";
                else if (uoTextBoxMin.Text.Equals("0") && uoTextBoxMax.Text.Equals("0") && sServiceType.ToUpper().Equals("DAILY"))
                    sServiceType = "Daily";

                itemCapacity.ServiceType = sServiceType;
                itemCapacity.From = GlobalCode.Field2Int(uoTextBoxMin.Text);
                itemCapacity.To = GlobalCode.Field2Int(uoTextBoxMax.Text);

                list.Add(itemCapacity);
                list = list.OrderBy(a => a.ServiceType).ToList();

                Session["ContractSafeguardTypeDuration"] = list;
                BindServiceTypeDurationListView(0, 0);
                BindSafeguardTypeDropDown(0, 0);
                BindServiceTypeDuration();
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
        private void ServiceTypeDurationRemove(int iServiceType, string sServiceType)
        {
            List<ContractServiceTypeDuration> list = new List<ContractServiceTypeDuration>();
            if (Session["ContractSafeguardTypeDuration"] != null)
            {
                list = (List<ContractServiceTypeDuration>)Session["ContractSafeguardTypeDuration"];
                list.RemoveAll(a => list.Exists(b => a.ServiceType == sServiceType));
                list = list.OrderBy(a => a.ServiceType).ToList();
            }
            Session["ContractSafeguardTypeDuration"] = list;

            List<ServiceType> listServiceType = new List<ServiceType>();
            ServiceType item = new ServiceType();
            if (Session["ServiceType"] != null)
            {
                listServiceType = (List<ServiceType>)Session["ServiceType"];
            }
            //item.ServiceTypeID = iServiceType;
            //item.ServiceTypeName = sServiceType;

            //listServiceType.Add(item);

            Session["ServiceType"] = listServiceType;

            BindServiceTypeDurationListView(0, 0);
            BindSafeguardTypeDropDown(0, 0);
            BindServiceTypeDuration();
        }
        /// <summary>
        /// Date Created:   23/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Attachments
        /// </summary>        
        private void BindAttachmentListView(int iContractID, Int16 iLoadType)
        {
            List<ContractSafeguardAttachment> list = new List<ContractSafeguardAttachment>();
            list = GetAttachmentList();
            uoListViewAttachment.DataSource = list;
            uoListViewAttachment.DataBind();
        }
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Attachment List from Session or ListView
        /// </summary> 
        private List<ContractSafeguardAttachment> GetAttachmentList()
        {
            List<ContractSafeguardAttachment> list = new List<ContractSafeguardAttachment>();
            if (Session["ContractSafeguardAttachment"] != null)
            {
                list = (List<ContractSafeguardAttachment>)Session["ContractSafeguardAttachment"];
            }
            else
            {
                ContractSafeguardAttachment itemList;
                //HiddenField uoHiddenFieldID;
                HiddenField uoLabelServiceTypeName;
                foreach (ListViewItem item in uoListViewAttachment.Items)
                {
                    itemList = new ContractSafeguardAttachment();
                    //uoHiddenFieldID = (HiddenField)item.FindControl("uoHiddenFieldID");
                    uoLabelServiceTypeName = (HiddenField)item.FindControl("uoLabelServiceTypeName");

                    itemList.FileName = uoLabelServiceTypeName.Value;
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
            List<ContractSafeguardAttachment> list = new List<ContractSafeguardAttachment>();
            ContractSafeguardAttachment item = new ContractSafeguardAttachment();
            list = GetAttachmentList();

            list.RemoveAll(a => list.Exists(b => a.FileName == sFileName));
            list = list.OrderBy(a => a.FileName).ToList();

            Session["ContractSafeguardAttachment"] = list;
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
            DataTable dtServiceTypeDuration = null;
            DataTable dtAttachment = null;
            DataTable dtDetails = null;

            try
            {
                string strLogDescription;

                if (GlobalCode.Field2Int(uoHiddenFieldContractID.Value) == 0)
                {
                    strLogDescription = "Add Service contract";
                }
                else
                {
                    strLogDescription = "Ammend Service contract";
                }
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                List<ContractServiceTypeDuration> listServiceTypeDuration = GetServiceTypeDurationList();
                List<ContractSafeguardAttachment> listAttachment = GetAttachmentList();
                List<ContractServiceDetailsAmt> listDetails = GetContractServiceDetailsList();


                var lServiceTypeDuration = (from a in listServiceTypeDuration
                                            select new
                                            {
                                                SafeguardVendorID = GlobalCode.Field2Int(uoHiddenFieldVendorId.Value),
                                                ServiceTypeID = GlobalCode.Field2Int(a.ServiceTypeID),
                                                ServiceType = a.ServiceType,
                                                MinCapacity = GlobalCode.Field2Int(a.From),
                                                MaxCapacity = GlobalCode.Field2Int(a.To),
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
                                    //BranchID = a.BranchID,
                                    ServiceType = a.ServiceType,
                                    RateAmount = a.RateAmount,
                                    Tax = a.Tax,
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

                //dtVehicleTypeCapacity = getDataTable(lVehicleTypeCapacity);
                dtAttachment = getDataTable(lAttachment);
                dtDetails = getDataTable(lDetails);
                dtServiceTypeDuration = getDataTable(lServiceTypeDuration);

                BLL.SafeguardBLL.SafeguardInsertContract(0,
                    GlobalCode.Field2Int(uoHiddenFieldVendorId.Value), uoTextBoxContractTitle.Text,
                    uoTextBoxRemarks.Text, uoTextBoxContractStartDate.Text, uoTextBoxContractEndDate.Text,
                    uotextboxRCCLRep.Text, uoTextBoxRCCLDateAccepted.Text, uotextboxVendorRep.Text,
                    uoTextBoxVendorDateAccepted.Text, GlobalCode.Field2Int(uoDropDownListCurrency.SelectedValue),
                    uoHiddenFieldUserName.Value, strLogDescription, "SaveContract", Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(currentDate).ToString(), dtServiceTypeDuration,
                    dtAttachment, dtDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtServiceTypeDuration != null)
                {
                    dtServiceTypeDuration.Dispose();
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
            Response.Redirect("~/ContractManagement/SafeguardContractListView.aspx?vmId=" + Request.QueryString["vmId"] + "&cId=" + Request.QueryString["cID"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
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
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   Add contract vehicle details to session
        /// -----------------------------------------------------------
        /// Date Modified:   03/Oct/2013
        /// Created By:     Marco Abejar
        /// (description)   Modify route fields (from/to)
        /// </summary> 
        private void ContractServiceDetailsAdd(int iServiceType, string sServiceType)
        {
            uoGridViewService.DataSource = null;
            try
            {
                List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
                ContractServiceDetailsAmt item = new ContractServiceDetailsAmt();

                list = GetContractServiceDetailsList();

                item.BranchID = GlobalCode.Field2Int(uoHiddenFieldVendorId.Value);
                item.ContractID = GlobalCode.Field2Int(uoHiddenFieldContractID.Value);
                //item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListServiceTypeDuration.SelectedValue);
                //item.ContractServiceDurationID = GlobalCode.Field2Int(uoDropDownListServiceTypeDuration.SelectedItem.Value);
                item.ContractServiceDurationID = iServiceType;
                //item.ServiceType = uoDropDownListServiceTypeDuration.SelectedItem.Text;
                item.ServiceType = sServiceType;
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
                list = list.OrderBy(a => a.ServiceType).ToList();

                Session["ContractServiceDetailsAmt"] = list;
                BindContractServiceDetailsList();
                uoButtonAddService.Text = uoHiddenFieldDetail.Value == "0" ? "Add" : "Save";
            }
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
            uoGridViewService.DataSource = null;
            try
            {
                List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
                list = GetContractServiceDetailsList();

                list.RemoveAll(a => list.Exists(b => a.ContractDetailID == iContractDetailID));
                list = list.OrderBy(a => a.ServiceType).ToList();

                Session["ContractServiceDetailsAmt"] = list;
                BindContractServiceDetailsList();
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
        private List<ContractServiceDetailsAmt> GetContractServiceDetailsList()
        {
            List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
            ContractServiceDetailsAmt item = null;
            if (Session["ContractServiceDetailsAmt"] != null)
            {
                list = (List<ContractServiceDetailsAmt>)Session["ContractServiceDetailsAmt"];
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
        private void BindContractServiceDetailsList()
        {
            List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
            uoGridViewService.DataSource = null;

            if (Session["ContractServiceDetailsAmt"] != null)
            {
                list = (List<ContractServiceDetailsAmt>)Session["ContractServiceDetailsAmt"];
                uoGridViewService.DataSource = list;
            }
            else
            {
                uoGridViewService.DataSource = null;
            }
            uoGridViewService.Columns[0].Visible = true;
            uoGridViewService.Columns[1].Visible = true;
            //uoGridViewService.Columns[2].Visible = true;
            uoGridViewService.DataBind();
            uoGridViewService.Columns[0].Visible = false;
            uoGridViewService.Columns[1].Visible = false;
            //uoGridViewService.Columns[2].Visible = false;
        }
        #endregion      

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}

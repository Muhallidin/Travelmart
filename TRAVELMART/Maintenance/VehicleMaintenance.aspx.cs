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
using System.Net;
using System.Configuration;

namespace TRAVELMART.Maintenance
{
    public partial class VehicleMaintenance : System.Web.UI.Page
    {
        #region Events


        private AsyncTaskDelegate _dlgtColor;
        // Create delegate. 
        protected delegate void AsyncTaskDelegate();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldURLFrom.Value = GlobalCode.Field2String(Session["strPrevPage"]);
                
                if (Request.QueryString["vmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                VehicleVendorLogAuditTrail();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldVehicleVendorIdInt.Value = Request.QueryString["vmId"];

                //vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));                
                vendorInfoLoad(0);
                BindVehicleTypeListPlateNo(0);
                //vendorCountryLoad();


            }
        }

        /// <summary>
        /// Date Created: 27/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Save new vendor        
        /// -------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change endorMaintenanceBLL.vendorMaintenanceInsert to endorMaintenanceBLL.VehicleVendorsSave
        ///                 Add Audit Trail in the same script
        ///                 Remove the separate Audit Trail
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtPlateNo = null;

            try
            {
                string strLogDescription;
                string strFunction = "uoButtonSave_Click";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                if (GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value) == 0)
                {
                    strLogDescription = "Vehicle vendor added.";
                }
                else
                {
                    strLogDescription = "Vehicle vendor updated.";
                }

                string vendorPrimaryId = GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value).ToString();//(Session["vendorPrimaryId"].ToString() == null) ? "0" : Session["vendorPrimaryId"].ToString();
                
                dt = new DataTable();
                dt.Columns.Add("VehicleTypeID", typeof(int));

                DataRow r;
                HiddenField hiddenFieldVehicleTypeID;

                foreach (ListViewItem item in uoListViewVehicleType.Items)
                {
                    r = dt.NewRow();
                    hiddenFieldVehicleTypeID = (HiddenField)item.FindControl("uoHiddenFieldVehicleTypeID");
                    r[0] = GlobalCode.Field2Int(hiddenFieldVehicleTypeID.Value);
                    dt.Rows.Add(r);
                }

                dtPlateNo = new DataTable();

                dtPlateNo.Columns.Add("VehicleTypeID", typeof(int));
                dtPlateNo.Columns.Add("PlateNo", typeof(string));
                dtPlateNo.Columns.Add("VehicleMakeID", typeof(int));
                dtPlateNo.Columns.Add("VehicleColor", typeof(string));
                dtPlateNo.Columns.Add("VehicleColorName", typeof(string));

                DataRow rPlateNo;
                HiddenField hiddenFieldVehicleTypeIDPlateNo;
                HiddenField uoHiddenFileVehicleMakeID;

                HiddenField uoHiddenFieldColor;
                HiddenField uoHiddenFieldColorName;

                Label labelPlateNo;
                foreach (ListViewItem item in uoListViewPlateNo.Items)
                {
                    rPlateNo = dtPlateNo.NewRow();

                    hiddenFieldVehicleTypeIDPlateNo = (HiddenField)item.FindControl("uoHiddenFieldVehicleTypeID");
                    uoHiddenFileVehicleMakeID = (HiddenField)item.FindControl("uoHiddenFileVehicleMakeID");

                    uoHiddenFieldColor = (HiddenField)item.FindControl("uoHiddenFieldColor");
                    uoHiddenFieldColorName = (HiddenField)item.FindControl("uoHiddenFieldColorName");

                    labelPlateNo = (Label)item.FindControl("uoLabelPlateNo");

                    rPlateNo[0] = GlobalCode.Field2Int(hiddenFieldVehicleTypeIDPlateNo.Value);
                    rPlateNo[1] = labelPlateNo.Text;
                    rPlateNo[2] = GlobalCode.Field2Int(uoHiddenFileVehicleMakeID.Value);
                    rPlateNo[3] = GlobalCode.Field2String(uoHiddenFieldColor.Value);
                    rPlateNo[4] = GlobalCode.Field2String(uoHiddenFieldColorName.Value); 

                    dtPlateNo.Rows.Add(rPlateNo);
                }

                VendorMaintenanceBLL.VehicleVendorsSave(GlobalCode.Field2Int(vendorPrimaryId),
                    uoTextBoxVendorName.Text.Trim(), GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
                    GlobalCode.Field2Int(uoDropDownListCity.SelectedValue), uoTextBoxContactNo.Text,
                    uoTextBoxFaxNo.Text, uoTextBoxContactPerson.Text, uoTextBoxVendorAddress.Text,
                    uoTextBoxEmailCc.Text, uoTextBoxEmailTo.Text, uoTextBoxWebsite.Text, uoTextBoxVendorID.Text.Trim(),
                    uoHiddenFieldUser.Value, strLogDescription, strFunction,
                    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, dt, dtPlateNo);

                //OpenParentPage();
                AlertMessage("Save successfully.", true);
                //Response.Redirect("VehicleMaintenanceView.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
                Response.Redirect(uoHiddenFieldURLFrom.Value);
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message, true);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtPlateNo != null)
                {
                    dtPlateNo.Dispose();
                }
            }
        }

        protected void btnSavePhoto_LoadImage(object sender, EventArgs e)
        {
            try 
            {
                if (uoFileUploadFile.HasFile)
                {
                    SavePhoto();
                }
                else
                {
                    AlertMessage("No vehicle image file specified!", true);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message, true);
            }
        }

        protected void uoButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("VehicleMaintenanceView.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 1)
            {
                vendorCityLoad(1, uoDropDownListCountry.SelectedValue);
                //ChangeToUpperCase(uoDropDownListCity);
            }
        }

        protected void uoButtonFilterCity_Click(object sender, EventArgs e)
        {
            vendorCityLoad(1, uoDropDownListCountry.SelectedValue);
        }
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Delete vehicle type
        /// -------------------------------------------
        //protected void uoListViewVehicleType_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;

        //    int index = GlobalCode.Field2Int(e.CommandArgument);
        //    if (e.CommandName == "Delete")
        //    {
        //        //MaintenanceViewBLL.DeleteVehicleVendor(index, GlobalCode.Field2String(Session["UserName"]));

        //        ////Insert log audit trail (Gabriel Oquialda - 17/11/2011)
        //        //strLogDescription = "Vehicle vendor branch deleted. (flagged as inactive)";
        //        //strFunction = "uoVehicleVendorList_ItemCommand";

        //        //DateTime currentDate = CommonFunctions.GetCurrentDateTime();

        //        //BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //        //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

        //        BindVendorTypeList(1);
        //    }          
        //}
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine  gad
        /// (description)   Add Type in ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonVehicleTypeAdd_Click(object sender, EventArgs e)
        {
            List<VehicleType> listType = new List<VehicleType>();
            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();

            listType = GetVehicleTypeList(0);
            listVendor = GetVehicleVendorTypeList(0);

            listType.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue)));
            listType = listType.OrderBy(a => a.VehicleTypeName).ToList();

            VendorVehicleType item = new VendorVehicleType();


            item.VehicleType = uoDropDownListVehicleType.SelectedItem.Text;
            item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue);
            item.VehicleVendorID = GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value);

            listVendor.Add(item);
            listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

            Session["VendorVehicleType"] = listVendor;
            Session["VehicleType"] = listType;

            BindVendorTypeList(0);
            BindVehicleTypeList(0);
            BindVehicleTypeListPlateNo(0);
        }        
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine  gad
        /// (description)   Add Plate No. in ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonVehicleTypePlateAdd_Click(object sender, EventArgs e)
        {

           
            List<VehiclePlate> list = new List<VehiclePlate>();
            list = GetVehicleVendorPlateNo(0);
            
            char[] delimiterChars = { '-'};
            string[] words = uoDropDownListVehicleMaker.SelectedItem.Value.Split(delimiterChars);

            VehiclePlate item = new VehiclePlate();
            item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVendorTypePlate.SelectedValue);
            item.VehicleTypeName = uoDropDownListVendorTypePlate.SelectedItem.Text;
            
            item.VehiclePlateName = uoTextBoxPlateNo.Text.Trim();

            if (words.Length > 1) { 
                item.VehicleMakeID = GlobalCode.Field2Int(words[1]);
                item.VehicleMakeName = uoDropDownListVehicleMaker.SelectedItem.Text;
            }

            item.VehicleBrandID = GlobalCode.Field2Int(uoDropDownListVehicleBrand.SelectedValue);
            item.VehicleBrandName = uoDropDownListVehicleBrand.SelectedItem.Text;
           

            item.VehicleColor = uoHiddenFieldColorCode.Value;
            item.VehicleColorName = uoHiddenFieldColorName.Value;

            list.Add(item);
            list = list.OrderBy(a => a.VehicleTypeName).ThenBy(a => a.VehiclePlateName).ToList();

            Session["VendorVehiclePlate"] = list;
            BindVehiclePlateNo(0);
            uoTextBoxPlateNo.Text = "";

        }

        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine  gad
        /// (description)   Remove Type in ListView
        /// </summary>
        
        protected void uoListViewVehicleType_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField uoHiddenFieldVehicleTypeID = (HiddenField)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoHiddenFieldVehicleTypeID");
            Label uoLabelVehicleTypeName = (Label)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoLabelVehicleTypeName");
            //BindVendorTypeList(1);

            List<VehicleType> listType = new List<VehicleType>();
            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();

            listType = GetVehicleTypeList(0);
            listVendor = GetVehicleVendorTypeList(0);

            listVendor.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value)));
            listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

            VehicleType item = new VehicleType();
            item.VehicleTypeID = GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value);
            item.VehicleTypeName = uoLabelVehicleTypeName.Text;
            listType.Add(item);

            Session["VendorVehicleType"] = listVendor;
            Session["VehicleType"] = listType;

            BindVendorTypeList(0);
            BindVehicleTypeList(0);
        }
        
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine  Gad
        /// (description)   Remove Plate No in ListView
        /// </summary>
        protected void uoListViewPlateNo_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField uoListHiddenFieldVehicleTypeID = (HiddenField)uoListViewPlateNo.Items[e.ItemIndex].FindControl("uoHiddenFieldVehicleTypeID");
            Label uoListLabelPlateNo = (Label)uoListViewPlateNo.Items[e.ItemIndex].FindControl("uoLabelPlateNo");

            List<VehiclePlate> list = new List<VehiclePlate>();

            list = GetVehicleVendorPlateNo(0);

            list.RemoveAll(a => list.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoListHiddenFieldVehicleTypeID.Value)
                                                && a.VehiclePlateName.Trim() == uoListLabelPlateNo.Text.Trim()
                                            ));

            list = list.OrderBy(a => a.VehicleTypeName).ThenBy(a => a.VehiclePlateName).ToList();

            Session["VendorVehiclePlate"] = list;
            BindVehiclePlateNo(0);
        }
        #endregion

        #region Functions

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) format data to uppercase        
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        /// <summary>
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (decription) Loads vendor information     
        /// -----------------------------------------------
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (decription) Loads vendor information     
        /// -----------------------------------------------
        /// Date Modifed:   06/Aug/2013
        /// Created By:     Josephine Gad
        /// (decription)    Load Vendor using VendorMaintenanceBLL.VehicleVendorsGetByID
        ///                 Use Sessions instead of DataTable        
        /// </summary>
        private void vendorInfoLoad(Int16 iLoadType)
        {
            if (Request.QueryString["vmId"].ToString() != "0")
            {

                VendorMaintenanceBLL.VehicleVendorsGetByID(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value), iLoadType);
                List<VendorVehicleDetails> listVehicleDetails = new List<VendorVehicleDetails>();

                if (Session["VehicleVendorDetails"] != null)
                {
                    listVehicleDetails = (List<VendorVehicleDetails>)Session["VehicleVendorDetails"];
                    if (listVehicleDetails.Count > 0)
                    {
                        uoTextBoxCity.Text = GlobalCode.Field2String(Session["VehicleVendorCityFilter"]);
                        uoTextBoxVendorName.Text = listVehicleDetails[0].VendorName;
                        uoTextBoxVendorAddress.Text = listVehicleDetails[0].Address;
                        uoTextBoxWebsite.Text = listVehicleDetails[0].Website;

                        uoTextBoxEmailTo.Text = listVehicleDetails[0].EmailTo;
                        uoTextBoxEmailCc.Text = listVehicleDetails[0].EmailCC;

                        vendorCountryLoad();
                        if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(listVehicleDetails[0].CountryID)) != null)
                        {
                            uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(listVehicleDetails[0].CountryID);
                        }
                        vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                        if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(listVehicleDetails[0].CityID)) != null)
                        {
                            uoDropDownListCity.SelectedValue = GlobalCode.Field2String(listVehicleDetails[0].CityID);
                        }
                        uoTextBoxContactNo.Text = listVehicleDetails[0].ContactNo;
                        uoTextBoxContactPerson.Text = listVehicleDetails[0].ContactPerson;
                        uoTextBoxFaxNo.Text = listVehicleDetails[0].FaxNo;
                        
                        BindVendorTypeList(0);
                        BindVehicleTypeList(0);
                        BindVehiclePlateNo(0);
                        BindVehicleMake();
                        BindVehicleBrand();

                        uoTextBoxVendorID.Text = listVehicleDetails[0].VendorIMS_ID;


                        PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                        Page.RegisterAsyncTask(TaskPort1);

                    }
                }
                else
                {
                    uoTextBoxCity.Text = "";
                    uoTextBoxVendorName.Text = "";
                    uoTextBoxVendorAddress.Text = "";
                    uoTextBoxWebsite.Text = "";

                    uoTextBoxEmailTo.Text = "";
                    uoTextBoxEmailCc.Text = "";

                    vendorCountryLoad();
                    vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                    uoTextBoxContactNo.Text = "";
                    uoTextBoxContactPerson.Text = "";
                    uoTextBoxFaxNo.Text = "";
                    uoTextBoxVendorID.Text = "";
                    
                    uoListViewVehicleType.DataSource = null;
                    uoListViewVehicleType.DataBind();
                    BindVehicleTypeList(1);
                    BindVehiclePlateNo(1);
                }
            }
            else
            {
                uoTextBoxCity.Text = "";
                uoTextBoxVendorName.Text = "";
                uoTextBoxVendorAddress.Text = "";
                uoTextBoxWebsite.Text = "";

                uoTextBoxEmailTo.Text = "";
                uoTextBoxEmailCc.Text = "";

                vendorCountryLoad();
                vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                uoTextBoxContactNo.Text = "";
                uoTextBoxContactPerson.Text = "";
                uoTextBoxFaxNo.Text = "";
                uoTextBoxVendorID.Text = "";
                uoListViewVehicleType.DataSource = null;
                uoListViewVehicleType.DataBind();
                BindVehicleTypeList(1);
                BindVehiclePlateNo(1);
            }
        }


       public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtColor = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtColor.BeginInvoke(cb, extraData);
            return result;
        }
        public void OnEndExceptions(IAsyncResult ar)
        {
            _dlgtColor.EndInvoke(ar);
            GetColor();
        }



        public void GetColor()
        {
            List<ColorCodes> listColor = new List<ColorCodes>();
            VendorMaintenanceBLL BLL = new VendorMaintenanceBLL();
            listColor = BLL.GetColor();
            uoListViewColor.DataSource = listColor;
            uoListViewColor.DataBind();
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist         
        /// ----------------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change CityListbyCountry to CityBLL.GetCityByCountry
        ///                 Add Session, Add default value
        /// </summary>
        private void vendorCityLoad(Int16 iLoad, string vendorCountryId)
        {
            DataTable dt = null;
            try
            {
                if (iLoad == 0)
                {
                    List<CityList> listCity = new List<CityList>();

                    uoDropDownListCity.Items.Clear();
                    if (Session["CityList"] != null)
                    {
                        listCity = ((List<CityList>)Session["CityList"]);
                        uoDropDownListCity.DataSource = listCity;
                        uoDropDownListCity.DataTextField = "CityName";
                        uoDropDownListCity.DataValueField = "CityId";
                    }
                    uoDropDownListCity.Items.Insert(0, (new ListItem("--Select City--", "0")));
                    uoDropDownListCity.DataBind();
                }
                else
                {

                    //dt = CityBLL.CityListbyCountry(vendorCountryId);
                    dt = CityBLL.GetCityByCountry(vendorCountryId, uoTextBoxCity.Text.Trim(), uoTextBoxCity.Text);
                    uoDropDownListCity.Items.Clear();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select a City--", "0"));
                    if (dt.Rows.Count > 0)
                    {
                        uoDropDownListCity.DataSource = dt;
                        uoDropDownListCity.DataTextField = "colCityNameVarchar";
                        uoDropDownListCity.DataValueField = "colCityIDInt";
                        uoDropDownListCity.DataBind();

                        if (dt.Rows.Count == 1)
                        {
                            if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(dt.Rows[0]["colCityIDInt"])) != null)
                            {
                                uoDropDownListCity.SelectedValue = GlobalCode.Field2String(dt.Rows[0]["colCityIDInt"]);
                            }
                        }
                    }
                    else
                    {
                        uoDropDownListCity.DataBind();
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

        private void BindVehicleMake()
        {
            DataTable dt = null;
            try
            {

                List<VehicleMaker> vm = new List<VehicleMaker>();
              
                if (Session["VehicleMake"] != null)
                {
                    vm= (List<VehicleMaker>)Session["VehicleMake"];
                   
                }

               
                uoDropDownListVehicleMaker.Items.Clear();

                uoDropDownListVehicleMaker.DataSource = vm;
                uoDropDownListVehicleMaker.DataTextField = "VehicleMakeName";
                uoDropDownListVehicleMaker.DataValueField = "VehicleMakeBrandID";
                uoDropDownListVehicleMaker.DataBind();
                uoDropDownListVehicleMaker.Items.Insert(0, new ListItem("--Select Vehicle Make--", "0"));

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


        private void BindVehicleBrand()
        {
            DataTable dt = null;
            try
            {
                
                List<VehicleBrand> vm = new List<VehicleBrand>();
                uoDropDownListVehicleBrand.Items.Clear();
                if (Session["VehicleBrand"] != null)
                {
                    vm = (List<VehicleBrand>)Session["VehicleBrand"];
                }

                uoDropDownListVehicleBrand.DataSource = vm;
                uoDropDownListVehicleBrand.DataTextField = "VehicleBrandName";
                uoDropDownListVehicleBrand.DataValueField = "VehicleBrandID";
                uoDropDownListVehicleBrand.DataBind();
                uoDropDownListVehicleBrand.Items.Insert(0, new ListItem("--Select Vehicle Brand--", "0"));

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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist            
        /// -----------------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change Datatable to Session
        /// -----------------------------------------------------
        /// Date Modified:  06/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Add DataTable if Session is null
        /// </summary>
        private void vendorCountryLoad()
        {
            DataTable dt = null;
            try
            {
                List<CountryList> listCountry = new List<CountryList>();

                uoDropDownListCountry.Items.Clear();
                if (Session["CountryList"] != null)
                {
                    listCountry = ((List<CountryList>)Session["CountryList"]);
                    uoDropDownListCountry.DataSource = listCountry;
                    uoDropDownListCountry.DataTextField = "CountryName";
                    uoDropDownListCountry.DataValueField = "CountryId";

                }
                else
                {
                    dt = CountryBLL.CountryList();
                    if (dt.Rows.Count > 0)
                    {
                        uoDropDownListCountry.DataSource = dt;
                        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                        uoDropDownListCountry.DataValueField = "colCountryIDInt";
                    }
                }

                uoDropDownListCountry.Items.Insert(0, (new ListItem("--Select Country--", "0")));
                uoDropDownListCountry.DataBind();
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
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page
        /// -------------------------------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Gabriel Oquialda
        /// (description) Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"
        ///               to "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicle\"            
        /// </summary>
        //private void OpenParentPage()
        //{
        //    string sScript = "<script language='javascript'>";
        //    sScript += " window.parent.$(\"#ctl00_HeaderContent_uoHiddenFieldPopupVehicle\").val(\"1\"); ";
        //    sScript += " parent.$.fancybox.close(); ";
        //    sScript += "</script>";

        //    ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        //}

        /// Date Modified:  27/06/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace ', \n and \r with empty string
        /// </summary>
        private void AlertMessage(string s, bool IsScripManager)
        {
            string sScript = "<script language='JavaScript'>";

            s = s.Replace("'", "");
            s = s.Replace("\n", "");
            s = s.Replace("\r", " ");

            sScript += "alert('" + s + "');";
            sScript += "</script>";
            if (IsScripManager)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            }
        }
        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void VehicleVendorLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            if (Request.QueryString["vmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for vehicle vendor editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for vehicle vendor editor clicked.";
            }

            strFunction = "VehicleVendorLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type
        /// </summary>
        private void BindVendorTypeList(int iLoadType)
        {
            uoListViewVehicleType.DataSource = GetVehicleVendorTypeList(iLoadType);
            uoListViewVehicleType.DataBind();
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type to be added
        /// </summary>
        private void BindVehicleTypeList(int iLoadType)
        {
            List<VehicleType> list = new List<VehicleType>();
            list = GetVehicleTypeList(iLoadType);

            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();
            listVendor = GetVehicleVendorTypeList(iLoadType);

            
            for (int i = 0; i< listVendor.Count; i++)
            {
                list.RemoveAll(a => list.Exists(b => a.VehicleTypeID == listVendor[i].VehicleTypeID));                            
            }

            list = list.OrderBy(a => a.VehicleTypeName).ToList();
            Session["VehicleType"] = list;

            uoDropDownListVehicleType.Items.Clear();
            uoDropDownListVehicleType.DataSource = list;

            uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
            uoDropDownListVehicleType.DataValueField = "VehicleTypeID";

            uoDropDownListVehicleType.DataBind();
            uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));
        }

        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Vendor Vehicle Type
        /// </summary>
        private List<VendorVehicleType> GetVehicleVendorTypeList(int iLoadType)
        {
            List<VendorVehicleType> list = new List<VendorVehicleType>();
            if (iLoadType == 0)
            {
                if (Session["VendorVehicleType"] != null)
                {
                    list = (List<VendorVehicleType>)Session["VendorVehicleType"];
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsTypeGet(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value));
                list = (List<VendorVehicleType>)Session["VendorVehicleType"];
            }
            return list;
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Vehicle Type
        /// </summary>
        private List<VehicleType> GetVehicleTypeList(int iLoadType)
        {
            List<VehicleType> list = new List<VehicleType>();
            if (iLoadType == 0)
            {
                if (Session["VehicleType"] != null)
                {
                    list = (List<VehicleType>)Session["VehicleType"];
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsTypeGet(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value));
                list = (List<VehicleType>)Session["VehicleType"];
            }

            return list;
        }
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get ListView of Plate No.
        /// </summary>
        private List<VehiclePlate> GetVehicleVendorPlateNo(int iLoadType)
        {
            List<VehiclePlate> list = new List<VehiclePlate>();
            if (iLoadType == 0)
            {
                if (Session["VendorVehiclePlate"] != null)
                {
                    list = (List<VehiclePlate>)Session["VendorVehiclePlate"];
                }
            }
            else
            {
                list = VendorMaintenanceBLL.VehicleVendorsPlateNoGet(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value));
                Session["VendorVehiclePlate"] = list;
            }
            return list;
        }
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type for Plate no.
        /// </summary>
        private void BindVehicleTypeListPlateNo(int iLoadType)
        {            
            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();
            listVendor = GetVehicleVendorTypeList(iLoadType);

         
            uoDropDownListVendorTypePlate.Items.Clear();
            uoDropDownListVendorTypePlate.DataSource = listVendor;

            uoDropDownListVendorTypePlate.DataTextField = "VehicleType";
            uoDropDownListVendorTypePlate.DataValueField = "VehicleTypeID";

            uoDropDownListVendorTypePlate.DataBind();
            uoDropDownListVendorTypePlate.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));
        }


        protected string GetCellColor()
        {
            var ColorCode = Eval("VehicleColor");
          
            string result = "<td></td>";
            if (ColorCode != null )
            {
                result = "<td style=\" background-color:" + ColorCode + "\"/>";
            }
            return result  ;
        }

        protected string GetCellVehicleColor()
        {
            var ColorCode = Eval("ColorCode");
            var ColorName = Eval("ColorName");
            string result = "<td></td>";
            if (ColorCode != null)
            {
                return result = "<td runat=\"server\" id=\"" + ColorCode + "_" + ColorName + "\" style=\" background-color:" + ColorCode + "; width:25px; \"  onclick =\"GetColorCode(this)\"/>";
            }
            return result;
        }

         /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type for Plate no.
        /// </summary>
        private void BindVehiclePlateNo(int iLoadType)
        {
            uoListViewPlateNo.DataSource = GetVehicleVendorPlateNo(iLoadType);
            uoListViewPlateNo.DataBind();
        }



        //protected void UploadButton_Click(object sender, EventArgs e)
        //{
        //    if (uoFileUploadFile.HasFile)
        //    {
        //        try
        //        {
        //            string filename = Path.GetFileName(uoFileUploadFile.FileName);
        //            uoFileUploadFile.SaveAs("~/FileUploaded/Vehicle/" + filename);
        //        }
        //        catch (Exception ex)
        //        {
        //            //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        //            throw ex;
        //        }
        //    }
        //}


        /// <summary>
        /// Date Modified:  03/Jan/2018
        /// Modified By:    Muhallidin G Wali
        /// (description)   Save photo in Directory and call TM-API to save it in Panda
        /// </summary>
        /// <returns></returns>
        private bool SavePhoto()
        {
            bool bReturn = false;
            try
            {


                string sFileExtension = "";
                string sLocation = "";
                string sIDName = "";


                sFileExtension = Path.GetExtension(uoFileUploadFile.FileName);
                sLocation = Server.MapPath("~/FileUploaded/Vehicle/");
                sIDName = "VehicleID";
                
              
                sFileExtension = sFileExtension.Replace(".", "");

                string[] sImageExtension = ConfigurationManager.AppSettings["ImageExtension"].ToString().Split(",".ToCharArray());
                if (sImageExtension.Contains(sFileExtension.ToLower().Trim()))
                {
                    string sVehicleID = uoHiddenFieldVehicleDetailID.Value;

                    DirectoryInfo dir = new DirectoryInfo(sLocation);
                    FileInfo[] files = dir.GetFiles(sVehicleID + ".*");
                    if (files.Length > 0)
                    {
                        //File exists
                        foreach (FileInfo file in files)
                        {
                            file.Delete();
                        }
                    }
                    //else
                    //{
                    //    //File does not exist
                    //}

                    //save photo in directory
                    sLocation += sVehicleID + "." + sFileExtension.ToLower();
                    uoFileUploadFile.SaveAs(sLocation);

                    bReturn = WSConnection.SaveToMediaServer(sIDName, sVehicleID, sFileExtension.ToLower(), "vehicle");
                    bReturn = true;

                }
                else
                {
                    AlertMessage("Invalid extension file", true);
                }
                return bReturn;
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message, true);
                return bReturn;
            }
        }




        //public string SendInvoiceXML(string Method, string Uri, string Body, string authentication, string InvoiceNumber)
        //{
        //    try
        //    {
        //        var content = "";

        //        string _CredentialBase64 = authentication;
        //        const string _ContentType = "application/json";

        //        var request = new  HttpRequestMessage();
        //        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
        //        request.Headers.Add("Authorization", String.Format("Bearer {0}", _CredentialBase64));
        //        request.Headers.Add("Cache-Control", "no-cache");
        //        request.Method = HttpMethod.Post;
        //        request.RequestUri = new Uri(Uri);

        //        request.Content = new StringContent(Body, UTF8Encoding.UTF8, "Application/xml"); 

        //        var httpClient = new HttpClient();
        //        var response = httpClient.SendAsync(request).Result;

        //        // handle result code
        //        content = response.Content.ReadAsStringAsync().Result;

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            G.LogError(content, "/Logs/exception");
        //        }
        //        else
        //        {
        //            SubmittedInvoices(InvoiceNumber);
        //            //G.LogError(content, "/Logs/success");
        //        }


        //        return content;

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}



        #endregion             
    }
}

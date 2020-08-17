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

namespace TRAVELMART.Maintenance
{
    public partial class VehicleMaintenanceBranch : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            SFStatus.Visible = false;
            if (!IsPostBack)
            {
                uoHiddenFieldBranchId.Value = Request.QueryString["vmId"];

                VehicleVendorBranchLogAuditTrail();

                vehicleGetCompany();
                countryList();
                cityListByCountry(Convert.ToInt32(uoDropDownListCountry.SelectedValue));                
                //vehicleGetType();
                
                textChangeToUpperCase(uoDropDownListVendorName);                
                textChangeToUpperCase(uoDropDownListCountry);
                textChangeToUpperCase(uoDropDownListCity);                
                //textChangeToUpperCase(uoDropDownListVehicleType);                
                          
                //ViewState["Add"] = "False";
                uoHyperLinkVehicleAdd.HRef = "~/Maintenance/VehicleTypeBranch.aspx?vID=0" + "&vmID=" + uoHiddenFieldBranchId.Value;
                
                getVendorType();
                if (Request.QueryString["vmId"] == "0")
                {                    
                    vehicleVendorBranchInfoLoad(Request.QueryString["vmId"]);
                }
                else
                {
                    uoPanel1.Visible = true;
                    vehicleVendorBranchInfoLoad(Request.QueryString["vmId"]);
                    vehicleTypeBranchInfoLoad(Request.QueryString["vmId"]);
                    
                }
            }
        }
        
        private void getVendorType()
        {
            try
            {
                DataTable dr;
                dr = VendorMaintenanceBLL.getVendorType(0);
                uoRadioButtonListType.DataSource = dr;
                uoRadioButtonListType.DataTextField = "colVendorTypeNameVarchar";
                uoRadioButtonListType.DataValueField = "colVendorTypeIdInt";
                uoRadioButtonListType.DataBind();
            }
            catch 
            {
                throw;
            }
        }

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 1)
            {
                cityListByCountry(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                textChangeToUpperCase(uoDropDownListCity);
            }            
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void uoButtonViewCity_Click(object sender, EventArgs e)
        {
            cityListByCountry(int.Parse(uoDropDownListCountry.SelectedValue));
        }

        /// <summary>
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert/Update vendor vehicle branch        
        /// </summary>
        protected void uoButtonVehicleBranchSave_Click(object sender, EventArgs e)
        {

            string vendorId = uoDropDownListVendorName.SelectedValue;
            string country = uoDropDownListCountry.SelectedValue;
            string city = uoDropDownListCity.SelectedValue;

            string strLogDescription;
            string strFunction;

            bool isValidTest = false;
            Validate("Header");
            isValidTest = IsValid;

            if (!isValidTest)
            {
                return;
            }

            VendorMaintenanceBLL.vehicleInsertUpdateVendorBranch(uoTextBoxVendorBranchName.Text, uoTextBoxBranchCode.Text, uoTextBoxVendorAddress.Text, Convert.ToInt32(city), Convert.ToInt32(country),
                                 uoTextBoxContactNo.Text, GlobalCode.Field2String(Session["UserName"]), Convert.ToInt32(vendorId), uoTextBoxContactPerson.Text,
                                 uoCheckBoxFranchise.Checked, uoHiddenFieldBranchId.Value, GlobalCode.Field2TinyInt(uoRadioButtonListType.SelectedValue));

            if (Request.QueryString["vmId"] == "0" || Request.QueryString["vmId"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle vendor branch added.";
                strFunction = "uoButtonVehicleBranchSave_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle vendor branch updated.";
                strFunction = "uoButtonVehicleBranchSave_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            

            //if (Request.QueryString["vmId"] == "0")
            //{
                Response.Redirect("~/Maintenance/VehicleMaintenanceBranchView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"]);
            //}            
        }        

        protected void uoListViewVehiclePager_PreRender(object sender, EventArgs e)
        {
            //vehicleVendorBranchInfoLoad(Request.QueryString["vmId"]);
            vehicleTypeBranchInfoLoad(Request.QueryString["vmId"]);
        }

        protected void uoListViewVehicle_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeleteVehicleTypeBranch(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle vendor branch vehicle type deleted. (flagged as inactive)";
                strFunction = "uoListViewVehicle_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                vehicleTypeBranchInfoLoad(Request.QueryString["vmId"]);                
            }
        }

        protected void uoListViewVehicle_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }

        ///// <summary>
        ///// Date Created: 06/09/2011
        ///// Created By: Gabriel Oquialda
        ///// (description) Insert/Update vehicle type branch        
        ///// </summary>
        //protected void uoButtonVehicleTypeBranchSave_Click(object sender, EventArgs e)
        //{
        //    bool isValidTest = false;                       
        //    Validate("Vehicle");
        //    isValidTest &= IsValid;

        //    if (!isValidTest)
        //    {
        //        return;
        //    }

        //    string branchID = Request.QueryString["vmId"];
        //    vehicleSaveGridItems(Convert.ToInt32(branchID));

        //    Response.Redirect("~/Maintenance/VehicleMaintenanceBranchView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"]);
        //}

        //protected void uoButtonAddVehicle_Click(object sender, EventArgs e)
        //{
        //    createVehicleDataTable();
        //    Clear();
        //}

        //protected void uoGridViewVehicle_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    dt = (DataTable)ViewState["Table"];
           
        //    GridViewRow row = uoGridViewVehicle.SelectedRow;
        //    VehicleBLL.RemoveVehicleType(uoHiddenFieldVendorId.Value, row.Cells[0].Text, GlobalCode.Field2String(Session["UserName"]));

        //    dt.Rows.RemoveAt(this.uoGridViewVehicle.SelectedIndex);
        //    uoGridViewVehicle.DataSource = dt;
        //    uoGridViewVehicle.DataBind();
        //}

        //protected void uoGridViewVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    uoGridViewVehicle.PageIndex = e.NewPageIndex;
        //    vehicleVendorBranchInfoLoad(Request.QueryString["vmId"]);
        //}

        //protected void uoButtonBranchVehicleSave_Click(object sender, EventArgs e)
        //{
        //    vehicleSaveGridItems();
        //}        
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 06/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load vendor name to dropdownlist            
        /// </summary> 
        private void vehicleGetCompany()
        {
            DataTable dt = null;
            try
            {
                uoDropDownListVendorName.Items.Clear();
                dt = VehicleBLL.vehicleGetCompany();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendorName.DataSource = dt;
                    uoDropDownListVendorName.DataTextField = "colVendorNameVarchar";
                    uoDropDownListVendorName.DataValueField = "colVendorIdInt";
                    uoDropDownListVendorName.DataBind();
                }
                else
                {
                    uoDropDownListVendorName.DataBind();
                }

                uoDropDownListVendorName.Items.Insert(0, (new ListItem("--Select Vendor--", "0")));
                if (dt.Rows.Count == 1)
                {
                    uoDropDownListVendorName.SelectedIndex = 1;
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
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Select country list by vendor id   
        /// </summary>
        private void countryList()
        {
            DataTable dt = new DataTable();
            dt = CountryBLL.CountryList();
            if (dt.Rows.Count > 0)
            {
                uoDropDownListCountry.DataSource = dt;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("--Select a Country--", "0"));
            }
            else
            {
                uoDropDownListCountry.DataBind();
            }

            //DataTable dt = null;
            //try
            //{
            //    uoDropDownListCountry.Items.Clear();
            //    dt = VendorMaintenanceBLL.countryList();
            //    if (dt.Rows.Count > 0)
            //    {
            //        uoDropDownListCountry.DataSource = dt;
            //        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
            //        uoDropDownListCountry.DataValueField = "colCountryIDInt";
            //        uoDropDownListCountry.DataBind();
            //    }
            //    else
            //    {
            //        uoDropDownListCountry.DataBind();
            //    }

            //    uoDropDownListCountry.Items.Insert(0, (new ListItem("--Select Country--", "0")));
            //    if (dt.Rows.Count == 1)
            //    {
            //        uoDropDownListCountry.SelectedIndex = 1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }

        /// <summary>
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Select city list by vendor and country id   
        /// </summary>
        private void cityListByCountry(int vendorCountryId)
        {
            DataTable dt = null;
            try
            {
                dt = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxFilterCity.Text.Trim(), "0");
                ListItem item = new ListItem("--SELECT CITY--", "0");
                uoDropDownListCity.Items.Clear();
                uoDropDownListCity.Items.Add(item);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataTextField = "colCityNameVarchar";
                    uoDropDownListCity.DataValueField = "colCityIDInt";
                    uoDropDownListCity.DataSource = dt;
                }
                uoDropDownListCity.DataBind();

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListCity.SelectedIndex = 1;
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

            //DataTable dt = null;
            //try
            //{
            //    int countryID = 0;
            //    if (uoDropDownListCountry.SelectedValue != "0")
            //    {
            //        countryID = Convert.ToInt32(uoDropDownListCountry.SelectedValue);
            //    }               

            //    uoDropDownListCity.Items.Clear();
            //    dt = VendorMaintenanceBLL.cityListByCountry(countryID);
            //    if (dt.Rows.Count > 0)
            //    {
            //        uoDropDownListCity.DataSource = dt;
            //        uoDropDownListCity.DataTextField = "colCityNameVarchar";
            //        uoDropDownListCity.DataValueField = "colCityIDInt";
            //        uoDropDownListCity.DataBind();
            //    }
            //    else
            //    {
            //        uoDropDownListCity.DataBind();
            //    }

            //    uoDropDownListCity.Items.Insert(0, (new ListItem("--Select City--", "0")));
            //    if (dt.Rows.Count == 1)
            //    {
            //        uoDropDownListCity.SelectedIndex = 1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }

        ///// <summary> 
        ///// Date Created:   24/08/2011
        ///// Created By:     Gabriel Oquialda
        ///// (description)   Load vehicle type to dropdownlist
        ///// ----------------------------------------------------
        ///// Date Modified:  06/09/2011
        ///// Modified By:    Josephine Gad
        ///// (description)   Change vehicleGetType to vehicleGetTypeBrandMake          
        ///// </summary>
        //private void vehicleGetType()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = VendorMaintenanceBLL.vehicleGetTypeList();
        //        if (dt.Rows.Count > 0)
        //        {
        //            uoDropDownListVehicleType.DataSource = dt;
        //            uoDropDownListVehicleType.DataTextField = "vehicleType";
        //            uoDropDownListVehicleType.DataValueField = "vehicleTypeID";
        //            uoDropDownListVehicleType.DataBind();
        //        }
        //        else
        //        {
        //            uoDropDownListVehicleType.DataBind();
        //        }

        //        uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));

        //        if (dt.Rows.Count == 1)
        //        {
        //            uoDropDownListVehicleType.SelectedIndex = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Date Created:   08/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Loads vendor branch information            
        /// ---------------------------------------------------
        /// Date Modified:  27/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to IDataReader
        /// </summary>
        private void vehicleVendorBranchInfoLoad(String branchId)
        {
            IDataReader dr = null;
            try
            {
                if (branchId != "0" && branchId != null)
                {
                    dr = VendorMaintenanceBLL.vehicleVendorBranchMaintenanceInformation(Int32.Parse(branchId));
                    if (dr.Read())
                    {
                        uoDropDownListVendorName.Enabled = false;
                        uoDropDownListVendorName.SelectedValue = dr["colVendorIdInt"].ToString();
                        uoTextBoxVendorBranchName.Text = dr["colVendorBranchNameVarchar"].ToString();

                        uoTextBoxVendorAddress.Text = dr["colAddressVarchar"].ToString();

                        uoDropDownListCountry.SelectedValue = dr["colCountryIDInt"].ToString();
                        uoTextBoxFilterCity.Text = dr["colCityNameVarchar"].ToString();
                        if (uoDropDownListCountry.SelectedIndex > 1)
                            cityListByCountry(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                        uoDropDownListCity.SelectedValue = dr["colCityIDInt"].ToString();
                        uoTextBoxContactNo.Text = dr["colContactNoVarchar"].ToString();
                        uoTextBoxContactPerson.Text = dr["colContactPersonVarchar"].ToString();
                        uoCheckBoxFranchise.Checked = Convert.ToBoolean(dr["colFranchiseBit"].ToString());

                        uoRadioButtonListType.SelectedValue = dr["colVehicleTypeTinyInt"].ToString();

                        textChangeToUpperCase(uoDropDownListCountry);
                        textChangeToUpperCase(uoDropDownListCity);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 07/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor type branch information            
        /// </summary>
        private void vehicleTypeBranchInfoLoad(String branchId)
        {
            if (branchId != "0" && branchId != null)
            {
                uoListViewVehicle.DataSource = VendorMaintenanceBLL.GetVehicleTypeBranchList(Int32.Parse(branchId));
                uoListViewVehicle.DataBind();
            }
        }

        //private void createVehicleDataTable()
        //{
        //    if (ViewState["Add"].ToString() == "True")
        //    {
        //        DataTable dt = new DataTable();
        //        dt = (DataTable)ViewState["Table"];
        //        int i_TotalRows = dt.Rows.Count;
        //        DataRow dr;
        //        dr = dt.NewRow();

        //        dt.Rows.Add(this.createDataRow_GridViewDataSource(dr, Convert.ToInt32(""), Convert.ToInt32(uoDropDownListVehicleType.SelectedValue), uoDropDownListVehicleType.SelectedItem.Text,
        //            uoTextBoxVehicleTypeName.Text, Convert.ToInt32(uoTextBoxVehicleCapacity.Text)));
        //        uoGridViewVehicle.DataSource = dt;
        //        uoGridViewVehicle.DataBind();
        //        ViewState["Table"] = dt;
        //    }
        //    else
        //    {
        //        createDataTable_GridViewDataSource(Convert.ToInt32(""), Convert.ToInt32(uoDropDownListVehicleType.SelectedValue), uoDropDownListVehicleType.SelectedItem.Text,
        //            uoTextBoxVehicleTypeName.Text, Convert.ToInt32(uoTextBoxVehicleCapacity.Text));
        //        ViewState["Add"] = true;
        //        uoGridViewVehicle.Enabled = true;
        //        uoGridViewVehicle.DataBind();
        //    }
        //}

        //void createDataTable_GridViewDataSource(Int32 vehicleID, Int32 vehicleTypeID, string Type, string Name, Int32 Capacity)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("TypeID");
        //    dt.Columns.Add("Type");
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("Capacity");

        //    DataRow dr;
        //    dr = dt.NewRow();
        //    dt.Rows.Add(this.createDataRow_GridViewDataSource(dr, vehicleID, vehicleTypeID, Type, Name, Capacity));

        //    ViewState["Table"] = dt;
        //    this.uoGridViewVehicle.DataSource = dt;
        //}

        //DataRow createDataRow_GridViewDataSource(DataRow dr, Int32 vehicleID, Int32 vehicleTypeID, string Type, string Name, Int32 Capacity)
        //{
        //    dr["ID"] = vehicleID;
        //    dr["TypeID"] = vehicleTypeID;
        //    dr["Type"] = Type;
        //    dr["Name"] = Name;
        //    dr["Capacity"] = Capacity;

        //    return dr;
        //}

        //private void vehicleSaveGridItems(Int32 branchID)
        //{
        //    DataTable dt = new DataTable(); ;
        //    dt = (DataTable)ViewState["Table"];
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < uoGridViewVehicle.Rows.Count; i++) //Save the values one by one
        //            {
        //                string vehicleID = dt.Rows[i]["ID"].ToString();
        //                string vehicleTypeID = dt.Rows[i]["TypeID"].ToString();
        //                string Name = dt.Rows[i]["Name"].ToString();
        //                string Capacity = dt.Rows[i]["Capacity"].ToString();

        //                VendorMaintenanceBLL.vehicleInsertUpdateBranch(branchID, Convert.ToInt32(vehicleID), Convert.ToInt32(vehicleTypeID), Name, Convert.ToInt32(Capacity), GlobalCode.Field2String(Session["UserName"]));
        //            }
        //        }                
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Maintenance/VehicleMaintenanceBranchView.aspx?st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"]);
        //    }
        //}

        ///// <summary>
        ///// Date Created: 06/09/2011
        ///// Created By: Gabriel Oquialda
        ///// (description) Close this page and update parent page            
        ///// </summary>
        //private void parentPageRefresh()
        //{            
        //    string sScript = "<script language='javascript'>";
        //    sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicleContract\").val(\"1\"); ";
        //    sScript += " parent.$.fancybox.close(); ";
        //    sScript += "</script>";

        //    ScriptManager.RegisterClientScriptBlock(uoButtonVendorBranchSave, this.GetType(), "scr", sScript, false);
        //}

        private void textChangeToUpperCase(DropDownList ddl)
        {       

            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }        

        //private void Clear()
        //{
        //    uoDropDownListVehicleType.SelectedIndex = 0;            
        //}

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void VehicleVendorBranchLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"] != "0")
            {
                strLogDescription = "Edit linkbutton for vehicle vendor branch editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for vehicle vendor branch editor clicked.";
            }

            strFunction = "VehicleVendorBranchLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion               
    }
}

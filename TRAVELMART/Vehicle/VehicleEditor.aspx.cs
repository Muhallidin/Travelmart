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

namespace TRAVELMART
{
    public partial class VehicleEditor : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            } 
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            {
                string sCountry = GlobalCode.Field2String(Session["UserCountry"]);
                if (sCountry == "0" || sCountry == "")
                {
                    GetBranchInfo();
                }
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["sfId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                if (Session["UserRole"]== null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    GetBranchInfo();
                }
                Session["strSFCode"] = Request.QueryString["sfId"].Trim();                
                Session["strTravelLocatorID"] = Request.QueryString["ID"];
                Session["strSFSeqNo"] = Request.QueryString["SN"];

                uoHiddenFieldStatus.Value = Request.QueryString["st"];
                uoHiddenFieldRecordLocator.Value = Request.QueryString["recloc"].ToString();
                uoHiddenFieldTravelRequestID.Value = Request.QueryString["trID"];
                uoHiddenFieldManualRequestID.Value = Request.QueryString["manualReqID"]; 

                if (Request.QueryString["vID"] != null)
                {
                    uoHiddenFieldVehicleBookingID.Value = Request.QueryString["vID"];
                }
                else
                {
                    uoHiddenFieldVehicleBookingID.Value = "0";
                }
                if (Request.QueryString["PendingID"] != null)
                {
                    uoHiddenFieldPendingId.Value = Request.QueryString["PendingID"].ToString();
                }
                else
                {
                    uoHiddenFieldPendingId.Value = "0";
                }
                seafarerGetName();
                vehicleGetPortDetails();
                vehicleGetCompany();
                countryListByVendorID();
                cityListByVendorCountryID();
                vehicleGetBranch();
                //vehicleGetBrand();
                //vehicleGetMake();
                vehicleGetType();

                //ChangeToUpperCase(uoDropDownListVehicleCompany);
                //ChangeToUpperCase(uoDropDownListCategory);
                //ChangeToUpperCase(uoDropDownListCountry);
                //ChangeToUpperCase(uoDropDownListCity);
                //ChangeToUpperCase(uoDropDownListVehicleBranch);
                
                //ChangeToUpperCase(uoDropDownListVehicleType);
                
                if (Request.QueryString["ID"] != null && Request.QueryString["SN"] != null)
                {
                    seafarerGetInfo(Convert.ToInt32(Request.QueryString["ID"]), Convert.ToInt32(Request.QueryString["SN"]));
                }
                EnableDisableControl();
            }
        }        
       
        protected void uoDropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            vehicleGetCompany();
            countryListByVendorID();
            cityListByVendorCountryID();
            vehicleGetBranch();
            vehicleGetType();

            //ChangeToUpperCase(uoDropDownListVehicleCompany);
            //ChangeToUpperCase(uoDropDownListCountry);
            //ChangeToUpperCase(uoDropDownListCity);
            //ChangeToUpperCase(uoDropDownListVehicleBranch);
            //ChangeToUpperCase(uoDropDownListVehicleType);
        }
        protected void uoDropDownListVehicleCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            countryListByVendorID();
            cityListByVendorCountryID();
            vehicleGetBranch();
            vehicleGetType();

            //ChangeToUpperCase(uoDropDownListCountry);
            //ChangeToUpperCase(uoDropDownListCity);
            //ChangeToUpperCase(uoDropDownListVehicleBranch);
            //ChangeToUpperCase(uoDropDownListVehicleType);

            string scriptString = "return OpenContract('" + uoDropDownListVehicleBranch.SelectedValue + "');";
            uoLinkButtonContract.Attributes.Add("OnClick", scriptString);
        }
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            cityListByVendorCountryID();
            vehicleGetBranch();
            vehicleGetType();

            //ChangeToUpperCase(uoDropDownListCity);
            //ChangeToUpperCase(uoDropDownListVehicleBranch);
            //ChangeToUpperCase(uoDropDownListVehicleType);
        }
        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            vehicleGetBranch();
            vehicleGetType();

            //ChangeToUpperCase(uoDropDownListVehicleBranch);
            //ChangeToUpperCase(uoDropDownListVehicleType);
        }
        protected void uoDropDownListVehicleBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //vehicleGetBrand();
            //vehicleGetMake();
            vehicleGetType();            
            //ChangeToUpperCase(uoDropDownListVehicleType);
            string scriptString = "return OpenContract('" + uoDropDownListVehicleBranch.SelectedValue + "');";
            uoLinkButtonContract.Attributes.Add("OnClick", scriptString);
        }

        //protected void uoDropDownListVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    vehicleGetBrand();
        //    vehicleGetMake();            
        //    ChangeToUpperCase(uoDropDownListVehicleBrand);
        //    ChangeToUpperCase(uoDropDownListVehicleMake);
        //}

        //protected void uoDropDownListVehicleBrand_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    vehicleGetMake();
        //    ChangeToUpperCase(uoDropDownListVehicleMake);
        //}

        //protected void uoDropDownListVehicleMake_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    vehicleGetType();
        //    ChangeToUpperCase(uoDropDownListVehicleType);
        //}       

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Show/Hide bill to crew checkbox for cancelled vehicle status            
        /// </summary>
        protected void uoDropDownListVehicleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {          
            if (uoDropDownListVehicleStatus.SelectedValue == "Cancelled")
            {
                uoCheckBoxBilledToCrew.Visible = true;
            }
            else
            {
                uoCheckBoxBilledToCrew.Checked = false;
                uoCheckBoxBilledToCrew.Visible = false;
            }
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Save vehicle transaction     
        /// -----------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change vehicleInsertTransaction to vehicleInsertTransactionOther
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {           
            try
            {
                string vendorId = uoDropDownListVehicleCompany.SelectedValue;
                string category = uoDropDownListCategory.SelectedValue;
                string countryId = uoDropDownListCountry.SelectedValue;
                string cityId = uoDropDownListCity.SelectedValue;
                string branchId = uoDropDownListVehicleBranch.SelectedValue;
                //string vehicleBrandId = uoDropDownListVehicleBrand.SelectedValue;
                //string vehicleMakeId = uoDropDownListVehicleMake.SelectedValue;
                string vehicleTypeId = uoDropDownListVehicleType.SelectedValue;
                string seafarerStatus = uoHiddenFieldStatus.Value;
                //string travelLocatorId = GlobalCode.Field2String(Session["strTravelLocatorID"]);
                DateTime currentDate = DateTime.Now;
                string currentDateString = currentDate.ToString(TravelMartVariable.DateTimeFormat);

                string strLogDescription;
                string strFunction;

                if (uoHiddenFieldPendingId.Value == "0")
                {
                    if (Request.QueryString["SN"] == null)
                    {
                        if (pickupdatetimeInInsertExist() == false)
                        {
                            if (Convert.ToDateTime(uoTextBoxPickUpDate.Text) <= Convert.ToDateTime(uoTextBoxDropOffDate.Text))
                            {                               
                                //VehicleBLL.vehicleInsertTransactionOther(uoHiddenFieldTravelRequestID.Value,
                                //    uoHiddenFieldRecordLocator.Value, uoHiddenFieldManualRequestID.Value,
                                //    vendorId, countryId, cityId, branchId, vehicleTypeId, "", uoTextBoxPickUpDate.Text,
                                //    uoTextBoxPickupTime.Text, uoTextBoxDropOffDate.Text, uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                                //    uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text, Session["UserName"].ToString(), seafarerStatus,
                                //    uoTextBoxRemarks.Text, uoCheckBoxBilledToCrew.Checked);
                                VehicleBLL.vehicleInsertTransactionPending("", "", "", uoHiddenFieldTravelRequestID.Value,
                                    "", uoHiddenFieldManualRequestID.Value,
                                    vendorId, countryId, cityId, branchId, vehicleTypeId, "", uoTextBoxPickUpDate.Text,
                                    uoTextBoxPickupTime.Text, uoTextBoxDropOffDate.Text, uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                                    uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text, GlobalCode.Field2String(Session["UserName"]), currentDateString,"","",
                                    seafarerStatus, uoTextBoxRemarks.Text, uoCheckBoxBilledToCrew.Checked, "Add");

                                //OpenParentPage();

                                string sMsg = uoDropDownListVehicleBranch.SelectedItem.Text + " has been added and pending for approval.";
                                sMsg += "<br/>Seafarer: " + uoTextBoxSeafarer.Text;
                                sMsg += "<br/>E1 ID: " + Request.QueryString["sfId"].ToString();                                
                                SendEmail("Travelmart: New Vehicle booking added", sMsg);

                                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                                strLogDescription = "Vehicle booking added.";
                                strFunction = "uoButtonSave_Click";

                                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                                BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                                OpenParentPagePending();
                            }
                            else if (Convert.ToDateTime(uoTextBoxPickUpDate.Text) > Convert.ToDateTime(uoTextBoxDropOffDate.Text))
                            {
                                //string sScript = "<script language=JavaScript>";
                                //sScript += "alert('Drop-off date has already passed.');";
                                //sScript += "</script>";
                                //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "Prompt", sScript);

                                AlertMessage("Drop-off date is already passed.");
                            }
                        }
                        else if (pickupdatetimeInInsertExist() == true)
                        {
                            //string sScript = "<script language=JavaScript>";
                            //sScript += "alert('Please select another pick-up date and time.');";
                            //sScript += "</script>";
                            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "Prompt", sScript);

                            AlertMessage("Please select another pick-up date and time.");
                        }
                    }
                    else
                    {
                        if (pickupdatetimeInUpdateExist() == false)
                        {
                            if (uoHiddenFieldVehicleBookingID.Value == "0")
                            {
                                if (Convert.ToDateTime(uoTextBoxPickUpDate.Text) <= Convert.ToDateTime(uoTextBoxDropOffDate.Text))
                                {
                                    //VehicleBLL.vehicleUpdateTransaction(Int32.Parse(Request.QueryString["ID"].ToString()),
                                    //    Convert.ToInt32(Request.QueryString["SN"]), Convert.ToInt32(vendorId), //Convert.ToInt32(category),
                                    //    Convert.ToInt32(countryId), Convert.ToInt32(cityId), Convert.ToInt32(branchId), //Convert.ToInt32(vehicleBrandId),
                                    //    //Convert.ToInt32(vehicleMakeId), Convert.ToInt32(vehicleTypeId), //uoTextBoxYear.Text, uoTextBoxPlateNo.Text,
                                    //    Convert.ToInt32(vehicleTypeId),
                                    //    Convert.ToDateTime(uoTextBoxPickUpDate.Text), uoTextBoxPickupTime.Text,
                                    //    Convert.ToDateTime(uoTextBoxDropOffDate.Text), uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                                    //    uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text, Session["UserName"].ToString(), seafarerStatus,
                                    //    uoTextBoxRemarks.Text, Convert.ToBoolean(uoCheckBoxBilledToCrew.Checked));
                                    VehicleBLL.vehicleInsertTransactionPending("", Request.QueryString["ID"].ToString(), Request.QueryString["SN"].ToString(),
                                        uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value, uoHiddenFieldManualRequestID.Value,
                                    vendorId, countryId, cityId, branchId, vehicleTypeId, "", uoTextBoxPickUpDate.Text,
                                    uoTextBoxPickupTime.Text, uoTextBoxDropOffDate.Text, uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                                    uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text, 
                                    uoHiddenFieldCreatedBy.Value, uoHiddenFieldCreatedDate.Value, GlobalCode.Field2String(Session["UserName"]), currentDateString,
                                    seafarerStatus, uoTextBoxRemarks.Text, uoCheckBoxBilledToCrew.Checked, "Edit");

                                    //OpenParentPage();
                                    string sMsg = uoDropDownListVehicleBranch.SelectedItem.Text + " has been updated and pending for approval.";
                                    sMsg += "<br/>Seafarer: " + uoTextBoxSeafarer.Text;
                                    sMsg += "<br/>E1 ID: " + Request.QueryString["sfId"].ToString();                                    
                                    SendEmail("Travelmart: Vehicle booking updated", sMsg);

                                    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                                    strLogDescription = "Vehicle booking updated.";
                                    strFunction = "uoButtonSave_Click";

                                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                                    OpenParentPagePending();
                                }
                                else if (Convert.ToDateTime(uoTextBoxPickUpDate.Text) > Convert.ToDateTime(uoTextBoxDropOffDate.Text))
                                {
                                    //string sScript = "<script language=JavaScript>";
                                    //sScript += "alert('Drop-off date has already passed.');";
                                    //sScript += "</script>";
                                    //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "Prompt", sScript);

                                    AlertMessage("Drop-off date is already passed.");
                                }
                            }
                            else
                            {
                                if (Convert.ToDateTime(uoTextBoxPickUpDate.Text) <= Convert.ToDateTime(uoTextBoxDropOffDate.Text))
                                {
                                    //VehicleBLL.vehicleUpdateTransactionOther(Int32.Parse(uoHiddenFieldVehicleBookingID.Value),
                                    //Convert.ToInt32(vendorId), 
                                    //Convert.ToInt32(countryId), Convert.ToInt32(cityId), Convert.ToInt32(branchId),                                 
                                    //Convert.ToInt32(vehicleTypeId),
                                    //Convert.ToDateTime(uoTextBoxPickUpDate.Text), uoTextBoxPickupTime.Text,
                                    //Convert.ToDateTime(uoTextBoxDropOffDate.Text), uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                                    //uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text, Session["UserName"].ToString(), seafarerStatus,
                                    //uoTextBoxRemarks.Text, Convert.ToBoolean(uoCheckBoxBilledToCrew.Checked));
                                    VehicleBLL.vehicleInsertTransactionPending(uoHiddenFieldVehicleBookingID.Value, "", Request.QueryString["SN"].ToString(), uoHiddenFieldTravelRequestID.Value,
                                        "", uoHiddenFieldManualRequestID.Value, vendorId, countryId, cityId, branchId, vehicleTypeId, "", uoTextBoxPickUpDate.Text,
                                    uoTextBoxPickupTime.Text, uoTextBoxDropOffDate.Text, uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                                    uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text,
                                    uoHiddenFieldCreatedBy.Value, uoHiddenFieldCreatedDate.Value, GlobalCode.Field2String(Session["UserName"]), currentDateString,
                                    seafarerStatus, uoTextBoxRemarks.Text, Convert.ToBoolean(uoCheckBoxBilledToCrew.Checked), "Edit");

                                    string sMsg = uoDropDownListVehicleBranch.SelectedItem.Text + " has been updated and pending for approval.";
                                    sMsg += "<br/>Seafarer: " + uoTextBoxSeafarer.Text;
                                    sMsg += "<br/>E1 ID: " + Request.QueryString["sfId"].ToString();                                   
                                    SendEmail("Travelmart: Vehicle booking updated", sMsg);

                                    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                                    strLogDescription = "Vehicle booking updated.";
                                    strFunction = "uoButtonSave_Click";

                                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                                    OpenParentPagePending();
                                }
                                else if (Convert.ToDateTime(uoTextBoxPickUpDate.Text) > Convert.ToDateTime(uoTextBoxDropOffDate.Text))
                                {
                                    //string sScript = "<script language=JavaScript>";
                                    //sScript += "alert('Drop-off date has already passed.');";
                                    //sScript += "</script>";
                                    //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "Prompt", sScript);

                                    AlertMessage("Drop-off date is already passed.");
                                }
                            }
                        }
                        else if (pickupdatetimeInUpdateExist() == true)
                        {
                            //string sScript = "<script language=JavaScript>";
                            //sScript += "alert('Please select another pick-up date and time.');";
                            //sScript += "</script>";
                            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "Prompt", sScript);

                            AlertMessage("Please select another pick-up date and time.");
                        }
                    }
                }
                else
                { 
                    //edit pending vehicle booking
                    VehicleBLL.vehicleUpdateTransactionPending(uoHiddenFieldPendingId.Value, uoHiddenFieldVehicleBookingID.Value,
                        Request.QueryString["ID"].ToString(), Request.QueryString["SN"].ToString(),
                        uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value, uoHiddenFieldManualRequestID.Value,
                        vendorId, countryId, cityId, branchId, vehicleTypeId, "", uoTextBoxPickUpDate.Text,
                        uoTextBoxPickupTime.Text, uoTextBoxDropOffDate.Text, uoTextBoxDropoffTime.Text, uoTextBoxPickUpPlace.Text,
                        uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text, GlobalCode.Field2String(Session["UserName"]), currentDateString,
                        seafarerStatus, uoTextBoxRemarks.Text, uoCheckBoxBilledToCrew.Checked, uoHiddenFieldAction.Value);

                    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                    strLogDescription = "Vehicle booking updated.";
                    strFunction = "uoButtonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                    OpenParentPagePending();
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load seafarer name to textbox            
        /// </summary>
        private void seafarerGetName()
        {
            IDataReader dr = null;
            try
            {
                string E1TravelReqID = "0";
                if (Request.QueryString["e1TR"] != null)
                {
                    E1TravelReqID = Request.QueryString["e1TR"];
                }
                dr = SeafarerBLL.SeafarerGetDetails(Session["strSFCode"].ToString(),
                    GlobalCode.Field2String(Session["TravelRequestID"]), GlobalCode.Field2String(Session["ManualRequestID"]), true);
                if (dr.Read())
                {
                    uoTextBoxSeafarer.Text = dr["NAME"].ToString();
                    uoHiddenFieldSeafarerID.Value = dr["colSeafarerIdInt"].ToString();
                    uoHiddenFieldPort.Value = dr["PORTID"].ToString();
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   21/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get country and city of seafarer's port of embarkation/disembarkation
        /// -------------------------------------------
        /// </summary>
        private void vehicleGetPortDetails()
        {
            uoHiddenFieldPortCountry.Value = "0";
            uoHiddenFieldPortCity.Value = "0";
            IDataReader dr = null;
            try
            {
                string PortID = (uoHiddenFieldPort.Value == "" ? "0" : uoHiddenFieldPort.Value);
                dr = PortBLL.GetPortToEdit(int.Parse(PortID));
                if (dr.Read())
                {
                    uoHiddenFieldPortCountry.Value = dr["colCountryIDInt"].ToString();
                    uoHiddenFieldPortCity.Value = dr["colCityIDInt"].ToString();
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
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load vendor name to dropdownlist     
        /// -------------------------------------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Set the default value if there is only 1 item
        /// </summary>
        private void vehicleGetCompany()
        {                 
            //DataTable dt = new DataTable();
            //dt = VehicleBLL.vehicleGetCompany();
            //uoDropDownListVehicleCompany.DataSource = dt;
            //uoDropDownListVehicleCompany.DataTextField = "VendorName";
            //uoDropDownListVehicleCompany.DataValueField = "VendorId";
            //uoDropDownListVehicleCompany.DataBind();

            DataTable dt = null;
            try
            {                
                //uoDropDownListVehicleCompany.Items.Clear();
                bool IsAccredited = true;
                if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    uoDropDownListCategory.SelectedValue = "0";
                }
                                
                if(uoDropDownListCategory.SelectedValue != "1")
                {
                    IsAccredited = false;
                }
                uoDropDownListVehicleCompany.Items.Clear();
                uoDropDownListVehicleCompany.Items.Add(new ListItem("--Select Vendor--", "0"));
                dt = SuperViewBLL.GetVendor("VE", IsAccredited, uoHiddenFieldPortCountry.Value, "0", "0", GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["UserRole"]));

                if (dt.Rows.Count > 0)
                {
                   // uoDropDownListVehicleCompany.SelectedValue = "0";
                    uoDropDownListVehicleCompany.DataSource = dt;
                    uoDropDownListVehicleCompany.DataTextField = "colVendorNameVarchar";
                    uoDropDownListVehicleCompany.DataValueField = "colVendorIdInt";
                    uoDropDownListVehicleCompany.DataBind();

                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                    {
                        if (uoDropDownListVehicleCompany.Items.FindByValue(GlobalCode.Field2String(Session["UserVendor"])) != null)
                        {
                            uoDropDownListVehicleCompany.SelectedValue = GlobalCode.Field2String(Session["UserVendor"]);
                        }
                    }
                }
                else
                {   
                    uoDropDownListVehicleCompany.DataBind();
                }
                //uoDropDownListVehicleCompany.Items.Insert(0, new ListItem("--Select Vendor--", "0"));

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListVehicleCompany.SelectedIndex = 1;
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
        /// Date Created:   25/08/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Select country list by vendor id   
        /// ------------------------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Set the default value if there is only 1 item
        /// </summary>
        private void countryListByVendorID()
        {
            DataTable dt = null;
            try
            {
                //uoDropDownListCountry.Items.Clear();
                int VendorID = 0;
                if (uoDropDownListVehicleCompany.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVehicleCompany.SelectedValue);
                }
                uoDropDownListCountry.Items.Clear();
                uoDropDownListCountry.Items.Add(new ListItem("--SELECT COUNTRY--", "0"));
                //uoDropDownListCountry.Items.Add(new ListItem("--SELECT COUNTRY--", "0"));
                dt = VehicleBLL.CountryListByVendorID(VendorID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCountry.DataSource = dt;
                    uoDropDownListCountry.DataTextField = "CountryName";
                    uoDropDownListCountry.DataValueField = "CountryId";
                    uoDropDownListCountry.DataBind();
                    if (uoHiddenFieldPortCountry.Value != "0")
                    {
                        if (uoDropDownListCountry.Items.FindByValue(uoHiddenFieldPortCountry.Value) != null)
                        {
                            uoDropDownListCountry.SelectedValue = uoHiddenFieldPortCountry.Value;
                        }
                    }
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                    {
                        if (uoDropDownListCountry.Items.FindByValue(Session["UserCountry"].ToString()) != null)
                        {
                            uoDropDownListCountry.SelectedValue = Session["UserCountry"].ToString();
                        }
                    }
                }
                else
                {
                    uoDropDownListCountry.DataBind();
                }
                //uoDropDownListCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
              

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListCountry.SelectedIndex = 1;                    
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
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Select city list by vendor and country id   
        /// ---------------------------------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Set the default value if there is only 1 item
        /// </summary>
        private void cityListByVendorCountryID()
        {            
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListVehicleCompany.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVehicleCompany.SelectedValue);
                }

                int CountryID = 0;
                if (uoDropDownListCountry.SelectedValue != "")
                {
                    CountryID = Convert.ToInt32(uoDropDownListCountry.SelectedValue);
                }

                uoDropDownListCity.Items.Clear();
                uoDropDownListCity.ClearSelection();
                uoDropDownListCity.Items.Add(new ListItem("--SELECT CITY--", "0"));
                dt = VehicleBLL.CityListByVendorCountryID(VendorID, CountryID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataSource = dt;
                    uoDropDownListCity.DataTextField = "CityName";
                    uoDropDownListCity.DataValueField = "CityId";
                    uoDropDownListCity.DataBind();
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                    {
                        if (uoDropDownListCity.Items.FindByValue(Session["UserCity"].ToString()) != null)
                        {
                            uoDropDownListCity.SelectedValue = Session["UserCity"].ToString();
                        }
                    }
                }
                else
                {
                    uoDropDownListCity.DataBind();
                }
                //uoDropDownListCity.Items.Insert(0,(new ListItem("--Select City--", "0")));                
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
        }

        /// <summary> 
        /// Date Created: 01/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load vehicle branch to dropdownlist
        /// -------------------------------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter VendorID
        ///                 Set the default value if there is only 1 item
        /// </summary>
        private void vehicleGetBranch()
        {                   
            DataTable dt = new DataTable();

            int cityID = 0;
            int vendorID = 0;
            if (uoDropDownListCity.SelectedValue != "0")
            {
                cityID = Convert.ToInt32(uoDropDownListCity.SelectedValue);
            }
            if (uoDropDownListVehicleCompany.SelectedValue != "0")
            {
                vendorID = Convert.ToInt32(uoDropDownListVehicleCompany.SelectedValue);
            }
            
            uoDropDownListVehicleBranch.Items.Clear();
            uoDropDownListVehicleBranch.Items.Add(new ListItem("--Select Branch--", "0"));
            dt = VehicleBLL.vehicleGetBranchByVendorUserCity(vendorID, GlobalCode.Field2String(Session["UserName"]), cityID, uoHiddenFieldRole.Value);
            if (dt.Rows.Count > 0)
            {
                uoDropDownListVehicleBranch.DataSource = dt;
                uoDropDownListVehicleBranch.DataTextField = "colVendorBranchNameVarchar";
                uoDropDownListVehicleBranch.DataValueField = "colBranchIDInt";
                uoDropDownListVehicleBranch.DataBind();
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                {
                    if (uoDropDownListVehicleBranch.Items.FindByValue(Session["UserBranchID"].ToString()) != null)
                    {
                        uoDropDownListVehicleBranch.SelectedValue = Session["UserBranchID"].ToString();
                    }
                }
            }
            else
            {
                uoDropDownListVehicleBranch.DataBind();
            }
            //uoDropDownListVehicleBranch.Items.Insert(0,new ListItem("--Select Branch--", "0"));
            if (dt.Rows.Count == 1)
            {
                uoDropDownListVehicleBranch.SelectedIndex = 1;
            }
        }

        /// <summary> 
        /// Date Created: 01/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load vehicle brand to dropdownlist
        /// </summary>
        private void vehicleGetBrand()
        {             
            DataTable dt = new DataTable();

            int branchID = 0;
            if (uoDropDownListVehicleBranch.SelectedValue != "0")
            {
                branchID = Convert.ToInt32(uoDropDownListVehicleBranch.SelectedValue);
            }

            //dt = VehicleBLL.vehicleGetBrand(branchID);
            //if (dt.Rows.Count > 0)
            //{
            //    uoDropDownListVehicleBrand.DataSource = dt;
            //    uoDropDownListVehicleBrand.DataTextField = "VehicleBrandName";
            //    uoDropDownListVehicleBrand.DataValueField = "VehicleBrandId";
            //    uoDropDownListVehicleBrand.DataBind();
            //}
            //else
            //{
            //    uoDropDownListVehicleBrand.DataBind();
            //}
        }

        /// <summary> 
        /// Date Created: 26/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Load vehicle make to dropdownlist
        /// </summary>
        private void vehicleGetMake()
        {            

            DataTable dt = new DataTable();

            int branchID = 0;
            if (uoDropDownListVehicleBranch.SelectedValue != "0")
            {
                branchID = Convert.ToInt32(uoDropDownListVehicleBranch.SelectedValue);
            }

            //dt = VehicleBLL.vehicleGetMake(branchID);
            //if (dt.Rows.Count > 0)
            //{
            //    uoDropDownListVehicleMake.DataSource = dt;
            //    uoDropDownListVehicleMake.DataTextField = "VehicleMakeName";
            //    uoDropDownListVehicleMake.DataValueField = "VehicleMakeId";
            //    uoDropDownListVehicleMake.DataBind();
            //}
            //else
            //{
            //    uoDropDownListVehicleMake.DataBind();
            //}
        }

        /// <summary> 
        /// Date Created:   24/08/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Load vehicle type to dropdownlist
        /// ----------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change vehicleGetType to vehicleGetTypeBrandMake,            
        ///                 Set the default value if there is only 1 item
        /// ------------------------------------------------------                
        /// Date Modified:  09/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Group vehicle type list,                    
        /// </summary>
        private void vehicleGetType()
        {           
            DataTable dt = new DataTable();

            int branchID = 0;
            if (uoDropDownListVehicleBranch.SelectedValue != "0")
            {
                branchID = Convert.ToInt32(uoDropDownListVehicleBranch.SelectedValue);
            }

            uoDropDownListVehicleType.Items.Clear();
            uoDropDownListVehicleType.Items.Add(new ListItem("--Select Vehicle Type--", "0"));
            
            dt = VehicleBLL.vehicleGetTypeBrandMake(branchID);
            if (dt.Rows.Count > 0)
            {
                ListItem item = new ListItem();
                foreach (DataRow row in dt.Rows)
                {
                    item = new ListItem(row["vehicleType"].ToString(), row["colVehicleIdBigint"].ToString());
                    item.Attributes["OptionGroup"] = row["colVehicleTypeNameVarchar"].ToString();
                    uoDropDownListVehicleType.Items.Add(item);
                }
                
                //    uoDropDownListVehicleType.DataSource = dt;
                //    uoDropDownListVehicleType.DataTextField = "vehicleType";
                //    uoDropDownListVehicleType.DataValueField = "colVehicleIDInt";
            }
            uoDropDownListVehicleType.DataBind();

            if (dt.Rows.Count == 1)
            {
                uoDropDownListVehicleType.SelectedIndex = 1;
            }
        }

        //private void vehicleGetType()
        //{ 
        ///// <summary> 
        ///// Date Created: 12/08/2011
        ///// Created By: Gabriel Oquialda
        ///// (description) Load vehicle type to dropdownlist
        ///// </summary>

        //    DataTable dt = new DataTable();

        //    int VendorID = 0;
        //    if (uoDropDownListVehicleCompany.SelectedValue != "")
        //    {
        //        VendorID = Convert.ToInt32(uoDropDownListVehicleCompany.SelectedValue);
        //    }

        //    dt = VehicleBLL.vehicleGetType(VendorID);
        //    if (dt.Rows.Count > 0)
        //    {
        //        uoDropDownListVehicleType.DataSource = dt;
        //        uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
        //        uoDropDownListVehicleType.DataValueField = "VehicleTypeId";
        //        uoDropDownListVehicleType.DataBind();
        //    }
        //    else
        //    {
        //        uoDropDownListVehicleType.DataBind();
        //    } 
        //}        
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get seafarer vehicle transaction   
        /// -----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>       
        private void seafarerGetInfo(Int32 vehiclePrimaryId, Int32 seqNo)
        {            
            IDataReader dr = null;
            try
            {
                if (uoHiddenFieldPendingId.Value == "0")
                {
                    if (uoHiddenFieldVehicleBookingID.Value == "0")
                    {
                        dr = VehicleBLL.vehicleGetTransaction(vehiclePrimaryId, seqNo);
                    }
                    else
                    {
                        dr = VehicleBLL.vehicleGetTransactionByID(uoHiddenFieldVehicleBookingID.Value);
                    }
                }
                else
                {
                    dr = VehicleBLL.vehicleGetPendingByID(uoHiddenFieldPendingId.Value);
                }
                if (dr.Read())
                {
                    uoTextBoxSeafarer.Text = dr["seafarerName"].ToString();
                    
                    //vehicleGetCompany();
                    uoDropDownListVehicleCompany.SelectedValue = dr["vendorId"].ToString();
                    
                    //uoDropDownListCategory.SelectedValue = dt.Rows[0]["Category"].ToString();
                    countryListByVendorID();
                    uoDropDownListCountry.SelectedValue = dr["Country"].ToString();

                    cityListByVendorCountryID();
                    uoDropDownListCity.SelectedValue = dr["City"].ToString();

                    vehicleGetBranch();
                    uoDropDownListVehicleBranch.SelectedValue = dr["BranchId"].ToString();

                    string scriptString = "return OpenContract('" + uoDropDownListVehicleBranch.SelectedValue + "');";
                    uoLinkButtonContract.Attributes.Add("OnClick", scriptString);

                    //                uoDropDownListVehicleBrand.SelectedValue = dt.Rows[0]["vehicleBrand"].ToString();
                    //uoDropDownListVehicleMake.SelectedValue = dt.Rows[0]["vehicleMake"].ToString();
                    vehicleGetType();
                    uoDropDownListVehicleType.SelectedValue = dr["vehicleTypeId"].ToString();
                    //uoTextBoxPlateNo.Text = dt.Rows[0]["vehiclePlateNo"].ToString();

                    DateTime dtPickUpDate = (dr["vehiclePickUpDate"].ToString().Length > 0)
                           ? Convert.ToDateTime(dr["vehiclePickUpDate"].ToString())
                           : DateTime.Now;
                    string strPickUpDate = String.Format("{0:MM/dd/yyyy}", dtPickUpDate);
                    uoTextBoxPickUpDate.Text = strPickUpDate;

                    if (dr["vehiclePickUpTime"] != null && dr["vehiclePickUpTime"].ToString() != "")
                    {
                        DateTime dtPickUpTime = DateTime.Parse(dr["vehiclePickUpTime"].ToString());
                        string strPickUpTime = String.Format("{0:HH:mm}", dtPickUpTime);
                        uoTextBoxPickupTime.Text = strPickUpTime;
                    }
                    else
                    {
                        uoTextBoxPickupTime.Text = "";
                    }

                    DateTime dtDropOffDate = (dr["vehicleDropOffDate"].ToString().Length > 0)
                           ? Convert.ToDateTime(dr["vehicleDropOffDate"].ToString())
                           : DateTime.Now;
                    string strDropOffDate = String.Format("{0:MM/dd/yyyy}", dtDropOffDate);
                    uoTextBoxDropOffDate.Text = strDropOffDate;

                    if (dr["vehicleDropOffTime"] != null && dr["vehicleDropOffTime"].ToString() != "")
                    {
                        DateTime dtDropOffTime = DateTime.Parse(dr["vehicleDropOffTime"].ToString());
                        string strDropOffTime = String.Format("{0:HH:mm}", dtDropOffTime);
                        uoTextBoxDropoffTime.Text = strDropOffTime;
                    }
                    else
                    {
                        uoTextBoxDropoffTime.Text = "";
                    }

                    uoTextBoxPickUpPlace.Text = dr["vehiclePickUpLocation"].ToString();
                    uoTextBoxDropOffPlace.Text = dr["vehicleDropOffLocation"].ToString();
                    string strVehicleStatus = dr["vehicleStatus"].ToString();
                    uoDropDownListVehicleStatus.SelectedValue = Convert.ToString(uoDropDownListVehicleStatus.Items.FindByText(strVehicleStatus).Value);
                    uoTextBoxRemarks.Text = dr["vehicleRemarks"].ToString();

                    if (strVehicleStatus == "Cancelled")
                    {
                        uoCheckBoxBilledToCrew.Visible = true;
                        uoCheckBoxBilledToCrew.Checked = Convert.ToBoolean(dr["vehicleBillToCrew"].ToString());
                    }

                    if (uoHiddenFieldPendingId.Value != "0")
                    {
                        uoHiddenFieldAction.Value = dr["ActionTaken"].ToString();
                    }
                    else
                    {
                        uoHiddenFieldCreatedBy.Value = dr["CreatedBy"].ToString();
                        uoHiddenFieldCreatedDate.Value = ((DateTime)dr["CreatedDate"]).ToString(TravelMartVariable.DateTimeFormat);
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
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   20/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user branch details if user is vendor or Service Provider
        /// -------------------------------------------------
        /// Date Modified:   27/11/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to IDataReader
        /// </summary>
        private void GetBranchInfo()
        {
            IDataReader dr = null;
            try
            {
                dr = UserAccountBLL.GetUserBranchDetails(GlobalCode.Field2String(Session["UserName"]), uoHiddenFieldRole.Value);
                if (dr.Read())
                {
                    Session["UserVendor"] = dr["VendorID"].ToString();
                    Session["UserBranchID"] = dr["BranchID"].ToString();
                    Session["UserCountry"] = dr["CountryID"].ToString();
                    Session["UserCity"] = dr["CityID"].ToString();
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
        /// Date Created: 05/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert validation of vehicle pick-up date and time
        /// </summary>
        private Boolean pickupdatetimeInInsertExist()
        {          
            String travelLocatorId = Request.QueryString["ID"];
            String SeqNo = Session["strSFSeqNo"].ToString();
            String branchId = uoDropDownListVehicleBranch.SelectedValue;
            DateTime pickupDate = Convert.ToDateTime(uoTextBoxPickUpDate.Text);
            String pickupTime = (uoTextBoxPickupTime.Text == null) ? "" : uoTextBoxPickupTime.Text;

            Boolean bValidation = VehicleBLL.pickupdatetimeExist(travelLocatorId, SeqNo, branchId, pickupDate, pickupTime);
            return bValidation;
        }

        /// <summary>
        /// Date Created: 05/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update validation of vehicle pick-up date and time
        /// </summary>
        private Boolean pickupdatetimeInUpdateExist()
        {            
            String travelLocatorId = Request.QueryString["ID"].ToString();
            String SeqNo = Session["strSFSeqNo"].ToString();
            String branchId = uoDropDownListVehicleBranch.SelectedValue;
            DateTime pickupDate = Convert.ToDateTime(uoTextBoxPickUpDate.Text);
            String pickupTime = (uoTextBoxPickupTime.Text == null) ? "" : uoTextBoxPickupTime.Text;

            Boolean bValidation = VehicleBLL.pickupdatetimeExist(travelLocatorId, SeqNo, branchId, pickupDate, pickupTime);
            return bValidation;
        }

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Format data to uppercase        
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {            
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page            
        /// </summary>
        private void OpenParentPage()
        {                 
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicle\").val(\"1\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        private void OpenParentPagePending()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehiclePending\").val(\"1\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created:   21/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Enable/Disable dropdown list for hotel vendor
        /// </summary>
        private void EnableDisableControl()
        {
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            {
                uoDropDownListCategory.Enabled = false;
                uoDropDownListVehicleCompany.Enabled = false;
                uoDropDownListCountry.Enabled = false;
                uoDropDownListCity.Enabled = false;
                uoDropDownListVehicleBranch.Enabled = false;
            }
            else
            {
                uoDropDownListCategory.Enabled = true;
                uoDropDownListVehicleCompany.Enabled = true;
                uoDropDownListCountry.Enabled = true;
                uoDropDownListCity.Enabled = true;
                uoDropDownListVehicleBranch.Enabled = true;
            }

            if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
            {
                uoDropDownListCategory.Enabled = false;
            }
        }
        /// <summary>
        /// Date Created: 18/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {            
            //string sScript = "<script language=JavaScript>";
            //s = s.Replace("'", "\"");
            //sScript += "alert('" + s + "');";
            //sScript += "</script>";    
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
                    
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);       
        }
        private void SendEmail(string sSubject, string sMessage)
        {
            string sBody;
            DataTable dt = null;
            try
            {
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, uoDropDownListVehicleBranch.SelectedValue, uoDropDownListCountry.SelectedValue);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
                    sBody += "<br/><br/>Kindly approve.";
                    sBody += "<br/><br/>Thank you.";
                    sBody += "<br/><br/><i>Auto generated email.</i>";
                    sBody += "</TR></TD></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email 24*7
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleCrewAssist, "0", uoDropDownListCountry.SelectedValue);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + "Admin" + ", <br/><br/> " + sMessage;
                    sBody += "<br/><br/>Kindly approve.";
                    sBody += "<br/><br/>Thank you.";
                    sBody += "<br/><br/><i>Auto generated email.</i>";
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel specialist of the country affected
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleVehicleSpecialist, "0", uoDropDownListCountry.SelectedValue);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleVehicleSpecialist + ", <br/><br/> " + sMessage;
                    sBody += "<br/><br/>Kindly approve.";
                    sBody += "<br/><br/>Thank you.";
                    sBody += "<br/><br/><i>Auto generated email.</i>";
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel vendor
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleVehicleVendor, uoDropDownListVehicleBranch.SelectedValue, uoDropDownListCountry.SelectedValue);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleVehicleVendor + ", <br/><br/> " + sMessage;
                    sBody += "<br/><br/>Thank you.";
                    sBody += "<br/><br/><i>Auto generated email.</i>";
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
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
        #endregion                
    }
}

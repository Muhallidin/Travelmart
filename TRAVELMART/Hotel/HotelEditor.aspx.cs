using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART
{
    public partial class HotelEditor : System.Web.UI.Page
    {
        #region Declaration
        Boolean bValidation = false;
        //Boolean bContract;
        //Boolean bOverride;
        //string withOverride;
        //bool bWithTax;
        #endregion

        #region "Events"
        /// <summary>
        /// Date Created:   25/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Load all basic  hotel details        
        /// -------------------------------------------
        /// Date Modified:   09/08/2011
        /// Modified By:     Josephine Gad
        /// (description)    Close DataTable
        /// -------------------------------------------
        /// Date Modified:   25/08/2011
        /// Modified By:     Josephine Gad
        /// (description)    Remove the try and catch  block and put in new function HotelBookingLoad
        ///                  Add hID for none Sabre bookings
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || 
                uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
            {
                if (GlobalCode.Field2String(Session["UserCountry"]) == "0" || GlobalCode.Field2String(Session["UserCountry"]) == "")
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
                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {
                    Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                }
                uoTextBoxCheckInTime.Text = "00:00";
                uoTextBoxTaxAmount.Text = "0";
                uoTextBoxRoomAmount.Text = "0";

                uoHiddenFieldRole.Value = MUser.GetUserRole();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;
                Session["strSFCode"] = Request.QueryString["sfId"];
                //Session["strSFStatus"] = Request.QueryString["st"];
                Session["strTravelLocatorID"] = Request.QueryString["ID"];
                //TravelMartVariable.strRecordLocator = Request.QueryString["recloc"];


                uoHiddenFieldStatus.Value = Request.QueryString["st"];
                uoHiddenFieldRecordLocator.Value = Request.QueryString["recloc"].ToString();
                uoHiddenFieldTravelRequestID.Value = Request.QueryString["trID"];
                uoHiddenFieldManualRequestID.Value = Request.QueryString["manualReqID"]; 
                uoHiddenFieldOnOffDate.Value = Request.QueryString["Date"];

                if (Request.QueryString["hID"] != null)
                {
                    uoHiddenFieldHotelBookingID.Value = Request.QueryString["hID"];
                }
                else
                {
                    uoHiddenFieldHotelBookingID.Value = "0";
                }
                if (Request.QueryString["PendingID"] != null)
                {
                    uoHiddenFieldPendingId.Value = Request.QueryString["PendingID"].ToString();
                }
                else
                {
                    uoHiddenFieldPendingId.Value = "0";
                }
                
                SeafarerLoad();
                HotelGetPortDetails();
                HotelLoad();
                HotelGetCountryList();
                HotelGetCityList();
                HotelGetVendorBranch();
                HotelGetRoomType();
                VoucherGetDetails();
                //HotelGetMeals();

                //ChangeToUpperCase(uoDropDownListCategory);   
                //ChangeToUpperCase(uoDropDownListHotel);                
                //ChangeToUpperCase(uoDropDownListCountry);
                //ChangeToUpperCase(uoDropDownListCity);
                //ChangeToUpperCase(uoDropDownListBranch);
                //ChangeToUpperCase(uoDropDownListRoomType);
                
                //GetNumberOfRoomsAvailableByHotelAndLocation();
                HotelBookingLoad();
                HotelEventNotification();
                //HotelRoomBlockCountZero();
                EnableDisableControl();
                //uoTextBoxNoOfdays.Attributes.Add("onchange", "return GetAmount();");


            }            
        }
        protected void uoDropDownListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {                       
            HotelLoad();
            HotelGetCountryList();
            HotelGetCityList();
            HotelGetVendorBranch();
            HotelGetRoomType();
            //HotelGetMeals();

            //ChangeToUpperCase(uoDropDownListHotel);
            //ChangeToUpperCase(uoDropDownListCountry);
            //ChangeToUpperCase(uoDropDownListCity);
            //ChangeToUpperCase(uoDropDownListBranch);
            //ChangeToUpperCase(uoDropDownListRoomType);
        }                       
        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select hotel status and make visible the check box bill to crew when status cancelled        
        /// </summary>
        protected void uoDropDownListHotelStatus_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (uoDropDownListHotelStatus.SelectedValue == "Cancelled")
            {
                uoCheckBoxCrewBill.Visible = true;               
            }
            else
            {
                uoCheckBoxCrewBill.Checked = false;
                uoCheckBoxCrewBill.Visible = false;              
            }
        }                  
        /// <summary>
        /// Date Created:   25/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Select hotel and make all the branch available      
        /// ---------------------------------------------------------------   
        /// Date Modified:   07/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Add loading of other Dropdown list (Ccountry,City, Meals)
        ///                  Add link to contract
        /// ---------------------------------------------------------------    
        /// </summary>
        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelGetCountryList();
            HotelGetCityList();
            HotelGetVendorBranch();
            HotelGetRoomType();
            HotelEventNotification();            
            VoucherGetDetails();
            CurrencyLoad();
            //HotelGetMeals();
            
            //ChangeToUpperCase(uoDropDownListCountry);
//            ChangeToUpperCase(uoDropDownListCity);
            //ChangeToUpperCase(uoDropDownListBranch);
            //ChangeToUpperCase(uoDropDownListRoomType);

            string scriptString = "return OpenContract('" + uoDropDownListBranch.SelectedValue + "');";            
            uoLinkButtonContract.Attributes.Add("OnClick", scriptString);

            string scriptEventString = "return OpenEventsList('" + uoDropDownListBranch.SelectedValue + "', '" + uoDropDownListCity.SelectedValue + "', '" + uoHiddenFieldOnOffDate.Value + "');";
            uoLinkButtonEvent.Attributes.Add("OnClick", scriptEventString);
        }
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelGetCityList();
            HotelGetVendorBranch();
            HotelGetRoomType();
            CurrencyLoad();            
            VoucherGetDetails();
            //HotelGetMeals();

            //ChangeToUpperCase(uoDropDownListCity);
            //ChangeToUpperCase(uoDropDownListBranch);
            //ChangeToUpperCase(uoDropDownListRoomType);
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelGetVendorBranch();
            HotelGetRoomType();
            VoucherGetDetails();
            CurrencyLoad();
            //HotelGetMeals();
            
            //ChangeToUpperCase(uoDropDownListBranch);
            //ChangeToUpperCase(uoDropDownListRoomType);
        }
        protected void uoDropDownListBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelGetRoomType();
            HotelEventNotification();
            //HotelRoomBlockCountZero();

            string scriptString = "return OpenContract('" + uoDropDownListBranch.SelectedValue + "');";
            uoLinkButtonContract.Attributes.Add("OnClick", scriptString);

            string scriptEventString = "return OpenEventsList('" + uoDropDownListBranch.SelectedValue + "', '" + uoDropDownListCity.SelectedValue + "', '" + uoHiddenFieldOnOffDate.Value + "');";
            uoLinkButtonEvent.Attributes.Add("OnClick", scriptEventString);
            VoucherGetDetails();
            GetVendorHotelDetails();
            CurrencyLoad();
            HotelContractLoad(uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue);
        }
       
        /// <summary>
        /// Date Created:   01/08/2011
        /// Created By:     Ryan Bautista
        /// (description)   Viewing of number of rooms available by hotel name and hotel location        
        /// ------------------------------------------------------------------------------------
        /// Date Modified:  08/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add HotelContractLoad()
        /// </summary>
        ///         
        protected void uoDropDownListRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetNumberOfRoomsAvailableByHotelAndLocation();
            //HotelRoomBlockCountZero();
            HotelContractLoad(uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue);
            VoucherGetDetails();
        }        
        protected void uoCheckBoxTax_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxTax.Checked == true)
            {
                uoLabelTaxAmount.Visible = true;
                uoTextBoxTaxAmount.Visible = true;                
            }
            else
            {
                uoTextBoxTaxAmount.Text = "";
                uoLabelTaxAmount.Visible = false;
                uoTextBoxTaxAmount.Visible = false;
            }
        }
        /// <summary>
        /// Date Created:   25/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Saving of hotel transaction        
        /// ------------------------------------------
        /// Date Modified:  26/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change InsertHotelBooking to InsertHotelBookingOther
        /// ------------------------------------------
        /// Date Modified:  11/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add paramater record loc and manual Req ID in Insert
        /// ------------------------------------------
        /// Date Modified:  10/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add HotelSendEmail
        /// </summary>
        /// 
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {

            uoTextBoxVoucher.Text = uoHiddenFieldVoucherAmount.Value;
            Save();
            OpenParentPagePending();

            //return;
            //HotelValidationSettings();            
            //try
            //{
            //    DateTime CheckInDate = Convert.ToDateTime(uoTextBoxCheckInDate.Text);

            //    if (CheckInDate < DateTime.Today)
            //    {
            //        AlertMessage("Check-In Date is already passed.");
            //    }
            //    else
            //    {
            //        uoTextBoxVoucher.Text = uoHiddenFieldVoucherAmount.Value;
            //        if (HotelRoomBlockCountZero() == false)
            //        {
            //            Save();
            //            OpenParentPagePending();
            //        }
            //        else
            //        {
            //            //AlertMessage("Available room block(s) are full, to override please enter room amount.");

            //            if (uoTextBoxRoomAmount.Text == "0")
            //            {
            //                AlertMessage("There are no available room block(s), to override please enter room amount.");
            //            }
            //            else
            //            {
            //                if (uoCheckBoxTax.Checked == true)
            //                {
            //                    if (uoTextBoxTaxAmount.Text == "0")
            //                    {
            //                        AlertMessage("Please enter tax amount.");
            //                    }
            //                    else
            //                    {
            //                        Save();
            //                        OpenParentPagePending();
            //                    }
            //                }
            //                else
            //                {
            //                    Save();
            //                    OpenParentPagePending();
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    AlertMessage(ex.Message);
            //}
        }

        protected void uoCheckBoxLunchDinner_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void uoCheckBoxLunch_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void uoCheckBoxDinner_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void uoButtonValidate_Click(object sender, EventArgs e)
        {
            uoHiddenFieldIsValidated.Value = "1";
            CurrencyLoad();
            BindGridViewDate(true);            
        }
        #endregion

        #region "Functions"

        #region Save

        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Saving
        /// -------------------------------
        /// Date MOdified:  06/12/2011
        /// Modified By:    Charlene Remotigue
        /// (description)   add contractId as parameter
        /// -------------------------------
        /// Date MOdified:  17/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add contract/override for each day
        /// -------------------------------
        /// Date MOdified:  08/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add approval after saving hotel bookings
        /// </summary>
        private void Save()
        {
            string CheckInDateString = uoTextBoxCheckInDate.Text;
            string CheckInTimeString = uoTextBoxCheckInTime.Text;
            DateTime CheckInDateTime = DateTime.Parse(CheckInDateString + " " + CheckInTimeString);

            DateTime currentDate = DateTime.Now;
            string currentDateString = currentDate.ToString(TravelMartVariable.DateTimeFormat);

            string strLogDescription;
            string strFunction;

            string sContractFrom = null;
            if (TravelMartVariable.RolePortSpecialist == MUser.GetUserRole())
            {
                sContractFrom = "Port";
            }

            if (uoHiddenFieldPendingId.Value == "0")
            {
                //Add new booking
                if (Request.QueryString["Add"] == "1")
                {                                       
                    Int32 pID = 0;
                    pID = HotelBLL.InsertHotelBookingPending("", "", "", uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value,
                      uoHiddenFieldManualRequestID.Value, uoDropDownListHotel.SelectedValue,
                      uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue, CheckInDateTime.ToString(),
                      CheckInTimeString, uoTextBoxNoOfdays.Text, uoDropDownListHotelStatus.Text, GlobalCode.Field2String(Session["strSFStatus"]),
                      GlobalCode.Field2String(Session["UserName"]), currentDateString, "", "", uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked, uoCheckBoxBreakfast.Checked,
                      uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked, uoCheckBoxLunchDinner.Checked, uoTextBoxVoucher.Text, uoCheckBoxShuttle.Checked,
                      "Add", uoTextBoxConfirmation.Text);
                    //pID = HotelBLL.InsertHotelBookingPending("", "", "", uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value,
                    //    uoHiddenFieldManualRequestID.Value, uoDropDownListHotel.SelectedValue,
                    //    uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue, CheckInDateTime.ToString(),
                    //    CheckInTimeString, uoTextBoxNoOfdays.Text, uoDropDownListHotelStatus.Text, GlobalCode.Field2String(Session["strSFStatus"]),
                    //    GlobalCode.Field2String(Session["UserName"]), currentDateString, "", "", uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked, uoCheckBoxBreakfast.Checked,
                    //    uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked, uoCheckBoxLunchDinner.Checked, uoTextBoxVoucher.Text, uoCheckBoxShuttle.Checked,
                    //    uoTextBoxRoomAmount.Text, Convert.ToInt32(uoHiddenFieldCurrencyID.Value),
                    //    uoCheckBoxTax.Checked, uoTextBoxTaxAmount.Text, "Add", uoTextBoxConfirmation.Text, uoHiddenFieldContractId.Value);
                    
                   SaveDetailsByDate(pID, sContractFrom, true);
                   ApproveBooking(pID.ToString(), uoHiddenFieldUser.Value);
                    string sMsg = uoDropDownListBranch.SelectedItem.Text + " has been added and pending for approval.";
                    sMsg += "<br/>Seafarer: " + uoTextBoxName.Text;
                    sMsg += "<br/>E1 ID: " + uoHiddenFieldSfID.Value;
                    //Comment HotelSendEmail in dev only
                    HotelSendEmail("Travelmart: New Hotel booking added", sMsg);

                    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                    strLogDescription = "Hotel booking added and pending for approval.";
                    strFunction = "uoButtonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(pID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    
                }
                else
                {
                    //Edit Booking from Sabre
                    if (uoHiddenFieldHotelBookingID.Value == "0")
                    {                        
                        Int32 pID = 0;                    
                        //pID = HotelBLL.InsertHotelBookingPending("", Request.QueryString["ID"].ToString(), Request.QueryString["SN"].ToString(),
                        //    uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value, uoHiddenFieldManualRequestID.Value,
                        //    uoDropDownListHotel.SelectedValue, uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue,
                        //    CheckInDateTime.ToString(), CheckInTimeString, uoTextBoxNoOfdays.Text, uoDropDownListHotelStatus.Text,
                        //    GlobalCode.Field2String(Session["strSFStatus"]), uoHiddenFieldCreatedBy.Value, uoHiddenFieldCreatedDate.Value,
                        //    GlobalCode.Field2String(Session["UserName"]), currentDateString, uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked,
                        //    uoCheckBoxBreakfast.Checked, uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked,
                        //    uoCheckBoxLunchDinner.Checked, uoTextBoxVoucher.Text, uoCheckBoxShuttle.Checked, uoTextBoxRoomAmount.Text, Convert.ToInt32(uoHiddenFieldCurrencyID.Value),
                        //    uoCheckBoxTax.Checked, uoTextBoxTaxAmount.Text, "Edit", uoTextBoxConfirmation.Text, uoHiddenFieldContractId.Value);

                        //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                        strLogDescription = "Hotel booking updated and pending for approval.";
                        strFunction = "uoButtonSave_Click";

                        DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(pID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                        //BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pID.Rows[0]["@pPendingHotelId"].ToString()), Convert.ToInt32(pID.Rows[0]["@pSeqNo"].ToString()), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                        //                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));                            
                    }
                    else
                    {
                        //Edit Booking from Other
                        Int32 pID = 0;
                        pID = HotelBLL.InsertHotelBookingPending(uoHiddenFieldHotelBookingID.Value, "", Request.QueryString["SN"].ToString(),
                            uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value, uoHiddenFieldManualRequestID.Value,
                            uoDropDownListHotel.SelectedValue, uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue,
                            CheckInDateTime.ToString(), CheckInTimeString, uoTextBoxNoOfdays.Text, uoDropDownListHotelStatus.Text,
                            GlobalCode.Field2String(Session["strSFStatus"]), uoHiddenFieldCreatedBy.Value, uoHiddenFieldCreatedDate.Value,
                            GlobalCode.Field2String(Session["UserName"]), currentDateString, uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked,
                            uoCheckBoxBreakfast.Checked, uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked,
                            uoCheckBoxLunchDinner.Checked, uoTextBoxVoucher.Text, uoCheckBoxShuttle.Checked, 
                            //uoTextBoxRoomAmount.Text, Convert.ToInt32(uoHiddenFieldCurrencyID.Value), uoCheckBoxTax.Checked, uoTextBoxTaxAmount.Text, 
                            "Edit", uoTextBoxConfirmation.Text);
                        
                        SaveDetailsByDate(pID, sContractFrom, true);
                        ApproveBooking(pID.ToString(), uoHiddenFieldUser.Value);

                        //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                        strLogDescription = "Hotel booking updated and pending for approval.";
                        strFunction = "uoButtonSave_Click";

                        DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(pID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }

                    string sMsg = uoDropDownListBranch.SelectedItem.Text + " has been updated and pending for approval.";
                    sMsg += "<br/>Seafarer: " + uoTextBoxName.Text;
                    sMsg += "<br/>E1 ID: " + uoHiddenFieldSfID.Value;
                    //Comment HotelSendEmail in dev only
                    HotelSendEmail("Travelmart: Hotel booking updated", sMsg);
                }
            }
            else
            {
                HotelBLL.UpdateHotelBookingPending(uoHiddenFieldPendingId.Value, uoHiddenFieldHotelBookingID.Value, "",
                   Request.QueryString["SN"].ToString(),
                   uoHiddenFieldTravelRequestID.Value, uoHiddenFieldRecordLocator.Value, uoHiddenFieldManualRequestID.Value,
                   uoDropDownListHotel.SelectedValue, uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue,
                   CheckInDateTime.ToString(), CheckInTimeString, uoTextBoxNoOfdays.Text, uoDropDownListHotelStatus.Text,
                   GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["UserName"]), currentDateString, uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked,
                   uoCheckBoxBreakfast.Checked, uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked,
                   uoCheckBoxLunchDinner.Checked, uoTextBoxVoucher.Text, uoCheckBoxShuttle.Checked, "Edit");

                SaveDetailsByDate(GlobalCode.Field2Int(uoHiddenFieldPendingId.Value), sContractFrom, false);
                ApproveBooking(uoHiddenFieldPendingId.Value, uoHiddenFieldUser.Value);

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Hotel booking updated and pending for approval.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(uoHiddenFieldPendingId.Value), Request.QueryString["SN"].ToString(), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="sContractFrom"></param>
        private void SaveDetailsByDate(Int32 pID, string sContractFrom, bool IsNew)
        {           
            DataTable dt = null;
            DataTable dtOldValues = null;
            try
            {
                dt = new DataTable();
                
                DataColumn colID = new DataColumn("colID", typeof(Int32));
                DataColumn colDate = new DataColumn("colDate", typeof(DateTime));

                dt.Columns.Add(colID);
                dt.Columns.Add(colDate);

                DataRow dr;

                foreach (GridViewRow gridRow in uoGridViewDate.Rows)
                {
                    string sDetailsID = gridRow.Cells[0].Text;
                    string sContractID = gridRow.Cells[1].Text;

                    //From table Other
                    if (uoHiddenFieldHotelBookingID.Value != "0")
                    {
                        sDetailsID = "0";
                    }
            
                    DateTime dDate = CommonFunctions.ConvertDateByFormat(gridRow.Cells[2].Text);

                    Int32 iDetailsID = GlobalCode.Field2Int(sDetailsID);

                    CheckBox CheckBoxFromContract = (CheckBox)gridRow.FindControl("uoCheckBoxFromContract");
                    TextBox TextBoxRate = (TextBox)gridRow.FindControl("uoTextBoxRate");
                    CheckBox CheckBoxTax = (CheckBox)gridRow.FindControl("uoCheckBoxTax");
                    TextBox TextBoxTaxPercent = (TextBox)gridRow.FindControl("uoTextBoxTaxPercent");
                    
                    iDetailsID = HotelBLL.InsertHotelBookingPendingDetails(iDetailsID, pID, dDate, GlobalCode.Field2Int(sContractID),
                        sContractFrom, GlobalCode.Field2Int(uoHiddenFieldCurrencyID.Value),
                        GlobalCode.Field2Decimal(TextBoxRate.Text), GlobalCode.Field2Decimal(TextBoxTaxPercent.Text),
                        CheckBoxTax.Checked, uoHiddenFieldUser.Value);

                    dr = dt.NewRow();
                    dr["colID"] = iDetailsID;
                    dr["colDate"] = dDate;
                    dt.Rows.Add(dr);
                }

                if (!IsNew)
                {
                    dtOldValues = HotelBLL.SelectHotelBookingPendingDetails(pID);
                    foreach (DataRow r in dtOldValues.Rows)
                    {
                        bool IsExists = false;
                        foreach (DataRow rNew in dt.Rows)
                        {
                            if (r["colHotelTransPendingDetailsIDBigInt"].ToString() == rNew["colID"].ToString())
                            {
                                IsExists = true;
                                break;
                            }
                        }
                        if (!IsExists)
                        {
                            HotelBLL.DeleteHotelBookingPendingDetails(GlobalCode.Field2Int(r["colHotelTransPendingDetailsIDBigInt"].ToString())
                                , uoHiddenFieldUser.Value);
                        }
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
                if (dtOldValues != null)
                {
                    dtOldValues.Dispose();
                }
            }
        }
        #endregion

        #region Country Currency Load

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all currency  
        /// </summary>
        private void CurrencyLoad()
        {
            IDataReader drCurrency = null;
            try
            {
                drCurrency = ContractBLL.GetCurrencyByCountry(uoDropDownListCountry.SelectedValue);
                if (drCurrency.Read())
                {
                    uoTextBoxCurrency.Text = drCurrency["colCurrencyNameVarchar"].ToString();
                    uoHiddenFieldCurrencyID.Value = drCurrency["colCurrencyIDInt"].ToString();
                }
                else
                {
                    uoTextBoxCurrency.Text = "No available currency.";
                    uoHiddenFieldCurrencyID.Value = "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drCurrency != null)
                {
                    drCurrency.Close();
                    drCurrency.Dispose();
                }
            }
        }
        #endregion
        /// <summary>
        /// Date Created:   17/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Add BindGridViewDate        
        /// </summary>
        private void HotelBookingLoad()
        {
            IDataReader dr = null;
            try
            {
                ViewState["hBranch"] = "";
                if (Request.QueryString["Add"] != "1")
                {
                    if (uoHiddenFieldPendingId.Value == "0")
                    {
                        if (uoHiddenFieldHotelBookingID.Value != "0")
                        {
                            dr = HotelBLL.HotelBookingDetailsOtherByID(uoHiddenFieldHotelBookingID.Value);
                        }
                        else
                        {
                            if (Request.QueryString["ID"] != null)
                            {
                                dr = HotelBLL.HotelBookingDetailsByID(Convert.ToInt32(Request.QueryString["ID"]),
                                    Convert.ToInt32(Request.QueryString["SN"]));
                            }
                        }

                    }
                    else
                    {
                        dr = HotelBLL.HotelBookingPendingByID(uoHiddenFieldPendingId.Value);
                    }
                    if (dr.Read())
                    {
                        //uoHiddenFieldContractId.Value = dr["colContractIDInt"].ToString();

                        this.uoTextBoxName.Text = dr["colSeafarerNameVarhar"].ToString();

                        uoDropDownListHotel.SelectedValue = dr["colVendorIdInt"].ToString();
                                              
                        HotelGetCountryList();
                        //ChangeToUpperCase(uoDropDownListCountry);
                        uoDropDownListCountry.SelectedValue = dr["colCountryIDInt"].ToString();

                        HotelGetCityList();
                        //ChangeToUpperCase(uoDropDownListCity);
                        uoDropDownListCity.SelectedValue = dr["colCityIDInt"].ToString();

                        DateTime dtTrans = (dr["colTimeSpanStartDate"].ToString().Length > 0)
                        ? Convert.ToDateTime(dr["colTimeSpanStartDate"].ToString())
                        : DateTime.Now;
                        string TransDate = String.Format("{0:MM/dd/yyyy}", dtTrans);
                        uoTextBoxCheckInDate.Text = TransDate;

                        if (dr["colTimeSpanStartTime"] != null && dr["colTimeSpanStartTime"].ToString() != "")
                        {
                            DateTime CheckinTime = DateTime.Parse(dr["colTimeSpanStartTime"].ToString());
                            string TransTime = String.Format("{0:HH:mm}", CheckinTime);
                            uoTextBoxCheckInTime.Text = TransTime;
                        }
                        else
                        {
                            uoTextBoxCheckInTime.Text = "";
                        }
                        uoTextBoxNoOfdays.Text = dr["colTimeSpanDurationInt"].ToString();
                        uoTextBoxConfirmation.Text = dr["colConfirmationNoVarchar"].ToString();

                        string strStatus = dr["colHotelStatusVarchar"].ToString();
                        uoDropDownListHotelStatus.SelectedValue = Convert.ToString(uoDropDownListHotelStatus.Items.FindByText(strStatus).Value);

                        HotelGetVendorBranch();
                        //ChangeToUpperCase(uoDropDownListBranch);
                        uoDropDownListBranch.SelectedValue = dr["colBranchIDInt"].ToString();
                        ViewState["hBranch"] = uoDropDownListBranch.SelectedValue;
                        string scriptString = "return OpenContract('" + uoDropDownListBranch.SelectedValue + "');";
                        uoLinkButtonContract.Attributes.Add("OnClick", scriptString);

                        string scriptEventString = "return OpenEventsList('" + uoDropDownListBranch.SelectedValue + "', '" + uoDropDownListCity.SelectedValue + "', '" + uoHiddenFieldOnOffDate.Value + "');";
                        uoLinkButtonEvent.Attributes.Add("OnClick", scriptEventString);

                        HotelGetRoomType();
                        //ChangeToUpperCase(uoDropDownListRoomType);
                        uoDropDownListRoomType.SelectedValue = dr["colRoomTypeIDInt"].ToString();

                        VoucherGetDetails();
                        //HotelGetMeals();                       

                        uoCheckBoxBreakfast.Checked = Convert.ToBoolean(dr["colBreakfastBit"].ToString());
                        uoCheckBoxLunch.Checked = Convert.ToBoolean(dr["colLunchBit"].ToString());
                        uoCheckBoxDinner.Checked = Convert.ToBoolean(dr["colDinnerBit"].ToString());
                        uoCheckBoxLunchDinner.Checked = Convert.ToBoolean(dr["colLunchOrDinnerBit"].ToString());
                        uoTextBoxRemarks.Text = dr["colRemarksVarchar"].ToString();
                        uoCheckBoxShuttle.Checked = Convert.ToBoolean(dr["colWithShuttleBit"].ToString());
                        if (strStatus == "Cancelled")
                        {
                            uoCheckBoxCrewBill.Visible = true;
                            uoCheckBoxCrewBill.Checked = Convert.ToBoolean(dr["colIsBilledToCrewBit"].ToString());
                        }
                        if (uoHiddenFieldPendingId.Value != "0")
                        {
                            uoHiddenFieldAction.Value = dr["ActionTaken"].ToString();
                        }
                        else
                        {
                            uoHiddenFieldCreatedBy.Value = dr["CreatedBy"].ToString();
                            if (dr["CreatedDate"] != null)
                            {
                                uoHiddenFieldCreatedDate.Value = ((DateTime)dr["CreatedDate"]).ToString(TravelMartVariable.DateTimeFormat);
                            }
                        }
                        CurrencyLoad();
                        BindGridViewDate(false);
                        
                        //withOverride = dt.Rows[0]["colRoomAmountMoney"].ToString();
                        ////bWithTax = Convert.ToBoolean(dt.Rows[0]["colWithTaxBit"].ToString());

                        //if(withOverride != "" || withOverride != null)
                        //{
                        //    uoLabelRoomAmount.Visible = true;
                        //    uoTextBoxRoomAmount.Visible = true;
                        //    uoCheckBoxTax.Visible = true;

                        //    uoTextBoxRoomAmount.Text = dt.Rows[0]["colRoomAmountMoney"].ToString();
                        //    uoCheckBoxTax.Checked = Convert.ToBoolean(dt.Rows[0]["colWithTaxBit"].ToString());

                        //    if (uoCheckBoxTax.Checked == true)
                        //    {                                
                        //        uoLabelTaxAmount.Visible = true;
                        //        uoTextBoxTaxAmount.Visible = true;
                        //        uoTextBoxTaxAmount.Text = dt.Rows[0]["colTaxAmountMoney"].ToString();                                
                        //    }
                        //    else
                        //    {
                        //        uoLabelTaxAmount.Visible = false;
                        //        uoTextBoxTaxAmount.Visible = false;
                        //    }
                        //}
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Viewing of number of rooms available by hotel name and hotel location        
        /// </summary>
        /// 
        //private void GetNumberOfRoomsAvailableByHotelAndLocation()
        //{
          
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = HotelBLL.GetNumberOfRoomsAvailableByHotelAndLocation(Convert.ToInt32(uoDropDownListHotel.SelectedValue),
        //                                                                            Convert.ToInt32(uoDropDownListRoomType.SelectedValue));
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all seafarer name and details   
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// </summary>
        /// 
        private void SeafarerLoad()
        {           
            IDataReader dr = null;
            try
            {
                string E1TravelReqID = "0";
                if (Request.QueryString["e1TR"] != null)
                {
                    E1TravelReqID = Request.QueryString["e1TR"];
                }
                dr = SeafarerBLL.SeafarerGetDetails(GlobalCode.Field2String(Session["strSFCode"]),
                    GlobalCode.Field2String(Session["TravelRequestID"]), GlobalCode.Field2String(Session["ManualRequestID"]), true);
                if(dr.Read())
                {
                    uoTextBoxName.Text = dr["NAME"].ToString();
                    //uoTextBoxVoucher.Text = Convert.ToDouble(dt.Rows[0]["VoucherAmount"].ToString()).ToString("#,##0.00");
                    uoHiddenFieldSfID.Value = dr["colSeafarerIdInt"].ToString();
                    uoHiddenFieldRankType.Value =dr["RANKTYPE"].ToString();
                    uoHiddenFieldPort.Value = dr["PORTID"].ToString();
                    uoHiddenFieldStripe.Value = dr["STRIPES"].ToString();
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

        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// -----------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// 
        private void VoucherGetDetails()
        {
            IDataReader dr = null;
            try
            {

                decimal dSrtipe = GlobalCode.Field2Decimal(uoHiddenFieldStripe.Value);
                dr = SeafarerBLL.VoucherGetDetails(dSrtipe, uoDropDownListBranch.SelectedValue, uoTextBoxNoOfdays.Text);
                if (dr.Read())
                {
                    uoHiddenFieldVoucherAmount.Value = Convert.ToDouble(dr["colAmountMoney"].ToString()).ToString("#,##0.00");
                }
                else
                {
                    uoHiddenFieldVoucherAmount.Value = "0";
                }

                uoTextBoxVoucher.Text = uoHiddenFieldVoucherAmount.Value;
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
        /// Date Created:   21/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get country and city of seafarer's port of embarkation/disembarkation
        /// -------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private void HotelGetPortDetails()
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all hotel details  
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// -------------------------------------------
        /// Date Modified:  24/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change HotelBLL.HotelVendorGetDetails() to SuperViewBLL.GetVendor("HO",IsAccredited)
        /// </summary>
        private void HotelLoad()
        {
            DataTable dt = null;
            try
            {
                //dt = HotelBLL.HotelVendorGetDetails();
                uoDropDownListHotel.Items.Clear();
                bool IsAccredited = true;

                if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    uoDropDownListCategory.SelectedValue = "2";
                }

                if (uoDropDownListCategory.SelectedValue != "1")
                {
                    IsAccredited = false;
                }

                uoDropDownListHotel.Items.Add(new ListItem("--Select Hotel--", "0"));
                dt = SuperViewBLL.GetVendor("HO", IsAccredited, uoHiddenFieldPortCountry.Value, "0", uoHiddenFieldPort.Value, GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["UserRole"]));
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListHotel.DataSource = dt;
                    uoDropDownListHotel.DataTextField = "colVendorNameVarchar";
                    uoDropDownListHotel.DataValueField = "colVendorIdInt";
                    uoDropDownListHotel.DataBind();

                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                    {
                        if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["UserVendor"])) != null)
                        {
                            uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["UserVendor"]);
                        }
                    }
                }
                else
                {
                    uoDropDownListHotel.DataBind();
                }
                //uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListHotel.SelectedIndex = 1;
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
            //    //dt = HotelBLL.HotelVendorGetDetails();
            //    uoDropDownListHotel.Items.Clear();
            //    bool IsAccredited = true;

            //    if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
            //    {
            //        uoDropDownListCategory.SelectedValue = "2";
            //    }

            //    if (uoDropDownListCategory.SelectedValue != "1")
            //    {
            //        IsAccredited = false;
            //    }

            //    uoDropDownListHotel.Items.Add(new ListItem("--Select Hotel--", "0"));
            //    dt = SuperViewBLL.GetVendor("HO", IsAccredited, uoHiddenFieldPortCountry.Value,"0");
            //    if (dt.Rows.Count > 0)
            //    {
            //        uoDropDownListHotel.DataSource = dt;
            //        uoDropDownListHotel.DataTextField = "colVendorNameVarchar";
            //        uoDropDownListHotel.DataValueField = "colVendorIdInt";
            //        uoDropDownListHotel.DataBind();

            //        if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            //        {
            //            if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["UserVendor"]))!= null)
            //            {
            //                uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["UserVendor"]);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        uoDropDownListHotel.DataBind();
            //    }
            //    //uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                
            //    if (dt.Rows.Count == 1)
            //    {
            //        uoDropDownListHotel.SelectedIndex = 1;
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all hotel room type   
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Dispose DataTable
        /// -------------------------------------------
        /// Date Modified:  25/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change HotelRoomTypeGetDetails to HotelRoomTypeGetDetailsByBranch
        /// -------------------------------------------
        /// Date Modified:  02/01/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add default room type base from rank
        /// </summary>
        private void HotelGetRoomType()
        {                       
            DataTable dt = null;
            IDataReader dr = null;
            try
            {
                //dt = HotelBLL.HotelRoomTypeGetDetailsByBranch(uoDropDownListBranch.SelectedValue);
                dt = HotelBLL.HotelRoomTypeGetDetails();
                uoDropDownListRoomType.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListRoomType.DataSource = dt;
                    uoDropDownListRoomType.DataTextField = "colRoomNameVarchar";
                    uoDropDownListRoomType.DataValueField = "colRoomTypeID";
                   
                }
                uoDropDownListRoomType.Items.Insert(0, new ListItem("--Select Room Type--","0"));
                uoDropDownListRoomType.DataBind();

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListRoomType.SelectedIndex = 1;
                }
                else
                {
                    decimal dStripe = GlobalCode.Field2Decimal(uoHiddenFieldStripe.Value);
                    string sRoomType = "0";
                    dr = SeafarerBLL.GetRoomTypeByStripe(dStripe);
                    if (dr.Read())
                    {
                        sRoomType = dr["RoomID"].ToString();
                    }
                    if (uoDropDownListRoomType.Items.FindByValue(sRoomType) != null)
                    {
                        uoDropDownListRoomType.SelectedValue = sRoomType;
                    }
                    //GetNumberOfRoomsAvailableByHotelAndLocation();                    
                    HotelContractLoad(uoDropDownListBranch.SelectedValue, uoDropDownListRoomType.SelectedValue);
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
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   05/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Load meals
        /// </summary>
        /// 
        //private void HotelGetMeals()
        //{           
        //    DataTable BfastDataTable = null;
        //    DataTable LunchDataTable = null;
        //    DataTable DinnerDataTable = null;
        //    DataTable LunchDinnerDataTable = null;
        //    try
        //    {
        //        BfastDataTable = HotelBLL.HotelMealsGetByBranch(uoDropDownListBranch.SelectedValue, "1", "0");
        //        uoDropDownListBreakfast.Items.Clear();
        //        uoDropDownListBreakfast.Items.Add(new ListItem("--Select Meals--","0"));
        //        uoDropDownListBreakfast.DataSource = BfastDataTable;
        //        uoDropDownListBreakfast.DataBind();
        //        if (BfastDataTable.Rows.Count == 1)
        //        {
        //            uoDropDownListBreakfast.SelectedIndex = 1;
        //        }

        //        LunchDataTable = HotelBLL.HotelMealsGetByBranch(uoDropDownListBranch.SelectedValue, "2", "0");
        //        uoDropDownListLunch.Items.Clear();
        //        uoDropDownListLunch.Items.Add(new ListItem("--Select Meals--", "0"));
        //        uoDropDownListLunch.DataSource = LunchDataTable;
        //        uoDropDownListLunch.DataBind();
        //        if (LunchDataTable.Rows.Count == 1)
        //        {
        //            uoDropDownListLunch.SelectedIndex = 1;
        //        }

        //        DinnerDataTable = HotelBLL.HotelMealsGetByBranch(uoDropDownListBranch.SelectedValue, "3", "0");
        //        uoDropDownListDinner.Items.Clear();
        //        uoDropDownListDinner.Items.Add(new ListItem("--Select Meals--", "0"));
        //        uoDropDownListDinner.DataSource = DinnerDataTable;
        //        uoDropDownListDinner.DataBind();
        //        if (DinnerDataTable.Rows.Count == 1)
        //        {
        //            uoDropDownListDinner.SelectedIndex = 1;
        //        }

        //        //LunchDinnerDataTable = HotelBLL.HotelMealsGetByBranch(uoDropDownListBranch.SelectedValue, "2", "3");                
                
        //        uoDropDownListLunchDinner.Items.Clear();
        //        uoDropDownListLunchDinner.Items.Add(new ListItem("--Select Meals--", "0"));

        //        ListItem item = new ListItem();                
        //        foreach (DataRow row in LunchDataTable.Rows)
        //        {
        //            item = new ListItem(row["colMealNameVarchar"].ToString(), row["colMealIDInt"].ToString());
        //            item.Attributes["OptionGroup"] = "Lunch";
        //            uoDropDownListLunchDinner.Items.Add(item);
        //        }
        //        foreach (DataRow row in DinnerDataTable.Rows)
        //        {
        //            item = new ListItem(row["colMealNameVarchar"].ToString(), row["colMealIDInt"].ToString());
        //            item.Attributes["OptionGroup"] = "Dinner";
        //            uoDropDownListLunchDinner.Items.Add(item);
        //        }

        //        //uoDropDownListLunchDinner.DataSource = LunchDinnerDataTable;
        //        uoDropDownListLunchDinner.DataBind();
        //        //if (LunchDinnerDataTable.Rows.Count == 1)
        //        //{
        //        //    uoDropDownListLunchDinner.SelectedIndex = 1;
        //        //}


                
        //        //ChangeToUpperCase(uoDropDownListBreakfast);
        //        //ChangeToUpperCase(uoDropDownListLunch);
        //        //ChangeToUpperCase(uoDropDownListDinner);
        //        //ChangeToUpperCase(uoDropDownListLunchDinner);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally 
        //    {
        //        if (BfastDataTable != null)
        //        {
        //            BfastDataTable.Dispose();
        //        }
        //        if (LunchDataTable != null)
        //        {
        //            LunchDataTable.Dispose();
        //        }
        //        if (DinnerDataTable != null)
        //        {
        //            DinnerDataTable.Dispose();
        //        }
        //        if (LunchDinnerDataTable != null)
        //        {
        //            LunchDinnerDataTable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page        
        /// </summary>
        private void OpenParentPage()
        {          
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Close this page and update parent page        
        /// </summary>
        private void OpenParentPagePending()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotelPending\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created: 08/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {           
            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            
            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        //private void HotelValidationSettings()
        //{
        //    if (uoCheckBoxBreakfast.Checked)
        //    {
        //        RequiredFieldValidator_Breakfast.Enabled = true;
        //    }
        //    else
        //    {
        //        RequiredFieldValidator_Breakfast.Enabled = false;
        //    }
        //    if (uoCheckBoxLunch.Checked)
        //    {
        //        RequiredFieldValidator_Lunch.Enabled = true;
        //    }
        //    else
        //    {
        //        RequiredFieldValidator_Lunch.Enabled = false;
        //    }
        //    if (uoCheckBoxDinner.Checked)
        //    {
        //        RequiredFieldValidator_Dinner.Enabled = true;
        //    }
        //    else
        //    {
        //        RequiredFieldValidator_Dinner.Enabled = false;
        //    }
        //    if (uoCheckBoxLunchDinner.Checked)
        //    {
        //        RequiredFieldValidator_LunchDinner.Enabled = true;
        //    }
        //    else
        //    {
        //        RequiredFieldValidator_LunchDinner.Enabled = false;
        //    }
        //}
        /// <summary>
        /// Date Created:   07/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Select country list by vendor id   
        /// ------------------------------------------------            
        /// </summary>
        private void HotelGetCountryList()
        {            
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListHotel.SelectedValue != "")
                {
                    VendorID = Convert.ToInt32(uoDropDownListHotel.SelectedValue);
                }

                uoDropDownListCountry.Items.Clear();
                uoDropDownListCountry.Items.Add(new ListItem("--Select Country--", "0"));
                dt = HotelBLL.CountryListByVendorID(VendorID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCountry.DataSource = dt;
                    uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                    uoDropDownListCountry.DataValueField = "colCountryIDInt";
                    uoDropDownListCountry.DataBind();
                    if (uoHiddenFieldPortCountry.Value != "0")
                    {
                        if (uoDropDownListCountry.Items.FindByValue(uoHiddenFieldPortCountry.Value) != null)
                        {
                            uoDropDownListCountry.SelectedValue = uoHiddenFieldPortCountry.Value;
                        }
                    }
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                    {
                        if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(Session["UserCountry"])) != null)
                        {
                            uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(Session["UserCountry"]);
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
        /// Date Created:   07/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Select city list by vendor and country id   
        /// ------------------------------------------------            
        /// </summary>
        private void HotelGetCityList()
        {          
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListHotel.SelectedValue != "")
                {
                    VendorID = Convert.ToInt32(uoDropDownListHotel.SelectedValue);
                }

                int CountryID = 0;
                if (uoDropDownListCountry.SelectedValue != "")
                {
                    CountryID = Convert.ToInt32(uoDropDownListCountry.SelectedValue);
                }

                uoDropDownListCity.Items.Clear();
                uoDropDownListCity.Items.Add(new ListItem("--Select City--", "0"));
                dt = VehicleBLL.CityListByVendorCountryID(VendorID, CountryID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataSource = dt;
                    uoDropDownListCity.DataTextField = "CityName";
                    uoDropDownListCity.DataValueField = "CityId";
                    uoDropDownListCity.DataBind();
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                    {
                        if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(Session["UserCity"])) != null)
                        {
                            uoDropDownListCity.SelectedValue = GlobalCode.Field2String(Session["UserCity"]);
                        }
                    }
                }
                else
                {
                    uoDropDownListCity.DataBind();
                }

                //uoDropDownListCity.Items.Insert(0, (new ListItem("--Select City--", "0")));
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select city list by vendor ID   
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// -------------------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change CityListByVendorID to GetVendorBranch
        /// </summary>
        private void HotelGetVendorBranch()
        {
            DataTable dt = null;
            try
            {
                int HotelID = 0;
                int CityID = 0;
                if (uoDropDownListHotel.SelectedValue != "0")
                {
                    HotelID = Convert.ToInt32(uoDropDownListHotel.SelectedValue);
                }
                if (uoDropDownListCity.SelectedValue != "0")
                {
                    CityID = Convert.ToInt32(uoDropDownListCity.SelectedValue);
                }

                dt = HotelBLL.GetVendorBranch(HotelID, CityID);
                uoDropDownListBranch.Items.Clear();
                uoDropDownListBranch.Items.Add(new ListItem("--Select Branch--", "0"));
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListBranch.DataSource = dt;
                    uoDropDownListBranch.DataTextField = "colVendorBranchNameVarchar";
                    uoDropDownListBranch.DataValueField = "colBranchIDInt";
                    uoDropDownListBranch.DataBind();
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                    {
                        if (uoDropDownListBranch.Items.FindByValue(GlobalCode.Field2String(Session["UserBranchID"])) != null)
                        {
                            uoDropDownListBranch.SelectedValue = GlobalCode.Field2String(Session["UserBranchID"]);
                        }
                    }
                }
                else
                {
                    uoDropDownListBranch.DataBind();
                }
                //uoDropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListBranch.SelectedIndex = 1;
                    GetVendorHotelDetails();
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
        /// Date Created: 19/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Event notification
        /// ---------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private void HotelEventNotification()
        {
            IDataReader dr = null;
            try
            {                
                int HotelBranchID = 0;
                int CityID = 0;
               
                if (uoDropDownListBranch.SelectedValue != "0")
                {
                    HotelBranchID = Convert.ToInt32(uoDropDownListBranch.SelectedValue);
                }

                if (uoDropDownListCity.SelectedValue != "0")
                {
                    CityID = Convert.ToInt32(uoDropDownListCity.SelectedValue);
                }
                               
                //OnOffDate = Convert.ToDateTime(uoHiddenFieldOnOffDate.Value);

                dr = HotelBLL.GetEventNotification(HotelBranchID, CityID, uoHiddenFieldOnOffDate.Value);

                if (dr.Read())
                {
                    uoLabel.Visible = true;
                    uoLinkButtonEvent.Visible = true;
                    uoLinkButtonEvent.Text = dr["EVENT"].ToString();                   
                }
                else
                {
                    uoLabel.Visible = false;
                    uoLinkButtonEvent.Visible = false;
                    uoLinkButtonEvent.Text = ""; 
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
        /// Date Created:   08/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get contract details by Vendor and Room selected
        /// -------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ---------------------------------------------------------------
        /// Date Modified: 02/01/2011
        /// Modified By:   Josephine Gad
        /// (description)  Change vendorID to BranchID
        /// </summary>
        /// <param name="vendorID"></param>
        /// <param name="RoomID"></param>
        private void HotelContractLoad(string branchID, string RoomID)
        {
            IDataReader dr = null;
            try 
            {
                dr = ContractBLL.GetVendorHotelBranchContractByRoomType(branchID, RoomID);
                if (dr.Read())
                {
                    uoCheckBoxBreakfast.Checked = (bool)dr["colBreakfastBit"];
                    uoCheckBoxLunch.Checked = (bool)dr["colLunchBit"];
                    uoCheckBoxDinner.Checked = (bool)dr["colDinnertBit"];
                    uoCheckBoxLunchDinner.Checked = (bool)dr["colLunchORDinnerBit"];
                    if (!uoCheckBoxShuttle.Checked)
                    {
                        uoCheckBoxShuttle.Checked = (bool)dr["colWithShuttleBit"];
                    }
                    uoHiddenFieldContractId.Value = dr["colContractIdInt"].ToString();
                }
                else
                {
                    uoCheckBoxBreakfast.Checked = false;
                    uoCheckBoxLunch.Checked = false;
                    uoCheckBoxDinner.Checked = false;
                    uoCheckBoxLunchDinner.Checked = false;
                    //uoCheckBoxShuttle.Checked = false;
                    uoHiddenFieldContractId.Value = null;
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
        /// Date Created:   21/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Enable/Disable dropdown list for vehicle vendor
        /// </summary>
        private void EnableDisableControl()
        {
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            {
                uoDropDownListCategory.Enabled = false;
                uoDropDownListHotel.Enabled = false;
                uoDropDownListCountry.Enabled = false;
                uoDropDownListCity.Enabled = false;
                uoDropDownListBranch.Enabled = false;
            }
            else
            {
                uoDropDownListCategory.Enabled = true;
                uoDropDownListHotel.Enabled = true;
                uoDropDownListCountry.Enabled = true;
                uoDropDownListCity.Enabled = true;
                uoDropDownListBranch.Enabled = true;
            }

            if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
            {
                uoDropDownListCategory.Enabled = false;
            }
        }
        /// <summary>
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Hotel send email
        /// </summary>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        private void HotelSendEmail(string sSubject, string sMessage)
        {
            //string sBody;
            //DataTable dt = null;
            //try
            //{
            //    dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, "0", uoDropDownListCountry.SelectedValue);
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
            //        sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
            //        sBody += "<br/><br/>Kindly approve.<br/>";
            //        sBody += "<br/><br/>Thank you.";
            //        sBody += "<br/><br/><i>Auto generated email.</i>";
            //        sBody += "</TD></TR></TABLE>";

            //        CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
            //    }
            //    //Email 24*7
            //    dt = UserAccountBLL.GetUserEmail(TravelMartVariable.Role24x7, "0", uoDropDownListCountry.SelectedValue);
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
            //        sBody += "Dear " + "Admin" + ", <br/><br/> " + sMessage;
            //        sBody += "<br/><br/>Kindly approve.<br/>";
            //        sBody += "<br/><br/>Thank you.";
            //        sBody += "<br/><br/><i>Auto generated email.</i>";
            //        sBody += "</TD></TR></TABLE>";

            //        CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
            //    }
            //    //Email Hotel specialist of the country affected
            //    dt = new DataTable();
            //    dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelSpecialist, "0", uoDropDownListCountry.SelectedValue);
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
            //        sBody += "Dear " + TravelMartVariable.RoleHotelSpecialist + ", <br/><br/> " + sMessage;
            //        sBody += "<br/><br/>Kindly approve.<br/>";
            //        sBody += "<br/><br/>Thank you.";
            //        sBody += "<br/><br/><i>Auto generated email.</i>";
            //        sBody += "</TD></TR></TABLE>";

            //        CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
            //    }
            //    //Email Hotel vendor
            //    dt = new DataTable();
            //    dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelVendor, uoDropDownListBranch.SelectedValue, uoDropDownListCountry.SelectedValue);
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
            //        sBody += "Dear " + TravelMartVariable.RoleHotelVendor + ", <br/><br/> " + sMessage;
            //        sBody += "<br/><br/>Thank you.";
            //        sBody += "<br/><br/><i>Auto generated email.</i>";
            //        sBody += "</TD></TR></TABLE>";

            //        CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
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
        /// Date Created: 24/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Room block count
        /// ---------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private Boolean HotelRoomBlockCountZero()
        {
            IDataReader dr = null;            
            try
            {
                int HotelBranchID = 0;
                int RoomTypeID = 0;
                string CheckInDate;
                int Duration;
                int ContractRoomBlocks;
                int OverrideRoomBlocks;      

                if (uoDropDownListBranch.SelectedValue != "0")
                {
                    HotelBranchID = Convert.ToInt32(uoDropDownListBranch.SelectedValue);
                }

                if (uoDropDownListRoomType.SelectedValue != "0")
                {
                    RoomTypeID = Convert.ToInt32(uoDropDownListRoomType.SelectedValue);
                }
                
                CheckInDate = uoTextBoxCheckInDate.Text;

                Duration = (uoTextBoxNoOfdays.Text == "" ? 1 : Convert.ToInt32(uoTextBoxNoOfdays.Text));

                //GetActiveHotelContractID(HotelBranchID, RoomTypeID, CheckInDate, Duration, GlobalCode.Field2String(Session["UserName"]));

                //get contract room block availability
                if ((Request.QueryString["Add"] == "1") || (uoHiddenFieldContractId.Value == null || uoHiddenFieldContractId.Value==""))
                {
                    dr = HotelBLL.GetContractRoomBlockCount(HotelBranchID, RoomTypeID, CheckInDate, Duration, GlobalCode.Field2String(Session["UserName"]), uoHiddenFieldRole.Value);

                    if (dr.Read())
                    {
                        ContractRoomBlocks = Convert.ToInt32(dr["AvailableBlocks"].ToString());

                        if (ContractRoomBlocks == 0 || ContractRoomBlocks < 0)
                        {
                            //get override room block availability
                            dr = HotelBLL.GetOverrideRoomBlockCount(HotelBranchID, RoomTypeID, CheckInDate, Duration, GlobalCode.Field2String(Session["UserName"]), uoHiddenFieldRole.Value);
                            uoHiddenFieldContractId.Value = null;
                            if (dr.Read())
                            {
                                OverrideRoomBlocks = Convert.ToInt32(dr["AvailableBlocks"].ToString());
                                if (OverrideRoomBlocks == 0 || OverrideRoomBlocks < 0)
                                {
                                    uoLabelRoomAmount.Visible = true;
                                    uoTextBoxRoomAmount.Visible = true;
                                    uoCheckBoxTax.Visible = true;
                                    bValidation = true;

                                }
                                else
                                {
                                    uoLabelRoomAmount.Visible = false;
                                    uoTextBoxRoomAmount.Visible = false;
                                    uoCheckBoxTax.Visible = false;
                                    uoLabelTaxAmount.Visible = false;
                                    uoTextBoxTaxAmount.Visible = false;
                                    uoCheckBoxTax.Visible = false;
                                    uoTextBoxRoomAmount.Text = "";
                                    uoTextBoxTaxAmount.Text = "";
                                    bValidation = false;

                                }
                            }
                            else
                            {
                                uoLabelRoomAmount.Visible = true;
                                uoTextBoxRoomAmount.Visible = true;
                                uoCheckBoxTax.Visible = true;
                                bValidation = true;
                                uoHiddenFieldContractId.Value = null;
                            }
                        }
                        else
                        {
                            uoLabelRoomAmount.Visible = false;
                            uoTextBoxRoomAmount.Visible = false;
                            uoCheckBoxTax.Visible = false;
                            uoLabelTaxAmount.Visible = false;
                            uoTextBoxTaxAmount.Visible = false;
                            uoCheckBoxTax.Checked = false;
                            uoTextBoxRoomAmount.Text = "";
                            uoTextBoxTaxAmount.Text = "";
                            bValidation = false;
                            uoHiddenFieldContractId.Value = dr["colContractIdInt"].ToString();
                        }
                    }
                    else
                    {
                        uoLabelRoomAmount.Visible = true;
                        uoTextBoxRoomAmount.Visible = true;
                        uoCheckBoxTax.Visible = true;
                        bValidation = true;
                        uoHiddenFieldContractId.Value = null;
                    }
                }
                else
                { }
                return bValidation;
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
        /// Date Created: 24/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get active hotel contract id
        /// ---------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private void GetActiveHotelContractID()
        {
            IDataReader dr = null;
            try
            {
                dr = HotelBLL.GetActiveHotelContractID(uoDropDownListHotel.SelectedValue,uoDropDownListRoomType.SelectedValue,uoTextBoxCheckInDate.Text,
                    uoTextBoxNoOfdays.Text, GlobalCode.Field2String(Session["UserName"]));

                if (dr.Read())
                {
                    uoHiddenFieldContractId.Value = dr["colContractIdInt"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   27/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vendor details
        /// ---------------------------------------------
        /// </summary>
        private void GetVendorHotelDetails()
        {
            IDataReader dr = null;
            try
            {
                //dr = VendorMaintenanceBLL.vendorBranchMaintenanceInformation(int.Parse(uoDropDownListBranch.SelectedValue));
                if (dr.Read())
                {
                    if (!uoCheckBoxShuttle.Checked)
                    {
                        uoCheckBoxShuttle.Checked = (bool)dr["colWithShuttleBit"];
                    }
                    else
                    {
                        uoCheckBoxShuttle.Checked = false;
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
        /// Date Created:   16/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get contract by date
        /// ---------------------------------------------
        /// </summary>
        private void BindGridViewDate(bool IsNew)
        {
            DataTable dt = null;
            
            try
            {
                string sIDBigint = "";
                if (Request.QueryString["ID"] != null)
                {
                    sIDBigint = Request.QueryString["ID"].ToString();
                }
                string sSeqNo = "";
                if (Request.QueryString["SN"] != null)
                {
                    sSeqNo = Request.QueryString["SN"].ToString();
                }

                string sTransHotelID = uoHiddenFieldHotelBookingID.Value;
                string sPending = uoHiddenFieldPendingId.Value;

                DateTime dtDateFrom = CommonFunctions.ConvertDateByFormat(uoTextBoxCheckInDate.Text);
                DateTime dtDateTo = dtDateFrom.AddDays(double.Parse(uoTextBoxNoOfdays.Text) - 1);
                //bool isNew = false;

                //if (Request.QueryString["Add"] != null)
                //{
                //    if (Request.QueryString["Add"] == "1")
                //    {
                //        isNew = true;
                //    }
                //}

                dt = ContractBLL.GetHotelContractOverrideByDate(sIDBigint, sSeqNo, sTransHotelID, sPending,
                    uoHiddenFieldUser.Value, uoDropDownListBranch.SelectedValue, dtDateFrom, dtDateTo,
                    "", uoDropDownListRoomType.SelectedValue, IsNew);

                uoGridViewDate.DataSource = dt;
                uoGridViewDate.Columns[0].Visible = true;
                uoGridViewDate.Columns[1].Visible = true;

                uoGridViewDate.Columns[4].HeaderText = "Room Rate " + "(" + uoTextBoxCurrency.Text + ")";
                uoGridViewDate.DataBind();
                uoGridViewDate.Columns[0].Visible = false;
                uoGridViewDate.Columns[1].Visible = false;

                foreach (GridViewRow gridRow in uoGridViewDate.Rows)
                {                   
                    CheckBox uoCheckBoxFromContract = (CheckBox)gridRow.FindControl("uoCheckBoxFromContract");
                    TextBox uoTextBoxRate = (TextBox)gridRow.FindControl("uoTextBoxRate");
                    CheckBox uoCheckBoxTax = (CheckBox)gridRow.FindControl("uoCheckBoxTax");
                    TextBox uoTextBoxTaxPercent = (TextBox)gridRow.FindControl("uoTextBoxTaxPercent");

                    if (!uoCheckBoxFromContract.Checked)
                    {
                        uoTextBoxRate.Enabled = true;
                        uoCheckBoxTax.Enabled = true;
                        uoTextBoxTaxPercent.Enabled = true;
                    }
                    else
                    {
                        uoTextBoxRate.Enabled = false;
                        uoCheckBoxTax.Enabled = false;
                        uoTextBoxTaxPercent.Enabled = false;
                    }
                }

                //dt = ContractBLL.GetHotelContractOverrideByDate
                ////Cache.Remove("HotelDetailsByDate");
                //DataColumn colDetailsIDInt = new DataColumn("colDetailsIDInt", typeof(string));
                //DataColumn colContractIDInt = new DataColumn("colContractIDInt", typeof(string));
                
                //DataColumn colDate = new DataColumn("colDate", typeof(DateTime));
                //DataColumn colIsFromContract = new DataColumn("colIsFromContract", typeof(bool));
                //DataColumn colRoomRate = new DataColumn("colRoomRate", typeof(decimal));
                //DataColumn colIsWithTax = new DataColumn("colIsWithTax", typeof(bool));
                //DataColumn colTaxPercent = new DataColumn("colTaxPercent", typeof(decimal));

                //dt = new DataTable();
                //dt.Columns.Add(colDetailsIDInt);
                //dt.Columns.Add(colContractIDInt);
                
                //dt.Columns.Add(colDate);
                //dt.Columns.Add(colIsFromContract);
                //dt.Columns.Add(colRoomRate);
                //dt.Columns.Add(colIsWithTax);
                //dt.Columns.Add(colTaxPercent);

                //DateTime dtDate = CommonFunctions.ConvertDateByFormat(uoTextBoxCheckInDate.Text);
                //if(uoGridViewDate.Rows.Count == 0)
                //{
                //    for (int i = 1; i <= GlobalCode.Field2TinyInt(uoTextBoxNoOfdays.Text); i++)
                //    {
                //        DataRow r = dt.NewRow();
                //        r["colDetailsIDInt"] = null;
                //        r["colContractIDInt"] = null;
                                             
                //        r["colDate"] = dtDate;
                //        r["colIsFromContract"] = true;
                //        r["colRoomRate"] = DBNull.Value;
                //        r["colIsWithTax"] = false;
                //        r["colTaxPercent"] = DBNull.Value;
                //        dt.Rows.Add(r);
                //        dtDate = dtDate.AddDays(1);
                //    }
                //}
                ////Cache.Insert("HotelDetailsByDate", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                //uoGridViewDate.DataSource = dt;
                //uoGridViewDate.Columns[0].Visible = true;
                //uoGridViewDate.Columns[1].Visible = true;
                
                //uoGridViewDate.DataBind();
                //uoGridViewDate.Columns[0].Visible = false;
                //uoGridViewDate.Columns[1].Visible = false;
                                
                //foreach (GridViewRow gridRow in uoGridViewDate.Rows)
                //{
                //    string sCOntractID = gridRow.Cells[1].Text;
                //    CheckBox uoCheckBoxFromContract = (CheckBox)gridRow.FindControl("uoCheckBoxFromContract");
                //    TextBox uoTextBoxRate = (TextBox)gridRow.FindControl("uoTextBoxRate");
                //    CheckBox uoCheckBoxTax = (CheckBox)gridRow.FindControl("uoCheckBoxTax");
                //    TextBox uoTextBoxTaxPercent = (TextBox)gridRow.FindControl("uoTextBoxTaxPercent");

                //    if (sCOntractID.ToString() == "")
                //    {
                //        uoTextBoxRate.Enabled = true;
                //        uoCheckBoxTax.Enabled = true;
                //        uoTextBoxTaxPercent.Enabled = true;
                //    }
                //    else
                //    {
                //        uoTextBoxRate.Enabled = false;
                //        uoCheckBoxTax.Enabled = false;
                //        uoTextBoxTaxPercent.Enabled = false;
                //    }                    
                //}
                uoHiddenFieldIsValidated.Value = "1";
                uoHiddenFieldBranchID.Value = uoDropDownListBranch.SelectedValue;
                uoHiddenFieldRoomTypeID.Value = uoDropDownListRoomType.SelectedValue;
                uoHiddenFieldCheckinDate.Value = uoTextBoxCheckInDate.Text;
                uoHiddenFieldDuration.Value = uoTextBoxNoOfdays.Text;
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
        /// Date Created:   08/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Hotel booking approval
        /// ---------------------------------------------
        /// </summary>
        /// <param name="pendingID"></param>
        /// <param name="ApproveByString"></param>
        private void ApproveBooking(string pendingID, string ApproveByString)
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelApproveTransaction(pendingID, ApproveByString);
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

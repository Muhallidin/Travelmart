using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using System.Data;
using TRAVELMART.BLL;

using System.IO;

namespace TRAVELMART
{
    public partial class AirEditor : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["sfid"] == null || Session["UserName"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            Session["strSFCode"] = Request.QueryString["sfId"].Trim();
            Session["strSFStatus"] = Request.QueryString["st"];
            Session["strSFSeqNo"] = Request.QueryString["SN"];
            Session["strRecordLocator"] = Request.QueryString["recloc"];
            Session["strTravelLocatorID"] = Request.QueryString["ID"];

            if (Request.QueryString["Add"] != "1")
            {
                uoHiddenFieldAirID.Value = Request.QueryString["aId"];
            }
            
            if (!IsPostBack)
            {
                AirGetTravelDetails();
            }
        }
        protected void uoDropDownListAirStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (uoDropDownListAirStatus.SelectedValue == "Cancelled" || uoDropDownListAirStatus.SelectedValue == "Rebooked")
            //{
            //    uoCheckBoxChargedToCrew.Visible = true;
            //}
            //else
            //{
            //    uoCheckBoxChargedToCrew.Checked = false;
            //    uoCheckBoxChargedToCrew.Visible = false;
            //}
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                bool IsChargedToCrew = uoCheckBoxChargedToCrew.Checked;
                //if (uoDropDownListAirStatus.SelectedValue != "Cancelled" && uoDropDownListAirStatus.SelectedValue != "Rebooked")
                //{
                //    IsChargedToCrew = false;
                //}
                AirBLL.UpdateAirBooking(GlobalCode.Field2Int(Session["strTravelLocatorID"]).ToString(), GlobalCode.Field2Int(Session["strSFSeqNo"]).ToString(), 
                    uoDropDownListAirStatus.SelectedValue,
                    IsChargedToCrew, uoTextBoxRemarks.Text, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Air booking updated.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(Session["strTravelLocatorID"]), GlobalCode.Field2Int(Session["strSFSeqNo"]).ToString(), 
                    strLogDescription, strFunction, Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                OpenParentPage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }

        protected void uoDropDownListAirStatus2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListAirStatus2.SelectedValue == "Cancelled" || uoDropDownListAirStatus2.SelectedValue == "Rebooked")
            {
                uoCheckBoxChargedToCrew2.Visible = true;
            }
            else
            {
                uoCheckBoxChargedToCrew2.Checked = false;
                uoCheckBoxChargedToCrew2.Visible = false;
            }
        }
        protected void uoButtonSave2_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                bool IsChargedToCrew = uoCheckBoxChargedToCrew2.Checked;
                if (uoDropDownListAirStatus2.SelectedValue != "Cancelled" && uoDropDownListAirStatus2.SelectedValue != "Rebooked")
                {
                    IsChargedToCrew = false;
                }
                
                if (Request.QueryString["Add"] == "1")
                {
                    AirBLL.InsertAirBookingOther(GlobalCode.Field2String(Session["TravelRequestID"]), uoTextBoxFlightNo2.Text,
                        uoTextBoxArrivalDatetime2.Text, uoTextBoxDepartureDatetime2.Text,
                        uoTextBoxDepartureLoc2.Text, uoTextBoxArrivalLoc2.Text,
                        uoDropDownListAirline.SelectedValue,
                        uoTextBoxTicketNo2.Text, uoDropDownListAirStatus2.SelectedValue, GlobalCode.Field2String(Session["UserName"]),
                        Session["strSFStatus"].ToString(), uoTextBoxRemarks2.Text, uoCheckBoxChargedToCrew2.Checked);
                }
                else
                { 
                 AirBLL.UpdateAirBookingOther(uoHiddenFieldAirID.Value, uoTextBoxFlightNo2.Text,
                        uoTextBoxArrivalDatetime2.Text, uoTextBoxDepartureDatetime2.Text,
                        uoTextBoxDepartureLoc2.Text, uoTextBoxArrivalLoc2.Text,
                        uoDropDownListAirline.SelectedValue,
                        uoTextBoxTicketNo2.Text, uoDropDownListAirStatus2.SelectedValue, GlobalCode.Field2String(Session["UserName"]),
                        Session["strSFStatus"].ToString(), uoTextBoxRemarks2.Text, uoCheckBoxChargedToCrew2.Checked);                
                }

                if (Request.QueryString["Add"] == "1")
                {
                    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                    strLogDescription = "Air booking added.";
                    strFunction = "uoButtonSave2_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                    //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
                else
                {
                    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                    strLogDescription = "Air booking updated.";
                    strFunction = "uoButtonSave2_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                    //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, Session["UserName"].ToString());
                }
                
                OpenParentPage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        #endregion        

        #region Functions

        /// <summary>
        /// Date Created:   08/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Get flight info of seafarer per leg
        /// -------------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// -------------------------------------------------
        /// <summary>
        private void AirGetTravelDetails()
        {           
            IDataReader dr = null;
            try
            {
                if (Request.QueryString["Add"] == "1")
                {
                    AirGetTravelDetailsOther();                    
                }
                else
                {
                    if (GlobalCode.Field2Int(Session["strTravelLocatorID"]).ToString() != "0")
                    {
                        uoDivSabre.Visible = true;
                        uoDivOther.Visible = false;

                        dr = AirBLL.GetSFAirTravelDetailsById((Session["strTravelLocatorID"]).ToString(), Session["strSFSeqNo"].ToString());

                        if (dr.Read())
                        {
                            uoTextBoxSeafarer.Text = dr["SeafarerName"].ToString();

                            uoTextBoxDepartureDatetime.Text = dr["colDepartureDateTime"].ToString();
                            uoTextBoxDepartureDatetime.Text = DateTime.Parse(uoTextBoxDepartureDatetime.Text).ToString("dd-MMM-yyyy HHmm");
                            uoTextBoxArrivalDatetime.Text = dr["colArrivalDateTime"].ToString();
                            uoTextBoxArrivalDatetime.Text = DateTime.Parse(uoTextBoxArrivalDatetime.Text).ToString("dd-MMM-yyyy HHmm");

                            uoTextBoxAirline.Text = dr["colMarketingAirlineCodeVarchar"].ToString();
                            uoTextBoxFlightNo.Text = dr["colFlightNoVarchar"].ToString();
                            uoTextBoxTicketNo.Text = dr["colTicketNoVarchar"].ToString();
                            
                            //uoCheckBoxChargedToCrew.Visible = false;                 
                            uoTextBoxRemarks.Text = dr["colRemarksVarchar"].ToString();
                            uoTextBoxDepartureLoc.Text = dr["colDepartureAirportLocationCodeVarchar"].ToString();
                            uoTextBoxArrivalLoc.Text = dr["colArrivalAirportLocationCodeVarchar"].ToString();

                            if (dr["colAirStatusVarchar"] != null)
                            {
                                if (dr["colAirStatusVarchar"].ToString() != "")
                                {
                                    uoDropDownListAirStatus.SelectedValue = dr["colAirStatusVarchar"].ToString();
                                    //if (uoDropDownListAirStatus.SelectedValue == "Cancelled" || uoDropDownListAirStatus.SelectedValue == "Rebooked")
                                    //{
                                        //uoCheckBoxChargedToCrew.Visible = true;
                                    if (dr["colIsBilledToCrewBit"] != null)
                                        {
                                            //uoCheckBoxChargedToCrew.Checked = (bool)dr["colIsBilledToCrewBit"];
                                                                                                                             
                                            if (dr.IsDBNull(dr.GetOrdinal("colIsBilledToCrewBit")) == false)
                                            {
                                                uoCheckBoxChargedToCrew.Checked = dr.GetBoolean(dr.GetOrdinal("colIsBilledToCrewBit")); 
                                            }
                                            {
                                                uoCheckBoxChargedToCrew.Checked = false;
                                            }                                            
                                        }
                                    //}
                                }
                            }
                        }
                    }
                    else
                    {
                        AirGetTravelDetailsOther();
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

        private void AirGetTravelDetailsOther()
        {
            IDataReader dr  = null;
            try
            {
                uoDivSabre.Visible = false;
                uoDivOther.Visible = true;

                uoTextBoxSeafarer2.Text = AirGetSeafarerName();
                AirGetAirline();
                if (uoHiddenFieldAirID.Value != "0")
                {
                    dr = AirBLL.GetSFAirTravelDetailsOtherById(uoHiddenFieldAirID.Value);
                    if (dr.Read())
                    {
                        uoTextBoxDepartureDatetime2.Text = dr["colDepartureDateTime"].ToString();
                        //uoTextBoxDepartureDatetime2.Text = DateTime.Parse(uoTextBoxDepartureDatetime2.Text).ToString("dd-MMM-yyyy HHmm");
                        uoTextBoxArrivalDatetime2.Text = dr["colArrivalDateTime"].ToString();
                        //uoTextBoxArrivalDatetime2.Text = DateTime.Parse(uoTextBoxArrivalDatetime2.Text).ToString("dd-MMM-yyyy HHmm");

                        uoDropDownListAirline.SelectedValue = dr["colMarketingAirlineCodeVarchar"].ToString();
                        uoTextBoxFlightNo2.Text = dr["colFlightNoVarchar"].ToString();
                        uoTextBoxTicketNo2.Text = dr["colTicketNoVarchar"].ToString();

                        uoTextBoxRemarks2.Text = dr["colRemarksVarchar"].ToString();

                        uoTextBoxDepartureLoc2.Text = dr["colDepartureAirportLocationCodeVarchar"].ToString();
                        uoTextBoxArrivalLoc2.Text = dr["colArrivalAirportLocationCodeVarchar"].ToString();

                        if (dr["colAirStatusVarchar"] != null)
                        {
                            if (dr["colAirStatusVarchar"].ToString() != "")
                            {
                                uoDropDownListAirStatus2.SelectedValue = dr["colAirStatusVarchar"].ToString();
                                if (uoDropDownListAirStatus2.SelectedValue == "Cancelled" || uoDropDownListAirStatus2.SelectedValue == "Rebooked")
                                {
                                    uoCheckBoxChargedToCrew2.Visible = true;
                                    if (dr["colIsBilledToCrewBit"] != null)
                                    {
                                        uoCheckBoxChargedToCrew2.Checked = (bool)dr["colIsBilledToCrewBit"];
                                    }
                                }
                            }
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
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 07/19/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page
        /// ----------------------------------------------------
        /// </summary>
        private void OpenParentPage()
        {                        
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupAir\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created:   26/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message
        /// ---------------------------------
        /// </summary>
        private void AlertMessage(string s)
        {            
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created:   14/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer's name
        /// </summary>
        /// <returns></returns>
        private string AirGetSeafarerName()
        {
            IDataReader dr = null;
            string SeafarernameString = "";
            try 
            {               
                dr = SeafarerBLL.SeafarerGetDetails(Session["strSFCode"].ToString(),
                    GlobalCode.Field2String(Session["TravelRequestID"]), GlobalCode.Field2String(Session["ManualRequestID"]), true);
                if (dr.Read())
                {
                    SeafarernameString = dr["NAME"].ToString();
                }
                return SeafarernameString;
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

        private void AirGetAirline()
        {
            DataTable AirlineDataTable = null;
            try
            {
                AirlineDataTable = AirBLL.GetAirline();

                uoDropDownListAirline.Items.Clear();
                uoDropDownListAirline.Items.Add(new ListItem("--Select Airline--", ""));
                if (AirlineDataTable.Rows.Count > 0)
                {
                    uoDropDownListAirline.DataSource = AirlineDataTable;
                    uoDropDownListAirline.DataTextField = "Airline";
                    uoDropDownListAirline.DataValueField = "colAirlineCodeVarchar";
                }
                uoDropDownListAirline.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (AirlineDataTable != null)
                {
                    AirlineDataTable.Dispose();
                }
            }
        }
        #endregion
    }
}

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
    public partial class HotelRoomEmergencyEdit : System.Web.UI.Page
    {
        DashboardBLL dbBLL = new DashboardBLL();

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (GlobalCode.Field2String(Session["UserName"]) == "")
                {
                    Response.Redirect("Login.aspx");
                }
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                if (Request.QueryString["bID"] != null)
                {
                    uoHiddenFieldBranchID.Value = Request.QueryString["bID"].ToString();
                }
                if (Request.QueryString["rID"] != null)
                {
                    //uoHiddenFieldRoomID.Value = Request.QueryString["rID"].ToString();
                    uoDropDownListRoomType.SelectedValue = Request.QueryString["rID"].ToString();
                    uoHiddenFieldRoomTypeID.Value = Request.QueryString["rID"].ToString();
                }
                if (Request.QueryString["dt"] != null)
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                    uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                }
                HotelRoomEmergencyLogAuditTrail();
                GetCurrency();
                GetHotelRoomEmergency();
            }
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveHotelRoomEmergency();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   10/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Save Emergency room blocks
        /// ---------------------------------------------------------------
        /// Date Modified: 03/03/2012
        /// Modified By:   Gabriel Oquialda
        /// (description)  Added validation for emergency room block(s) versus the total no. of emergency booking(s)
        /// </summary>
        private void SaveHotelRoomEmergency()
        {
            IDataReader drEmergencyTotalBookings = null;

            Int32 EmergencyTotalBookings = 0;            
            Decimal EmergencyTotal = 0;

            try
            {
                Int32 EmergencyRoomCount = GlobalCode.Field2Int(uoTextBoxEmergencyRoomCount.Text);

                String Date = string.Format("{0:dd-MMM-yyyy}", uoHiddenFieldDate.Value);
                Int32 BranchID = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
                Int32 RoomTypeID = GlobalCode.Field2Int(uoHiddenFieldRoomTypeID.Value);                

                drEmergencyTotalBookings = HotelBLL.GetEmergencyTotalBookings(Date, BranchID, RoomTypeID);

                if (drEmergencyTotalBookings.Read())
                {
                    EmergencyTotalBookings = GlobalCode.Field2Int(drEmergencyTotalBookings["colEmergencyTotalBookingInt"].ToString());
                }

                if(RoomTypeID == 1)
                {
                    EmergencyTotal = EmergencyTotalBookings;
                }
                else if (RoomTypeID == 2)
                {
                    EmergencyTotal = GlobalCode.Field2Decimal(EmergencyTotalBookings) / 2;
                }

                if (EmergencyRoomCount < EmergencyTotal)
                {
                    AlertMessage("Emergency room block(s) must not be less than the total no. of emergency booking(s)!");
                }
                else
                {
                    DataTable dtEmergency = null;

                    string strLogDescription;
                    string strFunction;

                    DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);
                    dtEmergency = HotelBLL.SaveHotelRoomEmergencyByBranch(uoHiddenFieldBranchID.Value, dDate,
                        uoTextBoxEmergencyAmount.Text, uoDropDownListCurrency.SelectedValue, uoTextBoxEmergencyTax.Text,
                        uoCheckEmergencyBoxTaxInclusive.Checked, uoDropDownListRoomType.SelectedValue, uoTextBoxEmergencyRoomCount.Text,
                        uoHiddenFieldUser.Value);

                    if (Convert.ToInt32(dtEmergency.Rows[0]["dtReturnType"]) == 0)
                    {
                        //Insert log audit trail (Gabriel Oquialda - 22/02/2012)
                        strLogDescription = "Hotel room emergency added.";
                        strFunction = "SaveHotelRoomEmergency";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dtEmergency.Rows[0]["dtHotelRoomID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }
                    else if (Convert.ToInt32(dtEmergency.Rows[0]["dtReturnType"]) == 1)
                    {
                        //Insert log audit trail (Gabriel Oquialda - 22/02/2012)
                        strLogDescription = "Hotel room emergency updated.";
                        strFunction = "SaveHotelRoomEmergency";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dtEmergency.Rows[0]["dtHotelRoomID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }

                    OpenParentPage();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drEmergencyTotalBookings != null)
                {
                    drEmergencyTotalBookings.Close();
                    drEmergencyTotalBookings.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   10/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Get hotel room blocks from emergency room blocks           
        /// ---------------------------------------------------       
        /// Date Modified:   16/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Replace IDataReader with List
        /// ---------------------------------------------------  
        /// </summary>
        private void GetHotelRoomEmergency()
        {
            List<HotelRoomBlocksEmergencyDTO> list = null;
            try
            {
                list = HotelBLL.GetHotelRoomEmergencyByBranch(uoHiddenFieldBranchID.Value, 
                    uoDropDownListRoomType.SelectedValue, uoHiddenFieldDate.Value);
                if (list.Count > 0)
                {
                    uoLabelBranchName.Text = list[0].BranchName;//dr["BranchName"].ToString();
                   
                    if (uoHiddenFieldDate.Value != "")
                    {
                        DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);
                        uoLabelDate.Text = dDate.ToString("dd-MMM-yyyy (dddd)");
                    }
                    uoTextBoxEmergencyRoomCount.Text = list[0].RoomBlocks;//dr["EmergencyRoomBlocks"].ToString();
                    Decimal fAmount = GlobalCode.Field2Decimal(list[0].Rate);//GlobalCode.Field2Decimal(dr["EmergencyRate"].ToString());
                    uoTextBoxEmergencyAmount.Text = fAmount.ToString("0.00");
                    //if (dr["EmergencyCurrentcyID"] == null)
                    //{
                    //    uoDropDownListCurrency.SelectedValue = dr["OverrideCurrencyID"].ToString();
                    //}
                    //else
                    //{
                    //    uoDropDownListCurrency.SelectedValue = dr["EmergencyCurrentcyID"].ToString();
                    //}
                    uoDropDownListCurrency.SelectedValue = list[0].Currency;//dr["EmergencyCurrentcyID"].ToString();
                    uoCheckEmergencyBoxTaxInclusive.Checked = list[0].IsTaxInclusive;//(bool)dr["EmergencyTaxInclusive"];
                    uoTextBoxEmergencyTax.Text = list[0].Tax;//dr["EmergencyTaxPercent"].ToString();

                    //Decimal fTax = GlobalCode.Field2Decimal(list[0].Tax);
                    //uoTextBoxEmergencyTax.Text = fTax.ToString("0.0"); //dr["EmergencyTaxPercent"].ToString();

                    if (!uoCheckEmergencyBoxTaxInclusive.Checked)
                    {
                        uoTextBoxEmergencyTax.Enabled = false;
                        uoTextBoxEmergencyTax.Text = "0.0";
                    }
                    else
                    {
                        uoTextBoxEmergencyTax.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }                
            }
        }
        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all currency  
        /// </summary>
        private void GetCurrency()
        {
            DataTable dt = null;
            try
            {
                dt = ContractBLL.CurrencyLoad();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCurrency.DataSource = dt;
                    uoDropDownListCurrency.DataTextField = "colCurrencyNameVarchar"; 
                    uoDropDownListCurrency.DataValueField = "colCurrencyIDInt";
                    uoDropDownListCurrency.DataBind();
                }
                else
                {
                    uoDropDownListCurrency.DataBind();
                }
                uoDropDownListCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));
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
        private void OpenParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\").val(\"1\"); ";            
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelRoomEmergencyLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Hotel room emergency editor. (Edit)";
            strFunction = "HotelRoomEmergencyLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion       
    }
}

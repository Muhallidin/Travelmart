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
    public partial class HotelRoomEmergencyEdit2 : System.Web.UI.Page
    {
        DashboardBLL dbBLL = new DashboardBLL();

        #region "Events"
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// --------------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {     
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) =="")
            {
                closePage();
            }
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            if (!IsPostBack)
            {
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
                if (GlobalCode.Field2String(Request.QueryString["dt"]) != "")
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                    uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                }
                else
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
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
        /// Author:Charlene Remotigue
        /// Date Created: 10/04/2012
        /// Description: close page on expire
        /// </summary>
        private void closePage()
        {
            string str = "";
            if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Dashboard"))
            {
                str = "ctl00_NaviPlaceHolder_uoHiddenFieldPopupHotel";
            }
            else
            {
                str = "ctl00_BodyPlaceHolder_uoHiddenFieldPopupHotel";
            }
            string sScript = "<script language='javascript'>";

            sScript += " window.parent.$(\"#" + str + "\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
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
                Int32 EmergencyRoomBlock = GlobalCode.Field2Int(uoLabelEmergencyRoomBlocks.Text) + GlobalCode.Field2Int(uoTextBoxEmergencyRoomCount.Text);

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

                if (EmergencyRoomBlock < EmergencyTotal)
                {
                    AlertMessage("Emergency room block(s) must not be less than the total no. of emergency booking(s)!");
                }
                else
                {
                    DataTable dtEmergency = null;

                    string strLogDescription;
                    string strFunction;

                    DateTime dDate = CommonFunctions.ConvertDateByFormat(Date);
                    dtEmergency = HotelBLL.SaveHotelRoomEmergencyByBranch(BranchID.ToString(), dDate,
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
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);
                    }
                    else if (Convert.ToInt32(dtEmergency.Rows[0]["dtReturnType"]) == 1)
                    {
                        //Insert log audit trail (Gabriel Oquialda - 22/02/2012)
                        strLogDescription = "Hotel room emergency updated.";
                        strFunction = "SaveHotelRoomEmergency";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dtEmergency.Rows[0]["dtHotelRoomID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);
                    }

                    OpenParentPage("Room blocks successfully saved!");
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
                    //uoTextBoxEmergencyRoomCount.Text = list[0].RoomBlocks;
                    uoLabelEmergencyRoomBlocks.Text = list[0].RoomBlocks;
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
        private void OpenParentPage(string s)
        {
            string str = "";
            if (GlobalCode.Field2String(Session["strPrevPage"]).Contains("Dashboard")) //|| GlobalCode.Field2String(Session["strPrevPage"]).Contains("Overflow") || GlobalCode.Field2String(Session["strPrevPage"]).Contains("Exception"))
            {
                str = "ctl00_NaviPlaceHolder_uoHiddenFieldPopupHotel";
            }
            else
            {
                str = "ctl00_BodyPlaceHolder_uoHiddenFieldPopupHotel";
            }
            string sScript = "<script language='javascript'>";
            sScript += "alert('" + s + "');";
            sScript += " window.parent.$(\"#" + str + "\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
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
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        #endregion       
    }
}

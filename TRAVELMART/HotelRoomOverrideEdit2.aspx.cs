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
    public partial class HotelRoomOverrideEdit2 : System.Web.UI.Page
    {
        DashboardBLL dbBLL = new DashboardBLL();

        #region "Events"
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// ----------------------------------------------------
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
                    uoDropDownListRoomType.SelectedValue = Request.QueryString["rID"].ToString();
                }
                if (Request.QueryString["dt"] != null && GlobalCode.Field2String(Request.QueryString["dt"]) != "")
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                    uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                }
                else
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToShortDateString();
                }

                if (Request.QueryString["rc"] != null)
                {
                    uoHiddenFieldReservedCount.Value = Request.QueryString["rc"].ToString();
                }

                HotelRoomOverrideLogAuditTrail();
                GetCurrency();
                GetHotelRoomOverride();
            }
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveHotelRoomOverride();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   27/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Save override room blocks
        /// ---------------------------------------------------------------
        /// Date Modified: 03/03/2012
        /// Modified By:   Gabriel Oquialda
        /// (description)  Added validation for total room block(s) versus the no. of reserved room
        /// </summary>
        private void SaveHotelRoomOverride() 
        {
            try
            {
                DataTable dtOverride = null;

                string strLogDescription;
                string strFunction;

                DateTime dDate = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
                dtOverride = HotelBLL.SaveHotelRoomOverrideByBranch(uoHiddenFieldBranchID.Value, dDate,
                    uoTextBoxOverrideAmount.Text, uoDropDownListCurrency.SelectedValue, uoTextBoxOverrideTax.Text,
                    uoCheckOverrideBoxTaxInclusive.Checked, uoDropDownListRoomType.SelectedValue, uoTextBoxOverrideRoomCount.Text,
                    uoHiddenFieldUser.Value);

                if (Convert.ToInt32(dtOverride.Rows[0]["dtReturnType"]) == 0)
                {
                    //Insert log audit trail (Gabriel Oquialda - 22/02/2012)
                    strLogDescription = "Hotel room override added.";
                    strFunction = "SaveHotelRoomOverride";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dtOverride.Rows[0]["dtHotelRoomID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);
                }
                else if (Convert.ToInt32(dtOverride.Rows[0]["dtReturnType"]) == 1)
                {
                    //Insert log audit trail (Gabriel Oquialda - 22/02/2012)
                    strLogDescription = "Hotel room override updated.";
                    strFunction = "SaveHotelRoomOverride";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dtOverride.Rows[0]["dtHotelRoomID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);
                }
                //AlertMessage();
                OpenParentPage("Room blocks successfully saved!");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   25/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel room blocks from contract and override
        /// ---------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ---------------------------------------------------------------
        /// Date Modified: 16/02/2012
        /// Modified By:   Josephine Gad
        /// (description)  replace datareader with List
        /// </summary>
        private void GetHotelRoomOverride()
        {
            List<HotelRoomBlocksDTO> listHotelRoomBlocks = null;           
            try
            {
                listHotelRoomBlocks = HotelBLL.GetHotelRoomOverrideByBranch(uoHiddenFieldBranchID.Value, uoDropDownListRoomType.SelectedValue, uoHiddenFieldDate.Value);
               
                if (listHotelRoomBlocks.Count > 0)
                {
                    bool isWithOverflow = listHotelRoomBlocks[0].IsWithOverflow;
                    //if (isWithOverflow)
                    //{
                    //    uoButtonSave.Attributes.Add("onclick", "return SaveRecord(1);");
                    //}

                    uoLabelBranchName.Text = listHotelRoomBlocks[0].BranchName; //dr["BranchName"].ToString();
                    //uoLabelRoomTypeName.Text = dr["RoomName"].ToString();

                    if (uoHiddenFieldDate.Value != "")
                    {
                        DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);
                        uoLabelDate.Text = dDate.ToString("dd-MMM-yyyy (dddd)");
                    }

                    uoLabelContractRoomCount.Text = listHotelRoomBlocks[0].ContractRoomBlocks; //dr["ContractRoomBlocks"].ToString();
                    uoLabelContractCurrency.Text = listHotelRoomBlocks[0].ContractCurrency;//["ContractCurrency"].ToString();
                    uoLabelContractAmount.Text = listHotelRoomBlocks[0].ContractRate;//dr["ContractRate"].ToString();
                    uoCheckContractBoxTaxInclusive.Checked = listHotelRoomBlocks[0].ContractIsTaxInclusive;//(bool)dr["ContractIsTaxInclusive"];
                    uoLabelContractTax.Text = listHotelRoomBlocks[0].ContractTaxPercent;//dr["ContractTaxPercent"].ToString();
                    uoHiddenFieldReservedCount.Value = listHotelRoomBlocks[0].OverrideReservedRoom;

                    uoLabelOverrideRoomBlocks.Text = listHotelRoomBlocks[0].OverrideRoomBlocks;
                    //uoTextBoxOverrideRoomCount.Text = listHotelRoomBlocks[0].OverrideRoomBlocks;//dr["OverrideRoomBlocks"].ToString();
                    Decimal fAmount = GlobalCode.Field2Decimal(listHotelRoomBlocks[0].OverrideRate);
                    uoTextBoxOverrideAmount.Text = fAmount.ToString("0.00");
                    //if (dr["OverrideCurrentcyID"] == null)
                    //{
                    //    uoDropDownListCurrency.SelectedValue = dr["ContractCurrencyID"].ToString();
                    //}
                    //else
                    //{
                    //    uoDropDownListCurrency.SelectedValue = dr["OverrideCurrentcyID"].ToString();
                    //}
                    uoDropDownListCurrency.SelectedValue = listHotelRoomBlocks[0].OverrideCurrentcyID;//dr["ContractCurrencyID"].ToString();

                    uoCheckOverrideBoxTaxInclusive.Checked = listHotelRoomBlocks[0].OverrideIsTaxInclusive;//(bool)dr["OverrideIsTaxInclusive"];                    
                    uoTextBoxOverrideTax.Text = listHotelRoomBlocks[0].OverrideTaxPercent;//dr["OverrideTaxPercent"].ToString();

                    //Decimal fTax = GlobalCode.Field2Decimal(listHotelRoomBlocks[0].OverrideTaxPercent);
                    //uoTextBoxOverrideTax.Text = fTax.ToString("0.0"); //dr["OverrideTaxPercent"].ToString();

                    if (!uoCheckOverrideBoxTaxInclusive.Checked)
                    {
                        uoTextBoxOverrideTax.Enabled = false;
                        uoTextBoxOverrideTax.Text = "0.0";
                    }
                    else
                    {
                        uoTextBoxOverrideTax.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (listHotelRoomBlocks != null)
                {
                    listHotelRoomBlocks = null;
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
        /// <summary>
        /// Date Created:   06/Jun/2013
        /// Created By:     Josephine Gad
        /// (description)   Don't use fancybosx close if from Hotel Editor page
        /// </summary>
        /// <param name="s"></param>
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

            string sPageFrom ="";
            if (Request.QueryString["pFrom"] != null)
            {
                sPageFrom = GlobalCode.Field2String(Request.QueryString["pFrom"]);
            }

            if (sPageFrom == "")
            {
                sScript += " parent.$.fancybox.close(); ";
            }
            else
            {
                //sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldOverride\").val(\"1\"); ";
                sScript += " window.opener.document.getElementById('ctl00_ContentPlaceHolder1_uoHiddenFieldOverride').value = '1';  ";
                sScript += " window.opener.document.forms[0].submit(); ";   
                sScript += "   close(); ";
            }
            
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        /// Date Created:   06/Jun/2013
        /// Created By:     Josephine Gad
        /// (description)   Don't use fancybosx close if from Hotel Editor page
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
            //sScript += " parent.$.fancybox.close(); ";
            
            string sPageFrom = "";
            if (Request.QueryString["pFrom"] != null)
            {
                sPageFrom = GlobalCode.Field2String(Request.QueryString["pFrom"]);
            }

            if (sPageFrom == "")
            {
                sScript += " parent.$.fancybox.close(); ";
            }
            else
            {                
                sScript += "   close(); ";
            }
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
        protected void HotelRoomOverrideLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Hotel room override editor. (Edit)";
            strFunction = "HotelRoomOverrideLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        #endregion       
    }
}

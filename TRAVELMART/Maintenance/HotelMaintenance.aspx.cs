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
    public partial class HotelMaintenance : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["vmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                HotelVendorLogAuditTrail();

                //VendorMaintenanceVariables.vendorPrimaryIdString = Request.QueryString["vmId"];
                //VendorMaintenanceVariables.vendorTypeString = Request.QueryString["vmType"];   
                uoHiddenFieldVendorIdInt.Value = Request.QueryString["vmId"];
                uoHiddenFieldVendorType.Value = Request.QueryString["vmType"];
                                
                vendorCountryLoad();
                //ChangeToUpperCase(uoDropDownListCountry);
                vendorCityLoad(GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue));
                //ChangeToUpperCase(uoDropDownListCity);
                vendorInfoLoad();
            }
        }
        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// Description: Save vendor                           
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {            
            try
            {
                string strLogDescription;
                string strFunction;

                //string vendorPrimaryId = (VendorMaintenanceVariables.vendorPrimaryIdString == null) ? "" : VendorMaintenanceVariables.vendorPrimaryIdString;
                string vendorPrimaryId = uoHiddenFieldVendorIdInt.Value;

                Int32 VendorID = VendorMaintenanceBLL.vendorMaintenanceInsert(uoTextBoxVendorCode.Text.ToUpper(), uoTextBoxVendorName.Text.ToUpper(), uoHiddenFieldVendorType.Value,
                    uoTextBoxVendorAddress.Text, Convert.ToInt32(uoDropDownListCity.SelectedValue), Convert.ToInt32(uoDropDownListCountry.SelectedValue),
                    uoTextBoxContactNo.Text, GlobalCode.Field2String(Session["UserName"]), vendorPrimaryId, uoTextBoxPerson.Text);

                if (vendorPrimaryId == "0" || vendorPrimaryId == "")
                {
                    //Insert log audit trail (Gabriel Oquialda - 16/11/2011)
                    strLogDescription = "Hotel vendor added.";
                    strFunction = "uoButtonSave_Click";
                    
                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(VendorID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }       
                else
                {
                    //Insert log audit trail (Gabriel Oquialda - 16/11/2011)
                    strLogDescription = "Hotel vendor updated.";
                    strFunction = "uoButtonSave_Click";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(VendorID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }

                OpenParentPage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 1)
            {
                vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                //ChangeToUpperCase(uoDropDownListCity);
            }
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void uoButtonFilterCity_Click(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 0)
            {
                vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                //ChangeToUpperCase(uoDropDownListCity);
            }
        }
        #endregion

        #region Functions

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) format data to uppercase
        /// --------------------------------------------------------
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        /// <summary>
        /// Date Created:   01/08/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Loads vendor information 
        /// -----------------------------------------
        /// Date Modified:  28/11/2011
        /// Modified By:    Charlene Remotigue
        /// (description)   optimization (use datareader instead of datatable
        /// -----------------------------------------
        /// Date Modified:  24/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Close IDataReader and add cityname in textbox
        ///                 Disable uoTextBoxVendorCode if there is value
        /// </summary>
        private void vendorInfoLoad()
        {
            IDataReader drVendorInfo = null;
            try
            {
                if (Request.QueryString["vmId"].ToString() != "0")
                {                    
                    drVendorInfo = VendorMaintenanceBLL.vendorMaintenanceInformation(GlobalCode.Field2Int(uoHiddenFieldVendorIdInt.Value));
                    if (drVendorInfo.Read())
                    {
                        uoTextBoxVendorCode.Text = drVendorInfo["colVendorCodeVarchar"].ToString();
                        if (uoTextBoxVendorCode.Text.Trim() != "")
                        {
                            uoTextBoxVendorCode.CssClass = "ReadOnly";
                            uoTextBoxVendorCode.ReadOnly = true;
                        }
                        uoTextBoxVendorName.Text = drVendorInfo["colVendorNameVarchar"].ToString();

                        string strVendorType = drVendorInfo["colVendorTypeVarchar"].ToString();
                        uoHiddenFieldVendorType.Value = strVendorType;

                        //string strHotelCategory = drVendorInfo.Rows[0]["colVendorHotelCategoryVarchar"].ToString();
                        //uoDropDownListHotelCategory.SelectedValue = strHotelCategory;

                        uoTextBoxVendorAddress.Text = drVendorInfo["colAddressVarchar"].ToString();

                        uoDropDownListCountry.SelectedValue = drVendorInfo["colCountryIDInt"].ToString();
                        uoTextBoxCityName.Text = drVendorInfo["colCityNameVarchar"].ToString();
                        if (uoDropDownListCountry.SelectedIndex > 1)
                            vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                        uoDropDownListCity.SelectedValue = drVendorInfo["colCityIDInt"].ToString();

                        uoTextBoxPerson.Text = drVendorInfo["colContactPersonVarchar"].ToString();
                        uoTextBoxContactNo.Text = drVendorInfo["colContactNoVarchar"].ToString();
                        //uoCheckBoxVendorAccredited.Checked = Convert.ToBoolean(drVendorInfo["colIsAccreditedBit"].ToString());

                        ChangeToUpperCase(uoDropDownListCountry);
                        ChangeToUpperCase(uoDropDownListCity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (drVendorInfo != null)
                {
                    drVendorInfo.Close();
                    drVendorInfo.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   29/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Loads vendor city to dropdownlist            
        /// ===============================================
        /// Date Modified:  24/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace CityListbyCountry with GetCityByCountry
        /// </summary>
        private void vendorCityLoad(int vendorCountryId)
        {
            DataTable dt = null;
            try
            {                
                //dt = CityBLL.CityListbyCountry(vendorCountryId);
                dt = CityBLL.GetCityByCountry(vendorCountryId.ToString(), uoTextBoxCityName.Text.Trim(), "0");
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataSource = dt;
                    uoDropDownListCity.DataTextField = "colCityNameVarchar";
                    uoDropDownListCity.DataValueField = "colCityIDInt";
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select a City--", ""));
                }
                else
                {
                    uoDropDownListCity.DataBind();
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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist            
        /// </summary>
        private void vendorCountryLoad()
        {            
            DataTable dt = new DataTable();
            dt = CountryBLL.CountryList();
            if (dt.Rows.Count > 0)
            {
                uoDropDownListCountry.DataSource = dt;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("- Select a Country -", ""));
                uoDropDownListCountry.SelectedValue = dt.Rows[0]["colCountryIDInt"].ToString();
            }
            else
            {
                uoDropDownListCountry.DataBind();
            }
        }        
        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Close this page and update parent page
            /// -------------------------------------------------------------------------------
            /// Date Modified: 02/08/2011
            /// Modified By: Gabriel Oquialda
            /// (description) Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"
            ///               to "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\"            
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupHotel\").val(\"1\"); "; //ContentPlaceHolder1
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created: 18/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// --------------------------------------
        /// Date Modified:  27/06/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace ', \n and \r with empty string
        /// </summary>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\n", "");
            s = s.Replace("\r", " ");

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelVendorLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"].ToString() != "0")
            {                
                strLogDescription = "Edit linkbutton for hotel vendor editor clicked.";                
            }
            else
            {                
                strLogDescription = "Add button for hotel vendor editor clicked.";                               
            }

            strFunction = "HotelVendorLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion             
    }
}

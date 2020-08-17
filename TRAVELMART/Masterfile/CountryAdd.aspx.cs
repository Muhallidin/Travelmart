using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.IO;

namespace TRAVELMART
{
    public partial class CountryAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CountryLogAuditTrail();
                //MasterfileVariables.RegionIdString = Request.QueryString["vrId"];
                 Session["CountryId"] = Request.QueryString["vcId"];

                if ( Session["CountryId"] .ToString()!= "0")
                {
                    Session["CountryName"] = Request.QueryString["vcName"];
                    // Session["CountryId"] .ToString()= Request.QueryString["vcId"];
                    //MasterfileVariables.RegionNameString = Request.QueryString["vrName"];
                     Session["CountryCode"]  = Request.QueryString["vcCode"];
                  
                    //uoDropDownListRegion.Visible = false;
                    //uoTextBoxRegion.Visible = true;
                    //uoTextBoxRegion.Text = MasterfileVariables.RegionNameString;
                    uoTextBoxCountryName.Text = Session["CountryName"] .ToString();
                    uoTextBoxCountryCode.Text =  Session["CountryCode"] .ToString();
                    uoTextBoxCountryCode.Enabled = false;
                    //UpdatePanel1.Update();
                }
                else
                {
                    //BindRegion();
                    //uoTextBoxRegion.Visible = false;
                    //uoDropDownListRegion.Visible = true;
                    //UpdatePanel1.Update();
                    uoTextBoxCountryName.Text = "";
                    uoTextBoxCountryCode.Text = "";
                    uoTextBoxCountryCode.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Save all changes
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int rId = 0;

            string cName = Session["CountryName"] .ToString();
            string cCode =  Session["CountryCode"] .ToString();
            if (MasterfileVariables.RegionIdString == "")
            {
                rId = 0;
            }
            else
            {
                rId = Convert.ToInt32(MasterfileVariables.RegionIdString);
            }

            if (countrycodeInInsertExist() == false)
            {
                if ( Session["CountryId"] .ToString()== "0")
                {
                    //MasterfileBLL.countryAddMasterfileInsert(Convert.ToInt32(uoDropDownListRegion.SelectedValue), uoTextBoxCountryCode.Text, uoTextBoxCountryName.Text,
                    //    GlobalCode.Field2String(Session["UserName"]));
                    Int32 CountryID = MasterfileBLL.countryAddMasterfileInsert(uoTextBoxCountryCode.Text, uoTextBoxCountryName.Text,
                        GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Country added.";
                    strFunction = "uoButtonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(CountryID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                    OpenParentPage();
                }
                else
                {
                    int cID = Convert.ToInt32( Session["CountryId"] .ToString());
                    MasterfileBLL.countryAddMasterfileUpdate(rId, cID, uoTextBoxCountryName.Text, uoTextBoxCountryCode.Text, GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Country updated.";
                    strFunction = "uoButtonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(cID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                    OpenParentPage();
                }
            }
            else if (countrycodeInInsertExist() == true)
            {
                AlertMessage("Country code already used.");
            }            
        }
        #region METHODS
         /// <summary>
        /// Date Created: 21/02/2012
        /// Created By: Gabriel Oquialda
        /// (description) Insert validation of country code
        /// </summary>
        private Boolean countrycodeInInsertExist()
        {          
            String CountryCode = uoTextBoxCountryCode.Text;

            Boolean bValidation = HotelBLL.countrycodeInInsertExist(CountryCode, GlobalCode.Field2Int( Session["CountryId"] .ToString()));
            return bValidation;
        }
        ///// <summary>
        ///// Date Created: 03/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: binds all regions to dropdownlist
        ///// </summary>
        //private void BindRegion()
        //{
        //    DataTable RegionDataTable = null;
        //    try
        //    {
        //        RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));

        //        uoDropDownListRegion.Items.Clear();
        //        ListItem item = new ListItem("--Select Region--", "0");
        //        uoDropDownListRegion.Items.Add(item);
        //        uoDropDownListRegion.DataSource = RegionDataTable;
        //        uoDropDownListRegion.DataTextField = "colRegionNameVarchar";
        //        uoDropDownListRegion.DataValueField = "colRegionIDInt";
        //        uoDropDownListRegion.DataBind();
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (RegionDataTable != null)
        //        {
        //            RegionDataTable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='Javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupCountry\").val(\"1\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void CountryLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vcId"] != "0")
            {
                strLogDescription = "Edit linkbutton for country editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for country editor clicked.";
            }

            strFunction = "CountryLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        /// <summary>
        /// Date Created: 18/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        #endregion            
    }
}

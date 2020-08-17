using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART
{
    public partial class CityAdd : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region EVENTS
        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// --------------------------------
        /// Date Modified: 07/05/2012
        /// Modified By: Charlene Remotigue
        /// Description: change .ToString() to GlobalCode
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
             Session["CountryId"]  = Request.QueryString["vcId"];
            if (!IsPostBack)

            {
                CityLogAuditTrail();
                if ( Session["CountryId"] != null)//edit
                {
                    Session["CityId"]  = Request.QueryString["vcTId"];
                    Session["CityCode"] = Request.QueryString["vcTCode"];
                    Session["CityName"] = Request.QueryString["vcTName"];
                    Session["CountryName"] = Request.QueryString["vcName"];
                    //MasterfileVariables.RegionNameString = Request.QueryString["vrName"];
                    
                    uoDropDownListCountry.Visible = false;
                    //uoDropDownListRegion.Visible = false;
                    uoTextBoxCountry.Visible = true;
                    //uoTextBoxRegion.Visible = true;
                    //uoTextBoxRegion.Text = MasterfileVariables.RegionNameString;
                    uoTextBoxCountry.Text = GlobalCode.Field2String(Session["CountryName"]);
                    uoTextBoxCity.Text = GlobalCode.Field2String(Session["CityName"]);
                    uoTextBoxCityCode.Text = GlobalCode.Field2String(Session["CityCode"]);
                    //UpdatePanel1.Update();
                }
                else //new
                {
                    BindCountry();
                    //BindRegion();
                    //uoDropDownListRegion.Visible = true;
                    uoDropDownListCountry.Visible = true;
                    uoTextBoxCountry.Visible = false;
                    //uoTextBoxRegion.Visible = false;
                    //UpdatePanel1.Update();
                }
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int cId = Convert.ToInt32( Session["CountryId"] .ToString());
           
            if (cId == 0)
            {
                Int32 CityID = MasterfileBLL.cityAddMasterfileInsert(Convert.ToInt32(uoDropDownListCountry.SelectedValue), uoTextBoxCityCode.Text,
                    uoTextBoxCity.Text, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "City added.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(CityID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                OpenParentPage();
            }
            else
            { 
                int cityId = GlobalCode.Field2Int(Session["CityId"]);
                MasterfileBLL.cityAddMasterfileUpdate(cId, cityId, uoTextBoxCity.Text,
                    uoTextBoxCityCode.Text, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "City updated.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(cityId, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                OpenParentPage();
            }
        }

        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindCountry();
        //    UpdatePanel2.Update();
        //}
        #endregion

        #region METHODS

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
        //        uoDropDownListRegion.DataTextField = "colMapNameVarchar";
        //        uoDropDownListRegion.DataValueField = "colMapIDInt";
        //        uoDropDownListRegion.DataBind();

        //        item = new ListItem("--Select Country--", "0");
        //        uoDropDownListCountry.Items.Add(item);
                
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
        /// Descrition: binds country to dropdownlist
        /// </summary>

        private void BindCountry()
        {
            DataTable CountryDataTable = null;
            try
            {
                //CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, "");
                CountryDataTable = CountryBLL.CountryList();
                uoDropDownListCountry.Items.Clear();
                ListItem item = new ListItem("--Select Country--", "0");
                uoDropDownListCountry.Items.Add(item);
                uoDropDownListCountry.DataSource = CountryDataTable;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// ----------------------------------------------------
        /// Date Modified: 07/05/2012
        /// Modified By: CHarlene Remotigue
        /// Description: change parent placeholder
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='Javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupCity\").val(\"1\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void CityLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if ( Session["CountryId"] .ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for city editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for city editor clicked.";
            }

            strFunction = "CityLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion

    }
}

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
    public partial class CountryView_old : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region Events

        /// <summary>
        /// Date Created:   03/10/2011
        /// Created By:     Charlene Remotigue
        /// ------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHyperLinkCountryAdd.HRef = "CountryAdd.aspx?vrId=0&vcId=0&vcName=";
                //uoTextBoxSearchParam.Text = "";
                Session["strPrevPage"] = Request.RawUrl;
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            //BindCountry();
        }

        protected void uoCountryList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                int countryId = Convert.ToInt32(e.CommandArgument);
                masterBLL.MasterfileCountryDelete(countryId, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Country deleted. (flagged as inactive)";
                strFunction = "uoCountryList_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(countryId, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               
            }
        }

        protected void uoCountryList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoCountryListPager_PreRender(object sender, EventArgs e)
        {
            BindCountry();
        }
        #endregion

        #region Methods
        

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: binds country to listview
        /// </summary>

        private void BindCountry()
        {
            try
            {
                //uoHiddenFieldRegionId.Value = GlobalCode.Field2String(Session["Region"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
               
                uoCountryList.Items.Clear();
                uoCountryList.DataSource = null;
                uoCountryList.DataSourceID = "ObjectDataSource1";
                UpdatePanel2.Update();
                //uoTextBoxSearchParam.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        #endregion

        #region DELETED
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
        //        RegionDataTable = MasterfileBLL.regionViewMasterfileSearch("");
        //        uoDropDownListRegion.Items.Clear();
        //        ListItem item = new ListItem("--Select Region--", "0");
        //        uoDropDownListRegion.Items.Add(item);
        //        uoDropDownListRegion.DataSource = RegionDataTable;
        //        uoDropDownListRegion.DataTextField = "colMapNameVarchar";
        //        uoDropDownListRegion.DataValueField = "colMapIDInt";
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
        #endregion



    }
}

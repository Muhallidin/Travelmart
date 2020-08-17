using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class SafeguardMaintenanceView : System.Web.UI.Page
    {

        #region Events
        /// Date Modified:  16/03/2012
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
                //BindRegion();
                GetSafeguardVendorList();
                uoHyperLinkSafeguardAdd.HRef = "~/Maintenance/SafeguardMaintenance.aspx?vmId=0&vmType=VE";

                if (User.IsInRole(TravelMartVariable.RoleAdministrator) ||                   
                   User.IsInRole(TravelMartVariable.RoleVehicleSpecialist)
                   )
                {
                    uoBtnSafeguardAdd.Visible = true;
                }
                else
                {
                    uoBtnSafeguardAdd.Visible = false;
                    uoHiddenFieldVendor.Value = "false";
                }
            }
            else
            {

                if (uoHiddenFieldPopupSafeguard.Value == "1")
                {
                    GetSafeguardVendorList();
                }
                uoHiddenFieldPopupSafeguard.Value = "0";
            }
        } 
      
        protected void uoSafeguardVendorList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = GlobalCode.Field2Int(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeleteVehicleVendor(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Safeguard vendor branch deleted. (flagged as inactive)";
                strFunction = "uoSafeguardVendorList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetSafeguardVendorList();
            }
            else if (e.CommandName != "")
            {
                uoHiddenFieldSortBy.Value = e.CommandName;
                GetSafeguardVendorList();
            }
            else
            {
                uoHiddenFieldSortBy.Value = "SortByName";
            }

        }

        protected void uoSafeguardVendorList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }

        protected void uoSafeguardVendorListPager_PreRender(object sender, EventArgs e)
        {
            //GetSafeguardVendorListByUser();
        }

        protected void uoObjectDataSourceSafeguard_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sSafeguardVendorName"] = uoTextBoxSearchParam.Text;
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetSafeguardVendorList();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created:   04/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get the list of vehicle vendors    
        /// ----------------------------------------------
        /// Date Modified:  28/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change uoDropDownListRegion.SelectedValue to GlobalCode.Field2String(Session["Region"])
        /// ----------------------------------------------
        /// Date Modified:  02/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change uoSafeguardVendorList.DataSource to uoObjectDataSourceSafeguard.DataBind()
        /// </summary>
        private void GetSafeguardVendorList()
        {
            //uoSafeguardVendorList.DataSource = MaintenanceViewBLL.GetSafeguardVendorListByUser(strSafeguardName, GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["Region"]),
            //    GlobalCode.Field2String(Session["UserVendor"]), GlobalCode.Field2String(Session["UserRole"]));
            //uoSafeguardVendorList.DataBind();
            
            //            uoObjectDataSourceSafeguard.DataBind();
            uoSafeguardVendorList.DataSourceID = "uoObjectDataSourceSafeguard";
            uoSafeguardVendorList.DataBind();
        }

        /// <summary>
        /// Date Created: 03/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Contact number US format            
        /// </summary>
        //protected string FormatUSContactNo(object oUSContactNo)
        //{
        //    String strUSContactNo = (String)oUSContactNo;

        //    if (strUSContactNo != "")
        //    {
        //        string strFormat;
        //        strFormat = string.Format("({0}) {1}-{2}",
        //            strUSContactNo.Substring(0, 3),
        //            strUSContactNo.Substring(3, 3),
        //            strUSContactNo.Substring(6));
        //        return strFormat;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

       

        ///// <summary>
        ///// Date Created: 19/08/2011
        ///// Created By: Marco Abejar
        ///// (description) Bind assigned region           
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

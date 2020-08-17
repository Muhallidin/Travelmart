using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class PortAgentVehicleDashboard : System.Web.UI.Page
    {
        #region DECLARATIONS
        protected DashboardBLL dbBLL = new DashboardBLL();
        #endregion

        #region EVENTS
        /// <summary>
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
                uoHiddenFieldPortAgent.Value = GlobalCode.Field2String(Session["UserBranchID"]);
                uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                uoHiddenFieldEndDate.Value = GlobalCode.Field2String(Session["DateTo"]);
                LoadVehicleBrand();
                LoadDashboardDetails();
            }
        }

        protected void uoDropDownListBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVehicleBranch();
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldbranch.Value = uoDropDownListBranch.SelectedValue;
            uoHiddenFieldBrand.Value = uoDropDownListBrand.SelectedValue;
            LoadDashboardDetails();
        }

        protected void uoButtonViewAll_Click(object sender, EventArgs e)
        {
            uoHiddenFieldbranch.Value = "0";
            uoHiddenFieldBrand.Value = "0";
            LoadDashboardDetails();
        }

        protected void uoVehicleDashboardListPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region METHODS
        protected void LoadVehicleBrand()
        {
            DataTable brandDatatable = null;

            try
            {
                brandDatatable = dbBLL.loadVehicleBrandByPort(GlobalCode.Field2String(Session["UserBranchID"]));
                ListItem item = new ListItem("--Select Vehicle Brand--", "0");
                uoDropDownListBrand.Items.Clear();
                uoDropDownListBrand.Items.Add(item);
                uoDropDownListBrand.DataSource = brandDatatable;
                uoDropDownListBrand.DataTextField = "colVendorNameVarchar";
                uoDropDownListBrand.DataValueField = "colVendorIDInt";
                uoDropDownListBrand.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (brandDatatable != null)
                {
                    brandDatatable.Dispose();
                }
            }
        }

        protected void LoadVehicleBranch()
        {
            DataTable branchDataTable = null;
            try
            {
                branchDataTable = dbBLL.loadVehicleBranchByPort(Int32.Parse(uoDropDownListBrand.SelectedValue));
                ListItem item = new ListItem("--Select Vehicle Branch--", "0");
                uoDropDownListBranch.Items.Clear();
                uoDropDownListBranch.Items.Add(item);
                uoDropDownListBranch.DataSource = branchDataTable;
                uoDropDownListBranch.DataTextField = "colVendorBranchNameVarchar";
                uoDropDownListBranch.DataValueField = "colBranchIDInt";
                uoDropDownListBranch.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (branchDataTable != null)
                {
                    branchDataTable.Dispose();
                }
            }
        }

        protected void LoadDashboardDetails()
        {
            uoVehicleDashboardList.Items.Clear();
            uoVehicleDashboardList.DataSource = null;
            uoVehicleDashboardList.DataSourceID = "ObjectDataSource1";
           
        }

        string lastVehicleBrand = null;
        string lastVehicleBranch = null;
        protected string addDashBoardGroup()
        {
            string BrandTitle = "Vehicle Brand :";
            string BranchTitle = "Vehicle Branch :";

            string currentVehicleBrand = Eval("colVendorName").ToString();
            string currentVehicleBranch = Eval("colBranchName").ToString();

            if (currentVehicleBrand.Length == 0)
            {
                currentVehicleBrand = "";
            }
            if (currentVehicleBranch.Length == 0)
            {
                currentVehicleBranch = "";
            }

            if (lastVehicleBrand != currentVehicleBrand && lastVehicleBranch != currentVehicleBranch)
            {
                lastVehicleBranch = currentVehicleBranch;
                lastVehicleBrand = currentVehicleBrand;
                return string.Format("<tr><td class=\"group\" colspan=\"8\">{0}: <strong>{1}</strong></td></tr> " +
                            "<tr><td class=\"group\" colspan=\"8\">{2}: <strong>{3}</strong></td></tr>", BrandTitle,
                            currentVehicleBrand, BranchTitle, currentVehicleBranch);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}

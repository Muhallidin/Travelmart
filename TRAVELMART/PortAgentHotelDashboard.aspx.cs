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
    public partial class PortAgentHotelDashboard : System.Web.UI.Page
    {
        #region DECLARATIONS
        protected DashboardBLL dbBLL = new DashboardBLL();
        string lastHotelBrand = null;
        string lastHotelBranch = null;
        #endregion

        #region EVENTS
        /// <summary>
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ======================================================== 
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
                uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                uoHiddenFieldEndDate.Value = GlobalCode.Field2String(Session["DateTo"]);
                uoHiddenFieldPortAgent.Value = GlobalCode.Field2String(Session["UserBranchID"]);
                LoadHotelBrand();
                LoadDashboardDetails();
            }
        }

        protected void uoDropDownListBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHotelBranch();
        }

        protected void uoHotelDashboardDetailsPager_PreRender(object sender, EventArgs e)
        {

        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldBrandId.Value = uoDropDownListBrand.SelectedValue;
            uoHiddenFieldBranchId.Value = uoDropDownListBranch.SelectedValue;
            LoadDashboardDetails();
        }

        protected void uoButtonViewAll_Click(object sender, EventArgs e)
        {
            uoHiddenFieldBrandId.Value = "0";
            uoHiddenFieldBranchId.Value = "0";
            LoadDashboardDetails();
        }
        #endregion

        #region EVENTS
        protected void LoadHotelBrand()
        {
            DataTable brandDatatable = null;

            try
            {
                brandDatatable = dbBLL.loadHotelBrandByPort(GlobalCode.Field2String(Session["UserBranchID"]));
                ListItem item = new ListItem("--Select Hotel Brand--", "0");
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

        protected void LoadHotelBranch()
        {
            DataTable branchDatatable = null;

            try
            {
                branchDatatable = dbBLL.loadHotelBranchByPort(Int32.Parse(uoDropDownListBrand.SelectedValue));
                ListItem item = new ListItem("--Select Hotel Branch--", "0");
                uoDropDownListBranch.Items.Clear();
                uoDropDownListBranch.Items.Add(item);
                uoDropDownListBranch.DataSource = branchDatatable;
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
                if (branchDatatable != null)
                {
                    branchDatatable.Dispose();
                }
            }
        }

        protected void LoadDashboardDetails()
        {
            uoHotelDashboardDetails.Items.Clear();
            uoHotelDashboardDetails.DataSource = null;
            uoHotelDashboardDetails.DataSourceID = "ObjectDataSource1";
            
        }

        
        protected string addDashBoardGroup()
        {
            string BrandTitle = "Hotel Brand :";
            string BranchTitle = "Hotel Branch :";

            string currentHotelBrand = Eval("colVendorName").ToString();
            string currentHotelBranch = Eval("colBranchName").ToString();

            if (currentHotelBrand.Length == 0)
            {
                currentHotelBrand = "";
            }
            if (currentHotelBranch.Length == 0)
            {
                currentHotelBranch = "";
            }

            if (lastHotelBrand != currentHotelBrand && lastHotelBranch != currentHotelBranch)
            {
                lastHotelBranch = currentHotelBranch;
                lastHotelBrand = currentHotelBrand;
                return string.Format("<tr><td class=\"group\" colspan=\"10\">{0}: <strong>{1}</strong></td></tr> " +
                            "<tr><td class=\"group\" colspan=\"10\">{2}: <strong>{3}</strong></td></tr>", BrandTitle,
                            currentHotelBrand, BranchTitle, currentHotelBranch);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

       

        

    }
}

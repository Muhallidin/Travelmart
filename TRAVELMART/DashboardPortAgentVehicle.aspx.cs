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
    public partial class DashboardPortAgentVehicle : System.Web.UI.Page
    {
        protected DashboardBLL dbBLL = new DashboardBLL();
        
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/11/2011
        /// ========================================================        
        /// Date Modified:  16/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 Session["strPrevPage"] = Request.RawUrl;
                uoHiddenFieldEndDate.Value = GlobalCode.Field2String(Session["DateTo"]);
                uoHiddenFieldStartDate.Value = GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])).ToString();
                uoHiddenFieldPortAgentId.Value = Session["UserBranchID"].ToString();
                LoadVehicleBrand();
                LoadDashboardDetails();
                //UpdatePanelVehicle.Update();
               
            }
        }

        protected void uoVehicleDashboardListPager_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                lastDataFieldValue = null;
                
            }
        }

        protected void uoDropDownListBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastDataFieldValue = null;
           
            LoadVehicleBranch();
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            lastDataFieldValue = null;
            uoHiddenFieldBrandId.Value = uoDropDownListBrand.SelectedValue;
            uoHiddenFieldBranchId.Value = uoDropDownListBranch.SelectedValue;
          
            LoadDashboardDetails();
           
        }

        protected void uoButtonViewAll_Click(object sender, EventArgs e)
        {
            ClearFields();
           
            LoadDashboardDetails();
           
        }
        #endregion

        #region EVENTS
        protected void LoadDashboardDetails()
        {
            lastDataFieldValue = null;
            uoVehicleDashboardList.Items.Clear();
            uoVehicleDashboardList.DataSource = null;
            uoVehicleDashboardList.DataSourceID = "ObjectDataSource1";
           
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/11/2011
        /// Description: Set dashboard groupings
        /// </summary>
        string lastDataFieldValue = null;
        string lastDataFieldBrand = null;
        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row   
            string GroupTitleString = "Vehicle Brand: ";
            string GroupTitleValue = Eval("colVendorName").ToString();
            string GroupTextString = "Vehicle Branch: ";
            string GroupValueString = "colBranchName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue && lastDataFieldBrand != GroupTitleValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                return string.Format("<tr><td class=\"group\" colspan=\"8\">{0}: <strong>{1}</strong></td></tr> " +
                            "<tr><td class=\"group\" colspan=\"8\">{2}: <strong>{3}</strong></td></tr>", GroupTitleString,
                            GroupTitleValue, GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        protected void LoadVehicleBrand()
        {
            DataTable vehicleDataTable = dbBLL.loadVehicleBrandByPort(Session["UserBranchID"].ToString());
            ListItem item = new ListItem("--Select Vehicle Brand--", "0");
            uoDropDownListBrand.Items.Clear();
            uoDropDownListBrand.Items.Add(item);
            uoDropDownListBrand.DataSource = vehicleDataTable;
            uoDropDownListBrand.DataTextField = "colVendorNameVarchar";
            uoDropDownListBrand.DataValueField = "colVendorIDInt";
            uoDropDownListBrand.DataBind();
        }

        protected void LoadVehicleBranch()
        {
            DataTable vehicleDataTable = dbBLL.loadVehicleBranchByPort(Int32.Parse(uoDropDownListBrand.SelectedValue));
            ListItem item = new ListItem("--Select Vehicle Branch--", "0");
            uoDropDownListBranch.Items.Clear();
            uoDropDownListBranch.Items.Add(item);
            uoDropDownListBranch.DataSource = vehicleDataTable;
            uoDropDownListBranch.DataTextField = "colVendorBranchNameVarchar";
            uoDropDownListBranch.DataValueField = "colBranchIDInt";
            uoDropDownListBranch.DataBind();
        }

        protected void ClearFields()
        {
            uoDropDownListBrand.SelectedValue = "0";
            uoDropDownListBranch.SelectedValue = "0";
            uoHiddenFieldBranchId.Value = "0";
            uoHiddenFieldBrandId.Value = "0";
            lastDataFieldValue = null;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentContractVehicleView : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadValues();
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created:10/11/2011
        /// Description: load Service Provider contract vehicle details
        /// </summary>
        protected void LoadValues()
        {
            IDataReader dr = null;
            try
            {
                //dr = ContractBLL.LoadPortContractVehicleDetails(Request.QueryString["ServiceId"]);
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        uoLabelBrandName.Text = dr["colVendorNameVarchar"].ToString();
                        uoLabelBranchName.Text = dr["colVendorBranchNameVarchar"].ToString();
                    }
                }
                LoadVehicles();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
           
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider contract vehicles specifications
        /// </summary>
        protected void LoadVehicles()
        {
            DataTable vehicleDataTable = null;// ContractBLL.LoadPortAgentContractVehicles(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
            uoVehicleList.Items.Clear();
            uoVehicleList.DataSource = vehicleDataTable;
            uoVehicleList.DataBind();

            if (vehicleDataTable.Rows.Count > 0)
            {
                uoTRVehicles.Visible = true;
            }
            else
            {
                uoTRVehicles.Visible = false;
            }
        }
        #endregion
    }
}

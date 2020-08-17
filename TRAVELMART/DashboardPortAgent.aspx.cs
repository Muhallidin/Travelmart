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
    public partial class DashboardPortAgent : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 Session["strPrevPage"] = Request.RawUrl;
                LoadDashboardDetails(); 
            }
        }
        #endregion

        #region METHODS
        /// <summary>
       /// Author: Charlene Remotigue
       /// Date Created: 14/11/2011
       /// Description: Set dashboard groupings
       /// </summary>
        string lastDataFieldValue = null;
        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Port Name: ";
            string GroupValueString = "PortName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                return string.Format("<tr><td class=\"group\" colspan=\"6\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        protected void LoadDashboardDetails()
        {
            DataTable dashboardDataTable = null;
            try
            {
                dashboardDataTable = DashboardBLL.GetPortAgentPortDashboardDetails(Session["UserBranchID"].ToString(), GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])).ToString(),
                                        GlobalCode.Field2String(Session["DateTo"]));
                uoDashboardList.Items.Clear();
                uoDashboardList.DataSource = dashboardDataTable;
                uoDashboardList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dashboardDataTable != null)
                {
                    dashboardDataTable.Dispose();
                }
            }
        }
        #endregion
    }
}

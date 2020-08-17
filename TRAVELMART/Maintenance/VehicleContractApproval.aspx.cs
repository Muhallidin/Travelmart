using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART.Maintenance
{
    public partial class VehicleContractApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                Session["VehiclePath"] = Path.GetFileName(Request.Path);
                GetVehicleVendorBranchNoActiveContractList();                
            }
        }

        private void GetVehicleVendorBranchNoActiveContractList()
        {
            /// <summary>
            /// Date Created: 13/10/2011
            /// Created By: Gabriel Oquialda
            /// (description) Get the list of vehicle vendors branch            
            /// </summary>

            uoVehicleVendorList.DataSource = BLL.ContractBLL.GetVendorVehicleBranchPendingContract(uoTextBoxVehicle.Text.Trim());
            uoVehicleVendorList.DataBind();
        }
                
        protected void uoVehicleVendorListPager_PreRender(object sender, EventArgs e)
        {

        }

        protected void uoVehicleVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int index = GlobalCode.Field2Int(e.CommandArgument);
            if (e.CommandName == "Select")
            {
                if (index > 0)
                {
                    ApproveContract(index);
                }
            }
        }                
        protected void uoVehicleVendorList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void uoVehicleVendorList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }
        
        protected void uoVehicleVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetVehicleVendorBranchNoActiveContractList();
        }
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {            
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", sScript, false);
        }
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Approve Vehicle Contract 
        /// </summary>
        private void ApproveContract(int index)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                strLogDescription = "Vehicle pending contract approved.";
                strFunction = "uoVehicleVendorList_ItemCommand";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                BLL.ContractBLL.UpdateVehicleContractStatus(index, uoHiddenFieldUser.Value,
                    strLogDescription, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate));
                GetVehicleVendorBranchNoActiveContractList();
            }
            catch (Exception ex)
            {                
                AlertMessage(ex.Message);
            }

        }

      

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class VehicleNoActiveContractList : System.Web.UI.Page
    {
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                GetVehicleVendorBranchList();
                //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;
            }
        }

        private void GetVehicleVendorBranchList()
        {
            /// <summary>
            /// Date Created: 13/10/2011
            /// Created By: Gabriel Oquialda
            /// (description) Get the list of vehicle vendors branch            
            /// </summary>

            uoVehicleVendorList.DataSource = BLL.ContractBLL.GetVendorVehicleBranchNoActiveContract(GlobalCode.Field2String(Session["UserName"]));
            uoVehicleVendorList.DataBind();
        }
                
        protected void uoVehicleVendorListPager_PreRender(object sender, EventArgs e)
        {

        }
        
        protected void uoVehicleVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }        
                
        protected void uoVehicleVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
    }
}

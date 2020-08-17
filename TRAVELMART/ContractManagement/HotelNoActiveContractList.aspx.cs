using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Web.UI.HtmlControls;

namespace TRAVELMART.ContractManagement
{
    public partial class HotelNoActiveContractList : System.Web.UI.Page
    {
        #region Events
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
                GetHotelVendorBranchList();
                ShowHideAddContractControls();
                //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;
            }
        }

        private void GetHotelVendorBranchList()
        {
            /// <summary>
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) Get the list of hotel vendors branch            
            /// </summary>

            uoHotelVendorList.DataSource = BLL.ContractBLL.GetVendorHotelBranchNoActiveContract(GlobalCode.Field2String(Session["UserName"]));
            uoHotelVendorList.DataBind();
        }

        #region uoHotelVendorListPager_PreRender
        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region uoHotelVendorList_ItemCommand
        protected void uoHotelVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }
        #endregion

        #region uoHotelVendorList_ItemDeleting
        protected void uoHotelVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion
        #endregion

        #region Functions
        /// <summary>
        /// Date Modified:      13/01/2012
        /// Modified By:        Gelo Oquialda
        /// (description)       Show add contract controls when role is Administrator, Contract Manager, and 24x7
        /// </summary>
        private void ShowHideAddContractControls()
        {
            HtmlControl ContractTH = (HtmlControl)uoHotelVendorList.FindControl("ContractTH");            

            //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;

            if (User.IsInRole(TravelMartVariable.RoleAdministrator) || User.IsInRole(TravelMartVariable.RoleContractManager) ||
                User.IsInRole(TravelMartVariable.RoleCrewAssist))
            {
                uoHiddenFieldAddContract.Value = "true";

                if (ContractTH != null)
                {
                    ContractTH.Style.Add("display", "inline");
                }                
            }
            else
            {
                uoHiddenFieldAddContract.Value = "false";                
                uoHiddenFieldAddContractClass.Value = "hideElement";

                if (ContractTH != null)
                {
                    ContractTH.Style.Add("display", "none");
                }                
            }
            uoHotelVendorList.DataBind();
        }
        #endregion
    }
}

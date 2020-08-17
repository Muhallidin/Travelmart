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
    public partial class HotelContractApproval : System.Web.UI.Page
    {
        #region Page_Load
        /// -------------------------------------------
        /// Date Modified:  15/03/2012
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
                Session["HotelPath"] = Path.GetFileName(Request.Path);
                GetHotelVendorBranchNoActiveContractList();
                Session["strPrevPage"] = Request.RawUrl;
            }
            //if hiden is 1
                // BLL.ContractBLL.UpdateContractStatus(index, GlobalCode.Field2String(Session["UserName"]));
                //GetHotelVendorBranchNoActiveContractList();
            //hidden = 0                  
        }
        #endregion

        #region GetHotelVendorBranchNoActiveContractList
        /// <summary>
        /// Date Created: 04/08/2011
        /// Created By: Ryan Bautista
        /// (description) Get the list of hotel vendors branch            
        /// </summary>
        private void GetHotelVendorBranchNoActiveContractList()
        {            
            uoHotelVendorList.DataSource = BLL.ContractBLL.GetVendorHotelBranchPendingContract();
            uoHotelVendorList.DataBind();
        }
        #endregion

        #region uoHotelVendorListPager_PreRender
        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {
            GetHotelVendorBranchNoActiveContractList();
        }
        #endregion

        #region uoHotelVendorList_ItemCommand
        /// <summary>
        /// Date Modified:      20/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Include validation if there is existing live contract in firt load
        ///                     inactivate previous contract if there's any
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoHotelVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Select")
            {               
                if (uoHiddenFieldInactivePrevious.Value == "1")
                {
                    BLL.ContractBLL.UpdateContractFlag(GlobalCode.Field2Int(uoHiddenFieldPrevActiveContractID.Value), GlobalCode.Field2String(Session["UserName"]));
                    uoHiddenFieldInactivePrevious.Value = "0";
                }
                BLL.ContractBLL.UpdateContractStatus(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Hotel pending contract approved.";
                strFunction = "uoHotelVendorList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetHotelVendorBranchNoActiveContractList();
                //}   
            }
        }
        #endregion

        protected void uoHotelVendorList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }
        protected void uoHotelVendorList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
       
        #region uoHotelVendorList_ItemDeleting
        //protected void uoHotelVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        //{

        //}
        #endregion

    }
}

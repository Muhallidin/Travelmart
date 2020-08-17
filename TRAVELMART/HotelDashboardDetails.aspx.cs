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
    public partial class HotelDashboardDetails : System.Web.UI.Page
    {
        #region DECLARATIONS
        DashboardBLL dbBLL = new DashboardBLL();
        public string VendorName = string.Empty;
        public string BranchName = string.Empty;
        public string Status = string.Empty;
        #endregion

        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 22/11/2011
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //Status = Request.QueryString["Status"];
                if (TravelMartVariable.RoleHotelVendor != GlobalCode.Field2String(Session["UserRole"]))
                {
                    Session["UserVendor"] = Request.QueryString["vId"];
                    Session["UserBranchID"] = Request.QueryString["bId"];
                }
                LoadDetails();
            }
        }
        #endregion

        #region METHODS
        protected void LoadDetails()
        {
            DataTable hotelDataTable = null;
            try
            {
                //hotelDataTable = dbBLL.HotelDashboardbyDate(Int32.Parse(GlobalCode.Field2String(Session["UserVendor"])),
                //    Int32.Parse(GlobalCode.Field2String(Session["UserBranchID"])), Request.QueryString["Status"], DateTime.Parse(Request.QueryString["date"]),
                //    Request.QueryString["rType"]);
                //VendorName = hotelDataTable.Rows[0]["colVendorNameVarchar"].ToString();
                //BranchName = hotelDataTable.Rows[0]["colVendorBranchNameVarchar"].ToString();
                //uoDashBoardListDetails.Items.Clear();
                //uoDashBoardListDetails.DataSource = hotelDataTable;
                //uoDashBoardListDetails.DataBind();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (hotelDataTable != null)
                {
                    hotelDataTable.Dispose();
                }
            }
        }

      
        #endregion
    }
}

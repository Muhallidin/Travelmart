using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class HotelMaintenanceView_old : System.Web.UI.Page
    {

        #region Events
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date Modified: 27/10/2011
        /// Description: added checking for hotel vendor
        /// ----------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  27/10/2011
        /// Description:    Change MUser.GetUserRole() to User.IsInRole
        /// ----------------------------------------------
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //BindRegion();
                GetHotelVendorListByUser(uoTextBoxSearchParam.Text);              
                uoHyperLinkHotelAdd.HRef = "~/Maintenance/HotelMaintenance.aspx?vmId=0&vmType=HO";
                Session["strPrevPage"] = Request.RawUrl;

                if (User.IsInRole(TravelMartVariable.RoleAdministrator) ||
                    User.IsInRole(TravelMartVariable.Role24x7) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAdmin) ||
                    User.IsInRole(TravelMartVariable.RolePortSpecialist) ||
                    User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
                    )
                {
                    uoBtnHotelAdd.Visible = true;
                }
                else
                {
                    uoBtnHotelAdd.Visible = false;
                    uoHiddenFieldVendor.Value = "false";
                }
            }
            Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            SFStatus.Visible = false;           
        }
        protected void uoHotelVendorList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeleteHotelVendor(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Hotel vendor deleted. (flagged as inactive)";
                strFunction = "uoHotelVendorList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetHotelVendorListByUser(uoTextBoxSearchParam.Text);
            }
        }
        protected void uoHotelVendorList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {
        }
        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {
            GetHotelVendorListByUser(uoTextBoxSearchParam.Text);
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetHotelVendorListByUser(uoTextBoxSearchParam.Text);
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Functions
        //private void GetHotelVendorList(string strHotelName)
        //{
        //    /// <summary>
        //    /// Date Created: 04/08/2011
        //    /// Created By: Marco Abejar
        //    /// (description) Get the list of hotel vendors            
        //    /// </summary>
        //    //GlobalCode.Field2String(Session["Region"]), 
        //    // GlobalCode.Field2String(Session["Country"]),
        //    //GlobalCode.Field2String(Session["City"]),
        //    //GlobalCode.Field2String(Session["Port"]), 
        //    //GlobalCode.Field2String(Session["Hotel"]);
        //    uoHotelVendorList.DataSource = MaintenanceViewBLL.GetHotelVendorList(strHotelName, GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue);
        //    uoHotelVendorList.DataBind();
        //}
        /// <summary>
        /// Date Created:   04/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get the list of hotel vendors     
        /// --------------------------------------------------
        /// Date Modified:  27/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Validate if user role is not empty
        /// </summary>
        private void GetHotelVendorListByUser(string strHotelName)
        {            
            //GlobalCode.Field2String(Session["Region"]), 
            // GlobalCode.Field2String(Session["Country"]),
            //GlobalCode.Field2String(Session["City"]),
            //GlobalCode.Field2String(Session["Port"]), 
            //GlobalCode.Field2String(Session["Hotel"]);
            if (Session["UserRole"] == null)
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }
            uoHotelVendorList.DataSource = MaintenanceViewBLL.GetHotelVendorListByUser(strHotelName, GlobalCode.Field2String(Session["UserName"]),
                                            GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["UserVendor"]), GlobalCode.Field2String(Session["UserRole"]));
            uoHotelVendorList.DataBind();
        }
        /// <summary>
        /// Date Created: 03/08/2011
        /// Created By:   Gabriel Oquialda
        /// (description) Contact number US format            
        /// </summary>
        protected string FormatUSContactNo(object oUSContactNo)
        {         
            String strUSContactNo = (String)oUSContactNo;
            if (strUSContactNo != "")
            {
                string strFormat;
                strFormat = string.Format("({0}) {1}-{2}",
                    strUSContactNo.Substring(0, 3),
                    strUSContactNo.Substring(3, 3),
                    strUSContactNo.Substring(6));
                return strFormat;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Date Created: 19/08/2011
        /// Created By: Marco Abejar
        /// (description) Bind assigned region           
        /// </summary>
        private void BindRegion()
        {         
            DataTable RegionDataTable = null;
            try
            {
                RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListRegion.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListRegion.Items.Add(item);
                uoDropDownListRegion.DataSource = RegionDataTable;
                uoDropDownListRegion.DataTextField = "colMapNameVarchar";
                uoDropDownListRegion.DataValueField = "colMapIDInt";
                uoDropDownListRegion.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }   
        #endregion
    }
}

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class MaintenanceView : System.Web.UI.Page
    {


        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegion();
                GetHotelVendorList("");
                GetVehicleVendorList("");
                GetPortList("");
                uoHyperLinkHotelAdd.HRef = "~/Maintenance/HotelMaintenance.aspx?vmId=0&vmType=HO";
                uoHyperLinkVehicleAdd.HRef = "~/Maintenance/VehicleMaintenance.aspx?vmId=0&vmType=VE";
                uoHyperLinkPortAdd.HRef = "~/Maintenance/PortMaintenance.aspx?vmId=0";
            }
            Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            SFStatus.Visible = false;
        
            if (User.IsInRole("administrator"))
            {
                uotableHotelVendorList.Visible = true;
                uotableVehicleVendorList.Visible = true;
                uotablePortList.Visible = true;                
            }
            else if (User.IsInRole("Vehicle Specialist"))
            {
                uoVehicleVendorListPager.PageSize = 20;
                uotableHotelVendorList.Visible = false;
                uotableVehicleVendorList.Visible = true;
                uotablePortList.Visible = false;
                uocollapsibleVehicle.Collapsed = false;
            }
            else if (User.IsInRole("Hotel Specialist"))
            {
                uoHotelVendorListPager.PageSize = 20;
                uotableHotelVendorList.Visible = true;
                uotableVehicleVendorList.Visible = false;
                uotablePortList.Visible = false;
                uocollapsibleHotel.Collapsed = false;
            }
            else if (User.IsInRole("Port Specialist"))
            {
                uoPortListPager.PageSize = 20;
                uotableHotelVendorList.Visible = false;
                uotableVehicleVendorList.Visible = false;
                uotablePortList.Visible = true;
                uocollapsiblePort.Collapsed = false;
            }
            if (uoHiddenFieldPopupPort.Value == "1")
            {
                GetPortList("");
            }
            uoHiddenFieldPopupPort.Value = "0";
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

                GetHotelVendorList("");
            }
        }
        protected void uoHotelVendorList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {
        }
        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {
            GetHotelVendorList("");
        }
        protected void uoVehicleVendorList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeleteVehicleVendor(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Vehicle vendor deleted. (flagged as inactive)";
                strFunction = "uoVehicleVendorList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));


                GetVehicleVendorList("");
            }
        }
        protected void uoVehicleVendorList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uoVehicleVendorListPager_PreRender(object sender, EventArgs e)
        {
            GetVehicleVendorList("");
        }
        protected void uoPortList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeletePort(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Port vendor deleted. (flagged as inactive)";
                strFunction = "uoPortList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                
                GetPortList("");
            }
        }
        protected void uoPortList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uoPortListPager_PreRender(object sender, EventArgs e)
        {
            GetPortList("");
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPortList("");
        }    
        #endregion

        #region Functions
        private void GetHotelVendorList(string strHotelName)
        {
            /// <summary>
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) Get the list of hotel vendors            
            /// </summary>

            uoHotelVendorList.DataSource = MaintenanceViewBLL.GetHotelVendorList(strHotelName, GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue);
            uoHotelVendorList.DataBind();
        }

        private void GetVehicleVendorList(string strVehicleName)
        {
            /// <summary>
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) Get the list of vehicle vendors            
            /// </summary>

            uoVehicleVendorList.DataSource = MaintenanceViewBLL.GetVehicleVendorList(strVehicleName, GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue);
            uoVehicleVendorList.DataBind();
        }
       
        private void GetPortList(string strPortName)
        {
            /// <summary>
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) Get the list of ports            
            /// </summary>

            //uoPortList.DataSource = MaintenanceViewBLL.GetPortList(strPortName, 0, GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue);
            uoPortList.DataBind();
        }  

        protected string FormatUSContactNo(object oUSContactNo)
        {
            /// <summary>
            /// Date Created: 03/08/2011
            /// Created By: Gabriel Oquialda
            /// (description) Contact number US format            
            /// </summary>

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

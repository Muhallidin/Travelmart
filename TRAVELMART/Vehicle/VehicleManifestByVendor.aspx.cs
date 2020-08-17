using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using System.Web.Security;
using TRAVELMART.BLL;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;

namespace TRAVELMART.Vehicle
{
    public partial class VehicleManifestByVendor : System.Web.UI.Page
    {
        VehicleManifestBLL BLL = new VehicleManifestBLL();

        #region "Events"
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       10/Oct/2013
        /// Description:        Load Page of Vehicle Manifest
        /// ------------------------------------
        /// Modifed By:         Josephine Gad
        /// Date Modified:      21/May/2014
        /// Description:        Add uoHiddenFieldRoleVehicleVendor for multiple role with Vehicle  Vendor Role
        /// ------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            
            string sUserName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }
           
            Session["strPrevPage"] = Request.RawUrl;
            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            
            if (!IsPostBack)
            {                
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                uoHiddenFieldRoleVehicleVendor.Value = uoHiddenFieldRole.Value;
                if (User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                {
                    uoHiddenFieldRoleVehicleVendor.Value = TravelMartVariable.RoleVehicleVendor;
                }
                BindBrand();
                BindVessel();

                BindVehicleManifest();
                BindVehicleManifestConfirm();

                uoHiddenFieldVehicle.Value = "0";
                BindVehicleDropDown();
                uoButtonConfirm.Visible = false;

                BindRoute();
            }
            else
            {
                BindVehicleManifest();
                BindVehicleManifestConfirm();
                uoHiddenFieldServicePopup.Value = "0";
            }
            uoListViewManifestHeader.DataSource = null;
            uoListViewManifestHeader.DataBind();

            uoListViewHeaderConfirm.DataSource = null;
            uoListViewHeaderConfirm.DataBind();

            uoListViewHeaderCancel.DataSource = null;
            uoListViewHeaderCancel.DataBind();           
        }
 
        protected void uoDropDownListVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";
            //BindVehicleManifest();
            uoHiddenFieldVehicle.Value = uoDropDownListVehicle.SelectedValue;
            Session["VehicleID"] = uoHiddenFieldVehicle.Value;
            BindVehicleManifest();
            //ButtonSaveTranspoSettings();
        }



        //protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Port"] = uoDropDownListPort.SelectedValue;
        //    uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;            
        //    BindVehicleManifest();            
        //}
        protected void uoDataPagerManifest_PreRender(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
        }
        protected void uoListViewManifestDetails_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //MBLL.GetHotelConfirmManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Confirm");
        }
        protected void uoListViewManifestConfirm_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            BLL.GetVehicleManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Confirm");
        }
        protected void uoListViewManifestCancel_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            BLL.GetVehicleManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Cancel");
        }
        //protected void uoDropDownListVehicle_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    uoHiddenFieldVehicle.Value = uoDropDownListVehicle.SelectedValue;
        //    Session["VehicleID"] = uoHiddenFieldVehicle.Value;
        //    BindVehicleManifest();
        //    //BindVehicleManifestConfirm();
        //    //BindVehicleManifestCancel();
        //}
        protected void uoListViewManifestHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            uoHiddenFieldLoadType.Value = "1";
        }
        protected void uoListViewHeaderConfirm_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
            uoHiddenFieldSortBy.Value = e.CommandName;
            uoDataPagerManifest.SetPageProperties(0, uoDataPagerManifest.PageSize, false);
            uoDataPagerManifestConfirm.SetPageProperties(0, uoDataPagerManifestConfirm.PageSize, false);
            uoDataPagerManifestCancel.SetPageProperties(0, uoDataPagerManifestCancel.PageSize, false);

            //BindVehicleManifest();           
        }
        protected void uoListViewHeaderCancel_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
            uoHiddenFieldSortBy.Value = e.CommandName;
            uoDataPagerManifest.SetPageProperties(0, uoDataPagerManifest.PageSize, false);
            uoDataPagerManifestConfirm.SetPageProperties(0, uoDataPagerManifestConfirm.PageSize, false);
            uoDataPagerManifestCancel.SetPageProperties(0, uoDataPagerManifestCancel.PageSize, false);

            //BindVehicleManifest();   
        }
        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtConfirm = null;
            DataTable dtCancel = null;

            try
            {

                BLL.GetVehicleManifestExport(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value);

                if (Session["VehicleManifest_NewdManifestExport"] != null)
                {
                    dt = (DataTable)Session["VehicleManifest_NewdManifestExport"];
                }
                if (Session["VehicleManifest_ConfirmedManifestExport"] != null)
                {
                    dtConfirm = (DataTable)Session["VehicleManifest_ConfirmedManifestExport"];
                }
                if (Session["VehicleManifest_CancelledManifestExport"] != null)
                {
                    dtCancel = (DataTable)Session["VehicleManifest_CancelledManifestExport"];
                }
                CreateFile(dt, dtConfirm, dtCancel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtConfirm != null)
                {
                    dtConfirm.Dispose();
                }
                if (dtCancel != null)
                {
                    dtCancel.Dispose();
                }
            }
        }

        //protected void uoButtonSave_Click(object sender, EventArgs e)
        //{
        //    SaveVehicleManifest();
        //}
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       09/Oct/2013
        /// Description:        Setup buttons depending on ListView
        /// ------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoListViewManifestDetails_DataBound(object sender, EventArgs e)
        {
            //uoButtonConfirm.Visible = false;          

            //if (uoListViewManifestDetails.Items.Count > 0)
            //{
            //    if (GlobalCode.Field2Int(uoHiddenFieldVehicle.Value) > 0)
            //    {
            //        uoButtonConfirm.Visible = true;
            //    }
            //}
            ButtonSettings();
            CountSettings();
        }
        protected void uoListViewManifestConfirm_DataBound(object sender, EventArgs e)
        {
            ButtonSettings();
        }
        protected void uoListViewManifestCancel_DataBound(object sender, EventArgs e)
        {
            ButtonSettings();
        }
        protected void uoButtonConfirm_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";
           
            ConfirmManifest(false, "", "");
            BindVehicleManifest();
            BindVehicleManifestConfirm();
            BindVehicleManifestCancel();
        }
        protected void uoButtonFilter_Click(object sender, EventArgs e)
        {

        }       
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
            uoHiddenFieldRouteFrom.Value = uoDropDownListRouteFrom.SelectedValue;
            uoHiddenFieldRouteTo.Value = uoDropDownListRouteTo.SelectedValue;
            
            uoHiddenFieldCityFrom.Value = uoDropDownListCityFrom.SelectedValue;
            uoHiddenFieldCityTo.Value = uoDropDownListCityTo.SelectedValue;

            BindCity(0);
        }
        protected void uoDropDownListBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";
            Session["Brand"] = uoDropDownListVessel.SelectedValue;
            uoHiddenFieldBrandID.Value = uoDropDownListBrand.SelectedValue;

            Session.Remove("HotelDashboardDTO_VesselList");
            BindVessel();

            //BindVehicleDropDown(1);
            BindVehicleManifest();
        }

        protected void uoDropDownListVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";
           
            Session["Vessel"] = uoDropDownListVessel.SelectedValue;
            uoHiddenFieldVesselID.Value = uoDropDownListVessel.SelectedValue;

            //BindVehicleDropDown(1);
            BindVehicleManifest();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
        /// </summary>
        //private void BindRegionList()
        //{
        //    List<RegionList> list = new List<RegionList>();
        //    try
        //    {
        //        if (Session["HotelDashboardDTO_RegionList"] != null)
        //        {
        //            list = (List<RegionList>)Session["HotelDashboardDTO_RegionList"];
        //        }
        //        else
        //        {
        //            list = CountryBLL.RegionListByUser(uoHiddenFieldUser.Value);
        //            Session["HotelDashboardDTO_RegionList"] = list;
        //        }
        //        if (list.Count > 0)
        //        {
        //            uoDropDownListRegion.Items.Clear();
        //            uoDropDownListRegion.DataSource = list;
        //            uoDropDownListRegion.DataTextField = "RegionName";
        //            uoDropDownListRegion.DataValueField = "RegionId";
        //            uoDropDownListRegion.DataBind();
        //        }
        //        uoDropDownListRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));

        //        string sRegion = GlobalCode.Field2String(Session["Region"]);
        //        if (sRegion != "")
        //        {
        //            if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
        //            {
        //                uoDropDownListRegion.SelectedValue = sRegion;
        //            }
        //        }
        //        uoHiddenFieldRegion.Value = uoDropDownListRegion.SelectedValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        //private void BindPortList()
        //{
        //    List<PortList> list = new List<PortList>();
        //    try
        //    {
        //        list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

        //        uoDropDownListPort.Items.Clear();
        //        ListItem item = new ListItem("--SELECT PORT--", "0");
        //        uoDropDownListPort.Items.Add(item);
        //        if (list.Count > 0)
        //        {
        //            uoDropDownListPort.DataSource = list;
        //            uoDropDownListPort.DataTextField = "PORTName";
        //            uoDropDownListPort.DataValueField = "PORTID";
        //            uoDropDownListPort.DataBind();

        //            if (GlobalCode.Field2String(Session["Port"]) != "")
        //            {
        //                if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
        //                {
        //                    uoDropDownListPort.SelectedValue = GlobalCode.Field2String(Session["Port"]);
        //                }
        //            }
        //        }
        //        uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///<summary>
        ///Date Created: 02/Sept/2014
        ///Created By: Michael Brian C. Evangelista
        ///Description: Add Oncheckedchanged event for tagging to TblTag_Vehicle.
        ///</summary>
        

        /// <summary>
        /// Date Modified:   08/Oct/2013
        /// Modified By:     Josephine Gad
        /// (description)    Bind the Vehicle Manifest List
        /// </summary>
        private void BindVehicleManifest()
        {
            uoListViewManifestDetails.DataBind();            
        }
        /// <summary>
        /// Date Modified:   10/Oct/2013
        /// Modified By:     Josephine Gad
        /// (description)    Bind the Vehicle Confirmed Manifest List
        /// </summary>
        private void BindVehicleManifestConfirm()
        {
            uoListViewManifestConfirm.DataBind();
        }
        /// <summary>
        /// Date Modified:   10/Oct/2013
        /// Modified By:     Josephine Gad
        /// (description)    Bind the Vehicle Cancelled Manifest List
        /// </summary>
        private void BindVehicleManifestCancel()
        {
            uoListViewManifestCancel.DataBind();
        }
        /// <summary>
        /// Date Modified:   20/May/2014
        /// Modified By:     Josephine Gad
        /// (description)    Bind the drop down list of Vehicle Vendor
        /// </summary>
        /// <param name="iLoadType"></param>
        private void BindVehicleDropDown()
        {
            List<VehicleVendorList> list = new List<VehicleVendorList>();

            if (Session["VehiclManifest_VehicleVendor"] != null)
            {
                list = (List<VehicleVendorList>)Session["VehiclManifest_VehicleVendor"];
            }
            
            uoDropDownListVehicle.Items.Clear();
            uoDropDownListVehicle.Items.Add(new ListItem("--Select Vehicle Company--", "0"));
            //uoDropDownListVehicle.Items.Add(new ListItem("--Select Without Vehicle--", "-1"));

            if (list.Count > 0)
            {
                uoDropDownListVehicle.DataSource = list;
                uoDropDownListVehicle.DataTextField = "VehicleVendorName";
                uoDropDownListVehicle.DataValueField = "VehicleVendorID";

            }
            uoDropDownListVehicle.DataBind();


            if (list.Count == 1)
            {
                uoDropDownListVehicle.SelectedValue = list[0].VehicleVendorID.ToString();
            }
            else if (uoDropDownListVehicle.Items.FindByValue(uoHiddenFieldVehicle.Value) != null)
            {
                uoDropDownListVehicle.SelectedValue = uoHiddenFieldVehicle.Value;
            }
            uoHiddenFieldVehicle.Value = uoDropDownListVehicle.SelectedValue;
            Session["VehicleID"] = uoDropDownListVehicle.SelectedValue;
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   09/Oct/2013
        /// description     Confirm Manifest
        /// </summary>
        private void ConfirmManifest(bool bIsSave, string sEmailTo, string sEmailCc)
        {
            try
            {
                string strLogDescription;
                if (bIsSave)
                {
                    strLogDescription = "Save Vehicle Vendor Email,Confirm Vehicle Manifest";
                }
                else
                {
                    strLogDescription = "Confirm Vehicle Manifest";
                }
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.ConfirmVehicleManifest(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                    GlobalCode.Field2Int(uoHiddenFieldVehicle.Value), uoHiddenFieldRole.Value,
                    bIsSave, sEmailTo, sEmailCc,
                    strLogDescription, "ConfirmManifest",
                    Path.GetFileName(Request.UrlReferrer.AbsolutePath), CommonFunctions.GetDateTimeGMT(dateNow), dateNow);

                //AlertMessage("Confirmed!");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   09/Oct/2013
        /// description     Get New, Confirm and Cancelled Manifest List
        /// ================================================================
        /// Modified By:     Josephine Gad
        /// Date Modified:   21/May/2014
        /// description      Add parameter sRole 
        /// </summary>
        /// <returns></returns>
        public List<VehicleManifestList> GetVehicleConfirmManifestList(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
            int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy, string ListType,
             Int16 iRouteFrom, Int16 iRouteTo, string sCityFrom, string sCityTo, string sStatus, string sRole, 
             Int32 iBrandID, Int32 iVesselID)
        {
            List<VehicleManifestList> list = new List<VehicleManifestList>();
            if (ListType == "New")
            {                
                BLL.GetVehicleManifest(dDate, sUserID, iRegionID, iPortID, iVehicleID,
                    iStartRow, iMaxRow, iLoadType, sOrderBy, iRouteFrom, iRouteTo, sCityFrom, sCityTo, sStatus,
                    sRole, iBrandID, iVesselID);//GlobalCode.Field2String(uoHiddenFieldStatus.Value));

                if (Session["VehiclManifest_ManifestList"] != null)
                {

                    list = (List<VehicleManifestList>)Session["VehiclManifest_ManifestList"];
                    //var filtered = (from i in list.Where(i => i.colIsVisibleBit == true)
                    //                select i).ToList();
                    var filtered = (from i in list 
                                    select i).ToList();
                    list = filtered;
                }
            }
            else if (ListType == "Confirm")
            {
                if (Session["VehiclManifest_ConfirmedManifest"] != null)
                {
                    list = (List<VehicleManifestList>)Session["VehiclManifest_ConfirmedManifest"];
                    var filtered = (from i in list.Where(i => i.colIsVisibleBit == true)
                                    select i).ToList();
                    list = filtered;
                }
            }
            else if (ListType == "Cancel")
            {
                if (Session["VehiclManifest_CancelledManifest"] != null)
                {
                    list = (List<VehicleManifestList>)Session["VehiclManifest_CancelledManifest"];
                    //var filtered = (from i in list.Where(i => i.colIsVisibleBit == true)
                    //                select i).ToList();
                    //list = filtered;//(List<VehicleManifestList>)filtered;               
                }
            }
            return list;
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   09/Oct/2013
        /// description     Get New, Confirm and Cancelled Manifest Count
        /// </summary>
        /// <returns></returns>
        public int GetVehicleConfirmManifestCount(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
            Int16 iLoadType, string sOrderBy, string ListType,
            Int16 iRouteFrom, Int16 iRouteTo, string sCityFrom, string sCityTo, string sStatus, string sRole, 
            Int32 iBrandID, Int32 iVesselID)
        {
            int iCount = 0;

            if (ListType == "New")
            {
                if (Session["VehiclManifest_ManifestCount"] != null)
                {
                    iCount = GlobalCode.Field2Int(Session["VehiclManifest_ManifestCount"]);
                }
            }
            else if (ListType == "Confirm")
            {
                if (Session["VehiclManifest_ConfirmedManifesCount"] != null)
                {
                    iCount = GlobalCode.Field2Int(Session["VehiclManifest_ConfirmedManifesCount"]);
                }
            }
            else if (ListType == "Cancel")
            {
                if (Session["VehiclManifest_CancelledManifestCount"] != null)
                {
                    iCount = GlobalCode.Field2Int(Session["VehiclManifest_CancelledManifestCount"]);
                }
            }
            return iCount;
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       09/Oct/2013
        /// Description:        Setup buttons depending on ListView
        /// </summary>
        private void ButtonSettings()
        {
            uoButtonConfirm.Visible = false;
            uoButtonExport.Visible = false;
            uoButtonAssign.Visible = false;
            //uoButtonAssignConfirm.Visible = false;

            if (uoListViewManifestDetails.Items.Count > 0 || uoListViewManifestConfirm.Items.Count > 0)
            {
                uoButtonExport.Visible = true;

                if (GlobalCode.Field2Int(uoDropDownListVehicle.SelectedValue) > 0)
                {
                    uoButtonAssign.Visible = true;
                    uoButtonConfirm.Visible = true;
                }
            }

            if (uoListViewManifestConfirm.Items.Count > 0)
            {
                //uoButtonExport.Visible = true;

                if (GlobalCode.Field2Int(uoDropDownListVehicle.SelectedValue) > 0)
                {
                    //uoButtonAssignConfirm.Visible = true;
                }
            }
            if (uoListViewManifestCancel.Items.Count > 0)
            {
                uoButtonExport.Visible = true;
                if (GlobalCode.Field2Int(uoDropDownListVehicle.SelectedValue) > 0)
                {
                    uoButtonConfirm.Visible = true;
                }
            }            
        }   
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   31/Oct/2013
        /// Description:    create the file to be exported
        /// ---------------------------------------------
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt, DataTable dtConfirmed, DataTable dtCancelled)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/VehicleManifest/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");

                string FileName = "VehicleManifest_" + sDateManifest + '_' + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName, dtConfirmed, dtCancelled, uoDropDownListVehicle.SelectedItem.Text);
                OpenExcelFile(FileName, strFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/Apr/2013
        /// Description:    create the file to be exported
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  31/Oct/2013
        /// Description:    Create Excel file         
        /// </summary>        
        public static void CreateExcel(DataTable dtSource, string strFileName, DataTable dtConfirmed, DataTable dtCancelled, string sVehicleName)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    string sBranchName = sVehicleName;// dtSource.Rows[0]["HotelBranch"].ToString().TrimEnd();
                    int iLength = sBranchName.Length;
                    int iLengthRemove = iLength - 20;
                    if (iLength > 20)
                    {
                        sBranchName = sBranchName.Remove(20, iLengthRemove);
                    }
                    int iColCount = dtSource.Columns.Count + 1;
                    //Format the output file for reading easier
                    xtwWriter.Formatting = Formatting.Indented;

                    // <?xml version="1.0"?>
                    xtwWriter.WriteStartDocument();

                    // <?mso-application progid="Excel.Sheet"?>
                    xtwWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

                    // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
                    xtwWriter.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

                    //Write definition of namespace
                    xtwWriter.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
                    xtwWriter.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
                    xtwWriter.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
                    xtwWriter.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

                    // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                    xtwWriter.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

                    // Write document properties
                    xtwWriter.WriteElementString("Author", "Travelmart");
                    xtwWriter.WriteElementString("LastAuthor", Environment.UserName);
                    xtwWriter.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
                    xtwWriter.WriteElementString("Company", "RCCL");
                    xtwWriter.WriteElementString("Version", "1");

                    // </DocumentProperties>
                    xtwWriter.WriteEndElement();

                    // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of workbook
                    xtwWriter.WriteElementString("WindowHeight", "13170");
                    xtwWriter.WriteElementString("WindowWidth", "17580");
                    xtwWriter.WriteElementString("WindowTopX", "120");
                    xtwWriter.WriteElementString("WindowTopY", "60");
                    xtwWriter.WriteElementString("ProtectStructure", "False");
                    xtwWriter.WriteElementString("ProtectWindows", "False");

                    // </ExcelWorkbook>
                    xtwWriter.WriteEndElement();

                    // <Styles>
                    xtwWriter.WriteStartElement("Styles");

                    // <Style ss:ID="Default" ss:Name="Normal">
                    xtwWriter.WriteStartElement("Style");
                    xtwWriter.WriteAttributeString("ss", "ID", null, "Default");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Normal");

                    // <Alignment ss:Vertical="Bottom"/>
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();

                    // Write null on the other properties
                    xtwWriter.WriteElementString("Borders", null);
                    xtwWriter.WriteElementString("Font", null);
                    xtwWriter.WriteElementString("Interior", null);
                    xtwWriter.WriteElementString("NumberFormat", null);
                    xtwWriter.WriteElementString("Protection", null);
                    // </Style>
                    xtwWriter.WriteEndElement();

                    //Style for header
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s62">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s62");
                    xtwWriter.WriteStartElement("Font");
                    // <Font ss:Bold="1"/>
                    xtwWriter.WriteAttributeString("ss", "Bold", null, "1");
                    //end of font
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    //Style for total summary numbers
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s64");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Right");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    //Style for Rows
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s65");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    //End Style for Rows
                    xtwWriter.WriteEndElement();

                    // </Styles>
                    xtwWriter.WriteEndElement();

                    int i = 1;
                    int iRow = 15;

                    if (dtSource.Rows.Count > 0)
                    {
                        #region "New Manifest"

                        // <Worksheet ss:Name="xxx">
                        xtwWriter.WriteStartElement("Worksheet");
                        xtwWriter.WriteAttributeString("ss", "Name", null, sBranchName);

                        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                        xtwWriter.WriteStartElement("Table");

                        iRow = dtSource.Rows.Count + 15;

                        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

                        //Header
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");

                        foreach (DataColumn Header in dtSource.Columns)
                        {
                            if (i <= iColCount && i > 1)
                            {
                                xtwWriter.WriteStartElement("Cell");
                                // xxx
                                xtwWriter.WriteStartElement("Data");
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                // Write content of cell
                                xtwWriter.WriteValue(Header.ColumnName);
                                xtwWriter.WriteEndElement();
                                xtwWriter.WriteEndElement();
                            }
                            i++;
                        }
                        xtwWriter.WriteEndElement();


                        // Run through all rows of data source

                        //dtSource.RowDeleted();

                        foreach (DataRow row in dtSource.Rows)
                        {
                            // <Row>
                            xtwWriter.WriteStartElement("Row");
                            xtwWriter.WriteAttributeString("ss", "StyleID", null, "s65");

                            i = 1;
                            // Run through all cell of current rows
                            foreach (object cellValue in row.ItemArray)
                            {
//                                IsVisibleToVendor
//dtSource.Columns[]
                                if (i <= iColCount && i > 1)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNITES" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "SEQNO" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHTNO")
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    //check cost center if number or not
                                    else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                    {
                                        if (GlobalCode.Field2Int(cellValue) > 0)
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                        }
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }

                                    // Write content of cell
                                    xtwWriter.WriteValue(cellValue);

                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
                                }
                                i++;
                            }
                            // </Row>
                            xtwWriter.WriteEndElement();

                        }

                        // </Table>
                        xtwWriter.WriteEndElement();

                        // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                        xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                        // Write settings of page
                        xtwWriter.WriteStartElement("PageSetup");
                        xtwWriter.WriteStartElement("Header");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("Footer");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("PageMargins");
                        xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                        xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteEndElement();

                        // <Selected/>
                        xtwWriter.WriteElementString("Selected", null);

                        // <Panes>
                        xtwWriter.WriteStartElement("Panes");

                        // <Pane>
                        xtwWriter.WriteStartElement("Pane");

                        // Write settings of active field
                        xtwWriter.WriteElementString("Number", "1");
                        xtwWriter.WriteElementString("ActiveRow", "1");
                        xtwWriter.WriteElementString("ActiveCol", "1");

                        // </Pane>
                        xtwWriter.WriteEndElement();

                        // </Panes>
                        xtwWriter.WriteEndElement();

                        // <ProtectObjects>False</ProtectObjects>
                        xtwWriter.WriteElementString("ProtectObjects", "False");

                        // <ProtectScenarios>False</ProtectScenarios>
                        xtwWriter.WriteElementString("ProtectScenarios", "False");

                        // </WorksheetOptions>
                        xtwWriter.WriteEndElement();

                        // </Worksheet>
                        xtwWriter.WriteEndElement();

                        #endregion
                    }

                    if (dtConfirmed.Rows.Count > 0)
                    {
                        //=======================================CONFIRMED SHEET===============================================
                        #region COMNFIRMED SHEET

                        iColCount = (dtConfirmed.Columns.Count + 1);
                        iRow = dtConfirmed.Rows.Count + 15;

                        // <Worksheet ss:Name="xxx">
                        xtwWriter.WriteStartElement("Worksheet");
                        xtwWriter.WriteAttributeString("ss", "Name", null, "Confirmed Manifest");

                        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                        xtwWriter.WriteStartElement("Table");

                        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                        //Header
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                        i = 1;
                        foreach (DataColumn Header in dtConfirmed.Columns)
                        {
                            if (i <= iColCount && i > 1)
                            {
                                xtwWriter.WriteStartElement("Cell");
                                // xxx
                                xtwWriter.WriteStartElement("Data");
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                // Write content of cell
                                xtwWriter.WriteValue(Header.ColumnName);
                                xtwWriter.WriteEndElement();
                                xtwWriter.WriteEndElement();
                            }
                            i++;
                        }
                        xtwWriter.WriteEndElement();


                        // Run through all rows of data source
                        foreach (DataRow row in dtConfirmed.Rows)
                        {
                            // <Row>
                            xtwWriter.WriteStartElement("Row");

                            i = 1;
                            // Run through all cell of current rows
                            foreach (object cellValue in row.ItemArray)
                            {
                                if (i <= iColCount && i > 1)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtConfirmed.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                        dtConfirmed.Columns[i - 1].Caption.ToUpper() == "HOTELNITES" ||
                                        dtConfirmed.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                        dtConfirmed.Columns[i - 1].Caption.ToUpper() == "VOUCHER" ||
                                        dtConfirmed.Columns[i - 1].Caption.ToUpper() == "SEQNO" ||
                                        dtConfirmed.Columns[i - 1].Caption.ToUpper() == "FLIGHTNO")
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    //check cost center if number or not
                                    else if (dtConfirmed.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                    {
                                        if (GlobalCode.Field2Int(cellValue) > 0)
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                        }
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }
                                    // Write content of cell
                                    xtwWriter.WriteValue(cellValue);

                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
                                }
                                i++;
                            }
                            // </Row>
                            xtwWriter.WriteEndElement();
                        }
                        // </Table>
                        xtwWriter.WriteEndElement();

                        // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                        xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                        // Write settings of page
                        xtwWriter.WriteStartElement("PageSetup");
                        xtwWriter.WriteStartElement("Header");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("Footer");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("PageMargins");
                        xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                        xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteEndElement();

                        // <Selected/>
                        xtwWriter.WriteElementString("Selected", null);

                        // <Panes>
                        xtwWriter.WriteStartElement("Panes");

                        // <Pane>
                        xtwWriter.WriteStartElement("Pane");

                        // Write settings of active field
                        xtwWriter.WriteElementString("Number", "1");
                        xtwWriter.WriteElementString("ActiveRow", "1");
                        xtwWriter.WriteElementString("ActiveCol", "1");

                        // </Pane>
                        xtwWriter.WriteEndElement();

                        // </Panes>
                        xtwWriter.WriteEndElement();

                        // <ProtectObjects>False</ProtectObjects>
                        xtwWriter.WriteElementString("ProtectObjects", "False");

                        // <ProtectScenarios>False</ProtectScenarios>
                        xtwWriter.WriteElementString("ProtectScenarios", "False");

                        // </WorksheetOptions>
                        xtwWriter.WriteEndElement();

                        // </Worksheet>
                        xtwWriter.WriteEndElement();

                        #endregion
                        //=======================================CONFIRMED SHEET===============================================
                    }

                    if (dtCancelled.Rows.Count > 0)
                    {
                        //=======================================CANCELLED SHEET===============================================
                        #region CANCELLED SHEET

                        iColCount = (dtCancelled.Columns.Count + 1);
                        iRow = dtCancelled.Rows.Count + 15;

                        // <Worksheet ss:Name="xxx">
                        xtwWriter.WriteStartElement("Worksheet");
                        xtwWriter.WriteAttributeString("ss", "Name", null, "Cancelled Manifest");

                        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                        xtwWriter.WriteStartElement("Table");

                        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                        //Header
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                        i = 1;
                        foreach (DataColumn Header in dtCancelled.Columns)
                        {
                            if (i <= iColCount && i > 1)
                            {
                                xtwWriter.WriteStartElement("Cell");
                                // xxx
                                xtwWriter.WriteStartElement("Data");
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                // Write content of cell
                                xtwWriter.WriteValue(Header.ColumnName);
                                xtwWriter.WriteEndElement();
                                xtwWriter.WriteEndElement();
                            }
                            i++;
                        }
                        xtwWriter.WriteEndElement();


                        // Run through all rows of data source
                        foreach (DataRow row in dtCancelled.Rows)
                        {
                            // <Row>
                            xtwWriter.WriteStartElement("Row");

                            i = 1;
                            // Run through all cell of current rows
                            foreach (object cellValue in row.ItemArray)
                            {
                                if (i <= iColCount && i > 1)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtCancelled.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                        dtCancelled.Columns[i - 1].Caption.ToUpper() == "HOTELNITES" ||
                                        dtCancelled.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                        dtCancelled.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    //check cost center if number or not
                                    else if (dtCancelled.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                    {
                                        if (GlobalCode.Field2Int(cellValue) > 0)
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                        }
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }
                                    // Write content of cell
                                    xtwWriter.WriteValue(cellValue);

                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
                                }
                                i++;
                            }
                            // </Row>
                            xtwWriter.WriteEndElement();
                        }
                        // </Table>
                        xtwWriter.WriteEndElement();

                        // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                        xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                        // Write settings of page
                        xtwWriter.WriteStartElement("PageSetup");
                        xtwWriter.WriteStartElement("Header");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("Footer");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("PageMargins");
                        xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                        xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteEndElement();

                        // <Selected/>
                        xtwWriter.WriteElementString("Selected", null);

                        // <Panes>
                        xtwWriter.WriteStartElement("Panes");

                        // <Pane>
                        xtwWriter.WriteStartElement("Pane");

                        // Write settings of active field
                        xtwWriter.WriteElementString("Number", "1");
                        xtwWriter.WriteElementString("ActiveRow", "1");
                        xtwWriter.WriteElementString("ActiveCol", "1");

                        // </Pane>
                        xtwWriter.WriteEndElement();

                        // </Panes>
                        xtwWriter.WriteEndElement();

                        // <ProtectObjects>False</ProtectObjects>
                        xtwWriter.WriteElementString("ProtectObjects", "False");

                        // <ProtectScenarios>False</ProtectScenarios>
                        xtwWriter.WriteElementString("ProtectScenarios", "False");

                        // </WorksheetOptions>
                        xtwWriter.WriteEndElement();

                        // </Worksheet>
                        xtwWriter.WriteEndElement();

                        #endregion
                        //=======================================CANCELLED SHEET===============================================
                    }

                    // </Workbook>
                    xtwWriter.WriteEndElement();

                    // Write file on hard disk
                    xtwWriter.Flush();
                    xtwWriter.Close();

                    //FileInfo FileName = new FileInfo(strFileName);
                    //FileStream fs = new FileStream(FileName.FullName, FileMode.Create);                
                    //fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSource != null)
                {
                    dtSource.Dispose();
                }
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  31/Oct/2013
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/VehicleManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
        }

        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  14/Nov/2013
        /// Description:    Bind Vehicle Route
        /// </summary>
        private void BindRoute()
        {
            List<VehicleRoute> list = new List<VehicleRoute>();
            if (Session["VehiclManifest_VehicleRoute"] != null)
            {
                list = (List<VehicleRoute>)Session["VehiclManifest_VehicleRoute"];
            }
            
            uoDropDownListRouteFrom.Items.Clear();
            uoDropDownListRouteFrom.Items.Insert(0, new ListItem("--Select Route From--", "0"));
            uoDropDownListRouteFrom.DataSource = list;
            uoDropDownListRouteFrom.DataTextField = "RouteDesc";
            uoDropDownListRouteFrom.DataValueField= "RouteID";
            uoDropDownListRouteFrom.DataBind();

            uoDropDownListRouteTo.Items.Clear();
            uoDropDownListRouteTo.Items.Insert(0, new ListItem("--Select Route To--", "0"));
            uoDropDownListRouteTo.DataSource = list;
            uoDropDownListRouteTo.DataTextField = "RouteDesc";
            uoDropDownListRouteTo.DataValueField = "RouteID";
            uoDropDownListRouteTo.DataBind();
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  15/Nov/2013
        /// Description:    Bind City From and City To
        /// </summary>
        private void BindCity(Int16 iLoadType)
        {
          
            List<AirportDTO> listAir = new List<AirportDTO>();
            List<SeaportDTO> listSea = new List<SeaportDTO>();

            uoDropDownListCityFrom.Items.Clear();
            uoDropDownListCityTo.Items.Clear();

            uoDropDownListCityFrom.Items.Add(new ListItem("--Select City From--","0"));
            uoDropDownListCityTo.Items.Add(new ListItem("--Select City To--", "0"));           

            //FROM CITY
            if (uoHiddenFieldRouteFrom.Value == "1") // Ship
            {
                listSea = VehicleBLL.GetVehicleVendorSeaportList(GlobalCode.Field2Int(uoHiddenFieldVehicle.Value), 0);
                uoDropDownListCityFrom.DataSource = listSea; ;
                uoDropDownListCityFrom.DataTextField = "SeaportNameString";
                uoDropDownListCityFrom.DataValueField = "SeaportCodeString";
                uoDropDownListCityFrom.DataBind();
            }
            else if (uoHiddenFieldRouteFrom.Value == "2" || uoHiddenFieldRouteFrom.Value == "3") // Hotel, Airport
            {
                listAir = VehicleBLL.GetVehicleVendorAirportList(GlobalCode.Field2Int(uoHiddenFieldVehicle.Value), 0);
                uoDropDownListCityFrom.DataSource = listAir; ;
                uoDropDownListCityFrom.DataTextField = "AirportNameString";
                uoDropDownListCityFrom.DataValueField = "AirportCodeString";
                uoDropDownListCityFrom.DataBind();
            }

            //TO CITY
            if (uoHiddenFieldRouteTo.Value == "1") // Ship
            {
                listSea = VehicleBLL.GetVehicleVendorSeaportList(GlobalCode.Field2Int(uoHiddenFieldVehicle.Value), 0);
                uoDropDownListCityTo.DataSource = listSea; ;
                uoDropDownListCityTo.DataTextField = "SeaportNameString";
                uoDropDownListCityTo.DataValueField = "SeaportCodeString";
                uoDropDownListCityTo.DataBind();
            }
            else if (uoHiddenFieldRouteTo.Value == "2" || uoHiddenFieldRouteTo.Value == "3") // Hotel, Airport
            {
                listAir = VehicleBLL.GetVehicleVendorAirportList(GlobalCode.Field2Int(uoHiddenFieldVehicle.Value), 0);
                uoDropDownListCityTo.DataSource = listAir; ;
                uoDropDownListCityTo.DataTextField = "AirportNameString";
                uoDropDownListCityTo.DataValueField = "AirportCodeString";
                uoDropDownListCityTo.DataBind();
            }

            //Set default value
            if (uoDropDownListCityFrom.Items.FindByValue(uoHiddenFieldCityFrom.Value) != null)
            {
                uoDropDownListCityFrom.SelectedValue = uoHiddenFieldCityFrom.Value;
            }
            if (uoDropDownListCityTo.Items.FindByValue(uoHiddenFieldCityTo.Value) != null)
            {
                uoDropDownListCityTo.SelectedValue = uoHiddenFieldCityTo.Value;
            }           
        }
        /// <summary>
        /// Created By:    Josephine Gad
        /// Date Created:  11/Feb/2014
        /// Description:   Count Settings
        /// </summary>
        private void CountSettings()
        {
            List<VehicleCountList> list = new List<VehicleCountList>();
            if (Session["VehiclManifest_VehicleCountList"] != null)
            {
                list = (List<VehicleCountList>)Session["VehiclManifest_VehicleCountList"];
            }
            uoLabelONCount.Text = "0";
             uoLabelOFFCount.Text = "0";
                uoLabelOFFCountPotential.Text = "0";
            if (list.Count > 0)
            {
                var itemON = list.Where(data => data.Status == "ON").ToList();
                var itemOFF = list.Where(data => data.Status == "OFF").ToList();
                var itemOFFPotential = list.Where(data => data.Status == "PotentialOFF").ToList();

                if (itemON.Count > 0)
                {
                    uoLabelONCount.Text = GlobalCode.Field2Int(itemON[0].OnOffDate).ToString();
                }
                if (itemOFF.Count > 0)
                {
                    uoLabelOFFCount.Text = GlobalCode.Field2Int(itemOFF[0].OnOffDate).ToString();
                }
                if (itemOFFPotential.Count > 0)
                {
                    uoLabelOFFCountPotential.Text = GlobalCode.Field2Int(itemOFFPotential[0].OnOffDate).ToString();
                }
            }            
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   19/Nov/2014
        /// Description:    Bind Brand list to drop down list
        /// </summary>
        private void BindBrand()
        {
            List<BrandList> listBrand = new List<BrandList>();
            if (Session["HotelDashboardDTO_BrandList"] != null)
            {
                listBrand = (List<BrandList>)Session["HotelDashboardDTO_BrandList"];
            }
            else
            {
                MasterfileBLL BLL = new MasterfileBLL();
                listBrand = BLL.GetBrandList();
            }
            Session["HotelDashboardDTO_BrandList"] = listBrand;

            uoDropDownListBrand.Items.Clear();
            uoDropDownListBrand.Items.Add(new ListItem("--Select Brand--", "0"));
            uoDropDownListBrand.DataBind();

            if (listBrand.Count > 0)
            {
                uoDropDownListBrand.DataSource = listBrand;
                uoDropDownListBrand.DataTextField = "BrandName";
                uoDropDownListBrand.DataValueField = "BrandID";
                uoDropDownListBrand.DataBind();
            }
            string sBrand = GlobalCode.Field2String(Session["Brand"]);
            if (sBrand != "")
            {
                if (uoDropDownListBrand.Items.FindByValue(sBrand) != null)
                {
                    uoDropDownListBrand.SelectedValue = sBrand;
                }
            }
            uoHiddenFieldBrandID.Value = uoDropDownListBrand.SelectedValue;
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   19/Nov/2014
        /// Description:    Bind Vessel list to drop down list
        /// </summary>
        private void BindVessel()
        {
            List<VesselList> list = new List<VesselList>();
            if (Session["HotelDashboardDTO_VesselList"] != null)
            {
                list = (List<VesselList>)Session["HotelDashboardDTO_VesselList"];
            }
            else
            {
                MasterfileBLL BLL = new MasterfileBLL();
                list = BLL.GetVesselList(uoDropDownListBrand.SelectedValue);
            }
            Session["HotelDashboardDTO_VesselList"] = list;

            uoDropDownListVessel.Items.Clear();
            uoDropDownListVessel.Items.Add(new ListItem("--Select Vessel--", "0"));
            uoDropDownListVessel.DataBind();

            if (list.Count > 0)
            {
                uoDropDownListVessel.DataSource = list;
                uoDropDownListVessel.DataTextField = "VesselName";
                uoDropDownListVessel.DataValueField = "VesselID";
                uoDropDownListVessel.DataBind();
            }
            string sVessel = GlobalCode.Field2String(Session["Vessel"]);
            if (sVessel != "")
            {
                if (uoDropDownListVessel.Items.FindByValue(sVessel) != null)
                {
                    uoDropDownListVessel.SelectedValue = sVessel;
                }
            }
            uoHiddenFieldVesselID.Value = uoDropDownListVessel.SelectedValue;
        }
        #endregion

       
    }
}

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace TRAVELMART
{
    public partial class VehicleMaintenanceView : System.Web.UI.Page
    {

        #region Events
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                GetVehicleVendorList();

                BindRegion();
                BindBrand();
                BindSeaport(0);

                //uoHyperLinkVehicleAdd.HRef = "~/Maintenance/VehicleMaintenance.aspx?vmId=0&vmType=VE&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];

                if (User.IsInRole(TravelMartVariable.RoleAdministrator))
                {
                    uoBtnVehicleAdd.Visible = true;
                }
                else if (User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                {
                    uoHiddenFieldVendor.Value = "false";
                }
                else
                {
                    uoBtnVehicleAdd.Visible = false;
                    uoHiddenFieldVendor.Value = "false";
                    uoHiddenFieldShowEditContractAddCol.Value = "false";
                }
            }
            else
            {

                if (uoHiddenFieldPopupVehicle.Value == "1")
                {
                    GetVehicleVendorList();
                }
                uoHiddenFieldPopupVehicle.Value = "0";
            }
        } 
      
        protected void uoVehicleVendorList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = GlobalCode.Field2Int(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeleteVehicleVendor(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle vendor branch deleted. (flagged as inactive)";
                strFunction = "uoVehicleVendorList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetVehicleVendorList();
            }
            else if (e.CommandName != "")
            {
                uoHiddenFieldSortBy.Value = e.CommandName;
                GetVehicleVendorList();
            }
            else
            {
                uoHiddenFieldSortBy.Value = "SortByName";
            }

        }

        protected void uoVehicleVendorList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }

        protected void uoVehicleVendorListPager_PreRender(object sender, EventArgs e)
        {
            //GetVehicleVendorListByUser();
        }

        protected void uoObjectDataSourceVehicle_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sVehicleVendorName"] = uoTextBoxSearchParam.Text;
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            Session["Brand"] = uoDropDownListBrand.SelectedValue;
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["SeaportID"] = uoDropDownListSeaport.SelectedValue;
            uoHiddenFieldLoadType.Value = "0";
            GetVehicleVendorList();
        }
        protected void uoBtnVehicleAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("VehicleMaintenance.aspx?vmId=0&vmType=VE&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
       
        protected void uoVehicleVendorList_DataBound(object sender, EventArgs e)
        {
            HtmlControl EditTH = (HtmlControl)uoVehicleVendorList.FindControl("EditTH");
            HtmlControl ContractTH = (HtmlControl)uoVehicleVendorList.FindControl("ContractTH");
            HtmlControl AddContractTH = (HtmlControl)uoVehicleVendorList.FindControl("AddContractTH");

            if (EditTH != null)
            {
                if (User.IsInRole(TravelMartVariable.RoleAdministrator))
                {

                    EditTH.Visible = true;
                    ContractTH.Visible = true;
                    AddContractTH.Visible = true;
                }
                else if (User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                {

                    EditTH.Visible = true;
                    ContractTH.Visible = false;
                    AddContractTH.Visible = false;
                }
                else
                {

                    EditTH.Visible = false;
                    ContractTH.Visible = false;
                    AddContractTH.Visible = false;

                    if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                    {
                        ContractTH.Visible = true;
                    }
                }
            }
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
            BindSeaport(1);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created:   04/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get the list of vehicle vendors    
        /// ----------------------------------------------
        /// Date Modified:  28/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change uoDropDownListRegion.SelectedValue to GlobalCode.Field2String(Session["Region"])
        /// ----------------------------------------------
        /// Date Modified:  02/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change uoVehicleVendorList.DataSource to uoObjectDataSourceVehicle.DataBind()
        /// </summary>
        private void GetVehicleVendorList()
        {
            //uoVehicleVendorList.DataSource = MaintenanceViewBLL.GetVehicleVendorListByUser(strVehicleName, GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["Region"]),
            //    GlobalCode.Field2String(Session["UserVendor"]), GlobalCode.Field2String(Session["UserRole"]));
            //uoVehicleVendorList.DataBind();
            
            //            uoObjectDataSourceVehicle.DataBind();
            uoVehicleVendorList.DataSourceID = "uoObjectDataSourceVehicle";
            uoVehicleVendorList.DataBind();


        }

        /// <summary>
        /// Date Created:   01/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Region
        /// </summary>
        private void BindRegion()
        {
            List<RegionList> listRegion = new List<RegionList>();
            if (Session["VehicleVendor_Region"] != null)
            {
                listRegion = (List<RegionList>)Session["VehicleVendor_Region"];
            }
            uoDropDownListRegion.Items.Clear();
            ListItem item = new ListItem("--Select Region--", "0");
            uoDropDownListRegion.Items.Add(item);

            item = new ListItem("--No Region--", "-1");
            uoDropDownListRegion.Items.Add(item);

            item = new ListItem("--With Region--", "-2");
            uoDropDownListRegion.Items.Add(item);

            uoDropDownListRegion.DataSource = listRegion;
            uoDropDownListRegion.DataTextField = "RegionName";
            uoDropDownListRegion.DataValueField = "RegionId";
            uoDropDownListRegion.DataBind();

            string sRegion = GlobalCode.Field2String(Session["Region"]);
            if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
            {
                uoDropDownListRegion.SelectedValue = sRegion;
            }
        }
        /// <summary>
        /// Date Created:   01/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Brand
        /// </summary>
        private void BindBrand()
        {
            List<BrandList> listBrand = new List<BrandList>();
            if (Session["VehicleVendor_Brand"] != null)
            {
                listBrand = (List<BrandList>)Session["VehicleVendor_Brand"];
            }
            uoDropDownListBrand.Items.Clear();
            ListItem item = new ListItem("--Select Brand--", "0");
            uoDropDownListBrand.Items.Add(item);

            uoDropDownListBrand.DataSource = listBrand;
            uoDropDownListBrand.DataTextField = "BrandName";
            uoDropDownListBrand.DataValueField = "BrandID";
            uoDropDownListBrand.DataBind();

            string sBrand = GlobalCode.Field2String(Session["Brand"]);
            if (uoDropDownListBrand.Items.FindByValue(sBrand) != null)
            {
                uoDropDownListBrand.SelectedValue = sBrand;
            }
        }
        /// <summary>
        /// Date Created:   17/Jul/2014
        /// Created By:     Josephine Monteza
        /// (description)   Bind Seaport
        /// </summary>
        private void BindSeaport(Int16 iLoadType)
        {
            List<SeaportDTO> list = new List<SeaportDTO>();      

            if(iLoadType == 0)
            {
                if (Session["VehicleVendor_Seaport"] != null)
                {
                    list = (List<SeaportDTO>)Session["VehicleVendor_Seaport"];
                }
            }
            else
            {
                List<PortList> listSeaport = new List<PortList>();
                
                string sRegion = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue).ToString();
                listSeaport = PortBLL.GetPortListByRegion(uoHiddenFieldUser.Value, uoDropDownListRegion.SelectedValue, "", "");

                list = (from a in listSeaport
                        select new SeaportDTO
                        {
                            SeaportIDString = a.PortId.ToString(),
                            SeaportNameString = a.PortName

                        }).ToList();
            }
            uoDropDownListSeaport.Items.Clear();
            ListItem item = new ListItem("--Select Seaport--", "0");
            uoDropDownListSeaport.Items.Add(item);

            uoDropDownListSeaport.DataSource = list;
            uoDropDownListSeaport.DataTextField = "SeaportNameString";
            uoDropDownListSeaport.DataValueField = "SeaportIDString";
            uoDropDownListSeaport.DataBind();

            string sSeaport = GlobalCode.Field2String(Session["SeaportID"]);
            if (uoDropDownListSeaport.Items.FindByValue(sSeaport) != null)
            {
                uoDropDownListSeaport.SelectedValue = sSeaport;
            }
        }

       
        #endregion

      
    }
}

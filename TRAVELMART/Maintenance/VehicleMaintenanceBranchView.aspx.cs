using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.UI.HtmlControls;
using System.IO;

namespace TRAVELMART.Maintenance
{
    public partial class VehicleMaintenanceBranchView : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Modified by:    Josephine Gad
        /// Date Modified:  27/10/2011
        /// Description:    Add validation to user primary role
        ///                 Hide edit, view contract, add contract columns for unauthorized person
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            SFStatus.Visible = false;
            if (!IsPostBack)
            {
                //BindRegion();
                GetVehicleVendorBranchListByUser(uoTextBoxSearchParam.Text);
                Session["strPrevPage"]  = Request.RawUrl;
                Session["VehiclePath"] = Path.GetFileName(Request.Path);
                //uoHyperLinkVehicleAdd.HRef = "~/Maintenance/VehicleMaintenanceBranch.aspx?vmId=0&vmType=VE";

                if (User.IsInRole(TravelMartVariable.RoleAdministrator)
                || User.IsInRole(TravelMartVariable.RoleCrewAssist)
                || User.IsInRole(TravelMartVariable.RoleCrewAdmin)
                || User.IsInRole(TravelMartVariable.RolePortSpecialist)
                || User.IsInRole(TravelMartVariable.RoleVehicleSpecialist)
                || User.IsInRole(TravelMartVariable.RoleContractManager)
                || User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                {
                    uoButtonVehicleAdd.Visible = true;
                }
                else
                {
                    uoButtonVehicleAdd.Visible = false;
                    uoHiddenFieldVendor.Value = "false";
                    uoHiddenFieldVendorClass.Value = "hideElement";
                    HtmlControl EditTH = (HtmlControl)uoVehicleVendorList.FindControl("EditTH");
                    if (EditTH != null)
                    {
                        EditTH.Style.Add("display", "none");
                    }
                }

                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {
                    Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                }
                string UserRole = GlobalCode.Field2String(Session["UserRole"]);;

                if (UserRole == TravelMartVariable.RoleAdministrator
                || UserRole == TravelMartVariable.RoleCrewAssist
                || UserRole == TravelMartVariable.RoleCrewAdmin
                || UserRole == TravelMartVariable.RoleVehicleSpecialist
                || UserRole == TravelMartVariable.RoleContractManager)
                {
                    uoHiddenFieldViewContract.Value = "true";
                }
                else
                {
                    uoHiddenFieldViewContract.Value = "false";
                    uoHiddenFieldViewContractClass.Value = "hideElement";
                    HtmlControl ContractListTH = (HtmlControl)uoVehicleVendorList.FindControl("ContractListTH");
                    if (ContractListTH != null)
                    {
                        ContractListTH.Style.Add("display", "none");
                    }
                }

                if (User.IsInRole(TravelMartVariable.RoleAdministrator)
                    || User.IsInRole(TravelMartVariable.RoleContractManager))
                {
                    uoHiddenFieldEditAddContract.Value = "true";
                }
                else
                {
                    uoHiddenFieldEditAddContract.Value = "false";
                    uoHiddenFieldEditAddContractClass.Value = "hideElement";
                    HtmlControl ContractTH = (HtmlControl)uoVehicleVendorList.FindControl("ContractTH");
                    if (ContractTH != null)
                    {
                        ContractTH.Style.Add("display", "none");
                    }
                }
            }
        }

        protected void uoButtonVehicleAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Maintenance/VehicleMaintenanceBranch.aspx?st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&vmId=0&vmType=VE" + "&dt=" + Request.QueryString["dt"]);            
        }

        protected void uoVehicleVendorListPager_PreRender(object sender, EventArgs e)
        {
            GetVehicleVendorBranchListByUser(uoTextBoxSearchParam.Text);
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {

        }

        protected void uoVehicleVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void uoVehicleVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion


        #region Functions
        /// <summary>
        /// Date Created:   08/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get the list of vehicle vendors branch     
        /// ----------------------------------------------
        /// Date Modified:  28/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change uoDropDownListRegion.SelectedValue to GlobalCode.Field2String(Session["Region"])
        /// </summary>
        private void GetVehicleVendorBranchListByUser(string strVehicleName)
        {
            uoVehicleVendorList.DataSource = MaintenanceViewBLL.GetVehicleVendorBranchListByUser(strVehicleName, GlobalCode.Field2String(Session["UserName"]),
                GlobalCode.Field2String(Session["Region"]),  Session["Country"] .ToString(),
                 Session["City"] .ToString(),  Session["Port"] .ToString(),  Session["Hotel"] .ToString(), GlobalCode.Field2String(Session["UserRole"]));

            uoVehicleVendorList.DataBind();
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

        /// <summary>
        /// Date Created: 03/08/2011
        /// Created By: Gabriel Oquialda
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
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART.Maintenance
{
    public partial class VehicleTypeBranch : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldVehicleId.Value = Request.QueryString["vID"];
                uoHiddenFieldBranchId.Value = Request.QueryString["vmID"];

                vehicleGetType();
                textChangeToUpperCase(uoDropDownListVehicleType);

                vehicleTypeBranchInfoLoad(uoHiddenFieldVehicleId.Value);
            }        
        }

        /// <summary> 
        /// Date Created:   06/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Insert/Update vehicle type for branch
        /// </summary>
        protected void uoButtonSaveVehicleType_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            Int32 pVehicleID =  VendorMaintenanceBLL.vehicleInsertUpdateVehicleTypeBranch(Convert.ToInt32(uoHiddenFieldVehicleId.Value), Convert.ToInt32(uoDropDownListVehicleType.SelectedValue), uoTextBoxVehicleTypeName.Text,
                Convert.ToInt32(uoHiddenFieldBranchId.Value), Convert.ToInt32(uoTextBoxVehicleCapacity.Text), GlobalCode.Field2String(Session["UserName"]));

            if (Request.QueryString["vID"] == "0" || Request.QueryString["vID"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle type added.";
                strFunction = "uoButtonSaveVehicleType_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(pVehicleID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle type updated.";
                strFunction = "uoButtonSaveVehicleType_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(pVehicleID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            OpenParentPage();
        }
        #endregion


        #region Functions
        /// <summary>
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vehicle type branch information            
        /// </summary>
        private void vehicleTypeBranchInfoLoad(String vehicleId)
        { 
            if (vehicleId != "0")
            {
                DataTable dtVendorInfo = VendorMaintenanceBLL.vehicleTypeBranchInfoLoad(Int32.Parse(vehicleId));
                if (dtVendorInfo.Rows.Count > 0)
                {
                    uoDropDownListVehicleType.SelectedValue = dtVendorInfo.Rows[0]["colVehicleTypeIdInt"].ToString();
                    uoTextBoxVehicleTypeName.Text = dtVendorInfo.Rows[0]["colVehicleNameVarchar"].ToString();
                    uoTextBoxVehicleCapacity.Text = dtVendorInfo.Rows[0]["colVehicleCapacityInt"].ToString();
                }
            }
        }

        /// <summary> 
        /// Date Created:   24/08/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Load vehicle type to dropdownlist
        /// ----------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change vehicleGetType to vehicleGetTypeBrandMake          
        /// </summary>
        private void vehicleGetType()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = VendorMaintenanceBLL.vehicleGetTypeList();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVehicleType.DataSource = dt;
                    uoDropDownListVehicleType.DataTextField = "vehicleType";
                    uoDropDownListVehicleType.DataValueField = "vehicleTypeID";
                    uoDropDownListVehicleType.DataBind();
                }
                else
                {
                    uoDropDownListVehicleType.DataBind();
                }

                uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListVehicleType.SelectedIndex = 1;
                }
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
            }
        }

        private void textChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page
        /// -------------------------------------------------------------------------------
        /// Date Modified: 06/10/2011
        /// Modified By: Gabriel Oquialda
        /// (description) Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"
        ///               to "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicle\"            
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicle\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSaveVehicleType, this.GetType(), "scr", sScript, false);
        }
        #endregion        
    }
}

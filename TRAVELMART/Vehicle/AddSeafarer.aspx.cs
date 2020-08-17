using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.Vehicle
{
    public partial class AddSeafarer : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strSFStatus"] = Request.QueryString["st"];
                Session["strRecordLocator"] = Request.QueryString["recloc"].ToString();

                seafarerGetName();
                vehicleGetCompany();
                vehicleGetType();
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 27/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Save vehicle transaction            
            /// </summary>
    
            //try
            //{                
            //    string vendorId = uoDropDownListVehicleCompany.SelectedValue;                
            //    string vehicleType = uoDropDownListVehicleType.SelectedValue;
            //    string seafarerStatus = GlobalCode.Field2String(Session["strSFStatus"]);
            //    string travelLocatorId = GlobalCode.Field2String(Session["strTravelLocatorID"]);

            //    if (Request.QueryString["vId"] == "")
            //    {
            //        if (pickupdatetimeInInsertExist() == false)
            //        {
            //            //VehicleBLL.vehicleInsertTransaction(travelLocatorId, Convert.ToInt32(vendorId),
            //            //            Convert.ToInt32(vehicleType), uoTextBoxPlateNo.Text, Convert.ToDateTime(uoTextBoxPickUpDate.Text),
            //            //            uoTextBoxPickupTime.Text, Convert.ToDateTime(uoTextBoxDropOffDate.Text), uoTextBoxDropoffTime.Text,
            //            //            uoTextBoxPickUpPlace.Text, uoTextBoxDropOffPlace.Text, uoDropDownListVehicleStatus.Text,
            //            //            MUser.GetUserName(), seafarerStatus, uoTextBoxRemarks.Text, Convert.ToBoolean(uoCheckBoxBilledToCrew.Checked), Convert.ToInt32(uoHiddenFieldSeafarerID.Value));

            //            //OpenParentPage();
            //        }
            //        else if (pickupdatetimeInInsertExist() == true)
            //        {
            //            string sScript = "<script language=JavaScript>";
            //            sScript += "alert('Please select another pick-up date and time.');";
            //            sScript += "</script>";
            //            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "Prompt", sScript);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    AlertMessage(ex.Message);
            //}
        }
        
        protected void uoDropDownListSeafarerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 27/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Loads brand, vessel, and rank by seafarer id            
            /// </summary>

            if (uoDropDownListSeafarerName.SelectedValue != "0")
            {
                GetSeafarerBySeafarerId(uoDropDownListSeafarerName.SelectedValue);
                uoHiddenFieldSeafarerID.Value = uoDropDownListSeafarerName.SelectedValue;
            }
            else
            {
                uoTextBoxBrand.Text = "";
                uoTextBoxVessel.Text = "";
                uoTextBoxRank.Text = "";
            }
        }

        protected void uoDropDownListVehicleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 27/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Show/Hide bill to crew checkbox for cancelled vehicle status            
            /// </summary>
      
            if (uoDropDownListVehicleStatus.SelectedValue == "Cancelled")
            {
                uoCheckBoxBilledToCrew.Visible = true;
            }
            else
            {
                uoCheckBoxBilledToCrew.Checked = false;
                uoCheckBoxBilledToCrew.Visible = false;
            }
        }
        #endregion
        
        #region Functions
        private void seafarerGetName()
        {
            /// <summary>
            /// Date Created: 27/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Load seafarer name to dropdownlist            
            /// </summary>
      
            DataTable dt = new DataTable(); 
            dt = SeafarerBLL.GetSeafarer();

            if (dt.Rows.Count > 0)
            {
                uoDropDownListSeafarerName.DataSource = dt;
                uoDropDownListSeafarerName.DataTextField = "NAME";
                uoDropDownListSeafarerName.DataValueField = "colSeafarerIdInt";
                uoDropDownListSeafarerName.DataBind();
                uoDropDownListSeafarerName.Items.Insert(0, new ListItem("- Select a Seafarer -", "0"));
            }
        }

        private void GetSeafarerBySeafarerId(string seafarerId)
        {
            /// <summary>
            /// Date Created: 27/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Load brand, vessel, and rank 
            ///               information by seafarer id            
            /// </summary>
         
            //IDataReader dr = SeafarerBLL.SeafarerGetDetails(seafarerId,"","");
            //if (dr.Read())
            //{
            //    uoTextBoxBrand.Text = dr["Brand"].ToString();
            //    uoTextBoxVessel.Text = dr["Vessel"].ToString();
            //    uoTextBoxRank.Text = dr["Rank"].ToString();
            //}
            //else
            //{
            //    uoTextBoxBrand.Text = "";
            //    uoTextBoxVessel.Text = "";
            //    uoTextBoxRank.Text = "";
            //}
        }

        private void vehicleGetCompany()
        {
            /// <summary>
            /// Date Created: 27/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Load vendor name to dropdownlist            
            /// </summary>
       
            DataTable dt = new DataTable();
            dt = VehicleBLL.vehicleGetCompany();
            uoDropDownListVehicleCompany.DataSource = dt;
            uoDropDownListVehicleCompany.DataTextField = "VendorName";
            uoDropDownListVehicleCompany.DataValueField = "VendorId";
            uoDropDownListVehicleCompany.DataBind();
        }

        private void vehicleGetType()
        {
            /// <summary> 
            /// Date Created: 12/08/2011
            /// Created By: Gabriel Oquialda
            /// (description) Load vehicle type to dropdownlist
            /// </summary>

            int VendorID = 0;
            DataTable dt = new DataTable();

            VendorID = Convert.ToInt32(uoDropDownListVehicleCompany.SelectedValue);
            dt = VehicleBLL.vehicleGetType(VendorID);            
            uoDropDownListVehicleType.DataSource = dt;
            uoDropDownListVehicleType.DataTextField = "VehicleName";
            uoDropDownListVehicleType.DataValueField = "VehicleId";
            uoDropDownListVehicleType.DataBind();
        }

        //private Boolean pickupdatetimeInInsertExist()
        //{
        //    /// <summary>
        //    /// Date Created: 05/08/2011
        //    /// Created By: Gabriel Oquialda
        //    /// (description) Validate vehicle pick-up date and time           
        //    /// </summary>

        //    //String travelLocatorId = Request.QueryString["ID"];
        //    //String SeqNo = TravelMartVariable.strSFSeqNo;
        //    //String branchId = uoDropDownListVehicleBranch.SelectedValue;
        //    //DateTime pickupDate = Convert.ToDateTime(uoTextBoxPickUpDate.Text);
        //    //String pickupTime = (uoTextBoxPickupTime.Text == null) ? "NULL" : uoTextBoxPickupTime.Text;

        //    //Boolean bValidation = VehicleBLL.pickupdatetimeExist(travelLocatorId, SeqNo, pickupDate, pickupTime);
        //    //return bValidation;
        //}        

        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Close this page and update parent page
            /// ----------------------------------------------------------------------------------
            /// Date Modified: 28/07/2011
            /// Modified By: Gabriel Oquialda
            /// (description) Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\"
            ///               to "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"            
            /// </summary>
     
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        private void AlertMessage(string s)
        {
            /// <summary>
            /// Date Created: 18/07/2011
            /// Created By: Marco Abejar
            /// (description) Show pop up message            
            /// </summary>

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        #endregion
    }
}

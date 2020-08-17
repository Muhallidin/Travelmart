using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART.Hotel
{
    public partial class AddSeafarer : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Ryan Bautista
            /// (description) Populate Seafarer details        
            /// </summary>
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                OpenParentPage();
            }

            if (!IsPostBack)
            {
                GetSeafarer();
                HotelLoad();
                HotelRoomTypeLoad();
                ChangeToUpperCase(uoDropDownListHotel);
                CityListByVendorID(Convert.ToInt32(uoDropDownListHotel.SelectedValue));
            }
            //OpenParentPage();
        }              
   
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Ryan Bautista
            /// (description) Save Seafarer details
            /// ----------------------------------------
            /// Date Modified: 28/07/2011
            /// Modified By: Gabriel Oquialda
            /// (description) Added OpenParentPage() for 
            ///               javascript fancybox close        
            /// </summary>

            string CheckInDateString = uoTextBoxCheckInDate.Text;
            string CheckInTimeString = uoTextBoxCheckInTime.Text;
            DateTime CheckInDateTime = GlobalCode.Field2DateTime(CheckInDateString + " " + CheckInTimeString);

            //if (Request.QueryString["hId"] == null)
            //{
                HotelBLL.InsertHotelBooking(Convert.ToInt32(uoHiddenFieldSfID.Value), Convert.ToInt32(uoDropDownListHotel.SelectedValue),
                                               Convert.ToInt32(uoDropDownListRoomType.SelectedValue),
                                               CheckInDateTime,
                                               Convert.ToInt32(uoTextBoxNoOfdays.Text),
                                               uoDropDownListHotelStatus.Text, GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["strSFStatus"]),
                                               Request.QueryString["SeqNo"], Convert.ToInt32(uoDropDownListLocation.SelectedValue),
                                               CheckInTimeString, uoCheckBoxBreakfast.Checked, uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked,
                                               uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked, uoCheckBoxShuttle.Checked);

                OpenParentPage();
            //}
            //else
            //{
            //    HotelBLL.UpdateHotelBooking(Int32.Parse(Request.QueryString["hId"].ToString()), Convert.ToInt32(uoDropDownListHotel.SelectedValue),
            //                                   Convert.ToInt32(uoDropDownListRoomType.SelectedValue), 
            //                                   CheckInDateTime,
            //                                   Convert.ToInt32(uoTextBoxNoOfdays.Text),
            //                                   uoDropDownListHotelStatus.Text, GlobalCode.Field2String(Session["UserName"]), Convert.ToInt32(uoDropDownListLocation.SelectedValue),
            //                                   CheckInTimeString, uoCheckBoxBreakfast.Checked, uoCheckBoxLunch.Checked, uoCheckBoxDinner.Checked,
            //                                   uoTextBoxRemarks.Text, uoCheckBoxCrewBill.Checked);
            //}
        }     

        protected void uoDropDownListSeafarer_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Ryan Bautista
            /// (description) Select Seafarer details by Seafarer ID        
            /// </summary>

            if (uoDropDownListSeafarer.SelectedValue != "0")
            {
                GetSeafarerBySfID(uoDropDownListSeafarer.SelectedValue);
                uoHiddenFieldSfID.Value = uoDropDownListSeafarer.SelectedValue;
            }
            else
            {
                uoTextBoxBrand.Text = "";
                uoTextBoxVessel.Text = "";
                uoTextBoxRank.Text = "";
            }
        }
                    
        protected void uoDropDownListHotelStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 25/07/2011
            /// Created By: Ryan Bautista
            /// (description) Select hotel status and make visible the check box
            ///               bill to crew when status cancelled        
            /// </summary>

            if (uoDropDownListHotelStatus.SelectedValue == "Cancelled")
            {
                uoCheckBoxCrewBill.Visible = true;
                //ucLabelCrewBill.Visible = true;
            }
            else
            {
                uoCheckBoxCrewBill.Checked = false;
                uoCheckBoxCrewBill.Visible = false;
                //ucLabelCrewBill.Visible = false;
            }
        }
                 
        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            /// <summary>
            /// Date Created: 25/07/2011
            /// Created By: Ryan Bautista
            /// (description) Select hotel and make all the branch available        
            /// </summary>
            
            CityListByVendorID(Convert.ToInt32(uoDropDownListHotel.SelectedValue));
        }
        #endregion

        #region Function    
        private void GetSeafarer()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Ryan Bautista
            /// (description) Populate Seafarer details in dropdown list        
            /// </summary>

            DataTable dt = SeafarerBLL.GetSeafarer();
            if (dt.Rows.Count > 0)
            {
                this.uoDropDownListSeafarer.DataSource = dt;
                this.uoDropDownListSeafarer.DataTextField = "NAME";
                this.uoDropDownListSeafarer.DataValueField = "colSeafarerIdInt";
                this.uoDropDownListSeafarer.DataBind();
                this.uoDropDownListSeafarer.Items.Insert(0, new ListItem("- Select a Seafarer -", "0"));
            }
        }

        private void GetSeafarerBySfID(string SfID)
       {
           /// <summary>
           /// Date Created: 19/07/2011
           /// Created By: Ryan Bautista
           /// (description) Populate Seafarer details in dropdown list by Seafarer ID        
           /// </summary>

            IDataReader dt = SeafarerBLL.SeafarerGetDetails(SfID, "","", true);
            if (dt.Read())
            {
                uoTextBoxBrand.Text = dt["Brand"].ToString();
                uoTextBoxVessel.Text = dt["Vessel"].ToString();
                uoTextBoxRank.Text = dt["Rank"].ToString();
            }
            else
            {
                uoTextBoxBrand.Text = "";
                uoTextBoxVessel.Text = "";
                uoTextBoxRank.Text = "";
            }
            dt.Dispose();
        }
                   
        private void HotelLoad()
        {
            /// <summary>
            /// Date Created: 25/07/2011
            /// Created By: Ryan Bautista
            /// (description) Select all hotel details        
            /// </summary>

            DataTable dt = HotelBLL.HotelVendorGetDetails();

            uoDropDownListHotel.DataSource = dt;
            uoDropDownListHotel.DataTextField = "colVendorNameVarchar";
            uoDropDownListHotel.DataValueField = "colVendorIdInt";
            uoDropDownListHotel.DataBind();
            uoDropDownListHotel.SelectedValue = dt.Rows[0]["colVendorIdInt"].ToString();
        }       
     
        private void HotelRoomTypeLoad()
        {
            /// <summary>
            /// Date Created: 25/07/2011
            /// Created By: Ryan Bautista
            /// (description) Select all hotel room type        
            /// </summary>

            uoDropDownListRoomType.DataSource = HotelBLL.HotelRoomTypeGetDetails();
            uoDropDownListRoomType.DataTextField = "colRoomType";
            uoDropDownListRoomType.DataValueField = "colRoomTypeID";
            uoDropDownListRoomType.DataBind();
        }             
     
        private void CityListByVendorID(Int32 VendorID)
        {
            /// <summary>
            /// Date Created: 25/07/2011
            /// Created By: Ryanb Bautista
            /// (description) Select city list by vendor ID        
            /// </summary>

            DataTable dt = HotelBLL.CityListByVendorID(VendorID);
            uoDropDownListLocation.DataSource = dt;
            uoDropDownListLocation.DataTextField = "colCityNameVarchar";
            uoDropDownListLocation.DataValueField = "colCityIDInt";
            uoDropDownListLocation.DataBind();
        }        
     
        private void ChangeToUpperCase(DropDownList ddl)
        {
            /// <summary>
            /// Date Created: 25/07/2011
            /// Created By: Ryan Bautista
            /// (description) Format data to uppercase
            /// </summary>

            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }       

        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// Description: Close this page and update parent page
            /// --------------------------------------------------------------------------------     
            /// Date Modified: 28/07/2011
            /// Modified By: Gabriel Oquialda
            /// Description: Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\"
            ///              to "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"        
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        #endregion

    }
}

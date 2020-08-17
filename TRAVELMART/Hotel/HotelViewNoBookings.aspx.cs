using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.Hotel
{
    public partial class HotelViewNoBookings : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/01/2012
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
                LoadDefaultValues();
                LoadDates();
                LoadHotel();
                LoadSeafarerBookings();
            }
        }
       
      
        protected void uoViewAllButton_Click(object sender, EventArgs e)
        {
            LoadSeafarerBookings();
            LoadDefaultValues();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/01/2012
        /// description: load check-in dates
        /// </summary>
        protected void LoadDates()
        {
            //DateTime checkIndate = DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"]));
            //String cDate = "";
            //ListItem item= new ListItem();
            //while (checkIndate.ToShortDateString() != (DateTime.Parse(Session["DateTo"]).ToShortDateString()))
            //{
            //    cDate = String.Format("{0:dd-MMM-yyyy}", checkIndate);
            //    item = new ListItem(cDate, cDate);
            //    uoDropDownListDate.Items.Add(item);

            //    checkIndate = checkIndate.AddDays(1);                
            //}
            //cDate = String.Format("{0:dd-MMM-yyyy}", checkIndate);
            //item = new ListItem(cDate, cDate);
            //uoDropDownListDate.Items.Add(item);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/01/2012
        /// description: load seafarer bookings
        /// </summary>
        protected void LoadSeafarerBookings()
        {
            //uoSeafarerList.DataSource = null;
            //uoSeafarerList.DataSourceID = "ObjectDataSource1";
            //uoSeafarerList.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue  
        /// Date Created: 12/01/2012
        /// Description: load default values on page load
        /// </summary>
        protected void LoadDefaultValues()
        {
            //uoDropDownListDate.SelectedIndex = 0;
            //uoDropDownListFilter.SelectedIndex = 0;
            //uoDropDownListHotel.SelectedIndex = 0;
            //uoTextBoxFilter.Text = "";
            //uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            //uoHiddenFieldEndDate.Value = Session["DateTo"];
            //uoHiddenFieldRole.Value = MUser.GetUserRole();
            //uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            //uoSeafarerList.DataSource = null;
            //uoSeafarerList.DataBind();
        }

        string lastDataFieldValue = null;
        protected string SetHotelGroupings()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel: ";
            string GroupValueString = "HotelName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                return string.Format("<tr><td class=\"group\" colspan=\"9\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        protected void LoadHotel()
        {
            //DataTable dt = null;
            try
            {
            //    uoDropDownListHotel.Items.Add(new ListItem("--Select Hotel--", "0"));
            //    dt = SuperViewBLL.GetVendor("HO", true, "0","0");
            //    if (dt.Rows.Count > 0)
            //    {
            //        uoDropDownListHotel.DataSource = dt;
            //        uoDropDownListHotel.DataTextField = "colVendorNameVarchar";
            //        uoDropDownListHotel.DataValueField = "colVendorIdInt";
            //        uoDropDownListHotel.DataBind();

            //        if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
            //        {
            //            if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["UserVendor"]))!= null)
            //            {
            //                uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["UserVendor"]);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        uoDropDownListHotel.DataBind();
            //    }
            //    //uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                
            //    if (dt.Rows.Count == 1)
            //    {
            //        uoDropDownListHotel.SelectedIndex = 1;
            //    }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
        #endregion

      

       

    }
}

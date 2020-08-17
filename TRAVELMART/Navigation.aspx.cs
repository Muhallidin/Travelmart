using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class Navigation : System.Web.UI.Page
    {
        /// <summary>
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
            }
        }

        protected void uoBtnException_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }

        protected void uoBtnResources_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HotelDashboardRoomType2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }

        protected void uoBtnOverflow_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hotel/HotelOverflowBooking3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
    }
}

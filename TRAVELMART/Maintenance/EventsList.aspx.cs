using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.Maintenance
{
    public partial class EventsList : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Created: 20/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Page load
        /// --------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;

            uoHiddenFieldBranch.Value = Request.QueryString["bId"];
            uoHiddenFieldCity.Value = Request.QueryString["cityId"];
            uoHiddenFieldOnOffDate.Value = Request.QueryString["Date"];

            //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;
            HotelGetEvents();
        }

        protected void uoListViewEventsPager_PreRender(object sender, EventArgs e)
        {
            //HotelGetEvents();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 20/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get hotel events list
        /// </summary>       
        private void HotelGetEvents()
        {
            DataTable dt = null;

            try
            {
                dt = BLL.MaintenanceViewBLL.GetHotelEventsList(Convert.ToInt32(uoHiddenFieldBranch.Value), Convert.ToInt32(uoHiddenFieldCity.Value),
                    Convert.ToDateTime(uoHiddenFieldOnOffDate.Value));

                uoListViewEvents.DataSource = dt;
                uoListViewEvents.DataBind();                
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
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAVELMART
{
    public partial class HotelRequestView : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //Pending Request
            uoListViewHead.DataSource = null;
            uoListViewHead.DataBind();
            GetHotelRequestList();
            //Booked Request
            //uoListViewHeadBooked.DataSource = null;
            //uoListViewHeadBooked.DataBind();
            //GetBookedHotelRequestList();
        }
        protected void uoListViewHead_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName != "")
            {
                uoHiddenFieldSortBy.Value = e.CommandName;
                GetHotelRequestList();
            }
        }
        //protected void uoListViewHeadBooked_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "")
        //    {
        //        uoHiddenFieldSortBy.Value = e.CommandName;
        //        GetBookedHotelRequestList();
        //    }
        //}
        protected void uoObjectDataSourceHotelRequestView_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["SortBy"] = uoHiddenFieldSortBy.Value;
        }
        //protected void uoObjectDataSourceHotelBookedRequestView_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        //{
        //    e.InputParameters["SortBy"] = uoHiddenFieldSortBy.Value;
        //}
        #endregion
        #region Functions
        private void GetHotelRequestList()
        {
            try
            {
                uoListViewHotelRequest.DataSource = null;
                uoListViewHotelRequest.DataSourceID = "uoObjectDataSourceHotelRequestView";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void GetBookedHotelRequestList()
        //{
        //    try
        //    {
        //        uoListViewHeadBookedList.DataSource = null;
        //        uoListViewHeadBookedList.DataSourceID = "uoObjectDataSourceBookedHotelRequestView";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
    }
}

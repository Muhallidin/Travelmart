using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART
{
    public partial class DashboardHotel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                 Session["strPrevPage"] = Request.RawUrl;
         
                GetDashboardDetails();
                GetDashboardDetailsWeek();
            }
        }

        private void GetDashboardDetails()
        {
            DataView dv = null;
            DataTable dt = DashboardBLL.GetHotelDashboardDetails(Session["UserBranchID"].ToString(), GlobalCode.Field2DateTime((GlobalCode.Field2String(Session["DateFrom"]))).ToString(), GlobalCode.Field2String(Session["DateTo"]));
            dv = dt.DefaultView;
            //dv.Sort = uoDropDownListGroup.SelectedValue;
            uoListViewDashboard.DataSource = dv;
            uoListViewDashboard.DataBind();
        }

        private void GetDashboardDetailsWeek()
        {
            DataView dv = null;
            DataTable dt = DashboardBLL.GetHotelDashboardDetailsWeek(Session["UserBranchID"].ToString());
            dv = dt.DefaultView;
            //dv.Sort = uoDropDownListGroup.SelectedValue;
            uoListViewDashboard2.DataSource = dv;
            uoListViewDashboard2.DataBind();
        }

        protected void uoListViewDashboard_PreRender(object sender, EventArgs e)
        {
            //GetDashboardDetails();
        }

        protected void uoListViewDashboard2_PreRender(object sender, EventArgs e)
        {
 
        }

        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void uoListViewDashboard2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        /// <summary>
        /// Date Created:   26/10/2011
        /// Created By:     Ryan Bautista
        /// (description)   Set dashboard groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Room Type";
            string GroupValueString = "colRoomNameVarchar";

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
                return string.Format("<tr><td class=\"group\" colspan=\"2\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

    }
}

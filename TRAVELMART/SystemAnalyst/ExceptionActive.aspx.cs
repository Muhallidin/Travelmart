using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class ExceptionActive : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                //Delete unncessary links from master page
                LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");
                HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");
                Label uoLabelDate = (Label)Master.FindControl("uoLabelDate");
                ImageButton uoIBtnNext = (ImageButton)Master.FindControl("uoIBtnNext");
                ImageButton uoIBtnBack = (ImageButton)Master.FindControl("uoIBtnBack");
                LinkButton uoLabelDateToday = (LinkButton)Master.FindControl("uoLabelDateToday");
                HtmlControl ucSpanToday = (HtmlControl)Master.FindControl("ucSpanToday");

                uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                uoLinkExport.Visible = false;
                ucSpanViewWeek.Visible = false;
                ucSpanExportALL.Visible = false;
                uoLabelDate.Visible = false;
                uoIBtnNext.Visible = false;
                uoIBtnBack.Visible = false;
                uoLabelDateToday.Visible = false;
                ucSpanToday.Visible = false;
                //Delete unncessary links
            }
            ListView1.DataSource = null;
            ListView1.DataBind();
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            BindExceptionList();
        }
        protected void uoObjectDataSourceTRException_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["iFilterBy"] = GlobalCode.Field2TinyInt(uoDropDownListFilterBy.SelectedValue);
            e.InputParameters["sFilter"] = uoTextBoxFilter.Text.Trim();

            e.InputParameters["sRecordLocator"] = uoTextBoxRecordLocator.Text;
            e.InputParameters["iSequenceNo"] = GlobalCode.Field2Int(uoTextBoxSequenceNo.Text);
        }
        protected void uoListViewExceptionPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region Events
        /// <summary>
        /// Date Created:   07/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   Refresh listview
        /// -------------------------------------------
        /// </summary>
        protected void BindExceptionList()
        {
            uoListViewException.DataSource = null;
            uoListViewException.DataSourceID = "uoObjectDataSourceTRException";
        }
        #endregion

    }
}

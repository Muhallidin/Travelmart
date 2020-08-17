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
    public partial class ExceptionPNR : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Created:   5/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   View of Exception from XML file in TM
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            SetDefaultValues();

            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);

                //Delete unncessary links from master page
                LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");
                HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");
                LinkButton uoLabelDateToday = (LinkButton)Master.FindControl("uoLabelDateToday");
                HtmlControl ucSpanToday = (HtmlControl)Master.FindControl("ucSpanToday");

                uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                uoLinkExport.Visible = false;
                ucSpanViewWeek.Visible = false;
                ucSpanExportALL.Visible = false;
                uoLabelDateToday.Visible = false;
                ucSpanToday.Visible = false;
                //Delete unncessary links
            }

             HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
             if (uoHiddenFieldPopupCalendar.Value == "1")
             {
                 uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                 uoTextBoxTo.Text = "";                 
                 BindExceptionList();
             }

            ListView1.DataSource = null;
            ListView1.DataBind();

        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            if (uoTextBoxFrom.Text.Trim() != "")
            {
                uoHiddenFieldDate.Value = uoTextBoxFrom.Text;
            }
            else
            {
                uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            }
            BindExceptionList();
        }
        
        /// <summary>
        /// Date Created:   5/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   Refresh parameters of datasource
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoObjectDataSourceTRException_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sUserName"] =  uoHiddenFieldUser.Value;
            e.InputParameters["sRole"] = uoHiddenFieldUserRole.Value;

            e.InputParameters["DateFrom"] = uoHiddenFieldDate.Value;
            e.InputParameters["DateTo"] = uoTextBoxTo.Text.Trim();
            e.InputParameters["sRecordLocator"] = uoTextBoxRecordLocator.Text;
            e.InputParameters["iSequenceNo"] = GlobalCode.Field2Int(uoTextBoxSequenceNo.Text);
        }
        protected void uoListViewExceptionPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created:   5/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   Default values upon loading
        protected void SetDefaultValues()
        {
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldUserRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            Session["strPrevPage"] = Request.RawUrl;
        }
        /// <summary>
        /// Date Created:   5/Dec/2012
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

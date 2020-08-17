using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Globalization;

namespace TRAVELMART.Administration
{
    public partial class AuditTrailLogs_old : System.Web.UI.Page
    {
        #region "Events"
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{                
            //    Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //    SFStatus.Visible = false;                
            //}
            //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;

            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                SFStatus.Visible = false;
                Session["strPrevPage"] = Request.RawUrl;
            }            
        }

        protected void uoListViewAuditTrail_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoObjectDataSourceAuditTrail_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            //e.InputParameters["DateFrom"] = DateTime.ParseExact(GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), TravelMartVariable.DateFormat, enCulture);
            e.InputParameters["DateFrom"] = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
            //e.InputParameters["DateTo"] = DateTime.ParseExact(GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), TravelMartVariable.DateFormat, enCulture);
            e.InputParameters["DateTo"] = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
        }
        #endregion

        #region "Functions"
        #endregion
    }
}

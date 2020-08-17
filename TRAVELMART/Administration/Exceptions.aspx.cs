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
using System.Web.Security;
namespace TRAVELMART
{
    public partial class Exceptions : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// ------------------------------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 03/04/2012
        /// Description: Transfer to new master page; Initialize Session Values
        /// ------------------------------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 27/04/2012
        /// Description: Bind data when date in calendar is changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (uoHiddenFieldPopupCalendar.Value == "1")
            {
                uoListViewException.DataSourceID = uoObjectDataSourceException.UniqueID;
            }
        }
        protected void uoListViewException_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoObjectDataSourceException_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFrom"] = uoHiddenFieldDate.Value;
            e.InputParameters["DateTo"] = uoHiddenFieldDateTo.Value;
        }
        #endregion


        #region "Functions"
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/04/2012
        /// Description: initialize session values
        /// </summary>
        protected void InitializeValues()
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
                
            }
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (mUser == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!mUser.IsOnline)
                { 
                    Response.Redirect("~/Login.aspx");
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToString("MM/dd/yyyy");
            }
            Session["strPrevPage"] = Request.RawUrl;

            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
            uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
            uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");

        }

        #endregion
    }
}

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
    public partial class Errors : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// -----------------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 04/05/2012
        /// Description: Move to new master page
        ///              move initalization of values to different class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (uoHiddenFieldPopupCalendar.Value == "1")
            {
                uoListViewError.DataSourceID = uoObjectDataSourceError.UniqueID;
            }
        }
        protected void uoListViewError_PreRender(object sender, EventArgs e)
        {
         
        }
        protected void uoObjectDataSourceError_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            //e.InputParameters["DateFrom"] = DateTime.ParseExact(GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), TravelMartVariable.DateFormat, enCulture);
            e.InputParameters["DateFrom"] = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
            //e.InputParameters["DateTo"] = DateTime.ParseExact(GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), TravelMartVariable.DateFormat, enCulture);
            e.InputParameters["DateTo"] = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
        }
        #endregion
       

        #region "Functions"
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/05/2012
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


            uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
            uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

        }

        #endregion
    }
}

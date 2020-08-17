using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;


namespace TRAVELMART.Hotel
{
    public partial class HotelEmailView : System.Web.UI.Page
    {
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    Session["UserRole"] = UserRolePrimary;                   
                }
                GetEmailSent();
            }
        }

        #region uolistviewHotelInfoPager_PreRender
        protected void uolistviewHotelInfoPager_PreRender(object sender, EventArgs e)
        {
            GetEmailSent();
        }
        #endregion

        private void GetEmailSent()
        {
            //DataTable dt = new DataTable();
            //dt = CommonFunctions.GetEmailSent();
            //uoListViewEmail.DataSource = dt;
            //uoListViewEmail.DataBind();
        }
    }
}
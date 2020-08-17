using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using System.Web.Security;

namespace TRAVELMART
{
    public partial class Popup : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Date Modified:  04/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Reset membership time
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinueWorking_Click(object sender, EventArgs e)
        {
            //Do nothing. But the Session will be refreshed as a result of 
            //this method being called, which is its entire purpose.
            string sUser = GlobalCode.Field2String(MUser.GetUserName());
            if (sUser != "")
            {
                MembershipUser mUser = Membership.GetUser(sUser);
                if (mUser != null)
                {
                    mUser.LastActivityDate = DateTime.Now;
                    Membership.UpdateUser(mUser);
                }
            }           
        }
        /// <summary>
        /// Date Modified:  04/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Set  mUser.LastActivityDate to -Membership.UserIsOnlineTimeWindow          
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExitWorking_Click(object sender, EventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (mUser != null)
            {
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);
            }
            FormsAuthentication.SignOut();
            try
            {
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                try
                {
                    Response.Redirect("../Login.aspx", false);

                }
                catch
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }
    }
}

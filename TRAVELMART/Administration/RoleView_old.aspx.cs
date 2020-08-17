using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.UI.HtmlControls;
using System.IO;

namespace TRAVELMART.Administration
{
    public partial class RoleView_old : System.Web.UI.Page
    {
        #region  Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRoles();

                if (User.IsInRole("administrator"))
                {
                    uoHyperLinkRoleAdd.Visible = true;
                }
                else
                {
                    uoHyperLinkRoleAdd.Visible = false;
                }
                uoHyperLinkRoleAdd.HRef = "~/Administration/AddRole.aspx";
            }
            if (uoHiddenFieldPopupRole.Value == "1")
            {
                GetRoles();
            }
            uoHiddenFieldPopupRole.Value = "0";
        }
        protected void uoRoleList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                UserAccountBLL.DeleteRole(e.CommandArgument.ToString());

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Role deleted. (flagged as inactive)";
                strFunction = "uoRoleList_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetRoles();
            }
        }
        protected void uoRoleList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {
        }
        protected void uoRoleListPager_PreRender(object sender, EventArgs e)
        {
        }

        #endregion

        #region Functions
        /// <summary>
        /// Date Created:    19/08/2011
        /// Created By:      Marco Abejar
        /// (description)    Get role list            
        /// -----------------------------------
        /// Date Modified:   14/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Hide edit and delete header if not administrator      
        /// </summary>
        private void GetRoles()
        {           
            DataTable dtRole = UserAccountBLL.GetUserRoles();
            try
            {
                uoRoleList.DataSource = dtRole;
                uoRoleList.DataBind();

                HtmlControl EditTH = (HtmlControl)uoRoleList.FindControl("EditTH");
                HtmlControl DeleteTH = (HtmlControl)uoRoleList.FindControl("DeleteTH");

                if (EditTH != null)
                {
                    if (!User.IsInRole("administrator"))
                    {
                        EditTH.Style.Add("display", "none");
                        DeleteTH.Style.Add("display", "none");
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                        DeleteTH.Style.Add("display", "display");
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (dtRole != null)
                {
                    dtRole.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   14/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Hide Element if not administrator
        /// </summary>
        protected string HideElemet()
        {
            if (!User.IsInRole("administrator"))
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}

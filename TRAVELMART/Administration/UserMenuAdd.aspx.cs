using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.IO;

namespace TRAVELMART
{
    public partial class UserMenuAdd : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                OpenParentPage();
            }
            if (!IsPostBack)
            {
                UserMenuLogAuditTrail();
                ucLabelTitle.Text = GlobalCode.Field2String(Session["SelectedRoleName"]) + " Menu";
                GetMenu(GlobalCode.Field2String(Session["SelectedRoleKey"]));
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            Int32 MenuID = UserRightsBLL.InsertMenu(GlobalCode.Field2String(Session["SelectedRoleKey"]), uoDropDownListMenu.SelectedValue, GlobalCode.Field2String(Session["UserName"]));

            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
            strLogDescription = "User menu added.";
            strFunction = "uoButtonSave_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

            OpenParentPage();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   19/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get role list            
        /// </summary>
        /// 
        private void GetMenu(string KeyRoleString)
        {
            DataTable MenuDataTable = null;
            try
            { 
                MenuDataTable = UserRightsBLL.GetMenuNotAdded(KeyRoleString);
                ListItem item = new ListItem("--Select Menu--", "0");
                uoDropDownListMenu.Items.Add(item);
                uoDropDownListMenu.DataSource = MenuDataTable;
                uoDropDownListMenu.DataBind();
                //CommonFunctions.ChangeToUpperCase(uoDropDownListMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MenuDataTable != null)
                {
                    MenuDataTable.Dispose();
                }
            }
        }
        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created:   22/08/2011
            /// Created By:     Josephine Gad
            /// (description)   Close this page and update parent page            
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldAdd\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void UserMenuLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";
            
            strLogDescription = "Add button for user menu editor clicked.";
            strFunction = "UserMenuLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

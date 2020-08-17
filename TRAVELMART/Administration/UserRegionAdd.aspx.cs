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
    public partial class UserRegionAdd : System.Web.UI.Page
    {
        #region "Events"
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["uID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            UserRegionLogAuditTrail();
            if (!IsPostBack)
            {
                GetRegionToBeAdded();
            }
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            Int32 UserRegionID = UserRightsBLL.AddUserMapRef(Request.QueryString["uID"].ToString(), uoDropDownListRegion.SelectedValue, GlobalCode.Field2String(Session["UserName"]));

            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
            strLogDescription = "User region added.";
            strFunction = "uoButtonSave_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            BLL.AuditTrailBLL.InsertLogAuditTrail(UserRegionID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

            OpenParentPage();
        }
        #endregion

        #region "Functions"
        private void GetRegionToBeAdded()
        {
            //DataTable UserDataTable = null;
            //try
            //{

            //    UserDataTable = UserRightsBLL.GetUserRegionNotAdded(Request.QueryString["uID"].ToString(), GlobalCode.Field2String(Session["UserName"]));
            //    uoDropDownListRegion.Items.Clear();
            //    uoDropDownListRegion.Items.Add(new ListItem("--Select Region--", "0"));
            //    uoDropDownListRegion.DataSource = UserDataTable;
            //    uoDropDownListRegion.DataBind();
            //    //CommonFunctions.ChangeToUpperCase(uoDropDownListRegion);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (UserDataTable != null)
            //    {
            //        UserDataTable.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Close this page and update parent page     
        /// ------------------------------------------------------
        /// Date Created:   19/04/2012
        /// Created By:     Josephine Gad
        /// (description)   Change ctl00_ContentPlaceHolder1_uoHiddenFieldAdd to ctl00_NaviPlaceHolder_uoHiddenFieldAdd        
        /// </summary>
        private void OpenParentPage()
        {            
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldAdd\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void UserRegionLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";
            
            strLogDescription = "Add button for user region editor clicked.";
            strFunction = "UserRegionLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

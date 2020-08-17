using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Data.SqlClient;

namespace TRAVELMART
{
    public partial class AddRole : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RoleLogAuditTrail();
                if (Request.QueryString["rId"] != null)
                {
                    uohiddenRoleId.Value = Request.QueryString["rId"];
                    GetRole(uohiddenRoleId.Value);
                }
            }
        }
        protected void uobuttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                string strMessage = UserAccountBLL.AddUpdateRole(uotextboxRoleName.Text, uohiddenRoleId.Value, uotextboxRoleDesc.Text);
                AlertMessage(strMessage);

                if (Request.QueryString["rId"] == "0" || Request.QueryString["rId"] == null)
                {
                    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                    strLogDescription = "User role added.";
                    strFunction = "uobuttonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
                else
                {
                    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                    strLogDescription = "User role updated.";
                    strFunction = "uobuttonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }

                OpenParentPage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get user role          
        /// ---------------------------------------------------
        /// Date Modified:  27/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to SqlDataReader
        /// </summary>
        /// 
        private void GetRole(string rid)
        {
            SqlDataReader dr = null;
            try
            {
                dr = UserAccountBLL.GetRole(rid);
                if (dr.Read())
                {
                    uotextboxRoleName.Text = dr["role"].ToString();
                    uotextboxRoleDesc.Text = dr["roledesc"].ToString();
                }

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Close this page and update parent page            
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupRole\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uobuttonSave, this.GetType(), "scr", sScript, false);
        }
        private void AlertMessage(string s)
        {
            /// <summary>
            /// Date Created: 08/07/2011
            /// Created By: Marco Abejar
            /// (description) Show pop up message            
            /// </summary>

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void RoleLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["rId"] != null)
            {
                strLogDescription = "Edit linkbutton for role editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for role editor clicked.";
            }

            strFunction = "RoleLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}


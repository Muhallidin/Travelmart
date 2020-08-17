using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class UserRegions_old : System.Web.UI.Page
    {
        #region "Events"
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                SFStatus.Visible = false;

                string userName = GlobalCode.Field2String(Session["UserName"]);
                if (Session["UserRole"] == null)
                {                    
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                    Session["UserRole"] = UserRolePrimary;                    
                }
                uoHiddenFieldUser.Value = userName;
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
                GetRoles();
                GetUsers("0");
                GetRegion();                
            }
            if (uoHiddenFieldAdd.Value == "1")
            {
                GetRegion();
            }
            uoHiddenFieldAdd.Value = "0";
        }
        protected void uoDropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUsers(uoDropDownListRoles.SelectedItem.Text);
            GetRegion();
            if (uoHiddenFieldRole.Value != TravelMartVariable.RoleAdministrator)
            {
                if (uoDropDownListRoles.SelectedItem.Text == TravelMartVariable.RoleAdministrator)
                {
                    uoButtonAdd.Visible = false;
                }
                else
                {
                    uoButtonAdd.Visible = true;
                }
            }
        }
        protected void uoDropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHyperLinkAdd.HRef = "UserRegionAdd.aspx?uID=" + uoDropDownListUsers.SelectedValue;
            GetRegion();            
        }
        protected void uoGridViewRegions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                string MapIdString = e.CommandArgument.ToString();
                UserRightsBLL.DeleteUserMapRef(MapIdString, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "User region deleted. (flagged as inactive)";
                strFunction = "uoGridViewRegions_RowCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(MapIdString), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                
                GetRegion();
            }
        }
        protected void uoGridViewRegions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   19/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get role list            
        /// -----------------------------
        /// Date Modified:  22/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change GetUserRoles to GetUserRolesAccess
        /// </summary>
        private void GetRoles()
        {
            
            //DataTable dtRole = UserAccountBLL.GetUserRoles();
            DataTable dtRole = UserAccountBLL.GetUserRolesAccess(uoHiddenFieldRole.Value);
            try
            {
                uoDropDownListRoles.Items.Clear();
                uoDropDownListRoles.DataSource = dtRole;
                uoDropDownListRoles.DataBind();
                uoDropDownListRoles.Items.Insert(0, new ListItem("--Select Role--", ""));
                //CommonFunctions.ChangeToUpperCase(uoDropDownListRoles);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtRole != null)
                {
                    dtRole.Dispose();
                }
            }
        }
        private void GetUsers(string role)
        {

            /// <summary>
            /// Date Created: 19/08/2011
            /// Created By: Marco Abejar
            /// (description) Get user list            
            /// </summary>

            DataTable dtUsers = UserAccountBLL.GetUsers(role, "", uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, "");
            try
            {
                uoDropDownListUsers.Items.Clear();
                uoDropDownListUsers.DataSource = dtUsers;
                uoDropDownListUsers.DataBind();
                uoDropDownListUsers.Items.Insert(0, new ListItem("--Select User--", "0"));
                //CommonFunctions.ChangeToUpperCase(uoDropDownListUsers);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtUsers != null)
                {
                    dtUsers.Dispose();
                }
            }
        }
        private void GetRegion()
        {
            //DataTable UserDataTable = null;
            //try
            //{
            //    UserDataTable = UserRightsBLL.GetUserRegion(uoDropDownListUsers.SelectedValue, GlobalCode.Field2String(Session["UserName"]));
            //    if (UserDataTable.Rows.Count == 0)
            //    {
            //        DataRow r = UserDataTable.NewRow();                    
            //        r["IsExist"] = false;
            //        UserDataTable.Rows.Add(r);                    
            //        uoGridViewRegions.DataSource = UserDataTable;
            //        uoGridViewRegions.DataBind();

            //        int colInt = uoGridViewRegions.Rows[0].Cells.Count;
            //        uoGridViewRegions.HeaderRow.Cells[1].Visible = false;

            //        uoGridViewRegions.Rows[0].Cells.Clear();
            //        uoGridViewRegions.Rows[0].Cells.Add(new TableCell());
            //        uoGridViewRegions.Rows[0].Cells[0].ColumnSpan = colInt;
            //        uoGridViewRegions.Rows[0].Cells[0].Text = "No Record Found.";
            //        uoGridViewRegions.Rows[0].Cells[0].CssClass = "leftAligned";                    
            //    }
            //    else
            //    {                    
            //        uoGridViewRegions.DataSource = UserDataTable;
            //        uoGridViewRegions.DataBind();

            //        if (uoHiddenFieldRole.Value != TravelMartVariable.RoleAdministrator)
            //        {
            //            if (uoDropDownListRoles.SelectedItem.Text == TravelMartVariable.RoleAdministrator)
            //            {
            //                uoGridViewRegions.Columns[1].Visible = false;
            //            }
            //            else
            //            {
            //                uoGridViewRegions.Columns[1].Visible = true;
            //            }
            //        }                    
            //    }              
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
        #endregion
    }
}

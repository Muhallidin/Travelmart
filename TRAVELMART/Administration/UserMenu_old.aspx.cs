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
    public partial class UserMenu_old : System.Web.UI.Page
    {
        #region "Events"
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                SFStatus.Visible = false;
                GetRoles();
                GetMenu();
                if (User.IsInRole("administrator"))
                {
                    uoHyperLinkAdd.Visible = true;
                }
                else
                {
                    uoHyperLinkAdd.Visible = false;
                }
            }
            if (uoHiddenFieldEdit.Value == "1" || uoHiddenFieldAdd.Value == "1")
            {
                GetMenu();
            }
            
            uoHiddenFieldEdit.Value = "0";
            uoHiddenFieldAdd.Value = "0";
        }
        protected void uoDropDownListRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedRoleKey"] = uoDropDownListRole.SelectedValue;
            Session["SelectedRoleName"] = uoDropDownListRole.SelectedItem.Text;
            GetMenu();
        }
        protected void uoGridViewMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                string MenuIdString = e.CommandArgument.ToString();
                UserRightsBLL.DeleteMenu(MenuIdString, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "User menu deleted. (flagged as inactive)";
                strFunction = "uoGridViewMenu_RowCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(MenuIdString), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetMenu();
            }
            else if (e.CommandName == "Activate")
            {
                string MenuIdString = e.CommandArgument.ToString();
                UserRightsBLL.ActivateMenu(MenuIdString, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "User menu activated. (flagged as active)";
                strFunction = "uoGridViewMenu_RowCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(MenuIdString), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetMenu();
            }
        }
        protected void uoGridViewMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        //protected void uoCheckBoxShowInactive_CheckedChanged(object sender, EventArgs e)
        //{
        //    GetMenu();
        //}
        #endregion

        #region "Procedures"
        /// <summary>
        /// Date Created:   19/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get role list            
        /// </summary>
        private void GetRoles()
        {            
            DataTable dtRole = UserAccountBLL.GetUserRoles();
            try
            {
                uoDropDownListRole.Items.Clear();
                uoDropDownListRole.DataSource = dtRole;
                uoDropDownListRole.DataBind();
                uoDropDownListRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
                //CommonFunctions.ChangeToUpperCase(uoDropDownListRole);
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
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get menu list by Role
        /// </summary>
        /// 
        private void GetMenu()
        {           
            DataTable MenuDataTable = null;
            try
            {                
                MenuDataTable = UserRightsBLL.GetMenu(uoDropDownListRole.SelectedValue, true);
                if (MenuDataTable.Rows.Count == 0)
                {
                    MenuDataTable.Rows.Add(MenuDataTable.NewRow());
                    uoGridViewMenu.DataSource = MenuDataTable;
                    uoGridViewMenu.DataBind();
                    int colInt = uoGridViewMenu.Rows[0].Cells.Count;
                    
                    uoGridViewMenu.HeaderRow.Cells[0].Visible = false;
                    uoGridViewMenu.HeaderRow.Cells[2].Visible = false;
                    uoGridViewMenu.HeaderRow.Cells[3].Visible = false;

                    uoGridViewMenu.Rows[0].Cells.Clear();
                    uoGridViewMenu.Rows[0].Cells.Add(new TableCell());
                    uoGridViewMenu.Rows[0].Cells[0].ColumnSpan = colInt;
                    uoGridViewMenu.Rows[0].Cells[0].Text = "No Record Found.";
                    uoGridViewMenu.Rows[0].Cells[0].CssClass = "leftAligned";
                }
                else
                {                   
                    uoGridViewMenu.DataSource = MenuDataTable;
                    uoGridViewMenu.Columns[0].Visible = true;
                    uoGridViewMenu.DataBind();
                    uoGridViewMenu.Columns[0].Visible = false;                  

                    if (User.IsInRole("administrator"))
                    { 
                        //uoGridViewMenu.Columns[2].Visible = true;
                        uoGridViewMenu.Columns[3].Visible = true;
                    }
                    else
                    {
                        //uoGridViewMenu.Columns[2].Visible = false;
                        uoGridViewMenu.Columns[3].Visible = false;
                    }
                }
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
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Show/Hide view link if there is existing submenu
        /// </summary>
        ///
        protected bool ViewIsVisible(object colPageIDInt)
        {
            bool IsVisibleBool = true;
            string PageString = colPageIDInt.ToString();
            PageString = (PageString == "" ? "0" : PageString);
            
            DataTable SubMenuDataTable = null;
            try
            {
                SubMenuDataTable = UserRightsBLL.GetSubMenuAll(PageString);
                if (SubMenuDataTable.Rows.Count > 0)
                {
                    IsVisibleBool = true;
                }
                else
                {
                    IsVisibleBool = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SubMenuDataTable != null)
                {
                    SubMenuDataTable.Dispose();
                }
            }
            return IsVisibleBool;
        }
        #endregion      
    }
}

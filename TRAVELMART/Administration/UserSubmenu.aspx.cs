using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Collections;

using System.IO;

namespace TRAVELMART
{
    public partial class UserSubmenu : System.Web.UI.Page
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
                UserSubmenuLogAuditTrail();
                ucLabelTitle.Text = Request.QueryString["mName"] + " Submenu";
                GetSubmenu();

                if (User.IsInRole("administrator"))
                {
                    uoButtonSave.Visible = true;
                    uoTreeViewSubmenu.Enabled = true;
                }
                else
                {
                    uoButtonSave.Visible = false;
                    uoTreeViewSubmenu.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Save submenus
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string UserString = GlobalCode.Field2String(Session["UserName"]);
            bool WithChildNodeBool;

            string strLogDescription;
            string strFunction;

            foreach (TreeNode Node in uoTreeViewSubmenu.Nodes)
            {
                if (Node.ChildNodes.Count > 0)
                {
                    WithChildNodeBool = false;
                    foreach (TreeNode ChildNode in Node.ChildNodes)
                    {
                        if (ChildNode.Checked)
                        {
                            Int32 MenuID = UserRightsBLL.InsertMenu(GlobalCode.Field2String(Session["SelectedRoleKey"]), ChildNode.Value, UserString);
                            WithChildNodeBool = true;

                            if (MenuID > 0)
                            {
                                //Insert log audit trail (Gabriel Oquialda - 19/03/2012)
                                strLogDescription = "User menu added.";
                                strFunction = "uoButtonSave_Click";

                                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                                BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, UserString);
                            }
                        }
                        else
                        {
                            Int32 MenuID = UserRightsBLL.DeleteMenuByRoleId(GlobalCode.Field2String(Session["SelectedRoleKey"]), ChildNode.Value, UserString);

                            if (MenuID > 0)
                            {
                                //Insert log audit trail (Gabriel Oquialda - 19/03/2012)
                                strLogDescription = "User menu deleted. (flagged as inactive)";
                                strFunction = "uoButtonSave_Click";

                                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                                BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, UserString);
                            }
                        }
                    }
                    if (WithChildNodeBool)
                    {
                        Int32 MenuID = UserRightsBLL.InsertMenu(GlobalCode.Field2String(Session["SelectedRoleKey"]), Node.Value, UserString);
                        if (MenuID > 0)
                        {
                            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                            strLogDescription = "User menu added.";
                            strFunction = "uoButtonSave_Click";

                            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, UserString);
                        }
                    }
                    else
                    {
                        Int32 MenuID = UserRightsBLL.DeleteMenuByRoleId(GlobalCode.Field2String(Session["SelectedRoleKey"]), Node.Value, UserString);

                        if (MenuID > 0)
                        {
                            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                            strLogDescription = "User menu deleted. (flagged as inactive)";
                            strFunction = "uoButtonSave_Click";

                            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, UserString);
                        }
                    }
                }
                else
                {
                    if (Node.Checked)
                    {
                        Int32 MenuID = UserRightsBLL.InsertMenu(GlobalCode.Field2String(Session["SelectedRoleKey"]), Node.Value, UserString);

                        if (MenuID > 0)
                        {
                            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                            strLogDescription = "User menu added.";
                            strFunction = "uoButtonSave_Click";

                            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, UserString);
                        }
                    }
                    else
                    {
                        Int32 MenuID = UserRightsBLL.DeleteMenuByRoleId(GlobalCode.Field2String(Session["SelectedRoleKey"]), Node.Value, UserString);
                        if (MenuID > 0)
                        {
                            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                            strLogDescription = "User menu deleted. (flagged as inactive)";
                            strFunction = "uoButtonSave_Click";

                            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(MenuID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                        }
                    }
                }
            }

            //if ()
            //{
            //    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
            //    strLogDescription = "User menu added.";
            //    strFunction = "uoButtonSave_Click";

            //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //    BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            //}
            //else
            //{
            //    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
            //    strLogDescription = "User menu deleted. (flagged as inactive)";
            //    strFunction = "uoButtonSave_Click";

            //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //    BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            //}

            OpenParentPage();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Submenu       
        /// ===================================
        /// Date Modified:   21/May/2014
        /// Modified By:     Josephine Gad
        /// (description)    Add   sMenuDisplayName to show menu name and description     
        /// </summary>
        private void GetSubmenu()
        {
            uoTreeViewSubmenu.Nodes.Clear();
            DataTable SubMenuDataTable = null;
            DataTable ChildDataTable = null;
            string sMenuDisplayName;
            try
            {                
                SubMenuDataTable = UserRightsBLL.GetSubMenuAll(Request.QueryString["mId"]);
                if (SubMenuDataTable.Rows.Count > 0)
                {
                    uoButtonSave.Visible = true;
                    uoDivMsg.Visible = false;
                    //uoTreeViewSubmenu.DataSource = SubMenuDataTable;
                    //                   TreeNode NewNode = new TreeNode(

                    bool IsExistsBool = false;
                    foreach (DataRow SubMenuRow in SubMenuDataTable.Rows)
                    {
                        sMenuDisplayName = SubMenuRow["colDisplayNameVarchar"].ToString() + " - " + SubMenuRow["colDescriptionVarchar"].ToString();
                        TreeNode NewNode = new TreeNode(sMenuDisplayName, SubMenuRow["colPageIDInt"].ToString());
                        NewNode.ShowCheckBox = true;
                        IsExistsBool = UserRightsBLL.IsMenuExists(GlobalCode.Field2String(Session["SelectedRoleKey"]), SubMenuRow["colPageIDInt"].ToString());
                        NewNode.Checked = IsExistsBool;
                        
                        ChildDataTable = UserRightsBLL.GetSubMenuAll(SubMenuRow["colPageIDInt"].ToString());
                        foreach (DataRow ChildRow in ChildDataTable.Rows)
                        {
                            sMenuDisplayName = ChildRow["colDisplayNameVarchar"].ToString() + " - " + ChildRow["colDescriptionVarchar"].ToString();
                            TreeNode ChileNode = new TreeNode(sMenuDisplayName, ChildRow["colPageIDInt"].ToString());
                            ChileNode.ShowCheckBox = true;
                            IsExistsBool = UserRightsBLL.IsMenuExists(GlobalCode.Field2String(Session["SelectedRoleKey"]), ChildRow["colPageIDInt"].ToString());
                            ChileNode.Checked = IsExistsBool;
                        
                            NewNode.ChildNodes.Add(ChileNode);                            
                        }
                        uoTreeViewSubmenu.Nodes.Add(NewNode);
                        uoTreeViewSubmenu.NodeStyle.CssClass = "BlackText";
                    }
                }
                else
                {
                    uoButtonSave.Visible = false;
                    uoDivMsg.Visible = true;
                }
                //uoGridViewSubMenu.DataSource = SubMenuDataTable;
                //uoGridViewSubMenu.Columns[0].Visible = true;
                //uoGridViewSubMenu.DataBind();
                //uoGridViewSubMenu.Columns[0].Visible = false;
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
                if (ChildDataTable != null)
                {
                    ChildDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Close this page and update parent page            
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
        protected void UserSubmenuLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";
                        
            strLogDescription = "View linkbutton for user submenu clicked.";
            strFunction = "UserSubmenuLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion       
    }
}

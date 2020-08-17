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
using System.Web.Security;
using System.Reflection;

namespace TRAVELMART
{
    public partial class UserRegions : System.Web.UI.Page
    {
      
        #region "Events"
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  16/04/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Move to new master page
        ///                 Initialize session values
        ///                 MOve initialization to initializevalues function
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "User Region Maintenance Viewed";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetRoles();
                GetUsers("0");
                //GetRegion(); 
                BindRegionList(true);
            }
            if (uoHiddenFieldAdd.Value == "1")
            {
                //GetRegion();
            }
            uoHiddenFieldAdd.Value = "0";
        }

        protected void uoDropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUsers(uoDropDownListRoles.SelectedItem.Text);
            BindRegionList(true);
            //GetRegion();
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
            if (uoDropDownListUsers.SelectedValue == "0")
            {
                BindRegionList(true);
            }
            else
            {
                BindRegionList(false);
            }
            //uoHyperLinkAdd.HRef = "../Administration/UserRegionAdd.aspx?uID=" + uoDropDownListUsers.SelectedValue;
            //GetRegion();            
        }
        //protected void uoGridViewRegions_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;

        //    if (e.CommandName == "Delete")
        //    {
        //        string MapIdString = e.CommandArgument.ToString();
        //        UserRightsBLL.DeleteUserMapRef(MapIdString, GlobalCode.Field2String(Session["UserName"]));

        //        //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
        //        strLogDescription = "User region deleted. (flagged as inactive)";
        //        strFunction = "uoGridViewRegions_RowCommand";

        //        DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(MapIdString), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                
        //        //GetRegion();
        //    }
        //}
        //protected void uoGridViewRegions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //}
        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            AddRegion();
        }
        protected void uoButtonDelete_Click(object sender, EventArgs e)
        {
            RemoveRegion();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveUserRegion();
        }
        #endregion

        #region "Functions"  
        
        protected void InitializeValues()
        {
            string userName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
            }
            MembershipUser UserName = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (UserName == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!UserName.IsOnline)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            userName = GlobalCode.Field2String(Session["UserName"]);

            if (Session["UserRole"] == null)
            {
                string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                Session["UserRole"] = UserRolePrimary;
            }
            uoHiddenFieldUser.Value = userName;
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]); ;

        }
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

            DataTable dtUsers = UserAccountBLL.GetUsers(role, "", uoHiddenFieldUser.Value, uoHiddenFieldRole.Value,"");
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
        //private void GetRegion()
        //{
        //    DataTable UserDataTable = null;
        //    try
        //    {
        //        UserDataTable = UserRightsBLL.GetUserRegion(uoDropDownListUsers.SelectedValue, GlobalCode.Field2String(Session["UserName"]));
        //        if (UserDataTable.Rows.Count == 0)
        //        {
        //            DataRow r = UserDataTable.NewRow();                    
        //            r["IsExist"] = false;
        //            UserDataTable.Rows.Add(r);                    
        //            uoGridViewRegions.DataSource = UserDataTable;
        //            uoGridViewRegions.DataBind();

        //            int colInt = uoGridViewRegions.Rows[0].Cells.Count;
        //            uoGridViewRegions.HeaderRow.Cells[1].Visible = false;

        //            uoGridViewRegions.Rows[0].Cells.Clear();
        //            uoGridViewRegions.Rows[0].Cells.Add(new TableCell());
        //            uoGridViewRegions.Rows[0].Cells[0].ColumnSpan = colInt;
        //            uoGridViewRegions.Rows[0].Cells[0].Text = "No Record Found.";
        //            uoGridViewRegions.Rows[0].Cells[0].CssClass = "leftAligned";                    
        //        }
        //        else
        //        {                    
        //            uoGridViewRegions.DataSource = UserDataTable;
        //            uoGridViewRegions.DataBind();

        //            if (uoHiddenFieldRole.Value != TravelMartVariable.RoleAdministrator)
        //            {
        //                if (uoDropDownListRoles.SelectedItem.Text == TravelMartVariable.RoleAdministrator)
        //                {
        //                    uoGridViewRegions.Columns[1].Visible = false;
        //                }
        //                else
        //                {
        //                    uoGridViewRegions.Columns[1].Visible = true;
        //                }
        //            }                    
        //        }              
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (UserDataTable != null)
        //        {
        //            UserDataTable.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Region of Users  
        /// </summary>
        private void BindRegionList(bool IsEmpty)
        {
            if (IsEmpty)
            {
                uoListViewRegion.DataSource = null;
                uoListViewRegionToAdd.DataSource = null;

                uoListViewRegion.DataBind();
                uoListViewRegionToAdd.DataBind();

                //uoTableRegion.Visible = false;
                uoTableRegion.Style.Add("display", "none");
            }
            else
            {
                //uoTableRegion.Visible = true;
                uoTableRegion.Style.Add("display", "inline");


                List<UserRegionList> RegionList = new List<UserRegionList>();
                List<UserRegionToAdd> RegionToAdd = new List<UserRegionToAdd>();

                uoListViewRegion.DataSource = null;
                uoListViewRegionToAdd.DataSource = null;
                UserRightsBLL.GetUserRegion(uoDropDownListUsers.SelectedValue, uoHiddenFieldUser.Value);

                if (Session["UserRegionList"] != null)
                {
                    RegionList = (List<UserRegionList>)Session["UserRegionList"];
                    uoListViewRegion.DataSource = RegionList;
                }
                if (Session["UserRegionToAdd"] != null)
                {
                    RegionToAdd = (List<UserRegionToAdd>)Session["UserRegionToAdd"];
                    uoListViewRegionToAdd.DataSource = RegionToAdd;
                }

                uoListViewRegion.DataBind();
                uoListViewRegionToAdd.DataBind();

                if (uoDropDownListRoles.SelectedItem.Text == TravelMartVariable.RoleHotelSpecialist)
                {
                    uoButtonDelete.Visible = false;
                    foreach (ListViewItem item in uoListViewRegion.Items)
                    {
                        CheckBox uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                        uoCheckBoxSelect.Visible = false;
                    }
                }
                else
                {
                    uoButtonDelete.Visible = true;
                }
            }
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Move Region from left to right list
        /// </summary>
        private void AddRegion()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldRegion;
            Label uoLabelRegion;

            List<UserRegionList> listAdded = new List<UserRegionList>();
            List<UserRegionToAdd> listToBeAdded = new List<UserRegionToAdd>();

            //List<UserRegionList> listToAdd = new List<UserRegionList>();
            
            //Get the list of user's region
            if (Session["UserRegionList"] != null)
            {
                listAdded = (List<UserRegionList>)Session["UserRegionList"];
            }
            else 
            {
                foreach (ListViewItem item in uoListViewRegion.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                    uoLabelRegion = (Label)item.FindControl("uoLabelRegion");
                    bool IsExist = false;
                    if (uoCheckBoxSelect.Enabled)
                    {
                        IsExist = true;
                    }
                    UserRegionList listAddedItem = new UserRegionList();
                    listAddedItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    listAddedItem.RegionName = uoLabelRegion.Text;
                    listAddedItem.UserRegionID = 0;
                    listAddedItem.IsExist = IsExist;
                    
                    listAdded.Add(listAddedItem);
                }
            }

            //Get the list of region to be added to user
            if (Session["UserRegionToAdd"] != null)
            {
                listToBeAdded = (List<UserRegionToAdd>)Session["UserRegionToAdd"];
            }
            else
            {
                foreach (ListViewItem item in uoListViewRegionToAdd.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                    uoLabelRegion = (Label)item.FindControl("uoLabelRegion");

                    UserRegionToAdd listItem = new UserRegionToAdd();
                    listItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    listItem.RegionName = uoLabelRegion.Text;

                    listToBeAdded.Add(listItem);
                }
            }

            //Get selected region to add
            foreach (ListViewItem item in uoListViewRegionToAdd.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                uoLabelRegion = (Label)item.FindControl("uoLabelRegion");

                if (uoCheckBoxSelect.Checked)
                {
                    UserRegionList listItem = new UserRegionList();
                    listItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    listItem.RegionName = uoLabelRegion.Text;
                    listItem.IsExist = true;
                    listItem.UserRegionID = 0;

                    listAdded.Add(listItem);
                    listToBeAdded.RemoveAll(a => a.RegionID == GlobalCode.Field2Int(uoHiddenFieldRegion.Value));
                }
            }
           
            listToBeAdded = listToBeAdded.OrderBy(a => a.RegionName).ToList();
            listAdded = listAdded.OrderBy(a => a.RegionName).ToList();

            Session["UserRegionToAdd"] = listToBeAdded;
            Session["UserRegionList"] = listAdded;

            uoListViewRegion.DataSource = listAdded;
            uoListViewRegion.DataBind();

            uoListViewRegionToAdd.DataSource = listToBeAdded;
            uoListViewRegionToAdd.DataBind();
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Move Region from right to left list
        /// </summary>
        private void RemoveRegion()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldRegion;
            Label uoLabelRegion;

            List<UserRegionList> listAdded = new List<UserRegionList>();
            List<UserRegionToAdd> listToBeAdded = new List<UserRegionToAdd>();

            //List<UserRegionList> listToAdd = new List<UserRegionList>();

            //Get the list of user's region
            if (Session["UserRegionList"] != null)
            {
                listAdded = (List<UserRegionList>)Session["UserRegionList"];
            }
            else
            {
                foreach (ListViewItem item in uoListViewRegion.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                    uoLabelRegion = (Label)item.FindControl("uoLabelRegion");
                    bool IsExist = false;
                    if (uoCheckBoxSelect.Enabled)
                    {
                        IsExist = true;
                    }
                    UserRegionList listAddedItem = new UserRegionList();
                    listAddedItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    listAddedItem.RegionName = uoLabelRegion.Text;
                    listAddedItem.UserRegionID = 0;
                    listAddedItem.IsExist = IsExist;

                    listAdded.Add(listAddedItem);
                }
            }

            //Get the list of region to be added to user
            if (Session["UserRegionToAdd"] != null)
            {
                listToBeAdded = (List<UserRegionToAdd>)Session["UserRegionToAdd"];
            }
            else
            {
                foreach (ListViewItem item in uoListViewRegionToAdd.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                    uoLabelRegion = (Label)item.FindControl("uoLabelRegion");

                    UserRegionToAdd listItem = new UserRegionToAdd();
                    listItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    listItem.RegionName = uoLabelRegion.Text;

                    listToBeAdded.Add(listItem);
                }
            }

            //Get selected region to add
            foreach (ListViewItem item in uoListViewRegion.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                uoLabelRegion = (Label)item.FindControl("uoLabelRegion");

                if (uoCheckBoxSelect.Checked)
                {
                    UserRegionToAdd listItem = new UserRegionToAdd();
                    listItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    listItem.RegionName = uoLabelRegion.Text;

                    listToBeAdded.Add(listItem);
                    listAdded.RemoveAll(a => a.RegionID == GlobalCode.Field2Int(uoHiddenFieldRegion.Value));
                }
            }

            listToBeAdded = listToBeAdded.OrderBy(a => a.RegionName).ToList();
            listAdded = listAdded.OrderBy(a => a.RegionName).ToList();

            Session["UserRegionToAdd"] = listToBeAdded;
            Session["UserRegionList"] = listAdded;

            uoListViewRegion.DataSource = listAdded;
            uoListViewRegion.DataBind();

            uoListViewRegionToAdd.DataSource = listToBeAdded;
            uoListViewRegionToAdd.DataBind();
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Save Region to Database
        /// </summary>
        private void SaveUserRegion()
        {
            DataTable dt = null;
            try
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldRegion;
                Label uoLabelRegion;

                List<UserRegionList> listAdded = new List<UserRegionList>();
                List<UserRegionToAdd> listToBeAdded = new List<UserRegionToAdd>();

                //List<UserRegionList> listToAdd = new List<UserRegionList>();

                //Get the list of user's region
                if (Session["UserRegionList"] != null)
                {
                    listAdded = (List<UserRegionList>)Session["UserRegionList"];
                }
                else
                {
                    foreach (ListViewItem item in uoListViewRegion.Items)
                    {
                        uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                        uoHiddenFieldRegion = (HiddenField)item.FindControl("uoHiddenFieldRegion");
                        uoLabelRegion = (Label)item.FindControl("uoLabelRegion");
                        bool IsExist = false;
                        if (uoCheckBoxSelect.Enabled)
                        {
                            IsExist = true;
                        }
                        UserRegionList listAddedItem = new UserRegionList();
                        listAddedItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                        listAddedItem.RegionName = uoLabelRegion.Text;
                        listAddedItem.UserRegionID = 0;
                        listAddedItem.IsExist = IsExist;

                        listAdded.Add(listAddedItem);
                    }
                }

                dt = new DataTable();
                dt.Columns.Add("UserName", typeof(string));
                dt.Columns.Add("RegonID", typeof(string));

                for (int i = 0; i < listAdded.Count; i++)
                {
                    //UserRegionToAdd item = new UserRegionToAdd();
                    //item.RegionID = listAdded[i].RegionID;
                    //item.RegionName = listAdded[i].RegionName;
                    //listToBeAdded.Add(item);
                    DataRow r = dt.NewRow();
                    r["UserName"] = uoDropDownListUsers.SelectedValue;
                    r["RegonID"] = listAdded[i].RegionID;
                    dt.Rows.Add(r);
                }

                string strLogDescription = "Region of User added";
                string strFunction = "SaveUserRegion";

                //dt = getDataTable(listToBeAdded);
                UserRightsBLL.SaveUserRegion(dt, uoHiddenFieldUser.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path));
                AlertMessage("User Region successfully saved.");

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   11/Jan/2013
        /// Description:    convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            System.Reflection.PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (System.Reflection.PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// Description:    Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
        }
        #endregion
    }
}

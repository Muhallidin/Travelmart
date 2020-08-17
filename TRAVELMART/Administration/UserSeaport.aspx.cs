using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.Reflection;
using System.IO;

namespace TRAVELMART
{
    public partial class UserSeaport : System.Web.UI.Page
    {
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Request.QueryString["uId"]);
                uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);
                BindListViewSeaportNotInUser(true, false);
                BindListViewSeaportInUser(true);
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            BindListViewSeaportNotInUser(true, true);
        }
        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            AddSeaport();
        }
        protected void uoButtonDelete_Click(object sender, EventArgs e)
        {
            RemoveSeaport();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveUserSeaport();
        }
        #endregion

        #region "Functions"

        private void BindListViewSeaportNotInUser(bool IsNew, bool IsFromSearch)
        {
            List<Seaport> list = GetSeaportNotInUser(IsNew, IsFromSearch);
            uoListViewSeaport.DataSource = list;
            uoListViewSeaport.DataBind();
        }
        private void BindListViewSeaportInUser(bool IsNew)
        {
            List<Seaport> list = GetSeaportInUser(IsNew);
            uoListViewSeaportSaved.DataSource = list;
            uoListViewSeaportSaved.DataBind();
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport not in user's access
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<Seaport> GetSeaportNotInUser(bool IsNew, bool IsFromSearch)
        {
            List<Seaport> list = new List<Seaport>();
            List<Seaport> listAdded = new List<Seaport>();   
            string sSeaportCode = "";
            string sSeaportName = "";

            if(uoDropDownListSearchType.SelectedValue == "Code")
            {
                sSeaportCode = uoTextBoxSearch.Text.Trim();
            }
            else
            {
                sSeaportName = uoTextBoxSearch.Text.Trim();
            }

            if (IsNew)
            {
                list = UserAccountBLL.GetUserSeaport(uoHiddenFieldUser.Value, sSeaportCode, sSeaportName, true);
            }
            else
            {
                if (Session["SeaportListNotInUser"] == null)
                {
                    list = UserAccountBLL.GetUserSeaport(uoHiddenFieldUser.Value, sSeaportCode, sSeaportName, true);
                }
                else
                {
                    list = (List<Seaport>)Session["SeaportListNotInUser"];
                }
            }

            if (IsFromSearch)
            {
                listAdded = GetSeaportInUser(false);
            }
            else
            {
                listAdded = GetSeaportInUser(IsNew);
            }
            list.RemoveAll(a => listAdded.Exists(b=> a.SeaportName == b.SeaportName));
            list = list.OrderBy(a => a.SeaportName).ToList();

            Session["SeaportListNotInUser"] = list;
            return list;
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport in user's access
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<Seaport> GetSeaportInUser(bool IsNew)
        {
            List<Seaport> list = new List<Seaport>();
           
            if (IsNew)
            {
                list = UserAccountBLL.GetUserSeaport(uoHiddenFieldUser.Value, "", "", false);
            }
            else
            {
                if (Session["SeaportListInUser"] == null)
                {
                    list = UserAccountBLL.GetUserSeaport(uoHiddenFieldUser.Value, "", "", false);
                }
                else
                {
                    list = (List<Seaport>)Session["SeaportListInUser"];
                }
            }
            list = list.OrderBy(a => a.SeaportName).ToList();
            Session["SeaportListInUser"] = list;
            return list;
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Move Seaport from left to right
        /// </summary>
        private void AddSeaport()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldSeaport;
            Label uoLabelSeaport;

            List<Seaport> listToBeAdded = new List<Seaport>();
            List<Seaport> listAdded = new List<Seaport>();

            listToBeAdded = GetSeaportNotInUser(false, false);
            listAdded = GetSeaportInUser(false);

            foreach (ListViewItem item in uoListViewSeaport.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");
                    uoLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                    var listToAdd = (from a in listToBeAdded
                                     where a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value)
                                     select new
                                     {
                                         SeaportID = a.SeaportID,
                                         SeaportName = a.SeaportName,
                                     }).ToList();

                    listToBeAdded.RemoveAll(a => a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value));
                    Seaport newSeaport = new Seaport();
                    newSeaport.SeaportID = listToAdd[0].SeaportID;
                    newSeaport.SeaportName = listToAdd[0].SeaportName;
                    listAdded.Insert(listAdded.Count, newSeaport);
                }
            }
            listToBeAdded = listToBeAdded.OrderBy(a => a.SeaportName).ToList();
            listAdded = listAdded.OrderBy(a => a.SeaportName).ToList();

            Session["SeaportListNotInUser"] = listToBeAdded;
            Session["SeaportListInUser"] = listAdded;

            uoListViewSeaport.DataSource = listToBeAdded;
            uoListViewSeaport.DataBind();

            uoListViewSeaportSaved.DataSource = listAdded;
            uoListViewSeaportSaved.DataBind();
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Move Seaport from right to left
        /// </summary>
        private void RemoveSeaport()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldSeaport;
            Label uoLabelSeaport;

            List<Seaport> listToBeAdded = new List<Seaport>();
            List<Seaport> listAdded = new List<Seaport>();

            listToBeAdded = GetSeaportNotInUser(false, false);
            listAdded = GetSeaportInUser(false);

            foreach (ListViewItem item in uoListViewSeaportSaved.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");
                    uoLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                    var listToRemove = (from a in listAdded
                                     where a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value)
                                     select new
                                     {
                                         SeaportID = a.SeaportID,
                                         SeaportName = a.SeaportName,
                                     }).ToList();

                    listAdded.RemoveAll(a => a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value));
                    Seaport removedSeaport = new Seaport();
                    removedSeaport.SeaportID = listToRemove[0].SeaportID;
                    removedSeaport.SeaportName = listToRemove[0].SeaportName;
                    listToBeAdded.Insert(listAdded.Count, removedSeaport);
                }
            }
            listToBeAdded = listToBeAdded.OrderBy(a => a.SeaportName).ToList();
            listAdded = listAdded.OrderBy(a => a.SeaportName).ToList();

            Session["SeaportListNotInUser"] = listToBeAdded;
            Session["SeaportListInUser"] = listAdded;

            uoListViewSeaport.DataSource = listToBeAdded;
            uoListViewSeaport.DataBind();

            uoListViewSeaportSaved.DataSource = listAdded;
            uoListViewSeaportSaved.DataBind();
        }
        /// <summary>
        /// Author: Chralene Remotigue
        /// Date Created: 17/04/2012
        /// Description: convert list to datatable
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

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 17/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
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
        /// Date Created:   25/09/2011
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
        /// <summary>
        /// Date Created:   25/09/2011
        /// Created By:     Josephine Gad
        /// Description:    Save User Seaport
        /// </summary>
        private void SaveUserSeaport()
        {
            DataTable dt = null;
            HiddenField uoHiddenFieldSeaport;

            List<UserSeaportList> list = new List<UserSeaportList>();
            try
            {
                foreach (ListViewItem item in uoListViewSeaportSaved.Items)
                {
                    uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");

                    UserSeaportList SeaportItem = new UserSeaportList();
                    SeaportItem.SeaportID = GlobalCode.Field2Int(uoHiddenFieldSeaport.Value);
                    SeaportItem.UserName = uoHiddenFieldUser.Value;
                    list.Add(SeaportItem);
                }
                string strLogDescription = "Seaport of User added";
                string strFunction = "SaveUserSeaport";

                dt = getDataTable(list);
                UserAccountBLL.SaveUserSeaport(dt, uoHiddenFieldUserName.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path));
                AlertMessage("User Seaport successfully saved.");
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
        #endregion
    }
}

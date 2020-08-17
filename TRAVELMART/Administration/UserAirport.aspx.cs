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
    public partial class UserAirport : System.Web.UI.Page
    {
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Request.QueryString["uId"]);
                uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);
                BindListViewAirportNotInUser(true, false);
                BindListViewAirportInUser(true);
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            BindListViewAirportNotInUser(true, true);
        }
        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            AddAirport();
        }
        protected void uoButtonDelete_Click(object sender, EventArgs e)
        {
            RemoveAirport();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveUserAirport();
        }
        #endregion

        #region "Functions"

        private void BindListViewAirportNotInUser(bool IsNew, bool IsFromSearch)
        {
            List<AirportDTO> list = GetAirportNotInUser(IsNew, IsFromSearch);
            uoListViewAirport.DataSource = list;
            uoListViewAirport.DataBind();
        }
        private void BindListViewAirportInUser(bool IsNew)
        {
            List<AirportDTO> list = GetAirportInUser(IsNew);
            uoListViewAirportSaved.DataSource = list;
            uoListViewAirportSaved.DataBind();
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Airport not in user's access
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<AirportDTO> GetAirportNotInUser(bool IsNew, bool IsFromSearch)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            List<AirportDTO> listAdded = new List<AirportDTO>();   
            string sAirportCode = "";
            string sAirportName = "";

            if(uoDropDownListSearchType.SelectedValue == "Code")
            {
                sAirportCode = uoTextBoxSearch.Text.Trim();
            }
            else
            {
                sAirportName = uoTextBoxSearch.Text.Trim();
            }

            if (IsNew)
            {
                list = UserAccountBLL.GetUserAirport(uoHiddenFieldUser.Value, sAirportCode, sAirportName, true);
            }
            else
            {
                if (Session["AirportListNotInUser"] == null)
                {
                    list = UserAccountBLL.GetUserAirport(uoHiddenFieldUser.Value, sAirportCode, sAirportName, true);
                }
                else
                {
                    list = (List<AirportDTO>)Session["AirportListNotInUser"];
                }
            }

            if (IsFromSearch)
            {
                listAdded = GetAirportInUser(false);
            }
            else
            {
                listAdded = GetAirportInUser(IsNew);
            }
            list.RemoveAll(a => listAdded.Exists(b=> a.AirportNameString == b.AirportNameString));
            list = list.OrderBy(a => a.AirportNameString).ToList();

            Session["AirportListNotInUser"] = list;
            return list;
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Airport in user's access
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<AirportDTO> GetAirportInUser(bool IsNew)
        {
            List<AirportDTO> list = new List<AirportDTO>();
           
            if (IsNew)
            {
                list = UserAccountBLL.GetUserAirport(uoHiddenFieldUser.Value, "", "", false);
            }
            else
            {
                if (Session["AirportListInUser"] == null)
                {
                    list = UserAccountBLL.GetUserAirport(uoHiddenFieldUser.Value, "", "", false);
                }
                else
                {
                    list = (List<AirportDTO>)Session["AirportListInUser"];
                }
            }
            list = list.OrderBy(a => a.AirportNameString).ToList();
            Session["AirportListInUser"] = list;
            return list;
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Move Airport from left to right
        /// </summary>
        private void AddAirport()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldAirport;
            Label uoLabelAirport;

            List<AirportDTO> listToBeAdded = new List<AirportDTO>();
            List<AirportDTO> listAdded = new List<AirportDTO>();

            listToBeAdded = GetAirportNotInUser(false, false);
            listAdded = GetAirportInUser(false);

            foreach (ListViewItem item in uoListViewAirport.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirport");
                    uoLabelAirport = (Label)item.FindControl("uoLabelAirport");

                    var listToAdd = (from a in listToBeAdded
                                     where a.AirportIDString == GlobalCode.Field2String(uoHiddenFieldAirport.Value)
                                     select new
                                     {
                                         AirportID = a.AirportIDString,
                                         AirportName = a.AirportNameString,
                                     }).ToList();

                    listToBeAdded.RemoveAll(a => a.AirportIDString == GlobalCode.Field2String(uoHiddenFieldAirport.Value));
                    AirportDTO newAirport = new AirportDTO();
                    newAirport.AirportIDString = listToAdd[0].AirportID;
                    newAirport.AirportNameString = listToAdd[0].AirportName;
                    listAdded.Insert(listAdded.Count, newAirport);
                }
            }
            listToBeAdded = listToBeAdded.OrderBy(a => a.AirportNameString).ToList();
            listAdded = listAdded.OrderBy(a => a.AirportNameString).ToList();

            Session["AirportListNotInUser"] = listToBeAdded;
            Session["AirportListInUser"] = listAdded;

            uoListViewAirport.DataSource = listToBeAdded;
            uoListViewAirport.DataBind();

            uoListViewAirportSaved.DataSource = listAdded;
            uoListViewAirportSaved.DataBind();
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Move Airport from right to left
        /// </summary>
        private void RemoveAirport()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldAirport;
            Label uoLabelAirport;

            List<AirportDTO> listToBeAdded = new List<AirportDTO>();
            List<AirportDTO> listAdded = new List<AirportDTO>();

            listToBeAdded = GetAirportNotInUser(false, false);
            listAdded = GetAirportInUser(false);

            foreach (ListViewItem item in uoListViewAirportSaved.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirport");
                    uoLabelAirport = (Label)item.FindControl("uoLabelAirport");

                    var listToRemove = (from a in listAdded
                                     where a.AirportIDString == GlobalCode.Field2String(uoHiddenFieldAirport.Value)
                                     select new
                                     {
                                         VesslID = a.AirportIDString,
                                         AirportName = a.AirportNameString,
                                     }).ToList();

                    listAdded.RemoveAll(a => a.AirportIDString == GlobalCode.Field2String(uoHiddenFieldAirport.Value));
                    AirportDTO removedAirport = new AirportDTO();
                    removedAirport.AirportIDString = listToRemove[0].VesslID;
                    removedAirport.AirportNameString = listToRemove[0].AirportName;
                    listToBeAdded.Insert(listAdded.Count, removedAirport);
                }
            }
            listToBeAdded = listToBeAdded.OrderBy(a => a.AirportNameString).ToList();
            listAdded = listAdded.OrderBy(a => a.AirportNameString).ToList();

            Session["AirportListNotInUser"] = listToBeAdded;
            Session["AirportListInUser"] = listAdded;

            uoListViewAirport.DataSource = listToBeAdded;
            uoListViewAirport.DataBind();

            uoListViewAirportSaved.DataSource = listAdded;
            uoListViewAirportSaved.DataBind();
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
        /// Description:    Save User Airport
        /// </summary>
        private void SaveUserAirport()
        {
            DataTable dt = null;
            HiddenField uoHiddenFieldAirport;

            List<UserAirportList> list = new List<UserAirportList>();
            try
            {
                foreach (ListViewItem item in uoListViewAirportSaved.Items)
                {
                    uoHiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirport");

                    UserAirportList AirportItem = new UserAirportList();
                    AirportItem.AirportID = GlobalCode.Field2Int(uoHiddenFieldAirport.Value);
                    AirportItem.UserName = uoHiddenFieldUser.Value;
                    list.Add(AirportItem);
                }
                string strLogDescription = "Airport of User added";
                string strFunction = "SaveUserAirport";

                dt = getDataTable(list);
                UserAccountBLL.SaveUserAirport(dt, uoHiddenFieldUserName.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path));
                AlertMessage("User Airport successfully saved.");
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

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
    public partial class UserVessel : System.Web.UI.Page
    {
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                uoHiddenFieldUser.Value = GlobalCode.Field2String(Request.QueryString["uId"]);
                uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);

                //Audit Trail
                string strLogDescription = "User Vessel Maintenance Viewed";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(uoHiddenFieldUser.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                BindListViewVesselNotInUser(true, false);
                BindListViewVesselInUser(true);
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            BindListViewVesselNotInUser(true, true);
        }
        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            AddVessel();
        }
        protected void uoButtonDelete_Click(object sender, EventArgs e)
        {
            RemoveVessel();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveUserVessel();
        }
        #endregion

        #region "Functions"

        private void BindListViewVesselNotInUser(bool IsNew, bool IsFromSearch)
        {
            List<VesselDTO> list = GetVesselNotInUser(IsNew, IsFromSearch);
            uoListViewVessel.DataSource = list;
            uoListViewVessel.DataBind();
        }
        private void BindListViewVesselInUser(bool IsNew)
        {
            List<VesselDTO> list = GetVesselInUser(IsNew);
            uoListViewVesselSaved.DataSource = list;
            uoListViewVesselSaved.DataBind();
        }
        /// <summary>
        /// Date Created:   24/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Vessel not in user's access
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<VesselDTO> GetVesselNotInUser(bool IsNew, bool IsFromSearch)
        {
            List<VesselDTO> list = new List<VesselDTO>();
            List<VesselDTO> listAdded = new List<VesselDTO>();   
            string sVesselCode = "";
            string sVesselName = "";

            if(uoDropDownListSearchType.SelectedValue == "Code")
            {
                sVesselCode = uoTextBoxSearch.Text.Trim();
            }
            else
            {
                sVesselName = uoTextBoxSearch.Text.Trim();
            }

            if (IsNew)
            {
                list = UserAccountBLL.GetUserVessel(uoHiddenFieldUser.Value, sVesselCode, sVesselName, true);
            }
            else
            {
                if (Session["VesselListNotInUser"] == null)
                {
                    list = UserAccountBLL.GetUserVessel(uoHiddenFieldUser.Value, sVesselCode, sVesselName, true);
                }
                else
                {
                    list = (List<VesselDTO>)Session["VesselListNotInUser"];
                }
            }

            if (IsFromSearch)
            {
                listAdded = GetVesselInUser(false);
            }
            else
            {
                listAdded = GetVesselInUser(IsNew);
            }
            list.RemoveAll(a => listAdded.Exists(b=> a.VesselNameString == b.VesselNameString));
            list = list.OrderBy(a => a.VesselNameString).ToList();

            Session["VesselListNotInUser"] = list;
            return list;
        }
        /// <summary>
        /// Date Created:   24/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Vessel in user's access
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<VesselDTO> GetVesselInUser(bool IsNew)
        {
            List<VesselDTO> list = new List<VesselDTO>();
           
            if (IsNew)
            {
                list = UserAccountBLL.GetUserVessel(uoHiddenFieldUser.Value, "", "", false);
            }
            else
            {
                if (Session["VesselListInUser"] == null)
                {
                    list = UserAccountBLL.GetUserVessel(uoHiddenFieldUser.Value, "", "", false);
                }
                else
                {
                    list = (List<VesselDTO>)Session["VesselListInUser"];
                }
            }
            list = list.OrderBy(a => a.VesselNameString).ToList();
            Session["VesselListInUser"] = list;
            return list;
        }
        /// <summary>
        /// Date Created:   24/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Move Vessel from left to right
        /// </summary>
        private void AddVessel()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldVessel;
            Label uoLabelVessel;

            List<VesselDTO> listToBeAdded = new List<VesselDTO>();
            List<VesselDTO> listAdded = new List<VesselDTO>();

            listToBeAdded = GetVesselNotInUser(false, false);
            listAdded = GetVesselInUser(false);

            foreach (ListViewItem item in uoListViewVessel.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldVessel = (HiddenField)item.FindControl("uoHiddenFieldVessel");
                    uoLabelVessel = (Label)item.FindControl("uoLabelVessel");

                    var listToAdd = (from a in listToBeAdded
                                     where a.VesselIDString == GlobalCode.Field2String(uoHiddenFieldVessel.Value)
                                     select new
                                     {
                                         VesslID = a.VesselIDString,
                                         VesselName = a.VesselNameString,
                                     }).ToList();

                    listToBeAdded.RemoveAll(a => a.VesselIDString == GlobalCode.Field2String(uoHiddenFieldVessel.Value));
                    VesselDTO newVessel = new VesselDTO();
                    newVessel.VesselIDString = listToAdd[0].VesslID;
                    newVessel.VesselNameString = listToAdd[0].VesselName;
                    listAdded.Insert(listAdded.Count, newVessel);
                }
            }
            listToBeAdded = listToBeAdded.OrderBy(a => a.VesselNameString).ToList();
            listAdded = listAdded.OrderBy(a => a.VesselNameString).ToList();

            Session["VesselListNotInUser"] = listToBeAdded;
            Session["VesselListInUser"] = listAdded;

            uoListViewVessel.DataSource = listToBeAdded;
            uoListViewVessel.DataBind();

            uoListViewVesselSaved.DataSource = listAdded;
            uoListViewVesselSaved.DataBind();
        }
        /// <summary>
        /// Date Created:   24/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Move Vessel from right to left
        /// </summary>
        private void RemoveVessel()
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldVessel;
            Label uoLabelVessel;

            List<VesselDTO> listToBeAdded = new List<VesselDTO>();
            List<VesselDTO> listAdded = new List<VesselDTO>();

            listToBeAdded = GetVesselNotInUser(false, false);
            listAdded = GetVesselInUser(false);

            foreach (ListViewItem item in uoListViewVesselSaved.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked == true)
                {
                    uoHiddenFieldVessel = (HiddenField)item.FindControl("uoHiddenFieldVessel");
                    uoLabelVessel = (Label)item.FindControl("uoLabelVessel");

                    var listToRemove = (from a in listAdded
                                     where a.VesselIDString == GlobalCode.Field2String(uoHiddenFieldVessel.Value)
                                     select new
                                     {
                                         VesslID = a.VesselIDString,
                                         VesselName = a.VesselNameString,
                                     }).ToList();

                    listAdded.RemoveAll(a => a.VesselIDString == GlobalCode.Field2String(uoHiddenFieldVessel.Value));
                    VesselDTO removedVessel = new VesselDTO();
                    removedVessel.VesselIDString = listToRemove[0].VesslID;
                    removedVessel.VesselNameString = listToRemove[0].VesselName;
                    listToBeAdded.Insert(listAdded.Count, removedVessel);
                }
            }
            listToBeAdded = listToBeAdded.OrderBy(a => a.VesselNameString).ToList();
            listAdded = listAdded.OrderBy(a => a.VesselNameString).ToList();

            Session["VesselListNotInUser"] = listToBeAdded;
            Session["VesselListInUser"] = listAdded;

            uoListViewVessel.DataSource = listToBeAdded;
            uoListViewVessel.DataBind();

            uoListViewVesselSaved.DataSource = listAdded;
            uoListViewVesselSaved.DataBind();
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
        /// Date Created:   24/09/2011
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
        /// Date Created:   24/09/2011
        /// Created By:     Josephine Gad
        /// Description:    Save User Vessel
        /// </summary>
        private void SaveUserVessel()
        {
            DataTable dt = null;
            HiddenField uoHiddenFieldVessel;

            List<UserVesselList> list = new List<UserVesselList>();
            try
            {
                foreach (ListViewItem item in uoListViewVesselSaved.Items)
                {
                    uoHiddenFieldVessel = (HiddenField)item.FindControl("uoHiddenFieldVessel");

                    UserVesselList vesselItem = new UserVesselList();
                    vesselItem.VesselID = GlobalCode.Field2Int(uoHiddenFieldVessel.Value);
                    vesselItem.UserName = uoHiddenFieldUser.Value;
                    list.Add(vesselItem);
                }
                string strLogDescription = "Vessel of User added";
                string strFunction = "SaveUserVessel";

                dt = getDataTable(list);
                UserAccountBLL.SaveUserVessel(dt, uoHiddenFieldUserName.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path));
                AlertMessage("User vessel successfully saved.");
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

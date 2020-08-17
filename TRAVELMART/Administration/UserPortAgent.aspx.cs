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
        public partial class UserPortAgent : System.Web.UI.Page
        {
            #region "Events"
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    uoHiddenFieldUser.Value = GlobalCode.Field2String(Request.QueryString["uId"]);
                    uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);
                    BindListViewPortAgenttNotInUser(true, false);
                    BindListViewSeaportInUser(true);
                }
            }
            protected void uoButtonSearch_Click(object sender, EventArgs e)
            {
                BindListViewPortAgenttNotInUser(true, true);
            }
            protected void uoButtonAdd_Click(object sender, EventArgs e)
            {
                AddPortAgent();
            }
            protected void uoButtonDelete_Click(object sender, EventArgs e)
            {
                RemovePortAgent();
            }
            protected void uoButtonSave_Click(object sender, EventArgs e)
            {
                SaveUserPortAgent();
            }
            #endregion

            #region "Functions"

            private void BindListViewPortAgenttNotInUser(bool IsNew, bool IsFromSearch)
            {
                List<PortAgentDTO> list = GetPortAgentNotInUser(IsNew, IsFromSearch);
                uoListViewPortAgent.DataSource = list;
                uoListViewPortAgent.DataBind();
            }
            private void BindListViewSeaportInUser(bool IsNew)
            {
                List<PortAgentDTO> list = GetPortAgentInUser(IsNew);
                uoListViewPortAgentSaved.DataSource = list;
                uoListViewPortAgentSaved.DataBind();
            }
            /// <summary>
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// (description)   Get Service Provider not in user's access
            /// </summary>
            /// <param name="IsNew"></param>
            /// <returns></returns>
            private List<PortAgentDTO> GetPortAgentNotInUser(bool IsNew, bool IsFromSearch)
            {
                List<PortAgentDTO> list = new List<PortAgentDTO>();
                List<PortAgentDTO> listAdded = new List<PortAgentDTO>();   


                if (IsNew)
                {
                    list = UserVendorBLL.GetUserPortAgent(uoHiddenFieldUser.Value, uoTextBoxSearch.Text.Trim(), true);
                }
                else
                {
                    if (Session["PortAgentListNotInUser"] == null)
                    {
                        list = UserVendorBLL.GetUserPortAgent(uoHiddenFieldUser.Value, uoTextBoxSearch.Text.Trim(), true);
                    }
                    else
                    {
                        list = (List<PortAgentDTO>)Session["PortAgentListNotInUser"];
                    }
                }

                if (IsFromSearch)
                {
                    listAdded = GetPortAgentInUser(false);
                }
                else
                {
                    listAdded = GetPortAgentInUser(IsNew);
                }
                list.RemoveAll(a => listAdded.Exists(b => a.PortAgentName == b.PortAgentName));
                list = list.OrderBy(a => a.PortAgentName).ToList();

                Session["PortAgentListNotInUser"] = list;
                return list;
            }
            /// <summary>
            /// Date Created:   25/09/2012
            /// Created By:     Josephine Gad
            /// (description)   Get Seaport in user's access
            /// </summary>
            /// <param name="IsNew"></param>
            /// <returns></returns>
            private List<PortAgentDTO> GetPortAgentInUser(bool IsNew)
            {
                List<PortAgentDTO> list = new List<PortAgentDTO>();
               
                if (IsNew)
                {
                    list = UserVendorBLL.GetUserPortAgent(uoHiddenFieldUser.Value, "", false);
                }
                else
                {
                    if (Session["PortAgentListInUser"] == null)
                    {
                        list = UserVendorBLL.GetUserPortAgent(uoHiddenFieldUser.Value, "", false);
                    }
                    else
                    {
                        list = (List<PortAgentDTO>)Session["PortAgentListInUser"];
                    }
                }
                list = list.OrderBy(a => a.PortAgentName).ToList();
                Session["PortAgentListInUser"] = list;
                return list;
            }
            /// <summary>
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// (description)   Move PortAgent from left to right
            /// </summary>
            private void AddPortAgent()
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldPortAgent;
                Label uoLabelSeaport;

                List<PortAgentDTO> listToBeAdded = new List<PortAgentDTO>();
                List<PortAgentDTO> listAdded = new List<PortAgentDTO>();

                listToBeAdded = GetPortAgentNotInUser(false, false);
                listAdded = GetPortAgentInUser(false);

                foreach (ListViewItem item in uoListViewPortAgent.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    if (uoCheckBoxSelect.Checked == true)
                    {
                        uoHiddenFieldPortAgent = (HiddenField)item.FindControl("uoHiddenFieldPortAgent");
                        uoLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                        var listToAdd = (from a in listToBeAdded
                                         where a.PortAgentID == (uoHiddenFieldPortAgent.Value)
                                         select new
                                         {
                                             PortAgentID = a.PortAgentID,
                                             PortAgentName = a.PortAgentName,
                                         }).ToList();

                        listToBeAdded.RemoveAll(a => a.PortAgentID == uoHiddenFieldPortAgent.Value);
                        PortAgentDTO newSeaport = new PortAgentDTO();
                        newSeaport.PortAgentID = listToAdd[0].PortAgentID;
                        newSeaport.PortAgentName = listToAdd[0].PortAgentName;
                        listAdded.Insert(listAdded.Count, newSeaport);
                    }
                }
                listToBeAdded = listToBeAdded.OrderBy(a => a.PortAgentName).ToList();
                listAdded = listAdded.OrderBy(a => a.PortAgentName).ToList();

                Session["PortAgentListNotInUser"] = listToBeAdded;
                Session["PortAgentListInUser"] = listAdded;

                uoListViewPortAgent.DataSource = listToBeAdded;
                uoListViewPortAgent.DataBind();

                uoListViewPortAgentSaved.DataSource = listAdded;
                uoListViewPortAgentSaved.DataBind();
            }
            /// <summary>
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// (description)   Move PortAgent from right to left
            /// </summary>
            private void RemovePortAgent()
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldPortAgent;
                Label uoLabelSeaport;

                List<PortAgentDTO> listToBeAdded = new List<PortAgentDTO>();
                List<PortAgentDTO> listAdded = new List<PortAgentDTO>();

                listToBeAdded = GetPortAgentNotInUser(false, false);
                listAdded = GetPortAgentInUser(false);

                foreach (ListViewItem item in uoListViewPortAgentSaved.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    if (uoCheckBoxSelect.Checked == true)
                    {
                        uoHiddenFieldPortAgent = (HiddenField)item.FindControl("uoHiddenFieldPortAgent");
                        uoLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                        var listToRemove = (from a in listAdded
                                         where a.PortAgentID == uoHiddenFieldPortAgent.Value
                                         select new
                                         {
                                             PortAgentID = a.PortAgentID,
                                             PortAgentName = a.PortAgentName,
                                         }).ToList();

                        listAdded.RemoveAll(a => a.PortAgentID == uoHiddenFieldPortAgent.Value);
                        PortAgentDTO removedPortAgent = new PortAgentDTO();
                        removedPortAgent.PortAgentID = listToRemove[0].PortAgentID;
                        removedPortAgent.PortAgentName = listToRemove[0].PortAgentName;
                        listToBeAdded.Insert(listToBeAdded.Count, removedPortAgent);
                    }
                }
                listToBeAdded = listToBeAdded.OrderBy(a => a.PortAgentName).ToList();
                listAdded = listAdded.OrderBy(a => a.PortAgentName).ToList();

                Session["PortAgentListNotInUser"] = listToBeAdded;
                Session["PortAgentListInUser"] = listAdded;

                uoListViewPortAgent.DataSource = listToBeAdded;
                uoListViewPortAgent.DataBind();

                uoListViewPortAgentSaved.DataSource = listAdded;
                uoListViewPortAgentSaved.DataBind();
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
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// Description:    Save User Service Provider
            /// </summary>
            private void SaveUserPortAgent()
            {
                DataTable dt = null;
                HiddenField uoHiddenFieldPortAgent;

                List<UserPortAgentList> list = new List<UserPortAgentList>();
                try
                {
                    foreach (ListViewItem item in uoListViewPortAgentSaved.Items)
                    {
                        uoHiddenFieldPortAgent = (HiddenField)item.FindControl("uoHiddenFieldPortAgent");

                        UserPortAgentList PortAgentItem = new UserPortAgentList();
                        PortAgentItem.PortAgentID = GlobalCode.Field2Int(uoHiddenFieldPortAgent.Value);
                        PortAgentItem.UserName = uoHiddenFieldUser.Value;
                        list.Add(PortAgentItem);
                    }
                    string strLogDescription = "Service Provider of User added";
                    string strFunction = "SaveUserSeaport";

                    dt = getDataTable(list);
                    UserVendorBLL.SaveUserPortAgent(dt, uoHiddenFieldUserName.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path));
                    AlertMessage("User Service Provider successfully saved.");
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

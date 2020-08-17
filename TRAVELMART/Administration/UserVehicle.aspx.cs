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
        public partial class UserVehicle : System.Web.UI.Page
        {
            #region "Events"
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    uoHiddenFieldUser.Value = GlobalCode.Field2String(Request.QueryString["uId"]);
                    uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);
                    BindListViewVehicleNotInUser(true, false);
                    BindListViewSeaportInUser(true);
                }
            }
            protected void uoButtonSearch_Click(object sender, EventArgs e)
            {
                BindListViewVehicleNotInUser(true, true);
            }
            protected void uoButtonAdd_Click(object sender, EventArgs e)
            {
                AddVehicleVendor();
            }
            protected void uoButtonDelete_Click(object sender, EventArgs e)
            {
                RemoveVehicleVendor();
            }
            protected void uoButtonSave_Click(object sender, EventArgs e)
            {
                SaveUserVehicleVendor();
            }
            #endregion

            #region "Functions"

            private void BindListViewVehicleNotInUser(bool IsNew, bool IsFromSearch)
            {
                List<VehicleVendorDTO> list = GetVehicleVendorNotInUser(IsNew, IsFromSearch);
                uoListViewVehicle.DataSource = list;
                uoListViewVehicle.DataBind();
            }
            private void BindListViewSeaportInUser(bool IsNew)
            {
                List<VehicleVendorDTO> list = GetVehicleVendorInUser(IsNew);
                uoListViewVehicleSaved.DataSource = list;
                uoListViewVehicleSaved.DataBind();
            }
            /// <summary>
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// (description)   Get Vehicle Vendor not in user's access
            /// </summary>
            /// <param name="IsNew"></param>
            /// <returns></returns>
            private List<VehicleVendorDTO> GetVehicleVendorNotInUser(bool IsNew, bool IsFromSearch)
            {
                List<VehicleVendorDTO> list = new List<VehicleVendorDTO>();
                List<VehicleVendorDTO> listAdded = new List<VehicleVendorDTO>();   


                if (IsNew)
                {
                    list = UserVendorBLL.GetUserVehicleVendor(uoHiddenFieldUser.Value, uoTextBoxSearch.Text.Trim(), true);
                }
                else
                {
                    if (Session["VehicleListNotInUser"] == null)
                    {
                        list = UserVendorBLL.GetUserVehicleVendor(uoHiddenFieldUser.Value, uoTextBoxSearch.Text.Trim(), true);
                    }
                    else
                    {
                        list = (List<VehicleVendorDTO>)Session["VehicleListNotInUser"];
                    }
                }

                if (IsFromSearch)
                {
                    listAdded = GetVehicleVendorInUser(false);
                }
                else
                {
                    listAdded = GetVehicleVendorInUser(IsNew);
                }
                list.RemoveAll(a => listAdded.Exists(b => a.VehicleName == b.VehicleName));
                list = list.OrderBy(a => a.VehicleName).ToList();

                Session["VehicleListNotInUser"] = list;
                return list;
            }
            /// <summary>
            /// Date Created:   25/09/2012
            /// Created By:     Josephine Gad
            /// (description)   Get Seaport in user's access
            /// </summary>
            /// <param name="IsNew"></param>
            /// <returns></returns>
            private List<VehicleVendorDTO> GetVehicleVendorInUser(bool IsNew)
            {
                List<VehicleVendorDTO> list = new List<VehicleVendorDTO>();
               
                if (IsNew)
                {
                    list = UserVendorBLL.GetUserVehicleVendor(uoHiddenFieldUser.Value, "", false);
                }
                else
                {
                    if (Session["VehicleListInUser"] == null)
                    {
                        list = UserVendorBLL.GetUserVehicleVendor(uoHiddenFieldUser.Value, "", false);
                    }
                    else
                    {
                        list = (List<VehicleVendorDTO>)Session["VehicleListInUser"];
                    }
                }
                list = list.OrderBy(a => a.VehicleName).ToList();
                Session["VehicleListInUser"] = list;
                return list;
            }
            /// <summary>
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// (description)   Move PortAgent from left to right
            /// </summary>
            private void AddVehicleVendor()
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldVehicleVendor;
                Label uoLabelSeaport;

                List<VehicleVendorDTO> listToBeAdded = new List<VehicleVendorDTO>();
                List<VehicleVendorDTO> listAdded = new List<VehicleVendorDTO>();

                listToBeAdded = GetVehicleVendorNotInUser(false, false);
                listAdded = GetVehicleVendorInUser(false);

                foreach (ListViewItem item in uoListViewVehicle.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    if (uoCheckBoxSelect.Checked == true)
                    {
                        uoHiddenFieldVehicleVendor = (HiddenField)item.FindControl("uoHiddenFieldVehicleVendor");
                        uoLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                        var listToAdd = (from a in listToBeAdded
                                         where a.VehicleID == (uoHiddenFieldVehicleVendor.Value)
                                         select new
                                         {
                                             VehicleID = a.VehicleID,
                                             VehicleName = a.VehicleName,
                                         }).ToList();

                        listToBeAdded.RemoveAll(a => a.VehicleID == uoHiddenFieldVehicleVendor.Value);
                        VehicleVendorDTO newSeaport = new VehicleVendorDTO();
                        newSeaport.VehicleID = listToAdd[0].VehicleID;
                        newSeaport.VehicleName = listToAdd[0].VehicleName;
                        listAdded.Insert(listAdded.Count, newSeaport);
                    }
                }
                listToBeAdded = listToBeAdded.OrderBy(a => a.VehicleName).ToList();
                listAdded = listAdded.OrderBy(a => a.VehicleName).ToList();

                Session["VehicleListNotInUser"] = listToBeAdded;
                Session["VehicleListInUser"] = listAdded;

                uoListViewVehicle.DataSource = listToBeAdded;
                uoListViewVehicle.DataBind();

                uoListViewVehicleSaved.DataSource = listAdded;
                uoListViewVehicleSaved.DataBind();
            }
            /// <summary>
            /// Date Created:   04/Mar/2014
            /// Created By:     Josephine Gad
            /// (description)   Move PortAgent from right to left
            /// </summary>
            private void RemoveVehicleVendor()
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldVehicleVendor;
                Label uoLabelSeaport;

                List<VehicleVendorDTO> listToBeAdded = new List<VehicleVendorDTO>();
                List<VehicleVendorDTO> listAdded = new List<VehicleVendorDTO>();

                listToBeAdded = GetVehicleVendorNotInUser(false, false);
                listAdded = GetVehicleVendorInUser(false);

                foreach (ListViewItem item in uoListViewVehicleSaved.Items)
                {
                    uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    if (uoCheckBoxSelect.Checked == true)
                    {
                        uoHiddenFieldVehicleVendor = (HiddenField)item.FindControl("uoHiddenFieldVehicleVendor");
                        uoLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                        var listToRemove = (from a in listAdded
                                            where a.VehicleID == uoHiddenFieldVehicleVendor.Value
                                         select new
                                         {
                                             VehicleID = a.VehicleID,
                                             VehicleName = a.VehicleName,
                                         }).ToList();

                        listAdded.RemoveAll(a => a.VehicleID == uoHiddenFieldVehicleVendor.Value);
                        VehicleVendorDTO removedPortAgent = new VehicleVendorDTO();
                        removedPortAgent.VehicleID = listToRemove[0].VehicleID;
                        removedPortAgent.VehicleName = listToRemove[0].VehicleName;
                        listToBeAdded.Insert(listToBeAdded.Count, removedPortAgent);
                    }
                }
                listToBeAdded = listToBeAdded.OrderBy(a => a.VehicleName).ToList();
                listAdded = listAdded.OrderBy(a => a.VehicleName).ToList();

                Session["VehicleListNotInUser"] = listToBeAdded;
                Session["VehicleListInUser"] = listAdded;

                uoListViewVehicle.DataSource = listToBeAdded;
                uoListViewVehicle.DataBind();

                uoListViewVehicleSaved.DataSource = listAdded;
                uoListViewVehicleSaved.DataBind();
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
            /// Date Created:   20/May/2014
            /// Created By:     Josephine Gad
            /// Description:    Save User Vehicle Vendor
            /// </summary>
            private void SaveUserVehicleVendor()
            {
                DataTable dt = null;
                HiddenField uoHiddenFieldVehicleVendor;

                List<UserPortAgentList> list = new List<UserPortAgentList>();
                try
                {
                    foreach (ListViewItem item in uoListViewVehicleSaved.Items)
                    {
                        uoHiddenFieldVehicleVendor = (HiddenField)item.FindControl("uoHiddenFieldVehicleVendor");

                        UserPortAgentList PortAgentItem = new UserPortAgentList();
                        PortAgentItem.PortAgentID = GlobalCode.Field2Int(uoHiddenFieldVehicleVendor.Value);
                        PortAgentItem.UserName = uoHiddenFieldUser.Value;
                        list.Add(PortAgentItem);
                    }
                    string strLogDescription = "Vehicle Vendor of User added";
                    string strFunction = "SaveUserVehicleVendor";

                    dt = getDataTable(list);
                    UserVendorBLL.SaveUserVehicleVendor(dt, uoHiddenFieldUserName.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path));
                    AlertMessage("User Vehicle Vendor successfully saved.");
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

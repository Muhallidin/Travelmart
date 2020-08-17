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
        public partial class UserDriver : System.Web.UI.Page
        {
            #region "Events"
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    uoHiddenFieldUser.Value = GlobalCode.Field2String(Request.QueryString["uId"]);
                    uoHiddenFieldUserName.Value = GlobalCode.Field2String(Session["UserName"]);
                    //BindListViewVehicleNotInUser(true, false);
                    //BindListViewSeaportInUser(true);
                }
            }
            protected void uoButtonSearch_Click(object sender, EventArgs e)
            {
                uoHiddenFieldLoadType.Value = "1";
                uoHiddenFieldToLoad.Value = "VehicleToAdd";
                //BindListViewVehicleNotInUser(true, true);
            }
            protected void uoButtonAdd_Click(object sender, EventArgs e)
            {
                //uoHiddenFieldToLoad.Value = "BothVehicle";
                AddVehicleVendor();
            }
            protected void uoButtonDelete_Click(object sender, EventArgs e)
            {
               // uoHiddenFieldToLoad.Value = "BothVehicle";  
                RemoveVehicleVendor();
            }
            //protected void uoButtonSave_Click(object sender, EventArgs e)
            //{
            //    SaveUserVehicleVendor();
            //}
            //protected void uoPagerVehicle_PreRender(object sender, EventArgs e)
            //{
            //    uoPagerVehicle.SetPageProperties(0, uoPagerVehicle.PageSize, false);
            //}
            protected void uoListViewVehicle_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
            {
                uoHiddenFieldLoadType.Value = "1";
                uoHiddenFieldToLoad.Value = "VehicleToAdd";                
            }
            protected void uoListViewVehicleSaved_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
            {
                uoHiddenFieldLoadType.Value = "1";
                uoHiddenFieldToLoad.Value = "VehicleAdded";

                //UserAccountBLL.DriverVendorGet(uoHiddenFieldUser.Value, "", false, "Vehicle",
                //        e.StartRowIndex, e.MaximumRows, 1, uoHiddenFieldSortBy.Value);
            }
           
            protected void uoObjectDataSourceVehicleToAdd_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
            {
                //if (uoHiddenFieldLoadType.Value == "0" || uoHiddenFieldToLoad.Value == "VehicleToAdd" ||
                //    uoHiddenFieldToLoad.Value == "BothVehicle")
                //{
                //    //Refresh the data
                //}
                //else
                //{
                //    e.Cancel = true;
                //}
            }
            protected void uoObjectDataSourceVehicleAdded_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
            {
                //if (uoHiddenFieldLoadType.Value == "0" || uoHiddenFieldToLoad.Value == "VehicleSaved" ||
                //    uoHiddenFieldToLoad.Value == "BothVehicle")
                //{
                //    //Refresh the data
                //}
                //else
                //{
                //    e.Cancel = true;
                //}
            }

            protected void uoObjectDataSourceVehicleToAdd_Selected(object sender, ObjectDataSourceStatusEventArgs e)
            {
                
            }
         
            protected void uoObjectDataSourceVehicleAdded_Selected(object sender, ObjectDataSourceStatusEventArgs e)
            {
                
            }
            protected void uoPagerVehicle_PreRender(object sender, EventArgs e)
            {
                //uoHiddenFieldLoadType.Value = "1";
            }

            protected void uoPagerVehicleSaved_PreRender(object sender, EventArgs e)
            {
                //uoHiddenFieldLoadType.Value = "1";
            }
            #endregion

            #region "Functions"

            //private void BindListViewVehicleNotInUser(bool IsNew, bool IsFromSearch)
            //{
            //    List<VehicleVendorDTO> list = GetVehicleVendorNotInUser(IsNew, IsFromSearch);
            //    uoListViewVehicle.DataSource = list;
            //    uoListViewVehicle.DataBind();
            //}
            //private void BindListViewSeaportInUser(bool IsNew)
            //{
            //    List<VehicleVendorDTO> list = GetVehicleVendorInUser(IsNew);
            //    uoListViewVehicleSaved.DataSource = list;
            //    uoListViewVehicleSaved.DataBind();
            //}
            /// <summary>
            /// Date Created:   03/Oct/2017
            /// Created By:     Josephine Monteza
            /// (description)   Get Vehicle Vendor not in user's access
            /// </summary>
            /// <param name="IsNew"></param>
            /// <returns></returns>
            //private List<VehicleVendorDTO> GetVehicleVendorNotInUser(bool IsNew, bool IsFromSearch)
            //{
            //    List<VehicleVendorDTO> list = new List<VehicleVendorDTO>();
            //    List<VehicleVendorDTO> listAdded = new List<VehicleVendorDTO>();   


            //    if (IsNew)
            //    {
            //        list = UserVendorBLL.GetUserVehicleVendor(uoHiddenFieldUser.Value, uoTextBoxVehicleSearch.Text.Trim(), true);
            //    }
            //    else
            //    {
            //        if (Session["User_VehicleVendorToAdd"] == null)
            //        {
            //            list = UserVendorBLL.GetUserVehicleVendor(uoHiddenFieldUser.Value, uoTextBoxVehicleSearch.Text.Trim(), true);
            //        }
            //        else
            //        {
            //            list = (List<VehicleVendorDTO>)Session["User_VehicleVendorToAdd"];
            //        }
            //    }

            //    if (IsFromSearch)
            //    {
            //        listAdded = GetVehicleVendorInUser(false);
            //    }
            //    else
            //    {
            //        listAdded = GetVehicleVendorInUser(IsNew);
            //    }
            //    list.RemoveAll(a => listAdded.Exists(b => a.VehicleName == b.VehicleName));
            //    list = list.OrderBy(a => a.VehicleName).ToList();

            //    Session["User_VehicleVendorToAdd"] = list;
            //    return list;
            //}
            ///// <summary>
            ///// Date Created:   03/Oct/2017
            ///// Created By:     Josephine Monteza
            ///// (description)   Get Vehicle Vendor in user's access
            ///// </summary>
            ///// <param name="IsNew"></param>
            ///// <returns></returns>
            //private List<VehicleVendorDTO> GetVehicleVendorInUser(bool IsNew)
            //{
            //    List<VehicleVendorDTO> list = new List<VehicleVendorDTO>();
               
            //    if (IsNew)
            //    {
            //        UserVendorBLL.DriverVendorGet(uoHiddenFieldUserName.Value, uoHiddenFieldUser.Value, "", false, "Vehicle", 0,
            //                 uoPagerVehicleSaved.TotalRowCount, 1, uoHiddenFieldSortBy.Value);
            //        list = (List<VehicleVendorDTO>)Session["User_VehicleVendorAdded"];
            //    }
            //    else
            //    {
            //        if (Session["User_VehicleVendorAdded"] == null)
            //        {
            //            UserVendorBLL.DriverVendorGet(uoHiddenFieldUserName.Value, uoHiddenFieldUser.Value, "", false, "Vehicle", 0, 
            //                 uoPagerVehicleSaved.TotalRowCount, 1, uoHiddenFieldSortBy.Value);
            //             list = (List<VehicleVendorDTO>)Session["User_VehicleVendorAdded"];
            //        }
            //        else
            //        {
            //            list = (List<VehicleVendorDTO>)Session["User_VehicleVendorAdded"];
            //        }
            //    }
            //    list = list.OrderBy(a => a.VehicleName).ToList();
            //    Session["User_VehicleVendorAdded"] = list;
            //    return list;
            //}
            /// <summary>
            /// <summary>
            /// Date Created:   03/Oct/2017
            /// Created By:     Josephine Gad
            /// (description)   Save vehicle Vendor
            /// </summary>
            private void AddVehicleVendor()
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldVehicleVendor;

                using (DataTable dtVendor = new DataTable())
                {
                    dtVendor.Columns.Add("UserName", typeof(string));
                    dtVendor.Columns.Add("VendorID", typeof(Int64));
                    DataRow dRow;

                    foreach (ListViewItem item in uoListViewVehicle.Items)
                    {
                        uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                        if (uoCheckBoxSelect.Checked == true)
                        {
                            uoHiddenFieldVehicleVendor = (HiddenField)item.FindControl("uoHiddenFieldVehicleVendor");

                            dRow = dtVendor.NewRow();
                            dRow["UserName"] = uoHiddenFieldUser.Value;
                            dRow["VendorID"] = GlobalCode.Field2Long(uoHiddenFieldVehicleVendor.Value);
                            dtVendor.Rows.Add(dRow);
                        }
                    }
                    if (dtVendor.Rows.Count > 0)
                    {
                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        UserVendorBLL.DriverVendorAddEdit(uoHiddenFieldUser.Value, true, dtVendor,
                            "Vehicle", "Add Driver Vehicle Vendor assignment", "AddVehicleVendor",
                            Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate),
                            uoHiddenFieldUserName.Value);
                    }
                }
                uoHiddenFieldLoadType.Value = "0";
                uoHiddenFieldToLoad.Value = "VehicleAdded";

                uoListViewVehicle.DataBind();
                uoListViewVehicleSaved.DataBind();
            }
            /// <summary>
            /// Date Created:   03/Oct/2017
            /// Created By:     Josephine Monteza
            /// (description)   Remove vehicle Vendor from right to left
            /// </summary>
            private void RemoveVehicleVendor()
            {
                CheckBox uoCheckBoxSelect;
                HiddenField uoHiddenFieldVehicleVendor;

                using (DataTable dtVendor = new DataTable())
                {
                    dtVendor.Columns.Add("UserName", typeof(string));
                    dtVendor.Columns.Add("VendorID", typeof(Int64));
                    DataRow dRow;

                    foreach (ListViewItem item in uoListViewVehicleSaved.Items)
                    {
                        uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                        if (uoCheckBoxSelect.Checked == true)
                        {
                            uoHiddenFieldVehicleVendor = (HiddenField)item.FindControl("uoHiddenFieldVehicleVendor");

                            dRow = dtVendor.NewRow();
                            dRow["UserName"] = uoHiddenFieldUser.Value;
                            dRow["VendorID"] = GlobalCode.Field2Long(uoHiddenFieldVehicleVendor.Value);
                            dtVendor.Rows.Add(dRow);
                        }
                    }
                    if (dtVendor.Rows.Count > 0)
                    {
                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        UserVendorBLL.DriverVendorAddEdit(uoHiddenFieldUser.Value, false, dtVendor,
                            "Vehicle", "Remove Driver Vehicle Vendor assignment", "RemoveVehicleVendor",
                            Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate),
                            uoHiddenFieldUserName.Value);
                    }
                }
                uoHiddenFieldLoadType.Value = "0";
                uoHiddenFieldToLoad.Value = "VehicleAdded";

                uoListViewVehicle.DataBind();
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
        /// Author:         Josephine Monteza
        /// Date Created:   03/Oct/2017
        /// Descrption:     Get Vendor list of Driver
        /// =============================================================     
        /// </summary>
            public List<VehicleVendorDTO> GetDriverVendorList(string sLoginUser, string sUserID, string sVendorToFind, bool bIsToBeAdded, string sVendorType,
               int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy, string sToLoad)
            {
                string sToFind = sVendorToFind == null ? "" : sVendorToFind;

                if (iLoadType == 0 || sToLoad == "VehicleToAdd")
                {
                    UserVendorBLL.DriverVendorGet(sLoginUser, sUserID, sToFind, bIsToBeAdded, sVendorType,
                         iStartRow, iMaxRow, iLoadType, sOrderBy);
                }


                List<VehicleVendorDTO> listVehicleVendorToAdd = new List<VehicleVendorDTO>();

                if (sVendorType == "Vehicle")
                {
                    if (bIsToBeAdded)
                    {

                        if (Session["User_VehicleVendorToAdd"] != null)
                        {
                            listVehicleVendorToAdd = (List<VehicleVendorDTO>)Session["User_VehicleVendorToAdd"];
                        }
                    }
                   
                }
                return listVehicleVendorToAdd;
            }
            /// <summary>
            /// Author:         Josephine Monteza
            /// Date Created:   03/Oct/2017
            /// Descrption:     Get Vendor list of Driver
            /// =============================================================     
            /// </summary>
            public List<VehicleVendorDTO> GetDriverVehicleVendorAddedList(string sLoginUser, string sUserID, string sVendorToFind, bool bIsToBeAdded, string sVendorType,
               int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy, string sToLoad)
            {
                List<VehicleVendorDTO> listVehicleVendorAdded = new List<VehicleVendorDTO>();

                string sToFind = "";

                if (iLoadType == 1 || sToLoad == "VehicleAdded")
                {
                    UserVendorBLL.DriverVendorGet(sLoginUser, sUserID, sToFind, bIsToBeAdded, sVendorType,
                         iStartRow, iMaxRow, iLoadType, sOrderBy);
                }

                if (Session["User_VehicleVendorAdded"] != null)
                {
                    listVehicleVendorAdded = (List<VehicleVendorDTO>)Session["User_VehicleVendorAdded"];
                }
                return listVehicleVendorAdded;
            }
            /// <summary>
            /// Author:         Josephine Monteza
            /// Date Created:   03/Oct/2017
            /// Descrption:     Get Vendor Count of Driver
            /// =============================================================
            /// </summary>
            /// <param name="sUserID"></param>
            /// <param name="sVendorToFind"></param>
            /// <param name="bIsToBeAdded"></param>
            /// <param name="sVendorType"></param>
            /// <param name="iLoadType"></param>
            /// <param name="sOrderBy"></param>
            /// <returns></returns>
            public int GetDriverVendorCount(string sLoginUser, string sUserID, string sVendorToFind, bool bIsToBeAdded, string sVendorType,
               Int16 iLoadType, string sOrderBy, string sToLoad)
            {
                int iCount = 0;
                if (sVendorType == "Vehicle")
                {
                    if (bIsToBeAdded)
                    {
                        if (Session["User_VehicleVendorCountToAdd"] != null)
                        {
                            iCount = (int)Session["User_VehicleVendorCountToAdd"];
                        }
                    }
                    else
                    {
                        if (Session["User_VehicleVendorCountAdded"] != null)
                        {
                            iCount = (int)Session["User_VehicleVendorCountAdded"];
                        }
                    }
                }
                else if (sVendorType == "Hotel")
                {
                    if (bIsToBeAdded)
                    {
                        if (Session["User_HotelVendorCountToAdd"] != null)
                        {
                            iCount = (int)Session["User_HotelVendorCountToAdd"];
                        }
                    }
                    else
                    {
                        if (Session["User_HotelVendorCountAdded"] != null)
                        {
                            iCount = (int)Session["User_HotelVendorCountAdded"];
                        }
                    }
                }
                else if (sVendorType == "ServiceProvider")
                {
                    if (bIsToBeAdded)
                    {
                        if (Session["User_ServiceProviderCountToAdd"] != null)
                        {
                            iCount = (int)Session["User_ServiceProviderCountToAdd"];
                        }
                    }
                    else
                    {
                        if (Session["User_ServiceProviderCountAdded"] != null)
                        {
                            iCount = (int)Session["User_ServiceProviderCountAdded"];
                        }
                    }
                }
                return iCount;
            }
            #endregion

           
           
        }
    }

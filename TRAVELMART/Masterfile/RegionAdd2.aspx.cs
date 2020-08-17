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
using System.Reflection;

namespace TRAVELMART
{
    public partial class RegionAdd2 : System.Web.UI.Page
    {
        #region EVENTS

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// </summary>
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (GlobalCode.Field2Int(Request.QueryString["vmId"]) == 0)
                {
                    uoHiddenFieldNew.Value = "true";
                }
                else
                {
                    uoHiddenFieldRegion.Value = GlobalCode.Field2Int(Request.QueryString["vmId"]).ToString();
                    uoHiddenFieldRegionName.Value = GlobalCode.Field2String(Request.QueryString["vmName"]);
                }

                if (!GlobalCode.Field2Bool(uoHiddenFieldNew.Value))
                {
                    uoTextBoxRegionName.Text = uoHiddenFieldRegionName.Value;
                }

                LoadRegionPage();
            }
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
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
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
        /// Description: Save Region
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// -------------------------------------------
        /// Date Modified:   01/06/2012
        /// Modified By:     Josephine Gad
        /// (description)    Use Session["RegionSeaportList"] instead of RegionClass.RegionSeaportList
        ///                  Dispose DataTable dt
        /// -------------------------------------------
        /// Date Modified:   19/Feb/2013
        /// Modified By:     Josephine Gad
        /// (description)    Add Audit Trail data to save
        /// -------------------------------------------
        /// </summary>
       
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                List<RegionSeaport> list = new List<RegionSeaport>();
                if (Session["RegionSeaportList"] != null)
                {
                    list = (List<RegionSeaport>)Session["RegionSeaportList"];
                }
                string strDecription = "";
                string strFunction = "uoButtonSave_Click";

                int rID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                string rName = GlobalCode.Field2String(uoHiddenFieldRegionName.Value);

                var RSList = (from a in list//RegionClass.RegionSeaportList
                              select new
                              {
                                  RegionSeaportID = a.RegionSeaportID,
                                  RegionID = a.RegionID,
                                  SeaportID = a.SeaportID,
                                  UserName = GlobalCode.Field2String(Session["UserName"]),
                              }).ToList();

                dt = getDataTable(RSList);

                if (rID == 0)
                {
                    strDecription = "Region Added";
                }
                else
                {
                    strDecription = "Region Edited";
                }

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                MasterfileBLL.SaveRegionSeaport(dt, GlobalCode.Field2String(Session["UserName"]), rID, uoTextBoxRegionName.Text,
                    strDecription, strFunction,
                    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);

                if (rID == 0)
                {
                    OpenParentPage("Region Successfully added.");
                }
                else
                {
                    OpenParentPage("Region Successfully updated.");
                }               
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

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            BindSeaportToAdd(true);
            uoTextBoxSeaport.Text = "";
        }

        protected void uoButtonView2_Click(object sender, EventArgs e)
        {
            FilterRegionSeaport(false);
            uoTextBoxSeaport2.Text = "";
        }

        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(uoSeaportList);
        }
        protected void uoButtonDelete_Click(object sender, EventArgs e)
        {
            RemoveSelectedItems(uoSeaportAddedList);
        }
        protected void uoDropDownListContinent_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountry();
        }
        protected void uoDropDownListContinent2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountry2();
        }

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSeaportToAdd(true);
        }

        protected void uoButtonRemoveFilter_Click(object sender, EventArgs e)
        {
            FilterRegionSeaport(true);
            uoDropDownListContinent2.SelectedIndex = 0;
            uoDropDownListCountry2.SelectedIndex = 0;
            uoTextBoxSeaport2.Text = "";
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Show pop up message            
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
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// </summary>
        private void OpenParentPage(string s)
        {
            string sScript = "<script language='Javascript'>";
            sScript += " alert('" + s + "');";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupRegion\").val(\"1\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";
          
            sScript += "</script>";
            
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(),"scr", sScript,false);
        }


        
        /// <summary>
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Bind uoGridViewSeaport
        /// -----------------------------------
        /// Date Modified:  07/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change uoGridViewSeaport to uoSeaportAddedList  
        /// -------------------------------------------
        /// Date Modified:   01/06/2012
        /// Modified By:     Josephine Gad
        /// (description)    Use Session["RegionSeaportList"] instead of RegionClass.RegionSeaportList
        /// -------------------------------------------
        ///                 
        /// </summary>
        private void BindRegionSeaport(bool IsNew)
        {
            try
            {
                List<RegionSeaport> list = new List<RegionSeaport>();
                if (Session["RegionSeaportList"] != null)
                {
                    list = (List<RegionSeaport>)Session["RegionSeaportList"];
                }

                uoSeaportAddedList.DataSource = list;//RegionClass.RegionSeaportList;
                uoSeaportAddedList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
       /// <summary>
       /// Date Created:    04/05/2012
       /// Created By:      Josephine Gad
       /// (description)    Get list Region not yet added to this Region
       /// --------------------------------------------
       /// </summary>
       /// <param name="IsNewLoad"></param>
        private void BindSeaportToAdd(bool IsNewLoad)
        {
            List<RegionSeaportNotExists> list = null;
            List<RegionSeaport> listRegionSeaport = new List<RegionSeaport>();
            if (Session["RegionSeaportList"] != null)
            {
                listRegionSeaport = (List<RegionSeaport>)Session["RegionSeaportList"];
            }
            try
            {
                int RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                int CountryID = GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue);
                int ContinentID = GlobalCode.Field2Int(uoDropDownListContinent.SelectedValue);
                if (IsNewLoad || Session["RegionSeaportToAdd"] == null)
                {
                    list = RegionBLL.GetSeaportNotExistsInRegion(RegionID, ContinentID, CountryID, uoTextBoxSeaport.Text);
                    var regionAdded = (from a in listRegionSeaport//RegionClass.RegionSeaportList
                                       where a.CountryID == CountryID
                                       select new
                                       {
                                           RegionSeaportID = a.RegionSeaportID,
                                           RegionID = a.RegionID,
                                           CountryID = a.CountryID,
                                           SeaportID = a.SeaportID,
                                           SeaportName = a.SeaportName,
                                       }).ToList();
                   // RegionSeaportNotExists.RegionSeaportToAdd = list;
                    //RegionSeaportNotExists.RegionSeaportToAdd.RemoveAll(a => regionAdded.Exists(b => a.SeaportID == b.SeaportID));
                    list.RemoveAll(a => regionAdded.Exists(b => a.SeaportID == b.SeaportID));
                    Session["RegionSeaportToAdd"] = list;
                }
                else
                {
                    list = (List<RegionSeaportNotExists>)Session["RegionSeaportToAdd"];
                }
                
                uoSeaportList.DataSource = list;//RegionSeaportNotExists.RegionSeaportToAdd;
                uoSeaportList.DataBind();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }
        }

        /// <summary>
        /// MOdified By: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: bind continent to list
        /// -------------------------------------------
        /// Date Modified:   01/06/2012
        /// Modified By:     Josephine Gad
        /// (description)    Use Session["ContinentList"] instead of RegionClass.ContinentList
        /// -------------------------------------------
        /// </summary>
        private void BindContinent()
        {
            List<Continent> list = new List<Continent>();
            if (Session["ContinentList"] != null)
            {
                list = (List<Continent>)Session["ContinentList"];
            }
                        
            uoDropDownListContinent.Items.Clear();
            uoDropDownListContinent2.Items.Clear();

            if (list.Count > 0)
            {
                uoDropDownListContinent.DataSource = list;
                uoDropDownListContinent.DataTextField = "ContinentName";
                uoDropDownListContinent.DataValueField = "ContinentID";
                uoDropDownListContinent.DataBind();
                //2nd drop down
                uoDropDownListContinent2.DataSource = list;
                uoDropDownListContinent2.DataTextField = "ContinentName";
                uoDropDownListContinent2.DataValueField = "ContinentID";
                uoDropDownListContinent2.DataBind();

            }
            uoDropDownListContinent.Items.Insert(0, new ListItem("--Select Continent--", "0"));            
            uoDropDownListContinent2.Items.Insert(0, new ListItem("--Select Continent--", "0"));
        }

        private void BindCountry()
        {
            List<Country> list = new List<Country>();
            list = CountryBLL.GetCountryByContinent(uoDropDownListContinent.SelectedValue);
            uoDropDownListCountry.Items.Clear();
            uoDropDownListCountry.DataSource = list;
            uoDropDownListCountry.DataTextField = "CountryName";
            uoDropDownListCountry.DataValueField = "CountryID";
            uoDropDownListCountry.DataBind();


            uoDropDownListCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
        }

        private void BindCountry2()
        {
            List<Country> list = new List<Country>();
            list = CountryBLL.GetCountryByContinent(uoDropDownListContinent2.SelectedValue);
            uoDropDownListCountry2.Items.Clear();
            uoDropDownListCountry2.DataSource = list;
            uoDropDownListCountry2.DataTextField = "CountryName";
            uoDropDownListCountry2.DataValueField = "CountryID";
            uoDropDownListCountry2.DataBind();


            uoDropDownListCountry2.Items.Insert(0, new ListItem("--Select Country--", "0"));
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: load region seaport page
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  01/06/2012
        /// Description:    Add  Session["ContinentList"] and Session["RegionSeaportList"]
        /// </summary>
        protected void LoadRegionPage()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (uoHiddenFieldRegion.Value != "0")
            {
                strLogDescription = "Edit linkbutton for region editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for region editor clicked.";
            }

            strFunction = "LoadRegionPage";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            RegionBLL.LoadRegionPage(GlobalCode.Field2Int(uoHiddenFieldRegion.Value), strLogDescription, strFunction, 
                Path.GetFileName(Request.UrlReferrer.AbsolutePath),CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now,
                GlobalCode.Field2String(Session["UserName"]));


            Session["ContinentList"] = RegionClass.ContinentList;
            Session["RegionSeaportList"] = RegionClass.RegionSeaportList;

            BindContinent();
            BindRegionSeaport(true);
            uoSeaportList.DataSource = null;
            uoSeaportList.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: Add seaport to region
        /// -------------------------------------------
        /// Date Modified:   01/06/2012
        /// Modified By:     Josephine Gad
        /// (description)    Use Session["RegionSeaportList"] instead of RegionClass.RegionSeaportList;
        ///                  Use Session["RegionSeaportToAdd"] instead of RegionSeaportNotExists.RegionSeaportToAdd
        /// -------------------------------------------
        /// </summary>
        /// <param name="source"></param>
        private void MoveSelectedItems(ListView source)
        {
            CheckBox uoCheckBoxSelect;
            HiddenField uoHiddenFieldSeaport;

            List<RegionSeaportNotExists> RListToAdd = new List<RegionSeaportNotExists>();// RegionSeaportNotExists.RegionSeaportToAdd;
            if (Session["RegionSeaportToAdd"] != null)
            {
                RListToAdd = (List<RegionSeaportNotExists>)Session["RegionSeaportToAdd"];
            }
            
            List<RegionSeaport> rList = new List<RegionSeaport>();                        
            if (Session["RegionSeaportList"] != null)            
            {
                rList = (List<RegionSeaport>)Session["RegionSeaportList"];//RegionClass.RegionSeaportList;
            }

            foreach (ListViewItem item in source.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");

                if (uoCheckBoxSelect.Checked)
                {
                    uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");
                    var listToAdd = (from a in RListToAdd
                                     where a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value)
                                     select new
                                     {
                                         RegionSeaportID = a.RegionSeaportID,
                                         SeaportID = a.SeaportID,
                                         SeaportName = a.SeaportName,
                                         CountryID = a.CountryID,
                                     }).ToList();
                    //RegionSeaportNotExists.RegionSeaportToAdd.RemoveAll(a => a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value));
                    RListToAdd.RemoveAll(a => a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value));
                    RegionSeaport seaportItem = new RegionSeaport();
                    seaportItem.RegionSeaportID = listToAdd[0].RegionSeaportID;
                    seaportItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    seaportItem.SeaportID = listToAdd[0].SeaportID;
                    seaportItem.SeaportName = listToAdd[0].SeaportName;
                    seaportItem.CountryID = listToAdd[0].CountryID;
                    rList.Insert(rList.Count, seaportItem);
                }
            }
            //RegionSeaport.RegionSeaportList = null;
            //RegionClass.RegionSeaportList = rList.OrderBy(a => a.SeaportName).ToList();
            Session["RegionSeaportList"] = rList.OrderBy(a => a.SeaportName).ToList();
            BindRegionSeaport(true);
            BindSeaportToAdd(false);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: Remove seaport from region
        /// ---------------------------------------------------
        /// Date Modified:   01/06/2012
        /// Modified By:     Josephine Gad
        /// (description)    Use Session["RegionSeaportList"] instead of RegionClass.RegionSeaportList;
        ///                  Use Session["RegionSeaportToAdd"] instead of RegionSeaportNotExists.RegionSeaportToAdd
        /// -------------------------------------------
        /// </summary>
        /// <param name="source"></param>
        private void RemoveSelectedItems(ListView source)
        {
            CheckBox uoCheckBoxSelect;
             HiddenField uoHiddenFieldSeaport;
            HiddenField uoHiddenFieldRegionSeaport;

            List<RegionSeaport> rList = new List<RegionSeaport>();//RegionClass.RegionSeaportList; 
            if (Session["RegionSeaportList"] != null)
            {
                rList = (List<RegionSeaport>)Session["RegionSeaportList"];
            }                     

            List<RegionSeaportNotExists> RListToAdd = new List<RegionSeaportNotExists>();// RegionSeaportNotExists.RegionSeaportToAdd;
            if (Session["RegionSeaportToAdd"] != null)
            {
                RListToAdd = (List<RegionSeaportNotExists>)Session["RegionSeaportToAdd"];
            }            

            //if (RegionSeaportNotExists.RegionSeaportToAdd == null)
            //{
            //    RListToAdd = new List<RegionSeaportNotExists>();
            //}
            //else
            //{
            //    RListToAdd = RegionSeaportNotExists.RegionSeaportToAdd;
            //}

            foreach (ListViewItem item in source.Items)
            {
                uoCheckBoxSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");

                if (uoCheckBoxSelect.Checked)
                {
                    uoHiddenFieldRegionSeaport = (HiddenField)item.FindControl("uoHiddenFieldRegionSeaport");
                    uoHiddenFieldSeaport = (HiddenField)item.FindControl("uoHiddenFieldSeaport");
                    var listToRemove = (from a in rList
                                     where a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value)
                                     select new
                                     {
                                         RegionSeaportID = a.RegionSeaportID,
                                         SeaportID = a.SeaportID,
                                         SeaportName = a.SeaportName,
                                         CountryID = a.CountryID,
                                         RegionID = a.RegionID,                                         
                                     }).ToList();
                    //RegionClass.RegionSeaportList.RemoveAll(a => a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value));
                    rList.RemoveAll(a => a.SeaportID == GlobalCode.Field2Int(uoHiddenFieldSeaport.Value));

                    RegionSeaportNotExists seaportItem = new RegionSeaportNotExists();
                    seaportItem.RegionSeaportID = listToRemove[0].RegionSeaportID;
                    seaportItem.RegionID = GlobalCode.Field2Int(uoHiddenFieldRegion.Value);
                    seaportItem.SeaportID = listToRemove[0].SeaportID;
                    seaportItem.SeaportName = listToRemove[0].SeaportName;
                    seaportItem.CountryID = listToRemove[0].CountryID;
                    if (seaportItem.CountryID == GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue))
                    {
                        RListToAdd.Insert(RListToAdd.Count, seaportItem);
                    }
                }
            }
            //RegionSeaport.RegionSeaportList = null;
            //RegionSeaportNotExists.RegionSeaportToAdd = RListToAdd.OrderBy(a => a.SeaportName).ToList();
            Session["RegionSeaportToAdd"] = RListToAdd.OrderBy(a => a.SeaportName).ToList();
            BindRegionSeaport(true);
            BindSeaportToAdd(false);
        }

        /// <summary>
        /// Date Created: 09/05/2012
        /// Created By:   Charlene Remotigue
        /// (description) Filter region seaport list
        /// -------------------------------------------
        /// Date Modified:   01/06/2012
        /// Modified By:     Josephine Gad
        /// (description)    Use Session["RegionSeaportList"] instead of RegionClass.RegionSeaportList;
        ///                  Use Session["RegionSeaportToAdd"] instead of RegionSeaportNotExists.RegionSeaportToAdd
        /// -------------------------------------------
        /// </summary>
        /// <param name="viewAll"></param>
        private void FilterRegionSeaport(bool viewAll)
        {
            try
            {
                List<RegionSeaport> rList = new List<RegionSeaport>();//RegionClass.RegionSeaportList; 
                if (Session["RegionSeaportList"] != null)
                {
                    rList = (List<RegionSeaport>)Session["RegionSeaportList"];
                }

                List<RegionSeaportNotExists> RListToAdd = new List<RegionSeaportNotExists>();// RegionSeaportNotExists.RegionSeaportToAdd;
                if (Session["RegionSeaportToAdd"] != null)
                {
                    RListToAdd = (List<RegionSeaportNotExists>)Session["RegionSeaportToAdd"];
                }    

                if (viewAll)
                {
                    uoSeaportAddedList.DataSource = rList;//RegionClass.RegionSeaportList;
                    uoSeaportAddedList.DataBind();
                }
                else
                {
                    var filteredList = (from a in rList//RegionClass.RegionSeaportList
                                        where a.SeaportID == -1
                                        select new
                                        {
                                            RegionSeaportID = a.RegionSeaportID,
                                            RegionID = a.RegionID,
                                            CountryID = a.CountryID,
                                            SeaportID = a.SeaportID,
                                            SeaportName = a.SeaportName,
                                        }).ToList();                    

                    if (GlobalCode.Field2Int(uoDropDownListCountry2.SelectedValue) == 0 && uoTextBoxSeaport2.Text.Trim() == "")
                    {
                        filteredList = (from a in rList//RegionClass.RegionSeaportList
                                        select new
                                        {
                                            RegionSeaportID = a.RegionSeaportID,
                                            RegionID = a.RegionID,
                                            CountryID = a.CountryID,
                                            SeaportID = a.SeaportID,
                                            SeaportName = a.SeaportName,
                                        }).ToList();
                    }
                    else
                    {
                        if (GlobalCode.Field2Int(uoDropDownListCountry2.SelectedValue) != 0)
                        {
                            filteredList = (from a in filteredList
                                            where a.CountryID == GlobalCode.Field2Int(uoDropDownListCountry2.SelectedValue)
                                            select new
                                            {
                                                RegionSeaportID = a.RegionSeaportID,
                                                RegionID = a.RegionID,
                                                CountryID = a.CountryID,
                                                SeaportID = a.SeaportID,
                                                SeaportName = a.SeaportName,
                                            }).ToList();
                        }

                        if (uoTextBoxSeaport2.Text.Trim() != "")
                        {
                            filteredList = (from a in filteredList
                                            where a.SeaportName == uoTextBoxSeaport2.Text
                                            select new
                                            {
                                                RegionSeaportID = a.RegionSeaportID,
                                                RegionID = a.RegionID,
                                                CountryID = a.CountryID,
                                                SeaportID = a.SeaportID,
                                                SeaportName = a.SeaportName,
                                            }).ToList();
                        }
                    }
                    uoSeaportAddedList.DataSource = filteredList;
                    uoSeaportAddedList.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion        
       
    }
}

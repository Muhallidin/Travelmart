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

namespace TRAVELMART.Maintenance
{
    public partial class SafeguardMaintenance : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["vmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                SafeguardVendorLogAuditTrail();

                uoHiddenFieldSafeguardVendorIdInt.Value = Request.QueryString["vmId"];

                //vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));                
                vendorInfoLoad(0);
                vendorCountryLoad();

                if (uoHiddenFieldSafeguardVendorIdInt.Value == "0")
                {
                    BindAirportSeaport(0, 0);
                }
                else
                {
                    BindAirportSeaport(Convert.ToInt32(uoHiddenFieldSafeguardVendorIdInt.Value), 0);
                }
            }
        }

        /// <summary>
        /// Date Created: 27/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Save new vendor        
        /// -------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change endorMaintenanceBLL.vendorMaintenanceInsert to endorMaintenanceBLL.VehicleVendorsSave
        ///                 Add Audit Trail in the same script
        ///                 Remove the separate Audit Trail
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                string strLogDescription;
                string strFunction = "uoButtonSave_Click";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                if (GlobalCode.Field2Int(uoHiddenFieldSafeguardVendorIdInt.Value) == 0)
                {
                    strLogDescription = "Safeguard vendor added.";
                }
                else
                {
                    strLogDescription = "Safeguard vendor updated.";
                }

                string vendorPrimaryId = GlobalCode.Field2Int(uoHiddenFieldSafeguardVendorIdInt.Value).ToString();//(Session["vendorPrimaryId"].ToString() == null) ? "0" : Session["vendorPrimaryId"].ToString();

                dt = new DataTable();
                dt.Columns.Add("VehicleTypeID", typeof(int));

                //DataRow r;
                //HiddenField hiddenFieldVehicleTypeID;

                //foreach (ListViewItem item in uoListViewVehicleType.Items)
                //{
                //    r = dt.NewRow();
                //    hiddenFieldVehicleTypeID = (HiddenField)item.FindControl("uoHiddenFieldVehicleTypeID");
                //    r[0] = GlobalCode.Field2Int(hiddenFieldVehicleTypeID.Value);
                //    dt.Rows.Add(r);
                //}

                DataTable dtSeaport = null;
                List<Seaport> listSeaport = GetSeaportList();

                var lSeaport = (from a in listSeaport
                                select new
                                {
                                    SeaportID = a.SeaportID
                                }).ToList();
                dtSeaport = getDataTable(lSeaport);

                SafeguardBLL.SafeguardVendorsSave(GlobalCode.Field2Int(vendorPrimaryId),
                    uoTextBoxVendorName.Text, GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
                    GlobalCode.Field2Int(uoDropDownListCity.SelectedValue), uoTextBoxContactNo.Text,
                    uoTextBoxFaxNo.Text, uoTextBoxContactPerson.Text, uoTextBoxVendorAddress.Text,
                    uoTextBoxEmailCc.Text, uoTextBoxEmailTo.Text, uoTextBoxWebsite.Text,
                    GlobalCode.Field2String(Session["UserName"]), strLogDescription, strFunction,
                    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, dt, dtSeaport);

                OpenParentPage();
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
        /// Date Created:   15/Aug/2013
        /// Description:    convert list to datatable
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
        /// Author:         Josephine Gad
        /// Date Created:   15/Aug/2013
        /// Description:    get item type
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

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 1)
            {
                vendorCityLoad(1, uoDropDownListCountry.SelectedValue);
                //ChangeToUpperCase(uoDropDownListCity);
            }
        }

        protected void uoButtonFilterCity_Click(object sender, EventArgs e)
        {
            vendorCityLoad(1, uoDropDownListCountry.SelectedValue);
        }
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Delete vehicle type
        /// -------------------------------------------
        //protected void uoListViewVehicleType_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;

        //    int index = GlobalCode.Field2Int(e.CommandArgument);
        //    if (e.CommandName == "Delete")
        //    {
        //        //MaintenanceViewBLL.DeleteVehicleVendor(index, GlobalCode.Field2String(Session["UserName"]));

        //        ////Insert log audit trail (Gabriel Oquialda - 17/11/2011)
        //        //strLogDescription = "Vehicle vendor branch deleted. (flagged as inactive)";
        //        //strFunction = "uoVehicleVendorList_ItemCommand";

        //        //DateTime currentDate = CommonFunctions.GetCurrentDateTime();

        //        //BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //        //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

        //        BindVendorTypeList(1);
        //    }          
        //}
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine  gad
        /// (description)   Add Type in ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void uoButtonVehicleTypeAdd_Click(object sender, EventArgs e)
        //{
        //    List<VehicleType> listType = new List<VehicleType>();
        //    List<VendorVehicleType> listVendor = new List<VendorVehicleType>();

        //    listType = GetVehicleTypeList(0);
        //    listVendor = GetVehicleVendorTypeList(0);

        //    listType.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue)));
        //    listType = listType.OrderBy(a => a.VehicleTypeName).ToList();

        //    VendorVehicleType item = new VendorVehicleType();
        //    item.VehicleType = uoDropDownListVehicleType.SelectedItem.Text;
        //    item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue);
        //    item.VehicleVendorTypeID = GlobalCode.Field2Int(uoHiddenFieldSafeguardVendorIdInt.Value);

        //    listVendor.Add(item);
        //    listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

        //    Session["VendorVehicleType"] = listVendor;
        //    Session["VehicleType"] = listType;

        //    BindVendorTypeList(0);
        //    BindVehicleTypeList(0);
        //}
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine  gad
        /// (description)   Remove Type in ListView
        /// </summary>
        //protected void uoListViewVehicleType_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        //{
        //    HiddenField uoHiddenFieldVehicleTypeID = (HiddenField)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoHiddenFieldVehicleTypeID");
        //    Label uoLabelVehicleTypeName = (Label)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoLabelVehicleTypeName");
        //    //BindVendorTypeList(1);

        //    List<VehicleType> listType = new List<VehicleType>();
        //    List<VendorVehicleType> listVendor = new List<VendorVehicleType>();

        //    listType = GetVehicleTypeList(0);
        //    listVendor = GetVehicleVendorTypeList(0);

        //    listVendor.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value)));
        //    listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

        //    VehicleType item = new VehicleType();
        //    item.VehicleTypeID = GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value);
        //    item.VehicleTypeName = uoLabelVehicleTypeName.Text;
        //    listType.Add(item);

        //    Session["VendorVehicleType"] = listVendor;
        //    Session["VehicleType"] = listType;

        //    BindVendorTypeList(0);
        //    BindVehicleTypeList(0);
        //}
        #endregion

        #region Functions

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) format data to uppercase        
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        /// <summary>
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (decription) Loads vendor information     
        /// -----------------------------------------------
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (decription) Loads vendor information     
        /// -----------------------------------------------
        /// Date Modifed:   06/Aug/2013
        /// Created By:     Josephine Gad
        /// (decription)    Load Vendor using VendorMaintenanceBLL.VehicleVendorsGetByID
        ///                 Use Sessions instead of DataTable        
        /// </summary>
        private void vendorInfoLoad(Int16 iLoadType)
        {
            if (Request.QueryString["vmId"].ToString() != "0")
            {

                SafeguardBLL.SafeguardVendorsGetByID(GlobalCode.Field2Int(uoHiddenFieldSafeguardVendorIdInt.Value), iLoadType);
                List<VendorSafeguardDetails> listSafeguardDetails = new List<VendorSafeguardDetails>();

                if (Session["SafeguardVendorDetails"] != null)
                {
                    listSafeguardDetails = (List<VendorSafeguardDetails>)Session["SafeguardVendorDetails"];
                    if (listSafeguardDetails.Count > 0)
                    {
                        uoTextBoxCity.Text = GlobalCode.Field2String(Session["SafeguardVendorCityFilter"]);
                        uoTextBoxVendorName.Text = listSafeguardDetails[0].VendorName;
                        uoTextBoxVendorAddress.Text = listSafeguardDetails[0].Address;
                        uoTextBoxWebsite.Text = listSafeguardDetails[0].Website;

                        uoTextBoxEmailTo.Text = listSafeguardDetails[0].EmailTo;
                        uoTextBoxEmailCc.Text = listSafeguardDetails[0].EmailCC;

                        vendorCountryLoad();
                        if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(listSafeguardDetails[0].CountryID)) != null)
                        {
                            uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(listSafeguardDetails[0].CountryID);
                        }
                        vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                        if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(listSafeguardDetails[0].CityID)) != null)
                        {
                            uoDropDownListCity.SelectedValue = GlobalCode.Field2String(listSafeguardDetails[0].CityID);
                        }
                        uoTextBoxContactNo.Text = listSafeguardDetails[0].ContactNo;
                        uoTextBoxContactPerson.Text = listSafeguardDetails[0].ContactPerson;
                        uoTextBoxFaxNo.Text = listSafeguardDetails[0].FaxNo;
                        //BindVendorTypeList(0);
                        //BindVehicleTypeList(0);
                    }
                }
                else
                {
                    uoTextBoxCity.Text = "";
                    uoTextBoxVendorName.Text = "";
                    uoTextBoxVendorAddress.Text = "";
                    uoTextBoxWebsite.Text = "";

                    uoTextBoxEmailTo.Text = "";
                    uoTextBoxEmailCc.Text = "";

                    vendorCountryLoad();
                    vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                    uoTextBoxContactNo.Text = "";
                    uoTextBoxContactPerson.Text = "";
                    uoTextBoxFaxNo.Text = "";

                    //uoListViewVehicleType.DataSource = null;
                    //uoListViewVehicleType.DataBind();
                    //BindVehicleTypeList(1);
                }
            }
            else
            {
                uoTextBoxCity.Text = "";
                uoTextBoxVendorName.Text = "";
                uoTextBoxVendorAddress.Text = "";
                uoTextBoxWebsite.Text = "";

                uoTextBoxEmailTo.Text = "";
                uoTextBoxEmailCc.Text = "";

                vendorCountryLoad();
                vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                uoTextBoxContactNo.Text = "";
                uoTextBoxContactPerson.Text = "";
                uoTextBoxFaxNo.Text = "";
                //uoListViewVehicleType.DataSource = null;
                //uoListViewVehicleType.DataBind();
                //BindVehicleTypeList(1);
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist         
        /// ----------------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change CityListbyCountry to CityBLL.GetCityByCountry
        ///                 Add Session, Add default value
        /// </summary>
        private void vendorCityLoad(Int16 iLoad, string vendorCountryId)
        {
            DataTable dt = null;
            try
            {
                if (iLoad == 0)
                {
                    List<CityList> listCity = new List<CityList>();

                    uoDropDownListCity.Items.Clear();
                    if (Session["CityList"] != null)
                    {
                        listCity = ((List<CityList>)Session["CityList"]);
                        uoDropDownListCity.DataSource = listCity;
                        uoDropDownListCity.DataTextField = "CityName";
                        uoDropDownListCity.DataValueField = "CityId";
                    }
                    uoDropDownListCity.Items.Insert(0, (new ListItem("--Select City--", "0")));
                    uoDropDownListCity.DataBind();
                }
                else
                {

                    //dt = CityBLL.CityListbyCountry(vendorCountryId);
                    dt = CityBLL.GetCityByCountry(vendorCountryId, uoTextBoxCity.Text.Trim(), uoTextBoxCity.Text);
                    uoDropDownListCity.Items.Clear();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select a City--", ""));
                    if (dt.Rows.Count > 0)
                    {
                        uoDropDownListCity.DataSource = dt;
                        uoDropDownListCity.DataTextField = "colCityNameVarchar";
                        uoDropDownListCity.DataValueField = "colCityIDInt";
                        uoDropDownListCity.DataBind();

                        if (dt.Rows.Count == 1)
                        {
                            if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(dt.Rows[0]["colCityIDInt"])) != null)
                            {
                                uoDropDownListCity.SelectedValue = GlobalCode.Field2String(dt.Rows[0]["colCityIDInt"]);
                            }
                        }
                    }
                    else
                    {
                        uoDropDownListCity.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist            
        /// -----------------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change Datatable to Session
        /// -----------------------------------------------------
        /// </summary>
        private void vendorCountryLoad()
        {
            List<CountryList> listCountry = new List<CountryList>();

            uoDropDownListCountry.Items.Clear();
            if (Session["CountryList"] != null)
            {
                listCountry = ((List<CountryList>)Session["CountryList"]);
                uoDropDownListCountry.DataSource = listCountry;
                uoDropDownListCountry.DataTextField = "CountryName";
                uoDropDownListCountry.DataValueField = "CountryId";

            }
            else
            {              
                DataTable dtCountry = CountryBLL.CountryList();
                if (dtCountry.Rows.Count > 0)
                {                   
                    listCountry = (from a in dtCountry.AsEnumerable()
                                   select new CountryList
                                   {
                                       CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                       CountryName = GlobalCode.Field2String(a["colCountryNameVarchar"]),
                                   }).ToList();

                    uoDropDownListCountry.DataSource = listCountry;
                    uoDropDownListCountry.DataTextField = "CountryName";
                    uoDropDownListCountry.DataValueField = "CountryId";

                    HttpContext.Current.Session["CountryList"] = listCountry;
                }
            }
            uoDropDownListCountry.Items.Insert(0, (new ListItem("--Select Country--", "0")));
            uoDropDownListCountry.DataBind();           
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page
        /// -------------------------------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Gabriel Oquialda
        /// (description) Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"
        ///               to "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupVehicle\"            
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_HeaderContent_uoHiddenFieldPopupSafeguard\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created: 18/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void SafeguardVendorLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            if (Request.QueryString["vmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for safeguard vendor editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for safeguard vendor editor clicked.";
            }

            strFunction = "SafeguardVendorLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport and Seaport
        private void BindAirportSeaport(int iVendorID, Int16 iLoadType)
        {
            //if (iVendorID == 0)
            //{
                //VendorMaintenanceBLL.VehicleVendorsAirportGet(0,
                //    GlobalCode.Field2TinyInt(uoDropDownListAirportFilter.SelectedValue),
                //    uoTextBoxAirportFilter.Text.Trim(),
                //    false, 0);

            SafeguardBLL.SafeguardVendorsSeaportGet(iVendorID,
                    GlobalCode.Field2TinyInt(uoDropDownListSeaportFilter.SelectedValue),
                    uoTextBoxSeaportFilter.Text.Trim(),
                    false, 0);

            //}
            //BindAirportDroDown(iContractID, iLoadType);
            //BindAirportListView(iContractID, iLoadType);

            BindSeaportDroDown(iVendorID, iLoadType);
            BindSeaportListView(iVendorID, iLoadType);
        }
        /// <summary>
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Seaport in Drop Down List
        ///                 List of Seaport to be added in Contract
        /// </summary>        
        private void BindSeaportDroDown(int iVendorID, Int16 iLoadType)
        {
            List<Seaport> list = new List<Seaport>();
            uoDropDownListSeaport.Items.Clear();

            if (iLoadType == 0)
            {
                if (Session["VendorSeaportNOTExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportNOTExists"];
                    uoDropDownListSeaport.DataSource = list;
                    uoDropDownListSeaport.DataTextField = "SeaportName";
                    uoDropDownListSeaport.DataValueField = "SeaportID";
                    uoDropDownListSeaport.DataBind();
                }
            }
            else
            {
                SafeguardBLL.SafeguardVendorsSeaportGet(iVendorID,
                   GlobalCode.Field2TinyInt(uoDropDownListSeaportFilter.SelectedValue),
                   uoTextBoxSeaportFilter.Text.Trim(), false, 1);

                if (Session["VendorSeaportNOTExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportNOTExists"];
                    uoDropDownListSeaport.DataSource = list;
                    uoDropDownListSeaport.DataTextField = "SeaportName";
                    uoDropDownListSeaport.DataValueField = "SeaportID";
                    uoDropDownListSeaport.DataBind();
                }
            }
            if (list.Count == 1)
            {
                if (uoDropDownListSeaport.Items.FindByValue(list[0].SeaportID.ToString()) != null)
                {
                    uoDropDownListSeaport.SelectedValue = list[0].SeaportID.ToString();
                }
            }
            uoDropDownListSeaport.Items.Insert(0, new ListItem("--Select Seaport--", "0"));
        }

        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Seaport in ListView
        ///                 List of Seaport in Contract
        /// </summary>        
        private void BindSeaportListView(int iVendorID, Int16 iLoadType)
        {
            List<Seaport> list = new List<Seaport>();
            uoListViewSeaport.DataSource = null;

            if (iLoadType == 0)
            {
                if (Session["VendorSeaportExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportExists"];
                    uoListViewSeaport.DataSource = list;
                }
            }
            else
            {
                SafeguardBLL.SafeguardVendorsSeaportGet(0,
                   GlobalCode.Field2TinyInt(uoDropDownListSeaportFilter.SelectedValue),
                   uoTextBoxSeaportFilter.Text.Trim(), true, 1);

                if (Session["VendorSeaportExists"] != null)
                {
                    list = (List<Seaport>)Session["VendorSeaportExists"];
                    uoListViewSeaport.DataSource = list;
                }
            }
            uoListViewSeaport.DataBind();
        }

        protected void uoButtonSeaportFilter_Click(object sender, EventArgs e)
        {
            BindSeaportDroDown(GlobalCode.Field2Int(uoHiddenFieldSafeguardVendorIdInt.Value), 1);
        }

        protected void uoButtonSeaportAdd_Click(object sender, EventArgs e)
        {
            SeaportListViewAdd(GlobalCode.Field2Int(uoDropDownListSeaport.SelectedValue), uoDropDownListSeaport.SelectedItem.Text);
        }

        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Add Seaport in ListView and remove from Drop Down
        /// </summary> 
        private void SeaportListViewAdd(int iSeaportID, string sSeaportName)
        {
            uoListViewSeaport.DataSource = null;

            try
            {
                List<Seaport> list = new List<Seaport>();
                Seaport seaItem = null;

                list = GetSeaportList();

                seaItem = new Seaport();
                seaItem.ID = 0;
                seaItem.SeaportID = iSeaportID;
                seaItem.SeaportName = sSeaportName;
                list.Add(seaItem);

                list = list.OrderBy(a => a.SeaportName).ToList();

                Session["VendorSeaportExists"] = list;
                BindSeaportListView(0, 0);

                //==================================================================
                //Remove selected from DropDownList
                //==================================================================
                List<Seaport> listDDL = new List<Seaport>();
                if (Session["VendorSeaportNOTExists"] != null)
                {
                    listDDL = (List<Seaport>)Session["VendorSeaportNOTExists"];
                }
                else
                {
                    seaItem = new Seaport();
                    seaItem.ID = 0;
                    seaItem.SeaportID = iSeaportID;
                    seaItem.SeaportName = sSeaportName;
                    listDDL.Add(seaItem);
                }
                listDDL.RemoveAll(a => listDDL.Exists(b => a.SeaportID == iSeaportID));
                listDDL = listDDL.OrderBy(a => a.SeaportName).ToList();
                Session["VendorSeaportNOTExists"] = listDDL;
                BindSeaportDroDown(0, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List from Session or ListView
        /// </summary> 
        private List<Seaport> GetSeaportList()
        {
            List<Seaport> list = new List<Seaport>();
            Seaport seaItem = null;
            if (Session["VendorSeaportExists"] != null)
            {
                list = (List<Seaport>)Session["VendorSeaportExists"];
            }
            else
            {
                HiddenField lvHiddenFieldSeaportID;
                HiddenField lvHiddenFieldContractSeaportID;
                Label lvLabelSeaport;


                foreach (ListViewItem item in uoListViewSeaport.Items)
                {
                    lvHiddenFieldContractSeaportID = (HiddenField)item.FindControl("uoHiddenFieldContractSeaportID");
                    lvHiddenFieldSeaportID = (HiddenField)item.FindControl("uoHiddenFieldSeaportID");
                    lvLabelSeaport = (Label)item.FindControl("uoLabelSeaport");

                    seaItem = new Seaport();

                    seaItem.ID = GlobalCode.Field2Int(lvHiddenFieldContractSeaportID.Value);
                    seaItem.SeaportID = GlobalCode.Field2Int(lvHiddenFieldSeaportID.Value);
                    seaItem.SeaportName = lvLabelSeaport.Text;

                    list.Add(seaItem);
                }
            }

            return list;
        }

        protected void uoListViewSeaport_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label uoLabelSeaport = (Label)uoListViewSeaport.Items[e.ItemIndex].FindControl("uoLabelSeaport");
            HiddenField uoHiddenFieldSeaportID = (HiddenField)uoListViewSeaport.Items[e.ItemIndex].FindControl("uoHiddenFieldSeaportID");

            int i = GlobalCode.Field2Int(uoHiddenFieldSeaportID.Value);
            SeaportListViewRemove(i, uoLabelSeaport.Text);
        }

        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine Gad
        /// (description)   Remove Seaport in ListView and add in Drop Down
        /// </summary> 
        private void SeaportListViewRemove(int iSeaportID, string sSeaportName)
        {
            List<Seaport> list = new List<Seaport>();
            list = GetSeaportList();

            list.RemoveAll(a => list.Exists(b => a.SeaportID == iSeaportID));
            list = list.OrderBy(a => a.SeaportName).ToList();

            Session["VendorSeaportExists"] = list;
            BindSeaportListView(0, 0);


            List<Seaport> listDDL = new List<Seaport>();
            if (Session["VendorSeaportNOTExists"] != null)
            {
                listDDL = (List<Seaport>)Session["VendorSeaportNOTExists"];
            }
            Seaport seaItem = null;
            seaItem = new Seaport();
            seaItem.ID = 0;
            seaItem.SeaportID = iSeaportID;
            seaItem.SeaportName = sSeaportName;
            listDDL.Add(seaItem);
            listDDL = listDDL.OrderBy(a => a.SeaportName).ToList();

            Session["VendorSeaportNOTExists"] = listDDL;
            BindSeaportDroDown(0, 0);
        }

        #endregion
    }
}

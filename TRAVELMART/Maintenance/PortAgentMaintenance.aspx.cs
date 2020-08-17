using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using System.Data;
using TRAVELMART.Common;

using System.IO;

namespace TRAVELMART
{
    public partial class PortAgentMaintenance : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldURLFrom.Value = GlobalCode.Field2String(Session["strPrevPage"]);
                if (Request.QueryString["vmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                PortAgentLogAuditTrail();

                uoHiddenFieldPortAgentIdInt.Value = Request.QueryString["vmId"];

                //vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));                
                vendorInfoLoad(0);
                //vendorCountryLoad();
            }
        }

        /// <summary>
        /// Date Created: 27/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Save new vendor        
        /// -------------------------------------------
        /// Date Modified:  05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Change endorMaintenanceBLL.vendorMaintenanceInsert to endorMaintenanceBLL.PortAgentsSave
        ///                 Add Audit Trail in the same script
        ///                 Remove the separate Audit Trail
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            DataTable dtAirport = null;
            DataTable dtVehicleType = null;
            try
            {
                string strLogDescription;
                string strFunction = "uoButtonSave_Click";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                if (GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value) == 0)
                {
                    strLogDescription = "Service Provider  vendor added.";
                }
                else
                {
                    strLogDescription = "Service Provider vendor updated.";
                }

                string vendorPrimaryId = GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value).ToString();//(Session["vendorPrimaryId"].ToString() == null) ? "0" : Session["vendorPrimaryId"].ToString();

                //Airport Table
                dtAirport = new DataTable();
                dtAirport.Columns.Add("AirportCode", typeof(string));

                DataRow r;
                HiddenField hiddenFieldAirport;

                foreach (ListViewItem item in uoListViewAirport.Items)
                {
                    r = dtAirport.NewRow();
                    hiddenFieldAirport = (HiddenField)item.FindControl("uoHiddenFieldAirportID");
                    r[0] = hiddenFieldAirport.Value;
                    dtAirport.Rows.Add(r);
                }

                //Vehicle Type Table
                dtVehicleType = new DataTable();
                dtVehicleType.Columns.Add("VehicleTypeID", typeof(int));

                DataRow rVehicleType;
                HiddenField hiddenFieldVehicleTypeID;

                foreach (ListViewItem item in uoListViewVehicleType.Items)
                {
                    rVehicleType = dtVehicleType.NewRow();
                    hiddenFieldVehicleTypeID = (HiddenField)item.FindControl("uoHiddenFieldVehicleTypeID");
                    rVehicleType[0] = GlobalCode.Field2Int(hiddenFieldVehicleTypeID.Value);
                    dtVehicleType.Rows.Add(rVehicleType);
                }

                VendorMaintenanceBLL.PortAgentVendorsSave(GlobalCode.Field2Int(vendorPrimaryId),
                    uoTextBoxVendorName.Text.Trim(), GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
                    GlobalCode.Field2Int(uoDropDownListCity.SelectedValue), uoTextBoxContactNo.Text,
                    uoTextBoxFaxNo.Text, uoTextBoxContactPerson.Text, uoTextBoxVendorAddress.Text,
                    uoTextBoxEmailCc.Text, uoTextBoxEmailTo.Text, uoTextBoxWebsite.Text, uoTextBoxVendorID.Text.Trim(),
                    uoHiddenFieldUser.Value, strLogDescription, strFunction,
                    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now,
                    dtAirport, dtVehicleType);

                AlertMessage("Save successfully.", true);
                //Response.Redirect(uoHiddenFieldURLFrom.Value +"?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
                Response.Redirect(uoHiddenFieldURLFrom.Value);

            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message, true);
            }
            finally
            {
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
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
        //protected void uoListViewAirport_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;

        //    int index = GlobalCode.Field2Int(e.CommandArgument);
        //    if (e.CommandName == "Delete")
        //    {
        //        //MaintenanceViewBLL.DeletePortAgent(index, GlobalCode.Field2String(Session["UserName"]));

        //        ////Insert log audit trail (Gabriel Oquialda - 17/11/2011)
        //        //strLogDescription = "Vehicle vendor branch deleted. (flagged as inactive)";
        //        //strFunction = "uoPortAgentList_ItemCommand";

        //        //DateTime currentDate = CommonFunctions.GetCurrentDateTime();

        //        //BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //        //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

        //        BindVendorTypeList(1);
        //    }          
        //}
        
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine  gad
        /// (description)   Add Airport in ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonPortAgentAirportAdd_Click(object sender, EventArgs e)
        {
            List<AirportDTO> listAirport = new List<AirportDTO>();
            List<AirportDTO> ListPortAgentAirport = new List<AirportDTO>();

            listAirport = GetAirportNotExist(0);
            ListPortAgentAirport = GetPortAgentAirport(0);

            listAirport.RemoveAll(a => listAirport.Exists(b => a.AirportIDString == uoDropDownListAirport.SelectedValue));
            listAirport = listAirport.OrderBy(a => a.AirportNameString).ToList();

            AirportDTO item = new AirportDTO();
            item.AirportIDString = uoDropDownListAirport.SelectedValue;
            item.AirportNameString = uoDropDownListAirport.SelectedItem.Text;

            ListPortAgentAirport.Add(item);
            ListPortAgentAirport = ListPortAgentAirport.OrderBy(a => a.AirportNameString).ToList();

            Session["PortAgentAirportNotExist"] = listAirport;
            Session["PortAgentAirport"] = ListPortAgentAirport;
            
            BindAirportList(0);
            BindPortAgentAirportList(0);
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine  gad
        /// (description)   Add Filter Airport
        /// </summary>
        protected void uoButtonAirportFilter_Click(object sender, EventArgs e)
        {
            BindAirportList(0);
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine  gad
        /// (description)   Remove Airport in Service Provider
        /// </summary>
        protected void uoListViewAirport_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField uoHiddenFieldAirportID = (HiddenField)uoListViewAirport.Items[e.ItemIndex].FindControl("uoHiddenFieldAirportID");
            Label uoLabelAirportName = (Label)uoListViewAirport.Items[e.ItemIndex].FindControl("uoLabelAirportName");

            List<AirportDTO> listAirport = new List<AirportDTO>();
            List<AirportDTO> ListPortAgentAirport = new List<AirportDTO>();
            
            listAirport = GetAirportNotExist(0);
            ListPortAgentAirport = GetPortAgentAirport(0);

            ListPortAgentAirport.RemoveAll(a => ListPortAgentAirport.Exists(b => a.AirportIDString == uoHiddenFieldAirportID.Value));
            ListPortAgentAirport = ListPortAgentAirport.OrderBy(a => a.AirportNameString).ToList();

            AirportDTO item = new AirportDTO();
            item.AirportIDString = uoHiddenFieldAirportID.Value;
            item.AirportNameString = uoLabelAirportName.Text;
            listAirport.Add(item);

            Session["PortAgentAirportNotExist"] = listAirport;
            Session["PortAgentAirport"] = ListPortAgentAirport;

            BindAirportList(0);
            BindPortAgentAirportList(0);
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine  gad
        /// (description)   Add Service Provider Vehicle Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonVehicleTypeAdd_Click(object sender, EventArgs e)
        {
            List<VehicleType> listType = new List<VehicleType>();
            List<PortAgentVehicleType> listVendor = new List<PortAgentVehicleType>();

            listType = GetVehicleTypeList(0);
            listVendor = GetPortAgentVehicleTypeList(0);

            listType.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue)));
            listType = listType.OrderBy(a => a.VehicleTypeName).ToList();

            PortAgentVehicleType item = new PortAgentVehicleType();
            item.VehicleType = uoDropDownListVehicleType.SelectedItem.Text;
            item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue);
            item.PortAgentVendorID = GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value);

            listVendor.Add(item);
            listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

            Session["PortAgentVehicleType"] = listVendor;
            Session["VehicleType"] = listType;

            BindVendorTypeList(0);
            BindVehicleTypeList(0);
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine  gad
        /// (description)   Remove Type in ListView
        /// </summary>
        protected void uoListViewVehicleType_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField uoHiddenFieldVehicleTypeID = (HiddenField)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoHiddenFieldVehicleTypeID");
            Label uoLabelVehicleTypeName = (Label)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoLabelVehicleTypeName");
            //BindVendorTypeList(1);

            List<VehicleType> listType = new List<VehicleType>();
            List<PortAgentVehicleType> listVendor = new List<PortAgentVehicleType>();

            listType = GetVehicleTypeList(0);
            listVendor = GetPortAgentVehicleTypeList(0);

            listVendor.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value)));
            listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

            VehicleType item = new VehicleType();
            item.VehicleTypeID = GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value);
            item.VehicleTypeName = uoLabelVehicleTypeName.Text;
            listType.Add(item);

            Session["PortAgentVehicleType"] = listVendor;
            Session["VehicleType"] = listType;

            BindVendorTypeList(0);
            BindVehicleTypeList(0);
        }
        protected void uoButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(uoHiddenFieldURLFrom.Value);
        }
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
        /// (decription)    Load Vendor using VendorMaintenanceBLL.PortAgentsGetByID
        ///                 Use Sessions instead of DataTable        
        /// </summary>
        private void vendorInfoLoad(Int16 iLoadType)
        {
            if (Request.QueryString["vmId"].ToString() != "0")
            {

                VendorMaintenanceBLL.GetPortAgentByID(GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value), iLoadType);
                List<VendorPortAgentDetails> list = new List<VendorPortAgentDetails>();

                if (Session["PortAgentVendorDetails"] != null)
                {
                    list = (List<VendorPortAgentDetails>)Session["PortAgentVendorDetails"];
                    if (list.Count > 0)
                    {
                        uoTextBoxCity.Text = GlobalCode.Field2String(Session["PortAgentVendorCityFilter"]);
                        uoTextBoxVendorName.Text = list[0].PortAgentName;
                        uoTextBoxVendorAddress.Text = list[0].Address;
                        uoTextBoxWebsite.Text = list[0].Website;

                        uoTextBoxEmailTo.Text = list[0].EmailTo;
                        uoTextBoxEmailCc.Text = list[0].EmailCC;

                        vendorCountryLoad();
                        if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(list[0].CountryID)) != null)
                        {
                            uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(list[0].CountryID);
                        }
                        vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                        if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(list[0].CityID)) != null)
                        {
                            uoDropDownListCity.SelectedValue = GlobalCode.Field2String(list[0].CityID);
                        }
                        uoTextBoxContactNo.Text = list[0].ContactNo;
                        uoTextBoxContactPerson.Text = list[0].ContactPerson;
                        uoTextBoxFaxNo.Text = list[0].FaxNo;
                        uoTextBoxVendorID.Text = list[0].VendorIMS_ID;
                        BindAirportList(0);
                        BindPortAgentAirportList(0);
                        BindVendorTypeList(0);
                        BindVehicleTypeList(0);
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
                    uoTextBoxVendorID.Text = "";

                    uoListViewAirport.DataSource = null;
                    uoListViewAirport.DataBind();
                    BindAirportList(1);

                    BindVendorTypeList(1);
                    BindVehicleTypeList(1);
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
                uoTextBoxVendorID.Text = "";

                uoListViewAirport.DataSource = null;
                uoListViewAirport.DataBind();
                BindAirportList(1);

                BindVendorTypeList(1);
                BindVehicleTypeList(1);
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
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Change Datatable to Session
        /// -----------------------------------------------------
        /// </summary>
        private void vendorCountryLoad()
        {
            DataTable dt = null;
            try
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
                    dt = CountryBLL.CountryList();
                    if (dt.Rows.Count > 0)
                    {
                        uoDropDownListCountry.DataSource = dt;
                        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                        uoDropDownListCountry.DataValueField = "colCountryIDInt";
                    }
                }

                uoDropDownListCountry.Items.Insert(0, (new ListItem("--Select Country--", "0")));
                uoDropDownListCountry.DataBind();
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
            sScript += " window.parent.$(\"#ctl00_HeaderContent_uoHiddenFieldPopupPortAgent\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created: 18/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s, bool IsScripManager)
        {
            s = s.Replace("'", "\"");
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            if (IsScripManager)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            }
        }
        /// Date Modified:   05/Nov/2013
        /// Modified By:     Josephine Gad
        /// (description)    Audit trail logs
        protected void PortAgentLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            if (Request.QueryString["vmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for Service Provider vendor editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for Service Provider vendor editor clicked.";
            }

            strFunction = "PortAgentLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Airport under PortAgent
        /// </summary>
        private void BindPortAgentAirportList(int iLoadType)
        {
            uoListViewAirport.DataSource = GetPortAgentAirport(iLoadType);
            uoListViewAirport.DataBind();
        }
         //<summary>
         //Date Created:   05/Nov/2013
         //Created By:     Josephine gad
         //(description)   Bind list of Airport to be added
         //</summary>
        private void BindAirportList(int iLoadType)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            List<AirportDTO> listAirport = new List<AirportDTO>();
            list = GetAirportNotExist(iLoadType);

            List<AirportDTO> listVendor = new List<AirportDTO>();
            listVendor = GetPortAgentAirport(iLoadType);


            for (int i = 0; i < listVendor.Count; i++)
            {
                list.RemoveAll(a => list.Exists(b => a.AirportIDString == listVendor[i].AirportIDString));
            }

            list = list.OrderBy(a => a.AirportNameString).ToList();
            Session["PortAgentAirportNotExist"] = list;

            if (uoTextBoxAirportCode.Text.Trim() != "")
            {
                listAirport = (from a in list
                               where a.AirportIDString.ToUpper().StartsWith(uoTextBoxAirportCode.Text.ToUpper())
                               select new AirportDTO
                               {
                                   AirportIDString = a.AirportIDString,
                                   AirportNameString = a.AirportNameString
                               }).ToList();
            }
            else
            {
                listAirport = list;
            }

            uoDropDownListAirport.Items.Clear();
            uoDropDownListAirport.DataSource = listAirport;

            uoDropDownListAirport.DataTextField = "AirportNameString";
            uoDropDownListAirport.DataValueField = "AirportIDString";

            uoDropDownListAirport.DataBind();
            uoDropDownListAirport.Items.Insert(0, new ListItem("--Select Airport--", "0"));

            if (listAirport.Count == 1)
            {
                if (uoDropDownListAirport.Items.FindByValue(uoTextBoxAirportCode.Text.ToUpper()) != null)
                {
                    uoDropDownListAirport.SelectedValue = listAirport[0].AirportIDString;
                }
            }
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Service Provider Airport
        /// </summary>
        private List<AirportDTO> GetPortAgentAirport(int iLoadType)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            if (iLoadType == 0)
            {
                if (Session["PortAgentAirport"] != null)
                {
                    list = (List<AirportDTO>)Session["PortAgentAirport"];
                }
            }
            else
            {
                VendorMaintenanceBLL.GetPortAgentAirport(GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value));
                list = (List<AirportDTO>)Session["PortAgentAirport"];
            }
            return list;
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Airport not yet assigned to Service Provider
        /// </summary>
        private List<AirportDTO> GetAirportNotExist(int iLoadType)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            if (iLoadType == 0)
            {
                if (Session["PortAgentAirportNotExist"] != null)
                {
                    list = (List<AirportDTO>)Session["PortAgentAirportNotExist"];
                }
            }
            else
            {
                VendorMaintenanceBLL.GetPortAgentAirport(GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value));
                list = (List<AirportDTO>)Session["PortAgentAirportNotExist"];
            }

            return list;
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type
        /// </summary>
        private void BindVendorTypeList(int iLoadType)
        {
            uoListViewVehicleType.DataSource = GetPortAgentVehicleTypeList(iLoadType);
            uoListViewVehicleType.DataBind();
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type to be added
        /// </summary>
        private void BindVehicleTypeList(int iLoadType)
        {
            List<VehicleType> list = new List<VehicleType>();
            list = GetVehicleTypeList(iLoadType);

            List<PortAgentVehicleType> listVendor = new List<PortAgentVehicleType>();
            listVendor = GetPortAgentVehicleTypeList(iLoadType);


            for (int i = 0; i < listVendor.Count; i++)
            {
                list.RemoveAll(a => list.Exists(b => a.VehicleTypeID == listVendor[i].VehicleTypeID));
            }

            list = list.OrderBy(a => a.VehicleTypeName).ToList();
            Session["VehicleType"] = list;

            uoDropDownListVehicleType.Items.Clear();
            uoDropDownListVehicleType.DataSource = list;

            uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
            uoDropDownListVehicleType.DataValueField = "VehicleTypeID";

            uoDropDownListVehicleType.DataBind();
            uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Vehicle Type--", "0"));
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Vehicle Type
        /// </summary>
        private List<VehicleType> GetVehicleTypeList(int iLoadType)
        {
            List<VehicleType> list = new List<VehicleType>();
            if (iLoadType == 0)
            {
                if (Session["VehicleType"] != null)
                {
                    list = (List<VehicleType>)Session["VehicleType"];
                }
            }
            else
            {
                VendorMaintenanceBLL.GetPortAgentVehicleType(GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value));
                list = (List<VehicleType>)Session["VehicleType"];
            }

            return list;
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of PortAgent Vehicle Type
        /// </summary>
        private List<PortAgentVehicleType> GetPortAgentVehicleTypeList(int iLoadType)
        {
            List<PortAgentVehicleType> list = new List<PortAgentVehicleType>();
            if (iLoadType == 0)
            {
                if (Session["PortAgentVehicleType"] != null)
                {
                    list = (List<PortAgentVehicleType>)Session["PortAgentVehicleType"];
                }
            }
            else
            {
                VendorMaintenanceBLL.GetPortAgentVehicleType(GlobalCode.Field2Int(uoHiddenFieldPortAgentIdInt.Value));
                list = (List<PortAgentVehicleType>)Session["PortAgentVehicleType"];
            }
            return list;
        }
        #endregion

        
    }
}

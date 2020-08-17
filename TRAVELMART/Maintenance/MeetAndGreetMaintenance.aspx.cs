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

namespace TRAVELMART.Maintenance
{
    public partial class MeetAndGreetMaintenance : System.Web.UI.Page
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
                VehicleVendorLogAuditTrail();

                uoHiddenFieldVehicleVendorIdInt.Value = Request.QueryString["vmId"];

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

                if (GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value) == 0)
                {
                    strLogDescription = "Meet and Greet vendor added.";
                }
                else
                {
                    strLogDescription = "Meet and Greet vendor updated.";
                }

                string vendorPrimaryId = GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value).ToString();//(Session["vendorPrimaryId"].ToString() == null) ? "0" : Session["vendorPrimaryId"].ToString();
                
                dt = new DataTable();
                dt.Columns.Add("VehicleTypeID", typeof(int));

                DataRow r;
                HiddenField hiddenFieldVehicleTypeID;

                foreach (ListViewItem item in uoListViewVehicleType.Items)
                {
                    r = dt.NewRow();
                    hiddenFieldVehicleTypeID = (HiddenField)item.FindControl("uoHiddenFieldVehicleTypeID");
                    r[0] = GlobalCode.Field2Int(hiddenFieldVehicleTypeID.Value);
                    dt.Rows.Add(r);
                }

                //Commment for now BY JHO
                //MeetAndGreetBLL.MeetAndGreetSave(GlobalCode.Field2Int(vendorPrimaryId),
                //    uoTextBoxVendorName.Text, //GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
                //    GlobalCode.Field2Int(uoDropDownListCity.SelectedValue), uoTextBoxContactNo.Text,
                //    uoTextBoxFaxNo.Text, uoTextBoxContactPerson.Text, uoTextBoxVendorAddress.Text,
                //    uoTextBoxEmailCc.Text, uoTextBoxEmailTo.Text, uoTextBoxWebsite.Text,
                //    GlobalCode.Field2String(Session["UserName"]), strLogDescription, strFunction,
                //    Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, dt,dt);

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
        protected void uoButtonVehicleTypeAdd_Click(object sender, EventArgs e)
        {
            List<VehicleType> listType = new List<VehicleType>();
            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();

            listType = GetVehicleTypeList(0);
            listVendor = GetVehicleVendorTypeList(0);

            listType.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue)));
            listType = listType.OrderBy(a => a.VehicleTypeName).ToList();

            VendorVehicleType item = new VendorVehicleType();
            item.VehicleType = uoDropDownListVehicleType.SelectedItem.Text;
            item.VehicleTypeID = GlobalCode.Field2Int(uoDropDownListVehicleType.SelectedValue);
            //Commment for now BY JHO
            //item.VehicleVendorTypeID = GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value);

            listVendor.Add(item);
            listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

            Session["VendorVehicleType"] = listVendor;
            Session["VehicleType"] = listType;

            BindVendorTypeList(0);
            BindVehicleTypeList(0);
        }
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine  gad
        /// (description)   Remove Type in ListView
        /// </summary>
        protected void uoListViewVehicleType_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField uoHiddenFieldVehicleTypeID = (HiddenField)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoHiddenFieldVehicleTypeID");
            Label uoLabelVehicleTypeName = (Label)uoListViewVehicleType.Items[e.ItemIndex].FindControl("uoLabelVehicleTypeName");
            //BindVendorTypeList(1);

            List<VehicleType> listType = new List<VehicleType>();
            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();

            listType = GetVehicleTypeList(0);
            listVendor = GetVehicleVendorTypeList(0);

            listVendor.RemoveAll(a => listType.Exists(b => a.VehicleTypeID == GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value)));
            listVendor = listVendor.OrderBy(a => a.VehicleType).ToList();

            VehicleType item = new VehicleType();
            item.VehicleTypeID = GlobalCode.Field2Int(uoHiddenFieldVehicleTypeID.Value);
            item.VehicleTypeName = uoLabelVehicleTypeName.Text;
            listType.Add(item);

            Session["MeetAndGreetAirport"] = listVendor;
            Session["MeetAndGreetType"] = listType;

            BindVendorTypeList(0);
            BindVehicleTypeList(0);
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
        /// (decription)    Load Vendor using VendorMaintenanceBLL.VehicleVendorsGetByID
        ///                 Use Sessions instead of DataTable        
        /// </summary>
        private void vendorInfoLoad(Int16 iLoadType)
        {
            if (Request.QueryString["vmId"].ToString() != "0")
            {

                //Commment for now BY JHO
                //MeetAndGreetBLL.MeetAndGreetVendorsGetByID(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value), iLoadType);
                List<VendorVehicleDetails> listVehicleDetails = new List<VendorVehicleDetails>();

                if (Session["MeetAndGreetVendorDetails"] != null)
                {
                    listVehicleDetails = (List<VendorVehicleDetails>)Session["MeetAndGreetVendorDetails"];
                    if (listVehicleDetails.Count > 0)
                    {
                        uoTextBoxCity.Text = GlobalCode.Field2String(Session["MeetAndGreetVendorCityFilter"]);
                        uoTextBoxVendorName.Text = listVehicleDetails[0].VendorName;
                        uoTextBoxVendorAddress.Text = listVehicleDetails[0].Address;
                        uoTextBoxWebsite.Text = listVehicleDetails[0].Website;

                        uoTextBoxEmailTo.Text = listVehicleDetails[0].EmailTo;
                        uoTextBoxEmailCc.Text = listVehicleDetails[0].EmailCC;

                        vendorCountryLoad();
                        if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(listVehicleDetails[0].CountryID)) != null)
                        {
                            uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(listVehicleDetails[0].CountryID);
                        }
                        vendorCityLoad(0, uoDropDownListCountry.SelectedValue);
                        if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(listVehicleDetails[0].CityID)) != null)
                        {
                            uoDropDownListCity.SelectedValue = GlobalCode.Field2String(listVehicleDetails[0].CityID);
                        }
                        uoTextBoxContactNo.Text = listVehicleDetails[0].ContactNo;
                        uoTextBoxContactPerson.Text = listVehicleDetails[0].ContactPerson;
                        uoTextBoxFaxNo.Text = listVehicleDetails[0].FaxNo;
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
                    
                    uoListViewVehicleType.DataSource = null;
                    uoListViewVehicleType.DataBind();
                    BindVehicleTypeList(1);
                }
            }
            else
            {
                //Commment for now BY JHO
                //MeetAndGreetBLL.MeetAndGreetVendorsGetByID(0, iLoadType);
                List<VendorVehicleDetails> listVehicleDetails = new List<VendorVehicleDetails>();

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
                uoListViewVehicleType.DataSource = null;
                uoListViewVehicleType.DataBind();
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
            sScript += " window.parent.$(\"#ctl00_HeaderContent_uoHiddenFieldPopupVehicle\").val(\"1\"); ";
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
        protected void VehicleVendorLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            if (Request.QueryString["vmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for meet and greet vendor editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for meet and greet vendor editor clicked.";
            }

            strFunction = "VehicleVendorLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Bind ListView of Vehicle Type
        /// </summary>
        private void BindVendorTypeList(int iLoadType)
        {
            uoListViewVehicleType.DataSource = GetVehicleVendorTypeList(iLoadType);
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

            List<VendorVehicleType> listVendor = new List<VendorVehicleType>();
            listVendor = GetVehicleVendorTypeList(iLoadType);

            
            for (int i = 0; i< listVendor.Count; i++)
            {
                list.RemoveAll(a => list.Exists(b => a.VehicleTypeID == listVendor[i].VehicleTypeID));                            
            }

            list = list.OrderBy(a => a.VehicleTypeName).ToList();
            Session["MeetAndGreetType"] = list;

            uoDropDownListVehicleType.Items.Clear();
            uoDropDownListVehicleType.DataSource = list;

            uoDropDownListVehicleType.DataTextField = "VehicleTypeName";
            uoDropDownListVehicleType.DataValueField = "VehicleTypeID";

            uoDropDownListVehicleType.DataBind();
            uoDropDownListVehicleType.Items.Insert(0, new ListItem("--Select Airport--", "0"));
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Vendor Vehicle Type
        /// </summary>
        private List<VendorVehicleType> GetVehicleVendorTypeList(int iLoadType)
        {
            List<VendorVehicleType> list = new List<VendorVehicleType>();
            if (iLoadType == 0)
            {
                if (Session["MeetAndGreetAirport"] != null)
                {
                    list = (List<VendorVehicleType>)Session["MeetAndGreetAirport"];
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsTypeGet(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value));
                list = (List<VendorVehicleType>)Session["MeetAndGreetAirport"];
            }
            return list;
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine gad
        /// (description)   Get ListView of Vehicle Type
        /// </summary>
        private List<VehicleType> GetVehicleTypeList(int iLoadType)
        {
            List<VehicleType> list = new List<VehicleType>();
            if (iLoadType == 0)
            {
                if (Session["MeetAndGreetType"] != null)
                {
                    list = (List<VehicleType>)Session["MeetAndGreetType"];
                }
            }
            else
            {
                VendorMaintenanceBLL.VehicleVendorsTypeGet(GlobalCode.Field2Int(uoHiddenFieldVehicleVendorIdInt.Value));
                list = (List<VehicleType>)Session["MeetAndGreetType"];
            }

            return list;
        }
        #endregion             
    }
}

using System;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;

namespace TRAVELMART
{
    public partial class PortMaintenance : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                PortVendorLogAuditTrail();

                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]); ;
                if (uoHiddenFieldRole.Value == "")
                {
                    uoHiddenFieldRole.Value = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                }
                Session["UserRole"] = uoHiddenFieldRole.Value;
                Session["UserName"] = uoHiddenFieldUser.Value;

                if (Request.QueryString["vmId"] != null)
                {
                    Session["vendorPrimaryId"] = Request.QueryString["vmId"];
                    uoHiddenFieldPortID.Value = Request.QueryString["vmId"];
                }
                PortCountryLoad();
                //ChangeToUpperCase(uoDropDownListCountry);
                //PortCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                //ChangeToUpperCase(uoDropDownListCity);
                LoadPort();
                LoadAirport(true);
                LoadAirportDDL(true);
            }
        }

        /// <summary>
        /// Date Created:   01/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Save new port    
        /// --------------------------------
        /// Date Modified:  07/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add saving of Airport to Seaport          
        /// --------------------------------
        /// Date Modified:  18/07/2012
        /// Modified By:    Jefferson Bermundo
        /// (description)   Update uoHiddenFieldPortID to the value of the the new pPortId
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;
            DateTime currentDate = CommonFunctions.GetCurrentDateTime();
            DateTime GMTDate = CommonFunctions.GetDateTimeGMT(currentDate);
            string sPageName = Path.GetFileName(Request.Path);

            try
            {
                //PortBLL.CheckPortCode(uoTextBoxPortCode.Text, Convert.ToInt32(uoHiddenFieldPortID.Value));
                Int32 pPortID = PortBLL.AddNewPort(uoTextBoxPortName.Text, null,
                    Convert.ToInt32(uoDropDownListCountry.SelectedValue),
                    GlobalCode.Field2String(Session["UserName"]), Int32.Parse(Session["vendorPrimaryId"].ToString()),
                    uoTextBoxPortCode.Text);

                uoHiddenFieldPortID.Value = pPortID.ToString();

                List<Airport> AirportList = new List<Airport>();
                List<Airport> AirportNotInList = new List<Airport>();

                AirportList = AirportInSeaport(false);
                AirportNotInList = AirportNotInSeaport(false);

                for (int i = 0; i < AirportList.Count; i++)
                {
                    if (AirportList[i].AirportSeaportID == 0)
                    {
                        strLogDescription = "Airport In Seaport added.";
                        strFunction = "uoButtonSave_Click";

                        BLL.MaintenanceViewBLL.InsertAirportInSeaport(uoHiddenFieldUser.Value, AirportList[i].AirportID,
                          uoHiddenFieldPortID.Value, strLogDescription, strFunction, sPageName);
                    }
                }

                if (Request.QueryString["vmId"] == "0" || Request.QueryString["vmId"] == null)
                {
                    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                    strLogDescription = "Port added.";
                    strFunction = "uoButtonSave_Click";

                    BLL.AuditTrailBLL.InsertLogAuditTrail(pPortID, "", strLogDescription, strFunction, sPageName,
                                                          GMTDate, currentDate, uoHiddenFieldUser.Value);
                }
                else
                {
                    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                    strLogDescription = "Service Provider seaport updated.";
                    strFunction = "uoButtonSave_Click";

                    //DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(pPortID, "", strLogDescription, strFunction, sPageName,
                                                          GMTDate, currentDate, uoHiddenFieldUser.Value);
                }

                OpenParentPage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        /// <summary>
        /// Date Modified:      18/07/2012
        /// Modified by:        Jefferson Bermundo
        /// Description:        PortCityLoad not used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 1)
            {
                //PortCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                //ChangeToUpperCase(uoDropDownListCity);
            }
        }
        protected void uoGridViewAirport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = uoGridViewAirport.SelectedIndex;

            Int32 AirportSeaportID = GlobalCode.Field2Int(uoGridViewAirport.Rows[i].Cells[0].Text);
            string AirportID = uoGridViewAirport.Rows[i].Cells[1].Text;
            string AirportCode = uoGridViewAirport.Rows[i].Cells[2].Text;
            string AirportName = uoGridViewAirport.Rows[i].Cells[3].Text;

            DeleteAirport(AirportSeaportID, AirportID, AirportCode, AirportName);
        }
        protected void uoButtonAddAirport_Click(object sender, EventArgs e)
        {
            CreateDatatableAirport();
        }

        #endregion


        #region Functions
        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) format data to uppercase
        /// --------------------------------------------------------
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }
        /// <summary>
        /// Date Created:   01/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Load port to edit
        /// ---------------------------------------------------
        /// Date Edited:    18/07/2012
        /// Created By:     Jefferson Bermundo
        /// (description)   Remove PortCityLoad
        /// </summary>
        private void LoadPort()
        {
            if (Request.QueryString["vmId"].ToString() != "0")
            {

                Session["vendorPrimaryId"] = Request.QueryString["vmId"];
                IDataReader dtSFInfo = PortBLL.GetPortToEdit(Int32.Parse(Session["vendorPrimaryId"].ToString()));
                if (dtSFInfo.Read())
                {
                    uoTextBoxPortCode.Text = dtSFInfo["colPortCodeVarchar"].ToString();
                    uoTextBoxPortName.Text = dtSFInfo["colPortNameVarchar"].ToString();
                    uoDropDownListCountry.SelectedValue = dtSFInfo["colCountryIDInt"].ToString();
                    if (uoDropDownListCountry.SelectedIndex > 1)
                    {
                        //PortCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                        //uoDropDownListCity.SelectedValue = dtSFInfo["colCityIDInt"].ToString();
                    }
                    //ChangeToUpperCase(uoDropDownListCountry);
                    //ChangeToUpperCase(uoDropDownListCity);
                }
            }
        }
        /// <summary>
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Load Airport to Gridview
        /// --------------------------------------------------------
        /// Date Edited:    18/07/2012
        /// Created By:     Jefferson Bermundo
        /// (description)   Remove Validation is vmId != 0, 
        ///                 for adding Airport to port when
        ///                 a port is newly created
        /// </summary>
        public void LoadAirport(bool IsNew)
        {
            //if (Request.QueryString["vmId"].ToString() != "0")
            //{
            List<Airport> list = new List<Airport>();
            list = AirportInSeaport(IsNew);
            //if (IsNew || Airport.AirportList == null)
            //{
            //   list = MaintenanceViewBLL.GetAirportList(uoHiddenFieldUser.Value,
            //    uoHiddenFieldRole.Value, GlobalCode.Field2Int(uoHiddenFieldPortID.Value), 0, true);

            //   Airport.AirportList = list;
            //}
            Airport.AirportList = list;
            var xList = (from a in list
                         orderby a.AirportName
                         select a).ToList();
            uoGridViewAirport.DataSource = xList;

            uoGridViewAirport.Columns[0].Visible = true;
            uoGridViewAirport.Columns[1].Visible = true;
            uoGridViewAirport.DataBind();
            uoGridViewAirport.Columns[0].Visible = false;
            uoGridViewAirport.Columns[1].Visible = false;
            //}
        }
        /// <summary>
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Load Airport to DropDown
        ///                 List of airport that are not yet added
        /// --------------------------------------------------------
        /// </summary>
        public void LoadAirportDDL(bool IsNew)
        {
            List<Airport> list = new List<Airport>();
            list = AirportNotInSeaport(IsNew);
            Airport.AirportNotInList = list;

            //if (IsNew || Airport.AirportNotInList == null)
            //{
            //    list = MaintenanceViewBLL.GetAirportList(uoHiddenFieldUser.Value,
            //    uoHiddenFieldRole.Value, GlobalCode.Field2Int(uoHiddenFieldPortID.Value), 0, false);
            //Airport.AirportNotInList = list;
            //}

            uoDropDownListAiport.Items.Clear();
            if (Airport.AirportNotInList.Count > 0)
            {
                list = new List<Airport>();
                list = Airport.AirportNotInList;
                var xList = (from a in list
                             orderby a.AirportCodeName
                             select a).ToList();

                uoDropDownListAiport.DataSource = xList;

                uoDropDownListAiport.DataTextField = "AirportCodeName";
                uoDropDownListAiport.DataValueField = "AirportID";
                uoDropDownListAiport.DataBind();
            }
            else
            {
                uoDropDownListAiport.DataBind();
            }
            uoDropDownListAiport.Items.Insert(0, new ListItem("--Select Airport--", "0"));
        }

        /// <summary>
        /// Date Created: 01/08/2011
        /// Created By: Marco Abejar
        /// (description) Loads port city to dropdownlist
        /// -----------------------------------------------
        /// Date Edited:    18/07/2012
        /// Created By:     Jefferson Bermundo
        /// (description)   Remove-- Port city not being used
        /// </summary>
        //private void PortCityLoad(int CountryID)
        //{
        //    DataTable dt = new DataTable();
        //    dt = CityBLL.CityListbyCountry(CountryID);
        //    if (dt.Rows.Count > 0)
        //    {
        //        uoDropDownListCity.DataSource = dt;
        //        uoDropDownListCity.DataTextField = "colCityNameVarchar";
        //        uoDropDownListCity.DataValueField = "colCityIDInt";
        //        uoDropDownListCity.DataBind();
        //        uoDropDownListCity.Items.Insert(0, new ListItem("- Select a City -", ""));
        //    }
        //    else
        //    {
        //        uoDropDownListCity.DataBind();
        //    }
        //}

        private void PortCountryLoad()
        {
            /// <summary>
            /// Date Created: 01/08/2011
            /// Created By: Marco Abejar
            /// (description) Loads port country to dropdownlist           
            /// </summary>

            DataTable dt = new DataTable();
            dt = CountryBLL.CountryList();
            if (dt.Rows.Count > 0)
            {
                uoDropDownListCountry.DataSource = dt;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("- Select a Country -", ""));
                uoDropDownListCountry.SelectedValue = dt.Rows[0]["colCountryIDInt"].ToString();
            }
            else
            {
                uoDropDownListCountry.DataBind();
            }
        }
        private void AlertMessage(string s)
        {
            /// <summary>
            /// Date Created: 07/18/2011
            /// Created By: Marco Abejar
            /// (description) Show pop up message            
            /// </summary>

            //string sScript = "<script language='JavaScript'>";
            //sScript += "alert('" + s + "');";
            //sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);

            s = s.Replace("'", "");
            s = s.Replace("\n", "");
            s = s.Replace("\r", " ");

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
        }
        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Close this page and update parent page            
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupPort\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void PortVendorLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for port editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for port editor clicked.";
            }

            strFunction = "PortVendorLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        /// <summary>
        /// Created Date:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Create data table for Airport
        /// </summary>
        private void CreateDatatableAirport()
        {
            List<Airport> AirportList = null;
            Airport AirportItem = null;

            List<Airport> AirportNotInList = null;
            try
            {
                AirportList = AirportInSeaport(false);
                AirportNotInList = AirportNotInSeaport(false);

                string[] sAirportCodeName = uoDropDownListAiport.SelectedItem.Text.Split("::".ToCharArray());
                if (sAirportCodeName.Length > 0)
                {
                    string sCode = sAirportCodeName[2].Trim();
                    string sName = sAirportCodeName[0].Trim();

                    //Remove selected room type from drop down list
                    AirportNotInList.RemoveAll(a => a.AirportName == sName);

                    AirportItem = new Airport();
                    AirportItem.AirportSeaportID = 0;
                    AirportItem.AirportID = GlobalCode.Field2TinyInt(uoDropDownListAiport.SelectedValue);

                    AirportItem.AirportName = sName;
                    AirportItem.AirportCode = sCode;
                    AirportList.Add(AirportItem);

                    Airport.AirportList = AirportList;
                    LoadAirport(false);
                    LoadAirportDDL(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        /// <summary>
        /// Date Created:      07/03/2012
        /// Created By:        Josephine Gad
        /// (description)      Add try and catch and dispose DataTable       
        /// ---------------------------------------------------------------------                           
        /// </summary>
        protected void DeleteAirport(Int32 AirportSeaportID, string AirportID, string AirportCode, string AirportName)
        {

            List<Airport> AirportList = null;
            Airport AirportItem = null;

            List<Airport> AirportNotInList = null;
            try
            {
                AirportList = AirportInSeaport(false);
                AirportNotInList = AirportNotInSeaport(false);

                string strLogDescription;
                string strFunction;

                if (uoHiddenFieldPortID.Value != "" && uoHiddenFieldPortID.Value != "0")
                {
                    if (AirportSeaportID != 0)
                    {
                        strLogDescription = "Airport deleted from Seaport. (flagged as inactive)";
                        strFunction = "DeleteAirport";

                        MaintenanceViewBLL.RemoveAirportInSeaport(uoHiddenFieldUser.Value, AirportID, uoHiddenFieldPortID.Value,
                            strLogDescription, strFunction, Path.GetFileName(Request.Path));
                    }
                }

                AirportItem = new Airport();
                AirportItem.AirportSeaportID = 0;
                AirportItem.AirportID = GlobalCode.Field2TinyInt(AirportID);
                AirportItem.AirportCode = AirportCode;
                AirportItem.AirportName = AirportName;
                AirportItem.AirportCodeName = AirportName + " :: " + AirportCode;

                AirportNotInList.Add(AirportItem);
                AirportList.RemoveAll(a => a.AirportID == GlobalCode.Field2TinyInt(AirportID));

                Airport.AirportList = AirportList;
                Airport.AirportNotInList = AirportNotInList;

                uoGridViewAirport.DataSource = AirportList;
                uoGridViewAirport.Columns[0].Visible = true;
                uoGridViewAirport.Columns[1].Visible = true;
                uoGridViewAirport.DataBind();
                uoGridViewAirport.Columns[0].Visible = false;
                uoGridViewAirport.Columns[1].Visible = false;

                LoadAirportDDL(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        /// <summary>
        /// Date Created:      07/03/2012
        /// Created By:        Josephine Gad
        /// (description)      Get Airport in Seaport        
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<Airport> AirportInSeaport(bool IsNew)
        {
            List<Airport> list = new List<Airport>();
            if (IsNew || Airport.AirportList == null)
            {
                list = MaintenanceViewBLL.GetAirportList(uoHiddenFieldUser.Value,
                uoHiddenFieldRole.Value, GlobalCode.Field2Int(uoHiddenFieldPortID.Value), 0, true);

                Airport.AirportList = list;
            }
            else
            {
                if (Airport.AirportList == null)
                {
                    List<Airport> AirportList = new List<Airport>();
                    Airport AirportItem = new Airport();
                    for (int i = 0; i < uoGridViewAirport.Rows.Count; i++)
                    {
                        AirportItem = new Airport();

                        AirportItem.AirportSeaportID = GlobalCode.Field2Int(uoGridViewAirport.Rows[i].Cells[0].Text);
                        AirportItem.AirportID = GlobalCode.Field2TinyInt(uoGridViewAirport.Rows[i].Cells[1].Text);
                        AirportItem.AirportCode = uoGridViewAirport.Rows[i].Cells[2].Text;
                        AirportItem.AirportName = uoGridViewAirport.Rows[i].Cells[3].Text;

                        AirportList.Add(AirportItem);
                    }
                    Airport.AirportList = AirportList;
                }
                else
                {
                    list = Airport.AirportList;
                }
            }
            return list;
        }
        /// <summary>
        /// Date Created:      07/03/2012
        /// Created By:        Josephine Gad
        /// (description)      Get Airport not in Seaport        
        /// </summary>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        private List<Airport> AirportNotInSeaport(bool IsNew)
        {
            List<Airport> list = new List<Airport>();
            if (IsNew || Airport.AirportNotInList == null)
            {
                list = MaintenanceViewBLL.GetAirportList(uoHiddenFieldUser.Value,
                uoHiddenFieldRole.Value, GlobalCode.Field2Int(uoHiddenFieldPortID.Value), 0, false);
                Airport.AirportNotInList = list;
            }
            else
            {
                if (Airport.AirportNotInList == null)
                {
                    List<Airport> AirportNotInList = new List<Airport>();
                    Airport AirportItem = new Airport();

                    string[] sAirportNameCode;
                    string sCode;
                    string sName;
                    for (int i = 1; i <= uoDropDownListAiport.Items.Count; i++)
                    {
                        sAirportNameCode = uoDropDownListAiport.Items[i].Text.Split("::".ToCharArray());
                        if (sAirportNameCode.Length > 0)
                        {
                            sCode = sAirportNameCode[2].Trim();
                            sName = sAirportNameCode[0].Trim();

                            AirportItem = new Airport();
                            AirportItem.AirportSeaportID = 0;
                            AirportItem.AirportID = GlobalCode.Field2TinyInt(uoDropDownListAiport.Items[i].Value);
                            AirportItem.AirportCode = sCode;
                            AirportItem.AirportName = sName;

                            AirportNotInList.Add(AirportItem);
                        }
                    }
                    Airport.AirportNotInList = AirportNotInList;
                }
                else
                {
                    list = Airport.AirportNotInList;
                }
            }
            return list;
        }

        #endregion

        #region PageMethods

        [WebMethod]
        public static bool CheckPortCode(string portCode, string portId)
        {
            return PortBLL.CheckPortCode(portCode, Convert.ToInt32(portId));
        }
        #endregion
    }
}

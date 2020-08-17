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
    public partial class SailMasterAdd : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region EVENTS
        /// <summary>
        /// Created By: Charlene Remotigue
        /// Date Created: 11/10/11
        /// Description: Binds regions to dropdownlist
        /// </summary>
        /// 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegion();
                ClearFields();
                SailMasterLogAuditTrail();
                if (Request.QueryString["sId"] != "0")
                {                    
                    LoadSailMasterDetails();
                }                
            }
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountry();
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            uoDropDownListLetters.SelectedValue = "0";
            BindCity();
            uoTextBoxCity.Text = "";
        }

        protected void uoDropDownListLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoTextBoxCity.Text = "";
            BindCity();
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSeaport();
        }

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLetters();
        }

        protected void uoDropDownListSeaport_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            DataTable tempDT = null;

            string strLogDescription;
            string strFunction;

            int SailMasterId = Convert.ToInt32(Request.QueryString["sId"]);
            int vesselId = Convert.ToInt32(Session["VesselID"].ToString());
            int portId = Convert.ToInt32(uoDropDownListSeaport.SelectedValue);
            DateTime ScheduleDate = Convert.ToDateTime(uoTextBoxScheduleDate.Text);
            int DaySeq = Convert.ToInt32(uoTextBoxDaySeq.Text);
            DateTime dateFrom = Convert.ToDateTime(uoTextBoxFrom.Text);
            DateTime dateTo = Convert.ToDateTime(uoTextBoxTo.Text);
            //int RetValue = masterBLL.SailMasterAddMaintenanceSave(SailMasterId, portId, vesselId,
            tempDT = masterBLL.SailMasterAddMaintenanceSave(SailMasterId, portId, vesselId,
                uoTextBoxItinerary.Text, uoTextBoxVoyage.Text, DaySeq, ScheduleDate, dateFrom, dateTo, GlobalCode.Field2String(Session["UserName"]));
            
            //if (RetValue == 0)
            if (Convert.ToInt32(tempDT.Rows[0]["dtReturnValue"]) == 0)
            {
                AlertMessage("Sail Master successfully saved.");
            }
            else
            {
                AlertMessage("Cannot add seaport. Ship is already in a different seaport during those dates.");
            }

            if (Request.QueryString["sId"] == "0" || Request.QueryString["sId"] == null)
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Sail master added.";
                strFunction = "uoButtonSave_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(tempDT.Rows[0]["dtSailMasterID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Sail master updated.";
                strFunction = "uoButtonSave_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(tempDT.Rows[0]["dtSailMasterID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            OpenParentPage();
            
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Date Created: 24/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }

        /// <summary>
        /// Created By: Charlene Remotigue
        /// Date Created: 07/10/11
        /// Description: Binds regions to dropdownlist
        /// </summary>
        /// 
        private void BindRegion()
        {
            DataTable RegionDataTable = null;
            try
            {
                RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListRegion.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListRegion.Items.Add(item);

                uoDropDownListRegion.DataSource = RegionDataTable;
                uoDropDownListRegion.DataTextField = "colRegionNameVarchar";
                uoDropDownListRegion.DataValueField = "colRegionIDInt";
                uoDropDownListRegion.DataBind();

                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds country to dropdownlist
        /// </summary>
        private void BindCountry()
        {
            DataTable CountryDataTable = null;
            try
            {
                ViewState["cityQueryCount"] = 0;
                CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, "");
                uoDropDownListCountry.Items.Clear();
                ListItem item = new ListItem("--Select Country--", "0");
                uoDropDownListCountry.Items.Add(item);
                uoDropDownListCountry.DataSource = CountryDataTable;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: populates dropdownlist with a-z
        /// </summary>
        private void BindLetters()
        {

            int cInt = 65;
            char cLetter = 'A';
            uoDropDownListLetters.Items.Clear();
            ListItem item = new ListItem("--", "0");
            uoDropDownListLetters.Items.Add(item);
            item = new ListItem("+", "+");
            uoDropDownListLetters.Items.Add(item);
            while (cLetter <= 'Z')
            {
                item = new ListItem(cLetter.ToString(), cLetter.ToString());
                uoDropDownListLetters.Items.Add(item);
                cInt += 1;
                cLetter = Convert.ToChar(cInt);
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds city to uodropdownlistcity
        /// </summary>
        private void BindCity()
        {
            DataTable CityDataTable = null;

            try
            {

                CityDataTable = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxCity.Text, uoDropDownListLetters.SelectedValue);
                uoDropDownListCity.Items.Clear();
                ListItem item = new ListItem("--Select City--", "0");

                uoDropDownListCity.Items.Add(item);
                uoDropDownListCity.DataSource = CityDataTable;
                uoDropDownListCity.DataTextField = "colCityNameVarchar";
                uoDropDownListCity.DataValueField = "colCityIDInt";
                uoDropDownListCity.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds city to uodropdownlistcity
        /// </summary>
        private void BindSeaport()
        {
            DataTable SeaportDataTable = null;

            try
            {

                SeaportDataTable = MasterfileBLL.PortListMaintenanceSearch(Convert.ToInt32(uoDropDownListCity.SelectedValue),0, true);
                uoDropDownListSeaport.Items.Clear();
                ListItem item = new ListItem("--Select Seaport--");

                uoDropDownListSeaport.Items.Add(item);
                uoDropDownListSeaport.DataSource = SeaportDataTable;
                uoDropDownListSeaport.DataTextField = "PORT";
                uoDropDownListSeaport.DataValueField = "PORTID";
                uoDropDownListSeaport.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SeaportDataTable != null)
                {
                    SeaportDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: set default values
        /// </summary>
        private void ClearFields()
        {
            uoTextBoxCity.Text = "";
            uoTextBoxFrom.Text = DateTime.Now.ToShortDateString();
            uoTextBoxTo.Text = DateTime.Now.ToShortDateString();
            uoTextBoxVoyage.Text = "";
            uoTextBoxItinerary.Text = "";
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='Javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldSailMaster\").val(\"1\"); ";
            sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: loads sail master details
        /// </summary>
        private void LoadSailMasterDetails()
        {
            IDataReader dr = null;

            try
            {

                dr = masterBLL.SailMasterAddMaintenanceLoadDetails(Convert.ToInt32(Request.QueryString["sId"]));
                if (dr.Read())
                {
                    DateTime ScheduleDate = Convert.ToDateTime(dr["colScheduleDate"]);
                    DateTime dateFrom = Convert.ToDateTime(dr["colScheduleDatetimeFrom"]);
                    DateTime dateTo = Convert.ToDateTime(dr["colScheduleDatetimeTo"]);
                    uoDropDownListRegion.SelectedValue = dr["colMapIDInt"].ToString();
                    BindCountry();
                    uoDropDownListCountry.SelectedValue = dr["colCountryIDInt"].ToString();
                    BindLetters();
                    if (dr["colCityNameVarchar"].ToString() != "")
                    {
                        uoDropDownListLetters.SelectedValue = dr["colCityNameVarchar"].ToString().Remove(1);
                    }
                    BindCity();
                    uoDropDownListCity.SelectedValue = dr["colCityIDInt"].ToString();
                    BindSeaport();
                    uoDropDownListSeaport.SelectedValue = dr["colPortIdInt"].ToString();
                    uoTextBoxItinerary.Text = dr["colItineraryCodeVarchar"].ToString();
                    uoTextBoxVoyage.Text = dr["colVoyageNoVarchar"].ToString();                    
                    uoTextBoxScheduleDate.Text = ScheduleDate.ToShortDateString();
                    uoTextBoxDaySeq.Text = dr["colDayNoTinyint"].ToString();
                    uoTextBoxFrom.Text = dateFrom.ToShortDateString();
                    uoTextBoxTo.Text = dateTo.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void SailMasterLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["sId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for sail master editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for sail master editor clicked.";
            }

            strFunction = "SailMasterLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

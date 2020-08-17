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
    public partial class PortAgentSeaportAdd : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region EVENTS
        /// <summary>
        /// Created By: Charlene Remotigue
        /// Date Created: 10/07/11
        /// </summary>
        /// 
        public int pId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PortAgentSeaportLogAuditTrail();
                pId = Convert.ToInt32(Request.QueryString["pId"]);
                BindRegion();
            }
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown();
            BindCountry();
        }

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLetters();
        }

        protected void uoDropDownListCity2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSeaport();
        }

        protected void uoDropDownListLetter_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity();
        }      

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            pId = Convert.ToInt32(Request.QueryString["pId"]);
            Int32 PortAgentSeaportID = MasterfileBLL.portListMaintenanceInsert(pId, Convert.ToInt32(uoDropDownListSeaport.SelectedValue), GlobalCode.Field2String(Session["UserName"]));

            //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
            strLogDescription = "Service Provider seaport added.";
            strFunction = "uoButtonSave_Click";

            DateTime currentDate = CommonFunctions.GetCurrentDateTime();

            BLL.AuditTrailBLL.InsertLogAuditTrail(PortAgentSeaportID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

            OpenParentPage();
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            BindCity();
            
        }

        #endregion


        #region METHODS
        /// <summary>
        /// Date Created: 05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: clears and initializes dropdown data
        /// </summary>
        private void InitializeDropDown()
        {
            ViewState["cityQueryCount"] = 0;
            uoDropDownListCountry.Items.Clear();
            ListItem item = new ListItem("--Select Country--", "0");
            uoDropDownListCountry.Items.Add(item);
            item = new ListItem("--Select City--", "0");
            uoDropDownListCity2.Items.Clear();
            uoDropDownListCity2.Items.Add(item);
            //uoSeaportList.DataSource = null;
            //uoSeaportList.DataBind();
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


                InitializeDropDown();
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
        /// Date Created: 07/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds country to dropdownlist
        /// </summary>
        private void BindCountry()
        {
            DataTable CountryDataTable = null;
            try
            {
                ViewState["cityQueryCount"] = 0;
                CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, ""); ;
                uoDropDownListCountry.Items.Clear();
                ListItem item = new ListItem("--Select Country--", "0");
                uoDropDownListCountry.Items.Add(item);
                uoDropDownListCountry.DataSource = CountryDataTable;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCity2.Items.Clear();
                item = new ListItem("--Select City--", "0");
                uoDropDownListCity2.Items.Add(item);
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
        /// Date Created: 06/10/2011
        /// Created By: Charlene Remotigue
        /// Description: populates dropdownlist with a-z
        /// </summary>
        private void BindLetters()
        {

            int cInt = 65;
            char cLetter = 'A';
            uoDropDownListLetter.Items.Clear();
            ListItem item = new ListItem("--", "0");
            uoDropDownListLetter.Items.Add(item);
            item = new ListItem("+", "+");
            uoDropDownListLetter.Items.Add(item);
            while (cLetter <= 'Z')
            {
                item = new ListItem(cLetter.ToString(), cLetter.ToString());
                uoDropDownListLetter.Items.Add(item);
                cInt += 1;
                cLetter = Convert.ToChar(cInt);
            }
        }

        /// <summary>
        /// Date Created: 07/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds city to uodropdownlistcity
        /// </summary>
        private void BindCity()
        {
            DataTable CityDataTable = null;

            try
            {

                CityDataTable = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxCityName.Text, uoDropDownListLetter.SelectedValue);
                uoDropDownListCity2.Items.Clear();
                ListItem item = new ListItem("--Select City--", "0");

                uoDropDownListCity2.Items.Add(item);
                uoDropDownListCity2.DataSource = CityDataTable;
                uoDropDownListCity2.DataTextField = "colCityNameVarchar";
                uoDropDownListCity2.DataValueField = "colCityIDInt";
                uoDropDownListCity2.DataBind();

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

            uoTextBoxCityName.Text = "";
        }

        /// <summary>
        /// Date Created: 07/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds city to uodropdownlistcity
        /// </summary>
        private void BindSeaport()
        {
            DataTable SeaportDataTable = null;

            try
            {

                SeaportDataTable = MasterfileBLL.PortListMaintenanceSearch(Int32.Parse(uoDropDownListCity2.SelectedValue), Int32.Parse(Request.QueryString["pId"].ToString()), false);
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
        /// Date Created: 07/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='Javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupSeaPort\").val(\"1\"); ";
            sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void PortAgentSeaportLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            //if (Request.QueryString["pId"].ToString() != "0")
            //{
            //    strLogDescription = "Edit linkbutton for Service Provider seaport editor clicked.";
            //}
            //else
            //{
            //    strLogDescription = "Add button for Service Provider seaport editor clicked.";
            //}

            strLogDescription = "Add button for Service Provider seaport editor clicked.";
            strFunction = "PortAgentSeaportLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

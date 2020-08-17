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
    public partial class PortListMaintenance : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Date Created: 04/10/2011
        /// Created By: Charlene Remotigue
        /// </summary>      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PortAgentPortLogAuditTrail();
                uoHiddenFieldPortAgentId.Value = Request.QueryString["pId"].ToString();
                uoLabelPortName.Text = Request.QueryString["pName"].ToString();
                //BindRegion();
                UpdatePanelSeaportList.Update();
                 Session["strPrevPage"]  = Request.RawUrl;
                //set visibililty
                if (MUser.GetUserRole() == TravelMartVariable.RolePortSpecialist)
                {
                    uoHyperLinkPortAgentSeaportAdd.Visible = false;
                    uoBtnPortAgentSeaportAdd.Visible = false;
                }
            }
            uoHyperLinkPortAgentSeaportAdd.HRef = "PortAgentSeaportAdd.aspx?pid=" + uoHiddenFieldPortAgentId.Value;
        }

        protected void uoSeaportList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                uoHiddenFielPortAgentSeaportId.Value = e.CommandArgument.ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                MasterfileBLL.portListMaintenanceDelete(Convert.ToInt32(e.CommandArgument), GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Service Provider seaport deleted. (flagged as inactive)";
                strFunction = "uoSeaportList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(uoHiddenFielPortAgentSeaportId.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));                
            }
        }

        protected void uoSeaportList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoSeaportListPager_PreRender(object sender, EventArgs e)
        {            
                BindPortAgentSeaport();
        }
        #endregion

        
        #region METHODS
        /// <summary>
        /// Date Created: 04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds Service Provider seaport to listview
        /// </summary>
        private void BindPortAgentSeaport()
        {
            DataTable AgentSeaportDataTable = null;
            try
            {
                uoHiddenFieldCityId.Value = "-1";
                if ( Session["City"] == null ||  Session["City"] .ToString() == "0")
                {
                    uoHiddenFieldCityId.Value = "-1";
                }
                else
                {
                    uoHiddenFieldCityId.Value =  Session["City"] .ToString();
                }
                uoSeaportList.DataSource = null;
                uoSeaportList.DataSourceID = "ObjectDataSource1";

              
                //AgentSeaportDataTable =
                //    MasterfileBLL.PortListMaintenanceSearchAgentSeaport(Convert.ToInt32(ViewState["PortId"]), CityId);

                //uoSeaportList.Items.Clear();
                //uoSeaportList.DataSource = AgentSeaportDataTable;
                //uoSeaportList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (AgentSeaportDataTable != null)
                {
                    AgentSeaportDataTable.Dispose();
                }
            }
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void PortAgentPortLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            strLogDescription = "Service Provider seaport information viewed.";
            strFunction = "PortAgentPortLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion

        #region Deleted/ hidden
        //protected void uoDropDownListLetter_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindCity();
        //}
        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    InitializeDropDown();
        //    BindCountry();
        //}

        //protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindLetters();
        //}
        //protected void uoDropDownListCity2_SelectedIndexChanged(object sender, EventArgs e)
        //{


        //}

        //protected void uoButtonSearch_Click(object sender, EventArgs e)
        //{
        //    uoDropDownListCity2.SelectedIndex = 0;
        //    BindCity();
        //}
        ///// <summary>
        ///// Date Created: 05/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: binds all regions to uodropdownlistregion 
        ///// </summary>
        //private void BindRegion()
        //{
        //    DataTable RegionDataTable = null;
        //    try
        //    {
        //        RegionDataTable = MasterfileBLL.regionViewMasterfileSearch("");
        //        uoDropDownListRegion.Items.Clear();
        //        ListItem item = new ListItem("--Select Region--", "0");
        //        uoDropDownListRegion.Items.Add(item);

        //        uoDropDownListRegion.DataSource = RegionDataTable;
        //        uoDropDownListRegion.DataTextField = "colMapNameVarchar";
        //        uoDropDownListRegion.DataValueField = "colMapIDInt";
        //        uoDropDownListRegion.DataBind();


        //        InitializeDropDown();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (RegionDataTable != null)
        //        {
        //            RegionDataTable.Dispose();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Date Created: 05/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: clears and initializes dropdown data
        ///// </summary>
        //private void InitializeDropDown()
        //{
        //    ViewState["cityQueryCount"] = 0;
        //    uoDropDownListCountry.Items.Clear();
        //    ListItem item = new ListItem("--Select Country--", "0");
        //    uoDropDownListCountry.Items.Add(item);
        //    item = new ListItem("--Select City--", "0");
        //    uoDropDownListCity2.Items.Clear();
        //    uoDropDownListCity2.Items.Add(item);

        //}

        ///// <summary>
        ///// Date Created: 05/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: binds country to uodropdownlistcountry
        ///// </summary>
        //private void BindCountry()
        //{
        //    DataTable CountryDataTable = null;
        //    try
        //    {
        //        ViewState["cityQueryCount"] = 0;
        //        CountryDataTable = MasterfileBLL.GetSearchCountryListByRegion(uoDropDownListRegion.SelectedValue, "");
        //        uoDropDownListCountry.Items.Clear();
        //        ListItem item = new ListItem("--Select Country--", "0");
        //        uoDropDownListCountry.Items.Add(item);
        //        uoDropDownListCountry.DataSource = CountryDataTable;
        //        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
        //        uoDropDownListCountry.DataValueField = "colCountryIDInt";
        //        uoDropDownListCountry.DataBind();
        //        uoDropDownListCity2.Items.Clear();
        //        item = new ListItem("--Select City--", "0");
        //        uoDropDownListCity2.Items.Add(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (CountryDataTable != null)
        //        {
        //            CountryDataTable.Dispose();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Date Created: 04/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: binds city to uodropdownlistcity
        ///// </summary>
        //private void BindCity()
        //{
        //    DataTable CityDataTable = null;

        //    try
        //    {

        //        CityDataTable = MasterfileBLL.cityViewMasterfileSearch(uoDropDownListCountry.SelectedValue, uoTextBoxCityName.Text, uoDropDownListLetter.SelectedValue);
        //        uoDropDownListCity2.Items.Clear();
        //        ListItem item = new ListItem("--Select City--", "0");

        //        uoDropDownListCity2.Items.Add(item);
        //        uoDropDownListCity2.DataSource = CityDataTable;
        //        uoDropDownListCity2.DataTextField = "colCityNameVarchar";
        //        uoDropDownListCity2.DataValueField = "colCityIDInt";
        //        uoDropDownListCity2.DataBind();


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (CityDataTable != null)
        //        {
        //            CityDataTable.Dispose();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Date Created: 06/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: populates dropdownlist with a-z
        ///// </summary>
        //private void BindLetters()
        //{

        //    int cInt = 65;
        //    char cLetter = 'A';
        //    uoDropDownListLetter.Items.Clear();
        //    ListItem item = new ListItem("--", "0");
        //    uoDropDownListLetter.Items.Add(item);
        //    item = new ListItem("+", "+");
        //    uoDropDownListLetter.Items.Add(item);
        //    while (cLetter <= 'Z')
        //    {
        //        item = new ListItem(cLetter.ToString(), cLetter.ToString());
        //        uoDropDownListLetter.Items.Add(item);
        //        cInt += 1;
        //        cLetter = Convert.ToChar(cInt);
        //    }
        //}

        #endregion
     
      

       

        

        

      

        

       

        

       

    }
}

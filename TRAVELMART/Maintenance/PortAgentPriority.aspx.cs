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
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class PortAgentPriority : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                BindBrand();
                BindRegionList();
                BindAirport();
                GetPortAgentList();
                if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                {
                    uoHiddenFieldContractList.Value = "true";
                    uoHiddenFieldVendor.Value = "false";

                }

            }

            //uoHiddenFieldVendor
        }
        protected void uoListViewPortAgent_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {

        }
        protected void uoListViewPortAgent_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
            {
                uoHiddenFieldContractList.Value = "true";
                uoHiddenFieldVendor.Value = "false";
            }
        }
        protected void uoListViewPortAgent_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator))
            {
                uoHiddenFieldContractList.Value = "true";
                uoHiddenFieldVendor.Value = "false";
            }
        }
        protected void uoBtnPortAgentAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PortAgentMaintenance.aspx?vmId=0&vmType=VE&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
        protected void uoListViewPortAgentPager_PreRender(object sender, EventArgs e)
        {
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAirport();
        }
        #endregion

        #region Function
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetPortAgentList();
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get the list of Service Provider vendors    
        /// ----------------------------------------------        
        /// </summary>
        private void GetPortAgentList()
        {
            uoListViewPortAgent.DataSourceID = "uoObjectDataSourcePortAgent";
            uoListViewPortAgent.DataBind();
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
        /// Modified by:        Josephine Gad
        /// Modified Created:   25/05/2012
        /// Description:        Use session to get region list; use RegionListByUser if session is null
        /// </summary>
        private void BindRegionList()
        {
            List<RegionList> list = new List<RegionList>();
            try
            {
                if (Session["HotelDashboardDTO_RegionList"] != null)
                {
                    //Session["HotelDashboardDTO_RegionList"] = HotelDashboardDTO.RegionList;
                    list = (List<RegionList>)Session["HotelDashboardDTO_RegionList"];
                }
                else
                {
                    list = CountryBLL.RegionListByUser(uoHiddenFieldUser.Value);
                    Session["HotelDashboardDTO_RegionList"] = list;
                }
                if (list.Count > 0)
                {
                    uoDropDownListRegion.Items.Clear();
                    uoDropDownListRegion.DataSource = list;
                    uoDropDownListRegion.DataTextField = "RegionName";
                    uoDropDownListRegion.DataValueField = "RegionId";
                    uoDropDownListRegion.DataBind();
                }
                uoDropDownListRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));
                uoDropDownListRegion.Items.Insert(0, new ListItem("--No Region--", "-1"));


                string sRegion = GlobalCode.Field2String(Session["Region"]);
                if (sRegion != "")
                {
                    if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
                    {
                        uoDropDownListRegion.SelectedValue = sRegion;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport By Region
        /// -----------------------------------
        /// </summary>
        private void BindAirport()
        {
            //DataTable dt = null;
            try
            {
                //dt = AirBLL.GetAirportByCountry("0", uoDropDownListRegion.SelectedValue, "", "0");
                List<AirportDTO> list = new List<AirportDTO>();
                list = AirBLL.GetAirportByRegion(uoDropDownListRegion.SelectedValue, "");
                uoDropDownListAirport.Items.Clear();
                               
                uoDropDownListAirport.Items.Insert(0, new ListItem("--SELECT AIRPORT--", "0"));

                if (list.Count > 0)
                {
                    uoDropDownListAirport.DataSource = list;
                    uoDropDownListAirport.DataValueField = "AirportIDString";
                    uoDropDownListAirport.DataTextField = "AirportNameString";

                }
                uoDropDownListAirport.DataBind();                
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Brand
        /// -----------------------------------
        /// </summary>
        private void BindBrand()
        {
            try
            {
                MasterfileBLL BLL = new MasterfileBLL();
                List<BrandList> list = new List<BrandList>();
                list = BLL.GetBrandList();
                uoDropDownListBrand.Items.Clear();

                uoDropDownListBrand.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

                if (list.Count > 0)
                {
                    uoDropDownListBrand.DataSource = list;
                    uoDropDownListBrand.DataValueField = "BrandID";
                    uoDropDownListBrand.DataTextField = "BrandName";

                }
                uoDropDownListBrand.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        

        
    }
}

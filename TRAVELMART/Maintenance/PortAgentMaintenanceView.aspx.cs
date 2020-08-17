using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class PortAgentMaintenanceView : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  05/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   
        /// -------------------------------------------
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
                                               
                GetPortAgentList();

                if (User.IsInRole(TravelMartVariable.RolePortSpecialist))
                {
                    uoHiddenFieldRole.Value = TravelMartVariable.RolePortSpecialist;
                }

                if (User.IsInRole(TravelMartVariable.RoleAdministrator))                   
                {
                    uoBtnPortAgentAdd.Visible = true;
                    //uoTableSearch.Visible = true;
                }
                else
                {
                    uoBtnPortAgentAdd.Visible = false;
                    uoHiddenFieldVendor.Value = "false";
                    if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                    {
                        uoHiddenFieldContractList.Value = "true";
                    }                    
                }
                BindRegionList();
                BindPortList();
            }
            else
            {

                if (uoHiddenFieldPopupPortAgent.Value == "1")
                {
                    GetPortAgentList();
                }
                uoHiddenFieldPopupPortAgent.Value = "0";
            }
        }

        protected void uoListViewPortAgent_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = GlobalCode.Field2Int(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeleteVehicleVendor(index, uoHiddenFieldUser.Value);

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle vendor branch deleted. (flagged as inactive)";
                strFunction = "uoListViewPortAgent_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);

                GetPortAgentList();
            }
            else if (e.CommandName != "")
            {
                uoHiddenFieldSortBy.Value = e.CommandName;
                GetPortAgentList();
            }
            else
            {
                uoHiddenFieldSortBy.Value = "SortByName";
            }

        }

        protected void uoListViewPortAgent_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }

        protected void uoListViewPortAgentPager_PreRender(object sender, EventArgs e)
        {
        }

        //protected void uoObjectDataSourcePortAgent_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        //{
        //    e.InputParameters["sPortAgentVendor"] = uoTextBoxSearchParam.Text;
        //}
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetPortAgentList();
        }
        protected void uoBtnPortAgentAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("PortAgentMaintenance.aspx?vmId=0&vmType=VE&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPortList();
        }
        protected void uoButtonViewBranch_Click(object sender, EventArgs e)
        {
            GetPortAgentList();
        }
        protected void uoButtonFilterSeaport_Click(object sender, EventArgs e)
        {
            BindPortList();
        }
        #endregion

        #region Functions
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
        /// Date Created:   06/07/2012
        /// Created By:     Josephine Gad
        /// (description)   Load Port List using session
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegion(uoHiddenFieldUser.Value, uoDropDownListRegion.SelectedValue, "", uoTextBoxSearchSeaport.Text.Trim());

                uoDropDownListSeaport.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListSeaport.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListSeaport.DataSource = list;
                    uoDropDownListSeaport.DataTextField = "PORTName";
                    uoDropDownListSeaport.DataValueField = "PORTID";
                    uoDropDownListSeaport.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListSeaport.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListSeaport.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                        }
                    }
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
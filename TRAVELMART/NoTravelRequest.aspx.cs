using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.Office.Interop;

using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Text;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class NoTravelRequest : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = GlobalCode.Field2String(Session["UserName"]);
            if (userName == "")
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["dt"] != null)
                {
                    if (Request.QueryString["dt"].ToString() != "")
                    {
                        uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                        uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                    }                   
                }
                Session["strPrevPage"] = Request.RawUrl;
                string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                string userRole = UserRolePrimary;

                uoHiddenFieldUserRole.Value = userRole;//MUser.GetUserRole();
                uoHiddenFieldUser.Value = userName;//Session["UserName"].ToString();                

                //SetDefaultValues();
                GetVessel();
                GetNationality();
                GetGender();
                GetRank();

                if (userRole == TravelMartVariable.RoleHotelVendor || userRole == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }                
                HtmlControl uoRowDateTo = (HtmlControl)Master.FindControl("uoRowDateTo");
                uoRowDateTo.Visible = false;
            }
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";

            uoObjectDataSourceTR.SelectParameters["LoadType"].DefaultValue = uoHiddenFieldLoadType.Value;
            uoObjectDataSourceTR.SelectParameters["FromDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceTR.SelectParameters["ToDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceTR.SelectParameters["UserID"].DefaultValue = uoHiddenFieldUser.Value;
            uoObjectDataSourceTR.SelectParameters["Role"].DefaultValue = uoHiddenFieldUserRole.Value;
            uoObjectDataSourceTR.SelectParameters["OrderBy"].DefaultValue = uoHiddenFieldOrderBy.Value;

            uoObjectDataSourceTR.SelectParameters["VesselID"].DefaultValue = uoDropDownListVessel.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["FilterByName"].DefaultValue = uoDropDownListFilterBy.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["SeafarerID"].DefaultValue = uoTextBoxFilter.Text;

            uoObjectDataSourceTR.SelectParameters["NationalityID"].DefaultValue = uoDropDownListNationality.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["Gender"].DefaultValue = uoDropDownListGender.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["RankID"].DefaultValue = uoDropDownListRank.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["Status"].DefaultValue = uoDropDownListStatus.SelectedValue;

            uoObjectDataSourceTR.SelectParameters["RegionID"].DefaultValue = GlobalCode.Field2String(Session["Region"]);
            uoObjectDataSourceTR.SelectParameters["CountryID"].DefaultValue = GlobalCode.Field2String(Session["Country"]);
            uoObjectDataSourceTR.SelectParameters["CityID"].DefaultValue = GlobalCode.Field2String(Session["City"]);
            uoObjectDataSourceTR.SelectParameters["PortID"].DefaultValue = GlobalCode.Field2String(Session["Port"]);
            
            GetNoTravelRequestList();
        }
        protected void uoListViewTRPager_PreRender(object sender, EventArgs e)
        {           
            
        }
        protected void uoObjectDataSourceTR_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["LoadType"] = GlobalCode.Field2String(uoHiddenFieldLoadType.Value);
            e.InputParameters["FromDate"] = uoHiddenFieldDate.Value;
            e.InputParameters["ToDate"] = uoHiddenFieldDate.Value;
            e.InputParameters["UserID"] = uoHiddenFieldUser.Value;
            e.InputParameters["Role"] = uoHiddenFieldUserRole.Value;
            e.InputParameters["OrderBy"] = uoHiddenFieldOrderBy.Value;

            e.InputParameters["VesselID"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["FilterByName"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["SeafarerID"] = uoTextBoxFilter.Text;

            e.InputParameters["NationalityID"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["RankID"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;

            e.InputParameters["RegionID"] =  GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["CountryID"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["CityID"] = GlobalCode.Field2String(Session["City"]);
            e.InputParameters["PortID"] = GlobalCode.Field2String(Session["Port"]);
        }
        protected void uoListViewTR_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName.ToString();
            if (e.CommandName != "")
            {
                GetNoTravelRequestList();
            }
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                VesselDataTable = VesselBLL.GetVessel(uoHiddenFieldUser.Value, uoHiddenFieldDate.Value,
                    uoHiddenFieldDate.Value, GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldUserRole.Value);
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Ship--", "0");
                uoDropDownListVessel.Items.Add(item);
                uoDropDownListVessel.DataSource = VesselDataTable;
                uoDropDownListVessel.DataTextField = "VesselName";
                uoDropDownListVessel.DataValueField = "VesselID";
                uoDropDownListVessel.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VesselDataTable != null)
                {
                    VesselDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   09/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Refresh No Travel Request List
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetNoTravelRequestList()
        {
            try
            {

                uoListViewTR.DataSource = null;
                uoListViewTR.DataSourceID = "uoObjectDataSourceTR";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of nationality
        /// </summary>
        private void GetNationality()
        {
            DataTable dt = null;
            try
            {
                dt = MasterfileBLL.GetReference("Nationality");
                ListItem item = new ListItem("--Select Nationality--", "0");
                uoDropDownListNationality.Items.Clear();
                uoDropDownListNationality.Items.Add(item);
                uoDropDownListNationality.DataSource = dt;
                uoDropDownListNationality.DataTextField = "RefName";
                uoDropDownListNationality.DataValueField = "RefID";
                uoDropDownListNationality.DataBind();
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
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of gender
        /// </summary>
        private void GetGender()
        {
            DataTable dt = null;
            try
            {
                dt = MasterfileBLL.GetReference("Gender");
                ListItem item = new ListItem("--Select Gender--", "0");
                uoDropDownListGender.Items.Clear();
                uoDropDownListGender.Items.Add(item);
                uoDropDownListGender.DataSource = dt;
                uoDropDownListGender.DataTextField = "RefName";
                uoDropDownListGender.DataValueField = "RefID";
                uoDropDownListGender.DataBind();
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
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of rank
        /// </summary>
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetRankByVessel(uoDropDownListVessel.SelectedValue);
                ListItem item = new ListItem("--Select Rank--", "0");
                uoDropDownListRank.Items.Clear();
                uoDropDownListRank.Items.Add(item);
                uoDropDownListRank.DataSource = dt;
                uoDropDownListRank.DataTextField = "RankName";
                uoDropDownListRank.DataValueField = "RankID";
                uoDropDownListRank.DataBind();
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
        /// Date Created:   29/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get the directory of image status            
        /// <summary>
        protected string GetStatusImage(object StatusObj)
        {
            string ImageString = "~/Images/";
            string StatusString = StatusObj.ToString();
            StatusString = StatusString.ToLower();

            switch (StatusString)
            {
                case "ok":
                    ImageString += "positive.png";
                    break;
                case "notok":
                    ImageString += "positive-red.png";
                    break;
                case "none":
                    ImageString += "neutral.png";
                    break;
                default:
                    ImageString += "neutral.png";
                    break;
            }
            return ImageString;
        }
        /// <summary>
        /// Date Created:   20/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Set No Travel Request groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string TRAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Ship";
            string GroupValueString = "Vessel";

            string currentDataFieldValue = Eval("VesselCode").ToString() + " - " + Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                if (Eval("IsWithSail").ToString() == "True")
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"8\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
                else
                {
                    return string.Format("<tr><td class=\"groupRed\" colspan=\"15\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        #endregion

        
    }
}

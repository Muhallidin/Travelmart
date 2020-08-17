using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART
{
    public partial class HotelLockedManifest : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       02/Jan/2013
        /// Description:        Load Page for Locked Manifest
        /// ------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldUserRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                BindRegionList();
                BindPortList();
                GetHotelFilter();
                LoadTravelDetails();
                BindManifestType();
                uoHiddenFieldLoadType.Value = "1";
            }
            else
            { 
                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
                if (uoHiddenFieldPopupCalendar.Value == "1" || Request.QueryString["chDate"] != null)
                {
                    //BindRegionList();
                    //BindPortList();
                    //GetHotelFilter();
                    //BindManifestType();
                    uoHiddenFieldFromDefaultView.Value = "1";
                    GetSFHotelTravelDetails();
                }
            }
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = "";
            Session.Remove("Port"); // remove the current selected Port 05/07/2012
            BindPortList();
            GetHotelFilter();
        }

        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            GetHotelFilter();

        }
        //protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //}
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            if (uoTextBoxFrom.Text.Trim() != "")
            {
                uoHiddenFieldDate.Value = uoTextBoxFrom.Text;
            }
            else
            {
                uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            }
            uoHiddenFieldFromDefaultView.Value = "1";
            
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;

            GetSFHotelTravelDetails();
        }

        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["UserID"] = uoHiddenFieldUser.Value;
            e.InputParameters["iLoadType"] = GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value);

            e.InputParameters["sDateFrom"] = uoHiddenFieldDate.Value;
            e.InputParameters["sDateTo"] = uoTextBoxTo.Text.Trim();

            e.InputParameters["iManifestTypeID"] = GlobalCode.Field2TinyInt(uoDropDownListHours.SelectedValue);

            e.InputParameters["iRegion"] = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue); 
            e.InputParameters["iPort"] = GlobalCode.Field2Int(uoDropDownListPortPerRegion.SelectedValue);
            e.InputParameters["iBranch"] = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue); 
            
            e.InputParameters["FromDefaultView"] = GlobalCode.Field2TinyInt(uoHiddenFieldFromDefaultView.Value);
        }
        protected void uoListViewManifestPager_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoListViewManifest_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                HiddenField uoHiddenFieldManifestTypeID = (HiddenField)e.Item.FindControl("uoHiddenFieldManifestTypeID");
                HiddenField uoHiddenFieldBranchID = (HiddenField)e.Item.FindControl("uoHiddenFieldBranchID");
                Label uoLabelDate = (Label)e.Item.FindControl("uoLabelDate");

                int iManifestTypeID = GlobalCode.Field2Int(uoHiddenFieldManifestTypeID.Value);
                int iBranchID = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
                string sDate = GlobalCode.Field2Date(uoLabelDate.Text);

                Session["ManifestHrs"] = iManifestTypeID;
                Session["Hotel"] = iBranchID;
                Session["DateFrom"] = sDate;
                string URLString = "HotelManifest3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + sDate + "&chDate=1";
                Response.Redirect(URLString);
            }
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       02/Jan/2013
        /// Description:        Set default values
        /// ------------------------------------
        /// </summary>
        protected void InitializeValues()
        {
            string sUserName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }
            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            Session["strPrevPage"] = Request.RawUrl;

            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       20/Dec/2012
        /// Description:        Bind Region List
        /// ------------------------------------
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
        /// Author:             Josephine Gad
        /// Date Created:       20/Dec/2012
        /// Description:        Bind Port List
        /// ------------------------------------
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

                uoDropDownListPortPerRegion.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListPortPerRegion.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPortPerRegion.DataSource = list;
                    uoDropDownListPortPerRegion.DataTextField = "PORTName";
                    uoDropDownListPortPerRegion.DataValueField = "PORTID";
                    uoDropDownListPortPerRegion.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListPortPerRegion.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListPortPerRegion.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Author:             Josephine Gad
        /// Date Created:       02/Jan/2013
        /// Description:        Load Manifest Type in Dropdown filter
        /// ------------------------------------
        private void BindManifestType()
        {
            List<ManifestClass> list = (List<ManifestClass>)Session["ManifestClass"];

            uoDropDownListHours.Items.Clear();
            if (list!= null)
            {
                uoDropDownListHours.DataSource = list;
                uoDropDownListHours.DataTextField = "ManifestName";
                uoDropDownListHours.DataValueField = "ManifestType";
            }
            uoDropDownListHours.DataBind();
            uoDropDownListHours.Items.Insert(0, new ListItem("--Select Manifest Type", "0"));
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  20/Dec/2012
        /// Description:    Get Hotel list to filter
        /// ----------------------------------------------
        /// </summary>
        private void GetHotelFilter()
        {
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["Region"]),
                    Session["Port"] == null ? "0" : Session["Port"].ToString(), "0", "0");

                int iRowCount = list.Count;
                if (iRowCount == 1)
                {
                    Session["Hotel"] = list[0].HotelIDString;//dt.Rows[0]["BranchID"].ToString();
                }
                if (iRowCount > 0)
                {
                    uoDropDownListHotel.Items.Clear();
                    uoDropDownListHotel.DataSource = list;
                    uoDropDownListHotel.DataTextField = "HotelNameString";
                    uoDropDownListHotel.DataValueField = "HotelIDString";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    uoDropDownListHotel.SelectedValue = "0";

                    if (GlobalCode.Field2Int(Session["Region"]) > 0)
                    {
                        if (uoDropDownListHotel.Items.FindByValue("-1") == null)
                        {
                            uoDropDownListHotel.Items.Insert(1, new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                    else
                    {
                        if (uoDropDownListHotel.Items.FindByValue("-1") != null)
                        {
                            uoDropDownListHotel.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                    
                    uoDropDownListHotel.Enabled = true;

                    if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                    {
                        uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                    }
                    Session["Hotel"] = uoDropDownListHotel.SelectedValue;
                    Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;
                    //LoadTravelDetails();
                }
                else
                {
                    uoDropDownListHotel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       02/Jan/2013
        /// Description:        Get Default values of Locked Manifest list
        /// ------------------------------------
        /// </summary>
        private void LoadTravelDetails()
        {
            LockedManifestBLL.LoadLockedManifestSummary(uoHiddenFieldUser.Value, 0,
                uoHiddenFieldDate.Value, uoTextBoxTo.Text.Trim(),
                GlobalCode.Field2TinyInt(uoDropDownListHours.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListPortPerRegion.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue),
                1, 0, 50);

            uoHiddenFieldFromDefaultView.Value = "0";
            GetSFHotelTravelDetails();
        }
        private void GetSFHotelTravelDetails()
        {
            try
            {
                uoListViewManifest.DataSource = null;
                uoListViewManifest.DataSourceID = "uoObjectDataSourceManifest";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion              
    }
}

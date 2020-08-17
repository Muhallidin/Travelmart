using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Drawing;
using System.Web.Security;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TRAVELMART
{
    public partial class TravelMartHotel : System.Web.UI.MasterPage
    {
        #region "Events"    
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// ------------------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["ufn"] != null)
                {
                    //if (Request.QueryString["dt"] != null)
                    //{
                    //    Session["DateTo"] = GlobalCode.Field2String(Session["DateFrom"]); 
                    //}

                    Session["DateFrom"] = Request.QueryString["dt"]; //gelo
                    Session["DateFrom"] = GlobalCode.Field2String(Session["DateFrom"]).Replace("_", "/");
                    //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl; //gelo

                    if (GlobalCode.Field2String(Session["UserName"]) == "")
                    { 
                    Session["UserName"] = MUser.GetUserName();
                    }
                    string userName = GlobalCode.Field2String(Session["UserName"]);
                    //string userRole = MUser.GetUserRole();
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                    string userRole = UserRolePrimary;
                    Session["UserRole"] = UserRolePrimary;
                    uoHiddenFieldRole.Value = userRole;
                    uclabelFName.Text = Request.QueryString["ufn"];

                    if (GlobalCode.Field2String(Session["DateFrom"]) == "")
                    {
                        string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
                        uoCalendarDashboard.SelectedDate = DateTime.Now;
                        uoCalendarDashboard.VisibleDate = DateTime.Now;
                        Session["DateFrom"] = currentDate;
                    }
                    else
                    {
                        uoHiddenFieldDateFrom.Value = GlobalCode.Field2String(Session["DateFrom"]);
                        //uoHiddenFieldDateFrom.Value = GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])).ToString();

                        uoCalendarDashboard.SelectedDate = GlobalCode.Field2DateTime(Session["DateFrom"]);
                        uoCalendarDashboard.VisibleDate = GlobalCode.Field2DateTime(Session["DateFrom"]);
                        //uoCalendarDashboard.SelectedDate = Convert.ToDateTime(DateTime.Parse(GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])).ToString()).ToString("MM/dd/yyyy"));
                        //uoCalendarDashboard.VisibleDate = Convert.ToDateTime(DateTime.Parse(GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])).ToString()).ToString("MM/dd/yyyy")); 
                    }
                    uoHiddenFieldViewRegion.Value = GlobalCode.Field2String(Session["ViewRegion"]);
                    uoHiddenFieldViewCountry.Value = GlobalCode.Field2String(Session["ViewCountry"]);
                    uoHiddenFieldViewCity.Value = GlobalCode.Field2String(Session["ViewCity"]);
                    uoHiddenFieldViewHotel.Value = GlobalCode.Field2String(Session["ViewHotel"]);
                    uoHiddenFieldViewPort.Value = GlobalCode.Field2String(Session["ViewPort"]);
                    uoHiddenFieldViewLegend.Value = GlobalCode.Field2String(Session["ViewLegend"]);
                    uoHiddenFieldFilter.Value = GlobalCode.Field2String(Session["ViewFilter"]);
                    uoHiddenFieldDashboard.Value =GlobalCode.Field2String(Session["iewDashboard"]);
                    uoHiddenFieldDashboard2.Value = GlobalCode.Field2String(Session["ViewDashboard2"]);

                    uoHiddenFieldHotelID.Value = GlobalCode.Field2String(Session["Hotel"]);

                    if (uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelVendor && uoHiddenFieldRole.Value != TravelMartVariable.RoleVehicleVendor)
                    {
                        uoTextBoxCountryName.Text = GlobalCode.Field2String(Session["CountryName"]);
                        uoTextBoxCityName.Text = GlobalCode.Field2String(Session["CityName"]);

                        BindRegion();
                        BindCountry();
                        BindCity();
                        BindPort();
                        BindHotel();
                    }
                    ShowHideFilters();
                    SetValues();
                    uoHiddenFieldCurrentPage.Value = Page.AppRelativeVirtualPath;
                    RoleSettings();

                    if (GlobalCode.Field2String(Session["Hotel"]) == "" || GlobalCode.Field2String(Session["Hotel"]) == "0")
                    {
                        uoTableLeftMenu.Visible = false;
                    }
                    else
                    {
                        uoTableLeftMenu.Visible = true;
                    }
                }
            }
           
            //Literal ucLiteralMenu = (Literal)TravelmartMenuLinks1.FindControl("ucLiteralMenu");
            //if (ucLiteralMenu.Text.Contains("<li>"))
            //{
            //    uoRowLinkMenu.Visible = true;
            //}
            //else
            //{
            //    uoRowLinkMenu.Visible = false;
            //}

           
        }
        protected void uoCalendarDashboard_DayRender(object sender, DayRenderEventArgs e)
        {
            if (uoCalendarDashboard.Visible)
            {
                DateTime day = e.Day.Date;
                List<ManifestCalendar> calList = null;
                //IDataReader dr = null;
                //DataTable dtCurrentDate = null;
                try
                {
                    
                    if (uoHiddenFieldCurrentPage.Value == "~/Hotel/HotelManifest2.aspx")
                    {
                        calList = GetCalendarTable();

                        //Get rows as enumerable
                        //EnumerableRowCollection<DataRow> dateRows = //dt.AsEnumerable();

                        //Filter Rows on collection
                        var filteredRow = (from a in calList
                                           where a.colDate.Equals(day) //a.Field<DateTime>("colDate").Equals(day)
                                           select a).ToList();

                        //Recover the resulting datatable
                        //dtCurrentDate = filteredRow.CopyToDataTable();

                        //if (dtCurrentDate.Rows[0]["TotalCount"].ToString() != "0")
                        //if (filteredRow[0]["TotalCount"].ToString() != "0")
                        if (filteredRow[0].TotalCount.ToString() != "0")
                        {                        
                            Label lblTotalCount = new Label();
                            lblTotalCount.Text = "<br/>Count:" + filteredRow[0].TotalCount.ToString() + "<br/>";
                            lblTotalCount.ForeColor = Color.Red;
                            lblTotalCount.Font.Size = 8;
                            e.Cell.Controls.Add(lblTotalCount);
                        }                  
                    }
                    else
                    {
                        calList = GetCalendarTable();
                        
                        //Get rows as enumerable
                        //EnumerableRowCollection<DataRow> dateRows = dt.AsEnumerable();

                        //Filter Rows on collection
                        var filteredRow = (from a in calList
                                           where a.colDate.Equals(day) //a.Field<DateTime>("colDate").Equals(day)
                                           select a).ToList();

                        //Recover the resulting datatable
                        //dtCurrentDate = filteredRow.CopyToDataTable();

                        if (filteredRow[0].TotalCount.ToString() != "0")
                        {
                            Label lblTotalCount = new Label();
                            lblTotalCount.Text = "<br/>Count:" + filteredRow[0].TotalCount.ToString() + "<br/>";
                            lblTotalCount.ForeColor = Color.Red;
                            lblTotalCount.Font.Size = 8;
                            e.Cell.Controls.Add(lblTotalCount);
                        }                                               
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {                   
                    //if (dt != null)
                    //{
                    //    dt.Dispose();
                    //}                    
                }
            }
        }        
        /// <summary>        
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use dSelectedDate
        ///                    to avoid error in date conversion        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoCalendarDashboard_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dSelectedDate = uoCalendarDashboard.SelectedDate;
            //uoTextBoxFrom.Text = dSelectedDate.ToString("dd-MMM-yyyy");
            uoHiddenFieldHotelID.Value = GlobalCode.Field2String(Session["Hotel"]);

            //PostBackMasterPage("Manifest.aspx?ufn=" + Request.QueryString["ufn"].ToString());

            SetValues();

            string sURL = GlobalCode.Field2String(Session["strPrevPage"]);
            sURL = sURL.Replace("%2f", "/");
           
            if (Request.QueryString["dt"] != null)
            {
                sURL = sURL.Replace(Request.QueryString["dt"].ToString(), dSelectedDate.ToString("MM/dd/yyyy"));
            }
            else
            {
                sURL = sURL + "&dt=" + dSelectedDate.ToString("MM/dd/yyyy");
            }

            //PostBackMasterPage(GlobalCode.Field2String(Session["strPrevPage"]));
            PostBackMasterPage(sURL);
        }         
        protected void uoButtonDateRange_Click(object sender, EventArgs e)
        {
            PostBackMasterPage(GlobalCode.Field2String(Session["strPrevPage"])); 
        }
        protected void uoCalendarDashboard_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            uoHiddenFieldDateFrom.Value = e.NewDate.ToString("MM/dd/yyyy");
            Session.Remove("TentativeManifestCalendarDashboard");
            GetCalendarTable();
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            mUser.LastActivityDate = DateTime.Now.AddMinutes(-15);
            Membership.UpdateUser(mUser);

            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
        #endregion

        #region "Functions"
        private void PostBackMasterPage(string sPage)
        {
            Session["ViewRegion"] = uoHiddenFieldViewRegion.Value;
            Session["ViewCountry"] = uoHiddenFieldViewCountry.Value;
            Session["ViewCity"] = uoHiddenFieldViewCity.Value;
            Session["ViewHotel"] = uoHiddenFieldViewHotel.Value;
            Session["ViewPort"] = uoHiddenFieldViewPort.Value;
            Session["ViewLegend"] = uoHiddenFieldViewLegend.Value;
            Session["ViewFilter"] = uoHiddenFieldFilter.Value;
            Session["ViewDashboard"] = uoHiddenFieldDashboard.Value;            
            Session["ViewDashboard2"] = uoHiddenFieldDashboard2.Value;

            Session["Region"] = uoHiddenFieldRegionID.Value;
            Session["Country"] = uoHiddenFieldCountryID.Value;
            Session["City"] = uoHiddenFieldCityID.Value;
            Session["Port"] = uoHiddenFieldPortID.Value;
            Session["Hotel"] = uoHiddenFieldHotelID.Value;
            Session["Vehicle"] = "0";

            Session["CountryName"] = uoHiddenFieldCountryName.Value;
            Session["CityName"] = uoHiddenFieldCityName.Value;

            //if (uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelVendor && uoHiddenFieldRole.Value != TravelMartVariable.RoleVehicleVendor)
            //{
            //    BindCountry();
            //    BindCity();
            //    BindHotel();
            //    BindPort();
            //}
            //SetValues();
            ShowHideFilters();

            //Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]));    
            
            Response.Redirect(sPage);
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set variable values
        /// ---------------------------------------------------------------------------
        /// </summary>
        public void SetValues()
        {
            SetTitle();
            Session["DateFrom"] = uoCalendarDashboard.SelectedDate.ToString("MM/dd/yyyy");

            Session["strPendingFilter"] = "1";
            if (uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelVendor && uoHiddenFieldRole.Value != TravelMartVariable.RoleVehicleVendor)
            {                
                Session["Region"] = uoDropDownListRegion.SelectedValue;
                Session["Country"] = uoDropDownListCountry.SelectedValue;
                Session["City"] = uoDropDownListCity.SelectedValue;
                Session["Port"] = uoDropDownListPort.SelectedValue;
                Session["Hotel"] = uoHiddenFieldHotelID.Value;//uoDropDownListHotel.SelectedValue;
            }
            Session["Vehicle"] = "0";

            Session["ViewRegion"] = uoHiddenFieldViewRegion.Value;
            Session["ViewCountry"] = uoHiddenFieldViewCountry.Value;
            Session["ViewCity"] = uoHiddenFieldViewCity.Value;
            Session["ViewHotel"] = uoHiddenFieldViewHotel.Value;
            Session["ViewPort"] = uoHiddenFieldViewPort.Value;
            Session["ViewLegend"] = uoHiddenFieldViewLegend.Value;
            Session["ViewFilter"] = uoHiddenFieldFilter.Value;
            Session["ViewDashboard"] = uoHiddenFieldDashboard.Value;            
            Session["ViewDashboard2"] = uoHiddenFieldDashboard2.Value;
            Session["UserBranchID"] = uoHiddenFieldRoleBranchID.Value;

            //else
            //{
            //    string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            //    GlobalCode.Field2String(Session["DateFrom"]) = currentDate;
            //    Session["DateTo"] = currentDate;
            //    Session["strPendingFilter"] = "0";

            //    GlobalCode.Field2String(Session["Region"]) = "0";
            //     GlobalCode.Field2String(Session["Country"]) = "0";
            //    GlobalCode.Field2String(Session["City"]) = "0";
            //    GlobalCode.Field2String(Session["Port"]) = "0";
            //    GlobalCode.Field2String(Session["Hotel"]) = "0";
            //    Session["Vehicle"] = "0";

            //    TravelMartVariable.ViewRegion = "1";
            //    TravelMartVariable.ViewCountry = "1";
            //    TravelMartVariable.ViewCity = "1";
            //    TravelMartVariable.ViewHotel = "1";
            //    TravelMartVariable.ViewPort = "1";
            //    TravelMartVariable.ViewLegend = "1";
            //    TravelMartVariable.ViewFilter = "0";
            //    TravelMartVariable.ViewDashboard = "0";
            //    TravelMartVariable.ViewDashboard2 = "0";
            //    GlobalCode.Field2String(Session["UserBranchID"]) = "";
            //}
        }
        private void RoleSettings()
        {            
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            {               
                uoRowFilterRegion.Visible = false;
                uoRowFilterCountryName.Visible = false;
                uoRowFilterCountry.Visible = false;
                uoRowFilterCityName.Visible = false;
                uoRowFilterCity.Visible = false;
                uoRowFilterPort.Visible = false;
                uoRowFilterHotel.Visible = false;
                uoRowFilter.Visible = false;
                GetBranchInfo();
            }
            else if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
            {
                uoRowFilterHotel.Visible = false;
            }
            else
            {
                uoRowFilterRegion.Visible = true;
                uoRowFilterCountryName.Visible = true;
                uoRowFilterCountry.Visible = true;
                uoRowFilterCityName.Visible = true;
                uoRowFilterCity.Visible = true;
                uoRowFilterPort.Visible = true;
                uoRowFilterHotel.Visible = true;
                uoRowFilter.Visible = true;
            }
            //if (uoHiddenFieldRole.Value != TravelMartVariable.RoleAdministrator &&
            //    uoHiddenFieldRole.Value != TravelMartVariable.Role24x7 &&
            //    uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelSpecialist
            //    )
            //{
            //    uoRowLinkMenu.Visible = false;
            //}
        }
        /// <summary>
        /// Date Created:   11/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Show/hide filters from left menu
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void ShowHideFilters()
        {
            string showImage = "Images/box_plus.png";
            string hideImage = "Images/box_minus.png";
            //Region
            if (uoHiddenFieldViewRegion.Value == "1")
            {
                spanRegion.Style.Add("display", "inline");
                uoDropDownListRegion.Style.Add("display", "inline");
                uoImageRegion.ImageUrl = hideImage;
            }
            else
            {
                spanRegion.Style.Add("display", "none");
                uoDropDownListRegion.Style.Add("display", "none");
                uoImageRegion.ImageUrl = showImage;
            }
            //Country
            if (uoHiddenFieldViewCountry.Value == "1")
            {
                spanCountry.Style.Add("display", "inline");
                spanCountryName.Style.Add("display", "inline");
                uoDropDownListCountry.Style.Add("display", "inline");
                uoTextBoxCountryName.Style.Add("display", "inline");
                uoImageCountry.ImageUrl = hideImage;
            }
            else
            {
                spanCountry.Style.Add("display", "none");
                spanCountryName.Style.Add("display", "none");
                uoDropDownListCountry.Style.Add("display", "none");
                uoTextBoxCountryName.Style.Add("display", "none");
                uoImageCountry.ImageUrl = showImage;
            }
            //City
            if (uoHiddenFieldViewCity.Value == "1")
            {
                spanCity.Style.Add("display", "inline");
                spanCityName.Style.Add("display", "inline");
                uoDropDownListCity.Style.Add("display", "inline");
                uoTextBoxCityName.Style.Add("display", "inline");
                uoImageCity.ImageUrl = hideImage;
            }
            else
            {
                spanCity.Style.Add("display", "none");
                spanCityName.Style.Add("display", "none");
                uoDropDownListCity.Style.Add("display", "none");
                uoTextBoxCityName.Style.Add("display", "none");
                uoImageCity.ImageUrl = showImage;
            }
            //Hotel
            if (uoHiddenFieldViewHotel.Value == "1")
            {
                spanHotel.Style.Add("display", "inline");
                uoDropDownListHotel.Style.Add("display", "inline");
                uoImageHotel.ImageUrl = hideImage;
            }
            else
            {
                spanHotel.Style.Add("display", "none");
                uoDropDownListHotel.Style.Add("display", "none");
                uoImageHotel.ImageUrl = showImage;
            }
            //Port
            if (uoHiddenFieldViewPort.Value == "1")
            {
                spanPort.Style.Add("display", "inline");
                uoDropDownListPort.Style.Add("display", "inline");
                uoImagePort.ImageUrl = hideImage;
            }
            else
            {
                spanPort.Style.Add("display", "none");
                uoDropDownListPort.Style.Add("display", "none");
                uoImagePort.ImageUrl = showImage;
            }                       
            //Filter
            if (uoHiddenFieldFilter.Value == "1")
            {
                uoPanelFilter_CollapsiblePanelExtender.Collapsed = false;
            }
            else
            {
                uoPanelFilter_CollapsiblePanelExtender.Collapsed = true;
            }
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor ||
                uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            {
                uoImageFilter.Style.Add("display", "none");
            }
            
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set Title by date
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void SetTitle()
        {            
            //uclabelStatus.Text = 
            //if (uoTextBoxFrom != null)
            //{
            //    string FromDate = DateTime.Parse(uoTextBoxFrom.Text).ToString("dd-MMM-yyyy");
            //    string ToDate = DateTime.Parse(uoTextBoxTo.Text).ToString("dd-MMM-yyyy");

            //    uclabelStatus.Text = "Based on " + uoDropDownListBasedOn.SelectedItem.Text + " From:" + FromDate + " To:" + ToDate;
            //}
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get regions
        /// ---------------------------------------------------------------------------
        /// </summary>
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

                if (GlobalCode.Field2String(Session["Region"])  != "")
                {
                    if (uoDropDownListRegion.Items.FindByValue(GlobalCode.Field2String(Session["Region"])) != null)
                    {
                        uoDropDownListRegion.SelectedValue = GlobalCode.Field2String(Session["Region"]);
                    }
                }
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
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get country by region id
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindCountry()
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, uoTextBoxCountryName.Text);
                //                CountryDataTable = CountryBLL.CountryListByRegion(GlobalCode.Field2String(Session["Region"]), uoHiddenFieldCountryName.Value);
                uoDropDownListCountry.Items.Clear();
                ListItem item = new ListItem("--Select Country--", "0");
                uoDropDownListCountry.Items.Add(item);
                uoDropDownListCountry.DataSource = CountryDataTable;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();

                if (GlobalCode.Field2String(Session["Country"])  != "")
                {
                    if (uoDropDownListCountry.Items.FindByValue(GlobalCode.Field2String(Session["Country"])) != null)
                    {
                        uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(Session["Country"]);
                    }
                }
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
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get city by country id
        /// ---------------------------------------------------------------------------
        /// </summary>

        private void BindCity()
        {
            DataTable CityDataTable = null;
            try
            {
                CityDataTable = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxCityName.Text, "0");
                uoDropDownListCity.Items.Clear();
                ListItem item = new ListItem("--Select City--", "0");
                uoDropDownListCity.Items.Add(item);
                uoDropDownListCity.DataSource = CityDataTable;
                uoDropDownListCity.DataTextField = "colCityNameVarchar";
                uoDropDownListCity.DataValueField = "colCityIDInt";
                uoDropDownListCity.DataBind();

                if (GlobalCode.Field2String(Session["City"]) != "")
                {
                    if (uoDropDownListCity.Items.FindByValue(GlobalCode.Field2String(Session["City"])) != null)
                    {
                        uoDropDownListCity.SelectedValue = GlobalCode.Field2String(Session["City"]);
                    }
                }
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
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get port by user and city
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindPort()
        {
            DataTable PortDataTable = null;
            try
            {
                PortDataTable = PortBLL.GetPortListByCity(GlobalCode.Field2String(Session["UserName"]), uoDropDownListCity.SelectedValue);
                uoDropDownListPort.Items.Clear();
                ListItem item = new ListItem("--Select Port--", "0");
                uoDropDownListPort.Items.Add(item);
                uoDropDownListPort.DataSource = PortDataTable;
                uoDropDownListPort.DataTextField = "PORT";
                uoDropDownListPort.DataValueField = "PORTID";
                uoDropDownListPort.DataBind();

                if (GlobalCode.Field2String(Session["Port"]) != "")
                {
                    if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                    {
                        uoDropDownListPort.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel list
        /// ---------------------------------------------------------------------------        
        /// Date Modified:   14/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindHotel()
        {
            try
            {
                List<HotelDTO> hotelList = HotelBLL.GetHotelBranchByCity("", uoDropDownListCity.SelectedValue);
                var listHotel = (from a in hotelList
                                 select new
                                 {
                                     HotelID = a.HotelIDString,
                                     HotelName = a.HotelNameString
                                 }).ToList();

                uoDropDownListHotel.Items.Clear();
                ListItem item = new ListItem("--SELECT HOTEL--", "0");
                uoDropDownListHotel.Items.Add(item);

                uoDropDownListHotel.DataSource = listHotel;
                uoDropDownListHotel.DataTextField = "HotelName";
                uoDropDownListHotel.DataValueField = "HotelID";
                uoDropDownListHotel.DataBind();

                if (hotelList.Count == 1)
                {
                    uoDropDownListHotel.SelectedIndex = 1;
                }
                else if (hotelList.Count > 0)
                {
                    if (GlobalCode.Field2String(Session["Hotel"]) != "")
                    {
                        if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                        {
                            uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //DataTable HotelDataTable = null;
            //try
            //{
            //    HotelDataTable = HotelBLL.GetHotelBranchByCity(MUser.GetUserName(), uoDropDownListCity.SelectedValue);
            //    uoDropDownListHotel.Items.Clear();
            //    ListItem item = new ListItem("--Select Hotel--", "0");
            //    uoDropDownListHotel.Items.Add(item);
            //    uoDropDownListHotel.DataSource = HotelDataTable;
            //    uoDropDownListHotel.DataTextField = "BranchName";
            //    uoDropDownListHotel.DataValueField = "BranchID";
            //    uoDropDownListHotel.DataBind();

            //    if (GlobalCode.Field2String(Session["Hotel"]) != "")
            //    {
            //        if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
            //        {
            //            uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (HotelDataTable != null)
            //    {
            //        HotelDataTable.Dispose();
            //    }
            //}
        }   
        /// <summary>    
        /// Date Created:   20/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user branch details if user is vendor or Service Provider
        /// </summary>        
        private void GetBranchInfo()
        {
            if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist ||
               uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor ||
               uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            {
                IDataReader dr = null;
                try
                {
                    dr = UserAccountBLL.GetUserBranchDetails(GlobalCode.Field2String(Session["UserName"]), uoHiddenFieldRole.Value);
                    if (dr.Read())
                    {
                        uoLabelBranchName.Text = dr["BranchName"].ToString().ToUpper();
                        uoHiddenFieldRoleBranchID.Value = dr["BranchID"].ToString().ToUpper();
                        Session["UserRoleKey"]  = dr["RoleID"].ToString();
                        Session["UserBranchID"]  = dr["BranchID"].ToString();
                        Session["UserCountry"]  = dr["CountryID"].ToString();
                        Session["UserCity"]  = dr["CityID"].ToString();
                        Session["UserVendor"]  = dr["VendorID"].ToString();
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
                        dr.Close();
                        dr.Dispose();
                    }
                }
            }
        }
       
        /// <summary>
        /// Date Created:   18/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set dashboard groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "City";
            string GroupValueString = "City";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

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
                return string.Format("<tr><td class=\"group\" colspan=\"4\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>
        /// Date Created:   18/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set dashboard sub groupings
        /// <summary>
        string lastDataFieldValue2 = null;
        protected string DashboardAddSubGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Port";
            string GroupValueString = "Port";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue2 != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue2 = currentDataFieldValue;
                return string.Format("<tr><td class=\"group\" colspan=\"4\">&nbsp;&nbsp&nbsp;&nbsp{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>        
        /// Date Created:   06/02/2011
        /// Created By:     Josephine Gad
        /// (description)   Set the Calendar Dashboard count        
        /// </summary>
        /// <returns></returns>
        private List<ManifestCalendar> GetCalendarTable()
        {
            if (uoCalendarDashboard.Visible)
            {
                //DataTable dt = null;
                //StringWriter sw = new StringWriter();
                //XmlReader sr = null;
                //XmlDataDocument xmlDoc = null;
                //StringReader strReader = null;
                //TextWriter txtWriter = null;
                ManifestCalendar cal = new ManifestCalendar();
                List<ManifestCalendar> calList = new List<ManifestCalendar>();
                //IDataReader dr = null;
                try
                {
                    if (Session["TentativeManifestCalendarDashboard"] == null)
                    {
                        DateTime day = GlobalCode.Field2DateTime(uoHiddenFieldDateFrom.Value);
                        string sPendingFilter = "0";
                        string sRegion = "0";
                        string sCountry = "0";
                        string sCity = "0";
                        string sPort = "0";
                        string sHotel = "0";
                        string sVehicle = "0";
                        string sManifestHrs = "0";
                        if (GlobalCode.Field2String(Session["strPendingFilter"]) != "")
                        {
                            sPendingFilter = GlobalCode.Field2String(Session["strPendingFilter"]);
                        }
                        if (GlobalCode.Field2String(Session["Region"])  != "")
                        {
                            sRegion = GlobalCode.Field2String(Session["Region"]);
                        }
                        if (GlobalCode.Field2String(Session["Country"])  != "")
                        {
                            sCountry = GlobalCode.Field2String(Session["Country"]);
                        }
                        if (GlobalCode.Field2String(Session["City"])  != "")
                        {
                            sCity = GlobalCode.Field2String(Session["City"]);
                        }
                        if (GlobalCode.Field2String(Session["Port"]) != "")
                        {
                            sPort = GlobalCode.Field2String(Session["Port"]);
                        }
                        if (GlobalCode.Field2String(Session["Hotel"]) != "")
                        {
                            sHotel = GlobalCode.Field2String(Session["Hotel"]);
                        }
                        if (GlobalCode.Field2String(Session["Vehicle"]) != "")
                        {
                            sVehicle = GlobalCode.Field2String(Session["Vehicle"]);
                        }
                        if (GlobalCode.Field2String(Session["ManifestHrs"]) != "")
                        {
                            sManifestHrs = GlobalCode.Field2String(Session["ManifestHrs"]);
                        }
                        if (uoHiddenFieldCurrentPage.Value == "~/Hotel/HotelManifest2.aspx")
                        {
                            LockedManifestBLL lockedBLL = new LockedManifestBLL();
                            calList = lockedBLL.LoadLockedManifestCalendar( GlobalCode.Field2String(Session["UserName"]), day, Int32.Parse(sManifestHrs),
                                Int32.Parse(sHotel));
                        }
                        else
                        {
                            calList = HotelManifestBLL.GetTentativeManifestDashboard(day.ToString("MM/dd/yyyy"),
                           "", GlobalCode.Field2String(Session["UserName"]), "1",
                           "1", "", "0",
                           "0", "0", "0",
                          sRegion, sCountry,
                          sCity, sPort, sHotel, "0",
                          uoHiddenFieldRole.Value, sManifestHrs);                           
                        }
                        //calList = (from a in dt.AsEnumerable()
                        //           select new  ManifestCalendar
                        //           {
                        //               colDate = GlobalCode.Field2DateTime(a["colDate"]),
                        //               TotalCount = GlobalCode.Field2Int(a["TotalCount"])
                        //            }).ToList();
                       
                        Session["TentativeManifestCalendarDashboard"] = calList;                       
                        return calList;
                    }
                    else
                    {
                        calList = (List<ManifestCalendar>)Session["TentativeManifestCalendarDashboard"];
                        return calList;
                        ////XmlSerializer deserializer = new XmlSerializer(typeof(List<ManifestCalendar>));                        
                        //////TextReader rd = new StreamReader((Stream)Session["TentativeManifestCalendarDashboard"]);
                        //////deserializer = (XmlSerializer)Session["TentativeManifestCalendarDashboard"];
                        ////xmlDoc = new XmlDataDocument();
                        //////xmlDoc.LoadXml((XmlText)Session["TentativeManifestCalendarDashboard"]);
                        ////TextReader textReader = new StreamReader(xmlDoc.Value); //(@"C:\movie.xml");
                        //////calList = (List<ManifestCalendar>)deserializer.Deserialize();
                        ////calList = (List<ManifestCalendar>)deserializer.Deserialize(textReader);
                        ////textReader.Close();
                        ////textReader.Dispose();

                      // return calList;
                        
                        //XmlSerializer deserializer = new XmlSerializer(typeof(List<ManifestCalendar>));                        
                        //xmlDoc = new XmlDataDocument();
                        //xmlDoc.LoadXml(GlobalCode.Field2String(Session["TentativeManifestCalendarDashboard"]));
                        //TextReader textReader = new StreamReader(xmlDoc.InnerXml); //(@"C:\movie.xml");
                        
                        //calList = (List<ManifestCalendar>)deserializer.Deserialize(textReader);
                        //textReader.Close();

                        //return calList;

                        //sr = new StringReader(GlobalCode.Field2String(Session["TentativeManifestCalendarDashboard"]));
                        //dt = (DataTable)Cache["TentativeManifestCalendarDashboard"];
                        //dt.ReadXml(sr);
                        //xmlDoc = new XmlDataDocument();
                        //xmlDoc.LoadXml(GlobalCode.Field2String(Session["TentativeManifestCalendarDashboard"]));
                        ////strReader = new StringReader(GlobalCode.Field2String(Session["TentativeManifestCalendarDashboard"]));
                        //strReader = new StringReader(xmlDoc.InnerXml);
                        ////sr = new XmlNodeReader(xmlDoc);
                        //dt.ReadXml(strReader);
                        //return dt;

                        //sr = new XmlReader.Create("");
                        //XmlSerializer  ser =  new XmlSerializer(typeof(DataTable) );

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //if (dt != null)
                    //{
                    //    dt.Dispose();
                    //}
                    //if (sw != null)
                    //{
                    //    sw.Close();
                    //    sw.Dispose();
                    //}
                    //if (sr != null)
                    //{
                    //    sr.Close();                        
                    //}
                    //if (strReader != null)
                    //{
                    //    strReader.Close();
                    //    strReader.Dispose();
                    //}
                    //if (txtWriter != null)
                    //{
                    //    txtWriter.Close();
                    //    txtWriter.Dispose();
                    //}                    
                }
            }
            else
            {
                return null;
            }
        }
        #endregion                       

       
    }
}

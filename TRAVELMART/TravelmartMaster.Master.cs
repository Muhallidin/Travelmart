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

using System.Threading;

using System.Web.Security;

namespace TRAVELMART
{
    public partial class TravelMartMaster : System.Web.UI.MasterPage
    {
        #region "Events"
        private string vUser ="";
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  15/08/2012
        /// Description:    Get UserDateRange from UserAccountList
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {                                
                if (Request.QueryString["ufn"] != null)
                {
                    //if (Request.QueryString["dt"] != null)
                    //{
                        if (Page.AppRelativeVirtualPath != "~/HotelDashboard.aspx")
                        {
                            Session["DateFrom"] = DateTime.Parse(Request.QueryString["dt"].ToString().Replace("_", "/")).ToString("MM/dd/yyyy");
                            
                            DateTime dtTo = GlobalCode.Field2DateTime(Session["DateTo"]);
                            DateTime dtFrom = GlobalCode.Field2DateTime(Session["DateFrom"]);
                            if (dtFrom > dtTo)
                            {
                                Session["DateTo"] = GlobalCode.Field2String(Session["DateFrom"]);
                            }
                        }
                //}
                    string userName = GlobalCode.Field2String(Session["UserName"]);
                    //string userRole = MUser.GetUserRole();
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                    string userRole = UserRolePrimary;                    
                    Session["UserRole"] = UserRolePrimary;
                    uoHiddenFieldRole.Value = userRole;

                    List<UserAccountList> listUser = GetUserAccountList(userName);
                    int UserDateRange = listUser[0].iDayNo;//UserAccountBLL.GetUserDateRange(userName);

                    GetBranchInfo();

                    if (Page.AppRelativeVirtualPath == "~/Manifest.aspx")
                    {
                        uoRowLegend.Visible = true;
                    }
                    else
                    {
                        uoRowLegend.Visible = false;
                    }
                 
                    uclabelFName.Text = Request.QueryString["ufn"];
                    uoDropDownListBasedOn.SelectedValue = GlobalCode.Field2String(Session["strPendingFilter"]);
                    RoleSettings();

                    if (Session["DateFrom"] == null)
                    {
                        string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
                        string currenDateTo = DateTime.Now.AddDays(UserDateRange).ToString("MM/dd/yyyy");
                        if (uoTextBoxFrom.Text == "")
                        {
                            uoTextBoxFrom.Text = currentDate;
                        }
                        if (uoTextBoxTo.Text == "")
                        {
                            uoTextBoxTo.Text = currenDateTo;
                        }
//                        uoCalendarDashboard.SelectedDate = DateTime.Now;
  //                      uoCalendarDashboard.VisibleDate = DateTime.Now; 
                    }
                    if (GlobalCode.Field2String(Session["DateTo"]) == "")
                    {
                        string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
                        string currenDateTo = DateTime.Now.AddDays(UserDateRange).ToString("MM/dd/yyyy");
                        if (uoTextBoxFrom.Text == "")
                        {
                            uoTextBoxFrom.Text = currentDate;
                        }
                        if (uoTextBoxTo.Text == "")
                        {
                            uoTextBoxTo.Text = currenDateTo;
                        }                        
                    }
                    else
                    {
                        uoTextBoxFrom.Text = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                        uoTextBoxTo.Text = GlobalCode.Field2DateTime(Session["DateTo"]).ToString("MM/dd/yyyy");                        
                    }
                    uoHiddenFieldViewRegion.Value = GlobalCode.Field2String(Session["ViewRegion"]);
                    uoHiddenFieldViewCountry.Value = GlobalCode.Field2String(Session["ViewCountry"]);
                    uoHiddenFieldViewCity.Value = GlobalCode.Field2String(Session["ViewCity"]);
                    uoHiddenFieldViewHotel.Value = GlobalCode.Field2String(Session["ViewHotel"]);
                    uoHiddenFieldViewPort.Value = GlobalCode.Field2String(Session["ViewPort"]);
                    uoHiddenFieldViewLegend.Value = GlobalCode.Field2String(Session["ViewLegend"]);
                    uoHiddenFieldFilter.Value = GlobalCode.Field2String(Session["ViewFilter"]);
                    uoHiddenFieldDashboard.Value = GlobalCode.Field2String(Session["ViewDashboard"]);

                    uoHiddenFieldLeftMenu.Value = GlobalCode.Field2String(Session["ViewLeftMenu"]);
                    uoHiddenFieldDashboard2.Value = GlobalCode.Field2String(Session["ViewDashboard2"]);

                    //TravelMartVariable.TreadUserID = Session["UserName"].ToString();

                    ProcessTravelMartLoad();                 
                }      
            }
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //LeftMenu
            Session["ViewLeftMenu"] = uoHiddenFieldLeftMenu.Value;            
            if (uoHiddenFieldLeftMenu.Value == "1")
            {
                
                CollapsiblePanelExtender_uoPanelLeftMenu.Collapsed = false;                
            }
            else
            {
                CollapsiblePanelExtender_uoPanelLeftMenu.Collapsed = true;                
            }
            if (Session["strPrevPage"].ToString().Contains("HotelDashboard.aspx"))
            {
                uoPanelDateRange.Visible = false;
                FilterTable.Visible = false;
                uoPanelFilter.Visible = false;
            }           
        }


        protected void btnContinueWorking_Click(object sender, EventArgs e)
        {
            string sUser = GlobalCode.Field2String(MUser.GetUserName());
            if (sUser != "")
            {
                MembershipUser mUser = Membership.GetUser(sUser);
                if (mUser != null)
                {
                    mUser.LastActivityDate = DateTime.Now;
                    Membership.UpdateUser(mUser);
                }
            }
        }

        protected void btnExitWorking_Click(object sender, EventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (mUser != null)
            {
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);
            }
            FormsAuthentication.SignOut();
            try
            {
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                try
                {
                    Response.Redirect("../Login.aspx", false);

                }
                catch
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }



        #endregion

        /// <summary>
        /// =================================
        /// Date Created:   15/12/2011
        /// Created By:     Muhallidin G Wali
        /// Description:                             
        /// =================================
        /// </summary>
        #region > Tread Call <
        void ProcessTravelMartLoad()
        {
            try
            {
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
            }
            catch 
            { 
                throw; 
            }
        }


        #endregion

        #region >  <

        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindCountry();
        //    BindCity();
        //    BindPort();
        //    BindHotel();            
        //}
        //protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindCity();
        //    BindPort();
        //    BindHotel();
        //}
        //protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindPort();
        //    BindHotel();
        //}
        protected void uoButtonDateRange_Click(object sender, EventArgs e)
        {            
            string sURL = GlobalCode.Field2String(Session["strPrevPage"]);
            if (Request.QueryString["dt"] != null)
            {
                string sDate = Request.QueryString["dt"];
                sURL = sURL.Replace("%2f", "/");
                sURL = sURL.Replace(sDate, DateTime.Parse(uoTextBoxFrom.Text).ToString("MM/dd/yyyy"));
            }
            else
            {
                sURL = sURL + "&dt=" + DateTime.Parse(uoTextBoxFrom.Text).ToString("MM/dd/yyyy");
            }
            PostBackMasterPage(sURL);
        }
        string sFirstDay = "";
        DateTime dLastDay;
        protected void uoCalendarDashboard_DayRender(object sender, DayRenderEventArgs e)
        {            
            DateTime day = e.Day.Date;
            //DataTable dt = null;
            IDataReader dr = null;
            try
            {
                string sPendingFilter = "0";
                string sRegion = "0";
                string sCountry = "0";
                string sCity = "0";
                string sPort = "0";
                string sHotel = "0";
                string sVehicle = "0";

                sPendingFilter = GlobalCode.Field2Int(Session["strPendingFilter"]).ToString();
                sRegion = GlobalCode.Field2Int(Session["Region"]).ToString();
                sCountry = GlobalCode.Field2Int(Session["Country"]).ToString();
                sCity = GlobalCode.Field2Int(Session["City"]).ToString(); 
                sPort = GlobalCode.Field2Int(Session["Port"]).ToString(); 
                sHotel = GlobalCode.Field2Int(Session["Hotel"]).ToString(); 
                sVehicle = GlobalCode.Field2Int(Session["Vehicle"]).ToString();
                
                dr = SeafarerTravelBLL.GetTravelManifestDashboard(day.ToString("MM/dd/yyyy"), day.ToString("MM/dd/yyyy"), GlobalCode.Field2String(Session["UserName"]),
                     sPendingFilter, sRegion, sCountry,
                     sCity, "", "1", "", sPort, sHotel,
                     sVehicle, "0", "0", "0", "0", uoHiddenFieldRole.Value);
                if (dr.Read())
                {
                    //Label lblOnOff = new Label();
                    //lblOnOff.Text = "<br/>On:" + dr["SignOn"].ToString() + "<br/>";
                    //lblOnOff.Text += "Off:" + dr["SignOff"].ToString() + "<br/>";
                    //lblOnOff.ForeColor = Color.Red;
                    //lblOnOff.Font.Size = 8;
                    //e.Cell.Controls.Add(lblOnOff);
                    ////e.Cell.ToolTip = "On:" + dr["SignOn"].ToString() + " Off: " + dr["SignOff"].ToString();                         
                    ////e.Cell.CssClass = "testCSS";

                    if (dr["SignOn"].ToString() != "0")
                    {
                        Label lblOn = new Label();
                        lblOn.Text = "*";
                        lblOn.ForeColor = Color.Green;
                        e.Cell.Controls.Add(lblOn);
                    }
                    if (dr["SignOff"].ToString() != "0")
                    {
                        Label lblOff = new Label();
                        lblOff.Text = "*";
                        lblOff.ForeColor = Color.Red;
                        e.Cell.Controls.Add(lblOff);
                    }
                    e.Cell.CssClass = "hoverDate";
                    e.Cell.ID = day.ToString("MM_dd_yyyy");
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
            
            Session["ViewLeftMenu"] = uoHiddenFieldLeftMenu.Value;
            Session["ViewDashboard2"] = uoHiddenFieldDashboard2.Value;

            Session["Region"] = uoHiddenFieldRegionID.Value;
            Session["Country"] = uoHiddenFieldCountryID.Value;
            Session["City"] = uoHiddenFieldCityID.Value;
            Session["Port"] = uoHiddenFieldPortID.Value;
            Session["Hotel"] = uoHiddenFieldHotelID.Value;
            Session["Vehicle"] = "0";

            Session["CountryName"] = uoHiddenFieldCountryName.Value;
            Session["CityName"] = uoHiddenFieldCityName.Value;

            if (uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelVendor && uoHiddenFieldRole.Value != TravelMartVariable.RoleVehicleVendor)
            {
                BindCountry();
                BindCity();
                BindHotel();
                BindPort();
            }
            SetValues();
            ShowHideFilters();

            //Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]));

            //string sURL;
            //if (Request.QueryString["dt"] != null)
            //{
            //    sURL = sPage.Replace("dt=" + Request.QueryString["dt"].ToString(), "dt=" + DateTime.Parse(uoTextBoxFrom.Text).ToString("MM/dd/yyyy"));
            //}
            //else
            //{
            //    sURL = sPage + "&dt=" + DateTime.Parse(uoTextBoxFrom.Text).ToString("MM/dd/yyyy");
            //}
            //Response.Redirect(sURL);
            
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
            if (uoTextBoxFrom != null)
            {
                if (Page.AppRelativeVirtualPath != "~/ManifestSearchView.aspx")
                {
                    SetTitle();
                }                
                
                Session["DateFrom"] = uoTextBoxFrom.Text;
                Session["DateTo"] = uoTextBoxTo.Text;
                Session["strPendingFilter"] = uoDropDownListBasedOn.SelectedValue;

                if (uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelVendor && uoHiddenFieldRole.Value != TravelMartVariable.RoleVehicleVendor)
                {
                    Session["Region"] = uoDropDownListRegion.SelectedValue;
                    Session["Country"] = uoDropDownListCountry.SelectedValue;
                    Session["City"] = uoDropDownListCity.SelectedValue;
                    Session["Port"] = uoDropDownListPort.SelectedValue;
                    Session["Hotel"] = uoDropDownListHotel.SelectedValue;
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
                
                Session["ViewLeftMenu"] = uoHiddenFieldLeftMenu.Value;
                Session["ViewDashboard2"] = uoHiddenFieldDashboard2.Value;
                Session["UserBranchID"] = uoHiddenFieldRoleBranchID.Value;
            }
            else
            {
                string currentDate = DateTime.Now.ToString("MM/dd/yyyy");                                
                Session["DateFrom"] = currentDate;                                
                Session["DateTo"] = currentDate;
                Session["strPendingFilter"] = "0";

                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["City"] = "0";
                Session["Port"] = "0";
                Session["Hotel"] = "0";
                Session["Vehicle"] = "0";

                Session["ViewRegion"] = "1";
                Session["ViewCountry"] = "1";
                Session["ViewCity"] = "1";
                Session["ViewHotel"] = "1";
                Session["ViewPort"] = "1";
                Session["ViewLegend"] = "1";
                Session["ViewFilter"] = "0";
                Session["ViewDashboard"] = "0";
                Session["ViewDashboard2"] = "0";
                Session["UserBranchID"] = "";
            }
        }
        private void RoleSettings()
        {
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)                
            {
                //uoHiddenFieldViewRegion.Value = "0";
                //uoHiddenFieldViewCountry.Value = "0";
                //uoHiddenFieldViewCity.Value = "0";
                //uoHiddenFieldViewPort.Value = "0"; 
                uoRowFilterRegion.Visible = false;
                uoRowFilterCountryName.Visible = false;
                uoRowFilterCountry.Visible = false;
                uoRowFilterCityName.Visible = false;
                uoRowFilterCity.Visible = false;
                uoRowFilterPort.Visible = false;
                uoRowFilterHotel.Visible = false;
                //uoRowFilterBrk.Visible = false;
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
                //uoRowFilterBrk.Visible = true;

            }
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
            //Legend
            if (uoHiddenFieldViewLegend.Value == "1")
            {
                CollapsiblePanelExtender_Legend.Collapsed = true;
            }
            else
            {
                CollapsiblePanelExtender_Legend.Collapsed = false;
            }
            //Dashboard
            //if (uoHiddenFieldDashboard.Value == "1")
            //{
            //    CollapsiblePanelExtender_Dashboard.Collapsed = true;
            //}
            //else
            //{
            //    CollapsiblePanelExtender_Dashboard.Collapsed = false;
            //}
            if (uoHiddenFieldDashboard2.Value == "1")
            {
                CollapsiblePanelExtender_Dashboard2.Collapsed = true;
            }
            else
            {
                CollapsiblePanelExtender_Dashboard2.Collapsed = false;
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
           
            //LeftMenu
            if (uoHiddenFieldLeftMenu.Value == "1")
            {
                CollapsiblePanelExtender_uoPanelLeftMenu.Collapsed = false;
                //TravelmartCalendar1.Visible = true;
            }
            else
            {
                CollapsiblePanelExtender_uoPanelLeftMenu.Collapsed = true;
                //TravelmartCalendar1.Visible = false;
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
            if (uclabelStatus.Text.Trim() == "")
            {
                if (uoTextBoxFrom != null)
                {
                    //string FromDate = DateTime.Parse(uoTextBoxFrom.Text).ToString("dd-MMM-yyyy");
                    string sDate = Request.QueryString["dt"].ToString();
                    sDate = sDate.Replace("_", "/");
                    string FromDate = DateTime.Parse(sDate).ToString("dd-MMM-yyyy");
                    string ToDate = DateTime.Parse(uoTextBoxTo.Text).ToString("dd-MMM-yyyy");
                    
                    string sPage =Page.AppRelativeVirtualPath;
                    if (sPage == "~/Manifest.aspx" || sPage == "~/Hotel/HotelOverflowBooking2.aspx" ||
                       sPage == "~/NoTravelRequest.aspx")
                    {
                        uclabelStatus.Text = "Based on " + uoDropDownListBasedOn.SelectedItem.Text + ": " + FromDate;
                    }
                    else
                    {
                        uclabelStatus.Text = "Based on " + uoDropDownListBasedOn.SelectedItem.Text + " From:" + FromDate + " To:" + ToDate;
                    }
                }
            }
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get regions
        /// ---------------------------------------------------------------------------
        /// Date Modified : 26/01/2012
        /// Modified By:    Gelo Oquialda
        /// (description)   change MapRefrence to Region
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

                if (GlobalCode.Field2String(Session["Region"]) != "")
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

                if (GlobalCode.Field2String(Session["Country"]) != "")
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
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List and use session
        /// </summary>
        private void BindPort()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                //PortDataTable = PortBLL.GetPortListByCity(Session["UserName"].ToString(), uoDropDownListCity.SelectedValue); GetPortListByRegion
                //PortDataTable = PortBLL.GetPortListByCity(TravelMartVariable.TreadUserID, uoDropDownListCity.SelectedValue);
                //list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, uoDropDownListCountry.SelectedValue);

                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");
                
                uoDropDownListPort.Items.Clear();
                ListItem item = new ListItem("--Select Port--", "0");
                uoDropDownListPort.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPort.DataSource = list;
                    uoDropDownListPort.DataTextField = "PORTName";
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
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel list
        /// ---------------------------------------------------------------------------
        /// Date Modified:   15/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Delete DataTable and replace with List
        /// ---------------------------------------------------------------------------
        /// </summary>
         private void BindHotel()
         {                        
            try
            {
                //HotelDataTable = HotelBLL.GetHotelBranchByCity(TravelMartVariable.TreadUserID, uoDropDownListCity.SelectedValue);
                List<HotelDTO> hotelList = HotelBLL.GetHotelBranchByRegionPortCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, uoDropDownListPort.SelectedValue, uoDropDownListCountry.SelectedValue, "0");

                uoDropDownListHotel.Items.Clear();
                ListItem item = new ListItem("--Select Hotel--", "0");
                uoDropDownListHotel.Items.Add(item);

                var listHotel = (from a in hotelList
                                 select new
                                 {
                                     BranchID = a.HotelIDString,
                                     BranchName = a.HotelNameString
                                 }).ToList();
                uoDropDownListHotel.DataSource = listHotel;
                uoDropDownListHotel.DataTextField = "BranchName";
                uoDropDownListHotel.DataValueField = "BranchID";
                uoDropDownListHotel.DataBind();

                if (GlobalCode.Field2String(Session["Hotel"]) != "")
                {
                    if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                    {
                        uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
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
                         Session["UserRoleKey"] = dr["RoleID"].ToString();
                         Session["UserBranchID"] = dr["BranchID"].ToString();
                         Session["UserCountry"] = dr["CountryID"].ToString();
                         Session["UserCity"] = dr["CityID"].ToString();
                         Session["UserVendor"] = dr["VendorID"].ToString();
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

         protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
         {
             //MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
             //mUser.LastActivityDate = DateTime.Now.AddMinutes(-15);
             //Membership.UpdateUser(mUser);
            
             //FormsAuthentication.SignOut();
             //Response.Redirect("~/Login.aspx");
             MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
             if (mUser != null)
             {
                 mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                 Membership.UpdateUser(mUser);
             }
             FormsAuthentication.SignOut();
             Response.Redirect("~/Login.aspx");
         }
         /// <summary>
         /// Author:         Josephine Gad
         /// Date Created:   15/08/2012
         /// Description:    Get user details using session
         /// </summary>
         /// <returns></returns>
         private List<UserAccountList> GetUserAccountList(string sUserName)
         {
             List<UserAccountList> list = new List<UserAccountList>();

             if (Session["UserAccountList"] != null)
             {
                 list = (List<UserAccountList>)Session["UserAccountList"];
             }
             else
             {
                 list = UserAccountBLL.GetUserInfoListByName("sUserName");
                 Session["UserAccountList"] = list;
             }
             return list;
         }
        #endregion                            
    }
     
}



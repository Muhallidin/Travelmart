using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Web.Security;
using System.Globalization;
using PDF = iTextSharp;
using System.Collections.ObjectModel;


namespace TRAVELMART.Hotel
{
    public partial class HotelConfirmManifest : System.Web.UI.Page
    {
        ManifestBLL MBLL = new ManifestBLL();

        #region Event
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2012
        /// Description:    Hotel Manifest
        /// --------------------------------------------------------------      
        /// Modified By:    Josephine Gad
        /// Date Modified:  01/Jul/2013
        /// Description:    Add control settings for Admin/Hotel Specialist, Hotel vendor and Finance
        /// --------------------------------------------------------------   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //LoadHotelTimeZone();
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldNoOfDays.Value = GlobalCode.Field2Int(Session["NoOfDays_Hotel"]).ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                SetDefaultValues();
                //GetNationality();
                //GetGender();
                //GetRank();
                //GetVessel();Crewtravel@Admin

                GetHotelFilter();

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    uoTDRegionPort.Visible = false;
                }
                //else
                //{
                //    uoTRVessel.Visible = true;
                //}

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator)
                {
                    uoHyperLinkSendEmails.Visible = true;
                }
                else
                {
                    uoHyperLinkSendEmails.Visible = false;
                }

                if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist) || User.IsInRole(TravelMartVariable.RoleAdministrator))
                {
                    uoHyperLinkHotelBookingCancel.Visible = true;
                }
                else
                {
                    uoHyperLinkHotelBookingCancel.Visible = false;
                }

                //DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                //DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                //TimeSpan timeDiff = dDateFrom - dDateCurrent;
                //int dDays = timeDiff.Days;               

                string sHotel = GlobalCode.Field2String(Session["Hotel"]);
                if (sHotel != "" && sHotel != "0")
                {
                    if (uoDropDownListHotel.Items.FindByValue(sHotel) != null)
                    {
                        uoDropDownListHotel.SelectedValue = sHotel;
                    }
                }
                LoadDefaults(0);

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                 uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator)
                {
                    uoDivHotelSpecialist.Visible = true;
                    //uoLabelTitle.Text = "Confirm Manifest";
                    uoButtonConfirmByVendor.Visible = false;
                    uoHyperLinkSendEmails.Visible = true;
                    uoHiddenFieldIsVendor.Value = "false";
                }
                else if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    uoDivHotelSpecialist.Visible = false;
                    //uoLabelTitle.Text = "Hotel Manifest";
                    uoButtonConfirmByVendor.Visible = true;
                    uoHyperLinkSendEmails.Visible = false;
                    uoHiddenFieldIsVendor.Value = "true";

                    uoListViewHeaderConfirmed.DataSource = null;
                    uoListViewHeaderConfirmed.DataBind();

                    ControlSettings();
                    //ControlSettingsChangeName();

                    LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                    uoLinkExport.Visible = false;

                    HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");
                    ucSpanExportALL.Visible = false;

                    LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                    HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");

                    uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                    ucSpanViewWeek.Visible = false;
                }
                else
                {
                    uoButtonConfirmByVendor.Visible = false;
                    uoHiddenFieldIsVendor.Value = "false";


                    LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                    uoLinkExport.Visible = false;

                    HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");
                    ucSpanExportALL.Visible = false;

                    LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                    HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");

                    uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                    ucSpanViewWeek.Visible = false;
                }
                BindControlNo();
            }
            else
            {
                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

                if (uoHiddenFieldPopupCalendar.Value == "1" || (sChangeDate == "1" && uoHiddenFieldLoadType.Value == "0"))
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                    GetSFHotelTravelDetails();
                    GetConfirmedList();
                    GetCancelledList();
                    BindControlNo();
                }
            }
            if (uoHiddenFieldSendEmail.Value == "1")
            {
                SendEmailButton(false);
            }
            if (uoHiddenFieldSendSaveEmail.Value == "1")
            {
                SendEmailButton(true);
            }
            uoHiddenFieldSendEmail.Value = "0";
            uoHiddenFieldSendSaveEmail.Value = "0";

            uoHiddenFieldConfirmation.Value = (Session["ConfirmationTag"] != null) ? Session["ConfirmationTag"].ToString() : "0";
            if (uoHiddenFieldConfirmation.Value == "1")
            {
                GetSFHotelTravelDetails();
                GetConfirmedList();
                GetCancelledList();

                uoHiddenFieldConfirmation.Value = "0";
                Session["ConfirmationTag"] = "0";
            }
            if (uoHiddenFieldHotelCancelPopup.Value == "1")
            {
                GetSFHotelTravelDetails();
                GetConfirmedList();
                GetCancelledList();

                uoHiddenFieldHotelCancelPopup.Value = "0";
            }
            if (uoHiddenFieldPopEditor.Value == "1")
            {
                GetSFHotelTravelDetails();
                GetConfirmedList();
                GetCancelledList();

                uoHiddenFieldPopEditor.Value = "0";
            }
        }

        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //uoHiddenFieldLoadType.Value = "1";
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;

            uoHiddenFieldLoadType.Value = "0";
            GetSFHotelTravelDetails();
            BindControlNo();
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
            BindControlNo();
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            uoHiddenFieldRegion.Value = uoDropDownListRegion.SelectedValue;
            uoHiddenFieldPort.Value = "0";

            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = "";
            Session.Remove("Port"); // remove the current selected Port 05/07/2012
            LoadDefaults(1);
            GetHotelFilter();
            BindControlNo();
        }

        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPort.SelectedValue;
            uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;
            LoadDefaults(1);
            GetHotelFilter();
            BindControlNo();
        }
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtCancelled = null;

            try
            {
                MBLL.GetHotelConfirmManifestExport(uoHiddenFieldDate.Value, uoHiddenFieldUser.Value,
                   uoDropDownListHotel.SelectedValue, uoHiddenFieldSortBy.Value);

                List<HotelManifest> list = new List<HotelManifest>();
                List<HotelManifest> listCancelled = new List<HotelManifest>();
                list = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
                listCancelled = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];

                if (list.Count > 0 || listCancelled.Count > 0)
                {
                    dt = GetConfirmedDataTable(list);
                    dtCancelled = GetCancelledDataTable(listCancelled);
                    CreateFile(dt, dtCancelled);
                }
                else
                {
                    AlertMessage("There is no record to export.");
                }
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   14/Dec/2014
        /// Description:    Create PDF file
        /// -------------------------------------
        /// Modified by:    Josephine Monteza
        /// Date Modified:  10/Jul/2015
        /// Description:    Create PDF file for the manifest with control no.
        /// -------------------------------------
        /// </summary>        
        protected void uoLinkButtonControlNo_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtCancelled = null;

            try
            {
                int iControlID = GlobalCode.Field2Int(uoHiddenFieldContolID.Value);
                MBLL.GetHotelConfirmManifestWithControlNoExport(iControlID, uoHiddenFieldSortBy.Value);

                List<HotelManifest> list = new List<HotelManifest>();
                List<HotelManifest> listCancelled = new List<HotelManifest>();
                list = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
                listCancelled = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];

                if (list.Count > 0 || listCancelled.Count > 0)
                {
                    dt = GetConfirmedDataTable(list);
                    dtCancelled = GetCancelledDataTable(listCancelled);

                    string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                    string sDateOnly = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM dd, yyyy");
                    string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM_dd_yyy");

                    string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                    //string FileName = FilePath + "HotelManifest_" + sDateManifest + '_' + sDate + ".xls";
                    //string FileNameWithDiff = FilePath + "HotelManifestWithDiff_" + sDateManifest + '_' + sDate + ".xls";
                    //string FileNameDiff = "";
                    string sFileName = "HotelManifest_" + sDateManifest + '_' + sDate + ".pdf";
                    string PDFFileName = FilePath + sFileName;

                    CreatePDF(sDateOnly, PDFFileName, dt, dtCancelled);
                    OpenExcelFile(sFileName, FilePath, true);
                }
                else
                {
                    AlertMessage("There is no record to export.");
                }
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Feb/2013
        /// Description:    Get the order to be used
        /// -------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   18/Sept/2013
        /// Description:    Update DataPager
        /// -------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            uolistviewHotelInfoPager.SetPageProperties(0, uolistviewHotelInfoPager.PageSize, false);
            uoDataPagerConfirmed.SetPageProperties(0, uoDataPagerConfirmed.PageSize, false);
            uoDataPagerCancelled.SetPageProperties(0, uoDataPagerCancelled.PageSize, false);

            GetSFHotelTravelDetails();
            GetConfirmedList();
            GetCancelledList();
        }

        protected void uoListViewHeaderConfirmed_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            uolistviewHotelInfoPager.SetPageProperties(0, uolistviewHotelInfoPager.PageSize, false);
            uoDataPagerConfirmed.SetPageProperties(0, uoDataPagerConfirmed.PageSize, false);
            uoDataPagerCancelled.SetPageProperties(0, uoDataPagerCancelled.PageSize, false);

            GetSFHotelTravelDetails();
            GetConfirmedList();
            GetCancelledList();

            //GetOverflow();
        }
        protected void uoListViewManifestConfirmed_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Tag")
            {
                Label uoLblRecLocLV = (Label)e.Item.FindControl("uoLblRecLoc");
                Label uoLblSfIDLV = (Label)e.Item.FindControl("uoLblSfID");
                HiddenField uoHiddenFieldStatusLV = (HiddenField)e.Item.FindControl("uoHiddenFieldStatus");

                string arg = e.CommandArgument.ToString();
                string[] args = arg.Split(":".ToCharArray());
                if (args.Count() > 1)
                {
                    TagSeafarer(args[0].ToString(), args[1].ToString(), args[2].ToString(),
                        uoLblRecLocLV.Text, uoLblSfIDLV.Text, uoHiddenFieldStatusLV.Value);
                }
            }
        }

        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2013
        /// Description:    Add settings of Email and uoBtnExportList if hide or show
        ///                 Get the Hotel Total Bookings
        /// -------------------------------------
        protected void uoListViewCancelled_DataBound(object sender, EventArgs e)
        {
            //Email Settings
            EmailSettings();

            //Settings of Export Button
            List<HotelManifest> TentativeManifest = new List<HotelManifest>();
            List<HotelManifest> TentativeManifestCancel = new List<HotelManifest>();

            if (Session["ConfirmManifest_ConfirmedManifest"] != null)
            {
                TentativeManifest = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
            }

            if (Session["ConfirmManifest_CancelledManifest"] != null)
            {
                TentativeManifestCancel = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];
            }

            if (TentativeManifest.Count > 0 || TentativeManifestCancel.Count > 0)
            {
                uoBtnExportList.Visible = true;
            }
            else
            {
                uoBtnExportList.Visible = false;
            }

            List<HotelDashboardList> CountSummary = new List<HotelDashboardList>();
            if (Session["ConfirmManifest_CountSummary"] != null)
            {
                CountSummary = (List<HotelDashboardList>)Session["ConfirmManifest_CountSummary"];
            }

            uoListViewDashboard.DataSource = CountSummary;
            uoListViewDashboard.DataBind();

            BindControlNo();
        }
        protected void uoListViewManifestConfirmed_DataBound(object sender, EventArgs e)
        {
            CancelButtonSettings();
        }
        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            CancelButtonSettings();
            BindNoOfDays();            
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   25/Apr/2013
        /// Description:    Confirm Manifest for Hotel Vendor        
        /// -------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonConfirmByVendor_Click(object sender, EventArgs e)
        {
            ConfirmManifest(false, "", "");

            List<HotelManifest> list = new List<HotelManifest>();
            List<HotelManifest> listCancelled = new List<HotelManifest>();
            list = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
            listCancelled = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];

            if (list.Count > 0 || listCancelled.Count > 0)
            {

                GetSFHotelTravelDetails();
                GetConfirmedList();
                GetCancelledList();
                BindControlNo();

                AlertMessage("Manifest Confirmed!");
            }
            else
            {
                AlertMessage("There is no manifest to confirm.");
            }
        }

        protected void uoListViewManifestConfirmed_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            MBLL.GetHotelConfirmManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Confirm");
        }
        protected void uoListViewCancelled_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            MBLL.GetHotelConfirmManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Cancel");
        }
        protected void uoDropDownListDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NoOfDays_Hotel"] = uoDropDownListDays.SelectedValue;
            uoHiddenFieldNoOfDays.Value = uoDropDownListDays.SelectedValue;
            uoHiddenFieldLoadType.Value = "0";
            GetSFHotelTravelDetails();
        }
        #endregion


        #region Functions
        protected void InitializeValues()
        {
            string sUserName = "";

            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                if (GlobalCode.Field2String(User.Identity.Name) == "")
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    Session["UserName"] = GlobalCode.Field2String(User.Identity.Name);
                }
            }

            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }
            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            Session["strPrevPage"] = Request.RawUrl;

            uoListView1.DataSource = null;
            uoListView1.DataBind();

            uoListViewHeaderConfirmed.DataSource = null;
            uoListViewHeaderConfirmed.DataBind();

            ControlSettings();
            //ControlSettingsChangeName();

            uoListViewHeaderCancelled.DataSource = null;
            uoListViewHeaderCancelled.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2013
        /// Description:    Bind the Tentative Manifest and Hotel List 
        /// -------------------------------------
        /// </summary>
        private void GetSFHotelTravelDetails()
        {
            uolistviewHotelInfo.DataSource = null;
            uolistviewHotelInfo.DataSourceID = "uoObjectDataSourceManifest";
            uolistviewHotelInfo.DataBind();

            //EmailSettings();
        }
        private void GetConfirmedList()
        {
            uoListViewManifestConfirmed.DataSource = null;
            uoListViewManifestConfirmed.DataSourceID = "uoObjectDataSourceConfirmed";
            uoListViewManifestConfirmed.DataBind();
        }
        private void GetCancelledList()
        {
            uoListViewCancelled.DataSource = null;
            uoListViewCancelled.DataSourceID = "uoObjectDataSourceCancelled";
            uoListViewCancelled.DataBind();
        }
        /// Author:         Josephine Gad
        /// Date Created:   03/Apr/2013
        /// Description:    Set email values
        /// -------------------------------------      
        private void EmailSettings()
        {
            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator)
            {
                if (GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue) > 0)
                {
                    uoHyperLinkSendEmails.Visible = true;
                    //uoBtnExportList.Visible = true;

                    if (Session["ConfirmManifest_EmailRecipient"] != null)
                    {
                        List<EmailRecipient> email = (List<EmailRecipient>)Session["ConfirmManifest_EmailRecipient"];
                        if (email.Count > 0)
                        {
                            uoTextBoxTo.Text = email[0].EmailTo;
                            uoTextBoxCc.Text = email[0].EmailCc;
                            //uoHiddenFieldTo.Value = email[0].EmailTo;
                            //uoHiddenFieldCc.Value = email[0].EmailCc;
                        }
                        else
                        {
                            uoTextBoxTo.Text = "";
                            uoTextBoxCc.Text = "";
                            uoHiddenFieldTo.Value = "";
                            uoHiddenFieldCc.Value = "";
                        }
                    }
                }
                else
                {
                    uoHyperLinkSendEmails.Visible = false;
                    // uoBtnExportList.Visible = false;
                    uoTextBoxTo.Text = "";
                    uoTextBoxCc.Text = "";
                    uoHiddenFieldTo.Value = "";
                    uoHiddenFieldCc.Value = "";
                }
            }
        }
        private void SetDefaultValues()
        {
            if (GlobalCode.Field2String(Session["Region"]) == "")
            {
                Session["strPendingFilter"] = "0";
                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["City"] = "0";
                Session["Port"] = "0";
                Session["Hotel"] = "0";
                Session["Vehicle"] = "0";
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  09/07/2012
        /// Description:    Change DataTable to List
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  02/10/2012
        /// Description:    Add option "Select ALL Hotel" ,"-1" if there is selected Region
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  24/Apr/2013
        /// Description:    Add Hotel for Vendor Role
        /// ----------------------------------------------
        /// </summary>
        private void GetHotelFilter()
        {
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                //For Hotel Vendor Role
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    uoDropDownListHotel.Items.Clear();
                    if (Session["UserBranchID"] == null)
                    {
                        GetUserBranchInfo();
                    }
                    string sBranchID = GlobalCode.Field2Int(Session["UserBranchID"]).ToString();
                    string sBranchName = GlobalCode.Field2String(Session["BranchName"]);

                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem(sBranchName, sBranchID));
                    Session["Hotel"] = sBranchID;

                    uoDropDownListHotel.Enabled = false;
                }
                else
                {
                    list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, Session["Region"].ToString(),
                        Session["Port"] == null ? "0" : Session["Port"].ToString(), "0", "0");

                    int iRowCount = list.Count;
                    if (iRowCount == 1)
                    {
                        Session["Hotel"] = list[0].HotelIDString;
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

                        if (GlobalCode.Field2Int(Session["Region"]) > 0 || GlobalCode.Field2Int(Session["Port"]) > 0)
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
                        RemoveDuplicateItems(uoDropDownListHotel);
                        uoDropDownListHotel.Enabled = true;

                        if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                        {
                            uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                        }
                        //LoadTravelDetails();
                    }
                    else
                    {
                        uoDropDownListHotel.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveDuplicateItems(DropDownList ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ddl.SelectedIndex = i;
                string str = ddl.SelectedItem.ToString();
                for (int counter = i + 1; counter < ddl.Items.Count; counter++)
                {
                    ddl.SelectedIndex = counter;
                    string compareStr = ddl.SelectedItem.ToString();
                    if (str == compareStr)
                    {
                        ddl.Items.RemoveAt(counter);
                        counter = counter - 1;
                    }
                }
            }
            ddl.SelectedIndex = 0;
        }
        /// <summary>
        /// Date Created:   22/02/2011
        /// Created By:     Josephine Gad
        /// (description)   disable lock button if past dates and if there is no data
        /// -------------------------------------------------------
        /// Date Modified:  23/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   disable lock button  if already locked
        /// -------------------------------------------------------
        /// Date Modified:  02/10/2012
        /// Modified By:    Josephine Gad
        /// (description)   disable lock button  if "ALL Hotel" is selected
        /// -------------------------------------------------------
        /// </summary>
        //private void ButtonLockSettings()
        //{
        //    if (uolistviewHotelInfo.Items.Count > 0)
        //    {
        //        uoButtonLock.Enabled = true;
        //        uoBtnExportList.Enabled = true;
        //        if (uoDropDownListHotel.SelectedValue == "-1")
        //        {
        //            uoButtonLock.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        uoButtonLock.Enabled = false;
        //        uoBtnExportList.Enabled = false;
        //    }

        //    string sDate = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
        //    if (GlobalCode.Field2DateTime((sDate)) < DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")))
        //    {
        //        uoButtonLock.Enabled = false;
        //    }

        //    bool IsAlreadyLocked = HotelManifestBLL.IsHotelHasLockedManifest(sDate, uoDropDownListHotel.SelectedValue, uoDropDownListHours.SelectedValue);
        //    if (IsAlreadyLocked)
        //    {
        //        uoButtonLock.Enabled = false;
        //    }
        //}

        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel"; //"Check In";
            string GroupValueString = "HotelBranch"; //"colTimeSpanStartDate";

            if (Eval(GroupValueString) != null)
            {
                string currentDataFieldValue = Eval(GroupValueString).ToString();

                //Specify name to display if dataFieldValue is a database NULL
                if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
                {
                    currentDataFieldValue = "";
                }
                //See if there's been a change in value
                if (lastDataFieldValue != currentDataFieldValue) //Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy")
                {
                    //There's been a change! Record the change and emit the table row
                    lastDataFieldValue = currentDataFieldValue; //Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy")
                    return string.Format("<tr><td class=\"group\" colspan=\"36\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
                }
                else
                {
                    //No change, return an empty string
                    return string.Empty;
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "scr", sScript, false);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        #endregion

        /// <summary>
        /// Author: Chralene Remotigue
        /// Date Created: 17/04/2012
        /// Description: convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 17/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: create the file to be exported
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  01/Apr/2013
        /// Description:    Add Cancelled List in Excel
        /// ---------------------------------------------
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt, DataTable dtCancelled)
        {
            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/HotelManifest/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");

            string FileName = "HotelManifest_" + sDateManifest + '_' + sDate + ".xls";
            strFileName = FilePath + FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            CreateExcel(dt, strFileName, dtCancelled);
            OpenExcelFile(FileName, strFileName, false);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   02/Apr/2013
        /// Description:    Create the excel file and send email
        /// ------------------------------------------------
        /// </summary>
        private void CreateEmail(DataTable dt, DataTable dtCancelled, string sEmailTo, string sEmailCc)
        {
            try
            {

                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");
                string sDateOnly = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM dd, yyyy");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "HotelManifest_" + sDateManifest + "_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

                //ExportToExcel(dt, strFileName, dtCancelled);
                CreateExcel(dt, strFileName, dtCancelled);

                string sSubject = "Travelmart: Hotel Manifest " + uoDropDownListHotel.SelectedItem.Text +
                           " " + sDateOnly;
                string sMsg = "Please find attached " + uoDropDownListHotel.SelectedItem.Text + " manifest for " +
                            sDateOnly + ".<br/><br/>Please send us confirmation and any questions to  " + uoHiddenFieldHotelSpecialistEmail.Value.Trim() + ".<br/><br/>Thank you.";
                EmailManifest(sSubject, sMsg, strFileName, "",
                 sEmailTo, sEmailCc, (strFileName + ";").TrimEnd(';'));

                string[] sEmailArray = sEmailTo.Split(";".ToCharArray());
                string[] sEmailCCArray = sEmailCc.Split(";".ToCharArray());

                if (sEmailArray.Count() > 0 || sEmailCCArray.Count() > 0)
                {
                    EPortalBLL PortalBLL = new EPortalBLL();
                    string sModuleID = GlobalCode.Field2Int(PortalBLL.GetPortalModuleID("COM")).ToString();

                    //Email main recipient
                    for (int i = 0; i < sEmailArray.Count(); i++)
                    {
                        WSConnection.PortalInboxSave(sEmailArray[i], uoHiddenFieldUser.Value, sSubject,
                            sMsg, sEmailArray[i], sModuleID);
                    }

                    //Email cc
                    for (int i = 0; i < sEmailCCArray.Count(); i++)
                    {
                        WSConnection.PortalInboxSave(sEmailCCArray[i], uoHiddenFieldUser.Value, sSubject,
                            sMsg, sEmailCCArray[i], sModuleID);
                    }
                }
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }

            }
    }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/Apr/2013
        /// Description:    create the file to be exported
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  06/Aug/2013
        /// Description:    validate cost center if numeric or not
        ///                 Add style S65  to align all rows to Left
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strFileName"></param>
        /// <param name="dtCancelled"></param>
        public static void CreateExcel(DataTable dtSource, string strFileName, DataTable dtCancelled)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    string sBranchName = dtSource.Rows[0]["HotelBranch"].ToString().TrimEnd();
                    int iLength = sBranchName.Length;
                    int iLengthRemove = iLength - 20;
                    if (iLength > 20)
                    {
                        sBranchName = sBranchName.Remove(20, iLengthRemove);
                    }
                    int iColCount = dtSource.Columns.Count + 1;
                    //Format the output file for reading easier
                    xtwWriter.Formatting = Formatting.Indented;

                    // <?xml version="1.0"?>
                    xtwWriter.WriteStartDocument();

                    // <?mso-application progid="Excel.Sheet"?>
                    xtwWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

                    // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
                    xtwWriter.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

                    //Write definition of namespace
                    xtwWriter.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
                    xtwWriter.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
                    xtwWriter.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
                    xtwWriter.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

                    // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                    xtwWriter.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

                    // Write document properties
                    xtwWriter.WriteElementString("Author", "Travelmart");
                    xtwWriter.WriteElementString("LastAuthor", Environment.UserName);
                    xtwWriter.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
                    xtwWriter.WriteElementString("Company", "RCCL");
                    xtwWriter.WriteElementString("Version", "1");

                    // </DocumentProperties>
                    xtwWriter.WriteEndElement();

                    // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of workbook
                    xtwWriter.WriteElementString("WindowHeight", "13170");
                    xtwWriter.WriteElementString("WindowWidth", "17580");
                    xtwWriter.WriteElementString("WindowTopX", "120");
                    xtwWriter.WriteElementString("WindowTopY", "60");
                    xtwWriter.WriteElementString("ProtectStructure", "False");
                    xtwWriter.WriteElementString("ProtectWindows", "False");

                    // </ExcelWorkbook>
                    xtwWriter.WriteEndElement();

                    // <Styles>
                    xtwWriter.WriteStartElement("Styles");

                    // <Style ss:ID="Default" ss:Name="Normal">
                    xtwWriter.WriteStartElement("Style");
                    xtwWriter.WriteAttributeString("ss", "ID", null, "Default");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Normal");

                    // <Alignment ss:Vertical="Bottom"/>
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();

                    // Write null on the other properties
                    xtwWriter.WriteElementString("Borders", null);
                    xtwWriter.WriteElementString("Font", null);
                    xtwWriter.WriteElementString("Interior", null);
                    xtwWriter.WriteElementString("NumberFormat", null);
                    xtwWriter.WriteElementString("Protection", null);
                    // </Style>
                    xtwWriter.WriteEndElement();

                    //Style for header
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s62">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s62");
                    xtwWriter.WriteStartElement("Font");
                    // <Font ss:Bold="1"/>
                    xtwWriter.WriteAttributeString("ss", "Bold", null, "1");
                    //end of font
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    //Style for total summary numbers
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s64");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Right");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    //End Style for header
                    xtwWriter.WriteEndElement();


                    //Style for Rows
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s64">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s65");
                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement();
                    //End Style for Rows
                    xtwWriter.WriteEndElement();


                    // </Styles>
                    xtwWriter.WriteEndElement();

                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    xtwWriter.WriteAttributeString("ss", "Name", null, sBranchName);

                    // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                    xtwWriter.WriteStartElement("Table");

                    int iRow = dtSource.Rows.Count + 15;

                    xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                    xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                    xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                    xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                    xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                    //Header
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                    int i = 1;
                    foreach (DataColumn Header in dtSource.Columns)
                    {
                        if (i <= iColCount)
                        {
                            xtwWriter.WriteStartElement("Cell");
                            // xxx
                            xtwWriter.WriteStartElement("Data");
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            // Write content of cell
                            xtwWriter.WriteValue(Header.ColumnName);
                            xtwWriter.WriteEndElement();
                            xtwWriter.WriteEndElement();
                        }
                        i++;
                    }
                    xtwWriter.WriteEndElement();


                    // Run through all rows of data source
                    foreach (DataRow row in dtSource.Rows)
                    {
                        // <Row>
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s65");

                        i = 1;
                        // Run through all cell of current rows
                        foreach (object cellValue in row.ItemArray)
                        {
                            if (i <= iColCount)
                            {
                                // <Cell>
                                xtwWriter.WriteStartElement("Cell");

                                // <Data ss:Type="String">xxx</Data>
                                xtwWriter.WriteStartElement("Data");

                                if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNIGHTS" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                //check cost center if number or not
                                else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                {
                                    if (GlobalCode.Field2Int(cellValue) > 0)
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }
                                }
                                else
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                }

                                // Write content of cell
                                xtwWriter.WriteValue(cellValue);

                                // </Data>
                                xtwWriter.WriteEndElement();

                                // </Cell>
                                xtwWriter.WriteEndElement();
                            }
                            i++;
                        }
                        // </Row>
                        xtwWriter.WriteEndElement();

                    }

                    // </Table>
                    xtwWriter.WriteEndElement();

                    // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of page
                    xtwWriter.WriteStartElement("PageSetup");
                    xtwWriter.WriteStartElement("Header");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("Footer");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("PageMargins");
                    xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                    xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();

                    // <Selected/>
                    xtwWriter.WriteElementString("Selected", null);

                    // <Panes>
                    xtwWriter.WriteStartElement("Panes");

                    // <Pane>
                    xtwWriter.WriteStartElement("Pane");

                    // Write settings of active field
                    xtwWriter.WriteElementString("Number", "1");
                    xtwWriter.WriteElementString("ActiveRow", "1");
                    xtwWriter.WriteElementString("ActiveCol", "1");

                    // </Pane>
                    xtwWriter.WriteEndElement();

                    // </Panes>
                    xtwWriter.WriteEndElement();

                    // <ProtectObjects>False</ProtectObjects>
                    xtwWriter.WriteElementString("ProtectObjects", "False");

                    // <ProtectScenarios>False</ProtectScenarios>
                    xtwWriter.WriteElementString("ProtectScenarios", "False");

                    // </WorksheetOptions>
                    xtwWriter.WriteEndElement();

                    // </Worksheet>
                    xtwWriter.WriteEndElement();


                    if (dtCancelled.Rows.Count > 0)
                    {
                        //=======================================CANCELLED SHEET===============================================
                        #region COMPARED SHEET

                        iColCount = (dtCancelled.Columns.Count + 1);
                        iRow = dtCancelled.Rows.Count + 15;

                        // <Worksheet ss:Name="xxx">
                        xtwWriter.WriteStartElement("Worksheet");
                        xtwWriter.WriteAttributeString("ss", "Name", null, "Cancelled Manifest");

                        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                        xtwWriter.WriteStartElement("Table");

                        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");


                        //Header
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                        i = 1;
                        foreach (DataColumn Header in dtCancelled.Columns)
                        {
                            if (i <= iColCount)
                            {
                                xtwWriter.WriteStartElement("Cell");
                                // xxx
                                xtwWriter.WriteStartElement("Data");
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                // Write content of cell
                                xtwWriter.WriteValue(Header.ColumnName);
                                xtwWriter.WriteEndElement();
                                xtwWriter.WriteEndElement();
                            }
                            i++;
                        }
                        xtwWriter.WriteEndElement();


                        // Run through all rows of data source
                        foreach (DataRow row in dtCancelled.Rows)
                        {
                            // <Row>
                            xtwWriter.WriteStartElement("Row");

                            i = 1;
                            // Run through all cell of current rows
                            foreach (object cellValue in row.ItemArray)
                            {
                                if (i <= iColCount)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtCancelled.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                        dtCancelled.Columns[i - 1].Caption.ToUpper() == "HOTELNIGHTS" ||
                                        dtCancelled.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                        dtCancelled.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    //check cost center if number or not
                                    else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                    {
                                        if (GlobalCode.Field2Int(cellValue) > 0)
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                        }
                                    }
                                    else
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                    }
                                    // Write content of cell
                                    xtwWriter.WriteValue(cellValue);

                                    // </Data>
                                    xtwWriter.WriteEndElement();

                                    // </Cell>
                                    xtwWriter.WriteEndElement();
                                }
                                i++;
                            }
                            // </Row>
                            xtwWriter.WriteEndElement();
                        }
                        // </Table>
                        xtwWriter.WriteEndElement();

                        // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                        xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                        // Write settings of page
                        xtwWriter.WriteStartElement("PageSetup");
                        xtwWriter.WriteStartElement("Header");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("Footer");
                        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteStartElement("PageMargins");
                        xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                        xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                        xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteEndElement();

                        // <Selected/>
                        xtwWriter.WriteElementString("Selected", null);

                        // <Panes>
                        xtwWriter.WriteStartElement("Panes");

                        // <Pane>
                        xtwWriter.WriteStartElement("Pane");

                        // Write settings of active field
                        xtwWriter.WriteElementString("Number", "1");
                        xtwWriter.WriteElementString("ActiveRow", "1");
                        xtwWriter.WriteElementString("ActiveCol", "1");

                        // </Pane>
                        xtwWriter.WriteEndElement();

                        // </Panes>
                        xtwWriter.WriteEndElement();

                        // <ProtectObjects>False</ProtectObjects>
                        xtwWriter.WriteElementString("ProtectObjects", "False");

                        // <ProtectScenarios>False</ProtectScenarios>
                        xtwWriter.WriteElementString("ProtectScenarios", "False");

                        // </WorksheetOptions>
                        xtwWriter.WriteEndElement();

                        // </Worksheet>
                        xtwWriter.WriteEndElement();

                        #endregion
                        //=======================================CANCELLED SHEET===============================================
                    }

                    // </Workbook>
                    xtwWriter.WriteEndElement();

                    // Write file on hard disk
                    xtwWriter.Flush();
                    xtwWriter.Close();

                    //FileInfo FileName = new FileInfo(strFileName);
                    //FileStream fs = new FileStream(FileName.FullName, FileMode.Create);                
                    //fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSource != null)
                {
                    dtSource.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }

        /// <summary>
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  12/04/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath, bool bIsPDF)
        {
            string strScript = "CloseModal('../Extract/HotelManifest/" + strFileName + "');";
            if (bIsPDF)
            {
                ScriptManager.RegisterStartupScript(uoLinkButtonControlNo, this.GetType(), "CloseModal", strScript, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
            }
        }
        /// <summary>
        /// Modified By:    Josephine gad
        /// Date Modified:  03/10/2012
        /// Description:    User BindRegionList to bind Region List
        /// </summary>
        public void LoadDefaults(short LoadType)
        {
            if (LoadType == 0)
            {
                BindRegionList();
            }
            BindPortList();
        }

        /// <summary>
        /// Date Created:   05/07/2012
        /// Created by:     Jefferson Bermundo
        /// Description:    For Filtering based on port per region
        /// ------------------------
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

                uoDropDownListPort.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
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
                uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static DataTable ConvertListToDataTable(List<HotelDTO> list)
        {
            DataTable table = null;
            try
            {
                // New table.
                table = new DataTable();
                table.Columns.Add("BranchId");
                table.Columns.Add("BranchName");
                // Add rows.
                foreach (var array in list)
                {
                    table.Rows.Add(array.HotelIDString, array.HotelNameString);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (table != null)
                {
                    table.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
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
                uoHiddenFieldRegion.Value = uoDropDownListRegion.SelectedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       20/Mar/2013
        /// Description:        Get the List of ListView datasource
        /// ------------------------------------
        /// Modified By:        Josephine Monteza
        /// Date Modified:      29/Aug/2014
        /// Description:        Add parameter iNoOfDays
        /// ------------------------------------
        /// </summary>       
        public List<HotelManifest> GetHotelConfirmManifestList(string DateFromString,
           string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
           Int16 LoadType, string SortBy, Int32 iNoOfDays, string ListType, int StartRow, int MaxRow)
        {
            List<HotelManifest> TentativeManifest = new List<HotelManifest>();

            filterNameOrID = GlobalCode.Field2String(filterNameOrID);
            if (ListType == "New")
            {
                MBLL.GetHotelConfirmManifestPage(DateFromString, strUser, DateFilter,
                    ByNameOrID, filterNameOrID.TrimEnd(),
                    Nationality, Gender, Rank, Status,
                    Region, Country, City, Port,
                    Hotel, Vessel, UserRole, StartRow, MaxRow, LoadType,
                    SortBy, iNoOfDays);

                if (Session["ConfirmManifest_TentativeManifest"] != null)
                {
                    TentativeManifest = (List<HotelManifest>)Session["ConfirmManifest_TentativeManifest"];
                }
            }
            else if (ListType == "Confirm")
            {
                if (Session["ConfirmManifest_ConfirmedManifest"] != null)
                {
                    TentativeManifest = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
                }
            }
            else if (ListType == "Cancel")
            {
                if (Session["ConfirmManifest_CancelledManifest"] != null)
                {
                    TentativeManifest = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];
                }
            }
            return TentativeManifest;
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       20/Mar/2013
        /// Description:        Get the Record Count of ListView
        /// ------------------------------------
        /// </summary>   
        public int GetHotelConfirmManifestCount(string DateFromString,
           string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
           Int16 LoadType, string SortBy, Int32 iNoOfDays, string ListType)
        {
            int iTotalRow = 0;
            if (ListType == "New")
            {
                iTotalRow = GlobalCode.Field2Int(Session["ConfirmManifest_TentativeManifestCount"]);
            }
            else if (ListType == "Confirm")
            {
                iTotalRow = GlobalCode.Field2Int(Session["ConfirmManifest_ConfirmedManifestCount"]);

            }
            else if (ListType == "Cancel")
            {
                iTotalRow = GlobalCode.Field2Int(Session["ConfirmManifest_CancelledManifestCount"]);

            }
            return iTotalRow;
        }
        /// <summary>
        /// Date Created: 02/Apr/2013
        /// Created By:   Josephine Gad
        /// (description) Save/Send Email and confirm manifest  
        /// </summary>
        //protected void uoButtonSend_Click(object sender, EventArgs e)
        private void SendEmailButton(bool bIsSave)
        {
            DataTable dt = null;
            DataTable dtCancelled = null;

            try
            {
                string EmailTo = uoHiddenFieldTo.Value;
                string EmailCc = uoHiddenFieldCc.Value;

                if (EmailTo != "")
                {
                    ConfirmManifest(bIsSave, EmailTo, EmailCc);
                    //string sConfirmed = GlobalCode.Field2String(Session["NonTurnPortDateConfirmed"]);

                    List<HotelManifest> list = new List<HotelManifest>();
                    List<HotelManifest> listCancelled = new List<HotelManifest>();
                    list = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
                    listCancelled = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];

                    if (list.Count > 0 || listCancelled.Count > 0)
                    {
                        dt = GetConfirmedDataTable(list);
                        dtCancelled = GetCancelledDataTable(listCancelled);

                        CreateEmail(dt, dtCancelled, EmailTo, EmailCc);

                        GetSFHotelTravelDetails();
                        GetConfirmedList();
                        GetCancelledList();
                        BindControlNo();

                        AlertMessage("Confirmed Manifest Email Sent!");
                    }
                    else
                    {
                        AlertMessage("There is no manifest to email.");
                    }
                }
                else
                {
                    AlertMessage("Please specify at least one recipient.");
                }
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
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   21/Mar/2013
        /// description     Confirm Manifest
        ///---------------------------------------
        /// Modified By:     Josephine Gad
        /// Date Modified:   05/Sep/2014
        /// description      Add No. Of days
        ///---------------------------------------
        /// Modified By:     Josephine Monteza
        /// Date Modified:   16/June/2015
        /// description      Add DataTable of manifest to confirm
        /// </summary>
        private void ConfirmManifest(bool bIsSave, string sEmailTo, string sEmailCc)
        {
            DataTable dt = null;
            try
            {
                string strLogDescription;
                if (bIsSave)
                {
                    strLogDescription = "Save Hotel Branch Email,Confirm Hotel Manifest";
                }
                else
                {
                    strLogDescription = "Confirm Hotel Manifest";

                }
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                CheckBox listuoSelectCheckBox;

                Label listuoLblRecLoc;
                Label listuoLblSfID;
                Label listuoLblCheckIn;

                DataColumn colIDBigInt = new DataColumn("colIDBigInt", typeof(Int64));
                DataColumn colTravelReqId = new DataColumn("colTravelReqId", typeof(Int64));
                DataColumn colRecLoc = new DataColumn("colRecLoc", typeof(string));
                DataColumn colEmployeeId = new DataColumn("colEmployeeId", typeof(Int64));
                DataColumn colTimeSpanStartDate = new DataColumn("colTimeSpanStartDate", typeof(DateTime));
                DataColumn colTimeSpanEndDate = new DataColumn("colTimeSpanEndDate", typeof(DateTime));
                DataColumn colTimeSpanDuration = new DataColumn("colTimeSpanDuration", typeof(Int32));
                DataColumn colHotelBranchID = new DataColumn("colHotelBranchID", typeof(Int64));
                DataColumn colTransHotelID = new DataColumn("colTransHotelID", typeof(Int64));

                dt = new DataTable();
                dt.Columns.Add(colIDBigInt);
                dt.Columns.Add(colTravelReqId);
                dt.Columns.Add(colRecLoc);
                dt.Columns.Add(colEmployeeId);
                dt.Columns.Add(colTimeSpanStartDate);
                dt.Columns.Add(colTimeSpanEndDate);
                dt.Columns.Add(colTimeSpanDuration);
                dt.Columns.Add(colHotelBranchID);
                dt.Columns.Add(colTransHotelID);

                DataRow dRow = dt.NewRow();

                int iTotal = uolistviewHotelInfo.Items.Count;
                int iBranchID = GlobalCode.Field2Int(Session["Hotel"]);

                for (int i = 0; i < iTotal; i++)
                {
                    listuoSelectCheckBox = (CheckBox)uolistviewHotelInfo.Items[i].FindControl("uoSelectCheckBox");
                    listuoLblRecLoc = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLblRecLoc");
                    listuoLblSfID = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLblSfID");
                    listuoLblCheckIn = (Label)uolistviewHotelInfo.Items[i].FindControl("uoLblCheckIn");


                    if (listuoSelectCheckBox.Checked)
                    {
                        dRow = dt.NewRow();
                        dRow[colRecLoc] = listuoLblRecLoc.Text;
                        dRow[colEmployeeId] = listuoLblSfID.Text;
                        dRow[colTimeSpanStartDate] = GlobalCode.Field2DateTime(listuoLblCheckIn.Text);
                        dRow[colHotelBranchID] = iBranchID;

                        dt.Rows.Add(dRow);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    MBLL.ConfirmHotelManifest(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                        GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue), uoHiddenFieldRole.Value,
                        bIsSave, sEmailTo, sEmailCc,
                        strLogDescription, "ConfirmManifest",
                        Path.GetFileName(Request.UrlReferrer.AbsolutePath), CommonFunctions.GetDateTimeGMT(dateNow), dateNow,
                        GlobalCode.Field2Int(uoHiddenFieldNoOfDays.Value), dt);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
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
        /// Author:         Josephine Gad
        /// Date Created:   01/Apr/2013
        /// Description:    Convert COnfirmed HotelManifest to DataTable
        /// ==========================================================
        /// Date Modified:  14/May/2013
        /// Modified By:    Marco Abejar
        /// (description)  Add birthday field
        /// ==========================================================         
        /// Date Modified:  05/June/2013
        /// Modified By:    Marco Abejar
        /// (description)  Change costcenter to code
        /// ==========================================================  
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable GetConfirmedDataTable(List<HotelManifest> list)
        {
            DataTable dt = null;

            try
            {
                if (uoHiddenFieldIsVendor.Value == "false")
                {
                    var e = (from a in list
                             select new
                             {
                                 Remarks = GlobalCode.Field2String(a.Remarks),
                                 HotelCity = GlobalCode.Field2String(a.HotelCity),
                                 CheckIn = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckIn)),
                                 CheckOut = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckOut)),
                                 HotelNights = a.HotelNights,
                                 ReasonCode = GlobalCode.Field2String(a.ReasonCode),

                                 LastName = GlobalCode.Field2String(a.LastName),
                                 FirstName = GlobalCode.Field2String(a.FirstName),

                                 EmployeeId = a.EmployeeId.ToString(),
                                 Gender = GlobalCode.Field2String(a.Gender),

                                 SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                                 Couple = GlobalCode.Field2String(a.Couple),
                                 Title = GlobalCode.Field2String(a.Title),
                                 Ship = GlobalCode.Field2String(a.ShipCode),

                                 //CostCenter = GlobalCode.Field2String(a.CostCenter),
                                 CostCenter = GlobalCode.Field2String(a.CostCenterCode),
                                 Nationality = GlobalCode.Field2String(a.Nationality),
                                 HotelRequest = GlobalCode.Field2String(a.HotelRequest),
                                 RecordLocator = GlobalCode.Field2String(a.RecLoc),

                                 DeptCity = GlobalCode.Field2String(a.DeptCity),
                                 ArvlCity = GlobalCode.Field2String(a.ArvlCity),
                                 DeptDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.DeptDate)),
                                 ArvlDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ArvlDate)),
                                 DeptTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.DeptTime)),
                                 ArvlTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.ArvlTime)),

                                 Carrier = GlobalCode.Field2String(a.Carrier),
                                 FlightNo = GlobalCode.Field2String(a.FlightNo),
                                 Voucher = GlobalCode.Field2String(a.Voucher),

                                 PassportNo = GlobalCode.Field2String(a.PassportNo),
                                 IssuedDate = GlobalCode.Field2String(a.PasportDateIssued),
                                 PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),
                                 Birthday = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.Birthday)),

                                 HotelBranch = GlobalCode.Field2String(a.HotelBranch),
                                 //ConfirmedDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ConfirmedDate)),

                             }).ToList();
                    dt = getDataTable(e);
                }
                else
                {
                    var e = (from a in list
                             select new
                             {
                                 Remarks = GlobalCode.Field2String(a.Remarks),
                                 HotelCity = GlobalCode.Field2String(a.HotelCity),
                                 CheckIn = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckIn)),
                                 CheckOut = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckOut)),
                                 HotelNights = a.HotelNights,
                                 // ReasonCode = GlobalCode.Field2String(a.ReasonCode),

                                 LastName = GlobalCode.Field2String(a.LastName),
                                 FirstName = GlobalCode.Field2String(a.FirstName),

                                 EmployeeId = a.EmployeeId.ToString(),
                                 Gender = GlobalCode.Field2String(a.Gender),

                                 SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                                 Couple = GlobalCode.Field2String(a.Couple),
                                 Title = GlobalCode.Field2String(a.Title),
                                 Ship = GlobalCode.Field2String(a.ShipCode),

                                 //CostCenter = GlobalCode.Field2String(a.CostCenter),
                                 CostCenter = GlobalCode.Field2String(a.CostCenterCode),
                                 Nationality = GlobalCode.Field2String(a.Nationality),
                                 //HotelRequest = GlobalCode.Field2String(a.HotelRequest),
                                 RecordLocator = GlobalCode.Field2String(a.RecLoc),

                                 DeptCity = GlobalCode.Field2String(a.DeptCity),
                                 ArvlCity = GlobalCode.Field2String(a.ArvlCity),
                                 DeptDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.DeptDate)),
                                 ArvlDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ArvlDate)),
                                 DeptTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.DeptTime)),
                                 ArvlTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.ArvlTime)),

                                 Carrier = GlobalCode.Field2String(a.Carrier),
                                 FlightNo = GlobalCode.Field2String(a.FlightNo),
                                 Voucher = GlobalCode.Field2String(a.Voucher),

                                 PassportNo = GlobalCode.Field2String(a.PassportNo),
                                 IssuedDate = GlobalCode.Field2String(a.PasportDateIssued),
                                 PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),
                                 Birthday = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.Birthday)),

                                 HotelBranch = GlobalCode.Field2String(a.HotelBranch),
                                 //ConfirmedDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ConfirmedDate)),

                             }).ToList();
                    dt = getDataTable(e);
                }
                return dt;
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
        /// Author:         Josephine Gad
        /// Date Created:   01/Apr/2013
        /// Description:    Convert Cancelled HotelManifest to DataTable 
        /// ==========================================================
        /// Date Modified:  14/May/2013
        /// Modified By:    Marco Abejar
        /// (description)  Add birthday column
        /// ==========================================================                 
        /// Date Modified:  05/June/2013
        /// Modified By:    Marco Abejar
        /// (description)  Change costcenter to code
        /// ==========================================================  
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable GetCancelledDataTable(List<HotelManifest> list)
        {
            DataTable dt = null;

            try
            {
                if (uoHiddenFieldIsVendor.Value == "false")
                {
                    var e = (from a in list
                             select new
                             {
                                 HotelCity = GlobalCode.Field2String(a.HotelCity),
                                 CheckIn = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckIn)),
                                 CheckOut = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckOut)),
                                 HotelNights = a.HotelNights,
                                 ReasonCode = GlobalCode.Field2String(a.ReasonCode),
                                 LastName = GlobalCode.Field2String(a.LastName),
                                 FirstName = GlobalCode.Field2String(a.FirstName),

                                 EmployeeId = a.EmployeeId.ToString(),
                                 Gender = GlobalCode.Field2String(a.Gender),
                                 Birthday = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.Birthday)),
                                 SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                                 Couple = GlobalCode.Field2String(a.Couple),
                                 Title = GlobalCode.Field2String(a.Title),
                                 Ship = GlobalCode.Field2String(a.ShipCode),

                                 //CostCenter = GlobalCode.Field2String(a.CostCenter),
                                 CostCenter = GlobalCode.Field2String(a.CostCenterCode),
                                 Nationality = GlobalCode.Field2String(a.Nationality),
                                 HotelRequest = GlobalCode.Field2String(a.HotelRequest),
                                 RecordLocator = GlobalCode.Field2String(a.RecLoc),

                                 DeptCity = GlobalCode.Field2String(a.DeptCity),
                                 ArvlCity = GlobalCode.Field2String(a.ArvlCity),
                                 DeptDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.DeptDate)),
                                 ArvlDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ArvlDate)),
                                 DeptTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.DeptTime)),
                                 ArvlTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.ArvlTime)),

                                 Carrier = GlobalCode.Field2String(a.Carrier),
                                 FlightNo = GlobalCode.Field2String(a.FlightNo),
                                 Voucher = GlobalCode.Field2String(a.Voucher),

                                 PassportNo = GlobalCode.Field2String(a.PassportNo),
                                 IssuedDate = GlobalCode.Field2String(a.PasportDateIssued),
                                 PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),

                                 HotelBranch = GlobalCode.Field2String(a.HotelBranch),

                             }).ToList();
                    dt = getDataTable(e);
                }
                else
                {
                    var e = (from a in list
                             select new
                             {
                                 HotelCity = GlobalCode.Field2String(a.HotelCity),
                                 CheckIn = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckIn)),
                                 CheckOut = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckOut)),
                                 HotelNights = a.HotelNights,
                                 //ReasonCode = GlobalCode.Field2String(a.ReasonCode),
                                 LastName = GlobalCode.Field2String(a.LastName),
                                 FirstName = GlobalCode.Field2String(a.FirstName),

                                 EmployeeId = a.EmployeeId.ToString(),
                                 Gender = GlobalCode.Field2String(a.Gender),
                                 Birthday = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.Birthday)),
                                 SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                                 Couple = GlobalCode.Field2String(a.Couple),
                                 Title = GlobalCode.Field2String(a.Title),
                                 Ship = GlobalCode.Field2String(a.ShipCode),

                                 //CostCenter = GlobalCode.Field2String(a.CostCenter),
                                 CostCenter = GlobalCode.Field2String(a.CostCenterCode),
                                 Nationality = GlobalCode.Field2String(a.Nationality),
                                 //HotelRequest = GlobalCode.Field2String(a.HotelRequest),
                                 RecordLocator = GlobalCode.Field2String(a.RecLoc),

                                 DeptCity = GlobalCode.Field2String(a.DeptCity),
                                 ArvlCity = GlobalCode.Field2String(a.ArvlCity),
                                 DeptDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.DeptDate)),
                                 ArvlDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ArvlDate)),
                                 DeptTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.DeptTime)),
                                 ArvlTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.ArvlTime)),

                                 Carrier = GlobalCode.Field2String(a.Carrier),
                                 FlightNo = GlobalCode.Field2String(a.FlightNo),
                                 Voucher = GlobalCode.Field2String(a.Voucher),

                                 PassportNo = GlobalCode.Field2String(a.PassportNo),
                                 IssuedDate = GlobalCode.Field2String(a.PasportDateIssued),
                                 PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),

                                 HotelBranch = GlobalCode.Field2String(a.HotelBranch),

                             }).ToList();
                    dt = getDataTable(e);
                }
                return dt;
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
        /// Created By:     Josephine Gad
        /// Date Created:   17/Mar/2013
        /// description     Email Manifest
        /// </summary>
        private void EmailManifest(string sSubject, string sMessage, string attachment1, string attachment2,
            string EmailVendor, string EmailCc, string file)
        {
            string sBody;
            try
            {
                string sPort = uoDropDownListPort.SelectedItem.Text;
                string[] sPortArr = sPort.Split("-".ToCharArray());

                sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                sBody += "Dear Hotel,  <br/><br/> " + sMessage;
                sBody += "</TR></TD></TABLE>";

                if (EmailVendor != "")
                {
                    string attachment = attachment1 + ";" + attachment2;
                    CommonFunctions.SendEmailWithAttachment("", EmailVendor, EmailCc, sSubject, sBody, attachment.TrimEnd(';'));
                }

                //Insert Email logs
                //CommonFunctions.InsertEmailLog(EmailVendor, EmailCc, "travelmart.ptc@gmail.com", sSubject, file, DateTime.Now, uoHiddenFieldUser.Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2013
        /// Description:    get user and branch info
        /// --------------------------------        
        ///  
        /// </summary>
        protected void GetUserBranchInfo()
        {
            List<UserAccountList> userAccount = UserAccountBLL.GetUserInfoListByName(uoHiddenFieldUser.Value);
            Session["UserAccountList"] = userAccount;

            List<UserPrimaryDetails> userDetails = (List<UserPrimaryDetails>)Session["UserPrimaryDetails"];

            Session["UserBranchID"] = userDetails[0].iBranchID;
            Session["BranchName"] = userDetails[0].sBranchName;
            Session["VendorID"] = userDetails[0].iVendorID;

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
                list = UserAccountBLL.GetUserInfoListByName(sUserName);
                Session["UserAccountList"] = list;
            }
            return list;
        }

        string lastDataFieldValueVendor = null;
        protected string DashboardAddGroup()
        {
            string GroupValueString = "HotelBranchName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();
            if (currentDataFieldValue != "")
            {
                uoHiddenFieldHotelName.Value = currentDataFieldValue;
            }

            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            if (lastDataFieldValueVendor != currentDataFieldValue)
            {
                lastDataFieldValueVendor = currentDataFieldValue;

                string sEvent = "";

                string sContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"View Contract\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractId") + "\")'\"><img ID=\"uoImageContract\" src=\"../Images/contract.jpg\" Width=\"20px\" alt=\"View Contract\" border=\"0\"/></a> " + sEvent + "</td>";
                //string sNoContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"No Contract\"><img ID=\"uoImageContract\"  visible=\"false\" src=\"../Images/contract.jpg\" Width=\"20px\" alt=\"No Contract\" border=\"0\"/></a> " + sEvent + "</td>";
                string sNoContract = "<td class=\"tdEvent\"><a id=\"uoContract\" class=\"rightAligned\" title=\"No Contract\"></a> " + sEvent + "</td>";

                string sResult = "";
                if (Eval("IsWithContract").ToString() == "True")
                {
                    sResult = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><span class=\"leftAligned\">{0}</span></strong></td>" + sContract + "</tr>", currentDataFieldValue);
                }
                else
                {
                    sResult = string.Format("<tr><td class=\"group\" colspan=\"3\"><strong><a class=\"groupLink\"><a class=\"leftAligned\" href=\"#\">{0}<a/></strong></td>" + sNoContract + "</tr>", currentDataFieldValue);
                }
                return sResult;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Date Modified:  26/Apr/2013
        /// Modified By:    Josephine Gad
        /// (description)   Remove GetConfirmBooking and assign the datasource directly
        /// ------------------------------------------- 
        /// Date Modified:  11/Sep/2014
        /// Modified By:    Josephine Gad
        /// (description)   Change SeafarerTravelBLL.InsertTag to MBLL.TagToHotel, table used was tblTag to TblTag_Hotel 
        /// ------------------------------------------- 
        /// </summary>
        /// <param name="sIdBigint"></param>
        /// <param name="sTRId"></param>
        /// <param name="sBranch"></param>
        private void TagSeafarer(string sIdBigint, string sTRId, string sBranch, string sRecLoc, string sE1Id, string sStatusOnOff)
        {
            string sUser = uoHiddenFieldUser.Value;
            string sRole = uoHiddenFieldRole.Value;

            string strLogDescription = "Tag Seafarer To Hotel Vendor";
            string strFunction = "TagSeafarer";
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //SeafarerTravelBLL.InsertTag(sIdBigint, sTRId, sUser, sRole, "0", "0", sBranch, uoHiddenFieldTagTime.Value,
            //    sRecLoc, sE1Id, sStatusOnOff, strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //             TimeZone.CurrentTimeZone.StandardName.ToString(), CommonFunctions.GetDateTimeGMT(dateNow));

            MBLL.TagToHotel(GlobalCode.Field2Long(sIdBigint), GlobalCode.Field2Long(sTRId), sRecLoc,
                GlobalCode.Field2Long(sE1Id), sStatusOnOff, GlobalCode.Field2Long(uoDropDownListHotel.SelectedValue), 0,
                uoHiddenFieldUser.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path),
                CommonFunctions.GetDateTimeGMT(dateNow), dateNow);

            if (sBranch != "0")
            {
                GetSFHotelTravelDetails();
                GetConfirmedList();
                GetCancelledList();
            }
        }
        /// Date Created:   11/Jun/2013
        /// Created By:     Josephine Gad
        /// (description)   Settings if header Tag is visible or hidden
        /// ------------------------------------------- 
        private void ControlSettings()
        {
            HtmlControl TagTH = (HtmlControl)uoListViewHeaderConfirmed.Controls[0].FindControl("TagTH");
            HtmlControl CancelBoxTH = (HtmlControl)uoListViewHeaderConfirmed.Controls[0].FindControl("CancelBoxTH");

            if (TagTH != null)
            {
                if (User.IsInRole( TravelMartVariable.RoleHotelVendor))
                {
                    TagTH.Visible = true;
                }
                else
                {
                    TagTH.Visible = false;
                }
            }
            if (CancelBoxTH != null)
            {
                if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist) ||
                    User.IsInRole(TravelMartVariable.RoleAdministrator))
                {
                    CancelBoxTH.Visible = true;
                }
                else
                {
                    CancelBoxTH.Visible = false;
                }
            }
        }
        /// Date Created:   21/Jan/2015
        /// Created By:     Josephine Monteza
        /// (description)   Settings if header ChangeName is visible or hidden
        /// ------------------------------------------- 
        //private void ControlSettingsChangeName()
        //{
        //    //HtmlControl ChangeNameTH = (HtmlControl)uoListViewHeaderConfirmed.Controls[0].FindControl("ChangeNameTH");
        //    //if (ChangeNameTH != null)
        //    //{
        //    //    if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
        //    //    {
        //    //        ChangeNameTH.Visible = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        ChangeNameTH.Visible = false;
        //    //    }
        //    //}
        //}

        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Muhallidin G wali
        /// (description) Hide Cancel button Hotel  
        /// -------------------------------------------
        /// Date Modifed:   13/07/2015
        /// Modifed By:     Josephine Monteza
        /// (description)   Add RoleAdministrator and leftAligned class
        /// </summary>
        protected string HideCancelHotel()
        {
            if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist) || User.IsInRole(TravelMartVariable.RoleAdministrator))
            {
                return "";
            }
            else
            {
                return "hideElement";
            }
        }


        /// <summary>
        /// Date Created:   08/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Change the backgroung color of old record
        /// ==============================================
        /// Date Modified:  20/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   add branch id parameter
        /// </summary>
        protected bool InactiveControl(object BranchID, object Type)
        {
            if (Type.ToString() == "HO")
            {
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Date Created:    16/May/2014
        /// Created By:      Josephine Gad
        /// (description)    Cancel button settings
        /// ==============================================
        /// Date Modified:   27/May/2014
        /// Modified By:     Josephine Gad
        /// (description)    Allow all role to cancel manifest
        /// ==============================================
        /// </summary>
        private void CancelButtonSettings()
        {
            if ((uolistviewHotelInfo.Items.Count > 0 || uoListViewManifestConfirmed.Items.Count > 0) &&
                GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue) > 1)
            //(
            //    uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
            //    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator)
            //)
            {
                uoButtonHotelBookingCancel.Visible = true;
            }
            else
            {
                uoButtonHotelBookingCancel.Visible = false;
            }
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   28/Aug/2014
        /// Descrption:     Bind No. Of Days
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindNoOfDays()
        {
            if (GlobalCode.Field2String(uoDropDownListDays.SelectedValue) == "")
            {

                uoDropDownListDays.Items.Clear();
                uoDropDownListDays.Items.Add(new ListItem("--Select No. of Days--", "0"));

                int iNoOfdays = TMSettings.NoOfDays;
                ListItem item;
                for (int i = 1; i <= iNoOfdays; i++)
                {
                    item = new ListItem(i.ToString(), i.ToString());
                    uoDropDownListDays.Items.Add(item);
                }

                uoDropDownListDays.DataBind();
                if (Session["NoOfDays_Hotel"] != null)
                {
                    string sDay = GlobalCode.Field2Int(Session["NoOfDays_Hotel"]).ToString();
                    if (uoDropDownListDays.Items.FindByValue(sDay) != null)
                    {
                        uoDropDownListDays.SelectedValue = sDay;
                    }
                }
                Session["NoOfDays_Hotel"] = uoDropDownListDays.SelectedValue;
                uoHiddenFieldNoOfDays.Value = uoDropDownListDays.SelectedValue;

                uoHiddenFieldHotelSpecialistEmail.Value = TravelMartVariable.HotelSpecialistEmail;
            }
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Monteza
        /// Date Created:   28/Nov/2014
        /// Descrption:     Bind Control No
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindControlNo()
        {
            uoLabelControlNo.Text = "";
            uoLinkButtonControlNo.Text = "";
            uoLabelControlNo.Visible = false;
            uoLinkButtonControlNo.Visible = false;

            //List<HotelManifest> listConfirmed = new List<HotelManifest>();
            //List<HotelManifest> listCancelled = new List<HotelManifest>();

            //if (Session["ConfirmManifest_ConfirmedManifest"] != null)
            //{
            //    listConfirmed = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];
            //}
            //if (Session["ConfirmManifest_CancelledManifest"] != null)
            //{
            //    listCancelled = (List<HotelManifest>)Session["ConfirmManifest_CancelledManifest"];
            //}


            if (GlobalCode.Field2Int(uoDropDownListDays.SelectedValue) == 0 &&
                (uoListViewManifestConfirmed.Items.Count > 0 || uoListViewCancelled.Items.Count > 0)
                //(listConfirmed.Count > 0 || listCancelled.Count > 0)
               )
            {
                int iBrandId = GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue);
                MBLL.GetHotelControlNo(iBrandId, uoHiddenFieldDate.Value);

                List<HotelControlNo> listControl = new List<HotelControlNo>();
                List<HotelCancelationTerms> listCancelTerms = new List<HotelCancelationTerms>();

                listControl = (List<HotelControlNo>)Session["ConfirmManifest_ControlNo"];
                listCancelTerms = (List<HotelCancelationTerms>)Session["ConfirmManifest_Cancelterms"];

                if (listControl.Count > 0)
                {
                    uoLinkButtonControlNo.Visible = true;
                    uoLinkButtonControlNo.Text = "Control# " + listControl[0].ControlNumber;
                    uoHiddenFieldContolID.Value = GlobalCode.Field2Int(listControl[0].ControlID).ToString();
                }
                else
                {

                    string sAvailableAt = "";
                    DateTime dYesterday = DateTime.Now.AddDays(-1);
                    dYesterday = GlobalCode.Field2DateTime(dYesterday.ToShortDateString());

                    //if date of manifest is yesterday or more
                    if (listCancelTerms.Count > 0 && 
                        (GlobalCode.Field2DateTime(uoHiddenFieldDate.Value) >= dYesterday))
                    {
                        double dHrs = GlobalCode.Field2Double(listCancelTerms[0].CancelationHours); if (listCancelTerms[0].CutoffTime != null)
                        {
                            DateTime dDate = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
                            string sDate = GlobalCode.Field2DateTime(GlobalCode.Field2Date(dDate.AddHours(-dHrs))).ToString("MMM dd, yyy");

                            DateTime dCutOff = GlobalCode.Field2DateTime(listCancelTerms[0].CutoffTime);


                            sAvailableAt = "Control # will be available " + sDate + " at " + string.Format("{0:hh:mm tt}", dCutOff)
                                + " " + listCancelTerms[0].sTimezone;

                            uoLabelControlNo.Visible = true;
                            uoLabelControlNo.Text = uoLabelControlNo.Text + sAvailableAt;
                        }
                    }
                    else if (listCancelTerms.Count > 0 &&
                        (GlobalCode.Field2DateTime(uoHiddenFieldDate.Value) < dYesterday))
                    {
                        if (listCancelTerms.Count > 0)
                        {
                            if (listCancelTerms[0].sTimezone != "")
                            {
                                uoLabelControlNo.Visible = true;
                                uoLabelControlNo.Text = "Cannot generate Control No. for past dated record!";
                            }                           
                        }
                    }
                    else
                    {
                        uoLabelControlNo.Text = "";
                    }
                }
            }
        }
        /// <summary>        
        /// Date Created:   04/Dec/2014
        /// Created By:     Josephine Monteza
        /// (description)   Create PDF file based from Datatable      
        /// </summary>
        private void CreatePDF(string sDateOnly, string PDFFileName, DataTable dt, DataTable dtCancel)
        {
            try
            {
                // step 1: creation of a document-object            
                using (PDF.text.Document document = new PDF.text.Document(PDF.text.PageSize.A2.Rotate(), 5, 5, 50, 10))
                {

                    // step 2: we create a writer that listens to the document            
                    PDF.text.pdf.PdfWriter.GetInstance(document, new FileStream(PDFFileName, FileMode.Create));

                    document.Open();

                    // step 4: we add content to the document                    
                    CreatePages(document, dt, sDateOnly, dtCancel);

                    // step 5: we close the document
                    document.Close();
                }

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
                if (dtCancel != null)
                {
                    dtCancel.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create PDF pages
        /// </summary>
        /// <param name="document"></param>
        /// <param name="dt"></param>
        /// <param name="sDateOnly"></param>
        public void CreatePages(PDF.text.Document document, DataTable dt, string sDateOnly, DataTable dtCancel)
        {
            try
            {
                if (dt.Rows.Count > 0 || dtCancel.Rows.Count > 0)
                {
                    string sHeader = "Hotel Manifest "
                           + uoLinkButtonControlNo.Text + ": " + uoDropDownListHotel.SelectedItem.Text + " (" + sDateOnly + ")";

                    bool first = true;
                    if (first)
                        first = false;
                    else
                        document.NewPage();

                    document.Add(FormatHeaderPhrase(sHeader));
                }

                if (dt.Rows.Count > 0)
                {
                    int iColCount = dt.Columns.Count;

                    iTextSharp.text.pdf.PdfPTable pdfTable = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count); //-2
                    pdfTable.DefaultCell.Padding = 1;
                    pdfTable.WidthPercentage = 100; // percentage
                    pdfTable.DefaultCell.BorderWidth = 1;
                    //pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_LEFT;

                    int iCol = 1;
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (iCol <= iColCount)
                        {
                            pdfTable.AddCell(FormatHeaderTable(column.ColumnName));
                        }
                        iCol++;
                    }

                    pdfTable.HeaderRows = 1;  // this is the end of the table header
                    pdfTable.DefaultCell.BorderWidth = 1;

                    PDF.text.BaseColor altRow = new PDF.text.BaseColor(242, 242, 242);


                    int i = 0;
                    int iRow = 0;

                    //Row
                    foreach (DataRow row in dt.Rows)
                    {
                        i++;
                        if (i % 2 == 1)
                        {
                            pdfTable.DefaultCell.BackgroundColor = altRow;
                        }
                        //Each column of row
                        iCol = 1;
                        foreach (object cell in row.ItemArray)
                        {
                            if (iCol <= iColCount)
                            {    //assume toString produces valid output
                                pdfTable.AddCell(FormatPhrase(cell.ToString()));
                            }
                            iCol++;
                        }
                        if (i % 2 == 1)
                        {
                            pdfTable.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                        }

                        iRow++;
                    }


                    //Border and background settings
                    pdfTable.DefaultCell.Border = 0;
                    pdfTable.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                    pdfTable.DefaultCell.BorderColor = PDF.text.BaseColor.WHITE;
                    //Blank Row
                    iTextSharp.text.pdf.PdfPCell BlankRow = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(""));
                    BlankRow.Colspan = iColCount;
                    BlankRow.Border = 0;
                    pdfTable.AddCell(BlankRow);

                    document.Add(pdfTable);
                }

                //Cancelled Manifest
                if (dtCancel.Rows.Count > 0)
                {
                    int iColCount = dtCancel.Columns.Count;


                    document.Add(new PDF.text.Paragraph(""));
                    document.Add(FormatHeaderPhrase("Cancelled Manifest"));

                    iTextSharp.text.pdf.PdfPTable pdfTableCancel = new iTextSharp.text.pdf.PdfPTable(dtCancel.Columns.Count); //-2
                    pdfTableCancel.DefaultCell.Padding = 1;
                    pdfTableCancel.WidthPercentage = 100; // percentage
                    pdfTableCancel.DefaultCell.BorderWidth = 1;
                    //pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_CENTER;
                    pdfTableCancel.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_LEFT;




                    int iCol = 1;
                    foreach (DataColumn column in dtCancel.Columns)
                    {
                        if (iCol <= iColCount)
                        {
                            pdfTableCancel.AddCell(FormatHeaderTable(column.ColumnName));
                        }
                        iCol++;
                    }

                    pdfTableCancel.HeaderRows = 1;  // this is the end of the table header
                    pdfTableCancel.DefaultCell.BorderWidth = 1;

                    PDF.text.BaseColor altRow = new PDF.text.BaseColor(242, 242, 242);


                    int i = 0;
                    int iRow = 0;

                    //Row
                    foreach (DataRow row in dtCancel.Rows)
                    {

                        i++;
                        if (i % 2 == 1)
                        {
                            pdfTableCancel.DefaultCell.BackgroundColor = altRow;
                        }
                        //Each column of row
                        iCol = 1;
                        foreach (object cell in row.ItemArray)
                        {
                            if (iCol <= iColCount)
                            {    //assume toString produces valid output
                                pdfTableCancel.AddCell(FormatPhrase(cell.ToString()));
                            }
                            iCol++;
                        }
                        if (i % 2 == 1)
                        {
                            pdfTableCancel.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                        }

                        iRow++;
                    }

                    //Border and background settings
                    pdfTableCancel.DefaultCell.Border = 0;
                    pdfTableCancel.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                    pdfTableCancel.DefaultCell.BorderColor = PDF.text.BaseColor.WHITE;

                    //Blank Row
                    iTextSharp.text.pdf.PdfPCell BlankRow = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(""));
                    BlankRow.Colspan = iColCount;
                    BlankRow.Border = 0;
                    pdfTableCancel.AddCell(BlankRow);

                    document.Add(pdfTableCancel);
                }
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
                if (dtCancel != null)
                {
                    dtCancel.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   04/Dec/2014
        /// Created By:     Josephine Monteza
        /// (description)   Format the phrase. Apply font and size here.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static PDF.text.Phrase FormatHeaderTable(string value)
        {
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6, PDF.text.Font.BOLD));
        }
        private static PDF.text.Phrase FormatPhrase(string value)
        {
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6));
        }
        private static PDF.text.Phrase FormatHeaderPhrase(string value)
        {
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 10, PDF.text.Font.BOLD, new PDF.text.BaseColor(0, 0, 255)));
        }



        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   21/Jan/2015
        /// Descrption:     Check checkin date to cancel request.
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static int CheckCancellationCheckinDate(string cTHrs, string cTZ, string cCOT)
        {
            try
            {

                DateTime TimeZoneDate = GlobalCode.Field2TimeZoneTime(DateTime.Now, cTZ);

                // Difference in days, hours, and minutes.
                TimeSpan ts = GlobalCode.Field2DateTime(cCOT) - TimeZoneDate;
                // Difference in days.
                double differenceInDays = ts.TotalHours;

                if (differenceInDays <= GlobalCode.Field2Double(cTHrs))
                    return 0;
                else
                    return 1;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

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

namespace TRAVELMART.Hotel
{
    public partial class HotelConfirmManifestForecast : System.Web.UI.Page
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
            
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); 
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                HtmlTable uoTblDate = (HtmlTable)Master.FindControl("uoTblDate");
                uoTblDate.Style.Add("display", "none");

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
                //GetVessel();
                GetHotelFilter();

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    uoDivRegionPort.Visible = false;
                }               

                //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                //    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                //    uoHiddenFieldRole.Value == TravelMartVariable.RoleFinance)
                //{
                //    uoHyperLinkSendEmails.Visible = true;
                //}
                //else
                //{
                //    uoHyperLinkSendEmails.Visible = false;
                //}               

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
                 uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                 uoHiddenFieldRole.Value == TravelMartVariable.RoleFinance)
                {
                    uoDivHotelSpecialist.Visible = true;                    
                    //uoButtonConfirmByVendor.Visible = false;
                    //uoHyperLinkSendEmails.Visible = true;
                    uoHiddenFieldIsVendor.Value = "false";
                }
                else if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    uoDivHotelSpecialist.Visible = false;                    
                    //uoButtonConfirmByVendor.Visible = true;
                    //uoHyperLinkSendEmails.Visible = false;
                    uoHiddenFieldIsVendor.Value = "true";

                    uoListViewHeaderConfirmed.DataSource = null;
                    uoListViewHeaderConfirmed.DataBind();

                    uoListViewHeaderCancelled.DataSource = null;
                    uoListViewHeaderCancelled.DataBind();

                    ControlSettings();

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
                    //uoButtonConfirmByVendor.Visible = false;
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
                    //GetSFHotelTravelDetails();
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
                //GetSFHotelTravelDetails();
                GetConfirmedList();
                GetCancelledList();

                uoHiddenFieldConfirmation.Value = "0";
                Session["ConfirmationTag"] = "0";
            }
        }

        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //uoHiddenFieldLoadType.Value = "1";
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;

            uoHiddenFieldLoadType.Value = "0";
            //GetSFHotelTravelDetails();
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(uoTextBoxFrom.Text).ToString("MM/dd/yyyy");
            uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(uoTextBoxToDate.Text).ToString("MM/dd/yyyy");
            GetConfirmedList();
            GetCancelledList();
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
        }

        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPort.SelectedValue;
            uoHiddenFieldPort.Value = uoDropDownListPort.SelectedValue;
            LoadDefaults(1);
            GetHotelFilter();
        }
        protected void uoBtnView_Click(object sender, EventArgs e)
        {
            GetConfirmedList();
            GetCancelledList(); ;
        }
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dtCancelled = null;

            try
            {
                MBLL.GetHotelConfirmManifestExport(uoHiddenFieldDate.Value,  uoHiddenFieldUser.Value,
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
        /// Author By:    Muhallidin G Wali
        /// Modified Date:  03/09/2013
        /// Description:   Cancel manifest (to be continue)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void uolistviewHotelInfo_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;
        //    SuperViewBLL svbll = new SuperViewBLL();
        //    string info = e.CommandArgument.ToString();

        //    string[] arg = new string[2];
        //    char[] splitter = { ';' };
        //    arg = info.Split(splitter);
        //    string IDBigInt = arg[0];
        //    string SeqNo = arg[1];
        //    string TransHotelIDBigInt = arg[2];

        //    if (e.CommandName == "Delete")
        //    {


        //        //if (Convert.ToInt32(IDBigInt) == 0)
        //        //{
        //        //    HotelBLL.DeleteHotelBookingOther(Convert.ToInt32(TransHotelIDBigInt), GlobalCode.Field2String(Session["UserName"]));

        //        //    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
        //        //    strLogDescription = "Hotel transaction deleted - Non Sabre feed. (flagged as inactive)";
        //        //    strFunction = "uolistviewHotelInfo_ItemCommand";

        //        //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //        //    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(TransHotelIDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //        //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        //        //}
        //        //else
        //        //{
        //        //    HotelBLL.DeleteHotelBooking(Convert.ToInt32(IDBigInt), GlobalCode.Field2String(Session["UserName"]), Convert.ToInt32(SeqNo));

        //        //    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
        //        //    strLogDescription = "Hotel transaction deleted - Sabre feed. (flagged as inactive)";
        //        //    strFunction = "uolistviewHotelInfo_ItemCommand";

        //        //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //        //    BLL.AuditTrailBLL.InsertLogAuditTrail(Int32.Parse(IDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //        //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        //        //}
        //        //GetSFHotelDetails();

        //        //Messagebox.show("Are you sure");
        //        //Page.RegisterClientScriptBlock("mes", "<script language='javascript'>alert('Are you sure')</script>");

        //        //string script = "alert('New Password is Created');\n";
        //        //script += "var f='" + Session["flag"].ToString() + "';\n";
        //        //script += "if (f == 'L')\n";
        //        //script += "location.href='loginpage.aspx';\n";

        //        //Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", script, true);

        //        long SeafarerID = GlobalCode.Field2Long(Request.QueryString["sfId"]);
        //        long TRID = GlobalCode.Field2Long(Request.QueryString["trID"]);
        //        DataTable dt = svbll.GetCancelHotelTransactionOther(GlobalCode.Field2Long(TransHotelIDBigInt), TRID, SeafarerID);

        //        uolistviewHotelInfo.DataSource = dt;
        //        uolistviewHotelInfo.DataBind();

        //    }

        //    else
        //    {
        //        HiddenField hfIdBigint = (HiddenField)e.Item.FindControl("hfIdBigint");
        //        HiddenField hfSeqNo = (HiddenField)e.Item.FindControl("hfSeqNo");
        //        EditSeafarer(uclabelE1ID.Text, TransHotelIDBigInt, hfIdBigint.Value, hfSeqNo.Value);
        //    }
        //}



        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Feb/2013
        /// Description:    Get the order to be used
        /// -------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    uoHiddenFieldSortBy.Value = e.CommandName;
        //    GetSFHotelTravelDetails();
        //    //GetOverflow();
        //}

        protected void uoListViewHeaderConfirmed_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            uoDataPagerConfirmed.SetPageProperties(0, uoDataPagerConfirmed.PageSize, false);
            uoDataPagerCancelled.SetPageProperties(0, uoDataPagerCancelled.PageSize, false);
            GetConfirmedList();
            GetCancelledList();
//            GetSFHotelTravelDetails();  
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
        }
        
        protected void uoListViewManifestConfirmed_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //MBLL.GetHotelConfirmManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Confirm");
        }
        protected void uoListViewCancelled_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //MBLL.GetHotelConfirmManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Cancel");
        }
        #endregion


        #region Functions
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
            
            Session["strPrevPage"] = Request.RawUrl;

            //ListView1.DataSource = null;
            //ListView1.DataBind();

            uoListViewHeaderConfirmed.DataSource = null;
            uoListViewHeaderConfirmed.DataBind();

            ControlSettings();

            uoListViewHeaderCancelled.DataSource = null;
            uoListViewHeaderCancelled.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2013
        /// Description:    Bind the Tentative Manifest and Hotel List 
        /// -------------------------------------
        /// </summary>
        //private void GetSFHotelTravelDetails()
        //{           
        //    uolistviewHotelInfo.DataSource = null;
        //    uolistviewHotelInfo.DataSourceID = "uoObjectDataSourceManifest";
        //    uolistviewHotelInfo.DataBind();

        //    //EmailSettings();
        //}
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
        //private void EmailSettings()
        //{
        //    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
        //        uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
        //        uoHiddenFieldRole.Value == TravelMartVariable.RoleFinance)
        //    {
        //        if (GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue) > 0)
        //        {
        //            //uoHyperLinkSendEmails.Visible = true;
        //            //uoBtnExportList.Visible = true;

        //            if (Session["ConfirmManifest_EmailRecipient"] != null)
        //            {
        //                List<EmailRecipient> email = (List<EmailRecipient>)Session["ConfirmManifest_EmailRecipient"];
        //                if (email.Count > 0)
        //                {
        //                    uoTextBoxTo.Text = email[0].EmailTo;
        //                    uoTextBoxCc.Text = email[0].EmailCc;
        //                    //uoHiddenFieldTo.Value = email[0].EmailTo;
        //                    //uoHiddenFieldCc.Value = email[0].EmailCc;
        //                }
        //                else
        //                {
        //                    uoTextBoxTo.Text = "";
        //                    uoTextBoxCc.Text = "";
        //                    uoHiddenFieldTo.Value = "";
        //                    uoHiddenFieldCc.Value = "";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //uoHyperLinkSendEmails.Visible = false;
        //            // uoBtnExportList.Visible = false;
        //            uoTextBoxTo.Text = "";
        //            uoTextBoxCc.Text = "";
        //            uoHiddenFieldTo.Value = "";
        //            uoHiddenFieldCc.Value = "";
        //        }
        //    }
        //}
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
            OpenExcelFile(FileName, strFileName);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   02/Apr/2013
        /// Description:    Create the excel file and send email
        /// ------------------------------------------------
        /// </summary>
        private void CreateEmail(DataTable dt, DataTable dtCancelled, string sEmailTo, string sEmailCc)
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
                        sDateOnly + ".<br/><br/>Please send us confirmation and any questions to  HRPortLogistics@rccl.com.<br/><br/>Thank you.";
            EmailManifest(sSubject, sMsg, strFileName, "",
             sEmailTo, sEmailCc, (strFileName + ";").TrimEnd(';'));
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
                    xtwWriter.WriteAttributeString("ss", "Name", null, "ConfirmedManifest");

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
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER" )
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
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/HotelManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
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
        /// </summary>       
        public List<HotelManifest> GetHotelConfirmManifestList(string DateFromString, string DateToString,
           string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
           Int16 LoadType, string SortBy, string ListType, int StartRow, int MaxRow)
        {
            List<HotelManifest> TentativeManifest = new List<HotelManifest>();

            if (ListType == "Confirm")
            {
                filterNameOrID = GlobalCode.Field2String(filterNameOrID);
                MBLL.GetHotelConfirmManifestForecast(DateFromString, DateToString, strUser, DateFilter,
                        ByNameOrID, filterNameOrID.TrimEnd(),
                        Nationality, Gender, Rank, Status,
                        Region, Country, City, Port,
                        Hotel, Vessel, UserRole, StartRow, MaxRow, LoadType,
                        SortBy);
            }
            if (ListType == "Confirm")
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
        /// Date Created:       03/Sept/2013
        /// Description:        Get the Record Count of ListView
        /// ------------------------------------
        /// Modified By:        Josephine Gad
        /// Date Modified:      17/Sept/2013
        /// Description:        Include cancelled manifest in forecast
        /// </summary>   
        public int GetHotelConfirmManifestCount(string DateFromString, string DateToString,
           string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
           Int16 LoadType, string SortBy, string ListType)
        {
            int iTotalRow = 0;
            if (ListType == "Confirm")
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
                    //ConfirmManifest(bIsSave, EmailTo, EmailCc);
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
                        
//                        GetSFHotelTravelDetails();
                        GetConfirmedList();
                        GetCancelledList();

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
        /// </summary>
        //private void ConfirmManifest(bool bIsSave, string sEmailTo, string sEmailCc)
        //{
        //    try
        //    {
        //        string strLogDescription;
        //        if (bIsSave)
        //        {
        //            strLogDescription = "Save Hotel Branch Email,Confirm Hotel Manifest";
        //        }
        //        else
        //        {
        //            strLogDescription = "Confirm Hotel Manifest";

        //        }
        //        DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //        MBLL.ConfirmHotelManifest(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
        //            GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue), uoHiddenFieldRole.Value,
        //            bIsSave, sEmailTo, sEmailCc,
        //            strLogDescription, "ConfirmManifest",
        //            Path.GetFileName(Request.UrlReferrer.AbsolutePath), CommonFunctions.GetDateTimeGMT(dateNow), dateNow);                

        //        //AlertMessage("Confirmed!");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }            
        //}
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
        /// (description)   Change costcenter to code
        /// ==========================================================  
        /// Date Modified:  03/Oct/2013
        /// Modified By:    Josephine Gad
        /// (description)   Re-order columnns for finance role use.
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

                                 LastName = GlobalCode.Field2String(a.LastName),
                                 FirstName = GlobalCode.Field2String(a.FirstName),
                                 EmployeeId = a.EmployeeId.ToString(),
                                 Ship = GlobalCode.Field2String(a.ShipCode),

                                 SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                                 Title = GlobalCode.Field2String(a.Title),
                                 Gender = GlobalCode.Field2String(a.Gender),
                                 CostCenter = GlobalCode.Field2String(a.CostCenterCode),
                                 Nationality = GlobalCode.Field2String(a.Nationality),
                                 
                                 Voucher = GlobalCode.Field2String(a.Voucher),

                                 ReasonCode = GlobalCode.Field2String(a.ReasonCode),
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

                                 PassportNo = GlobalCode.Field2String(a.PassportNo),
                                 IssuedDate = GlobalCode.Field2String(a.PasportDateIssued),
                                 PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),
                                 Birthday = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.Birthday)),
                                 HotelBranch = GlobalCode.Field2String(a.HotelBranch),

                                 //Couple = GlobalCode.Field2String(a.Couple),
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

                                 LastName = GlobalCode.Field2String(a.LastName),
                                 FirstName = GlobalCode.Field2String(a.FirstName),
                                 EmployeeId = a.EmployeeId.ToString(),
                                 Ship = GlobalCode.Field2String(a.ShipCode),

                                 SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                                 Title = GlobalCode.Field2String(a.Title),
                                 Gender = GlobalCode.Field2String(a.Gender),
                                 CostCenter = GlobalCode.Field2String(a.CostCenterCode),
                                 Nationality = GlobalCode.Field2String(a.Nationality),

                                 Voucher = GlobalCode.Field2String(a.Voucher),

                                 ReasonCode = GlobalCode.Field2String(a.ReasonCode),
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

                                 PassportNo = GlobalCode.Field2String(a.PassportNo),
                                 IssuedDate = GlobalCode.Field2String(a.PasportDateIssued),
                                 PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),
                                 Birthday = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.Birthday)),
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
        /// </summary>
        /// <param name="sIdBigint"></param>
        /// <param name="sTRId"></param>
        /// <param name="sBranch"></param>
        private void TagSeafarer(string sIdBigint, string sTRId, string sBranch, string sRecLoc, string sE1Id, string sStatusOnOff)
        {
            string sUser = uoHiddenFieldUser.Value;
            string sRole = uoHiddenFieldRole.Value;

            string strLogDescription = "Tag Seafarer";
            string strFunction = "TagSeafarer";
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            SeafarerTravelBLL.InsertTag(sIdBigint, sTRId, sUser, sRole, "0", "0", sBranch, uoHiddenFieldTagTime.Value,
                sRecLoc, sE1Id, sStatusOnOff, strLogDescription, strFunction, Path.GetFileName(Request.Path),
                         TimeZone.CurrentTimeZone.StandardName.ToString(), CommonFunctions.GetDateTimeGMT(dateNow));
            
            if (sBranch != "0")
            {
                //GetSFHotelTravelDetails();
                GetConfirmedList();
                //GetCancelledList();          
            }
        }
        /// Date Created:   11/Jun/2013
        /// Created By:     Josephine Gad
        /// (description)   Settings if header Tag is visible or hidden
        /// ------------------------------------------- 
        private void ControlSettings()
        {
            HtmlControl TagTH = (HtmlControl)uoListViewHeaderConfirmed.Controls[0].FindControl("TagTH");
            if (TagTH != null)
            {
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    TagTH.Visible = true;
                }
                else
                {
                    TagTH.Visible = false;
                }
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Muhallidin G wali
        /// (description) Hide Cancel button Hotel            
        /// </summary>
        protected string HideCancelHotel()
        {
            if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
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
    }
}

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
using System.Web.Security;

namespace TRAVELMART
{
    public partial class Manifest : System.Web.UI.Page
    {
        #region "Events"
        private SeafarerTravelBLL BLL = new SeafarerTravelBLL();
        ManifestBLL manifestBLL = new ManifestBLL();
        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                    to avoid error in date conversion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitializeValues()
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;                
            }
            MembershipUser sUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (sUser == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!sUser.IsOnline)
                {
                    Response.Redirect("Login.aspx");
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }

            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

            Session["strPrevPage"] = Request.RawUrl;
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldUserId.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            uoHiddenFieldUserRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            string userName = GlobalCode.Field2String(Session["UserName"]);
            
            if (!IsPostBack)
            {
                SetDefaultValues();
                GetVessel();
                GetNationality();
                GetGender();
                GetRank();

                if (User.IsInRole(TravelMartVariable.RoleAdministrator)
                    || User.IsInRole(TravelMartVariable.RoleCrewAssist)
                    || User.IsInRole(TravelMartVariable.RoleCrewAdmin)
                    || User.IsInRole(TravelMartVariable.RolePortSpecialist)
                    || User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                {
                    uoButtonRequest.Visible = true;
                }
                else
                {
                    uoButtonRequest.Visible = false;
                }

                if (GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RoleHotelVendor ||
                    GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }
               
            }

            if (uoHiddenFieldManifest.Value == "1")
            {
                GetTravelManifestWithCount();
            }

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (uoHiddenFieldPopupCalendar.Value == "1")
            {
                GetTravelManifestWithCount();
            }

            uoHiddenFieldManifest.Value = "0";
            
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "2";

            uoObjectDataSourceManifest.SelectParameters["LoadType"].DefaultValue = uoHiddenFieldLoadType.Value;
            uoObjectDataSourceManifest.SelectParameters["FromDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceManifest.SelectParameters["ToDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceManifest.SelectParameters["UserID"].DefaultValue = uoHiddenFieldUser.Value;
            uoObjectDataSourceManifest.SelectParameters["Role"].DefaultValue = uoHiddenFieldUserRole.Value;
            uoObjectDataSourceManifest.SelectParameters["OrderBy"].DefaultValue = uoHiddenFieldOrderBy.Value;

            uoObjectDataSourceManifest.SelectParameters["VesselID"].DefaultValue = uoDropDownListVessel.SelectedValue;
            uoObjectDataSourceManifest.SelectParameters["FilterByName"].DefaultValue = uoDropDownListFilterBy.SelectedValue;
            uoObjectDataSourceManifest.SelectParameters["SeafarerID"].DefaultValue = uoTextBoxFilter.Text;

            uoObjectDataSourceManifest.SelectParameters["NationalityID"].DefaultValue = uoDropDownListNationality.SelectedValue;
            uoObjectDataSourceManifest.SelectParameters["Gender"].DefaultValue = uoDropDownListGender.SelectedValue;
            uoObjectDataSourceManifest.SelectParameters["RankID"].DefaultValue = uoDropDownListRank.SelectedValue;
            uoObjectDataSourceManifest.SelectParameters["Status"].DefaultValue = uoDropDownListStatus.SelectedValue;

            uoObjectDataSourceManifest.SelectParameters["RegionID"].DefaultValue = GlobalCode.Field2String(Session["Region"]);
            uoObjectDataSourceManifest.SelectParameters["CountryID"].DefaultValue = GlobalCode.Field2String(Session["Country"]);
            uoObjectDataSourceManifest.SelectParameters["CityID"].DefaultValue = GlobalCode.Field2String(Session["City"]);
            uoObjectDataSourceManifest.SelectParameters["PortID"].DefaultValue = GlobalCode.Field2String(Session["Port"]);
            uoObjectDataSourceManifest.SelectParameters["HotelID"].DefaultValue = GlobalCode.Field2String(Session["Hotel"]);
            
            uoObjectDataSourceManifest.SelectParameters["ViewNoHotelOnly"].DefaultValue = uoHiddenFieldNoBooking.Value;  
            GetTravelManifestWithCount();
        }
        protected void uoListViewManifest_PreRender(object sender, EventArgs e)
        {
            // GetTravelManifest();  
           // BindManifest(0);
        }
        protected void uoListViewManifest_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName.ToString();
            if (e.CommandName != "")
            {
                GetTravelManifestWithCount();
            }
        }
        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //e.InputParameters["DateFrom"] = GlobalCode.Field2String(Session["DateFrom"]);
            //e.InputParameters["DateTo"] = Session["DateTo"];
            //e.InputParameters["UserID"] = uoHiddenFieldUser.Value;

            //e.InputParameters["DateFilter"] = Session["strPendingFilter"];
            //e.InputParameters["RegionId"] = Session["Region"];
            //e.InputParameters["CountryId"] =  GlobalCode.Field2String(Session["Country"]);
            //e.InputParameters["CityId"] = Session["City"] ;
            //e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;
            //e.InputParameters["ByNameOrID"] = uoDropDownListFilterBy.SelectedValue;
            //e.InputParameters["filterNameOrID"] = uoTextBoxFilter.Text.Trim();
            //e.InputParameters["PortId"] = Session["Port"];
            //e.InputParameters["HotelId"] = GlobalCode.Field2String(Session["Hotel"]);
            //e.InputParameters["VehicleId"] = Session["Vehicle"];
            //e.InputParameters["VesselId"] = uoDropDownListVessel.SelectedValue;
            //e.InputParameters["Nationality"] = uoDropDownListNationality.SelectedValue;
            //e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            //e.InputParameters["Rank"] = uoDropDownListRank.SelectedValue;

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

            //e.InputParameters["RegionID"] = Session["Region"];
            //e.InputParameters["CountryID"] =  GlobalCode.Field2String(Session["Country"]);
            //e.InputParameters["CityID"] = Session["City"] ;
            //e.InputParameters["PortID"] = Session["Port"];
            //e.InputParameters["HotelID"] = GlobalCode.Field2String(Session["Hotel"]);
            e.InputParameters["RegionID"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["CountryID"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["CityID"] = GlobalCode.Field2String(Session["City"]);
            e.InputParameters["PortID"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["HotelID"] = GlobalCode.Field2String(Session["Hotel"]);

            e.InputParameters["ViewNoHotelOnly"] = uoHiddenFieldNoBooking.Value;

            //e.InputParameters["StartRow"] = 0;
            //e.InputParameters["MaxRow"] = GetCount();

            //e.InputParameters["ByVessel"] = Int16.Parse(uoHiddenFieldByVessel.Value);
            //e.InputParameters["ByName"] = Int16.Parse(uoHiddenFieldByName.Value);
            //e.InputParameters["ByRecLoc"] = Int16.Parse(uoHiddenFieldByRecLoc.Value);
            //e.InputParameters["ByE1ID"] = Int16.Parse(uoHiddenFieldByE1ID.Value);
            //e.InputParameters["ByDateOnOff"] = Int16.Parse(uoHiddenFieldByDateOnOff.Value);
            //e.InputParameters["ByDateArrDep"] = Int16.Parse(uoHiddenFieldByDateArrDep.Value);
            //e.InputParameters["ByStatus"] = Int16.Parse(uoHiddenFieldByStatus.Value);
            //e.InputParameters["ByBrand"] = Int16.Parse(uoHiddenFieldByBrand.Value);

            //e.InputParameters["ByPort"] = Int16.Parse(uoHiddenFieldByPort.Value);
            //e.InputParameters["ByRank"] = Int16.Parse(uoHiddenFieldByRank.Value);
            //e.InputParameters["ByAirStatus"] = Int16.Parse(uoHiddenFieldByAirStatus.Value);
            //e.InputParameters["ByHotelStatus"] = Int16.Parse(uoHiddenFieldByHotelStatus.Value);
            //e.InputParameters["ByVehicleStatus"] = Int16.Parse(uoHiddenFieldByVehicleStatus.Value);      

        }

        //protected void uoLinkButtonName_Click(object sender, EventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;

        //    //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
        //    strLogDescription = "Seafarer itinerary information viewed.";
        //    strFunction = "uoLinkButtonName_Click";

        //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //    AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, Session["UserName"].ToString());

        //    Response.Redirect("SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + ManifestDTO.ManifestList[0].SfID + "&recloc=" + ManifestDTO.ManifestList[0].RecLoc + "&st=" + ManifestDTO.ManifestList[0].Status + "&ID=" + ManifestDTO.ManifestList[0].IDBigInt + "&trID=" + ManifestDTO.ManifestList[0].TravelRequestID + "&manualReqID=" + ManifestDTO.ManifestList[0].RequestID + "&dt=" + Request.QueryString["dt"]);
        //}

        protected void uoLinkButtonReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report.aspx?rpt=Manifest");
        }

        protected void uoButtonSaveBarcode_Click(object sender, EventArgs e)
        {

        }
        //protected void uoButtonRequestSendEmail_Click(object sender, EventArgs e)
        //{
        //    BindManifestExcel();
        //}
        public override void VerifyRenderingInServerForm(Control control)
        {
            //Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }
        protected void uoCheckBoxShowNoBooking_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxShowNoBooking.Checked)
            {
                uoHiddenFieldNoBooking.Value = "1";
            }
            else
            {
                uoHiddenFieldNoBooking.Value = "0";
            }
            GetTravelManifestWithCount();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   14/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Create excel file of manifest and email
        /// ---------------------------------------------------------------------------
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================
        /// </summary>
        //private void BindManifestExcel()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        SeafarerTravelBLL bll = new SeafarerTravelBLL();
        //        dt = bll.GetTravelManifestWithCount(GlobalCode.Field2String(Session["DateFrom"]), Session["DateTo"],
        //            uoHiddenFieldUser.Value, Session["strPendingFilter"], Session["Region"],
        //            Session["Country"] Session["City"] , uoDropDownListStatus.SelectedValue,
        //            uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(), Session["Port"],
        //            GlobalCode.Field2String(Session["Hotel"]), Session["Vehicle"], uoDropDownListVessel.SelectedValue,
        //            uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue,
        //            Int16.Parse(uoHiddenFieldByVessel.Value), Int16.Parse(uoHiddenFieldByName.Value), Int16.Parse(uoHiddenFieldByRecLoc.Value),
        //            Int16.Parse(uoHiddenFieldByE1ID.Value), Int16.Parse(uoHiddenFieldByDateOnOff.Value), Int16.Parse(uoHiddenFieldByDateArrDep.Value),
        //            Int16.Parse(uoHiddenFieldByStatus.Value), Int16.Parse(uoHiddenFieldByBrand.Value), Int16.Parse(uoHiddenFieldByPort.Value),
        //            Int16.Parse(uoHiddenFieldByRank.Value), Int16.Parse(uoHiddenFieldByAirStatus.Value),
        //            Int16.Parse(uoHiddenFieldByHotelStatus.Value), Int16.Parse(uoHiddenFieldByVehicleStatus.Value),
        //            0, uoListViewManifestPager.TotalRowCount);
        //        uoGridViewManifest.Visible = true;
        //        if (dt.Rows.Count > 0)
        //        {
        //            uoGridViewManifest.DataSource = dt;
        //        }
        //        else
        //        {
        //            uoGridViewManifest.EmptyDataText = "No Record.";
        //        }
        //        uoGridViewManifest.DataBind();                
        //        //uoGridViewManifest.ro.Cells.Add(ManifestCell);


        //        //popup
        //        //Response.ClearContent();
        //        //Response.AddHeader("content-disposition", "attachment; filename=" + "ExcelTest.xls");                
        //        ////to open the file without saving it
        //        ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        ////Response.AddHeader("Content-Disposition", "inline; filename=" + "ExcelTest.xls");
        //        ////string filePath = MapPath("Manifest.xls");                
        //        //Response.ContentType = "application/ms-excel";
        //        //StringWriter sw = new StringWriter();
        //        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        //uoGridViewManifest.RenderControl(htw);
        //        //Response.Write(sw.ToString());
        //        //Response.End();


        //        //popup2
        //        //Response.Clear();
        //        //Response.AddHeader("content-disposition", "inline;filename=FileName.xls");
        //        //Response.Charset = "";
        //        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        //Response.ContentType = "application/vnd.xls";
        //        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //        //uoGridViewManifest.RenderControl(htmlWrite);
        //        //Response.Write(stringWrite.ToString());
        //        //Response.End();

        //         //non popup
        //        //string FilePath = MapPath("Extract/Manifest/");
        //        //FileInfo FileName = new FileInfo(FilePath + "Manifest.xls");
        //        //Response.Clear();
        //        //Response.AddHeader("Content-Disposition", "attachment; filename=" + FilePath + FileName.Name);
        //        ////Response.AddHeader("Content-Length", FileName.Length.ToString());
        //        //System.IO.StringWriter sw = new System.IO.StringWriter();
        //        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //        //uoGridViewManifest.RenderControl(htw);
        //        //Response.WriteFile(FileName.FullName);

        //        ////auto save
        //        string FilePath = MapPath("Extract/Manifest/");
        //        string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
        //        FileInfo FileName = new FileInfo(FilePath + "Manifest_" + sDate + ".xls");
        //        Response.Clear();
        //        Response.ClearContent();
        //        StringWriter stringWrite = new StringWriter();
        //        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //        uoGridViewManifest.RenderControl(htmlWrite);
        //        FileStream fs = new FileStream(FileName.FullName, FileMode.Create);
        //        StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
        //        sWriter.Write(stringWrite.ToString().Trim());
        //        sWriter.Close();
        //        fs.Close();
        //        //Use below line instead of Response.End() to avoid Error: Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.;  
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //        ManifestSendEmail("Travelmart: Manifest","This is a sample Manifest", FileName.FullName);
        //        AlertMessage("Email sent.");
        //    }
        //    catch (Exception ex)
        //    {
        //        DateTime currentDate = CommonFunctions.GetCurrentDateTime();
        //        ErrorBLL.InsertError(ex.Message, ex.StackTrace.ToString(), Request.Url.AbsolutePath,
        //            currentDate, CommonFunctions.GetDateTimeGMT(currentDate), Session["UserName"].ToString());
        //        AlertMessage(ex.Message);
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //        uoGridViewManifest.Visible = false;
        //    }
        //}               
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set default values of global variables
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void SetDefaultValues()
        {
            //string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            //if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            //{
            //GlobalCode.Field2String(Session["DateFrom"]) = currentDate;
            //Session["DateTo"] = currentDate;
            if (GlobalCode.Field2String(Session["Region"]) == "" || Session["Region"] == null)
            {
                Session["strPendingFilter"] = "0";
                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["City"] = "0";
                Session["Port"] = "0";
                Session["Hotel"] = "0";
                Session["Vehicle"] = "0";
            }
            //}
        }
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
                    GlobalCode.Field2String(Session["City"]) , GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value);
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
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Manifest List        
        /// ---------------------------------------------------------------------------
        /// </summary>
        //private void GetTravelManifest()
        //{
        //    GetTravelManifestWithCount();
        //    return;
        //    DataTable ManifestDataTable = null;
        //    DataView ManifestDataView = null;
        //    try
        //    {
        //        ManifestDataTable = SeafarerTravelBLL.GetTravelManifest(GlobalCode.Field2String(Session["DateFrom"]), Session["DateTo"],
        //            Session["UserName"].ToString(), Session["strPendingFilter"], Session["Region"], Session["Country"]
        //            Session["City"] , uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
        //            Session["Port"], GlobalCode.Field2String(Session["Hotel"]), Session["Vehicle"], uoDropDownListVessel.SelectedValue,
        //            uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue);

        //        ManifestDataView = ManifestDataTable.DefaultView;
        //        string SortBy = TravelMartVariable.ManifestSortByString;
        //        if (SortBy == "")
        //        {
        //            SortBy = "Name";
        //            TravelMartVariable.ManifestSortByString = SortBy;
        //        }
        //        //ManifestDataView.Sort = "Vessel, Name ";
        //        ManifestDataView.Sort = "Vessel, " + SortBy + " " + TravelMartVariable.ManifestAscDesc;

        //        uoListViewManifest.DataSource = ManifestDataView;
        //        uoListViewManifest.DataBind();                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (ManifestDataTable != null)
        //        {
        //            ManifestDataTable.Dispose();
        //        }
        //        if (ManifestDataView != null)
        //        {
        //            ManifestDataView.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Manifest List  with count      
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetTravelManifestWithCount()
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
        /// Date Created:   09/01/2012
        /// Created By:     Muhallidin G Wali
        /// (description)   
        protected short getDefualtLoadType()
        {
            //Get the data field value of interest for this row            
            short LoadType;

            switch (GlobalCode.Field2String(Session["UserName"]))
            {
                case "Vehicle Vendor":
                case "Hotel Vendor":
                    LoadType = 0;
                    break;
                default:
                    LoadType = 1;
                    break;

            }

            return LoadType;
        }


        /// <summary>
        /// Date Created:   20/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Set manifest groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string ManifestAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Ship";
            string GroupValueString = "Vessel";

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
                if (Eval("IsWithSail").ToString() == "True")
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"15\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
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
        /// <summary>
        /// Date Created:   24/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Show/Hide Scan button
        /// </summary>
        /// <param name="TravelReqID"></param>
        /// <param name="ManualReqID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        protected bool ShowScanBtn(object TravelReqID, object ManualReqID)
        {
            bool bReturn = false;
            bReturn = SeafarerTravelBLL.IsLOEScanned(TravelReqID.ToString(), ManualReqID.ToString(), uoHiddenFieldUser.Value);
            return bReturn;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/02/2012
        /// Description:    Set default values
        /// </summary>
        /// <param name="LoadType"></param>
        protected void BindManifest(Int16 LoadType)
        {                        
            //manifestBLL.LoadAllManifestTables(Session["UserName"].ToString(), DateTime.Parse(uoHiddenFieldStartDate.Value), uoDropDownListStatus.SelectedValue, LoadType, 0, 20);
            manifestBLL.LoadAllManifestTables(LoadType, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                GlobalCode.Field2DateTime(uoHiddenFieldDate.Value), 
                uoHiddenFieldUser.Value,
                uoHiddenFieldUserRole.Value, uoHiddenFieldOrderBy.Value, 0, 20,
                GlobalCode.Field2Int(uoDropDownListVessel.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListFilterBy.SelectedValue),
                uoTextBoxFilter.Text,
                GlobalCode.Field2Int(uoDropDownListNationality.SelectedValue), 
                GlobalCode.Field2Int(uoDropDownListGender.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListRank.SelectedValue),
                uoDropDownListStatus.SelectedValue, 
                GlobalCode.Field2Int(GlobalCode.Field2String(Session["Region"])),
                GlobalCode.Field2Int(GlobalCode.Field2String(Session["Country"])),
                GlobalCode.Field2Int(GlobalCode.Field2String(Session["City"])),
                GlobalCode.Field2Int(GlobalCode.Field2String(Session["Port"])),
                GlobalCode.Field2Int(GlobalCode.Field2String(Session["Hotel"])),
                uoHiddenFieldNoBooking.Value
                );
            uoObjectDataSourceManifest.TypeName = "TRAVELMART.Common.ManifestDTO";
            uoObjectDataSourceManifest.SelectCountMethod = "GetManifestListCount";
            uoObjectDataSourceManifest.SelectMethod = "GetManifestList";

            uoListViewManifest.DataSourceID = uoObjectDataSourceManifest.UniqueID;
        }       
        #endregion     

      
    }
}
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



namespace TRAVELMART.Hotel
{
    public partial class HotelViewList : System.Web.UI.Page
    {
        #region Event

        //Int32 gsfID;
        //Int32 gTravelReqIdInt;
        //string gStatus;
        //string gRecLoc;
        //Int32 gRequestID;
        //Int32 gIDBigInt;

        //DataTable tempDT = null;

        #region Page_Load
        /// <summary>       
        /// Date Created:      25/07/2011
        /// Created By:        Ryan Bautista
        /// (description)      Load all basic hotel details
        /// -------------------------------------------------------
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                    to avoid error in date conversion
        /// -------------------------------------------
        /// Date Modified:     15/03/2012
        /// Modified By:       Josephine Gad
        /// (description)      Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); //gelo     
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl; //gelo

                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
                //BindMapRef();
                SetDefaultValues();
                GetNationality();
                GetGender();
                GetRank();
                GetVessel();
                GetHotelFilter();

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist)
                {
                    uoButtonLock.Visible = true;
                }
                else
                {
                    uoButtonLock.Visible = false;
                }                
                DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                TimeSpan timeDiff = dDateFrom - dDateCurrent;
                int dDays = timeDiff.Days;

                if (dDays <= 1)
                {
                    uoDropDownListHours.SelectedValue = "24";
                    uoDropDownListHours.Enabled = false;
                }
                else if (dDays == 2)
                {
                    uoDropDownListHours.SelectedValue = "48";
                    uoDropDownListHours.Enabled = false;
                }
                else if (dDays > 2)
                {
                    uoDropDownListHours.SelectedValue = "72";
                    uoDropDownListHours.Enabled = false;
                }
                SetCalendarDashboard();
                //ButtonLockSettings();
            }
        }
        #endregion

        #region uolistviewHotelInfoPager_PreRender
        protected void uolistviewHotelInfoPager_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region uoButtonView_Click
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
        }
        protected void uoButtonView2_Click(object sender, EventArgs e)
        {            
        }
        #endregion

        //#region uoButtonRequestSendEmail_Click
        //protected void uoButtonRequestSendEmail_Click(object sender, EventArgs e)
        //{
        //    BindHotelManifestExcel();
        //}
        //#endregion

        //#region VerifyRenderingInServerForm
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //Confirms that an HtmlForm control is rendered for the
        //    //specified ASP.NET server control at run time.
        //}
        //#endregion

        //#region uoGridViewHotelManifest_DataBound
        //protected void uoGridViewHotelManifest_DataBound(object sender, EventArgs e)
        //{
        //    GridView grid = sender as GridView;
        //    if (grid != null)
        //    {
        //        Label SFStatus = (Label)Master.FindControl("uclabelStatus");

        //        GridViewRow ManifestRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
        //        TableCell ManifestCell = new TableCell();
        //        ManifestCell.ColumnSpan = uoGridViewHotelManifest.Columns.Count;
        //        ManifestCell.Text = "Hotel Manifest: " + SFStatus.Text;
        //        ManifestCell.BackColor = ColorTranslator.FromHtml("#1f497d");
        //        ManifestCell.ForeColor = Color.White;
        //        ManifestCell.Font.Bold = true;
        //        ManifestRow.Font.Size = 14;
        //        ManifestRow.Cells.Add(ManifestCell);

        //        Table t = grid.Controls[0] as Table;
        //        {
        //            if (t != null)
        //            {
        //                t.Rows.AddAt(0, ManifestRow);
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region uoButtonLock_Click
        protected void uoButtonLock_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (uoDropDownListHotel.SelectedValue != "")
            {
                if (uoDropDownListHotel.SelectedValue != "0")
                {
                    Int32 ID = HotelManifestBLL.InsertHotelManifestLockHeader(uoDropDownListHours.SelectedValue, uoDropDownListHotel.SelectedValue,
                                                                                uoHiddenFieldUser.Value, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString());

                    //HotelManifestBLL.InsertHotelManifestLockDetails((DataTable)ViewState["Table"], ID);

                    //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                    strLogDescription = "Hotel tentative manifest locked.";
                    strFunction = "uoButtonLock_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    AuditTrailBLL.InsertLogAuditTrail(ID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

                    uoButtonLock.Enabled = false;
                    AlertMessage("Manifest has been locked.");
                }
                else
                {
                    AlertMessage("Select hotel.");
                }
            }
            else
            {
                AlertMessage("Select hotel.");
            }
        }
        #endregion

        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;
            SetCalendarDashboard();
            GetSFHotelTravelDetails();

            Session.Remove("TentativeManifestCalendarDashboard");
            GetCalendarTable();
        }
        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFromString"] = uoHiddenFieldDate.Value;
            e.InputParameters["strUser"] = uoHiddenFieldUser.Value;

            e.InputParameters["DateFilter"] = "1";
            e.InputParameters["ByNameOrID"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["filterNameOrID"] = uoTextBoxFilter.Text.Trim();

            e.InputParameters["Nationality"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["Rank"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;

            e.InputParameters["Region"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["Country"] = GlobalCode.Field2String(Session["Country"]); 
            e.InputParameters["City"] = GlobalCode.Field2String(Session["City"]);

            e.InputParameters["Port"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["Hotel"] = uoDropDownListHotel.SelectedValue;
            e.InputParameters["Vessel"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["UserRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["LoadType"] = "0";
        }
        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            ButtonLockSettings();
        }
        #endregion

        #region Function


        #region GetHotelFilter
        /// <summary>
        /// Date Modified:  01/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   change datasource of hotel branch from SeafarerTravelBLL.GetSFHotelTravelListView2 
        ///                 to HotelManifestBLL.GetHotelBranchListByUser
        /// </summary>
        private void GetHotelFilter()
        {
            DataTable dt = null;
            try
            {
                dt = HotelManifestBLL.GetHotelBranchListByUser("", uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, "");
                
                int iRowCount = dt.Rows.Count;
                if (iRowCount == 1)
                {
                    Session["Hotel"] = dt.Rows[0]["BranchID"].ToString();
                }
                if (iRowCount > 0)
                {
                    uoDropDownListHotel.Items.Clear();
                    uoDropDownListHotel.DataSource = dt;
                    uoDropDownListHotel.DataTextField = "BranchName";
                    uoDropDownListHotel.DataValueField = "BranchID";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    RemoveDuplicateItems(uoDropDownListHotel);
                    uoDropDownListHotel.Enabled = true;

                    if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                    {
                        uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                        SetCalendarDashboard();
                    }
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
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        #region SetDefaultValues
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set default values of global variables
        /// ---------------------------------------------------------------------------
        /// </summary>
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
        #endregion

        #region GetVessel
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
                VesselDataTable = VesselBLL.GetVessel(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(),
                    GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), GlobalCode.Field2String(Session["Region"]),GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value);
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
        #endregion

        #region GetNationality
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
        #endregion

        #region GetGender
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
        #endregion

        #region GetRank
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Get Rank List        
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetRankByVessel("0");
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
        #endregion


        /// <summary>        
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Load all hotel travel details.
        /// ----------------------------------------------
        /// Date Modified:  01/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete Parameter strSFStatus and strFlightDateRange
        ///                 Change parameter of GetSFHotelTravelListView2 and delete unused parameters
        /// ----------------------------------------------
        /// Date Modified:  22/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   set the list view datasource
        /// </summary>
        private void GetSFHotelTravelDetails()
        {
            try
            {
                uolistviewHotelInfo.DataSource = null;
                uolistviewHotelInfo.DataSourceID = "uoObjectDataSourceManifest";
                //ButtonLockSettings();
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        #endregion

        #region RemoveDuplicateItems
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
        #endregion

        #region DateFormat
        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Format date        
        /// </summary>
        protected string DateFormat(object oDatetime)
        {
            Int32 DateTimeInt32 = (Int32)oDatetime;
            if (DateTimeInt32 == 1)
            {
                return "{0:dd-MMM-yyyy HHmm}";
            }
            else
            {
                return "{0:dd-MMM-yyyy}";
            }
        }
        #endregion

        #region HotelAddGroup
        /// <summary>
        /// Date Created:   20/07/2011
        /// Created By:     Ryan bautista
        /// (description)   Set hotel view  groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel"; //"Check In";
            string GroupValueString = "HotelBranch"; //"colTimeSpanStartDate";

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
                return string.Format("<tr><td class=\"group\" colspan=\"34\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        #region ManifestSendEmail
        /// <summary>
        /// Date Created:   15/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Send email
        /// </summary>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        /// <param name="attachment"></param>
        private void ManifestSendEmail(string sSubject, string sMessage, string attachment, string EmailVendor, string file)
        {
            string sBody;
            DataTable dt = null;
            try
            {                
                sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
                sBody += "</TR></TD></TABLE>";

                ////Insert Email logs
                //CommonFunctions.InsertEmailLog(EmailVendor, "travelmart.ptc@gmail.com", "", sSubject, file, DateTime.Now, uoHiddenFieldUser.Value);

                //if (EmailVendor != null || EmailVendor != "")
                //{
                //    CommonFunctions.SendEmailWithAttachment("", EmailVendor, "", sSubject, sBody, attachment);
                //}


                //}
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
        #endregion

        #region AlertMessage
        /// <summary>
        /// Date Created:   14/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message            
        /// </summary>
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



        #region ExportToSpreadsheet
        private void ExportToSpreadsheet(DataTable table)
        {
            string FilePath = MapPath("~/Extract/HotelManifest/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            FileInfo FileName = new FileInfo(FilePath + "HotelManifest_" + sDate + ".xls");

            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            foreach (DataColumn column in table.Columns)
            {
                context.Response.Write(column.ColumnName + ";");
            }
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.ContentType = "text/xls";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            context.Response.End();
        }
        #endregion

        #region Excel_FromDataTable
        private void Excel_FromDataTable(DataTable dt)
        {
            Excel.ApplicationClass excel = new Excel.ApplicationClass();
            Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

            // Add column headings...
            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }
            // for each row of data...
            int iRow = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;

                // add each row's cell data...
                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            // Global missing reference for objects we are not defining...
            object missing = System.Reflection.Missing.Value;
            string FilePath = MapPath("~/Extract/HotelManifest/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            //FileInfo FileName = new FileInfo(FilePath + "HotelManifest_" + sDate + ".xls");
            string FileName = FilePath + "HotelManifest_" + sDate + ".xls";

            // If wanting <strong class="highlight">to</strong> Save the workbook...
            workbook.SaveAs(FileName, Excel.XlFileFormat.xlXMLSpreadsheet,
                missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            // If wanting <strong class="highlight">to</strong> make <strong class="highlight">Excel</strong> visible and activate the worksheet...
            excel.Visible = true;
            Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
            ((Excel._Worksheet)worksheet).Activate();

            // If wanting <strong class="highlight">excel</strong> <strong class="highlight">to</strong> shutdown...
            ((Excel._Application)excel).Quit();
        }
        #endregion

        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set the Calendar visibility if there is selected hotel branch
        /// </summary>
        private void SetCalendarDashboard()
        {
            Calendar uoCalendarDashboard = (Calendar)Master.FindControl("uoCalendarDashboard");
            HtmlControl uoTableDashboard = (HtmlControl)Master.FindControl("uoTableDashboard");
            HtmlControl uoTableLeftMenu = (HtmlControl)Master.FindControl("uoTableLeftMenu");
            if (uoDropDownListHotel.SelectedValue == "0" || uoDropDownListHotel.SelectedValue == "")
            {
                uoCalendarDashboard.Visible = false;
                uoTableDashboard.Visible = false;
                uoTableLeftMenu.Visible = false;
            }
            else
            {
                uoCalendarDashboard.Visible = true;
                uoTableDashboard.Visible = true;
                uoTableLeftMenu.Visible = true;
                Session.Remove("TentativeManifestCalendarDashboard");
                GetCalendarTable();
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
            DataTable dt = null;
            ManifestCalendar cal = new ManifestCalendar();
            List<ManifestCalendar> calList = new List<ManifestCalendar>();
           
            try
            {
                if (Session["TentativeManifestCalendarDashboard"] == null)
                {
                    DateTime day = GlobalCode.Field2DateTime(Request.QueryString["dt"].ToString());
                    string sPendingFilter = "0";
                    string sRegion = "0";
                    string sCountry = "0";
                    string sCity = "0";
                    string sPort = "0";
                    string sHotel = "0";
                    string sVehicle = "0";
                    string sManifestHrs = "0";
                    if (Session["strPendingFilter"] != null && GlobalCode.Field2String(Session["strPendingFilter"]) != "")
                    {
                        sPendingFilter = GlobalCode.Field2String(Session["strPendingFilter"]);
                    }
                    if (Session["Region"] != null && GlobalCode.Field2String(Session["Region"]) != "")
                    {
                        sRegion = GlobalCode.Field2String(Session["Region"]);
                    }
                    if (Session["Country"] != null && GlobalCode.Field2String(Session["Country"]) != "")
                    {
                        sCountry = GlobalCode.Field2String(Session["Country"]);
                    }
                    if (Session["City"] != null && GlobalCode.Field2String(Session["City"]) != "")
                    {
                        sCity = GlobalCode.Field2String(Session["City"]);
                    }
                    if (Session["Port"] != null && GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        sPort =  GlobalCode.Field2String(Session["Port"]);
                    }
                    if (Session["Hotel"] != null && GlobalCode.Field2String(Session["Hotel"]) != "")
                    {
                        sHotel = GlobalCode.Field2String(Session["Hotel"]);
                    }
                    if (Session["Vehicle"] != null && GlobalCode.Field2String(Session["Vehicle"]) != "")
                    {
                        sVehicle = Session["Vehicle"].ToString();
                    }
                    if (Session["ManifestHrs"] != null && GlobalCode.Field2String(Session["ManifestHrs"]) != "")
                    {
                        sManifestHrs = Session["ManifestHrs"].ToString();
                    }

                    calList = HotelManifestBLL.GetTentativeManifestDashboard(day.ToString("MM/dd/yyyy"),
                   "", uoHiddenFieldUser.Value, "1",
                   "1", "", "0",
                   "0", "0", "0",
                  sRegion, sCountry,
                  sCity, sPort, sHotel, "0",
                  uoHiddenFieldRole.Value, sManifestHrs);

                    //Cache.Remove("TentativeManifestCalendarDashboard");
                    //Store TentativeManifestCalendarDashboard dr in Cache for 5 minutes
                    //Cache.Insert("TentativeManifestCalendarDashboard", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));

                    //calList = (from a in dt.AsEnumerable()
                    //           select new ManifestCalendar
                    //           {
                    //               colDate = GlobalCode.Field2DateTime(a["colDate"]),
                    //               TotalCount = GlobalCode.Field2Int(a["TotalCount"])
                    //           }).ToList();

                    Session["TentativeManifestCalendarDashboard"] = calList;
                    //dt.WriteXml(sw);
                    ////sw.Flush();                        
                    ////Cache.Remove("TentativeManifestCalendarDashboard");
                    ////Store TentativeManifestCalendarDashboard dr in Cache for 5 minutes
                    ////Cache.Insert("TentativeManifestCalendarDashboard", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                    //XmlSerializer serializer = new XmlSerializer(typeof(ManifestCalendar));

                    //serializer.Serialize(sw, cal);
                    //Session["TentativeManifestCalendarDashboard"] = sw;// sw.ToString();
                    return calList;                    
                }
                else
                {
                    calList = (List<ManifestCalendar>)Session["TentativeManifestCalendarDashboard"];
                    return calList;
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
            }
        }
        /// <summary>
        /// Date Created:   22/02/2011
        /// Created By:     Josephine Gad
        /// (description)   disable lock button if past dates and if there is no data
        /// </summary>
        private void ButtonLockSettings()
        {
            if (uolistviewHotelInfo.Items.Count > 0)
            {
                uoButtonLock.Enabled = true;
            }
            else
            {
                uoButtonLock.Enabled = false;
            }

            string sDate = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
            if (GlobalCode.Field2DateTime((sDate)) < DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")))
            {
                uoButtonLock.Enabled = false;
            }
        }
        #endregion
    }
}

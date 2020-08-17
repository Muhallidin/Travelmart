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
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using PDF = iTextSharp;
using System.Collections.Generic;

namespace TRAVELMART.Hotel
{
    public partial class HotelManifest : System.Web.UI.Page
    {
        #region DECLARATION
        public string compareHeader = string.Empty;
        #endregion

        #region Event

        #region Page_Load
        /// <summary>        
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                    to avoid error in date conversion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                GetManifestType();
                GetCompareType();
                GetHotelBranch();

                SetDefaultValues();
                GetNationality();
                GetGender();
                GetRank();
                GetVessel();
                
                Session["ViewDateRange"] = "0";
                Session["ViewFilter2"] = "0";
                Session["strPendingFilter"] = "1";
                GetHotelManifestByHours();

                //HotelHasEvents();
                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }
                SetTitle();
                SetCalendarDashboard();
                SetControls();

                //if ((uoDropDownListBranch.SelectedValue == "0") || uoDropDownListHours.SelectedValue == "0")
                //{
                //    uoButtonRequestSendEmail.Visible = false;
                //}
                //else
                //{
                //    uoButtonRequestSendEmail.Visible = true;
                //}


                if (uoHiddenFieldRole.Value != TravelMartVariable.RoleAdministrator &&
               uoHiddenFieldRole.Value != TravelMartVariable.Role24x7 &&
               uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelSpecialist
               )
                {
                    uoCheckBoxDiff.Visible = false;
                    uoButtonRequestSendEmail.Visible = false;
                }
            }
        }
        #endregion

        //#region uolistviewHotelInfoPager_PreRender
        //protected void uolistviewHotelInfoPager_PreRender(object sender, EventArgs e)
        //{

        //}
        //#endregion

        #region uoButtonView_Click
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            //GetSFHotelTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
            Session["ManifestHrs"] = uoDropDownListHours.SelectedValue;
            Session["Hotel"] = uoDropDownListBranch.SelectedValue;

            SetTitle();
            SetCalendarDashboard();
            GetHotelManifestByHours();
            //SetControls();
            if (uoListDiff.Items.Count > 0)
            {
                uoCheckBoxDiff.Enabled = true;
            }
            else
            {
                uoCheckBoxDiff.Enabled = false;
            }
        }
        #endregion

        #region uoButtonRequestSendEmail_Click
        protected void uoButtonRequestSendEmail_Click(object sender, EventArgs e)
        {
            //EmailNoAttachment();           
            if (Request.QueryString["email"] != null)
            {
                BindHotelManifestExcel();
                if (uoCheckBoxDiff.Checked == true)
                {
                    if (uoListDiff.Items.Count > 0)
                    {
                        DataTabletoExcel();
                    }
                }
            }
            else
            {
                CreateExcelManifest();
            }
            //BindHotelManifestExcel();
            //if (uoCheckBoxDiff.Checked == true)
            //{
            //    if (uoListDiff.Items.Count > 0)
            //    {
            //        DataTabletoExcel();
            //    }
            //}
            
            AlertMessage("Email sent.");
        }
        #endregion

        #region VerifyRenderingInServerForm
        public override void VerifyRenderingInServerForm(Control control)
        {
            //Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.

        }
        #endregion

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

        protected void uoDropDownListHours_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ManifestHrs"] = uoDropDownListHours.SelectedValue;
            uoDropDownListCompare.SelectedIndex = 0;
            GetCompareType();
            if (uolistviewHotelInfo.Items.Count <= 0)
            {
                uolistviewHotelInfo.DataBind();
            }
            //if (uoListDiff.Items.Count >= 0)
            //{
            //    uoListDiff.DataBind();
            //}
            //GetHotelManifestByHours();

        }

        protected void uoButtonCompare_Click(object sender, EventArgs e)
        {
            if (uoDropDownListCompare.SelectedIndex > 0)
            {
                compareHeader = "Difference of " + uoDropDownListHours.SelectedItem.Text + " VS " + uoDropDownListCompare.SelectedItem.Text;
                uoLabelCompareHeader.Visible = true;
                uoListDiff.Visible = true;
                uoUpdatePanelDiffHeader.Update();
                uoUpdatePanelDiff.Update();
                SetTitle();
                GetHotelManifestByHours();

                GetManifestDiff();
                if (uoListDiff.Items.Count > 0)
                {
                    uoCheckBoxDiff.Enabled = true;
                }
                else
                {
                    uoCheckBoxDiff.Enabled = false;
                }
            }
        }
        #endregion

        #region Function

        #region DataTabletoExcel
        private void DataTabletoExcel()//DataTable dt
        {
            ////auto save
            string FilePath = Server.MapPath("~/Extract/HotelManifest/");
            //string FilePath = ConfigurationSettings.AppSettings["TravelmartFileDirectory"].ToString();
            //FilePath += "\\HotelManifest\\";

            //string sLogicalDrive = Environment.GetEnvironmentVariable("USERPROFILE").Split(":".ToCharArray())[0];
            //sLogicalDrive += ":\\";

            //string FilePath = sLogicalDrive;

            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            FileInfo FileName = new FileInfo(FilePath + "HotelManifestDifference_" + sDate + ".xls");
            string File = "HotelManifestDifference_" + sDate + ".xls";
            Response.Clear();
            Response.ClearContent();
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //uoGridViewHotelManifest.RenderControl(htmlWrite);
            uoListDiff.RenderControl(htmlWrite);
            FileStream fs = new FileStream(FileName.FullName, FileMode.Create);
            StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
            sWriter.Write(stringWrite.ToString().Trim());
            sWriter.Close();
            fs.Close();
            //Use below line instead of Response.End() to avoid Error: Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.;  
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);
            string sMsg = "Please find attached difference of " + uoDropDownListHours.SelectedItem.Text + " and " + uoDropDownListCompare.SelectedItem.Text + " for " + dDate.ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";

            // Send Email Function
            ManifestSendEmail("Travelmart: Hotel Manifest Difference", sMsg, FileName.FullName, uoHiddenFieldEmail.Value, File);
            //ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, "This is a sample Hotel Manifest " + CurrentHotel, FileName, dt.Rows[iRow]["HotelEmail"].ToString(), File);

            //AlertMessage("Email sent.");

            ////Insert log audit trail (Gabriel Oquialda - 17/11/2011)
            //strLogDescription = "Hotel manifest as .xls file was sent to e-mail.";
            //strFunction = "BindHotelManifestExcel";

            //DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
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
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            if (Session["DateFrom"] == null)
            {
                Session["DateFrom"] = currentDate;
                Session["DateTo"] = currentDate;
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
                VesselDataTable = VesselBLL.GetVessel(uoHiddenFieldUser.Value, uoHiddenFieldDate.Value,
                    uoHiddenFieldDate.Value, GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value);
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Vessel--", "0");
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
            string GroupTextString = "Check In";
            string GroupValueString = "CheckIn";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != "")
            {
                if (currentDataFieldValue != "")
                {
                    if (lastDataFieldValue != Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy"))
                    {
                        //There's been a change! Record the change and emit the table row
                        lastDataFieldValue = Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy");
                        return string.Format("<tr><td class=\"group\" colspan=\"34\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
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
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        #endregion

        #region uoButtonEmailView_Click
        /// <summary>
        /// Date Created:   14/11/2011
        /// Created By:     Ryan Bautista
        /// (description)   View Email sent           
        /// </summary>
        ///
        protected void uoButtonEmailView_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hotel/HotelEmailView.aspx?ufn=" + Request.QueryString["ufn"].ToString());
        }
        #endregion

        #region HotelHasEvents
        ///// <summary>                                               
        ///// Date Created: 19/10/2011
        ///// Created By: Gabriel Oquialda
        ///// (description)  Event notification
        ///// </summary>  
        //private void HotelHasEvents()
        //{
        //    DataTable HotelEvents = null;

        //    try
        //    {
        //        HotelEvents = BLL.HotelBLL.HotelHasEvents();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (HotelEvents != null)
        //        {
        //            HotelEvents.Dispose();
        //        }
        //    }
        //}
        #endregion

        #region BindHotelManifestExcel
        /// <summary>
        /// Date Created:   15/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Create excel file of hotel manifest and email
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindHotelManifestExcel()
        {
            //string[] drive = Environment.GetLogicalDrives();
            //string sLogicalDrives;
            //foreach (string sDrive in drive)
            //{
            //    sLogicalDrives = sDrive.ToString();
            //}

            //string sLogicalDrives = Environment.GetEnvironmentVariable("USERPROFILE");

            //string sLogicalDrive = Environment.GetEnvironmentVariable("USERPROFILE").Split(":".ToCharArray())[0];
            //sLogicalDrive += ":\\";
            //Excel.ApplicationClass excel = new Excel.ApplicationClass();
            Excel.ApplicationClass excel = null;
            excel = new Excel.ApplicationClass();
            //Excel.Workbook workbook = excel.Application.Workbooks.Add(true);
            //string sTempExcel = ConfigurationSettings.AppSettings["TravelmartFileDirectory"].ToString();
            //sTempExcel += "\\HotelManifest\\HotelManifestTemp.xls";
            Excel.Workbook workbook = excel.Application.Workbooks.Add(true);
            Excel.Range oHeader;

            string CurrentHotel;
            string PreviousHotel = null;
            string PreviousEmail = null;
            string FileName = null;
            double Total = 0;
            double TotalMale = 0;
            double TotalFemale = 0;
            double TotalMaleSingle = 0;
            double TotalMaleDouble = 0;
            double TotalFemaleSingle = 0;
            double TotalFemaleDouble = 0;
            string Gender = "";
            //Int32 TotalRoom = 0;
            //Int32 TotalSingleRoom = 0;
            //Int32 TotalDoubleRoom = 0;
            Int32 MaleSingle = 0;
            Int32 MaleDouble = 0;
            Int32 FemaleSingle = 0;
            Int32 FemaleDouble = 0;
            //Int32 MaleSingleTotal = 0;
            //Int32 MaleDoubleTotal = 0;
            //Int32 FemaleSingleTotal = 0;
            //Int32 FemaleDoubleTotal = 0;
            Int32 MaleSDTotal = 0;
            Int32 FemaleSDTotal = 0;
            string RoomType = "";

            DataTable dt = null;
            //DataTable dt2 = null;

            try
            {
                DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);

                string sMsg = "Please find attached " + uoDropDownListHours.SelectedItem.Text + " for " + dDate.ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";

                dt = HotelManifestBLL.GetListHotelManifest_Locked(dDate, uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["strPendingFilter"]),
                     GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
                     GlobalCode.Field2String(Session["City"]),
                     uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                      GlobalCode.Field2String(Session["Port"]), uoDropDownListBranch.SelectedValue, GlobalCode.Field2String(Session["Vehicle"]),
                     uoDropDownListVessel.SelectedValue,
                     uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                     uoDropDownListRank.SelectedValue,
                     uoHiddenFieldRole.Value, uoDropDownListHours.SelectedValue
                    );

                if (dt.Rows.Count > 0)
                {
                    int iRow = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        //iRow++;
                        CurrentHotel = dt.Rows[iRow]["HotelBranch"].ToString();
                        string name = dt.Rows[iRow]["Last Name"].ToString();
                        Gender = dt.Rows[iRow]["Gender"].ToString();
                        RoomType = dt.Rows[iRow]["Single/Double"].ToString();

                        if (PreviousHotel == null)
                        {
                            PreviousHotel = dt.Rows[iRow]["HotelBranch"].ToString();
                            PreviousEmail = dt.Rows[iRow]["HotelEmail"].ToString();
                            uoHiddenFieldEmail.Value = PreviousEmail;
                            //excel.Cells[1, 1] = dt.Rows[0]["HotelBranch"].ToString();

                            // Add column headings...
                            int iCol = 0;
                            foreach (DataColumn c in dt.Columns)
                            {
                                iCol++;
                                //if (GlobalCode.Field2Bool(Session["EnableHourManifest"]) != true)
                                //{
                                //if (iCol <= 29)
                                //{
                                //    excel.Cells[1, iCol] = c.ColumnName;
                                //}
                                //}
                                //else
                                //{
                                if (iCol <= 34)
                                {
                                    excel.Cells[1, iCol] = c.ColumnName; //excel.Cells[2, iCol] = c.ColumnName;                                    
                                }
                                //}
                            }

                            iCol = 0;
                            foreach (DataColumn c in dt.Columns)
                            {
                                iCol++;

                                if (iCol <= 34)
                                {
                                    excel.Cells[iRow + 2, iCol] = row[c.ColumnName]; //excel.Cells[iRow + 3, iCol] = row[c.ColumnName];
                                }

                            }
                            // Gender count
                            if (Gender == "Male")
                            {
                                if (RoomType == "Single")
                                {
                                    TotalMaleSingle = TotalMaleSingle + .5;
                                    MaleSingle = MaleSingle + 1;
                                }
                                else
                                {
                                    TotalMaleDouble = TotalMaleDouble + 1;
                                    MaleDouble = MaleDouble + 1;
                                }
                                TotalMale = TotalMaleSingle + TotalMaleDouble;
                            }
                            else
                            {
                                if (RoomType == "Single")
                                {
                                    TotalFemaleSingle = TotalFemaleSingle + .5;
                                    FemaleSingle = FemaleSingle + 1;
                                }
                                else
                                {
                                    TotalFemaleDouble = TotalFemaleDouble + 1;
                                    FemaleDouble = FemaleDouble + 1;
                                }
                                TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                            }
                            Total = Total + 1; //Total = TotalMale + TotalFemale;
                            MaleSDTotal = MaleSingle + MaleDouble;
                            FemaleSDTotal = FemaleSingle + FemaleDouble;

                            ////Room count
                            //if (RoomType == "Single")
                            //{
                            //    TotalSingleRoom = TotalSingleRoom + 1;
                            //}
                            //else
                            //{
                            //    TotalDoubleRoom = TotalDoubleRoom + 1;
                            //}
                            //TotalRoom = TotalRoom + 1;                            

                            if (dt.Rows.Count == iRow + 1)
                            {
                                excel.Cells[iRow + 4, 11] = "Male Room:";
                                excel.Cells[iRow + 4, 12] = TotalMale;
                                excel.Cells[iRow + 5, 11] = "Female Room:";
                                excel.Cells[iRow + 5, 12] = TotalFemale;

                                excel.Cells[iRow + 6, 11] = "Total Room:";
                                excel.Cells[iRow + 6, 12] = Total;

                                excel.Cells[iRow + 8, 11] = "Male Single:";
                                excel.Cells[iRow + 8, 12] = MaleSingle;
                                excel.Cells[iRow + 9, 11] = "Male Double:";
                                excel.Cells[iRow + 9, 12] = MaleDouble;
                                excel.Cells[iRow + 10, 11] = "Male Single/Double Total:";
                                excel.Cells[iRow + 10, 12] = MaleSDTotal;

                                excel.Cells[iRow + 12, 11] = "Female Single:";
                                excel.Cells[iRow + 12, 12] = FemaleSingle;
                                excel.Cells[iRow + 13, 11] = "Female Double:";
                                excel.Cells[iRow + 13, 12] = FemaleDouble;
                                excel.Cells[iRow + 14, 11] = "Female Single/Double Total:";
                                excel.Cells[iRow + 14, 12] = FemaleSDTotal;

                                //// Room count 
                                //excel.Cells[iRow + 7, 11] = "Total Single:";
                                //excel.Cells[iRow + 7, 12] = TotalSingleRoom;
                                //excel.Cells[iRow + 8, 11] = "Total Double:";
                                //excel.Cells[iRow + 8, 12] = TotalDoubleRoom;

                                //excel.Cells[iRow + 9, 11] = "Total Single/Double:";
                                //excel.Cells[iRow + 9, 12] = TotalRoom;                                

                                // Global missing reference for objects we are not defining...
                                object missing = System.Reflection.Missing.Value;
                                string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                                //string FilePath = ConfigurationSettings.AppSettings["TravelmartFileDirectory"].ToString();
                                //FilePath += "\\HotelManifest\\";
                                //string FilePath = sLogicalDrive;
                                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                                FileName = FilePath + "HotelManifest_" + sDate + ".xls";
                                string File = "HotelManifest_" + sDate + ".xls";

                                // Auto fit worksheet columns and rows.
                                Excel.Worksheet WorksheetXLS = (Excel.Worksheet)workbook.ActiveSheet;
                                WorksheetXLS.Name = dt.Rows[0]["HotelBranch"].ToString(); //PreviousHotel;  
                                oHeader = WorksheetXLS.get_Range("A1", "AH1");
                                oHeader.Font.Bold = true;
                                WorksheetXLS.Cells.Columns.AutoFit();
                                WorksheetXLS.Cells.Rows.AutoFit();

                                // XLS page format.                               
                                Excel.PageSetup PageSetupXLS = WorksheetXLS.PageSetup;

                                PageSetupXLS.Orientation = Excel.XlPageOrientation.xlLandscape;

                                PageSetupXLS.Zoom = false;
                                PageSetupXLS.FitToPagesWide = 1;
                                PageSetupXLS.FitToPagesTall = 1;

                                PageSetupXLS.PaperSize = Excel.XlPaperSize.xlPaperA4;

                                PageSetupXLS.HeaderMargin = 0.3;
                                PageSetupXLS.TopMargin = 0.25;
                                PageSetupXLS.LeftMargin = 0.2;
                                PageSetupXLS.RightMargin = 0.2;
                                PageSetupXLS.BottomMargin = 0.25;
                                PageSetupXLS.FooterMargin = 0.3;
                                PageSetupXLS.CenterHorizontally = true;

                                // If wanting to Save the workbook...
                                workbook.SaveAs(FileName, Excel.XlFileFormat.xlXMLSpreadsheet,
                                    missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                    missing, missing, missing, missing, missing);

                                // If wanting to make Excel visible and activate the worksheet...
                                excel.Visible = false;
                                ((Excel._Worksheet)WorksheetXLS).Activate();

                                // If wanting excel to shutdown...
                                workbook.Close(null, null, null);
                                ((Excel._Application)excel).Quit();
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(WorksheetXLS);
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);

                                if (uoCheckBoxPDF.Checked == true)
                                {
                                    //Saving workbook to PDF format...
                                    string paramSourceBookPath = FileName;
                                    object paramMissing = Type.Missing;

                                    string paramExportFilePath = FilePath + "HotelManifest_" + sDate + ".pdf";
                                    Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
                                    Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
                                    bool paramOpenAfterPublish = false;
                                    bool paramIncludeDocProps = true;
                                    bool paramIgnorePrintAreas = true;
                                    object paramFromPage = Type.Missing;
                                    object paramToPage = Type.Missing;
                                    string paramFile = "HotelManifest_" + sDate + ".pdf";

                                    try
                                    {
                                        // Open the source workbook.
                                        workbook = excel.Workbooks.Open(paramSourceBookPath,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing);

                                        // Save it in the target format.
                                        if (workbook != null)
                                            workbook.ExportAsFixedFormat(paramExportFormat,
                                                paramExportFilePath, paramExportQuality,
                                                paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
                                                paramToPage, paramOpenAfterPublish,
                                                paramMissing);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Respond to the error.
                                    }
                                    finally
                                    {
                                        // Close the workbook object.
                                        if (workbook != null)
                                        {
                                            workbook.Close(false, paramMissing, paramMissing);
                                            workbook = null;
                                        }

                                        // Quit Excel and release the ApplicationClass object.
                                        if (excel != null)
                                        {
                                            excel.Quit();
                                            excel = null;
                                        }

                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                    }

                                    // Send Email Function .pdf
                                    //ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, "This is a sample Hotel Manifest " + CurrentHotel, paramExportFilePath, dt.Rows[iRow]["HotelEmail"].ToString(), paramFile, CurrentHotel);
                                    ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, paramExportFilePath, dt.Rows[iRow]["HotelEmail"].ToString(), paramFile, CurrentHotel);

                                    AlertMessage("Email sent.");
                                }
                                else
                                {
                                    // Send Email Function .xls
                                    //ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, "This is a sample Hotel Manifest " + CurrentHotel, FileName, dt.Rows[iRow]["HotelEmail"].ToString(), File, CurrentHotel);
                                    ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, FileName, dt.Rows[iRow]["HotelEmail"].ToString(), File, CurrentHotel);

                                    AlertMessage("Email sent.");
                                }
                            }
                        }
                        else
                        {
                            CurrentHotel = dt.Rows[iRow]["HotelBranch"].ToString();
                            string name2 = dt.Rows[iRow]["Last Name"].ToString();
                            //string Gender2 = dt.Rows[iRow]["Gender"].ToString();

                            if (PreviousHotel == CurrentHotel)
                            {
                                int iCol = 0;
                                foreach (DataColumn c in dt.Columns)
                                {
                                    iCol++;
                                    //if (GlobalCode.Field2Bool(Session["EnableHourManifest"]) != true)
                                    //{
                                    //if (iCol <= 29)
                                    //{
                                    //    excel.Cells[iRow + 2, iCol] = row[c.ColumnName];
                                    //}
                                    //}
                                    //else
                                    //{
                                    if (iCol <= 34)
                                    {
                                        excel.Cells[iRow + 2, iCol] = row[c.ColumnName];
                                    }
                                    //}
                                }
                                // Gender count
                                if (Gender == "Male")
                                {
                                    if (RoomType == "Single")
                                    {
                                        TotalMaleSingle = TotalMaleSingle + .5;
                                        MaleSingle = MaleSingle + 1;
                                    }
                                    else
                                    {
                                        TotalMaleDouble = TotalMaleDouble + 1;
                                        MaleDouble = MaleDouble + 1;
                                    }
                                    TotalMale = TotalMaleSingle + TotalMaleDouble;
                                }
                                else
                                {
                                    if (RoomType == "Single")
                                    {
                                        TotalFemaleSingle = TotalFemaleSingle + .5;
                                        FemaleSingle = FemaleSingle + 1;
                                    }
                                    else
                                    {
                                        TotalFemaleDouble = TotalFemaleDouble + 1;
                                        FemaleDouble = FemaleDouble + 1;
                                    }
                                    TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                                }
                                Total = Total + 1; //Total = TotalMale + TotalFemale;
                                MaleSDTotal = MaleSingle + MaleDouble;
                                FemaleSDTotal = FemaleSingle + FemaleDouble;

                                ////Room count
                                //if (RoomType == "Single")
                                //{
                                //    TotalSingleRoom = TotalSingleRoom + 1;
                                //}
                                //else
                                //{
                                //    TotalDoubleRoom = TotalDoubleRoom + 1;
                                //}
                                //TotalRoom = TotalRoom + 1;                                

                                if (dt.Rows.Count == iRow)
                                {
                                    excel.Cells[iRow + 4, 11] = "Male Room:";
                                    excel.Cells[iRow + 4, 12] = TotalMale;
                                    excel.Cells[iRow + 5, 11] = "Female Room:";
                                    excel.Cells[iRow + 5, 12] = TotalFemale;

                                    excel.Cells[iRow + 6, 11] = "Total Room:";
                                    excel.Cells[iRow + 6, 12] = Total;

                                    excel.Cells[iRow + 8, 11] = "Male Single:";
                                    excel.Cells[iRow + 8, 12] = MaleSingle;
                                    excel.Cells[iRow + 9, 11] = "Male Double:";
                                    excel.Cells[iRow + 9, 12] = MaleDouble;
                                    excel.Cells[iRow + 10, 11] = "Male Single/Double Total:";
                                    excel.Cells[iRow + 10, 12] = MaleSDTotal;

                                    excel.Cells[iRow + 12, 11] = "Female Single:";
                                    excel.Cells[iRow + 12, 12] = FemaleSingle;
                                    excel.Cells[iRow + 13, 11] = "Female Double:";
                                    excel.Cells[iRow + 13, 12] = FemaleDouble;
                                    excel.Cells[iRow + 14, 11] = "Female Single/Double Total:";
                                    excel.Cells[iRow + 14, 12] = FemaleSDTotal;

                                    //// Room count 
                                    //excel.Cells[iRow + 7, 11] = "Total Single:";
                                    //excel.Cells[iRow + 7, 12] = TotalSingleRoom;
                                    //excel.Cells[iRow + 8, 11] = "Total Double:";
                                    //excel.Cells[iRow + 8, 12] = TotalDoubleRoom;

                                    //excel.Cells[iRow + 9, 11] = "Total Single/Double:";
                                    //excel.Cells[iRow + 9, 12] = TotalRoom;                                 

                                    // Global missing reference for objects we are not defining...
                                    object missing = System.Reflection.Missing.Value;
                                    string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                                    //string FilePath = ConfigurationSettings.AppSettings["TravelmartFileDirectory"].ToString();
                                    FilePath += "\\HotelManifest\\";
                                    string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                                    FileName = FilePath + "HotelManifest_" + sDate + ".xls";
                                    string File = "HotelManifest_" + sDate + ".xls";

                                    // Auto fit worksheet columns and rows.
                                    Excel.Worksheet WorksheetXLS = (Excel.Worksheet)workbook.ActiveSheet;
                                    WorksheetXLS.Name = dt.Rows[0]["HotelBranch"].ToString(); //PreviousHotel;
                                    oHeader = WorksheetXLS.get_Range("A1", "AH1");
                                    oHeader.Font.Bold = true;
                                    WorksheetXLS.Cells.Columns.AutoFit();
                                    WorksheetXLS.Cells.Rows.AutoFit();

                                    // XLS page format.                                    
                                    Excel.PageSetup PageSetupXLS = WorksheetXLS.PageSetup;

                                    PageSetupXLS.Orientation = Excel.XlPageOrientation.xlLandscape;

                                    PageSetupXLS.Zoom = false;
                                    PageSetupXLS.FitToPagesWide = 1;
                                    PageSetupXLS.FitToPagesTall = 1;

                                    PageSetupXLS.PaperSize = Excel.XlPaperSize.xlPaperA4;

                                    PageSetupXLS.HeaderMargin = 0.3;
                                    PageSetupXLS.TopMargin = 0.25;
                                    PageSetupXLS.LeftMargin = 0.2;
                                    PageSetupXLS.RightMargin = 0.2;
                                    PageSetupXLS.BottomMargin = 0.25;
                                    PageSetupXLS.FooterMargin = 0.3;
                                    PageSetupXLS.CenterHorizontally = true;

                                    // If wanting to Save the workbook...
                                    workbook.SaveAs(FileName, Excel.XlFileFormat.xlXMLSpreadsheet,
                                        missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                        missing, missing, missing, missing, missing);

                                    // If wanting to make Excel visible and activate the worksheet...
                                    excel.Visible = false;
                                    ((Excel._Worksheet)WorksheetXLS).Activate();

                                    // If wanting excel to shutdown...
                                    workbook.Close(null, null, null);
                                    ((Excel._Application)excel).Quit();
                                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(WorksheetXLS);
                                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);

                                    if (uoCheckBoxPDF.Checked == true)
                                    {
                                        //Saving workbook to PDF format...
                                        string paramSourceBookPath = FileName;
                                        object paramMissing = Type.Missing;

                                        string paramExportFilePath = FilePath + "HotelManifest_" + sDate + ".pdf";
                                        Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
                                        Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
                                        bool paramOpenAfterPublish = false;
                                        bool paramIncludeDocProps = true;
                                        bool paramIgnorePrintAreas = true;
                                        object paramFromPage = Type.Missing;
                                        object paramToPage = Type.Missing;
                                        string paramFile = "HotelManifest_" + sDate + ".pdf";

                                        try
                                        {
                                            // Open the source workbook.
                                            workbook = excel.Workbooks.Open(paramSourceBookPath,
                                                paramMissing, paramMissing, paramMissing, paramMissing,
                                                paramMissing, paramMissing, paramMissing, paramMissing,
                                                paramMissing, paramMissing, paramMissing, paramMissing,
                                                paramMissing, paramMissing);

                                            // Save it in the target format.
                                            if (workbook != null)
                                                workbook.ExportAsFixedFormat(paramExportFormat,
                                                    paramExportFilePath, paramExportQuality,
                                                    paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
                                                    paramToPage, paramOpenAfterPublish,
                                                    paramMissing);
                                        }
                                        catch (Exception ex)
                                        {
                                            // Respond to the error.
                                        }
                                        finally
                                        {
                                            // Close the workbook object.
                                            if (workbook != null)
                                            {
                                                workbook.Close(false, paramMissing, paramMissing);
                                                workbook = null;
                                            }

                                            // Quit Excel and release the ApplicationClass object.
                                            if (excel != null)
                                            {
                                                excel.Quit();
                                                excel = null;
                                            }

                                            GC.Collect();
                                            GC.WaitForPendingFinalizers();
                                            GC.Collect();
                                            GC.WaitForPendingFinalizers();
                                        }

                                        // Send Email Function .pdf
                                        ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, paramExportFilePath, dt.Rows[iRow]["HotelEmail"].ToString(), paramFile, CurrentHotel);

                                        AlertMessage("Email sent.");

                                        break;
                                    }
                                    else
                                    {
                                        // Send Email Function .xls                                      
                                        ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, FileName, dt.Rows[iRow]["HotelEmail"].ToString(), File, CurrentHotel);

                                        AlertMessage("Email sent.");

                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // Gender count
                                if (Gender == "Male")
                                {
                                    if (RoomType == "Single")
                                    {
                                        TotalMaleSingle = TotalMaleSingle + .5;
                                        MaleSingle = MaleSingle + 1;
                                    }
                                    else
                                    {
                                        TotalMaleDouble = TotalMaleDouble + 1;
                                        MaleDouble = MaleDouble + 1;
                                    }
                                    TotalMale = TotalMaleSingle + TotalMaleDouble;
                                }
                                else
                                {
                                    if (RoomType == "Single")
                                    {
                                        TotalFemaleSingle = TotalFemaleSingle + .5;
                                        FemaleSingle = FemaleSingle + 1;
                                    }
                                    else
                                    {
                                        TotalFemaleDouble = TotalFemaleDouble + 1;
                                        FemaleDouble = FemaleDouble + 1;
                                    }
                                    TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                                }
                                Total = Total + 1; //Total = TotalMale + TotalFemale;
                                MaleSDTotal = MaleSingle + MaleDouble;
                                FemaleSDTotal = FemaleSingle + FemaleDouble;

                                //// Room count
                                //if (RoomType == "Single")
                                //{
                                //    TotalSingleRoom = TotalSingleRoom + 1;
                                //}
                                //else
                                //{
                                //    TotalDoubleRoom = TotalDoubleRoom + 1;
                                //}
                                //TotalRoom = TotalRoom + 1;                                

                                excel.Cells[iRow + 4, 11] = "Male Room:";
                                excel.Cells[iRow + 4, 12] = TotalMale;
                                excel.Cells[iRow + 5, 11] = "Female Room:";
                                excel.Cells[iRow + 5, 12] = TotalFemale;

                                excel.Cells[iRow + 6, 11] = "Total Room:";
                                excel.Cells[iRow + 6, 12] = Total;

                                excel.Cells[iRow + 8, 11] = "Male Single:";
                                excel.Cells[iRow + 8, 12] = MaleSingle;
                                excel.Cells[iRow + 9, 11] = "Male Double:";
                                excel.Cells[iRow + 9, 12] = MaleDouble;
                                excel.Cells[iRow + 10, 11] = "Male Single/Double Total:";
                                excel.Cells[iRow + 10, 12] = MaleSDTotal;

                                excel.Cells[iRow + 12, 11] = "Female Single:";
                                excel.Cells[iRow + 12, 12] = FemaleSingle;
                                excel.Cells[iRow + 13, 11] = "Female Double:";
                                excel.Cells[iRow + 13, 12] = FemaleDouble;
                                excel.Cells[iRow + 14, 11] = "Female Single/Double Total:";
                                excel.Cells[iRow + 14, 12] = FemaleSDTotal;

                                //// Room count 
                                //excel.Cells[iRow + 7, 11] = "Total Single:";
                                //excel.Cells[iRow + 7, 12] = TotalSingleRoom;
                                //excel.Cells[iRow + 8, 11] = "Total Double:";
                                //excel.Cells[iRow + 8, 12] = TotalDoubleRoom;

                                //excel.Cells[iRow + 9, 11] = "Total Single/Double:";
                                //excel.Cells[iRow + 9, 12] = TotalRoom;

                                // Global missing reference for objects we are not defining...
                                object missing = System.Reflection.Missing.Value;
                                string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                                //string FilePath = ConfigurationSettings.AppSettings["TravelmartFileDirectory"].ToString();
                                //FilePath += "\\HotelManifest\\";
                                //string FilePath = sLogicalDrive;
                                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                                FileName = FilePath + "HotelManifest_" + sDate + ".xls";
                                string File = "HotelManifest_" + sDate + ".xls";

                                // Auto fit worksheet columns and rows.
                                Excel.Worksheet WorksheetXLS = (Excel.Worksheet)workbook.ActiveSheet;
                                WorksheetXLS.Name = dt.Rows[0]["HotelBranch"].ToString(); //PreviousHotel;
                                oHeader = WorksheetXLS.get_Range("A1", "AH1");
                                oHeader.Font.Bold = true;
                                WorksheetXLS.Cells.Columns.AutoFit();
                                WorksheetXLS.Cells.Rows.AutoFit();

                                // XLS page format.                                
                                Excel.PageSetup PageSetupXLS = WorksheetXLS.PageSetup;

                                PageSetupXLS.Orientation = Excel.XlPageOrientation.xlLandscape;

                                PageSetupXLS.Zoom = false;
                                PageSetupXLS.FitToPagesWide = 1;
                                PageSetupXLS.FitToPagesTall = 1;

                                PageSetupXLS.PaperSize = Excel.XlPaperSize.xlPaperA4;

                                PageSetupXLS.HeaderMargin = 0.3;
                                PageSetupXLS.TopMargin = 0.25;
                                PageSetupXLS.LeftMargin = 0.2;
                                PageSetupXLS.RightMargin = 0.2;
                                PageSetupXLS.BottomMargin = 0.25;
                                PageSetupXLS.FooterMargin = 0.3;
                                PageSetupXLS.CenterHorizontally = true;

                                // If wanting to Save the workbook...
                                workbook.SaveAs(FileName, Excel.XlFileFormat.xlXMLSpreadsheet,
                                    missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                    missing, missing, missing, missing, missing);

                                // If wanting to make Excel visible and activate the worksheet...
                                excel.Visible = false;
                                ((Excel._Worksheet)WorksheetXLS).Activate();

                                // If wanting excel to shutdown...
                                workbook.Close(null, null, null);
                                ((Excel._Application)excel).Quit();
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(WorksheetXLS);
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);

                                if (uoCheckBoxPDF.Checked == true)
                                {
                                    //Saving workbook to PDF format...
                                    string paramSourceBookPath = FileName;
                                    object paramMissing = Type.Missing;

                                    string paramExportFilePath = FilePath + "HotelManifest_" + sDate + ".pdf";
                                    Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
                                    Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
                                    bool paramOpenAfterPublish = false;
                                    bool paramIncludeDocProps = true;
                                    bool paramIgnorePrintAreas = true;
                                    object paramFromPage = Type.Missing;
                                    object paramToPage = Type.Missing;
                                    string paramFile = "HotelManifest_" + sDate + ".pdf";

                                    try
                                    {
                                        // Open the source workbook.
                                        workbook = excel.Workbooks.Open(paramSourceBookPath,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing);

                                        // Save it in the target format.
                                        if (workbook != null)
                                            workbook.ExportAsFixedFormat(paramExportFormat,
                                                paramExportFilePath, paramExportQuality,
                                                paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
                                                paramToPage, paramOpenAfterPublish,
                                                paramMissing);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Respond to the error.
                                    }
                                    finally
                                    {
                                        // Close the workbook object.
                                        if (workbook != null)
                                        {
                                            workbook.Close(false, paramMissing, paramMissing);
                                            workbook = null;
                                        }

                                        // Quit Excel and release the ApplicationClass object.
                                        if (excel != null)
                                        {
                                            excel.Quit();
                                            excel = null;
                                        }

                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                    }

                                    // Send Email Function .pdf
                                    ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, paramExportFilePath, dt.Rows[iRow]["HotelEmail"].ToString(), paramFile, CurrentHotel);

                                    AlertMessage("Email sent.");
                                }
                                else
                                {
                                    // Send Email Function .xls
                                    ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, FileName, dt.Rows[iRow]["HotelEmail"].ToString(), File, CurrentHotel);

                                    AlertMessage("Email sent.");
                                }

                                // New excel
                                excel = null;
                                workbook = null;
                                excel = new Excel.ApplicationClass();
                                //workbook = excel.Application.Workbooks.Add(true);
                                workbook = excel.Application.Workbooks.Add(File);
                                PreviousHotel = dt.Rows[iRow]["HotelBranch"].ToString();

                                

                                int iCol = 0;
                                foreach (DataColumn c in dt.Columns)
                                {
                                    iCol++;
                                    if (GlobalCode.Field2Bool(Session["EnableHourManifest"]) != true)
                                    {
                                        if (iCol <= 34)
                                        {
                                            excel.Cells[1, iCol] = c.ColumnName;
                                        }
                                    }
                                    else
                                    {
                                        if (iCol <= 21)
                                        {
                                            excel.Cells[1, iCol] = c.ColumnName;
                                        }
                                    }
                                }

                                iCol = 0;
                                int iRow2 = 1;
                                foreach (DataColumn c in dt.Columns)
                                {
                                    iCol++;
                                    if (GlobalCode.Field2Bool(Session["EnableHourManifest"]) != true)
                                    {
                                        if (iCol <= 34)
                                        {
                                            excel.Cells[iRow2 + 1, iCol] = row[c.ColumnName];
                                        }
                                    }
                                    else
                                    {
                                        if (iCol <= 21)
                                        {
                                            excel.Cells[iRow2 + 1, iCol] = row[c.ColumnName];
                                        }
                                    }
                                }
                                iRow2++;

                            }

                            if (dt.Rows.Count == iRow + 1)
                            {
                                excel.Cells[iRow + 4, 11] = "Male Room:";
                                excel.Cells[iRow + 4, 12] = TotalMale;
                                excel.Cells[iRow + 5, 11] = "Female Room:";
                                excel.Cells[iRow + 5, 12] = TotalFemale;

                                excel.Cells[iRow + 6, 11] = "Total Room:";
                                excel.Cells[iRow + 6, 12] = Total;

                                excel.Cells[iRow + 8, 11] = "Male Single:";
                                excel.Cells[iRow + 8, 12] = MaleSingle;
                                excel.Cells[iRow + 9, 11] = "Male Double:";
                                excel.Cells[iRow + 9, 12] = MaleDouble;
                                excel.Cells[iRow + 10, 11] = "Male Single/Double Total:";
                                excel.Cells[iRow + 10, 12] = MaleSDTotal;

                                excel.Cells[iRow + 12, 11] = "Female Single:";
                                excel.Cells[iRow + 12, 12] = FemaleSingle;
                                excel.Cells[iRow + 13, 11] = "Female Double:";
                                excel.Cells[iRow + 13, 12] = FemaleDouble;
                                excel.Cells[iRow + 14, 11] = "Female Single/Double Total:";
                                excel.Cells[iRow + 14, 12] = FemaleSDTotal;

                                //// Room count //
                                //excel.Cells[iRow + 7, 11] = "Total Single:";
                                //excel.Cells[iRow + 7, 12] = TotalSingleRoom;
                                //excel.Cells[iRow + 8, 11] = "Total Double:";
                                //excel.Cells[iRow + 8, 12] = TotalDoubleRoom;

                                //excel.Cells[iRow + 9, 11] = "Total Single/Double:";
                                //excel.Cells[iRow + 9, 12] = TotalRoom;

                                // Global missing reference for objects we are not defining...
                                object missing = System.Reflection.Missing.Value;
                                string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                                //string FilePath = ConfigurationSettings.AppSettings["TravelmartFileDirectory"].ToString();
                                //FilePath += "\\HotelManifest\\";
                                //string FilePath = sLogicalDrive;
                                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                                FileName = FilePath + "HotelManifest_" + sDate + ".xls";
                                string File = "HotelManifest_" + sDate + ".xls";

                                // Auto fit worksheet columns and rows.
                                Excel.Worksheet WorksheetXLS = (Excel.Worksheet)workbook.ActiveSheet;
                                WorksheetXLS.Name = dt.Rows[0]["HotelBranch"].ToString(); //PreviousHotel;
                                oHeader = WorksheetXLS.get_Range("A1", "AH1");
                                oHeader.Font.Bold = true;
                                WorksheetXLS.Cells.Columns.AutoFit();
                                WorksheetXLS.Cells.Rows.AutoFit();

                                // XLS page format.                                
                                Excel.PageSetup PageSetupXLS = WorksheetXLS.PageSetup;

                                PageSetupXLS.Orientation = Excel.XlPageOrientation.xlLandscape;

                                PageSetupXLS.Zoom = false;
                                PageSetupXLS.FitToPagesWide = 1;
                                PageSetupXLS.FitToPagesTall = 1;

                                PageSetupXLS.PaperSize = Excel.XlPaperSize.xlPaperA4;

                                PageSetupXLS.HeaderMargin = 0.3;
                                PageSetupXLS.TopMargin = 0.25;
                                PageSetupXLS.LeftMargin = 0.2;
                                PageSetupXLS.RightMargin = 0.2;
                                PageSetupXLS.BottomMargin = 0.25;
                                PageSetupXLS.FooterMargin = 0.3;
                                PageSetupXLS.CenterHorizontally = true;

                                // If wanting to Save the workbook...
                                workbook.SaveAs(FileName, Excel.XlFileFormat.xlXMLSpreadsheet,
                                    missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                    missing, missing, missing, missing, missing);

                                // If wanting to make Excel visible and activate the worksheet...
                                excel.Visible = false;
                                ((Excel._Worksheet)WorksheetXLS).Activate();

                                // If wanting excel to shutdown...
                                workbook.Close(null, null, null);
                                ((Excel._Application)excel).Quit();

                                if (uoCheckBoxPDF.Checked == true)
                                {
                                    //Saving workbook to PDF format...
                                    string paramSourceBookPath = FileName;
                                    object paramMissing = Type.Missing;

                                    string paramExportFilePath = FilePath + "HotelManifest_" + sDate + ".pdf";
                                    Excel.XlFixedFormatType paramExportFormat = Excel.XlFixedFormatType.xlTypePDF;
                                    Excel.XlFixedFormatQuality paramExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
                                    bool paramOpenAfterPublish = false;
                                    bool paramIncludeDocProps = true;
                                    bool paramIgnorePrintAreas = true;
                                    object paramFromPage = Type.Missing;
                                    object paramToPage = Type.Missing;
                                    string paramFile = "HotelManifest_" + sDate + ".pdf";

                                    try
                                    {
                                        // Open the source workbook.
                                        workbook = excel.Workbooks.Open(paramSourceBookPath,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing, paramMissing, paramMissing,
                                            paramMissing, paramMissing);

                                        // Save it in the target format.
                                        if (workbook != null)
                                            workbook.ExportAsFixedFormat(paramExportFormat,
                                                paramExportFilePath, paramExportQuality,
                                                paramIncludeDocProps, paramIgnorePrintAreas, paramFromPage,
                                                paramToPage, paramOpenAfterPublish,
                                                paramMissing);
                                    }
                                    catch (Exception ex)
                                    {
                                        // Respond to the error.
                                    }
                                    finally
                                    {
                                        // Close the workbook object.
                                        if (workbook != null)
                                        {
                                            workbook.Close(false, paramMissing, paramMissing);
                                            workbook = null;
                                        }

                                        // Quit Excel and release the ApplicationClass object.
                                        if (excel != null)
                                        {
                                            excel.Quit();
                                            excel = null;
                                        }

                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                        GC.Collect();
                                        GC.WaitForPendingFinalizers();
                                    }

                                    // Send Email Function .pdf
                                    ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, paramExportFilePath, dt.Rows[iRow]["HotelEmail"].ToString(), paramFile, CurrentHotel);

                                    AlertMessage("Email sent.");

                                    break;
                                }
                                else
                                {
                                    // Send Email Function .xls                                       
                                    ManifestSendEmail("Travelmart: Hotel Manifest " + CurrentHotel, sMsg, FileName, dt.Rows[iRow]["HotelEmail"].ToString(), File, CurrentHotel);

                                    AlertMessage("Email sent.");

                                    break;
                                }
                            }
                        }
                        iRow++;
                    }
                    //// Send Email Function
                    //ManifestSendEmail("Travelmart: Hotel Manifest", "This is a sample Hotel Manifest", FileName);

                    //AlertMessage("Email sent.");
                }
            }
            catch (Exception ex)
            {
                //DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                //ErrorBLL.InsertError(ex.Message, ex.StackTrace.ToString(), Request.Url.AbsolutePath,
                //    currentDate, CommonFunctions.GetDateTimeGMT(currentDate), GlobalCode.Field2String(Session["UserName"])); 
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                //                uoGridViewHotelManifest.Visible = false;
            }
        }
        #endregion

        #region GetHotelManifestByHours
        /// <summary>
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  29/11/2011
        /// Description:    Enable comparison
        /// </summary>
        private void GetHotelManifestByHours()
        {
            DataTable dt = null;
            try
            {
                DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);
                dt = HotelManifestBLL.GetLockedManifest(dDate, uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["strPendingFilter"]),
                     GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
                     GlobalCode.Field2String(Session["City"]),
                     uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                      GlobalCode.Field2String(Session["Port"]), uoDropDownListBranch.SelectedValue, GlobalCode.Field2String(Session["Vehicle"]),
                     uoDropDownListVessel.SelectedValue,
                     uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                     uoDropDownListRank.SelectedValue,
                     uoHiddenFieldRole.Value, uoDropDownListHours.SelectedValue
                    );

                if (dt.Rows.Count > 0)
                {
                    uolistviewHotelInfo.DataSource = dt;
                    uoLabelCompare.Visible = true;
                    uoDropDownListCompare.Visible = true;
                    uoButtonCompare.Visible = true;
                    uoLabelCompareHeader.Visible = true;
                    uoCheckBoxPDF.Enabled = true;
                    uoButtonRequestSendEmail.Visible = true;
                }
                else
                {
                    uoLabelCompare.Visible = false;
                    uoDropDownListCompare.Visible = false;
                    uoButtonCompare.Visible = false;
                    uoCheckBoxPDF.Enabled = false;
                    uoLabelCompareHeader.Visible = false;
                    uoButtonRequestSendEmail.Visible = false;
                    uoLabelCompareHeader.Visible = false;
                    uoListDiff.Visible = false;
                    uoUpdatePanelDiffHeader.Update();
                    uoUpdatePanelDiff.Update();
                }

                uolistviewHotelInfo.DataBind();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        #endregion

        #region _Create_DataTable
        //void _Create_DataTable(DataTable dt)
        //{
        //    DataTable dt2 = new DataTable();
        //    dt2.Columns.Add("Room Type");
        //    dt2.Columns.Add("Date From");
        //    dt2.Columns.Add("Date To");

        //    DataRow dr;
        //    dr = dt.NewRow();
        //    dt2.Rows.Add(this._Create_DataRow(dr, currency));

        //    ViewState["Table"] = dt;
        //}
        #endregion

        #region _Create_DataRow
        //DataRow _Create_DataRow(DataRow dr, string currency)
        //{
        //    dr["Currency"] = currency;

        //    return dr;
        //}
        #endregion

        #region ManifestSendEmail
        /// <summary>
        /// Date Created:   15/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Send email
        /// </summary>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        /// <param name="attachment"></param>
        private void ManifestSendEmail(string sSubject, string sMessage, string attachment, string EmailVendor, string file, string Hotel)
        {
            string sBody;
            DataTable dt = null;
            try
            {
                //dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, GlobalCode.Field2String(Session["UserBranchID"]),  GlobalCode.Field2String(Session["Country"]));
                //foreach (DataRow r in dt.Rows)
                //{
                sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                sBody += "Dear " + Hotel + ", <br/><br/> " + sMessage;
                sBody += "</TR></TD></TABLE>";

                if (EmailVendor != "")
                {
                    CommonFunctions.SendEmailWithAttachment("", EmailVendor, sSubject, sBody, attachment);
                }

                //Insert Email logs
                CommonFunctions.InsertEmailLog(EmailVendor, "travelmart.ptc@gmail.com", sSubject, file, DateTime.Now, uoHiddenFieldUser.Value);
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
            IDataReader dr = null;
            try
            {
                //dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, GlobalCode.Field2String(Session["UserBranchID"]),  GlobalCode.Field2String(Session["Country"]));
                dr = UserAccountBLL.GetUserInfoByName(uoHiddenFieldUser.Value);
                //foreach (DataRow r in dt.Rows)
                //{
                if (dr.Read())
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "<br/><br/> " + sMessage;
                    sBody += "</TR></TD></TABLE>";

                    if (EmailVendor != "")
                    {
                        CommonFunctions.SendEmailWithAttachment(dr["Email"].ToString(), EmailVendor, sSubject, sBody, attachment);
                    }
                }
                //Insert Email logs
                CommonFunctions.InsertEmailLog(EmailVendor, "travelmart.ptc@gmail.com", sSubject, file, DateTime.Now, uoHiddenFieldUser.Value);
                //}
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


        #region DataTabletoExcel
        //private void DataTabletoExcel(DataTable dt)
        //{
        //    ////auto save
        //    string FilePath = MapPath("~/Extract/HotelManifest/");
        //    string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
        //    FileInfo FileName = new FileInfo(FilePath + "HotelManifest_" + sDate + ".xls");
        //    Response.Clear();
        //    Response.ClearContent();
        //    StringWriter stringWrite = new StringWriter();
        //    HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //    uoGridViewHotelManifest.RenderControl(htmlWrite);
        //    FileStream fs = new FileStream(FileName.FullName, FileMode.Create);
        //    StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
        //    sWriter.Write(stringWrite.ToString().Trim());
        //    sWriter.Close();
        //    fs.Close();
        //    //Use below line instead of Response.End() to avoid Error: Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.;  
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();

        //    // Send Email Function
        //    ManifestSendEmail("Travelmart: Hotel Manifest", "This is a sample Hotel Manifest", FileName.FullName);

        //    AlertMessage("Email sent.");

        //    //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
        //    strLogDescription = "Hotel manifest as .xls file was sent to e-mail.";
        //    strFunction = "BindHotelManifestExcel";

        //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //    BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        //}
        #endregion

        //#region ExportToSpreadsheet
        //private void ExportToSpreadsheet(DataTable table)
        //{
        //    string FilePath = Server.MapPath("~/Extract/HotelManifest/");
        //    string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
        //    FileInfo FileName = new FileInfo(FilePath + "HotelManifest_" + sDate + ".xls");

        //    HttpContext context = HttpContext.Current;
        //    context.Response.Clear();
        //    foreach (DataColumn column in table.Columns)
        //    {
        //        context.Response.Write(column.ColumnName + ";");
        //    }
        //    context.Response.Write(Environment.NewLine);
        //    foreach (DataRow row in table.Rows)
        //    {
        //        for (int i = 0; i < table.Columns.Count; i++)
        //        {
        //            context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
        //        }
        //        context.Response.Write(Environment.NewLine);
        //    }
        //    context.Response.ContentType = "text/xls";
        //    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
        //    context.Response.End();
        //}
        //#endregion

        //#region Excel_FromDataTable
        //private void Excel_FromDataTable(DataTable dt)
        //{
        //    Excel.ApplicationClass excel = new Excel.ApplicationClass();
        //    Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

        //    // Add column headings...
        //    int iCol = 0;
        //    foreach (DataColumn c in dt.Columns)
        //    {
        //        iCol++;
        //        excel.Cells[1, iCol] = c.ColumnName;
        //    }
        //    // for each row of data...
        //    int iRow = 0;
        //    foreach (DataRow r in dt.Rows)
        //    {
        //        iRow++;

        //        // add each row's cell data...
        //        iCol = 0;
        //        foreach (DataColumn c in dt.Columns)
        //        {
        //            iCol++;
        //            excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
        //        }
        //    }

        //    // Global missing reference for objects we are not defining...
        //    object missing = System.Reflection.Missing.Value;
        //    string FilePath = Server.MapPath("~/Extract/HotelManifest/");
        //    string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
        //    //FileInfo FileName = new FileInfo(FilePath + "HotelManifest_" + sDate + ".xls");
        //    string FileName = FilePath + "HotelManifest_" + sDate + ".xls";

        //    // If wanting <strong class="highlight">to</strong> Save the workbook...
        //    workbook.SaveAs(FileName, Excel.XlFileFormat.xlXMLSpreadsheet,
        //        missing, missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
        //        missing, missing, missing, missing, missing);

        //    // If wanting <strong class="highlight">to</strong> make <strong class="highlight">Excel</strong> visible and activate the worksheet...
        //    excel.Visible = true;
        //    Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
        //    ((Excel._Worksheet)worksheet).Activate();

        //    // If wanting <strong class="highlight">excel</strong> <strong class="highlight">to</strong> shutdown...
        //    ((Excel._Application)excel).Quit();
        //}
        //#endregion

        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Manifest Type or Hours
        /// ---------------------------------------------
        /// Date Modified:  29/11/2011
        /// Modified By:    Charlene Remotigue
        /// (description)   populate manifest comparison dropdownlist
        /// </summary>
        private void GetManifestType()
        {
            DataTable dt = null;
            try
            {
                #region MANIFEST HOURS
                uoDropDownListHours.Items.Clear();
                ListItem item = new ListItem("--Select Manifest Hours--", "0");
                uoDropDownListHours.Items.Add(item);

                dt = HotelManifestBLL.GetManifestType();

                if (dt.Rows.Count > 0)
                {
                    uoDropDownListHours.DataTextField = "Manifest";
                    uoDropDownListHours.DataValueField = "ManifestHrs";
                    uoDropDownListHours.DataSource = dt;
                }
                uoDropDownListHours.DataBind();
                if (GlobalCode.Field2String(Session["ManifestHrs"]) != "")
                {
                    uoDropDownListHours.SelectedValue = GlobalCode.Field2String(Session["ManifestHrs"]);
                }
                #endregion


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
        /// ---------------------------------------------
        /// Date Modified:  29/11/2011
        /// Modified By:    Charlene Remotigue
        /// (description)   populate manifest comparison dropdownlist
        /// </summary>
        private void GetCompareType()
        {
            DataTable dt = null;
            try
            {
                #region MANIFEST COMPARE
                uoDropDownListCompare.Items.Clear();
                ListItem item = new ListItem("--Select Comparison--", "-1");
                uoDropDownListCompare.Items.Add(item);
                item = new ListItem("Current Transactions", "0");
                uoDropDownListCompare.Items.Add(item);

                dt = HotelManifestBLL.GetManifestType();

                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCompare.DataTextField = "Manifest";
                    uoDropDownListCompare.DataValueField = "ManifestHrs";
                    uoDropDownListCompare.DataSource = dt;
                }
                uoDropDownListCompare.DataBind();
                if (GlobalCode.Field2String(Session["ManifestHrs"]) != "")
                {
                    uoDropDownListCompare.Items.Remove(uoDropDownListHours.SelectedItem);
                }
                #endregion

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
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Hotel Branch
        /// </summary>
        private void GetHotelBranch()
        {
            DataTable dt = null;
            try
            {
                uoDropDownListBranch.Items.Clear();
                ListItem item = new ListItem("--Select Hotel--", "0");
                uoDropDownListBranch.Items.Add(item);

                dt = HotelManifestBLL.GetHotelBranchListByUser("", uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, "");

                int iRowCOunt = dt.Rows.Count;
                if (iRowCOunt == 1)
                {
                    Session["Hotel"] = dt.Rows[0]["BranchID"].ToString();
                }

                if (iRowCOunt > 0)
                {
                    uoDropDownListBranch.DataTextField = "BranchName";
                    uoDropDownListBranch.DataValueField = "BranchID";
                    uoDropDownListBranch.DataSource = dt;
                }
                uoDropDownListBranch.DataBind();
                if (GlobalCode.Field2String(Session["Hotel"]) != "")
                {
                    uoDropDownListBranch.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
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
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set the title based from Manifest Type or Hours
        /// </summary>
        private void SetTitle()
        {
            if (uoDropDownListHours.SelectedValue != "0")
            {
                uoLabelHeader.Text = uoDropDownListHours.SelectedItem.Text + " for " + GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("dd-MMM-yyyy");
                //Label MasterLabel = (Label)Master.FindControl("uclabelStatus");
                //MasterLabel.Text = uoDropDownListHours.SelectedItem.Text + " for " + DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"])).ToString("dd-MMM-yyyy"); ;
            }
        }
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
            if (uoDropDownListBranch.SelectedValue == "0")
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

                Cache.Remove("TentativeManifestCalendarDashboard");
                GetCalendarTable();
            }
        }
        private void SetControls()
        {
            if (uolistviewHotelInfo.Items.Count > 0)
            {
                uoLabelCompare.Visible = true;
                uoDropDownListCompare.Visible = true;
                uoButtonCompare.Visible = true;
                uoLabelCompareHeader.Visible = true;
                uoCheckBoxPDF.Enabled = true;
                uoButtonRequestSendEmail.Visible = true;
            }
            else
            {
                uoLabelCompare.Visible = false;
                uoDropDownListCompare.Visible = false;
                uoButtonCompare.Visible = false;
                uoLabelCompareHeader.Visible = false;
                uoCheckBoxPDF.Enabled = false;
                uoButtonRequestSendEmail.Visible = false;
            }
            uoPanelCompare.Update();
        }

        private void GetManifestDiff()
        {
            uoHiddenFieldManifestDate.Value = uoHiddenFieldDate.Value;
            //DataTable dt = HotelManifestBLL.
            uoListDiff.Items.Clear();
            uoListDiff.DataSource = null;
            uoListDiff.DataSourceID = "ObjectDataSource1";
        }

        DateTime GetDateTime(object dateValue)
        {
            if (dateValue == DBNull.Value)
                return DateTime.Now;
            else
                return (DateTime)dateValue;
        }
        /// <summary>
        /// Date Created:   31/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Send Email to Vendor without attachment
        /// </summary>
        private void EmailNoAttachment()
        {
            DataTable dt = null;
            try
            {

                DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);

                dt = HotelManifestBLL.GetListHotelManifest_Locked(dDate, uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["strPendingFilter"]),
                     GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
                     GlobalCode.Field2String(Session["City"]),
                     uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                      GlobalCode.Field2String(Session["Port"]), uoDropDownListBranch.SelectedValue, GlobalCode.Field2String(Session["Vehicle"]),
                     uoDropDownListVessel.SelectedValue,
                     uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                     uoDropDownListRank.SelectedValue,
                     uoHiddenFieldRole.Value, uoDropDownListHours.SelectedValue
                    );
                if (dt.Rows.Count > 0)
                {
                    string sMsg = "Please find attached " + uoDropDownListHours.SelectedItem.Text + " for " + dDate.ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";
                    if (uoCheckBoxDiff.Checked == true)
                    {
                        sMsg = "Please find attached difference of " + uoDropDownListHours.SelectedItem.Text + " and " + uoDropDownListCompare.SelectedItem.Text + " for " + dDate.ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";
                    }
                    CommonFunctions.SendEmail("", dt.Rows[0]["HotelEmail"].ToString(), "Travelmart: Hotel Manifest", sMsg);
                    AlertMessage("Email Sent.");
                }
                else
                {
                    AlertMessage("Vendor have no email address!");
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
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create Hotel Manifest based from selected hour
        /// </summary>
        private void CreateExcelManifest() 
        { 
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                DateTime dDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldDate.Value);

                dt = HotelManifestBLL.GetListHotelManifest_Locked(dDate, uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["strPendingFilter"]),
                    GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]),
                    uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                     GlobalCode.Field2String(Session["Port"]), uoDropDownListBranch.SelectedValue, GlobalCode.Field2String(Session["Vehicle"]),
                    uoDropDownListVessel.SelectedValue,
                    uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                    uoDropDownListRank.SelectedValue,
                    uoHiddenFieldRole.Value, uoDropDownListHours.SelectedValue
                   );

                // Create XMLWriter
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDateOnly = DateTime.Now.ToString("dd-MMM-yyyy");
                //string sLogicalDrive = Environment.GetEnvironmentVariable("USERPROFILE").Split(":".ToCharArray())[0];
                //sLogicalDrive += ":\\";
                string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                string FileName = FilePath + "HotelManifest_" + sDate + ".xls";
                string PDFFileName = FilePath + "HotelManifest_" + sDate + ".pdf";
                if (dt.Rows.Count > 0)
                {
                    //dt.WriteXml(FileName);
                    //dt.WriteXml(XMLFileName);   
                                        
                    if (uoCheckBoxPDF.Checked)
                    {
                        //CreateExcel(dt, FileName);
                        //ConvertExcelToPDF(FilePath, FileName, sDate);
                        //ConvertToPDF(FilePath, sDate, PDFFileName, dt);
                        //ConvertToPDF2(FilePath, FileName, sDate, PDFFileName, dt);
                        //ConvertToPDF3(FilePath, FileName, sDate, PDFFileName, dt);
                        CreatePDF(sDateOnly, PDFFileName, dt);
                        //ConvertExcelToPDF(FilePath, sDate, PDFFileName, dt);
                        string sMsg = "Please find attached " + uoDropDownListHours.SelectedItem.Text + " for " + dDate.ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";
                        ManifestSendEmail("Travelmart: Hotel Manifest " + dt.Rows[0]["HotelBranch"].ToString(), sMsg, PDFFileName, dt.Rows[0]["HotelEmail"].ToString(), PDFFileName, dt.Rows[0]["HotelBranch"].ToString());
                    }
                    else
                    {
                        CreateExcel(dt, FileName);
                        string sMsg = "Please find attached " + uoDropDownListHours.SelectedItem.Text + " for " + dDate.ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";
                        ManifestSendEmail("Travelmart: Hotel Manifest " + dt.Rows[0]["HotelBranch"].ToString(), sMsg, FileName, dt.Rows[0]["HotelEmail"].ToString(), FileName, dt.Rows[0]["HotelBranch"].ToString());
                    }                    
                }                
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();                    
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create one Excel-XML-Document with SpreadsheetML from a DataTable
        /// </summary>
        /// <param name="dataSource">Datable which would be exported in Excel</param>
        /// <param name="fileName">Name of exported file</param>
        public static void CreateExcel(DataTable dtSource, string strFileName)
        {          
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 34;
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


                // </Styles>
                xtwWriter.WriteEndElement();

                // <Worksheet ss:Name="xxx">
                xtwWriter.WriteStartElement("Worksheet");
                xtwWriter.WriteAttributeString("ss", "Name", null, dtSource.Rows[0]["HotelBranch"].ToString());

                // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                xtwWriter.WriteStartElement("Table");
               
                int iRow = dtSource.Rows.Count + 15;

                xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");
                
                //Variables for computation
                string Gender;
                string RoomType;

                double Total = 0;
                double TotalMale = 0;
                double TotalFemale = 0;
                double TotalMaleSingle = 0;
                double TotalMaleDouble = 0;
                double TotalFemaleSingle = 0;
                double TotalFemaleDouble = 0;

                Int32 MaleSingle = 0;
                Int32 MaleDouble = 0;
                Int32 FemaleSingle = 0;
                Int32 FemaleDouble = 0;

                Int32 MaleSDTotal = 0;
                Int32 FemaleSDTotal = 0;
                
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
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            // Write content of cell
                            xtwWriter.WriteValue(cellValue);
                            //if (cellValue.ToString() != "")
                            //{
                            //    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            //    // Write content of cell
                            //    xtwWriter.WriteValue(cellValue);
                            //}
                            //else 
                            //{
                            //    xtwWriter.WriteAttributeString("Type", "");
                            //    // Write content of cell
                            //    xtwWriter.WriteValue("");
                            //}

                            // </Data>
                            xtwWriter.WriteEndElement();

                            // </Cell>
                            xtwWriter.WriteEndElement();
                        }
                        i++;
                    }
                    // </Row>
                    xtwWriter.WriteEndElement();

                    //Computation
                    Gender = row["Gender"].ToString();
                    RoomType = row["Single/Double"].ToString();
                    // Gender count					
                    if (Gender == "Male")
                    {
                        if (RoomType == "Single")
                        {
                            TotalMaleSingle = TotalMaleSingle + .5;
                            MaleSingle = MaleSingle + 1;
                        }
                        else
                        {
                            TotalMaleDouble = TotalMaleDouble + .5;
                            MaleDouble = MaleDouble + 1;
                        }
                        TotalMale = TotalMaleSingle + TotalMaleDouble;
                    }
                    else
                    {
                        if (RoomType == "Single")
                        {
                            TotalFemaleSingle = TotalFemaleSingle + .5;
                            FemaleSingle = FemaleSingle + 1;
                        }
                        else
                        {
                            TotalFemaleDouble = TotalFemaleDouble + .5;
                            FemaleDouble = FemaleDouble + 1;
                        }
                        TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                    }
                    Total = Total + .5;
                }
                //Total = TotalMale + TotalFemale;					
                MaleSDTotal = MaleSingle + MaleDouble;
                FemaleSDTotal = FemaleSingle + FemaleDouble;


                //TotalSummary

                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");                    
                        xtwWriter.WriteStartElement("Data");
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");                    
                            xtwWriter.WriteValue("");
                        xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();


                //Row for Male Room:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                    
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Room:");
                    }
                    else if (x == 12)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");                        
                        xtwWriter.WriteValue(TotalMale.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Male Room:
                xtwWriter.WriteEndElement();


                //Row for Female Room:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                    
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Room:");
                    }
                    else if (x == 12)
                    {                        
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                        xtwWriter.WriteValue(TotalFemale.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Room:
                xtwWriter.WriteEndElement();


                //Row for Total Room:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                  
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Total Room:");
                    }
                    else if (x == 12)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                       
                        xtwWriter.WriteValue(Total.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Total Room:
                xtwWriter.WriteEndElement();


                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    xtwWriter.WriteValue("");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();


                //Male Single:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                   
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Single:");
                    }
                    else if (x == 12)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                        
                        xtwWriter.WriteValue(MaleSingle.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //Male Single:
                xtwWriter.WriteEndElement();

                //Male Double:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                   
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Double:");
                    }
                    else if (x == 12)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                        
                        xtwWriter.WriteValue(MaleDouble.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Male Double:
                xtwWriter.WriteEndElement();


                //Male Single/Double Total:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                    
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Single/Double Total:");
                    }
                    else if (x == 12)
                    {                        
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue(MaleSDTotal.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Male Single/Double Total:
                xtwWriter.WriteEndElement();


                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    xtwWriter.WriteValue("");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();


                //Female Single:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                   
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Single:");
                    }
                    else if (x == 12)
                    {                        
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue(FemaleSingle.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Single:
                xtwWriter.WriteEndElement();


                //Female Double:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                    
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Double:");
                    }
                    else if (x == 12)
                    {                        
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue(FemaleDouble.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Double:
                xtwWriter.WriteEndElement();


                //Female Single/Double Total:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {                   
                    if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Single/Double Total:");
                    }
                    else if (x == 12)
                    {                        
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue(FemaleSDTotal.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Single/Double Total:
                xtwWriter.WriteEndElement();


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
        /// <summary>        
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create PDF file based from Datatable      
        /// </summary>
        /// <param name="sDateOnly">Date with correct format to be included in PDF file</param>
        /// <param name="PDFFileName">PDF filename to be used</param>
        /// <param name="dt">Datatable to convert</param>
        private void CreatePDF(string sDateOnly, string PDFFileName, DataTable dt)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + PDFFileName);

            // step 1: creation of a document-object
            PDF.text.Document document = new PDF.text.Document(PDF.text.PageSize.LEGAL.Rotate(), 5, 5, 50, 10);
            //PDF.text.Document document = new PDF.text.Document(PDF.text.PageSize.A2.Rotate(), 5, 5, 50, 10);

            // step 2: we create a writer that listens to the document
            //PDF.text.pdf.PdfWriter writer = PDF.text.pdf.PdfWriter.GetInstance(document, Response.OutputStream);
            PDF.text.pdf.PdfWriter.GetInstance(document, new FileStream(PDFFileName, FileMode.Create));
            //set some header stuff
            //document.AddTitle(name);
            //document.AddSubject("Table of " + name);
            //document.AddCreator("This Application");
            //document.AddAuthor("Me");
            
            // we Add a Header that will show up on PAGE 1
            //Phrase phr = new Phrase(""); //empty phrase for page numbering
            //HeaderFooter footer = new HeaderFooter(phr, true);
            //document.Footer = footer;
            
            // step 3: we open the document
            document.Open();

            // step 4: we add content to the document
            CreatePages(document, dt, sDateOnly);
            
            // step 5: we close the document
            document.Close();
        }
        /// <summary>
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create PDF pages
        /// </summary>
        /// <param name="document"></param>
        /// <param name="dt"></param>
        /// <param name="sDateOnly"></param>
        public void CreatePages(PDF.text.Document document, DataTable dt, string sDateOnly)
        {            
            if (dt.Rows.Count > 0)
            {               
                bool first = true;
                if (first)
                    first = false;
                else
                    document.NewPage();
                
                int iColCount = 34;

                document.Add(FormatHeaderPhrase(dt.Rows[0]["HotelBranch"].ToString() + ": " + uoDropDownListHours.SelectedItem.Text + " (" + sDateOnly + ")"));        
                iTextSharp.text.pdf.PdfPTable pdfTable = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count - 2);
                pdfTable.DefaultCell.Padding = 1;
                pdfTable.WidthPercentage = 100; // percentage
                pdfTable.DefaultCell.BorderWidth = 1;
                //pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_CENTER;
                pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_LEFT;

                int iCol = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    if (iCol <= 34)
                    {
                        pdfTable.AddCell(FormatHeaderTable(column.ColumnName));
                    }
                    iCol++;
                }

                pdfTable.HeaderRows = 1;  // this is the end of the table header
                pdfTable.DefaultCell.BorderWidth  = 1;

                PDF.text.BaseColor altRow = new PDF.text.BaseColor(242, 242, 242);
                
                string Gender;
                string RoomType;

                double Total = 0;
                double TotalMale = 0;
                double TotalFemale = 0;
                double TotalMaleSingle = 0;
                double TotalMaleDouble = 0;
                double TotalFemaleSingle = 0;
                double TotalFemaleDouble = 0;

                Int32 MaleSingle = 0;
                Int32 MaleDouble = 0;
                Int32 FemaleSingle = 0;
                Int32 FemaleDouble = 0;

                Int32 MaleSDTotal = 0;
                Int32 FemaleSDTotal = 0;
	
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

                        Gender = dt.Rows[iRow]["Gender"].ToString();
                        RoomType = dt.Rows[iRow]["Single/Double"].ToString();
                        // Gender count					
                        if (Gender == "Male")
                        {
                            if (RoomType == "Single")
                            {
                                TotalMaleSingle = TotalMaleSingle + .5;
                                MaleSingle = MaleSingle + 1;
                            }
                            else
                            {
                                TotalMaleDouble = TotalMaleDouble + .5;
                                MaleDouble = MaleDouble + 1;
                            }
                            TotalMale = TotalMaleSingle + TotalMaleDouble;
                        }
                        else
                        {
                            if (RoomType == "Single")
                            {
                                TotalFemaleSingle = TotalFemaleSingle + .5;
                                FemaleSingle = FemaleSingle + 1;
                            }
                            else
                            {
                                TotalFemaleDouble = TotalFemaleDouble + .5;
                                FemaleDouble = FemaleDouble + 1;
                            }
                            TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                        }
                        Total = Total + .5;			                    
                    iRow++;                  
                }
                 //Total = TotalMale + TotalFemale;					
                MaleSDTotal = MaleSingle + MaleDouble;
                FemaleSDTotal = FemaleSingle + FemaleDouble;		
                
                //Border and background settings
                pdfTable.DefaultCell.Border = 0;
                pdfTable.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                pdfTable.DefaultCell.BorderColor = PDF.text.BaseColor.WHITE;
                //Blank Row
                iTextSharp.text.pdf.PdfPCell BlankRow = new  iTextSharp.text.pdf.PdfPCell(FormatPhrase(""));
                BlankRow.Colspan = iColCount;
                BlankRow.Border = 0;
                pdfTable.AddCell(BlankRow);
                                
                //Male Room                               
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Room:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;                        
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //lblSummary.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        //pdfTable.AddCell(FormatPhrase(TotalMale.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(TotalMale.ToString("0.#")));                        
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }                    
                }
               
                //Female Room:               
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Room:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(TotalFemale.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(TotalFemale.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
               
                //Total Room:               
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Total Room:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(Total.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(Total.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                
                pdfTable.AddCell(BlankRow);

                //Male Single:               
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Single:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(MaleSingle.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(MaleSingle.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Male Double:               
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {                        
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Double:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(MaleDouble.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(MaleDouble.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Male Single/Double Total:
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Single/Double Total:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(MaleSDTotal.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(MaleSDTotal.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                pdfTable.AddCell(BlankRow);
                //Female Single:
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Single:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(FemaleSingle.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(FemaleSingle.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Female Double:
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Double:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(FemaleDouble.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(FemaleDouble.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Female Single/Double Total:
                for (int x = 1; x <= 32; x++)
                {
                    if (x == 11)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Single/Double Total:"));
                        lblSummary.Colspan = 3;
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 14)
                    {
                        //pdfTable.AddCell(FormatPhrase(FemaleSDTotal.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(FemaleSDTotal.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                document.Add(pdfTable);
            }
        }        
       /// <summary>
       /// Date Created:   01/02/2012
       /// Created By:     Josephine Gad
        /// (description)  Format the phrase. Apply font and size here.
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
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6, PDF.text.Font.BOLD, new PDF.text.BaseColor(0,0,255)));
        }
        /// <summary>        
        /// Date Created:   06/02/2011
        /// Created By:     Josephine Gad
        /// (description)   Set the Calendar Dashboard count        
        /// </summary>
        /// <returns></returns>
        private DataTable GetCalendarTable()
        {
            DataTable dt = null;
            //IDataReader dr = null;
            try
            {
                if (Cache["TentativeManifestCalendarDashboard"] == null)
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
                        sCountry =  GlobalCode.Field2String(Session["Country"]);
                    }
                    if (GlobalCode.Field2String(Session["City"]) != null && GlobalCode.Field2String(Session["City"]) != "")
                    {
                        sCity = GlobalCode.Field2String(Session["City"]);
                    }
                    if (GlobalCode.Field2String(Session["Port"]) != null && GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        sPort = GlobalCode.Field2String(Session["Port"]);
                    }
                    if (GlobalCode.Field2String(Session["Hotel"]) != null && GlobalCode.Field2String(Session["Hotel"]) != "")
                    {
                        sHotel = GlobalCode.Field2String(Session["Hotel"]);
                    }
                    if (Session["Vehicle"] != null && Session["Vehicle"] != "")
                    {
                        sVehicle = GlobalCode.Field2String(Session["Vehicle"]);
                    }
                    if (GlobalCode.Field2String(Session["ManifestHrs"]) != null && GlobalCode.Field2String(Session["ManifestHrs"]) != "")
                    {
                        sManifestHrs = GlobalCode.Field2String(Session["ManifestHrs"]);
                    }

                    dt = HotelManifestBLL.GetLockedManifestDashboard(day, GlobalCode.Field2String(Session["UserName"]), sPendingFilter,
                     sRegion, sCountry,
                     sCity, "", "1", "", sPort, sHotel, sVehicle, "0", "0", "0", "0",
                     uoHiddenFieldRole.Value, sManifestHrs);

                    Cache.Remove("TentativeManifestCalendarDashboard");
                    //Store TentativeManifestCalendarDashboard dr in Cache for 5 minutes
                    Cache.Insert("TentativeManifestCalendarDashboard", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                    return dt;
                }
                else
                {
                    dt = (DataTable)Cache["TentativeManifestCalendarDashboard"];
                    return dt;
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
                //if (dr != null)
                //{
                //    dr.Close();
                //    dr.Dispose();
                //}
            }
        }
        #endregion
    }
}

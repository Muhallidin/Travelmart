using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using PDF = iTextSharp;
using System.IO;
using System.Text;

namespace TRAVELMART.Hotel
{
    public partial class HotelManifest2 : System.Web.UI.Page
    {
        LockedManifestBLL lockedBLL = new LockedManifestBLL();
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// ------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
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
                Session["strPrevPage"] = Request.RawUrl;
                LoadValues();
                uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                if ((Session["Hotel"] != null && GlobalCode.Field2String(Session["Hotel"]) != ""
                    && GlobalCode.Field2String(Session["Hotel"]) != "0") && (Session["ManifestHrs"] != null && GlobalCode.Field2String(Session["ManifestHrs"]) != ""
                    && GlobalCode.Field2String(Session["ManifestHrs"]) != "0"))
                {
                    SetDefaultValues();
                    object sender1 = uoBtnView;
                    EventArgs e1 = new EventArgs();
                    uoBtnView_Click(sender1, e1);                                        
                }               
            }
           
        }
        
        protected void uoBtnView_Click(object sender, EventArgs e)
        {
            Session.Remove("TentativeManifestCalendarDashboard");
            
            SetLockedManifestDetails();
            //SetCalendarDashboard();
        }

        protected void uoBtnCompare_Click(object sender, EventArgs e)
        {
            //SetManifestDifferenceDetails();
        }

        protected void uoBtnSearch_Click(object sender, EventArgs e)
        {
            SearchLockedManifest();
        }
        
        protected void uoBtnSendEmail_Click(object sender, EventArgs e)
        {
            getEmail();
            //CreateEmail();
        }
        #endregion

        #region METHODS

        #region Values
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load default values
        /// </summary>
        protected void LoadValues()
        {
            lockedBLL.LoadLockedManifestPage(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2DateTime(Session["DateFrom"]),
                GlobalCode.Field2Int(Session["Region"]));
            //LoadBranch();
            //LoadManifestType();
            //LoadNationality();
            //LoadVessel();
            //LoadRank();
            SetDefaultValues();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load hotel branch
        /// </summary>
        //protected void LoadBranch()
        //{
        //    ListItem item = new ListItem("--Select Hotel Branch--", "0");
        //    uoDropDownListBranch.Items.Add(item);
        //    uoDropDownListBranch.DataSource = LockedManifestClass.UserBranch;
        //    uoDropDownListBranch.DataTextField = "BranchName";
        //    uoDropDownListBranch.DataValueField = "BranchID";
        //    uoDropDownListBranch.DataBind();
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest type
        /// </summary>
        //protected void LoadManifestType()
        //{ 
        //    ListItem item = new ListItem("--Select Manifest Type--", "0");
        //    uoDropDownListManifest.Items.Add(item);
        //    uoDropDownListManifest.DataSource = LockedManifestClass.ManifestType;
        //    uoDropDownListManifest.DataTextField = "ManifestName";
        //    uoDropDownListManifest.DataValueField = "ManifestType";
        //    uoDropDownListManifest.DataBind();
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load vessel type
        /// </summary>
        //protected void LoadVessel()
        //{ 
        //    ListItem item = new ListItem("--Select Ship--", "0");
        //    uoDropDownListVessel.Items.Add(item);
        //    uoDropDownListVessel.DataSource = LockedManifestClass.Vessel;
        //    uoDropDownListVessel.DataTextField = "VesselName";
        //    uoDropDownListVessel.DataValueField =  "VesselId";
        //    uoDropDownListVessel.DataBind();
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load Nationality
        /// </summary>
        //protected void LoadNationality()
        //{ 
        //    ListItem item = new ListItem("--Select Nationality--", "");
        //    uoDropDownListNationality.Items.Add(item);
        //    uoDropDownListNationality.DataSource = LockedManifestClass.SFNationality;
        //    uoDropDownListNationality.DataTextField = "Nationality";
        //    uoDropDownListNationality.DataValueField = "Nationality";
        //    uoDropDownListNationality.DataBind();
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load Rank
        /// </summary>
        //protected void LoadRank()
        //{ 
        //    ListItem item = new ListItem("--Select Rank--", "");
        //    uoDropDownListRank.Items.Add(item);
        //    uoDropDownListRank.DataSource = LockedManifestClass.SFRank;
        //    uoDropDownListRank.DataTextField = "RankName";
        //    uoDropDownListRank.DataValueField = "RankName";
        //    uoDropDownListRank.DataBind();
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: set default values
        /// </summary>
        protected void SetDefaultValues()
        {
            if (Session["Hotel"] != null && GlobalCode.Field2String(Session["Hotel"]) != ""
                    && GlobalCode.Field2String(Session["Hotel"]) != "0")
            {
                uoDropDownListGender.SelectedIndex = 0;
                uoDropDownListNationality.SelectedIndex = 0;
                uoDropDownListRank.SelectedIndex = 0;
                uoDropDownListStatus.SelectedIndex = 0;
                uoDropDownListVessel.SelectedIndex = 0;
                uoDropDownListBranch.SelectedValue = GlobalCode.Field2Int(Session["Hotel"]).ToString();
                uoDropDownListManifest.SelectedValue = GlobalCode.Field2Int(Session["ManifestHrs"]).ToString();
                uoHiddenBranch.Value = GlobalCode.Field2Int(Session["Hotel"]).ToString();
                uoHiddenFieldManifest.Value = GlobalCode.Field2Int(Session["ManifestHrs"]).ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString();
                uoChkBoxPDF.Enabled = true;
                EventArgs e = new EventArgs();
                uoBtnView_Click(uoBtnView, e);
            }
            else
            {
                uoDropDownListGender.SelectedIndex = 0;
                uoDropDownListNationality.SelectedIndex = 0;
                uoDropDownListRank.SelectedIndex = 0;
                uoDropDownListStatus.SelectedIndex = 0;
                uoDropDownListVessel.SelectedIndex = 0;
                uoChkBoxPDF.Enabled = false;
                //uoHiddenBranch.Value = "0";
                //uoHiddenFieldManifest.Value = "0";
            }
            
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest type
        /// </summary>
        //protected void LoadCompareType()
        //{
        //    ListItem item = new ListItem("--Select Manifest Type--", "-1");

        //    var CompareManifest = (from a in LockedManifestClass.ManifestType
        //                           where a.ManifestType != Int32.Parse(uoDropDownListManifest.SelectedValue)
        //                           select new
        //                           {
        //                               CompareId = a.ManifestType,
        //                               CompareName = a.ManifestName,
        //                           }).ToList();

        //    uoDropDownListCompare.Items.Clear();
        //    uoDropDownListCompare.Items.Add(item);
        //    item = new ListItem("Current Transactions", "0");
        //    uoDropDownListCompare.Items.Add(item);
        //    uoDropDownListCompare.DataSource = CompareManifest;
        //    uoDropDownListCompare.DataTextField = "CompareName";
        //    uoDropDownListCompare.DataValueField = "CompareId";
        //    uoDropDownListCompare.DataBind();
        //}

        #endregion

        #region Locked
        

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest details
        /// </summary>
        protected void SetLockedManifestDetails()
        {
            uoDropDownListCompare.Visible = true;
            uoBtnCompare.Visible = true;
            uoLblCompare.Visible = true;

            uoLblManifestHeader.Text = uoDropDownListManifest.SelectedItem.Text + " for "
                + String.Format("{0:dd-MMM-yyyy}", GlobalCode.Field2DateTime(Session["DateFrom"]));

            //LoadCompareType();

            string sfName ="";
            int sfId = 0;
            if (uoDropDownListFilter.SelectedValue == "0")
            {
                sfName = uoTextBoxFilter.Text;
            }
            else
            { 
                if(uoTextBoxFilter.Text !="")
                {
                    sfId = GlobalCode.Field2Int(uoTextBoxFilter.Text);
                }
            }



            //lockedBLL.LoadLockedManifest(uoHiddenFieldUser.Value, 0, DateTime.Parse(uoHiddenFieldDate.Value), Int32.Parse(uoHiddenFieldManifest.Value),
            //    Int32.Parse(uoHiddenBranch.Value), 0, "", "",
            //    "", 0, "", "",
            //    0, 25);

            //ObjectDataSource1.TypeName = "TRAVELMART.Common.LockedManifestClass";
            //ObjectDataSource1.SelectCountMethod = "GetLockedManifestCount";
            //ObjectDataSource1.SelectMethod = "GetLockedManifest";
            //uoListLockedManifest.DataSourceID = ObjectDataSource1.UniqueID;
            //Session["Hotel"] = uoHiddenBranch.Value;
            //Session["ManifestHrs"] = uoHiddenFieldManifest.Value;

            //if (LockedManifestClass.LockedManifestCount > 0)
            //{
            //    uoChkBoxPDF.Enabled = true;
            //    uoBtnSendEmail.Enabled = true;
            //    uoChkBoxSendDiff.Enabled = false;
            //}
            //else
            //{
            //    uoChkBoxPDF.Enabled = false;
            //    uoBtnSendEmail.Enabled = false;
            //    uoChkBoxSendDiff.Enabled = false;
            //}
        }       

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 02/23/2012
        /// (description)   Set the Calendar visibility if there is selected hotel branch
        /// </summary>
        private void SetCalendarDashboard()
        {
            Calendar uoCalendarDashboard = (Calendar)Master.FindControl("uoCalendarDashboard");
            HtmlControl uoTableDashboard = (HtmlControl)Master.FindControl("uoTableDashboard");
            HtmlControl uoTableLeftMenu = (HtmlControl)Master.FindControl("uoTableLeftMenu");
            if (uoDropDownListBranch.SelectedValue == "0" || uoDropDownListManifest.SelectedValue =="0")
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

                
                GetCalendarTable();
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 02/23/2012
        /// (description)   set calendar table
        /// -----------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  19/03/2012
        /// (description)   Use session instead of cache
        /// </summary>
        protected void GetCalendarTable()
        {
            try
            {
                DateTime day = GlobalCode.Field2DateTime(Request.QueryString["dt"].ToString());
                string sHotel = GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue).ToString(); 
                string sManifestHrs = GlobalCode.Field2Int(uoDropDownListManifest.SelectedValue).ToString();

                List<ManifestCalendar> calList = new List<ManifestCalendar>();

                LockedManifestBLL lockedBLL = new LockedManifestBLL();
                calList = lockedBLL.LoadLockedManifestCalendar(GlobalCode.Field2String(Session["UserName"]), day, Int32.Parse(sManifestHrs),
                    Int32.Parse(sHotel));
                Session["TentativeManifestCalendarDashboard"] = calList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            //try
            //{
            //    if (LockedManifestClass.ManifestCalendar != null)
            //    {
            //        DataTable dt = new DataTable();

            //        // Add columns.
            //        dt.Columns.Add("colDate", typeof(DateTime));
            //        dt.Columns.Add("TotalCount", typeof(int));

            //        // Add rows.
            //        foreach (var array in LockedManifestClass.ManifestCalendar)
            //        {
            //            dt.Rows.Add(array.colDate, array.TotalCount);
            //        }


            //        if (Cache["TentativeManifestCalendarDashboard"] == null)
            //        {
            //            Cache.Remove("TentativeManifestCalendarDashboard");
            //            Cache.Insert("TentativeManifestCalendarDashboard", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
            //        }
            //    }            
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    
        
        #endregion

        #region Difference
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 24/02/2012
        /// Description: set manifest difference details
        /// </summary>
        //protected void SetManifestDifferenceDetails()
        //{
        //    LockedManifestClass.LockedManifestDifference = null;
        //    LockedManifestClass.LockedManifestDifferenceWithRows = null;
        //    LockedManifestClass.LockedManifestDifferenceWithRowsCount = null;
            
        //    uoLblCompareHeader.Text = uoDropDownListManifest.SelectedItem.Text + " vs. " + uoDropDownListCompare.SelectedItem.Text;

        //    lockedBLL.LoadManifestDifference(uoHiddenFieldUser.Value, 0, DateTime.Parse(uoHiddenFieldDate.Value),
        //        Int32.Parse(uoDropDownListManifest.SelectedValue), Int32.Parse(uoDropDownListCompare.SelectedValue),
        //        Int32.Parse(uoDropDownListBranch.SelectedValue));

        //    uoDifferenceList.DataSourceID = ObjectDataSource2.UniqueID;
        //    if (LockedManifestClass.LockedManifestDifferenceWithRowsCount > 0)
        //    {
                
        //        uoChkBoxSendDiff.Enabled = true;
                
        //    }
        //    else
        //    { 
                
        //        uoChkBoxSendDiff.Enabled = false;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest difference list
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        //public List<LockedManifestDifference> GetDifferenceClass(int startRowIndex, int maximumRows)
        //{

        //    int lastRow = startRowIndex + maximumRows;
        //    var locked = (from a in LockedManifestClass.LockedManifestDifferenceWithRows
        //                  where a.RowNum >= startRowIndex + 1 &&
        //                  a.RowNum <= lastRow
        //                  select new LockedManifestDifference
        //                  {
        //                      IdBigInt = a.IdBigInt,
        //                      E1TravelReqId = a.E1TravelReqId,
        //                      TravelReqId = a.TravelReqId,
        //                      ManualReqId = a.ManualReqId,
        //                      Couple = a.Couple,
        //                      Gender = a.Gender,
        //                      Nationality = a.Nationality,
        //                      CostCenter = a.CostCenter,
        //                      CheckIn = a.CheckedIn,
        //                      CheckOut = a.CheckedOut,
        //                      Name = a.Name,
        //                      EmployeeId = a.EmployeeId,
        //                      Ship = a.Ship,
        //                      HotelRequest = a.HotelRequest,
        //                      SingleDouble = a.RoomType,
        //                      Title = a.Title,
        //                      HotelCity = a.HotelCity,
        //                      HotelNites = a.HotelNites,
        //                      FromCity = a.FromCity,
        //                      ToCity = a.ToCity,
        //                      AL = a.Airline,
        //                      RecordLocator = a.RecordLocator,
        //                      PassportNo = a.PassportNo,
        //                      IssuedBy = a.IssuedBy,
        //                      PassportExpiration = a.PassportExpiration,
        //                      DeptDate = a.DeptDate,
        //                      ArvlDate = a.ArvlDate,
        //                      DeptCity = a.DeptCity,
        //                      ArvlCity = a.ArvlCity,
        //                      Carrier = a.Carrier,
        //                      FlightNo = a.FlightNo,
        //                      OnOffDate = a.OnOffDate,
        //                      Voucher = a.Voucher,
        //                      TravelDate = a.TravelDate,
        //                      Reason = a.Reason,
        //                      Status = a.Status,
        //                      isDeleted = a.isDeleted,                              
        //                      QueryRemarks = a.QueryRemarks,
        //                      QueryRemarksID = a.QueryRemarksID
        //                  }).ToList();

        //    return locked;
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest difference count
        /// </summary>
        /// <returns></returns>
        //public int GetDifferenceClassCount()
        //{
        //    return (int)LockedManifestClass.LockedManifestDifferenceWithRowsCount;
        //}

        public string SetValue()
        {
            bool isDeleted = Convert.ToBoolean(Eval("isDeleted"));

            if (isDeleted)
            {
                return "text-decoration:line-through; color:Red";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region SEARCH
        protected void SearchLockedManifest()
        {
            string sfName = "";
            int sfId = 0;
            if (uoDropDownListFilter.SelectedValue == "0")
            {
                sfName = uoTextBoxFilter.Text;
            }
            else
            {
                if (uoTextBoxFilter.Text != "")
                {
                    sfId = GlobalCode.Field2Int(uoTextBoxFilter.Text);
                }
            }
            uoHiddenFieldName.Value = sfName;
            uoHiddenFieldId.Value = sfId.ToString();
            uoHiddenFieldGender.Value = uoDropDownListGender.SelectedValue;
            uoHiddenFieldNationality.Value = uoDropDownListNationality.SelectedValue;
            uoHiddenFieldRank.Value = uoDropDownListRank.SelectedValue;
            uoHiddenFieldStatus.Value = uoDropDownListStatus.SelectedValue;
            uoHiddenFieldVessel.Value = uoDropDownListVessel.SelectedValue;

            
            //ObjectDataSource1.TypeName = "TRAVELMART.Common.LockedManifestClass";
            //ObjectDataSource1.SelectCountMethod = "GetLockedManifestCount";
            //ObjectDataSource1.SelectMethod = "GetLockedManifest";
            //uoListLockedManifest.DataSourceID = ObjectDataSource1.UniqueID;
            //Session["Hotel"] = uoHiddenBranch.Value;
            //Session["ManifestHrs"] = uoHiddenFieldManifest.Value;
        }
        #endregion

        #region EMAIL
        protected void getEmail()
        { 
            int LoadType = 0;
            if(uoChkBoxSendDiff.Checked)
            {
                LoadType = 1;
            }

            //lockedBLL.LoadManifestEmailList(uoHiddenFieldUser.Value, LoadType, GlobalCode.Field2Int(uoDropDownListManifest.SelectedValue),
            //    GlobalCode.Field2Int(uoHiddenBranch.Value), GlobalCode.Field2Int(uoDropDownListCompare.SelectedValue), 
            //    GlobalCode.Field2DateTime(uoHiddenFieldDate.Value));
        }

        //protected void CreateEmail()
        //{
        //    string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
        //    string sDateOnly = DateTime.Now.ToString("dd-MMM-yyyy");
        //    //string sLogicalDrive = Environment.GetEnvironmentVariable("USERPROFILE").Split(":".ToCharArray())[0];
        //    //sLogicalDrive += ":\\";
        //    string FilePath = Server.MapPath("~/Extract/HotelManifest/");
        //    string FileName = FilePath + "HotelManifest_" + sDate + ".xls";
        //    string FileNameDiff = "";
        //    string PDFFileName = FilePath + "HotelManifest_" + sDate + ".pdf";
        //    string PDFFileNameDiff = "";
        //    DataTable locked = new DataTable();
        //    DataTable diff = new DataTable();
        //    try
        //    {

        //        locked = getDataTable(LockedManifestClass.LockedManifestEmail);
                

        //        if (uoChkBoxPDF.Checked)
        //        {
        //            CreatePDF(sDateOnly, PDFFileName, locked);
        //            if (uoChkBoxSendDiff.Checked)
        //            {
        //                PDFFileNameDiff = FilePath + "HotelManifestDifference_" + sDate + ".pdf";
        //                diff = getDataTable(LockedManifestClass.ManifestDifferenceEmail);
        //                CreatePDF(sDateOnly, PDFFileNameDiff, diff);
        //            }


        //            //ConvertExcelToPDF(FilePath, sDate, PDFFileName, dt);
        //            string sMsg = "Please find attached " + uoDropDownListBranch.SelectedItem.Text + " for " +
        //                DateTime.Parse(uoHiddenFieldDate.Value).ToString("dd-MMM-yyyy") +
        //                ".<br/><br/>Thank you.";
        //            ManifestSendEmail("Travelmart: Hotel Manifest " +
        //                uoDropDownListBranch.SelectedItem.Text, sMsg, PDFFileName, PDFFileNameDiff, LockedManifestClass.HotelEmail[0].EmailAddress,
        //                (PDFFileName + ";" + PDFFileNameDiff).TrimEnd(';'), uoDropDownListBranch.SelectedItem.Text);
        //        }
        //        else
        //        {
        //            CreateExcel(locked, FileName);

        //            if (uoChkBoxSendDiff.Checked)
        //            {
        //                FileNameDiff = FilePath + "HotelManifestDifference_" + sDate + ".xls";
        //                diff = getDataTable(LockedManifestClass.ManifestDifferenceEmail);
        //                CreateExcel(diff, FileNameDiff);
        //            }

        //            string sMsg = "Please find attached " + uoDropDownListManifest.SelectedItem.Text + " for " +
        //                DateTime.Parse(uoHiddenFieldDate.Value).ToString("dd-MMM-yyyy") + ".<br/><br/>Thank you.";

        //            ManifestSendEmail("Travelmart: Hotel Manifest " +
        //                uoDropDownListBranch.SelectedItem.Text, sMsg, FileName, FileNameDiff, LockedManifestClass.HotelEmail[0].EmailAddress,
        //                (FileName + ";" + FileNameDiff).TrimEnd(';'), uoDropDownListBranch.SelectedItem.Text);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        if (locked != null)
        //        {
        //            locked.Dispose();
        //        }
        //        if (diff != null)
        //        {
        //            diff.Dispose();
        //        }
        //    }
            
        //}

        private void CreatePDF(string sDateOnly, string PDFFileName, DataTable dt)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + PDFFileName);

            // step 1: creation of a document-object
            PDF.text.Document document = new PDF.text.Document(PDF.text.PageSize.LEGAL.Rotate(), 5, 5, 50, 10);

            // step 2: we create a writer that listens to the document
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

                document.Add(FormatHeaderPhrase(dt.Rows[0]["HotelBranch"].ToString() + ": " + uoDropDownListManifest.SelectedItem.Text + " (" + sDateOnly + ")"));
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
                pdfTable.DefaultCell.BorderWidth = 1;

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
                    RoomType = dt.Rows[iRow]["SingleDouble"].ToString();
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
                iTextSharp.text.pdf.PdfPCell BlankRow = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(""));
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
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6, PDF.text.Font.BOLD, new PDF.text.BaseColor(0, 0, 255)));
        }

        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

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

        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        private void ManifestSendEmail(string sSubject, string sMessage, string attachment1,string attachment2,
            string EmailVendor, string file, string Hotel)
        {
            string sBody;
            DataTable dt = null;
            try
            {
               
                sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                sBody += "Dear " + Hotel + ", <br/><br/> " + sMessage;
                sBody += "</TR></TD></TABLE>";

                if (EmailVendor != "")
                {
                    string attachment = attachment1 + ";" + attachment2;
                    CommonFunctions.SendEmailWithAttachment("", EmailVendor,"", sSubject, sBody, attachment.TrimEnd(';'));
                }

                //Insert Email logs
                //CommonFunctions.InsertEmailLog(EmailVendor, "travelmart.ptc@gmail.com", "", sSubject, file, DateTime.Now, uoHiddenFieldUser.Value);
                
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
                    RoomType = row["SingleDouble"].ToString();
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
        #endregion

        #endregion




    }
}

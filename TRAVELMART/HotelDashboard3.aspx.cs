using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Linq.Expressions;

namespace TRAVELMART
{
    public partial class HotelDashboard3 : System.Web.UI.Page
    {
        #region DECLARATIONS
        public string lastDate = null;
        public string lastStatus = null;
        DashboardBLL BLL = new DashboardBLL();
        #endregion
        /// <summary>
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------        
        /// Modified by:    Josephine Gad
        /// Date MOdified:  29/03/2012
        /// Description:    Add uoHiddenFieldPopupCalendar from Calendar popup 
        ///                 Delete comparison of Request.QueryString["dt"] from uoHiddenFieldDate.Value  
        ///                 Move InsertLogAuditTrail in first load of page instead of every postback
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                string sDateFrom = Request.QueryString["dt"];
                Session["DateFrom"] = sDateFrom;
            }
            else
            {
                if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
                {
                    string sDateFrom = GlobalCode.Field2String(Session["DateFrom"]);
                }
                else
                {
                    string sDateFrom = Request.QueryString["dt"];
                }
            }
            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {
                SetDefaults(0);
            }
            if (!IsPostBack)
            {
                //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                strLogDescription = "Hotel dashboard information viewed. (Date)";
                strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                uoHiddenFieldFromDefaultView.Value = "1";
            }
            if (uoHiddenFieldPopupHotel.Value == "1" || TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserRole"]))
            {
                UpdateStatusDashboard(4);
            }
            uoHiddenFieldPopupHotel.Value = "0";

            //Hide Other field for Hotel Vendor
            if (TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserRole"]))
            {
                divNotVendor.Style.Add("display", "none");
            }
        }

        protected void uoButtonApprove_Click(object sender, EventArgs e)
        {
            BookSeafarer();
        }
        //uoButtonConfirmedBookings_Click


        /// <summary>
        /// ===============================================
        /// Author: Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: export Exception Bookings to excel
        /// ===============================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonConfirmedBookings_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                //List<ConfirmBooking> list = new List<ConfirmBooking>();
                //list = (List<ConfirmBooking>)Session["HotelDashboardClass_ConfirmBooking"];
                dt = DashboardBLL.LoadHotelDashboardConfirmedExport(uoHiddenFieldUser.Value,GlobalCode.Field2Int(uoHiddenFieldBranchId.Value));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        CreateFile(dt, "ConfirmedBookingsList", uoButtonConfirmedBookings);
                    }
                    else 
                    {
                        AlertMessage("There is no record to export.");
                    }
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
            }
        }

        protected void uoLinkConfirmed_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton) sender;
            uoHiddenFieldSortBy.Value = lnkBtn.CommandArgument;
            LoadConfirmedBookings();
        }

        protected void uoLinkOverBooking_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;
            uoHiddenFieldSortBy.Value = lnkBtn.CommandArgument;
            LoadPendingBookings();
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: 
        /// ---------------------------------------------
        /// </summary>
        //private void ExportConfirmedBookings(List<ConfirmBooking> list)
        //{
        //    try
        //    {
        //        var e = (from b in list
        //                 select new
        //                 {
        //                     RecordLocator = GlobalCode.Field2String(b.RecordLocator),
        //                     E1ID = GlobalCode.Field2String(b.E1ID),
        //                     RoomType = GlobalCode.Field2String(b.RoomType),
        //                     Name = GlobalCode.Field2String(b.Name),
        //                     CheckInDate = GlobalCode.Field2String(b.CheckInDate),
        //                     CheckOutDate = GlobalCode.Field2String(b.CheckOutDate),
        //                     RankName = GlobalCode.Field2String(b.RankName),
        //                     Gender = GlobalCode.Field2String(b.Gender),
        //                     Nationality = GlobalCode.Field2String(b.Nationality),
        //                     CostCenter = GlobalCode.Field2String(b.CostCenter),
        //                     HotelNites = GlobalCode.Field2String(b.HotelNites),
        //                     HotelCity = GlobalCode.Field2String(b.HotelCity),
        //                     Airline = GlobalCode.Field2String(b.Airline),
        //                     MealAllowance = GlobalCode.Field2String(string.Format("{0:#,##0.00}", b.MealAllowance)),
        //                     //ArrivalTime = GlobalCode.Field2String(b.ArrivalTime),

        //                     //GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ArvlDate)),
        //                     ArrivalDateTime = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", b.ArrivalTime) + " " + string.Format("{0:hh:mm:ss}", b.ArrivalTime)),
        //                     FromCity = GlobalCode.Field2String(b.FromCity),
        //                     ToCity = GlobalCode.Field2String(b.ToCity),
        //                     HotelStatus = GlobalCode.Field2String(b.HotelStatus),
        //                     TravelReqId = GlobalCode.Field2String(b.TravelReqId),
        //                     ReqId = GlobalCode.Field2String(b.ReqId),
        //                     SFStatus = GlobalCode.Field2String(b.SFStatus),
        //                     BookingType = GlobalCode.Field2String(b.BookingType),
        //                     E1TravelReqIdInt = GlobalCode.Field2String(b.E1TravelReqIdInt),
        //                     //HotelRequest = GlobalCode.Field2String(b.HotelRequest),
        //                     //WithSailMaster = GlobalCode.Field2String(b.WithSailMaster)
        //                 }).ToList();


        //        DataTable exception = getDataTable(e);

        //        //foreach (DataRow dr in exception.Rows)
        //        //{
        //        //    if (dr["HotelRequest"].ToString() == "False")
        //        //        dr["HotelRequest"] = "No";
        //        //    else
        //        //        dr["HotelRequest"] = "Yes";

        //        //    if (dr["WithSailMaster"].ToString() == "False")
        //        //        dr["WithSailMaster"] = "No";
        //        //    else
        //        //        dr["WithSailMaster"] = "Yes";

        //        //    if (dr["WithSailMaster"].ToString() == "False")
        //        //        dr["WithSailMaster"] = "No";
        //        //    else
        //        //        dr["WithSailMaster"] = "Yes";

        //        //}

        //        CreateFile(exception, "ConfirmedBookingsList", uoButtonConfirmedBookings);



        //        if (exception != null)
        //        {
        //            exception.Dispose();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        /// <summary>
        /// ===============================================
        /// Author: Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: export Exception Bookings to excel
        /// ===============================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  07/Jan/2013
        /// Description:    Use table instead of list using DashboardBLL.LoadHotelDashboardOverflowExport
        /// ===============================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonExportOverflow_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                //List<OverflowBooking> list = new List<OverflowBooking>();
                //list = (List<OverflowBooking>)Session["HotelDashboardClass_PendingBooking"];

                //if (list.Count > 0)
                //{
                //    ExportOverflow(list);
                //}
                dt = DashboardBLL.LoadHotelDashboardOverflowExport(uoHiddenFieldUser.Value);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        CreateFile(dt, "OverflowList", uoButtonExportOverflow);
                    }
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
            }
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: 
        /// ---------------------------------------------
        /// </summary>
        //private void ExportOverflow(List<OverflowBooking> list)
        //{
        //    try
        //    {
        //        var e = (from a in list
        //                 select new
        //                 {
        //                    CoupleId = GlobalCode.Field2String(a.CoupleId),
        //                    Gender = GlobalCode.Field2String(a.Gender),
        //                    Nationality = GlobalCode.Field2String(a.Nationality),
        //                    CostCenter = GlobalCode.Field2String(a.CostCenter),
        //                    CheckInDate = GlobalCode.Field2String(a.CheckInDate),
        //                    CheckOutDate = GlobalCode.Field2String(a.CheckOutDate),

        //                    TravelReqId = GlobalCode.Field2String(a.TravelReqId),
        //                    SFStatus = GlobalCode.Field2String(a.SFStatus),
        //                    Name = GlobalCode.Field2String(a.Name),
        //                    SeafarerId = GlobalCode.Field2String(a.SeafarerId),
        //                    VesselName = GlobalCode.Field2String(a.VesselName),
        //                    RoomName = GlobalCode.Field2String(a.RoomName),
        //                    RankName = GlobalCode.Field2String(a.RankName),
        //                    CityId = GlobalCode.Field2String(a.CityId),
        //                    CountryId = GlobalCode.Field2String(a.CountryId),
        //                    HotelCity = GlobalCode.Field2String(a.HotelCity),
        //                    HotelNites = GlobalCode.Field2String(a.HotelNites),
        //                    FromCity = GlobalCode.Field2String(a.FromCity),
        //                    ToCity = GlobalCode.Field2String(a.ToCity),
        //                    RecordLocator = GlobalCode.Field2String(a.RecordLocator),
        //                    Carrier = GlobalCode.Field2String(a.Carrier),
        //                    DepartureDate = GlobalCode.Field2String(a.DepartureDate),
        //                    ArrivalDate = GlobalCode.Field2String(a.ArrivalDate),
        //                    FlightNo = GlobalCode.Field2String(a.FlightNo),
        //                    OnOffDate = GlobalCode.Field2String(a.OnOffDate),
        //                    Voucher = GlobalCode.Field2String(a.Voucher),
        //                    ReasonCode = GlobalCode.Field2String(a.ReasonCode),
        //                    Stripe = GlobalCode.Field2String(a.Stripe),
        //                    VendorId = GlobalCode.Field2String(a.VendorId),
        //                    BranchId = GlobalCode.Field2String(a.BranchId),
        //                    RoomTypeId = GlobalCode.Field2String(a.RoomTypeId),
        //                    //PortId = GlobalCode.Field2String(a.PortId),
        //                    //VesselId = GlobalCode.Field2String(a.VesselId),
        //                    //EnabledBit = GlobalCode.Field2String(a.EnabledBit),
        //                    //WithSailMaster = GlobalCode.Field2String(a.WithSailMaster),
        //                    //HotelRequest = GlobalCode.Field2String(a.HotelRequest)
        //                 }).ToList();

        //        DataTable exception = getDataTable(e);
        //        CreateFile(exception,"OverflowList",uoButtonExportOverflow);
        //        if (exception != null)
        //        {
        //            exception.Dispose();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: 
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
        /// Author:         Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created: 11/04/2012
        /// Description: create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt,string Excelname,object exportButton)
        {
            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/HotelManifest/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value).ToString("MMM_dd_yyy");
            string FileName = Excelname + "_" + sDateManifest + "_" + sDate + ".xls";
            strFileName = FilePath + FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            CreateExcel(dt, strFileName, uoLabelBranchName.Text);
            OpenExcelFile(FileName, strFileName, exportButton);
        }

        public static void CreateExcel(DataTable dtSource, string strFileName, string sHotel)
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
                xtwWriter.WriteAttributeString("ss", "Name", null, sHotel);

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
                            xtwWriter.WriteValue(GlobalCode.Field2String( cellValue));

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
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  12/04/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath,object ButtonExportFrom)
        {

            //Response.Redirect("~/Extract/ExceptionList/" + strFileName, false);

            Button NewButton = (Button)ButtonExportFrom;
            string strScript = "CloseModal('../Extract/HotelManifest/" + strFileName + "');";

            if (NewButton.Text.ToString() == "Export Confirmed Booking List")
                ScriptManager.RegisterStartupScript(uoButtonConfirmedBookings, this.GetType(), "CloseModal", strScript, true);
            else
                ScriptManager.RegisterStartupScript(uoButtonExportOverflow, this.GetType(), "CloseModal", strScript, true);
            
        }


        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/02/2012
        /// Description: Book Seafarer to a hotel
        /// </summary>
        protected void BookSeafarer()
        {
            string sfName = "";
            string sfId = "";
            string sfStripe = "";
            string SFStatus = "";
            string recLoc = "";
            string trId = "";
            string mReqId = "";
            foreach (ListViewItem item in uoHotelList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    HiddenField SeafarerName = (HiddenField)item.FindControl("hfName");
                    Label SeafarerId = (Label)item.FindControl("uoLblE1ID");
                    Label Stripes = (Label)item.FindControl("uoLblStripe");
                    HiddenField Status = (HiddenField)item.FindControl("hfSFStatus");
                    Label RLoc = (Label)item.FindControl("uoLblRecLoc");
                    HiddenField tReq = (HiddenField)item.FindControl("hfTRID");

                    sfName = sfName + SeafarerName.Value + "|";
                    sfId = sfId + SeafarerId.Text + "|";
                    sfStripe = sfStripe + Stripes.Text + "|";
                    SFStatus += Status.Value + '|';
                    recLoc += RLoc.Text + "|";
                    trId += tReq.Value + "|";
                    mReqId += "0" + "|";
                }
            }

            sfName = sfName.TrimEnd('|');
            sfId = sfId.TrimEnd('|');
            sfStripe = sfStripe.TrimEnd('|');
            SFStatus = SFStatus.TrimEnd('|');
            recLoc = recLoc.TrimEnd('|');
            trId = trId.TrimEnd('|');
            mReqId = mReqId.TrimEnd('|');

            string sscript = "OpenRequestEditor('" + sfId + "','" + sfName + "','"
                + sfStripe + "','" + SFStatus + "','" + recLoc + "','" + trId + "','"
                + Request.QueryString["branchId"] + "','" + mReqId + "')";

            ScriptManager.RegisterClientScriptBlock(uoButtonApprove, this.GetType(), "scr", sscript, true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenRequestEditor", sscript, true);
        }

        protected void SetDefaults(Int16 LoadType)
        {
            ViewState["InvalidRequest"] = "";
            Session["strPrevPage"] = Request.RawUrl;
            Session["ViewLeftMenu"] = "0";

            uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            uoHiddenFieldBranchId.Value = Request.QueryString["branchId"];
            uoLabelBranchName.Text = Request.QueryString["branchName"];

            BLL.LoadAllHotelDashboard2Tables2(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), GlobalCode.Field2Int(uoHiddenFieldBranchId.Value),
                LoadType, uoHiddenFieldUser.Value, 0, 20);
            if (LoadType == 0)
            {
                uoHiddenFieldFromDefaultView.Value = "0";
            }
            else
            {
                uoHiddenFieldFromDefaultView.Value = "1";
            }
            GetEvenCount();
            LoadStatusDashboard();
            LoadConfirmedBookings();

            if (TravelMartVariable.RoleHotelVendor == uoHiddenFieldUser.Value)
            {
                PendingDiv.Visible = false;
                uoPanelPending.Visible = false;
            }
            else
            {
                LoadPendingBookings();
            }
            //uoHiddenFieldFromDefaultView.Value = "1";
        }

        protected void UpdateStatusDashboard(Int16 LoadType)
        {
            ViewState["InvalidRequest"] = "";
            Session["strPrevPage"] = Request.RawUrl;
            Session["ViewLeftMenu"] = "0";

            uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            //uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            //Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");
            //uclabelStatus.Text = "Based on Checkin Date: " + DateTime.Parse(uoHiddenFieldStartDate.Value).ToString("dd-MMM-yyyy");
            uoHiddenFieldBranchId.Value = Request.QueryString["branchId"];
            uoLabelBranchName.Text = Request.QueryString["branchName"];

            BLL.LoadAllHotelDashboard2Tables2(GlobalCode.Field2DateTime(uoHiddenFieldStartDate.Value), GlobalCode.Field2Int(uoHiddenFieldBranchId.Value),
                LoadType, uoHiddenFieldUser.Value, 0, 20);

            LoadStatusDashboard();
        }

        private void ApproveRequest()
        {
            IDataReader dr = null;
            DataTable dTable = null;
            try
            {
                string strFunction = "ApproveRequest";


                string ApproveByString = GlobalCode.Field2String(Session["UserName"]);
                string InvalidTravelReq = "";

                foreach (ListViewItem item in uoHotelList.Items)
                {
                    CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                    bool IsApprovedBool = CheckSelect.Checked;

                    HiddenField uoHiddenFieldIsManual = (HiddenField)item.FindControl("uoHiddenFieldIsManual");
                    HiddenField uoPendingId = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");
                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    if (IsApprovedBool)
                    {
                        if (uoHiddenFieldIsManual.Value == "Yes")
                        {
                            dTable = HotelBLL.HotelApproveTransaction(uoPendingId.Value, GlobalCode.Field2String(Session["UserName"]));
                        }
                        else
                        {
                            String UserId = GlobalCode.Field2String(Session["UserName"]);
                            Label Duration = (Label)item.FindControl("uoLblDuration");
                            HiddenField TravelReqId = (HiddenField)item.FindControl("hfTRID");
                            HiddenField ManualReqId = (HiddenField)item.FindControl("hfMRID");
                            Label RecLoc = (Label)item.FindControl("uoLblRecLoc");
                            HiddenField VendorId = (HiddenField)item.FindControl("hfVendor");
                            //HiddenField HiddenID = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");
                            HiddenField BranchID = (HiddenField)item.FindControl("hfBranch");
                            HiddenField RoomTypeId = (HiddenField)item.FindControl("hfRoom");
                            HiddenField CheckIndate = (HiddenField)item.FindControl("hfCheckIn");
                            HiddenField CheckOutDate = (HiddenField)item.FindControl("hfCheckOut");
                            //HiddenField HotelStatus= (HiddenField)item.FindControl("hfHotelStatus");
                            HiddenField SFStatus = (HiddenField)item.FindControl("hfSFStatus");
                            HiddenField CityId = (HiddenField)item.FindControl("hfCity");
                            HiddenField CountryId = (HiddenField)item.FindControl("hfCountry");
                            HiddenField ContractId = (HiddenField)item.FindControl("hfContractID");
                            Label Voucher = (Label)item.FindControl("uoLblVoucher");
                            HiddenField DataSource = (HiddenField)item.FindControl("hfDatasource");
                            HiddenField PendingId = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");
                            HiddenField HotelName = (HiddenField)item.FindControl("hfHotelName");
                            Label E1ID = (Label)item.FindControl("uoLblE1ID");
                            HiddenField Name = (HiddenField)item.FindControl("hfName");

                            Boolean valid = true;
                            if (PendingId.Value == "0")
                            {
                                valid = AutomaticBookingBLL.ApproveBookings(GlobalCode.Field2Int(TravelReqId.Value), RecLoc.Text, GlobalCode.Field2Int(VendorId.Value),
                               GlobalCode.Field2Int(BranchID.Value), GlobalCode.Field2Int(RoomTypeId.Value), GlobalCode.Field2DateTime(CheckIndate.Value), GlobalCode.Field2DateTime(CheckOutDate.Value),
                               SFStatus.Value, GlobalCode.Field2Int(CityId.Value), GlobalCode.Field2Int(CountryId.Value), Voucher.Text, GlobalCode.Field2Int(ContractId.Value),
                               UserId, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);
                            }
                            else
                            {
                                HotelBLL.HotelApproveTransaction(PendingId.Value, UserId);
                            }

                            if (!valid)
                            {
                                InvalidTravelReq = InvalidTravelReq + TravelReqId.Value + ",";
                            }
                            else
                            {
                                //dr = VendorMaintenanceBLL.vehicleVendorBranchMaintenanceInformation(Int32.Parse(BranchID.Value));
                                //if (dr.Read())
                                //{
                                string sMsg = "Booking of seafarer <b>" + Name.Value + "</b> with E1 ID <b>" + E1ID.Text + "</b> has been approved. <br/><br/>";
                                sMsg += "Hotel Branch: " + HotelName.Value + "<br/>";
                                sMsg += "Checkin Date: " + CheckIndate.Value + "";
                                sMsg += "<br/><br/>Thank you.";
                                sMsg += "<br/><br/><i>Auto generated email.</i>";

                                //SendEmail("Travelmart: Pending Hotel Booking Approval", sMsg, CountryId.Value, BranchID.Value);
                                //}
                            }

                        }
                    }
                }
                if (InvalidTravelReq != "")
                {
                    AlertMessage("Some bookings were not approved. Please check the room allocations of the highlighed transactions.");
                    ViewState["InvalidRequest"] = InvalidTravelReq.TrimEnd(',').ToString();
                }
                else
                {
                    AlertMessage("Approved");
                }

                SetDefaults(3);
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dTable != null)
                {
                    dTable.Dispose();
                }
            }
        }

        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonApprove, this.GetType(), "scr", sScript, false);
        }
        private void SendEmail(string sSubject, string sMessage, string sCountry, string sBranchID)
        {
            string sBody;
            DataTable dt = null;
            try
            {
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email 24*7
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleCrewAssist, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + "Admin" + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel specialist of the country affected
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelSpecialist, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleHotelSpecialist + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel vendor
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelVendor, sBranchID, sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleHotelVendor + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
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
        /// Date Modified: 12/03/2012
        /// Modified By:   Gabriel Oquialda
        /// (description)  Added tooltip          
        /// -------------------------------------------
        /// Date Modified: 19/10/2012
        /// Modified By:   Josephine Gad
        /// (description)  Change  HotelDashboardClass.EventCount to Session         
        /// -------------------------------------------
        /// </summary>
        protected void GetEvenCount()
        {
            if (GlobalCode.Field2Int(Session["HotelDashboardClass_EventCount"]) > 0)
            {
                //uoLinkButtonEvents.Visible = true;
                uoLinkButtonEvents.Enabled = true;
                uoLinkButtonEvents.ToolTip = "View Event(s)";

                string scriptEventString = "return OpenEventsList('" + uoHiddenFieldBranchId.Value
                        + "', '" + 0 + "', '" + uoHiddenFieldStartDate.Value + "');";
                uoLinkButtonEvents.Attributes.Add("OnClick", scriptEventString);
            }
            else
            {
                uoLinkButtonEvents.Visible = false;
                //uoLinkButtonEvents.Enabled = false;
                //uoLinkButtonEvents.ToolTip = "No Event(s)";
            }
        }

        protected void LoadStatusDashboard(string orderby)
        {
            List<HotelRoomBlocks> list = (List<HotelRoomBlocks>)Session["HotelDashboardClass_HotelRoomBlocks"];
            uoHotelDashboardDetails.DataSource = (string.IsNullOrEmpty(orderby) ? list :
                list.AsQueryable().OrderBy(orderby).ToList());
            uoHotelDashboardDetails.DataBind();
        }

        ///
        protected void uoLinkButtonConfirmedCheckin_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            LoadStatusDashboard(lnkButton.CommandArgument);
        }

        /// <summary>
        /// Date Modified: 19/10/2012
        /// Modified By:   Josephine Gad
        /// (description)  Change  HotelDashboardClass.HotelRoomBlocks to Session         
        /// -------------------------------------------
        /// </summary>
        protected void LoadStatusDashboard()
        {
            LoadStatusDashboard("");
        }

        protected void LoadConfirmedBookings()
        {
            //ObjectDataSource1.TypeName = "TRAVELMART.Common.HotelDashboardClass";
            //ObjectDataSource1.SelectCountMethod = "GetConfirmBookingCount";
            //ObjectDataSource1.SelectMethod = "GetConfirmBooking";
            uoDashboardListDetails.DataSourceID = ObjectDataSource1.UniqueID;
            uoUpdatePanelDetails.Update();
        }

        protected void LoadPendingBookings()
        {
            //ObjectDataSource2.TypeName = "TRAVELMART.Common.HotelDashboardClass";
            //ObjectDataSource2.SelectCountMethod = "GetPendingBookingCount";
            //ObjectDataSource2.SelectMethod = "GetPendingBooking";

            uoHotelList.DataSourceID = ObjectDataSource2.UniqueID;
            //uoPanelPending.Update();
        }
        #endregion

        #region LISTVIEW GROUPINGS AND VALIDATIONS
        public string setDate()
        {
            string currentDate = Eval("Date").ToString();

            //GlobalCode.Field2String(Session["UserVendor"]) = Eval("colBrandId").ToString();
            //GlobalCode.Field2String(Session["UserBranchID"]) = Eval("colBranchId").ToString();

            if (currentDate.Length == 0)
            {
                currentDate = "";
            }

            if (lastDate != currentDate)
            {
                lastDate = currentDate;
                DateTime dt = GlobalCode.Field2DateTime(currentDate);
                return String.Format("{0:dd-MMM-yyyy (dddd)}", dt);
            }
            else
            {
                return "";
            }
        }

        //protected string SetStatus()
        //{
        //    string StatusTitle = "Status :";
        //    string currentStatus = Eval("HotelStatus").ToString();

        //    if (currentStatus.Length == 0)
        //    {
        //        currentStatus = "";
        //    }

        //    if (lastStatus != currentStatus)
        //    {
        //        lastStatus = currentStatus;
        //        return string.Format("<tr><td class=\"group\" colspan=\"18\">{0} <strong>{1}</strong></td></tr> ", StatusTitle, currentStatus);
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        protected string SetSailMaster()
        {
            bool SailType = GlobalCode.Field2Bool(Eval("WithSailMaster"));

            if (SailType == true)
            {
                return string.Format("<td class=\"SailMaster\">" + GlobalCode.Field2String(Eval("BookingType")) + "&nbsp;&nbsp; </td>");
            }
            else
            {
                return string.Format("<td class=\"NoSailMaster\">" + GlobalCode.Field2String(Eval("BookingType")) + "&nbsp;&nbsp; </td>");
            }
        }

        public string ValidateRequest()
        {
            Boolean Invalid = false;

            if (ViewState["InvalidRequest"].ToString().Contains(Eval("TravelReqId").ToString()))
            {
                Invalid = true;
            }
            else
            {
                Invalid = false;
            }

            if (Invalid)
            {
                return string.Format("background-color: #FFCC66; border: thin solid #000000; font-weight: bold");
            }
            else
            {
                return string.Format("");
            }
        }


        protected void uoListViewTR_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //if (e.CommandName == "Tag")
            //{
            //    string arg = e.CommandArgument.ToString();
            //    string[] args = arg.Split(":".ToCharArray());
            //    if (args.Count() > 1)
            //    {
            //        TagSeafarer(args[0].ToString(), args[1].ToString(), args[2].ToString());
            //    }
            //}
        }

        //private void TagSeafarer(string sIdBigint, string sTRId, string sBranch)
        //{
        //    string sUser = uoHiddenFieldUser.Value;
        //    string sRole = uoHiddenFieldRole.Value;

        //    SeafarerTravelBLL.InsertTag(sIdBigint, sTRId, sUser, sRole, "0",sBranch);
        //    if (sBranch != "0")
        //    {
        //        SetDefaults(0);
        //    }
        //}
        #endregion


    }

    static class IQueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> items, string propertyName)
        {
            var typeOfT = typeof(T);
            var parameter = Expression.Parameter(typeOfT, "parameter");
            var propertyType = typeOfT.GetProperty(propertyName).PropertyType;
            var propertyAccess = Expression.PropertyOrField(parameter, propertyName);
            var orderExpression = Expression.Lambda(propertyAccess, parameter);

            var expression = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeOfT, propertyType }, items.Expression, Expression.Quote(orderExpression));
            return items.Provider.CreateQuery<T>(expression);
        }
    }
}

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
using System.Reflection;
using System.IO;
using System.Xml;
using System.Text;

namespace TRAVELMART
{
    public partial class CalendarPopUpForecast : System.Web.UI.Page
    {
        #region DEFINITIONS
        CalendarBLL calendar = new CalendarBLL();
        #endregion
        #region EVENTS
        /// <summary>
        /// Date Created:   08/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Page for calendar of room count needed daily
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            uoHiddenFieldBranchID.Value = GlobalCode.Field2String(Request.QueryString["bID"]);
            if (!IsPostBack)
            {
                DateTime sDateFrom = new DateTime();

                if (GlobalCode.Field2String(Request.QueryString["dt"]) == "")
                {
                    sDateFrom = GlobalCode.Field2DateTime(Session["DateFrom"]);
                }
                else
                {
                    sDateFrom = GlobalCode.Field2DateTime(Request.QueryString["dt"]);
                }

                Session["DateFrom"] = sDateFrom.ToShortDateString();
                Session.Remove("CalendarRoomNeeded");
                uoCalendarDashboard.SelectedDate = sDateFrom;
                uoCalendarDashboard.VisibleDate = sDateFrom;
                
                GetCaption();
            }
            
            uoCalendarDashboard.Visible = true;

        }
        /// <summary>
        /// Date Created:   08/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Calendar settings
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoCalendarDashboard_DayRender(object sender, DayRenderEventArgs e)
        {
            //DataTable dt = null;
            List<CalendarRoomNeeded> list = new List<CalendarRoomNeeded>();

            try
            {
                DateTime day = e.Day.Date;
                
                    list = GetCalendarRoomCount();
                    //EnumerableRowCollection<DataRow> dateRows = dt.AsEnumerable();

                    //Filter Rows on collection
                    var filteredRow = (from a in list
                                       where a.colDate.Equals(day) //a.Field<DateTime>("colDate").Equals(day)
                                       select a).ToList();

                    //Recover the resulting datatable
                    //dtCurrentDate = filteredRow.CopyToDataTable();

                    //if (dtCurrentDate.Rows[0]["TotalCount"].ToString() != "0")
                    if (filteredRow.Count > 0)
                    {
                        if (filteredRow[0].SingleCount.ToString() != "0" || filteredRow[0].DoubleCount.ToString() != "0")
                        {
                            Label lblSingleCount = new Label();
                            Label lblDoubleCount = new Label();
                            if (filteredRow[0].SingleCount.ToString() != "0")
                            {
                                lblSingleCount.Text = "<br/>SGL:" + filteredRow[0].SingleCount.ToString();
                                lblSingleCount.ForeColor = Color.Green;
                                lblSingleCount.Font.Size = 8;
                                e.Cell.Controls.Add(lblSingleCount);
                            }
                            else
                            {
                                lblSingleCount.Text = "<br/>";
                                lblSingleCount.Font.Size = 8;
                                e.Cell.Controls.Add(lblSingleCount);
                            }
                            if (filteredRow[0].DoubleCount.ToString() != "0")
                            {
                                lblDoubleCount.Text = "</br>DBL:" + filteredRow[0].DoubleCount.ToString();
                                lblDoubleCount.ForeColor = Color.Red;
                                lblDoubleCount.Font.Size = 8;
                                e.Cell.Controls.Add(lblDoubleCount);
                            }
                            else
                            {
                                lblDoubleCount.Text = "</br>";
                                lblDoubleCount.Font.Size = 8;
                                e.Cell.Controls.Add(lblDoubleCount);
                            }
                            e.Cell.ID = day.ToString("MM_dd_yyyy");

                        }
                    }                   
                
                e.Cell.Width = 50;
                e.Cell.Height = 50;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   08/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Add  uoCalendarDashboard.VisibleDate = dtSelected to avoid error "out of index"
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoCalendarDashboard_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dtSelected = uoCalendarDashboard.SelectedDate;
            Session["DateFrom"] = dtSelected.ToShortDateString();
            uoCalendarDashboard.VisibleDate = dtSelected;
            //Session.Remove("OnOffCalendarDashboard");
            OpenParentPage();
        }

        protected void uoCalendarDashboard_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            Session["DateFrom"] = e.NewDate.ToString("MM/dd/yyyy");
            Session.Remove("CalendarRoomNeeded");
            GetCalendarRoomCount();
        }
        protected void uoLinkButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                List<CalendarRoomNeeded> list = GetCalendarRoomCount();
                if (list.Count > 0)
                {
                    dt = getDataTable(list);
                    CreateFile(dt);
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

        #region METHODS
        /// <summary>
        /// Date Created:   08/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Calendar Room Count using session
        /// -------------------------------------------
        /// </summary>
        /// <returns></returns>
        protected List<CalendarRoomNeeded> GetCalendarRoomCount()
        {
            List<CalendarRoomNeeded> list = new List<CalendarRoomNeeded>();
            
            try
            {
                if (Session["CalendarRoomNeeded"] == null)
                {
                    list = calendar.GetCalendarRoomNeeded(GlobalCode.Field2String(Session["UserName"]), 
                        GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])), "",
                        GlobalCode.Field2Int(Session["Region"]), GlobalCode.Field2Int(Session["Port"]), GlobalCode.Field2Int(uoHiddenFieldBranchID.Value) 
                        );
                    Session.Remove("CalendarRoomNeeded");
                    Session["CalendarRoomNeeded"] = list;
                    return list;
                }
                else
                {
                    list = (List<CalendarRoomNeeded>)Session["CalendarRoomNeeded"];
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }                   
        private void OpenParentPage()
        {

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_uoHiddenFieldPopupCalendar\").val(\"1\"); ";
            sScript += " window.parent.RefreshPageFromPopup(); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created:   08/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Show the Branch name, Port & Region of filters used
        /// -------------------------------------------
        /// </summary>
        private void GetCaption()
        { 
            string sCaption = "";
            string sBranch;
            string sPort;
            string sRegion;

            sBranch = GlobalCode.Field2String(Request.QueryString["b"]);
            sPort = GlobalCode.Field2String(Request.QueryString["p"]);
            sRegion = GlobalCode.Field2String(Request.QueryString["r"]);

            sBranch = (sBranch.ToLower().Contains("select") == true ? "" : sBranch);
            sPort = (sPort.ToLower().Contains("select") == true ? "" : sPort);
            sRegion = (sRegion.ToLower().Contains("select") == true ? "" : sRegion);

            if (sBranch != "")
            {
                sCaption = sBranch;
            }
            if (sPort != "")
            {
                sCaption += (sCaption == "" ? "" : " | ") + sPort;
            }
            if (sRegion != "")
            {
                sCaption += (sCaption == "" ? "" : " | ") + sRegion;
            }

            uoLabelCaption.Text = sCaption;
        }
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
      
        protected void CreateFile(DataTable dt)
        {
            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/Calendar/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string FileName = "RoomCount_" + sDate + ".xls";
            strFileName = FilePath + FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            CreateExcel(dt, strFileName, uoLabelCaption.Text);
            OpenExcelFile(FileName, strFileName);
        }
        /// Date Modifed:   04/Feb/2013
        /// Modifed By:     Josephine Gad
        /// (description)   Add fields override count, emergency and contracted room counts       
        /// -------------------------------------------
        public static void CreateExcel(DataTable dtSource, string strFileName, string sCaption)
        {
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 7;
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
                xtwWriter.WriteAttributeString("ss", "Name", null, "Room Needed");

                // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                xtwWriter.WriteStartElement("Table");

                int iRow = dtSource.Rows.Count + 15;

                xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "100");

                //Title caption
                xtwWriter.WriteStartElement("Row");
                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
               

                xtwWriter.WriteStartElement("Cell");
                //xtwWriter.WriteAttributeString("ss", "MergeAcross", null, "2");
                //xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                // xxx
                xtwWriter.WriteStartElement("Data");
                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                // Write content of cell
                xtwWriter.WriteValue(sCaption);
                
                xtwWriter.WriteEndElement();
                xtwWriter.WriteEndElement();

                xtwWriter.WriteEndElement();




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
                        if (Header.ColumnName == "sDate")
                        {
                            xtwWriter.WriteValue("Date");
                        }
                        else if (Header.ColumnName == "SingleCount")
                        {
                            xtwWriter.WriteValue("SGL Needed");
                        }
                        else if (Header.ColumnName == "DoubleCount")
                        {
                            xtwWriter.WriteValue("DBL Needed");
                        }
                        else if (Header.ColumnName == "TotalNeededRoom")
                        {
                            xtwWriter.WriteValue("Total Room Needed");
                        }
                        //else if (Header.ColumnName == "ContractRoomSingle")
                        //{
                        //    xtwWriter.WriteValue("SGL Contracted");
                        //}
                        //else if (Header.ColumnName == "ContractRoomDouble")
                        //{
                        //    xtwWriter.WriteValue("DBL Contracted");
                        //}
                        else if (Header.ColumnName == "TotalContractRoom")
                        {
                            xtwWriter.WriteValue("Total Contracted Room");
                        }
                        //else if (Header.ColumnName == "OverrideRoomSingle")
                        //{
                        //    xtwWriter.WriteValue("SGL Override");
                        //}
                        //else if (Header.ColumnName == "OverrideRoomDouble")
                        //{
                        //    xtwWriter.WriteValue("DBL Override");
                        //}
                        else if (Header.ColumnName == "TotalOverrideRoom")
                        {
                            xtwWriter.WriteValue("Total Override Room");
                        }
                        //else if (Header.ColumnName == "TotalRoomsSingle")
                        //{
                        //    xtwWriter.WriteValue("Total SGL Room");
                        //}
                        //else if (Header.ColumnName == "TotalRoomDouble")
                        //{
                        //    xtwWriter.WriteValue("Total DBL Room");
                        //}
                        else if (Header.ColumnName == "TotalRoom")
                        {
                            xtwWriter.WriteValue("Total Room Block (Contracted + Override)");
                        }
                        //else if (Header.ColumnName == "EmergencyRoomSingle")
                        //{
                        //    xtwWriter.WriteValue("SGL Emergency");
                        //}
                        //else if (Header.ColumnName == "EmergencyRoomDouble")
                        //{
                        //    xtwWriter.WriteValue("DBL Emergency");
                        //}
                        //else if (Header.ColumnName == "TotalEmergencyRoom")
                        //{
                        //    xtwWriter.WriteValue("Total Emergency Room");
                        //}

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

                            if (dtSource.Columns[i - 1].Caption.ToUpper() == "SDATE")
                            {
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            }
                            else
                            {
                                xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
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
        /// Modified By:    Josephine Gad
        /// Date Modified:  08/Nov/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/Calendar/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoLinkButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
        #endregion  

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.Security;
using System.IO;
using AjaxControlToolkit;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;


namespace TRAVELMART.Hotel
{
    public partial class HotelExceptionBookingsDashboard : System.Web.UI.Page
    {
        ExceptionBLL BLL = new ExceptionBLL();

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                HtmlTable uoTblDate = (HtmlTable)Master.FindControl("uoTblDate");
                uoTblDate.Style.Add("display", "none");

                BindRegionList();
                BindPortList();
                BindYear();
                BindMonth();
                BindDashboard();
            }
            ListView1.DataSource = null;
            ListView1.DataBind();
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session.Remove("Port");
            BindPortList();            
        }
        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;     
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            BindDashboard();
        }
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            ExportException();
        }
        #endregion

        #region "Procedure"
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
        /// Modified by:        Josephine Gad
        /// Modified Created:   25/05/2012
        /// Description:        Use session to get region list; use RegionListByUser if session is null
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   06/07/2012
        /// Created By:     Josephine Gad
        /// (description)   Load Port List 
        /// </summary>
        private void BindPortList()
        {
            List<PortList> list = new List<PortList>();
            try
            {
                list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");

                uoDropDownListPortPerRegion.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListPortPerRegion.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPortPerRegion.DataSource = list;
                    uoDropDownListPortPerRegion.DataTextField = "PORTName";
                    uoDropDownListPortPerRegion.DataValueField = "PORTID";
                    uoDropDownListPortPerRegion.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListPortPerRegion.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListPortPerRegion.SelectedValue = GlobalCode.Field2String(Session["Port"]);
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
        /// Date Created:   10/Jan/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Year in Dropdown list
        /// </summary>
        private void BindYear()
        {
            uoDropDownListYear.Items.Clear();
            
            int iYear = 2013;
            int iYearCurrent = DateTime.Now.Year;

            while (iYear <= (iYearCurrent + 1))
            {
                uoDropDownListYear.Items.Add(new ListItem(iYear.ToString(), iYear.ToString()));
                iYear += 1;
            }
            
            uoDropDownListYear.DataBind();
            uoDropDownListYear.SelectedValue = iYearCurrent.ToString();
        }
        /// <summary>
        /// Date Created:   10/Jan/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Month in Dropdown list
        /// </summary>
        private void BindMonth()
        {
            uoDropDownListMonth.Items.Clear();
            uoDropDownListMonth.Items.Add(new ListItem("--Select Month--", "0"));
           // uoDropDownListMonth.Items.Add(new ListItem("--Current & Future--", "-1"));
            int i = 1;
            int iTotal = 12;
            while (i <= iTotal)
            {
                string sMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                uoDropDownListMonth.Items.Add(new ListItem(sMonth, i.ToString()));
                i += 1;
            }
            uoDropDownListMonth.DataBind();
            uoDropDownListMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
        /// <summary>
        /// Date Created:   13/Jan/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Dashboard
        /// </summary>
        private void BindDashboard()
        {
            List<ExceptionsByMonth> list = new List<ExceptionsByMonth>();

            int iYear;
            Int16 iMonth;
            int iRegion;
            int iPort;

            iYear = GlobalCode.Field2Int(uoDropDownListYear.SelectedValue);
            iMonth = GlobalCode.Field2TinyInt(uoDropDownListMonth.SelectedValue);
            iRegion = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue);
            iPort = GlobalCode.Field2Int(uoDropDownListPortPerRegion.SelectedValue);

            list = BLL.GetExceptionByMonth(iYear, iMonth, iRegion, iPort, uoHiddenFieldUser.Value);
            uoListViewDashboard.DataSource = list;
            uoListViewDashboard.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   13/Jan/2014
        /// Description:    create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/ExceptionList/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "ExceptionByDashboard_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                string sRegion = "";
                string sPort = "";

                if (uoDropDownListRegion.SelectedValue == "0")
                {
                    sRegion = "All";
                }
                else
                {
                    sRegion = uoDropDownListRegion.SelectedItem.Text;
                }

                if (uoDropDownListPortPerRegion.SelectedValue == "0")
                {
                        sPort = "All";
                }
                else
                { 
                
                    string[] sArr = uoDropDownListPortPerRegion.SelectedItem.Text.Split("-".ToCharArray());
                    if (sArr.Length > 0)
                    {
                        sPort = sArr[0];
                    }
                }

                string sMonth = "";
                if (uoDropDownListMonth.SelectedValue == "0")
                {
                    sMonth = "All";
                }
                if (uoDropDownListMonth.SelectedValue == "-1")
                {
                    sMonth = "Current&Future";
                }
                else
                {
                    sMonth = uoDropDownListMonth.SelectedItem.Text;
                }

                string sSheetName = uoDropDownListYear.SelectedValue + "-" + sMonth
                        + "_Region-" + sRegion + "_Port-" + sPort ;
                CreateExcel(dt, strFileName, sSheetName);
                OpenExcelFile(FileName, strFileName);
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
        /// Date Created:   13/Jan/2014
        /// Description:    Get Exception List to export
        /// </summary>
        private void ExportException()
        {
            DataTable dt = null;
            try
            {
                dt = BLL.GetExceptionByMonthExport(uoHiddenFieldUser.Value);
                if (dt.Rows.Count > 0)
                {
                    CreateFile(dt);
                }
                else
                {
                    AlertMessage("No record to export!");
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
        /// Author:         Josephine Gad
        /// Date Created:   13/Jan/2014
        /// Description:    Create Excel file
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName, string sSheetName)
        {
            decimal iDec = 0;
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = dtSource.Columns.Count;
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
                xtwWriter.WriteAttributeString("ss", "Name", null, sSheetName);

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
                    if (Header.ColumnName != "xRow")
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
                    }
                    i++;
                }
                xtwWriter.WriteEndElement();


                // Run through all rows of data source
                foreach (DataRow row in dtSource.Rows)
                {
                    // <Row>
                    xtwWriter.WriteStartElement("Row");

                    i = 0;
                    // Run through all cell of current rows
                    foreach (object cellValue in row.ItemArray)
                    {
                        if (i <= (iColCount - 1))
                        {
                            if (dtSource.Columns[i].ColumnName.ToUpper() != "XROW")
                            {
                                // <Cell>
                                xtwWriter.WriteStartElement("Cell");

                                // <Data ss:Type="String">xxx</Data>
                                xtwWriter.WriteStartElement("Data");

                                if (dtSource.Columns[i].ColumnName.ToUpper() == "EMPLOYEEID" ||
                                    dtSource.Columns[i].ColumnName.ToUpper() == "STRIPES" ||
                                    dtSource.Columns[i].ColumnName.ToUpper() == "SINGLEDOUBLE" ||
                                    dtSource.Columns[i].ColumnName.ToUpper() == "FLIGHTNO"
                                    )
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
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
        /// Date Modified:  13/Jan/2014
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {

            //Response.Redirect("~/Extract/ExceptionList/" + strFileName, false);

            string strScript = "CloseModal('../Extract/ExceptionList/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       13/Jan/2014
        /// Description:        pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        #endregion                    
    }
}

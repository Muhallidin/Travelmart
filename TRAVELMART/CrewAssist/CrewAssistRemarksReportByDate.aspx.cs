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
    public partial class CrewAssistRemarksReportByDate : System.Web.UI.Page
    {
        ReportBLL BLL = new ReportBLL();

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                HtmlTable uoTblDate = (HtmlTable)Master.FindControl("uoTblDate");
                uoTblDate.Style.Add("display", "none");

                //BindRegionList();
                //BindPortList();
                BindYear();
                BindMonth();

                uoHiddenFieldLoadType.Value = "0";
                //BindSeafarer();
                BindDashboard();
                BindSourceType();
            }
            ListView1.DataSource = null;
            ListView1.DataBind();
        }

        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Region"] = uoDropDownListRegion.SelectedValue;
        //    Session.Remove("Port");
        //    BindPortList();            
        //}
        //protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;     
        //}
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";
            //if (uoCheckBoxByDateRange.Checked)
            //{
            //    ucTRDate.Visible = true;
            //    ucTRYearMonth.Visible = false;
            //}
            //else
            //{ 
            //    ucTRDate.Visible = false;
            //    ucTRYearMonth.Visible = true;
            //}

            BindDashboard();
        }
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            ExportReport();
        }
        protected void uoObjectDataSourceCrewAssist_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //e.InputParameters["iYear"] = GlobalCode.Field2Int(uoDropDownListYear.SelectedValue);
            //e.InputParameters["iMonth"] = GlobalCode.Field2Int(uoDropDownListMonth.SelectedValue);

            //if (uoDropDownListReportBy.SelectedValue == "3")
            //{
            //    e.InputParameters["sCreatedBy"] = uoDropDownListFilter.SelectedValue;
            //}
            //else
            //{
            //    e.InputParameters["sCreatedBy"] = "";
            //}
            //e.InputParameters["sUserID"] = uoHiddenFieldUser.Value;
            //e.InputParameters["iLoadType"] = uoHiddenFieldLoadType.Value;
            //e.InputParameters["iFilterBy"] = uoDropDownListReportBy.SelectedValue;
            //e.InputParameters["sFilterValue"] = (uoDropDownListFilter.SelectedValue == "0" ? "" : uoDropDownListFilter.SelectedValue);
            //e.InputParameters["sOrderBy"] = uoHiddenFieldOrderBy.Value;
        }
        protected void uoListViewManifestPager_PreRender(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "1";
        }
        //protected void uoButtonSearch_Click(object sender, EventArgs e)
        //{
            //uoHiddenFieldLoadType.Value = "0";

            //if (uoDropDownListReportBy.SelectedValue == "1" || uoDropDownListReportBy.SelectedValue == "2")
            //{
            //    BindSeafarer();
            //    if (uoDropDownListFilter.SelectedValue != "0")
            //    {
            //        BindDashboard();
            //    }
            //}
            //else if (uoDropDownListReportBy.SelectedValue == "3" )
            //{
            //    BindUsers();
            //    if (uoDropDownListFilter.SelectedValue != "0")
            //    {
            //BindDashboard();
            //    }
            //}
        //}
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName;
        }
        #endregion

        #region "Procedure"        
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
        /// Date Created:   29/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Bind Dashboard
        /// </summary>
        private void BindDashboard()
        {
            Int16 iRequestSourceID;
            iRequestSourceID = GlobalCode.Field2TinyInt(uoDropDownListSourceType.SelectedValue);
            int iYear = GlobalCode.Field2Int(uoDropDownListYear.SelectedValue);
            int iMonth = GlobalCode.Field2Int(uoDropDownListMonth.SelectedValue);
            DateTime dFrom = DateTime.Now;
            DateTime dTo = dFrom;

            if (uoCheckBoxByDateRange.Checked)
            {
                dFrom = GlobalCode.Field2DateTime(uoTextBoxFrom.Text);
                dTo = GlobalCode.Field2DateTime(uoTextBoxTo.Text);
            }


            List<RemarksByDateList> list = new List<RemarksByDateList>();
            list = BLL.GetCrewAssistRemarksByDate(iRequestSourceID, uoCheckBoxByDateRange.Checked,
                iYear, iMonth, dFrom, dTo, uoHiddenFieldUser.Value, GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value),
                uoHiddenFieldOrderBy.Value, 0,0);

            uoListViewDashboard.DataSource = list;
            //uoListViewDashboard.DataSource = null;
            //uoListViewDashboard.DataSourceID = "uoObjectDataSourceCrewAssist";
            uoListViewDashboard.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   29/Jan/2015
        /// Description:    Bind Source Type
        /// </summary>
        private void BindSourceType()
        {
            List<RemarksSourceList> listSource = new List<RemarksSourceList>();
            listSource = (List<RemarksSourceList>)Session["CrewAssistRemarksReportByDate_SourceList"];

            uoDropDownListSourceType.Items.Clear();

            uoDropDownListSourceType.DataSource = listSource;
            uoDropDownListSourceType.DataTextField = "RequestSource";
            uoDropDownListSourceType.DataValueField = "RequestSourceIDint";
            uoDropDownListSourceType.DataBind();
            uoDropDownListSourceType.Items.Insert(0, new ListItem("--Select Source--", "0"));

        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   26/Jan/2015
        /// Description:    create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/CrewAssist/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "CrewAssistReportByDate_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

                CreateExcel(dt, strFileName, "Remarks");
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
        private void ExportReport()
        {
            DataTable dt = null;
            try
            {
                List<RemarksByDateList> listSource = new List<RemarksByDateList>();
                if (Session["CrewAssistRemarksReportByDate_RemarksList"] != null)
                {
                    listSource = (List<RemarksByDateList>)Session["CrewAssistRemarksReportByDate_RemarksList"];
                    dt = getDataTable(listSource);
                }
                else
                {
                    dt = BLL.GetCrewAssistRemarksByDateExport(uoHiddenFieldUser.Value);
                }
               

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
        /// Author:         Josephine Monteza
        /// Date Created:   26/Jun/2015
        /// Description:    Create Excel file
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName, string sSheetName)
        {
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

                                if (dtSource.Columns[i].ColumnName.ToUpper() == "iCount")
                                    
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

            string strScript = "CloseModal('../Extract/CrewAssist/" + strFileName + "');";
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
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   29/Jun/2015
        /// Description:    convert list to datatable
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
        /// Author:         Josephine Monteza
        /// Date Created:   29/Jun/2015
        /// Description:    get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                //if (!t.IsValueType)
                //{
                //    return t;
                //}
                //else
                //{
                //    return Nullable.GetUnderlyingType(t);
                //}
                return t;
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Date Created:   26/June/2015
        /// Created By:     Josephine monteza
        /// (description)   Get Seafarer list
        /// ---------------------------------------------
        /// </summary>
        //private void BindSeafarer()
        //{
        //    List<SeafarerListDTO> list = new List<SeafarerListDTO>();

        //    if (uoDropDownListReportBy.SelectedValue == "1" || uoDropDownListReportBy.SelectedValue == "2")
        //    {
        //        list = SeafarerBLL.GetSeafarerListByFilter(uoDropDownListReportBy.SelectedValue, uoTextBoxSearch.Text.Trim());            
        //    }
           
        //    uoDropDownListFilter.Items.Clear();

        //    uoDropDownListFilter.DataSource = list;
        //    uoDropDownListFilter.DataTextField = "Name";
        //    uoDropDownListFilter.DataValueField = "SFID";
        //    uoDropDownListFilter.DataBind();
            
        //    ListItem item = new ListItem("--SELECT SEAFARER--", "0");
        //    uoDropDownListFilter.Items.Insert(0, item);

        //    if (list.Count == 1)
        //    {
        //        uoDropDownListFilter.SelectedValue = list[0].SFID;
        //    }
        //    else
        //    {
        //        uoDropDownListFilter.SelectedValue = "0";
        //    }
        //}
        /// <summary>
        /// Date Created:   26/June/2015
        /// Created By:     Josephine monteza
        /// (description)   Get User list
        /// ---------------------------------------------
        /// </summary>
        //private void BindUsers()
        //{
        //    List<UserList> list = new List<UserList>();

        //    if (uoDropDownListReportBy.SelectedValue == "3")
        //    {
        //        list = ReportBLL.GetUserList(uoTextBoxSearch.Text.Trim(), true);
        //    }

        //    uoDropDownListFilter.Items.Clear();

        //    uoDropDownListFilter.DataSource = list;
        //    uoDropDownListFilter.DataTextField = "sUserName";
        //    uoDropDownListFilter.DataValueField = "sUserName";
        //    uoDropDownListFilter.DataBind();

        //    ListItem item = new ListItem("--SELECT USERNAME--", "0");
        //    uoDropDownListFilter.Items.Insert(0, item);

        //    if (list.Count == 1)
        //    {
        //        uoDropDownListFilter.SelectedValue = list[0].sUserName;

        //    }
        //    //if (uoHiddenFieldFilterValue.Value != "")
        //    //{
        //    //    if (uoDropDownListFilter.Items.FindByValue(uoHiddenFieldFilterValue.Value) != null)
        //    //    {
        //    //        uoDropDownListFilter.SelectedValue = uoHiddenFieldFilterValue.Value;
        //    //    }
        //    //}
        //    else
        //    {
        //        uoDropDownListFilter.SelectedValue = "0";
        //    }

        //}
        #endregion                    

       

       
    }
}

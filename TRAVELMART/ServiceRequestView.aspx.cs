using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using System.Web.Security;
using TRAVELMART.BLL;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;

namespace TRAVELMART
{
    public partial class ServiceRequestView : System.Web.UI.Page
    {
        ServiceRequestBLL BLL = new ServiceRequestBLL();

        #region "EVENTS"
        protected void Page_Load(object sender, EventArgs e)
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
            else{
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }
            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            Session["strPrevPage"] = Request.RawUrl;

            uoListViewServiceRequestHeader.DataSource = null;
            uoListViewServiceRequestHeader.DataBind();
            
            if (!IsPostBack)
            {
                BinCrewAssistUsers();
            }

        }
        protected void uoListViewServiceRequest_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //BLL.GetVehicleManifestByPageNumber(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value, e.StartRowIndex, e.MaximumRows, "Confirm");
        }
        protected void uoListViewServiceRequest_DataBound(object sender, EventArgs e)
        {
            //ButtonSettings();
        }
        //protected void uoCheckBoxViewAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    BindServiceRequest();
        //}
        protected void uoListViewServiceRequest_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //if (e.CommandName != "")
            //{
            //    uoHiddenFieldSortBy.Value = e.CommandName;
            //    BindServiceRequest();
            //}
        }
        protected void uoListViewServiceRequestHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName != "")
            {
                uoHiddenFieldSortBy.Value = e.CommandName;
                BindServiceRequest();
            }
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoDataPagerManifest.SetPageProperties(0, uoDataPagerManifest.PageSize, false);
            uoHiddenFieldEmployeeID.Value = uoTextBoxEmployeeID.Text;
            BindServiceRequest();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            CancelActivateServiceRequest();
        }
        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                dt = BLL.GetServiceRequestExport(uoHiddenFieldUser.Value);
                CreateFile(dt);
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

        #region "FUNCTIONS"
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   18/Oct/2013
        /// description     Refresh Service Request List
        /// </summary>
        private void BindServiceRequest() 
        {
            uoListViewServiceRequest.DataBind();
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   18/Oct/2013
        /// description     Get Service Request List
        /// </summary>       
        public List<ServiceRequestList> GetServiceRequestList(DateTime dDate, string sUser, int iStartRow, int iMaxRow,
            Int16 iLoad, string sOrderBy, Int16 iViewFilter, Int16 iViewActive, Int16 iViewBooked,
            Int16 iFilterType, Int64 iEmployeeID, string sCrewAssistUser)
        {
            List<ServiceRequestList> list = new List<ServiceRequestList>();

            BLL.GetServiceRequestList(dDate, sUser, iStartRow, iMaxRow, iLoad, sOrderBy,
                iViewFilter, iViewActive, iViewBooked, iFilterType, iEmployeeID, sCrewAssistUser);

            if (Session["ServiceRequestView_ServiceReqList"] != null)
            {
                list = (List<ServiceRequestList>)Session["ServiceRequestView_ServiceReqList"];
            }
            return list;
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   18/Oct/2013
        /// description     Get Service Request Count
        /// </summary>        
        public int GetServiceRequestCount(DateTime dDate, string sUser,
           Int16 iLoad, string sOrderBy, Int16 iViewFilter, Int16 iViewActive, Int16 iViewBooked,
            Int16 iFilterType, Int64 iEmployeeID, string sCrewAssistUser)
        {
            int iCount = 0;
            if (Session["ServiceRequestView_Count"] != null)
            {
                iCount = GlobalCode.Field2Int(Session["ServiceRequestView_Count"]);
            }
            return iCount;
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   25/Oct/2013
        /// description     Cancel or Activate Service Request
        /// </summary>
        private void CancelActivateServiceRequest()
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();                
                DataColumn col = new DataColumn("IdentityID", typeof(Int64));
                DataRow row;

                dt.Columns.Add(col);
                col = new DataColumn("ServiceType", typeof(string));
                dt.Columns.Add(col);
                col = new DataColumn("IsActiveBit", typeof(bool));
                dt.Columns.Add(col);

                int iCount = uoListViewServiceRequest.Items.Count;

                HiddenField lvHiddenFielReqIdHotel;
                HiddenField lvHiddenFielReqIdVehicle;
                HiddenField lvHiddenFielReqIdMeetGreet;

                CheckBox lvCheckBoxHotelActive;
                CheckBox lvCheckBoxVehicleActive;
                CheckBox lvCheckBoxMeetGreetActive;
                int i = 0;
                for (i = 0; i < iCount; i++)
                {
                    lvHiddenFielReqIdHotel = (HiddenField)uoListViewServiceRequest.Items[i].FindControl("uoHiddenFielReqIdHotel");
                    lvHiddenFielReqIdVehicle = (HiddenField)uoListViewServiceRequest.Items[i].FindControl("uoHiddenFielReqIdVehicle");
                    lvHiddenFielReqIdMeetGreet = (HiddenField)uoListViewServiceRequest.Items[i].FindControl("uoHiddenFielReqIdMeetGreet");

                    lvCheckBoxHotelActive = (CheckBox)uoListViewServiceRequest.Items[i].FindControl("uoCheckBoxHotelActive");
                    lvCheckBoxVehicleActive = (CheckBox)uoListViewServiceRequest.Items[i].FindControl("uoCheckBoxVehicleActive");
                    lvCheckBoxMeetGreetActive = (CheckBox)uoListViewServiceRequest.Items[i].FindControl("uoCheckBoxMeetGreetActive");
                    
                    Int64 iReqIDHotel = GlobalCode.Field2Int(lvHiddenFielReqIdHotel.Value);
                    Int64 iReqIDVehicle = GlobalCode.Field2Int(lvHiddenFielReqIdVehicle.Value);
                    Int64 iReqIDMeetGreet = GlobalCode.Field2Int(lvHiddenFielReqIdMeetGreet.Value);

                    if (iReqIDHotel > 0 && lvCheckBoxHotelActive.Enabled)
                    {                        
                        row = dt.NewRow();
                        row[0] = GlobalCode.Field2Int(lvHiddenFielReqIdHotel.Value);
                        row[1] = "HotelRequest";
                        row[2] = lvCheckBoxHotelActive.Checked;
                        dt.Rows.Add(row);
                    }
                    if (iReqIDVehicle > 0 && lvCheckBoxVehicleActive.Enabled)
                    {
                        row = dt.NewRow();
                        row[0] = GlobalCode.Field2Int(lvHiddenFielReqIdVehicle.Value);
                        row[1] = "VehicleRequest";
                        row[2] = lvCheckBoxVehicleActive.Checked;
                        dt.Rows.Add(row);
                    }
                    if (iReqIDMeetGreet > 0 && lvCheckBoxMeetGreetActive.Enabled)
                    {
                        row = dt.NewRow();
                        row[0] = GlobalCode.Field2Int(lvHiddenFielReqIdMeetGreet.Value);
                        row[1] = "MeetGreetRequest";
                        row[2] = lvCheckBoxMeetGreetActive.Checked;
                        dt.Rows.Add(row);
                    }
                }
                if (i > 0)
                {
                    string strLogDescription = "Cancel or Activate Service Request";
                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                    BLL.CancelActivateServiceRequest(uoHiddenFieldUser.Value, strLogDescription, "CancelActivateServiceRequest",
                        Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate).ToString(), dt);

                    AlertMessage("Service Request successfully  updated!");
                }
                else
                {
                    AlertMessage("No Service Request!");
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
        /// Date Created:   25/Oct/2013
        /// Description:    pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = '" + s + "'; ";
            sScript += "alert( msg );";            

            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/Oct/2013
        /// Description:    create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/TentativeManifest/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string FileName = "ServiceRequest_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName);
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
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  31/October/2013
        /// Description:    Create the XML format of Excel file
        ///                 Add style S65  to align all rows to Left
        public static void CreateExcel(DataTable dtSource, string strFileName)
        {
            try
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
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Service Request");

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
                        if (i <= iColCount && i >= 2)
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
                            if (i <= iColCount && i >= 2)
                            {
                                // <Cell>
                                xtwWriter.WriteStartElement("Cell");

                                // <Data ss:Type="String">xxx</Data>
                                xtwWriter.WriteStartElement("Data");

                                if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHTNO" //||
                                    //--dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                    //dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER"
                                    )
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                //check cost center if number or not
                                //else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                //{
                                //    if (GlobalCode.Field2Int(cellValue) > 0)
                                //    {
                                //        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                //    }
                                //    else
                                //    {
                                //        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                //    }
                                //}
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
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  31/Oct/2013
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/TentativeManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  05/Nov/2013
        /// Description:    Bind the Crew Assist Users in Drop Down List
        /// </summary>
        private void BinCrewAssistUsers()
        {
            List<CrewAssistUsers> list = new List<CrewAssistUsers>();
            list = BLL.GetCrewAssist();
            uoDropDownListCrewAssist.Items.Clear();
            uoDropDownListCrewAssist.Items.Insert(0, new ListItem("--Select Crew Assist--", "0"));
            if (list.Count > 0)
            {
                uoDropDownListCrewAssist.DataSource = list;
                uoDropDownListCrewAssist.DataTextField = "UserName";
                uoDropDownListCrewAssist.DataValueField = "UserID";
            }
            uoDropDownListCrewAssist.DataBind();
        }
        #endregion       
    }
}

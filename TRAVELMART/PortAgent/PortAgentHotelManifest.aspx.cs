using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;

namespace TRAVELMART
{
    public partial class PortAgentHotelManifest : System.Web.UI.Page
    {
        PortAgentBLL BLL = new PortAgentBLL();

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("..\\Login.aspx");
            }  
            Session["strPrevPage"] = Request.RawUrl;
          

            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); 
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                 
                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }
                if (sChangeDate == "1")
                {
                    uoHiddenFieldPortAgentID.Value = GlobalCode.Field2Int(Session["PortAgentID"]).ToString();
                }
                else
                {
                    uoHiddenFieldPortAgentID.Value = GlobalCode.Field2Int(Request.QueryString["pid"]).ToString();
                    Session["PortAgentID"] = uoHiddenFieldPortAgentID.Value;
                }

                BindPortAgent();
                BindNoOfDays();
                BindStatus();

                if (Request.QueryString["Dy"] != null)
                {
                    uoDropDownListDays.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListDays, GlobalCode.Field2Int(Request.QueryString["Dy"]));
                }
                if (Request.QueryString["PA"] != null)
                {
                    uoDropDownListPortAgent.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListPortAgent, GlobalCode.Field2Int(Request.QueryString["PA"]));
                }
                if (Request.QueryString["st"] != null)
                {
                    uoDropDownListStatus.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownListStatus, GlobalCode.Field2Int(Request.QueryString["st"]));
                }

                BindHotelManifest(0);                
               
            }
            else
            {
                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

                if (uoHiddenFieldPopupCalendar.Value == "1" || (sChangeDate == "1"))
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                    BindHotelManifest(0);
                }
            }

            uoListViewHotelHeader.DataSource = null;
            uoListViewHotelHeader.DataBind();
            if (uoHiddenFieldHotelConfirm.Value == "1")
            {
                BindHotelManifest(1);                
                uoHiddenFieldHotelConfirm.Value = "0";                            
            }

        }

        protected void uoDropDownListPortAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldPortAgentID.Value = uoDropDownListPortAgent.SelectedValue;
            Session["PortAgentID"] = uoHiddenFieldPortAgentID.Value;            
        }

        protected void uoDropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldManifestStatus.Value = uoDropDownListStatus.SelectedValue;
            Session["PortAgentManifestStatus"] = uoDropDownListStatus.SelectedValue;
        }
        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
        }
        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/Mar/2014
        /// Description:    Get the order to be used
        /// -------------------------------------  
        protected void uoListViewHotelHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrder.Value = e.CommandName;
            //uolistviewHotelInfoPager.SetPageProperties(0, uolistviewHotelInfoPager.PageSize, false);            
        }
        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            if (Session["URL"] != null)
            {
                string sURL = GlobalCode.Field2String(Session["URL"]);
                linkConfirm.HRef = sURL;
            }
            if (uolistviewHotelInfo.Items.Count > 0)
            {
                if (uoDropDownListPortAgent.SelectedValue != "0" && (User.IsInRole(TravelMartVariable.RolePortSpecialist)))
                {
                    uoButtonConfirm.Visible = true;
                    uoButtonCancelVendor.Visible = true;
                }
                else
                {
                    uoButtonConfirm.Visible = false;
                    uoButtonCancelVendor.Visible = false;
                }

                if (uoDropDownListPortAgent.SelectedValue != "0" && (User.IsInRole(TravelMartVariable.RoleHotelSpecialist) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAssist)))
                {
                    uoButtonEdit.Visible = true;
                }
                else
                {
                    uoButtonEdit.Visible = false;
                }

                if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAssist))
                {
                    uoButtonApprove.Visible = true;
                    uoButtonCancelRCCL.Visible = true;
                }
                else
                {
                    uoButtonApprove.Visible = false;
                    uoButtonCancelRCCL.Visible = false;
                }
                uoButtonExport.Visible = true;
            }
            else
            {
                uoButtonConfirm.Visible = false;
                uoButtonApprove.Visible = false;
                uoButtonCancelVendor.Visible = false;
                uoButtonCancelRCCL.Visible = false;
                uoButtonExport.Visible = false;
            }
        }
        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                dt = BLL.PortAgentManifestGetConfirmHotelExport(uoHiddenFieldUser.Value);
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
        protected void uoDropDownListDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NoOfDays"] = uoDropDownListDays.SelectedValue;
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Monteza
        /// Date Created:   05/Jan/2015
        /// Descrption:     Tag Record
        /// -------------------------------------------------------------------
        /// </summary>
        protected void uolistviewHotelInfo_ItemCommand(object sender, ListViewCommandEventArgs e)
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
        #endregion

        #region "Functions"
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Bind Service Provider in DropDownList
        /// -------------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  07/Jul/2014
        /// Descrption:     If Session is NULL go to BLL.GetPortAgentListByPortId
        /// -------------------------------------------------------------------
        private void BindPortAgent()
        {
            List<PortAgentDTO> list = new List<PortAgentDTO>();

            uoDropDownListPortAgent.Items.Clear();
            uoDropDownListPortAgent.Items.Add(new ListItem("--Select Service Provider--", "0"));

            if (Session["PortAgentDTO"] != null)
            {
                list = (List<PortAgentDTO>)Session["PortAgentDTO"];               
            }
            else
            {
                list = BLL.GetPortAgentListByPortId(GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue),
                    uoHiddenFieldUser.Value, uoHiddenFieldRole.Value);
                Session["PortAgentDTO"] = list;
            }
            uoDropDownListPortAgent.DataSource = list;
            uoDropDownListPortAgent.DataTextField = "PortAgentName";
            uoDropDownListPortAgent.DataValueField = "PortAgentID";
            uoDropDownListPortAgent.DataBind();

            if (list.Count == 1)
            {
                uoDropDownListPortAgent.SelectedValue = list[0].PortAgentID;
            }
            else if (uoDropDownListPortAgent.Items.FindByValue(uoHiddenFieldPortAgentID.Value) != null)
            {
                uoDropDownListPortAgent.SelectedValue = uoHiddenFieldPortAgentID.Value;
            }
            Session["PortAgentID"] = uoDropDownListPortAgent.SelectedValue;
            uoHiddenFieldPortAgentID.Value = uoDropDownListPortAgent.SelectedValue;
        }
        private void BindHotelManifest(Int16 iLoadType)
        {
            uolistviewHotelInfo.DataSourceID = uoObjectDataSourceManifest.UniqueID;
            if (iLoadType != 0)
            {
                uolistviewHotelInfo.DataBind();
            }           
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   07/Mar/2014
        /// Descrption:     Bind Hotel Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public List<PortAgentHotelManifestList> GetHotelConfirmManifestList(int iStatusID, string sType, int iPortAgentID, string sDate, string sUserID,
            string sRole, string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
            List<PortAgentHotelManifestList> list = new List<PortAgentHotelManifestList>();
            
            if(sType == "New") 
            {
                BLL.PortAgentManifestGetConfirmHotel(iStatusID, iPortAgentID, sDate, sUserID, sRole, sOrderBy, iLoadType, iNoOfDay, iSFID, iStartRow, iMaxRow);
                list = (List<PortAgentHotelManifestList>)Session["PortAgentHotelManifestList"];

                string sAddCancel = "";
                string sRecordLocID = "";
                string sTReqID = "";
                string sTransID = "";
                Session["URL"] = null;
                foreach (PortAgentHotelManifestList item in list)
                {
                    if (item.RequestStatus == "Cancelled")
                    {
                        if (sAddCancel != "")
                        {
                            sAddCancel += ",";
                        }
                        if (sRecordLocID != "")
                        {
                            sRecordLocID += ",";
                        }
                        if (sTReqID != "")
                        {
                            sTReqID += ",";
                        }
                        if (sTransID != "")
                        {
                            sTransID += ",";
                        }
                        sAddCancel += "Cancel";
                        sRecordLocID += item.IdBigint.ToString();
                        sTReqID += item.TravelReqID.ToString();
                        sTransID += item.TransHotelID.ToString();
                    }
                }

                string sURL = "PortAgentHotelEditor.aspx?AddCancel=" + sAddCancel + "&RecLoc=" + 
                    sRecordLocID + "&TReqID=" + sTReqID + "&TransID=" + sTransID;
                if (sURL != "")
                {
                    Session["URL"] = sURL;
                }
            }
            return list;
        }
        public int GetHotelConfirmManifestCount(int iStatusID, string sType, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID)
        {
            int iTotalRow = GlobalCode.Field2Int(Session["PortAgentHotelManifestCount"]);
            return iTotalRow;
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Get List of Service Provider, Hotel Manifest and Vehicle Manifest
        /// -------------------------------------------------------------------
        /// </summary>        
        //private void BindList(Int16 iLoadType)
        //{
        //    BLL.PortAgentManifestGet(GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue),
        //        uoHiddenFieldDate.Value, uoHiddenFieldUser.Value, uoHiddenFieldOrder.Value,
        //        iLoadType);

        //}
        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Service Provider"; //"Check In";
            string GroupValueString = "PortAgentName"; //"colTimeSpanStartDate";

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
                    return string.Format("<tr><td class=\"group\" colspan=\"37\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2014
        /// Descrption:     Bind Manifest Status in drop down list
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindStatus()
        {
            List<ManifestStatus> listStatus = new List<ManifestStatus>();
            if (Session["ManifestStatus"] != null)
            {
                listStatus = (List<ManifestStatus>)Session["ManifestStatus"];
            }
            else
            {
                listStatus = BLL.GetManifestStatus();
                Session["ManifestStatus"] = listStatus;
            }
            uoDropDownListStatus.Items.Clear();
            uoDropDownListStatus.Items.Add(new ListItem("--Select Status--","0"));
            
            uoDropDownListStatus.DataSource = listStatus;
            uoDropDownListStatus.DataTextField = "sStatus";
            uoDropDownListStatus.DataValueField = "iStatusID";
            uoDropDownListStatus.DataBind();
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2014
        /// Description:    create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/PortManifest/");

                string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);

                string FileName = "PA_HotelManifest_" + sDateManifest + "_" + sDate + ".xls";
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
        /// Created By:     Josephine Gad
        /// Date Created:   19/Mar/2014
        /// Description:    Create the XML-excel format file
        /// ---------------------------------------------
        public static void CreateExcel(DataTable dtSource, string strFileName)
        {
            try
            {
                string sPortAgentName = GlobalCode.Field2String(dtSource.Rows[0]["PortAgentName"]);
                sPortAgentName = (sPortAgentName.Trim() == "" ? "PortAgentManifest" : sPortAgentName);
                
                int iLength;
                iLength = sPortAgentName.Length;
                if (iLength > 25)
                {
                    iLength = 25;
                }

                sPortAgentName = sPortAgentName.Substring(0, iLength);

                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = (dtSource.Columns.Count-3);
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
                    xtwWriter.WriteAttributeString("ss", "Name", null, sPortAgentName);

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
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNITES" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "CONTRACTEDRATE" ||
                                    dtSource.Columns[i - 1].Caption.ToUpper() == "CONFIRMEDRATE")
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                //check cost center if number or not
                                else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                {
                                    if (GlobalCode.Field2Long(cellValue) > 0)
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
        /// Date Modified:  19/Mar/2014
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/PortManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   27/Mar/2014
        /// Descrption:     Bind No. Of Days
        /// -------------------------------------------------------------------
        /// </summary>
        private void BindNoOfDays()
        {
            uoDropDownListDays.Items.Clear();
            uoDropDownListDays.Items.Add(new ListItem("--Select No. of Days--", "0"));

            int iNoOfdays = TMSettings.NoOfDays;
            ListItem item;
            for (int i = 1; i <= iNoOfdays; i++)
            {
                item = new ListItem(i.ToString(), i.ToString());
                uoDropDownListDays.Items.Add(item);
            }

            uoDropDownListDays.DataBind();
            if (Session["NoOfDays"] != null)
            {
                string sDay = GlobalCode.Field2Int(Session["NoOfDays"]).ToString();
                if (uoDropDownListDays.Items.FindByValue(sDay) != null)
                {
                    uoDropDownListDays.SelectedValue = sDay;
                }
            }
            Session["NoOfDays"] = uoDropDownListDays.SelectedValue;
        }
        /// Date Created:   05/Jan/2015
        /// Created By:     Josephine Monteza
        /// (description)   tag record to TblTag_Hotel 
        /// -------------------------------------------     
        private void TagSeafarer(string sIdBigint, string sTRId, string sBranch, string sRecLoc, string sE1Id, string sStatusOnOff)
        {
            string sUser = uoHiddenFieldUser.Value;
            string sRole = uoHiddenFieldRole.Value;

            string strLogDescription = "Tag Seafarer To Hotel Service Provider";
            string strFunction = "TagSeafarer";
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();
            ManifestBLL MBLL = new ManifestBLL();
            
            MBLL.TagToHotel(GlobalCode.Field2Long(sIdBigint), GlobalCode.Field2Long(sTRId), sRecLoc,
                GlobalCode.Field2Long(sE1Id), sStatusOnOff,0, GlobalCode.Field2Long(sBranch),
                uoHiddenFieldUser.Value, strLogDescription, strFunction, Path.GetFileName(Request.Path),
                CommonFunctions.GetDateTimeGMT(dateNow), dateNow);

            if (sBranch != "0")
            {
                BindHotelManifest(1);
            }
        }
        #endregion      
    }
}

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
using System.Net;
using System.Web.Security;

namespace TRAVELMART
{
    public partial class ArrivalDepartureSameDate : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Date Modified:  19/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Use Session instead of Query String for date
        ///                 Get uoHiddenFieldPopupCalendar value of master page to refresh list
        /// -------------------------------------
        /// Date Modified:  29/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Get default value of date from QueryString if new load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //InitializeValues();
            string userRole = InitializeValues();
            SetDefaultValues();
            string userName = MUser.GetUserName();
            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;
            }
            else
            {
                if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                }
                else
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"];
                }
            }
            if (!IsPostBack)
            {
                //Session["strPrevPage"] = Request.RawUrl;
                //string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                //string userRole = UserRolePrimary;

                //uoHiddenFieldUserRole.Value = userRole;//MUser.GetUserRole();
                //uoHiddenFieldUser.Value = userName;//MUser.GetUserName();                

                //SetDefaultValues();
                GetVessel();
                GetNationality();
                GetGender();
                GetRank();
                BindRegionList();
                BindPortList();
                if (userRole == TravelMartVariable.RoleHotelVendor || userRole == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }
            }
            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            if (uoHiddenFieldPopupCalendar.Value == "1")
            {
                BindRegionList();
                GetTravelRequestList();

            }

            ListView1.DataSource = null;
            ListView1.DataBind();

        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "0";

            uoObjectDataSourceTR.SelectParameters["LoadType"].DefaultValue = uoHiddenFieldLoadType.Value;
            uoObjectDataSourceTR.SelectParameters["FromDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceTR.SelectParameters["ToDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceTR.SelectParameters["UserID"].DefaultValue = uoHiddenFieldUser.Value;
            uoObjectDataSourceTR.SelectParameters["Role"].DefaultValue = uoHiddenFieldUserRole.Value;
            uoObjectDataSourceTR.SelectParameters["OrderBy"].DefaultValue = uoHiddenFieldOrderBy.Value;

            uoObjectDataSourceTR.SelectParameters["VesselID"].DefaultValue = uoDropDownListVessel.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["FilterByName"].DefaultValue = uoDropDownListFilterBy.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["SeafarerID"].DefaultValue = uoTextBoxFilter.Text;

            uoObjectDataSourceTR.SelectParameters["NationalityID"].DefaultValue = uoDropDownListNationality.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["Gender"].DefaultValue = uoDropDownListGender.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["RankID"].DefaultValue = uoDropDownListRank.SelectedValue;
            uoObjectDataSourceTR.SelectParameters["Status"].DefaultValue = uoDropDownListStatus.SelectedValue;

            uoObjectDataSourceTR.SelectParameters["RegionID"].DefaultValue = GlobalCode.Field2Int(Session["Region"]).ToString();
            uoObjectDataSourceTR.SelectParameters["CountryID"].DefaultValue = GlobalCode.Field2Int(uoHiddenFieldCountry.Value).ToString();
            uoObjectDataSourceTR.SelectParameters["CityID"].DefaultValue = GlobalCode.Field2Int(uoHiddenFieldCity.Value).ToString();
            uoObjectDataSourceTR.SelectParameters["PortID"].DefaultValue = GlobalCode.Field2Int(uoHiddenFieldPort.Value).ToString();
            GetTravelRequestList();
        }
        protected void uoListViewTRPager_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoObjectDataSourceTR_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
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

            e.InputParameters["RegionID"] = GlobalCode.Field2Int(Session["Region"]).ToString();
            e.InputParameters["CountryID"] = GlobalCode.Field2Int(uoHiddenFieldCountry.Value).ToString();
            e.InputParameters["CityID"] = GlobalCode.Field2Int(uoHiddenFieldCity.Value).ToString();
            e.InputParameters["PortID"] = GlobalCode.Field2Int(uoHiddenFieldPort.Value).ToString();
        }


        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName.ToString();
            if (e.CommandName != "")
            {
                GetTravelRequestList();
            }
        }

        protected void uoBtnSaveToExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                dt = GetList();
                if (dt.Rows.Count > 0)
                {
                    CreateFile(dt);
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
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session.Remove("Port");
            BindPortList();
            uoHiddenFieldRegion.Value = uoDropDownListRegion.SelectedValue;

            GetTravelRequestList();
        }
        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            uoHiddenFieldPort.Value = uoDropDownListPortPerRegion.SelectedValue;
            GetTravelRequestList();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 17/04/2012
        /// Description: pop up alert message
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

        protected void SetDefaultValues()
        {
            if (GlobalCode.Field2String(Session["Region"]) != "")
            {
                uoHiddenFieldRegion.Value = GlobalCode.Field2String(Session["Region"]);
            }
            if (GlobalCode.Field2String(Session["Country"]) != "")
            {
                uoHiddenFieldCountry.Value = GlobalCode.Field2String(Session["Country"]);
            }
            if (GlobalCode.Field2String(Session["City"]) != "")
            {
                uoHiddenFieldCity.Value = GlobalCode.Field2String(Session["City"]);
            }
            if (GlobalCode.Field2String(Session["Port"]) != "")
            {
                uoHiddenFieldPort.Value = GlobalCode.Field2String(Session["Port"]);
            }
            uoHiddenFieldLoadType.Value = "0";
            uoHiddenFieldOrderBy.Value = "SortByStatus";

            //if (GlobalCode.Field2String(Session["Region"]) != null && GlobalCode.Field2String(Session["Region"]) != "")
            //{
            //    uoHiddenFieldRegion.Value = GlobalCode.Field2String(Session["Region"]);
            //}
            //if (GlobalCode.Field2String(Session["Country"]) != null && GlobalCode.Field2String(Session["Country"]) != "")
            //{
            //    uoHiddenFieldCountry.Value = GlobalCode.Field2String(Session["Country"]);
            //}
            //if (GlobalCode.Field2String(Session["City"]) != null && GlobalCode.Field2String(Session["City"]) != "")
            //{
            //    uoHiddenFieldCity.Value = GlobalCode.Field2String(Session["City"]);
            //}
            //if (GlobalCode.Field2String(Session["Port"]) != null && GlobalCode.Field2String(Session["Port"]) != "")
            //{
            //   uoHiddenFieldPort.Value = GlobalCode.Field2String(Session["Port"]);
            //}
            //uoHiddenFieldLoadType.Value = "0";
            //uoHiddenFieldOrderBy.Value = "SortByStatus";
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
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldUserRole.Value);
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
        /// Date Created:   09/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Refresh No Travel Request List
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetTravelRequestList()
        {
            try
            {
                uoListViewTR.DataSource = null;
                uoListViewTR.DataSourceID = "uoObjectDataSourceTR";

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
                    ImageString += "negative.png";
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
        /// Date Created:   20/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Set No Travel Request groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string TRAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Ship";
            string GroupValueString = "Vessel";

            string currentDataFieldValue = Eval("VesselCode").ToString() + " - " + Eval(GroupValueString).ToString();

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
                    return string.Format("<tr><td class=\"group\" colspan=\"9\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
                else
                {
                    return string.Format("<tr><td class=\"groupRed\" colspan=\"16\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        protected DataTable GetList()
        {
            TravelRequestBLL TRBLL = new TravelRequestBLL();
            return TRBLL.GetTravelRequestArrivalDepartureSameDateListExport(0, GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                GlobalCode.Field2DateTime(uoHiddenFieldDate.Value), uoHiddenFieldUser.Value, uoHiddenFieldUserRole.Value,
                uoHiddenFieldOrderBy.Value, 0, 0, GlobalCode.Field2Int(uoDropDownListVessel.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListFilterBy.SelectedValue),
                uoTextBoxFilter.Text, GlobalCode.Field2Int(uoDropDownListNationality.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListGender.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListRank.SelectedValue),
                uoDropDownListStatus.SelectedValue, GlobalCode.Field2Int(uoHiddenFieldRegion.Value),
                GlobalCode.Field2Int(uoHiddenFieldCountry.Value), GlobalCode.Field2Int(uoHiddenFieldCity.Value),
                GlobalCode.Field2Int(uoHiddenFieldPort.Value));
        }

        protected void CreateFile(DataTable dt)
        {
            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/ArrDepSameOnOffDt/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            if (uoChkBoxPDF.Checked)
            {

            }
            else
            {
                string FileName = "ArrDepSameOnOffDt_" + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName);

                OpenExcelFile(FileName, strFileName);
            }
        }
        /// <summary>      
        /// Author:         Marco Abejar Gad
        /// Modifed Date:   26/April/2013
        /// Description:    Addd coulms and format employee id column as number
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName)
        {
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 29;
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
                xtwWriter.WriteAttributeString("ss", "Name", null, "Same Day ArrDep");

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
                int iFirstCol = 0;
                foreach (DataColumn Header in dtSource.Columns)
                {
                    if (iFirstCol == 0)
                    {
                        iFirstCol = 1;
                    }
                    else
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
                }
                xtwWriter.WriteEndElement();


                // Run through all rows of data source                
                foreach (DataRow row in dtSource.Rows)
                {
                    // <Row>

                    xtwWriter.WriteStartElement("Row");

                    iFirstCol = 0;
                    i = 1;
                    // Run through all cell of current rows
                    foreach (object cellValue in row.ItemArray)
                    {
                        if (iFirstCol == 0)
                        {
                            iFirstCol = 1;
                        }
                        else
                        {
                            if (i <= iColCount)
                            {
                                // <Cell>
                                xtwWriter.WriteStartElement("Cell");

                                // <Data ss:Type="String">xxx</Data>
                                xtwWriter.WriteStartElement("Data");

                                if (dtSource.Columns[i].Caption.ToUpper() == "EMPLOYEE ID" ||                                    
                                    dtSource.Columns[i].Caption.ToUpper() == "E1 TRAVELREQUEST"  )                                 
                                    //dtSource.Columns[i].Caption.ToUpper() == "DEPT TIME" ||
                                    //dtSource.Columns[i].Caption.ToUpper() == "ARVL TIME" ||
                                    //dtSource.Columns[i].Caption.ToUpper() == "FLIGHTNO" )
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                else
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                }                               
                                // Write content of cell
                                xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));

                                // </Data>
                                xtwWriter.WriteEndElement();

                                // </Cell>
                                xtwWriter.WriteEndElement();
                            }
                            i++;
                        }
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
        /// Description:    open file via javascript
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            //Response.Redirect("~/Extract/ArrDepSameOnOffDt/" + strFileName, true);

            string strScript = "CloseModal('../Extract/ArrDepSameOnOffDt/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnSaveToExcel, this.GetType(), "CloseModal", strScript, true);
        }

        protected string InitializeValues()
        {
            string sUserName = GlobalCode.Field2String(Session["UserName"]);
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
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(sUserName);
            }

            uoHiddenFieldUserRole.Value = GlobalCode.Field2String(Session["UserRole"]);//MUser.GetUserRole();
            uoHiddenFieldUser.Value = sUserName;//MUser.GetUserName();                

            return uoHiddenFieldUserRole.Value;

            //string sUserName = "";
            //if (GlobalCode.Field2String(Session["UserName"]) == "")
            //{
            //    sUserName = MUser.GetUserName();
            //    Session["UserName"] = sUserName;
            //}

            //MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            //if (muser == null)
            //{
            //    Response.Redirect("~/Login.aspx", false);
            //}
            //else
            //{
            //    if (muser.IsOnline == false)
            //    {
            //        Response.Redirect("~/Login.aspx", false);
            //    }
            //}            
        }

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
                    //Session["HotelDashboardDTO_RegionList"] = HotelDashboardDTO.RegionList;
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
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.Office.Interop;

using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Text;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Xml;

namespace TRAVELMART.CrewAgent
{
    public partial class Immigration : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            string userName = GlobalCode.Field2String(Session["UserName"]);

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

            if (!IsPostBack)
            {
                //Audit Trail
                string strLogDescription = "Immigration Page Viewed";
                string strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                uoHyperLinkItinerary.NavigateUrl = "../ReportViewer.aspx?report=FlightItineraryAll&uID=" + uoHiddenFieldUserId.Value;
            }

            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {
                string sStatus = GlobalCode.Field2String(Session["Status"]);
                if (sStatus != "" && sStatus != "0")
                {
                    uoRadioButtonListStatus.SelectedValue = sStatus;
                }
                DropDownAirSettings();
                GetTravelCrewTravelWithCount();
                BindSeaport();
                //BindRoute();
                //CheckAllOffSettings();

//                uoCheckBoxSelectAllOFF.Checked = false;
            }
            else
            {
                uoHiddenFieldLoadType.Value = "1";
            }

            if (uoHiddenFieldTransportation.Value != "0" || uoHiddenFieldSelectPrintItinerary.Value != "0")
            {
                //uoCheckBoxSelectAllOFF.Checked = false;
                GetTravelCrewTravelWithCount();
            }
            uoHiddenFieldTransportation.Value = "0";
            uoHiddenFieldSelectPrintItinerary.Value = "0";
        }
        protected void uoObjectDataSourceImmigration_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["LoadType"] = GlobalCode.Field2TinyInt((uoHiddenFieldLoadType.Value));
            e.InputParameters["FromDate"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
            e.InputParameters["ToDate"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value);
            e.InputParameters["UserID"] = uoHiddenFieldUser.Value;
            e.InputParameters["Role"] = uoHiddenFieldUserRole.Value;
            e.InputParameters["OrderBy"] = uoHiddenFieldOrderBy.Value;

            e.InputParameters["SeaportID"] = GlobalCode.Field2Int(Session["SeaportID"]);
            e.InputParameters["FilterByName"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["SeafarerID"] = uoTextBoxFilter.Text;

            e.InputParameters["NationalityID"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["RankID"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Status"] = uoRadioButtonListStatus.SelectedValue; //uoDropDownListStatus.SelectedValue;
            e.InputParameters["iAirLeg"] = GlobalCode.Field2TinyInt(uoDropDownListAirLeg.SelectedValue);

            //e.InputParameters["iRouteFrom"] = GlobalCode.Field2TinyInt(uoDropDownListRouteFrom.SelectedValue);
            //e.InputParameters["iRouteTo"] = GlobalCode.Field2TinyInt(uoDropDownListRouteTo.SelectedValue);
            e.InputParameters["iRouteFrom"] = GlobalCode.Field2TinyInt(0);
            e.InputParameters["iRouteTo"] = GlobalCode.Field2TinyInt(0);
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoListViewManifestPager.SetPageProperties(0, uoListViewManifestPager.PageSize, false);
            GetTravelCrewTravelWithCount();
            //CheckAllOffSettings();
        }
        protected void uoListViewHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName.ToString();
            if (e.CommandName != "")
            {
                uoListViewManifestPager.SetPageProperties(0, uoListViewManifestPager.PageSize, false);
                GetTravelCrewTravelWithCount();
                //CheckAllOffSettings();
            }
        }          
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   09/Jan/2013
        /// Description:    Update manifest in list when new vessel selected   
        /// ===============================================================
        /// Author:         Josephine Gad
        /// Modified Date:   03/May/2013
        /// Description:    Comment BindManifest, set loadtype to 0, exec GetTravelCrewTravelWithCount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoDropDownListSeaport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SeaportID"] = uoDropDownListSeaport.SelectedValue;
            //BindManifest();  
            uoHiddenFieldLoadType.Value = "1";

            uoListViewManifestPager.SetPageProperties(0, uoListViewManifestPager.PageSize, false);            
            GetTravelCrewTravelWithCount();
            //CheckAllOffSettings();
        }
        //protected void uoButtonSave_Click(object sender, EventArgs e)
        //{
        //    SaveVehicleManifest();
        //}
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   09/Jan/2013
        /// Description:    Export Manifest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {

                dt = CrewAdminBLL.GetCrewAdminManifestExport(uoHiddenFieldUser.Value);
                if (dt.Rows.Count > 0)
                {
                    CreateFile(dt);
                }
                else
                {
                    AlertMessage("No Record to Export!");
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
        /// Created Date:   07/May/2014
        /// Description:    Print Itinerary
        /// </summary>       
        //protected void uoButtonPrintItinerary_Click(object sender, EventArgs e)
        //{

        //}
        /// <summary>
        /// <summary>
        /// Date Created:   20/Dec/2013
        /// Created By:     Josephine Gad
        /// (description)   Setup Air legs upon selecting Status
        /// </summary>
        protected void uoRadioButtonListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Status"] = uoRadioButtonListStatus.SelectedValue;
            DropDownAirSettings();

            uoListViewManifestPager.SetPageProperties(0, uoListViewManifestPager.PageSize, false);
            uoHiddenFieldLoadType.Value = "0";
            GetTravelCrewTravelWithCount();
            //CheckAllOffSettings();
        }
        protected void uoDropDownListAirLeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoListViewManifestPager.SetPageProperties(0, uoListViewManifestPager.PageSize, false);
            uoHiddenFieldLoadType.Value = "0";
            GetTravelCrewTravelWithCount();
            //CheckAllOffSettings();
        }
        //protected void uoButtonSubmit_Click(object sender, EventArgs e)
        //{
        //    uoListViewManifestPager.SetPageProperties(0, uoListViewManifestPager.PageSize, false);
        //    GetTravelCrewTravelWithCount();
        //    //CheckAllOffSettings();
        //}
        //protected void uoCheckBoxNeedTransportALL_CheckedChanged(object sender, EventArgs e)
        //{
        //    CrewAdminBLL bll = new CrewAdminBLL();
        //    CheckBox chk = (CheckBox)sender;
        //    bll.TagCrewAsNeedVehicle(uoHiddenFieldUserId.Value, chk.Checked);
        //    GetTravelCrewTravelWithCount();
        //}
        #endregion
        #region Functions
        /// <summary>
        /// Date Created:   09/01/2012
        /// Created By:     Muhallidin G Wali
        /// (description)   
        protected short getDefualtLoadType()
        {
            //Get the data field value of interest for this row            
            short LoadType;

            switch (GlobalCode.Field2String(Session["UserName"]))
            {
                case "Vehicle Vendor":
                case "Hotel Vendor":
                    LoadType = 0;
                    break;
                default:
                    LoadType = 1;
                    break;

            }

            return LoadType;
        }
        protected void InitializeValues()
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
            }
            MembershipUser sUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (sUser == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!sUser.IsOnline)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }

            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

            Session["strPrevPage"] = Request.RawUrl;
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldUserId.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            uoHiddenFieldUserRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            
            uoListViewHeader.DataSource = null;
            uoListViewHeader.DataBind();
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/08/2012
        /// Description:    Call function to get the page's default values if Load Type = 0
        /// ------------------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  20/Dec/2013
        /// Description:    Add uoCheckBoxNeedTransportALL 
        /// </summary>
        private void GetTravelCrewTravelWithCount()
        {
            try
            {
                ImmigrationBLL bll = new ImmigrationBLL();
                List<ImmigrationManifestList> listImmigration = new List<ImmigrationManifestList>(); 

                //List<CrewAdminGenericClass> list = new List<CrewAdminGenericClass>();
                bll.LoadImmigrationPage(GlobalCode.Field2TinyInt(uoHiddenFieldLoadType.Value),
                    GlobalCode.Field2DateTime(uoHiddenFieldDate.Value), GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                    uoHiddenFieldUser.Value, uoHiddenFieldUserRole.Value, uoHiddenFieldOrderBy.Value, GlobalCode.Field2Int(Session["SeaportID"]),
                    uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(), uoDropDownListNationality.SelectedValue,
                    uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue, uoRadioButtonListStatus.SelectedValue,
                    GlobalCode.Field2TinyInt(uoDropDownListAirLeg.SelectedValue),
                    0,
                    0,
                    0, uoListViewManifestPager.PageSize);

                uoLabelOnCount.Text = GlobalCode.Field2Int(Session["Immigration_OnCount"]).ToString();
                uoLabelOffCount.Text = GlobalCode.Field2Int(Session["Immigration_OffCount"]).ToString();


                 //uoListViewCrewAdmin.DataSource = null;
                 uoListViewDetails.DataSourceID = "uoObjectDataSourceImmigration";
                 uoListViewDetails.DataBind();

                //CheckBox chk = (CheckBox)uoListViewCrewAdminHeader.Controls[0].FindControl("uoCheckBoxNeedTransportALL");
                //if (uoRadioButtonListStatus.SelectedValue == "0" || uoRadioButtonListStatus.SelectedValue == "ON")
                //{
                //    chk.Visible = false;
                //}
                //else
                //{
                //    chk.Visible = true;
                //}
                //ListView1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   27/08/2012
        /// Description:    Bind filters and Counts
        /// </summary>
        /// <param name="list"></param>
        //private void BindControls(List<CrewAdminGenericClass> list)
        //{
        //    uoDropDownListNationality.Items.Clear();
        //    uoDropDownListNationality.DataSource = list[0].NationalityList;
        //    uoDropDownListNationality.DataTextField = "Nationality";
        //    uoDropDownListNationality.DataValueField = "NationalityID";
        //    uoDropDownListNationality.DataBind();
        //    uoDropDownListNationality.Items.Insert(0, new ListItem("--Select Nationality--", "0"));

        //    uoDropDownListRank.Items.Clear();
        //    uoDropDownListRank.DataSource = list[0].RankList;
        //    uoDropDownListRank.DataTextField = "Rank";
        //    uoDropDownListRank.DataValueField = "RankID";
        //    uoDropDownListRank.DataBind();
        //    uoDropDownListRank.Items.Insert(0, new ListItem("--Select Rank--", "0"));

        //    uoDropDownListGender.Items.Clear();
        //    uoDropDownListGender.DataSource = list[0].GenderList;
        //    uoDropDownListGender.DataTextField = "Gender";
        //    uoDropDownListGender.DataValueField = "GenderID";
        //    uoDropDownListGender.DataBind();
        //    uoDropDownListGender.Items.Insert(0, new ListItem("--Select Gender--", "0"));

        //    uoLabelOnCount.Text = GlobalCode.Field2String(list[0].OnCount);
        //    uoLabelOffCount.Text = GlobalCode.Field2String(list[0].OffCount);
        //}
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   09/Jan/2013
        /// Description:    Bind User's Vessel
        /// </summary>
        private void BindSeaport()
        {
            List<SeaportDTO> list = new List<SeaportDTO>();
            if (Session["Immigration_Seaport"] != null)
            {
                list = (List<SeaportDTO>)Session["Immigration_Seaport"];
            }
            uoDropDownListSeaport.Items.Clear();
            uoDropDownListSeaport.DataSource = list;
            uoDropDownListSeaport.DataTextField = "SeaportNameString";
            uoDropDownListSeaport.DataValueField = "SeaportIDString";
            uoDropDownListSeaport.DataBind();
            uoDropDownListSeaport.Items.Insert(0, new ListItem("--Select Seaport--", "0"));

            if (list.Count == 1)
            {
                uoDropDownListSeaport.SelectedIndex = 1;
            }
            else
            {
                int iSeaportID = GlobalCode.Field2Int(Session["SeaportID"]);
                if (uoDropDownListSeaport.Items.FindByValue(iSeaportID.ToString()) != null)
                {
                    uoDropDownListSeaport.SelectedValue = iSeaportID.ToString();
                }
            }
            Session["SeaportID"] = uoDropDownListSeaport.SelectedValue;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   09/Jan/2013
        /// Description:    Refresh ListView
        /// </summary>
        private void BindManifest()
        {
            uoListViewDetails.DataSource = null;
            uoListViewDetails.DataSourceID = "uoObjectDataSourceCrewAdmin";            
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Created Date:   09/Jan/2013
        /// Description:    Create File for Export
        /// ============================================
        /// Author:         Marco Abejar Gad
        /// Created Date:   26/April/2013
        /// Description:    Added columns and include aie legs
        /// ============================================
        /// Modified By:    Josephine  Gad
        /// CModified Date: 02/Sep/2013
        /// Description:    Change filename to Crew Admin Manifest + date of manifest + date extracted
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            try
            {
                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/Immigration/");
                string sDateNow = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDate = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");
                string FileName = "ImmigrationManifest_" + sDate + "_" + sDateNow + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                string[] sVesselArr = uoDropDownListSeaport.SelectedItem.Text.Split("-".ToCharArray());
                CreateExcel(dt, strFileName, sVesselArr[0].ToString().TrimEnd());
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
        /// Author:         Marco Abejar Gad
        /// Modified Date:   26/April/2013
        /// Description:    Added columns and include aie legs
        /// </summary>
        public static void CreateExcel(DataTable dtSource, string strFileName, string sPort)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    int iColCount = (dtSource.Columns.Count) - 3;
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

                    //////////////////////////////////////////////////////////////////////
                    //Style for for group with border
                    xtwWriter.WriteStartElement("Style");
                    //<Style ss:ID="s63">
                    xtwWriter.WriteAttributeString("ss", "ID", null, "s63");

                    xtwWriter.WriteStartElement("Alignment");
                    xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                    xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                    xtwWriter.WriteEndElement(); //End of Alignment
                    xtwWriter.WriteStartElement("Borders");
                    xtwWriter.WriteStartElement("Border");
                    xtwWriter.WriteAttributeString("ss", "Position", null, "Top");
                    xtwWriter.WriteAttributeString("ss", "LineStyle", null, "Continuous");
                    xtwWriter.WriteAttributeString("ss", "Weight", null, "1");
                    xtwWriter.WriteEndElement(); //End of Borders
                    xtwWriter.WriteEndElement(); // End of Border

                    //End Style for group with border
                    xtwWriter.WriteEndElement();
                    ////////////////////////////////////////////////////////////////////////

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
                    // </Alignment>
                    xtwWriter.WriteEndElement();
                    // </Style>
                    xtwWriter.WriteEndElement();
                    

                    // </Styles>
                    xtwWriter.WriteEndElement();

                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    xtwWriter.WriteAttributeString("ss", "Name", null, "Seaport_" + sPort);

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
                            if (Header.ColumnName.ToUpper() != "ISVISIBLE")
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
                        //// <Row>
                        //xtwWriter.WriteStartElement("Row");

                        //i = 1;
                        //// Run through all cell of current rows
                        //foreach (object cellValue in row.ItemArray)
                        //{
                        //    if (i <= iColCount)
                        //    {
                        //        // <Cell>
                        //        xtwWriter.WriteStartElement("Cell");

                        //        // <Data ss:Type="String">xxx</Data>
                        //        xtwWriter.WriteStartElement("Data");
                        //        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                        //        // Write content of cell                           
                        //        if (dtSource.Columns[i - 1].Caption.ToUpper() == "AIRSEQUENCE" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "FROM CITY" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "TO CITY" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT DATE" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT TIME" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL DATE" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL TIME" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "CARRIER" ||
                        //                   dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHTNO")
                        //        {
                        //            // Write content of cell
                        //            xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                        //        }
                        //        else
                        //        {
                        //            xtwWriter.WriteValue("");
                        //        }

                        //        // </Data>
                        //        xtwWriter.WriteEndElement();

                        //        // </Cell>
                        //        xtwWriter.WriteEndElement();
                        //    }
                        //    i++;
                        //}
                        //// </Row>
                        //xtwWriter.WriteEndElement();

                        bool bGroup = false;
                        bGroup = GlobalCode.Field2Bool(row["IsVisible"]);

                        // <Row>
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s65");
                        i = 1;

                        //sEmployeeName = row["Name"].ToString();

                        // Run through all cell of current rows

                        foreach (object cellValue in row.ItemArray)
                        {
                            if (dtSource.Columns[i - 1].Caption.ToUpper() != "ISVISIBLE")
                            {
                                if (bGroup)
                                {
                                    if (i <= iColCount)
                                    {

                                        // <Cell>
                                        xtwWriter.WriteStartElement("Cell");
                                        //Border
                                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s63");                             
                                        // <Data ss:Type="String">xxx</Data>
                                        xtwWriter.WriteStartElement("Data");

                                        if (dtSource.Columns[i - 1].Caption.ToUpper() == "E1ID" ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "HOTEL NITES" ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "ROOM TYPE" ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "MEAL ALLOWANCE" ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHT NO." ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT TIME" ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL TIME" ||
                                           dtSource.Columns[i - 1].Caption.ToUpper() == "AIR SEQUENCE")
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else if  (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                        {
                                            if (GlobalCode.Field2Int(cellValue) > 0)
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
                                        xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));

                                        // </Data>
                                        xtwWriter.WriteEndElement();

                                        // </Cell>
                                        xtwWriter.WriteEndElement();
                                    }
                                }
                                else
                                {
                                    if (i <= iColCount)
                                    {
                                        // <Cell>
                                        xtwWriter.WriteStartElement("Cell");

                                        // <Data ss:Type="String">xxx</Data>
                                        xtwWriter.WriteStartElement("Data");

                                        if (
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHT NO." ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT TIME" ||
                                            //dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL TIME" ||
                                          dtSource.Columns[i - 1].Caption.ToUpper() == "AIR SEQUENCE")
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                        }
                                        else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                        {
                                            if (GlobalCode.Field2Int(cellValue) > 0)
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

                                        if (dtSource.Columns[i - 1].Caption.ToUpper() == "AIR SEQUENCE" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "FROM CITY" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "TO CITY" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT DATE" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "DEPT TIME" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL DATE" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "ARVL TIME" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "AIRLINE" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "FLIGHT NO." ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "HOTEL" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "CHECKIN" ||
                                            dtSource.Columns[i - 1].Caption.ToUpper() == "CHECKOUT")
                                        {
                                            // Write content of cell
                                            xtwWriter.WriteValue(GlobalCode.Field2String(cellValue).ToUpper());
                                        }
                                        else
                                        {
                                            xtwWriter.WriteValue("");
                                        }
                                        // </Data>
                                        xtwWriter.WriteEndElement();

                                        // </Cell>
                                        xtwWriter.WriteEndElement();
                                    }
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
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/Immigration/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/Jan/2013
        /// Description:    pop up alert message
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
        string lastDateFieldValue2 = null;
        string lastClassColor = "alternateBg";
        protected string DashboardChangeRowColor()
        {
            string RowTextString = Eval("IDBigInt").ToString();
            string currentDataFieldValue = RowTextString;
            //See if there's been a change in value
            if (lastDateFieldValue2 != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDateFieldValue2 = currentDataFieldValue;
                if (lastClassColor == "")
                {
                    lastClassColor = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
                else
                {
                    lastClassColor = "";
                    return "<tr>";
                }
            }
            else
            {
                if (lastClassColor == "")
                {
                    lastClassColor = "";
                    return "<tr>";
                }
                else
                {
                    lastClassColor = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
            }
        }
        /// <summary>
        /// Date Modified:   16/Oct/2013
        /// Modified By:     Josephine Gad
        /// (description)    Save the Vehicle Manifest if visible or hidden to vendor
        /// </summary>
        //private void SaveVehicleManifest()
        //{
        //    string strLogDescription = "Show or Hide Vehicle Manifest to Vendor";
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = new DataTable();
        //        DateTime currentDate = CommonFunctions.GetCurrentDateTime();
        //        int iCount = uoListViewCre.Items.Count;
        //        DataColumn col = new DataColumn("TransactionID", typeof(Int64));
        //        dt.Columns.Add(col);
        //        col = new DataColumn("IsVisible", typeof(Boolean));
        //        dt.Columns.Add(col);
        //        DataRow row;
        //        HiddenField listUOHiddenFieldTransID;
        //        CheckBox listuoCheckBoxShow;

        //        for (int i = 0; i < iCount; i++)
        //        {
        //            listuoCheckBoxShow = (CheckBox)uoListViewCrewAdmin.Items[i].FindControl("uoCheckBoxNoVehicleNeed");

        //            if (listuoCheckBoxShow != null)
        //            {
        //                listUOHiddenFieldTransID = (HiddenField)uoListViewCrewAdmin.Items[i].FindControl("uoHiddenFieldTransID");
        //                row = dt.NewRow();
        //                row["TransactionID"] = listUOHiddenFieldTransID.Value;
        //                if (listuoCheckBoxShow.Visible == true)
        //                {
        //                    row["IsVisible"] = GlobalCode.Field2Bool(listuoCheckBoxShow.Checked);
        //                }
        //                else
        //                {
        //                    row["IsVisible"] = false;
        //                }
        //                dt.Rows.Add(row);
        //            }
        //        }
        //        VehicleManifestBLL BLL = new VehicleManifestBLL();
        //        BLL.UpdateVehicleManifestShowHide(uoHiddenFieldUser.Value, strLogDescription, "SaveVehicleManifest",
        //            Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate).ToString(), dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Modified:   22/Dec/2013
        /// Modified By:     Josephine Gad
        /// (description)    Setup value of DropDownList for Air legs
        /// </summary>
        private void DropDownAirSettings()
        {
            if (uoRadioButtonListStatus.SelectedValue == "0")
            {
                uoDropDownListAirLeg.SelectedValue = "0";
            }
            if (uoRadioButtonListStatus.SelectedValue == "ON")
            {
                uoDropDownListAirLeg.SelectedValue = "2";
            }
            if (uoRadioButtonListStatus.SelectedValue == "OFF")
            {
                uoDropDownListAirLeg.SelectedValue = "1";
            }
        }
        /// <summary>
        /// Date Modified:   08/Jan/2014
        /// Modified By:     Josephine Gad
        /// (description)    Bind Route        
        /// </summary>
        //private void BindRoute()
        //{
        //    uoDropDownListRouteFrom.Items.Clear();
        //    uoDropDownListRouteFrom.Items.Add(new ListItem("--Select Route From--", "0"));

        //    uoDropDownListRouteTo.Items.Clear();
        //    uoDropDownListRouteTo.Items.Add(new ListItem("--Select Route To--", "0"));

        //    List<VehicleRoute> list = new List<VehicleRoute>();
        //    if (Session["CrewAdminRoute"] != null)
        //    {
        //        list = (List<VehicleRoute>)Session["CrewAdminRoute"];
        //        if (list.Count > 0)
        //        {
        //            uoDropDownListRouteFrom.DataSource = list;
        //            uoDropDownListRouteFrom.DataTextField = "RouteDesc";
        //            uoDropDownListRouteFrom.DataValueField = "RouteID";

        //            uoDropDownListRouteTo.DataSource = list;
        //            uoDropDownListRouteTo.DataTextField = "RouteDesc";
        //            uoDropDownListRouteTo.DataValueField = "RouteID";
        //        }                
        //    }
        //    uoDropDownListRouteFrom.DataBind();
        //    uoDropDownListRouteTo.DataBind();
        //}
        /// <summary>
        /// Date Modified:   15/Jan/2014
        /// Modified By:     Josephine Gad
        /// (description)    settings of uoCheckBoxSelectAllOFF
        
        /// </summary>
        //private void CheckAllOffSettings()
        //{
        //    uoCheckBoxSelectAllOFF.Visible = false;
        //    if (uoRadioButtonListStatus.SelectedValue == "OFF")
        //    {
        //        if (uoListViewCrewAdmin.Items.Count > 0)
        //        {
        //            uoCheckBoxSelectAllOFF.Visible = true;
        //        }
        //    }
        //}
        #endregion       
    }
}

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
using System.ComponentModel;

namespace TRAVELMART.Vehicle
{
    public partial class PotentialSignOFF : System.Web.UI.Page
    {
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
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }



            DateTime date = GlobalCode.Field2DateTime(Request.QueryString["dDate"]);
            int vID = GlobalCode.Field2Int(Request.QueryString["VID"]);


            uoHiddenFieldDate.Value = GlobalCode.Field2String(date);
            uoHiddenFieldVendorID.Value = GlobalCode.Field2String(vID);
            uoHiddenFieldVendor.Value = GlobalCode.Field2String(Request.QueryString["sVN"]);

            VehicleManifestBLL cs = new VehicleManifestBLL();
            List<VehicleManifestList> List = new List<VehicleManifestList>();
            List = cs.GetPotentialSIGNOFF(0, vID, date);

            Session["PotenstialOFF"] = List;


            uoListViewManifestDetails.DataSource = null;
            uoListViewManifestDetails.DataBind();

            uoListViewManifestDetails.DataSource = List;
            uoListViewManifestDetails.DataBind();

            Session["strPrevPage"] = Request.RawUrl;

        }

        protected void uoButtonExport_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                GlobalCode GC = new GlobalCode();
               List<VehicleManifestList> list = (List<VehicleManifestList>)Session["PotenstialOFF"];


               var myList = (from n in list
                             select new {
                                No = "",
                                RouteFrom = n.RouteFrom.ToUpper(),
                                RouteTO = n.RouteTo.ToUpper(),
                                PickupDate = GlobalCode.Field2DateTime(n.colPickUpDate).ToShortDateString(),
                                PickupTime	= n.colPickUpTime,
                                FromCity = n.colFromVarchar.ToString(),
                                ToCity = n.colToVarchar.ToString(),
                                Status = n.colSFStatus.ToString(),
                                LastName	= n.LastName.ToUpper(),
                                FirstName  = n.FirstName.ToUpper(),
                                EmployeeID = GlobalCode.Field2Int(n.SeafarerIdInt.ToString()),
                                Gender = n.Gender.ToString(),
                                CostCenter = n.CostCenter.ToString(),
                                Title = n.RankName.ToString(),
                                Ship = n.VesselName.ToString(),
                                RecordLocator = n.RecordLocator.ToString()
                             
                             }).ToList();

               dt = GC.getDataTable(myList);
               if (dt.Rows.Count > 0)
               {
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

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   23/Dec/2013
        /// Description:    create the file to be exported
        /// ---------------------------------------------
        /// </summary>
        /// <param name="dt"></param>
        //protected void CreateFile(DataTable dt, DataTable dtConfirmed, DataTable dtCancelled)
        protected void CreateFile(DataTable dt)
        {
            try
            {

                string strFileName = "";
                string FilePath = Server.MapPath("~/Extract/VehicleManifest/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMMddyyy");

                string FileName = "VehicleManifest_" + sDateManifest + '_' + sDate + ".xls";
                strFileName = FilePath + FileName;
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
                CreateExcel(dt, strFileName, uoHiddenFieldVendor.Value);
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
        /// Author:         Muhallidin G Wali
        /// Date Created:   23/Dec/2013
        /// Description:    create the file to be exported 
        /// ---------------------------------------------
            
        public static void CreateExcel(DataTable dtSource, string strFileName, string sVehicleName)
        {
            try
            {
                // Create XMLWriter
                using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
                {
                    string sBranchName = sVehicleName;// dtSource.Rows[0]["HotelBranch"].ToString().TrimEnd();
                    int iLength = sBranchName.Length;
                    int iLengthRemove = iLength - 20;
                    if (iLength > 20)
                    {
                        sBranchName = sBranchName.Remove(20, iLengthRemove);
                    }
                    int iColCount = dtSource.Columns.Count + 1;
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

                    int i = 1;
                    int iRow = 15;

                    if (dtSource.Rows.Count > 0)
                    {
                        #region "New Manifest"

                        // <Worksheet ss:Name="xxx">
                        xtwWriter.WriteStartElement("Worksheet");
                        xtwWriter.WriteAttributeString("ss", "Name", null, sBranchName);

                        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                        xtwWriter.WriteStartElement("Table");

                        iRow = dtSource.Rows.Count + 15;

                        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

                        //Header
                        xtwWriter.WriteStartElement("Row");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");

                        foreach (DataColumn Header in dtSource.Columns)
                        {
                            if (i <= iColCount && i > 1)
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
                                if (i <= iColCount && i > 1)
                                {
                                    // <Cell>
                                    xtwWriter.WriteStartElement("Cell");

                                    // <Data ss:Type="String">xxx</Data>
                                    xtwWriter.WriteStartElement("Data");

                                    if (dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNITES" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                        dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                                    {
                                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                    }
                                    //check cost center if number or not
                                    else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                                    {
                                        if (GlobalCode.Field2Int(cellValue) > 0)
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");


                                            // Write content of cell
                                            xtwWriter.WriteValue(GlobalCode.Field2Int(cellValue));

                                        }
                                        else
                                        {
                                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                                            // Write content of cell
                                            xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                                        }
                                    }
                                    else
                                    {



                                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                                        // Write content of cell
                                        xtwWriter.WriteValue(GlobalCode.Field2String(cellValue));
                                    }

                                    // Write content of cell
                                    //xtwWriter.WriteValue(cellValue);

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

                        #endregion
                    }

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

            //string strScript = "CloseModal('../Extract/VehicleManifest/" + strFileName + "');";
            //ScriptManager.RegisterStartupScript(uoButtonExport, this.GetType(), "CloseModal", strScript, true);
            string strPath;
            //rootDir = Common.GetCUFileServerRoot().Trim();
            strPath = Server.MapPath("../Extract/VehicleManifest/" + strFileName);

            if (!string.IsNullOrEmpty(strPath))
            {


                FileInfo file = new FileInfo(strPath);
                if (file.Exists)
                {


                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);


                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";


                    Response.WriteFile(file.FullName);


                    Response.End();


                }


            }

        }

    }
}

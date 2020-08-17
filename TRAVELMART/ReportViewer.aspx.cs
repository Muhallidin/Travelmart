using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;
using TRAVELMART.Common;
using System.Configuration;

namespace TRAVELMART
{
    public partial class ReportViewer : System.Web.UI.Page
    {        
        #region "Event"
        /// <summary>
        /// Created by:     Josephine Monteza
        /// Date Created:   06/May/2014
        /// Description:    Show Report
        /// ----------------------------------------------------
        /// </summary>        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewReport();
            }
        }
        #endregion

        #region "Functions"

        /// <summary>
        /// Created by:     Josephine Monteza
        /// Date Created:   06/May/2014
        /// Description:    Show Report
        /// ----------------------------------------------------
        /// </summary>        
        private void ViewReport()
        {
            string sReport = GlobalCode.Field2String(Request.QueryString["report"]);            
            string sEmployeeID = GlobalCode.Field2Long(Request.QueryString["sfID"]).ToString();
            string sRecLoc = GlobalCode.Field2String(Request.QueryString["recLoc"]);
            string sUserID = GlobalCode.Field2String(Request.QueryString["uID"]);

             string sTitle;
            if (sRecLoc == "")
            {
                sRecLoc = " ";
            }

            // Set the processing mode for the ReportViewer to Remote
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ServerReport serverReport = ReportViewer1.ServerReport;

            string sServerURL = ConfigurationSettings.AppSettings["ReportServerURL"].ToString();
            string sReportFolder = ConfigurationSettings.AppSettings["ReportFolder"].ToString();

            
            // Set the report server URL and report path
            serverReport.ReportServerUrl = new Uri(sServerURL);
            //serverReport.ReportServerCredentials = irsc;

            //ReportViewer1.ServerReport.Refresh();

            //if (sReport == "FlightItinerary")
            //{
            //    serverReport.ReportPath = "/TravelmartReport/FlightItinerary";

            //    // Create report parameter
            //    ReportParameter paramEmployeeID = new ReportParameter();
            //    paramEmployeeID.Name = "pcolSeafarerIdInt";
            //    paramEmployeeID.Values.Add(sEmployeeID);

            //    ReportParameter paramRecLoc = new ReportParameter();
            //    paramRecLoc.Name = "pRecordLocator";
            //    paramRecLoc.Values.Add(sRecLoc);

            //    // Set the report parameters for the report
            //    ReportViewer1.ServerReport.SetParameters(
            //        new ReportParameter[] { paramEmployeeID, paramRecLoc });

            //    sTitle = "FlightItinerary_" + sEmployeeID;
            //    ExportPDF("PDF", sTitle);
            //}


            if (sReport == "FlightItineraryAll")
            {
                serverReport.ReportPath = "/" + sReportFolder + "/FlightItineraryAll";
                               

                sTitle = "FlightItineraryAll";
                ExportPDFAll("PDF", sTitle, sUserID);
            }
            else if (sReport == "Immigration")
            {

                serverReport.ReportPath = "/" + sReportFolder + "/Immigration";

                string LOE = GlobalCode.Field2String(Request.QueryString["LOEControlNo"]);  

                sTitle = "Crew Verification";
                ExportPDFCrewverification("PDF", sTitle, sUserID, LOE);

            }
            else if (sReport == "ImmigrationLOE")
            {

                string JoinDate = GlobalCode.Field2String(Request.QueryString["JoinDate"]).ToString();
                string Ship = GlobalCode.Field2String(Request.QueryString["pVessel"]);
                string Port = GlobalCode.Field2String(Request.QueryString["pport"]);


                serverReport.ReportPath = "/" + sReportFolder + "/Immigration";

                string LOE = GlobalCode.Field2String(Request.QueryString["LOEControlNo"]);

                sTitle = "Crew Verification";
                ExportPDFCrewverification("PDF", sTitle, sUserID, LOE, JoinDate, Ship , Port);


            }
        }
        /// <summary>
        /// Created by:     Josephine Monteza
        /// Date Created:   07/May/2014
        /// Description:    Export Report
        /// ----------------------------------------------------
        /// </summary>        
        public bool ExportPDFAll(string exportType, string reportsTitle, string sUserID)
        {
            try
            {
                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;
                // just gets the Report title... make your own method
                //ReportViewer needs a specific type (not always the same as the extension)

                filetype = exportType == "PDF" ? "PDF" : exportType;

                string sServerUser = ConfigurationSettings.AppSettings["ReportUser"].ToString();
                string sServerPwd = ConfigurationSettings.AppSettings["ReportPwd"].ToString();
                string sServerDomain = ConfigurationSettings.AppSettings["ReportDomain"].ToString();

                IReportServerCredentials irsc = new ReportCredentials(sServerUser, sServerPwd, sServerDomain);

                ReportViewer1.ServerReport.ReportServerCredentials = irsc;

                // Create report parameter
                ReportParameter paramUser = new ReportParameter();
                paramUser.Name = "pUserID";
                paramUser.Values.Add(sUserID);

                // Set the report parameters for the report
                ReportViewer1.ServerReport.SetParameters(
                    new ReportParameter[] { paramUser });


                byte[] bytes = ReportViewer1.ServerReport.Render(filetype, null, // deviceinfo not needed for csv
                out mimeType, out encoding, out extension, out streamIds, out warnings);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = mimeType;
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + reportsTitle + "." + exportType);
                System.Web.HttpContext.Current.Response.BinaryWrite(bytes);

                FileStream fs = new FileStream(Server.MapPath("~/Extract/CrewAdmin/" + reportsTitle + "." + exportType), FileMode.OpenOrCreate);

                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                System.Web.HttpContext.Current.Response.Flush();
                // System.Web.HttpContext.Current.Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            { 
            
            }
            return true;
        }


        /// <summary>
        /// Created by:     Josephine Monteza
        /// Date Created:   07/May/2014
        /// Description:    Export Report
        /// ----------------------------------------------------
        /// </summary>        
        public bool ExportPDFCrewverification(string exportType, string reportsTitle, string SeafareID,string LOENumber, string joindate, string Vessel, string Port)
        {
            try
            {

                List<SeafarerImage> SeafarerImage = new List<SeafarerImage>();
                MGW.QRCode.Codec.QRCodeEncoder rq = new MGW.QRCode.Codec.QRCodeEncoder();

                System.Drawing.Bitmap myImage;
                List<CrewImmigration> Immigration = new List<CrewImmigration>();

                DateTime Mydate = new DateTime();
                Mydate =GlobalCode.Field2DateTime(joindate);


                myImage = rq.Encode(SeafareID + "+" + LOENumber + "+" + Mydate.ToString("MMMM") + " " + Mydate.Day.ToString() + " " + Mydate.Year.ToString() + "+" + Vessel + "+" + Port);
                SeafarerImage.Add(new SeafarerImage
                {
                    Image = GlobalCode.Field2BitmapByte(myImage),
                    ImageType = "jpg",
                    LOENumber = LOENumber,
                    SeaparerID = GlobalCode.Field2Long(SeafareID),
                    LogInUser = GlobalCode.Field2String(Session["UserName"])
                });

                 

                TRAVELMART.BLL.ImmigrationBLL BLL = new TRAVELMART.BLL.ImmigrationBLL();
                BLL.InsertQRCode(SeafarerImage);


                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;
                // just gets the Report title... make your own method
                //ReportViewer needs a specific type (not always the same as the extension)

                filetype = exportType == "PDF" ? "PDF" : exportType;

                string sServerUser = ConfigurationSettings.AppSettings["ReportUser"].ToString();
                string sServerPwd = ConfigurationSettings.AppSettings["ReportPwd"].ToString();
                string sServerDomain = ConfigurationSettings.AppSettings["ReportDomain"].ToString();

                IReportServerCredentials irsc = new ReportCredentials(sServerUser, sServerPwd, sServerDomain);

                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ReportParameter[] ParamReport = new ReportParameter[2];
                ParamReport[0] = new ReportParameter("pSeafarerID", SeafareID);
                ParamReport[1] = new ReportParameter("pLOEControlNumber", LOENumber);

                //// Create report parameter
                //ReportParameter ParamReport = new ReportParameter();
                //ParamReport.Name = "pSeafarerID";
                //ParamReport.Values.Add(SeafareID);
                //ParamReport.Name = "pLOEControlNumber";
                //ParamReport.Values.Add(LOENumber);


                ReportViewer1.ServerReport.SetParameters(ParamReport);




                //paramUser.Values.Add(sUserID);

                // Set the report parameters for the report
                //ReportViewer1.ServerReport.SetParameters(
                //    new ReportParameter[] { ParamReport });


                byte[] bytes = ReportViewer1.ServerReport.Render(filetype, null, // deviceinfo not needed for csv
                out mimeType, out encoding, out extension, out streamIds, out warnings);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = mimeType;
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + reportsTitle + "." + exportType);
                System.Web.HttpContext.Current.Response.BinaryWrite(bytes);

                FileStream fs = new FileStream(Server.MapPath("~/Extract/Immigration/" + reportsTitle + "." + exportType), FileMode.OpenOrCreate);

                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                System.Web.HttpContext.Current.Response.Flush();
                // System.Web.HttpContext.Current.Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {

            }
            return true;
        }


        /// <summary>
        /// Created by:     Josephine Monteza
        /// Date Created:   07/May/2014
        /// Description:    Export Report
        /// ----------------------------------------------------
        /// </summary>        
        public bool ExportPDFCrewverification(string exportType, string reportsTitle, string SeafareID,string LOENumber)
        {
            try
            {
                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;
                // just gets the Report title... make your own method
                //ReportViewer needs a specific type (not always the same as the extension)

                filetype = exportType == "PDF" ? "PDF" : exportType;

                string sServerUser = ConfigurationSettings.AppSettings["ReportUser"].ToString();
                string sServerPwd = ConfigurationSettings.AppSettings["ReportPwd"].ToString();
                string sServerDomain = ConfigurationSettings.AppSettings["ReportDomain"].ToString();

                IReportServerCredentials irsc = new ReportCredentials(sServerUser, sServerPwd, sServerDomain);

                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ReportParameter[] ParamReport = new ReportParameter[2];
                ParamReport[0] = new ReportParameter("pSeafarerID", SeafareID);
                ParamReport[1] = new ReportParameter("pLOEControlNumber", LOENumber);

                //// Create report parameter
                //ReportParameter ParamReport = new ReportParameter();
                //ParamReport.Name = "pSeafarerID";
                //ParamReport.Values.Add(SeafareID);
                //ParamReport.Name = "pLOEControlNumber";
                //ParamReport.Values.Add(LOENumber);


                ReportViewer1.ServerReport.SetParameters(ParamReport);




                //paramUser.Values.Add(sUserID);

                // Set the report parameters for the report
                //ReportViewer1.ServerReport.SetParameters(
                //    new ReportParameter[] { ParamReport });


                byte[] bytes = ReportViewer1.ServerReport.Render(filetype, null, // deviceinfo not needed for csv
                out mimeType, out encoding, out extension, out streamIds, out warnings);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType = mimeType;
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + reportsTitle + "." + exportType);
                System.Web.HttpContext.Current.Response.BinaryWrite(bytes);

                FileStream fs = new FileStream(Server.MapPath("~/Extract/Immigration/" + reportsTitle + "." + exportType), FileMode.OpenOrCreate);

                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                System.Web.HttpContext.Current.Response.Flush();
                // System.Web.HttpContext.Current.Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {

            }
            return true;
        }





        #endregion
    }
}

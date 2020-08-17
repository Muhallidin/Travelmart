using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;
using System.Text;
using System.Drawing;

namespace TRAVELMART.Vehicle
{
    public partial class VehicleViewList : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    Session["UserRole"] = UserRolePrimary;
                }
                SetDefaultValues();
                GetVessel();                              
                GetNationality();
                GetGender();
                GetRank();

                //Session["strSFStatus"] = Request.QueryString["st"];
                //GlobalCode.Field2String(Session["strSFFlightDateRange"]) = Request.QueryString["dt"];
                //GetVehicleManifest(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));

                GetVehicleManifest();
                 Session["strPrevPage"]  = Request.RawUrl;
                if (Session["UserRole"]  == TravelMartVariable.RoleHotelVendor || Session["UserRole"]  == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }
            }            
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            GetVehicleManifest();            
        }

        protected void uoListViewVehicle_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoButtonSendEmail_Click(object sender, EventArgs e)
        {
            BindVehicleManifestExcel();
        }
        protected void uoGridViewVehicleManifest_DataBound(object sender, EventArgs e)
        {
            GridView grid = sender as GridView;
            if (grid != null)
            {
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");

                GridViewRow ManifestRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell ManifestCell = new TableCell();
                ManifestCell.ColumnSpan = uoGridViewVehicleManifest.Columns.Count;
                ManifestCell.Text = "Vehicle Manifest: " + SFStatus.Text;
                ManifestCell.BackColor = ColorTranslator.FromHtml("#1f497d");
                ManifestCell.ForeColor = Color.White;
                ManifestCell.Font.Bold = true;
                ManifestRow.Font.Size = 14;
                ManifestRow.Cells.Add(ManifestCell);

                Table t = grid.Controls[0] as Table;
                {
                    if (t != null)
                    {
                        t.Rows.AddAt(0, ManifestRow);
                    }
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }
        #endregion


        #region Functions
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list        
        /// </summary>
        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                VesselDataTable = VesselBLL.GetVessel(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["DateFrom"]),
                    GlobalCode.Field2String(Session["DateTo"]), GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), GlobalCode.Field2String(Session["UserRole"]));
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Vessel--", "0");
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
        /// Date Created:   28/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Get list of rank
        /// </summary>
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetRankByVessel("0");
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
        /// Date Created:   04/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle manifest                
        /// </summary>
        private void GetVehicleManifest()
        {
            DataTable VehicleManifestDataTable = null;
            try
            {
                VehicleManifestDataTable = SeafarerTravelBLL.GetSFVehicleTravelListView(
                     GlobalCode.Field2String(Session["DateFrom"]), GlobalCode.Field2String(Session["DateTo"]), GlobalCode.Field2String(Session["strPendingFilter"]),
                     uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(), GlobalCode.Field2String(Session["UserName"]),
                     uoDropDownListStatus.SelectedValue, uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                     uoDropDownListRank.SelectedValue, uoDropDownListVessel.SelectedValue, GlobalCode.Field2String(Session["Region"]),
                     GlobalCode.Field2String(Session["Country"]), GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]),
                     GlobalCode.Field2String(Session["Hotel"]), Session["Vehicle"].ToString(), GlobalCode.Field2String(Session["UserRole"]));

                //VehicleManifestDataTable = SeafarerTravelBLL.GetVehicleManifest(GlobalCode.Field2String(Session["DateFrom"]), GlobalCode.Field2String(Session["DateTo"]),
                //    Session["UserName"].ToString(), GlobalCode.Field2String(Session["strPendingFilter"]), GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                //    GlobalCode.Field2String(Session["City"]), uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                //    GlobalCode.Field2String(Session["Port"]), GlobalCode.Field2String(Session["Hotel"]), Session["Vehicle"].ToString(), uoDropDownListVessel.SelectedValue,
                //    uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue);
                
                uoListViewVehicle.DataSource = VehicleManifestDataTable;
                uoListViewVehicle.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VehicleManifestDataTable != null)
                {
                    VehicleManifestDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set default values of global variables        
        /// </summary>
        private void SetDefaultValues()
        {
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"]  = currentDate;
                Session["DateTo"]  = currentDate;
                Session["strPendingFilter"]  = "0";
                Session["Region"]  = "0";
                Session["Country"]  = "0";
                Session["City"]  = "0";
                Session["Port"]  = "0";
                Session["Hotel"]  = "0";
                Session["Vehicle"]  = "0";
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Josephine Gad
        /// (description) Date and time format        
        /// </summary>
        protected string DateFormat(object oDatetime)
        {
            Int32 DateTimeInt32 = (Int32)oDatetime;
            if (DateTimeInt32 == 1)
            {
                return "{0:dd-MMM-yyyy HHmm}";
            }
            else
            {
                return "{0:dd-MMM-yyyy}";
            }
        }

        /// <summary>
        /// Date Created:   14/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Set vehicle view  groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string VehicleAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Vessel";
            string GroupValueString = "Vessel";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

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
                return string.Format("<tr><td class=\"group\" colspan=\"13\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// Date Created:   16/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Create excel file of hotel manifest and email
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindVehicleManifestExcel()
        {
            string strLogDescription;
            string strFunction;

            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetSFVehicleTravelListView(
                     GlobalCode.Field2String(Session["DateFrom"]), GlobalCode.Field2String(Session["DateTo"]), GlobalCode.Field2String(Session["strPendingFilter"]),
                     uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(), GlobalCode.Field2String(Session["UserName"]),
                     uoDropDownListStatus.SelectedValue, uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                     uoDropDownListRank.SelectedValue, uoDropDownListVessel.SelectedValue, GlobalCode.Field2String(Session["Region"]),
                     GlobalCode.Field2String(Session["Country"]), GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]),
                     GlobalCode.Field2String(Session["Hotel"]), Session["Vehicle"].ToString(), GlobalCode.Field2String(Session["UserRole"]));

                uoGridViewVehicleManifest.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    uoGridViewVehicleManifest.DataSource = dt;
                }
                else
                {
                    uoGridViewVehicleManifest.EmptyDataText = "No Record.";
                }
                uoGridViewVehicleManifest.DataBind();


                ////auto save
                string FilePath = MapPath("~/Extract/VehicleManifest/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                FileInfo FileName = new FileInfo(FilePath + "VehicleManifest_" + sDate + ".xls");
                Response.Clear();
                Response.ClearContent();
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                uoGridViewVehicleManifest.RenderControl(htmlWrite);
                FileStream fs = new FileStream(FileName.FullName, FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
                sWriter.Write(stringWrite.ToString().Trim());
                sWriter.Close();
                fs.Close();
                //Use below line instead of Response.End() to avoid Error: Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.;  
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                ManifestSendEmail("Travelmart: Vehicle Manifest", "This is a sample Vehicle Manifest", FileName.FullName);
                AlertMessage("Email sent.");

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Vehicle manifest as .xls file was sent to e-mail.";
                strFunction = "BindVehicleManifestExcel";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //BLL.AuditTrailBLL.InsertLogAuditTrail(strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            catch (Exception ex)
            {
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                ErrorBLL.InsertError(ex.Message, ex.StackTrace.ToString(), Request.Url.AbsolutePath,
                    currentDate, CommonFunctions.GetDateTimeGMT(currentDate), GlobalCode.Field2String(Session["UserName"]));
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                uoGridViewVehicleManifest.Visible = false;
            }
        }
        /// <summary>
        /// Date Created:   16/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Send email
        /// </summary>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        /// <param name="attachment"></param>
        private void ManifestSendEmail(string sSubject, string sMessage, string attachment)
        {
            string sBody;
            DataTable dt = null;
            try
            {
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, GlobalCode.Field2String(Session["UserBranchID"]), GlobalCode.Field2String(Session["Country"]));
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
                    sBody += "</TR></TD></TABLE>";

                    CommonFunctions.SendEmailWithAttachment("", r["Email"].ToString(),"", sSubject, sBody, attachment);
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
        /// Date Created:   14/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scr", sScript, false);
        }
        #endregion        
    }
}

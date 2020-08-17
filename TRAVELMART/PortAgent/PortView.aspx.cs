using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Web;
using System.Text;
using System.Web.UI;


namespace TRAVELMART
{
    public partial class PortView : System.Web.UI.Page
    {
        #region EVENTS
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            } 
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                uoHiddenFieldRole.Value = UserRolePrimary;

                GetVessel();
                GetNationality();
                GetGender();
                GetRank();
                //GetSFPortTravelDetails();
                Session["strPrevPage"] = Request.RawUrl;
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            //GetSFPortTravelDetails();
            GetSFPortTravelDetailsWithCount();

            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Search button for port view clicked.";
            strFunction = "uoButtonSearch_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }        
        protected void uolistviewPortTravelInfoPager_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            //GetSFPortTravelDetails();
            GetSFPortTravelDetailsWithCount();

            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "View button for port view clicked.";
            strFunction = "uoButtonView_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        protected void uoObjectDataSourcePortView_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFrom"] = GlobalCode.Field2String(Session["DateFrom"]);
            e.InputParameters["DateTo"] = GlobalCode.Field2String(Session["DateTo"]);
            e.InputParameters["UserID"] = uoHiddenFieldUser.Value;
            e.InputParameters["FilterByDate"] = GlobalCode.Field2String(Session["strPendingFilter"]);
            e.InputParameters["RegionID"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["CountryID"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["CityID"] = GlobalCode.Field2String(Session["City"]);
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;
            e.InputParameters["FilterByNameID"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["FilterNameID"] = uoTextBoxFilter.Text.Trim();
            e.InputParameters["PortID"] = GlobalCode.Field2String(Session["Port"]);

            e.InputParameters["VesselID"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["Nationality"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["Rank"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Role"] = uoHiddenFieldRole.Value;

            e.InputParameters["ByVessel"] = Int16.Parse(uoHiddenFieldByVessel.Value);
            e.InputParameters["ByName"] = Int16.Parse(uoHiddenFieldByName.Value);
            e.InputParameters["ByRecLoc"] = Int16.Parse(uoHiddenFieldByRecLoc.Value);
            e.InputParameters["ByE1ID"] = Int16.Parse(uoHiddenFieldByE1ID.Value);
            e.InputParameters["ByDateOnOff"] = Int16.Parse(uoHiddenFieldByDateOnOff.Value);

            e.InputParameters["ByStatus"] = Int16.Parse(uoHiddenFieldByStatus.Value);
            e.InputParameters["ByPort"] = Int16.Parse(uoHiddenFieldByPort.Value);
            e.InputParameters["ByRank"] = Int16.Parse(uoHiddenFieldByRank.Value);
            e.InputParameters["ByPortStatus"] = Int16.Parse(uoHiddenFieldByPortStatus.Value);
            e.InputParameters["ByNationality"] = Int16.Parse(uoHiddenFieldByNationality.Value);

        }
        protected void uolistviewPortTravelInfo_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SortByRLoc")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "2";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByE1")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "2";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByName")
            {
                uoHiddenFieldByName.Value = "2";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByOnOff")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "2";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByRank")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "2";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByPort")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "2";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByStatus")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "2";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "0";
                uoHiddenFieldByNationality.Value = "0";
            }
            else if (e.CommandName == "SortByPortStatus")
            {
                uoHiddenFieldByName.Value = "0";
                uoHiddenFieldByRecLoc.Value = "0";
                uoHiddenFieldByE1ID.Value = "0";
                uoHiddenFieldByDateOnOff.Value = "0";
                uoHiddenFieldByStatus.Value = "0";
                uoHiddenFieldByPort.Value = "0";
                uoHiddenFieldByRank.Value = "0";
                uoHiddenFieldByPortStatus.Value = "2";
                uoHiddenFieldByNationality.Value = "0";
            }
            if (e.CommandName != "")
            {
                GetSFPortTravelDetailsWithCount();
            }
        }
        protected void uoButtonSendEmail_Click(object sender, EventArgs e)
        {
            BindPortManifestExcel();
        }
        protected void uoGridViewPortManifest_DataBound(object sender, EventArgs e)
        {
            GridView grid = sender as GridView;
            if (grid != null)
            {
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");

                GridViewRow ManifestRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell ManifestCell = new TableCell();
                ManifestCell.ColumnSpan = uoGridViewPortManifest.Columns.Count;
                ManifestCell.Text = "Port Manifest: " + SFStatus.Text;
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

        #region FUNCTIONS
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
                VesselDataTable = VesselBLL.GetVessel(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["DateFrom"]),
                    GlobalCode.Field2String(Session["DateTo"]), GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value);
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
        /// Date Created:   08/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Get seafarer port info
        /// ------------------------------------------------------
        /// Modified Date:  21/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Make header Onsigning/Offsigning dynamic
        /// ------------------------------------------------------
        /// Date Modified:  02/08/2011
        /// Modifed By:     Josephine Gad
        /// (description)   Add Date Range parameter
        /// ------------------------------------------------------
        /// Date Created:   05/08/2011
        /// Modifed By:     Josephine gad
        /// (description)   Add parameter seafarer's name, Close DataTable       
        /// </summary>
        private void GetSFPortTravelDetails()
        {                       
            DataTable PortDataTable = null;
            try
            {
                PortDataTable = SeafarerTravelBLL.GetSFPortTravelDetails(GlobalCode.Field2String(Session["DateFrom"]),
                    GlobalCode.Field2String(Session["DateTo"]), uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["strPendingFilter"]),
                    GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]), GlobalCode.Field2String(Session["City"]),
                    uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                    GlobalCode.Field2String(Session["Port"]), uoDropDownListVessel.SelectedValue,
                    uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                    uoDropDownListRank.SelectedValue, uoHiddenFieldRole.Value);
                
                uolistviewPortTravelInfo.DataSource = PortDataTable;
                uolistviewPortTravelInfo.DataBind();             
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer port info using datasource
        /// ------------------------------------------------------
        /// </summary>
        private void GetSFPortTravelDetailsWithCount()
        {
            try
            {
                uolistviewPortTravelInfo.DataSource = null;
                uolistviewPortTravelInfo.DataSourceID = "uoObjectDataSourcePortView";
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
        protected bool hideshow()
        {
            return User.IsInRole("Hotel Specialist");
        }
        /// <summary>
        /// Date Created:   20/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Set manifest groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string PortAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Ship";
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
                return string.Format("<tr><td class=\"group\" colspan=\"15\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>
        /// Date Created:   16/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Create excel file of hotel manifest and email
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindPortManifestExcel()
        {            
            DataTable dt = null;
            try
            {
                SeafarerTravelBLL bll = new SeafarerTravelBLL();
                dt = bll.GetSFPortTravelDetailsWithCount(GlobalCode.Field2String(Session["DateFrom"]), Session["Date"].ToString(),
                uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["strPendingFilter"]), GlobalCode.Field2String(Session["Region"]),
                GlobalCode.Field2String(Session["Country"]), GlobalCode.Field2String(Session["City"]), uoDropDownListStatus.SelectedValue,
                uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(), GlobalCode.Field2String(Session["Port"]),
                uoDropDownListVessel.SelectedValue, uoDropDownListNationality.SelectedValue,
                uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue, uoHiddenFieldRole.Value, 
                Int16.Parse(uoHiddenFieldByVessel.Value), Int16.Parse(uoHiddenFieldByName.Value), 
                Int16.Parse(uoHiddenFieldByRecLoc.Value), Int16.Parse(uoHiddenFieldByE1ID.Value), 
                Int16.Parse(uoHiddenFieldByDateOnOff.Value), Int16.Parse(uoHiddenFieldByStatus.Value), 
                Int16.Parse(uoHiddenFieldByPort.Value), Int16.Parse(uoHiddenFieldByRank.Value), 
                Int16.Parse(uoHiddenFieldByPortStatus.Value), Int16.Parse(uoHiddenFieldByNationality.Value),
                0, uolistviewPortTravelInfoPager.TotalRowCount);             

                uoGridViewPortManifest.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    uoGridViewPortManifest.DataSource = dt;
                }
                else
                {
                    uoGridViewPortManifest.EmptyDataText = "No Record.";
                }
                uoGridViewPortManifest.DataBind();


                ////auto save
                string FilePath = MapPath("~/Extract/PortManifest/");
                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                FileInfo FileName = new FileInfo(FilePath + "PortManifest_" + sDate + ".xls");
                Response.Clear();
                Response.ClearContent();
                StringWriter stringWrite = new StringWriter();
                HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                uoGridViewPortManifest.RenderControl(htmlWrite);
                FileStream fs = new FileStream(FileName.FullName, FileMode.Create);
                StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
                sWriter.Write(stringWrite.ToString().Trim());
                sWriter.Close();
                fs.Close();
                //Use below line instead of Response.End() to avoid Error: Unable to evaluate expression because the code is optimized or a native frame is on top of the call stack.;  
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                ManifestSendEmail("Travelmart: Port Manifest", "This is a sample Port Manifest", FileName.FullName);
                AlertMessage("Email sent.");
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
                uoGridViewPortManifest.Visible = false;
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
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, Session["UserBranchID"].ToString(), 
                    GlobalCode.Field2String(Session["Country"]));
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
                    sBody += "</TR></TD></TABLE>";

                    CommonFunctions.SendEmailWithAttachment("", r["Email"].ToString(), "", sSubject, sBody, attachment);
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

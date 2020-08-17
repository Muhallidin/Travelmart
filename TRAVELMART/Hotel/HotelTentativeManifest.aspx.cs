using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Web.Security;

namespace TRAVELMART.Hotel
{
    public partial class HotelTentativeManifest : System.Web.UI.Page
    {
        #region Event
        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   23/03/2012
        /// Description:    move tentative manifest to new page and new master
        /// --------------------------------------------------------------
        /// Date Modified:  23/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Refresh list view, Manifest Hrs Drop down, 
        ///                 selected date and default hotel if new date is selected
        /// -------------------------------------------
        /// Date Modified:  06/07/2012
        /// Modified By:    Jefferson Bermundo
        /// Description:    Add Region and Port filter,
        ///                 adjustment on the report view, if the hotel list for a port had only one
        ///                 Hotel, select the hotel and shows the report.
        /// -------------------------------------------
        /// Date Modified:  27/09/2012
        /// Modified By:    Josephine Gad
        /// Description:    Add 8 days and 16 days to the manifest hours selection
        /// -------------------------------------------
        /// Date Modified:  05/10/2012
        /// Modified By:    Josephine Gad
        /// Description:    Remove 8 and 16 days
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); //gelo     
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                SetDefaultValues();
                GetNationality();
                GetGender();
                GetRank();
                GetVessel();
                GetHotelFilter();

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor || uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelSpecialist ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleAdministrator ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist)
                {
                    uoButtonLock.Visible = true;
                }
                else
                {
                    uoButtonLock.Visible = false;
                }
                DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                TimeSpan timeDiff = dDateFrom - dDateCurrent;
                int dDays = timeDiff.Days;

                if (dDays <= 1)
                {
                    uoDropDownListHours.SelectedValue = "24";
                    uoDropDownListHours.Enabled = false;
                }
                else if (dDays == 2)
                {
                    uoDropDownListHours.SelectedValue = "48";
                    uoDropDownListHours.Enabled = false;
                }
                else if (dDays > 2)
                {
                    uoDropDownListHours.SelectedValue = "72";
                    uoDropDownListHours.Enabled = false;
                }
                //else if (dDays > 2 && dDays < 8)
                //{
                //    uoDropDownListHours.SelectedValue = "72";
                //    uoDropDownListHours.Enabled = false;
                //}
                //else if (dDays > 7 && dDays < 16)
                //{
                //    uoDropDownListHours.SelectedValue = "192";
                //    uoDropDownListHours.Enabled = false;
                //}
                //else if (dDays > 15)
                //{
                //    uoDropDownListHours.SelectedValue = "384";
                //    uoDropDownListHours.Enabled = false;
                //}

                string sHotel = GlobalCode.Field2String(Session["Hotel"]);
                if (sHotel != "" && sHotel != "0")
                {
                    if (uoDropDownListHotel.Items.FindByValue(sHotel) != null)
                    {
                        uoDropDownListHotel.SelectedValue = sHotel;
                    }
                }
                LoadDefaults(0);
            }
            else
            {
                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

                if (uoHiddenFieldPopupCalendar.Value == "1" || sChangeDate == "1")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                    DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
                    DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
                    TimeSpan timeDiff = dDateFrom - dDateCurrent;
                    int dDays = timeDiff.Days;

                    if (dDays <= 1)
                    {
                        uoDropDownListHours.SelectedValue = "24";
                        uoDropDownListHours.Enabled = false;
                    }
                    else if (dDays == 2)
                    {
                        uoDropDownListHours.SelectedValue = "48";
                        uoDropDownListHours.Enabled = false;
                    }
                    else if (dDays > 2)
                    {
                        uoDropDownListHours.SelectedValue = "72";
                        uoDropDownListHours.Enabled = false;
                    }
                    //else if (dDays > 2 && dDays < 8 )
                    //{
                    //    uoDropDownListHours.SelectedValue = "72";
                    //    uoDropDownListHours.Enabled = false;
                    //}
                    //else if (dDays > 7 && dDays < 16)
                    //{
                    //    uoDropDownListHours.SelectedValue = "192";
                    //    uoDropDownListHours.Enabled = false;
                    //}
                    //else if (dDays > 15)
                    //{
                    //    uoDropDownListHours.SelectedValue = "384";
                    //    uoDropDownListHours.Enabled = false;
                    //}
                    GetSFHotelTravelDetails();
                }
            }
        }

        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTravelDetails();
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
        }

        protected void uoObjectDataSourceManifest_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFromString"] = uoHiddenFieldDate.Value;
            e.InputParameters["strUser"] = uoHiddenFieldUser.Value;

            e.InputParameters["DateFilter"] = "1";
            e.InputParameters["ByNameOrID"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["filterNameOrID"] = uoTextBoxFilter.Text.Trim();

            e.InputParameters["Nationality"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["Rank"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;

            e.InputParameters["Region"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["Country"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["City"] = GlobalCode.Field2String(Session["City"]);

            e.InputParameters["Port"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["Hotel"] = uoDropDownListHotel.SelectedValue;
            e.InputParameters["Vessel"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["UserRole"] = uoHiddenFieldRole.Value;
            e.InputParameters["LoadType"] = "0";
            e.InputParameters["SortBy"] = uoHiddenFieldSortBy.Value;
        }

        protected void uolistviewHotelInfo_DataBound(object sender, EventArgs e)
        {
            ButtonLockSettings();
        }

        protected void uoButtonLock_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (uoDropDownListHotel.SelectedValue != "")
            {
                if (uoDropDownListHotel.SelectedValue != "0")
                {
                    Int32 ID = HotelManifestBLL.InsertHotelManifestLockHeader(uoDropDownListHours.SelectedValue, uoDropDownListHotel.SelectedValue,
                                                                                uoHiddenFieldUser.Value, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString());

                    //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                    strLogDescription = "Hotel tentative manifest locked.";
                    strFunction = "uoButtonLock_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    AuditTrailBLL.InsertLogAuditTrail(ID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
                    
                    uoLabelHeader.Text = "Locked By: " + Page.User.Identity.Name + " Date: " + DateTime.Now.ToString("dd-MMM-yyyy HHmm");
                    uoButtonLock.Enabled = false;
                    AlertMessage("Manifest has been locked.");
                }
                else
                {
                    AlertMessage("Select hotel.");
                }
            }
            else
            {
                AlertMessage("Select hotel.");
            }
        }

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 23/03/2012
        /// Description: Clears the filters and displays the tentative manifest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonClear_Click(object sender, EventArgs e)
        {
            uoDropDownListVessel.SelectedIndex = 0;
            uoDropDownListFilterBy.SelectedIndex = 0;
            uoDropDownListNationality.SelectedIndex = 0;
            uoDropDownListGender.SelectedIndex = 0;
            uoDropDownListRank.SelectedIndex = 0;
            uoDropDownListStatus.SelectedIndex = 0;
            uoTextBoxFilter.Text = "";

            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            GetSFHotelTravelDetails();
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = "";
            Session.Remove("Port"); // remove the current selected Port 05/07/2012
            LoadDefaults(1);
            GetHotelFilter();
        }

        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            LoadDefaults(1);
            GetHotelFilter();

        }
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            ManifestBLL MBLL = new ManifestBLL();
            List<TentativeManifestExport> TentativeExport = new List<TentativeManifestExport>();
            TentativeExport = MBLL.GetTentativeExportList(GlobalCode.Field2DateTime(Session["DateFrom"]),
                GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2Int(uoDropDownListHotel.SelectedValue));
            DataTable dt = null;
            try
            {
                if (TentativeExport.Count > 0)
                {
                    try
                    {
                        dt = getDataTable(TentativeExport);
                        CreateFile(dt);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {

                    }
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

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Feb/2013
        /// Description:    Get the order to be used
        /// -------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldSortBy.Value = e.CommandName;
            GetSFHotelTravelDetails();
            //GetOverflow();
        }
        #endregion


        #region EVENTS
        protected void InitializeValues()
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

            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = GlobalCode.Field2DateTime(Request.QueryString["dt"]).ToShortDateString();
            }
            uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            Session["strPrevPage"] = Request.RawUrl;

            ListView1.DataSource = null;
            ListView1.DataBind();
        }

        private void GetSFHotelTravelDetails()
        {
            try
            {
                uolistviewHotelInfo.DataSource = null;
                uolistviewHotelInfo.DataSourceID = "uoObjectDataSourceManifest";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetDefaultValues()
        {
            if (GlobalCode.Field2String(Session["Region"]) == "")
            {
                Session["strPendingFilter"] = "0";
                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["City"] = "0";
                Session["Port"] = "0";
                Session["Hotel"] = "0";
                Session["Vehicle"] = "0";
            }
        }

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

        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                VesselDataTable = VesselBLL.GetVessel(uoHiddenFieldUser.Value, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(),
                    GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
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
        /// Modified By:    Josephine Gad
        /// Date Modified:  09/07/2012
        /// Description:    Change DataTable to List
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  02/10/2012
        /// Description:    Add option "Select ALL Hotel" ,"-1" if there is selected Region
        /// ----------------------------------------------
        /// </summary>
        private void GetHotelFilter()
        {
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, Session["Region"].ToString(),
                    Session["Port"] == null ? "0" : Session["Port"].ToString(), "0", "0");

                int iRowCount = list.Count;
                if (iRowCount == 1)
                {
                    Session["Hotel"] = list[0].HotelIDString;//dt.Rows[0]["BranchID"].ToString();
                }
                if (iRowCount > 0)
                {
                    uoDropDownListHotel.Items.Clear();
                    uoDropDownListHotel.DataSource = list;
                    uoDropDownListHotel.DataTextField = "HotelNameString";
                    uoDropDownListHotel.DataValueField = "HotelIDString";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
                    uoDropDownListHotel.SelectedValue = "0";

                    if (GlobalCode.Field2Int(Session["Region"]) > 0)
                    {
                        if (uoDropDownListHotel.Items.FindByValue("-1") == null)
                        {
                            uoDropDownListHotel.Items.Insert(1, new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                    else
                    {
                        if (uoDropDownListHotel.Items.FindByValue("-1") != null)
                        {
                            uoDropDownListHotel.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                    RemoveDuplicateItems(uoDropDownListHotel);
                    uoDropDownListHotel.Enabled = true;

                    if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                    {
                        uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                    }
                    LoadTravelDetails();
                }
                else
                {
                    uoDropDownListHotel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public static void RemoveDuplicateItems(DropDownList ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ddl.SelectedIndex = i;
                string str = ddl.SelectedItem.ToString();
                for (int counter = i + 1; counter < ddl.Items.Count; counter++)
                {
                    ddl.SelectedIndex = counter;
                    string compareStr = ddl.SelectedItem.ToString();
                    if (str == compareStr)
                    {
                        ddl.Items.RemoveAt(counter);
                        counter = counter - 1;
                    }
                }
            }
            ddl.SelectedIndex = 0;
        }
        /// <summary>
        /// Date Created:   22/02/2011
        /// Created By:     Josephine Gad
        /// (description)   disable lock button if past dates and if there is no data
        /// -------------------------------------------------------
        /// Date Modified:  23/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   disable lock button  if already locked
        /// -------------------------------------------------------
        /// Date Modified:  02/10/2012
        /// Modified By:    Josephine Gad
        /// (description)   disable lock button  if "ALL Hotel" is selected
        /// -------------------------------------------------------
        /// </summary>
        private void ButtonLockSettings()
        {
            if (uolistviewHotelInfo.Items.Count > 0)
            {
                uoButtonLock.Enabled = true;
                uoBtnExportList.Enabled = true;
                if (uoDropDownListHotel.SelectedValue == "-1")
                {
                    uoButtonLock.Enabled = false;
                }
            }
            else
            {
                uoButtonLock.Enabled = false;
                uoBtnExportList.Enabled = false;
            }

            string sDate = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString();
            if (GlobalCode.Field2DateTime((sDate)) < DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy")))
            {
                uoButtonLock.Enabled = false;
            }
            
            bool IsAlreadyLocked = HotelManifestBLL.IsHotelHasLockedManifest(sDate, uoDropDownListHotel.SelectedValue, uoDropDownListHours.SelectedValue);
            if (IsAlreadyLocked)
            {
                uoButtonLock.Enabled = false;
            }
        }

        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel"; //"Check In";
            string GroupValueString = "HotelBranch"; //"colTimeSpanStartDate";

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
                    return string.Format("<tr><td class=\"group\" colspan=\"36\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
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

        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "scr", sScript, false);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        #endregion
       
        /// <summary>
        /// Author: Chralene Remotigue
        /// Date Created: 17/04/2012
        /// Description: convert list to datatable
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
        /// Author: Charlene Remotigue
        /// Date Created: 17/04/2012
        /// Description: get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: create the file to be exported
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt)
        {
            string strFileName = "";
            string FilePath = Server.MapPath("~/Extract/TentativeManifest/");
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string FileName = "TentativeManifest_" + sDate + ".xls";
            strFileName = FilePath + FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            CreateExcel(dt, strFileName);
            OpenExcelFile(FileName, strFileName);
        }
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  06/Aug/2013
        /// Description:    validate cost center if numeric or not
        ///                 Add style S65  to align all rows to Left
        public static void CreateExcel(DataTable dtSource, string strFileName)
        {
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 30;
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
                xtwWriter.WriteAttributeString("ss", "Name", null, "Tentative Manifest");

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

        /// <summary>
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  12/04/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/TentativeManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
        }

        /// <summary>
        /// Modified By:    Josephine gad
        /// Date Modified:  03/10/2012
        /// Description:    User BindRegionList to bind Region List
        /// </summary>
        public void LoadDefaults(short LoadType)
        {
            if (LoadType == 0)
            {
                BindRegionList();
            }
            BindPortList();
        }

        /// <summary>
        /// Date Created:   05/07/2012
        /// Created by:     Jefferson Bermundo
        /// Description:    For Filtering based on port per region
        /// ------------------------
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
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

        static DataTable ConvertListToDataTable(List<HotelDTO> list)
        {
            DataTable table = null;
            try
            {
                // New table.
                table = new DataTable();
                table.Columns.Add("BranchId");
                table.Columns.Add("BranchName");
                // Add rows.
                foreach (var array in list)
                {
                    table.Rows.Add(array.HotelIDString, array.HotelNameString);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (table != null)
                {
                    table.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   July 6, 2012
        /// Created by:     Jefferson S. Bermundo
        /// Description:    Created a class for loading the Hotel Details,
        ///                 since loading can be called by multiple events
        /// ----------------------------------------------------------------
        /// Date Modified:  27/09/2012
        /// Modified By:    Josephine Gad
        /// Description:    Add 8 days and 16 days to the manifest hours selection
        /// -------------------------------------------
        /// Date Modified:  05/10/2012
        /// Modified By:    Josephine Gad
        /// Description:    Remove 8 and 16 days
        /// -------------------------------------------
        /// Date Modified:  29/10/2012
        /// Modified By:    Jefferson Bermundo
        /// Description:    Add Label for last Manifest Locked
        /// -------------------------------------------
        /// </summary>
        private void LoadTravelDetails()
        {
            Session["Hotel"] = uoDropDownListHotel.SelectedValue;
            Session["HotelNameToSearch"] = uoDropDownListHotel.SelectedItem.Text;
            if (uoDropDownListHotel.SelectedValue == "0")
            {
                uoButtonLock.Enabled = false;
                uoBtnExportList.Enabled = false;
            }
            else
            {
                //uoButtonLock.Enabled = true;
                uoBtnExportList.Enabled = true;
            }

            if (uoDropDownListHotel.SelectedValue == "-1")
            {
                uoDropDownListPortPerRegion.SelectedValue = "0";
                Session["Port"] = "0";
                uoDropDownListPortPerRegion.Enabled = false;
                uoButtonLock.Enabled = false;
            }
            else
            {
                uoDropDownListPortPerRegion.Enabled = true;
            }

            uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
            DateTime dDateCurrent = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"));
            DateTime dDateFrom = GlobalCode.Field2DateTime((uoHiddenFieldDate.Value));
            TimeSpan timeDiff = dDateFrom - dDateCurrent;
            int dDays = timeDiff.Days;

            if (dDays <= 1)
            {
                uoDropDownListHours.SelectedValue = "24";
                uoDropDownListHours.Enabled = false;
            }
            else if (dDays == 2)
            {
                uoDropDownListHours.SelectedValue = "48";
                uoDropDownListHours.Enabled = false;
            }
            else if (dDays > 2)
            {
                uoDropDownListHours.SelectedValue = "72";
                uoDropDownListHours.Enabled = false;
            }
            //else if (dDays > 2 && dDays < 8)
            //{
            //    uoDropDownListHours.SelectedValue = "72";
            //    uoDropDownListHours.Enabled = false;
            //}
            //else if (dDays > 7 && dDays < 16)
            //{
            //    uoDropDownListHours.SelectedValue = "192";
            //    uoDropDownListHours.Enabled = false;
            //}
            //else if (dDays > 15)
            //{
            //    uoDropDownListHours.SelectedValue = "384";
            //    uoDropDownListHours.Enabled = false;
            //}
            uoLabelHeader.Text = "";
            string sLockedBy = "", sLockedDate = "";
            HotelManifestBLL.HotelLastLockedDetails(GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), uoDropDownListHotel.SelectedValue,
                    GlobalCode.Field2TinyInt(uoDropDownListHours.SelectedValue),
                    ref sLockedBy, ref sLockedDate);
            if (!string.IsNullOrEmpty(sLockedBy) || !string.IsNullOrEmpty(sLockedDate))
            {
                uoLabelHeader.Text = "Locked By: " + sLockedBy + " Date :" + sLockedDate;
            }
            GetSFHotelTravelDetails();
            Session.Remove("TentativeManifestCalendarDashboard");
            //ButtonLockSettings();
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
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
    }
}

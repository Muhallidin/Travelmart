using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Configuration;

namespace TRAVELMART
{
    public partial class TravelMartMaster2 : System.Web.UI.MasterPage
    {
        #region DEFINITIONS
        #endregion

        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 06/03/2012
        /// ----------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  31/08/2012
        /// Description:    Force logout if there is new session id for this user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {          
            InitializeValues();
            string sUserName = GlobalCode.Field2String(Session["UserName"]);
            string sLDAPSid = UserAccountBLL.GetUserSessionID_LDAP(sUserName);
            bool isAuthenticated = MUser.IsLDAPSessionValid(sUserName, sLDAPSid);
            if (!isAuthenticated)
            {
                try
                {
                    Response.Redirect("/Login.aspx", false);
                }
                catch
                {
                    Response.Redirect("../Login.aspx");
                }
            }

            if (!IsPostBack || uoHiddenFieldPopupCalendar.Value == "1")
            {
                if (MUser.GetLDAPLoginIsON() == "1")
                {
                    uoLinkButtonGoBackLDAP.Visible = true;
                }
                else
                {
                    uoLinkButtonGoBackLDAP.Visible = false;                
                }
                
                if (Request.QueryString["ufn"] != null)
                {
                    string sUserSessionID = UserAccountBLL.GetUserSessionID(GlobalCode.Field2String(Session["UserName"]));
                    if (sUserSessionID != "" && sUserSessionID != Session.SessionID)
                    {
                        var rCookie = Request.Cookies["asp.net_sessionid"];
                        if (rCookie != null)
                        {
                            HttpCookie respCookie = new HttpCookie("asp.net_sessionid", sUserSessionID);
                            respCookie.Expires = DateTime.Now.AddMinutes(-1);
                            Session["ForceLogout"] = "1";
                            Response.Redirect("/Login.aspx", false);
                        }
                    }
                    else
                    {
                        Session["ForceLogout"] = "0";
                    }

                    SetDefaultValues();
                    if (!IsPostBack)
                    {
                        SetValues();
                    }

                    string sUserRole = GlobalCode.Field2String(Session["UserRole"]); ;
                    if (sUserRole == "" && GlobalCode.Field2String(Session["HotelNameToSearch"]) == "")
                    {
                        GetUserBranchInfo();
                    }
                    else
                    {
                        if (sUserRole == TravelMartVariable.RolePortSpecialist ||
                            sUserRole == TravelMartVariable.RoleHotelVendor ||
                            sUserRole == TravelMartVariable.RoleVehicleVendor)
                        {
                            uoLabelBranchName.Text = GlobalCode.Field2String(Session["HotelNameToSearch"]);
                        }
                        else
                        {
                            uoLabelBranchName.Text = "";
                        }
                    }
                    uoHiddenFieldRole.Value = sUserRole;
                    //uoHyperlinkCalendar.HRef = "~/CalendarPopUp.aspx";
                }

                ucLabelFName.Text = Request.QueryString["ufn"];
                //string[] strUser = Regex.Split(ucLabelFName.Text, " as ");
                //string st = strUser[1].ToString();

                //if (strUser[1].ToString().Trim().Equals("Crew Admin"))
                //    uoHiddenFieldRoleManual.Value = "toc.html";
                //else if(strUser[1].ToString().Trim().Equals("Hotel Vendor"))
                //    uoHiddenFieldRoleManual.Value = "HotelVendorManual.html";


                if (uoHiddenFieldRole.Value != TravelMartVariable.RoleAdministrator &&
                     uoHiddenFieldRole.Value != TravelMartVariable.RoleHotelSpecialist)
                {
                    uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                    ucSpanViewWeek.Visible = false;
                }


            }
           
        }
        /// <summary>
        /// Date Modified:  04/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Reset membership time
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContinueWorking_Click(object sender, EventArgs e)
        {
            //Do nothing. But the Session will be refreshed as a result of 
            //this method being called, which is its entire purpose.
            string sUser = GlobalCode.Field2String(MUser.GetUserName());
            if (sUser != "")
            {
                MembershipUser mUser = Membership.GetUser(sUser);
                if (mUser != null)
                {
                    mUser.LastActivityDate = DateTime.Now;
                    Membership.UpdateUser(mUser);                    
                }                
            }
            
            //HtmlMeta meta = new HtmlMeta();
            //HtmlHead head = (HtmlHead)Page.Header;
            //meta.HttpEquiv = "refresh";
            ////meta.Content = "5";
            //head.Controls.Add(meta);
        }
        /// <summary>
        /// Date Modified:  04/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Set  mUser.LastActivityDate to -Membership.UserIsOnlineTimeWindow          
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExitWorking_Click(object sender, EventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (mUser != null)
            {
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);
            }
            FormsAuthentication.SignOut();
            try
            {                
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                try
                {
                    Response.Redirect("../Login.aspx", false);

                }
                catch
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        protected void uoIBtnNext_Click(object sender, ImageClickEventArgs e)
        {
            string sDateFrom = GlobalCode.Field2String(Session["DateFrom"]);
            string branchId = "0";
            string branchName = "";
            if (Request.QueryString["branchId"] != null)
            {
                branchId = Request.QueryString["branchId"];
            }
            if (Request.QueryString["branchName"] != null)
            {
                branchName = Request.QueryString["branchName"];
            }
            if (Session["Hotel"] != null)
            {
                Session["Hotel"] = Session["Hotel"];
            }
            DateTime dt = DateTime.Parse(sDateFrom).AddDays(1);
            uoLabelDate.Text = String.Format("{0:dd-MMM-yyyy}", dt);
            Session["DateFrom"] = dt.ToString();
            Session.Remove("OnOffCalendarDashboard");
            Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]).Remove(GlobalCode.Field2String(Session["strPrevPage"]).IndexOf('?')) + "?ufn=" 
                + Request.QueryString["ufn"] + "&dt=" + dt.ToString() + "&branchId=" + branchId + "&branchName=" + branchName + "&chDate=1");

        }

        protected void uoIBtnBack_Click(object sender, ImageClickEventArgs e)
        {
            string branchId = "0";
            string branchName = "";
            if (Request.QueryString["branchId"] != null)
            {
                branchId = Request.QueryString["branchId"];
            }
            if (Request.QueryString["branchName"] != null)
            {
                branchName = Request.QueryString["branchName"];
            }
            if (Session["Hotel"] != null)
            {
                Session["Hotel"] = Session["Hotel"];
            }
            DateTime dt = DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"])).AddDays(-1);
            uoLabelDate.Text = String.Format("{0:dd-MMM-yyyy}", dt);
            Session["DateFrom"] = dt.ToString();
            Session.Remove("OnOffCalendarDashboard");
            Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]).Remove(GlobalCode.Field2String(Session["strPrevPage"]).IndexOf('?')) + "?ufn="
                + Request.QueryString["ufn"] + "&dt=" + dt.ToString() + "&branchId=" + branchId + "&branchName=" + branchName + "&chDate=1");
        }

        protected void uoLabelDateToday_Click(object sender, EventArgs e)
        {
            string branchId = "0";
            string branchName = "";
            if (Request.QueryString["branchId"] != null)
            {
                branchId = Request.QueryString["branchId"];
            }
            if (Request.QueryString["branchName"] != null)
            {
                branchName = Request.QueryString["branchName"];
            }
            if (Session["Hotel"] != null)
            {
                Session["Hotel"] = Session["Hotel"];
            }
            DateTime dt = DateTime.Now;
            uoLabelDate.Text = String.Format("{0:dd-MMM-yyyy}", dt);
            Session["DateFrom"] = dt.ToShortDateString();
            Session.Remove("OnOffCalendarDashboard");
            Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]).Remove(GlobalCode.Field2String(Session["strPrevPage"]).IndexOf('?')) + "?ufn="
                + Request.QueryString["ufn"] + "&dt=" + dt.ToShortDateString() + "&branchId=" + branchId + "&branchName=" + branchName + "&chDate=1");
        }

        protected void uoLabelViewHotelDashboardByWeek_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HotelDashboardRoomType3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// ------------------------------
        /// Date Modified:  03/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Set  mUser.LastActivityDate to -Membership.UserIsOnlineTimeWindow         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (mUser != null)
            {
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-Membership.UserIsOnlineTimeWindow);
                Membership.UpdateUser(mUser);
            }
            FormsAuthentication.SignOut();
            try
            {
                MUser.LogoutUserInLDAP(GlobalCode.Field2String(Session["UserName"]));
                Response.Redirect("Login.aspx");                
            }
            catch (Exception ex)
            {
                try
                {
                    Response.Redirect("../Login.aspx", false);

                }
                catch
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        protected void uoLinkButtonGoBackLDAP_Click(object sender, EventArgs e)
        {
            
             string sLDAPURL = ConfigurationSettings.AppSettings["LDAP-URL-Main"].ToString();
             sLDAPURL = sLDAPURL + "index";
             Response.Redirect(sLDAPURL);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Modified By: Charlene Remotigue
        /// Date MOdified: 13/04/2012
        /// Description: save prev page url to hiddenfield
        /// </summary>
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
                try
                {
                    Response.Redirect("/Login.aspx", false);
                }
                catch
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    try
                    {
                        Response.Redirect("/Login.aspx", false);
                    }
                    catch
                    {
                        Response.Redirect("../Login.aspx");
                    }
                }
            }

            uoHiddenFieldPrevPage.Value = GlobalCode.Field2String(Session["strPrevPage"]);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: Set default values
        /// </summary>
        protected void SetDefaultValues()
        {
            DateTime dt= DateTime.Now;
            string branchId = "0";
            string branchName = "";
            if (Request.QueryString["branchId"] != null)
            {
                branchId = Request.QueryString["branchId"];
            }
            if (Request.QueryString["branchName"] != null)
            {
                branchName = Request.QueryString["branchName"];
            }

            
            string strURL = GlobalCode.Field2String(Session["strPrevPage"]).Remove(GlobalCode.Field2String(Session["strPrevPage"]).IndexOf('?'));
            //uoHyperlinkDateToday.HRef = strURL + "?ufn=" + Request.QueryString["ufn"] + "&dt=" +
            //    dt.ToString() + "&branchId=" + branchId + "&branchName=" + branchName;
            uoLabelDateToday.Text = "Today";
            uoLinkButtonViewHotelDashboardByWeek.Text = "View All";
          

            if (uoHiddenFieldPopupCalendar.Value != "1")
            {
                if (Session["DateFrom"] == null)
                {
                    if (Request.QueryString["dt"] != "")
                    {
                        Session["DateFrom"] = Request.QueryString["dt"];
                    }
                    else
                    {
                        Session["DateFrom"] = dt.ToString();
                    }
                }
                else
                {
                    string sDate = GlobalCode.Field2String(Session["DateFrom"]);
                    string[] sDateArrr;

                    if (sDate.Contains("#"))
                    {
                        sDateArrr = sDate.Split("#".ToCharArray());
                        sDate = sDateArrr[0];
                    }                    
                    dt = GlobalCode.Field2DateTime(sDate);
                }
            }
            else
            {

                dt = DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"]));
                uoHiddenFieldPopupCalendar.Value = "0";
                //Response.Redirect(strURL + "?ufn=" + Request.QueryString["ufn"] + "&dt=" + dt.ToString());
            }

            uoLabelDate.Text = String.Format("{0:dd-MMM-yyyy}", dt) + ", " + dt.DayOfWeek.ToString();
            if (GlobalCode.Field2String(Session["strPrevPage"]) == "" || GlobalCode.Field2String(Session["strPrevPage"]).Contains("Navigation"))
            {
                uoTblDate.Visible = false;
            }
            else
            {
                uoTblDate.Visible = true;
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: get user and branch info
        /// --------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  15/08/2012
        /// Description:    Get UserDateRange from UserAccountList
        ///  
        /// </summary>
        protected void GetUserBranchInfo()
        {
            string sUserName = GlobalCode.Field2String(Session["UserName"]);//MUser.GetUserName();
            string sUserRole = GlobalCode.Field2String(Session["UserRole"]);
            string sDateFrom = GlobalCode.Field2String(Session["DateFrom"]);
            if (sUserName == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }
            if (sUserRole == "")
            {
                sUserRole = UserAccountBLL.GetUserPrimaryRole(sUserName);
                Session["UserRole"] = sUserRole;
            }

            List<UserAccountList> listUser = GetUserAccountList(sUserName);
            int UserDateRange = listUser[0].iDayNo;//UserAccountBLL.GetUserDateRange(sUserName);

            DateTime dt = DateTime.Parse(sDateFrom).AddDays(UserDateRange);
            Session["DateTo"] = dt.ToString();

            if (sUserRole == TravelMartVariable.RolePortSpecialist ||
                sUserRole == TravelMartVariable.RoleHotelVendor ||
                sUserRole == TravelMartVariable.RoleVehicleVendor)
            {
                IDataReader dr = null;
                try
                {
                    dr = UserAccountBLL.GetUserBranchDetails(GlobalCode.Field2String(Session["UserName"]), sUserRole);
                    if (dr.Read())
                    {
                        uoLabelBranchName.Text = dr["BranchName"].ToString().ToUpper();
                        Session["HotelNameToSearch"] = uoLabelBranchName.Text;
                        Session["UserRoleKey"] = dr["RoleID"].ToString();
                        Session["UserBranchID"] = dr["BranchID"].ToString();
                        Session["UserCountry"] = dr["CountryID"].ToString();
                        Session["UserCity"] = dr["CityID"].ToString();
                        Session["UserVendor"] = dr["VendorID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  23/03/2012
        /// Description:    check session if null before set its default value
        /// </summary>
        public void SetValues()
        {
            if (Session["Region"] == null)
            {
                Session["Region"] = "0";
            }
            if (Session["Country"] == null)
            {
                Session["Country"] = "0";
            }
            if (Session["City"] == null)
            {
                Session["City"] = "0";
            }
            if (Session["Port"] == null)
            {
                Session["Port"] = "0";
            }
            if (Session["Hotel"] == null)
            {
                Session["Hotel"] = "0";
            }
            if (Session["ViewRegion"] == null)
            {
                Session["ViewRegion"] = "1";
            }
            if (Session["ViewCountry"] == null)
            {
                Session["ViewCountry"] = "1";
            }
            if (Session["ViewCity"] == null)
            {
                Session["ViewCity"] = "1";
            }
            if (Session["ViewHotel"] == null)
            {
                Session["ViewHotel"] = "1";
            }
            if (Session["ViewPort"] == null)
            {
                Session["ViewPort"] = "1";
            }
            if (Session["ViewLegend"] == null)
            {
                Session["ViewLegend"] = "1";
            }
            if (Session["ViewFilter"] == null)
            {
                Session["ViewFilter"] = "0";
            }
            if (Session["ViewDashboard"] == null)
            {
                Session["ViewDashboard"] = "0";
            }
            if (Session["ViewDashboard2"] == null)
            {
                Session["ViewDashboard2"] = "0";
            }
            if (Session["UserBranchID"] == null)
            {
                Session["UserBranchID"] = "";
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Get user details using session
        /// </summary>
        /// <returns></returns>
        private List<UserAccountList> GetUserAccountList(string sUserName)
        {
            List<UserAccountList> list = new List<UserAccountList>();

            if (Session["UserAccountList"] != null)
            {
                list = (List<UserAccountList>)Session["UserAccountList"];
            }
            else
            {
                list = UserAccountBLL.GetUserInfoListByName(sUserName);
                Session["UserAccountList"] = list;
            }
            return list;
        }
        #endregion
       
        protected void uoHomeLink_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/HotelDashboardRoomType5.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + GlobalCode.Field2String(Session["DateFrom"]), false);                                
            }
            catch(Exception ex)
            {
                Response.Redirect("../HotelDashboardRoomType5.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + GlobalCode.Field2String(Session["DateFrom"]));
                
            }
        }

        protected void uoLinkExport_Click(object sender, EventArgs e)
        {
            DataSet ds = ManifestBLL.getAllDataFiles(GlobalCode.Field2DateTime(Session["DateFrom"]), GlobalCode.Field2String(Session["UserName"]));
            string FilePath = Server.MapPath("~/Extract/Manifest/");
            string mDate = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string FileName = "Manifest_" + mDate+ "_" + sDate + ".xls";

            ds.Tables[0].TableName = "Same day Arrival Departure";
            ds.Tables[1].TableName = "No Travel request";
            int count = 2;
            while (count < ds.Tables.Count)
            {
                ds.Tables[count].TableName = ds.Tables[count].Rows[0]["HotelName"].ToString();
                count += 1;
            }
            string strFileName = FilePath +FileName;
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            Convert(ds, strFileName);
            OpenExcelFile(FileName);
        }       
        public void Convert(DataSet ds, string fileName)
        {
            Convert(ds.Tables, fileName);
            
        }

        public void Convert(IEnumerable tables, string fileName)
        {
            using (XmlTextWriter x = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                int sheetNumber = 0;
                x.WriteRaw("<?xml version=\"1.0\"?><?mso-application progid=\"Excel.Sheet\"?>");
                x.WriteRaw("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
                x.WriteRaw("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
                x.WriteRaw("xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                //x.WriteRaw("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");

                //x.WriteRaw("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                x.WriteRaw("<Styles><Style ss:ID='sText'>" +
                           "<NumberFormat ss:Format='@'/></Style>");
                x.WriteRaw("<Style ss:ID='sDate'><NumberFormat" +
                           " ss:Format='[$-409]m/d/yy\\ h:mm\\ AM/PM;@'/>");
                x.WriteRaw("</Style></Styles>");
                foreach (DataTable dt in tables)
                {
                    sheetNumber++;
                    string sheetName = !string.IsNullOrEmpty(dt.TableName) ?
                           dt.TableName : "Sheet" + sheetNumber.ToString();
                    x.WriteRaw("<Worksheet ss:Name='" + sheetName + "'>");
                    x.WriteRaw("<Table>");
                    string[] columnTypes = new string[dt.Columns.Count];

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string colType = dt.Columns[i].DataType.ToString().ToLower();

                        if (colType.Contains("datetime"))
                        {
                            columnTypes[i] = "DateTime";
                            x.WriteRaw("<Column ss:StyleID='sDate'/>");

                        }
                        else if (colType.Contains("string"))
                        {
                            columnTypes[i] = "String";
                            x.WriteRaw("<Column ss:StyleID='sText'/>");

                        }
                        else
                        {
                            x.WriteRaw("<Column />");

                            if (colType.Contains("boolean"))
                            {
                                columnTypes[i] = "Boolean";
                            }
                            else
                            {
                                //default is some kind of number.
                                columnTypes[i] = "Number";
                            }

                        }
                    }
                    //column headers
                    x.WriteRaw("<Row>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName != "HotelName")
                        {
                            x.WriteRaw("<Cell ss:StyleID='sText'><Data ss:Type='String'>");
                            x.WriteRaw(col.ColumnName);
                            x.WriteRaw("</Data></Cell>");
                        }
                    }
                    x.WriteRaw("</Row>");
                    //data
                    bool missedNullColumn = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        x.WriteRaw("<Row>");
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (row[i].ToString() != dt.TableName)
                            {
                                if (!row.IsNull(i))
                                {

                                    if (missedNullColumn)
                                    {
                                        int displayIndex = i + 1;
                                        x.WriteRaw("<Cell ss:Index='" + displayIndex.ToString() +
                                                   "'><Data ss:Type='" +
                                                   columnTypes[i] + "'>");
                                        missedNullColumn = false;
                                    }
                                    else
                                    {
                                        x.WriteRaw("<Cell><Data ss:Type='" +
                                                   columnTypes[i] + "'>");
                                    }

                                    switch (columnTypes[i])
                                    {
                                        case "DateTime":
                                            x.WriteRaw(((DateTime)row[i]).ToString("s"));
                                            break;
                                        case "Boolean":
                                            x.WriteRaw(((bool)row[i]) ? "1" : "0");
                                            break;
                                        case "String":
                                            x.WriteString(row[i].ToString());
                                            break;
                                        default:
                                            x.WriteString(row[i].ToString());
                                            break;
                                    }

                                    x.WriteRaw("</Data></Cell>");
                                }
                                else
                                {
                                    missedNullColumn = true;
                                }
                            }
                        }
                        x.WriteRaw("</Row>");
                    }
                    x.WriteRaw("</Table></Worksheet>");
                }
                x.WriteRaw("</Workbook>");
                x.Flush();
                x.Close();
            }
            
        }

        public void OpenExcelFile(string strFileName)
        {
            //Response.Redirect("~/Extract/NoTravelRequest/" + strFileName, true);

            string strScript = "CloseModal('../Extract/Manifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoLinkExport, this.GetType(), "CloseModal", strScript, true);
        }
       

    }
}

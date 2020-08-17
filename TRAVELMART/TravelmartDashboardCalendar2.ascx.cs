using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;

namespace TRAVELMART
{
    public partial class TravelmartDashboardCalendar2 : System.Web.UI.UserControl
    {
        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["ViewLeftMenu"]) == "1")
            {
                BindCalendar();
            }
        }
        #endregion

        #region "Functions"
        /// <summary>        
        /// Date Created:  09/12/2011
        /// Created By:    Josephine Gad
        /// description    Create calendar
        /// ----------------------------------------------
        /// Date Modified:  14/12/2011
        /// Modified By:    Muhallidin G Wali
        /// (description)   Optimized calendar binding
        /// ----------------------------------------------
        /// Date Modified:  27/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Optimization (close IDataReader Res)
        /// ----------------------------------------------
        /// </summary>
        private void BindCalendar()
        {
            bool boolBindAgain = false;
            IDataReader Res = null;
            try
            {
                string CalendarString = "";
                string CalendarAsterisk;
                string sID;

                DateTime dSelectedDate = DateTime.Parse(GlobalCode.Field2String(Session["DateFrom"]));
                string sMonthYear = dSelectedDate.ToString("MMMM yyyy");
                string sUser = "";
                if (Request.QueryString["ufn"].ToString() != null)
                {
                    sUser = Request.QueryString["ufn"].ToString();
                }


                string sSelectedDate = "";

                //string sURL = Request.Url.AbsolutePath;
                string sURL = "/Manifest.aspx";

                if (Cache["CalendarDashboard"] == null)
                {
                    CalendarString = "<table id=\"uoCalendarDashboard\" cellspacing=\"0\" cellpadding=\"2\" title=\"Calendar\" border=\"0\" ";
                    CalendarString += " style=\"width:300px;height:190px;font-size:9pt;font-family:Verdana;color:Black;border-width:1px;border-style:solid;border-color:White;background-color:#ECDFC4;border-collapse:collapse;\"> ";
                    //First row of calendar
                    CalendarString += " <tr> <td colspan=\"7\" style=\"background-color:White;border-color:Black;border-width:4px;border-style:solid;\"> ";
                    CalendarString += " <table cellspacing=\"0\" border=\"0\" style=\"color:#333399;font-family:Verdana;font-size:12pt;font-weight:bold;width:100%;border-collapse:collapse;\"> ";
                    CalendarString += " <tr> ";
                    CalendarString += " 	<td valign=\"bottom\" style=\"color:#333333;font-size:8pt;font-weight:bold;width:15%;\"> ";
                    //CalendarString += "		    <a href=\"" + Request.Url.AbsolutePath + "?ufn=" + sUser + "&mt=" + dSelectedDate.AddMonths(-1).Month.ToString() + "\" style=\"color:#333333\" title=\"Go to the previous month\"> " + sPrevMonth + " </a> </td> ";
                    //CalendarString += 		         sPrevMonth + "</td> ";
                    CalendarString += "     </td> ";
                    CalendarString += "     <td align=\"center\" style=\"width:70%;\">" + sMonthYear + " </td> ";
                    CalendarString += "     <td align=\"right\" valign=\"bottom\" style=\"color:#333333;font-size:8pt;font-weight:bold;width:15%;\"> ";
                    //CalendarString += " 		<a href=\"" + Request.Url.AbsolutePath + "?ufn=" + sUser + "&mt=" + dSelectedDate.AddMonths(1).Month.ToString() + "\" style=\"color:#333333\" title=\"Go to the next month\">" + sNextMonth + "</a> </td> ";
                    //CalendarString +=                sNextMonth + "</td> ";
                    CalendarString += "     </td> ";
                    CalendarString += " </tr> </table> </td> </tr> ";

                    //Second row of calendar (Header)
                    CalendarString += " <tr> <th align=\"center\" abbr=\"Sunday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Sun</th> ";
                    CalendarString += "      <th align=\"center\" abbr=\"Monday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Mon</th> ";
                    CalendarString += "      <th align=\"center\" abbr=\"Tuesday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Tue</th> ";
                    CalendarString += "      <th align=\"center\" abbr=\"Wednesday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Wed</th> ";
                    CalendarString += "      <th align=\"center\" abbr=\"Thursday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Thu</th> ";
                    CalendarString += "      <th align=\"center\" abbr=\"Friday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Fri</th> ";
                    CalendarString += "      <th align=\"center\" abbr=\"Saturday\" scope=\"col\" style=\"font-size:8pt;font-weight:bold;\">Sat</th> </tr> ";



                    Res = SeafarerTravelBLL.GetTravelRequestDashboard(0, dSelectedDate, MUser.GetUserName(), GlobalCode.Field2String(Session["UserRole"]));
                    List<DashboardCounter> DBCountet = new List<DashboardCounter>();

                    while (Res.Read())
                    {
                        DBCountet.Add(new DashboardCounter() { DataOnOff = DateTime.Parse(Res["DataOnOff"].ToString()), DateOnOffName = Res["DateOnOffName"].ToString(), SINGON = Res["SIGNON"].ToString(), SINGOFF = Res["SIGNOFF"].ToString() });
                    }

                    DateTime dDay = DateTime.Now;

                    int count = 0;

                    for (int iRow = 1; iRow <= 6; iRow++)
                    {
                        CalendarString += " <tr> ";
                        for (int iCol = 1; iCol <= 7; iCol++)
                        {
                            //add asterisk if there is ON or OFF crew
                            CalendarAsterisk = "";

                            if (DBCountet.Count > 0)
                            {
                                dDay = GlobalCode.Field2DateTime(DBCountet[count].DataOnOff);
                                if (DBCountet[count].SINGON != "0")
                                {
                                    CalendarAsterisk += " <span style=\"color:Green;\">*</span> ";
                                }
                                if (DBCountet[count].SINGOFF != "0")
                                {
                                    CalendarAsterisk += " <span style=\"color:Red;\">*</span> ";
                                }
                            }

                            sID = " id=\"" + dDay.ToString("MM_dd_yyyy") + "\"";
                            if (CalendarAsterisk.Trim() != "")
                            {
                                sID += " class=\"hoverDate\" ";
                            }

                            if (dDay.Month != dSelectedDate.Month)
                            {
                                CalendarString += "<td " + sID + " align=\"left\" style=\"font-size:8.5pt; color:#999999;border-color:#666699;border-width:1px;border-style:solid;width:14%;\"> ";
                                CalendarString += "     <a href=\"" + sURL + "?ufn=" + sUser + "&dt=" + dDay.ToString("MM_dd_yyyy") + "\" style=\"color:#999999\" title=\"" + dDay.ToString("MMMM dd") + "\">" + dDay.ToString("dd") + "</a> ";
                            }
                            else
                            {
                                if (dDay == dSelectedDate)
                                {
                                    //make style bold if selected
                                    CalendarString += " <td " + sID + " align=\"left\" style=\"white-space: nowrap;font-size:8.5pt; color:Black;background-color:#FFFFCC;border-color:#666699;border-width:1px;border-style:solid;font-weight:bold;width:14%;\"> ";
                                    sSelectedDate = dDay.ToString("MM/dd/yyyy") + ",<td " + sID + " align=\"left\" style=\"white-space: nowrap;font-size:8.5pt; color:Black;background-color:#FFFFCC;border-color:#666699;border-width:1px;border-style:solid;font-weight:bold;width:14%;\">";
                                }
                                else
                                {
                                    CalendarString += " <td " + sID + " align=\"left\" style=\"font-size:8.5pt; border-color:#666699;border-width:1px;border-style:solid;width:14%;\"> ";
                                }
                                CalendarString += "     <a href=\"" + sURL + "?ufn=" + sUser + "&dt=" + dDay.ToString("MM_dd_yyyy") + "\" style=\"color:Black\" title=\"" + dDay.ToString("MMMM dd") + "\"> " + dDay.ToString("dd") + " </a> ";
                            }

                            //add asterisk if there is ON or OFF crew

                            CalendarString += CalendarAsterisk;
                            CalendarString += " </td> ";

                            count += 1;
                        }

                        CalendarString += " </tr> ";

                    }
                    CalendarString += "</table>";

                    //Store calendar string in Cache for 5 minutes
                    Cache.Insert("CalendarDashboard", CalendarString, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(300));
                    Cache.Insert("CalendarSelectedDate", sSelectedDate, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(300));

                }
                else
                {

                    sSelectedDate = Cache["CalendarSelectedDate"].ToString();
                    string[] sSelectedArray = sSelectedDate.Split(",".ToCharArray());
                    if (sSelectedArray[0].ToString() != dSelectedDate.ToString("MM/dd/yyyy"))
                    {
                        DateTime dDay = DateTime.Parse(sSelectedArray[0]);

                        if (dDay.Month != dSelectedDate.Month)
                        {
                            Cache.Remove("CalendarDashboard");
                            boolBindAgain = true;
                            return;
                        }

                        CalendarString = Cache["CalendarDashboard"].ToString();
                        string sSelectedOldStyle;
                        string sSelectedNewStyle;
                        string sSelectedDateNew;

                        string sOld;
                        string sIDOld;
                        string sIDNew;
                        //add asterisk if there is ON or OFF crew for OLD Selected
                        CalendarAsterisk = GetDateCount(dDay);
                        sIDOld = " id=\"" + dDay.ToString("MM_dd_yyyy") + "\"";
                        if (CalendarAsterisk.Trim() != "")
                        {
                            sIDOld += " class=\"hoverDate\" ";
                        }

                        //add asterisk if there is ON or OFF crew for NEW Selected
                        //CalendarAsterisk = GetDateCount(dSelectedDate);

                        sIDNew = " id=\"" + dSelectedDate.ToString("MM_dd_yyyy") + "\"";
                        if (CalendarAsterisk.Trim() != "")
                        {
                            sIDNew += " class=\"hoverDate\" ";
                        }

                        sOld = "<td " + sIDOld + " align=\"left\" style=\"font-size:8.5pt; color:#999999;border-color:#666699;border-width:1px;border-style:solid;width:14%;\"> ";
                        CalendarString = CalendarString.Replace(sSelectedArray[1], sOld);

                        if (dDay.Month != dSelectedDate.Month)
                        {
                            //style of new selected date
                            sSelectedOldStyle = "<td " + sIDNew + " align=\"left\" style=\"font-size:8.5pt; color:#999999;border-color:#666699;border-width:1px;border-style:solid;width:14%;\">";
                            sSelectedNewStyle = "<td " + sIDNew + " align=\"left\" style=\"white-space: nowrap;font-size:8.5pt; color:Black;background-color:#FFFFCC;border-color:#666699;border-width:1px;border-style:solid;font-weight:bold;width:14%;\">";
                            CalendarString = CalendarString.Replace(sSelectedOldStyle, sSelectedNewStyle);
                        }
                        else
                        {
                            //style of new selected date
                            sSelectedOldStyle = "<td " + sIDNew + " align=\"left\" style=\"font-size:8.5pt; border-color:#666699;border-width:1px;border-style:solid;width:14%;\">";
                            sSelectedNewStyle = "<td " + sIDNew + " align=\"left\" style=\"white-space: nowrap;font-size:8.5pt; color:Black;background-color:#FFFFCC;border-color:#666699;border-width:1px;border-style:solid;font-weight:bold;width:14%;\">";
                            CalendarString = CalendarString.Replace(sSelectedOldStyle, sSelectedNewStyle);
                        }
                        sSelectedDateNew = dSelectedDate.ToString("MM/dd/yyyy") + ",<td " + sIDNew + " align=\"left\" style=\"white-space: nowrap;font-size:8.5pt; color:Black;background-color:#FFFFCC;border-color:#666699;border-width:1px;border-style:solid;font-weight:bold;width:14%;\">";
                        Cache["CalendarDashboard"] = CalendarString;
                        Cache["CalendarSelectedDate"] = sSelectedDateNew;
                    }
                }
                CalendarString = Cache["CalendarDashboard"].ToString();
                ucLiteralCalendar.Text = CalendarString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (boolBindAgain)
                {
                    BindCalendar();
                }
                if (Res != null)
                {
                    Res.Close();
                    Res.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:  12/12/2011
        /// Created By:    Muhallidin G Wali
        /// description    Chech if input text are DateTime                  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        DateTime FieldTodateTime(object sender)
        {
            try
            {
                DateTime Test = (DateTime)sender;

                if (Test != null)
                    return Test;
                else
                    return DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
        }
        /// <summary>        
        /// Date Created:  09/12/2011
        /// Created By:    Josephine Gad
        /// description    Get last Sunday of the month
        /// </summary>
        private DateTime LastSundayInMonth(DateTime dDate)
        {
            DateTime dtLastSundayInMonth = new DateTime(dDate.Year, dDate.Month, 1).AddMonths(1).AddDays(-1);
            while (dtLastSundayInMonth.DayOfWeek != DayOfWeek.Sunday)
            {
                dtLastSundayInMonth = dtLastSundayInMonth.AddDays(-1);
            }
            return dtLastSundayInMonth;
        }
        /// <summary>        
        /// Date Created:  09/12/2011
        /// Created By:    Josephine Gad
        /// description    Get first Sunday of the month
        /// </summary>
        DateTime FirstSaturdayInMonth(DateTime dStart)
        {
            DateTime dtFirstSaturdayInMonth = new DateTime(dStart.Year, dStart.Month, 1);
            while (dtFirstSaturdayInMonth.DayOfWeek != DayOfWeek.Saturday)
            {
                dtFirstSaturdayInMonth = dtFirstSaturdayInMonth.AddDays(1);
            }
            return dtFirstSaturdayInMonth;
        }

        /// <summary>        
        /// Date Created:  12/12/2011
        /// Created By:    Muhallidin G Wali
        /// description    Use load time               
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private string GetDateCount(DateTime day)
        {
            IDataReader dt = null;
            string sReturn = "";
            try
            {

                dt = SeafarerTravelBLL.GetTravelRequestDashboard(1, GlobalCode.Field2DateTime(day.ToString("MM/dd/yyyy")), MUser.GetUserName(), GlobalCode.Field2String(Session["UserRole"]));

                if (dt.Read())
                {
                    if (dt["SIGNON"].ToString() != "0")
                    {
                        sReturn += " <span style=\"color:Green;\">*</span> ";
                    }
                    if (dt["SIGNOFF"].ToString() != "0")
                    {
                        sReturn += " <span style=\"color:Red;\">*</span> ";
                    }
                }
                return sReturn;
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
    }
}
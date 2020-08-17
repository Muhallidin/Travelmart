using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class CalendarBLL
    {
        CalendarDAL calendar = new CalendarDAL();
        /// <summary>
        /// Date Modified:  19/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        /// -------------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List<ManifestOnOffCalendar> LoadOnOffCalendar(string UserId, DateTime Date,
            Int32 RegionID, Int32 PortID, Int32 VesselID, Int32 HotelID, string sPage, Int16 TypeView)
        {
            try
            {
                return calendar.LoadOnOffCalendar(UserId, Date, RegionID, PortID, VesselID, HotelID, sPage, TypeView);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   19/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   Get of ON/OFF count of calendar
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<ManifestOnOffCalendar> LoadOnOffCalendarExport(string UserId)
        {
            try
            {
                return calendar.LoadOnOffCalendarExport(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   07/11/2012
        /// Created By:     Josephine Gad
        /// (description)   get list of calendar for room needed per day
        ///  </summary>
        public List<CalendarRoomNeeded> GetCalendarRoomNeeded(string UserId, DateTime Date, string sDateTo,
            Int32 RegionID, Int32 PortID, Int32 BranchID)
        {
            try
            {
                return calendar.GetCalendarRoomNeeded(UserId, Date, sDateTo, RegionID, PortID, BranchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
          /// <summary>
        /// Date Created:   14/Jan/2015
        /// Created By:     Josephine Monteza
        /// (description)   get list of calendar for room needed per day, hotel as column
        /// ---------------------------------------------------------------
        ///  </summary>
        public DataTable GetCalendarRoomNeeded_Forecast(string UserId, DateTime Date, string sDateTo,
            Int32 RegionID, Int32 PortID, Int32 BranchID)
        {
            try
            {
                return calendar.GetCalendarRoomNeeded_Forecast(UserId, Date, sDateTo, RegionID, PortID, BranchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

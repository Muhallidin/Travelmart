using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.DAL;
using System.Web;

namespace TRAVELMART.BLL
{
    public class OverFlowBookingBLL
    {
        OverFlowBookingDAL OverflowDAL = new OverFlowBookingDAL();

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/02/2012
        /// Description: load all queries for overflow bookings
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="FilterBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <returns></returns>
        public void LoadAllOverflowBooking(DateTime Date, string UserId, int Loadtype, int FilterBy,
            int startRowIndex, int MaximumRows)
        {
            List<HotelOverflowGenericClass> overflowBooking = new List<HotelOverflowGenericClass>();
            try
            {
                overflowBooking = OverflowDAL.LoadAllOverflowBooking(Date, UserId, Loadtype, FilterBy,
                                startRowIndex, MaximumRows);

                if (overflowBooking.Count > 0)
                {
                    HotelOverflowBooking.OverflowBookingCount = overflowBooking[0].OverflowBookingCount;
                    HotelOverflowBooking.OverflowBooking = overflowBooking[0].OverflowBooking;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: load overflowbooking list on paging
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="FilterBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <returns></returns>
        public List<ExceptionBooking> LoadOverflowBookingTable(DateTime Date, string UserId, int Loadtype, int FilterBy,
            int startRowIndex, int MaximumRows)
        { 
            List<ExceptionBooking> overflow = new List<ExceptionBooking>();
            int countRow = 0;

            overflow = OverflowDAL.LoadOverflowBookingTable(Date, UserId, Loadtype, FilterBy, startRowIndex, MaximumRows, out countRow);
            HotelOverflowBooking.OverflowBooking = overflow;
            HotelOverflowBooking.OverflowBookingCount = countRow;
            return overflow;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: load overflowbooking list count on paging
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="FilterBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <returns></returns>
        public int LoadOverflowBookingTableCount(DateTime Date, string UserId, int Loadtype, int FilterBy)
        {
            return GlobalCode.Field2Int(HotelOverflowBooking.OverflowBookingCount);
        }

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 08/03/2012
        /// Description: load all queries for overflow bookings for new UI  
        /// ----------------------------------------------------------------
        /// Modiofied By:   Josephine Gad
        /// Date Modified:  25/07/2012
        /// Description:    Change HotelTransactionOverflow.OverflowBooking2 to Session["HotelTransactionOverflowOverflowBooking"]
        ///                 Change HotelTransactionOverflow.Hotels  to Session["HotelTransactionOverflowHotels"]
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        public void LoadHotelOverflowPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID, 
            int iBrandID, int iVesselID, int iRoomType)
        {
            List<HotelTransactionOverflowGenericClass> Overflow = new List<HotelTransactionOverflowGenericClass>();
            try
            {

                Overflow = OverflowDAL.LoadHotelOverflowPage(Date, UserId, Loadtype, RegionID, PortID, iBrandID, iVesselID, iRoomType);

                if (Overflow.Count > 0)
                {
                    //HotelTransactionOverflow.OverflowBooking2 = Overflow[0].OverflowBooking2;
                    //HotelTransactionOverflow.Hotels = Overflow[0].Hotels;

                    HttpContext.Current.Session["HotelTransactionOverflowOverflowBooking"] = Overflow[0].OverflowBooking2;
                    HttpContext.Current.Session["HotelTransactionOverflowHotels"] = Overflow[0].Hotels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Apr/2014
        /// Description:    Load details for Service Provider for 0 to 6 days
        /// ---------------------------------------------------------------      
        /// </summary>        
        public void LoadHotelOverflowPageDays(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<HotelTransactionOverflowGenericClass> Overflow = new List<HotelTransactionOverflowGenericClass>();
            try
            {

                Overflow = OverflowDAL.LoadHotelOverflowPageDays(Date, UserId, Loadtype, RegionID, PortID);

                if (Overflow.Count > 0)
                {                    
                    HttpContext.Current.Session["HotelTransactionOverflowOverflowBooking"] = Overflow[0].OverflowBooking2;
                    HttpContext.Current.Session["HotelTransactionOverflowHotels"] = Overflow[0].Hotels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  04/Apr/2014
        /// Description:    Get overflow list to extract
        /// </summary>
        /// <returns></returns>
        public DataTable GetOverflowExtract(string sUser)
        {
            return OverflowDAL.GetOverflowExtract(sUser);
        }
          /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   06/Aug/2014
        /// Description:    Add or remove record from Overflow List
        /// ---------------------------------------------------------------      
        public static void OverflowAddRemoveFromList(DataTable dt, bool IsRemovedBit, string UserId,
            String strLogDescription, String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate,
            string sComment)
        {
            try
            {
                OverFlowBookingDAL.OverflowAddRemoveFromList(dt, IsRemovedBit, UserId,
                strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, sComment);
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
        /// Author:         Josephine Monteza
        /// Date Created:   07/Aug/2014
        /// Description:    Get records removed from Overflow List
        /// ---------------------------------------------------------------        
        public static List<OverflowBooking2> OverflowGetRemovedList(DateTime Date, string UserId, int LoadType, int RegionID, int PortID)
        {
            return OverFlowBookingDAL.OverflowGetRemovedList(Date, UserId, LoadType, RegionID, PortID);
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/Oct/2014
        /// Description:    Get overflow list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<OverflowByMonth> GetOverflowByMonthList(int iYear, Int16 iMonth, int iRegion, int iPort, string sUser,
            bool IsPageChanged, int StartRow, int MaxRow)
        {
            return OverflowDAL.GetOverflowByMonthList(iYear, iMonth, iRegion, iPort, sUser,
            IsPageChanged, StartRow, MaxRow);
        }
        public int GetOverflowByMonthCount(int iYear, Int16 iMonth, int iRegion, int iPort, string sUser,
           bool IsPageChanged)
        {
            int i = GlobalCode.Field2Int(HttpContext.Current.Session["HotelOverflowBookingsDashboard_Count"]);
            return i;
        }
         /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   30/Oct/2014
        /// Description:    Get overflow list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public DataTable GetOverflowByMonthExport(string sUser)
        {
            return OverflowDAL.GetOverflowByMonthExport(sUser);
        }
    }
}

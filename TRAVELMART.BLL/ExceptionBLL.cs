using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;
using System.Web;

namespace TRAVELMART.BLL
{
    public class ExceptionBLL
    {
        ExceptionDAL ExceptionDAL = new ExceptionDAL();
        
        public static DataTable GetException(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            try
            {
                dt = ExceptionDAL.GetException(DateFrom, DateTo, StartRow, MaxRow);
                return dt;
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
        public static int GetExceptionCount(DateTime DateFrom, DateTime DateTo)
        {
            return ExceptionDAL.GetExceptionCount(DateFrom, DateTo);
        }

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: load all queries for exception bookings for new UI
        /// ----------------------------------------------------------------
        /// Modiofied By:   Josephine Gad
        /// Date Modified:  25/07/2012
        /// Description:    Change HotelTransactionException.ExceptionBooking to Session["HotelTransactionExceptionExceptionBooking"]
        ///                 Change HotelTransactionException.Hotels  to Session["HotelTransactionExceptionHotels"]
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        public void LoadHotelExceptionPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<HotelTransactionExceptionGenericClass> Exceptions = new List<HotelTransactionExceptionGenericClass>();
            try
            {
                Exceptions = ExceptionDAL.LoadHotelExceptionPage(Date, UserId, Loadtype, RegionID, PortID);

                if (Exceptions.Count > 0)
                {
                    //HotelTransactionException.ExceptionBooking = Exceptions[0].ExceptionBooking;
                    //HotelTransactionException.Hotels = Exceptions[0].Hotels;
                    HttpContext.Current.Session["HotelTransactionExceptionExceptionBooking"] = Exceptions[0].ExceptionBooking;
                    HttpContext.Current.Session["HotelTransactionExceptionHotels"] = Exceptions[0].Hotels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  Jan-06-2014
        /// Description:    Get top 50 exception list
        /// ---------------------------------------------------------------
        /// <returns></returns>
        public void LoadHotelExceptionPageTop(string UserId, int Loadtype, int RegionID,
            int PortID, string sOrderBy)
        {
            List<HotelTransactionExceptionGenericClass> Exceptions = new List<HotelTransactionExceptionGenericClass>();
            try
            {
                Exceptions = ExceptionDAL.LoadHotelExceptionPageTop(UserId, Loadtype, RegionID, PortID, sOrderBy);

                if (Exceptions.Count > 0)
                {
                    HttpContext.Current.Session["HotelTransactionExceptionExceptionBooking"] = Exceptions[0].ExceptionBooking;
                    HttpContext.Current.Session["HotelTransactionExceptionHotels"] = Exceptions[0].Hotels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  Apr-02-2014
        /// Description:    Get exception list from 0 to 6 days
        /// ---------------------------------------------------------------
        /// <returns></returns>
        public void LoadHotelExceptionPageDays(DateTime dDate, string UserId, int RegionID,
            int PortID, string sRole, Int16 iLoadType)
        {
            List<HotelTransactionExceptionGenericClass> Exceptions = new List<HotelTransactionExceptionGenericClass>();
            try
            {
                Exceptions = ExceptionDAL.LoadHotelExceptionPageDays(dDate, UserId, RegionID, PortID, sRole, iLoadType);

                if (Exceptions.Count > 0)
                {
                    HttpContext.Current.Session["HotelTransactionExceptionExceptionBooking"] = Exceptions[0].ExceptionBooking;
                    HttpContext.Current.Session["HotelTransactionExceptionHotels"] = Exceptions[0].Hotels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  Jan-07-2014
        /// Description:    Get exception list to extract
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionExtract(string sUser)
        {
            return ExceptionDAL.GetExceptionExtract(sUser);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Add or remove record from Exception List
        /// ---------------------------------------------------------------
        /// </summary>
        /// <param name="ExceptionIDInt"></param>
        /// <param name="IsRemovedBit"></param>
        /// <param name="UserId"></param>
        /// <param name="strLogDescription"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public static void ExceptionAddRemoveFromList(Int64 ExceptionIDInt, bool IsRemovedBit, string UserId,
            String strLogDescription, String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate,
            string sComment)
        {
           ExceptionDAL.ExceptionAddRemoveFromList(ExceptionIDInt, IsRemovedBit, UserId,
            strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, sComment);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Get records removed from Exception List
        /// ---------------------------------------------------------------
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="RegionID"></param>
        /// <param name="PortID"></param>
        /// <returns></returns>
        public static List<ExceptionBooking> ExceptionGetRemovedList(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            return ExceptionDAL.ExceptionGetRemovedList(Date, UserId, Loadtype, RegionID, PortID);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   13/Jan/2014
        /// Description:    Get exception list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<ExceptionsByMonth> GetExceptionByMonth(int iYear, Int16 iMonth, int iRegion, int iPort, string sUser)
        {
            return ExceptionDAL.GetExceptionByMonth(iYear, iMonth, iRegion, iPort, sUser);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   13/Jan/2014
        /// Description:    Get exception list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionByMonthExport(string sUser)
        {
            return ExceptionDAL.GetExceptionByMonthExport(sUser);
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   01/oct/2014
        /// Description:    Get Excption Data to load in the page
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<ExceptionPageData> GetExceptionPageData(short LoadType, string UserId
                , DateTime Date, int RegionID, int PortID, int CountryID, string UserRole

            )
        {
            try
            {
                return ExceptionDAL.GetExceptionPageData(LoadType, UserId, Date, RegionID, PortID, CountryID, UserId);
            }
            catch {
                throw; 
            
            }
        }





    }
}

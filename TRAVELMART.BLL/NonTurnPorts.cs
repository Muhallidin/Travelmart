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
    public class NonTurnPorts
    {
        HotelNoContractDAL ExceptionDAL = new HotelNoContractDAL();
        
        public static DataTable GetException(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            try
            {
                dt = HotelNoContractDAL.GetException(DateFrom, DateTo, StartRow, MaxRow);
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
            return HotelNoContractDAL.GetExceptionCount(DateFrom, DateTo);
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
                Exceptions = ExceptionDAL.LoadNoHotelHotelExceptionPage(Date, UserId, Loadtype, RegionID, PortID);

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
    }
}

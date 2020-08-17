using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 01/02/2012
    /// Description: Initialize Lists
    /// </summary>
    /// 
    public class HotelOverflowBooking
    {
       
        public static int ? OverflowBookingCount { get; set; }
        public int GetOverflowBookingCount(DateTime Date, string UserId, int Loadtype, int FilterBy)
        {
            try
            {
                return (int)OverflowBookingCount;
            }
            catch
            {
                throw;
            }
            finally
            {
                OverflowBookingCount = null;
            }
        }
        public static List<ExceptionBooking> OverflowBooking { get; set; }
        public List<ExceptionBooking> GetOverflowBooking(DateTime Date, string UserId, int Loadtype, int FilterBy,
            int startRowIndex, int MaximumRows)
        {
            try
            {
                return OverflowBooking;
            }
            catch
            {
                throw;
            }
            finally
            {
                OverflowBooking = null;
            }
        }

 
    }

    public class HotelOverflowGenericClass
    {
        public int? OverflowBookingCount { get; set; }
        public List<ExceptionBooking> OverflowBooking { get; set; }
    }
}

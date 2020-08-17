using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class HotelTransactionExceptionGenericClass
    {
        public List<Hotels> Hotels { get; set; }
        public List<ExceptionBooking> ExceptionBooking { get; set; }
    }

    //public class HotelTransactionException
    //{
    //    public static List<Hotels> Hotels { get; set; }
    //    public static List<ExceptionBooking> ExceptionBooking { get; set; }
    //}

    public class HotelTransactionOverflowGenericClass
    {
        public List<Hotels> Hotels { get; set; }
        public List<OverflowBooking2> OverflowBooking2 { get; set; }
    }

    //public class HotelTransactionOverflow
    //{
    //    public static List<Hotels> Hotels { get; set; }
    //    public static List<OverflowBooking2> OverflowBooking2 { get; set; }
    //}
}

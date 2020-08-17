using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{

    public class HotelDashboardClass
    {
        //public static int? ConfirmBookingCount { get; set; }
        //public int GetConfirmBookingCount(DateTime StartDate,
        //    Int32 BranchId, Int16 LoadType, String UserId)
        //{
        //    try
        //    {
        //        return (int)ConfirmBookingCount;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        ConfirmBookingCount = null;
        //    }
        //}

        //public static List<ConfirmBooking> ConfirmBooking { get; set; }
        //public List<ConfirmBooking> GetConfirmBooking(DateTime StartDate,
        //    Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    try
        //    {
        //        return ConfirmBooking;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        ConfirmBooking = null;
        //    }
        //}

        //public static int? PendingBookingCount { get; set; }
        //public int GetPendingBookingCount(DateTime StartDate,
        //          Int32 BranchId, Int16 LoadType, String UserId)
        //{
        //    try
        //    {
        //        return (int)PendingBookingCount;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        PendingBookingCount = null;
        //    }
        //}

        public static List<OverflowBooking> PendingBooking { get; set; }
        //public List<OverflowBooking> GetPendingBooking(DateTime StartDate,
        //   Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    try
        //    {
        //        return PendingBooking;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        PendingBooking = null;
        //    }
        //}

        //public static int? EventCount { get; set; }

        //public static List<HotelRoomBlocks> HotelRoomBlocks { get; set; }



    }

    public class HotelDashBoardGenericClass
    {
        public int? EventCount { get; set; }
        public List<HotelRoomBlocks> HotelRoomBlocks { get; set; }
        public int? ConfirmBookingCount { get; set; }
        public List<ConfirmBooking> ConfirmBooking { get; set; }
        public int? PendingBookingCount { get; set; }
        public List<OverflowBooking> PendingBooking { get; set; }

    }

    [Serializable]
    public class HotelDashBoardPAGenericClass
    {
     
        public List<PortList> PortList { get; set; }

        public List<HotelDashBoardPortAgentClass> HotelDashBoardPortAgentClass { get; set; }

    }

    [Serializable]
    public class HotelDashBoardPortAgentClass
    {


        public int? PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        public int? TotalDoubleRoomBlock { get; set; }
        public int? TotalSingleRoomBlock { get; set; }
        public int? ContractID { get; set; }

        public DateTime? DashBoardDate { get; set; }
    }

}

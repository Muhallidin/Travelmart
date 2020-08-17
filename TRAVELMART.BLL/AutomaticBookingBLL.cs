using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using System.Data;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class AutomaticBookingBLL
    {
        AutomaticBookingDAL DAL = new AutomaticBookingDAL();
        
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/01/2012
        /// description: get automatic booking count
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AvailableBooking"></param>
        /// <returns></returns>
        public Int32 SelectAutomaticBookingCount(String UserId)
        {
            try
            {
                return DAL.SelectAutomaticBookingCount(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 12/01/2012
        /// description: get automatic bookings
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable SelectAutomaticBooking(String UserId, Int32 StartRowIndex, Int32 maximumRows)
        {
            try
            {
                return DAL.SelectAutomaticBooking(UserId, StartRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 19/01/2012
        /// Description: temporarily save bookings
        /// </summary>
        /// <param name="colDate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static void SaveTempBookings(DateTime colDate, String UserId)
        {
            try
            {
                AutomaticBookingDAL.SaveTempBookings(colDate,UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 21/01/2012
        /// description: Book pending and automatic bookings
        /// </summary>
        /// <param name="Duration"></param>
        /// <param name="TravelReqId"></param>
        /// <param name="ManualReqId"></param>
        /// <param name="RecLoc"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomTypeId"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="SFStatus"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="ContractId"></param>
        /// <param name="Voucher"></param>
        /// <param name="DataSource"></param>
        /// <param name="HotelPendingId"></param>
        public static Boolean ApproveBookings(Int32 TravelReq, String RecordLocator, Int32 VendorId, Int32 BranchId, Int32 RoomType,
            DateTime CheckInDate, DateTime CheckOutDate, String SFStatus, Int32 CityId, Int32 CountryId, String VoucherAmount,
            Int32 ContractId, String UserId, String strFunction, String Filepath, DateTime GMTDate, DateTime DateNow)
        {
            try
            {
                return AutomaticBookingDAL.ApproveBookings(TravelReq, RecordLocator, VendorId, BranchId, RoomType,
                        CheckInDate, CheckOutDate, SFStatus, CityId, CountryId, VoucherAmount,
                        ContractId, UserId, strFunction, Filepath, GMTDate, DateNow);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/01/2012
        /// description: get automatic booking count
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AvailableBooking"></param>
        /// <returns></returns>
        public Int32 SelectOverFlowBookingCount(String UserId, String SFStatus)
        {
            try
            {
                return DAL.SelectOverFlowBookingCount(UserId, SFStatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 12/01/2012
        /// description: get automatic bookings
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AvailableBooking"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable SelectOverFlowBooking(Int32 StartRowIndex, Int32 maximumRows, String UserId, String SFStatus)
        {
            try
            {
                return DAL.SelectOverFlowBooking(StartRowIndex, maximumRows, UserId, SFStatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Boolean ApproveOverFlowBooking(Int32 BranchId, Int32 roomTypeId, Int32 travelReq, DateTime CheckIn,
           DateTime CheckOut, String Status, Int32 seafarerID, String RecordLocator, Decimal Stripe, String UserId, String strFunction,
            String FileName, DateTime GMTDate, DateTime Now)
        {
            try
            {
                return AutomaticBookingDAL.ApproveOverFlowBooking(BranchId, roomTypeId, travelReq,  CheckIn,
                        CheckOut, Status, seafarerID, RecordLocator, Stripe, UserId, strFunction, FileName, GMTDate, Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 19/01/2012
        /// Description: temporarily save bookings
        /// </summary>
        /// <param name="AirportId"></param>
        /// <param name="RoomType"></param>
        /// <param name="colDate"></param>
        /// <param name="Branch"></param>
        /// <param name="UserId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static void SaveTempAutomaticBookings(DateTime colDate,
            Int32 Branch, String UserId)
        {
            try
            {
                AutomaticBookingDAL.SaveTempAutomaticBookings(colDate,
                            Branch, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Author: Charlene Remotigue
        ///// Date Created: 02/02/2012
        ///// description: load all tables for overflow
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="SFStatus"></param>
        ///// <param name="LoadType"></param>
        ///// <param name="StartRowIndex"></param>
        ///// <param name="MaximumRows"></param>
        //public void LoadAllOverflowTables(String UserId, DateTime StartDate, String SFStatus, Int16 LoadType,
        //    Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    try
        //    {
        //        AutomaticBookingDAL DAL = new AutomaticBookingDAL();
        //        List<HotelOverflowGenericClass> OverflowList = new List<HotelOverflowGenericClass>();

        //        OverflowList = DAL.LoadAllOverflowTables(UserId, StartDate, SFStatus, LoadType,
        //            StartRowIndex, MaximumRows);

        //        if (OverflowList.Count > 0)
        //        {
        //            HotelOverflowBooking.OverflowBookingCount = OverflowList[0].OverflowBookingCount;
        //            HotelOverflowBooking.OverflowBooking = OverflowList[0].OverflowBooking;
        //            HotelOverflowBooking.UserBranchList = OverflowList[0].UserBranchList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Author:Cherlene Remotigue
        ///// Date Created: Charlene remotigue
        ///// Description: load OverflowBooking on Seafarer status change
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="SFStatus"></param>
        ///// <param name="LoadType"></param>
        ///// <param name="StartRowIndex"></param>
        ///// <param name="MaximumRows"></param>
        //public void LoadOverflowBookingPerStatus(String UserId, DateTime StartDate, String SFStatus, Int16 LoadType,
        //    Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    List<OverflowBooking> OverflowList = new List<OverflowBooking>();
        //    int countROw = 0;
        //    OverflowList = DAL.LoadOverflowBookingTable(UserId, StartDate, SFStatus, LoadType, StartRowIndex, MaximumRows, out countROw);
        //    HotelOverflowBooking.OverflowBookingCount = countROw;
        //    HotelOverflowBooking.OverflowBooking = OverflowList;

        //}

        ///// <summary>
        ///// Author:Charlene Remotigue
        ///// Date Created: 02/02/2012
        ///// Description: load overflow booking paging list
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="SFStatus"></param>
        ///// <param name="LoadType"></param>
        ///// <param name="StartRowIndex"></param>
        ///// <param name="MaximumRows"></param>
        ///// <returns></returns>
        //public List<OverflowBooking> LoadOverflowBookingTable(String UserId, DateTime StartDate, String SFStatus, Int16 LoadType,
        //    Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    List<OverflowBooking> OverflowList = new List<OverflowBooking>();
        //    int countROw = 0;
        //    OverflowList = DAL.LoadOverflowBookingTable(UserId, StartDate, SFStatus, LoadType, StartRowIndex, MaximumRows, out countROw);
        //    //countROw;

        //    HotelOverflowBooking.OverflowBookingCount = countROw;
        //    HotelOverflowBooking.OverflowBooking = OverflowList;
        //    return OverflowList;
        //}

        ///// <summary>
        ///// Author:Charlene Remotigue
        ///// Date Created: 02/02/2012
        ///// Description: load overflow booking paging list count
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="SFStatus"></param>
        ///// <param name="LoadType"></param>
        ///// <returns></returns>
        //public int LoadOverflowBookingCount(String UserId, DateTime StartDate, String SFStatus, Int16 LoadType)
        //{
        //    return GlobalCode.Field2Int(HotelOverflowBooking.OverflowBookingCount);
        //}
           
    }
}

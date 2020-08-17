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
    public class HotelBookingsBLL
    {
        HotelBookingsDAL BookingsDAL = new HotelBookingsDAL();

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: get all user branch details
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  27/09/2012
        /// Description:    Replace class with session
        /// </summary>
        /// <param name="UserId"></param>
        public void LoadUserBranchDetails(string UserId,DateTime Date, int HotelTransId)
        {
            List<HotelBookingsGenericClass> branchList = new List<HotelBookingsGenericClass>();
            branchList = BookingsDAL.LoadUserBranchDetails(UserId, Date, HotelTransId);
            if (branchList.Count > 0)
            {
                //HotelBookings.BranchList = branchList[0].BranchList;
                //HotelBookings.RegionList = branchList[0].RegionList;
                //HotelBookings.CountryList = branchList[0].CountryList;
                //HotelBookings.CityList = branchList[0].CityList;

                HttpContext.Current.Session["HotelBookingsBranchList"] = branchList[0].BranchList;
                HttpContext.Current.Session["HotelBookingsRegionList"] = branchList[0].RegionList;
                HttpContext.Current.Session["HotelBookingsCountryList"] = branchList[0].CountryList;
                HttpContext.Current.Session["HotelBookingsCityList"] = branchList[0].CityList;
                
                if (HotelTransId != 0)
                {
                    //HotelBookings.SeafarerDetailsList = branchList[0].SeafarerDetailsList;
                    HttpContext.Current.Session["HotelBookingsSeafarerDetailsList"] = branchList[0].SeafarerDetailsList;
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: get room allocations
        /// ---------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    return roomList intead of assigning to HotelBookings.RoomAllocations
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="Duration"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public List<RoomAllocations> getRoomBlocks(DateTime CheckInDate, int Duration, int numOfPeople, int RoomType, int BranchId, int ContractId)
        {
            try
            {
                List<RoomAllocations> roomList = new List<RoomAllocations>();
                roomList = BookingsDAL.getRoomBlocks(CheckInDate, Duration, numOfPeople, RoomType, BranchId, ContractId);
                                
                //HotelBookings.RoomAllocations = roomList;                
                return roomList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 14/05/2013
        /// Description:  get room allocations
        /// ---------------------------------
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="Duration"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        //public List<RoomAllocations> getRoomBlocks(DateTime CheckInDate, int Duration, int numOfPeople, int RoomType, int BranchId, int ContractId)
        public List<RoomAllocations> getRoomBlocks(DateTime CheckInDate, int Duration, int numOfPeople, int RoomType, int BranchId, int ContractId,decimal Stripe)
        {
            try
            {
                List<RoomAllocations> roomList = new List<RoomAllocations>();
                roomList = BookingsDAL.getRoomBlocks(CheckInDate, Duration, numOfPeople, RoomType, BranchId, ContractId, Stripe);

                //HotelBookings.RoomAllocations = roomList;                
                return roomList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: Book Seafarer
        /// </summary>
        /// <param name="TravelReqId"></param>
        /// <param name="RecordLocator"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="SfStatus"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="Stripes"></param>
        /// <param name="ShuttleBit"></param>
        public bool BookSeafarer(string IDBigint, string SeqNo, string TravelReqId, string ReqId, string RecordLocator, int Source, int ContractId, 
            int VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId, string SfStatus,
            int CityId, int CountryId, string Remarks, string Stripes, string HotelCity, string IsPort,
            bool ShuttleBit, string Description, string Function, string PathName, 
            string TimeZone, DateTime GMTDate, DateTime DateNow)
        {
            try
            {
                bool isValid = true;
                List<HotelEmail> emailList = new List<HotelEmail>();
                emailList = BookingsDAL.BookSeafarer(IDBigint, SeqNo, TravelReqId, ReqId, RecordLocator, Source, ContractId, VendorId, BranchId,
                    RoomType,
                    CheckInDate, CheckInTime, Duration, confirmationNum, HotelStatus, UserId, SfStatus,
                    CityId, CountryId, Remarks, Stripes, HotelCity, IsPort, ShuttleBit, Description, Function, PathName,
                    TimeZone, GMTDate, DateNow, out isValid);

                if (emailList != null)
                {
                    HotelBookings.HotelEmail = emailList;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author:       Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description:  Book Seafarer
        /// </summary>
        /// <param name="TravelReqId"></param>
        /// <param name="RecordLocator"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="SfStatus"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="Stripes"></param>
        /// <param name="ShuttleBit"></param>
        /// <param name="RoomCount"></param>
        public bool BookSeafarer(string IDBigint, string SeqNo, string TravelReqId, string ReqId, string RecordLocator, int Source, int? ContractId,
            int VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId, string SfStatus,
            int CityId, int CountryId, string Remarks, string Stripes, string HotelCity, string IsPort,
            bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow,decimal roomCount, bool Emergency)
        {         
            try
            {
                bool isValid = true;
                List<HotelEmail> emailList = new List<HotelEmail>();
                emailList = BookingsDAL.BookSeafarer(IDBigint, SeqNo, TravelReqId, ReqId, RecordLocator, Source, ContractId, VendorId, BranchId,
                    RoomType,
                    CheckInDate, CheckInTime, Duration, confirmationNum, HotelStatus, UserId, SfStatus,
                    CityId, CountryId, Remarks, Stripes, HotelCity, IsPort, ShuttleBit, Description, Function, PathName,
                    TimeZone, GMTDate, DateNow, out isValid, roomCount, Emergency);

                if (emailList != null)
                {
                    HotelBookings.HotelEmail = emailList;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   


        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: Book Seafarer
        /// </summary>
        /// <param name="TravelReqId"></param>
        /// <param name="RecordLocator"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="SfStatus"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="Stripes"></param>
        /// <param name="ShuttleBit"></param>
        public bool BookSeafarer(List<HotelTransactionOverFlowBooking> BookHotel,short savefrom)
        {
            try
            {
                bool isValid = true;
                List<HotelEmail> emailList = new List<HotelEmail>();
                emailList = BookingsDAL.BookSeafarer(BookHotel,savefrom, out isValid);

                if (emailList != null)
                {
                    HotelBookings.HotelEmail = emailList;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/02/2012
        /// Description: Update Seafarer Hotel Bookings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="ShuttleBit"></param>
        /// <param name="Description"></param>
        /// <param name="Function"></param>
        /// <param name="PathName"></param>
        /// <param name="TimeZone"></param>
        /// <param name="GMTDate"></param>
        /// <param name="DateNow"></param>
        /// <param name="HotelTransId"></param>
        public void UpdateSeafarerBookings(int Source, int? ContractId, int? VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId,
            int CityId, int CountryId, string Remarks, bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, int HotelTransId)
        {
            try
            {
                List<HotelEmail> emailList = new List<HotelEmail>();
                emailList = BookingsDAL.UpdateSeafarerBookings(Source, ContractId, VendorId, BranchId, RoomType,
                    CheckInDate, CheckInTime, Duration, confirmationNum, HotelStatus, UserId,
                    CityId, CountryId, Remarks, ShuttleBit, Description, Function, PathName,
                    TimeZone, GMTDate, DateNow, HotelTransId);
                HotelBookings.HotelEmail = emailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/02/2012
        /// Description: Update Seafarer Hotel Bookings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="ShuttleBit"></param>
        /// <param name="Description"></param>
        /// <param name="Function"></param>
        /// <param name="PathName"></param>
        /// <param name="TimeZone"></param>
        /// <param name="GMTDate"></param>
        /// <param name="DateNow"></param>
        /// <param name="HotelTransId"></param>
        public void UpdateSeafarerBookings(int Source, int? ContractId, int? VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId,
            int CityId, int CountryId, string Remarks, bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, string Stripes, bool Emergency, int HotelTransId)
        {
            try
            {
                List<HotelEmail> emailList = new List<HotelEmail>();
                emailList = BookingsDAL.UpdateSeafarerBookings(Source, ContractId, VendorId, BranchId, RoomType,
                    CheckInDate, CheckInTime, Duration, confirmationNum, HotelStatus, UserId,
                    CityId, CountryId, Remarks, ShuttleBit, Description, Function, PathName,
                    TimeZone, GMTDate, DateNow, Stripes,Emergency, HotelTransId);
                HotelBookings.HotelEmail = emailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   19/Dec/2014
        /// Description:    Update Seafarer ID of Hotel Booking
        /// </summary>       
        /// <returns></returns>
        public void UpdateSeafarerBookingsEditSeafarer(int Source, int? ContractId, int? VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId,
            int CityId, int CountryId, string Remarks, bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, string Stripes, bool Emergency, int HotelTransId,
            Int64 iTRID, Int64 iIDBigInt, string SfStatus, string RecLoc, Int32 iSeqNo)
        {
            BookingsDAL.UpdateSeafarerBookingsEditSeafarer(Source, ContractId, VendorId, BranchId, RoomType,
                   CheckInDate, CheckInTime, Duration, confirmationNum, HotelStatus, UserId,
                   CityId, CountryId, Remarks, ShuttleBit, Description, Function, PathName,
                   TimeZone, GMTDate, DateNow, Stripes, Emergency, HotelTransId, iTRID, iIDBigInt,
                   SfStatus, RecLoc, iSeqNo);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/02/2012
        /// Description: get event count
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public static int GetEventCount(DateTime Date, int BranchId)
        {
            try
            {
                HotelBookingsDAL DAL = new HotelBookingsDAL();
                return DAL.GetEventCount(Date, BranchId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  24/07/2012
        /// Description:    return roomList instead of using HotelBookings.RoomBlocks
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List <RoomBlocks> getRemainingRooms(DateTime CheckInDate, DateTime CheckOutDate, int numOfPeople, int RoomType, int BranchId)
        {
            try
            {
                List<RoomBlocks> roomList = new List<RoomBlocks>();
                roomList = BookingsDAL.getRemainingRooms(CheckInDate, CheckOutDate, numOfPeople, RoomType, BranchId);

                //HotelBookings.RoomBlocks = roomList;
                return roomList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Modified By:    Muhallidin G Wali
        /// Date Modified:  24/07/2012
        /// Description:    return roomList instead of using HotelBookings.RoomBlocks
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<RemainRoomBlocks> getRemainingRooms(DateTime CheckInDate, DateTime CheckOutDate, int RoomType, int BranchId)
        {
            try
            {
                List<RemainRoomBlocks> roomList = new List<RemainRoomBlocks>();
                roomList = BookingsDAL.getRemainingRooms(CheckInDate, CheckOutDate, RoomType, BranchId);

                //HotelBookings.RoomBlocks = roomList;
                return roomList;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 14/05/2013
        /// Description: Get available room block
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="RoomType"></param>
        /// <param name="TransHotelID"></param>
        /// <returns></returns>
        public List<RemainRoomBlocksWithHotelID> getRemainingRooms(DateTime DateFrom, DateTime DateTo, int BranchId, int RoomType, long TransHotelID)
        {
            try
            {
                List<RemainRoomBlocksWithHotelID> roomList = new List<RemainRoomBlocksWithHotelID>();
                roomList = BookingsDAL.getRemainingRooms(DateFrom, DateTo, BranchId, RoomType, TransHotelID);

                //HotelBookings.RoomAllocations = roomList;                
                return roomList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   18/Dec/2014
        /// Description:    Get Crew Schedule
        /// </summary>
        public List<SeafarerDetailList> GetCrewScedule(Int64 iEmployeeID, bool bShowPast)
        {
            return BookingsDAL.GetCrewScedule(iEmployeeID,bShowPast);
        }
         /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   18/Dec/2014
        /// Description:    Get Air Transaction
        /// </summary>
        public List<CrewAssistAirTransaction> GetAirTransaction(Int64 IDBigint)
        {
            return BookingsDAL.GetAirTransaction(IDBigint);
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   27/Nov/2017
        /// Description:    Delete selected Hotel Exception
        /// </summary>
        public short DeleteHotelException(string Excption, string UserID, string Comment)
        {
           return BookingsDAL.DeleteHotelException(Excption, UserID, Comment);
        }
    }
}

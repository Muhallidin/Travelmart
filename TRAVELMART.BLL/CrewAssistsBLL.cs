using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;



namespace TRAVELMART.BLL
{
    public class CrewAssistsBLL
    {


        /// <summary>
        /// Author:      Muhallidin G Wali
        /// Created:     16/Nov/2013
        /// description  Get Vendors of Port Selected
        /// 
        /// </summary>
        public List<CrewAssistGenericVendor> GetGenericVendors(Int16 LoadTpe, string Username, int SeaPortID, int AirPortID)
        {

            try
            {
                CrewAssistsDAL CA = new CrewAssistsDAL();
                return CA.GetGenericVendors(LoadTpe, Username, SeaPortID, AirPortID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<CrewMemberInformation> GetCrewAssitCMInformation(short loadType, long SeafarerID, string UserID)
        {
            try
            {
                CrewAssistsDAL CA = new CrewAssistsDAL();
                return CA.GetCrewAssitCMInformation(loadType, SeafarerID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Prioritize voucher from Hotel Other rather than Hotel Request table
        ///                Change MealVoucherMoney  = GlobalCode.Field2Double(r["colMealVoucherMoney"])
        ///                to     MealVoucherMoney = (GlobalCode.Field2Double(r["colVoucherAmountMoney"]) == 0 ? GlobalCode.Field2Double(r["[colMealVoucherMoney]"]) 
        ///                                                                                                        : GlobalCode.Field2Double(r["colVoucherAmountMoney"])),
        /// </summary>
        /// <returns></returns>
        public List<CrewAssistCMTransaction> GetCrewMemberTransaction(short LoadType, long SeafarerID,
            long TravelRequestID, long IDBigInt, long SeqNo, DateTime Startdate, string DepCode,
             string ArrCode, bool IsAir, string UserID)
        {
            try
            {
                CrewAssistsDAL CA = new CrewAssistsDAL();
                return CA.GetCrewMemberTransaction(LoadType, SeafarerID, TravelRequestID, IDBigInt, SeqNo, Startdate, DepCode, ArrCode, IsAir, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Created By:    Muhallidin G Wali
        /// Date Modified: 10/24/2014
        /// (description)  Insert Air Status.
        /// </summary>
        public List<CrewAssistCMTransaction> InsertAirTransactionStatus(long TravelRequestID, long IdBigint, int SeqNo, int StatusID, string OldStatus, string UserID)
        {
            try
            {
                CrewAssistsDAL CA = new CrewAssistsDAL();
                return CA.InsertAirTransactionStatus(TravelRequestID, IdBigint, SeqNo, StatusID, OldStatus, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CrewAssistHotelBooking> InsertHotelTransRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
          string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
          bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
          string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
          string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
          string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
          bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
          double ContractedRate, string EmailTO, string HotelCity, short ReqSource,
          string ContactName, string ContactNo, string Recipient,
          string CCEmail, string BlindCopy, double RateConfirm, bool IsMedical, long TransHotelID, List<HotelRequestCompanion> HRC)
        {
            try
            {
                CrewAssistsDAL CA = new CrewAssistsDAL();
                return CA.InsertHotelTransRequest(RequestNo, SfID, LastName, FirstName, Gender, RegionID,
                       PortID, AirportID, HotelID, CheckinDate, CheckoutDate, NoNites, RoomType,
                       MealBreakfast, MealLunch, MealDinner, MealLunchDinner, WithShuttle,
                       RankID, VesselInt, CostCenter, Comments, SfStatus, TimeIn, TimeOut,
                       RoomAmount, IsRoomTax, RoomTaxPercent, UserID, TrID, Currency,
                       strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, IsAir, SequentNo, IDBig,
                       BrandID, MealVoucher, ConfirmRate, ContractedRate, EmailTO, HotelCity, ReqSource,
                       ContactName, ContactNo, Recipient, CCEmail, BlindCopy, RateConfirm, IsMedical, TransHotelID, HRC);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        // <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public List<CrewAssistHotelBooking> SavePortAgentHotelRequest(List<HotelTransactionPortAgent> HotelTranPortAgent, string UserID, List<HotelRequestCompanion> HRC)
        {
            CrewAssistsDAL DAL = new CrewAssistsDAL();
            return DAL.SavePortAgentHotelRequest(HotelTranPortAgent, UserID, HRC);
        }



        /// <summary>
        /// Date Create:    07/MAR/2014
        /// Create By:      Muhallidin G Wali
        /// (description)  	Get Port Agent Detail
        /// </summary>
        public List<CrewAssistHotelInformation> GetCrewAssistHotelVendor(short LoadType, long HotelVendorID, long TravelRequestID, long IDBigInt, string PortCode)
        {
            CrewAssistsDAL DAL = new CrewAssistsDAL();
            return DAL.GetCrewAssistHotelVendor(LoadType, HotelVendorID, TravelRequestID, IDBigInt, PortCode);
        }


        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Save SF Hotel Request
        /// -------------------------------------------
        /// Date Modified: 30/May/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add Shuttle, MealLunchDinner, Tax Bit and Tax Percent
        ///                Add fields for audit trail use 
        /// -------------------------------------------
        /// </summary>
        public List<CrewAssistHotelBooking> SeafarerSaveRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
            double ContractedRate, string EmailTO, string HotelCity, short ReqSource,
            string ContactName, string ContactNo, string Recipient,
            string CCEmail, string BlindCopy, double RateConfirm, bool IsMedical, long TransHotelID, List<HotelRequestCompanion> HRC)
        {

            try
            {
                CrewAssistsDAL CA = new CrewAssistsDAL();
                return CA.SeafarerSaveRequest(RequestNo, SfID, LastName, FirstName, Gender, RegionID,
                    PortID, AirportID, HotelID, CheckinDate, CheckoutDate, NoNites, RoomType,
                    MealBreakfast, MealLunch, MealDinner, MealLunchDinner, WithShuttle,
                    RankID, VesselInt, CostCenter, Comments, SfStatus, TimeIn, TimeOut,
                    RoomAmount, IsRoomTax, RoomTaxPercent, UserID, TrID, Currency,
                    strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, IsAir, SequentNo, IDBig,
                    BrandID, MealVoucher, ConfirmRate, ContractedRate, EmailTO, HotelCity, ReqSource,
                    ContactName, ContactNo, Recipient, CCEmail, BlindCopy, RateConfirm, IsMedical, TransHotelID, HRC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

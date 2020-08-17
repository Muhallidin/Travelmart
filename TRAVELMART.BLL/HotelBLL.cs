using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class HotelBLL
    {
        #region METHODS   

        /// <summary>
        /// Date Created:   15/7/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel details By Vendor ID
        /// --------------------------------------------------
        /// Date Modified:  24/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// </summary>
        public static DataTable HotelDetailsByVendorID(Int32 VendorID)
        {
            DataTable dt  = null;
            try
            {
                dt = HotelDAL.HotelDetailsByVendorID(VendorID);
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
        /// <summary>
        /// Date Created:   19/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Selecting Hotel by Region and/or Port and/or Country
        /// ----------------------------------------------
        /// Date Modified:   15/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        //public static DataTable GetHotelBranchByRegionPortCountry(string userString, string regionString, string portString, string countryString, string airportString)
        //{
        public static List<HotelDTO> GetHotelBranchByRegionPortCountry(string userString, string regionString, string portString, string countryString, string airportString)
        {
            return HotelDAL.GetHotelBranchByRegionPortCountry(userString, regionString, portString, countryString, airportString);
            //DataTable dt = null;
            //try
            //{
            //    dt = HotelDAL.GetHotelBranchByRegionPortCountry(userString, regionString, portString, countryString, airportString);
            //    return dt;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Created:    29/Sept/2015
        /// Created By:      Josephine Monteza
        /// (description)    Selecting Hotel by City of given Hotel
        /// ----------------------------------------------
        /// <returns></returns>
        public static List<HotelDTO> GetHotelBranchByCityOfHotel(Int64 iBranchID)
        {
           // return HotelDAL.GetHotelBranchByCityOfHotel(iBranchID);
            return null;
        }
        /// <summary>
        /// Date Created:   26/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel by City
        /// --------------------------------------------
        /// Date Modified:   14/02/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change output from DataTable to List
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public static List<HotelDTO> GetHotelBranchByCity(string userString, string cityString)
        {
            return HotelDAL.GetHotelBranchByCity(userString, cityString);
            //DataTable dt = null;
            //try
            //{
            //    dt = HotelDAL.GetHotelBranchByCity(userString, cityString);
            //    return dt;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }
         /// Date Created:   24/01/2012
        ///  Created By:     Josephine Gad
        /// (description)   Selecting All Hotel
        /// </summary>        
        /// <param name="cityString"></param>
        /// <returns></returns>
        public static List<HotelAirportDTO> GetHotelBranchAll(string cityString, Int32 Airport, bool IsViewExist, Int16 iRoomType)
        {
            return HotelDAL.GetHotelBranchAll(cityString, Airport, IsViewExist, iRoomType);
        }
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Country List By Vendor ID
        /// </summary>
        public static DataTable CountryListByVendorID(Int32 VendorID)
        {
             DataTable dt  = null;
             try
             {
                 dt = HotelDAL.CountryListByVendorID(VendorID);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Country  By Vendor branch ID
        /// </summary>
        public static DataTable CountryByVendorBranchID(Int32 VendorID)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.CountryByVendorBranchID(VendorID);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting hotel branch
        /// </summary>
        public static DataTable HotelBranchList()
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelBranchList();
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting hotel branch
        /// </summary>
        public static DataTable HotelBranchListByVendorID(Int32 VendorID, string Username)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelBranchListByVendorID(VendorID, Username);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor ID
        /// </summary>
        public static DataTable CityListByVendorID(Int32 VendorID)
        {
             DataTable dt  = null;
             try
             {
                 dt = HotelDAL.CityListByVendorID(VendorID);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Country ID
        /// </summary>
        public static DataTable CityListByCountryID(Int32 CountryID)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.CityListByCountryID(CountryID);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Country ID and vendor ID
        /// </summary>
        public static DataTable CityListByVendorIDCountryID(Int32 VendorID, Int32 CountryID)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.CityListByVendorIDCountryID(VendorID, CountryID);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Country ID and vendor ID
        /// </summary>
        public static DataTable CityByVendorBranchIDCountryID(Int32 VendorID, Int32 CountryID)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.CityByVendorBranchIDCountryID(VendorID, CountryID);
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

        /// <summary>
        /// Date Created:   07/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Branch By Vendor and City
        /// ----------------------------------------------
        /// </summary>
        public static DataTable GetVendorBranch(Int32 VendorID, Int32 CityID)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.GetVendorBranch(VendorID, CityID);
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

        /// <summary>
        /// Date Created:   19/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel event notification   
        /// ----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// -------------------------------------------
        /// Date Modified: 23/01/2012
        /// Modified By:   Josephine Gad
        /// (description)  make OnOffDate string to allow null values
        /// -------------------------------------------
        /// </summary>
        public static IDataReader GetEventNotification(Int32 HotelBranchID, Int32 CityID, string OnOffDate)
        {
            IDataReader dt = null;
            try
            {
                dt = HotelDAL.GetEventNotification(HotelBranchID, CityID, OnOffDate);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Date Created:   24/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel room count for contract
        /// ----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// --------------------------------------
        /// Date Modified: 07/12/2011
        /// Modified By:   Josephine Gad
        /// (description)  Add UserRole parameter
        /// </summary>
        public static IDataReader GetContractRoomBlockCount(Int32 HotelBranchID, Int32 RoomTypeID, String CheckInDate, 
            Int32 Duration, String User, string UserRole)
        {
            IDataReader dt = null;
            try
            {
                dt = HotelDAL.GetContractRoomBlockCount(HotelBranchID, RoomTypeID, CheckInDate, Duration, User, UserRole);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel room count for override
        /// -------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader GetOverrideRoomBlockCount(Int32 HotelBranchID, Int32 RoomTypeID, String CheckInDate, Int32 Duration, String User, String UserRole)
        {
            IDataReader dt = null;
            try
            {
                dt = HotelDAL.GetOverrideRoomBlockCount(HotelBranchID, RoomTypeID, CheckInDate, Duration, User, UserRole);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get active hotel contract id
        /// ----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader GetActiveHotelContractID(String HotelBranchID, String RoomTypeID, String CheckInDate, String Duration, String User)
        {
            IDataReader dt = null;
            try
            {
                int BranchId = Int32.Parse(HotelBranchID);
                int roomType = Int32.Parse(RoomTypeID);
                DateTime checkIn = DateTime.Parse(CheckInDate);
                int noOfDays = Int32.Parse(Duration);
                
                dt = HotelDAL.GetActiveHotelContractID(BranchId, roomType, checkIn, noOfDays, User);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor Code and Type
        /// </summary>
        public static DataTable CityListByVendorCodeAndType(string VendorCode, string VendorType)
        {
             DataTable dt  = null;
             try
             {
                 dt = HotelDAL.CityListByVendorCodeAndType(VendorCode, VendorType);
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Hotel Booking Details By Booking ID
        /// ------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader HotelBookingDetailsByID(int TravelLocID, int SeqNo)
        {
            IDataReader dt  = null;
            try
            {
                dt = HotelDAL.HotelBookingDetailsByID(TravelLocID, SeqNo);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel Booking Details By Booking ID (None Sabre)
        /// --------------------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader HotelBookingDetailsOtherByID(string TransHotelIDBigInt)
        {
            IDataReader dt = null;
            try
            {
                dt = HotelDAL.HotelBookingDetailsOtherByID(TransHotelIDBigInt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>        
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer hotel transaction (pending)
        /// --------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// --------------------------------------------------------
        /// Date Modified: 19/12/2011
        /// Modified By:   Josephine Gad
        /// (description)  Do not close IDataReader
        /// </summary>  
        public static IDataReader HotelBookingPendingByID(string PendingHotelID)
        {
            IDataReader dr = null;
             try
             {
                 dr = HotelDAL.HotelBookingPendingByID(PendingHotelID);
                 return dr;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             //finally
             //{
             //    if (dr != null)
             //    {
             //        dr.Dispose();
             //    }
             //}
        }
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Hotel Details 
        /// </summary>
        public static DataTable HotelVendorGetDetails()
        {
             DataTable dt  = null;
             try
             {
                 dt = HotelDAL.HotelVendorGetDetails();
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

        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Hotel Details by region
        /// </summary>
        public static DataTable HotelVendorGetDetailsByRegion(string Username)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelVendorGetDetailsByRegion(Username);
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

        /// <summary>
        /// Date Created:   15/7/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Room Type Details 
        /// --------------------------------------------------
        /// Date Modified:  24/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter RankTypeString
        /// </summary>
        public static DataTable HotelRoomTypeGetDetails()
        {
            DataTable dt  = null;
            try
            {
                dt = HotelDAL.HotelRoomTypeGetDetails();
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
         /// <summary>
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel Room Type Details not exists in Branch
        /// -----------------------------------------------
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -----------------------------------------------
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static List<HotelBranchRoomTypeNotExist> HotelRoomTypeGetNotExist(string BranchID)
        {
            return HotelDAL.HotelRoomTypeGetNotExist(BranchID);
        }
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Hotel Booking 
        /// </summary>
        public static void InsertHotelBooking(int SfID, int HotelID, int RoomType, DateTime CheckInDate, int duration, string HotelStatus, string User,
                            string SfStatus, string LocatorID, int CityID, string CheckInTime, Boolean WithBreakfast, Boolean WithLunch,
                            Boolean WithDinner, string Remarks, Boolean BillToCrew, Boolean WithShuttle)
        {
            HotelDAL.InsertHotelBooking(SfID, HotelID, RoomType, CheckInDate, duration, HotelStatus, User, SfStatus, LocatorID, CityID, CheckInTime,
                                        WithBreakfast, WithLunch, WithDinner, Remarks, BillToCrew, WithShuttle);
        }

        /// <summary>
        /// Date Created:   26/7/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Hotel Booking NOT from Sabre
        /// ---------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josphine Gad
        /// (description)   Add parameter WithLunchOrDinner and other meal parameters
        /// </summary>
        /// 
        public static void InsertHotelBookingOther(string TravelReqID, string RecordLoc, string RequestID, string VendorID, string BranchID, string RoomTypeID,
          string CheckInDate, string CheckInTime, string Duration, string HotelStatus, string SfStatus, string CreatedBy,
          string Remarks, bool BillToCrew, bool WithBreakfast, bool WithLunch, bool WithDinner, bool WithLunchOrDinner, bool WithShuttle
          //string BreakfastIDString, string LunchIDString, string DinnerIDString, string LunchDinnerIDString
            )         
        {
            HotelDAL.InsertHotelBookingOther(TravelReqID, RecordLoc, RequestID, VendorID, BranchID, RoomTypeID, CheckInDate, CheckInTime, Duration,
                HotelStatus, SfStatus, CreatedBy, Remarks, BillToCrew, WithBreakfast, WithLunch, WithDinner, WithLunchOrDinner, WithShuttle
                //BreakfastIDString, LunchIDString, DinnerIDString, LunchDinnerIDString
                );
        }
         /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Pending Hotel Booking 
        ///-------------------------------------------------------------------------------           
        /// Date Modified:   05/12/2011
        /// Modified By:     Josephine Gad
        /// (description)    add parameter Confirmation
        ///-------------------------------------------------------------------------------
        /// Date Modified:   17/01/2012
        /// Modified By:     Josephine Gad
        /// (description)    Remove parameter RoomAmount,CurrencyID, WithTax, TaxAmount and ContractId
        ///-------------------------------------------------------------------------------
        /// </summary>
        public static Int32 InsertHotelBookingPending(string TransHotelID, string IdBigint, string SeqNo,
            string TravelReqID, string RecordLoc, string RequestID, string VendorID,
            string BranchID, string RoomTypeID,
            string CheckInDate, string CheckInTime, string Duration, string HotelStatus, string SfStatus,
            string CreatedBy, string CreatedDate, string ModifiedByString, string ModifiedDate,
            string Remarks, bool BillToCrew, bool WithBreakfast, bool WithLunch, bool WithDinner, bool WithLunchOrDinner, string VoucherAmount,
            bool WithShuttle, 
            //string RoomAmount, int CurrencyID, bool WithTax, string TaxAmount, 
            string Action, string Confirmation
            //, string ContractId
            )
        {
            return HotelDAL.InsertHotelBookingPending(TransHotelID, IdBigint, SeqNo, TravelReqID, RecordLoc,
                RequestID, VendorID, BranchID, RoomTypeID, CheckInDate, CheckInTime, Duration, HotelStatus,
                SfStatus, CreatedBy, CreatedDate, ModifiedByString, ModifiedDate, Remarks, BillToCrew,
                WithBreakfast, WithLunch, WithDinner, WithLunchOrDinner, VoucherAmount, WithShuttle,
                 Action, Confirmation);
        }
         /// <summary>
        /// Date Created:   17/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Insert Pending Hotel Booking Details for each day
        ///------------------------------------------------------------------------------- 
        /// </summary>
        /// <param name="HotelTransPendingDetailsID"></param>
        /// <param name="PendingHotelIDBigInt"></param>
        /// <param name="dDate"></param>
        /// <param name="ContractID"></param>
        /// <param name="ContractFromVarchar"></param>
        /// <param name="CurrencyIDInt"></param>
        /// <param name="RatePerDay"></param>
        /// <param name="RoomRateTaxPercentage"></param>
        /// <param name="RoomRateTaxInclusive"></param>
        /// <param name="CreatedByVarchar"></param>
        public static Int32 InsertHotelBookingPendingDetails(Int32 HotelTransPendingDetailsID, Int32 PendingHotelIDBigInt,
            DateTime dDate, Int32 ContractID, string ContractFromVarchar, Int32 CurrencyIDInt, decimal RatePerDay,
            decimal RoomRateTaxPercentage, bool RoomRateTaxInclusive, string CreatedByVarchar)
        {
            return HotelDAL.InsertHotelBookingPendingDetails(HotelTransPendingDetailsID, PendingHotelIDBigInt,
            dDate, ContractID, ContractFromVarchar, CurrencyIDInt, RatePerDay, RoomRateTaxPercentage,
            RoomRateTaxInclusive, CreatedByVarchar);
        }
         /// <summary>
        /// Date Created:   18/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Delete Pending Hotel Booking Details 
        ///-------------------------------------------------------------------------------          
        /// </summary>
        /// <param name="HotelTransPendingDetailsID"></param>
        /// <param name="DeletedByVarchar"></param>
        public static void DeleteHotelBookingPendingDetails(Int32 HotelTransPendingDetailsID, string DeletedByVarchar)
        {
            HotelDAL.DeleteHotelBookingPendingDetails(HotelTransPendingDetailsID, DeletedByVarchar);
        }
         /// <summary>
        /// Date Created:   18/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Select Pending Hotel Booking Details 
        ///-------------------------------------------------------------------------------          
        /// </summary>
        /// <param name="HotelTransPendingDetailsID"></param>
        /// <param name="DeletedByVarchar"></param>
        public static DataTable SelectHotelBookingPendingDetails(Int32 PendingHotelID)
        {
            return HotelDAL.SelectHotelBookingPendingDetails(PendingHotelID);
        }
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Update Hotel Booking 
        /// ---------------------------------------
        /// Date Modified:  02/09/2011
        /// Modified By:    Josphine Gad
        /// (description)   Add parameter WithLunchOrDinner and other meal parameters
        /// </summary>
        public static void UpdateHotelBooking(int HotelBookingID, int HotelID, int BranchIDInt, int RoomType, DateTime CheckInDate, int duration, string HotelStatus,
            string User, string CheckInTime, Boolean WithBreakfast, Boolean WithLunch,
            Boolean WithDinner, Boolean WithLunchOrDinner, string Remarks, Boolean BillToCrew, Boolean WithShuttle, int SeqNo
            //string BreakfastIDString, string LunchIDString, string DinnerIDString, string LunchDinnerIDString
            )
        {
            HotelDAL.UpdateHotelBooking(HotelBookingID, HotelID, BranchIDInt, RoomType, CheckInDate, duration, HotelStatus, User, CheckInTime,
                                        WithBreakfast, WithLunch, WithDinner, WithLunchOrDinner, Remarks, BillToCrew, WithShuttle, SeqNo
                                        //BreakfastIDString, LunchIDString, DinnerIDString, LunchDinnerIDString
                                        );
        }

        #region UpdateHotelBookingOther
        /// <summary>
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Update Hotel Booking (none Sabre)
        ///------------------------------------------                           
        /// Date Modified:   02/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter WithLunchOrDinner
        /// -----------------------------------------
        /// </summary>
        /// 
        public static void UpdateHotelBookingOther(string TransHotelID, string TravelReqID, string VendorId,
           string BranchID, string RoomType, string CheckInDate, string CheckInTime, string duration,
           string HotelStatus, string ModifiedBy, string Remarks, bool BillToCrew, bool WithBreakfast,
           bool WithLunch, bool WithDinner,bool WithLunchOrDinner, bool WithShuttle
           //string BreakfastIDString, string LunchIDString, string DinnerIDString, string LunchDinnerIDString
            )
        {
            HotelDAL.UpdateHotelBookingOther(TransHotelID, TravelReqID, VendorId, BranchID, RoomType, 
                CheckInDate, CheckInTime, duration, HotelStatus, ModifiedBy, Remarks, BillToCrew, WithBreakfast,
                WithLunch, WithDinner, WithLunchOrDinner, WithShuttle
                //BreakfastIDString, LunchIDString, DinnerIDString, LunchDinnerIDString
                );
        }
        #endregion

        #region UpdateHotelBookingPending
        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Update Pending Hotel Booking 
        ///-------------------------------------------------------------------------------           
        /// </summary>
        public static void UpdateHotelBookingPending(string PendingID, string TransHotelID, string IdBigint, string SeqNo,
            string TravelReqID, string RecordLoc, string RequestID, string VendorID, string BranchID, string RoomTypeID,
            string CheckInDate, string CheckInTime, string Duration, string HotelStatus, string SfStatus,
            string ModifiedByString, string ModifiedDate,
            string Remarks, bool BillToCrew, bool WithBreakfast, bool WithLunch, bool WithDinner, bool WithLunchOrDinner,
            string VoucherAmount, bool WithShuttle, string Action)
        {
            HotelDAL.UpdateHotelBookingPending(PendingID, TransHotelID, IdBigint, SeqNo,
            TravelReqID, RecordLoc, RequestID, VendorID, BranchID, RoomTypeID,
            CheckInDate, CheckInTime, Duration, HotelStatus, SfStatus,
            ModifiedByString, ModifiedDate,
            Remarks, BillToCrew, WithBreakfast, WithLunch, WithDinner, WithLunchOrDinner,
            VoucherAmount, WithShuttle, Action);
        }
        #endregion

        #region DeleteHotelBookingPending
        /// <summary>
        /// Date Created:   04/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Pending Hotel Booking
        /// </summary>
        public static void DeleteHotelBookingPending(int PendingHotelID, string DeletedByString)
        {
            HotelDAL.DeleteHotelBookingPending(PendingHotelID, DeletedByString);
        }
        #endregion

        #region DeleteHotelBooking
        public static void DeleteHotelBooking(int HotelBookingID, string User, int SeqNo)
        {
            /// <summary>
            /// Date Created: 15/7/2011
            /// Created By: Ryan Bautista
            /// (description) Delete Hotel Booking 
            /// </summary>

            HotelDAL.DeleteHotelBooking(HotelBookingID, User, SeqNo);
        }
        #endregion

        #region DeleteHotelBookingOther
        public static void DeleteHotelBookingOther(int TransHotelIDBigInt, string DeletedByString)
        {
            HotelDAL.DeleteHotelBookingOther(TransHotelIDBigInt, DeletedByString);
        }
        #endregion

        #region GetNumberOfRoomsAvailableByHotelAndLocation
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Get number of rooms available by hotel name and hotel location 
        /// </summary>
        public static DataTable GetNumberOfRoomsAvailableByHotelAndLocation(Int32 VendorID, Int32 RoomTypeID)
        {
            DataTable dt  = null;
            try
            {
                dt = HotelDAL.GetNumberOfRoomsAvailableByHotelAndLocation(VendorID, RoomTypeID);
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
        #endregion

        #region HotelRoomTypeGetDetailsByBranch
        /// <summary>
        /// Date Created:    25/08/2011
        /// Created By:      Josephine Gad
        /// (description)    Selecting Hotel Room Type Details by Vendor Branch
        /// -----------------------------------------------
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -----------------------------------------------        
        /// </summary>
        public static List<HotelBranchRoomType> HotelRoomTypeGetDetailsByBranch(string BranchIDString)
        {
            return HotelDAL.HotelRoomTypeGetDetailsByBranch(BranchIDString);
        }
          /// <summary>
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Hotel Room Type Capacity and Rate Details by Branch
        /// -----------------------------------------------
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static DataTable HotelRoomTypeCapacityGetDetailsByBranch(string BranchID)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelRoomTypeCapacityGetDetailsByBranch(BranchID);
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
        #endregion

        #region HotelRoomType
        /// <summary>
        /// Date Created:   25/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel Room Type Details 
        /// --------------------------------------------------       
        /// </summary>
        public static DataTable HotelRoomType()
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelRoomType();
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
        #endregion

        #region HotelMealsGetByBranch
        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel Meals
        /// -----------------------------------------------
        /// </summary>
        /// 
        public static DataTable HotelMealsGetByBranch(string BranchIDString, string MealTypeString, string MealTypeString2)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelMealsGetByBranch(BranchIDString, MealTypeString, MealTypeString2);
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
        #endregion

        #region HotelMealsType
        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals type
        /// -----------------------------------------------
        /// </summary>
        /// 
        public static DataTable HotelMealsType()
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelMealsType();
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
        #endregion

        #region HotelMealsPref
        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals preference
        /// -----------------------------------------------
        /// </summary>
        /// 
        public static DataTable HotelMealsPref()
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelMealsPreference();
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
        #endregion

        #region HotelMealsPreferenceByMealType
        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals preference by meal type
        /// -----------------------------------------------
        /// </summary>
        /// 
        public static DataTable HotelMealsPreferenceByMealType(string MealType)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelMealsPreferenceByMealType(MealType);
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
        #endregion

        #region  HotelMealsPreferenceByMealTypePref
        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals preference by meal type and preference
        /// -----------------------------------------------
        /// </summary>
        /// 
        public static DataTable HotelMealsPreferenceByMealTypePref(string MealType, string MealPref)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelMealsPreferenceByMealTypePref(MealType, MealPref);
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
        #endregion

        #region HotelMeals
        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals
        /// -----------------------------------------------
        /// </summary>
        /// 
        public static DataTable HotelMeals()
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelMeals();
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
        #endregion

        #region InsertHotelMealBranch
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Hotel meal branch 
        /// </summary>
        public static Int32 InsertHotelMealBranch(string BranchID, string MealName, string Username)
        {
            return HotelDAL.InsertHotelMealBranch(BranchID, MealName, Username);
        }
        #endregion

        #region RemoveHotelMealBranch
        /// <summary>
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Remove Hotel meal branch 
        /// </summary>
        public static void RemoveHotelMealBranch(string BranchID, string MealName, string Username)
        {
            HotelDAL.RemoveHotelMealBranch(BranchID, MealName, Username);
        }
        #endregion

        #region HotelHasEvents
        /// <summary>                                               
        /// Date Created: 19/10/2011
        /// Created By: Gabriel Oquialda
        /// (description)  Event notification
        /// </summary>          
        public static DataTable HotelHasEvents()
        {
            DataTable HotelEvents = null;
            try
            {
                HotelEvents = HotelDAL.HotelHasEvents();
                return HotelEvents;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelEvents != null)
                {
                    HotelEvents.Dispose();
                }
            }
        }
        #endregion

        #region HotelApproveTransaction
        /// <summary>
        /// Date Created:   04/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Approve Pending hotel transaction
        /// </summary>        
        /// <param name="PendingHotelID"></param>
        /// <param name="ApprovedBy"></param>
        public static DataTable HotelApproveTransaction(string PendingHotelID, string ApprovedBy)
        {
            DataTable dt = null;
            try
            {
                dt = HotelDAL.HotelApproveTransaction(PendingHotelID, ApprovedBy);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
        #endregion

          /// <summary>
        /// Date Created:   25/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Hotel Room Override By Branch
        /// ---------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ---------------------------------------------------
        /// Date Modified: 16/02/2012
        /// Modified By:   Josephine Gad
        /// (description)  Change DataReader to List
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="EffectiveDate"></param>
        /// <returns></returns>
        //public static IDataReader GetHotelRoomOverrideByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        public static List<HotelRoomBlocksDTO> GetHotelRoomOverrideByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        {
            try
            {
                return HotelDAL.GetHotelRoomOverrideByBranch(BranchID, RoomTypeID, EffectiveDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  27/11/2011
        /// Created By:    Josephine Gad
        /// (description)  Save Hotel Room Override by branch, room and date
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="EffectiveDate"></param>
        /// <param name="RatePerDayMoney"></param>
        /// <param name="Currency"></param>
        /// <param name="RoomRateTaxPercentage"></param>
        /// <param name="RoomRateTaxInclusive"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="RoomBlocksPerDayInt"></param>
        /// <param name="CreatedByVarchar"></param>
        public static DataTable SaveHotelRoomOverrideByBranch(string BranchID, DateTime EffectiveDate, string RatePerDayMoney,
           string Currency, string RoomRateTaxPercentage, bool RoomRateTaxInclusive, string RoomTypeID, string RoomBlocksPerDayInt,
           string CreatedByVarchar)
        {
            DataTable dtOverride = null;
            return dtOverride = HotelDAL.SaveHotelRoomOverrideByBranch(BranchID, EffectiveDate, RatePerDayMoney, Currency,
                RoomRateTaxPercentage, RoomRateTaxInclusive, RoomTypeID, RoomBlocksPerDayInt, CreatedByVarchar);
        }
         /// <summary>
        /// Date Created:  16/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get department and stripes of hotel branch
        /// ------------------------------------------------------------
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        /// ------------------------------------------------------------
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change void to List<HotelBranchDeptStripe>
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static List<HotelBranchDeptStripe> GetHotelBranchDeptStripes(string BranchID)
        {
            try
            {
               return  HotelDAL.GetHotelBranchDeptStripes(BranchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>
        /// Date Created:  23/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get rank exceptions of hotel branch
        /// -----------------------------------------------------
        /// Date Modified: 21/03/2012
        /// Modified By:   Josephine Gad
        /// (description)  Change DataTable to List
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static List<HotelRankException> GetHotelBranchRankException(string BranchID)
        {
            try
            {
                return HotelDAL.GetHotelBranchRankException(BranchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/12/2011
        /// Description: automatically book seafarers
        /// </summary>
        /// <param name="trID"></param>
        /// <param name="mrID"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="roomTypeId"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="UserId"></param>
        /// <param name="Status"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BfastBit"></param>
        /// <param name="LunchBit"></param>
        /// <param name="DinnerBit"></param>
        /// <param name="LunchOrDinnerBit"></param>
        /// <param name="withShuttle"></param>
        /// <param name="VoucherAmount"></param>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        //public static Boolean AutomaticBooking(String trID, String mrID, String VendorId, String BranchId, String roomTypeId,
        //    String CheckInDate, String UserId, String Status, String CityId, String CountryId, String BfastBit, String LunchBit,
        //    String DinnerBit, String LunchOrDinnerBit, String withShuttle, String VoucherAmount, String ContractId)
        //{
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
         /// <summary>
        /// Date Created:    10/02/2012
        /// Created By:      Josephine Gad
        /// (description)    Get Hotel Emergency Room Blocks
        /// ---------------------------------------------------           
        /// Date Modified:   16/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Replace IDataReader with List
        /// ---------------------------------------------------  
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="EffectiveDate"></param>
        /// <returns></returns>
       // public static IDataReader GetHotelRoomEmergencyByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        public static List<HotelRoomBlocksEmergencyDTO> GetHotelRoomEmergencyByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        {
            try
            {
                return HotelDAL.GetHotelRoomEmergencyByBranch(BranchID, RoomTypeID, EffectiveDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  10/02/2012
        /// Created By:    Josephine Gad
        /// (description)  Save Hotel Emergency Room by branch, room and date
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="EffectiveDate"></param>
        /// <param name="RatePerDayMoney"></param>
        /// <param name="Currency"></param>
        /// <param name="RoomRateTaxPercentage"></param>
        /// <param name="RoomRateTaxInclusive"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="RoomBlocksPerDayInt"></param>
        /// <param name="CreatedByVarchar"></param>
        public static DataTable SaveHotelRoomEmergencyByBranch(string BranchID, DateTime EffectiveDate, string RatePerDayMoney,
           string Currency, string RoomRateTaxPercentage, bool RoomRateTaxInclusive, string RoomTypeID, string RoomBlocksPerDayInt,
           string CreatedByVarchar)
        {
            DataTable dtEmergency = null;
            return dtEmergency = HotelDAL.SaveHotelRoomEmergencyByBranch(BranchID, EffectiveDate, RatePerDayMoney, Currency,
                RoomRateTaxPercentage, RoomRateTaxInclusive, RoomTypeID, RoomBlocksPerDayInt, CreatedByVarchar);            
        }

        /// <summary>
        /// Date Created: 21/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Validate country code
        /// </summary> 
        public static Boolean countrycodeInInsertExist(String CountryCode, Int32 CountryID)
        {
            Boolean bValidation;

            try
            {
                bValidation = HotelDAL.countrycodeInInsertExist(CountryCode, CountryID);
                return bValidation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 03/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Added validation for emergency room block(s) versus the total no. of emergency booking(s)
        /// </summary> 
        public static IDataReader GetEmergencyTotalBookings(String Date, Int32 BranchID, Int32 RoomTypeID)
        {
            try
            {
                return HotelDAL.GetEmergencyTotalBookings(Date, BranchID, RoomTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:    08/05/2013
        /// Created By:      Marco Abejar
        /// (description)    Get available room type per hotel
        /// ----------------------------------------------
        public static DataTable GetAvailHotelRoomType(string HotelId, string CheckInDate)
        {
            return HotelDAL.GetAvailHotelRoomType(HotelId, CheckInDate);            
        }

        ///<summary>
        ///Date Created:    09/09/2015
        ///Created By:      Michael Evangelista
        ///Description:     Add Hotel Vendor List for Create Hotel Request on Non Turn Ports
        ///</summary>
        public  void GetHotelVendorListforNonTurn() {
           // DAL.GetHotelVendorListforNonTurn();

        }
        #endregion
    }
}

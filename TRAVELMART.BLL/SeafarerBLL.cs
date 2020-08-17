using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class SeafarerBLL
    {
        #region DECLARATIONS
        
        private SeafarerDAL dal = new SeafarerDAL();
        
        #endregion

        #region METHODS

        public static IDataReader SeafarerGetDetails(string SfID, string TravelRequestID, string ManualRequestID, bool ViewInTR)
        {
             IDataReader dt = null;
             try
             {
                 dt = SeafarerDAL.SeafarerGetDetails(SfID, TravelRequestID, ManualRequestID, ViewInTR);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
           
        }
        /// <summary>
        /// Date Modified: 27/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Hotel Request
        /// </summary>
        public static IDataReader SeafarerGetRequestDetails(string SfID, string TravelRequestID, string HotelRequestID, string AppNew)
        {
             IDataReader dt = null;
             try
             {
                 dt = SeafarerDAL.SeafarerGetRequestDetails(SfID, TravelRequestID, HotelRequestID, AppNew );
                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
           
        }
        /// <summary>
        /// Date Modified: 27/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Companion Details
        /// </summary>
        public static DataTable SeafarerGetCompanionDetails(string HotelRequestID)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerDAL.SeafarerGetCompanionDetails(HotelRequestID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
        public static string SeafarerSaveRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID,string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            try
            {
                return SeafarerDAL.SeafarerSaveRequest( RequestNo, SfID, LastName, FirstName, Gender, RegionID,
                    PortID, AirportID, HotelID, CheckinDate, CheckoutDate, NoNites, RoomType,
                    MealBreakfast, MealLunch, MealDinner, MealLunchDinner, WithShuttle,
                    RankID, VesselInt, CostCenter, Comments, SfStatus, TimeIn, TimeOut,
                    RoomAmount, IsRoomTax, RoomTaxPercent, UserID, TrID, Currency,
                    strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Date Modified: 04/02/2013
        /// Modified By:   Marco Abejar
        /// (description)  Submit SF Hotel Request
        /// </summary>
        public static void SeafarerSubmitRequest(string RequestID, string UserID, string ContactName, string ContactNo,
            string Description, string Function, string FileName)
        {
            try
            {
                SeafarerDAL.SeafarerSubmitRequest(RequestID, UserID, ContactName, ContactNo,
                    Description, Function, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Date Modified: 27/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Save SF Companion Hotel Request
        /// </summary>
        public static void SeafarerSaveComapnionRequest(string RequestDetailID, string RequestID, string LastName, string FirstName, string Relationship, string Gender, string UserID)
        {
            try
            {
                SeafarerDAL.SeafarerSaveComapnionRequest(RequestDetailID, RequestID, LastName, FirstName, Relationship, Gender, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="SfID"></param>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static IDataReader VoucherGetDetails(decimal dStripe, string BranchID, string Days)
        {
            IDataReader dt = null;
            try
            {
                //Int32 SfIDInt = Convert.ToInt32(SfID);
                Int32 BranchIDInt = Convert.ToInt32(BranchID);
                Int32 NoOfDays = Convert.ToInt32(Days);
                dt = SeafarerDAL.VoucherGetDetails(dStripe, BranchIDInt, NoOfDays);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public static DataTable GetSeafarer()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerDAL.GetSeafarer();
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
        /// Date Created:    03/10/2011
        /// Created By:      Josephine Gad
        /// (description)    Select seafarers by filter (Name or E1 ID)
        /// ---------------------------------------------
        /// Date Modified:   17/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------
        /// </summary>
        /// <param name="filterBy"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        //public static DataTable GetSeafarerByFilter(string filterBy, string filterValue)
        public static List<SeafarerDTO> GetSeafarerByFilter(string filterBy, string filterValue)
        {
            return SeafarerDAL.GetSeafarerByFilter(filterBy, filterValue);         
        }
        // <summary>
        /// Date Modified:   17/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Get Seafarer List
        /// ---------------------------------------------
        /// </summary>
        /// <param name="filterBy"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        public static List<SeafarerListDTO> GetSeafarerListByFilter(string filterBy, string filterValue)
        {
            return SeafarerDAL.GetSeafarerListByFilter(filterBy, filterValue); 
        }
         /// <summary>
        /// Date Created:   25/10/2011
        /// Created By:     Charlene Remotigue
        /// (description)   Load all users that has tagged seafarer as scanned
        /// ---------------------------------------------
        /// </summary>
        /// <param name="mReqId"></param>
        /// <param name="tReqId"></param>
        /// <returns></returns>
        public static DataTable GetSeafarerOtherInfo(string mReqId, string tReqId)
        {
            try
            {
                return SeafarerDAL.GetSeafarerOtherInfo(mReqId, tReqId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }
        /// <summary>
        /// Date Created:   02/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get room type by stripe
        /// --------------------------------------------- 
        /// </summary>
        /// <param name="dStripe"></param>
        /// <returns></returns>
        public static IDataReader GetRoomTypeByStripe(decimal dStripe)
        {
            try
            {
                return SeafarerDAL.GetRoomTypeByStripe(dStripe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Modified: 01/04/2013
        /// Modified By:   Marco Abejar
        /// (description)  Remove Companion
        /// Date Modified: 08/29/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Add parameter RequestDetailID
        /// </summary>
        public static DataTable RemoveRequestCompanion(long RequestDetailID, long RequestID)
        {
            try
            {
                return SeafarerDAL.RemoveRequestCompanion(RequestDetailID, RequestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Date Modified: 01/04/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Hotel Request List View
        /// </summary>
        public static DataTable GetHotelRequestList(string SortBy)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerDAL.GetHotelRequestList(SortBy);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Date Modified: 01/04/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Booked Hotel Request List View
        /// </summary>
        public static DataTable GetBookedHotelRequestList(string SortBy)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerDAL.GetBookedHotelRequestList(SortBy);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetHotelRequestListCount(string SortBy)
        {
            //return GlobalCode.Field2Int(ManifestSearchViewDTO.ManifestSearchViewListCount);
            return 5;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class RequestBLL
    {       
        #region "Select"

        /// <summary>
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of hotel/vehicle request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="UserID"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="Status"></param>
        /// <param name="FilterByNameID"></param>
        /// <param name="FilterNameID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <param name="VehicleID"></param>
        /// <param name="VesselID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="Approved"></param>
        /// <returns></returns>
        public static DataTable GetRequest(string DateFrom, string DateTo,
                   string UserID, string RegionID, string CountryID, string CityID,
                   string Status, string FilterByNameID, string FilterNameID, string PortID,
                   string HotelID, string VehicleID, string VesselID, string Nationality,
                   string Gender, string Rank, string Approved, string Role)
        {
            DataTable dt = null;
            try
            {
                dt = RequestDAL.GetRequest(DateFrom, DateTo, UserID, RegionID, CountryID,
                    CityID, Status, FilterByNameID, FilterNameID, PortID, HotelID, GlobalCode.Field2Int(VehicleID).ToString(),
                    VesselID, Nationality, Gender, Rank, Approved, Role);
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
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel/vehicle request details by Request ID
        /// --------------------------------------------- 
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public static IDataReader GetRequestDetailsByID(string RequestID)
        {
            IDataReader dt = null;
            try
            {
                dt = RequestDAL.GetRequestDetailsByID(RequestID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
         /// <summary>
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   validate if there is pending manual request
        /// --------------------------------------------- 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>
        public static bool IsPendingRequestExists(string Username, string UserRole)
        {
            return RequestDAL.IsPendingRequestExists(Username, UserRole);
        }
        #endregion

        #region "Insert/Update/Delete"
        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert hotel/vehicle request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="SeafarerId"></param>
        /// <param name="VesselId"></param>
        /// <param name="OnOffDate"></param>
        /// <param name="Status"></param>
        /// <param name="IsNeedHotel"></param>
        /// <param name="BranchID"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="IsNeedTransportation"></param>
        /// <param name="Origin"></param>
        /// <param name="OriginRemarks"></param>
        /// <param name="Destination"></param>
        /// <param name="DestinationRemarks"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PortId"></param>
        /// <param name="CityID"></param>
        /// <param name="PortAgentId"></param>
        /// <param name="CostCenterID"></param>
        /// <param name="RankID"></param>
        /// <param name="User"></param>
        public static Int32 RequestInsert(string RequestID, string SeafarerId,
        string VesselId, string OnOffDate, string Status, bool IsNeedHotel,
        string BranchID, string CheckInDate, string CheckOutDate, bool IsNeedTransportation,
        string Origin, string OriginRemarks, string Destination, string DestinationRemarks, string PickupDate,
        string PortId, string CityID, string PortAgentId, string CostCenterID, string RankID, string User)
        {
            Int32 pRequestID = 0;
            pRequestID = RequestDAL.RequestInsert(RequestID, SeafarerId, VesselId, OnOffDate, Status,
                    IsNeedHotel, BranchID, CheckInDate, CheckOutDate, IsNeedTransportation,
                    Origin, OriginRemarks, Destination, DestinationRemarks, PickupDate, PortId, CityID,
                    PortAgentId, CostCenterID, RankID, User);
            return pRequestID;
        }

        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Update hotel/vehicle request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="SeafarerId"></param>
        /// <param name="VesselId"></param>
        /// <param name="OnOffDate"></param>
        /// <param name="Status"></param>
        /// <param name="IsNeedHotel"></param>
        /// <param name="BranchID"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="IsNeedTransportation"></param>
        /// <param name="Origin"></param>
        /// <param name="OriginRemarks"></param>
        /// <param name="Destination"></param>
        /// <param name="DestinationRemarks"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PortId"></param>
        /// <param name="CityID"></param>
        /// <param name="PortAgentId"></param>
        /// <param name="CostCenterID"></param>
        /// <param name="RankID"></param>
        /// <param name="User"></param>
        public static void RequestUpdate(string RequestID, string SeafarerId,
        string VesselId, string OnOffDate, string Status, bool IsNeedHotel,
        string BranchID, string CheckInDate, string CheckOutDate, bool IsNeedTransportation,
        string Origin, string OriginRemarks, string Destination, string DestinationRemarks, string PickupDate,
        string PortId, string CityID, string PortAgentId, string CostCenterID, string RankID,
            string User)
        {
            RequestDAL.RequestUpdate(RequestID, SeafarerId, VesselId, OnOffDate, Status,
                   IsNeedHotel, BranchID, CheckInDate, CheckOutDate, IsNeedTransportation,
                   Origin, OriginRemarks, Destination, DestinationRemarks, PickupDate, PortId, CityID,
                   PortAgentId, CostCenterID, RankID, User);
        }
        /// <summary>
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Approve or disapprove manual request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="IsApproved"></param>
        /// <param name="ApprovedBy"></param>
        public static void ApprovedRequest(string RequestID, bool IsApproved, string ApprovedBy)
        {
            RequestDAL.ApprovedRequest(RequestID, IsApproved, ApprovedBy);
        }
        #endregion
        
    }
}

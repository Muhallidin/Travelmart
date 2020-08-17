using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   08/02/2012
    /// Created By:     Gelo Oquialda
    /// (description)   Create Data Transfer Object (DTO) for ManifestSearchView page
    /// </summary>
    public class ManifestSearchViewDTO
    {
        public static List<ManifestSearchViewList> ManifestSearchViewList { get; set; }
        public List<ManifestSearchViewList> GetManifestSearchViewList(Int16 LoadType, DateTime CurrentDate, string UserID, string Role,
            string OrderBy, int StartRow, int MaxRow, string SeafarerID, string SeafarerLN, string SeafarerFN,
            string RecordLocator, string VesselCode, string VesselName, int RegionID, int CountryID, int CityID, int PortID, int HotelID)
        {
            try
            {
                return ManifestSearchViewList;
            }
            catch
            {
                throw;
            }
            finally
            {
                ManifestSearchViewList = null;
            }
        }
        
        public static int? ManifestSearchViewListCount { get; set; }
        public int GetManifestSearchViewListCount(Int16 LoadType, DateTime CurrentDate, string UserID, string Role, string OrderBy,
            string SeafarerID, string SeafarerLN, string SeafarerFN, string RecordLocator, string VesselCode, string VesselName, int RegionID, int CountryID,
            int CityID, int PortID, int HotelID)
        {
            try
            {
                return (int)ManifestSearchViewListCount;
            }
            catch
            {
                throw;
            }
            finally
            {
                ManifestSearchViewListCount = null;
            }
        }
    }    

    /// <summary>
    /// Date Created:   03/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Manifest List
    /// -------------------------------
    /// Date Modified:  27/03/2012
    /// Modified By:    Josephine Gad
    /// (description)   Add Remarks
    /// -------------------------------
    /// Date Modified:  21/05/2012
    /// Modified By:    Josephine Gad
    /// (description)   Add OriginDestination
    /// -------------------------------
    /// Date Modified:  15/May/2013
    /// Modified By:    Josephine Gad
    /// (description)   Add Tag for MeetGreet, Service Provider and Hotel Vendor
    /// -------------------------------
    /// Date Modified:  25/Jun/2013
    /// Modified By:    Josephine Gad
    /// (description)   Add IsVisible for multiple status single Rec Loc-Status use
    ///                 Add IsAddRequestVisible for Add Request Link
    ///                 Delete DateArrivalDeparture field
    /// -------------------------------
    /// Date Modified:  07/Jan/2015
    /// Modified By:    Josephine Monteza
    /// (description)   Add IsVehicleVendor field
    /// -------------------------------
    /// </summary>
    public class ManifestSearchViewList
    {
        public bool IsAddRequestVisible { get; set; }
        public bool IsVisible { get; set; }
        public string IsManual { get; set; }
        public Int32 IDBigInt { get; set; }
        public int E1TravelRequest { get; set; }
        public Int32 RequestID { get; set; }
        public Int32 TravelRequestID { get; set; }
        public string RecLoc { get; set; }
        public Int64 SfID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOnOff { get; set; }
        //public DateTime? DateArrivalDeparture { get; set; }
        public string Status { get; set; }

        public string Brand { get; set; }
        public string Vessel { get; set; }

        public string PortCode { get; set; }
        public string Port { get; set; }

        public string RankCode { get; set; }
        public string Rank { get; set; }

        public string AirStatus { get; set; }
        public string colAirStatusVarchar { get; set; }

        public string HotelStatus { get; set; }
        public string colHotelStatusVarchar { get; set; }

        public string VehicleStatus { get; set; }
        public string colVehicleStatusVarchar { get; set; }

        public string ReasonCode { get; set; }
        public bool IsWithSail { get; set; }

        public string OriginDestination { get; set; }
        public string Remarks { get; set; }
        public string RemarksURL { get; set; }
        public string RemarksParameter { get; set; }

        public string IsMeetGreet { get; set; }
        public string IsPortAgent { get; set; }
        public string IsHotelVendor { get; set; }
        public string IsVehicleVendor { get; set; }


        public string MGTagDate { get; set; }
        public string PATagDate { get; set; }
        public string HVTagDate { get; set; }
        public string VVTagDate { get; set; }

    }

    /// <summary>
    /// Date Created:   03/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class to call to assign values
    /// ---------------------------------------------------------
    /// Date Modified:  17/May/2013
    /// Modified By:    Josephine Gad
    /// (description)   Add IsAddRequestVisible
    /// </summary>
    public class ManifestSearchViewDTOGenericClass
    {
        public List<ManifestSearchViewList> ManifestSearchViewList { get; set; }
        public int? ManifestSearchViewListCount { get; set; }
        public bool? IsAddRequestVisible { get; set; }       

    }
}


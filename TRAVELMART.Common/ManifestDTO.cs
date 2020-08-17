using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   03/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Create Data Transfer Object (DTO) for Manifest page
    /// </summary>
    public class ManifestDTO
    {
        public static List<ManifestList> ManifestList { get; set; }
        public List<ManifestList> GetManifestList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID, string ViewNoHotelOnly)
        {
            try
            {
                return ManifestList;
            }
            catch
            {
                throw;
            }
            finally
            {
                ManifestList = null;
            }
        }
        public static int? ManifestListCount { get; set; }
        public int GetManifestListCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID, string ViewNoHotelOnly)
        {
            try
            {
                return (int)ManifestListCount;
            }
            catch
            {
                throw;
            }
            finally
            {
                ManifestListCount = null;
            }
        }

        //public static List<TentativeManifest> TentativeManifestList { get; set; }
        //public static int? TentativeManifestCount { get; set; }
    }
    /// <summary>
    /// Date Created:   03/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Manifest List
    /// </summary>
    public class ManifestList
    {        
        public string IsManual { get; set; }
        public Int32 IDBigInt { get; set; }
        public int E1TravelRequest { get; set; }
        public Int32 RequestID { get; set; }
        public Int32 TravelRequestID { get; set; }
        public string RecLoc { get; set; }
        public Int32 SfID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOnOff { get; set; }
        public DateTime? DateArrivalDeparture { get; set; }
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
    }
    /// <summary>
    /// Date Created:   03/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class to call to assign values
    /// </summary>
    public class ManifestDTOGenericClass
    {
        public List<ManifestList> ManifestList { get; set; }
        public int? ManifestListCount { get; set; }       
    }
    /// <summary>
    /// Date Created:    22/02/2012
    /// Created By:      Josephine Gad
    /// (description)    class for hotel tentative manifest
    /// -----------------------------------------------------
    /// Date Modified:    11/Feb/2013
    /// Modified By:      Josephine Gad
    /// (description)     Change Name to LastName and FirstName
    /// -----------------------------------------------------
    /// Date Modified:    15/May/2013
    /// Modified By:      Josephine Gad
    /// (description)     Add Birthdate
    /// -----------------------------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class TentativeManifest
    {
        public Int32 IDBigInt { get; set; }
        public Int32 colTravelReqIdInt { get; set; }
        public string RecLoc { get; set; }
        public Int64 SfID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string colPassportNoVarchar { get; set; }
        public string colPassportExpirationDate { get; set; }
        public string colPassportIssuedDate { get; set; }

        public string Status { get; set; }
        public Int32 RequestID { get; set; }
        public string Vessel { get; set; }
        public string colVesselCodeVarchar { get; set; }

        public string Rank { get; set; }
        public string CostCenter { get; set; }
        public string HotelRequest { get; set; }
        public string colHotelStatusVarchar { get; set; }

        public DateTime? colTimeSpanStartDate { get; set; }
        public DateTime? colTimeSpanEndDate { get; set; }
        public Int16 colTimeSpanDurationInt { get; set; }

        public string colRoomNameVarchar { get; set; }
        public Int32 colRoomNameID { get; set; }
        
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string HotelCity { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }

        public string HotelBranch { get; set; }
        public Int32 HotelBranchID { get; set; }
        public string HotelEmail { get; set; }

        public bool WithEvent { get; set; }

        public DateTime? colArrivalDateTime { get; set; }
        public DateTime? colDepartureDateTime { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }

        public string ArvlCity { get; set; }
        public string DeptCity { get; set; }

        public string colFlightNoVarchar { get; set; }
        public string AL { get; set; }
        public string Carrier { get; set; }

        public Int32 colPortIdInt { get; set; }
        public Int32 CountryID { get; set; }
        public Int32 CityID { get; set; }

        public Int32? Couple { get; set; }
        public DateTime? SignOn { get; set; }
        public decimal? Voucher { get; set; }
        public DateTime? TravelDate { get; set; }
        public string Reason { get; set; }

        public int? colRemarksIDInt { get; set; }
        public string colRemarksVarchar { get; set; }

        public int? E1TravelReqId { get; set; }
        public string Birthdate { get; set; }

    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modifed:   19/07/2012
    /// Description:    Get required fields only; re-order columns
    /// ----------------------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modifed:   11/Feb/2013
    /// Description:    Change Name to LastName and FirstName
    /// </summary>
    public class TentativeManifestExport
    {
        public string HotelCity { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Int16 HotelNights { get; set; }
        public string ReasonCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public Int64 EmployeeId { get; set; }
        public string Gender { get; set; }
        public string SingleDouble { get; set; }
        public string Couple { get; set; }
        public string Title { get; set; }
        public string Ship { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string HotelRequest { get; set; }
        public string RecLoc { get; set; }

        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }
        public string DeptDate { get; set; }
        public string ArvlDate { get; set; }
        public string DeptTime { get; set; }
        public string ArvlTime { get; set; }

        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public decimal Voucher { get; set; }

        public string PassportNo { get; set; }
        public string PassportExpiration { get; set; }
        public string PasportDateIssued { get; set; }
        public string Birthdate { get; set; }

        public string HotelBranch { get; set; }
        public string BookingRemarks { get; set; }
    }
    /// <summary>
    /// Date Created:   22/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class to call to assign values
    /// </summary>
    public class TentativeManifestGenericClass
    {
        public List<TentativeManifest> TentativeManifestList { get; set; }
        public int? TentativeManifestCount { get; set; }
    }
    /// <summary>
    /// Date Created:    20/Mar/2013
    /// Created By:      Josephine Gad
    /// (description)    class for hotel manifest
    /// -----------------------------------------------------
    /// Date Modified:  11/Jun/2013
    /// Modified By:    Josephine Gad
    /// (description)   Add IsHotelVendor field
    /// ==========================================================
    /// Date Modified:  05/Mar/2015
    /// Modified By:    Michael Brian C. Evangelista
    /// Description:    Added fields for flightstats
    /// </summary>
    /// 
    [Serializable]
    public class HotelManifest
    {
        public Int32 IDBigInt { get; set; }
        public Int32 TravelReqIdInt { get; set; }

        public string HotelCity { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public Int16 HotelNights { get; set; }
        public string ReasonCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public Int64 EmployeeId { get; set; }
        public string Gender { get; set; }
        public string SingleDouble { get; set; }
        public string Couple { get; set; }
        public string Title { get; set; }
        public string ShipCode { get; set; }
        public string ShipName { get; set; }

        public string CostCenterCode { get; set; }
        public string CostCenter { get; set; }

        public string Nationality { get; set; }
        public string HotelRequest { get; set; }
        public string RecLoc { get; set; }

        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }
        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }
        public TimeSpan? DeptTime { get; set; }
        public TimeSpan? ArvlTime { get; set; }


        /*flightstats*/
        public string ActualArrivalDate { get; set; }
        public string ActualDepartureDate { get; set; }
        public string ActualArrivalGate { get; set; }
        public string ActualArrivalStatus { get; set; }
        public string ActualArrivalBaggage { get; set; }



        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public decimal Voucher { get; set; }

        public string PassportNo { get; set; }
        public string PasportDateIssued { get; set; }
        public string PassportExpiration { get; set; }

        public string HotelBranch { get; set; }
        public string BookingRemarks { get; set; }
        public bool WithEvent { get; set; }
        public string Status { get; set; }
        public Int32 RequestID { get; set; }


        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }

        public string Remarks { get; set; }
        public bool IsTaggedByUser { get; set; }
        public bool IsTagLinkVisible { get; set; }
        public bool IsHotelVendor { get; set; }

        public string ConfirmationNo { get; set; }
        public DateTime? Birthday { get; set; }
        public long? HotelTransID { get; set; }



        public int? CancellationTermsInt { get; set; }
        public string HotelTimeZoneID { get; set; }
        public DateTime? CutOffTime { get; set; }

        
    }
    /// <summary>
    /// ===============================================================
    /// Author:         Josephine Gad
    /// Date Created:   02/Apr/2013
    /// Description:    Emails address List 
    /// ===============================================================
    /// </summary>
    [Serializable]
    public class EmailRecipient
    {
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
    }
    /// <summary>
    /// ===============================================================
    /// Author:         Josephine Gad
    /// Date Created:   04/Oct/2013
    /// Description:    Vehicle Manifest Manifest
    /// ===============================================================
    /// Author:         Marco Abejar
    /// Date Created:   14/Oct/2013
    /// Description:    Added driver name
    /// ===============================================================
    /// Modified By:    Josephine Gad
    /// Date Modified:  20/Jan/2014
    /// Description:    Add air details
    /// ===============================================================
    /// Modified By:    Josephine Gad
    /// Date Modified:  16/Jun/2014
    /// Description:    Add Passport info
    /// ===============================================================
    /// Modified By:    Michael Brian C. Evangelista
    /// Date Modified:  10/Sep/2014
    /// Description:    Added Tagging variables
    /// </summary>
    [Serializable]
    public class VehicleManifestList
    {

        public Int64 ConfirmManifestID { get; set; }
        public string Remarks { get; set;}
        public Int64 TransVehicleID {get;set;}
        public Int64 SeafarerIdInt {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        public Int64 colIdBigint {get;set;}
        public Int64 colTravelReqIDInt {get; set;} 
        public string RecordLocator {get; set;} 
        public DateTime OnOffDate {get; set;} 
        public Int64? colRequestIDInt {get; set;} 
        public Int64? colVehicleVendorIDInt {get; set;} 
        public string VehicleVendorname {get; set;}
        public string VehiclePlateNoVarchar {get; set;}
        public string DriverName { get; set; }
        public string VehicleDispatchTime { get; set; }

        
        public DateTime? colPickUpDate {get; set;} 
        public TimeSpan? colPickUpTime {get; set;} 
        public DateTime? colDropOffDate {get; set;} 
        public TimeSpan? colDropOffTime {get; set;}  
        
        public string colConfirmationNoVarchar {get; set;}
        public string colVehicleStatusVarchar {get; set;} 
        
        public Int32 colVehicleTypeIdInt {get; set;} 
        public string VehicleTypeName {get; set;} 
        public string  colSFStatus {get; set;} 
        //colRouteIDFromInt, 
        public string RouteFrom {get; set;}
        //colRouteIDToInt, 
        public string RouteTo {get; set;} 
        public string colFromVarchar {get; set;} 
        public string colToVarchar {get; set;}
        public string BookingRemarks {get; set;}
        //colHotelIDInt 
        public string HotelVendorName {get; set;}
        //colRankIDInt, 
        public string RankName {get; set;}
        //colCostCenterIDInt, 
        public string CostCenter {get; set;}
        public string Nationality { get; set; } 

        public bool colIsVisibleBit {get; set;} 
        public Int32? colContractIdInt {get; set;}
        public string Gender {get; set;} 
        public Int32? colVesselIdInt {get; set;} 
        public string VesselName {get; set;}

        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }

        public string FlightNo { get; set; }
        public string Carrier { get; set; }

        public Int16? SeqNo { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }

        public DateTime? DateDep { get; set; }
        public DateTime? DateArr { get; set; }
        /* for tagging */

        public bool TaggedActive { get; set; }
        public string CreatedUserTag { get; set; }
        public string ModifiedUserTag { get; set; }
        public Int32? TaggedVehicleVendorId { get; set; }
        
        /* end tagging */
        
        /* for flightstats*/
        public string ActualDepartureDate { get; set; }
        public string ActualArrivalDate { get; set; }
        public string ActualArrivalGate { get; set; }
        public string ActualArrivalStatus { get; set; }
        public string ActualArrivalBaggage { get; set; }


        public string PassportNo { get; set; }
        public string PassportExp { get; set; }
        public string PassportIssued { get; set; }
        
        public DateTime? Birthday { get; set; }   
        public bool IsToConfirm { get; set; }

        public int? GreeterID { get; set; }
        public string GreeterName { get; set; }

    }
    /// <summary>
    /// Date Created:    27/Nov/2014
    /// Created By:      Josephine Monteza
    /// (description)    Hotel Control No.
    /// -----------------------------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class HotelControlNo
    {
        public Int32 ControlID { get; set; }
        public string ControlNumber{ get; set; }
    }
    /// <summary>
    /// Date Created:    27/Nov/2014
    /// Created By:      Josephine Monteza
    /// (description)    Hotel Cancelation Terms
    /// -----------------------------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class HotelCancelationTerms
    {
        public Int32 CancelationHours { get; set; }
        public TimeSpan? CutoffTime { get; set; }
        public string sTimezone { get; set; }
    }
}

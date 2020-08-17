using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   08/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Create Data Transfer Object (DTO) for NO TravelRequest
    /// ----------------------------------------------------------------------    
    /// Modified By:    Josephine Gad
    /// Date Modified:  16/05/2012
    /// Descrption:     Remove Origin
    /// </summary>
    public class NoTravelRequest
    {
        public int TravelRequestID { get; set; }
        public Int64 SfID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOnOff { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string VesselCode { get; set; }
        public string Vessel { get; set; }
        public string PortCode { get; set; }
        public string Port { get; set; }
        public string RankCode { get; set; }
        public string Rank { get; set; }
        public string ReasonCode { get; set; }
        public string Nationality { get; set; }
        public bool? IsWithSail { get; set; }
        public string NationalityName { get; set; }
        public DateTime? Birthday { get; set; }
        //public string Origin { get; set; }

        public static List<NoTravelRequest> NoTravelRequestList { get; set; }
        public static int NoTravelRequestCount { get; set; }
    }
    /// <summary>
    /// Date Created:   30/03/2012
    /// Created By:     Josephine Gad
    /// (description)   Create Data Transfer Object (DTO) for TravelRequest with ON/OFF date same with Arrival/Departure Date
    /// ----------------------------------------------------------------------
    /// Date Modified:  16/07/2012
    /// Modified by:    Jefferson Bermundo
    /// Description:    Add Hotel Request Column
    /// ----------------------------------------------------------------------
    /// Date Modified:  19/07/2012
    /// Modified by:    Josephine Gad
    /// Description:    Add Booking Remarks Column
    /// </summary>
    public class TravelRequestArrivalDepartureSameDate
    {
        public int TravelRequestID { get; set; }
        public int E1TravelRequest { get; set; }
        public Int64 SfID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOnOff { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string VesselCode { get; set; }
        public string Vessel { get; set; }
        public string PortCode { get; set; }
        public string Port { get; set; }
        public string RankCode { get; set; }
        public string Rank { get; set; }
        public string ReasonCode { get; set; }
        public string Nationality { get; set; }
        public bool? IsWithSail { get; set; }
        public string NationalityName { get; set; }
        //public DateTime? AirArrDepDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public string Airline { get; set; }
        public string FlightNo { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        //public bool HotelRequest { get; set; }
        public string BookingRemarks { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string PassportNo { get; set; }
        public string PasportDateIssued { get; set; }
        public string PassportExpiration { get; set; }
        public DateTime? Birthday { get; set; }


        //public static List<TravelRequestArrivalDepartureSameDate> ArrivalDepartureSameDateList { get; set; }
        public static int ArrivalDepartureSameDateCount { get; set; }
    }
    /// <summary>
    /// Date Created:   18/10/2012
    /// Created By:     Josephine Gad
    /// (description)   List of seafarers with restricted nationality
    /// </summary>
    public class TravelRequestRestrictedNationality
    {
        public string RecLoc { get; set; }
        public int TravelRequestID { get; set; }
        public int E1TravelRequest { get; set; }
        public Int64 SfID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOnOff { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string VesselCode { get; set; }
        public string Vessel { get; set; }
        public string PortCode { get; set; }
        public string Port { get; set; }
        public string RankCode { get; set; }
        public string Rank { get; set; }
        public string ReasonCode { get; set; }
        public string Nationality { get; set; }
        public bool? IsWithSail { get; set; }
        public string NationalityName { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public string Airline { get; set; }
        public string FlightNo { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime? Birthday { get; set; }
    }
    public class TravelRequestRestrictedNationalityGenClass
    {
        public List<TravelRequestRestrictedNationality> TravelRequestRestrictedNationalityList { get; set; }
        public int TravelRequestRestrictedNationalityCount { get; set; }
    }
    /// <summary>
    /// Date Created:   05/07/2012
    /// Created By:     Josephine Gad
    /// (description)   Create Data Transfer Object (DTO) for TravelRequest Remarks
    /// ----------------------------------------------------------------------
    /// </summary>
    public class TravelRequestRemarks
    {
        public int RemarkIDBigInt { get; set; }
        public int TravelReqIdInt { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    /// <summary>
    /// Date Created:   16/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for travel request of meet and greet role
    /// ===============================================================
    /// Date Created:   21/03/2013
    /// Created By:     Marco Abejar
    /// (description)   Add fields for meet and greet
    /// ===============================================================
    /// Date Modified:  29/Aug/2013
    /// Modified By:    Josephine Gad
    /// (description)   Change Arrival Deaprture Datetime to seperate data and time in string format
    ///                 Change Birthday, DateOnOff, SequenceNo to string    
    ///                 Add IsFirstPartition
    /// ===============================================================
    /// </summary>
    /// 
    [Serializable]
    public class MeetGreetTravelRequest
    {
        public Int32 IDBigInt { get; set; }
        public string RecordLocator { get; set; }
        public string Brand { get; set; }
        public string Vessel { get; set; }
        public string Port { get; set; }
        public string Rank { get; set; }
        public Int32 TravelRequestID { get; set; }
        public Int64 SfID { get; set; }
        public int E1TrID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DateOnOff { get; set; }
        public string Hotel { get; set; }
        public DateTime? Checkin { get; set; }        
        public int? Duration { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public string Airline { get; set; }
        public string FlightNo { get; set; }

        public int? AirportID { get; set; }
        public int? SeaportID { get; set; }
        public string PassportNo { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
        public bool IsTaggedByUser { get; set; }

        //added fields
        public DateTime? Checkout { get; set; }        
        public string SingleDouble { get; set; }
        public string Gender { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string MealAllowance { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string TagDateTime { get; set; }
        public string Birthday { get; set; }
        //end

        public string SequenceNo { get; set; }
        public bool IsFirstPartition { get; set; }
    }
    /// <summary>
    /// Date Created:   24/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for travel request of meet and greet role
    /// </summary>
    [Serializable]
    public class MeetGreetTravelRequestGenericClass
    {
        public List<MeetGreetTravelRequest> MeetGreetTravelRequestList { get; set; }
        public int MeetGreetTravelRequestCount { get; set; }

        public List<VesselDTO> VesselList { get; set; }
        public List<NationalityList> NationalityList { get; set; }
        public List<GenderList> GenderList { get; set; }
        public List<RankList> RankList { get; set; }

        public int CountTagged { get; set; }
        public int CountUntagged { get; set; }
        public int CountAll { get; set; }
    }
    /// <summary>
    /// Date Created:   16/Nov/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for travel request of meet and greet AND Service Provider Role for Export to Excel use
    /// </summary>
    /// 
    //[Serializable]
    //public class MeetGreetTravelRequestExport
    //{
    //    public int IDBigInt { get; set; }
    //    public string RecordLocator { get; set; }
    //    public string Brand { get; set; }
    //    public string Vessel { get; set; }
    //    public string Port { get; set; }
    //    public string Rank { get; set; }
    //    public int TravelRequestID { get; set; }
    //    public int SfID { get; set; }
    //    public Int16 E1TrID { get; set; }
    //    public string Name { get; set; }
    //    public string Status { get; set; }
    //    public DateTime? DateOnOff { get; set; }
    //    public string Hotel { get; set; }
    //    public DateTime? Checkin { get; set; }
    //    public int? Duration { get; set; }
    //    public string Departure { get; set; }
    //    public string Arrival { get; set; }
    //    public DateTime? DepartureDateTime { get; set; }
    //    public DateTime? ArrivalDateTime { get; set; }
    //    public string Airline { get; set; }
    //    public string FlightNo { get; set; }

    //    public int? AirportID { get; set; }
    //    public int? SeaportID { get; set; }

    //    public string PassportNo { get; set; }
    //    public DateTime? PassportIssueDate { get; set; }
    //    public DateTime? PassportExpiryDate { get; set; }

    //    public bool IsTaggedByUser { get; set; }
    //}
    /// <summary>
    /// Date Created:   16/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for travel request of meet and greet role
    /// ===============================================================
    /// Date Created:   21/03/2013
    /// Created By:     Marco Abejar
    /// (description)   Add fields for meet and greet
    /// ===============================================================
    /// Date Modified:  29/Aug/2013
    /// Modified By:    Josephine Gad
    /// (description)   Change Arrival Deaprture Datetime to seperate data and time in string format
    ///                 Change Birthday, DateOnOff, SequenceNo to string    
    ///                 Add IsFirstPartition
    /// ===============================================================
    /// </summary>
    /// 
    [Serializable]
    public class PortAgentRequest
    {
        public Int32 IDBigInt { get; set; }
        public string RecordLocator { get; set; }
        public string Brand { get; set; }
        public string Vessel { get; set; }
        public string Port { get; set; }
        public string Rank { get; set; }
        public Int32 TravelRequestID { get; set; }
        public Int64 SfID { get; set; }
        public int E1TrID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DateOnOff { get; set; }
        public string Hotel { get; set; }
        public string Checkin { get; set; }
        public int? Duration { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }

        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public string Airline { get; set; }
        public string FlightNo { get; set; }

        public int? AirportID { get; set; }
        public int? SeaportID { get; set; }
        public string PassportNo { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
        public bool IsTaggedByUser { get; set; }

        //added fields
        public string Checkout { get; set; }
        public string SingleDouble { get; set; }
        public string Gender { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string MealAllowance { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string TagDateTime { get; set; }
        public string Birthday { get; set; }

        public bool IsHotel { get; set; }
        public bool IsTransportation { get; set; }
        public bool IsMAG { get; set; }
        public bool IsLuggage { get; set; }
        public bool IsSafeguard { get; set; }
        public bool IsVisa { get; set; }
        public bool IsOther { get; set; }
        //end

        public string SequenceNo { get; set; }
        public bool IsFirstPartition { get; set; }
    }

    /// <summary>
    /// Date Created:   24/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for travel request of meet and greet role
    /// </summary>
    [Serializable]
    public class PortAgentRequestGenericClass
    {
        public List<PortAgentRequest> PortAgentRequestList { get; set; }
        public int PortAgentRequestCount { get; set; }

        public List<VesselDTO> VesselList { get; set; }
        public List<NationalityList> NationalityList { get; set; }
        public List<GenderList> GenderList { get; set; }
        public List<RankList> RankList { get; set; }

        public int CountTagged { get; set; }
        public int CountUntagged { get; set; }
        public int CountAll { get; set; }
    }
}

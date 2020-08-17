using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    [Serializable]
    public class NonTurnPortsList
    {
        public long HotelTransID { get; set; }
        public long IDBigInt {get;set;}
        public long TravelReqID {get;set;}
        public int E1TravelReqID { get; set; }
        public int RoomTypeId { get; set; }
        public int PortId { get; set; }
        public string SFStatus { get; set; }
        public int? SeqNo { get; set; }
        public string ONOFFDATE { get; set; }

        public string HotelCity {get;set;}
        public string Checkin {get;set;}
        public string CheckOut {get;set;}
        public string HotelNite {get;set;}
        public string HotelName { get; set; }
        public string LastName {get;set;}
        public string FirstName { get; set; }

        public long ? Employee {get;set;}
        public string Gender {get;set;}
        public string SingleDouble {get;set;}
        public string Couple {get;set;}
        public string Title {get;set;}
        public string Ship {get;set;}
        public string Costcenter {get;set;}
        public string Nationality {get;set;}
        public string HotelRequest {get;set;}
        public string RecLoc {get;set;}
        public long ? RecLocID { get; set; }
        public int? AirSequence { get; set; }
        public string deptCity {get;set;}
        public string ArvlCity {get;set;}
        public string Arvldate {get;set;}
        public string ArvlTime {get;set;}
        public string Deptdate {get;set;}
        public string DeptTime {get;set;}

        public string ArvlCityName { get; set; }
        public string deptCityName { get; set; }

        public string Carrier {get;set;}
        public string FlightNo {get;set;}
        public string Voucher {get;set;}
        public string PassportNo {get;set;}
        public string PassportExp {get;set;}
        public string PassportIssued {get;set;}
        public string HotelBranch {get;set;}
        public string Booking {get;set;}
        public string Bookingremark { get; set; }
        public bool IsVisible { get; set; }
        public decimal stripes { get; set; }

        public string ConfirmedBy { get; set; }
        public string ConfirmedDate { get; set; }

        public string Remarks { get; set; }
        public Int16? GroupNo { get; set; }

        public string PortName { get; set; }
        public string Birthday { get; set; }

        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }


        public string ServiceRequested { get; set; }
        public string ServiceRequestedDate { get; set; }
        public int StatusID { get; set; }
        public bool IsMedical { get; set; }


        public Double? ContractedRate { get; set; }
        public Double? ConfirmedRate { get; set; }

        //public List<NonTurnPortsListAir> NonTurnPortsListAir { get; set; }

    }


    public class GenericNonTurnPort {

        public List<NonTurnPortsList> NonTurnPortsRequest { get; set; }
        public List<NonTurnPortsList> NonTurnPortsConfirm { get; set; }
        public List<NonTurnPortsList> NonTurnPortsCancel { get; set; }
    
    
    }



    /// <summary>
    /// ===============================================================
    /// Author:         Josephine Gad
    /// Date Created:   17/03/2013
    /// Description:    Service Provider's emails address List 
    /// ===============================================================
    /// </summary>
    [Serializable]
    public class PortAgentEmail
    {
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
    }

    [Serializable]
    public class NonTurnTransportation
    { 
    
        public int?     VendorID  { get; set; }
        public string     VendorName { get; set; }
        public string       VendorContractName { get; set; }
        public int  ContractID  { get; set; }
        public int  CurrentcyID { get; set; }
        public string   Currency  { get; set; }
        public double   Rate  { get; set; }
        public string Email { get; set; }
    
    }

    [Serializable]
    public class PortAgenRequestVendor
    { 
    

        public int? VendorID { get; set; }
        public string VendorName { get; set; }
        public string  VendorContractName { get; set; }
        public int? VendorContractID  { get; set; }
        public int?  CurrentID  { get; set; }
        public string CurrentName  { get; set; }

        public decimal? MealStandard  { get; set; }
        public decimal? MealIncrease  { get; set; }
        public decimal? SingleRate  { get; set; }
        public decimal? DoubleRate { get; set; }
        public decimal? Voucher  { get; set; }
        public string EmailTo { get; set; }



    
    }
    [Serializable]
    public class NonTurnportGenericList
    {


        public List<PortAgentVehicleManifestList> PortAgentVehicleManifestList { get; set; } 

        public List<PortAgenRequestVendor> PortAgenRequestVendor { get; set; }
        public List<NonTurnTransportation> NonTurnTransportation { get; set; }
        public List<NonTurnPortsList> NonTurnPortsLists { get; set; }
        public List<ComboGenericClass> Currency { get; set; }

        public List<VehicleType> VehicleType { get; set; }

        public List<ComboGenericClass> Requestor { get; set; }

    
    }

    [Serializable]
    public class NonTurnRequestBooking
    {
        
        
        public long?       TransHotelID { get; set; }  
        public long?       TravelReqID  { get; set; }  
        public long?       SeafarerID  { get; set; } 
        public long?       IdBigint  { get; set; }  
        public string      RecordLocator  { get; set; }  
        public int?        SeqNo  { get; set; }  
        public int?        VendorID  { get; set; }  
        public int?        RoomTypeID  { get; set; }  
        public DateTime ?  CheckIn  { get; set; }
        public DateTime?   CheckOut { get; set; }  
        public int?        Duration  { get; set; }  
        public double?     VoucherAmount  { get; set; } 
        public int?        ContractID  { get; set; }  
        public string      ApprovedBy  { get; set; }  
        public DateTime ?  ApprovedDate  { get; set; }  
        public string      HotelCity  { get; set; }
        public float?      RoomCount  { get; set; } 
        public string      HotelName  { get; set; }  
        public double?     ConfirmRateMoney  { get; set; }  
        public double?     ContractedRateMoney  { get; set; }  
        public string      EmailTo  { get; set; } 
        public string      EmailCC  { get; set; } 
        public string      Comment  { get; set; } 
        public int?        Currency  { get; set; }  
        public string      ConfirmBy  { get; set; }  
        public int?        StatusID  { get; set; }  
        public bool?       IsMedical  { get; set; }  
        public string      UserID  { get; set; } 

    }


    [Serializable]
    public class NonTurnTransportationRequest
    {
        public long? TransVehicleID { get; set; } 
        public long? SeafarerID  { get; set; } 
        public long? IdBigint  { get; set; }
        public long? TravelReqID { get; set; } 
        public string RecordLocator  { get; set; } 
        public int? RequestID  { get; set; } 
        public int? VehicleVendorID  { get; set; } 
        public string VehiclePlateNo  { get; set; } 
        public DateTime? PickUpDate  { get; set; } 
        public DateTime? PickUpTime  { get; set; } 
        public DateTime? DropOffDate  { get; set; } 
        public DateTime? DropOffTime  { get; set; } 
        public string  ConfirmationNo  { get; set; } 
        public string VehicleStatus  { get; set; } 
        public int? VehicleTypeId  { get; set; } 
        public string SFStatus  { get; set; } 
        public int? RouteIDFrom  { get; set; } 
        public int? RouteIDTo  { get; set; } 
        public string FromVarchar  { get; set; } 
        public string ToVarchar  { get; set; } 
        public string RemarksForAudit  { get; set; } 
        public int? HotelID  { get; set; } 
        public int? ContractId  { get; set; } 
        public int? SeqNoInt  { get; set; } 
        public int? DriverID  { get; set; } 
        public string VehicleDispatchTime  { get; set; } 
        public string RouteFromVarchar  { get; set; } 
        public string RouteToVarchar  { get; set; } 
        public long? VehicleUnset  { get; set; } 
        public string VehicleUnsetBy  { get; set; } 
        public DateTime? VehicleUnsetDate  { get; set; } 
        public string ConfirmBy  { get; set; }
        public string Comments { get; set; }
        public double ConfirmedRateMoney { get; set; }
        public short StatusID { get; set; }

    }


    [Serializable]
    public class NonTurnTransportationCancelRequest
    {

        public long? TransVehicleID { get; set; }
        public long? SeafarerID { get; set; }
        public long? IdBigint { get; set; }
        public long? TravelReqID { get; set; }
        public string RecordLocator { get; set; }
        public int? RequestID { get; set; }
        public int? VehicleVendorID { get; set; }
        public string VehiclePlateNo { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? DropOffDate { get; set; }
        public DateTime? DropOffTime { get; set; }
        public string ConfirmationNo { get; set; }
        public string VehicleStatus { get; set; }
        public int? VehicleTypeId { get; set; }
        public string SFStatus { get; set; }
        public int? RouteIDFrom { get; set; }
        public int? RouteIDTo { get; set; }
        public string FromVarchar { get; set; }
        public string ToVarchar { get; set; }
        public string RemarksForAudit { get; set; }
        public int? HotelID { get; set; }
        public int? ContractId { get; set; }
        public int? SeqNoInt { get; set; }
        public int? DriverID { get; set; }
        public string VehicleDispatchTime { get; set; }
        public string RouteFromVarchar { get; set; }
        public string RouteToVarchar { get; set; }
        public long? VehicleUnset { get; set; }
        public string VehicleUnsetBy { get; set; }
        public DateTime? VehicleUnsetDate { get; set; }
        public string ConfirmBy { get; set; }
        public string Comments { get; set; }


    }

}

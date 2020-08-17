using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Modified By:    Josephine Monteza
    /// Modified Date:  14/Jul/2014
    /// (desciption)    Create Manifest list for Immigration
    /// </summary>
    [Serializable]
    public class ImmigrationManifestList
    {

        public long IDBigInt { get; set; }
        public int E1TravelRequest { get; set; }
        public int RequestID { get; set; }
        public long TravelRequestID { get; set; }
        public string RecLoc { get; set; }
        public long SeafarerID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOnOff { get; set; }
        public DateTime? DateArrivalDeparture { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string Vessel { get; set; }
        public string Port { get; set; }
        public string Rank { get; set; }

        public string ReasonCode { get; set; }
        public bool IsWithSail { get; set; }

        public string Arrival { get; set; }
        public string Departure { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public string FlightNo { get; set; }
        public string Airline { get; set; }

        public string Hotel { get; set; }

        public string PassportNo { get; set; }
        public string PassportIssued { get; set; }
        public string PassportExp { get; set; }

        public bool IsMeetGreet { get; set; }
        public bool IsPortAgent { get; set; }
        public bool IsHotelVendor { get; set; }

        public string Remarks { get; set; }

        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public string SingleDouble { get; set; }
        public string Gender { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string MealAllowance { get; set; }
        public int? Duration { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int SequenceNo { get; set; }
        public bool IsVisible { get; set; }
        public DateTime? Birthday { get; set; }

        public int? TransVehicleID { get; set; }
        public bool IsCheckBoxForVehicleVisible { get; set; }
        public bool IsVisibleToVehicleVendor { get; set; }
        public bool IsNeedVehicleBooking { get; set; }
        public bool IsToPrintItinerary { get; set; }

        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }
        public DateTime? PickupDatetime { get; set; }

        public string VehicleName { get; set; }
        public string Confirm { get; set; }

        public string ConfirmBy { get; set; }
        public string ConfirmDate { get; set; }

    }


    [Serializable]
    public class EmployeeParent 
    {

        public long? EmployeeID { get; set; } 
        public string FatherName { get; set; }
        public string MotherName { get; set; }
    
    }



    /// <summary>
    /// Modified By:    Muhallidin G Wali
    /// Modified Date:  29/Mar/2015
    /// (desciption)    Emlployee Immigration Name
    /// </summary>
    /// 
    [Serializable]
    public class CrewImmigration : SeafarerInfo
    {
        public long? CrewVericationID { get; set; }
        public string LOEControlNumber { get; set; }
        public string ReprintSequence { get; set; } 
        public DateTime?  SignOnDate { get; set; } 
        public string CostCenter { get; set; }  
        public DateTime?  LOEIssueDate { get; set; } 
        public string Lemcu { get; set; }  
        public string ArrivalSequence { get; set; }  
        public string UpdateFlag1 { get; set; }
        public string UpdateFlag2 { get; set; }
        public string UpdateFlag3 { get; set; }
        public string UpdateFlag4 { get; set; }
        public string UpdateFlag5 { get; set; }
        public string LOEIssueID { get; set; }  
        public bool? NewHire { get; set; }

        public int VesselID { get; set; }
        public string VesselCode { get; set; }
        public string Vessel { get; set; }
         
        public int SeaportID { get; set; }
        public string SeaportCode { get; set; }
        public string Seaport { get; set; }

        public int BrandID { get; set; }
        public string Brand { get; set; }
        public string BrandCode { get; set; }


        public long? TravelReqID { get; set; }
        public long? IDBigint { get; set; }
        public string RecordLocator { get; set; }
        public int? ItinerarySeqNo { get; set; }

        public DateTime?  Joindate { get; set; }
        public DateTime? DateHired { get; set; }
        public string JoinPort { get; set; }
        public string JoinCity { get; set; }

        public int? Reason { get; set; }
        public bool? IsFraudulentDoc { get; set; }
        public bool? IsPriorImmigIssues { get; set; }
        public bool? IsPriorConDep { get; set; }
        public bool? IsOther { get; set; }
        public bool? IsApproved { get; set; }
        public string OtherDetail { get; set; }

        public string UserID { get; set; }


        public string UserName { get; set; }
        public DateTime?  ProcessDate { get; set; }


        public List<ImmigrationAirTransaction> ImmigrationAirTransaction { get; set; }
        public List<ImmigrationHotelBooking> ImmigrationHotelBooking { get; set; }
        public List<ImmigrationTransportion> ImmigrationTransportion { get; set; }
        public List<ImmigrationEmploymentHistory> ImmigrationEmploymentHistory { get; set; }

        public List<SeafarerImage> SeafarerImage { get; set; }

        public List<EmployeeParent> Parent { get; set; }


        public CtracDetail CtracDetail { get; set; }


    }

    [Serializable]
    public class CtracDetail
    {
        public string user_id { get; set; }
        public string jde_id { get; set; }
    
    }


    [Serializable]
    public class SeafarerInfo
    {

        public long? SeaparerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string PassportNo { get; set; }
        public string EmailAdd { get; set; }

        public string PassportExpiredate { get; set; }
        public string PassportIssuedate { get; set; }

        public int NatioalityID { get; set; }
        public string NationalityCode { get; set; }
        public string Nationality { get; set; }


        public int RankID { get; set; }
        public string RankCode { get; set; }
        public string Rank { get; set; }

    }
    
    [Serializable]
    public class SeafarerImage
    {

        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        public string LOENumber { get; set; }
        public long? SeaparerID { get; set; }
        public string LogInUser { get; set; }
    
    }



    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load Rout
    ///// </summary>
    ///// 
    //public interface INationality
    //{

    //     int NatioalityID { get; set; }
    //     string NationalityCode { get; set; }
    //     string Nationality { get; set; }
    //}

     
    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load Rout
    ///// </summary>
    ///// 
    //public interface IVessel
    //{

    //     int VesselID { get; set; }
    //     string VesselCode { get; set; }
    //     string Vessel { get; set; }
    //}

    //public interface  IBranchName
    //{

    //     string BranchID { get; set; }
    //     string BranchCode { get; set; }
    //     string BranchName { get; set; }
    //     string Email { get; set; }
    //}

    //public interface IVehicleVendor
    //{

    //     long? VehicleVendorID { get; set; } 
    //     string VehicleVendorName { get; set; }  
    //     string ContactNo { get; set; }  
    //     string FaxNo { get; set; }  
    //     string ContactPerson { get; set; }  
    //     string Address { get; set; } 
    //     string EmailCc { get; set; } 
    //     string EmailTo { get; set; } 
    //     string Website { get; set; }  

    //}


    // <summary>
    /// Author:         Muhallidin G Wali
    /// Date Created:   08/02/2013
    /// Description:    Load rank
    /// </summary>
    /// 
    //public interface IRankCode
    //{

    //     int RankID { get; set; }
    //     string RankCode { get; set; }
    //     string Rank { get; set; }
    //}

    //// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load rank
    ///// </summary>
    ///// 
    //public interface IAirportCode
    //{

    //     int AirportID { get; set; }
    //     string AirportCode { get; set; }
    //     string Airport { get; set; }
    //}

    //// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load rank
    ///// </summary>
    ///// 
    //public interface ISeaportCode
    //{

    //     int SeaportID { get; set; }
    //     string SeaportCode { get; set; }
    //     string Seaport { get; set; }

    //}
    [Serializable]
    public class  Recordlocator
    { 
        public long IdBigint { get; set; } 
        public string RecordLocator { get; set; } 
        public short? ItinerarySeqNo { get; set; } 
        public  string NameReference { get; set; }  
        public  string Origin { get; set; } 
        public  string Destination { get; set; } 
        public long? TravelReqIDInt { get; set; }
    }
    [Serializable]
    public class ImmigrationAirTransaction : Recordlocator
    {
     
        public int ItinerarySeqNo { get; set; }
        public int SeqNo { get; set; }
        public string AirlineCode { get; set; }

        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }

        public string DepartureAirportLocationCode { get; set; }
        public string ArrivalAirportLocationCode { get; set; }
        public string SeatNo { get; set; }
        public string TicketNo { get; set; }
        public string AirStatus { get; set; }
        public bool IsBilledToCrew { get; set; }
        public string ActionCode { get; set; }
        public int OrderNo { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string FligthNo { get; set; }
        public string AirLine { get; set; }


 
    }

    /// <summary>
    /// Date Modified: 25/Nov/2013
    /// Modified By:   Muhallidin G Wali
    /// (description)   
    /// </summary>
    /// 
    [Serializable]
    public class ImmigrationTransportion : Recordlocator
    {

        public long? ReqVehicleIDBigint { get; set; }
        public string ReqVehicleVarchar { get; set; }
      
        public short? SeqNoInt { get; set; }
        public string VehiclePlateNoVarchar { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? DropOffDate { get; set; }
        public DateTime? DropOffTime { get; set; }
        public string ConfirmationNoVarchar { get; set; }
        public string VehicleStatusVarchar { get; set; }
        public int? VehicleTypeIdInt { get; set; }
        public string SFStatus { get; set; }
        public int? RouteIDFromInt { get; set; }
        public int? RouteIDToInt { get; set; }
        public string FromVarchar { get; set; }
        public string ToVarchar { get; set; }
        public string UserID { get; set; }
        public string Comment { get; set; }
        public string TransSender { get; set; }
        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }
        public string ConfirmBy { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public bool? IsPortAgent { get; set; }
        public bool? IsMedical { get; set; }
        public double? ConfirmRate { get; set; }
        public string TranspoCost { get; set; }
        public short? ReqSourceID { get; set; }

        public int? StatusID { get; set; }
        public string ColorCode { get; set; }
        public string ForeColor { get; set; }



        public long? VehicleVendorID { get; set; }
        public string VehicleVendorName { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string EmailCc { get; set; }
        public string EmailTo { get; set; }
        public string Website { get; set; }  

    }


    /// <summary>
    /// Date Created:    22/10/2013
    /// Created By:      Muhallidin G Wali
    /// (description)    Get Hotel transaction Other
    /// </summary>
    [Serializable]
    public class ImmigrationHotelBooking :  Recordlocator 
    {

        public long? TransHotelID { get; set; }
        public string ReserveUnderName { get; set; }
        public string ConfirmationNo { get; set; }
        public string HotelStatus { get; set; }
        public string Remarks { get; set; }
        public Decimal? VoucherAmount { get; set; }
        public long? ContractID { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ContractFrom { get; set; }
        public string RemarksForAudit { get; set; }
        public string HotelCity { get; set; }

        public DateTime? TimeSpanStartDate { get; set; }
        public DateTime? TimeSpanStartTime { get; set; }
        public DateTime? TimeSpanEndDate { get; set; }
        public DateTime? TimeSpanEndTime { get; set; }

        public int? TimeSpanDurationInt { get; set; }
        public string RoomType { get; set; }

        public int? StatusID { get; set; }
        public string ColorCode { get; set; }
        public string ForeColor { get; set; }
        public string ConfirmBy { get; set; }

        public string BranchID { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Email { get; set; }

    }



    /// <summary>
    /// Date Created:    22/10/2013
    /// Created By:      Muhallidin G Wali
    /// (description)    Employment history
    /// </summary>
    [Serializable]
    public class ImmigrationEmploymentHistory : CrewImmigration //Recordlocator
    {

        
        public DateTime? SignOnDate { get; set; }
        public DateTime? SignOffDate { get; set; }
        public int? ShipID { get; set; }
        public string Ship { get; set; }
        public int? RankID { get; set; }
        public string Rank { get; set; }
        public string ColorCode { get; set; }
        public string ForeColor { get; set; }


    }

    /// <summary>
    /// Date Created:    22/10/2013
    /// Created By:      Muhallidin G Wali
    /// (description)    Employment history
    /// </summary>
    [Serializable]
    public class ImmigrationCompany
    { 
    
    
        public int? ImmigrationCompanyID { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }

        public string Country  { get; set; }
        public string City  { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string EmailAdd { get; set; }
        public string Contact { get; set; }
        public string UserID { get; set; }    
    
    }

    /// <summary>
    /// Date Created:    22/10/2013
    /// Created By:      Muhallidin G Wali
    /// (description)    Employment history
    /// </summary>
    [Serializable]
    public class ImmigrationCompanyGenericClass
    {

        public List<ImmigrationCompany> ImmigrationCompany { get; set; }
        public List<Country> Country { get; set; }  
    
    
    }







}

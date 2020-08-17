using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Created By:         Jefferson Bermundo
    /// Date Created:       July 27, 2012
    /// Description:        Class Model For Contract Hotel Attachments
    /// </summary>
    public class ContractHotelAttachment
    {
        public int AttachmentId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] uploadedFile { get; set; }
        public int colContractId { get; set; }
        public DateTime UploadedDate { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       08/Aug/2013
    /// Description:        Class Model For Contract Vehicle
    /// ---------------------------------------------------
    /// Modified By:        Josephine Gad
    /// Date Modifed:       11/Jul/2014
    /// Description:        Add field CssContractAmendVisible
    /// </summary>
    /// 
    [Serializable]
    public class ContractVehicle
    {
        public Int64 ContractID  { get; set; }
        public string VehicleName { get; set; }
        public string ContractStatus { get; set; }
        public string ContractName { get; set; }
        public string ContractDateStart { get; set; }
        public string ContractDateEnd { get; set; }

        public string Remarks { get; set; }
        public Int64 BranchID { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }

        public DateTime DateCreated { get; set; }
        public string CssContractAmendVisible { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       16/Aug/2013
    /// Description:        Class Model For Contract Vehicle Attachments
    /// </summary>
    [Serializable]
    public class ContractVehicleAttachment
    {
        public int AttachmentId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] uploadedFile { get; set; }
        public int colContractId { get; set; }
        public DateTime UploadedDate { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       25/Sept/2013
    /// Description:        Vehicle Contract Details
    /// -------------------------------------------------
    /// Modified  By:       Josephine Gad
    /// Date Modified:      17/Jul/2014
    /// Description:        Add IsRCI, IsAZA, IsCEL, IsPUL	
    /// </summary>
    /// 
    [Serializable]
    public class ContractVehicleDetails
    {
        public Int64 ContractID { get; set; }
        public Int64 BranchID { get; set; }

        public string VehicleName { get; set; }
        public Int64 CityID { get; set; }
        public string CityName { get; set; }
        public Int64 CountryID { get; set; }
        public string CountryName { get; set; }

        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }

        public string EmailCc { get; set; }
        public string EmailTo { get; set; }

        public string FaxNo { get; set; }
        public DateTime? ContractDateStart { get; set; }
        public DateTime? ContractDateEnd { get; set; }
        public string ContractName { get; set; }        
        public string ContractStatus { get; set; }

        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }

        public DateTime? RCCLAcceptedDate { get; set; }
        public string RCCLPersonnel { get; set; }

        public DateTime? VendorAcceptedDate { get; set; }
        public string VendorPersonnel { get; set; }

        public string Remarks { get; set; }
        public bool IsAirportToHotel { get; set; }
        public bool IsHotelToShip { get; set; }

        public bool IsRCI { get; set; }
        public bool IsAZA { get; set; }
        public bool IsCEL { get; set; }
        public bool IsPUL { get; set; }
        public bool IsSKS { get; set; }
         
    }
    /// <summary>
    /// Date Created:   30/Sep/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Contract Details
    /// --------------------------------------------------
    /// Date Modified:  3/Oct/2013
    /// Created By:     Marco Abejar
    /// (description)   Added Route From & To Fields
    /// </summary>
    [Serializable]
    public class ContractVehicleDetailsAmt
    {
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int BranchID { get; set; }
        public int ContractVehicleCapacityID { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public int RouteIDFrom { get; set; }
        public int RouteIDTo { get; set; }
        //public int RouteID { get; set; }
        //public string Route { get; set; }   
        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }   
        public string Origin { get; set; }
        public string Destination { get; set; }
        public float RateAmount { get; set; }
        public float Tax { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       25/Sept/2013
    /// Description:        Currency List
    /// </summary>
    /// 
    [Serializable]
    public class Currency
    {
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       13/Nov/2013
    /// Description:        Class Model For Contract Service Provider
    /// </summary>
    /// 
    [Serializable]
    public class ContractPortAgent
    {
        public Int64 ContractID { get; set; }
        public string PortAgentName { get; set; }
        public string ContractStatus { get; set; }
        public string ContractName { get; set; }
        public string ContractDateStart { get; set; }
        public string ContractDateEnd { get; set; }

        public string Remarks { get; set; }
        public Int64 BranchID { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }

        public DateTime DateCreated { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       11/Nov/2013
    /// Description:        Service Provider Contract Details
    /// =====================================================
    /// Modified By:        Josephine Gad
    /// Date Modified:      10/Apr/2014
    /// Description:        Add IsAirportToHotel and IsHotelToShip
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetails
    {
        public Int64 ContractID { get; set; }
        public Int64 PortAgentID { get; set; }

        public string PortAgentName { get; set; }
        public Int64 CityID { get; set; }
        public string CityName { get; set; }
        public Int64 CountryID { get; set; }
        public string CountryName { get; set; }

        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }

        public string EmailCc { get; set; }
        public string EmailTo { get; set; }

        public string FaxNo { get; set; }
        public DateTime? ContractDateStart { get; set; }
        public DateTime? ContractDateEnd { get; set; }
        public string ContractName { get; set; }
        public string ContractStatus { get; set; }

        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }

        public DateTime? RCCLAcceptedDate { get; set; }
        public string RCCLPersonnel { get; set; }

        public DateTime? VendorAcceptedDate { get; set; }
        public string VendorPersonnel { get; set; }

        public string Remarks { get; set; }

        public bool IsAirportToHotel { get; set; }
        public bool IsHotelToShip { get; set; }

        public bool IsRCI { get; set; }
        public bool IsAZA { get; set; }
        public bool IsCEL { get; set; }
        public bool IsPUL { get; set; }
        public bool IsSKS { get; set; }

    }
    /// Created By:         Josephine Gad
    /// Date Created:       11/Nov/2013
    /// Description:        Class Model For Contract Service Provider Attachments
    /// </summary>
    [Serializable]
    public class ContractPortAgentAttachment
    {
        public int AttachmentId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] uploadedFile { get; set; }
        public int colContractId { get; set; }
        public DateTime UploadedDate { get; set; }
    }
    /// Date Modified:  3/Oct/2013
    /// Created By:     Marco Abejar
    /// (description)   Added Route From & To Fields
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmt
    {
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int PortAgentID { get; set; }
        public int ContractVehicleCapacityID { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public int RouteIDFrom { get; set; }
        public int RouteIDTo { get; set; }
        //public int RouteID { get; set; }
        //public string Route { get; set; }   
        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public float RateAmount { get; set; }
        public float Tax { get; set; }
    }
    /// Date Modified:  01/Mar/2014
    /// Modified By:    Josephine Gad
    /// (description)   Hotel details in Service Provider Contract
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmtHotel
    {
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int PortAgentID { get; set; }

        public bool IsRateByPercentBit {get; set;}
        public float RoomCostPercent {get; set;}
        
        public float RoomRateTaxPercentage {get; set;}
        public bool IsTaxInclusive  {get; set;}

        public float RoomSingleRate {get; set;}
        public float RoomDoubleRate {get; set;}
        public float MealStandardDecimal {get; set;}
        public float MealIncreasedDecimal {get; set;}
        public float MealTax { get; set; }

        public float SurchargeSingle {get; set;}
        public float SurchargeDouble { get; set; }

        public float BreakfastRate { get; set; }
        public float LunchRate { get; set; }
        public float DinnerRate { get; set; }
        public float MiscellaneaRate { get; set; }

    }
    /// Date Modified:   01/Mar/2014
    /// Modified By:     Josephine Gad
    /// (description)    Luggage details in Service Provider Contract
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmtLuggage
    {
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int PortAgentID { get; set; }

        public float LuggageRate { get; set; }
        public int LuggageUOMId { get; set; }
    }   
    /// Date Modified:   01/Mar/2014
    /// Modified By:     Josephine Gad
    /// (description)    Luggage UOM
    /// </summary>
    [Serializable]
    public class LuggageUOM
    {        
        public int LuggageUOMId { get; set; }
        public string LuggageUOMName { get; set; }
    }
    /// Date Modified:   01/Mar/2014
    /// Modified By:     Josephine Gad
    /// (description)    Safeguard details in Service Provider Contract
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmtSafeguard
    {
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int PortAgentID { get; set; }

        public float SafeguardRate { get; set; }
        public int SafeguardUOMId { get; set; }
        public string SafeguardUOMName { get; set; }
    }
    /// Date Modified:   04/Apr/2014
    /// Modified By:     Josephine Gad
    /// (description)    Meet & Greet details
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmtMeetGreet
    {
        public decimal MeetGreetRate { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public bool IsSurchargePercent { get; set; }
        public decimal MeetGreetSurcahrgeFee { get; set; }
        public string MeetGreetRemarks { get; set; }
    }
    /// Date Modified:   01/Mar/2014
    /// Modified By:     Josephine Gad
    /// (description)    Luggage UOM
    /// </summary>
    [Serializable]
    public class SafeguardUOM
    {
        public int SafeguardUOMId { get; set; }
        public string SafeguardUOMName { get; set; }
    }
    /// Date Modified:   22/Apr/2014
    /// Modified By:     Josephine Gad
    /// (description)    Visa details in Service Provider Contract
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmtVisa
    {
        public float VisaAmount { get; set; }
        public float VisaAccompany { get; set; }
        public float ImmigrationFees { get; set; }
        public float ImmigrationFees2 { get; set; }
        public float LetterOfInvitation { get; set; }
        public float BusinessParoleRequest { get; set; }
        public float BusinessParoleProcessingFee { get; set; }
        public float ImmigrationPortCaptaincyLetter { get; set; }
        public string VisaRemarks { get; set; }
    }
    /// Date Modified:   22/Apr/2014
    /// Modified By:     Josephine Gad
    /// (description)    Other details in Service Provider Contract
    /// </summary>
    [Serializable]
    public class ContractPortAgentDetailsAmtOther
    {
        public float ShorePassesRate {get;set;} 
        public float AnsweringTelephoneCallsEmailRate {get; set;}
        public float LostLuggageRate {get; set;} 
        public float CarRate {get; set;} 
        public float ImmigrationCustodyServiceAirportHotelRate {get; set;} 
        public float ImmigrationCustodyServiceHotelRate {get; set;}
        public float ImmigrationCustodyServiceHotelShipRate {get; set;}
        public float TransportationToPharmacyRate {get; set;}
        public float TransportationToMedicalFacilityRate {get; set;}
        public float WaitingTimeRate {get; set;}
        public string OtherRemarks { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Monteza
    /// Date Created:       24/Sept/2015
    /// Description:        Hotel Contract 
    /// </summary>
    [Serializable]
    public class ContractHotel
    {
        public Int64 contractID { get; set; }
        public string contractStatus { get; set; }
        public DateTime? contractStartDate { get; set; }
        public DateTime? contractEndDate { get; set; }
    }

    /// <summary>
    /// Created By:         Josephine Monteza
    /// Date Created:       01/Jun/2016
    /// Description:        Hotel Contract details
    [Serializable]
    public class ContractHotelDetails
    { 
         public Int32 RowNumber {get; set;}
         public Int32 DetailId {get; set;}
         public Int32 DetailId2 {get; set;}
         public DateTime DateFrom {get; set;}
         public DateTime DateTo {get; set;}
         public Int32 Sun {get; set;}
         public Int32 Mon {get; set;}
         public Int32 Tue {get; set;}
         public Int32 Wed {get; set;}
         public Int32 Thu {get; set;}
         public Int32 Fri {get; set;}
         public Int32 Sat {get; set;}
         public Int32 Sun2 {get; set;}
         public Int32 Mon2 {get; set;}
         public Int32 Tue2 {get; set;}
         public Int32 Wed2 {get; set;}
         public Int32 Thu2 {get; set;}
         public Int32 Fri2 {get; set;}
         public Int32 Sat2 {get; set;}
         public decimal SingleRate {get; set;}
         public decimal DoubleRate {get; set;}
         public decimal Tax {get; set;}
         public bool TaxInclusive {get; set;}
         public string Currency { get; set; }
         public Int32 CurrencyId { get; set; }
    }
}


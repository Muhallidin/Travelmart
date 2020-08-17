using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class HotelTransactionMedical
    {
        public long? TransHotelID { get; set; }
        public long? SeafarerID { get; set; }
        public string FullName { get; set; }
        public long? TravelReqID { get; set; }
        public long? IdBigint { get; set; }
        public string RecordLocator { get; set; }
        public int? SeqNo { get; set; }
        public long? PortAgentVendorID { get; set; }
        public int? RoomTypeID { get; set; }
        public string ReserveUnderName { get; set; }
        public DateTime? TimeSpanStartDate { get; set; }
        public DateTime? TimeSpanStartTime { get; set; }
        public DateTime? TimeSpanEndDate { get; set; }
        public DateTime? TimeSpanEndTime { get; set; }
        public int? TimeSpanDuration { get; set; }
        public string ConfirmationNo { get; set; }
        public string HotelStatus { get; set; }
        public DateTime? DateCreatedDatetime { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public decimal? VoucherAmount { get; set; }
        public long? ContractID { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ContractFrom { get; set; }
        public string RemarksForAudit { get; set; }
        public bool? IsBilledToCrew { get; set; }
        public bool? Breakfast { get; set; }
        public bool? Lunch { get; set; }
        public bool? Dinner { get; set; }
        public bool? LunchOrDinner { get; set; }
        public bool? WithShuttle { get; set; } 
        public string HotelCity { get; set; }
        public float? RoomCount { get; set; }
        public string HotelName { get; set; }
        public decimal? ConfirmRateMoney { get; set; }
        public decimal? ContractedRateMoney { get; set; }
        public string EmailTo { get; set; }
        public string Comment { get; set; }
        public int? CurrencyID { get; set; }
        public string ConfirmBy { get; set; }
        public short? StatusID { get; set; }

        public bool? IsPortAgent { get; set; }
        public string ColorCode { get; set; }
        public string ForeColor { get; set; }
        public bool? IsMedical { get; set; } 
        public string CancellationTermsInt { get; set; } 
        public string HotelTimeZoneID { get; set; }
        public string  CutOffTime { get; set; }
        public string IsConfirmed { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string RoomType { get; set; }
        public string VendorName { get; set; }

    }

    public class VehicleTransactionMedical
    { 
        public long? TransVehicleID { get; set; }
	    public long?  SeafarerID { get; set; }
	    public long?  IdBigint { get; set; }
	    public long?  TravelReqID { get; set; }
	    public string RecordLocator { get; set; }
	    public long?  TranspoVendorID { get; set; }
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
	    public string From { get; set; }
	    public string To { get; set; }
	    public DateTime? DateCreated { get; set; }
	    public DateTime? DateModified { get; set; }
	    public string CreatedBy { get; set; }
	    public string Modifiedby { get; set; }
	    public bool? IsActive { get; set; } 
	    public string RemarksForAudit { get; set; } 
	    public int? HotelID { get; set; } 
	    public bool? IsVisible { get; set; }
	    public int? ContractId { get; set; } 
	    public bool? IsSeaport { get; set; }
	    public int? SeqNo { get; set; } 
	    public string Driver { get; set; } 
	    public string VehicleDispatchTime { get; set; }
	    public string RouteFrom { get; set; } 
	    public string RouteTo { get; set; } 
	    public bool? VehicleUnset { get; set; } 
	    public string VehicleUnsetBy { get; set; } 
	    public DateTime? VehicleUnsetDate { get; set; } 
	    public string ConfirmBy { get; set; } 
	    public string Comments { get; set; } 
	    public string VehicleVendor { get; set; } 
	    public double? ContractedRateMoney { get; set; }
	    public double? ConfirmRateMoney { get; set; } 
	    public int? CurrencyInt { get; set; } 
	    public short? StatusID { get; set; } 
	    public string ApprovedBy { get; set; } 
	    public DateTime? ApprovedDate { get; set; } 
	    public string EmailTo { get; set; }
	    public short? RequestSourceID { get; set; } 
	    public string TransportationDetails { get; set; } 
        public bool? IsPortAgent { get; set; }
    
        public string ColorCode { get; set; } 
        public string ForeColor { get; set; }

    }






}

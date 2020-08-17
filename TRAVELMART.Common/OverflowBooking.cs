using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Modified By: Charlene Remotigue
    /// Date Modified: 03/02/2012
    /// Description: Pending Booking / Automatic Booking class
    /// -------------------------------
    /// Modified By: Jefferson Bermundo
    /// Date Modified: 13/07/2012
    /// Description: Include Hotel Request Identifier
    /// </summary>
    /// 
    [Serializable]
    public class OverflowBooking
    {
        
        public int? CoupleId { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CostCenter { get; set; }
        public DateTime ? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        
        public int? TravelReqId { get; set; }
        public string SFStatus { get; set; }
        public string Name { get; set; }
        public int? SeafarerId { get; set; }
        public string VesselName { get; set; }
        public string RoomName { get; set; }
        public string RankName { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string HotelCity { get; set; }
        public int? HotelNites { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string RecordLocator { get; set; }
        public string Carrier { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string FlightNo { get; set; }
        public DateTime? OnOffDate { get; set; }
        public string Voucher { get; set; }
        public string ReasonCode { get; set; }
        public decimal? Stripe { get; set; }
        public int? VendorId { get; set; }
        public int? BranchId { get; set; }
        public int? RoomTypeId { get; set; }
        public int? PortId { get; set; }
        public int? VesselId { get; set; }
        public bool? EnabledBit { get; set; }
        public bool WithSailMaster { get; set; }
        public bool HotelRequest { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   29/Oct/2014
    /// Description:    List of overflow by month
    /// ----------------------------------
    /// </summary>
    [Serializable]
    public class OverflowByMonth
    {
        public string HotelName { get; set; }
        public string HotelCity { get; set; }
        public string PortCode { get; set; }
        public int January { get; set; }
        public int February { get; set; }
        public int March { get; set; }
        public int April { get; set; }
        public int May { get; set; }
        public int June { get; set; }
        public int July { get; set; }
        public int August { get; set; }
        public int September { get; set; }
        public int October { get; set; }
        public int November { get; set; }
        public int December { get; set; }
        public int Total { get; set; }
    }
}

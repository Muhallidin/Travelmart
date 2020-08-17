using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  27/03/2012
    /// Description:    Add Vessel Name
    /// ----------------------------------
    /// Modified By:    Jefferson Bermundo
    /// Date Modified:  16/07/2012
    /// Description:    Add Hotel Request Column
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  16/07/2012
    /// Description:    Add Booking Remarks Column
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  26/07/2012
    /// Description:    Add IDBigint and SeqNo, Add Serializable
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  13/Feb/2012
    /// Description:    Add HotelName
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  07/Aug/2014
    /// Description:    Add field Comments and RemovedBy
    /// </summary>
    /// </summary>
    /// 
    [Serializable]
    public class OverflowBooking2
    {
        public Int64? TravelReqId { get; set; }
        public int? E1TravelReqId { get; set; }
        public Int64? SeafarerId { get; set; }
        public string SeafarerName { get; set; }
        public int? PortId { get; set; }
        public string PortName { get; set; }
        public string SFStatus { get; set; }
        public DateTime? OnOffDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? ArrivalDepartureDatetime { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string RankName { get; set; }
        public Decimal? Stripes { get; set; }
        public string RecordLocator { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int? RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public string ReasonCode { get; set; }

        public string VesselName { get; set; }
        public bool HotelRequest { get; set; }
        public string BookingRemarks { get; set; }

        public int? IDBigint { get; set; }
        public int? SeqNo { get; set; }
       
        public string HotelCity { get; set; }
        public string IsByPort { get; set; }
        public string HotelName { get; set; }
        public long HotelOverflowID { get; set; }

        public string Comments { get; set; }
        public string RemovedBy { get; set; }
        public DateTime? Datecreated { get; set; }
        
        public DateTime? CheckOutDate { get; set; }
        public int? HotelNights { get; set; } 

    }
}

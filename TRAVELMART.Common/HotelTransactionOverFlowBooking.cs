using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.BLL
{
    public class HotelTransactionOverFlowBooking
    {
        public long? IDBigint { get; set; } //
	    public int? SeqNo { get; set; } //	varchar(max),
        public long? SeafarerId { get; set; } //varchar(max) ='1|2|3',
        public long? TravelReqId { get; set; } //varchar(max) ='1|2|3',
        public long? ReqId { get; set; } //varchar(max) ='0|0|0',
	    public string RecordLocator  { get; set; } //varchar(max) = 'a|b|c',
        public int? Source { get; set; } // int = 0,a
        public int? ContractId { get; set; } //int = 2,
        public int? VendorId { get; set; } //int =2,
        public int? BranchId { get; set; } //int = 2,
        public int? RoomType { get; set; } //int = 1,		
	    public DateTime? CheckInDate  { get; set; } //date = null,
	    public DateTime? CheckInTime  { get; set; } //time =null,
	    public int? Duration  { get; set; } //int=1,
	    public string ConfirmationNum  { get; set; } //varchar(50)=null,
	    public string HotelStatus  { get; set; } //varchar(50)=null,
	    public string UserId  { get; set; } //varchar(50)=null, 
	    public string SfStatus  { get; set; } //varchar(max)=null,
        public int? CityId { get; set; } //int = 1,
        public int? CountryId { get; set; } //int = 232,
	    public string Remarks  { get; set; } //varchar(max)=null,
	    public decimal? Stripes  { get; set; } //varchar(max)='0.0|0.0|0.0',	
	    public string HotelCity  { get; set; } //VARCHAR(MAX)='',
	    public bool? IsPort  { get; set; } //VARCHAR(MAX)='',
	    public bool? ShuttleBit  { get; set; } //bit=null,
	    public string Description  { get; set; } //varchar(max)=null,
	    public string Function  { get; set; } //varchar(max)=null,
	    public string FileName  { get; set; } //varchar(max)=null,
	    public string Timezone  { get; set; } //varchar(max)=null,
	    public DateTime? GMTDATE  { get; set; } //DateTIME=null,
        public DateTime? CreateDate { get; set; } //DateTIME=null,
        public float? SeafarerCount { get; set; } //
        public bool? IsEmergency { get; set; }
        public long? HotelOverflowID { get; set; } //

    }
}

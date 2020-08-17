using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created: 30/01/2012
    /// Created By: Muhallidin G Wali
    /// Description: ConfirmBooking Class 
    /// --------------------------------------
    /// Date Modified: 03/02/2012
    /// Modified by: Charlene Remotigue
    /// Descriprition: parameter Name change
    /// --------------------------------------
    /// Date Modified:  20/02/2012
    /// Modified by:    Josephine Gad
    /// Descriprition:  Add BookingType
    /// --------------------------------------
    /// Date Modified:  22/08/2012
    /// Modified By:    Jefferson Bermundo
    /// Description:    Add IsTaggedByUser
    /// --------------------------------------
    /// Date Modified:  11/Feb/2012
    /// Modified By:    Josephine Gad
    /// Description:    Reorder Columns based from Locked manifest exported file
    /// --------------------------------------
    /// Date Modified:  14/May/2013
    /// Modified By:    Josephine Gad
    /// Description:    Add column IsMeetGreet, IsPortAgent and IsHotelVendor 
    ///                 Add Birthday
    /// --------------------------------------
    /// </summary> 
    /// 
    [Serializable]
    public class ConfirmBooking
    {
        public string HotelCity { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? HotelNites { get; set; }
        public string ReasonCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? EmployeeId { get; set; }

        public string Gender { get; set; }
        public string SingleDouble { get; set; }
        public string Couple { get; set; }
        public string Title { get; set; }
        public string Ship { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string HotelRequest { get; set; }

        public string RecordLocator { get; set; }
        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }

        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }

        public TimeSpan? DeptTime { get; set; }
        public TimeSpan? ArvlTime { get; set; }

        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public double? Voucher { get; set; }

        public string PassportNo { get; set; }
        public string IssuedDate { get; set; }
        public string PassportExpiration { get; set; }

        public bool? WithSailMaster { get; set; }
        public bool? IsTaggedByUser { get; set; }
        
        public int? TravelReqId { get; set; }
        public string SFStatus { get; set; }
        public int? colIdBigInt { get; set; }
        public string colConfirmation { get; set; }
        public string RoomType { get; set; }
        public DateTime? TagDateTime { get; set; }

        public string IsMeetGreet { get; set; }
        public string IsPortAgent { get; set; }
        public string IsHotelVendor { get; set; }

        public string Birthday { get; set; }

        
        //public string RecordLocator { get; set; }
        //public int ? E1ID { get; set; }
        //public string RoomType { get; set; }
        //public string Name { get; set; }
        //public DateTime ? CheckInDate { get; set; }
        //public DateTime ? CheckOutDate { get; set; }
        //public string RankName { get; set; }
        //public string Gender { get; set; }
        //public string Nationality { get; set; }
        //public string CostCenter { get; set; }
        //public int ? HotelNites { get; set; }
        //public string HotelCity { get; set; }
        //public string Airline { get; set; }
        //public double ? MealAllowance { get; set; }
        //public DateTime ? ArrivalTime { get; set; }
        //public string FromCity { get; set; }
        //public string ToCity { get; set; }
        //public string HotelStatus { get; set; }
        //public int ? TravelReqId { get; set; }
        //public int? ReqId { get; set; }
        //public string SFStatus { get; set; }
        //public string BookingType { get; set; }
        //public int ? E1TravelReqIdInt { get; set; }
        //public bool ? WithSailMaster { get; set; }
        //public bool ? HotelRequest { get; set; }
        //public bool ? IsTaggedByUser { get; set; }
        //public int ? colIdBigInt { get; set; }

        //public string PassportNo { get; set; }
        //public string PassportExp { get; set; }
        //public string PassportIssued { get; set; }

    }

    public class ConfirmBookingExc
    {
        public string RecordLocator { get; set; }
        public string E1ID { get; set; }
        public string RoomType { get; set; }
        public string Name { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string RankName { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CostCenter { get; set; }
        public string HotelNites { get; set; }
        public string HotelCity { get; set; }
        public string Airline { get; set; }
        public string MealAllowance { get; set; }
        public string ArrivalTime { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string HotelStatus { get; set; }
        public string TravelReqId { get; set; }
        public string ReqId { get; set; }
        public string SFStatus { get; set; }
        public string BookingType { get; set; }
        public string E1TravelReqIdInt { get; set; }
        public string WithSailMaster { get; set; }
        public string HotelRequest { get; set; }
        public string IsTaggedByUser { get; set; }
        public string colIdBigInt { get; set; }
    }



}

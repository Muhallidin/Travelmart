using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 01/02/2012
    /// Description: Overflow Booking class
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  27/03/2012
    /// Description:    Add Vessel Name
    /// ----------------------------------
    /// Modified By:    Jefferson Bermundo
    /// Date Modified:  16/07/2012
    /// Description:    Add booking remarks
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  24/07/2012
    /// Description:    Make List Serializable
    ///                 Add column IdBigint and SeqNo
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  05/Mar/2013
    /// Description:    Add Comments, RemovedBy field
    /// ----------------------------------
    /// Date Modified:  14/May/2013
    /// Modified By:    Marco Abejar
    /// (description)  Add birthday field
    /// ----------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class ExceptionBooking
    {
        public Int64 ExceptionIdBigInt { get; set; }
        public int? IdBigint { get; set; }
        public int? SeqNo { get; set; }

        public int ? TravelReqId {get;set;}
        public int ? E1TravelReqId { get; set; }
        public int ? SeafarerId { get; set; }
        public string SeafarerName { get; set; }
        public int? PortId { get; set; }
        public string PortName { get; set; }
        public string SFStatus { get; set; }
        public DateTime ? OnOffDate { get; set; }
        public DateTime ? ArrivalDepartureDatetime { get; set; }
        public string ArrivalDeparturetime { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string RankName { get; set; }
        public Decimal ? Stripes { get; set; }
        public string RecordLocator { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public int ? RoomTypeId { get; set; }
        public string RoomType { get; set; }        
        public string ReasonCode { get; set; }
        public string ExceptionRemarks { get; set; }
        public bool ? Invalid { get; set; }

        public string VesselName { get; set; }
        public string BookingRemarks { get; set; }
        public DateTime ? DateCreated { get; set; }
        public string HotelCity { get; set; }
        public string IsByPort { get; set; }

        public string Comments { get; set; }
        public string RemovedBy { get; set; }
        public DateTime? Birthday { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   13/Jan/2014
    /// Description:    List of exception by month
    /// ----------------------------------
    /// </summary>
    [Serializable]
    public class ExceptionsByMonth
    {
        public int ExceptiopnID { get; set; }        
        public string ExceptionRemarks { get; set; }
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




    /// <summary>
    /// Author:         Muhallidin G Wali
    /// Date Created:   01/Oct/2014
    /// Description:    Get Exception Page Detail
    /// ----------------------------------
    /// </summary>
    [Serializable]
    public class ExceptionPageData
    {

        public List<PortList> PortList { get; set; }
        public List<RegionList> RegionList { get; set; }

        public List<ExceptionBooking> ExceptionBooking { get; set; }
        public List<Hotels> Hotels { get; set; }

    }



}

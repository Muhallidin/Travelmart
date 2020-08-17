using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 21/02/2012
    /// Description: get Manifest Types
    /// </summary>
    /// 
    [Serializable]
    public class ManifestClass
    {
        public int? ManifestType { get; set; }
        public int? ManifestHrs { get; set; }
        public string ManifestName { get; set; }
        public string ManifesDesc { get; set; }
    }

    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 23/02/2012
    /// Description: get Locked Manifest for paging
    /// ------------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  03/10/2012
    /// Description:    Add HotelBranch
    /// ------------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  11/Feb/2013
    /// Description:    Change Name to LastName & FirstName
    /// </summary>
    public class LockedManifest
    {
        public int? RowNo { get; set; }
        public int? IdBigInt { get; set; }
        public int? E1TravelReqId { get; set; }
        public int? TravelReqId { get; set; }
        public int? ManualReqId { get; set; }
        public string Couple { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CostCenter { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public int? EmployeeId { get; set; }
        public int? ShipId { get; set; }
        public string Ship { get; set; }
        public string HotelRequest { get; set; }
        public string SingleDouble { get; set; }
        public string Title { get; set; }
        public string HotelCity { get; set; }
        public int? HotelNights { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string AL { get; set; }
        public string RecordLocator { get; set; }
        public string PassportNo { get; set; }
        public string IssuedDate { get; set; }
        public string PassportExpiration { get; set; }
        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }
        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public DateTime? OnOffDate { get; set; }
        public string Voucher { get; set; }
        public DateTime? TravelDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int? BranchId { get; set; }
        public string HotelBranch { get; set; }
        //public string DeptTime { get; set; }
        //public string ArvlTime { get; set; }
        //public string HotelBranch { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modified:  19/07/2012
    /// Description:    Reorder and get the required fields only
    /// ----------------------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  11/Feb/2013
    /// Description:    Change Name to LastName & FirstName
    /// </summary>
    /// 
    [Serializable]
    public class LockedManifestEmail
    {
        public string HotelCity { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int? HotelNights { get; set; }
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
        public string DeptDate { get; set; }
        public string ArvlDate { get; set; }
        public string DeptTime { get; set; }
        public string ArvlTime { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public string Voucher { get; set; }
        public string PassportNo { get; set; }
        public string IssuedDate { get; set; }
        public string PassportExpiration { get; set; }
        public string HotelBranch { get; set; }

        //public string Couple { get; set; }
        //public string Gender { get; set; }
        //public string Nationality { get; set; }
        //public string CostCenter { get; set; }
        //public string CheckIn { get; set; }
        //public string CheckOut { get; set; }
        //public string Name { get; set; }
        //public int? EmployeeId { get; set; }
        //public string Ship { get; set; }
        //public string HotelRequest { get; set; }
        //public string SingleDouble { get; set; }
        //public string Title { get; set; }
        //public string HotelCity { get; set; }
        //public int? HotelNites { get; set; }
        //public string FromCity { get; set; }
        //public string ToCity { get; set; }
        //public string AL { get; set; }
        //public string RecordLocator { get; set; }
        //public string PassportNo { get; set; }
        //public string IssuedBy { get; set; }
        //public string PassportExpiration { get; set; }
        //public string DeptDate { get; set; }
        //public string ArvlDate { get; set; }
        //public string DeptCity { get; set; }
        //public string ArvlCity { get; set; }
        //public string DeptTime { get; set; }
        //public string ArvlTime { get; set; }
        //public string Carrier { get; set; }
        //public string FlightNo { get; set; }
        //public string OnOffDate { get; set; }
        //public string Voucher { get; set; }
        //public string TravelDate { get; set; }
        //public string Reason { get; set; }
        //public string HotelBranch { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modified:  18/07/2012
    /// Description:    Add QueryRemarksID  and QueryRemarks
    ///                 Reorder and get the required fields only
    /// ----------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  11/Jan/2013
    /// Description:    Change Name to LastName & FirstName
    /// </summary>
    /// 
    [Serializable]
    public class ManifestDifferenceEmail
    {
        public string Remarks { get; set; }
        public string HotelCity { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int? HotelNights { get; set; }
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
        public string DeptDate { get; set; }
        public string ArvlDate { get; set; }
        public string DeptTime { get; set; }
        public string ArvlTime { get; set; }

        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public string Voucher { get; set; }

        public string PassportNo { get; set; }
        public string IssuedDate { get; set; }
        public string PassportExpiration { get; set; }

        public string HotelBranch { get; set; }

        //public string Couple { get; set; }
        //public string Gender { get; set; }
        //public string Nationality { get; set; }
        //public string CostCenter { get; set; }
        //public string CheckIn { get; set; }
        //public string CheckOut { get; set; }
        //public string Name { get; set; }
        //public int? EmployeeId { get; set; }
        //public string Ship { get; set; }
        //public string HotelRequest { get; set; }
        //public string SingleDouble { get; set; }
        //public string Title { get; set; }
        //public string HotelCity { get; set; }
        //public int? HotelNites { get; set; }
        //public string FromCity { get; set; }
        //public string ToCity { get; set; }
        //public string AL { get; set; }
        //public string RecordLocator { get; set; }
        //public string PassportNo { get; set; }
        //public string IssuedBy { get; set; }
        //public string PassportExpiration { get; set; }
        //public string DeptDate { get; set; }
        //public string ArvlDate { get; set; }
        //public string DeptCity { get; set; }
        //public string ArvlCity { get; set; }
        //public string DeptTime { get; set; }
        //public string ArvlTime { get; set; }
        //public string Carrier { get; set; }
        //public string FlightNo { get; set; }
        //public string OnOffDate { get; set; }
        //public string Voucher { get; set; }
        //public string TravelDate { get; set; }
        //public string Reason { get; set; }
        //public string HotelBranch { get; set; }

        //public int? QueryRemarksID { get; set; }
        //public string QueryRemarks { get; set; }
    }

    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 202/2012
    /// Description: locked manifest difference list
    /// =================================================
    /// Modified By:    Josephine Gad
    /// Date Modified:  18/07/2012
    /// Description:    Add column QueryRemarksID and QueryRemarks
    /// =================================================
    /// Modified By:    Josephine Gad
    /// Date Modified:  11/Feb/2013
    /// Description:    Change Name to LastName & FirstName
    /// </summary>
    public class LockedManifestDifference
    {
        public int? ManifestId { get; set; }
        public int? ManifestType { get; set; }
        public int? IdBigInt { get; set; }
        public int? E1TravelReqId { get; set; }
        public int? TravelReqId { get; set; }
        public int? ManualReqId { get; set; }
        public string Couple { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CostCenter { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public int? EmployeeId { get; set; }
        public string Ship { get; set; }
        public string HotelRequest { get; set; }
        public string SingleDouble { get; set; }
        public string Title { get; set; }
        public string HotelCity { get; set; }
        public int? HotelNights { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string AL { get; set; }
        public string RecordLocator { get; set; }
        public string PassportNo { get; set; }
        public string IssuedDate { get; set; }
        public DateTime? PassportExpiration { get; set; }
        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }
        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public DateTime? OnOffDate { get; set; }
        public string Voucher { get; set; }
        public DateTime? TravelDate { get; set; }
        public string Reason { get; set; }
        public bool? isDeleted { get; set; }
        public string Status { get; set; }

        public int? QueryRemarksID { get; set; }
        public string QueryRemarks { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modified:  18/07/2012
    /// Description:    Add column QueryRemarksID and QueryRemarks
    /// ----------------------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  11/Feb/2013
    /// Description:    Change Name to LastName & FirstName
    /// </summary>
    /// 
    [Serializable]
    public class LockedManifestDifferenceWithRows
    {
        public int? RowNum { get; set; }
        public int? ManifestId { get; set; }
        public int? ManifestType { get; set; }
        public int? IdBigInt { get; set; }
        public int? E1TravelReqId { get; set; }
        public int? TravelReqId { get; set; }
        public int? ManualReqId { get; set; }
        public string Couple { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CostCenter { get; set; }
        public DateTime? CheckedIn { get; set; }
        public DateTime? CheckedOut { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public int? EmployeeId { get; set; }
        public string Ship { get; set; }
        public string HotelRequest { get; set; }
        public string RoomType { get; set; }
        public string Title { get; set; }
        public string HotelCity { get; set; }
        public int? HotelNights { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string Airline { get; set; }
        public string RecordLocator { get; set; }
        public string PassportNo { get; set; }
        public string IssuedDate { get; set; }
        public DateTime? PassportExpiration { get; set; }
        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }
        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public DateTime? OnOffDate { get; set; }
        public string Voucher { get; set; }
        public DateTime? TravelDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public bool? isDeleted { get; set; }

        public int? QueryRemarksID { get; set; }
        public string QueryRemarks { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modified:  28/03/2012
    /// Description:    Add [Serializable]
    /// </summary>
    [Serializable]
    public class ManifestCalendar
    {
        public DateTime colDate { get; set; }
        public int TotalCount { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   19/03/2012
    /// Description:    On/Off calendar count
    /// ------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  28/03/2012
    /// Description:    Add [Serializable]
    /// </summary>
    /// 
    [Serializable]
    public class ManifestOnOffCalendar
    {
        public string sDate { get; set; }
        public int ONCount { get; set; }
        public int OffCount { get; set; }
        public DateTime colDate { get; set; }
    }
    /// Author:         Josephine Gad
    /// Date Created:   07/11/2012
    /// Description:    List of room needed per day
    ///--------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  04/Feb/2013
    /// Description:    Add Contract Room Count, Override and Total Room Counts
    ///--------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  13/Feb/2013
    /// Description:    Remove SGL/DBL Room count for contract and override. Delete emergency room count
    [Serializable]
    public class CalendarRoomNeeded
    {
        public string sDate { get; set; }
        public int SingleCount { get; set; }
        public int DoubleCount { get; set; }
        public int TotalNeededRoom { get; set; }
        public int TotalRoom { get; set; }

        //public int ContractRoomSingle { get; set; }
        //public int ContractRoomDouble { get; set; }
        public int TotalContractRoom { get; set; }
        //public int OverrideRoomSingle { get; set; }
        //public int OverrideRoomDouble { get; set; }
        public int TotalOverrideRoom { get; set; }
        //public int TotalRoomsSingle { get; set; }
        //public int TotalRoomDouble { get; set; }

        //public int EmergencyRoomSingle { get; set; }
        //public int EmergencyRoomDouble { get; set; }
        //public int TotalEmergencyRoom { get; set; }

        public DateTime colDate { get; set; }
    }
    /// Author:         Josephine Gad
    /// Date Created:   26/Nov/2012
    /// Description:    List of Nationality
    //[Serializable]
    //public class NationalityList
    //{
    //    public int NationalityID { get; set; }
    //    public string NationalityName { get; set; }
    //}
    //[Serializable]
    //public class GenderList
    //{
    //    public int GenderID { get; set; }
    //    public string GenderName { get; set; }
    //}
    //[Serializable]
    //public class RankList
    //{
    //    public int RankID { get; set; }
    //    public string RankName { get; set; }
    //}
    [Serializable]
    public class VesselList
    {
        public int VesselID { get; set; }
        public string VesselName { get; set; }
    }
    //[Serializable]
    //public class HotelList
    //{
    //    public int HotelID { get; set; }
    //    public string HotelName { get; set; }
    //}
    public class ForeCastFilters
    {
        public List<NationalityList> NationalityList { get; set; }
        public List<GenderList> GenderList { get; set; }
        public List<RankList> RankList { get; set; }
        public List<VesselList> VesselList { get; set; }
        public List<HotelDTO> HotelDTO { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   20/Dec/2012
    /// Description:    Locked Manifest Summary
    /// </summary>
    /// 
    [Serializable]
    public class LockedManifestSummary
    {
        public int ManifestIDBigint { get; set; }
        public Int16 ManifestTypeIDTinyint { get; set; }
        public string ManifestNameVarchar { get; set; }
        public Int16 ManifestHrsTinyint { get; set; }
        public string ManifestDescVarchar { get; set; }
        public string CreatedByVarchar { get; set; }
        public DateTime? DateCreatedDateTime { get; set; }
        public string BranchName { get; set; }
        public int BranchID { get; set; }

        public DateTime? ManifestDate { get; set; }
    }   
}
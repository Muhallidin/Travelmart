using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
     /// <summary>
    /// -------------------------------------------------------------------
    /// Author:         Josephine Gad
    /// Date Created:   05/Mar/2014
    /// Descrption:     Set class for Service Provider Services
    /// -------------------------------------------------------------------
    /// </summary>
    [Serializable]
    public class PortAgentServicesCount
    {
        public int iRow { get; set; }
        public int TotalCount { get; set; }
        public int PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        
        public int PendingVendor { get; set; }
        public int PendingRCCL { get; set; }
        public int PendingRCCLCost { get; set; }
        public int Approved { get; set; }
        public int Cancelled { get; set; }

        public string PendingColor { get; set; }
        public string PendingRCCLColor { get; set; }
        public string PendingRCCLCostColor { get; set; }
        public string CancelledColor { get; set; }
        public string ApprovedColor { get; set; }
               
    }

    public class PortAgentConfirmCancelledManifest
    {
        public int? ConfirmedCount { get; set; }
        public List<PortAgentHotelManifestList> PortAgentConfirmHotelManifestList { get; set; } 

        public int? CancelledCount { get; set; }
        public List<PortAgentHotelManifestList> PortAgentCancelledHotelManifestList { get; set; }

        public List<ManifestStatus> listStatus { get; set; }


        public int? VehicleRequestCount { get; set; }
        public List<PortAgentVehicleManifestList> PortAgentResquestVehicleManifestList { get; set; }

        public int? VehicleConfirmedCount { get; set; }
        public List<PortAgentVehicleManifestList> PortAgentConfirmVehicleManifestList { get; set; }

        public int? VehicleCancelledCount { get; set; }
        public List<PortAgentVehicleManifestList> PortAgentCancelledVehicleManifestList { get; set; }


    }




    /// <summary>
    /// -------------------------------------------------------------------
    /// Author:         Josephine Gad
    /// Date Created:   05/Mar/2014
    /// Descrption:     Set class for Service Provider Hotel Manifest
    /// -------------------------------------------------------------------
    /// Modifed by:     Josephine Gad
    /// Date Modified:  21/May/2014
    /// Descrption:     Add Nationality
    /// -------------------------------------------------------------------
    /// Modifed by:     Josephine Monteza
    /// Date Modified:  05/Jan/2015
    /// Descrption:     Add IsTagged and IsVendor
    /// -------------------------------------------------------------------
    /// Modified By:    Michael Evangelista
    /// Date Modified:  05/Mar/2015
    /// Description:    Add FlightStats to fields
    /// 
    /// </summary>
    [Serializable]
    public class PortAgentHotelManifestList
    {
        public Int64 PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        
        public Int64 TransHotelID { get; set; }
        public Int64 IdBigint { get; set; }
        public Int64 TravelReqID { get; set; }

        public string RecordLocator { get; set; }

        public Int64 SeafarerIdInt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        
        public string HotelName { get; set; }
        public string ConfirmationNo { get; set; }

        public string VesselName { get; set; }
        public string RankName { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        
        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }

        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }

        public TimeSpan? DeptTime { get; set; }
        public TimeSpan? ArvlTime { get; set; }


        public string ActualDepartureDate { get; set; }
        public string ActualArrivalDate { get; set; }
        public string ActualArrivalGate { get; set; }
        public string ActualArrivalStatus { get; set; }
        public string ActualArrivalBaggage { get; set; }


        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public string RoomType { get; set; }
        public string CrewStatus { get; set; }
        public DateTime DateOnOff { get; set; }


        public string PassportNo { get; set; }
        public string PassportExp { get; set;}
        public string PassportIssued { get; set; }

        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }

        public Int16 HotelNites { get; set; }
        public double? Voucher { get; set; }

        public double? RateConfirmed { get; set; }
        public double? RateContracted { get; set; }

        public string RequestStatus { get; set; }
        public bool IsConfirmVisible { get; set; }

        public bool IsCancelVisible { get; set; }
        public Int32 CurrencyID { get; set; }

        public string Comment { get; set; }
        public string CommentBy { get; set; }
        public string ConfirmedBy { get; set; }

        public bool IsTagged { get; set; }
        public bool IsVendor { get; set; }
        public int? RemarkID { get; set; }
        public string Remark { get; set; }

    }
    /// <summary>
    /// -------------------------------------------------------------------
    /// Author:         Josephine Gad
    /// Date Created:   05/Mar/2014
    /// Descrption:     Set class for Service Provider Vehicle Manifest
    /// -------------------------------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  21/May/2014
    /// Descrption:     Add Nationality
    /// -------------------------------------------------------------------
    /// Modifed by:     Josephine Monteza
    /// Date Modified:  06/Jan/2015
    /// Descrption:     Add IsTagged and IsVendor
    /// -------------------------------------------------------------------
    /// </summary>
    [Serializable]
    public class PortAgentVehicleManifestList
    {
        public Int32 ContractID { get; set; }
        public Int64 PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        public Int64 TransVehicleID { get; set; }
        public Int64 IdBigint { get; set; }
        public Int64 TravelReqID { get; set; }
        public string RecordLocator { get; set; }
        public Int64 SeafarerIdInt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string VehicleVendorName { get; set; }        
        public string ConfirmationNo { get; set; }
        public string VesselName { get; set; }
        public string RankName { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string DeptCity { get; set; }
        public string ArvlCity { get; set; }
        public string ActualDepartureDate { get; set; }
        public string ActualArrivalDate { get; set; }
        public string ActualArrivalGate { get; set; }
        public string ActualArrivalStatus { get; set; }
        public string ActualArrivalBaggage { get; set; }
        public DateTime? DeptDate { get; set; }
        public DateTime? ArvlDate { get; set; }
        public TimeSpan? DeptTime { get; set; }
        public TimeSpan? ArvlTime { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }
        public string VehicleTypeName { get; set; }
        public Int16 VehicleTypeID { get; set; }
        public string CrewStatus { get; set; }
        public DateTime DateOnOff { get; set; }
        public string PassportNo { get; set; }
        public string PassportExp { get; set; }
        public string PassportIssued { get; set; }
        public DateTime? PickupDate { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public int RouteFromID { get; set; }
        public int RouteToID { get; set; }
        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }
        public string RouteFromDisplay { get; set; }
        public string RouteToDisplay { get; set; }
        public string CityFrom { get; set; }
        public string CityTo { get; set; }
        public double? RateContracted { get; set; }
        public double? RateConfirmed { get; set; }
        public string RequestStatus { get; set; }
        public bool IsConfirmVisible  { get; set; }
        public bool IsCancelVisible { get; set; }
        public Int32 CurrencyID { get; set; }
        public string TransportationDetails { get; set; }
        public string Comment { get; set; }
        public string CommentBy { get; set; }
        public string ConfirmedBy { get; set; }

        public int SeqNo { get; set; }

        public bool IsTagged { get; set; }
        public bool IsVendor { get; set; }


        public int? RemarkID { get; set; }
        public string Remark { get; set; }



    }
    /// <summary>
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  08/Mar/2014
    /// (desciption)    Hotel to Add/Cancel
    /// </summary>
    [Serializable]
    public class HotelManifestToConfirm
    {
        public string AddCancel { get; set; }
        public Int64 IDBigint { get; set; }
        public Int64 TReqID { get; set; }
        public Int64 TransID { get; set; }
    }
    /// <summary>
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  12/Mar/2014
    /// (desciption)    Transport to Add/Cancel
    /// </summary>
    [Serializable]
    public class VehicleManifestToConfirm
    {
        public string AddCancel { get; set; }
        public Int64 IDBigint { get; set; }
        public Int64 TReqID { get; set; }
        public Int64 TransID { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  19/Mar/2014
    /// (desciption)    Manifest Status
    /// </summary>
    [Serializable]
    public class ManifestStatus
    {
        public int iStatusID { get; set; }
        public string sStatus { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  25/Mar/2014
    /// (desciption)    Remarks Request Source
    /// </summary>
    [Serializable]
    public class RequestSource
    {
        public int RequestSourceID { get; set; }
        public string RequestSourceName { get; set; }
    }
    [Serializable]
    public class TransactionRemarks
    {

        public long? TravelRequestID { get; set; }
        public long? RemarksID { get; set; }
        public string Remarks { get; set; }
        public string RemarksBy { get; set; }
        public string RemarksDate { get; set; }       

        public short? ReqResourceID { get; set; }
        public string Resource { get; set; }
    }

  
    [Serializable]
    public class RouteFromTo
    {
        public string RouteID { get; set; }
        public string RouteName { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  11/Apr/2014
    /// (desciption)    Service Provider Vehicle Contract Amt
    /// </summary>
    [Serializable]
    public class PortAgentVehicleContractAmt
    {
        public int ContractID { get; set; }
        
        public Int16 RouteFromInt { get; set; }
        public Int16 RouteToInt { get; set; }
        
        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }

        public string RouteFromCity { get; set; }
        public string RouteToCity { get; set; }

        public float RateAmount { get; set; }
    }
    /// <summary>
    /// Modified By:    Josephine Monteza
    /// Modified Date:  05/Nov/2014
    /// (desciption)    Ok To Board in Brazil list
    /// </summary>
    [Serializable]
    public class OkToBrazilList
    {
        public Int64 SeafarerID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Nationality { get; set; }
        
        public Int64 IDBigint { get; set; }
        public Int64 TRID { get; set; }

        public string RecLoc { get; set; }
        public string Status { get; set; }

        public DateTime OnOffDate { get; set; }
        public string ReasonCode { get; set; }

        public string PortCode { get; set; }
        public string PortName { get; set; }

        public string VesselCode { get; set; }
        public string VesselName { get; set; }

        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }

        public DateTime? DepartureDatetime { get; set; }
        public DateTime? ArrivalDatetime { get; set; }

        public string Airline { get; set; }
        public string FlightNo { get; set; }

        public string PassportNo { get; set; }
        public DateTime? PassportIssuedDate { get; set; }
        public DateTime? PassportExpiredDate { get; set; }


        public string SeamansBook { get; set; }
        public DateTime? SeaBookIssuedDate { get; set; }
        public DateTime? SeaBookExpiredDate { get; set; }

    }
    ///<summary>
    ///Created By:  Michael Evangelista
    ///Date Created:    30-09-2015
    ///Description: PortAgent List for new Non turn ports
    ///</summary>
    ///
    [Serializable]
    public class PortAgentVendorManifestListName
    {
        public Int64 PortAgentID { get; set; }
        public string PortAgentName { get; set; }

    }

    ///<summary>
    ///Created By:  Michael Evangelista
    ///Date Created:    30-09-2015
    ///Description: Hotel List for new Non turn ports
    ///</summary>
    [Serializable]
    public class PortAgentHotelManifestListName
    {
        public Int64 HotelID { get; set; }
        public string HotelVendorName { get; set; }
    }

    ///<summary>
    ///Created By:   Muhallidin G Wali
    ///Date Created: 29-09-2016
    ///Description:  Non Turn port count
    ///</summary>
    public class PortAgentHotelVehicle
    {
        public long PortAgentID { get; set; }
        public string VendorName { get; set; }
        public DateTime StartDate { get; set; }
       
        public int PortID { get; set; }
        public string PortCode { get; set; }
        public string PortName { get; set; }

        public int HotelRequestCount { get; set; }
        public int HotelConfirmedCount { get; set; }
        public int HotelPendingCount { get; set; }

        public int VehicleRequestCount { get; set; }
        public int VehicleConfirmedCount { get; set; }
        public int VehiclePendingCount { get; set; }

        public string Request { get; set; }				
        public string Confirmed { get; set; }
        public string Pending { get; set; }
        public string UserID { get; set; }
        public string Days { get; set; }
        public int? ContractID { get; set; }


    }

    ///<summary>
    ///Created By:   Muhallidin G Wali
    ///Date Created: 04-10-2016
    ///Description:  Non Turn port count
    ///</summary>
    public class DaysList
    {
        public int Days { get; set; }
    }

     
    ///<summary>
    ///Created By:   Muhallidin G Wali
    ///Date Created: 29-09-2016
    ///Description:  Non Turn port count
    ///</summary>
    public class SeaportPortagentDaysList
    {
        public List<DaysList> DaysList { get; set; }
        public List<PortList> Seaport { get; set; }
        public List<UserBranch> Portagent { get; set; }
    }
     
}

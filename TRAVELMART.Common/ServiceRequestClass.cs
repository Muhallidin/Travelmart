using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Modified: 17/Oct/2013
    /// Modified By:   Josephine Gad
    /// (description)  Service Request List
    /// </summary>
    [Serializable]    
    public class ServiceRequestList
    {
        public int HotelRequestIDBigint { get; set; }
        public int VehicleRequestIDBigint { get; set; }
        public int MeetGreetRequestIDBigint { get; set; }
        public int PortAgentRequestIDBigint { get; set; }        

        public bool IsAir { get; set; }
        public int AirSeqNo { get; set; }
        public Int64 IDBigInt { get; set; }
        public Int64 TravelReqIDInt { get; set; }
        //public Int64 RequestIDInt { get; set; }
        public Int64 SeafarerIDInt { get; set; }
        public string SFStatus { get; set; }
        public DateTime? SignOnOffDate { get; set; }
        public Int64 VesselID { get; set; }
        public string VesselName { get; set; }

        public bool IsWithHotelRequest { get; set; }
        public bool IsWithVehicleRequest { get; set; }
        public bool IsWithMeetGreetRequest { get; set; }
        public bool IsWithPortAgentRequest { get; set; }


        public bool IsHotelRequestActive { get; set; }
        public bool IsVehicleRequestActive { get; set; }
        public bool IsMeetGreetRequestActive { get; set; }
        public bool IsPortAgentRequestActive { get; set; }


        public bool IsHotelRequestBook { get; set; }
        public bool IsVehicleRequestBook { get; set; }
        public bool IsMeetGreetRequestBook { get; set; }
        public bool IsPortAgentRequestBook { get; set; }


        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RankName { get; set; }

        public bool IsEmailVisible { get; set; }

        public string HotelRequestCreatedBy { get; set; }
        public string VehicleRequestCreatedBy { get; set; }
        public string PortAgentRequestCreatedBy { get; set; }

        public DateTime? HotelRequestCreatedDate { get; set; }
        public DateTime? VehicleRequestCreatedDate { get; set; }
        public DateTime? PortAgentRequestCreatedDate { get; set; }
    }
    ///// <summary>
    ///// Date Modified: 25/Oct/2013
    ///// Modified By:   Josephine Gad
    ///// (description)  List for Cancel and Active Service Request
    ///// </summary>
    //[Serializable]
    //public class CancelServiceRequest
    //{
    //    public Int64 iIdentity { get; set; }
    //    public string sServiceType { get; set; }
    //    public bool bIsActive { get; set; }
    //}
    
     //<summary>
     //Date Created:    22/Oct/2013
     //Created By:      Josephine Gad
     //(description)    Get Service Request Email info
     //</summary>
    [Serializable]
    public class ServiceRequestEmailList
    {
        public Int32 HotelID { get; set; }
        public string HotelEmailTo { get; set; }
        public string HotelName { get; set; }


        public Int32 VehicleID { get; set; }
        public string VehicleEmailTo { get; set; }
        public string VehicleName { get; set; }

        public Int32 MeetGreetID { get; set; }
        public string MeetGreetEmailTo { get; set; }
        public string MeetGreetName { get; set; }

        public Int32 PortAgentID { get; set; }
        public string PortAgentEmailTo { get; set; }
        public string PortAgentName { get; set; }

        public Int32 VesselID { get; set; }
        public string VesselEmailTo { get; set; }
    }
    /// <summary>
    //Date Created:    05/Nov/2013
    //Created By:      Josephine Gad
    //(description)    Crew Assist users
    /// </summary>
    [Serializable]    
    public class CrewAssistUsers
    {
        public string UserID { get; set; }
        public string UserName { get; set; }

    }
}

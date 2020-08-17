using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
   public class CrewAdminClass
   {
       public static string BranchName { get; set; }
       public static int? CrewAdminListCount { get; set; }
       public int GetCrewAdminCount(Int16 LoadType, string FromDate, string ToDate,
           string UserID, string Role, string OrderBy, string VesselID, string RegionID, string CountryID
            , string CityID, string PortID, string HotelID)
        {
            try
            {
                return (int)CrewAdminListCount;
            }
            catch
            {
                throw;
            }
            finally
            {
                CrewAdminListCount = null;
            }
        }
        public static List<CrewAdminList> CrewAdminList { get; set; }
        public List<CrewAdminList> GetCrewAdmin(Int16 LoadType, string FromDate, string ToDate,
           string UserID, string Role, string OrderBy, string VesselID, string RegionID, string CountryID
            , string CityID, string PortID, string HotelID, int StartRow, int MaxRow)
        {
            try
            {
                return CrewAdminList;
            }
           catch
            {
                throw;
            }
            finally
            {
                CrewAdminList = null;
            }
        }
   }

   /// <summary>
   /// Date Created:   16/09/2012
   /// Created By:     Muhallidin
   /// (description)   Class to call to assign values
   /// --------------------------------------------------
   /// Date Modified:  27/08/2012
   /// Created By:     Josephine Gad
   /// (description)   Add Nationality, Gender, Rank, OnCount,OffCount
   /// </summary>
   public class CrewAdminGenericClass
   {
       public List<CrewAdminList> CrewAdminList { get; set; }
       public int? CrewAdminListCount { get; set; }

       public List<NationalityList> NationalityList { get; set; }
       public List<GenderList> GenderList { get; set; }
       public List<RankList> RankList { get; set; }

       public int? OnCount { get; set; }
       public int? OffCount { get; set; }
   }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  22/08/2012
    /// (desciption)    Delete transaction status
    ///                 Add Arrival, Departure
    /// Modified By:    Michael Evangelista
    /// Modified Date:  23/02/2015
    /// (description)   Added Actual Arrival and Arrival Gate from FlightStats
    /// </summary>
    /// 
    [Serializable]
   public class CrewAdminList
   {
        public bool IsManual { get; set; }
        public long IDBigInt  { get; set; }
        public int E1TravelRequest  { get; set; }
        public int RequestID  { get; set; }
        public long TravelRequestID  { get; set; }
        public string RecLoc  { get; set; }
        public long SeafarerID  { get; set; }
        public string Name  { get; set; }
        public DateTime? DateOnOff	 { get; set; }
        public DateTime? DateArrivalDeparture	 { get; set; }
        public string Status  { get; set; }
        public string Brand	 { get; set; }
        public string Vessel { get; set; }
        //public string PortCode	{ get; set; }
        public string Port	{ get; set; }
        //public string RankCode  { get; set; }
        public string Rank  { get; set; }
     
        public string ReasonCode  { get; set; }
        public bool IsWithSail { get; set; }

        public string Arrival { get; set; }
        public string Departure { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public string FlightNo { get; set; }
        public string Airline { get; set; }

        public string Hotel { get; set; }

        public string PassportNo { get; set; }
        public string PassportIssued { get; set; }
        public string PassportExp { get; set; }

        public bool IsMeetGreet { get; set; }
        public bool IsPortAgent { get; set; }
        public bool IsHotelVendor { get; set; }

        public string Remarks { get; set; }

        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public string SingleDouble { get; set; }
        public string Gender { get; set; }
        public string CostCenter { get; set; }
        public string Nationality { get; set; }
        public string MealAllowance { get; set; }
        public int? Duration { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int SequenceNo { get; set; }
        public bool IsVisible { get; set; }
        public DateTime? Birthday { get; set; }

        public int? TransVehicleID { get; set; }
        public bool IsCheckBoxForVehicleVisible { get; set; }
        public bool IsVisibleToVehicleVendor { get; set; }
        public bool IsNeedVehicleBooking { get; set; }
        public bool IsToPrintItinerary { get; set; }

        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }
        public DateTime? PickupDatetime { get; set; }

        public string VehicleName { get; set; }
        public string Confirm { get; set; }

        public string ConfirmBy { get; set; }
        public string ConfirmDate { get; set; }
        // Added Actual Arrival Date/Time and Arrival Gate
        public string ActualArrivalDate { get; set; }
        public string ActualDepartureDate { get; set; }
        public string ActualArrivalGate { get; set; }
        public string ActualArrivalStatus { get; set; }
        public string ActualArrivalBaggage { get; set; }
        public string DateHired { get; set; }


   }
    /// <summary>
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  22/Dec/2013
    /// (desciption)    Crew Admin Transport
    /// </summary>
    [Serializable]
    public class CrewAdminTransport
    {
        public Int64 IDBigint { get; set; }
        public Int64 TransID { get; set; }
        public Int64 E1ID { get; set; }        

        public Int32 SeqNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }
        public string FlightNo { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }

        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string RecLoc { get; set; }

        public DateTime? PickupDateTime { get; set; }
        
    }
    /// <summary>
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  22/Dec/2013
    /// (desciption)    Crew Admin Transport to Add/Cancel
    /// </summary>
    [Serializable]
    public class CrewAdminTransportAddCancel
    {
        public string AddCancel { get; set; }        
        public Int64 IDBigint { get; set; }
        public Int64 TReqID { get; set; }
        public Int64 TransID { get; set; }        
    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  23/Dec/2013
    /// (desciption)    Vehicle Vendor Email
    /// </summary>
    [Serializable]
    public class VehicleVendorEmail
    {
        public Int64 VehicleIDInt { get; set; }
        public string VehicleName { get; set; }
        public string VehicleEmailTo { get; set; }
    }
}

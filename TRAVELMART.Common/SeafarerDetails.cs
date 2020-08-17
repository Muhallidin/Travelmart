using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modified:  27/07/2012
    /// Description:    Add Serializable
    /// --------------------------------------
    /// Modified By:    Josephine Monteza
    /// Date Modified:  18/Dec/2014
    /// Description:    Add fields EmployeeID, LastName, FirstName, HotelName
    ///                 ContractID, CurrencyID, CurrencyName
    /// </summary>
    [Serializable]
    public class SeafarerDetails
    {
        public int VendorId { get; set; }
        
        public int BranchId { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckInTime { get; set; }
        public int Duration { get; set; }
        public int RoomSource { get; set; }
        public string Remarks { get; set; }
        public string HotelStatus { get; set; }
        public bool WithShuttle { get; set; }
        public string ConfirmationNum { get; set; }
        public bool IsAccredited { get; set; }
        public float Stripes { get; set; }

        public Int64 EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string HotelName { get; set; }
        public Int64 ContractID { get; set; }

        public Int32 CurrencyID { get; set; }
        public string CurrencyName { get; set; }

        public Int32 CityID { get; set; }
        public Int32 CountryID { get; set; }

        public Int64 TravelRequetID { get; set; }
        public Int64 IDBigint { get; set; }

    }

    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 21/02/2012
    /// Description: load seafarer nationality
    /// </summary>
    /// 
    [Serializable]
    public class SFNationality
    {
        public int? NationalityId { get; set; }
        public string Nationality { get; set; }
    }
    [Serializable]
    public class SFRank
    {
        public int? RankId { get; set; }
        public string RankName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class HotelSummary
    {
        public int VendorId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public bool isAccredited { get; set; }
        public decimal TotalSingleBookings { get; set; }
        public decimal TotalDoubleBookings { get; set; }
        public decimal TotalSingleAvailableRooms { get; set; }
        public decimal TotalDoubleAvailableRooms { get; set; }
        public DateTime? colDate { get; set; }
        public bool withEvent { get; set; }
        public bool withContract { get; set; }
        public int ContractId { get; set; }
        public decimal ReservedRoom { get; set; }
    }
    /// ----------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  24/07/2012
    /// Description:    Make List Serializable
    /// ----------------------------------
    [Serializable]
    public class Hotels
    { 
        public int VendorId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public bool isAccredited { get; set; }
        public bool withEvent { get; set; }
        public bool withContract { get; set; }
        public int ContractId { get; set; }
        public DateTime colDate { get; set; }
    }
}

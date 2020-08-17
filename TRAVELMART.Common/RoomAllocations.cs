using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  27/07/2012
    /// Description:    Add Serializable
    /// </summary>
    [Serializable]
    public class RoomAllocations
    {
        public DateTime? coldate { get; set; }
        public int? colNumOfPeople { get; set; }
        public decimal? remaining { get; set; }
        public decimal? remainContrat { get; set; }
        public bool? valid { get; set; }
        public int? sourceAllocation { get; set; }
        public int? contractId { get; set; }

        public int? BranchId { get; set; }
        public int? RoomTypeId { get; set; }
        public int? ReservedOverride { get; set; }
        public int? EmergencyVacant { get; set; }
        public decimal? RoomCount { get; set; }

    }
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Modified Date:  27/07/2012
    /// Description:    Add parameter , BranchId, RoomTypeId, ReservedOverride
    /// </summary>

    public class SelectedRoomAllocations
    {
        public DateTime? coldate { get; set; }
        public int? colNumOfPeople { get; set; }
        public decimal? remaining { get; set; }
        public decimal? remainContrat { get; set; }
        public bool? valid { get; set; }
        public int? sourceAllocation { get; set; }
        public int? contractId { get; set; }

        public int? BranchId { get; set; }
        public int? RoomTypeId { get; set; }
        public int? ReservedOverride { get; set; }
        public int? EmergencyVacant { get; set; }
        public decimal? RoomCount { get; set; }


    }
    
    [Serializable]
    public class RoomBlocks
    { 
        public DateTime? colDate { get; set; }
        public int BranchId { get; set; }
        public int RoomTypeId { get; set; }
        public string Room { get; set; }
        public decimal? TotalBooking { get; set; }
        public decimal? ContractBlocks { get; set; }
        public decimal? OverrideBlocks { get; set; }
        public decimal? ReservedOverride { get; set; }
        public decimal? EmergencyBlocks { get; set; }
        public decimal? ReservedEmergency { get; set; }
        public bool validContract { get; set; }
        public bool validOverride { get; set; }
        public bool validEmergency { get; set; }
        public int ContractId { get; set; }
        public decimal? RemainingRooms { get; set; }

    }


    public class RemainRoomBlocks
    {
        public decimal? RemainContract { get; set; }
        public decimal? RemainVacantRoom { get; set; }
        public DateTime? Date { get; set; }
        public int? RoomType { get; set; }
        public int? BranchID { get; set; }
        public int? ContractRoomBlock { get; set; }
        public int? OverrideRoomBlock { get; set; }
        public decimal? UsedContractRoomBlock { get; set; }
        public decimal? UsedOverrideRoomBlock { get; set; }
        public int? ContractID { get; set; }
        public decimal? RemainContractRoom { get; set; }
        public decimal? EmergencyRoomBlock { get; set; } 
    }

    public class RemainRoomBlocksWithHotelID
    {
        public decimal? RemainVacantRoom { get; set; }
        public DateTime? Date { get; set; }
        public int? RoomType { get; set; }
        public int? BranchID { get; set; }
        public int? ContractID { get; set; }
        public long? TransHotelID { get; set; }
    }
}

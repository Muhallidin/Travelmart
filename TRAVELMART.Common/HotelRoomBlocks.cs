using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TRAVELMART.Common
{
    /// <summary>
    /// Modified By: Charlene Remotigue
    /// Date Modified: 03/02/2012
    /// Description: HOtel Blocks Count per Status Class
    /// --------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  17/02/2012
    /// Description:    Add Emergency Room Blocks and Bookings
    /// --------------------------------------------
    /// Modified By:    Josephine Gad
    /// Date Modified:  19/10/2012
    /// Description:    Add [Serializable]
    /// </summary>
    /// 
    [Serializable]
    public class HotelRoomBlocks
    {
        public DateTime ? Date { get; set; }
        public string Day { get; set; }
        public int ? RoomTypeID { get; set; }
        public string RoomName { get; set; }
        public decimal ? ReservedRoom { get; set; }
        public int Expected { get; set; }
        public int Arriving { get; set; }
        public int CheckedIn { get; set; }
        public int CheckedOut { get; set; }
        public int Cancelled { get; set; }
        public int NoShow { get; set; }
        public Decimal ? TotalRoomBookings { get; set; }
        public int TotalRoomBlocks { get; set; }
        public Decimal? TotalEmergencyBookings { get; set; }
        public int TotalEmergencyRoomBlocks { get; set; }
    }
}

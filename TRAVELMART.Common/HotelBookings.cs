using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class HotelBookings
    {
        //public static List<RegionList> RegionList { get; set; }
        //public static List<CountryList> CountryList { get; set; }
        //public static List<CityList> CityList { get; set; }
        //public static List<BranchList> BranchList { get; set; }
        //public static List<RoomAllocations> RoomAllocations { get; set; }
        public static List<HotelEmail> HotelEmail { get; set; }
        //public static List<SeafarerDetails> SeafarerDetailsList { get; set; }
        public static List<SelectedRoomAllocations> SelectedRoomAllocations { get; set; }
        //public static List<RoomBlocks> RoomBlocks { get; set; }
    }
    [Serializable]
    public class HotelEmail
    {
        public string RoleName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class HotelBookingsGenericClass
    {
        public List<RegionList> RegionList { get; set; }
        public List<CountryList> CountryList { get; set; }
        public List<CityList> CityList { get; set; }
        public List<BranchList> BranchList { get; set; }
        public List<RoomAllocations> RoomAllocations { get; set; }
        public List<HotelEmail> HotelEmail { get; set; }
        public List<SeafarerDetails> SeafarerDetailsList { get; set; }
    }
}

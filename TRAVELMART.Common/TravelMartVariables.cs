using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Modifed By:         Josephine Gad
    /// Date Modifed:       Jun 04, 2014
    /// Description:        change Service Provider to Service Provider
    /// </summary>
    public class TravelMartVariable
    {
        //private static string _strCreateMessge = string.Empty;
        //private static string _strPrevPage = String.Empty;
        //private static string _strCurrentPage = String.Empty;
        //private static string _strSFID = String.Empty;
        //private static string _strSFStatus = string.Empty;
        //private static string _strSFFlightDateRange = string.Empty;
        //private static string _strSFSeqNo = string.Empty;
        //private static string _strPendingFilter = string.Empty;
        //private static string _strAirStatusFilter = string.Empty;
        //private static string _strTravelLocatorID = string.Empty;  
        //private static string _strRecordLocator = string.Empty;

        //private static string _strOnOffDate = string.Empty;        

        //private static string _UserRoleString = string.Empty;
        //private static string _UserRoleKeyString = string.Empty;
        //private static string _UserVendorString = "";
        //private static string _UserBranchIDString = "";
        //private static string _UserCountryString = "";
        //private static string _UserCityString = "";

        //private static string _SelectedRoleKeyString = string.Empty;
        //private static string _SelectedRoleNameString = string.Empty;

        //private static string _TravelRequestIDString = string.Empty;
        //private static string _ManualRequestIDString = string.Empty;
        
        //private static string _RegionString = string.Empty;
        //private static string _CountryString = string.Empty;
        //private static string _CityString = string.Empty;

        //private static string _AirportString = string.Empty;

        //private static string _DateFromString = "";
        //private static string _DateToString = "";

        //private static string _PortString = "";
        //private static string _HotelString = "";
        //private static string _VehicleString = "";

        //private static string _HotelNameToSearch = "";

        //private static string _CountryNameString = "";
        //private static string _CityNameString = "";

        //private static string _ViewRegion = "";
        //private static string _ViewCountry = "";
        //private static string _ViewCity = "";
        //private static string _ViewHotel = "";
        //private static string _ViewPort = "";
        //private static string _ViewLegend = "";
        //private static string _ViewFilter = "";
        //private static string _ViewDashboard = "";
        ////private static string _ViewLinkMenu = "";
        //private static string _ViewLeftMenu = "";
        //private static string _ViewDashboard2 = "";
        //private static string _ViewDateRange = "";
        //private static string _ViewFilter2 = "";
        //private static string _EnableHourManifest;

        //private static string _ManifestSortByString = "";
        //private static string _ManifestAscDesc = "";
        //private static string _TotalRowCount = "";
     
        public const string RoleAdministrator = "Administrator";
        public const string RoleCrewAssist = "Crew Assist";
        public const string RoleAirSpecialist = "Air Specialist";
        public const string RoleCrewAdmin = "Crew Admin";
        public const string RoleHotelSpecialist = "Hotel Specialist";
        public const string RoleVehicleSpecialist = "Vehicle Specialist";
        public const string RolePortSpecialist = "Service Provider";
        public const string RoleHotelVendor = "Hotel Vendor";
        public const string RoleVehicleVendor = "Vehicle Vendor";
        public const string RoleContractManager = "Contract Manager";
        public const string RoleImmigration= "Immigration Officer";
        public const string RoleMeetGreet = "Meet and Greet";
        public const string RoleFinance = "Finance";
        public const string RoleSystemAnalyst = "System Analyst";
        public const string RoleCrewMedical = "Crew Medical";
        public const string RoleCrewAssistTeamLead = "Crew Assist Team Lead";

        public const string RoleDriver = "Driver";
        public const string RoleGreeter = "Greeter";
        
        public const string DateFormat = "MM/dd/yyyy";
        public const string DateTimeFormat = "MM/dd/yyyy HH:mm:ss:fff";
        public const string DateTimeFormatFileExtension = "MMddyy_HH-mm-ss-fff";
        public const string UserCultureInfo = "en-US";

        public static string HotelSpecialistEmail = "";

        //public static string _ManifestHrs = "";


        //private static string _TreadUserID = "";

        //private static string _ItineraryCode = "";

        ////for audit trail - Gabriel Oquialda 17/02/2012
        //private static string _gHotelPath = "";
        //private static string _gVehiclePath = "";
        //private static string _gPortPath = "";
        //private static string _gRequestPath = "";

        //public static string strCreateMessge
        //{
        //    get
        //    {
        //        return _strCreateMessge;
        //    }
        //    set
        //    {
        //        _strCreateMessge = value;
        //    }
        //}
        //public static string strPrevPage
        //{
        //    get
        //    {
        //        return _strPrevPage;
        //    }
        //    set
        //    {
        //        _strPrevPage = value;
        //    }
        //}
        //public static string strCurrentPage
        //{
        //    get
        //    {
        //        return _strCurrentPage;
        //    }
        //    set
        //    {
        //        _strCurrentPage = value;
        //    }
        //}
        //public static string strSFCode
        //{
        //    get
        //    {
        //        return _strSFID;
        //    }
        //    set
        //    {
        //        _strSFID = value;
        //    }
        //}
        //public static string strSFID
        //{
        //    get
        //    {
        //        return _strSFID;
        //    }
        //    set
        //    {
        //        _strSFID = value;
        //    }
        //}
        //public static string strSFStatus
        //{
        //    get
        //    {
        //        return _strSFStatus;
        //    }
        //    set
        //    {
        //        _strSFStatus = value;
        //    }
        //}
        //public static string strSFFlightDateRange
        //{
        //    get
        //    {
        //        return _strSFFlightDateRange;
        //    }
        //    set
        //    {
        //        _strSFFlightDateRange = value;
        //    }
        //}
        //public static string strSFSeqNo
        //{
        //    get 
        //    {
        //        return _strSFSeqNo;
        //    }
        //    set 
        //    {
        //        _strSFSeqNo = value;
        //    }
        //}
        //public static string strPendingFilter
        //{
        //    get
        //    {
        //        return _strPendingFilter;
        //    }
        //    set
        //    {
        //        _strPendingFilter = value;
        //    }
        //}
        //public static string strAirStatusFilter
        //{
        //    get
        //    {
        //        return _strAirStatusFilter;
        //    }
        //    set
        //    {
        //        _strAirStatusFilter = value;
        //    }
        //}
        //public static string strTravelLocatorID
        //{
        //    get
        //    {
        //        return _strTravelLocatorID;
        //    }
        //    set
        //    {
        //        _strTravelLocatorID = value; 
        //    }
        //}
        //public static string strRecordLocator
        //{
        //    get
        //    {
        //        return _strRecordLocator;
        //    }
        //    set
        //    {
        //        _strRecordLocator = value; 
        //    }
        //}
        //public static string strOnOffDate
        //{
        //    get
        //    {
        //        return _strOnOffDate;
        //    }
        //    set
        //    {
        //        _strOnOffDate = value;
        //    }
        //}        
        //public static string UserRoleString
        //{
        //    get
        //    {
        //        return _UserRoleString;
        //    }
        //    set
        //    {
        //        _UserRoleString = value;
        //    }
        //}
        //public static string UserRoleKeyString
        //{
        //    get
        //    {
        //        return _UserRoleKeyString;
        //    }
        //    set
        //    {
        //        _UserRoleKeyString = value;
        //    }
        //}
        //public static string UserCountryString
        //{
        //    get 
        //    {
        //        return _UserCountryString;
        //    }
        //    set
        //    {
        //        _UserCountryString = value;
        //    }
        //}
        //public static string UserCityString
        //{
        //    get
        //    {
        //        return _UserCityString;
        //    }
        //    set
        //    {
        //        _UserCityString = value;
        //    }
        //}
        //public static string SelectedRoleKeyString
        //{
        //    get
        //    {
        //        return _SelectedRoleKeyString;
        //    }
        //    set
        //    {
        //        _SelectedRoleKeyString = value;
        //    }
        //}
        //public static string SelectedRoleNameString
        //{
        //    get
        //    {
        //        return _SelectedRoleNameString;
        //    }
        //    set
        //    {
        //        _SelectedRoleNameString = value;
        //    }
        //}
        //public static string TravelRequestIDString
        //{
        //    get
        //    {
        //        return _TravelRequestIDString;
        //    }
        //    set
        //    {
        //        _TravelRequestIDString = value;
        //    }
        //}
        //public static string ManualRequestIDString
        //{
        //    get
        //    {
        //        return _ManualRequestIDString;
        //    }
        //    set
        //    {
        //        _ManualRequestIDString = value;
        //    }
        //}
        //public static string RegionString
        //{
        //    get 
        //    {
        //        return _RegionString;
        //    }
        //    set 
        //    {
        //        _RegionString = value;
        //    }
        //}
        //public static string CountryString
        //{
        //    get
        //    {
        //        return _CountryString;
        //    }
        //    set
        //    {
        //        _CountryString = value;
        //    }
        //}
        //public static string CityString
        //{
        //    get
        //    {
        //        return _CityString;
        //    }
        //    set
        //    {
        //        _CityString = value;
        //    }
        //}
        //public static string AirportString
        //{
        //    get
        //    {
        //        return _AirportString;
        //    }
        //    set
        //    {
        //        _AirportString = value;
        //    }
        //}
        //public static string DateFromString
        //{
        //    get
        //    {
        //        if (_DateFromString == "")
        //        {
        //            return DateTime.Now.ToString();
        //        }
        //        else
        //        {
        //            return _DateFromString;
        //        }
        //    }
        //    set
        //    {
        //        _DateFromString = value;
        //    }
        //}
        //public static string DateToString
        //{
        //    get
        //    {
        //        if (_DateToString == "")
        //        {
        //            return DateTime.Now.ToString();
        //        }
        //        else
        //        {
        //            return _DateToString;
        //        }
        //    }
        //    set
        //    {
        //        _DateToString = value;
        //    }
        //}
        //public static string PortString
        //{
        //    get
        //    {
        //        return _PortString;
        //    }
        //    set
        //    {
        //        _PortString = value;
        //    }
        //}
        //public static string HotelString
        //{
        //    get
        //    {
        //        return _HotelString;
        //    }
        //    set
        //    {
        //        _HotelString = value;
        //    }
        //}
        //public static string HotelNameToSearch
        //{
        //    get
        //    {
        //        return _HotelNameToSearch;
        //    }
        //    set
        //    {
        //        _HotelNameToSearch = value;
        //    }
        //}
        //public static string VehicleString
        //{
        //    get
        //    {
        //        return _VehicleString;
        //    }
        //    set
        //    {
        //        _VehicleString = value;
        //    }
        //}
        //public static string CountryNameString
        //{
        //    get
        //    {
        //        return _CountryNameString;
        //    }
        //    set
        //    {
        //        _CountryNameString = value;
        //    }
        //}
        //public static string CityNameString
        //{
        //    get
        //    {
        //        return _CityNameString;
        //    }
        //    set
        //    {
        //        _CityNameString = value;
        //    }
        //}
        //public static string ViewRegion
        //{
        //    get
        //    {
        //        return _ViewRegion;
        //    }
        //    set
        //    {
        //        _ViewRegion = value;
        //    }
        //}
        //public static string ViewCountry
        //{
        //    get
        //    {
        //        return _ViewCountry;
        //    }
        //    set
        //    {
        //        _ViewCountry = value;
        //    }
        //}
        //public static string ViewCity
        //{
        //    get
        //    {
        //        return _ViewCity;
        //    }
        //    set
        //    {
        //        _ViewCity = value;
        //    }
        //}
        //public static string ViewHotel
        //{
        //    get
        //    {
        //        return _ViewHotel;
        //    }
        //    set
        //    {
        //        _ViewHotel = value;
        //    }
        //}
        //public static string ViewPort
        //{
        //    get
        //    {
        //        return _ViewPort;
        //    }
        //    set
        //    {
        //        _ViewPort = value;
        //    }
        //}
        //public static string ViewLegend
        //{
        //    get
        //    {
        //        return _ViewLegend;
        //    }
        //    set
        //    {
        //        _ViewLegend = value;
        //    }
        //}
        //public static string ViewDashboard
        //{
        //    get
        //    {
        //        return _ViewDashboard;
        //    }
        //    set
        //    {
        //        _ViewDashboard = value;
        //    }
        //}
        //public static string ViewDashboard2
        //{
        //    get
        //    {
        //        return _ViewDashboard2;
        //    }
        //    set
        //    {
        //        _ViewDashboard2 = value;
        //    }
        //}

        //public static string ViewDateRange
        //{
        //    get
        //    {
        //        return _ViewDateRange;
        //    }
        //    set
        //    {
        //        _ViewDateRange = value;
        //    }
        //}
        //public static string EnableHourManifest
        //{
        //    get
        //    {
        //        return _EnableHourManifest;
        //    }
        //    set
        //    {
        //        _EnableHourManifest = value;
        //    }
        //}
        //public static string ViewFilter
        //{
        //    get
        //    {
        //        return _ViewFilter;
        //    }
        //    set
        //    {
        //        _ViewFilter = value;
        //    }
        //}

        //public static string ViewFilter2
        //{
        //    get
        //    {
        //        return _ViewFilter2;
        //    }
        //    set
        //    {
        //        _ViewFilter2 = value;
        //    }
        //}
        ////public static string ViewLinkMenu
        ////{
        ////    get
        ////    {
        ////        return _ViewLinkMenu;
        ////    }
        ////    set
        ////    {
        ////        _ViewLinkMenu = value;
        ////    }
        ////}
        //public static string ViewLeftMenu
        //{
        //    get
        //    {
        //        return _ViewLeftMenu;
        //    }
        //    set
        //    {
        //        _ViewLeftMenu = value;
        //    }
        //}
        //public static string ManifestSortByString
        //{
        //    get
        //    {
        //        return _ManifestSortByString;
        //    }
        //    set
        //    {
        //        _ManifestSortByString = value;
        //    }
        //}
        //public static string ManifestAscDesc
        //{
        //    get
        //    {
        //        return _ManifestAscDesc;
        //    }
        //    set
        //    {
        //        _ManifestAscDesc = value;
        //    }
        //}
        //public static string TotalRowCount
        //{
        //    get
        //    {
        //        return _TotalRowCount;
        //    }
        //    set
        //    {
        //        _TotalRowCount = value;
        //    }
        //}
        //public static string UserVendorString
        //{
        //    get
        //    {
        //        return _UserVendorString;
        //    }
        //    set
        //    {
        //        _UserVendorString = value;
        //    }
        //}
        //public static string UserBranchIDString
        //{
        //    get
        //    {
        //        return _UserBranchIDString;
        //    }
        //    set 
        //    {
        //        _UserBranchIDString = value;
        //    }
        //}
        //public static string ManifestHrs
        //{
        //    get
        //    {
        //        return _ManifestHrs;
        //    }
        //    set
        //    {
        //        _ManifestHrs = value;
        //    }
        //}


        //public static string TreadUserID
        //{
        //    get
        //    {
        //        return _TreadUserID;
        //    }
        //    set
        //    {
        //        _TreadUserID = value;
        //    }
        //}

        //public static string ItineraryCode
        //{
        //    get
        //    {
        //        return _ItineraryCode;
        //    }
        //    set
        //    {
        //        _ItineraryCode = value;
        //    }
        //}

        //public static string gHotelPath
        //{
        //    get
        //    {
        //        return _gHotelPath;
        //    }
        //    set
        //    {
        //        _gHotelPath = value;
        //    }
        //}

        //public static string gVehiclePath
        //{
        //    get
        //    {
        //        return _gVehiclePath;
        //    }
        //    set
        //    {
        //        _gVehiclePath = value;
        //    }
        //}

        //public static string gPortPath
        //{
        //    get
        //    {
        //        return _gPortPath;
        //    }
        //    set
        //    {
        //        _gPortPath = value;
        //    }
        //}

        //public static string gRequestPath
        //{
        //    get
        //    {
        //        return _gRequestPath;
        //    }
        //    set
        //    {
        //        _gRequestPath = value;
        //    }
        //}
        public class SheetRange
        {
            public int Number { get; set; }
            public string Letter { get; set; }

        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   17/Mar/2013
        /// Description:    Sheet to be used in Excel export
        /// ------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static List<SheetRange> SheetColumn()
        {
            List<SheetRange> list = new List<SheetRange>();
            SheetRange item = new SheetRange();
            item.Number = 1;
            item.Letter = "A";
            list.Add(item);

            item = new SheetRange();
            item.Number = 2;
            item.Letter = "B";
            list.Add(item);

            item = new SheetRange();
            item.Number = 3;
            item.Letter = "C";
            list.Add(item);

            item = new SheetRange();
            item.Number = 4;
            item.Letter = "D";
            list.Add(item);

            item = new SheetRange();
            item.Number = 5;
            item.Letter = "E";
            list.Add(item);

            item = new SheetRange();
            item.Number = 6;
            item.Letter = "F";
            list.Add(item);

            item = new SheetRange();
            item.Number = 7;
            item.Letter = "G";
            list.Add(item);

            item = new SheetRange();
            item.Number = 8;
            item.Letter = "H";
            list.Add(item);

            item = new SheetRange();
            item.Number = 9;
            item.Letter = "I";
            list.Add(item);

            item = new SheetRange();
            item.Number = 10;
            item.Letter = "J";
            list.Add(item);

            item = new SheetRange();
            item.Number = 11;
            item.Letter = "K";
            list.Add(item);

            item = new SheetRange();
            item.Number = 12;
            item.Letter = "L";
            list.Add(item);

            item = new SheetRange();
            item.Number = 13;
            item.Letter = "M";
            list.Add(item);

            item = new SheetRange();
            item.Number = 14;
            item.Letter = "N";
            list.Add(item);

            item = new SheetRange();
            item.Number = 15;
            item.Letter = "O";
            list.Add(item);

            item = new SheetRange();
            item.Number = 16;
            item.Letter = "P";
            list.Add(item);

            item = new SheetRange();
            item.Number = 17;
            item.Letter = "Q";
            list.Add(item);

            item = new SheetRange();
            item.Number = 18;
            item.Letter = "R";
            list.Add(item);

            item = new SheetRange();
            item.Number = 19;
            item.Letter = "S";
            list.Add(item);

            item = new SheetRange();
            item.Number = 20;
            item.Letter = "T";
            list.Add(item);

            item = new SheetRange();
            item.Number = 21;
            item.Letter = "U";
            list.Add(item);

            item = new SheetRange();
            item.Number = 22;
            item.Letter = "V";
            list.Add(item);

            item = new SheetRange();
            item.Number = 23;
            item.Letter = "W";
            list.Add(item);

            item = new SheetRange();
            item.Number = 24;
            item.Letter = "X";
            list.Add(item);

            item = new SheetRange();
            item.Number = 25;
            item.Letter = "Y";
            list.Add(item);

            item = new SheetRange();
            item.Number = 26;
            item.Letter = "Z";
            list.Add(item);

            item = new SheetRange();
            item.Number = 27;
            item.Letter = "AA";
            list.Add(item);

            item = new SheetRange();
            item.Number = 28;
            item.Letter = "AB";
            list.Add(item);

            item = new SheetRange();
            item.Number = 29;
            item.Letter = "AC";
            list.Add(item);

            item = new SheetRange();
            item.Number = 30;
            item.Letter = "AD";
            list.Add(item);

            item = new SheetRange();
            item.Number = 31;
            item.Letter = "AE";
            list.Add(item);

            item = new SheetRange();
            item.Number = 32;
            item.Letter = "AF";
            list.Add(item);

            item = new SheetRange();
            item.Number = 33;
            item.Letter = "AG";
            list.Add(item);

            item = new SheetRange();
            item.Number = 34;
            item.Letter = "AH";
            list.Add(item);

            item = new SheetRange();
            item.Number = 35;
            item.Letter = "AI";
            list.Add(item);

            item = new SheetRange();
            item.Number = 36;
            item.Letter = "AJ";
            list.Add(item);

            item = new SheetRange();
            item.Number = 37;
            item.Letter = "AK";
            list.Add(item);

            item = new SheetRange();
            item.Number = 38;
            item.Letter = "AL";
            list.Add(item);

            item = new SheetRange();
            item.Number = 39;
            item.Letter = "AM";
            list.Add(item);

            item = new SheetRange();
            item.Number = 40;
            item.Letter = "AN";
            list.Add(item);

            return list;
        }
    }
    
}

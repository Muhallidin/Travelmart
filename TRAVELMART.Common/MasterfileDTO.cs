using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   04/Mar/2014
    /// Created By:     Josephine Gad
    /// (description)   Brand List
    /// </summary>
    /// 
    [Serializable]
    public class BrandList
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public bool IsAssigned { get; set; }
    }
    /// <summary>
    /// Date Created:   04/10/2011
    /// Created By:     Josephine Gad
    /// (description)   Set vessel class
    /// </summary>
    /// 
    [Serializable]
    public class VesselDTO
    {
        public VesselDTO() { }

        public string VesselIDString { get; set; }
        public string VesselNameString { get; set; }
        //public string PortIDString = null;
    }
    /// <summary>
    /// Date Created:   14/02/2011
    /// Created By:     Josephine Gad
    /// (description)   Create list class for VesselDTO
    /// </summary>
    public class VesselDTOList
    {
        public static List<VesselDTO> VesselList { get; set; }
    }
    /// <summary>
    /// Date Created:   17/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Set vessel details class
    /// </summary>
    public class VesselDetailsDTO
    {
        public VesselDetailsDTO() { }

        public string PortName = null;
        public string CountryName = null;
        public string PortID = null;
        public string CountryID = null;
    }
    /// <summary>
    /// Date Created:   07/10/2011
    /// Created By:     Josephine Gad
    /// (description)   Set country class 
    /// </summary>
    [Serializable]
    public class CountryDTO
    {
        public CountryDTO() { }

        public string CountryIDString;
        public string CountryNameString;
    }
    /// <summary>
    /// Date Created:   07/10/2011
    /// Created By:     Josephine Gad
    /// (description)   Set City class
    /// </summary>
    [Serializable]
    public class CityDTO
    {
        public CityDTO() { }

        public string CityIDString = null;
        public string CityNameString = null;
    }
    /// <summary>
    /// Date Created:   19/01/2012
    /// Created By:     Gelo Oquialda
    /// (description)   Set Airport class
    /// </summary>
    /// 
    [Serializable]
    public class AirportDTO
    {
        public AirportDTO() { }

        public string AirportIDString { get; set; }
        public string AirportNameString { get; set; }
        public string AirportCodeString { get; set; }
    }
    /// <summary>
    /// Date Created:   14/Nov/2013
    /// Created By:     Josephine Gad
    /// (description)   Set Seaport class
    /// </summary>
    /// 
    [Serializable]
    public class SeaportDTO
    {
        public string SeaportIDString { get; set; }
        public string SeaportNameString { get; set; }
        public string SeaportCodeString { get; set; }

    }
    /// <summary>
    /// Date Created:   07/10/2011
    /// Created By:     Josephine Gad
    /// (description)   Set Hotel class 
    /// </summary>
    /// 
    [Serializable]
    public class HotelDTO
    {
        public HotelDTO() { }

        public string HotelIDString {get; set;}
        public string HotelNameString { get; set; }
    }
    /// <summary>
    /// Date Created:   07/10/2011
    /// Created By:     Josephine Gad
    /// (description)   Set Port class  
    /// </summary>
    public class PortDTO
    {
        public PortDTO() { }

        public string PortIDString = null;
        public string PortNameString = null;
    }
    /// <summary>
    /// Date Created:   14/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Set Service Provider class
    /// </summary>
    /// 
    [Serializable]
    public class PortAgentDTO
    {
        public string PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        public DateTime? EndOfContract { get; set; }
        public DateTime? BeginOfContract { get; set; }   
    }
    /// <summary>
    /// Date Created:   20/May/2014
    /// Created By:     Josephine Gad
    /// (description)   Vehicle Vendor List
    /// </summary>
    [Serializable]
    public class VehicleVendorDTO
    {
        public string VehicleID { get; set; }
        public string VehicleName { get; set; }
    }
    /// <summary>
    /// Date Created:   14/02/2011
    /// Created By:     Josephine Gad
    /// (description)   Create list class for VesselDTO
    /// </summary>
    //public class PortAgentDTOList
    //{
    //    public static List<PortAgentDTO> PortAgentList { get; set; }
    //}

    /// <summary>
    /// Date Created:   29/11/2011
    /// Created By:     Josephine Gad
    /// (description)   Set Home Dashboard Count Summary  
    /// </summary>
    public class HomeDashboardDTO
    {
        public HomeDashboardDTO() { }

        public string Onsigning = null;
        public string Offsigning = null;
    }
    /// <summary>
    /// Date Created:   09/12/2011
    /// Created By:     Josephine Gad
    /// (description)   Seafarer's information
    /// </summary>
    public class SeafarerDTO
    {
        public SeafarerDTO() { }

        public string Nationality = null;
        public string Gender = null;
        public string Rank = null;
        public string CostCenter = null;
        public string NationalityID = null;
        public string RankID = null;
        public string CostCenterID = null;
    }
    /// <summary>
    /// Date Created:   17/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Seafarer List
    /// </summary>
    public class SeafarerListDTO
    {
        public string SFID {get; set;}
        public string Name {get; set;}
    }
    /// <summary>
    /// Date Created:   16/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Set Hotel Room Blocks  (contract and override) Class 
    /// -------------------------------------
    /// Date Modified:  04/Feb/2013
    /// Created By:     Josephine Gad
    /// (description)   Add field IsWithOverflow 
    /// -------------------------------------
    /// Date Modified:  30/May/2013
    /// Created By:     Josephine Gad
    /// (description)   Add field IsWithSuttle
    ///                 Add IsMealBreakfast, IsMealLunch
    ///                 Add IsMealDinner, IsMealLunchDinner       
    /// -------------------------------------
    /// </summary>
    public class HotelRoomBlocksDTO
    { 
        public string BranchIDInt = null;
        public string BranchName = null;

        public string OverrideCurrentcyID = null;
        public string OverrideRate = null;
        public string OverrideTaxPercent = null;
        public string OverrideRoomBlocks = null;
        public bool OverrideIsTaxInclusive = false;

        public string ContractRoomBlocks = null;
        public string ContractCurrencyID = null;
        public string ContractRate = null;
        public string ContractTaxPercent = null;
        public bool ContractIsTaxInclusive = false;

        public string ContractCurrency = null;
        public string OverrideReservedRoom = null;
        public bool IsWithOverflow = false;

        public int ContractID = 0;
        public bool IsWithSuttle = false;

        public bool IsMealBreakfast = false;
        public bool IsMealLunch = false;
        public bool IsMealDinner = false;
        public bool IsMealLunchDinner = false;

    }
    /// <summary>
    /// Date Created:   16/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Set Hotel Room Blocks (emergency) Class 
    /// </summary>
    public class HotelRoomBlocksEmergencyDTO
    {
        public string BranchIDInt = null;
        public string BranchName = null;

        public string Currency = null;
        public string Rate = null;
        public string Tax = null;
        public string RoomBlocks = null;
        public bool IsTaxInclusive;
    }
    /// <summary>
    /// Date Created:   06/03/2012
    /// Created By:     Josephine Gad
    /// (description)   Set Hotel and Airport
    /// </summary>
    public class HotelAirportDTO
    {
        public Int32 BranchID {get; set;}
        public string BranchName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        //public static List<HotelAirportDTO> HotelAirportList { get; set; }
    }
    /// <summary>
    /// Date Created:   24/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Nationality
    /// -----------------------------------
    /// Date Modified:  25/Nov/2014
    /// Modified By:    Josephine Monteza
    /// (description)   Add NationalityCode
    /// </summary>
    /// 
    [Serializable]
    public class NationalityList
    {
        public int NationalityID { get; set; }
        public string Nationality { get; set; }
        public string NationalityCode { get; set; }
        public bool IsOKTB { get; set; }
    }
    /// <summary>
    /// Date Created:   24/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Gender
    /// </summary>
    /// 
    [Serializable]
    public class GenderList
    {
        public int GenderID { get; set; }
        public string Gender { get; set; }
    }
    /// <summary>
    /// Date Created:   24/08/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Rank
    /// </summary>
    /// 
    [Serializable]
    public class RankList
    {
        public int RankID { get; set; }
        public string Rank { get; set; }
    }
    /// <summary>
    /// Date Created:   24/09/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for User Vessel
    /// </summary>
    public class UserVesselList
    {
        public string UserName { get; set; }
        public int VesselID {get;set;}
    }
    /// <summary>
    /// Date Created:   25/09/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for User Airport
    /// </summary>
    public class UserAirportList
    {
        public string UserName { get; set; }
        public int AirportID { get; set; }
    }
    /// <summary>
    /// Date Created:   25/09/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for User Seaport
    /// </summary>
    public class UserSeaportList
    {
        public string UserName { get; set; }
        public int SeaportID { get; set; }
    }
    /// <summary>
    /// Date Created:   03/Mar/2014
    /// Created By:     Josephine Gad
    /// (description)   Class for User Service Provider
    /// </summary>
    public class UserPortAgentList
    {
        public string UserName { get; set; }
        public int PortAgentID { get; set; }
    }
    /// <summary>
    /// Date Created:   27/Nov/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for user menu
    /// </summary>
    /// 
    [Serializable]
    public class UserMenus
    {
        public int PageIDInt { get; set; }
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public string DisplayName { get; set; }
    }
    [Serializable]
    public class UserSubMenus
    {
        public int ParentIDInt { get; set; }
        public int PageIDInt { get; set; }
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public string DisplayName { get; set; }
        public int Sequence { get; set; }
    }
    public class UserMenuList
    {
        public List<UserMenus> UserMenu { get; set; }
        public List<UserSubMenus> UserSubMenu { get; set; }
    }

    /// <summary>
    /// Date Created:   02/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor
    /// ---------------------------------------------
    /// Date Modified:  28/Feb/2014
    /// Created By:     Josephine Gad
    /// (description)   Add IsWithContract and colContractIdInt
    /// ---------------------------------------------
    /// Date Modified:  11/Jul/2014
    /// Created By:     Josephine Gad
    /// (description)   Add CssEditContractAddVisible and CssContractListVisible
    /// </summary>
    [Serializable]
    public class VendorVehicleList
    {
        public int VehicleID { get; set; }
        public string VendorName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailTo { get; set; }
        public string Website { get; set; }
        public bool IsWithContract { get; set; }
        public int colContractIdInt { get; set; }

        public string CssEditContractAddVisible { get; set; }
        public string CssEditContractVisible { get; set; }
        public string CssAddContractVisible { get; set; }
        public string CssContractListVisible { get; set; }

    }

    /// <summary>
    /// Date Created:   06/Nov/2013
    /// Created By:     Marco Abejar
    /// (description)   Class for Safeguard Vendor
    /// </summary>
    [Serializable]
    public class VendorSafeguardList
    {
        public int SafeguardID { get; set; }
        public string VendorName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailTo { get; set; }
        public string Website { get; set; }
    }
    /// <summary>
    /// Date Created:   02/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor
    /// </summary>
    [Serializable]
    public class VendorVehicleDetails
    {
        public int VehicleID { get; set; }
        public string VendorName { get; set; }
        public Int32 CountryID { get; set; }
        public string CountryName { get; set; }
        public Int32 CityID { get; set; }
        public string CityName { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string Website { get; set; }
        public string VendorIMS_ID { get; set; }
    }
    /// <summary>
    /// Date Created:   03/Nov/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Service Provider Vendor
    /// </summary>
    [Serializable]
    public class VendorPortAgentList
    {
        public int PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string EmailTo { get; set; }
        public string Website { get; set; }

        public string BrandCode { get; set; }
        public Int16 Priority { get; set; }
        public int ContractID { get; set; }
        public bool IsWithContract { get; set; }

    }
    /// <summary>
    /// Date Created:   05/Nov/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for PortAgent Vendor
    /// </summary>
    [Serializable]
    public class VendorPortAgentDetails
    {
        public int PortAgentID { get; set; }
        public string PortAgentName { get; set; }
        public Int32 CountryID { get; set; }
        public string CountryName { get; set; }
        public Int32 CityID { get; set; }
        public string CityName { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string Website { get; set; }
        public string VendorIMS_ID { get; set; }
    }
    /// Date Created:   11/Nov/2013
    /// Created By:     Josephine Gad
    /// (description)   Services Type of Service Provider
    /// </summary>
    [Serializable]
    public class PortAgentServices
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
    }
    /// Date Created:   01/Apr/2014
    /// Created By:     Josephine Gad
    /// (description)   Room Type
    /// </summary>
    [Serializable]
    public class RoomType
    {
        public float RoomID { get; set; }
        public string RoomName { get; set; }
    }

    public class TMSettings
    {
        public static int NoOfDays { get; set; }
        public static int NoOfDaysForecast { get; set; }
        public static int NoOfDaysForecastVendor { get; set; }

        public static DateTime E1CHLastProcessedDate { get; set; }

        public static List<RoomType> RoomType { get; set; }
    }
    /// <summary>
    /// Date Created:   30/June/2014
    /// Created By:     Josephine Gad
    /// (description)   Class for Hotel Vendor Details
    /// ---------------------------------------------  
    /// </summary>
    [Serializable]
    public class VendorHotelList
    {
        public int BrandAirHotelID { get; set; }
        public int HotelID { get; set; }
        public int VendorID { get; set; }
        public string HotelName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        //public string ContactNo { get; set; }
        //public string FaxNo { get; set; }
        //public string EmailTo { get; set; }
        //public string Website { get; set; }
        public bool IsWithContract { get; set; }
        public string ContractStatus { get; set; }
        public int colContractIdInt { get; set; }

        public bool IsContractListVisible { get; set; }
        public bool IsContractAddEditVisible { get; set; }
        public bool IsPriorityVisible { get; set; }
        public string Priority { get; set; }
        public string Email { get; set; }
    }   
}

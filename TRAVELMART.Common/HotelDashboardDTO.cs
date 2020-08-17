using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   08/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Create Data Transfer Object (DTO) for Hotel Dashboard Page
    /// </summary>
    public class HotelDashboardDTO
    {
        //public static List<HotelDashboardList> HotelDashboardList{ get; set; }
        //public List<HotelDashboardList> GetHotelDashboardList(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
        //    Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, 
        //    DateTime dFrom, DateTime dTo, string sBranchName, int StartRow, int MaxRow)
        //{
        //    try
        //    {
        //        return HotelDashboardList;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        HotelDashboardList = null;
        //    }
        //}
        //public static int? HotelDashboardListCount { get; set; }
        //public int GetHotelDashboardListCount(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
        //    Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, 
        //    DateTime dFrom, DateTime dTo, string sBranchName)
        //{
        //    try
        //    {
        //        return (int)HotelDashboardListCount;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        HotelDashboardListCount = null;
        //    }
        //}
        //public static Int32? HotelExceptionCount { get; set; }
        //public static Int32? NoContractCount { get; set; }

        //public static Int32? HotelOverflowCount { get; set; }
        //public static Int32? NoTravelRequestCount { get; set; }
        //public static Int32? ArrDeptSameOnOffDateCount { get; set; }
        //public int GetHotelExceptionCount()
        //{
        //    try
        //    {
        //        return (int)HotelExceptionCount;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        HotelExceptionCount = null;
        //    }
        //}
        //public static List<HotelExceptionNoTravelRequestList> HotelExceptionNoTravelRequestList { get; set; }

        /// <summary>
        /// Date Created:   22/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Region List
        /// </summary>
        //public static List<RegionList> RegionList { get; set; }
        //public List<RegionList> GetRegionList()
        //{
        //    try
        //    {
        //        return RegionList;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        RegionList = null;
        //    }
        //}
       
    }
    /// <summary>
    /// Date Created:   08/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Hotel Dashboard List
    /// </summary>
    /// 
    [Serializable]
    public class HotelDashboardList
    {
        public Int32 RowNo { get; set; }
        public Int64 BranchID { get; set; }
        public Int32 BrandID { get; set; }
        public Int16 RoomTypeID { get; set; }
        public string HotelBranchName { get; set; }
        public DateTime colDate { get; set; }
        public string colDateName { get; set; }
        public string RoomType { get; set; }
        public decimal ReservedCrew { get; set; }
        public decimal OverflowCrew { get; set; }
        public decimal TotalCrew { get; set; }
        public decimal ReservedRoom{ get; set; }
        public decimal TotalRoomBlocks { get; set; }
        public decimal AvailableRoomBlocks { get; set; }
        public decimal TotalException { get; set; }
        public decimal EmergencyRoomBlocks { get; set; }
        public decimal AvailableEmergencyRoomBlocks { get; set; }
        public bool IsWithEvent { get; set; }
        public bool IsAccredited { get; set; }
        public bool IsWithContract { get; set; }
        public decimal TotalSingleRoomBlock { get; set; }
        public decimal TotalDoubleRoomBlock { get; set; }
        public decimal TotalSingleAvailableRoom { get; set; }
        public decimal TotalDoubleAvailableRoom { get; set; }
        public decimal SingleAvailableContractRooms { get; set; }
        public decimal DoubleAvailableContractRooms { get; set; }
        public decimal SingleAvailableOverrideRooms { get; set; }
        public decimal DoubleAvailableOverrideRooms { get; set; }
        public int ContractId {get;set;}
        public int CountryId { get; set; }
        public int CityIdInt { get; set; }
    }    
    /// <summary>
    /// Date Created:   16/03/2012
    /// Created By:     Gabriel Oquialda
    /// (description)   Hotel Exception and No Travel Request List
    /// ------------------------------------------
    /// Date Modified:  02/04/2012
    /// Modified By:    Josephine Gad
    /// (description)   Add ArrDeptSameOnOffDateCount
    /// </summary>
    /// 
    [Serializable]
    public class HotelExceptionNoTravelRequestList
    {
        //public Int32 Total { get; set; }               
        //public string Title { get; set; }
        public DateTime colDate { get; set; }
        public int ExceptionCount { get; set; }
        public int NoTravelCount { get; set; }
        public int ArrDeptSameOnOffDateCount { get; set; }
    }
       
    /// <summary>
    /// Date Created:   08/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class to call to assign values
    /// ------------------------------------------
    /// Date Modified:  22/05/2012
    /// Modified By:    Josephine Gad
    /// (description)   Add RegionList
    /// </summary>
    public class HotelDashboardDTOGenericClass
    {
        public List<HotelDashboardList> HotelDashboardList { get; set; }
        public List<HotelExceptionNoTravelRequestList> HotelExceptionNoTravelRequestList { get; set; }
        public int? HotelDashboardListCount { get; set; }
        public Int32? HotelExceptionCount { get; set; }
        public Int32? NoContractCount { get; set; }

        public Int32? HotelOverflowCount { get; set; }
		public Int32? NoTravelRequestCount { get; set; }
        public Int32? ArrDeptSameOnOffDateCount { get; set; }
        public Int32? RestrictedNationalityCount { get; set; }

        public List<RegionList> RegionList { get; set; }
        //public List<PortList> PortList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class Events
    {
        public int? colEventIdInt { get; set; }
        public string colEventNameVarchar { get; set; }
        public DateTime? colEventDateFromDate { get; set; }
        public DateTime? colEventDateToDate { get; set; }
        public bool? colIsDoneBit { get; set; }
        public string colVendorBranchNameVarchar { get; set; }
    }

    #region "Hotel Branch Maintenance"
    /// <summary>
    /// Date Created:   23/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Hotel Branch Voucher
    /// --------------------------------------
    /// Date Modified:   31/05/2012
    /// Modified By:     Josephine Gad
    /// (description)    Remove line "public static List<HotelBranchVoucherList> VoucherList { get; set; }"
    ///                  Add [Serializable] 
    /// --------------------------------------
    /// Date Modified:   17/09/2012
    /// Modified By:     Josephine Gad
    /// (description)    Add VoucherID
    /// --------------------------------------
    ///                                    
    /// </summary>
    /// 
    [Serializable]
    public class HotelBranchVoucherList
    {
        public int VoucherID { get; set; }
        public decimal Stripe {get; set;}
        public decimal Amount { get; set; }
        public Int32 BranchID { get; set; }
        public Int16 DayNo { get; set; }

        //public static List<HotelBranchVoucherList> VoucherList { get; set; }
    }
    /// <summary>
    /// Date Created:   24/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Hotel Branch Room Type
    /// --------------------------------------
    /// Date Modified:   31/05/2012
    /// Modified By:     Josephine Gad
    /// (description)    Remove line "public static List<HotelBranchRoomType> RoomTypeList { get; set; }"
    ///                  Add [Serializable]    
    /// </summary>
    /// 
    [Serializable]
    public class HotelBranchRoomType
    {
        public Int16 colRoomTypeID { get; set; }
        public string colRoomNameVarchar { get; set; }

        //public static List<HotelBranchRoomType> RoomTypeList { get; set; }
    }
    /// <summary>
    /// Date Created:   24/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Hotel Branch Room Type
    /// --------------------------------------
    /// Date Modified:   31/05/2012
    /// Modified By:     Josephine Gad
    /// (description)    Remove line "public static List<HotelBranchRoomTypeNotExist> RoomTypeList { get; set; }"
    ///                  Add [Serializable]
    /// </summary>    
    [Serializable]
    public class HotelBranchRoomTypeNotExist
    {
        public Int16 colRoomTypeID { get; set; }
        public string colRoomNameVarchar { get; set; }

        //public static List<HotelBranchRoomTypeNotExist> RoomTypeList { get; set; }
    }   
    /// <summary>
    /// Date Created:   24/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Department
    /// --------------------------------------
    /// Date Modified:   31/05/2012
    /// Modified By:     Josephine Gad
    /// (description)    Remove line "public static List<Department> DeptList { get; set; }"
    ///                  Add [Serializable]
    /// </summary>
        
    [Serializable]
    public class Department
    {
        public Int16 DeptID { get; set; }
        public string DeptName { get; set; }

        //public static List<Department> DeptList { get; set; }
    }
    /// <summary>
    /// Date Created:    24/02/2012
    /// Created By:      Josephine Gad
    /// (description)    Class for DepartmentStripe in Branch
    /// ------------------------------------------------------
    /// Date Modified:   31/05/2012
    /// Modified By:     Josephine Gad
    /// (description)    Remove line "public static List<HotelBranchDeptStripe> DeptStripeList { get; set; }"
    ///                  Add [Serializable]
    /// </summary>
    /// 
    [Serializable]
    public class HotelBranchDeptStripe
    {
        public Int32? BranchDeptStripeID { get; set; }
        public decimal Stripes { get; set; }
        public string StripeName { get; set; }
        public Int16 DeptID { get; set; }
        public string DeptName { get; set; }

        //public static List<HotelBranchDeptStripe> DeptStripeList { get; set; }
    }
    /// <summary>
    /// Date Created:   24/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for DepartmentStripe not in the Branch
    /// </summary>
    public class HotelBranchDeptStripeNotExist
    {        
        public decimal Stripes { get; set; }
        public string StripeName { get; set; }
        
        public static List<HotelBranchDeptStripeNotExist> DeptStripeList { get; set; }
    }
    /// <summary>
    /// Date Created:   24/02/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Stripes
    /// -----------------------------------
    /// Date Modified:  31/05/2012
    /// Modified By:    Josephine Gad
    /// (description)   Remove line "public static List<Stripe> StripeList { get; set; }"
    /// </summary>
    /// 
    [Serializable]
    public class Stripe
    {
        public int? StripesID { get; set; }
        public decimal Stripes { get; set; }
        public string StripeName { get; set; }

        //public static List<Stripe> StripeList { get; set; }
    }
    /// <summary>
    /// Date Created:   21/03/2012
    /// Created By:     Josephine Gad
    /// (description)   Class Rank Exception
    /// -----------------------------------
    /// Date Modified:  02/04/2012
    /// Modified By:    Josephine Gad
    /// (description)   Add [Serializable]
    /// -----------------------------------
    /// Date Modified:  31/05/2012
    /// Modified By:    Josephine Gad
    /// (description)   Remove line "public static List<HotelRankException> RankExceptionList { get; set; }"
    /// </summary>
    /// 
    [Serializable]
    public class HotelRankException
    {
        public int BranchRankExceptID { get; set; }
        public int BranchID { get; set; }
        public int RankID { get; set; }
        public string RankName { get; set; }        
       // public static List<HotelRankException> RankExceptionList { get; set; }
    }
    /// -----------------------------------
    /// Date Modified:  31/05/2012
    /// Modified By:    Josephine Gad
    /// (description)   Remove line "public static List<HotelRankException> RankExceptionList { get; set; }"
    /// 
    [Serializable]
    public class HotelRankExceptionNotExist
    {
        public int BranchRankExceptID { get; set; }
        public int BranchID { get; set; }
        public int RankID { get; set; }
        public string RankName { get; set; }        
        //public static List<HotelRankExceptionNotExist> RankExceptionList { get; set; }
    }
    #endregion

    /// <summary>
    /// Date Created:   07/03/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Seaport and Airport Matrix
    /// </summary>
    public class SeaportAirport
    {
        public int PortID { get; set; }
        public string PortCode { get; set; }
        public string PortName { get; set; }
        public string CountryPort { get; set; }
        public string City { get; set; }
        public int? AirportID { get; set; }
        public string AirportCode { get; set; }
        public string AirporName { get; set; }
        public string CountryAirPort { get; set; }

        public static List<SeaportAirport> SeaportAirportList { get; set; }
        public static Int32 SeaportAirportCount { get; set; }
    }
    /// <summary>
    /// Date Created:   07/03/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Aiport List
    /// </summary>
    /// 
    [Serializable]
    public class Airport
    {
        public Int32? AirportSeaportID { get; set; }
        public Int32 AirportID { get; set; }
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string AirportCodeName { get; set; }

        public static List<Airport> AirportList { get; set; }
        public static List<Airport> AirportNotInList { get; set; }
    }    
}
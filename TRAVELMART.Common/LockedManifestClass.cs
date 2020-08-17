using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class LockedManifestClass
    {
        //public static List<Vessel> Vessel { get; set; }
        //public static List<SFNationality> SFNationality { get; set; }
        //public static List<SFRank> SFRank { get; set; }
        //public static List<UserBranch> UserBranch { get; set; }
        //public static List<ManifestClass> ManifestType { get; set; }
        //public static List<RegionList> RegionList { get; set; }
        //public static List<PortList> PortList { get; set; }

        //public static int? LockedManifestCount { get; set; }
        //public int GetLockedManifestCount(string UserId, int LoadType, DateTime Date,
        //    int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
        //    string Gender,string Status)
        //{
        //    try
        //    {
        //        return (int)LockedManifestCount;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        LockedManifestCount = null;
        //    }
        //}

        //public static List<LockedManifest> LockedManifest { get; set; }
        //public List<LockedManifest> GetLockedManifest(string UserId, int LoadType, DateTime Date,
        //    int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
        //    string Gender,string Status,int startRowIndex, int maximumRows)
        //{
        //    try
        //    {
        //        return LockedManifest;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        LockedManifest = null;
        //    }
        //}
        public static List<ManifestCalendar> ManifestCalendar { get; set; }
           
        //public static List<LockedManifestDifferenceWithRows> LockedManifestDifferenceWithRows { get; set; }
        //public static int? LockedManifestDifferenceWithRowsCount { get; set; }
        //public static List<LockedManifestDifference> LockedManifestDifference { get; set; }

        //public static List<HotelEmail> HotelEmail { get; set; }
        //public static List<LockedManifestEmail> LockedManifestEmail { get; set; }
        //public static List<ManifestDifferenceEmail> ManifestDifferenceEmail { get; set; }
        public static double BookedInPrevDateCount { get; set; }
    }

    public class LockedManifestGenericClass
    {
        public List<UserBranch> UserBranch { get; set; }
        public List<ManifestClass> ManifestType { get; set; }
        public List<Vessel> Vessel { get; set; }
        public List<SFNationality> SFNationality { get; set; }
        public List<SFRank> SFRank { get; set; }
        public List<ManifestCalendar> ManifestCalendar { get; set; }

        public List<RegionList> RegionList { get; set; }
        public List<PortList> PortList { get; set; }
    }

    public class LockedManifestListClass
    {
        public List<ManifestCalendar> ManifestCalendar { get; set; }
        public int? LockedManifestCount { get; set; }
        public List<LockedManifest> LockedManifest { get; set; }
    }

    public class ManifestDifferenceGenericList
    {
        public int? LockedManifestDifferenceWithRowsCount { get; set; }
        public List<LockedManifestDifferenceWithRows> LockedManifestDifferenceWithRows { get; set; }
    }

    public class ManifestEmailList
    {
        public List<HotelEmail> HotelEmail { get; set; }
        public List<LockedManifestEmail> LockedManifestEmail { get; set; }
        public List<ManifestDifferenceEmail> ManifestDifferenceEmail { get; set; }
    }

}

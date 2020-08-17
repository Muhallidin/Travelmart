using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.DAL;
using System.Web;

namespace TRAVELMART.BLL
{
    public class LockedManifestBLL
    {
        LockedManifestDAL LockedDal = new LockedManifestDAL();
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest values
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  04/10/2012
        /// Description:    Change LockedManifestClass.RegionList to HttpContext.Current.Session["HotelDashboardDTO_RegionList"]
        ///                 Change LockedManifestClass.PortList to HttpContext.Current.Session["LockedManifestClass_PortList"]
        ///                 Change LockedManifestClass.ManifestType to HttpContext.Current.Session["LockedManifestClass_ManifestType"] 
        ///                 Delete LockedManifestClass.UserBranch 
        ///                 Change LockedManifestClass.Vessel to HttpContext.Current.Session["LockedManifestClass_Vessel"]
        ///                 Change LockedManifestClass.SFNationality to HttpContext.Current.Session["LockedManifestClass_SFNationality"]
        ///                 Change LockedManifestClass.SFRank  to HttpContext.Current.Session["LockedManifestClass_SFRank"]
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public void LoadLockedManifestPage(string UserId, DateTime Date, int iRegionID)
        {
            List<LockedManifestGenericClass> listValues = new List<LockedManifestGenericClass>();
            try
            {
                listValues = LockedDal.LoadLockedManifestPage(UserId, Date, iRegionID);
                if (listValues.Count > 0)
                {
                    HttpContext.Current.Session["LockedManifestClass_ManifestType"] = listValues[0].ManifestType;
                    HttpContext.Current.Session["LockedManifestClass_Vessel"] = listValues[0].Vessel;
                    HttpContext.Current.Session["LockedManifestClass_SFNationality"] = listValues[0].SFNationality;
                    HttpContext.Current.Session["LockedManifestClass_SFRank"] = listValues[0].SFRank;

                    HttpContext.Current.Session["HotelDashboardDTO_RegionList"] = listValues[0].RegionList;
                    HttpContext.Current.Session["LockedManifestClass_PortList"] = listValues[0].PortList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 22/03/2012
        /// Description: load locked manifest list paging
        /// -----------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Delete LockedManifestClass.LockedManifest
        ///                 Change LockedManifestClass.LockedManifestCount to Session["LockedManifestClass_LockedManifestCount"]
        /// -----------------------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        public List<LockedManifest> LoadLockedManifestPaging2(string UserId, int LoadType, DateTime Date,
            int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
            string Gender, string Status, int RegionId, int startRowIndex, int maximumRows)
        {
            List<LockedManifest> ManifestList = new List<LockedManifest>();
            try
            {
                int count;
                string lockedBy;
                string lockedDate;
                ManifestList = LockedDal.LoadLockedManifestPaging2(UserId, LoadType, Date,
                    ManifestType, BranchId, VesselId, RankName, sfName, Nationality, sfId,
                    Gender, Status, RegionId, startRowIndex, maximumRows, out count, out lockedBy, out lockedDate);
                HttpContext.Current.Session["LockedManifestClass_LockedManifestCount"] = count;
                HttpContext.Current.Session["LockedManifestClass_LockedManifestLockedBy"] = lockedBy;
                HttpContext.Current.Session["LockedManifestClass_LockedManifestLockedDate"] = lockedDate;
                return ManifestList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int LoadLockedManifestPagingCount2(string UserId, int LoadType, DateTime Date,
            int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
            string Gender, string Status, int RegionId)
        {

            return GlobalCode.Field2Int(HttpContext.Current.Session["LockedManifestClass_LockedManifestCount"]); //(int)LockedManifestClass.LockedManifestCount;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest difference list
        /// -------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestDifferenceWithRows to Session["LockedManifestClass_LockedManifestDifferenceWithRows"]
        ///                 Change LockedManifestClass.LockedManifestDifferenceWithRowsCount to Session["LockedManifestClass_LockedManifestDifferenceWithRowsCount"]
        /// -------------------------------------------
        /// Modified By:    Jefferson Bermundo
        /// Date Modified:  10/29/2012
        /// Description:    Add Locked By and Locked Date for Comparison Manifest
        /// -------------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="CompareType"></param>
        /// <param name="BranchId"></param>
        public void LoadManifestDifference(string UserId, int LoadType, DateTime Date,
           int ManifestType, int CompareType, int BranchId, ref string lockBy, ref string lockDate)
        {
            List<ManifestDifferenceGenericList> ManifestList = new List<ManifestDifferenceGenericList>();
            try
            {
                ManifestList = LockedDal.LoadManifestDifference(UserId, LoadType, Date,
                    ManifestType, CompareType, BranchId, ref lockBy, ref lockDate);

                if (ManifestList.Count > 0)
                {
                    HttpContext.Current.Session["LockedManifestClass_LockedManifestDifferenceWithRows"] = ManifestList[0].LockedManifestDifferenceWithRows;
                    HttpContext.Current.Session["LockedManifestClass_LockedManifestDifferenceWithRowsCount"] = ManifestList[0].LockedManifestDifferenceWithRowsCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestEmail to Session["LockedManifestClass_LockedManifestEmail"]
        ///                 Change LockedManifestClass.ManifestDifferenceEmail to Session
        ///                 Delete LockedManifestClass.HotelEmail because it has no use
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="CompareType"></param>
        /// <param name="Date"></param>
        public void LoadManifestEmailList(string UserId, int LoadType,
             int ManifestType, int BranchId, int CompareType, DateTime Date, int RegionId)
        {
            List<ManifestEmailList> emailList = new List<ManifestEmailList>();
            try
            {
                emailList = LockedDal.LoadManifestEmailList(UserId, LoadType,
                    ManifestType, BranchId, CompareType, Date, RegionId);

                HttpContext.Current.Session["LockedManifestClass_LockedManifestEmail"] = emailList[0].LockedManifestEmail;
                HttpContext.Current.Session["LockedManifestClass_ManifestDifferenceEmail"] = emailList[0].ManifestDifferenceEmail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Modified:   19/03/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ------------------------------------------------------   
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<ManifestCalendar> LoadLockedManifestCalendar(string UserId, DateTime Date, int ManifestType, int BranchId)
        {
            try
            {
                return LockedDal.LoadLockedManifestCalendar(UserId, Date, ManifestType, BranchId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   20/Dec/2012
        /// Description:    Load locked Manifest Summary
        /// ----------------------------------- 
        /// </summary>
        public static List<LockedManifestSummary> LoadLockedManifestSummary(string UserID, Int16 iLoadType,
           string sDateFrom, string sDateTo, Int16 iManifestTypeID,
           int iRegion, int iPort, int iBranch, Int16 FromDefaultView, int StartRow, int MaxRow )
        {
            List<LockedManifestSummary> list = new List<LockedManifestSummary>();

            if (FromDefaultView == 1)
            {
                LockedManifestDAL.LoadLockedManifestSummary(UserID, iLoadType,
                   sDateFrom, sDateTo, iManifestTypeID, iRegion, iPort, iBranch,
                   StartRow, MaxRow);
            }
            list = (List<LockedManifestSummary>)HttpContext.Current.Session["LockedManifestSummary"];
            return list;
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   21/Dec/2012
        /// Description:    Load locked Manifest Summary
        /// ----------------------------------- 
        /// </summary>
        public static int LoadLockedManifestSummaryCount(string UserID, Int16 iLoadType,
           string sDateFrom, string sDateTo, Int16 iManifestTypeID,
           int iRegion, int iPort, int iBranch, Int16 FromDefaultView)
        {
            int iCount = 0;
            iCount = GlobalCode.Field2Int(HttpContext.Current.Session["LockedManifestSummaryCount"]);
            return iCount;
        }
    }
}

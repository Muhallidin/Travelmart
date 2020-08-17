using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class HotelManifestBLL
    {
        private HotelManifestDAL hmDAL = new HotelManifestDAL();
        // <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select manifest type
        /// </summary>
        /// <returns></returns>
        public static DataTable GetManifestType()
        {
            try
            {
                return HotelManifestDAL.GetManifestType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Hotel Branch by User
        /// </summary>
        public static DataTable GetHotelBranchListByUser(string BranchName, string UserName, string UserRole, string sDate)
        {
            try
            {
                return HotelManifestDAL.GetHotelBranchListByUser(BranchName, UserName, UserRole, sDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Hotel Locked Manifest
        /// </summary>
        /// <param name="dDate"></param>
        /// <param name="UserID"></param>
        /// <param name="FilterByDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="Status"></param>
        /// <param name="FilterByNameID"></param>
        /// <param name="FilterNameID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <param name="VehicleID"></param>
        /// <param name="VesselID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="sRole"></param>
        /// <returns></returns>
        public static DataTable GetLockedManifest(DateTime dDate, string UserID, string FilterByDate,
            string RegionID, string CountryID, string CityID, string Status, string FilterByNameID,
            string FilterNameID, string PortID, string HotelID, string VehicleID, string VesselID,
            string Nationality, string Gender, string Rank, string sRole, string ManifestHrs)
        {
            try
            {
                return HotelManifestDAL.GetLockedManifest(dDate, UserID, FilterByDate,
                RegionID, CountryID, CityID, Status, FilterByNameID, FilterNameID,
                PortID, HotelID, VehicleID, VesselID, Nationality, Gender, Rank, sRole, ManifestHrs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   30/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get list of hotel manifest (locked)
        /// </summary>
        /// <param name="dDate"></param>
        /// <param name="UserID"></param>
        /// <param name="FilterByDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="Status"></param>
        /// <param name="FilterByNameID"></param>
        /// <param name="FilterNameID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <param name="VehicleID"></param>
        /// <param name="VesselID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="sRole"></param>
        /// <returns></returns>
        public static DataTable GetListHotelManifest_Locked(DateTime dDate, string UserID, string FilterByDate,
            string RegionID, string CountryID, string CityID, string Status, string FilterByNameID,
            string FilterNameID, string PortID, string HotelID, string VehicleID, string VesselID,
            string Nationality, string Gender, string Rank, string sRole, string ManifestHrs)
        {
            try
            {
                return HotelManifestDAL.GetListHotelManifest_Locked(dDate, UserID, FilterByDate,
                RegionID, CountryID, CityID, Status, FilterByNameID, FilterNameID,
                PortID, HotelID, VehicleID, VesselID, Nationality, Gender, Rank, sRole, ManifestHrs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   29/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Hotel Locked Manifest Count
        /// </summary>
        /// <param name="dDate"></param>
        /// <param name="UserID"></param>
        /// <param name="FilterByDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="Status"></param>
        /// <param name="FilterByNameID"></param>
        /// <param name="FilterNameID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <param name="VehicleID"></param>
        /// <param name="VesselID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="sRole"></param>
        /// <param name="ManifestHrs"></param>
        /// <returns></returns>
        public static DataTable GetLockedManifestDashboard(DateTime dDate, string UserID, string FilterByDate,
            string RegionID, string CountryID, string CityID, string Status, string FilterByNameID,
            string FilterNameID, string PortID, string HotelID, string VehicleID, string VesselID,
            string Nationality, string Gender, string Rank, string sRole, string ManifestHrs)
        {
            try
            {
                return HotelManifestDAL.GetLockedManifestDashboard(dDate, UserID, FilterByDate,
                RegionID, CountryID, CityID, Status, FilterByNameID, FilterNameID,
                PortID, HotelID, VehicleID, VesselID, Nationality, Gender, Rank, sRole, ManifestHrs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>                        
        /// Date Created:   29/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Hotel Tentative Manifest Count
        /// ------------------------------------------------------
        /// Date Modified:   29/11/2011
        /// Modified By:     Josephine Gad
        /// (description)    Delete unused parameter and change parameters
        /// ------------------------------------------------------
        /// Date Modified:   19/03/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ------------------------------------------------------        
        /// </summary>           
        public static List<ManifestCalendar> GetTentativeManifestDashboard(string DateFromString,
            string NameString, string strUser,  string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status,
            string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole, string Hours)
        {                 
            try
            {
                List<ManifestCalendar> list = new List<ManifestCalendar>();

                list = HotelManifestDAL.GetTentativeManifestDashboard(DateFromString,
                 NameString, strUser, DateFilter, ByNameOrID, filterNameOrID,
                 Nationality, Gender, Rank, Status, Region, Country, City, Port, Hotel, Vessel, UserRole);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Ryan Bautista
        /// (description)   Insert Hotel Branch manifest lock
        /// ---------------------------------------
        /// Date Modified:   09/12/2011
        /// Modified By:     Josephine Gad
        /// (description)    Do not add days in Datefrom
        /// </summary>
        public static Int32 InsertHotelManifestLockHeader(string ManifestType, string BranchID, string User, string From)
        {
            try
            {
                Int32 ManifestTypeInt = Convert.ToInt32(ManifestType);
                Int32 BranchIDInt = Convert.ToInt32(BranchID);
                //DateTime DteFrom = Convert.ToDateTime(From).AddHours(ManifestTypeInt);
                DateTime DteFrom = Convert.ToDateTime(From);

                return HotelManifestDAL.InsertHotelManifestLockHeader(ManifestTypeInt, BranchIDInt, User, DteFrom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 11/05/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Lock hotel tentative manifest 
        /// </summary>
        public static Int32 LockHotelTentativeManifest(string DateFromString,
          string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
          string Nationality, string Gender, string Rank, string Status,
          string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
          int StartRow, int MaxRow, Int16 LoadType, string ManifestType)
        {
            try
            {
                return HotelManifestDAL.LockHotelTentativeManifest(DateFromString, strUser, DateFilter, ByNameOrID,
                    filterNameOrID, Nationality, Gender, Rank, Status, Region, Country, City, Port, Hotel, Vessel, UserRole,
                    StartRow, MaxRow, LoadType, ManifestType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Ryan Bautista
        /// (description)   Insert Hotel Branch manifest lock
        /// </summary>
        public static void InsertHotelManifestLockDetails(DataTable dt, Int32 ID)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Int32 HotelID = 0;// Convert.ToInt32(row["pcolTransHotelIDBigInt"].ToString());
                    Int32 IDBigInt = Convert.ToInt32(row["IDBigInt"].ToString());
                    Int32 SeqNoInt = Convert.ToInt32(row["SeqNo"].ToString());
                    Int32 TravReq = Convert.ToInt32(row["colTravelReqIdInt"].ToString());
                    Int32 ReqID = 0; //Convert.ToInt32(row[""].ToString());
                    Int32 VendorID = 0;// Convert.ToInt32(row["colTravelReqIDInt"].ToString());
                    Int32 BranchID = Convert.ToInt32(row["HotelBranchID"].ToString());
                    Int32 RoomID = Convert.ToInt32(row["colRoomNameID"].ToString());
                    string ReservedName = "";// Convert.ToInt32(row["colTravelReqIDInt"].ToString());
                    DateTime StartDate = Convert.ToDateTime(row["colTimeSpanStartDate"].ToString());
                    //DateTime StartTime = Convert.ToDateTime(row["colTravelReqIDInt"].ToString());
                    DateTime EndDate = Convert.ToDateTime(row["colTimeSpanEndDate"].ToString());
                    Int32 Duration = Convert.ToInt32(row["colTimeSpanDurationInt"].ToString());
                    string HotelStatus = row["colHotelStatusVarchar"].ToString();
                    string User = MUser.GetUserName();
                    string SfStatus = row["Status"].ToString();
                    Int32 PortID = Convert.ToInt32(row["colPortIdInt"].ToString());
                    Int32 CountryID = Convert.ToInt32(row["CountryID"].ToString());
                    Int32 CityID = Convert.ToInt32(row["CityID"].ToString());
                    //Int32 SeqNoInt = Convert.ToInt32(row["colTravelReqIDInt"].ToString());
                    //Int32 SeqNoInt = Convert.ToInt32(row["colTravelReqIDInt"].ToString());
                    //Int32 SeqNoInt = Convert.ToInt32(row["colTravelReqIDInt"].ToString());
                    HotelManifestDAL.InsertHotelManifestLockDetails(ID, HotelID, IDBigInt, SeqNoInt, TravReq, ReqID, VendorID, BranchID, RoomID,
                                                                       ReservedName, StartDate, EndDate, Duration, HotelStatus, User, SfStatus,
                                                                       PortID, CountryID, CityID);
                }   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   29/11/2011
        /// (description)   get manifest difference count
        /// </summary>
        /// <param name="ManifestType"></param>
        /// <param name="CompareManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="ManifesDate"></param>
        /// <returns></returns>
        public Int32 GetHotelManifestDiffCount(int ManifestType, int CompareManifestType, int BranchId, DateTime ManifesDate)
        {
            try
            {
                return hmDAL.GetHotelManifestDiffCount(ManifestType, CompareManifestType, BranchId, ManifesDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   29/11/2011
        /// (description)   get manifest difference
        /// </summary>
        /// <param name="ManifestType"></param>
        /// <param name="CompareManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="ManifesDate"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable GetHotelManifestDiff(int ManifestType, int CompareManifestType, int BranchId, DateTime ManifesDate, Int32 startRowIndex, Int32 maximumRows)
        {
            try
            {
                return hmDAL.GetHotelManifestDiff(ManifestType, CompareManifestType, BranchId, ManifesDate, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       01/12/2011
        /// (description)       Check if hotel has manifest for the given date and hrs
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="sBranchID"></param>
        /// <param name="sManifestHrs"></param>
        /// <returns></returns>
        public static bool IsHotelHasLockedManifest(string sDate, string sBranchID, string sManifestHrs)
        {
            try
            {
                return HotelManifestDAL.IsHotelHasLockedManifest(sDate, sBranchID, sManifestHrs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void HotelLastLockedDetails(string sDate, string sBranchID, Int16 iHours, ref string LockedBy, ref string LockedDate)
        {
            try
            {
                HotelManifestDAL.HotelLastLockedDetails(sDate, sBranchID, iHours, ref LockedBy, ref LockedDate);
            }
            catch (Exception excp)
            {
                throw excp;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;


namespace TRAVELMART.DAL
{
    public class HotelManifestDAL
    {
        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select manifest type
        /// </summary>
        /// <returns></returns>
        public static DataTable GetManifestType()
        {
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetManifestType");
                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (comm != null)
                {                    
                    comm.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   28/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Hotel Branch by User
        /// </summary>
        public static DataTable GetHotelBranchListByUser(string BranchName, string UserName, string UserRole, string sDate)
        {
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspSelectHotelBranchListByUser");
                db.AddInParameter(comm, "@pHotelName", DbType.String, BranchName);
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, UserName);
                db.AddInParameter(comm, "@pUserRole", DbType.String, UserRole);
                if (sDate != "")
                {
                    db.AddInParameter(comm, "@pDate", DbType.DateTime, DateTime.Parse(sDate));
                }
                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
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
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspSelectHotelManifestLocked");
                db.AddInParameter(comm, "@pDate", DbType.Date, dDate);
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, UserID);
                db.AddInParameter(comm, "@pFilterByDate", DbType.String, FilterByDate);
                db.AddInParameter(comm, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionID));
                db.AddInParameter(comm, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryID));
                db.AddInParameter(comm, "@pCityIDInt", DbType.Int16, Int16.Parse(CityID));
                db.AddInParameter(comm, "@pStatus", DbType.String, Status);
                db.AddInParameter(comm, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                db.AddInParameter(comm, "@pFilterNameID", DbType.String, FilterNameID);

                db.AddInParameter(comm, "@pPortIDInt", DbType.Int16, Int16.Parse(PortID));
                db.AddInParameter(comm, "@pHotelIDInt", DbType.Int16, Int16.Parse(HotelID));
                db.AddInParameter(comm, "@pVehicleIDInt", DbType.Int16, Int16.Parse(VehicleID));
                db.AddInParameter(comm, "@pVesselIDInt", DbType.Int16, Int16.Parse(VesselID));
                db.AddInParameter(comm, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                db.AddInParameter(comm, "@pGender", DbType.Int16, Int16.Parse(Gender));
                db.AddInParameter(comm, "@pRank", DbType.Int16, Int16.Parse(Rank));
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pManifestHrsTinyint", DbType.Int16, Int16.Parse(ManifestHrs));

                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
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
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetListHotelManifest_Locked");
                db.AddInParameter(comm, "@pDate", DbType.Date, dDate);
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, UserID);
                db.AddInParameter(comm, "@pFilterByDate", DbType.String, FilterByDate);
                db.AddInParameter(comm, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionID));
                db.AddInParameter(comm, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryID));
                db.AddInParameter(comm, "@pCityIDInt", DbType.Int16, Int16.Parse(CityID));
                db.AddInParameter(comm, "@pStatus", DbType.String, Status);
                db.AddInParameter(comm, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                db.AddInParameter(comm, "@pFilterNameID", DbType.String, FilterNameID);

                db.AddInParameter(comm, "@pPortIDInt", DbType.Int16, Int16.Parse(PortID));
                db.AddInParameter(comm, "@pHotelIDInt", DbType.Int16, Int16.Parse(HotelID));
                db.AddInParameter(comm, "@pVehicleIDInt", DbType.Int16, Int16.Parse(VehicleID));
                db.AddInParameter(comm, "@pVesselIDInt", DbType.Int16, Int16.Parse(VesselID));
                db.AddInParameter(comm, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                db.AddInParameter(comm, "@pGender", DbType.Int16, Int16.Parse(Gender));
                db.AddInParameter(comm, "@pRank", DbType.Int16, Int16.Parse(Rank));
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pManifestHrsTinyint", DbType.Int16, Int16.Parse(ManifestHrs));

                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
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
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand comm = null;
            DataTable dt = null;
            try
            {
                comm = db.GetStoredProcCommand("uspSelectHotelManifestLockedDashboard");

                db.AddInParameter(comm, "@pDate", DbType.Date, dDate);
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, UserID);
                db.AddInParameter(comm, "@pFilterByDate", DbType.String, FilterByDate);
                db.AddInParameter(comm, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionID));
                db.AddInParameter(comm, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryID));
                db.AddInParameter(comm, "@pCityIDInt", DbType.Int16, Int16.Parse(CityID));
                db.AddInParameter(comm, "@pStatus", DbType.String, Status);
                db.AddInParameter(comm, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                db.AddInParameter(comm, "@pFilterNameID", DbType.String, FilterNameID);

                db.AddInParameter(comm, "@pPortIDInt", DbType.Int16, Int16.Parse(PortID));
                db.AddInParameter(comm, "@pHotelIDInt", DbType.Int16, Int16.Parse(HotelID));
                db.AddInParameter(comm, "@pVehicleIDInt", DbType.Int16, Int16.Parse(VehicleID));
                db.AddInParameter(comm, "@pVesselIDInt", DbType.Int16, Int16.Parse(VesselID));
                db.AddInParameter(comm, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                db.AddInParameter(comm, "@pGender", DbType.Int16, Int16.Parse(Gender));
                db.AddInParameter(comm, "@pRank", DbType.Int16, Int16.Parse(Rank));
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pManifestHrsTinyint", DbType.Int16, Int16.Parse(ManifestHrs));

                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
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
            string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;           
            DataTable dt = null;            
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelManifestTentativeDashboard");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);
                
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);

                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                
                //SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                List<ManifestCalendar> list = new List<ManifestCalendar>();
                list = (from a in dt.AsEnumerable()
                        select new ManifestCalendar
                        {
                            colDate = GlobalCode.Field2DateTime(a["colDate"]),
                            TotalCount = GlobalCode.Field2Int(a["TotalCount"])
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }                
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Hotel manifest lock 
        /// </summary>
        public static Int32 InsertHotelManifestLockHeader(Int32 ManifestType, Int32 BranchID, string User, DateTime DteFrom)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelManifestLockHeader");
                SFDatebase.AddInParameter(SFDbCommand, "pManifestType", DbType.Int32, ManifestType);
                SFDatebase.AddInParameter(SFDbCommand, "pBranchID", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "pUser", DbType.String, User);
                SFDatebase.AddInParameter(SFDbCommand, "pFrom", DbType.DateTime, DteFrom);
                SFDatebase.AddOutParameter(SFDbCommand, "pIDInt",DbType.Int32,8);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();

                Int32 pID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@pIDInt"));
                return pID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
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
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspLockHotelTentativeManifestHeader");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.String, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.String, MaxRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.String, LoadType);

                SFDatebase.AddInParameter(SFDbCommand, "pManifestType", DbType.Int32, ManifestType);                
                SFDatebase.AddOutParameter(SFDbCommand, "pIDInt", DbType.Int32, 8);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();

                Int32 pID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@pIDInt"));
                return pID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Hotel manifest lock 
        /// </summary>
        public static void InsertHotelManifestLockDetails(Int32 ID, Int32 HotelID, Int32 IDBigInt, Int32 SeqNoInt, Int32 TravReq, Int32 ReqID,
                                                            Int32 VendorID, Int32 BranchID, Int32 RoomID, string ReservedName, DateTime StartDate,
                                                            DateTime EndDate, Int32 Duration, string HotelStatus, string User, string SfStatus,
                                                            Int32 PortID, Int32 CountryID, Int32 CityID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelManifestLockDetails");
                SFDatebase.AddInParameter(SFDbCommand, "pManifestID", DbType.Int32, ID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolTransHotelIDBigInt", DbType.Int32, HotelID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolIdBigint", DbType.Int32, IDBigInt);
                SFDatebase.AddInParameter(SFDbCommand, "pcolSeqNoInt", DbType.Int32, SeqNoInt);
                SFDatebase.AddInParameter(SFDbCommand, "pcolTravelReqIDInt", DbType.Int32, TravReq);
                SFDatebase.AddInParameter(SFDbCommand, "pcolRequestIDInt", DbType.Int32, ReqID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolVendorIDInt", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolBranchIDInt", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolRoomTypeIDInt", DbType.Int32, RoomID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolReserveUnderNameVarchar", DbType.String, ReservedName);

                SFDatebase.AddInParameter(SFDbCommand, "pcolTimeSpanStartDate", DbType.DateTime, StartDate);
                SFDatebase.AddInParameter(SFDbCommand, "pcolTimeSpanEndDate", DbType.DateTime, EndDate);
                SFDatebase.AddInParameter(SFDbCommand, "pcolTimeSpanDurationInt", DbType.Int32, Duration);
                SFDatebase.AddInParameter(SFDbCommand, "pcolHotelStatusVarchar", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "pcolCreatedByVarchar", DbType.String, User);
                SFDatebase.AddInParameter(SFDbCommand, "pcolSfStatus", DbType.String, SfStatus);

                SFDatebase.AddInParameter(SFDbCommand, "pcolPortID", DbType.Int32, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolCountryID", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolCity", DbType.Int32, CityID);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
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
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            int MaximumRows = 0;
            IDataReader dr = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelManifestDifferenceCount");
                db.AddInParameter(dbCommand, "@pManifestType", DbType.Int32, ManifestType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, ManifesDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pCompareManifestType", DbType.Int32, CompareManifestType);
                dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    MaximumRows = Convert.ToInt32(dr["maximumRows"]);
                }

                return MaximumRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
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
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable ManifestDataTable = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelManifestDifference");
                db.AddInParameter(dbCommand, "@pManifestType", DbType.Int32, ManifestType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, ManifesDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pCompareManifestType", DbType.Int32, CompareManifestType);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                ManifestDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return ManifestDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ManifestDataTable != null)
                {
                    ManifestDataTable.Dispose();
                }
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
            IDataReader dr = null;
            DbCommand comm = null;
            try
            {
                bool bIsExists = false;
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspSelectHotelManifestLockedExists");
                db.AddInParameter(comm, "@pDate", DbType.DateTime, DateTime.Parse(sDate));
                db.AddInParameter(comm, "@pHotelIDInt", DbType.Int64, GlobalCode.Field2Int(sBranchID));
                db.AddInParameter(comm, "@pManifestHrs", DbType.Int32, GlobalCode.Field2Int(sManifestHrs));
                dr = db.ExecuteReader(comm);
                if (dr.Read())
                {
                    bIsExists = (bool)dr["IsExists"];
                }
                return bIsExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }

        public static void HotelLastLockedDetails(string sDate, string sBranchID, Int16 iHours, ref string sLockedBy, ref string sLockedDate)
        {
            IDataReader dr = null;
            DbCommand comm = null;
            try
            {
                
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("upsSelectHotelManifestLastLockedRecord");
                db.AddInParameter(comm, "@pDate", DbType.DateTime, DateTime.Parse(sDate));
                db.AddInParameter(comm, "@pHotelIDInt", DbType.Int64, GlobalCode.Field2Int(sBranchID));
                db.AddInParameter(comm, "@pManifestHr", DbType.Int16, iHours);

                dr = db.ExecuteReader(comm);
                if (dr.Read())
                {
                    sLockedBy = dr["LockedBy"].ToString();
                    sLockedDate = dr["LockedDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }
    }
}

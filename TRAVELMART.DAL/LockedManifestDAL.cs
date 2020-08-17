using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class LockedManifestDAL
    {
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest values
        /// -----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  09/07/2012
        /// Description:    Add RegionList and PortList, and paramater iRegionID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List<LockedManifestGenericClass> LoadLockedManifestPage(string UserId, DateTime Date, int iRegionID)
        {
            DataTable Branch = null;
            DataTable ManifestType = null;
            DataTable Vessel = null;
            DataTable Nationality = null;
            DataTable Rank = null;
            DataTable Region = null;
            DataTable Port = null;

            List<LockedManifestGenericClass> listValues = new List<LockedManifestGenericClass>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadLockedManifestPage");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, iRegionID);
                ds = db.ExecuteDataSet(dbCommand);

                if (ds.Tables.Count > 0)
                {
                    Branch = ds.Tables[0];
                    ManifestType = ds.Tables[1];
                    Vessel = ds.Tables[2];
                    Nationality = ds.Tables[3];
                    Rank = ds.Tables[4];
                    Region = ds.Tables[5];
                    Port = ds.Tables[6];

                    listValues.Add(new LockedManifestGenericClass()
                    {
                        UserBranch = (from a in Branch.AsEnumerable()
                                      select new UserBranch
                                      {
                                          BranchID = a.Field<int?>("BranchID"),
                                          BranchName = a.Field<string>("BranchName"),
                                      }).ToList(),
                        ManifestType = (from a in ManifestType.AsEnumerable()
                                        select new ManifestClass
                                        {
                                            ManifesDesc = a.Field<string>("ManifesDesc"),
                                            ManifestHrs = a.Field<int?>("ManifestHrs"),
                                            ManifestName = a.Field<string>("ManifestName"),
                                            ManifestType = a.Field<int?>("ManifestType"),
                                        }).ToList(),
                        Vessel = (from a in Vessel.AsEnumerable()
                                  select new Vessel
                                  {
                                      VesselId = a.Field<int?>("VesselId"),
                                      VesselName = a.Field<string>("VesselName"),
                                      VesselCode = a.Field<string>("VesselCode"),
                                  }).ToList(),
                        SFNationality = (from a in Nationality.AsEnumerable()
                                         select new SFNationality
                                         {
                                             NationalityId = a.Field<int?>("NationalityId"),
                                             Nationality = a.Field<string>("Nationality"),
                                         }).ToList(),
                        SFRank = (from a in Rank.AsEnumerable()
                                  select new SFRank
                                  {
                                      RankId = a.Field<int?>("RankId"),
                                      RankName = a.Field<string>("RankName"),
                                  }).ToList(),
                        RegionList = (from a in Region.AsEnumerable()
                                      select new RegionList
                                      {
                                          RegionId = GlobalCode.Field2Int(a["RegionID"]),
                                          RegionName = a.Field<string>("RegionName")
                                      }).ToList(),
                        PortList = (from a in Port.AsEnumerable()
                                    select new PortList
                                    {
                                        PortId = GlobalCode.Field2Int(a["PortID"]),
                                        PortName = a.Field<string>("PortName")
                                    }).ToList()

                    });
                }

                return listValues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (Branch != null)
                {
                    Branch.Dispose();
                }
                if (Vessel != null)
                {
                    Vessel.Dispose();
                }
                if (ManifestType != null)
                {
                    ManifestType.Dispose();
                }
                if (Nationality != null)
                {
                    Nationality.Dispose();
                }
                if (Rank != null)
                {
                    Rank.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (Region != null)
                {
                    Region.Dispose();
                }
                if (Port != null)
                {
                    Port.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        //public List<LockedManifestListClass> LoadLockedManifest(string UserId, int LoadType, DateTime Date,
        //    int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
        //    string Gender,string Status, int startRowIndex, int maximumRows)
        //{
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;
        //    DataSet ds = null;
        //    DataTable Calendar = null;
        //    DataTable Manifest = null;
        //    int count = 0;
        //    List<LockedManifestListClass> ManifestList = new List<LockedManifestListClass>();

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadLockedManifest_old");
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
        //        db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
        //        db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, ManifestType);
        //        db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
        //        db.AddInParameter(dbCommand, "@pVesselId", DbType.Int32, VesselId);
        //        db.AddInParameter(dbCommand, "@pSfId", DbType.Int64, sfId);
        //        db.AddInParameter(dbCommand, "@pRankName", DbType.String, RankName);
        //        db.AddInParameter(dbCommand, "@pSfName", DbType.String, sfName);
        //        db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
        //        db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
        //        db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, Status);
        //        ds = db.ExecuteDataSet(dbCommand);
        //        if (ds.Tables.Count > 0)
        //        {
        //            Calendar = ds.Tables[0];
        //            Manifest = ds.Tables[2];
        //            count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
        //            ManifestList.Add(new LockedManifestListClass()
        //            {
        //                ManifestCalendar = (from a in Calendar.AsEnumerable()
        //                                    select new ManifestCalendar
        //                                    {
        //                                        colDate = a.Field<DateTime>("colDate"),
        //                                        TotalCount = a.Field<int>("TotalCount"),
        //                                    }).ToList(),
        //                LockedManifest = (from a in Manifest.AsEnumerable()
        //                                  select new LockedManifest
        //                                  { 
        //                                                RowNo = GlobalCode.Field2Int(a["RowRank"]),
        //                                                IdBigInt = a.Field<int?>("IdBigInt"),
        //                                                E1TravelReqId = a.Field<int?>("E1TravelReqId"),
        //                                                TravelReqId = a.Field<int?>("TravelReqId"),
        //                                                ManualReqId = a.Field<int?>("ManualReqId"),
        //                                                Couple = a.Field<string>("Couple"),
        //                                                Gender = a.Field<string>("Gender"),
        //                                                Nationality = a.Field<string>("Nationality"),
        //                                                CostCenter = a.Field<string>("CostCenter"),
        //                                                CheckIn = a.Field<DateTime?>("CheckedIn"),
        //                                                CheckOut = a.Field<DateTime?>("CheckedOut"),
        //                                                Name = a.Field<string>("Name"),
        //                                                EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
        //                                                ShipId = a.Field<int?>("ShipId"),
        //                                                Ship = a.Field<string>("Ship"),
        //                                                HotelRequest = a.Field<string>("HotelRequest"),
        //                                                SingleDouble = a.Field<string>("RoomType"),
        //                                                Title = a.Field<string>("Title"),
        //                                                HotelCity = a.Field<string>("HotelCity"),
        //                                                HotelNites = a.Field<int?>("HotelNites"),
        //                                                FromCity = a.Field<string>("FromCity"),
        //                                                ToCity = a.Field<string>("ToCity"),
        //                                                AL = a.Field<string>("Airline"),
        //                                                RecordLocator = a.Field<string>("RecordLocator"),
        //                                                PassportNo = a.Field<string>("PassportNo"),
        //                                                IssuedBy = a.Field<string>("IssuedBy"),
        //                                                PassportExpiration = a.Field<string>("PassportExpiration"),
        //                                                DeptDate = a.Field<DateTime?>("DeptDate"),
        //                                                ArvlDate = a.Field<DateTime?>("ArvlDate"),
        //                                                DeptCity = a.Field<string>("DeptCity"),
        //                                                ArvlCity = a.Field<string>("ArvlCity"),
        //                                                Carrier = a.Field<string>("Carrier"),
        //                                                FlightNo = a.Field<string>("FlightNo"),
        //                                                OnOffDate = a.Field<DateTime?>("OnOffDate"),
        //                                                Voucher = a.Field<string>("Voucher"),
        //                                                TravelDate = a.Field<DateTime?>("TravelDate"),
        //                                                Reason = a.Field<string>("Reason"),
        //                                                Status = a.Field<string>("Status"),
        //                                          }).ToList(),
        //                    LockedManifestCount = count
        //            });
        //        }
        //        return ManifestList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //        if (Manifest != null)
        //        {
        //            Manifest.Dispose();
        //        }
        //        if (Calendar != null)
        //        {
        //            Calendar.Dispose();
        //        }
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        //public List<LockedManifest> LoadLockedManifestPaging(string UserId, int LoadType, DateTime Date,
        //    int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
        //    string Gender, string Status, int startRowIndex, int maximumRows, out int count)
        //{
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;
        //    DataSet ds = null;
        //    DataTable Calendar = null;
        //    DataTable Manifest = null;
        //    count = 0;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadLockedManifest_old");
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
        //        db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
        //        db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, ManifestType);
        //        db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
        //        db.AddInParameter(dbCommand, "@pVesselId", DbType.Int32, VesselId);
        //        db.AddInParameter(dbCommand, "@pSfId", DbType.Int64, sfId);
        //        db.AddInParameter(dbCommand, "@pRankName", DbType.String, RankName);
        //        db.AddInParameter(dbCommand, "@pSfName", DbType.String, sfName);
        //        db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
        //        db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
        //        db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, Status);
        //        db.AddInParameter(dbCommand, "@pStartRownIndex", DbType.Int32, startRowIndex);
        //        db.AddInParameter(dbCommand, "@pMaximumRows", DbType.Int32, maximumRows);
        //        ds = db.ExecuteDataSet(dbCommand);
        //        if (ds.Tables.Count > 0)
        //        {
        //            //Calendar = ds.Tables[0];
        //            Manifest = ds.Tables[1];
        //            count = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

        //            var query = (from a in Manifest.AsEnumerable()
        //                         select new LockedManifest
        //                         {
        //                             RowNo = GlobalCode.Field2Int(a["RowRank"]),
        //                             IdBigInt = a.Field<int?>("IdBigInt"),
        //                             E1TravelReqId = a.Field<int?>("E1TravelReqId"),
        //                             TravelReqId = a.Field<int?>("TravelReqId"),
        //                             ManualReqId = a.Field<int?>("ManualReqId"),
        //                             Couple = a.Field<string>("Couple"),
        //                             Gender = a.Field<string>("Gender"),
        //                             Nationality = a.Field<string>("Nationality"),
        //                             CostCenter = a.Field<string>("CostCenter"),
        //                             CheckIn = a.Field<DateTime?>("CheckedIn"),
        //                             CheckOut = a.Field<DateTime?>("CheckedOut"),
        //                             Name = a.Field<string>("Name"),
        //                             EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
        //                             ShipId = a.Field<int?>("ShipId"),
        //                             Ship = a.Field<string>("Ship"),
        //                             HotelRequest = a.Field<string>("HotelRequest"),
        //                             SingleDouble = a.Field<string>("RoomType"),
        //                             Title = a.Field<string>("Title"),
        //                             HotelCity = a.Field<string>("HotelCity"),
        //                             HotelNites = a.Field<int?>("HotelNites"),
        //                             FromCity = a.Field<string>("FromCity"),
        //                             ToCity = a.Field<string>("ToCity"),
        //                             AL = a.Field<string>("Airline"),
        //                             RecordLocator = a.Field<string>("RecordLocator"),
        //                             PassportNo = a.Field<string>("PassportNo"),
        //                             IssuedBy = a.Field<string>("IssuedBy"),
        //                             PassportExpiration = a.Field<string>("PassportExpiration"),
        //                             DeptDate = a.Field<DateTime?>("DeptDate"),
        //                             ArvlDate = a.Field<DateTime?>("ArvlDate"),
        //                             DeptCity = a.Field<string>("DeptCity"),
        //                             ArvlCity = a.Field<string>("ArvlCity"),
        //                             Carrier = a.Field<string>("Carrier"),
        //                             FlightNo = a.Field<string>("FlightNo"),
        //                             OnOffDate = a.Field<DateTime?>("OnOffDate"),
        //                             Voucher = a.Field<string>("Voucher"),
        //                             TravelDate = a.Field<DateTime?>("TravelDate"),
        //                             Reason = a.Field<string>("Reason"),
        //                             Status = a.Field<string>("Status"),
        //                         }).ToList();
        //            return query;
        //        }
        //        else 
        //        { 
        //            return null;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //        if (Manifest != null)
        //        {
        //            Manifest.Dispose();
        //        }
        //        if (Calendar != null)
        //        {
        //            Calendar.Dispose();
        //        }
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   02/23/2012
        /// Description:    get manifest difference
        /// ==================================================        
        /// Modified By:    Josephine Gad
        /// Date Modified:  18/07/2012
        /// Description:    Add column QueryRemarksID and QueryRemarks
        /// ==================================================
        /// Modified By:    Jefferson Bermundo
        /// Date Modified:  10/29/2012
        /// Description:    Add Locked By and Locked Date for Comparison Manifest
        /// ==================================================
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="CompareType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<ManifestDifferenceGenericList> LoadManifestDifference(string UserId, int LoadType, DateTime Date,
           int ManifestType, int CompareType, int BranchId, ref string lockedBy, ref string lockedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable Manifest = null;
            int count = 0;
            List<ManifestDifferenceGenericList> ManifestList = new List<ManifestDifferenceGenericList>();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadManifestDifference");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, ManifestType);
                db.AddInParameter(dbCommand, "@pCompareType", DbType.Int32, CompareType);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    Manifest = ds.Tables[1];
                    count = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lockedBy = ds.Tables[2].Rows[0][0].ToString();
                        lockedDate = ds.Tables[2].Rows[0][1].ToString();
                    }
                    ManifestList.Add(new ManifestDifferenceGenericList()
                    {
                        LockedManifestDifferenceWithRowsCount = count,
                        LockedManifestDifferenceWithRows = (from a in Manifest.AsEnumerable()
                                                            select new LockedManifestDifferenceWithRows
                                                            {
                                                                RowNum = GlobalCode.Field2Int(a["RowRank"]),
                                                                ManifestType = a.Field<int?>("ManifestType"),
                                                                IdBigInt = a.Field<int?>("IdBigInt"),
                                                                E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                                                TravelReqId = a.Field<int?>("TravelReqId"),
                                                                ManualReqId = a.Field<int?>("ManualReqId"),
                                                                Couple = a.Field<string>("Couple"),
                                                                Gender = a.Field<string>("Gender"),
                                                                Nationality = a.Field<string>("Nationality"),
                                                                CostCenter = a.Field<string>("CostCenter"),
                                                                CheckedIn = a.Field<DateTime?>("CheckedIn"),
                                                                CheckedOut = a.Field<DateTime?>("CheckedOut"),
                                                                LastName = a.Field<string>("LastName"),
                                                                FirstName = a.Field<string>("FirstName"),

                                                                EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                                                Ship = a.Field<string>("Ship"),
                                                                HotelRequest = a.Field<string>("HotelRequest"),
                                                                RoomType = a.Field<string>("RoomType"),
                                                                Title = a.Field<string>("Title"),
                                                                HotelCity = a.Field<string>("HotelCity"),
                                                                HotelNights = a.Field<int?>("HotelNites"),
                                                                FromCity = a.Field<string>("FromCity"),
                                                                ToCity = a.Field<string>("ToCity"),
                                                                Airline = a.Field<string>("Airline"),
                                                                RecordLocator = a.Field<string>("RecordLocator"),
                                                                PassportNo = a.Field<string>("PassportNo"),
                                                                IssuedDate = a.Field<string>("IssuedDate"),
                                                                PassportExpiration = a.Field<DateTime?>("PassportExpiration"),
                                                                DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                ArvlDate = a.Field<DateTime?>("ArvlDate"),
                                                                DeptCity = a.Field<string>("DeptCity"),
                                                                ArvlCity = a.Field<string>("ArvlCity"),
                                                                Carrier = a.Field<string>("Carrier"),
                                                                FlightNo = a.Field<string>("FlightNo"),
                                                                OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                                                Voucher = a.Field<string>("Voucher"),
                                                                TravelDate = a.Field<DateTime?>("TravelDate"),
                                                                Reason = a.Field<string>("Reason"),
                                                                Status = a.Field<string>("Status"),
                                                                isDeleted = GlobalCode.Field2Bool(a["IsDeleted"].ToString()),
                                                                QueryRemarks = a.Field<string>("QueryRemarks"),
                                                                QueryRemarksID = GlobalCode.Field2Int(a["QueryRemarksID"].ToString())
                                                            }).ToList()

                    });
                }
                return ManifestList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (Manifest != null)
                {
                    Manifest.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  19/07/2012
        /// Description:    remove unnecessary columns in LockedManifestEmail; re-order columns
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  03/10/2012
        /// Description:    Add parameter RegionId
        /// -----------------------------------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="CompareType"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List<ManifestEmailList> LoadManifestEmailList(string UserId, int LoadType,
             int ManifestType, int BranchId, int CompareType, DateTime Date, int RegionId)
        {
            List<ManifestEmailList> emailList = new List<ManifestEmailList>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable locked = null;
            DataTable difference = null;
            DataTable email = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadManifestEmail");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pManifestType", DbType.Int32, ManifestType);
                db.AddInParameter(dbCommand, "@pCompareType", DbType.Int32, CompareType);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionId);
                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (LoadType == 0)
                {
                    locked = ds.Tables[0];
                    email = ds.Tables[1];
                    emailList.Add(new ManifestEmailList()
                    {
                        LockedManifestEmail = (from a in locked.AsEnumerable()
                                               select new LockedManifestEmail
                                               {
                                                   HotelCity = a.Field<string>("HotelCity"),
                                                   CheckIn = a.Field<string>("CheckedIn"),
                                                   CheckOut = a.Field<string>("CheckedOut"),
                                                   HotelNights = a.Field<int?>("HotelNites"),
                                                   ReasonCode = a.Field<string>("Reason"),
                                                   LastName = a.Field<string>("LastName"),
                                                   FirstName = a.Field<string>("FirstName"),

                                                   EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                                   Gender = a.Field<string>("Gender"),
                                                   SingleDouble = a.Field<string>("RoomType"),
                                                   Couple = a.Field<string>("Couple"),
                                                   Title = a.Field<string>("Title"),
                                                   Ship = a.Field<string>("Ship"),
                                                   CostCenter = a.Field<string>("CostCenter"),
                                                   Nationality = a.Field<string>("Nationality"),
                                                   HotelRequest = a.Field<string>("HotelRequest"),
                                                   RecordLocator = a.Field<string>("RecordLocator"),

                                                   DeptCity = a.Field<string>("DeptCity"),
                                                   ArvlCity = a.Field<string>("ArvlCity"),
                                                   DeptDate = a.Field<string>("DeptDate"),
                                                   ArvlDate = a.Field<string>("ArvlDate"),
                                                   DeptTime = a.Field<string>("DeptTime"),
                                                   ArvlTime = a.Field<string>("ArvlTime"),

                                                   Carrier = a.Field<string>("Carrier"),
                                                   FlightNo = a.Field<string>("FlightNo"),
                                                   Voucher = a.Field<string>("Voucher"),

                                                   PassportNo = a.Field<string>("PassportNo"),
                                                   IssuedDate = a.Field<string>("IssuedDate"),
                                                   PassportExpiration = a.Field<string>("PassportExpiration"),
                                                   HotelBranch = a.Field<string>("HotelBranch")

                                                   //FromCity = a.Field<string>("FromCity"),
                                                   //ToCity = a.Field<string>("ToCity"),
                                                   //AL = a.Field<string>("Airline"),
                                                   //OnOffDate = a.Field<string>("OnOffDate"),

                                                   //TravelDate = a.Field<string>("TravelDate"),
                                                   //Reason = a.Field<string>("Reason"),

                                               }).ToList(),
                        HotelEmail = (from a in email.AsEnumerable()
                                      select new HotelEmail
                                      {
                                          RoleName = a.Field<string>("RoleName"),
                                          EmailAddress = a.Field<string>("Email"),
                                      }).ToList()

                    });
                    LockedManifestClass.BookedInPrevDateCount = GlobalCode.Field2Double(ds.Tables[2].Rows[0][0].ToString());
                }
                else
                {
                    locked = ds.Tables[0];
                    difference = ds.Tables[1];
                    email = ds.Tables[2];
                    emailList.Add(new ManifestEmailList()
                    {
                        LockedManifestEmail = (from a in locked.AsEnumerable()
                                               select new LockedManifestEmail
                                               {
                                                   HotelCity = a.Field<string>("HotelCity"),
                                                   CheckIn = a.Field<string>("CheckedIn"),
                                                   CheckOut = a.Field<string>("CheckedOut"),
                                                   HotelNights = a.Field<int?>("HotelNites"),
                                                   ReasonCode = a.Field<string>("Reason"),
                                                   LastName = a.Field<string>("LastName"),
                                                   FirstName = a.Field<string>("FirstName"),

                                                   EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                                   Gender = a.Field<string>("Gender"),
                                                   SingleDouble = a.Field<string>("RoomType"),
                                                   Couple = a.Field<string>("Couple"),
                                                   Title = a.Field<string>("Title"),
                                                   Ship = a.Field<string>("Ship"),
                                                   CostCenter = a.Field<string>("CostCenter"),
                                                   Nationality = a.Field<string>("Nationality"),
                                                   HotelRequest = a.Field<string>("HotelRequest"),
                                                   RecordLocator = a.Field<string>("RecordLocator"),

                                                   DeptCity = a.Field<string>("DeptCity"),
                                                   ArvlCity = a.Field<string>("ArvlCity"),
                                                   DeptDate = a.Field<string>("DeptDate"),
                                                   ArvlDate = a.Field<string>("ArvlDate"),
                                                   DeptTime = a.Field<string>("DeptTime"),
                                                   ArvlTime = a.Field<string>("ArvlTime"),

                                                   Carrier = a.Field<string>("Carrier"),
                                                   FlightNo = a.Field<string>("FlightNo"),
                                                   Voucher = a.Field<string>("Voucher"),

                                                   PassportNo = a.Field<string>("PassportNo"),
                                                   IssuedDate = a.Field<string>("IssuedDate"),
                                                   PassportExpiration = a.Field<string>("PassportExpiration"),
                                                   HotelBranch = a.Field<string>("HotelBranch")

                                                   //FromCity = a.Field<string>("FromCity"),
                                                   //ToCity = a.Field<string>("ToCity"),
                                                   //AL = a.Field<string>("Airline"),
                                                   //OnOffDate = a.Field<string>("OnOffDate"),

                                                   //TravelDate = a.Field<string>("TravelDate"),
                                                   //Reason = a.Field<string>("Reason"),
                                               }).ToList(),
                        HotelEmail = (from a in email.AsEnumerable()
                                      select new HotelEmail
                                      {
                                          RoleName = a.Field<string>("RoleName"),
                                          EmailAddress = a.Field<string>("Email"),
                                      }).ToList(),
                        ManifestDifferenceEmail = (from a in difference.AsEnumerable()
                                                   select new ManifestDifferenceEmail
                                                   {
                                                       Remarks = a.Field<string>("QueryRemarks"),
                                                       HotelCity = a.Field<string>("HotelCity"),
                                                       CheckIn = a.Field<string>("CheckedIn"),
                                                       CheckOut = a.Field<string>("CheckedOut"),
                                                       HotelNights = a.Field<int?>("HotelNites"),
                                                       LastName = a.Field<string>("LastName"),
                                                       FirstName = a.Field<string>("FirstName"),

                                                       EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                                       Gender = a.Field<string>("Gender"),
                                                       SingleDouble = a.Field<string>("RoomType"),
                                                       Couple = a.Field<string>("Couple"),

                                                       Title = a.Field<string>("Title"),
                                                       Ship = a.Field<string>("Ship"),
                                                       CostCenter = a.Field<string>("CostCenter"),
                                                       Nationality = a.Field<string>("Nationality"),
                                                       HotelRequest = a.Field<string>("HotelRequest"),

                                                       RecordLocator = a.Field<string>("RecordLocator"),
                                                       DeptCity = a.Field<string>("DeptCity"),
                                                       ArvlCity = a.Field<string>("ArvlCity"),
                                                       DeptDate = a.Field<string>("DeptDate"),
                                                       ArvlDate = a.Field<string>("ArvlDate"),
                                                       DeptTime = a.Field<string>("DeptTime"),
                                                       ArvlTime = a.Field<string>("ArvlTime"),

                                                       Carrier = a.Field<string>("Carrier"),
                                                       FlightNo = a.Field<string>("FlightNo"),
                                                       Voucher = a.Field<string>("Voucher"),

                                                       PassportNo = a.Field<string>("PassportNo"),
                                                       IssuedDate = a.Field<string>("IssuedDate"),
                                                       PassportExpiration = a.Field<string>("PassportExpiration"),

                                                       HotelBranch = a.Field<string>("HotelBranch")
                                                    

                                                       //FromCity = a.Field<string>("FromCity"),
                                                       //ToCity = a.Field<string>("ToCity"),
                                                       //AL = a.Field<string>("Airline"),

                                                       //OnOffDate = a.Field<string>("OnOffDate"),
                                                       //TravelDate = a.Field<string>("TravelDate"),
                                                       //Reason = a.Field<string>("Reason"),
                                                       //QueryRemarksID = GlobalCode.Field2Int("QueryRemarksID"),

                                                   }).ToList()

                    });
                    LockedManifestClass.BookedInPrevDateCount = GlobalCode.Field2Double(ds.Tables[3].Rows[0][0].ToString());
                }

                return emailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (locked != null)
                {
                    locked.Dispose();
                }
                if (email != null)
                {
                    email.Dispose();
                }
                if (difference != null)
                {
                    difference.Dispose();
                }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable Calendar = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetLockedCalendar");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, ManifestType);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Date);
                Calendar = db.ExecuteDataSet(dbCommand).Tables[0];

                List<ManifestCalendar> list = new List<ManifestCalendar>();
                list = (from a in Calendar.AsEnumerable()
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
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }

                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        //public List<LockedManifestListClass> LoadLockedManifest2(string UserId, int LoadType, DateTime Date,
        //    int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
        //    string Gender, string Status, int startRowIndex, int maximumRows)
        //{
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;
        //    DataSet ds = null;
        //    DataTable Manifest = null;
        //    int count = 0;
        //    List<LockedManifestListClass> ManifestList = new List<LockedManifestListClass>();

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadLockedManifest");
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
        //        db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
        //        db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, ManifestType);
        //        db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
        //        db.AddInParameter(dbCommand, "@pVesselId", DbType.Int32, VesselId);
        //        db.AddInParameter(dbCommand, "@pSfId", DbType.Int64, sfId);
        //        db.AddInParameter(dbCommand, "@pRankName", DbType.String, RankName);
        //        db.AddInParameter(dbCommand, "@pSfName", DbType.String, sfName);
        //        db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
        //        db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
        //        db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, Status);
        //        ds = db.ExecuteDataSet(dbCommand);
        //        if (ds.Tables.Count > 0)
        //        {
        //            Manifest = ds.Tables[1];
        //            count = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
        //            ManifestList.Add(new LockedManifestListClass()
        //            {

        //                LockedManifest = (from a in Manifest.AsEnumerable()
        //                                  select new LockedManifest
        //                                  {
        //                                      RowNo = GlobalCode.Field2Int(a["RowRank"]),
        //                                      IdBigInt = GlobalCode.Field2Int(a["IdBigInt"]),
        //                                      E1TravelReqId = GlobalCode.Field2TinyInt("E1TravelReqId"),
        //                                      TravelReqId = GlobalCode.Field2Int("TravelReqId"),
        //                                      ManualReqId = GlobalCode.Field2Int("ManualReqId"),
        //                                      Couple = a.Field<string>("Couple"),
        //                                      Gender = a.Field<string>("Gender"),
        //                                      Nationality = a.Field<string>("Nationality"),
        //                                      CostCenter = a.Field<string>("CostCenter"),
        //                                      CheckIn = a.Field<DateTime?>("CheckedIn"),
        //                                      CheckOut = a.Field<DateTime?>("CheckedOut"),
        //                                      Name = a.Field<string>("Name"),
        //                                      EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
        //                                      ShipId = GlobalCode.Field2Int("ShipId"),
        //                                      Ship = a.Field<string>("Ship"),
        //                                      HotelRequest = a.Field<string>("HotelRequest"),
        //                                      SingleDouble = a.Field<string>("RoomType"),
        //                                      Title = a.Field<string>("Title"),
        //                                      HotelCity = a.Field<string>("HotelCity"),
        //                                      HotelNites = a.Field<int?>("HotelNites"),
        //                                      FromCity = a.Field<string>("FromCity"),
        //                                      ToCity = a.Field<string>("ToCity"),
        //                                      AL = a.Field<string>("Airline"),
        //                                      RecordLocator = a.Field<string>("RecordLocator"),
        //                                      PassportNo = a.Field<string>("PassportNo"),
        //                                      IssuedBy = a.Field<string>("IssuedBy"),
        //                                      PassportExpiration = a.Field<string>("PassportExpiration"),
        //                                      DeptDate = a.Field<DateTime?>("DeptDate"),
        //                                      ArvlDate = a.Field<DateTime?>("ArvlDate"),
        //                                      DeptCity = a.Field<string>("DeptCity"),
        //                                      ArvlCity = a.Field<string>("ArvlCity"),
        //                                      Carrier = a.Field<string>("Carrier"),
        //                                      FlightNo = a.Field<string>("FlightNo"),
        //                                      OnOffDate = a.Field<DateTime?>("OnOffDate"),
        //                                      Voucher = a.Field<string>("Voucher"),
        //                                      TravelDate = a.Field<DateTime?>("TravelDate"),
        //                                      Reason = a.Field<string>("Reason"),
        //                                      Status = a.Field<string>("Status"),                                              
        //                                  }).ToList(),
        //                LockedManifestCount = count
        //            });
        //        }
        //        return ManifestList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //        if (Manifest != null)
        //        {
        //            Manifest.Dispose();
        //        }
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest
        /// -----------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  03/10/2012
        /// Description:    Add parameter RegionId, add HotelBranch in select of LockedManifest
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoadType"></param>
        /// <param name="Date"></param>
        /// <param name="ManifestType"></param>
        /// <param name="BranchId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public List<LockedManifest> LoadLockedManifestPaging2(string UserId, int LoadType, DateTime Date,
            int ManifestType, int BranchId, int VesselId, string RankName, string sfName, string Nationality, Int64 sfId,
            string Gender, string Status, int RegionId, int startRowIndex, int maximumRows, out int count,
            out string lockedBy, out string lockedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable Calendar = null;
            DataTable Manifest = null;
            count = 0;
            lockedBy = "";
            lockedDate = "";
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadLockedManifest");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, ManifestType);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pVesselId", DbType.Int32, VesselId);
                db.AddInParameter(dbCommand, "@pSfId", DbType.Int64, sfId);
                db.AddInParameter(dbCommand, "@pRankName", DbType.String, RankName);
                db.AddInParameter(dbCommand, "@pSfName", DbType.String, sfName);
                db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
                db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, Status);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionId);

                db.AddInParameter(dbCommand, "@pStartRownIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pMaximumRows", DbType.Int32, maximumRows);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    //Calendar = ds.Tables[0];
                    Manifest = ds.Tables[1];
                    count = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    DataTable ManifestDetails = ds.Tables[2];
                    if (ManifestDetails.Rows.Count > 0)
                    {
                        lockedBy = ManifestDetails.Rows[0]["colCreatedByVarchar"].ToString();
                        lockedDate = ManifestDetails.Rows[0]["colDateCreatedDateTime"].ToString();
                    }
                    var query = (from a in Manifest.AsEnumerable()
                                 select new LockedManifest
                                 {
                                     RowNo = GlobalCode.Field2Int(a["RowRank"]),
                                     IdBigInt = GlobalCode.Field2Int(a["IdBigInt"]),
                                     E1TravelReqId = GlobalCode.Field2TinyInt(a["E1TravelReqId"]),
                                     TravelReqId = GlobalCode.Field2Int(a["TravelReqId"]),
                                     ManualReqId = GlobalCode.Field2Int(a["ManualReqId"]),
                                     Couple = a.Field<string>("Couple"),
                                     Gender = a.Field<string>("Gender"),
                                     Nationality = a.Field<string>("Nationality"),
                                     CostCenter = a.Field<string>("CostCenter"),
                                     CheckIn = a.Field<DateTime?>("CheckedIn"),
                                     CheckOut = a.Field<DateTime?>("CheckedOut"),
                                     LastName = a.Field<string>("LastName"),
                                     FirstName = a.Field<string>("FirstName"),

                                     EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                     ShipId = GlobalCode.Field2Int(a["ShipId"]),
                                     Ship = a.Field<string>("Ship"),
                                     HotelRequest = a.Field<string>("HotelRequest"),
                                     SingleDouble = a.Field<string>("RoomType"),
                                     Title = a.Field<string>("Title"),
                                     HotelCity = a.Field<string>("HotelCity"),
                                     HotelNights = GlobalCode.Field2TinyInt(a["HotelNites"]),
                                     FromCity = a.Field<string>("FromCity"),
                                     ToCity = a.Field<string>("ToCity"),
                                     AL = a.Field<string>("Airline"),
                                     RecordLocator = a.Field<string>("RecordLocator"),
                                     PassportNo = a.Field<string>("PassportNo"),
                                     IssuedDate = a.Field<string>("IssuedDate"),
                                     PassportExpiration = a.Field<string>("PassportExpiration"),
                                     DeptDate = a.Field<DateTime?>("DeptDate"),
                                     ArvlDate = a.Field<DateTime?>("ArvlDate"),
                                     DeptCity = a.Field<string>("DeptCity"),
                                     ArvlCity = a.Field<string>("ArvlCity"),
                                     Carrier = a.Field<string>("Carrier"),
                                     FlightNo = a.Field<string>("FlightNo"),
                                     OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                     Voucher = a.Field<string>("Voucher"),
                                     TravelDate = a.Field<DateTime?>("TravelDate"),
                                     Reason = a.Field<string>("Reason"),
                                     Status = a.Field<string>("Status"),
                                     BranchId = a.Field<int?>("BranchId"),
                                     HotelBranch = a.Field<string>("HotelBranch")
                                 }).ToList();
                    return query;
                }
                else
                {
                    return null;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (Manifest != null)
                {
                    Manifest.Dispose();
                }
                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   20/Dec/2012
        /// Description:    Load locked Manifest Summary
        /// ----------------------------------- 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="iLoadType"></param>
        /// <param name="sDateFrom"></param>
        /// <param name="sDateTo"></param>
        /// <param name="iManifestTypeID"></param>
        /// <param name="iRegion"></param>
        /// <param name="iPort"></param>
        /// <param name="iBranch"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        public static void LoadLockedManifestSummary(string UserID, Int16 iLoadType,
            string sDateFrom, string sDateTo, Int16 iManifestTypeID, 
            int iRegion, int iPort, int iBranch, int StartRow, int MaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable Calendar = null;
            DataTable Manifest = null;
            DataTable ManifestType = null;
            int iCount = 0;

            List<LockedManifestSummary> list = new List<LockedManifestSummary>();
            List<ManifestClass> listType = new List<ManifestClass>();

            HttpContext.Current.Session["LockedManifestSummary"] = list;
            HttpContext.Current.Session["LockedManifestSummaryCount"] = iCount;
            

            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadLockedManifestSummary");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.Date, GlobalCode.Field2DateTime(sDateFrom));
                if (sDateTo != "" && sDateTo != "MM/dd/yyyy")
                {
                    db.AddInParameter(dbCommand, "@pDateTo", DbType.Date, GlobalCode.Field2DateTime(sDateTo));
                }
                db.AddInParameter(dbCommand, "@pManifestTypeId", DbType.Int32, iManifestTypeID);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, iRegion);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, iPort);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, iBranch);

                db.AddInParameter(dbCommand, "@pStartRownIndex", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaximumRows", DbType.Int32, MaxRow);
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    //Calendar = ds.Tables[0];
                    Manifest = ds.Tables[1];
                    iCount = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                   
                    list = (from a in Manifest.AsEnumerable()
                                 select new LockedManifestSummary
                                 {
                                     ManifestIDBigint = GlobalCode.Field2Int(a["colManifestIDBigint"]),
                                     ManifestTypeIDTinyint = GlobalCode.Field2TinyInt(a["colManifestTypeIDTinyint"]),
                                     ManifestNameVarchar = GlobalCode.Field2String(a["colManifestNameVarchar"]),
                                     ManifestHrsTinyint = GlobalCode.Field2TinyInt(a["colManifestHrsTinyint"]),
                                     ManifestDescVarchar = GlobalCode.Field2String(a["colManifestDescVarchar"]),
                                     
                                     CreatedByVarchar = a.Field<string>("colCreatedByVarchar"),
                                     DateCreatedDateTime = a.Field<DateTime?>("colDateCreatedDateTime"),
                                     BranchName = a.Field<string>("colBranchName"),
                                     BranchID = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                     ManifestDate = a.Field<DateTime?>("colManifestDate"),
                                     
                                 }).ToList();
                    HttpContext.Current.Session["LockedManifestSummary"] = list;
                    HttpContext.Current.Session["LockedManifestSummaryCount"] = iCount; 
                }

                if (iLoadType == 0)
                {
                    ManifestType = ds.Tables[2];
                    listType = (from a in ManifestType.AsEnumerable()
                                select new ManifestClass {
                                    ManifestType = GlobalCode.Field2Int(a["colManifestTypeIDTinyint"]),
                                    ManifestName = GlobalCode.Field2String(a["ManifestType"]),

                                }).ToList();
                    HttpContext.Current.Session["ManifestClass"] = listType;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (Manifest != null)
                {
                    Manifest.Dispose();
                }
                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (ManifestType != null)
                {
                    ManifestType.Dispose();
                }
            }
        }
    }
}

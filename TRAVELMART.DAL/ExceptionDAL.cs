using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class ExceptionDAL
    {
        /// <summary>
        /// Date Created:   22/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Exception
        /// </summary>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>        
        /// <returns></returns>
        public static DataTable GetException(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectLogException");
                db.AddInParameter(command, "@pDateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@pDateTo", DbType.DateTime, DateTo);
                db.AddInParameter(command, "@pStartRow", DbType.Int16, StartRow);
                db.AddInParameter(command, "@pMaxRow", DbType.Int64, MaxRow);
                dt = db.ExecuteDataSet(command).Tables[0];
                
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
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   22/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Exception Total Row Count
        /// ------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static int GetExceptionCount(DateTime DateFrom, DateTime DateTo)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand command = null;
            IDataReader dr = null;
            try
            {
                command = db.GetStoredProcCommand("uspSelectLogExceptionCount");
                db.AddInParameter(command, "@pDateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@pDateTo", DbType.DateTime, DateTo);

                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    return int.Parse(dr["TotalRowCount"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 08/03/2012
        /// Description: load all queries for exception bookings for new UI
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  22/05/2012
        /// Description:    Add parameter RegionID
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  06/07/2012
        /// Description:    Add parameter PortID
        /// ---------------------------------------------------------------
        /// Modified By:    Jefferson Bermundo
        /// Date Modified:  16/07/2012
        /// Description:    Add Booking Remarks
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <returns></returns>
        public List<HotelTransactionExceptionGenericClass> LoadHotelExceptionPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<HotelTransactionExceptionGenericClass> overflow = new List<HotelTransactionExceptionGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            DataTable dtHotels = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelExceptionPage");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);                
                ds = db.ExecuteDataSet(dbCommand);
                dtHotels = ds.Tables[0];
                dtException = ds.Tables[1];

                overflow.Add(new HotelTransactionExceptionGenericClass()
                {
                    Hotels = (from a in dtHotels.AsEnumerable()
                                    select new Hotels 
                                    { 
                                        VendorId = a.Field<int>("VendorId"),
                                        BranchId = a.Field<int>("BranchId"),
                                        BranchName = a.Field<string>("BranchName"),
                                        CountryId = a.Field<int>("CountryId"),
                                        CityId = a.Field<int>("CityId"),
                                        isAccredited = a.Field<bool>("isAccredited"),                                        
                                        withEvent = a.Field<bool>("withEvent"),
                                        ContractId = a.Field<int>("ContractId"),
                                        withContract = a.Field<bool>("withContract"),
                                        colDate = a.Field<DateTime>("colDate"),
                                    }).ToList(),
                    ExceptionBooking = (from a in dtException.AsEnumerable()
                                        select new ExceptionBooking
                                        {
                                            ExceptionIdBigInt = GlobalCode.Field2Int(a["ExceptionIdBigInt"]),
                                            IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                            SeqNo = GlobalCode.Field2TinyInt(a["SeqNo"]),
                                            TravelReqId = a.Field<int?>("TravelReqId"),
                                            E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                            SeafarerId = a.Field<int?>("SeafarerId"),
                                            SeafarerName = a.Field<string>("SeafarerName"),
                                            PortId = a.Field<int?>("PortId"),
                                            PortName = a.Field<string>("PortName"),
                                            SFStatus = a.Field<string>("SFStatus"),
                                            OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                            ArrivalDepartureDatetime = a.Field<DateTime?>("ArrivalDepartureDatetime"),
                                            ArrivalDeparturetime = a.Field<string>("ArrivalDeparturetime"),
                                            Carrier = a.Field<string>("Carrier"),
                                            FlightNo = a.Field<string>("FlightNo"),
                                            FromCity = a.Field<string>("FromCity"),
                                            ToCity = a.Field<string>("ToCity"),
                                            RankName = a.Field<string>("RankName"),
                                            Stripes = a.Field<decimal?>("Stripes"),
                                            RecordLocator = a.Field<string>("RecordLocator"),
                                            Gender = a.Field<string>("Gender"),
                                            Nationality = a.Field<string>("Nationality"),
                                            RoomTypeId = a.Field<int?>("RoomTypeId"),
                                            RoomType = a.Field<string>("RoomType"),
                                            ReasonCode = a.Field<string>("ReasonCode"),
                                            ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                            Invalid = a.Field<bool?>("Invalid"),
                                            VesselName = a.Field<string>("VesselName"),
                                            BookingRemarks = a.Field<string>("BookingRemarks"),
                                            DateCreated = a.Field<DateTime?>("DateCreated"),
                                            HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                            IsByPort = GlobalCode.Field2String(a["IsPort"])

                                        }).ToList()
                });
                return overflow;
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
                if (dtException != null)
                {
                    dtException.Dispose();
                }
                if (dtHotels != null)
                {
                    dtHotels.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  Jan-06-2014
        /// Description:    Get top 50 exception list
        /// ---------------------------------------------------------------
        /// <returns></returns>
        public List<HotelTransactionExceptionGenericClass> LoadHotelExceptionPageTop(string UserId, int Loadtype, int RegionID, 
            int PortID, string sOrderBy)
        {
            List<HotelTransactionExceptionGenericClass> overflow = new List<HotelTransactionExceptionGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            DataTable dtHotels = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelExceptionPageTop");                
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, sOrderBy);
                dbCommand.CommandTimeout = 0;
                ds = db.ExecuteDataSet(dbCommand);
                dtHotels = ds.Tables[0];
                dtException = ds.Tables[1];

                overflow.Add(new HotelTransactionExceptionGenericClass()
                {
                    Hotels = (from a in dtHotels.AsEnumerable()
                              select new Hotels
                              {
                                  VendorId = a.Field<int>("VendorId"),
                                  BranchId = a.Field<int>("BranchId"),
                                  BranchName = a.Field<string>("BranchName"),
                                  CountryId = a.Field<int>("CountryId"),
                                  CityId = a.Field<int>("CityId"),
                                  isAccredited = a.Field<bool>("isAccredited"),
                                  withEvent = a.Field<bool>("withEvent"),
                                  ContractId = a.Field<int>("ContractId"),
                                  withContract = a.Field<bool>("withContract"),
                                  colDate = a.Field<DateTime>("colDate"),
                              }).ToList(),
                    ExceptionBooking = (from a in dtException.AsEnumerable()
                                        select new ExceptionBooking
                                        {
                                            ExceptionIdBigInt = GlobalCode.Field2Int(a["ExceptionIdBigInt"]),
                                            IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                            SeqNo = GlobalCode.Field2TinyInt(a["SeqNo"]),
                                            TravelReqId = a.Field<int?>("TravelReqId"),
                                            E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                            SeafarerId = a.Field<int?>("SeafarerId"),
                                            SeafarerName = a.Field<string>("SeafarerName"),
                                            PortId = a.Field<int?>("PortId"),
                                            PortName = a.Field<string>("PortName"),
                                            SFStatus = a.Field<string>("SFStatus"),
                                            OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                            ArrivalDepartureDatetime = a.Field<DateTime?>("ArrivalDepartureDatetime"),
                                            Carrier = a.Field<string>("Carrier"),
                                            FlightNo = a.Field<string>("FlightNo"),
                                            FromCity = a.Field<string>("FromCity"),
                                            ToCity = a.Field<string>("ToCity"),
                                            RankName = a.Field<string>("RankName"),
                                            Stripes = a.Field<decimal?>("Stripes"),
                                            RecordLocator = a.Field<string>("RecordLocator"),
                                            Gender = a.Field<string>("Gender"),
                                            Nationality = a.Field<string>("Nationality"),
                                            RoomTypeId = a.Field<int?>("RoomTypeId"),
                                            RoomType = a.Field<string>("RoomType"),
                                            ReasonCode = a.Field<string>("ReasonCode"),
                                            ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                            Invalid = a.Field<bool?>("Invalid"),
                                            VesselName = a.Field<string>("VesselName"),
                                            BookingRemarks = a.Field<string>("BookingRemarks"),

                                            HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                            IsByPort = GlobalCode.Field2String(a["IsPort"])

                                        }).ToList()
                });
                return overflow;
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
                if (dtException != null)
                {
                    dtException.Dispose();
                }
                if (dtHotels != null)
                {
                    dtHotels.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  Apr-02-2014
        /// Description:    Get exception list from 0 to 6 days
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Monteza
        /// Modified Date:  Apr-021-2016
        /// Description:    Added colNoOfDays_CrewAssist
        ///                 Added sRole and iLoadType parameters
        /// ---------------------------------------------------------------
        /// <returns></returns>
        public List<HotelTransactionExceptionGenericClass> LoadHotelExceptionPageDays(DateTime dDate, string UserId, int RegionID,
            int PortID, string sRole, Int16 iLoadType)
        {
            List<HotelTransactionExceptionGenericClass> overflow = new List<HotelTransactionExceptionGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            DataTable dtHotels = null;
            DataTable dtNoOfDays = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelExceptionPageDays");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, dDate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);

                ds = db.ExecuteDataSet(dbCommand);
                dtHotels = ds.Tables[0];
                dtException = ds.Tables[1];

                overflow.Add(new HotelTransactionExceptionGenericClass()
                {
                    Hotels = (from a in dtHotels.AsEnumerable()
                              select new Hotels
                              {
                                  VendorId = a.Field<int>("VendorId"),
                                  BranchId = a.Field<int>("BranchId"),
                                  BranchName = a.Field<string>("BranchName"),
                                  CountryId = a.Field<int>("CountryId"),
                                  CityId = a.Field<int>("CityId"),
                                  isAccredited = a.Field<bool>("isAccredited"),
                                  withEvent = a.Field<bool>("withEvent"),
                                  ContractId = a.Field<int>("ContractId"),
                                  withContract = a.Field<bool>("withContract"),
                                  colDate = a.Field<DateTime>("colDate"),
                              }).ToList(),
                    ExceptionBooking = (from a in dtException.AsEnumerable()
                                        select new ExceptionBooking
                                        {
                                            ExceptionIdBigInt = GlobalCode.Field2Int(a["ExceptionIdBigInt"]),
                                            IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                            SeqNo = GlobalCode.Field2TinyInt(a["SeqNo"]),
                                            TravelReqId = a.Field<int?>("TravelReqId"),
                                            E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                            SeafarerId = a.Field<int?>("SeafarerId"),
                                            SeafarerName = a.Field<string>("SeafarerName"),
                                            PortId = a.Field<int?>("PortId"),
                                            PortName = a.Field<string>("PortName"),
                                            SFStatus = a.Field<string>("SFStatus"),
                                            OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                            ArrivalDepartureDatetime = a.Field<DateTime?>("ArrivalDepartureDatetime"),
                                            ArrivalDeparturetime = a.Field<string>("ArrivalDeparturetime"),
                                            Carrier = a.Field<string>("Carrier"),
                                            FlightNo = a.Field<string>("FlightNo"),
                                            FromCity = a.Field<string>("FromCity"),
                                            ToCity = a.Field<string>("ToCity"),
                                            RankName = a.Field<string>("RankName"),
                                            Stripes = a.Field<decimal?>("Stripes"),
                                            RecordLocator = a.Field<string>("RecordLocator"),
                                            Gender = a.Field<string>("Gender"),
                                            Nationality = a.Field<string>("Nationality"),
                                            RoomTypeId = a.Field<int?>("RoomTypeId"),
                                            RoomType = a.Field<string>("RoomType"),
                                            ReasonCode = a.Field<string>("ReasonCode"),
                                            ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                            Invalid = a.Field<bool?>("Invalid"),
                                            VesselName = a.Field<string>("VesselName"),
                                            BookingRemarks = a.Field<string>("BookingRemarks"),
                                            DateCreated = a.Field<DateTime?>("DateCreated"),
                                            HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                            IsByPort = GlobalCode.Field2String(a["IsPort"])

                                        }).ToList()
                });


                if (iLoadType == 0)
                {
                    dtNoOfDays = ds.Tables[2];
                    TMSettings.NoOfDays = GlobalCode.Field2Int(dtNoOfDays.Rows[0][0].ToString());
                }
                
                return overflow;
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
                if (dtException != null)
                {
                    dtException.Dispose();
                }
                if (dtHotels != null)
                {
                    dtHotels.Dispose();
                }
                if (dtNoOfDays != null)
                {
                    dtNoOfDays.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  Jan-07-2014
        /// Description:    Get exception list to extract
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionExtract(string sUser)
        {
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetHotelExceptionPageTopExport");
                db.AddInParameter(comm, "@pUserId", DbType.String, sUser);
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
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Add or remove record from Exception List
        /// ---------------------------------------------------------------
        /// </summary>
        /// <param name="ExceptionIDInt"></param>
        /// <param name="IsRemovedBit"></param>
        /// <param name="UserId"></param>
        /// <param name="strLogDescription"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public static void ExceptionAddRemoveFromList(Int64 ExceptionIDInt, bool IsRemovedBit, string UserId,
            String strLogDescription, String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate,
            string sComment)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSaveExceptionAddRemoveFromList");

                db.AddInParameter(dbCommand, "@pHotelExceptionIdBigInt", DbType.Int64, ExceptionIDInt);
                db.AddInParameter(dbCommand, "@pIsRemovedBit", DbType.Boolean, IsRemovedBit);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Get records removed from Exception List
        /// ---------------------------------------------------------------        
        /// Modified By:    Josephine Gad
        /// Date Modified:  05/Mar/2013
        /// Description:    Add Comments field
        /// ---------------------------------------------------------------                
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="RegionID"></param>
        /// <param name="PortID"></param>
        /// <returns></returns>
        public static List<ExceptionBooking> ExceptionGetRemovedList(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<ExceptionBooking> ExceptionList = new List<ExceptionBooking>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            int iTotalRows = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelExceptionRemovedList");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                ds = db.ExecuteDataSet(dbCommand);
                dtException = ds.Tables[1];
                iTotalRows = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());
                ExceptionList = (from a in dtException.AsEnumerable()
                                    select new ExceptionBooking
                                    {
                                        ExceptionIdBigInt = GlobalCode.Field2Int(a["ExceptionIdBigInt"]),
                                        IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                        SeqNo = GlobalCode.Field2TinyInt(a["SeqNo"]),
                                        TravelReqId = a.Field<int?>("TravelReqId"),
                                        E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                        SeafarerId = a.Field<int?>("SeafarerId"),
                                        SeafarerName = a.Field<string>("SeafarerName"),
                                        PortId = a.Field<int?>("PortId"),
                                        PortName = a.Field<string>("PortName"),
                                        SFStatus = a.Field<string>("SFStatus"),
                                        OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                        ArrivalDepartureDatetime = a.Field<DateTime?>("ArrivalDepartureDatetime"),
                                        Carrier = a.Field<string>("Carrier"),
                                        FlightNo = a.Field<string>("FlightNo"),
                                        FromCity = a.Field<string>("FromCity"),
                                        ToCity = a.Field<string>("ToCity"),
                                        RankName = a.Field<string>("RankName"),
                                        Stripes = a.Field<decimal?>("Stripes"),
                                        RecordLocator = a.Field<string>("RecordLocator"),
                                        Gender = a.Field<string>("Gender"),
                                        Nationality = a.Field<string>("Nationality"),
                                        RoomTypeId = a.Field<int?>("RoomTypeId"),
                                        RoomType = a.Field<string>("RoomType"),
                                        ReasonCode = a.Field<string>("ReasonCode"),
                                        ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                        Invalid = a.Field<bool?>("Invalid"),
                                        VesselName = a.Field<string>("VesselName"),
                                        BookingRemarks = a.Field<string>("BookingRemarks"),
                                        Comments = a.Field<string>("Comments"),
                                        RemovedBy = a.Field<string>("RemovedBy")
                                    }).ToList();

                    HttpContext.Current.Session["HotelTransactionExceptionExceptionBookingRemovedCount"] = iTotalRows;
                    return ExceptionList;
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
                if (dtException != null)
                {
                    dtException.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   13/Jan/2014
        /// Description:    Get exception list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<ExceptionsByMonth> GetExceptionByMonth(int iYear, Int16 iMonth, int iRegion, int iPort, string sUser)
        {
            List<ExceptionsByMonth> list = new List<ExceptionsByMonth>();
            DbCommand com = null;
            DataTable dt = null;

            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("uspGetHotelExceptionDashboard");
                db.AddInParameter(com, "@pYear", DbType.Int32, iYear);
                db.AddInParameter(com, "@pMonth", DbType.Int16, iMonth);
                db.AddInParameter(com, "@pRegion", DbType.Int32, iRegion);
                db.AddInParameter(com, "@pPort", DbType.Int32, iPort);
                db.AddInParameter(com, "@pUser", DbType.String, sUser);
                dt = db.ExecuteDataSet(com).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new ExceptionsByMonth {
                            ExceptiopnID = GlobalCode.Field2Int(a["ExceptionID"]),
                            ExceptionRemarks = GlobalCode.Field2String(a["ExceptionRemarks"]),

                            January = GlobalCode.Field2Int(a["January"]),
                            February = GlobalCode.Field2Int(a["February"]),
                            March = GlobalCode.Field2Int(a["March"]),
                            April = GlobalCode.Field2Int(a["April"]),
                            May = GlobalCode.Field2Int(a["May"]),
                            June = GlobalCode.Field2Int(a["June"]),
                            July = GlobalCode.Field2Int(a["July"]),
                            August = GlobalCode.Field2Int(a["August"]),
                            September = GlobalCode.Field2Int(a["September"]),
                            October = GlobalCode.Field2Int(a["October"]),
                            November = GlobalCode.Field2Int(a["November"]),
                            December = GlobalCode.Field2Int(a["December"]),
                            Total = GlobalCode.Field2Int(a["Total"]),
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (com != null)
                {
                    com.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   13/Jan/2014
        /// Description:    Get exception list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptionByMonthExport (string sUser)
        {
            DataTable dt = null;
            DbCommand com = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("uspGetHotelExceptionDashboardExport");
                db.AddInParameter(com, "@pUser", DbType.String, sUser);
                dt = db.ExecuteDataSet(com).Tables[0];
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
                if (com != null)
                {
                    com.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   01/oct/2014
        /// Description:    Get Excption Data to load in the page
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<ExceptionPageData> GetExceptionPageData(short LoadType, string UserId
                ,DateTime Date, int RegionID, int PortID, int CountryID, string UserRole
            
            )
        {
            DataSet  ds = new DataSet();
            List<ExceptionPageData> ExceptionPageData = new List<ExceptionPageData>(); 
            DbCommand com = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("upsGetExceptionPageData");
                db.AddInParameter(com, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(com, "@pUserId", DbType.String, UserId);
                db.AddInParameter(com, "@pDate", DbType.DateTime, Date);
                db.AddInParameter(com, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(com, "@pPortIDInt", DbType.Int32, PortID );
                db.AddInParameter(com, "@pCountryIdInt", DbType.Int32, CountryID);
                db.AddInParameter(com, "@pUserRolevarchar", DbType.String, UserRole);


                ds = db.ExecuteDataSet(com);

                ExceptionPageData.Add(new ExceptionPageData { 
                    RegionList = (from a in ds.Tables[0].AsEnumerable()
                                  select new RegionList
                                 {
                                    RegionId = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                    RegionName = GlobalCode.Field2String(a["colRegionNameVarchar"])
                                }).ToList(),

                     PortList = (from a in ds.Tables[1].AsEnumerable()
                                 select new PortList
                                 {
                                    PortId = GlobalCode.Field2Int(a["PORTID"]),
                                    PortName = GlobalCode.Field2String(a["PORT"])
                                }).ToList(),

                    Hotels = (from a in ds.Tables[2].AsEnumerable()
                              select new Hotels
                              {

                                    VendorId  = GlobalCode.Field2Int(a["VendorId"]),
                                    BranchId  = GlobalCode.Field2Int(a["BranchId"]),
                                    BranchName = GlobalCode.Field2String(a["BranchName"]),
                                    CountryId  = GlobalCode.Field2Int(a["CountryId"]),
                                    CityId   = GlobalCode.Field2Int(a["CityId"]),
                                    isAccredited  = GlobalCode.Field2Bool(a["isAccredited"]),
                                    withEvent = GlobalCode.Field2Bool(a["withEvent"]),
                                    withContract = GlobalCode.Field2Bool(a["withContract"]),
                                    ContractId   = GlobalCode.Field2Int(a["ContractId"]),
                                    colDate   = GlobalCode.Field2DateTime(a["colDate"])
            
                              }).ToList(),
                    ExceptionBooking = (from a in ds.Tables[3].AsEnumerable()
                                        select new ExceptionBooking
                                        {


                                            ExceptionIdBigInt = GlobalCode.Field2Int(a["ExceptionIdBigInt"]),
                                            IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                            SeqNo = GlobalCode.Field2Int(a["SeqNo"]),

                                            TravelReqId = GlobalCode.Field2Int(a["TravelReqId"]),
                                            E1TravelReqId = GlobalCode.Field2Int(a["E1TravelReqId"]),
                                            SeafarerId = GlobalCode.Field2Int(a["SeafarerId"]),
                                            SeafarerName = GlobalCode.Field2String(a["SeafarerName"]),


                                            PortId = GlobalCode.Field2Int(a["PortId"]),
                                            PortName = GlobalCode.Field2String(a["PortName"]),
                                            VesselName = GlobalCode.Field2String(a["VesselName"]),

                                            SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                                            OnOffDate = GlobalCode.Field2DateTime(a["OnOffDate"]),



                                            ArrivalDepartureDatetime = GlobalCode.Field2DateTime(a["ArrivalDepartureDatetime"]),


                                            Carrier = GlobalCode.Field2String(a["Carrier"]),
                                            FlightNo = GlobalCode.Field2String(a["FlightNo"]),

                                            FromCity = GlobalCode.Field2String(a["FromCity"]),
                                            ToCity = GlobalCode.Field2String(a["ToCity"]),
                                            RankName = GlobalCode.Field2String(a["RankName"]),



                                            Stripes = GlobalCode.Field2Decimal(a["Stripes"]),
                                            RecordLocator = GlobalCode.Field2String(a["RecordLocator"]),
                                            Gender = GlobalCode.Field2String(a["Gender"]),
                                            Nationality = GlobalCode.Field2String(a["Nationality"]),
                                            RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                            RoomType = GlobalCode.Field2String(a["RoomType"]),

                                            ReasonCode = GlobalCode.Field2String(a["ReasonCode"]),
                                            ExceptionRemarks = GlobalCode.Field2String(a["ExceptionRemarks"]),
                                            Invalid = GlobalCode.Field2Bool(a["Invalid"]),


                                            BookingRemarks = GlobalCode.Field2String(a["BookingRemarks"]),

                                            HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                            IsByPort = GlobalCode.Field2String(a["IsPort"]),

                                            //Comments = GlobalCode.Field2String(a["VendorId"]),
                                            //RemovedBy = GlobalCode.Field2String(a["VendorId"]),
                                            //Birthday = GlobalCode.Field2DateTime(a["VendorId"]),

















                                        }).ToList()
                
                }); 


                return ExceptionPageData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                 
                if (com != null)
                {
                    com.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

    }
}

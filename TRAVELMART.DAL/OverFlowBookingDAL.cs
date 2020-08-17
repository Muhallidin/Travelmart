using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using TRAVELMART.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class OverFlowBookingDAL
    {
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/02/2012
        /// Description: load all queries for overflow bookings
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="FilterBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <returns></returns>
        public List<HotelOverflowGenericClass> LoadAllOverflowBooking(DateTime Date, string UserId, int Loadtype, int FilterBy,
            int startRowIndex, int MaximumRows)
        {
            List<HotelOverflowGenericClass> overflow = new List<HotelOverflowGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            int dtCount = 0;
            DataTable dt = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectOverFlowBookings");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, Loadtype);
                db.AddInParameter(dbCommand, "@pFilterBy", DbType.Int16, FilterBy);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
                ds = db.ExecuteDataSet(dbCommand);
                dtCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                dt = ds.Tables[1];

                overflow.Add(new HotelOverflowGenericClass()
                {
                    OverflowBookingCount = dtCount,
                    OverflowBooking = (from a in dt.AsEnumerable()
                                       select new ExceptionBooking
                                       {
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
                                           Invalid = a.Field<bool?>("Invalid"),
                                           ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

      /// <summary>
      /// Author: Charlene Remotigue
      /// Date Created: 13/02/2012
      /// Description: load overflowbooking list on paging
      /// </summary>
      /// <param name="Date"></param>
      /// <param name="UserId"></param>
      /// <param name="Loadtype"></param>
      /// <param name="FilterBy"></param>
      /// <param name="startRowIndex"></param>
      /// <param name="MaximumRows"></param>
      /// <param name="dtCount"></param>
      /// <returns></returns>
        public List<ExceptionBooking> LoadOverflowBookingTable(DateTime Date, string UserId, int Loadtype, int FilterBy,
            int startRowIndex, int MaximumRows, out int dtCount)
        {
           
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dt = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectOverFlowBookings");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, Loadtype);
                db.AddInParameter(dbCommand, "@pFilterBy", DbType.Int16, FilterBy);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
                ds = db.ExecuteDataSet(dbCommand);
                dtCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                dt = ds.Tables[1];

                var overflow = (from a in dt.AsEnumerable()
                                select new ExceptionBooking
                                {
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
                                    Invalid = a.Field<bool?>("Invalid"),
                                }).ToList();

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 08/03/2012
        /// Description: load all queries for overflow bookings for new UI
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  22/05/2012
        /// Description:    Add parameter RegionID
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  22/05/2012
        /// Description:    Add parameter PortID
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  26/05/2012
        /// Description:    Add BookingRemarks, IDBigint and SeqNo
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Monteza
        /// Date Modified:  18/Nov/2014
        /// Description:    Add parameters iBrandID, iVesselID, iRoomType
        /// ---------------------------------------------------------------
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <returns></returns>
        public List<HotelTransactionOverflowGenericClass> LoadHotelOverflowPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID,
            int iBrandID, int iVesselID, int iRoomType)
        {
            List<HotelTransactionOverflowGenericClass> overflow = new List<HotelTransactionOverflowGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            DataTable dtHotels = null;
            DataTable dtBrand = null;
            DataTable dtVessel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelOverflowPage");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                db.AddInParameter(dbCommand, "@pBrandID", DbType.Int32, iBrandID);
                db.AddInParameter(dbCommand, "@pVesseID", DbType.Int32, iVesselID);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, iRoomType);

                ds = db.ExecuteDataSet(dbCommand);
                dtHotels = ds.Tables[0];
                dtException = ds.Tables[1];

                dtBrand = ds.Tables[2];
                dtVessel = ds.Tables[3];

                overflow.Add(new HotelTransactionOverflowGenericClass()
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
                    OverflowBooking2 = (from a in dtException.AsEnumerable()
                                        select new OverflowBooking2
                                        {
                                            TravelReqId = a.Field<Int64>("TravelReqId"),
                                            E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                            SeafarerId = a.Field<Int64>("SeafarerId"),
                                            SeafarerName = a.Field<string>("SeafarerName"),
                                            PortId = a.Field<int?>("PortId"),
                                            PortName = a.Field<string>("PortName"),
                                            SFStatus = a.Field<string>("SFStatus"),
                                            OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                            CheckInDate = a.Field<DateTime?>("CheckInDate"),
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
                                            VesselName = a.Field<string>("VesselName"),
                                            HotelRequest = Convert.ToBoolean(a.Field<bool>("HotelRequest")),
                                            BookingRemarks = a.Field<string>("colRemarksVarchar"),

                                            IDBigint = GlobalCode.Field2Int(a["colIDBigint"]),
                                            SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                           
                                            HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                            IsByPort = GlobalCode.Field2String(a["IsByPort"]),
                                            HotelName = GlobalCode.Field2String(a["HotelName"]),
                                            HotelOverflowID = GlobalCode.Field2Long(a["HotelOverflowID"]),
                                            Datecreated = a.Field<DateTime?>("Datecreated"),

                                            CheckOutDate = a.Field<DateTime?>("CheckOutDate"),
                                            HotelNights = a.Field<int?>("HotelNight") 


                                        }).ToList()
                });


                List<BrandList> listBrand;
                List<VesselList> listVessel;

                listBrand = (from a in dtBrand.AsEnumerable()
                             select new BrandList
                             {
                                 BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                                 BrandName = a.Field<string>("BrandName")
                             }).ToList();

                listVessel = (from a in dtVessel.AsEnumerable()
                             select new VesselList
                             {
                                 VesselID = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                 VesselName = a.Field<string>("VesselName")
                             }).ToList();


                HttpContext.Current.Session["HotelDashboardDTO_BrandList"] = listBrand;
                HttpContext.Current.Session["HotelDashboardDTO_VesselList"] = listVessel;

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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtVessel != null)
                {
                    dtVessel.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Apr/2014
        /// Description:    Load details for Service Provider for 0 to 6 days
        /// ---------------------------------------------------------------      
        /// </summary>        
        public List<HotelTransactionOverflowGenericClass> LoadHotelOverflowPageDays(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<HotelTransactionOverflowGenericClass> overflow = new List<HotelTransactionOverflowGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            DataTable dtHotels = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelOverflowPageDays");
               // db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                ds = db.ExecuteDataSet(dbCommand);
                dtHotels = ds.Tables[0];
                dtException = ds.Tables[1];

                overflow.Add(new HotelTransactionOverflowGenericClass()
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
                    OverflowBooking2 = (from a in dtException.AsEnumerable()
                                        select new OverflowBooking2
                                        {
                                            TravelReqId = a.Field<int?>("TravelReqId"),
                                            E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                            SeafarerId = a.Field<int?>("SeafarerId"),
                                            SeafarerName = a.Field<string>("SeafarerName"),
                                            PortId = a.Field<int?>("PortId"),
                                            PortName = a.Field<string>("PortName"),
                                            SFStatus = a.Field<string>("SFStatus"),
                                            OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                            CheckInDate = a.Field<DateTime?>("CheckInDate"),
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
                                            VesselName = a.Field<string>("VesselName"),
                                            HotelRequest = Convert.ToBoolean(a.Field<bool>("HotelRequest")),
                                            BookingRemarks = a.Field<string>("colRemarksVarchar"),

                                            IDBigint = GlobalCode.Field2Int(a["colIDBigint"]),
                                            SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),

                                            HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                            IsByPort = GlobalCode.Field2String(a["IsByPort"]),
                                            HotelName = GlobalCode.Field2String(a["HotelName"]),
                                            HotelOverflowID = GlobalCode.Field2Long(a["HotelOverflowID"])
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Modified:  04/Apr/2014
        /// Description:    Get overflow list to extract
        /// </summary>
        /// <returns></returns>
        public DataTable GetOverflowExtract(string sUser)
        {
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                comm = db.GetStoredProcCommand("uspGetHotelOverflowPageDaysExport");
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
        /// Date Created:   06/Aug/2014
        /// Description:    Add or remove record from Overflow List
        /// ---------------------------------------------------------------      
        public static void OverflowAddRemoveFromList(DataTable dt,bool IsRemovedBit, string UserId,
            String strLogDescription, String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate,
            string sComment)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSaveOverflowAddRemoveFromList");
                
                db.AddInParameter(dbCommand, "@pIsRemovedBit", DbType.Boolean, IsRemovedBit);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   07/Aug/2014
        /// Description:    Get records removed from Overflow List
        /// ---------------------------------------------------------------        
        public static List<OverflowBooking2> OverflowGetRemovedList(DateTime Date, string UserId, int LoadType, int RegionID, int PortID)
        {
            List<OverflowBooking2> OverflowList = new List<OverflowBooking2>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dt = null;
            int iTotalRows = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelOverflowRemovedList");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[1];
                iTotalRows = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());
                OverflowList = (from a in dt.AsEnumerable()
                                select new OverflowBooking2
                                 {
                                     HotelOverflowID = GlobalCode.Field2Int(a["HotelOverflowIdInt"]),
                                     IDBigint = GlobalCode.Field2Int(a["IDBigint"]),
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
                                     //ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                     //Invalid = a.Field<bool?>("Invalid"),
                                     VesselName = a.Field<string>("VesselName"),
                                     BookingRemarks = a.Field<string>("BookingRemarks"),
                                     Comments = a.Field<string>("Comments"),
                                     RemovedBy = a.Field<string>("RemovedBy")
                                 }).ToList();

                HttpContext.Current.Session["HotelOverflowBookingsRemovedCount"] = iTotalRows;
                return OverflowList;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/Oct/2014
        /// Description:    Get overflow list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<OverflowByMonth> GetOverflowByMonthList(int iYear, Int16 iMonth, int iRegion, int iPort, string sUser, 
            bool IsPageChanged, int StartRow, int MaxRow)
        {
            List<OverflowByMonth> list = new List<OverflowByMonth>();
            DbCommand com = null;
            
            DataSet ds = null;
            DataTable dt = null;

            int iCount = 0;

            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                com = db.GetStoredProcCommand("uspGetHotelOverflowDashboard");
                db.AddInParameter(com, "@pYear", DbType.Int32, iYear);
                db.AddInParameter(com, "@pMonth", DbType.Int16, iMonth);
                db.AddInParameter(com, "@pRegion", DbType.Int32, iRegion);
                db.AddInParameter(com, "@pPort", DbType.Int32, iPort);
                db.AddInParameter(com, "@pUser", DbType.String, sUser);
                db.AddInParameter(com, "@pIsPageChange", DbType.Boolean, IsPageChanged);

                db.AddInParameter(com, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(com, "@pMaxRow", DbType.Int32, MaxRow);
                com.CommandTimeout = 0;

                ds = db.ExecuteDataSet(com);
                dt = ds.Tables[1];
                list = (from a in dt.AsEnumerable()
                        select new OverflowByMonth
                        {
                            HotelName = a.Field<string>("HotelName"),
                            PortCode = a.Field<string>("PortCode"),
                            HotelCity = a.Field<string>("HotelCity"),

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


                iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                HttpContext.Current.Session["HotelOverflowBookingsDashboard_Count"] = iCount;

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
                 if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   30/Oct/2014
        /// Description:    Get overflow list count by month
        /// ---------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public DataTable GetOverflowByMonthExport(string sUser)
        {
            DataTable dt = null;
            DbCommand com = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                com = db.GetStoredProcCommand("uspGetHotelOverflowDashboardExport");
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
    }
}

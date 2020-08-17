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

namespace TRAVELMART.DAL
{
    public class AutomaticBookingDAL
    {
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/01/2012
        /// description: get automatic booking count
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="userId"></param>
        /// <param name="sfName"></param>
        /// <param name="e1Id"></param>
        /// <param name="hotelName"></param>
        /// <returns></returns>
        public Int32 SelectAutomaticBookingCount(String UserId)
        {
            Int32 maximumCount = 0;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectAutomaticBookingCount");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    maximumCount = Int32.Parse(dr["maximumRows"].ToString());
                }
                return maximumCount;
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
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 12/01/2012
        /// description: get automatic bookings
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="userId"></param>
        /// <param name="sfName"></param>
        /// <param name="e1Id"></param>
        /// <param name="hotelName"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable SelectAutomaticBooking(String UserId, Int32 StartRowIndex,  Int32 maximumRows)
        {
            DataTable BookingDataTable = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectAutomaticBooking");
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                BookingDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return BookingDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (BookingDataTable != null)
                {
                    BookingDataTable.Dispose();
                }
                if (dbCommand != null)
                {
                    BookingDataTable.Dispose();
                }
            }
        }


        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 19/01/2012
        /// Description: temporarily save bookings
        /// </summary>
        /// <param name="colDate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static void SaveTempBookings(DateTime colDate,String UserId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectTravelRequest");
                db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, colDate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }

       /// <summary>
       /// Author: Charlene Remotigue
       /// Date Created: 21/01/2012
       /// description: Book pending and automatic bookings
       /// --------------------------------------------------
       /// MOdified By: Charlene Remotigue
       /// Date Modified: 30/01/2012
       /// Description: Changed Parameters
       /// </summary>
       /// <param name="TravelReq"></param>
       /// <param name="RecordLocator"></param>
       /// <param name="VendorId"></param>
       /// <param name="BranchId"></param>
       /// <param name="RoomType"></param>
       /// <param name="CheckInDate"></param>
       /// <param name="CheckOutDate"></param>
       /// <param name="SFStatus"></param>
       /// <param name="CityId"></param>
       /// <param name="CountryId"></param>
       /// <param name="VoucherAmount"></param>
       /// <param name="ContractId"></param>
       /// <param name="UserId"></param>
       /// <returns></returns>
        public static Boolean ApproveBookings(Int32 TravelReq, String RecordLocator, Int32 VendorId, Int32 BranchId, Int32 RoomType,
            DateTime CheckInDate, DateTime CheckOutDate, String SFStatus, Int32 CityId, Int32 CountryId, String VoucherAmount,
            Int32 ContractId, String UserId, String strFunction, String Filepath, DateTime GMTDate, DateTime DateNow)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspApproveSaveAutomaticBooking");
                db.AddInParameter(dbCommand, "@pTravelReq", DbType.Int32, TravelReq);
                db.AddInParameter(dbCommand, "@pRecordLocator", DbType.String, RecordLocator);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.DateTime2, CheckInDate);
                db.AddInParameter(dbCommand, "@pCheckOutDate", DbType.DateTime2, CheckOutDate);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, SFStatus);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pVoucherAmount", DbType.String, VoucherAmount);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddOutParameter(dbCommand, "@pOutput", DbType.Boolean, 100);
                db.AddInParameter(dbCommand, "@pstrFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, Filepath);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDate", DbType.DateTime, GMTDate);
                db.AddInParameter(dbCommand, "@pDateNow", DbType.DateTime, DateNow);
                db.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
                Boolean valid = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@pOutput"));
                return valid;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }

 
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/01/2012
        /// description: get automatic booking count
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="userId"></param>
        /// <param name="sfName"></param>
        /// <param name="e1Id"></param>
        /// <param name="hotelName"></param>
        /// <returns></returns>
        public Int32 SelectOverFlowBookingCount(String UserId, String SFStatus)
        {
            Int32 maximumCount = 0;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectAutomaticBookingoOverflowCount");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, SFStatus);
                dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    maximumCount = Int32.Parse(dr["maximumRows"].ToString());
                   
                }
                return maximumCount;
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
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 12/01/2012
        /// description: get automatic bookings
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="userId"></param>
        /// <param name="sfName"></param>
        /// <param name="e1Id"></param>
        /// <param name="hotelName"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable SelectOverFlowBooking(Int32 StartRowIndex, Int32 maximumRows, String UserId, String SFStatus)
        {
            DataTable BookingDataTable = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {

                dbCommand = db.GetStoredProcCommand("uspSelectAutomaticBookingOverflow");
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, SFStatus);
                BookingDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return BookingDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (BookingDataTable != null)
                {
                    BookingDataTable.Dispose();
                }
                if (dbCommand != null)
                {
                    BookingDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 19/01/2012
        /// Description: temporarily save bookings
        /// ---------------------------------------
        /// Author: Charlene Remotigue
        /// Date Modified: 30/01/2012
        /// Description: parameter changes and stored procedure changes
        /// </summary>
        /// <param name="AirportId"></param>
        /// <param name="RoomType"></param>
        /// <param name="colDate"></param>
        /// <param name="Branch"></param>
        /// <param name="UserId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static Boolean ApproveOverFlowBooking(Int32 BranchId, Int32 roomTypeId, Int32 travelReq, DateTime CheckIn,
            DateTime CheckOut, String Status, Int32 seafarerID, String RecordLocator, Decimal Stripe, String UserId,
            String strFunction, String FileName, DateTime GMTDate, DateTime Now)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspApproveOverFlowBookings");
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, roomTypeId);
                db.AddInParameter(dbCommand, "@pTravelReqId", DbType.Int32, travelReq);
                db.AddInParameter(dbCommand, "@pRecordLocator", DbType.String, RecordLocator);
                db.AddInParameter(dbCommand, "@pStripe", DbType.Decimal, Stripe);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.DateTime, CheckIn);
                db.AddInParameter(dbCommand, "@pCheckOutDate", DbType.DateTime, CheckOut);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, Status);
                db.AddOutParameter(dbCommand, "@pOutput", DbType.String, 100);
                db.AddInParameter(dbCommand, "@pSeafarerId", DbType.Int32, seafarerID);
                db.AddInParameter(dbCommand, "@pstrFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, FileName);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDate", DbType.DateTime, GMTDate);
                db.AddInParameter(dbCommand, "@pDateNow", DbType.DateTime, Now);
                db.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
                Boolean Invalid = Boolean.Parse(db.GetParameterValue(dbCommand, "@pOutput").ToString()); 
                return Invalid;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 19/01/2012
        /// Description: temporarily save bookings
        /// </summary>
        /// <param name="colDate"></param>
        /// <param name="Branch"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static void SaveTempAutomaticBookings(DateTime colDate,
            Int32 Branch, String UserId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectTravelRequestbyBranch");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, colDate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, Branch);
                db.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }

        ///// <summary>
        ///// Author: Charlene Remotigue
        ///// Date Created: 02/01/2012
        ///// Descrption: send all overflow queries to list
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="SFStatus"></param>
        ///// <param name="LoadType"></param>
        ///// <param name="StartRowIndex"></param>
        ///// <param name="MaximumRows"></param>
        ///// <returns></returns>
        //public List<HotelOverflowGenericClass> LoadAllOverflowTables(String UserId, DateTime StartDate, String SFStatus, Int16 LoadType,
        //    Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    List<HotelOverflowGenericClass> OverflowList = new List<HotelOverflowGenericClass>();
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    DataTable dtBranch = null;
        //    Int32 maxRows = 0;
        //    DataTable dtOverflow = null;
        //    DataSet ds = null;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspSelectOverflowPageQueries");
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, StartDate);
        //        db.AddInParameter(dbCommand, "@pSFtatus", DbType.String, SFStatus);
        //        db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
        //        db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
        //        db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
        //        ds = db.ExecuteDataSet(dbCommand);
        //        dtBranch = ds.Tables[0];
        //        maxRows = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
        //        dtOverflow = ds.Tables[2];

        //        OverflowList.Add(new HotelOverflowGenericClass()
        //        {
        //            UserBranchList = (from a in dtBranch.AsEnumerable()
        //                              select new UserBranchList
        //                              {
        //                                  BranchID = a.Field<int>("BranchID"),
        //                                  BranchName = a.Field<string>("BranchName"),
        //                              }).ToList(),                    
        //            OverflowBookingCount = maxRows,
        //            OverflowBooking = (from c in dtOverflow.AsEnumerable()
        //                               select new OverflowBooking
        //                               {
        //                                   TravelReqId =  GlobalCode.Field2Int(c["colTravelReqIdInt"]),
        //                                   E1TravelReqId =GlobalCode.Field2Int(c["colE1TravelReqIdInt"]),
        //                                   SeafarerId = GlobalCode.Field2Int(c["colSeafarerIdInt"]),
        //                                   SeafarerName = c.Field<string>("colNameVarchar"),
        //                                   PortId = GlobalCode.Field2Int(c["colPortIdInt"]),
        //                                   PortName = c.Field<string>("colPortNameVarchar"),
        //                                   SFStatus = c.Field<string>("colStatusVarchar"),
        //                                   OnOffDate = c.Field<DateTime?>("colOnOffDate"),
        //                                   DepartureDate = c.Field<DateTime?>("colDepartureDatetime"),
        //                                   ArrivalDate = c.Field<DateTime?>("colArrivalDatetime"),
        //                                   FromCity = c.Field<string>("colDepartureAirportVarchar"),
        //                                   ToCity = c.Field<string>("colArrivalAirportVarchar"),
        //                                   RankId = GlobalCode.Field2Int(c["colRankIdInt"]),
        //                                   RankName = c.Field<string>("colRankNameVarchar"),
        //                                   Stripes = GlobalCode.Field2Decimal(c["colStripesDecimal"]),
        //                                   RecordLocator = c.Field<string>("colRecordLocatorVarchar"),
        //                                   Gender = c.Field<string>("colGenderVarchar"),
        //                                   Nationality = c.Field<string>("colNationalityVarchar"),
        //                                   RoomTypeId = GlobalCode.Field2Int(c["colRoomTypeIdInt"]),
        //                                   RoomType = c.Field<string>("colRoomTypeVarchar"),
        //                                   CostCenter = c.Field<string>("colCostCenterVarchar"),
        //                                   ReasonCode = c.Field<string>("colReasonCodeVarchar"),
        //                                   Invalid = GlobalCode.Field2Bool(c["InValid"]),
        //                               }).ToList()
        //        });

        //        return OverflowList;
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
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //        if (dtBranch != null)
        //        {
        //            dtBranch.Dispose();
        //        }
        //        if (dtOverflow != null)
        //        {
        //            dtOverflow.Dispose();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Author: Charlene Remotigue
        ///// Date Created: 01/02/2012
        ///// Description: Hotel overflow paging
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="SFStatus"></param>
        ///// <param name="LoadType"></param>
        ///// <param name="StartRowIndex"></param>
        ///// <param name="MaximumRows"></param>
        ///// <param name="countRows"></param>
        ///// <returns></returns>
        //public List<OverflowBooking> LoadOverflowBookingTable(String UserId, DateTime StartDate, String SFStatus, Int16 LoadType,
        //    Int32 StartRowIndex, Int32 MaximumRows, out int countRows)
        //{
        //    //List<OverflowBooking> OverflowList = new List<OverflowBooking>();

        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    DataTable dtOverflow = null;
        //    DataSet ds = null;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspSelectOverflowPageQueries");
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, StartDate);
        //        db.AddInParameter(dbCommand, "@pSFtatus", DbType.String, SFStatus);
        //        db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
        //        db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
        //        db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
        //        ds = db.ExecuteDataSet(dbCommand);
        //        //dtBranch = ds.Tables[0];
        //        //dtEmail = ds.Tables[1];
        //        countRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
        //        dtOverflow = ds.Tables[1];

        //        var query = (from c in dtOverflow.AsEnumerable()
        //                       select new OverflowBooking
        //                       {
        //                           TravelReqId = GlobalCode.Field2Int(c["colTravelReqIdInt"]),
        //                           E1TravelReqId = GlobalCode.Field2Int(c["colE1TravelReqIdInt"]),
        //                           SeafarerId = GlobalCode.Field2Int(c["colSeafarerIdInt"]),
        //                           SeafarerName = c.Field<string>("colNameVarchar"),
        //                           PortId = GlobalCode.Field2Int(c["colPortIdInt"]),
        //                           PortName = c.Field<string>("colPortNameVarchar"),
        //                           SFStatus = c.Field<string>("colStatusVarchar"),
        //                           OnOffDate = GlobalCode.Field2DateTime(c["colOnOffDate"]),
        //                           DepartureDate = GlobalCode.Field2DateTime(c["colDepartureDatetime"]),
        //                           ArrivalDate = GlobalCode.Field2DateTime(c["colArrivalDatetime"]),
        //                           FromCity = c.Field<string>("colDepartureAirportVarchar"),
        //                           ToCity = c.Field<string>("colArrivalAirportVarchar"),
        //                           RankId = GlobalCode.Field2Int(c["colRankIdInt"]),
        //                           RankName = c.Field<string>("colRankNameVarchar"),
        //                           Stripes = GlobalCode.Field2Decimal(c["colStripesDecimal"]),
        //                           RecordLocator = c.Field<string>("colRecordLocatorVarchar"),
        //                           Gender = c.Field<string>("colGenderVarchar"),
        //                           Nationality = c.Field<string>("colNationalityVarchar"),
        //                           RoomTypeId = GlobalCode.Field2Int(c["colRoomTypeIdInt"]),
        //                           RoomType = c.Field<string>("colRoomTypeVarchar"),
        //                           CostCenter = c.Field<string>("colCostCenterVarchar"),
        //                           ReasonCode = c.Field<string>("colReasonCodeVarchar"),
        //                           Invalid = GlobalCode.Field2Bool(c["InValid"]),
        //                       }).ToList();

        //        return query;
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
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //        if (dtOverflow != null)
        //        {
        //            dtOverflow.Dispose();
        //        }
        //    }
        //}




    }
}

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
    public class TravelRequestDAL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/02/2012
        /// Descrption:     Get Tables for No Travel Request List
        /// -------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  16/05/2012
        /// Descrption:     Remove Origin
        /// ==========================================================
        /// Date Modified:  14/May/2013
        /// Modified By:    Marco Abejar
        /// (description)  Add birthday field
        /// ==========================================================
        /// </summary>        
        /// <returns></returns>
        public List<NoTravelRequest> GetNoTravelRequestList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            List<NoTravelRequest> list = new List<NoTravelRequest>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectNoTravelRequest");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, FilterByName);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, Gender);
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, RankID);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                NoTravelRequest.NoTravelRequestCount = maxRows;

                list = (from a in dtManifest.AsEnumerable()
                        select new NoTravelRequest
                        {
                            TravelRequestID = GlobalCode.Field2Int(a["TravelRequestID"]),
                            SfID = GlobalCode.Field2Int(a["SfID"]),
                            Name = a.Field<string>("Name"),
                            DateOnOff = a.Field<DateTime?>("DateOnOff"),
                            Status = a.Field<string>("Status"),

                            Brand = a.Field<string>("Brand"),
                            Vessel = a.Field<string>("Vessel"),
                            VesselCode = a.Field<string>("VesselCode"),

                            PortCode = a.Field<string>("PortCode"),
                            Port = a.Field<string>("Port"),

                            RankCode = a.Field<string>("RankCode"),
                            Rank = a.Field<string>("Rank"),

                            Nationality = a.Field<string>("Rank"),
                            ReasonCode = a.Field<string>("ReasonCode"),
                            IsWithSail = GlobalCode.Field2Bool(a["IsWithSail"]),
                            NationalityName = a.Field<string>("NationalityName"),
                            Birthday = a.Field<DateTime?>("Birthday"),

                            //Origin = a.Field<string>("colOriginVarchar")
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/03/2012
        /// Description: get no travel request list for export
        /// -------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  23/05/2012
        /// Description:    Add parameter RegionID
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetNoTravelList(DateTime Date, string UserName, int RegionID)
        {
            DataTable dt = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("spGetNoTravelReqExport");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, UserName);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
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
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/03/2012
        /// Descrption:     Get Tables for Travel Request List with ON/OFF date same with Arrival/Departure Date
        /// ---------------------------------------------------
        /// Modified By:    Jefferson Bermundo
        /// Date Created:   16/07/2012
        /// Description:    Add Hotel Request
        /// ---------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   19/07/2012
        /// Description:    Add Booking Remarks column
        /// -----------------------------------------------------
        /// Date Modified:  14/May/2013
        /// Modified By:    Marco Abejar
        /// (description)  Add birthday field
        /// ==========================================================
        /// </summary>        
        /// <returns></returns>
        public List<TravelRequestArrivalDepartureSameDate> GetTravelRequestArrivalDepartureSameDateList
            (Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            List<TravelRequestArrivalDepartureSameDate> list = new List<TravelRequestArrivalDepartureSameDate>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectArrivalDepartureSameDate");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, FilterByName);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, Gender);
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, RankID);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                TravelRequestArrivalDepartureSameDate.ArrivalDepartureSameDateCount = maxRows;

                list = (from a in dtManifest.AsEnumerable()
                        select new TravelRequestArrivalDepartureSameDate
                        {
                            TravelRequestID = GlobalCode.Field2Int(a["TravelRequestID"]),
                            E1TravelRequest = GlobalCode.Field2Int(a["E1TravelRequest"]),
                            SfID = GlobalCode.Field2Int(a["SfID"]),
                            Name = a.Field<string>("Name"),
                            DateOnOff = a.Field<DateTime?>("DateOnOff"),
                            Status = a.Field<string>("Status"),

                            Brand = a.Field<string>("Brand"),
                            Vessel = a.Field<string>("Vessel"),
                            VesselCode = a.Field<string>("VesselCode"),

                            PortCode = a.Field<string>("PortCode"),
                            Port = a.Field<string>("Port"),

                            RankCode = a.Field<string>("RankCode"),
                            Rank = a.Field<string>("Rank"),

                            Nationality = a.Field<string>("Rank"),
                            ReasonCode = a.Field<string>("ReasonCode"),
                            IsWithSail = GlobalCode.Field2Bool(a["IsWithSail"]),
                            NationalityName = a.Field<string>("NationalityName"),

                            //AirArrDepDateTime = a.Field<DateTime?>("ArrivalDepartureDate"),
                            //AirOrigin = a.Field<string>("colAirOriginVarchar"),
                            //AirDestination = a.Field<string>("colAirDestinationVarchar"),

                            DepartureDateTime = a.Field<DateTime?>("DepartureDateTime"),
                            ArrivalDateTime = a.Field<DateTime?>("ArrivalDateTime"),
                            Airline = a.Field<string>("Airline"),
                            FlightNo = a.Field<string>("FlightNo"),
                            OriginAirport = a.Field<string>("OriginAirport"),
                            DestinationAirport = a.Field<string>("DestinationAirport"),

                            BookingRemarks = a.Field<string>("colRemarksVarchar"),
                            PassportNo = a.Field<string>("Passport"),
                            PasportDateIssued = a.Field<string>("PassportIssued"),
                            PassportExpiration = a.Field<string>("PassportExpiration"),
                            Lastname = a.Field<string>("Lastname"),
                            Firstname = a.Field<string>("Firstname"),
                            Birthday = a.Field<DateTime?>("Birthday"),
                            //HotelRequest = Convert.ToBoolean(a.Field<Int32>("HotelRequest"))
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/03/2012
        /// Descrption:     Get Tables for Travel Request List with ON/OFF date same with Arrival/Departure Date for Export use        
        /// </summary>        
        /// <returns></returns>
        public DataTable GetTravelRequestArrivalDepartureSameDateListExport
            (Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectArrivalDepartureSameDateExport");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, FilterByName);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, Gender);
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, RankID);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[0];
                return dtManifest;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Insert Travel Request Remarks
        /// --------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  18/Mar/2014
        /// Descrption:     Add parameter sRole and idBigint
        /// </summary>
        /// <param name="sTravelRequestID"></param>
        /// <param name="sRemarks"></param>
        /// <param name="sCreatedBy"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        /// <returns></returns>
        public Int32 InsertTravelRequestRemarks(string sRole, string sTravelRequestID, string sRemarks, string sCreatedBy,
           String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate, Int64 idBigint)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertTravelRequestRemarks");

                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, Int32.Parse(sTravelRequestID));
                db.AddInParameter(dbCommand, "@pRamarksVarchar", DbType.String, sRemarks);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, sCreatedBy);
                db.AddOutParameter(dbCommand, "@pRemarkIDBigInt", DbType.Int32, 8);

                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);
                db.AddInParameter(dbCommand, "@pIdBigint", DbType.Int64, idBigint);


                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 pRemarkIDBigInt = GlobalCode.Field2Int(db.GetParameterValue(dbCommand, "@pRemarkIDBigInt"));
                return pRemarkIDBigInt;
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
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Update Travel Request Remarks
        /// </summary>
        /// <param name="iRemarksID"></param>
        /// <param name="sTravelRequestID"></param>
        /// <param name="sRemarks"></param>
        /// <param name="sModifiedBy"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public void UpdateTravelRequestRemarks(int iRemarksID, string sTravelRequestID, string sRemarks, string sModifiedBy,
           String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateTravelRequestRemarks");

                db.AddInParameter(dbCommand, "@pRemarkIDBigInt", DbType.Int32, iRemarksID);
                db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, Int32.Parse(sTravelRequestID));
                db.AddInParameter(dbCommand, "@pRamarksVarchar", DbType.String, sRemarks);
                db.AddInParameter(dbCommand, "@pModifiedByVarchar", DbType.String, sModifiedBy);
                //db.AddOutParameter(dbCommand, "@pRemarkIDBigInt", DbType.Int32, 8);

                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);

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
            }
        }
        // <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Delete Travel Request Remarks        
        /// </summary>
        /// <param name="iDeleteType"></param>
        /// <param name="iRemarksID"></param>
        /// <param name="sTravelRequestID"></param>
        /// <param name="sRemarks"></param>
        /// <param name="sModifiedBy"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public void DeleteTravelRequestRemarks(Int16 iDeleteType, int iRemarksID, string sTravelRequestID, string sModifiedBy,
           String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteTravelRequestRemarks");

                db.AddInParameter(dbCommand, "@pDelTypeTinyInt", DbType.Int16, iDeleteType);
                db.AddInParameter(dbCommand, "@pRemarkIDBigInt", DbType.Int32, iRemarksID);
                db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, Int32.Parse(sTravelRequestID));
                db.AddInParameter(dbCommand, "@pModifiedByVarchar", DbType.String, sModifiedBy);

                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);

                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);

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
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Get Travel Request Remarks 
        /// </summary>
        /// <param name="LoadType"></param>
        /// <param name="iTravelRequestID"></param>
        /// <param name="sUser"></param>
        /// <returns></returns>
        public List<TravelRequestRemarks> GetTravelRequestRemarks(Int16 LoadType, int iRemarksID, int iTravelRequestID, string sUser)
        {
            List<TravelRequestRemarks> list = new List<TravelRequestRemarks>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectTravelRequestRemarks");
                db.AddInParameter(dbCommand, "@pLoadTypeTinyInt", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pRemarkIDBigInt", DbType.Int32, iRemarksID);
                db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, iTravelRequestID);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, sUser);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new TravelRequestRemarks
                        {
                            RemarkIDBigInt = GlobalCode.Field2Int(a["colRemarkIDBigInt"]),
                            TravelReqIdInt = GlobalCode.Field2Int(a["colTravelReqIdInt"]),
                            Remarks = a.Field<string>("colRamarksVarchar"),
                            CreatedBy = a.Field<string>("colCreatedByVarchar"),
                            CreatedDate = a.Field<DateTime?>("colDateCreatedDateTime"),
                            ModifiedBy = a.Field<string>("colModifiedByVarchar"),
                            ModifiedDate = a.Field<DateTime?>("colDateModifiedDateTime"),
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/08/2012
        /// Description:     Get Travel Request of Meet & Greet Role
        /// ========================================================
        /// Author:         Marco Abejar
        /// Date Created:   22/03/2013
        /// Description:    Add sorting
        /// ========================================================
        /// Date Modified:  29/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change Arrival Deaprture Datetime to seperate data and time in string format
        ///                 Change Birthday, DateOnOff, SequenceNo to string    
        /// ===============================================================
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sRole"></param>
        /// <param name="sPortID"></param>
        /// <param name="sAirportID"></param>
        /// <param name="dDate"></param>
        /// <param name="iStartRow"></param>
        /// <param name="iMaxRow"></param>
        /// <returns></returns>
        public List<MeetGreetTravelRequestGenericClass> GetMeetGreetTravelRequest(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
            DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iViewType, string SortBy)
        {
            List<MeetGreetTravelRequestGenericClass> list = new List<MeetGreetTravelRequestGenericClass>();
            List<MeetGreetTravelRequest> recordList = new List<MeetGreetTravelRequest>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dt = null;
            DataTable dtVessel = null;
            DataTable dtNationality = null;
            DataTable dtGender = null;
            DataTable dtRank = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectMeetGreetPage");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRoleVarchar", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(sPortID));
                db.AddInParameter(dbCommand, "@pAirportIDInt", DbType.Int32, GlobalCode.Field2Int(sAirportID));

                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, dDate);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, GlobalCode.Field2Int(VesselID));
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, GlobalCode.Field2TinyInt(FilterByName));
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, GlobalCode.Field2Int(NationalityID));
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, GlobalCode.Field2Int(Gender));
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, GlobalCode.Field2Int(RankID));
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pViewType", DbType.Int16, iViewType);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, SortBy);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[1];

                //if (dt.Rows.Count > 0)
                //{
                //    DataView dv = dt.DefaultView;
                //    dv.Sort = SortBy;
                //    dt = dv.ToTable();
                //}

                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                recordList = (from a in dt.AsEnumerable()
                              select new MeetGreetTravelRequest
                {
                    IDBigInt = GlobalCode.Field2Int(a["colIdBigint"]),
                    RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                    Brand = a.Field<string>("Brand"),
                    Vessel = a.Field<string>("Vessel"),
                    Port = a.Field<string>("Port"),
                    Rank = a.Field<string>("Rank"),

                    TravelRequestID = GlobalCode.Field2Int(a["colTravelReqIdInt"]),
                    SfID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                    E1TrID = GlobalCode.Field2Int(a["colE1TravelReqIdInt"]),

                    Name = a.Field<string>("SeafarerName"),
                    Status = a.Field<string>("colStatusVarchar"),
                    DateOnOff = a.Field<string>("SignOnOffDate"),

                    Hotel = a.Field<string>("Hotel"),
                    Checkin = a.Field<DateTime?>("CheckIn"),
                    Duration = GlobalCode.Field2Int(a["Duration"]),

                    Departure = a.Field<string>("AirportDeparture"),
                    Arrival = a.Field<string>("AirportArrival"),

                    //DepartureDateTime = GlobalCode.Field2DateTime(a["colDepartureDateTime"]),
                    //ArrivalDateTime = GlobalCode.Field2DateTime(a["colArrivalDateTime"]),

                    DepartureDate = a.Field<string>("colDepartureDate"),
                    ArrivalDate = a.Field<string>("colArrivalDate"),

                    DepartureTime = a.Field<string>("colDepartureTime"),
                    ArrivalTime = a.Field<string>("colArrivalTime"),

                    Airline = a.Field<string>("Airline"),
                    FlightNo = a.Field<string>("colFlightNoVarchar"),

                    AirportID = GlobalCode.Field2Int(a["colAirportIDInt"]),
                    SeaportID = GlobalCode.Field2Int(a["colPortIdInt"]),

                    PassportNo = a.Field<string>("PassportNo"),
                    PassportIssueDate = a.Field<string>("PassportIssueDate"),
                    PassportExpiryDate = a.Field<string>("PassportExpiryDate"),
                    IsTaggedByUser = GlobalCode.Field2Bool(a["IsTaggedByUser"]),

                    Checkout = a.Field<DateTime?>("Checkout"),
                    SingleDouble = a.Field<string>("colSingleDoubleFloat"),
                    Gender = a.Field<string>("colGender"),
                    CostCenter = a.Field<string>("colCostCenter"),
                    Nationality = a.Field<string>("colNationality"),
                    MealAllowance = a.Field<string>("colMealAllowance"),
                    Lastname = a.Field<string>("colLastname"),
                    Firstname = a.Field<string>("colFirstname"),
                    TagDateTime = a.Field<string>("colTagDateTime"),
                    Birthday = a.Field<string>("colBirthday"),

                    SequenceNo = GlobalCode.Field2String(a["colSeqNoInt"]),
                    IsFirstPartition = GlobalCode.Field2Bool(a["IsFirstPartition"]),
                }).ToList();

                if (LoadType == 0)
                {
                    dtVessel = ds.Tables[2];
                    dtNationality = ds.Tables[3];
                    dtGender = ds.Tables[4];
                    dtRank = ds.Tables[5];

                    list.Add(new MeetGreetTravelRequestGenericClass()
                    {
                        MeetGreetTravelRequestCount = maxRows,
                        MeetGreetTravelRequestList = recordList,

                        VesselList = (from a in dtVessel.AsEnumerable()
                                      select new VesselDTO
                                      {
                                          VesselIDString = GlobalCode.Field2String(a["VesselID"]),
                                          VesselNameString = GlobalCode.Field2String(a["Vessel"])
                                      }).ToList(),

                        NationalityList = (from a in dtNationality.AsEnumerable()
                                           select new NationalityList
                                           {
                                               NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                                               Nationality = GlobalCode.Field2String(a["Nationality"])
                                           }).ToList(),

                        GenderList = (from a in dtGender.AsEnumerable()
                                      select new GenderList
                                       {
                                           GenderID = GlobalCode.Field2Int(a["GenderID"]),
                                           Gender = GlobalCode.Field2String(a["Gender"])
                                       }).ToList(),

                        RankList = (from a in dtRank.AsEnumerable()
                                    select new RankList
                                     {
                                         RankID = GlobalCode.Field2Int(a["RankID"]),
                                         Rank = GlobalCode.Field2String(a["Rank"])
                                     }).ToList(),

                        CountTagged = Int32.Parse(ds.Tables[6].Rows[0][0].ToString()),
                        CountUntagged = Int32.Parse(ds.Tables[7].Rows[0][0].ToString()),
                        CountAll = Int32.Parse(ds.Tables[8].Rows[0][0].ToString()),
                    });
                }
                else if (LoadType == 2)
                {
                    list.Add(new MeetGreetTravelRequestGenericClass()
                    {
                        MeetGreetTravelRequestCount = maxRows,
                        MeetGreetTravelRequestList = recordList,
                        CountTagged = Int32.Parse(ds.Tables[2].Rows[0][0].ToString()),
                        CountUntagged = Int32.Parse(ds.Tables[3].Rows[0][0].ToString()),
                        CountAll = Int32.Parse(ds.Tables[4].Rows[0][0].ToString()),
                    });
                }
                else
                {
                    list.Add(new MeetGreetTravelRequestGenericClass()
                    {
                        MeetGreetTravelRequestCount = maxRows,
                        MeetGreetTravelRequestList = recordList,
                    });
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtVessel != null)
                {
                    dtVessel.Dispose();
                }
                if (dtNationality != null)
                {
                    dtNationality.Dispose();
                }
                if (dtGender != null)
                {
                    dtGender.Dispose();
                }
                if (dtRank != null)
                {
                    dtRank.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   18/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of seafarers with restricted nationality
        /// </summary>
        /// <returns></returns>
        public List<TravelRequestRestrictedNationalityGenClass> GetTRRestrictedNationalityList
           (Int16 LoadType, DateTime FromDate, DateTime ToDate,
           string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
           string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
           int RegionID, int CountryID, int CityID, int PortID)
        {
            List<TravelRequestRestrictedNationalityGenClass> list = new List<TravelRequestRestrictedNationalityGenClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectRestrictedNationalities");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, FilterByName);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, Gender);
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, RankID);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                //TravelRequestArrivalDepartureSameDate.ArrivalDepartureSameDateCount = maxRows;

                var seafarerList = (from a in dtManifest.AsEnumerable()
                                    select new TravelRequestRestrictedNationality
                                    {
                                        RecLoc = a.Field<string>("colRecordLocatorVarchar"),
                                        TravelRequestID = GlobalCode.Field2Int(a["TravelRequestID"]),
                                        E1TravelRequest = GlobalCode.Field2Int(a["E1TravelRequest"]),
                                        SfID = GlobalCode.Field2Int(a["SfID"]),
                                        Name = a.Field<string>("Name"),
                                        DateOnOff = a.Field<DateTime?>("DateOnOff"),
                                        Status = a.Field<string>("Status"),

                                        Brand = a.Field<string>("Brand"),
                                        Vessel = a.Field<string>("Vessel"),
                                        VesselCode = a.Field<string>("VesselCode"),

                                        PortCode = a.Field<string>("PortCode"),
                                        Port = a.Field<string>("Port"),

                                        RankCode = a.Field<string>("RankCode"),
                                        Rank = a.Field<string>("Rank"),

                                        Nationality = a.Field<string>("Rank"),
                                        ReasonCode = a.Field<string>("ReasonCode"),
                                        IsWithSail = GlobalCode.Field2Bool(a["IsWithSail"]),
                                        NationalityName = a.Field<string>("NationalityName"),

                                        DepartureDateTime = a.Field<DateTime?>("DepartureDateTime"),
                                        ArrivalDateTime = a.Field<DateTime?>("ArrivalDateTime"),
                                        Airline = a.Field<string>("Airline"),
                                        FlightNo = a.Field<string>("FlightNo"),
                                        OriginAirport = a.Field<string>("OriginAirport"),
                                        DestinationAirport = a.Field<string>("DestinationAirport"),
                                        Birthday = a.Field<DateTime?>("Birthday"),

                                    }).ToList();

                list.Add(new TravelRequestRestrictedNationalityGenClass()
                {
                    TravelRequestRestrictedNationalityList = seafarerList,
                    TravelRequestRestrictedNationalityCount = maxRows

                });
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   18/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of seafarers with restricted nationality to export
        /// </summary>
        /// <returns></returns>
        public DataTable GetTRRestrictedNationalityExport(string UserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectRestrictedNationalitiesExport");
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[0];
                //TravelRequestArrivalDepartureSameDate.ArrivalDepartureSameDateCount = maxRows;

                return dtManifest;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/11/2012
        /// Description:    Get Travel Request of Meet & Greet & Service Provider Role for Export to Excel
        /// ========================================================
        /// Author:         Marco Abejar
        /// Date Created:   22/03/2013
        /// Description:    Add sorting
        /// ========================================================
        /// </summary>
        public static DataTable GetMeetGreetTravelRequestExport(Int16 LoadType, string sUser, string sRole, string
            sPortID, string sAirportID, string SortBy)
        {
            //List<MeetGreetTravelRequestExport> list = new List<MeetGreetTravelRequestExport>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dt = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectMeetGreetExport");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, sUser);

                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(sPortID));
                db.AddInParameter(dbCommand, "@pAirportIDInt", DbType.Int32, GlobalCode.Field2Int(sAirportID));
                db.AddInParameter(dbCommand, "@pSortBy", DbType.String, SortBy);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];

                return dt;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Marco Abejar
        /// Date Created:   13/11/2013
        /// Description:     Get PortAgent request
        /// 
        public static void GetPortAgentRequest(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
            DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iViewType, string SortBy, int iVendorId, int iNoOfDay)
        {
            List<PortAgentRequest> recordList = new List<PortAgentRequest>();
            int iCount = 0;

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            HttpContext.Current.Session["PortAgentRequestCount"] = 0;
            HttpContext.Current.Session["PortAgentRequest"] = recordList;

            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectPortAgentRequestService");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRoleVarchar", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(sPortID));
                db.AddInParameter(dbCommand, "@pAirportIDInt", DbType.Int32, GlobalCode.Field2Int(sAirportID));

                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, dDate);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, GlobalCode.Field2Int(VesselID));
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, GlobalCode.Field2TinyInt(FilterByName));
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, GlobalCode.Field2Int(NationalityID));
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, GlobalCode.Field2Int(Gender));
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, GlobalCode.Field2Int(RankID));
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pViewType", DbType.Int16, iViewType);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, SortBy);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int16, iVendorId);
                db.AddInParameter(dbCommand, "@piNoOfDay", DbType.Int16, iNoOfDay);


                ds = db.ExecuteDataSet(dbCommand);

                DataTable dt = ds.Tables[0];
                iCount = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = SortBy;
                    dt = dv.ToTable();
                }

                recordList = (from a in dt.AsEnumerable()
                              select new PortAgentRequest
                              {
                                  IDBigInt = GlobalCode.Field2Int(a["colIdBigint"]),
                                  RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                  Brand = a.Field<string>("Brand"),
                                  Vessel = a.Field<string>("Vessel"),
                                  Port = a.Field<string>("Port"),
                                  Rank = a.Field<string>("Rank"),

                                  TravelRequestID = GlobalCode.Field2Int(a["colTravelReqIdInt"]),
                                  SfID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                  E1TrID = GlobalCode.Field2Int(a["colE1TravelReqIdInt"]),

                                  Name = a.Field<string>("SeafarerName"),
                                  Status = a.Field<string>("colStatusVarchar"),
                                  DateOnOff = a.Field<string>("SignOnOffDate"),

                                  Hotel = a.Field<string>("Hotel"),
                                  Checkin = a.Field<string>("CheckIn"),
                                  Duration = GlobalCode.Field2Int(a["Duration"]),

                                  Departure = a.Field<string>("AirportDeparture"),
                                  Arrival = a.Field<string>("AirportArrival"),

                                  //DepartureDateTime = GlobalCode.Field2DateTime(a["colDepartureDateTime"]),
                                  //ArrivalDateTime = GlobalCode.Field2DateTime(a["colArrivalDateTime"]),

                                  //DepartureDate = a.Field<string>("colDepartureDate"),
                                  //ArrivalDate = a.Field<string>("colArrivalDate"),
                                  DepartureDate = a.Field<string>("colDepartureDateTime"),
                                  ArrivalDate = a.Field<string>("colArrivalDateTime"),

                                  DepartureTime = a.Field<string>("colDepartureDateTime"),
                                  ArrivalTime = a.Field<string>("colArrivalDateTime"),

                                  Airline = a.Field<string>("Airline"),
                                  FlightNo = a.Field<string>("colFlightNoVarchar"),

                                  AirportID = GlobalCode.Field2Int(a["colAirportIDInt"]),
                                  SeaportID = GlobalCode.Field2Int(a["colPortIdInt"]),

                                  PassportNo = a.Field<string>("PassportNo"),
                                  PassportIssueDate = a.Field<string>("PassportIssueDate"),
                                  PassportExpiryDate = a.Field<string>("PassportExpiryDate"),
                                  IsTaggedByUser = GlobalCode.Field2Bool(a["IsTaggedByUser"]),

                                  Checkout = a.Field<string>("Checkout"),
                                  SingleDouble = a.Field<string>("colSingleDoubleFloat"),
                                  Gender = a.Field<string>("colGender"),
                                  CostCenter = a.Field<string>("colCostCenter"),
                                  Nationality = a.Field<string>("colNationality"),
                                  MealAllowance = a.Field<string>("colMealAllowance"),
                                  Lastname = a.Field<string>("colLastname"),
                                  Firstname = a.Field<string>("colFirstname"),
                                  TagDateTime = a.Field<string>("colTagDateTime"),
                                  Birthday = a.Field<string>("colBirthday"),


                                  IsHotel = GlobalCode.Field2Bool(a["colIsHotelBit"]),
                                  IsTransportation = GlobalCode.Field2Bool(a["colIsTransBit"]),
                                  IsMAG = GlobalCode.Field2Bool(a["colIsMAGBit"]),
                                  IsLuggage = GlobalCode.Field2Bool(a["colIsLuggageBit"]),
                                  IsSafeguard = GlobalCode.Field2Bool(a["colIsSafeguardBit"]),
                                  IsVisa = GlobalCode.Field2Bool(a["colIsVisaBit"]),
                                  IsOther = GlobalCode.Field2Bool(a["colIsOtherBit"]),

                                  SequenceNo = GlobalCode.Field2String(a["colSeqNoInt"]),
                                  IsFirstPartition = GlobalCode.Field2Bool(a["IsFirstPartition"]),
                              }).ToList();

                HttpContext.Current.Session["PortAgentRequestCount"] = iCount;
                HttpContext.Current.Session["PortAgentRequest"] = recordList;

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// ========================================================
        /// Author:         Josephine Gad
        /// Date Created:   11/Aug/2014
        /// Description:    Export list of Service Provider Request
        /// ========================================================
        /// </summary>
        public static DataTable GetPortAgentRequestExport(Int16 LoadType, string sUser, string SortBy)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dt = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectPortAgentRequestService_Export");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, sUser);

                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, SortBy);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];

                return dt;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

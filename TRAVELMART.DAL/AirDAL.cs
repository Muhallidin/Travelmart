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
    public class AirDAL
    {
     
     /// <summary>        
     /// Date Created:  25/07/2011
     /// Created By:    Josephine Gad
     /// (description)  Selecting list of air travel details  by Air ID      
     ///----------------------------------------------------------------
     /// Date Modified: 28/11/2011
     /// Modified By:   Charlene Remotigue
     /// (description)  optimization (use datareader instead of datatable
     /// </summary>
     /// <param name="AirIdBigint"></param>
     /// <param name="SeqNo"></param>
     /// <returns></returns>
        public static IDataReader GetSFAirTravelDetailsById(string AirIdBigint, string SeqNo)
        {

            Database SFDatebase = ConnectionSetting.GetConnection(); //DatabaseFactory.CreateDatabase();  //ConnectionSetting.GetConnection(); 
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFAirTraveInfoById");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolIdBigint", DbType.Int32, AirIdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSeqNoInt", DbType.Int32, SeqNo);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }
        /// <summary>
        /// Date Created:  14/09/2011
        /// Created By:    Josephine Gad
        /// (description)  Selecting list of air travel details  by Air ID (nonSabre)
        /// --------------------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable)
        /// </summary>
        /// <param name="AirIdBigint"></param>
        /// <returns></returns>
        public static IDataReader GetSFAirTravelDetailsOtherById(string AirIdBigint)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFAirTraveInfoOtherById");
                SFDatebase.AddInParameter(SFDbCommand, "@TransAirIDBigInt", DbType.Int32, AirIdBigint);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }

        /// <summary>             
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Update air travel details  by Air ID
        /// --------------------------------------------------
        /// Date Modified: 05/08/2011
        /// Modified By: Marco Abejar
        /// (description) Add sequence number        
        /// </summary>
        /// <param name="AirIdBigint"></param>
        /// <param name="SeqNo"></param>
        /// <param name="AirStatusString"></param>
        /// <param name="IsBillToCrew"></param>
        /// <param name="RemarksString"></param>
        /// <param name="ModifiedByString"></param>
        public static void UpdateAirBooking(string AirIdBigint, string SeqNo, string AirStatusString, bool IsBillToCrew, 
            string RemarksString, string ModifiedByString)
        {
           

            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;            
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateAirBookings");
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, Int64.Parse(AirIdBigint));
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSeqNoInt", DbType.Int64, Int64.Parse(SeqNo));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirStatusVarchar", DbType.String, AirStatusString);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolIsBilledToCrewBit", DbType.String, IsBillToCrew);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolRemarksVarchar", DbType.String, RemarksString);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolDateModifiedDatetime", DbType.String, ModifiedByString);
                SFDatebase.ExecuteNonQuery(SFDbCommand);                
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
            }
        }
        /// <summary>
        /// Date Created:   14/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert air travel details  by Air ID (nonSabre)
        /// </summary>
        /// <param name="TravelReqIDString"></param>
        /// <param name="FlightNoString"></param>
        /// <param name="ArrivalDateString"></param>
        /// <param name="DepartureDateString"></param>
        /// <param name="AirlineString"></param>
        /// <param name="TicketNoString"></param>
        /// <param name="AirStatusString"></param>
        /// <param name="CreatedByString"></param>
        /// <param name="StatusString"></param>
        /// <param name="RemarksString"></param>
        /// <param name="IsBillToCrew"></param>
        public static void InsertAirBookingOther(string TravelReqIDString, string FlightNoString,
            string ArrivalDateString, string DepartureDateString, string DepartureLoc, string ArrivalLoc,
            string AirlineString, string TicketNoString, string AirStatusString, string CreatedByString,
            string StatusString, string RemarksString, bool IsBillToCrew )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertAirBookingsOther");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int64, Int64.Parse(TravelReqIDString));
                SFDatebase.AddInParameter(SFDbCommand, "@pFlightNoVarchar", DbType.String, FlightNoString);

                SFDatebase.AddInParameter(SFDbCommand, "@pArrivalDateTime", DbType.DateTime, DateTime.Parse(ArrivalDateString));
                SFDatebase.AddInParameter(SFDbCommand, "@pDepartureDateTime", DbType.DateTime, DateTime.Parse(DepartureDateString));

                SFDatebase.AddInParameter(SFDbCommand, "@pDepartureAirportLocationCodeVarchar", DbType.String, DepartureLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pArrivalAirportLocationCodeVarchar", DbType.String, ArrivalLoc);


                SFDatebase.AddInParameter(SFDbCommand, "@pMarketingAirlineCodeVarchar", DbType.String, AirlineString);
                SFDatebase.AddInParameter(SFDbCommand, "@pTicketNoVarchar", DbType.String, TicketNoString);

                SFDatebase.AddInParameter(SFDbCommand, "@pAirStatusVarchar", DbType.String, AirStatusString);

                SFDatebase.AddInParameter(SFDbCommand, "@pDateCreatedDatetime", DbType.DateTime, DateTime.Now);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedByVarchar", DbType.String, CreatedByString);

                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, StatusString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarksVarchar", DbType.String, RemarksString);

                SFDatebase.AddInParameter(SFDbCommand, "@pIsBilledToCrewBit", DbType.String, IsBillToCrew);                                                
                SFDatebase.ExecuteNonQuery(SFDbCommand);
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
            }
        }
        /// <summary>
        /// Date Created:   14/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Update air travel details  by Air ID (nonSabre)
        /// </summary>
        /// <param name="AirIDString"></param>
        /// <param name="FlightNoString"></param>
        /// <param name="ArrivalDateString"></param>
        /// <param name="DepartureDateString"></param>
        /// <param name="DepartureLoc"></param>
        /// <param name="ArrivalLoc"></param>
        /// <param name="AirlineString"></param>
        /// <param name="TicketNoString"></param>
        /// <param name="AirStatusString"></param>
        /// <param name="ModifiedByString"></param>
        /// <param name="StatusString"></param>
        /// <param name="RemarksString"></param>
        /// <param name="IsBillToCrew"></param>
        public static void UpdateAirBookingOther(string AirIDString, string FlightNoString,
            string ArrivalDateString, string DepartureDateString, string DepartureLoc, string ArrivalLoc,
            string AirlineString, string TicketNoString, string AirStatusString, string ModifiedByString,
            string StatusString, string RemarksString, bool IsBillToCrew)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                DateTime ArrivalDateTime = DateTime.Parse(ArrivalDateString);
                DateTime DepartureDateTime = DateTime.Parse(DepartureDateString);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateAirBookingsOther");
                SFDatebase.AddInParameter(SFDbCommand, "@TransAirIDBigInt", DbType.Int64, Int64.Parse(AirIDString));
                SFDatebase.AddInParameter(SFDbCommand, "@pFlightNoVarchar", DbType.String, FlightNoString);

                SFDatebase.AddInParameter(SFDbCommand, "@pArrivalDateTime", DbType.DateTime, ArrivalDateTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pDepartureDateTime", DbType.DateTime, DepartureDateTime);

                SFDatebase.AddInParameter(SFDbCommand, "@pDepartureAirportLocationCodeVarchar", DbType.String, DepartureLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pArrivalAirportLocationCodeVarchar", DbType.String, ArrivalLoc);


                SFDatebase.AddInParameter(SFDbCommand, "@pMarketingAirlineCodeVarchar", DbType.String, AirlineString);
                SFDatebase.AddInParameter(SFDbCommand, "@pTicketNoVarchar", DbType.String, TicketNoString);

                SFDatebase.AddInParameter(SFDbCommand, "@pAirStatusVarchar", DbType.String, AirStatusString);

                SFDatebase.AddInParameter(SFDbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.Now);
                SFDatebase.AddInParameter(SFDbCommand, "@pModifiedbyVarchar", DbType.String, ModifiedByString);

                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, StatusString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarksVarchar", DbType.String, RemarksString);

                SFDatebase.AddInParameter(SFDbCommand, "@pIsBilledToCrewBit", DbType.String, IsBillToCrew);
                SFDatebase.ExecuteNonQuery(SFDbCommand);
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
            }
        }
        /// <summary>
        ///  Date Created:  14/09/2011
        ///  Created By:    Josephine Gad
        /// (description)   Get airline list
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAirline()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectAirline");                
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   19/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Get Airport List By country ID
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static DataTable GetAirportByCountry(string CountryID, string RegionID, string AiportName, string AirportInitial)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectAirportByCountryID");                
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportName", DbType.String, AiportName);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportInitial", DbType.String, AirportInitial);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List By Region ID
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetAirportByRegion(string RegionID, string AiportName)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            DataTable dt = null;
            DbCommand comm = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();

            try
            {
                comm = db.GetStoredProcCommand("uspGetAirportListByRegion");
                db.AddInParameter(comm, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(comm, "@pAirportName", DbType.String, AiportName);
                dt = db.ExecuteDataSet(comm).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new AirportDTO {
                            AirportIDString = GlobalCode.Field2String(a["colAirportIDInt"]),
                            AirportCodeString = GlobalCode.Field2String(a["colAirportCodeVarchar"]),
                            AirportNameString = GlobalCode.Field2String(a["AirportName"]),
                        }).ToList();
                return list;
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
        /// Date Created: 20/01/2012
        /// Created By: Gelo Oquialda
        /// (description) Load airport list by region
        /// ---------------------------------------------        
        /// </summary>
        //public static DataTable AirportLoad(string regionString, string userString)
        //{
        //    Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;
        //    IDataReader dataReader = null;
        //    DataTable HotelDataTable = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectAirportByRegionID");
        //        SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, regionString);
        //        SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
        //        dataReader = SFDatebase.ExecuteReader(SFDbCommand);
        //        HotelDataTable = new DataTable();
        //        HotelDataTable.Load(dataReader);
        //        return HotelDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (SFDbCommand != null)
        //        {
        //            SFDbCommand.Dispose();
        //        }
        //        if (HotelDataTable != null)
        //        {
        //            HotelDataTable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Date Created: 27/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Loads airport code with airport name
        /// </summary>
        public static IDataReader GetAirportCodeName(int AirportID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetAirportCodeName");
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.Int32, AirportID);                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
            }
        }

        /// <summary>
        /// Date Created: 31/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Insert airport hotel assignment
        /// ---------------------------------------------
        /// Date Modified: 07/Feb/2014
        /// Created By:    Josephine Gad
        /// (description)  Add audit trail
        /// </summary>
        public static void InsertAirportHotel(Int32 AirportID, Int32 BranchID, String User, Int16 iRoomType, 
            string sLogDescription, string sFunction, string sPageName)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                dbCommand = db.GetStoredProcCommand("uspAirportHotel_Insert");

                db.AddInParameter(dbCommand, "@pAirportID", DbType.Int32, AirportID);
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, BranchID);
                db.AddInParameter(dbCommand, "@pUser", DbType.String, User);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int16, iRoomType);

                db.AddInParameter(dbCommand, "@pLogDescription", DbType.String, sLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pPageName", DbType.String, sPageName);

                db.AddInParameter(dbCommand, "@pDateGMT", DbType.DateTime, dtGMT);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);


                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                //Int32 AirportHotelID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pAirportHotelID"));
                //return AirportHotelID;
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
    }
}

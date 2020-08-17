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
    public class SeafarerDAL
    {
        #region METHODS        
        /// <summary>                
        /// Date Created:   12/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Seafarer's Details        
        /// ---------------------------------------------
        /// Date Modified:  02/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close IDataReader and DataTable
        /// ---------------------------------------------
        /// Date Modified:  21/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add TravelRequestID and ManualRequestID parameter
        /// -------------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// -------------------------------------------------------------------
        /// Date Modified: 08/03/2012
        /// Modified By:   Josephine Gad
        /// (description)  Add E1TravelRequest parameter to include seafarer with no TR
        /// </summary>
        public static IDataReader SeafarerGetDetails(string SfID, string TravelRequestID, string ManualRequestID, bool ViewInTR)
        {            
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //DataTable SFDataTable = null;
            IDataReader dataReader = null;
            try
            {
                TravelRequestID = (TravelRequestID == "" ? "0" : TravelRequestID);
                ManualRequestID = (ManualRequestID == "" ? "0" : ManualRequestID);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSeafarerDetails");
                SFDatebase.AddInParameter(SFDbCommand, "@pSfID", DbType.String, SfID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, Int64.Parse(TravelRequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pManualRequestID", DbType.Int64, Int64.Parse(ManualRequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pViewInTR", DbType.Boolean, ViewInTR);
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

        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Request Info
        /// </summary>
        public static IDataReader SeafarerGetRequestDetails(string SfID, string TravelRequestID, string HotelRequestID, string AppNew)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                TravelRequestID = (TravelRequestID == "" ? "0" : TravelRequestID);               

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFInfoHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pSfID", DbType.String, SfID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, Int64.Parse(TravelRequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelRequestID", DbType.Int64, Int64.Parse(HotelRequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pAppNew", DbType.Int32, Int32.Parse(AppNew));
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
        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Request Info
        /// </summary>
        public static DataTable SeafarerGetCompanionDetails(string HotelRequestID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFCompanionHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelRequestID", DbType.Int64, GlobalCode.Field2Long(HotelRequestID));
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

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
            }
        }
        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Save SF  Hotel Request 
        /// -------------------------------------------
        /// Date Modified: 30/May/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add Shuttle, MealLunchDinner, Tax Bit and Tax Percent
        ///                Add fields for audit trail use 
        /// -------------------------------------------
        /// </summary>
        public static string SeafarerSaveRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType, 
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut, 
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent,  string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertSFHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestNo", DbType.String, RequestNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerIDInt", DbType.Int32, Int32.Parse(SfID));
                SFDatebase.AddInParameter(SFDbCommand, "@pLastNameVarchar", DbType.String, LastName);
                SFDatebase.AddInParameter(SFDbCommand, "@pFirstNameVarchar", DbType.String, FirstName);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Int32.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.Int32, Int32.Parse(AirportID));
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.Int32, Int32.Parse(HotelID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckinDate", DbType.DateTime, CheckinDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckoutDate", DbType.DateTime, CheckoutDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pNoNitesInt", DbType.Int32, Int32.Parse(NoNites));
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeId", DbType.String, RoomType);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealBreakfastBit", DbType.Boolean, MealBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealLunchBit", DbType.Boolean, MealLunch);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealDinnerBit", DbType.Boolean, MealDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealLunchDinnerBit", DbType.Boolean, MealLunchDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithShuttleBit", DbType.Boolean, WithShuttle);

                SFDatebase.AddInParameter(SFDbCommand, "@pRankIDInt", DbType.Int32, Int32.Parse(RankID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselInt", DbType.Int32, Int32.Parse(VesselInt));
                SFDatebase.AddInParameter(SFDbCommand, "@pCostCenterInt", DbType.Int32, Int32.Parse(CostCenter));
                SFDatebase.AddInParameter(SFDbCommand, "@pCommentsVarchar", DbType.String, Comments);
                SFDatebase.AddInParameter(SFDbCommand, "@pSfStatus", DbType.String, SfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeIn", DbType.String, TimeIn);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeOut", DbType.String, TimeOut);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmount", DbType.String, RoomAmount);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxInclusive", DbType.Boolean, IsRoomTax);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxPercentage", DbType.Double, GlobalCode.Field2Double(RoomTaxPercent));

                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTrID", DbType.String, TrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCurrency", DbType.String, Currency);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, strLogDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, strFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, strPageName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                 string sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();
                 trans.Commit();

                 return sHRID;
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// Date Modified: 04/02/2013
        /// Modified By:   Marco Abejar
        /// (description)  Submit SF  Hotel Request 
        /// ===========================================
        /// Date Modified: 07/Jun/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add fields for Audit Trail
        /// </summary>
        public static void SeafarerSubmitRequest(string RequestID, string UserID, string ContactName, string ContactNo,
            string Description, string Function, string FileName)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertSFBookRequest");                
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, Int32.Parse(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, CommonFunctions.GetDateTimeGMT(currentDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, currentDate);

                SFDatebase.ExecuteNonQuery(SFDbCommand);
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public static void SeafarerSaveComapnionRequest(string RequestDetailID, string RequestID, string LastName, string FirstName, string Relationship, string Gender, string UserID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertSFCompanionHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestDetailID", DbType.Int32, Int32.Parse(RequestDetailID));
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestID", DbType.Int32, Int32.Parse(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pLastNameVarchar", DbType.String, LastName);
                SFDatebase.AddInParameter(SFDbCommand, "@pFirstNameVarchar", DbType.String, FirstName);
                SFDatebase.AddInParameter(SFDbCommand, "@pRelationshipVarchar", DbType.String, Relationship);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Int32.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
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




        /// ---------------------------------------------
        /// Date Created:  24/10/2011
        /// Created By:    Ryan Bautista
        /// (description)   Voucher details
        /// -------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// -------------------------------------------
        /// Date Modified: 02/01/2011
        /// Modified By:   Josephine Gad
        /// (description)  Change parameter SfID to dStripe
        /// </summary>
        public static IDataReader VoucherGetDetails(decimal dStripe, Int32 BranchID, Int32 Days)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            //Double amount = 0;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVoucherDetails");
                SFDatebase.AddInParameter(SFDbCommand, "@pStripe", DbType.Decimal, dStripe);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolBranchID", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolDayNo", DbType.Int32, Days);
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
        /// Date Created: 26/07/2011
        /// Created By: Ryan bautista
        /// (description) Selecting Seafarer's Details
        /// </summary>
        public static DataTable GetSeafarer()
        {           
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSeafarer");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable = new DataTable();
                SFDataTable.Load(dataReader);
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
        /// Date Created:    03/10/2011
        /// Created By:      Josephine Gad
        /// (description)    Select seafarers by filter (Name or E1 ID)
        /// ---------------------------------------------
        /// Date Modified:   17/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------
        /// </summary>
        /// <param name="filterBy"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
       // public static DataTable GetSeafarerByFilter(string filterBy, string filterValue)
        public static List<SeafarerDTO> GetSeafarerByFilter(string filterBy, string filterValue)        
        {
            DataTable dt = null;
            DbCommand dbCommand = null;
            List<SeafarerDTO> list = null;
            try
            {
                filterBy = "2";
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                dbCommand = db.GetStoredProcCommand("uspGetSeafarer");
                db.AddInParameter(dbCommand, "@pFilterBy", DbType.String, filterBy);
                db.AddInParameter(dbCommand, "@pFilterValue", DbType.String, filterValue);
                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new SeafarerDTO
                        {
                            Nationality = a["Nationality"].ToString(),
                            Gender = a["Gender"].ToString(),
                            Rank = a["Rank"].ToString(),
                            CostCenter = a["CostCenter"].ToString(),
                            NationalityID = a["NationalityID"].ToString(),
                            RankID = a["RankID"].ToString(),
                            CostCenterID = a["CostCenterID"].ToString()
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
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Modified:   17/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Get Seafarer List
        /// ---------------------------------------------
        /// </summary>
        /// <param name="filterBy"></param>
        /// <param name="filterValue"></param>
        /// <returns></returns>
        public static List<SeafarerListDTO> GetSeafarerListByFilter(string filterBy, string filterValue)
        {
            DataTable dt = null;
            DbCommand dbCommand = null;
            List<SeafarerListDTO> list = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                dbCommand = db.GetStoredProcCommand("uspGetSeafarer");
                db.AddInParameter(dbCommand, "@pFilterBy", DbType.String, filterBy);
                db.AddInParameter(dbCommand, "@pFilterValue", DbType.String, filterValue);
                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new SeafarerListDTO
                        {
                            SFID = a["SFID"].ToString(),
                            Name = a["Name"].ToString()                            
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
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   25/10/2011
        /// Created By:     Charlene Remotigue
        /// (description)   Load all users that has tagged seafarer as scanned
        /// ---------------------------------------------
        /// </summary>
        /// <param name="mReqId"></param>
        /// <param name="tReqId"></param>
        /// <returns></returns>
        public static DataTable GetSeafarerOtherInfo(string mReqId, string tReqId)
        {
            DataTable dt = null;
            DbCommand dbCommand = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                dbCommand = db.GetStoredProcCommand("uspLoadScanningUsers");
                db.AddInParameter(dbCommand, "@pmReqId", DbType.Int32, Int32.Parse(mReqId));
                db.AddInParameter(dbCommand, "@ptReqId", DbType.Int32, Int32.Parse(tReqId));
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
        /// Date Created:   02/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get room type by stripe
        /// --------------------------------------------- 
        /// </summary>
        /// <param name="dStripe"></param>
        /// <returns></returns>
        public static IDataReader GetRoomTypeByStripe(decimal dStripe)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectHotelRoomTypeByStripe");
                db.AddInParameter(command, "@pStripes", DbType.Decimal, dStripe);
                dr = db.ExecuteReader(command);
                return dr;
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
        /// Date Modified: 01/04/2013
        /// Modified By:   Marco Abejar
        /// (description)  Remove Companion
        /// </summary>
        public static DataTable RemoveRequestCompanion(long RequestDetailID,long RequestID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspRemoveCompanionRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestDetailID", DbType.Int64,  RequestDetailID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDBigint", DbType.Int64, RequestID);
                //SFDatebase.ExecuteNonQuery(SFDbCommand);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }
        /// <summary>
        /// Date Modified: 01/04/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Hotel Request List View
        /// </summary>
        public static DataTable GetHotelRequestList(string SortBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelRequestList");
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = SortBy;
                    dt = dv.ToTable();
                }

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
            }
        }
        /// <summary>
        /// Date Modified: 01/04/2013
        /// Modified By:   Marco Abejar
        /// (description)  Get SF Booked Hotel Request List View
        /// </summary>
        public static DataTable GetBookedHotelRequestList(string SortBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectBookedHotelRequestList");
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = SortBy;
                    dt = dv.ToTable();
                }

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
            }
        }
        #endregion
    }
}

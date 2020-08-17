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
using System.Data.SqlClient;


namespace TRAVELMART.DAL
{
    public class HotelForecastDAL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date            29/Jan/2015
        /// Description:    Get Forecast from Micro
        /// </summary>
        /// <returns></returns>
        public List<HotelForecastForApprovalList> GetForecastManifestList(string sBranchName,
           string sDateFrom, string sDateTo, 
            string sVesselCode, int sPortID,
            string sUser, string sRole, bool bIsHotelVendorView,
            Int16 LoadType, bool bShowAll, int StartRow, int MaxRow)
        {
            List<HotelForecastForApprovalList> list = new List<HotelForecastForApprovalList>();
            List<HotelForecastCurrency> listCurrencySelected = new List<HotelForecastCurrency>();
            List<Currency> listCurrency = new List<Currency>();

            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dt = null;
            DataTable dtNoOfDays = null;

            DataTable dtCurrencySelected = null;
            DataTable dtCurrency = null;
            DataTable dtHotelBranch = null;

            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspHotelForecastGet");

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pBranchName", DbType.String, sBranchName);

                db.AddInParameter(dbCommand, "@pDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(sDateFrom));
                db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, GlobalCode.Field2DateTime(sDateTo));

                db.AddInParameter(dbCommand, "@pVesselCode", DbType.String, "");

                db.AddInParameter(dbCommand, "@pAirportCode", DbType.String, "");
                db.AddInParameter(dbCommand, "@pHotelVendorView", DbType.Boolean, bIsHotelVendorView);

                db.AddInParameter(dbCommand, "@pShowAll", DbType.Boolean, bShowAll);
                
                
                //db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                //db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, MaxRow);
                //db.AddInParameter(dbCommand, "@pLoadType", DbType.String, LoadType);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());


                list = (from a in dt.AsEnumerable()
                        select new HotelForecastForApprovalList
                        {
                            colBranchIDInt = GlobalCode.Field2Long(a["colBranchIDInt"]),
                            colDate = GlobalCode.Field2DateTime(a["colDate"]),
                            
                           // Confirmed_DBL = GlobalCode.Field2Int(a["Confirmed_DBL"]),
                           // Overflow_DBL = GlobalCode.Field2Int(a["Overflow_DBL"]),
                           
                           // Confirmed_SGL = GlobalCode.Field2Int(a["Confirmed_SGL"]),
                           // Overflow_SGL = GlobalCode.Field2Int(a["Overflow_SGL"]),

                            Forecast_DBL = GlobalCode.Field2Int(a["colForecastDBL"]),
                            Forecast_SGL = GlobalCode.Field2Int(a["colForecastSGL"]),

                            Forecast_DBL_Adj = GlobalCode.Field2Int(a["colForecastDBLAdj"]),
                            Forecast_SGL_Adj = GlobalCode.Field2Int(a["colForecastSGLAdj"]),

                            RoomBlock_DBL = GlobalCode.Field2Int(a["colRoomBlockDBL"]),
                            RoomBlock_SGL = GlobalCode.Field2Int(a["colRoomBlockSGL"]),

                            RoomBlock_DBL_Total = GlobalCode.Field2Int(a["colRoomBlockDBLTotal"]),
                            RoomBlock_SGL_Total = GlobalCode.Field2Int(a["colRoomBlockSGLTotal"]),

                            TMBooked_DBL = GlobalCode.Field2Float(a["colTMBookedDBL"]),
                            TMBooked_SGL = GlobalCode.Field2Float(a["colTMBookedSGL"]),

                            ToBeAdded_DBL = GlobalCode.Field2Int(a["colToAddDBL"]),
                            ToBeAdded_SGL = GlobalCode.Field2Int(a["colToAddSGL"]),

                            IsEnable = GlobalCode.Field2Bool(a["colIsEnableBit"]),

                            Forecast_DBL_Old = GlobalCode.Field2Int(a["colForecastDBLOld"]),
                            Forecast_SGL_Old = GlobalCode.Field2Int(a["colForecastSGLOld"]),

                            ToBeAdded_DBL_Suggested = GlobalCode.Field2Int(a["colToAddDBLSuggested"]),
                            ToBeAdded_SGL_Suggested = GlobalCode.Field2Int(a["colToAddSGLSuggested"]),

                            Remarks = a.Field<string>("colRemarksVarchar"),

                            ApprovedDBL = GlobalCode.Field2Int(a["colApprovedDBL"]),
                            ApprovedSGL = GlobalCode.Field2Int(a["colApprovedSGL"]),
                            ActionDone = a.Field<string>("colActionVarchar"),

                            IsLinkToRequestVisibleDBL = GlobalCode.Field2Bool(a["IsLinkToRequestVisibleDBL"]),
                            IsLinkToRequestVisibleSGL = GlobalCode.Field2Bool(a["IsLinkToRequestVisibleSGL"]),

                            IsNeededHotelVisibleDBL = GlobalCode.Field2Bool(a["IsNeededHotelVisibleDBL"]),
                            IsNeededHotelVisibleSGL = GlobalCode.Field2Bool(a["IsNeededHotelVisibleSGL"]),

                            RoomToDropDBL = GlobalCode.Field2Int(a["colRoomToDropDBL"]),
                            RoomToDropSGL = GlobalCode.Field2Int(a["colRoomToDropSGL"]),

                            RoomToDropColorDBL = a.Field<string>("RoomToDropColorDBL"),
                            RoomToDropColorSGL = a.Field<string>("RoomToDropColorSGL"),

                            RatePerDayMoneySGL = GlobalCode.Field2Float(a["colRatePerDayMoneySGL"]),
                            RatePerDayMoneyDBL = GlobalCode.Field2Float(a["colRatePerDayMoneyDBL"]),
                            CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                            RoomRateTaxPercentage = GlobalCode.Field2Float(a["colRoomRateTaxPercentage"]),
                            RoomRateIsTaxInclusive = GlobalCode.Field2Bool(a["colRoomRateIsTaxInclusive"]),

                            IsRoomToDropVisibleToVendorBDL = GlobalCode.Field2Bool(a["IsRoomToDropVisibleToVendorBDL"]),
                            IsRoomToDropVisibleToVendorSGL = GlobalCode.Field2Bool(a["IsRoomToDropVisibleToVendorSGL"]),

                            IsRCCLApprovalVisible = GlobalCode.Field2Bool(a["IsRCCLApprovalVisible"]),
                            MessageToVendor = GlobalCode.Field2String(a["MessageToVendor"]),
                            CurrencyName = GlobalCode.Field2String(a["CurrencyName"]),
                        }).ToList();

                HttpContext.Current.Session["HotelForecastMicroApproval_Count"] = GlobalCode.Field2Int(maxRows);

                dtCurrencySelected = ds.Tables[2];
                listCurrencySelected = (from a in dtCurrencySelected.AsEnumerable()
                                        select new HotelForecastCurrency
                                        {
                                            CurrencyID = GlobalCode.Field2Int(a["CurrencyID"]),
                                            CurrencyName = a.Field<string>("CurrencyName"),

                                            RateMoney = GlobalCode.Field2Decimal(a["RateMoney"]),
                                            IsTaxInclusive = GlobalCode.Field2Bool(a["IsTaxInclusive"]),
                                            Tax = GlobalCode.Field2Decimal(a["TaxPercentage"]),
                                            RoomTypeID = GlobalCode.Field2TinyInt(a["colRoomTypeIDInt"]),
                                        }).ToList();

                HttpContext.Current.Session["HotelForecastMicroApproval_CurrencySelected"] = listCurrencySelected;

                dtHotelBranch = ds.Tables[3];
                List<ContractHotel> listBranch = new List<ContractHotel>();
                listBranch = (from a in dtHotelBranch.AsEnumerable()
                              select new ContractHotel
                              {
                                  contractID = GlobalCode.Field2Long(a["colContractIdInt"]),
                                  contractStatus = GlobalCode.Field2String(a["colContractStatusVarchar"]),
                                  contractStartDate = a.Field<DateTime?>("colContractDateStartedDate"),
                                  contractEndDate = a.Field<DateTime?>("colContractDateEndDate"),
                              }).ToList();
                HttpContext.Current.Session["HotelForecastMicroApproval_ContractHotel"] = listBranch;


                if (LoadType == 0)
                {
                    dtNoOfDays = ds.Tables[4];
                    TMSettings.NoOfDaysForecast = GlobalCode.Field2TinyInt(dtNoOfDays.Rows[0]["colNoOfDays_Forecast"]);
                    TMSettings.NoOfDaysForecastVendor = GlobalCode.Field2TinyInt(dtNoOfDays.Rows[0]["colNoOfDays_Forecast_Vendor"]);

                   
                    //dtCurrency = ds.Tables[5];
                    //listCurrency = (from a in dtCurrency.AsEnumerable()
                    //                select new Currency
                    //                {
                    //                    CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                    //                    CurrencyName = a.Field<string>("colCurrencyNameVarchar"),
                    //                }).ToList();

                    //HttpContext.Current.Session["HotelForecastMicroApproval_Currency"] = listCurrency;
                    
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
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (dtCurrencySelected != null)
                {
                    dtCurrencySelected.Dispose();
                }
                if(dtNoOfDays != null)
                {
                    dtNoOfDays.Dispose();
                }
                if (dtHotelBranch != null)
                {
                    dtHotelBranch.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>            
        /// Date Created:   04/Feb/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast
        /// ----------------------------------------        
        /// </summary>

        public void UpdateForecastManifest(int iHotelID,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtRoomToBeAdded)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspHotelForecastUpdate");

                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iHotelID);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, dtDateCreated);

                SqlParameter param = new SqlParameter("@pTempHotelForecast", dtRoomToBeAdded);
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
                if (dtRoomToBeAdded != null)
                {
                    dtRoomToBeAdded.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   05/May/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast to Override Room Blocks
        /// ----------------------------------------        
        /// </summary>
        public void ApproveForecastManifest(int iHotelID,
            //float fRateSGL, Int32 iCurrencySGL, float fTaxPercentSGL, bool bIsTaxInclusiveSGL,
            //float fRateDBL, Int32 iCurrencyDBL, float fTaxPercentDBL, bool bIsTaxInclusiveDBL,

            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtRoomToBeAdded)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspHotelForecastApproveWithEmail");

                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iHotelID);

                //db.AddInParameter(dbCommand, "@pRatePerDayMoneySGL", DbType.Decimal, fRateSGL);
                //db.AddInParameter(dbCommand, "@pCurrencyIDIntSGL", DbType.Int32, iCurrencySGL);
                //db.AddInParameter(dbCommand, "@pRoomRateTaxPercentageSGL", DbType.Decimal, fTaxPercentSGL);
                //db.AddInParameter(dbCommand, "@pRoomRateTaxInclusiveSGL", DbType.Boolean, bIsTaxInclusiveSGL);


                //db.AddInParameter(dbCommand, "@pRatePerDayMoneyDBL", DbType.Decimal, fRateDBL);
                //db.AddInParameter(dbCommand, "@pCurrencyIDIntDBL", DbType.Int32, iCurrencyDBL);
                //db.AddInParameter(dbCommand, "@pRoomRateTaxPercentageDBL", DbType.Decimal, fTaxPercentDBL);
                //db.AddInParameter(dbCommand, "@pRoomRateTaxInclusiveDBL", DbType.Boolean, bIsTaxInclusiveDBL);


                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, dtDateCreated);

                SqlParameter param = new SqlParameter("@pTempHotelForecast", dtRoomToBeAdded);
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
                if (dtRoomToBeAdded != null)
                {
                    dtRoomToBeAdded.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   07/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast to Override Room Blocks By RCCL
        /// ----------------------------------------        
        /// </summary>
        public void ApproveForecastManifestByRCCL(int iHotelID,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtRoomToBeAdded)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspHotelForecastApproveWithEmail_ByRCCL");

                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iHotelID);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, dtDateCreated);

                SqlParameter param = new SqlParameter("@pTempHotelForecast", dtRoomToBeAdded);
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
                if (dtRoomToBeAdded != null)
                {
                    dtRoomToBeAdded.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   29/Jul/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast through Request
        /// ----------------------------------------        
        /// </summary>
        public void RequestHotelRoom(DateTime dDate, bool bIsSGL, bool bISDBL, Int32 iBranchID,
            Int32 iBranchIDSGL, Int32 iBranchIDDBL, int iRoomCountSGL, int iRoomCountDBL,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspHotelForecastRequest");

                db.AddInParameter(dbCommand, "@pDate", DbType.Date, dDate);

                db.AddInParameter(dbCommand, "@pIsSGL", DbType.Boolean, bIsSGL);
                db.AddInParameter(dbCommand, "@pIsDBL", DbType.Boolean, bISDBL);

                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, iBranchID);                
                db.AddInParameter(dbCommand, "@pBranchIDSGL", DbType.Int32, iBranchIDSGL);
                db.AddInParameter(dbCommand, "@pBranchIDDBL", DbType.Int32, iBranchIDDBL);

                db.AddInParameter(dbCommand, "@pRoomCountSGL", DbType.Int32, iRoomCountSGL);
                db.AddInParameter(dbCommand, "@pRoomCountDBL", DbType.Int32, iRoomCountDBL);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);

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
        /// Author:         Josephine Monteza
        /// Date            30/Jul/2015
        /// Description:    Get Forecast with action already
        /// </summary>
        /// <returns></returns>
        public static List<HotelWithAction> ValidateHotelForecast(int iBranchIDInt, string sDate)
        {
            List<HotelWithAction> list = new List<HotelWithAction>();
           
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable dtResult = null;
           
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspHotelForecastUpdateValidate");
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iBranchIDInt);
                db.AddInParameter(dbCommand, "@pDate", DbType.String, sDate);

                ds = db.ExecuteDataSet(dbCommand);
                dtResult = ds.Tables[0];

                list = (from a in dtResult.AsEnumerable()
                        select new HotelWithAction
                        {
                            colDate = a.Field<string>("sDate"),
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
                if (dtResult != null)
                {
                    dtResult.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
    }
}

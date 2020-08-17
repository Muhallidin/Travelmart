using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class SafeguardDAL
    {
        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Get list of Safeguard Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsGet(string sSafeguardVendorName, string sOrderyBy, int iStartRow, int iMaxRow)
        {
            List<VendorSafeguardList> SafeguardList = new List<VendorSafeguardList>();
            int iCount = 0;

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dt = null;

            HttpContext.Current.Session["SafeguardVendorCount"] = 0;
            HttpContext.Current.Session["SafeguardVendorList"] = SafeguardList;

            try
            {
                dbCom = db.GetStoredProcCommand("uspSafeguardVendorsGet");
                db.AddInParameter(dbCom, "@pSafeguardName", DbType.String, sSafeguardVendorName);
                db.AddInParameter(dbCom, "@pOrderBy", DbType.String, sOrderyBy);
                db.AddInParameter(dbCom, "@pStartRow", DbType.String, iStartRow);
                db.AddInParameter(dbCom, "@pMaxRow", DbType.String, iMaxRow);

                dSet = db.ExecuteDataSet(dbCom);
                iCount = GlobalCode.Field2Int(dSet.Tables[0].Rows[0][0]);
                dt = dSet.Tables[1];

                SafeguardList = (from a in dt.AsEnumerable()
                                 select new VendorSafeguardList
                                 {
                                     SafeguardID = GlobalCode.Field2Int(a["colSafeguardVendorIDInt"]),
                                     VendorName = a.Field<string>("colSafeguardNameVarchar"),
                                     Country = a.Field<string>("colCountryNameVarchar"),
                                     City = a.Field<string>("colCityNameVarchar"),
                                     ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                                     FaxNo = a.Field<string>("colFaxNoVarchar"),
                                     EmailTo = a.Field<string>("colEmailToVarchar"),
                                     Website = a.Field<string>("colWebsiteVarchar"),
                                 }).ToList();

                HttpContext.Current.Session["SafeguardVendorCount"] = iCount;
                HttpContext.Current.Session["SafeguardVendorList"] = SafeguardList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Get Safeguard Vendor Details, Country List and City List
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsGetByID(Int32 iVendorID, Int16 iLoadType)
        {
            List<CountryList> listCountry = new List<CountryList>();
            List<CityList> listCity = new List<CityList>();
            List<VendorSafeguardDetails> listSafeguardDetails = new List<VendorSafeguardDetails>();
            List<Seaport> listSeaportContract = new List<Seaport>();
            List<Seaport> listSeaportNotInContract = new List<Seaport>();


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtSafeguard = null;
            DataTable dtCountry = null;
            DataTable dtCity = null;
            DataTable dtCityFilter = null;
            DataTable dtVendorSeaport = null;
            DataTable dtVendorNotSeaport = null;

            HttpContext.Current.Session["CountryList"] = listCountry;
            HttpContext.Current.Session["CityList"] = listCountry;
            HttpContext.Current.Session["SafeguardVendorDetails"] = listSafeguardDetails;
            HttpContext.Current.Session["SafeguardVendorCityFilter"] = "";
            HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
            HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;


            try
            {
                dbCom = db.GetStoredProcCommand("uspSafeguardVendorsGetByID");
                db.AddInParameter(dbCom, "@pSafeguardVendorIDInt", DbType.String, iVendorID);
                db.AddInParameter(dbCom, "@pLoadType", DbType.String, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[0] != null)
                {
                    dtSafeguard = dSet.Tables[0];
                    listSafeguardDetails = (from a in dtSafeguard.AsEnumerable()
                                            select new VendorSafeguardDetails
                                          {
                                              SafeguardID = GlobalCode.Field2Int(a["colSafeguardVendorIDInt"]),
                                              VendorName = GlobalCode.Field2String(a["colSafeguardNameVarchar"]),

                                              CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                              CountryName = GlobalCode.Field2String(a["CountryName"]),

                                              CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                              CityName = GlobalCode.Field2String(a["CityName"]),

                                              ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                                              FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                                              ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                                              Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                                              EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                                              EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                                              Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
                                          }).ToList();
                }
                if (dSet.Tables[1] != null)
                {
                    dtCountry = dSet.Tables[1];
                    listCountry = (from a in dtCountry.AsEnumerable()
                                   select new CountryList
                                   {
                                       CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                       CountryName = GlobalCode.Field2String(a["colCountryNameVarchar"]),
                                   }).ToList();
                }
                if (dSet.Tables[2] != null)
                {
                    dtCity = dSet.Tables[2];
                    listCity = (from a in dtCity.AsEnumerable()
                                select new CityList
                                {
                                    CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
                                    CityName = GlobalCode.Field2String(a["CityCodeName"]),
                                }).ToList();
                }
                if (dSet.Tables[3] != null)
                {
                    dtCityFilter = dSet.Tables[3];
                }

                if (dSet.Tables[4] != null)
                {
                    dtVendorSeaport = dSet.Tables[4];
                    listSeaportContract = (from a in dtVendorSeaport.AsEnumerable()
                                           select new Seaport
                                           {
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = a.Field<string>("Seaport")
                                           }).ToList();
                }
                if (dSet.Tables[5] != null)
                {
                    dtVendorNotSeaport = dSet.Tables[5];
                    listSeaportNotInContract = (from a in dtVendorNotSeaport.AsEnumerable()
                                                select new Seaport
                                                {
                                                    SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                    SeaportName = a.Field<string>("Seaport")
                                                }).ToList();
                }


                HttpContext.Current.Session["CountryList"] = listCountry;
                HttpContext.Current.Session["CityList"] = listCity;
                HttpContext.Current.Session["SafeguardVendorDetails"] = listSafeguardDetails;
                HttpContext.Current.Session["SafeguardVendorCityFilter"] = GlobalCode.Field2String(dtCityFilter.Rows[0][0]);
                HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
                HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtSafeguard != null)
                {
                    dtSafeguard.Dispose();
                }
                if (dtCountry != null)
                {
                    dtCountry.Dispose();
                }
                if (dtCity != null)
                {
                    dtCity.Dispose();
                }
                if (dtCityFilter != null)
                {
                    dtCityFilter.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Save Safeguard Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsSave(Int32 iSafeguardVendorID, string sSafeguardVendorName, Int32 iCountryID,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
            string sEmailCc, string sEmailTo, string sWebsite,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dt, DataTable dtSeaport)
        {
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                dbCom = db.GetStoredProcCommand("uspSafeguardVendorsSave");

                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                dbCom.Parameters.Add(param);


                db.AddInParameter(dbCom, "@pSafeguardVendorIDInt", DbType.Int64, iSafeguardVendorID);
                db.AddInParameter(dbCom, "@pSafeguardVendorNameVarchar", DbType.String, sSafeguardVendorName);
                db.AddInParameter(dbCom, "@pCountryIDInt", DbType.Int32, iCountryID);
                db.AddInParameter(dbCom, "@pCityIDInt", DbType.Int32, iCityID);

                db.AddInParameter(dbCom, "@pContactNoVarchar", DbType.String, sContactNo);
                db.AddInParameter(dbCom, "@pFaxNoVarchar", DbType.String, sFaxNo);
                db.AddInParameter(dbCom, "@pContactPersonVarchar", DbType.String, sContactPerson);
                db.AddInParameter(dbCom, "@pAddressVarchar", DbType.String, sAddress);
                db.AddInParameter(dbCom, "@pEmailCcVarchar", DbType.String, sEmailCc);
                db.AddInParameter(dbCom, "@pEmailToVarchar", DbType.String, sEmailTo);
                db.AddInParameter(dbCom, "@pWebsiteVarchar", DbType.String, sWebsite);

                db.AddInParameter(dbCom, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCom, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCom, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCom, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCom, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCom, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCom, "@pCreateDate", DbType.DateTime, CreatedDate);

                param = new SqlParameter("@pTblSeaport", dtSeaport);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCom.Parameters.Add(param);

                db.ExecuteNonQuery(dbCom, trans);
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
                if (dbCom != null)
                {
                    dbCom.Dispose();
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
        /// Date Created:   06/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Get service type 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void ServiceTypeGet(Int32 iContractID, Int32 iSafeguardVendorID,
            bool isViewExists, Int16 iLoadType)
        {
            List<ServiceType> listServiceType = new List<ServiceType>();
            List<ContractServiceTypeDuration> listServiceTypeDuration = new List<ContractServiceTypeDuration>();


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtServiceType = null;
            DataTable dtServiceTypeDuration = null;


            try
            {
                dbCom = db.GetStoredProcCommand("uspSafeguardGetServiceType");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pSafeguardIDInt", DbType.Int64, iSafeguardVendorID);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    if (dSet.Tables[0] != null)
                    {
                        dtServiceType = dSet.Tables[0];
                        listServiceType = (from a in dtServiceType.AsEnumerable()
                                           select new ServiceType
                                           {
                                               ServiceTypeID = GlobalCode.Field2Int(a["ServiceTypeID"]),
                                               ServiceTypeName = GlobalCode.Field2String(a["ServiceTypeName"])

                                           }).ToList();
                    }
                    HttpContext.Current.Session["ServiceType"] = listServiceType;
                    if (dSet.Tables[1] != null)
                    {
                        dtServiceTypeDuration = dSet.Tables[1];
                        listServiceTypeDuration = (from a in dtServiceTypeDuration.AsEnumerable()
                                                   select new ContractServiceTypeDuration
                                               {
                                                   ContractSafeguardDurationIDInt = GlobalCode.Field2Int(a["colContractServiceTypeIDInt"]),
                                                   ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                   ServiceTypeID = GlobalCode.Field2Int(a["ServiceTypeID"]),
                                                   ServiceType = GlobalCode.Field2String(a["ServiceTypeName"]),
                                                   From = GlobalCode.Field2Int(a["FromInt"]),
                                                   To = GlobalCode.Field2Int(a["ToInt"]),
                                               }).ToList();

                    }

                    else
                    {
                        if (isViewExists)
                        {
                            if (dSet.Tables[1] != null)
                            {
                                dtServiceTypeDuration = dSet.Tables[1];
                                listServiceTypeDuration = (from a in dtServiceTypeDuration.AsEnumerable()
                                                           select new ContractServiceTypeDuration
                                               {
                                                   ContractSafeguardDurationIDInt = GlobalCode.Field2Int(a["colContractServiceTypeIDInt"]),
                                                   ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                   ServiceTypeID = GlobalCode.Field2Int(a["ServiceTypeID"]),
                                                   ServiceType = GlobalCode.Field2String(a["ServiceTypeName"]),
                                                   From = GlobalCode.Field2Int(a["FromInt"]),
                                                   To = GlobalCode.Field2Int(a["ToInt"]),
                                               }).ToList();
                            }
                        }
                    }
                    HttpContext.Current.Session["ContractSafeguardTypeDuration"] = listServiceTypeDuration;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtServiceType != null)
                {
                    dtServiceType.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 07/Nov/2013
        /// Created By: Marco Abejar
        /// (description) Insert vendor service contract  
        /// --------------------------------------------------
        /// </summary>       
        public static void SafeguardInsertContract(int iContractID, int iSafeguardVendorID,
            string sContractName, string sRemarks, string sDateStart,
            string sDateEnd, string sRCCLPerconnel, string sRCCLDateAccepted,
            string sVendorPersonnel, string sVendorDateAccepted, int iCurrency,
            string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtContractServiceType,
            DataTable dtAttachment, DataTable dtDetails
            )
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspInsertSafeguardContract");

                db.AddInParameter(dbCommand, "@pContractIdInt", DbType.Int32, iContractID);
                db.AddInParameter(dbCommand, "@pSafeguardVendorIDInt", DbType.Int32, iSafeguardVendorID);
                db.AddInParameter(dbCommand, "@pContractNameVarchar", DbType.String, sContractName);
                db.AddInParameter(dbCommand, "@pRemarksVarchar", DbType.String, sRemarks);

                db.AddInParameter(dbCommand, "@pContractDateStartedDate", DbType.Date, GlobalCode.Field2Date(sDateStart));
                db.AddInParameter(dbCommand, "@pContractDateEndDate", DbType.Date, GlobalCode.Field2Date(sDateEnd));

                db.AddInParameter(dbCommand, "@pRCCLPersonnel", DbType.String, sRCCLPerconnel);

                if (sRCCLDateAccepted != "")
                {
                    db.AddInParameter(dbCommand, "@pRCCLAcceptedDate", DbType.Date, sRCCLDateAccepted);
                }
                db.AddInParameter(dbCommand, "@pVendorPersonnel", DbType.String, sVendorPersonnel);
                if (sVendorDateAccepted != "")
                {
                    db.AddInParameter(dbCommand, "@pVendorAcceptedDate", DbType.Date, sVendorDateAccepted);
                }
                db.AddInParameter(dbCommand, "@pCurrencyIDInt", DbType.Int32, iCurrency);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFilename", DbType.String, sFilename);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);

                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GlobalCode.Field2Date(sGMTDate));
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);

                //db.AddInParameter(dbCommand, "@pFileData", DbType.Binary, FileData);
                //db.AddInParameter(dbCommand, "@pDateUploaded", DbType.DateTime, DateUploaded);
                //db.AddOutParameter(dbCommand, "@pID", DbType.Int32, 8);

                SqlParameter param = new SqlParameter("@pTblContractSafeguardService", dtContractServiceType);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractSafeguardAttachments", dtAttachment);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblTempContractSafeguardDetail", dtDetails);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                //Int32 pID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pID"));
                //return pID;
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
                if (dtContractServiceType != null)
                {
                    dtContractServiceType.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 29/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch contract details by branch ID
        /// ----------------------------------------------------------------------        
        /// Date Modified:  08/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void and put it in List
        /// </summary>     
        public static List<ContractSafeguard> GetVendorSafeguardBranchContractByBranchID(string SafeguardBranchID)
        {
            List<ContractSafeguard> contractList = new List<ContractSafeguard>();

            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorSafeguardBranchContractByBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorBranchIdInt", DbType.Int32, Convert.ToInt32(SafeguardBranchID));
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                contractList = (from a in SFDataTable.AsEnumerable()
                                select new ContractSafeguard
                                {
                                    ContractID = GlobalCode.Field2Int(a["cId"]),
                                    SafeguardName = GlobalCode.Field2String(a["BranchName"]),
                                    ContractStatus = GlobalCode.Field2String(a["CONTRACTSTATUS"]),
                                    ContractName = GlobalCode.Field2String(a["CONTRACTNAME"]),
                                    ContractDateStart = GlobalCode.Field2String(a["CONTRACTSTARTDATE"]),
                                    ContractDateEnd = GlobalCode.Field2String(a["CONTRACTENDDATE"]),
                                    Remarks = GlobalCode.Field2String(a["REMARKS"]),
                                    BranchID = GlobalCode.Field2Int(a["BRANCHID"]),
                                    IsActive = GlobalCode.Field2Bool(a["colIsActiveBit"]),
                                    IsCurrent = GlobalCode.Field2Bool(a["IsCurrent"]),
                                    DateCreated = a.Field<DateTime>("DateCreated")
                                }).ToList();

                return contractList;
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
        /// Date Created: 14/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update contract vehicle status
        /// ------------------------------------------------
        /// Date Modified:  26/Sep/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add audit trail fields
        /// </summary>     
        public static void UpdateSafeguardContractFlag(Int32 ContractID, string Username,
             string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVendorSafeguardBranchContractFlag");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   08/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vendor branch maintenance information                 
        /// -------------------------------------------------------------
        /// Date Modified:  25/Sept/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void
        ///                 Put the data in Sessions
        /// -------------------------------------------------------------
        /// Date Modified:  3/Oct/2013
        /// Modified By:    Marco Abejar
        /// (description)   Modify route fields
        /// -------------------------------------------------------------
        /// </summary> 
        public static void GetVendorSafeguardContractByContractID(string contractId, string branchId, Int16 iLoadType)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            //DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand com = null;
            DataSet ds = null;
            DataTable dt = null;
            DataTable dtAirContract = null;
            DataTable dtAirportNotInContract = null;
            DataTable dtSeaContract = null;
            DataTable dtSeaNotInContract = null;
            DataTable dtCapacity = null;
            DataTable dtAttachment = null;
            DataTable dtCurrency = null;
            DataTable dtDetails = null;
            DataTable dtService = null;

            List<ContractSafeguardDetails> list = new List<ContractSafeguardDetails>();

            List<Airport> listAirportContract = new List<Airport>();
            List<Airport> listAirportNotInContract = new List<Airport>();

            List<Seaport> listSeaportContract = new List<Seaport>();
            List<Seaport> listSeaportNotInContract = new List<Seaport>();

            List<VehicleType> listVehicleType = new List<VehicleType>();

            List<ContractServiceTypeDuration> listServiceTypeDuration = new List<ContractServiceTypeDuration>();

            List<VehicleRoute> listRoute = new List<VehicleRoute>();

            List<Currency> listCurrency = new List<Currency>();
            List<ContractSafeguardAttachment> listAttachment = new List<ContractSafeguardAttachment>();

            List<ContractServiceDetailsAmt> listDetails = new List<ContractServiceDetailsAmt>();

            HttpContext.Current.Session["SafeguardVendorDetails"] = list;

            HttpContext.Current.Session["VendorAirportExists"] = listAirportContract;
            HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotInContract;

            HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
            HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;

            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["ContractSafeguardTypeDuration"] = listServiceTypeDuration;

            HttpContext.Current.Session["ContractCurrency"] = listCurrency;
            HttpContext.Current.Session["ContractSafeguardAttachment"] = listAttachment;

            HttpContext.Current.Session["VehicleRoute"] = listRoute;
            HttpContext.Current.Session["ContractServiceDetailsAmt"] = listDetails;

            try
            {
                com = db.GetStoredProcCommand("uspGetVendorSafeguardContractByContractID");
                db.AddInParameter(com, "@pcolContractIdInt", DbType.Int32, contractId);
                db.AddInParameter(com, "@pcolBranchIdInt", DbType.Int32, branchId);
                db.AddInParameter(com, "@pLoadType", DbType.Int16, iLoadType);
                ds = db.ExecuteDataSet(com);
                //dt = VendorTransactionDatebase.ExecuteDataSet(VMDbCommand).Tables[0];
                //return dt;

                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    list = (from a in dt.AsEnumerable()
                            select new ContractSafeguardDetails
                            {
                                ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                BranchID = GlobalCode.Field2Int(a["SafeguardVendorID"]),

                                //VehicleName = a.Field<string>("VehicleVendorName"),
                                CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                CityName = a.Field<string>("City"),
                                CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                CountryName = a.Field<string>("Country"),

                                Address = a.Field<string>("colAddressVarchar"),
                                ContactNo = a.Field<string>("colContactNoVarchar"),
                                ContactPerson = a.Field<string>("colContactPersonVarchar"),

                                EmailCc = a.Field<string>("colEmailCcVarchar"),
                                EmailTo = a.Field<string>("colEmailToVarchar"),

                                FaxNo = a.Field<string>("colFaxNoVarchar"),

                                ContractDateStart = GlobalCode.Field2String(a["colContractDateStartedDate"]),
                                ContractDateEnd = GlobalCode.Field2String(a["colContractDateEndDate"]),
                                ContractName = a.Field<string>("colContractNameVarchar"),
                                ContractStatus = a.Field<string>("colContractStatusVarchar"),

                                CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),

                                RCCLAcceptedDate = GlobalCode.Field2String(a["colRCCLAcceptedDate"]),
                                RCCLPersonnel = a.Field<string>("colRCCLPersonnel"),

                                VendorAcceptedDate = GlobalCode.Field2String(a["colVendorAcceptedDate"]),
                                VendorPersonnel = a.Field<string>("colVendorPersonnel"),

                                Remarks = a.Field<string>("colRemarksVarchar"),

                            }).ToList();
                }

                if (iLoadType == 0)
                {
                    dtAirContract = ds.Tables[1];
                    dtAirportNotInContract = ds.Tables[2];

                    dtSeaContract = ds.Tables[3];
                    dtSeaNotInContract = ds.Tables[4];


                    dtAttachment = ds.Tables[5];
                    dtCurrency = ds.Tables[6];
                    dtDetails = ds.Tables[7];
                    dtService = ds.Tables[8];

                    listAirportContract = (from a in dtAirContract.AsEnumerable()
                                           select new Airport
                                           {
                                               AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                               AirportName = a.Field<string>("Airport")
                                           }).ToList();

                    listAirportNotInContract = (from a in dtAirportNotInContract.AsEnumerable()
                                                select new Airport
                                                {
                                                    AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                    AirportName = a.Field<string>("Airport")
                                                }).ToList();


                    listSeaportContract = (from a in dtSeaContract.AsEnumerable()
                                           select new Seaport
                                           {
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = a.Field<string>("Seaport")
                                           }).ToList();

                    listSeaportNotInContract = (from a in dtSeaNotInContract.AsEnumerable()
                                                select new Seaport
                                                {
                                                    SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                    SeaportName = a.Field<string>("Seaport")
                                                }).ToList();

                    listCurrency = (from a in dtCurrency.AsEnumerable()
                                    select new Currency
                                    {
                                        CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                        CurrencyName = a.Field<string>("Currency")
                                    }).ToList();
                    listAttachment = (from a in dtAttachment.AsEnumerable()
                                      select new ContractSafeguardAttachment
                                      {
                                          FileName = a.Field<string>("colFileNameVarChar"),
                                          FileType = a.Field<string>("colFileTypeVarChar"),
                                          UploadedDate = a.Field<DateTime>("colUploadedDate"),
                                          uploadedFile = a.Field<byte[]>("colContractAttachmentVarBinary"),
                                          AttachmentId = a.Field<int>("colContractAttachmentIdInt")
                                      }).ToList();

                    listDetails = (from a in dtDetails.AsEnumerable()
                                   select new ContractServiceDetailsAmt
                                   {
                                       ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                                       ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                       BranchID = GlobalCode.Field2Int(a["colSafeguardVendorIDInt"]),
                                       ContractServiceDurationID = GlobalCode.Field2Int(a["colContractServiceTypeIDInt"]),
                                       ServiceTypeID = GlobalCode.Field2Int(a["ServiceTypeID"]),
                                       ServiceType = a.Field<string>("ServiceType"),
                                       RateAmount = GlobalCode.Field2Float(a["colRateAmount"]),
                                       Tax = GlobalCode.Field2Float(a["colTaxPercent"]),
                                   }).ToList();

                    listServiceTypeDuration = (from a in dtService.AsEnumerable()
                                               select new ContractServiceTypeDuration
                                               {
                                                   ContractSafeguardDurationIDInt = GlobalCode.Field2Int(a["colContractServiceTypeIDInt"]),
                                                   ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                                   ServiceTypeID = GlobalCode.Field2Int(a["ServiceTypeID"]),
                                                   ServiceType = GlobalCode.Field2String(a["ServiceType"]),
                                                   From = GlobalCode.Field2Int(a["FromInt"]),
                                                   To = GlobalCode.Field2Int(a["ToInt"]),
                                               }).ToList();

                }

                HttpContext.Current.Session["SafeguardVendorDetails"] = list;
                HttpContext.Current.Session["VendorAirportExists"] = listAirportContract;
                HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotInContract;
                HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
                HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;
                HttpContext.Current.Session["ContractSafeguardTypeDuration"] = listServiceTypeDuration;
                HttpContext.Current.Session["ContractCurrency"] = listCurrency;
                HttpContext.Current.Session["ContractSafeguardAttachment"] = listAttachment;
                HttpContext.Current.Session["ContractServiceDetailsAmt"] = listDetails;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtAirContract != null)
                {
                    dtAirContract.Dispose();
                }
                if (dtAirportNotInContract != null)
                {
                    dtAirportNotInContract.Dispose();
                }
                if (dtSeaContract != null)
                {
                    dtSeaContract.Dispose();
                }
                if (dtSeaNotInContract != null)
                {
                    dtSeaNotInContract.Dispose();
                }
                if (dtCapacity != null)
                {
                    dtCapacity.Dispose();
                }
                if (dtAttachment != null)
                {
                    dtAttachment.Dispose();
                }
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                }
                if (dtService != null)
                {
                    dtService.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport of Vendor Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsSeaportGet(Int32 iVendorID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            List<Seaport> listSeaport = new List<Seaport>();
            List<Seaport> listSeaportNotExist = new List<Seaport>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtExists = null;
            DataTable dtNotExists = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspSafeguardVendorsGetSeaport");
                db.AddInParameter(dbCom, "@pSafeguardIdInt", DbType.Int64, iVendorID);
                db.AddInParameter(dbCom, "@pFilterByInt", DbType.Int16, iFilterBy);
                db.AddInParameter(dbCom, "@pSeaportFilter", DbType.String, sFilter);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;

                    if (dSet.Tables[0] != null)
                    {
                        dtExists = dSet.Tables[0];
                        listSeaport = (from a in dtExists.AsEnumerable()
                                       select new Seaport
                                       {
                                           ID = GlobalCode.Field2Int(a["ContractSeaID"]),
                                           SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                           SeaportName = GlobalCode.Field2String(a["Seaport"])

                                       }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtNotExists = dSet.Tables[1];
                        listSeaportNotExist = (from a in dtNotExists.AsEnumerable()
                                               select new Seaport
                                               {
                                                   SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                   SeaportName = GlobalCode.Field2String(a["Seaport"])
                                               }).ToList();
                    }

                    HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;

                        if (dSet.Tables[0] != null)
                        {
                            dtExists = dSet.Tables[0];
                            listSeaport = (from a in dtExists.AsEnumerable()
                                           select new Seaport
                                           {
                                               ID = GlobalCode.Field2Int(a["ContractSeaID"]),
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = GlobalCode.Field2String(a["Seaport"])

                                           }).ToList();
                        }
                        HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    }
                    else
                    {
                        HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;

                        if (dSet.Tables[0] != null)
                        {
                            dtNotExists = dSet.Tables[0];
                            listSeaportNotExist = (from a in dtNotExists.AsEnumerable()
                                                   select new Seaport
                                                   {
                                                       SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                       SeaportName = GlobalCode.Field2String(a["Seaport"])
                                                   }).ToList();
                        }
                        HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtExists != null)
                {
                    dtExists.Dispose();
                }
                if (dtNotExists != null)
                {
                    dtNotExists.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type of company
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsServiceTypeGet(Int32 iContractID, Int32 iSafeguardVendorID,
            bool isViewExists, Int16 iLoadType)
        {

            List<ContractServiceTypeDuration> listServiceTypeDuration = new List<ContractServiceTypeDuration>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtServiceTypeDuration = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspSafeguardVendorsGetServiceType");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pSafeguardVendorIDInt", DbType.Int64, iSafeguardVendorID);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["ContractServiceTypeDuration"] = listServiceTypeDuration;

                    if (dSet.Tables[1] != null)
                    {
                        dtServiceTypeDuration = dSet.Tables[1];
                        listServiceTypeDuration = (from a in dtServiceTypeDuration.AsEnumerable()
                                                   select new ContractServiceTypeDuration
                                                   {
                                                       ContractSafeguardDurationIDInt = GlobalCode.Field2Int(a["ContractSafeguardDurationIDInt"]),
                                                       ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                       ServiceType = GlobalCode.Field2String(a["ServiceType"]),
                                                       From = GlobalCode.Field2Int(a["FromInt"]),
                                                       To = GlobalCode.Field2Int(a["ToInt"]),
                                                   }).ToList();
                    }
                    HttpContext.Current.Session["ContractServiceTypeDuration"] = listServiceTypeDuration;
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["ContractServiceTypeDuration"] = listServiceTypeDuration;

                        if (dSet.Tables[0] != null)
                        {
                            listServiceTypeDuration = (from a in dtServiceTypeDuration.AsEnumerable()
                                                       select new ContractServiceTypeDuration
                                                       {
                                                           ContractSafeguardDurationIDInt = GlobalCode.Field2Int(a["ContractSafeguardDurationIDInt"]),
                                                           ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                           ServiceType = GlobalCode.Field2String(a["ServiceType"]),
                                                           From = GlobalCode.Field2Int(a["FromInt"]),
                                                           To = GlobalCode.Field2Int(a["ToInt"]),
                                                       }).ToList();
                        }
                        HttpContext.Current.Session["ContractServiceTypeDuration"] = listServiceTypeDuration;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtServiceTypeDuration != null)
                {
                    dtServiceTypeDuration.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 12/Nov/2013
        /// Created By: Marco Abejar
        /// (description) Selecting list of safeguard branch contract pending
        /// </summary>     
        public static DataTable GetVendorSafeguardBranchPendingContract()
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorSafeguardBranchContractPending");
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
        /// Date Created: 12/Nov/2013
        /// Created By: Marco Abejar
        /// (description) Selecting safeguard contract if it's live
        /// </summary> 
        public static Int32 GetVendorSafeguardBranchContractActiveByContractID(Int32 cID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            Int32 count = 0;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorSafeguardBranchContractActiveByContractID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractIdInt", DbType.Int32, cID);
                count = (Int32)SFDatebase.ExecuteScalar(SFDbCommand);
                return count;
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
        /// Date Created: 12/Nov/2013
        /// Created By: Marco Abejar
        /// (description) Update contract safeguard status
        /// </summary>     
        public static void UpdateSafeguardContractStatus(Int32 ContractID, string Username)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVendorSafeguardBranchContractStatus");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
            }
        }

        /// <summary>
        /// Date created:   07/31/2012
        /// Created by:     Jefferson Bermundo
        /// Description:    Get the list of the hotel Attachments.
        /// </summary>
        /// <param name="contractId">selected contract id</param>
        /// <returns>return model list for contract hotel attachments</returns>
        public static List<ContractSafeguardAttachment> GetSafeguardContractAttachment(int contractId)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            List<ContractSafeguardAttachment> contractHotelAttachment = new List<ContractSafeguardAttachment>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSafeguardContractAttachment");
                SFDatebase.AddInParameter(SFDbCommand, "@pContractID", DbType.Int32, contractId);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                contractHotelAttachment = (from list in SFDataTable.AsEnumerable()
                                           select new ContractSafeguardAttachment
                                           {
                                               FileName = list.Field<string>("colFileNameVarChar"),
                                               FileType = list.Field<string>("colFileTypeVarChar"),
                                               UploadedDate = list.Field<DateTime>("colUploadedDate"),
                                               uploadedFile = list.Field<byte[]>("colContractAttachmentVarBinary"),
                                               AttachmentId = list.Field<int>("colContractAttachmentIdInt")
                                           }).ToList();
                return contractHotelAttachment;

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
        /// Date Created: 13/11/2013
        /// Created By: Marco Abejar
        /// (description) Selecting safeguard contract attachment       
        /// </summary>  
        //public static DataTable GetSafeguardContractAttachment(Int32 ContractID, Int32 BranchID)
        //{

        //    Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand SFDbCommand = null;
        //    DataTable SFDataTable = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSafeguardContractAttachment");
        //        SFDatebase.AddInParameter(SFDbCommand, "pContractID", DbType.Int32, ContractID);
        //        SFDatebase.AddInParameter(SFDbCommand, "pcolBranchIDInt", DbType.Int32, BranchID);
        //        SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
        //        return SFDataTable;
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
        //        if (SFDataTable != null)
        //        {
        //            SFDataTable.Dispose();
        //        }
        //    }
        //}
    }
}

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
    public class PortDAL
    {

        #region Methods

        /// <summary>            
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get port list
        /// ---------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>    
        public static DataTable GetPortList()
        {                    
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable PortDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortList");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                PortDataTable = new DataTable();
                PortDataTable.Load(dataReader);
                return PortDataTable;
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
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }               
            }
        }
        /// <summary>
        /// Date Created:   18/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Get port list by Region and/or Country
        /// ------------------------
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ------------------------------------------------
        /// Date Modified:   15/08/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter userRoleString
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="regionString"></param>
        /// <param name="countryString"></param>
        /// <returns></returns>
        public static List<PortList> GetPortListByRegionCountry(string userString, string regionString, string countryString, string userRoleString)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            DataTable PortDataTable = null;
            List<PortList> list = new List<PortList>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortListByRegionCountry");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(regionString));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, Int32.Parse(countryString));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserRoleVarchar", DbType.String, userRoleString);

                //dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                PortDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in PortDataTable.AsEnumerable()
                        select new PortList
                        {
                            PortId = GlobalCode.Field2Int(a["PORTID"]),
                            PortName = a["PORT"].ToString()
                        }).ToList();
                return list;
                //PortDataTable.Load(dataReader);
                //return PortDataTable;
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
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
                //if (dataReader != null)
                //{
                //    dataReader.Close();
                //    dataReader.Dispose();
                //}
            }
        }
        /// <summary>
        /// Date Created:   19/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Get port list by Region
        /// ------------------------
        /// </summary>
        public static List<PortList> GetPortListByRegion(string userString, string regionString, string userRoleString, string sPortName)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            DataTable PortDataTable = null;
            List<PortList> list = new List<PortList>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortListByRegion");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(regionString));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserRoleVarchar", DbType.String, userRoleString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeaportName", DbType.String, sPortName);


                PortDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in PortDataTable.AsEnumerable()
                        select new PortList
                        {
                            PortId = GlobalCode.Field2Int(a["PORTID"]),
                            PortName = a["PORT"].ToString()
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
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   26/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get port list by City
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public static DataTable GetPortListByCity(string userString, string cityString)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable PortDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortListByCity");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(cityString));

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                PortDataTable = new DataTable();
                PortDataTable.Load(dataReader);
                return PortDataTable;
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
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
       /// <summary>       
       /// Date Created:   11/07/2011
       /// Created By:     Marco Abejar
       /// (description)   Get seafarer port transaction details
       /// ---------------------------------------------------
       /// Date Modified:  02/08/2011
       /// Modified By:    Josephine Gad
       /// (description)   Close IDataReader and DataTable     
       /// ---------------------------------------------------
       /// Date Modified:  12/10/2011
       /// Modified By:    Josephine Gad
       /// (description)   Add parameter TravelReq, Rec Loc, and Manual Req ID
       /// ---------------------------------------------------
       /// Date Modified:  18/10/2011
       /// Modified By:    Josephine Gad
       /// (description)   Remove rec loc parameter
       /// -----------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
       /// </summary>
       /// <param name="sfCode"></param>
       /// <param name="sfPID"></param>
       /// <param name="sfStatus"></param>
       /// <returns></returns>
       ///         
        public static IDataReader GetSFPortTransDetails(string sfCode, string sfPID, string sfStatus,
            string TravelReqId, string ManualRequestID)
        {                       
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFPortTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSeafarerIdInt", DbType.Int32, Int32.Parse(sfCode));
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortAgentTransIdBigint", DbType.Int64, Int64.Parse(sfPID));
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                if (TravelReqId != "0")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int64, Int64.Parse(TravelReqId));
                    //SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLocator);
                }
                if (ManualRequestID != "0")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, Int32.Parse(ManualRequestID));
                }
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
        /// Date Created:   18/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Insert/update port travel status
        /// ===============================================
        /// Date Modified:  12/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter Travel Request ID, ManualRequestID and others
        /// </summary>
        /// 
        //public static void InsertUpdatePortStatus(string sfCode, string PortTransStatus, string PortTransDate, string SFStatus, string PortID, string SFPID, string SFRecLoc)
        public static Boolean InsertUpdatePortStatus(string PortAgentTransId, string TravelReqId,
            string ManualRequestID, string SeafarerId, string PortAgentId,
            string PortId, string PortAgentStatus, string UserName, string SFStatus, string PortTransactionDate, string ContractId)    
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DbConnection dbConnection = SFDatebase.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                DateTime currentDatetime = DateTime.Now;
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertPortTravelStatus");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentTransIdBigint", DbType.Int64, Int64.Parse(PortAgentTransId));
                if (TravelReqId != "0")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int64, Int64.Parse(TravelReqId));
                    //SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLocator);
                }
                if (ManualRequestID != "0")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int64, Int64.Parse(ManualRequestID));
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerIdInt", DbType.Int32, Int32.Parse(SeafarerId));

                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentIdInt", DbType.Int32, Int32.Parse(PortAgentId));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentStatusVarchar", DbType.String, PortAgentStatus);

                SFDatebase.AddInParameter(SFDbCommand, "@pDatetime", DbType.DateTime, currentDatetime);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserName", DbType.String, UserName);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, SFStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortTransactionDate", DbType.DateTime, DateTime.Parse(PortTransactionDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pContractId", DbType.String, ContractId);
                SFDatebase.ExecuteNonQuery(SFDbCommand, dbTrans);
                dbTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                return false;
            }
            finally
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }                
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Delete Port Transaction        
        /// </summary>
        /// 
        public static void DeletePortTransaction(Int32 colPortAgentTransIdInt, string DeletedBy)
        {           
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;            
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeletePortTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortAgentTransIdInt", DbType.Int32, colPortAgentTransIdInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolModifiedByVarchar", DbType.String, DeletedBy);                
                SFDatebase.ExecuteNonQuery(SFDbCommand).ToString();
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
        /// Date Created: 01/08/2011
        /// Created By: Marco Abejar
        /// (description) Get port details to edit
        /// ---------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// -----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static IDataReader GetPortToEdit(Int32 PortID)
        {                      
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortToEdit");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortIdInt", DbType.Int32, PortID);
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
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Add new port
        /// </summary>
        /// 
        public static Int32 AddNewPort(string PortName, Int32? PortCity, Int32 PortCountry, string CreatedBy, Int32 PortID, string PortCode)
        {           
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();          
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspAddNewPort");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortNameVarchar", DbType.String, PortName);
                if (PortCity != 0)
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pCityIDint", DbType.Int32, PortCity);
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDint", DbType.Int32, PortCountry);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortIdInt", DbType.Int32, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddOutParameter(SFDbCommand, "@PortID", DbType.Int32, 8);
                SFDatebase.ExecuteNonQuery(SFDbCommand).ToString();
                Int32 pPortID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@PortID"));
                return pPortID;
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
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Get port company list          
        /// </summary>     
        public static DataTable GetPortCompanyList()
        {                   
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable PortDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortCompanyList");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                PortDataTable = new DataTable();
                PortDataTable.Load(dataReader);
                return PortDataTable;
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
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>                        
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Get port company details to edit  
        /// -----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>     
        public static IDataReader GetPortCompanyToEdit(Int32 PortCompanyID)
        {                 
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortCompanyToEdit");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortAgentCompanyIdInt", DbType.Int32, PortCompanyID);
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
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Add new port company
        /// </summary>
        /// 
        public static Int32 AddNewPortCompany(Int32 PortCompanyID, String CompanyName, String Address, Int32 CountryID, String ContactPerson, String ContactNo, String User)
        {            
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspAddNewPortCompany");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortAgentCompanyIdInt", DbType.Int32, PortCompanyID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolCompanyNameVarchar", DbType.String, CompanyName);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolAddressVarchar", DbType.String, Address);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolCountryIDInt", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContactPerson", DbType.String, ContactPerson);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolMainContactNumberVarchar", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedBy", DbType.String, User);
                SFDatebase.AddOutParameter(SFDbCommand, "@PortAgentCompanyID", DbType.Int32, 8);
                SFDatebase.ExecuteNonQuery(SFDbCommand).ToString();
                Int32 PortAgentCompanyID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@PortAgentCompanyID"));
                return PortAgentCompanyID;
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
        /// Date Created:   31/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Add new Service Provider
        /// ------------------------------------
        /// Date Modified:  12/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameters, Change uspAddNewPortCompany to uspAddNewPortAgent
        /// </summary>
        /// 
        public static Int32 AddNewPortAgent(string PortAgentIdInt, string PortAgentName, string PortCompanyID, String LName, String FName, String MName, String Address, 
            String ContactNo, String Email, String ContactPerson, String ContactPersonPhone,
            String ContactPersonEmail, string UserString)
        {           
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            try
            {
                PortAgentIdInt = (PortAgentIdInt == "" ? "0" : PortAgentIdInt);
                PortCompanyID = (PortCompanyID == "" ? "0" : PortCompanyID);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspAddNewPortAgent");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentIdInt", DbType.Int32, Int32.Parse(PortAgentIdInt));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentName", DbType.String, PortAgentName);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentCompanyIdInt", DbType.Int32, Int32.Parse(PortCompanyID));
                SFDatebase.AddInParameter(SFDbCommand, "@pFNameVarchar", DbType.String, FName);
                SFDatebase.AddInParameter(SFDbCommand, "@pLNameVarchar", DbType.String, LName);
                SFDatebase.AddInParameter(SFDbCommand, "@pMNameVarchar", DbType.String, MName);

                SFDatebase.AddInParameter(SFDbCommand, "@pMainAddressVarchar", DbType.String, Address);
                SFDatebase.AddInParameter(SFDbCommand, "@pMainContactNoVarchar", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pMainEmailAddressVarchar", DbType.String, Email);

                SFDatebase.AddInParameter(SFDbCommand, "@pAlternateContactPersonVarchar", DbType.String, ContactPerson);
                SFDatebase.AddInParameter(SFDbCommand, "@pAlternateContactNumberVarchar", DbType.String, ContactPersonPhone);
                SFDatebase.AddInParameter(SFDbCommand, "@pAlternateContactEmailVarchar", DbType.String, ContactPersonEmail);

                SFDatebase.AddInParameter(SFDbCommand, "@pDateCreatedDatetime", DbType.DateTime, DateTime.Now);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.Now);

                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedByVarchar", DbType.String, UserString);
                SFDatebase.AddInParameter(SFDbCommand, "@pModifiedByVarchar", DbType.String, UserString);
                SFDatebase.AddOutParameter(SFDbCommand, "@PortAgentID", DbType.Int32, 8);

                SFDatebase.ExecuteNonQuery(SFDbCommand).ToString();

                Int32 PortAgentID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@PortAgentID"));
                return PortAgentID;
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
        /// Date Created:   05/10/2011
        /// Created By:     Josephine gad
        /// (description)   Get list of Service Provider by port id
        /// ------------------------------------        
        /// Date Modified:  11/11/2011
        /// Modified By:    Josephine gad
        /// (description)   Add Service Provider ID parameter
        /// ------------------------------------
        /// Date Modified:  14/02/2012
        /// Modified By:    Josephine gad
        /// (description)   Change return value from DataTable to List
        /// ------------------------------------
        /// </summary>
        /// <param name="PortID"></param>
        /// <returns></returns>
        //public static DataTable GetPortAgentByPortID(string PortID, string PortAgentID)
        public static List<PortAgentDTO> GetPortAgentByPortID(string PortID, string PortAgentID)
        {            
            DataTable dt = null;
            DbCommand command = null;
            List<PortAgentDTO> portAgentList = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetPortAgentByPortID");
                db.AddInParameter(command, "@pPortIDInt", DbType.String, PortID);
                db.AddInParameter(command, "@pPortAgentID", DbType.String, PortAgentID);
                dt = db.ExecuteDataSet(command).Tables[0];

                portAgentList = (from a in dt.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]).ToString(),
                                     PortAgentName = a["PortAgenName"].ToString()
                                 }).ToList();


                return portAgentList;
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
                if (portAgentList != null)
                {
                    portAgentList = null;
                }
            }
        }        
        /// <summary>            
        /// Date Created: 20/10/2011
        /// Created By: Charlene Remotigue
        /// (description) load Service Provider details
        /// </summary>     
        public static IDataReader GetPortAgentDetails(string portAgentId)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortAgentDetails");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentId", DbType.Int32, Convert.ToInt32(portAgentId));
                IDataReader dr = SFDatebase.ExecuteReader(SFDbCommand);
                return dr;
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
        /// Author: Charlene Remotigue
        /// Date Created: 02/11/2011
        /// Description: get Vendor brand with no contracts by port 
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="VendorType"></param>
        /// <returns></returns>
       public static DataTable getVendorBrandByPort(string portAgentId, string VendorType, string ContractId, string contractBranchId)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DataTable VendorBrandDataTable = null;
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorBrandWithNoContracts");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, Int32.Parse(portAgentId));
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, Int32.Parse(ContractId));
                db.AddInParameter(dbCommand, "@pVendorType", DbType.Int32, Int32.Parse(VendorType));
                db.AddInParameter(dbCommand, "@pcontractBranchId", DbType.Int32, Int32.Parse(contractBranchId));
                VendorBrandDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return VendorBrandDataTable;
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
                if (VendorBrandDataTable != null)
                {
                    VendorBrandDataTable.Dispose();
                }
            }

        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 02/11/2011
        /// Deascription: get vendor branch with no contracts by vendor brand Id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="VendorType"></param>
        /// <returns></returns>
        public static DataTable getVendorBranchbyVendorBrand(string VendorId, string VendorType, string contractBranchId, string ContractId)
        {
            DataTable vendorBranchDataTable = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorBranchWithNoContracts");
                db.AddInParameter(dbCommand, "@pVendorType", DbType.Int32, Int32.Parse(VendorType));
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, Int32.Parse(VendorId));
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, Int32.Parse(ContractId));
                db.AddInParameter(dbCommand, "@pcontractBranchId", DbType.Int32, Int32.Parse(contractBranchId));
                vendorBranchDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return vendorBranchDataTable;
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
                if (vendorBranchDataTable != null)
                {
                    vendorBranchDataTable.Dispose();
                }
            }
        }

       

       ///// <summary>
       ///// Author:Charlene Remotigue
       ///// Date Created" 03/11/2011
       ///// Description: get added vendor brand
       ///// </summary>
       ///// <param name="ContractId"></param>
       ///// <param name="VendorType"></param>
       ///// <param name="UserId"></param>
       ///// <returns></returns>
       // public static DataTable getAddedVendorBrand(string ContractId, int VendorType, string UserId)
       // {
       //     Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
       //     DataTable VendorBrandDataTable = null;
       //     DbCommand dbCommand = null;
       //     try
       //     {
       //         dbCommand = db.GetStoredProcCommand("uspGetAddedVendorBrand");
       //         db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, Int32.Parse(ContractId));
       //         db.AddInParameter(dbCommand, "@pVendorType", DbType.Int32, VendorType);
       //         db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
       //         VendorBrandDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
       //         return VendorBrandDataTable;
       //     }
       //     catch (Exception ex)
       //     {
       //         throw ex;
       //     }
       //     finally
       //     {
       //         if (dbCommand != null)
       //         {
       //             dbCommand.Dispose();
       //         }
       //         if (VendorBrandDataTable != null)
       //         {
       //             VendorBrandDataTable.Dispose();
       //         }
       //     }

       // }

        ///// <summary>
        ///// Author: Charlene Remotigue
        ///// Date Created: 03/11/2011
        ///// Description: get added vendor Branch
        ///// </summary>
        ///// <param name="VendorBrand"></param>
        ///// <param name="ContractId"></param>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //public static DataTable getAddedVendorBranch(string VendorBrand, string ContractId, string UserId)
        //{
        //    DataTable vendorBranchDataTable = null;
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspGetAddedVendorBranch");
        //        db.AddInParameter(dbCommand, "@pVendorBrand", DbType.Int32, Int32.Parse(VendorBrand));
        //        db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, Int32.Parse(ContractId));
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        vendorBranchDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
        //        return vendorBranchDataTable;
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
        //        if (vendorBranchDataTable != null)
        //        {
        //            vendorBranchDataTable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Date Edited:  19/07/2012
        /// Created By:   Jefferson Bermundo
        /// (description) Check Port Code If Existing.                 
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public static bool CheckPortCode(string portCode, int portId)
        {
            DataTable vendorBranchDataTable = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspCheckPortCode");
                db.AddInParameter(dbCommand, "@portCode", DbType.String, portCode);
                db.AddInParameter(dbCommand, "@portId", DbType.Int32, portId);
                vendorBranchDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                if (vendorBranchDataTable.Rows.Count > 0) { return false; }
                return true;
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
                if (vendorBranchDataTable != null)
                {
                    vendorBranchDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 13/Mar/2013
        /// Created By:   Josephine Gad
        /// (description) Get list of Ports to be used in Non Turn Port
        ///               Get the count of Travel Request in Non Turn Port
        /// </summary>
        /// <param name="sPortName"></param>
        /// <param name="sPortCode"></param>
        /// <param name="sPortID"></param>
        /// <param name="dDate"></param>
        public static void GetPortForNonTurn(string sPortName, string sPortCode, string sPortID, DateTime dDate )
        {
            List<PortList> list = new List<PortList>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
            DataSet ds = null;
            HttpContext.Current.Session["NonTurnPortCount"] = 0;
            HttpContext.Current.Session["GetPortForNonTurn"] = null;
            try
            {
                comm = db.GetStoredProcCommand("uspGetPortListForNonTurn");
                db.AddInParameter(comm, "@pPortName", DbType.String, sPortName);
                db.AddInParameter(comm, "@pPortCode", DbType.String, sPortCode);
                db.AddInParameter(comm, "@pPortID", DbType.Int32, GlobalCode.Field2Int(sPortID));
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(dDate));

                ds = db.ExecuteDataSet(comm);
                dt = ds.Tables[1];
                list = (from a in dt.AsEnumerable()
                        select new PortList { 
                            PortId = a.Field<Int64>("PortID"),
                            PortName = a.Field<string>("PortName"),
                        
                        }).ToList();

                HttpContext.Current.Session["NonTurnPortCount"] = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());
                HttpContext.Current.Session["PortForNonTurn"] = list;

                
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   04/Apr/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Port with incompete details in TM
        /// </summary>      
        public static void GetPortForNotInTM(string sPortName, string sPortCode, DateTime dDate)
        {
            List<PortNotExistList> list = new List<PortNotExistList>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
            DataSet ds = null;
            HttpContext.Current.Session["PortNotExistList"] = list;
            try
            {
                comm = db.GetStoredProcCommand("uspGetPortListForNonTurnNoInTM");
                db.AddInParameter(comm, "@pPortName", DbType.String, sPortName);
                db.AddInParameter(comm, "@pPortCode", DbType.String, sPortCode);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(dDate));

                ds = db.ExecuteDataSet(comm);
                dt = ds.Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new PortNotExistList
                        {
                            PortCode = a.Field<string>("PortCode"),                            
                            PortName = a.Field<string>("PortName"),
                        }).ToList();

                HttpContext.Current.Session["PortNotExistList"] = list;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// Date Modified:   15/08/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter userRoleString
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="regionString"></param>
        /// <param name="countryString"></param>
        /// <returns></returns>
        public static List<PortAgentVendorList> GetPortAgentVendor(int vendorid)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            DataTable PortDataTable = null;
            List<PortAgentVendorList> list = new List<PortAgentVendorList>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortAgentVendor");
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorId", DbType.Int32, vendorid);

                PortDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in PortDataTable.AsEnumerable()
                        select new PortAgentVendorList
                        {
                            PortAgentVendorId = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                            PortAgentVendorName = a["colPortAgentVendorNameVarchar"].ToString()
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
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
                //if (dataReader != null)
                //{
                //    dataReader.Close();
                //    dataReader.Dispose();
                //}
            }
        }
        #endregion
    }
}

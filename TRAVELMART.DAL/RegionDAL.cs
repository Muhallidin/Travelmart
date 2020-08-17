using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlClient;
using System.Globalization;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class RegionDAL
    {
        /// <summary>
        /// Date Created:   27/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Get seaport list
        /// -------------------------------    
        /// Date Modified:  06/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add paremeters RegionID and IsViewExist
        /// -------------------------------  
        /// </summary>        
        public static List<Seaport> GetSeaport(int RegionID, bool IsViewExist)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            List<Seaport> list = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSeaport");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionID", DbType.Int32, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsViewExist", DbType.Boolean, IsViewExist);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new Seaport
                        {
                            SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                            SeaportName = a.Field<string>("SeaportName")
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   04/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport not exists in Region
        /// -------------------------------  
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static List<RegionSeaportNotExists> GetSeaportNotExistsInRegion(int RegionID, int ContinentID, int CountryID, string PortName)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            List<RegionSeaportNotExists> list = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSeaport");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionID", DbType.Int32, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsViewExist", DbType.Boolean, false);
                SFDatebase.AddInParameter(SFDbCommand, "@pContinent", DbType.Int32, ContinentID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountry", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortName", DbType.String, PortName);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new RegionSeaportNotExists
                        {
                            RegionSeaportID = 0,
                            SeaportID = GlobalCode.Field2TinyInt(a["SeaportID"]),
                            SeaportName = a.Field<string>("SeaportName"),
                            CountryID = GlobalCode.Field2TinyInt(a["CountryID"])
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   27/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Get region seaport      
        /// ------------------------------------
        /// Date Modified:  07/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter CountryID and PortName
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static List<RegionSeaport> GetRegionSeaport(string RegionID, string CountryID, string PortName)
        {
            DataTable dt = null;
            DbCommand command = null;
            List<RegionSeaport> list = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectRegionSeaport");
                db.AddInParameter(command, "@pRegionIDInt", DbType.Int16, Int32.Parse(RegionID));
                db.AddInParameter(command, "@pCountryIDInt", DbType.Int16, Int32.Parse(RegionID));
                db.AddInParameter(command, "@pPortName", DbType.String, PortName);
                dt = db.ExecuteDataSet(command).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new RegionSeaport
                        {
                            RegionSeaportID = GlobalCode.Field2Int(a["RegionSeaportID"]),
                            RegionID = GlobalCode.Field2TinyInt(a["RegionID"]),
                            SeaportID = GlobalCode.Field2TinyInt(a["SeaportID"]),
                            SeaportName = a.Field<string>("SeaportName"),
                            CountryID = GlobalCode.Field2TinyInt(a["CountryID"])
                        }).ToList();
                return list;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }

        /// <summary>            
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Delete region seaport (flagged as inactive)
        /// </summary>
        public static void DeleteRegionSeaport(string RegionSeaportID, string DeletedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteRegionSeaport");

                db.AddInParameter(dbCommand, "@pRegionSeaportID", DbType.Int32, Int32.Parse(RegionSeaportID));
                db.AddInParameter(dbCommand, "@pDeletedBy", DbType.String, DeletedBy);

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
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Insert region seaport
        /// </summary>
        public static Int32 InsertRegionSeaport(string RegionSeaportID, string RegionID, string SeaportID,
            string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertRegionSeaport");

                db.AddInParameter(dbCommand, "@pRegionSeaportID", DbType.Int32, Int32.Parse(RegionSeaportID));
                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, Int32.Parse(RegionID));
                db.AddInParameter(dbCommand, "@pSeaportID", DbType.Int32, Int32.Parse(SeaportID));                
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@pRegionSeaportIDInt", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 pRegionSeaportID = GlobalCode.Field2Int(db.GetParameterValue(dbCommand, "@pRegionSeaportIDInt"));
                return pRegionSeaportID;
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
        /// Date Created: 28/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Delete region (flagged as inactive)
        /// </summary>
        public static void DeleteRegion(Int32 RegionID, string DeletedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspRegion_Delete");

                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pDeletedBy", DbType.String, DeletedBy);

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
        /// Date Created:   04/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Continent list
        /// -------------------------------  
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static List<Continent> GetContinent()
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            List<Continent> list = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetContinent");                
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new Continent
                        {
                            ContinentID = GlobalCode.Field2TinyInt(a["colMapIDInt"]),
                            ContinentName = a.Field<string>("colMapNameVarchar")
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: Load Region Seaport page
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public static List<RegionGenericClass> LoadRegionPage(int RegionId, string strLogDescription, string strFunction, string PathName,
                DateTime GMTDate, DateTime DateNow, string UserName)
        {
            List<RegionGenericClass> overflow = new List<RegionGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtRegionSeaport = null;
            DataTable dtContinent = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspLoadRegionSeaportPage");
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionId);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPathName", DbType.String, PathName);
                db.AddInParameter(dbCommand, "@pGMTDate", DbType.DateTime, GMTDate);
                db.AddInParameter(dbCommand, "@pDateNow", DbType.DateTime, DateNow);
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, UserName);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);
                ds = db.ExecuteDataSet(dbCommand);
                dtContinent = ds.Tables[0];
                dtRegionSeaport = ds.Tables[1];

                overflow.Add(new RegionGenericClass()
                {
                    ContinentList = (from a in dtContinent.AsEnumerable()
                              select new Continent
                              {
                                  ContinentID = a.Field<int>("colMapIDInt"),
                                  ContinentName = a.Field<string>("colMapNameVarchar"),
                              }).ToList(),
                    RegionSeaportList = (from a in dtRegionSeaport.AsEnumerable()
                                        select new RegionSeaport
                                        {
                                            RegionSeaportID = a.Field<Int64>("RegionSeaportID"),
                                            RegionID = a.Field<int>("RegionID"),
                                            CountryID = a.Field<int>("CountryID"),
                                            SeaportID = a.Field<int>("SeaportID"),
                                            SeaportName = a.Field<string>("SeaportName"),
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
                if (dtContinent != null)
                {
                    dtContinent.Dispose();
                }
                if (dtRegionSeaport != null)
                {
                    dtRegionSeaport.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
    }
}

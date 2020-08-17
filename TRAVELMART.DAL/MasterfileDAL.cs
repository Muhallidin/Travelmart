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
using System.Configuration;

namespace TRAVELMART.DAL
{
    public class MasterfileDAL
    {

        #region Country DAL
        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) View list of countries by region (used in CountryView)
        /// </summary>
        public DataTable MasterfileCountryViewByRole(string RoleID, string CountryName, Int32 startRowIndex, Int32 maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspViewCountryList");                
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, RoleID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryName", DbType.String, CountryName);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                CountryDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return CountryDataTable;
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
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Count country 
        /// </summary>
        public Int32 MasterfileCountryViewByRoleCount(string RoleID, string CountryName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int maximumRows = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspViewCountryListCount");                
                db.AddInParameter(dbCommand, "@pRole", DbType.String, RoleID);
                db.AddInParameter(dbCommand, "@pCountryName", DbType.String, CountryName);
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        maximumRows = Convert.ToInt32(dr["maximumRows"]);
                    }
                }

                return maximumRows;
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
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Delete country
        /// </summary>
        public void MasterfileCountryDelete(int CountryId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspViewCountryList_DeletePerCountry");
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Country
        /// --------------------------------
        /// Date Modified: 12/10/2011
        /// Modified By: Charlene Remotigue
        /// Description: added paging
        /// </summary>
        /// 
        public DataTable GetSearchCountryListByRegion(string RegionID, string CountryName, Int32 startRowIndex, Int32 maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSearchCountryList");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionId", DbType.String, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryName", DbType.String, CountryName);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                CountryDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return CountryDataTable;
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
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Country 
        /// </summary>
        public Int32 GetSearchCountryListByRegionCount(string RegionID, string CountryName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int maximumRows = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSearchCountryListCount");
                db.AddInParameter(dbCommand, "@pRegionId", DbType.String, RegionID);
                db.AddInParameter(dbCommand, "@pCountryName", DbType.String, CountryName);
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        maximumRows = Convert.ToInt32(dr["maximumRows"]);
                    }
                }

                return maximumRows;
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
            }
        }
               

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Add new Country
        /// </summary>
        public static Int32 countryAddMasterfileInsert(string countryCode, string CountryName, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertCountry");
                //db.AddInParameter(dbCommand, "@pMapId", DbType.Int32, regionId);
                db.AddInParameter(dbCommand, "@pCountryCode", DbType.String, countryCode);
                db.AddInParameter(dbCommand, "@pCountryName", DbType.String, CountryName);
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@pCountryID", DbType.Int32, 8);
                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                Int32 CountryID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pCountryID"));
                return CountryID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();                
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Update country
        /// </summary>
        /// 
        public static void countryAddMasterfileUpdate(int regionId, int countryId, string countryName, string countryCode, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateCountry");

                db.AddInParameter(dbCommand, "@pRegionId", DbType.Int32, regionId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, countryId);
                db.AddInParameter(dbCommand, "@pCountryName", DbType.String, countryName);
                db.AddInParameter(dbCommand, "@pCountryCode", DbType.String, countryCode);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete country
        /// </summary>
        public void countryViewMasterfileDelete(int CountryId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteCountry");
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
       
        #endregion

        #region Region DAL
        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) View list of regions
        /// ---------------------------------------
        /// Date Modified:   06/Feb/2014
        /// Modified By:     Josephine Gad
        /// (description)    Add Count of Rows and store in Session
        /// </summary>
        public DataTable MasterfileRegionView(string regionName, Int32 startRowIndex, Int32 maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            DataSet ds = null;
            DataTable RegionDataTable = null;
            DataTable dtCount = null;
            try
            {
                if (regionName == null)
                { regionName = ""; }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspViewRegionList");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionName", DbType.String, regionName);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                RegionDataTable = ds.Tables[0];
                dtCount = ds.Tables[1];

                int iTotal = GlobalCode.Field2Int(dtCount.Rows[0][0]);
                HttpContext.Current.Session["RegionViewTotalCount"] = iTotal;

                return RegionDataTable;
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
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
                if (dtCount != null)
                {
                    dtCount.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Count list of regions
        /// </summary>
        //public Int32 MasterfileRegionViewCount(string regionName)
        //{
        //    Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand SFDbCommand = null;
        //    int Region = 0;
        //    try
        //    {
        //        if (regionName == null)
        //        { regionName = ""; }
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspViewRegionListCount");
        //        SFDatebase.AddInParameter(SFDbCommand, "@pRegionName", DbType.String, regionName);
        //        using (IDataReader dr = SFDatebase.ExecuteReader(SFDbCommand))
        //        {
        //            if (dr.Read())
        //            {
        //                Region = Convert.ToInt32(dr["maximumRows"]);
        //            }
        //        }

        //        return Region;
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

        //    }
        //}        

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Delete region
        /// </summary>
        public static void MasterfileRegionDelete(int regionId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspViewRegionList_DeletePerRegion");
                db.AddInParameter(dbCommand, "@pRegionId", DbType.Int32, regionId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Insert region
        /// </summary>
        public static Int32 MasterfileRegionInsert(string regionName, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspRegion_Insert");

                db.AddInParameter(dbCommand, "@pRegionName", DbType.String, regionName);
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@pRegionID", DbType.Int32, 8);
                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                Int32 RegionID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pRegionID"));
                return RegionID;
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
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Update region
        /// </summary>
        /// 
        public static void MasterfileRegionUpdate(int regionId, string regionName, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspRegion_Update");

                db.AddInParameter(dbCommand, "@pRegionId", DbType.Int32, regionId);
                db.AddInParameter(dbCommand, "@pRegionName", DbType.String, regionName);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
        /// Date Created:19/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count region
        /// </summary>
        public Int32 regionViewMasterfileSearchCount(string regionName)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            int Region = 0;
            try
            {
                if (regionName == null)
                { regionName = ""; }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSearchMapListCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionName", DbType.String, regionName);
                using (IDataReader dr = SFDatebase.ExecuteReader(SFDbCommand))
                {
                    if (dr.Read())
                    {
                        Region = Convert.ToInt32(dr["maximumRows"]);
                    }
                }
               
                return Region;
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
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Search for region
        /// </summary>
        public DataTable regionViewMasterfileSearch(string regionName, Int32 startRowIndex, Int32 maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable RegionDataTable = null;
            try
            {
                if (regionName == null)
                { regionName = ""; }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSearchMapList");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionName", DbType.String, regionName);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                RegionDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return RegionDataTable;
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
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Insert region
        /// </summary>
        public static void regionAddMasterfileInsert(string regionName, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertRegion");

                db.AddInParameter(dbCommand, "@pRegionName", DbType.String, regionName);
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
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
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Update region
        /// </summary>
        /// 
        public static void regionAddMasterfileUpdate(int regionId, string regionName, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateRegion");

                db.AddInParameter(dbCommand, "@pRegionId", DbType.Int32, regionId);
                db.AddInParameter(dbCommand, "@pRegionName", DbType.String, regionName);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete region
        /// </summary>
        public static void regionAddMasterfileDelete(int regionId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteRegion");
                db.AddInParameter(dbCommand, "@pRegionId", DbType.Int32, regionId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        /// Date Created:   18/Feb/2013
        /// Created By:     Josephine Gad
        /// Description:    Delete region
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="UserName"></param>
        /// <param name="RegionId"></param>
        /// <param name="RegionName"></param>
        /// <param name="sFunction"></param>
        /// <param name="sFileName"></param>
        /// <param name="GMTDate"></param>
        /// <param name="Now"></param>
        public static void SaveRegionSeaport(DataTable dt, string UserName, int RegionId,
            string RegionName, string sDescription, string sFunction, string sFileName, DateTime GMTDate, DateTime Now)
        {
            string ConnStr = ConnStr = ConfigurationManager.ConnectionStrings["TRAVELMARTConnectionString"].ToString();
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            SqlCommand sqlCmd = new SqlCommand("uspSaveRegionSeaport", sqlConn);
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                sqlConn.Open();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SqlParameter param1 = new SqlParameter("@pUserName", UserName);
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.VarChar;
                SqlParameter param2 = new SqlParameter("@pRegionId", RegionId);
                param2.Direction = ParameterDirection.Input;
                param2.SqlDbType = SqlDbType.Int;
                SqlParameter param3 = new SqlParameter("@pRegionName", RegionName);
                param3.Direction = ParameterDirection.Input;
                param3.SqlDbType = SqlDbType.VarChar;

                SqlParameter param4 = new SqlParameter("@pLogDescription", sDescription);
                param4.Direction = ParameterDirection.Input;
                param4.SqlDbType = SqlDbType.VarChar;

                SqlParameter param5 = new SqlParameter("@pstrFunction", sFunction);
                param5.Direction = ParameterDirection.Input;
                param5.SqlDbType = SqlDbType.VarChar;

                SqlParameter param6 = new SqlParameter("@pFileName", sFileName);
                param6.Direction = ParameterDirection.Input;
                param6.SqlDbType = SqlDbType.VarChar;

                SqlParameter param7 = new SqlParameter("@pGMTDate", GMTDate);
                param7.Direction = ParameterDirection.Input;
                param7.SqlDbType = SqlDbType.DateTime;

                SqlParameter param8 = new SqlParameter("@pTimeZone", strTimeZone);
                param8.Direction = ParameterDirection.Input;
                param8.SqlDbType = SqlDbType.VarChar;

                SqlParameter param9 = new SqlParameter("@pDateNow", Now);
                param9.Direction = ParameterDirection.Input;
                param9.SqlDbType = SqlDbType.DateTime;

                SqlParameter[] ParamArray = { param, param1, param2, param3, param4, param5, param6, param7, param8, param9 };
                sqlCmd.Parameters.AddRange(ParamArray);
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
                if (sqlCmd != null)
                {
                    sqlCmd.Dispose();
                }
            }
        }
        #endregion

        #region City DAL
        /// <summary>
        /// Date Created: 26/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) View list of cities by country                
        /// </summary>
        public DataTable MasterfileCityView(string CountryId, string RoleID, string pCityName, Int32 startRowIndex, Int32 maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable RegionDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspViewCityList");
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryId", DbType.Int32, CountryId);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, RoleID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityName", DbType.String, (pCityName == null ? "" : pCityName));
                SFDatebase.AddInParameter(SFDbCommand, "@startRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@maximumRows", DbType.Int32, maximumRows);
                RegionDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return RegionDataTable;
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
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 26/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Count list of cities by country
        /// </summary>
        public Int32 MasterfileCityViewCount(string pCountryId, string RoleID, string pCityName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int City = 0;            

            try
            {                
                dbCommand = db.GetStoredProcCommand("uspViewCityListCount");
                db.AddInParameter(dbCommand, "@pCountryId", DbType.String, pCountryId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, RoleID);
                db.AddInParameter(dbCommand, "@pCityName", DbType.String, (pCityName == null ? "" : pCityName));
                IDataReader dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    City = Convert.ToInt32(dr["maximumRows"]);
                }
                return City;
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

            }
        }

        /// <summary>
        /// Date Created: 26/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Delete City
        /// </summary>
        public static void MasterfileCityDelete(string pCityId, string pModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspViewCityList_DeletePerCity");
                db.AddInParameter(dbCommand, "@pCityId", DbType.String, pCityId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, pModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Search for City
        /// </summary>
        public DataTable cityViewMasterfileSearch(string CountryId, string CityName, Int32 startRowIndex, Int32 maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable RegionDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSearchCityList");
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryId", DbType.Int32, CountryId);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityName", DbType.String, CityName);
                SFDatebase.AddInParameter(SFDbCommand, "@startRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@maximumRows", DbType.Int32, maximumRows);
                RegionDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return RegionDataTable;
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
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count City 
        /// </summary>
        public Int32 CityViewMasterfileSearchCount(string pCountryId, string pCityName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int City = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSearchCityListCount");
                db.AddInParameter(dbCommand, "@pCountryId", DbType.String, pCountryId);
                db.AddInParameter(dbCommand, "@pCityName", DbType.String, pCityName);
                IDataReader dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    City = Convert.ToInt32(dr["maximumRows"]);
                }
                return City;
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
                
            }
        }

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Add new City
        /// </summary>
        public static Int32 cityAddMasterfileInsert(int CountryId, string CityCode, string CityName, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertCity");
                db.AddInParameter(dbCommand, "@pCityCode", DbType.String, CityCode);
                db.AddInParameter(dbCommand, "@pCityName", DbType.String, CityName);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@pCityID", DbType.Int32, 8);
                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                Int32 CityID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pCityID"));
                return CityID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
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
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Update city
        /// </summary>
        /// 
        public static void cityAddMasterfileUpdate(int CountryId, int CityId, string CityName, string CityCode, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateCity");

                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCityName", DbType.String, CityName);
                db.AddInParameter(dbCommand, "@pCityCode", DbType.String, CityCode);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete city
        /// </summary>
        public static void cityViewMasterfileDelete(string pCityId, string pModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteCity");
                db.AddInParameter(dbCommand, "@pCityId", DbType.String, pCityId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, pModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        #endregion

        #region PortAgentSeaport
        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Search Service Provider seaport
        /// </summary>
        public static DataTable PortListMaintenanceSearchAgentSeaport(string portAgentId, string CityId, Int32 startRowIndex, Int32 maximumRows)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable AgentSeaportDataTable = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSearchPortAgentSeaport");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.String, portAgentId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.String, CityId);
                db.AddInParameter(dbCommand, "@startRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@maximumRows", DbType.Int32, maximumRows);
                AgentSeaportDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return AgentSeaportDataTable;
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
                if (AgentSeaportDataTable != null)
                {
                    AgentSeaportDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Service Provider seaport
        /// </summary>
        public Int32 PortListMaintenanceSearchAgentSeaportCount(string portAgentId, string CityId)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int VesselInt = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSearchPortAgentSeaportCount");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.String, portAgentId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.String, CityId);

                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        VesselInt = Convert.ToInt32(dr["maximumRows"]);
                    }
                }

                return VesselInt;
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
            }

        }

        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Load seaport
        /// </summary>
        public static DataTable PortListMaintenanceSearch(int CityId, int PortAgentId, bool loadAll)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable AgentSeaportDataTable = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSearchSeaport");

                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, PortAgentId);
                db.AddInParameter(dbCommand, "@pAllData", DbType.Boolean, loadAll);
                AgentSeaportDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return AgentSeaportDataTable;
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
                if (AgentSeaportDataTable != null)
                {
                    AgentSeaportDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Add new Service Provider seaport
        /// </summary>
        public static Int32 PortListMaintenanceInsert(int portAgentId, int PortId, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertPortAgentSeaport");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, portAgentId);
                db.AddInParameter(dbCommand, "@pPortId", DbType.Int32, PortId);
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@PortAgentSeaportID", DbType.Int32, 8);
                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                Int32 PortAgentSeaportID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@PortAgentSeaportID"));
                return PortAgentSeaportID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
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
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Service Provider Seaport
        /// </summary>
        public static void portListMaintenanceDelete(int portAgentSeaportId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeletePortAgentSeaport");
                db.AddInParameter(dbCommand, "@pPortAgentSeaportId", DbType.Int32, portAgentSeaportId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        #endregion

        #region Sail Master
        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count vessels
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        public Int32 SailMasterMaintenanceVesselSearchCount(string pVesselName, bool viewAll)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int maximumRows = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVesselListCount");
                db.AddInParameter(dbCommand, "@pVesselName", DbType.String, pVesselName);
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateFrom"]));
                db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateTo"]));
                db.AddInParameter(dbCommand, "@pViewAll", DbType.Boolean, viewAll);
                db.AddInParameter(dbCommand, "@pPortAgent", DbType.Int16, GlobalCode.Field2Int(HttpContext.Current.Session["UserBranchID"]));
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        maximumRows = Convert.ToInt32(dr["maximumRows"]);
                    }
                }

                return maximumRows;
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
               
            }

        }

        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load all vessels
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        public DataTable SailMasterMaintenanceVesselSearch(string pVesselName, bool viewAll, Int32 startRowIndex, Int32 maximumRows)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable VesselDataTable = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVesselAll");
                db.AddInParameter(dbCommand, "@pVesselName", DbType.String, pVesselName);
                db.AddInParameter(dbCommand, "@startRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@maximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pViewAll", DbType.Boolean, viewAll);
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateFrom"]));
                db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateTo"]));
                db.AddInParameter(dbCommand, "@pPortAgent", DbType.Int16, GlobalCode.Field2Int(HttpContext.Current.Session["UserBranchID"])); 
                VesselDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return VesselDataTable;
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
                if (VesselDataTable != null)
                {
                    VesselDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Sail Master
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        public Int32 SailMasterAddMaintenanceSearchCount(string pVesselName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int maximumRows = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSailMasterListCount");
                db.AddInParameter(dbCommand, "@pVesselId", DbType.String, pVesselName);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateFrom"]));
                db.AddInParameter(dbCommand, "@pEndDate", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateTo"]));
                db.AddInParameter(dbCommand, "@pPortId", DbType.String, GlobalCode.Field2String(HttpContext.Current.Session["Port"])); 
                db.AddInParameter(dbCommand, "@pItineraryCode", DbType.String, GlobalCode.Field2String(HttpContext.Current.Session["ItineraryCode"])); 
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        maximumRows = Convert.ToInt32(dr["maximumRows"]);
                    }
                }

                return maximumRows;
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
               
            }

        }

        /// <summary>
        /// Date Created:05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load all sail master
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        public DataTable SailMasterAddMaintenanceSearch(string pVesselId, Int32 startRowIndex, Int32 maximumRows)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable VesselDataTable = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSailMaster");
                db.AddInParameter(dbCommand, "@pVesselId", DbType.String, pVesselId);
                db.AddInParameter(dbCommand, "@startRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@maximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateFrom"]));
                db.AddInParameter(dbCommand, "@pEndDate", DbType.DateTime, GlobalCode.Field2DateTime(HttpContext.Current.Session["DateTo"]));
                db.AddInParameter(dbCommand, "@pPortId", DbType.String, GlobalCode.Field2String(HttpContext.Current.Session["Port"])); 
                db.AddInParameter(dbCommand, "@pItineraryCode", DbType.String, GlobalCode.Field2String(HttpContext.Current.Session["ItineraryCode"]));
                VesselDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return VesselDataTable;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (VesselDataTable != null)
                {
                    VesselDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load sail master details
        /// </summary>
        public IDataReader SailMasterAddMaintenanceLoadDetails(int sailMasterId)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSailMasterbyId");
                db.AddInParameter(dbCommand, "@pSailMasterId", DbType.String, sailMasterId);
                IDataReader dr = db.ExecuteReader(dbCommand);
                return dr;
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
            }

        }

        /// <summary>
        /// Date Created:04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Save sail master
        /// </summary>
        public DataTable SailMasterAddMaintenanceSave(int sailMasterId, int PortId, int VesselId,
            string itineraryCode, string voyageNo, int DaySeq, DateTime ScheduleDate, DateTime dateFrom, DateTime dateTo, string UserId)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            int returnValue = 0;           
            DataTable tempDT = new DataTable();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveSailMaster");
                db.AddInParameter(dbCommand, "@pSailMasterId", DbType.Int32, sailMasterId);
                db.AddInParameter(dbCommand, "@pPortId", DbType.Int32, PortId);
                db.AddInParameter(dbCommand, "@pVesselId", DbType.Int32, VesselId);
                db.AddInParameter(dbCommand, "@pItineraryCode", DbType.String, itineraryCode);
                db.AddInParameter(dbCommand, "@pVoyageNo", DbType.String, voyageNo);
                db.AddInParameter(dbCommand, "@pDaySeq", DbType.Int32, DaySeq);
                db.AddInParameter(dbCommand, "@pScheduleDate", DbType.DateTime, ScheduleDate);
                db.AddInParameter(dbCommand, "@pUser", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.DateTime, dateFrom);
                db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, dateTo);
                db.AddOutParameter(dbCommand, "@pRetValue", DbType.Int32, returnValue);
                db.AddOutParameter(dbCommand, "@SailMasterID", DbType.Int32, 8);
                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                                                
                //returnValue = Int32.Parse(dbCommand.Parameters["@pRetValue"].Value.ToString());                
                //return returnValue;

                returnValue = Int32.Parse(dbCommand.Parameters["@pRetValue"].Value.ToString());
                Int32 pSailMasterID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@SailMasterID"));

                tempDT.Columns.Add("dtReturnValue");
                tempDT.Columns.Add("dtSailMasterID");

                tempDT.Rows.Add(returnValue, pSailMasterID);
                return tempDT;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
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
        /// Date Created:11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Sail Master
        /// </summary>
        public static void SailMasterViewDelete(string pSailMasterId, string pModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteSailMaster");
                db.AddInParameter(dbCommand, "@pSailMasterId", DbType.String, pSailMasterId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, pModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        #endregion

        #region Select
        /// <summary>
        /// Date Created:  03/10/2011
        /// Created By:    Josephine Gad
        /// (description)  Selecting list of reference by code              
        /// </summary>
        /// <param name="RefCode"></param>
        /// <returns></returns>
        public static DataTable GetReference(string RefCode)
        {
            Database RefDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand RefDbCommand = null;
            DataTable RefDataTable = null;
            try
            {
                RefDbCommand = RefDatebase.GetStoredProcCommand("uspGetReference");
                RefDatebase.AddInParameter(RefDbCommand, "@pRefCodeVarchar", DbType.String, RefCode);
                RefDataTable = RefDatebase.ExecuteDataSet(RefDbCommand).Tables[0];
                return RefDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RefDbCommand != null)
                {
                    RefDbCommand.Dispose();
                }
                if (RefDataTable != null)
                {
                    RefDataTable.Dispose();
                }
            }
        }
        
        #endregion

        #region Events
        /// <summary>
        /// Date Created:12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load events
        /// -----------------------------------
        /// Date Modified: 22/02/2012
        /// Modified By: Charlene Remotigue
        /// Description: Save Event to list
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public List<MaintenanceGenericClass> EventsViewLoadEvents(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
            int BranchId, int LoadType, int startRowIndex, int maximumRows)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            int EventCount = 0;
            List<MaintenanceGenericClass> MaintenanceList = new List<MaintenanceGenericClass>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectEventList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserId", DbType.String, UserId);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateFrom", DbType.Date, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateTo", DbType.Date, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionId", DbType.Int32, RegionId);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryId", DbType.Int32, CountryId);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, BranchId);
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);

                DataSet ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                EventCount = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                if (ds.Tables.Count > 1)
                {                    
                    CountryDataTable = ds.Tables[1];
                }

                MaintenanceList.Add(new MaintenanceGenericClass()
                {
                    EventCount = GlobalCode.Field2Int(EventCount),
                    Events = (from a in CountryDataTable.AsEnumerable()
                              select new Events 
                              { 
                                  colEventIdInt = a.Field<int?>("colEventIdInt"),
                                  colEventNameVarchar = a.Field<string>("colEventNameVarchar"),
                                  colEventDateFromDate = a.Field<DateTime?>("colEventDateFromDate"),
                                  colEventDateToDate = a.Field<DateTime?>("colEventDateToDate"),
                                  colIsDoneBit = GlobalCode.Field2Bool(a["colIsDoneBit"]),
                                  colVendorBranchNameVarchar = a.Field<string>("colVendorBranchNameVarchar"),
                              }).ToList(),
                });

                return MaintenanceList;
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
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 22/02/2012
        /// Description: get events paging
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="EventCount"></param>
        /// <returns></returns>
        public List<Events> EventsViewLoadEventsPage(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
            int BranchId, int LoadType, int startRowIndex, int maximumRows, out int EventCount)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectEventList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserId", DbType.String, UserId);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateFrom", DbType.Date, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateTo", DbType.Date, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionId", DbType.Int32, RegionId);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryId", DbType.Int32, CountryId);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, BranchId);
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);

                DataSet ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                EventCount = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                if (ds.Tables.Count > 1)
                {
                    CountryDataTable = ds.Tables[1];
                }

                var query = (from a in CountryDataTable.AsEnumerable()
                             select new Events 
                             {
                                 colEventIdInt = a.Field<int?>("colEventIdInt"),
                                 colEventNameVarchar = a.Field<string>("colEventNameVarchar"),
                                 colEventDateFromDate = a.Field<DateTime?>("colEventDateFromDate"),
                                 colEventDateToDate = a.Field<DateTime?>("colEventDateToDate"),
                                 colIsDoneBit = GlobalCode.Field2Bool(a["colIsDoneBit"]),
                                 colVendorBranchNameVarchar = a.Field<string>("colVendorBranchNameVarchar"),
                             }).ToList();
               

                return query;
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
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Events 
        /// </summary>
        public Int32 EventsViewLoadEventsCount(string CityId, string BranchId, DateTime DateFrom, DateTime DateTo, bool viewAll)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            int maximumRows = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectEventListCount");
                db.AddInParameter(dbCommand, "@pCityId", DbType.String, CityId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.String, BranchId);
                db.AddInParameter(dbCommand, "@pViewAll", DbType.Boolean, viewAll);
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.Date, DateFrom);
                db.AddInParameter(dbCommand, "@pDateTo", DbType.Date, DateTo);
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        maximumRows = Convert.ToInt32(dr["maximumRows"]);
                    }
                }

                return maximumRows;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
               
            }
        }

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Event
        /// </summary>
        public void EventsViewMaintenanceDelete(string EventId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteEvent");
                db.AddInParameter(dbCommand, "@pEventId", DbType.Int32, EventId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Tag Event as done
        /// </summary>
        public void EventsViewMaintenanceTag(string EventId, string ModifiedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspTagEvent");
                db.AddInParameter(dbCommand, "@pEventId", DbType.Int32, EventId);
                db.AddInParameter(dbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
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
                connection.Close();
                connection.Dispose();
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
        /// Date Created:12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load event details
        /// </summary>
        public IDataReader EventsAddMaintenanceLoadDetails(string EventId)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectEventDetails");
                db.AddInParameter(dbCommand, "@EventId", DbType.String, EventId);
                IDataReader dr = db.ExecuteReader(dbCommand);
                return dr;
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
                
            }

        }

        /// <summary>
        /// Date Created:04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Save event
        /// </summary>
        public static Int32 EventsAddMaintenanceSave(string EventId, string EventName, DateTime DateFrom,
            DateTime DateTo, string BranchId, string CityId, string UserId, string Remarks)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveEventDetails");
                db.AddInParameter(dbCommand, "@pEventId", DbType.String, EventId);
                db.AddInParameter(dbCommand, "@pEventName", DbType.String, EventName);
                db.AddInParameter(dbCommand, "@pEventDateFrom", DbType.Date, DateFrom);
                db.AddInParameter(dbCommand, "@pEventDateTo", DbType.Date, DateTo);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.String, BranchId);
                db.AddInParameter(dbCommand, "@pUser", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.String, CityId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddOutParameter(dbCommand, "@EventID", DbType.Int32, 8);
                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                Int32 pEventID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@EventID"));
                return pEventID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
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

        #endregion

        #region DELETED
        ///// <summary>
        ///// Date Created:30/09/2011
        ///// Created By: Charlene Remotigue
        ///// Description: get country and region name
        ///// </summary>
        //public static void getCountryRegionName(int countryId)
        //{
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspGetCountryName");
        //        db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, countryId);
        //        using (IDataReader dr = db.ExecuteReader(dbCommand))
        //        {
        //            if (dr.Read())
        //            {
        //                MasterfileVariables.CountryNameString = dr["colCountryNameVarchar"].ToString();
        //                MasterfileVariables.RegionNameString = dr["colMapNameVarchar"].ToString();
        //            }

        //        }

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
        //    }
        //}
        #endregion      

        /// <summary>
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetAirportList(bool IsSearchByCode, string sFilter)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetAirportListByCodeOrName");
                db.AddInParameter(dbComm, "@pIsByAirportCode", DbType.Boolean, IsSearchByCode);
                db.AddInParameter(dbComm, "@pFilterVarchar", DbType.String, sFilter);
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new AirportDTO
                        {
                            AirportIDString = GlobalCode.Field2String(a["AirportID"]),
                            AirportNameString = a.Field<string>("AirportName"),
                            AirportCodeString = a.Field<string>("AirportCode")
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<SeaportDTO> GetSeaportList(bool IsSearchByCode, string sFilter)
        {
            List<SeaportDTO> list = new List<SeaportDTO>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetSeaportListByCodeOrName");
                db.AddInParameter(dbComm, "@pIsBySeaportCode", DbType.Boolean, IsSearchByCode);
                db.AddInParameter(dbComm, "@pFilterVarchar", DbType.String, sFilter);
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new SeaportDTO
                        {
                            SeaportIDString = GlobalCode.Field2String(a["SeaportID"]),
                            SeaportNameString = GlobalCode.Field2String(a["SeaportName"]),
                            SeaportCodeString = a.Field<string>("SeaportCode")
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Vessel Brand List
        /// ----------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static List<BrandList> GetBrandList()
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
            try
            {
                List<BrandList> list = new List<BrandList>();

                comm = db.GetStoredProcCommand("uspBrandGet");
                dt = db.ExecuteDataSet(comm).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new BrandList
                        {
                            BrandID = GlobalCode.Field2Int(a["BrandID"]),
                            BrandName = GlobalCode.Field2String(a["BrandName"])

                        }).ToList();

                return list;
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
            }
        }
        /// <summary>
        /// Date Created:   19/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get Vessel By Brand
        /// ----------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static List<VesselList> GetVesselList(string sBrandID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
            try
            {
                List<VesselList> list = new List<VesselList>();

                comm = db.GetStoredProcCommand("uspGetVesselByBrand");
                db.AddInParameter(comm, "@pBrandID", DbType.Int32, GlobalCode.Field2Int(sBrandID));

                dt = db.ExecuteDataSet(comm).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new VesselList
                        {
                            VesselID = GlobalCode.Field2Int(a["colVesselIdInt"]),
                            VesselName = GlobalCode.Field2String(a["VesselName"])

                        }).ToList();

                return list;
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
            }
        }

        /// <summary>
        /// Date Created:   25/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get Nationality List
        /// ----------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static List<NationalityList> GetNationalityList(string sFilter, string sSortedBy, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
            DataTable dtCount = null;

            int iCount = 0;
            try
            {
                List<NationalityList> list = new List<NationalityList>();

                comm = db.GetStoredProcCommand("uspNationalityGet");
                db.AddInParameter(comm, "@pFilter", DbType.String, sFilter);
                db.AddInParameter(comm, "@pSortedBy", DbType.String, sSortedBy);
                db.AddInParameter(comm, "@pStartRow", DbType.String, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.String, iMaxRow);

                dtCount = db.ExecuteDataSet(comm).Tables[0];
                dt = db.ExecuteDataSet(comm).Tables[1];
                list = (from a in dt.AsEnumerable()
                        select new NationalityList
                        {
                            NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                            NationalityCode = a.Field<string>("NationalityCode"),
                            Nationality = a.Field<string>("NationalityName"),
                            IsOKTB = GlobalCode.Field2Bool(a["OKTB"]),

                        }).ToList();

                iCount = GlobalCode.Field2Int(dtCount.Rows[0][0]);
                HttpContext.Current.Session["NationalityList_Count"] = iCount;
                return list;
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
                if (dtCount != null)
                {
                    dtCount.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   25/Nov/2014
        /// Description:    Update Nationality OK To Brazil
        /// ---------------------------------------------------------------
        /// </summary>
        public void UpdateNationalityOkTB(string UserId, int iNationalityID,   bool IsOKTB,       
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspNationalityUpdateOkTB");

                db.AddInParameter(dbCommand, "@pNatioalityIdInt", DbType.Int32, iNationalityID);
                db.AddInParameter(dbCommand, "@pIsOKToBrazilBit", DbType.Boolean, IsOKTB);

                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, UserId);


                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.ExecuteDataSet(dbCommand, trans);

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
                if (ds != null)
                {
                    ds.Dispose();
                }               
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   09/Jul/2015
        /// Description:    Insert Timezone in SQL DB
        /// ---------------------------------------------------------------
        /// </summary>
        public void TimezoneInsert(string sTimeZoneName, string sStandardName, double fUTCOffset, string sCreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            try
            {

                dbCommand = db.GetStoredProcCommand("uspTimezoneInsert");

                db.AddInParameter(dbCommand, "@pTimeZoneNameVarchar", DbType.String, sTimeZoneName);
                db.AddInParameter(dbCommand, "@pStandardNameVarchar", DbType.String, sStandardName);
                db.AddInParameter(dbCommand, "@pUTCOffset", DbType.Double, fUTCOffset);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, sCreatedBy);
                
                db.ExecuteDataSet(dbCommand, trans);

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
    }
}

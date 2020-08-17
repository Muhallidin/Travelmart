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
    public class CountryDAL
    {
        #region Function                  
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Country List
        /// ---------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable CountryList()
        {            
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryLists");

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
        /// Date Created:    16/08/2011
        /// Created By:      Josephine Gad
        /// (description)    Get Country List by user                
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable CountryListByUser(string UserID)
        {           
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
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
        /// Date Created:    23/09/2011
        /// Created By:      Josephine Gad
        /// (description)    Get Country List by Region ID
        /// ----------------------------------------------
        /// Date Created:    07/10/2011
        /// Created By:      Josephine Gad
        /// (description)    Add parameter countryName
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static DataTable CountryListByRegion(string RegionID, string CountryName)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryList");
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryName", DbType.String, CountryName);
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
        /// Date Created: 15/08/2011
        /// Created By: Josephine Gad
        /// (description) Get Region List
        /// </summary>
        //public static DataTable RegionList()
        //{            
        //    Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;            
        //    DataTable RegionDataTable = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectRegionList");                
        //        RegionDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];                
        //        return RegionDataTable;
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
        //        if (RegionDataTable != null)
        //        {
        //            RegionDataTable.Dispose();
        //        }               
        //    }
        //}
        /// <summary>        
        /// Date Created:   15/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Region List        
        /// ----------------------------------
        /// Date Modified:   25/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Remove Country parameter
        ///                  Change Datatable to List
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<RegionList> RegionListByUser(string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            List<RegionList> list = new List<RegionList>();
            DataTable RegionDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectRegionList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                RegionDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in RegionDataTable.AsEnumerable()
                        select new RegionList
                        {
                            RegionId = GlobalCode.Field2TinyInt(a["colRegionIDInt"]),
                            RegionName = a.Field<string>("colRegionNameVarchar")
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
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
            //Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            //DbCommand SFDbCommand = null;
            //DataTable RegionDataTable = null;
            //try
            //{
            //    SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectRegionList");
            //    SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);                
            //    RegionDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
            //    return RegionDataTable;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (SFDbCommand != null)
            //    {
            //        SFDbCommand.Dispose();
            //    }
            //    if (RegionDataTable != null)
            //    {
            //        RegionDataTable.Dispose();
            //    }
            //}
        }
        /// <summary>        
        /// Date Created:   16/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Map List        
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable MapListByUser(string UserID)
        {            
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable RegionDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectMapList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);                
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
        /// Date Created:   04/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Country list by continent
        /// ---------------------------------------------
        /// </summary>
        /// <param name="ContinentID"></param>
        /// <returns></returns>
        public static List<Country> GetCountryByContinent(string ContinentID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            List<Country> list = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetCountry");
                SFDatebase.AddInParameter(SFDbCommand, "@pContinentID", DbType.Int32, GlobalCode.Field2Int(ContinentID));                
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new Country
                        {
                            CountryID = GlobalCode.Field2TinyInt(a["colCountryIDInt"]),
                            CountryName = a.Field<string>("colCountryNameVarchar")
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
        /// Date Created:   19/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get the generic class of country
        /// </summary>
        public static void GetCountryList()
        {
            List<Country> list = new List<Country>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataTable dt = null;
            DataSet ds = null;
            
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCountryLists");
                
                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new Country {
                            CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                            CountryName = a.Field<string>("colCountryNameVarchar"),
                        }).ToList();


                CountryGenericClass.CountryList = list;

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
        /// Date Created:   19/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get the generic class of country
        /// </summary>
        public static List<NationalityGenericClass> GetRestrictedNationalityList(bool IsViewBoth, bool IsRestricted, int CountryID)
        {
            List<NationalityGenericClass> list = new List<NationalityGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataTable dt = null;
            DataTable dtNon = null;

            DataSet ds = null;

            try
            {
                int iCount = 0;
                dbCommand = db.GetStoredProcCommand("uspSelectRestrictedNationalityLists");
                db.AddInParameter(dbCommand, "@pViewBoth", DbType.Boolean, IsViewBoth);
                db.AddInParameter(dbCommand, "@pIsRestricted", DbType.Boolean, IsRestricted);
                db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32, CountryID); 

                ds = db.ExecuteDataSet(dbCommand);

                if (IsViewBoth)
                {
                    dtNon = ds.Tables[0];
                    iCount = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0].ToString());
                    dt = ds.Tables[2];

                    list.Add(new NationalityGenericClass()
                    {
                        RestNationalityCount = iCount,
                        RestNationalityList = (from a in dt.AsEnumerable()
                                           select new Nationality
                                           {
                                               RestrictedID = GlobalCode.Field2Int(a["RestrictedID"]),
                                               NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                                               NationalityCode = a.Field<string>("NationalityCode"),
                                               NationalityName = a.Field<string>("NationalityName"),
                                           }).ToList(),
                        NonRestNationalityList = (from a in dtNon.AsEnumerable()
                                                  select new Nationality
                                                  {
                                                      NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                                                      NationalityCode = a.Field<string>("NationalityCode"),
                                                      NationalityName = a.Field<string>("NationalityName"),
                                                  }).ToList(),
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
                if (dtNon != null)
                {
                    dtNon.Dispose();
                } 
            }
        }
        /// <summary>
        /// Date Created:   21/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Save Restricted Nationality in Country
        /// </summary>
        public static void  SaveRestrictedNationality(int CountryID, int NationalityID, string sRemarks, string sUserName,
          String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveRestrictedNationalities");

                db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, sUserName);


                db.AddInParameter(dbCommand, "@pLogDescriptionVarchar", DbType.String, sRemarks);
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
        /// Date Created:   21/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Delete Restricted Nationality in Country
        /// </summary>
        public static void DeleteRestrictedNationality(int RestrictedID, string sRemarks, string sUserName,
          String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteRestrictedNationalities");

                db.AddInParameter(dbCommand, "@pRestrictedIDInt", DbType.Int32, RestrictedID);
                db.AddInParameter(dbCommand, "@pDeletedBy", DbType.String, sUserName);

                db.AddInParameter(dbCommand, "@pLogDescriptionVarchar", DbType.String, sRemarks);
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
        #endregion 
    }
}

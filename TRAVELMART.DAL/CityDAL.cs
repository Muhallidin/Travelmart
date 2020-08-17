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
    public class CityDAL
    {

        #region Method        
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List
        /// ---------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable CityList()
        {          
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityList");

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
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By City ID
        /// ---------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable CityListByID(Int32 CityID)
        {           
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByID");
                SFDatebase.AddInParameter(SFDbCommand, "pCityID", DbType.Int32, CityID);
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
       /// Date Created:    01/08/2011
       /// Created By:      Marco Abejar
       /// (description)    Get City List By Country ID
       /// </summary>
       /// <param name="CountryID"></param>
       /// <returns></returns>
        public static DataTable CityListbyCountry(Int32 CountryID)
        {           
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByCountryID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolCountryIDInt", DbType.Int32, CountryID);
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
        /// Date Created:   07/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get City List By country ID
        /// ---------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static DataTable GetCityByCountry(string CountryID, string CityName, string CityInitial)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityByCountryID");
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityName", DbType.String, CityName);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityInitial", DbType.String, CityInitial);
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
        #endregion 


    }
}

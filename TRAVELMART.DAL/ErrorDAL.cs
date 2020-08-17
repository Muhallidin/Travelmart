using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Globalization;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class ErrorDAL
    {
        /// <summary>
        /// Date Created:   15/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Error
        /// </summary>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>        
        /// <returns></returns>
        public static DataTable GetError(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            DbCommand command = null;
            try 
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectLogError");
                db.AddInParameter(command, "@pDateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@pDateTo", DbType.DateTime, DateTo);
                db.AddInParameter(command, "@pStartRow", DbType.Int16, StartRow);
                db.AddInParameter(command, "@pMaxRow", DbType.Int64, MaxRow);
                dt = db.ExecuteDataSet(command).Tables[0];
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
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   15/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Error Total Row Count
        /// --------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static int GetErrorCount(DateTime DateFrom, DateTime DateTo)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand command = null;
            IDataReader dr = null;
            try
            {
                command = db.GetStoredProcCommand("uspSelectLogErrorCount");
                db.AddInParameter(command, "@pDateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@pDateTo", DbType.DateTime, DateTo);

                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    return int.Parse(dr["TotalRowCount"].ToString());
                }
                else
                {
                    return 0;
                }
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
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   15/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Error
        /// </summary>
        /// <param name="sError"></param>
        /// <param name="sDescription"></param>
        /// <param name="sPageName"></param>
        /// <param name="dDate"></param>
        /// <param name="dDateGMT"></param>
        /// <param name="sUser"></param>
        public static void InsertError(string sError,  string sDescription, string sPageName, DateTime dDate, DateTime dDateGMT, string sUser)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string sTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                //DateTime dDateGMT = dDate.ToUniversalTime();
                
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertLogError");
                SFDatebase.AddInParameter(SFDbCommand, "@pErrorVarchar", DbType.String, sError);
                SFDatebase.AddInParameter(SFDbCommand, "@pErrorDescVarchar", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pPageNameVarchar", DbType.String, sPageName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeZoneVarchar", DbType.String, sTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateCreatedGMT", DbType.DateTime, dDateGMT);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateCreatedDate", DbType.DateTime, dDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedByVarchar", DbType.String, sUser);              
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
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}

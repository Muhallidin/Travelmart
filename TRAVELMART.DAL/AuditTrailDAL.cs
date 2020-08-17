using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.IO;
using System.Configuration;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class AuditTrailDAL
    {
        /// <summary>            
        /// Date Created: 15/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert log audit trail
        /// ----------------------------------------
        /// Date Modified:  01/10/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add trans.Dispose();
        /// </summary>
        public static void InsertLogAuditTrail(Int32 pID, string pSeqNo, String strLogDescription, String strFunction, String strPageName,
                                               DateTime DateGMT, DateTime CreatedDate, String CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspInsertLogAuditTrail");

                if (pID != 0)
                {
                    db.AddInParameter(dbCommand, "@pIdBigint", DbType.Int32, pID);
                }
                
                if (pSeqNo.Trim() != "0" && pSeqNo != "")
                {
                    db.AddInParameter(dbCommand, "@pSeqNoInt", DbType.Int32, Convert.ToInt32(pSeqNo));
                }
                
                db.AddInParameter(dbCommand, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, CreatedBy);              

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
        /// Date Created: 17/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get audit trail logs
        /// </summary>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>        
        /// <returns></returns>
        public static DataTable GetAuditTrail(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectLogAuditTrail");
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
        /// Date Created: 17/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get audit trail logs count
        /// --------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static int GetAuditTrailCount(DateTime DateFrom, DateTime DateTo)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand command = null;
            IDataReader dr = null;
            try
            {
                command = db.GetStoredProcCommand("uspSelectLogAuditTrailCount");
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
    }
}

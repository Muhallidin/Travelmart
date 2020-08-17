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
    public class EmailDAL
    {
        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Load e-mail address
        /// </summary>
        public static IDataReader LoadEmailAddress(string BranchId)
        {
            Database EmailDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand EmailDbCommand = null;
            IDataReader EmailDataReader = null;
            try
            {
                EmailDbCommand = EmailDatebase.GetStoredProcCommand("uspGetHotelBranchEmailAddress");
                EmailDatebase.AddInParameter(EmailDbCommand, "@pBranchId", DbType.Int32, BranchId);                
                EmailDataReader = EmailDatebase.ExecuteReader(EmailDbCommand);
                return EmailDataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (EmailDbCommand != null)
                {
                    EmailDbCommand.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 28/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Save e-mail address
        /// </summary>    
        public static void SaveEmailAddress(string BranchId, string EmailTo, string EmailCc)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveEmailAddress");

                db.AddInParameter(dbCommand, "@pcolBranchIdInt", DbType.String, BranchId);
                db.AddInParameter(dbCommand, "@pcolEmailToVarchar", DbType.String, EmailTo);
                db.AddInParameter(dbCommand, "@pcolEmailCcVarchar", DbType.String, EmailCc);                

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
        /// Date Created: 04/Mar/2013
        /// Created By:   Josephine Gad
        /// (description) Get email add of all Active Users
        /// </summary>
        /// <returns></returns>
        public static List<ActiveUserEmail> GetActiveUserEmail()
        {
            Database db = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
            DbCommand dbcomm = null;
            DataTable dt = null;
            List<ActiveUserEmail> list = new List<ActiveUserEmail>();

            try
            {
                dbcomm = db.GetStoredProcCommand("uspGetActiveUserEmail");
                dt = db.ExecuteDataSet(dbcomm).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new ActiveUserEmail() {
                            sEmail = a.Field<string>("Email")
                        
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
                if (dbcomm != null)
                {
                    dbcomm.Dispose();                    
                }
            }

        }
    }
}

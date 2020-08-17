using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class FinanceDAL
    {
        #region Reimbursement
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/10/2011
        /// Description: count seafarer reimbursement
        /// </summary>
        /// <param name="SeafarerId"></param>
        /// <returns></returns>
        public Int32 SelectReimbursementListbySeafarerCount(string SeafarerId, int mReqId, int tReqId)
        {
            int maximumRows = 0;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetReimbursementsBySeafarerIdCount");
                db.AddInParameter(dbCommand, "@pcolSeafarerId", DbType.Int32, Int32.Parse(SeafarerId));
                db.AddInParameter(dbCommand, "@pMReqId", DbType.Int32, mReqId);
                db.AddInParameter(dbCommand, "@pTReqId", DbType.Int32, tReqId);
                using (IDataReader dr = db.ExecuteReader(dbCommand))
                {
                    if (dr.Read())
                    {
                        maximumRows = Int32.Parse(dr["maximumRows"].ToString());
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
        /// Author: Charlene Remotigue
        /// Date Created: 27/10/2011
        /// Description: load seafarer reimbursement list
        /// </summary>
        /// <param name="SeafarerId"></param>
        /// <param name="startRownIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable SelectReimbursementListbySeafarer(string SeafarerId, Int32 startRownIndex, Int32 maximumRows, int mReqId, int tReqId)
        {
            DataTable ReimbursementDataTable = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {

                dbCommand = db.GetStoredProcCommand("uspGetReimbursementsBySeafarerId");
                db.AddInParameter(dbCommand, "@pcolSeafarerId", DbType.Int32, Int32.Parse(SeafarerId));
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRownIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pMReqId", DbType.Int32, mReqId);
                db.AddInParameter(dbCommand, "@pTReqId", DbType.Int32, tReqId);
                ReimbursementDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return ReimbursementDataTable;
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
                if (ReimbursementDataTable != null)
                {
                    ReimbursementDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/10/2011
        /// Description: insert/update reimbursement
        /// </summary>
        /// <param name="ReimbursementId"></param>
        /// <param name="ReimbursementName"></param>
        /// <param name="SeafarerId"></param>
        /// <param name="mReqId"></param>
        /// <param name="tReqId"></param>
        /// <param name="Amount"></param>
        /// <param name="Currency"></param>
        /// <param name="Remarks"></param>
        /// <param name="UserID"></param>
        public void SaveSeafarerReimbursement(string ReimbursementId, string ReimbursementName, string SeafarerId,
                string mReqId, string tReqId, string Amount, string Currency, string Remarks, string UserID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveReimbursement");
                db.AddInParameter(dbCommand, "@pSeafarerId", DbType.Int32, Int32.Parse(SeafarerId));
                db.AddInParameter(dbCommand, "@pReimbursementId", DbType.Int32, Int32.Parse(ReimbursementId));
                db.AddInParameter(dbCommand, "@pReimbursementName", DbType.String, ReimbursementName);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pmReqId", DbType.Int32, Int32.Parse(mReqId));
                db.AddInParameter(dbCommand, "@ptReqId", DbType.Int32, Int32.Parse(tReqId));
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pAmount", DbType.String,Amount);
                db.AddInParameter(dbCommand, "@pCurrency", DbType.Int32, Int32.Parse(Currency));
                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if(dbTrans != null)
                {
                    dbTrans.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 28/10/2011
        /// Description: delete seafarer reimbursement
        /// </summary>
        /// <param name="ReimbursementId"></param>
        /// <param name="UserId"></param>
        public void DeleteSeafarerReimbursement(string ReimbursementId, string UserId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteReimbursement");
                db.AddInParameter(dbCommand, "@pReimbursementId", DbType.Int32, Int32.Parse(ReimbursementId));
                db.AddInParameter(dbCommand, "@pUserid", DbType.String, UserId);
                db.ExecuteNonQuery(dbCommand, dTrans);
                dTrans.Commit();
            }
            catch (Exception ex)
            {
                dTrans.Rollback();
                throw ex;
            }
            finally 
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dTrans != null)
                {
                    dTrans.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 28/10/2011
        /// Description: Load Seafarer Reimbursement details
        /// </summary>
        /// <param name="SeafarerId"></param>
        /// <returns></returns>
        public IDataReader LoadSeafarerReimbursementDetails(string SeafarerId, string ReimbursementId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadReimbursementDetails");
                db.AddInParameter(dbCommand, "@pcolSeafarerId", DbType.Int32, Int32.Parse(SeafarerId));
                db.AddInParameter(dbCommand, "@pcolReimbursementId", DbType.Int32, Int32.Parse(ReimbursementId));
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
        #endregion
    }
}

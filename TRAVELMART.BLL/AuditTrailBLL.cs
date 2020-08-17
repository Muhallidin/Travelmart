using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;

namespace TRAVELMART.BLL
{
    public class AuditTrailBLL
    {
        /// <summary>            
        /// Date Created: 15/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert log audit trail
        /// </summary>
        public static void InsertLogAuditTrail(Int32 pID, string pSeqNo, String strLogDescription, String strFunction, String strPageName,
                                               DateTime DateGMT, DateTime CreatedDate, String CreatedBy)
        {
            DAL.AuditTrailDAL.InsertLogAuditTrail(pID, pSeqNo, strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, CreatedBy);
        }

        /// <summary>            
        /// Date Created: 17/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get audit trail logs
        /// </summary>
        public static DataTable GetAuditTrail(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            try
            {
                dt = AuditTrailDAL.GetAuditTrail(DateFrom, DateTo, StartRow, MaxRow);
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
            }
        }

        /// <summary>            
        /// Date Created: 17/11/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get audit trail logs count
        /// </summary>
        public static int GetAuditTrailCount(DateTime DateFrom, DateTime DateTo)
        {
            return AuditTrailDAL.GetAuditTrailCount(DateFrom, DateTo);
        }
    }
}

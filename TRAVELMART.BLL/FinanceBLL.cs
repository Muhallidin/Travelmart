using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using System.Data;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class FinanceBLL
    {
        #region Declarations
        FinanceDAL fDAL = new FinanceDAL();
        #endregion

        #region Reimbursement
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/10/2011
        /// Description: count seafarer reimbursement
        /// </summary>
        /// <param name="SeafarerId"></param>
        /// <returns></returns>
        public Int32 SelectReimbursementListbySeafarerCount(string SeafarerId, string mReqId, string tReqId, string E1TRId)
        {
            try
            {
                int mReq = 0;
                int tReq = 0;
                if (mReqId != null && mReqId != "")
                {
                    mReq = Int32.Parse(mReqId);
                }
                if (tReqId != null && tReqId != "")
                {
                    tReq = Int32.Parse(tReqId);
                }
                int iE1TRId = GlobalCode.Field2Int(E1TRId);
                if (iE1TRId == 0)
                {
                    tReq = 0;
                }
                return fDAL.SelectReimbursementListbySeafarerCount(SeafarerId, mReq, tReq);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectReimbursementListbySeafarer(string SeafarerId, Int32 startRowIndex, Int32 maximumRows, string mReqId, string tReqId, string E1TRId)
        {
            try
            {                
                int mReq = 0;
                int tReq = 0;
                if (mReqId != null && mReqId != "")
                {
                    mReq = Int32.Parse(mReqId);
                }
                if (tReqId != null && tReqId != "")
                {
                    tReq = Int32.Parse(tReqId);
                }
                int iE1TRId = GlobalCode.Field2Int(E1TRId);
                //if (iE1TRId == 0)
                //{
                //    tReq = 0;
                //}
                return fDAL.SelectReimbursementListbySeafarer(SeafarerId, startRowIndex, maximumRows, mReq, tReq);

            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                fDAL.SaveSeafarerReimbursement(ReimbursementId, ReimbursementName, SeafarerId, mReqId, tReqId,
                        Amount, Currency, Remarks, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                fDAL.DeleteSeafarerReimbursement(ReimbursementId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                return fDAL.LoadSeafarerReimbursementDetails(SeafarerId, ReimbursementId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

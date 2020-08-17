using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;

namespace TRAVELMART.BLL
{
    public class EmailBLL
    {
        /// <summary>
        /// Date Created: 27/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Load e-mail address
        /// </summary>
        public static IDataReader LoadEmailAddress(string BranchId)
        {
            try
            {
                IDataReader dr = null;
                dr = EmailDAL.LoadEmailAddress(BranchId);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Date Created: 28/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Save e-mail address          
        /// </summary>         
        public static void SaveEmailAddress(string BranchId, string EmailTo, string EmailCc)
        {
            EmailDAL.SaveEmailAddress(BranchId, EmailTo, EmailCc);
        }
          /// <summary>
        /// Date Created: 04/Mar/2013
        /// Created By:   Josephine Gad
        /// (description) Get email add of all Active Users
        /// </summary>
        /// <returns></returns>
        public static List<ActiveUserEmail> GetActiveUserEmail()
        {
            return EmailDAL.GetActiveUserEmail();
        }
    }
}

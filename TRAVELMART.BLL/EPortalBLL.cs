using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{  
    public class EPortalBLL
    {
        EPortalDAL DAL = new EPortalDAL();
        /// <summary>
        /// Date Created:   13/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get User List by Session ID
        /// --------------------------------------- 
        /// </summary>       
        public static List<UserList> GetUserBySessionID(string sSessionID)
        {
            return EPortalDAL.GetUserBySessionID(sSessionID);
        }
         /// <summary>
        /// Date Created:   11/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get User List with token
        /// --------------------------------------- 
        /// </summary>       
        public static List<UserList> GetUserList()
        {
             return EPortalDAL.GetUserList();
        }        
        
        /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   04/Feb/2016
        /// Description:    Get module ID used by Portal
        /// ===============================================================
        /// </summary>
        public string GetPortalModuleID(string sModuleCode)
        {
            return DAL.GetPortalModuleID(sModuleCode);
        }
    }
}

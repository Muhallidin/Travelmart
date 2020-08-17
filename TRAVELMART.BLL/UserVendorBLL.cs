using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;
//using System.Data.SqlClient;

namespace TRAVELMART.BLL
{
    public class UserVendorBLL
    {

        /// <summary>
        /// Date Created:   03/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Save User Service Provider
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sCreatedBy"></param>
        public static void SaveUserPortAgent(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
        {
            UserVendorDAL.SaveUserPortAgent(dt, sCreatedBy, strLogDescription, strFunction, strPageName);
        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get portagent list to be added and existing portagent in user's account
        /// </summary>
        public static List<PortAgentDTO> GetUserPortAgent(string sUser, string sPortAgentName, bool bIsToBeAdded)
        {
            return UserVendorDAL.GetUserPortAgent(sUser, sPortAgentName, bIsToBeAdded);
        }

        /// <summary>
        /// Date Created:   20/May/2014
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle vendor list to be added and existing vendor in user's account
        /// </summary>
        public static List<VehicleVendorDTO> GetUserVehicleVendor(string sUser, string sVehicleVendor, bool bIsToBeAdded)
        {
            return UserVendorDAL.GetUserVehicleVendor(sUser, sVehicleVendor, bIsToBeAdded);
        }
        /// <summary>
        /// Date Created:   20/May/2014
        /// Created By:     Josephine Gad
        /// (description)   Save User Vehicle Vendor
        /// </summary>        
        public static void SaveUserVehicleVendor(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
        {
            UserVendorDAL.SaveUserVehicleVendor(dt, sCreatedBy, strLogDescription, strFunction, strPageName);
        }

        /// <summary>
        /// Date Created:   12/Dec/2015
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Active User vendor
        /// --------------------------------------- 
        /// </summary>       
        public static int GetActiveUserVendor(string userID)
        {
            UserVendorDAL dal = new UserVendorDAL();
            return dal.GetActiveUserVendor(userID);
        }
        
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   03/Oct/2017
        /// Descrption:     Get Vendor list of Driver
        /// =============================================================     
        /// </summary>
        public static void DriverVendorGet(string sLoginUser, string sUserID, string sVendorToFind, bool bIsToBeAdded, string sVendorType,
           int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            UserVendorDAL.DriverVendorGet(sLoginUser, sUserID, sVendorToFind, bIsToBeAdded, sVendorType,
            iStartRow, iMaxRow, iLoadType, sOrderBy);
        }
        /// <summary>            
        /// Date Created:   03/Oct/2017
        /// Created By:     Josephine Monteza
        /// (description)   Add/Edit Driver Vendor Matrix
        /// ----------------------------------------
        /// </summary>
        public static void DriverVendorAddEdit(string sUserID, bool bIsToBeAdded, DataTable dtVendor, string sVendorType,
            String strLogDescription, String strFunction, String strPageName,
            DateTime DateGMT, String CreatedBy)
        {
            try
            {
                UserVendorDAL.DriverVendorAddEdit(sUserID, bIsToBeAdded, dtVendor, sVendorType,
                strLogDescription, strFunction, strPageName,
                DateGMT, CreatedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtVendor != null)
                {
                    dtVendor.Dispose();
                }
            }

        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   07/Oct/2017
        /// Descrption:     Get Vendor list of Greeter
        /// =============================================================     
        /// </summary>
        public static void GreeterVendorGet(string sLoginUser, string sUserID, string sVendorToFind, bool bIsToBeAdded, string sVendorType,
           int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            UserVendorDAL.GreeterVendorGet(sLoginUser, sUserID, sVendorToFind, bIsToBeAdded, sVendorType,
            iStartRow, iMaxRow, iLoadType, sOrderBy);
        }
        /// <summary>            
        /// Date Created:   07/Oct/2017
        /// Created By:     Josephine Monteza
        /// (description)   Add/Edit Greeter Vendor Matrix
        /// ----------------------------------------
        /// </summary>
        public static void GreeterVendorAddEdit(string sUserID, bool bIsToBeAdded, DataTable dtVendor, string sVendorType,
            String strLogDescription, String strFunction, String strPageName,
            DateTime DateGMT, String CreatedBy)
        {
            try
            {
                UserVendorDAL.GreeterVendorAddEdit(sUserID, bIsToBeAdded, dtVendor, sVendorType,
                strLogDescription, strFunction, strPageName,
                DateGMT, CreatedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtVendor != null)
                {
                    dtVendor.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   17/Oct/2017
        /// Created By:     Josephine Monteza
        /// (description)   Get Vendor list of user
        /// </summary>
        /// <param name="sLoginName"></param>
        /// <param name="sUsername"></param>
        /// <returns></returns>
        public static List<UserVendorList> UserVendorGet(string sLoginName, string sUsername, string sRoleToCheck)
        {
            return UserVendorDAL.UserVendorGet(sLoginName, sUsername, sRoleToCheck);
        }
    }
}

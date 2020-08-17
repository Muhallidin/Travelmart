using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;
using System.Data.SqlClient;

namespace TRAVELMART.BLL
{
    public class UserAccountBLL
    {
        public static UserAccountDAL dbDAL = new UserAccountDAL();
        /// <summary>
        /// Date Created:   11/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Create new user account for travelmart       
        /// ----------------------------------------------------
        /// Date Modified:  26/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete RoleID and RoleName parameter
        /// ----------------------------------------------------
        /// Date Modified:  26/10/2011
        /// Modified By:    Muhallidin G Wali
        /// (description)   Add DaysNo Parameter
        /// </summary>
        /// ----------------------------------------------------
        /// Date Modified:  07/05/2015
        /// Modified By:    Muhallidin G Wali
        /// (description)   Add Alternate email
        /// </summary>
        public static String CreateUserAccount(string LName, string FName, string Email, string UName, string PWD,
            string uId, string uNoOfDays, bool IsAltEmail, string AlternateEmail, string sCreatedBy, string sContact)
        {
            //string strMessage = UserAccountDAL.CreateUserAccount(LName, FName, Email, UName, PWD, RoleID, RoleName, uId);
            //string strMessage = UserAccountDAL.CreateUserAccount(LName, FName, Email, UName, PWD, uId);

            string strMessage = UserAccountDAL.CreateUserAccount(LName, FName, Email, UName, PWD, uId, uNoOfDays,
                IsAltEmail, AlternateEmail, sCreatedBy, sContact);
            return strMessage;
        }
        /// <summary>
        /// Date Created:   26/10/2011
        /// Created By:     Josephnine Gad
        /// (description)   Add or update user role and branch
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RoleName"></param>
        /// <param name="BranchID"></param>
        public static void InsertUpdateUserInRole(string UserName, string RoleName, string BranchID, string UserLogin)
        {
            UserAccountDAL.InsertUpdateUserInRole(UserName, RoleName, BranchID, UserLogin);
        }
        /// <summary>        
        /// Date Created:   26/10/2011
        /// Created By:     Josephnine Gad
        /// (description)   Delete user role    
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RoleName"></param>
        /// <param name="UserLogin"></param>
        public static void DeleteUserRole(string UserName, string RoleName, string UserLogin)
        {
            UserAccountDAL.DeleteUserRole(UserName, RoleName, UserLogin);
        }
        /// <summary>
        /// Date Created:   26/10/2011
        /// Created By:     Josephnine Gad
        /// (description)   Update primary user role
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="RoleName"></param>
        /// <param name="IsPrimary"></param>
        public static void UpdatePrimaryRole(string UserID, string RoleName, bool IsPrimary)
        {
            UserAccountDAL.UpdatePrimaryRole(UserID, RoleName, IsPrimary);
        }
        /// <summary>
        /// Date Created: 05/07/2011
        /// Created By: Marco Abejar
        /// (description) Get user firstname for welcome message    
        /// </summary>
        /// 
        public static String GetUserFirstname(string Uname)
        {
            string strFirstname = UserAccountDAL.GetUserFirstname(Uname);
            return strFirstname;
        }
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Add or update role    
        /// </summary>
        /// 
        public static String AddUpdateRole(string rolename, string rId, string roledesc)
        {
            string strMessage = UserAccountDAL.AddUpdateRole(rolename, rId, roledesc);
            return strMessage;
        }
        /// <summary>
        /// Date Created: 05/07/2011
        /// Created By: Marco Abejar
        /// (description) Add user firstname,lastname, and username      
        /// </summary>
        /// 
        public static void DeleteUser(string uid)
        {
            UserAccountDAL.DeleteUser(uid);
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get list of users                        
        /// </summary>  
        public static DataTable GetUsers(string role, string name, string userLogin, string userLoginRole, string sOrderBy)
        {
            DataTable dtUsers = null;
            try
            {
                dtUsers = UserAccountDAL.GetUsers(role, name, userLogin, userLoginRole, sOrderBy);
                return dtUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtUsers != null)
                {
                    dtUsers.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of users    
        /// ----------------------------------
        /// Date Modified:  28/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to SqlDataReader
        /// </summary> 

        public static SqlDataReader GetUserInfo(string uid)
        {
            try
            {
                return UserAccountDAL.GetUserInfo(uid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //DataTable dtUsers = null;
            //try
            //{
            //    dtUsers = UserAccountDAL.GetUserInfo(uid);
            //    return dtUsers;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dtUsers != null)
            //    {
            //        dtUsers.Dispose();
            //    }
            //}
        }
        /// <summary>            
        /// Date Created:   26/10/2011
        /// Created By:     Josephine Gad
        /// (description)   get user info by user name    
        /// ---------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ---------------------------------------------
        /// </summary> 
        //public static IDataReader GetUserInfoByName(string UserName)
        //{
        //    IDataReader drUsers = null;
        //    try
        //    {
        //        drUsers = UserAccountDAL.GetUserInfoByName(UserName);
        //        return drUsers;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (drUsers != null)
        //        {
        //            drUsers.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   15/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user account using list
        /// ---------------------------------------------
        /// Date Modified:  27/Nov/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add session
        /// ---------------------------------------------
        /// </summary> 
        public static List<UserAccountList> GetUserInfoListByName(string UserName)
        {
            List<UserAccountList> list = new List<UserAccountList>();
            List<UserAccountGenericClass> listUser = new List<UserAccountGenericClass>();
            try
            {
                HttpContext.Current.Session["UserPrimaryDetails"] = null;
                listUser = UserAccountDAL.GetUserInfoListByName(UserName);

                list = listUser[0].UserAccountList;
                HttpContext.Current.Session["UserPrimaryDetails"] = listUser[0].UserPrimaryDetails;

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>            
        /// Date Created:   26/10/2011
        /// Created By:     Josephine Gad
        /// (description)   get user id by user name                   
        /// </summary> 
        public static string GetUserID(string UserName)
        {
            return UserAccountDAL.GetUserID(UserName);
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get user roles                        
        /// </summary>
        /// 


        public static DataTable GetUserRoles()
        {

            DataTable dtUsers = null;
            try
            {
                dtUsers = UserAccountDAL.GetUserRoles();
                return dtUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtUsers != null)
                {
                    dtUsers.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   25/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Check if user is in role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public static bool IsUserInRole(string userName, string RoleName)
        {
            return UserAccountDAL.IsUserInRole(userName, RoleName);
        }
        /// <summary>
        /// Date Created:   25/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user roles by User ID     
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataTable GetRolesByUser(string userID, string LoginName)
        {
            DataTable dtUsers = null;
            try
            {
                dtUsers = UserAccountDAL.GetRolesByUser(userID, LoginName);
                return dtUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtUsers != null)
                {
                    dtUsers.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description) Delete role                    
        /// </summary>   
        /// 
        public static void DeleteRole(string role)
        {
            UserAccountDAL.DeleteRole(role);
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get role to edit  
        /// ---------------------------------------------------
        /// Date Modified:  27/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to SqlDataReader
        /// </summary>  
        public static SqlDataReader GetRole(string rid)
        {
            try
            {
                return UserAccountDAL.GetRole(rid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created: 11/08/2011
        /// Created By: Marco Abejar
        /// (description) Message for user creation            
        /// </summary>    
        public static string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        /// <summary>
        /// Date Created: 19/10/2011
        /// Created By: Charlene Remotigue
        /// (description) Save user role with branchid            
        /// </summary>  
        public static void UpdateUserBranch(string userName, string BranchId, string UserId, bool isActive, string oldUser)
        {
            try
            {
                UserAccountDAL.UpdateUserBranch(userName, BranchId, UserId, isActive, oldUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Date Created: 19/10/2011
        ///// Created By: Charlene Remotigue
        ///// (description) get branchid            
        ///// </summary>  
        //public static string LoadUserBranch(string userName)
        //{
        //    try
        //    {
        //        return UserAccountDAL.LoadUserBranch(userName);
        //    }
        //    catch (Exception ex)
        //    { 
        //        throw ex; 
        //    }
        //}
        /// <summary>
        /// Date Created:   19/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Branch details by Username and user role
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>
        public static IDataReader GetUserBranchDetails(string UserName, string UserRole)
        {
            return UserAccountDAL.GetUserBranchDetails(UserName, UserRole);
        }
        /// <summary>
        /// Date Created:   19/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get User Primary Role
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static string GetUserPrimaryRole(string UserName)
        {
            return UserAccountDAL.GetUserPrimaryRole(UserName);
        }
        /// <summary>
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get User Email address
        /// </summary>
        /// <param name="sRole"></param>
        /// <param name="sBranchID"></param>
        /// <param name="sRegion"></param>
        /// <returns></returns>
        public static DataTable GetUserEmail(string sRole, string sBranchID, string sCountry)
        {
            DataTable dt = null;
            try
            {
                dt = UserAccountDAL.GetUserEmail(sRole, sBranchID, sCountry);
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
        /// Date Created:   22/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user roles access
        /// </summary>
        public static DataTable GetUserRolesAccess(string RoleName)
        {
            DataTable dt = null;
            try
            {
                dt = UserAccountDAL.GetUserRolesAccess(RoleName);
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
        /// Date Created:   12/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get User Date Range
        /// -------------------------------------------       
        /// </summary> 
        //public static int GetUserDateRange(string UserName)
        //{
        //    try
        //    {
        //        return UserAccountDAL.GetUserDateRange(UserName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Date Created:   22/08/2012
        /// Created By:     Jefferson S. Bermundo
        /// Description:    Get User Branch Id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static int GetUserBranchId(string userName, string roleName)
        {
            return UserAccountDAL.GetUserBranchId(userName, roleName);
        }

        /// <summary>
        /// Date Created:   22/08/2012
        /// Created By:     Jefferson S. Bermundo
        /// Description:    Get User Vendor Id 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static int GetUserVendorId(string userName, string roleName)
        {
            return UserAccountDAL.GetUserVendorId(userName, roleName);
        }

        /// <summary>
        /// Date Created:   22/08/2012
        /// Created By:     Jefferson S. Bermundo
        /// Description:    Get User Branch Name
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static String GetUserBranchName(string userName, string roleName)
        {
            return UserAccountDAL.GetUserBranchName(userName, roleName);
        }
        /// <summary>
        /// Date Created:   31/08/2012
        /// Created By:     Josephine Gad
        /// (description)   Update User SessionID
        /// </summary>  
        public static void UpdateUserSessionID(string userName, string SessionID)
        {
            UserAccountDAL.UpdateUserSessionID(userName, SessionID);
        }
        /// <summary>
        /// Date Created:   30/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Update User SessionID from LDAP
        /// </summary>  
        public static void UpdateUserSessionID_LDAP(string userName, string SessionID)
        {
            UserAccountDAL.UpdateUserSessionID_LDAP(userName, SessionID);
        }
        /// <summary>
        /// Date Created:   31/08/2012
        /// Created By:     Josephine Gad
        /// (description)   Get User SessionID
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static String GetUserSessionID(string userName)
        {
            return UserAccountDAL.GetUserSessionID(userName);
        }
        /// <summary>
        /// Date Created:   30/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get User SessionID from LDAP
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static String GetUserSessionID_LDAP(string userName)
        {
            return UserAccountDAL.GetUserSessionID_LDAP(userName);
        }
        /// <summary>
        /// Date Created:   24/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get User's Vessel and Vessel not in his access
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sVesselCode"></param>
        /// <param name="sVesselName"></param>
        /// <param name="bIsToBeAdded"></param>
        /// <returns></returns>
        public static List<VesselDTO> GetUserVessel(string sUser, string sVesselCode, string sVesselName, bool bIsToBeAdded)
        {
            return UserAccountDAL.GetUserVessel(sUser, sVesselCode, sVesselName, bIsToBeAdded);
        }
        /// <summary>
        /// Date Created:   24/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Save User Vessel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sCreatedBy"></param>
        public static void SaveUserVessel(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
        {
            UserAccountDAL.SaveUserVessel(dt, sCreatedBy, strLogDescription, strFunction, strPageName);
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get User's Airport and airport not yet in his access
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sAirportCode"></param>
        /// <param name="sAirportName"></param>
        /// <param name="bIsToBeAdded"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetUserAirport(string sUser, string sAirportCode, string sAirportName, bool bIsToBeAdded)
        {
            return UserAccountDAL.GetUserAirport(sUser, sAirportCode, sAirportName, bIsToBeAdded);
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Save User Airport
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sCreatedBy"></param>
        public static void SaveUserAirport(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
        {
            UserAccountDAL.SaveUserAirport(dt, sCreatedBy, strLogDescription, strFunction, strPageName);
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Get seaport list to be added and existing seaport in user's account
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sSeaportCode"></param>
        /// <param name="sSeaportName"></param>
        /// <param name="bIsToBeAdded"></param>
        /// <returns></returns>
        public static List<Seaport> GetUserSeaport(string sUser, string sSeaportCode, string sSeaportName, bool bIsToBeAdded)
        {
            return UserAccountDAL.GetUserSeaport(sUser, sSeaportCode, sSeaportName, bIsToBeAdded);
        }
        /// <summary>
        /// Date Created:   25/09/2012
        /// Created By:     Josephine Gad
        /// (description)   Save User Seaport
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sCreatedBy"></param>
        public static void SaveUserSeaport(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
        {
            UserAccountDAL.SaveUserSeaport(dt, sCreatedBy, strLogDescription, strFunction, strPageName);
        }
        /// <summary>
        /// Date Created:   15/Jul/2013
        /// Created By:     Josephine Gad
        /// (description)   Save User Region
        public static void SaveUserRegion(string sUserName)
        {
            UserAccountDAL.SaveUserRegion(sUserName);
        }
        
       
        ///===================================================
        /// <summary>            
        /// Date Created: 03/11/2015
        /// Created By: Muhallidin Wali
        /// (description) Get alternate email list of users                        
        ///===================================================
        public static string GetUserInfoAlterEmail(string uid)
        {
            try
            {
                return UserAccountDAL.GetUserInfoAlterEmail(uid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   04/11/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get User list
        /// </summary>
        public static List<UserList_LDAP> GetUserList(DateTime dtFrom, DateTime dtTo, string sRole, bool bIsWithAlternateEmail,
            bool bIsSearchByName, string sUserOrEmail)
        {
            return UserAccountDAL.GetUserList(dtFrom, dtTo, sRole, bIsWithAlternateEmail, bIsSearchByName, sUserOrEmail);
        }
        /// Date Created:   04/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Send Email to User
        /// </summary>        
        public static void EmailUserPassword(string sUsername, string sPassword, string sEmail)
        {
            UserAccountDAL.EmailUserPassword(sUsername, sPassword, sEmail);
        }
        /// Date Created:   10/Nov/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Get Immigration User List to Extract
        /// </summary>
        public static DataTable GetImmigrationUsersToExtract(string sRole, bool bIsWithAlternateEmail, string sPassword, DataTable dtUser)
        {
            try
            {
                return UserAccountDAL.GetImmigrationUsersToExtract(sRole, bIsWithAlternateEmail, sPassword, dtUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtUser != null)
                {
                    dtUser.Dispose();
                }
            }
        }
        /// Date Created:   11/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Send Email to User
        /// </summary>        
        public static void EmailNewUser(string sLastName, string sFirstName, string sUsername, string sEmail,
            string sURL, bool bIsNew, bool bIsLDAPOn)
        {
            UserAccountDAL.EmailNewUser(sLastName, sFirstName, sUsername, sEmail, sURL, bIsNew, bIsLDAPOn);
        }
        /// Date Created:   13/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Update/Insert user to LDAP table
        /// </summary>
        public static void LDAPImmigrationUpdate(string sUsername, string sEmail, string sAlternateEmail, bool bIsDelete)
        {
            UserAccountDAL.LDAPImmigrationUpdate(sUsername, sEmail, sAlternateEmail, bIsDelete);
        }
    }
}

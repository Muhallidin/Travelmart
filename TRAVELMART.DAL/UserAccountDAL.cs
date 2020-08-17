using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using TRAVELMART.Common;
using System.Web.Security;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRAVELMART.DAL
{
    public class UserAccountDAL
    {
        #region Declarations
        private static string ConnStr { get; set; }
        public static SqlConnection SQLConn { get; set; }
        #endregion

        #region Events
        public UserAccountDAL()
        {
            ConnStr = ConnectionSetting.GetConnectionSecuritySetting(); // ConfigurationManager.ConnectionStrings["APPSERVICESConnectionString"].ToString();
        }

        public static void Dispose()
        {
            if (SQLConn != null)
            {
                try
                {
                    SQLConn.Dispose();
                    SQLConn = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Date Created: 11/08/2011
        /// Created By: Marco Abejar
        /// (description) Create new user account for travelmart            
        /// </summary>     
        /// 
        /// <summary>
        /// Date Created: 11/08/2011
        /// Created By: Muhallidin G Wali
        /// (description) Add Parameter colDaysNoTinyint       
        /// </summary>     


        //public static String CreateUserAccount(string LName, string FName, string Email, string UName, string PWD, string RoleID, string RoleName, string uId)
        //public static String CreateUserAccount(string LName, string FName, string Email, string UName, string PWD, string uId)

        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        public static String CreateUserAccount(string LName, string FName, string Email, string UName, string PWD,
             string uId, string UNumberDays, bool IsAltEmail, string AlternateEmail, string sCreatedBy, string sContactNo)
        {
            SqlCommand command = null;
            try
            {

                HttpContext.Current.Session["strCreateMessge"] = "Username already exist.";
                if (uId.Length > 0)
                {
                    using (SQLConn = new SqlConnection(ConnStr))
                    {
                        SQLConn.Open();

                        command = new SqlCommand("uspUpdateUser", SQLConn);
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter param = new SqlParameter("@pUserID", uId);
                        param.Direction = ParameterDirection.Input;
                        param.SqlDbType = SqlDbType.VarChar;

                        SqlParameter param1 = new SqlParameter("@pEmail", Email);
                        param1.Direction = ParameterDirection.Input;
                        param1.SqlDbType = SqlDbType.VarChar;
                        //SqlParameter param2 = new SqlParameter("@pRoleId", RoleID);
                        //param1.Direction = ParameterDirection.Input;
                        //param1.SqlDbType = SqlDbType.VarChar;

                        SqlParameter param3 = new SqlParameter("@pcolUsernameVarchar", UName);
                        param.Direction = ParameterDirection.Input;
                        param.SqlDbType = SqlDbType.VarChar;

                        SqlParameter param4 = new SqlParameter("@pcolFNameVarchar", FName);
                        param1.Direction = ParameterDirection.Input;
                        param1.SqlDbType = SqlDbType.VarChar;


                        SqlParameter param5 = new SqlParameter("@pcolLNameVarchar", LName);
                        param1.Direction = ParameterDirection.Input;
                        param1.SqlDbType = SqlDbType.VarChar;



                        if (UNumberDays == "")
                        {
                            UNumberDays = null;
                        }

                        SqlParameter param6 = new SqlParameter("@pcolDaysNoTinyint", UNumberDays);
                        param6.Direction = ParameterDirection.Input;
                        param6.SqlDbType = SqlDbType.VarChar;

                        SqlParameter param7 = new SqlParameter("@pIsAltEmail", IsAltEmail);
                        param7.Direction = ParameterDirection.Input;
                        param7.SqlDbType = SqlDbType.Bit;

                        SqlParameter param8 = new SqlParameter("@pAlternateEmail", AlternateEmail);
                        param8.Direction = ParameterDirection.Input;
                        param8.SqlDbType = SqlDbType.VarChar;

                        SqlParameter param9 = new SqlParameter("@pModifiedBy", sCreatedBy);
                        param9.Direction = ParameterDirection.Input;
                        param9.SqlDbType = SqlDbType.VarChar;

                        SqlParameter param10 = new SqlParameter("@pContactNo", sContactNo);
                        param10.Direction = ParameterDirection.Input;
                        param10.SqlDbType = SqlDbType.VarChar;

                        SqlParameter[] ParamArray = { param, param1, param3, param4, param5, param6, param7, param8, param9, param10 };
                        command.Parameters.AddRange(ParamArray);
                        command.ExecuteNonQuery();
                        HttpContext.Current.Session["strCreateMessge"] = "User account has been updated successfully.";
                    }
                }
                else
                {
                    MembershipCreateStatus status;
                    MembershipUser newUser = Membership.CreateUser(UName, PWD, Email, "question?", "yes", true, out status);
                    if (newUser == null)
                    {
                        HttpContext.Current.Session["strCreateMessge"] = GetErrorMessage(status);
                    }
                    else
                    {
                        //Roles.AddUserToRole(UName, RoleName);
                        UserAccountDAL.InsertUserInfo(LName, FName, UName, UNumberDays, Email, IsAltEmail, AlternateEmail, sCreatedBy, sContactNo);
                        HttpContext.Current.Session["strCreateMessge"] = "New user has been created successfully.";
                    }
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
            }
            return HttpContext.Current.Session["strCreateMessge"].ToString();
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                if (!IsUserInRole(UserName, RoleName))
                {
                    Roles.AddUserToRole(UserName, RoleName);
                }
                if (RoleName == TravelMartVariable.RoleHotelVendor || RoleName == TravelMartVariable.RoleVehicleVendor
                    || RoleName == TravelMartVariable.RolePortSpecialist || RoleName == TravelMartVariable.RoleImmigration)
                {
                    if (GlobalCode.Field2Int(BranchID) > 0)
                    {
                        command = db.GetStoredProcCommand("uspUpdateUserRoleBranch");
                        db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                        db.AddInParameter(command, "@pRoleName", DbType.String, RoleName);
                        db.AddInParameter(command, "@pBranchID", DbType.Int64, Int64.Parse(BranchID));
                        db.AddInParameter(command, "@pUpdatedBy", DbType.String, UserLogin);
                        db.ExecuteNonQuery(command);
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                command = db.GetStoredProcCommand("uspUpdateUserRolePrimary");
                db.AddInParameter(command, "@pUserId", DbType.String, UserID);
                db.AddInParameter(command, "@pRoleName", DbType.String, RoleName);
                db.AddInParameter(command, "@pIsPrimary", DbType.Boolean, IsPrimary);
                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                if (IsUserInRole(UserName, RoleName))
                {
                    Roles.RemoveUserFromRole(UserName, RoleName);
                    if (RoleName == TravelMartVariable.RoleHotelVendor)
                    {
                        command = db.GetStoredProcCommand("uspDeleteUserRoleBranch");
                        db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                        db.AddInParameter(command, "@pRoleName", DbType.String, RoleName);
                        db.AddInParameter(command, "@pUpdatedBy", DbType.String, UserLogin);
                        db.ExecuteNonQuery(command);
                    }
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Add or update role    
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        /// 
        public static String AddUpdateRole(string rolename, string rID, string roledesc)
        {
            if (rID.Length > 0)
            {
                SqlConnection SQLConn = new SqlConnection(ConnStr);
                SqlCommand command = new SqlCommand("uspUpdateRole", SQLConn);
                try
                {
                    SQLConn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter("@pRoleId", rID);
                    param.Direction = ParameterDirection.Input;
                    param.SqlDbType = SqlDbType.VarChar;
                    SqlParameter param1 = new SqlParameter("@pRoleName", rolename);
                    param1.Direction = ParameterDirection.Input;
                    param1.SqlDbType = SqlDbType.VarChar;
                    SqlParameter param2 = new SqlParameter("@pRoleDesc", roledesc);
                    param2.Direction = ParameterDirection.Input;
                    param2.SqlDbType = SqlDbType.VarChar;

                    SqlParameter[] ParamArray = { param, param1, param2 };
                    command.Parameters.AddRange(ParamArray);
                    command.ExecuteNonQuery();

                    HttpContext.Current.Session["strCreateMessge"] = "User role has been updated successfully.";
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
                    SQLConn.Close();
                    SQLConn.Dispose();
                }
            }
            else
            {
                Roles.CreateRole(rolename);
                HttpContext.Current.Session["strCreateMessge"] = "User role has been created successfully.";
            }
            return HttpContext.Current.Session["strCreateMessge"].ToString();

        }
        /// <summary>
        /// Date Created: 05/07/2011
        /// Created By: Marco Abejar
        /// (description) Add user firstname,lastname, and username      
        /// =========================================================
        /// Date Modified:  13/Nov/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Added Email parameter 
        ///                 Changed SqlConnection to DbConnection
        /// </summary>
        /// 
        public static void InsertUserInfo(string lname, string fname, string uname, string UNumberDays, string sEmail, 
            bool IsAltEmail, string AlternateEmail, string sCreatedBy,string sContactNo)
        {
                              
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {

                command = db.GetStoredProcCommand("uspInsertUserInfo");
                db.AddInParameter(command, "@pcolFNameVarchar", DbType.String, fname);
                db.AddInParameter(command, "@pcolLNameVarchar", DbType.String, lname);
                db.AddInParameter(command, "@pcolUsernameVarchar", DbType.String, uname);

                int iNoOfDays = GlobalCode.Field2Int(UNumberDays);
                db.AddInParameter(command, "@pcolDaysNoTinyint", DbType.Int32, iNoOfDays);
                db.AddInParameter(command, "@pEmail", DbType.String, sEmail);

                db.AddInParameter(command, "@pIsAltEmail", DbType.Boolean, IsAltEmail);
                db.AddInParameter(command, "@pAlternateEmail", DbType.String, AlternateEmail);

                db.AddInParameter(command, "@pcolCreatedByVarchar", DbType.String, sCreatedBy);
                db.AddInParameter(command, "@pContactNo", DbType.String, sContactNo);

                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }                               
        }
        /// <summary>
        /// Date Created: 05/07/2011
        /// Created By: Marco Abejar
        /// (description) Get user firstname for welcome message    
        /// </summary>
        /// 
        public static string GetUserFirstname(string uname)
        {
            SqlConnection SQLConn = new SqlConnection(ConnStr);
            SqlCommand command = new SqlCommand("uspGetUserFirstName", SQLConn);
            try
            {
                SQLConn.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@pcolUsernameVarchar", uname);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(param);

                string strFirstname = Convert.ToString(command.ExecuteScalar());
                return strFirstname;
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
                SQLConn.Close();
                SQLConn.Dispose();
            }
        }
        /// <summary>
        /// Date Created:   05/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Add user firstname,lastname, and username   
        /// -----------------------------------
        /// Date Modified:  26/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   use DbTransaction trans
        /// </summary>
        /// 
        public static void DeleteUser(string uid)
        {
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                command = db.GetStoredProcCommand("uspDeleteUser");
                db.AddInParameter(command, "pUserID", DbType.String, uid);

                db.ExecuteNonQuery(command);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            //SqlConnection SQLConn = new SqlConnection(ConnStr);
            //SqlCommand command = new SqlCommand("uspDeleteUser", SQLConn);
            //try
            //{
            //    SQLConn.Open();
            //    command.CommandType = CommandType.StoredProcedure;
            //    SqlParameter param = new SqlParameter("@pUserID", uid);
            //    param.Direction = ParameterDirection.Input;
            //    param.SqlDbType = SqlDbType.VarChar;

            //    command.Parameters.Add(param);
            //    command.ExecuteNonQuery();

            //}
            //catch (Exception ex)
            //{
            //    throw ex;

            //}
            //finally
            //{
            //    if (command != null)
            //    {
            //        command.Dispose();
            //    }
            //    SQLConn.Close();
            //    SQLConn.Dispose();
            //}
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get list of users                        
        /// ---------------------------------
        /// Date Modified:  20/Feb/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter sOrderBy
        /// ---------------------------------
        /// Date Modified:  10/Oct/2017
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter sOrderBy
        /// ---------------------------------
        /// </summary>  
        public static DataTable GetUsers(string role, string name, string userLogin, string userLoginRole, string sOrderBy)
        {
            //SqlConnection SQLConn = new SqlConnection(ConnStr);
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand com = null;
            DataTable dt = new DataTable();
            DataSet ds = null;

            try
            {
                com = db.GetStoredProcCommand("uspGetUsers");
                db.AddInParameter(com, "@pRoleName", DbType.String, role);
                db.AddInParameter(com, "@pName", DbType.String, name);
                db.AddInParameter(com, "@pLoginName", DbType.String, userLogin);
                db.AddInParameter(com, "@pLoginRole", DbType.String, userLoginRole);
                db.AddInParameter(com, "@pOrderBy", DbType.String, sOrderBy);
                ds = db.ExecuteDataSet(com);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
                if (dt!= null)
                {
                    dt.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of users                        
        /// -----------------------------
        /// Date Modified:  27/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to SqlDataReader
        /// </summary> 
        public static SqlDataReader GetUserInfo(string uid)
        {
            SqlConnection SQLConn = new SqlConnection(ConnStr);
            SqlDataReader SFDataTable = null;
            SqlCommand command = new SqlCommand("uspGetUserInfo", SQLConn);

            try
            {
                SQLConn.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@pUserID", uid);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(param);
                return command.ExecuteReader();
                //using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                //{
                //    adapter.Fill(SFDataTable);
                //}
                //return SFDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //SQLConn.Close();
                //SQLConn.Dispose();

                if (command != null)
                {
                    command.Dispose();
                }
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
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
        public static IDataReader GetUserInfoByName(string UserName)
        {
            IDataReader dr = null;
            DbCommand command = null;

            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetUserInfoByName");
                db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                dr = db.ExecuteReader(command);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>     
        /// Date Created:    15/08/2012
        /// Created By:      Josephine Gad
        /// (description)    Get user account using list
        /// ----------------------------------------------
        /// Date Modified:   27/Nov/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change UserAccountList to UserAccountGenericClass
        /// </summary> 
        public static List<UserAccountGenericClass> GetUserInfoListByName(string UserName)
        {
            List<UserAccountGenericClass> list = new List<UserAccountGenericClass>();
            DbCommand command = null;
            DataSet ds = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetUserInfoByName");
                db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                ds = db.ExecuteDataSet(command);

                list.Add(new UserAccountGenericClass()
                {
                    UserAccountList = (from a in ds.Tables[0].AsEnumerable()
                                       select new UserAccountList
                                       {
                                           sRole = GlobalCode.Field2String(a["ROLE"]),
                                           bIsPrimary = GlobalCode.Field2Bool(a["IsPrimary"]),
                                           iDayNo = GlobalCode.Field2Int(a["colDaysNoTinyint"]),
                                           bIsAirportMeetGreet = GlobalCode.Field2Bool(a["colIsAirportMeetGreet"]),
                                           bIsSeaportMeetGreet = GlobalCode.Field2Bool(a["colIsSeaportMeetGreet"])
                                       }).ToList(),
                    UserPrimaryDetails = (from a in ds.Tables[1].AsEnumerable()
                                          select new UserPrimaryDetails
                                          {
                                              sFirstName = GlobalCode.Field2String(a["FNAME"]),
                                              iBranchID = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                              sBranchName = GlobalCode.Field2String(a["colVendorBranchNameVarchar"]),
                                              iVendorID = GlobalCode.Field2Int(a["colVendorIDInt"])
                                          }).ToList(),
                });
                return list;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   26/10/2011
        /// Created By:     Josephine Gad
        /// (description)   get user info by user name   
        /// -------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static string GetUserID(string UserName)
        {
            string sReturn = "";
            try
            {
                using (IDataReader dr = GetUserInfoByName(UserName))
                {
                    if (dr.Read())
                    {
                        sReturn = dr["UID"].ToString();
                    }
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                Roles.DeleteRole(role);
            }
            catch
            {
            }
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get user roles                        
        /// </summary>
        public static DataTable GetUserRoles()
        {
            SqlConnection SQLConn = new SqlConnection(ConnStr);
            DataTable SFDataTable = new DataTable();
            SqlCommand command = new SqlCommand("uspGetUserRoles", SQLConn);

            try
            {
                SQLConn.Open();
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(SFDataTable);
                }

                return SFDataTable;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                SQLConn.Close();
                SQLConn.Dispose();

                if (command != null)
                {
                    command.Dispose();
                }
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   25/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Check if user is in role
        /// ========================================
        /// Date Created:   12/12/2011
        /// Created By:     Muhallidin G Wali
        /// (description)   Adding adding Linq statement to minize the looping
        /// </summary>
        /// 
        /// <param name="userName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public static bool IsUserInRole(string userName, string RoleName)
        {
            bool bReturn = false;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                command = db.GetStoredProcCommand("uspGetUserRolesByUserName");
                db.AddInParameter(command, "@pUserName", DbType.String, userName);
                db.AddInParameter(command, "@pLoginName", DbType.String, RoleName);
                dt = db.ExecuteDataSet(command).Tables[0];

                var query = from p in dt.AsEnumerable()
                            where (string)p.Field<string>("RoleName") == RoleName
                            select p;

                foreach (DataRow r in query)
                {
                    if (r["RoleName"].ToString() == RoleName)
                    {
                        bReturn = (bool)r["IsSelected"];
                    }
                }


                //foreach (DataRow r in dt.Rows)
                //{
                //    if (r["RoleName"].ToString() == RoleName)
                //    {
                //        bReturn = (bool)r["IsSelected"];
                //        goto exit_Here;
                //    }
                //}

                return bReturn;
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
        /// Date Created:   25/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user roles by User ID  
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataTable GetRolesByUser(string userID, string LoginName)
        {
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                command = db.GetStoredProcCommand("uspGetUserRolesByUser");
                db.AddInParameter(command, "@pUserID", DbType.String, userID);
                db.AddInParameter(command, "@pLoginName", DbType.String, LoginName);
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
            SqlConnection SQLConn = new SqlConnection(ConnStr);
            SqlDataReader dr = null;
            SqlCommand command = new SqlCommand("uspGetRoleToEdit", SQLConn);

            try
            {
                SQLConn.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@pRoleId", rid);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(param);

                //using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                //{
                //    adapter.Fill(SFDataTable);
                //}
                dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //SQLConn.Close();
                //SQLConn.Dispose();

                if (command != null)
                {
                    command.Dispose();
                }
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
        public static void UpdateUserBranch(string userName, string BranchId, string UserId, bool isActive, string OldUser)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveUserBranch");
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, userName);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.String, BranchId);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pIsActiveBit", DbType.Boolean, isActive);
                db.AddInParameter(dbCommand, "@pOldUser", DbType.String, OldUser);
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
                connection.Close();
                connection.Dispose();

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
        ///// <summary>
        ///// Date Created: 19/10/2011
        ///// Created By: Charlene Remotigue
        ///// (description) get branchid            
        ///// </summary>  
        //public static string LoadUserBranch(string userName)
        //{
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;
        //    string BranchId = string.Empty;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspSelectUserBranch");
        //        db.AddInParameter(dbCommand, "@pUserName", DbType.String, userName);
        //        using (IDataReader dr = db.ExecuteReader(dbCommand))
        //        {
        //            if (dr.Read())
        //            {
        //                BranchId = dr["colBranchIDInt"].ToString();
        //            }
        //        }
        //        return BranchId;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   19/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Branch details by Username and user role
        /// -------------------------------------------------
        /// Date Modified:   27/11/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to IDataReader
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>
        public static IDataReader GetUserBranchDetails(string UserName, string UserRole)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectUserBranchDetails");
                db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                db.AddInParameter(command, "@pUserRole", DbType.String, UserRole);
                dr = db.ExecuteReader(command);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (dt != null)
                //{
                //    dt.Dispose();
                //}
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   19/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get User Primary Role
        /// -----------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static string GetUserPrimaryRole(string UserName)
        {
            string sPrimaryRole = "";
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
                command = db.GetStoredProcCommand("uspGetUserPrimaryRole");
                db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    sPrimaryRole = dr["RoleName"].ToString();
                }
                return sPrimaryRole;
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
            }
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
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetUserEmail");
                db.AddInParameter(command, "@pRoleName", DbType.String, sRole);
                db.AddInParameter(command, "@pBranchID", DbType.Int32, Int32.Parse(sBranchID));
                db.AddInParameter(command, "@pCountryID", DbType.Int32, Int32.Parse(sCountry));
                dt = db.ExecuteDataSet(command).Tables[0];
                //foreach (DataRow r in dt.Rows)
                //{
                //    if (sReturn != "")
                //    {
                //        sReturn += "; ";
                //    }
                //    sReturn += r["Email"];
                //}
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
        /// Date Created:   22/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user roles access
        /// </summary>
        public static DataTable GetUserRolesAccess(string RoleName)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
                command = db.GetStoredProcCommand("uspGetRoleAccess");
                db.AddInParameter(command, "@pRoleName", DbType.String, RoleName);
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
        /// Date Created:   12/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get User Date Range
        /// -------------------------------------------       
        /// </summary> 
        //public static int GetUserDateRange(string UserName)
        //{
        //    int iReturn = 0;
        //    IDataReader dr = null;
        //    try
        //    {
        //        dr = GetUserInfoByName(UserName);
        //        if (dr.Read())
        //        {
        //            iReturn = int.Parse(dr["colDaysNoTinyint"].ToString());
        //        }
        //        return iReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 31/01/2012
        /// Description: get all emails for hotel branch
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<HotelEmail> GetHotelUserEmail(Int32 BranchId, String UserId)
        {
            DataTable dt = null;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbCommand dbCommand = null;
            //List<HotelEmail> HotelEmail = new List<HotelEmail>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelUserEmail");
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                var HotelEmail = (from n in dt.AsEnumerable()
                                  select new HotelEmail
                                  {
                                      RoleName = n.Field<string>("RoleName"),
                                      EmailAddress = n.Field<string>("Email")
                                  }).ToList();

                return HotelEmail;
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
        /// Created by:     Jefferson Bermundo
        /// Date Created:   16/08/2012
        /// Description:    Returns the logging user branchId.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static int GetUserBranchId(string userName, string roleName)
        {
            int iReturn = 0;
            IDataReader dr = null;
            try
            {
                dr = GetUserBranchDetails(userName, roleName);
                if (dr.Read())
                {
                    int.TryParse(dr["BranchID"].ToString(), out iReturn);
                }
                return iReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        public static int GetUserVendorId(string userName, string roleName)
        {
            int iReturn = 0;
            IDataReader dr = null;
            try
            {
                dr = GetUserBranchDetails(userName, roleName);
                if (dr.Read())
                {
                    int.TryParse(dr["VendorID"].ToString(), out iReturn);
                }
                return iReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        public static String GetUserBranchName(string userName, string roleName)
        {
            string iReturn = "";
            IDataReader dr = null;
            try
            {
                dr = GetUserBranchDetails(userName, roleName);
                if (dr.Read())
                {
                    iReturn = dr["BranchName"].ToString();
                }
                return iReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   31/08/2012
        /// Created By:     Josephine Gad
        /// (description)   Update User SessionID
        /// </summary>  
        public static void UpdateUserSessionID(string userName, string SessionID)
        {
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateUserSessionID");
                db.AddInParameter(dbCommand, "@pUser", DbType.String, userName);
                db.AddInParameter(dbCommand, "@pSessionID", DbType.String, SessionID);
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
                connection.Close();
                connection.Dispose();

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
        /// Date Created:   30/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Update User SessionID from LDAP
        /// </summary>  
        public static void UpdateUserSessionID_LDAP(string userName, string SessionID)
        {

            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") //;

            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateUserSessionID_LDAP");
                db.AddInParameter(dbCommand, "@pUser", DbType.String, userName);
                db.AddInParameter(dbCommand, "@pSessionID", DbType.String, SessionID);
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
                connection.Close();
                connection.Dispose();

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
        /// Date Created:   31/08/2012
        /// Created By:     Josephine Gad
        /// (description)   Get User SessionID
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static String GetUserSessionID(string userName)
        {
            IDataReader dr = null;
            DbCommand command = null;
            string sReturn = "";
            try
            {
                Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
                command = db.GetStoredProcCommand("uspGetUserSessionID");
                db.AddInParameter(command, "@pUser", DbType.String, userName);
                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    sReturn = GlobalCode.Field2String(dr["colSessionIDVarchar"]);
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                    dr.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
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
            IDataReader dr = null;
            DbCommand command = null;
            string sReturn = "";
            try
            {
                Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
                command = db.GetStoredProcCommand("uspGetUserSessionID_LDAP");
                db.AddInParameter(command, "@pUser", DbType.String, userName);
                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    sReturn = GlobalCode.Field2String(dr["colLDAPSessionID"]);
                }
                return sReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                    dr.Close();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
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
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                List<VesselDTO> list = new List<VesselDTO>();

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetVesselByUser");
                db.AddInParameter(command, "@pUserName", DbType.String, sUser);
                db.AddInParameter(command, "@pVesselCode", DbType.String, sVesselCode);
                db.AddInParameter(command, "@pVesselName", DbType.String, sVesselName);
                db.AddInParameter(command, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new VesselDTO()
                        {
                            VesselIDString = GlobalCode.Field2String(a["VesselID"]),
                            VesselNameString = GlobalCode.Field2String(a["VesselName"])
                        }
                        ).ToList();
                return list;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                command = db.GetStoredProcCommand("uspSaveUserVessel");

                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                command.Parameters.Add(param);

                db.AddInParameter(command, "@pCreatedBy", DbType.String, sCreatedBy);
                db.AddInParameter(command, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(command, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(command, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(command, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(command, "@pDateCreatedGMT", DbType.DateTime, dtGMT);

                //command.Parameters.Add(dt);
                //command.Parameters.Add(sCreatedBy);


                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                List<AirportDTO> list = new List<AirportDTO>();

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetAirportByUser");
                db.AddInParameter(command, "@pUserName", DbType.String, sUser);
                db.AddInParameter(command, "@pAirportCode", DbType.String, sAirportCode);
                db.AddInParameter(command, "@pAirportName", DbType.String, sAirportName);
                db.AddInParameter(command, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new AirportDTO()
                        {
                            AirportIDString = GlobalCode.Field2String(a["AirportID"]),
                            AirportNameString = GlobalCode.Field2String(a["AirportName"])
                        }
                        ).ToList();
                return list;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                command = db.GetStoredProcCommand("uspSaveUserAirport");

                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                command.Parameters.Add(param);

                db.AddInParameter(command, "@pCreatedBy", DbType.String, sCreatedBy);
                db.AddInParameter(command, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(command, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(command, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(command, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(command, "@pDateCreatedGMT", DbType.DateTime, dtGMT);

                //command.Parameters.Add(dt);
                //command.Parameters.Add(sCreatedBy);


                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                List<Seaport> list = new List<Seaport>();

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetSeaportByUser");
                db.AddInParameter(command, "@pUserName", DbType.String, sUser);
                db.AddInParameter(command, "@pSeaportCode", DbType.String, sSeaportCode);
                db.AddInParameter(command, "@pSeaportName", DbType.String, sSeaportName);
                db.AddInParameter(command, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new Seaport()
                        {
                            SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                            SeaportName = GlobalCode.Field2String(a["SeaportName"])
                        }
                        ).ToList();
                return list;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                command = db.GetStoredProcCommand("uspSaveUserSeaport");

                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                command.Parameters.Add(param);

                db.AddInParameter(command, "@pCreatedBy", DbType.String, sCreatedBy);
                db.AddInParameter(command, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(command, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(command, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(command, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(command, "@pDateCreatedGMT", DbType.DateTime, dtGMT);

                //command.Parameters.Add(dt);
                //command.Parameters.Add(sCreatedBy);


                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   15/Jul/2013
        /// Created By:     Josephine Gad
        /// (description)   Save User Region
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sCreatedBy"></param>
        public static void SaveUserRegion(string sUserName)
        {
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                command = db.GetStoredProcCommand("uspSaveRegionSeaportByUser");

                db.AddInParameter(command, "@pUserIDVarchar", DbType.String, sUserName);
                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        


        ///===================================================
        /// <summary>            
        /// Date Created: 03/11/2015
        /// Created By: Muhallidin Wali
        /// (description) Get alternate email list of users                        
        ///===================================================
        public static string GetUserInfoAlterEmail(string uid)
        {
            SqlConnection SQLConn = new SqlConnection(ConnStr);
            SqlDataReader SFDataTable = null;
            SqlCommand command = new SqlCommand("uspGetUserInfoAlterEmail", SQLConn);
            try
            {
                string alternate= "";
                SQLConn.Open();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@pUserID", uid);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(param);
                SFDataTable =  command.ExecuteReader();
                if (SFDataTable.Read())
                {
                    alternate = SFDataTable["AlternateEmail"].ToString();
                }
                return alternate;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //SQLConn.Close();
                //SQLConn.Dispose();

                if (command != null)
                {
                    command.Dispose();
                }
                if (SFDataTable != null)
                {
                    SFDataTable.Close();
                    SFDataTable.Dispose();
                }                
            }
        }
        /// <summary>
        /// Date Created:   04/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get User list
        /// </summary>
        public static List<UserList_LDAP> GetUserList(DateTime dtFrom, DateTime dtTo, string sRole, bool bIsWithAlternateEmail,
            bool bIsSearchByName, string sUserOrEmail)
        {
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                List<UserList_LDAP> list = new List<UserList_LDAP>();

                Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
                command = db.GetStoredProcCommand("uspGetUsersAll_LDAP");
                db.AddInParameter(command, "@pDateFrom", DbType.Date, dtFrom);
                db.AddInParameter(command, "@pDateTo", DbType.Date, dtTo);
                db.AddInParameter(command, "@pRole", DbType.String, sRole);
                db.AddInParameter(command, "@pIsWithAlternateEmail", DbType.Boolean, bIsWithAlternateEmail);
                db.AddInParameter(command, "@pIsSearchByName", DbType.Boolean, bIsWithAlternateEmail);
                db.AddInParameter(command, "@pUserNameOrEmail", DbType.String, sUserOrEmail);

                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new UserList_LDAP()
                        {
                            UserName    = GlobalCode.Field2String(a["UserName"]),
                            FirstName   = GlobalCode.Field2String(a["Firstname"]),
                            MiddleName  = GlobalCode.Field2String(a["Middlename"]),
                            LastName    = GlobalCode.Field2String(a["Lastname"]),
                            Email       = a.Field<string>("Email"),
                            Status      = a.Field<string>("Status"),
                            Group       = a.Field<string>("Group"),
                            CreatedDate = a.Field<DateTime?>("CreatedDate"),
                            Role        = a.Field<string>("Role"),
                            AlternateEmail = a.Field<string>("AlternateEmail"),
                            
                        }).ToList();
                return list;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// Date Created:   04/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Send Email to User
        /// </summary>        
        public static void EmailUserPassword(string sUsername, string sPassword, string sEmail)
        {
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {

                command = db.GetStoredProcCommand("uspUserPasswordEmail");

                db.AddInParameter(command, "@pUserName", DbType.String,sUsername);
                db.AddInParameter(command, "@pPassword", DbType.String, sPassword);
                db.AddInParameter(command, "@pEmailTo", DbType.String, sEmail);

                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }              
            }
        }
        /// Date Created:   10/Nov/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Get Immigration User List to Extract
        /// </summary>
        public static DataTable GetImmigrationUsersToExtract(string sRole, bool bIsWithAlternateEmail, string sPassword, DataTable dtUser)
        {
            Database SFDatebase = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspLDAP_ImmigrationRole_Extract");
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, sRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsWithAlternateEmail", DbType.Boolean, bIsWithAlternateEmail);
                SFDatebase.AddInParameter(SFDbCommand, "@pPassword", DbType.String, sPassword);

                SqlParameter param = new SqlParameter("@pTblTempUser", dtUser);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SFDbCommand.Parameters.Add(param);


                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtUser != null)
                {
                    dtUser.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {

                command = db.GetStoredProcCommand("uspUserNewEmail");
                db.AddInParameter(command, "@pLastName", DbType.String, sLastName);
                db.AddInParameter(command, "@pFirstName", DbType.String, sFirstName);
                                
                db.AddInParameter(command, "@pUserName", DbType.String, sUsername);
                db.AddInParameter(command, "@pEmailTo", DbType.String, sEmail);
                db.AddInParameter(command, "@pURL", DbType.String, sURL);

                db.AddInParameter(command, "@bIsNew", DbType.Boolean, bIsNew);
                db.AddInParameter(command, "@bIsLDAPOn", DbType.Boolean, bIsLDAPOn);

                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        /// Date Created:   13/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Update/Insert user to LDAP table
        /// </summary>
        public static void LDAPImmigrationUpdate(string sUsername, string sEmail, string sAlternateEmail, bool bIsDelete)
        {
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnectionSecurity();  // DatabaseFactory.CreateDatabase("APPSERVICESConnectionString") // //;
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                command = db.GetStoredProcCommand("uspLDAPImmigrationUpdate");
                db.AddInParameter(command, "@pcolUsernameVarchar", DbType.String, sUsername);
                db.AddInParameter(command, "@pEmail", DbType.String, sEmail);

                db.AddInParameter(command, "@pAlternateEmail", DbType.String, sAlternateEmail);
                db.AddInParameter(command, "@pIsDelete", DbType.Boolean, bIsDelete);

                db.ExecuteNonQuery(command);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
                       
        #endregion
    }
}

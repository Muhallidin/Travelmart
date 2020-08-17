using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;
using System.Web;
using System.Data.SqlClient;

namespace TRAVELMART.DAL
{
    public class UserRightsDAL
    {
        /// <summary>
        /// Date Created:   18/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user role's key
        /// ---------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static string GetUserRoleKey(string RoleString)
        {
            IDataReader RoleDataReader = null;
            DbCommand RoleDbCommand = null;
            try
            {                
                Database RoleDatabase = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
                string RoleKeyString = "";
                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetUserRoles");
                RoleDatabase.AddInParameter(RoleDbCommand, "@RoleName", DbType.String, RoleString);
                RoleDataReader = RoleDatabase.ExecuteReader(RoleDbCommand);
                if (RoleDataReader.Read())
                {
                    RoleKeyString = RoleDataReader["RID"].ToString();
                }
                return RoleKeyString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
            }

        }

        /// <summary>
        /// Date Created:   18/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user module rights
        /// </summary>
        public static DataTable GetUserModule(string RoleKeyString)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetModuleRights");
                RoleDatabase.AddInParameter(RoleDbCommand, "@RoleIDUnique", DbType.String, RoleKeyString);
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   18/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user module menu
        /// </summary>
        public static DataTable GetMenu(string RoleKeyString, bool ViewInactiveBool)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                RoleKeyString = (RoleKeyString == "0" ? "00000000-0000-0000-0000-000000000000" : RoleKeyString);
                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetMenu");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pRoleIDUnique", DbType.String, RoleKeyString);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pViewInactive", DbType.Boolean, ViewInactiveBool);
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   27/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user module menu by user name
        /// </summary>
        public static DataTable GetMenuByUser(string UserName)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetMenuByUser");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pUserName", DbType.String, UserName);
                
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   27/Nov/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Menu of users using list
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static List<UserMenuList> GetMenuListByUser(string UserName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataSet ds = null;

            List<UserMenuList> list = new List<UserMenuList>();
            try
            {
                comm = db.GetStoredProcCommand("uspGetMenuByUser");
                db.AddInParameter(comm, "@pUserName", DbType.String, UserName);
                ds = db.ExecuteDataSet(comm);
                if (ds.Tables.Count > 0)
                {
                    list.Add(new UserMenuList()
                    {
                        UserMenu = (from a in ds.Tables[0].AsEnumerable()
                                    select new UserMenus
                                    {
                                        PageIDInt = GlobalCode.Field2Int(a["colPageIDInt"]),
                                        ModuleName = a["colModuleNameVarchar"].ToString(),
                                        PageName = a["colPageNameVarchar"].ToString(),
                                        DisplayName = a["colDisplayNameVarchar"].ToString()
                                    }).ToList(),
                        UserSubMenu = (from a in ds.Tables[1].AsEnumerable()
                                       select new UserSubMenus
                                       {
                                           ParentIDInt = GlobalCode.Field2Int(a["colParentPageIDInt"]),
                                           PageIDInt = GlobalCode.Field2Int(a["colPageIDInt"]),
                                           ModuleName = a["colModuleNameVarchar"].ToString(),
                                           PageName = a["colPageNameVarchar"].ToString(),
                                           DisplayName = a["colDisplayNameVarchar"].ToString(),
                                           Sequence = GlobalCode.Field2Int(a["colSequenceInt"]),
                                       }).ToList(),

                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   18/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user module submenu
        /// </summary>
        public static DataTable GetSubMenu(string RoleKeyString, string ParentIdInt)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetMenuSub");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pRoleIDUnique", DbType.String, RoleKeyString);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pParentPageIDInt", DbType.Int16, Int16.Parse(ParentIdInt));                
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   27/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user module submenu by user
        /// </summary>
        public static DataTable GetSubMenuByUser(string UserName, string ParentIdInt)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetMenuSubByUser");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pUserName", DbType.String, UserName);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pParentPageIDInt", DbType.Int16, Int16.Parse(ParentIdInt));
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user module submenu
        /// </summary>
        public static DataTable GetSubMenuAll(string ParentIdInt)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetMenuSubAll");                
                RoleDatabase.AddInParameter(RoleDbCommand, "@pParentPageIDInt", DbType.Int16, Int16.Parse(ParentIdInt));                
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get menu not yet added in Role
        /// </summary>
        public static DataTable GetMenuNotAdded(string RoleKeyString)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetMenuNotAdded");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pRoleIDUnique", DbType.String, RoleKeyString);
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                return RoleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Menu
        /// </summary>
        public static Int32 InsertMenu(string RoleKeyString, string PageIDString, string CreatedByString)
        {
            Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand RoleDbCommand = null;
            DbConnection connection = RoleDatabase.CreateConnection();
            connection.Open();
            DbTransaction RoleTransaction = connection.BeginTransaction();
            try
            {                
                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspAddMenu");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pRoleIDUnique", DbType.String, RoleKeyString);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pPageIDInt", DbType.Int16, Int16.Parse(PageIDString));
                RoleDatabase.AddInParameter(RoleDbCommand, "@pCreatedByVarchar", DbType.String, CreatedByString);
                //RoleDatabase.AddInParameter(RoleDbCommand, "@pMenuID", DbType.Int32, 0);
                RoleDatabase.AddOutParameter(RoleDbCommand, "@pMenuID", DbType.Int32, 8);
                RoleDatabase.ExecuteDataSet(RoleDbCommand);
                RoleTransaction.Commit();
                Int32 MenuID = Convert.ToInt32(RoleDatabase.GetParameterValue(RoleDbCommand, "@pMenuID"));
                return MenuID;
            }
            catch (Exception ex)
            {
                RoleTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   22/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Menu
        /// </summary>
        public static void DeleteMenu(string MenuID, string ModifiedByString)
        {
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspDeleteMenu");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pMenuIDInt", DbType.Int16, Int16.Parse(MenuID));                
                RoleDatabase.AddInParameter(RoleDbCommand, "@pModifiedByVarchar", DbType.String, ModifiedByString);
                RoleDatabase.ExecuteDataSet(RoleDbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Menu by Role ID and Page ID
        /// </summary>
        public static Int32 DeleteMenuByRoleId(string RoleKeyString, string PageIDString, string ModifiedBy)
        {
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspDeleteMenuByRoleId");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pRoleIDUnique", DbType.String, RoleKeyString);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pPageIDInt", DbType.Int16, Int16.Parse(PageIDString));
                RoleDatabase.AddInParameter(RoleDbCommand, "@pModifiedByVarchar", DbType.String, ModifiedBy);
                RoleDatabase.AddOutParameter(RoleDbCommand, "@pMenuID", DbType.Int32, 8);
                RoleDatabase.ExecuteDataSet(RoleDbCommand);
                Int32 MenuID = Convert.ToInt32(RoleDatabase.GetParameterValue(RoleDbCommand, "@pMenuID"));
                return MenuID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Activate Menu
        /// </summary>
        public static void ActivateMenu(string MenuID, string ModifiedByString)
        {
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspActivateMenu");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pMenuIDInt", DbType.Int16, Int16.Parse(MenuID));
                RoleDatabase.AddInParameter(RoleDbCommand, "@pModifiedByVarchar", DbType.String, ModifiedByString);
                RoleDatabase.ExecuteDataSet(RoleDbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Check if menu exists
        /// </summary>
        public static bool IsMenuExists(string RoleKeyString, string PageIDString)
        {
            DataTable RoleDataTable = null;
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspIsMenuExists");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pRoleIDUnique", DbType.String, RoleKeyString);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pPageIDInt", DbType.String, Int16.Parse(PageIDString));
                RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
                if(RoleDataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
                if(RoleDataTable != null)
                {
                    RoleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user Regions
        /// ------------------------------------
        /// Date Created:   22/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Add parameter Loginame
        /// </summary>
        //public static DataTable GetUserRegion(string UserString, string LoginName)
        //{
        //    DataTable RoleDataTable = null;
        //    DbCommand RoleDbCommand = null;
        //    try
        //    {
        //        Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

        //        RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetUserMapRef");
        //        RoleDatabase.AddInParameter(RoleDbCommand, "@pUserIDVarchar", DbType.String, UserString);
        //        RoleDatabase.AddInParameter(RoleDbCommand, "@pLoginName", DbType.String, LoginName);
        //        RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
        //        return RoleDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (RoleDbCommand != null)
        //        {
        //            RoleDbCommand.Dispose();
        //        }
        //        if (RoleDataTable != null)
        //        {
        //            RoleDataTable.Dispose();
        //        }
        //    }
        //}
        ///// <summary>
        ///// Date Created:   24/08/2011
        ///// Created By:     Josephine Gad
        ///// (description)   Get user Regions not yet added in user
        ///// -------------------------------------------------------
        ///// Date Created:   22/11/2011
        ///// Created By:     Josephine Gad
        ///// (description)   Add parameter LoginName
        ///// </summary>
        //public static DataTable GetUserRegionNotAdded(string UserString, string LoginName)
        //{
        //    DataTable RoleDataTable = null;
        //    DbCommand RoleDbCommand = null;
        //    try
        //    {
        //        Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

        //        RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspGetUserMapRefNotAdded");
        //        RoleDatabase.AddInParameter(RoleDbCommand, "@pUserIDVarchar", DbType.String, UserString);
        //        RoleDatabase.AddInParameter(RoleDbCommand, "@pLoginName", DbType.String, LoginName);
        //        RoleDataTable = RoleDatabase.ExecuteDataSet(RoleDbCommand).Tables[0];
        //        return RoleDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (RoleDbCommand != null)
        //        {
        //            RoleDbCommand.Dispose();
        //        }
        //        if (RoleDataTable != null)
        //        {
        //            RoleDataTable.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   24/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Add user region/mapReference
        /// </summary>
        public static Int32 AddUserMapRef(string UserIDString, string MapIDString, string CreatedByString)
        {
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspAddUserMapRef");
                RoleDatabase.AddInParameter(RoleDbCommand, "@pUserIDVarchar", DbType.String, UserIDString);
                RoleDatabase.AddInParameter(RoleDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(MapIDString));
                RoleDatabase.AddInParameter(RoleDbCommand, "@pCreatedByVarchar", DbType.String, CreatedByString);
                RoleDatabase.AddOutParameter(RoleDbCommand, "@pUserRegionID", DbType.Int32, 8);
                RoleDatabase.ExecuteDataSet(RoleDbCommand);
                Int32 UserRegionID = Convert.ToInt32(RoleDatabase.GetParameterValue(RoleDbCommand, "@pUserRegionID"));
                return UserRegionID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   24/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete user region/mapReference
        /// </summary>
        public static void DeleteUserMapRef(string UserMapIDInt, string ModifiedByString)
        {
            DbCommand RoleDbCommand = null;
            try
            {
                Database RoleDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                RoleDbCommand = RoleDatabase.GetStoredProcCommand("uspDeleteUserMapRef");
                //RoleDatabase.AddInParameter(RoleDbCommand, "@pUserIDVarchar", DbType.String, UserIDString);
                //RoleDatabase.AddInParameter(RoleDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(MapIDString));
                RoleDatabase.AddInParameter(RoleDbCommand, "@pUserMapIDInt", DbType.Int16, Int16.Parse(UserMapIDInt));
                RoleDatabase.AddInParameter(RoleDbCommand, "@pModifiedByVarchar", DbType.String, ModifiedByString);
                RoleDatabase.ExecuteDataSet(RoleDbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoleDbCommand != null)
                {
                    RoleDbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Get user's region and regions to be added
        /// </summary>
        public static void GetUserRegion(string UserString, string LoginName)
        {
            DbCommand comm = null;
            DataSet ds = null;
            try
            {
                HttpContext.Current.Session.Remove("UserRegionList");
                HttpContext.Current.Session.Remove("UserRegionToAdd");

                List<UserRegionList> RegionList = new List<UserRegionList>();
                List<UserRegionToAdd> RegionListToAdd = new List<UserRegionToAdd>(); 
 

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                comm = db.GetStoredProcCommand("uspGetUserRegion");
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, UserString);
                db.AddInParameter(comm, "@pLoginName", DbType.String, LoginName);
                ds = db.ExecuteDataSet(comm);

                RegionList = (from a in ds.Tables[0].AsEnumerable()
                              select new UserRegionList {
                                  UserRegionID = GlobalCode.Field2Int(a["colUserRegionIDInt"]),
                                  RegionID = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                  RegionName = GlobalCode.Field2String(a["colRegionNameVarchar"]),
                                  IsExist = GlobalCode.Field2Bool(a["IsExist"])
                              }).ToList();

                RegionListToAdd = (from a in ds.Tables[1].AsEnumerable()
                              select new UserRegionToAdd
                              {
                                  RegionID = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                  RegionName = GlobalCode.Field2String(a["colRegionNameVarchar"])
                              }).ToList();

                HttpContext.Current.Session["UserRegionList"] =  RegionList;
                HttpContext.Current.Session["UserRegionToAdd"] = RegionListToAdd;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Save User Region
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sCreatedBy"></param>
        public static void SaveUserRegion(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
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

                command = db.GetStoredProcCommand("uspSaveUserRegion");

                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                command.Parameters.Add(param);

                db.AddInParameter(command, "@pCreatedBy", DbType.String, sCreatedBy);
                db.AddInParameter(command, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(command, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(command, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(command, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(command, "@pDateCreatedGMT", DbType.DateTime, dtGMT);

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
    }
}

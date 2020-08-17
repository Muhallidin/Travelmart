using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class UserRightsBLL
    {
        public static string GetUserRoleKey(string RoleString)
        {
            return UserRightsDAL.GetUserRoleKey(RoleString);            
        }
        public static DataTable GetUserModule(string RoleKeyString)
        {
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetUserModule(RoleKeyString);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
                }
            }
        }
        public static DataTable GetMenu(string RoleKeyString, bool ViewInactiveBool)
        {
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetMenu(RoleKeyString, ViewInactiveBool);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
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
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetMenuByUser(UserName);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
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
        public static List<UserMenus> GetMenuListByUser(string UserName)
        {
            HttpContext.Current.Session["UserMenuList"] = null;

            List<UserMenus> menu = new List<UserMenus>();
            List<UserMenuList> list = UserRightsDAL.GetMenuListByUser(UserName);
            menu = list[0].UserMenu;

            HttpContext.Current.Session["UserMenuList"] = menu;
            HttpContext.Current.Session["UserSubMenuList"] = list[0].UserSubMenu;

            return menu;
        }
        public static DataTable GetSubMenu(string RoleKeyString, string ParentIdInt)
        {
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetSubMenu(RoleKeyString, ParentIdInt);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
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
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetSubMenuByUser(UserName, ParentIdInt);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
                }
            }
        }
        public static DataTable GetSubMenuAll(string ParentIdInt)
        {
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetSubMenuAll(ParentIdInt);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
                }
            }
        }
        public static DataTable GetMenuNotAdded(string RoleKeyString)
        {
            DataTable ModuleDataTable = null;
            try
            {
                ModuleDataTable = UserRightsDAL.GetMenuNotAdded(RoleKeyString);
                return ModuleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ModuleDataTable != null)
                {
                    ModuleDataTable.Dispose();
                }
            }
        }
        public static Int32 InsertMenu(string RoleKeyString, string PageIDString, string CreatedByString)
        {
            Int32 MenuID = 0;
            MenuID = UserRightsDAL.InsertMenu(RoleKeyString, PageIDString, CreatedByString);
            return MenuID;
        }
        public static void DeleteMenu(string MenuID, string ModifiedByString)
        {
            UserRightsDAL.DeleteMenu(MenuID, ModifiedByString);
        }
        public static Int32 DeleteMenuByRoleId(string RoleKeyString, string PageIDString, string ModifiedBy)
        {
            Int32 MenuID = 0;
            MenuID = UserRightsDAL.DeleteMenuByRoleId(RoleKeyString, PageIDString, ModifiedBy);
            return MenuID;
        }
        public static void ActivateMenu(string MenuID, string ModifiedByString)
        {
            UserRightsDAL.ActivateMenu(MenuID, ModifiedByString);
        }
        public static bool IsMenuExists(string RoleKeyString, string PageIDString)
        {
             return  UserRightsDAL.IsMenuExists(RoleKeyString, PageIDString);
        }
        //public static DataTable GetUserRegion(string UserString, string LoginName)
        //{
        //    DataTable UserDataTable = null;
        //    try
        //    {
        //        UserDataTable = UserRightsDAL.GetUserRegion(UserString, LoginName);
        //        return UserDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (UserDataTable != null)
        //        {
        //            UserDataTable.Dispose();
        //        }
        //    }
        //}
        //public static DataTable GetUserRegionNotAdded(string UserString, string LoginName)
        //{
        //    DataTable UserDataTable = null;
        //    try
        //    {
        //        UserDataTable = UserRightsDAL.GetUserRegionNotAdded(UserString, LoginName);
        //        return UserDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (UserDataTable != null)
        //        {
        //            UserDataTable.Dispose();
        //        }
        //    }
        //}
        public static Int32 AddUserMapRef(string UserIDString, string MapIDString, string CreatedByString)
        {
            Int32 UserRegionID = 0;
            UserRegionID = UserRightsDAL.AddUserMapRef(UserIDString, MapIDString, CreatedByString);
            return UserRegionID;
        }
        public static void DeleteUserMapRef(string UserMapIDInt, string ModifiedByString)
        {
            UserRightsDAL.DeleteUserMapRef(UserMapIDInt, ModifiedByString);
        }
        /// <summary>
        /// Date Created:   11/Jan/2013
        /// Created By:     Josephine Gad
        /// (description)   Get user's region and regions to be added
        /// </summary>
        public static void GetUserRegion(string UserString, string LoginName)
        {
            UserRightsDAL.GetUserRegion(UserString, LoginName);
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
            UserRightsDAL.SaveUserRegion(dt, sCreatedBy, strLogDescription, strFunction, strPageName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class EPortalDAL
    {    
        /// <summary>
        /// Date Created:   11/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get User List by Session ID
        /// --------------------------------------- 
        /// </summary>       
        public static List<UserList> GetUserBySessionID (string sSessionID)
        {
            Database db = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
            DbCommand comm = null;
            DataSet ds = null;
            DataTable dt = null;

            List<UserList> list = new List<UserList>();
            try
            {
                comm = db.GetStoredProcCommand("uspGetUserBySessionID_EPortal");
                db.AddInParameter(comm, "@pSessionID", DbType.String, sSessionID);

                ds = db.ExecuteDataSet(comm);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    list = (from a in dt.AsEnumerable()
                            select new UserList
                            {
                               // sUserID = a.Field<Guid>("UserId"),
                                sUserName = a.Field<string>("colUsernameVarchar"),
                                sUserEmail = a.Field<string>("Email"),

                                sLastName = a.Field<string>("colLNameVarchar"),
                                sFirstName = a.Field<string>("colFNameVarchar"),
//                                dDateCreated = a.Field<DateTime>("CreateDate"),

                            }).ToList();

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }        
        }
        /// <summary>
        /// Date Created:   11/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get User List with token
        /// --------------------------------------- 
        /// </summary>       
        public static List<UserList> GetUserList()
        {
            Database db = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
            DbCommand comm = null;
            DataSet ds = null;
            DataTable dt = null;

            List<UserList> list = new List<UserList>();
            try
            {
                comm = db.GetStoredProcCommand("uspGetUsersAll");
                

                ds = db.ExecuteDataSet(comm);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[1];

                    list = (from a in dt.AsEnumerable()
                            select new UserList {
                                sUserID = a.Field<Guid>("UserId"),
                                sEnc =  CommonFunctions.EncryptString(GlobalCode.Field2String(a["UserId"]), GlobalCode.Field2String(a["UserName"])),
                                sDec = CommonFunctions.DecryptString(CommonFunctions.EncryptString(GlobalCode.Field2String(a["UserId"]), GlobalCode.Field2String(a["UserName"])), GlobalCode.Field2String(a["UserName"])),
                                sUserName = a.Field<string>("UserName"),
                                sUserEmail = a.Field<string>("Email"),

                                sLastName = a.Field<string>("colLNameVarchar"),
                                sFirstName = a.Field<string>("colFNameVarchar"),
                                dDateCreated = a.Field<DateTime>("CreateDate"),

                            }).ToList();

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dt = null;
            string sReturn = "0";
            try
            {
                dbCommand = db.GetStoredProcCommand("upsPortalModuleGet");

                db.AddInParameter(dbCommand, "@pModuleCode", DbType.String, sModuleCode);                
                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];
                if (dt != null)
                {
                    sReturn = dt.Rows[0]["colPortalModuleIDInt"].ToString();                    
                }
                return sReturn;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

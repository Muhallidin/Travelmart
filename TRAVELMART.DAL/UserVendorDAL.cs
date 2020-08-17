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
    public class UserVendorDAL
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
            DbCommand command = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                command = db.GetStoredProcCommand("uspSaveUserPortAgent");

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get portagent list to be added and existing portagent in user's account
        /// </summary>
        public static List<PortAgentDTO> GetUserPortAgent(string sUser, string sPortAgentName, bool bIsToBeAdded)
        {
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                List<PortAgentDTO> list = new List<PortAgentDTO>();

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetPortAgentByUser");
                db.AddInParameter(command, "@pUserName", DbType.String, sUser);
                db.AddInParameter(command, "@pPortAgent", DbType.String, sPortAgentName);
                db.AddInParameter(command, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new PortAgentDTO()
                        {
                            PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                            PortAgentName = GlobalCode.Field2String(a["PortAgentName"])
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

        /// <summary>
        /// Date Created:   20/May/2014
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle vendor list to be added and existing vendor in user's account
        /// </summary>
        public static List<VehicleVendorDTO> GetUserVehicleVendor(string sUser, string sVehicleVendor, bool bIsToBeAdded)
        {
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                List<VehicleVendorDTO> list = new List<VehicleVendorDTO>();

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetVehicleVendorByUser");
                db.AddInParameter(command, "@pUserName", DbType.String, sUser);
                db.AddInParameter(command, "@pVehicleVendor", DbType.String, sVehicleVendor);
                db.AddInParameter(command, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new VehicleVendorDTO()
                        {
                            VehicleID = GlobalCode.Field2String(a["VehicleID"]),
                            VehicleName = GlobalCode.Field2String(a["VehicleName"])
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

        /// <summary>
        /// Date Created:   20/May/2014
        /// Created By:     Josephine Gad
        /// (description)   Save User Vehicle Vendor
        /// </summary>        
        public static void SaveUserVehicleVendor(DataTable dt, string sCreatedBy, string strLogDescription, String strFunction, String strPageName)
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

                command = db.GetStoredProcCommand("uspSaveUserVehicleVendor");

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }


        /// <summary>
        /// Date Created:   12/Dec/2015
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Active User vendor
        /// --------------------------------------- 
        /// </summary>       
        public int GetActiveUserVendor(string userID)
        {
            Database db = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
            DbCommand comm = null;
            DataSet ds = null;
            DataTable dt = null;

            List<UserList> list = new List<UserList>();
            try
            {
                //  comm = db.GetStoredProcCommand("uspGetUsersActiveVendor");
                // db.AddInParameter(comm, "@pUserName", DbType.String, userID);

                // dt = db.ExecuteDataSet(comm).Tables[0];

                return 1;//dt.Rows.Count;

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
        /// Date Created:   02/Oct/2017
        /// Created By:     Josephine Monteza
        /// (description)   Get vehicle vendor list to be added and existing vendor in Driver's account
        /// </summary>
        //public static List<VehicleVendorDTO> GetDriverVehicleVendor(string sUser, string sVehicleVendor, bool bIsToBeAdded)
        //{
        //    DbCommand command = null;
        //    DataTable dt = null;
        //    try
        //    {
        //        List<VehicleVendorDTO> list = new List<VehicleVendorDTO>();

        //        Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //        command = db.GetStoredProcCommand("uspGetVehicleVendorByUser");
        //        db.AddInParameter(command, "@pUserName", DbType.String, sUser);
        //        db.AddInParameter(command, "@pVehicleVendor", DbType.String, sVehicleVendor);
        //        db.AddInParameter(command, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
        //        dt = db.ExecuteDataSet(command).Tables[0];

        //        list = (from a in dt.AsEnumerable()
        //                select new VehicleVendorDTO()
        //                {
        //                    VehicleID = GlobalCode.Field2String(a["VehicleID"]),
        //                    VehicleName = GlobalCode.Field2String(a["VehicleName"])
        //                }).ToList();
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (command != null)
        //        {
        //            command.Dispose();
        //        }
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   03/Oct/2017
        /// Descrption:     Get Vendor list of Driver
        /// =============================================================     
        /// </summary>
        public static void DriverVendorGet(string sLoginUser, string sUserID, string sVendorToFind, bool bIsToBeAdded, string sVendorType,
           int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            Int32 iCountVehicleToAdd = 0;
            Int32 iCountVehicleAdded = 0;

            Int32 iCountHotelToAdd = 0;
            Int32 iCountHotelAdded = 0;

            Int32 iCountServiceProviderToAdd = 0;
            Int32 iCountServiceProviderAdded = 0;

            List<VehicleVendorDTO> listVehicleVendorToAdd = new List<VehicleVendorDTO>();
            List<VehicleVendorDTO> listVehicleVendorAdded = new List<VehicleVendorDTO>();

            List<HotelDTO> listHotelVendorToAdd = new List<HotelDTO>();
            List<HotelDTO> listHotelVendorAdded = new List<HotelDTO>();

            List<PortAgentDTO> listServiceProviderToAdd = new List<PortAgentDTO>();
            List<PortAgentDTO> listServiceProviderAdded = new List<PortAgentDTO>();


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dtVehicleVendorToAdd = null;
            DataTable dtVehicleVendorAdded = null;

            DataTable dtHotelVendorToAdd = null;
            DataTable dtHotelVendorAdded = null;

            DataTable dtServiceProviderToAdd = null;
            DataTable dtServiceProviderAdded = null;

            DataSet ds = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDriverVendorGet");
                db.AddInParameter(dbCommand, "@pLoginName", DbType.String, sLoginUser);
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pVendorToFind", DbType.String, sVendorToFind);
                db.AddInParameter(dbCommand, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                db.AddInParameter(dbCommand, "@pVendorType", DbType.String, sVendorType);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);
                ds = db.ExecuteDataSet(dbCommand);


                //Load All
                if (iLoadType == 0)
                {
                    if (ds != null)
                    {
                        dtVehicleVendorToAdd = ds.Tables[0];
                        iCountVehicleToAdd = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);

                        dtVehicleVendorAdded = ds.Tables[2];
                        iCountVehicleAdded = GlobalCode.Field2Int(ds.Tables[3].Rows[0][0]);
                    }
                }
                else
                {
                    //Load Vehicle Vendor list
                    if (sVendorType == "Vehicle")
                    {
                        if (bIsToBeAdded)
                        {
                            dtVehicleVendorToAdd = ds.Tables[0];
                            iCountVehicleToAdd = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);
                        }
                        else
                        {
                            dtVehicleVendorAdded = ds.Tables[0];
                            iCountVehicleAdded = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);
                        }
                    }
                }

                //Vehicle Vendor 
                if (dtVehicleVendorToAdd != null)
                {
                    listVehicleVendorToAdd = (from a in dtVehicleVendorToAdd.AsEnumerable()
                                              select new VehicleVendorDTO
                                              {
                                                  VehicleID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                  VehicleName = a.Field<string>("VendorName")
                                              }).ToList();


                    HttpContext.Current.Session["User_VehicleVendorCountToAdd"] = iCountVehicleToAdd;
                    HttpContext.Current.Session["User_VehicleVendorToAdd"] = listVehicleVendorToAdd;

                }
                if (dtVehicleVendorAdded != null)
                {
                    listVehicleVendorAdded = (from a in dtVehicleVendorAdded.AsEnumerable()
                                              select new VehicleVendorDTO
                                              {
                                                  VehicleID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                  VehicleName = a.Field<string>("VendorName")
                                              }).ToList();

                    HttpContext.Current.Session["User_VehicleVendorCountAdded"] = iCountVehicleAdded;
                    HttpContext.Current.Session["User_VehicleVendorAdded"] = listVehicleVendorAdded;
                }

                //Hotel Vendor
                if (dtHotelVendorToAdd != null)
                {
                    listHotelVendorToAdd = (from a in dtHotelVendorToAdd.AsEnumerable()
                                            select new HotelDTO
                                            {
                                                HotelIDString = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                HotelNameString = a.Field<string>("VendorName")
                                            }).ToList();


                    HttpContext.Current.Session["User_HotelVendorCountToAdd"] = iCountHotelToAdd;
                    HttpContext.Current.Session["User_HotelVendorToAdd"] = listHotelVendorToAdd;

                }
                if (dtHotelVendorAdded != null)
                {
                    listHotelVendorAdded = (from a in dtHotelVendorAdded.AsEnumerable()
                                            select new HotelDTO
                                            {
                                                HotelIDString = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                HotelNameString = a.Field<string>("VendorName")
                                            }).ToList();


                    HttpContext.Current.Session["User_HotelVendorCountAdded"] = iCountHotelAdded;
                    HttpContext.Current.Session["User_HotelVendorAdded"] = listHotelVendorAdded;
                }

                // Service Provider
                if (dtServiceProviderToAdd != null)
                {
                    listServiceProviderToAdd = (from a in dtServiceProviderToAdd.AsEnumerable()
                                                select new PortAgentDTO
                                                {
                                                    PortAgentID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                    PortAgentName = a.Field<string>("VendorName")
                                                }).ToList();


                    HttpContext.Current.Session["User_ServiceProviderCountToAdd"] = iCountServiceProviderToAdd;
                    HttpContext.Current.Session["User_ServiceProviderToAdd"] = listServiceProviderToAdd;
                }

                if (dtServiceProviderAdded != null)
                {
                    listServiceProviderAdded = (from a in dtServiceProviderAdded.AsEnumerable()
                                                select new PortAgentDTO
                                                {
                                                    PortAgentID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                    PortAgentName = a.Field<string>("VendorName")
                                                }).ToList();


                    HttpContext.Current.Session["User_ServiceProviderCountAdded"] = iCountServiceProviderAdded;
                    HttpContext.Current.Session["User_ServiceProviderAdded"] = listServiceProviderAdded;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtVehicleVendorToAdd != null)
                {
                    dtVehicleVendorToAdd.Dispose();
                }
                if (dtVehicleVendorAdded != null)
                {
                    dtVehicleVendorAdded.Dispose();
                }
                if (dtHotelVendorToAdd != null)
                {
                    dtHotelVendorToAdd.Dispose();
                }
                if (dtHotelVendorAdded != null)
                {
                    dtHotelVendorAdded.Dispose();
                }
                if (dtServiceProviderToAdd != null)
                {
                    dtServiceProviderToAdd.Dispose();
                }
                if (dtServiceProviderAdded != null)
                {
                    dtServiceProviderAdded.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspDriverVendorAddEdit");

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                db.AddInParameter(dbCommand, "@pVendorType", DbType.String, sVendorType);

                db.AddInParameter(dbCommand, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                //db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, CreatedBy);

                SqlParameter param = new SqlParameter("@pTblUserVendor", dtVendor);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
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
            Int32 iCountVehicleToAdd = 0;
            Int32 iCountVehicleAdded = 0;

            Int32 iCountHotelToAdd = 0;
            Int32 iCountHotelAdded = 0;

            Int32 iCountServiceProviderToAdd = 0;
            Int32 iCountServiceProviderAdded = 0;

            List<VehicleVendorDTO> listVehicleVendorToAdd = new List<VehicleVendorDTO>();
            List<VehicleVendorDTO> listVehicleVendorAdded = new List<VehicleVendorDTO>();

            List<HotelDTO> listHotelVendorToAdd = new List<HotelDTO>();
            List<HotelDTO> listHotelVendorAdded = new List<HotelDTO>();

            List<PortAgentDTO> listServiceProviderToAdd = new List<PortAgentDTO>();
            List<PortAgentDTO> listServiceProviderAdded = new List<PortAgentDTO>();


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dtVehicleVendorToAdd = null;
            DataTable dtVehicleVendorAdded = null;

            DataTable dtHotelVendorToAdd = null;
            DataTable dtHotelVendorAdded = null;

            DataTable dtServiceProviderToAdd = null;
            DataTable dtServiceProviderAdded = null;

            DataSet ds = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGreeterVendorGet");

                db.AddInParameter(dbCommand, "@pLoginName", DbType.String, sLoginUser);
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pVendorToFind", DbType.String, sVendorToFind);
                db.AddInParameter(dbCommand, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                db.AddInParameter(dbCommand, "@pVendorType", DbType.String, sVendorType);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);
                ds = db.ExecuteDataSet(dbCommand);


                //Load All
                if (iLoadType == 0)
                {
                    if (ds != null)
                    {
                        dtVehicleVendorToAdd = ds.Tables[0];
                        iCountVehicleToAdd = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);

                        dtVehicleVendorAdded = ds.Tables[2];
                        iCountVehicleAdded = GlobalCode.Field2Int(ds.Tables[3].Rows[0][0]);
                    }
                }
                else
                {
                    //Load Vehicle Vendor list
                    if (sVendorType == "Vehicle")
                    {
                        if (bIsToBeAdded)
                        {
                            dtVehicleVendorToAdd = ds.Tables[0];
                            iCountVehicleToAdd = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);
                        }
                        else
                        {
                            dtVehicleVendorAdded = ds.Tables[0];
                            iCountVehicleAdded = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);
                        }
                    }
                }

                //Vehicle Vendor 
                if (dtVehicleVendorToAdd != null)
                {
                    listVehicleVendorToAdd = (from a in dtVehicleVendorToAdd.AsEnumerable()
                                              select new VehicleVendorDTO
                                              {
                                                  VehicleID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                  VehicleName = a.Field<string>("VendorName")
                                              }).ToList();


                    HttpContext.Current.Session["User_VehicleVendorCountToAdd"] = iCountVehicleToAdd;
                    HttpContext.Current.Session["User_VehicleVendorToAdd"] = listVehicleVendorToAdd;

                }
                if (dtVehicleVendorAdded != null)
                {
                    listVehicleVendorAdded = (from a in dtVehicleVendorAdded.AsEnumerable()
                                              select new VehicleVendorDTO
                                              {
                                                  VehicleID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                  VehicleName = a.Field<string>("VendorName")
                                              }).ToList();

                    HttpContext.Current.Session["User_VehicleVendorCountAdded"] = iCountVehicleAdded;
                    HttpContext.Current.Session["User_VehicleVendorAdded"] = listVehicleVendorAdded;
                }

                //Hotel Vendor
                if (dtHotelVendorToAdd != null)
                {
                    listHotelVendorToAdd = (from a in dtHotelVendorToAdd.AsEnumerable()
                                            select new HotelDTO
                                            {
                                                HotelIDString = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                HotelNameString = a.Field<string>("VendorName")
                                            }).ToList();


                    HttpContext.Current.Session["User_HotelVendorCountToAdd"] = iCountHotelToAdd;
                    HttpContext.Current.Session["User_HotelVendorToAdd"] = listHotelVendorToAdd;

                }
                if (dtHotelVendorAdded != null)
                {
                    listHotelVendorAdded = (from a in dtHotelVendorAdded.AsEnumerable()
                                            select new HotelDTO
                                            {
                                                HotelIDString = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                HotelNameString = a.Field<string>("VendorName")
                                            }).ToList();


                    HttpContext.Current.Session["User_HotelVendorCountAdded"] = iCountHotelAdded;
                    HttpContext.Current.Session["User_HotelVendorAdded"] = listHotelVendorAdded;
                }

                // Service Provider
                if (dtServiceProviderToAdd != null)
                {
                    listServiceProviderToAdd = (from a in dtServiceProviderToAdd.AsEnumerable()
                                                select new PortAgentDTO
                                                {
                                                    PortAgentID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                    PortAgentName = a.Field<string>("VendorName")
                                                }).ToList();


                    HttpContext.Current.Session["User_ServiceProviderCountToAdd"] = iCountServiceProviderToAdd;
                    HttpContext.Current.Session["User_ServiceProviderToAdd"] = listServiceProviderToAdd;
                }

                if (dtServiceProviderAdded != null)
                {
                    listServiceProviderAdded = (from a in dtServiceProviderAdded.AsEnumerable()
                                                select new PortAgentDTO
                                                {
                                                    PortAgentID = GlobalCode.Field2Int(a["VendorID"]).ToString(),
                                                    PortAgentName = a.Field<string>("VendorName")
                                                }).ToList();


                    HttpContext.Current.Session["User_ServiceProviderCountAdded"] = iCountServiceProviderAdded;
                    HttpContext.Current.Session["User_ServiceProviderAdded"] = listServiceProviderAdded;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtVehicleVendorToAdd != null)
                {
                    dtVehicleVendorToAdd.Dispose();
                }
                if (dtVehicleVendorAdded != null)
                {
                    dtVehicleVendorAdded.Dispose();
                }
                if (dtHotelVendorToAdd != null)
                {
                    dtHotelVendorToAdd.Dispose();
                }
                if (dtHotelVendorAdded != null)
                {
                    dtHotelVendorAdded.Dispose();
                }
                if (dtServiceProviderToAdd != null)
                {
                    dtServiceProviderToAdd.Dispose();
                }
                if (dtServiceProviderAdded != null)
                {
                    dtServiceProviderAdded.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspGreeterVendorAddEdit");

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pIsToBeAdded", DbType.Boolean, bIsToBeAdded);
                db.AddInParameter(dbCommand, "@pVendorType", DbType.String, sVendorType);

                db.AddInParameter(dbCommand, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                //db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, CreatedBy);

                SqlParameter param = new SqlParameter("@pTblUserVendor", dtVendor);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DbCommand commSummary = null;

            DataTable dt = null;
            List<UserVendorList> list = new List<UserVendorList>();

            try
            {
                comm = db.GetStoredProcCommand("uspUserVendorGet");
                db.AddInParameter(comm, "@pLoginName", DbType.String, sLoginName);
                db.AddInParameter(comm, "@pUserName", DbType.String, sUsername);
                db.AddInParameter(comm, "@pRoleToCheck", DbType.String, sRoleToCheck);

                db.ExecuteNonQuery(comm);

                commSummary = db.GetStoredProcCommand("uspUserVendorGetSummary");
                db.AddInParameter(commSummary, "@pLoginName", DbType.String, sLoginName);
                db.AddInParameter(commSummary, "@pUserName", DbType.String, sUsername);
                dt = db.ExecuteDataSet(commSummary).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    list = (from a in dt.AsEnumerable()
                            select new UserVendorList
                           {
                               UserName = a.Field<string>("UserName"),
                               UserRole = a.Field<string>("UserRole"),
                               VendorID = GlobalCode.Field2Int(a["VendorID"]),
                               VendorName = a.Field<string>("VendorName"),
                               VendorType = a.Field<string>("VendorType")
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
                if (commSummary != null)
                {
                    commSummary.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

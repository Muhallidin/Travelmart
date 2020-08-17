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
    public class MaintenanceViewDAL
    {
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of hotel vendor            
        /// </summary>    
        public static DataTable GetHotelVendorList(string strHotelName, string strUser, string strMapRef)
        {                   
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;            
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorsList");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelName", DbType.String, strHotelName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }      
            }
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of hotel vendor  by user  
        /// ----------------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting parameter        
        /// </summary>    
        public static DataTable GetHotelVendorListByUser(string strHotelName, string strUser, string strMapRef, Int32 VendorID, string UserRole)//, string OrderBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorsListByUser");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelName", DbType.String, strHotelName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                //SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, OrderBy); 
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }


        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Ryan Bautista
        /// (description) Get list of hotel vendor branch            
        /// </summary>   
        public static DataTable GetHotelVendorBranchList(string strHotelName, string strUser, string strMapRef, string Region, string Country, 
                                                            string City, string Port, string Hotel)
        {         
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorBranchList");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelName", DbType.String, strHotelName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   19/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Get list of hotel vendor branch by UserID and role           
        /// ---------------------------------------------------------------
        /// Date Modified:  27/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete MapID parameter
        /// ----------------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting parameter
        /// </summary>   
        public static DataTable GetHotelVendorBranchListByUser(string strHotelName, string strUser, string Region, string Country,
                                                            string Airport, string Port, string Hotel, string UserRole,
                                                            string SortByBranch, string SortByPriority)//, string OrderBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorBranchListByUser");
                SFDbCommand.CommandTimeout = 60;
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelName", DbType.String, strHotelName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                //SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.String, Airport);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);

                SFDatebase.AddInParameter(SFDbCommand, "@pSortByBranch", DbType.Int16, Int16.Parse(SortByBranch));
                SFDatebase.AddInParameter(SFDbCommand, "@pSortByPriority", DbType.Int16, Int16.Parse(SortByPriority));
                //SFDatebase.AddInParameter(SFDbCommand, "@pSortBy", DbType.String, OrderBy);

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Marco Abejar
        /// (description) Delete selected hotel vendor            
        /// </summary>  
        public static void DeleteHotelVendor(int HotelVendorID, string User)
        {                      
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteHotelVendor");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelVendorID", DbType.Int32, HotelVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedBy", DbType.String, User);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of vehicle vendor            
        /// </summary>        
        public static DataTable GetVehicleVendorList(string strVehicleName, string strUser, string strMapRef)
        {                
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleVendorsList");
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleName", DbType.String, strVehicleName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);               
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }      
            }
        }
        ///<summary>
        ///Date Created: 04/08/2014
        ///Created By: Michael Brian C. Evangelista
        ///Description: Get list of Seaports
        ///</summary>
        public static void SeaportGetList(Int16 LoadType, int RegionID, int SeaportID, string SeaportName, string sOrderBy, string sUserID, int iStartRow, int iMaxRow)
        {
            List<SeaportActivation> SeaportList = new List<SeaportActivation>();
            List<RegionList> listRegion = new List<RegionList>();
            List<SeaportDTO> listSeaport = new List<SeaportDTO>();
            int iCount = 0;

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dt = null;

            DataTable dtBrand = null;
            DataTable dtRegion = null;
            DataTable dtSeaport = null;

            HttpContext.Current.Session["VehicleVendorCount"] = 0;
            HttpContext.Current.Session["VehicleVendorList"] = SeaportList;

            HttpContext.Current.Session["VehicleVendor_Region"] = listRegion;
            HttpContext.Current.Session["VehicleVendor_Seaport"] = listSeaport;

            try {

                dbCom = db.GetStoredProcCommand("uspGetSeaportActivation");
                db.AddInParameter(dbCom, "@pRegionID", DbType.Int32, RegionID);
                db.AddInParameter(dbCom, "@pSeaportID", DbType.Int32, SeaportID);

                db.AddInParameter(dbCom, "@pSeaportName", DbType.String, SeaportName);
                db.AddInParameter(dbCom, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(dbCom, "@pUserIDVarchar", DbType.String, sUserID);

                db.AddInParameter(dbCom, "@pStartRow", DbType.String, iStartRow);
                db.AddInParameter(dbCom, "@pMaxRow", DbType.String, "20");

                dSet = db.ExecuteDataSet(dbCom);
                iCount = GlobalCode.Field2Int(dSet.Tables[0].Rows[0][0]);
                dt = dSet.Tables[1];

                dtBrand = dSet.Tables[2];
                dtRegion = dSet.Tables[3];
                dtSeaport = dSet.Tables[3];

                SeaportList = (from a in dt.AsEnumerable()
                               select new SeaportActivation
                               { 
                       SeaportID = GlobalCode.Field2Int(a["colPortIDInt"]),
                       SeaportName = a.Field<string>("colPortNameVarchar"),
                       SeaportCode = a.Field<string>("colPortCodeVarchar"),
                       SeaportActivated = GlobalCode.Field2Bool(a["ActiveSeaPort"])                  
                               }
                                   ).ToList();
                listRegion = (from a in dtBrand.AsEnumerable()
                              select new RegionList
                              {
                                  RegionId = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                  RegionName = GlobalCode.Field2String(a["colRegionNameVarchar"]),
                              }).ToList();


                listSeaport = (from a in dtRegion.AsEnumerable()
                               select new SeaportDTO
                               {
                                   SeaportIDString = GlobalCode.Field2Int(a["colPortIdInt"]).ToString(),
                                   SeaportNameString = GlobalCode.Field2String(a["Seaport"]),
                               }).ToList();
                HttpContext.Current.Session["SeaportRegion"] = listRegion;
                HttpContext.Current.Session["SeaportListAll"] = listSeaport;
                HttpContext.Current.Session["SeaportActiveList"] = SeaportList;
                HttpContext.Current.Session["SeaportActiveCount"] = iCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtRegion != null)
                {
                    dtRegion.Dispose();
                }
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                }
            }
        }


        /// <summary>
        /// Date Created:   02/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get list of Vehicle Vendor
        /// ---------------------------------------------------------------     
        /// Date Modified:  28/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Add IsWithContract and colContractIdInt
        /// ---------------------------------------------
        /// Date Modified:  11/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Add IsEditContractAddVisible and IsContractListVisible
        /// </summary>
        public static void VehicleVendorsGet(Int16 iLoadType, int iRegionID, int iSeaportID, Int16 iBrandID, string sVehicleVendorName, 
            string sOrderyBy, string sUserID,  int iStartRow, int iMaxRow)
        {
            List<VendorVehicleList> VehicleList = new List<VendorVehicleList>();
            int iCount = 0;
            
            List<BrandList> listBrand = new List<BrandList>();
            List<RegionList> listRegion = new List<RegionList>();
            List<SeaportDTO> listSeaport = new List<SeaportDTO>();


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dt = null;

            DataTable dtBrand = null;
            DataTable dtRegion = null;
            DataTable dtSeaport = null;


            HttpContext.Current.Session["VehicleVendorCount"] = 0;
            HttpContext.Current.Session["VehicleVendorList"] = VehicleList;

            HttpContext.Current.Session["VehicleVendor_Brand"] = listBrand;
            HttpContext.Current.Session["VehicleVendor_Region"] = listRegion;
            HttpContext.Current.Session["VehicleVendor_Seaport"] = listSeaport;

            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsGet");
                db.AddInParameter(dbCom, "@pRegionID", DbType.Int32, iRegionID);
                db.AddInParameter(dbCom, "@pSeaportID", DbType.Int32, iSeaportID);
                db.AddInParameter(dbCom, "@pBrandID", DbType.Int16, iBrandID);
                    
                db.AddInParameter(dbCom, "@pVehicleName", DbType.String, sVehicleVendorName);
                db.AddInParameter(dbCom, "@pOrderBy", DbType.String, sOrderyBy);
                db.AddInParameter(dbCom, "@pUserIDVarchar", DbType.String, sUserID);

                db.AddInParameter(dbCom, "@pStartRow", DbType.String, iStartRow);
                db.AddInParameter(dbCom, "@pMaxRow", DbType.String, iMaxRow);

                dSet = db.ExecuteDataSet(dbCom);
                iCount = GlobalCode.Field2Int(dSet.Tables[0].Rows[0][0]);
                dt = dSet.Tables[1];

                dtBrand = dSet.Tables[2];
                dtRegion = dSet.Tables[3];
                dtSeaport = dSet.Tables[4];

                VehicleList = (from a in dt.AsEnumerable()
                               select new VendorVehicleList 
                         {
                            VehicleID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                            VendorName = a.Field<string>("colVehicleVendorNameVarchar"),
                            Country = a.Field<string>("colCountryNameVarchar"),
                            City = a.Field<string>("colCityNameVarchar"),
                            ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                            FaxNo = a.Field<string>("colFaxNoVarchar"),
                            EmailTo = a.Field<string>("colEmailToVarchar"),
                            Website = a.Field<string>("colWebsiteVarchar"),

                            IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"]),
                            colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),

                            CssEditContractAddVisible = a.Field<string>("CssEditContractAddVisible"),
                            CssContractListVisible = a.Field<string>("CssContractListVisible"),

                            CssEditContractVisible = a.Field<string>("CssEditContractVisible"),
                            CssAddContractVisible = a.Field<string>("CssAddContractVisible"),

                         }).ToList();

                listBrand = (from a in dtBrand.AsEnumerable()
                             select new BrandList
                             {
                                 BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                                 BrandName = GlobalCode.Field2String(a["BrandName"]),
                             }).ToList();

                listRegion = (from a in dtRegion.AsEnumerable()
                              select new RegionList
                              {
                                  RegionId = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                  RegionName = GlobalCode.Field2String(a["colRegionNameVarchar"]),
                              }).ToList();


                listSeaport = (from a in dtSeaport.AsEnumerable()
                               select new SeaportDTO
                               {
                                   SeaportIDString = GlobalCode.Field2Int(a["colPortIdInt"]).ToString(),
                                   SeaportNameString = GlobalCode.Field2String(a["Seaport"]),
                               }).ToList();

                HttpContext.Current.Session["VehicleVendorCount"] = iCount;
                HttpContext.Current.Session["VehicleVendorList"] = VehicleList;

                HttpContext.Current.Session["VehicleVendor_Brand"] = listBrand;
                HttpContext.Current.Session["VehicleVendor_Region"] = listRegion;
                HttpContext.Current.Session["VehicleVendor_Seaport"] = listSeaport;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtRegion != null)
                {
                    dtRegion.Dispose();
                }
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                }
            }
        }

        ///// <summary>
        ///// Date Created: 29/07/2011
        ///// Created By: Marco Abejar
        ///// (description) Get list of vehicle vendor            
        ///// </summary>        
        //public static DataTable GetVehicleVendorListByUser(string strVehicleName, string strUser, string strMapRef, string VendorID, string UserRole)//, string OrderBy)
        //{
        //    Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand SFDbCommand = null;
        //    IDataReader dataReader = null;
        //    DataTable SFDataTable = new DataTable();
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleVendorsListByUser");
        //        SFDatebase.AddInParameter(SFDbCommand, "@pVehicleName", DbType.String, strVehicleName);
        //        SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
        //        SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
        //        SFDatebase.AddInParameter(SFDbCommand, "@pVendorID", DbType.Int32, (VendorID == "") ? 0 : Convert.ToInt32(VendorID));
        //        SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
        //        //SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, OrderBy);
        //        dataReader = SFDatebase.ExecuteReader(SFDbCommand);
        //        SFDataTable.Load(dataReader);
        //        return SFDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (SFDbCommand != null)
        //        {
        //            SFDbCommand.Dispose();
        //        }
        //        if (SFDataTable != null)
        //        {
        //            SFDataTable.Dispose();
        //        }
        //        if (dataReader != null)
        //        {
        //            dataReader.Close();
        //            dataReader.Dispose();
        //        }
        //    }
        //}


        /// <summary>
        /// Date Created: 08/09/2011
        /// Created By:   Gabriel Oquialda
        /// (description) Get list of vehicle vendor branch            
        /// </summary>   
        public static DataTable GetVehicleVendorBranchList(string strVehicleName, string strUser, string strMapRef, string Region,
            string Country, string City, string Port, string Hotel)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleVendorBranchList");
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleName", DbType.String, strVehicleName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   08/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get list of vehicle vendor branch            
        /// ----------------------------------------------
        /// Date Modified:  28/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete strMapRef parameter
        /// ----------------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting parameter
        /// </summary>   
        public static DataTable GetVehicleVendorBranchListByUser(string strVehicleName, string strUser, string Region,
            string Country, string City, string Port, string Hotel, string UserRole)//, string OrderBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleVendorBranchListByUser");
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleName", DbType.String, strVehicleName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);                
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                //SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, OrderBy);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Delete selected vehicle vendor               
        /// </summary>
        public static void DeleteVehicleVendor(int VehicleVendorID, string User)
        {                        
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteVehicleVendor");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorIdInt", DbType.Int32, VehicleVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolModifiedByVarchar", DbType.String, User);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Delete selected vehicle vendor               
        /// </summary>
        public static void DeleteVehicleTypeBranch(int VehicleID, string User)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteVehicleTypeBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVehicleIdBigint", DbType.Int32, VehicleID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUser", DbType.String, User);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   29/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Get list of port            
        /// ---------------------------------------------
        /// Date Modified:  07/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter sRole,  RegionID, StartRow, MaxRow and LoadType
        ///                 Change DataTable to List
        /// ---------------------------------------------
        /// Date Created:   15/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Add sorting 
        /// </summary>
        public static List<SeaportAirport> GetPortList(string strPortName, Int32 CountryID, string strUser, string sRole,
            int RegionID, int PortAgentID, int StartRow, int MaxRow, Int16 LoadType, string OrderBy)
        {            
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
           // IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            DataSet ds = null;
            List<SeaportAirport> list = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortList");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortName", DbType.String, strPortName);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryID", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoleVarchar", DbType.String, sRole);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentId", DbType.Int32, Int16.Parse(GlobalCode.Field2String(Session["UserBranchID"]) == "" ? "0" : GlobalCode.Field2String(Session["UserBranchID"])));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentId", DbType.Int32, PortAgentID);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int32, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, MaxRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, OrderBy);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                SFDataTable = ds.Tables[1];

                Int32 iAirSeaportCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0]["AirSeaportCount"].ToString());

                list = new List<SeaportAirport>();
                list = (from a in SFDataTable.AsEnumerable()
                            select new SeaportAirport
                            {
                                PortID = GlobalCode.Field2Int(a["PortID"]),
                                PortCode = a.Field<string>("PortCode"),
                                PortName =  a.Field<string>("PortName"),
                                CountryPort = a.Field<string>("CountryPort"),                               
                                AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                AirportCode = a.Field<string>("AirportCode"),
                                AirporName = a.Field<string>("AirporName"),
                                CountryAirPort = a.Field<string>("CountryAirPort")
                            }               
                       ).ToList();

                SeaportAirport.SeaportAirportCount = iAirSeaportCount;
                return list;
                //dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                //SFDataTable.Load(dataReader);              
                //return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }    
                if(ds != null) 
                {
                    ds.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Delete selected port            
        /// </summary>
        public static void DeletePort(int PortID, string User)
        {                       
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeletePort");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolPortIdInt", DbType.Int32, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolModifiedByVarchar", DbType.String, User);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of port  
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>            
        public static DataTable GetPortCompanyList(string strPortName, string strUser, string strMapRef)
        {            
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortCompanyListFiltered");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCompanyName", DbType.String, strPortName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentId", DbType.Int16, Int16.Parse(HttpContext.Current.Session["UserBranchID"] == null ? "0" : HttpContext.Current.Session["UserBranchID"].ToString()));
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Marco Abejar
        /// (description) Get list of port        
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  05/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Replace parameter to sPortAgentVendor, 
        ///                 Delete @ppMapIDInt param
        ///                 Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:  19/Feb/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter iRegionID and iPortID
        /// -------------------------------------------
        /// Date Modified:  13/Mar/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter sUserID and sRole
        /// -------------------------------------------
        /// </summary>        
        public static List<VendorPortAgentList> GetPortAgentList(string sUserID, string sRole,
            string sPortAgentVendor, string sOrder, 
            int iRegionID, int iPortID,  int iStartRow, int iMaxRow)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            DataTable SFDataTable = null;
            DataSet ds = null;
            List<VendorPortAgentList> list = new List<VendorPortAgentList>();
            HttpContext.Current.Session["VendorPortAgentCount"] = 0;

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspPortAgentVendorGet");
                sPortAgentVendor = (sPortAgentVendor == null ? "" : sPortAgentVendor);
                
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, sUserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, sRole);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentVendor", DbType.String, sPortAgentVendor);
                SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, sOrder);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionID", DbType.Int32, iRegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortID", DbType.Int32, iPortID);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int32, iStartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, iMaxRow);
                //dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                //SFDataTable.Load(dataReader);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                SFDataTable = ds.Tables[1];

                list = (from a in SFDataTable.AsEnumerable()
                        select new VendorPortAgentList
                        {
                            PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                            PortAgentName = a.Field<string>("colPortAgentVendorNameVarchar"),
                            Country = a.Field<string>("colCountryNameVarchar"),
                            City = a.Field<string>("colCityNameVarchar"),
                            ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                            FaxNo = a.Field<string>("colFaxNoVarchar"),
                            EmailTo = a.Field<string>("colEmailToVarchar"),
                            Website = a.Field<string>("colWebsiteVarchar"),
                            ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                            IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"]),
                        }).ToList();
                
                HttpContext.Current.Session["VendorPortAgentCount"] = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                return list;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>        
        /// -------------------------------------------
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider list by region, airport and brand
        /// -------------------------------------------
        /// </summary>        
        public static List<VendorPortAgentList> GetPortAgentListByAirportBrand(string sPortAgentVendor, string sOrder,
            int iRegionID, int iAirportID, int iBrandID, int iStartRow, int iMaxRow)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            DataSet ds = null;
            List<VendorPortAgentList> list = new List<VendorPortAgentList>();
            HttpContext.Current.Session["VendorPortAgentCount"] = 0;

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspPortAgentVendorGetWithBrand");
                sPortAgentVendor = (sPortAgentVendor == null ? "" : sPortAgentVendor);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentVendor", DbType.String, sPortAgentVendor);
                SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, sOrder);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionID", DbType.Int32, iRegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.Int32, iAirportID);
                SFDatebase.AddInParameter(SFDbCommand, "@pBrandID", DbType.Int32, iBrandID);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int32, iStartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, iMaxRow);
                
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                SFDataTable = ds.Tables[1];

                list = (from a in SFDataTable.AsEnumerable()
                        select new VendorPortAgentList
                        {
                            PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                            PortAgentName = a.Field<string>("colPortAgentVendorNameVarchar"),
                            Country = a.Field<string>("colCountryNameVarchar"),
                            City = a.Field<string>("colCityNameVarchar"),
                            ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                            FaxNo = a.Field<string>("colFaxNoVarchar"),
                            EmailTo = a.Field<string>("colEmailToVarchar"),
                            Website = a.Field<string>("colWebsiteVarchar"),
                            ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                            Priority = GlobalCode.Field2TinyInt(a["colPriorityTinyint"]),
                            IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"])
                        }).ToList();

                HttpContext.Current.Session["VendorPortAgentCount"] = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                return list;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        ///// <summary>
        ///// Date Created:   12/09/2011
        ///// Created By:     Josephine Gad
        ///// (description)   Get Service Provider details
        ///// ------------------------------------------
        ///// Date Modified: 28/11/2011
        ///// Modified By:   Charlene Remotigue
        ///// (description)  optimization (use datareader instead of datatable
        ///// ------------------------------------------
        ///// Date Modified: 05/Nov/2013
        ///// Modified By:   Josephine Gad
        ///// (description)  Rename GetPortAgent to GetPortAgentByID
        /////                Change IDataReader to void
        ///// </summary>        
        //public static void GetPortAgentByID(int iPortAgentID, Int16 iLoadType)
        //{
        //    List<CountryList> listCountry = new List<CountryList>();
        //    List<CityList> listCity = new List<CityList>();
        //    List<VendorPortAgentDetails> listPortAgentDetails = new List<VendorPortAgentDetails>();
        //    List<AirportDTO> listAirportPortAgent = new List<AirportDTO>();
        //    List<AirportDTO> listAirportNotInPortAgent = new List<AirportDTO>();
            

        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCom = null;
        //    DataSet dSet = null;
        //    DataTable dtPortAgent = null;
        //    DataTable dtCountry = null;
        //    DataTable dtCity = null;
        //    DataTable dtCityFilter = null;
        //    DataTable dtAirport = null;
        //    DataTable dtAirportNotExist = null;

        //    HttpContext.Current.Session["CountryList"] = listCountry;
        //    HttpContext.Current.Session["CityList"] = listCountry;
        //    HttpContext.Current.Session["PortAgentVendorDetails"] = listPortAgentDetails;
        //    HttpContext.Current.Session["PortAgentVendorCityFilter"] = "";
        //    HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
        //    HttpContext.Current.Session["PortAgentAirportNotExist"] = listAirportNotInPortAgent;

        //    try
        //    {
        //        dbCom = db.GetStoredProcCommand("uspGetPortAgentByID");
        //        db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.String, iPortAgentID);
        //        db.AddInParameter(dbCom, "@pLoadType", DbType.String, iLoadType);
        //        dSet = db.ExecuteDataSet(dbCom);

        //        if (dSet.Tables[0] != null)
        //        {
        //            dtPortAgent = dSet.Tables[0];
        //            listPortAgentDetails = (from a in dtPortAgent.AsEnumerable()
        //                                  select new VendorPortAgentDetails
        //                                  {
        //                                      PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
        //                                      PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

        //                                      CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
        //                                      CountryName = GlobalCode.Field2String(a["CountryName"]),

        //                                      CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
        //                                      CityName = GlobalCode.Field2String(a["CityName"]),

        //                                      ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
        //                                      FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
        //                                      ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
        //                                      Address = GlobalCode.Field2String(a["colAddressVarchar"]),
        //                                      EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
        //                                      EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
        //                                      Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
        //                                  }).ToList();
        //        }
        //        if (dSet.Tables[1] != null)
        //        {
        //            dtCountry = dSet.Tables[1];
        //            listCountry = (from a in dtCountry.AsEnumerable()
        //                           select new CountryList
        //                           {
        //                               CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
        //                               CountryName = GlobalCode.Field2String(a["colCountryNameVarchar"]),
        //                           }).ToList();
        //        }
        //        if (dSet.Tables[2] != null)
        //        {
        //            dtCity = dSet.Tables[2];
        //            listCity = (from a in dtCity.AsEnumerable()
        //                        select new CityList
        //                        {
        //                            CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
        //                            CityName = GlobalCode.Field2String(a["CityCodeName"]),
        //                        }).ToList();
        //        }
        //        if (dSet.Tables[3] != null)
        //        {
        //            dtCityFilter = dSet.Tables[3];
        //        }
        //        if (dSet.Tables[4] != null)
        //        {
        //            dtAirport = dSet.Tables[4];
        //            listAirportPortAgent = (from a in dtAirport.AsEnumerable()
        //                               select new AirportDTO
        //                               {
        //                                   AirportIDString = GlobalCode.Field2String(a["AirportCode"]),
        //                                   AirportNameString = GlobalCode.Field2String(a["AirportName"])
        //                               }).ToList();
        //        }
        //        if (dSet.Tables[5] != null)
        //        {
        //            dtAirportNotExist = dSet.Tables[5];
        //            listAirportNotInPortAgent = (from a in dtAirportNotExist.AsEnumerable()
        //                                     select new AirportDTO
        //                                     {
        //                                         AirportIDString = GlobalCode.Field2String(a["AirportCode"]),
        //                                         AirportNameString = GlobalCode.Field2String(a["AirportName"])
        //                                     }).ToList();
        //        }
              
        //        HttpContext.Current.Session["CountryList"] = listCountry;
        //        HttpContext.Current.Session["CityList"] = listCountry;
        //        HttpContext.Current.Session["PortAgentVendorDetails"] = listPortAgentDetails;
        //        HttpContext.Current.Session["PortAgentVendorCityFilter"] = GlobalCode.Field2String(dtCityFilter.Rows[0][0]);
        //        HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
        //        HttpContext.Current.Session["PortAgentAirportNotExist"] = listAirportNotInPortAgent;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbCom != null)
        //        {
        //            dbCom.Dispose();
        //        }
        //        if (dSet != null)
        //        {
        //            dSet.Dispose();
        //        }
        //        if (dtPortAgent != null)
        //        {
        //            dtPortAgent.Dispose();
        //        }
        //        if (dtCountry != null)
        //        {
        //            dtCountry.Dispose();
        //        }
        //        if (dtCity != null)
        //        {
        //            dtCity.Dispose();
        //        }
        //        if (dtCityFilter != null)
        //        {
        //            dtCityFilter.Dispose();
        //        }
        //        if (dtAirport != null)
        //        {
        //            dtAirport.Dispose();
        //        }
        //        if (dtAirportNotExist != null)
        //        {
        //            dtAirportNotExist.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created: 20/10/2011
        /// Created By:   Gabriel Oquialda
        /// (description) Get hotel events list
        /// </summary>        
        public static DataTable GetHotelEventsList(Int32 branchId, Int32 cityId, DateTime OnOffDate)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable SFDataTable = new DataTable();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelEventsList");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, branchId);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityID", DbType.Int32, cityId);
                SFDatebase.AddInParameter(SFDbCommand, "@pOnOffDate", DbType.Date, OnOffDate);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                SFDataTable.Load(dataReader);
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:    15/12/2011
        /// Created By:      Josephine Gad
        /// (description)    Get Department list
        /// ------------------------------------------         
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ------------------------------------------    
        /// </summary>        
        public static List<Department> GetDepartment()
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            List<Department> list = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectDepartment");                
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new Department {
                            DeptID = GlobalCode.Field2TinyInt(a["DeptID"]),
                            DeptName = a.Field<string>("DeptName")
                        }).ToList();
                return list;
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
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   21/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get stripes
        /// ---------------------------------
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void
        /// ---------------------------------
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change void to List<Stripe>
        /// </summary>        
        public static List<Stripe> GetStripes()
        {
            DataTable dt = null;
            DbCommand command = null;
            List<Stripe> list = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectStripes");                
                dt = db.ExecuteDataSet(command).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new Stripe { 
                            StripesID = GlobalCode.Field2Int(a["StripesID"]),
                            Stripes = GlobalCode.Field2Decimal(a["Stripes"]),
                            StripeName = a.Field<string>("StripeName")
                        }).ToList();
                //Stripe.StripeList = list;
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
                if (list!= null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:  22/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get list of Ranks
        /// </summary>        
        public static DataTable GetRanks(string stripe, string RankName)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetRankByStripe");
                if (stripe != "")
                {
                    db.AddInParameter(command, "@pStripesDecimal", DbType.Double, double.Parse(stripe));
                }
                db.AddInParameter(command, "@pRankNameVarchar", DbType.String, RankName);
                dt = db.ExecuteDataSet(command).Tables[0];
                return dt;
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
        /// Date Created:  26/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get list of Stripes and Room Type
        /// </summary>        
        public static DataTable GetStripesRoomType()
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectStripesRooms");              
                dt = db.ExecuteDataSet(command).Tables[0];
                return dt;
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
        /// Date Created:  26/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Insert stripe and room type
        /// </summary>
        /// <param name="StripeRoomIDInt"></param>
        /// <param name="StripesFloat"></param>
        /// <param name="RoomIDInt"></param>
        /// <param name="DateEffective"></param>
        /// <param name="ContractLength"></param>
        /// <param name="CreatedByVarchar"></param>
        public static DataTable SaveStripeRoomType(string StripeRoomIDInt, string StripesFloat, string RoomIDInt,
            DateTime DateEffective, string ContractLength, string CreatedByVarchar)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            DataTable dtStripeRoom = new DataTable();

            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspInsertStripesRooms");
                db.AddInParameter(command, "@pStripeRoomIDInt", DbType.Int64, Int64.Parse(StripeRoomIDInt));
                db.AddInParameter(command, "@pStripesDecimal", DbType.Double, double.Parse(StripesFloat));
                db.AddInParameter(command, "@pRoomIDInt", DbType.Int16, Int16.Parse(RoomIDInt));
                db.AddInParameter(command, "@pDateEffective", DbType.DateTime, DateEffective);

                ContractLength = (ContractLength.Trim() == "" ? "0" : ContractLength);
                db.AddInParameter(command, "@pContractLength", DbType.Int16, Int16.Parse(ContractLength));
                
                db.AddInParameter(command, "@pCreatedByVarchar", DbType.String, CreatedByVarchar);
                db.AddOutParameter(command, "@StripeRoomID", DbType.Int32, 8);
                db.AddOutParameter(command, "@ReturnType", DbType.Int32, 8);

                db.ExecuteNonQuery(command, trans);
                trans.Commit();

                Int32 StripeRoomID = Convert.ToInt32(db.GetParameterValue(command, "@StripeRoomID"));
                Int32 ReturnType = Convert.ToInt32(db.GetParameterValue(command, "@ReturnType"));

                dtStripeRoom.Columns.Add("dtStripeRoomID");
                dtStripeRoom.Columns.Add("dtReturnType");

                dtStripeRoom.Rows.Add(StripeRoomID, ReturnType);
                return dtStripeRoom;
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
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:  26/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Delete stripe and room type
        /// </summary>
        /// <param name="StripeRoomIDInt"></param>
        /// <param name="DeletedByVarchar"></param>
        public static void DeleteStripeRoomType(string StripeRoomIDInt, string DeletedByVarchar)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspDeleteStripesRooms");
                db.AddInParameter(command, "@pStripeRoomIDInt", DbType.Int64, Int64.Parse(StripeRoomIDInt));
                db.AddInParameter(command, "@pDeletedByVarchar", DbType.String, DeletedByVarchar);

                db.ExecuteNonQuery(command, trans);
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
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
       /// <summary>
       /// Date Created:  28/12/2011
       /// Created By:    Josephine Gad
       /// (description)  Update Hotel branch priority no.
       /// -------------------------------------------------
       /// Date Created:  07/Feb/2014
       /// Created By:    Josephine Gad
       /// (description)  Add Room Type
       /// </summary>
       /// <param name="BranchId"></param>
       /// <param name="sPriority"></param>
       /// <param name="sUser"></param>
        public static Int32 SaveHotelPriority(string AirportId, string BranchId, string sPriority, string sUser, string sRoomType)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspUpdateHotelPriority");
                db.AddInParameter(command, "@pAirportIDInt", DbType.Int32, Int32.Parse(AirportId));
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int32, Int32.Parse(BranchId));
                if (sPriority.Trim() != "" && sPriority.Trim() != "0")
                {
                    db.AddInParameter(command, "@pPriorityTinyInt", DbType.Int16, Int16.Parse(sPriority));
                }
                db.AddInParameter(command, "@pModifiedByVarchar", DbType.String, sUser);
                db.AddInParameter(command, "@pRoomType", DbType.String, sRoomType);
                db.AddOutParameter(command, "@pAirportHotelID", DbType.Int32, 8);

                db.ExecuteNonQuery(command, trans);
                trans.Commit();

                Int32 AirportHotelID = Convert.ToInt32(db.GetParameterValue(command, "@pAirportHotelID"));
                return AirportHotelID;
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
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   24/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of hotel vendor branch by UserID and role with count
        /// --------------------------------------------------------------- 
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add orderby parameter       
        /// --------------------------------------------------------------- 
        /// Date Modified:  06/Feb/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add Row Count and store in Session
        /// </summary>   
        public static DataTable GetHotelVendorBranchListByUserWithCount(string strHotelName, 
                string strUser, Int32 Region, Int32 Country,
                Int32 Airport, Int32 Port, Int32 Hotel, string UserRole,
                int LoadType, Int32 StartRow, Int32 MaxRow, string SortBy, string OrderBy, Int16 iRoomType)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;

            DataSet ds = null;
            DataTable SFDataTable = new DataTable();
            DataTable dtCount = null;

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorBranchListByUserWithCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelName", DbType.String, strHotelName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.Int32, Airport);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.Int32, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int32, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, MaxRow);
                
                SFDatebase.AddInParameter(SFDbCommand, "@pSortBy", DbType.String, SortBy);
                SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, OrderBy);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomType", DbType.Int16, iRoomType); 

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                //SFDataTable.Load(dataReader);

                SFDataTable = ds.Tables[0];
                dtCount = ds.Tables[1];

                int iCount = GlobalCode.Field2Int(dtCount.Rows[0][0]);
                HttpContext.Current.Session["HotelAirportView_TotalCount"] = iCount;

                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }                
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtCount != null)
                {
                    dtCount.Dispose();
                }
            }
        }  
        /// <summary>
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of Aiport
        /// ---------------------------------------------------------------        
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sRole"></param>
        /// <param name="PortID"></param>
        /// <param name="RegionID"></param>
        /// <param name="IsViewExist"></param>
        /// <returns></returns>
        public static List<Airport> GetAirportList(string sUser, string sRole, Int32 PortID, Int32 RegionID, bool IsViewExist)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand command = null;
            DataTable dt = null;
            List<Airport> list = null;
            try
            {
                command = db.GetStoredProcCommand("uspGetAirPortList");
                db.AddInParameter(command, "@pUserIDVarchar", DbType.String, sUser);
                db.AddInParameter(command, "@pRoleVarchar", DbType.String, sRole);
                db.AddInParameter(command, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(command, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(command, "@pIsViewExist", DbType.Boolean, IsViewExist);
                dt = db.ExecuteDataSet(command).Tables[0];
                list = new List<Airport>();
                list = (from a in dt.AsEnumerable()
                            select new Airport { 
                                AirportSeaportID = a.Field<Int32?>("colAirportSeaportIDInt"),
                                AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                AirportCode = a.Field<string>("AirportCode"),
                                AirportName = a.Field<string>("AirportName"),
                                AirportCodeName = a.Field<string>("AirportCodeName")
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
                if (list != null)
                {
                    list = null;
                }
            }        
        }
        /// <summary>            
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Remove Airport in Seaport
        /// </summary>
        public static void RemoveAirportInSeaport(string sUser, string sAirportIDInt, string sPortIDInt,
            string sLogDescription, string sFunction, string sPageName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                dbCommand = db.GetStoredProcCommand("[uspRemoveAirportInSeaport]");

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pAirportIDInt", DbType.Int32, GlobalCode.Field2Int(sAirportIDInt));
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(sPortIDInt));
                db.AddInParameter(dbCommand, "@pLogDescription", DbType.String, sLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pPageName", DbType.String, sPageName);

                db.AddInParameter(dbCommand, "@pDateGMT", DbType.DateTime, dtGMT);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);

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
            }
        }
        /// <summary>            
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Insert Airport in Seaport
        /// </summary>
        public static void InsertAirportInSeaport(string sUser, Int32 AirportIDInt, string sPortIDInt,
            string sLogDescription, string sFunction, string sPageName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                dbCommand = db.GetStoredProcCommand("[uspInsertAirportInSeaport]");

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pAirportIDInt", DbType.Int32, AirportIDInt);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(sPortIDInt));
                db.AddInParameter(dbCommand, "@pLogDescription", DbType.String, sLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pPageName", DbType.String, sPageName);

                db.AddInParameter(dbCommand, "@pDateGMT", DbType.DateTime, dtGMT);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);

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
            }
        }
        /// <summary>            
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Remove Airport in Seaport
        /// </summary>
        public static void RemoveHotelInAirport(string sUser, string sAirportID, string sBranchID,
            string sLogDescription, string sFunction, string sPageName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dtGMT = CommonFunctions.GetDateTimeGMT(DateTime.Now);

                dbCommand = db.GetStoredProcCommand("[uspRemoveHotelInAirport]");

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pAirportIDInt", DbType.Int32, GlobalCode.Field2Int(sAirportID));
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, GlobalCode.Field2Int(sBranchID));
                db.AddInParameter(dbCommand, "@pLogDescription", DbType.String, sLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pPageName", DbType.String, sPageName);

                db.AddInParameter(dbCommand, "@pDateGMT", DbType.DateTime, dtGMT);
                db.AddInParameter(dbCommand, "@pTimeZone", DbType.String, strTimeZone);

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
            }
        }
        /// <summary>
        /// Date Created:   28/May/2013
        /// Created By:     Josephine Gad
        /// (description)   Get list of Aiport By Region and Seaport
        /// ---------------------------------------------------------------               
        public static List<Airport> GetAirportListByRegionBySeaport(string sUser, string sRole, Int32 PortID, Int32 RegionID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand command = null;
            DataTable dt = null;
            List<Airport> list = null;
            try
            {
                command = db.GetStoredProcCommand("uspGetAirPortListByRegionByPort");
                db.AddInParameter(command, "@pUserIDVarchar", DbType.String, sUser);
                db.AddInParameter(command, "@pRoleVarchar", DbType.String, sRole);
                db.AddInParameter(command, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(command, "@pRegionIDInt", DbType.Int32, RegionID);                
                dt = db.ExecuteDataSet(command).Tables[0];
                list = new List<Airport>();
                list = (from a in dt.AsEnumerable()
                        select new Airport
                        {
                            AirportSeaportID = a.Field<Int32?>("colAirportSeaportIDInt"),
                            AirportID = GlobalCode.Field2Int(a["AirportID"]),
                            AirportCode = a.Field<string>("AirportCode"),
                            AirportName = a.Field<string>("AirportName"),
                            AirportCodeName = a.Field<string>("AirportCodeName")
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
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   30/Jun/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get list of Hotel By Brand
        ///                 Get all list necessary for Hotel Airport-Brand Assignment
        /// ---------------------------------------------------------------  
        /// </summary>
        public static void GetHotelVendorBranchListByBrand(int iRegionID, int iAirportID, string sAirportName,
            Int16 iRoomType, int iBrandID, string sHotelName, string sOrderBy, Int16 iLoadType, string sUserID,
            int iStartRow, int iMaxRow)
        {
            List<VendorHotelList> listHotel = new List<VendorHotelList>();
            List<BrandList> listBrand = new List<BrandList>();
            List<RegionList> listRegion = new List<RegionList>();
            List<AirportDTO> listAirport = new List<AirportDTO>();
            List<RoomType> listRoom = new List<RoomType>();

            
            int iHotelCount = 0;

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand command = null;

            DataTable dtHotel = null;
            DataTable dtBrand = null;
            DataTable dtRegion = null;
            DataTable dtAirport = null;
            DataTable dtRoom = null;


            HttpContext.Current.Session["HotelAirportBrand_Hotel"] = listHotel;
            HttpContext.Current.Session["HotelAirportBrand_HotelCount"] = iHotelCount;

            HttpContext.Current.Session["HotelAirportBrand_Brand"] = listBrand;
            HttpContext.Current.Session["HotelAirportBrand_Region"] = listRegion;
            HttpContext.Current.Session["HotelAirportBrand_Airport"] = listAirport;
            HttpContext.Current.Session["HotelAirportBrand_Room"] = listRoom;

            DataSet ds = null;
            
            try
            {
                command = db.GetStoredProcCommand("uspSelectHotelVendorBranchListByBrand");
                db.AddInParameter(command, "@pRegionID", DbType.Int32, iRegionID);
                db.AddInParameter(command, "@pAirportID", DbType.Int32, iAirportID);
                db.AddInParameter(command, "@pAirportName", DbType.String, sAirportName);
                db.AddInParameter(command, "@pRoomTypeID", DbType.Int16, iRoomType);
                db.AddInParameter(command, "@pBrandID", DbType.Int32, iBrandID);

                db.AddInParameter(command, "@pHotelName", DbType.String, sHotelName);
                db.AddInParameter(command, "@pOrderBY", DbType.String, sOrderBy);
                db.AddInParameter(command, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(command, "@pUserIDVarchar", DbType.String, sUserID);
                
                db.AddInParameter(command, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(command, "@pMaxRow", DbType.Int32, iMaxRow);

                ds = db.ExecuteDataSet(command);

                dtHotel = ds.Tables[0];
                iHotelCount = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);

                listHotel = (from a in dtHotel.AsEnumerable()
                             select new VendorHotelList {
                                 BrandAirHotelID = GlobalCode.Field2Int(a["BrandAirHotelID"]),
                                 HotelID = GlobalCode.Field2Int(a["HotelID"]),
                                 VendorID = GlobalCode.Field2Int(a["VendorID"]),
                                 HotelName = GlobalCode.Field2String(a["HotelName"]),
                                 Country = GlobalCode.Field2String(a["Country"]),
                                 City = GlobalCode.Field2String(a["City"]),
                                 IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"]),
                                 ContractStatus = GlobalCode.Field2String(a["colContractStatusVarchar"]),
                                 colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),

                                 IsContractListVisible = GlobalCode.Field2Bool(a["IsContractListVisible"]),
                                 IsContractAddEditVisible = GlobalCode.Field2Bool(a["IsContractAddEditVisible"]),
                                 IsPriorityVisible = GlobalCode.Field2Bool(a["IsPriorityVisible"]),
                                 Priority = a.Field<string>("PriorityInt"),
                                 Email = a.Field<string>("Email"),
                             }).ToList();

                if (iLoadType == 0)
                {
                    dtBrand = ds.Tables[2];
                    listBrand = (from a in dtBrand.AsEnumerable()
                                 select new BrandList
                                 {
                                     BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                                     BrandName = GlobalCode.Field2String(a["BrandName"]),                                     
                                 }).ToList();

                    dtRegion = ds.Tables[3];
                    listRegion = (from a in dtRegion.AsEnumerable()
                                  select new RegionList
                                 {
                                     RegionId = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                     RegionName = GlobalCode.Field2String(a["colRegionNameVarchar"]),
                                 }).ToList();


                    dtAirport = ds.Tables[4];
                    listAirport = (from a in dtAirport.AsEnumerable()
                                  select new AirportDTO
                                  {
                                      AirportIDString = GlobalCode.Field2Int(a["colAirportIDInt"]).ToString(),
                                      AirportNameString = GlobalCode.Field2String(a["Airport"]),
                                  }).ToList();


                    dtRoom = ds.Tables[5];
                    listRoom = (from a in dtRoom.AsEnumerable()
                                select new RoomType
                                {
                                    RoomID = GlobalCode.Field2Int(a["colRoomIDInt"]),
                                    RoomName = GlobalCode.Field2String(a["colRoomNameVarchar"]),
                                }).ToList();
                }

                HttpContext.Current.Session["HotelAirportBrand_Hotel"] = listHotel;
                HttpContext.Current.Session["HotelAirportBrand_HotelCount"] = iHotelCount;

                HttpContext.Current.Session["HotelAirportBrand_Brand"] = listBrand;
                HttpContext.Current.Session["HotelAirportBrand_Region"] = listRegion;
                HttpContext.Current.Session["HotelAirportBrand_Airport"] = listAirport;
                HttpContext.Current.Session["HotelAirportBrand_Room"] = listRoom;
               
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
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtRegion != null)
                {
                    dtRegion.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtRoom != null)
                {
                    dtRoom.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   03/Jul/2014
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Brand-Airport Matrix
        /// ----------------------------------------        
        /// </summary>
        public static void BrandAirportHotelSavePriority(
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtBrandAirportHotel
            )
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspBrandAirportHotelSavePriority");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, dtDateCreated);

                SqlParameter param = new SqlParameter("@pTblTempBrandAirportHotel", dtBrandAirportHotel);
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
                if (dtBrandAirportHotel != null)
                {
                    dtBrandAirportHotel.Dispose();
                }
            }
        }

        ///<summary>
        ///Date Created: 07/08/2014
        ///Created By: Michael Brian C. Evangelista
        /// </summary>
        public static void BrandAirportVehicleSave(int portAgentID, DataTable airport, DataTable brand,string username)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
      
            int portagentid = portAgentID;
            int airportid = 0;
            int brandid = 0;
            //save (INSERT Data)
            DateTime now = DateTime.Now;
            int airPortCount = airport.Rows.Count;
            int brandCount = brand.Rows.Count;
            try
            {

                dbCommand = db.GetStoredProcCommand("uspBrandAirportPortAgentSave");
                db.AddInParameter(dbCommand, "@pcolPortAgentInt", DbType.Int32, portagentid);
                db.AddInParameter(dbCommand, "@pcolUserName", DbType.String, username);

                SqlParameter param = new SqlParameter("@pTblTempBrand", brand);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblTempAirport", airport);
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
            finally { 
            
            }
        }

        /// <summary>            
        /// Date Created:   04/Jul/2014
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Airport-Brand Matrix with Audit Trail
        /// ----------------------------------------        
        /// </summary>
        
        public static void BrandAirportHotelSave(int iHotelID, 
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtBrand, DataTable dtAirport
            )
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspBrandAirportHotelSave");

                db.AddInParameter(dbCommand, "@pHotelID", DbType.Int32, iHotelID);
                
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, dtDateCreated);

                SqlParameter param = new SqlParameter("@pTblTempBrand", dtBrand);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblTempAirport", dtAirport);
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
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   03/Jul/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get hotel Details
        /// </summary>
        public static void GetHotelDetails(int iHotelID)
        {
            List<VendorHotelList> listHotel = new List<VendorHotelList>();            
            List<BrandList> listBrand = new List<BrandList>();
            
            List<AirportDTO> listAirportAssigned = new List<AirportDTO>();
            List<AirportDTO> listAirportNotAssigned = new List<AirportDTO>();

            HttpContext.Current.Session["HotelAirportBrandPop_Hotel"] = listHotel;
            HttpContext.Current.Session["HotelAirportBrandPop_Brand"] = listBrand;
            HttpContext.Current.Session["HotelAirportBrandPop_AirportAssigned"] = listAirportAssigned;
            HttpContext.Current.Session["HotelAirportBrandPop_AirportNotAssigned"] = listAirportNotAssigned;
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DataSet ds = null;
            DbCommand command = null;

            DataTable dtHotel = null;
            DataTable dtBrand = null;
            DataTable dtAirportAssigned = null;
            DataTable dtAirportNotAssigned = null;

            try
            {
                command = db.GetStoredProcCommand("uspSelectHotelDetails");
                db.AddInParameter(command, "@pHotelIDInt", DbType.Int32, iHotelID);                
                ds = db.ExecuteDataSet(command);

                dtHotel = ds.Tables[0];
                dtBrand = ds.Tables[1];
                dtAirportAssigned = ds.Tables[2];
                dtAirportNotAssigned = ds.Tables[3];

                listHotel = (from a in dtHotel.AsEnumerable()
                             select new VendorHotelList {
                                 HotelID = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                 HotelName = GlobalCode.Field2String(a["colVendorBranchNameVarchar"])
                             }).ToList();

                listBrand = (from a in dtBrand.AsEnumerable()
                             select new BrandList
                             {
                                 BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                                 BrandName = GlobalCode.Field2String(a["BrandName"]),
                                 IsAssigned = GlobalCode.Field2Bool(a["IsAssignedToHotel"])
                             }).ToList();

                listAirportAssigned = (from a in dtAirportAssigned.AsEnumerable()
                             select new AirportDTO
                             {
                                 AirportIDString= GlobalCode.Field2String(a["colAirportIDInt"]),
                                 AirportNameString = GlobalCode.Field2String(a["AirportName"])
                             }).ToList();

                listAirportNotAssigned = (from a in dtAirportNotAssigned.AsEnumerable()
                                       select new AirportDTO
                                       {
                                           AirportIDString = GlobalCode.Field2String(a["colAirportIDInt"]),
                                           AirportNameString = GlobalCode.Field2String(a["AirportName"])
                                       }).ToList();

                HttpContext.Current.Session["HotelAirportBrandPop_Hotel"] = listHotel;
                HttpContext.Current.Session["HotelAirportBrandPop_Brand"] = listBrand;
                HttpContext.Current.Session["HotelAirportBrandPop_AirportAssigned"] = listAirportAssigned;
                HttpContext.Current.Session["HotelAirportBrandPop_AirportNotAssigned"] = listAirportNotAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();                
                }
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtAirportAssigned != null)
                {
                    dtAirportAssigned.Dispose();
                }
                if (dtAirportNotAssigned != null)
                {
                    dtAirportNotAssigned.Dispose();
                }
            }
        }




        /// <summary>            
        /// Date Created:   04/Jul/2014
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Immiration Company
        /// ----------------------------------------        
        /// </summary>

        public List<ImmigrationCompanyGenericClass>  GetImmigationCompany(string UserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds;
            List<ImmigrationCompanyGenericClass> ImmigrationCompany = new List<ImmigrationCompanyGenericClass>(); 
            try
            {
                
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspUserImmigrationCompany");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID);
                ds   = db.ExecuteDataSet(dbCommand);


                ImmigrationCompany.Add(new ImmigrationCompanyGenericClass
                    {

                        ImmigrationCompany = (from e in ds.Tables[0].AsEnumerable()
                                              select new ImmigrationCompany
                                              {

                                                  ImmigrationCompanyID = GlobalCode.Field2Int(e["colImmigrationCompanyIDint"]) ,
                                                  Address = GlobalCode.Field2String(e["colAddressVarchar"]) ,
                                                  City = GlobalCode.Field2String(e["colCityNameVarchar"])  ,
                                                  CityID =GlobalCode.Field2Int(e["colCityIDInt"]) ,
                                                  Country = GlobalCode.Field2String(e["colCountryNameVarchar"]),
                                                  CountryID = GlobalCode.Field2Int(e["colCountryIDInt"]),
                                                  Contact = GlobalCode.Field2String(e["colContactVarchar"]),
                                                  UserID = GlobalCode.Field2String(e["colUsernameVarchar"]),
                                                  EmailAdd = GlobalCode.Field2String(e["colEmailAddressVarchar"]),
                                                  Company =GlobalCode.Field2String(e["colCompanyVarchar"]) ,

                                              }).ToList(),
                        Country = (from n in ds.Tables[1].AsEnumerable()
                                   select new Country
                                   {
                                       CountryID = n.Field<int?>("colCountryIDInt"),
                                       CountryName = n.Field<string>("colCountryNameVarchar"),
                                       City = (from a in ds.Tables[2].AsEnumerable()
                                               where a.Field<int?>("colCountryIDInt") == n.Field<int?>("colCountryIDInt")
                                               select new CityList
                                               {
                                                   CityName = a.Field<string>("colCityNameVarchar"),
                                                   CityId = a.Field<int?>("colCityIDInt")
                                               }).ToList() 
                                       
                                   }).ToList(),


                    });

                 

                return ImmigrationCompany;
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
                
            }
        }








        /// <summary>            
        /// Date Created:   04/Jul/2014
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Immiration Company
        /// ----------------------------------------        
        /// </summary>

        public List<ImmigrationCompany> SaveImmigationCompany(int ImmigrationCompanyID, int CountryID , int CityID,string Company, string address, string emailAdd,string Contact,string UserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds;
            List<ImmigrationCompany> ImmigrationCompany = new List<ImmigrationCompany>();
            try
            {
 

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspSaveImmigrationCompany");
                db.AddInParameter(dbCommand, "@pImmigrationCompanyID", DbType.Int32, ImmigrationCompanyID);
                db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32 , CountryID );
                db.AddInParameter(dbCommand, "@pCityID", DbType.Int32, CityID );
                db.AddInParameter(dbCommand, "@pCompany", DbType.String, Company );

                db.AddInParameter(dbCommand, "@pAddress", DbType.String, address );
                db.AddInParameter(dbCommand, "@pEmailAddress", DbType.String, emailAdd);
                db.AddInParameter(dbCommand, "@pContact", DbType.String, Contact );
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID);

                ds = db.ExecuteDataSet(dbCommand);

                ImmigrationCompany = (from e in ds.Tables[0].AsEnumerable()
                                      select new ImmigrationCompany
                                      {

                                          ImmigrationCompanyID = GlobalCode.Field2Int(e["colImmigrationCompanyIDint"]),
                                          Address = GlobalCode.Field2String(e["colAddressVarchar"]),
                                          City = GlobalCode.Field2String(e["colCityNameVarchar"]),
                                          CityID = GlobalCode.Field2Int(e["colCityIDInt"]),
                                          Country = GlobalCode.Field2String(e["colCountryNameVarchar"]),
                                          CountryID = GlobalCode.Field2Int(e["colCountryIDInt"]),
                                          Contact = GlobalCode.Field2String(e["colContactVarchar"]),
                                          UserID = GlobalCode.Field2String(e["colUsernameVarchar"]),
                                          EmailAdd = GlobalCode.Field2String(e["colEmailAddressVarchar"]),
                                          Company = GlobalCode.Field2String(e["colCompanyVarchar"]),

                                      }).ToList();


                return ImmigrationCompany;
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

            }
        }



        /// <summary>            
        /// Date Created:   04/Jul/2014
        /// Created By:     Muhallidin G Wali
        /// (description)   delete Immiration Company
        /// ----------------------------------------        
        /// </summary>

        public List<ImmigrationCompany> DeleteImmigationCompany(int ImmigrationCompanyID, string UserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds;
            List<ImmigrationCompany> ImmigrationCompany = new List<ImmigrationCompany>();
            try
            {


                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspDeleteImmigrationCompany");
                db.AddInParameter(dbCommand, "@pImmigrationCompanyID", DbType.Int32, ImmigrationCompanyID);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID);

                ds = db.ExecuteDataSet(dbCommand);

                ImmigrationCompany = (from e in ds.Tables[0].AsEnumerable()
                                      select new ImmigrationCompany
                                      {

                                          ImmigrationCompanyID = GlobalCode.Field2Int(e["colImmigrationCompanyIDint"]),
                                          Address = GlobalCode.Field2String(e["colAddressVarchar"]),
                                          City = GlobalCode.Field2String(e["colCityNameVarchar"]),
                                          CityID = GlobalCode.Field2Int(e["colCityIDInt"]),
                                          Country = GlobalCode.Field2String(e["colCountryNameVarchar"]),
                                          CountryID = GlobalCode.Field2Int(e["colCountryIDInt"]),
                                          Contact = GlobalCode.Field2String(e["colContactVarchar"]),
                                          UserID = GlobalCode.Field2String(e["colUsernameVarchar"]),
                                          EmailAdd = GlobalCode.Field2String(e["colEmailAddressVarchar"]),
                                          Company = GlobalCode.Field2String(e["colCompanyVarchar"]),

                                      }).ToList();


                return ImmigrationCompany;
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

            }
        }

        /// <summary>            
        /// Date Created:   17/Feb/2016
        /// Created By:     Josephine Monteza
        /// (description)   Get the list of Blackout dates
        /// ----------------------------------------        
        /// </summary>
        public List<BlackoutDateList> BlackoutDateGet(Int16 iLoadType,int iBrandID, int iPortID, string dDate,
            string sOrderBy, int iStartRow, int iRowCount)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtBlackout = null;
            DataTable dtBrand = null;
            DataTable dtSeaport = null;
            List<BlackoutDateList> list = new List<BlackoutDateList>();
            List<BrandList> listBrand = new List<BrandList>();
            List<PortList> listSeaport = new List<PortList>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspBlackoutDateGet");
                db.AddInParameter(dbCommand, "@pLoadTypeInt", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pBrandIdInt", DbType.Int32, iBrandID);                
                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, iPortID);

                if (dDate.Trim() != "")
                {
                    DateTime dBlackoutdate = GlobalCode.Field2DateTime(dDate);
                    db.AddInParameter(dbCommand, "@pBlackoutDate", DbType.DateTime, dBlackoutdate);
                }
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, iRowCount);
                ds = db.ExecuteDataSet(dbCommand);

                dtBlackout = ds.Tables[1];

                

                int iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                list = (from e in dtBlackout.AsEnumerable()
                      select new BlackoutDateList
                      {

                          BlackoutDateID = GlobalCode.Field2Int(e["BlackoutDateID"]),
                          BrandId = GlobalCode.Field2Int(e["BrandId"]),
                          BrandName = GlobalCode.Field2String(e["BrandName"]),
                          PortId = GlobalCode.Field2Int(e["PortId"]),
                          PortName = GlobalCode.Field2String(e["PortName"]),
                          BlackoutDateFrom = GlobalCode.Field2DateTime(e["BlackoutDateFrom"]),
                          BlackoutDateTo = GlobalCode.Field2DateTime(e["BlackoutDateTo"]),
                      }).ToList();

                if (iLoadType == 0)
                {
                    dtBrand = ds.Tables[2];
                    dtSeaport = ds.Tables[3];

                    listBrand = (from a in dtBrand.AsEnumerable()
                                 select new BrandList
                                 {
                                     BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                                     BrandName = a.Field<string>("BrandName"),
                                 }).ToList();

                    listSeaport = (from a in dtSeaport.AsEnumerable()
                                   select new PortList
                                   {
                                       PortId = GlobalCode.Field2Int(a["colPortIdInt"]),
                                       PortName = a.Field<string>("PortName"),
                                   }).ToList();

                    HttpContext.Current.Session["BlackOutDate_Brand"] = listBrand;
                    HttpContext.Current.Session["BlackOutDate_Seaport"] = listSeaport;
                }

                HttpContext.Current.Session["BlackOutDate_RowCount"] = iCount;                
                return list;
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
                if (dtBlackout != null)
                {
                    dtBlackout.Dispose();
                }
                if (dtBrand != null)
                {
                    dtBrand.Dispose();
                }
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   19/Feb/2016
        /// Created By:     Josephine Monteza
        /// (description)   Save Blackout Dates with Audit Trail
        /// ----------------------------------------        
        /// </summary>
        public static void BlackoutDateSave(int iBlackoutDateID, int iBrandID, int iPort, 
            string sDateFrom, string sDateTo,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated
            )
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspBlackoutDateSave");

                db.AddInParameter(dbCommand, "@pBlackoutDateIDInt", DbType.Int32, iBlackoutDateID);

                db.AddInParameter(dbCommand, "@pBrandIdInt", DbType.Int32, iBrandID);
                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, iPort);

                db.AddInParameter(dbCommand, "@pBlackoutDateFrom", DbType.String, sDateFrom);
                db.AddInParameter(dbCommand, "@pBlackoutDateTo", DbType.String, sDateTo);

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pLogDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pstrFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dDateGMT);
                db.AddInParameter(dbCommand, "@pDateNow", DbType.DateTime, dtDateCreated);

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
            }
        }
    }
}

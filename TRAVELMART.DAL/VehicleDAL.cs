using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Globalization;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class VehicleDAL
    {
        /// <summary>            
        /// Date Created: 15/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle company
        /// -----------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable and DbConnection            
        /// </summary>
        public static DataTable vehicleGetCompanyByUser(string Username)
        {            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectVehicleVendorByRegion"); //uspGetListVehicleCompany
                db.AddInParameter(dbCommand, "pUserIDVarchar", DbType.String, Username);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 15/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle company
        /// -----------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable and DbConnection            
        /// </summary>
        public static DataTable vehicleGetCompany()
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleCompany");                
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting city list by vendor id
        /// </summary>
        public static DataTable CountryListByBranchID(Int32 BranchID)
        {           
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryListByBranchVehicle");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, BranchID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting city list by vendor id
        /// </summary>
        public static DataTable CountryListByVendorID(Int32 VendorID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryListByVendorVehicle");
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorID", DbType.Int32, VendorID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting city list by vendor and country id          
        /// </summary>
        public static DataTable CityListByVendorCountryID(Int32 VendorID, Int32 CountryID)
        {           
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByVendorCountry");
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryID", DbType.Int32, CountryID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor ID
        /// ----------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable CityListByVendorID(Int32 VendorID)
        {            
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByVendorID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   01/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle brand     
        /// ===================================
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter vendorID       
        /// </summary>
        public static DataTable vehicleGetBranch(Int32 vendorID, string Username)
        {           
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleBranch");                
                db.AddInParameter(dbCommand, "@pVendorID", DbType.Int32, vendorID);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, Username);                
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   26/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get all Vehicle Branch
        /// ===================================
        /// </summary>
        /// <returns></returns>
        public static DataTable vehicleGetBranchAll()
        {
            DataTable dt = null;
            DbCommand command = null;
            try 
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetListVehicleBranchAll");
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
        /// Date Created:   20/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle branch by vendor, user and city
        /// </summary>
        public static DataTable vehicleGetBranchByVendorUserCity(Int32 vendorID, string Username, Int32 CityID, string Role)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()            
            DbCommand dbCommand = null;            
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleBranch");
                db.AddInParameter(dbCommand, "@pVendorID", DbType.Int32, vendorID);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, Username);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                dt = db.ExecuteDataSet(dbCommand).Tables[0];                
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }                
            }
        }
        /// <summary>            
        /// Date Created: 01/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle brand               
        /// </summary>
        public static DataTable vehicleGetBrand(Int32 branchID)
        {          
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleBrand");
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, branchID);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }


        ///<summary>
        ///Date Created: 09/03/2014
        ///Created By: Michael Evangelista
        ///Description: Tag to Vehicle
        ///======================================
        ///Date Modified:   18/Sept/2014
        ///Modified By:     Josephine Monetza
        ///Description:     Add variable dGMTDate
        ///======================================        
        ///Date Modified:   06/Jan/2014
        ///Modified By:     Josephine Monteza
        ///Description:     Change Int32 to Int64 for colIDBigint, colTravelReqIDInt and colSeafarerIdInt
        ///======================================
        /// </summary>
        public static DataTable TagtoVehicle(Int64 colIDBigint, Int64 colTravelReqIDInt, string colRecordLocatorVarchar,
            Int64 colSeafarerIdInt, string colOnOff, int colVehicleVendorIDInt, Int32 colPortAgentVendorIDInt, string colIsCheck, string UserId,
            string sDescription, string sFunction, string sFileName)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dt = null;
            try
            {

                dbCommand = db.GetStoredProcCommand("uspTagtoVehicle");
                DateTime today = CommonFunctions.GetCurrentDateTime();

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                DateTime dGMTDate = CommonFunctions.GetDateTimeGMT(today);

                db.AddInParameter(dbCommand, "@pcolIDBigint", DbType.Int64, colIDBigint);
                db.AddInParameter(dbCommand, "@pcolTravelReqIDInt", DbType.Int64, colTravelReqIDInt);
                db.AddInParameter(dbCommand, "@pcolRecordLocatorVarchar", DbType.String, colRecordLocatorVarchar);
                db.AddInParameter(dbCommand, "@pcolSeafarerIdInt", DbType.Int32, colSeafarerIdInt);
                db.AddInParameter(dbCommand, "@pcolOnOff", DbType.String, colOnOff );

                db.AddInParameter(dbCommand, "@pcolVehicleVendorIDInt", DbType.Int32, colVehicleVendorIDInt);
                db.AddInParameter(dbCommand, "@pcolPortAgentVendorIDInt", DbType.Int32, colPortAgentVendorIDInt);
                
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, today);
                db.AddInParameter(dbCommand, "@pCheck", DbType.String, colIsCheck);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, dGMTDate);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, sFileName);

                db.ExecuteReader(dbCommand);

                dt = new DataTable();
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        
        
        }

        /// <summary>            
        /// Date Created: 26/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle make               
        /// </summary>
        public static DataTable vehicleGetMake(Int32 branchID)
        {            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleMake");
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, branchID);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 12/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle type               
        /// </summary>
        public static DataTable vehicleGetType(Int32 branchID)
        {           
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleType");
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, branchID);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type, brand, make year               
        /// </summary>
        public static DataTable vehicleGetTypeBrandMake(Int32 branchID)
        {            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleBrandMake");
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, branchID);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   08/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle type luggage van
        /// </summary>
        public static DataTable vehicleGetTypeLuggageVan(Int32 branchID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListVehicleTypeLuggageVan");
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, branchID);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get seafarer vehicle transaction
        /// -----------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable and DbConnection   
        /// --------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader vehicleGetTransaction(int vehiclePrimaryId, int seqNo)
        {                      
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSFVehicleTransaction");
                db.AddInParameter(dbCommand, "@pcolIdBigint", DbType.Int32, vehiclePrimaryId);
                db.AddInParameter(dbCommand, "@pcolSeqNoInt", DbType.Int32, seqNo);
                dr = db.ExecuteReader(dbCommand);
                return dr;
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
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer vehicle transaction (non-Sabre)
        /// ------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader vehicleGetTransactionByID(string VehicleBookingIDString)
        {                     
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSFVehicleTransactionByID");
                db.AddInParameter(dbCommand, "@pTransVehicleIDBigInt", DbType.Int32, Int32.Parse(VehicleBookingIDString));                
                dr = db.ExecuteReader(dbCommand);
                return dr;
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
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer vehicle transaction (pending)
        /// ----------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader vehicleGetPendingByID(string PendingVehicleID)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetSFVehiclePending");
                db.AddInParameter(command, "@pPendingVehicleIDBigInt", DbType.String, PendingVehicleID);
                dr = db.ExecuteReader(command);
                return dr;
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
        /// Date Created: 18/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vehicle transaction
        /// ----------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close DataTable           
        /// </summary>   
        public static void vehicleInsertTransaction(string travelLocatorId, Int32 vendorId, Int32 category, Int32 countryID, Int32 cityId, Int32 branchId,
            Int32 vehicleBrandId, Int32 vehicleMakeId, Int32 vehicleTypeId, //string vehicleYear, string vehiclePlateNo,
            DateTime PickUpDate,
            string PickUpTime, DateTime DropOffDate, string DropOffTime, string PickUpLocation, string DropOffLocation, string vehicleStatus,
            string createdby, string seafarerStatus, string vehicleRemarks, Boolean vehicleBillToCrew, int seafarerId)
        {                      
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                if (PickUpTime == "")
                {
                    PickUpTime = null;
                }
                if (DropOffTime == "")
                {
                    DropOffTime = null;
                }

                dbCommand = db.GetStoredProcCommand("uspInsertVehicleBookings");

                db.AddInParameter(dbCommand, "@pcolIdBigint", DbType.Int32, Convert.ToInt32(travelLocatorId));
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.Int32, vendorId);
                db.AddInParameter(dbCommand, "@pcolIsAccreditedBit", DbType.Int32, category);
                db.AddInParameter(dbCommand, "@pcolCountryIDInt", DbType.Int32, countryID);
                db.AddInParameter(dbCommand, "@pcolCityIDInt", DbType.Int32, cityId);
                db.AddInParameter(dbCommand, "@pcolBranchIDInt", DbType.Int32, branchId);
                db.AddInParameter(dbCommand, "@pcolVehicleBrandIDInt", DbType.Int32, vehicleBrandId);
                db.AddInParameter(dbCommand, "@pcolVehicleMakeIdInt", DbType.Int32, vehicleMakeId);
                db.AddInParameter(dbCommand, "@pcolVehicleTypeIdInt", DbType.Int32, vehicleTypeId);
                //db.AddInParameter(dbCommand, "@pcolVehicleYearVarchar", DbType.String, vehicleYear);
                //db.AddInParameter(dbCommand, "@pcolVehiclePlateNoVarchar", DbType.String, vehiclePlateNo);
                db.AddInParameter(dbCommand, "@pcolPickUpDate", DbType.DateTime, PickUpDate);
                db.AddInParameter(dbCommand, "@pcolPickUpTime", DbType.Time, PickUpTime);
                db.AddInParameter(dbCommand, "@pcolDropOffDate", DbType.DateTime, DropOffDate);
                db.AddInParameter(dbCommand, "@pcolDropOffTime", DbType.Time, DropOffTime);
                db.AddInParameter(dbCommand, "@pcolPickUpLocationLocationCodeVarchar", DbType.String, PickUpLocation);
                db.AddInParameter(dbCommand, "@pcolDropOffLocationLocationCodeVarchar", DbType.String, DropOffLocation);
                db.AddInParameter(dbCommand, "@pcolVehicleStatusVarchar", DbType.String, vehicleStatus);
                db.AddInParameter(dbCommand, "@pcolCreatedbyVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolSFStatus", DbType.String, seafarerStatus);
                db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, vehicleRemarks);
                db.AddInParameter(dbCommand, "@pcolIsBilledToCrewBit", DbType.Boolean, vehicleBillToCrew);
                db.AddInParameter(dbCommand, "@pcolSeafarerIdInt", DbType.Int32, seafarerId);
                
                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Vehicle Booking NOT from Sabre
        /// ----------------------------------------          
        /// Date Modified:   11/10/2011
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter RecordLoc and RequestID
        ///------------------------------------------------------------------------------- 
        /// </summary>    
        public static void vehicleInsertTransactionOther(string TravelReqIDString,string RecordLoc, string RequestID, string VendorIDString, string CountryIDString,
            string CityIDString, string BranchIDString, string VehicleIDString, string VehiclePlateNoString, string PickUpDate,
            string PickUpTime, string DropOffDate, string DropOffTime, string PickUpLocationLocationCode, string DropOffLocationLocationCode,
            string VehicleStatusString, string CreatedByString, string SFStatusString, string RemarksString, bool IsBilledToBool)
        {                    
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                if (PickUpTime == "")
                {
                    PickUpTime = null;
                }
                if (DropOffTime == "")
                {
                    DropOffTime = null;
                }

                dbCommand = db.GetStoredProcCommand("uspInsertVehicleBookingsOthers");

                if (TravelReqIDString.Trim() != "0" && TravelReqIDString.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, Convert.ToInt32(TravelReqIDString));
                }
                if (RecordLoc.Trim() != "0" && RecordLoc.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLoc);
                }
                if (RequestID.Trim() != "0" && RequestID.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRequestIDInt", DbType.Int32, Convert.ToInt32(RequestID));
                }
                db.AddInParameter(dbCommand, "@pVendorIdInt", DbType.Int32, Convert.ToInt32(VendorIDString));
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, Convert.ToInt32(CountryIDString));
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, Convert.ToInt32(CityIDString));
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, Convert.ToInt32(BranchIDString));
                db.AddInParameter(dbCommand, "@pVehicleIDInt", DbType.Int32, Convert.ToInt32(VehicleIDString));
                db.AddInParameter(dbCommand, "@pVehiclePlateNoVarchar", DbType.String, VehiclePlateNoString);
                db.AddInParameter(dbCommand, "@pPickUpDate", DbType.Date, DateTime.Parse(PickUpDate));
                db.AddInParameter(dbCommand, "@pPickUpTime", DbType.Time, PickUpTime);

                db.AddInParameter(dbCommand, "@pDropOffDate", DbType.Date, DateTime.Parse(DropOffDate));
                db.AddInParameter(dbCommand, "@pDropOffTime", DbType.Time, DropOffTime);

                db.AddInParameter(dbCommand, "@pPickUpLocationLocationCodeVarchar", DbType.String, PickUpLocationLocationCode);
                db.AddInParameter(dbCommand, "@pDropOffLocationLocationCodeVarchar", DbType.String, DropOffLocationLocationCode);

                db.AddInParameter(dbCommand, "@pVehicleStatusVarchar", DbType.String, VehicleStatusString);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, CreatedByString);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, SFStatusString);
                db.AddInParameter(dbCommand, "@pRemarksVarchar", DbType.String, RemarksString);
                db.AddInParameter(dbCommand, "@pIsBilledToCrewBit", DbType.Boolean, IsBilledToBool);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Vehicle Booking, pending for approval
        ///------------------------------------------------------------------------------- 
        /// </summary>    
        public static void vehicleInsertTransactionPending(string TransVehicleID, string IDBigint, string SeqNo, 
            string TravelReqIDString, string RecordLoc, string RequestID, string VendorIDString, string CountryIDString,
            string CityIDString, string BranchIDString, string VehicleIDString, string VehiclePlateNoString, string PickUpDate,
            string PickUpTime, string DropOffDate, string DropOffTime, string PickUpLocationLocationCode, string DropOffLocationLocationCode,
            string VehicleStatusString, string CreatedByString, string CreatedDate, string ModifiedByString, string ModifiedDate,
            string SFStatusString, string RemarksString, bool IsBilledToBool, string Action)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                CultureInfo enCulture = new CultureInfo("en-US");
                string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";
                
                if (PickUpTime == "")
                {
                    PickUpTime = null;
                }
                if (DropOffTime == "")
                {
                    DropOffTime = null;
                }

                dbCommand = db.GetStoredProcCommand("uspInsertVehicleBookingsPending");

                if (TransVehicleID.Trim() != "0" && TransVehicleID != "")
                {
                    db.AddInParameter(dbCommand, "@pTransVehicleIDBigInt", DbType.Int32, Convert.ToInt32(TransVehicleID));
                }
                if (IDBigint.Trim() != "0" && IDBigint.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pIdBigint", DbType.Int32, Convert.ToInt32(IDBigint));
                }
                if (SeqNo.Trim() != "0" && SeqNo.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pSeqNoInt", DbType.Int32, Convert.ToInt32(SeqNo));
                }
                if (TravelReqIDString.Trim() != "0" && TravelReqIDString.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, Convert.ToInt32(TravelReqIDString));
                }
                if (RecordLoc.Trim() != "0" && RecordLoc.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLoc);
                }
                if (RequestID.Trim() != "0" && RequestID.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRequestIDInt", DbType.Int32, Convert.ToInt32(RequestID));
                }
                db.AddInParameter(dbCommand, "@pVendorIdInt", DbType.Int32, Convert.ToInt32(VendorIDString));
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, Convert.ToInt32(CountryIDString));
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, Convert.ToInt32(CityIDString));
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, Convert.ToInt32(BranchIDString));
                db.AddInParameter(dbCommand, "@pVehicleIDInt", DbType.Int32, Convert.ToInt32(VehicleIDString));
                db.AddInParameter(dbCommand, "@pVehiclePlateNoVarchar", DbType.String, VehiclePlateNoString);
                db.AddInParameter(dbCommand, "@pPickUpDate", DbType.Date, DateTime.Parse(PickUpDate));
                db.AddInParameter(dbCommand, "@pPickUpTime", DbType.Time, PickUpTime);

                db.AddInParameter(dbCommand, "@pDropOffDate", DbType.Date, DateTime.Parse(DropOffDate));
                db.AddInParameter(dbCommand, "@pDropOffTime", DbType.Time, DropOffTime);

                db.AddInParameter(dbCommand, "@pPickUpLocationLocationCodeVarchar", DbType.String, PickUpLocationLocationCode);
                db.AddInParameter(dbCommand, "@pDropOffLocationLocationCodeVarchar", DbType.String, DropOffLocationLocationCode);

                db.AddInParameter(dbCommand, "@pVehicleStatusVarchar", DbType.String, VehicleStatusString);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, CreatedByString);
                //db.AddInParameter(dbCommand, "@pDateCreatedDatetime", DbType.DateTime, DateTime.Parse(CreatedDate));
                db.AddInParameter(dbCommand, "@pDateCreatedDatetime", DbType.DateTime, DateTime.ParseExact(CreatedDate, dtFormaString, enCulture));

                if (ModifiedByString.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pModifiedbyVarchar", DbType.String, ModifiedByString);
                    //db.AddInParameter(dbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.Parse(ModifiedDate));
                    //db.AddInParameter(dbCommand, "@pDateModifiedDatetime", DbType.DateTime, Convert.ToDateTime(ModifiedDate,CultureInfo.InvariantCulture));
                    db.AddInParameter(dbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.ParseExact(ModifiedDate, dtFormaString, enCulture));
                }

                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, SFStatusString);
                db.AddInParameter(dbCommand, "@pRemarksVarchar", DbType.String, RemarksString);
                db.AddInParameter(dbCommand, "@pIsBilledToCrewBit", DbType.Boolean, IsBilledToBool);
                db.AddInParameter(dbCommand, "@pActionVarchar", DbType.String, Action);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Update Pending Vehicle Booking
        ///------------------------------------------------------------------------------- 
        /// </summary>    
        public static void vehicleUpdateTransactionPending(string PendingID,string TransVehicleID, string IDBigint, string SeqNo,
            string TravelReqIDString, string RecordLoc, string RequestID, string VendorIDString, string CountryIDString,
            string CityIDString, string BranchIDString, string VehicleIDString, string VehiclePlateNoString, string PickUpDate,
            string PickUpTime, string DropOffDate, string DropOffTime, string PickUpLocationLocationCode, string DropOffLocationLocationCode,
            string VehicleStatusString, string CreatedByString, string ModifiedDate, string SFStatusString, string RemarksString, bool IsBilledToBool, string Action)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                CultureInfo enCulture = new CultureInfo("en-US");
                string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";

                if (PickUpTime == "")
                {
                    PickUpTime = null;
                }
                if (DropOffTime == "")
                {
                    DropOffTime = null;
                }

                dbCommand = db.GetStoredProcCommand("uspUpdateVehicleBookingsPending");

                db.AddInParameter(dbCommand, "@pPendingVehicleIDBigInt", DbType.Int32, Convert.ToInt32(PendingID));
                if (TransVehicleID.Trim() != "0" && TransVehicleID != "")
                {
                    db.AddInParameter(dbCommand, "@pTransVehicleIDBigInt", DbType.Int32, Convert.ToInt32(TransVehicleID));
                }
                if (IDBigint.Trim() != "0" && IDBigint.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pIdBigint", DbType.Int32, Convert.ToInt32(IDBigint));
                }
                if (SeqNo.Trim() != "0" && SeqNo.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pSeqNoInt", DbType.Int32, Convert.ToInt32(SeqNo));
                }
                if (TravelReqIDString.Trim() != "0" && TravelReqIDString.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int32, Convert.ToInt32(TravelReqIDString));
                }
                if (RecordLoc.Trim() != "0" && RecordLoc.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLoc);
                }
                if (RequestID.Trim() != "0" && RequestID.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRequestIDInt", DbType.Int32, Convert.ToInt32(RequestID));
                }
                db.AddInParameter(dbCommand, "@pVendorIdInt", DbType.Int32, Convert.ToInt32(VendorIDString));
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, Convert.ToInt32(CountryIDString));
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, Convert.ToInt32(CityIDString));
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, Convert.ToInt32(BranchIDString));
                db.AddInParameter(dbCommand, "@pVehicleIDInt", DbType.Int32, Convert.ToInt32(VehicleIDString));
                db.AddInParameter(dbCommand, "@pVehiclePlateNoVarchar", DbType.String, VehiclePlateNoString);
                db.AddInParameter(dbCommand, "@pPickUpDate", DbType.Date, DateTime.Parse(PickUpDate));
                db.AddInParameter(dbCommand, "@pPickUpTime", DbType.Time, PickUpTime);

                db.AddInParameter(dbCommand, "@pDropOffDate", DbType.Date, DateTime.Parse(DropOffDate));
                db.AddInParameter(dbCommand, "@pDropOffTime", DbType.Time, DropOffTime);

                db.AddInParameter(dbCommand, "@pPickUpLocationLocationCodeVarchar", DbType.String, PickUpLocationLocationCode);
                db.AddInParameter(dbCommand, "@pDropOffLocationLocationCodeVarchar", DbType.String, DropOffLocationLocationCode);

                db.AddInParameter(dbCommand, "@pVehicleStatusVarchar", DbType.String, VehicleStatusString);
                db.AddInParameter(dbCommand, "@pModifiedbyVarchar", DbType.String, CreatedByString);
                //db.AddInParameter(dbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.Parse(ModifiedDate));
                db.AddInParameter(dbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.ParseExact(ModifiedDate, dtFormaString, enCulture));

                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, SFStatusString);
                db.AddInParameter(dbCommand, "@pRemarksVarchar", DbType.String, RemarksString);
                db.AddInParameter(dbCommand, "@pIsBilledToCrewBit", DbType.Boolean, IsBilledToBool);
                db.AddInParameter(dbCommand, "@pActionVarchar", DbType.String, Action);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   18/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Update vehicle transaction
        /// ----------------------------------------
        /// Date Modified:  02/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable    
        /// ----------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Remove Brand,Make,Year and vehicle Type and change to vehicleID 
        /// </summary>   
        public static void vehicleUpdateTransaction(Int32 vehiclePrimaryId, Int32 seqNo, Int32 vendorId, Int32 countryID, Int32 cityId,
            Int32 branchId, Int32 vehicleID,            
            DateTime PickUpDate, string PickUpTime, DateTime DropOffDate, string DropOffTime, string PickUpLocation, string DropOffLocation,
            string vehicleStatus, string createdby, string seafarerStatus, string vehicleRemarks, Boolean vehicleBillToCrew)
        {                     
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                if (PickUpTime == "")
                {
                    PickUpTime = null;
                }
                if (DropOffTime == "")
                {
                    DropOffTime = null;
                }

                dbCommand = db.GetStoredProcCommand("uspUpdateVehicleBookings");

                db.AddInParameter(dbCommand, "@pcolIdBigint", DbType.Int32, vehiclePrimaryId);
                db.AddInParameter(dbCommand, "@pcolSeqNoInt", DbType.Int32, seqNo);
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.Int32, vendorId);
                //db.AddInParameter(dbCommand, "@pcolIsAccreditedBit", DbType.Int32, category);
                db.AddInParameter(dbCommand, "@pcolCountryIDInt", DbType.Int32, countryID);
                db.AddInParameter(dbCommand, "@pcolCityIDInt", DbType.Int32, cityId);
                db.AddInParameter(dbCommand, "@pcolBranchIDInt", DbType.Int32, branchId);
                db.AddInParameter(dbCommand, "@pVehicleIDInt", DbType.Int32, vehicleID);

                //db.AddInParameter(dbCommand, "@pcolVehicleBrandIDInt", DbType.Int32, vehicleBrandId);
                //db.AddInParameter(dbCommand, "@pcolVehicleMakeIdInt", DbType.Int32, vehicleMakeId);
                //db.AddInParameter(dbCommand, "@pcolVehicleTypeIdInt", DbType.Int32, vehicleTypeId);
                //db.AddInParameter(dbCommand, "@pcolVehicleYearVarchar", DbType.String, vehicleYear);
                //db.AddInParameter(dbCommand, "@pcolVehiclePlateNoVarchar", DbType.String, vehiclePlateNo);
                db.AddInParameter(dbCommand, "@pcolPickUpDate", DbType.DateTime, PickUpDate);
                db.AddInParameter(dbCommand, "@pcolPickUpTime", DbType.Time, PickUpTime);
                db.AddInParameter(dbCommand, "@pcolDropOffDate", DbType.DateTime, DropOffDate);
                db.AddInParameter(dbCommand, "@pcolDropOffTime", DbType.Time, DropOffTime);
                db.AddInParameter(dbCommand, "@pcolPickUpLocationLocationCodeVarchar", DbType.String, PickUpLocation);
                db.AddInParameter(dbCommand, "@pcolDropOffLocationLocationCodeVarchar", DbType.String, DropOffLocation);
                db.AddInParameter(dbCommand, "@pcolVehicleStatusVarchar", DbType.String, vehicleStatus);
                db.AddInParameter(dbCommand, "@pcolCreatedbyVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolSFStatus", DbType.String, seafarerStatus);
                db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, vehicleRemarks);
                db.AddInParameter(dbCommand, "@pcolIsBilledToCrewBit", DbType.Boolean, vehicleBillToCrew);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Update vehicle transaction (non-Sabre data)
        /// </summary>             
        public static void vehicleUpdateTransactionOther(Int32 vehicleBookingID, Int32 vendorId, Int32 countryID, Int32 cityId,
            Int32 branchId, Int32 vehicleID,            
            DateTime PickUpDate, string PickUpTime, DateTime DropOffDate, string DropOffTime, string PickUpLocation, string DropOffLocation,
            string vehicleStatus, string createdby, string seafarerStatus, string vehicleRemarks, Boolean vehicleBillToCrew)
        {            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                if (PickUpTime == "")
                {
                    PickUpTime = null;
                }
                if (DropOffTime == "")
                {
                    DropOffTime = null;
                }

                dbCommand = db.GetStoredProcCommand("uspUpdateVehicleBookingsOther");

                db.AddInParameter(dbCommand, "@pTransVehicleIDBigInt", DbType.Int32, vehicleBookingID);
                
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.Int32, vendorId);
                //db.AddInParameter(dbCommand, "@pcolIsAccreditedBit", DbType.Int32, category);
                db.AddInParameter(dbCommand, "@pcolCountryIDInt", DbType.Int32, countryID);
                db.AddInParameter(dbCommand, "@pcolCityIDInt", DbType.Int32, cityId);
                db.AddInParameter(dbCommand, "@pcolBranchIDInt", DbType.Int32, branchId);
                db.AddInParameter(dbCommand, "@pVehicleIDInt", DbType.Int32, vehicleID);

                //db.AddInParameter(dbCommand, "@pcolVehicleBrandIDInt", DbType.Int32, vehicleBrandId);
                //db.AddInParameter(dbCommand, "@pcolVehicleMakeIdInt", DbType.Int32, vehicleMakeId);
                //db.AddInParameter(dbCommand, "@pcolVehicleTypeIdInt", DbType.Int32, vehicleTypeId);
                //db.AddInParameter(dbCommand, "@pcolVehicleYearVarchar", DbType.String, vehicleYear);
                //db.AddInParameter(dbCommand, "@pcolVehiclePlateNoVarchar", DbType.String, vehiclePlateNo);
                db.AddInParameter(dbCommand, "@pcolPickUpDate", DbType.DateTime, PickUpDate);
                db.AddInParameter(dbCommand, "@pcolPickUpTime", DbType.Time, PickUpTime);
                db.AddInParameter(dbCommand, "@pcolDropOffDate", DbType.DateTime, DropOffDate);
                db.AddInParameter(dbCommand, "@pcolDropOffTime", DbType.Time, DropOffTime);
                db.AddInParameter(dbCommand, "@pcolPickUpLocationLocationCodeVarchar", DbType.String, PickUpLocation);
                db.AddInParameter(dbCommand, "@pcolDropOffLocationLocationCodeVarchar", DbType.String, DropOffLocation);
                db.AddInParameter(dbCommand, "@pcolVehicleStatusVarchar", DbType.String, vehicleStatus);
                db.AddInParameter(dbCommand, "@pcolCreatedbyVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolSFStatus", DbType.String, seafarerStatus);
                db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, vehicleRemarks);
                db.AddInParameter(dbCommand, "@pcolIsBilledToCrewBit", DbType.Boolean, vehicleBillToCrew);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   19/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Delete seafarer vehicle transaction    
        /// ----------------------------------------
        /// Date Modified:  08/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter seqInt
        /// </summary>  
        public static void vehicleDeleteBooking(Int32 vehiclePrimaryId, int seqInt, string deletedBy)
        {                    
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteVehicleBookings");
                db.AddInParameter(dbCommand, "@pcolIdBigint", DbType.Int32, vehiclePrimaryId);
                db.AddInParameter(dbCommand, "@pcolSeqNoInt", DbType.Int32, seqInt);
                db.AddInParameter(dbCommand, "@pcolModifiedByVarchar", DbType.String, deletedBy);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete seafarer vehicle transaction (Non-Sabre data)
        /// ----------------------------------------
        /// </summary>  
        public static void vehicleDeleteBookingOther(Int32 VehicleBookingID, string DeletedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteVehicleBookingsOther");
                db.AddInParameter(dbCommand, "@pTransVehicleIDBigInt", DbType.Int32, VehicleBookingID);
                db.AddInParameter(dbCommand, "@pModifiedByVarchar", DbType.String, DeletedBy);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete seafarer pending vehicle transaction
        /// ----------------------------------------
        /// </summary>  
        public static void vehicleDeleteBookingPending(string PendingID, string DeletedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dt = new DataTable();
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteVehicleBookingsPending");
                db.AddInParameter(dbCommand, "@pPendingVehicleIDBigInt", DbType.Int32, Int32.Parse(PendingID));
                db.AddInParameter(dbCommand, "@pModifiedByVarchar", DbType.String, DeletedBy);

                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                dbConnection.Close();
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 04/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Validate vehicle pick-up date and time
        /// </summary>  
        public static Boolean pickupdatetimeExist(String travelLocatorId, String SeqNo, String branchId, DateTime pickupDate, String pickupTime)
        {            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspValidateVehiclePickUpDateTime");
                db.AddInParameter(dbCommand, "@pcolIdBigint", DbType.String, travelLocatorId);
                db.AddInParameter(dbCommand, "@pcolSeqNoInt", DbType.String, SeqNo);
                db.AddInParameter(dbCommand, "@pcolBranchIdInt", DbType.String, branchId);
                db.AddInParameter(dbCommand, "@pcolPickUpDate", DbType.DateTime, pickupDate);
                db.AddInParameter(dbCommand, "@pcolPickUpTime", DbType.String, pickupTime);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                if (dt.Rows.Count > 0)
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
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 29/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) remove vehicle type 
        /// </summary>
        public static void RemoveVehicleType(string BranchID, string VehicleID, string Username)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            //DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVehicleBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolBranchIDInt", DbType.Int32, Convert.ToInt32(BranchID));
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVehicleID", DbType.Int32, Convert.ToInt32(VehicleID));
                SFDatebase.AddInParameter(SFDbCommand, "@Username", DbType.String, Username);

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
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Approve Pending vehicle transaction
        /// </summary>
        /// <param name="PendingVehicleID"></param>
        /// <param name="ApprovedBy"></param>
        public static DataTable vehicleApproveTransaction(string PendingVehicleID, string ApprovedBy)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            DataTable dTable = new DataTable();
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspApproveVehicleBookingsPending");
                db.AddInParameter(command, "@pPendingVehicleIDBigInt", DbType.Int64,Int64.Parse(PendingVehicleID));
                db.AddInParameter(command, "@pApprovedBy", DbType.String, ApprovedBy);
                db.AddInParameter(command, "@pApprovedDatetime", DbType.DateTime, DateTime.Now);
                db.AddOutParameter(command, "@PK", DbType.Int32, 8);
                db.AddOutParameter(command, "@SeqNo", DbType.Int32, 8);

                db.ExecuteNonQuery(command, trans);
                trans.Commit();

                Int32 ID = Convert.ToInt32(db.GetParameterValue(command, "@PK"));
                Int32 Seq = Convert.ToInt32(db.GetParameterValue(command, "@SeqNo"));

                dTable.Columns.Add("PK");
                dTable.Columns.Add("SeqNum");

                dTable.Rows.Add(ID, Seq);
                return dTable;
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
        /// Date Created:   02/11/2011
        /// Created By:     Marco Abejar
        /// (description)   Get vehicle driver
        public static List<Driver> vehicleDriver(string VendorId)
        {
            List<Driver> list = new List<Driver>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetVendorVehicleDriver");
                db.AddInParameter(dbComm, "@VendorId", DbType.Int32, VendorId);
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new Driver
                        {
                            DriverId = GlobalCode.Field2Int(a["colDriverIDInt"]),
                            DriverName = a.Field<string>("colDriverName"),
                            DriverImage = a.Field<string>("colDriverImageFilename")
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   02/11/2011
        /// Created By:     Marco Abejar
        /// (description)   Get vendor vehicle type
        public static List<VehicleType> vendorvehicleType()
        {
            List<VehicleType> list = new List<VehicleType>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetVendorVehicleType");
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new VehicleType
                        {
                            VehicleTypeID = GlobalCode.Field2Int(a["colVehicleTypeIDInt"]),
                            VehicleTypeName = a.Field<string>("colVehicleTypeNameVarchar")
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }           
        }
        /// <summary>
        /// Date Created:   02/11/2011
        /// Created By:     Marco Abejar
        /// (description)   Get vendor vehicle plate
        public static List<VehiclePlate> vendorvehiclePlate(string vendorID, string VehicleType)
        {
            List<VehiclePlate> list = new List<VehiclePlate>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetVendorVehiclePlate");
                db.AddInParameter(dbComm, "@VendorId", DbType.Int32, vendorID);
                db.AddInParameter(dbComm, "@VehicleTypeID", DbType.Int32, VehicleType);
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new VehiclePlate
                        {
                            VehiclePlateID = GlobalCode.Field2Int(a["colPlateID"]),
                            VehiclePlateName = a.Field<string>("colPlateNumber")
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   02/11/2011
        /// Created By:     Marco Abejar
        /// (description)   Save vehicle assignment
        public static void vendorAssignVehicle(string status, string IDS, string VehicleTypeID, string driverID, string vehiclePlate, string DispatchTime, string sUserID)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            DataTable dTable = new DataTable();
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspAssignVehicle");
                db.AddInParameter(command, "@BigInts", DbType.String, IDS);
                db.AddInParameter(command, "@VehicleTypeID", DbType.String, VehicleTypeID);
                db.AddInParameter(command, "@DriverIDInt", DbType.String, driverID);
                db.AddInParameter(command, "@VehiclePlate", DbType.String, vehiclePlate);
                db.AddInParameter(command, "@DispatchTime", DbType.String, DispatchTime);
                db.AddInParameter(command, "@UserID", DbType.String, sUserID);
                db.AddInParameter(command, "@Status", DbType.String, status);

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
        /// Date Created:   16/10/2013
        /// Created By:     Marco Abejar
        /// (description)   Add driver
        public static void vendorAddDriver(string VendorID, string drivername, string ImageFilename, string sUserID)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            DataTable dTable = new DataTable();
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspAddVendorDriver");
                db.AddInParameter(command, "@VehiclevendorID", DbType.String, VendorID);
                db.AddInParameter(command, "@DriverName", DbType.String, drivername);
                db.AddInParameter(command, "@DriverImageFile", DbType.String, ImageFilename);
                db.AddInParameter(command, "@UserID", DbType.String, sUserID);

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
        /// Author:         Marco Abejar
        /// Date Created:   10/Oct/2013
        /// Descrption:     Get vehicle manifest 
        /// </summary>
        public static void GetVehicleManifestByVendor(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
            int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            Int32 iCount = 0;
            Int32 iCountConfirm = 0;
            Int32 iCountCancel = 0;
            List<VehicleManifestList> TentativeManifest = new List<VehicleManifestList>();
            List<VehicleManifestList> ConfirmedManifest = new List<VehicleManifestList>();
            List<VehicleManifestList> CancelledManifest = new List<VehicleManifestList>();

            List<VehicleVendorList> listVehicle = new List<VehicleVendorList>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dtManifestNew = null;
            DataTable dtManifestConfirm = null;
            DataTable dtManifestCancel = null;

            DataTable dtVehicle = null;

            DataSet ds = null;

            HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
            HttpContext.Current.Session["VehiclManifes_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["VehiclManifes_CancelledManifest"] = CancelledManifest;

            HttpContext.Current.Session["VehiclManifest_ManifestCountByVendor"] = iCount;
            HttpContext.Current.Session["VehiclManifes_ConfirmedManifest"] = iCountConfirm;
            HttpContext.Current.Session["VehiclManifes_CancelledManifest"] = iCountCancel;

            //HttpContext.Current.Session["VehiclManifest_ManifestList"] = list;
            HttpContext.Current.Session["VehiclManifest_VehicleVendor"] = listVehicle;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifest");
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, dDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, iRegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, iPortID);
                db.AddInParameter(dbCommand, "@pcolVehicleVendorIDInt", DbType.Int32, iVehicleID);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);
                ds = db.ExecuteDataSet(dbCommand);
                iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                dtManifestNew = ds.Tables[1];

                dtManifestConfirm = ds.Tables[3];

                dtManifestCancel = ds.Tables[5];

                dtVehicle = ds.Tables[6];


                TentativeManifest = (from a in dtManifestNew.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                         colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                         VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                         VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                         colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                         colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                         colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                         colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                         colSFStatus = a.Field<string>("colSFStatus"),
                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         //colRankIDInt, 
                                         RankName = a.Field<string>("RankName"),
                                         //colCostCenterIDInt, 
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                         colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                         VesselName = a.Field<string>("VesselName")

                                     }).ToList();

                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new VehicleVendorList
                               {
                                   VehicleVendorID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                   VehicleVendorName = a.Field<string>("colVehicleVendorNameVarchar"),
                               }).ToList();


                HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
                HttpContext.Current.Session["VehiclManifes_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["VehiclManifes_CancelledManifest"] = CancelledManifest;

                HttpContext.Current.Session["VehiclManifest_ManifestCount"] = iCount;
                HttpContext.Current.Session["VehiclManifes_ConfirmedManifest"] = iCountConfirm;
                HttpContext.Current.Session["VehiclManifes_CancelledManifest"] = iCountCancel;

                HttpContext.Current.Session["VehiclManifest_VehicleVendor"] = listVehicle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtManifestNew != null)
                {
                    dtManifestNew.Dispose();
                }
                if (dtManifestConfirm != null)
                {
                    dtManifestConfirm.Dispose();
                }
                if (dtManifestCancel != null)
                {
                    dtManifestCancel.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   15/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List of Vehicle Vendor
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetVehicleVendorAirportList(int iVehicleVendor, Int16 iLoadType)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspVehicleVendorsGetAirportAccess");
                db.AddInParameter(dbComm, "@pVehicleVendorIDInt", DbType.Int32, iVehicleVendor);
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, iLoadType );
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new AirportDTO
                        {
                            AirportIDString = GlobalCode.Field2String(a["AirportID"]),
                            AirportNameString = a.Field<string>("AirportName"),
                            AirportCodeString = a.Field<string>("AirportCode")
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// Date Created:   15/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport List of Vehicle Vendor
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<SeaportDTO> GetVehicleVendorSeaportList(int iVehicleVendor, Int16 iLoadType)
        {
            List<SeaportDTO> list = new List<SeaportDTO>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspVehicleVendorsGetSeaportAccess");
                db.AddInParameter(dbComm, "@pVehicleVendorIDInt", DbType.Int32, iVehicleVendor);
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, iLoadType);
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new SeaportDTO
                        {
                            SeaportIDString = GlobalCode.Field2String(a["SeaportID"]),
                            SeaportNameString = GlobalCode.Field2String(a["SeaportName"]),
                            SeaportCodeString = a.Field<string>("SeaportCode")
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

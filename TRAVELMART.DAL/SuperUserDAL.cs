using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class SuperUserDAL
    {
        /// <summary>
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer personal info
        /// -----------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable 
        /// -----------------------------------------------------------            
        /// </summary>      
        public static DataTable GetSFInfo(string sfCode)
        {                   
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()            
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFPersonalInfo");
                SFDatebase.AddInParameter(SFDbCommand, "@pSfCode", DbType.String, sfCode);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }              
            }
        }
        /// <summary>            
        /// Date Created: 21/07/2011
        /// Created By: Josephine Gad
        /// (description) Get seafarer personal info based from status and itinerary no            
        /// </summary>   
        public static DataTable GetSFTravelInfo(string sfCode, string status, string recloc)
        {                                
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFPersonalInfo");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSeafarerIdInt", DbType.String, sfCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatusVarchar", DbType.String, status);
                SFDatebase.AddInParameter(SFDbCommand, "@pItineraryreclocTinyint", DbType.String, recloc);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }               
            }
        }
        /// <summary>                        
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer air travel info
        /// --------------------------------------------------
        /// Date Modified: 20/07/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter Status and ItineraryNo
        /// --------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable            
        /// </summary>  
        public static DataTable GetSFAirTravelDetails(string sfCode, string status, string recloc, string travelreqId)
        {                     
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFAirTravelInfo");
                SFDatebase.AddInParameter(SFDbCommand, "@pSfCode", DbType.String, sfCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, status);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolRecordLocatorVarchar", DbType.String, recloc);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.String, travelreqId);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }                
            }
        }
        /// <summary>                        
        /// Date Created:   31/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer air bookings by travel request ID
        /// --------------------------------------------------                  
        /// </summary>   
        public static DataTable GetSFAirTravelDetailsAll(string sfCode, string status)
        {                    
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFAirTravelInfoAll");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int32, Int32.Parse(sfCode));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, status);                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer vehicle travel info
        /// --------------------------------------------------
        /// Date Modified: 20/07/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter Status and ItineraryNo
        /// --------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable 
        /// --------------------------------------------------
        /// </summary>
        public static DataTable GetSFVehicleTravelDetails(string sfCode, string status, string recloc)
        {                 
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()          
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFVehicleInfo");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSeafarerIdInt", DbType.String, sfCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, status);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolRecordLocatorVarchar", DbType.String, recloc);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }               
            }
        }
        /// <summary>            
        /// Date Created:   02/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer vehicle bookings by travel request ID
        /// -------------------------------------------------
        /// </summary>
        public static DataTable GetSFVehicleTravelDetailsAll(string TravelReqIDInt, string statusString)
        {            
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFVehicleInfoAll");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.String, TravelReqIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, statusString);                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 08/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer vehicle travel info
        /// --------------------------------------------------
        /// Date Modified: 20/07/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter Status and ItineraryNo
        /// </summary>    
        public static DataTable GetSFHotelDetails(string sfCode, string status, string recloc)
        {                   
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
             DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFHotelInfo");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSeafarerIdInt", DbType.String, sfCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, status);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolRecordLocatorVarchar", DbType.String, recloc);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }               
            }
        }
        /// <summary>
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer hotel bookings by travel request ID
        /// --------------------------------------------------       
        /// </summary>    
        public static DataTable GetSFHotelDetailsAll(string TravelReqIDInt, string ManualRequestIdInt, string statusString)
        {                  
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFHotelInfoAll");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, GlobalCode.Field2Int(TravelReqIDInt));
                SFDatebase.AddInParameter(SFDbCommand, "@pManualReqIDInt", DbType.Int64, GlobalCode.Field2Int(ManualRequestIdInt));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, statusString);                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:   21/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer port information per travel request ID/ request ID
        /// --------------------------------------------------        
        /// Date Modified:  03/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter ViewInTR for the data in TblNoTravelRequest
        /// --------------------------------------------------      
        /// Date Modified:  17/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Remove parameter ViewInTR 
        /// --------------------------------------------------   
        /// </summary>     
        public static DataTable GetSFPortTravelDetailsByTravelReqID(string TravelReqID, string ManualReqID, string SFStatus)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;            
            DataTable InfoDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFPortInfoByTravelRequestID");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int64, Int64.Parse(TravelReqID));                
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int64, Int64.Parse(ManualReqID));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, SFStatus);
                //SFDatebase.AddInParameter(SFDbCommand, "@pViewInTR", DbType.Boolean, ViewInTR);
                InfoDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];                
                return InfoDataTable;
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
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   11/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Get seafarer port travel info
        /// --------------------------------------------------
        /// Date Modified:  21/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter Status and ItineraryNo
        /// --------------------------------------------------
        /// Date Modified:  11/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change parameters to TravelReqID, RecLoc, and ManualReqID
        /// </summary>     
        public static DataTable GetSFPortTravelDetails(string TravelReqID, string RecLoc, string ManualReqID, string SFStatus)
        {                   
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()           
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable InfoDataTable = null; 
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFPortInfo");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int64, Int64.Parse(TravelReqID));
                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, RecLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int64, Int64.Parse(ManualReqID));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, SFStatus);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                InfoDataTable = new DataTable();
                InfoDataTable.Load(dataReader);
                return InfoDataTable;
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
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (InfoDataTable != null)
                {
                    InfoDataTable.Dispose();
                }              
            }
        }
        /// <summary>
        /// Date Created:   24/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vendor by Type (HO/VE)
        /// ------------------------------------------
        /// Date Created:   21/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Add parameter country and city
        /// </summary>      
        public static DataTable GetVendor(string TypeString, bool IsAccredited, string country, string city, string port, string user, string role)
        {                 
            DataTable VendorDataTable = null;
            DbCommand VendorDbCommand = null;
            try
            {
                Database VendorDatabase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                country = (country == "" ? "0" : country);
                city = (city == "" ? "0" : city);

                VendorDbCommand = VendorDatabase.GetStoredProcCommand("uspSelectVendor");
                VendorDatabase.AddInParameter(VendorDbCommand, "@pType", DbType.String, TypeString);
                VendorDatabase.AddInParameter(VendorDbCommand, "@pAccredited", DbType.Boolean, IsAccredited);
                VendorDatabase.AddInParameter(VendorDbCommand, "@pCountry", DbType.Int32, Int32.Parse(country));
                VendorDatabase.AddInParameter(VendorDbCommand, "@pCity", DbType.Int32, Int32.Parse(city));
                VendorDatabase.AddInParameter(VendorDbCommand, "@pPort", DbType.Int32, Int32.Parse(port));
                VendorDatabase.AddInParameter(VendorDbCommand, "@pUser", DbType.String, user);
                VendorDatabase.AddInParameter(VendorDbCommand, "@pRole", DbType.String, role);
                VendorDataTable = VendorDatabase.ExecuteDataSet(VendorDbCommand).Tables[0];
                return VendorDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VendorDbCommand != null)
                {
                    VendorDbCommand.Dispose();
                }
                if (VendorDataTable != null)
                {
                    VendorDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   10/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle booking by record locator or manual request ID, travel req ID, and SF ID
        /// </summary>      
        public static DataTable GetSFVehicleTravelDetailsByID(string TravelReqID, string SfID, string sfStatus,
            string recLoc, string manualReqID)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                command = db.GetStoredProcCommand("uspGetSFVehicleInfoByID");
                db.AddInParameter(command, "@pTravelReqIDInt", DbType.Int64, Int64.Parse(TravelReqID));
                db.AddInParameter(command, "@pSeafarerIdInt", DbType.Int64, Int64.Parse(SfID));
                db.AddInParameter(command, "@pSFStatus", DbType.String, sfStatus);
                db.AddInParameter(command, "@pRecordLocator", DbType.String, recLoc);
                db.AddInParameter(command, "@pRequestIDInt", DbType.Int64, Int64.Parse(manualReqID));
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
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get pending vehicle bookings
        /// </summary>      
        public static DataTable GetSFVehicleTravelDetailsPending(string TravelReqID, string manualReqID)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                command = db.GetStoredProcCommand("uspGetSFVehiclePendingByID");
                db.AddInParameter(command, "@pTravelReqIDInt", DbType.Int64, Int64.Parse(TravelReqID));                
                db.AddInParameter(command, "@pRequestIDInt", DbType.Int64, Int64.Parse(manualReqID));
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
        /// Date Created:   10/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel booking by record locator or manual request ID, travel req ID, and SF ID
        /// </summary>      
        public static DataTable GetSFHotelDetailsByID(string TravelReqID, string SfID, string sfStatus,
            string recLoc, string manualReqID)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                command = db.GetStoredProcCommand("uspGetSFHotelInfoByID");
                db.AddInParameter(command, "@pTravelReqIDInt", DbType.Int64, Int64.Parse(TravelReqID));
                db.AddInParameter(command, "@pSeafarerIdInt", DbType.Int64, Int64.Parse(SfID));
                db.AddInParameter(command, "@pSFStatus", DbType.String, sfStatus);
                db.AddInParameter(command, "@pRecordLocator", DbType.String, recLoc);
                db.AddInParameter(command, "@pRequestIDInt", DbType.Int64, Int64.Parse(manualReqID));
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
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get pending hotel bookings
        /// </summary>      
        public static DataTable GetSFHotelTravelDetailsPending(string TravelReqID, string manualReqID)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()

                command = db.GetStoredProcCommand("uspGetSFHotelPendingByID");
                db.AddInParameter(command, "@pTravelReqIDInt", DbType.Int64, Int64.Parse(TravelReqID));
                db.AddInParameter(command, "@pRequestIDInt", DbType.Int64, Int64.Parse(manualReqID));
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
    }
}

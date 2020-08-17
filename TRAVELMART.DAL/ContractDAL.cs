using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.IO;
using System.Configuration;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class ContractDAL
    {
        /// Date Modified:  10/Jun/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Add trans.Dispose(0)
        /// 
        public static DataTable GetCurrencyList()
        {
            
            /// <summary>            
            /// Date Created: 23/08/2011
            /// Created By: Ryan Bautista
            /// (description) Selecting all currency
            /// </summary>            

            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCurrencyList");
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Select currency by country ID
        /// ---------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>    
        public static IDataReader GetCurrencyByCountry(Int16 CountryID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCurrencyByCountryID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolCountryID", DbType.Int16, CountryID);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }

        /// <summary>            
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of port transaction details 
        /// </summary>     
        public static DataTable GetVendorHotelContractList(string Hotel, string UserName)
        {                  
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelContractList");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelName", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserName", DbType.String, UserName);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }
        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel contract details 
        /// ----------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>    
        public static IDataReader GetVendorHotelContract(string vmId)
        {                    
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelContract");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorIdInt", DbType.Int32, vmId);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel contract details 
        /// </summary>     
        public static DataTable GetVendorHotelBranchContract(string vmId)
        {            
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelBranchContract");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorIdInt", DbType.Int32, vmId);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch contract details by branch ID
        /// </summary>     
        public static DataTable GetVendorHotelBranchContractByBranchID(string HotelBranchID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelBranchContractByBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorBranchIdInt", DbType.Int32, Convert.ToInt32(HotelBranchID));
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch contract details by branch ID
        /// </summary>     
        public static DataTable GetVendorHotelBranchContractActiveByBranchID(string HotelBranchID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelBranchContractActiveByBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorBranchIdInt", DbType.Int32, Convert.ToInt32(HotelBranchID));
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch contract pending
        /// </summary>     
        public static DataTable GetVendorHotelBranchPendingContract()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelBranchContractPending");
                //SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorBranchIdInt", DbType.Int32, Convert.ToInt32(HotelBranchID));
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch contract pending
        /// --------------------------------------------------------------
        /// Date Modified:  18/Jul/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter sVehicleName
        /// --------------------------------------------------------------
        /// </summary>     
        public static DataTable GetVendorVehicleBranchPendingContract(string sVehicleName)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorVehicleBranchContractPending");
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorName", DbType.String, sVehicleName);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch no active contract 
        /// </summary>     
        public static DataTable GetVendorHotelBranchNoActiveContract(string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorBranchNoActiveContractList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, Username);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch no active contract 
        /// </summary>     
        public static DataTable GetVendorVehicleBranchNoActiveContract(string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleVendorBranchNoActiveContractList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, Username);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created:   08/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of hotel contract details By Room
        /// ---------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ---------------------------------------------------------------
        /// Date Modified: 02/01/2011
        /// Modified By:   Josephine Gad
        /// (description)  Change vendorID to BranchID
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>            
        public static IDataReader GetVendorHotelBranchContractByRoomType(string branchID, string RoomID)
        {            
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                if (GlobalCode.Field2String(HttpContext.Current.Session["UserRole"])
                    == TravelMartVariable.RolePortSpecialist)
                {

                }
                else
                {
                    SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelBranchContractByRoomType");
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pcolBranchIDInt", DbType.Int32, branchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolRoomTypeID", DbType.Int32, RoomID);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }

        /// <summary>            
        /// Date Created:   08/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vendor branch maintenance information                 
        /// -------------------------------------------------------------
        /// Date Modified:  25/Sept/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void
        ///                 Put the data in Sessions
        /// -------------------------------------------------------------
        /// Date Modified:  3/Oct/2013
        /// Modified By:    Marco Abejar
        /// (description)   Modify route fields
        /// -------------------------------------------------------------
        /// Date Modified:  06/Feb/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add Currency Name
        /// -------------------------------------------------------------
        /// </summary> 
        public static void GetVendorVehicleContractByContractID(string contractId, string branchId, Int16 iLoadType)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            //DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand com = null;
            DataSet ds = null;
            DataTable dt = null;
            DataTable dtAirContract = null;
            DataTable dtAirportNotInContract = null;

            DataTable dtSeaContract = null;
            DataTable dtSeaNotInContract = null;

            DataTable dtVehicleType = null;
            DataTable dtCapacity = null;
            DataTable dtAttachment = null;
            DataTable dtCurrency = null;
            DataTable dtRoute = null;
            DataTable dtDetails = null;

            List<ContractVehicleDetails> list = new List<ContractVehicleDetails>();
            
            List<Airport> listAirportContract = new List<Airport>();
            List<Airport> listAirportNotInContract = new List<Airport>();

            List<Seaport> listSeaportContract = new List<Seaport>();
            List<Seaport> listSeaportNotInContract = new List<Seaport>();

            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();

            List<VehicleRoute> listRoute = new List<VehicleRoute>();

            List<Currency> listCurrency = new List<Currency>();
            List<ContractVehicleAttachment> listAttachment = new List<ContractVehicleAttachment>();

            List<ContractVehicleDetailsAmt> listDetails = new List<ContractVehicleDetailsAmt>();

            HttpContext.Current.Session["ContractVehicleDetails"] = list;

            HttpContext.Current.Session["VendorAirportExists"] = listAirportContract;
            HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotInContract;

            HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
            HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;

            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;

            HttpContext.Current.Session["ContractCurrency"] = listCurrency;
            HttpContext.Current.Session["ContractVehicleAttachment"] = listAttachment;

            HttpContext.Current.Session["VehicleRoute"] = listRoute;
            HttpContext.Current.Session["ContractVehicleDetailsAmt"] = listDetails;

            try
            {
                com = db.GetStoredProcCommand("uspGetVendorVehicleContractByContractID");
                db.AddInParameter(com, "@pcolContractIdInt", DbType.Int32, contractId);
                db.AddInParameter(com, "@pcolBranchIdInt", DbType.Int32, branchId);
                db.AddInParameter(com, "@pLoadType", DbType.Int16, iLoadType);
                ds = db.ExecuteDataSet(com);
                //dt = VendorTransactionDatebase.ExecuteDataSet(VMDbCommand).Tables[0];
                //return dt;

                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    list = (from a in dt.AsEnumerable()
                            select new ContractVehicleDetails
                            {
                                ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                BranchID = GlobalCode.Field2Int(a["VehicleVendorID"]),

                                VehicleName = a.Field<string>("VehicleVendorName"),
                                CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                CityName = a.Field<string>("City"),
                                CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                CountryName = a.Field<string>("Country"),

                                Address = a.Field<string>("colAddressVarchar"),
                                ContactNo = a.Field<string>("colContactNoVarchar"),
                                ContactPerson = a.Field<string>("colContactPersonVarchar"),

                                EmailCc = a.Field<string>("colEmailCcVarchar"),
                                EmailTo = a.Field<string>("colEmailToVarchar"),

                                FaxNo =  a.Field<string>("colFaxNoVarchar"),
                                
                                ContractDateStart =  a.Field<DateTime?>("colContractDateStartedDate"),
                                ContractDateEnd = a.Field<DateTime?>("colContractDateEndDate"),
                                ContractName = a.Field<string>("colContractNameVarchar"),
                                ContractStatus = a.Field<string>("colContractStatusVarchar"),

                                CurrencyID  = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                CurrencyName = GlobalCode.Field2String(a["CurrencyName"]),

                                RCCLAcceptedDate = a.Field<DateTime?>("colRCCLAcceptedDate"),
                                RCCLPersonnel = a.Field<string>("colRCCLPersonnel"),
       
                                VendorAcceptedDate = a.Field<DateTime?>("colVendorAcceptedDate"),
                                VendorPersonnel = a.Field<string>("colVendorPersonnel"),

                                Remarks = a.Field<string>("colRemarksVarchar"),
                                IsAirportToHotel = GlobalCode.Field2Bool(a["colIsAutoAirportToHotel"]),
                                IsHotelToShip = GlobalCode.Field2Bool(a["colIsAutoHotelToShip"]),

                                IsRCI = GlobalCode.Field2Bool(a["colIsRCI"]),
                                IsAZA = GlobalCode.Field2Bool(a["colIsAZA"]),
                                IsCEL = GlobalCode.Field2Bool(a["colIsCEL"]),
                                IsPUL = GlobalCode.Field2Bool(a["colIsPUL"]),
                                IsSKS = GlobalCode.Field2Bool(a["colIsSKS"]),
                            }).ToList();
                }
                
                if (iLoadType == 0)
                {
                    dtAirContract = ds.Tables[1];
                    dtAirportNotInContract = ds.Tables[2];

                    dtSeaContract = ds.Tables[3];
                    dtSeaNotInContract = ds.Tables[4];

                    dtVehicleType = ds.Tables[5];
                    dtCapacity = ds.Tables[6];
                    dtAttachment = ds.Tables[7];
                    dtCurrency = ds.Tables[8];
                    dtRoute = ds.Tables[9];
                    dtDetails = ds.Tables[10];

                    listAirportContract = (from a in dtAirContract.AsEnumerable()
                                           select new Airport
                                           {
                                               AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                               AirportName = a.Field<string>("Airport")
                                           }).ToList();

                    listAirportNotInContract = (from a in dtAirportNotInContract.AsEnumerable()
                                                select new Airport
                                                {
                                                    AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                    AirportName = a.Field<string>("Airport")
                                                }).ToList();


                    listSeaportContract = (from a in dtSeaContract.AsEnumerable()
                                           select new Seaport
                                           {
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = a.Field<string>("Seaport")
                                           }).ToList();

                    listSeaportNotInContract = (from a in dtSeaNotInContract.AsEnumerable()
                                                select new Seaport
                                                {
                                                    SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                    SeaportName = a.Field<string>("Seaport")
                                                }).ToList();

                    listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                       select new VehicleType
                                       {
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])

                                       }).ToList();

                    listVehicleTypeCapacity = (from a in dtCapacity.AsEnumerable()
                                               select new ContractVendorVehicleTypeCapacity
                                               {
                                                   ContractVehicleCapacityIDInt = GlobalCode.Field2Int(a["ContractVehicleCapacityID"]),
                                                   ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                   VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                   VehicleType = GlobalCode.Field2String(a["VehicleTypeName"]),
                                                   MinCapacity = GlobalCode.Field2Int(a["MinCapacity"]),
                                                   MaxCapacity = GlobalCode.Field2Int(a["MaxCapacity"]),
                                               }).ToList();


                    listCurrency = (from a in dtCurrency.AsEnumerable()
                                    select new Currency 
                                    {
                                        CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                        CurrencyName = a.Field<string>("Currency")
                                    }).ToList();
                    listAttachment = (from a in dtAttachment.AsEnumerable()
                                        select new ContractVehicleAttachment
                                        {
                                            FileName = a.Field<string>("colFileNameVarChar"),
                                            FileType = a.Field<string>("colFileTypeVarChar"),
                                            UploadedDate = a.Field<DateTime>("colUploadedDate"),
                                            uploadedFile = a.Field<byte[]>("colContractAttachmentVarBinary"),
                                            AttachmentId = a.Field<int>("colContractAttachmentIdInt")
                                        }).ToList();

                    listRoute = (from a in dtRoute.AsEnumerable()
                                 select new VehicleRoute
                                 {
                                     RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                     RouteDesc = a.Field<string>("colRouteNameVarchar")
                                 }).ToList();

                    //listRoute = (from a in dtRoute.AsEnumerable()
                    //          select new VehicleRoute {
                    //              RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                    //              RouteOrigin = a.Field<string>("colOriginVarchar"),
                    //              RouteDestination = a.Field<string>("colDestinationVarchar")
                    //          }).ToList();

                    listDetails = (from a in dtDetails.AsEnumerable()
                                   select new ContractVehicleDetailsAmt
                                   {
                                       ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                                       ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                       BranchID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                       ContractVehicleCapacityID = GlobalCode.Field2Int(a["colContractVehicleCapacityIDInt"]),
                                       VehicleTypeID = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                       VehicleType = a.Field<string>("VehicleType"),
                                       RouteIDFrom = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                       RouteIDTo = GlobalCode.Field2Int(a["colRouteIDToInt"]),
                                       RouteFrom = a.Field<string>("RouteFrom"),
                                       RouteTo = a.Field<string>("RouteTo"),
                                       Origin = a.Field<string>("colFromVarchar"),
                                       Destination = a.Field<string>("colToVarchar"),
                                       RateAmount = GlobalCode.Field2Float(a["colRateAmount"]),
                                       Tax = GlobalCode.Field2Float(a["colTaxPercent"]),
                                   }).ToList();

                    //listDetails = (from a in dtDetails.AsEnumerable()
                    //               select new ContractVehicleDetailsAmt { 
                    //                    ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                    //                    ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                    //                    BranchID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                    //                    ContractVehicleCapacityID = GlobalCode.Field2Int(a["colContractVehicleCapacityIDInt"]),
                    //                    VehicleTypeID = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                    //                    VehicleType = a.Field<string>("VehicleType"),  
                    //                    RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                    //                    Route = a.Field<string>("VehicleRoute"),
                    //                    Origin = a.Field<string>("colOriginVarchar"),
                    //                    Destination = a.Field<string>("colDestinationVarchar"),
                    //                    RateAmount = GlobalCode.Field2Float(a["colRateAmount"]),
                    //                    Tax = GlobalCode.Field2Float(a["colTaxPercent"]),
                    //            }).ToList();

                }

                HttpContext.Current.Session["ContractVehicleDetails"] = list;
                HttpContext.Current.Session["VendorAirportExists"] = listAirportContract;
                HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotInContract;

                HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
                HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;

                HttpContext.Current.Session["VehicleType"] = listVehicleType;
                HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;
                HttpContext.Current.Session["ContractCurrency"] = listCurrency;
                HttpContext.Current.Session["ContractVehicleAttachment"] = listAttachment;
                HttpContext.Current.Session["VehicleRoute"] = listRoute;
                HttpContext.Current.Session["ContractVehicleDetailsAmt"] = listDetails;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtAirContract != null)
                {
                    dtAirContract.Dispose();
                }
                if (dtAirportNotInContract != null)
                {
                    dtAirportNotInContract.Dispose();
                }
                if (dtSeaContract != null)
                {
                    dtSeaContract.Dispose();
                }
                if (dtSeaNotInContract != null)
                {
                    dtSeaNotInContract.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtCapacity != null)
                {
                    dtCapacity.Dispose();
                }
                if (dtAttachment != null)
                {
                    dtAttachment.Dispose();
                } 
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor branch maintenance information with luggage van               
        /// </summary> 
        public static DataTable GetVendorVehicleContractWithLuggageVanByContractID(string contractId, string branchId)
        {
            Database VendorTransactionDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            //DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand VMDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVendorVehicleContractWithLuggageVanByContractID");
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolContractIdInt", DbType.Int32, contractId);
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolBranchIdInt", DbType.Int32, branchId);
                dt = VendorTransactionDatebase.ExecuteDataSet(VMDbCommand).Tables[0];
                return dt;
                //dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
                //dt = new DataTable();
                //dt.Load(dataReader);
                //return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VMDbCommand != null)
                {
                    VMDbCommand.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //}
            }
        }

        /// <summary>            
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor branch maintenance information with service rate               
        /// </summary> 
        public static DataTable GetVendorVehicleContractWithServiceRateByContractID(string contractId, string branchId)
        {
            Database VendorTransactionDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            //DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand VMDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVendorVehicleContractWithServiceRateByContractID");
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolContractIdInt", DbType.Int32, contractId);
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolBranchIdInt", DbType.Int32, branchId);
                dt = VendorTransactionDatebase.ExecuteDataSet(VMDbCommand).Tables[0];
                return dt;
                //dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
                //dt = new DataTable();
                //dt.Load(dataReader);
                //return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VMDbCommand != null)
                {
                    VMDbCommand.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //}
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel contract details 
        /// </summary> 
        public static DataTable GetVendorHotelContractByContractID(string cID, string BranchID)
        {                      
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelContractByContractID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractIdInt", DbType.Int32, cID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolBranchID", DbType.Int32, BranchID);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting hotel contract if it's live
        /// </summary> 
        public static Int32 GetVendorHotelBranchContractActiveByContractID(Int32 cID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            Int32 count = 0;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelBranchContractActiveByContractID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractIdInt", DbType.Int32, cID);
                count = (Int32)SFDatebase.ExecuteScalar(SFDbCommand);
                return count;
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
            }
        }

        /// <summary>            
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting vehicle contract if it's live
        /// </summary> 
        public static Int32 GetVendorVehicleBranchContractActiveByContractID(Int32 cID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            Int32 count = 0;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorVehicleBranchContractActiveByContractID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractIdInt", DbType.Int32, cID);
                count = (Int32)SFDatebase.ExecuteScalar(SFDbCommand);
                return count;
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
            }
        }

        /// <summary>            
        /// Date Created: 15/05/2013
        /// Created By: Marco Abejar
        /// (description) get approved contract id
        /// </summary>  
        public static Int32 GetApprovedVendorHotelBranchContractByBranchID(Int32 bID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            Int32 count = 0;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetApprovedVendorHotelBranchContractByBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorBranchIdInt", DbType.Int32, bID);
                count = (Int32)SFDatebase.ExecuteScalar(SFDbCommand);
                return count;
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
            }
        }

        /// <summary>            
        /// Date Created: 17/08/2011
        /// Created By: Marco Abejar
        /// (description) Add/save vendor contract
        /// --------------------------------------
        /// Date Modified:  31/07/2012
        /// Modified By:    Jefferson Bermundo
        /// Description:    Remove disposing of database, since transaction
        ///                 is being used in the later part for hotel contract attachments.
        /// </summary>    
        //public static Int32 AddSaveHotelContract(string VendorID, string vContract, string Remarks, string dtStart,
        //                                        string dtEnd, string RCCLRep, string vRep, string dtRCCLAccepted,
        //                                        string dtVendorAccepted, string CountryID, string CityID, string Username,
        //                                        string MealRate, string MealRateTax, bool TaxInclusive,
        //                                        bool Breakfast, bool Lunch, bool Dinner, bool LunchDinner, bool Shuttle,
        //                                        string Filename, string FileType, byte[] FileData, string DateUploaded,
        //                                        Int32 ContractIDInt, out object sqlTransaction)
        //{            
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;

        //    DbConnection connection = db.CreateConnection();
        //    connection.Open();
        //    DbTransaction trans = connection.BeginTransaction();
        //    sqlTransaction = trans;

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspAddSaveHotelContract");

        //        db.AddInParameter(dbCommand, "@pcolVendorID", DbType.String, VendorID);
        //        db.AddInParameter(dbCommand, "@pcolContractNameVarchar", DbType.String, vContract);
        //        db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, Remarks);
        //        db.AddInParameter(dbCommand, "@pcolContractDateStartedDate", DbType.String, dtStart);
        //        db.AddInParameter(dbCommand, "@pcolContractDateEndDate", DbType.String, dtEnd);
        //        db.AddInParameter(dbCommand, "@pcolRCCLPersonnel", DbType.String, RCCLRep);
        //        db.AddInParameter(dbCommand, "@pcolVendorPersonnel", DbType.String, vRep);
        //        db.AddInParameter(dbCommand, "@colRCCLAcceptedDate", DbType.String, dtRCCLAccepted);
        //        db.AddInParameter(dbCommand, "@colVendorAcceptedDate", DbType.String, dtVendorAccepted);
        //        db.AddInParameter(dbCommand, "@pcolCountryID", DbType.String, CountryID);
        //        db.AddInParameter(dbCommand, "@pcolCityID", DbType.String, CityID);
        //        db.AddInParameter(dbCommand, "@pUsername", DbType.String, Username);
        //        db.AddInParameter(dbCommand, "@pMealRate", DbType.String, MealRate);
        //        db.AddInParameter(dbCommand, "@pMealTax", DbType.String, MealRateTax);
        //        db.AddInParameter(dbCommand, "@pTaxInclusive", DbType.Boolean, TaxInclusive);
        //        db.AddInParameter(dbCommand, "@pBreakfast", DbType.Boolean, Breakfast);
        //        db.AddInParameter(dbCommand, "@pLunch", DbType.Boolean, Lunch);
        //        db.AddInParameter(dbCommand, "@pDinner", DbType.Boolean, Dinner);
        //        db.AddInParameter(dbCommand, "@pLunchDinner", DbType.Boolean, LunchDinner);
        //        db.AddInParameter(dbCommand, "@pShuttle", DbType.Boolean, Shuttle);

        //        db.AddInParameter(dbCommand, "@pFilename", DbType.String, Filename);
        //        db.AddInParameter(dbCommand, "@pFileType", DbType.String, FileType);
        //        db.AddInParameter(dbCommand, "@pFileData", DbType.Binary, FileData);
        //        db.AddInParameter(dbCommand, "@pDateUploaded", DbType.String, DateUploaded);
        //        db.AddInParameter(dbCommand, "@pContractID", DbType.Int32, ContractIDInt);


        //        db.AddOutParameter(dbCommand, "@pID", DbType.Int32, 8);
                
        //        db.ExecuteNonQuery(dbCommand, trans);
        //        //trans.Commit();
        //        Int32 pID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pID"));
        //        return pID;
        //        //
                   
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //do not close connection yet, saving for contract attachment to follow
        //        //if (connection != null)
        //        //{
        //        //    connection.Close();
        //        //    connection.Dispose();
        //        //}
        //        //if (dbCommand != null)
        //        //{
        //        //    dbCommand.Dispose();
        //        //}
        //    }
        //}


        /// <summary>            
        /// Date Created: 17/08/2011
        /// Created By: Marco Abejar
        /// (description) Add/save vendor contract
        /// --------------------------------------
        /// Date Modified:  31/07/2012
        /// Modified By:    Jefferson Bermundo
        /// Description:    Remove disposing of database, since transaction
        ///                 is being used in the later part for hotel contract attachments.
        /// --------------------------------------
        /// Date Modified:  09/12/2013
        /// Modified By:    Muhallidin G Wali
        /// Description:    Add column Contact RCCL Personnel No, and Vendor Personnel No
        /// --------------------------------------
        /// Date Modified:  14/June/2016
        /// Modified By:    Josephine Monteza
        /// Description:    Added parameter BranchID, removed parameter CountryID and CityID
        /// </summary>    
        public static Int32 AddSaveHotelContract(Int32 VendorID, Int32 BranchID, string vContract, string Remarks, string dtStart,
                                                string dtEnd, string RCCLRep, string vRep, string dtRCCLAccepted,
                                                string dtVendorAccepted, string Username,
                                                string MealRate, string MealRateTax, bool TaxInclusive,
                                                bool Breakfast, bool Lunch, bool Dinner, bool LunchDinner, bool Shuttle,
                                                string Filename, string FileType, byte[] FileData, string DateUploaded,
                                                Int32 ContractIDInt, out object sqlTransaction,
                                                string VendorRepContactNo, string RCCLRepContactNo,
                                                string VendorRepEmailAdd, string RCCLRepEmailAdd,
                                                int iCancelationTerms, string sCutoffTime ,
                                                string HotelTimeZoneID, int CurrencyIDInt, float RoomRateDbl, float RoomRateSgl)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            sqlTransaction = trans;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspAddSaveHotelContract");

                db.AddInParameter(dbCommand, "@pcolVendorID", DbType.Int32, VendorID);
                db.AddInParameter(dbCommand, "@pcolBranchID", DbType.String, BranchID);
                db.AddInParameter(dbCommand, "@pcolContractNameVarchar", DbType.String, vContract);
                db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pcolContractDateStartedDate", DbType.String, dtStart);
                db.AddInParameter(dbCommand, "@pcolContractDateEndDate", DbType.String, dtEnd);
                db.AddInParameter(dbCommand, "@pcolRCCLPersonnel", DbType.String, RCCLRep);
                db.AddInParameter(dbCommand, "@pcolVendorPersonnel", DbType.String, vRep);
                db.AddInParameter(dbCommand, "@colRCCLAcceptedDate", DbType.String, dtRCCLAccepted);
                db.AddInParameter(dbCommand, "@colVendorAcceptedDate", DbType.String, dtVendorAccepted);
                db.AddInParameter(dbCommand, "@pUsername", DbType.String, Username);
                db.AddInParameter(dbCommand, "@pMealRate", DbType.String, MealRate);
                db.AddInParameter(dbCommand, "@pMealTax", DbType.String, MealRateTax);
                db.AddInParameter(dbCommand, "@pTaxInclusive", DbType.Boolean, TaxInclusive);
                db.AddInParameter(dbCommand, "@pBreakfast", DbType.Boolean, Breakfast);
                db.AddInParameter(dbCommand, "@pLunch", DbType.Boolean, Lunch);
                db.AddInParameter(dbCommand, "@pDinner", DbType.Boolean, Dinner);
                db.AddInParameter(dbCommand, "@pLunchDinner", DbType.Boolean, LunchDinner);
                db.AddInParameter(dbCommand, "@pShuttle", DbType.Boolean, Shuttle);

                db.AddInParameter(dbCommand, "@pFilename", DbType.String, Filename);
                db.AddInParameter(dbCommand, "@pFileType", DbType.String, FileType);
                db.AddInParameter(dbCommand, "@pFileData", DbType.Binary, FileData);
                db.AddInParameter(dbCommand, "@pDateUploaded", DbType.String, DateUploaded);
                db.AddInParameter(dbCommand, "@pContractID", DbType.Int32, ContractIDInt);
                db.AddInParameter(dbCommand, "@pVendorRepContactNo", DbType.String, VendorRepContactNo);
                db.AddInParameter(dbCommand, "@pRCCLRepContactNo", DbType.String, RCCLRepContactNo);
                db.AddInParameter(dbCommand, "@pVendorRepEmailAdd", DbType.String, VendorRepEmailAdd);
                db.AddInParameter(dbCommand, "@pRCCLRepEmailAdd", DbType.String, RCCLRepEmailAdd);

                db.AddInParameter(dbCommand, "@pCancellationTermsInt", DbType.Int32, iCancelationTerms);
                if (sCutoffTime != "")
                {
                    db.AddInParameter(dbCommand, "@pCutOffTime", DbType.DateTime, sCutoffTime);
                }
                db.AddInParameter(dbCommand, "@pHotelTimeZoneID", DbType.String, HotelTimeZoneID);
                
                db.AddInParameter(dbCommand, "@pCurrencyIDInt", DbType.Int32, CurrencyIDInt);
                db.AddInParameter(dbCommand, "@pRoomAmountSglFloat", DbType.Double, RoomRateSgl);
                db.AddInParameter(dbCommand, "@pRoomAmountDblFloat", DbType.Double, RoomRateDbl);


                db.AddOutParameter(dbCommand, "@pID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                //trans.Commit();
                Int32 pID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pID"));
                return pID;
                //

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //do not close connection yet, saving for contract attachment to follow
                //if (connection != null)
                //{
                //    connection.Close();
                //    connection.Dispose();
                //}
                //if (dbCommand != null)
                //{
                //    dbCommand.Dispose();
                //}
            }
        }





        /// <summary>
        /// Create by:      Jefferson Bermundo
        /// Date Created:   07/31/2012
        /// Description:    Add/Insert hotel contract attachments.
        ///                 Disposing of the current transaction is being done in this section.
        /// </summary>
        public static void AddSaveHotelContractAttachments(int contractId, string fileName, string fileType, byte[] uploadedFile,
                                                            DateTime dateUploaded, object sqlTransaction, bool LastAttachment)
        {
            DbTransaction sqlTrans = (DbTransaction)sqlTransaction;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            
            try
            {
                if (contractId != 0)
                {
                    dbCommand = db.GetStoredProcCommand("uspInsertContractHotelAttachment");

                    db.AddInParameter(dbCommand, "@pcolContractId", DbType.Int32, contractId);
                    db.AddInParameter(dbCommand, "@pcolFileName", DbType.String, fileName);
                    db.AddInParameter(dbCommand, "@pcolFileType", DbType.String, fileType);
                    db.AddInParameter(dbCommand, "@pcolAttachment", DbType.Binary, uploadedFile);
                    db.AddInParameter(dbCommand, "@pcolUploadDate", DbType.String, dateUploaded);

                    db.ExecuteNonQuery(dbCommand, sqlTrans);
                }
                if (LastAttachment)
                    sqlTrans.Commit();
            }
            catch (Exception ex)
            {
                sqlTrans.Rollback();
                throw ex;
            }
            finally
            {
                // do not dispose if the all attachment is not yet saved
                if (LastAttachment)
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
                    if (sqlTrans != null)
                    {
                        sqlTrans.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Date created:   07/31/2012
        /// Created by:     Jefferson Bermundo
        /// Description:    Get the list of the hotel Attachments.
        /// </summary>
        /// <param name="contractId">selected contract id</param>
        /// <returns>return model list for contract hotel attachments</returns>
        public static List<ContractHotelAttachment> GetHotelContractAttachment(int contractId)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            List<ContractHotelAttachment> contractHotelAttachment = new List<ContractHotelAttachment>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelContractAttachments");
                SFDatebase.AddInParameter(SFDbCommand, "@pColContractId", DbType.Int32, contractId);

                SFDbCommand.CommandTimeout = 0;
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                contractHotelAttachment = (from list in SFDataTable.AsEnumerable()
                                           select new ContractHotelAttachment {
                                               FileName = list.Field<string>("colFileNameVarChar"),
                                               FileType = list.Field<string>("colFileTypeVarChar"),
                                               UploadedDate = list.Field<DateTime>("colUploadedDate"),
                                               uploadedFile = list.Field<byte[]>("colContractAttachmentVarBinary"),
                                               AttachmentId = list.Field<int>("colContractAttachmentIdInt")
                                           }).ToList();
                return contractHotelAttachment;
                
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
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Update contract hotel status
        /// </summary>     
        public static void UpdateContractStatus(Int32 ContractID, string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVendorHotelBranchContractStatus");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);
                SFDbCommand.CommandTimeout = 1800;

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Insert attach contract
        /// </summary>     
        public static void InsertAttachHotelContract(string filename, Byte[]  contract, Int32 length, string mime)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertContractAttachment");
                SFDatebase.AddInParameter(SFDbCommand, "@filename", DbType.String, filename);
                SFDatebase.AddInParameter(SFDbCommand, "@contract", DbType.Binary, contract);
                SFDatebase.AddInParameter(SFDbCommand, "@length", DbType.Int32, length);
                SFDatebase.AddInParameter(SFDbCommand, "@mime", DbType.String, mime);
                SFDatebase.AddInParameter(SFDbCommand, "@contractpdf", DbType.Binary, contract);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 14/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update contract vehicle status
        /// --------------------------------------------
        /// Date Modified:  07/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add audit trail
        /// </summary>     
        public static void UpdateVehicleContractStatus(Int32 ContractID, string Username,
             string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try 
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVendorVehicleBranchContractStatus");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserId", DbType.String, Username);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);
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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {                    
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Update contract hotel status
        /// </summary>     
        public static void UpdateContractFlag(Int32 ContractID, string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;


            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVendorHotelBranchContractFlag");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);
                SFDbCommand.CommandTimeout = 1800;

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 14/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update contract vehicle status
        /// ------------------------------------------------
        /// Date Modified:  26/Sep/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add audit trail fields
        /// </summary>     
        public static void UpdateVehicleContractFlag(Int32 ContractID, string Username,
             string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateVendorVehicleBranchContractFlag");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 17/08/2011
        /// Created By: Ryan Bautista
        /// (description) Add/save vendor contract detail
        /// </summary>   
        public static Int32 AddSaveHotelDetailContract(Int32 pID, string Desc,
                                                string UserName, DataTable dt)
        {
            string ConnStr = ConnStr = ConfigurationManager.ConnectionStrings["TRAVELMARTConnectionString"].ToString();
            SqlConnection sqlConn = new SqlConnection(ConnStr);
            sqlConn.Open();
            SqlCommand sqlCmd = new SqlCommand("uspAddSaveHotelDetailContract", sqlConn);
            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SqlParameter param1 = new SqlParameter("@pcolCreatedByVarchar", UserName);
                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.VarChar;
                SqlParameter param2 = new SqlParameter("@pcolContractIdInt", pID);
                param2.Direction = ParameterDirection.Input;
                param2.SqlDbType = SqlDbType.Int;
                SqlParameter param3 = new SqlParameter("@pcolDescriptionVarchar", Desc);
                param3.Direction = ParameterDirection.Input;
                param3.SqlDbType = SqlDbType.VarChar;

                SqlParameter[] ParamArray = { param, param1, param2, param3 };
                sqlCmd.Parameters.AddRange(ParamArray);
                sqlCmd.ExecuteNonQuery();


                //Int32 ContractDetailID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pContractDetailIdInt"));
                return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConn != null)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
                if (sqlCmd != null)
                {
                    sqlCmd.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }        
        
        /// <summary>            
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Add/save vendor contract detail
        /// </summary>     
        public static void AddSaveVehicleDetailContract(Int32 pID, int Type, string Capacity, string Origin, string Destination, Int32 Currency, string Rate,
                                                        int LVType, string LVBags, string LVOrigin, string LVDestination, Int32 LVCurrency, string LVRate,
                                                        Int32 SRSeamansVisaCurrencyID, string SRSeamansVisaCurrencyRate, Int32 SRBaggageTraceCurrencyID, string SRBaggageTraceCurrencyRate,
                                                        Int32 SRAgencyFeesCurrencyID, string SRAgencyFeesCurrencyRate, Int32 SROkToBoardCurrencyID, string SROkToBoardCurrencyRate, string User)                                                       
        {            
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspAddSaveVehicleDetailContract");

                db.AddInParameter(dbCommand, "@pcolContractIdInt", DbType.Int32, pID);
                db.AddInParameter(dbCommand, "@pcolVehicleIDInt", DbType.Int32, Type);
                db.AddInParameter(dbCommand, "@pCapacity", DbType.String, Capacity);
                db.AddInParameter(dbCommand, "@pOrigin", DbType.String, Origin);
                db.AddInParameter(dbCommand, "@pDestination", DbType.String, Destination);                
                db.AddInParameter(dbCommand, "@pCurrency", DbType.Int32, Currency);
                db.AddInParameter(dbCommand, "@pRate", DbType.String, Rate);
                db.AddInParameter(dbCommand, "@pcolVehicleLVIDInt", DbType.Int32, LVType);
                db.AddInParameter(dbCommand, "@pLVBags", DbType.String, LVBags);
                db.AddInParameter(dbCommand, "@pLVOrigin", DbType.String, LVOrigin);
                db.AddInParameter(dbCommand, "@pLVDestination", DbType.String, LVDestination);
                db.AddInParameter(dbCommand, "@pLVCurrency", DbType.Int32, LVCurrency);
                db.AddInParameter(dbCommand, "@pLVRate", DbType.String, LVRate);
                db.AddInParameter(dbCommand, "@pSRSeamansVisaCurrencyID", DbType.Int32, SRSeamansVisaCurrencyID);
                db.AddInParameter(dbCommand, "@pSRSeamansVisaCurrencyRate", DbType.String, SRSeamansVisaCurrencyRate);
                db.AddInParameter(dbCommand, "@pSRBaggageTraceCurrencyID", DbType.Int32, SRBaggageTraceCurrencyID);
                db.AddInParameter(dbCommand, "@pSRBaggageTraceCurrencyRate", DbType.String, SRBaggageTraceCurrencyRate);
                db.AddInParameter(dbCommand, "@pSRAgencyFeesCurrencyID", DbType.Int32, SRAgencyFeesCurrencyID);
                db.AddInParameter(dbCommand, "@pSRAgencyFeesCurrencyRate", DbType.String, SRAgencyFeesCurrencyRate);
                db.AddInParameter(dbCommand, "@pSROkToBoardCurrencyID", DbType.Int32, SROkToBoardCurrencyID);
                db.AddInParameter(dbCommand, "@pSROkToBoardCurrencyRate", DbType.String, SROkToBoardCurrencyRate);
                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, User);

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
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor vehicle contract list
        /// </summary>   
        public static DataTable vendorVehicleGetContractList(String strVehicleName, String strUserName)
        {                    
            Database vDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand vDbCommand = null;
            DataTable vDataTable = null;
            try
            {
                vDbCommand = vDatebase.GetStoredProcCommand("uspGetVendorVehicleContractList");
                vDatebase.AddInParameter(vDbCommand, "@pVehicleName", DbType.String, strVehicleName);
                vDatebase.AddInParameter(vDbCommand, "@pUserName", DbType.String, strUserName);
                vDataTable = vDatebase.ExecuteDataSet(vDbCommand).Tables[0];
                return vDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (vDbCommand != null)
                {
                    vDbCommand.Dispose();
                }
                if (vDataTable != null)
                {
                    vDataTable.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 29/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch contract details by branch ID
        /// ----------------------------------------------------------------------        
        /// Date Modified:  08/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void and put it in List
        /// ----------------------------------------------------------------------        
        /// Date Modified:  11/Jul/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add sUserID parameter, add field CssContractAmendVisible
        /// </summary>     
        public static List<ContractVehicle> GetVendorVehicleBranchContractByBranchID(string VehicleBranchID, string sUserID)
        {
            List<ContractVehicle> contractList = new List<ContractVehicle>();
            
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorVehicleBranchContractByBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolVendorBranchIdInt", DbType.Int32, Convert.ToInt32(VehicleBranchID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, sUserID);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                contractList = (from a in SFDataTable.AsEnumerable()
                                select new ContractVehicle {
                                    ContractID = GlobalCode.Field2Int(a["cId"]),
                                    VehicleName = GlobalCode.Field2String(a["BranchName"]),
                                    ContractStatus = GlobalCode.Field2String(a["CONTRACTSTATUS"]),
                                    ContractName = GlobalCode.Field2String(a["CONTRACTNAME"]),
                                    ContractDateStart = GlobalCode.Field2String(a["CONTRACTSTARTDATE"]),
                                    ContractDateEnd = GlobalCode.Field2String(a["CONTRACTENDDATE"]),
                                    Remarks = GlobalCode.Field2String(a["REMARKS"]),
                                    BranchID = GlobalCode.Field2Int(a["BRANCHID"]),
                                    IsActive = GlobalCode.Field2Bool(a["colIsActiveBit"]),
                                    IsCurrent = GlobalCode.Field2Bool(a["IsCurrent"]),
                                    DateCreated = a.Field<DateTime>("DateCreated"),
                                    CssContractAmendVisible = a.Field<string>("CssContractAmendVisible")
                                }).ToList();

               return contractList;
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
            }
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor vehicle contract detail
        /// </summary>  
        public static DataTable vendorVehicleGetContractDetail(String vendorId, String branchId)
        {                     
            Database vDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand vDbCommand = null;
            DataTable vDataTable = null;
            try
            {
                vDbCommand = vDatebase.GetStoredProcCommand("uspGetVendorVehicleContract");
                vDatebase.AddInParameter(vDbCommand, "@pcolVendorIdInt", DbType.String, vendorId);
                vDatebase.AddInParameter(vDbCommand, "@pcolBranchIdInt", DbType.String, branchId);
                vDataTable = vDatebase.ExecuteDataSet(vDbCommand).Tables[0];
                return vDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (vDbCommand != null)
                {
                    vDbCommand.Dispose();
                }
                if (vDataTable != null)
                {
                    vDataTable.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vendor vehicle contract            
        /// </summary> 
        //public static Int32 vehicleInsertContract(Int32 vendorId, Int32 branchID, String contractName, String contractStartDate, String contractEndDate,
        //                                         String strRemarks, String strUserName, string Filename, string FileType, byte[] FileData,
        //                                         DateTime DateUploaded)
        //{
        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vendor vehicle contract    
        /// --------------------------------------------------
        /// Date Modified:  15/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add dtContractAirport, dtContractSeaport, dtContractVehicleType
        ///                 Include Audit Trail
        /// --------------------------------------------------
        /// Date Modified:  17/July/2014
        /// Modified By:    Josephine Monteza
        /// (description)   Add  bool bIsRCL, bool bIsAZA, bool bIsCEL, bool bIsPUL, 
        /// --------------------------------------------------
        /// </summary>         
        public static void vehicleInsertContract(int iContractID, int iVehicleVendorID,
            string sContractName, string sRemarks, string sDateStart,
            string sDateEnd, string sRCCLPerconnel, string sRCCLDateAccepted,
            string sVendorPersonnel, string sVendorDateAccepted, int iCurrency,
            bool bIsAirportToHotel, bool bIsHotelToShip, 
            string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, bool bIsRCL, bool bIsAZA, bool bIsCEL, bool bIsPUL, bool bIsSKS,
            DataTable dtContractAirport, DataTable dtContractSeaport, DataTable dtContractVehicleType,
            DataTable dtAttachment, DataTable dtDetails
            )
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                
                dbCommand = db.GetStoredProcCommand("uspInsertVehicleContract");

                db.AddInParameter(dbCommand, "@pContractIdInt", DbType.Int32, iContractID);
                db.AddInParameter(dbCommand, "@pVehicleVendorIDInt", DbType.Int32, iVehicleVendorID);                
                db.AddInParameter(dbCommand, "@pContractNameVarchar", DbType.String, sContractName);
                db.AddInParameter(dbCommand, "@pRemarksVarchar", DbType.String, sRemarks);

                if (sDateStart != "")
                {
                    db.AddInParameter(dbCommand, "@pContractDateStartedDate", DbType.Date, GlobalCode.Field2Date(sDateStart));
                }
                if (sDateEnd != "")
                {
                    db.AddInParameter(dbCommand, "@pContractDateEndDate", DbType.Date, GlobalCode.Field2Date(sDateEnd));
                }

                db.AddInParameter(dbCommand, "@pRCCLPersonnel", DbType.String, sRCCLPerconnel);

                if (sRCCLDateAccepted != "")
                {
                    db.AddInParameter(dbCommand, "@pRCCLAcceptedDate", DbType.Date, sRCCLDateAccepted);
                }
                db.AddInParameter(dbCommand, "@pVendorPersonnel", DbType.String, sVendorPersonnel);
                if (sVendorDateAccepted != "")
                {
                    db.AddInParameter(dbCommand, "@pVendorAcceptedDate", DbType.Date, sVendorDateAccepted);
                }
                db.AddInParameter(dbCommand, "@pCurrencyIDInt", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pIsAutoAirportToHotel", DbType.Boolean, bIsAirportToHotel);
                db.AddInParameter(dbCommand, "@pIsAutoHotelToShip", DbType.Boolean, bIsHotelToShip);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFilename", DbType.String, sFilename);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);

                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GlobalCode.Field2Date(sGMTDate));
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);

                db.AddInParameter(dbCommand, "@pIsRCI", DbType.Boolean, bIsRCL);
                db.AddInParameter(dbCommand, "@pIsAZA", DbType.Boolean, bIsAZA);
                db.AddInParameter(dbCommand, "@pIsCEL", DbType.Boolean, bIsCEL);
                db.AddInParameter(dbCommand, "@pIsPUL", DbType.Boolean, bIsPUL);
                db.AddInParameter(dbCommand, "@pIsSKS", DbType.Boolean, bIsSKS);


                //db.AddInParameter(dbCommand, "@pFileData", DbType.Binary, FileData);
                //db.AddInParameter(dbCommand, "@pDateUploaded", DbType.DateTime, DateUploaded);
                //db.AddOutParameter(dbCommand, "@pID", DbType.Int32, 8);


                SqlParameter param = new SqlParameter("@pTblContractVehicleAirport", dtContractAirport);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;                
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractVehicleSeaport", dtContractSeaport);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractVehicleType", dtContractVehicleType);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractVehicleAttachment", dtAttachment);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblTempContractVehicleDetail", dtDetails);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                //Int32 pID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pID"));
                //return pID;
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
                if (dtContractAirport != null)
                {
                    dtContractAirport.Dispose();
                }
                if (dtContractSeaport != null)
                {
                    dtContractSeaport.Dispose();
                }
                if (dtContractVehicleType != null)
                {
                    dtContractVehicleType.Dispose();
                }
                if (dtAttachment != null)
                {
                    dtAttachment.Dispose();
                }
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update vendor vehicle contract
        /// -----------------------------------------------
        /// Date Modified:  23/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Remove parameter contractTotalCost, Close dbCommand
        /// </summary>  
        public static void vehicleUpdateContract(Int32 contractId, Int32 contractDetailId, Int32 vendorId, String contractName, DateTime contractStartDate, DateTime contractEndDate,
                                                  String contractRate, String contractTax, String strRemarks, String strDescription,
                                                  Int32 vehicleType, DateTime strStartDate, DateTime strEndDate, String strUserName)
        {              
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateVehicleContract");

                db.AddInParameter(dbCommand, "@pcolContractIdInt", DbType.Int32, contractId);
                db.AddInParameter(dbCommand, "@pcolContractDetailIdInt", DbType.Int32, contractDetailId);
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.Int32, vendorId);                
                db.AddInParameter(dbCommand, "@pcolContractNameVarchar", DbType.String, contractName);
                db.AddInParameter(dbCommand, "@pcolContractDateStartedDate", DbType.Date, contractStartDate);
                db.AddInParameter(dbCommand, "@pcolContractDateEndDate", DbType.Date, contractEndDate);
                db.AddInParameter(dbCommand, "@pcolContractRateMoney", DbType.String, contractRate);
                db.AddInParameter(dbCommand, "@pcolContractTaxDecimal", DbType.String, contractTax);                
                db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, strRemarks);
                db.AddInParameter(dbCommand, "@pcolDescriptionVarchar", DbType.String, strDescription);
                db.AddInParameter(dbCommand, "@pcolVehicleTypeIdInt", DbType.Int32, vehicleType); 
                db.AddInParameter(dbCommand, "@pcolStartDate", DbType.Date, strStartDate);
                db.AddInParameter(dbCommand, "@pcolEndDate", DbType.Date, strEndDate);
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, strUserName);

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
        ///// <summary>            
        ///// Date Created: 23/08/2011
        ///// Created By: Marco Abejar
        ///// (description) Selecting list of port contract details 
        ///// </summary>  
        //public static DataTable GetPortContractList(string PortCompany, string UserName)
        //{                      
        //    Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;
        //    DataTable SFDataTable = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortContractList");
        //        SFDatebase.AddInParameter(SFDbCommand, "@pcolCompanyNameVarchar", DbType.String, PortCompany);
        //        SFDatebase.AddInParameter(SFDbCommand, "@pUserName", DbType.String, UserName);
        //        SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
        //    }
        //}
        /// <summary>            
        /// Date Created: 17/08/2011
        /// Created By: Marco Abejar
        /// (description) Add/save port contract
        /// </summary> 
        public static void AddSavePortContract(Int32 PortContractID, Int32 PortCompanyID, 
            string vContract, string Remarks, string dtStart, string dtEnd, string RateperHead,
            string TaxRate, string Currency, bool TaxInclusive, string RCCLRep, string vRep,
            Int32 NumberOfSeafarer, Int32 PortAgentId, string userId,
            string fileName, string fileType, byte[] imageBytes, bool attachChanged, Int32 CountryId)
        {               
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspAddSavePortContract");

                db.AddInParameter(dbCommand, "@pcolContractIdInt", DbType.Int32, PortContractID);
                db.AddInParameter(dbCommand, "@pcolPortAgentCompanyIdInt", DbType.Int32, PortCompanyID);
                db.AddInParameter(dbCommand, "@pcolCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pcolContractNameVarchar", DbType.String, vContract);
                db.AddInParameter(dbCommand, "@pcolRemarksVarchar", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pcolContractDateStartedDate", DbType.DateTime, dtStart);
                db.AddInParameter(dbCommand, "@pcolContractDateEndDate", DbType.DateTime, dtEnd);
                db.AddInParameter(dbCommand, "@pcolContractRatePerHeadMoney", DbType.String, RateperHead);
                db.AddInParameter(dbCommand, "@pcolContractTaxDecimal", DbType.String, TaxRate);
                db.AddInParameter(dbCommand, "@pcolCurrencyIDInt", DbType.Int32, Currency);
                db.AddInParameter(dbCommand, "@pcolTaxInclusiveBit", DbType.Boolean, TaxInclusive);
                db.AddInParameter(dbCommand, "@pcolRCCLPersonnel", DbType.String, RCCLRep);
                db.AddInParameter(dbCommand, "@pcolVendorPersonnel", DbType.String, vRep);
                db.AddInParameter(dbCommand, "@pSeafarerCount", DbType.Int32, NumberOfSeafarer);
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, PortAgentId);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, userId);
                db.AddInParameter(dbCommand, "@pContractFileName", DbType.String, fileName);
                db.AddInParameter(dbCommand, "@pContractFileType", DbType.String, fileType);
                db.AddInParameter(dbCommand, "@pContractAttachment", DbType.Binary, imageBytes);
                db.AddInParameter(dbCommand, "@pAttachedChanged", DbType.Boolean, attachChanged);
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
        /// Date Created: 23/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of port contract details 
        /// </summary>            
        //public static DataTable GetPortContractDetails(Int32 PortContractID)
        //{
        //    Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;
        //    DataTable SFDataTable = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortContractDetails");
        //        SFDatebase.AddInParameter(SFDbCommand, "@colContractIdInt", DbType.Int32, PortContractID);
        //        SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
        //    }
        //}

        /// <summary>            
        /// Date Created: 23/08/2011
        /// Created By: Ryan bautista
        /// (description) Selecting contract attachment
        /// --------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader GetContractAttachment(Int32 ContractID, Int32 BranchID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectContractAttachment");
                SFDatebase.AddInParameter(SFDbCommand, "pContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolBranchIDInt", DbType.Int32, BranchID);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }

        /// <summary>            
        /// Date Created: 18/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting vehicle contract attachment
        /// -----------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader GetVehicleContractAttachment(Int32 ContractID, Int32 BranchID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader SFDataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleContractAttachment");
                SFDatebase.AddInParameter(SFDbCommand, "pContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "pcolBranchIDInt", DbType.Int32, BranchID);
                SFDataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return SFDataReader;
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
            }
        }
        /// <summary>
        /// Date Created:   16/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Hotel Contract Date by Date
        /// -----------------------------------------------------
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="BranchID"></param>
        /// <param name="dDate"></param>
        /// <param name="sRole"></param>
        /// <param name="sRoomType"></param>
        /// <returns></returns>
        public static DataTable GetHotelContractOverrideByDate(string IDBigint, string SeqNo, 
            string TransHotelID, string PendingHotelID, string sUser, string BranchID,
            DateTime dDateFrom, DateTime dDateTo, string sRole, string sRoomType, bool IsNew)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelContractOverrideByDate");
                if (IDBigint != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int32, GlobalCode.Field2Int(IDBigint));
                }
                if (SeqNo != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int16, GlobalCode.Field2TinyInt(SeqNo));
                }
                if (TransHotelID != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelIDBigInt", DbType.Int32, GlobalCode.Field2Int(TransHotelID));
                }
                if (PendingHotelID != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pPendingHotelIDBigInt", DbType.Int32, GlobalCode.Field2Int(PendingHotelID));
                }

                SFDatebase.AddInParameter(SFDbCommand, "@pUserName", DbType.String, sUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, GlobalCode.Field2Int(BranchID));
                SFDatebase.AddInParameter(SFDbCommand, "@pDateFrom", DbType.DateTime, dDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pDateTo", DbType.DateTime, dDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, sRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomType", DbType.Int16, GlobalCode.Field2TinyInt(sRoomType));
                SFDatebase.AddInParameter(SFDbCommand, "@pIsnew", DbType.Boolean, IsNew);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
            }
        }


        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel contract details 
        /// ===============================================================
        /// Date Modified:  15/Jun/2016
        /// Modified By:    Josephine Monteza
        /// (description)   Removed Currency ID from Contract details
        /// </summary> 
        public static DataTable GetVendorHotelContract(string cID, string BranchID, out DataTable dt2)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;

            List<ContractHotelDetails> listContractHotelDetails = new List<ContractHotelDetails>();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorHotelContractForSaving");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractIdInt", DbType.Int32, cID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolBranchID", DbType.Int32, BranchID);
                DataSet ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                SFDataTable = ds.Tables[0];
                dt2 = ds.Tables[1];

                listContractHotelDetails = (from a in dt2.AsEnumerable()
                                            select new ContractHotelDetails
                                            { 
                                                RowNumber = GlobalCode.Field2Int(a["RowNumber"]),
                                                DetailId = GlobalCode.Field2Int(a["DetailId"]),
                                                DetailId2 = GlobalCode.Field2Int(a["DetailId2"]),
                                                DateFrom = GlobalCode.Field2DateTime(a["DateFrom"]),
                                                DateTo = GlobalCode.Field2DateTime(a["DateTo"]),
                                                Sun = GlobalCode.Field2Int(a["Sun"]),
                                                Mon = GlobalCode.Field2Int(a["Mon"]),
                                                Tue = GlobalCode.Field2Int(a["Tue"]),
                                                Wed = GlobalCode.Field2Int(a["Wed"]),
                                                Thu = GlobalCode.Field2Int(a["Thu"]),
                                                Fri = GlobalCode.Field2Int(a["Fri"]),
                                                Sat = GlobalCode.Field2Int(a["Sat"]),

                                                Sun2 = GlobalCode.Field2Int(a["Sun2"]),
                                                Mon2 = GlobalCode.Field2Int(a["Mon2"]),
                                                Tue2 = GlobalCode.Field2Int(a["Tue2"]),
                                                Wed2 = GlobalCode.Field2Int(a["Wed2"]),
                                                Thu2 = GlobalCode.Field2Int(a["Thu2"]),
                                                Fri2 = GlobalCode.Field2Int(a["Fri2"]),
                                                Sat2 = GlobalCode.Field2Int(a["Sat2"]),

                                                SingleRate = GlobalCode.Field2Decimal(a["SingleRate"]),
                                                DoubleRate = GlobalCode.Field2Decimal(a["DoubleRate"]),
                                                Tax = GlobalCode.Field2Decimal(a["Tax"]),
                                                TaxInclusive = GlobalCode.Field2Bool(a["TaxInclusive"]),
                                                //Currency = a.Field<string>("Currency"),
                                                //CurrencyId = GlobalCode.Field2Int(a["CurrencyId"]),
                                            }).ToList();

                HttpContext.Current.Session["ContractAdd_HotelDetails"] = listContractHotelDetails;

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
            }
        }


        #region PORT CONTRACT
        /// <summary>            
        /// Date Created: 19/10/2011
        /// Created By: Charlene Remotigue
        /// (description) load Service Provider contract list
        /// </summary>     
        public DataTable GetPortContractList(string portAgentId)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortContractListbyPortAgent");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentId", DbType.Int32, Convert.ToInt32(portAgentId));
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 19/10/2011
        /// Created By: Charlene Remotigue
        /// (description) get port contract list count
        /// </summary>     
        public Int32 GetPortContractListCount(string portAgentId)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            int portContracts = 0;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortContractListbyPortAgentCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentId", DbType.Int32, Convert.ToInt32(portAgentId));

                IDataReader dr = SFDatebase.ExecuteReader(SFDbCommand);
                if (dr.Read())
                {
                    portContracts = Convert.ToInt32(dr["maximumRows"]);
                }
                return portContracts;
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

            }
        }

                /// <summary>
        /// Date Created:20/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Port Contract
        /// </summary>
        public void PortAgentContractListViewDelete(int ContractId, string UserId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeletePortContract");
                db.AddInParameter(dbCommand, "@pcolContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pcolModifiedByVarchar", DbType.String, UserId);
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
        ///// Date Created: 24/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: count Pending Service Provider Contracts 
        ///// </summary>
        //public Int32 PortAgentContractApprovalListCount()
        //{
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    int PendingContracts = 0;

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspSelectPendingPortAgentContractListCount");
        //        IDataReader dr = db.ExecuteReader(dbCommand);
        //        if (dr.Read())
        //        {
        //            PendingContracts = Convert.ToInt32(dr["maximumRows"]);
        //        }
        //        return PendingContracts;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Date Created:24/11/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Service Provider pending contract        
        /// ----------------------------------------------
        /// Date Moodified: 18/Feb/2014
        /// Modified By:    Josephine Gad
        /// Description:    Add count in Session, Use DataSet variable, add sPortName
        /// </summary>
        /// 
        public DataTable PortAgentContractApprovalList(string SortParam, Int32 startRowIndex, Int32 maximumRows, string sPortName)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable CountryDataTable = null;
            DataSet ds = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectPendingPortAgentContractList");
                SFDatebase.AddInParameter(SFDbCommand, "@pSortParameter", DbType.String, SortParam);
                SFDatebase.AddInParameter(SFDbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                SFDatebase.AddInParameter(SFDbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                if (sPortName != null)
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pPortName", DbType.String, sPortName);
                }
                else
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pPortName", DbType.String, "");
                }
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                
                int iTotal = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                HttpContext.Current.Session["PortAgentApproval_Count"] = iTotal;

                CountryDataTable = ds.Tables[1];                
                return CountryDataTable;
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
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        ///// <summary>
        ///// Date Created:24/11/2011
        ///// Created By: Charlene Remotigue
        ///// Description: Approve pending Service Provider contract       
        ///// </summary>
        ///// 
        //public Int32 ApprovePortAgentContract(Int32 returnVal, Int32 ContractId)
        //{
        //    Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspApprovePendingPortAgentContract");
        //        SFDatebase.AddInParameter(SFDbCommand, "@pContractId", DbType.Int32, ContractId);
        //        SFDatebase.AddOutParameter(SFDbCommand, "@pRetValue", DbType.Int32, returnVal);
        //        SFDatebase.ExecuteNonQuery(SFDbCommand);
        //        returnVal = Int32.Parse(SFDbCommand.Parameters["@pRetValue"].Value.ToString());
        //        return returnVal;
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

        //    }
        //}

       #endregion

        #region PORT CONTRACT HOTEL
       
       /// <summary>
       /// Author: Charlene Remotigue
       /// Date Created: 03/11/2011
        /// Description: Save Service Provider contract hotel
       /// --------------------------------------------
       /// Modified By: Charlene Remotigue
       /// Date Modified: 08/12/2011
       /// Description: chenge stored procedure, removed other parameters
       /// </summary>
       /// <param name="contractBranchId"></param>
       /// <param name="BrandId"></param>
       /// <param name="BranchId"></param>
       /// <param name="PortId"></param>
       /// <param name="MealRate"></param>
       /// <param name="MealRateTax"></param>
       /// <param name="MealRateTaxInc"></param>
       /// <param name="withBreakfast"></param>
       /// <param name="withLunch"></param>
       /// <param name="withDinner"></param>
       /// <param name="withLunchOrDinner"></param>
       /// <param name="withShuttle"></param>
       /// <param name="UserId"></param>
       /// <param name="contractId"></param>
        public static Int32 SaveContractPortAgentVendorHotel(string contractBranchId, string BrandId, string BranchId,
            string PortId, string MealRate, string MealRateTax, bool MealRateTaxInc, bool withBreakfast,
            bool withLunch, bool withDinner, bool withLunchOrDinner, bool withShuttle, string UserId, string contractId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveContractPortAgentHotel");
                db.AddInParameter(dbCommand, "@pContractBranchId", DbType.Int32, Int32.Parse(contractBranchId));
                db.AddInParameter(dbCommand, "@pBrandIdInt", DbType.Int32, Int32.Parse(BrandId));
                db.AddInParameter(dbCommand, "@pBranchIdInt", DbType.Int32, Int32.Parse(BranchId));
                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                db.AddInParameter(dbCommand, "@pMealRate", DbType.String, MealRate);
                db.AddInParameter(dbCommand, "@pMealRateTax", DbType.String, MealRateTax);
                db.AddInParameter(dbCommand, "@pMealRateTaxInclusive", DbType.Boolean, MealRateTaxInc);
                db.AddInParameter(dbCommand, "@pBreakfastBit", DbType.Boolean, withBreakfast);
                db.AddInParameter(dbCommand, "@pLunchBit", DbType.Boolean, withLunch);
                db.AddInParameter(dbCommand, "@pDinnerBit", DbType.Boolean, withDinner);
                db.AddInParameter(dbCommand, "@pLunchOrDinnerBit", DbType.Boolean, withLunchOrDinner);
                db.AddInParameter(dbCommand, "@pWithShuttle", DbType.Boolean, withShuttle);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.String, contractId);
                db.AddOutParameter(dbCommand, "@pID", DbType.Int32, 100);
                db.ExecuteNonQuery(dbCommand, dbTrans);
                dbTrans.Commit();

                Int32 pID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pID"));
                return pID;
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTrans != null)
                {
                    dbTrans.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/12/2011
        /// Description: save Service Provider contract hotel rooms
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        /// <param name="ContractBranchId"></param>
        /// <param name="contractId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="roomRate"></param>
        /// <param name="currencyId"></param>
        /// <param name="roomTax"></param>
        /// <param name="taxinclusive"></param>
        /// <param name="roomType"></param>
        /// <param name="Mon"></param>
        /// <param name="Tue"></param>
        /// <param name="Wed"></param>
        /// <param name="Thu"></param>
        /// <param name="Fri"></param>
        /// <param name="Sat"></param>
        /// <param name="Sun"></param>
        public static void SaveContractPortAgentHotelRooms(Int32 ContractBranchId, Int32 contractId,
            DateTime dateFrom, DateTime dateTo, String roomRate, Int32 currencyId,
            Decimal roomTax, Boolean taxinclusive, Int32 roomType, Int32 Mon,
            Int32 Tue, Int32 Wed, Int32 Thu, Int32 Fri, Int32 Sat, Int32 Sun)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveContractPortAgentHotelRooms");
                db.AddInParameter(dbCommand, "@pContractBranchId", DbType.Int32, ContractBranchId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, contractId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, dateFrom);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, dateTo);
                db.AddInParameter(dbCommand, "@pRatePerDay", DbType.String, roomRate);
                db.AddInParameter(dbCommand, "@pCurrencyId", DbType.Int32, currencyId);
                db.AddInParameter(dbCommand, "@pTaxRate", DbType.Decimal, roomTax);
                db.AddInParameter(dbCommand, "@pTaxInclusive", DbType.Boolean, taxinclusive);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, roomType);
                db.AddInParameter(dbCommand, "@pMon", DbType.Int32, Mon);
                db.AddInParameter(dbCommand, "@pTue", DbType.Int32, Tue);
                db.AddInParameter(dbCommand, "@pWed", DbType.Int32, Wed);
                db.AddInParameter(dbCommand, "@pThu", DbType.Int32, Thu);
                db.AddInParameter(dbCommand, "@pFri", DbType.Int32, Fri);
                db.AddInParameter(dbCommand, "@pSat", DbType.Int32, Sat);
                db.AddInParameter(dbCommand, "@pSun", DbType.Int32, Sun);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, GlobalCode.Field2String(HttpContext.Current.Session["UserName"]));
                db.ExecuteNonQuery(dbCommand, dbTransaction);
                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   18/Feb/2014
        /// Description:    Approve Service Provider and Add audit trail
        /// -------------------------------------------
        /// </summary>
        public static void UpdatePortAgentContractStatus(Int32 ContractID, string Username,
            string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspApprovePendingPortAgentContract");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserId", DbType.String, Username);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);
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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        #endregion

        #region PORT CONTRACT VEHICLE
        
        /// <summary>
        /// Author: Charlene Remotgiue
        /// Date Created: 10/11/2011
        /// Description: save Service Provider contract vehicle specifications
        /// </summary>
        /// <param name="contractPortServiceId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="Currency"></param>
        /// <param name="serviceRate"></param>
        /// <param name="Origin"></param>
        /// <param name="Destination"></param>
        /// <param name="VehicleType"></param>
        /// <param name="Capacity"></param>
        /// <param name="UserID"></param>
        public static void SaveContractPortAgentVehicleSpecifications(string contractPortServiceId, string dateFrom, string dateTo,
            string Currency, string serviceRate, string Origin, string Destination, string VehicleType, string Capacity, string UserID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSavePortAgentContractSpecifications");
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(contractPortServiceId));
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.DateTime, DateTime.Parse(dateFrom));
                db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, DateTime.Parse(dateFrom));
                db.AddInParameter(dbCommand, "@pCurrency", DbType.Int32, Int32.Parse(Currency));
                db.AddInParameter(dbCommand, "@pServiceRate", DbType.String, serviceRate);
                db.AddInParameter(dbCommand, "@pOrigin", DbType.String, Origin);
                db.AddInParameter(dbCommand, "@pDestination", DbType.String, Destination);
                db.AddInParameter(dbCommand, "@pVehicleType", DbType.Int32, Int32.Parse(VehicleType));
                db.AddInParameter(dbCommand, "@pVehicleCapacity", DbType.Int32, Int32.Parse(Capacity));
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID);
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
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTrans != null)
                {
                    dbTrans.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: save Service Provider contract vehicle service
        /// </summary>
        /// <param name="contractPortAgentServiceId"></param>
        /// <param name="BrandId"></param>
        /// <param name="BranchId"></param>
        /// <param name="PortId"></param>
        /// <param name="VendorType"></param>
        /// <param name="userId"></param>
        public static void SaveContractPortAgentVendorVehicle(string contractPortAgentServiceId, string BrandId, string BranchId,
           string PortId, string VendorType, string userId, string ContractId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveContractPortAgentVendor");
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(contractPortAgentServiceId));
                db.AddInParameter(dbCommand, "@pBrandIdInt", DbType.Int32, Int32.Parse(BrandId));
                db.AddInParameter(dbCommand, "@pBranchIdInt", DbType.Int32, Int32.Parse(BranchId));
                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                db.AddInParameter(dbCommand, "@pVendorTypeInt", DbType.Int32, Int32.Parse(VendorType));
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, userId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, Int32.Parse(ContractId));
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
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTrans != null)
                {
                    dbTrans.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
              
        #endregion

        #region PORT CONTRACT MEDICAL
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: Save Service Provider contract medical details
        /// </summary>
        /// <param name="contractPortAgentServiceId"></param>
        /// <param name="PortId"></param>
        /// <param name="VendorType"></param>
        /// <param name="ServiceRate"></param>
        /// <param name="UserId"></param>
        /// <param name="contractId"></param>
        /// <param name="AccomodationDays"></param>
        public static void SaveContractPortAgentMedicalDetails(string contractPortAgentServiceId, 
            string PortId, string VendorType, string ServiceRate, string UserId, string contractId, 
            string AccomodationDays, string detailID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveContractPortAgentVendor");
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(contractPortAgentServiceId));
                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                db.AddInParameter(dbCommand, "@pVendorTypeInt", DbType.Int32, Int32.Parse(VendorType));
                db.AddInParameter(dbCommand, "@pMealRate", DbType.String, ServiceRate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.String, contractId);
                db.AddInParameter(dbCommand, "@pAccomodationDays", DbType.Int32, Int32.Parse(AccomodationDays));
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceDetailId", DbType.Int32, Int32.Parse(detailID));
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
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTrans != null)
                {
                    dbTrans.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: save Service Provider contract medical specifications
        /// </summary>
        /// <param name="contractPortServiceId"></param>
        /// <param name="Currency"></param>
        /// <param name="serviceRate"></param>
        /// <param name="Origin"></param>
        /// <param name="Destination"></param>
        /// <param name="UserID"></param>
        //public static void SaveContractPortAgentMedicalSpecifications(string contractPortServiceId, 
        //    string Currency, string serviceRate, string Origin, string Destination, string remarks, string UserID)
        //{
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    DbConnection dbConnection = db.CreateConnection();
        //    dbConnection.Open();
        //    DbTransaction dbTrans = dbConnection.BeginTransaction();

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspSavePortAgentContractSpecifications");
        //        db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(contractPortServiceId));
        //        db.AddInParameter(dbCommand, "@pCurrency", DbType.Int32, Int32.Parse(Currency));
        //        db.AddInParameter(dbCommand, "@pServiceRate", DbType.String, serviceRate);
        //        db.AddInParameter(dbCommand, "@pOrigin", DbType.String, Origin);
        //        db.AddInParameter(dbCommand, "@pDestination", DbType.String, Destination);
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserID );
        //        db.AddInParameter(dbCommand, "@pRemarks", DbType.String, remarks);
        //        db.ExecuteNonQuery(dbCommand, dbTrans);
        //        dbTrans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        dbTrans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbConnection != null)
        //        {
        //            dbConnection.Close();
        //            dbConnection.Dispose();
        //        }
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //        if (dbTrans != null)
        //        {
        //            dbTrans.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/11/2011
        /// Description: load Service Provider contract medical service
        /// --------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="ContractPortAgentVendorId"></param>
        /// <returns></returns>
        //public static IDataReader LoadPortAgentMedicalServices(string ContractPortAgentServiceId)
        //{
        //    IDataReader portAgentMedicalDataReader = null;
        //    DbCommand dbCommand = null;
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadPortContractMedical");
        //        db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(ContractPortAgentServiceId));
        //        portAgentMedicalDataReader = db.ExecuteReader(dbCommand);
        //        return portAgentMedicalDataReader;
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
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider contract medical specifications
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public static DataTable LoadPortAgentMedicalSpecifications(string serviceId, string userId)
        //{
        //    DataTable transferDataTable = null;
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadPortAgentContractTransfers");
        //        db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(serviceId));
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, userId);
        //        transferDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
        //        return transferDataTable;
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
        //        if (transferDataTable != null)
        //        {
        //            transferDataTable.Dispose();
        //        }
        //    }
        //}
        #endregion

        #region PORT CONTRACT SERVICES
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/11/2011
        /// Description: load all service types
        /// </summary>
        /// <returns></returns>
        //public static DataTable LoadPortAgentContractServiceTypes()
        //{
        //    DataTable ServiceTypeDatatable = null;
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspGetVendorTypes");
        //        ServiceTypeDatatable = db.ExecuteDataSet(dbCommand).Tables[0];
        //        return ServiceTypeDatatable;
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
        //        if (ServiceTypeDatatable != null)
        //        {
        //            ServiceTypeDatatable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/11/2011
        /// Description: load Service Provider contract services
        /// </summary>
        /// <param name="ContractId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //public static DataTable LoadPortContractServices(string ContractId, string UserId)
        //{
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    DataTable HotelDetailsDataTable;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadPortContractServices");
        //        db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, Int32.Parse(ContractId));
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        HotelDetailsDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
        //        return HotelDetailsDataTable;
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
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: delete port contract service
        /// </summary>
        /// <param name="portContractVendorId"></param>
        /// <param name="UserId"></param>
        //public static void DeletePortContractService(string portContractVendorId, string UserId)
        //{
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    DbConnection dbConnection = db.CreateConnection();
        //    dbConnection.Open();
        //    DbTransaction dbTrans = dbConnection.BeginTransaction();

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspDeletePortContractService");
        //        db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(portContractVendorId));
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.ExecuteNonQuery(dbCommand, dbTrans);
        //        dbTrans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        dbTrans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbConnection != null)
        //        {
        //            dbConnection.Close();
        //            dbConnection.Dispose();
        //        }
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //        if (dbTrans != null)
        //        {
        //            dbTrans.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: delete port contract specifications
        /// </summary>
        /// <param name="PortAgentSpecId"></param>
        /// <param name="userId"></param>
        //public static void DeletePortContractSpecifications(string PortAgentSpecId, string userId)
        //{
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;
        //    DbConnection dbConnection = db.CreateConnection();
        //    dbConnection.Open();
        //    DbTransaction dbTrans = dbConnection.BeginTransaction();

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspDeletePortContractSpecifications");
        //        db.AddInParameter(dbCommand, "@pContractPortAgentServiceSpecifications", DbType.Int32, Int32.Parse(PortAgentSpecId));
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, userId);
        //        db.ExecuteNonQuery(dbCommand, dbTrans);
        //        dbTrans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        dbTrans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dbConnection != null)
        //        {
        //            dbConnection.Close();
        //            dbConnection.Dispose();
        //        }
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //        if (dbTrans != null)
        //        {
        //            dbTrans.Dispose();
        //        }
        //    }
        //}
        #endregion

        #region Service Provider CONTRACT
        /// <summary>            
        /// Date Created: 20/10/2011
        /// Created By: Charlene Remotigue
        /// (description) load Service Provider contract details
        /// ---------------------------------------- 
        /// Date Modifed:   11/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Change  uspGetPortContractListbyContractId to uspPortAgentContractGetByContractID 
        ///                 Change funaction name from GetPortContractDetails to GetPortAgentContractByContractID
        /// ---------------------------------------- 
        /// Date Modifed:   28/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Add Transportation rate
        ///                 Add Hotel rate, Luggage rate
        /// </summary>     
        public static void GetPortAgentContractByContractID(string contractId, string branchId, Int16 iLoadType)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand com = null;
            DataSet ds = null;
            DataTable dt = null;
            DataTable dtAirContract = null;
            DataTable dtAirportNotInContract = null;

            DataTable dtSeaContract = null;
            DataTable dtSeaNotInContract = null;

            DataTable dtVehicleType = null;
            DataTable dtCapacity = null;

            //DataTable dtServices = null;
            DataTable dtAttachment = null;
            DataTable dtCurrency = null;
            DataTable dtRoute = null;
            DataTable dtDetails = null;
            DataTable dtDetailsHotel = null;
            DataTable dtDetailsLuggage = null;
            DataTable dtLuggageUOM = null;
            DataTable dtDetailsSafeguard = null;
            DataTable dtSafeguardUOM = null;
            DataTable dtMeetGreet = null;
            DataTable dtVisa = null;
            DataTable dtOther = null;

            List<ContractPortAgentDetails> list = new List<ContractPortAgentDetails>();

            List<Airport> listAirportContract = new List<Airport>();
            List<Airport> listAirportNotInContract = new List<Airport>();

            List<Seaport> listSeaportContract = new List<Seaport>();
            List<Seaport> listSeaportNotInContract = new List<Seaport>();

            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();

            List<VehicleRoute> listRoute = new List<VehicleRoute>();

            List<Currency> listCurrency = new List<Currency>();
            List<ContractPortAgentAttachment> listAttachment = new List<ContractPortAgentAttachment>();

            List<ContractPortAgentDetailsAmt> listDetails = new List<ContractPortAgentDetailsAmt>();
            List<ContractPortAgentDetailsAmtHotel> listDetailsHotel = new List<ContractPortAgentDetailsAmtHotel>();
            
            List<ContractPortAgentDetailsAmtLuggage> listDetailsLuggage = new List<ContractPortAgentDetailsAmtLuggage>();
            List<LuggageUOM> listLuggageUOM = new List<LuggageUOM>();

            List<ContractPortAgentDetailsAmtSafeguard> listDetailsSafeguard = new List<ContractPortAgentDetailsAmtSafeguard>();
            List<SafeguardUOM> listSafeguardUOM = new List<SafeguardUOM>();
            List<ContractPortAgentDetailsAmtMeetGreet> listMeetGreet = new List<ContractPortAgentDetailsAmtMeetGreet>();
            List<ContractPortAgentDetailsAmtVisa> listVisa = new List<ContractPortAgentDetailsAmtVisa>();
            List<ContractPortAgentDetailsAmtOther> listOther = new List<ContractPortAgentDetailsAmtOther>();
                        
            HttpContext.Current.Session["ContractPortAgentDetails"] = list;
            HttpContext.Current.Session["VendorAirportExists"] = listAirportContract;
            HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotInContract;
            HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
            HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;

            //HttpContext.Current.Session["PortAgentServices"] = listServices;
            //HttpContext.Current.Session["ContractPortAgentAttachment"] = listAttachment;
            //HttpContext.Current.Session["ContractPortAgentDetailsAmt"] = listDetails;

            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;

            HttpContext.Current.Session["ContractCurrency"] = listCurrency;
            HttpContext.Current.Session["ContractPortAgentAttachment"] = listAttachment;

            HttpContext.Current.Session["VehicleRoute"] = listRoute;
            HttpContext.Current.Session["ContractPortAgentDetailsAmt"] = listDetails;
            HttpContext.Current.Session["ContractPortAgentDetailsAmtHotel"] = listDetailsHotel;

            HttpContext.Current.Session["ContractPortAgentDetailsAmtLuggage"] = listDetailsLuggage;
            HttpContext.Current.Session["LuggageUOM"] = listLuggageUOM;

            HttpContext.Current.Session["ContractPortAgentDetailsAmtSafeguard"] = listDetailsSafeguard;
            HttpContext.Current.Session["SafeguardUOM"] = listSafeguardUOM;
            HttpContext.Current.Session["ContractPortAgentDetailsAmtMeetGreet"] = listMeetGreet;
            HttpContext.Current.Session["ContractPortAgentDetailsAmtVisa"] = listVisa;
            HttpContext.Current.Session["ContractPortAgentDetailsAmtOther"] = listOther;

            try
            {
                com = db.GetStoredProcCommand("uspPortAgentContractGetByContractID");
                db.AddInParameter(com, "@pContractIdInt", DbType.Int32, GlobalCode.Field2Int(contractId));
                db.AddInParameter(com, "@pPortAgentVendorIDInt", DbType.Int32, GlobalCode.Field2Int(branchId));
                db.AddInParameter(com, "@pLoadType", DbType.Int16, iLoadType);
                ds = db.ExecuteDataSet(com);

                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    list = (from a in dt.AsEnumerable()
                            select new ContractPortAgentDetails
                            {
                                ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                PortAgentID = GlobalCode.Field2Int(a["VendorID"]),

                                PortAgentName = a.Field<string>("VendorName"),
                                CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                CityName = a.Field<string>("City"),
                                CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                CountryName = a.Field<string>("Country"),

                                Address = a.Field<string>("colAddressVarchar"),
                                ContactNo = a.Field<string>("colContactNoVarchar"),
                                ContactPerson = a.Field<string>("colContactPersonVarchar"),

                                EmailCc = a.Field<string>("colEmailCcVarchar"),
                                EmailTo = a.Field<string>("colEmailToVarchar"),

                                FaxNo = a.Field<string>("colFaxNoVarchar"),

                                ContractDateStart = a.Field<DateTime?>("colContractDateStartedDate"),
                                ContractDateEnd = a.Field<DateTime?>("colContractDateEndDate"),
                                ContractName = a.Field<string>("colContractNameVarchar"),
                                ContractStatus = a.Field<string>("colContractStatusVarchar"),

                                CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                CurrencyName = GlobalCode.Field2String(a["CurrencyName"]),

                                RCCLAcceptedDate = a.Field<DateTime?>("colRCCLAcceptedDate"),
                                RCCLPersonnel = a.Field<string>("colRCCLPersonnel"),

                                VendorAcceptedDate = a.Field<DateTime?>("colVendorAcceptedDate"),
                                VendorPersonnel = a.Field<string>("colVendorPersonnel"),

                                Remarks = a.Field<string>("colRemarksVarchar"),
                                IsAirportToHotel = GlobalCode.Field2Bool(a["colIsAutoAirportToHotel"]),
                                IsHotelToShip = GlobalCode.Field2Bool(a["colIsAutoHotelToShip"]),
                                IsRCI = GlobalCode.Field2Bool(a["colIsRCI"]),
                                IsAZA = GlobalCode.Field2Bool(a["colIsAZA"]),
                                IsCEL = GlobalCode.Field2Bool(a["colIsCEL"]),
                                IsPUL = GlobalCode.Field2Bool(a["colIsPUL"]),
                                IsSKS = GlobalCode.Field2Bool(a["colIsSKS"]),
                            }).ToList();
                }

                if (iLoadType == 0)
                {
                    dtAirContract = ds.Tables[1];
                    dtAirportNotInContract = ds.Tables[2];

                    dtSeaContract = ds.Tables[3];
                    dtSeaNotInContract = ds.Tables[4];

                    dtVehicleType = ds.Tables[5];
                    dtCapacity = ds.Tables[6];
                    dtAttachment = ds.Tables[7];
                    dtCurrency = ds.Tables[8];
                    dtRoute = ds.Tables[9];
                    dtDetails = ds.Tables[10];
                    dtDetailsHotel = ds.Tables[11];
                    dtDetailsLuggage = ds.Tables[12];
                    dtLuggageUOM = ds.Tables[13];
                    dtDetailsSafeguard = ds.Tables[14];
                    dtSafeguardUOM = ds.Tables[15];
                    dtMeetGreet = ds.Tables[16];
                    dtVisa = ds.Tables[17];
                    dtOther = ds.Tables[18];

                    listAirportContract = (from a in dtAirContract.AsEnumerable()
                                           select new Airport
                                           {
                                               AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                               AirportName = a.Field<string>("Airport")
                                           }).ToList();

                    listAirportNotInContract = (from a in dtAirportNotInContract.AsEnumerable()
                                                select new Airport
                                                {
                                                    AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                    AirportName = a.Field<string>("Airport")
                                                }).ToList();

                    listSeaportContract = (from a in dtSeaContract.AsEnumerable()
                                           select new Seaport
                                           {
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = a.Field<string>("Seaport")
                                           }).ToList();

                    listSeaportNotInContract = (from a in dtSeaNotInContract.AsEnumerable()
                                                select new Seaport
                                                {
                                                    SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                    SeaportName = a.Field<string>("Seaport")
                                                }).ToList();


                    listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                       select new VehicleType
                                       {
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])

                                       }).ToList();

                    listVehicleTypeCapacity = (from a in dtCapacity.AsEnumerable()
                                               select new ContractVendorVehicleTypeCapacity
                                               {
                                                   ContractVehicleCapacityIDInt = GlobalCode.Field2Int(a["ContractVehicleCapacityID"]),
                                                   ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                   VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                   VehicleType = GlobalCode.Field2String(a["VehicleTypeName"]),
                                                   MinCapacity = GlobalCode.Field2Int(a["MinCapacity"]),
                                                   MaxCapacity = GlobalCode.Field2Int(a["MaxCapacity"]),
                                               }).ToList();


                    listCurrency = (from a in dtCurrency.AsEnumerable()
                                    select new Currency
                                    {
                                        CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                        CurrencyName = a.Field<string>("Currency")
                                    }).ToList();
                    listAttachment = (from a in dtAttachment.AsEnumerable()
                                      select new ContractPortAgentAttachment
                                      {
                                          FileName = a.Field<string>("colFileNameVarChar"),
                                          FileType = a.Field<string>("colFileTypeVarChar"),
                                          UploadedDate = a.Field<DateTime>("colUploadedDate"),
                                          uploadedFile = a.Field<byte[]>("colContractAttachmentVarBinary"),
                                          AttachmentId = a.Field<int>("colContractAttachmentIdInt")
                                      }).ToList();

                    listRoute = (from a in dtRoute.AsEnumerable()
                                 select new VehicleRoute
                                 {
                                     RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                     RouteDesc = a.Field<string>("colRouteNameVarchar")
                                 }).ToList();

                    //listRoute = (from a in dtRoute.AsEnumerable()
                    //          select new VehicleRoute {
                    //              RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                    //              RouteOrigin = a.Field<string>("colOriginVarchar"),
                    //              RouteDestination = a.Field<string>("colDestinationVarchar")
                    //          }).ToList();

                    listDetails = (from a in dtDetails.AsEnumerable()
                                   select new ContractPortAgentDetailsAmt
                                   {
                                       ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                                       ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                       PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                       ContractVehicleCapacityID = GlobalCode.Field2Int(a["colContractVehicleCapacityIDInt"]),
                                       VehicleTypeID = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                       VehicleType = a.Field<string>("VehicleType"),
                                       RouteIDFrom = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                       RouteIDTo = GlobalCode.Field2Int(a["colRouteIDToInt"]),
                                       RouteFrom = a.Field<string>("RouteFrom"),
                                       RouteTo = a.Field<string>("RouteTo"),
                                       Origin = a.Field<string>("colFromVarchar"),
                                       Destination = a.Field<string>("colToVarchar"),
                                       RateAmount = GlobalCode.Field2Float(a["colRateAmount"]),
                                       Tax = GlobalCode.Field2Float(a["colTaxPercent"]),
                                   }).ToList();

                    listDetailsHotel = (from a in dtDetailsHotel.AsEnumerable()
                                        select new ContractPortAgentDetailsAmtHotel { 
                                        ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                                        ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                        PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),

                                        IsRateByPercentBit = GlobalCode.Field2Bool(a["colIsRateByPercentBit"]),
                                        RoomCostPercent = GlobalCode.Field2Float(a["colRoomCostPercent"]),
        
                                        RoomRateTaxPercentage = GlobalCode.Field2Float(a["colRoomRateTaxPercentage"]),
                                        IsTaxInclusive = GlobalCode.Field2Bool(a["colIsTaxInclusive"]),

                                        RoomSingleRate =  GlobalCode.Field2Float(a["colRoomSingleRate"]),
                                        RoomDoubleRate  = GlobalCode.Field2Float(a["colRoomDoubleRate"]),
                                        MealStandardDecimal  = GlobalCode.Field2Float(a["colMealStandardDecimal"]),
                                        MealIncreasedDecimal  = GlobalCode.Field2Float(a["colMealIncreasedDecimal"]),
                                        MealTax = GlobalCode.Field2Float(a["colMealTaxPercent"]),

                                        SurchargeSingle = GlobalCode.Field2Float(a["colSurchargeSingle"]),
                                        SurchargeDouble = GlobalCode.Field2Float(a["colSurchargeDouble"]),

                                        BreakfastRate = GlobalCode.Field2Float(a["colBreakfastRate"]),
                                        LunchRate = GlobalCode.Field2Float(a["colLunchRate"]),
                                        DinnerRate = GlobalCode.Field2Float(a["colDinnerRate"]),
                                        MiscellaneaRate = GlobalCode.Field2Float(a["colMiscellaneaRate"]),
                                    }).ToList();

                    listDetailsLuggage = (from a in dtDetailsLuggage.AsEnumerable()
                                    select new ContractPortAgentDetailsAmtLuggage
                                    {
                                        ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                                        ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                        PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),

                                        LuggageRate = GlobalCode.Field2Float(a["colLuggageRate"]),
                                        LuggageUOMId = GlobalCode.Field2Int(a["colUOMIdIint"]),                                            
                                    }).ToList();

                    listLuggageUOM = (from a in dtLuggageUOM.AsEnumerable()
                                          select new LuggageUOM
                                          {
                                              LuggageUOMId = GlobalCode.Field2Int(a["colUOMIdIint"]),
                                              LuggageUOMName = GlobalCode.Field2String(a["colUOMName"])                                              
                                          }).ToList();

                    listDetailsSafeguard = (from a in dtDetailsSafeguard.AsEnumerable()
                                          select new ContractPortAgentDetailsAmtSafeguard
                                          {
                                              ContractDetailID = GlobalCode.Field2Int(a["colContractDetailIdInt"]),
                                              ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                              PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),

                                              SafeguardRate = GlobalCode.Field2Float(a["colSafeguardRate"]),
                                              SafeguardUOMId = GlobalCode.Field2Int(a["colUOMIdIint"]),
                                              SafeguardUOMName = GlobalCode.Field2String(a["colUOMName"]),
                                          }).ToList();

                    listSafeguardUOM = (from a in dtSafeguardUOM.AsEnumerable()
                                      select new SafeguardUOM
                                      {
                                          SafeguardUOMId = GlobalCode.Field2Int(a["colUOMIdIint"]),
                                          SafeguardUOMName = GlobalCode.Field2String(a["colUOMName"])
                                      }).ToList();

                    listMeetGreet = (from a in dtMeetGreet.AsEnumerable()
                                        select new ContractPortAgentDetailsAmtMeetGreet
                                        {
                                            MeetGreetRate = GlobalCode.Field2Decimal(a["colRateAmount"]),
                                            TimeFrom = GlobalCode.Field2String(a["TimeFrom"]),
                                            TimeTo = GlobalCode.Field2String(a["TimeTo"]),
                                            IsSurchargePercent = GlobalCode.Field2Bool(a["colIsSurchargeByPercentBit"]),
                                            MeetGreetSurcahrgeFee = GlobalCode.Field2Decimal(a["colSurchargeFee"]),
                                            MeetGreetRemarks = a.Field<string>("colRemarksVarchar")
                                        }).ToList();
                    listVisa= (from a in dtVisa.AsEnumerable()
                                     select new ContractPortAgentDetailsAmtVisa
                                     {
                                         VisaAmount = GlobalCode.Field2Float(a["colVisaAmount"]),
                                         VisaAccompany = GlobalCode.Field2Float(a["colVisaAccompany"]),
                                         ImmigrationFees = GlobalCode.Field2Float(a["colImmigrationFees"]),
                                         ImmigrationFees2 = GlobalCode.Field2Float(a["colImmigrationFees2"]),
                                         LetterOfInvitation = GlobalCode.Field2Float(a["colLetterOfInvitation"]),
                                         BusinessParoleRequest = GlobalCode.Field2Float(a["colBusinessParoleRequest"]),
                                         BusinessParoleProcessingFee = GlobalCode.Field2Float(a["colBusinessParoleProcessingFee"]),
                                         ImmigrationPortCaptaincyLetter = GlobalCode.Field2Float(a["colImmigrationPortCaptaincyLetter"]),
                                         VisaRemarks = a.Field<string>("colRemarksVarchar")
                                     }).ToList();
                    listOther = (from a in dtOther.AsEnumerable()
                                select new ContractPortAgentDetailsAmtOther
                                {
                                    ShorePassesRate = GlobalCode.Field2Float(a["colShorePassesRate"]),
                                    AnsweringTelephoneCallsEmailRate = GlobalCode.Field2Float(a["colAnsweringTelephoneCallsEmailRate"]),
                                    LostLuggageRate = GlobalCode.Field2Float(a["colLostLuggageRate"]),
                                    CarRate = GlobalCode.Field2Float(a["colCarRate"]),
                                    ImmigrationCustodyServiceAirportHotelRate = GlobalCode.Field2Float(a["colImmigrationCustodyServiceAirportHotelRate"]),
                                    ImmigrationCustodyServiceHotelRate = GlobalCode.Field2Float(a["colImmigrationCustodyServiceHotelRate"]),
                                    ImmigrationCustodyServiceHotelShipRate = GlobalCode.Field2Float(a["colImmigrationCustodyServiceHotelShipRate"]),
                                    TransportationToPharmacyRate = GlobalCode.Field2Float(a["colTransportationToPharmacyRate"]),
                                    TransportationToMedicalFacilityRate = GlobalCode.Field2Float(a["colTransportationToMedicalFacilityRate"]),
                                    WaitingTimeRate = GlobalCode.Field2Float(a["colWaitingTimeRate"]),
                                    OtherRemarks = a.Field<string>("colRemarksVarchar")
                                }).ToList();
                }

                HttpContext.Current.Session["ContractPortAgentDetails"] = list;
                HttpContext.Current.Session["VendorAirportExists"] = listAirportContract;
                HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotInContract;
                HttpContext.Current.Session["VendorSeaportExists"] = listSeaportContract;
                HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotInContract;
                HttpContext.Current.Session["VehicleType"] = listVehicleType;
                HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;
                HttpContext.Current.Session["ContractCurrency"] = listCurrency;
                HttpContext.Current.Session["ContractPortAgentAttachment"] = listAttachment;
                HttpContext.Current.Session["VehicleRoute"] = listRoute;
                HttpContext.Current.Session["ContractPortAgentDetailsAmt"] = listDetails;
                HttpContext.Current.Session["ContractPortAgentDetailsAmtHotel"] = listDetailsHotel;
                HttpContext.Current.Session["ContractPortAgentDetailsAmtLuggage"] = listDetailsLuggage;
                HttpContext.Current.Session["LuggageUOM"] = listLuggageUOM;
                HttpContext.Current.Session["ContractPortAgentDetailsAmtSafeguard"] = listDetailsSafeguard;
                HttpContext.Current.Session["SafeguardUOM"] = listSafeguardUOM;
                HttpContext.Current.Session["ContractPortAgentDetailsAmtMeetGreet"] = listMeetGreet;
                HttpContext.Current.Session["ContractPortAgentDetailsAmtVisa"] = listVisa;
                HttpContext.Current.Session["ContractPortAgentDetailsAmtOther"] = listOther;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtAirContract != null)
                {
                    dtAirContract.Dispose();
                }
                if (dtAirportNotInContract != null)
                {
                    dtAirportNotInContract.Dispose();
                }
                if (dtSeaContract != null)
                {
                    dtSeaContract.Dispose();
                }
                if (dtSeaNotInContract != null)
                {
                    dtSeaNotInContract.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtCapacity != null)
                {
                    dtCapacity.Dispose();
                }
                if (dtAttachment != null)
                {
                    dtAttachment.Dispose();
                }
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                }
                if (dtDetailsHotel != null)
                {
                    dtDetailsHotel.Dispose();
                }
                if (dtDetailsLuggage != null)
                {
                    dtDetailsLuggage.Dispose();
                }
                if (dtLuggageUOM != null)
                {
                    dtLuggageUOM.Dispose();
                }
                if (dtDetailsSafeguard != null)
                {
                    dtDetailsSafeguard.Dispose();
                }
                if (dtSafeguardUOM != null)
                {
                    dtSafeguardUOM.Dispose();
                } 
                if (dtMeetGreet != null)
                {
                    dtMeetGreet.Dispose();
                } 
                if (dtVisa != null)
                {
                    dtVisa.Dispose();
                }
                if (dtOther != null)
                {
                    dtOther.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   11/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport of Service Provider Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentContractAirport(Int32 iContractID, Int32 iPortAgentID ,Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            List<Airport> listAirport = new List<Airport>();
            List<Airport> listAirportNotExist = new List<Airport>();

            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtExists = null;
            DataTable dtNotExists = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVendorGetContractAirport");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pPortAgentIdInt", DbType.Int64, iPortAgentID);
                db.AddInParameter(dbCom, "@pFilterByInt", DbType.Int16, iFilterBy);
                db.AddInParameter(dbCom, "@pAirportFilter", DbType.String, sFilter);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                    HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;

                    if (dSet.Tables[0] != null)
                    {
                        dtExists = dSet.Tables[0];
                        listAirport = (from a in dtExists.AsEnumerable()
                                       select new Airport
                                       {
                                           AirportSeaportID = GlobalCode.Field2Int(a["ContractAirID"]),
                                           AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                           AirportName = GlobalCode.Field2String(a["Airport"])

                                       }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtNotExists = dSet.Tables[1];
                        listAirportNotExist = (from a in dtNotExists.AsEnumerable()
                                               select new Airport
                                               {
                                                   AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                   AirportName = GlobalCode.Field2String(a["Airport"])
                                               }).ToList();
                    }

                    HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                    HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["VendorAirportExists"] = listAirport;

                        if (dSet.Tables[0] != null)
                        {
                            dtExists = dSet.Tables[0];
                            listAirport = (from a in dtExists.AsEnumerable()
                                           select new Airport
                                           {
                                               AirportSeaportID = GlobalCode.Field2Int(a["ContractAirID"]),
                                               AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                               AirportName = GlobalCode.Field2String(a["Airport"])

                                           }).ToList();
                        }
                        HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                    }
                    else
                    {
                        HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;

                        if (dSet.Tables[0] != null)
                        {
                            dtNotExists = dSet.Tables[0];
                            listAirportNotExist = (from a in dtNotExists.AsEnumerable()
                                                   select new Airport
                                                   {
                                                       AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                       AirportName = GlobalCode.Field2String(a["Airport"])
                                                   }).ToList();
                        }
                        HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;
                    }
                }
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
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtExists != null)
                {
                    dtExists.Dispose();
                }
                if (dtNotExists != null)
                {
                    dtNotExists.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport of Service Provider Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentContractSeaport(Int32 iContractID, Int32 iPortAgentID,Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            List<Seaport> listSeaport = new List<Seaport>();
            List<Seaport> listSeaportNotExist = new List<Seaport>();

            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtExists = null;
            DataTable dtNotExists = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVendorGetContractSeaport");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pPortAgentIdInt", DbType.Int64, iPortAgentID);
                db.AddInParameter(dbCom, "@pFilterByInt", DbType.Int16, iFilterBy);
                db.AddInParameter(dbCom, "@pSeaportFilter", DbType.String, sFilter);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;

                    if (dSet.Tables[0] != null)
                    {
                        dtExists = dSet.Tables[0];
                        listSeaport = (from a in dtExists.AsEnumerable()
                                       select new Seaport
                                       {
                                           ID = GlobalCode.Field2Int(a["ContractSeaID"]),
                                           SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                           SeaportName = GlobalCode.Field2String(a["Seaport"])

                                       }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtNotExists = dSet.Tables[1];
                        listSeaportNotExist = (from a in dtNotExists.AsEnumerable()
                                               select new Seaport
                                               {
                                                   SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                   SeaportName = GlobalCode.Field2String(a["Seaport"])
                                               }).ToList();
                    }

                    HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;

                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;

                        if (dSet.Tables[0] != null)
                        {
                            dtExists = dSet.Tables[0];
                            listSeaport = (from a in dtExists.AsEnumerable()
                                           select new Seaport
                                           {
                                               ID = GlobalCode.Field2Int(a["ContractSeaID"]),
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = GlobalCode.Field2String(a["Airport"])

                                           }).ToList();
                        }
                        HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    }
                    else
                    {
                        HttpContext.Current.Session["VendorSeaportNotExists"] = listSeaportNotExist;

                        if (dSet.Tables[0] != null)
                        {
                            dtNotExists = dSet.Tables[0];
                            listSeaportNotExist = (from a in dtNotExists.AsEnumerable()
                                                   select new Seaport
                                                   {
                                                       SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                       SeaportName = GlobalCode.Field2String(a["Airport"])
                                                   }).ToList();
                        }
                        HttpContext.Current.Session["VendorSeaportNotExists"] = listSeaportNotExist;
                    }
                }
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
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtExists != null)
                {
                    dtExists.Dispose();
                }
                if (dtNotExists != null)
                {
                    dtNotExists.Dispose();
                }
            }
        }
        /// Date Created:  13/Nov/2013
        /// Created By:    Josephine Gad
        /// (description)  Add dtContractAirport, dtContractSeaport
        ///                Include Audit Trail
        /// --------------------------------------------------
        /// Date Modified: 28/Feb/2014
        /// Created By:    Josephine Gad
        /// (description)  Add dtContractVehicleType, dtDetails for Transportation Rate details             
        ///                Add Hotel rate details
        ///                Add Luggage rate details
        ///                Add Safeguard details
        /// --------------------------------------------------
        /// Date Modified: 04/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  Add Meet & Greet Details
        /// --------------------------------------------------
        /// Date Modified: 11/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  Add param bIsAirportToHotel and bIsHotelToShip
        /// --------------------------------------------------
        /// Date Modified: 22/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  Add Hotel details: Breakfast, Lunch, Dinner and Misc
        ///                Add Visa parameters, Add Other paremeters
        /// --------------------------------------------------
        /// Date Modified: 21/Jul/2014
        /// Created By:    Michael Brian C. Evangelista
        /// (description)  Add bool bIsRCL, bool bIsAZA, bool bIsCEL, bool bIsPUL,
        /// </summary>         
        public static void PortAgentInsertContract(int iContractID, int iPortAgentID,
            string sContractName, string sRemarks, string sDateStart,
            string sDateEnd, string sRCCLPerconnel, string sRCCLDateAccepted,
            string sVendorPersonnel, string sVendorDateAccepted, int iCurrency, bool bIsAirportToHotel, bool bIsHotelToShip,
             bool bIsRCL, bool bIsAZA, bool bIsCEL, bool bIsPUL, bool bIsSKS,
            string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtContractAirport, DataTable dtContractSeaport, DataTable dtContractVehicleType,
            DataTable dtAttachment, DataTable dtDetails, bool bIsRateByPercent, decimal dRoomCostPercent,
            decimal dRoomRateTaxPercentage, bool bIsTaxInclusive, decimal dRoomSingleRate, decimal dRoomDoubleRate,
            decimal dMealStandard, decimal dMealIncreased, decimal dMealTax, decimal dSurchargeSingle, decimal dSurchargeDouble,
            decimal dBreakfastRate, decimal dLunchRate, decimal dDinnerRate, decimal dMiscellaneaRate, 
            int iUOM, decimal dLuggageRate, DataTable dtSafeguardDetails, decimal dMeetGreetRate,
            string sFromTime, string sToTime,  bool bIsMeetGreetSurchargePercent, decimal dMeetGreetSurchargeFee, string sMeetGreetRemarks,
            
            //Visa details
            decimal dVisaAmount, decimal dVisaAccompany, decimal dImmigrationFees, decimal dImmigrationFees2,
            decimal dLetterOfInvitation, decimal dBusinessParoleRequest, decimal dBusinessParoleProcessingFee,
            decimal dImmigrationPortCaptaincyLetter, string sVisaRemarks,
            
            //Other details
            decimal dShorePassesRate, decimal dAnsweringTelephoneCallsEmailRate, 
            decimal dLostLuggageRate, decimal dCarRate,
	        decimal dImmigrationCustodyServiceAirportHotelRate,  decimal dImmigrationCustodyServiceHotelRate, 
	        decimal dImmigrationCustodyServiceHotelShipRate, decimal dTransportationToPharmacyRate, 
	        decimal dTransportationToMedicalFacilityRate,   
	        decimal dWaitingTimeRate, string sOtherRemarks)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspInsertPortAgentContract");

                db.AddInParameter(dbCommand, "@pContractIdInt", DbType.Int32, iContractID);
                db.AddInParameter(dbCommand, "@pPortAgentVendorIDInt", DbType.Int32, iPortAgentID);
                db.AddInParameter(dbCommand, "@pContractNameVarchar", DbType.String, sContractName);
                db.AddInParameter(dbCommand, "@pRemarksVarchar", DbType.String, sRemarks);

                db.AddInParameter(dbCommand, "@pContractDateStartedDate", DbType.Date, GlobalCode.Field2Date(sDateStart));
                db.AddInParameter(dbCommand, "@pContractDateEndDate", DbType.Date, GlobalCode.Field2Date(sDateEnd));

                db.AddInParameter(dbCommand, "@pRCCLPersonnel", DbType.String, sRCCLPerconnel);

                if (sRCCLDateAccepted != "")
                {
                    db.AddInParameter(dbCommand, "@pRCCLAcceptedDate", DbType.Date, sRCCLDateAccepted);
                }
                db.AddInParameter(dbCommand, "@pVendorPersonnel", DbType.String, sVendorPersonnel);
                if (sVendorDateAccepted != "")
                {
                    db.AddInParameter(dbCommand, "@pVendorAcceptedDate", DbType.Date, sVendorDateAccepted);
                }
                db.AddInParameter(dbCommand, "@pCurrencyIDInt", DbType.Int32, iCurrency);

                db.AddInParameter(dbCommand, "@pIsAutoAirportToHotel", DbType.Boolean, bIsAirportToHotel);
                db.AddInParameter(dbCommand, "@pIsAutoHotelToShip", DbType.Boolean, bIsHotelToShip);

                db.AddInParameter(dbCommand, "@pIsRCI", DbType.Boolean, bIsRCL);
                db.AddInParameter(dbCommand, "@pIsAZA", DbType.Boolean, bIsAZA);
                db.AddInParameter(dbCommand, "@pIsCEL", DbType.Boolean, bIsCEL);
                db.AddInParameter(dbCommand, "@pIsPUL", DbType.Boolean, bIsPUL);
                db.AddInParameter(dbCommand, "@pIsSKS", DbType.Boolean, bIsSKS);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFilename", DbType.String, sFilename);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);

                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GlobalCode.Field2Date(sGMTDate));
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);
               

                SqlParameter param = new SqlParameter("@pTblContractPortAgentAirport", dtContractAirport);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractPortAgentSeaport", dtContractSeaport);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractVehicleType", dtContractVehicleType);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractPortAgentAttachment", dtAttachment);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractVehicleDetail", dtDetails);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                param = new SqlParameter("@pTblContractSafeguardDetail", dtSafeguardDetails);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                //Hotel Details
                db.AddInParameter(dbCommand, "@pIsRateByPercentBit", DbType.Boolean, bIsRateByPercent);
                db.AddInParameter(dbCommand, "@pRoomCostPercent", DbType.Decimal, dRoomCostPercent);
                db.AddInParameter(dbCommand, "@pRoomRateTaxPercentage", DbType.Decimal, dRoomRateTaxPercentage);
                db.AddInParameter(dbCommand, "@pIsTaxInclusive", DbType.Boolean, bIsTaxInclusive);
                db.AddInParameter(dbCommand, "@pRoomSingleRate", DbType.Decimal, dRoomSingleRate);
                db.AddInParameter(dbCommand, "@pRoomDoubleRate", DbType.Decimal, dRoomDoubleRate);
                db.AddInParameter(dbCommand, "@pMealStandardDecimal", DbType.Decimal, dMealStandard);
                db.AddInParameter(dbCommand, "@pMealIncreasedDecimal", DbType.Decimal, dMealIncreased);
                db.AddInParameter(dbCommand, "@pMealTaxPercent", DbType.Decimal, dMealTax);

                db.AddInParameter(dbCommand, "@pSurchargeSingle", DbType.Decimal, dSurchargeSingle);
                db.AddInParameter(dbCommand, "@pSurchargeDouble", DbType.Decimal, dSurchargeDouble);

                db.AddInParameter(dbCommand, "@pBreakfastRate", DbType.Decimal, dBreakfastRate);
                db.AddInParameter(dbCommand, "@pLunchRate", DbType.Decimal, dLunchRate);
                db.AddInParameter(dbCommand, "@pDinnerRate", DbType.Decimal, dDinnerRate);
                db.AddInParameter(dbCommand, "@pMiscellaneaRate", DbType.Decimal, dMiscellaneaRate);                

                //Luggage Details
                db.AddInParameter(dbCommand, "@pUOMIdIint", DbType.Int32, iUOM);
                db.AddInParameter(dbCommand, "@pLuggageRate", DbType.Decimal, dLuggageRate);

                db.AddInParameter(dbCommand, "@pMeetGreetRate", DbType.Decimal, dMeetGreetRate);
                db.AddInParameter(dbCommand, "@pMeetGreetTimeFrom", DbType.Time, GlobalCode.Field2DateTime(sFromTime));
                db.AddInParameter(dbCommand, "@pMeetGreetTimeTo", DbType.Time, GlobalCode.Field2DateTime(sToTime));
                db.AddInParameter(dbCommand, "@pMeetGreetSurchargeByPercentBit", DbType.Boolean, bIsMeetGreetSurchargePercent);
                db.AddInParameter(dbCommand, "@pMeetGreetSurchargeFee", DbType.Decimal, dMeetGreetSurchargeFee);
                db.AddInParameter(dbCommand, "@pMeetGreetRemarks", DbType.String, sMeetGreetRemarks);

                //Visa Details
                db.AddInParameter(dbCommand, "@pVisaAmount", DbType.Decimal, dVisaAmount);
                db.AddInParameter(dbCommand, "@pVisaAccompany", DbType.Decimal, dVisaAccompany);
                db.AddInParameter(dbCommand, "@pImmigrationFees", DbType.Decimal, dImmigrationFees);
                db.AddInParameter(dbCommand, "@pImmigrationFees2", DbType.Decimal, dImmigrationFees2);
                db.AddInParameter(dbCommand, "@pLetterOfInvitation", DbType.Decimal, dLetterOfInvitation);
                db.AddInParameter(dbCommand, "@pBusinessParoleRequest", DbType.Decimal, dBusinessParoleRequest);
                db.AddInParameter(dbCommand, "@pBusinessParoleProcessingFee", DbType.Decimal, dBusinessParoleProcessingFee);
                db.AddInParameter(dbCommand, "@pImmigrationPortCaptaincyLetter", DbType.Decimal, dImmigrationPortCaptaincyLetter);
                db.AddInParameter(dbCommand, "@pVisaRemarks", DbType.String, sVisaRemarks);

                //Other Details
                db.AddInParameter(dbCommand, "@pShorePassesRate", DbType.Decimal, dShorePassesRate);
                db.AddInParameter(dbCommand, "@pAnsweringTelephoneCallsEmailRate", DbType.Decimal, dAnsweringTelephoneCallsEmailRate);
                db.AddInParameter(dbCommand, "@pImmigrationCustodyServiceAirportHotelRate", DbType.Decimal, dImmigrationCustodyServiceAirportHotelRate);
                db.AddInParameter(dbCommand, "@pImmigrationCustodyServiceHotelRate", DbType.Decimal, dImmigrationCustodyServiceHotelRate);
                db.AddInParameter(dbCommand, "@pImmigrationCustodyServiceHotelShipRate", DbType.Decimal, dImmigrationCustodyServiceHotelShipRate);
                db.AddInParameter(dbCommand, "@pTransportationToPharmacyRate", DbType.Decimal, dTransportationToPharmacyRate);
                db.AddInParameter(dbCommand, "@pTransportationToMedicalFacilityRate", DbType.Decimal, dTransportationToMedicalFacilityRate);
                db.AddInParameter(dbCommand, "@pLostLuggageRate", DbType.Decimal, dLostLuggageRate);
                db.AddInParameter(dbCommand, "@pWaitingTimeRate", DbType.Decimal, dWaitingTimeRate);
                db.AddInParameter(dbCommand, "@pCarRate", DbType.Decimal, dCarRate);
                db.AddInParameter(dbCommand, "@pOtherRemarks", DbType.String, sOtherRemarks);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                //Int32 pID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pID"));
                //return pID;
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
                if (dtContractAirport != null)
                {
                    dtContractAirport.Dispose();
                }
                if (dtContractSeaport != null)
                {
                    dtContractSeaport.Dispose();
                }
                if (dtContractVehicleType != null)
                {
                    dtContractVehicleType.Dispose();
                }
                if (dtAttachment != null)
                {
                    dtAttachment.Dispose();
                }   
                if (dtDetails != null)
                {
                    dtDetails.Dispose();
                }
                if (dtSafeguardDetails != null)
                {
                    dtSafeguardDetails.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>                     
        /// Date Modified:  13/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Get list of Service Provider Contracts
        /// </summary>     
        public static List<ContractPortAgent> GetPortAgentContractByBranchID(int iPortAgentID)
        {
            List<ContractPortAgent> contractList = new List<ContractPortAgent>();

            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetPortAgentContractByBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentVendorIDInt", DbType.Int32, iPortAgentID);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                contractList = (from a in SFDataTable.AsEnumerable()
                                select new ContractPortAgent
                                {
                                    ContractID = GlobalCode.Field2Int(a["cId"]),
                                    PortAgentName = GlobalCode.Field2String(a["BranchName"]),
                                    ContractStatus = GlobalCode.Field2String(a["CONTRACTSTATUS"]),
                                    ContractName = GlobalCode.Field2String(a["CONTRACTNAME"]),
                                    ContractDateStart = GlobalCode.Field2String(a["CONTRACTSTARTDATE"]),
                                    ContractDateEnd = GlobalCode.Field2String(a["CONTRACTENDDATE"]),
                                    Remarks = GlobalCode.Field2String(a["REMARKS"]),
                                    BranchID = GlobalCode.Field2Int(a["BRANCHID"]),
                                    IsActive = GlobalCode.Field2Bool(a["colIsActiveBit"]),
                                    IsCurrent = GlobalCode.Field2Bool(a["IsCurrent"]),
                                    DateCreated = a.Field<DateTime>("DateCreated")
                                }).ToList();

                return contractList;
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
            }
        }

        ///<summary>
        ///Date Created: 006/08/2014
        ///Created By: Michael Evangelista
        ///Description: Activate Seaport
        /// </summary>

        public static void SeaportActivate(string Username, int SeaportIDtoActivate, string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSeaportIDActivate");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeaportID", DbType.Int32, SeaportIDtoActivate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);
                //for audittrail
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        ///<summary>
        ///Date Created: 06/08/2014
        ///Created By: Michael Evangelista
        ///Description: De-Activate Seaport
        /// </summary>
        public static void SeaportInActivate(string Username, int SeaportIDtoActivate, string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSeaportIDInActivate");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeaportID", DbType.Int32, SeaportIDtoActivate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);
                //for audittrail
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created:   13/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Cancel Service Provider contract
        /// ------------------------------------------------
        /// </summary>     
        public static void PortAgentCancelContract(Int32 ContractID, string Username,
             string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspPortAgentCancelContract");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolContractID", DbType.Int32, ContractID);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolUserName", DbType.String, Username);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, sDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, sFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilename", DbType.String, sFilename);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);

                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);

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
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        #endregion

        #region PORT CONTRACT OTHER SERVICES
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: save Service Provider other services details
        /// </summary>
        /// <param name="contractPortAgentServiceId"></param>
        /// <param name="PortId"></param>
        /// <param name="VendorType"></param>
        /// <param name="ServiceRate"></param>
        /// <param name="UserId"></param>
        /// <param name="contractId"></param>
        /// <param name="Remarks"></param>
        /// <param name="detailID"></param>
        /// <param name="Currency"></param>
        public static void SaveContractPortAgentOther(string contractPortAgentServiceId,
            string PortId, string VendorType, string ServiceRate, string UserId, string contractId,
            string Remarks, string detailID, string Currency)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTrans = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveContractPortAgentVendor");
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(contractPortAgentServiceId));
                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                db.AddInParameter(dbCommand, "@pVendorTypeInt", DbType.Int32, Int32.Parse(VendorType));
                db.AddInParameter(dbCommand, "@pMealRate", DbType.String, ServiceRate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.String, contractId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceDetailId", DbType.Int32, Int32.Parse(detailID));
                db.AddInParameter(dbCommand, "@pCurrency", DbType.Int32, Int32.Parse(Currency));
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
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbTrans != null)
                {
                    dbTrans.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider contract other services details
        /// ------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="contractServiceId"></param>
        /// <returns></returns>
        public static IDataReader LoadPortAgentContractOthers(string contractServiceId)
        {
            IDataReader dr = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadPortContractOther");
                db.AddInParameter(dbCommand, "@pContractPortAgentServiceId", DbType.Int32, Int32.Parse(contractServiceId));
                dr = db.ExecuteReader(dbCommand);
                return dr;
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
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        #endregion
    }
}

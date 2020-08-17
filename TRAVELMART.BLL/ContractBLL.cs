using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using System.Data.Common;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class ContractBLL
    {
        private ContractDAL ctDAL = new ContractDAL();
        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list currency
        /// </summary>  

        public static DataTable CurrencyLoad()
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetCurrencyList();
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
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Select  currency by country ID
        /// -------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  

        public static IDataReader GetCurrencyByCountry(string CountryID)
        {
            IDataReader dt = null;
            try
            {
                Int16 CountryIDInt = Convert.ToInt16(CountryID);
                dt = ContractDAL.GetCurrencyByCountry(CountryIDInt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel contract
        /// </summary>  
        public static DataTable GetVendorHotelContractList(string Hotel, string UserName)
        {           
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelContractList(Hotel, UserName);
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
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch contract by branch ID
        /// </summary>  
        public static DataTable GetVendorHotelBranchContractByBranchID(string HotelBranchID)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelBranchContractByBranchID(HotelBranchID);
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
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch contract by branch ID
        /// </summary>  
        public static DataTable GetVendorHotelBranchContractActiveByBranchID(string HotelBranchID)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelBranchContractActiveByBranchID(HotelBranchID);
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
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch contract pending
        /// </summary>  
        public static DataTable GetVendorHotelBranchPendingContract()
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelBranchPendingContract();
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
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch contract pending
        /// </summary>  
        public static DataTable GetVendorVehicleBranchPendingContract(string sVehicleName)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorVehicleBranchPendingContract(sVehicleName);
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
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting list of hotel branch no active contract
        /// </summary>  
        public static DataTable GetVendorHotelBranchNoActiveContract(string Username)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelBranchNoActiveContract(Username);
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
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch no active contract
        /// </summary>  
        public static DataTable GetVendorVehicleBranchNoActiveContract(string Username)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorVehicleBranchNoActiveContract(Username);
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
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Get hotel contract details 
        /// ------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader GetVendorHotelContract(string vmId)
        {            
            IDataReader dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelContract(vmId);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
                    
        }

        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Ryan bautsita
        /// (description) Get hotel branch contract details 
        /// </summary>  
        public static DataTable GetVendorHotelBranchContract(string vmId)
        {           
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelBranchContract(vmId);
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
        /// Date Created:   08/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of hotel contract details By Room
        /// -----------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>            
        public static IDataReader GetVendorHotelBranchContractByRoomType(string branchID, string RoomID)
        {
            IDataReader dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelBranchContractByRoomType(branchID, RoomID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle vendor maintenance information   
        /// =====================================================================
        /// Date Modified:  25/Sept/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void
        /// -------------------------------------------------------------
        /// </summary>  
        public static void GetVendorVehicleContractByContractID(string contractId, string branchId,  Int16 iLoadType)
        {
            ContractDAL.GetVendorVehicleContractByContractID(contractId, branchId, iLoadType);           
        }

        /// <summary>
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle vendor maintenance information with luggage van           
        /// </summary>  
        public static DataTable GetVendorVehicleContractWithLuggageVanByContractID(string contractId, string branchId)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorVehicleContractWithLuggageVanByContractID(contractId, branchId);
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
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle vendor maintenance information with service rate           
        /// </summary>  
        public static DataTable GetVendorVehicleContractWithServiceRateByContractID(string contractId, string branchId)
        {
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorVehicleContractWithServiceRateByContractID(contractId, branchId);
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
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Get hotel contract details by contract ID
        /// </summary>  
        public static DataTable GetVendorHotelContractByContractID(string cID, string BranchID)
        {           
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetVendorHotelContractByContractID(cID, BranchID);
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
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Insert or update hotel contract details 
        /// </summary>  
        //public static Int32 AddSaveHotelContract(string VendorID, string vContract, string Remarks, string dtStart, string dtEnd, string RCCLRep,
        //                                        string vRep, string dtRCCLAccepted, string dtVendorAccepted, string CountryID, string CityID, string Username,
        //                                        string MealRate, string MealRateTax, bool TaxInclusive, bool Breakfast, bool Lunch, bool Dinner,
        //                                        bool LunchDinner, bool Shuttle, string Filename, string FileType, byte[] FileData, string DateUploaded,
        //                                        string ContractID, out object sqlTransaction)
        //{
        //    Int32 ContractIDInt = 0;
        //    if (ContractID != "")
        //    {
        //        ContractIDInt = Convert.ToInt32(ContractID);
        //    }
        //    else
        //    {
        //        ContractIDInt = 0;
        //    }

        //    return ContractDAL.AddSaveHotelContract(VendorID, vContract, Remarks, dtStart, dtEnd, RCCLRep, vRep, dtRCCLAccepted,
        //                                     dtVendorAccepted, CountryID, CityID, Username, MealRate, MealRateTax, TaxInclusive,
        //                                     Breakfast, Lunch, Dinner, LunchDinner, Shuttle, Filename, FileType, FileData, DateUploaded, ContractIDInt, out sqlTransaction);
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
        public static Int32 AddSaveHotelContract(Int32 VendorID, Int32 BranchID, string vContract, string Remarks, string dtStart, string dtEnd, string RCCLRep,
                                        string vRep, string dtRCCLAccepted, string dtVendorAccepted, string Username,
                                        string MealRate, string MealRateTax, bool TaxInclusive, bool Breakfast, bool Lunch, bool Dinner,
                                        bool LunchDinner, bool Shuttle, string Filename, string FileType, byte[] FileData, string DateUploaded,
                                        string ContractID, out object sqlTransaction,
                                        string VendorRepContactNo, string RCCLRepContactNo,
                                        string VendorRepEmailAdd, string RCCLRepEmailAdd,
                                        int iCancelationTerms, string sCutoffTime, string HotelTimeZoneID,
                                        int CurrencyIDInt, float RoomRateDbl, float RoomRateSgl)
        {

            Int32 ContractIDInt = 0;
            if (ContractID != "")
            {
                ContractIDInt = Convert.ToInt32(ContractID);
            }
            else
            {
                ContractIDInt = 0;
            }

            return ContractDAL.AddSaveHotelContract(VendorID, BranchID, vContract, Remarks, dtStart, dtEnd, RCCLRep, vRep, dtRCCLAccepted,
                                             dtVendorAccepted, Username, MealRate, MealRateTax, TaxInclusive,
                                             Breakfast, Lunch, Dinner, LunchDinner, Shuttle, Filename, FileType, FileData, DateUploaded,
                                             ContractIDInt, out sqlTransaction, VendorRepContactNo, RCCLRepContactNo,
                                             VendorRepEmailAdd, RCCLRepEmailAdd, iCancelationTerms, sCutoffTime,  HotelTimeZoneID, 
                                             CurrencyIDInt, RoomRateDbl, RoomRateSgl);
        }
            




            /// <summary>
        /// Create by:      Jefferson Bermundo
        /// Date Created:   07/31/2012
        /// Description:    Add/Insert hotel contract attachments.
        ///                 Disposing of the current transaction is being done in this section.
        /// </summary>
        public static void AddSaveHotelContractAttachments(int contractId, string fileName, string fileType,
            byte[] uploadedFile, DateTime dateUploaded, object sqlTrans, bool LastAttachment)
        {
            ContractDAL.AddSaveHotelContractAttachments(contractId, fileName, fileType, uploadedFile, dateUploaded, sqlTrans, LastAttachment);
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
            return ContractDAL.GetHotelContractAttachment(contractId);
        }


        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Insert or update hotel contract details 
        /// ------------------------------------------------------
        /// Date Modified:  02/June/2016
        /// Modified By:    Josephine Monteza
        /// (description)   Dispose DataTable for optimization
        /// </summary>  
        public static Int32 AddSaveHotelDetailContract(Int32 pID, string Desc, 
            string UserName, DataTable dt)
        {
            try
            {
                Int32 ContractDetailID = 0;
                ContractDetailID = ContractDAL.AddSaveHotelDetailContract(pID, Desc, UserName, dt);
                return ContractDetailID;
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
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert or update vehicle contract details 
        /// </summary>  
        public static void AddSaveVehicleDetailContract(Int32 pID, int Type, string Capacity, string Origin, string Destination, Int32 Currency, string Rate,
                                                        int LVType, string LVBags, string LVOrigin, string LVDestination, Int32 LVCurrency, string LVRate,
                                                        Int32 SRSeamansVisaCurrencyID, string SRSeamansVisaCurrencyRate, Int32 SRBaggageTraceCurrencyID, string SRBaggageTraceCurrencyRate,
                                                        Int32 SRAgencyFeesCurrencyID, string SRAgencyFeesCurrencyRate, Int32 SROkToBoardCurrencyID, string SROkToBoardCurrencyRate, string User)                                                       
        {            
            ContractDAL.AddSaveVehicleDetailContract(pID, Type, Capacity, Origin, Destination, Currency, Rate,
                                                     LVType, LVBags, LVOrigin, LVDestination, LVCurrency, LVRate,
                                                     SRSeamansVisaCurrencyID, SRSeamansVisaCurrencyRate, SRBaggageTraceCurrencyID, SRBaggageTraceCurrencyRate,
                                                     SRAgencyFeesCurrencyID, SRAgencyFeesCurrencyRate, SROkToBoardCurrencyID, SROkToBoardCurrencyRate, User);                                                    
        }

        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle contract 
        /// </summary>  
        public static DataTable vendorVehicleGetContractList(String strVehicleName, String strUserName)
        {            
            DataTable dt = null;
            try
            {
                dt = ContractDAL.vendorVehicleGetContractList(strVehicleName, strUserName);
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
        /// Date Created: 29/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of vehicle branch contract by branch ID
        /// =========================================================================
        /// Date Modified:  12Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void and put it in Session
        /// </summary>  
        public static List<ContractVehicle> GetVendorVehicleBranchContractByBranchID(string VehicleBranchID, string sUserID)
        {
            return ContractDAL.GetVendorVehicleBranchContractByBranchID(VehicleBranchID, sUserID);
        }

        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle contract details
        /// </summary>  
        public static DataTable vendorVehicleGetContractDetail(String contractId, String branchId)
        {           
            DataTable dt = null;
            try
            {
                dt = ContractDAL.vendorVehicleGetContractDetail(contractId, branchId);
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
        /// Date Created: 24/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vendor vehicle contract         
        /// --------------------------------------------------
        /// Date Modified:  15/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add dtContractAirport, dtContractSeaport, dtContractVehicleType
        /// --------------------------------------------------
        /// Date Modified:  28/Feb/2014
        /// Modified By:    Josephine Gad
        /// (description)   Dispose all Tables in finally clause
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
            try
            {
                ContractDAL.vehicleInsertContract(iContractID, iVehicleVendorID,
                sContractName, sRemarks, sDateStart, sDateEnd, sRCCLPerconnel, sRCCLDateAccepted,
                sVendorPersonnel, sVendorDateAccepted, iCurrency, bIsAirportToHotel, bIsHotelToShip, sUserName, sDescription, sFunction, sFilename,
                sGMTDate, bIsRCL, bIsAZA, bIsCEL, bIsPUL, bIsSKS,
                dtContractAirport, dtContractSeaport, dtContractVehicleType, dtAttachment, dtDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
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
            }
        }

        /// <summary>            
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Update vendor vehicle contract
        /// </summary> 
        public static void vehicleUpdateContract(Int32 contractId, Int32 contractDetailId, Int32 vendorId, String contractName, DateTime contractStartDate, DateTime contractEndDate,
                                                  String contractRate, String contractTax, String strRemarks, String strDescription,
                                                  Int32 vehicleType, DateTime strStartDate, DateTime strEndDate, String strUserName)
        {           
            ContractDAL.vehicleUpdateContract(contractId, contractDetailId, vendorId, contractName, contractStartDate, contractEndDate, contractRate, contractTax,
                                               strRemarks, strDescription, vehicleType, strStartDate, strEndDate, strUserName);
        }

       

        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Insert or update port contract details 
        /// </summary>  
        public static void AddSavePortContract(Int32 PortContractID, Int32 PortCompanyID, string vContract, string Remarks, string dtStart, 
            string dtEnd, string RateperHead, string TaxRate, string Currency, bool TaxInclusive, string RCCLRep, string vRep,
            Int32 SeafarerCount, Int32 PortAgentId, string UserId,
            string fileName, string fileType, byte[] imageBytes, bool attachChanged, Int32 CountryId)
        {           
            ContractDAL.AddSavePortContract(PortContractID, PortCompanyID, vContract, Remarks, dtStart, 
                dtEnd, RateperHead, TaxRate, Currency, TaxInclusive, RCCLRep, vRep,
                SeafarerCount,PortAgentId,UserId, fileName, fileType, imageBytes, attachChanged, CountryId);
        }

        /// <summary>            
        /// Date Created: 9/10/2011
        /// Created By: Ryan Bautista
        /// (description) check if hotel contract is active and approved
        /// </summary>  
        public static Int32 uspGetVendorHotelBranchContractActiveByContractID(Int32 ContractID)
        {
            Int32 Counter = 0;
            try
            {
                Counter = ContractDAL.GetVendorHotelBranchContractActiveByContractID(ContractID);
                return Counter;
            }
            catch (Exception ex)
            {
                Counter = 0;
                throw ex;
            }
            finally
            {
                //if (dt != null)
                //{
                //    dt.Dispose();
                //}
            }
        }

        /// <summary>            
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) check if vehicle contract is active and approved
        /// </summary>  
        public static Int32 uspGetVendorVehicleBranchContractActiveByContractID(Int32 contractID)
        {
            Int32 Counter = 0;
            try
            {
                Counter = ContractDAL.GetVendorVehicleBranchContractActiveByContractID(contractID);
                return Counter;
            }
            catch (Exception ex)
            {
                Counter = 0;
                throw ex;
            }
            finally
            {
                //if (dt != null)
                //{
                //    dt.Dispose();
                //}
            }
        }

        /// <summary>            
        /// Date Created: 15/05/2013
        /// Created By: Marco Abejar
        /// (description) get approved contract id
        /// </summary>  
        public static Int32 GetApprovedVendorHotelBranchContractByBranchID(Int32 BranchID)
        {
            Int32 Counter = 0;
            try
            {
                Counter = ContractDAL.GetApprovedVendorHotelBranchContractByBranchID(BranchID);
                return Counter;
            }
            catch (Exception ex)
            {
                Counter = 0;
                throw ex;
            }
            finally
            {
                //if (dt != null)
                //{
                //    dt.Dispose();
                //}
            }
        }

        /// <summary>            
        /// Date Created: 7/10/2011
        /// Created By: Ryan Bautista
        /// (description) update hotel contract status 
        /// </summary>  
        public static void UpdateContractStatus(Int32 ContractID, string Username)
        {
            ContractDAL.UpdateContractStatus(ContractID, Username);
        }

        /// <summary>            
        /// Date Created: 14/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) update vehicle contract status 
        /// --------------------------------------------
        /// Date Modified:  07/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add audit trail
        /// </summary>     
        /// </summary>  
        public static void UpdateVehicleContractStatus(Int32 ContractID, string Username,
             string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {      
            ContractDAL.UpdateVehicleContractStatus(ContractID, Username, sDescription, 
                sFunction, sFilename, GMTDate);
        }

        /// <summary>            
        /// Date Created: 7/10/2011
        /// Created By: Ryan Bautista
        /// (description) update hotel contract flag 
        /// </summary>  
        public static void UpdateContractFlag(Int32 ContractID, string Username)
        {
            ContractDAL.UpdateContractFlag(ContractID, Username);
        }

        /// <summary>            
        /// Date Created: 14/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) update vehicle contract flag 
        /// ------------------------------------------------
        /// Date Modified:  26/Sep/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add audit trail fields
        /// </summary>  
        public static void UpdateVehicleContractFlag(Int32 ContractID, string Username,
            string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            ContractDAL.UpdateVehicleContractFlag(ContractID, Username, sDescription, sFunction, sFilename, GMTDate);
        }
        /// <summary>            
        /// Date Created: 14/10/2011
        /// Created By: Ryan Bautista
        /// (description) Insert attach contract
        /// </summary>  
        public static void InsertAttachHotelContract(string filename, Byte[] contract, Int32 length, string mime)
        {
            ContractDAL.InsertAttachHotelContract(filename, contract, length, mime);
        }
        /// <summary>            
        /// Date Created: 14/10/2011
        /// Created By: Ryan Bautista
        /// (description) select attach contract
        /// --------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader GetContractAttachment(string ContractID, string BranchID)
        {
            Int32 intContractID = Convert.ToInt32(ContractID);
            Int32 intBranchID = Convert.ToInt32(BranchID);

            return ContractDAL.GetContractAttachment(intContractID, intBranchID);
        }

        /// <summary>            
        /// Date Created: 18/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) select attach vehicle contract
        /// --------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader GetVehicleContractAttachment(string ContractID, string BranchID)
        {
            Int32 intContractID = Convert.ToInt32(ContractID);
            Int32 intBranchID = Convert.ToInt32(BranchID);

            return ContractDAL.GetVehicleContractAttachment(intContractID, intBranchID);
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
            DataTable dt = null;
            try
            {
                dt = ContractDAL.GetHotelContractOverrideByDate(IDBigint, SeqNo,
                TransHotelID, PendingHotelID, sUser, BranchID, dDateFrom, dDateTo, sRole, sRoomType, IsNew);
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
        /// Date Created: 19/10/2011
        /// Created By: Charlene Remotigue
        /// (description) load Service Provider contract list
        /// </summary>     
        public DataTable GetPortContractList(string portAgentId)
        {
            try
            {
                return ctDAL.GetPortContractList(portAgentId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>            
        /// Date Created: 24/08/2011
        /// Created By: Ryan Bautista
        /// (description) Get hotel contract details by contract ID
        /// </summary>  
        public static DataTable GetVendorHotelContract(string cID, string BranchID, out DataTable dt2)
        {
            DataTable dt = null;
            try
            {
                DataTable dtRoomCount = new DataTable();
                dt = ContractDAL.GetVendorHotelContract(cID, BranchID, out dt2);
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

        #region PORT CONTRACT
        /// <summary>            
        /// Date Created: 19/10/2011
        /// Created By: Charlene Remotigue
        /// (description) load Service Provider contract list
        /// </summary>     
        public Int32 GetPortContractListCount(string portAgentId)
        {
            try
            {
                return ctDAL.GetPortContractListCount(portAgentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         //<summary>            
         //Date Created: 20/10/2011
         //Created By: Charlene Remotigue
        //(description) load Service Provider contract details
         //</summary>     
        //public static IDataReader GetPortContractDetails(string portAgentId, string ContractId)
        //{
        //    try
        //    {
        //        return ContractDAL.GetPortContractDetails(portAgentId, ContractId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Date Created:20/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Port Contract
        /// </summary>
        public void PortAgentContractListViewDelete(int ContractId, string UserId)
        {
            try
            {
                ctDAL.PortAgentContractListViewDelete(ContractId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 24/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Pending Service Provider Contracts 
        /// ---------------------------------------------------
        /// Date Modifed:   18/Feb/2014
        /// Modifed By:     Josephine Gad
        /// Description:    Use session to get count
        /// </summary>
        public Int32 PortAgentContractApprovalListCount(string sortParam, string sPortName)
        {
            try
            {
                return GlobalCode.Field2Int(HttpContext.Current.Session["PortAgentApproval_Count"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:24/11/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Service Provider pending contract        
        /// </summary>
        /// 
        public DataTable PortAgentContractApprovalList(string sortParam, Int32 startRowIndex, Int32 maximumRows, string sPortName)
        {
            try
            {
                return ctDAL.PortAgentContractApprovalList(sortParam, startRowIndex, maximumRows, sPortName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   18/Feb/2014
        /// Created By:     Josephine Gad
        /// Description:    Approve pending Service Provider contract       
        /// ----------------------------------------------------------
        /// </summary>       
        public static void UpdatePortAgentContractStatus(Int32 ContractID, string Username,
             string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            try
            {
                ContractDAL.UpdatePortAgentContractStatus(ContractID, Username, sDescription,
                sFunction, sFilename, GMTDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created: 25/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Service Provider with no Contracts 
        /// </summary>
        //public Int32 PortAgentNoActiveContractListCount(string sortParam)
        //{
        //    try
        //    {
        //        return ctDAL.PortAgentNoActiveContractListCount();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Date Created:25/11/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Service Provider with no contracts        
        /// </summary>
        /// 
        //public DataTable PortAgentNoActiveContractList(string sortParam, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    try
        //    {
        //        return ctDAL.PortAgentNoActiveContractList(sortParam, startRowIndex, maximumRows);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/11/2011
        /// Description: load Service Provider contract hotel services with details and specifications
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        //public static DataTable GetPortContractHotelServicesWithDetails(string ContractId)
        //{
        //    try
        //    {
        //        return ContractDAL.GetPortContractHotelServicesWithDetails(ContractId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/11/2011
        /// Description: load Service Provider contract vehicle services with details and specifications
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        //public static DataTable GetPortContractVehicleServicesWithDetails(string ContractId)
        //{
        //    try
        //    {
        //        return ContractDAL.GetPortContractVehicleServicesWithDetails(ContractId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/11/2011
        /// Description: load Service Provider contract medical services with details and specifications
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        //public static DataTable GetPortContractMedicalServicesWithDetails(string ContractId)
        //{
        //    try
        //    {
        //        return ContractDAL.GetPortContractMedicalServicesWithDetails(ContractId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/11/2011
        /// Description: load Service Provider contract other services with details and specifications
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        //public static DataTable GetPortContractOtherServicesWithDetails(string ContractId)
        //{
        //    try
        //    {
        //        return ContractDAL.GetPortContractOtherServicesWithDetails(ContractId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

       
        #endregion

        #region PORT CONTRACT HOTEL
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/11/2011
        /// Description: load Service Provider hotel service details
        /// ------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="contractPortServiceId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //public static IDataReader LoadPortContractHotelDetails(string contractPortServiceId, string UserId)
        //{
        //    try
        //    {
        //        return ContractDAL.LoadPortContractHotelDetails(contractPortServiceId, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// Description: Save Service Provider Contract hotel
        /// -------------------------------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 08/12/2011
        /// Description: chenge stored procedure, removed other parameters
        /// </summary>
        /// <param name="contractPortAgentVendorId"></param>
        /// <param name="BrandId"></param>
        /// <param name="BranchId"></param>
        /// <param name="PortId"></param>
        /// <param name="VendorType"></param>
        /// <param name="MealRate"></param>
        /// <param name="MealRateTax"></param>
        /// <param name="MealRateTaxInc"></param>
        /// <param name="withBreakfast"></param>
        /// <param name="withLunch"></param>
        /// <param name="withDinner"></param>
        /// <param name="withLunchOrDinner"></param>
        /// <param name="withShuttle"></param>
        /// <param name="UserId"></param>
        public static Int32 SaveContractPortAgentVendorHotel(string contractBranchId, string BrandId, string BranchId,
            string PortId, string MealRate, string MealRateTax, bool MealRateTaxInc, bool withBreakfast,
            bool withLunch, bool withDinner, bool withLunchOrDinner, bool withShuttle, string UserId, string ContractId)
        {
            try
            {
                return ContractDAL.SaveContractPortAgentVendorHotel(contractBranchId,BrandId, BranchId, PortId, MealRate,
                    MealRateTax, MealRateTaxInc, withBreakfast, withLunch, withDinner, withLunchOrDinner, withShuttle, UserId, ContractId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/12/2011
        /// Description: save Service Provider contract hotel rooms
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ContractBranchId"></param>
        /// <param name="contractId"></param>
        public static void SaveContractPortAgentHotelRooms(DataTable dt, Int32 ContractBranchId, Int32 contractId)
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime dateFrom = DateTime.Parse(row["DateFrom"].ToString());
                    DateTime dateTo = DateTime.Parse(row["DateTo"].ToString());
                    String roomRate = row["RoomRate"].ToString();
                    Int32 currency = Int32.Parse(row["CurrencyId"].ToString());
                    Decimal roomTax = Decimal.Parse(row["RoomTax"].ToString());
                    Boolean taxInclusive = Boolean.Parse(row["RoomTaxInclusive"].ToString());
                    Int32 roomType = Int32.Parse(row["RoomTypeId"].ToString());
                    Int32 Mon = Int32.Parse(row["Mon"].ToString());
                    Int32 Tue = Int32.Parse(row["Tue"].ToString());
                    Int32 Wed = Int32.Parse(row["Wed"].ToString());
                    Int32 Thu = Int32.Parse(row["Thu"].ToString());
                    Int32 Fri = Int32.Parse(row["Fri"].ToString());
                    Int32 Sat = Int32.Parse(row["Sat"].ToString());
                    Int32 Sun = Int32.Parse(row["Sun"].ToString());

                    ContractDAL.SaveContractPortAgentHotelRooms(ContractBranchId, contractId,
                        dateFrom, dateTo, roomRate, currency, roomTax, taxInclusive,
                        roomType, Mon, Tue, Wed, Thu, Fri, Sat, Sun);
                }
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
        /// Author: Charlene Remotigue
        /// Date Created: 08/12/2011
        /// Description: delete hotel rooms
        /// </summary>
        /// <param name="dt"></param>
        //public static void DeleteContractPortAgentHotelRooms(DataTable dt, string UserId, string Remarks)
        //{
        //    try
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            Int32 contractDetailId = Int32.Parse(row["colContractDetailIdInt"].ToString());
        //            ContractDAL.DeleteContractPortAgentHotelRooms(contractDetailId, UserId, Remarks);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}

       /// <summary>
       /// Author: Charlene Remotigue
       /// Date Created: 13/12/2011
       /// Description: delete port contract hotel
       /// </summary>
       /// <param name="cId"></param>
        //public static void DeleteContractPortAgentHotel(string cId, string UserId, string Remarks)
        //{
        //    try
        //    {
        //        Int32 contractId = Int32.Parse(cId);
        //        ContractDAL.DeleteContractPortAgentHotel(contractId, UserId, Remarks);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: save Service Provider hotel rooms
        /// </summary>
        /// <param name="contractPortServiceId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="Currency"></param>
        /// <param name="serviceRate"></param>
        /// <param name="serviceRateTax"></param>
        /// <param name="serviceTaxInclusive"></param>
        /// <param name="roomType"></param>
        /// <param name="mCount"></param>
        /// <param name="tCount"></param>
        /// <param name="wCount"></param>
        /// <param name="thCount"></param>
        /// <param name="fCount"></param>
        /// <param name="sCount"></param>
        /// <param name="suCount"></param>
        /// <param name="UserID"></param>
        //public static void SaveContractPortAgentHotelSpecifications(string contractPortServiceId, string dateFrom, string dateTo,
        //    string Currency, string serviceRate, string serviceRateTax, bool serviceTaxInclusive, string roomType, string mCount,
        //    string tCount, string wCount, string thCount, string fCount, string sCount, string suCount, string UserID)
        //{
        //    try
        //    {
        //        ContractDAL.SaveContractPortAgentHotelSpecifications(contractPortServiceId, dateFrom, dateTo, Currency, serviceRate,
        //            serviceRateTax, serviceTaxInclusive, roomType, mCount, tCount, wCount, thCount, fCount, sCount, suCount, UserID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: load port contract hotel rooms specifications
        /// </summary>
        /// <param name="contractPortServiceId"></param>
        /// <param name="UserId"></param>
        ///// <returns></returns>
        //public static DataTable LoadPortContractHotelRooms(string contractPortServiceId, string UserId)
        //{
        //    try
        //    {
        //        return ContractDAL.LoadPortContractHotelRooms(contractPortServiceId, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        
        #endregion
 
        #region PORT CONTRACT VEHICLE
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load all added vehicles
        /// </summary>
        /// <param name="contractServiceID"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public static DataTable LoadPortAgentContractVehicles(string contractServiceID, string userId)
        //{
        //    try
        //    {
        //        return ContractDAL.LoadPortAgentContractVehicles(contractServiceID, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

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
            try
            {
                ContractDAL.SaveContractPortAgentVehicleSpecifications(contractPortServiceId, dateFrom, dateTo, Currency,
                    serviceRate, Origin, Destination, VehicleType, Capacity, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                ContractDAL.SaveContractPortAgentVendorVehicle(contractPortAgentServiceId, BrandId, BranchId, PortId, VendorType, userId, ContractId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider contract vehicle details
        /// --------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="contractServiceId"></param>
        /// <returns></returns>
        //public static IDataReader LoadPortContractVehicleDetails(string contractServiceId)
        //{
        //    try
        //    {
        //        return ContractDAL.LoadPortContractVehicleDetails(contractServiceId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        #region MEDICAL
         /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// Description: Save Service Provider contract medical
        /// </summary>
        /// <param name="contractPortAgentVendorId"></param>
        /// <param name="BrandId"></param>
        /// <param name="BranchId"></param>
        /// <param name="PortId"></param>
        /// <param name="VendorType"></param>
        /// <param name="ServiceRate"></param>
        /// <param name="AccomodationDays"></param>
        /// <param name="UserId"></param>
        /// <param name="contractId"></param>
        public static void SaveContractPortAgentMedicalDetails(string contractPortAgentServiceId, 
            string PortId, string VendorType, string ServiceRate, string UserId, string contractId,
            string AccomodationDays, string detailID)
        {
            try
            {
                ContractDAL.SaveContractPortAgentMedicalDetails(contractPortAgentServiceId, PortId, VendorType, ServiceRate,
                       UserId, contractId, AccomodationDays, detailID);
            }
            catch (Exception ex)
            {
                throw ex;
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
        //    string Currency, string serviceRate, string Origin, string Destination,string remarks, string UserID)
        //{
        //    try
        //    {
        //        ContractDAL.SaveContractPortAgentMedicalSpecifications(contractPortServiceId, Currency, serviceRate,
        //            Origin, Destination,remarks, UserID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/11/2011
        /// Description: load Service Provider contract medical service
        /// -------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="ContractPortAgentVendorId"></param>
        /// <returns></returns>
        //public static IDataReader LoadPortAgentMedicalServices(string ContractPortAgentVendorId)
        //{
        //    try
        //    {
        //        return ContractDAL.LoadPortAgentMedicalServices(ContractPortAgentVendorId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
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
        //    try
        //    {
        //        return ContractDAL.LoadPortAgentMedicalSpecifications(serviceId, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion

        #region PORT CONTRACT SERVICES
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 08/11/2011
        /// Description: load all services
        /// </summary>
        /// <returns></returns>
        //public static DataTable LoadPortAgentContractServiceTypes()
        //{
        //    try
        //    {
        //        return ContractDAL.LoadPortAgentContractServiceTypes();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
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
        //    try
        //    {
        //        return ContractDAL.LoadPortContractServices(ContractId, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: delete port contract Service
        /// </summary>
        /// <param name="portContractVendorId"></param>
        /// <param name="UserId"></param>
        //public static void DeletePortContractService(string portContractVendorId, string UserId)
        //{
        //    try
        //    {
        //        ContractDAL.DeletePortContractService(portContractVendorId, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: delete port contract services
        /// </summary>
        /// <param name="PortAgentSpecId"></param>
        /// <param name="userId"></param>
        //public static void DeletePortContractSpecifications(string PortAgentSpecId, string userId)
        //{
        //    try
        //    {
        //        ContractDAL.DeletePortContractSpecifications(PortAgentSpecId, userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region Service Provider CONTRACT
        /// <summary>            
        /// Date Created:   11/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider contract details
        /// </summary>  
        public static void GetPortAgentContractByContractID(string contractId, string branchId, Int16 iLoadType)
        {
            ContractDAL.GetPortAgentContractByContractID(contractId, branchId, iLoadType);
        }
         /// <summary>
        /// Date Created:   11/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport of Service Provider Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentContractAirport(Int32 iContractID, Int32 iPortAgentID , Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            ContractDAL.GetPortAgentContractAirport(iContractID, iPortAgentID, iFilterBy,
                sFilter, isViewExists, iLoadType);
        }
          /// Date Created:   12/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport of Service Provider Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentContractSeaport(Int32 iContractID, Int32 iPortAgentID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            ContractDAL.GetPortAgentContractSeaport(iContractID, iPortAgentID, iFilterBy,
                sFilter, isViewExists, iLoadType);
        }
        /// Date Created:  13/Nov/2013
        /// Created By:    Josephine Gad
        /// (description)  Add dtContractAirport, dtContractSeaport
        ///                Include Audit Trail
        /// --------------------------------------------------
        /// Date Created:  28/Feb/2014
        /// Created By:    Josephine Gad
        /// (description)  Dispose all Tables
        ///                Add dtContractVehicleType and dtDetails
        ///                Add Hotel rate details
        /// --------------------------------------------------
        /// Date Modified: 04/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  Add Meet & Greet Details
        /// --------------------------------------------------
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
            string sFromTime, string sToTime, bool bIsMeetGreetSurchargePercent, decimal dMeetGreetSurchargeFee, string sMeetGreetRemarks,
            //Visa Details
            decimal dVisaAmount, decimal dVisaAccompany, decimal dImmigrationFees, decimal dImmigrationFees2,
            decimal dLetterOfInvitation, decimal dBusinessParoleRequest, decimal dBusinessParoleProcessingFee,
            decimal dImmigrationPortCaptaincyLetter, string sVisaRemarks,
            //Other Details
             decimal dShorePassesRate, decimal dAnsweringTelephoneCallsEmailRate,
            decimal dLostLuggageRate, decimal dCarRate,
            decimal dImmigrationCustodyServiceAirportHotelRate, decimal dImmigrationCustodyServiceHotelRate,
            decimal dImmigrationCustodyServiceHotelShipRate, decimal dTransportationToPharmacyRate,
            decimal dTransportationToMedicalFacilityRate,
            decimal dWaitingTimeRate, string sOtherRemarks)
        {
            try
            {
                ContractDAL.PortAgentInsertContract(iContractID, iPortAgentID,
                    sContractName, sRemarks, sDateStart, sDateEnd, sRCCLPerconnel, sRCCLDateAccepted,
                    sVendorPersonnel, sVendorDateAccepted, iCurrency, bIsAirportToHotel, bIsHotelToShip,
                      bIsRCL, bIsAZA, bIsCEL, bIsPUL,bIsSKS,
                    sUserName, sDescription, sFunction, sFilename,
                    sGMTDate, dtContractAirport, dtContractSeaport, dtContractVehicleType, dtAttachment, dtDetails,
                    bIsRateByPercent, dRoomCostPercent, dRoomRateTaxPercentage, bIsTaxInclusive, dRoomSingleRate, 
                    dRoomDoubleRate, dMealStandard,  dMealIncreased, dMealTax, dSurchargeSingle, dSurchargeDouble, 
                    dBreakfastRate, dLunchRate, dDinnerRate, dMiscellaneaRate, 
                    iUOM, dLuggageRate,
                    dtSafeguardDetails, dMeetGreetRate, sFromTime, sToTime, bIsMeetGreetSurchargePercent, dMeetGreetSurchargeFee, 
                    sMeetGreetRemarks,  
                    //Visa details
                    dVisaAmount, dVisaAccompany, dImmigrationFees, dImmigrationFees2,
                    dLetterOfInvitation, dBusinessParoleRequest, dBusinessParoleProcessingFee,
                    dImmigrationPortCaptaincyLetter, sVisaRemarks,
                    //Other details
                    dShorePassesRate, dAnsweringTelephoneCallsEmailRate, 
                    dLostLuggageRate, dCarRate,
	                dImmigrationCustodyServiceAirportHotelRate,  dImmigrationCustodyServiceHotelRate, 
	                dImmigrationCustodyServiceHotelShipRate, dTransportationToPharmacyRate, 
	                dTransportationToMedicalFacilityRate,   
	                dWaitingTimeRate, sOtherRemarks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
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
            }
        }
        /// <summary>                     
        /// Date Modified:  13/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Get list of Service Provider Contracts
        /// </summary>     
        public static List<ContractPortAgent> GetPortAgentContractByBranchID(int iPortAgentID)
        {
            return ContractDAL.GetPortAgentContractByBranchID(iPortAgentID);
        }

        ///<summary>
        ///Date Created: 05/08/2014
        ///Created By: Michael Evangelista
        ///Description: Activate Seaport
        /// </summary>

        public static void SeaportActivate(string Username,int seaportIDtoActivate,string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            ContractDAL.SeaportActivate(Username, seaportIDtoActivate,sDescription, sFunction, sFilename, GMTDate);
        }

        ///<summary>
        ///Date Created: 05/08/2014
        ///Created By: Michael Evangelista
        ///Description: De-activate Seaport
        /// </summary>
        /// 
        public static void SeaportInActivate(string Username, int seaportIDtoActivate, string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            ContractDAL.SeaportInActivate(Username, seaportIDtoActivate, sDescription, sFunction, sFilename, GMTDate);
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
            ContractDAL.PortAgentCancelContract(ContractID, Username,
             sDescription, sFunction, sFilename, GMTDate);
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

            try
            {
                ContractDAL.SaveContractPortAgentOther(contractPortAgentServiceId, PortId, VendorType, ServiceRate, UserId,
                    contractId, Remarks, detailID, Currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider contract other services details
        /// --------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="contractServiceId"></param>
        /// <returns></returns>
        public static IDataReader LoadPortAgentContractOthers(string contractServiceId)
        {
            try
            {
                return ContractDAL.LoadPortAgentContractOthers(contractServiceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
           
        }
        #endregion
    }
}

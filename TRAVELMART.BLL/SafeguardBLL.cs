using System;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Collections.Generic;
using System.Web;


namespace TRAVELMART.BLL
{
    public class SafeguardBLL
    {
        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Get list of Safeguard Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static List<VendorSafeguardList> SafeguardVendorsGet(string sSafeguardVendorName, string sOrderyBy, int iStartRow, int iMaxRow)
        {
            List<VendorSafeguardList> list = new List<VendorSafeguardList>();
            SafeguardDAL.SafeguardVendorsGet(sSafeguardVendorName, sOrderyBy, iStartRow, iMaxRow);

            list = (List<VendorSafeguardList>)HttpContext.Current.Session["SafeguardVendorList"];
            return list;
        }
        public static int SafeguardVendorsGetCount(string sSafeguardVendorName, string sOrderyBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["SafeguardVendorCount"]);
        }

        /// <summary>
        /// Date Created:   6/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Get Safeguard Vendor Details, Country List and City List
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsGetByID(Int32 iVendorID, Int16 iLoadType)
        {
            SafeguardDAL.SafeguardVendorsGetByID(iVendorID, iLoadType);
        }

        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Save Safeguard Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsSave(Int32 iSafeguardVendorID, string sSafeguardVendorName, Int32 iCountryID,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
            string sEmailCc, string sEmailTo, string sWebsite,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dt, DataTable dtSeaport)
        {
            SafeguardDAL.SafeguardVendorsSave(iSafeguardVendorID, sSafeguardVendorName, iCountryID,
            iCityID, sContactNo, sFaxNo, sContactPerson, sAddress, sEmailCc, sEmailTo, sWebsite,
            UserId, strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, dt, dtSeaport);
        }

        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Marco Abejar
        /// (description)   Get service type
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void ServiceTypeGet(Int32 iContractID, Int32 iSafeguardVendorID,
            bool isViewExists, Int16 iLoadType)
        {
            SafeguardDAL.ServiceTypeGet(iContractID, iSafeguardVendorID, isViewExists, iLoadType);
        }

        /// <summary>            
        /// Date Created: 07/Nov/2013
        /// Created By: Marco Abejar
        /// (description) Insert vendor service contract  
        /// --------------------------------------------------
        /// </summary> 
        public static void SafeguardInsertContract(int iContractID, int iSafeguardVendorID,
            string sContractName, string sRemarks, string sDateStart,
            string sDateEnd, string sRCCLPerconnel, string sRCCLDateAccepted,
            string sVendorPersonnel, string sVendorDateAccepted, int iCurrency,
            string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtContractServiceType,
            DataTable dtAttachment, DataTable dtDetails
            )
        {
            SafeguardDAL.SafeguardInsertContract(iContractID, iSafeguardVendorID,
            sContractName, sRemarks, sDateStart, sDateEnd, sRCCLPerconnel, sRCCLDateAccepted,
            sVendorPersonnel, sVendorDateAccepted, iCurrency, sUserName, sDescription, sFunction, sFilename,
            sGMTDate, dtContractServiceType, dtAttachment, dtDetails);
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
        public static List<ContractSafeguard> GetVendorSafeguardBranchContractByBranchID(string SafeguardBranchID)
        {
            return SafeguardDAL.GetVendorSafeguardBranchContractByBranchID(SafeguardBranchID);
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
        /// Date Created: 14/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) update vehicle contract flag 
        /// ------------------------------------------------
        /// Date Modified:  26/Sep/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add audit trail fields
        /// </summary>  
        public static void UpdateSafeguardContractFlag(Int32 ContractID, string Username,
            string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            SafeguardDAL.UpdateSafeguardContractFlag(ContractID, Username, sDescription, sFunction, sFilename, GMTDate);
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
        public static void GetVendorSafeguardContractByContractID(string contractId, string branchId, Int16 iLoadType)
        {
            SafeguardDAL.GetVendorSafeguardContractByContractID(contractId, branchId, iLoadType);            
        }

        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport of Vendor Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsSeaportGet(Int32 iVendorID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            SafeguardDAL.SafeguardVendorsSeaportGet(iVendorID, iFilterBy, sFilter, isViewExists, iLoadType);
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type of company
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void SafeguardVendorsServiceTypeGet(Int32 iContractID, Int32 iSafeguardVendorID,
            bool isViewExists, Int16 iLoadType)
        {
            SafeguardBLL.SafeguardVendorsServiceTypeGet(iContractID, iSafeguardVendorID, isViewExists, iLoadType);
        }
        /// <summary>            
        /// Date Created: 13/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting list of safeguard branch contract pending
        /// </summary>  
        public static DataTable GetVendorSafeguardBranchPendingContract()
        {
            DataTable dt = null;
            try
            {
                dt = SafeguardDAL.GetVendorSafeguardBranchPendingContract();
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
        /// Date Created: 12/Nov/2013
        /// Created By: Marco Abejar
        /// (description) check if safguard contract is active and approved
        /// </summary>  
        public static Int32 GetVendorSafeguardBranchContractActiveByContractID(Int32 contractID)
        {
            Int32 Counter = 0;
            try
            {
                Counter = SafeguardDAL.GetVendorSafeguardBranchContractActiveByContractID(contractID);
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
        /// Date Created: 14/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) update vehicle contract status 
        /// </summary>  
        public static void UpdateSafeguardContractStatus(Int32 ContractID, string Username)
        {
            SafeguardDAL.UpdateSafeguardContractStatus(ContractID, Username);
        }

        public static List<ContractSafeguardAttachment> GetSafeguardContractAttachment(int contractId)
        {
            List<ContractSafeguardAttachment> list = new List<ContractSafeguardAttachment>();          

            list =  SafeguardDAL.GetSafeguardContractAttachment(contractId);
            return list;
        }
    }
}


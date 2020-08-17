using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class PortBLL
    {
        public static PortDAL dbDAL = new PortDAL();

        /// <summary>            
        /// Date Created: 05/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting port list
        /// </summary>  
        public static DataTable GetPortList()
        {           
            DataTable dt = null;
            try
            {
                dt = PortDAL.GetPortList();
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
        /// Date Created:   18/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Get port list by Region and/or Country
        /// ------------------------------------------------
        /// Date Modified:   15/08/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter userRoleString
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="regionString"></param>
        /// <param name="countryString"></param>
        /// <returns></returns>
        public static List<PortList> GetPortListByRegionCountry(string userString, string regionString, string countryString, string userRoleString)
        {
            try
            {
                List<PortList> list = new List<PortList>();
                list = PortDAL.GetPortListByRegionCountry(userString, regionString, countryString, userRoleString);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
         /// <summary>
        /// Date Created:   19/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Get port list by Region
        /// ------------------------
        /// </summary>
        public static List<PortList> GetPortListByRegion(string userString, string regionString, string userRoleString, string sPortName)
        {
            try
            {
                List<PortList> list = new List<PortList>();
                list = PortDAL.GetPortListByRegion(userString, regionString, userRoleString, sPortName);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }
        /// <summary>
        /// Date Created:   26/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get port list by City
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public static DataTable GetPortListByCity(string userString, string cityString)
        {
            DataTable dt = null;
            try
            {
                dt = PortDAL.GetPortListByCity(userString, cityString);
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
        /// Date Created: 05/08/2011
        /// Created By: Marco Abejar
        /// (description) Get port transaction details
        /// --------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader GetSFPortTransDetails(string sfCode, string sfPID, string sfStatus,
             string TravelReqId, string ManualRequestID)
        {          
            IDataReader dt = null;
            try
            {
                dt = PortDAL.GetSFPortTransDetails(sfCode, sfPID, sfStatus, TravelReqId, ManualRequestID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>            
        /// Date Created:   05/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Insert or update port transaction status
        /// ==========================================================
        /// Date Modified:  12/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add paramters Travel Req ID, Manual Request ID and others
        /// </summary>  
        //public static void InsertSFPortDetails(string sfID, string PortTransStatus, string PortTransDate, string SFStatus, string PortID, string SFPID, string RecLoc)
        public static Boolean InsertUpdatePortStatus(string PortAgentTransId, string TravelReqId,
               string ManualRequestID, string SeafarerId, string PortAgentId,
               string PortId, string PortAgentStatus, string UserName, string SFStatus, string PortTransactionDate, string ContractId)
        {
            return PortDAL.InsertUpdatePortStatus(PortAgentTransId, TravelReqId, ManualRequestID,
                SeafarerId, PortAgentId, PortId, PortAgentStatus, UserName, SFStatus, PortTransactionDate, ContractId);
        }

        /// <summary>            
        /// Date Created: 05/08/2011
        /// Created By: Marco Abejar
        /// (description) Delete port transaction
        /// </summary> 
        /// 
        public static void DeletePortTransaction(Int32 colPortAgentTransIdInt, string DeletedBy)
        {           
            PortDAL.DeletePortTransaction(colPortAgentTransIdInt, DeletedBy);
        }

        /// <summary>            
        /// Date Created: 10/08/2011
        /// Created By: Marco Abejar
        /// (description) Add new port 
        /// ----------------------------------------------
        /// Date Edited:   18/07/2012
        /// Created By:     Jefferson Bermundo
        /// (description)   Remove pPortId ; returnd always 0 instead of the new port Code.
        ///                 Add Port Code to data being save or updated
        /// </summary> 
        public static Int32 AddNewPort(string PortName, Int32? PortCity, Int32 PortCountry, string CreatedBy, Int32 PortID, string portCode)
        {
            //Int32 pPortID = 0;
            PortID = PortDAL.AddNewPort(PortName, PortCity, PortCountry, CreatedBy, PortID, portCode);
            return PortID;
        }

        /// <summary>
        /// Date Edited:    19/07/2012
        /// Created By:     Jefferson Bermundo
        /// (description)   Check Port Code If Existing.                 
        /// </summary>
        /// <param name="portCode"></param>
        /// <returns></returns>
        public static bool CheckPortCode(string portCode, int portId)
        {
            return PortDAL.CheckPortCode(portCode, portId);
        }

        /// <summary>            
        /// Date Created: 10/08/2011
        /// Created By: Marco Abejar
        /// (description) Get port to edit / modify
        /// -----------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static IDataReader GetPortToEdit(Int32 PortID)
        {           
            IDataReader dt = null;
            try
            {
                dt = PortDAL.GetPortToEdit(PortID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>            
        /// Date Created: 23/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting port company list
        /// </summary> 
        public static DataTable GetPortCompanyList()
        {            
            DataTable dt = null;
            try
            {
                dt = PortDAL.GetPortCompanyList();
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
        /// Date Created: 23/08/2011
        /// Created By: Marco Abejar
        /// (description) Get port company to edit / modify
        /// --------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static IDataReader GetPortCompanyToEdit(Int32 PortCompanyID)
        {           
            IDataReader dt = null;
            try
            {
                dt = PortDAL.GetPortCompanyToEdit(PortCompanyID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>            
        /// Date Created: 05/08/2011
        /// Created By: Marco Abejar
        /// (description) Add new port company
        /// </summary> 
        public static Int32 AddNewPortCompany(Int32 PortCompanyID, String CompanyName, String Address, Int32 CountryID, String ContactPerson, String ContactNo, String User)
        {
            Int32 PortAgentCompanyID = 0;
            PortAgentCompanyID = PortDAL.AddNewPortCompany(PortCompanyID, CompanyName, Address, CountryID, ContactPerson, ContactNo, User);
            return PortAgentCompanyID;
        }

        /// <summary>            
        /// Date Created: 31/08/2011
        /// Created By: Marco Abejar
        /// (description) Add new Service Provider
        /// </summary> 
        public static Int32 AddNewPortAgent(string PortAgentIdInt, string PortAgentName, string PortCompanyID, String LName, String FName, String MName, String Address,
            String ContactNo, String Email, String ContactPerson, String ContactPersonPhone,
            String ContactPersonEmail, string UserString)
        {
            Int32 PortAgentID = 0;
            PortAgentID = PortDAL.AddNewPortAgent(PortAgentIdInt, PortAgentName, PortCompanyID, LName, FName, MName, Address, ContactNo, Email, ContactPerson, ContactPersonPhone,
            ContactPersonEmail, UserString);
            return PortAgentID;
        }

         /// <summary>
        /// Date Created:   05/10/2011
        /// Created By:     Josephine gad
        /// (description)   Get list of Service Provider by port id
        /// ------------------------------------
        /// Date Modified:  14/02/2012
        /// Modified By:    Josephine gad
        /// (description)   Change return value from DataTable to List
        /// ------------------------------------        
        /// </summary>
        /// <param name="PortID"></param>
        /// <returns></returns>
        //public static DataTable GetPortAgentByPortID(string PortID, string PortAgentID)        
        public static List<PortAgentDTO> GetPortAgentByPortID(string PortID, string PortAgentID)
        {
            return PortDAL.GetPortAgentByPortID(PortID, PortAgentID);
            //DataTable dt = null;
            //try
            //{
            //    dt = PortDAL.GetPortAgentByPortID(PortID, PortAgentID);
            //    return dt;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }

        /// <summary>            
        /// Date Created: 20/10/2011
        /// Created By: Charlene Remotigue
        /// (description) load Service Provider details
        /// </summary>     
        public static IDataReader GetPortAgentDetails(string portAgentId)
        {
            try
            {
                return PortDAL.GetPortAgentDetails(portAgentId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

       
        /// Author: Charlene Remotigue
        /// Date Created: 02/11/2011
        /// Description: get Vendor brand with no contracts by port 
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="VendorType"></param>
        /// <returns></returns>
        public static DataTable getVendorBrandByPort(string portAgentId, string VendorType, string ContractId, string contractBanchId)
        {
            try
            {
                return PortDAL.getVendorBrandByPort(portAgentId, VendorType, ContractId, contractBanchId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 02/11/2011
        /// Deascription: get vendor branch with no contracts by vendor brand Id
        /// </summary>
        /// <param name="VendorId"></param>
        /// <param name="VendorType"></param>
        /// <returns></returns>
        public static DataTable getVendorBranchbyVendorBrand(string VendorId, string VendorType, string contractBranchId, string ContractId)
        {
            try
            {
                return PortDAL.getVendorBranchbyVendorBrand(VendorId, VendorType, contractBranchId, ContractId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       

       //  /// <summary>
       ///// Author:Charlene Remotigue
       ///// Date Created" 03/11/2011
       ///// Description: get added vendor brand
       ///// </summary>
       ///// <param name="ContractId"></param>
       ///// <param name="VendorType"></param>
       ///// <param name="UserId"></param>
       ///// <returns></returns>
       // public static DataTable getAddedVendorBrand(string ContractId, int VendorType, string UserId)
       // {
       //     try
       //     {
       //         return PortDAL.getAddedVendorBrand(ContractId, VendorType, UserId);
       //     }
       //     catch (Exception ex)
       //     {
       //         throw ex;
       //     }
       // }

        // /// <summary>
        ///// Author: Charlene Remotigue
        ///// Date Created: 03/11/2011
        ///// Description: get added vendor Branch
        ///// </summary>
        ///// <param name="VendorBrand"></param>
        ///// <param name="ContractId"></param>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //public static DataTable getAddedVendorBranch(string VendorBrand, string ContractId, string UserId)
        //{
        //    try
        //    {
        //        return PortDAL.getAddedVendorBranch(VendorBrand, ContractId, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static void GetPortForNonTurn(string sPortName, string sPortCode, string sPortID, DateTime dDate)
        {
            PortDAL.GetPortForNonTurn(sPortName, sPortCode, sPortID, dDate);
        }
        /// <summary>
        /// Date Created:   04/Apr/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Port with incompete details in TM
        /// </summary>      
        public static void GetPortForNotInTM(string sPortName, string sPortCode, DateTime dDate)
        {
            PortDAL.GetPortForNotInTM(sPortName, sPortCode, dDate);

        }

        /// Date Modified:   15/08/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter userRoleString
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="regionString"></param>
        /// <param name="countryString"></param>
        /// <returns></returns>
        public static List<PortAgentVendorList> GetPortAgentVendor(int vendorid)
        {
            try
            {
                List<PortAgentVendorList> list = new List<PortAgentVendorList>();
                list = PortDAL.GetPortAgentVendor(vendorid);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

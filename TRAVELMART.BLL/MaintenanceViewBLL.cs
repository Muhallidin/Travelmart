using System;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Collections.Generic;
using System.Web;

namespace TRAVELMART.BLL
{
    public class MaintenanceViewBLL
    {
        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel vendor 
        /// </summary>  
        public static DataTable GetHotelVendorList(string strHotelName, string strUser, string strMapRef)
        {           
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetHotelVendorList(strHotelName, strUser, strMapRef);
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
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel vendor 
        /// ----------------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting parameter
        /// </summary>  
        public static DataTable GetHotelVendorListByUser(string strHotelName, string strUser, string strMapRef, string VendorID, string UserRole)//, string OrderBy)
        {
            DataTable dt = null;
            try
            {
                Int32 VendorIDInt = Convert.ToInt32(VendorID == "" ? "0" : VendorID);
                dt = MaintenanceViewDAL.GetHotelVendorListByUser(strHotelName, strUser, GlobalCode.Field2Int(strMapRef).ToString(), VendorIDInt, UserRole);//, OrderBy);
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
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel vendor branch
        /// </summary>  
        public static DataTable GetHotelVendorBranchList(string strHotelName, string strUser, string strMapRef, string Region, string Country, 
                                                        string City, string Port, string Hotel)
        {           
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetHotelVendorBranchList(strHotelName, strUser, strMapRef, Region, Country, City,
                                                                 Port, Hotel);
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
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of hotel vendor branch
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
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetHotelVendorBranchListByUser(strHotelName, strUser, Region, Country, Airport,
                                                                 Port, Hotel, UserRole, SortByBranch, SortByPriority);//, OrderBy);
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
        /// Created By:   Gabriel Oquialda
        /// (description) Selecting list of vehicle vendor branch
        /// </summary>  
        public static DataTable GetVehicleVendorBranchList(string strVehicleName, string strUser, string strMapRef, string Region,
            string Country, string City, string Port, string Hotel)
        {
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetVehicleVendorBranchList(strVehicleName, strUser, strMapRef, Region, Country, City,
                                                                   Port, Hotel);
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
        /// Created By:     Gabriel Oquialda
        /// (description)   Selecting list of vehicle vendor branch
        /// ----------------------------------------------
        /// Date Modified:  28/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete strMapRef parameter
        /// ----------------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting parameter
        /// </summary>  
        public static DataTable GetVehicleVendorBranchListByUser(string strVehicleName, string strUser,  string Region,
            string Country, string City, string Port, string Hotel, string UserRole)//, string OrderBy)
        {            
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetVehicleVendorBranchListByUser(strVehicleName, strUser, Region, Country, City,
                                                                   Port, Hotel, UserRole);//, OrderBy);
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
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Delete hotel vendor 
        /// </summary>  
        public static void DeleteHotelVendor(int HotelVendorID, string User)
        {          
            MaintenanceViewDAL.DeleteHotelVendor(HotelVendorID, User);
        }

        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of vehicle vendor 
        /// </summary>  
        public static DataTable GetVehicleVendorList(string strVehicleName, string strUser, string strMapRef)
        {            
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetVehicleVendorList(strVehicleName, strUser, strMapRef);
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

        ///<summary>
        /// Date Created: 04/08/2014
        /// Created By: Michael Brian C. Evangelista
        /// Description: get list for seaports for seaport
        /// </summary>
        public static List<SeaportActivation> SeaportGetList(Int16 LoadType, int RegionID, int SeaportID, string SeaportName, string sOrderBy, string sUserID, int iMaxRow, int iStartRow)
       {
           List<SeaportActivation> list = new List<SeaportActivation>();
           if(LoadType == 0)
           {
               MaintenanceViewDAL.SeaportGetList(LoadType, RegionID, SeaportID, SeaportName, sOrderBy, sUserID, iStartRow, iMaxRow);
           }
           list = (List<SeaportActivation>)HttpContext.Current.Session["SeaportActiveList"];
           return list;
       }

        public static int SeaportGetListCount(Int16 LoadType, int RegionID, int SeaportID, string SeaportName, string sOrderBy, string sUserID)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["SeaportActiveCount"]);
            
        }
        
        /// <summary>
        /// Date Created:   02/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get list of Vehicle Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static List<VendorVehicleList> VehicleVendorsGet(Int16 iLoadType, int iRegionID, int iSeaportID, Int16 iBrandID,
            string sVehicleVendorName, string sOrderyBy, string sUserID, int iStartRow, int iMaxRow)
        {
            List<VendorVehicleList> list = new List<VendorVehicleList>();

            if (iLoadType == 0)
            {                
                MaintenanceViewDAL.VehicleVendorsGet(iLoadType, iRegionID, iSeaportID, iBrandID, sVehicleVendorName,
                    sOrderyBy, sUserID, iStartRow, iMaxRow);
            }
            list = (List<VendorVehicleList>)HttpContext.Current.Session["VehicleVendorList"];
            return list;
        }

        public static int VehicleVendorsGetCount(Int16 iLoadType, int iRegionID, int iSeaportID, Int16 iBrandID, 
            string sVehicleVendorName, string sOrderyBy, string sUserID)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["VehicleVendorCount"]);
        }



        /// <summary>            
        /// Date Created: 16/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of vehicle vendor by user
        /// ----------------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting parameter
        /// </summary>  
        //public static DataTable GetVehicleVendorListByUser(string strVehicleName, string strUser, string strMapRef, string VendorID, string UserRole)//, string OrderBy)
        //{            
        //    DataTable dt = null;            
        //    try
        //    {
        //        dt = MaintenanceViewDAL.GetVehicleVendorListByUser(strVehicleName, strUser, strMapRef, VendorID, UserRole);//, OrderBy);
        //        return dt;
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

        public static void DeleteVehicleVendor(int VehicleVendorID, string User)
        {
            MaintenanceViewDAL.DeleteVehicleVendor(VehicleVendorID, User);
        }
        public static void DeleteVehicleTypeBranch(int VehicleID, string User)
        {
            MaintenanceViewDAL.DeleteVehicleTypeBranch(VehicleID, User);
        }
        /// <summary>
        /// Date Modified:  07/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter sRole,  RegionID, StartRow, MaxRow and LoadType
        ///                 Change DataTable to List
        /// ---------------------------------------------
        /// Date Created:   15/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Add sorting 
        /// </summary>
        /// <param name="strPortName"></param>
        /// <param name="CountryID"></param>
        /// <param name="strUser"></param>
        /// <param name="strMapRef"></param>
        /// <returns></returns>
        public static List<SeaportAirport> GetPortList(string strPortName, Int32 CountryID, string strUser, string sRole,
             int RegionID, int PortAgentID, int StartRow, int MaxRow, Int16 LoadType, string OrderBy)
        {
            List<SeaportAirport> list = null;
            try
            {
                list = MaintenanceViewDAL.GetPortList(strPortName, CountryID, strUser, sRole,
                    RegionID,  PortAgentID,  StartRow,  MaxRow,  LoadType, OrderBy);
                SeaportAirport.SeaportAirportCount = SeaportAirport.SeaportAirportCount;
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }
        }
       /// <summary>
       /// Date Modified: July 31, 2014
       /// Author: Michael Brian C. Evangelista
       /// </summary>
        public static int GetPortListCount(string strPortName, Int32 CountryID, string strUser, string sRole,
             int RegionID, int PortAgentID, Int16 LoadType)
        {
            return SeaportAirport.SeaportAirportCount;  
        }
        public static void DeletePort(int PortID, string User)
        {
            MaintenanceViewDAL.DeletePort(PortID, User);
        }
        public static DataTable GetPortCompanyList(string strPortName, string strUser, string strMapRef)
        {
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetPortCompanyList(strPortName, strUser, strMapRef);
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
        /// Date Modified:  05/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Replace parameter to sPortAgentVendor, 
        ///                 Delete @ppMapIDInt param
        ///                 Change DataTable to List
        /// -------------------------------------------
        /// </summary>        
        //public static DataTable GetPortAgentList(string strAgentName, string strUser, string strMapRef)
        //{
        public static List<VendorPortAgentList> GetPortAgentList(string sUserID, string sRole, 
            string sPortAgentVendor, string sOrder, 
            int iRegionID, int iPortID,  int iStartRow, int iMaxRow)
        {
            return MaintenanceViewDAL.GetPortAgentList(sUserID, sRole, sPortAgentVendor, sOrder, iRegionID, iPortID, iStartRow, iMaxRow);            
        }
        /// Date Modified:  05/Nov/2013
        /// Modified By:    Josephine Gad
        /// (description)   Replace parameter to sPortAgentVendor, 
        ///                 Delete @ppMapIDInt param
        ///                 Change DataTable to List
        /// -------------------------------------------
        /// </summary>        
        //public static DataTable GetPortAgentList(string strAgentName, string strUser, string strMapRef)
        //{
        public static int GetPortAgentCount(string sUserID, string sRole, string sPortAgentVendor, string sOrder, int iRegionID, int iPortID)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["VendorPortAgentCount"]);
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
            return MaintenanceViewDAL.GetPortAgentListByAirportBrand(sPortAgentVendor, sOrder,
            iRegionID, iAirportID, iBrandID, iStartRow, iMaxRow);
        }
        public static int GetPortAgentListByAirportBrandCount(string sPortAgentVendor, string sOrder, int iRegionID, int iAirportID, int iBrandID)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["VendorPortAgentCount"]);
        }
        ///// <summary>
        ///// Date Created:   12/09/2011
        ///// Created By:     Josephine Gad
        ///// (description)   Get Service Provider details
        ///// -----------------------------------------
        ///// Date Modified: 28/11/2011
        ///// Modified By:   Charlene Remotigue
        ///// (description)  optimization (use datareader instead of datatable
        ///// </summary>        
        ////public static IDataReader GetPortAgent(string portAgentIDString)
        ////{
        ///// ------------------------------------------
        ///// Date Modified: 05/Nov/2013
        ///// Modified By:   Josephine Gad
        ///// (description)  Rename GetPortAgent to GetPortAgentByID
        /////                Change IDataReader to void
        ///// </summary>        
        //public static void GetPortAgentByID(int iPortAgentID, Int16 iLoadType)
        //{
        //    MaintenanceViewDAL.GetPortAgentByID(iPortAgentID, iLoadType);
        ////    IDataReader dt = null;
        ////    try
        ////    {
        ////        dt = MaintenanceViewDAL.GetPortAgent(portAgentIDString);
        ////        return dt;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
           
        //}
        /// <summary>            
        /// Date Created: 20/10/2011
        /// Created By:   Gabriel Oquialda
        /// (description) Get hotel events list
        /// </summary>  
        public static DataTable GetHotelEventsList(Int32 branchId, Int32 cityId, DateTime OnOffDate)
        {
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewDAL.GetHotelEventsList(branchId, cityId, OnOffDate);
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
        /// Date Created:   15/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Department list
        /// ------------------------------------------    
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ------------------------------------------    
        /// </summary>        
        public static List<Department> GetDepartment()
        {
            return MaintenanceViewDAL.GetDepartment();
        }
        /// <summary>
        /// Date Created:  21/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get stripes
        /// ---------------------------------
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void
        /// ---------------------------------
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change void to List<Stripe>
        /// ---------------------------------
        /// </summary>        
        public static List<Stripe> GetStripes()
        {
            try
            {
                return MaintenanceViewDAL.GetStripes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>
        /// Date Created:  22/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get list of Ranks
        /// </summary>        
        public static DataTable GetRanks(string stripe, string RankName)
        {
            try
            {
                return MaintenanceViewDAL.GetRanks(stripe, RankName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  26/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get list of Stripes and Room Type
        /// </summary>        
        public static DataTable GetStripesRoomType()
        {
            try
            {
                return MaintenanceViewDAL.GetStripesRoomType();
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                DataTable dtStripeRoom = null;
                return dtStripeRoom = MaintenanceViewDAL.SaveStripeRoomType(StripeRoomIDInt, StripesFloat, RoomIDInt,
                DateEffective, ContractLength, CreatedByVarchar);                
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                MaintenanceViewDAL.DeleteStripeRoomType(StripeRoomIDInt, DeletedByVarchar);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
       /// Date Created:  28/12/2011
       /// Created By:    Josephine Gad
       /// (description)  Update Hotel branch priority no.
       /// </summary>
       /// <param name="BranchId"></param>
       /// <param name="sPriority"></param>
       /// <param name="sUser"></param>
        public static Int32 SaveHotelPriority(string AirportId, string BranchId, string sPriority, string sUser, string sRoomType)
        {
            try
            {
                Int32 AirportHotelID = 0;
                AirportHotelID = MaintenanceViewDAL.SaveHotelPriority(AirportId, BranchId, sPriority, sUser, sRoomType);
                return AirportHotelID;
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// </summary>   
        public static DataTable GetHotelVendorBranchListByUserWithCount(string strHotelName,
                string strUser, string Region, string Country,
                string Airport, string Port, string Hotel, string UserRole,
                string LoadType, Int32 StartRow, Int32 MaxRow, string SortBy, string OrderBy, Int16 iRoomType)
        {
            try
            {
                return MaintenanceViewDAL.GetHotelVendorBranchListByUserWithCount(strHotelName,
                 strUser,  GlobalCode.Field2Int(Region), GlobalCode.Field2Int(Country), 
                 GlobalCode.Field2Int(Airport), GlobalCode.Field2Int(Port), 
                 GlobalCode.Field2Int(Hotel), UserRole, GlobalCode.Field2TinyInt(LoadType),
                 StartRow, MaxRow, SortBy, OrderBy, iRoomType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   24/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of hotel vendor branch by UserID and role with count
        /// ---------------------------------------------------------------        
        /// </summary>   
        public static Int32 GetHotelVendorBranchListByUserCount(string strHotelName,
                string strUser, string Region, string Country,
                string Airport, string Port, string Hotel, string UserRole, string LoadType, string SortBy, string OrderBy, Int16 iRoomType)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["HotelAirportView_TotalCount"]);
            //DataTable dt = null;
            //try
            //{
            //    dt = MaintenanceViewDAL.GetHotelVendorBranchListByUserWithCount(strHotelName,
            //     strUser, GlobalCode.Field2Int(Region), GlobalCode.Field2Int(Country),
            //     GlobalCode.Field2Int(Airport), GlobalCode.Field2Int(Port),
            //     GlobalCode.Field2Int(Hotel), UserRole, 1, 0, 0, SortBy, OrderBy);
            //    if (dt.Rows.Count > 0)
            //    {
            //        Int32 iCount = (Int32)dt.Rows[0]["TotalRowCount"];
            //        return iCount;
            //    }
            //    else
            //    {
            //        return 0;
            //    }
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
            return MaintenanceViewDAL.GetAirportList(sUser, sRole, PortID, RegionID, IsViewExist);
        }
        /// <summary>
        /// Date Created:   28/May/2013
        /// Created By:     Josephine Gad
        /// (description)   Get list of Aiport By Region and Seaport
        /// ---------------------------------------------------------------               
        public static List<Airport> GetAirportListByRegionBySeaport(string sUser, string sRole, Int32 PortID, Int32 RegionID)
        {
            return MaintenanceViewDAL.GetAirportListByRegionBySeaport(sUser, sRole, PortID, RegionID);

        }
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Remove Airport in Seaport
        /// </summary>
        public static void RemoveAirportInSeaport(string sUser, string sAirportIDInt, string sPortIDInt,
            string sLogDescription, string sFunction, string sPageName)
        {
            MaintenanceViewDAL.RemoveAirportInSeaport(sUser, sAirportIDInt, sPortIDInt,
                sLogDescription, sFunction, sPageName);
        }
        /// <summary>            
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Insert Airport in Seaport
        /// </summary>
        public static void InsertAirportInSeaport(string sUser, Int32 AirportIDInt, string sPortIDInt,
            string sLogDescription, string sFunction, string sPageName)
        {
            MaintenanceViewDAL.InsertAirportInSeaport(sUser, AirportIDInt, sPortIDInt,
                sLogDescription, sFunction, sPageName);
        }
        /// <summary>            
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Remove Airport in Seaport
        /// </summary>
        public static void RemoveHotelInAirport(string sUser, string sAirportID, string sBranchID,
            string sLogDescription, string sFunction, string sPageName)
        {
            MaintenanceViewDAL.RemoveHotelInAirport(sUser, sAirportID, sBranchID,
            sLogDescription, sFunction, sPageName);
        }
        public  static void RemoveHotelInAirportNoUse()
        {
            return;
        }
         /// <summary>
        /// Date Created:   30/Jun/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get list of Hotel By Brand
        ///                 Get all list necessary for Hotel Airport-Brand Assignment
        /// ---------------------------------------------------------------  
        /// </summary>
        public void GetHotelVendorBranchListByBrand(int iRegionID, int iAirportID, string sAirportName,
            Int16 iRoomType, int iBrandID, string sHotelName, string sOrderBy, Int16 iLoadType,  string sUserID,
            int iStartRow, int iMaxRow)
        {
            MaintenanceViewDAL.GetHotelVendorBranchListByBrand(iRegionID, iAirportID, sAirportName,
            iRoomType, iBrandID, sHotelName, sOrderBy, iLoadType, sUserID,
            iStartRow, iMaxRow);
        }
          /// <summary>            
        /// Date Created:   03/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Save priority of Hotel by Airport-Brand with Audit Trail
        /// ----------------------------------------        
        /// </summary>
        public static void BrandAirportHotelSavePriority(
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtBrandAirportHotel)
        {
            try
            {
                MaintenanceViewDAL.BrandAirportHotelSavePriority(sUserName, sDescription, sFunction,
                    sFileName, dDateGMT, dtDateCreated, dtBrandAirportHotel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtBrandAirportHotel != null)
                {
                    dtBrandAirportHotel.Dispose();
                }
            }
        }

        ///<summary>
        /// Date Created: 07/07/2014
        /// Created By: Michael Brian C. Evangelista
        /// Description: Save Vehicle Brand Assignment Airport
        ///</summary>

        public static void BrandAirportVehicleSave(int portAgentID, DataTable airports, DataTable brands, string username) {

            try
            {
                MaintenanceViewDAL.BrandAirportVehicleSave(portAgentID,airports, brands,username);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                if (airports != null)
                {
                    airports.Dispose();
                }
                if (brands != null)
                {
                    brands.Dispose();
                }  
          
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
            try
            {
                MaintenanceViewDAL.BrandAirportHotelSave(iHotelID, sUserName, sDescription, sFunction,
                    sFileName, dDateGMT, dtDateCreated, dtBrand, dtAirport);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
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
            MaintenanceViewDAL.GetHotelDetails(iHotelID);
        }


        public List<ImmigrationCompanyGenericClass> GetImmigationCompany(string UserID)
        {
            try
            {
                MaintenanceViewDAL dal = new MaintenanceViewDAL();
                return dal.GetImmigationCompany(UserID); 
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        
        }

          /// <summary>            
        /// Date Created:   04/Jul/2014
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Immiration Company
        /// ----------------------------------------        
        /// </summary>

        public List<ImmigrationCompany> SaveImmigationCompany(int ImmigrationCompanyID, int CountryID, int CityID, string Company, string address, string emailAdd, string Contact, string UserID)
        {

            try
            {
                MaintenanceViewDAL dal = new MaintenanceViewDAL();
                return dal.SaveImmigationCompany(ImmigrationCompanyID, CountryID, CityID , Company , address , emailAdd , Contact , UserID);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                MaintenanceViewDAL dal = new MaintenanceViewDAL();
                return dal.DeleteImmigationCompany(ImmigrationCompanyID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>            
        /// Date Created:   17/Feb/2016
        /// Created By:     Josephine Monteza
        /// (description)   Get the list of Blackout dates
        /// ----------------------------------------        
        /// </summary>
        public List<BlackoutDateList> BlackoutDateGet(Int16 iLoadType, int iBrandID, int iPortID, string dDate,
            string sOrderBy, int iStartRow, int iRowCount)
        {
            MaintenanceViewDAL dal = new MaintenanceViewDAL();
            return dal.BlackoutDateGet(iLoadType, iBrandID, iPortID, dDate, sOrderBy, iStartRow, iRowCount);
        }         
        public int BlackoutDateGetCount(int iLoadType, int iBrandID, int iPortID, string dDate,string sOrderBy)
        {
            int i = GlobalCode.Field2Int(HttpContext.Current.Session["BlackOutDate_RowCount"]);
            return i;
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
            MaintenanceViewDAL.BlackoutDateSave(iBlackoutDateID, iBrandID,  iPort,
                sDateFrom, sDateTo,sUserName, sDescription, sFunction, sFileName,
                dDateGMT, dtDateCreated);
        }
    }
}




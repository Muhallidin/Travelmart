using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class VehicleBLL
    {
        /// <summary>            
        /// Date Created:   15/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle company  
        /// ---------------------------------------
        /// Date Modified:  24/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Dispose DataTable
        /// </summary>            
        public static DataTable vehicleGetCompanyByUser(string Username)
        {           
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetCompanyByUser(Username);
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
        /// Date Created:   15/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle company  
        /// ---------------------------------------
        /// Date Modified:  24/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Dispose DataTable
        /// </summary>            
        public static DataTable vehicleGetCompany()
        {
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetCompany();
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
        ///Date Created: 09/03/2014
        ///Created By: Michael Evangelista
        ///Description: Tag to Vehicle 
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
            DataTable dt = null;
            try
            {

                dt = VehicleDAL.TagtoVehicle(colIDBigint, colTravelReqIDInt, colRecordLocatorVarchar, colSeafarerIdInt,
                    colOnOff, colVehicleVendorIDInt, colPortAgentVendorIDInt, colIsCheck, UserId,
                    sDescription, sFunction, sFileName);
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
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting country list by branch id
        /// </summary>
        public static DataTable CountryListByBranchID(Int32 BranchID)
        {         
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.CountryListByBranchID(BranchID);
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
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting country list by vendor id
        /// </summary>
        public static DataTable CountryListByVendorID(Int32 VendorID)
        {
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.CountryListByVendorID(VendorID);
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
        /// Date Created: 25/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting city list by vendor and country id
        /// </summary>
        public static DataTable CityListByVendorCountryID(Int32 VendorID, Int32 CountryID)
        {         
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.CityListByVendorCountryID(VendorID, CountryID);
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
        /// Date Created: 15/7/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor ID
        /// </summary>
        public static DataTable CityListByVendorID(Int32 VendorID)
        {           
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.CityListByVendorID(VendorID);
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
        /// Date Created: 01/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle brand    
        /// ----------------------------------
        /// Date Modified:  07/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter vendorID
        /// </summary>  
        public static DataTable vehicleGetBranch(Int32 vendorID, string Username)
        {           
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetBranch(vendorID, Username);
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
        /// Date Created:   26/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get all Vehicle Branch
        /// ===================================
        /// </summary>
        /// <returns></returns>
        public static DataTable vehicleGetBranchAll()
        {
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetBranchAll();
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
        /// Date Created:   20/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle branch by vendor, user and city
        /// </summary>
        public static DataTable vehicleGetBranchByVendorUserCity(Int32 vendorID, string Username, Int32 CityID, string Role)
        {
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetBranchByVendorUserCity(vendorID, Username, CityID, Role);
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
        /// Date Created: 01/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle brand            
        /// </summary>  
        public static DataTable vehicleGetBrand(Int32 branchID)
        {            
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetBrand(branchID);
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
        /// Date Created: 26/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle make            
        /// </summary>  
        public static DataTable vehicleGetMake(Int32 branchID)
        {            
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetMake(branchID);
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
        /// Date Created: 12/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle type            
        /// </summary>  
        public static DataTable vehicleGetType(Int32 branchID)
        {            
             DataTable dt = null;
             try
             {
                 dt = VehicleDAL.vehicleGetType(branchID);
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
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type, brand, make year               
        /// </summary>
        public static DataTable vehicleGetTypeBrandMake(Int32 branchID)
        {            
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetTypeBrandMake(branchID);
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
        /// Date Created:   008/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle type luggage van               
        /// </summary>
        public static DataTable vehicleGetTypeLuggageVan(Int32 branchID)
        {
            DataTable dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetTypeLuggageVan(branchID);
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
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get seafarer vehicle transaction  
        /// -------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>       
        public static IDataReader vehicleGetTransaction(int vehiclePrimaryId, int seqNo)
        {                 
             IDataReader dt = null;
             try
             {
                 dt = VehicleDAL.vehicleGetTransaction(vehiclePrimaryId, seqNo);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
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
            IDataReader dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetTransactionByID(VehicleBookingIDString);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
         /// <summary>        
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer vehicle transaction (non-Sabre)
        /// -------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader vehicleGetPendingByID(string PendingVehicleID)
        {
            IDataReader dt = null;
            try
            {
                dt = VehicleDAL.vehicleGetPendingByID(PendingVehicleID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        /// <summary>            
        /// Date Created: 18/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vehicle transaction            
        /// </summary>
        public static void vehicleInsertTransaction(string travelLocatorId, Int32 vendorId, Int32 category, Int32 countryID, Int32 cityId,
            Int32 branchId, Int32 vehicleBrandId, Int32 vehicleMakeId, Int32 vehicleTypeId, //string vehicleYear, string vehiclePlateNo,
            DateTime PickUpDate, string PickUpTime, DateTime DropOffDate, string DropOffTime, string PickUpLocation, string DropOffLocation,
            string vehicleStatus, string createdby, string seafarerStatus, string vehicleRemarks, Boolean vehicleBillToCrew, int seafarerId)
        {                        
            try
            {
                VehicleDAL.vehicleInsertTransaction(travelLocatorId, vendorId, category, countryID, cityId, branchId, vehicleBrandId, vehicleMakeId,
                    vehicleTypeId, //vehicleYear, vehiclePlateNo,
                    PickUpDate, PickUpTime, DropOffDate, DropOffTime, PickUpLocation, DropOffLocation,
                    vehicleStatus, createdby, seafarerStatus, vehicleRemarks, vehicleBillToCrew, seafarerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>            
        /// Date Created:   06/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert vehicle transaction (non Sbare)
        /// </summary>
        public static void vehicleInsertTransactionOther(string TravelReqIDString, string RecordLoc, string RequestID, 
            string VendorIDString, string CountryIDString, string CityIDString, string BranchIDString, string VehicleIDString, 
            string VehiclePlateNoString, string PickUpDate, string PickUpTime, string DropOffDate, string DropOffTime, 
            string PickUpLocationLocationCode, string DropOffLocationLocationCode, string VehicleStatusString, 
            string CreatedByString, string SFStatusString, string RemarksString, bool IsBilledToBool)
        {          
            try
            {
                VehicleDAL.vehicleInsertTransactionOther(TravelReqIDString, RecordLoc, RequestID, VendorIDString, CountryIDString,
                CityIDString, BranchIDString, VehicleIDString, VehiclePlateNoString, PickUpDate,
                PickUpTime, DropOffDate, DropOffTime, PickUpLocationLocationCode, DropOffLocationLocationCode,
                VehicleStatusString, CreatedByString, SFStatusString, RemarksString, IsBilledToBool);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>            
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Vehicle Booking, pending for approval
        ///------------------------------------------------------------------------------- 
        /// </summary>    
        public static void vehicleInsertTransactionPending(string TransVehicleID, string IDBigint, string SeqNo, string TravelReqIDString, 
            string RecordLoc, string RequestID, string VendorIDString, string CountryIDString,
            string CityIDString, string BranchIDString, string VehicleIDString, string VehiclePlateNoString, string PickUpDate,
            string PickUpTime, string DropOffDate, string DropOffTime, string PickUpLocationLocationCode, string DropOffLocationLocationCode,
            string VehicleStatusString, string CreatedByString, string CreatedDate, string ModifiedByString, string ModifiedDate,
            string SFStatusString, string RemarksString, bool IsBilledToBool, string Action)
        {
            try
            {
                VehicleDAL.vehicleInsertTransactionPending(TransVehicleID, IDBigint, SeqNo, TravelReqIDString, RecordLoc, RequestID, VendorIDString,
                    CountryIDString, CityIDString, BranchIDString, VehicleIDString, VehiclePlateNoString, PickUpDate, PickUpTime,
                    DropOffDate, DropOffTime, PickUpLocationLocationCode, DropOffLocationLocationCode, VehicleStatusString,
                    CreatedByString, CreatedDate, ModifiedByString, ModifiedDate,
                    SFStatusString, RemarksString, IsBilledToBool, Action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>            
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Update Pending Vehicle Booking
        ///------------------------------------------------------------------------------- 
        /// </summary>    
        public static void vehicleUpdateTransactionPending(string PendingID, string TransVehicleID, string IDBigint, string SeqNo,
            string TravelReqIDString, string RecordLoc, string RequestID, string VendorIDString, string CountryIDString,
            string CityIDString, string BranchIDString, string VehicleIDString, string VehiclePlateNoString, string PickUpDate,
            string PickUpTime, string DropOffDate, string DropOffTime, string PickUpLocationLocationCode, string DropOffLocationLocationCode,
            string VehicleStatusString, string CreatedByString, string ModifiedDate, string SFStatusString, string RemarksString, bool IsBilledToBool, string Action)
        {
            try
            {
                VehicleDAL.vehicleUpdateTransactionPending(PendingID, TransVehicleID, IDBigint, SeqNo, TravelReqIDString, RecordLoc, RequestID, VendorIDString,
                    CountryIDString, CityIDString, BranchIDString, VehicleIDString, VehiclePlateNoString, PickUpDate, PickUpTime,
                    DropOffDate, DropOffTime, PickUpLocationLocationCode, DropOffLocationLocationCode, VehicleStatusString,
                    CreatedByString, ModifiedDate, SFStatusString, RemarksString, IsBilledToBool, Action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>            
        /// Date Created:   18/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Update vehicle transaction   
        /// ----------------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Remove Brand,Make,Year and vehicle Type and change to vehicleID 
        /// </summary>
        public static void vehicleUpdateTransaction(Int32 vehiclePrimaryId, Int32 seqNo, Int32 vendorId, Int32 countryID,
            Int32 cityId, Int32 branchId, 
            //Int32 vehicleBrandId, Int32 vehicleMakeId, Int32 vehicleTypeId, //string vehicleYear, string vehiclePlateNo,
            Int32 vehicleID,
            DateTime PickUpDate, string PickUpTime, DateTime DropOffDate, string DropOffTime, string PickUpLocation, string DropOffLocation,
            string vehicleStatus, string createdby, string seafarerStatus, string vehicleRemarks, Boolean vehicleBillToCrew)
        {           
            try
            {
                VehicleDAL.vehicleUpdateTransaction(vehiclePrimaryId, seqNo, vendorId, countryID, cityId, branchId, 
                    //vehicleBrandId, vehicleMakeId, vehicleTypeId, //vehicleYear, vehiclePlateNo,
                    vehicleID,
                    PickUpDate, PickUpTime, DropOffDate, DropOffTime,
                    PickUpLocation, DropOffLocation, vehicleStatus, createdby, seafarerStatus, vehicleRemarks, vehicleBillToCrew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void vehicleUpdateTransactionOther(Int32 vehicleBookingID, Int32 vendorId, Int32 countryID, Int32 cityId,
           Int32 branchId, Int32 vehicleID,
           DateTime PickUpDate, string PickUpTime, DateTime DropOffDate, string DropOffTime, string PickUpLocation, string DropOffLocation,
           string vehicleStatus, string createdby, string seafarerStatus, string vehicleRemarks, Boolean vehicleBillToCrew)
        {
            try
            {
                VehicleDAL.vehicleUpdateTransactionOther(vehicleBookingID, vendorId, countryID, cityId, branchId,                    
                    vehicleID,
                    PickUpDate, PickUpTime, DropOffDate, DropOffTime,
                    PickUpLocation, DropOffLocation, vehicleStatus, createdby, seafarerStatus, vehicleRemarks, vehicleBillToCrew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Delete seafarer vehicle transaction            
        /// </summary>    
        public static void vehicleDeleteBooking(Int32 vehiclePrimaryId, int seqInt, string deletedBy)
        {                     
            try
            {
                VehicleDAL.vehicleDeleteBooking(vehiclePrimaryId, seqInt, deletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                VehicleDAL.vehicleDeleteBookingOther(VehicleBookingID, DeletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                VehicleDAL.vehicleDeleteBookingPending(PendingID, DeletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created: 04/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Validate vehicle pick-up date and time
        /// </summary> 
        public static Boolean pickupdatetimeExist(String travelLocatorId, String SeqNo, String branchId, DateTime pickupDate, String pickupTime)
        {            
            Boolean bValidation;

            try
            {
                bValidation = VehicleDAL.pickupdatetimeExist(travelLocatorId, SeqNo, branchId, pickupDate, pickupTime);
                return bValidation;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Date Created: 29/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Remove vehicle type
        /// </summary>
        public static void RemoveVehicleType(string BranchID, string VehicleID, string Username)
        {
            VehicleDAL.RemoveVehicleType(BranchID, VehicleID, Username);
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
            DataTable dTable = null;
            return dTable = VehicleDAL.vehicleApproveTransaction(PendingVehicleID, ApprovedBy);            
        }
        /// <summary>
        /// Date Created:   10/10/2013
        /// Created By:     Marco Abejar
        /// (description)   Get vendor driver
        /// </summary>
        /// <param name="PendingVehicleID"></param>
        /// <param name="ApprovedBy"></param>
        public static List<Driver> vehicleDriver(string VendorId)
        {
            return VehicleDAL.vehicleDriver(VendorId);
        }
        /// <summary>
        /// Date Created:   10/10/2013
        /// Created By:     Marco Abejar
        /// (description)   Get vendor vehicle
        /// </summary>
        /// <param name="PendingVehicleID"></param>
        /// <param name="ApprovedBy"></param>
        public static List<VehicleType> vendorvehicleType()
        {
            return VehicleDAL.vendorvehicleType();
        }
        /// <summary>
        /// Date Created:   10/10/2013
        /// Created By:     Marco Abejar
        /// (description)   Get vendor vehicle plate
        /// </summary>
        /// <param name="PendingVehicleID"></param>
        /// <param name="ApprovedBy"></param>
        public static List<VehiclePlate> vendorvehiclePlate(string vendorID, string VehicleType)
        {
            return VehicleDAL.vendorvehiclePlate(vendorID, VehicleType);
        }
        /// <summary>
        /// Author:         Marco Abejar
        /// Date Created:   10/10/201210
        /// Descrption:     Get Vehicle Manifest by Vendor
        /// =============================================================
        /// </summary>
        public List<VehicleManifestList> GetVehicleManifestByVendor(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
            int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            VehicleDAL.GetVehicleManifestByVendor(dDate, sUserID, iRegionID, iPortID, iVehicleID,
                iStartRow, iMaxRow, iLoadType, sOrderBy);

            return (List<VehicleManifestList>)HttpContext.Current.Session["VehiclManifest_ManifestList"];
        }
        /// <summary>
        /// Date Created:   10/10/2013
        /// Created By:     Marco Abejar
        /// (description)   Get vendor vehicle
        /// </summary>
        /// <param name="PendingVehicleID"></param>
        /// <param name="ApprovedBy"></param>
        public static void vendorAssignVehicle(string status, string IDS, string VehicleTypeID, string driverID, string vehiclePlate, string DispatchTime, string sUserID)
        {
            VehicleDAL.vendorAssignVehicle(status,IDS, VehicleTypeID, driverID, vehiclePlate, DispatchTime, sUserID);
        }
        /// <summary>
        /// Date Created:   10/10/2013
        /// Created By:     Marco Abejar
        /// (description)   Get vendor vehicle
        /// </summary>
        /// <param name="PendingVehicleID"></param>
        /// <param name="ApprovedBy"></param>
        public static void vendorAddDriver(string VendorID, string drivername, string ImageFilename, string sUserID)
        {
            VehicleDAL.vendorAddDriver(VendorID, drivername, ImageFilename, sUserID);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/Oct/2013
        /// Descrption:     Get Vehicle Manifest
        /// =============================================================
        /// </summary>
        public int GetVehicleManifestCountByVendor(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
                Int16 iLoadType, string sOrderBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["VehiclManifest_ManifestCountByVendor"]);
        }
        /// <summary>
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetVehicleVendorAirportList(int iVehicleVendor, Int16 iLoadType)
        {
            return VehicleDAL.GetVehicleVendorAirportList(iVehicleVendor, iLoadType);
        }
        /// Date Created:  14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<SeaportDTO> GetVehicleVendorSeaportList(int iVehicleVendor, Int16 iLoadType)
        {
            return VehicleDAL.GetVehicleVendorSeaportList(iVehicleVendor, iLoadType);
        }
    }
}

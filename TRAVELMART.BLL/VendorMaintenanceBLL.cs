using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class VendorMaintenanceBLL
    {
        VendorMaintenanceDAL DAL = new VendorMaintenanceDAL();
        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vehicle vendor            
        /// </summary>
        public static Int32 vendorMaintenanceInsert(string vendorCode, string vendorName, string vendorType,
            string vendorAddress, Int32 city, Int32 country, string contactno, 
            string createdby, string vendorPrimaryId, string ContactPerson)
        {
            try
            {
                Int32 VendorID = 0;
                VendorID = VendorMaintenanceDAL.vendorMaintenanceInsert(vendorCode, vendorName, vendorType, vendorAddress,
                    city, country, contactno, createdby, vendorPrimaryId, ContactPerson);
                return VendorID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        ///<summary>
        ///Date Created: 27/06/2014
        ///Created By:   Michael Brian C. Evangelista
        ///Description:  Added GetServiceProviderAirportbyBrand
        ///</summary>
        ///
        public static DataTable GetServiceProviderAirportbyBrand(string vendorId) {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.GetServiceProviderAirportbyBrand(vendorId);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { 
            
            
            }
        
        }
        /// <summary>
        /// Date Created: 07/01/2014
        /// Created By: Michael Brian C. Evangelista
        /// Description: Added GetServiceProviderAirportbyVendor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public static DataTable GetServiceProviderAirportbyVendor(string vendorId) {
            DataTable dt = null;

            try
            {
                dt = VendorMaintenanceDAL.GetServiceProviderbyVendor(vendorId);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { 
            
            }
        
        }


        /// <summary>
        /// Date Created:   29/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Insert vendor branch            
        /// =================================
        /// Date Modified:  13/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add IsOn and IsOff parameter
        /// =================================
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change dt to listVoucher, dtRoom to listRoomType
        /// </summary>
        public static Int32 vendorBranchMaintenanceInsert(string vendorName,
                string vendorAddress, Int32 city, Int32 country, string contactno,
                string createdby, string vendorPrimaryId, string ContactPerson, Boolean IsPortAgent, string BranchID, Boolean Rating,
                Boolean Officer, string branchCode, string EmailTo, string EmailCc, bool IsOn, bool IsOff
            , string faxNo, string website, string InstructionON, string InstructionOFF) //Int32 airportID //string Email
        {            
            try
            {
                //decimal Stripes = 0;
                //decimal Amount = 0;
                //int DayNo = 1;
                Int32 BranchIDint = 0;
                //string Room;

                BranchIDint = VendorMaintenanceDAL.vendorBranchMaintenanceInsert(vendorName, vendorAddress,
                city, country, contactno, createdby, vendorPrimaryId, ContactPerson, IsPortAgent, BranchID, 
                Rating, Officer, branchCode, EmailTo, EmailCc, IsOn, IsOff, faxNo, website, InstructionON, InstructionOFF);

                //// List for the voucher                 
                //List<HotelBranchVoucherList> listVoucher= HotelBranchVoucherList.VoucherList;
                //if (listVoucher != null)
                //{
                //    if (listVoucher.Count > 0)
                //    {
                //        for (int i = 0; i < listVoucher.Count; i++) //Save the values one by one
                //        {
                //            Stripes = listVoucher[i].Stripe; //Convert.ToDecimal(dt.Rows[i]["Stripe"].ToString());
                //            Amount = listVoucher[i].Amount;//Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                //            DayNo = listVoucher[i].DayNo;//int.Parse(dt.Rows[i]["DayNo"].ToString());
                //            VendorMaintenanceDAL.InsertHotelBranchVoucher(createdby, Convert.ToString(BranchIDint), Stripes, Amount, DayNo);
                //        }
                //    }
                //}

                //// List for the Room Types
                //List<HotelBranchRoomType> listRoomType = HotelBranchRoomType.RoomTypeList;
                //if (listRoomType != null)
                //{
                //    if (listRoomType.Count > 0)
                //    {
                //        for (int i = 0; i < listRoomType.Count; i++) //Save the values one by one
                //        {
                //            Room = listRoomType[i].colRoomNameVarchar;
                //            VendorMaintenanceDAL.InsertHotelBranchRoomType(createdby, BranchIDint, Room);
                //        }
                //    }
                //}

                return BranchIDint;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void vendorBranchMaintenanceUpdate(int iBranchID, string InstructionON, string InstructionOFF,
            String strLogDescription, String strFunction, String strPageName,
            DateTime DateGMT, DateTime CreatedDate, String CreatedBy)
        {
            VendorMaintenanceDAL.vendorBranchMaintenanceUpdate(iBranchID, InstructionON, InstructionOFF,
            strLogDescription, strFunction, strPageName,
            DateGMT, CreatedDate, CreatedBy);
        }
        public static Int32 InsertHotelBranchVoucher(string createdby, string BranchID, decimal Stripes, decimal VAmount, int DayNo)
        {
            Int32 VoucherID = 0;
            VoucherID = VendorMaintenanceDAL.InsertHotelBranchVoucher(createdby, Convert.ToString(BranchID), Stripes, VAmount, DayNo);
            return VoucherID;
        }

        public static Int32 InsertHotelBranchRoomType(string createdby, Int32 BranchID, string RoomType)
        {
            Int32 RoomTypeID = 0;
            RoomTypeID = VendorMaintenanceDAL.InsertHotelBranchRoomType(createdby, BranchID, RoomType);
            return RoomTypeID;
        }
        
        /// <summary>
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle vendor maintenance information      
        /// -------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static IDataReader vendorMaintenanceInformation(Int32 vendorPrimaryId)
        {
             IDataReader dt = null;
             try
             {
                 dt = VendorMaintenanceDAL.vendorMaintenanceInformation(vendorPrimaryId);
                 return dt;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
           
        }

        /// <summary>
        /// Date Created: 01/08/2011
        /// Created By: Ryan Bautista
        /// (description) Get vehicle vendor maintenance information  
        /// ---------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static void vendorBranchMaintenanceInformation(Int32 HotelID)
        {
            VendorMaintenanceDAL.vendorBranchMaintenanceInformation(HotelID);
        }

        /// <summary>
        /// Date Created:       01/08/2011
        /// Created By:         Ryan Bautista
        /// (description)       Get vehicle vendor maintenance information       
        /// -----------------------------------------------------
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Replace DataTable with List
        /// </summary> 
        //public static DataTable GetHotelBranchVoucherByBranchID(string BranchID)
        public static List<HotelBranchVoucherList> GetHotelBranchVoucherByBranchID(string BranchID)
        {
            return VendorMaintenanceDAL.GetHotelBranchVoucherByBranchID(BranchID);
        }

        /// <summary>
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle vendor maintenance information            
        /// ---------------------------------------------------
        /// Date Modified:  27/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to IDataReader
        /// </summary> 
        public static IDataReader vehicleVendorBranchMaintenanceInformation(Int32 branchId)
        {            
            try
            {
                return VendorMaintenanceDAL.vehicleVendorBranchMaintenanceInformation(branchId);                
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        /// <summary>
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle type branch information            
        /// </summary> 
        public static DataTable vehicleTypeBranchInfoLoad(Int32 vehicleId)
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.vehicleTypeBranchInfoLoad(vehicleId);
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
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle branch type list            
        /// </summary> 
        public static DataTable GetVehicleTypeBranchList(Int32 branchId)
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.GetVehicleTypeBranchList(branchId);
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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vehicle vendor branch          
        /// </summary> 
        public static void vehicleInsertUpdateVendorBranch(String branchName, String branchCode, String vendorAddress, Int32 city, Int32 country, String contactNo,
                                                String strUserName, Int32 vendorId, String contactPerson, Boolean IsFranchise, String branchID,Int16 VehicleType)
        {
            try
            {
                VendorMaintenanceDAL.vehicleInsertUpdateVendorBranch(branchName, branchCode, vendorAddress, city, country, contactNo, strUserName,
                                                                vendorId, contactPerson, IsFranchise, branchID, VehicleType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert/update vehicle type branch          
        /// </summary> 
        public static Int32 vehicleInsertUpdateVehicleTypeBranch(Int32 vehicleId, Int32 vehicleType, String Name, Int32 branchId, Int32 Capacity, String User)
        {
            try
            {
                Int32 pVehicleID = 0;
                pVehicleID = VendorMaintenanceDAL.vehicleInsertUpdateVehicleTypeBranch(vehicleId, vehicleType, Name, branchId, Capacity, User);
                return pVehicleID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>            
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert/update vehicle type branch
        /// </summary>  
        public static void vehicleInsertUpdateBranch(Int32 branchID, Int32 vehicleID, Int32 vehicleTypeID, String Name, Int32 Capacity, String userName)
        {
            VendorMaintenanceDAL.vehicleInsertUpdateBranch(branchID, vehicleID, vehicleTypeID, Name, Capacity, userName);
        }

        public static DataTable vehicleGetBranchName(Int32 branchId)
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.vehicleGetBranchName(branchId);
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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting country list
        /// </summary>
        public static DataTable countryList()
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.countryList();
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
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting city list by country id
        /// </summary>
        public static DataTable cityListByCountry(Int32 countryID)
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.cityListByCountry(countryID);
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
        /// (description)   Get vehicle type, brand, make, and year               
        /// </summary>
        public static DataTable vehicleGetTypeList()
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.vehicleGetTypeList();
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
        /// Date Created:   13/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle branch          
        /// </summary>            
        public static DataTable vehicleGetBranch()
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.vehicleGetBranch();
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
        /// Date Created: 13/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle capacity by vehicle type id
        /// </summary>
        public static DataTable vehicleGetCapacity(Int32 VehicleTypeID)
        {
            DataTable dt = null;
            try
            {
                dt = VendorMaintenanceDAL.vehicleGetCapacity(VehicleTypeID);
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
        /// Date Created:   17/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get branch details by branchID
        /// ----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static IDataReader GetBranchDetails(string BranchID)
        {
            IDataReader dt = null;
            try
            {
                dt = VendorMaintenanceDAL.GetBranchDetails(BranchID);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Date Created: 06/10/2011
        /// Created By: Ryan Bautista
        /// (description) Remove hotel branch voucher  
        /// --------------------------------------------
        /// Date Modified:  17/09/2012
        /// Modified By:    Josephine Gad
        /// (description)   Use VoucherID in updating table
        ///                 Do not return value    
        /// --------------------------------------------
        /// </summary> 
        public static void RemoveHotelBranchVoucherByID(String userName, int VoucherID)
        {
            try
            {
                //Int32 VoucherID = 0;
                //Int32 branchIDint = Convert.ToInt32(branchID);
                //decimal stripesDec = Convert.ToDecimal(stripes);
               VendorMaintenanceDAL.RemoveHotelBranchVoucherByID(userName, VoucherID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 06/10/2011
        /// Created By: Ryan Bautista
        /// (description) Remove hotel branch room type       
        /// </summary> 
        public static Int32 RemoveHotelBranchRoomByID(string branchID, string RoomType, String userName)
        {
            try
            {
                Int32 BranchRoomID = 0;
                Int32 branchIDint = Convert.ToInt32(branchID);

                BranchRoomID = VendorMaintenanceDAL.RemoveHotelBranchRoomByID(branchIDint, RoomType, userName);
                return BranchRoomID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>            
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Room capacity
        /// </summary>
        //public static void InsertHotelBranchRoomCapacity(string BranchID, DateTime StartDate, DateTime EndDate,
        //    string RatePerDay, string CurrencyID, string RoomRateTaxPercentage, bool RoomRateTaxInclusive,
        //    string RoomTypeID, string NumberOfUnits, string Mon, string Tue, string Wed, string Thu,
        //    string Fri, string Sat, string Sun, string UserName)
        //{
        //    VendorMaintenanceDAL.InsertHotelBranchRoomCapacity(BranchID, StartDate, EndDate,
        //    RatePerDay, CurrencyID, RoomRateTaxPercentage, RoomRateTaxInclusive, RoomTypeID,
        //    NumberOfUnits, Mon, Tue, Wed, Thu, Fri, Sat, Sun, UserName);
        //}
        /// <summary>            
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Hotel Room with capacity
        /// </summary>
        public static void DeleteHotelBranchRoomCapacity(string HotelRoomID, string UserName)
        {
            VendorMaintenanceDAL.DeleteHotelBranchRoomCapacity(HotelRoomID, UserName);
        }
         /// <summary>            
        /// Date Created:   21/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert vendor branch Department Stripe           
        /// </summary>
        public static Int32 InsertHotelHotelBranchDeptStripe(string DeptStripeID, string BranchID, string DepartmentID,
            string Stripes, string CreatedBy)
        {
            try
            {
                Int32 BranchDeptStripeID = 0;
                BranchDeptStripeID = VendorMaintenanceDAL.InsertHotelHotelBranchDeptStripe(DeptStripeID, BranchID, DepartmentID, Stripes, CreatedBy);
                return BranchDeptStripeID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
          /// <summary>            
        /// Date Created:   21/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete vendor branch Department Stripe           
        /// </summary>
        public static void DeleteHotelHotelBranchDeptStripe(string DeptStripeID, string DeletedBy)
        {
            try
            {
                VendorMaintenanceDAL.DeleteHotelHotelBranchDeptStripe(DeptStripeID, DeletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>            
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert vendor branch Rank Exception      
        /// </summary>
        public static Int32 InsertHotelHotelBranchRankException(string BranchRankExceptionID,
            string BranchID, string RankID, string CreatedBy)
        {
            try
            {
                Int32 pBranchRankExceptionID = 0;
                pBranchRankExceptionID = VendorMaintenanceDAL.InsertHotelHotelBranchRankException(BranchRankExceptionID,
                    BranchID, RankID, CreatedBy);
                return pBranchRankExceptionID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>            
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete vendor branch Rank Exception  
        /// </summary>
        public static void DeleteHotelHotelBranchRankException(string BranchRankExceptionID, string DeletedBy)
        {
            try
            {
                VendorMaintenanceDAL.DeleteHotelHotelBranchRankException(BranchRankExceptionID, DeletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable getVendorType(Int16 loadType)
        {
            try
            {
                return VendorMaintenanceDAL.getVendorType(loadType);
            }
            catch 
            {
                throw;
            }        
        }
        /// <summary>
        /// Date Created:   5/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle Vendor Details, Country List and City List
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsGetByID(Int32 iVendorID, Int16 iLoadType)
        {
            VendorMaintenanceDAL.VehicleVendorsGetByID(iVendorID, iLoadType);
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle Type of Vendor 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsTypeGet(Int32 iVendorID)
        {
            VendorMaintenanceDAL.VehicleVendorsTypeGet(iVendorID);
        }
        /// <summary>
        /// Date Created:   05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Save Vehicle Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsSave(Int32 iVehicleVendorID, string sVehicleVendorName, Int32 iCountryID,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
            string sEmailCc, string sEmailTo, string sWebsite, string sVendorID,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dt, DataTable dtPlateNo)
        {
            VendorMaintenanceDAL.VehicleVendorsSave(iVehicleVendorID, sVehicleVendorName, iCountryID,
            iCityID, sContactNo, sFaxNo, sContactPerson, sAddress, sEmailCc, sEmailTo, sWebsite, sVendorID,
            UserId, strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, dt, dtPlateNo);
        }
        /// <summary>
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport of Vendor Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsAirportGet(Int32 iContractID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            VendorMaintenanceDAL.VehicleVendorsAirportGet(iContractID, iFilterBy, sFilter, isViewExists, iLoadType);
        }
         /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport of Vendor Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsSeaportGet(Int32 iContractID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            VendorMaintenanceDAL.VehicleVendorsSeaportGet(iContractID, iFilterBy, sFilter, isViewExists, iLoadType);
        }
        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle Route
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsRouteGet()
        {
            VendorMaintenanceDAL.VehicleVendorsRouteGet();
        }
         /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type of company
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsVehicleTypeGet(Int32 iContractID, Int32 iVehicleVendorID,
            bool isViewExists, Int16 iLoadType)
        {
            VendorMaintenanceDAL.VehicleVendorsVehicleTypeGet(iContractID, iVehicleVendorID, isViewExists, iLoadType);
        }
        /// <summary>
        /// Date Created:   12/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider details
        /// -----------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>        
        //public static IDataReader GetPortAgent(string portAgentIDString)
        //{
        /// ------------------------------------------
        /// Date Modified: 05/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Rename GetPortAgent to GetPortAgentByID
        ///                Change IDataReader to void
        /// </summary>        
        public static void GetPortAgentByID(int iPortAgentID, Int16 iLoadType)
        {
            VendorMaintenanceDAL.GetPortAgentByID(iPortAgentID, iLoadType);
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport of Service Provider
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentAirport(Int32 iVendorID)
        {
            VendorMaintenanceDAL.GetPortAgentAirport(iVendorID);
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle Type of Service Provider 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentVehicleType(Int32 iVendorID)
        {
            VendorMaintenanceDAL.GetPortAgentVehicleType(iVendorID);
        }
         /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Save Service Provider Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void PortAgentVendorsSave(Int32 iPortAgentID, string sPortAgentName, Int32 iCountryID,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
            string sEmailCc, string sEmailTo, string sWebsite, string sVendorID,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dtAirport, DataTable dtVehicleType)
        {
            try
            {
                VendorMaintenanceDAL.PortAgentVendorsSave(iPortAgentID, sPortAgentName, iCountryID,
                iCityID, sContactNo, sFaxNo, sContactPerson, sAddress,
                sEmailCc, sEmailTo, sWebsite, sVendorID, UserId, strLogDescription,
                strFunction, strPageName, DateGMT, CreatedDate, dtAirport, dtVehicleType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
            }
        }
         /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Plate No. of Vendor 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static List<VehiclePlate> VehicleVendorsPlateNoGet(Int32 iVendorID)
        {
            return VendorMaintenanceDAL.VehicleVendorsPlateNoGet(iVendorID);
        }
        /// <summary>
        /// Date Created:   28/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type of company
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void PortAgentVehicleTypeGet(Int32 iContractID, Int32 iPortAgentID,
            bool isViewExists, Int16 iLoadType)
        {
            VendorMaintenanceDAL.PortAgentVehicleTypeGet(iContractID, iPortAgentID, isViewExists, iLoadType);
        }
         /// ------------------------------------------
        /// Date Created:  04/Mar/2014
        /// Created By:    Josephine Gad
        /// (description)  Get Service Provider details, Airport and Brand
        /// ------------------------------------------
        /// </summary>        
        public static void GetPortAgentAirportBrand(int iPortAgentID,  Int16 iLoadType)
        {
            VendorMaintenanceDAL.GetPortAgentAirportBrand(iPortAgentID, iLoadType);

        }
         /// ------------------------------------------
        /// Date Created:  16/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  get Luggage UOM
        /// ------------------------------------------
        /// </summary>        
        public List<LuggageUOM> GetLuggageUOM()
        {
            return DAL.GetLuggageUOM();
        }
        /// ------------------------------------------
        /// Date Created:  16/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  get Luggage UOM
        /// ------------------------------------------
        /// </summary>        
        public List<SafeguardUOM> GetSafeguardUOM()
        {
            return DAL.GetSafeguardUOM();
        }

        public List<ColorCodes> GetColor()
        {
            return DAL.GetColor();
        }


    
    
    }
}

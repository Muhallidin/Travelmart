using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;


namespace TRAVELMART.BLL
{
    public class SuperViewBLL
    {
        /// <summary>
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer personal info             
        /// </summary>     
        public static DataTable GetSFInfo(string sfCode)
        {          
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFInfo(sfCode);
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
        /// Date Created: 21/07/2011
        /// Created By: Josephine Gad
        /// (description) Get seafarer personal info based from status and itinerary no            
        /// </summary>   
        public static DataTable GetSFTravelInfo(string sfCode, string status, string recloc)
        {                  
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFTravelInfo(sfCode, status, recloc);
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
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer air travel info                  
        /// </summary>  
        public static DataTable GetSFAirTravelDetails(string sfCode, string status, string recloc, string travelreqId)
        {           
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFAirTravelDetails(sfCode, status, recloc, travelreqId);
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
        /// Date Created:   31/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer air bookings by travel request ID
        /// </summary>  
        public static DataTable GetSFAirTravelDetailsAll(string sfCode, string status)
        {           
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFAirTravelDetailsAll(sfCode, status);
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
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer vehicle travel info            
        /// </summary>
        public static DataTable GetSFVehicleTravelDetails(string sfCode, string status, string recloc)
        {          
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFVehicleTravelDetails(sfCode, status, recloc);
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
        /// Date Created:   02/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer vehicle bookings by travel request ID
        /// -------------------------------------------------       
        /// </summary>
        public static DataTable GetSFVehicleTravelDetailsAll(string TravelReqIDInt, string statusString)
        {          
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFVehicleTravelDetailsAll(TravelReqIDInt, statusString);
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
        /// Date Created: 08/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer vehicle travel info           
        /// </summary>         
        public static DataTable GetSFHotelDetails(string sfCode, string status, string recloc)
        {          
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFHotelDetails(sfCode, status, recloc);
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
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer hotel bookings by travel request ID
        /// --------------------------------------------------       
        /// </summary>    
        public static DataTable GetSFHotelDetailsAll(string TravelReqIDInt, string ManualRequestIdInt, string statusString)
        {                
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFHotelDetailsAll(TravelReqIDInt, ManualRequestIdInt, statusString);
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
        /// Date Created:  21/07/2011
        /// Created By:    Josephine Gad
        /// (description)  Get seafarer port information per travel request ID/ request ID
        /// --------------------------------------------------       
        /// Date Modified:  03/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter ViewInTR for the data in TblNoTravelRequest
        /// --------------------------------------------------     
        /// </summary>     
        public static DataTable GetSFPortTravelDetailsByTravelReqID(string TravelReqID, string ManualReqID, string SFStatus)
        {
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFPortTravelDetailsByTravelReqID(TravelReqID, ManualReqID, SFStatus);
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
        /// Date Created: 11/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer port travel info         
        /// </summary>  
        public static DataTable GetSFPortTravelDetails(string TravelReqID, string RecLoc, string ManualReqID, string SFStatus)
        {              
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFPortTravelDetails(TravelReqID, RecLoc, ManualReqID, SFStatus);
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
             DataTable dt = null;
             try
             {
                 dt = SuperUserDAL.GetVendor(TypeString, IsAccredited, country, city, port, user, role);
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
        /// Date Created:   10/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle booking by record locator or manual request ID, travel req ID, and SF ID
        /// </summary>      
        public static DataTable GetSFVehicleTravelDetailsByID(string TravelReqID, string SfID, string sfStatus,
            string recLoc, string manualReqID)
        {
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFVehicleTravelDetailsByID(TravelReqID, SfID, sfStatus,
                recLoc, manualReqID);
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
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get pending vehicle bookings
        /// </summary>      
        public static DataTable GetSFVehicleTravelDetailsPending(string TravelReqID, string manualReqID)
        {
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFVehicleTravelDetailsPending(TravelReqID, manualReqID);
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
        /// Date Created:   10/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel booking by record locator or manual request ID, travel req ID, and SF ID
        /// </summary>      
        public static DataTable GetSFHotelDetailsByID(string TravelReqID, string SfID, string sfStatus,
            string recLoc, string manualReqID)
        {
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFHotelDetailsByID(TravelReqID, SfID, sfStatus,
                recLoc, manualReqID);
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
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get pending hotel bookings
        /// </summary>      
        public static DataTable GetSFHotelTravelDetailsPending(string TravelReqID, string manualReqID)
        {
            DataTable dt = null;
            try
            {
                dt = SuperUserDAL.GetSFHotelTravelDetailsPending(TravelReqID, manualReqID);
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
    }
}

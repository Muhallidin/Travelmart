using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class CityBLL
    {
        /// <summary>
        /// Date Created:   15/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting City List
        /// ---------------------------------
        /// Date Modified:  25/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// </summary>
        public static DataTable CityList()
        {            
            DataTable dt  = null;
            try
            {
                dt = CityDAL.CityList();
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
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By City ID
        /// </summary>
        public static DataTable CityListByID(Int32 CityID)
        {                    
            DataTable dt  = null;
            try
            {
                dt = CityDAL.CityListByID(CityID);
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
        /// Date Created: 02/08/2011
        /// Created By: Marco Abejar
        /// (description) Selecting City List based on Country ID
        /// </summary>
        public static DataTable CityListbyCountry(Int32 CountryID)
        {                        
             DataTable dt  = null;
             try
             {
                 dt = CityDAL.CityListbyCountry(CountryID);
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
        /// Date Created:   07/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get City List By country ID
        /// ---------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static DataTable GetCityByCountry(string CountryID, string CityName, string cityInitial)
        {
            DataTable dt = null;
            try
            {
                dt = CityDAL.GetCityByCountry(CountryID, CityName, cityInitial);
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

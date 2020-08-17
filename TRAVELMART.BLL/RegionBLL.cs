using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class RegionBLL
    {
        /// <summary>
        /// Date Created:   27/02/2012
        /// Modified By:    Gabriel Oquialda
        /// (description)   Get seaport list
        /// --------------------------------    
        /// </summary>        
        public static List<Seaport> GetSeaport(int RegionID, bool IsViewExist)
        {
            return RegionDAL.GetSeaport(RegionID, IsViewExist);
        }
         /// <summary>
        /// Date Created:   04/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport not exists in Region
        /// -------------------------------  
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static List<RegionSeaportNotExists> GetSeaportNotExistsInRegion(int RegionID, int ContinentID, int CountryID, string PortName)
        {
            return RegionDAL.GetSeaportNotExistsInRegion(RegionID, ContinentID, CountryID, PortName);
        }
        /// <summary>
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Get region seaport 
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static List<RegionSeaport> GetRegionSeaport(string RegionID, string CountryID, string PortName)
        {
            try
            {
                return RegionDAL.GetRegionSeaport(RegionID, CountryID, PortName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>            
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Delete region seaport (flagged as inactive)
        /// </summary>
        public static void DeleteRegionSeaport(string RegionSeaportID, string DeletedBy)
        {
            try
            {
                RegionDAL.DeleteRegionSeaport(RegionSeaportID, DeletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>            
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Insert region seaport
        /// </summary>
        public static Int32 InsertRegionSeaport(string RegionSeaportID, string RegionID, string SeaportID,
            string CreatedBy)
        {
            try
            {
                Int32 pRegionSeaportID = 0;
                pRegionSeaportID = RegionDAL.InsertRegionSeaport(RegionSeaportID, RegionID, SeaportID, CreatedBy);
                return pRegionSeaportID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>            
        /// Date Created: 28/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Delete region (flagged as inactive)
        /// </summary>
        public static void DeleteRegion(Int32 RegionID, string DeletedBy)
        {
            try
            {
                RegionDAL.DeleteRegion(RegionID, DeletedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>
        /// Date Created:   04/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Continent list
        /// -------------------------------  
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static List<Continent> GetContinent()
        {
            return RegionDAL.GetContinent();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: Load Region Seaport page
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public static void LoadRegionPage(int RegionId, string strLogDescription, string strFunction, string PathName,
                DateTime GMTDate, DateTime DateNow, string UserName)
        {
            List<RegionGenericClass> region = new List<RegionGenericClass>();

            region = RegionDAL.LoadRegionPage(RegionId, strLogDescription, strFunction, 
                PathName, GMTDate, DateNow,
                UserName);

            RegionClass.ContinentList = region[0].ContinentList;
            RegionClass.RegionSeaportList = region[0].RegionSeaportList;
        }
    }
}

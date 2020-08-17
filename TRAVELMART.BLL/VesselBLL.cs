using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class VesselBLL
    {
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of Vessel from Sail master based from user, date, region, etc.
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortId"></param>
        /// <returns></returns>
        public static DataTable GetVessel(string Username, string FromDate, string ToDate,
            string RegionID, string CountryID, string CityID, string PortId, string Role)
        {             
            DataTable dt = null;
            try
            {
                dt = VesselDAL.GetVessel(Username, FromDate, ToDate, RegionID,
                    CountryID, CityID, PortId, Role);
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
        /// Date Created:   16/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of Vessel using List and not DataTable
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortId"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public static List<VesselDTO> GetVesselList(string Username, string FromDate, string ToDate,
           string RegionID, string CountryID, string CityID, string PortId, string Role, bool ForCalendar)
        {
            return VesselDAL.GetVesselList(Username, FromDate, ToDate, RegionID,
                 CountryID, CityID, PortId, Role, ForCalendar);
        }
        /// <summary>
        /// Date Created:   05/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel details by Vessel ID
        /// -----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        public static IDataReader GetVesselPortDetails(string VesselID, string FromDate, string ToDate)
        {
            IDataReader dt = null;
            try
            {
                dt = VesselDAL.GetVesselPortDetails(VesselID, FromDate, ToDate);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

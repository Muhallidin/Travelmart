using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class Maintenance
    {
        public static int? EventCount { get; set; }
        public int GetEventCount(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
            int BranchId,int LoadType)
        {
            try
            {
                return (int)EventCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                EventCount = null;
            }

        }

        public static List<Events> Events { get; set; }
        public List<Events> GetEvents(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
            int BranchId, int LoadType, int startRowIndex, int maximumRows)
        {
            try
            {
                return Events;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Events = null;
            }
        }
    }

    public class MaintenanceGenericClass
    {
        public int EventCount { get; set; }
        public List<Events> Events { get; set; }
    }

    /// <summary>
    /// Date Created:   17/02/2016
    /// Created By:     Josephine Monteza
    ///(description)    Blackout Dates list
    /// </summary>
    [Serializable]    
    public class BlackoutDateList
    {
        public int BlackoutDateID { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int PortId { get; set; }
        public string PortName { get; set; }

        public DateTime BlackoutDateFrom { get; set; }
        public DateTime BlackoutDateTo { get; set; }

    }
}

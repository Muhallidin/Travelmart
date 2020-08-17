using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Modified By:    Josephine Gad
    /// Date Modified:  27/07/2012
    /// Description:    Add Serializable
    /// -----------------------------------------
    /// Modified By:    Josephine Monteza
    /// Date Modified:  30/03/2016
    /// Description:    Add Serializable
    /// </summary>
    [Serializable]
    public class BranchList
    {
        public int? VendorId { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public int? CountryId { get; set; }
        public Int64? CityId { get; set; }
        public int? CurrencyId { get; set; }
        public string Currency { get; set; }
        public int? ContractId { get; set; }
        public bool? withShuttle { get; set; }
        public bool isAccredited { get; set; }
        public int? EventCount { get; set; }
        public int? RegionId { get; set; }

        public string ContractDateStart { get; set; }
        public string ContractDateEnd { get; set; }

        public double NoOfDaysExpiry { get; set; }
    }

    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 21/02/2012
    /// Description: load user branch
    /// </summary>
    /// 
    [Serializable]
    public class UserBranch
    {
        public int? BranchID { get; set; }
        public string BranchName { get; set; }
    }
    [Serializable]
    public class RegionList
    {
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
    }
    [Serializable]
    public class CountryList
    {
        public int? RegionId { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
    }
    [Serializable]
    public class CityList
    { 
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   06/07/2012
    /// Description:    Load user port
    /// </summary>
    /// 
    [Serializable]
    public class PortList
    {
        public Int64? PortId { get; set; }        
        public string PortName { get; set; }
    }
    /// <summary>
    /// Author:         Marco Abejar
    /// Date Created:   10/10/2013
    /// Description:    Load Driver
    /// </summary>
    /// 
    [Serializable]
    public class Driver
    {
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverImage { get; set; }
    }

    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   06/07/2012
    /// Description:    Load user port
    /// </summary>
    /// 
    [Serializable]
    public class PortAgentVendorList
    {
        public Int64? PortAgentVendorId { get; set; }
        public string PortAgentVendorName { get; set; }
    }

    [Serializable]
    public class HotelVendorNonTurnPortList
    {
        public int? BranchID { get; set; }
        public int? VendorID { get; set; }
        public String VendorName { get; set; }
    }
    

}

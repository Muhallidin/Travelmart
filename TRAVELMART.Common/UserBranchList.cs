using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Author: Charlene Remotigue
    /// Date Created: 01/02/2012
    /// Description: User Branch List class
    /// </summary>
    public class UserBranchList
    {
        public int? BranchID { get; set; }
        public string BranchName { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   11/Jan/2013
    /// Description:    User Region List
    /// </summary>
    /// 
    [Serializable]
    public class UserRegionList
    {
        public int UserRegionID { get; set; }
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public bool IsExist { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   11/Jan/2013
    /// Description:    User Region List to be added
    /// </summary>
    /// 
    [Serializable]
    public class UserRegionToAdd
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
    }

    /// <summary>
    /// Author:         Josephine Monteza
    /// Date Created:   17/Oct/2017
    /// Description:    User Vendor
    /// </summary>
    /// 
    [Serializable]
    public class UserVendorList
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorType { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
    }
}

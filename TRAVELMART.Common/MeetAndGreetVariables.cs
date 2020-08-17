using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   14/Nov/2013
    /// Created By:     Jefferson Bermundo
    /// (description)   Class for Meet and Greet Vendor
    /// </summary>
    [Serializable]
    public class MeetAndGreetList
    {
        public int MeetandGreetVendorId { get; set; }
        public string MeetAndGreetVendorName { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string Website { get; set; }
    }
    /// <summary>
    /// Date Created:   14/Nov/2013
    /// Created By:     Jefferson Bermundo
    /// (description)   Class for Meet and Greet Vendor
    /// </summary>
    [Serializable]
    public class MeetAndGreetDetails
    {
        public int MeetandGreetVendorId { get; set; }
        public string MeetAndGreetVendorName { get; set; }
        public Int32 CountryID { get; set; }
        public string CountryName { get; set; }
        public Int32 CityID { get; set; }
        public string CityName { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string Website { get; set; }
    }

    [Serializable]
    public class ContractMeetAndGreet
    {
        public Int64 ContractID { get; set; }
        public string VendorName { get; set; }
        public string ContractStatus { get; set; }
        public string ContractName { get; set; }
        public string ContractDateStart { get; set; }
        public string ContractDateEnd { get; set; }

        public string Remarks { get; set; }
        public Int64 BranchID { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }

        public DateTime DateCreated { get; set; }
    }

}


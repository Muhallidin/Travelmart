using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    class HotelVariables
    {
        public string _HotelID = string.Empty;
        public string _HotelName = string.Empty;
        public string _HotelCode = string.Empty;
        public string _HotelAddress = string.Empty;
        public string _HotelCountry = string.Empty;
        public string _HotelCity = string.Empty;
    }
    /// <summary>
    /// Modified By:    Josephine Monteza
    /// Date Modified:  24/Aug/2016
    /// Description:    Add Hotel Branch class
    /// </summary>
    [Serializable]
    public class HotelBranchDetails
    {
        public string HotelName { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string ContractNo { get; set; }
        public int VendorId { get; set; }
        public int HotelId { get; set; }
        
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public bool IsFranchise { get; set; }

        //public string ChainCode { get; set; }
        public string BranchCode { get; set; }

        public string EmailTo { get; set; }
        public string EmailCc { get; set; }

        public string CityName { get; set; }

        public bool OnBit { get; set; }
        public bool OffBit{ get; set; }

        public string FaxNo { get; set; }
        public string Website { get; set; }

        public string InstructionOn { get; set; }
        public string InstructionOff { get; set; }
        
    }
    /// <summary>
    /// Modified By:    Josephine Monteza
    /// Date Modified:  24/Aug/2016
    /// Description:    Add IMS Vendor Class
    /// </summary>
    [Serializable]
    public class IMSVendor
    {
        public int iVendorID { get; set; }
        public string sVendorNameWithId { get; set; }
        public string sVendorName { get; set; }

    }
}

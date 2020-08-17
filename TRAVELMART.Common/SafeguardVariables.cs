using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class SafeguardVariables
    {
        
    }
    /// <summary>
    /// Date Created:   08/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor Type
    /// </summary>
    [Serializable]
    public class ServiceType
    {
        public int ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
    }
    /// <summary>
    /// Date Created:   14/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor Type in Contract
    /// </summary>
    [Serializable]
    public class ContractServiceTypeDuration
    {
        public int ContractSafeguardDurationIDInt { get; set; }
        public int ContractID { get; set; }
        public int ServiceTypeID { get; set; }
        public string ServiceType { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
    }

    /// <summary>    
    /// Date Created:  07/Nov/2013
    /// Created By:     Marco Abejar
    /// (description)   Added service detail
    /// </summary>
    [Serializable]
    public class ContractServiceDetailsAmt
    {
        public int ContractDetailID { get; set; }
        public int ContractID { get; set; }
        public int BranchID { get; set; }
        public int ContractServiceDurationID { get; set; }
        public int ServiceTypeID { get; set; }
        public string ServiceType { get; set; }        
        public float RateAmount { get; set; }
        public float Tax { get; set; }
    }
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       08/Aug/2013
    /// Description:        Class Model For Contract Vehicle
    /// </summary>
    /// 
    [Serializable]
    public class ContractSafeguard
    {
        public string SafeguardName { get; set; }
        public Int64 ContractID { get; set; }
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

    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       25/Sept/2013
    /// Description:        Vehicle Contract Details
    /// </summary>
    /// 
    [Serializable]
    public class ContractSafeguardDetails
    {
        public Int64 ContractID { get; set; }
        public Int64 BranchID { get; set; }

        //public string VehicleName { get; set; }
        public Int64 CityID { get; set; }
        public string CityName { get; set; }
        public Int64 CountryID { get; set; }
        public string CountryName { get; set; }

        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }

        public string EmailCc { get; set; }
        public string EmailTo { get; set; }

        public string FaxNo { get; set; }
        //public DateTime? ContractDateStart { get; set; }
        //public DateTime? ContractDateEnd { get; set; }
        public string ContractDateStart { get; set; }
        public string ContractDateEnd { get; set; }
        public string ContractName { get; set; }
        public string ContractStatus { get; set; }

        public int CurrencyID { get; set; }

        //public DateTime? RCCLAcceptedDate { get; set; }
        public string RCCLAcceptedDate { get; set; }
        public string RCCLPersonnel { get; set; }

        //public DateTime? VendorAcceptedDate { get; set; }
        public string VendorAcceptedDate { get; set; }
        public string VendorPersonnel { get; set; }

        public string Remarks { get; set; }
    }

    /// <summary>
    /// Date Created:   02/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor
    /// </summary>
    [Serializable]
    public class VendorSafeguardDetails
    {
        public int SafeguardID { get; set; }
        public string VendorName { get; set; }
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
    /// <summary>
    /// Created By:         Josephine Gad
    /// Date Created:       16/Aug/2013
    /// Description:        Class Model For Contract Vehicle Attachments
    /// </summary>
    [Serializable]
    public class ContractSafeguardAttachment
    {
        public int AttachmentId { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public byte[] uploadedFile { get; set; }
        public int colContractId { get; set; }
        public DateTime UploadedDate { get; set; }
    }
       
}

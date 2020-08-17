using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public static class ReportClass
    {
       
    }

    /// <summary>
    /// Date Created:    25/Jun/2015
    /// Created By:      Josephine Monteza
    /// Description:     Crew Assist Remarks List
    /// --------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class CrewAssistRemarksList
    {
        
        public Int64 TravelRequestID { get; set; }
        public Int64 SeafarerID { get; set; }
        public string Source { get; set; }
        public string RequestHeader { get; set; }
        public string RequestType { get; set; }
        public string Summary { get; set; }
        public string Remarks { get; set; }
        public string RemarksStatus { get; set; }

        public string Requestor { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? TransactionDate { get; set; }
        public TimeSpan? TransactionTime { get; set; }
        public bool IR { get; set; }

    }
    /// <summary>
    /// Date Created:    25/Jun/2015
    /// Created By:      Josephine Monteza
    /// Description:     Crew Assist Remarks List
    /// --------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class RemarksByDateList
    {
        public string RequestType { get; set; }
        public int iCount { get; set; }
    }
    /// <summary>
    /// Date Created:    29/Jun/2015
    /// Created By:      Josephine Monteza
    /// Description:     Request Source List
    /// --------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class RemarksSourceList
    {
        public int RequestSourceIDint { get; set; }
        public string RequestSource { get; set; }
    }
}

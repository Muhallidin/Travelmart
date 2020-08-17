using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   03/Dec/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Exception list from XML data
    /// </summary>
    /// 
    [Serializable]
    public class XMLExceptionList
    {
        public int ExceptionID { get; set; }
        public string RecordLocator { get; set; }
        public int SequenceNo { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
    [Serializable]
    public class ActiveExceptionList
    {
        public string RecordLocator { get; set; }
        public int SequenceNo { get; set; }
        public string Name { get; set; }
        public int E1No { get; set; }
        public int E1TRNo { get; set; }
        public string Status { get; set; }
        public DateTime OnOffDate { get; set; }
    }
    /// <summary>
    /// Author:         Josephine Gad
    /// Date Created:   04/Apr/2013
    /// Description:    Port not exist in TM
    /// </summary>
    /// 
    [Serializable]
    public class PortNotExistList
    {
        public string PortCode { get; set; }
        public string PortName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class AnalystExceptionBLL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Dec/2012
        /// Descrption:     Get XML Exception 
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sRole"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="sRecordLocator"></param>
        /// <param name="iSequenceNo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        public static List<XMLExceptionList> GetXMLExceptionList(string sUserName, string sRole,
            string DateFrom, string DateTo, string sRecordLocator, int iSequenceNo,
            int StartRow, int MaxRow)
        {
            AnalystExceptionDAL.GetXMLExceptionList(sUserName, sRole,  DateFrom, 
                DateTo, sRecordLocator, iSequenceNo, StartRow, MaxRow);
            
            List<XMLExceptionList> list = new List<XMLExceptionList>();
            list = (List<XMLExceptionList>)HttpContext.Current.Session["XMLExceptionList"];
            return list;
        }
        public static int GetXMLExceptionCount(string sUserName, string sRole,
            DateTime DateFrom, DateTime DateTo, string sRecordLocator, int iSequenceNo)
        {
            int iCount = GlobalCode.Field2Int(HttpContext.Current.Session["XMLExceptionCount"]);
            return iCount;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/Dec/2012
        /// Descrption:     Get Active PNR but cancel E1 Travel Routing Exception 
        /// </summary>
        public static List<ActiveExceptionList> GetActiveExceptionList(Int16 iFilterBy, string sFilter,
            string sRecordLocator, int iSequenceNo,
            int StartRow, int MaxRow)
        {
            AnalystExceptionDAL.GetActiveExceptionList(iFilterBy, sFilter, sRecordLocator, iSequenceNo,
            StartRow, MaxRow);
            
            List<ActiveExceptionList> list = new List<ActiveExceptionList>();
            list = (List<ActiveExceptionList>)HttpContext.Current.Session["ActiveExceptionList"];
            return list;
        }
        public static int GetActiveExceptionCount(Int16 iFilterBy, string sFilter,
            string sRecordLocator, int iSequenceNo)
        {
            int iCount = GlobalCode.Field2Int(HttpContext.Current.Session["ActiveExceptionCount"]);
            return iCount;
        }
          /// <summary>
        /// ===============================================================
        /// Modified By:    Josephine Gad
        /// Date Created:   17/Mar/2013
        /// Description:    Change List to Void
        ///                 Assign Session values here
        /// ===============================================================
        /// </summary>
        public static void GetNonTurnPortNotInTM(DateTime Date, string UserId, string PortCode, string OrderBy)
        {
            AnalystExceptionDAL.GetNonTurnPortNotInTM(Date, UserId, PortCode, OrderBy);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class ReportBLL
    {
        ReportDAL DAL = new ReportDAL();
        /// <summary>
        /// Date Created:   25/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks
        /// --------------------------------------- 
        /// </summary>       
        public static List<CrewAssistRemarksList> GetCrewAssistRemarks(Int32 iYear,
            Int32 iMonth, string sCreatedBy, string sUserID, Int16 iLoadType,
             Int16 iFilterBy, string sFilterValue,
            string sOrderBy, short sIRBy, int iStartRow, int iMaxRow)
        {
            List<CrewAssistRemarksList> list = new List<CrewAssistRemarksList>();

            list = ReportDAL.GetCrewAssistRemarks( iYear, iMonth, sCreatedBy, sUserID, iLoadType,
                    iFilterBy,  sFilterValue,
                    sOrderBy, sIRBy,iStartRow ,iMaxRow );

            return list;
        }
        public int GetCrewAssistRemarksCount(Int32 iYear,
           Int32 iMonth, string sCreatedBy, string sUserID, Int16 iLoadType,
            Int16 iFilterBy, string sFilterValue,
           string sOrderBy, short sIRBy)
        {
            int i = GlobalCode.Field2Int(HttpContext.Current.Session["CrewAssistRemarks_Count"]);
            return i;
        }
         /// <summary>
        /// Date Created:   26/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew User List
        /// --------------------------------------- 
        /// </summary>       
        public static List<UserList> GetUserList(string sUsername, bool bIsForCrewAssistRemarks)
        {
            return ReportDAL.GetUserList(sUsername, bIsForCrewAssistRemarks);
        }
        /// <summary>
        /// Date Created:   2526June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks to Export
        /// --------------------------------------- 
        /// </summary>       
        public DataTable GetCrewAssistRemarksToExport(string sUserID)
        {
            return DAL.GetCrewAssistRemarksToExport(sUserID);
        }
         /// <summary>
        /// Date Created:   29/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks By Date
        /// --------------------------------------- 
        /// </summary>       
        public List<RemarksByDateList> GetCrewAssistRemarksByDate(
            Int16 iRequestSourceID, bool bIsByDateRange,
            Int32 iYear, Int32 iMonth, DateTime dDateForm, DateTime dDateTo,
            string sUserID, Int16 iLoadType, string sOrderBy, int iStartRow, int iMaxRow)
        {
            return DAL.GetCrewAssistRemarksByDate( iRequestSourceID, bIsByDateRange,
             iYear, iMonth, dDateForm, dDateTo, sUserID, iLoadType,
             sOrderBy, iStartRow, iMaxRow);
        }
         /// <summary>
        /// Date Created:   29/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks to Export
        /// --------------------------------------- 
        /// </summary>       
        public DataTable GetCrewAssistRemarksByDateExport(string sUserID)
        {
            return DAL.GetCrewAssistRemarksByDateExport(sUserID);
        }
    }
}

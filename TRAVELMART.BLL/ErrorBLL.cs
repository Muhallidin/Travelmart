using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;

namespace TRAVELMART.BLL
{
    public class ErrorBLL
    {
        public static DataTable GetError(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            try
            {
                dt = ErrorDAL.GetError(DateFrom, DateTo, StartRow, MaxRow);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public static int GetErrorCount(DateTime DateFrom, DateTime DateTo)
        {
            return ErrorDAL.GetErrorCount(DateFrom, DateTo);
        }
        public static void InsertError(string sError, string sDescription, string sPageName, DateTime dDate, DateTime dDateGMT, string sUser)
        {
            ErrorDAL.InsertError(sError, sDescription, sPageName, dDate, dDateGMT, sUser);
        }
    }
}

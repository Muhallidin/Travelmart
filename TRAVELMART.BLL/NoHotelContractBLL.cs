using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;
using System.Web;

namespace TRAVELMART.BLL
{
    public class NoHotelContractBLL
    {
        HotelNoContractDAL ExceptionDAL = new HotelNoContractDAL();
        
        public static DataTable GetException(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            try
            {
                dt = HotelNoContractDAL.GetException(DateFrom, DateTo, StartRow, MaxRow);
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
        public static int GetExceptionCount(DateTime DateFrom, DateTime DateTo)
        {
            return HotelNoContractDAL.GetExceptionCount(DateFrom, DateTo);
        }

        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 07/03/2012
        /// Description: load all queries for exception bookings for new UI
        /// ----------------------------------------------------------------
        /// Modiofied By:   Josephine Gad
        /// Date Modified:  25/07/2012
        /// Description:    Change HotelTransactionException.ExceptionBooking to Session["HotelTransactionExceptionExceptionBooking"]
        ///                 Change HotelTransactionException.Hotels  to Session["HotelTransactionExceptionHotels"]
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        public void LoadHotelExceptionPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<HotelTransactionExceptionGenericClass> Exceptions = new List<HotelTransactionExceptionGenericClass>();
            try
            {
                Exceptions = ExceptionDAL.LoadNoHotelHotelExceptionPage(Date, UserId, Loadtype, RegionID, PortID);

                if (Exceptions.Count > 0)
                {
                    //HotelTransactionException.ExceptionBooking = Exceptions[0].ExceptionBooking;
                    //HotelTransactionException.Hotels = Exceptions[0].Hotels;
                    HttpContext.Current.Session["HotelTransactionExceptionExceptionBooking"] = Exceptions[0].ExceptionBooking;
                    HttpContext.Current.Session["HotelTransactionExceptionHotels"] = Exceptions[0].Hotels;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   11/13/2012
        /// Description:    load all queries for Non Turn Ports for new UI
        /// ----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   11/13/2012
        /// Description:    Remove NonTurnPortsGenericClass, DAL  has been changed already to session list
        /// ----------------------------------------------------------------
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        public void LoadNonTurnPortsExceptionPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            ExceptionDAL.LoadNonTurnPortsExceptionPage(Date, UserId, Loadtype, RegionID, PortID);
        }
        /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   21/Jan/2014
        /// Description:    Get NonTurn Port Manifest using dynamic table
        /// ===============================================================
        /// </summary>
        public void LoadNonTurnPortsExceptionPageDynamic(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            ExceptionDAL.LoadNonTurnPortsExceptionPageDynamic(Date, UserId, Loadtype, RegionID, PortID);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/Mar/2013
        /// Description:    Confirm record and get the new confirmed and cancelled record
        /// ---------------------------------------------------------------
        /// </summary>
        public static void ConfirmNonTurnPortList(string UserId, DateTime dDate, int iPort,
            bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            HotelNoContractDAL.ConfirmNonTurnPortList( UserId, dDate, iPort, 
                 bIsSave, sEmailTo, sEmailCc,
                strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/Jan/2014
        /// Description:    Confirm record and get the new confirmed and cancelled record using dynamic tables
        /// ---------------------------------------------------------------
        /// </summary>
        public static void ConfirmNonTurnPortListDynamic(string UserId, DateTime dDate, int iPort,
            bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            HotelNoContractDAL.ConfirmNonTurnPortListDynamic(UserId, dDate, iPort,
            bIsSave, sEmailTo, sEmailCc,  strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   24/Sept/2013
        /// Description:    Get confirmed and cancelled Nonturn Port Manifest
        /// ===============================================================
        /// </summary>
        public static DataSet GetNonTurnPortsExport(DateTime Date, string UserId, int RegionID, int PortID, string sOrderBy)
        {
            return HotelNoContractDAL.GetNonTurnPortsExport(Date, UserId, RegionID, PortID, sOrderBy);
        }
         /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   03/Feb/2014
        /// Description:    Get confirmed and cancelled Nonturn Port Manifest using dynamic table
        /// ===============================================================
        /// </summary>
        public static DataSet GetNonTurnPortsExportDynamic(DateTime Date, string UserId, int RegionID, int PortID, string sOrderBy)
        {
            return HotelNoContractDAL.GetNonTurnPortsExportDynamic(Date, UserId, RegionID, PortID, sOrderBy);

        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   11/13/2012
        /// Description:    load all queries for Non Turn Ports for new UI
        /// ----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   11/13/2012
        /// Description:    Remove NonTurnPortsGenericClass, DAL  has been changed already to session list
        /// ----------------------------------------------------------------
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        public List<NonTurnPortsList> LoadNonTurnPortsExceptionPageNew(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
           return ExceptionDAL.LoadNonTurnPortsExceptionPagNew(Date, UserId, Loadtype, RegionID, PortID);
        }

        public static void GetNonTurnPortsContractPageCount(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            HotelNoContractDAL.GetNonTurnPortsContractPageCount(Date, UserId, Loadtype, RegionID, PortID);
        }


        /// <summary>
        /// ===============================================================
        /// Modified By:    Muhallidin G Wali
        /// Date Created:   10/Feb/2016
        /// Description:    Get Service provider booking by statusID
        /// ===============================================================
        /// </summary>
        public GenericNonTurnPort LoadServiceProviderHotelBooking(short LT, string UserId, DateTime Date,
                int PortAgentID, int StatusTypeID, int Days)
        {
            HotelNoContractDAL DAL = new HotelNoContractDAL();
            return DAL.LoadServiceProviderHotelBooking(LT, UserId, Date, PortAgentID, StatusTypeID, Days);
        
        }
    }
}

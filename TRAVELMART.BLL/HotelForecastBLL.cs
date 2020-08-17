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
    public class HotelForecastBLL
    {
        private HotelForecastDAL DAL = new HotelForecastDAL();
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date            29/Jan/2015
        /// Description:    Get Forecast from Micro
        /// --------------------------------------------
        public List<HotelForecastForApprovalList> GetForecastManifestList(string sBranchName,
           string sDateFrom, string sDateTo,
            string sVesselCode, int sPortID,
            string sUser, string sRole, bool bIsHotelVendorView, Int16 LoadType, bool bShowAll,
            int StartRow, int MaxRow)
        {
            List<HotelForecastForApprovalList> list = new List<HotelForecastForApprovalList>();
            //if (LoadType == 0)
            //{
            //    HttpContext.Current.Session["HotelForecastMicroApproval_Count"] = 0;
            //}
            //else
            //{
            list = DAL.GetForecastManifestList(sBranchName,
               sDateFrom, sDateTo, sVesselCode, sPortID,
               sUser, sRole, bIsHotelVendorView, LoadType, bShowAll, StartRow, MaxRow);
            //}
            return list;
        }

        public Int32 GetForecastManifestCount(string sBranchName,
           string sDateFrom, string sDateTo,
           string sVesselCode, int sPortID,
           string sUser, string sRole,  bool bIsHotelVendorView,
           Int16 LoadType, bool bShowAll)
        {
            Int32 i;
            i = GlobalCode.Field2Int(HttpContext.Current.Session["HotelForecastMicroApproval_Count"]);
            return i;
        }
         /// <summary>            
        /// Date Created:   04/Feb/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast
        /// ----------------------------------------        
        /// </summary>

        public void UpdateForecastManifest(int iHotelID,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtRoomToBeAdded)
        {
            try
            {
                DAL.UpdateForecastManifest(iHotelID,
                    sUserName, sDescription, sFunction, sFileName,
                    dDateGMT, dtDateCreated, dtRoomToBeAdded);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtRoomToBeAdded != null)
                {
                    dtRoomToBeAdded.Dispose();
                }
            }
        }
         /// <summary>            
        /// Date Created:   05/May/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast to Override Room Blocks
        /// ----------------------------------------        
        /// </summary>
        public void ApproveForecastManifest(int iHotelID,
            //float fRateSGL, Int32 iCurrencySGL, float fTaxPercentSGL, bool bIsTaxInclusiveSGL,
            //float fRateDBL, Int32 iCurrencyDBL, float fTaxPercentDBL, bool bIsTaxInclusiveDBL,

            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtRoomToBeAdded)
        {
            try
            {
                DAL.ApproveForecastManifest(iHotelID,
                    //fRateSGL, iCurrencySGL, fTaxPercentSGL, bIsTaxInclusiveSGL,
                    //fRateDBL, iCurrencyDBL, fTaxPercentDBL, bIsTaxInclusiveDBL,

                    sUserName, sDescription, sFunction, sFileName,
                    dDateGMT, dtDateCreated, dtRoomToBeAdded);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtRoomToBeAdded != null)
                {
                    dtRoomToBeAdded.Dispose();
                }
            }
        }
         /// <summary>            
        /// Date Created:   07/Oct/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast to Override Room Blocks By RCCL
        /// ----------------------------------------        
        /// </summary>
        public void ApproveForecastManifestByRCCL(int iHotelID,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT, DateTime dtDateCreated, DataTable dtRoomToBeAdded)
        {
            try
            {
                DAL.ApproveForecastManifestByRCCL(iHotelID,
                    sUserName, sDescription, sFunction, sFileName,
                    dDateGMT, dtDateCreated, dtRoomToBeAdded);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtRoomToBeAdded != null)
                {
                    dtRoomToBeAdded.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   29/Jul/2015
        /// Created By:     Josephine Monteza
        /// (description)   Save Hotel Forecast through Request
        /// ----------------------------------------        
        /// </summary>
        public void RequestHotelRoom(DateTime dDate, bool bIsSGL, bool bISDBL, Int32 iBranchID,
            Int32 iBranchIDSGL, Int32 iBranchIDDBL, int iRoomCountSGL, int iRoomCountDBL,
            string sUserName, string sDescription, string sFunction, string sFileName,
                DateTime dDateGMT)
        {
            DAL.RequestHotelRoom(dDate, bIsSGL, bISDBL, iBranchID,
            iBranchIDSGL, iBranchIDDBL, iRoomCountSGL, iRoomCountDBL,
            sUserName, sDescription, sFunction, sFileName, dDateGMT);
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date            30/Jul/2015
        /// Description:    Get Forecast with action already
        /// </summary>
        /// <returns></returns>
        public static List<HotelWithAction> ValidateHotelForecast(int iBranchIDInt, string sDate)
        {
            return HotelForecastDAL.ValidateHotelForecast(iBranchIDInt, sDate);
        }
    }
}

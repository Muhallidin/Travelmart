using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;
using System.Data;


namespace TRAVELMART.BLL
{
    public class VehicleManifestBLL
    {
        VehicleManifestDAL DAL = new VehicleManifestDAL();

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/Oct/2013
        /// Descrption:     Get Vehicle Manifest
        /// =============================================================
        /// </summary>
        public List<VehicleManifestList> GetVehicleManifest(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
            int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy, Int16 iRouteFrom, Int16 iRouteTo,
            string sCityFrom, string sCityTo, string sStatus, string sRole, Int32 iBrandID, Int32 iVesselID)
        {
            DAL.GetVehicleManifest(dDate, sUserID, iRegionID, iPortID, iVehicleID,
                iStartRow, iMaxRow, iLoadType, sOrderBy, iRouteFrom, iRouteTo, sCityFrom, sCityTo, sStatus, sRole,
                iBrandID, iVesselID);

            return (List<VehicleManifestList>)HttpContext.Current.Session["VehiclManifest_ManifestList"];
        }
        
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Oct/2013
        /// Descrption:     Get vehicle manifest By Page Number
        /// =============================================================     
        /// </summary>
        public void GetVehicleManifestByPageNumber(string strUser, string UserRole, int StartRow, int RowCount, string loadType)
        {
            DAL.GetVehicleManifestByPageNumber(strUser, UserRole, StartRow, RowCount, loadType);
        }

        /// <summary>
        /// Author:         Marco Abejar
        /// Date Created:   10/Oct/2013
        /// Descrption:     Get Vehicle Manifest
        /// =============================================================
        /// </summary>
        public List<VehicleManifestList> GetVehicleManifestByVendor(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
            int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            DAL.GetVehicleManifestByVendor(dDate, sUserID, iRegionID, iPortID, iVehicleID,
                iStartRow, iMaxRow, iLoadType, sOrderBy);

            return (List<VehicleManifestList>)HttpContext.Current.Session["VehiclManifest_ManifestList"];
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/Oct/2013
        /// Descrption:     Get Vehicle Manifest
        /// =============================================================
        /// </summary>
        public int GetVehicleManifestCount(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
                Int16 iLoadType, string sOrderBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["VehiclManifest_ManifestCount"]);
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   08/Oct/2013
        /// Descrption:     Get vehicle vendor list 
        /// =============================================================
        /// </summary>
        /// <returns></returns>
        public List<VehicleVendorList> GetVehicleVendorList(string sUserID, string sRegionID, string sPort, string sVendorID)
        {
            return DAL.GetVehicleVendorList(sUserID, sRegionID, sPort, sVendorID);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   08/Oct/2013
        /// Descrption:     Update Vehicle Manifest if visible or hidden to Vendor
        /// =============================================================
        /// </summary>
        public void UpdateVehicleManifestShowHide(string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtManifest)
        {
            DAL.UpdateVehicleManifestShowHide(sUserName, sDescription, sFunction, sFilename,
            sGMTDate, dtManifest);
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   08/Oct/2013
        /// Descrption:     Get Potential SIGNOFF
        /// =============================================================
        /// </summary>
        public List<VehicleManifestList> GetPotentialSIGNOFF(short LoadType, int VehicleVendorID, DateTime Dates)
        {
            return DAL.GetPotentialSIGNOFF(LoadType, VehicleVendorID, Dates);
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/Oct/2013
        /// Description:    Confirm record and get the new confirmed and cancelled record
        /// ---------------------------------------------------------------
        /// </summary>
        public void ConfirmVehicleManifest(string UserId, DateTime dDate, int iBranchID,
            string sRole, bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.ConfirmVehicleManifest(UserId, dDate, iBranchID,
            sRole, bIsSave, sEmailTo, sEmailCc, strLogDescription, strFunction,
            strPageName, DateGMT, CreatedDate);
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   31/Oct/2013
        /// Descrption:     Get list for Vehicle Manifest excel file report
        /// =============================================================    
        /// </summary>
        public void GetVehicleManifestExport(string sUserID, string sRole)
        {
            DAL.GetVehicleManifestExport(sUserID, sRole);
        }

        /// <summary>
        /// Created By:     Muhallidin G Wali
        /// Date Created:   10/JAN/2014
        /// (description)   Get Air and hotel Detail for PDF print out
        /// </summary>
        public List<FlightHotelDetailPDF> GetFlightHotelDetailPDF(short LoadType, long SeafarerID, long TravelRequestID
                , long IDBigInt, int SeqNo)
        {
            return DAL.GetFlightHotelDetailPDF(LoadType, SeafarerID, TravelRequestID, IDBigInt, SeqNo);
        }
         /// =============================================================     
        /// Author:         Josephine Monteza
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save the record to confirm
        /// =============================================================            
        /// </summary>
        public void SaveVehicleManifestToConfirm(string UserID, bool IsSelected, Int64 iTravelReqID, Int64 iIDBigint)
        {
            DAL.SaveVehicleManifestToConfirm(UserID, IsSelected, iTravelReqID, iIDBigint);
        }

        /// =============================================================     
        /// Author:         Josephine Monteza
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save all the record to confirm
        /// =============================================================            
        /// </summary>
        public void SaveVehicleManifestToConfirmAll(string UserID, bool IsSelected)
        {
            DAL.SaveVehicleManifestToConfirmAll(UserID, IsSelected);
        }

        /// =============================================================     
        /// Author:         Muhallidin 
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save all the record to confirm
        /// =============================================================            
        /// </summary>
        public DriverGreeterVehHotelServProv DriverGreeterVehHotelServProv(short LT, int VendorID, string ConfirmedManifestID)
        {
            try
            {
                return DAL.DriverGreeterVehHotelServProv(LT, VendorID, ConfirmedManifestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// =============================================================     
        /// Author:        Muhallidin 
        /// Date Created:  06-Oct-2017
        /// Descrption:    Save Driver transaction
        /// =============================================================            
        /// </summary>
        public void SaveDriverTransaction(int VendorID, string UserID, string TransactionID, DataTable DriverTransaction, DataTable GreeterTransaction)
        {
            try
            {
                DAL.SaveDriverTransaction(VendorID, UserID, TransactionID, DriverTransaction, GreeterTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
    }
}

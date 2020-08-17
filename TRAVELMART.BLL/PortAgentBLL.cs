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
    public class PortAgentBLL
    {
        PortAgentDAL DAL = new PortAgentDAL();
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Get Service Provider, Hotel manifest and vehicle manifest of user
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGet(int iPortAgentID, string sDate, string sUserID, string sRole, string sOrderBy,
            int iSeaportID, Int16 iLoadType, int iNoOfDay)
        {
            DAL.PortAgentManifestGet(iPortAgentID, sDate, sUserID, sRole, sOrderBy, iSeaportID, iLoadType, iNoOfDay);
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author: Michael Brian C. Evangelista
        /// Date Created:   14/Aug/2014
        /// Description:    Get Service Provider, Hotel manifest and vehicle manifest of user
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetwithStatus(int iPortAgentID, string sDate, string sUserID, string sRole, string sOrderBy,
            int iSeaportID, Int16 iLoadType, int iNoOfDay, string status)
        {
            DAL.PortAgentManifestGetwithStatus(iPortAgentID, sDate, sUserID, sRole, sOrderBy, iSeaportID, iLoadType, iNoOfDay, status);
        }


        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   08/Mar/2014
        /// Descrption:     Get Hotel Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmHotel(int iStatusID, int iPortAgentID, string sDate, string sUserID,
            string sRole, string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
            DAL.PortAgentManifestGetConfirmHotel(iStatusID, iPortAgentID, sDate, sUserID, sRole, sOrderBy, iLoadType, iNoOfDay, iSFID, iStartRow, iMaxRow);            
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   12/Mar/2014
        /// Descrption:     Get Vehicle Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmVehicle(int iStatusID, int iPortAgentID, string sDate, string sUserID,
            string sRole, string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
            DAL.PortAgentManifestGetConfirmVehicle(iStatusID, iPortAgentID, sDate, sUserID, sRole, sOrderBy, iLoadType, iNoOfDay, iSFID, iStartRow, iMaxRow);
        }
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   08/Mar/2014
        /// Descrption:     Get Hotel Manifest TO confirm
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmHotelToAdd(DataTable dt, string UserId, string sRole,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            try
            {
                DAL.PortAgentManifestGetConfirmHotelToAdd(dt, UserId, sRole, strLogDescription, strFunction,
                strPageName, DateGMT, CreatedDate);
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
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2014
        /// Descrption:     Get Hotel Manifest TO confirm
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmHotelToAddFromNonTurn(int iPortID, string UserId, string sRole,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.PortAgentManifestGetConfirmHotelToAddFromNonTurn(iPortID, UserId, sRole, strLogDescription, strFunction,
               strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// --------------------------------------------------------------------
        /// Author:         Michael Evangelista
        /// Date Created:   30-09-2015
        /// Description:    Get PortAgent defaults 
        /// --------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmHotelToAddFromNonTurn2(int iPortID,string UserId, string sRole,
            string strLogDescription, string strFunction, string PageName,DateTime DateGMT, DateTime CreatedDate) 
        {
            DAL.PortAgentManifestGetConfirmHotelToAddFromNonTurn2(iPortID, UserId, sRole, strLogDescription, strFunction,
                   PageName, DateGMT, CreatedDate);
        }



        ///<summary>
        /// -------------------------------------------------------------------
        /// Author:         Michael Evangelista
        /// Date Created:   02/Oct/2015
        /// Description:    Get Service Provider/Hotel Details for Hotel Request
        /// -------------------------------------------------------------------
        /// </summary>

        public void PortAgentGetConfirmHotelToAdd(DataTable dt,string UserId, string sRole, int PortId,int VendorId , string VendorType,string strLogDescription, string strFunction, DateTime DateGMT, DateTime CreatedDate)
        {
            try
            {
                DAL.PortAgentGetConfirmHotelToAdd(dt,UserId,sRole,PortId,VendorId,VendorType,strLogDescription, strFunction, DateGMT,CreatedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                if(dt != null)
                {
                    dt.Dispose();
                }
            

            }
        }

        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   12/Mar/2014
        /// Descrption:     Get Vehicle Manifest TO confirm
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmVehicleToAdd(DataTable dt, string UserId, string sRole,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            try
            {
                DAL.PortAgentManifestGetConfirmVehicleToAdd(dt, UserId, sRole, strLogDescription, strFunction,
                   strPageName, DateGMT, CreatedDate);
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
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   11/Apr/2014
        /// Descrption:     Get Vehicle Manifest To confirm from Non Turn Port page
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmVehicleToAddFromNonTurn(int iPortID, string UserId, string sRole,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.PortAgentManifestGetConfirmVehicleToAddFromNonTurn(iPortID, UserId, sRole, strLogDescription, strFunction,
                  strPageName, DateGMT, CreatedDate);
        }
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   31/Mar/2014
        /// Descrption:     Get Vehicle Manifest TO confirm
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmVehicleToAddWithOrder(string UserId, string sRole,
            string sOrderBy)
        {
            DAL.PortAgentManifestGetConfirmVehicleToAddWithOrder(UserId, sRole, sOrderBy);
        }
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   01/Apr/2014
        /// Descrption:     Get Hotel Manifest TO confirm with order
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmHotelToAddWithOrder(string UserId, string sRole,
            string sOrderBy)
        {
            DAL.PortAgentManifestGetConfirmHotelToAddWithOrder(UserId, sRole, sOrderBy);
        }
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   10/Mar/2014
        /// Descrption:     Confirm hotel manifest of Service Provider
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotel(string sUser,string sRole, string sEmailTo,
            string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sHotelName, string sComment, string sConfirmedBy, string sRequestSource,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            try
            {
                DAL.PortAgentManifestConfirmHotel(sUser, sRole, sEmailTo,
                    sEmailCC, iCurrency, fRateConfirmed, sConfirmationNo,
                    sHotelName, sComment, sConfirmedBy, sRequestSource,
                    strLogDescription, strFunction, strPageName,
                    DateGMT, CreatedDate, dt);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   17/Mar/2014
        /// Descrption:     Approve hotel manifest of Service Provider by RCCL
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotelApprove(string sUser, string sRole, string sEmailTo,
            string sEmailCC, string sComment, string sConfirmedBy, string sRequestSource,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.PortAgentManifestConfirmHotelApprove(sUser, sRole, sEmailTo,
              sEmailCC, sComment, sConfirmedBy, sRequestSource, strLogDescription, strFunction, strPageName,
              DateGMT, CreatedDate);
        }
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   17/Mar/2014
        /// Descrption:     Cancel hotel manifest of Service Provider by RCCL
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotelCancel(string sUser, string sRole, string sEmailTo,
            string sEmailCC, string sComment, string sConfirmedBy, string sRequestSource, string strLogDescription, 
            string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.PortAgentManifestConfirmHotelCancel(sUser, sRole, sEmailTo,
              sEmailCC, sComment, sConfirmedBy, sRequestSource, strLogDescription, strFunction, strPageName,
              DateGMT, CreatedDate);
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   15/Mar/2014
        /// Descrption:     Confirm hotel amount of Service Provider manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotelAmount(string sUser, string sRole, string sEmailTo,
            string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sHotelName, string sComment, string sConfirmedBy, string sRequestSource,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            try
            {
                DAL.PortAgentManifestConfirmHotelAmount(sUser, sRole, sEmailTo,
                 sEmailCC, iCurrency, fRateConfirmed, sConfirmationNo,
                 sHotelName, sComment, sConfirmedBy, sRequestSource, strLogDescription,
                 strFunction, strPageName, DateGMT, CreatedDate, dt);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   10/Apr/2014
        /// Descrption:     Create hotel request of Service Provider
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotelAdd(string sUser, string sRole, string sEmailTo,
            string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sHotelName, string sComment, string sConfirmedBy, string sRequestSource,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            try
            {
                DAL.PortAgentManifestConfirmHotelAdd(sUser, sRole, sEmailTo,
                sEmailCC, iCurrency, fRateConfirmed, sConfirmationNo,
                sHotelName, sComment, sConfirmedBy, sRequestSource,
                strLogDescription, strFunction, strPageName,
                DateGMT, CreatedDate, dt);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   13/Mar/2014
        /// Descrption:     Confirm vehicle manifest of Service Provider
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicle(string sUser, string sRole, string sEmailTo,
            string sEmailCC, string sTimeSpan, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sVehicleName, string sDriverName, string sPlateNo, Int16 iVehicleTypeIDInt,
            string sComment, string sConfirmedBy, Int32 iContractIDInt, string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt, string sTransportationDetails)
        {
            try
            {
                DAL.PortAgentManifestConfirmVehicle(sUser, sRole, sEmailTo, sEmailCC, sTimeSpan, iCurrency,
                    fRateConfirmed, sConfirmationNo, sVehicleName, sDriverName, sPlateNo, iVehicleTypeIDInt,
                    sComment, sConfirmedBy, iContractIDInt, strLogDescription, strFunction, strPageName,
                    DateGMT, CreatedDate, dt, sTransportationDetails);
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
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   11/Ap3/2014
        /// Descrption:     Add vehicle request of Service Provider from NonTurnPort Page
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicleAdd(string sUser, string sRole, string sEmailTo,
           string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
           string sComment, string sConfirmedBy, string sRequestSource, Int32 iContractIDInt, Int16 iVehicleTypeId,
           string strLogDescription, string strFunction, string strPageName,
           DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            try
            {
                DAL.PortAgentManifestConfirmVehicleAdd(sUser, sRole, sEmailTo,
                sEmailCC, iCurrency, fRateConfirmed, sConfirmationNo,
                sComment, sConfirmedBy, sRequestSource, iContractIDInt, iVehicleTypeId,
                strLogDescription, strFunction, strPageName,
                DateGMT, CreatedDate, dt);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   24/Mar/2014
        /// Descrption:     Confirm vehicle amount of Service Provider manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicleAmount(string sUser, string sRole, string sEmailTo,
            string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sComment, string sConfirmedBy, string sRequestSource,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            try
            {
                DAL.PortAgentManifestConfirmVehicleAmount(sUser, sRole, sEmailTo,
                sEmailCC, iCurrency, fRateConfirmed, sConfirmationNo,
                sComment, sConfirmedBy, sRequestSource, strLogDescription,
                strFunction, strPageName, DateGMT, CreatedDate, dt);
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
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   24/Mar/2014
        /// Descrption:     Approve vehicle manifest of Service Provider by RCCL
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicleApprove(string sUser, string sRole, string sEmailTo,
           string sEmailCC, string sComment, string sConfirmedBy, string sRequestSource,
           string strLogDescription, string strFunction, string strPageName,
           DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.PortAgentManifestConfirmVehicleApprove(sUser, sRole, sEmailTo,
            sEmailCC, sComment, sConfirmedBy, sRequestSource, strLogDescription, strFunction,
            strPageName, DateGMT, CreatedDate);
        }       
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   24/Mar/2014
        /// Descrption:     Cancel vehicle manifest of Service Provider by RCCL
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicleCancel(string sUser, string sRole, string sEmailTo,
            string sEmailCC, string sComment, string sConfirmedBy, string sRequestSource, string strLogDescription,
            string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.PortAgentManifestConfirmVehicleCancel(sUser, sRole, sEmailTo,
             sEmailCC,  sComment, sConfirmedBy, sRequestSource, strLogDescription,
             strFunction, strPageName, DateGMT, CreatedDate);
        }
         /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   13/Mar/2014
        /// Descrption:     Get Service Provider List based from Seaport ID
        /// -------------------------------------------------------------------
        /// </summary>
        public List<PortAgentDTO> GetPortAgentListByPortId(int iPortID, string sUser, string sRole)
        {
            return DAL.GetPortAgentListByPortId(iPortID, sUser, sRole);
        }
          /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2014
        /// Descrption:     Get Manifest Status List
        /// -------------------------------------------------------------------
        /// </summary>
        public List<ManifestStatus> GetManifestStatus()
        {
            return DAL.GetManifestStatus();
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   19/Mar/2014
        /// Descrption:     Get Hotel Manifest to Export
        /// -------------------------------------------------------------------
        /// </summary>
        public DataTable PortAgentManifestGetConfirmHotelExport(string sUserID)
        {
            return DAL.PortAgentManifestGetConfirmHotelExport(sUserID);
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   26/Mar/2014
        /// Descrption:     Get Vehicle Manifest to Export
        /// -------------------------------------------------------------------
        /// </summary>
        public DataTable PortAgentManifestGetConfirmVehicleExport(string sUserID)
        {
            return DAL.PortAgentManifestGetConfirmVehicleExport(sUserID);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/Mar/2014
        /// Descrption:     Get Remarks
        /// -------------------------------------------------------------------
        /// </summary>        
        public List<TransactionRemarks> GetTransactionRemarks(Int64 iTRid, string sRecloc, Int16 iExpenseType)
        {
            return DAL.GetTransactionRemarks(iTRid, sRecloc, iExpenseType);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   11/Apr/2014
        /// Descrption:     Get Service Provider Vehicle Contract Amt
        /// -------------------------------------------------------------------
        /// </summary>        
        public List<PortAgentVehicleContractAmt> GetPortAgentVehicleContractAmt(Int32 iPortID, Int16 iVehicleType)
        {
            return DAL.GetPortAgentVehicleContractAmt(iPortID, iVehicleType);
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   06/Nov/2014
        /// Descrption:     Get List of OK to Board in Brazil
        /// -------------------------------------------------------------------
        /// </summary>        
        public List<OkToBrazilList> GetOKToBrazilList(string sUser, DateTime dDate, string sOrderBy, int iStartRow, int iMaxRow)
        {
            return DAL.GetOKToBrazilList(sUser, dDate, sOrderBy, iStartRow, iMaxRow);
        }
        public Int32 GetOKToBrazilCount(string sUser, DateTime dDate, string sOrderBy)
        {
            Int32 i = 0;
            i = GlobalCode.Field2Int(HttpContext.Current.Session["OkToBrazilCount"]);
            return i;
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   07/Nov/2014
        /// Descrption:     Get List of OK to Board in Brazil to export
        /// -------------------------------------------------------------------
        /// </summary>        
        public DataTable GetOKToBrazilExport(string sUser)
        {
            return DAL.GetOKToBrazilExport(sUser);
        }



        
        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Muhallidin F Wali
        ///  Date Created:  12-10-2015
        ///  Description:   Get Hotel Manifest TO confirm for new non-turn port
        ///  ------------------------------------------------------------------
        /// </summary>

        public List<NonTurnportGenericList> GetPortNonTurnHotelReques(int LoadType,  DateTime RequestDate, int PortID,int PortAgentID, string UserID, DataTable dt)
        { 
            DAL =  new PortAgentDAL();
            return DAL.GetPortNonTurnHotelRequest(LoadType, RequestDate, PortID,PortAgentID, UserID, dt);
        }

        ///<summary>
        ///Date Created:    09/09/2015
        ///Created By:      Muhallidin G Wali
        ///Description:     Add Hotel Vendor List for Create Hotel Request on Non Turn Ports
        ///</summary>
        ///

        public List<PortAgenRequestVendor> GetNonTurnporHotelProviderVendor(short loadType, int VendorID, int contractID)
        {

            DAL = new PortAgentDAL();
            return DAL.GetNonTurnporHotelProviderVendor(loadType, VendorID, contractID);

        }


        public string InsertNonTurnTransactionRequestBooking(DataTable dt,  string spName, string userID, string emailTo , string emailCC)
        {
            DAL = new PortAgentDAL();
            return DAL.InsertNonTurnTransactionRequestBooking(dt, spName, userID, emailTo, emailCC);

        }



        
        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Muhallidin G Wali
        ///  Date Created:  12-10-2015
        ///  Description:   Get Hotel Manifest TO confirm for new non-turn port
        ///  ------------------------------------------------------------------
        /// </summary>

        public List<NonTurnportGenericList> GetPortNonTurnTransportationRequest(int LoadType, DateTime RequestDate, int PortID, string UserID, DataTable dt)
        {
            DAL = new PortAgentDAL();
            return DAL.GetPortNonTurnTransportationRequest(LoadType, RequestDate, PortID , UserID, dt);

        }

        public List<NonTurnportGenericList> GetPortNonTurnTransportationRequest(int LoadType, DateTime RequestDate, int PortID, long PortAgentID, string UserID, DataTable dt)
        {
            DAL = new PortAgentDAL();
            return DAL.GetPortNonTurnTransportationRequest(LoadType, RequestDate, PortID,PortAgentID, UserID, dt);


        }

        public List<NonTurnTransportation> GetNonTurnporTransportionVendor(short loadType, int VendorID, int contractID)
        {

            DAL = new PortAgentDAL();
            return DAL.GetNonTurnporTransportionVendor(loadType, VendorID, contractID);


        }

        public List<PortAgentHotelVehicle> GetNonTurnporHotelVehicleDashboard(short loadType, string PortCode
                , int PortID, int PortAgentID, DateTime RequestDate, string UserID, int Days)
        {
            DAL = new PortAgentDAL();
            return DAL.GetNonTurnporHotelVehicleDashboard(loadType,  PortCode, PortID, PortAgentID, RequestDate,UserID, Days);

        }
        public  SeaportPortagentDaysList GetSeaportPortagentDays(string UserID)
        {
            DAL = new PortAgentDAL();
            return DAL.GetSeaportPortagentDays(UserID);
        }


        
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Muhallidin G. Wali
        /// Date Created:   13/Nov/2016
        /// Descrption:     Get Hotel confrim/cancel Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public PortAgentConfirmCancelledManifest PortAgentConfirmCancelledManifest(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, short iLoadType, int iNoOfDay, long iSFID, int iStartRow, int iMaxRow)
        {
            DAL = new PortAgentDAL();
            return DAL.PortAgentConfirmCancelledManifest( iStatusID,  iPortAgentID,  sDate,  sUserID,  sRole, sOrderBy,  iLoadType,  iNoOfDay,  iSFID,  iStartRow,  iMaxRow);
            
        }


        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         MUhallidin G Wali
        /// Date Created:   18/Nov/2016
        /// Descrption:     Get get vehicle request pending with rccl user
        /// -------------------------------------------------------------------
        /// </summary>
        public PortAgentConfirmCancelledManifest PortAgentVehicleRequestdManifest(int iStatusID, int iPortAgentID, string sDate, string sUserID,
            string sRole, string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
          return  DAL.PortAgentVehicleRequestdManifest(iStatusID, iPortAgentID, sDate, sUserID, sRole, sOrderBy, iLoadType, iNoOfDay, iSFID, iStartRow, iMaxRow);
        }


         public PortAgentConfirmCancelledManifest GetPortAgentConfirmVehicleManifest(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {


            return DAL.GetPortAgentConfirmVehicleManifest(iStatusID, iPortAgentID, sDate, sUserID, sRole, sOrderBy, iLoadType, iNoOfDay, iSFID, iStartRow, iMaxRow);
        }
    }
}

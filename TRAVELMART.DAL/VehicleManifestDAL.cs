using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using TRAVELMART.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class VehicleManifestDAL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Oct/2013
        /// Descrption:     Get vehicle manifest 
        /// =============================================================     
        /// Author:         Marco Abejar
        /// Date Created:   14/Oct/2013
        /// Descrption:     Added driver name 
        /// =============================================================    
        /// Modified By:    Josephine Gad
        /// Date Modified:  21/May/2014
        /// Descrption:     Add sRole parameter
        ///                 Add Nationality column
        /// =============================================================            
        /// </summary>
        public void GetVehicleManifest(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID, 
            int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy, Int16 iRouteFrom, Int16 iRouteTo, 
            string sCityFrom, string sCityTo, string sStatus, string sRole, Int32 iBrandID, Int32 iVesselID)
        {
            Int32 iCount = 0;
            Int32 iCountConfirm = 0;
            Int32 iCountCancel = 0;
            List<VehicleManifestList> TentativeManifest = new List<VehicleManifestList>();
            List<VehicleManifestList> ConfirmedManifest = new List<VehicleManifestList>();
            List<VehicleManifestList> CancelledManifest = new List<VehicleManifestList>();

            List<VehicleVendorList> listVehicle = new List<VehicleVendorList>();
            List<VehicleRoute> listRoute = new List<VehicleRoute>();
            List<VehicleCountList> listCount = new List<VehicleCountList>();

            //List<AirportDTO> listAir = new List<AirportDTO>();
            //List<SeaportDTO> listSea = new List<SeaportDTO>();
                        
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dtManifestNew = null;
            DataTable dtManifestConfirm = null;
            DataTable dtManifestCancel = null;

            DataTable dtVehicle = null;
            DataTable dtRoute = null;
            DataTable dtCount = null;
            
            DataSet ds = null;

            HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
            HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = CancelledManifest;

            HttpContext.Current.Session["VehiclManifest_ManifestCount"] = iCount;
            HttpContext.Current.Session["VehiclManifest_ConfirmedManifesCount"] = iCountConfirm;
            HttpContext.Current.Session["VehiclManifest_CancelledManifestCount"] = iCountCancel;

            HttpContext.Current.Session["VehiclManifest_VehicleVendor"] = listVehicle;
            HttpContext.Current.Session["VehiclManifest_VehicleRoute"] = listRoute;
            HttpContext.Current.Session["VehiclManifest_VehicleCountList"] = listCount;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifest");
                dbCommand.CommandTimeout = 60;

                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, dDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, iRegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, iPortID);
                db.AddInParameter(dbCommand, "@pcolVehicleVendorIDInt", DbType.Int32, iVehicleID);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);

                db.AddInParameter(dbCommand, "@pRouteFrom", DbType.Int16, iRouteFrom);
                db.AddInParameter(dbCommand, "@pRouteTo", DbType.Int16, iRouteTo);

                db.AddInParameter(dbCommand, "@pCityFrom", DbType.String, GlobalCode.Field2String(sCityFrom));
                db.AddInParameter(dbCommand, "@pCityTo", DbType.String, GlobalCode.Field2String(sCityTo));
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, GlobalCode.Field2String(sStatus));
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pBrandID", DbType.Int32, iBrandID);
                db.AddInParameter(dbCommand, "@pVesseID", DbType.Int32, iVesselID);

                ds = db.ExecuteDataSet(dbCommand);

                iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                dtManifestNew = ds.Tables[1];

                iCountConfirm = GlobalCode.Field2Int(ds.Tables[2].Rows[0][0]);
                dtManifestConfirm = ds.Tables[3];

                iCountCancel = GlobalCode.Field2Int(ds.Tables[4].Rows[0][0]);
                dtManifestCancel = ds.Tables[5];

                dtCount = ds.Tables[6];
                if (iLoadType == 0)
                {
                    dtVehicle = ds.Tables[7];
                    dtRoute = ds.Tables[8];

                    listVehicle = (from a in dtVehicle.AsEnumerable()
                                   select new VehicleVendorList
                                   {
                                       VehicleVendorID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                       VehicleVendorName = a.Field<string>("colVehicleVendorNameVarchar"),
                                   }).ToList();

                    listRoute = (from a in dtRoute.AsEnumerable()
                                 select new VehicleRoute
                                 {
                                     RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                     RouteDesc = GlobalCode.Field2String(a["colRouteNameVarchar"])
                                 }).ToList();
                }
                               
                TentativeManifest = (from a in dtManifestNew.AsEnumerable()
                        select new VehicleManifestList {
                         
                            TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                            SeafarerIdInt =  GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                            LastName = a.Field<string>("LastName"),
                            FirstName = a.Field<string>("FirstName"),
                            colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                            colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                            RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                            OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                            colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                            colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                            VehicleVendorname = a.Field<string>("VehicleVendorname"),
                            VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),
                            DriverName = a.Field<string>("colDriverName"),
        
                            colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                            colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                            colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                            colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),
        
                            colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                            colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),
        
                            colVehicleTypeIdInt  = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                            VehicleTypeName = a.Field<string>("VehicleTypeName"),
                            colSFStatus = a.Field<string>("colSFStatus"),
                            RouteFrom = a.Field<string>("RouteFrom"),
                            RouteTo = a.Field<string>("RouteTo"),

                            colFromVarchar = a.Field<string>("colFromVarchar"),
                            colToVarchar  = a.Field<string>("colToVarchar"),
                            BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                            HotelVendorName = a.Field<string>("HotelVendorName"),
        //colRankIDInt, 
                            RankName = a.Field<string>("RankName"),
        //colCostCenterIDInt, 
                            CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                            Nationality = a.Field<string>("Nationality"),

                            colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                            colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                            Gender = a.Field<string>("Gender"),
                            colVesselIdInt =  GlobalCode.Field2Int(a["colVesselIdInt"]),
                            VesselName = a.Field<string>("VesselName"),
                            VehicleDispatchTime = a.Field<string>("colVehicleDispatchTime"),

                            FlightNo = a.Field<string>("FlightNo"),
                            Carrier = a.Field<string>("Carrier"),

                            SeqNo = GlobalCode.Field2TinyInt(a["colSeqNoInt"]),
                            Departure = a.Field<string>("Departure"),
                            Arrival = a.Field<string>("Arrival"),

                            DateDep = a.Field<DateTime?>("DeptDate"),
                            DateArr = a.Field<DateTime?>("ArrDate"),

                            /* for tagging vehicle*/
                            
                            TaggedActive = GlobalCode.Field2Bool(a["colIsActiveBitTagged"]),
                            TaggedVehicleVendorId = GlobalCode.Field2Int(a["colIsActiveBitTagged"]),

                            /* end of tagging vehicle*/
                            /*for Flightstats*/
                            ActualDepartureDate = GlobalCode.Field2String(a["actDateD"]),
                            ActualArrivalDate = GlobalCode.Field2String(a["actDateT"]),
                            ActualArrivalGate = GlobalCode.Field2String(a["FlightArrGate"]),
                            ActualArrivalStatus = GlobalCode.Field2String(a["FlightStatus"]),
                            ActualArrivalBaggage = GlobalCode.Field2String(a["FlightBaggageClaim"]),
                            /*end of flightstats*/

                            PassportNo = a.Field<string>("PassportNo"),
                            PassportExp = a.Field<string>("PassportExp"),
                            PassportIssued = a.Field<string>("PassportIssued"),
                            Birthday = a.Field<DateTime?>("Birthday"),

                            IsToConfirm = GlobalCode.Field2Bool(a["IsToConfirm"])
                        }).ToList();


                ConfirmedManifest = (from a in dtManifestConfirm.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         ConfirmManifestID = GlobalCode.Field2Long(a["colConfirmedManifestIDBigint"]),
                                         Remarks = a.Field<string>("Remarks"),
                                         TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                         colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                         VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                         VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),
                                         DriverName = a.Field<string>("colDriverName"),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                         colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                         colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                         colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                         colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                         colSFStatus = a.Field<string>("colSFStatus"),
                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         //colRankIDInt, 
                                         RankName = a.Field<string>("RankName"),
                                         //colCostCenterIDInt, 
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         Nationality = a.Field<string>("Nationality"),
                                         colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                         colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                         VesselName = a.Field<string>("VesselName"),

                                         /*for Flightstats*/
                                         ActualDepartureDate = GlobalCode.Field2String(a["actDateD"]),
                                         ActualArrivalDate = GlobalCode.Field2String(a["actDateT"]),
                                         ActualArrivalGate = GlobalCode.Field2String(a["FlightArrGate"]),
                                         ActualArrivalStatus = GlobalCode.Field2String(a["FlightStatus"]),
                                         ActualArrivalBaggage = GlobalCode.Field2String(a["FlightBaggageClaim"]),
                                         /*end of flightstats*/



                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                         FlightNo = a.Field<string>("FlightNo"),
                                         Carrier = a.Field<string>("Carrier"),


                                         SeqNo = GlobalCode.Field2TinyInt(a["colSeqNoInt"]),
                                         Departure = a.Field<string>("Departure"),
                                         Arrival = a.Field<string>("Arrival"),

                                         DateDep = a.Field<DateTime?>("DeptDate"),
                                         DateArr = a.Field<DateTime?>("ArrDate"),

                                         /* for tagging vehicle*/

                                         TaggedActive = GlobalCode.Field2Bool(a["colIsActiveBitTagged"]),
                                         TaggedVehicleVendorId = GlobalCode.Field2Int(a["colIsActiveBitTagged"]),

                                         /* end of tagging vehicle*/

                                         PassportNo = a.Field<string>("PassportNo"),
                                         PassportExp = a.Field<string>("PassportExp"),
                                         PassportIssued = a.Field<string>("PassportIssued"),
                                         Birthday = a.Field<DateTime?>("Birthday"),

                                         GreeterID = GlobalCode.Field2Int(a["GreeterID"]),
                                         GreeterName = a.Field<string>("GreeterName"),

                                     }).ToList();


                CancelledManifest = (from a in dtManifestCancel.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                         colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                         VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                         VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                         colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                         colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                         colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                         colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                         colSFStatus = a.Field<string>("colSFStatus"),
                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         //colRankIDInt, 
                                         RankName = a.Field<string>("RankName"),
                                         //colCostCenterIDInt, 
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         Nationality = a.Field<string>("Nationality"),

                                         colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                         colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                         VesselName = a.Field<string>("VesselName"),

                                         //ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         //ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                         PassportNo = a.Field<string>("PassportNo"),
                                         PassportExp = a.Field<string>("PassportExp"),
                                         PassportIssued = a.Field<string>("PassportIssued"),
                                         Birthday = a.Field<DateTime?>("Birthday"),
                                     }).ToList();

               

                listCount = (from a in dtCount.AsEnumerable()
                             select new VehicleCountList
                             {
                                 OnOffDate = GlobalCode.Field2Int(a["OnOffCOunt"]),
                                 Status = GlobalCode.Field2String(a["colSFStatus"])
                             }).ToList();

               
                HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = CancelledManifest;

                HttpContext.Current.Session["VehiclManifest_ManifestCount"] = iCount;
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifesCount"] = iCountConfirm;
                HttpContext.Current.Session["VehiclManifest_CancelledManifestCount"] = iCountCancel;

                HttpContext.Current.Session["VehiclManifest_VehicleVendor"] = listVehicle;
                HttpContext.Current.Session["VehiclManifest_VehicleRoute"] = listRoute;
                HttpContext.Current.Session["VehiclManifest_VehicleCountList"] = listCount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtManifestNew != null)
                {
                    dtManifestNew.Dispose();
                }
                if (dtManifestConfirm != null)
                {
                    dtManifestConfirm.Dispose();
                }
                if (dtManifestCancel != null)
                {
                    dtManifestCancel.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }
                if (dtCount != null)
                {
                    dtCount.Dispose();
                }
                //if (dtTo != null)
                //{
                //    dtTo.Dispose();
                //}
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   14/Oct/2013
        /// Descrption:     Get vehicle manifest By Page Number
        /// =============================================================     
        /// </summary>
        public void GetVehicleManifestByPageNumber(string strUser, string UserRole, int StartRow, int RowCount, string loadType)
        {           
            List<VehicleManifestList> list = new List<VehicleManifestList>();
            if (loadType == "Confirm")
            {
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = list;
            }
            if (loadType == "Cancel")
            {
                HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = list;
            }
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = null;
            DataSet ds = null;         

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifestByPageNumber");
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, RowCount);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, loadType);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];

                if (loadType == "Confirm")
                {
                    list = (from a in dt.AsEnumerable()
                            select new VehicleManifestList
                            {
                                Remarks = a.Field<string>("Remarks"),
                                TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                LastName = a.Field<string>("LastName"),
                                FirstName = a.Field<string>("FirstName"),
                                colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                colSFStatus = a.Field<string>("colSFStatus"),
                                RouteFrom = a.Field<string>("RouteFrom"),
                                RouteTo = a.Field<string>("RouteTo"),

                                colFromVarchar = a.Field<string>("colFromVarchar"),
                                colToVarchar = a.Field<string>("colToVarchar"),
                                BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                HotelVendorName = a.Field<string>("HotelVendorName"),
                                //colRankIDInt, 
                                RankName = a.Field<string>("RankName"),
                                //colCostCenterIDInt, 
                                CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                Gender = a.Field<string>("Gender"),
                                colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                VesselName = a.Field<string>("VesselName"),

                                ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                ConfirmedDate = a.Field<string>("ConfirmedDate"),

                            }).ToList();

                }
                else if (loadType == "Cancel")
                {

                    list = (from a in dt.AsEnumerable()
                            select new VehicleManifestList
                            {
                                TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                LastName = a.Field<string>("LastName"),
                                FirstName = a.Field<string>("FirstName"),
                                colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                colSFStatus = a.Field<string>("colSFStatus"),
                                RouteFrom = a.Field<string>("RouteFrom"),
                                RouteTo = a.Field<string>("RouteTo"),

                                colFromVarchar = a.Field<string>("colFromVarchar"),
                                colToVarchar = a.Field<string>("colToVarchar"),
                                BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                HotelVendorName = a.Field<string>("HotelVendorName"),
                                //colRankIDInt, 
                                RankName = a.Field<string>("RankName"),
                                //colCostCenterIDInt, 
                                CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                Gender = a.Field<string>("Gender"),
                                colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                VesselName = a.Field<string>("VesselName"),

                                //ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                //ConfirmedDate = a.Field<string>("ConfirmedDate"),

                            }).ToList();

                }
                if (loadType == "Confirm")
                {
                    HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = list;
                }
                if (loadType == "Cancel")
                {
                    HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = list;
                }
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }                
            }
        }
        /// <summary>
        /// Author:         Marco Abejar
        /// Date Created:   10/Oct/2013
        /// Descrption:     Get vehicle manifest by vendor
        /// =============================================================     
        /// </summary>
        public void GetVehicleManifestByVendor(DateTime dDate, string sUserID, int iRegionID, int iPortID, int iVehicleID,
           int iStartRow, int iMaxRow, Int16 iLoadType, string sOrderBy)
        {
            Int32 iCount = 0;
            Int32 iCountConfirm = 0;
            Int32 iCountCancel = 0;
            List<VehicleManifestList> TentativeManifest = new List<VehicleManifestList>();
            List<VehicleManifestList> ConfirmedManifest = new List<VehicleManifestList>();
            List<VehicleManifestList> CancelledManifest = new List<VehicleManifestList>();

            List<VehicleVendorList> listVehicle = new List<VehicleVendorList>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dtManifestNew = null;
            DataTable dtManifestConfirm = null;
            DataTable dtManifestCancel = null;

            DataTable dtVehicle = null;

            DataSet ds = null;

            HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
            HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = CancelledManifest;

            HttpContext.Current.Session["VehiclManifest_ManifestCount"] = iCount;
            HttpContext.Current.Session["VehiclManifest_ConfirmedManifesCount"] = iCountConfirm;
            HttpContext.Current.Session["VehiclManifest_CancelledManifestCount"] = iCountCancel;

            //HttpContext.Current.Session["VehiclManifest_ManifestList"] = list;
            HttpContext.Current.Session["VehiclManifest_VehicleVendor"] = listVehicle;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifestByVendor");
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, dDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, iRegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, iPortID);
                db.AddInParameter(dbCommand, "@pcolVehicleVendorIDInt", DbType.Int32, iVehicleID);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, iMaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);
                ds = db.ExecuteDataSet(dbCommand);

                iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                dtManifestNew = ds.Tables[1];

                iCountConfirm = GlobalCode.Field2Int(ds.Tables[2].Rows[0][0]);
                dtManifestConfirm = ds.Tables[3];

                iCountCancel = GlobalCode.Field2Int(ds.Tables[4].Rows[0][0]);
                dtManifestCancel = ds.Tables[5];

                dtVehicle = ds.Tables[6];

                TentativeManifest = (from a in dtManifestNew.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                         colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                         VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                         VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                         colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                         colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                         colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                         colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                         colSFStatus = a.Field<string>("colSFStatus"),
                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         //colRankIDInt, 
                                         RankName = a.Field<string>("RankName"),
                                         //colCostCenterIDInt, 
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                         colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                         VesselName = a.Field<string>("VesselName")

                                     }).ToList();


                ConfirmedManifest = (from a in dtManifestConfirm.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         Remarks = a.Field<string>("Remarks"),
                                         TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                         colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                         VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                         VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                         colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                         colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                         colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                         colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                         colSFStatus = a.Field<string>("colSFStatus"),
                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         //colRankIDInt, 
                                         RankName = a.Field<string>("RankName"),
                                         //colCostCenterIDInt, 
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                         colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                         VesselName = a.Field<string>("VesselName"),

                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                     }).ToList();


                CancelledManifest = (from a in dtManifestCancel.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         TransVehicleID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = GlobalCode.Field2DateTime(a["colOnOffDate"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),
                                         colVehicleVendorIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                         VehicleVendorname = a.Field<string>("VehicleVendorname"),
                                         VehiclePlateNoVarchar = a.Field<string>("colVehiclePlateNoVarchar"),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         colDropOffDate = a.Field<DateTime?>("colDropOffDate"),
                                         colDropOffTime = a.Field<TimeSpan?>("colDropOffTime"),

                                         colConfirmationNoVarchar = a.Field<string>("colConfirmationNoVarchar"),
                                         colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                         colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                         colSFStatus = a.Field<string>("colSFStatus"),
                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         //colRankIDInt, 
                                         RankName = a.Field<string>("RankName"),
                                         //colCostCenterIDInt, 
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         colIsVisibleBit = GlobalCode.Field2Bool(a["colIsVisibleBit"]),
                                         colContractIdInt = GlobalCode.Field2Int(a["colContractIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         colVesselIdInt = GlobalCode.Field2Int(a["colVesselIdInt"]),
                                         VesselName = a.Field<string>("VesselName"),

                                         //ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         //ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                     }).ToList();

                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new VehicleVendorList
                               {
                                   VehicleVendorID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                   VehicleVendorName = a.Field<string>("colVehicleVendorNameVarchar"),
                               }).ToList();


                HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = CancelledManifest;

                HttpContext.Current.Session["VehiclManifest_ManifestCount"] = iCount;
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifesCount"] = iCountConfirm;
                HttpContext.Current.Session["VehiclManifest_CancelledManifestCount"] = iCountCancel;

                HttpContext.Current.Session["VehiclManifest_VehicleVendor"] = listVehicle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtManifestNew != null)
                {
                    dtManifestNew.Dispose();
                }
                if (dtManifestConfirm != null)
                {
                    dtManifestConfirm.Dispose();
                }
                if (dtManifestCancel != null)
                {
                    dtManifestCancel.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
            }
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
            List<VehicleVendorList> list = new List<VehicleVendorList>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetVendorVehicle");
                db.AddInParameter(dbComm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(dbComm, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(sRegionID));
                db.AddInParameter(dbComm, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(sPort));
                db.AddInParameter(dbComm, "@pVehicleVendorIDInt", DbType.Int32, GlobalCode.Field2Int(sVendorID));
                dt = db.ExecuteDataSet(dbComm).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new VehicleVendorList
                        {
                            VehicleVendorID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                            VehicleVendorName = a.Field<string>("colVehicleVendorNameVarchar")                        
                        }).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (dbComm != null)
                {
                    dbComm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspUpdateVehicleShowToVendor");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFilename", DbType.String, sFilename);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);

                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GlobalCode.Field2Date(sGMTDate));
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);
             
                SqlParameter param = new SqlParameter("@pTblTempVehicleTransactionShowHide", dtManifest);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
            }
        }


        public List<VehicleManifestList> GetPotentialSIGNOFF(short LoadType,int VehicleVendorID, DateTime Dates)
        { 
            List<VehicleManifestList> List = new List<VehicleManifestList>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetPotentialSignOFF");

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pVehicleVendorID", DbType.Int32, VehicleVendorID);
                db.AddInParameter(dbCommand, "@pTravelDate", DbType.DateTime, Dates);

                ds = db.ExecuteDataSet(dbCommand);
                List = (from a in ds.Tables[0].AsEnumerable()
                        select new VehicleManifestList
                        {
                            colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                            colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                            colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),

                            colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                            colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),

                            LastName = GlobalCode.Field2String(a["LastName"]),
                            FirstName = GlobalCode.Field2String(a["FirstName"]),

                            SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                            Gender = a.Field<string>("Gender"),
                            VehicleTypeName = a.Field<string>("VehicleTypeName"),

                            RankName = a.Field<string>("RankName"),
                            VesselName = a.Field<string>("VesselName"),
                            CostCenter = a.Field<string>("colCostCenterCodeVarchar"),

                            RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),

                            RouteFrom = a.Field<string>("RouteFrom"),
                            RouteTo = a.Field<string>("RouteTo"),

                            colFromVarchar = a.Field<string>("colFromVarchar"),
                            colToVarchar = a.Field<string>("colToVarchar"),

                            HotelVendorName = a.Field<string>("HotelVendorName"),
                            BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),
                            colSFStatus = a.Field<string>("colSFStatus"),

                            ConfirmedBy = a.Field<string>("colConfirmedBy"),
                            ConfirmedDate = a.Field<string>("ConfirmedDate"),

                        }).ToList();
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;
            //DataTable dtEmail = null;

            try
            {
                List<VehicleManifestList> TentativeManifest = new List<VehicleManifestList>();
                List<VehicleManifestList> ConfirmedManifest = new List<VehicleManifestList>();
                List<VehicleManifestList> CancelledManifest = new List<VehicleManifestList>();
                //List<EmailRecipient> EmailRecipient = new List<EmailRecipient>();

                HttpContext.Current.Session["VehiclManifest_ManifestList"] = TentativeManifest;
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = CancelledManifest;
                //HttpContext.Current.Session["ConfirmManifest_EmailRecipient"] = EmailRecipient;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspConfirmVehicleManifest");

                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.Date, dDate);
                db.AddInParameter(dbCommand, "@pVehicleVendorIDInt", DbType.Int32, iBranchID);

                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pIsSave", DbType.Boolean, bIsSave);
                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCc", DbType.String, sEmailCc);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);
                dtConfirmed = ds.Tables[0];
                dtCancelled = ds.Tables[1];
                //dtEmail = ds.Tables[2];

                ConfirmedManifest = (from a in dtConfirmed.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         //ConfirmManifestID = = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                         RankName = a.Field<string>("RankName"),
                                         VesselName = a.Field<string>("VesselName"),
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         
                                         RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),

                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),
                                        
                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),
                                         colSFStatus = a.Field<string>("colSFStatus"),

                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                     }).ToList();

                CancelledManifest = (from a in dtCancelled.AsEnumerable()
                                     select new VehicleManifestList
                                     {
                                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),

                                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),
                                         
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         Gender = a.Field<string>("Gender"),
                                         VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                         RankName = a.Field<string>("RankName"),
                                         VesselName = a.Field<string>("VesselName"),
                                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),
                                         
                                         RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),

                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),
                                        
                                         colFromVarchar = a.Field<string>("colFromVarchar"),
                                         colToVarchar = a.Field<string>("colToVarchar"),

                                         HotelVendorName = a.Field<string>("HotelVendorName"),
                                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),
                                         colSFStatus = a.Field<string>("colSFStatus"),

                                     }).ToList();

                //EmailRecipient = (from a in dtEmail.AsEnumerable()
                //                  select new EmailRecipient
                //                  {
                //                      EmailTo = a.Field<string>("EmailTo"),
                //                      EmailCc = a.Field<string>("EmailCc")
                //                  }).ToList();
                
                HttpContext.Current.Session["VehiclManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["VehiclManifest_CancelledManifest"] = CancelledManifest;
                //HttpContext.Current.Session["ConfirmManifest_EmailRecipient"] = EmailRecipient;

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            //    if (dtEmail != null)
            //    {
            //        dtEmail.Dispose();
            //    }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Oct/2013
        /// Descrption:     Get vehicle manifest 
        /// =============================================================    
        /// </summary>
        public void GetVehicleManifestExport(string sUserID, string sRole)
        {
            //List<VehicleManifestList> list = new List<VehicleManifestList>();
            //List<VehicleManifestList> listConfirmed = new List<VehicleManifestList>();
            //List<VehicleManifestList> listCancel = new List<VehicleManifestList>();


            //HttpContext.Current.Session["VehiclManifest_NewdManifestExport"] = list;
            //HttpContext.Current.Session["VehiclManifest_ConfirmedManifestExport"] = listConfirmed;
            //HttpContext.Current.Session["VehiclManifest_CancelledManifestExport"] = listCancel;


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            DataTable dt = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;

            HttpContext.Current.Session["VehicleManifest_NewdManifestExport"] = dt;
            HttpContext.Current.Session["VehicleManifest_ConfirmedManifestExport"] = dtConfirmed;
            HttpContext.Current.Session["VehicleManifest_CancelledManifestExport"] = dtCancelled;
            
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifestExport");
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(dbCommand, "@pUserRole", DbType.String, sRole);
                
                ds = db.ExecuteDataSet(dbCommand, trans);

                dt = ds.Tables[0];
                dtConfirmed = ds.Tables[1];
                dtCancelled = ds.Tables[2];
                
                //list = (from a in dt.AsEnumerable()
                //                     select new VehicleManifestList
                //                     {
                //                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                //                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                //                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),

                //                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                //                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),

                //                         LastName = GlobalCode.Field2String(a["LastName"]),
                //                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                //                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                //                         Gender = a.Field<string>("Gender"),
                //                         VehicleTypeName = a.Field<string>("VehicleTypeName"),

                //                         RankName = a.Field<string>("RankName"),
                //                         VesselName = a.Field<string>("VesselName"),
                //                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),

                //                         RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),

                //                         RouteFrom = a.Field<string>("RouteFrom"),
                //                         RouteTo = a.Field<string>("RouteTo"),

                //                         colFromVarchar = a.Field<string>("colFromVarchar"),
                //                         colToVarchar = a.Field<string>("colToVarchar"),

                //                         HotelVendorName = a.Field<string>("HotelVendorName"),
                //                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),
                //                         colSFStatus = a.Field<string>("colSFStatus"),

                //                         //ConfirmedBy = a.Field<string>("colConfirmedBy"),
                //                         //ConfirmedDate = a.Field<string>("ConfirmedDate"),

                //                     }).ToList();

                //listConfirmed = (from a in dtConfirmed.AsEnumerable()
                //                     select new VehicleManifestList
                //                     {
                //                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                //                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                //                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),

                //                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                //                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),

                //                         LastName = GlobalCode.Field2String(a["LastName"]),
                //                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                //                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                //                         Gender = a.Field<string>("Gender"),
                //                         VehicleTypeName = a.Field<string>("VehicleTypeName"),

                //                         RankName = a.Field<string>("RankName"),
                //                         VesselName = a.Field<string>("VesselName"),
                //                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),

                //                         RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),

                //                         RouteFrom = a.Field<string>("RouteFrom"),
                //                         RouteTo = a.Field<string>("RouteTo"),

                //                         colFromVarchar = a.Field<string>("colFromVarchar"),
                //                         colToVarchar = a.Field<string>("colToVarchar"),

                //                         HotelVendorName = a.Field<string>("HotelVendorName"),
                //                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),
                //                         colSFStatus = a.Field<string>("colSFStatus"),

                //                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                //                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                //                     }).ToList();

                //listCancel = (from a in dtCancelled.AsEnumerable()
                //                     select new VehicleManifestList
                //                     {
                //                         colIdBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                //                         colTravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                //                         colRequestIDInt = GlobalCode.Field2Int(a["colRequestIDInt"]),

                //                         colPickUpDate = a.Field<DateTime?>("colPickUpDate"),
                //                         colPickUpTime = a.Field<TimeSpan?>("colPickUpTime"),

                //                         LastName = GlobalCode.Field2String(a["LastName"]),
                //                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                //                         SeafarerIdInt = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                //                         Gender = a.Field<string>("Gender"),
                //                         VehicleTypeName = a.Field<string>("VehicleTypeName"),

                //                         RankName = a.Field<string>("RankName"),
                //                         VesselName = a.Field<string>("VesselName"),
                //                         CostCenter = a.Field<string>("colCostCenterCodeVarchar"),

                //                         RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),

                //                         RouteFrom = a.Field<string>("RouteFrom"),
                //                         RouteTo = a.Field<string>("RouteTo"),

                //                         colFromVarchar = a.Field<string>("colFromVarchar"),
                //                         colToVarchar = a.Field<string>("colToVarchar"),

                //                         HotelVendorName = a.Field<string>("HotelVendorName"),
                //                         BookingRemarks = a.Field<string>("colRemarksForAuditVarchar"),
                //                         colSFStatus = a.Field<string>("colSFStatus"),

                //                     }).ToList();

                HttpContext.Current.Session["VehicleManifest_NewdManifestExport"] = dt;
                HttpContext.Current.Session["VehicleManifest_ConfirmedManifestExport"] = dtConfirmed;
                HttpContext.Current.Session["VehicleManifest_CancelledManifestExport"] = dtCancelled;

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }              
            }
        }

        /// <summary>
        /// Created By:     Muhallidin G Wali
        /// Date Created:   10/JAN/2014
        /// (description)   Get Air and hotel Detail for PDF print out
        /// </summary>
        public List<FlightHotelDetailPDF> GetFlightHotelDetailPDF(short LoadType, long SeafarerID, long TravelRequestID
                ,long IDBigInt, int SeqNo)
        {
            List<FlightHotelDetailPDF> List = new List<FlightHotelDetailPDF>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DataSet ds = null;
            string destination = "";
            try
            {

                dbCommand = db.GetStoredProcCommand("uspGetItineraryFlightDetail");

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.Int64, SeafarerID);

                db.AddInParameter(dbCommand, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                db.AddInParameter(dbCommand, "@pIDBigint", DbType.Int64, IDBigInt);
                db.AddInParameter(dbCommand, "@pSeqNo", DbType.Int32, SeqNo);




                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[2].Rows.Count > 0)
                {
                    destination = ds.Tables[2].Rows[0][0].ToString();
                }

                List.Add(new FlightHotelDetailPDF
                {
                    AirDetailPDF = (from a in ds.Tables[0].AsEnumerable()
                                    select new AirDetailPDF
                                    {

                                        TravelDate = a["TravelDate"].ToString(),
                                        Carrier = a["Carrier"].ToString(),
                                        FlightNo = a["FlightNo"].ToString(),
                                        FromTo = a["FromTo"].ToString(),
                                        DepartureDateTime = a["DepartureDateTime"].ToString(),
                                        ArrivalDateTime = a["ArrivalDateTime"].ToString(),
                                        RecordLocator = a["RecordLocator"].ToString(),
                                        FlightStatus = a["FlightStatus"].ToString(),
                                        Status = a["Status"].ToString(),


                                        DepartureCode = a["DepartureCode"].ToString(),
                                        ArrivalCode = a["ArrivalCode"].ToString(),
                                        DeparturePort = a["DeparturePort"].ToString(),
                                        ArrivalPort = a["ArrivalPort"].ToString(),
                                        Seat = a["Seat"].ToString(),
                                        MileFlown = a["MileFlown"].ToString(),
                                        Class = a["Class"].ToString(),
                                        Meals = a["Meals"].ToString(),
                                        Aircraft = a["Aircraft"].ToString(),
                                        Duration = a["Duration"].ToString(),





                                        //TravelDate
                                        //Carrier
                                        //FlightNo
                                        //FromTo
                                        //DepartureDateTime
                                        //ArrivalDateTime
                                        //RecordLocator
                                        //[FlightStatus]
                                        //[Status]


                                        //DepartureCode
                                        //ArrivalCode

                                        //DeparturePort
                                        //ArrivalPort
                                        
                                        
                                        
                                       
                                       
                                       
                                        //Seat
                                        //MileFlown
                                        //Class
                                        //Meals
                                        //Aircraft
                                        //Duration,


                                    }).ToList(),

                    HotelDetailPDF = (from a in ds.Tables[1].AsEnumerable()
                                      select new HotelDetailPDF
                                      {

                                          CheckInDate = a["CheckInDate"].ToString(),
                                          Chain = a["Chain"].ToString(),
                                          Location = a["Location"].ToString(),
                                          Recordlocator = a["Recordlocator"].ToString(),
                                          RoomType = a["RoomType"].ToString(),
                                          NoOfDays = a["NoOfDays"].ToString(),
                                          Status = a["Status"].ToString(),
                                          ConfirmationNo = a["ConfirmationNo"].ToString(),

                                      }).ToList(),
                    Destination = destination

                });

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// =============================================================     
        /// Author:         Josephine Monteza
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save the record to confirm
        /// =============================================================            
        /// </summary>
        public void SaveVehicleManifestToConfirm(string UserID, bool IsSelected, Int64 iTravelReqID, Int64 iIDBigint)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifestToConfirm");

                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pIsSelected", DbType.Boolean, IsSelected);
                db.AddInParameter(dbCommand, "@pTravelReqIdInt", DbType.Int64, iTravelReqID);
                db.AddInParameter(dbCommand, "@pIdBigint", DbType.Int64, iIDBigint);

                dbCommand.CommandTimeout = 60;
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// =============================================================     
        /// Author:         Josephine Monteza
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save all the record to confirm
        /// =============================================================            
        /// </summary>
        public void SaveVehicleManifestToConfirmAll(string UserID, bool IsSelected)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVendorVehicleManifestToConfirmAll");

                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pIsSelected", DbType.Boolean, IsSelected);

                dbCommand.CommandTimeout = 60;
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }

        /// <summary>
        /// =============================================================     
        /// Author:       Muhallidin G Wali
        /// Date Created: 05/Oct/2016
        /// Descrption:   get Vehicle driver, greeter, and vehicle detail
        /// =============================================================            
        /// </summary>
        public DriverGreeterVehHotelServProv DriverGreeterVehHotelServProv(short LT, int VendorID, string confirmManifestID)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            
            try
            {
                DriverGreeterVehHotelServProv lst = new DriverGreeterVehHotelServProv();
                dbCommand = db.GetStoredProcCommand("uspGetVendorDriverGreeter");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16 , LT);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorID);
                db.AddInParameter(dbCommand, "@pConfirmManifestID", DbType.String, confirmManifestID);

                dbCommand.CommandTimeout = 60;
                DataSet ds = db.ExecuteDataSet(dbCommand);

                lst.Driver = (from a in ds.Tables[0].AsEnumerable()
                              select new DriverGreeter
                              {
                                  ID = a["UserID"].ToString(),
                                  FistName = a["FirstName"].ToString(),
                                  LastName = a["LastName"].ToString(),
                                  FullName = a["DriverName"].ToString(),
                              }).ToList();

                lst.Greeter = (from a in ds.Tables[1].AsEnumerable()
                              select new DriverGreeter
                              {
                                  ID = a["UserID"].ToString(),
                                  FistName = a["FirstName"].ToString(),
                                  LastName = a["LastName"].ToString(),
                                  FullName = a["GreeterName"].ToString(),
                              }).ToList();


                lst.VehHotelSerProv = (from a in ds.Tables[2].AsEnumerable()
                                       select new VehicleHotelServiceProvider
                               {
                                    VehicleHotelServProvID = GlobalCode.Field2Int(a["VehicleHotelServProvID"]),
                                    VendorID = GlobalCode.Field2Int(a["VendorID"]),
                                    VehicleDetailID = GlobalCode.Field2Int(a["VehicleDetailID"]),
                                    VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                    VehicleType = GlobalCode.Field2String(a["VehicleType"]),
                                    PlateNumber = GlobalCode.Field2String(a["PlateNumber"]),
                                    VehicleColor = GlobalCode.Field2String(a["VehicleColor"]),
                                    VehicleColorName = GlobalCode.Field2String(a["VehicleColorName"]),
                                    VehicleBrandID = GlobalCode.Field2Int(a["VehicleBrandID"]),
                                    VehicleBrand = GlobalCode.Field2String(a["VehicleBrand"]),
                                    VehicleMakeId = GlobalCode.Field2Int(a["VehicleMakeId"]),
                                    VehicleMakeName = GlobalCode.Field2String(a["VehicleMakeName"]),
                                    Capacity = GlobalCode.Field2String(a["Capacity"]),
                               }).ToList();

                lst.VehicleManifestList = (from a in ds.Tables[3].AsEnumerable()
                                           select new DriverTransaction
                                           {

                                               ConfirmManifestID = GlobalCode.Field2Long(a["colConfirmedManifestIDBigint"]),
                                               TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                               SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                                               LastName = GlobalCode.Field2String(a["LastName"]),
                                               FirstName = GlobalCode.Field2String(a["FirstName"]),
                                               colIdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                               colTravelReqIDInt = GlobalCode.Field2Long(a["colTravelReqIDInt"]),
                                               RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),
                                               OnOffDate = GlobalCode.Field2DateTime(a["colRequestIDInt"]),
                                               colRequestIDInt = GlobalCode.Field2Long(a["colRequestIDInt"]),
                                               colVehicleVendorIDInt = GlobalCode.Field2Long(a["colVehicleVendorIDInt"]),
                                               VehicleVendorname = GlobalCode.Field2String(a["VehicleVendorname"]),
                                               VehiclePlateNoVarchar = GlobalCode.Field2String(a["colVehiclePlateNoVarchar"]),
                                               colPickUpDate = GlobalCode.Field2DateTime(a["colPickUpDate"]),
                                               colDropOffDate = GlobalCode.Field2DateTime(a["colDropOffDate"]),
                                               colConfirmationNoVarchar = GlobalCode.Field2String(a["colConfirmationNoVarchar"]),
                                               colVehicleStatusVarchar = GlobalCode.Field2String(a["colVehicleStatusVarchar"]),
                                               colVehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                               VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"]),
                                               colSFStatus = GlobalCode.Field2String(a["colSFStatus"]),
                                               RouteFrom = GlobalCode.Field2String(a["RouteFrom"]),
                                               RouteTo = GlobalCode.Field2String(a["RouteTo"]),
                                               colFromVarchar = GlobalCode.Field2String(a["colFromVarchar"]),
                                               colToVarchar = GlobalCode.Field2String(a["colToVarchar"]),
                                               BookingRemarks = GlobalCode.Field2String(a["colRemarksForAuditVarchar"]),
                                               HotelVendorName = GlobalCode.Field2String(a["HotelVendorName"]),
                                               RankName = GlobalCode.Field2String(a["RankName"]),
                                               CostCenter = GlobalCode.Field2String(a["colCostCenterCodeVarchar"]),
                                               Nationality = GlobalCode.Field2String(a["Nationality"]),
                                               Gender = GlobalCode.Field2String(a["Gender"]),
                                               VesselName = GlobalCode.Field2String(a["VesselName"]),
                                               ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                                               ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),

                                               PickupDate = GlobalCode.Field2DateTime(a["colPickupDate"]),
                                               PickupTime = GlobalCode.Field2DateTime(a["colPickupTime"]),
                                               ParkingLocation = GlobalCode.Field2String(a["colParkingLocation"]),
                                               ParkingLatitude = GlobalCode.Field2Float(a["colParkingLatitude"]),
                                               ParkingLongitude = GlobalCode.Field2Float(a["colParkingLongitude"]),
                                               PickupLocation = GlobalCode.Field2String(a["colPickupLocation"]),
                                               PickupLatitude = GlobalCode.Field2Float(a["colPickupLatitude"]),
                                               PickupLongitude = GlobalCode.Field2Float(a["colPickupLongitude"]),
                                               DropOffLocation = GlobalCode.Field2String(a["colDropOffLocation"]),
                                               DropOffLatitude = GlobalCode.Field2Float(a["colDropOffLatitude"]),
                                               DropOffLongitude = GlobalCode.Field2Float(a["colDropOffLongitude"]),
                                               DriverRequestID = GlobalCode.Field2Long(a["colDriverRequestIDInt"]),
                                           }).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }

        }




        /// <summary>
        /// =============================================================     
        /// Author:       Muhallidin G Wali
        /// Date Created: 05/Oct/2016
        /// Descrption:   get Vehicle driver, greeter, and vehicle detail
        /// =============================================================            
        /// </summary>
        /// 
        public void SaveDriverTransaction(int VendorID,string UserID, string TransactionID, DataTable DriverTransaction, DataTable GreeterTransaction)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            try
            { 

                dbCommand = db.GetStoredProcCommand("uspSaveDriverTransaction");

                db.AddInParameter(dbCommand, "@pVendorID", DbType.Int32, VendorID);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pTransactionID", DbType.String, TransactionID );

                SqlParameter DTparam = new SqlParameter("@pDriverTransaction", DriverTransaction);
                DTparam.Direction = ParameterDirection.Input;
                DTparam.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(DTparam);

                SqlParameter GTparam = new SqlParameter("@pGreeterTransaction", GreeterTransaction);
                GTparam.Direction = ParameterDirection.Input;
                GTparam.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(GTparam);

                dbCommand.CommandTimeout = 60;
                DataSet ds = db.ExecuteDataSet(dbCommand);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using System.Web;
using System.Data.SqlClient;

namespace TRAVELMART.DAL
{
    public class PortAgentDAL
    {
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Get Service Provider, Hotel manifest and vehicle manifest of user
        /// -------------------------------------------------------------------
        /// Modified by:    Michael Brian C. Evangelista
        /// Date Modified:  11/Aug/2014
        /// Description:    Added Luggage ,Visa status to dashboard
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGet(int iPortAgentID, string sDate, string sUserID, string sRole, string sOrderBy, 
            int iSeaportID, Int16 iLoadType, int iNoOfDay)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtPortAgent = null;
            //DataTable dtHotel = null;
            //DataTable dtVehicle = null;
            DataTable dtHotelCount = null;
            DataTable dtVehicleCount = null;
            DataTable dtSeaport = null;
            DataTable dtSettings = null;
            DataTable dtLuggageCount = null;
            DataTable dtVisaCount = null;
            DataTable dtSafeGuardCount = null;
            DataTable dtMAGCount = null;


            DataSet ds = null;
            try
            {
                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<PortAgentServicesCount> listHotelCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listVehicleCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listLuggageCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listVisaCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listSafeGuardCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listMAGCount = new List<PortAgentServicesCount>();

                List<SeaportDTO> listSeaport = new List<SeaportDTO>();

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["PortAgentDTO"] = listPortAgent;
                    HttpContext.Current.Session["PortAgentSeaport"] = listSeaport;
                }
                HttpContext.Current.Session["PortAgentHotelCount"] = listHotelCount;
                HttpContext.Current.Session["PortAgentVehicleCount"] = listVehicleCount;
                HttpContext.Current.Session["PortAgentLuggageCount"] = listLuggageCount;
                HttpContext.Current.Session["PortAgentVisaCount"] = listVisaCount;
                HttpContext.Current.Session["PortAgentSafeGuardCount"] = listSafeGuardCount;
                HttpContext.Current.Session["PortAgentMAGCount"] = listMAGCount;

                comm = db.GetStoredProcCommand("uspPortAgentManifestGet");
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pPortID", DbType.Int32, iSeaportID);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);

                ds = db.ExecuteDataSet(comm);


                //if (iLoadType == 0)
                //{
                    dtPortAgent = ds.Tables[0];
                    dtSeaport = ds.Tables[3];
                    dtSettings = ds.Tables[4]; 

                //}
                dtHotelCount = ds.Tables[1];
                dtVehicleCount = ds.Tables[2];
                dtLuggageCount = ds.Tables[5];
                dtVisaCount = ds.Tables[6];
                dtSafeGuardCount = ds.Tables[7];
                dtMAGCount = ds.Tables[8];
                //if (iLoadType == 0)
                //{
                    listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                     select new PortAgentDTO
                                     {
                                         PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                                         PortAgentName = a.Field<string>("PortAgentName")
                                     }).ToList();

                    listSeaport = (from a in dtSeaport.AsEnumerable()
                                   select new SeaportDTO
                                   {
                                       SeaportIDString = GlobalCode.Field2Int(a["SeaportID"]).ToString(),
                                       SeaportNameString = GlobalCode.Field2String(a["SeaportName"]),

                                   }).ToList();

                //}

                listHotelCount = (from a in dtHotelCount.AsEnumerable()
                                select new PortAgentServicesCount
                                 {
                                     iRow = GlobalCode.Field2Int(a["xRow"]),
                                     TotalCount = GlobalCode.Field2Int(a["HotelCount"]),
                                     PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]),
                                     PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),

                                     PendingVendor = GlobalCode.Field2Int(a["PendingVendor"]),
                                     PendingRCCL = GlobalCode.Field2Int(a["PendingRCCL"]),
                                     PendingRCCLCost = GlobalCode.Field2Int(a["PendingRCCLCost"]),
                                     Approved = GlobalCode.Field2Int(a["Approved"]),
                                     Cancelled = GlobalCode.Field2Int(a["Cancelled"]),

                                     PendingColor = a.Field<string>("PendingColor"),
                                     PendingRCCLColor = a.Field<string>("PendingRCCLColor"),
                                     PendingRCCLCostColor = a.Field<string>("PendingRCCLCostColor"),
                                     CancelledColor = a.Field<string>("CancelledColor"),
                                     ApprovedColor = a.Field<string>("ApprovedColor"),

                                 }).ToList();

                listVehicleCount = (from a in dtVehicleCount.AsEnumerable()
                                  select new PortAgentServicesCount
                                  {
                                      iRow = GlobalCode.Field2Int(a["xRow"]),
                                      TotalCount = GlobalCode.Field2Int(a["VehicleCount"]),
                                      PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]),
                                      PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),

                                      PendingVendor = GlobalCode.Field2Int(a["PendingVendor"]),
                                      PendingRCCL = GlobalCode.Field2Int(a["PendingRCCL"]),
                                      PendingRCCLCost = GlobalCode.Field2Int(a["PendingRCCLCost"]),
                                      Approved = GlobalCode.Field2Int(a["Approved"]),
                                      Cancelled = GlobalCode.Field2Int(a["Cancelled"]),

                                      PendingColor = a.Field<string>("PendingColor"),
                                      PendingRCCLColor = a.Field<string>("PendingRCCLColor"),
                                      PendingRCCLCostColor = a.Field<string>("PendingRCCLCostColor"),
                                      CancelledColor = a.Field<string>("CancelledColor"),
                                      ApprovedColor = a.Field<string>("ApprovedColor"),

                                  }).ToList();
                //for luggage
                listLuggageCount = (from a in dtLuggageCount.AsEnumerable()
                                    select new PortAgentServicesCount
                                        {
                                            iRow = GlobalCode.Field2Int(a["xRow"]),
                                            TotalCount = GlobalCode.Field2Int(a["count"]),
                                            PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                            PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                            PendingVendor = 0,
                                            PendingRCCL = 0,
                                            PendingRCCLCost = 0,
                                            Approved = 0,
                                            Cancelled = 0,

                                            PendingColor = "",
                                            PendingRCCLColor = "",
                                            PendingRCCLCostColor = "",
                                            CancelledColor = "",
                                            ApprovedColor = "",

                                        }).ToList();

                //for visa
                listVisaCount = (from a in dtVisaCount.AsEnumerable()
                                 select new PortAgentServicesCount
                                 {
                                     iRow = GlobalCode.Field2Int(a["xRow"]),
                                     TotalCount = GlobalCode.Field2Int(a["count"]),
                                     PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                     PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                     PendingVendor = 0,
                                     PendingRCCL = 0,
                                     PendingRCCLCost = 0,
                                     Approved = 0,
                                     Cancelled = 0,

                                     PendingColor = "",
                                     PendingRCCLColor = "",
                                     PendingRCCLCostColor = "",
                                     CancelledColor = "",
                                     ApprovedColor = "",

                                 }).ToList();

                listSafeGuardCount = (from a in dtSafeGuardCount.AsEnumerable()
                                 select new PortAgentServicesCount
                                 {
                                     iRow = GlobalCode.Field2Int(a["xRow"]),
                                     TotalCount = GlobalCode.Field2Int(a["count"]),
                                     PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                     PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                     PendingVendor = 0,
                                     PendingRCCL = 0,
                                     PendingRCCLCost = 0,
                                     Approved = 0,
                                     Cancelled = 0,

                                     PendingColor = "",
                                     PendingRCCLColor = "",
                                     PendingRCCLCostColor = "",
                                     CancelledColor = "",
                                     ApprovedColor = "",

                                 }).ToList();

                listMAGCount = (from a in dtMAGCount.AsEnumerable()
                                      select new PortAgentServicesCount
                                      {
                                          iRow = GlobalCode.Field2Int(a["xRow"]),
                                          TotalCount = GlobalCode.Field2Int(a["count"]),
                                          PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                          PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                          PendingVendor = 0,
                                          PendingRCCL = 0,
                                          PendingRCCLCost = 0,
                                          Approved = 0,
                                          Cancelled = 0,

                                          PendingColor = "",
                                          PendingRCCLColor = "",
                                          PendingRCCLCostColor = "",
                                          CancelledColor = "",
                                          ApprovedColor = "",

                                      }).ToList();

                //if (iLoadType == 0)
                //{
                    HttpContext.Current.Session["PortAgentDTO"] = listPortAgent;
                    HttpContext.Current.Session["PortAgentSeaport"] = listSeaport;
                    TMSettings.NoOfDays = GlobalCode.Field2Int(dtSettings.Rows[0][0]);
                //}
                HttpContext.Current.Session["PortAgentHotelCount"] = listHotelCount;
                HttpContext.Current.Session["PortAgentVehicleCount"] = listVehicleCount;
                HttpContext.Current.Session["PortAgentLuggageCount"] = listLuggageCount;
                HttpContext.Current.Session["PortAgentVisaCount"] = listVisaCount;
                HttpContext.Current.Session["PortAgentSafeGuardCount"] = listSafeGuardCount;
                HttpContext.Current.Session["PortAgentMAGCount"] = listMAGCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }               
                if (dtHotelCount != null)
                {
                    dtHotelCount.Dispose();
                }
                if (dtVehicleCount != null)
                {
                    dtVehicleCount.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                } 
                if (dtSettings != null)
                {
                    dtSettings.Dispose();
                }
            }
        }

        ///<summary>
        /// -------------------------------------------------------------------
        /// Author: Michael Brian C. Evangelista
        /// Date Created: 14/Aug/2014
        /// Description: Added Get Service Provider, Manifest with Status
        /// -------------------------------------------------------------------
        ///</summary>
        public void PortAgentManifestGetwithStatus(int iPortAgentID, string sDate, string sUserID, string sRole, string sOrderBy,
            int iSeaportID, Int16 iLoadType, int iNoOfDay, string iStatus)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtPortAgent = null;
            //DataTable dtHotel = null;
            //DataTable dtVehicle = null;
            DataTable dtHotelCount = null;
            DataTable dtVehicleCount = null;
            DataTable dtSeaport = null;
            DataTable dtSettings = null;
            DataTable dtLuggageCount = null;
            DataTable dtVisaCount = null;
            DataTable dtSafeGuardCount = null;
            DataTable dtMAGCount = null;


            DataSet ds = null;
            try
            {
                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<PortAgentServicesCount> listHotelCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listVehicleCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listLuggageCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listVisaCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listSafeGuardCount = new List<PortAgentServicesCount>();
                List<PortAgentServicesCount> listMAGCount = new List<PortAgentServicesCount>();

                List<SeaportDTO> listSeaport = new List<SeaportDTO>();

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["PortAgentDTO"] = listPortAgent;
                    HttpContext.Current.Session["PortAgentSeaport"] = listSeaport;
                }
                HttpContext.Current.Session["PortAgentHotelCount"] = listHotelCount;
                HttpContext.Current.Session["PortAgentVehicleCount"] = listVehicleCount;
                HttpContext.Current.Session["PortAgentLuggageCount"] = listLuggageCount;
                HttpContext.Current.Session["PortAgentVisaCount"] = listVisaCount;
                HttpContext.Current.Session["PortAgentSafeGuardCount"] = listSafeGuardCount;
                HttpContext.Current.Session["PortAgentMAGCount"] = listMAGCount;

                int status = Int32.Parse(iStatus);

                comm = db.GetStoredProcCommand("uspPortAgentManifestGetwithStatus");
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pPortID", DbType.Int32, iSeaportID);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);
                db.AddInParameter(comm, "@pStatus", DbType.Int32, status);

                ds = db.ExecuteDataSet(comm);


                //if (iLoadType == 0)
                //{
                dtPortAgent = ds.Tables[0];
                dtSeaport = ds.Tables[3];
                dtSettings = ds.Tables[4];

                //}
                dtHotelCount = ds.Tables[1];
                dtVehicleCount = ds.Tables[2];
                dtLuggageCount = ds.Tables[5];
                dtVisaCount = ds.Tables[6];
                dtSafeGuardCount = ds.Tables[7];
                dtMAGCount = ds.Tables[8];
                //if (iLoadType == 0)
                //{
                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                                     PortAgentName = a.Field<string>("PortAgentName")
                                 }).ToList();

                listSeaport = (from a in dtSeaport.AsEnumerable()
                               select new SeaportDTO
                               {
                                   SeaportIDString = GlobalCode.Field2Int(a["SeaportID"]).ToString(),
                                   SeaportNameString = GlobalCode.Field2String(a["SeaportName"]),

                               }).ToList();

                //}

                listHotelCount = (from a in dtHotelCount.AsEnumerable()
                                  select new PortAgentServicesCount
                                  {
                                      iRow = GlobalCode.Field2Int(a["xRow"]),
                                      TotalCount = GlobalCode.Field2Int(a["HotelCount"]),
                                      PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]),
                                      PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),

                                      PendingVendor = GlobalCode.Field2Int(a["PendingVendor"]),
                                      PendingRCCL = GlobalCode.Field2Int(a["PendingRCCL"]),
                                      PendingRCCLCost = GlobalCode.Field2Int(a["PendingRCCLCost"]),
                                      Approved = GlobalCode.Field2Int(a["Approved"]),
                                      Cancelled = GlobalCode.Field2Int(a["Cancelled"]),

                                      PendingColor = a.Field<string>("PendingColor"),
                                      PendingRCCLColor = a.Field<string>("PendingRCCLColor"),
                                      PendingRCCLCostColor = a.Field<string>("PendingRCCLCostColor"),
                                      CancelledColor = a.Field<string>("CancelledColor"),
                                      ApprovedColor = a.Field<string>("ApprovedColor"),

                                  }).ToList();

                listVehicleCount = (from a in dtVehicleCount.AsEnumerable()
                                    select new PortAgentServicesCount
                                    {
                                        iRow = GlobalCode.Field2Int(a["xRow"]),
                                        TotalCount = GlobalCode.Field2Int(a["VehicleCount"]),
                                        PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]),
                                        PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),

                                        PendingVendor = GlobalCode.Field2Int(a["PendingVendor"]),
                                        PendingRCCL = GlobalCode.Field2Int(a["PendingRCCL"]),
                                        PendingRCCLCost = GlobalCode.Field2Int(a["PendingRCCLCost"]),
                                        Approved = GlobalCode.Field2Int(a["Approved"]),
                                        Cancelled = GlobalCode.Field2Int(a["Cancelled"]),

                                        PendingColor = a.Field<string>("PendingColor"),
                                        PendingRCCLColor = a.Field<string>("PendingRCCLColor"),
                                        PendingRCCLCostColor = a.Field<string>("PendingRCCLCostColor"),
                                        CancelledColor = a.Field<string>("CancelledColor"),
                                        ApprovedColor = a.Field<string>("ApprovedColor"),

                                    }).ToList();
                //for luggage
                listLuggageCount = (from a in dtLuggageCount.AsEnumerable()
                                    select new PortAgentServicesCount
                                    {
                                        iRow = GlobalCode.Field2Int(a["xRow"]),
                                        TotalCount = GlobalCode.Field2Int(a["count"]),
                                        PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                        PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                        PendingVendor = 0,
                                        PendingRCCL = 0,
                                        PendingRCCLCost = 0,
                                        Approved = 0,
                                        Cancelled = 0,

                                        PendingColor = "",
                                        PendingRCCLColor = "",
                                        PendingRCCLCostColor = "",
                                        CancelledColor = "",
                                        ApprovedColor = "",

                                    }).ToList();

                //for visa
                listVisaCount = (from a in dtVisaCount.AsEnumerable()
                                 select new PortAgentServicesCount
                                 {
                                     iRow = GlobalCode.Field2Int(a["xRow"]),
                                     TotalCount = GlobalCode.Field2Int(a["count"]),
                                     PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                     PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                     PendingVendor = 0,
                                     PendingRCCL = 0,
                                     PendingRCCLCost = 0,
                                     Approved = 0,
                                     Cancelled = 0,

                                     PendingColor = "",
                                     PendingRCCLColor = "",
                                     PendingRCCLCostColor = "",
                                     CancelledColor = "",
                                     ApprovedColor = "",

                                 }).ToList();

                listSafeGuardCount = (from a in dtSafeGuardCount.AsEnumerable()
                                      select new PortAgentServicesCount
                                      {
                                          iRow = GlobalCode.Field2Int(a["xRow"]),
                                          TotalCount = GlobalCode.Field2Int(a["count"]),
                                          PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                          PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                          PendingVendor = 0,
                                          PendingRCCL = 0,
                                          PendingRCCLCost = 0,
                                          Approved = 0,
                                          Cancelled = 0,

                                          PendingColor = "",
                                          PendingRCCLColor = "",
                                          PendingRCCLCostColor = "",
                                          CancelledColor = "",
                                          ApprovedColor = "",

                                      }).ToList();

                listMAGCount = (from a in dtMAGCount.AsEnumerable()
                                select new PortAgentServicesCount
                                {
                                    iRow = GlobalCode.Field2Int(a["xRow"]),
                                    TotalCount = GlobalCode.Field2Int(a["count"]),
                                    PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                    PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                    PendingVendor = 0,
                                    PendingRCCL = 0,
                                    PendingRCCLCost = 0,
                                    Approved = 0,
                                    Cancelled = 0,

                                    PendingColor = "",
                                    PendingRCCLColor = "",
                                    PendingRCCLCostColor = "",
                                    CancelledColor = "",
                                    ApprovedColor = "",

                                }).ToList();

                //if (iLoadType == 0)
                //{
                HttpContext.Current.Session["PortAgentDTO"] = listPortAgent;
                HttpContext.Current.Session["PortAgentSeaport"] = listSeaport;
                TMSettings.NoOfDays = GlobalCode.Field2Int(dtSettings.Rows[0][0]);
                //}
                HttpContext.Current.Session["PortAgentHotelCount"] = listHotelCount;
                HttpContext.Current.Session["PortAgentVehicleCount"] = listVehicleCount;
                HttpContext.Current.Session["PortAgentLuggageCount"] = listLuggageCount;
                HttpContext.Current.Session["PortAgentVisaCount"] = listVisaCount;
                HttpContext.Current.Session["PortAgentSafeGuardCount"] = listSafeGuardCount;
                HttpContext.Current.Session["PortAgentMAGCount"] = listMAGCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtHotelCount != null)
                {
                    dtHotelCount.Dispose();
                }
                if (dtVehicleCount != null)
                {
                    dtVehicleCount.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                }
                if (dtSettings != null)
                {
                    dtSettings.Dispose();
                }
            }
        }


        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   08/Mar/2014
        /// Descrption:     Get Hotel Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmHotel(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole, 
            string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtHotel = null;
            DataTable dtStatus = null;

            DataSet ds = null;
            try
            {
                int iTotalRow = 0;
                List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();                
                HttpContext.Current.Session["PortAgentHotelManifestList"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestCount"] = iTotalRow;


                comm = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmHotel");
                db.AddInParameter(comm, "@pStatusIDTinyint", DbType.Int16, iStatusID);
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);

                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);
                db.AddInParameter(comm, "@pSFID", DbType.Int64, iSFID);

                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);
                

                ds = db.ExecuteDataSet(comm);


                dtHotel = ds.Tables[1];
                iTotalRow = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());

                listHotel = (from a in dtHotel.AsEnumerable()
                                select new PortAgentHotelManifestList
                                    {
                                        PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                        PortAgentName = a.Field<string>("PortAgentName"),
        
                                        TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                        IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                        TravelReqID =  GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                        RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                        SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                        LastName =  a.Field<string>("colLastNameVarchar"),
                                        FirstName  =  a.Field<string>("colFirstNameVarchar"),
                                        
                                        HotelName  =  a.Field<string>("colHotelNameVarchar"),
                                        ConfirmationNo  =  a.Field<string>("ConfirmationNo"),

                                        VesselName  =  a.Field<string>("VesselName"),
                                        RankName  =  a.Field<string>("RankName"),
                                        CostCenter =  a.Field<string>("CostCenter"),
                                        Nationality = a.Field<string>("Nationality"),
                                        
                                        DeptCity  =  a.Field<string>("DeptCity"),
                                        ArvlCity  =  a.Field<string>("ArvlCity"),

                                        DeptDate  =  a.Field<DateTime?>("DeptDate"),
                                        ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                        DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                        ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                        /*Flight Stats*/
                                        //ActualDepartureDate = a.Field<string>("actDateD"),
                                        //ActualArrivalDate = a.Field<string>("actDateT"),
                                        //ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                        //ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                        //ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),


                                        Carrier = a.Field<string>("Carrier"),
                                        FlightNo = a.Field<string>("FlightNo"),
                                        RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                        CrewStatus = a.Field<string>("CrewStatus"),
                                        DateOnOff = a.Field<DateTime>("DateOnOff"),

                                        PassportNo = a.Field<string>("PassportNo"),
                                        PassportExp =  a.Field<string>("PassportExp"),
                                        PassportIssued = a.Field<string>("PassportIssued"),

                                        Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                        Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                        HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                        Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                        RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                        RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                        RequestStatus = a.Field<string>("RequestStatus"),
                                        IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                        IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                        Comment = a.Field<string>("colCommentVarchar"),
                                        ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                        CommentBy = a.Field<string>("colCommentByVarchar"),

                                        CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                        IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                        IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                        RemarkID = GlobalCode.Field2Int(a["colRemarkIDInt"]),
                                        Remark = GlobalCode.Field2String(a["colRemarkVarchar"]),

                                    }).ToList();

                HttpContext.Current.Session["PortAgentHotelManifestList"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestCount"] = iTotalRow;



                if (iLoadType == 0)
                {
                    List<ManifestStatus> listStatus = new List<ManifestStatus>();
                    HttpContext.Current.Session["ManifestStatus"] = listStatus;


                    dtStatus = ds.Tables[2];
                    listStatus = (from a in dtStatus.AsEnumerable()
                                  select new ManifestStatus
                                  {
                                      iStatusID = GlobalCode.Field2Int(a["colStatusIDTinyint"]),
                                      sStatus = a.Field<string>("colStatusName"),

                                  }).ToList();

                    HttpContext.Current.Session["ManifestStatus"] = listStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtStatus != null)
                {
                    dtStatus.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtHotel = null;
            DataTable dtHotelCancelled = null;

            DataTable dtPortAgent = null;

            DataTable dtCurrency = null;
            DataTable dtEmailVendor = null;
            DataTable dtRequestSource = null;
            DataTable dtRooms = null;


            DataSet ds = null;
            try
            {
                //int iTotalRow = 0;
                List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
                List<PortAgentHotelManifestList> listHotelCancelled = new List<PortAgentHotelManifestList>();

                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<Currency> listCurrency = new List<Currency>();
                List<RequestSource> listRequestSource = new List<RequestSource>();                

                string sEmailVendor = "";

                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;
                

                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmHotelToAdd");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand);
                dtHotel = ds.Tables[0];
                dtHotelCancelled = ds.Tables[1];

                dtPortAgent = ds.Tables[2];
                dtCurrency = ds.Tables[3];
                dtEmailVendor = ds.Tables[4];
                dtRequestSource = ds.Tables[5];
                dtRooms = ds.Tables[6];

                listHotel = (from a in dtHotel.AsEnumerable()
                             select new PortAgentHotelManifestList
                             {
                                 PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                 PortAgentName = a.Field<string>("PortAgentName"),

                                 TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                 IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                 TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                 RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                 SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                 LastName = a.Field<string>("colLastNameVarchar"),
                                 FirstName = a.Field<string>("colFirstNameVarchar"),

                                 HotelName = a.Field<string>("colHotelNameVarchar"),
                                 ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                 VesselName = a.Field<string>("VesselName"),
                                 RankName = a.Field<string>("RankName"),
                                 CostCenter = a.Field<string>("CostCenter"),

                                 DeptCity = a.Field<string>("DeptCity"),
                                 ArvlCity = a.Field<string>("ArvlCity"),




                                 /*Flight Stats*/
                                 ActualArrivalDate = a.Field<string>("actDateT"),
                                 ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                 ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                 ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),


                                 DeptDate = a.Field<DateTime?>("DeptDate"),
                                 ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                 DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                 ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                 Carrier = a.Field<string>("Carrier"),
                                 FlightNo = a.Field<string>("FlightNo"),
                                 RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                 CrewStatus = a.Field<string>("CrewStatus"),
                                 DateOnOff = a.Field<DateTime>("DateOnOff"),

                                 PassportNo = a.Field<string>("PassportNo"),
                                 PassportExp = a.Field<string>("PassportExp"),
                                 PassportIssued = a.Field<string>("PassportIssued"),

                                 Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                 Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                 HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                 Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                 RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                 RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                 CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                 //RequestStatus = a.Field<string>("RequestStatus"),
                                 //IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),

                             }).ToList();

                listHotelCancelled = (from a in dtHotelCancelled.AsEnumerable()
                             select new PortAgentHotelManifestList
                             {
                                 PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                 PortAgentName = a.Field<string>("PortAgentName"),

                                 TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                 IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                 TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                 RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                 SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                 LastName = a.Field<string>("colLastNameVarchar"),
                                 FirstName = a.Field<string>("colFirstNameVarchar"),

                                 HotelName = a.Field<string>("colHotelNameVarchar"),
                                 ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                 VesselName = a.Field<string>("VesselName"),
                                 RankName = a.Field<string>("RankName"),
                                 CostCenter = a.Field<string>("CostCenter"),

                                 DeptCity = a.Field<string>("DeptCity"),
                                 ArvlCity = a.Field<string>("ArvlCity"),

                                 DeptDate = a.Field<DateTime?>("DeptDate"),
                                 ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                 DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                 ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                 Carrier = a.Field<string>("Carrier"),
                                 FlightNo = a.Field<string>("FlightNo"),
                                 RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                 CrewStatus = a.Field<string>("CrewStatus"),
                                 DateOnOff = a.Field<DateTime>("DateOnOff"),

                                 PassportNo = a.Field<string>("PassportNo"),
                                 PassportExp = a.Field<string>("PassportExp"),
                                 PassportIssued = a.Field<string>("PassportIssued"),

                                 Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                 Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                 HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                 Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                 RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                 RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                 CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),                                

                             }).ToList();

                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO 
                                 { 
                                    PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]).ToString(),
                                    PortAgentName = a.Field<string>("PortAgentName"),
                                    EndOfContract = a.Field<DateTime?>("EndOfContract"),
                                    BeginOfContract = a.Field<DateTime?>("BeginOfContract"),
                                 }).ToList();

                listCurrency = (from a in dtCurrency.AsEnumerable()
                                 select new Currency
                                 {
                                     CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                     CurrencyName = a.Field<string>("CurrencyName"),
                                 }).ToList();

                listRequestSource = (from a in dtRequestSource.AsEnumerable()
                                     select new RequestSource
                                     {
                                         RequestSourceID = GlobalCode.Field2Int(a["colRequestSourceIDint"]),
                                         RequestSourceName = a.Field<string>("colRequestSourceVarchar"),
                                     }).ToList();

                sEmailVendor = GlobalCode.Field2String(dtEmailVendor.Rows[0][0]);

                TMSettings.RoomType = (from a in dtRooms.AsEnumerable()
                                       select new RoomType {
                                           RoomID = GlobalCode.Field2Float(a["RoomID"]),
                                           RoomName = GlobalCode.Field2String(a["colRoomNameVarchar"])                                       
                                       }).ToList();

                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;

                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

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
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtHotelCancelled != null)
                {
                    dtHotelCancelled.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (dtEmailVendor != null)
                {
                    dtEmailVendor.Dispose();
                }
                if (dtRequestSource != null)
                {
                    dtRequestSource.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtRooms != null)
                {
                    dtRooms.Dispose();
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtHotel = null;
            DataTable dtHotelCancelled = null;

            DataTable dtPortAgent = null;

            DataTable dtCurrency = null;
            DataTable dtEmailVendor = null;
            DataTable dtRequestSource = null;
            DataTable dtRooms = null;


            DataSet ds = null;
            try
            {
                //int iTotalRow = 0;
                List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
                List<PortAgentHotelManifestList> listHotelCancelled = new List<PortAgentHotelManifestList>();

                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<Currency> listCurrency = new List<Currency>();
                List<RequestSource> listRequestSource = new List<RequestSource>();

                string sEmailVendor = "";

                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;


                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetHotelToAddByNonTurnPort");
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPortID);
                
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);                

                ds = db.ExecuteDataSet(dbCommand);
                dtHotel = ds.Tables[0];
                dtHotelCancelled = ds.Tables[1];

                dtPortAgent = ds.Tables[2];
                dtCurrency = ds.Tables[3];
                dtEmailVendor = ds.Tables[4];
                dtRequestSource = ds.Tables[5];
                dtRooms = ds.Tables[6];

                listHotel = (from a in dtHotel.AsEnumerable()
                             select new PortAgentHotelManifestList
                             {
                                 PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                 PortAgentName = a.Field<string>("PortAgentName"),

                                 TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                 IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                 TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                 RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                 SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                 LastName = a.Field<string>("colLastNameVarchar"),
                                 FirstName = a.Field<string>("colFirstNameVarchar"),

                                 HotelName = a.Field<string>("colHotelNameVarchar"),
                                 ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                 VesselName = a.Field<string>("VesselName"),
                                 RankName = a.Field<string>("RankName"),
                                 CostCenter = a.Field<string>("CostCenter"),

                                 DeptCity = a.Field<string>("DeptCity"),
                                 ArvlCity = a.Field<string>("ArvlCity"),

                                 DeptDate = a.Field<DateTime?>("DeptDate"),
                                 ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                 DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                 ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                 Carrier = a.Field<string>("Carrier"),
                                 FlightNo = a.Field<string>("FlightNo"),
                                 RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                 CrewStatus = a.Field<string>("CrewStatus"),
                                 DateOnOff = a.Field<DateTime>("DateOnOff"),

                                 PassportNo = a.Field<string>("PassportNo"),
                                 PassportExp = a.Field<string>("PassportExp"),
                                 PassportIssued = a.Field<string>("PassportIssued"),

                                 Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                 Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                 HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                 Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                 RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                 RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                 CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                 //RequestStatus = a.Field<string>("RequestStatus"),
                                 //IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),

                             }).ToList();

                listHotelCancelled = (from a in dtHotelCancelled.AsEnumerable()
                                      select new PortAgentHotelManifestList
                                      {
                                          PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                          PortAgentName = a.Field<string>("PortAgentName"),

                                          TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                          IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                          TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                          RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                          SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                          LastName = a.Field<string>("colLastNameVarchar"),
                                          FirstName = a.Field<string>("colFirstNameVarchar"),

                                          HotelName = a.Field<string>("colHotelNameVarchar"),
                                          ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                          VesselName = a.Field<string>("VesselName"),
                                          RankName = a.Field<string>("RankName"),
                                          CostCenter = a.Field<string>("CostCenter"),

                                          DeptCity = a.Field<string>("DeptCity"),
                                          ArvlCity = a.Field<string>("ArvlCity"),

                                          DeptDate = a.Field<DateTime?>("DeptDate"),
                                          ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                          DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                          ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                          Carrier = a.Field<string>("Carrier"),
                                          FlightNo = a.Field<string>("FlightNo"),
                                          RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                          CrewStatus = a.Field<string>("CrewStatus"),
                                          DateOnOff = a.Field<DateTime>("DateOnOff"),

                                          PassportNo = a.Field<string>("PassportNo"),
                                          PassportExp = a.Field<string>("PassportExp"),
                                          PassportIssued = a.Field<string>("PassportIssued"),

                                          Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                          Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                          HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                          Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                          RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                          RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                          CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),

                                      }).ToList();

                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]).ToString(),
                                     PortAgentName = a.Field<string>("PortAgentName"),
                                     EndOfContract = a.Field<DateTime?>("EndOfContract"),
                                     BeginOfContract = a.Field<DateTime?>("BeginOfContract"),
                                 }).ToList();

                listCurrency = (from a in dtCurrency.AsEnumerable()
                                select new Currency
                                {
                                    CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                    CurrencyName = a.Field<string>("CurrencyName"),
                                }).ToList();

                listRequestSource = (from a in dtRequestSource.AsEnumerable()
                                     select new RequestSource
                                     {
                                         RequestSourceID = GlobalCode.Field2Int(a["colRequestSourceIDint"]),
                                         RequestSourceName = a.Field<string>("colRequestSourceVarchar"),
                                     }).ToList();

                if (dtEmailVendor.Rows.Count > 0)
                {
                    sEmailVendor = GlobalCode.Field2String(dtEmailVendor.Rows[0][0]);
                }
                else
                {
                    sEmailVendor = "";
                }

                TMSettings.RoomType = (from a in dtRooms.AsEnumerable()
                                       select new RoomType
                                       {
                                           RoomID = GlobalCode.Field2Float(a["RoomID"]),
                                           RoomName = GlobalCode.Field2String(a["colRoomNameVarchar"])
                                       }).ToList();

                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;

                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

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
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtHotelCancelled != null)
                {
                    dtHotelCancelled.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (dtEmailVendor != null)
                {
                    dtEmailVendor.Dispose();
                }
                if (dtRequestSource != null)
                {
                    dtRequestSource.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtRooms != null)
                {
                    dtRooms.Dispose();
                }
            }
        }


        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Michael Evangelista
        ///  Date Created:  30-09-2015
        ///  Description:   Get Hotel Manifest TO confirm for new non-turn port
        ///  ------------------------------------------------------------------
        /// </summary>

        public void PortAgentManifestGetConfirmHotelToAddFromNonTurn2(int iPortID, string UserId, string sRole,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtHotelList = null;
            DataTable dtPortAgentList = null;

            DataTable dtPortAgent = null;

            DataTable dtCurrency = null;
            DataTable dtEmailVendor = null;
            DataTable dtRequestSource = null;
            DataTable dtRooms = null;

            DataSet ds = null;

            try 
            {
                List<PortAgentVendorManifestListName> listPortAgent = new List<PortAgentVendorManifestListName>();
                List<PortAgentHotelManifestListName> listHotel = new List<PortAgentHotelManifestListName>();

                List<PortAgentDTO> listPortAgentData = new List<PortAgentDTO>();
                List<Currency> listCurrency = new List<Currency>();
                List<RequestSource> listRequestSource = new List<RequestSource>();

                string sEmailVendor = "";

                HttpContext.Current.Session["PortAgentList"] = listPortAgent;
                HttpContext.Current.Session["HotelList"] = listHotel;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;


                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetHotelToAddByNonTurnPort2");
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPortID);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand);

                dtPortAgentList = ds.Tables[0];
                dtHotelList = ds.Tables[1];

                if (dtPortAgentList.Rows.Count > 0)
                {
                    listPortAgent = (from a in dtPortAgentList.AsEnumerable()
                                     select new PortAgentVendorManifestListName
                                     {

                                         PortAgentID = GlobalCode.Field2Long(a["PortAgentID"]),
                                         PortAgentName = a.Field<string>("PortAgentName")

                                     }).ToList();
                }
                else 
                {
                    DataRow newBlankRow1 = dtPortAgentList.NewRow();
                    dtPortAgentList.Rows.Add(newBlankRow1);

                    listPortAgent = (from a in dtPortAgentList.AsEnumerable()
                                     select new PortAgentVendorManifestListName
                                     {

                                         PortAgentID = GlobalCode.Field2Long(a["PortAgentID"]),
                                         PortAgentName = a.Field<string>("PortAgentName")

                                     }).ToList();
                }

                listHotel = (from a in dtHotelList.AsEnumerable()
                             select new PortAgentHotelManifestListName { 
                                     HotelID = GlobalCode.Field2Long(a["BranchID"]),
                                     HotelVendorName = a.Field<string>("BranchName")
                             } ).ToList();

                /* if only one provider is available*/
                if(dtPortAgentList.Rows.Count > 0)
                {

                }
                else
                {
                listPortAgentData = null;

                listCurrency = null;

                listRequestSource = null;
                dtEmailVendor = null;

                TMSettings.RoomType = null;
                }

                HttpContext.Current.Session["PortAgentHotelListToConfirmToAdd"] = listPortAgent;
                HttpContext.Current.Session["HotelListToConfirmToAdd"] = listHotel;

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
                if (dtPortAgentList != null)
                {
                    dtPortAgentList.Dispose();
                }
                if (dtHotelList != null)
                {
                    dtHotelList.Dispose();
                }
            }           
        }

        /// <summary>
        /// ------------------------------------------------------------------
        /// Author:         Michael Evangelista
        /// Date Created:   02/Oct/2015
        /// Description:    Get PortAgent/Hotel Contract Details
        /// ------------------------------------------------------------------
        /// </summary>
        public void PortAgentGetConfirmHotelToAdd(DataTable dt, string UserId, string sRole,int PortId,int VendorId,string VendorType, string strLogDescription, string strFunction, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtHotel = null;
            DataTable dtHotelCancelled = null;

            DataTable dtPortAgent = null;

            DataTable dtCurrency = null;
            DataTable dtEmailVendor = null;
            DataTable dtRequestSource = null;
            DataTable dtRooms = null;

            DataSet ds = null;
            try
            {
                List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
                List<PortAgentHotelManifestList> listHotelCancelled = new List<PortAgentHotelManifestList>();

                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<Currency> listCurrency = new List<Currency>();
                List<RequestSource> listRequestSource = new List<RequestSource>();

                string sEmailVendor = "";

                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;


                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetHotelToAddByNonTurnPortDetails");
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortId);
                db.AddInParameter(dbCommand, "@pVendorID", DbType.Int32, PortId);

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, "");
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand);
                dtHotel = ds.Tables[0];
                dtHotelCancelled = ds.Tables[1];

                dtPortAgent = ds.Tables[2];
                dtCurrency = ds.Tables[3];
                dtEmailVendor = ds.Tables[4];
                dtRequestSource = ds.Tables[5];
                dtRooms = ds.Tables[6];

                listCurrency = (from a in dtCurrency.AsEnumerable()
                                select new Currency
                                {
                                    CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                    CurrencyName = a.Field<string>("CurrencyName"),
                                }).ToList();

                listRequestSource = (from a in dtRequestSource.AsEnumerable()
                                     select new RequestSource
                                     {
                                         RequestSourceID = GlobalCode.Field2Int(a["colRequestSourceIDint"]),
                                         RequestSourceName = a.Field<string>("colRequestSourceVarchar"),
                                     }).ToList();

                sEmailVendor = GlobalCode.Field2String(dtEmailVendor.Rows[0][0]);

                TMSettings.RoomType = (from a in dtRooms.AsEnumerable()
                                       select new RoomType
                                       {
                                           RoomID = GlobalCode.Field2Float(a["RoomID"]),
                                           RoomName = GlobalCode.Field2String(a["colRoomNameVarchar"])
                                       }).ToList();


                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;

                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

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
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtHotelCancelled != null)
                {
                    dtHotelCancelled.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }            
            
            }
        
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtHotel = null;
            DataTable dtHotelCancelled = null;

            DataSet ds = null;
            try
            {
                List<PortAgentHotelManifestList> listHotel = new List<PortAgentHotelManifestList>();
                List<PortAgentHotelManifestList> listHotelCancelled = new List<PortAgentHotelManifestList>();

                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmHotelToAddWithOrder");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, sOrderBy);


                ds = db.ExecuteDataSet(dbCommand);
                dtHotel = ds.Tables[0];
                dtHotelCancelled = ds.Tables[1];

                listHotel = (from a in dtHotel.AsEnumerable()
                             select new PortAgentHotelManifestList
                             {
                                 PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                 PortAgentName = a.Field<string>("PortAgentName"),

                                 TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                 IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                 TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                 RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                 SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                 LastName = a.Field<string>("colLastNameVarchar"),
                                 FirstName = a.Field<string>("colFirstNameVarchar"),

                                 HotelName = a.Field<string>("colHotelNameVarchar"),
                                 ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                 VesselName = a.Field<string>("VesselName"),
                                 RankName = a.Field<string>("RankName"),
                                 CostCenter = a.Field<string>("CostCenter"),

                                 DeptCity = a.Field<string>("DeptCity"),
                                 ArvlCity = a.Field<string>("ArvlCity"),

                                 DeptDate = a.Field<DateTime?>("DeptDate"),
                                 ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                 DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                 ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                 Carrier = a.Field<string>("Carrier"),
                                 FlightNo = a.Field<string>("FlightNo"),
                                 RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                 CrewStatus = a.Field<string>("CrewStatus"),
                                 DateOnOff = a.Field<DateTime>("DateOnOff"),

                                 PassportNo = a.Field<string>("PassportNo"),
                                 PassportExp = a.Field<string>("PassportExp"),
                                 PassportIssued = a.Field<string>("PassportIssued"),

                                 Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                 Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                 HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                 Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                 RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                 RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                 CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                 //RequestStatus = a.Field<string>("RequestStatus"),
                                 //IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),

                             }).ToList();

                listHotelCancelled = (from a in dtHotelCancelled.AsEnumerable()
                                      select new PortAgentHotelManifestList
                                      {
                                          PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                          PortAgentName = a.Field<string>("PortAgentName"),

                                          TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                          IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                          TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                          RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                          SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                          LastName = a.Field<string>("colLastNameVarchar"),
                                          FirstName = a.Field<string>("colFirstNameVarchar"),

                                          HotelName = a.Field<string>("colHotelNameVarchar"),
                                          ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                          VesselName = a.Field<string>("VesselName"),
                                          RankName = a.Field<string>("RankName"),
                                          CostCenter = a.Field<string>("CostCenter"),

                                          DeptCity = a.Field<string>("DeptCity"),
                                          ArvlCity = a.Field<string>("ArvlCity"),

                                          DeptDate = a.Field<DateTime?>("DeptDate"),
                                          ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                          DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                          ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                          Carrier = a.Field<string>("Carrier"),
                                          FlightNo = a.Field<string>("FlightNo"),
                                          RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                          CrewStatus = a.Field<string>("CrewStatus"),
                                          DateOnOff = a.Field<DateTime>("DateOnOff"),

                                          PassportNo = a.Field<string>("PassportNo"),
                                          PassportExp = a.Field<string>("PassportExp"),
                                          PassportIssued = a.Field<string>("PassportIssued"),

                                          Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                          Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                          HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                          Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                          RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                          RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                          CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),

                                      }).ToList();


                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToAdd"] = listHotel;
                HttpContext.Current.Session["PortAgentHotelManifestListToConfirmToCancel"] = listHotelCancelled;

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
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtHotelCancelled != null)
                {
                    dtHotelCancelled.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   10/Mar/2014
        /// Descrption:     Confirm hotel manifest of Service Provider
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotel(string sUser, string sRole, string sEmailTo,
            string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sHotelName, string sComment, string sConfirmedBy, string sRequestSource,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {              
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmHotel");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pCurrencyID", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pConfirmedRate", DbType.Decimal, fRateConfirmed);
                db.AddInParameter(dbCommand, "@pConfirmationNo", DbType.String, sConfirmationNo);
                db.AddInParameter(dbCommand, "@pHotelName", DbType.String, sHotelName);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);
               
                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmHotelAmount");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pCurrencyID", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pConfirmedRate", DbType.Decimal, fRateConfirmed);
                db.AddInParameter(dbCommand, "@pConfirmationNo", DbType.String, sConfirmationNo);
                db.AddInParameter(dbCommand, "@pHotelName", DbType.String, sHotelName);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmHotelApprove");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);
               
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   17/Mar/2014
        /// Descrption:     Cancel hotel manifest of Service Provider by RCCL
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmHotelCancel(string sUser, string sRole , string sEmailTo,
            string sEmailCC, string sComment, string sConfirmedBy, string sRequestSource, string strLogDescription, 
            string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmHotelCancel");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmHotelAdd");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pCurrencyID", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pConfirmedRate", DbType.Decimal, fRateConfirmed);
                db.AddInParameter(dbCommand, "@pConfirmationNo", DbType.String, sConfirmationNo);
                db.AddInParameter(dbCommand, "@pHotelName", DbType.String, sHotelName);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtVehicle = null;
            DataTable dtvehicleCancelled = null;

            DataTable dtPortAgent = null;
            DataTable dtCurrency = null;
            DataTable dtVehicleType = null;
            DataTable dtEmailVendor = null;
            DataTable dtRequestSource = null;
            DataTable dtRouteFromTo = null;

            DataSet ds = null;
            try
            {
                //int iTotalRow = 0;
                List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();
                List<PortAgentVehicleManifestList> listVehicleCancelled = new List<PortAgentVehicleManifestList>();

                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<Currency> listCurrency = new List<Currency>();
                List<VehicleType> listVehicleType = new List<VehicleType>();
                List<RequestSource> listRequestSource = new List<RequestSource>();
                List<RouteFromTo> listRouteFromTo = new List<RouteFromTo>();

                string sEmailVendor = "";

                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToAdd"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToCancel"] = listVehicleCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentVehicleType"] = listVehicleType;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;
                HttpContext.Current.Session["PortAgentRouteFromTo"] = listRouteFromTo;

                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmVehicleToAdd");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand);
                dtVehicle = ds.Tables[0];
                dtvehicleCancelled = ds.Tables[1];

                dtPortAgent = ds.Tables[2];
                dtCurrency = ds.Tables[3];
                dtVehicleType = ds.Tables[4];
                dtEmailVendor = ds.Tables[5];
                dtRequestSource = ds.Tables[6];
                dtRouteFromTo = ds.Tables[7];


                //iTotalRow = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());

                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new PortAgentVehicleManifestList
                               {
                                   ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                   PortAgentName = a.Field<string>("PortAgentName"),

                                   TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                   LastName = a.Field<string>("colLastNameVarchar"),
                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                   VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                   VesselName = a.Field<string>("VesselName"),
                                   RankName = a.Field<string>("RankName"),
                                   CostCenter = a.Field<string>("CostCenter"),

                                   DeptCity = a.Field<string>("DeptCity"),
                                   ArvlCity = a.Field<string>("ArvlCity"),

                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                   Carrier = a.Field<string>("Carrier"),
                                   FlightNo = a.Field<string>("FlightNo"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                   CrewStatus = a.Field<string>("CrewStatus"),
                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                   PassportNo = a.Field<string>("PassportNo"),
                                   PassportExp = a.Field<string>("PassportExp"),
                                   PassportIssued = a.Field<string>("PassportIssued"),

                                   PickupDate = a.Field<DateTime?>("PickupDate"),
                                   PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                   RouteFrom = a.Field<string>("RouteFrom"),
                                   RouteTo = a.Field<string>("RouteTo"),

                                   RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                   RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                   CityFrom = a.Field<string>("RouteFromCity"),
                                   CityTo = a.Field<string>("RouteToCity"),

                                   RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                   RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                   RequestStatus = a.Field<string>("RequestStatus"),
                                   //IsConfirmVisible_PortAgent = GlobalCode.Field2Bool(a["IsConfirmVisible_PortAgent"]),

                                   CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                   SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                   VehicleTypeID = GlobalCode.Field2TinyInt(a["colVehicleTypeIdInt"]),

                               }).ToList();

                listVehicleCancelled = (from a in dtvehicleCancelled.AsEnumerable()
                               select new PortAgentVehicleManifestList
                               {
                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                   PortAgentName = a.Field<string>("PortAgentName"),

                                   TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                   LastName = a.Field<string>("colLastNameVarchar"),
                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                   VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                   VesselName = a.Field<string>("VesselName"),
                                   RankName = a.Field<string>("RankName"),
                                   CostCenter = a.Field<string>("CostCenter"),

                                   DeptCity = a.Field<string>("DeptCity"),
                                   ArvlCity = a.Field<string>("ArvlCity"),

                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                   Carrier = a.Field<string>("Carrier"),
                                   FlightNo = a.Field<string>("FlightNo"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                   VehicleTypeID = GlobalCode.Field2TinyInt(a["colVehicleTypeIdInt"]),

                                   CrewStatus = a.Field<string>("CrewStatus"),
                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                   PassportNo = a.Field<string>("PassportNo"),
                                   PassportExp = a.Field<string>("PassportExp"),
                                   PassportIssued = a.Field<string>("PassportIssued"),

                                   PickupDate = a.Field<DateTime?>("PickupDate"),
                                   PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                   RouteFrom = a.Field<string>("RouteFrom"),
                                   RouteTo = a.Field<string>("RouteTo"),

                                   RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                   RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                   CityFrom = a.Field<string>("RouteFromCity"),
                                   CityTo = a.Field<string>("RouteToCity"),

                                   RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                   RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                   RequestStatus = a.Field<string>("RequestStatus"),
                                   //IsConfirmVisible_PortAgent = GlobalCode.Field2Bool(a["IsConfirmVisible_PortAgent"]),
                                   CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                   SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]) ,                                   

                               }).ToList();

                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]).ToString(),
                                     PortAgentName = a.Field<string>("PortAgentName"),
                                 }).ToList();

                listCurrency = (from a in dtCurrency.AsEnumerable()
                                select new Currency
                                {
                                    CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                    CurrencyName = a.Field<string>("CurrencyName"),
                                }).ToList();

                listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                select new VehicleType
                                {
                                    VehicleTypeID = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                    VehicleTypeName = a.Field<string>("colVehicleTypeNameVarchar"),
                                }).ToList();

                listRequestSource = (from a in dtRequestSource.AsEnumerable()
                                   select new RequestSource
                                   {
                                       RequestSourceID = GlobalCode.Field2Int(a["colRequestSourceIDint"]),
                                       RequestSourceName = a.Field<string>("colRequestSourceVarchar"),
                                   }).ToList();

                listRouteFromTo = (from a in dtRouteFromTo.AsEnumerable()
                                     select new RouteFromTo
                                     {
                                         RouteID = a.Field<string>("RouteID"),
                                         RouteName = a.Field<string>("RouteName"),
                                     }).ToList();

                sEmailVendor = GlobalCode.Field2String(dtEmailVendor.Rows[0][0]);

                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToAdd"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToCancel"] = listVehicleCancelled;


                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentVehicleType"] = listVehicleType;
                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;
                HttpContext.Current.Session["PortAgentRouteFromTo"] = listRouteFromTo;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtvehicleCancelled != null)
                {
                    dtvehicleCancelled.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                } 
                if (dtEmailVendor != null)
                {
                    dtEmailVendor.Dispose();
                }
                if (dtRequestSource != null)
                {
                    dtRequestSource.Dispose();
                }
                if (dtRouteFromTo != null)
                {
                    dtRouteFromTo.Dispose();
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtVehicle = null;
            DataTable dtvehicleCancelled = null;

            DataTable dtPortAgent = null;
            DataTable dtCurrency = null;
            DataTable dtVehicleType = null;
            DataTable dtEmailVendor = null;
            DataTable dtRequestSource = null;
            DataTable dtRouteFromTo = null;

            DataSet ds = null;
            try
            {
                //int iTotalRow = 0;
                List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();
                List<PortAgentVehicleManifestList> listVehicleCancelled = new List<PortAgentVehicleManifestList>();

                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
                List<Currency> listCurrency = new List<Currency>();
                List<VehicleType> listVehicleType = new List<VehicleType>();
                List<RequestSource> listRequestSource = new List<RequestSource>();
                List<RouteFromTo> listRouteFromTo = new List<RouteFromTo>();

                string sEmailVendor = "";

                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToAdd"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToCancel"] = listVehicleCancelled;

                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentVehicleType"] = listVehicleType;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;
                HttpContext.Current.Session["PortAgentRouteFromTo"] = listRouteFromTo;

                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetVehicleToAddByNonTurnPort");
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPortID);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);
            
                ds = db.ExecuteDataSet(dbCommand);
                dtVehicle = ds.Tables[0];
                dtvehicleCancelled = ds.Tables[1];

                dtPortAgent = ds.Tables[2];
                dtCurrency = ds.Tables[3];
                dtVehicleType = ds.Tables[4];
                dtEmailVendor = ds.Tables[5];
                dtRequestSource = ds.Tables[6];
                dtRouteFromTo = ds.Tables[7];


                //iTotalRow = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());

                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new PortAgentVehicleManifestList
                               {
                                   ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),

                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                   PortAgentName = a.Field<string>("PortAgentName"),

                                   TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                   LastName = a.Field<string>("colLastNameVarchar"),
                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                   VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                   VesselName = a.Field<string>("VesselName"),
                                   RankName = a.Field<string>("RankName"),
                                   CostCenter = a.Field<string>("CostCenter"),

                                   DeptCity = a.Field<string>("DeptCity"),
                                   ArvlCity = a.Field<string>("ArvlCity"),

                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                   Carrier = a.Field<string>("Carrier"),
                                   FlightNo = a.Field<string>("FlightNo"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                   CrewStatus = a.Field<string>("CrewStatus"),
                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                   PassportNo = a.Field<string>("PassportNo"),
                                   PassportExp = a.Field<string>("PassportExp"),
                                   PassportIssued = a.Field<string>("PassportIssued"),

                                   PickupDate = a.Field<DateTime?>("PickupDate"),
                                   PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                   RouteFrom = a.Field<string>("RouteFrom"),
                                   RouteTo = a.Field<string>("RouteTo"),

                                   RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                   RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                   CityFrom = a.Field<string>("RouteFromCity"),
                                   CityTo = a.Field<string>("RouteToCity"),

                                   RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                   RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                   RequestStatus = a.Field<string>("RequestStatus"),
                                   //IsConfirmVisible_PortAgent = GlobalCode.Field2Bool(a["IsConfirmVisible_PortAgent"]),

                                   CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                   SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                   VehicleTypeID = GlobalCode.Field2TinyInt(a["colVehicleTypeIdInt"]),

                               }).ToList();

                listVehicleCancelled = (from a in dtvehicleCancelled.AsEnumerable()
                                        select new PortAgentVehicleManifestList
                                        {
                                            PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                            PortAgentName = a.Field<string>("PortAgentName"),

                                            TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                            IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                            TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                            RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                            SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                            LastName = a.Field<string>("colLastNameVarchar"),
                                            FirstName = a.Field<string>("colFirstNameVarchar"),

                                            VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                            ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                            VesselName = a.Field<string>("VesselName"),
                                            RankName = a.Field<string>("RankName"),
                                            CostCenter = a.Field<string>("CostCenter"),

                                            DeptCity = a.Field<string>("DeptCity"),
                                            ArvlCity = a.Field<string>("ArvlCity"),

                                            DeptDate = a.Field<DateTime?>("DeptDate"),
                                            ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                            DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                            ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                            Carrier = a.Field<string>("Carrier"),
                                            FlightNo = a.Field<string>("FlightNo"),
                                            VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                            VehicleTypeID = GlobalCode.Field2TinyInt(a["colVehicleTypeIdInt"]),

                                            CrewStatus = a.Field<string>("CrewStatus"),
                                            DateOnOff = a.Field<DateTime>("DateOnOff"),

                                            PassportNo = a.Field<string>("PassportNo"),
                                            PassportExp = a.Field<string>("PassportExp"),
                                            PassportIssued = a.Field<string>("PassportIssued"),

                                            PickupDate = a.Field<DateTime?>("PickupDate"),
                                            PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                            RouteFrom = a.Field<string>("RouteFrom"),
                                            RouteTo = a.Field<string>("RouteTo"),

                                            RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                            RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                            CityFrom = a.Field<string>("RouteFromCity"),
                                            CityTo = a.Field<string>("RouteToCity"),

                                            RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                            RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                            RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                            RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                            RequestStatus = a.Field<string>("RequestStatus"),
                                            //IsConfirmVisible_PortAgent = GlobalCode.Field2Bool(a["IsConfirmVisible_PortAgent"]),
                                            CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),


                                        }).ToList();

                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2Int(a["PortAgentID"]).ToString(),
                                     PortAgentName = a.Field<string>("PortAgentName"),
                                 }).ToList();

                listCurrency = (from a in dtCurrency.AsEnumerable()
                                select new Currency
                                {
                                    CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                    CurrencyName = a.Field<string>("CurrencyName"),
                                }).ToList();

                listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                   select new VehicleType
                                   {
                                       VehicleTypeID = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                                       VehicleTypeName = a.Field<string>("colVehicleTypeNameVarchar"),
                                   }).ToList();

                listRequestSource = (from a in dtRequestSource.AsEnumerable()
                                     select new RequestSource
                                     {
                                         RequestSourceID = GlobalCode.Field2Int(a["colRequestSourceIDint"]),
                                         RequestSourceName = a.Field<string>("colRequestSourceVarchar"),
                                     }).ToList();

                listRouteFromTo = (from a in dtRouteFromTo.AsEnumerable()
                                   select new RouteFromTo
                                   {
                                       RouteID = a.Field<string>("RouteID"),
                                       RouteName = a.Field<string>("RouteName"),
                                   }).ToList();

                sEmailVendor = GlobalCode.Field2String(dtEmailVendor.Rows[0][0]);

                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToAdd"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToCancel"] = listVehicleCancelled;


                HttpContext.Current.Session["PortAgentDetails"] = listPortAgent;
                HttpContext.Current.Session["PortAgentCurrency"] = listCurrency;
                HttpContext.Current.Session["PortAgentVehicleType"] = listVehicleType;
                HttpContext.Current.Session["PortAgentEmailVendor"] = sEmailVendor;
                HttpContext.Current.Session["PortAgentRequestSource"] = listRequestSource;
                HttpContext.Current.Session["PortAgentRouteFromTo"] = listRouteFromTo;
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
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtvehicleCancelled != null)
                {
                    dtvehicleCancelled.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtCurrency != null)
                {
                    dtCurrency.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtEmailVendor != null)
                {
                    dtEmailVendor.Dispose();
                }
                if (dtRequestSource != null)
                {
                    dtRequestSource.Dispose();
                }
                if (dtRouteFromTo != null)
                {
                    dtRouteFromTo.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataTable dtVehicle = null;
            DataTable dtvehicleCancelled = null;
          
            DataSet ds = null;
            try
            {
                List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();
                List<PortAgentVehicleManifestList> listVehicleCancelled = new List<PortAgentVehicleManifestList>();
              
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToAdd"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToCancel"] = listVehicleCancelled;
                
                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmVehicleToAddWithOrder");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, sOrderBy);
            

                ds = db.ExecuteDataSet(dbCommand);
                dtVehicle = ds.Tables[0];
                dtvehicleCancelled = ds.Tables[1];

                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new PortAgentVehicleManifestList
                               {
                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                   PortAgentName = a.Field<string>("PortAgentName"),

                                   TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                   LastName = a.Field<string>("colLastNameVarchar"),
                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                   VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                   VesselName = a.Field<string>("VesselName"),
                                   RankName = a.Field<string>("RankName"),
                                   CostCenter = a.Field<string>("CostCenter"),

                                   DeptCity = a.Field<string>("DeptCity"),
                                   ArvlCity = a.Field<string>("ArvlCity"),

                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                   Carrier = a.Field<string>("Carrier"),
                                   FlightNo = a.Field<string>("FlightNo"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                   CrewStatus = a.Field<string>("CrewStatus"),
                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                   PassportNo = a.Field<string>("PassportNo"),
                                   PassportExp = a.Field<string>("PassportExp"),
                                   PassportIssued = a.Field<string>("PassportIssued"),

                                   PickupDate = a.Field<DateTime?>("PickupDate"),
                                   PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                   RouteFrom = a.Field<string>("RouteFrom"),
                                   RouteTo = a.Field<string>("RouteTo"),

                                   RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                   RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                   CityFrom = a.Field<string>("RouteFromCity"),
                                   CityTo = a.Field<string>("RouteToCity"),

                                   RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                   RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                   RequestStatus = a.Field<string>("RequestStatus"),
                                   //IsConfirmVisible_PortAgent = GlobalCode.Field2Bool(a["IsConfirmVisible_PortAgent"]),

                                   CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),

                               }).ToList();

                listVehicleCancelled = (from a in dtvehicleCancelled.AsEnumerable()
                                        select new PortAgentVehicleManifestList
                                        {
                                            PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                            PortAgentName = a.Field<string>("PortAgentName"),

                                            TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                            IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                            TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                            RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                            SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                            LastName = a.Field<string>("colLastNameVarchar"),
                                            FirstName = a.Field<string>("colFirstNameVarchar"),

                                            VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                            ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                            VesselName = a.Field<string>("VesselName"),
                                            RankName = a.Field<string>("RankName"),
                                            CostCenter = a.Field<string>("CostCenter"),

                                            DeptCity = a.Field<string>("DeptCity"),
                                            ArvlCity = a.Field<string>("ArvlCity"),

                                            DeptDate = a.Field<DateTime?>("DeptDate"),
                                            ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                            DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                            ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                            Carrier = a.Field<string>("Carrier"),
                                            FlightNo = a.Field<string>("FlightNo"),
                                            VehicleTypeName = a.Field<string>("VehicleTypeName"),
                                            VehicleTypeID = GlobalCode.Field2TinyInt(a["colVehicleTypeIdInt"]),

                                            CrewStatus = a.Field<string>("CrewStatus"),
                                            DateOnOff = a.Field<DateTime>("DateOnOff"),

                                            PassportNo = a.Field<string>("PassportNo"),
                                            PassportExp = a.Field<string>("PassportExp"),
                                            PassportIssued = a.Field<string>("PassportIssued"),

                                            PickupDate = a.Field<DateTime?>("PickupDate"),
                                            PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                            RouteFrom = a.Field<string>("RouteFrom"),
                                            RouteTo = a.Field<string>("RouteTo"),

                                            RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                            RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                            CityFrom = a.Field<string>("RouteFromCity"),
                                            CityTo = a.Field<string>("RouteToCity"),

                                            RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                            RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                            RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                            RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                            RequestStatus = a.Field<string>("RequestStatus"),
                                            //IsConfirmVisible_PortAgent = GlobalCode.Field2Bool(a["IsConfirmVisible_PortAgent"]),
                                            CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),


                                        }).ToList();              

                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToAdd"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestListToConfirmToCancel"] = listVehicleCancelled;
              
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
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtvehicleCancelled != null)
                {
                    dtvehicleCancelled.Dispose();
                }                
                if (ds != null)
                {
                    ds.Dispose();
                }                
            }
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   13/Mar/2014
        /// Descrption:     Confirm vehicle manifest of Service Provider
        /// -------------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  24/Jul/2014
        /// Descrption:     Add sTransportationDetails parameter
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicle(string sUser, string sRole, string sEmailTo,
            string sEmailCC, string sTimeSpan, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sVehicleName, string sDriverName, string sPlateNo, Int16 iVehicleTypeIDInt, 
            string sComment, string sConfirmedBy, Int32 iContractIDInt, string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt, string sTransportationDetails)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmVehicle");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                if (sTimeSpan != "")
                {
                    db.AddInParameter(dbCommand, "@pPickupTime", DbType.Time, sTimeSpan);
                }
                db.AddInParameter(dbCommand, "@pCurrencyID", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pConfirmedRate", DbType.Decimal, fRateConfirmed);

                db.AddInParameter(dbCommand, "@pConfirmationNo", DbType.String, sConfirmationNo);
                db.AddInParameter(dbCommand, "@pVehicleVendorName", DbType.String, sVehicleName);

                db.AddInParameter(dbCommand, "@pDriverName", DbType.String, sDriverName);
                db.AddInParameter(dbCommand, "@pPlateNo", DbType.String, sPlateNo);
                db.AddInParameter(dbCommand, "@pVehicleTypeID", DbType.Int16, iVehicleTypeIDInt);


                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pContractIDInt", DbType.Int32, iContractIDInt);

                db.AddInParameter(dbCommand, "@pTransporationDetails", DbType.String, sTransportationDetails);
                
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
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
        /// Descrption:     Add vehicle request of Service Provider from NonTurnPort Page
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestConfirmVehicleAdd(string sUser, string sRole, string sEmailTo,
            string sEmailCC, int iCurrency, float fRateConfirmed, string sConfirmationNo,
            string sComment, string sConfirmedBy, string sRequestSource, Int32 iContractIDInt, Int16 iVehicleTypeId,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, DataTable dt)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmVehicleAdd");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pCurrencyID", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pConfirmedRate", DbType.Decimal, fRateConfirmed);
                db.AddInParameter(dbCommand, "@pConfirmationNo", DbType.String, sConfirmationNo);
                //db.AddInParameter(dbCommand, "@pHotelName", DbType.String, sHotelName);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));
                db.AddInParameter(dbCommand, "@pContractIDInt", DbType.Int32, iContractIDInt);
                db.AddInParameter(dbCommand, "@pVehicleTypeIdInt", DbType.Int16, iVehicleTypeId);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmVehicleAmount");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pCurrencyID", DbType.Int32, iCurrency);
                db.AddInParameter(dbCommand, "@pConfirmedRate", DbType.Decimal, fRateConfirmed);
                db.AddInParameter(dbCommand, "@pConfirmationNo", DbType.String, sConfirmationNo);
                //db.AddInParameter(dbCommand, "@pHotelName", DbType.String, sHotelName);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16,  GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@pTable", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmVehicleApprove");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspPortAgentManifestConfirmVehicleCancel");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCC", DbType.String, sEmailCC);

                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);
                db.AddInParameter(dbCommand, "@pRequestSourceIDInt", DbType.Int16, GlobalCode.Field2TinyInt(sRequestSource));

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   13/Mar/2014
        /// Descrption:     Get Service Provider List based from Seaport ID
        /// -------------------------------------------------------------------
        /// Modifed By:     Josephine Gad
        /// Date Modified:  10/Jul/2014
        /// Descrption:     Add value of TMSettings.NoOfDays
        /// -------------------------------------------------------------------
        /// </summary>
        public  List<PortAgentDTO> GetPortAgentListByPortId(int iPortID, string sUser, string sRole)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtPortAgent = null;
            DataSet ds = null;
            try
            {
                List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();

                comm = db.GetStoredProcCommand("uspPortAgentVendorGetByPort");
                db.AddInParameter(comm, "@pPortID", DbType.Int32, iPortID);
                db.AddInParameter(comm, "@pUserID", DbType.String, sUser);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);

                ds = db.ExecuteDataSet(comm);

                dtPortAgent = ds.Tables[0];
              
                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                                     PortAgentName = a.Field<string>("PortAgentName")
                                 }).ToList();

                TMSettings.NoOfDays = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);

                return listPortAgent;              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }              
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                List<ManifestStatus> listStatus = new List<ManifestStatus>();

                comm = db.GetStoredProcCommand("uspManifestStatusGet");
                ds = db.ExecuteDataSet(comm);

                dt = ds.Tables[0];

                listStatus = (from a in dt.AsEnumerable()
                                 select new ManifestStatus
                                 {
                                     iStatusID = GlobalCode.Field2Int(a["colStatusIDTinyint"]),
                                     sStatus = a.Field<string>("colStatusName")
                                 }).ToList();

                return listStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtHotel = null;

            DataSet ds = null;
            try
            {

                comm = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmHotelExport");
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                ds = db.ExecuteDataSet(comm);
                dtHotel = ds.Tables[0];
                return dtHotel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtHotel = null;

            DataSet ds = null;
            try
            {

                comm = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmVehicleExport");
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                ds = db.ExecuteDataSet(comm);
                dtHotel = ds.Tables[0];
                return dtHotel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/Mar/2014
        /// Descrption:     Get Remarks
        /// -------------------------------------------------------------------
        /// </summary>        
        public List<TransactionRemarks> GetTransactionRemarks(Int64 iTRid, string sRecloc, Int16 iExpenseType)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;

            List<TransactionRemarks> list = new List<TransactionRemarks>();

            DataSet ds = null;
            try
            {
                comm = db.GetStoredProcCommand("uspPortAgentManifestGetRemarks");
                db.AddInParameter(comm, "@pTravelReqIdInt", DbType.Int64, iTRid);
                db.AddInParameter(comm, "@pRecordLocatorVarchar", DbType.String, sRecloc);
                db.AddInParameter(comm, "@pExpendTypeIdInt", DbType.String, iExpenseType);

                ds = db.ExecuteDataSet(comm);
                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new TransactionRemarks
                        {
                            TravelRequestID = a.Field<long>("TravelRequestID"),
                            RemarksID = a.Field<long>("RemarksID"),
                            Remarks = a.Field<string>("Remarks"),
                            RemarksBy = a.Field<string>("CreatedBy"),
                            RemarksDate = a.Field<string>("RemarksDate"),
                            ReqResourceID = GlobalCode.Field2TinyInt(a["colRequestSourceIDint"]),
                            Resource = a.Field<string>("RemarkSource"),

                        }).ToList();
                return list;
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
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   11/Apr/2014
        /// Descrption:     Get Service Provider Vehicle Contract Amt
        /// -------------------------------------------------------------------
        /// </summary>        
        public List<PortAgentVehicleContractAmt> GetPortAgentVehicleContractAmt(Int32 iPortID, Int16 iVehicleType)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;

            List<PortAgentVehicleContractAmt> list = new List<PortAgentVehicleContractAmt>();

            DataSet ds = null;
            try
            {
                comm = db.GetStoredProcCommand("uspPortAgentManifestGetVehicleContractAmt");
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int64, iPortID);
                db.AddInParameter(comm, "@pVehicleTypeID", DbType.Int16, iVehicleType);                

                ds = db.ExecuteDataSet(comm);
                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new PortAgentVehicleContractAmt
                        {
                            ContractID = GlobalCode.Field2Int(a["ContractId"]),

                            RouteFromInt = GlobalCode.Field2TinyInt(a["colRouteIDFromInt"]),
                            RouteToInt = GlobalCode.Field2TinyInt(a["colRouteIDToInt"]),                            

                            RouteFrom = a.Field<string>("RouteFrom"),
                            RouteTo = a.Field<string>("RouteTo"),

                            RouteFromCity = a.Field<string>("colFromVarchar"),
                            RouteToCity = a.Field<string>("colToVarchar"),

                            RateAmount = GlobalCode.Field2Float(a["RateAmount"]),  

                        }).ToList();
                return list;
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
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   06/Nov/2014
        /// Descrption:     Get List of OK to Board in Brazil
        /// -------------------------------------------------------------------
        /// </summary>        
        public List<OkToBrazilList> GetOKToBrazilList(string sUser, DateTime dDate, string sOrderBy, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;

            List<OkToBrazilList> list = new List<OkToBrazilList>();

            DataSet ds = null;
            try
            {
                comm = db.GetStoredProcCommand("uspGetManifestOkToBrazil");
                db.AddInParameter(comm, "@pUserID", DbType.String, sUser);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, dDate);
                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);

                db.AddInParameter(comm, "@pStartRow", DbType.String, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.String, iMaxRow);

                ds = db.ExecuteDataSet(comm);
                dt = ds.Tables[1];
                int iTotal = 0;

                list = (from a in dt.AsEnumerable()
                        select new OkToBrazilList
                        {
                            SeafarerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
            
                            LastName =  a.Field<string>("colLastNameVarchar"),
                            FirstName =  a.Field<string>("colFirstNameVarchar"),

                            Nationality  = a.Field<string>("Nationality"),
        
                            IDBigint =  GlobalCode.Field2Long(a["colIdBigint"]),
                            TRID  =  GlobalCode.Field2Long(a["colTravelReqIdInt"]),

                            RecLoc  =  a.Field<string>("colRecordLocatorVarchar"),
                            Status =  a.Field<string>("colStatusVarchar"),

                            OnOffDate = GlobalCode.Field2DateTime(a["OnOffDate"]),
                            ReasonCode =  a.Field<string>("colReasonCodeVarchar"),

                            PortCode = a.Field<string>("colPortCodeVarchar"),
                            PortName = a.Field<string>("colPortNameVarchar"),

                            VesselCode = a.Field<string>("colVesselCodeVarchar"),
                            VesselName = a.Field<string>("colVesselNameVarchar"),

                            DepartureAirport = a.Field<string>("DepartureAirport"),
                            ArrivalAirport = a.Field<string>("ArrivalAirport"),

                            DepartureDatetime = a.Field<DateTime?>("DepartureDatetime"),
                            ArrivalDatetime = a.Field<DateTime?>("ArrivalDatetime"),

                            Airline = a.Field<string>("Airline"),
                            FlightNo = a.Field<string>("FlightNo"),

                            PassportNo =  a.Field<string>("PassportNo"),        
                            PassportIssuedDate  = a.Field<DateTime?>("PassportIssued"),
                            PassportExpiredDate = a.Field<DateTime?>("PassportExpDate"),

                            SeamansBook =  a.Field<string>("SeamansBookNo"),
                            SeaBookIssuedDate = a.Field<DateTime?>("SeaBookIssuedDate"),
                            SeaBookExpiredDate = a.Field<DateTime?>("SeaBookExpDate"),

                        }).ToList();

                iTotal = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                HttpContext.Current.Session["OkToBrazilCount"] = iTotal;

                return list;
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
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   07/Nov/2014
        /// Descrption:     Get List of OK to Board in Brazil to export
        /// -------------------------------------------------------------------
        /// </summary>        
        public DataTable GetOKToBrazilExport(string sUser)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;
           
            DataSet ds = null;
            try
            {
                comm = db.GetStoredProcCommand("uspGetManifestOkToBrazilExport");
                db.AddInParameter(comm, "@pUserID", DbType.String, sUser);
                
                ds = db.ExecuteDataSet(comm);
                dt = ds.Tables[0];
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }
         
        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Muhallidin G Wali
        ///  Date Created:  12-10-2015
        ///  Description:   Get Hotel Manifest TO confirm for new non-turn port
        ///  ------------------------------------------------------------------
        /// </summary>

        public List<NonTurnportGenericList> GetPortNonTurnHotelRequest(int LoadType, DateTime RequestDate, int PortID,int PortAgentID, string UserID, DataTable dt)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                List<NonTurnportGenericList> lst = new List<NonTurnportGenericList>(); 
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentNonTurnportHotelRequest");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16 , LoadType);			
                db.AddInParameter(dbCommand, "@pRequestDate", DbType.DateTime , RequestDate );		
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32 , PortID );
                db.AddInParameter(dbCommand, "@pPortAgentID", DbType.Int32, PortAgentID);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);

                SqlParameter param = new SqlParameter("@pNonTurnPortRequest", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand);

                lst.Add(new NonTurnportGenericList
                {
                    PortAgenRequestVendor = (from n in ds.Tables[0].AsEnumerable()
                                             select new PortAgenRequestVendor
                                             {
                                                 VendorID = GlobalCode.Field2Int(n["VendorID"]),
                                                 VendorName = GlobalCode.Field2String(n["VendorName"]),
                                                 VendorContractName = GlobalCode.Field2String(n["VendorContractName"]),
                                                 VendorContractID = GlobalCode.Field2Int(n["VendorContractID"]),
                                                 CurrentID = GlobalCode.Field2Int(n["CurrentID"]),
                                                 CurrentName = GlobalCode.Field2String(n["CurrentName"]),

                                                 MealStandard = GlobalCode.Field2Decimal(n["MealStandard"]),
                                                 MealIncrease = GlobalCode.Field2Decimal(n["MealIncrease"]),
                                                 SingleRate = GlobalCode.Field2Decimal(n["SingleRate"]),
                                                 DoubleRate = GlobalCode.Field2Decimal(n["DoubleRate"]),
                                                 Voucher = GlobalCode.Field2Decimal(n["Voucher"]),
                                                 EmailTo = GlobalCode.Field2String(n["EmailTo"]),

                                             }).ToList(),
                     NonTurnPortsLists = (from a in ds.Tables[1].AsEnumerable()
                                    select new NonTurnPortsList
                                    {

                                        HotelTransID = GlobalCode.Field2Long(a["TransHotelID"]),
                                        IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                        TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                        E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                                        SeqNo = GlobalCode.Field2Int(a["SeqNo"]),
                                        RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                        PortId = GlobalCode.Field2Int(a["PortId"]),
                                        SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                                        HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                        Checkin = GlobalCode.Field2String(a["Checkin"]),
                                        CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                                        HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                                        LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                        FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),

                                        Employee = GlobalCode.Field2Long(a["Employee"]),
                                        Gender = GlobalCode.Field2String(a["Gender"]),
                                        SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                                        Couple = GlobalCode.Field2String(a["Couple"]),
                                        Title = GlobalCode.Field2String(a["Title"]),
                                        Ship = GlobalCode.Field2String(a["Ship"]),
                                        Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                                        Nationality = GlobalCode.Field2String(a["Natioality"]),
                                        Birthday = GlobalCode.Field2String(a["Birthday"]),
                                        HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                        RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                        RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                        AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                        
                                        deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                        ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
                                        ArvlCityName = GlobalCode.Field2String(a["ArrAirportName"]),
                                        deptCityName = GlobalCode.Field2String(a["DepAirportName"]),

                                        Deptdate = GlobalCode.Field2String(a["DeptDate"]),
                                        DeptTime = GlobalCode.Field2String(a["DeptTime"]),

                                        Arvldate = GlobalCode.Field2String(a["ArrvlDate"]),
                                        ArvlTime = GlobalCode.Field2String(a["ArrvlTime"]),

                                        Carrier = GlobalCode.Field2String(a["Carrier"]),
                                        FlightNo = GlobalCode.Field2String(a["FlightNo"]),
                                        Voucher = GlobalCode.Field2String(a["Voucher"]),
                                        PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                                        PassportExp = GlobalCode.Field2String(a["PassportExp"]),
                                        PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                                        HotelBranch = GlobalCode.Field2String(a["HotelBranch"]),
                                        Booking = GlobalCode.Field2String(a["Booking"]),
                                        Bookingremark = GlobalCode.Field2String(a["Bookingremark"]),
                                        IsVisible = GlobalCode.Field2Bool(a["IsVisible"]),
                                        stripes = GlobalCode.Field2Decimal(a["colStripesDecimal"]),
                                        GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),

                                        ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                                        ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"]),
                                        StatusID =  GlobalCode.Field2Int(a["StatusID"]),
                                        IsMedical = GlobalCode.Field2Bool(a["IsMedical"]),

                                        ConfirmedRate = GlobalCode.Field2Double(a["colConfirmRateMoney"]),
                                        ContractedRate = GlobalCode.Field2Double(a["colContractedRateMoney"]),


                                    }).ToList(),


                    Currency = (from a in ds.Tables[3].AsEnumerable()
                                select new ComboGenericClass 
                                {
                                    ID = a.Field<int?>("colCurrencyIDInt"),
                                    Name = a.Field<string>("colCurrencyNameVarchar"),
                                    NameCode = a.Field<string>("colCurrencyCodeVarchar"),
                                }).ToList() ,

                    Requestor  = (from a in ds.Tables[4].AsEnumerable()
                                select new ComboGenericClass 
                                {
                                    ID = a.Field<int?>("RequestorID"),
                                    Name = a.Field<string>("Requestor"),
                                    NameCode = a.Field<string>("RequestorCode"),

                                }).ToList() 

                });
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

        public List<PortAgenRequestVendor> GetNonTurnporHotelProviderVendor(short loadType, int VendorID, int contractID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                List<PortAgenRequestVendor> lst = new List<PortAgenRequestVendor>();
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnporHotelProviderVendor");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, loadType);
                db.AddInParameter(dbCommand, "@pVendorID", DbType.Int32, VendorID);
                db.AddInParameter(dbCommand, "@pContractID", DbType.Int32, contractID);

                ds = db.ExecuteDataSet(dbCommand);

                lst = (from n in ds.Tables[0].AsEnumerable()
                       select new PortAgenRequestVendor
                       {
                           VendorID = n.Field<int?>("VendorID"),
                           VendorName = n.Field<string>("VendorName"),
                           VendorContractName = n.Field<string>("VendorContractName"),
                           VendorContractID = n.Field<int?>("VendorContractID"),
                           CurrentID = n.Field<int?>("CurrentID"),
                           CurrentName = n.Field<string>("CurrentName"),

                           MealStandard = n.Field<decimal?>("MealStandard"),
                           MealIncrease = n.Field<decimal?>("MealIncrease"),
                           SingleRate = n.Field<decimal?>("SingleRate"),
                           DoubleRate = n.Field<decimal?>("DoubleRate"),
                           Voucher = n.Field<decimal?>("Voucher"),
                           EmailTo = n.Field<string>("EmailTo"),

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



        public string InsertNonTurnTransactionRequestBooking(DataTable dt, string spName, string userID, string emailTo , string emailCC)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            try
            {
 
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                SFDbCommand = SFDatebase.GetStoredProcCommand(spName);

                SqlParameter param = new SqlParameter("@NonTurnRequestBooking", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SFDbCommand.Parameters.Add(param);

                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, userID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTO", DbType.String, emailTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailCC", DbType.String, emailCC);

                string sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                return sHRID;

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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
            }
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
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                List<NonTurnportGenericList> lst = new List<NonTurnportGenericList>();
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentNonTurnTranspoRequest");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pRequestDate", DbType.DateTime, RequestDate);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);

                SqlParameter param = new SqlParameter("@pNonTurnPortRequest", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand);
 
                lst.Add(new NonTurnportGenericList
                {
                    NonTurnTransportation = (from n in ds.Tables[0].AsEnumerable()
                                             select new NonTurnTransportation
                                             {

                                                    VendorID  = GlobalCode.Field2Int(n["VendorID"]),
                                                    VendorName = GlobalCode.Field2String(n["VendorName"]),
                                                    VendorContractName = GlobalCode.Field2String(n["VendorContractName"]),
                                                    ContractID = GlobalCode.Field2Int(n["ContractID"]),
                                                    CurrentcyID = GlobalCode.Field2Int(n["CurrentcyID"]),
                                                    Currency = GlobalCode.Field2String(n["Currency"]),
                                                    Rate = GlobalCode.Field2Double(n["Rate"]),
                                                    Email = GlobalCode.Field2String(n["Email"]),

                                             }).ToList(),


                    PortAgentVehicleManifestList = (from a in ds.Tables[1].AsEnumerable()
                                         select new PortAgentVehicleManifestList
                                         {

                                             ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                             PortAgentID = GlobalCode.Field2Long(a["PortAgentID"]),
                                             PortAgentName = a.Field<string>("PortAgentName"),

                                             TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                             
                                             IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                             TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                             RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                             SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                             LastName = a.Field<string>("colLastNameVarchar"),
                                             FirstName = a.Field<string>("colFirstNameVarchar"),

                                             VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                             ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                             VesselName = a.Field<string>("VesselName"),
                                             RankName = a.Field<string>("RankName"),
                                             CostCenter = a.Field<string>("CostCenter"),

                                             DeptCity = a.Field<string>("DeptCity"),
                                             ArvlCity = a.Field<string>("ArvlCity"),

                                             DeptDate = a.Field<DateTime?>("DeptDate"),
                                             ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                             DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                             ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                             Carrier = a.Field<string>("Carrier"),
                                             FlightNo = a.Field<string>("FlightNo"),
                                             VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                             CrewStatus = a.Field<string>("CrewStatus"),
                                             DateOnOff = a.Field<DateTime>("DateOnOff"),

                                             PassportNo = a.Field<string>("PassportNo"),
                                             PassportExp = a.Field<string>("PassportExp"),
                                             PassportIssued = a.Field<string>("PassportIssued"),

                                             PickupDate = a.Field<DateTime?>("PickupDate"),
                                             PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                             RouteFrom = a.Field<string>("RouteFrom"),
                                             RouteTo = a.Field<string>("RouteTo"),

                                             RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                             RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                             CityFrom = a.Field<string>("RouteFromCity"),
                                             CityTo = a.Field<string>("RouteToCity"),

                                             RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                             RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                             RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                             RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                             RequestStatus = a.Field<string>("RequestStatus"),

                                             CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                             SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                             VehicleTypeID = GlobalCode.Field2TinyInt(a["colVehicleTypeIdInt"]),


                                         }).ToList(),


                    Currency = (from a in ds.Tables[2].AsEnumerable()
                                select new ComboGenericClass
                                {
                                    ID = a.Field<int?>("colCurrencyIDInt"),
                                    Name = a.Field<string>("colCurrencyNameVarchar"),
                                    NameCode = a.Field<string>("colCurrencyCodeVarchar"),
                                }).ToList(),


                    VehicleType  = (from a in ds.Tables[3].AsEnumerable()
                                    select new VehicleType
                                {
                                    VehicleTypeID  = a.Field<int>("colvehicleTypeIdInt"),
                                    VehicleTypeName = a.Field<string>("colVehicleTypeNameVarchar"),
                                }).ToList(),

                    Requestor  = (from a in ds.Tables[4].AsEnumerable()
                                select new ComboGenericClass 
                                {
                                    ID = a.Field<int?>("RequestorID"),
                                    Name = a.Field<string>("Requestor"),
                                    NameCode = a.Field<string>("RequestorCode"),

                                }).ToList() 



                });


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

        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Muhallidin G Wali
        ///  Date Created:  12-10-2015
        ///  Description:   Get Hotel Manifest TO confirm for new non-turn port
        ///  ------------------------------------------------------------------
        /// </summary>

        public List<NonTurnportGenericList> GetPortNonTurnTransportationRequest(int LoadType, DateTime RequestDate, int PortID,long PortAgentID, string UserID, DataTable dt)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                List<NonTurnportGenericList> lst = new List<NonTurnportGenericList>();
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnTransportationRequest");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pRequestDate", DbType.DateTime, RequestDate);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@PortAgentID", DbType.Int64, PortAgentID);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                
                SqlParameter param = new SqlParameter("@pNonTurnPortRequest", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand);

                lst.Add(new NonTurnportGenericList
                {
                    NonTurnTransportation = (from n in ds.Tables[0].AsEnumerable()
                                             select new NonTurnTransportation
                                             {

                                                 VendorID = GlobalCode.Field2Int(n["VendorID"]),
                                                 VendorName = GlobalCode.Field2String(n["VendorName"]),
                                                 VendorContractName = GlobalCode.Field2String(n["VendorContractName"]),
                                                 ContractID = GlobalCode.Field2Int(n["ContractID"]),
                                                 CurrentcyID = GlobalCode.Field2Int(n["CurrentcyID"]),
                                                 Currency = GlobalCode.Field2String(n["Currency"]),
                                                 Rate = GlobalCode.Field2Double(n["Rate"]),
                                                 Email = GlobalCode.Field2String(n["Email"]),

                                             }).ToList(),


                    PortAgentVehicleManifestList = (from a in ds.Tables[1].AsEnumerable()
                                                    select new PortAgentVehicleManifestList
                                                    {

                                                        ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                        PortAgentID = GlobalCode.Field2Long(a["PortAgentID"]),
                                                        PortAgentName = a.Field<string>("PortAgentName"),

                                                        TransVehicleID = GlobalCode.Field2Long(a["TransVehicleID"]),

                                                        IdBigint = GlobalCode.Field2Long(a["IdBigint"]),
                                                        TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),

                                                        RecordLocator = a.Field<string>("RecordLocator"),
                                                        SeafarerIdInt = GlobalCode.Field2Long(a["SeafarerIdInt"]),

                                                        LastName = a.Field<string>("LastName"),
                                                        FirstName = a.Field<string>("FirstName"),

                                                        VehicleVendorName = a.Field<string>("VehicleVendorName"),
                                                        ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                        VesselName = a.Field<string>("VesselName"),
                                                        RankName = a.Field<string>("RankName"),
                                                        CostCenter = a.Field<string>("CostCenter"),

                                                        DeptCity = a.Field<string>("DeptCity"),
                                                        ArvlCity = a.Field<string>("ArvlCity"),

                                                        DeptDate = a.Field<DateTime?>("DeptDate"),
                                                        ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                        DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                        ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                        Carrier = a.Field<string>("Carrier"),
                                                        FlightNo = a.Field<string>("FlightNo"),
                                                        VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                                        CrewStatus = a.Field<string>("CrewStatus"),
                                                        DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                        PassportNo = a.Field<string>("PassportNo"),
                                                        PassportExp = a.Field<string>("PassportExp"),
                                                        PassportIssued = a.Field<string>("PassportIssued"),

                                                        PickupDate = a.Field<DateTime?>("PickupDate"),
                                                        PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                                        RouteFrom = a.Field<string>("RouteFrom"),
                                                        RouteTo = a.Field<string>("RouteTo"),

                                                        RouteFromID = GlobalCode.Field2Int(a["RouteFromID"]),
                                                        RouteToID = GlobalCode.Field2Int(a["RouteToID"]),

                                                        CityFrom = a.Field<string>("RouteFromCity"),
                                                        CityTo = a.Field<string>("RouteToCity"),

                                                        RouteFromDisplay = a.Field<string>("RouteFromDisplay"),
                                                        RouteToDisplay = a.Field<string>("RouteToDisplay"),

                                                        RateContracted = GlobalCode.Field2Float(a["RateContracted"]),
                                                        RateConfirmed = GlobalCode.Field2Float(a["RateConfirmed"]),

                                                        RequestStatus = a.Field<string>("RequestStatus"),

                                                        CurrencyID = GlobalCode.Field2Int(a["CurrencyID"]),
                                                        SeqNo = GlobalCode.Field2Int(a["SeqNo"]),
                                                        VehicleTypeID = GlobalCode.Field2TinyInt(a["VehicleTypeID"]),


                                                    }).ToList(),

                                                     



                    Currency = (from a in ds.Tables[2].AsEnumerable()
                                select new ComboGenericClass
                                {
                                    ID = a.Field<int?>("colCurrencyIDInt"),
                                    Name = a.Field<string>("colCurrencyNameVarchar"),
                                    NameCode = a.Field<string>("colCurrencyCodeVarchar"),
                                }).ToList(),


                    VehicleType = (from a in ds.Tables[3].AsEnumerable()
                                   select new VehicleType
                                   {
                                       VehicleTypeID = a.Field<int>("colvehicleTypeIdInt"),
                                       VehicleTypeName = a.Field<string>("colVehicleTypeNameVarchar"),
                                   }).ToList(),

                    Requestor = (from a in ds.Tables[4].AsEnumerable()
                                 select new ComboGenericClass
                                 {
                                     ID = a.Field<int?>("RequestorID"),
                                     Name = a.Field<string>("Requestor"),
                                     NameCode = a.Field<string>("RequestorCode"),

                                 }).ToList()



                });


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



        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Muhallidin G Wali
        ///  Date Created:  10-03-2016
        ///  Description:   Get Hotel Manifest TO confirm for new non-turn port
        ///  ------------------------------------------------------------------
        /// </summary>
        public List<NonTurnTransportation> GetNonTurnporTransportionVendor(short loadType, int VendorID, int contractID)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                List<NonTurnTransportation> lst = new List<NonTurnTransportation>();
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnporTransportionVendor");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, loadType);
                db.AddInParameter(dbCommand, "@pVendorID", DbType.Int32, VendorID);
                db.AddInParameter(dbCommand, "@pContractID", DbType.Int32, contractID);

                ds = db.ExecuteDataSet(dbCommand);

              

                lst = (from n in ds.Tables[0].AsEnumerable()
                                         select new NonTurnTransportation
                                         {

                                             VendorID = GlobalCode.Field2Int(n["VendorID"]),
                                             VendorName = GlobalCode.Field2String(n["VendorName"]),
                                             VendorContractName = GlobalCode.Field2String(n["VendorContractName"]),
                                             ContractID = GlobalCode.Field2Int(n["ContractID"]),
                                             CurrentcyID = GlobalCode.Field2Int(n["CurrentcyID"]),
                                             Currency = GlobalCode.Field2String(n["Currency"]),
                                             Rate = GlobalCode.Field2Double(n["Rate"]),
                                             Email = GlobalCode.Field2String(n["Email"]),

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



        ///<summary>
        ///  ------------------------------------------------------------------
        ///  Author:        Muhallidin G Wali
        ///  Date Created:  10-03-2016
        ///  Description:   Get service provider dashboard.
        ///  ------------------------------------------------------------------
        /// </summary>
        public List<PortAgentHotelVehicle> GetNonTurnporHotelVehicleDashboard(short loadType
                , string PortCode, int PortID, int PortAgentID
                , DateTime RequestDate,string userID, int Days)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                List<PortAgentHotelVehicle> lst = new List<PortAgentHotelVehicle>();
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnportHotelVehicleDashboard");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, loadType);
                db.AddInParameter(dbCommand, "@pPortCode", DbType.String, PortCode);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32 , PortID);
                db.AddInParameter(dbCommand, "@pPortAgentIDInt", DbType.Int32, PortAgentID);
                db.AddInParameter(dbCommand, "@pRequestDate", DbType.DateTime, RequestDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, userID);
                db.AddInParameter(dbCommand, "@pDays", DbType.Int32, Days);

                ds = db.ExecuteDataSet(dbCommand);

                lst = (from n in ds.Tables[0].AsEnumerable()
                       select new PortAgentHotelVehicle
                       {

                           PortAgentID =  GlobalCode.Field2Int(n["PortAgentID"]),
                           VendorName =   GlobalCode.Field2String(n["VendorName"]),
                           StartDate = GlobalCode.Field2DateTime(n["StartDate"]),
                           PortID = GlobalCode.Field2Int(n["PortID"]),
                           PortCode = GlobalCode.Field2String(n["PortCode"]),
                           PortName = GlobalCode.Field2String(n["PortName"]),

                           HotelRequestCount = GlobalCode.Field2Int(n["HotelRequestCount"]),
                           HotelConfirmedCount = GlobalCode.Field2Int(n["HotelConfirmedCount"]),
                           HotelPendingCount = GlobalCode.Field2Int(n["HotelPendingCount"]),

                           VehicleRequestCount = GlobalCode.Field2Int(n["VehicleRequestCount"]),
                           VehicleConfirmedCount = GlobalCode.Field2Int(n["VehicleConfirmedCount"]),
                           VehiclePendingCount = GlobalCode.Field2Int(n["VehiclePendingCount"]),


                           Request = GlobalCode.Field2String(n["Request"]),
                           Confirmed = GlobalCode.Field2String(n["Confirmed"]),
                           Pending = GlobalCode.Field2String(n["Pending"]), 

                           UserID =  GlobalCode.Field2String(n["UserID"]),
                           Days = Days.ToString(),
                           ContractID = GlobalCode.Field2Int(n["ContractID"]) 

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

        public SeaportPortagentDaysList GetSeaportPortagentDays(string UserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DataSet ds = null;
            try
            {
                SeaportPortagentDaysList lst = new SeaportPortagentDaysList();
                dbCommand = db.GetStoredProcCommand("uspGetSeaportPortagentDays");
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                ds = db.ExecuteDataSet(dbCommand);
                lst.Seaport = (from n in ds.Tables[0].AsEnumerable()
                                select new PortList
                                {
                                    PortId = GlobalCode.Field2Int(n["SeaportID"]),
                                    PortName = GlobalCode.Field2String(n["PortName"]),
                                }).ToList();
                lst.Portagent  = (from n in ds.Tables[1].AsEnumerable()
                               select new UserBranch
                               {
                                   BranchID = GlobalCode.Field2Int(n["PortAgentVendorID"]),
                                   BranchName = GlobalCode.Field2String(n["PortAgentVendorName"]),
                               }).ToList();
                lst.DaysList = (from n in ds.Tables[2].AsEnumerable()
                                select new DaysList
                                {
                                    Days = GlobalCode.Field2Int(n["Day"]),
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
        /// -------------------------------------------------------------------
        /// Author:         Muhallidin G. Wali
        /// Date Created:   13/Nov/2016
        /// Descrption:     Get Hotel confrim/cancel Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public PortAgentConfirmCancelledManifest PortAgentConfirmCancelledManifest(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, short iLoadType, int iNoOfDay, long iSFID, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;

            DataSet ds = null;
            try
            {
                PortAgentConfirmCancelledManifest listHotel = new PortAgentConfirmCancelledManifest();


                comm = db.GetStoredProcCommand("uspPAConfirmedCancelledManifest");
                db.AddInParameter(comm, "@pStatusIDTinyint", DbType.Int16, iStatusID);
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);

                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);
                db.AddInParameter(comm, "@pSFID", DbType.Int64, iSFID);

                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);


                ds = db.ExecuteDataSet(comm);


                listHotel.ConfirmedCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());

                listHotel.PortAgentConfirmHotelManifestList = (from a in ds.Tables[1].AsEnumerable()
                             select new PortAgentHotelManifestList
                             {
                                 PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                 PortAgentName = a.Field<string>("PortAgentName"),

                                 TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                 IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                 TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                 RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                 SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                 LastName = a.Field<string>("colLastNameVarchar"),
                                 FirstName = a.Field<string>("colFirstNameVarchar"),

                                 HotelName = a.Field<string>("colHotelNameVarchar"),
                                 ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                 VesselName = a.Field<string>("VesselName"),
                                 RankName = a.Field<string>("RankName"),
                                 CostCenter = a.Field<string>("CostCenter"),
                                 Nationality = a.Field<string>("Nationality"),

                                 DeptCity = a.Field<string>("DeptCity"),
                                 ArvlCity = a.Field<string>("ArvlCity"),

                                 DeptDate = a.Field<DateTime?>("DeptDate"),
                                 ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                 DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                 ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                 /*Flight Stats*/
                                 //ActualDepartureDate = a.Field<string>("actDateD"),
                                 //ActualArrivalDate = a.Field<string>("actDateT"),
                                 //ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                 //ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                 //ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),


                                 Carrier = a.Field<string>("Carrier"),
                                 FlightNo = a.Field<string>("FlightNo"),
                                 RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                 CrewStatus = a.Field<string>("CrewStatus"),
                                 DateOnOff = a.Field<DateTime>("DateOnOff"),

                                 PassportNo = a.Field<string>("PassportNo"),
                                 PassportExp = a.Field<string>("PassportExp"),
                                 PassportIssued = a.Field<string>("PassportIssued"),

                                 Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                 Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                 HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                 Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                 RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                 RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                 RequestStatus = a.Field<string>("RequestStatus"),
                                 IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                 IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                 Comment = a.Field<string>("colCommentVarchar"),
                                 ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                 CommentBy = a.Field<string>("colCommentByVarchar"),

                                 CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                 IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                 IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                 RemarkID = GlobalCode.Field2Int(a["colRemarkIDInt"]),
                                 Remark = GlobalCode.Field2String(a["colRemarkVarchar"]),

                             }).ToList();



                listHotel.ConfirmedCount  = GlobalCode.Field2Int(ds.Tables[2].Rows[0][0].ToString());

                listHotel.PortAgentCancelledHotelManifestList = (from a in ds.Tables[3].AsEnumerable()
                                                               select new PortAgentHotelManifestList
                                                               {
                                                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                                                   PortAgentName = a.Field<string>("PortAgentName"),

                                                                   TransHotelID = GlobalCode.Field2Long(a["colTransHotelIDBigInt"]),
                                                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                                                   LastName = a.Field<string>("colLastNameVarchar"),
                                                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                                                   HotelName = a.Field<string>("colHotelNameVarchar"),
                                                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                                   VesselName = a.Field<string>("VesselName"),
                                                                   RankName = a.Field<string>("RankName"),
                                                                   CostCenter = a.Field<string>("CostCenter"),
                                                                   Nationality = a.Field<string>("Nationality"),

                                                                   DeptCity = a.Field<string>("DeptCity"),
                                                                   ArvlCity = a.Field<string>("ArvlCity"),

                                                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                                   /*Flight Stats*/
                                                                   //ActualDepartureDate = a.Field<string>("actDateD"),
                                                                   //ActualArrivalDate = a.Field<string>("actDateT"),
                                                                   //ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                                                   //ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                                                   //ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),


                                                                   Carrier = a.Field<string>("Carrier"),
                                                                   FlightNo = a.Field<string>("FlightNo"),
                                                                   RoomType = GlobalCode.Field2Float(a["RoomType"]).ToString(),
                                                                   CrewStatus = a.Field<string>("CrewStatus"),
                                                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                                   PassportNo = a.Field<string>("PassportNo"),
                                                                   PassportExp = a.Field<string>("PassportExp"),
                                                                   PassportIssued = a.Field<string>("PassportIssued"),

                                                                   Checkin = a.Field<DateTime?>("colTimeSpanStartDate"),
                                                                   Checkout = a.Field<DateTime?>("colTimeSpanEndDate"),

                                                                   HotelNites = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),
                                                                   Voucher = GlobalCode.Field2Float(a["Voucher"]),

                                                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),
                                                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),

                                                                   RequestStatus = a.Field<string>("RequestStatus"),
                                                                   IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                                                   IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                                                   Comment = a.Field<string>("colCommentVarchar"),
                                                                   ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                                                   CommentBy = a.Field<string>("colCommentByVarchar"),

                                                                   CurrencyID = GlobalCode.Field2Int(a["colCurrencyInt"]),
                                                                   IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                                                   IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                                                   RemarkID = GlobalCode.Field2Int(a["colRemarkIDInt"]),
                                                                   Remark = GlobalCode.Field2String(a["colRemarkVarchar"]),

                                                               }).ToList();



                listHotel.VehicleConfirmedCount = GlobalCode.Field2Int(ds.Tables[4].Rows[0][0].ToString());


                listHotel.PortAgentConfirmVehicleManifestList = (from a in ds.Tables[5].AsEnumerable()
                                                                   select new PortAgentVehicleManifestList
                                                                   {
                                                                       PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                                                       PortAgentName = a.Field<string>("PortAgentName"),

                                                                       TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                                                       IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                                                       TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                                                       RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                                                       SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                                                       LastName = a.Field<string>("colLastNameVarchar"),
                                                                       FirstName = a.Field<string>("colFirstNameVarchar"),

                                                                       VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                                                       ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                                       VesselName = a.Field<string>("VesselName"),
                                                                       RankName = a.Field<string>("RankName"),
                                                                       CostCenter = a.Field<string>("CostCenter"),
                                                                       Nationality = a.Field<string>("Nationality"),

                                                                       DeptCity = a.Field<string>("DeptCity"),
                                                                       ArvlCity = a.Field<string>("ArvlCity"),

                                                                       /*Flight Stats
                                                                       ActualDepartureDate = a.Field<string>("actDateD"),
                                                                       ActualArrivalDate = a.Field<string>("actDateT"),
                                                                       ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                                                       ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                                                       ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                                                       */

                                                                       DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                       ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                                       DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                                       ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                                       Carrier = a.Field<string>("Carrier"),
                                                                       FlightNo = a.Field<string>("FlightNo"),
                                                                       VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                                                       CrewStatus = a.Field<string>("CrewStatus"),
                                                                       DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                                       PassportNo = a.Field<string>("PassportNo"),
                                                                       PassportExp = a.Field<string>("PassportExp"),
                                                                       PassportIssued = a.Field<string>("PassportIssued"),

                                                                       PickupDate = a.Field<DateTime?>("PickupDate"),
                                                                       PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                                                       RouteFrom = a.Field<string>("RouteFrom"),
                                                                       RouteTo = a.Field<string>("RouteTo"),

                                                                       RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                                                       RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                                                       CityFrom = a.Field<string>("RouteFromCity"),
                                                                       CityTo = a.Field<string>("RouteToCity"),

                                                                       RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                                                       RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                                                       RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                                                       RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                                                       RequestStatus = a.Field<string>("RequestStatus"),
                                                                       IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                                                       IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                                                       Comment = a.Field<string>("colCommentVarchar"),
                                                                       TransportationDetails = a.Field<string>("colTransportationDetails"),
                                                                       ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                                                       CommentBy = a.Field<string>("colCommentByVarchar"),

                                                                       IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                                                       IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                                                       Remark = a.Field<string>("Remark"),
                                                                   }).ToList();



                listHotel.VehicleCancelledCount = GlobalCode.Field2Int(ds.Tables[6].Rows[0][0].ToString());


                listHotel.PortAgentCancelledVehicleManifestList = (from a in ds.Tables[7].AsEnumerable()
                                                                 select new PortAgentVehicleManifestList
                                                                 {
                                                                     PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                                                     PortAgentName = a.Field<string>("PortAgentName"),

                                                                     TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                                                     IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                                                     TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                                                     RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                                                     SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                                                     LastName = a.Field<string>("colLastNameVarchar"),
                                                                     FirstName = a.Field<string>("colFirstNameVarchar"),

                                                                     VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                                                     ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                                     VesselName = a.Field<string>("VesselName"),
                                                                     RankName = a.Field<string>("RankName"),
                                                                     CostCenter = a.Field<string>("CostCenter"),
                                                                     Nationality = a.Field<string>("Nationality"),

                                                                     DeptCity = a.Field<string>("DeptCity"),
                                                                     ArvlCity = a.Field<string>("ArvlCity"),

                                                                     /*Flight Stats
                                                                     ActualDepartureDate = a.Field<string>("actDateD"),
                                                                     ActualArrivalDate = a.Field<string>("actDateT"),
                                                                     ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                                                     ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                                                     ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                                                     */

                                                                     DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                     ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                                     DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                                     ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                                     Carrier = a.Field<string>("Carrier"),
                                                                     FlightNo = a.Field<string>("FlightNo"),
                                                                     VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                                                     CrewStatus = a.Field<string>("CrewStatus"),
                                                                     DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                                     PassportNo = a.Field<string>("PassportNo"),
                                                                     PassportExp = a.Field<string>("PassportExp"),
                                                                     PassportIssued = a.Field<string>("PassportIssued"),

                                                                     PickupDate = a.Field<DateTime?>("PickupDate"),
                                                                     PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                                                     RouteFrom = a.Field<string>("RouteFrom"),
                                                                     RouteTo = a.Field<string>("RouteTo"),

                                                                     RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                                                     RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                                                     CityFrom = a.Field<string>("RouteFromCity"),
                                                                     CityTo = a.Field<string>("RouteToCity"),

                                                                     RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                                                     RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                                                     RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                                                     RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                                                     RequestStatus = a.Field<string>("RequestStatus"),
                                                                     IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                                                     IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                                                     Comment = a.Field<string>("colCommentVarchar"),
                                                                     TransportationDetails = a.Field<string>("colTransportationDetails"),
                                                                     ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                                                     CommentBy = a.Field<string>("colCommentByVarchar"),

                                                                     IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                                                     IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                                                     Remark = a.Field<string>("Remark"),
                                                                 }).ToList();



                if (iLoadType == 0)
                {
                    listHotel.listStatus = (from a in ds.Tables[8].AsEnumerable()
                                  select new ManifestStatus
                                  {
                                      iStatusID = GlobalCode.Field2Int(a["colStatusIDTinyint"]),
                                      sStatus = a.Field<string>("colStatusName"),

                                  }).ToList();
                     
                }

                return listHotel;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }




        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Muhallidin G. Wali
        /// Date Created:   13/Nov/2016
        /// Descrption:     Get Hotel confrim/cancel Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public PortAgentConfirmCancelledManifest PortAgentVehicleRequestdManifest(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, short iLoadType, int iNoOfDay, long iSFID, int iStartRow, int iMaxRow)
        {

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;

            DataSet ds = null;
            try
            {
                PortAgentConfirmCancelledManifest listVehicle = new PortAgentConfirmCancelledManifest();

                comm = db.GetStoredProcCommand("uspGetPortAgentVehicleRequestdManifest");
                db.AddInParameter(comm, "@pStatusIDTinyint", DbType.Int32, iStatusID);
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);
                db.AddInParameter(comm, "@pSFID", DbType.Int64, iSFID);

                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);

                ds = db.ExecuteDataSet(comm);


             
                listVehicle.VehicleConfirmedCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());

                listVehicle.PortAgentResquestVehicleManifestList = (from a in ds.Tables[1].AsEnumerable()
                               select new PortAgentVehicleManifestList
                               {
                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                   PortAgentName = a.Field<string>("PortAgentName"),

                                   TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                   LastName = a.Field<string>("colLastNameVarchar"),
                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                   VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                   VesselName = a.Field<string>("VesselName"),
                                   RankName = a.Field<string>("RankName"),
                                   CostCenter = a.Field<string>("CostCenter"),
                                   Nationality = a.Field<string>("Nationality"),

                                   DeptCity = a.Field<string>("DeptCity"),
                                   ArvlCity = a.Field<string>("ArvlCity"),

                                   /*Flight Stats
                                   ActualDepartureDate = a.Field<string>("actDateD"),
                                   ActualArrivalDate = a.Field<string>("actDateT"),
                                   ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                   ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                   ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                   */

                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                   Carrier = a.Field<string>("Carrier"),
                                   FlightNo = a.Field<string>("FlightNo"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                   CrewStatus = a.Field<string>("CrewStatus"),
                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                   PassportNo = a.Field<string>("PassportNo"),
                                   PassportExp = a.Field<string>("PassportExp"),
                                   PassportIssued = a.Field<string>("PassportIssued"),

                                   PickupDate = a.Field<DateTime?>("PickupDate"),
                                   PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                   RouteFrom = a.Field<string>("RouteFrom"),
                                   RouteTo = a.Field<string>("RouteTo"),

                                   RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                   RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                   CityFrom = a.Field<string>("RouteFromCity"),
                                   CityTo = a.Field<string>("RouteToCity"),

                                   RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                   RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                   RequestStatus = a.Field<string>("RequestStatus"),
                                   IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                   IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                   Comment = a.Field<string>("colCommentVarchar"),
                                   TransportationDetails = a.Field<string>("colTransportationDetails"),
                                   ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                   CommentBy = a.Field<string>("colCommentByVarchar"),

                                   IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                   IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                   Remark = a.Field<string>("Remark"),
                               }).ToList();

                

                if (iLoadType == 0)
                {
                    List<ManifestStatus> listStatus = new List<ManifestStatus>();
                    listStatus = (from a in ds.Tables[2].AsEnumerable()
                                  select new ManifestStatus
                                  {
                                      iStatusID = GlobalCode.Field2Int(a["colStatusIDTinyint"]),
                                      sStatus = a.Field<string>("colStatusName"),

                                  }).ToList();

                }

                listVehicle.PortAgentConfirmVehicleManifestList = (from a in ds.Tables[3].AsEnumerable()
                                                                 select new PortAgentVehicleManifestList
                                                                 {
                                                                     PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                                                     PortAgentName = a.Field<string>("PortAgentName"),

                                                                     TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                                                     IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                                                     TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                                                     RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                                                     SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                                                     LastName = a.Field<string>("colLastNameVarchar"),
                                                                     FirstName = a.Field<string>("colFirstNameVarchar"),

                                                                     VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                                                     ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                                     VesselName = a.Field<string>("VesselName"),
                                                                     RankName = a.Field<string>("RankName"),
                                                                     CostCenter = a.Field<string>("CostCenter"),
                                                                     Nationality = a.Field<string>("Nationality"),

                                                                     DeptCity = a.Field<string>("DeptCity"),
                                                                     ArvlCity = a.Field<string>("ArvlCity"),

                                                                     /*Flight Stats
                                                                     ActualDepartureDate = a.Field<string>("actDateD"),
                                                                     ActualArrivalDate = a.Field<string>("actDateT"),
                                                                     ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                                                     ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                                                     ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                                                     */

                                                                     DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                     ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                                     DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                                     ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                                     Carrier = a.Field<string>("Carrier"),
                                                                     FlightNo = a.Field<string>("FlightNo"),
                                                                     VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                                                     CrewStatus = a.Field<string>("CrewStatus"),
                                                                     DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                                     PassportNo = a.Field<string>("PassportNo"),
                                                                     PassportExp = a.Field<string>("PassportExp"),
                                                                     PassportIssued = a.Field<string>("PassportIssued"),

                                                                     PickupDate = a.Field<DateTime?>("PickupDate"),
                                                                     PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                                                     RouteFrom = a.Field<string>("RouteFrom"),
                                                                     RouteTo = a.Field<string>("RouteTo"),

                                                                     RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                                                     RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                                                     CityFrom = a.Field<string>("RouteFromCity"),
                                                                     CityTo = a.Field<string>("RouteToCity"),

                                                                     RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                                                     RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                                                     RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                                                     RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                                                     RequestStatus = a.Field<string>("RequestStatus"),
                                                                     IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                                                     IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                                                     Comment = a.Field<string>("colCommentVarchar"),
                                                                     TransportationDetails = a.Field<string>("colTransportationDetails"),
                                                                     ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                                                     CommentBy = a.Field<string>("colCommentByVarchar"),

                                                                     IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                                                     IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                                                     Remark = a.Field<string>("Remark"),
                                                                 }).ToList();



                listVehicle.PortAgentCancelledVehicleManifestList = (from a in ds.Tables[4].AsEnumerable()
                                                                   select new PortAgentVehicleManifestList
                                                                   {
                                                                       PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                                                       PortAgentName = a.Field<string>("PortAgentName"),

                                                                       TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                                                       IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                                                       TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                                                       RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                                                       SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                                                       LastName = a.Field<string>("colLastNameVarchar"),
                                                                       FirstName = a.Field<string>("colFirstNameVarchar"),

                                                                       VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                                                       ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                                       VesselName = a.Field<string>("VesselName"),
                                                                       RankName = a.Field<string>("RankName"),
                                                                       CostCenter = a.Field<string>("CostCenter"),
                                                                       Nationality = a.Field<string>("Nationality"),

                                                                       DeptCity = a.Field<string>("DeptCity"),
                                                                       ArvlCity = a.Field<string>("ArvlCity"),

                                                                       /*Flight Stats
                                                                       ActualDepartureDate = a.Field<string>("actDateD"),
                                                                       ActualArrivalDate = a.Field<string>("actDateT"),
                                                                       ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                                                       ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                                                       ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                                                       */

                                                                       DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                       ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                                       DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                                       ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                                       Carrier = a.Field<string>("Carrier"),
                                                                       FlightNo = a.Field<string>("FlightNo"),
                                                                       VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                                                       CrewStatus = a.Field<string>("CrewStatus"),
                                                                       DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                                       PassportNo = a.Field<string>("PassportNo"),
                                                                       PassportExp = a.Field<string>("PassportExp"),
                                                                       PassportIssued = a.Field<string>("PassportIssued"),

                                                                       PickupDate = a.Field<DateTime?>("PickupDate"),
                                                                       PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                                                       RouteFrom = a.Field<string>("RouteFrom"),
                                                                       RouteTo = a.Field<string>("RouteTo"),

                                                                       RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                                                       RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                                                       CityFrom = a.Field<string>("RouteFromCity"),
                                                                       CityTo = a.Field<string>("RouteToCity"),

                                                                       RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                                                       RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                                                       RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                                                       RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                                                       RequestStatus = a.Field<string>("RequestStatus"),
                                                                       IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                                                       IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                                                       Comment = a.Field<string>("colCommentVarchar"),
                                                                       TransportationDetails = a.Field<string>("colTransportationDetails"),
                                                                       ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                                                       CommentBy = a.Field<string>("colCommentByVarchar"),

                                                                       IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                                                       IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                                                       Remark = a.Field<string>("Remark"),
                                                                   }).ToList();


                return listVehicle;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }

                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   12/Mar/2014
        /// Descrption:     Get Vehicle Manifest
        /// -------------------------------------------------------------------
        /// Modified by:    Josephine Monteza
        /// Date Modified:  06/Jan/2015
        /// Descrption:     Add IsTagged and IsVendor fields
        /// -------------------------------------------------------------------
        /// </summary>
        public void PortAgentManifestGetConfirmVehicle(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dtVehicle = null;
            DataTable dtStatus = null;

            DataSet ds = null;
            try
            {
                int iTotalRow = 0;
                List<PortAgentVehicleManifestList> listVehicle = new List<PortAgentVehicleManifestList>();
                HttpContext.Current.Session["PortAgentVehicleManifestList"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestCount"] = iTotalRow;


                comm = db.GetStoredProcCommand("uspPortAgentManifestGetConfirmVehicle");
                db.AddInParameter(comm, "@pStatusIDTinyint", DbType.Int32, iStatusID);
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);
                db.AddInParameter(comm, "@pSFID", DbType.Int64, iSFID);

                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);

                ds = db.ExecuteDataSet(comm);


                dtVehicle = ds.Tables[1];
                iTotalRow = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());

                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new PortAgentVehicleManifestList
                               {
                                   PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                   PortAgentName = a.Field<string>("PortAgentName"),

                                   TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                   IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                   TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                   RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                   SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                   LastName = a.Field<string>("colLastNameVarchar"),
                                   FirstName = a.Field<string>("colFirstNameVarchar"),

                                   VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                   ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                   VesselName = a.Field<string>("VesselName"),
                                   RankName = a.Field<string>("RankName"),
                                   CostCenter = a.Field<string>("CostCenter"),
                                   Nationality = a.Field<string>("Nationality"),

                                   DeptCity = a.Field<string>("DeptCity"),
                                   ArvlCity = a.Field<string>("ArvlCity"),

                                   /*Flight Stats
                                   ActualDepartureDate = a.Field<string>("actDateD"),
                                   ActualArrivalDate = a.Field<string>("actDateT"),
                                   ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                   ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                   ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                   */

                                   DeptDate = a.Field<DateTime?>("DeptDate"),
                                   ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                   DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                   ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                   Carrier = a.Field<string>("Carrier"),
                                   FlightNo = a.Field<string>("FlightNo"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                   CrewStatus = a.Field<string>("CrewStatus"),
                                   DateOnOff = a.Field<DateTime>("DateOnOff"),

                                   PassportNo = a.Field<string>("PassportNo"),
                                   PassportExp = a.Field<string>("PassportExp"),
                                   PassportIssued = a.Field<string>("PassportIssued"),

                                   PickupDate = a.Field<DateTime?>("PickupDate"),
                                   PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                   RouteFrom = a.Field<string>("RouteFrom"),
                                   RouteTo = a.Field<string>("RouteTo"),

                                   RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                   RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                   CityFrom = a.Field<string>("RouteFromCity"),
                                   CityTo = a.Field<string>("RouteToCity"),

                                   RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                   RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                   RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                   RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                   RequestStatus = a.Field<string>("RequestStatus"),
                                   IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                   IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                   Comment = a.Field<string>("colCommentVarchar"),
                                   TransportationDetails = a.Field<string>("colTransportationDetails"),
                                   ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                   CommentBy = a.Field<string>("colCommentByVarchar"),

                                   IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                   IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                   Remark = a.Field<string>("Remark"),
                               }).ToList();

                HttpContext.Current.Session["PortAgentVehicleManifestList"] = listVehicle;
                HttpContext.Current.Session["PortAgentVehicleManifestCount"] = iTotalRow;

                if (iLoadType == 0)
                {
                    List<ManifestStatus> listStatus = new List<ManifestStatus>();
                    HttpContext.Current.Session["ManifestStatus"] = listStatus;


                    dtStatus = ds.Tables[2];
                    listStatus = (from a in dtStatus.AsEnumerable()
                                  select new ManifestStatus
                                  {
                                      iStatusID = GlobalCode.Field2Int(a["colStatusIDTinyint"]),
                                      sStatus = a.Field<string>("colStatusName"),

                                  }).ToList();

                    HttpContext.Current.Session["ManifestStatus"] = listStatus;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtStatus != null)
                {
                    dtStatus.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }






        /// <summary>
        /// -------------------------------------------------------------------
        /// Author:         Muhallidin G Wali
        /// Date Created:   21/NoV/2016
        /// Descrption:     Get Vehicle Manifest
        /// -------------------------------------------------------------------
        /// </summary>
        public PortAgentConfirmCancelledManifest GetPortAgentConfirmVehicleManifest(int iStatusID, int iPortAgentID, string sDate, string sUserID, string sRole,
            string sOrderBy, Int16 iLoadType, int iNoOfDay, Int64 iSFID, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;

            DataSet ds = null;
            try
            {
                PortAgentConfirmCancelledManifest listVehicle = new PortAgentConfirmCancelledManifest();


                comm = db.GetStoredProcCommand("uspGetPortAgentConfirmVehicleManifest");
                db.AddInParameter(comm, "@pStatusIDTinyint", DbType.Int32, iStatusID);
                db.AddInParameter(comm, "@pPortAgentID", DbType.Int32, iPortAgentID);
                db.AddInParameter(comm, "@pDate", DbType.DateTime, GlobalCode.Field2DateTime(sDate));
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pRole", DbType.String, sRole);
                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);
                db.AddInParameter(comm, "@pDayCount", DbType.Int32, iNoOfDay);
                db.AddInParameter(comm, "@pSFID", DbType.Int64, iSFID);

                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);

                ds = db.ExecuteDataSet(comm);



                listVehicle.VehicleConfirmedCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());


                listVehicle.PortAgentConfirmVehicleManifestList = (from a in ds.Tables[1].AsEnumerable()
                                                                   select new PortAgentVehicleManifestList
                                                                   {
                                                                       PortAgentID = GlobalCode.Field2Long(a["colPortAgentVendorIDInt"]),
                                                                       PortAgentName = a.Field<string>("PortAgentName"),

                                                                       TransVehicleID = GlobalCode.Field2Long(a["colTransVehicleIDBigint"]),
                                                                       IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                                                       TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),

                                                                       RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                                                       SeafarerIdInt = GlobalCode.Field2Long(a["colSeafarerIdInt"]),

                                                                       LastName = a.Field<string>("colLastNameVarchar"),
                                                                       FirstName = a.Field<string>("colFirstNameVarchar"),

                                                                       VehicleVendorName = a.Field<string>("colVehicleVendorName"),
                                                                       ConfirmationNo = a.Field<string>("ConfirmationNo"),

                                                                       VesselName = a.Field<string>("VesselName"),
                                                                       RankName = a.Field<string>("RankName"),
                                                                       CostCenter = a.Field<string>("CostCenter"),
                                                                       Nationality = a.Field<string>("Nationality"),

                                                                       DeptCity = a.Field<string>("DeptCity"),
                                                                       ArvlCity = a.Field<string>("ArvlCity"),

                                                                       /*Flight Stats
                                                                       ActualDepartureDate = a.Field<string>("actDateD"),
                                                                       ActualArrivalDate = a.Field<string>("actDateT"),
                                                                       ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                                                       ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                                                       ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),

                                                                       */

                                                                       DeptDate = a.Field<DateTime?>("DeptDate"),
                                                                       ArvlDate = a.Field<DateTime?>("ArvlDate"),

                                                                       DeptTime = a.Field<TimeSpan?>("DeptTime"),
                                                                       ArvlTime = a.Field<TimeSpan?>("ArvlTime"),

                                                                       Carrier = a.Field<string>("Carrier"),
                                                                       FlightNo = a.Field<string>("FlightNo"),
                                                                       VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                                                       CrewStatus = a.Field<string>("CrewStatus"),
                                                                       DateOnOff = a.Field<DateTime>("DateOnOff"),

                                                                       PassportNo = a.Field<string>("PassportNo"),
                                                                       PassportExp = a.Field<string>("PassportExp"),
                                                                       PassportIssued = a.Field<string>("PassportIssued"),

                                                                       PickupDate = a.Field<DateTime?>("PickupDate"),
                                                                       PickupTime = a.Field<TimeSpan?>("PickupTime"),

                                                                       RouteFrom = a.Field<string>("RouteFrom"),
                                                                       RouteTo = a.Field<string>("RouteTo"),

                                                                       RouteFromID = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                                                                       RouteToID = GlobalCode.Field2Int(a["colRouteIDToInt"]),

                                                                       CityFrom = a.Field<string>("RouteFromCity"),
                                                                       CityTo = a.Field<string>("RouteToCity"),

                                                                       RouteFromDisplay = a.Field<string>("colRouteFromVarchar"),
                                                                       RouteToDisplay = a.Field<string>("colRouteToVarchar"),

                                                                       RateContracted = GlobalCode.Field2Float(a["colContractedRateMoney"]),
                                                                       RateConfirmed = GlobalCode.Field2Float(a["colConfirmRateMoney"]),

                                                                       RequestStatus = a.Field<string>("RequestStatus"),
                                                                       IsConfirmVisible = GlobalCode.Field2Bool(a["IsConfirmVisible"]),
                                                                       IsCancelVisible = GlobalCode.Field2Bool(a["IsCancelVisible"]),

                                                                       Comment = a.Field<string>("colCommentVarchar"),
                                                                       TransportationDetails = a.Field<string>("colTransportationDetails"),
                                                                       ConfirmedBy = a.Field<string>("colConfirmByVarchar"),
                                                                       CommentBy = a.Field<string>("colCommentByVarchar"),

                                                                       IsTagged = GlobalCode.Field2Bool(a["IsTagged"]),
                                                                       IsVendor = GlobalCode.Field2Bool(a["IsVendor"]),
                                                                       Remark = a.Field<string>("Remark"),
                                                                   }).ToList();


                if (iLoadType == 0)
                {
                    List<ManifestStatus> listStatus = new List<ManifestStatus>();


                    listVehicle.listStatus = (from a in ds.Tables[2].AsEnumerable()
                                              select new ManifestStatus
                                              {
                                                  iStatusID = GlobalCode.Field2Int(a["colStatusIDTinyint"]),
                                                  sStatus = a.Field<string>("colStatusName"),

                                              }).ToList();

                }


                return listVehicle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Dispose();
                }

                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

    }
}

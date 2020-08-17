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
    public class CrewAdminDAL
    {
        /// <summary>
        /// Author:         Muhallidin G wali
        /// Date Created:   03/02/2012
        /// Descrption:     Get all Crew Admin assignment queries to list
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  22/08/2012
        /// Description:    Add air details
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/08/2012
        /// Description:    Add list of nationality, gender, rank and count
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  08/10/2012
        /// Description:    Add IsMeetGreet, IsPortAgent, IsHotelVendor
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  09/Jan/2013
        /// Description:    Use VesselList for multiple vessel
        ///                 Add passport details
        /// -----------------------------------------------------------------
        /// Modified By:    Marco Abejar
        /// Modified Date:  03/May/2013
        /// Description:    Add crew count for loadtype 1
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  20/Dec/2013
        /// Description:    Add parameter iAirLeg
        /// -----------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  08/Jan/2014
        /// Description:    Add parameter iRouteFrom, iRouteTo
        /// -----------------------------------------------------------------
        /// </summary>
        /// <param name="LoadType"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="UserID"></param>
        /// <param name="Role"></param>
        /// <param name="OrderBy"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <param name="VesselID"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <returns></returns>
        public List<CrewAdminGenericClass> LoadCrewList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status, 
            Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo, int StartRow, int MaxRow)
        {
            List<CrewAdminGenericClass> CrewAdminTables = new List<CrewAdminGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtCrewAdmin = null;
            DataTable dtNationality = null;
            DataTable dtGender = null;
            DataTable dtRank = null;
            DataTable dtCount = null;
            DataTable dtVessel = null;
            DataTable dtRoute = null;
           
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminPage");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);
                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, GlobalCode.Field2TinyInt(FilterByName));
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, GlobalCode.Field2Int(NationalityID));
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, GlobalCode.Field2Int(Gender));
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, GlobalCode.Field2Int(RankID));
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);
                db.AddInParameter(dbCommand, "@pShowLegInt", DbType.Int16, iAirLeg);

                db.AddInParameter(dbCommand, "@pRouteIDFrom", DbType.Int16, iRouteFrom);
                db.AddInParameter(dbCommand, "@pRouteIDTo", DbType.Int16, iRouteTo);

                dbCommand.CommandTimeout = 0; //in sec
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                dtCrewAdmin = ds.Tables[1];              

                if (dtCrewAdmin.Rows.Count > 0)
                {
                    DataView dv = dtCrewAdmin.DefaultView;
                    dv.Sort = OrderBy;
                    dtCrewAdmin = dv.ToTable();
                }
                
                List<CrewAdminList> recordList = new List<CrewAdminList>();
                recordList = (from a in dtCrewAdmin.AsEnumerable()
                         select new CrewAdminList
                         {
                             IsManual = GlobalCode.Field2Bool(a["IsManual"]),
                             IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                             E1TravelRequest = GlobalCode.Field2Int(a["E1TravelRequest"]),
                             RequestID = GlobalCode.Field2Int(a["RequestID"]),
                             TravelRequestID = GlobalCode.Field2Long(a["TravelRequestID"]),
                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                             SeafarerID = GlobalCode.Field2Long(a["SeafarerID"]),
                             Name = GlobalCode.Field2String(a["Name"]),
                             DateOnOff = a.Field<DateTime>("DateOnOff"),
                             
                             Status = GlobalCode.Field2String(a["Status"]),
                             Brand = GlobalCode.Field2String(a["Brand"]),
                             Vessel = GlobalCode.Field2String(a["Vessel"]),
                             //PortCode = GlobalCode.Field2String(a["PortCode"]),
                             Port = GlobalCode.Field2String(a["Port"]),
                             //RankCode = GlobalCode.Field2String(a["RankCode"]),
                             Rank = GlobalCode.Field2String(a["Rank"]),
                             
                             ReasonCode = GlobalCode.Field2String(a["ReasonCode"]),
                             IsWithSail = GlobalCode.Field2Bool(a["IsWithSail"]),

                             Arrival = GlobalCode.Field2String(a["AirportArrival"]),
                             Departure = GlobalCode.Field2String(a["AirportDeparture"]),
                             ArrivalDateTime = a.Field<DateTime?>("colArrivalDateTime"),
                             DepartureDateTime = a.Field<DateTime?>("colDepartureDateTime"),
                             FlightNo = GlobalCode.Field2String(a["colFlightNoVarchar"]),
                             Airline = GlobalCode.Field2String(a["Airline"]),

                             /*add actual arrival time and arrival gate*/

                             ActualArrivalDate = GlobalCode.Field2String(a["actDateT"]),
                             ActualDepartureDate = GlobalCode.Field2String(a["actDateD"]),
                             ActualArrivalGate = GlobalCode.Field2String(a["FlightArrGate"]),
                             ActualArrivalStatus = GlobalCode.Field2String(a["FlightStatus"]),
                             ActualArrivalBaggage = GlobalCode.Field2String(a["FlightBaggageClaim"]),

                             Hotel = GlobalCode.Field2String(a["Hotel"]),
                             IsMeetGreet = GlobalCode.Field2Bool(a["IsMeetGreet"]),
                             IsPortAgent = GlobalCode.Field2Bool(a["IsPortAgent"]),
                             IsHotelVendor = GlobalCode.Field2Bool(a["IsHotelVendor"]),
                             Remarks = a.Field<string>("Remarks"),
                             
                             PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                             PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                             PassportExp  = GlobalCode.Field2String(a["PassportExp"]),

                             Checkin = a.Field<DateTime?>("CheckIn"),
                             Checkout = a.Field<DateTime?>("Checkout"),
                             SingleDouble = a.Field<string>("colSingleDoubleFloat"),
                             Gender = a.Field<string>("colGender"),
                             CostCenter = a.Field<string>("colCostCenter"),
                             Nationality = a.Field<string>("colNationality"),
                             MealAllowance = a.Field<string>("colMealAllowance"),
                             Duration = GlobalCode.Field2Int(a["Duration"]),
                             Lastname = a.Field<string>("Lastname"),
                             Firstname = a.Field<string>("Firstname"),
                             IsVisible = GlobalCode.Field2Bool(a["IsVisible"]),
                             SequenceNo = GlobalCode.Field2Int(a["SeqNo"]),
                             Birthday = a.Field<DateTime?>("colBirthday"),


                             TransVehicleID = GlobalCode.Field2Int(a["TransVehicleID"]),
                             IsCheckBoxForVehicleVisible = GlobalCode.Field2Bool(a["IsCheckBoxForVehicleVisible"]),
                             IsVisibleToVehicleVendor = GlobalCode.Field2Bool(a["IsVisibleToVehicleVendor"]),
                             IsNeedVehicleBooking = GlobalCode.Field2Bool(a["IsNeedVehicleBooking"]),
                             IsToPrintItinerary = GlobalCode.Field2Bool(a["IsToPrintItinerary"]),
                             
                             RouteFrom = a.Field<string>("RouteFrom"),
                             RouteTo = a.Field<string>("RouteTo"),
                             PickupDatetime = a.Field<DateTime?>("PickupDateTime"),
                             VehicleName = a["colVehicleVendorNameVarchar"].ToString(),
                             Confirm = a["Confirm"].ToString(),
                             ConfirmBy = a["colConfirmedBy"].ToString(),
                             ConfirmDate = a["colConfirmedDate"].ToString(),

                             DateHired = a["DateHired"].ToString(),

                         }
                     ).ToList();

                if (LoadType == 0)
                {
                    List<VesselDTO> VesselList = new List<VesselDTO>();
                    dtVessel = ds.Tables[2];
                    VesselList = (from a in dtVessel.AsEnumerable()
                                select new VesselDTO
                                {
                                    VesselIDString = GlobalCode.Field2String(a["colVesselIdInt"]),
                                    VesselNameString = GlobalCode.Field2String(a["Vessel"]),
                                }).ToList();
                    HttpContext.Current.Session["CrewAdminVessel"] = VesselList;

                    List<VehicleRoute> listRoute = new List<VehicleRoute>();
                    HttpContext.Current.Session["CrewAdminRoute"] = listRoute;

                    dtNationality = ds.Tables[3];
                    dtGender = ds.Tables[4];
                    dtRank = ds.Tables[5];
                    dtCount = ds.Tables[6];
                    dtRoute = ds.Tables[7];


                    int iOnCount = 0;
                    int iOffCount = 0;

                    if (dtCount.Rows.Count > 0)
                    {
                        var StatusCount = (from a in dtCount.AsEnumerable()
                                           //where GlobalCode.Field2String(a["Status"]).Equals("On")
                                               select new 
                                               {
                                                    iCount = GlobalCode.Field2Int(a["StatusCount"]),
                                                    status = GlobalCode.Field2String(a["Status"])
                                               } 
                                        ).ToList();
                        if (StatusCount.Count > 0)
                        {
                            var on = (from a in StatusCount
                                      where a.status.ToUpper().Equals("ON")
                                      select new
                                      {
                                          iCount = a.iCount
                                      }).ToList();
                            var off = (from a in StatusCount
                                       where a.status.ToUpper().Equals("OFF")
                                       select new
                                       {
                                           iCount = a.iCount
                                       }).ToList();
                            if (on.Count > 0)
                            {
                                iOnCount = GlobalCode.Field2Int(on[0].iCount);
                            }
                            if (off.Count > 0)
                            {
                                iOffCount = GlobalCode.Field2Int(off[0].iCount);
                            }
                        }
                    }

                    CrewAdminTables.Add(new CrewAdminGenericClass()
                    {
                        CrewAdminList = recordList,
                        CrewAdminListCount = maxRows,
                        NationalityList = (from a in dtNationality.AsEnumerable()
                                           select new NationalityList
                                           {
                                               NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                                               Nationality = GlobalCode.Field2String(a["Nationality"])
                                           }).ToList(),

                        GenderList = (from a in dtGender.AsEnumerable()
                                      select new GenderList
                                      {
                                          GenderID = GlobalCode.Field2Int(a["GenderID"]),
                                          Gender = GlobalCode.Field2String(a["Gender"])
                                      }).ToList(),

                        RankList = (from a in dtRank.AsEnumerable()
                                    select new RankList
                                    {
                                        RankID = GlobalCode.Field2Int(a["RankID"]),
                                        Rank = GlobalCode.Field2String(a["Rank"])
                                    }).ToList(),
                        OnCount = iOnCount,
                        OffCount = iOffCount
                        
                    });

                    listRoute = (from a in dtRoute.AsEnumerable()
                                 select new VehicleRoute
                                 {
                                     RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                     RouteDesc = GlobalCode.Field2String(a["colRouteNameVarchar"]),
                                 }).ToList();
                    HttpContext.Current.Session["CrewAdminRoute"] = listRoute;
                }
                else
                {
                    dtCount = ds.Tables[2];

                    int iOnCount = 0;
                    int iOffCount = 0;

                    if (dtCount.Rows.Count > 0)
                    {
                        var StatusCount = (from a in dtCount.AsEnumerable()
                                           //where GlobalCode.Field2String(a["Status"]).Equals("On")
                                           select new
                                           {
                                               iCount = GlobalCode.Field2Int(a["StatusCount"]),
                                               status = GlobalCode.Field2String(a["Status"])
                                           }
                                        ).ToList();
                        if (StatusCount.Count > 0)
                        {
                            var on = (from a in StatusCount
                                      where a.status.ToUpper().Equals("ON")
                                      select new
                                      {
                                          iCount = a.iCount
                                      }).ToList();
                            var off = (from a in StatusCount
                                       where a.status.ToUpper().Equals("OFF")
                                       select new
                                       {
                                           iCount = a.iCount
                                       }).ToList();
                            if (on.Count > 0)
                            {
                                iOnCount = GlobalCode.Field2Int(on[0].iCount);
                            }
                            if (off.Count > 0)
                            {
                                iOffCount = GlobalCode.Field2Int(off[0].iCount);
                            }
                        }
                    }

                    CrewAdminTables.Add(new CrewAdminGenericClass()
                    {
                        CrewAdminList = recordList,
                        CrewAdminListCount = maxRows,
                        OnCount = iOnCount,
                        OffCount = iOffCount
                    });
                }
                return CrewAdminTables;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtCrewAdmin != null)
                {
                    dtCrewAdmin.Dispose();
                }
                if (dtNationality != null)
                {
                    dtNationality.Dispose();
                }
                if (dtGender != null)
                {
                    dtGender.Dispose();
                }
                if (dtRank != null)
                {
                    dtRank.Dispose();
                }
                if (dtCount != null)
                {
                    dtCount.Dispose();
                }
                if (dtVessel != null)
                {
                    dtVessel.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }

            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/Jan/2013
        /// Descrption:     Get Crew Admin Manifest to Export
        /// -----------------------------------------------------------------
        /// </summary>
        /// <param name="sUser"></param>
        /// <returns></returns>
        public static DataTable GetCrewAdminManifestExport( string sUser)
        {
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspSelectCrewAdminExport");
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, sUser);
                dt = db.ExecuteDataSet(comm).Tables[0];
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
                if (comm != null)
                {
                    comm.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   22/Dec/2013
        /// Description:    Get details of crew to be added / cancelled in vehicle manifest
        /// ---------------------------------------------------------------
        /// </summary>
        public static void GetVehicleToAddCancel(DataTable dtTranspo, string UserId,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, bool IsCrewAdminSelectAll)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            DataTable dtAdded = null;
            DataTable dtCancelled = null;
            DataTable dtEmail = null;
            DataTable dtRoute = null;            
      
            try
            {
                List<CrewAdminTransport> AddedList = new List<CrewAdminTransport>();
                List<CrewAdminTransport> CancelledList = new List<CrewAdminTransport>();
                List<VehicleVendorEmail> EmailList = new List<VehicleVendorEmail>();
                List<VehicleRoute> RouteList = new List<VehicleRoute>();

                HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = AddedList;
                HttpContext.Current.Session["CrewAdminTranspo_CancelledList"] = CancelledList;
                HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;
                HttpContext.Current.Session["CrewAdminTranspo_RouteList"] = RouteList;
                
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminTranspo");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                                

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);
                db.AddInParameter(dbCommand, "@pIsCrewAdminSelectAll", DbType.Boolean, IsCrewAdminSelectAll);

                SqlParameter param = new SqlParameter("@TableTranspo", dtTranspo);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
                dtAdded = ds.Tables[0];
                dtCancelled = ds.Tables[1];
                dtEmail = ds.Tables[2];
                dtRoute = ds.Tables[3];

                AddedList = (from a in dtAdded.AsEnumerable()
                             select new CrewAdminTransport
                                     {
                                          IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                          TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                          E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                          SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                          LastName = a.Field<string>("colLastNameVarchar"),
                                          FirstName = a.Field<string>("colFirstNameVarchar"),
                                           
                                          RouteFrom = a.Field<string>("RouteFrom"),
                                          RouteTo = a.Field<string>("RouteTo"),
                                            
                                          FlightNo =  a.Field<string>("colFlightNoVarchar"),
                                          Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                                          Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                                          DepartureDate = a.Field<string>("DepartureDatetime"),
                                          ArrivalDate = a.Field<string>("ArrivalDatetime"),
                                          RecLoc = a.Field<string>("colRecordLocatorVarchar"),
            
                                     }).ToList();

                CancelledList = (from a in dtCancelled.AsEnumerable()
                                     select new CrewAdminTransport
                                     {
                                         IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                         TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                         E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                         SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                         LastName = a.Field<string>("colLastNameVarchar"),
                                         FirstName = a.Field<string>("colFirstNameVarchar"),

                                         RouteFrom = a.Field<string>("RouteFrom"),
                                         RouteTo = a.Field<string>("RouteTo"),

                                         FlightNo = a.Field<string>("colFlightNoVarchar"),
                                         Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                                         Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                                         DepartureDate = a.Field<string>("DepartureDatetime"),
                                         ArrivalDate = a.Field<string>("ArrivalDatetime"),
                                         RecLoc = a.Field<string>("colRecordLocatorVarchar"),

                                     }).ToList();

                EmailList = (from a in dtEmail.AsEnumerable()
                             select new VehicleVendorEmail {
                                 VehicleIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                 VehicleName = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),
                                 VehicleEmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                             }).ToList();

                RouteList = (from a in dtRoute.AsEnumerable()
                             select new VehicleRoute
                             {
                                 RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                 RouteDesc = GlobalCode.Field2String(a["colRouteNameVarchar"]),
                             }).ToList();

                HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = AddedList;
                HttpContext.Current.Session["CrewAdminTranspo_CancelledList"] = CancelledList;
                HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;
                HttpContext.Current.Session["CrewAdminTranspo_RouteList"] = RouteList;

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
                if (dtAdded != null)
                {
                    dtAdded.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (dtEmail != null)
                {
                    dtEmail.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }              
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   23/Dec/2013
        /// Description:    Add/Cancel Transpo using Crew Admin Page
        /// ---------------------------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Created:   03/Jan/2014
        /// Description:    Add Edit also
        /// ---------------------------------------------------------------
        /// </summary>
        public static void AddCancelVehicleManifest(string sComment, string sConfirmedBy, string sPickupDate, string sPickupTime,
            Int16 RouteIDFrom, Int16 RouteIDTo, string RouteFrom, string RouteTo,
            string UserId, string sTo, string sCC, string sBCC,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, string sAddCancelEdit)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            DataTable dtAdded = null;
            DataTable dtCancelled = null;
            DataTable dtEmail = null;

            try
            {
                List<CrewAdminTransport> AddedList = new List<CrewAdminTransport>();
                List<CrewAdminTransport> CancelledList = new List<CrewAdminTransport>();
                List<VehicleVendorEmail> EmailList = new List<VehicleVendorEmail>();


                HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = AddedList;
                HttpContext.Current.Session["CrewAdminTranspo_CancelledList"] = CancelledList;
                HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;


                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSendVehicleTransactionByCrewAdmin");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                db.AddInParameter(dbCommand, "@pConfirmedBy", DbType.String, sConfirmedBy);

                if (sPickupDate != "")
                {
                    db.AddInParameter(dbCommand, "@pPickupDate", DbType.DateTime, sPickupDate);
                }
                if (sPickupTime != "")
                {
                    db.AddInParameter(dbCommand, "@pPickupTime", DbType.DateTime, sPickupTime);
                }

                db.AddInParameter(dbCommand, "@pRouteFromID", DbType.Int16, RouteIDFrom);
                db.AddInParameter(dbCommand, "@pRouteToID", DbType.Int16, RouteIDTo);

                db.AddInParameter(dbCommand, "@pRouteFromOther", DbType.String, RouteFrom);
                db.AddInParameter(dbCommand, "@pRouteToOther", DbType.String, RouteTo);

                db.AddInParameter(dbCommand, "@pReceiver", DbType.String, sTo);
                db.AddInParameter(dbCommand, "@pCopy", DbType.String, sCC);
                db.AddInParameter(dbCommand, "@pBlindCopy", DbType.String, sBCC);                

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.AddInParameter(dbCommand, "@pAddCancelEdit", DbType.String, sAddCancelEdit);
                
                ds = db.ExecuteDataSet(dbCommand, trans);
                dtAdded = ds.Tables[0];
                dtCancelled = ds.Tables[1];
                dtEmail = ds.Tables[2];


                AddedList = (from a in dtAdded.AsEnumerable()
                             select new CrewAdminTransport
                             {
                                 IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                 TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                 E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                 SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                 LastName = a.Field<string>("colLastNameVarchar"),
                                 FirstName = a.Field<string>("colFirstNameVarchar"),

                                 RouteFrom = a.Field<string>("RouteFrom"),
                                 RouteTo = a.Field<string>("RouteTo"),

                                 FlightNo = a.Field<string>("colFlightNoVarchar"),
                                 Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                                 Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                                 DepartureDate = a.Field<string>("DepartureDatetime"),
                                 ArrivalDate = a.Field<string>("ArrivalDatetime"),
                                 RecLoc = a.Field<string>("colRecordLocatorVarchar"),

                             }).ToList();

                CancelledList = (from a in dtCancelled.AsEnumerable()
                                 select new CrewAdminTransport
                                 {
                                     IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                     TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                     E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                     SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                     LastName = a.Field<string>("colLastNameVarchar"),
                                     FirstName = a.Field<string>("colFirstNameVarchar"),

                                     RouteFrom = a.Field<string>("RouteFrom"),
                                     RouteTo = a.Field<string>("RouteTo"),

                                     FlightNo = a.Field<string>("colFlightNoVarchar"),
                                     Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                                     Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                                     DepartureDate = a.Field<string>("DepartureDatetime"),
                                     ArrivalDate = a.Field<string>("ArrivalDatetime"),
                                     RecLoc = a.Field<string>("colRecordLocatorVarchar"),

                                 }).ToList();

                EmailList = (from a in dtEmail.AsEnumerable()
                             select new VehicleVendorEmail
                             {
                                 VehicleIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                 VehicleName = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),
                                 VehicleEmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                             }).ToList();

                HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = AddedList;
                HttpContext.Current.Session["CrewAdminTranspo_CancelledList"] = CancelledList;
                HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;


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
                if (dtAdded != null)
                {
                    dtAdded.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (dtEmail != null)
                {
                    dtEmail.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/Dec/2013
        /// Description:    Get details of crew to be edited in vehicle manifest
        /// ---------------------------------------------------------------
        /// </summary>
        public static void GetVehicleToEdit(DataTable dtTranspo, string UserId,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            DataTable dtEdited = null;
            DataTable dtEmail = null;
            DataTable dtRoute = null;

            try
            {
                List<CrewAdminTransport> EditedList = new List<CrewAdminTransport>();
                List<VehicleVendorEmail> EmailList = new List<VehicleVendorEmail>();

                List<VehicleRoute> RouteList = new List<VehicleRoute>();

                HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = EditedList;
                HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;
                HttpContext.Current.Session["CrewAdminTranspo_RouteList"] = RouteList;
                
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminTranspoToEdit");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SqlParameter param = new SqlParameter("@TableTranspo", dtTranspo);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
                dtEdited = ds.Tables[0];
                dtEmail = ds.Tables[1];
                dtRoute = ds.Tables[2];

                EditedList = (from a in dtEdited.AsEnumerable()
                              select new CrewAdminTransport
                              {
                                  IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                                  TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                                  E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                                  SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                  LastName = a.Field<string>("colLastNameVarchar"),
                                  FirstName = a.Field<string>("colFirstNameVarchar"),

                                  RouteFrom = a.Field<string>("RouteFrom"),
                                  RouteTo = a.Field<string>("RouteTo"),

                                  FlightNo = a.Field<string>("colFlightNoVarchar"),
                                  Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                                  Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                                  DepartureDate = a.Field<string>("DepartureDatetime"),
                                  ArrivalDate = a.Field<string>("ArrivalDatetime"),
                                  RecLoc = a.Field<string>("colRecordLocatorVarchar"),

                                  PickupDateTime = a.Field<DateTime?>("PickupDateTime"),
                              }).ToList();

                EmailList = (from a in dtEmail.AsEnumerable()
                             select new VehicleVendorEmail
                             {
                                 VehicleIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                 VehicleName = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),
                                 VehicleEmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                             }).ToList();

                RouteList = (from a in dtRoute.AsEnumerable()
                             select new VehicleRoute
                             {
                                 RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                 RouteDesc = GlobalCode.Field2String(a["colRouteNameVarchar"]),
                             }).ToList();

                HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = EditedList;
                HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;
                HttpContext.Current.Session["CrewAdminTranspo_RouteList"] = RouteList;

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
                if (dtEdited != null)
                {
                    dtEdited.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   27/Dec/2013
        /// Description:    Edit Transpo using Crew Admin Page
        /// ---------------------------------------------------------------
        /// </summary>
        public static void EditVehicleManifest(string sComment, string sPickupDate, string sPickupTime,
            Int16 RouteIDFrom, Int16 RouteIDTo, string RouteFrom, string RouteTo,
            string UserId, string sTo, string sCC, string sBCC,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;

            DataTable dtEdited = null;
            //DataTable dtCancelled = null;
            //DataTable dtEmail = null;

            try
            {
                List<CrewAdminTransport> AddedList = new List<CrewAdminTransport>();
                List<CrewAdminTransport> CancelledList = new List<CrewAdminTransport>();
                List<VehicleVendorEmail> EmailList = new List<VehicleVendorEmail>();


                //HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = AddedList;
                //HttpContext.Current.Session["CrewAdminTranspo_CancelledList"] = CancelledList;
                //HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;


                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSendVehicleTransactionByCrewAdminEdit");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, sComment);
                if (sPickupDate != "")
                {
                    db.AddInParameter(dbCommand, "@pPickupDate", DbType.DateTime, sPickupDate);
                }
                if (sPickupTime != "")
                {
                    db.AddInParameter(dbCommand, "@pPickupTime", DbType.DateTime, sPickupTime);
                }

                db.AddInParameter(dbCommand, "@pRouteFromID", DbType.Int16, RouteIDFrom);
                db.AddInParameter(dbCommand, "@pRouteToID", DbType.Int16, RouteIDTo);

                db.AddInParameter(dbCommand, "@pRouteFromOther", DbType.String, RouteFrom);
                db.AddInParameter(dbCommand, "@pRouteToOther", DbType.String, RouteTo);

                db.AddInParameter(dbCommand, "@pReceiver", DbType.String, sTo);
                db.AddInParameter(dbCommand, "@pCopy", DbType.String, sCC);
                db.AddInParameter(dbCommand, "@pBlindCopy", DbType.String, sBCC);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);
                //dtAdded = ds.Tables[0];
                //dtCancelled = ds.Tables[1];
                //dtEmail = ds.Tables[2];


                //AddedList = (from a in dtAdded.AsEnumerable()
                //             select new CrewAdminTransport
                //             {
                //                 IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                //                 TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                //                 E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                //                 SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                //                 LastName = a.Field<string>("colLastNameVarchar"),
                //                 FirstName = a.Field<string>("colFirstNameVarchar"),

                //                 RouteFrom = a.Field<string>("RouteFrom"),
                //                 RouteTo = a.Field<string>("RouteTo"),

                //                 FlightNo = a.Field<string>("colFlightNoVarchar"),
                //                 Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                //                 Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                //                 DepartureDate = a.Field<string>("DepartureDatetime"),
                //                 ArrivalDate = a.Field<string>("ArrivalDatetime"),
                //                 RecLoc = a.Field<string>("colRecordLocatorVarchar"),

                //             }).ToList();

                //CancelledList = (from a in dtCancelled.AsEnumerable()
                //                 select new CrewAdminTransport
                //                 {
                //                     IDBigint = GlobalCode.Field2Int(a["colIdBigint"]),
                //                     TransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
                //                     E1ID = GlobalCode.Field2Int(a["colSeafarerIdInt"]),
                //                     SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                //                     LastName = a.Field<string>("colLastNameVarchar"),
                //                     FirstName = a.Field<string>("colFirstNameVarchar"),

                //                     RouteFrom = a.Field<string>("RouteFrom"),
                //                     RouteTo = a.Field<string>("RouteTo"),

                //                     FlightNo = a.Field<string>("colFlightNoVarchar"),
                //                     Departure = a.Field<string>("colDepartureAirportLocationCodeVarchar"),
                //                     Arrival = a.Field<string>("colArrivalAirportLocationCodeVarchar"),


                //                     DepartureDate = a.Field<string>("DepartureDatetime"),
                //                     ArrivalDate = a.Field<string>("ArrivalDatetime"),
                //                     RecLoc = a.Field<string>("colRecordLocatorVarchar"),

                //                 }).ToList();

                //EmailList = (from a in dtEmail.AsEnumerable()
                //             select new VehicleVendorEmail
                //             {
                //                 VehicleIDInt = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                //                 VehicleName = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),
                //                 VehicleEmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                //             }).ToList();

                //HttpContext.Current.Session["CrewAdminTranspo_AddedList"] = AddedList;
                //HttpContext.Current.Session["CrewAdminTranspo_CancelledList"] = CancelledList;
                //HttpContext.Current.Session["CrewAdminTranspo_VehicleEmailList"] = EmailList;


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
                if (dtEdited != null)
                {
                    dtEdited.Dispose();
                }                
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Descrption:     Save record to tblTempCrewAdminTransportationAddCancel to be able to tag all OFF to Need Vehicle
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsNeedVehicle(string UserID, bool IsNeedTransport)
        {           
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
           
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminTranspoTagALL");
                
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pIsNeedTransport", DbType.Boolean, IsNeedTransport);                

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
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Descrption:     Save record to tblTempCrewAdminTransportationAddCancel to be able to tag single OFF to "Need Vehicle"
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsNeedVehicleSingle(string UserID, bool IsNeedTransport, Int64 iTravelReqID, Int64 iIDBigint)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminTranspoTagAddCancel");

                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pIsNeedTransport", DbType.Boolean, IsNeedTransport);
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
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/May/2014
        /// Descrption:     Save record to tblTempCrewAdminPrintItinerary to be able to tag all to show itinerary
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsSelectedToPrintItinerary(string UserID, bool IsSelected)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminPrintItineraryALL");

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
        /// Author:         Josephine Gad
        /// Date Created:   29/May/2014
        /// Descrption:     Save record to tblTempCrewAdminPrintItinerary to be able to tag specific record to show itinerary
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsSelectedToPrintItinerarySingle(string UserID, bool IsSelected, Int64 iTravelReqID, Int64 iIDBigint)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCrewAdminPrintItinerary");

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
    }
}

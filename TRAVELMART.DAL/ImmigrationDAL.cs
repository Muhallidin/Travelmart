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
    public class ImmigrationDAL
    {
        public void LoadImmigrationPage(Int16 LoadType, DateTime FromDate, DateTime ToDate,
           string UserID, string Role, string OrderBy, int SeaportID, string FilterByName,
           string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
           Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo, int StartRow, int MaxRow)
        {
            List<ImmigrationManifestList> listImmigration = new List<ImmigrationManifestList>();
            HttpContext.Current.Session["Immigration_Manifest"] = listImmigration;
            HttpContext.Current.Session["Immigration_ManifestCount"] = 0;

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtImmigration = null;
            DataTable dtNationality = null;
            DataTable dtGender = null;
            DataTable dtRank = null;
            DataTable dtCount = null;
            DataTable dtSeaport = null;
            DataTable dtRoute = null;

            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectImmigrationPage");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);
                db.AddInParameter(dbCommand, "@pSeaportID", DbType.Int32, SeaportID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, GlobalCode.Field2TinyInt(FilterByName));
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);
                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, GlobalCode.Field2Int(NationalityID));
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, GlobalCode.Field2Int(Gender));
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, GlobalCode.Field2Int(RankID));
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);
                db.AddInParameter(dbCommand, "@pShowLegInt", DbType.Int16, iAirLeg);
                db.AddInParameter(dbCommand, "@pRouteIDFrom", DbType.Int16, iRouteFrom);
                db.AddInParameter(dbCommand, "@pRouteIDTo", DbType.Int16, iRouteTo);

                dbCommand.CommandTimeout = 60;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    HttpContext.Current.Session["Immigration_ManifestCount"] = maxRows;
                }
                dtImmigration = ds.Tables[1];

                //if (dtCrewAdmin.Rows.Count > 0)
                //{
                //    DataView dv = dtCrewAdmin.DefaultView;
                //    dv.Sort = OrderBy;
                //    dtCrewAdmin = dv.ToTable();
                //}

                //List<CrewAdminList> recordList = new List<CrewAdminList>();
                listImmigration = (from a in dtImmigration.AsEnumerable()
                              select new ImmigrationManifestList
                              {
                                  //IsManual = GlobalCode.Field2Bool(a["IsManual"]),
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

                                  Hotel = GlobalCode.Field2String(a["Hotel"]),
                                  IsMeetGreet = GlobalCode.Field2Bool(a["IsMeetGreet"]),
                                  IsPortAgent = GlobalCode.Field2Bool(a["IsPortAgent"]),
                                  IsHotelVendor = GlobalCode.Field2Bool(a["IsHotelVendor"]),
                                  Remarks = a.Field<string>("Remarks"),

                                  PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                                  PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                                  PassportExp = GlobalCode.Field2String(a["PassportExp"]),

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
                              }
                     ).ToList();

                if (LoadType == 0)
                {
                    List<SeaportDTO> listSeaport = new List<SeaportDTO>();
                    List<NationalityList> listNationality = new List<NationalityList>();
                    List<GenderList> listGender = new List<GenderList>();
                    List<RankList> listRank = new List<RankList>();
                    List<VehicleRoute> listRoute = new List<VehicleRoute>();

                    int iOnCount = 0;
                    int iOffCount = 0;

                    HttpContext.Current.Session["Immigration_Seaport"] = listSeaport;
                    HttpContext.Current.Session["Immigration_Nationality"] = listNationality;
                    HttpContext.Current.Session["Immigration_Gender"] = listGender;
                    HttpContext.Current.Session["Immigration_Rank"] = listRank;
                    HttpContext.Current.Session["Immigration_Route"] = listRoute;

                    HttpContext.Current.Session["Immigration_OnCount"] = iOnCount;
                    HttpContext.Current.Session["Immigration_OffCount"] = iOffCount;

                        

                    dtSeaport = ds.Tables[2];
                    listSeaport = (from a in dtSeaport.AsEnumerable()
                                  select new SeaportDTO
                                  {
                                      SeaportIDString = GlobalCode.Field2String(a["colPortIdInt"]),
                                      SeaportNameString = GlobalCode.Field2String(a["PortName"]),
                                  }).ToList();
                    HttpContext.Current.Session["Immigration_Seaport"] = listSeaport;
                   

                    dtNationality = ds.Tables[3];
                    dtGender = ds.Tables[4];
                    dtRank = ds.Tables[5];
                    dtCount = ds.Tables[6];
                    dtRoute = ds.Tables[7];


                    listNationality = (from a in dtNationality.AsEnumerable()
                                       select new NationalityList
                                       {
                                           NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                                           Nationality = GlobalCode.Field2String(a["Nationality"])
                                       }).ToList();

                    HttpContext.Current.Session["Immigration_Nationality"] = listNationality;

                    listGender = (from a in dtGender.AsEnumerable()
                                  select new GenderList
                                  {
                                      GenderID = GlobalCode.Field2Int(a["GenderID"]),
                                      Gender = GlobalCode.Field2String(a["Gender"])
                                  }).ToList();
                    HttpContext.Current.Session["Immigration_Gender"] = listGender;

                    listRank = (from a in dtRank.AsEnumerable()
                                select new RankList
                                {
                                    RankID = GlobalCode.Field2Int(a["RankID"]),
                                    Rank = GlobalCode.Field2String(a["Rank"])
                                }).ToList();
                    HttpContext.Current.Session["Immigration_Rank"] = listRank;



                    //int iOnCount = 0;
                    //int iOffCount = 0;

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
                   

                    HttpContext.Current.Session["Immigration_OnCount"] = iOnCount;
                    HttpContext.Current.Session["Immigration_OffCount"] = iOffCount;

                    listRoute = (from a in dtRoute.AsEnumerable()
                                 select new VehicleRoute
                                 {
                                     RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                     RouteDesc = GlobalCode.Field2String(a["colRouteNameVarchar"]),
                                 }).ToList();
                    HttpContext.Current.Session["Immigration_Route"] = listRoute;
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

                    //CrewAdminTables.Add(new CrewAdminGenericClass()
                    //{
                    //    CrewAdminList = recordList,
                    //    CrewAdminListCount = maxRows,
                    //    OnCount = iOnCount,
                    //    OffCount = iOffCount
                    //});

                    HttpContext.Current.Session["Immigration_OnCount"] = iOnCount;
                    HttpContext.Current.Session["Immigration_OffCount"] = iOffCount;

                }

                HttpContext.Current.Session["Immigration_Manifest"] = listImmigration;
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
                if (dtImmigration != null)
                {
                    dtImmigration.Dispose();
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
                if (dtSeaport != null)
                {
                    dtSeaport.Dispose();
                }
                if (dtRoute != null)
                {
                    dtRoute.Dispose();
                }
            }
        }


        public List<CrewImmigration> CrewImmigration(short LoadType, long SeafarerID, long TravelReqID, string LOEControlNumber, string UserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataSet ds = new DataSet();
            try
            {
                List<CrewImmigration> immigration = new List<CrewImmigration>();
                dbCommand = db.GetStoredProcCommand("uspGetCrewVerificationInformation");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.Int64, SeafarerID );
                db.AddInParameter(dbCommand, "@pTravelReqID", DbType.Int64, TravelReqID);
                db.AddInParameter(dbCommand, "@pLOEControlNumber", DbType.String, LOEControlNumber);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID); 
                dbCommand.CommandTimeout = 0;
                return ProcessCrewImmigration(db.ExecuteDataSet(dbCommand));
                 
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
                if (db != null)
                {
                    ds.Dispose();
                }
            }


        }

        private List<CrewImmigration> ProcessCrewImmigration(DataSet ds)
        {

            List<CrewImmigration> immigration = new List<CrewImmigration>();
            try
            {
                string user_id = "", jde_id = "";
                DataRow dr;
                if (ds.Tables[7].Rows.Count > 0)
                {
                    dr = ds.Tables[7].Rows[0];
                    user_id = dr["user_id"].ToString();
                    jde_id = dr["jde_id"].ToString();
                }

                immigration = (from a in ds.Tables[0].AsEnumerable()
                               select new CrewImmigration
                               {

                                   CrewVericationID = GlobalCode.Field2Long(a["colCrewVericationIDBigint"]),
                                   SeaparerID = GlobalCode.Field2Long(a["IDNumber"]),
                                   FirstName = GlobalCode.Field2String(a["FirstName"]),
                                   LastName = GlobalCode.Field2String(a["LastName"]),
                                   LOEControlNumber = GlobalCode.Field2String(a["LOEControlNumber"]),
                                   Nationality = GlobalCode.Field2String(a["NationalityName"]),
                                   ContactNo = GlobalCode.Field2String(a["ContactNo"]),
                                   EmailAdd = GlobalCode.Field2String(a["EmailAdd"]),
                                   PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                                   PassportExpiredate = GlobalCode.Field2DateTimeNull(a["ExpirationDate"]) == null ? null : GlobalCode.Field2DateTime(a["ExpirationDate"]).ToString("MM/dd/yyyy"),
                                   PassportIssuedate = GlobalCode.Field2String(a["IssueDate"]),
                                   Vessel = GlobalCode.Field2String(a["Ship"]),
                                   Brand = GlobalCode.Field2String(a["BrandName"]),
                                   SignOnDate = GlobalCode.Field2DateTime(a["SignOnDateAdj"]),
                                   Seaport = GlobalCode.Field2String(a["PortName"]),
                                   Rank = GlobalCode.Field2String(a["PositionName"]),
                                   NewHire = GlobalCode.Field2Bool(a["NewHire"]),
                                   Joindate = GlobalCode.Field2DateTime(a["SignOnDateAdj"]),
                                   JoinPort = GlobalCode.Field2String(a["SeaportCode"]),
                                   JoinCity = GlobalCode.Field2String(a["AirportCode"]),
                                   DateHired = GlobalCode.Field2DateTimeNull(a["DateHired"]),
                                   Reason = GlobalCode.Field2Int(a["colReasonInt"]),
                                   IsFraudulentDoc = GlobalCode.Field2Bool(a["colIsFraudulentDocBit"]),
                                   IsPriorImmigIssues = GlobalCode.Field2Bool(a["colIsPriorImmigIssuesBit"]),
                                   IsPriorConDep = GlobalCode.Field2Bool(a["colIsPriorConDepBit"]),
                                   IsOther = GlobalCode.Field2Bool(a["colIsOtherBit"]),
                                   OtherDetail = GlobalCode.Field2String(a["colOtherDetailVarchar"]),
                                   IsApproved = GlobalCode.Field2Bool(a["colIsApprovedBit"]),
                                   DateOfBirth = GlobalCode.Field2DateTimeNull(a["DateOfBirth"]),
                                   UserName = GlobalCode.Field2String(a["UserName"]),
                                   ProcessDate = GlobalCode.Field2DateTimeNull(a["ProcessDate"]),

                                   ImmigrationAirTransaction = (from n in ds.Tables[1].AsEnumerable()
                                                                select new ImmigrationAirTransaction
                                                                {
                                                                    SeqNo = GlobalCode.Field2Int(n["colSeqNoInt"]),
                                                                    AirLine = GlobalCode.Field2String(n["AirlineName"]),
                                                                    DepartureDateTime = GlobalCode.Field2DateTime(n["DepartureDateTime"]),
                                                                    ArrivalDateTime = GlobalCode.Field2DateTime(n["ArrivalDateTime"]),
                                                                    DepartureAirportLocationCode = GlobalCode.Field2String(n["DepartureAirportCode"]),
                                                                    ArrivalAirportLocationCode = GlobalCode.Field2String(n["ArrivalAirportCode"]),
                                                                }).ToList(),


                                   ImmigrationHotelBooking = (from i in ds.Tables[2].AsEnumerable()
                                                              select new ImmigrationHotelBooking
                                                              {
                                                                  BranchName = GlobalCode.Field2String(i["BranchName"]),
                                                                  TimeSpanStartDate = GlobalCode.Field2DateTime(i["CheckInDate"]),
                                                                  TimeSpanStartTime = GlobalCode.Field2DateTime(i["CheckInTime"]),
                                                                  TimeSpanEndDate = GlobalCode.Field2DateTime(i["CheckOutDate"]),
                                                                  TimeSpanEndTime = GlobalCode.Field2DateTime(i["CheckOutTime"]),
                                                                  TimeSpanDurationInt = GlobalCode.Field2Int(i["TimeSpanDuration"]),
                                                                  RoomType = GlobalCode.Field2String(i["RoomType"]),
                                                                  ForeColor = GlobalCode.Field2String(i["coldForeColorVarchar"]),
                                                                  ColorCode = GlobalCode.Field2String(i["colColorCodevarchar"]),
                                                              }).ToList(),

                                   ImmigrationTransportion = (from e in ds.Tables[3].AsEnumerable()
                                                              select new ImmigrationTransportion
                                                              {
                                                                  VehicleVendorName = GlobalCode.Field2String(e["Transportation"]),
                                                                  RouteFrom = GlobalCode.Field2String(e["RouteFrom"]),
                                                                  RouteTo = GlobalCode.Field2String(e["RouteTo"]),
                                                                  PickUpDate = GlobalCode.Field2DateTime(e["colPickUpDate"]),
                                                                  PickUpTime = GlobalCode.Field2DateTime(e["colPickUpTime"]),
                                                                  ForeColor = GlobalCode.Field2String(e["coldForeColorVarchar"]),
                                                                  ColorCode = GlobalCode.Field2String(e["colColorCodevarchar"]),
                                                              }).ToList(),

                                   ImmigrationEmploymentHistory = (from b in ds.Tables[4].AsEnumerable()
                                                                   select new ImmigrationEmploymentHistory
                                                                   {
                                                                       CrewVericationID = GlobalCode.Field2Long(b["colCrewVericationIDBigint"]),
                                                                       SeaparerID = GlobalCode.Field2Long(b["IDNumber"]),
                                                                       FirstName = GlobalCode.Field2String(b["FirstName"]),
                                                                       LastName = GlobalCode.Field2String(b["LastName"]),
                                                                       LOEControlNumber = GlobalCode.Field2String(b["LOEControlNumber"]),
                                                                       Nationality = GlobalCode.Field2String(b["NationalityName"]),
                                                                       ContactNo = GlobalCode.Field2String(b["ContactNo"]),
                                                                       EmailAdd = GlobalCode.Field2String(b["EmailAdd"]),
                                                                       PassportNo = GlobalCode.Field2String(b["PassportNo"]),
                                                                       PassportExpiredate = GlobalCode.Field2DateTime(b["ExpirationDate"]).ToString("MM/dd/yyyy"),
                                                                       PassportIssuedate = GlobalCode.Field2String(b["IssueDate"]),
                                                                       Vessel = GlobalCode.Field2String(b["Ship"]),
                                                                       Brand = GlobalCode.Field2String(b["BrandName"]),
                                                                       SignOnDate = GlobalCode.Field2DateTime(b["SignOnDate"]),
                                                                       Seaport = GlobalCode.Field2String(b["PortName"]),
                                                                       NewHire = GlobalCode.Field2Bool(b["NewHire"]),
                                                                       Joindate = GlobalCode.Field2DateTime(b["SignOnDate"]),
                                                                       JoinPort = GlobalCode.Field2String(b["SeaportCode"]),
                                                                       JoinCity = GlobalCode.Field2String(b["AirportCode"]),
                                                                       DateHired = GlobalCode.Field2DateTime(b["DateHired"]),
                                                                       Reason = GlobalCode.Field2Int(b["colReasonInt"]),
                                                                       IsFraudulentDoc = GlobalCode.Field2Bool(b["colIsFraudulentDocBit"]),
                                                                       IsPriorImmigIssues = GlobalCode.Field2Bool(b["colIsPriorImmigIssuesBit"]),
                                                                       IsPriorConDep = GlobalCode.Field2Bool(b["colIsPriorConDepBit"]),
                                                                       IsOther = GlobalCode.Field2Bool(b["colIsOtherBit"]),
                                                                       OtherDetail = GlobalCode.Field2String(b["colOtherDetailVarchar"]),
                                                                       IsApproved = GlobalCode.Field2Bool(b["colIsApprovedBit"]),
                                                                       DateOfBirth = GlobalCode.Field2DateTime(b["DateOfBirth"]),
                                                                       ShipID = GlobalCode.Field2Int(b["ShipID"]),
                                                                       Ship = GlobalCode.Field2String(b["Ship"]),
                                                                       RankID = GlobalCode.Field2Int(b["RankID"]),
                                                                       Rank = GlobalCode.Field2String(b["PositionName"]),
                                                                       ColorCode = GlobalCode.Field2String(b["ColorCode"]),
                                                                       ForeColor = GlobalCode.Field2String(b["ForeColor"]),
                                                                   }).ToList(),

                                   SeafarerImage = (from k in ds.Tables[5].AsEnumerable()
                                                    where GlobalCode.Field2Long(k["SeafarerID"]) == GlobalCode.Field2Long(a["IDNumber"])
                                                    select new SeafarerImage
                                                    {
                                                        SeaparerID = GlobalCode.Field2Int(k["SeafarerID"]),
                                                        Image = GlobalCode.Field2PictureByte(k["PictureImage"]),
                                                        ImageType = GlobalCode.Field2String(k["PictureType"]),
                                                    }).ToList(),

                                   Parent = (from p in ds.Tables[6].AsEnumerable()
                                             select new EmployeeParent
                                             {
                                                 EmployeeID = GlobalCode.Field2Long(p["EmployeeID"]),
                                                 FatherName = GlobalCode.Field2String(p["FatherName"]),
                                                 MotherName = GlobalCode.Field2String(p["MotherName"]),
                                             }).ToList(),

                                   CtracDetail = new CtracDetail { user_id = user_id, jde_id = jde_id }

                               }).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }

            return immigration;
            
        }


        public  List<CrewImmigration> InsertCrewImmigration(List<CrewImmigration> crewVerification) 
        {
        
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataSet ds = new DataSet();
            try
            {
 
                dbCommand = db.GetStoredProcCommand("uspInsertCrewVerification");

                db.AddInParameter(dbCommand, "@pCrewVericationIDBigint", DbType.Int64, crewVerification[0].CrewVericationID);// 	bigint
                db.AddInParameter(dbCommand, "@pSeafarerIDBigint", DbType.Int64,crewVerification[0].SeaparerID);// 			Bigint
                db.AddInParameter(dbCommand, "@pTravelReqIDbigint", DbType.Int64,crewVerification[0].TravelReqID);// 		bigint
                db.AddInParameter(dbCommand, "@pRecordlocatorVarchar", DbType.String ,crewVerification[0].RecordLocator);// 		varchar(10)
                db.AddInParameter(dbCommand, "@pItinerarySeqNoInt", DbType.Int32,crewVerification[0].ItinerarySeqNo);// 		int
                db.AddInParameter(dbCommand, "@pLOEControlNoVarchar", DbType.String,crewVerification[0].LOEControlNumber);// 		varchar(50)
                db.AddInParameter(dbCommand, "@pVesselIDInt", DbType.Int32,crewVerification[0].VesselID);// 				int
                db.AddInParameter(dbCommand, "@pRankIDInt", DbType.Int32,crewVerification[0].RankID);// 				int
                db.AddInParameter(dbCommand, "@pJoindateDateTime", DbType.DateTime, crewVerification[0].Joindate) ;// 			datetime
                db.AddInParameter(dbCommand, "@pJoinPortVarchar", DbType.String, crewVerification[0].JoinPort);// 			varchar(5)
                db.AddInParameter(dbCommand, "@pJoinCityVarchar", DbType.String, crewVerification[0].JoinCity);// 			varchar(5)
                db.AddInParameter(dbCommand, "@pReasonInt", DbType.Int32, crewVerification[0].Reason);// 				varchar(50)
                db.AddInParameter(dbCommand, "@pIsFraudulentDocBit", DbType.Boolean, crewVerification[0].IsFraudulentDoc);// 		bit	
                db.AddInParameter(dbCommand, "@pIsPriorImmigIssuesBit", DbType.Boolean, crewVerification[0].IsPriorImmigIssues);// 	bit
                db.AddInParameter(dbCommand, "@pIsPriorConDepBit", DbType.Boolean, crewVerification[0].IsPriorConDep );// 			bit
                db.AddInParameter(dbCommand, "@pIsOtherBit", DbType.Boolean, crewVerification[0].IsOther );// 				bit
                db.AddInParameter(dbCommand, "@pIsApprovedBit", DbType.Boolean, crewVerification[0].IsApproved);// 			bit
                db.AddInParameter(dbCommand, "@pOtherDetailVarchar", DbType.String, crewVerification[0].OtherDetail );// 		varchar(max)
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, crewVerification[0].UserID);// 			varchar(50)			

                dbCommand.CommandTimeout = 60;
                return ProcessCrewImmigration(db.ExecuteDataSet(dbCommand));
                 
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
                if (db != null)
                {
                    ds.Dispose();
                }
            }
        
        
        }


        public void InsertQRCode(List<SeafarerImage> SeafarerImage)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {

                DataTable dt = new DataTable(); 
                GlobalCode gc = new GlobalCode();

                dt = gc.getDataTable(SeafarerImage);

                SFDbCommand =  SFDatebase.GetStoredProcCommand("uspInsertCrewMemberQRCode");

                SqlParameter param = new SqlParameter("@pCrewMemberQRCode", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SFDbCommand.Parameters.Add(param);
                SFDatebase.ExecuteNonQuery(SFDbCommand);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }

            }

           

        }

    }
}

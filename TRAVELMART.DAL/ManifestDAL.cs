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
    public class ManifestDAL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/02/2012
        /// Descrption:     send all manifest queries to list
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
        /// <param name="FilterByName"></param>
        /// <param name="SeafarerID"></param>
        /// <param name="NationalityID"></param>
        /// <param name="Gender"></param>
        /// <param name="RankID"></param>
        /// <param name="Status"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <returns></returns>
        public List<ManifestDTOGenericClass> LoadAllManifestTables(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID, string ViewNoHotelOnly)
        {
            List<ManifestDTOGenericClass> ManifestTables = new List<ManifestDTOGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
           
            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectTravelReqManifestTable");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pFromDate", DbType.DateTime, FromDate);
                db.AddInParameter(dbCommand, "@pToDate", DbType.DateTime, ToDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pFilterByName", DbType.Int16, FilterByName);
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                db.AddInParameter(dbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                db.AddInParameter(dbCommand, "@pGender", DbType.Int32, Gender);
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, RankID);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.Int32, HotelID);

                db.AddInParameter(dbCommand, "@pViewNoHotelOnly", DbType.Int16, GlobalCode.Field2TinyInt(ViewNoHotelOnly)); 

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

                ManifestTables.Add(new ManifestDTOGenericClass()
                {
                    ManifestList = (from a in dtManifest.AsEnumerable()
                                    select new ManifestList
                                      {
                                          IsManual = a.Field<string>("IsManual"),
                                          IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                          E1TravelRequest = GlobalCode.Field2Int(a["E1TravelRequest"]),
                                          RequestID = GlobalCode.Field2Int(a["RequestID"]),
                                          TravelRequestID = GlobalCode.Field2Int(a["TravelRequestID"]),
                                          RecLoc = a.Field<string>("RecLoc"),
                                          SfID = GlobalCode.Field2Int(a["SfID"]),
                                          Name = a.Field<string>("Name"),
                                          DateOnOff = a.Field<DateTime?>("DateOnOff"),
                                          DateArrivalDeparture = a.Field<DateTime?>("DateArrivalDeparture"),
                                          Status = a.Field<string>("Status"),

                                          Brand = a.Field<string>("Brand"),
                                          Vessel = a.Field<string>("Vessel"),

                                          PortCode = a.Field<string>("PortCode"),
                                          Port = a.Field<string>("Port"),

                                          RankCode = a.Field<string>("RankCode"),
                                          Rank = a.Field<string>("Rank"),

                                          AirStatus = a.Field<string>("AirStatus"),
                                          colAirStatusVarchar = a.Field<string>("colAirStatusVarchar"),

                                          HotelStatus = a.Field<string>("HotelStatus"),
                                          colHotelStatusVarchar = a.Field<string>("colHotelStatusVarchar"),

                                          VehicleStatus = a.Field<string>("VehicleStatus"),
                                          colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                          ReasonCode = a.Field<string>("ReasonCode"),
                                          IsWithSail = GlobalCode.Field2Bool(a["IsWithSail"]),

                                      }).ToList(),
                    ManifestListCount = maxRows,
                });

                return ManifestTables;
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }                
            }
        }

        /// <summary>
        /// Author:         Gelo Oquialda
        /// Date Created:   08/02/2012
        /// Descrption:     send all manifest search view queries to list
        /// =============================================================
        /// Author:         Marco Abejar
        /// Date Created:   25/03/2013
        /// Descrption:     Search filter base on user role
        /// =============================================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  25/Jun/2013
        /// Descrption:     Change uspSelectTravelReqManifestTable_SearchView to uspSelectTravelReqManifestTable_SearchManifest
        ///                 Add IsAddRequestVisible and IsVisible fields
        ///                 Add IsShowAll to show past dated records
        /// =============================================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  07/Jan/2015
        /// Descrption:     Add IsVehicleVendor field
        /// =============================================================
        /// </summary>
        public List<ManifestSearchViewDTOGenericClass> LoadAllManifestSearchViewTables(Int16 LoadType, DateTime CurrentDate, string UserID,
            string Role, string OrderBy, int StartRow, int MaxRow, string SeafarerID, string SeafarerLN, string SeafarerFN,
            string RecordLocator, string VesselCode, string VesselName, int RegionID, int CountryID, int CityID, int PortID, 
            int HotelID, bool IsShowAll)
        {
            List<ManifestSearchViewDTOGenericClass> ManifestSearchViewTables = new List<ManifestSearchViewDTOGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {

                if (Role == TravelMartVariable.RoleHotelVendor 
                    || Role == TravelMartVariable.RoleVehicleVendor 
                    || Role == TravelMartVariable.RolePortSpecialist)
                {
                    //dbCommand = db.GetStoredProcCommand("uspSelectTravelReqManifestTable_SearchView_ByUserBranch");
                    dbCommand = db.GetStoredProcCommand("uspSelectTravelReqManifestTable_SearchManifest_ByVendor");
                }
                else
                {
                    //dbCommand = db.GetStoredProcCommand("uspSelectTravelReqManifestTable_SearchView");
                    dbCommand = db.GetStoredProcCommand("uspSelectTravelReqManifestTable_SearchManifest");
                }
                dbCommand.CommandTimeout = 0;

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pCurrentDate", DbType.DateTime, CurrentDate);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, Role);
                db.AddInParameter(dbCommand, "@pOrderBy", DbType.String, OrderBy);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);
                                
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.String, SeafarerID);
                db.AddInParameter(dbCommand, "@pSeafarerLN", DbType.String, SeafarerLN);
                db.AddInParameter(dbCommand, "@pSeafarerFN", DbType.String, SeafarerFN);
                db.AddInParameter(dbCommand, "@pRecordLocator", DbType.String, RecordLocator);

                db.AddInParameter(dbCommand, "@pVesselCode", DbType.String, VesselCode);
                db.AddInParameter(dbCommand, "@pVesselName", DbType.String, VesselName);
                                
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, CityID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.Int32, HotelID);

                db.AddInParameter(dbCommand, "@pShowAll", DbType.Boolean, IsShowAll);
                
                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];

                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

                ManifestSearchViewTables.Add(new ManifestSearchViewDTOGenericClass()
                {
                    ManifestSearchViewList = (from a in dtManifest.AsEnumerable()
                                    select new ManifestSearchViewList
                                    {
                                        IsAddRequestVisible = GlobalCode.Field2Bool(a["IsAddRequestVisible"]),
                                        IsVisible = GlobalCode.Field2TinyInt(a["VisibilityCount"]) == 1 ? true:false,
                                        IsManual = a.Field<string>("IsManual"),
                                        IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                        E1TravelRequest = GlobalCode.Field2Int(a["E1TravelRequest"]),
                                        RequestID = GlobalCode.Field2Int(a["RequestID"]),
                                        TravelRequestID = GlobalCode.Field2Int(a["TravelRequestID"]),
                                        RecLoc = a.Field<string>("RecLoc"),
                                        SfID = GlobalCode.Field2Int(a["SfID"]),
                                        Name = a.Field<string>("Name"),
                                        DateOnOff = a.Field<DateTime?>("DateOnOff"),
                                        //DateArrivalDeparture = a.Field<DateTime?>("DateArrivalDeparture"),
                                        Status = a.Field<string>("Status"),

                                        Brand = a.Field<string>("Brand"),
                                        Vessel = a.Field<string>("Vessel"),

                                        PortCode = a.Field<string>("PortCode"),
                                        Port = a.Field<string>("Port"),

                                        RankCode = a.Field<string>("RankCode"),
                                        Rank = a.Field<string>("Rank"),

                                        AirStatus = a.Field<string>("AirStatus"),
                                        colAirStatusVarchar = a.Field<string>("colAirStatusVarchar"),

                                        HotelStatus = a.Field<string>("HotelStatus"),
                                        colHotelStatusVarchar = a.Field<string>("colHotelStatusVarchar"),

                                        VehicleStatus = a.Field<string>("VehicleStatus"),
                                        colVehicleStatusVarchar = a.Field<string>("colVehicleStatusVarchar"),

                                        ReasonCode = a.Field<string>("ReasonCode"),
                                        IsWithSail = GlobalCode.Field2Bool(a["IsWithSail"]),

                                        OriginDestination = a.Field<string>("OriginDestination"),
                                        Remarks = a.Field<string>("Remarks"),
                                        RemarksURL = a.Field<string>("RemarksURL"),                                        
                                        RemarksParameter = a.Field<string>("RemarksParameter"),

                                        IsMeetGreet = GlobalCode.Field2String(a["IsMeetGreet"]),
                                        IsPortAgent = GlobalCode.Field2String(a["IsPortAgent"]),
                                        IsHotelVendor = GlobalCode.Field2String(a["IsHotelVendor"]),
                                        IsVehicleVendor = GlobalCode.Field2String(a["IsVehicleVendor"]),

                                        MGTagDate = GlobalCode.Field2String(a["MeetGreetTagDate"]),
                                        PATagDate = GlobalCode.Field2String(a["PortAgentTagDate"]),
                                        HVTagDate = GlobalCode.Field2String(a["HotelVendorTagDate"]),
                                        VVTagDate = GlobalCode.Field2String(a["VehicleVendorTageDate"]),

                                    }).ToList(),
                    ManifestSearchViewListCount = maxRows,
                    //IsAddRequestVisible = GlobalCode.Field2Bool(dtAddRequest.Rows[0][0])
                });

                return ManifestSearchViewTables;
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }                
            }
        }
        /// <summary>
        /// Author:          Josephine Gad
        /// Date Created:    22/02/2012
        /// Descrption:      Get tentative manifest list and total row count
        /// -----------------------------------------------------------------
        /// Modified By:     Josephine Gad
        /// Date Modified:   11/Feb/2013
        /// Descrption:      Change Name to LastName and FirstName
        /// </summary>
        /// <param name="DateFromString"></param>
        /// <param name="NameString"></param>
        /// <param name="strUser"></param>
        /// <param name="DateFilter"></param>
        /// <param name="ByNameOrID"></param>
        /// <param name="filterNameOrID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="Status"></param>
        /// <param name="Region"></param>
        /// <param name="Country"></param>
        /// <param name="City"></param>
        /// <param name="Port"></param>
        /// <param name="Hotel"></param>
        /// <param name="Vessel"></param>
        /// <param name="UserRole"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <param name="LoadType"></param>
        /// <returns></returns>
        public List<TentativeManifestGenericClass> GetTentativeManifestList(string DateFromString,
           string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
            int StartRow, int MaxRow, Int16 LoadType)
        {
            List<TentativeManifestGenericClass> TentativeManifest = new List<TentativeManifestGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectHotelManifestTentative");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));                                                
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);
                
                db.AddInParameter(dbCommand, "@pFilterByDate", DbType.String, DateFilter);
                db.AddInParameter(dbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                db.AddInParameter(dbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
                db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
                db.AddInParameter(dbCommand, "@pRank", DbType.String, Rank);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.String, Region);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.String, Country);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.String, City);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.String, Port);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.String, Hotel);
                db.AddInParameter(dbCommand, "@pVesselIDInt", DbType.String, Vessel);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, MaxRow);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, LoadType);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

                TentativeManifest.Add(new TentativeManifestGenericClass()
                {
                    TentativeManifestList = (from a in dtManifest.AsEnumerable()
                                              select new TentativeManifest
                                              {
                                                    IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                                    colTravelReqIdInt  = GlobalCode.Field2Int(a["colTravelReqIdInt"]),
                                                    RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                                    SfID = GlobalCode.Field2Int(a["SfID"]),
                                                    LastName = GlobalCode.Field2String(a["LastName"]),
                                                    FirstName = GlobalCode.Field2String(a["FirstName"]),
                                                    
                                                    colPassportNoVarchar = GlobalCode.Field2String(a["colPassportNoVarchar"]),
                                                    colPassportExpirationDate = a.Field<string>("colPassportExpirationDate"),  
                                                    colPassportIssuedDate = GlobalCode.Field2String(a["colPassportIssuedDate"]),

                                                    Status = a.Field<string>("Status"),
                                                    RequestID  = GlobalCode.Field2Int(a["RequestID"]),
                                                    Vessel = a.Field<string>("Vessel"),
                                                    colVesselCodeVarchar = a.Field<string>("colVesselCodeVarchar"),

                                                    Rank = a.Field<string>("Rank"),
                                                    CostCenter = a.Field<string>("CostCenter"),
                                                    HotelRequest  = a.Field<string>("HotelRequest"),
                                                    colHotelStatusVarchar = a.Field<string>("colHotelStatusVarchar"),

                                                    colTimeSpanStartDate =  a.Field<DateTime?>("colTimeSpanStartDate"),
                                                    colTimeSpanEndDate = a.Field<DateTime?>("colTimeSpanEndDate"),
                                                    colTimeSpanDurationInt = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),

                                                    colRoomNameVarchar = a.Field<string>("colRoomNameVarchar"),
                                                    colRoomNameID = GlobalCode.Field2Int(a["colRoomNameID"]),
                                                    
                                                    Gender = a.Field<string>("Gender"),
                                                    Nationality = a.Field<string>("Nationality"),
                                                    HotelCity = a.Field<string>("HotelCity"),
                                                    FromCity = a.Field<string>("FromCity"),
                                                    ToCity = a.Field<string>("ToCity"),

                                                    HotelBranch = a.Field<string>("HotelBranch"),
                                                    HotelBranchID =  GlobalCode.Field2Int(a["HotelBranchID"]),
                                                    HotelEmail =  a.Field<string>("HotelEmail"),

                                                    WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),

                                                    colArrivalDateTime = a.Field<DateTime?>("colArrivalDateTime"),
                                                    colDepartureDateTime = a.Field<DateTime?>("colDepartureDateTime"),
                                                    ArrivalTime = a.Field<TimeSpan?>("ArrivalTime"),
                                                    DepartureTime = a.Field<TimeSpan?>("DepartureTime"),

                                                    ArvlCity = a.Field<string>("ArvlCity"),
                                                    DeptCity = a.Field<string>("DeptCity"),

                                                    colFlightNoVarchar = a.Field<string>("colFlightNoVarchar"),
                                                    AL = a.Field<string>("AL"),
                                                    Carrier = a.Field<string>("Carrier"),

                                                    colPortIdInt = GlobalCode.Field2Int(a["colPortIdInt"]),
                                                    CountryID = GlobalCode.Field2Int(a["CountryID"]),
                                                    CityID = GlobalCode.Field2Int(a["CityID"]),

                                                    Couple =a.Field<int?>("Couple"),
                                                    SignOn = a.Field<DateTime?>("SignOn"),
                                                    Voucher = GlobalCode.Field2Decimal(a["Voucher"]),
                                                    TravelDate = a.Field<DateTime?>("TravelDate"),
                                                    Reason = a.Field<string>("Reason"),

                                                    colRemarksIDInt = GlobalCode.Field2Int(a["colRemarksIDInt"]),
                                                    colRemarksVarchar = a.Field<string>("colRemarksVarchar"),

                                                    E1TravelReqId = GlobalCode.Field2Int(a["E1TravelReqId"])
                                              }).ToList(),
                   TentativeManifestCount = maxRows,
                });

                return TentativeManifest;
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
                if (TentativeManifest != null)
                {
                    TentativeManifest = null;
                }
            }
        }
        /// <summary>
        /// Author:          Josephine Gad
        /// Date Created:    22/02/2012
        /// Descrption:      Get tentative manifest list and total row count
        /// -----------------------------------------------------------------
        /// Modified By:     Josephine Gad
        /// Date Modified:   11/Feb/2013
        /// Descrption:      Change Name to LastName and FirstName
        /// -----------------------------------------------------------------
        /// Modified By:     Muhallidin G Wali
        /// Date Modified:   11/Feb/2013
        /// Descrption:      Add parameter orderby for sorting
        /// </summary>
        /// <param name="DateFromString"></param>
        /// <param name="NameString"></param>
        /// <param name="strUser"></param>
        /// <param name="DateFilter"></param>
        /// <param name="ByNameOrID"></param>
        /// <param name="filterNameOrID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="Status"></param>
        /// <param name="Region"></param>
        /// <param name="Country"></param>
        /// <param name="City"></param>
        /// <param name="Port"></param>
        /// <param name="Hotel"></param>
        /// <param name="Vessel"></param>
        /// <param name="UserRole"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <param name="LoadType"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>

        public List<TentativeManifestGenericClass> GetTentativeManifestList2(string DateFromString,
          string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
          string Nationality, string Gender, string Rank, string Status,
          string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
           int StartRow, int MaxRow, Int16 LoadType, string orderBy)
        {
            List<TentativeManifestGenericClass> TentativeManifest = new List<TentativeManifestGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectHotelManifestTentative");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);

                db.AddInParameter(dbCommand, "@pFilterByDate", DbType.String, DateFilter);
                db.AddInParameter(dbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                db.AddInParameter(dbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
                db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
                db.AddInParameter(dbCommand, "@pRank", DbType.String, Rank);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.String, Region);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.String, Country);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.String, City);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.String, Port);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.String, Hotel);
                db.AddInParameter(dbCommand, "@pVesselIDInt", DbType.String, Vessel);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, MaxRow);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, LoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, orderBy);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

                TentativeManifest.Add(new TentativeManifestGenericClass()
                {
                    TentativeManifestList = (from a in dtManifest.AsEnumerable()
                                             select new TentativeManifest
                                             {
                                                 IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                                 colTravelReqIdInt = GlobalCode.Field2Int(a["colTravelReqIdInt"]),
                                                 RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                                 SfID = GlobalCode.Field2Int(a["SfID"]),
                                                 LastName = GlobalCode.Field2String(a["LastName"]),
                                                 FirstName = GlobalCode.Field2String(a["FirstName"]),

                                                 colPassportNoVarchar = GlobalCode.Field2String(a["colPassportNoVarchar"]),
                                                 colPassportExpirationDate = a.Field<string>("colPassportExpirationDate"),
                                                 colPassportIssuedDate = GlobalCode.Field2String(a["colPassportIssuedDate"]),

                                                 Status = a.Field<string>("Status"),
                                                 RequestID = GlobalCode.Field2Int(a["RequestID"]),
                                                 Vessel = a.Field<string>("Vessel"),
                                                 colVesselCodeVarchar = a.Field<string>("colVesselCodeVarchar"),

                                                 Rank = a.Field<string>("Rank"),
                                                 CostCenter = a.Field<string>("CostCenter"),
                                                 HotelRequest = a.Field<string>("HotelRequest"),
                                                 colHotelStatusVarchar = a.Field<string>("colHotelStatusVarchar"),

                                                 colTimeSpanStartDate = a.Field<DateTime?>("colTimeSpanStartDate"),
                                                 colTimeSpanEndDate = a.Field<DateTime?>("colTimeSpanEndDate"),
                                                 colTimeSpanDurationInt = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),

                                                 colRoomNameVarchar = a.Field<string>("colRoomNameVarchar"),
                                                 colRoomNameID = GlobalCode.Field2Int(a["colRoomNameID"]),

                                                 Gender = a.Field<string>("Gender"),
                                                 Nationality = a.Field<string>("Nationality"),
                                                 HotelCity = a.Field<string>("HotelCity"),
                                                 FromCity = a.Field<string>("FromCity"),
                                                 ToCity = a.Field<string>("ToCity"),

                                                 HotelBranch = a.Field<string>("HotelBranch"),
                                                 HotelBranchID = GlobalCode.Field2Int(a["HotelBranchID"]),
                                                 HotelEmail = a.Field<string>("HotelEmail"),

                                                 WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),

                                                 colArrivalDateTime = a.Field<DateTime?>("colArrivalDateTime"),
                                                 colDepartureDateTime = a.Field<DateTime?>("colDepartureDateTime"),
                                                 ArrivalTime = a.Field<TimeSpan?>("ArrivalTime"),
                                                 DepartureTime = a.Field<TimeSpan?>("DepartureTime"),

                                                 ArvlCity = a.Field<string>("ArvlCity"),
                                                 DeptCity = a.Field<string>("DeptCity"),

                                                 colFlightNoVarchar = a.Field<string>("colFlightNoVarchar"),
                                                 AL = a.Field<string>("AL"),
                                                 Carrier = a.Field<string>("Carrier"),

                                                 colPortIdInt = GlobalCode.Field2Int(a["colPortIdInt"]),
                                                 CountryID = GlobalCode.Field2Int(a["CountryID"]),
                                                 CityID = GlobalCode.Field2Int(a["CityID"]),

                                                 Couple = a.Field<int?>("Couple"),
                                                 SignOn = a.Field<DateTime?>("SignOn"),
                                                 Voucher = GlobalCode.Field2Decimal(a["Voucher"]),
                                                 TravelDate = a.Field<DateTime?>("TravelDate"),
                                                 Reason = a.Field<string>("Reason"),

                                                 colRemarksIDInt = GlobalCode.Field2Int(a["colRemarksIDInt"]),
                                                 colRemarksVarchar = a.Field<string>("colRemarksVarchar"),

                                                 E1TravelReqId = GlobalCode.Field2Int(a["E1TravelReqId"])
                                             }).ToList(),
                    TentativeManifestCount = maxRows,
                });

                return TentativeManifest;
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
                if (TentativeManifest != null)
                {
                    TentativeManifest = null;
                }
            }
        }


        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 17/04/2012
        /// Description: get tentative manifest export list
        /// -----------------------------------------------     
        /// Modified By:    Josephine Gad
        /// Date Modified:  19/07/2012
        /// Description:    Get required columns only, re-order column
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="userId"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<TentativeManifestExport> GetTentativeExportList(DateTime dateFrom, string userId, int BranchId)
        {
            DataTable dt = null;            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            List<TentativeManifestExport> TentativeList = new List<TentativeManifestExport>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectHotelManifestTentativeExport");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.Date, dateFrom);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, userId);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.Int32, BranchId);

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                TentativeList = (from a in dt.AsEnumerable()
                                     select new TentativeManifestExport 
                                     {
                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime>("CheckIn"),
                                         CheckOut = a.Field<DateTime>("CheckOut"),
                                         HotelNights = a.Field<Int16>("HotelNights"),
                                         ReasonCode = a.Field<string>("ReasonCode"),
                                         LastName = a.Field<string>("LastName"),
                                         FirstName = a.Field<string>("FirstName"),

                                         EmployeeId = a.Field<Int64>("EmployeeId"),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("SingleDouble"),
                                         Couple = GlobalCode.Field2String(a["couple"]),
                                         Title = a.Field<string>("Rank"),
                                         Ship = a.Field<string>("Ship"),
                                         CostCenter = a.Field<string>("CostCenter"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = a.Field<string>("RecLoc"),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<string>("DeptDate"),
                                         ArvlDate = a.Field<string>("ArvlDate"),
                                         DeptTime = a.Field<string>("DeptTime"),
                                         ArvlTime = a.Field<string>("ArvlTime"),
                                         Carrier = a.Field<string>("AL"),
                                         FlightNo = a.Field<string>("FlightNo"),
                                         Voucher = a.Field<decimal>("Voucher"),
                                        
                                         PassportNo = a.Field<string>("PassportNo"),
                                         PassportExpiration = a.Field<string>("PassportExpiration"),
                                         PasportDateIssued = a.Field<string>("PassportIssued"),

                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         Birthdate =  a.Field<string>("Birthdate")                                                                              
                                     }).ToList();

                return TentativeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/04/2012
        /// Description: Export ALL
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static DataSet getAllDataFiles(DateTime selectedDate, string UserId)
        {
            DataSet ds = null;
            Database db = DatabaseFactory.CreateDatabase("TRAVELMARTConnectionString");
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("spExportAll");
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, selectedDate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
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
        /// Author:         Josephine Gad
        /// Date            10/10/2012
        /// Description:    Get Forecast Manifest
        /// </summary>
        /// <returns></returns>
        public List<TentativeManifestGenericClass> GetForecastManifestList(string DateFromString,
           string DateToString,
           string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
            int StartRow, int MaxRow, Int16 LoadType)
        {
            List<TentativeManifestGenericClass> TentativeManifest = new List<TentativeManifestGenericClass>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            DataTable dtManifest = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectHotelManifestForecast");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));
                db.AddInParameter(dbCommand, "@pSFDateTo", DbType.DateTime, GlobalCode.Field2DateTime(DateToString));
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);

                db.AddInParameter(dbCommand, "@pFilterByDate", DbType.String, DateFilter);
                db.AddInParameter(dbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                db.AddInParameter(dbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
                db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
                db.AddInParameter(dbCommand, "@pRank", DbType.String, Rank);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.String, Region);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.String, Country);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.String, City);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.String, Port);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.String, Hotel);
                db.AddInParameter(dbCommand, "@pVesselIDInt", DbType.String, Vessel);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, MaxRow);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, LoadType);

                ds = db.ExecuteDataSet(dbCommand);
                dtManifest = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

                TentativeManifest.Add(new TentativeManifestGenericClass()
                {
                    TentativeManifestList = (from a in dtManifest.AsEnumerable()
                                             select new TentativeManifest
                                             {
                                                 IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                                 colTravelReqIdInt = GlobalCode.Field2Int(a["colTravelReqIdInt"]),
                                                 RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                                 SfID = GlobalCode.Field2Int(a["SfID"]),
                                                 LastName = GlobalCode.Field2String(a["LastName"]),
                                                 FirstName = GlobalCode.Field2String(a["FirstName"]),

                                                 colPassportNoVarchar = GlobalCode.Field2String(a["colPassportNoVarchar"]),
                                                 colPassportExpirationDate = a.Field<string>("colPassportExpirationDate"),
                                                 colPassportIssuedDate = GlobalCode.Field2String(a["colPassportIssuedDate"]),

                                                 Status = a.Field<string>("Status"),
                                                 RequestID = GlobalCode.Field2Int(a["RequestID"]),
                                                 Vessel = a.Field<string>("Vessel"),
                                                 colVesselCodeVarchar = a.Field<string>("colVesselCodeVarchar"),

                                                 Rank = a.Field<string>("Rank"),
                                                 CostCenter = a.Field<string>("CostCenter"),
                                                 HotelRequest = a.Field<string>("HotelRequest"),
                                                 colHotelStatusVarchar = a.Field<string>("colHotelStatusVarchar"),

                                                 colTimeSpanStartDate = a.Field<DateTime?>("colTimeSpanStartDate"),
                                                 colTimeSpanEndDate = a.Field<DateTime?>("colTimeSpanEndDate"),
                                                 colTimeSpanDurationInt = GlobalCode.Field2TinyInt(a["colTimeSpanDurationInt"]),

                                                 colRoomNameVarchar = a.Field<string>("colRoomNameVarchar"),
                                                 colRoomNameID = GlobalCode.Field2Int(a["colRoomNameID"]),

                                                 Gender = a.Field<string>("Gender"),
                                                 Nationality = a.Field<string>("Nationality"),
                                                 HotelCity = a.Field<string>("HotelCity"),
                                                 FromCity = a.Field<string>("FromCity"),
                                                 ToCity = a.Field<string>("ToCity"),

                                                 HotelBranch = a.Field<string>("HotelBranch"),
                                                 HotelBranchID = GlobalCode.Field2Int(a["HotelBranchID"]),
                                                 HotelEmail = a.Field<string>("HotelEmail"),

                                                 WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),

                                                 colArrivalDateTime = a.Field<DateTime?>("colArrivalDateTime"),
                                                 colDepartureDateTime = a.Field<DateTime?>("colDepartureDateTime"),
                                                 ArrivalTime = a.Field<TimeSpan?>("ArrivalTime"),
                                                 DepartureTime = a.Field<TimeSpan?>("DepartureTime"),

                                                 ArvlCity = a.Field<string>("ArvlCity"),
                                                 DeptCity = a.Field<string>("DeptCity"),

                                                 colFlightNoVarchar = a.Field<string>("colFlightNoVarchar"),
                                                 AL = a.Field<string>("AL"),
                                                 Carrier = a.Field<string>("Carrier"),

                                                 colPortIdInt = GlobalCode.Field2Int(a["colPortIdInt"]),
                                                 CountryID = GlobalCode.Field2Int(a["CountryID"]),
                                                 CityID = GlobalCode.Field2Int(a["CityID"]),

                                                 Couple = a.Field<int?>("Couple"),
                                                 SignOn = a.Field<DateTime?>("SignOn"),
                                                 Voucher = GlobalCode.Field2Decimal(a["Voucher"]),
                                                 TravelDate = a.Field<DateTime?>("TravelDate"),
                                                 Reason = a.Field<string>("Reason"),

                                                 colRemarksIDInt = GlobalCode.Field2Int(a["colRemarksIDInt"]),
                                                 colRemarksVarchar = a.Field<string>("colRemarksVarchar"),

                                                 E1TravelReqId = GlobalCode.Field2Int(a["E1TravelReqId"]),
                                                 Birthdate = GlobalCode.Field2String(a["Birthday"])
                                             }).ToList(),
                    TentativeManifestCount = maxRows,
                });

                return TentativeManifest;
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
                if (TentativeManifest != null)
                {
                    TentativeManifest = null;
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date            10/10/2012
        /// Description:    Get Forecast Manifest list to export
        /// </summary>
        public List<TentativeManifestExport> GetForecastExportList(DateTime dateFrom, DateTime dateTo, string userId, int BranchId)
        {
            DataTable dt = null;
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            List<TentativeManifestExport> TentativeList = new List<TentativeManifestExport>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectHotelManifestForecastExport");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.Date, dateFrom);
                db.AddInParameter(dbCommand, "@pSFDateTo", DbType.Date, dateTo);
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, userId);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.Int32, BranchId);

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                TentativeList = (from a in dt.AsEnumerable()
                                 select new TentativeManifestExport
                                 {
                                     HotelCity = a.Field<string>("HotelCity"),
                                     CheckIn = a.Field<DateTime>("CheckIn"),
                                     CheckOut = a.Field<DateTime>("CheckOut"),
                                     HotelNights = a.Field<Int16>("HotelNights"),
                                     ReasonCode = a.Field<string>("ReasonCode"),
                                     LastName = a.Field<string>("LastName"),
                                     FirstName = a.Field<string>("FirstName"),

                                     EmployeeId = a.Field<Int64>("EmployeeId"),
                                     Gender = a.Field<string>("Gender"),
                                     SingleDouble = GlobalCode.Field2Double(a["SingleDouble"]).ToString(),
                                     Couple = GlobalCode.Field2String(a["couple"]),
                                     Title = a.Field<string>("Rank"),
                                     Ship = a.Field<string>("Ship"),
                                     CostCenter = a.Field<string>("CostCenter"),
                                     Nationality = a.Field<string>("Nationality"),
                                     HotelRequest = a.Field<string>("HotelRequest"),
                                     RecLoc = a.Field<string>("RecLoc"),

                                     DeptCity = a.Field<string>("DeptCity"),
                                     ArvlCity = a.Field<string>("ArvlCity"),
                                     DeptDate = a.Field<string>("DeptDate"),
                                     ArvlDate = a.Field<string>("ArvlDate"),
                                     DeptTime = a.Field<string>("DeptTime"),
                                     ArvlTime = a.Field<string>("ArvlTime"),
                                     Carrier = a.Field<string>("AL"),
                                     FlightNo = a.Field<string>("FlightNo"),
                                     Voucher = a.Field<decimal>("Voucher"),

                                     PassportNo = a.Field<string>("PassportNo"),
                                     PassportExpiration = a.Field<string>("PassportExpiration"),
                                     PasportDateIssued = a.Field<string>("PassportIssued"),

                                     Birthdate = GlobalCode.Field2String(a["Birthday"]),
                                     
                                     HotelBranch = a.Field<string>("HotelBranch"),
                                     BookingRemarks = a.Field<string>("colRemarksVarchar")

                                 }).ToList();

                return TentativeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  26/Nov/2011
        /// Created By:    Josephine Gad
        /// (description)  Get the list of nationality, gender, rank, vessel and hotel in forecast page
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="RegionID"></param>
        /// <param name="PortID"></param>
        /// <param name="AirportID"></param>
        /// <returns></returns>
        public List<ForeCastFilters> ForecastGetFilters(string sUser, int RegionID, int PortID, int AirportID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataSet ds = null;
            //DataTable dtNationality = null;
            //DataTable dtGender = null;
            //DataTable dtRank = null;
            //DataTable dtVessel = null;
            //DataTable dtHotel = null;


            List<ForeCastFilters> list = new List<ForeCastFilters>();
            try
            {
                comm = db.GetStoredProcCommand("uspSelectHotelManifestForecastFilter");
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, sUser);
                db.AddInParameter(comm, "@pRegionIDInt", DbType.Int64, RegionID);
                db.AddInParameter(comm, "@pPortIDInt", DbType.Int64, PortID);
                db.AddInParameter(comm, "@pAirportID", DbType.Int64, AirportID);
                ds = db.ExecuteDataSet(comm);
                //dtGender = db.ExecuteDataSet(comm).Tables[1];
                //dtRank = db.ExecuteDataSet(comm).Tables[2];
                //dtVessel = db.ExecuteDataSet(comm).Tables[3];
                //dtHotel = db.ExecuteDataSet(comm).Tables[4];


                list.Add(new ForeCastFilters()
                {
                    NationalityList = (from a in ds.Tables[0].AsEnumerable()
                                       select new NationalityList
                                       {
                                           NationalityID = GlobalCode.Field2Int(a["NationalityID"]),
                                           Nationality = a["NationalityName"].ToString()
                                       }).ToList(),
                    GenderList = (from a in ds.Tables[1].AsEnumerable()
                                  select new GenderList
                                  {
                                      GenderID = GlobalCode.Field2Int(a["GenderID"]),
                                      Gender = a["GenderName"].ToString()
                                  }).ToList(),
                    RankList = (from a in ds.Tables[2].AsEnumerable()
                                select new RankList
                                {
                                    RankID = GlobalCode.Field2Int(a["RankID"]),
                                    Rank = a["RankName"].ToString()
                                }).ToList(),
                    VesselList = (from a in ds.Tables[3].AsEnumerable()
                                  select new VesselList
                                  {
                                      VesselID = GlobalCode.Field2Int(a["VesselID"]),
                                      VesselName = a["VesselName"].ToString()
                                  }).ToList(),
                    HotelDTO = (from a in ds.Tables[4].AsEnumerable()
                                 select new HotelDTO
                                 {
                                     HotelIDString = a["BranchID"].ToString(),
                                     HotelNameString = a["BranchName"].ToString()
                                 }).ToList()
                });

                return list;
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
                //if (dtGender != null)
                //{
                //    dtGender.Dispose();
                //}
                //if (dtRank != null)
                //{
                //    dtRank.Dispose();
                //}
                //if (dtVessel != null)
                //{
                //    dtVessel.Dispose();
                //}
                //if (dtHotel != null)
                //{
                //    dtHotel.Dispose();
                //}
            }
        }
        /// <summary>
        /// Date Created:  19/Mar/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list for HotelConfirmManifest Page
        /// ==========================================================
        /// Date Modified:  14/May/2013
        /// Modified By:    Marco Abejar
        /// (description)  Add birthday field
        /// ==========================================================
        /// Date Modified:  29/Aug/2014
        /// Modified By:    Josephine Monteza
        /// (description)   Add filter iNoOfDays for the date range
        /// ==========================================================
        /// Date Modified:  02/Mar/2015
        /// Modified By:    Michael Evangelista
        /// Description:    Add FlightStats data to Stored Procedure
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestPage(string DateFromString,
         string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
         string Nationality, string Gender, string Rank, string Status,
         string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
          int StartRow, int RowCount, Int16 LoadType, string orderBy, Int32 iNoOfDays)
        {
            List<HotelManifest> TentativeManifest = new List<HotelManifest>();
            List<HotelManifest> ConfirmedManifest = new List<HotelManifest>();
            List<HotelManifest> CancelledManifest = new List<HotelManifest>();
            List<EmailRecipient> EmailRecipient = new List<EmailRecipient>();
            List<HotelDashboardList> CountSummary = new List<HotelDashboardList>();

            List<HotelDTO> HotelList = new List<HotelDTO>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            Int32 ConfirmedCount = 0;
            Int32 CancelledCount = 0;


            DataTable dtManifest = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;
            DataTable dtCountSummary = null;
            DataTable dtEmail = null;
            DataTable dtNoOfDays = null;
            DataTable dtHotelSpecialistEmail = null;
            

            //DataTable dtHotel = null;
            DataSet ds = null;

            HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;
            HttpContext.Current.Session["ConfirmManifest_TentativeManifestCount"] = maxRows;

            HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["ConfirmManifest_ConfirmedManifestCount"] = ConfirmedCount;

            HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
            HttpContext.Current.Session["ConfirmManifest_CancelledManifestCount"] = CancelledCount;

            HttpContext.Current.Session["ConfirmManifest_CountSummary"] = CountSummary;
            
            try
            {                
                dbCommand = db.GetStoredProcCommand("uspGetHotelConfirmManifestPage");
                dbCommand.CommandTimeout = 60;
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);

                db.AddInParameter(dbCommand, "@pFilterByDate", DbType.String, DateFilter);
                db.AddInParameter(dbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                db.AddInParameter(dbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
                db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
                db.AddInParameter(dbCommand, "@pRank", DbType.String, Rank);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.String, Region);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.String, Country);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.String, City);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.String, Port);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.String, Hotel);
                db.AddInParameter(dbCommand, "@pVesselIDInt", DbType.String, Vessel);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, RowCount);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, LoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, orderBy);

                db.AddInParameter(dbCommand, "@pNoOfDays", DbType.Int32, iNoOfDays);

                ds = db.ExecuteDataSet(dbCommand);
                
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                ConfirmedCount = Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
                CancelledCount = Int32.Parse(ds.Tables[4].Rows[0][0].ToString());

                dtManifest = ds.Tables[1];
                dtConfirmed = ds.Tables[3];
                dtCancelled = ds.Tables[5];
                if (ds.Tables.Count > 9)
                {
                    dtCountSummary = ds.Tables[9];
                }
                TentativeManifest = (from a in dtManifest.AsEnumerable()
                                     select new HotelManifest
                                     {
                                        IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                        TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                        RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                        HotelCity = a.Field<string>("HotelCity"),
                                        CheckIn =  a.Field<DateTime?>("TimeSpanStartDate"),
                                        CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                        HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                        ReasonCode = a.Field<string>("Reason"),
                                        LastName = GlobalCode.Field2String(a["LastName"]),
                                        FirstName =  GlobalCode.Field2String(a["FirstName"]),

                                        EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                        Gender =  a.Field<string>("Gender"),
                                        SingleDouble = a.Field<string>("RoomName"),
                                        Couple = a.Field<string>("Couple"),

                                        Title = a.Field<string>("Rank"),
                                        ShipCode = a.Field<string>("colVesselCodeVarchar"),                                        
                                        ShipName =  a.Field<string>("Vessel"),
                                        CostCenterCode = a.Field<string>("CostCenter"),                                        
                                        CostCenter =  a.Field<string>("CostCenterName"),
                                        Nationality = a.Field<string>("Nationality"),
                                        HotelRequest = a.Field<string>("HotelRequest"),
                                        RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                        DeptCity =  a.Field<string>("DeptCity"),
                                        ArvlCity = a.Field<string>("ArvlCity"),
                                        DeptDate = a.Field<DateTime?>("DepartureDate"),
                                        ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                        DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                        ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                        /*Flight Stats*/
                                        ActualDepartureDate = a.Field<string>("actDateD"),
                                        ActualArrivalDate = a.Field<string>("actDateT"),
                                        ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                        ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                        ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),


                                        /*End of Flight Stats*/

                                        Carrier = a.Field<string>("Carrier"),
                                        FlightNo = a.Field<string>("FlightNoVarchar"),
                                        Voucher =  GlobalCode.Field2Decimal(a["Voucher"]),

                                        PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                        PassportExpiration = a.Field<string>("colPassportExpiration"),
                                        PasportDateIssued =  GlobalCode.Field2String(a["colPassportIssued"]),
                                        Birthday = a.Field<DateTime?>("Birthday"),


                                        HotelBranch =  a.Field<string>("HotelBranch"),
                                        BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                        WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                        Status = a.Field<string>("Status"),
                                        HotelTransID = GlobalCode.Field2Long(a["TransHotelID"]),                                                                             
                                     }).ToList();

                ConfirmedManifest = (from a in dtConfirmed.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),


                                         /*Flight Stats*/
                                         ActualDepartureDate = a.Field<string>("actDateD"),
                                         ActualArrivalDate = a.Field<string>("actDateT"),
                                         ActualArrivalGate = a.Field<string>("FlightArrGate"),
                                         ActualArrivalStatus = a.Field<string>("FlightStatus"),
                                         ActualArrivalBaggage = a.Field<string>("FlightBaggageClaim"),


                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                         Birthday = a.Field<DateTime?>("Birthday"),


                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                         Remarks = a.Field<string>("Remarks"),

                                         IsTaggedByUser = GlobalCode.Field2Bool(a["IsTaggedByUser"]),
                                         IsTagLinkVisible = GlobalCode.Field2Bool(a["IsTagLinkVisible"]),

                                         ConfirmationNo = a.Field<string>("ConfirmationNoNew"),
                                         IsHotelVendor = GlobalCode.Field2Bool(a["IsHotelVendor"]),
                                         HotelTransID = GlobalCode.Field2Long(a["TransHotelID"]),



                                         CancellationTermsInt = GlobalCode.Field2Int(a["CancellationTermsInt"]),
                                         HotelTimeZoneID = GlobalCode.Field2String(a["HotelTimeZoneIDVarchar"]),
                                         CutOffTime = GlobalCode.Field2DateTime(a["CutOffTime"]),


                                     }).ToList();

                CancelledManifest = (from a in dtCancelled.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                         Birthday = a.Field<DateTime?>("Birthday"),


                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                         HotelTransID = GlobalCode.Field2Long(a["TransHotelID"]),
                                     }).ToList();

                if (dtCountSummary != null)
                {
                    CountSummary = (from a in dtCountSummary.AsEnumerable()
                                    select new HotelDashboardList
                                    {
                                        BranchID = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                        HotelBranchName = GlobalCode.Field2String(a["HotelBranchName"]),
                                        TotalSingleRoomBlock = GlobalCode.Field2Decimal(a["TotalSingleBookings"]),
                                        TotalDoubleRoomBlock = GlobalCode.Field2Decimal(a["TotalDoubleBookings"]),
                                        IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"]),
                                        ContractId = GlobalCode.Field2Int(a["colContractIdInt"]),

                                    }).ToList();
                }

                if (LoadType == 0)
                {
                    HttpContext.Current.Session["ConfirmManifest_EmailRecipient"] = EmailRecipient;

                    dtEmail = ds.Tables[6];
                    EmailRecipient = (from a in dtEmail.AsEnumerable()
                                      select new EmailRecipient {
                                          EmailTo = a.Field<string>("EmailTo"),
                                          EmailCc = a.Field<string>("EmailCc")
                                      }).ToList();
                    
                    HttpContext.Current.Session["ConfirmManifest_EmailRecipient"] = EmailRecipient;

                    dtNoOfDays = ds.Tables[7];
                    TMSettings.NoOfDays = GlobalCode.Field2Int(dtNoOfDays.Rows[0]["colNoOfDays_HotelVendor"]);                    
                    
                    dtHotelSpecialistEmail = ds.Tables[8];
                    TravelMartVariable.HotelSpecialistEmail = GlobalCode.Field2String(dtHotelSpecialistEmail.Rows[0]["colEmailVarchar"]);
                }

                HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;
                HttpContext.Current.Session["ConfirmManifest_TentativeManifestCount"] = maxRows;

                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifestCount"] = ConfirmedCount;

                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
                HttpContext.Current.Session["ConfirmManifest_CancelledManifestCount"] = CancelledCount;

                HttpContext.Current.Session["ConfirmManifest_CountSummary"] = CountSummary;
              
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
                if (TentativeManifest != null)
                {
                    TentativeManifest = null;
                } 
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }       
                if (dtCancelled!= null)
                {
                    dtCancelled.Dispose();
                }
                if (dtCountSummary != null)
                {
                    dtCountSummary.Dispose();
                } 
                if (dtEmail != null)
                {
                    dtEmail.Dispose();
                } 
                if (dtNoOfDays != null)
                {
                    dtNoOfDays.Dispose();
                } 
                if (dtHotelSpecialistEmail != null)
                {
                    dtHotelSpecialistEmail.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:  18/Sept/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list for HotelConfirmManifest Page by Page Number
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestByPageNumber(string strUser,  string UserRole, int StartRow, int RowCount, string loadType)
        {
            List<HotelManifest> TentativeManifest = new List<HotelManifest>();

            if (loadType == "Confirm")
            {
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = TentativeManifest;
            }
            if (loadType == "Cancel")
            {
                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = TentativeManifest;
            }

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;


            DataTable dtManifest = null;
            DataSet ds = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelConfirmManifestByPageNumber");
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, RowCount);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, loadType);

                ds = db.ExecuteDataSet(dbCommand);

                dtManifest = ds.Tables[0];


                if (loadType == "Confirm")
                {
                    TentativeManifest = (from a in dtManifest.AsEnumerable()
                                         select new HotelManifest
                                         {
                                             IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                             TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                             RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                             HotelCity = a.Field<string>("HotelCity"),
                                             CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                             CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                             HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                             ReasonCode = a.Field<string>("Reason"),
                                             LastName = GlobalCode.Field2String(a["LastName"]),
                                             FirstName = GlobalCode.Field2String(a["FirstName"]),

                                             EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                             Gender = a.Field<string>("Gender"),
                                             SingleDouble = a.Field<string>("RoomName"),
                                             Couple = a.Field<string>("Couple"),

                                             Title = a.Field<string>("Rank"),
                                             ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                             ShipName = a.Field<string>("Vessel"),
                                             CostCenterCode = a.Field<string>("CostCenter"),
                                             CostCenter = a.Field<string>("CostCenterName"),
                                             Nationality = a.Field<string>("Nationality"),
                                             HotelRequest = a.Field<string>("HotelRequest"),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                             DeptCity = a.Field<string>("DeptCity"),
                                             ArvlCity = a.Field<string>("ArvlCity"),
                                             DeptDate = a.Field<DateTime?>("DepartureDate"),
                                             ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                             DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                             ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                             Carrier = a.Field<string>("Carrier"),
                                             FlightNo = a.Field<string>("FlightNoVarchar"),
                                             Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                             PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                             PassportExpiration = a.Field<string>("colPassportExpiration"),
                                             PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                             Birthday = a.Field<DateTime?>("Birthday"),


                                             HotelBranch = a.Field<string>("HotelBranch"),
                                             BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                             WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                             Status = a.Field<string>("Status"),

                                             ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                             ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                             Remarks = a.Field<string>("Remarks"),

                                             IsTaggedByUser = GlobalCode.Field2Bool(a["IsTaggedByUser"]),
                                             IsTagLinkVisible = GlobalCode.Field2Bool(a["IsTagLinkVisible"]),

                                             ConfirmationNo = a.Field<string>("ConfirmationNoNew"),
                                             IsHotelVendor = GlobalCode.Field2Bool(a["IsHotelVendor"]),
                                             HotelTransID = GlobalCode.Field2Long(a["TransHotelID"]),
                                         }).ToList();
                }
                else if (loadType == "Cancel")
                {
                    TentativeManifest = (from a in dtManifest.AsEnumerable()
                                         select new HotelManifest
                                         {
                                             IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                             TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                             RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                             HotelCity = a.Field<string>("HotelCity"),
                                             CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                             CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                             HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                             ReasonCode = a.Field<string>("Reason"),
                                             LastName = GlobalCode.Field2String(a["LastName"]),
                                             FirstName = GlobalCode.Field2String(a["FirstName"]),

                                             EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                             Gender = a.Field<string>("Gender"),
                                             SingleDouble = a.Field<string>("RoomName"),
                                             Couple = a.Field<string>("Couple"),

                                             Title = a.Field<string>("Rank"),
                                             ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                             ShipName = a.Field<string>("Vessel"),
                                             CostCenterCode = a.Field<string>("CostCenter"),
                                             CostCenter = a.Field<string>("CostCenterName"),
                                             Nationality = a.Field<string>("Nationality"),
                                             HotelRequest = a.Field<string>("HotelRequest"),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                             DeptCity = a.Field<string>("DeptCity"),
                                             ArvlCity = a.Field<string>("ArvlCity"),
                                             DeptDate = a.Field<DateTime?>("DepartureDate"),
                                             ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                             DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                             ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                             Carrier = a.Field<string>("Carrier"),
                                             FlightNo = a.Field<string>("FlightNoVarchar"),
                                             Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                             PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                             PassportExpiration = a.Field<string>("colPassportExpiration"),
                                             PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                             Birthday = a.Field<DateTime?>("Birthday"),


                                             HotelBranch = a.Field<string>("HotelBranch"),
                                             BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                             WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                             Status = a.Field<string>("Status"),

                                             HotelTransID = GlobalCode.Field2Long(a["TransHotelID"]),
                                         }).ToList();

                }
                if (loadType == "Confirm")
                {
                    HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = TentativeManifest;
                }
                if (loadType == "Cancel")
                {
                    HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = TentativeManifest;
                }
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
                if (dtManifest != null)
                {
                    dtManifest.Dispose();
                }
                if (TentativeManifest != null)
                {
                    TentativeManifest = null;
                }
            }
        }
        /// <summary>
        /// Date Created:  03/Apr/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list of Confirm Manifest for Export use
        /// ==========================================================
        /// Date Modified:  14/May/2013
        /// Modified By:    Marco Abejar
        /// (description)  Add birthday field
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestExport(string DateFromString,
         string strUser,  string Hotel, string orderBy)
        {
            //List<HotelManifest> TentativeManifest = new List<HotelManifest>();
            List<HotelManifest> ConfirmedManifest = new List<HotelManifest>();
            List<HotelManifest> CancelledManifest = new List<HotelManifest>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;           

            //DataTable dtManifest = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;

            DataSet ds = null;

            //HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;
            HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelConfirmManifestExport");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);
             
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.String, Hotel);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, orderBy);

                ds = db.ExecuteDataSet(dbCommand);
                
                //dtManifest = ds.Tables[0];
                dtConfirmed = ds.Tables[0];
                dtCancelled = ds.Tables[1];

                //TentativeManifest = (from a in dtManifest.AsEnumerable()
                //                     select new HotelManifest
                //                     {
                //                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                //                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                //                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                //                         HotelCity = a.Field<string>("HotelCity"),
                //                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                //                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                //                         HotelNites = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                //                         ReasonCode = a.Field<string>("Reason"),
                //                         LastName = GlobalCode.Field2String(a["LastName"]),
                //                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                //                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                //                         Gender = a.Field<string>("Gender"),
                //                         SingleDouble = a.Field<string>("RoomName"),
                //                         Couple = a.Field<string>("Couple"),

                //                         Title = a.Field<string>("Rank"),
                //                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                //                         ShipName = a.Field<string>("Vessel"),
                //                         CostCenterCode = a.Field<string>("CostCenter"),
                //                         CostCenter = a.Field<string>("CostCenterName"),
                //                         Nationality = a.Field<string>("Nationality"),
                //                         HotelRequest = a.Field<string>("HotelRequest"),
                //                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                //                         DeptCity = a.Field<string>("DeptCity"),
                //                         ArvlCity = a.Field<string>("ArvlCity"),
                //                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                //                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                //                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                //                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                //                         Carrier = a.Field<string>("Carrier"),
                //                         FlightNo = a.Field<string>("FlightNoVarchar"),
                //                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                //                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                //                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                //                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                //                         Birthday = a.Field<DateTime?>("Birthday"),

                //                         HotelBranch = a.Field<string>("HotelBranch"),
                //                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                //                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                //                         Status = a.Field<string>("Status"),

                //                     }).ToList();

                ConfirmedManifest = (from a in dtConfirmed.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                         Birthday = a.Field<DateTime?>("Birthday"),


                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                         Remarks = a.Field<string>("Remarks"),                                         

                                     }).ToList();

                CancelledManifest = (from a in dtCancelled.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights= GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                         Birthday = a.Field<DateTime?>("Birthday"),


                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                     }).ToList();
               
                //HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;               
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;               
                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
               
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
                //if (dtManifest != null)
                //{
                //    dtManifest.Dispose();
                //}
                //if (TentativeManifest != null)
                //{
                //    TentativeManifest = null;
                //}
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }                
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   21/Mar/2013
        /// Description:    Confirm record and get the new confirmed and cancelled record
        /// ---------------------------------------------------------------
        /// Date Modified:  11/Jun/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add IsHotelVendor field
        /// ==========================================================
        /// </summary>
        public void ConfirmHotelManifest(string UserId, DateTime dDate, int iBranchID,
            string sRole, bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate, int iNoOfDays,
            DataTable dtToConfirm)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;
            DataTable dtEmail = null;

            try
            {
                List<HotelManifest> TentativeManifest = new List<HotelManifest>();
                List<HotelManifest> ConfirmedManifest = new List<HotelManifest>();
                List<HotelManifest> CancelledManifest = new List<HotelManifest>();
                List<EmailRecipient> EmailRecipient = new List<EmailRecipient>();

                HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
                HttpContext.Current.Session["ConfirmManifest_EmailRecipient"] = EmailRecipient;

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspConfirmHotelManifest");

                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.Date, dDate);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.Int32, iBranchID);

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
                db.AddInParameter(dbCommand, "@pNoOfDays", DbType.Int32, iNoOfDays);

                SqlParameter param = new SqlParameter("@pTblTempHotelManifestConfirmed", dtToConfirm);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

                ds = db.ExecuteDataSet(dbCommand, trans);
                dtConfirmed = ds.Tables[0];
                dtCancelled = ds.Tables[1];
                dtEmail = ds.Tables[2];

                ConfirmedManifest = (from a in dtConfirmed.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),

                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                         Remarks = a.Field<string>("Remarks"),

                                     }).ToList();

                CancelledManifest = (from a in dtCancelled.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),

                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                     }).ToList();

                EmailRecipient = (from a in dtEmail.AsEnumerable()
                                  select new EmailRecipient
                                  {
                                      EmailTo = a.Field<string>("EmailTo"),
                                      EmailCc = a.Field<string>("EmailCc")
                                  }).ToList();

                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
                HttpContext.Current.Session["ConfirmManifest_EmailRecipient"] = EmailRecipient;

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
                if (dtEmail != null)
                {
                    dtEmail.Dispose();
                }
                if (dtToConfirm != null)
                {
                    dtToConfirm.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:  03/Sept/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list for HotelConfirmManifest Forecast
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestForecast(string DateFromString, string DateToString,
         string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
         string Nationality, string Gender, string Rank, string Status,
         string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
          int StartRow, int RowCount, Int16 LoadType, string orderBy)
        {
            List<HotelManifest> ConfirmedManifest = new List<HotelManifest>();
            List<HotelManifest> CancelledManifest = new List<HotelManifest>();

            List<HotelDTO> HotelList = new List<HotelDTO>();


            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            Int32 ConfirmedCount = 0;
            Int32 CancelledCount = 0;

            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;

            //DataTable dtHotel = null;
            DataSet ds = null;

            HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["ConfirmManifest_ConfirmedManifestCount"] = ConfirmedCount;

            HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
            HttpContext.Current.Session["ConfirmManifest_CancelledManifestCount"] = CancelledCount; 
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelConfirmManifestForecast");
                db.AddInParameter(dbCommand, "@pSFDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFromString));
                db.AddInParameter(dbCommand, "@pSFDateTo", DbType.DateTime, GlobalCode.Field2DateTime(DateToString));
                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, strUser);

                db.AddInParameter(dbCommand, "@pFilterByDate", DbType.String, DateFilter);
                db.AddInParameter(dbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                db.AddInParameter(dbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                db.AddInParameter(dbCommand, "@pNationality", DbType.String, Nationality);
                db.AddInParameter(dbCommand, "@pGender", DbType.String, Gender);
                db.AddInParameter(dbCommand, "@pRank", DbType.String, Rank);
                db.AddInParameter(dbCommand, "@pStatus", DbType.String, Status);

                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.String, Region);
                db.AddInParameter(dbCommand, "@pCountryIDInt", DbType.String, Country);
                db.AddInParameter(dbCommand, "@pCityIDInt", DbType.String, City);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.String, Port);
                db.AddInParameter(dbCommand, "@pHotelIDInt", DbType.String, Hotel);
                db.AddInParameter(dbCommand, "@pVesselIDInt", DbType.String, Vessel);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.String, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.String, RowCount);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.String, LoadType);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, orderBy);
                dbCommand.CommandTimeout = 0;
                ds = db.ExecuteDataSet(dbCommand);

                if (ds.Tables.Count > 0)
                {
                    ConfirmedCount = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    dtConfirmed = ds.Tables[1];

                    CancelledCount = Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
                    dtCancelled = ds.Tables[3];

                    ConfirmedManifest = (from a in dtConfirmed.AsEnumerable()
                                         select new HotelManifest
                                         {
                                             IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                             TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                             RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                             HotelCity = a.Field<string>("HotelCity"),
                                             CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                             CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                             HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                             ReasonCode = a.Field<string>("Reason"),
                                             LastName = GlobalCode.Field2String(a["LastName"]),
                                             FirstName = GlobalCode.Field2String(a["FirstName"]),

                                             EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                             Gender = a.Field<string>("Gender"),
                                             SingleDouble = a.Field<string>("RoomName"),
                                             Couple = a.Field<string>("Couple"),

                                             Title = a.Field<string>("Rank"),
                                             ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                             ShipName = a.Field<string>("Vessel"),
                                             CostCenterCode = a.Field<string>("CostCenter"),
                                             CostCenter = a.Field<string>("CostCenterName"),
                                             Nationality = a.Field<string>("Nationality"),
                                             HotelRequest = a.Field<string>("HotelRequest"),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                             DeptCity = a.Field<string>("DeptCity"),
                                             ArvlCity = a.Field<string>("ArvlCity"),
                                             DeptDate = a.Field<DateTime?>("DepartureDate"),
                                             ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                             DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                             ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                             Carrier = a.Field<string>("Carrier"),
                                             FlightNo = a.Field<string>("FlightNoVarchar"),
                                             Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                             PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                             PassportExpiration = a.Field<string>("colPassportExpiration"),
                                             PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                             Birthday = a.Field<DateTime?>("Birthday"),


                                             HotelBranch = a.Field<string>("HotelBranch"),
                                             BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                             WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                             Status = a.Field<string>("Status"),

                                             ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                             ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                             Remarks = a.Field<string>("Remarks"),

                                             IsTaggedByUser = GlobalCode.Field2Bool(a["IsTaggedByUser"]),
                                             IsTagLinkVisible = GlobalCode.Field2Bool(a["IsTagLinkVisible"]),

                                             ConfirmationNo = a.Field<string>("ConfirmationNoNew"),
                                             IsHotelVendor = GlobalCode.Field2Bool(a["IsHotelVendor"]),

                                         }).ToList();

                    CancelledManifest = (from a in dtCancelled.AsEnumerable()
                                         select new HotelManifest
                                         {
                                             IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                             TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                             RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                             HotelCity = a.Field<string>("HotelCity"),
                                             CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                             CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                             HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                             ReasonCode = a.Field<string>("Reason"),
                                             LastName = GlobalCode.Field2String(a["LastName"]),
                                             FirstName = GlobalCode.Field2String(a["FirstName"]),

                                             EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                             Gender = a.Field<string>("Gender"),
                                             SingleDouble = a.Field<string>("RoomName"),
                                             Couple = a.Field<string>("Couple"),

                                             Title = a.Field<string>("Rank"),
                                             ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                             ShipName = a.Field<string>("Vessel"),
                                             CostCenterCode = a.Field<string>("CostCenter"),
                                             CostCenter = a.Field<string>("CostCenterName"),
                                             Nationality = a.Field<string>("Nationality"),
                                             HotelRequest = a.Field<string>("HotelRequest"),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                             DeptCity = a.Field<string>("DeptCity"),
                                             ArvlCity = a.Field<string>("ArvlCity"),
                                             DeptDate = a.Field<DateTime?>("DepartureDate"),
                                             ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                             DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                             ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                             Carrier = a.Field<string>("Carrier"),
                                             FlightNo = a.Field<string>("FlightNoVarchar"),
                                             Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                             PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                             PassportExpiration = a.Field<string>("colPassportExpiration"),
                                             PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),

                                             HotelBranch = a.Field<string>("HotelBranch"),
                                             BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                             WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                             Status = a.Field<string>("Status"),

                                         }).ToList();

                }
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifestCount"] = ConfirmedCount;

                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;
                HttpContext.Current.Session["ConfirmManifest_CancelledManifestCount"] = CancelledCount; 
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
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                } 
            }
        }

        /// <summary>
        /// Date Created:  23/Apr/2014
        /// Created By:    Muhallidin G Wali
        /// (description)  cancel hotel booking and insert into a table (TblHotelTransactionOtherCancel)
        /// ==========================================================
        //  Date Modified:  27/May/2014
        /// Modified By:    Josephine Monteza
        /// (description)   Add sRole and audit trail parameters
        /// ==========================================================
        /// </summary>    
        public void CancelHotelBooking(string TravelReq, string User, string Email, string CCEmail, 
            string confirmby, string Comment, DateTime CancelDate, bool SendEmail, string sRole,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            try
            {
                DateTime currentDatetime = DateTime.Now;
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspCancelHotelBooking");
                db.AddInParameter(dbCommand, "@pTravelReqVarchar", DbType.String, TravelReq);
                db.AddInParameter(dbCommand, "@pUserVarchar", DbType.String, User);
                db.AddInParameter(dbCommand, "@pEmailVarchar", DbType.String, Email);
                db.AddInParameter(dbCommand, "@pCCEmailVarchar", DbType.String, CCEmail);
                db.AddInParameter(dbCommand, "@pConfirmbyVarchar", DbType.String, confirmby);
                db.AddInParameter(dbCommand, "@pCommentVarchar", DbType.String, Comment);
                db.AddInParameter(dbCommand, "@pCancelDate", DbType.DateTime, CancelDate);
                db.AddInParameter(dbCommand, "@pSendEmail", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, currentDatetime);

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
        /// Date Created:   11/Sep/2014
        /// Created By:     Josephine Monteza
        /// (description)   Tag To Hotel
        /// ==========================================================
        /// </summary>
        public void TagToHotel(Int64 iIDBigint, Int64 iTravelReqID, string sRecLoc, Int64 iSeafarerID,
            string sStatus, Int64 iBranchID, Int64 iPortAgentID, string sUserName,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();


            try
            {               

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspTagToHotel");

                db.AddInParameter(dbCommand, "@pIdBigint", DbType.Int64, iIDBigint);
                db.AddInParameter(dbCommand, "@pTravelReqIDInt", DbType.Int64, iTravelReqID);
                db.AddInParameter(dbCommand, "@pRecordLocatorVarchar", DbType.String, sRecLoc);
                db.AddInParameter(dbCommand, "@pSeafarerIdInt", DbType.Int64, iSeafarerID);

                db.AddInParameter(dbCommand, "@pOnOffVarchar", DbType.String, sStatus);
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iBranchID);
                db.AddInParameter(dbCommand, "@pPortAgentVendorIDInt", DbType.Int32, iPortAgentID);

                db.AddInParameter(dbCommand, "@pUserIDVarchar", DbType.String, sUserName);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.ExecuteDataSet(dbCommand, trans);              
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
            }
        }
        /// <summary>
        /// Date Created:  27/Nov/2014
        /// Created By:    Josephine Monteza
        /// (description)  Get Hotel Control No
        /// ==========================================================
        /// </summary>        
        public void GetHotelControlNo(int iBranchID, string sDate)
        {
            List<HotelControlNo> listControl = new List<HotelControlNo>();
            List<HotelCancelationTerms> listCancelTerms = new List<HotelCancelationTerms>();
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;            

            DataTable dtControl = null;
            DataTable dtCancelTerms = null;
            DataSet ds = null;

            HttpContext.Current.Session["ConfirmManifest_ControlNo"] = listControl;
            HttpContext.Current.Session["ConfirmManifest_Cancelterms"] = listCancelTerms;
           
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelControlNo");
                dbCommand.CommandTimeout = 60;

                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iBranchID);
                db.AddInParameter(dbCommand, "@pDateOfManifest", DbType.DateTime, GlobalCode.Field2DateTime(sDate));                

                ds = db.ExecuteDataSet(dbCommand);

              
                dtControl = ds.Tables[0];
                dtCancelTerms = ds.Tables[1];
                
                listControl = (from a in dtControl.AsEnumerable()
                                     select new HotelControlNo
                                     {
                                         ControlID = GlobalCode.Field2Int(a["colControlNoInt"]),
                                         ControlNumber = GlobalCode.Field2String(a["colControlNoVarchar"]),                                        
                                     }).ToList();

                listCancelTerms = (from a in dtCancelTerms.AsEnumerable()
                                     select new HotelCancelationTerms
                                     {
                                         CancelationHours = GlobalCode.Field2Int(a["colCancellationTermsInt"]),
                                         CutoffTime = a.Field<TimeSpan?>("colCutOffTime"),
                                         sTimezone = a.Field<string>("colHotelTimeZoneIDVarchar")
                                     }).ToList();


                HttpContext.Current.Session["ConfirmManifest_ControlNo"] = listControl;
                HttpContext.Current.Session["ConfirmManifest_Cancelterms"] = listCancelTerms;           

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
                if (dtControl != null)
                {
                    dtControl.Dispose();
                }               
                if (dtCancelTerms != null)
                {
                    dtCancelTerms.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:  10/Jul/2015
        /// Created By:    Josephine monteza
        /// (description)  Get the list of Confirm Manifest through Confirmation No. for Export use
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestWithControlNoExport(int iControlID, string orderBy)
        {
            //List<HotelManifest> TentativeManifest = new List<HotelManifest>();
            List<HotelManifest> ConfirmedManifest = new List<HotelManifest>();
            List<HotelManifest> CancelledManifest = new List<HotelManifest>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dtManifest = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;

            DataSet ds = null;

            //HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;
            HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
            HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelConfirmManifestWithControlNoExport");
                db.AddInParameter(dbCommand, "@pControlNoInt", DbType.Int32 ,iControlID);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, orderBy);

                ds = db.ExecuteDataSet(dbCommand);

                //dtManifest = ds.Tables[0];
                dtConfirmed = ds.Tables[0];
                dtCancelled = ds.Tables[1];

                //TentativeManifest = (from a in dtManifest.AsEnumerable()
                //                     select new HotelManifest
                //                     {
                //                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                //                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                //                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                //                         HotelCity = a.Field<string>("HotelCity"),
                //                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                //                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                //                         HotelNites = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                //                         ReasonCode = a.Field<string>("Reason"),
                //                         LastName = GlobalCode.Field2String(a["LastName"]),
                //                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                //                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                //                         Gender = a.Field<string>("Gender"),
                //                         SingleDouble = a.Field<string>("RoomName"),
                //                         Couple = a.Field<string>("Couple"),

                //                         Title = a.Field<string>("Rank"),
                //                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                //                         ShipName = a.Field<string>("Vessel"),
                //                         CostCenterCode = a.Field<string>("CostCenter"),
                //                         CostCenter = a.Field<string>("CostCenterName"),
                //                         Nationality = a.Field<string>("Nationality"),
                //                         HotelRequest = a.Field<string>("HotelRequest"),
                //                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                //                         DeptCity = a.Field<string>("DeptCity"),
                //                         ArvlCity = a.Field<string>("ArvlCity"),
                //                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                //                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                //                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                //                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                //                         Carrier = a.Field<string>("Carrier"),
                //                         FlightNo = a.Field<string>("FlightNoVarchar"),
                //                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                //                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                //                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                //                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                //                         Birthday = a.Field<DateTime?>("Birthday"),

                //                         HotelBranch = a.Field<string>("HotelBranch"),
                //                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                //                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                //                         Status = a.Field<string>("Status"),

                //                     }).ToList();

                ConfirmedManifest = (from a in dtConfirmed.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                         Birthday = a.Field<DateTime?>("Birthday"),


                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                         ConfirmedBy = a.Field<string>("colConfirmedBy"),
                                         ConfirmedDate = a.Field<string>("ConfirmedDate"),

                                         Remarks = a.Field<string>("Remarks"),

                                     }).ToList();

                CancelledManifest = (from a in dtCancelled.AsEnumerable()
                                     select new HotelManifest
                                     {
                                         IDBigInt = GlobalCode.Field2Int(a["IDBigInt"]),
                                         TravelReqIdInt = GlobalCode.Field2Int(a["TravelReqId"]),
                                         RequestID = GlobalCode.Field2Int(a["RequestID"]),

                                         HotelCity = a.Field<string>("HotelCity"),
                                         CheckIn = a.Field<DateTime?>("TimeSpanStartDate"),
                                         CheckOut = a.Field<DateTime?>("TimeSpanEndDate"),
                                         HotelNights = GlobalCode.Field2TinyInt(a["TimeSpanDuration"]),
                                         ReasonCode = a.Field<string>("Reason"),
                                         LastName = GlobalCode.Field2String(a["LastName"]),
                                         FirstName = GlobalCode.Field2String(a["FirstName"]),

                                         EmployeeId = GlobalCode.Field2Int(a["EmployeeId"]),
                                         Gender = a.Field<string>("Gender"),
                                         SingleDouble = a.Field<string>("RoomName"),
                                         Couple = a.Field<string>("Couple"),

                                         Title = a.Field<string>("Rank"),
                                         ShipCode = a.Field<string>("colVesselCodeVarchar"),
                                         ShipName = a.Field<string>("Vessel"),
                                         CostCenterCode = a.Field<string>("CostCenter"),
                                         CostCenter = a.Field<string>("CostCenterName"),
                                         Nationality = a.Field<string>("Nationality"),
                                         HotelRequest = a.Field<string>("HotelRequest"),
                                         RecLoc = GlobalCode.Field2String(a["RecLoc"]),

                                         DeptCity = a.Field<string>("DeptCity"),
                                         ArvlCity = a.Field<string>("ArvlCity"),
                                         DeptDate = a.Field<DateTime?>("DepartureDate"),
                                         ArvlDate = a.Field<DateTime?>("ArrivalDate"),
                                         DeptTime = a.Field<TimeSpan?>("DepartureTime"),
                                         ArvlTime = a.Field<TimeSpan?>("ArrivalTime"),

                                         Carrier = a.Field<string>("Carrier"),
                                         FlightNo = a.Field<string>("FlightNoVarchar"),
                                         Voucher = GlobalCode.Field2Decimal(a["Voucher"]),

                                         PassportNo = GlobalCode.Field2String(a["colPassportNo"]),
                                         PassportExpiration = a.Field<string>("colPassportExpiration"),
                                         PasportDateIssued = GlobalCode.Field2String(a["colPassportIssued"]),
                                         Birthday = a.Field<DateTime?>("Birthday"),


                                         HotelBranch = a.Field<string>("HotelBranch"),
                                         BookingRemarks = a.Field<string>("colRemarksVarchar"),
                                         WithEvent = GlobalCode.Field2Bool(a["WithEvent"]),
                                         Status = a.Field<string>("Status"),

                                     }).ToList();

                //HttpContext.Current.Session["ConfirmManifest_TentativeManifest"] = TentativeManifest;               
                HttpContext.Current.Session["ConfirmManifest_ConfirmedManifest"] = ConfirmedManifest;
                HttpContext.Current.Session["ConfirmManifest_CancelledManifest"] = CancelledManifest;

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
                //if (dtManifest != null)
                //{
                //    dtManifest.Dispose();
                //}
                //if (TentativeManifest != null)
                //{
                //    TentativeManifest = null;
                //}
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:  30/Mar/2016
        /// Created By:    Josephine Monteza
        /// (description)  Get the hotel details of selected hotel
        /// ==========================================================
        /// </summary>        
        public List<BranchList> GetHotelVendorDetails(Int32 iBranchID)
        {
            List<BranchList> list = new List<BranchList>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DataTable dt = null;
            DataSet ds = null;


            try
            {
                dbCommand = db.GetStoredProcCommand("uspHotelVendorGet");
                db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, iBranchID);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new BranchList
                        {
                            BranchId = GlobalCode.Field2Int(a["colBranchIDInt"]),
                            BranchName = a.Field<string>("colVendorBranchNameVarchar"),
                            ContractId = GlobalCode.Field2Int(a["colContractIdInt"]),
                            ContractDateStart = a.Field<string>("ContractDateStart"),
                            ContractDateEnd = a.Field<string>("ContractDateEnd"),
                        }).ToList();

                return list;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

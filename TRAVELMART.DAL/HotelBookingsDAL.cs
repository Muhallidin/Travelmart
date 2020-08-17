using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.Common;
using System.Data;
using System.Data.Sql;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web;

namespace TRAVELMART.DAL
{
    public class HotelBookingsDAL
    {
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: load user branch list details
        /// ---------------------------------------------------------------
        /// Modified By:    Josephine Monteza
        /// Date Modified:  17/Dec/2014
        /// Description:    Add Crew Schedule and Air Transaction session
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="isAccredited"></param>
        /// <returns></returns>
        public List<HotelBookingsGenericClass> LoadUserBranchDetails(string UserId, DateTime Date, int HotelTransId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable Branch = null;
            DataTable Details = null;
            DataTable Region = null;
            DataTable Country = null;
            DataTable City = null;

            DataTable dtCrewSched = null;
            DataTable dtAirTrans = null;

            try
            {
                List<HotelBookingsGenericClass> EditorDetails = new List<HotelBookingsGenericClass>();
                dbCommand = db.GetStoredProcCommand("uspGetHotelEditorDetails");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pHotelTransId", DbType.Int32, HotelTransId);
                DataSet ds = db.ExecuteDataSet(dbCommand);

                Region = ds.Tables[0];
                Country = ds.Tables[1];
                City = ds.Tables[2];
                Branch = ds.Tables[3];
                if (HotelTransId != 0)
                {
                    Details = ds.Tables[4];
                    dtCrewSched = ds.Tables[5];
                    dtAirTrans = ds.Tables[6];
                }

                if (HotelTransId == 0)
                {
                    EditorDetails.Add(new HotelBookingsGenericClass()
                    {
                        RegionList = (from a in Region.AsEnumerable()
                                      select new RegionList
                                      {
                                          RegionId = a.Field<int?>("RegionId"),
                                          RegionName = a.Field<string>("RegionName"),
                                      }).ToList(),
                        CountryList = (from a in Country.AsEnumerable()
                                       select new CountryList
                                       {
                                           RegionId = a.Field<int?>("RegionId"),
                                           CountryId = a.Field<int?>("CountryId"),
                                           CountryName = a.Field<string>("CountryName"),
                                       }).ToList(),
                        CityList = (from a in City.AsEnumerable()
                                    select new CityList
                                    {
                                        CountryId = a.Field<int?>("CountryId"),
                                        CityId = a.Field<int?>("CityId"),
                                        CityName = a.Field<string>("CityName"),
                                    }).ToList(),
                        BranchList = (from a in Branch.AsEnumerable()
                                      select new BranchList
                                      {
                                          VendorId = GlobalCode.Field2Int(a["colVendorIDInt"]),
                                          BranchId = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                          BranchName = a.Field<string>("colVendorBranchNameVarchar"),
                                          CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                          CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
                                          CurrencyId = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                          Currency = a.Field<string>("colCurrencyNameVarchar"),
                                          ContractId = GlobalCode.Field2Int(a["colContractIdInt"]),
                                          withShuttle = a.Field<bool?>("colWithShuttleBit"),
                                          isAccredited = a.Field<bool>("colIsAccreditedBit"),
                                          EventCount = GlobalCode.Field2Int(a["EventCount"]),
                                          RegionId = GlobalCode.Field2Int(a["RegionId"]),
                                      }).ToList()
                    });
                  
                    return EditorDetails;
                }
                else
                {
                    EditorDetails.Add(new HotelBookingsGenericClass()
                    {
                        RegionList = (from a in Region.AsEnumerable()
                                      select new RegionList
                                      {
                                          RegionId = a.Field<int?>("RegionId"),
                                          RegionName = a.Field<string>("RegionName"),
                                      }).ToList(),
                        CountryList =(from a in Country.AsEnumerable()
                                      select new CountryList
                                      {
                                          RegionId = a.Field<int?>("RegionId"),
                                          CountryId = a.Field<int?>("CountryId"),
                                          CountryName = a.Field<string>("CountryName"),
                                      }).ToList(),
                        CityList = (from a in City.AsEnumerable()
                                      select new CityList
                                      {
                                          CountryId = a.Field<int?>("CountryId"),
                                          CityId = a.Field<int?>("CityId"),
                                          CityName = a.Field<string>("CityName"),
                                      }).ToList(),
                        BranchList = (from a in Branch.AsEnumerable()
                                      select new BranchList
                                      {
                                          VendorId = GlobalCode.Field2Int(a["colVendorIDInt"]),
                                          BranchId = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                          BranchName = a.Field<string>("colVendorBranchNameVarchar"),
                                          CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                          CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
                                          CurrencyId = GlobalCode.Field2Int(a["colCurrencyIDInt"]),
                                          Currency = a.Field<string>("colCurrencyNameVarchar"),
                                          ContractId =GlobalCode.Field2Int(a["colContractIdInt"]),
                                          withShuttle = a.Field<bool?>("colWithShuttleBit"),
                                          isAccredited = a.Field<bool>("colIsAccreditedBit"),
                                          EventCount = GlobalCode.Field2Int(a["EventCount"]),
                                          RegionId = GlobalCode.Field2Int(a["RegionId"]),
                                      }).ToList(),
                        SeafarerDetailsList = (from b in Details.AsEnumerable()
                                               select new SeafarerDetails
                                               {
                                                   VendorId = b.Field<int>("colVendorIDInt"),
                                                   BranchId = b.Field<int>("BranchId"),
                                                   RoomTypeId = b.Field<int>("RoomTypeId"),
                                                   CheckInDate = b.Field<DateTime>("CheckInDate"),
                                                   CheckInTime = b.Field<DateTime>("CheckInTime"),
                                                   Duration = b.Field<int>("Duration"),
                                                   RoomSource = b.Field<int>("RoomSource"),
                                                   Remarks = b.Field<string>("Remarks"),
                                                   HotelStatus = b.Field<string>("HotelStatus"),
                                                   WithShuttle = b.Field<bool>("WithShuttle"),
                                                   ConfirmationNum = b.Field<string>("ConfirmationNum"),
                                                   IsAccredited = b.Field<bool>("IsAccredited"),
                                                   Stripes = GlobalCode.Field2Float(b["colStripesDecimal"]),
                                                   EmployeeID = GlobalCode.Field2Long(b["colSeafarerIdInt"]),
                                                   LastName = b.Field<string>("colLastNameVarchar"),
                                                   FirstName = b.Field<string>("colFirstNameVarchar"),
                                                   HotelName = b.Field<string>("HotelName"),
                                                   ContractID = GlobalCode.Field2Long(b["ContractID"]),

                                                   CurrencyID = GlobalCode.Field2Int(b["colCurrencyIDInt"]),
                                                   CurrencyName = b.Field<string>("CurrencyName"),

                                                   CityID = GlobalCode.Field2Int(b["colCityIDInt"]),
                                                   CountryID = GlobalCode.Field2Int(b["colCountryIDInt"]),
                                                   TravelRequetID = GlobalCode.Field2Long(b["colTravelReqIdInt"]),
                                                   IDBigint = GlobalCode.Field2Long(b["colIdBigint"]),
                                               }).ToList()
                    });

                    SeafarerList itemCrewName = new SeafarerList();
                    List<SeafarerList> listCrewName = new List<SeafarerList>();
                    List<SeafarerDetailList> listCrewSched = new List<SeafarerDetailList>();
                    List<CrewAssistAirTransaction> listAir = new List<CrewAssistAirTransaction>();

                    HttpContext.Current.Session["HotelBookingsSeafarerNameList"] = listCrewName;
                    HttpContext.Current.Session["HotelBookingsCrewSchedule"] = listCrewSched;
                    HttpContext.Current.Session["HotelBookingsAirTrans"] = listAir;


                    if (EditorDetails[0].SeafarerDetailsList.Count > 0)
                    {
                        itemCrewName.SeafarerID = EditorDetails[0].SeafarerDetailsList[0].EmployeeID;
                        itemCrewName.LastName = EditorDetails[0].SeafarerDetailsList[0].LastName;
                        itemCrewName.FirstName = EditorDetails[0].SeafarerDetailsList[0].FirstName;
                        itemCrewName.Name = EditorDetails[0].SeafarerDetailsList[0].LastName + ", " + EditorDetails[0].SeafarerDetailsList[0].FirstName;

                        listCrewName.Add(itemCrewName);
                    }

                    listCrewSched = (from a in dtCrewSched.AsEnumerable()
                                     select new SeafarerDetailList
                                     {
                                         TravelRequetID = GlobalCode.Field2Long(a["colTravelReqIdInt"]),
                                         IDBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                         RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                         OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                         PortCode = a.Field<string>("colPortCodeVarchar"),
                                         ReasonCode = a.Field<string>("colReasonCodeVarchar"),
                                         VesselCode = a.Field<string>("colVesselCodeVarchar"),
                                         Status = a.Field<string>("colStatusVarchar"),
                                         IsSelected = GlobalCode.Field2Bool(a["IsSelected"]),
                                     }).ToList();

                    listAir = (from a in dtAirTrans.AsEnumerable()
                               select new CrewAssistAirTransaction
                                     {
                                         IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                         SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                                         AirlineCode = a.Field<string>("Airline"),
                                         DepartureDateTime = a.Field<DateTime>("DeptDate"),
                                         ArrivalDateTime = a.Field<DateTime>("ArrDate"),

                                         DepartureAirportLocationCode = a.Field<string>("DeptCode"),
                                         ArrivalAirportLocationCode = a.Field<string>("ArrCode"),
                                         IsSelected = GlobalCode.Field2Bool(a["IsSelected"]),
                                     }).ToList();

                    HttpContext.Current.Session["HotelBookingsSeafarerNameList"] = listCrewName;
                    HttpContext.Current.Session["HotelBookingsCrewSchedule"] = listCrewSched;
                    HttpContext.Current.Session["HotelBookingsAirTrans"] = listAir;

                    return EditorDetails;
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
                if (Branch != null)
                {
                    Branch.Dispose();
                }
                if (Details != null)
                {
                    Details.Dispose();
                }
                if (Region != null)
                {
                    Region.Dispose();
                }
                if (Country != null)
                {
                    Country.Dispose();
                }
                if (City != null)
                {
                    City.Dispose();
                }
                if (dtCrewSched != null)
                {
                    dtCrewSched.Dispose();
                }
                if (dtAirTrans != null)
                {
                    dtAirTrans.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: get room allocations
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="Duration"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public List<RoomAllocations> getRoomBlocks(DateTime CheckInDate, int Duration, int numOfPeople, int RoomType, int BranchId, int ContractId)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspCheckRemainingBlocks");
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pNumberOfPeople", DbType.Int32, numOfPeople);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
               

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                var roomList = (from a in dt.AsEnumerable()
                                  select new RoomAllocations
                                  {
                                      coldate = a.Field<DateTime?>("coldate"),
                                      colNumOfPeople = a.Field<int?>("colNumOfPeople"),
                                      remaining = a.Field<decimal?>("remaining"),
                                      valid = GlobalCode.Field2Bool(a["valid"]),
                                      sourceAllocation = a.Field<int?>("sourceAllocation"),
                                      contractId = GlobalCode.Field2Int(a["contractId"]),

                                      BranchId = GlobalCode.Field2Int(a["BranchID"]),
                                      RoomTypeId = GlobalCode.Field2Int(a["RoomTypeID"]),
                                      ReservedOverride = GlobalCode.Field2Int(a["ReservedOverride"]),
                                      EmergencyVacant = GlobalCode.Field2Int(a["EmRoomBlockRemaining"])
                                  }).ToList();

                return roomList;
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
            }
        }


        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 14/05/2013
        /// Description: get room allocations
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="Duration"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public List<RoomAllocations> getRoomBlocks(DateTime CheckInDate, int Duration, int numOfPeople, int RoomType, int BranchId, int ContractId,decimal Stripe)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspCheckRemainingBlocks");
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pNumberOfPeople", DbType.Int32, numOfPeople);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pStripe", DbType.Decimal, Stripe);

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                var roomList = (from a in dt.AsEnumerable()
                                select new RoomAllocations
                                {
                                    coldate = a.Field<DateTime?>("coldate"),
                                    colNumOfPeople = a.Field<int?>("colNumOfPeople"),
                                    remaining = a.Field<decimal?>("remaining"),
                                    valid = GlobalCode.Field2Bool(a["valid"]),
                                    sourceAllocation = a.Field<int?>("sourceAllocation"),
                                    contractId = GlobalCode.Field2Int(a["contractId"]),

                                    BranchId = GlobalCode.Field2Int(a["BranchID"]),
                                    RoomTypeId = GlobalCode.Field2Int(a["RoomTypeID"]),
                                    ReservedOverride = GlobalCode.Field2Int(a["ReservedOverride"]),
                                    EmergencyVacant = GlobalCode.Field2Int(a["EmRoomBlockRemaining"]),
                                    RoomCount = GlobalCode.Field2Decimal(a["RoomCount"]),

                                    remainContrat = GlobalCode.Field2Decimal(a["remainingContract"]) 
                                }).ToList();

                return roomList;
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
            }
        }



        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: Book Seafarer
        /// --------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  25/07/2012
        /// Description:    Add parameter IDBigint and SeqNo
        /// </summary>
        /// <param name="TravelReqId"></param>
        /// <param name="RecordLocator"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="SfStatus"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="Stripes"></param>
        /// <param name="ShuttleBit"></param>
        public List<HotelEmail> BookSeafarer(string IDBigint, string SeqNo,string TravelReqId, string ReqId, string RecordLocator, int Source, 
            int ContractId, int VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId, string SfStatus,
            int CityId, int CountryId, string Remarks, string Stripes, string HotelCity, string IsPort,
            bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, out bool isValid)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspApproveHotelTransactionBookings");
                db.AddInParameter(dbCommand, "@pIDBigint", DbType.String, IDBigint);
                db.AddInParameter(dbCommand, "@pSeqNo", DbType.String, SeqNo);

                db.AddInParameter(dbCommand, "@pTravelReqId", DbType.String, TravelReqId);
                db.AddInParameter(dbCommand, "@pReqId", DbType.String, ReqId);
                db.AddInParameter(dbCommand, "@pRecordLocator", DbType.String, RecordLocator);
                db.AddInParameter(dbCommand, "@pSource", DbType.Int32, Source);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pConfirmationNum", DbType.String, confirmationNum);
                db.AddInParameter(dbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSfStatus", DbType.String, SfStatus);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pStripes", DbType.String, Stripes);

                db.AddInParameter(dbCommand, "@pHotelCity", DbType.String, HotelCity);
                db.AddInParameter(dbCommand, "@pIsPort", DbType.String, IsPort);
                
                db.AddInParameter(dbCommand, "@pShuttleBit", DbType.Boolean, ShuttleBit);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, Description);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, Function);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, PathName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, TimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.Date, DateNow);
                db.AddOutParameter(dbCommand, "@pValid", DbType.Boolean, 50);
                ds = db.ExecuteDataSet(dbCommand, dbTransaction);
                isValid = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@pValid"));

                dbTransaction.Commit();
                
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[3];
                    var emailList = (from a in dt.AsEnumerable()
                                     select new HotelEmail
                                     {
                                         RoleName = a.Field<string>("RoleName"),
                                         EmailAddress = a.Field<string>("Email"),
                                     }).ToList();
                    return emailList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
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
        /// Author:Charlene Remotigue
        /// Date Created: 13/02/2012
        /// Description: Book Seafarer
        /// --------------------------------
        /// Modified By:    Josephine Gad
        /// Modified Date:  25/07/2012
        /// Description:    Add parameter IDBigint and SeqNo
        /// --------------------------------
        /// Modified By:    Muhallidin G Wali
        /// Modified Date:  15/05/2013
        /// Description:    Add parameter RoomCount
        /// </summary>
        /// <param name="TravelReqId"></param>
        /// <param name="RecordLocator"></param>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="SfStatus"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="Stripes"></param>
        /// <param name="ShuttleBit"></param>
        public List<HotelEmail> BookSeafarer(string IDBigint, string SeqNo, string TravelReqId, string ReqId, string RecordLocator, int Source,
            int? ContractId, int VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId, string SfStatus,
            int CityId, int CountryId, string Remarks, string Stripes, string HotelCity, string IsPort,
            bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, out bool isValid, decimal roomcount, bool Emergency)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspApproveHotelEditTransBookings");
                db.AddInParameter(dbCommand, "@pIDBigint", DbType.String, IDBigint);
                db.AddInParameter(dbCommand, "@pSeqNo", DbType.String, SeqNo);

                db.AddInParameter(dbCommand, "@pTravelReqId", DbType.String, TravelReqId);
                db.AddInParameter(dbCommand, "@pReqId", DbType.String, ReqId);
                db.AddInParameter(dbCommand, "@pRecordLocator", DbType.String, RecordLocator);
                db.AddInParameter(dbCommand, "@pSource", DbType.Int32, Source);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pConfirmationNum", DbType.String, confirmationNum);
                db.AddInParameter(dbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSfStatus", DbType.String, SfStatus);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pStripes", DbType.String, Stripes);

                db.AddInParameter(dbCommand, "@pHotelCity", DbType.String, HotelCity);
                db.AddInParameter(dbCommand, "@pIsPort", DbType.String, IsPort);

                db.AddInParameter(dbCommand, "@pShuttleBit", DbType.Boolean, ShuttleBit);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, Description);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, Function);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, PathName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, TimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.Date, DateNow);
                db.AddOutParameter(dbCommand, "@pValid", DbType.Boolean, 50);
                db.AddInParameter(dbCommand, "@pRoomCount", DbType.Decimal, roomcount);
                db.AddInParameter(dbCommand, "@pEmergency", DbType.Boolean, Emergency);
                
                ds = db.ExecuteDataSet(dbCommand, dbTransaction);
                isValid = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@pValid"));

                dbTransaction.Commit();

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    var emailList = (from a in dt.AsEnumerable()
                                     select new HotelEmail
                                     {
                                         RoleName = a.Field<string>("RoleName"),
                                         EmailAddress = a.Field<string>("Email"),
                                     }).ToList();
                    return emailList;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
            
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
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
        /// Author:         Muhallidin G Wali
        /// Date Created:   13/02/2012
        /// Description:    Book Seafarer
        /// --------------------------------
        /// </summary>
        /// <param name="BookHotel"></param>
        /// <param name="isValid"></param>
        public List<HotelEmail> BookSeafarer(List<TRAVELMART.BLL.HotelTransactionOverFlowBooking> BookHotel,short savefrom, out bool isValid)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            DataTable dt = null;
            DataSet ds = null;
            try
            {

                GlobalCode gc = new GlobalCode();
                dt = gc.getDataTable(BookHotel);


                dbCommand = db.GetStoredProcCommand("uspApproveHotelTransactionManualBookings");
                db.AddOutParameter(dbCommand, "@pValid", DbType.Boolean, 50);
                db.AddInParameter(dbCommand, "@pSaveFrom", DbType.Int16, savefrom);
                SqlParameter param = new SqlParameter("@pTableValues", dt);

                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);
                ds = db.ExecuteDataSet(dbCommand, dbTransaction);
                isValid = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@pValid"));
                //dbCommand.ExecuteNonQuery();
                dbTransaction.Commit();
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[2];
                    var emailList = (from a in dt.AsEnumerable()
                                     select new HotelEmail
                                     {
                                         RoleName = a.Field<string>("RoleName"),
                                         EmailAddress = a.Field<string>("Email"),
                                     }).ToList();
                    return emailList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
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
        /// Author: Charlene Remotigue
        /// Date Created: 15/02/2012
        /// Description: Update Seafarer Hotel Bookings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="ShuttleBit"></param>
        /// <param name="Description"></param>
        /// <param name="Function"></param>
        /// <param name="PathName"></param>
        /// <param name="TimeZone"></param>
        /// <param name="GMTDate"></param>
        /// <param name="DateNow"></param>
        /// <param name="HotelTransId"></param>
        /// <returns></returns>
        public List<HotelEmail> UpdateSeafarerBookings(int Source, int? ContractId, int? VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId,
            int CityId, int CountryId, string Remarks, bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, int HotelTransId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateHotelTransactionBookings");
                db.AddInParameter(dbCommand, "@pSource", DbType.Int32, Source);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pConfirmationNum", DbType.String, confirmationNum);
                db.AddInParameter(dbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pShuttleBit", DbType.Boolean, ShuttleBit);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, Description);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, Function);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, PathName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, TimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.Date, DateNow);
                db.AddInParameter(dbCommand, "@pHotelTransId", DbType.Int32, HotelTransId);
                dt = db.ExecuteDataSet(dbCommand, dbTransaction).Tables[0];
                dbTransaction.Commit();

                var emailList = (from a in dt.AsEnumerable()
                                 select new HotelEmail
                                 {
                                     RoleName = a.Field<string>("RoleName"),
                                     EmailAddress = a.Field<string>("Email"),
                                 }).ToList();

                return emailList;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 15/02/2012
        /// Description: Update Seafarer Hotel Bookings
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="ContractId"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="RoomType"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckInTime"></param>
        /// <param name="Duration"></param>
        /// <param name="confirmationNum"></param>
        /// <param name="HotelStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="Remarks"></param>
        /// <param name="ShuttleBit"></param>
        /// <param name="Description"></param>
        /// <param name="Function"></param>
        /// <param name="PathName"></param>
        /// <param name="TimeZone"></param>
        /// <param name="GMTDate"></param>
        /// <param name="DateNow"></param>
        /// <param name="HotelTransId"></param>
        /// <returns></returns>
        public List<HotelEmail> UpdateSeafarerBookings(int Source, int? ContractId, int? VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId,
            int CityId, int CountryId, string Remarks, bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, string Stripes,bool Emergency, int HotelTransId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateHotelEditTranBookings");
                db.AddInParameter(dbCommand, "@pSource", DbType.Int32, Source);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pConfirmationNum", DbType.String, confirmationNum);
                db.AddInParameter(dbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pShuttleBit", DbType.Boolean, ShuttleBit);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, Description);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, Function);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, PathName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, TimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.Date, DateNow);
                db.AddInParameter(dbCommand, "@pStripes",DbType.String, @Stripes);
                db.AddInParameter(dbCommand, "@pEmergency", DbType.Boolean, Emergency);
                db.AddInParameter(dbCommand, "@pHotelTransId", DbType.Int32, HotelTransId);
                dt = db.ExecuteDataSet(dbCommand, dbTransaction).Tables[0];
                dbTransaction.Commit();

                var emailList = (from a in dt.AsEnumerable()
                                 select new HotelEmail
                                 {
                                     RoleName = a.Field<string>("RoleName"),
                                     EmailAddress = a.Field<string>("Email"),
                                 }).ToList();

                return emailList;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   19/Dec/2014
        /// Description:    Update Seafarer ID of Hotel Booking
        /// </summary>       
        /// <returns></returns>
        public void UpdateSeafarerBookingsEditSeafarer(int Source, int? ContractId, int? VendorId, int BranchId, int RoomType,
            DateTime CheckInDate, DateTime CheckInTime, int Duration, string confirmationNum, string HotelStatus, string UserId,
            int CityId, int CountryId, string Remarks, bool ShuttleBit, string Description, string Function, string PathName,
            string TimeZone, DateTime GMTDate, DateTime DateNow, string Stripes, bool Emergency, int HotelTransId, 
            Int64 iTRID, Int64 iIDBigInt, string SfStatus, string RecLoc, Int32 iSeqNo )
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateHotelEditTranBookingsEditSeafarer");
                db.AddInParameter(dbCommand, "@pSource", DbType.Int32, Source);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);

                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                db.AddInParameter(dbCommand, "@pDuration", DbType.Int32, Duration);
                db.AddInParameter(dbCommand, "@pConfirmationNum", DbType.String, confirmationNum);
                db.AddInParameter(dbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pRemarks", DbType.String, Remarks);
                db.AddInParameter(dbCommand, "@pShuttleBit", DbType.Boolean, ShuttleBit);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, Description);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, Function);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, PathName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, TimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.Date, DateNow);
                db.AddInParameter(dbCommand, "@pStripes", DbType.String, @Stripes);
                db.AddInParameter(dbCommand, "@pEmergency", DbType.Boolean, Emergency);
                db.AddInParameter(dbCommand, "@pHotelTransId", DbType.Int32, HotelTransId);

                db.AddInParameter(dbCommand, "@pTravelRequestID", DbType.Int64, iTRID);
                db.AddInParameter(dbCommand, "@pIDBigint", DbType.Int64, iIDBigInt);
                db.AddInParameter(dbCommand, "@pSfStatus", DbType.String, SfStatus);
                db.AddInParameter(dbCommand, "@pRecLoc", DbType.String, RecLoc);
                db.AddInParameter(dbCommand, "@pSeqNo", DbType.Int32, iSeqNo);
                
                db.ExecuteDataSet(dbCommand, dbTransaction);
                dbTransaction.Commit();

                //var emailList = (from a in dt.AsEnumerable()
                //                 select new HotelEmail
                //                 {
                //                     RoleName = a.Field<string>("RoleName"),
                //                     EmailAddress = a.Field<string>("Email"),
                //                 }).ToList();

                //return emailList;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/02/2012
        /// Description: get event count
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public int GetEventCount(DateTime Date, int BranchId)
        {
            int Count = 0;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            IDataReader dr = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetEventCountPerBranch");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    Count = Int32.Parse(dr["EventCount"].ToString());
                }

                return Count;
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
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/04/2012
        /// Description: get room blocks
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<RoomBlocks> getRemainingRooms(DateTime CheckInDate, DateTime CheckOutDate, int numOfPeople, int RoomType, int BranchId)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetRoomBlocks");
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pDateTo", DbType.Date, CheckOutDate);
                db.AddInParameter(dbCommand, "@pNumOfPeople", DbType.Int32, numOfPeople);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                var roomList = (from a in dt.AsEnumerable()
                                select new RoomBlocks
                                {
                                   colDate = a.Field<DateTime?>("colDate"),
                                   BranchId = GlobalCode.Field2Int(a["BranchId"]),
                                   RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                   Room= a.Field<string>("Room"),
                                   TotalBooking = GlobalCode.Field2Decimal(a["TotalBooking"]),
                                   ContractBlocks = GlobalCode.Field2Decimal(a["ContractBlocks"]),
                                   OverrideBlocks = GlobalCode.Field2Decimal(a["OverrideBlocks"]),
                                   ReservedOverride = GlobalCode.Field2Decimal(a["ReservedOverride"]),
                                   EmergencyBlocks = GlobalCode.Field2Decimal(a["EmergencyBlocks"]),
                                   ReservedEmergency = GlobalCode.Field2Decimal(a["ReservedEmergency"]),
                                   ContractId = GlobalCode.Field2Int(a["ContractId"]),
                                   validContract = a.Field<bool>("validContract"),
                                   validEmergency = a.Field<bool>("validEmergency"),
                                   validOverride = a.Field<bool>("validOverride"),
                                   RemainingRooms = GlobalCode.Field2Decimal(a["RemainingRooms"]),
                                }).ToList();

                return roomList;
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
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/04/2012
        /// Description: get room blocks
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<RemainRoomBlocksWithHotelID> getRemainingRooms(DateTime DateFrom, DateTime DateTo, int BranchId, int RoomType, long TransHotelID)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspCheckRemainingRoomForUpdate");
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.Date, DateFrom);
                db.AddInParameter(dbCommand, "@pDateTo", DbType.Date, DateTo);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pTransHotelID", DbType.Int64, TransHotelID);

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                var roomList = (from a in dt.AsEnumerable()
                                where GlobalCode.Field2Long(a["TransHotelID"]) != TransHotelID
                                select new RemainRoomBlocksWithHotelID
                                {

                                    RemainVacantRoom = GlobalCode.Field2Decimal(a["RemainVacantRoom"]),
                                    Date = GlobalCode.Field2DateTime(a["Date"]),
                                    RoomType = GlobalCode.Field2Int(a["RoomTypeID"]),
                                    BranchID = GlobalCode.Field2Int(a["BranchID"]),
                                    ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                    TransHotelID = GlobalCode.Field2Long(a["TransHotelID"])

                                }).ToList();

                return roomList;
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
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 04/04/2012
        /// Description: get room blocks
        /// </summary>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="numOfPeople"></param>
        /// <param name="RoomType"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<RemainRoomBlocks> getRemainingRooms(DateTime CheckInDate, DateTime CheckOutDate, int RoomType, int BranchId)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetRemainingRoomBlocks");
                db.AddInParameter(dbCommand, "@pDateFrom", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pDateTo", DbType.Date, CheckOutDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
               

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                var roomList = (from a in dt.AsEnumerable()
                                select new RemainRoomBlocks
                                {
                                    RemainContract = GlobalCode.Field2Decimal(a["RemainContract"]),//GlobalCode.Field2Decimal( a["RemainContract"]),
                                    RemainVacantRoom = GlobalCode.Field2Decimal(a["RemainVacantRoom"]),
                                    Date = GlobalCode.Field2DateTime(a["Date"]),
                                    RoomType = GlobalCode.Field2Int(a["RoomTypeID"]),
                                    BranchID = GlobalCode.Field2Int(a["BranchID"]),
                                    ContractRoomBlock = GlobalCode.Field2Int(a["ContractRoomBlock"]),
                                    OverrideRoomBlock = GlobalCode.Field2Int(a["OverrideRoomBlock"]),
                                    UsedContractRoomBlock = GlobalCode.Field2Decimal(a["ContractTotalBook"]),
                                    UsedOverrideRoomBlock = GlobalCode.Field2Decimal(a["OverrideTotalBook"]),
                                    ContractID = GlobalCode.Field2Int(a["ContractIDInt"]),
                                    RemainContractRoom = GlobalCode.Field2Decimal(a["RemainContractRoom"]),
                                    EmergencyRoomBlock = GlobalCode.Field2Decimal(a["EmergencyRoomBlock"]),
                                }).ToList();

                return roomList;
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
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   18/Dec/2014
        /// Description:    Get Crew Schedule
        /// </summary>
        public List<SeafarerDetailList> GetCrewScedule(Int64 iEmployeeID, bool bShowPast)
        {        
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            List<SeafarerDetailList> list = new List<SeafarerDetailList>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSeafarerCrewSchedule");
                db.AddInParameter(dbCommand, "@pSeafarerID", DbType.Int64, iEmployeeID);
                db.AddInParameter(dbCommand, "@pIsShowPast", DbType.Boolean, bShowPast);
                
                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new SeafarerDetailList
                            {
                                TravelRequetID = GlobalCode.Field2Long(a["colTravelReqIdInt"]),
                                IDBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                                SeafarerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                                Status = a.Field<string>("colStatusVarchar"),
                                RecordLocator = a.Field<string>("colRecordLocatorVarchar"),
                                OnOffDate = GlobalCode.Field2DateTime(a["OnOffDate"]),
                                PortCode = a.Field<string>("colPortCodeVarchar"),
                                ReasonCode = a.Field<string>("colReasonCodeVarchar"),
                                VesselCode = a.Field<string>("colVesselCodeVarchar"),
                                IsSelected = false,
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   18/Dec/2014
        /// Description:    Get Air Transaction
        /// </summary>
        public List<CrewAssistAirTransaction> GetAirTransaction(Int64 IDBigint)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            List<CrewAssistAirTransaction> list = new List<CrewAssistAirTransaction>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetSeafarerAirTrans");
                db.AddInParameter(dbCommand, "@pIDBigint", DbType.Int64, IDBigint);

                dt = db.ExecuteDataSet(dbCommand).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new CrewAssistAirTransaction
                        {
                             IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                             SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                             AirlineCode = a.Field<string>("Airline"),
                             DepartureDateTime = a.Field<DateTime>("DeptDate"),
                             ArrivalDateTime = a.Field<DateTime>("ArrDate"),

                             DepartureAirportLocationCode = a.Field<string>("DeptCode"),
                             ArrivalAirportLocationCode = a.Field<string>("ArrCode"),
                             IsSelected = false,
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   27/Nov/2017
        /// Description:    Delete selected Hotel Exceptin
        /// </summary>
        public short DeleteHotelException(string Excption, string UserID, string Comment)
        {
            DataTable dt = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            List<CrewAssistAirTransaction> list = new List<CrewAssistAirTransaction>();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteHotelException");
                db.AddInParameter(dbCommand, "@pExceptionID", DbType.String, Excption);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pComment", DbType.String, Comment);

                return GlobalCode.Field2TinyInt(db.ExecuteDataSet(dbCommand).Tables[0].Rows[0][0]);

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
            }
        }

    }
}

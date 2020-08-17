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
//using System.Web.Mail;
using System.Net.Mail;
using System.Net;
using System.Collections;


namespace TRAVELMART.DAL
{
    public class CrewAssistDAL
    {

        public List<SeafarerList> SeafarerList(short loadtype, string Seafarer, string userID)
        {
            //DataTable dt = null;
            DataSet ds = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetSeafarerList");
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, loadtype);
                db.AddInParameter(comm, "@pSeafarer", DbType.String, Seafarer);
                ds = db.ExecuteDataSet(comm);//.Tables[0];
                var SF = (from a in ds.Tables[0].AsEnumerable()
                          select new SeafarerList
                          {
                              SeafarerID = a.Field<long?>("EmployeeID"),
                              LastName = a.Field<string>("LastName"),
                              FirstName = a.Field<string>("FirstName"),
                              Name = a.Field<string>("LastName") + " " + a.Field<string>("FirstName"),

                          }).ToList();
                return SF;
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

        public List<SeafarerDetailHeader> SeafarerDetailTable(short loadtype, long SeafarerID, string UserID)
        {
            DataSet ds = null;
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetSeafarerDetailList");
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, loadtype);
                db.AddInParameter(comm, "@pSeafarerID", DbType.Int64, SeafarerID);
                db.AddInParameter(comm, "@pUserID", DbType.String, UserID);
                ds = db.ExecuteDataSet(comm);

                return ProcessSeafarerDetailHeader(ds, UserID);
                 
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

                if (ds != null)
                {
                    ds.Dispose();
                }
            }

        }

        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Hotel, Port, ExpendType
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssistGenericClass> GetGetHotelPortExpendTypeList(string userString, string regionString, string portString)
        {
            DataSet ds = null;
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            List<CrewAssistGenericClass> CrewAssistGenericClassList = new List<CrewAssistGenericClass>();
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelPortExpendTypeList");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(regionString));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(portString));
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                DataTable dt = ds.Tables[5];
                CrewAssistGenericClassList.Add(new CrewAssistGenericClass
                     {
                         CrewAssistHotelList = (from a in ds.Tables[0].AsEnumerable()
                                                select new CrewAssistHotelList
                                                {
                                                    HotelID = GlobalCode.Field2Int(a["BranchID"]),
                                                    HotelName = a["BranchName"].ToString(),
                                                    IsPortAgent = GlobalCode.Field2Bool(a["IsPortAgent"])
                                                }).ToList(),

                         CrewAssitPortList = (from b in ds.Tables[1].AsEnumerable()
                                              select new CrewAssitPortList
                                              {
                                                  PortId = GlobalCode.Field2Int(b["PORTID"]),
                                                  PortName = b["PORT"].ToString()
                                              }).ToList(),

                         CrewAssitExpendTypeList = (from c in ds.Tables[2].AsEnumerable()
                                                    select new CrewAssitExpendTypeList
                                                    {
                                                        ExpendTypeID = GlobalCode.Field2Int(c["colExpendTypeIdInt"]),
                                                        ExpendType = c["colExpendTypeVarchar"].ToString(),
                                                        IsSelected = GlobalCode.Field2Bool(c["colIsSelectedBit"]),
                                                    }).ToList(),


                         CrewAssistCurrency = (from d in ds.Tables[3].AsEnumerable()
                                               select new CrewAssistCurrency
                                                 {
                                                     CurrencyID = GlobalCode.Field2Int(d["colCurrencyIDInt"]),
                                                     CurrencyCode = d["colCurrencyCodeVarchar"].ToString(),
                                                     CurrencyName = d["colCurrencyNameVarchar"].ToString()
                                                 }).ToList(),

                         CrewAssistRout = (from e in ds.Tables[4].AsEnumerable()
                                           select new CrewAssistRout
                                                {
                                                    RoutId = GlobalCode.Field2Int(e["RouteID"]),
                                                    RoutName = e["Route"].ToString(),
                                                }).ToList(),

                         VehicleVendor = (from f in ds.Tables[5].AsEnumerable()
                                          select new VehicleVendor
                                                {
                                                    VehicleID = GlobalCode.Field2Int(f["colVehicleVendorIDInt"]),
                                                    Vehicle = f["colVehicleVendorNameVarchar"].ToString(),
                                                    PortCode = f["PortCode"].ToString(),
                                                    IsPortAgent = GlobalCode.Field2Bool(f["IsPortAgent"]),
                                                }).ToList(),


                         CrewAssistNationality = (from g in ds.Tables[6].AsEnumerable()
                                                  select new CrewAssistNationality
                                                 {
                                                     NatioalityID = GlobalCode.Field2Int(g["NatioalityID"]),
                                                     Nationality = g["Nationality"].ToString(),
                                                     NationalityCode = g["NationalityCode"].ToString(),
                                                 }).ToList(),

                         CrewAssistMeetAndGreetVendor = (from h in ds.Tables[7].AsEnumerable()
                                                         select new CrewAssistMeetAndGreetVendor
                                                       {
                                                           MeetAndGreetVendorID = GlobalCode.Field2Int(h["MeetAndGreetVendorID"]),
                                                           MeetAndGreetVendor = h["MeetAndGreetVendor"].ToString(),
                                                           AirportCodeID = GlobalCode.Field2Int(h["AirportCodeID"]),
                                                           AirportCode = h["AirportCode"].ToString(),
                                                       }).ToList(),

                         CrewAssistVendorPortAgent = (from i in ds.Tables[8].AsEnumerable()
                                                      select new CrewAssistVendorPortAgent
                                                {
                                                    PortAgentVendorID = GlobalCode.Field2Int(i["colPortAgentVendorIDInt"]),
                                                    PortAgentVendorName = i["colPortAgentVendorNameVarchar"].ToString(),
                                                    PortCodeID = GlobalCode.Field2Int(i["PortCodeID"]),
                                                    PortCode = i["PortCode"].ToString(),
                                                    IsAir = GlobalCode.Field2Bool(i["IsSeaport"]),
                                                }).ToList(),

                         CrewAssistSafeguardVendor = (from j in ds.Tables[9].AsEnumerable()
                                                      select new CrewAssistSafeguardVendor
                                                      {
                                                          SafeguardVendorID = GlobalCode.Field2Int(j["colSafeguardVendorIDInt"]),
                                                          SafeguardName = j["colSafeguardNameVarchar"].ToString(),
                                                          PortId = GlobalCode.Field2Int(j["colPortIdInt"]),
                                                          PortCode = j["colPortCodeVarchar"].ToString(),
                                                          PortName = j["colPortNameVarchar"].ToString(),
                                                      }).ToList(),

                         CrewAssistAirport = (from j in ds.Tables[10].AsEnumerable()
                                              select new CrewAssitPortList
                                                     {
                                                         PortId = GlobalCode.Field2Int(j["colAirportIDInt"]),
                                                         PortCode = j["colAirportCodeVarchar"].ToString(),
                                                         PortName = j["colAirportNameVarchar"].ToString(),
                                                     }).ToList(),


                         RemarkType = (from j in ds.Tables[11].AsEnumerable()
                                       select new CRRemarkType
                                                   {
                                                       RemarkTypeID = GlobalCode.Field2Int(j["colRemarksTypeHeaderIDInt"]),
                                                       RemarkType = j["colRemarksTypeHeaderVarchar"].ToString(),
                                                       RemarkTypeDetail = (from p in ds.Tables[14].AsEnumerable()
                                                                           where GlobalCode.Field2Int(j["colRemarksTypeHeaderIDInt"]) == GlobalCode.Field2Int(p["colRemarksTypeHeaderIDInt"])
                                                                        select new RemarkTypeDetail
                                                                        {
                                                                            RemarkTypeDetID = GlobalCode.Field2Int(p["colRemarksTypeIDInt"]),
                                                                            RemarkTypeDet = p["colRemarksTypeVarchar"].ToString(),
                                                                        }).OrderBy(r => r.RemarkTypeDet) .ToList()
                                                   }).OrderBy(r => r.RemarkType).ToList(),


                         RemarkStatus = (from j in ds.Tables[12].AsEnumerable()
                                         select new CRRemarkType
                                                     {
                                                         RemarkTypeID = GlobalCode.Field2Int(j["colRemarksStatusIDTinyInt"]),
                                                         RemarkType = j["colRemarksStatusVarchar"].ToString(),
                                                     }).OrderBy(r => r.RemarkType).ToList(),

                         RemarkRequestor = (from j in ds.Tables[13].AsEnumerable()
                                         select new CRRemarkType
                                                     {
                                                         RemarkTypeID = GlobalCode.Field2Int(j["colRequestorIDInt"]),
                                                         RemarkType = j["colRequestorVarchar"].ToString(),
                                                     }).OrderBy(r => r.RemarkType).ToList()


                     });
                return CrewAssistGenericClassList;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }


        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Hotel, Port, ExpendType
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssistHotelList> GetGetHotelPortExpendTypeList(short LoadType, string userString, string regionString, string portString)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelPortExpendTypeList");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadTypeInt", DbType.String, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(regionString));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(portString));
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                List<CrewAssistHotelList> _CrewAssistHotelList = new List<CrewAssistHotelList>();

                _CrewAssistHotelList = (from a in ds.Tables[0].AsEnumerable()
                                        select new CrewAssistHotelList
                                        {
                                            HotelID = GlobalCode.Field2Int(a["BranchID"]),
                                            HotelName = a["BranchName"].ToString(),
                                            Portcode = a["colPortCodeVarchar"].ToString(),
                                            IsPortAgent = GlobalCode.Field2Bool(a["IsPortAgent"])
                                        }).ToList();

                return _CrewAssistHotelList;
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
                if (ds != null)
                {
                    ds.Dispose();
                }

            }
        }



        /// <summary>
        /// Modified By:		Josephine Gad
        /// Create date:		25/Nov/2013
        /// Description:		Add param TravelRequestID to get Room Type ID of crew 
        /// ================================================= 
        /// </summary>
        public List<CrewAssistHotelInformation> CrewAssistHotelInformation(int TravelRequestID, int BranchID,
            string PortCode, string Arrcode, DateTime RequestDate, long EmployeeID)
        {
            DataSet dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetCrewAssistHotelInformation");

                db.AddInParameter(comm, "@pTravelRequest", DbType.String, TravelRequestID);
                db.AddInParameter(comm, "@pBranchID", DbType.String, BranchID);
                db.AddInParameter(comm, "@pPortCode", DbType.String, PortCode);
                db.AddInParameter(comm, "@pArrcode", DbType.String, Arrcode);
                db.AddInParameter(comm, "@pRequestDate", DbType.DateTime, RequestDate);
                db.AddInParameter(comm, "@pSeafererID", DbType.Int64, EmployeeID);
                comm.CommandTimeout = 0; 
                dt = db.ExecuteDataSet(comm);
                    
                var SF = (from a in dt.Tables[0].AsEnumerable()
                          select new CrewAssistHotelInformation
                          {

                              VendorBranchName = GlobalCode.Field2String(a["colVendorBranchNameVarchar"]),
                              CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                              CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                              ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                              VendorId = GlobalCode.Field2Int(a["colVendorIdInt"]),
                              BranchID = GlobalCode.Field2Int(a["colBranchIDInt"]),
                              ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                              Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                              EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                              EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                              CityName = GlobalCode.Field2String(a["colCityNameVarchar"]),
                              CityCode = GlobalCode.Field2String(a["HotelCityCode"]),
                              FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                              Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
                              MealVoucher = GlobalCode.Field2Double(a["MealVoucher"]).ToString("N2"),
                              ContractedRate = GlobalCode.Field2Double(a["ContractedRate"]).ToString("N2"),
                              ConfirmRate = GlobalCode.Field2Double(a["ConfirmRate"]).ToString("N2"),
                              ContractRoomRateTaxPercentage = GlobalCode.Field2Double(a["ContractRoomRateTaxPercentage"]).ToString("N2"),
                              IsBreakfast = GlobalCode.Field2Bool(a["colBreakfastBit"]),
                              IsDinner = GlobalCode.Field2Bool(a["colDinnertBit"]),
                              IsLunch = GlobalCode.Field2Bool(a["colLunchBit"]),
                              IsWithShuttle = GlobalCode.Field2Bool(a["colWithShuttleBit"]),
                              RoomTypeID = GlobalCode.Field2Int(a["RoomTypeID"]),

                              CurrencyID = GlobalCode.Field2Int(a["CurrencyID"]),

                              ContractDateStarted = GlobalCode.Field2DateTime(a["colContractDateStartedDate"]).ToShortDateString(),
                              ContractDateEnd = GlobalCode.Field2DateTime(a["colContractDateEndDate"]).ToShortDateString(),

                              ATTEMail = (from n in dt.Tables[1].AsEnumerable()
                                          select new ATTEMail
                                          {
                                              BranchID = GlobalCode.Field2String(n["BranchID"]),
                                              BranchCode = GlobalCode.Field2String(n["BranchCode"]),
                                              BranchName = GlobalCode.Field2String(n["BranchName"]),
                                              Email = GlobalCode.Field2String(n["Email"]),
                                          }).ToList(),

                          }).ToList();


                return SF;
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

        public string SeafarerSaveRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
            double ContractedRate, string EmailTO, string HotelCity, short ReqSource,
            string ContactName, string ContactNo, string Recipient,
            string CCEmail, string BlindCopy, double RateConfirm, bool IsMedical,long TransHotelID , List<HotelRequestCompanion> HRC)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {


                GlobalCode gc = new GlobalCode();
                DataTable dt = new DataTable();

                dt = gc.getDataTable(HRC);

                dt.Columns.Remove("GENDER");
                dt.Columns.Remove("RECORDLOCATOR");
                dt.Columns.Remove("IsPortAgent"); 

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertCrewAssistHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestNo", DbType.String, RequestNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerIDInt", DbType.Int32, Int32.Parse(SfID));
                SFDatebase.AddInParameter(SFDbCommand, "@pLastNameVarchar", DbType.String, LastName);
                SFDatebase.AddInParameter(SFDbCommand, "@pFirstNameVarchar", DbType.String, FirstName);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Int32.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.Int32, Int32.Parse(AirportID));
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.Int32, Int32.Parse(HotelID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckinDate", DbType.DateTime, CheckinDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckoutDate", DbType.DateTime, CheckoutDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pNoNitesInt", DbType.Int32, Int32.Parse(NoNites));
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeId", DbType.String, RoomType);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealBreakfastBit", DbType.Boolean, MealBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealLunchBit", DbType.Boolean, MealLunch);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealDinnerBit", DbType.Boolean, MealDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealLunchDinnerBit", DbType.Boolean, MealLunchDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithShuttleBit", DbType.Boolean, WithShuttle);

                SFDatebase.AddInParameter(SFDbCommand, "@pRankIDInt", DbType.Int32, Int32.Parse(RankID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselInt", DbType.Int32, Int32.Parse(VesselInt));
                SFDatebase.AddInParameter(SFDbCommand, "@pCostCenterInt", DbType.Int32, Int32.Parse(CostCenter));
                SFDatebase.AddInParameter(SFDbCommand, "@pCommentsVarchar", DbType.String, Comments);
                SFDatebase.AddInParameter(SFDbCommand, "@pSfStatus", DbType.String, SfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeIn", DbType.String, TimeIn);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeOut", DbType.String, TimeOut);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmount", DbType.String, RoomAmount);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxInclusive", DbType.Boolean, IsRoomTax);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxPercentage", DbType.Double, GlobalCode.Field2Double(RoomTaxPercent));

                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTrID", DbType.String, TrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCurrency", DbType.String, Currency);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, strLogDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, strFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, strPageName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAirBit", DbType.Boolean, IsAir);
                SFDatebase.AddInParameter(SFDbCommand, "@pSequentNoInt", DbType.Int32, SequentNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigInt", DbType.Int64, IDBig);
                SFDatebase.AddInParameter(SFDbCommand, "@pBrandIDInt", DbType.Int32, BrandID);

                SFDatebase.AddInParameter(SFDbCommand, "@pMealVoucherMoney", DbType.Double, MealVoucher);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmRateMoney", DbType.Double, ConfirmRate);
                SFDatebase.AddInParameter(SFDbCommand, "@pContractedRateMoney", DbType.Double, ContractedRate);

                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTO", DbType.String, EmailTO);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelCity", DbType.String, HotelCity);
                SFDatebase.AddInParameter(SFDbCommand, "@pReqSource", DbType.Int16, ReqSource);

                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pCCEmail", DbType.String, CCEmail);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);
                SFDatebase.AddInParameter(SFDbCommand, "@pRateConfirm", DbType.Double, RateConfirm);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsMedicalBit", DbType.Boolean, IsMedical);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelID", DbType.Int64, TransHotelID);

                SqlParameter param = new SqlParameter("@pCompanionTable", dt);

                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;

                SFDbCommand.Parameters.Add(param);

                string sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                trans.Commit();

                return sHRID;
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }

             

            }
        }


        /// <summary>
        // =============================================
        // Author:			Muhallididn G Wali
        // Create date:		04/02/2013
        // Description:		Submit SF Hotel Request. Booked and add in TblHotelTransactionOther
        // =============================================
        /// </summary>
        public string SendHotelTransactionOtherRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName
            , string Reciever, string Recipient, string BlindCopy, double ConfirmRate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();

            DataTable dt = new DataTable();
            DbCommand SFDbCommand = null;
            string res = "";
            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            try
            {
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelTransactionOtherRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, GlobalCode.Field2Int(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, CommonFunctions.GetDateTimeGMT(currentDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, currentDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, Reciever);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmRate", DbType.Double, ConfirmRate);


                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;


            }
            catch (Exception ex)
            {
                //trans.Rollback();
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }


        /// <summary>
        // =============================================
        // Author:			Muhallididn G Wali
        // Create date:		04/02/2013
        // Description:		Submit SF Hotel Request. Booked and add in TblHotelTransactionOther
        // =============================================
        /// </summary>
        public string SendHotelTransactionPortAgentRequest(long HotelTransID, long RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName
            , string Reciever, string Recipient, string BlindCopy)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();

            DataTable dt = new DataTable();
            DbCommand SFDbCommand = null;
            string res = "";
            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            try
            {
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelTransactionPortAgentRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelTransIDBigInt", DbType.Int64, HotelTransID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int64, RequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, CommonFunctions.GetDateTimeGMT(currentDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, currentDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, Reciever);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;

            }
            catch (Exception ex)
            {
                //trans.Rollback();
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        /// <summary>
        // =============================================
        // Author:			Muhallididn G Wali
        // Create date:		04/02/2013
        // Description:		Submit SF Hotel Request. Booked and add in TblHotelTransactionOther
        // =============================================
        /// </summary>
        public List<CrewAssistEmailDetail> SendHotelTransactionOtherRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();

            DataSet ds = new DataSet();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            try
            {
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelTransactionOtherRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, GlobalCode.Field2Int(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, CommonFunctions.GetDateTimeGMT(currentDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, currentDate);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                var SF = (from a in ds.Tables[0].AsEnumerable()
                          select new CrewAssistEmailDetail
                          {

                              SeafarerID = GlobalCode.Field2String(a["colSeafarerIDInt"]),
                              LastName = GlobalCode.Field2String(a["LastName"]),
                              FirstName = GlobalCode.Field2String(a["FirstName"]),
                              GenderDiscription = GlobalCode.Field2String(a["Gender"]),
                              BrandCode = GlobalCode.Field2String(a["Branch"]),
                              RankName = GlobalCode.Field2String(a["Rank"]),
                              SFStatus = GlobalCode.Field2String(a["Status"]),
                              Nationality = GlobalCode.Field2String(a["Nationality"]),
                              VesselName = GlobalCode.Field2String(a["Vessel"]),
                              VesselId = GlobalCode.Field2Int(a["colVesselIdInt"]),
                              CostCenterCode = GlobalCode.Field2String(a["CostCenter"]),
                              RoomDesc = GlobalCode.Field2String(a["RoomType"]),
                              SharingWith = GlobalCode.Field2String(a["SharingWith"]),

                              TimeSpanStartDate = String.Format("{0:dd MMMM yyyy}", GlobalCode.Field2DateTime(a["TimeSpanStartDate"])),
                              TimeSpanEndDate = String.Format("{0:dd MMMM yyyy}", GlobalCode.Field2DateTime(a["colTimeSpanEndDate"])),

                              TimeSpanStartTime = GlobalCode.Field2String(a["TimeSpanStartTime"]),
                              TimeSpanEndTime = GlobalCode.Field2String(a["TimeSpanEndTime"]),
                              NoOfNite = GlobalCode.Field2String(a["NoOfNite"]),
                              Mealvoucheramount = GlobalCode.Field2Double(a["Mealvoucheramount"]).ToString("n2"),
                              Confirmedbyhotelvendor = GlobalCode.Field2Double(a["Confirmedbyhotelvendor"]).ToString(),
                              ConfirmedbyRCCL = GlobalCode.Field2String(a["ConfirmedbyRCCL"]),
                              VendorBranch = GlobalCode.Field2String(a["colVendorBranchNameVarchar"]),
                              Roomrate = GlobalCode.Field2Double(a["Roomrate"]).ToString("n2"),// GlobalCode.Field2String(a["Roomrate"])
                              Comment = GlobalCode.Field2String(a["colCommentsVarchar"]),
                              RecordLocator = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),


                              CrewAssistEmailAirDetail = (from n in ds.Tables[1].AsEnumerable()
                                                          select new CrewAssistEmailAirDetail
                                                          {
                                                              AirDetail = GlobalCode.Field2String(n["AirDetail"])
                                                          }).ToList()

                          }).ToList();


                return SF;

            }
            catch (Exception ex)
            {
                //trans.Rollback();
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }


        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public void SeafarerSaveComapnionRequest(string RequestDetailID, string RequestID, string LastName, string FirstName, string Relationship, string Gender, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertSFCompanionHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestDetailID", DbType.Int32, GlobalCode.Field2Int(RequestDetailID));
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestID", DbType.Int32, GlobalCode.Field2Int(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pLastNameVarchar", DbType.String, LastName);
                SFDatebase.AddInParameter(SFDbCommand, "@pFirstNameVarchar", DbType.String, FirstName);
                SFDatebase.AddInParameter(SFDbCommand, "@pRelationshipVarchar", DbType.String, Relationship);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Int32.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
               
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



        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public void SeafarerSaveComapnionRequest(ref DataTable dt, string RequestDetailID, string RequestID
                        , string LastName, string FirstName, string Relationship
                        , string Gender, string UserID, long TravelRequestID, long IDBignt, int SeqNoInt)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertSFCompanionHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestDetailID", DbType.Int64, GlobalCode.Field2Int(RequestDetailID));
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestID", DbType.Int64, GlobalCode.Field2Int(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pLastNameVarchar", DbType.String, LastName);
                SFDatebase.AddInParameter(SFDbCommand, "@pFirstNameVarchar", DbType.String, FirstName);
                SFDatebase.AddInParameter(SFDbCommand, "@pRelationshipVarchar", DbType.String, Relationship);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Int32.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);


                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBignt", DbType.Int64, IDBignt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, SeqNoInt);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];


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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

         

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// ----------------------------------------------------
        /// Date Modified: 25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get RoomType ID from RoomTypeID instead of colRoomTypeId
        ///                Get MealVoucherMoney from MealVoucher instead of colMealVoucherMoney
        /// </summary>
        public List<crewassistrequest> GetHotelRequest(Int16 LoadType, long RequestID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = new DataTable();
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestID", DbType.Int64, RequestID);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                var SF = (from a in dt.AsEnumerable()
                          select new crewassistrequest
                          {
                              RequestID = GlobalCode.Field2Long(a["colRequestIDBigint"]),
                              SeafarerIDInt = GlobalCode.Field2Long(a["colSeafarerIDInt"]),
                              LastNameVarchar = GlobalCode.Field2String(a["colLastNameVarchar"]),
                              FirstNameVarchar = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                              Gender = GlobalCode.Field2String(a["colGender"]),
                              RegionIDInt = GlobalCode.Field2Int(a["colRegionIDInt"]),
                              PortIDInt = GlobalCode.Field2Int(a["colPortIDInt"]),
                              AirportIDInt = GlobalCode.Field2Int(a["colAirprotIDInt"]),
                              HotelIDInt = GlobalCode.Field2Int(a["colHotelIdInt"]),
                              CheckinDate = GlobalCode.Field2DateTime(a["colCheckinDate"]),
                              CheckoutDate = GlobalCode.Field2DateTime(a["colCheckoutDate"]),
                              NoNitesInt = GlobalCode.Field2Int(a["colNoNitesInt"]),
                              RoomTypeID = GlobalCode.Field2Int(a["RoomTypeID"]),
                              MealBreakfastBit = GlobalCode.Field2Bool(a["colMealBreakfastBit"]),
                              MealLunchBit = GlobalCode.Field2Bool(a["colMealLunchBit"]),
                              MealDinnerBit = GlobalCode.Field2Bool(a["colMealDinnerBit"]),
                              MealLunchDinnerBit = GlobalCode.Field2Bool(a["colMealLunchDinnerBit"]),
                              WithShuttleBit = GlobalCode.Field2Bool(a["colWithShuttleBit"]),
                              RankIDInt = GlobalCode.Field2Int(a["colRankIDInt"]),
                              VesselInt = GlobalCode.Field2Int(a["colVesselInt"]),
                              CostCenterInt = GlobalCode.Field2Int(a["colCostCenterInt"]),
                              CommentsVarchar = GlobalCode.Field2String(a["colCommentsVarchar"]),
                              SfStatus = GlobalCode.Field2String(a["colSFStatus"]),
                              TimeIn = GlobalCode.Field2String(a["colCheckinTime"]),
                              TimeOut = GlobalCode.Field2String(a["colCheckoutTime"]),
                              RoomAmount = GlobalCode.Field2String(a["colRoomAmountMoney"]),
                              RoomRateTaxInclusive = GlobalCode.Field2Bool(a["colRoomRateTaxInclusive"]),
                              RoomRateTaxPercentage = GlobalCode.Field2Double(a["colRoomRateTaxPercentage"]),
                              TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),
                              Currency = GlobalCode.Field2Int(a["colCurrency"]),
                              isAir = GlobalCode.Field2Bool(a["colIsAir"]),
                              SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                              IDBigInt = GlobalCode.Field2Long(a["colIDBigInt"]),


                              MealVoucherMoney = GlobalCode.Field2Double(a["MealVoucher"]),
                              ConfirmRateMoney = GlobalCode.Field2Double(a["colConfirmRateMoney"]),
                              ContractedRateMoney = GlobalCode.Field2Double(a["colContractedRateMoney"]),
                              HotelEmail = GlobalCode.Field2String(a["colEmailToVarchar"]),

                              HotelComment = GlobalCode.Field2String(a["colCommentsVarchar"]),

                              TransVehicleIDBigint = GlobalCode.Field2Long(a["colReqVehicleIDBigint"]),

                          }).ToList();
                return SF;


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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:		    Muhallidin G Wali
        /// Create date:    09/26/2013
        /// Description:	Add Copy Email 
        /// </summary>

        public void TblEmail(int EmailTypeID, string EmailType, string Email, long EmailFromID, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspTblEmail");

                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTypeInt", DbType.Int32, EmailTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTypeVarchar", DbType.String, EmailType);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailVarchar", DbType.String, Email);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTypeIDBigInt", DbType.Int64, EmailFromID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

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

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public List<CopyEmail> GetTblEmail(Int16 LoadType, Int16 EmailTypeID, long EmailHotelID, long EmailCrewAssistID, long EmailSchedulerID, long EmailShipID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = new DataTable();
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetTblEmail");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTypeID", DbType.Int16, EmailTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailHotelID", DbType.Int64, EmailHotelID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailCrewAssistID", DbType.Int64, EmailCrewAssistID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailSchedulerID", DbType.Int64, EmailSchedulerID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailShipID", DbType.Int64, EmailShipID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                var SF = (from a in dt.AsEnumerable()
                          select new CopyEmail
                          {
                              EmailType = GlobalCode.Field2Int(a["EmailType"]),
                              Email = GlobalCode.Field2String(a["Email"]),
                          }).ToList();
                return SF;


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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Get Vehicle
        /// </summary>
        public List<VehicleVendor> GetVehicleVendor(Int16 LoadType, string portCode)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = new DataTable();
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("GetVehicleVendor");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, portCode);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                var SF = (from a in dt.AsEnumerable()
                          select new VehicleVendor
                          {
                              ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                              VehicleID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                              Vehicle = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),
                              IsAirport = GlobalCode.Field2Bool(a["IsAirPort"]),
                          }).ToList();
                return SF;


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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        public void email_send()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("your mail@gmail.com");
            mail.To.Add("to_mail@gmail.com");
            mail.Subject = "Test Mail - 1";
            mail.Body = "mail with attachment";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("your mail@gmail.com", "your password");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }


        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Crew assist transportation Request
        /// </summary>
        public string SaveVehicleRequest(List<CrewAssistTranspo> list)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("upsSaveVehicleRequest");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqVehicleIDBigint", DbType.Int64, list[0].ReqVehicleIDBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, list[0].IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, list[0].TravelReqIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int16, list[0].SeqNoInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, list[0].RecordLocatorVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleVendorIDInt", DbType.Int32, list[0].VehicleVendorIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehiclePlateNoVarchar", DbType.String, list[0].VehiclePlateNoVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pPickUpDate", DbType.DateTime, list[0].PickUpDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pPickUpTime", DbType.DateTime, list[0].PickUpTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pDropOffDate", DbType.DateTime, list[0].DropOffDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pDropOffTime", DbType.DateTime, list[0].DropOffTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmationNoVarchar", DbType.String, list[0].ConfirmationNoVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleStatusVarchar", DbType.String, list[0].VehicleStatusVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleTypeIdInt", DbType.Int32, list[0].VehicleTypeIdInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, list[0].SFStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteIDFromInt", DbType.Int32, list[0].RouteIDFromInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteIDToInt", DbType.Int32, list[0].RouteIDToInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pFromVarchar", DbType.String, list[0].FromVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pToVarchar", DbType.String, list[0].ToVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, list[0].UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pComment", DbType.String, list[0].Comment);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAir", DbType.Boolean, list[0].IsAir);

                SFDatebase.AddInParameter(SFDbCommand, "@pRouteFrom", DbType.String, list[0].RouteFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteTo", DbType.String, list[0].RouteTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelID", DbType.Int32, list[0].HotelID);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmBy", DbType.String, list[0].ConfirmBy);

                string sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                return sHRID;

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

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Crew assist transportation Request
        /// </summary>
        public string SaveTransportationeReques(List<CrewAssistTranspo> list)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            try
            {
                List<CrewAssistTranspo> trans = new List<CrewAssistTranspo>();

                string sHRID = "";
                for (var i = 0; i < list.Count; i++)
                {

                    SFDbCommand = SFDatebase.GetStoredProcCommand("upsSaveVehicleRequest");

                    SFDatebase.AddInParameter(SFDbCommand, "@pReqVehicleIDBigint", DbType.Int64, list[i].ReqVehicleIDBigint);
                    SFDatebase.AddInParameter(SFDbCommand, "@pTransVehicleIDBigint", DbType.Int64, list[i].VehicleTransID);

                    SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, list[i].IdBigint);
                    SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, list[i].TravelReqIDInt);
                    SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int16, list[i].SeqNoInt);
                    SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, list[i].RecordLocatorVarchar);
                    SFDatebase.AddInParameter(SFDbCommand, "@pVehicleVendorIDInt", DbType.Int32, list[i].VehicleVendorIDInt);
                    SFDatebase.AddInParameter(SFDbCommand, "@pVehiclePlateNoVarchar", DbType.String, list[i].VehiclePlateNoVarchar);
                    SFDatebase.AddInParameter(SFDbCommand, "@pPickUpDate", DbType.DateTime, list[i].PickUpDate);
                    SFDatebase.AddInParameter(SFDbCommand, "@pPickUpTime", DbType.DateTime, list[i].PickUpTime);
                    SFDatebase.AddInParameter(SFDbCommand, "@pDropOffDate", DbType.DateTime, list[i].DropOffDate);
                    SFDatebase.AddInParameter(SFDbCommand, "@pDropOffTime", DbType.DateTime, list[i].DropOffTime);
                    SFDatebase.AddInParameter(SFDbCommand, "@pConfirmationNoVarchar", DbType.String, list[i].ConfirmationNoVarchar);
                    SFDatebase.AddInParameter(SFDbCommand, "@pVehicleStatusVarchar", DbType.String, list[i].VehicleStatusVarchar);
                    SFDatebase.AddInParameter(SFDbCommand, "@pVehicleTypeIdInt", DbType.Int32, list[i].VehicleTypeIdInt);
                    SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, list[i].SFStatus);
                    SFDatebase.AddInParameter(SFDbCommand, "@pRouteIDFromInt", DbType.Int32, list[i].RouteIDFromInt);
                    SFDatebase.AddInParameter(SFDbCommand, "@pRouteIDToInt", DbType.Int32, list[i].RouteIDToInt);
                    SFDatebase.AddInParameter(SFDbCommand, "@pFromVarchar", DbType.String, list[i].FromVarchar);
                    SFDatebase.AddInParameter(SFDbCommand, "@pToVarchar", DbType.String, list[i].ToVarchar);
                    SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, list[i].UserID);
                    SFDatebase.AddInParameter(SFDbCommand, "@pComment", DbType.String, list[i].Comment);
                    SFDatebase.AddInParameter(SFDbCommand, "@pIsAir", DbType.Boolean, list[i].IsAir);

                    SFDatebase.AddInParameter(SFDbCommand, "@pRouteFrom", DbType.String, list[i].RouteFrom);
                    SFDatebase.AddInParameter(SFDbCommand, "@pRouteTo", DbType.String, list[i].RouteTo);
                    SFDatebase.AddInParameter(SFDbCommand, "@pHotelID", DbType.Int32, list[i].HotelID);
                    SFDatebase.AddInParameter(SFDbCommand, "@pConfirmBy", DbType.String, list[i].ConfirmBy);
                    SFDatebase.AddInParameter(SFDbCommand, "@pIsPortAgent", DbType.String, list[i].IsPortAgent);
                    SFDatebase.AddInParameter(SFDbCommand, "@pEmailTo", DbType.String, list[i].Email);
                    SFDatebase.AddInParameter(SFDbCommand, "@pConfirmRate", DbType.String, list[i].ConfirmRate);
                    SFDatebase.AddInParameter(SFDbCommand, "@pReqSourceID", DbType.Int16, list[i].ReqSourceID);

                    sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();



                }

                return sHRID;

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



        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist transportation Request
        /// </summary>
        public string InsertVehicleRequest(List<CrewAssistTranspo> list)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("upsInsertVehicleRequest");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqVehicleIDBigint", DbType.Int64, list[0].ReqVehicleIDBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, list[0].IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, list[0].TravelReqIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int16, list[0].SeqNoInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, list[0].RecordLocatorVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleVendorIDInt", DbType.Int32, list[0].VehicleVendorIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehiclePlateNoVarchar", DbType.String, list[0].VehiclePlateNoVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pPickUpDate", DbType.DateTime, list[0].PickUpDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pPickUpTime", DbType.DateTime, list[0].PickUpTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pDropOffDate", DbType.DateTime, list[0].DropOffDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pDropOffTime", DbType.DateTime, list[0].DropOffTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmationNoVarchar", DbType.String, list[0].ConfirmationNoVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleStatusVarchar", DbType.String, list[0].VehicleStatusVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleTypeIdInt", DbType.Int32, list[0].VehicleTypeIdInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, list[0].SFStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteIDFromInt", DbType.Int32, list[0].RouteIDFromInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteIDToInt", DbType.Int32, list[0].RouteIDToInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pFromVarchar", DbType.String, list[0].FromVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pToVarchar", DbType.String, list[0].ToVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, list[0].UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pComment", DbType.String, list[0].Comment);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAir", DbType.Boolean, list[0].IsAir);

                SFDatebase.AddInParameter(SFDbCommand, "@pRouteFrom", DbType.String, list[0].RouteFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteTo", DbType.String, list[0].RouteTo);

                string sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                return sHRID;

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



        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public List<CrewAssistTranspo> SendVehicleTransactionRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName
            )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;
            try
            {


                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSendVehicleTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, GlobalCode.Field2Int(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, CommonFunctions.GetDateTimeGMT(currentDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, currentDate);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];


                var SF = (from a in dt.AsEnumerable()
                          select new CrewAssistTranspo
                          {

                              IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                              TravelReqIDInt = GlobalCode.Field2Long(a["colTravelReqIDInt"]),
                              SeqNoInt = GlobalCode.Field2TinyInt(a["colSeqNoInt"]),
                              RecordLocatorVarchar = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),
                              VehicleVendorIDInt = GlobalCode.Field2Long(a["colVehicleVendorIDInt"]),
                              VehicleVendor = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),

                              VehiclePlateNoVarchar = GlobalCode.Field2String(a["colVehiclePlateNoVarchar"]),
                              PickUpDate = GlobalCode.Field2DateTime(a["colPickUpDate"]),
                              PickUpTime = GlobalCode.Field2DateTime(a["colPickUpTime"]),
                              DropOffDate = GlobalCode.Field2DateTime(a["colDropOffDate"]),
                              DropOffTime = GlobalCode.Field2DateTime(a["colDropOffTime"]),
                              ConfirmationNoVarchar = GlobalCode.Field2String(a["colConfirmationNoVarchar"]),
                              VehicleStatusVarchar = GlobalCode.Field2String(a["colVehicleStatusVarchar"]),
                              VehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                              SFStatus = GlobalCode.Field2String(a["colSFStatus"]),
                              RouteIDFromInt = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                              RouteIDToInt = GlobalCode.Field2Int(a["colRouteIDToInt"]),
                              FromVarchar = GlobalCode.Field2String(a["colFromVarchar"]),
                              ToVarchar = GlobalCode.Field2String(a["colToVarchar"]),
                              Comment = GlobalCode.Field2String(a["colRemarksForAuditVarchar"]),
                              CostCenter = GlobalCode.Field2String(a["colCostCenterNameVarchar"]),
                              TransSender = UserID,

                              Ship = GlobalCode.Field2String(a["colVesselNameVarchar"]),
                              SeaparerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                              FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                              LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                              RankName = GlobalCode.Field2String(a["RankName"]),
                              Gender = GlobalCode.Field2String(a["colGenderDiscription"]),
                              NationalityName = GlobalCode.Field2String(a["Nationality"]),

                              RouteFrom = GlobalCode.Field2String(a["colRouteFromVarchar"]),
                              RouteTo = GlobalCode.Field2String(a["colRouteToVarchar"])

                          }).ToList();

                return SF;

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

                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public string SendVehicleTransactionRequest(string RequestID, string UserID,
            string ContactName, string ContactNo, string Description, string Function,
            string FileName, string Reciever, string Recipient, string BlindCopy, bool IsPortAgent)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt;
            DbCommand SFDbCommand = null;
            string res = "";
            try
            {

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSendVehicleTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, GlobalCode.Field2Int(RequestID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactName", DbType.String, ContactName);
                SFDatebase.AddInParameter(SFDbCommand, "@pContactNo", DbType.String, ContactNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, CommonFunctions.GetDateTimeGMT(currentDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, currentDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, Reciever);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsPortAgent", DbType.String, IsPortAgent);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;

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

        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Hotel, Port, ExpendType
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<VehicleVendor> GetVendorVehicleDetail(short LoadType, long VehicleVendorID, string PortCode)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("upsGetVendorVehicleDetail");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleVendorID", DbType.Int64, VehicleVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                List<VehicleVendor> VehicleVendor = new List<VehicleVendor>();

                VehicleVendor = (from a in ds.Tables[0].AsEnumerable()
                                 select new VehicleVendor
                                 {
                                     VehicleID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                     Email = a["colEmailToVarchar"].ToString(),
                                     VenConfirm = a["colContactPersonVarchar"].ToString(),
                                     Address = a["colAddressVarchar"].ToString(),
                                     Telephone = a["colContactNoVarchar"].ToString(),

                                     VehicleCost = (from n in ds.Tables[1].AsEnumerable()
                                                    select new CrewAssisVehicleCost
                                                    {
                                                        PortCode = n["PortCode"].ToString(),
                                                        CurrencyCode = n["CurrencyCode"].ToString(),
                                                        Route = n["Route"].ToString(),
                                                        VehicleTypeName = n["VehicleTypeName"].ToString(),
                                                        VehicleTypeID = GlobalCode.Field2Int(n["VehicleTypeID"]),

                                                        UnitOfMeasure = n["UnitOfMeasure"].ToString(),
                                                        TranspoCost = GlobalCode.Field2Double(n["Cost"]).ToString("N2"),
                                                        Capacity = n["Capacity"].ToString(),
                                                        VacantSeat = n["VacantSeat"].ToString(),

                                                    }).ToList()
                                 }).ToList();

                return VehicleVendor;
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
                if (ds != null)
                {
                    ds.Dispose();
                }

            }
        }




        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Hotel, Port, ExpendType
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssistTranspo> getVehicleRequest(short LoadType, long VehicleVendorID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt;
            DataSet ds = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("getVehicleRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@LoadType", DbType.String, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@ReqVehicleIDBigint", DbType.Int64, VehicleVendorID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];


                var SF = (from a in dt.AsEnumerable()
                          select new CrewAssistTranspo
                          {
                              ReqVehicleIDBigint = GlobalCode.Field2Long(a["colReqVehicleIDBigint"]),
                              IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                              TravelReqIDInt = GlobalCode.Field2Long(a["colTravelReqIDInt"]),
                              SeqNoInt = GlobalCode.Field2TinyInt(a["colSeqNoInt"]),
                              RecordLocatorVarchar = GlobalCode.Field2String(a["colRecordLocatorVarchar"]),
                              VehicleVendorIDInt = GlobalCode.Field2Long(a["colVehicleVendorIDInt"]),
                              VehiclePlateNoVarchar = GlobalCode.Field2String(a["colVehiclePlateNoVarchar"]),
                              PickUpDate = GlobalCode.Field2DateTime(a["colPickUpDate"]),
                              PickUpTime = GlobalCode.Field2DateTime(a["colPickUpTime"]),
                              DropOffDate = GlobalCode.Field2DateTime(a["colDropOffDate"]),
                              DropOffTime = GlobalCode.Field2DateTime(a["colDropOffTime"]),
                              ConfirmationNoVarchar = GlobalCode.Field2String(a["colConfirmationNoVarchar"]),
                              VehicleStatusVarchar = GlobalCode.Field2String(a["colVehicleStatusVarchar"]),
                              VehicleTypeIdInt = GlobalCode.Field2Int(a["colVehicleTypeIdInt"]),
                              SFStatus = GlobalCode.Field2String(a["colSFStatus"]),
                              RouteIDFromInt = GlobalCode.Field2Int(a["colRouteIDFromInt"]),
                              RouteIDToInt = GlobalCode.Field2Int(a["colRouteIDToInt"]),
                              FromVarchar = GlobalCode.Field2String(a["colFromVarchar"]),
                              ToVarchar = GlobalCode.Field2String(a["colToVarchar"]),
                              Comment = GlobalCode.Field2String(a["colRemarksForAuditVarchar"]),
                              Email = GlobalCode.Field2String(a["colEmailToVarchar"])
                          }).ToList();

                return SF;
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
                if (ds != null)
                {
                    ds.Dispose();
                }

            }
        }



        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Gett Visa nationality
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public string GetNationalityVisa(short LoadType, int NationalityIDInt, int VisitVisaIDInt)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            //DataSet ds = null;
            string Required = "";

            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVisa");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationalityIDInt", DbType.Int32, NationalityIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pVisitVisaIDInt", DbType.Int32, VisitVisaIDInt);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    Required = dt.Rows[0]["colRequiredVarchar"].ToString();
                }
                return Required;
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
                if (dt != null)
                {
                    dt.Dispose();
                }

            }
        }
         


        /// <summary>
        /// Date Created:    17/10/2013
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Airport, Seaport
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssitPortList> GetPort(short LoadType, string userString)
        {
            DataSet ds = null;
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            List<CrewAssitPortList> CrewAssistPort = new List<CrewAssitPortList>();
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("upsGetPort");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadTypeInt", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                CrewAssistPort = (from a in ds.Tables[0].AsEnumerable()
                                  select new CrewAssitPortList
                                  {
                                      PortId = GlobalCode.Field2Int(a["PORTID"]),
                                      PortName = a["PORT"].ToString()
                                  }).ToList();
                return CrewAssistPort;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:    17/10/2013
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Airport, Seaport
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssitPortListClass> GetPort(string userString)
        {
            DataSet ds = null;
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            List<CrewAssitPortListClass> PortListClass = new List<CrewAssitPortListClass>();
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("upsGetPort");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadTypeInt", DbType.Int16, 2);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                PortListClass.Add(new CrewAssitPortListClass
                {

                    SeaportList = (from a in ds.Tables[0].AsEnumerable()
                                   select new CrewAssitPortList
                                   {
                                       PortId = GlobalCode.Field2Int(a["PORTID"]),
                                       PortName = a["PORT"].ToString(),
                                       PortCode = a["PORTCODE"].ToString()
                                   }).ToList(),

                    AirportList = (from a in ds.Tables[1].AsEnumerable()
                                   select new CrewAssitPortList
                                   {
                                       PortId = GlobalCode.Field2Int(a["PORTID"]),
                                       PortName = a["PORT"].ToString(),
                                       PortCode = a["PORTCODE"].ToString()
                                   }).ToList()

                });

                return PortListClass;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:    17/10/2013
        /// Created By:      Muhallidin G Wali
        /// (description)    Select meet and greet Airport
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<MeetAndGreetGenericClass> GetMeetAndGreetAirport(short LoadType, int MeetAndGreetID)
        {
            DataSet ds = null;
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            List<MeetAndGreetGenericClass> MeetAndGreetClass = new List<MeetAndGreetGenericClass>();
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetMeetAndGreetAirport");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pMeetAndGreetID", DbType.Int32, MeetAndGreetID);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                MeetAndGreetClass.Add(new MeetAndGreetGenericClass
                {
                    CrewAssistMeetAndGreet = (from a in ds.Tables[0].AsEnumerable()
                                              select new CrewAssistMeetAndGreetVendor
                                      {
                                          MeetAndGreetVendor = GlobalCode.Field2String(a["colMeetAndGreetVendorNameVarchar"]),
                                          Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                                          FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                                          Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
                                          Rate = GlobalCode.Field2String(a["Rate"]),
                                          ServiceDate = GlobalCode.Field2DateTime(a["ServiceDate"]),
                                          FlightInfo = GlobalCode.Field2String(a["FlightInfo"]),
                                          ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                                          EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),

                                      }).ToList(),
                    ComboGenericClass = (from n in ds.Tables[1].AsEnumerable()
                                         select new ComboGenericClass
                                        {
                                            ID = GlobalCode.Field2Int(n["AirportID"]),
                                            Name = n["Airport"].ToString(),
                                            NameCode = n["AirportCode"].ToString()
                                        }).ToList()
                });


                return MeetAndGreetClass;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SaveMeetAndGreetRequest(List<CrewAssistMeetAndGreet> list)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("upsInsertMeetAndGreetRequest");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqMeetAndGreetID", DbType.Int64, list[0].ReqMeetAndGreetID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, list[0].IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, list[0].TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, list[0].SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, list[0].RecordLocator);
                SFDatebase.AddInParameter(SFDbCommand, "@pMeetAndGreetVendorIDInt", DbType.Int32, list[0].MeetAndGreetVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmationNoVarchar", DbType.String, list[0].ConfirmationNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportInt", DbType.Int32, list[0].AirportID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFligthNoVarchar", DbType.String, list[0].FligthNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirlineCode", DbType.String, list[0].AirportCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pArrTime", DbType.DateTime, list[0].ArrTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pDeptTime", DbType.DateTime, list[0].DeptTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pServiceDatetime", DbType.DateTime, list[0].ServiceDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pRateFloat", DbType.Double, list[0].Rate);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, list[0].SFStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, list[0].UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pComment", DbType.String, list[0].Comment);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAir", DbType.Boolean, list[0].IsAir);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmBy", DbType.String, list[0].ConfirmBy);

                string IdentityID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                return IdentityID;

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

        /// <summary>
        /// Date Create:    22/10/2013
        /// Create By:      Muhallidin G Wali
        /// (description)  	get port agent detail
        /// </summary>
        public List<CrewAssistVendorPortAgent> GetVendorPortAgent(short LoadType, long PortAgentVendorID)
        {
            List<CrewAssistVendorPortAgent> list = new List<CrewAssistVendorPortAgent>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();
            DbCommand dbComm = null;
            //DataTable dt = null;
            try
            {

                dbComm = db.GetStoredProcCommand("uspGetVendorPortAgent");
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbComm, "@pPortAgentVendorID", DbType.Int64, PortAgentVendorID);

                //dt = db.ExecuteDataSet(dbComm).Tables[0];
                ds = db.ExecuteDataSet(dbComm);//.Tables[0];


                list = (from a in ds.Tables[0].AsEnumerable()
                        select new CrewAssistVendorPortAgent
                        {
                            PortAgentVendorID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                            PortAgentVendorName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),
                            CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                            ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                            FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                            ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                            Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                            EmailCc = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                            EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                            Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),



                            PortAgentVehicle = (from n in ds.Tables[1].AsEnumerable()
                                                select new CrewAssisVehicleCost
                                                {

                                                    PortCode = n["PortCode"].ToString(),
                                                    CurrencyCode = n["CurrencyCode"].ToString(),
                                                    Route = n["Route"].ToString(),
                                                    VehicleTypeName = n["VehicleTypeName"].ToString(),
                                                    VehicleTypeID = GlobalCode.Field2Int(n["VehicleTypeID"]),

                                                    UnitOfMeasure = n["UnitOfMeasure"].ToString(),
                                                    //TranspoCost = n["Cost"].ToString(),
                                                    TranspoCost = GlobalCode.Field2Double(n["Cost"]).ToString("N2"),
                                                    Capacity = n["Capacity"].ToString(),
                                                    VacantSeat = n["VacantSeat"].ToString(),

                                                }).ToList(),

                            PortAgentHotel = (from n in ds.Tables[2].AsEnumerable()
                                              select new CrewAssisPortAgentHotelCost
                                              {

                                                  PortCode = n["PortCode"].ToString(),
                                                  CurrencyCode = n["CurrencyCode"].ToString(),
                                                  PortAgentVendorID = GlobalCode.Field2Int(n["PortAgentVendorID"]),
                                                  PortAgentVendorName = n["PortAgentVendorName"].ToString(),
                                                  IsRateByPercentBit = GlobalCode.Field2Bool(n["colIsRateByPercentBit"]),
                                                  RoomCostPercent = GlobalCode.Field2Decimal(n["colRoomCostPercent"]),
                                                  RoomDoubleRate = GlobalCode.Field2Decimal(n["colRoomDoubleRate"]),

                                                  RoomSingleRate = GlobalCode.Field2Decimal(n["colRoomSingleRate"]),
                                                  MealStandardDecimal = GlobalCode.Field2Decimal(n["colMealStandardDecimal"]),
                                                  MealIncreasedDecimal = GlobalCode.Field2Decimal(n["colMealIncreasedDecimal"]),
                                                  SurchargeSingle = GlobalCode.Field2Decimal(n["colSurchargeSingle"]),
                                                  SurchargeDouble = GlobalCode.Field2Decimal(n["colSurchargeDouble"]),
                                                  ContractID = GlobalCode.Field2Int(n["colContractIdInt"]),


                                                  ContractRate = GlobalCode.Field2Decimal(n["ContractRate"]),
                                                  ConfirmRate = GlobalCode.Field2Decimal(n["ConfirmRate"]),
                                                  MealVoucher = GlobalCode.Field2Decimal(n["MealVoucher"]),




                                              }).ToList()

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SavePortAgentRequest(List<CrewAssistPortAgentRequest> list
                    , List<VehicleTransactionPortAgent> VehicleTranPortAgent, List<HotelTransactionPortAgent> HotelTranPortAgent)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                GlobalCode gc = new GlobalCode();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                dt = gc.getDataTable(VehicleTranPortAgent);
                dt1 = gc.getDataTable(HotelTranPortAgent);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertPortAgentRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pReqPortAgentIDBigint", DbType.Int64, list[0].ReqPortAgentID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, list[0].IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, list[0].TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, list[0].SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, list[0].RecordLocator);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortAgentVendorIDInt", DbType.Int64, list[0].PortAgentVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortInt", DbType.Int32, list[0].PortCodeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, list[0].PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.Int32, list[0].AirportID);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportCode", DbType.String, list[0].AirportCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pFligthNoVarchar", DbType.String, list[0].FligthNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pServiceDatetime", DbType.DateTime, list[0].ServiceDatetime);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, list[0].SFStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pCommentVarchar", DbType.String, list[0].Comment);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsMAGBit", DbType.Boolean, list[0].IsMAG);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsTransBit", DbType.Boolean, list[0].IsTrans);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsHotelBit", DbType.Boolean, list[0].IsHotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsLuggageBit", DbType.Boolean, list[0].IsLuggage);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsSafeguardBit", DbType.Boolean, list[0].IsSafeguard);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsVisaBit", DbType.Boolean, list[0].IsVisa);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsOtherBit", DbType.Boolean, list[0].IsOther);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, list[0].UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAirBit", DbType.Boolean, list[0].IsAir);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmBy", DbType.String, list[0].ConfirmBy);

                SFDatebase.AddInParameter(SFDbCommand, "@IsPhoneCardBit", DbType.Boolean, list[0].IsPhoneCard); 
                SFDatebase.AddInParameter(SFDbCommand, "@PhoneCardMoney", DbType.Currency, list[0].PhoneCard);
                SFDatebase.AddInParameter(SFDbCommand, "@IsLaundryBit", DbType.Boolean, list[0].IsLaundry);
                SFDatebase.AddInParameter(SFDbCommand, "@LaundryMoney", DbType.Currency, list[0].Laundry);
                SFDatebase.AddInParameter(SFDbCommand, "@IsGiftCardBit", DbType.Boolean, list[0].IsGiftCard);
                SFDatebase.AddInParameter(SFDbCommand, "@GiftCardMoney", DbType.Currency, list[0].GiftCard);  


                string IdentityID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                return IdentityID;

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


        /// <summary>
        /// Date Create: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the meet and greet Request to table MeetAndGreetTransaction
        /// </summary>
        public List<CrewAssistMeetAndGreet> SendMeetAndGreetTransactionRequest(long MeetAndGreetID, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {

                List<CrewAssistMeetAndGreet> myList = new List<CrewAssistMeetAndGreet>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSaveMeetAndGreetTransaction");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqMeetAndGreetID", DbType.Int64, MeetAndGreetID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                myList = (from a in dt.AsEnumerable()
                          select new CrewAssistMeetAndGreet
                          {
                              IdBigint = GlobalCode.Field2Long(a["colIdBigint"]),
                              TravelReqID = GlobalCode.Field2Long(a["colTravelReqIDInt"]),
                              SeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                              RecordLocator = a["colRecordLocatorVarchar"].ToString(),
                              ReqMeetAndGreetID = GlobalCode.Field2Long(a["colReqMeetAndGreetIDBigint"]),
                              MeetAndGreetVendor = GlobalCode.Field2String(a["colMeetAndGreetVendorNameVarchar"]),
                              MeetAndGreetVendorID = GlobalCode.Field2Int(a["colMeetAndGreetVendorIDInt"]),
                              ConfirmationNo = a["colMeetAndGreetVendorIDInt"].ToString(),
                              AirportID = GlobalCode.Field2Int(a["colMeetAndGreetVendorIDInt"]),
                              AirportCode = a["colAirportCodeVarchar"].ToString(),
                              FligthNo = a["colFligthNoVarchar"].ToString(),
                              ArrTime = GlobalCode.Field2DateTime(a["colArrTime"]),
                              DeptTime = GlobalCode.Field2DateTime(a["colDeptTime"]),
                              ServiceDate = GlobalCode.Field2DateTime(a["colServiceDatetime"]),
                              Rate = GlobalCode.Field2Double(a["colRateFloat"]),
                              SFStatus = a["colSFStatus"].ToString(),
                              Comment = a["colCommentVarchar"].ToString(),
                              SeaparerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                              FirstName = a["colFirstNameVarchar"].ToString(),
                              LastName = a["colLastNameVarchar"].ToString() + ' ' + a["colMiddleNameVarchar"].ToString(),

                              Gender = a["Gender"].ToString(),
                              RankName = a["Rank"].ToString(),
                              NationalityName = a["Nationality"].ToString(),
                              Ship = a["Vessel"].ToString(),
                              CostCenter = a["CostCenter"].ToString(),
                              ConfirmBy = a["colConfirmByVarchar"].ToString(),

                          }).ToList();

                return myList;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Port agent Request to table PortAgentTransaction
        /// </summary>
        public List<CrewAssistPortAgentRequest> SendPortAgentTransactionRequest(long MeetAndGreetID, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {

                List<CrewAssistPortAgentRequest> myList = new List<CrewAssistPortAgentRequest>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSavePortAgentTransaction");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqPortAgentID", DbType.Int64, MeetAndGreetID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                myList = (from Q in dt.AsEnumerable()
                          select new CrewAssistPortAgentRequest
                          {

                              ReqPortAgentID = GlobalCode.Field2Long(Q["colReqPortAgentIDBigint"]),
                              IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                              TravelReqID = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                              SeqNo = GlobalCode.Field2Int(Q["colSeqNoInt"]),
                              RecordLocator = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),

                              PortAgentVendorID = GlobalCode.Field2Int(Q["colPortAgentVendorIDInt"]),
                              PortCodeID = GlobalCode.Field2Int(Q["colPortIdInt"]),
                              PortCode = GlobalCode.Field2String(Q["colPortCode"]),
                              AirportID = GlobalCode.Field2Int(Q["colAirportIDInt"]),
                              AirportCode = GlobalCode.Field2String(Q["colAirportCode"]),
                              FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                              ServiceDatetime = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                              SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                              Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),

                              IsMAG = GlobalCode.Field2Bool(Q["colIsMAGBit"]),
                              IsTrans = GlobalCode.Field2Bool(Q["colIsTransBit"]),
                              IsHotel = GlobalCode.Field2Bool(Q["colIsHotelBit"]),
                              IsLuggage = GlobalCode.Field2Bool(Q["colIsLuggageBit"]),
                              IsSafeguard = GlobalCode.Field2Bool(Q["colIsSafeguardBit"]),
                              IsVisa = GlobalCode.Field2Bool(Q["colIsVisaBit"]),
                              IsOther = GlobalCode.Field2Bool(Q["colIsOtherBit"]),
                              ConfirmBy = GlobalCode.Field2String(Q["colConfirmVarchar"]),

                          }).ToList();

                return myList;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }

        }




        /// <summary>
        /// Date Created: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Port agent Request to table PortAgentTransaction
        /// </summary>
        public string SendPortAgentTransactionRequest(long MeetAndGreetID, string UserID,
              string Reciever, string Recipient, string BlindCopy)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            string res = "";
            try
            {



                List<CrewAssistPortAgentRequest> myList = new List<CrewAssistPortAgentRequest>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSavePortAgentTransaction");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqPortAgentID", DbType.Int64, MeetAndGreetID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, Reciever);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);


                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;



                //myList = (from Q in dt.AsEnumerable()
                //          select new CrewAssistPortAgentRequest
                //          {

                //              ReqPortAgentID = GlobalCode.Field2Long(Q["colReqPortAgentIDBigint"]),
                //              IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                //              TravelReqID = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                //              SeqNo = GlobalCode.Field2Int(Q["colSeqNoInt"]),
                //              RecordLocator = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),

                //              PortAgentVendorID = GlobalCode.Field2Int(Q["colPortAgentVendorIDInt"]),
                //              PortCodeID = GlobalCode.Field2Int(Q["colPortIdInt"]),
                //              PortCode = GlobalCode.Field2String(Q["colPortCode"]),
                //              AirportID = GlobalCode.Field2Int(Q["colAirportIDInt"]),
                //              AirportCode = GlobalCode.Field2String(Q["colAirportCode"]),
                //              FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                //              ServiceDatetime = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                //              SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                //              Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),

                //              IsMAG = GlobalCode.Field2Bool(Q["colIsMAGBit"]),
                //              IsTrans = GlobalCode.Field2Bool(Q["colIsTransBit"]),
                //              IsHotel = GlobalCode.Field2Bool(Q["colIsHotelBit"]),
                //              IsLuggage = GlobalCode.Field2Bool(Q["colIsLuggageBit"]),
                //              IsSafeguard = GlobalCode.Field2Bool(Q["colIsSafeguardBit"]),
                //              IsVisa = GlobalCode.Field2Bool(Q["colIsVisaBit"]),
                //              IsOther = GlobalCode.Field2Bool(Q["colIsOtherBit"]),
                //              ConfirmBy = GlobalCode.Field2String(Q["colConfirmVarchar"]),




                //          }).ToList();

                //return myList;

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

        /// <summary>
        /// Date Created: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Port agent Request to table SaveRequestHotel
        /// </summary>
        public string SeafarerSaveRequestHotelOverflow(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
           string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
           bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
           string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
           string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
           string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
           bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
           double ContractedRate, string EmailTO, string HotelCity)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSaveOverFlowCrewAssistHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestNo", DbType.String, RequestNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerIDInt", DbType.Int32, Int32.Parse(SfID));
                SFDatebase.AddInParameter(SFDbCommand, "@pLastNameVarchar", DbType.String, LastName);
                SFDatebase.AddInParameter(SFDbCommand, "@pFirstNameVarchar", DbType.String, FirstName);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Int32.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.Int32, Int32.Parse(AirportID));
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.Int32, Int32.Parse(HotelID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckinDate", DbType.DateTime, CheckinDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckoutDate", DbType.DateTime, CheckoutDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pNoNitesInt", DbType.Int32, Int32.Parse(NoNites));
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeId", DbType.String, RoomType);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealBreakfastBit", DbType.Boolean, MealBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealLunchBit", DbType.Boolean, MealLunch);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealDinnerBit", DbType.Boolean, MealDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealLunchDinnerBit", DbType.Boolean, MealLunchDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithShuttleBit", DbType.Boolean, WithShuttle);
                SFDatebase.AddInParameter(SFDbCommand, "@pRankIDInt", DbType.Int32, Int32.Parse(RankID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselInt", DbType.Int32, Int32.Parse(VesselInt));
                SFDatebase.AddInParameter(SFDbCommand, "@pCostCenterInt", DbType.Int32, Int32.Parse(CostCenter));
                SFDatebase.AddInParameter(SFDbCommand, "@pCommentsVarchar", DbType.String, Comments);
                SFDatebase.AddInParameter(SFDbCommand, "@pSfStatus", DbType.String, SfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeIn", DbType.String, TimeIn);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimeOut", DbType.String, TimeOut);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmount", DbType.String, RoomAmount);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxInclusive", DbType.Boolean, IsRoomTax);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxPercentage", DbType.Double, GlobalCode.Field2Double(RoomTaxPercent));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTrID", DbType.String, TrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCurrency", DbType.String, Currency);
                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, strLogDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, strFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, strPageName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAirBit", DbType.Boolean, IsAir);
                SFDatebase.AddInParameter(SFDbCommand, "@pSequentNoInt", DbType.Int32, SequentNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigInt", DbType.Int64, IDBig);
                SFDatebase.AddInParameter(SFDbCommand, "@pBrandIDInt", DbType.Int32, BrandID);
                SFDatebase.AddInParameter(SFDbCommand, "@pMealVoucherMoney", DbType.Double, MealVoucher);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmRateMoney", DbType.Double, ConfirmRate);
                SFDatebase.AddInParameter(SFDbCommand, "@pContractedRateMoney", DbType.Double, ContractedRate);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailTO", DbType.String, EmailTO);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelCity", DbType.String, HotelCity);

                string sHRID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                trans.Commit();

                return sHRID;
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }

            }
        }




        /// <summary>
        /// Date Created: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Port agent Request to table PortAgentTransaction
        /// </summary>
        public List<CrewAssistSafeguardVendor> GetVendorSafeguard(short Loadtype, long SafeguardVendorIDInt)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet dt = null;
            try
            {

                List<CrewAssistSafeguardVendor> myList = new List<CrewAssistSafeguardVendor>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorSafeguard");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadtype", DbType.Int16, Loadtype);
                SFDatebase.AddInParameter(SFDbCommand, "@pSafeguardVendorIDInt", DbType.Int64, SafeguardVendorIDInt);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand);
                myList = (from Q in dt.Tables[0].AsEnumerable()
                          select new CrewAssistSafeguardVendor
                          {


                              SafeguardVendorID = GlobalCode.Field2Int(Q["colSafeguardVendorIDInt"]),
                              SafeguardName = GlobalCode.Field2String(Q["colSafeguardNameVarchar"]),
                              CountryID = GlobalCode.Field2Int(Q["colCountryIDInt"]),
                              CityID = GlobalCode.Field2Int(Q["colCityIDInt"]),
                              ContactNo = GlobalCode.Field2String(Q["colContactNoVarchar"]),
                              FaxNo = GlobalCode.Field2String(Q["colFaxNoVarchar"]),
                              ContactPerson = GlobalCode.Field2String(Q["colContactPersonVarchar"]),
                              Address = GlobalCode.Field2String(Q["colAddressVarchar"]),
                              EmailCc = GlobalCode.Field2String(Q["colEmailCcVarchar"]),
                              EmailTo = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                              Website = GlobalCode.Field2String(Q["colWebsiteVarchar"]),
                              CrewAssistSafeguardServiceType = (from n in dt.Tables[1].AsEnumerable()
                                                                select new CrewAssistSafeguardServiceType
                                                                {
                                                                    TypeID = GlobalCode.Field2Int(n["colTypeIDInt"]),
                                                                    ServiceDisplay = GlobalCode.Field2String(n["colDisplayVarchar"]),
                                                                    RateAmount = GlobalCode.Field2Double(n["colRateAmount"]).ToString("N2"),
                                                                    contractID = GlobalCode.Field2Int(n["colContractIdInt"]),
                                                                    ContractServiceTypeID = GlobalCode.Field2Int(n["colContractServiceTypeIDInt"]),
                                                                }).ToList()


                          }).ToList();

                return myList;

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



        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SaveSafeguardRequest(List<CrewAssistSafeguardRequest> list)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSaveSafeguardRequest");

                SFDatebase.AddInParameter(SFDbCommand, "@pReqSafeguardIDBigint", DbType.Int64, list[0].ReqSafeguardID);
                SFDatebase.AddInParameter(SFDbCommand, "@pReqSafeguardVarchar", DbType.String, list[0].ReqSafeguard);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, list[0].IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int64, list[0].TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, list[0].SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, list[0].RecordLocator);
                SFDatebase.AddInParameter(SFDbCommand, "@pSafeguardVendorIDInt", DbType.Int32, list[0].SafeguardVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTypeIDInt", DbType.Int32, list[0].TypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pContractIdInt", DbType.Int32, list[0].ContractId);
                SFDatebase.AddInParameter(SFDbCommand, "@pContractServiceTypeIDInt", DbType.Int32, list[0].ContractServiceTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFStatus", DbType.String, list[0].SFStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pCommentsVarchar", DbType.String, list[0].Comments);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAirBit", DbType.Boolean, list[0].IsAirBit);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransactionDate", DbType.DateTime, list[0].TransactionDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransactionTime", DbType.DateTime, list[0].TransactionTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, list[0].UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmBy", DbType.String, list[0].ConfirmBy);


                string IdentityID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();

                return IdentityID;
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




        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Get Transportation Rout
        /// </summary>
        public List<ComboGenericClass> GetTransportationRoute(string LoadType, long TransportID, long TravelRequestID, long RecLocID, int SeqNo, string PortCode, int PortID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetTransportationRoute");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.String, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pTranportsID", DbType.Int64, TransportID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecLocID", DbType.Int64, RecLocID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNo", DbType.Int32, SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortID", DbType.Int32, PortID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                var res = (from n in dt.AsEnumerable()
                           select new ComboGenericClass
                           {
                               ID = GlobalCode.Field2Int(n["ID"]),
                               Name = GlobalCode.Field2String(n["Name"]),
                               NameCode = GlobalCode.Field2String(n["Code"]),
                           }).ToList();

                return res;
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



        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Get Transportation Rout
        /// </summary>
        public List<CrewAssistGenericClass> GetComboGeneric(short LoadType, string PortCode, string ArrCode, int PortID, string storeproc)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = null;
            try
            {
                List<CrewAssistGenericClass> CrewAssistGenericClassList = new List<CrewAssistGenericClass>();

                SFDbCommand = SFDatebase.GetStoredProcCommand(storeproc);

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pArrPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pDepPortCode", DbType.String, ArrCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCodeID", DbType.Int32, PortID);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                //var res = (from n in dt.AsEnumerable()
                //           select new ComboGenericClass
                //           {
                //               ID = GlobalCode.Field2Int(n["ID"]),
                //               Name = GlobalCode.Field2String(n["Name"]),
                //               NameCode = GlobalCode.Field2String(n["Code"]),
                //           }).ToList();





                //DataTable dt = ds.Tables[5];
                CrewAssistGenericClassList.Add(new CrewAssistGenericClass
                {
                    CrewAssistHotelList = (from a in ds.Tables[0].AsEnumerable()
                                           select new CrewAssistHotelList
                                           {
                                               HotelID = GlobalCode.Field2Int(a["ID"]),
                                               HotelName = a["Name"].ToString(),
                                               IsPortAgent = GlobalCode.Field2Bool(a["IsPortAgent"])
                                           }).ToList(),



                    VehicleVendor = (from f in ds.Tables[1].AsEnumerable()
                                     select new VehicleVendor
                                     {
                                         VehicleID = GlobalCode.Field2Int(f["colVehicleVendorIDInt"]),
                                         Vehicle = f["colVehicleVendorNameVarchar"].ToString(),
                                         PortCode = f["PortCode"].ToString(),
                                         IsPortAgent = GlobalCode.Field2Bool(f["IsPortAgent"]),
                                     }).ToList(),


                    CrewAssistMeetAndGreetVendor = (from h in ds.Tables[2].AsEnumerable()
                                                    select new CrewAssistMeetAndGreetVendor
                                                    {
                                                        MeetAndGreetVendorID = GlobalCode.Field2Int(h["MeetAndGreetVendorID"]),
                                                        MeetAndGreetVendor = h["MeetAndGreetVendor"].ToString(),
                                                        AirportCodeID = GlobalCode.Field2Int(h["AirportCodeID"]),
                                                        AirportCode = h["AirportCode"].ToString(),
                                                    }).ToList(),

                    CrewAssistVendorPortAgent = (from i in ds.Tables[3].AsEnumerable()
                                                 select new CrewAssistVendorPortAgent
                                                 {
                                                     PortAgentVendorID = GlobalCode.Field2Int(i["colPortAgentVendorIDInt"]),
                                                     PortAgentVendorName = i["colPortAgentVendorNameVarchar"].ToString(),
                                                     PortCodeID = GlobalCode.Field2Int(i["PortCodeID"]),
                                                     PortCode = i["PortCode"].ToString(),
                                                     IsAir = GlobalCode.Field2Bool(i["IsSeaport"]),
                                                 }).ToList(),

                    CrewAssistSafeguardVendor = (from j in ds.Tables[4].AsEnumerable()
                                                 select new CrewAssistSafeguardVendor
                                                 {
                                                     SafeguardVendorID = GlobalCode.Field2Int(j["colSafeguardVendorIDInt"]),
                                                     SafeguardName = j["colSafeguardNameVarchar"].ToString(),
                                                     PortId = GlobalCode.Field2Int(j["colPortIdInt"]),
                                                     PortCode = j["colPortCodeVarchar"].ToString(),
                                                     PortName = j["colPortNameVarchar"].ToString(),
                                                 }).ToList(),



                });
                return CrewAssistGenericClassList;

                //return res;
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
                if (ds != null)
                {
                    ds.Dispose();
                }

            }
        }

        /// <summary>
        /// Date Modified: 25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Prioritize voucher from Hotel Other rather than Hotel Request table
        ///                Change MealVoucherMoney  = GlobalCode.Field2Double(r["colMealVoucherMoney"])
        ///                to     MealVoucherMoney = (GlobalCode.Field2Double(r["colVoucherAmountMoney"]) == 0 ? GlobalCode.Field2Double(r["[colMealVoucherMoney]"]) 
        ///                                                                                                        : GlobalCode.Field2Double(r["colVoucherAmountMoney"])),
        /// </summary>
        /// <returns></returns>
        public List<CrewAssistTransaction> GetCrewTransaction(short LoadType, long SeafarerID,
            long TravelRequestID, long IDBigInt, long SeqNo, DateTime Startdate, string DepCode,
             string ArrCode, bool IsAir, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds;

            try
            {
                List<CrewAssistTransaction> res = new List<CrewAssistTransaction>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetCrewTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerID", DbType.Int64, SeafarerID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigInt", DbType.Int64, IDBigInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNo", DbType.Int64, SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pStartdate", DbType.DateTime, Startdate);
                SFDatebase.AddInParameter(SFDbCommand, "@pDepCode", DbType.String, DepCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pArrCode", DbType.String, ArrCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsAir", DbType.Boolean, IsAir);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                if (LoadType == 0)
                {
                    res = (from n in ds.Tables[0].AsEnumerable()
                           select new CrewAssistTransaction
                           {

                               TransHotelOtherID = GlobalCode.Field2Long(n["TransHotelOtherID"]),
                               TransTransapotationID = GlobalCode.Field2Long(n["TransTransapotationID"]),
                               TransMeetAndGreetID = GlobalCode.Field2Long(n["TransMeetAndGreetID"]),
                               TransPortAgentID = GlobalCode.Field2Long(n["TransPortAgentID"]),
                               TransSafeguardID = GlobalCode.Field2Long(n["TransSafeguardID"]),

                               ReqHotelOtherID = GlobalCode.Field2Long(n["ReqHotelOtherID"]),
                               ReqTransapotati = GlobalCode.Field2Long(n["ReqTransapotationID"]),
                               ReqMeetAndGreet = GlobalCode.Field2Long(n["ReqMeetAndGreetID"]),
                               ReqPortAgentID = GlobalCode.Field2Long(n["ReqPortAgentID"]),
                               ReqSafeguardID = GlobalCode.Field2Long(n["ReqSafeguardID"]),

                           }).ToList();
                }
                else
                {
                    res = (from n in ds.Tables[0].AsEnumerable()
                           select new CrewAssistTransaction
                           {

                               TransHotelOtherID = GlobalCode.Field2Long(n["TransHotelOtherID"]),
                               TransTransapotationID = GlobalCode.Field2Long(n["TransTransapotationID"]),
                               TransMeetAndGreetID = GlobalCode.Field2Long(n["TransMeetAndGreetID"]),
                               TransPortAgentID = GlobalCode.Field2Long(n["TransPortAgentID"]),
                               TransSafeguardID = GlobalCode.Field2Long(n["TransSafeguardID"]),

                               ReqHotelOtherID = GlobalCode.Field2Long(n["ReqHotelOtherID"]),
                               ReqTransapotati = GlobalCode.Field2Long(n["ReqTransapotationID"]),
                               ReqMeetAndGreet = GlobalCode.Field2Long(n["ReqMeetAndGreetID"]),
                               ReqPortAgentID = GlobalCode.Field2Long(n["ReqPortAgentID"]),
                               ReqSafeguardID = GlobalCode.Field2Long(n["ReqSafeguardID"]),

                               CrewAssistHotelBooking = (from r in ds.Tables[1].AsEnumerable()
                                                         select new CrewAssistHotelBooking
                                                               {

                                                                   SeafarerIDInt = GlobalCode.Field2Long(r["colSeafarerIDInt"]),
                                                                   TransHotelID = GlobalCode.Field2Long(r["colTransHotelIDBigInt"]),
                                                                   IDBigInt = GlobalCode.Field2Long(r["colIdBigint"]),
                                                                   TravelReqID = GlobalCode.Field2Long(r["colTravelReqIDInt"]),
                                                                   SeqNo = GlobalCode.Field2TinyInt(r["colSeqNoInt"]),
                                                                   RecordLocator = GlobalCode.Field2String(r["colRecordLocatorVarchar"]),
                                                                   SfStatus = GlobalCode.Field2String(r["colSFStatus"]),
                                                                   RoomType = GlobalCode.Field2String(r["RoomType"]),
                                                                   RoomTypeID = GlobalCode.Field2Int(r["colRoomTypeIDInt"]),
                                                                   CheckinDate = GlobalCode.Field2DateTime(r["colTimeSpanStartDate"]),
                                                                   CheckoutDate = GlobalCode.Field2DateTime(r["colTimeSpanEndDate"]),
                                                                   NoNitesInt = GlobalCode.Field2Int(r["colTimeSpanDurationInt"]),
                                                                   HotelIDInt = GlobalCode.Field2Int(r["colBranchIDInt"]),
                                                                   Branch = GlobalCode.Field2String(r["colVendorBranchNameVarchar"]),
                                                                   HotelStatus = GlobalCode.Field2String(r["colHotelStatusVarchar"]),
                                                                   HotelComment = GlobalCode.Field2String(r["colCommentsVarchar"]),
                                                                   RoomRateTaxInclusive = GlobalCode.Field2Bool(r["colRoomRateTaxInclusive"]),
                                                                   RequestID = GlobalCode.Field2Long(r["colRequestIDInt"]),

                                                                   MealVoucherMoney = (GlobalCode.Field2Double(r["colVoucherAmountMoney"]) == 0 ? GlobalCode.Field2Double(r["colMealVoucherMoney"])
                                                                                                                                           : GlobalCode.Field2Double(r["colVoucherAmountMoney"])),
                                                                   ConfirmRateMoney = GlobalCode.Field2Double(r["colConfirmRateMoney"]),
                                                                   ContractedRateMoney = GlobalCode.Field2Double(r["colContractedRateMoney"]),
                                                                   HotelEmail = GlobalCode.Field2String(r["colEmailToVarchar"]),
                                                                   EmailAdd = GlobalCode.Field2String(r["colEmailToVarchar"]),
                                                                   MealBreakfastBit = GlobalCode.Field2Bool(r["colBreakfastBit"]),
                                                                   MealLunchBit = GlobalCode.Field2Bool(r["colLunchBit"]),
                                                                   MealDinnerBit = GlobalCode.Field2Bool(r["colDinnerBit"]),
                                                                   MealLunchDinnerBit = GlobalCode.Field2Bool(r["colLunchOrDinnerBit"]),
                                                                   WithShuttleBit = GlobalCode.Field2Bool(r["colWithShuttleBit"]),
                                                                   RoomCountFloat = GlobalCode.Field2Int(r["colRoomCountFloat"]),

                                                                   RoomAmount = GlobalCode.Field2String(r["colRoomAmountMoney"]),
                                                                   RoomRateTaxPercentage = GlobalCode.Field2Double(r["colRoomRateTaxPercentage"]),
                                                                   Currency = GlobalCode.Field2Int(r["colCurrency"]),
                                                                   isAir = GlobalCode.Field2Bool(r["colIsAir"]),
                                                                   IsPortAgent = GlobalCode.Field2Bool(r["colIsPortAgent"]),
                                                                   //IsApproved = GlobalCode.Field2Int(r["colStatusIDTinyint"]) >= 3 ? false : true,
                                                                   IsApproved = GlobalCode.Field2Bool(r["colIsPortAgent"]) == true ? GlobalCode.Field2Int(r["colStatusIDTinyint"]) >= 3 ? false : true : GlobalCode.Field2Long(n["TransHotelOtherID"]) > 0 ? false : true,
                                                                   StatusID = GlobalCode.Field2Int(r["colStatusIDTinyint"]),
                                                                   ConfirmBy = GlobalCode.Field2String(r["colConfirmBy"]),
                                                                   RequestSourceID = GlobalCode.Field2Int(r["colRequestSourceID"]),
                                                                   IsAutoAirportToHotel = GlobalCode.Field2Bool(r["colIsAutoAirportToHotel"]),
                                                                   IsAutoHotelToShip = GlobalCode.Field2Bool(r["colIsAutoHotelToShip"]),
                                                                   ReasonCode = GlobalCode.Field2String(r["colReasonCode"]),
                                                                   IsMedical = GlobalCode.Field2Bool(r["colIsMedical"]),

                                                                   ContractDateStarted = GlobalCode.Field2DateTime(r["colContractDateStartedDate"]).ToShortDateString(),
                                                                   ContractDateEnd = GlobalCode.Field2DateTime(r["colContractDateEndDate"]).ToShortDateString(),


                                                                   CancellationTermsInt = GlobalCode.Field2Int(r["colCancellationTermsInt"]),
                                                                   HotelTimeZoneID = GlobalCode.Field2String(r["colHotelTimeZoneIDVarchar"]),
                                                                   CutOffTime = GlobalCode.Field2DateTime(r["colCutOffTime"] ),




                                                                   HotelRemark = (from a in ds.Tables[13].AsEnumerable()
                                                                                  where GlobalCode.Field2TinyInt(a["colExpendTypeIdInt"]) == 1
                                                                                  select new CrewAssisRemark
                                                                                  {

                                                                                      TravelRequestID = a.Field<long>("TravelRequestID"),
                                                                                      RemarkID = a.Field<long>("RemarkID"),
                                                                                      Remark = a.Field<string>("Remark"),
                                                                                      RemarkBy = a.Field<string>("CreatedBy"),
                                                                                      RemarkDate = a.Field<DateTime>("RemarkDate"),
                                                                                      Visible = a.Field<string>("Visible"),
                                                                                      ReqResourceID = GlobalCode.Field2TinyInt(a["colRemarkSourceID"]),
                                                                                      Resource = a.Field<string>("colRemarkSource"),
                                                                                      IDBigInt = a.Field<long?>("IDBigInt"),
                                                                                      RecordLocator = a.Field<string>("RecordLocator")

                                                                                  }).ToList()


                                                               }).ToList(),


                               CrewAssistTranspo = (from Q in ds.Tables[2].AsEnumerable()
                                                    select new CrewAssistTranspo
                                                    {
                                                        ReqVehicleIDBigint = GlobalCode.Field2Long(Q["colReqVehicleIDBigint"]),
                                                        IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                        TravelReqIDInt = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                        SeqNoInt = GlobalCode.Field2TinyInt(Q["colSeqNoInt"]),
                                                        RecordLocatorVarchar = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                                                        VehicleVendorIDInt = GlobalCode.Field2Long(Q["colVehicleVendorIDInt"]),
                                                        VehiclePlateNoVarchar = GlobalCode.Field2String(Q["colVehiclePlateNoVarchar"]),
                                                        PickUpDate = GlobalCode.Field2DateTime(Q["colPickUpDate"]),
                                                        PickUpTime = GlobalCode.Field2DateTime(Q["colPickUpTime"]),
                                                        DropOffDate = GlobalCode.Field2DateTime(Q["colDropOffDate"]),
                                                        DropOffTime = GlobalCode.Field2DateTime(Q["colDropOffTime"]),
                                                        ConfirmationNoVarchar = GlobalCode.Field2String(Q["colConfirmationNoVarchar"]),
                                                        VehicleStatusVarchar = GlobalCode.Field2String(Q["colVehicleStatusVarchar"]),
                                                        VehicleTypeIdInt = GlobalCode.Field2Int(Q["colVehicleTypeIdInt"]),
                                                        SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                        RouteIDFromInt = GlobalCode.Field2Int(Q["colRouteIDFromInt"]),
                                                        RouteIDToInt = GlobalCode.Field2Int(Q["colRouteIDToInt"]),
                                                        FromVarchar = GlobalCode.Field2String(Q["colFromVarchar"]),
                                                        ToVarchar = GlobalCode.Field2String(Q["colToVarchar"]),
                                                        Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                                                        Email = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                                                        RouteFrom = GlobalCode.Field2String(Q["colRouteFromVarchar"]),
                                                        RouteTo = GlobalCode.Field2String(Q["colRouteToVarchar"]),
                                                        Address = GlobalCode.Field2String(Q["colAddressVarchar"]),
                                                        Telephone = GlobalCode.Field2String(Q["colContactNoVarchar"]),
                                                        TranspoCost = GlobalCode.Field2Double(Q["colConfirmRateMoney"]).ToString("N2"),
                                                        ConfirmBy = GlobalCode.Field2String(Q["colConfirmBy"]),

                                                        VehicleTransID = GlobalCode.Field2Long(Q["colTransVehicleIDBigint"]),
                                                        IsPortAgent = GlobalCode.Field2Bool(Q["colIsPortAgent"]),



                                                        ColorCode = GlobalCode.Field2String(Q["colColorCodevarchar"]),
                                                        ForeColor = GlobalCode.Field2String(Q["coldForeColorVarchar"]),
                                                        StatusID = GlobalCode.Field2Int(Q["colStatusID"]),




                                                        VehicleContract = (from x in ds.Tables[3].AsEnumerable()
                                                                           select new CrewAssisVehicleCost
                                                                           {

                                                                               PortCode = x["PortCode"].ToString(),
                                                                               CurrencyCode = x["CurrencyCode"].ToString(),
                                                                               Route = x["Route"].ToString(),
                                                                               VehicleTypeName = x["VehicleTypeName"].ToString(),
                                                                               VehicleTypeID = GlobalCode.Field2Int(x["VehicleTypeID"]),

                                                                               UnitOfMeasure = x["UnitOfMeasure"].ToString(),
                                                                               //TranspoCost = x["Cost"].ToString(),
                                                                               TranspoCost = GlobalCode.Field2Double(x["Cost"]).ToString("N2"),
                                                                               Capacity = x["Capacity"].ToString(),
                                                                               VacantSeat = x["VacantSeat"].ToString(),

                                                                           }).ToList(),


                                                        VehicleRemark = (from a in ds.Tables[13].AsEnumerable()
                                                                         where GlobalCode.Field2TinyInt(a["colExpendTypeIdInt"]) == 2
                                                                         select new CrewAssisRemark
                                                                         {

                                                                             TravelRequestID = a.Field<long>("TravelRequestID"),
                                                                             RemarkID = a.Field<long>("RemarkID"),
                                                                             Remark = a.Field<string>("Remark"),
                                                                             RemarkBy = a.Field<string>("CreatedBy"),
                                                                             RemarkDate = a.Field<DateTime>("RemarkDate"),
                                                                             Visible = a.Field<string>("Visible"),
                                                                             ReqResourceID = GlobalCode.Field2TinyInt(a["colRemarkSourceID"]),
                                                                             Resource = a.Field<string>("colRemarkSource"),
                                                                             IDBigInt = a.Field<long?>("IDBigInt"),
                                                                             RecordLocator = a.Field<string>("RecordLocator")
                                                                         }).ToList()

                                                    }).ToList(),

                               CrewAssistMeetAndGreet = (from Q in ds.Tables[4].AsEnumerable()
                                                         select new CrewAssistMeetAndGreet
                                                          {
                                                              ReqMeetAndGreetID = GlobalCode.Field2Long(Q["colReqMeetAndGreetIDBigint"]),
                                                              IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                              TravelReqID = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                              SeqNo = GlobalCode.Field2Int(Q["colSeqNoInt"]),
                                                              RecordLocator = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                                                              MeetAndGreetVendorID = GlobalCode.Field2Int(Q["colMeetAndGreetVendorIDInt"]),
                                                              MeetAndGreetVendor = GlobalCode.Field2String(Q["colMeetAndGreetVendorNameVarchar"]),
                                                              ConfirmationNo = GlobalCode.Field2String(Q["colConfirmationNoVarchar"]),
                                                              AirportID = GlobalCode.Field2Int(Q["colAirportInt"]),
                                                              AirportCode = GlobalCode.Field2String(Q["AirportName"]),
                                                              FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                                                              ServiceDate = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                                                              Rate = GlobalCode.Field2Double(Q["colRateFloat"]),
                                                              SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                              Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                                                              IsAir = GlobalCode.Field2Bool(Q["colIsAirBit"]),
                                                              Email = GlobalCode.Field2String(Q["colEmailToVarchar"])
                                                          }).ToList(),


                               CrewAssistPortAgentRequest = (from Q in ds.Tables[5].AsEnumerable()
                                                             select new CrewAssistPortAgentRequest
                                                             {

                                                                 ReqPortAgentID = GlobalCode.Field2Long(Q["colReqPortAgentIDBigint"]),
                                                                 IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                                 TravelReqID = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                                 SeqNo = GlobalCode.Field2Int(Q["colSeqNoInt"]),
                                                                 RecordLocator = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),

                                                                 PortAgentVendorID = GlobalCode.Field2Int(Q["colPortAgentVendorIDInt"]),
                                                                 PortCodeID = GlobalCode.Field2Int(Q["colPortIdInt"]),
                                                                 PortCode = GlobalCode.Field2String(Q["colPortCode"]),
                                                                 AirportID = GlobalCode.Field2Int(Q["colAirportIDInt"]),
                                                                 AirportCode = GlobalCode.Field2String(Q["colAirportCode"]),
                                                                 FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                                                                 ServiceDatetime = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                                                                 SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                                 Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),

                                                                 IsMAG = GlobalCode.Field2Bool(Q["colIsMAGBit"]),
                                                                 IsTrans = GlobalCode.Field2Bool(Q["colIsTransBit"]),
                                                                 IsHotel = GlobalCode.Field2Bool(Q["colIsHotelBit"]),
                                                                 IsLuggage = GlobalCode.Field2Bool(Q["colIsLuggageBit"]),
                                                                 IsSafeguard = GlobalCode.Field2Bool(Q["colIsSafeguardBit"]),
                                                                 IsVisa = GlobalCode.Field2Bool(Q["colIsVisaBit"]),
                                                                 IsOther = GlobalCode.Field2Bool(Q["colIsOtherBit"]),
                                                                 IsAir = GlobalCode.Field2Bool(Q["colIsAirBit"]),

                                                                 PortAgentVendorName = GlobalCode.Field2String(Q["colPortAgentVendorNameVarchar"]),
                                                                 CityID = GlobalCode.Field2Int(Q["colCityIDInt"]),
                                                                 ContactNo = GlobalCode.Field2String(Q["colContactNoVarchar"]),
                                                                 FaxNo = GlobalCode.Field2String(Q["colFaxNoVarchar"]),
                                                                 ContactPerson = GlobalCode.Field2String(Q["colContactPersonVarchar"]),
                                                                 Address = GlobalCode.Field2String(Q["colAddressVarchar"]),
                                                                 EmailCc = GlobalCode.Field2String(Q["colEmailCcVarchar"]),
                                                                 EmailTo = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                                                                 Website = GlobalCode.Field2String(Q["colWebsiteVarchar"]),

                                                                 IsPhoneCard = GlobalCode.Field2Bool(Q["colIsPhoneCardBit"]),
                                                                 PhoneCard = GlobalCode.Field2Double(Q["colPhoneCardMoney"]),
                                                                 IsLaundry = GlobalCode.Field2Bool(Q["colIsLaundryBit"]),
                                                                 Laundry = GlobalCode.Field2Double(Q["colLaundryMoney"]),
                                                                 IsGiftCard = GlobalCode.Field2Bool(Q["colIsGiftCardBit"]),
                                                                 GiftCard = GlobalCode.Field2Double(Q["colGiftCardMoney"]),





                                                                 VehicleTransactionPortAgent = (from s in ds.Tables[6].AsEnumerable()
                                                                                                select new VehicleTransactionPortAgent
                                                                                                {
                                                                                                    VehicleTransID = GlobalCode.Field2Long(s["colTransVehicleIDBigint"]),
                                                                                                    IdBigint = GlobalCode.Field2Long(s["colIdBigint"]),
                                                                                                    TravelReqIDInt = GlobalCode.Field2Long(s["colTravelReqIDInt"]),
                                                                                                    RecordLocator = GlobalCode.Field2String(s["colRecordLocatorVarchar"]),
                                                                                                    PortAgentVendorID = GlobalCode.Field2Long(s["colPortAgentVendorIDInt"]),
                                                                                                    VehiclePlateNo = GlobalCode.Field2String(s["colVehiclePlateNoVarchar"]),
                                                                                                    PickUpDate = GlobalCode.Field2DateTime(s["colPickUpDate"]),
                                                                                                    PickUpTime = GlobalCode.Field2DateTime(s["colPickUpTime"]),
                                                                                                    DropOffDate = GlobalCode.Field2DateTime(s["colDropOffDate"]),
                                                                                                    DropOffTime = GlobalCode.Field2DateTime(s["colDropOffTime"]),
                                                                                                    ConfirmationNo = GlobalCode.Field2String(s["colConfirmationNoVarchar"]),
                                                                                                    VehicleStatus = GlobalCode.Field2String(s["colVehicleStatusVarchar"]),
                                                                                                    VehicleTypeID = GlobalCode.Field2Int(s["colVehicleTypeIdInt"]),
                                                                                                    RouteIDFromInt = GlobalCode.Field2Int(s["colRouteIDFromInt"]),
                                                                                                    RouteIDToInt = GlobalCode.Field2Int(s["colRouteIDToInt"]),
                                                                                                    FromVarchar = GlobalCode.Field2String(s["colFromVarchar"]),
                                                                                                    ToVarchar = GlobalCode.Field2String(s["colToVarchar"]),

                                                                                                    RemarksForAudit = GlobalCode.Field2String(s["colRemarksForAuditVarchar"]),
                                                                                                    HotelID = GlobalCode.Field2Int(s["colHotelIDInt"]),
                                                                                                    SeqNo = GlobalCode.Field2Int(s["colSeqNoInt"]),
                                                                                                    DriverID = GlobalCode.Field2Int(s["colDriverIDInt"]),

                                                                                                    VehicleDispatchTime = GlobalCode.Field2DateTime(s["colVehicleDispatchTime"]),
                                                                                                    RouteFrom = GlobalCode.Field2String(s["colRouteFromVarchar"]),
                                                                                                    RouteTo = GlobalCode.Field2String(s["colRouteToVarchar"]),
                                                                                                    VehicleUnset = GlobalCode.Field2Long(s["colVehicleUnset"]),
                                                                                                    VehicleUnsetBy = GlobalCode.Field2String(s["colVehicleUnsetBy"]),
                                                                                                    VehicleUnsetDate = GlobalCode.Field2DateTime(s["colVehicleUnsetDate"]),
                                                                                                    ConfirmBy = GlobalCode.Field2String(s["colConfirmByVarchar"]),
                                                                                                    Comments = GlobalCode.Field2String(s["colCommentsVarchar"]),

                                                                                                }).ToList(),


                                                                 PortAgentVehicle = (from x in ds.Tables[7].AsEnumerable()
                                                                                     select new CrewAssisVehicleCost
                                                                                     {

                                                                                         PortCode = x["PortCode"].ToString(),
                                                                                         CurrencyCode = x["CurrencyCode"].ToString(),
                                                                                         Route = x["Route"].ToString(),
                                                                                         VehicleTypeName = x["VehicleTypeName"].ToString(),
                                                                                         VehicleTypeID = GlobalCode.Field2Int(x["VehicleTypeID"]),

                                                                                         UnitOfMeasure = x["UnitOfMeasure"].ToString(),
                                                                                         //TranspoCost = x["Cost"].ToString(),
                                                                                         TranspoCost = GlobalCode.Field2Double(x["Cost"]).ToString("N2"),
                                                                                         Capacity = x["Capacity"].ToString(),
                                                                                         VacantSeat = x["VacantSeat"].ToString(),

                                                                                     }).ToList(),





                                                                 HotelTransactionPortAgent = (from h in ds.Tables[8].AsEnumerable()
                                                                                              select new HotelTransactionPortAgent
                                                                                              {

                                                                                                  HotelTransID = GlobalCode.Field2Int(h["colTransHotelIDBigInt"]),
                                                                                                  TravelReqID = GlobalCode.Field2Int(h["colTravelReqIDInt"]),
                                                                                                  IdBigint = GlobalCode.Field2Int(h["colIdBigint"]),
                                                                                                  RecordLocator = GlobalCode.Field2String(h["colRecordLocatorVarchar"]),
                                                                                                  SeqNo = GlobalCode.Field2Int(h["colSeqNoInt"]),

                                                                                                  PortAgentVendorID = GlobalCode.Field2Int(h["colPortAgentVendorIDInt"]),
                                                                                                  RoomTypeID = GlobalCode.Field2Int(h["colRoomTypeIDInt"]),
                                                                                                  ReserveUnderName = GlobalCode.Field2String(h["colReserveUnderNameVarchar"]),
                                                                                                  TimeSpanStartDate = GlobalCode.Field2DateTime(h["colTimeSpanStartDate"]),
                                                                                                  TimeSpanStartTime = GlobalCode.Field2DateTime(h["colTimeSpanStartTime"]),
                                                                                                  TimeSpanEndDate = GlobalCode.Field2DateTime(h["colTimeSpanEndDate"]),
                                                                                                  TimeSpanEndTime = GlobalCode.Field2DateTime(h["colTimeSpanEndTime"]),
                                                                                                  TimeSpanDurationInt = GlobalCode.Field2Int(h["colTimeSpanDurationInt"]),

                                                                                                  ConfirmationNo = GlobalCode.Field2String(h["colConfirmationNoVarchar"]),
                                                                                                  HotelStatus = GlobalCode.Field2String(h["colHotelStatusVarchar"]),
                                                                                                  IsBilledToCrew = GlobalCode.Field2Bool(h["colIsBilledToCrewBit"]),
                                                                                                  Breakfast = GlobalCode.Field2Bool(h["colBreakfastBit"]),
                                                                                                  Lunch = GlobalCode.Field2Bool(h["colLunchBit"]),
                                                                                                  Dinner = GlobalCode.Field2Bool(h["colDinnerBit"]),

                                                                                                  LunchOrDinner = GlobalCode.Field2Bool(h["colLunchOrDinnerBit"]),
                                                                                                  BreakfastID = GlobalCode.Field2Int(h["colBreakfastIDInt"]),
                                                                                                  LunchID = GlobalCode.Field2Int(h["colLunchIDInt"]),
                                                                                                  DinnerID = GlobalCode.Field2Int(h["colDinnerIDInt"]),
                                                                                                  LunchOrDinnerID = GlobalCode.Field2Int(h["colLunchOrDinnerIDInt"]),


                                                                                                  ContractID = GlobalCode.Field2Int(h["colContractIDInt"]),
                                                                                                  ApprovedBy = GlobalCode.Field2String(h["colApprovedByVarchar"]),
                                                                                                  ApprovedDate = GlobalCode.Field2DateTime(h["colApprovedDate"]),

                                                                                                  ContractFrom = GlobalCode.Field2String(h["colContractFromVarchar"]),
                                                                                                  RemarksForAudit = GlobalCode.Field2String(h["colRemarksForAuditVarchar"]),
                                                                                                  HotelCity = GlobalCode.Field2String(h["colHotelCityVarchar"]),
                                                                                                  RoomCount = GlobalCode.Field2Decimal(h["colRoomCountFloat"]),
                                                                                                  HotelName = GlobalCode.Field2String(h["colHotelNameVarchar"]),

                                                                                                  VoucherAmount = GlobalCode.Field2Double(h["colVoucherAmountMoney"]),
                                                                                                  ContractRate = GlobalCode.Field2Double(h["ContractRate"]).ToString(),
                                                                                                  ConfirmRate = GlobalCode.Field2Double(h["ConfirmRate"]).ToString(),
                                                                                                  //IsAutoAirportToHotel = GlobalCode.Field2Bool(h["colIsAutoAirportToHotel"]),
                                                                                                  //IsAutoHotelToShip = GlobalCode.Field2Bool(h["colIsAutoHotelToShip"]),


                                                                                              }).ToList(),




                                                             }).ToList(),


                               CrewAssistSafeguardRequest = (from p in ds.Tables[9].AsEnumerable()
                                                             select new CrewAssistSafeguardRequest
                                                             {
                                                                 ReqSafeguardID = GlobalCode.Field2Long(p["colReqSafeguardIDBigint"]),
                                                                 IdBigint = GlobalCode.Field2Long(p["colIdBigint"]),
                                                                 TravelReqID = GlobalCode.Field2Long(p["colTravelReqIDInt"]),
                                                                 SeqNo = GlobalCode.Field2TinyInt(p["colSeqNoInt"]),
                                                                 RecordLocator = GlobalCode.Field2String(p["colRecordLocatorVarchar"]),
                                                                 SafeguardVendorID = GlobalCode.Field2Int(p["colSafeguardVendorIDInt"]),
                                                                 TypeID = GlobalCode.Field2Int(p["colTypeIDInt"]),
                                                                 Type = GlobalCode.Field2String(p["colTypeNameVarchar"]),
                                                                 ContractId = GlobalCode.Field2Int(p["colContractIdInt"]),
                                                                 ContractServiceTypeID = GlobalCode.Field2Int(p["colContractServiceTypeIDInt"]),
                                                                 ContractServiceType = GlobalCode.Field2String(p["colContractServiceType"]),
                                                                 SFStatus = GlobalCode.Field2String(p["colSFStatus"]),
                                                                 Comments = GlobalCode.Field2String(p["colCommentsVarchar"]),
                                                                 TransactionDate = GlobalCode.Field2DateTime(p["colTransactionDate"]),
                                                                 TransactionTime = GlobalCode.Field2DateTime(p["colTransactionTime"]),
                                                                 SGRate = GlobalCode.Field2Double(p["colRateAmount"]).ToString("N2"),

                                                                 Address = GlobalCode.Field2String(p["colAddressVarchar"]),
                                                                 ContactNo = GlobalCode.Field2String(p["colContactNoVarchar"]),
                                                                 EmailTo = GlobalCode.Field2String(p["colEmailToVarchar"]),
                                                             }).ToList(),


                               CrewAssistTranspoApprove = (from Q in ds.Tables[10].AsEnumerable()
                                                           select new CrewAssistTranspo
                                                           {
                                                               ReqVehicleIDBigint = GlobalCode.Field2Long(Q["colReqVehicleIDBigint"]),
                                                               IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                               TravelReqIDInt = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                               SeqNoInt = GlobalCode.Field2TinyInt(Q["colSeqNoInt"]),
                                                               RecordLocatorVarchar = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                                                               VehicleVendorIDInt = GlobalCode.Field2Long(Q["colVehicleVendorIDInt"]),
                                                               VehiclePlateNoVarchar = GlobalCode.Field2String(Q["colVehiclePlateNoVarchar"]),
                                                               VehicleVendor = GlobalCode.Field2String(Q["colVehicleVendorNameVarchar"]),
                                                               PickUpDate = GlobalCode.Field2DateTime(Q["colPickUpDate"]),
                                                               PickUpTime = GlobalCode.Field2DateTime(Q["colPickUpTime"]),
                                                               DropOffDate = GlobalCode.Field2DateTime(Q["colDropOffDate"]),
                                                               DropOffTime = GlobalCode.Field2DateTime(Q["colDropOffTime"]),
                                                               ConfirmationNoVarchar = GlobalCode.Field2String(Q["colConfirmationNoVarchar"]),
                                                               VehicleStatusVarchar = GlobalCode.Field2String(Q["colVehicleStatusVarchar"]),
                                                               VehicleTypeIdInt = GlobalCode.Field2Int(Q["colVehicleTypeIdInt"]),
                                                               SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                               RouteIDFromInt = GlobalCode.Field2Int(Q["colRouteIDFromInt"]),
                                                               RouteIDToInt = GlobalCode.Field2Int(Q["colRouteIDToInt"]),
                                                               FromVarchar = GlobalCode.Field2String(Q["colFromVarchar"]),
                                                               ToVarchar = GlobalCode.Field2String(Q["colToVarchar"]),
                                                               Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                                                               Email = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                                                               RouteFrom = GlobalCode.Field2String(Q["colRouteFromVarchar"]),
                                                               RouteTo = GlobalCode.Field2String(Q["colRouteToVarchar"]),
                                                               TranspoCost = GlobalCode.Field2Double(Q["colConfirmRateMoney"]).ToString("N2"),
                                                               VehicleTransID = GlobalCode.Field2Long(Q["colTransVehicleIDBigint"]),
                                                               IsPortAgent = GlobalCode.Field2Bool(Q["colIsPortAgent"]),
                                                               ColorCode = GlobalCode.Field2String(Q["colColorCodevarchar"]),
                                                               ForeColor = GlobalCode.Field2String(Q["coldForeColorVarchar"]),
                                                               StatusID = GlobalCode.Field2Int(Q["colStatusID"])

                                                           }).ToList(),


                               CopyEmail = (from s in ds.Tables[12].AsEnumerable()
                                            select new CopyEmail
                                            {
                                                EmailName = GlobalCode.Field2String(s["EmailType"]),
                                                EmailType = GlobalCode.Field2Int(s["EmailID"]),
                                                Email = GlobalCode.Field2String(s["Email"]),
                                            }).ToList(),



                               HotelRequestCompanion = (from k in ds.Tables[14].AsEnumerable()
                                                        select new HotelRequestCompanion
                                                        {
                                                            DETAILID = GlobalCode.Field2Long(k["DETAILID"]),
                                                            REQUESTID = GlobalCode.Field2Long(k["REQUESTID"]),
                                                            TRANSPOID = GlobalCode.Field2Long(k["TRANSPOID"]),
                                                            GENDERID = GlobalCode.Field2Int(k["GENDERID"]),
                                                            GENDER = GlobalCode.Field2String(k["GENDER"]),
                                                            LASTNAME = GlobalCode.Field2String(k["LASTNAME"]),
                                                            FIRSTNAME = GlobalCode.Field2String(k["FIRSTNAME"]),
                                                            RELATIONSHIP = GlobalCode.Field2String(k["RELATIONSHIP"]),
                                                            UserID = GlobalCode.Field2String(k["UserID"]),
                                                            IsMedical = GlobalCode.Field2Bool(k["IsMedical"]),
                                                            TRAVELREQID = GlobalCode.Field2Long(k["TravelRequestID"]),
                                                            IDBIGINT = GlobalCode.Field2Long(k["IDBignt"]),
                                                            RECORDLOCATOR = GlobalCode.Field2String(k["RecordLocator"]),
                                                            SEQNO = GlobalCode.Field2Int(k["SeqNo"]),
                                                            IsPortAgent = GlobalCode.Field2Bool(k["IsPortAgent"]), 

                                                        }).ToList(),

                           }).ToList();
                }



                return res;
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
        /// <summary>
        /// Date Created:   16/Nov/2013
        /// Created By:    Josephine Gad
        /// (description)   Get Vendors of Port Selected
        /// 
        /// </summary>
        public void GetVendors(Int16 iLoadTpe, string sUsername, int iSeaPortID, int iAirPortID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = null;
            DataTable dtVehicle = null;
            DataTable dtPortAgent = null;
            DataTable dtMeetAndGreet = null;
            DataTable dtSafeguard = null;

            List<VehicleVendor> listVehicle = new List<VehicleVendor>();
            List<PortAgentDTO> listPortAgent = new List<PortAgentDTO>();
            List<MeetAndGreetList> listMeetGreet = new List<MeetAndGreetList>();
            List<VendorSafeguardList> listSafeguard = new List<VendorSafeguardList>();

            HttpContext.Current.Session["CrewAssist_VehicleList"] = listVehicle;
            HttpContext.Current.Session["CrewAssist_PortAgentList"] = listPortAgent;
            HttpContext.Current.Session["CrewAssist_MeetGreet"] = listMeetGreet;
            HttpContext.Current.Session["CrewAssist_Safeguard"] = listSafeguard;

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCrewAssistGetVendor");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadTypeInt", DbType.Int16, iLoadTpe);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, sUsername);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, iSeaPortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.String, iAirPortID);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                dtVehicle = ds.Tables[0];
                dtPortAgent = ds.Tables[1];
                dtMeetAndGreet = ds.Tables[2];
                dtSafeguard = ds.Tables[3];


                listVehicle = (from a in dtVehicle.AsEnumerable()
                               select new VehicleVendor
                               {
                                   VehicleID = GlobalCode.Field2Int(a["VendorID"]),
                                   Vehicle = GlobalCode.Field2String(a["VehicleName"]),
                               }).ToList();

                listPortAgent = (from a in dtPortAgent.AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                                     PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),
                                 }).ToList();

                listMeetGreet = (from a in dtMeetAndGreet.AsEnumerable()
                                 select new MeetAndGreetList
                                 {
                                     MeetandGreetVendorId = GlobalCode.Field2Int(a["MeetAndGreetID"]),
                                     MeetAndGreetVendorName = GlobalCode.Field2String(a["MeetAndGreetName"]),
                                 }).ToList();

                listSafeguard = (from a in dtSafeguard.AsEnumerable()
                                 select new VendorSafeguardList
                                 {
                                     SafeguardID = GlobalCode.Field2Int(a["SafeguardID"]),
                                     VendorName = GlobalCode.Field2String(a["SafeguardName"]),
                                 }).ToList();

                HttpContext.Current.Session["CrewAssist_VehicleList"] = listVehicle;
                HttpContext.Current.Session["CrewAssist_PortAgentList"] = listPortAgent;
                HttpContext.Current.Session["CrewAssist_MeetGreet"] = listMeetGreet;
                HttpContext.Current.Session["CrewAssist_Safeguard"] = listSafeguard;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtMeetAndGreet != null)
                {
                    dtMeetAndGreet.Dispose();
                }
                if (dtSafeguard != null)
                {
                    dtSafeguard.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Modified: 17/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Send Email Transport
        /// </summary>
        public string SendEmailTransport(Int32 iTransactionID, string sUserID, string sReceiver, string sRecipient, string sBlindCopy)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;
            string res = "";
            try
            {

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSendVehicleTransactionEmail");
                SFDatebase.AddInParameter(SFDbCommand, "@VehTransID", DbType.Int32, GlobalCode.Field2Int(iTransactionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, sUserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, sReceiver);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, sRecipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, sBlindCopy);


                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Modified: 18/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Send Email for Hotel Request
        /// </summary>
        public string SendEmailHotel(Int32 iRequestID, string sUserID, string sReceiver, string sRecipient, string sBlindCopy)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;
            string res = "";
            try
            {

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSendHotelTransactionOtherRequest2");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, iRequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, sUserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, sReceiver);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, sRecipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, sBlindCopy);


                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Modified: 18/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Send Email for Hotel Request
        /// </summary>
        public string SendEmailPortAgent(Int32 iPortAgentReqID, string sUserID, string sReceiver, string sRecipient, string sBlindCopy)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;
            string res = "";
            try
            {

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSendPortAgentTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pReqPortAgentID", DbType.Int32, iPortAgentReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, sUserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, sReceiver);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, sRecipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, sBlindCopy);


                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    res = dt.Rows[0]["Tosend"].ToString() == "1" ? dt.Rows[0]["Message"].ToString() : "";
                }
                return res;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }


        public List<CrewAssisRemark> InsertCrewAssistRemark(long RemarkID, long TravelReqIdInt,
                                 string RamarksVarchar, string UserID
                                , long IDBigInt, string Role, short ReqSource
                                , long SeafarerID, int RemarkTypeID, short RemarkStatusID
                                , string SummaryCall, int RemarkRequestorID
                                ,DateTime TransactionDate, DateTime TransactionTime
                                ,string PortCode , bool IR)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;

            try
            {

                List<CrewAssisRemark> remark = new List<CrewAssisRemark>();

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertCrewAssistRemarks");
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarkID", DbType.Int32, RemarkID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int64, TravelReqIdInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerID", DbType.Int64, SeafarerID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigInt", DbType.Int64, IDBigInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoleVarchar", DbType.String, Role);
                SFDatebase.AddInParameter(SFDbCommand, "@pRamarksVarchar", DbType.String, RamarksVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pReqSource", DbType.Int16, ReqSource);
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarkTypeID", DbType.Int32, RemarkTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarkStatusID", DbType.Int16, RemarkStatusID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSummaryCall", DbType.String, SummaryCall);
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarkRequestorID", DbType.Int32, RemarkRequestorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransactionDate", DbType.Date, TransactionDate.Date);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransactionTime", DbType.Time, TransactionTime);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pIR", DbType.Boolean, IR);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                remark = (from n in dt.AsEnumerable()
                          select new CrewAssisRemark
                          {

                              TravelRequestID = GlobalCode.Field2Long(n["TravelRequestID"]),
                              SeafarerID = GlobalCode.Field2Long(n["SeafarerID"]),
                              RemarkID = GlobalCode.Field2Long(n["RemarkID"]),
                              Remark = GlobalCode.Field2String(n["Remark"]),
                              RemarkBy = GlobalCode.Field2String(n["RemarkBy"]),
                              RemarkDate = n.Field<DateTime?>("RemarkDate"),
                              Visible = GlobalCode.Field2String(n["Visible"]),
                              ReqResourceID = GlobalCode.Field2TinyInt(n["colRemarkSourceID"]),
                              Resource = GlobalCode.Field2String(n["colRemarkSource"]),
                              IDBigInt = GlobalCode.Field2Long(n["IDBigInt"]),
                              RecordLocator = GlobalCode.Field2String(n["RecordLocator"]),
                              RemarkTypeID = GlobalCode.Field2Int(n["RemarkTypeID"]),
                              RemarkType = GlobalCode.Field2String(n["RemarkType"]),
                              RemarkStatusID = GlobalCode.Field2TinyInt(n["RemarkStatusID"]),
                              RemarkStatus = GlobalCode.Field2String(n["RemarkStatus"]),
                              SummaryOfCall = GlobalCode.Field2String(n["SummaryCall"]),

                              RemarkRequestorID = GlobalCode.Field2Int(n["RemarkRequestorID"]),
                              RemarkRequestor = GlobalCode.Field2String(n["RemarkRequestor"]),
                              TransactionDate = GlobalCode.Field2DateTime(n["TransactionDate"]),
                              TransactionTime = GlobalCode.Field2DateTime(n["TransactionTime"])    ,
                              IR  = GlobalCode.Field2Bool(n["IR"])

                          }).ToList();

                return remark;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        public List<CrewAssisRemark> DeleteCrewAssistRemarks(long RemarkID, long TravelReqIdInt,string RamarksVarchar, string UserID)
        {

            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;

            try
            {

                List<CrewAssisRemark> remark = new List<CrewAssisRemark>();

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteCrewAssistRemarks");
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarkID", DbType.Int32, RemarkID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.String, TravelReqIdInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pRamarksVarchar", DbType.String, RamarksVarchar);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                remark = (from n in dt.AsEnumerable()
                          select new CrewAssisRemark
                          {

                              TravelRequestID = GlobalCode.Field2Long(n["TravelRequestID"]),
                              SeafarerID = GlobalCode.Field2Long(n["SeafarerID"]),
                              RemarkID = GlobalCode.Field2Long(n["RemarkID"]),
                              Remark = GlobalCode.Field2String(n["Remark"]),
                              RemarkBy = GlobalCode.Field2String(n["RemarkBy"]),
                              RemarkDate = n.Field<DateTime?>("RemarkDate"),
                              Visible = GlobalCode.Field2String(n["Visible"]),
                              ReqResourceID = GlobalCode.Field2TinyInt(n["colRemarkSourceID"]),
                              Resource = GlobalCode.Field2String(n["colRemarkSource"]),
                              IDBigInt = GlobalCode.Field2Long(n["IDBigInt"]),
                              RecordLocator = GlobalCode.Field2String(n["RecordLocator"]),
                              RemarkTypeID = GlobalCode.Field2Int(n["RemarkTypeID"]),
                              RemarkType = GlobalCode.Field2String(n["RemarkType"]),
                              RemarkStatusID = GlobalCode.Field2TinyInt(n["RemarkStatusID"]),
                              RemarkStatus = GlobalCode.Field2String(n["RemarkStatus"]),
                              SummaryOfCall = GlobalCode.Field2String(n["SummaryCall"]),
                              RemarkRequestorID = GlobalCode.Field2Int(n["RemarkRequestorID"]),
                              RemarkRequestor = GlobalCode.Field2String(n["RemarkRequestor"]),
                              TransactionDate = GlobalCode.Field2DateTime(n["TransactionDate"]),
                              TransactionTime = GlobalCode.Field2DateTime(n["TransactionTime"]),
                              PortCode = GlobalCode.Field2String(n["PortCode"])  ,
                              IR  = GlobalCode.Field2Bool(n["IR"])

                          }).ToList();

                return remark;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        public List<CrewAssisVehicleCost> GetCrewAssisVehicleCost(int VehicleVendorID, long TravelReqID, int VehicleTypeID, string PortCode, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;

            try
            {

                List<CrewAssisVehicleCost> VehicleCost = new List<CrewAssisVehicleCost>();

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetCrewAssistVehicleCost");

                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleVendorID", DbType.Int32, VehicleVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqID", DbType.Int64, TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleTypeID", DbType.Int32, VehicleTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                VehicleCost = (from a in dt.AsEnumerable()
                               select new CrewAssisVehicleCost
                               {
                                   TravelRequestID = a.Field<long>("TravelRequestID"),
                                   IDBigInt = a.Field<long>("RemarkID"),
                                   PortCode = a.Field<string>("PortCode"),
                                   CurrencyCode = a.Field<string>("CurrencyCode"),
                                   Route = a.Field<string>("Route"),
                                   VehicleTypeName = a.Field<string>("VehicleTypeName"),

                                   UnitOfMeasure = a.Field<string>("UnitOfMeasure"),
                                   //TranspoCost = a.Field<string>("Cost"),
                                   TranspoCost = GlobalCode.Field2Double(a["Cost"]).ToString("N2"),
                                   Capacity = a.Field<string>("Capacity"),
                                   VacantSeat = a.Field<string>("VacantSeat"),
                               }).ToList();

                return VehicleCost;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }



        /// <summary>
        /// Date Create:    07/MAR/2014
        /// Create By:      Muhallidin G Wali
        /// (description)  	Get Port Agent Detail
        /// </summary>
        public List<CrewAssistHotelInformation> GetPortAgentHotelVendor(short LoadType, long PortAgentVendorID, long TravelRequestID, long IDBigInt, string PortCode)
        {
            List<CrewAssistHotelInformation> list = new List<CrewAssistHotelInformation>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();
            DbCommand dbComm = null;
            try
            {

                dbComm = db.GetStoredProcCommand("upsGetPortAgentHotelVendor");
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, LoadType);

                db.AddInParameter(dbComm, "@pPortAgentVendorID", DbType.Int64, PortAgentVendorID);
                db.AddInParameter(dbComm, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                db.AddInParameter(dbComm, "@pIDBigInt", DbType.Int64, IDBigInt);
                db.AddInParameter(dbComm, "@pPortCode", DbType.String, PortCode);

                ds = db.ExecuteDataSet(dbComm);
                var SF = (from a in ds.Tables[0].AsEnumerable()
                          select new CrewAssistHotelInformation
                          {

                              VendorBranchName = GlobalCode.Field2String(a["PortAgentVendorName"]),

                              CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                              CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                              ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),

                              VendorId = GlobalCode.Field2Int(a["colVendorIdInt"]),
                              BranchID = GlobalCode.Field2Int(a["PortAgentVendorID"]),

                              ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                              Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                              EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                              EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                              CityName = GlobalCode.Field2String(a["colCityNameVarchar"]),
                              CityCode = GlobalCode.Field2String(a["PortCode"]),
                              FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                              Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),

                              MealVoucher = GlobalCode.Field2Double(a["MealVoucher"]).ToString("N2"),

                              ContractedRate = GlobalCode.Field2Double(a["ContractedRate"]).ToString("N2"),
                              ConfirmRate = GlobalCode.Field2Double(a["ConfirmRate"]).ToString("N2"),
                              ContractRoomRateTaxPercentage = GlobalCode.Field2Double(a["ContractRoomRateTaxPercentage"]).ToString("N2"),
                              IsBreakfast = GlobalCode.Field2Bool(a["colBreakfastBit"]),
                              IsDinner = GlobalCode.Field2Bool(a["colDinnertBit"]),
                              IsLunch = GlobalCode.Field2Bool(a["colLunchBit"]),
                              IsWithShuttle = GlobalCode.Field2Bool(a["colWithShuttleBit"]),
                              RoomTypeID = GlobalCode.Field2Int(a["RoomTypeID"]),
                              CurrencyID = GlobalCode.Field2Int(a["colCurrencyIDInt"]),

                              ContractDateStarted = GlobalCode.Field2Date(a["colContractDateStartedDate"]),
                              ContractDateEnd = GlobalCode.Field2Date(a["colContractDateEndDate"]),

                              ATTEMail = (from n in ds.Tables[1].AsEnumerable()
                                          select new ATTEMail
                                          {
                                              BranchID = GlobalCode.Field2String(n["BranchID"]),
                                              BranchCode = GlobalCode.Field2String(n["BranchCode"]),
                                              BranchName = GlobalCode.Field2String(n["BranchName"]),
                                              Email = GlobalCode.Field2String(n["Email"]),
                                          }).ToList(),

                          }).ToList();


                return SF;

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SavePortAgentHotelRequest(List<HotelTransactionPortAgent> HotelTranPortAgent, string UserID, List<HotelRequestCompanion> HRC)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {



                GlobalCode gc = new GlobalCode();
                DataTable dt = new DataTable();

                dt = gc.getDataTable(HRC);

                dt.Columns.Remove("GENDER");
                dt.Columns.Remove("RECORDLOCATOR");
                dt.Columns.Remove("IsPortAgent"); 



                DataTable dt1 = new DataTable();

                dt1 = gc.getDataTable(HotelTranPortAgent);

                DataColumnCollection columns;
                // Get the DataColumnCollection from a DataTable in a DataSet.
                columns = dt1.Columns;

                if (columns.Contains("IsAutoAirportToHotel"))
                {
                    columns.Remove("IsAutoAirportToHotel");
                }

                if (columns.Contains("IsAutoHotelToShip"))
                {
                    columns.Remove("IsAutoHotelToShip");
                }


                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSaveHotelPortAgentRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                SqlParameter param1 = new SqlParameter("@pHotelTable", dt1);
                SqlParameter param = new SqlParameter("@pCompanionTable", dt);

                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.Structured;

                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;

                SFDbCommand.Parameters.Add(param1);
                SFDbCommand.Parameters.Add(param);

                string IdentityID = SFDatebase.ExecuteScalar(SFDbCommand).ToString();
                return IdentityID;

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

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SaveCancelPortAgentHotelRequest(short LoadType, long TransHotelID, string UserID,
            string Reciever, string Recipient, string BlindCopy, string Comment)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCanAppHotelTransactionPortAgent");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelID", DbType.Int64, TransHotelID);   //BIGINT 
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);              //VARCHAR
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, Reciever);          //VARCHAR
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);        //VARCHAR
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);        //varchar
                SFDatebase.AddInParameter(SFDbCommand, "@pComment", DbType.String, Comment);

                return SFDatebase.ExecuteScalar(SFDbCommand).ToString();

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



        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public List<CrewAssisVehicleCost> GetCrewAssistTransportationCost(short LoadType, int VehicleVendorID, long TravelReqID, int VehicleTypeID, int FromID, int ToID, string PortCode, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = new DataSet();
            try
            {
                List<CrewAssisVehicleCost> VehicleCost = new List<CrewAssisVehicleCost>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetCrewAssistVehiclePortAgentCost");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleVendorID", DbType.Int32, VehicleVendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqID", DbType.Int64, TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleTypeID", DbType.Int32, VehicleTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFromIDInt", DbType.Int32, FromID);
                SFDatebase.AddInParameter(SFDbCommand, "@pToIDInt", DbType.Int64, ToID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                VehicleCost = (from n in ds.Tables[0].AsEnumerable()
                               select new CrewAssisVehicleCost
                               {

                                   PortCode = n["PortCode"].ToString(),
                                   CurrencyCode = n["CurrencyCode"].ToString(),
                                   Route = n["Route"].ToString(),
                                   VehicleTypeName = n["VehicleTypeName"].ToString(),
                                   VehicleTypeID = GlobalCode.Field2Int(n["VehicleTypeID"]),
                                   UnitOfMeasure = n["UnitOfMeasure"].ToString(),
                                   TranspoCost = GlobalCode.Field2Double(n["Cost"]).ToString("N2"),
                                   Capacity = n["Capacity"].ToString(),
                                   VacantSeat = n["VacantSeat"].ToString(),

                               }).ToList();

                return VehicleCost;
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    13/Mar/2014
        /// Description:    Get Vehicle Route
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public string GetVehicleRoute(short LoadType, long TravelReqID, long IDBigInt, int SeqNo, string Status, string Route, string PortCode, int RouteID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            string RouteName = "";

            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("GetVehicleRoute");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigInt", DbType.Int64, IDBigInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNo", DbType.Int32, SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoute", DbType.String, Route);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pRouteID", DbType.Int32, RouteID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                if (dt.Rows.Count > 0) RouteName = dt.Rows[0][0].ToString();

                return RouteName;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    13/Mar/2014
        /// Description:    Get Vehicle Route
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public List<CrewAssistTranspo> DeleteTransportationRequest(short LoadType, long SeafarerID, long TranslID
            , long TransactionID, long IDBigint, long TravelReqID, int SeqNo
            , string UserID, string Sender, string Recipient, string BlindCopy, string Comment)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = new DataSet();

            try
            {
                List<CrewAssistTranspo> list = new List<CrewAssistTranspo>();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteTransportationRequest");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerID", DbType.Int64, SeafarerID);

                SFDatebase.AddInParameter(SFDbCommand, "@pTranslID", DbType.Int64, TranslID);
                SFDatebase.AddInParameter(SFDbCommand, "@TransactionID", DbType.Int64, TransactionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigint", DbType.Int64, IDBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelReqID);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNo", DbType.Int32, SeqNo);

                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID); 
                SFDatebase.AddInParameter(SFDbCommand, "@pSender", DbType.String, Sender);  
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient); 
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);  
                SFDatebase.AddInParameter(SFDbCommand, "@pComment", DbType.String, Comment);


                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                list = (from Q in ds.Tables[0].AsEnumerable()
                        select new CrewAssistTranspo
                        {
                            ReqVehicleIDBigint = GlobalCode.Field2Long(Q["colReqVehicleIDBigint"]),
                            IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                            TravelReqIDInt = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                            SeqNoInt = GlobalCode.Field2TinyInt(Q["colSeqNoInt"]),
                            RecordLocatorVarchar = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                            VehicleVendorIDInt = GlobalCode.Field2Long(Q["colVehicleVendorIDInt"]),
                            VehicleVendor = GlobalCode.Field2String(Q["colVehicleVendorNameVarchar"]),
                            VehiclePlateNoVarchar = GlobalCode.Field2String(Q["colVehiclePlateNoVarchar"]),
                            PickUpDate = GlobalCode.Field2DateTime(Q["colPickUpDate"]),
                            PickUpTime = GlobalCode.Field2DateTime(Q["colPickUpTime"]),
                            DropOffDate = GlobalCode.Field2DateTime(Q["colDropOffDate"]),
                            DropOffTime = GlobalCode.Field2DateTime(Q["colDropOffTime"]),
                            ConfirmationNoVarchar = GlobalCode.Field2String(Q["colConfirmationNoVarchar"]),
                            VehicleStatusVarchar = GlobalCode.Field2String(Q["colVehicleStatusVarchar"]),
                            VehicleTypeIdInt = GlobalCode.Field2Int(Q["colVehicleTypeIdInt"]),
                            SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                            RouteIDFromInt = GlobalCode.Field2Int(Q["colRouteIDFromInt"]),
                            RouteIDToInt = GlobalCode.Field2Int(Q["colRouteIDToInt"]),
                            FromVarchar = GlobalCode.Field2String(Q["colFromVarchar"]),
                            ToVarchar = GlobalCode.Field2String(Q["colToVarchar"]),
                            Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                            Email = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                            RouteFrom = GlobalCode.Field2String(Q["colRouteFromVarchar"]),
                            RouteTo = GlobalCode.Field2String(Q["colRouteToVarchar"]),
                            TranspoCost = GlobalCode.Field2Double(Q["colConfirmRateMoney"]).ToString("N2"),
                            VehicleTransID = GlobalCode.Field2Long(Q["colReqVehicleIDBigint"]),

                            ColorCode = GlobalCode.Field2String(Q["colColorCodevarchar"]),
                            ForeColor = GlobalCode.Field2String(Q["coldForeColorVarchar"]),
                            StatusID = GlobalCode.Field2Int(Q["colStatusID"])

                        }).ToList();
                return list;
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
                if (ds != null)
                {
                    ds.Dispose();
                }


            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    20/Jun/2014
        /// Description:    Cancel Hotel booking
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public List<SeafarerDetailHeader> GetCancelCrewAssistHotelRequest(short LoadType, long HotelTransID, string UserID
                                    , string Email, string CCEmail, string Confirmby
                                    , string Comment, DateTime CancelDate, string Timezone, string GMTDATE)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = new DataSet();

            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCrewAssistCancelHotelRequest");
                SFDatebase.AddInParameter(SFDbCommand,"@pLoadType", DbType.Int64, LoadType);
                SFDatebase.AddInParameter(SFDbCommand,"@pHotelTransID", DbType.Int64, HotelTransID);
                SFDatebase.AddInParameter(SFDbCommand,"@pUserVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand,"@pEmailVarchar", DbType.String, Email);
                SFDatebase.AddInParameter(SFDbCommand,"@pCCEmailVarchar", DbType.String, CCEmail);
                SFDatebase.AddInParameter(SFDbCommand,"@pConfirmbyVarchar", DbType.String, Confirmby);
                SFDatebase.AddInParameter(SFDbCommand,"@pCommentVarchar", DbType.String, Comment);
                SFDatebase.AddInParameter(SFDbCommand,"@pCancelDate", DbType.DateTime, CancelDate);
                SFDatebase.AddInParameter(SFDbCommand,"@pTimezone", DbType.String, Timezone);
                SFDatebase.AddInParameter(SFDbCommand,"@pGMTDATE", DbType.String, GMTDATE);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                return ProcessSeafarerDetailHeader(ds, UserID);

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    20/Jun/2014
        /// Description:    Cancel Transpotation booking
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public List<SeafarerDetailHeader> GetCrewAssistCancelTransportationRequest(short LoadType, long VehicleTransID, long TravelRequestID
                                    , long RecLocID, int SeqNo, string UserID, string Email, string CCEmail, string Confirmby
                                    , string Comment, DateTime CancelDate, string Timezone, string GMTDATE) {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = new DataSet();
            try {

                List<SeafarerDetailHeader> res = new List<SeafarerDetailHeader>();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCrewAssistCancelTransportionRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehTransID", DbType.Int64, VehicleTransID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pEmailVarchar", DbType.String, Email);
                SFDatebase.AddInParameter(SFDbCommand, "@pCCEmailVarchar", DbType.String, CCEmail);
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmbyVarchar", DbType.String, Confirmby);
                SFDatebase.AddInParameter(SFDbCommand, "@pCommentVarchar", DbType.String, Comment);
                SFDatebase.AddInParameter(SFDbCommand, "@pCancelDate", DbType.DateTime, CancelDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, Timezone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.String, GMTDATE);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                return ProcessSeafarerDetailHeader(ds, UserID);
                 
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

                if (ds != null)
                {
                    ds.Dispose();
                }
            }

        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    20/Jun/2014
        /// Description:    check overflow Hotel
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public string GetHotelTransactionOverFlow(long SeafarerID, DateTime CheckInDate, DateTime CheckoutDate
            , long IDBigint, short SeqNo, long TravelReqId, int BranchID, string PortCode, long TransHotelID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            string res = "";
            try
            {

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelTransacttionOverFlow");
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerID", DbType.Int64, SeafarerID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.DateTime, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckoutDate", DbType.DateTime, CheckoutDate);

                SFDatebase.AddInParameter(SFDbCommand, "@pIDBigint", DbType.Int64, IDBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNo", DbType.Int16, SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqId", DbType.Int64, TravelReqId);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortCode", DbType.String, PortCode);
                SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelID", DbType.Int64, TransHotelID);
               
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                res = dt.Rows[0]["OverFlowBranch"].ToString();

                return res;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }        
    
    
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    22/Aug/2014
        /// Description:    Get all past date request
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        /// (short loadtype, long SeafarerID, string UserID)
        public List<SeafarerDetailHeader> GetAllPastTravelRequest(short loadtype, long SeafarerID, string UserID)
        {
            DataSet ds = null;
            DataTable dt = null;
            DbCommand comm = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetAllPastTravelRequest");
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, loadtype);
                db.AddInParameter(comm, "@pSeafarerID", DbType.Int64, SeafarerID);
                ds = db.ExecuteDataSet(comm);

                return ProcessSeafarerDetailHeader(ds, UserID);

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

                if (ds != null)
                {
                    ds.Dispose();
                }
            }

        }


        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Send SF Companion Hotel Request 
        /// </summary>
        public void SendCompanionHotelTransaction(long RequestIDInt, string UserID  , string Reciever, string Recipient , string BlindCopy, bool IsOverFlowRequest )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSendCompanionHotelTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int64, RequestIDInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String ,  UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pReciever", DbType.String, Reciever);
                SFDatebase.AddInParameter(SFDbCommand, "@pRecipient", DbType.String, Recipient);
                SFDatebase.AddInParameter(SFDbCommand, "@pBlindCopy", DbType.String, BlindCopy);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsOverFlowRequest", DbType.Boolean, IsOverFlowRequest);
 
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


        /// <summary>
        /// Created By:    Muhallidin G Wali
        /// Date Modified: 10/24/2014
        /// (description)  Insert Air Status.
        /// </summary>
        public List<SeafarerDetailHeader> InsertAirTransactionStatus(long TravelRequestID, long IdBigint
                    , int SeqNo, int StatusID, string OldStatus, string UserID)
        {
            DataSet ds = null;
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {
                 
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertAirTransactionStatus");
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatusID", DbType.Int32, StatusID);
                SFDatebase.AddInParameter(SFDbCommand, "@pOldStatus", DbType.String, OldStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                return ProcessSeafarerDetailHeader(ds, UserID);
                
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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }


        public List<SeafarerDetailHeader> ProcessSeafarerDetailHeader(DataSet ds, string UserID)
        {
            try
            {
                List<SeafarerDetailHeader> Slist = new List<SeafarerDetailHeader>();

                if (ds == null) return Slist;

                return (from a in ds.Tables[0].AsEnumerable()
                          select new SeafarerDetailHeader
                          {
                              TravelRequetID = GlobalCode.Field2Long(a["colTravelReqIdInt"]),
                              SeafarerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                              Gender = GlobalCode.Field2String(a["colGenderDiscription"]),
                              GenderID = GlobalCode.Field2Int(a["colGenderIDInt"]),
                              NationalityID = GlobalCode.Field2Int(a["colNatioalityIdInt"]),
                              Status = GlobalCode.Field2String(a["colStatusVarchar"]),
                              NationalityCode = GlobalCode.Field2String(a["colNationalityCodeVarchar"]),
                              Nationality = GlobalCode.Field2String(a["colNationalityDescriptionVarchar"]),
                              VesselID = GlobalCode.Field2Int(a["colVesselIdInt"]),
                              VesselCode = GlobalCode.Field2String(a["colVesselCodeVarchar"]),
                              Vessel = GlobalCode.Field2String(a["colVesselLongCodeVarchar"]),
                              RequestDate = a.Field<DateTime?>("RequestDate"),
                              PortID = GlobalCode.Field2Int(a["colPortIdInt"]),
                              PortCode = GlobalCode.Field2String(a["colPortCodeVarchar"]),
                              PortName = GlobalCode.Field2String(a["colPortNameVarchar"]),
                              RankID = GlobalCode.Field2Int(a["colRankIDInt"]),
                              RankCode = GlobalCode.Field2String(a["colRankCodeVarchar"]),
                              RankName = GlobalCode.Field2String(a["colRankNameVarchar"]),
                              CostCenterID = GlobalCode.Field2Int(a["colCostCenterIDInt"]),
                              CostCenterCode = GlobalCode.Field2String(a["colCostCenterCodeVarchar"]),
                              CostCenterName = GlobalCode.Field2String(a["colCostCenterNameVarchar"]),

                              HotelComments = GlobalCode.Field2String(a["Comments"]),
                              FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                              LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),

                              BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                              BrandCode = GlobalCode.Field2String(a["colBrandCodeVarchar"]),
                              Brand = GlobalCode.Field2String(a["colBrandNameVarchar"]),
                              CrewAssistRequestID = GlobalCode.Field2Long(a["colRequestIDBigint"]),
                              CrewAssistRequestNo = GlobalCode.Field2String(a["colRequestNo"]),
                              ReasonCode = GlobalCode.Field2String(a["colReasonCodeVarchar"]),
                              CrewAssistAirTransaction = (from n in ds.Tables[1].AsEnumerable()
                                                          where GlobalCode.Field2Long(a["colTravelReqIdInt"]) == GlobalCode.Field2Long(n["colTravelReqIdInt"])
                                                          select new CrewAssistAirTransaction
                                                          {
                                                              TravelReqId = GlobalCode.Field2Long(n["colTravelReqIdInt"]),
                                                              RecordLocator = GlobalCode.Field2String(n["colRecordLocatorVarchar"]),
                                                              ItinerarySeqNo = GlobalCode.Field2TinyInt(n["colItinerarySeqNoSmallint"]),
                                                              IdBigint = GlobalCode.Field2Long(n["colIdBigint"]),
                                                              SeqNo = GlobalCode.Field2Int(n["colSeqNoInt"]),
                                                              ArrivalDateTime = GlobalCode.Field2DateTime(n["colArrivalDateTime"]),
                                                              DepartureDateTime = GlobalCode.Field2DateTime(n["colDepartureDateTime"]),
                                                              DepartureAirportLocationCode = GlobalCode.Field2String(n["colDepartureAirportLocationCodeVarchar"]),
                                                              ArrivalAirportLocationCode = GlobalCode.Field2String(n["colArrivalAirportLocationCodeVarchar"]),
                                                              SeatNo = GlobalCode.Field2String(n["colSeatNoVarchar"]),
                                                              TicketNo = GlobalCode.Field2String(n["colTicketNoVarchar"]),
                                                              AirStatus = GlobalCode.Field2String(n["colAirStatusVarchar"]),
                                                              IsBilledToCrew = GlobalCode.Field2Bool(n["colIsBilledToCrewBit"]),
                                                              ActionCode = GlobalCode.Field2String(n["colActionCodeVarchar"]),
                                                              AirlineCode = GlobalCode.Field2String(n["colMarketingAirlineCodeVarchar"]),
                                                              OrderNo = GlobalCode.Field2Int(n["colOrderNo"]),
                                                              HotelRequestID = GlobalCode.Field2Long(n["colRequestIDBigint"]),
                                                              BranchID = GlobalCode.Field2Int(n["colBranchIDInt"]),
                                                              RoomTypeID = GlobalCode.Field2Int(n["colRoomTypeIDInt"]),
                                                              TimeSpanStartDate = GlobalCode.Field2DateTime(n["colTimeSpanStartDate"]),
                                                              TimeSpanStartTime = GlobalCode.Field2DateTime(n["colTimeSpanStartTime"]),
                                                              TimeSpanEndDate = GlobalCode.Field2DateTime(n["colTimeSpanEndDate"]),
                                                              TimeSpanEndTime = GlobalCode.Field2DateTime(n["colTimeSpanEndTime"]),
                                                              Duration = GlobalCode.Field2Int(n["colTimeSpanDurationInt"]),
                                                              ReqVehicleID = GlobalCode.Field2Long(n["colReqVehicleIDBigint"]),
                                                              ReqMeetAndGreetID = GlobalCode.Field2Long(n["colReqMeetAndGreetIDBigint"]),
                                                              ReqPortAgentID = GlobalCode.Field2Long(n["colReqPortAgentIDBigint"]),
                                                              FligthNo = GlobalCode.Field2String(n["colFlightNoVarchar"]),
                                                              
                                                              StatusID = GlobalCode.Field2Int(n["colStatusIDInt"]),
                                                              Status = GlobalCode.Field2String(n["colStatus"]),
                                                              StatusMessage = GlobalCode.Field2String(n["StatusMessage"]),
                                                          
                                                          }).ToList(),
                              SeafarerDetailList = (from B in ds.Tables[2].AsEnumerable()
                                                    select new SeafarerDetailList
                                                    {
                                                        TravelRequetID = GlobalCode.Field2Long(B["colTravelReqIdInt"]),
                                                        SeafarerID = GlobalCode.Field2Long(B["colSeafarerIdInt"]),
                                                        Gender = GlobalCode.Field2String(B["colGenderDiscription"]),
                                                        GenderID = GlobalCode.Field2Int(B["colGenderIDInt"]),
                                                        NationalityID = GlobalCode.Field2Int(B["colNatioalityIdInt"]),
                                                        Status = GlobalCode.Field2String(B["colStatusVarchar"]),
                                                        NationalityCode = GlobalCode.Field2String(B["colNationalityCodeVarchar"]),
                                                        Nationality = GlobalCode.Field2String(B["colNationalityDescriptionVarchar"]),
                                                        VesselID = GlobalCode.Field2Int(B["colVesselIdInt"]),
                                                        VesselCode = GlobalCode.Field2String(B["colVesselCodeVarchar"]),
                                                        Vessel = GlobalCode.Field2String(B["colVesselLongCodeVarchar"]),
                                                        RequestDate = B.Field<DateTime?>("RequestDate"),
                                                        PortID = GlobalCode.Field2Int(B["colPortIdInt"]),
                                                        PortCode = GlobalCode.Field2String(B["colPortCodeVarchar"]),
                                                        PortName = GlobalCode.Field2String(B["colPortNameVarchar"]),
                                                        RankID = GlobalCode.Field2Int(B["colRankIDInt"]),
                                                        RankCode = GlobalCode.Field2String(B["colRankCodeVarchar"]),
                                                        RankName = GlobalCode.Field2String(B["colRankNameVarchar"]),
                                                        CostCenterID = GlobalCode.Field2Int(B["colCostCenterIDInt"]),
                                                        CostCenterCode = GlobalCode.Field2String(B["colCostCenterCodeVarchar"]),
                                                        CostCenterName = GlobalCode.Field2String(B["colCostCenterNameVarchar"]),

                                                        FirstName = GlobalCode.Field2String(B["colFirstNameVarchar"]),
                                                        LastName = GlobalCode.Field2String(B["colLastNameVarchar"]),

                                                        BrandID = GlobalCode.Field2Int(B["colBrandIdInt"]),
                                                        BrandCode = GlobalCode.Field2String(B["colBrandCodeVarchar"]),
                                                        Brand = GlobalCode.Field2String(B["colBrandNameVarchar"]),

                                                        CrewAssistRequestID = GlobalCode.Field2Long(B["colRequestIDBigint"]),
                                                        CrewAssistRequestNo = GlobalCode.Field2String(B["colRequestNo"]),

                                                        ReqVehicleID = GlobalCode.Field2Long(B["colReqVehicleIDBigint"]),
                                                        ReqMeetAndGreetID = GlobalCode.Field2Long(B["colReqMeetAndGreetIDBigint"]),
                                                        ReqPortAgentID = GlobalCode.Field2Long(B["colReqPortAgentIDBigint"]),
                                                        IDBigint = GlobalCode.Field2Long(B["colIDBigInt"]),
                                                        RecordLocator = GlobalCode.Field2String(B["colRecordLocatorVarchar"]),
                                                        ReqSafeguardID = GlobalCode.Field2Long(B["colReqSafeguardIDBigint"]),
                                                        ReasonCode = GlobalCode.Field2String(B["colReasonCodeVarchar"]),
                                                        IsDeleted = GlobalCode.Field2Bool(B["colIsDeletedFromCHBit"]),
                                                        LOEStatus = GlobalCode.Field2String(B["LOEStatus"]),

                                                        LOEDate = B.Field<DateTime?>("LOEDate"),

                                                        LOEImmigrationOfficer = GlobalCode.Field2String(B["LOEImmigrationOfficer"]),
                                                        LOEImmigrationPlace = GlobalCode.Field2String(B["LOEImmigrationPlace"]),

                                                        LOEReason = GlobalCode.Field2String(B["Reason"]),

                                                        //IsConfirm = B.Field<bool?>("IsConfirmed"),


                                                        ContractStatus = GlobalCode.Field2String(B["colContractStatus"]),
                                                        AirStatus = GlobalCode.Field2String(B["colAirStatus"]),
                                                        BookingStatus = GlobalCode.Field2String(B["colBookingStatus"]),
                                                        AppointmentStatus = GlobalCode.Field2String(B["colAssignmentStatus"]),


                                                    }).ToList(),

                              CrewAssistShipEmail = (from s in ds.Tables[3].AsEnumerable()
                                                     select new CrewAssistShipEmail
                                                     {
                                                         VesselName = GlobalCode.Field2String(s["colVesselNameVarchar"]),
                                                         VesselCode = GlobalCode.Field2String(s["colVesselCodeVarchar"]),
                                                         Email = GlobalCode.Field2String(s["Email"]),
                                                     }).ToList(),

                              CopyEmail = (from s in ds.Tables[4].AsEnumerable()
                                           select new CopyEmail
                                           {
                                               EmailName = GlobalCode.Field2String(s["EmailType"]),
                                               EmailType = GlobalCode.Field2Int(s["EmailID"]),
                                               Email = GlobalCode.Field2String(s["Email"]),
                                           }).ToList(),

                              CrewAssistTranspo = (from Q in ds.Tables[5].AsEnumerable()
                                                   select new CrewAssistTranspo
                                                   {
                                                       ReqVehicleIDBigint = GlobalCode.Field2Long(Q["colReqVehicleIDBigint"]),
                                                       IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                       TravelReqIDInt = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                       SeqNoInt = GlobalCode.Field2TinyInt(Q["colSeqNoInt"]),
                                                       RecordLocatorVarchar = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                                                       VehicleVendorIDInt = GlobalCode.Field2Long(Q["colVehicleVendorIDInt"]),
                                                       VehiclePlateNoVarchar = GlobalCode.Field2String(Q["colVehiclePlateNoVarchar"]),
                                                       PickUpDate = GlobalCode.Field2DateTime(Q["colPickUpDate"]),
                                                       PickUpTime = GlobalCode.Field2DateTime(Q["colPickUpTime"]),
                                                       DropOffDate = GlobalCode.Field2DateTime(Q["colDropOffDate"]),
                                                       DropOffTime = GlobalCode.Field2DateTime(Q["colDropOffTime"]),
                                                       ConfirmationNoVarchar = GlobalCode.Field2String(Q["colConfirmationNoVarchar"]),
                                                       VehicleStatusVarchar = GlobalCode.Field2String(Q["colVehicleStatusVarchar"]),
                                                       VehicleTypeIdInt = GlobalCode.Field2Int(Q["colVehicleTypeIdInt"]),
                                                       SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                       RouteIDFromInt = GlobalCode.Field2Int(Q["colRouteIDFromInt"]),
                                                       RouteIDToInt = GlobalCode.Field2Int(Q["colRouteIDToInt"]),
                                                       FromVarchar = GlobalCode.Field2String(Q["colFromVarchar"]),
                                                       ToVarchar = GlobalCode.Field2String(Q["colToVarchar"]),
                                                       Comment = GlobalCode.Field2String(Q["colRemarksForAuditVarchar"]),
                                                       Email = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                                                       RouteFrom = GlobalCode.Field2String(Q["colRouteFromVarchar"]),
                                                       RouteTo = GlobalCode.Field2String(Q["colRouteToVarchar"]),
                                                       TranspoCost = GlobalCode.Field2Double(Q["colConfirmRateMoney"]).ToString("N2"),
                                                       VehicleTransID = GlobalCode.Field2Long(Q["colTransVehicleIDBigint"])
                                                   }).ToList(),

                              CrewAssistMeetAndGreet = (from Q in ds.Tables[7].AsEnumerable()
                                                        select new CrewAssistMeetAndGreet
                                                        {
                                                            ReqMeetAndGreetID = GlobalCode.Field2Long(Q["colReqMeetAndGreetIDBigint"]),
                                                            IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                            TravelReqID = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                            SeqNo = GlobalCode.Field2Int(Q["colSeqNoInt"]),
                                                            RecordLocator = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                                                            MeetAndGreetVendorID = GlobalCode.Field2Int(Q["colMeetAndGreetVendorIDInt"]),
                                                            MeetAndGreetVendor = GlobalCode.Field2String(Q["colMeetAndGreetVendorNameVarchar"]),
                                                            ConfirmationNo = GlobalCode.Field2String(Q["colConfirmationNoVarchar"]),
                                                            AirportID = GlobalCode.Field2Int(Q["colAirportInt"]),
                                                            AirportCode = GlobalCode.Field2String(Q["AirportName"]),
                                                            FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                                                            ServiceDate = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                                                            Rate = GlobalCode.Field2Double(Q["colRateFloat"]),
                                                            SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                            Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                                                            IsAir = GlobalCode.Field2Bool(Q["colIsAirBit"]),
                                                            Email = GlobalCode.Field2String(Q["colEmailToVarchar"])
                                                        }).ToList(),


                              CrewAssistPortAgentRequest = (from Q in ds.Tables[8].AsEnumerable()
                                                            select new CrewAssistPortAgentRequest
                                                            {

                                                                ReqPortAgentID = GlobalCode.Field2Long(Q["colReqPortAgentIDBigint"]),
                                                                IdBigint = GlobalCode.Field2Long(Q["colIdBigint"]),
                                                                TravelReqID = GlobalCode.Field2Long(Q["colTravelReqIDInt"]),
                                                                SeqNo = GlobalCode.Field2Int(Q["colSeqNoInt"]),
                                                                RecordLocator = GlobalCode.Field2String(Q["colRecordLocatorVarchar"]),
                                                                PortAgentVendorID = GlobalCode.Field2Int(Q["colPortAgentVendorIDInt"]),
                                                                PortCodeID = GlobalCode.Field2Int(Q["colPortIdInt"]),
                                                                PortCode = GlobalCode.Field2String(Q["colPortCode"]),
                                                                AirportID = GlobalCode.Field2Int(Q["colAirportIDInt"]),
                                                                AirportCode = GlobalCode.Field2String(Q["colAirportCode"]),
                                                                FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                                                                ServiceDatetime = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                                                                SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                                                Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                                                                IsMAG = GlobalCode.Field2Bool(Q["colIsMAGBit"]),
                                                                IsTrans = GlobalCode.Field2Bool(Q["colIsTransBit"]),
                                                                IsHotel = GlobalCode.Field2Bool(Q["colIsHotelBit"]),
                                                                IsLuggage = GlobalCode.Field2Bool(Q["colIsLuggageBit"]),
                                                                IsSafeguard = GlobalCode.Field2Bool(Q["colIsSafeguardBit"]),
                                                                IsVisa = GlobalCode.Field2Bool(Q["colIsVisaBit"]),
                                                                IsOther = GlobalCode.Field2Bool(Q["colIsOtherBit"]),
                                                                IsAir = GlobalCode.Field2Bool(Q["colIsAirBit"]),
                                                                PortAgentVendorName = GlobalCode.Field2String(Q["colPortAgentVendorNameVarchar"]),
                                                                CityID = GlobalCode.Field2Int(Q["colCityIDInt"]),
                                                                ContactNo = GlobalCode.Field2String(Q["colContactNoVarchar"]),
                                                                FaxNo = GlobalCode.Field2String(Q["colFaxNoVarchar"]),
                                                                ContactPerson = GlobalCode.Field2String(Q["colContactPersonVarchar"]),
                                                                Address = GlobalCode.Field2String(Q["colAddressVarchar"]),
                                                                EmailCc = GlobalCode.Field2String(Q["colEmailCcVarchar"]),
                                                                EmailTo = GlobalCode.Field2String(Q["colEmailToVarchar"]),
                                                                Website = GlobalCode.Field2String(Q["colWebsiteVarchar"])

                                                            }).ToList(),

                              CrewAssistSafeguardRequest = (from p in ds.Tables[9].AsEnumerable()
                                                            select new CrewAssistSafeguardRequest
                                                            {
                                                                ReqSafeguardID = GlobalCode.Field2Long(p["colReqSafeguardIDBigint"]),
                                                                IdBigint = GlobalCode.Field2Long(p["colIdBigint"]),
                                                                TravelReqID = GlobalCode.Field2Long(p["colTravelReqIDInt"]),
                                                                SeqNo = GlobalCode.Field2TinyInt(p["colSeqNoInt"]),
                                                                RecordLocator = GlobalCode.Field2String(p["colRecordLocatorVarchar"]),
                                                                SafeguardVendorID = GlobalCode.Field2Int(p["colSafeguardVendorIDInt"]),
                                                                TypeID = GlobalCode.Field2Int(p["colTypeIDInt"]),
                                                                ContractId = GlobalCode.Field2Int(p["colContractIdInt"]),
                                                                ContractServiceTypeID = GlobalCode.Field2Int(p["colContractServiceTypeIDInt"]),
                                                                SFStatus = GlobalCode.Field2String(p["colSFStatus"]),
                                                                Comments = GlobalCode.Field2String(p["colCommentsVarchar"]),
                                                                TransactionDate = GlobalCode.Field2DateTime(p["colTransactionDate"]),
                                                                TransactionTime = GlobalCode.Field2DateTime(p["colTransactionTime"]),
                                                                SGRate = GlobalCode.Field2Double(p["colRateAmount"]).ToString("N2"),

                                                                Address = GlobalCode.Field2String(p["colAddressVarchar"]),
                                                                ContactNo = GlobalCode.Field2String(p["colContactNoVarchar"]),
                                                                EmailTo = GlobalCode.Field2String(p["colEmailToVarchar"]),
                                                            }).ToList(),

                              CrewAssistHotelBooking = (from r in ds.Tables[10].AsEnumerable()
                                                        select new CrewAssistHotelBooking
                                                        {

                                                            SeafarerIDInt = GlobalCode.Field2Long(r["colSeafarerIDInt"]),

                                                            TransHotelID = GlobalCode.Field2Long(r["colTransHotelIDBigInt"]),
                                                            IDBigInt = GlobalCode.Field2Long(r["colIdBigint"]),
                                                            TravelReqID = GlobalCode.Field2Long(r["colTravelReqIDInt"]),
                                                            SeqNo = GlobalCode.Field2TinyInt(r["colSeqNoInt"]),
                                                            RecordLocator = GlobalCode.Field2String(r["colRecordLocatorVarchar"]),
                                                            SfStatus = GlobalCode.Field2String(r["colSFStatus"]),
                                                            RoomType = GlobalCode.Field2String(r["RoomType"]),
                                                            RoomTypeID = GlobalCode.Field2Int(r["colRoomTypeIDInt"]),
                                                            CheckinDate = GlobalCode.Field2DateTime(r["colTimeSpanStartDate"]),
                                                            CheckoutDate = GlobalCode.Field2DateTime(r["colTimeSpanEndDate"]),
                                                            NoNitesInt = GlobalCode.Field2Int(r["colTimeSpanDurationInt"]),
                                                            HotelIDInt = GlobalCode.Field2Int(r["colBranchIDInt"]),
                                                            Branch = GlobalCode.Field2String(r["colVendorBranchNameVarchar"]),
                                                            HotelStatus = GlobalCode.Field2String(r["colHotelStatusVarchar"]),

                                                            HotelComment = GlobalCode.Field2String(r["colCommentsVarchar"]),

                                                            RoomRateTaxInclusive = GlobalCode.Field2Bool(r["colRoomRateTaxInclusive"]),
                                                            RequestID = GlobalCode.Field2Long(r["colRequestIDInt"]),


                                                            MealVoucherMoney = GlobalCode.Field2Double(r["colMealVoucherMoney"]),
                                                            ConfirmRateMoney = GlobalCode.Field2Double(r["colConfirmRateMoney"]),
                                                            ContractedRateMoney = GlobalCode.Field2Double(r["colContractedRateMoney"]),
                                                            HotelEmail = GlobalCode.Field2String(r["colEmailToVarchar"]),

                                                            MealBreakfastBit = GlobalCode.Field2Bool(r["colBreakfastBit"]),
                                                            MealLunchBit = GlobalCode.Field2Bool(r["colLunchBit"]),
                                                            MealDinnerBit = GlobalCode.Field2Bool(r["colDinnerBit"]),
                                                            MealLunchDinnerBit = GlobalCode.Field2Bool(r["colLunchOrDinnerBit"]),
                                                            WithShuttleBit = GlobalCode.Field2Bool(r["colWithShuttleBit"]),
                                                            RoomCountFloat = GlobalCode.Field2Int(r["colRoomCountFloat"]),


                                                            RoomAmount = GlobalCode.Field2String(r["colRoomAmountMoney"]),
                                                            RoomRateTaxPercentage = GlobalCode.Field2Double(r["colRoomRateTaxPercentage"]),
                                                            Currency = GlobalCode.Field2Int(r["colCurrency"]),
                                                            isAir = GlobalCode.Field2Bool(r["colIsAir"]),

                                                            StatusID = GlobalCode.Field2Int(r["colStatusId"]),

                                                            ColorCode = GlobalCode.Field2String(r["colColorCodevarchar"]),
                                                            ForeColor = GlobalCode.Field2String(r["coldForeColorVarchar"]),
                                                            IsPortAgent = GlobalCode.Field2Bool(r["colIsPortAgent"]),
                                                            ReasonCode = GlobalCode.Field2String(r["colReasonCode"]),
                                                            IsMedical = GlobalCode.Field2Bool(r["colIsMedical"]),


                                                            CancellationTermsInt = GlobalCode.Field2Int(r["colCancellationTermsInt"]),
                                                            HotelTimeZoneID = GlobalCode.Field2String(r["colHotelTimeZoneIDVarchar"]),
                                                            CutOffTime = GlobalCode.Field2DateTime(r["colCutOffTime"]),

                                                            IsConfirmed = GlobalCode.Field2Bool(r["IsConfirmed"]),
                                                             

                                                        }).ToList(),


                              Remark = (from n in ds.Tables[11].AsEnumerable()
                                        select new CrewAssisRemark
                                        {

                                            TravelRequestID = GlobalCode.Field2Long(n["TravelRequestID"]), 
                                            SeafarerID = GlobalCode.Field2Long(n["SeafarerID"]), 
                                            RemarkID = GlobalCode.Field2Long(n["RemarkID"]), 
                                            Remark = GlobalCode.Field2String(n["Remark"]), 
                                            RemarkBy = GlobalCode.Field2String(n["RemarkBy"]), 
                                            RemarkDate = n.Field<DateTime?>("RemarkDate"),
                                            Visible = GlobalCode.Field2String(n["Visible"]),
                                            ReqResourceID = GlobalCode.Field2TinyInt(n["colRemarkSourceID"]),
                                            Resource = GlobalCode.Field2String(n["colRemarkSource"]), 
                                            IDBigInt = GlobalCode.Field2Long(n["IDBigInt"]), 
                                            RecordLocator = GlobalCode.Field2String(n["RecordLocator"]), 
                                            RemarkTypeID = GlobalCode.Field2Int(n["RemarkTypeID"]), 
                                            RemarkType = GlobalCode.Field2String(n["RemarkType"]), 
                                            RemarkStatusID = GlobalCode.Field2TinyInt(n["RemarkStatusID"]), 
                                            RemarkStatus = GlobalCode.Field2String(n["RemarkStatus"]), 
                                            SummaryOfCall = GlobalCode.Field2String(n["SummaryCall"]),
                                            RemarkRequestorID = GlobalCode.Field2Int(n["RemarkRequestorID"]),
                                            RemarkRequestor = GlobalCode.Field2String(n["RemarkRequestor"]),
                                            TransactionDate = GlobalCode.Field2DateTime(n["TransactionDate"]),
                                            TransactionTime = GlobalCode.Field2DateTime(n["TransactionTime"])   ,
                                            IR = GlobalCode.Field2Bool(n["IR"])
                                        }).ToList()

                          }).ToList();
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

            }
       
        }

        /// <summary>
        /// Date Created:  01/08/2016
        /// Created By:    Muha Wali
        /// (description)  
        /// ---------------------------------------------
        /// Date Modified:  01/Sep/2016
        /// Modified By:    Josephine Monteza
        /// (description)   Close SFDbCommand
        /// ---------------------------------------------
        /// </summary>        
        public ArrayList GetVendorContractAmount(short LoadType ,int BranchID, int RoomID, DateTime checkindate)
        {
            ArrayList arr = new ArrayList();
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataTable dt = null;
            DbCommand SFDbCommand = null;
            try
            {
                 
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetVendorContractAmount");

                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRequestDate", DbType.DateTime, checkindate);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0] ; 
                if (dt.Rows.Count > 0)
                {
                    arr.Add(dt.Rows[0][0].ToString());
                    arr.Add(dt.Rows[0][1].ToString());
                }
                else { 
                    arr.Add( "0.00");
                    arr.Add( "0.00");
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
            }
            return arr;
        }


    }
}
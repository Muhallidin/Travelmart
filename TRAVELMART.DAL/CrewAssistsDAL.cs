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

namespace TRAVELMART.DAL
{
    public class CrewAssistsDAL
    {


        /// <summary>
        /// Author:      Muhallidin G Wali
        /// Created:     16/Nov/2013
        /// description  Get Vendors of Port Selected
        /// 
        /// </summary>
        public List<CrewAssistGenericVendor> GetGenericVendors(Int16 iLoadTpe, string sUsername, int iSeaPortID, int iAirPortID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds = null;

            List<CrewAssistGenericVendor> CrewAssistGenericVendor = new List<CrewAssistGenericVendor>();


            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetCrewAssistVendor");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadTypeInt", DbType.Int16, iLoadTpe);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, sUsername);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, iSeaPortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.String, iAirPortID);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                CrewAssistGenericVendor.Add(new CrewAssistGenericVendor
                {

                    listHotel = (from a in ds.Tables[0].AsEnumerable()
                                 select new CrewAssistHotelList
                                 {
                                     HotelID = GlobalCode.Field2Int(a["BranchID"]),
                                     HotelName = GlobalCode.Field2String(a["BranchName"]),
                                 }).ToList(),

                    listVehicle = (from a in ds.Tables[1].AsEnumerable()
                                   select new VehicleVendor
                                   {
                                       VehicleID = GlobalCode.Field2Int(a["VendorID"]),
                                       Vehicle = GlobalCode.Field2String(a["VehicleName"]),
                                   }).ToList(),

                    listPortAgent = (from a in ds.Tables[2].AsEnumerable()
                                     select new PortAgentDTO
                                     {
                                         PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                                         PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),
                                     }).ToList(),

                    listMeetGreet = (from a in ds.Tables[3].AsEnumerable()
                                     select new MeetAndGreetList
                                     {
                                         MeetandGreetVendorId = GlobalCode.Field2Int(a["MeetAndGreetID"]),
                                         MeetAndGreetVendorName = GlobalCode.Field2String(a["MeetAndGreetName"]),
                                     }).ToList(),

                    listSafeguard = (from a in ds.Tables[4].AsEnumerable()
                                     select new VendorSafeguardList
                                     {
                                         SafeguardID = GlobalCode.Field2Int(a["SafeguardID"]),
                                         VendorName = GlobalCode.Field2String(a["SafeguardName"]),
                                     }).ToList(),


                });

                return CrewAssistGenericVendor;
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




        public List<CrewMemberInformation> GetCrewAssitCMInformation(short loadType, long SeafarerID, string UserID)
        {

            DataSet ds = null;
            DataTable dt = null;
            DbCommand comm = null;
            try
            {

                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                comm = db.GetStoredProcCommand("uspGetCrewAssistCMInformation");
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, loadType);
                db.AddInParameter(comm, "@pSeafarerID", DbType.Int64, SeafarerID);
                db.AddInParameter(comm, "@pUserID", DbType.String, UserID);
                ds = db.ExecuteDataSet(comm);

                return ProcessSeafarerInfo(ds, UserID);

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


        private List<CrewMemberInformation> ProcessSeafarerInfo(DataSet ds, string UserID)
        {

            List<CrewMemberInformation> SeafarerInfo = new List<CrewMemberInformation>();
            try
            {
                if (ds != null)
                {

                    if (ds.Tables.Count > 0)
                    {

                        return (from a in ds.Tables[0].AsEnumerable()
                                select new CrewMemberInformation
                                {

                                    SeafarerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                                    FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                                    LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                    Name = GlobalCode.Field2String(a["FullName"]),
                                    Gender = GlobalCode.Field2String(a["colGenderDiscription"]),
                                    NationalityID = GlobalCode.Field2Int(a["colNatioalityIdInt"]),
                                    NationalityCode = GlobalCode.Field2String(a["colNationalityCodeVarchar"]),
                                    Nationality = GlobalCode.Field2String(a["colNationalityDescriptionVarchar"]),

                                    CrewSchedule = (from n in ds.Tables[1].AsEnumerable()
                                                    select new CrewSchedule
                                                    {

                                                        TravelRequestID = n.Field<long?>("TravelRequestID"),
                                                        IDBigint = n.Field<long?>("colIDBigInt"),
                                                        SeqNo = n.Field<int?>("SeqNo"),
                                                        RecordLocator = n.Field<string>("colRecordLocatorVarchar"),
                                                        VesselID = n.Field<int?>("colVesselIdInt"),
                                                        Vessel = n.Field<string>("colVesselLongCodeVarchar"),
                                                        VesselCode = n.Field<string>("colVesselCodeVarchar"),

                                                        Status = n.Field<string>("colStatusVarchar"),

                                                        SignOnOffDate = n.Field<DateTime?>("RequestDate"),
                                                        PortID = n.Field<int?>("colPortIdInt"),
                                                        Port = n.Field<string>("colPortNameVarchar"),
                                                        PortCode = n.Field<string>("colPortCodeVarchar"),

                                                        ReasonCode = n.Field<string>("colReasonCodeVarchar"),
                                                        CostcenterID = n.Field<int?>("colCostCenterIDInt"),
                                                        Costcenter = n.Field<string>("colCostCenterNameVarchar"),

                                                        RankID = n.Field<int?>("colRankIDInt"),
                                                        Rank = n.Field<string>("colRankNameVarchar"),
                                                        RankCode = n.Field<string>("colRankCodeVarchar"),

                                                        BrandID = n.Field<int?>("colBrandIdInt"),
                                                        Brand = n.Field<string>("colBrandNameVarchar"),


                                                        LOEStatus = GlobalCode.Field2String(n["LOEStatus"]),
                                                        LOEDate = n.Field<DateTime?>("LOEDate"),
                                                        LOEImmigrationOfficer = GlobalCode.Field2String(n["LOEImmigrationOfficer"]),
                                                        LOEImmigrationPlace = GlobalCode.Field2String(n["LOEImmigrationPlace"]),
                                                        LOEReason = GlobalCode.Field2String(n["Reason"]),

                                                    }).ToList(),

                                    Remark = (from n in ds.Tables[2].AsEnumerable()
                                              select new CrewAssisRemark
                                              {
                                                  TravelRequestID = n.Field<long?>("TravelRequestID"),
                                                  RemarkID = n.Field<long?>("RemarkID"),
                                                  Remark = n.Field<string>("Remark"),
                                                  RemarkBy = n.Field<string>("RemarkBy"),
                                                  RemarkDate = n.Field<DateTime?>("RemarkDate"),
                                                  Visible = n.Field<string>("Visible"),//n.Field<string>("CreatedBy") == UserID ? "True" : "False", 
                                                  ReqResourceID = GlobalCode.Field2TinyInt(n["colRemarkSourceID"]),
                                                  Resource = n.Field<string>("colRemarkSource"),
                                                  IDBigInt = n.Field<long?>("IDBigInt"),
                                                  RecordLocator = n.Field<string>("RecordLocator")
                                              }).ToList()

                                }).ToList();
                    }
                }
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

            return SeafarerInfo;


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
        public List<CrewAssistCMTransaction> GetCrewMemberTransaction(short LoadType, long SeafarerID,
            long TravelRequestID, long IDBigInt, long SeqNo, DateTime Startdate, string DepCode,
             string ArrCode, bool IsAir, string UserID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds;

            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetCrewMemberTransaction");
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

                return GetCrewMemberTransaction(ds);

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



        List<CrewAssistCMTransaction> GetCrewMemberTransaction(DataSet ds)
        {
            List<CrewAssistCMTransaction> res = new List<CrewAssistCMTransaction>();
            List<CrewAssistGenericVendor> CrewAssistGenericVendor = new List<CrewAssistGenericVendor>();

            CrewAssistGenericVendor.Add(new CrewAssistGenericVendor
            {

                listHotel = (from a in ds.Tables[10].AsEnumerable()
                             select new CrewAssistHotelList
                             {
                                 HotelID = GlobalCode.Field2Int(a["BranchID"]),
                                 HotelName = GlobalCode.Field2String(a["BranchName"]),
                             }).ToList(),

                listVehicle = (from a in ds.Tables[11].AsEnumerable()
                               select new VehicleVendor
                               {
                                   VehicleID = GlobalCode.Field2Int(a["VendorID"]),
                                   Vehicle = GlobalCode.Field2String(a["VehicleName"]),
                               }).ToList(),

                listPortAgent = (from a in ds.Tables[12].AsEnumerable()
                                 select new PortAgentDTO
                                 {
                                     PortAgentID = GlobalCode.Field2String(a["PortAgentID"]),
                                     PortAgentName = GlobalCode.Field2String(a["PortAgentName"]),
                                 }).ToList(),

                listMeetGreet = (from a in ds.Tables[13].AsEnumerable()
                                 select new MeetAndGreetList
                                 {
                                     MeetandGreetVendorId = GlobalCode.Field2Int(a["MeetAndGreetID"]),
                                     MeetAndGreetVendorName = GlobalCode.Field2String(a["MeetAndGreetName"]),
                                 }).ToList(),

                listSafeguard = (from a in ds.Tables[14].AsEnumerable()
                                 select new VendorSafeguardList
                                 {
                                     SafeguardID = GlobalCode.Field2Int(a["SafeguardID"]),
                                     VendorName = GlobalCode.Field2String(a["SafeguardName"]),
                                 }).ToList()
            });

            res.Add(new CrewAssistCMTransaction
            {




                CrewAssistHotelBooking = (from r in ds.Tables[0].AsEnumerable()
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

                                              MealVoucherMoney = (GlobalCode.Field2Double(r["colVoucherAmountMoney"]) == 0 ? GlobalCode.Field2Double(r["colMealVoucherMoney"]) : GlobalCode.Field2Double(r["colVoucherAmountMoney"])),
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
                                              IsApproved = GlobalCode.Field2Bool(r["colIsPortAgent"]) == true ? GlobalCode.Field2Int(r["colStatusIDTinyint"]) >= 3 ? false : true : GlobalCode.Field2Long(r["colTransHotelIDBigInt"]) > 0 ? false : true,
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
                                              CutOffTime = GlobalCode.Field2DateTime(r["colCutOffTime"]),

                                              ForeColor = GlobalCode.Field2String(r["coldForeColorVarchar"]),
                                              ColorCode = GlobalCode.Field2String(r["colColorCodevarchar"]),

                                              Address = GlobalCode.Field2String(r["colAddressVarchar"]),
                                              ContactNo = GlobalCode.Field2String(r["colContactNoVarchar"]),



                                              HotelRemark = (from a in ds.Tables[7].AsEnumerable()
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


                CrewAssistTranspo = (from Q in ds.Tables[1].AsEnumerable()
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
                                         Address = GlobalCode.Field2String(Q["colAddressVarchar"]),
                                         Telephone = GlobalCode.Field2String(Q["colContactNoVarchar"]),
                                         TranspoCost = GlobalCode.Field2Double(Q["colConfirmRateMoney"]).ToString("N2"),
                                         ConfirmBy = GlobalCode.Field2String(Q["colConfirmBy"]),
                                         VehicleTransID = GlobalCode.Field2Long(Q["colTransVehicleIDBigint"]),
                                         IsPortAgent = GlobalCode.Field2Bool(Q["colIsPortAgent"]),
                                         ColorCode = GlobalCode.Field2String(Q["colColorCodevarchar"]),
                                         ForeColor = GlobalCode.Field2String(Q["coldForeColorVarchar"]),
                                         StatusID = GlobalCode.Field2Int(Q["colStatusID"]),
                                         VehicleContract = (from x in ds.Tables[2].AsEnumerable()
                                                            select new CrewAssisVehicleCost
                                                            {
                                                                CurrencyCode = x["CurrencyCode"].ToString(),
                                                                Route = x["Route"].ToString(),
                                                                VehicleTypeName = x["VehicleTypeName"].ToString(),
                                                                VehicleTypeID = GlobalCode.Field2Int(x["VehicleTypeID"]),
                                                                UnitOfMeasure = x["UnitOfMeasure"].ToString(),
                                                                TranspoCost = GlobalCode.Field2Double(x["Cost"]).ToString("N2"),
                                                                Capacity = x["Capacity"].ToString(),
                                                                VacantSeat = x["VacantSeat"].ToString(),
                                                            }).ToList(),
                                         VehicleRemark = (from a in ds.Tables[7].AsEnumerable()
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

                CrewAssistMeetAndGreet = (from Q in ds.Tables[3].AsEnumerable()
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
                CrewAssistPortAgentRequest = (from Q in ds.Tables[4].AsEnumerable()
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
                                              }).ToList(),
                CrewAssistSafeguardRequest = (from p in ds.Tables[5].AsEnumerable()
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
                CopyEmail = (from s in ds.Tables[6].AsEnumerable()
                             select new CopyEmail
                             {
                                 EmailName = GlobalCode.Field2String(s["EmailType"]),
                                 EmailType = GlobalCode.Field2Int(s["EmailID"]),
                                 Email = GlobalCode.Field2String(s["Email"]),
                             }).ToList(),
                HotelRequestCompanion = (from k in ds.Tables[8].AsEnumerable()
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


                CrewAssistAirTransaction = (from n in ds.Tables[9].AsEnumerable()
                                            select new CrewAssistCMAirTransaction
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
                                                FligthNo = GlobalCode.Field2String(n["colFlightNoVarchar"]),
                                                StatusID = GlobalCode.Field2Int(n["colStatusIDInt"]),
                                                Status = GlobalCode.Field2String(n["colStatus"]),
                                                StatusMessage = GlobalCode.Field2String(n["StatusMessage"]),
                                            }).ToList(),




                CrewAssistGenericVendor = CrewAssistGenericVendor,




            });

            return res;

        }

        /// <summary>
        /// Created By:    Muhallidin G Wali
        /// Date Modified: 10/24/2014
        /// (description)  Insert Air Status.
        /// </summary>
        public List<CrewAssistCMTransaction> InsertAirTransactionStatus(long TravelRequestID, long IdBigint, int SeqNo, int StatusID, string OldStatus, string UserID)
        {
            DataSet ds = null;
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCrewMemberInsertAirStatus");

                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int64, IdBigint);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, SeqNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatusID", DbType.Int32, StatusID);
                SFDatebase.AddInParameter(SFDbCommand, "@pOldStatus", DbType.String, OldStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                return GetCrewMemberTransaction(ds);
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



        public List<CrewAssistHotelBooking> InsertHotelTransRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
            double ContractedRate, string EmailTO, string HotelCity, short ReqSource,
            string ContactName, string ContactNo, string Recipient,
            string CCEmail, string BlindCopy, double RateConfirm, bool IsMedical, long TransHotelID, List<HotelRequestCompanion> HRC)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataSet ds;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {

                List<CrewAssistHotelBooking> CrewAssistHotelBooking = new List<CrewAssistHotelBooking>();

                GlobalCode gc = new GlobalCode();
                DataTable dt = new DataTable();

                dt = gc.getDataTable(HRC);

                dt.Columns.Remove("GENDER");
                dt.Columns.Remove("RECORDLOCATOR");
                dt.Columns.Remove("IsPortAgent");

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertCrewAssistHotelTransRequest");
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
                SFDatebase.AddInParameter(SFDbCommand, "@pTrID", DbType.Int64, TrID);
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


                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                CrewAssistHotelBooking = ProcessHotelBooking(ds);


                trans.Commit();

                return CrewAssistHotelBooking;

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
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public List<CrewAssistHotelBooking> SavePortAgentHotelRequest(List<HotelTransactionPortAgent> HotelTranPortAgent, string UserID, List<HotelRequestCompanion> HRC)
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

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSaveHotelTransPortAgentRequest");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);

                SqlParameter param1 = new SqlParameter("@pHotelTable", dt1);
                SqlParameter param = new SqlParameter("@pCompanionTable", dt);

                param1.Direction = ParameterDirection.Input;
                param1.SqlDbType = SqlDbType.Structured;

                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;

                SFDbCommand.Parameters.Add(param1);
                SFDbCommand.Parameters.Add(param);

                return ProcessHotelBooking(SFDatebase.ExecuteDataSet(SFDbCommand));
                 
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

        List<CrewAssistHotelBooking> ProcessHotelBooking(DataSet ds)
        {
            List<CrewAssistHotelBooking> CrewAssistHotelBooking = new List<CrewAssistHotelBooking>();
            CrewAssistHotelBooking = (from r in ds.Tables[0].AsEnumerable()
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

                                          MealVoucherMoney = (GlobalCode.Field2Double(r["colVoucherAmountMoney"]) == 0 ? GlobalCode.Field2Double(r["colMealVoucherMoney"]) : GlobalCode.Field2Double(r["colVoucherAmountMoney"])),
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
                                          IsApproved = GlobalCode.Field2Bool(r["colIsPortAgent"]) == true ? GlobalCode.Field2Int(r["colStatusIDTinyint"]) >= 3 ? false : true : GlobalCode.Field2Long(r["colTransHotelIDBigInt"]) > 0 ? false : true,
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
                                          CutOffTime = GlobalCode.Field2DateTime(r["colCutOffTime"]),

                                          ForeColor = GlobalCode.Field2String(r["coldForeColorVarchar"]),
                                          ColorCode = GlobalCode.Field2String(r["colColorCodevarchar"]),

                                          Address = GlobalCode.Field2String(r["colAddressVarchar"]),
                                          ContactNo = GlobalCode.Field2String(r["colContactNoVarchar"]),

                                          HotelRemark = (from a in ds.Tables[7].AsEnumerable()
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
                                      }).ToList();
            return CrewAssistHotelBooking;

        }


        /// <summary>
        /// Date Create:    07/MAR/2014
        /// Create By:      Muhallidin G Wali
        /// (description)  	Get Port Agent Detail
        /// </summary>
        public List<CrewAssistHotelInformation> GetCrewAssistHotelVendor(short LoadType, long BranchID, long TravelRequestID, long IDBigInt, string PortCode)
        {
            List<CrewAssistHotelInformation> list = new List<CrewAssistHotelInformation>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DataSet ds = new DataSet();
            DbCommand dbComm = null;
            try
            {
                dbComm = db.GetStoredProcCommand("upsGetCrewAssistHotelVendor");
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbComm, "@pBranchID", DbType.Int64, BranchID);
                db.AddInParameter(dbComm, "@pTravelRequestID", DbType.Int64, TravelRequestID);
                db.AddInParameter(dbComm, "@pIDBigInt", DbType.Int64, IDBigInt);
                db.AddInParameter(dbComm, "@pPortCode", DbType.String, PortCode);
                ds = db.ExecuteDataSet(dbComm);
                var SF = (from a in ds.Tables[0].AsEnumerable()
                          select new CrewAssistHotelInformation
                          {
                              VendorBranchName = GlobalCode.Field2String(a["BranchName"]),
                              BranchID = GlobalCode.Field2Int(a["HotelID"]),
                              CityID = GlobalCode.Field2Int(a["CityID"]),
                              CountryID = GlobalCode.Field2Int(a["CountryID"]),
                              ContactNo = GlobalCode.Field2String(a["ContactNo"]),
                              ContactPerson = GlobalCode.Field2String(a["ContactPerson"]),
                              Address = GlobalCode.Field2String(a["Address"]),

                              EmailTo = GlobalCode.Field2String(a["EmailTo"]),
                              EmailCC = GlobalCode.Field2String(a["EmailCC"]),

                              CityName = GlobalCode.Field2String(a["CityName"]),
                              CityCode = GlobalCode.Field2String(a["CityCode"]),
                              FaxNo = GlobalCode.Field2String(a["FaxNo"]),
                              Website = GlobalCode.Field2String(a["WebSite"]),
                              MealVoucher = GlobalCode.Field2Double(a["MealVoucher"]).ToString("N2"),
                              ContractedRate = GlobalCode.Field2Double(a["ContractedRate"]).ToString("N2"),
                              ConfirmRate = GlobalCode.Field2Double(a["ConfirmRate"]).ToString("N2"),
                              ContractRoomRateTaxPercentage = GlobalCode.Field2Double(a["ContractRoomRateTaxPercentage"]).ToString("N2"),
                              IsBreakfast = GlobalCode.Field2Bool(a["BreakfastBit"]),
                              IsDinner = GlobalCode.Field2Bool(a["DinnertBit"]),
                              IsLunch = GlobalCode.Field2Bool(a["LunchBit"]),
                              IsWithShuttle = GlobalCode.Field2Bool(a["WithShuttleBit"]),
                              RoomTypeID = GlobalCode.Field2Int(a["RoomTypeID"]),
                              CurrencyID = GlobalCode.Field2Int(a["CurrencyID"]),
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


        public List<CrewAssistHotelBooking> SeafarerSaveRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
            double ContractedRate, string EmailTO, string HotelCity, short ReqSource,
            string ContactName, string ContactNo, string Recipient,
            string CCEmail, string BlindCopy, double RateConfirm, bool IsMedical, long TransHotelID, List<HotelRequestCompanion> HRC)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DbConnection connection = SFDatebase.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {

                List<CrewAssistHotelBooking> lst = new List<CrewAssistHotelBooking>();
                GlobalCode gc = new GlobalCode();
                DataTable dt = new DataTable();

                dt = gc.getDataTable(HRC);

                dt.Columns.Remove("GENDER");
                dt.Columns.Remove("RECORDLOCATOR");
                dt.Columns.Remove("IsPortAgent");

                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertCrewMemberHotelRequest");
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

                var obj = (from n in SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0].AsEnumerable()
                           select n).ToList();
                return lst;
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

    }
}

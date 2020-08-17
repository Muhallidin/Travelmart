using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using TRAVELMART.Common;
using System.Data;
using System.Data.SqlClient;

namespace TRAVELMART.DAL
{
   public class MedicalDAL
    {



       DataSet ds = new DataSet();
       DataTable dt = new DataTable();

       public IMSClassList GetMedicalClassList(short LoadType, int BranchID)
       {

           Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
           DbCommand SFDbCommand = null;
           try
           {
               IMSClassList IMSClass = new IMSClassList();

               SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetMedicalVendor");
               SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
               SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, BranchID);

               ds = SFDatebase.ExecuteDataSet(SFDbCommand);
               IMSClass.Vendor = (from a in ds.Tables[0].AsEnumerable()
                                  select new Vendor
                                  {
                                      VendorNumber = a["colVendorNumber"].ToString(),
                                      VendorNumID = a["colVendorNumIDInt"].ToString(),
                                      BranchID = GlobalCode.Field2Int(a["BranchID"].ToString()),
                                      BranchName = a["Branch"].ToString(),
                                      UserID = a["colCreatedByVarchar"].ToString(),
                                      createdDate = a["colCreatedDateTime"].ToString(),
                                  }).ToList();

            

               IMSClass.Port = (from a in ds.Tables[1].AsEnumerable()
                                select new Port
                                {
                                    PortId = GlobalCode.Field2Int(a["PortId"]),
                                    PortName = a["PortName"].ToString(),
                                    IMSPortNumber = GlobalCode.Field2Int(a["IMSPortNumber"]),
                                }).ToList();

               return IMSClass;

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


       public List<CrewMemberInformation> GetHotelTransactionMedical(short loadType, long SeafarerID, string UserID)
       {

           DataSet ds = null;
           DataTable dt = null;
           DbCommand comm = null;
           try
           {

               Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
               comm = db.GetStoredProcCommand("uspGetCrewAssistCMInformation");
               db.AddInParameter(comm, "@pLoadType", DbType.Int16, loadType);
               db.AddInParameter(comm, "@pSeafarerID", DbType.Int64, SeafarerID);
               db.AddInParameter(comm, "@pUserID", DbType.String, UserID);
               ds = db.ExecuteDataSet(comm);
            return   ProcessSeafarerInfo(ds, UserID);

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

                                             }).ToList(),


                                  HotelTransactionMedical = (from n in ds.Tables[3].AsEnumerable()
                                                   select new HotelTransactionMedical
                                                   {

                                                        TransHotelID = GlobalCode.Field2Long(n["colTransHotelIDBigInt"]),
                                                        SeafarerID = GlobalCode.Field2Long(n["colSeafarerIDBigInt"]),
                                                        FullName = GlobalCode.Field2String(n["colFullNameVarchar"]),
                                                        TravelReqID = GlobalCode.Field2Long(n["colTravelReqIDInt"]),
                                                        IdBigint = GlobalCode.Field2Long(n["colIdBigint"]),
                                                        RecordLocator = GlobalCode.Field2String(n["colRecordLocatorVarchar"]),
                                                        SeqNo = GlobalCode.Field2Int(n["colSeqNoInt"]),
                                                        PortAgentVendorID = GlobalCode.Field2Long(n["colPortAgentVendorIDInt"]),
                                                        RoomTypeID = GlobalCode.Field2Int(n["colRoomTypeIDInt"]),
                                                        RoomType = GlobalCode.Field2String(n["RoomType"]),
                                                        ReserveUnderName = GlobalCode.Field2String(n["colReserveUnderNameVarchar"]),
                                                        TimeSpanStartDate = GlobalCode.Field2DateTime(n["colTimeSpanStartDate"]),
                                                        TimeSpanStartTime = GlobalCode.Field2DateTime(n["colTimeSpanStartTime"]),
                                                        TimeSpanEndDate = GlobalCode.Field2DateTime(n["colTimeSpanEndDate"]),
                                                        TimeSpanEndTime = GlobalCode.Field2DateTime(n["colTimeSpanEndTime"]),
                                                        TimeSpanDuration = GlobalCode.Field2Int(n["colTimeSpanDurationInt"]),
                                                        ConfirmationNo = GlobalCode.Field2String(n["colConfirmationNoVarchar"]),
                                                        HotelStatus = GlobalCode.Field2String(n["colHotelStatusVarchar"]),
                                                        DateCreatedDatetime = GlobalCode.Field2DateTime(n["colDateCreatedDatetime"]),
                                                        CreatedBy = GlobalCode.Field2String(n["colCreatedByVarchar"]),
                                                        IsActive = GlobalCode.Field2Bool(n["colIsActiveBit"]),
                                                        VoucherAmount = GlobalCode.Field2Long(n["colVoucherAmountMoney"]),
                                                        ContractID = GlobalCode.Field2Long(n["colContractIDInt"]),
                                                        ApprovedBy = GlobalCode.Field2String(n["colApprovedByVarchar"]),
                                                        ApprovedDate = GlobalCode.Field2DateTime(n["colApprovedDate"]),
                                                        ContractFrom = GlobalCode.Field2String(n["colContractFromVarchar"]),
                                                        RemarksForAudit = GlobalCode.Field2String(n["colRemarksForAuditVarchar"]),
                                                        HotelCity = GlobalCode.Field2String(n["colHotelCityVarchar"]),
                                                        RoomCount = GlobalCode.Field2Float(n["colRoomCountFloat"]),
                                                        HotelName = GlobalCode.Field2String(n["colHotelNameVarchar"]),
                                                        ConfirmRateMoney = GlobalCode.Field2Decimal(n["colConfirmRateMoney"]),
                                                        ContractedRateMoney = GlobalCode.Field2Decimal(n["colContractedRateMoney"]),
                                                        EmailTo = GlobalCode.Field2String(n["colEmailToVarchar"]),
                                                        Comment = GlobalCode.Field2String(n["colCommentVarchar"]),
                                                        CurrencyID = GlobalCode.Field2Int(n["colCurrencyInt"]),
                                                        ConfirmBy = GlobalCode.Field2String(n["colConfirmByVarchar"]),
                                                        StatusID = GlobalCode.Field2TinyInt(n["colStatusIDTinyint"]),

                                                        ColorCode = GlobalCode.Field2String(n["ColorCode"]),
                                                        ForeColor = GlobalCode.Field2String(n["ForeColor"]),
                                                        IsMedical = GlobalCode.Field2Bool(n["IsMedical"]),
                                                        CancellationTermsInt = GlobalCode.Field2String(n["CancellationTermsInt"]),
                                                        HotelTimeZoneID = GlobalCode.Field2String(n["HotelTimeZoneID"]),
                                                        CutOffTime = GlobalCode.Field2String(n["CutOffTime"]),
                                                        IsConfirmed = GlobalCode.Field2String(n["IsConfirmed"]),
                                                        Address = GlobalCode.Field2String(n["Address"]),
                                                        ContactNo = GlobalCode.Field2String(n["ContactNo"]),
                                                        Breakfast = GlobalCode.Field2Bool(n["colBreakfastBit"]),
                                                        IsBilledToCrew = GlobalCode.Field2Bool(n["colIsBilledToCrewBit"]),
                                                        Lunch = GlobalCode.Field2Bool(n["colLunchBit"]),
                                                        Dinner = GlobalCode.Field2Bool(n["colDinnerBit"]),
                                                        LunchOrDinner = GlobalCode.Field2Bool(n["colLunchOrDinnerBit"]),
                                                        WithShuttle = GlobalCode.Field2Bool(n["colWithShuttleBit"]),
                                                        VendorName = GlobalCode.Field2String(n["VendorName"]),
                                                        IsPortAgent = GlobalCode.Field2Bool(n["IsPortAgent"]),
                                                   }).ToList(),
                                                   
                                                   VehicleTransactionMedical = (from n in ds.Tables[4].AsEnumerable()
                                                                                select new VehicleTransactionMedical
                                                                                {
                                                                                  TransVehicleID = GlobalCode.Field2Long(n["colTransVehicleIDBigint"]),
                                                                                  SeafarerID = GlobalCode.Field2Long(n["colSeafarerIDBigint"]),
                                                                                  IdBigint = GlobalCode.Field2Long(n["colIdBigint"]),
                                                                                  TravelReqID = GlobalCode.Field2Long(n["colTravelReqIDInt"]),
                                                                                  RecordLocator = GlobalCode.Field2String(n["colRecordLocatorVarchar"]),
                                                                                  TranspoVendorID = GlobalCode.Field2Long(n["colTranspoVendorIDInt"]),
                                                                                  VehiclePlateNo = GlobalCode.Field2String(n["colVehiclePlateNoVarchar"]),
                                                                                  PickUpDate = GlobalCode.Field2DateTime(n["colPickUpDate"]),
                                                                                  PickUpTime = GlobalCode.Field2DateTime(n["colPickUpTime"]),
                                                                                  DropOffDate = GlobalCode.Field2DateTime(n["colDropOffDate"]),
                                                                                  DropOffTime = GlobalCode.Field2DateTime(n["colDropOffTime"]),
                                                                                  ConfirmationNo = GlobalCode.Field2String(n["colConfirmationNoVarchar"]),
                                                                                  VehicleStatus = GlobalCode.Field2String(n["colVehicleStatusVarchar"]),
                                                                                  VehicleTypeId = GlobalCode.Field2Int(n["colVehicleTypeIdInt"]),
                                                                                  SFStatus = GlobalCode.Field2String(n["colSFStatus"]),
                                                                                  RouteIDFrom = GlobalCode.Field2Int(n["colRouteIDFromInt"]),
                                                                                  RouteIDTo = GlobalCode.Field2Int(n["colRouteIDToInt"]),
                                                                                  From = GlobalCode.Field2String(n["colFromVarchar"]),
                                                                                  To = GlobalCode.Field2String(n["colToVarchar"]),
                                                                                  DateCreated = GlobalCode.Field2DateTime(n["colDateCreatedDatetime"]),
                                                                                  DateModified = GlobalCode.Field2DateTime(n["colDateModifiedDatetime"]),
                                                                                  CreatedBy = GlobalCode.Field2String(n["colCreatedByVarchar"]),
                                                                                  Modifiedby = GlobalCode.Field2String(n["colModifiedbyVarchar"]),

                                                                                  IsActive = GlobalCode.Field2Bool(n["colIsActiveBit"]), 
                                                                                  RemarksForAudit = GlobalCode.Field2String(n["colRemarksForAuditVarchar"]), 
                                                                                  HotelID = GlobalCode.Field2Int(n["colHotelIDInt"]), 
                                                                                  IsVisible = GlobalCode.Field2Bool(n["colIsVisibleBit"]),
                                                                                  ContractId = GlobalCode.Field2Int(n["colContractIdInt"]), 
                                                                                  IsSeaport = GlobalCode.Field2Bool(n["colIsSeaportBit"]),
                                                                                  SeqNo = GlobalCode.Field2Int(n["colSeqNoInt"]), 
                                                                                  Driver = GlobalCode.Field2String(n["colDriverVarchar"]), 
                                                                                  VehicleDispatchTime = GlobalCode.Field2String(n["colVehicleDispatchTime"]),
                                                                                  RouteFrom = GlobalCode.Field2String(n["colRouteFromVarchar"]), 
                                                                                  RouteTo = GlobalCode.Field2String(n["colRouteToVarchar"]), 
                                                                                  VehicleUnset = GlobalCode.Field2Bool(n["colVehicleUnset"]), 
                                                                                  VehicleUnsetBy = GlobalCode.Field2String(n["colVehicleUnsetBy"]), 
                                                                                  VehicleUnsetDate = GlobalCode.Field2DateTime(n["colVehicleUnsetDate"]), 
                                                                                  ConfirmBy = GlobalCode.Field2String(n["colConfirmByVarchar"]), 
                                                                                  Comments = GlobalCode.Field2String(n["colCommentsVarchar"]),
                                                                                  ContractedRateMoney = GlobalCode.Field2Double(n["colContractedRateMoney"]),
                                                                                  ConfirmRateMoney = GlobalCode.Field2Double(n["colConfirmRateMoney"]), 
                                                                                  CurrencyInt = GlobalCode.Field2Int(n["colCurrencyInt"]), 
                                                                                  StatusID = GlobalCode.Field2TinyInt(n["colStatusIDTinyint"]), 
                                                                                  ApprovedBy = GlobalCode.Field2String(n["colApprovedByVarchar"]), 
                                                                                  ApprovedDate = GlobalCode.Field2DateTime(n["colApprovedDate"]), 
                                                                                  EmailTo = GlobalCode.Field2String(n["colEmailTovarchar"]),
                                                                                  RequestSourceID = GlobalCode.Field2TinyInt(n["colRequestSourceIDInt"]), 
                                                                                  TransportationDetails = GlobalCode.Field2String(n["colTransportationDetails"]), 
                                                                                  IsPortAgent = GlobalCode.Field2Bool(n["colIsPortAgentBit"]),
                                                                                  VehicleVendor = GlobalCode.Field2String(n["VendorName"]),

                                                                                  ColorCode = GlobalCode.Field2String(n["ColorCode"]),
                                                                                  ForeColor = GlobalCode.Field2String(n["ForeColor"]),
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



       public List<HotelTransactionMedical> InsertHotelTransactionMedical(List<HotelTransactionMedical> Medical)
       {
           Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
           DbCommand SFDbCommand = null;
           try
           {

               List<HotelTransactionMedical> HotelTransactionMedical = new List<HotelTransactionMedical>();
               GlobalCode gc = new GlobalCode();
               DataTable dt = new DataTable();

               DataSet ds = new DataSet();

               dt = gc.getDataTable(Medical);

               dt.Columns.Remove("ColorCode");
               dt.Columns.Remove("ForeColor");
               dt.Columns.Remove("IsMedical");
               dt.Columns.Remove("CancellationTermsInt");
               dt.Columns.Remove("HotelTimeZoneID");
               dt.Columns.Remove("CutOffTime");
               dt.Columns.Remove("IsConfirmed");
               dt.Columns.Remove("Address");
               dt.Columns.Remove("ContactNo");
               dt.Columns.Remove("VendorName");
               dt.Columns.Remove("RoomType");



               string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
               SFDbCommand = SFDatebase.GetStoredProcCommand("uspHotelTransactionMedicalIns");
                //SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.Int16, UserID	);
               SqlParameter param = new SqlParameter("@pHotelTransactionMedical", dt);

               param.Direction = ParameterDirection.Input;
               param.SqlDbType = SqlDbType.Structured;

               SFDbCommand.Parameters.Add(param);
               ds = SFDatebase.ExecuteDataSet(SFDbCommand);

               HotelTransactionMedical = (from n in ds.Tables[0].AsEnumerable()
                                          select new HotelTransactionMedical
                                          {

                                              TransHotelID = GlobalCode.Field2Long(n["colTransHotelIDBigInt"]),
                                              SeafarerID = GlobalCode.Field2Long(n["colSeafarerIDBigInt"]),
                                              FullName = GlobalCode.Field2String(n["colFullNameVarchar"]),
                                              TravelReqID = GlobalCode.Field2Long(n["colTravelReqIDInt"]),
                                              IdBigint = GlobalCode.Field2Long(n["colIdBigint"]),
                                              RecordLocator = GlobalCode.Field2String(n["colRecordLocatorVarchar"]),
                                              SeqNo = GlobalCode.Field2Int(n["colSeqNoInt"]),
                                              PortAgentVendorID = GlobalCode.Field2Long(n["colPortAgentVendorIDInt"]),
                                              RoomTypeID = GlobalCode.Field2Int(n["colRoomTypeIDInt"]),
                                              RoomType = GlobalCode.Field2String(n["RoomType"]),
                                              ReserveUnderName = GlobalCode.Field2String(n["colReserveUnderNameVarchar"]),
                                              TimeSpanStartDate = GlobalCode.Field2DateTime(n["colTimeSpanStartDate"]),
                                              TimeSpanStartTime = GlobalCode.Field2DateTime(n["colTimeSpanStartTime"]),
                                              TimeSpanEndDate = GlobalCode.Field2DateTime(n["colTimeSpanEndDate"]),
                                              TimeSpanEndTime = GlobalCode.Field2DateTime(n["colTimeSpanEndTime"]),
                                              TimeSpanDuration = GlobalCode.Field2Int(n["colTimeSpanDurationInt"]),
                                              ConfirmationNo = GlobalCode.Field2String(n["colConfirmationNoVarchar"]),
                                              HotelStatus = GlobalCode.Field2String(n["colHotelStatusVarchar"]),
                                              DateCreatedDatetime = GlobalCode.Field2DateTime(n["colDateCreatedDatetime"]),
                                              CreatedBy = GlobalCode.Field2String(n["colCreatedByVarchar"]),
                                              IsActive = GlobalCode.Field2Bool(n["colIsActiveBit"]),
                                              VoucherAmount = GlobalCode.Field2Long(n["colVoucherAmountMoney"]),
                                              ContractID = GlobalCode.Field2Long(n["colContractIDInt"]),
                                              ApprovedBy = GlobalCode.Field2String(n["colApprovedByVarchar"]),
                                              ApprovedDate = GlobalCode.Field2DateTime(n["colApprovedDate"]),
                                              ContractFrom = GlobalCode.Field2String(n["colContractFromVarchar"]),
                                              RemarksForAudit = GlobalCode.Field2String(n["colRemarksForAuditVarchar"]),
                                              HotelCity = GlobalCode.Field2String(n["colHotelCityVarchar"]),
                                              RoomCount = GlobalCode.Field2Float(n["colRoomCountFloat"]),
                                              HotelName = GlobalCode.Field2String(n["colHotelNameVarchar"]),
                                              ConfirmRateMoney = GlobalCode.Field2Decimal(n["colConfirmRateMoney"]),
                                              ContractedRateMoney = GlobalCode.Field2Decimal(n["colContractedRateMoney"]),
                                              EmailTo = GlobalCode.Field2String(n["colEmailToVarchar"]),
                                              Comment = GlobalCode.Field2String(n["colCommentVarchar"]),
                                              CurrencyID = GlobalCode.Field2Int(n["colCurrencyInt"]),
                                              ConfirmBy = GlobalCode.Field2String(n["colConfirmByVarchar"]),
                                              StatusID = GlobalCode.Field2TinyInt(n["colStatusIDTinyint"]),

                                              ColorCode = GlobalCode.Field2String(n["ColorCode"]),
                                              ForeColor = GlobalCode.Field2String(n["ForeColor"]),
                                              IsMedical = GlobalCode.Field2Bool(n["IsMedical"]),
                                              CancellationTermsInt = GlobalCode.Field2String(n["CancellationTermsInt"]),
                                              HotelTimeZoneID = GlobalCode.Field2String(n["HotelTimeZoneID"]),
                                              CutOffTime = GlobalCode.Field2String(n["CutOffTime"]),
                                              IsConfirmed = GlobalCode.Field2String(n["IsConfirmed"]),
                                              Address = GlobalCode.Field2String(n["Address"]),
                                              ContactNo = GlobalCode.Field2String(n["ContactNo"]),
                                              Breakfast = GlobalCode.Field2Bool(n["colBreakfastBit"]),
                                              IsBilledToCrew = GlobalCode.Field2Bool(n["colIsBilledToCrewBit"]),
                                              Lunch = GlobalCode.Field2Bool(n["colLunchBit"]),
                                              Dinner = GlobalCode.Field2Bool(n["colDinnerBit"]),
                                              LunchOrDinner = GlobalCode.Field2Bool(n["colLunchOrDinnerBit"]),
                                              WithShuttle = GlobalCode.Field2Bool(n["colWithShuttleBit"]),
                                              VendorName = GlobalCode.Field2String(n["VendorName"]),
                                              IsPortAgent = GlobalCode.Field2Bool(n["IsPortAgent"]),
                                          }).ToList();

               return HotelTransactionMedical;

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






           

       public List<VehicleTransactionMedical> InsertVehicleTransactionMedical(List<VehicleTransactionMedical> Medical)
       {
           Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
           DbCommand SFDbCommand = null;
           try
           {
               List<VehicleTransactionMedical> VehicleTransMedical = new List<VehicleTransactionMedical>();
               GlobalCode gc = new GlobalCode();
               DataTable dt = new DataTable();

               DataSet ds = new DataSet();

               dt = gc.getDataTable(Medical);

               dt.Columns.Remove("ColorCode");
               dt.Columns.Remove("ForeColor"); 
                  
                

               string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
               SFDbCommand = SFDatebase.GetStoredProcCommand("uspVehicleTransactionMedicalIns");
               SqlParameter param = new SqlParameter("@pVehicleTransactionMedical", dt);

               param.Direction = ParameterDirection.Input;
               param.SqlDbType = SqlDbType.Structured;

               SFDbCommand.Parameters.Add(param);
               ds = SFDatebase.ExecuteDataSet(SFDbCommand);
               
               VehicleTransMedical = (from n in ds.Tables[0].AsEnumerable()
                                   select new VehicleTransactionMedical
                                   {
                                        TransVehicleID = GlobalCode.Field2Long(n["colTransVehicleIDBigint"]),
                                        SeafarerID = GlobalCode.Field2Long(n["colSeafarerIDBigint"]),
                                        IdBigint = GlobalCode.Field2Long(n["colIdBigint"]),
                                        TravelReqID = GlobalCode.Field2Long(n["colTravelReqIDInt"]),
                                        RecordLocator = GlobalCode.Field2String(n["colRecordLocatorVarchar"]),
                                        TranspoVendorID = GlobalCode.Field2Long(n["colTranspoVendorIDInt"]),
                                        VehiclePlateNo = GlobalCode.Field2String(n["colVehiclePlateNoVarchar"]),
                                        PickUpDate = GlobalCode.Field2DateTime(n["colPickUpDate"]),
                                        PickUpTime = GlobalCode.Field2DateTime(n["colPickUpTime"]),
                                        DropOffDate = GlobalCode.Field2DateTime(n["colDropOffDate"]),
                                        DropOffTime = GlobalCode.Field2DateTime(n["colDropOffTime"]),
                                        ConfirmationNo = GlobalCode.Field2String(n["colConfirmationNoVarchar"]),
                                        VehicleStatus = GlobalCode.Field2String(n["colVehicleStatusVarchar"]),
                                        VehicleTypeId = GlobalCode.Field2Int(n["colVehicleTypeIdInt"]),
                                        SFStatus = GlobalCode.Field2String(n["colSFStatus"]),
                                        RouteIDFrom = GlobalCode.Field2Int(n["colRouteIDFromInt"]),
                                        RouteIDTo = GlobalCode.Field2Int(n["colRouteIDToInt"]),
                                        From = GlobalCode.Field2String(n["colFromVarchar"]),
                                        To = GlobalCode.Field2String(n["colToVarchar"]),
                                        DateCreated = GlobalCode.Field2DateTime(n["colDateCreatedDatetime"]),
                                        DateModified = GlobalCode.Field2DateTime(n["colDateModifiedDatetime"]),
                                        CreatedBy = GlobalCode.Field2String(n["colCreatedByVarchar"]),
                                        Modifiedby = GlobalCode.Field2String(n["colModifiedbyVarchar"]),

                                        IsActive = GlobalCode.Field2Bool(n["colIsActiveBit"]), 
                                        RemarksForAudit = GlobalCode.Field2String(n["colRemarksForAuditVarchar"]), 
                                        HotelID = GlobalCode.Field2Int(n["colHotelIDInt"]), 
                                        IsVisible = GlobalCode.Field2Bool(n["colIsVisibleBit"]),
                                        ContractId = GlobalCode.Field2Int(n["colContractIdInt"]), 
                                        IsSeaport = GlobalCode.Field2Bool(n["colIsSeaportBit"]),
                                        SeqNo = GlobalCode.Field2Int(n["colSeqNoInt"]), 
                                        Driver = GlobalCode.Field2String(n["colDriverVarchar"]), 
                                        VehicleDispatchTime = GlobalCode.Field2String(n["colVehicleDispatchTime"]),
                                        RouteFrom = GlobalCode.Field2String(n["colRouteFromVarchar"]), 
                                        RouteTo = GlobalCode.Field2String(n["colRouteToVarchar"]), 
                                        VehicleUnset = GlobalCode.Field2Bool(n["colVehicleUnset"]), 
                                        VehicleUnsetBy = GlobalCode.Field2String(n["colVehicleUnsetBy"]), 
                                        VehicleUnsetDate = GlobalCode.Field2DateTime(n["colVehicleUnsetDate"]), 
                                        ConfirmBy = GlobalCode.Field2String(n["colConfirmByVarchar"]), 
                                        Comments = GlobalCode.Field2String(n["colCommentsVarchar"]), 
                                        VehicleVendor = GlobalCode.Field2String(n["colVehicleVendorName"]), 
                                        ContractedRateMoney = GlobalCode.Field2Double(n["colContractedRateMoney"]),
                                        ConfirmRateMoney = GlobalCode.Field2Double(n["colConfirmRateMoney"]), 
                                        CurrencyInt = GlobalCode.Field2Int(n["colCurrencyInt"]), 
                                        StatusID = GlobalCode.Field2TinyInt(n["colStatusIDTinyint"]), 
                                        ApprovedBy = GlobalCode.Field2String(n["colApprovedByVarchar"]), 
                                        ApprovedDate = GlobalCode.Field2DateTime(n["colApprovedDate"]), 
                                        EmailTo = GlobalCode.Field2String(n["colEmailTovarchar"]),
                                        RequestSourceID = GlobalCode.Field2TinyInt(n["colRequestSourceIDInt"]), 
                                        TransportationDetails = GlobalCode.Field2String(n["colTransportationDetails"]), 
                                        IsPortAgent = GlobalCode.Field2Bool(n["colIsPortAgentBit"]),

                                        ColorCode = GlobalCode.Field2String(n["ColorCode"]),
                                        ForeColor = GlobalCode.Field2String(n["ForeColor"]),

                                   }).ToList();

               return VehicleTransMedical;

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

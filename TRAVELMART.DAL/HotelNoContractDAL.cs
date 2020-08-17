using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class HotelNoContractDAL
    {
        /// <summary>
        /// Date Created:   22/10/2011
        /// Created By:     Muhallidin G Wali
        /// (description)   Get No Hotel Contract Exception
        /// </summary>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>        
        /// <returns></returns>
        public static DataTable GetException(DateTime DateFrom, DateTime DateTo, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectLogException");
                db.AddInParameter(command, "@pDateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@pDateTo", DbType.DateTime, DateTo);
                db.AddInParameter(command, "@pStartRow", DbType.Int16, StartRow);
                db.AddInParameter(command, "@pMaxRow", DbType.Int64, MaxRow);
                dt = db.ExecuteDataSet(command).Tables[0];

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
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// =================================================================
        /// Date Created:   22/10/2011
        /// Created By:     Muhallidin G wali
        /// (description)   Get No Hotel Contract Exception Total Row Count
        /// =================================================================
        /// </summary>
        public static int GetExceptionCount(DateTime DateFrom, DateTime DateTo)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand command = null;
            IDataReader dr = null;
            try
            {
                command = db.GetStoredProcCommand("uspSelectLogExceptionCount");
                db.AddInParameter(command, "@pDateFrom", DbType.DateTime, DateFrom);
                db.AddInParameter(command, "@pDateTo", DbType.DateTime, DateTo);

                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    return int.Parse(dr["TotalRowCount"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }

        /// <summary>
        /// ===============================================================
        /// Author: Muhallidin G Wali
        /// Date Created: 08/03/2012
        /// Description: load all queries for No Contract exception bookings for new UI
        /// ===============================================================
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <returns></returns>
        public List<HotelTransactionExceptionGenericClass> LoadNoHotelHotelExceptionPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<HotelTransactionExceptionGenericClass> overflow = new List<HotelTransactionExceptionGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            DataTable dtHotels = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelNoContractPage");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                ds = db.ExecuteDataSet(dbCommand);
                dtHotels = ds.Tables[0];
                dtException = ds.Tables[1];

                overflow.Add(new HotelTransactionExceptionGenericClass()
                {
                    Hotels = (from a in dtHotels.AsEnumerable()
                              select new Hotels
                              {
                                  VendorId = a.Field<int>("VendorId"),
                                  BranchId = a.Field<int>("BranchId"),
                                  BranchName = a.Field<string>("BranchName"),
                                  CountryId = a.Field<int>("CountryId"),
                                  CityId = a.Field<int>("CityId"),
                                  isAccredited = a.Field<bool>("isAccredited"),
                                  withEvent = a.Field<bool>("withEvent"),
                                  ContractId = a.Field<int>("ContractId"),
                                  withContract = a.Field<bool>("withContract"),
                                  colDate = a.Field<DateTime>("colDate"),
                              }).ToList(),
                    ExceptionBooking = (from a in dtException.AsEnumerable()
                                        select new ExceptionBooking
                                        {   
                                            ExceptionIdBigInt = GlobalCode.Field2Long(a["ExceptionIdBigInt"]),
                                            IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                            SeqNo = GlobalCode.Field2TinyInt(a["SeqNo"]),
                                            TravelReqId = a.Field<int?>("TravelReqId"),
                                            E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                            SeafarerId = a.Field<int?>("SeafarerId"),
                                            SeafarerName = a.Field<string>("SeafarerName"),
                                            PortId = a.Field<int?>("PortId"),
                                            PortName = a.Field<string>("PortName"),
                                            SFStatus = a.Field<string>("SFStatus"),
                                            OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                            ArrivalDepartureDatetime = a.Field<DateTime?>("ArrivalDepartureDatetime"),
                                            Carrier = a.Field<string>("Carrier"),
                                            FlightNo = a.Field<string>("FlightNo"),
                                            FromCity = a.Field<string>("FromCity"),
                                            ToCity = a.Field<string>("ToCity"),
                                            RankName = a.Field<string>("RankName"),
                                            Stripes = a.Field<decimal?>("Stripes"),
                                            RecordLocator = a.Field<string>("RecordLocator"),
                                            Gender = a.Field<string>("Gender"),
                                            Nationality = a.Field<string>("Nationality"),
                                            Birthday = a.Field<DateTime?>("Birthday"),
                                            RoomTypeId = a.Field<int?>("RoomTypeId"),
                                            RoomType = a.Field<string>("RoomType"),
                                            ReasonCode = a.Field<string>("ReasonCode"),
                                            ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                            Invalid = a.Field<bool?>("Invalid"),
                                            VesselName = a.Field<string>("VesselName"),
                                            BookingRemarks = a.Field<string>("BookingRemarks")
                                        }).ToList()
                });
                return overflow;
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
                if (dtException != null)
                {
                    dtException.Dispose();
                }
                if (dtHotels != null)
                {
                    dtHotels.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// ===============================================================
        /// Modified By:    Josephine Gad
        /// Date Created:   17/Mar/2013
        /// Description:    Change List to Void
        ///                 Assign Session values here
        /// ===============================================================
        /// </summary>
        public void LoadNonTurnPortsExceptionPage(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            //List<NonTurnPortsGenericClass> NonTurnPorts = new List<NonTurnPortsGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtNew = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;
            DataTable dtEmail = null;
            DataTable dtEmailHotelSpecialist = null;


            HttpContext.Current.Session["HotelNonTurnPortsListTableNew"] = dtNew;

            try
            {
                List<NonTurnPortsList> NonTurnPortsList = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsConfirmed = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsCancelled = new List<NonTurnPortsList>();
                List<PortAgentEmail> PortAgentEmail = new List<PortAgentEmail>();
                                
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsContractPage");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                dbCommand.CommandTimeout = 60;
                ds = db.ExecuteDataSet(dbCommand);

                dtNew = ds.Tables[0];
                dtConfirmed = ds.Tables[1];
                dtCancelled = ds.Tables[2];
                dtEmail = ds.Tables[3];
                dtEmailHotelSpecialist = ds.Tables[4];
                

                HttpContext.Current.Session["HotelNonTurnPortsListTableNew"] = dtNew;

                
                HttpContext.Current.Session["HotelNonTurnPortsListExceptionBooking"] = NonTurnPortsList;
                HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;


                NonTurnPortsList = (from a in dtNew.AsEnumerable()
                                    select new NonTurnPortsList
                                    {

                                        IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                        TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                        E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
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
                                        Birthday = a.Field<string>("Birthday"),
                                        HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                        RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                        RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                        AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                        deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                        ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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
                                        ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])

                                    }).ToList();

                NonTurnPortsConfirmed= (from a in dtConfirmed.AsEnumerable()
                                    select new NonTurnPortsList
                                    {

                                        IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                        TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                        E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
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
                                        Birthday = a.Field<string>("Birthday"),
                                        HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                        RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                        RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                        AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                        deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                        ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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

                                        ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                                        ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),

                                        Remarks = GlobalCode.Field2String(a["Remarks"]),
                                        GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),

                                        ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                                        ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])
                                       
                                    }).ToList();

                NonTurnPortsCancelled = (from a in dtCancelled.AsEnumerable()
                                         select new NonTurnPortsList
                                         {

                                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
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
                                             Birthday = a.Field<string>("Birthday"),
                                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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

                                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),
                                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),

                                             ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                                             ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])

                                         }).ToList();

                PortAgentEmail = (from a in dtEmail.AsEnumerable()
                                         select new PortAgentEmail
                                         {
                                             EmailTo = GlobalCode.Field2String(a["EmailTo"]),
                                             EmailCc = GlobalCode.Field2String(a["EmailCc"]),

                                         }).ToList();


                HttpContext.Current.Session["HotelNonTurnPortsListExceptionBooking"] = NonTurnPortsList;
                HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;

                TravelMartVariable.HotelSpecialistEmail = "";

                if (dtEmailHotelSpecialist.Rows.Count > 0)
                { 
                    TravelMartVariable.HotelSpecialistEmail = GlobalCode.Field2String(dtEmailHotelSpecialist.Rows[0]["colEmailVarchar"]);
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
                if (dtNew != null)
                {
                    dtNew.Dispose();
                }
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (dtEmail != null)
                {
                    dtEmail.Dispose();
                }
                if (dtEmailHotelSpecialist != null)
                {
                    dtEmailHotelSpecialist.Dispose();
                }
            }
        }
        /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   21/Jan/2014
        /// Description:    Get NonTurn Port Manifest using dynamic table
        /// ===============================================================
        /// </summary>
        public void LoadNonTurnPortsExceptionPageDynamic(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            //List<NonTurnPortsGenericClass> NonTurnPorts = new List<NonTurnPortsGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtNew = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;
            DataTable dtEmail = null;

            HttpContext.Current.Session["HotelNonTurnPortsListTableNew"] = dtNew;

            try
            {
                List<NonTurnPortsList> NonTurnPortsList = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsConfirmed = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsCancelled = new List<NonTurnPortsList>();
                List<PortAgentEmail> PortAgentEmail = new List<PortAgentEmail>();

                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsContractPageDynamic");
                dbCommand.CommandTimeout = 60;
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                ds = db.ExecuteDataSet(dbCommand);

                dtNew = ds.Tables[0];
                dtConfirmed = ds.Tables[1];
                dtCancelled = ds.Tables[2];
                dtEmail = ds.Tables[3];


                HttpContext.Current.Session["HotelNonTurnPortsListTableNew"] = dtNew;
                HttpContext.Current.Session["HotelNonTurnPortsListTableConfirmed"] = dtConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListTableCancelled"] = dtCancelled;


                //HttpContext.Current.Session["HotelNonTurnPortsListExceptionBooking"] = NonTurnPortsList;
                //HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                //HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;

                //NonTurnPortsList = (from a in dtNew.AsEnumerable()
                //                    select new NonTurnPortsList
                //                    {

                //                        IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                //                        TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                //                        E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                //                        RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                //                        PortId = GlobalCode.Field2Int(a["PortId"]),
                //                        SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                //                        HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                //                        Checkin = GlobalCode.Field2String(a["Checkin"]),
                //                        CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                //                        HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                //                        LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                //                        FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),

                //                        Employee = GlobalCode.Field2Long(a["Employee"]),
                //                        Gender = GlobalCode.Field2String(a["Gender"]),
                //                        SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                //                        Couple = GlobalCode.Field2String(a["Couple"]),
                //                        Title = GlobalCode.Field2String(a["Title"]),
                //                        Ship = GlobalCode.Field2String(a["Ship"]),
                //                        Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                //                        Nationality = GlobalCode.Field2String(a["Natioality"]),
                //                        Birthday = a.Field<string>("Birthday"),
                //                        HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                //                        RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                //                        RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                //                        AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                //                        deptCity = GlobalCode.Field2String(a["DeptCity"]),
                //                        ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
                //                        Arvldate = GlobalCode.Field2String(a["ArrvlDate"]),
                //                        ArvlTime = GlobalCode.Field2String(a["ArrvlTime"]),
                //                        Carrier = GlobalCode.Field2String(a["Carrier"]),
                //                        FlightNo = GlobalCode.Field2String(a["FlightNo"]),
                //                        Voucher = GlobalCode.Field2String(a["Voucher"]),
                //                        PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                //                        PassportExp = GlobalCode.Field2String(a["PassportExp"]),
                //                        PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                //                        HotelBranch = GlobalCode.Field2String(a["HotelBranch"]),
                //                        Booking = GlobalCode.Field2String(a["Booking"]),
                //                        Bookingremark = GlobalCode.Field2String(a["Bookingremark"]),
                //                        IsVisible = GlobalCode.Field2Bool(a["IsVisible"]),
                //                        stripes = GlobalCode.Field2Decimal(a["colStripesDecimal"]),
                //                        GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),

                //                        ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                //                        ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestedDate"])

                //                    }).ToList();

                //NonTurnPortsConfirmed = (from a in dtConfirmed.AsEnumerable()
                //                         select new NonTurnPortsList
                //                         {

                //                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                //                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                //                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                //                             RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                //                             PortId = GlobalCode.Field2Int(a["PortId"]),
                //                             SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                //                             HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                //                             Checkin = GlobalCode.Field2String(a["Checkin"]),
                //                             CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                //                             HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                //                             LastName = GlobalCode.Field2String(a["LastName"]),
                //                             FirstName = GlobalCode.Field2String(a["FirstName"]),

                //                             Employee = GlobalCode.Field2Long(a["Employee"]),
                //                             Gender = GlobalCode.Field2String(a["Gender"]),
                //                             SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                //                             Couple = GlobalCode.Field2String(a["Couple"]),
                //                             Title = GlobalCode.Field2String(a["Title"]),
                //                             Ship = GlobalCode.Field2String(a["Ship"]),
                //                             Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                //                             Nationality = GlobalCode.Field2String(a["Natioality"]),
                //                             Birthday = a.Field<string>("Birthday"),
                //                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                //                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                //                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                //                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                //                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                //                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
                //                             Arvldate = GlobalCode.Field2String(a["ArrvlDate"]),
                //                             ArvlTime = GlobalCode.Field2String(a["ArrvlTime"]),
                //                             Carrier = GlobalCode.Field2String(a["Carrier"]),
                //                             FlightNo = GlobalCode.Field2String(a["FlightNo"]),
                //                             Voucher = GlobalCode.Field2String(a["Voucher"]),
                //                             PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                //                             PassportExp = GlobalCode.Field2String(a["PassportExp"]),
                //                             PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                //                             HotelBranch = GlobalCode.Field2String(a["HotelBranch"]),
                //                             Booking = GlobalCode.Field2String(a["Booking"]),
                //                             Bookingremark = GlobalCode.Field2String(a["Bookingremark"]),
                //                             IsVisible = GlobalCode.Field2Bool(a["IsVisible"]),
                //                             stripes = GlobalCode.Field2Decimal(a["colStripesDecimal"]),

                //                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                //                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),

                //                             Remarks = GlobalCode.Field2String(a["Remarks"]),
                //                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),

                //                             ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                //                             ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestedDate"])

                //                         }).ToList();

                //NonTurnPortsCancelled = (from a in dtCancelled.AsEnumerable()
                //                         select new NonTurnPortsList
                //                         {

                //                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                //                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                //                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                //                             RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                //                             PortId = GlobalCode.Field2Int(a["PortId"]),
                //                             SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                //                             HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                //                             Checkin = GlobalCode.Field2String(a["Checkin"]),
                //                             CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                //                             HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                //                             LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                //                             FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),

                //                             Employee = GlobalCode.Field2Long(a["Employee"]),
                //                             Gender = GlobalCode.Field2String(a["Gender"]),
                //                             SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                //                             Couple = GlobalCode.Field2String(a["Couple"]),
                //                             Title = GlobalCode.Field2String(a["Title"]),
                //                             Ship = GlobalCode.Field2String(a["Ship"]),
                //                             Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                //                             Nationality = GlobalCode.Field2String(a["Natioality"]),
                //                             Birthday = a.Field<string>("Birthday"),
                //                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                //                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                //                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                //                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                //                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                //                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
                //                             Arvldate = GlobalCode.Field2String(a["ArrvlDate"]),
                //                             ArvlTime = GlobalCode.Field2String(a["ArrvlTime"]),
                //                             Carrier = GlobalCode.Field2String(a["Carrier"]),
                //                             FlightNo = GlobalCode.Field2String(a["FlightNo"]),
                //                             Voucher = GlobalCode.Field2String(a["Voucher"]),
                //                             PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                //                             PassportExp = GlobalCode.Field2String(a["PassportExp"]),
                //                             PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                //                             HotelBranch = GlobalCode.Field2String(a["HotelBranch"]),
                //                             Booking = GlobalCode.Field2String(a["Booking"]),
                //                             Bookingremark = GlobalCode.Field2String(a["Bookingremark"]),
                //                             IsVisible = GlobalCode.Field2Bool(a["IsVisible"]),
                //                             stripes = GlobalCode.Field2Decimal(a["colStripesDecimal"]),

                //                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                //                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),
                //                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),

                //                             ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                //                             ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestedDate"])

                //                         }).ToList();

                PortAgentEmail = (from a in dtEmail.AsEnumerable()
                                  select new PortAgentEmail
                                  {
                                      EmailTo = GlobalCode.Field2String(a["EmailTo"]),
                                      EmailCc = GlobalCode.Field2String(a["EmailCc"]),

                                  }).ToList();


                //HttpContext.Current.Session["HotelNonTurnPortsListExceptionBooking"] = NonTurnPortsList;
                //HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                //HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;

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
                if (dtNew != null)
                {
                    dtNew.Dispose();
                }
                if (dtConfirmed != null)
                {
                    dtConfirmed.Dispose();
                }
                if (dtCancelled != null)
                {
                    dtCancelled.Dispose();
                }
                if (dtEmail != null)
                {
                    dtEmail.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   01/10/2012
        /// Description:    Add or remove record from Exception List
        /// ---------------------------------------------------------------
        /// </summary>
        /// <param name="ExceptionIDInt"></param>
        /// <param name="IsRemovedBit"></param>
        /// <param name="UserId"></param>
        /// <param name="strLogDescription"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public static void ExceptionAddRemoveFromList(Int64 ExceptionIDInt, bool IsRemovedBit, string UserId,
            String strLogDescription, String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspSaveExceptionAddRemoveFromList");

                db.AddInParameter(dbCommand, "@pHotelExceptionIdBigInt", DbType.Int64, ExceptionIDInt);
                db.AddInParameter(dbCommand, "@pIsRemovedBit", DbType.Boolean, IsRemovedBit);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

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
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Get records removed from Exception List
        /// ---------------------------------------------------------------
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserId"></param>
        /// <param name="Loadtype"></param>
        /// <param name="RegionID"></param>
        /// <param name="PortID"></param>
        /// <returns></returns>
        public static List<ExceptionBooking> ExceptionGetRemovedList(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<ExceptionBooking> ExceptionList = new List<ExceptionBooking>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtException = null;
            int iTotalRows = 0;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelExceptionRemovedList");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                ds = db.ExecuteDataSet(dbCommand);
                dtException = ds.Tables[1];
                iTotalRows = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());
                ExceptionList = (from a in dtException.AsEnumerable()
                                 select new ExceptionBooking
                                 {
                                     ExceptionIdBigInt = GlobalCode.Field2Int(a["ExceptionIdBigInt"]),
                                     IdBigint = GlobalCode.Field2Int(a["IDBigint"]),
                                     SeqNo = GlobalCode.Field2TinyInt(a["SeqNo"]),
                                     TravelReqId = a.Field<int?>("TravelReqId"),
                                     E1TravelReqId = a.Field<int?>("E1TravelReqId"),
                                     SeafarerId = a.Field<int?>("SeafarerId"),
                                     SeafarerName = a.Field<string>("SeafarerName"),
                                     PortId = a.Field<int?>("PortId"),
                                     PortName = a.Field<string>("PortName"),
                                     SFStatus = a.Field<string>("SFStatus"),
                                     OnOffDate = a.Field<DateTime?>("OnOffDate"),
                                     ArrivalDepartureDatetime = a.Field<DateTime?>("ArrivalDepartureDatetime"),
                                     Carrier = a.Field<string>("Carrier"),
                                     FlightNo = a.Field<string>("FlightNo"),
                                     FromCity = a.Field<string>("FromCity"),
                                     ToCity = a.Field<string>("ToCity"),
                                     RankName = a.Field<string>("RankName"),
                                     Stripes = a.Field<decimal?>("Stripes"),
                                     RecordLocator = a.Field<string>("RecordLocator"),
                                     Gender = a.Field<string>("Gender"),
                                     Nationality = a.Field<string>("Nationality"),
                                     RoomTypeId = a.Field<int?>("RoomTypeId"),
                                     RoomType = a.Field<string>("RoomType"),
                                     ReasonCode = a.Field<string>("ReasonCode"),
                                     ExceptionRemarks = a.Field<string>("ExceptionRemarks"),
                                     Invalid = a.Field<bool?>("Invalid"),
                                     VesselName = a.Field<string>("VesselName"),
                                     BookingRemarks = a.Field<string>("BookingRemarks")
                                 }).ToList();

                HttpContext.Current.Session["HotelTransactionExceptionExceptionBookingRemovedCount"] = iTotalRows;
                return ExceptionList;
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
                if (dtException != null)
                {
                    dtException.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/Mar/2013
        /// Description:    Confirm record and get the new confirmed and cancelled record
        /// ---------------------------------------------------------------
        /// </summary>
        public static void ConfirmNonTurnPortList(string UserId, DateTime dDate, int iPort,
            bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
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
                List<NonTurnPortsList> NonTurnPortsList = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsConfirmed = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsCancelled = new List<NonTurnPortsList>();
                List<PortAgentEmail> PortAgentEmail = new List<PortAgentEmail>();


                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspConfirmNonTurnPorts");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, dDate);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPort);

                db.AddInParameter(dbCommand, "@pIsSave", DbType.Boolean, bIsSave);
                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCc", DbType.String, sEmailCc);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);//db.ExecuteNonQuery(dbCommand, trans);
                dtConfirmed = ds.Tables[0];
                dtCancelled = ds.Tables[1];
                dtEmail = ds.Tables[2];

                HttpContext.Current.Session["HotelNonTurnPortsListExceptionBooking"] = NonTurnPortsList;
                HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;
                HttpContext.Current.Session["NonTurnPortDateConfirmed"] = null;


                NonTurnPortsConfirmed = (from a in dtConfirmed.AsEnumerable()
                                         select new NonTurnPortsList {
                                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
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
                                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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

                                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),

                                             Remarks = GlobalCode.Field2String(a["Remarks"]),
                                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),
                                             Birthday = GlobalCode.Field2String(a["Birthday"]),

                                             ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                                             ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])
                                         
                                         }).ToList();
                NonTurnPortsCancelled = (from a in dtCancelled.AsEnumerable()
                                         select new NonTurnPortsList
                                         {
                                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
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
                                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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

                                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),
                                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),
                                             Birthday = GlobalCode.Field2String(a["Birthday"]),

                                         }).ToList();

                PortAgentEmail = (from a in dtEmail.AsEnumerable()
                                  select new PortAgentEmail
                                  {
                                      EmailTo = GlobalCode.Field2String(a["EmailTo"]),
                                      EmailCc = GlobalCode.Field2String(a["EmailCc"]),

                                  }).ToList();

                HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;
                HttpContext.Current.Session["NonTurnPortDateConfirmed"] = ds.Tables[3].Rows[0]["DateConfirmed"].ToString();

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
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/Jan/2014
        /// Description:    Confirm record and get the new confirmed and cancelled record using dynamic tables
        /// ---------------------------------------------------------------
        /// </summary>
        public static void ConfirmNonTurnPortListDynamic(string UserId, DateTime dDate, int iPort,
            bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            DataSet ds = null;
            DataTable dtConfirmed = null;
            DataTable dtConfirmedQuery = null;

            DataTable dtCancelled = null;
            DataTable dtEmail = null;

            try
            {

                HttpContext.Current.Session["HotelNonTurnPortsListTableNew"] = null;
                HttpContext.Current.Session["HotelNonTurnPortsListTableConfirmed"] = dtConfirmed;

                //List<NonTurnPortsList> NonTurnPortsList = new List<NonTurnPortsList>();
                //List<NonTurnPortsList> NonTurnPortsConfirmed = new List<NonTurnPortsList>();
                List<NonTurnPortsList> NonTurnPortsCancelled = new List<NonTurnPortsList>();
                List<PortAgentEmail> PortAgentEmail = new List<PortAgentEmail>();


                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspConfirmNonTurnPortsDynamic");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, dDate);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPort);

                db.AddInParameter(dbCommand, "@pIsSave", DbType.Boolean, bIsSave);
                db.AddInParameter(dbCommand, "@pEmailTo", DbType.String, sEmailTo);
                db.AddInParameter(dbCommand, "@pEmailCc", DbType.String, sEmailCc);

                db.AddInParameter(dbCommand, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                ds = db.ExecuteDataSet(dbCommand, trans);//db.ExecuteNonQuery(dbCommand, trans);
                dtConfirmed = ds.Tables[0];
                dtConfirmedQuery = ds.Tables[1];

                dtCancelled = ds.Tables[2];
                dtEmail = ds.Tables[3];


                HttpContext.Current.Session["HotelNonTurnPortsListTableConfirmed"] = dtConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListQueryConfirmed"] = GlobalCode.Field2String(dtConfirmedQuery.Rows[0][1]);


                //HttpContext.Current.Session["HotelNonTurnPortsListExceptionBooking"] = NonTurnPortsList;
                //HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;
                HttpContext.Current.Session["NonTurnPortDateConfirmed"] = null;


                //NonTurnPortsConfirmed = (from a in dtConfirmed.AsEnumerable()
                //                         select new NonTurnPortsList
                //                         {
                //                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                //                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                //                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                //                             RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                //                             PortId = GlobalCode.Field2Int(a["PortId"]),
                //                             SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                //                             HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                //                             Checkin = GlobalCode.Field2String(a["Checkin"]),
                //                             CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                //                             HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                //                             LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                //                             FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),

                //                             Employee = GlobalCode.Field2Long(a["Employee"]),
                //                             Gender = GlobalCode.Field2String(a["Gender"]),
                //                             SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                //                             Couple = GlobalCode.Field2String(a["Couple"]),
                //                             Title = GlobalCode.Field2String(a["Title"]),
                //                             Ship = GlobalCode.Field2String(a["Ship"]),
                //                             Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                //                             Nationality = GlobalCode.Field2String(a["Natioality"]),
                //                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                //                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                //                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                //                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                //                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                //                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
                //                             Arvldate = GlobalCode.Field2String(a["ArrvlDate"]),
                //                             ArvlTime = GlobalCode.Field2String(a["ArrvlTime"]),
                //                             Carrier = GlobalCode.Field2String(a["Carrier"]),
                //                             FlightNo = GlobalCode.Field2String(a["FlightNo"]),
                //                             Voucher = GlobalCode.Field2String(a["Voucher"]),
                //                             PassportNo = GlobalCode.Field2String(a["PassportNo"]),
                //                             PassportExp = GlobalCode.Field2String(a["PassportExp"]),
                //                             PassportIssued = GlobalCode.Field2String(a["PassportIssued"]),
                //                             HotelBranch = GlobalCode.Field2String(a["HotelBranch"]),
                //                             Booking = GlobalCode.Field2String(a["Booking"]),
                //                             Bookingremark = GlobalCode.Field2String(a["Bookingremark"]),
                //                             IsVisible = GlobalCode.Field2Bool(a["IsVisible"]),
                //                             stripes = GlobalCode.Field2Decimal(a["colStripesDecimal"]),

                //                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                //                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),

                //                             Remarks = GlobalCode.Field2String(a["Remarks"]),
                //                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),
                //                             Birthday = GlobalCode.Field2String(a["Birthday"]),

                //                             ServiceRequested = GlobalCode.Field2String(a["ServiceRequested"]),
                //                             ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])

                                         //}).ToList();
                NonTurnPortsCancelled = (from a in dtCancelled.AsEnumerable()
                                         select new NonTurnPortsList
                                         {
                                             IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                             TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                             E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
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
                                             HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                             RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                             RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                             AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                             deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                             ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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

                                             ConfirmedBy = GlobalCode.Field2String(a["colConfirmedBy"]),
                                             ConfirmedDate = GlobalCode.Field2String(a["colConfirmedDate"]),
                                             GroupNo = GlobalCode.Field2TinyInt(a["GroupNo"]),
                                             Birthday = GlobalCode.Field2String(a["Birthday"]),

                                         }).ToList();

                PortAgentEmail = (from a in dtEmail.AsEnumerable()
                                  select new PortAgentEmail
                                  {
                                      EmailTo = GlobalCode.Field2String(a["EmailTo"]),
                                      EmailCc = GlobalCode.Field2String(a["EmailCc"]),

                                  }).ToList();

                //HttpContext.Current.Session["HotelNonTurnPortsListConfirmed"] = NonTurnPortsConfirmed;
                HttpContext.Current.Session["HotelNonTurnPortsListCancelled"] = NonTurnPortsCancelled;
                HttpContext.Current.Session["PortAgentEmail"] = PortAgentEmail;
                HttpContext.Current.Session["NonTurnPortDateConfirmed"] = ds.Tables[4].Rows[0]["DateConfirmed"].ToString();

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
            }
        }
        /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   24/Sept/2013
        /// Description:    Get confirmed and cancelled Nonturn Port Manifest
        /// ===============================================================
        /// </summary>
        public static DataSet GetNonTurnPortsExport(DateTime Date, string UserId, int RegionID, int PortID, string sOrderBy)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsExport");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Date);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);

                ds = db.ExecuteDataSet(dbCommand);
                return ds;
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
            }
        }
        /// <summary>
        /// ===============================================================
        /// Created By:     Josephine Gad
        /// Date Created:   03/Feb/2014
        /// Description:    Get confirmed and cancelled Nonturn Port Manifest using dynamic table
        /// ===============================================================
        /// </summary>
        public static DataSet GetNonTurnPortsExportDynamic(DateTime Date, string UserId, int RegionID, int PortID, string sOrderBy)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsExportDynamic");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Date);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, sOrderBy);

                ds = db.ExecuteDataSet(dbCommand);
                return ds;
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
            }
        }

        /// <summary>
        /// ===============================================================
        /// Modified By:    Muhallidin G Wali
        /// Date Created:   10/Feb/2016
        /// Description:    Change List to Void
        ///                 Assign Session values here
        /// ===============================================================
        /// </summary>
        public List<NonTurnPortsList> LoadNonTurnPortsExceptionPagNew(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            //List<NonTurnPortsGenericClass> NonTurnPorts = new List<NonTurnPortsGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtNew = null;

            List<NonTurnPortsList> nonturnport = new List<NonTurnPortsList>();

            try
            {
              
          

                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsContractPageNew");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                dbCommand.CommandTimeout = 60;
                ds = db.ExecuteDataSet(dbCommand);

                dtNew = ds.Tables[0];
                nonturnport = (from a in dtNew.AsEnumerable()
                                    select new NonTurnPortsList
                                    {

                                        IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                        TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                        E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                                        RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                        PortId = GlobalCode.Field2Int(a["PortId"]),
                                        SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                                        HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                        Checkin = GlobalCode.Field2String(a["Checkin"]),
                                        CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                                        HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                                        Employee = GlobalCode.Field2Long(a["Employee"]),
                                        LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                        FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                                        Gender = GlobalCode.Field2String(a["Gender"]),
                                        SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                                        Couple = GlobalCode.Field2String(a["Couple"]),
                                        Title = GlobalCode.Field2String(a["Title"]),
                                        Ship = GlobalCode.Field2String(a["Ship"]),
                                        Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                                        Nationality = GlobalCode.Field2String(a["Nationality"]),
                                        Birthday = a.Field<string>("Birthday"),
                                        HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                        RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                        RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                        AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                        deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                        ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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
                                        ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])

                   }).ToList();
                 
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
                if (dtNew != null)
                {
                    dtNew.Dispose();
                }
               
            }

            return nonturnport;
        }



        /// <summary>
        /// Date Created:   13/Mar/2013
        /// Created By:     Josephine Gad
        /// (description)   Get list of Ports to be used in Non Turn Port
        ///                 Get the count of Travel Request in Non Turn Port
        /// </summary>
        /// <param name="sPortName"></param>
        /// <param name="sPortCode"></param>
        /// <param name="sPortID"></param>
        /// <param name="dDate"></param>
        public static void GetNonTurnPortsContractPageCount(DateTime Date, string UserId, int Loadtype, int RegionID, int PortID)
        {
            List<PortList> list = new List<PortList>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable dt = null;
            DataSet ds = null;
            HttpContext.Current.Session["NonTurnPortCount"] = 0;
            HttpContext.Current.Session["GetPortForNonTurn"] = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsContractPageCount");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortIDInt", DbType.Int32, PortID);

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[1];
                list = (from a in dt.AsEnumerable()
                        select new PortList
                        {
                            PortId =GlobalCode.Field2Long(a["PortID"]), //a.Field<Int64>("PortID"),
                            PortName = a.Field<string>("PortName"),

                        }).ToList();

                HttpContext.Current.Session["NonTurnPortCount"] = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());
                HttpContext.Current.Session["PortForNonTurn"] = list;

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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }


        /// <summary>
        /// ===============================================================
        /// Modified By:    Muhallidin G Wali
        /// Date Created:   10/Feb/2016
        /// Description:    Get Service provider booking by statusID
        /// ===============================================================
        /// </summary>
        //public List<NonTurnPortsList> LoadServiceProviderHotelBooking(short LoadType,string UserId, DateTime Date,
        //        int PortAgentID, int StatusTypeID, int Days)
        //{

        public GenericNonTurnPort LoadServiceProviderHotelBooking(short LoadType, string UserId, DateTime Date,
                int PortAgentID, int StatusTypeID, int Days)
        {
            //List<NonTurnPortsGenericClass> NonTurnPorts = new List<NonTurnPortsGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            GenericNonTurnPort nonturnport = new GenericNonTurnPort();
            try
            {

                dbCommand = db.GetStoredProcCommand("uspGetServiceProviderHotelBooking");

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);			
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pPortAgentID", DbType.Int32 , PortAgentID);
                db.AddInParameter(dbCommand, "@pStatusTypeID", DbType.Int32, StatusTypeID);
                db.AddInParameter(dbCommand, "@pDays", DbType.Int32, Days);				


                dbCommand.CommandTimeout = 60;
                ds = db.ExecuteDataSet(dbCommand);

                nonturnport.NonTurnPortsRequest = (from a in ds.Tables[0].AsEnumerable()
                               select new NonTurnPortsList
                               {
                                   HotelTransID = GlobalCode.Field2Long(a["ExceptionIdBigInt"]),
                                   IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                   TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                   E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                                   RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                   PortId = GlobalCode.Field2Int(a["PortId"]),
                                   SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                                   HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                   Checkin = GlobalCode.Field2String(a["Checkin"]),
                                   CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                                   HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                                   Employee = GlobalCode.Field2Long(a["Employee"]),
                                   LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                   FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                                   Gender = GlobalCode.Field2String(a["Gender"]),
                                   SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                                   Couple = GlobalCode.Field2String(a["Couple"]),
                                   Title = GlobalCode.Field2String(a["Title"]),
                                   Ship = GlobalCode.Field2String(a["Ship"]),
                                   Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                                   Nationality = GlobalCode.Field2String(a["Nationality"]),
                                   Birthday = a.Field<string>("Birthday"),
                                   HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                   RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                   RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                   AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                   deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                   ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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
                                   ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])
                               }).ToList();

                nonturnport.NonTurnPortsConfirm = (from a in ds.Tables[1].AsEnumerable()
                                                   select new NonTurnPortsList
                                                   {
                                                       HotelTransID = GlobalCode.Field2Long(a["ExceptionIdBigInt"]),
                                                       IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                                       TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                                       E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                                                       RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                                       PortId = GlobalCode.Field2Int(a["PortId"]),
                                                       SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                                                       HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                                       Checkin = GlobalCode.Field2String(a["Checkin"]),
                                                       CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                                                       HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                                                       Employee = GlobalCode.Field2Long(a["Employee"]),
                                                       LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                                       FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                                                       Gender = GlobalCode.Field2String(a["Gender"]),
                                                       SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                                                       Couple = GlobalCode.Field2String(a["Couple"]),
                                                       Title = GlobalCode.Field2String(a["Title"]),
                                                       Ship = GlobalCode.Field2String(a["Ship"]),
                                                       Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                                                       Nationality = GlobalCode.Field2String(a["Nationality"]),
                                                       Birthday = a.Field<string>("Birthday"),
                                                       HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                                       RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                                       RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                                       AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                                       deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                                       ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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
                                                       ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])
                                                   }).ToList();
            
              nonturnport.NonTurnPortsCancel = (from a in ds.Tables[2].AsEnumerable()
                                                   select new NonTurnPortsList
                                                   {
                                                       HotelTransID = GlobalCode.Field2Long(a["ExceptionIdBigInt"]),
                                                       IDBigInt = GlobalCode.Field2Long(a["IDBigInt"]),
                                                       TravelReqID = GlobalCode.Field2Long(a["TravelReqID"]),
                                                       E1TravelReqID = GlobalCode.Field2Int(a["E1TravelReqID"]),
                                                       RoomTypeId = GlobalCode.Field2Int(a["RoomTypeId"]),
                                                       PortId = GlobalCode.Field2Int(a["PortId"]),
                                                       SFStatus = GlobalCode.Field2String(a["SFStatus"]),
                                                       HotelCity = GlobalCode.Field2String(a["HotelCity"]),
                                                       Checkin = GlobalCode.Field2String(a["Checkin"]),
                                                       CheckOut = GlobalCode.Field2String(a["CheckOut"]),
                                                       HotelNite = GlobalCode.Field2String(a["HotelNite"]),
                                                       Employee = GlobalCode.Field2Long(a["Employee"]),
                                                       LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                                       FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                                                       Gender = GlobalCode.Field2String(a["Gender"]),
                                                       SingleDouble = GlobalCode.Field2String(a["SingleDouble"]),
                                                       Couple = GlobalCode.Field2String(a["Couple"]),
                                                       Title = GlobalCode.Field2String(a["Title"]),
                                                       Ship = GlobalCode.Field2String(a["Ship"]),
                                                       Costcenter = GlobalCode.Field2String(a["Costcenter"]),
                                                       Nationality = GlobalCode.Field2String(a["Nationality"]),
                                                       Birthday = a.Field<string>("Birthday"),
                                                       HotelRequest = GlobalCode.Field2String(a["HotelRequest"]),
                                                       RecLoc = GlobalCode.Field2String(a["RecLoc"]),
                                                       RecLocID = GlobalCode.Field2Long(a["RecLocID"]),
                                                       AirSequence = GlobalCode.Field2Int(a["AirSequence"]),
                                                       deptCity = GlobalCode.Field2String(a["DeptCity"]),
                                                       ArvlCity = GlobalCode.Field2String(a["ArvlCity"]),
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
                                                       ServiceRequestedDate = GlobalCode.Field2String(a["ServiceRequestDate"])
                                                   }).ToList();
            
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

            return nonturnport;
        }















    }
}


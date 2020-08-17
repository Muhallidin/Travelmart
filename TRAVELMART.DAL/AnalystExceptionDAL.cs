using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using TRAVELMART.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class AnalystExceptionDAL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Dec/2012
        /// Descrption:     Get XML Exception and store in session
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sRole"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="sRecordLocator"></param>
        /// <param name="iSequenceNo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        public static void GetXMLExceptionList(string sUserName, string sRole,
            string DateFrom, string DateTo, string sRecordLocator, int iSequenceNo, 
            int StartRow, int MaxRow)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand comm = null;
            DataSet ds = null;
            List<XMLExceptionList> list = new List<XMLExceptionList>();
            try
            {
                 HttpContext.Current.Session["XMLExceptionList"] = list;
                 HttpContext.Current.Session["XMLExceptionCount"] = 0;

                comm = db.GetStoredProcCommand("uspSelectExceptionXML");
                db.AddInParameter(comm, "@pUserIDVarchar", DbType.String, sUserName);
                db.AddInParameter(comm, "@pUserRoleVarchar", DbType.String, sRole);
                db.AddInParameter(comm, "@pDateFrom", DbType.DateTime,  GlobalCode.Field2DateTime(DateFrom));
                if (DateTo.Trim() != "")
                {
                    db.AddInParameter(comm, "@pDateTo", DbType.DateTime, GlobalCode.Field2DateTime(DateTo));
                }
                db.AddInParameter(comm, "@pRecordLocator", DbType.String, sRecordLocator);
                db.AddInParameter(comm, "@pItinerarySequence", DbType.Int32, iSequenceNo);
                db.AddInParameter(comm, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, MaxRow);

                ds = db.ExecuteDataSet(comm);
                int iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);

                list = (from a in ds.Tables[1].AsEnumerable()
                            select new XMLExceptionList {
                                ExceptionID = GlobalCode.Field2Int(a["ExceptionIDBigint"]),
                                RecordLocator = a["FK_ItineraryRefID"].ToString(),
                                SequenceNo = GlobalCode.Field2Int(a["FK_ItinerarySeqNmbr"]),
                                Description = a["Description"].ToString(),
                                DateCreated = GlobalCode.Field2DateTime(a["DateCreatedDatetime"])
                        }).ToList();

                HttpContext.Current.Session["XMLExceptionList"] = list;
                HttpContext.Current.Session["XMLExceptionCount"] = iCount;
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
        /// Author:         Josephine Gad
        /// Date Created:   07/Dec/2012
        /// Descrption:     Get Active Exception List (active in PNR but X in Travel Routing) and store in session
        /// </summary>
        public static void GetActiveExceptionList(Int16 iFilterBy, string sFilter,
            string sRecordLocator, int iSequenceNo,
            int StartRow, int MaxRow)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand comm = null;
            DataSet ds = null;
            List<ActiveExceptionList> list = new List<ActiveExceptionList>();
            try
            {
                HttpContext.Current.Session["ActiveExceptionList"] = list;
                HttpContext.Current.Session["ActiveExceptionCount"] = 0;

                comm = db.GetStoredProcCommand("uspSelectExceptionTravelRouting");
                db.AddInParameter(comm, "@pFilterBy", DbType.Int16, iFilterBy);
                db.AddInParameter(comm, "@pFilterVarchar", DbType.String, sFilter);
               
                db.AddInParameter(comm, "@pRecordLocator", DbType.String, sRecordLocator);
                db.AddInParameter(comm, "@pItinerarySequence", DbType.Int32, iSequenceNo);

                db.AddInParameter(comm, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, MaxRow);

                ds = db.ExecuteDataSet(comm);
                int iCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);

                list = (from a in ds.Tables[1].AsEnumerable()
                        select new ActiveExceptionList
                        {
                            RecordLocator = a["colRecordLocatorVarchar"].ToString(),
                            SequenceNo = GlobalCode.Field2Int(a["colItinerarySeqNoSmallint"]),
                            Name = a["Name"].ToString(),
                            E1No = GlobalCode.Field2Int( a["colSeafarerIdInt"]),
                            E1TRNo = GlobalCode.Field2Int(a["colE1TravelReqIdInt"]),
                            Status = a["colStatusVarchar"].ToString(),
                            OnOffDate = GlobalCode.Field2DateTime(a["DateOnOff"])
                        }).ToList();

                HttpContext.Current.Session["ActiveExceptionList"] = list;
                HttpContext.Current.Session["ActiveExceptionCount"] = iCount;
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
        /// ===============================================================
        /// Modified By:    Josephine Gad
        /// Date Created:   17/Mar/2013
        /// Description:    Change List to Void
        ///                 Assign Session values here
        /// ===============================================================
        /// </summary>
        public static void GetNonTurnPortNotInTM(DateTime Date, string UserId, string PortCode, string OrderBy)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataSet ds = null;
            DataTable dtNew = null;
            DataTable dtConfirmed = null;
            DataTable dtCancelled = null;
            DataTable dtEmail = null;
            try
            {
                List<NonTurnPortsList> NonTurnPortsList = new List<NonTurnPortsList>();
                HttpContext.Current.Session["PortNotExistExceptionList"] = NonTurnPortsList;

                dbCommand = db.GetStoredProcCommand("uspGetNonTurnPortsNoInTM");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, Date);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pPortCode", DbType.String, PortCode);
                db.AddInParameter(dbCommand, "@pOrderby", DbType.String, OrderBy);

                ds = db.ExecuteDataSet(dbCommand);

                dtNew = ds.Tables[0];              
                
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
                                        PortName = GlobalCode.Field2String(a["PortName"]),
                                    }).ToList();

                HttpContext.Current.Session["PortNotExistExceptionList"] = NonTurnPortsList;                
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
    }
}

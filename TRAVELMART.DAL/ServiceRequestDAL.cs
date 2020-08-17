using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;
using System.Web;
using System.Data.SqlClient;


namespace TRAVELMART.DAL
{
    public class ServiceRequestDAL
    {
        /// <summary>
        /// Date Modified: 17/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get Service Request List
        /// </summary>
        public void GetServiceRequestList(DateTime dDate, string sUser, int iStartRow, int iMaxRow,
            Int16 iLoad, string sOrderBy, Int16 iViewFilter, Int16 iViewActive, Int16 iViewBooked,
            Int16 iFilterType, Int64 iEmployeeID, string sCrewAssistUser)
        {
            List<ServiceRequestList> list = new List<ServiceRequestList>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataSet ds = null;
            DataTable dt = null;
            int iCount = 0;

            HttpContext.Current.Session["ServiceRequestView_Count"] = iCount;
            HttpContext.Current.Session["ServiceRequestView_ServiceReqList"] = list;

            try
            {
                dbComm = db.GetStoredProcCommand("uspGetServiceRequest");
                db.AddInParameter(dbComm, "@pDate", DbType.DateTime, dDate);
                db.AddInParameter(dbComm, "@pUserID", DbType.String, sUser);
                db.AddInParameter(dbComm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(dbComm, "@pMaxRow", DbType.Int32, iMaxRow);
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, iLoad);
                db.AddInParameter(dbComm, "@pOrderby", DbType.String, sOrderBy);
                
                db.AddInParameter(dbComm, "@pViewFilter", DbType.Int16, iViewFilter);
                db.AddInParameter(dbComm, "@pViewActive", DbType.Int16, iViewActive);
                db.AddInParameter(dbComm, "@pViewBooked", DbType.Int16, iViewBooked);

                //db.AddInParameter(dbComm, "@pViewAllDate", DbType.Boolean, isViewAllDate);
                db.AddInParameter(dbComm, "@pViewFilterType", DbType.Int16, iFilterType);
                db.AddInParameter(dbComm, "@pSeafarerIDInt", DbType.Int64, iEmployeeID);
                db.AddInParameter(dbComm, "@pCrewAssistUser", DbType.String, sCrewAssistUser);


                dbComm.CommandTimeout = 120;
                ds = db.ExecuteDataSet(dbComm);
                iCount = GlobalCode.Field2Int( ds.Tables[0].Rows[0][0]);
                dt = ds.Tables[1];

                list = (from a in dt.AsEnumerable()
                        select new ServiceRequestList
                        {

                            HotelRequestIDBigint = GlobalCode.Field2Int(a["HotelRequestIDBigint"]),
                            VehicleRequestIDBigint = GlobalCode.Field2Int(a["VehicleRequestIDBigint"]),
                            MeetGreetRequestIDBigint = GlobalCode.Field2Int(a["MeetGreetRequestIDBigint"]),
                            PortAgentRequestIDBigint = GlobalCode.Field2Int(a["PortAgentRequestIDBigint"]),
                            
                            IDBigInt = GlobalCode.Field2Int(a["colIDBigInt"]),
                            TravelReqIDInt = GlobalCode.Field2Int(a["colTravelReqIDInt"]),
                            //RequestIDInt = 0,


                            SeafarerIDInt = GlobalCode.Field2Int(a["colSeafarerIDInt"]),
                            SFStatus = a.Field<string>("colSFStatus"),
                            SignOnOffDate = a.Field<DateTime?>("SignOnOffDate"),

                            VesselID = GlobalCode.Field2Int(a["colVesselIdInt"]),
                            VesselName = a.Field<string>("Vessel"),

                            LastName = a.Field<string>("LastName"),
                            FirstName = a.Field<string>("FirstName"),

                            IsWithHotelRequest = GlobalCode.Field2Bool(a["IsWithHotelRequest"]),
                            IsWithVehicleRequest = GlobalCode.Field2Bool(a["IsWithVehicleRequest"]),
                            IsWithMeetGreetRequest = GlobalCode.Field2Bool(a["IsWithMeetGreetRequest"]),
                            IsWithPortAgentRequest = GlobalCode.Field2Bool(a["IsWithPortAgentRequest"]),
                            
                            IsHotelRequestActive = GlobalCode.Field2Bool(a["IsHotelRequestActive"]),
                            IsVehicleRequestActive = GlobalCode.Field2Bool(a["IsVehicleRequestActive"]),
                            IsMeetGreetRequestActive = GlobalCode.Field2Bool(a["IsMeetGreetRequestActive"]),
                            IsPortAgentRequestActive = GlobalCode.Field2Bool(a["IsPortAgentRequestActive"]),

                            IsHotelRequestBook = GlobalCode.Field2Bool(a["IsHotelRequestBook"]),
                            IsVehicleRequestBook = GlobalCode.Field2Bool(a["IsVehicleRequestBook"]),
                            IsMeetGreetRequestBook = GlobalCode.Field2Bool(a["IsMeetGreetRequestBook"]),
                            IsPortAgentRequestBook = GlobalCode.Field2Bool(a["IsPortAgentRequestBook"]),

                            IsEmailVisible = GlobalCode.Field2Bool(a["IsEmailVisible"]),
                            AirSeqNo = GlobalCode.Field2Int(a["colSeqNoInt"]),
                            RankName = GlobalCode.Field2String(a["RankName"]),

                            HotelRequestCreatedBy = a.Field<string>("HotelRequestCreatedBy"),
                            VehicleRequestCreatedBy = a.Field<string>("VehicleRequestCreatedBy"),
                            PortAgentRequestCreatedBy = a.Field<string>("PortAgentRequestCreatedBy"),

                            HotelRequestCreatedDate = a.Field<DateTime?>("HotelRequestCreatedDate"),
                            VehicleRequestCreatedDate = a.Field<DateTime?>("VehicleRequestCreatedDate"),
                            PortAgentRequestCreatedDate = a.Field<DateTime?>("PortAgentRequestCreatedDate"),
                            
                        }).ToList();

                HttpContext.Current.Session["ServiceRequestView_Count"] = iCount;
                HttpContext.Current.Session["ServiceRequestView_ServiceReqList"] = list;

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
        /// Author:         Josephine Gad
        /// Date Created:   25/Oct/2013
        /// Descrption:     Cancel or Activate Service Request
        /// =============================================================
        /// </summary>
        public void CancelActivateServiceRequest(string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtServiceRequest)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
                dbCommand = db.GetStoredProcCommand("uspCancelServiceRequest");

                db.AddInParameter(dbCommand, "@pUserId", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pDescription", DbType.String, sDescription);
                db.AddInParameter(dbCommand, "@pFunction", DbType.String, sFunction);
                db.AddInParameter(dbCommand, "@pFilename", DbType.String, sFilename);
                db.AddInParameter(dbCommand, "@pTimezone", DbType.String, strTimeZone);

                db.AddInParameter(dbCommand, "@pGMTDATE", DbType.Date, GlobalCode.Field2Date(sGMTDate));
                db.AddInParameter(dbCommand, "@pCreateDate", DbType.DateTime, DateTime.Now);

                SqlParameter param = new SqlParameter("@pTblTempServiceRequest", dtServiceRequest);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                dbCommand.Parameters.Add(param);

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
                if (dtServiceRequest != null)
                {
                    dtServiceRequest.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:  22/Oct/2013
        /// Created By:    Josephine Gad
        /// (description)  GetServiceRequestEmail
        /// ---------------------------------------
        /// Date Modified: 25/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add Service Provider Email
        /// </summary>
        public void GetServiceRequestEmail(Int32 iHotelRequestID, Int32 iVehicleRequestID,  
            int iPortAgentRequestID,  Int32 iMeetGreetRequestID, Int16 iLoadType)
        {
            List<ServiceRequestEmailList> list = new List<ServiceRequestEmailList>();
            List<CrewAssistEmailDetail> hotelEmail = new List<CrewAssistEmailDetail>();
            List<CrewAssistTranspo> vehicleEmail = new List<CrewAssistTranspo>();
            List<CrewAssistMeetAndGreet> meetGreetEmail = new List<CrewAssistMeetAndGreet>();
            List<CopyEmail> copyMail = new List<CopyEmail>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataSet ds = null;
            DataTable dt = null;
            DataTable dtCopy = null;
            DataTable dtHotel = null;
            DataTable dtVehicle = null;
            DataTable dtMeetGreet = null;
            DataTable dtAirDetail = null;

            if (iLoadType == 0)
            {
                HttpContext.Current.Session["ServiceRequestEmail_EmailList"] = list;
                HttpContext.Current.Session["ServiceRequestEmail_CopyMailList"] = copyMail;
            }
            HttpContext.Current.Session["ServiceRequestEmail_HotelEmailDetails"] = hotelEmail;
            HttpContext.Current.Session["ServiceRequestEmail_VehicleEmailDetails"] = vehicleEmail;
            HttpContext.Current.Session["ServiceRequestEmail_MeetGreetEmailDetails"] = meetGreetEmail;

            try
            {
                dbComm = db.GetStoredProcCommand("uspGetServiceRequestEmail");
                db.AddInParameter(dbComm, "@pHotelRequestID", DbType.Int32, iHotelRequestID);
                db.AddInParameter(dbComm, "@pVehicleRequestID", DbType.Int32, iVehicleRequestID);
                db.AddInParameter(dbComm, "@pMeetGreetRequestID", DbType.Int32, iMeetGreetRequestID);
                db.AddInParameter(dbComm, "@pPortAgentRequestID", DbType.Int32, iPortAgentRequestID);
                db.AddInParameter(dbComm, "@pLoadType", DbType.Int16, iLoadType);
                ds = db.ExecuteDataSet(dbComm);

                if (iLoadType == 0)
                {
                    dt = ds.Tables[0];
                    list = (from a in dt.AsEnumerable()
                            select new ServiceRequestEmailList
                            {
                                HotelID = GlobalCode.Field2Int(a["HotelID"]),
                                HotelEmailTo = a.Field<string>("HotelEmailTo"),
                                HotelName = a.Field<string>("HotelName"),

                                VehicleID = GlobalCode.Field2Int(a["VehicleID"]),
                                VehicleEmailTo = a.Field<string>("VehicleEmailTo"),
                                VehicleName = a.Field<string>("VehicleName"),

                                MeetGreetID = GlobalCode.Field2Int(a["MeetGreetID"]),
                                MeetGreetEmailTo = a.Field<string>("MeetGreetEmailTo"),
                                MeetGreetName = a.Field<string>("MeetGreetName"),

                                PortAgentID = GlobalCode.Field2Int(a["MeetGreetID"]),
                                PortAgentName = a.Field<string>("PortAgentName"),
                                PortAgentEmailTo = a.Field<string>("PortAgentEmailTo"),


                                VesselID = GlobalCode.Field2Int(a["VesselID"]),
                                VesselEmailTo = a.Field<string>("VesselEmail")

                            }).ToList();

                    dtCopy = ds.Tables[1];
                    dtHotel = ds.Tables[2];
                    dtVehicle = ds.Tables[3];
                    dtMeetGreet = ds.Tables[4];
                    dtAirDetail = ds.Tables[5];

                    copyMail = (from a in dtCopy.AsEnumerable()
                                select new CopyEmail
                                {
                                    EmailName = GlobalCode.Field2String(a["EmailType"]),
                                    EmailType = GlobalCode.Field2Int(a["EmailID"]),
                                    Email = GlobalCode.Field2String(a["Email"]),
                                }).ToList();

                    HttpContext.Current.Session["ServiceRequestEmail_EmailList"] = list;
                    HttpContext.Current.Session["ServiceRequestEmail_CopyMailList"] = copyMail;
                


                }
                else if (iLoadType == 1)
                {
                    dtHotel = ds.Tables[0];
                    dtVehicle = ds.Tables[1];
                    dtMeetGreet = ds.Tables[2];
                    dtAirDetail = ds.Tables[3];
                }
                //----------------------------------Get the request details to be emailed----------------------------------
                if (dtHotel != null)
                {
                    hotelEmail = (from a in dtHotel.AsEnumerable()
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
                                      TimeSpanStartDate = GlobalCode.Field2Date(a["TimeSpanStartDate"]),
                                      TimeSpanEndDate = GlobalCode.Field2Date(a["TimeSpanStartDate"]),
                                      TimeSpanStartTime = GlobalCode.Field2String(a["TimeSpanStartTime"]),
                                      TimeSpanEndTime = GlobalCode.Field2String(a["TimeSpanEndTime"]),
                                      Mealvoucheramount = GlobalCode.Field2Double(a["Mealvoucheramount"]).ToString("n2"),
                                      //Confirmedbyhotelvendor = GlobalCode.Field2Double(a["Confirmedbyhotelvendor"]).ToString(),
                                      ConfirmedbyRCCL = GlobalCode.Field2String(a["ConfirmedbyRCCL"]),
                                      VendorBranch = GlobalCode.Field2String(a["colVendorBranchNameVarchar"]),
                                      Roomrate = GlobalCode.Field2Double(a["Roomrate"]).ToString("n2"),// GlobalCode.Field2String(a["Roomrate"])
                                      Comment = GlobalCode.Field2String(a["colCommentsVarchar"]),
                                      Confirmedbyhotelvendor = GlobalCode.Field2String(a["colConfirmName"]),
                                      CrewAssistEmailAirDetail = (from n in dtAirDetail.AsEnumerable()
                                                                  select new CrewAssistEmailAirDetail
                                                                  {
                                                                      AirDetail = GlobalCode.Field2String(n["AirDetail"])
                                                                  }).ToList()

                                  }).ToList();

                    HttpContext.Current.Session["ServiceRequestEmail_HotelEmailDetails"] = hotelEmail;
                }
                if (dtVehicle != null)
                {
                    vehicleEmail = (from a in dtVehicle.AsEnumerable()
                                    select new CrewAssistTranspo
                                    {
                                        VehicleTransID = GlobalCode.Field2Int(a["colTransVehicleIDBigint"]),
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

                                        //TransSender = UserID,

                                        SeaparerID = GlobalCode.Field2Long(a["colSeafarerIdInt"]),
                                        FirstName = GlobalCode.Field2String(a["colFirstNameVarchar"]),
                                        LastName = GlobalCode.Field2String(a["colLastNameVarchar"]),
                                        RankName = GlobalCode.Field2String(a["RankName"]),
                                        Gender = GlobalCode.Field2String(a["colGenderDiscription"]),
                                        NationalityName = GlobalCode.Field2String(a["Nationality"]),

                                    }).ToList();
                    HttpContext.Current.Session["ServiceRequestEmail_VehicleEmailDetails"] = vehicleEmail;
                }
                if (dtMeetGreet != null)
                {
                    meetGreetEmail = (from Q in dtMeetGreet.AsEnumerable()
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
                                          FligthNo = GlobalCode.Field2String(Q["colFligthNoVarchar"]),
                                          ServiceDate = GlobalCode.Field2DateTime(Q["colServiceDatetime"]),
                                          Rate = GlobalCode.Field2Double(Q["colRateFloat"]),
                                          SFStatus = GlobalCode.Field2String(Q["colSFStatus"]),
                                          Comment = GlobalCode.Field2String(Q["colCommentVarchar"]),
                                          IsAir = GlobalCode.Field2Bool(Q["colIsAirBit"]),
                                          Email = GlobalCode.Field2String(Q["colEmailToVarchar"])
                                      }).ToList();
                    HttpContext.Current.Session["ServiceRequestEmail_MeetGreetEmailDetails"] = meetGreetEmail;
                }
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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtCopy != null)
                {
                    dtCopy.Dispose();
                }
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtMeetGreet != null)
                {
                    dtMeetGreet.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtAirDetail != null)
                {
                    dtAirDetail.Dispose();
                }
            }
        }
         /// <summary>
        /// Date Modified: 30/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get Service Request List for Export Use
        /// </summary>
        public DataTable GetServiceRequestExport(string sUser)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbComm = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetServiceRequestExport");
                db.AddInParameter(dbComm, "@pUserID", DbType.String, sUser);
                ds = db.ExecuteDataSet(dbComm);
                dt = ds.Tables[0];
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Modified: 05/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get Service Request List for Export Use
        /// </summary>
        /// <returns></returns>
        public List<CrewAssistUsers> GetCrewAssist()
        {
            Database db = DatabaseFactory.CreateDatabase("APPSERVICESConnectionString");
            DbCommand dbComm = null;
            DataSet ds = null;
            DataTable dt = null;
            List<CrewAssistUsers> list = new List<CrewAssistUsers>();
            try
            {
                dbComm = db.GetStoredProcCommand("uspGetCrewAssist");               
                ds = db.ExecuteDataSet(dbComm);
                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new CrewAssistUsers {
                            UserID = GlobalCode.Field2String(a["UserID"]),
                            UserName = GlobalCode.Field2String(a["UserName"]),
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

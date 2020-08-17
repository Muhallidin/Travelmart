using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Globalization;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class ReportDAL
    {
        /// <summary>
        /// Date Created:   25/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks
        /// --------------------------------------- 
        /// </summary>       
        public static List<CrewAssistRemarksList> GetCrewAssistRemarks(Int32 iYear,
            Int32 iMonth, string sCreatedBy, string sUserID, Int16 iLoadType,
            Int16 iFilterBy, string sFilterValue,
            string sOrderBy, short IR, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataSet ds = null;
            DataTable dt = null;
            int iRow = 0;

            List<CrewAssistRemarksList> list = new List<CrewAssistRemarksList>();
            try
            {
                comm = db.GetStoredProcCommand("uspGetCrewAssistRemarks");
                db.AddInParameter(comm, "@pYear", DbType.Int32, iYear);
                db.AddInParameter(comm, "@pMonth", DbType.Int32, iMonth);

                db.AddInParameter(comm, "@pCreatedBy", DbType.String, sCreatedBy);
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);

                db.AddInParameter(comm, "@pFilterBy", DbType.Int16, iFilterBy);
                db.AddInParameter(comm, "@pFilterValue", DbType.String, sFilterValue);

                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);
                db.AddInParameter(comm, "@pIncidentReport", DbType.Int16, IR);

                ds = db.ExecuteDataSet(comm);
                if (ds.Tables.Count > 0)
                {
                    iRow = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0]);
                    dt = ds.Tables[1];

                    list = (from a in dt.AsEnumerable()
                            select new CrewAssistRemarksList
                            {
                                TravelRequestID = GlobalCode.Field2Long(a["colTravelReqIdInt"]),
                                SeafarerID = GlobalCode.Field2Long(a["colSeafarerIDBigint"]),
                                Source = a.Field<string>("RequestSource"),
                                RequestHeader = a.Field<string>("RemarksTypeHeader"),
                                RequestType = a.Field<string>("RemarksType"),
                                Summary = a.Field<string>("Summary"),
                                Remarks = a.Field<string>("Remarks"),
                                RemarksStatus = a.Field<string>("RemarksStatus"),
                                Requestor = a.Field<string>("Requestor"),
                                CreatedDate = GlobalCode.Field2DateTime(a["colDateCreatedDateTime"]),
                                CreatedBy = a.Field<string>("colCreatedByVarchar"),
                                TransactionDate = a.Field<DateTime?>("colTransactionDate"),
                                TransactionTime = a.Field<TimeSpan?>("colTransactionTime"),
                                IR = GlobalCode.Field2Bool(a["IRBit"])
                            }).ToList();

                    HttpContext.Current.Session["CrewAssistRemarks_Count"] = iRow;

                }
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   26/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew User List
        /// --------------------------------------- 
        /// </summary>       
        public static List<UserList> GetUserList(string sUsername, bool bIsForCrewAssistRemarks)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataSet ds = null;
            DataTable dt = null;

            List<UserList> list = new List<UserList>();
            try
            {
                comm = db.GetStoredProcCommand("uspGetUser");
                db.AddInParameter(comm, "@pUsername", DbType.String, sUsername);
                db.AddInParameter(comm, "@pIsForCrewAssistRemarks", DbType.Boolean, bIsForCrewAssistRemarks);


                ds = db.ExecuteDataSet(comm);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];

                    list = (from a in dt.AsEnumerable()
                            select new UserList
                            {
                                sUserName = a.Field<string>("colUsernameVarchar"),
                            }).ToList();
                }
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   26June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks to Export
        /// --------------------------------------- 
        /// </summary>       
        public DataTable GetCrewAssistRemarksToExport(string sUserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;

            try
            {

                comm = db.GetStoredProcCommand("uspGetCrewAssistRemarksExport");
                
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   29/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks By Date
        /// --------------------------------------- 
        /// </summary>       
        public List<RemarksByDateList> GetCrewAssistRemarksByDate(
            Int16 iRequestSourceID, bool bIsByDateRange,
            Int32 iYear, Int32 iMonth, DateTime dDateForm, DateTime dDateTo,
            
            string sUserID, Int16 iLoadType,
            string sOrderBy, int iStartRow, int iMaxRow)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataSet ds = null;
            DataTable dt = null;
            DataTable dtSource = null;

            //int iRow = 0;

            List<RemarksByDateList> list = new List<RemarksByDateList>();
            try
            {
                comm = db.GetStoredProcCommand("uspGetCrewAssistRemarksReportByDate");
                db.AddInParameter(comm, "@pRequestSourceIDint", DbType.Int16, iRequestSourceID);
                db.AddInParameter(comm, "@pIsByDateRange", DbType.Boolean, bIsByDateRange);

                db.AddInParameter(comm, "@pYear", DbType.Int32, iYear);
                db.AddInParameter(comm, "@pMonth", DbType.Int32, iMonth);

                db.AddInParameter(comm, "@pDateFrom", DbType.DateTime, dDateForm);
                db.AddInParameter(comm, "@pDateTo", DbType.DateTime, dDateTo);
                
                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                db.AddInParameter(comm, "@pLoadType", DbType.Int16, iLoadType);

                db.AddInParameter(comm, "@pOrderBy", DbType.String, sOrderBy);
                db.AddInParameter(comm, "@pStartRow", DbType.Int32, iStartRow);
                db.AddInParameter(comm, "@pMaxRow", DbType.Int32, iMaxRow);

                ds = db.ExecuteDataSet(comm);
                if (ds.Tables.Count > 0)
                {
                    //iRow = GlobalCode.Field2Int(ds.Tables[1].Rows[0][0]);
                    dt = ds.Tables[0];


                    list = (from a in dt.AsEnumerable()
                            select new RemarksByDateList
                            {
                                RequestType = GlobalCode.Field2String(a["colRemarksTypeVarchar"]),
                                iCount = GlobalCode.Field2Int(a["xCount"]),
                            }).ToList();
                    //HttpContext.Current.Session["CrewAssistRemarksReportByDate_Total"] = iRow;

                    if (iLoadType == 0)
                    {
                        dtSource = ds.Tables[1];
                        List<RemarksSourceList> listSource = new List<RemarksSourceList>();
                        listSource = (from a in dtSource.AsEnumerable()
                                      select new RemarksSourceList {
                                          RequestSource =  GlobalCode.Field2String(a["RequestSource"]),
                                          RequestSourceIDint = GlobalCode.Field2Int(a["RequestSourceIDint"])
                                      }).ToList();

                        HttpContext.Current.Session["CrewAssistRemarksReportByDate_SourceList"] = listSource;
                    }
                    HttpContext.Current.Session["CrewAssistRemarksReportByDate_RemarksList"] = list;
                }
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
                if (dt != null)
                {
                    dt.Dispose();
                } 
                if (dtSource != null)
                {
                    dtSource.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   29/June/2015
        /// Created By:     Josephine Monteza
        /// (description)   Get Crew Assist Remarks to Export
        /// --------------------------------------- 
        /// </summary>       
        public DataTable GetCrewAssistRemarksByDateExport(string sUserID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand comm = null;
            DataTable dt = null;

            try
            {
                comm = db.GetStoredProcCommand("uspGetCrewAssistRemarksReportByDate");

                db.AddInParameter(comm, "@pUserID", DbType.String, sUserID);
                dt = db.ExecuteDataSet(comm).Tables[0];
                return dt;
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
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}

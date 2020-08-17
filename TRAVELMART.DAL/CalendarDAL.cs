using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class CalendarDAL
    {
        /// <summary>
        /// Date Modified:  19/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:  15/09/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter RegionID, PortID, sPage
        /// -------------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public List <ManifestOnOffCalendar> LoadOnOffCalendar(string UserId, DateTime Date,
            Int32 RegionID, Int32 PortID, Int32 VesselID, Int32 HotelID, string sPage, Int16 TypeView)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable Calendar = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetOnOffCalendar");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Date);
                
                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortID);

                db.AddInParameter(dbCommand, "@pVesselID", DbType.Int32, VesselID);
                db.AddInParameter(dbCommand, "@pHotelID", DbType.Int32, HotelID);
                
                db.AddInParameter(dbCommand, "@pPage", DbType.String, sPage);

                db.AddInParameter(dbCommand, "@pTypeView", DbType.Int16, TypeView);

                Calendar = db.ExecuteDataSet(dbCommand).Tables[0];
                List<ManifestOnOffCalendar> list = new List<ManifestOnOffCalendar>();
                list = (from a in Calendar.AsEnumerable()
                        select new ManifestOnOffCalendar
                        {
                            colDate = GlobalCode.Field2DateTime(a["colDate"]),
                            ONCount = GlobalCode.Field2Int(a["ONCount"]),
                            OffCount = GlobalCode.Field2Int(a["OffCount"]),
                            sDate = GlobalCode.Field2String(a["sDate"])
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

                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   19/Dec/2012
        /// Created By:     Josephine Gad
        /// (description)   Get of ON/OFF count of calendar
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<ManifestOnOffCalendar> LoadOnOffCalendarExport(string UserId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable Calendar = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetOnOffCalendarExport");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);

                Calendar = db.ExecuteDataSet(dbCommand).Tables[0];
                List<ManifestOnOffCalendar> list = new List<ManifestOnOffCalendar>();
                list = (from a in Calendar.AsEnumerable()
                        select new ManifestOnOffCalendar
                        {
                            colDate = GlobalCode.Field2DateTime(a["colDate"]),
                            ONCount = GlobalCode.Field2Int(a["ONCount"]),
                            OffCount = GlobalCode.Field2Int(a["OffCount"]),
                            sDate = GlobalCode.Field2String(a["sDate"]),

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
                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   07/11/2012
        /// Created By:     Josephine Gad
        /// (description)   get list of calendar for room needed per day
        /// ---------------------------------------------------------------
        /// Date Modified:  04/Feb/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add Contract Room Count, Override, Total and Emergency room counts
        /// ---------------------------------------------------------------
        /// 
        ///  </summary>
        public List<CalendarRoomNeeded> GetCalendarRoomNeeded(string UserId, DateTime Date, string sDateTo,
            Int32 RegionID, Int32 PortID, Int32 BranchID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable Calendar = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCalendarRoomCount");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Date);

                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, BranchID);

                if (sDateTo != "")
                {
                    db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, GlobalCode.Field2Date(sDateTo));
                }

                Calendar = db.ExecuteDataSet(dbCommand).Tables[0];
                List<CalendarRoomNeeded> list = new List<CalendarRoomNeeded>();
                list = (from a in Calendar.AsEnumerable()
                        select new CalendarRoomNeeded
                        {
                            colDate = GlobalCode.Field2DateTime(a["colDate"]),
                            sDate = GlobalCode.Field2String(a["sDate"]),
                            SingleCount = GlobalCode.Field2Int(a["SingleCount"]),
                            DoubleCount = GlobalCode.Field2Int(a["DoubleCount"]),
                            TotalNeededRoom = GlobalCode.Field2Int(a["TotalNeededRoom"]),
                            
                            //ContractRoomSingle = GlobalCode.Field2Int(a["ContractRoomSingle"]),
                            //ContractRoomDouble = GlobalCode.Field2Int(a["ContractRoomDouble"]),
                            TotalContractRoom = GlobalCode.Field2Int(a["TotalContractRoom"]),

                            //OverrideRoomSingle = GlobalCode.Field2Int(a["OverrideRoomSingle"]),
                            //OverrideRoomDouble = GlobalCode.Field2Int(a["OverrideRoomDouble"]),
                            TotalOverrideRoom = GlobalCode.Field2Int(a["TotalOverrideRoom"]),
                           
                            //TotalRoomsSingle  = GlobalCode.Field2Int(a["TotalRoomsSingle"]),
                            //TotalRoomDouble = GlobalCode.Field2Int(a["TotalRoomDouble"]),
                            TotalRoom = GlobalCode.Field2Int(a["TotalRoom"]),

                            //EmergencyRoomSingle = GlobalCode.Field2Int(a["EmergencyRoomSingle"]),
                            //EmergencyRoomDouble = GlobalCode.Field2Int(a["EmergencyRoomDouble"]),
                            //TotalEmergencyRoom = GlobalCode.Field2Int(a["TotalEmergencyRoom"]),

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

                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   14/Jan/2015
        /// Created By:     Josephine Monteza
        /// (description)   get list of calendar for room needed per day, hotel as column
        /// ---------------------------------------------------------------
        ///  </summary>
        public DataTable GetCalendarRoomNeeded_Forecast(string UserId, DateTime Date, string sDateTo,
            Int32 RegionID, Int32 PortID, Int32 BranchID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable Calendar = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSelectCalendarRoomCount_Forecast");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Date);

                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, RegionID);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, BranchID);

                if (sDateTo != "")
                {
                    db.AddInParameter(dbCommand, "@pDateTo", DbType.DateTime, GlobalCode.Field2Date(sDateTo));
                }

                Calendar = db.ExecuteDataSet(dbCommand).Tables[0];

                return Calendar;
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

                if (Calendar != null)
                {
                    Calendar.Dispose();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class VesselDAL
    {
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of Vessel from Sail master based from user, date, region, etc.
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortId"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public static DataTable GetVessel(string Username, string FromDate, string ToDate,
            string RegionID, string CountryID, string CityID, string PortId, string Role)
        {                  
            Database VesselDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand VesselDbCommand = null;
            DataTable VesselDataTable = null;
            try
            {
                VesselDbCommand = VesselDatebase.GetStoredProcCommand("uspGetVessel");
                VesselDatebase.AddInParameter(VesselDbCommand, "@pUserName", DbType.String, Username);
                VesselDatebase.AddInParameter(VesselDbCommand, "@pFromDate", DbType.Date, FromDate);
                VesselDatebase.AddInParameter(VesselDbCommand, "@pToDate", DbType.Date, FromDate);

                VesselDatebase.AddInParameter(VesselDbCommand, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(RegionID));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pCountryIDInt", DbType.Int32, GlobalCode.Field2Int(CountryID));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pCityIDInt", DbType.Int32, GlobalCode.Field2Int(CityID));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pPortIdInt", DbType.Int32, GlobalCode.Field2Int(PortId));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pRole", DbType.String, Role);

                VesselDataTable = VesselDatebase.ExecuteDataSet(VesselDbCommand).Tables[0];
                return VesselDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VesselDbCommand != null)
                {
                    VesselDbCommand.Dispose();
                }
                if (VesselDataTable != null)
                {
                    VesselDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   16/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of Vessel using List and not DataTable
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortId"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public static List<VesselDTO> GetVesselList(string Username, string FromDate, string ToDate,
            string RegionID, string CountryID, string CityID, string PortId, string Role, bool ForCalendar)
        {
            Database VesselDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand VesselDbCommand = null;
            DataSet ds = null;
            List<VesselDTO> vesselList = null;
            try
            {
                VesselDbCommand = VesselDatebase.GetStoredProcCommand("uspGetVessel");
                VesselDatebase.AddInParameter(VesselDbCommand, "@pUserName", DbType.String, Username);
                VesselDatebase.AddInParameter(VesselDbCommand, "@pFromDate", DbType.Date, FromDate);
                VesselDatebase.AddInParameter(VesselDbCommand, "@pToDate", DbType.Date, FromDate);

                VesselDatebase.AddInParameter(VesselDbCommand, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(RegionID));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pCountryIDInt", DbType.Int32, GlobalCode.Field2Int(CountryID));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pCityIDInt", DbType.Int32, GlobalCode.Field2Int(CityID));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pPortIdInt", DbType.Int32, GlobalCode.Field2Int(PortId));
                VesselDatebase.AddInParameter(VesselDbCommand, "@pRole", DbType.String, Role);
                VesselDatebase.AddInParameter(VesselDbCommand, "@pForCalendar", DbType.Boolean, ForCalendar);


                ds = VesselDatebase.ExecuteDataSet(VesselDbCommand);

                vesselList = (from a in ds.Tables[0].AsEnumerable()
                              select new VesselDTO
                              {
                                  VesselIDString = a["VesselID"].ToString(),
                                  VesselNameString = a["VesselName"].ToString()
                              }).ToList();
                return vesselList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VesselDbCommand != null)
                {
                    VesselDbCommand.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (vesselList != null)
                {
                    vesselList = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   05/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel details by Vessel ID
        /// -------------------------------------------------
        /// Date Modified:  24/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FromDate and ToDate
        /// ----------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="VesselID"></param>
        /// <returns></returns>
        public static IDataReader GetVesselPortDetails(string VesselID, string FromDate, string ToDate)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetVesselByVesselID");
                db.AddInParameter(command, "@pVesselIdInt", DbType.String, VesselID);
                db.AddInParameter(command, "@pFromDate", DbType.DateTime, DateTime.Parse(FromDate));
                db.AddInParameter(command, "@pToDate", DbType.DateTime, DateTime.Parse(ToDate));
                dr = db.ExecuteReader(command);
                return dr;
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
    }
}

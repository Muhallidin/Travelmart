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
    public class RequestDAL
    {
        #region "Select"
        
        /// <summary>
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of hotel/vehicle request
        /// ---------------------------------------------
        /// Date Modified:  16/04/2012
        /// Created By:     Charlene Remotigue
        /// (description)   use globalcode instead of .parse
        /// ---------------------------------------------
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="UserID"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="Status"></param>
        /// <param name="FilterByNameID"></param>
        /// <param name="FilterNameID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <param name="VehicleID"></param>
        /// <param name="VesselID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="Approved"></param>
        /// <returns></returns>
        public static DataTable GetRequest(string DateFrom, string DateTo,
                   string UserID, string RegionID, string CountryID, string CityID,
                   string Status, string FilterByNameID, string FilterNameID, string PortID,
                   string HotelID, string VehicleID, string VesselID, string Nationality,
                   string Gender, string Rank, string Approved, string Role)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectHotelVehicleRequest");

                if (DateFrom != "")
                {
                    db.AddInParameter(command, "@pDateFrom", DbType.DateTime, GlobalCode.Field2DateTime(DateFrom));
                    db.AddInParameter(command, "@pDateTo", DbType.DateTime, GlobalCode.Field2DateTime(DateTo));
                }
                db.AddInParameter(command, "@pUserIDVarchar", DbType.String, UserID);
                db.AddInParameter(command, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(RegionID));
                db.AddInParameter(command, "@pCountryIDInt", DbType.Int32, GlobalCode.Field2Int(CountryID));
                db.AddInParameter(command, "@pCityIDInt", DbType.Int32, GlobalCode.Field2Int(CityID));

                db.AddInParameter(command, "@pStatus", DbType.String, Status);
                db.AddInParameter(command, "@pFilterByNameID", DbType.Int32, GlobalCode.Field2Int(FilterByNameID));
                db.AddInParameter(command, "@pFilterNameID", DbType.String, FilterNameID);

                db.AddInParameter(command, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(PortID));
                db.AddInParameter(command, "@pHotelIDInt", DbType.Int32, GlobalCode.Field2Int(HotelID));

                if (VehicleID == "")
                {
                    VehicleID = "0";
                }

                db.AddInParameter(command, "@pVehicleIDInt", DbType.Int32, GlobalCode.Field2Int(VehicleID));
                db.AddInParameter(command, "@pVesselIDInt", DbType.Int32, GlobalCode.Field2Int(VesselID));
                db.AddInParameter(command, "@pNationality", DbType.Int32, GlobalCode.Field2Int(Nationality));
                db.AddInParameter(command, "@pGender", DbType.Int32, GlobalCode.Field2Int(Gender));
                db.AddInParameter(command, "@pRank", DbType.Int32, GlobalCode.Field2Int(Rank));
                db.AddInParameter(command, "@pRole", DbType.String, Role);
                if (Approved != "")
                {
                    bool IsApproved = false;
                    if (Approved == "1")
                    {
                        IsApproved = true;
                    }
                    db.AddInParameter(command, "@pApproved", DbType.Boolean, IsApproved);
                }

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
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel/vehicle request details by Request ID
        /// --------------------------------------------- 
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public static IDataReader GetRequestDetailsByID(string RequestID)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectHotelVehicleRequestDetailsByID");
                db.AddInParameter(command, "@pRequestID", DbType.Int64, Int64.Parse(RequestID));

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
        /// <summary>
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   validate if there is pending manual request
        /// --------------------------------------------- 
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="UserRole"></param>
        /// <returns></returns>
        public static bool IsPendingRequestExists(string Username, string UserRole)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try 
            {
                bool IsExists = false;

                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectHotelVehicleRequestPending");
                db.AddInParameter(command, "@pUserIDVarchar", DbType.String, Username);
                db.AddInParameter(command, "@pUserRole", DbType.String, UserRole);

                dr = db.ExecuteReader(command);
                if (dr.Read())
                {
                    if (dr["colRequestIDInt"].ToString() != "0")
                    {
                        IsExists = true;
                    }
                }
                return IsExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        #endregion

        #region "Insert/Update/Delete"
        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert hotel/vehicle request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="SeafarerId"></param>
        /// <param name="VesselId"></param>
        /// <param name="OnOffDate"></param>
        /// <param name="Status"></param>
        /// <param name="IsNeedHotel"></param>
        /// <param name="BranchID"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="IsNeedTransportation"></param>
        /// <param name="Origin"></param>
        /// <param name="OriginRemarks"></param>
        /// <param name="Destination"></param>
        /// <param name="DestinationRemarks"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PortId"></param>
        /// <param name="CityID"></param>
        /// <param name="PortAgentId"></param>
        /// <param name="CostCenterID"></param>
        /// <param name="RankID"></param>
        /// <param name="User"></param>
        public static Int32 RequestInsert(string RequestID, string SeafarerId,
        string VesselId, string OnOffDate, string Status, bool IsNeedHotel,
        string BranchID, string CheckInDate, string CheckOutDate, bool IsNeedTransportation,
        string Origin, string OriginRemarks, string Destination, string DestinationRemarks,
        string PickupDate, string PortId, string CityID, string PortAgentId, 
        string CostCenterID, string RankID, string User)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertHotelVehicleRequest");

                db.AddInParameter(dbCommand, "@pRequestIDInt", DbType.Int64, Int64.Parse(RequestID));
                db.AddInParameter(dbCommand, "@pSeafarerIdInt", DbType.Int32, Int32.Parse(SeafarerId));
                db.AddInParameter(dbCommand, "@pVesselIdInt", DbType.Int32, Int32.Parse(VesselId));
                db.AddInParameter(dbCommand, "@pOnOffDate", DbType.Date, DateTime.Parse(OnOffDate));
                db.AddInParameter(dbCommand, "@pStatusVarchar", DbType.String, Status);
                db.AddInParameter(dbCommand, "@pIsNeedHotelBit", DbType.Boolean, IsNeedHotel);

                if (IsNeedHotel)
                {
                    db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, Int32.Parse(BranchID));
                    db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, DateTime.Parse(CheckInDate));
                    db.AddInParameter(dbCommand, "@pCheckOutDate", DbType.Date, DateTime.Parse(CheckOutDate));
                }

                db.AddInParameter(dbCommand, "@pIsNeedTransportationBit", DbType.Boolean, IsNeedTransportation);
                if (IsNeedTransportation)
                {
                    db.AddInParameter(dbCommand, "@pOriginVarchar", DbType.String, Origin);
                    db.AddInParameter(dbCommand, "@pOriginRemarksVarchar", DbType.String, OriginRemarks);

                    db.AddInParameter(dbCommand, "@pDestinationVarchar", DbType.String, Destination);
                    db.AddInParameter(dbCommand, "@pDestinationRemarksVarchar", DbType.String, DestinationRemarks);

                    db.AddInParameter(dbCommand, "@pPickupDate", DbType.Date, DateTime.Parse(PickupDate));
                }

                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                if (CityID != "")
                {
                    db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(CityID));
                }
                db.AddInParameter(dbCommand, "@pPortAgentIdInt", DbType.Int32, Int32.Parse(PortAgentId));

                db.AddInParameter(dbCommand, "@pCostCenterIDInt", DbType.Int32, Int32.Parse(CostCenterID));
                db.AddInParameter(dbCommand, "@pRankIDInt", DbType.Int32, Int32.Parse(RankID));

                db.AddInParameter(dbCommand, "@pDateCreatedDatetime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, User);

                db.AddOutParameter(dbCommand, "@pRequestID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 pRequestID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pRequestID"));
                return pRequestID;
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
            }
        }

        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Update hotel/vehicle request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="SeafarerId"></param>
        /// <param name="VesselId"></param>
        /// <param name="OnOffDate"></param>
        /// <param name="Status"></param>
        /// <param name="IsNeedHotel"></param>
        /// <param name="BranchID"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="CheckOutDate"></param>
        /// <param name="IsNeedTransportation"></param>
        /// <param name="Origin"></param>
        /// <param name="OriginRemarks"></param>
        /// <param name="Destination"></param>       
        /// <param name="DestinationRemarks"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PortId"></param>
        /// <param name="CityID"></param>
        /// <param name="PortAgentId"></param>
        /// <param name="CostCenterID"></param>
        /// <param name="RankID"></param>
        /// <param name="User"></param>        
        public static void RequestUpdate(string RequestID, string SeafarerId,
        string VesselId, string OnOffDate, string Status, bool IsNeedHotel,
        string BranchID, string CheckInDate, string CheckOutDate, bool IsNeedTransportation,
        string Origin, string OriginRemarks, string Destination, string DestinationRemarks, string PickupDate,
        string PortId, string CityID, string PortAgentId, string CostCenterID, string RankID, string User)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspUpdateHotelVehicleRequest");

                db.AddInParameter(dbCommand, "@pRequestIDInt", DbType.Int64, Int64.Parse(RequestID));
                db.AddInParameter(dbCommand, "@pSeafarerIdInt", DbType.Int32, Int32.Parse(SeafarerId));
                db.AddInParameter(dbCommand, "@pVesselIdInt", DbType.Int32, Int32.Parse(VesselId));
                db.AddInParameter(dbCommand, "@pOnOffDate", DbType.Date, DateTime.Parse(OnOffDate));
                db.AddInParameter(dbCommand, "@pStatusVarchar", DbType.String, Status);
                db.AddInParameter(dbCommand, "@pIsNeedHotelBit", DbType.Boolean, IsNeedHotel);

                if (IsNeedHotel)
                {
                    db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int32, Int32.Parse(BranchID));
                    db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, DateTime.Parse(CheckInDate));
                    db.AddInParameter(dbCommand, "@pCheckOutDate", DbType.Date, DateTime.Parse(CheckOutDate));
                }

                db.AddInParameter(dbCommand, "@pIsNeedTransportationBit", DbType.Boolean, IsNeedTransportation);
                if (IsNeedTransportation)
                {
                    db.AddInParameter(dbCommand, "@pOriginVarchar", DbType.String, Origin);
                    db.AddInParameter(dbCommand, "@pOriginRemarksVarchar", DbType.String, OriginRemarks);

                    db.AddInParameter(dbCommand, "@pDestinationVarchar", DbType.String, Destination);
                    db.AddInParameter(dbCommand, "@pDestinationRemarksVarchar", DbType.String, DestinationRemarks);

                    db.AddInParameter(dbCommand, "@pPickupDate", DbType.Date, DateTime.Parse(PickupDate));
                }

                db.AddInParameter(dbCommand, "@pPortIdInt", DbType.Int32, Int32.Parse(PortId));
                if (CityID != "")
                {
                    db.AddInParameter(dbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(CityID));
                }
                db.AddInParameter(dbCommand, "@pPortAgentIdInt", DbType.Int32, Int32.Parse(PortAgentId));

                db.AddInParameter(dbCommand, "@pCostCenterIDInt", DbType.Int32, Int32.Parse(CostCenterID));
                db.AddInParameter(dbCommand, "@pRankIDInt", DbType.Int32, Int32.Parse(RankID));

                db.AddInParameter(dbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@pModifiedByVarchar", DbType.String, User);

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
            }
        }
        /// <summary>
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Approve or disapprove manual request
        /// ---------------------------------------------
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="IsApproved"></param>
        /// <param name="ApprovedBy"></param>
        public static void ApprovedRequest(string RequestID, bool IsApproved, string ApprovedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand command = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                DateTime DateNow = DateTime.Now;
                command = db.GetStoredProcCommand("uspUpdateHotelVehicleRequestApprove");

                db.AddInParameter(command, "@pRequestID", DbType.Int64, Int64.Parse(RequestID));
                db.AddInParameter(command, "@pApproved", DbType.Boolean, IsApproved);
                db.AddInParameter(command, "@pApprovedBy", DbType.String, ApprovedBy);
                db.AddInParameter(command, "@pDateApproved", DbType.DateTime, DateNow.ToString("MM/dd/yyyy hh:mm:ss"));

                db.ExecuteNonQuery(command, trans);
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
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        #endregion                           
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using TRAVELMART.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace TRAVELMART.DAL
{
    public class DashboardDAL
    {
        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description) crew dashboard       
        /// </summary>
        public static DataTable GetDashboardDetails(DateTime DateFrom, DateTime DateTo, Int32 PortID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectDashboardDetails");
                SFDatebase.AddInParameter(SFDbCommand, "pcolDateFrom", DbType.Date, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "pcolDateTo", DbType.Date, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "pcolPortID", DbType.Int32, PortID);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                //SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[1];


                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard     
        /// </summary>
        public static DataTable GetHotelDashboardDetails(Int32 BranchID, DateTime DateFrom, DateTime DateTo)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelDashboard");
                SFDatebase.AddInParameter(SFDbCommand, "pBranchID", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "pDateFrom", DbType.DateTime, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "pDateTo", DbType.DateTime, DateTo);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard weekly    
        /// </summary>
        public static DataTable GetHotelDashboardDetailsWeek(Int32 BranchID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectDashboardContract");
                SFDatebase.AddInParameter(SFDbCommand, "pBranchID", DbType.Int32, BranchID);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard main   
        /// </summary>
        public static DataTable GetHotelDashboardDetailsMain(Int32 BranchID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelDashboardMain");
                SFDatebase.AddInParameter(SFDbCommand, "pBranchID", DbType.Int32, BranchID);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:  11/11/2011
        /// Created By:    Gabriel Oquialda
        /// (description)  vehicle dashboard details  
        /// </summary>
        public static DataTable GetVehicleDashboardDetails(Int32 BranchID, DateTime DateFrom, DateTime DateTo)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleDashboard");
                SFDatebase.AddInParameter(SFDbCommand, "pBranchID", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "pDateFrom", DbType.DateTime, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "pDateTo", DbType.DateTime, DateTo);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return SFDataTable;
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
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }

        #region PORT AGENT DASHBOARD
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/11/2011
        /// Description: get port agent port dashboard port
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <returns></returns>
        public static DataTable GetPortAgentPortDashboardDetails(int portAgentId, DateTime StartDate, DateTime EndDate)
        {
            DataTable portDataTable = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentDashboardDetails");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, portAgentId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, StartDate);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, EndDate);
                portDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return portDataTable;
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
                if (portDataTable != null)
                {
                    portDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/11/2011
        /// Description: get port agent hotel dasgboard details count
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public Int32 PortAgentHotelDashboardDetailsCount(int portAgentId, int BranchId, int BrandId, DateTime startDate, DateTime EndDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            int HotelCount = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentHotelDashboardDetailsCount");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, portAgentId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, EndDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                IDataReader dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    HotelCount = Convert.ToInt32(dr["maximumRows"]);
                }

                return HotelCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/11/2011
        /// Description: get port agent hotel dasgboard details
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable PortAgentHotelDashboardDetails(int portAgentId, int BranchId, int BrandId, DateTime startDate, DateTime endDate, Int32 startRowIndex, Int32 maximumRows)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentHotelDashboardDetails");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, portAgentId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, endDate);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/11/2011
        /// Description: load hotel brand with transactions by port agent
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <returns></returns>
        public DataTable loadHotelBrandByPort(int portAgentId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelBrandbyPort");
                db.AddInParameter(dbCommand, "@pportAgentId", DbType.Int32, portAgentId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/11/2011
        /// Description: load hotel branch with transactions by port
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="vendorBrandId"></param>
        /// <returns></returns>
        public DataTable loadHotelBranchByPort(int vendorBrandId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelBranchbyPort");
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, vendorBrandId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/11/2011
        /// Description: get port agent vehicle dashboard details count
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public Int32 PortAgentVehicleDashboardDetailsCount(int portAgentId, int BranchId, int BrandId, DateTime startDate, DateTime EndDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            int HotelCount = 0;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentVehicleDashboardDetailsCount");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, portAgentId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, EndDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                IDataReader dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    HotelCount = Convert.ToInt32(dr["maximumRows"]);
                }

                return HotelCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/11/2011
        /// Description: get port agent vehicle dasgboard details
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable PortAgentVehicleDashboardDetails(int portAgentId, int BranchId, int BrandId, DateTime startDate, DateTime endDate, Int32 startRowIndex, Int32 maximumRows)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetPortAgentVehicleDashboardDetails");
                db.AddInParameter(dbCommand, "@pPortAgentId", DbType.Int32, portAgentId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, endDate);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/11/2011
        /// Description: load vehicle brand with transactions by port agent
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <returns></returns>
        public DataTable loadVehicleBrandByPort(int portAgentId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadVehicleBrandbyPort");
                db.AddInParameter(dbCommand, "@pportAgentId", DbType.Int32, portAgentId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/11/2011
        /// Description: load vehicle branch with transactions by port
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <param name="vendorBrandId"></param>
        /// <returns></returns>
        public DataTable loadVehicleBranchByPort(int vendorBrandId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadVehicleBranchbyPort");
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, vendorBrandId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 13/12/2011
        /// Description: get hotel dashboard with status by date 
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="BrandId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable HotelDashboardbyDate(int BranchId, int BrandId, int RegionId, DateTime startDate, DateTime endDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardStatusbyDate");

                db.AddInParameter(dbCommand, "@pRegionId", DbType.Int32, RegionId);
                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, endDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/11/2011
        /// Description: get hotel dashboard details 
        /// ------------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date modified: 13/12/2011
        /// Description: added startrowindex and maximum rows as parameters. changed stored procedure being called.
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="BranchId"></param>
        /// <param name="Status"></param>
        /// <param name="cDate"></param>
        /// <param name="RoomType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable HotelDashboardDetailsbyStatus(int BrandId, int BranchId, DateTime cDate,
            Int32 startRowIndex, Int32 maximumRows)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable hotelDataTable = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardDetailsbyDate");
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, cDate);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, maximumRows);
                hotelDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return hotelDataTable;
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
                if (hotelDataTable != null)
                {
                    hotelDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/11/2011
        /// Description: get hotel dasboard details count
        /// -----------------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 13/12/2011
        /// Description: changed procedure name from HotelDashboardDetailsCount -> HotelDashboardDetailsbyStatusCount.
        ///              changed stored procedure being called.
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="BrandId"></param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public Int32 HotelDashboardDetailsbyStatusCount(int BranchId, int BrandId, DateTime cDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            int HotelCount = 0;

            try
            {

                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardDetailsbyDateCount");

                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, cDate);
                IDataReader dr = db.ExecuteReader(dbCommand);
                if (dr.Read())
                {
                    HotelCount = Convert.ToInt32(dr["maximumRows"]);
                }

                return HotelCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/11/2011
        /// Description:    get hotel dashboard room types
        /// =====================================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  10/01/2012
        /// Description:    Add parameters regionID, countryID, and cityID
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="BranchID"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <returns></returns>
        public DataTable getHotelDashboardRoomType(string RegionID, string CountryID, string CityID,
            string UserName, string UserRole, string BranchID, string DateFrom, string DateTo,
            string BranchName, int StartRow, int MaxRow)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable hotelDataTable = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardRoomTypeByUser");

                if (RegionID.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, Int32.Parse(RegionID));
                }
                if (CountryID.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32, Int32.Parse(CountryID));
                }
                if (CityID.Trim() != "")
                {
                    db.AddInParameter(dbCommand, "@pCityID", DbType.Int32, Int32.Parse(CityID));
                }
                db.AddInParameter(dbCommand, "@pUserName", DbType.String, UserName);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, UserRole);
                db.AddInParameter(dbCommand, "@pBranchID", DbType.String, BranchID);
                db.AddInParameter(dbCommand, "@pFrom", DbType.DateTime, DateTime.Parse(DateFrom));
                db.AddInParameter(dbCommand, "@pTo", DbType.DateTime, DateTime.Parse(DateTo));
                db.AddInParameter(dbCommand, "@pBranchName", DbType.String, BranchName);
                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                hotelDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                return hotelDataTable;
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
                if (hotelDataTable != null)
                {
                    hotelDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/11/2011
        /// Description:    get hotel dashboard room types Total Count
        /// ----------------------------------------------------------
        /// Modified By:    Charlene Remotigue
        /// Date Modified:  28/11/2011
        /// Description:    change datatable to reader
        /// =====================================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  10/01/2012
        /// Description:    Add parameters regionID, countryID, and cityID
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="BranchID"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <returns></returns>
        public int getHotelDashboardRoomTypeCount(string RegionID, string CountryID, string CityID,
            string UserName, string UserRole, string BranchID, string DateFrom, string DateTo, string BranchName)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand command = null;
            IDataReader dr = null;
            try
            {
                command = db.GetStoredProcCommand("uspGetHotelDashboardRoomTypeByUserCount");
                if (RegionID.Trim() != "")
                {
                    db.AddInParameter(command, "@pRegionID", DbType.Int32, Int32.Parse(RegionID));
                }
                if (CountryID.Trim() != "")
                {
                    db.AddInParameter(command, "@pCountryID", DbType.Int32, Int32.Parse(CountryID));
                }
                if (CityID.Trim() != "")
                {
                    db.AddInParameter(command, "@pCityID", DbType.Int32, Int32.Parse(CityID));
                }
                db.AddInParameter(command, "@pUserName", DbType.String, UserName);
                db.AddInParameter(command, "@pRole", DbType.String, UserRole);
                db.AddInParameter(command, "@pBranchID", DbType.String, BranchID);
                db.AddInParameter(command, "@pFrom", DbType.DateTime, DateTime.Parse(DateFrom));
                db.AddInParameter(command, "@pTo", DbType.DateTime, DateTime.Parse(DateTo));
                db.AddInParameter(command, "@pBranchName", DbType.String, BranchName);

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
        /// Author: Charlene Remotigue
        /// Date Created: 25/11/2011
        /// Description: save hotel room override
        /// </summary>
        /// <param name="HotelRoomId"></param>
        /// <param name="BranchId"></param>
        /// <param name="EffectiveDate"></param>
        /// <param name="rate"></param>
        /// <param name="Currency"></param>
        /// <param name="TaxRate"></param>
        /// <param name="taxInclusive"></param>
        /// <param name="RoomType"></param>
        /// <param name="RoomBlocks"></param>
        /// <param name="AddedRooms"></param>
        /// <param name="userId"></param>
        public int SaveHotelOverride(int HotelRoomId, int BranchId, DateTime EffectiveDate, string rate, int Currency,
            string TaxRate, bool taxInclusive, int RoomType, int RoomBlocks, int AddedRooms, string userId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            try
            {
                dbCommand = db.GetStoredProcCommand("uspSaveHotelRoomOverride");
                db.AddInParameter(dbCommand, "@pHotelRoomId", DbType.Int32, HotelRoomId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pEffectiveDate", DbType.Date, EffectiveDate);
                db.AddInParameter(dbCommand, "@pRatePerDay", DbType.String, rate);
                db.AddInParameter(dbCommand, "@pCurrency", DbType.Int32, Currency);
                db.AddInParameter(dbCommand, "@pRateTax", DbType.String, TaxRate);
                db.AddInParameter(dbCommand, "@pTaxInclusiveBit", DbType.Boolean, taxInclusive);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                db.AddInParameter(dbCommand, "@pRoomBlocks", DbType.Int32, RoomBlocks);
                db.AddInParameter(dbCommand, "@pAddedRoomBlocks", DbType.Int32, AddedRooms);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, userId);
                db.AddOutParameter(dbCommand, "@pRetVal", DbType.Int32, 1);
                db.ExecuteNonQuery(dbCommand, dbTransaction);
                int retVal = (int)dbCommand.Parameters["@pRetVal"].Value;
                dbTransaction.Commit();
                return retVal;

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 25/11/2011
        /// Description: get hotel room override details
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="HotelRoomId"></param>
        /// <param name="RoomType"></param>
        /// <returns></returns>
        public IDataReader getOverrideDetails(int BranchId, int HotelRoomId, int RoomType)
        {
            IDataReader overrideDataReader = null;
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetOverrideDetails");
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pHotelRoomId", DbType.Int32, HotelRoomId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, RoomType);
                overrideDataReader = db.ExecuteReader(dbCommand);
                return overrideDataReader;
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
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/12/2011
        /// Description: get hotel dashboard with status by date from contract
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="BrandId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable HotelDashboardStatfromContract(int BranchId, int BrandId, DateTime startDate)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DataTable PortAgentHotel = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardStatusbyDatefromContract");

                db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, startDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pBrandId", DbType.Int32, BrandId);
                PortAgentHotel = db.ExecuteDataSet(dbCommand).Tables[0];
                return PortAgentHotel;
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
                if (PortAgentHotel != null)
                {
                    PortAgentHotel.Dispose();
                }
            }
        }
        #endregion

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   03/02/2012
        /// Description:    get all dashboard2 tables
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/02/2012
        /// Description:    Add Total Emergency Room in HotelRoomBlocks List
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  20/02/2012
        /// Description:    Add booking type in confirmed booking list
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <returns></returns>
        //public List<HotelDashBoardGenericClass> LoadAllHotelDashboard2Tables(DateTime StartDate,
        //    Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    List<HotelDashBoardGenericClass> DashBoardList = new List<HotelDashBoardGenericClass>();
        //    Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;

        //    int EventCount = 0;
        //    int ConfirmedCount = 0;
        //    int PendingCount = 0;
        //    DataTable StatusDT = null;
        //    DataTable ConfirmedDT = null;
        //    DataTable PendingDT = null;
        //    DataSet ds = null;
        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspLoadHotelDashBoard2PageQueries");
        //        db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, StartDate);
        //        db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
        //        db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
        //        db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
        //        db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
        //        db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
        //        ds = db.ExecuteDataSet(dbCommand);
        //        EventCount = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
        //        StatusDT = ds.Tables[1];
        //        ConfirmedCount = Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
        //        ConfirmedDT = ds.Tables[3];
        //        PendingCount = Int32.Parse(ds.Tables[4].Rows[0][0].ToString());
        //        PendingDT = ds.Tables[5];
        //        //if (ds.Tables[6] != null)
        //        //{
        //        //    EmailDT = ds.Tables[6];
        //        //}

        //        DashBoardList.Add(new HotelDashBoardGenericClass()
        //        {
        //            EventCount = EventCount,
        //            HotelRoomBlocks = (from a in StatusDT.AsEnumerable()
        //                               select new HotelRoomBlocks
        //                               {
        //                                   Date = GlobalCode.Field2DateTime(a["colDate"]),
        //                                   Day = a.Field<string>("colDay"),
        //                                   RoomName = a.Field<string>("colRoomNameVarchar"),
        //                                   Expected = GlobalCode.Field2Int(a["Expected"]),
        //                                   Arriving = GlobalCode.Field2Int(a["Arriving"]),
        //                                   CheckedIn = GlobalCode.Field2Int(a["CheckedIn"]),
        //                                   CheckedOut = GlobalCode.Field2Int(a["CheckedOut"]),
        //                                   Cancelled = GlobalCode.Field2Int(a["Cancelled"]),
        //                                   NoShow = GlobalCode.Field2Int(a["NoShow"]),
        //                                   TotalRoomBookings = GlobalCode.Field2Decimal(a["TotalRoomBookings"]),
        //                                   TotalRoomBlocks = GlobalCode.Field2Int(a["TotalRoomBlocks"]),
        //                                   TotalEmergencyBookings = GlobalCode.Field2Decimal(a["TotalEmergencyBookings"]),
        //                                   TotalEmergencyRoomBlocks = GlobalCode.Field2Int(a["TotalEmergencyRoomBlocks"]),
        //                               }).ToList(),
        //            ConfirmBookingCount = ConfirmedCount,
        //            ConfirmBooking = (from b in ConfirmedDT.AsEnumerable()
        //                              select new ConfirmBooking
        //                              {
        //                                  RecordLocator = b.Field<string>("colRecordLocatorVarchar"),
        //                                  E1ID = GlobalCode.Field2Int(b["colE1IdInt"]),
        //                                  RoomType = b.Field<string>("colRoomTypeName"),
        //                                  Name = b.Field<string>("colNameVarchar"),
        //                                  CheckInDate = b.Field<DateTime?>("colCheckinDate"),
        //                                  CheckOutDate = b.Field<DateTime?>("colCheckOutDate"),
        //                                  RankName = b.Field<string>("colRankNameVarchar"),
        //                                  Gender = b.Field<string>("colGenderVarchar"),
        //                                  Nationality = b.Field<string>("colNationalityVarchar"),
        //                                  CostCenter = b.Field<string>("colCostCenterVarchar"),
        //                                  HotelNites = GlobalCode.Field2Int(b["colDurationInt"]),
        //                                  HotelCity = b.Field<string>("colCityNameVarchar"),
        //                                  Airline = b.Field<string>("colAirlineVarchar"),
        //                                  FromCity = b.Field<string>("colFromCityVarchar"),
        //                                  ToCity = b.Field<string>("colToCityVarchar"),
        //                                  HotelStatus = b.Field<string>("colHotelStatusVarchar"),
        //                                  TravelReqId = GlobalCode.Field2Int(b["colTravelReqIdInt"]),
        //                                  ReqId = GlobalCode.Field2Int(b["colReqIdint"]),
        //                                  SFStatus = b.Field<string>("colSFStatus"),
        //                                  BookingType = b["colBookingTypeVarchar"].ToString(),
        //                                  E1TravelReqIdInt = b.Field<int>("colE1TravelReqIdInt"),
        //                              }).ToList(),
        //            PendingBookingCount = PendingCount,
        //            PendingBooking = (from c in PendingDT.AsEnumerable()
        //                              select new OverflowBooking
        //                              {
        //                                  CoupleId = c.Field<int?>("colCoupleIdInt"),
        //                                  Gender = c.Field<string>("colGenderVarchar"),
        //                                  Nationality = c.Field<string>("colNationalityVarchar"),
        //                                  CostCenter = c.Field<string>("coLCostCenterVarchar"),
        //                                  CheckInDate = GlobalCode.Field2DateTime(c["colCheckInDateTime"]),
        //                                  CheckOutDate = GlobalCode.Field2DateTime(c["colCheckOutDatetime"]),

        //                                  TravelReqId = GlobalCode.Field2Int(c["colTravelRequestIdInt"]),
        //                                  SFStatus = c.Field<string>("colStatusVarchar"),
        //                                  Name = c.Field<string>("colNameVarchar"),
        //                                  SeafarerId = GlobalCode.Field2Int(c["colSeafarerIdInt"]),
        //                                  VesselName = c.Field<string>("colVesselNameVarchar"),
        //                                  RoomName = c.Field<string>("colRoomNameVarchar"),
        //                                  RankName = c.Field<string>("colRankNameVarchar"),
        //                                  CityId = GlobalCode.Field2Int(c["colCityIdInt"]),
        //                                  CountryId = GlobalCode.Field2Int(c["colCountryIdInt"]),
        //                                  HotelCity = c.Field<string>("colHotelCityVarchar"),
        //                                  HotelNites = GlobalCode.Field2Int(c["colHotelNitesInt"]),
        //                                  FromCity = c.Field<string>("colFromCityVarchar"),
        //                                  ToCity = c.Field<string>("colToCityVarchar"),
        //                                  RecordLocator = c.Field<string>("colRecordLocatorVarchar"),
        //                                  Carrier = c.Field<string>("colCarrierVarchar"),
        //                                  DepartureDate = GlobalCode.Field2DateTime(c["colDepartureDateTime"]),
        //                                  ArrivalDate = GlobalCode.Field2DateTime(c["colArrivalDatetime"]),
        //                                  FlightNo = c.Field<string>("colFlightNoVarchar"),
        //                                  OnOffDate = GlobalCode.Field2DateTime(c["colOnOffDate"]),
        //                                  Voucher = c["colVoucherMoney"].ToString(),
        //                                  ReasonCode = c.Field<string>("colReasonCodeVarchar"),
        //                                  Stripe = c.Field<decimal?>("colStripesDecimal"),
        //                                  VendorId = GlobalCode.Field2Int(c["colVendorIdInt"]),
        //                                  BranchId = GlobalCode.Field2Int(c["colBranchIdInt"]),
        //                                  RoomTypeId = GlobalCode.Field2Int(c["colRoomTypeIdInt"]),
        //                                  PortId = GlobalCode.Field2Int(c["colPortIdInt"]),
        //                                  VesselId = GlobalCode.Field2Int(c["colVesselIdInt"]),
        //                                  EnabledBit = GlobalCode.Field2Bool(c["isEnabled"]),
        //                              }).ToList()
        //        });
        //        return DashBoardList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //        }
        //        if (ConfirmedDT != null)
        //        {
        //            ds.Dispose();
        //        }
        //        if (StatusDT != null)
        //        {
        //            StatusDT.Dispose();
        //        }
        //        if (PendingDT != null)
        //        {
        //            PendingDT.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author:         Charlene Remotigue
        /// Date Created:   03/02/2012
        /// Description:    get all dashboard2 tables
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/02/2012
        /// Description:    Add Total Emergency Room in HotelRoomBlocks List
        /// ------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  20/02/2012
        /// Description:    Add booking type in confirmed booking list
        /// -------------------------------------------------------------------------------------
        /// Modfied by:     Gabriel Oquialda
        /// Date Modified:  13/03/2012
        /// Description:    This is a modified 'LoadAllHotelDashboard2Tables2' copy for new screens
        /// /// -------------------------------------------------------------------------------------
        /// Modfied by:     Jefferson Bermundo
        /// Date Modified:  13/07/2012
        /// Description:    Includes Hotel Request in Confirmed Booking and Pending/Overflow
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <returns></returns>
        public List<HotelDashBoardGenericClass> LoadAllHotelDashboard2Tables2(DateTime StartDate,
            Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            List<HotelDashBoardGenericClass> DashBoardList = new List<HotelDashBoardGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            int EventCount = 0;
            int ConfirmedCount = 0;
            int PendingCount = 0;
            DataTable StatusDT = null;
            DataTable ConfirmedDT = null;
            DataTable PendingDT = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelDashBoard2PageQueries_PROTOTYPE2");
                db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, StartDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
                ds = db.ExecuteDataSet(dbCommand);
                if (LoadType == 4)
                {
                    StatusDT = ds.Tables[0];

                    DashBoardList.Add(new HotelDashBoardGenericClass()
                    {
                        HotelRoomBlocks = (from a in StatusDT.AsEnumerable()
                                           select new HotelRoomBlocks
                                           {
                                               Date = GlobalCode.Field2DateTime(a["colDate"]),
                                               Day = a.Field<string>("colDay"),
                                               RoomTypeID = GlobalCode.Field2Int(a["RoomTypeID"]),
                                               RoomName = a.Field<string>("colRoomNameVarchar"),
                                               ReservedRoom = GlobalCode.Field2Decimal(a["ReservedRoom"]),
                                               Expected = GlobalCode.Field2Int(a["Expected"]),
                                               Arriving = GlobalCode.Field2Int(a["Arriving"]),
                                               CheckedIn = GlobalCode.Field2Int(a["CheckedIn"]),
                                               CheckedOut = GlobalCode.Field2Int(a["CheckedOut"]),
                                               Cancelled = GlobalCode.Field2Int(a["Cancelled"]),
                                               NoShow = GlobalCode.Field2Int(a["NoShow"]),
                                               TotalRoomBookings = GlobalCode.Field2Decimal(a["TotalRoomBookings"]),
                                               TotalRoomBlocks = GlobalCode.Field2Int(a["TotalRoomBlocks"]),
                                               TotalEmergencyBookings = GlobalCode.Field2Decimal(a["TotalEmergencyBookings"]),
                                               TotalEmergencyRoomBlocks = GlobalCode.Field2Int(a["TotalEmergencyRoomBlocks"]),
                                           }).ToList()
                    });
                    return DashBoardList;
                }
                else
                {
                    EventCount = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    StatusDT = ds.Tables[1];
                    ConfirmedCount = Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
                    ConfirmedDT = ds.Tables[3];
                    PendingCount = Int32.Parse(ds.Tables[4].Rows[0][0].ToString());
                    PendingDT = ds.Tables[5];
                    //if (ds.Tables[6] != null)
                    //{
                    //    EmailDT = ds.Tables[6];
                    //}

                    DashBoardList.Add(new HotelDashBoardGenericClass()
                    {
                        EventCount = EventCount,
                        HotelRoomBlocks = (from a in StatusDT.AsEnumerable()
                                           select new HotelRoomBlocks
                                           {
                                               Date = GlobalCode.Field2DateTime(a["colDate"]),
                                               Day = a.Field<string>("colDay"),
                                               RoomTypeID = GlobalCode.Field2Int(a["RoomTypeID"]),
                                               RoomName = a.Field<string>("colRoomNameVarchar"),
                                               ReservedRoom = GlobalCode.Field2Decimal(a["ReservedRoom"]),
                                               Expected = GlobalCode.Field2Int(a["Expected"]),
                                               Arriving = GlobalCode.Field2Int(a["Arriving"]),
                                               CheckedIn = GlobalCode.Field2Int(a["CheckedIn"]),
                                               CheckedOut = GlobalCode.Field2Int(a["CheckedOut"]),
                                               Cancelled = GlobalCode.Field2Int(a["Cancelled"]),
                                               NoShow = GlobalCode.Field2Int(a["NoShow"]),
                                               TotalRoomBookings = GlobalCode.Field2Decimal(a["TotalRoomBookings"]),
                                               TotalRoomBlocks = GlobalCode.Field2Int(a["TotalRoomBlocks"]),
                                               TotalEmergencyBookings = GlobalCode.Field2Decimal(a["TotalEmergencyBookings"]),
                                               TotalEmergencyRoomBlocks = GlobalCode.Field2Int(a["TotalEmergencyRoomBlocks"]),
                                           }).ToList(),
                        ConfirmBookingCount = ConfirmedCount,
                        ConfirmBooking = (from b in ConfirmedDT.AsEnumerable()
                                          select new ConfirmBooking
                                          {
                                              HotelCity = b.Field<string>("HotelCity"),
                                              CheckIn = b.Field<DateTime?>("CheckIn"),
                                              CheckOut = b.Field<DateTime?>("CheckOut"),
                                              HotelNites = GlobalCode.Field2Int(b["HotelNites"]),
                                              ReasonCode = b.Field<string>("ReasonCode"),
                                              LastName = b.Field<string>("LastName"),
                                              FirstName = b.Field<string>("FirstName"),
                                              EmployeeId = GlobalCode.Field2Int(b["EmployeeId"]),
                                              Gender = b.Field<string>("Gender"),
                                              SingleDouble = b.Field<string>("SingleDouble"),
                                              Couple = b.Field<string>("Couple"),

                                              Title = b.Field<string>("Title"),
                                              Ship = b.Field<string>("Ship"),
                                              CostCenter = b.Field<string>("CostCenter"),
                                              Nationality = b.Field<string>("Nationality"),

                                              HotelRequest = b.Field<string>("HotelRequest"),
                                              RecordLocator = b.Field<string>("RecordLocator"),

                                              DeptCity = b.Field<string>("DeptCity"),
                                              ArvlCity = b.Field<string>("ArvlCity"),
                                              DeptDate = b.Field<DateTime?>("DeptDate"),
                                              ArvlDate = b.Field<DateTime?>("ArvlDate"),
                                              DeptTime = b.Field<TimeSpan?>("DeptTime"),
                                              ArvlTime = b.Field<TimeSpan?>("ArvlTime"),

                                              Carrier = b.Field<string>("Carrier"),
                                              FlightNo = b.Field<string>("FlightNo"),


                                              Voucher = GlobalCode.Field2Double(b["Voucher"]),
                                              PassportNo = b.Field<string>("PassportNo"),
                                              IssuedDate = b.Field<string>("IssuedDate"),
                                              PassportExpiration = b.Field<string>("PassportExpiration"),

                                              WithSailMaster = GlobalCode.Field2Bool(b["colWithSailMasterBit"]),
                                              IsTaggedByUser = GlobalCode.Field2Bool(b["IsTaggedByUser"]),
                                               
                                              TravelReqId = GlobalCode.Field2Int(b["colTravelReqIdInt"]),
                                              SFStatus = b.Field<string>("colSFStatus"),
                                              colIdBigInt = GlobalCode.Field2Int(b["colIdBigInt"]),
                                              colConfirmation = b.Field<string>("colConfirmationVarchar"),
                                              RoomType = GlobalCode.Field2String(b["RoomFloat"]),
                                              TagDateTime = GlobalCode.Field2DateTimeWithTime(b["colTagDateTime"]),

                                              IsMeetGreet = GlobalCode.Field2String(b["IsMeetGreet"]),
                                              IsPortAgent = GlobalCode.Field2String(b["IsPortAgent"]),
                                              IsHotelVendor = GlobalCode.Field2String(b["IsHotelVendor"]),

                                              Birthday = GlobalCode.Field2String(b["Birthday"])
                                              //TagDateTime = b.Field<string>("colTagDateTime")                                             
                                              //ReqId = GlobalCode.Field2Int(b["colReqIdint"]),
                                              //HotelStatus = b.Field<string>("colHotelStatusVarchar"),
                                              //BookingType = b["colBookingTypeVarchar"].ToString(),
                                              //E1TravelReqIdInt = b.Field<int>("colE1TravelReqIdInt"),


                                              //PassportNo = b.Field<string>("PassportNo"),
                                              //PassportExp = b.Field<string>("PassportExp"),
                                              //PassportIssued = b.Field<string>("PassportIssued"),

                                          }).ToList(),
                        PendingBookingCount = PendingCount,
                        PendingBooking = (from c in PendingDT.AsEnumerable()
                                          select new OverflowBooking
                                          {
                                              CoupleId = c.Field<int?>("colCoupleIdInt"),
                                              Gender = c.Field<string>("colGenderVarchar"),
                                              Nationality = c.Field<string>("colNationalityVarchar"),
                                              CostCenter = c.Field<string>("coLCostCenterVarchar"),
                                              CheckInDate = c.Field<DateTime?>("colCheckInDateTime"),
                                              CheckOutDate = c.Field<DateTime?>("colCheckOutDatetime"),

                                              TravelReqId = GlobalCode.Field2Int(c["colTravelRequestIdInt"]),
                                              SFStatus = c.Field<string>("colStatusVarchar"),
                                              Name = c.Field<string>("colNameVarchar"),
                                              SeafarerId = GlobalCode.Field2Int(c["colSeafarerIdInt"]),
                                              VesselName = c.Field<string>("colVesselNameVarchar"),
                                              RoomName = c.Field<string>("colRoomNameVarchar"),
                                              RankName = c.Field<string>("colRankNameVarchar"),
                                              CityId = GlobalCode.Field2Int(c["colCityIdInt"]),
                                              CountryId = GlobalCode.Field2Int(c["colCountryIdInt"]),
                                              HotelCity = c.Field<string>("colHotelCityVarchar"),
                                              HotelNites = GlobalCode.Field2Int(c["colHotelNitesInt"]),
                                              FromCity = c.Field<string>("colFromCityVarchar"),
                                              ToCity = c.Field<string>("colToCityVarchar"),
                                              RecordLocator = c.Field<string>("colRecordLocatorVarchar"),
                                              Carrier = c.Field<string>("colCarrierVarchar"),
                                              DepartureDate = c.Field<DateTime?>("colDepartureDateTime"),
                                              ArrivalDate = c.Field<DateTime?>("colArrivalDatetime"),
                                              FlightNo = c.Field<string>("colFlightNoVarchar"),
                                              OnOffDate = c.Field<DateTime?>("colOnOffDate"),
                                              Voucher = c["colVoucherMoney"].ToString(),
                                              ReasonCode = c.Field<string>("colReasonCodeVarchar"),
                                              Stripe = c.Field<decimal?>("colStripesDecimal"),
                                              VendorId = GlobalCode.Field2Int(c["colVendorIdInt"]),
                                              BranchId = GlobalCode.Field2Int(c["colBranchIdInt"]),
                                              RoomTypeId = GlobalCode.Field2Int(c["colRoomTypeIdInt"]),
                                              PortId = GlobalCode.Field2Int(c["colPortIdInt"]),
                                              VesselId = GlobalCode.Field2Int(c["colVesselIdInt"]),
                                              EnabledBit = GlobalCode.Field2Bool(c["isEnabled"]),
                                              WithSailMaster = GlobalCode.Field2Bool(c["colWithSailMasterBit"]),
                                              HotelRequest = Convert.ToBoolean(c.Field<Int32>("HotelRequest"))
                                          }).ToList()
                    });
                    return DashBoardList;
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
                if (ConfirmedDT != null)
                {
                    ds.Dispose();
                }
                if (StatusDT != null)
                {
                    StatusDT.Dispose();
                }
                if (PendingDT != null)
                {
                    PendingDT.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/02/2012
        /// Description: get confirmed booking paging
        /// ----------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   20/02/2012
        /// Description:    Add booking type
        /// ----------------------------------------
        /// Modified By:    Jefferson Bermundo
        /// Date Created:   13/07/2012
        /// Description:    Includes With Sail Master and Hotel Request In paging
        /// ----------------------------------------
        /// Modified By:    Muhallidin G Wali
        /// Date Created:   19/12/2012
        /// Description:    Add a ArrivalTime and meal allowance
        /// ----------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   04/Jan/2013
        /// Description:    Add Passport details
        /// ----------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Created:   12/Feb/2013
        /// Description:    Reorder Columns based from Locked manifest exported file
        /// ----------------------------------------
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <param name="MaxRows"></param>
        /// <returns></returns>
        public List<ConfirmBooking> LoadHotelDashboardconfirmedTable(DateTime StartDate,
           Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows, out int MaxRows, String SortBy)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataTable ConfirmedDT = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelDashBoard2PageQueries");
                db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, StartDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
                db.AddInParameter(dbCommand, "@pSortBy", DbType.String, SortBy);
                ds = db.ExecuteDataSet(dbCommand);
                MaxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                ConfirmedDT = ds.Tables[1];

                var query = (from b in ConfirmedDT.AsEnumerable()
                             select new ConfirmBooking
                             {
                                 HotelCity = b.Field<string>("HotelCity"),
                                 CheckIn = b.Field<DateTime?>("CheckIn"),
                                 CheckOut = b.Field<DateTime?>("CheckOut"),
                                 HotelNites = GlobalCode.Field2Int(b["HotelNites"]),
                                 ReasonCode = b.Field<string>("ReasonCode"),
                                 LastName = b.Field<string>("LastName"),
                                 FirstName = b.Field<string>("FirstName"),
                                 EmployeeId = GlobalCode.Field2Int(b["EmployeeId"]),
                                 Gender = b.Field<string>("Gender"),
                                 SingleDouble = b.Field<string>("SingleDouble"),
                                 Couple = b.Field<string>("Couple"),

                                 Title = b.Field<string>("Title"),
                                 Ship = b.Field<string>("Ship"),
                                 CostCenter = b.Field<string>("CostCenter"),
                                 Nationality = b.Field<string>("Nationality"),

                                 HotelRequest = b.Field<string>("HotelRequest"),
                                 RecordLocator = b.Field<string>("RecordLocator"),

                                 DeptCity = b.Field<string>("DeptCity"),
                                 ArvlCity = b.Field<string>("ArvlCity"),
                                 DeptDate = b.Field<DateTime?>("DeptDate"),
                                 ArvlDate = b.Field<DateTime?>("ArvlDate"),
                                 DeptTime = b.Field<TimeSpan?>("DeptTime"),
                                 ArvlTime = b.Field<TimeSpan?>("ArvlTime"),

                                 Carrier = b.Field<string>("Carrier"),
                                 FlightNo = b.Field<string>("FlightNo"),


                                 Voucher = GlobalCode.Field2Double(b["Voucher"]),
                                 PassportNo = b.Field<string>("PassportNo"),
                                 IssuedDate = b.Field<string>("IssuedDate"),
                                 PassportExpiration = b.Field<string>("PassportExpiration"),

                                 WithSailMaster = GlobalCode.Field2Bool(b["colWithSailMasterBit"]),
                                 IsTaggedByUser = GlobalCode.Field2Bool(b["IsTaggedByUser"]),


                                 TravelReqId = GlobalCode.Field2Int(b["colTravelReqIdInt"]),
                                 SFStatus = b.Field<string>("colSFStatus"),
                                 colIdBigInt = GlobalCode.Field2Int(b["colIdBigInt"]),
                                 colConfirmation = b.Field<string>("colConfirmationVarchar"),
                                 RoomType = b.Field<double>("RoomFloat").ToString(),
                                 //TagDateTime = b.Field<string>("colTagDateTime")
                                 TagDateTime = GlobalCode.Field2DateTimeWithTime(b["colTagDateTime"]),
                                 
                                 IsMeetGreet = GlobalCode.Field2String(b["IsMeetGreet"]),
                                 IsPortAgent = GlobalCode.Field2String(b["IsPortAgent"]),
                                 IsHotelVendor = GlobalCode.Field2String(b["IsHotelVendor"]),

                                 Birthday = GlobalCode.Field2String(b["Birthday"])
                                 //RecordLocator = b.Field<string>("colRecordLocatorVarchar"),
                                 //E1ID = GlobalCode.Field2Int(b["colE1IdInt"]),
                                 //RoomType = b.Field<string>("colRoomTypeName"),
                                 //Name = b.Field<string>("colNameVarchar"),
                                 //CheckInDate = b.Field<DateTime?>("colCheckinDate"),
                                 //CheckOutDate = b.Field<DateTime?>("colCheckOutDate"),
                                 //RankName = b.Field<string>("colRankNameVarchar"),
                                 //Gender = b.Field<string>("colGenderVarchar"),
                                 //Nationality = b.Field<string>("colNationalityVarchar"),
                                 //CostCenter = b.Field<string>("colCostCenterVarchar"),
                                 //HotelNites = GlobalCode.Field2Int(b["colDurationInt"]),
                                 //HotelCity = b.Field<string>("colCityNameVarchar"),
                                 //Airline = b.Field<string>("colAirlineVarchar"),
                                 //MealAllowance = GlobalCode.Field2Double(b["colMealAllowanceMoney"]),
                                 //ArrivalTime = GlobalCode.Field2DateTime(b["colArrivalTimeDateTime"]),
                                 //FromCity = b.Field<string>("colFromCityVarchar"),
                                 //ToCity = b.Field<string>("colToCityVarchar"),
                                 //HotelStatus = b.Field<string>("colHotelStatusVarchar"),
                                 //TravelReqId = GlobalCode.Field2Int(b["colTravelReqIdInt"]),
                                 //ReqId = GlobalCode.Field2Int(b["colReqIdint"]),
                                 //SFStatus = b.Field<string>("colSFStatus"),
                                 //BookingType = b["colBookingTypeVarchar"].ToString(),
                                 //E1TravelReqIdInt = b.Field<int>("colE1TravelReqIdInt"),
                                 //HotelRequest = Convert.ToBoolean(b.Field<Int32>("HotelRequest")),
                                 //WithSailMaster = b.Field<Boolean>("colWithSailMasterBit"),
                                 //PassportNo = b.Field<string>("PassportNo"),
                                 //PassportIssued = b.Field<string>("PassportIssued"),
                                 //PassportExp = b.Field<string>("PassportExp"),

                             }).ToList();

                return query;
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
                if (ConfirmedDT != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:            Josephine Gad
        /// Date Created:      07/Jan/2013
        /// Description:       Export Confirmed booking
        /// ----------------------------------------
        /// Author:            Marco Abejar
        /// Date Created:      08/April/2013
        /// Description:       Add Branch Param
        /// ----------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static DataTable LoadHotelDashboardConfirmedExport(String UserId, Int32 BranchId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataTable ConfirmedDT = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelDashBoardExportConfirm");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);

                ds = db.ExecuteDataSet(dbCommand);
                ConfirmedDT = ds.Tables[0];

                return ConfirmedDT;
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
                if (ConfirmedDT != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Author:            Josephine Gad
        /// Date Created:      07/Jan/2013
        /// Description:       Export Overflow booking
        /// ----------------------------------------
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static DataTable LoadHotelDashboardOverflowExport(String UserId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataTable ConfirmedDT = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelDashBoardExportOverflow");
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);

                ds = db.ExecuteDataSet(dbCommand);
                ConfirmedDT = ds.Tables[0];

                return ConfirmedDT;
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
                if (ConfirmedDT != null)
                {
                    ds.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date created: 03/02/2012
        /// Description: load pending booking paging
        /// ----------------------------------------
        /// Modified By:    Jefferson Bermundo
        /// Date Created:   13/07/2012
        /// Description:    Includes Hotel Request In paging
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="MaximumRows"></param>
        /// <param name="MaxRows"></param>
        /// <returns></returns>
        public List<OverflowBooking> LoadHotelDashboardPendingTable(DateTime StartDate,
           Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows, out int MaxRows,
            string SortBy)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataTable PendingDT = null;
            DataSet ds = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspLoadHotelDashBoard2PageQueries");
                db.AddInParameter(dbCommand, "@pStartDate", DbType.DateTime, StartDate);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pstartRowIndex", DbType.Int32, StartRowIndex);
                db.AddInParameter(dbCommand, "@pmaximumRows", DbType.Int32, MaximumRows);
                db.AddInParameter(dbCommand, "@pSortBy", DbType.String, SortBy);
                ds = db.ExecuteDataSet(dbCommand);
                MaxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                PendingDT = ds.Tables[1];

                var query = (from c in PendingDT.AsEnumerable()
                             select new OverflowBooking
                             {

                                 CoupleId = c.Field<int?>("colCoupleSeafarerIdInt"),
                                 Gender = c.Field<string>("colGenderVarchar"),
                                 Nationality = c.Field<string>("colNationalityVarchar"),
                                 CostCenter = c.Field<string>("coLCostCenterVarchar"),
                                 CheckInDate = c.Field<DateTime?>("colCheckInDateTime"),
                                 CheckOutDate = c.Field<DateTime?>("colCheckOutDatetime"),

                                 TravelReqId = GlobalCode.Field2Int(c["colTravelRequestIdInt"]),
                                 SFStatus = c.Field<string>("colStatusVarchar"),
                                 Name = c.Field<string>("colNameVarchar"),
                                 SeafarerId = GlobalCode.Field2Int(c["colSeafarerIdInt"]),
                                 VesselName = c.Field<string>("colVesselNameVarchar"),
                                 RoomName = c.Field<string>("colRoomNameVarchar"),
                                 RankName = c.Field<string>("colRankNameVarchar"),
                                 CityId = GlobalCode.Field2Int(c["colCityIdInt"]),
                                 CountryId = GlobalCode.Field2Int(c["colCountryIdInt"]),
                                 HotelCity = c.Field<string>("colHotelCityVarchar"),
                                 HotelNites = GlobalCode.Field2Int(c["colHotelNitesInt"]),
                                 FromCity = c.Field<string>("colFromCityVarchar"),
                                 ToCity = c.Field<string>("colToCityVarchar"),
                                 RecordLocator = c.Field<string>("colRecordLocatorVarchar"),
                                 Carrier = c.Field<string>("colCarrierVarchar"),
                                 DepartureDate = c.Field<DateTime?>("colDepartureDateTime"),
                                 ArrivalDate = c.Field<DateTime?>("colArrivalDatetime"),
                                 FlightNo = c.Field<string>("colFlightNoVarchar"),
                                 OnOffDate = c.Field<DateTime?>("colOnOffDate"),
                                 Voucher = c["colVoucherMoney"].ToString(),
                                 ReasonCode = c.Field<string>("colReasonCodeVarchar"),
                                 Stripe = GlobalCode.Field2Decimal(c["colStripesDecimal"]),
                                 VendorId = GlobalCode.Field2Int(c["colVendorIdInt"]),
                                 BranchId = GlobalCode.Field2Int(c["colBranchIdInt"]),
                                 RoomTypeId = GlobalCode.Field2Int(c["colRoomTypeIdInt"]),
                                 PortId = GlobalCode.Field2Int(c["colPortIdInt"]),
                                 VesselId = GlobalCode.Field2Int(c["colVesselIdInt"]),
                                 EnabledBit = GlobalCode.Field2Bool(c["isEnabled"]),
                                 HotelRequest = Convert.ToBoolean(c.Field<Int32>("HotelRequest"))
                             }).ToList();

                return query;
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
                if (PendingDT != null)
                {
                    ds.Dispose();
                }
            }
        }
    }
}

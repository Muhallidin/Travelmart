using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class DashboardBLL
    {
        private DashboardDAL dbDAL = new DashboardDAL();
        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard     
        /// </summary>
        public static DataTable GetDashboardDetails(string DateFrom, string DateTo, string Port)
        {
            DataTable dt = null;
            try
            {
                DateTime dtDateFrom = Convert.ToDateTime(DateFrom);
                DateTime dtDateTo = Convert.ToDateTime(DateTo);
                Int32 PortID = Convert.ToInt32(Port);

                dt = DashboardDAL.GetDashboardDetails(dtDateFrom, dtDateTo, PortID);
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
            }
        }

        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard details     
        /// </summary>
        public static DataTable GetHotelDashboardDetails(string BranchID, string DateFrom, string DateTo)
        {
            DataTable dt = null;
            try
            {
                Int32 BranchIDInt = Convert.ToInt32(BranchID);
                DateTime DteFrom = Convert.ToDateTime(DateFrom);
                DateTime DteTo = Convert.ToDateTime(DateTo);

                dt = DashboardDAL.GetHotelDashboardDetails(BranchIDInt, DteFrom, DteTo);
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
            }
        }

        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard details per week     
        /// </summary>
        public static DataTable GetHotelDashboardDetailsWeek(string BranchID)
        {
            DataTable dt = null;
            try
            {
                Int32 BranchIDInt = Convert.ToInt32(BranchID);

                dt = DashboardDAL.GetHotelDashboardDetailsWeek(BranchIDInt);
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
            }
        }
        /// <summary>        
        /// Date Created:  25/07/2011
        /// Created By:    Ryan Bautista
        /// (description)  hotel dashboard main     
        /// </summary>
        public static DataTable GetHotelDashboardDetailsMain(string BranchID)
        {
            DataTable dt = null;
            try
            {
                Int32 BranchIDInt = Convert.ToInt32(BranchID);

                dt = DashboardDAL.GetHotelDashboardDetailsMain(BranchIDInt);
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
            }
        }
        /// <summary>        
        /// Date Created:  11/11/2011
        /// Created By:    Gabriel Oquialda
        /// (description)  vehicle dashboard details     
        /// </summary>
        public static DataTable GetVehicleDashboardDetails(string BranchID, string DateFrom, string DateTo)
        {
            DataTable dt = null;
            try
            {
                Int32 BranchIDInt = Convert.ToInt32(BranchID);
                DateTime DteFrom = Convert.ToDateTime(DateFrom);
                DateTime DteTo = Convert.ToDateTime(DateTo);

                dt = DashboardDAL.GetVehicleDashboardDetails(BranchIDInt, DteFrom, DteTo);
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
        public static DataTable GetPortAgentPortDashboardDetails(string portAgentId, string StartDate, string EndDate)
        {
            try
            {
                return DashboardDAL.GetPortAgentPortDashboardDetails(Int32.Parse(portAgentId), DateTime.Parse(StartDate), DateTime.Parse(EndDate));
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                return dbDAL.PortAgentHotelDashboardDetailsCount(portAgentId, BranchId, BrandId, startDate, EndDate);
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
            try
            {
                return dbDAL.PortAgentHotelDashboardDetails(portAgentId, BranchId, BrandId, startDate, endDate, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 15/11/2011
        /// Description: load hotel with transactions by port agent
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <returns></returns>
        public DataTable loadHotelBrandByPort(string portAgentId)
        {
            try
            {
                return dbDAL.loadHotelBrandByPort(Int32.Parse(portAgentId));
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                return dbDAL.loadHotelBranchByPort(vendorBrandId);
            }
            catch (Exception ex)
            {
                throw ex;
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
        public Int32 PortAgentVehicleDashboardDetailsCount(int portAgentId, int BranchId, int BrandId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return dbDAL.PortAgentVehicleDashboardDetailsCount(portAgentId, BranchId, BrandId, startDate, endDate);
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
            try
            {
                return dbDAL.PortAgentVehicleDashboardDetails(portAgentId, BranchId, BrandId, startDate, endDate, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 16/11/2011
        /// Description: load vehicle with transactions by port agent
        /// </summary>
        /// <param name="portAgentId"></param>
        /// <returns></returns>
        public DataTable loadVehicleBrandByPort(string portAgentId)
        {
            try
            {
                return dbDAL.loadVehicleBrandByPort(Int32.Parse(portAgentId));
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                return dbDAL.loadVehicleBranchByPort(vendorBrandId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/11/2011
        /// Description: get port agent hotel dasgboard details
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="BrandId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable HotelDashboardbyDate(int BranchId, int BrandId, int RegionId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return dbDAL.HotelDashboardbyDate(BranchId, BrandId, RegionId, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/11/2011
        /// Description: get hotel dashboard details by status
        /// ----------------------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 13/12/2011
        /// Description: added parameters startRownIndex and maximumRows
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="BranchId"></param>
        /// <param name="Status"></param>
        /// <param name="cDate"></param>
        /// <param name="RoomType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public DataTable HotelDashboardDetailsbyStatus(DateTime cDate, int BranchId, int BrandId, 
            Int32 startRowIndex, Int32 maximumRows)
        {
            try
            {
                return dbDAL.HotelDashboardDetailsbyStatus(BrandId, BranchId,  cDate, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                return dbDAL.HotelDashboardDetailsbyStatusCount(BranchId, BrandId, cDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/11/2011
        /// Description:    get hotel dashboard room types by status
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
            try
            {
                return dbDAL.getHotelDashboardRoomType(RegionID, CountryID, CityID,
                    UserName, UserRole, BranchID, DateFrom, DateTo, BranchName, StartRow, MaxRow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/11/2011
        /// Description:    get hotel dashboard room types Total Count
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
            try
            {
                return dbDAL.getHotelDashboardRoomTypeCount(RegionID, CountryID, CityID,
                    UserName, UserRole, BranchID, DateFrom, DateTo, BranchName);
            }
            catch (Exception ex)
            {
                throw ex;
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
        public int SaveHotelOverride(string HotelRoomId, string BranchId, string EffectiveDate, string rate, string Currency,
            string TaxRate, bool taxInclusive, string RoomType, string RoomBlocks, int AddedRooms, string userId)
        {
          
            try
            {
                int RId = Int32.Parse(HotelRoomId);
                int bId = Int32.Parse(BranchId);
                DateTime eDate = DateTime.Parse(EffectiveDate);
                int curr = Int32.Parse(Currency);
                int rType = Int32.Parse(RoomType);
                int rBlocks = Int32.Parse(RoomBlocks);
               
                return dbDAL.SaveHotelOverride(RId, bId, eDate, rate, curr, TaxRate,
                    taxInclusive, rType, rBlocks, AddedRooms, userId);
            }
            catch (Exception ex)
            {
               
                throw ex;
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
        public IDataReader  getOverrideDetails(string BranchId, string HotelRoomId, string RoomType)
        {
           
            try
            {
                int branch = GlobalCode.Field2Int(BranchId);
                int hrId = GlobalCode.Field2Int( HotelRoomId);
                int rType = GlobalCode.Field2Int(RoomType);
                return dbDAL.getOverrideDetails(branch, hrId, rType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/11/2011
        /// Description: get port agent hotel dasgboard details
        /// </summary>
        /// <param name="BranchId"></param>
        /// <param name="BrandId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable HotelDashboardStatfromContract(int BranchId, int BrandId, DateTime startDate)
        {
            try
            {
                return dbDAL.HotelDashboardStatfromContract(BranchId, BrandId, startDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DashboardDAL HotelDashboardDAL = new DashboardDAL();
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/02/2012
        /// Description: get all hotel dashboard 2 tables
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="MaximumRows"></param>
        //public void LoadAllHotelDashboard2Tables(DateTime StartDate,
        //    Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows)
        //{
        //    try
        //    {
        //        List<HotelDashBoardGenericClass> HotelDashboardList = new List<HotelDashBoardGenericClass>();

        //        HotelDashboardList = HotelDashboardDAL.LoadAllHotelDashboard2Tables(StartDate, BranchId, LoadType, UserId,
        //            StartRowIndex, MaximumRows);

        //        if (HotelDashboardList.Count > 0)
        //        {
        //            HotelDashboardClass.EventCount = HotelDashboardList[0].EventCount;
        //            HotelDashboardClass.ConfirmBookingCount = HotelDashboardList[0].ConfirmBookingCount;
        //            HotelDashboardClass.ConfirmBooking = HotelDashboardList[0].ConfirmBooking;
        //            HotelDashboardClass.HotelRoomBlocks = HotelDashboardList[0].HotelRoomBlocks;
        //            HotelDashboardClass.PendingBookingCount = HotelDashboardList[0].PendingBookingCount;
        //            //HotelDashboardClass.PendingBooking = HotelDashboardList[0].PendingBooking;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

		/// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/02/2012
        /// Description: get all hotel dashboard 2 tables
        /// -------------------------------------------------------------------------------------
        /// Modfied by:     Gabriel Oquialda
        /// Date Modified:  13/03/2012
        /// Description:    This is a modified 'LoadAllHotelDashboard2Tables2' copy for new screens
        /// -------------------------------------------------------------------------------------
        /// Modfied by:     Josephine Gad
        /// Date Modified:  19/10/2012
        /// Description:    Change Class to Session
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="UserId"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="MaximumRows"></param>
        public void LoadAllHotelDashboard2Tables2(DateTime StartDate,
            Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows)
        {
            try
            {
                List<HotelDashBoardGenericClass> HotelDashboardList = new List<HotelDashBoardGenericClass>();

                HotelDashboardList = HotelDashboardDAL.LoadAllHotelDashboard2Tables2(StartDate, BranchId, LoadType, UserId,
                    StartRowIndex, MaximumRows);

                if (LoadType == 4)
                {
                    if (HotelDashboardList.Count > 0)
                    {
                        //HotelDashboardClass.HotelRoomBlocks = HotelDashboardList[0].HotelRoomBlocks;
                        HttpContext.Current.Session["HotelDashboardClass_HotelRoomBlocks"] = HotelDashboardList[0].HotelRoomBlocks;
                    }
                }
                else
                {
                    if (HotelDashboardList.Count > 0)
                    {
                        //HotelDashboardClass.EventCount = HotelDashboardList[0].EventCount;
                        //HotelDashboardClass.ConfirmBookingCount = HotelDashboardList[0].ConfirmBookingCount;
                        //HotelDashboardClass.ConfirmBooking = HotelDashboardList[0].ConfirmBooking;
                        //HotelDashboardClass.HotelRoomBlocks = HotelDashboardList[0].HotelRoomBlocks;
                        //HotelDashboardClass.PendingBookingCount = HotelDashboardList[0].PendingBookingCount;
                        //HotelDashboardClass.PendingBooking = HotelDashboardList[0].PendingBooking;

                        HttpContext.Current.Session["HotelDashboardClass_EventCount"] = HotelDashboardList[0].EventCount;
                        HttpContext.Current.Session["HotelDashboardClass_ConfirmBookingCount"] = HotelDashboardList[0].ConfirmBookingCount;
                        HttpContext.Current.Session["HotelDashboardClass_ConfirmBooking"] = HotelDashboardList[0].ConfirmBooking;

                        HttpContext.Current.Session["HotelDashboardClass_HotelRoomBlocks"] = HotelDashboardList[0].HotelRoomBlocks;
                        HttpContext.Current.Session["HotelDashboardClass_PendingBookingCount"] = HotelDashboardList[0].PendingBookingCount;
                        HttpContext.Current.Session["HotelDashboardClass_PendingBooking"] = HotelDashboardList[0].PendingBooking;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modfied by:     Josephine Gad
        /// Date Modified:  19/10/2012
        /// Description:    Get list from session on first load
        /// /// ---------------------------------------------
        /// Date Created:   15/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Add sorting         
        /// </summary>
        public List<ConfirmBooking> LoadHotelDashboardconfirmedTable(DateTime StartDate,
          Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows, Int16 FromDefaultView, String SortBy)
        {
            List<ConfirmBooking> ConfirmBooking = new List<ConfirmBooking>();

            if (FromDefaultView == 0)
            {
                ConfirmBooking = (List<ConfirmBooking>)HttpContext.Current.Session["HotelDashboardClass_ConfirmBooking"];
            }
            else
            {
                int countROw = 0;
                ConfirmBooking = HotelDashboardDAL.LoadHotelDashboardconfirmedTable(StartDate,
                    BranchId, LoadType, UserId, StartRowIndex, MaximumRows, out countROw, SortBy);

                HttpContext.Current.Session["HotelDashboardClass_ConfirmBookingCount"] = countROw;
                //HotelDashboardClass.ConfirmBookingCount = countROw;
                //HotelDashboardClass.ConfirmBooking = ConfirmBooking;
            }
            return ConfirmBooking;
        }
        
        public int LoadHotelDashboardconfirmedTableCount(DateTime StartDate,
          Int32 BranchId, Int16 LoadType, String UserId, Int16 FromDefaultView, String SortBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["HotelDashboardClass_ConfirmBookingCount"]);
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
            return DashboardDAL.LoadHotelDashboardConfirmedExport(UserId, BranchId);
        }
       
        public static DataTable LoadHotelDashboardOverflowExport(String UserId)
        {
            return DashboardDAL.LoadHotelDashboardOverflowExport(UserId);
        }
        /// <summary>
        /// Modfied by:     Josephine Gad
        /// Date Modified:  19/10/2012
        /// Description:    Get list from session on first load
        /// </summary>
        public List<OverflowBooking> LoadHotelDashboardPendingTable(DateTime StartDate,
          Int32 BranchId, Int16 LoadType, String UserId, Int32 StartRowIndex, Int32 MaximumRows, Int16 FromDefaultView,
            string SortBy)
        {
            List<OverflowBooking> PendingBooking = new List<OverflowBooking>();
            if (FromDefaultView == 0)
            {
                PendingBooking = (List<OverflowBooking>)HttpContext.Current.Session["HotelDashboardClass_PendingBooking"];
            }
            else
            {
                int countROw = 0;
                PendingBooking = HotelDashboardDAL.LoadHotelDashboardPendingTable(StartDate,
                    BranchId, LoadType, UserId, StartRowIndex, MaximumRows, out countROw, SortBy);
                HttpContext.Current.Session["HotelDashboardClass_PendingBookingCount"] = countROw;
                //HotelDashboardClass.PendingBooking = PendingBooking;
            }
            return PendingBooking;
        }

        public int LoadHotelDashboardPendingTableCount(DateTime StartDate,
          Int32 BranchId, Int16 LoadType, String UserId, Int16 FromDefaultView, string SortBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["HotelDashboardClass_PendingBookingCount"]);
        }
        #endregion
    }
}

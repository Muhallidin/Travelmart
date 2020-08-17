using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;
namespace TRAVELMART.BLL
{
    public class MasterfileBLL
    {
        public MasterfileDAL masterDAL = new MasterfileDAL();

        #region Country BLL
        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) View list of countries by region (used in CountryView)
        /// </summary>

        public DataTable MasterfileCountryViewByRole(string RoleID, string CountryName, Int32 startRowIndex, Int32 maximumRows)
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = masterDAL.MasterfileCountryViewByRole(RoleID, CountryName, startRowIndex, maximumRows);
                return CountryDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Count list of countries
        /// </summary>
        public Int32 MasterfileCountryViewByRoleCount(string RoleID, string CountryName)
        {
            try
            {
                return masterDAL.MasterfileCountryViewByRoleCount(RoleID, CountryName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Delete country
        /// </summary>
        public void MasterfileCountryDelete(int CountryId, string ModifiedBy)
        {
            try
            {
                masterDAL.MasterfileCountryDelete(CountryId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Country by region (used in countryView)
        /// </summary>
       
        public DataTable SearchCountryListByRegion(string RegionID, string CountryName, Int32 startRowIndex, Int32 maximumRows)
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = masterDAL.GetSearchCountryListByRegion(RegionID, CountryName,startRowIndex, maximumRows);
                return CountryDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count country 
        /// </summary>
        public Int32 GetSearchCountryListByRegionCount(string RegionID, string CountryName)
        {
            try
            {
                return masterDAL.GetSearchCountryListByRegionCount(RegionID, CountryName);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }


        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Add new country
        /// </summary>
        public static Int32 countryAddMasterfileInsert(string countryCode, string CountryName, string CreatedBy)
        {
            Int32 CountryID = 0;
            CountryID = MasterfileDAL.countryAddMasterfileInsert(countryCode, CountryName, CreatedBy);
            return CountryID;

            //try
            //{
            //    Int32 CountryID = 0;
            //    CountryID = MasterfileDAL.countryAddMasterfileInsert(countryCode, CountryName, CreatedBy);
            //    return CountryID;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Update Country
        /// </summary>
        /// 
        public static void countryAddMasterfileUpdate(int regionId, int countryId, string countryName, string countryCode, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.countryAddMasterfileUpdate(regionId, countryId, countryName, countryCode, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Delete Country
        /// </summary>
        public void CountryViewMasterfileDelete(int CountryId, string ModifiedBy)
        {
            try
            {
                masterDAL.countryViewMasterfileDelete(CountryId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Region BLL
        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Count list of regions
        /// ----------------------------------
        /// Date Modified: 06/Feb/2012
        /// Modified By:   Josephine Gad
        /// (description)  Count list of regions
        /// </summary>
        public Int32 MasterfileRegionViewCount(string regionName)
        {
            try
            {
                //return masterDAL.MasterfileRegionViewCount(regionName);
                return GlobalCode.Field2Int(HttpContext.Current.Session["RegionViewTotalCount"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) View list of regions
        /// </summary>
        public DataTable MasterfileRegionView(string regionName, Int32 startRowIndex, Int32 maximumRows)
        {
            try
            {
                return masterDAL.MasterfileRegionView(regionName, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Delete region
        /// </summary>
        public static void MasterfileRegionDelete(int regionId, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.MasterfileRegionDelete(regionId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Insert region
        /// </summary>
        public static Int32 MasterfileRegionInsert(string regionName, string CreatedBy)
        {
            try
            {
                Int32 RegionID = 0;
                RegionID = MasterfileDAL.MasterfileRegionInsert(regionName, CreatedBy);
                return RegionID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Created: 25/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Update Region
        /// </summary>
        /// 
        public static void MasterfileRegionUpdate(int regionId, string regionName, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.MasterfileRegionUpdate(regionId, regionName, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:19/10/2011
        /// Created By: Charlene Remotigue
        /// (description) View list of regions
        /// </summary>
        public Int32 regionViewMasterfileSearchCount(string regionName)
        {
            try
            {
                return masterDAL.regionViewMasterfileSearchCount(regionName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Search for region
        /// </summary>

        public DataTable regionViewMasterfileSearch(string regionName, Int32 startRowIndex, Int32 maximumRows)
        {
            try
            {
                return masterDAL.regionViewMasterfileSearch(regionName, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Insert Region
        /// </summary>
        public static void regionAddMasterfileInsert(string regionName, string CreatedBy)
        {
            try
            {
                MasterfileDAL.regionAddMasterfileInsert(regionName, CreatedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Update Region
        /// </summary>
        /// 
        public static void regionAddMasterfileUpdate(int regionId, string regionName, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.regionAddMasterfileUpdate(regionId, regionName, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Delete Region
        /// </summary>
        public static void regionAddMasterfileDelete(int regionId, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.regionAddMasterfileDelete(regionId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveRegionSeaport(DataTable dt, string UserName, int RegionId,
            string RegionName, string sDescription, string sFunction, string sFileName, DateTime GMTDate, DateTime Now)
        {
            MasterfileDAL.SaveRegionSeaport(dt, UserName, RegionId, RegionName, sDescription, sFunction, sFileName, GMTDate, Now);
        }
        #endregion

        #region City BLL
        /// <summary>
        /// Date Created: 26/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) View list of cities by country                
        /// </summary>
        public DataTable MasterfileCityView(string pCountryId, string RoleID, string pCityName, Int32 startRowIndex, Int32 maximumRows)
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = masterDAL.MasterfileCityView(pCountryId, RoleID, pCityName, startRowIndex, maximumRows);
                return CountryDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 26/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Count list of cities by country
        /// </summary>
        public Int32 MasterfileCityViewCount(string pCountryId, string RoleID, string pCityName)
        {
            try
            {
                return masterDAL.MasterfileCityViewCount(pCountryId, RoleID, pCityName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 26/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Delete City
        /// </summary>
        public static void MasterfileCityDelete(string pCityId, string pModifiedBy)
        {
            try
            {
                MasterfileDAL.MasterfileCityDelete(pCityId, pModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Select City by Country 
        /// ------------------------------------
        /// Date Modified: 11/10/2011
        /// Modified by: Charlene Remotigue
        /// Description: set paging
        /// </summary>
        public DataTable cityViewMasterfileSearch(string pCountryId, string pCityName, Int32 startRowIndex, Int32 maximumRows)
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = masterDAL.cityViewMasterfileSearch(pCountryId, pCityName, startRowIndex, maximumRows);
                return CountryDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count City 
        /// </summary>
        public Int32 CityViewMasterfileSearchCount(string pCountryId, string pCityName)
        {
            try
            {
                return masterDAL.CityViewMasterfileSearchCount(pCountryId, pCityName);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Add new city
        /// </summary>
        public static Int32 cityAddMasterfileInsert(int CountryId, string CityCode, string CityName, string CreatedBy)
        {
            try
            {
                Int32 CityID = 0;
                CityID = MasterfileDAL.cityAddMasterfileInsert(CountryId, CityCode, CityName, CreatedBy);
                return CityID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Update City
        /// </summary>
        public static void cityAddMasterfileUpdate(int CountryId, int CityId, string CityName, string CityCode, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.cityAddMasterfileUpdate(CountryId, CityId, CityName, CityCode, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Descrition: Delete City
        /// </summary>
        public static void CityViewMasterfileDelete(string pCityId, string pModifiedBy)
        {
            try
            {
                MasterfileDAL.cityViewMasterfileDelete(pCityId, pModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PortAgentSeaport
        /// <summary>
        /// Date Created: 05/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Service Provider Seaport 
        /// </summary>
        public static DataTable PortListMaintenanceSearchAgentSeaport(string portAgentId, string CityId, Int32 startRowIndex, Int32 maximumRows)
        {
            try
            {
                return MasterfileDAL.PortListMaintenanceSearchAgentSeaport(portAgentId, CityId, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Service Provider Seaport 
        /// </summary>
        public Int32 PortListMaintenanceSearchAgentSeaportCount(string portAgentId, string CityId)
        {
            try
            {
                return masterDAL.PortListMaintenanceSearchAgentSeaportCount(portAgentId, CityId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created: 07/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Select Seaport 
        /// </summary>
        public static DataTable PortListMaintenanceSearch(int CityId, int PortAgentId, bool loadAll)
        {
            try
            {
                return MasterfileDAL.PortListMaintenanceSearch(CityId, PortAgentId, loadAll);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Add new Service Provider seaport
        /// </summary>
        public static Int32 portListMaintenanceInsert(int portAgentId, int PortId, string CreatedBy)
        {
            try
            {
                Int32 PortAgentSeaportID = 0;
                PortAgentSeaportID = MasterfileDAL.PortListMaintenanceInsert(portAgentId, PortId, CreatedBy);
                return PortAgentSeaportID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Service Provider Seaport
        /// </summary>
        public static void portListMaintenanceDelete(int portAgentSeaportId, string ModifiedBy)
        {
            try
            {
                MasterfileDAL.portListMaintenanceDelete(portAgentSeaportId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Sail Master
        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count vessels 
        /// </summary>
        public Int32 SailMasterMaintenanceVesselSearchCount(string pVesselName, bool viewAll)
        {
            try
            {
                return masterDAL.SailMasterMaintenanceVesselSearchCount(pVesselName, viewAll);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load all vessels 
        /// </summary>
        public DataTable SailMasterMaintenanceVessel(string pVesselName,  bool viewAll, Int32 maximumRows, Int32 startRowIndex)
        {
            try
            {
                return masterDAL.SailMasterMaintenanceVesselSearch(pVesselName, viewAll, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count SailMaster 
        /// </summary>
        public Int32 SailMasterAddMaintenanceSearchCount(string pVesselId)
        {
            try
            {
                return masterDAL.SailMasterAddMaintenanceSearchCount(pVesselId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load all SailMasters 
        /// </summary>
        public DataTable SailMasterAddMaintenanceSearch(string pVesselId, Int32 maximumRows, Int32 startRowIndex)
        {
            try
            {
                return masterDAL.SailMasterAddMaintenanceSearch(pVesselId, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created:11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load sail master details
        /// </summary>
        public IDataReader SailMasterAddMaintenanceLoadDetails(int sailMasterId)
        {
            try
            {
                return masterDAL.SailMasterAddMaintenanceLoadDetails(sailMasterId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }

        }

        /// <summary>
        /// Date Created:04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Save sail master
        /// </summary>
        public DataTable SailMasterAddMaintenanceSave(int sailMasterId, int PortId, int VesselId,
                   string itineraryCode, string voyageNo, int DaySeq, DateTime ScheduleDate, DateTime dateFrom, DateTime dateTo, string UserId)
        {
            try
            {
                DataTable tempDT = null;
                return tempDT = masterDAL.SailMasterAddMaintenanceSave(sailMasterId, PortId, VesselId,
                    itineraryCode, voyageNo, DaySeq, ScheduleDate, dateFrom, dateTo, UserId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        /// <summary>
        /// Date Created:11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Sail Master
        /// </summary>
        public static void SailMasterViewDelete(string pSailMasterId, string pModifiedBy)
        {
            try
            {
                MasterfileDAL.SailMasterViewDelete(pSailMasterId, pModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select

        /// <summary>
        /// Date Created:  03/10/2011
        /// Created By:    Josephine Gad
        /// (description)  Selecting list of reference by code              
        /// </summary>
        /// <param name="RefCode"></param>
        /// <returns></returns>
        public static DataTable GetReference(string RefCode)
        {
            DataTable dt = null;
            try
            {
                dt = MasterfileDAL.GetReference(RefCode);
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
        /// Date Created:  26/Nov/2011
        /// Created By:    Josephine Gad
        /// (description)  Get the list of nationality, gender, rank, vessel and hotel in forecast page
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="RegionID"></param>
        /// <param name="PortID"></param>
        /// <param name="AirportID"></param>
        /// <returns></returns>
        //public void ForecastGetFilters(string sUser, int RegionID, int PortID, int AirportID)
        //{
        //    HttpContext.Current.Session["Forecast_Nationality"] = null;
        //    HttpContext.Current.Session["Forecast_Gender"] = null;
        //    HttpContext.Current.Session["Forecast_Rank"] = null;
        //    HttpContext.Current.Session["Forecast_Vessel"] = null;
        //    HttpContext.Current.Session["Forecast_Hotel"] = null;

        //    List<ForeCastFilters> list = new List<ForeCastFilters>();
        //    list = masterDAL.fo (sUser, RegionID, PortID, AirportID);
        //    if (list.Count > 0)
        //    {
        //        HttpContext.Current.Session["Forecast_Nationality"] = list[0].NationalityList;
        //        HttpContext.Current.Session["Forecast_Gender"] = list[0].GenderList;
        //        HttpContext.Current.Session["Forecast_Rank"] = list[0].RankList;
        //        HttpContext.Current.Session["Forecast_Vessel"] = list[0].VesselList;
        //        HttpContext.Current.Session["Forecast_Hotel"] = list[0].HotelList;
        //    }
        //}
        #endregion

        #region Events
        /// <summary>
        /// Date Created:12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load events
        ///-----------------------------------------------
        /// Date Created:12/02/2011
        /// Created By: Charlene Remotigue
        /// Description: load events
        ///-----------------------------------------------
        /// Date Modified:  13/06/2012
        /// Modified By:    Josephine Gad
        /// Description:    Change void to List<Events>
        /// </summary>
        /// <param name="CityId"></param>
        /// <param name="BranchId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="viewAll"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        public List<Events> EventsViewLoadEvents(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
            int BranchId, int LoadType, int startRowIndex, int maximumRows)
        {
            try
            {
                List<Events> list = new List<Events>();
                List<MaintenanceGenericClass> MaintenanceList = new List<MaintenanceGenericClass>();

                MaintenanceList = masterDAL.EventsViewLoadEvents(UserId, DateFrom, DateTo, RegionId, CountryId,
                    BranchId, LoadType, startRowIndex, maximumRows);

                Maintenance.EventCount = 0;
                if (MaintenanceList.Count > 0)
                {
                    Maintenance.EventCount = MaintenanceList[0].EventCount;
                    //Maintenance.Events = MaintenanceList[0].Events;
                    list = MaintenanceList[0].Events;
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   13/12/2012
        /// Description:    return count of Events List
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public int EventsViewLoadEventsCount(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
           int BranchId, int LoadType)
        {
            return GlobalCode.Field2Int(Maintenance.EventCount);            
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 22/02/2012
        /// Description: get events paging
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        //public List<Events> EventsViewLoadEventsPage(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
        //   int BranchId, int LoadType, int startRowIndex, int maximumRows)
        //{
        //    try
        //    {
        //        int EventCount =0;

        //        List<Events> EventList = new List<Events>();
        //        EventList = masterDAL.EventsViewLoadEventsPage(UserId, DateFrom, DateTo, RegionId, CountryId,
        //            BranchId, LoadType, startRowIndex, maximumRows, out EventCount);

        //        Maintenance.EventCount = EventCount;

        //        return EventList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
           
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 22/02/2012
        /// Description: get events paging count
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BranchId"></param>
        /// <param name="LoadType"></param>
        /// <returns></returns>
        //public int EventsViewLoadEventsPageCount(string UserId, DateTime DateFrom, DateTime DateTo, int RegionId, int CountryId,
        //   int BranchId, int LoadType)
        //{
        //    try
        //    {                
        //        return GlobalCode.Field2Int(Maintenance.EventCount);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: count Events 
        /// </summary>
        //public Int32 EventsViewLoadEventsCount(string CityId, string BranchId, DateTime DateFrom, DateTime DateTo, bool viewAll)
        //{
        //    try
        //    {               
        //        return masterDAL.EventsViewLoadEventsCount(CityId,BranchId,DateFrom,DateTo,viewAll);
        //    }
        //    catch (Exception ex)
        //    { 
        //        throw ex; 
        //    }
        //}

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Delete Event
        /// </summary>
        public void EventsViewMaintenanceDelete(string EventId, string ModifiedBy)
        {
            try
            {
                masterDAL.EventsViewMaintenanceDelete(EventId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:03/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Tag Event as done
        /// </summary>
        public void EventsViewMaintenanceTag(string EventId, string ModifiedBy)
        {           
            try
            {
                masterDAL.EventsViewMaintenanceTag(EventId, ModifiedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        /// <summary>
        /// Date Created:12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load event details
        /// </summary>
        public IDataReader EventsAddMaintenanceLoadDetails(string EventId)
        {
            try
            {
                return masterDAL.EventsAddMaintenanceLoadDetails(EventId);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }

        }

        /// <summary>
        /// Date Created:04/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Save event
        /// </summary>
        public static Int32 EventsAddMaintenanceSave(string EventId, string EventName, DateTime DateFrom,
            DateTime DateTo, string BranchId, string CityId, string UserId, string Remarks)
        {
           try
            {
                Int32 pEventID = 0;
                pEventID = MasterfileDAL.EventsAddMaintenanceSave(EventId, EventName, DateFrom,
                    DateTo, BranchId, CityId, UserId, Remarks);
                return pEventID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        #endregion

        #region DELETED
        ///// <summary>
        ///// Date Created:30/09/2011
        ///// Created By: Charlene Remotigue
        ///// Description: get region name (used in CountryAdd)
        ///// </summary>
        ///// 
        //public static string getRegionName(int RegionID)
        //{

        //    try
        //    {
        //        return MasterfileDAL.getRegionName(RegionID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        ///// <summary>
        ///// Date Created:30/09/2011
        ///// Created By: Charlene Remotigue
        ///// Description: get country anf region name 
        ///// </summary>
        ///// 
        //public static void getCountryRegionName(int CountryId)
        //{

        //    try
        //    {
        //        MasterfileDAL.getCountryRegionName(CountryId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        #endregion

        /// <summary>
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetAirportList(bool IsSearchByCode, string sFilter)
        {
            return MasterfileDAL.GetAirportList(IsSearchByCode, sFilter);
        }
         /// Date Created:  14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<SeaportDTO> GetSeaportList(bool IsSearchByCode, string sFilter)
        {
            return MasterfileDAL.GetSeaportList(IsSearchByCode, sFilter);

        }
        /// <summary>
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Vessel Brand List
        /// ----------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<BrandList> GetBrandList()
        {
            return MasterfileDAL.GetBrandList();
        }
          /// <summary>
        /// Date Created:   19/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get Vessel By Brand
        /// ----------------------------------------------
        /// </summary>
        /// <returns></returns>
        public List<VesselList> GetVesselList(string sBrandID)
        {
            return MasterfileDAL.GetVesselList(sBrandID);
        }
         /// <summary>
        /// Date Created:   25/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Get Nationality List
        /// ----------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static List<NationalityList> GetNationalityList(string sFilter, string sSortedBy, int iStartRow, int iMaxRow)
        {
            return MasterfileDAL.GetNationalityList(sFilter, sSortedBy, iStartRow, iMaxRow);
        }
        public int GetNationalityCount(string sFilter, string sSortedBy)
        {
            int i = GlobalCode.Field2Int(HttpContext.Current.Session["NationalityList_Count"]);
            return i;
        }
         /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   25/Nov/2014
        /// Description:    Update Nationality OK To Brazil
        /// ---------------------------------------------------------------
        /// </summary>
        public void UpdateNationalityOkTB(string UserId, int iNationalityID,   bool IsOKTB,       
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            masterDAL.UpdateNationalityOkTB(UserId, iNationalityID,  IsOKTB,       
             strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   09/Jul/2015
        /// Description:    Insert Timezone in SQL DB
        /// ---------------------------------------------------------------
        /// </summary>
        public void TimezoneInsert(string sTimeZoneName, string sStandardName, double fUTCOffset, string sCreatedBy)
        {
            masterDAL.TimezoneInsert(sTimeZoneName, sStandardName, fUTCOffset, sCreatedBy);
        }
    }
}

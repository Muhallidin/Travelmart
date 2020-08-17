using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using System.Data;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.BLL
{
    public class ManifestBLL
    {
        ManifestDAL DAL = new ManifestDAL();

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/02/2012
        /// Descrption:     send all manifest queries to list
        /// </summary>
        /// <param name="LoadType"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="UserID"></param>
        /// <param name="Role"></param>
        /// <param name="OrderBy"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <param name="VesselID"></param>
        /// <param name="FilterByName"></param>
        /// <param name="SeafarerID"></param>
        /// <param name="NationalityID"></param>
        /// <param name="Gender"></param>
        /// <param name="RankID"></param>
        /// <param name="Status"></param>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        public void LoadAllManifestTables(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID, string ViewNoHotelOnly)
        {
            try
            {
                List<ManifestDTOGenericClass> ManifestTables = new List<ManifestDTOGenericClass>();

                ManifestTables = DAL.LoadAllManifestTables(LoadType, FromDate, ToDate,
                UserID,  Role,  OrderBy,  StartRow, MaxRow, VesselID,  FilterByName,
                SeafarerID,  NationalityID,  Gender,  RankID,  Status,
                RegionID, CountryID, CityID, PortID, HotelID, ViewNoHotelOnly);

                ManifestDTO.ManifestList = ManifestTables[0].ManifestList;
                ManifestDTO.ManifestListCount = ManifestTables[0].ManifestListCount;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ManifestList> LoadManifestList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID, string ViewNoHotelOnly)
        {
            List<ManifestDTOGenericClass> ManifestListTable = new List<ManifestDTOGenericClass>();
            
            ManifestListTable = DAL.LoadAllManifestTables(LoadType, FromDate, ToDate,
            UserID, Role, OrderBy, StartRow, MaxRow, VesselID,  FilterByName,
             SeafarerID,  NationalityID,  Gender,  RankID,  Status,
             RegionID, CountryID, CityID, PortID, HotelID, ViewNoHotelOnly);

            ManifestDTO.ManifestList = ManifestListTable[0].ManifestList;
            ManifestDTO.ManifestListCount = ManifestListTable[0].ManifestListCount;
            return ManifestListTable[0].ManifestList;            
        }

        public int LoadManifestListCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID, string ViewNoHotelOnly)
        {
            return GlobalCode.Field2Int(ManifestDTO.ManifestListCount);
        }        

        /// <summary>
        /// Author:         Gelo Oquialda
        /// Date Created:   08/02/2012
        /// Descrption:     send all manifest search view queries to list
        /// </summary>
        /// <param name="LoadType"></param>        
        /// <param name="UserID"></param>
        /// <param name="Role"></param>
        /// <param name="OrderBy"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <param name="SeafarerID"></param>
        /// <param name="SeafarerLN"></param>
        /// <param name="SeafarerFN"></param>
        /// <param name="RecordLocator"></param>       
        /// <param name="VesselCode"></param>
        /// <param name="VesselName"></param> 
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <param name="CityID"></param>
        /// <param name="PortID"></param>
        /// <param name="HotelID"></param>
        /// <returns></returns>
        public void LoadAllManifestSearchViewTables(Int16 LoadType, DateTime CurrentDate, string UserID, string Role, string OrderBy,
            int StartRow, int MaxRow, string SeafarerID, string SeafarerLN, string SeafarerFN, string RecordLocator, string VesselCode,
            string VesselName, int RegionID, int CountryID, int CityID, int PortID, int HotelID, bool IsShowAll)
        {
            try
            {
                List<ManifestSearchViewDTOGenericClass> ManifestSearchViewTables = new List<ManifestSearchViewDTOGenericClass>();

                ManifestSearchViewTables = DAL.LoadAllManifestSearchViewTables(LoadType, CurrentDate, UserID, Role, OrderBy, StartRow,
                    MaxRow, SeafarerID, SeafarerLN, SeafarerFN, RecordLocator, VesselCode, VesselName, RegionID,
                CountryID, CityID, PortID, HotelID, IsShowAll);

                ManifestSearchViewDTO.ManifestSearchViewList = ManifestSearchViewTables[0].ManifestSearchViewList;
                ManifestSearchViewDTO.ManifestSearchViewListCount = ManifestSearchViewTables[0].ManifestSearchViewListCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modified By:        Josephine Gad
        /// Date Modified:      17/May/2013
        /// Descrption:         Add Session["ManifestSearchIsAddRequest"]
        /// ----------------------------------------
        /// Modified By:        Josephine Gad
        /// Date Modified:      25/June/2013
        /// Descrption:         Remove Session["ManifestSearchIsAddRequest"] and change to IsAddRequestVisible in the returned list
        ///                     Add IsShowAll to show past dated records
        /// </summary>
        public List<ManifestSearchViewList> LoadManifestSearchViewList(Int16 LoadType, DateTime CurrentDate, string UserID, string Role,
            string OrderBy, int StartRow, int MaxRow, string SeafarerID, string SeafarerLN, string SeafarerFN,
            string RecordLocator, string VesselCode, string VesselName, int RegionID, int CountryID, int CityID,
            int PortID, int HotelID, bool IsShowAll)
        {
            List<ManifestSearchViewDTOGenericClass> ManifestSearchViewListTable = new List<ManifestSearchViewDTOGenericClass>();

            ManifestSearchViewListTable = DAL.LoadAllManifestSearchViewTables(LoadType, CurrentDate, UserID, Role, OrderBy, StartRow,
                MaxRow, SeafarerID, SeafarerLN, SeafarerFN, RecordLocator, VesselCode, VesselName, RegionID, CountryID, CityID, PortID, HotelID, IsShowAll);

            ManifestSearchViewDTO.ManifestSearchViewList = ManifestSearchViewListTable[0].ManifestSearchViewList;
            ManifestSearchViewDTO.ManifestSearchViewListCount = ManifestSearchViewListTable[0].ManifestSearchViewListCount;

            //HttpContext.Current.Session["ManifestSearchIsAddRequest"] = ManifestSearchViewListTable[0].IsAddRequestVisible;
            return ManifestSearchViewListTable[0].ManifestSearchViewList;
        }

        public int LoadManifestSearchViewListCount(Int16 LoadType, DateTime CurrentDate, string UserID, string Role, string OrderBy,
            string SeafarerID, string SeafarerLN, string SeafarerFN, string RecordLocator, string VesselCode, string VesselName, int RegionID, int CountryID,
            int CityID, int PortID, int HotelID, bool IsShowAll)
        {
            return GlobalCode.Field2Int(ManifestSearchViewDTO.ManifestSearchViewListCount);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   22/02/2012
        /// Descrption:     Get tentative manifest list and total row count
        /// ------------------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  05/10/2012
        /// Description:    Delete ManifestDTO.TentativeManifestList
        ///                 Change ManifestDTO.TentativeManifestCount to Session
        /// ------------------------------------------------------------------
        /// </summary>
        /// <param name="DateFromString"></param>
        /// <param name="NameString"></param>
        /// <param name="strUser"></param>
        /// <param name="DateFilter"></param>
        /// <param name="ByNameOrID"></param>
        /// <param name="filterNameOrID"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="Status"></param>
        /// <param name="Region"></param>
        /// <param name="Country"></param>
        /// <param name="City"></param>
        /// <param name="Port"></param>
        /// <param name="Hotel"></param>
        /// <param name="Vessel"></param>
        /// <param name="UserRole"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <param name="LoadType"></param>
        /// <returns></returns>
        public List<TentativeManifest> GetTentativeManifestList(string DateFromString,
          string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
          string Nationality, string Gender, string Rank, string Status,
          string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole
             ,int StartRow, int MaxRow , Int16 LoadType)
        {
            List<TentativeManifestGenericClass> ManifestTables = new List<TentativeManifestGenericClass>();

            ManifestTables = DAL.GetTentativeManifestList(DateFromString,
            strUser, DateFilter, ByNameOrID, filterNameOrID,
            Nationality, Gender, Rank, Status, Region, Country, City,
            Port, Hotel, Vessel, UserRole, StartRow, MaxRow, LoadType);

            //ManifestDTO.TentativeManifestList = ManifestTables[0].TentativeManifestList;
            //ManifestDTO.TentativeManifestCount = ManifestTables[0].TentativeManifestCount;
            HttpContext.Current.Session["ManifestDTO_TentativeManifestCount"] = ManifestTables[0].TentativeManifestCount;
            return ManifestTables[0].TentativeManifestList;
        }

        public List<TentativeManifest> GetTentativeManifestList2(string DateFromString,
         string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
         string Nationality, string Gender, string Rank, string Status,
         string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole
           , Int16 LoadType, string SortBy, int StartRow, int MaxRow)
        {
            List<TentativeManifestGenericClass> ManifestTables = new List<TentativeManifestGenericClass>();

            ManifestTables = DAL.GetTentativeManifestList2(DateFromString,
            strUser, DateFilter, ByNameOrID, filterNameOrID,
            Nationality, Gender, Rank, Status, Region, Country, City,
            Port, Hotel, Vessel, UserRole, StartRow, MaxRow, LoadType, SortBy);

            //ManifestDTO.TentativeManifestList = ManifestTables[0].TentativeManifestList;
            //ManifestDTO.TentativeManifestCount = ManifestTables[0].TentativeManifestCount;
            HttpContext.Current.Session["ManifestDTO_TentativeManifestCount"] = ManifestTables[0].TentativeManifestCount;
            return ManifestTables[0].TentativeManifestList;
        }

        public int GetTentativeManifestCount(string DateFromString,
         string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
         string Nationality, string Gender, string Rank, string Status,
         string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
         Int16 LoadType, string SortBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["ManifestDTO_TentativeManifestCount"]);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 17/04/2012
        /// Description: get tentative manifest export list
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="userId"></param>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        public List<TentativeManifestExport> GetTentativeExportList(DateTime dateFrom, string userId, int BranchId)
        {
            return DAL.GetTentativeExportList(dateFrom, userId, BranchId);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/04/2012
        /// Description: Export ALL
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static DataSet getAllDataFiles(DateTime selectedDate, string UserId)
        {
            return ManifestDAL.getAllDataFiles(selectedDate, UserId);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date            10/10/2012
        /// Description:    Get Forecast Manifest
        /// --------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date            27/Nov/2012
        /// Description:    return default value if Load Type = 0
        /// </summary>
        /// <returns></returns>
        public List<TentativeManifest> GetForecastManifestList(string DateFromString,
           string DateToString, string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
            int StartRow, int MaxRow, Int16 LoadType)
        {
            List<TentativeManifestGenericClass> ManifestTables = new List<TentativeManifestGenericClass>();
            List<TentativeManifest> list = new List<TentativeManifest>();
            if (LoadType == 0)
            {
                HttpContext.Current.Session["ManifestDTO_TentativeManifestCount"] = 0;
            }
            else
            {
                ManifestTables = DAL.GetForecastManifestList(DateFromString, DateToString,
                strUser, DateFilter, ByNameOrID, filterNameOrID,
                Nationality, Gender, Rank, Status, Region, Country, City,
                Port, Hotel, Vessel, UserRole, StartRow, MaxRow, LoadType);

                HttpContext.Current.Session["ManifestDTO_TentativeManifestCount"] = ManifestTables[0].TentativeManifestCount;
                list = ManifestTables[0].TentativeManifestList;
            }
            return list;
        }
        public int GetForecastManifestCount(string DateFromString,
           string DateToString, string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
           string Nationality, string Gender, string Rank, string Status,
           string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
           Int16 LoadType)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["ManifestDTO_TentativeManifestCount"]);
        }
        public List<TentativeManifestExport> GetForecastExportList(DateTime dateFrom, DateTime dateTo, string userId, int BranchId)
        {
            return DAL.GetForecastExportList(dateFrom, dateTo, userId, BranchId);
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
        public void ForecastGetFilters(string sUser, int RegionID, int PortID, int AirportID)
        {
            HttpContext.Current.Session["Forecast_Nationality"] = null;
            HttpContext.Current.Session["Forecast_Gender"] = null;
            HttpContext.Current.Session["Forecast_Rank"] = null;
            HttpContext.Current.Session["Forecast_Vessel"] = null;
            HttpContext.Current.Session["Forecast_Hotel"] = null;

            List<ForeCastFilters> list = new List<ForeCastFilters>();
            list = DAL.ForecastGetFilters(sUser, RegionID, PortID, AirportID);
            if (list.Count > 0)
            {
                HttpContext.Current.Session["Forecast_Nationality"] = list[0].NationalityList;
                HttpContext.Current.Session["Forecast_Gender"] = list[0].GenderList;
                HttpContext.Current.Session["Forecast_Rank"] = list[0].RankList;
                HttpContext.Current.Session["Forecast_Vessel"] = list[0].VesselList;
                HttpContext.Current.Session["Forecast_Hotel"] = list[0].HotelDTO;
            }
        }
        /// <summary>
        /// Date Created:  19/Mar/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list for HotelConfirmManifest Page
        /// </summary>        
        public void GetHotelConfirmManifestPage(string DateFromString,
         string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
         string Nationality, string Gender, string Rank, string Status,
         string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
          int StartRow, int MaxRow, Int16 LoadType, string orderBy, Int32 iNoOfDays)
        {

            DAL.GetHotelConfirmManifestPage(DateFromString,
                strUser,  DateFilter,  ByNameOrID,  filterNameOrID, Nationality,  
                Gender,  Rank,  Status, Region,  Country,  City,  Port,  Hotel,  
                Vessel,  UserRole, StartRow,  MaxRow,  LoadType,  orderBy, iNoOfDays);
        }
        /// <summary>
        /// Date Created:  18/Sept/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list for HotelConfirmManifest Page by Page Number
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestByPageNumber(string strUser, string UserRole, int StartRow, int RowCount, string loadType)
        {
            DAL.GetHotelConfirmManifestByPageNumber(strUser, UserRole, StartRow, RowCount, loadType);
        }
        /// <summary>
        /// Date Created:  03/Apr/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list of Confirm Manifest for Export use
        /// </summary>        
        public void GetHotelConfirmManifestExport(string DateFromString,
         string strUser, string Hotel, string orderBy)
        {
            DAL.GetHotelConfirmManifestExport(DateFromString,
            strUser, Hotel, orderBy);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   21/Mar/2013
        /// Description:    Confirm record and get the new confirmed and cancelled record
        /// ---------------------------------------------------------------
        /// </summary>
        public void ConfirmHotelManifest(string UserId, DateTime dDate, int iBranchID,
            string sRole, bool bIsSave, string sEmailTo, string sEmailCc,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            int iNoOfDays, DataTable dtToConfirm)
        {
            try
            {
                DAL.ConfirmHotelManifest(UserId, dDate, iBranchID,
                    sRole, bIsSave, sEmailTo, sEmailCc,
                    strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, iNoOfDays, dtToConfirm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtToConfirm != null)
                {
                    dtToConfirm.Dispose();
                }
            }
        }
         /// <summary>
        /// Date Created:  03/Sept/2013
        /// Created By:    Josephine Gad
        /// (description)  Get the list for HotelConfirmManifest Forecast
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestForecast(string DateFromString, string DateToString,
         string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
         string Nationality, string Gender, string Rank, string Status,
         string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole,
          int StartRow, int RowCount, Int16 LoadType, string orderBy)
        {
            DAL.GetHotelConfirmManifestForecast(DateFromString, DateToString,
             strUser, DateFilter, ByNameOrID, filterNameOrID, Nationality, Gender, Rank, Status,
             Region, Country, City, Port, Hotel, Vessel, UserRole, StartRow, RowCount, LoadType, orderBy);
        }


        /// <summary>
        /// Date Created:  23/Apr/2014
        /// Created By:    Muhallidin G Wali
        /// (description)  cancel hotel booking and insert into a table (TblHotelTransactionOtherCancel)
        /// ==========================================================
        /// </summary>    
        public void CancelHotelBooking(string TravelReq, string User, string Email, string CCEmail,
            string confirmby, string Comment, DateTime CancelDate, bool SendEmail, string sRole,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT)
        {
            DAL.CancelHotelBooking(TravelReq, User, Email, CCEmail, confirmby, Comment, CancelDate, SendEmail, 
                sRole, strLogDescription, strFunction, strPageName, DateGMT);
        }
        /// Date Created:   11/Sep/2014
        /// Created By:     Josephine Monteza
        /// (description)   Tag To Hotel
        /// ==========================================================
        /// </summary>
        public void TagToHotel(Int64 iIDBigint, Int64 iTravelReqID, string sRecLoc, Int64 iSeafarerID,
            string sStatus, Int64 iBranchID, Int64 iPortAgentID, string sUserName,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.TagToHotel(iIDBigint, iTravelReqID, sRecLoc, iSeafarerID,
                sStatus, iBranchID, iPortAgentID, sUserName, strLogDescription, 
                strFunction, strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// Date Created:  27/Nov/2014
        /// Created By:    Josephine Monteza
        /// (description)  Get Hotel Control No
        /// ==========================================================
        /// </summary>        
        public void GetHotelControlNo(int iBranchID, string sDate)
        {
            DAL.GetHotelControlNo(iBranchID, sDate);
        }
         /// <summary>
        /// Date Created:  10/Jul/2015
        /// Created By:    Josephine monteza
        /// (description)  Get the list of Confirm Manifest through Confirmation No. for Export use
        /// ==========================================================
        /// </summary>        
        public void GetHotelConfirmManifestWithControlNoExport(int iControlID, string orderBy)
        {
            DAL.GetHotelConfirmManifestWithControlNoExport(iControlID, orderBy);
        }

        /// <summary>
        /// Date Created:  30/Mar/2016
        /// Created By:    Josephine Monteza
        /// (description)  Get the hotel details of selected hotel
        /// ==========================================================
        /// </summary>        
        public List<BranchList> GetHotelVendorDetails(Int32 iBranchID)
        {
            return DAL.GetHotelVendorDetails(iBranchID);
        }
    }
}

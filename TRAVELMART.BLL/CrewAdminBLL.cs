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

    public class CrewAdminBLL
    {
        CrewAdminDAL DAL = new CrewAdminDAL();
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   27/08/2012
        /// Descrption:     Get all necessary list to Crew Admin's Home Page
        /// ---------------------------------------------------------
        /// </summary>
        public List<CrewAdminGenericClass> LoadCrewListPage(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo, int StartRow, int MaxRow)
        {
            List<CrewAdminGenericClass> CrewAdminListTable = new List<CrewAdminGenericClass>();
            CrewAdminListTable = DAL.LoadCrewList(LoadType, FromDate, ToDate,
            UserID, Role, OrderBy, VesselID, FilterByName, SeafarerID, NationalityID,
            Gender, RankID, Status, iAirLeg, iRouteFrom, iRouteTo, StartRow, MaxRow);

            HttpContext.Current.Session["CrewAdminList"] = CrewAdminListTable[0].CrewAdminList;
            HttpContext.Current.Session["CrewAdminListCount"] = CrewAdminListTable[0].CrewAdminListCount;
            return CrewAdminListTable;
        }
        
        /// <summary>
        /// Author:         Muhallidin
        /// Date Created:   03/02/2012
        /// Descrption:     Get All crew list assign in a vessel
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  27/22/2012
        /// Descrption:     Use session to load list; Add parameter Nationality,Gender and Rank
        /// </summary>       
        /// 
        public List<CrewAdminList> LoadCrewList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo, int StartRow, int MaxRow)
        {
            List<CrewAdminGenericClass> CrewAdminListTable = new List<CrewAdminGenericClass>();
            List<CrewAdminList> returnList = new List<CrewAdminList>();

            if (LoadType == 0)
            {
                returnList = (List<CrewAdminList>)HttpContext.Current.Session["CrewAdminList"];
            }
            else
            {
                CrewAdminListTable = DAL.LoadCrewList(LoadType, FromDate, ToDate,
                UserID, Role, OrderBy, VesselID, FilterByName, SeafarerID, NationalityID,
                Gender, RankID, Status, iAirLeg, iRouteFrom, iRouteTo, StartRow, MaxRow);
                returnList = CrewAdminListTable[0].CrewAdminList;
                HttpContext.Current.Session["CrewAdminListCount"] = CrewAdminListTable[0].CrewAdminListCount;
            }
            return returnList;           
        }
        public int LoadCrewListCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status, 
            Int16 iAirLeg,  Int16 iRouteFrom, Int16 iRouteTo)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["CrewAdminListCount"]);
        }   
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/Jan/2013
        /// Descrption:     Get Crew Admin Manifest to Export
        /// -----------------------------------------------------------------
        /// </summary>
        /// <param name="sUser"></param>
        /// <returns></returns>
        public static DataTable GetCrewAdminManifestExport(string sUser)
        {
            return CrewAdminDAL.GetCrewAdminManifestExport(sUser);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   22/Dec/2013
        /// Description:    Add/Cancel Transpo using Crew Admin Page
        /// ---------------------------------------------------------------
        /// </summary>
        public static void GetVehicleToAddCancel(DataTable dtTranspo, string UserId,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, bool IsCrewAdminSelectAll)
        {
            CrewAdminDAL.GetVehicleToAddCancel(dtTranspo, UserId,
            strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, IsCrewAdminSelectAll);
        }
        
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   22/Dec/2013
        /// Description:    Add/Cancel Transpo using Crew Admin Page
        /// ---------------------------------------------------------------
        /// </summary>
        public static void AddCancelVehicleManifest(string sComment, string sConfirmedBy, string sPickupDate, string sPickupTime,
            Int16 RouteIDFrom, Int16 RouteIDTo, string RouteFrom, string RouteTo,
            string UserId, string sTo, string sCC, string sBCC,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate, string sAddCancelEdit)
        {
            CrewAdminDAL.AddCancelVehicleManifest(sComment, sConfirmedBy, sPickupDate, sPickupTime,
            RouteIDFrom, RouteIDTo, RouteFrom, RouteTo,
            UserId, sTo, sCC, sBCC, strLogDescription, strFunction, strPageName,
            DateGMT, CreatedDate, sAddCancelEdit);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   22/Dec/2013
        /// Description:    Add/Cancel Transpo using Crew Admin Page
        /// ---------------------------------------------------------------
        /// </summary>
        public static void GetVehicleToEdit(DataTable dtTranspo, string UserId,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            CrewAdminDAL.GetVehicleToEdit(dtTranspo, UserId,
            strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   27/Dec/2013
        /// Description:    Edit Transpo using Crew Admin Page
        /// ---------------------------------------------------------------
        /// </summary>
        public static void  EditVehicleManifest(string sComment, string sPickupDate, string sPickupTime,
            Int16 RouteIDFrom, Int16 RouteIDTo, string RouteFrom, string RouteTo,
            string UserId, string sTo, string sCC, string sBCC,
            string strLogDescription, string strFunction, string strPageName,
            DateTime DateGMT, DateTime CreatedDate)
        {
            CrewAdminDAL.EditVehicleManifest(sComment, sPickupDate, sPickupTime,
            RouteIDFrom, RouteIDTo, RouteFrom, RouteTo,
            UserId, sTo, sCC, sBCC,
            strLogDescription, strFunction, strPageName,
            DateGMT, CreatedDate);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Descrption:     Save record to tblTempCrewAdminTransportationAddCancel to be able to tag all OFF to Need Vehicle
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsNeedVehicle(string UserID, bool IsNeedTransport)
        {
            DAL.TagCrewAsNeedVehicle(UserID, IsNeedTransport);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Descrption:     Save record to tblTempCrewAdminTransportationAddCancel to be able to tag single OFF to "Need Vehicle"
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsNeedVehicleSingle(string UserID, bool IsNeedTransport, Int64 iTravelReqID, Int64 iIDBigint)
        {
            DAL.TagCrewAsNeedVehicleSingle(UserID, IsNeedTransport, iTravelReqID, iIDBigint);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/May/2014
        /// Descrption:     Save record to tblTempCrewAdminPrintItinerary to be able to tag all to show itinerary
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsSelectedToPrintItinerary(string UserID, bool IsSelected)
        {
            DAL.TagCrewAsSelectedToPrintItinerary(UserID, IsSelected);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/May/2014
        /// Descrption:     Save record to tblTempCrewAdminPrintItinerary to be able to tag specific record to show itinerary
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        public void TagCrewAsSelectedToPrintItinerarySingle(string UserID, bool IsSelected, Int64 iTravelReqID, Int64 iIDBigint)
        {
            DAL.TagCrewAsSelectedToPrintItinerarySingle(UserID, IsSelected, iTravelReqID, iIDBigint);

        }
    }
}

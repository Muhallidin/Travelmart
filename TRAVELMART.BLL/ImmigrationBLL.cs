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
    public class ImmigrationBLL
    {
        ImmigrationDAL DAL = new ImmigrationDAL();
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   14/Jul/2014
        /// Descrption:     Get all necessary list for Immigration Page
        /// ---------------------------------------------------------
        /// </summary>
        public void LoadImmigrationPage(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int SeaportID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo, int StartRow, int MaxRow)
        {          

            DAL.LoadImmigrationPage(LoadType, FromDate, ToDate,
                UserID, Role, OrderBy, SeaportID, FilterByName, SeafarerID, NationalityID,
                Gender, RankID, Status, iAirLeg, iRouteFrom, iRouteTo, StartRow, MaxRow);            
         
        }

        public List<ImmigrationManifestList> GetImmigrationList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int SeaportID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo, int StartRow, int MaxRow)
        {
            List<ImmigrationManifestList> listImmigration = new List<ImmigrationManifestList>();

            if (LoadType == 0)
            {
                listImmigration = (List<ImmigrationManifestList>)HttpContext.Current.Session["Immigration_Manifest"];
            }
            else
            {
                DAL.LoadImmigrationPage(LoadType, FromDate, ToDate,
                    UserID, Role, OrderBy, SeaportID, FilterByName, SeafarerID, NationalityID,
                    Gender, RankID, Status, iAirLeg, iRouteFrom, iRouteTo, StartRow, MaxRow);

                listImmigration = (List<ImmigrationManifestList>)HttpContext.Current.Session["Immigration_Manifest"];
            }
            return listImmigration;
        }

        public Int32 GetImmigrationCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int SeaportID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iAirLeg, Int16 iRouteFrom, Int16 iRouteTo)
        {
            Int32 iCount = 0;
            iCount = GlobalCode.Field2Int(HttpContext.Current.Session["Immigration_ManifestCount"]);
            return iCount;
        }

        public List<CrewImmigration> CrewImmigration(short LoadType, long SeafarerID, long TravelReqID, string LOEControlNumber, string UserID)
        {
            try
            {
                ImmigrationDAL DAL = new ImmigrationDAL();
                return DAL.CrewImmigration(LoadType, SeafarerID, TravelReqID, LOEControlNumber, UserID);

            }
            catch (Exception ex)
            { 
                throw ex;           
            }
        
        
        }

        public List<CrewImmigration> InsertCrewImmigration(List<CrewImmigration> crewVerification)
        {
            try
            {
                ImmigrationDAL DAL = new ImmigrationDAL();
                return DAL.InsertCrewImmigration(crewVerification);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void InsertQRCode(List<SeafarerImage> SeafarerImage)
        {


            try
            {
                ImmigrationDAL DAL = new ImmigrationDAL();
                DAL.InsertQRCode(SeafarerImage);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}


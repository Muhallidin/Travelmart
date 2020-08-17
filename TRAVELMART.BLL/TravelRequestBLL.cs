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
    public class TravelRequestBLL
    {
        TravelRequestDAL DAL = new TravelRequestDAL();

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/02/2012
        /// Descrption:     Get Tables for No Travel Request List
        /// </summary>        
        /// <returns></returns>
        public List<NoTravelRequest> GetNoTravelRequestList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            try
            {
                List<NoTravelRequest> list = new List<NoTravelRequest>();

                list = DAL.GetNoTravelRequestList(LoadType, FromDate, ToDate,
                     UserID, Role, OrderBy, StartRow, MaxRow, VesselID, FilterByName,
                     SeafarerID, NationalityID, Gender, RankID, Status,
                     RegionID, CountryID, CityID, PortID);
                
                NoTravelRequest.NoTravelRequestCount = NoTravelRequest.NoTravelRequestCount;
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   07/02/2012
        /// Descrption:     Get count
        /// </summary>        
        /// <returns></returns>
        public int GetNoTravelRequestListCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            return NoTravelRequest.NoTravelRequestCount;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/03/2012
        /// Description: get no travel request list for export
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public DataTable GetNoTravelList(DateTime Date, string UserName, int RegionID)
        {
            return DAL.GetNoTravelList(Date, UserName, RegionID);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/03/2012
        /// Descrption:     Get Tables for Travel Request List with ON/OFF date same with Arrival/Departure Date
        /// </summary>        
        /// <returns></returns>
        public List<TravelRequestArrivalDepartureSameDate> GetTravelRequestArrivalDepartureSameDateList(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            try
            {
                List<TravelRequestArrivalDepartureSameDate> list = new List<TravelRequestArrivalDepartureSameDate>();

                list = DAL.GetTravelRequestArrivalDepartureSameDateList(LoadType, FromDate, ToDate,
                     UserID, Role, OrderBy, StartRow, MaxRow, VesselID, FilterByName,
                     SeafarerID, NationalityID, Gender, RankID, Status,
                     RegionID, CountryID, CityID, PortID);

                TravelRequestArrivalDepartureSameDate.ArrivalDepartureSameDateCount = TravelRequestArrivalDepartureSameDate.ArrivalDepartureSameDateCount;
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/03/2012
        /// Descrption:     Get count
        /// </summary>        
        /// <returns></returns>
        public int GetTravelRequestArrivalDepartureSameDateCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            return TravelRequestArrivalDepartureSameDate.ArrivalDepartureSameDateCount;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   30/03/2012
        /// Descrption:     Get Tables for Travel Request List with ON/OFF date same with Arrival/Departure Date for Export use
        /// </summary>        
        /// <returns></returns>
        public DataTable GetTravelRequestArrivalDepartureSameDateListExport
            (Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID)
        {
            return DAL.GetTravelRequestArrivalDepartureSameDateListExport(LoadType, FromDate, ToDate,
                     UserID, Role, OrderBy, StartRow, MaxRow, VesselID, FilterByName,
                     SeafarerID, NationalityID, Gender, RankID, Status,
                     RegionID, CountryID, CityID, PortID);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Insert Remarks
        /// </summary>
        /// <param name="sTravelRequestID"></param>
        /// <param name="sRemarks"></param>
        /// <param name="sCreatedBy"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        /// <returns></returns>
        public Int32 InsertTravelRequestRemarks(string sRole, string sTravelRequestID, string sRemarks, string sCreatedBy,
           String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate, Int64 idBigint)
        {
            return DAL.InsertTravelRequestRemarks(sRole, sTravelRequestID, sRemarks, sCreatedBy,
            strFunction, strPageName, DateGMT, CreatedDate, idBigint);
        }
         /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Update Travel Request Remarks
        /// </summary>
        /// <param name="iRemarksID"></param>
        /// <param name="sTravelRequestID"></param>
        /// <param name="sRemarks"></param>
        /// <param name="sModifiedBy"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public void UpdateTravelRequestRemarks(int iRemarksID, string sTravelRequestID, string sRemarks, string sModifiedBy,
           String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.UpdateTravelRequestRemarks(iRemarksID, sTravelRequestID, sRemarks, sModifiedBy,
            strFunction, strPageName, DateGMT, CreatedDate);
        }
         // <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Delete Travel Request Remarks        
        /// </summary>
        /// <param name="iDeleteType"></param>
        /// <param name="iRemarksID"></param>
        /// <param name="sTravelRequestID"></param>
        /// <param name="sRemarks"></param>
        /// <param name="sModifiedBy"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        public void DeleteTravelRequestRemarks(Int16 iDeleteType, int iRemarksID, string sTravelRequestID, string sModifiedBy,
           String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            DAL.DeleteTravelRequestRemarks(iDeleteType, iRemarksID, sTravelRequestID, sModifiedBy,
            strFunction, strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Get Travel Request Remarks 
        /// </summary>
        /// <param name="LoadType"></param>
        /// <param name="iTravelRequestID"></param>
        /// <param name="sUser"></param>
        /// <returns></returns>
        public List<TravelRequestRemarks> GetTravelRequestRemarks(Int16 LoadType,int iRemarksID, int iTravelRequestID, string sUser)
        {
            try
            {
                List<TravelRequestRemarks> list = new List<TravelRequestRemarks>();

                list = DAL.GetTravelRequestRemarks(LoadType, iRemarksID, iTravelRequestID, sUser);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/08/2012
        /// Descrption:     Get Travel Request of Meet & Greet Role necessary list for the whole page on first load
        /// ========================================================
        /// Author:         Marco Abejar
        /// Date Created:   22/03/2013
        /// Description:    Add sorting
        /// ========================================================
        /// </summary>
        /// <param name="LoadType"></param>
        /// <param name="sUser"></param>
        /// <param name="sRole"></param>
        /// <param name="sPortID"></param>
        /// <param name="sAirportID"></param>
        /// <param name="dDate"></param>
        /// <param name="iStartRow"></param>
        /// <param name="iMaxRow"></param>
        /// <param name="VesselID"></param>
        /// <param name="FilterByName"></param>
        /// <param name="SeafarerID"></param>
        /// <param name="NationalityID"></param>
        /// <param name="Gender"></param>
        /// <param name="RankID"></param>
        /// <param name="Status"></param>
        /// <param name="iViewType"></param>
        /// <returns></returns>
        public List<MeetGreetTravelRequestGenericClass> GetMeetGreetTravelRequestPage(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
           DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
           string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
           Int16 iViewType, string SortBy)
        {
            List<MeetGreetTravelRequestGenericClass> list = new List<MeetGreetTravelRequestGenericClass>();

            list = DAL.GetMeetGreetTravelRequest(LoadType, sUser, sRole, sPortID, sAirportID, dDate, iStartRow, iMaxRow,
                 VesselID, FilterByName, SeafarerID, NationalityID, Gender, RankID, Status, iViewType, SortBy);
            //MeetGreetTravelRequest.MeetGreetTravelRequestCount = MeetGreetTravelRequest.MeetGreetTravelRequestCount;
            HttpContext.Current.Session["MeetGreetTravelRequestList"] = list[0].MeetGreetTravelRequestList;
            HttpContext.Current.Session["MeetGreetTravelRequestCount"] = list[0].MeetGreetTravelRequestCount;
            return list;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/08/2012
        /// Descrption:     Get Travel Request of Meet & Greet Role
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sRole"></param>
        /// <param name="sPortID"></param>
        /// <param name="sAirportID"></param>
        /// <param name="dDate"></param>
        /// <param name="iStartRow"></param>
        /// <param name="iMaxRow"></param>
        /// <returns></returns>
        public List<MeetGreetTravelRequest> GetMeetGreetTravelRequest(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
            DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            Int16 iViewType, string SortBy)
        {
            List<MeetGreetTravelRequestGenericClass> list = new List<MeetGreetTravelRequestGenericClass>();
            List<MeetGreetTravelRequest> returnList = new List<MeetGreetTravelRequest>();
            if (LoadType == 0)
            {
                returnList = (List<MeetGreetTravelRequest>)HttpContext.Current.Session["MeetGreetTravelRequestList"];
            }
            else
            {
                list = DAL.GetMeetGreetTravelRequest(LoadType, sUser, sRole, sPortID, sAirportID, dDate, iStartRow, iMaxRow,
                     VesselID, FilterByName, SeafarerID, NationalityID, Gender, RankID, Status, iViewType, SortBy);
                //MeetGreetTravelRequest.MeetGreetTravelRequestCount = MeetGreetTravelRequest.MeetGreetTravelRequestCount;
                returnList = list[0].MeetGreetTravelRequestList;
                HttpContext.Current.Session["MeetGreetTravelRequestCount"] = list[0].MeetGreetTravelRequestCount;
            }
            return returnList;
        }
        public int GetMeetGreetTravelRequestCount(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
            DateTime dDate, string VesselID, string FilterByName,
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status, 
            Int16 iViewType, string SortBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["MeetGreetTravelRequestCount"]);//MeetGreetTravelRequest.MeetGreetTravelRequestCount;
        }
        /// <summary>
        /// Date Created:   18/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of seafarers with restricted nationality
        /// </summary>
        /// <returns></returns>
        public List<TravelRequestRestrictedNationality> GetTRRestrictedNationalityList
           (Int16 LoadType, DateTime FromDate, DateTime ToDate,
           string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
           string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
           int RegionID, int CountryID, int CityID, int PortID)
        {
            List<TravelRequestRestrictedNationalityGenClass> list = new List<TravelRequestRestrictedNationalityGenClass>();
            list = DAL.GetTRRestrictedNationalityList(LoadType, FromDate, ToDate,
               UserID, Role, OrderBy, StartRow, MaxRow, VesselID, FilterByName,
               SeafarerID, NationalityID, Gender, RankID, Status,
               RegionID, CountryID, CityID, PortID);
         
                List<TravelRequestRestrictedNationality> listReturn = new List<TravelRequestRestrictedNationality>();
                listReturn = list[0].TravelRequestRestrictedNationalityList;
                HttpContext.Current.Session["TravelRequestRestrictedNationalityCount"] = list[0].TravelRequestRestrictedNationalityCount;
                return listReturn;
        }
        public int GetTRRestrictedNationalityCount
           (Int16 LoadType, DateTime FromDate, DateTime ToDate,
           string UserID, string Role, string OrderBy, int VesselID, int FilterByName,
           string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
           int RegionID, int CountryID, int CityID, int PortID)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["TravelRequestRestrictedNationalityCount"]);
        }
        /// <summary>
        /// Date Created:   18/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get list of seafarers with restricted nationality to export
        /// </summary>
        /// <returns></returns>
        public DataTable GetTRRestrictedNationalityExport(string UserID)
        {
            return DAL.GetTRRestrictedNationalityExport(UserID);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   16/11/2012
        /// Description:    Get Travel Request of Meet & Greet & Service Provider Role for Export to Excel
        /// ========================================================
        /// Author:         Marco Abejar
        /// Date Created:   22/03/2013
        /// Description:    Add sorting
        /// ========================================================
        /// </summary>
        public static DataTable GetMeetGreetTravelRequestExport(Int16 LoadType, string sUser, string sRole, string
            sPortID, string sAirportID, string SortBy)
        {
            return  TravelRequestDAL.GetMeetGreetTravelRequestExport(LoadType, sUser, sRole, sPortID, sAirportID, SortBy);
        }

        /// <summary>
        /// Author:         Marco Abejar
        /// Date Created:   13/11/2013
        /// Descrption:     Get PrtAgent Request
        /// 
        public static List<PortAgentRequest> GetPortAgentRequest(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
          DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
          string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
          Int16 iViewType, string SortBy, int iVendorId, int iNoOfDay)
        {
            List<PortAgentRequest> list = new List<PortAgentRequest>();
            TravelRequestDAL.GetPortAgentRequest(LoadType, sUser, sRole, sPortID, sAirportID, dDate, iStartRow, iMaxRow,
                 VesselID, FilterByName, SeafarerID, NationalityID, Gender, RankID, Status, iViewType, SortBy, iVendorId, iNoOfDay);

            list = (List<PortAgentRequest>)HttpContext.Current.Session["PortAgentRequest"];
            return list;
        }

        //public List<PortAgentRequestGenericClass> GetPortAgentRequest(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
        //   DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
        //   string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
        //   Int16 iViewType, string SortBy, int iVendorId)
        //{
        //    List<PortAgentRequestGenericClass> list = new List<PortAgentRequestGenericClass>();

        //    list = DAL.GetPortAgentRequest(LoadType, sUser, sRole, sPortID, sAirportID, dDate, iStartRow, iMaxRow,
        //         VesselID, FilterByName, SeafarerID, NationalityID, Gender, RankID, Status, iViewType, SortBy, iVendorId);
        //    //MeetGreetTravelRequest.MeetGreetTravelRequestCount = MeetGreetTravelRequest.MeetGreetTravelRequestCount;
        //    HttpContext.Current.Session["PortAgentRequestList"] = list[0].PortAgentRequestList;
        //    HttpContext.Current.Session["PortAgentRequestCount"] = list[0].PortAgentRequestCount;
        //    return list;
        //}

        public static int GetPortAgentRequestCount(Int16 LoadType, string sUser, string sRole, string sPortID, string sAirportID,
           DateTime dDate, int iStartRow, int iMaxRow, string VesselID, string FilterByName,
           string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
           Int16 iViewType, string SortBy, int iVendorId)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["PortAgentRequestCount"]);//MeetGreetTravelRequest.MeetGreetTravelRequestCount;
        }
         /// ========================================================
        /// Author:         Josephine Gad
        /// Date Created:   11/Aug/2014
        /// Description:    Export list of Service Provider Request
        /// ========================================================
        /// </summary>
        public static DataTable GetPortAgentRequestExport(Int16 LoadType, string sUser, string SortBy)
        {
            DataTable dt = null;
            try
            {
                dt = TravelRequestDAL.GetPortAgentRequestExport(LoadType, sUser, SortBy);
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
    }
}

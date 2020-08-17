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
    public class HotelDashboardBLL
    {
        HotelDashboardDAL DAL = new HotelDashboardDAL();
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/02/2012
        /// Descrption:     send hotel dashboard queries (list and count) to list
        /// </summary>
        /// <param name="iRegionID"></param>
        /// <param name="iCountryID"></param>
        /// <param name="iCityID"></param>
        /// <param name="sUserName"></param>
        /// <param name="sRole"></param>
        /// <param name="iBranchID"></param>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="sBranchName"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <returns></returns>
        public List<HotelDashboardList> LoadHotelDashboardList(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
            Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
            string sBranchName, int StartRow, int MaxRow, Int16 FromDefaultView)
        {
            List<HotelDashboardDTOGenericClass> HotelDashBoardListTable = new List<HotelDashboardDTOGenericClass>();
            List<HotelDashboardList> list = new List<HotelDashboardList>();
            if (FromDefaultView == 0)
            {
                HotelDashBoardListTable = DAL.LoadAllHotelDashboardTables(iLoadType, iRegionID, iCountryID,
                 iCityID, iPortID, sUserName, sRole, iBranchID, dFrom, dTo,
                 sBranchName, StartRow, MaxRow);

                HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardList"] = HotelDashBoardListTable[0].HotelDashboardList;
                HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardListCount"] = HotelDashBoardListTable[0].HotelDashboardListCount;
                list = HotelDashBoardListTable[0].HotelDashboardList;

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["HotelDashboardDTO_HotelExceptionNoTravelRequestList"] = HotelDashBoardListTable[1].HotelExceptionNoTravelRequestList;
                }
            }
            else
            {
                list = (List<HotelDashboardList>)HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardList"];
            }
            return list;
        }
        public int LoadHotelDashboardListCount(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
            Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
            string sBranchName, Int16 FromDefaultView)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardListCount"]);
        }
        
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/02/2012
        /// Descrption:     send hotel dashboard queries (list and count) to list
        /// --------------------------------------------------------------------------------
        /// Modfied by:     Gabriel Oquialda
        /// Date Modified:  13/03/2012
        /// Description:    This is a modified 'LoadHotelDashboardList' copy for new screens
        /// --------------------------------------------------------------------------------
        /// Modfied by:     Josephine Gad
        /// Date Modified:  03/10/2012
        /// Description:    Change HotelDashboardDTO.HotelDashboardList  to Session["HotelDashboardDTO_HotelDashboardList"]
        ///                 Change HotelDashboardDTO.HotelDashboardListCount to  HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardListCount"]
        ///                 Add HttpContext.Current.Session["HotelDashboardDTO_RegionList"]
        ///                 Add HttpContext.Current.Session["HotelDashboardDTO_HotelExceptionCount"]
        ///                 Add HttpContext.Current.Session["HotelDashboardDTO_HotelOverflowCount"]
        ///                 Add HttpContext.Current.Session["HotelDashboardDTO_NoTravelRequestCount"]
        ///                 Add HttpContext.Current.Session["HotelDashboardDTO_ArrDeptSameOnOffDateCount"]
        ///                 Add HttpContext.Current.Session["HotelDashboardDTO_NoContractCount"]
        /// --------------------------------------------------------------------------------
        /// </summary>
        /// <param name="iRegionID"></param>
        /// <param name="iCountryID"></param>
        /// <param name="iCityID"></param>
        /// <param name="sUserName"></param>
        /// <param name="sRole"></param>
        /// <param name="iBranchID"></param>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="sBranchName"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <returns></returns>
        public List<HotelDashboardList> LoadHotelDashboardList2(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
            Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
            string sBranchName, int StartRow, int MaxRow)
        {
            List<HotelDashboardList> listReturn = new List<HotelDashboardList>();
            List<HotelDashboardDTOGenericClass> HotelDashBoardListTable = new List<HotelDashboardDTOGenericClass>();

            HotelDashBoardListTable = DAL.LoadAllHotelDashboardTables2(iLoadType, iRegionID, iCountryID,
             iCityID, iPortID, sUserName, sRole, iBranchID, dFrom, dTo,
             sBranchName, StartRow, MaxRow);

            HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardList"] = HotelDashBoardListTable[0].HotelDashboardList;
            //HotelDashboardDTO.HotelDashboardListCount = HotelDashBoardListTable[0].HotelDashboardListCount;
            HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardListCount"] = HotelDashBoardListTable[0].HotelDashboardListCount;

            if (iLoadType == 0)
            {
                HttpContext.Current.Session["HotelDashboardDTO_RegionList"] = HotelDashBoardListTable[1].RegionList;
                HttpContext.Current.Session["HotelDashboardDTO_HotelExceptionCount"] = HotelDashBoardListTable[0].HotelExceptionCount;
                HttpContext.Current.Session["HotelDashboardDTO_HotelOverflowCount"] = HotelDashBoardListTable[0].HotelOverflowCount;
                HttpContext.Current.Session["HotelDashboardDTO_NoTravelRequestCount"] = HotelDashBoardListTable[0].NoTravelRequestCount;
                HttpContext.Current.Session["HotelDashboardDTO_ArrDeptSameOnOffDateCount"] = HotelDashBoardListTable[0].ArrDeptSameOnOffDateCount;
                HttpContext.Current.Session["HotelDashboardDTO_NoContractCount"] = HotelDashBoardListTable[0].NoContractCount;
                HttpContext.Current.Session["HotelDashboardDTO_RestrictedNationalityCount"] = HotelDashBoardListTable[0].RestrictedNationalityCount;
            }
            listReturn = HotelDashBoardListTable[0].HotelDashboardList;

            return listReturn;
        }
       
        public List<HotelDashboardList> LoadHotelDashboardList3(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
           Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
           string sBranchName, int StartRow, int MaxRow, Int16 FromDefaultView)
        {
            List<HotelDashboardList> listReturn = new List<HotelDashboardList>();
            listReturn = (List<HotelDashboardList>)(HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardList"]);
            
            //if (FromDefaultView == 0)
            //{
            //    listReturn = (List<HotelDashboardList>)(HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardList"]);
            //}
            //else
            //{
            //    HotelDashboardBLL bll = new HotelDashboardBLL();
            //    listReturn = bll.LoadHotelDashboardList2(iLoadType, iRegionID, iCountryID,
            //    iCityID, iPortID, sUserName, sRole, iBranchID, dFrom, dTo,
            //    sBranchName, StartRow, MaxRow);
            //}
            return listReturn;
        }
        public int LoadHotelDashboardListCount3(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
           Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
           string sBranchName, Int16 FromDefaultView)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["HotelDashboardDTO_HotelDashboardListCount"]);
        }


        public List<HotelDashBoardPAGenericClass> GetNotTurnPort(short LoadType, int PortID, string UserID, DateTime Dates)
        {
            try
            {
                return DAL.GetNotTurnPort(LoadType, PortID, UserID, Dates);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}

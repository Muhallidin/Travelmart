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
    public class CountryBLL
    {
        /// <summary>
        /// Date Created:   15/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Country List
        /// ========================================                   
        /// Date Modified:  15/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Dispose DataTable
        /// </summary>
        /// </summary>
        public static DataTable CountryList()
        {           
            DataTable CityDataTable = null;
            try
            {
                CityDataTable = CountryDAL.CountryList();
                return CityDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:  15/08/2011
        /// Created By:    Josephine Gad
        /// (description)  Get Country List by user        
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static DataTable CountryListByUser(string UserID)
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = CountryDAL.CountryListByUser(UserID);
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
        /// Date Created:    23/09/2011
        /// Created By:      Josephine Gad
        /// (description)    Get Country List by Region ID
        /// </summary>
        /// <param name="RegionID"></param>
        /// <returns></returns>
        public static DataTable CountryListByRegion(string RegionID, string CountryName)
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = CountryDAL.CountryListByRegion(RegionID, CountryName);
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
        ///// <summary>         
        ///// Date Create:    15/08/2011
        ///// Create By:      Josephine Gad
        ///// (description)   Get Region List
        ///// </summary>
        //public static DataTable RegionList()
        //{           
        //    DataTable RegionDataTable = null;
        //    try
        //    {
        //        RegionDataTable = CountryDAL.RegionList();
        //        return RegionDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (RegionDataTable != null)
        //        {
        //            RegionDataTable.Dispose();
        //        }
        //    }
        //}
        /// <summary>         
        /// Date Create:    15/08/2011
        /// Create By:      Josephine Gad
        /// (description)   Get Region List By UserID
        /// ----------------------------------
        /// Date Modified:   25/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Remove Country parameter
        ///                  Change Datatable to List
        /// </summary>
        /// </summary>
        public static List<RegionList> RegionListByUser(string UserID)
        {
            return CountryDAL.RegionListByUser(UserID);
            //DataTable RegionDataTable = null;
            //try
            //{
            //    RegionDataTable = CountryDAL.RegionListByUser(UserID, CountryID);
            //    return RegionDataTable;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (RegionDataTable != null)
            //    {
            //        RegionDataTable.Dispose();
            //    }
            //}
        }
        /// <summary>         
        /// Date Create:    15/08/2011
        /// Create By:      Josephine Gad
        /// (description)   Get Region List By UserID
        /// </summary>
        public static DataTable MapListByUser(string UserID)
        {           
            DataTable MapDataTable = null;
            try
            {
                MapDataTable = CountryDAL.MapListByUser(UserID);
                return MapDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MapDataTable != null)
                {
                    MapDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   04/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Country list by continent
        /// ---------------------------------------------
        /// </summary>
        /// <param name="ContinentID"></param>
        /// <returns></returns>
        public static List<Country> GetCountryByContinent(string ContinentID)
        {
            return CountryDAL.GetCountryByContinent(ContinentID);
        }
        /// <summary>
        /// Date Created:   19/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get the generic class of country
        /// </summary>
        public static void GetCountryList()
        {
            CountryDAL.GetCountryList();
        }
        /// <summary>
        /// Date Created:   19/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Get the generic class of country
        /// </summary>
        public static void GetRestrictedNationalityList(bool IsViewBoth, bool IsRestricted, int CountryID)
        {
            List<NationalityGenericClass> list = new List<NationalityGenericClass>();
            list = CountryDAL.GetRestrictedNationalityList(IsViewBoth, IsRestricted, CountryID);
            if(IsViewBoth)
            {
                HttpContext.Current.Session["NationalityGenericClass_NonRestNationalityList"] =list[0].NonRestNationalityList;
                HttpContext.Current.Session["NationalityGenericClass_RestNationalityList"] =list[0].RestNationalityList;
                HttpContext.Current.Session["NationalityGenericClass_RestNationalityCount"] = list[0].RestNationalityCount;
            }
        }
          /// <summary>
        /// Date Created:   21/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Save Restricted Nationality in Country
        /// </summary>
        public static void SaveRestrictedNationality(int CountryID, int NationalityID, string sRemarks, string sUserName,
          String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            CountryDAL.SaveRestrictedNationality(CountryID, NationalityID, sRemarks, sUserName,
            strFunction, strPageName, DateGMT, CreatedDate);
        }
        /// <summary>
        /// Date Created:   21/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Delete Restricted Nationality in Country
        /// </summary>
        public static void DeleteRestrictedNationality(int RestrictedID, string sRemarks, string sUserName,
          String strFunction, String strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            CountryDAL.DeleteRestrictedNationality(RestrictedID, sRemarks, sUserName,
            strFunction, strPageName, DateGMT, CreatedDate);
        }
    }
}

using System;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web.SessionState;
using System.Web;  

namespace TRAVELMART.BLL
{
    public class SeafarerTravelBLL: IRequiresSessionState
    {
        /// <summary>                                               
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description) Selecting Arriving Seafarer base on flight details 
        /// </summary> 
        /// 
        public static DataTable GetSFTravelList(string sfStatus, string strFlightDateRange, string strPendingFilter,
            string strAirStatusFilter, string strSeafarerName, string DateFromString, string DateToString, string strUser,
            string strRegion, string strFilterBy)
        {
            DataTable TravelDataTable = null;
            try
            {
                TravelDataTable = SeafarerTravelDAL.GetSFTravelList(sfStatus, strFlightDateRange, strPendingFilter, strAirStatusFilter,
                    strSeafarerName, DateFromString, DateToString, strUser, strRegion, strFilterBy);
                return TravelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (TravelDataTable != null)
                {
                    TravelDataTable.Dispose();
                }
            }
        }
        /// <summary>                                               
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description)  Selecting list of hotel transaction details    
        /// </summary> 
        /// 
        public static DataTable GetSFHotelTravelList(string sfStatus, string strFlightDateRange, string DateFromString, string DateToString,
            string NameString, string strUser, string strMapRef, string strFilterBy)
        {
            DataTable HotelDataTable = null;
            try
            {
                HotelDataTable = SeafarerTravelDAL.GetSFHotelTravelList(sfStatus, strFlightDateRange, DateFromString, DateToString, NameString,
                    strUser, strMapRef, strFilterBy);
                return HotelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }
        /// <summary>                                               
        /// Date Created: 4/10/2011
        /// Created By: Ryan Bautista
        /// (description)  Selecting list of hotel transaction details    
        /// </summary> 
        /// 
        public static DataTable GetSFHotelTravelListView(string sfStatus, string strFlightDateRange, string DateFromString, string DateToString,
            string NameString, string strUser, string strMapRef, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status, string Region, string Country, string City, string Port, string Hotel,
            string Vessel, string UserRole)
        {
            DataTable HotelDataTable = null;
            try
            {
                HotelDataTable = SeafarerTravelDAL.GetSFHotelTravelListView(sfStatus, strFlightDateRange, DateFromString, DateToString, NameString,
                    strUser, strMapRef, DateFilter, ByNameOrID, filterNameOrID, Nationality, Gender, Rank, Status, Region, Country, City,
                    Port, Hotel, Vessel, UserRole);
                return HotelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>                                               
        /// Date Created: 4/10/2011
        /// Created By: Ryan Bautista
        /// (description)  Selecting list of hotel transaction details   
        /// ----------------------------------------------
        /// Date Modified:  01/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete Parameter strFlightDateRange and DateToString
        ///                 delete HotelFilter, use Hotel parameter instead
        /// ----------------------------------------------
        /// Date Modified:  05/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Remove adding of days in DateFrom
        /// </summary>
        /// </summary> 
        /// 
        public static DataTable GetSFHotelTravelListView2(string DateFromString,
            string NameString, string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status, string Region, string Country, string City, string Port, string Hotel,
            string Vessel, string UserRole, string Hours)
        {
            DataTable HotelDataTable = null;
            try
            {
                //Int32 HotelFilterInt = 0;
                //if (HotelFilter == "")
                //{
                //    HotelFilterInt = 0;
                //}
                //else
                //{
                //    HotelFilterInt = Convert.ToInt32(HotelFilter);
                //}

                //DateFromString = Convert.ToDateTime(DateFromString).AddHours(Convert.ToInt16(Hours)).ToString();

                HotelDataTable = SeafarerTravelDAL.GetSFHotelTravelListView2(DateFromString, NameString,
                    strUser, DateFilter, ByNameOrID, filterNameOrID, Nationality, Gender, Rank, Status, Region, Country, City,
                    Port, Hotel, Vessel, UserRole);
                return HotelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>                                               
        /// Date Created: 4/10/2011
        /// Created By: Ryan Bautista
        /// (description)  Selecting list of hotel transaction details    
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary> 
        /// ,
        public static DataTable GetSFHotelTravelListView_ByHour(string sfStatus, string strFlightDateRange, string Hour,
            string NameString, string strUser, string strMapRef, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status, string Region, string Country, string City, string Port, string Hotel,
            string Vessel, string UserRole, bool ByHour, string HotelFilter)
        {
            DataTable HotelDataTable = null;
            string DateFromString;
            string DateToString;

            Int32 HotelFilterInt = 0;
            if (HotelFilter == "")
            {
                HotelFilterInt = 0;
            }
            else
            {
                HotelFilterInt = Convert.ToInt32(HotelFilter);
            }

            if (ByHour)
            {
                DateTime dte = DateTime.Now;
                //DateFromString = dte.ToString();
                DateFromString = GlobalCode.Field2String(HttpContext.Current.Session["DateFrom"]);
                DateToString = dte.AddHours(Convert.ToInt16(Hour)).ToString();
            }
            else
            {
                DateFromString = GlobalCode.Field2String(HttpContext.Current.Session["DateFrom"]);
                DateToString = GlobalCode.Field2String(HttpContext.Current.Session["DateTo"]);
            }

            try
            {
                //dt = HotelManifestBLL.GetLockedManifest(dDate, MUser.GetUserName(), Session["strPendingFilter"],
                //     GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
                //     GlobalCode.Field2String(Session["City"]),
                //     uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                //      GlobalCode.Field2String(Session["Port"]), uoDropDownListBranch.SelectedValue, Session["Vehicle"],
                //     uoDropDownListVessel.SelectedValue,
                //     uoDropDownListNationality.SelectedValue, uoDropDownListGender.SelectedValue,
                //     uoDropDownListRank.SelectedValue,
                //     uoHiddenFieldRole.Value, uoDropDownListHours.SelectedValue
                //    );

                HotelDataTable = SeafarerTravelDAL.GetSFHotelTravelListView_ByHour(sfStatus, strFlightDateRange, DateFromString, NameString,
                    strUser, strMapRef, DateFilter, ByNameOrID, filterNameOrID, Nationality, Gender, Rank, Status, Region, Country, City,
                    Port, Hotel, Vessel, UserRole, ByHour, HotelFilterInt);
                return HotelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>                                               
        /// Date Created: 4/10/2011
        /// Created By: Ryan Bautista
        /// (description)  Selecting list of hotel transaction details    
        /// </summary> 
        /// 
        public static DataTable GetSFHotelTravelListView_ByHour3(string sfStatus, string strFlightDateRange, string Hour,
            string NameString, string strUser, string strMapRef, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status, string Region, string Country, string City, string Port, string Hotel,
            string Vessel, string UserRole, bool ByHour)
        {
            DataTable HotelDataTable = null;
            string DateFromString;
            string DateToString;

            //if (ByHour)
            //{
            DateTime dte = DateTime.Now;
            DateFromString = dte.ToString();
            DateToString = dte.AddHours(Convert.ToInt16(Hour)).ToString();
            //}
            //else
            //{
            //    DateFromString = GlobalCode.Field2String(Session["DateFrom"]);
            //    DateToString = TravelMartVariable.DateToString;
            //}

            try
            {
                HotelDataTable = SeafarerTravelDAL.GetSFHotelTravelListView_ByHour3(sfStatus, strFlightDateRange, DateFromString, NameString,
                    strUser, strMapRef, DateFilter, ByNameOrID, filterNameOrID, Nationality, Gender, Rank, Status, Region, Country, City,
                    Port, Hotel, Vessel, UserRole, ByHour);
                return HotelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>                                               
        /// Date Created: 01/08/2011
        /// Created By: Ryan Bautista
        /// (description) Searching in hotel transaction details
        /// </summary> 
        /// 
        public static DataTable GetSFHotelTravelListSearch(string sfStatus, string strFlightDateRange, string SFName)
        {
            DataTable HotelDataTable = null;
            try
            {
                HotelDataTable = SeafarerTravelDAL.GetSFHotelTravelListSearch(sfStatus, strFlightDateRange, SFName);
                return HotelDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }
        /// <summary>                                               
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description)  Selecting list of vehicle travel details   
        /// </summary>                         
        public static DataTable GetSFVehicleTravelDetails(string sfStatus, string strFlightDateRange, string strSeafarerName,
            string DateFromString, string DateToString, string strUser, string strMapRef, string strFilterBy)
        {
            DataTable VehicleDataTable = null;
            try
            {
                VehicleDataTable = SeafarerTravelDAL.GetSFVehicleTravelDetails(sfStatus, strFlightDateRange, strSeafarerName,
                    DateFromString, DateToString, strUser, strMapRef, strFilterBy);
                return VehicleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VehicleDataTable != null)
                {
                    VehicleDataTable.Dispose();
                }
            }
        }
        /// <summary>                                               
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description)   Selecting list of port transaction details     
        /// ============================================================
        /// Date Modified:  13/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter Country, City, Port, Nationality and others
        /// </summary>    
        public static DataTable GetSFPortTravelDetails(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank, string Role)
        {
            DataTable PortDataTable = null;
            try
            {
                PortDataTable = SeafarerTravelDAL.GetSFPortTravelDetails(DateFrom, DateTo, UserID,
                 FilterByDate, RegionID, CountryID, CityID, Status, FilterByNameID, FilterNameID,
                 PortID, VesselID, Nationality, Gender, Rank, Role);
                return PortDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   09/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get total row count of port transaction details    
        /// --------------------------------------------------------
        /// </summary>
        public int GetSFPortTravelDetailsCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank, string Role,
            int ByVessel, int ByName, int ByRecLoc, int ByE1ID, int ByDateOnOff, int ByStatus,
            int ByPort, int ByRank, int ByPortStatus, int ByNationality)
        {
            return SeafarerTravelDAL.GetSFPortTravelDetailsCount(DateFrom, DateTo, UserID,
                FilterByDate, RegionID, CountryID, CityID, Status,
                FilterByNameID, FilterNameID, PortID, VesselID,
                Nationality, Gender, Rank, Role);
        }
        /// <summary>            
        /// Date Created:   09/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get port transaction details    
        /// --------------------------------------------------------
        /// </summary>
        public DataTable GetSFPortTravelDetailsWithCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank, string Role,
            int ByVessel, int ByName, int ByRecLoc, int ByE1ID, int ByDateOnOff, int ByStatus,
            int ByPort, int ByRank, int ByPortStatus, int ByNationality, int StartRow, int MaxRow)
        {
            DataTable PortDataTable = null;
            try
            {
                PortDataTable = SeafarerTravelDAL.GetSFPortTravelDetailsWithCount(DateFrom, DateTo, UserID,
                 FilterByDate, RegionID, CountryID, CityID, Status, FilterByNameID, FilterNameID,
                 PortID, VesselID, Nationality, Gender, Rank, Role,
                 ByVessel, ByName, ByRecLoc, ByE1ID, ByDateOnOff, ByStatus,
                 ByPort, ByRank, ByPortStatus, ByNationality, StartRow, MaxRow);
                return PortDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (PortDataTable != null)
                {
                    PortDataTable.Dispose();
                }
            }
        }
        /// <summary>                                               
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description)  Selecting list of air travel details   
        /// /// Date Modified:  12/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter Country, City, PortID, Nationality, etc
        /// </summary>     
        /// 
        public static DataTable GetSFAirTravelDetails(string DateFrom, string DateTo, string AirStatusFilter,
            string UserID, string FilterByDate, string RegionID, string CountryID, string CityID,
            string Status, string FilterByNameID, string FilterNameID, string PortID,
            string VesselID, string Nationality, string Gender, string Rank)
        {
            DataTable AirDataTable = null;
            try
            {
                AirDataTable = SeafarerTravelDAL.GetSFAirTravelDetails(DateFrom, DateTo, AirStatusFilter,
                UserID, FilterByDate, RegionID, CountryID, CityID, Status, FilterByNameID, FilterNameID,
                PortID, VesselID, Nationality, Gender, Rank);
                return AirDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (AirDataTable != null)
                {
                    AirDataTable.Dispose();
                }
            }
        }

        /// <summary>                                               
        /// Date Created: 03/08/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer count per flight status
        /// ---------------------------------------------------------------------          
        /// Date Modified: 04/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter for Date Range            
        /// ---------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FilterByString    
        /// </summary> 
        /// 
        public static String GetAirStatusCount(string sfStatus, string strFlightDateRange, string strAirStatusFilter,
              string DateFromString, string DateToString, string FilterByString)
        {
            String strAirStatusCount = SeafarerTravelDAL.GetAirStatusCount(sfStatus, strFlightDateRange, strAirStatusFilter,
                  DateFromString, DateToString, FilterByString);
            return strAirStatusCount;
        }

        /// <summary>                                               
        /// Date Created: 03/08/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer count with pending transactions
        /// ---------------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FilterByString
        /// </summary> 
        /// 

        public static String GetPendingTransactionCount(string sfStatus, string strFlightDateRange, string strPendingFilter,
              string DateFromString, string DateToString, string FilterByString)
        {
            String strPendingCount = SeafarerTravelDAL.GetPendingTransactionCount(sfStatus, strFlightDateRange, strPendingFilter,
                DateFromString, DateToString, FilterByString);
            return strPendingCount;
        }

        /// <summary>                                               
        /// Date Created: 03/08/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer count based on date range (onsigning/offsigning)   
        /// ---------------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FilterByString
        /// </summary> 
        /// 
        public static String GetSeafarerCountByDateRange(string sfStatus, string strFlightDateRange, string FilterByString)
        {
            String strSFCount = SeafarerTravelDAL.GetSeafarerCountByDateRange(sfStatus, strFlightDateRange, FilterByString);
            return strSFCount;
        }
        /// <summary>
        /// Date Created:   26/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Travel Manifest        
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="UserID"></param>
        /// <param name="DateFilter"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="CityId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static DataTable GetTravelManifest(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank)
        {
            DataTable ManifestDataTable = null;
            try
            {
                ManifestDataTable = SeafarerTravelDAL.GetTravelManifest(DateFrom, DateTo, UserID, DateFilter,
                RegionId, CountryId, CityId, Status, ByNameOrID, filterNameOrID,
                PortId, HotelId, VehicleId, VesselId, Nationality, Gender, Rank);
                return ManifestDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ManifestDataTable != null)
                {
                    ManifestDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Travel Manifest with row count and order by
        /// =============================================================
        /// Date Created:   13/10/2011
        /// Created By:     Muhallidin G Wali 
        /// (description)   Check the input date if in Correct format
        /// </summary>
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="UserID"></param>
        /// <param name="DateFilter"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="CityId"></param>
        /// <param name="Status"></param>
        /// <param name="ByNameOrID"></param>
        /// <param name="filterNameOrID"></param>
        /// <param name="PortId"></param>
        /// <param name="HotelId"></param>
        /// <param name="VehicleId"></param>
        /// <param name="VesselId"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="ByVessel"></param>
        /// <param name="ByName"></param>
        /// <param name="ByRecLoc"></param>
        /// <param name="ByE1ID"></param>
        /// <param name="ByDateOnOff"></param>
        /// <param name="ByDateArrDep"></param>
        /// <param name="ByStatus"></param>
        /// <param name="ByBrand"></param>
        /// <param name="ByPort"></param>
        /// <param name="ByRank"></param>
        /// <param name="ByAirStatus"></param>
        /// <param name="ByHotelStatus"></param>
        /// <param name="ByVehicleStatus"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <returns></returns>
        public DataTable GetTravelManifestWithCount(string DateFrom, string DateTo, string UserID, string DateFilter,
           string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
           string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
           string Gender, string Rank,
            int ByVessel, int ByName, int ByRecLoc,
            int ByE1ID, int ByDateOnOff, int ByDateArrDep, int ByStatus, int ByBrand, int ByPort,
            int ByRank, int ByAirStatus, int ByHotelStatus, int ByVehicleStatus, int StartRow, int MaxRow)
        {
            DataTable ManifestDataTable = null;
            try
            {
                ManifestDataTable = SeafarerTravelDAL.GetTravelManifestWithCount(DateFrom, DateTo, UserID, DateFilter,
                GlobalCode.Field2Int(RegionId).ToString(), GlobalCode.Field2Int(CountryId).ToString(), GlobalCode.Field2Int(CityId).ToString(), Status, ByNameOrID, filterNameOrID,
                GlobalCode.Field2Int(PortId).ToString(), GlobalCode.Field2Int(HotelId).ToString(), VehicleId, VesselId, Nationality, Gender, Rank,
                ByVessel, ByName, ByRecLoc,
                ByE1ID, ByDateOnOff, ByDateArrDep, ByStatus, ByBrand, ByPort,
                ByRank, ByAirStatus, ByHotelStatus, ByVehicleStatus, StartRow, MaxRow, GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]));
                return ManifestDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ManifestDataTable != null)
                {
                    ManifestDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   14/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Travel Manifest count
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="UserID"></param>
        /// <param name="DateFilter"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="CityId"></param>
        /// <param name="Status"></param>
        /// <param name="ByNameOrID"></param>
        /// <param name="filterNameOrID"></param>
        /// <param name="PortId"></param>
        /// <param name="HotelId"></param>
        /// <param name="VehicleId"></param>
        /// <param name="VesselId"></param>
        /// <param name="Nationality"></param>
        /// <param name="Gender"></param>
        /// <param name="Rank"></param>
        /// <param name="ByVessel"></param>
        /// <param name="ByName"></param>
        /// <param name="ByRecLoc"></param>
        /// <param name="ByE1ID"></param>
        /// <param name="ByDateOnOff"></param>
        /// <param name="ByDateArrDep"></param>
        /// <param name="ByStatus"></param>
        /// <param name="ByBrand"></param>
        /// <param name="ByPort"></param>
        /// <param name="ByRank"></param>
        /// <param name="ByAirStatus"></param>
        /// <param name="ByHotelStatus"></param>
        /// <param name="ByVehicleStatus"></param>
        /// <returns></returns>
        public int GetTravelManifestCount(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank,
             int ByVessel, int ByName, int ByRecLoc,
             int ByE1ID, int ByDateOnOff, int ByDateArrDep, int ByStatus, int ByBrand, int ByPort,
             int ByRank, int ByAirStatus, int ByHotelStatus, int ByVehicleStatus)
        {

            return SeafarerTravelDAL.GetTravelManifestCount(DateFrom, DateTo, UserID, DateFilter,
             RegionId, CountryId, CityId, Status, ByNameOrID, filterNameOrID,
             PortId, HotelId, VehicleId, VesselId, Nationality, Gender, Rank,
             ByVessel, ByName, ByRecLoc, ByE1ID, ByDateOnOff, ByDateArrDep, ByStatus,
             ByBrand, ByPort, ByRank, ByAirStatus, ByHotelStatus, ByVehicleStatus, GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]));
        }
        /// <summary>
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get rank list by vessel       
        /// </summary>
        /// <param name="vessel"></param>
        /// <returns></returns>
        public static DataTable GetRankByVessel(string vessel)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelDAL.GetRankByVessel(vessel);
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
        /// Date Created:   26/03/2013
        /// Created By:     Marco Abejar
        /// (description)   Get Cost Center By Rank      
        /// </summary>
        /// <param name="vessel"></param>
        /// <returns></returns>
        public static string GetCostCenterByRank(string rank)
        {
            
            try
            {
               return SeafarerTravelDAL.GetCostCenterByRank(rank);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  04/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle manifest (test)        
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo"></param>
        /// <param name="UserID"></param>
        /// <param name="DateFilter"></param>
        /// <param name="RegionId"></param>
        /// <param name="CountryId"></param>
        /// <param name="CityId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static DataTable GetVehicleManifest(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank)
        {
            DataTable ManifestDataTable = null;
            try
            {
                ManifestDataTable = SeafarerTravelDAL.GetVehicleManifest(DateFrom, DateTo, UserID, DateFilter,
                RegionId, CountryId, CityId, Status, ByNameOrID, filterNameOrID,
                PortId, HotelId, VehicleId, VesselId, Nationality, Gender, Rank);
                return ManifestDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ManifestDataTable != null)
                {
                    ManifestDataTable.Dispose();
                }
            }
        }
        /// <summary>                                               
        /// Date Created: 4/10/2011
        /// Created By: Gabriel Oquialda
        /// (description)  Get vehicle manifest
        /// </summary>         
        public static DataTable GetSFVehicleTravelListView(string DateFromString,
            string DateToString, string DateFilter, string ByNameOrID, string NameString, string strUser, string Status,
            string Nationality, string Gender, string Rank, string Vessel, string Region, string Country, string City,
            string Port, string Hotel, string Vehicle, string Role)
        {
            DataTable VehicleDataTable = null;
            try
            {
                VehicleDataTable = SeafarerTravelDAL.GetSFVehicleTravelListView(DateFromString,
                    DateToString, DateFilter, ByNameOrID, NameString, strUser, Status, Nationality, Gender,
                    Rank, Vessel, Region, Country, City, Port, Hotel, Vehicle, Role);
                return VehicleDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VehicleDataTable != null)
                {
                    VehicleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   24/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Check if LOE has been scanned by the user  
        /// </summary>
        /// <param name="TravelReqID"></param>
        /// <param name="ManualReqID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static bool IsLOEScanned(string TravelReqID, string ManualReqID, string UserID)
        {
            try
            {
                return SeafarerTravelDAL.IsLOEScanned(TravelReqID, ManualReqID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:25/11/2011
        /// Created By: Charlene Remotigue
        /// Description: tag seafarer as scanned      
        /// </summary>
        /// 
        public static Int32 uspTagSeafarerAsScanned(Int32 returnVal, Int32 e1Id, Int32 mReqId, Int32 tReqId, string uId,
                string uRole)
        {

            try
            {
                return SeafarerTravelDAL.uspTagSeafarerAsScanned(returnVal, e1Id, mReqId, tReqId, uId, uRole);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>                        
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of seafarer with pending hotel bookings
        /// </summary>          
        public DataTable GetSFHotelTravelPending(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int StartRow, int MaxRow, int BranchId)
        {
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                ds = SeafarerTravelDAL.GetSFHotelTravelPending(DateFrom, DateTo, UserID,
                 FilterByDate, RegionID, CountryID, CityID, Status,
                 FilterByNameID, FilterNameID, PortID, VesselID, Nationality,
                 Gender, Rank, Role, ByVessel, ByName, ByE1ID,
                 ByDateOnOff, ByStatus, ByPort, ByRank, StartRow, MaxRow, BranchId);

                SeafarerTravelBLL.SFHotelTravelPendingCount = GlobalCode.Field2Int(ds.Tables[0].Rows[0][0].ToString());
                dt = ds.Tables[1];
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
                if (ds != null)
                {
                    ds.Dispose();                    
                }
            }
        }
        /// <summary>
        /// Date Created:   06/02/2011
        /// Created By:     Josephine Gad
        /// (description)   Storage of Pending Hotel Transaction Count
        /// </summary>
        private static Int32 _pendingCount;
        public static Int32 SFHotelTravelPendingCount
        { 
            get
            {
                return _pendingCount;
            }
            set
            {
                _pendingCount = value;
            }            
        }
        /// <summary>            
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get total row count of pending hotel bookings
        /// --------------------------------------------------------
        /// Date Created:   06/02/2011
        /// Created By:     Josephine Gad
        /// (description)   Return the count from assigned variable
        ///                 Do not use the SeafarerTravelDAL.GetSFHotelTravelPendingCount
        /// </summary>
        public int GetSFHotelTravelPendingCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int BranchId)
        {            
            //return SeafarerTravelDAL.GetSFHotelTravelPendingCount(DateFrom, DateTo, UserID,
            //     FilterByDate, RegionID, CountryID, CityID, Status,
            //     FilterByNameID, FilterNameID, PortID, VesselID, Nationality,
            //     Gender, Rank, Role, BranchId);

            return SeafarerTravelBLL.SFHotelTravelPendingCount;
        }
        /// <summary>                        
        /// Date Created:   11/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of seafarer with pending vehicle bookings
        /// </summary>           
        public DataTable GetSFVehicleTravelPending(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int StartRow, int MaxRow)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelDAL.GetSFVehicleTravelPending(DateFrom, DateTo, UserID,
                 FilterByDate, RegionID, CountryID, CityID, Status,
                 FilterByNameID, FilterNameID, PortID, VesselID, Nationality,
                 Gender, Rank, Role, ByVessel, ByName, ByE1ID,
                 ByDateOnOff, ByStatus, ByPort, ByRank, StartRow, MaxRow);
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
        /// Date Created:   11/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get total row count of pending vehicle bookings
        /// --------------------------------------------------------
        /// </summary>
        public int GetSFVehicleTravelPendingCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank)
        {
            return SeafarerTravelDAL.GetSFVehicleTravelPendingCount(DateFrom, DateTo, UserID,
                 FilterByDate, RegionID, CountryID, CityID, Status,
                 FilterByNameID, FilterNameID, PortID, VesselID, Nationality,
                 Gender, Rank, Role);
        }
        /// <summary>
        /// Date Created:   18/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Travel Manifest Dashboard      
        /// </summary>        
        public static IDataReader GetTravelManifestDashboard(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank, string Role)
        {
            return SeafarerTravelDAL.GetTravelManifestDashboard(DateFrom, DateTo, UserID,
                DateFilter, RegionId, CountryId, CityId, Status, ByNameOrID, filterNameOrID,
                PortId, HotelId, VehicleId, VesselId, Nationality, Gender, Rank, Role);
        }
        public static IDataReader GetTravelManifestDashboardExists(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank, string Role)
        {
            return SeafarerTravelDAL.GetTravelManifestDashboardExists(DateFrom, DateTo, UserID,
              DateFilter, RegionId, CountryId, CityId, Status, ByNameOrID, filterNameOrID,
              PortId, HotelId, VehicleId, VesselId, Nationality, Gender, Rank, Role);
        }

        /// <summary>
        /// Date Created:    15/12/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Get all Travel Manifest Dashboard       
        /// --------------------------------------------------
        /// </summary>    
        public static IDataReader GetTravelRequestDashboard(short loadType, DateTime DateFrom, string UserID, string Role)
        {
            return SeafarerTravelDAL.GetTravelRequestDashboard(loadType, DateFrom, UserID, Role);
        }


        /// <summary>                        
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of seafarer with pending hotel bookings
        /// </summary>           
        public DataTable GetSFHotelTravelNoTransaction(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int StartRow, int MaxRow, int BranchId)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelDAL.GetSFHotelTravelNoTransaction(DateFrom, DateTo, UserID,
                 FilterByDate, RegionID, CountryID, CityID, Status,
                 FilterByNameID, FilterNameID, PortID, VesselID, Nationality,
                 Gender, Rank, Role, ByVessel, ByName, ByE1ID,
                 ByDateOnOff, ByStatus, ByPort, ByRank, StartRow, MaxRow, BranchId);
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
        /// Date Created:   19/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get total row count of no hotel transaction
        /// --------------------------------------------------------
        /// </summary>
        public static int GetSFHotelTravelNoTransactionCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int BranchId)
        {
            return SeafarerTravelDAL.GetSFHotelTravelNoTransactionCount(DateFrom, DateTo, UserID,
             FilterByDate, RegionID, CountryID, CityID, Status,
             FilterByNameID, FilterNameID, PortID, VesselID, Nationality,
             Gender, Rank, Role, ByVessel, ByName, ByE1ID,
             ByDateOnOff, ByStatus, ByPort, ByRank, BranchId);
        }

        /// <summary>            
        /// Date Created:   22/12/2011
        /// Created By:     Muhallidin G Wali 
        /// (description)   Get between date Manifest 
        /// --------------------------------------------------------        
        /// Date Modified:   11/01/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add FilterByName, Region, Country, City, Port and HotelID Parameters
        /// --------------------------------------------------------
        /// </summary>        
        //public static DataTable GetSelectTravelReqManifestWithCount(Int16 LoadType, DateTime FromDate, DateTime ToDate, string UserID, string Role, string OrderBy, int StartRow, int MaxRow, string VesselID, string SeafarerID, string NationalityID, string Gender, string RankID, string Status)            
        public static DataTable GetSelectTravelReqManifestWithCount(Int16 LoadType, DateTime FromDate, DateTime ToDate, 
            string UserID, string Role,string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID)
        {
            //return SeafarerTravelDAL.GetSelectTravelReqManifestWithCount(GlobalCode.Field2TinyInt(LoadType), GlobalCode.Field2DateTime(FromDate), GlobalCode.Field2DateTime(ToDate), UserID, Role, Orderby, StartRow, MaxRow);
            return SeafarerTravelDAL.GetSelectTravelReqManifestWithCount(LoadType, GlobalCode.Field2DateTime(FromDate), 
                GlobalCode.Field2DateTime(ToDate), UserID, Role, OrderBy, StartRow, MaxRow,
                GlobalCode.Field2Int(VesselID), GlobalCode.Field2Int(FilterByName), SeafarerID, GlobalCode.Field2Int(NationalityID), 
                GlobalCode.Field2Int(Gender), GlobalCode.Field2Int(RankID), Status,
                GlobalCode.Field2Int(RegionID), GlobalCode.Field2Int(CountryID), 
                GlobalCode.Field2Int(CityID), GlobalCode.Field2Int(PortID), GlobalCode.Field2Int(HotelID));
        }

        /// <summary>            
        /// Date Created:    22/12/2011
        /// Created By:      Muhallidin G Wali 
        /// (description)    Get between date count Manifest 
        /// --------------------------------------------------------
        /// Date Modified:   11/01/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add FilterByName, Region, Country, City, Port and HotelID Parameters
        /// --------------------------------------------------------
        /// Date Modified:   24/01/2012
        /// Modified By:     Josephine Gad
        /// (description)    Close temp Table
        /// --------------------------------------------------------
        /// </summary>
        public static int GetSelectTravelReqManifestCount(Int16 LoadType, DateTime FromDate, DateTime ToDate, 
            string UserID, string Role, string OrderBy, string VesselID, string FilterByName, 
            string SeafarerID, string NationalityID, string Gender, string RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID)
        {
            DataTable temp = null;
            try
            {
                int count = 0;
                temp = SeafarerTravelDAL.GetSelectTravelReqManifestWithCount(1, GlobalCode.Field2DateTime(FromDate), GlobalCode.Field2DateTime(ToDate),
                    UserID, Role, OrderBy, 0, 0, GlobalCode.Field2Int(VesselID), GlobalCode.Field2Int(FilterByName), SeafarerID,
                    GlobalCode.Field2Int(NationalityID), GlobalCode.Field2Int(Gender), GlobalCode.Field2Int(RankID), Status,
                    GlobalCode.Field2Int(RegionID), GlobalCode.Field2Int(CountryID),
                    GlobalCode.Field2Int(CityID), GlobalCode.Field2Int(PortID), GlobalCode.Field2Int(HotelID));
                if (temp != null && temp.Rows.Count > 0)
                {
                    count = (int)temp.Rows[0][0];
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (temp != null)
                {
                    temp.Dispose();
                }
            }
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   16/08/2012
        /// Description:    Tag seafarer   
        /// ---------------------------------
        /// Modified By:    Mabejar Gad
        /// Date Modified:  10/04/2013
        /// Description:    Add TagTime
        /// </summary>
        /// 
        public static void InsertTag(string sIdBigint, string sTRId, string sUser, string sRole,
            string sAirport, string sPort, string sBranch, string sTagtime, string sRecLoc, string sE1Id, string sStatusOnOff,
            string Description, string Function, string PathName, string TimeZone, DateTime GMTDate)
        {
            SeafarerTravelDAL.InsertTag(sIdBigint, sTRId, sUser, sRole, sAirport, sPort, sBranch, sTagtime,
                sRecLoc, sE1Id, sStatusOnOff, Description, Function, PathName, TimeZone, GMTDate);
        }
        
        /// <summary>
        /// Created By:     Marco Abejar
        /// Date Created:   19/03/2013
        /// Description:    Add or update confirmation
        /// </summary>
        /// 
        public static void UpdateConfirmation(string sIdBigint, string sTRId, string sUser, string sRole,
            string sBranch, string sConfirmation, string strLogDescription,
            string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            SeafarerTravelDAL.UpdateConfirmation(sIdBigint, sTRId, sUser, sRole, "0", "0", sBranch, sConfirmation,
                strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
        }
    }
}

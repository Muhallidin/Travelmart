using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;

namespace TRAVELMART.DAL
{
    public class SeafarerTravelDAL
    {
        #region METHODS

        /// <summary>                                               
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description) Selecting Arriving Seafarer base on flight details   
        /// ----------------------------------------------------------------
        /// Date Modified: 01/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter seafarer name
        /// ----------------------------------------------------------------
        /// Date Modified: 04/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter for Date Range
        /// ----------------------------------------------------------------
        /// Date Modified: 15/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter strRegion and strUser
        /// ----------------------------------------------------------------
        /// Date Modified: 17/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter strFilterBy
        /// </summary>
        public static DataTable GetSFTravelList(string sfStatus, string strFlightDateRange, string strPendingFilter,
            string strAirStatusFilter, string strSeafarerName, string DateFromString, string DateToString, string strUser,
            string strRegion, string strFilterBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                string SFDateFrom;
                string SFDateTo;
                if (strFlightDateRange == "ByDate")
                {
                    SFDateFrom = DateFromString;
                    SFDateTo = DateToString;
                }
                else
                {
                    string[] SFDateRange = strFlightDateRange.Split('-');
                    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFTravelList");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pPendingFilter", DbType.String, strPendingFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pAirStatusFilter", DbType.String, strAirStatusFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, strSeafarerName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(strRegion));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, strFilterBy);
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
        /// Date Created:   14/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Selecting list of hotel transaction details    
        /// ---------------------------------------------------------
        /// Date Modified:  05/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter for Date Range
        /// ---------------------------------------------------------
        /// Date Modified:  08/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter seafarer name
        /// ---------------------------------------------------------
        /// Date Modified:  16/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter strMapRef, strUser and strFilterBy
        /// </summary>           
        public static DataTable GetSFHotelTravelList(string sfStatus, string strFlightDateRange, string DateFromString,
            string DateToString, string NameString, string strUser, string strMapRef, string strFilterBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                string SFDateFrom;
                string SFDateTo;
                if (strFlightDateRange == "ByDate")
                {
                    SFDateFrom = DateFromString;
                    SFDateTo = DateToString;
                }
                else
                {
                    string[] SFDateRange = strFlightDateRange.Split('-');
                    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelList");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, strFilterBy);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>                        
        /// Date Created:   4/10/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting list of hotel transaction details    
        /// </summary>           
        public static DataTable GetSFHotelTravelListView(string sfStatus, string strFlightDateRange, string DateFromString,
            string DateToString, string NameString, string strUser, string strMapRef, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status,
            string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                //string SFDateFrom;
                //string SFDateTo;
                //if (strFlightDateRange == "ByDate")
                //{
                //    SFDateFrom = DateFromString;
                //    SFDateTo = DateToString;
                //}
                //else
                //{
                //    string[] SFDateRange = strFlightDateRange.Split('-');
                //    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                //    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                //}
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelListView");
                //SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateToString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);

                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>                        
        /// Date Created:   4/10/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting list of hotel transaction details  
        /// ----------------------------------------------
        /// Date Modified:  01/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Delete Parameter sfStatus, strFlightDateRange and DateToString
        /// </summary>           
        public static DataTable GetSFHotelTravelListView2(string DateFromString,
            string NameString, string strUser, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status,
            string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelListView2");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);
                //SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateToString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                //SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);

                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                //SFDatebase.AddInParameter(SFDbCommand, "@pHotelFilter", DbType.Int32, HotelFilter);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>                        
        /// Date Created:   4/10/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting list of hotel transaction details    
        /// </summary>           
        public static DataTable GetSFHotelTravelListView_ByHour(string sfStatus, string strFlightDateRange, string DateFromString,
            string NameString, string strUser, string strMapRef, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status,
            string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole, Boolean ByHour, Int32 HotelFilter)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                //string SFDateFrom;
                //string SFDateTo;
                //if (strFlightDateRange == "ByDate")
                //{
                //    SFDateFrom = DateFromString;
                //    SFDateTo = DateToString;
                //}
                //else
                //{
                //    string[] SFDateRange = strFlightDateRange.Split('-');
                //    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                //    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                //}
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelListView_ByHour");
                //SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);
                //SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateToString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);

                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pByHour", DbType.Boolean, ByHour);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelFilter", DbType.Int32, HotelFilter);

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>                        
        /// Date Created:   4/10/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting list of hotel transaction details    
        /// </summary>           
        public static DataTable GetSFHotelTravelListView_ByHour3(string sfStatus, string strFlightDateRange, string DateFromString,
            string NameString, string strUser, string strMapRef, string DateFilter, string ByNameOrID, string filterNameOrID,
            string Nationality, string Gender, string Rank, string Status,
            string Region, string Country, string City, string Port, string Hotel, string Vessel, string UserRole, Boolean ByHour)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                //string SFDateFrom;
                //string SFDateTo;
                //if (strFlightDateRange == "ByDate")
                //{
                //    SFDateFrom = DateFromString;
                //    SFDateTo = DateToString;
                //}
                //else
                //{
                //    string[] SFDateRange = strFlightDateRange.Split('-');
                //    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                //    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                //}
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelListView_ByHour2");
                //SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);
                //SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateToString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);

                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pByHour", DbType.Boolean, ByHour);

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>    
        /// Date Created: 01/08/2011
        /// Created By: Ryan Bautista
        /// (description) Searching in hotel transaction details
        /// ----------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable
        /// </summary>    
        public static DataTable GetSFHotelTravelListSearch(string sfStatus, string strFlightDateRange, string SFName)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                string[] SFDateRange = strFlightDateRange.Split('-');
                string SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                string SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelListSearch");
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFName", DbType.String, SFName);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 14/07/2011
        /// Created By: Marco Abejar
        /// (description) Selecting list of vehicle travel details    
        /// ------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter seafarer name
        /// ------------------------------------------------------
        /// Date Modified: 05/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter for Date Range                        
        /// ---------------------------------------------------------
        /// Date Modified:  16/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter strMapRef, strUser and strFilterBy
        /// </summary>            
        public static DataTable GetSFVehicleTravelDetails(string sfStatus, string strFlightDateRange, string strSeafarerName,
            string DateFromString, string DateToString, string strUser, string strMapRef, string strFilterBy)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                string SFDateFrom;
                string SFDateTo;
                if (strFlightDateRange == "ByDate")
                {
                    SFDateFrom = DateFromString;
                    SFDateTo = DateToString;
                }
                else
                {
                    string[] SFDateRange = strFlightDateRange.Split('-');
                    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFVehicleTravelList");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, strSeafarerName);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, strFilterBy);
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
        /// Date Created:   14/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Selecting list of port transaction details    
        /// --------------------------------------------------------
        /// Date Modified:  05/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter for Date Range   
        /// ----------------------------------------------------------------
        /// Date Modified:  16/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter strMapRef, strUser and strFilterBy
        /// ----------------------------------------------------------------
        /// Date Modified:  13/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter Country, City, Port, Nationality and others
        /// </summary>    
        public static DataTable GetSFPortTravelDetails(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                //string SFDateFrom;
                //string SFDateTo;
                //if (strFlightDateRange == "ByDate")
                //{
                //    SFDateFrom = DateFromString;
                //    SFDateTo = DateToString;
                //}
                //else
                //{
                //    string[] SFDateRange = strFlightDateRange.Split('-');
                //    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                //    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                //}
                //SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFPortTravelList");
                //SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                //SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                //SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                //SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerName", DbType.String, strSeafarerName);
                //SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                //SFDatebase.AddInParameter(SFDbCommand, "@pMapIDInt", DbType.Int16, Int16.Parse(strMapRef));
                //SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, strFilterBy);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFPortTravelList");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.Date, DateTime.Parse(DateFrom));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.Date, DateTime.Parse(DateTo));

                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, Int32.Parse(CountryID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(CityID));

                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, (Status == "0" ? "" : Status));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.Int32, Int32.Parse(VesselID));

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int16, Int16.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.Int16, Int16.Parse(Rank));
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

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
        /// Date Created:   09/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get total row count of port transaction details    
        /// --------------------------------------------------------
        /// </summary>
        public static int GetSFPortTravelDetailsCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFPortTravelListCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.Date, DateTime.Parse(DateFrom));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.Date, DateTime.Parse(DateTo));

                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, Int32.Parse(CountryID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(CityID));

                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, (Status == "0" ? "" : Status));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.Int32, Int32.Parse(VesselID));

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int16, Int16.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.Int16, Int16.Parse(Rank));
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                dr = SFDatebase.ExecuteReader(SFDbCommand);  
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   09/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get port transaction details    
        /// --------------------------------------------------------
        /// </summary>
        public static DataTable GetSFPortTravelDetailsWithCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank, string Role,
            int ByVessel, int ByName, int ByRecLoc, int ByE1ID, int ByDateOnOff, int ByStatus,
            int ByPort, int ByRank, int ByPortStatus, int ByNationality, int StartRow, int MaxRow)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFPortTravelListWithCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.Date, DateTime.Parse(DateFrom));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.Date, DateTime.Parse(DateTo));

                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, Int32.Parse(CountryID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(CityID));

                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, (Status == "0" ? "" : Status));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.Int32, Int32.Parse(VesselID));

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int16, Int16.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.Int16, Int16.Parse(Rank));
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);               

                SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRecLoc", DbType.Int16, ByRecLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);
                SFDatebase.AddInParameter(SFDbCommand, "@pByPortStatus", DbType.Int16, ByPortStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByNationality", DbType.Int16, ByNationality);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int16, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int16, MaxRow);

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
        /// Date Created:   14/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Selecting list of air travel details  
        /// --------------------------------------------------
        /// Date Modified:  02/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter seafarer name
        /// --------------------------------------------------
        /// Date Modified:  05/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter for Date Range          
        /// ----------------------------------------------------------------
        /// Date Modified:  16/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter strMapRef, strUser and strFilterBy    
        /// ----------------------------------------------------------------
        /// Date Modified:  12/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter Country, City, PortID, Nationality, etc
        /// </summary>     
        public static DataTable GetSFAirTravelDetails(string DateFrom, string DateTo, string AirStatusFilter,
            string UserID, string FilterByDate, string RegionID, string CountryID, string CityID,
            string Status, string FilterByNameID, string FilterNameID, string PortID, string VesselID,
            string Nationality, string Gender, string Rank)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                //string SFDateFrom;
                //string SFDateTo;
                //if (strFlightDateRange == "ByDate")
                //{
                //    SFDateFrom = DateFromString;
                //    SFDateTo = DateToString;
                //}
                //else
                //{
                //    string[] SFDateRange = strFlightDateRange.Split('-');
                //    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                //    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                //}
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFAirTravelList");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.Date, DateTime.Parse(DateFrom));
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.Date, DateTime.Parse(DateTo));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirStatusFilter", DbType.String, AirStatusFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);

                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryID));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityID));

                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, (Status == "0" ? "" : Status));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.Int16, Int16.Parse(FilterByNameID));
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int16, Int16.Parse(PortID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.Int16, Int16.Parse(VesselID));
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.Int16, Int16.Parse(Nationality));
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int16, Int16.Parse(Gender));
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.Int16, Int16.Parse(Rank));
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
        /// Date Created: 03/08/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer count per flight status
        /// ---------------------------------------------------------------------
        /// Date Modified: 04/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter for Date Range
        ///               Change ExecuteScalar to ExecuteDataSet to get the count                         
        /// ---------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FilterByString            
        /// </summary>
        public static string GetAirStatusCount(string sfStatus, string strFlightDateRange, string strAirStatusFilter,
            string DateFromString, string DateToString, string FilterByString)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            String strCount = "0";
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DataTable AirDataTable = null;
            try
            {
                string SFDateFrom;
                string SFDateTo;
                if (strFlightDateRange == "ByDate")
                {
                    SFDateFrom = DateFromString;
                    SFDateTo = DateToString;
                }
                else
                {
                    string[] SFDateRange = strFlightDateRange.Split('-');
                    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFAirStatusCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolAirStatusVarchar", DbType.String, strAirStatusFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, FilterByString);
                //strCount = SFDatebase.ExecuteScalar(SFDbCommand).ToString();
                AirDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                strCount = AirDataTable.Rows.Count.ToString();
                return strCount;
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
                if (strCount != null)
                {
                    strCount = string.Empty;
                }
                if (AirDataTable != null)
                {
                    AirDataTable.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 03/08/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer count with pending transactions
        /// ---------------------------------------------------------------------
        /// Date Modified: 05/08/2011
        /// Modified By: Josephine Gad
        /// (description) Add parameter for Date Range
        ///               Change ExecuteScalar to ExecuteDataSet to get the count         
        /// ---------------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FilterByString
        /// </summary>
        public static string GetPendingTransactionCount(string sfStatus, string strFlightDateRange, string strPendingFilter,
            string DateFromString, string DateToString, string FilterByString)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            String strCount = "0";
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DataTable PendingDataTable = null;
            try
            {
                string SFDateFrom;
                string SFDateTo;
                if (strFlightDateRange == "ByDate")
                {
                    SFDateFrom = DateFromString;
                    SFDateTo = DateToString;
                }
                else
                {
                    string[] SFDateRange = strFlightDateRange.Split('-');
                    SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                    SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFPendingCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pPendingFilter", DbType.String, strPendingFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, FilterByString);
                //strCount = SFDatebase.ExecuteScalar(SFDbCommand).ToString();                
                PendingDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                strCount = PendingDataTable.Rows.Count.ToString();

                return strCount;
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
                if (strCount != null)
                {
                    strCount = string.Empty;
                }
                if (PendingDataTable != null)
                {
                    PendingDataTable.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created:   03/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get seafarer count based on date range (onsigning/offsigning)    
        /// ---------------------------------------------------------------------------
        /// Date Modified:  06/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add parameter FilterByString
        /// </summary>
        public static string GetSeafarerCountByDateRange(string sfStatus, string strFlightDateRange, string FilterByString)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            String strCount = "0";
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DataTable SFDataTable = null;
            try
            {


                string[] SFDateRange = strFlightDateRange.Split('-');
                string SFDateFrom = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[0])).ToShortDateString();
                string SFDateTo = DateTime.Now.AddDays(Convert.ToInt32(SFDateRange[1])).ToShortDateString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSFCountByDateRange");
                SFDatebase.AddInParameter(SFDbCommand, "@pcolSFStatus", DbType.String, sfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, SFDateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, SFDateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterBy", DbType.String, FilterByString);
                SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                strCount = SFDataTable.Rows.Count.ToString();
                return strCount;
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
                if (strCount != null)
                {
                    strCount = string.Empty;
                }
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
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
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelManifest");

                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityId));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortId);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, VehicleId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselId);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);

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
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Travel Manifest with row count and order by
        /// </summary>
        public static DataTable GetTravelManifestWithCount(string DateFrom, string DateTo, string UserID, string DateFilter,
           string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
           string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
           string Gender, string Rank,
            int ByVessel, int ByName, int ByRecLoc,
            int ByE1ID, int ByDateOnOff, int ByDateArrDep, int ByStatus, int ByBrand, int ByPort,
            int ByRank, int ByAirStatus, int ByHotelStatus, int ByVehicleStatus, int StartRow, int MaxRow, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelManifestWithCount");

                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryId));


                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityId));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortId);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, VehicleId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselId);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);

                SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRecLoc", DbType.Int16, ByRecLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateArrDep", DbType.Int16, ByDateArrDep);
                SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByBrand", DbType.Int16, ByBrand);
                SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);
                SFDatebase.AddInParameter(SFDbCommand, "@pByAirStatus", DbType.Int16, ByAirStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByHotelStatus", DbType.Int16, ByHotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByVehicleStatus", DbType.Int16, ByVehicleStatus);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int16, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int16, MaxRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

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
        /// Date Created:   14/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Travel Manifest count
        /// </summary>
        public static int GetTravelManifestCount(string DateFrom, string DateTo, string UserID, string DateFilter,
           string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
           string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
           string Gender, string Rank,
            int ByVessel, int ByName, int ByRecLoc,
            int ByE1ID, int ByDateOnOff, int ByDateArrDep, int ByStatus, int ByBrand, int ByPort,
            int ByRank, int ByAirStatus, int ByHotelStatus, int ByVehicleStatus, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelManifestCount");

                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityId));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortId);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, VehicleId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselId);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);

                SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRecLoc", DbType.Int16, ByRecLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateArrDep", DbType.Int16, ByDateArrDep);
                SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByBrand", DbType.Int16, ByBrand);
                SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);
                SFDatebase.AddInParameter(SFDbCommand, "@pByAirStatus", DbType.Int16, ByAirStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByHotelStatus", DbType.Int16, ByHotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByVehicleStatus", DbType.Int16, ByVehicleStatus);

                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                dr = SFDatebase.ExecuteReader(SFDbCommand);

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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
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
            DbCommand dbCommand = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                dbCommand = db.GetStoredProcCommand("uspGetSFRank");
                db.AddInParameter(dbCommand, "@pVessel", DbType.Int32, Int32.Parse(vessel));
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
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
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
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
            DbCommand dbCommand = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                dbCommand = db.GetStoredProcCommand("uspGetCostCenterByRank");
                db.AddInParameter(dbCommand, "@pRankId", DbType.Int32, Int32.Parse(rank));
                return db.ExecuteScalar(dbCommand).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   04/10/2011
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
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable SFDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVehicleViewList");

                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityId));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortId);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, VehicleId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselId);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);

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
        /// Date Created:   4/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get vehicle manifest
        /// </summary>           
        public static DataTable GetSFVehicleTravelListView(string DateFromString,
            string DateToString, string DateFilter, string ByNameOrID, string NameString, string strUser, string Status,
            string Nationality, string Gender, string Rank, string Vessel, string Region, string Country, string City,
            string Port, string Hotel, string Vehicle, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = SFDatebase.CreateConnection();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFVehicleTravelListView");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateToString);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, strUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);             
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, NameString);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pVessel", DbType.String, Vessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, Region);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, Country);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, City);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, Port);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, Hotel);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, Vehicle);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                dt = new DataTable();
                dt.Load(dataReader);
                return dt;

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
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
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
            DataTable dt = null;
            DbCommand command = null;
            try 
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspGetLOEScan");
                db.AddInParameter(command, "@pRequestIDInt", DbType.Int64, Int64.Parse(ManualReqID));
                db.AddInParameter(command, "@pTravelReqIDInt", DbType.Int64, Int64.Parse(TravelReqID));
                db.AddInParameter(command, "@pUserNameVarchar", DbType.String, UserID);
                dt = db.ExecuteDataSet(command).Tables[0];
                if(dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
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
        /// Date Created:25/11/2011
        /// Created By: Charlene Remotigue
        /// Description: tag seafarer as scanned      
        /// </summary>
        /// 
        public static Int32 uspTagSeafarerAsScanned(Int32 returnVal, Int32 e1Id, Int32 mReqId, Int32 tReqId, string uId,
                string uRole)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspTagSeafarerAsScanned");
                SFDatebase.AddInParameter(SFDbCommand, "@pE1Id", DbType.Int32, e1Id);
                SFDatebase.AddOutParameter(SFDbCommand, "@pReturnValue", DbType.Int32, returnVal);
                SFDatebase.AddInParameter(SFDbCommand, "@pManualRequestId", DbType.Int32, mReqId);
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelRequestId", DbType.Int32, tReqId);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserId", DbType.String, uId);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserRole", DbType.String, uRole);
                SFDatebase.ExecuteNonQuery(SFDbCommand);
                returnVal = Int32.Parse(SFDbCommand.Parameters["@pReturnValue"].Value.ToString());
                return returnVal;
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

            }
        }
        /// <summary>                        
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of seafarer with pending hotel bookings
        /// </summary>           
        public static DataSet GetSFHotelTravelPending(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int StartRow, int MaxRow, int BranchId)
        {                        
            DbCommand SFDbCommand = null;                       
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelPendingList");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, FilterByNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselID);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, BranchId);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int16, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, MaxRow);
                                
                return SFDatebase.ExecuteDataSet(SFDbCommand);                    
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
            }
        }
        /// <summary>            
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get total row count of pending hotel bookings
        /// --------------------------------------------------------
        /// </summary>
        public static int GetSFHotelTravelPendingCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int BranchId)
        {            
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelPendingListCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, FilterByNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselID);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, BranchId);

                dr = SFDatebase.ExecuteReader(SFDbCommand);
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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }
        /// <summary>                        
        /// Date Created:   11/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting list of seafarer with pending vehicle bookings
        /// </summary>           
        public static DataTable GetSFVehicleTravelPending(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int StartRow, int MaxRow)
        {
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFVehicleTravelPendingList");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, FilterByNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselID);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int16, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return dt;

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
        public static int GetSFVehicleTravelPendingCount(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role)
        {
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFvehicleTravelPendingListCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, FilterByNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselID);
                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                dr = SFDatebase.ExecuteReader(SFDbCommand);

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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:    18/11/2011
        /// Created By:      Josephine Gad
        /// (description)    Get Travel Manifest Dashboard      
        /// -------------------------------------------------
        /// Date Modified:   27/11/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to IDataReader
        /// </summary>        
        public static IDataReader GetTravelManifestDashboard(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelManifestDashboard");

                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityId));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortId);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, VehicleId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselId);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                dr = SFDatebase.ExecuteReader(SFDbCommand);
                return dr;
            }
            catch (Exception ex)
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                //if (dr != null)
                //{
                //    dr.Close();
                //    dr.Dispose();
                //}
            }
        }
        public static IDataReader GetTravelManifestDashboardExists(string DateFrom, string DateTo, string UserID, string DateFilter,
            string RegionId, string CountryId, string CityId, string Status, string ByNameOrID, string filterNameOrID,
            string PortId, string HotelId, string VehicleId, string VesselId, string Nationality,
            string Gender, string Rank, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelManifestDashboardIsExists");

                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, DateFilter);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int16, Int16.Parse(RegionId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int16, Int16.Parse(CountryId));
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int16, Int16.Parse(CityId));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, ByNameOrID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, filterNameOrID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortId);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVehicleIDInt", DbType.String, VehicleId);
                SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselId);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                dr = SFDatebase.ExecuteReader(SFDbCommand);
                return dr;
            }
            catch (Exception ex)
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }              
            }
        }

        /// <summary>
        /// Date Created:    15/12/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Get all Travel Manifest Dashboard       
        /// --------------------------------------------------
        /// </summary>    


        public static IDataReader GetTravelRequestDashboard(short loadType, DateTime DateFrom, string UserID, string Role)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelRequestDashboard");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, loadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pDate", DbType.DateTime, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                dr = SFDatebase.ExecuteReader(SFDbCommand);

                return dr;

            }
            catch (Exception ex)
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
            }
        }



        /// <summary>                        
        /// Date Created:   19/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of no hotel transaction
        /// </summary>           
        public static DataTable GetSFHotelTravelNoTransaction(string DateFrom, string DateTo, string UserID,
            string FilterByDate, string RegionID, string CountryID, string CityID, string Status,
            string FilterByNameID, string FilterNameID, string PortID, string VesselID, string Nationality,
            string Gender, string Rank, string Role, int ByVessel, int ByName, int ByE1ID,
            int ByDateOnOff, int ByStatus, int ByPort, int ByRank, int StartRow, int MaxRow, int BranchId)
        {
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelNoTransaction");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, CountryID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, CityID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                //SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, FilterByNameID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                //SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                //SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, BranchId);

                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int16, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return dt;

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
            DbCommand SFDbCommand = null;
            IDataReader dr = null;
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectSFHotelTravelNoTransactionCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFrom);
                SFDatebase.AddInParameter(SFDbCommand, "@pSFDateTo", DbType.String, DateTo);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByDate", DbType.String, FilterByDate);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.String, RegionID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.String, CountryID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.String, CityID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);
                //SFDatebase.AddInParameter(SFDbCommand, "@pFilterByNameID", DbType.String, FilterByNameID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pFilterNameID", DbType.String, FilterNameID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.String, PortID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pVesselIDInt", DbType.String, VesselID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pNationality", DbType.String, Nationality);
                //SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.String, Gender);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRank", DbType.String, Rank);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);

                //SFDatebase.AddInParameter(SFDbCommand, "@pByVessel", DbType.Int16, ByVessel);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByName", DbType.Int16, ByName);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByE1ID", DbType.Int16, ByE1ID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByDateOnOff", DbType.Int16, ByDateOnOff);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByStatus", DbType.Int16, ByStatus);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByPort", DbType.Int16, ByPort);
                //SFDatebase.AddInParameter(SFDbCommand, "@pByRank", DbType.Int16, ByRank);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, BranchId);                

                dr = SFDatebase.ExecuteReader(SFDbCommand);

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
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created:   22/12/2011
        /// Created By:     Muhallidin G Wali 
        /// (description)   Get between date total Manifest 
        /// --------------------------------------------------------
        /// Date Modified:   11/01/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add FilterByName, Region, Country, City, Port and HotelID Parameters
        /// --------------------------------------------------------
        /// </summary>
        public static DataTable GetSelectTravelReqManifestWithCount(Int16 LoadType, DateTime FromDate, DateTime ToDate,
            string UserID, string Role, string OrderBy, int StartRow, int MaxRow, int VesselID, int FilterByName,
            string SeafarerID, int NationalityID, int Gender, int RankID, string Status,
            int RegionID, int CountryID, int CityID, int PortID, int HotelID)
        {
            //DataTable dt = null;
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectTravelReqManifestWithCount");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pFromDate", DbType.DateTime, FromDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pToDate", DbType.DateTime, ToDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, Role);
                SFDatebase.AddInParameter(SFDbCommand, "@pOrderBy", DbType.String, OrderBy);
                SFDatebase.AddInParameter(SFDbCommand, "@pStartRow", DbType.Int32, StartRow);
                SFDatebase.AddInParameter(SFDbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                SFDatebase.AddInParameter(SFDbCommand, "@pVesselID", DbType.Int32, VesselID);
                SFDatebase.AddInParameter(SFDbCommand, "@pFilterByName", DbType.Int16, FilterByName);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerID", DbType.String, SeafarerID);

                SFDatebase.AddInParameter(SFDbCommand, "@pNationalityID", DbType.Int32, NationalityID);
                SFDatebase.AddInParameter(SFDbCommand, "@pGender", DbType.Int32, Gender);
                SFDatebase.AddInParameter(SFDbCommand, "@pRankID", DbType.Int32, RankID);
                SFDatebase.AddInParameter(SFDbCommand, "@pStatus", DbType.String, Status);

                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, RegionID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, CountryID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, PortID);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelIDInt", DbType.Int32, HotelID);

                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return dt;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally 
            {
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   16/08/2012
        /// Description:    Tag seafarer  
        /// ---------------------------------
        /// Modified By:    Mabejar 
        /// Date Modified:  10/04/2013
        /// Description:    Add TagTime      
        /// ---------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  19/Jul/2013
        /// Description:    Add RecLoc, E1ID and StatusOnOff parameters
        ///                 Add Audit Trail data
        /// ---------------------------------
        /// </summary>
        /// 
        public static void InsertTag(string sIdBigint, string sTRId, string sUser, string sRole,
            string sAirport, string sPort, string sBranch, string sTagTime, string sRecLoc, string sE1Id, string sStatusOnOff,
            string Description, string Function, string PathName, string TimeZone, DateTime GMTDate)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertTag");
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int32, GlobalCode.Field2Int(sIdBigint));
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int32, GlobalCode.Field2Int(sTRId));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserNameVarchar", DbType.String, sUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserRoleVarchar", DbType.String, sRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pProcessDatetime", DbType.DateTime, CommonFunctions.GetCurrentDateTime());
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportIDInt", DbType.Int32, GlobalCode.Field2Int(sAirport));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIdInt", DbType.Int32, GlobalCode.Field2Int(sPort));
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedByVarchar", DbType.String, sUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, sBranch);
                SFDatebase.AddInParameter(SFDbCommand, "@pTagTime", DbType.Date, GlobalCode.Field2DateTimeWithTime(sTagTime));

                SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, sRecLoc);
                SFDatebase.AddInParameter(SFDbCommand, "@pSeafarerIdInt", DbType.String, sE1Id);
                SFDatebase.AddInParameter(SFDbCommand, "@pOnOffVarchar", DbType.String, sStatusOnOff);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, PathName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, TimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.Date, GMTDate);
                
                SFDatebase.ExecuteNonQuery(SFDbCommand);
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

            }
        }
        /// <summary>
        /// Created By:     Marco Abejar
        /// Date Created:   19/03/2013
        /// Description:    Add or update confirmation      
        /// --------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  14/Jun/2013
        /// Description:    Add fields for Audit Trail
        /// --------------------------------------------        
        /// </summary>
        /// 
        public static void UpdateConfirmation(string sIdBigint, string sTRId, string sUser, string sRole,
            string sAirport, string sPort, string sBranch, string sConfirmation, string strLogDescription, 
            string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspAddUpdateConfirmation");
                SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int32, GlobalCode.Field2Int(sIdBigint));
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIDInt", DbType.Int32, GlobalCode.Field2Int(sTRId));
                SFDatebase.AddInParameter(SFDbCommand, "@pUserNameVarchar", DbType.String, sUser);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserRoleVarchar", DbType.String, sRole);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchId", DbType.Int32, sBranch);
                SFDatebase.AddInParameter(SFDbCommand, "@pcolConfirmationVarchar", DbType.String, sConfirmation);

                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, strLogDescription);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, strFunction);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, strPageName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);
                SFDatebase.AddInParameter(SFDbCommand, "@pCreateDate", DbType.DateTime, CreatedDate);

                SFDatebase.ExecuteNonQuery(SFDbCommand);
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

            }
        }
        #endregion
    }
}

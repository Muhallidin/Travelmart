using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.SqlClient;
using System.Globalization;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class HotelDAL
    {
        #region METHODS

        #region HotelDetailsByVendorID
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting City List By Vendor ID
        /// </summary>
        public static DataTable HotelDetailsByVendorID(Int32 VendorID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVendorDetailsByVendorID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        #region GetHotelBranchByRegionPortCountry
        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Josephine Gad
        /// (description)    Selecting Hotel by City
        /// ----------------------------------------------
        /// Date Modified:   15/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public static List<HotelDTO> GetHotelBranchByRegionPortCountry(string userString, string regionString, string portString, string countryString, string airportString)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DataTable dt = null;
            List<HotelDTO> HotelList = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelBranchListByRegionPortCountry");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, GlobalCode.Field2Int(regionString));
                SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, GlobalCode.Field2Int(portString));
                SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, GlobalCode.Field2Int(countryString));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.Int32, GlobalCode.Field2Int(airportString));
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                HotelList = (from a in dt.AsEnumerable()
                             select new HotelDTO
                             {
                                 HotelIDString = a["BranchID"].ToString(),
                                 HotelNameString = a["BranchName"].ToString()
                             }).ToList();
                return HotelList;
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
                if (HotelList != null)
                {
                    HotelList = null;
                }
            }
        }
        /// <summary>
        /// Date Created:    29/Sept/2015
        /// Created By:      Josephine Monteza
        /// (description)    Selecting Hotel by City of given Hotel
        /// ----------------------------------------------
        /// <returns></returns>
        public static List<HotelDTO> GetHotelBranchByCityOfHotel(Int64 iBranchID )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            
            DataTable dt = null;
            List<HotelDTO> HotelList = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelBranchListByHotelCity");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int64, iBranchID);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                
                HotelList = (from a in dt.AsEnumerable()
                             select new HotelDTO {
                                 HotelIDString = a["BranchID"].ToString(),
                                 HotelNameString = a["BranchName"].ToString()
                             }).ToList();
                return HotelList;                
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
                if (HotelList != null)
                {
                    HotelList = null;
                }
            }
            //Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            //DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            //DataTable CityDataTable = null;
            //try
            //{
            //    SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelBranchListByRegionPortCountry");
            //    SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
            //    SFDatebase.AddInParameter(SFDbCommand, "@pRegionIDInt", DbType.Int32, Int32.Parse(regionString));
            //    SFDatebase.AddInParameter(SFDbCommand, "@pPortIDInt", DbType.Int32, Int32.Parse(portString));
            //    SFDatebase.AddInParameter(SFDbCommand, "@pCountryIDInt", DbType.Int32, Int32.Parse(countryString));
            //    SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.Int32, GlobalCode.Field2Int(airportString));
            //    dataReader = SFDatebase.ExecuteReader(SFDbCommand);
            //    CityDataTable = new DataTable();
            //    CityDataTable.Load(dataReader);
            //    return CityDataTable;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (SFDbCommand != null)
            //    {
            //        SFDbCommand.Dispose();
            //    }
            //    if (CityDataTable != null)
            //    {
            //        CityDataTable.Dispose();
            //    }
            //}
        }

        /// <summary>
        /// Date Created:    08/05/2013
        /// Created By:      Marco Abejar
        /// (description)    Get available room type per hotel
        /// ----------------------------------------------

        public static DataTable GetAvailHotelRoomType(string HotelId, string CheckInDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;

            DataTable dt = null;          
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetAvailRoomTypePerHotel");
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelID", DbType.String, HotelId);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.String, CheckInDate);
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
        #endregion

        #region GetHotelBranchByCity
        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Josephine Gad
        /// (description)    Selecting Hotel by City
        /// -----------------------------------------
        /// Date Modified:   14/02/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change output from DataTable to List
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        //public static DataTable GetHotelBranchByCity(string userString, string cityString)
        public static List<HotelDTO> GetHotelBranchByCity(string userString, string cityString)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;            
            DataTable dt = null;
            try
            {
                List<HotelDTO> hotelList;
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelBranchListByCity");
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(cityString));
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                hotelList = (from a in dt.AsEnumerable()
                             select new HotelDTO {
                                HotelIDString = GlobalCode.Field2Int(a["BranchID"].ToString()).ToString(),
                                HotelNameString = a["BranchName"].ToString()
                             }).ToList();
                return hotelList;                
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
            //Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            //DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            //DataTable CityDataTable = null;
            //try
            //{
            //    SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelBranchListByCity");
            //    SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, userString);
            //    SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(cityString));
            //    dataReader = SFDatebase.ExecuteReader(SFDbCommand);
            //    CityDataTable = new DataTable();
            //    CityDataTable.Load(dataReader);
            //    return CityDataTable;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (SFDbCommand != null)
            //    {
            //        SFDbCommand.Dispose();
            //    }
            //    if (CityDataTable != null)
            //    {
            //        CityDataTable.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Created:    24/01/2012
        /// Created By:      Josephine Gad
        /// (description)    Selecting All Hotel
        /// ======================================
        /// Date Modified:   06/03/2012
        /// Modified By:      Josephine Gad
        /// (description)    Add parameter to Airport and IsViewExist
        ///                  Change DataTale to List
        /// </summary>        
        /// </summary>        
        /// <param name="cityString"></param>
        /// <returns></returns>
        //public static DataTable GetHotelBranchAll(string cityString, )
        public static List<HotelAirportDTO> GetHotelBranchAll(string cityString,  Int32 Airport, bool IsViewExist, Int16 iRoomType)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;            
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelBranchListAll");                
                SFDatebase.AddInParameter(SFDbCommand, "@pCityIDInt", DbType.Int32, Int32.Parse(cityString));
                SFDatebase.AddInParameter(SFDbCommand, "@pAirport", DbType.Int32, Airport);
                SFDatebase.AddInParameter(SFDbCommand, "@pIsViewExist", DbType.Boolean, IsViewExist);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomType", DbType.Int16, iRoomType);
                dt = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                List<HotelAirportDTO> list = (from a in dt.AsEnumerable()
                                              select new HotelAirportDTO() 
                                              {
                                                BranchID = GlobalCode.Field2Int(a["BranchID"]),
                                                BranchName = a.Field<string>("BranchName"),
                                                Country = a.Field<string>("Country"),
                                                City = a.Field<string>("City"),
                                              }).ToList();
               return list;
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
        #endregion

        #region CountryListByVendorID
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting country List By Vendor ID
        /// </summary>
        public static DataTable CountryListByVendorID(Int32 VendorID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryListByVendorID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        #region CountryByVendorBranchID
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting country By Vendor branch ID
        /// </summary>
        public static DataTable CountryByVendorBranchID(Int32 VendorID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCountryByVendorBranchID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        #region HotelBranchList
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting hotel branch
        /// </summary>
        public static DataTable HotelBranchList()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelBranchList");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        #region HotelBranchListByVendorID
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting hotel branch by vendorID
        /// </summary>
        public static DataTable HotelBranchListByVendorID(Int32 VendorID, string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelBranchListByVendorID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "pUserIDVarchar", DbType.String, Username);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        #region CityListByVendorID
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor ID
        /// ----------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable CityListByVendorID(Int32 VendorID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByVendorID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        #region CityListByVendorIDCountryID
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor ID
        /// </summary>
        public static DataTable CityListByVendorIDCountryID(Int32 VendorID, Int32 CountryID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByVendorIDCountryID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "pCountryID", DbType.Int32, CountryID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }
        #endregion

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City By Vendor branch ID and country ID
        /// </summary>
        public static DataTable CityByVendorBranchIDCountryID(Int32 VendorID, Int32 CountryID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityByVendorBranchIDCountryID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "pCountryID", DbType.Int32, CountryID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Coutry ID
        /// </summary>
        public static DataTable CityListByCountryID(Int32 CountryID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByCountryID");
                SFDatebase.AddInParameter(SFDbCommand, "pcolCountryIDInt", DbType.Int32, CountryID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }


        /// <summary>
        /// Date Created:   07/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Branch By Vendor and City
        /// ----------------------------------------------
        /// </summary>
        public static DataTable GetVendorBranch(Int32 VendorID, Int32 CityID)
        {          
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable BranchDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetListVendorBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityId", DbType.Int32, CityID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                BranchDataTable = new DataTable();
                BranchDataTable.Load(dataReader);
                return BranchDataTable;
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
                if (BranchDataTable != null)
                {
                    BranchDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   19/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel event notification 
        /// -----------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// -------------------------------------------
        /// Date Modified: 23/01/2012
        /// Modified By:   Josephine Gad
        /// (description)  make OnOffDate string to allow null values
        /// -------------------------------------------
        /// </summary>
        public static IDataReader GetEventNotification(Int32 HotelBranchID, Int32 CityID, string OnOffDate)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetHotelEventNotification");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, HotelBranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCityId", DbType.Int32, CityID);
                if (OnOffDate != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pOnOffDate", DbType.Date, OnOffDate);
                }
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
        /// Date Created:   24/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel room count
        /// --------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// --------------------------------------
        /// Date Modified: 07/12/2011
        /// Modified By:   Josephine Gad
        /// (description)  Add UserRole parameter
        /// </summary>
        public static IDataReader GetContractRoomBlockCount(Int32 HotelBranchID, Int32 RoomTypeID, String CheckInDate, 
            Int32 Duration, String User, string UserRole)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCheckAvailableHotelRoomBlocks_Contract");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, HotelBranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.String, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pDuration", DbType.Int32, Duration);
                SFDatebase.AddInParameter(SFDbCommand, "@pUser", DbType.String, User);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
        /// Date Created:   28/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel room count for override
        /// -----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader GetOverrideRoomBlockCount(Int32 HotelBranchID, Int32 RoomTypeID, String CheckInDate, Int32 Duration, String User, String UserRole)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspCheckAvailableHotelRoomBlocks_Override");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, HotelBranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.String, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pDuration", DbType.Int32, Duration);
                SFDatebase.AddInParameter(SFDbCommand, "@pUser", DbType.String, User);
                SFDatebase.AddInParameter(SFDbCommand, "@pRole", DbType.String, UserRole);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
        /// Date Created:   28/11/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Hotel room count for override
        /// -----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader GetActiveHotelContractID(Int32 HotelBranchID, Int32 RoomTypeID, DateTime CheckInDate, Int32 Duration, String User)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetActiveHotelContractID");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, HotelBranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.DateTime, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pDuration", DbType.Int32, Duration);
                SFDatebase.AddInParameter(SFDbCommand, "@pUser", DbType.String, User);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting City List By Vendor Code and Type
        /// ---------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable CityListByVendorCodeAndType(string VendorCode, string VendorType)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable CityDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectCityListByVendorCodeAndType");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorCode", DbType.String, VendorCode);
                SFDatebase.AddInParameter(SFDbCommand, "pVendorType", DbType.String, VendorType);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                CityDataTable = new DataTable();
                CityDataTable.Load(dataReader);
                return CityDataTable;
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
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Hotel Details By Booking ID
        /// ---------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// -----------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader HotelBookingDetailsByID(int TravelLocID, int SeqNo)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelBookingsDetailsByID");
                SFDatebase.AddInParameter(SFDbCommand, "pTravelLocID", DbType.Int32, TravelLocID);
                SFDatebase.AddInParameter(SFDbCommand, "pSeqNo", DbType.Int32, SeqNo);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel Details By Booking ID (None Sabre)
        /// ---------------------------------------------------            
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public static IDataReader HotelBookingDetailsOtherByID(string TransHotelIDBigInt)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelBookingsDetailsOtherByID");
                SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelIDBigInt", DbType.Int32, TransHotelIDBigInt);
                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                return dataReader;
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
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer hotel transaction (pending)
        /// ----------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>  
        public static IDataReader HotelBookingPendingByID(string PendingHotelID)
        {
            IDataReader dt = null;
            DbCommand command = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspGetSFHotelPending");
                db.AddInParameter(command, "@pPendingHotelIDBigInt", DbType.String, PendingHotelID);
                dt = db.ExecuteReader(command);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Hotel Details 
        /// ---------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable HotelVendorGetDetails()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendor");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting Hotel Details by region
        /// </summary>
        public static DataTable HotelVendorGetDetailsByRegion(string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelVendorByRegion");
                SFDatebase.AddInParameter(SFDbCommand, "pUserIDVarchar", DbType.String, Username);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   15/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Room Type Details 
        /// -----------------------------------------------
        /// Date Modified:  02/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close IDataReader and DataTable
        /// -----------------------------------------------  
        /// </summary>
        public static DataTable HotelRoomTypeGetDetails()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelRoomType");                
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:    17/11/2011
        /// Created By:      Josephine Gad
        /// (description)    Selecting Hotel Room Type Details not exists in Branch
        /// -----------------------------------------------
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -----------------------------------------------
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static List<HotelBranchRoomTypeNotExist> HotelRoomTypeGetNotExist(string BranchID)
        {
            DataTable dt = null;
            DbCommand command = null;
            List<HotelBranchRoomTypeNotExist> list = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectHotelRoomTypeNotExist");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int64, Int64.Parse(BranchID));
                dt = db.ExecuteDataSet(command).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new HotelBranchRoomTypeNotExist { 
                            colRoomTypeID = GlobalCode.Field2TinyInt(a["colRoomTypeID"]),
                            colRoomNameVarchar = a.Field<string>("colRoomNameVarchar")
                        }).ToList();
                return list;
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
                if (command != null)
                {
                    command.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Hotel Booking 
        ///-------------------------------------------------------------------------------
        /// Date Modified: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Add Parameter breakfast, lunch, dinner, remarks and Bill to Crew
        ///------------------------------------------------------------------------------- 
        /// Date Modified:   18/11/2011
        /// Modified By:     Charlene Remotigue
        ///  (description)   add parameter pContractFromVarchar
        ///-------------------------------------------------------------------------------
        /// </summary>
        public static void InsertHotelBooking(int SfID, int HotelID, int RoomType, DateTime CheckInDate, int duration, string HotelStatus,
                            string User, string SfStatus, string LocatorID, int CityID, string CheckInTime, Boolean WithBreakfast, Boolean WithLunch,
                            Boolean WithDinner, string Remarks, Boolean BillToCrew, Boolean WithShuttle)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                if (CheckInTime == "")
                {
                    CheckInTime = null;
                }

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelBookings");
                SFDatebase.AddInParameter(SFDbCommand, "pSfID", DbType.Int32, SfID);
                SFDatebase.AddInParameter(SFDbCommand, "pHotelID", DbType.Int32, HotelID);
                SFDatebase.AddInParameter(SFDbCommand, "pRoomType", DbType.Int32, RoomType);
                SFDatebase.AddInParameter(SFDbCommand, "pCheckInDate", DbType.DateTime, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "pDuration", DbType.Int32, duration);
                SFDatebase.AddInParameter(SFDbCommand, "pHotelStatus", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "pSfStatus", DbType.String, SfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "pCreatedBy", DbType.String, User);
                SFDatebase.AddInParameter(SFDbCommand, "pLocatorID", DbType.Int32, Convert.ToInt32(LocatorID));
                SFDatebase.AddInParameter(SFDbCommand, "pCityID", DbType.Int32, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "pCheckInTime", DbType.Time, CheckInTime);

                SFDatebase.AddInParameter(SFDbCommand, "pWithBreakfast", DbType.Boolean, WithBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "pWithLunch", DbType.Boolean, WithLunch);
                SFDatebase.AddInParameter(SFDbCommand, "pWithDinner", DbType.Boolean, WithDinner);
                SFDatebase.AddInParameter(SFDbCommand, "pBillToCrew", DbType.Boolean, BillToCrew);
                SFDatebase.AddInParameter(SFDbCommand, "pRemarks", DbType.String, Remarks);
                SFDatebase.AddInParameter(SFDbCommand, "pWithShuttle", DbType.Boolean, WithShuttle);
                if (TravelMartVariable.RolePortSpecialist == GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]))
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pContractFromVarchar", DbType.String, "port");
                }
                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }

        /// <summary>
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Hotel Booking NOT from Sabre
        ///-------------------------------------------------------------------------------    
        /// Date Modified:   02/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter WithLunchOrDinner and other meal parameters
        ///------------------------------------------------------------------------------- 
        /// Date Modified:   11/10/2011
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter RecordLoc and RequestID
        ///------------------------------------------------------------------------------- 
        /// Date Modified:   18/11/2011
        /// Modified By:     Charlene Remotigue
        ///  (description)   add parameter pContractFromVarchar
        ///-------------------------------------------------------------------------------        
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        public static void InsertHotelBookingOther(string TravelReqID, string RecordLoc, string RequestID, string VendorID, 
            string BranchID, string RoomTypeID,
            string CheckInDate, string CheckInTime, string Duration, string HotelStatus, string SfStatus, string CreatedBy,
            string Remarks, bool BillToCrew, bool WithBreakfast, bool WithLunch, bool WithDinner, bool WithLunchOrDinner,
            bool WithShuttle
            //string BreakfastIDString, string LunchIDString, string DinnerIDString, string LunchDinnerIDString 
            )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                if (CheckInTime == "")
                {
                    CheckInTime = null;
                }

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelBookingsOthers");
                if (TravelReqID.Trim() != "0" && TravelReqID.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int32, Convert.ToInt32(TravelReqID));
                }
                if (RecordLoc.Trim() != "0" && RecordLoc.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLoc);
                }
                if (RequestID.Trim() != "0" && RequestID.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, Convert.ToInt32(RequestID));
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorIdInt", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.DateTime, DateTime.Parse(CheckInDate));
                SFDatebase.AddInParameter(SFDbCommand, "pCheckInTime", DbType.Time, CheckInTime);
                SFDatebase.AddInParameter(SFDbCommand, "pDuration", DbType.Int32, Duration);

                SFDatebase.AddInParameter(SFDbCommand, "pHotelStatus", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "pSfStatus", DbType.String, SfStatus);
                SFDatebase.AddInParameter(SFDbCommand, "pCreatedBy", DbType.String, CreatedBy);                
                //SFDatebase.AddInParameter(SFDbCommand, "pCityID", DbType.Int32, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "pRemarks", DbType.String, Remarks);
                SFDatebase.AddInParameter(SFDbCommand, "pBillToCrew", DbType.Boolean, BillToCrew);

                SFDatebase.AddInParameter(SFDbCommand, "pWithBreakfast", DbType.Boolean, WithBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "pWithLunch", DbType.Boolean, WithLunch);
                SFDatebase.AddInParameter(SFDbCommand, "pWithDinner", DbType.Boolean, WithDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunchOrDinnerBit", DbType.Boolean, WithLunchOrDinner);                               
                SFDatebase.AddInParameter(SFDbCommand, "pWithShuttle", DbType.Boolean, WithShuttle);
                if (TravelMartVariable.RolePortSpecialist == GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]))
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pContractFromVarchar", DbType.String, "port");
                }
                //if (WithBreakfast)
                //{
                //    SFDatebase.AddInParameter(SFDbCommand, "@pBreakfastIDInt", DbType.Int16, Int16.Parse(BreakfastIDString));
                //}
                //if (WithLunchOrDinner)
                //{
                //    SFDatebase.AddInParameter(SFDbCommand, "@pLunchOrDinnerIDInt", DbType.Int16, Int16.Parse(LunchDinnerIDString));
                //}
                //else
                //{
                //    if (WithLunch)
                //    {
                //        SFDatebase.AddInParameter(SFDbCommand, "@pLunchIDInt", DbType.Int16, Int16.Parse(LunchIDString));
                //    }
                //    if (WithDinner)
                //    {
                //        SFDatebase.AddInParameter(SFDbCommand, "@pDinnerIDInt", DbType.Int16, Int16.Parse(DinnerIDString));
                //    }
                //}

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert Pending Hotel Booking 
        ///------------------------------------------------------------------------------- 
        /// Date Modified:   18/11/2011
        /// Modified By:     Charlene Remotigue
        /// (description)   add parameter pContractFromVarchar
        ///-------------------------------------------------------------------------------
        /// Date Modified:   05/12/2011
        /// Modified By:     Josephine Gad
        /// (description)    add parameter Confirmation
        ///-------------------------------------------------------------------------------
        /// Date Modified:   17/01/2012
        /// Modified By:     Josephine Gad
        /// (description)    Remove parameter RoomAmount,CurrencyID, WithTax, TaxAmount and ContractId
        ///-------------------------------------------------------------------------------        
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        public static Int32 InsertHotelBookingPending(string TransHotelID, string IdBigint, string SeqNo,
            string TravelReqID, string RecordLoc, string RequestID, string VendorID,
            string BranchID, string RoomTypeID,
            string CheckInDate, string CheckInTime, string Duration, string HotelStatus, string SfStatus,
            string CreatedBy, string CreatedDate, string ModifiedByString, string ModifiedDate,
            string Remarks, bool BillToCrew, bool WithBreakfast, bool WithLunch, bool WithDinner, bool WithLunchOrDinner, string VoucherAmount,
            bool WithShuttle, 
            //string RoomAmount, int CurrencyID, bool WithTax, string TaxAmount, 
            string Action, string Confirmation
            //string ContractId
            )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            //IDataReader dataReader = null;
            //DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                CultureInfo enCulture = new CultureInfo("en-US");
                string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";

                if (CheckInTime == "")
                {
                    CheckInTime = null;
                }

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertHotelBookingsPending");
                if (TransHotelID.Trim() != "0" && TransHotelID != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelIDBigInt", DbType.Int32, Convert.ToInt32(TransHotelID));
                }
                if (IdBigint.Trim() != "0" && IdBigint.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int32, Convert.ToInt32(IdBigint));
                }
                if (SeqNo.Trim() != "0" && SeqNo.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, Convert.ToInt32(SeqNo));
                }
                if (TravelReqID.Trim() != "0" && TravelReqID.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int32, Convert.ToInt32(TravelReqID));
                }
                if (RecordLoc.Trim() != "0" && RecordLoc.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLoc);
                }
                if (RequestID.Trim() != "0" && RequestID.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, Convert.ToInt32(RequestID));
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorIdInt", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.DateTime, DateTime.Parse(CheckInDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pDuration", DbType.Int32, Duration);

                SFDatebase.AddInParameter(SFDbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSfStatus", DbType.String, SfStatus);

                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                if (CreatedDate.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pDateCreatedDatetime", DbType.DateTime, DateTime.ParseExact(CreatedDate, dtFormaString, enCulture));
                }
                
                SFDatebase.AddInParameter(SFDbCommand, "@pModifiedbyVarchar", DbType.String, ModifiedByString);
                if (ModifiedDate.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.ParseExact(ModifiedDate, dtFormaString, enCulture));
                }         
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarks", DbType.String, Remarks);
                SFDatebase.AddInParameter(SFDbCommand, "@pBillToCrew", DbType.Boolean, BillToCrew);

                SFDatebase.AddInParameter(SFDbCommand, "@pWithBreakfast", DbType.Boolean, WithBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunch", DbType.Boolean, WithLunch);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithDinner", DbType.Boolean, WithDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunchOrDinnerBit", DbType.Boolean, WithLunchOrDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pVoucherAmountMoney", DbType.String, VoucherAmount);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithShuttle", DbType.Boolean, WithShuttle);

                //SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmountMoney", DbType.String, RoomAmount);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmountCurrencyID", DbType.Int32, CurrencyID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pWithTax", DbType.Boolean, WithTax);
                //SFDatebase.AddInParameter(SFDbCommand, "@pTaxAmountMoney", DbType.String, TaxAmount);                

                SFDatebase.AddInParameter(SFDbCommand, "@pActionVarchar", DbType.String, Action);
                if (TravelMartVariable.RolePortSpecialist == GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]))
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pContractFromVarchar", DbType.String, "port");
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pConfirmationNoVarchar", DbType.String, Confirmation);
                //SFDatebase.AddInParameter(SFDbCommand, "@pContractId", DbType.String, ContractId);
                SFDatebase.AddOutParameter(SFDbCommand, "@pPendingHotelId", DbType.Int32, 8);
                
                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
                
                Int32 pID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@pPendingHotelId"));
                return pID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created:   17/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Insert Pending Hotel Booking Details for each day
        ///------------------------------------------------------------------------------- 
        /// </summary>
        /// <param name="HotelTransPendingDetailsID"></param>
        /// <param name="PendingHotelIDBigInt"></param>
        /// <param name="dDate"></param>
        /// <param name="ContractID"></param>
        /// <param name="ContractFromVarchar"></param>
        /// <param name="CurrencyIDInt"></param>
        /// <param name="RatePerDay"></param>
        /// <param name="RoomRateTaxPercentage"></param>
        /// <param name="RoomRateTaxInclusive"></param>
        /// <param name="CreatedByVarchar"></param>
        public static Int32 InsertHotelBookingPendingDetails(Int32 HotelTransPendingDetailsID, Int32 PendingHotelIDBigInt,
            DateTime dDate, Int32 ContractID, string ContractFromVarchar, Int32 CurrencyIDInt, decimal RatePerDay,
            decimal RoomRateTaxPercentage, bool RoomRateTaxInclusive, string CreatedByVarchar
            )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;           
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertUpdateHotelBookingsPendingDetails");                               
                SFDatebase.AddOutParameter(SFDbCommand, "@pHotelTransPendingDetailsIDBigInt", DbType.Int32, HotelTransPendingDetailsID);

                SFDatebase.AddInParameter(SFDbCommand, "@pPendingHotelIDBigInt", DbType.Int32, PendingHotelIDBigInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pDate", DbType.DateTime, dDate);
                if (ContractID != 0)
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pContractIDInt", DbType.Int32, ContractID);
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pContractFromVarchar", DbType.String, ContractFromVarchar);
                
                SFDatebase.AddInParameter(SFDbCommand, "@pCurrencyIDInt", DbType.Int32, CurrencyIDInt);                
                SFDatebase.AddInParameter(SFDbCommand, "@pRatePerDayMoney", DbType.Decimal, RatePerDay);

                SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxInclusive", DbType.Boolean, RoomRateTaxInclusive);
                if (RoomRateTaxInclusive)
                {                                        
                    SFDatebase.AddInParameter(SFDbCommand, "@pRoomRateTaxPercentage", DbType.Decimal, RoomRateTaxPercentage);
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pCreatedByVarchar", DbType.String, CreatedByVarchar);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();

                Int32 pID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@pHotelTransPendingDetailsIDBigInt"));
                return pID;
                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   18/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Delete Pending Hotel Booking Details 
        ///-------------------------------------------------------------------------------          
        /// </summary>
        /// <param name="HotelTransPendingDetailsID"></param>
        /// <param name="DeletedByVarchar"></param>
        public static void DeleteHotelBookingPendingDetails(Int32 HotelTransPendingDetailsID, string DeletedByVarchar)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteHotelBookingsPendingDetails");

                SFDatebase.AddInParameter(SFDbCommand, "@pHotelTransPendingDetailsIDBigInt", DbType.Int32, HotelTransPendingDetailsID);
                SFDatebase.AddInParameter(SFDbCommand, "@pDeleteBy", DbType.String, DeletedByVarchar);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created:   18/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Select Pending Hotel Booking Details 
        ///-------------------------------------------------------------------------------          
        /// </summary>
        /// <param name="HotelTransPendingDetailsID"></param>
        /// <param name="DeletedByVarchar"></param>
        public static DataTable SelectHotelBookingPendingDetails(Int32 PendingHotelID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelBookingsPendingDetails");
                SFDatebase.AddInParameter(SFDbCommand, "@pPendingHotelIDBigInt", DbType.Int32, PendingHotelID);              

                dt =  SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {                
                if (dt!= null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   15/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Update Hotel Booking 
        ///---------------------------------------- 
        /// Date Modified:  21/07/2011
        /// Modified By:    Josphine Gad
        /// (description)   Add Parameter CheckInTime
        /// ---------------------------------------
        /// Date Modified:  31/07/2011
        /// Modified By:    Josphine Gad
        /// (description)   Replace CityId With BranchID
        /// ---------------------------------------
        /// Date Modified:  02/09/2011
        /// Modified By:    Josphine Gad
        /// (description)   Add parameter WithLunchOrDinner and other meal paramaters
        /// </summary>
        public static void UpdateHotelBooking(int HotelBookingID, int HotelID, int BranchIDInt, int RoomType, DateTime CheckInDate, int duration, string HotelStatus,
              string User, string CheckInTime, Boolean WithBreakfast, Boolean WithLunch,
              Boolean WithDinner, Boolean WithLunchOrDinner, string Remarks, Boolean BillToCrew, Boolean WithShuttle, int SeqNo
              //string BreakfastIDString, string LunchIDString, string DinnerIDString, string LunchDinnerIDString
            )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                if (CheckInTime == "")
                {
                    CheckInTime = null;
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateHotelBookings");
                SFDatebase.AddInParameter(SFDbCommand, "pHotelBookingID", DbType.Int32, HotelBookingID);
                SFDatebase.AddInParameter(SFDbCommand, "pHotelID", DbType.Int32, HotelID);
                SFDatebase.AddInParameter(SFDbCommand, "pBranchIDInt", DbType.Int32, BranchIDInt);

                SFDatebase.AddInParameter(SFDbCommand, "pRoomType", DbType.Int32, RoomType);
                SFDatebase.AddInParameter(SFDbCommand, "pCheckInDate", DbType.DateTime, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "pDuration", DbType.Int32, duration);
                SFDatebase.AddInParameter(SFDbCommand, "pHotelStatus", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "pCreatedBy", DbType.String, User);
                //SFDatebase.AddInParameter(SFDbCommand, "pCityID", DbType.Int32, CityID);
                SFDatebase.AddInParameter(SFDbCommand, "pCheckInTime", DbType.Time, CheckInTime);

                SFDatebase.AddInParameter(SFDbCommand, "pWithBreakfast", DbType.Boolean, WithBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "pWithLunch", DbType.Boolean, WithLunch);
                SFDatebase.AddInParameter(SFDbCommand, "pWithDinner", DbType.Boolean, WithDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunchOrDinnerBit", DbType.Boolean, WithLunchOrDinner);

                SFDatebase.AddInParameter(SFDbCommand, "pBillToCrew", DbType.Boolean, BillToCrew);
                SFDatebase.AddInParameter(SFDbCommand, "pRemarks", DbType.String, Remarks);
                SFDatebase.AddInParameter(SFDbCommand, "pWithShuttle", DbType.Boolean, WithShuttle);
                SFDatebase.AddInParameter(SFDbCommand, "pSeqNo", DbType.Int32, SeqNo);

                //if (WithBreakfast)
                //{
                //    SFDatebase.AddInParameter(SFDbCommand, "@pBreakfastIDInt", DbType.Int16, Int16.Parse(BreakfastIDString));
                //}
                //if (WithLunchOrDinner)
                //{
                //    SFDatebase.AddInParameter(SFDbCommand, "@pLunchOrDinnerIDInt", DbType.Int16, Int16.Parse(LunchDinnerIDString));
                //}
                //else
                //{
                //    if (WithLunch)
                //    {
                //        SFDatebase.AddInParameter(SFDbCommand, "@pLunchIDInt", DbType.Int16, Int16.Parse(LunchIDString));
                //    }
                //    if (WithDinner)
                //    {
                //        SFDatebase.AddInParameter(SFDbCommand, "@pDinnerIDInt", DbType.Int16, Int16.Parse(DinnerIDString));
                //    }
                //}

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }

        /// <summary>
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Update Hotel Booking (none Sabre)
        ///----------------------------------------             
        /// Date Modified:   02/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Add parameter WithLunchOrDinner and other meal paramaters
        /// ---------------------------------------
        /// </summary>
        public static void UpdateHotelBookingOther(string TransHotelID, string TravelReqID, string VendorId,
            string BranchID, string RoomType, string CheckInDate, string CheckInTime, string duration,
            string HotelStatus, string ModifiedBy, string Remarks, bool BillToCrew, bool WithBreakfast,
            bool WithLunch, bool WithDinner, bool WithLunchOrDinner, bool WithShuttle
            //string BreakfastIDString, string LunchIDString, string DinnerIDString, string LunchDinnerIDString
            )
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                if (CheckInTime == "")
                {
                    CheckInTime = null;
                }
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateHotelBookingsOther");
                SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelIDBigInt", DbType.Int32, Int32.Parse(TransHotelID));
                SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int32, Int32.Parse(TravelReqID));
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorIdInt", DbType.Int32, Int32.Parse(VendorId));
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int32, Int32.Parse(BranchID));
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, Int32.Parse(RoomType));                
                
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.DateTime, CheckInDate);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInTime", DbType.Time, CheckInTime);

                SFDatebase.AddInParameter(SFDbCommand, "@pDuration", DbType.Int32, Int32.Parse(duration));
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pModifiedBy", DbType.String, ModifiedBy);
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarks", DbType.String, Remarks);

                SFDatebase.AddInParameter(SFDbCommand, "@pBillToCrew", DbType.Boolean, BillToCrew);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithBreakfast", DbType.Boolean, WithBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunch", DbType.Boolean, WithLunch);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithDinner", DbType.Boolean, WithDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunchOrDinnerBit", DbType.Boolean, WithLunchOrDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithShuttle", DbType.Boolean, WithShuttle);

                //if (WithBreakfast)
                //{
                //    SFDatebase.AddInParameter(SFDbCommand, "@pBreakfastIDInt", DbType.Int16, Int16.Parse(BreakfastIDString));                   
                //}
                //if (WithLunchOrDinner)
                //{
                //    SFDatebase.AddInParameter(SFDbCommand, "@pLunchOrDinnerIDInt", DbType.Int16, Int16.Parse(LunchDinnerIDString));
                //}
                //else
                //{
                //    if (WithLunch)
                //    {
                //        SFDatebase.AddInParameter(SFDbCommand, "@pLunchIDInt", DbType.Int16, Int16.Parse(LunchIDString));
                //    }
                //    if (WithDinner)
                //    {
                //        SFDatebase.AddInParameter(SFDbCommand, "@pDinnerIDInt", DbType.Int16, Int16.Parse(DinnerIDString));
                //    }
                //}
                
                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Update Pending Hotel Booking 
        ///-------------------------------------------------------------------------------           
        /// </summary>
        public static void UpdateHotelBookingPending(string PendingID, string TransHotelID, string IdBigint, string SeqNo,
            string TravelReqID, string RecordLoc, string RequestID, string VendorID, string BranchID, string RoomTypeID,
            string CheckInDate, string CheckInTime, string Duration, string HotelStatus, string SfStatus,
            string ModifiedByString, string ModifiedDate,
            string Remarks, bool BillToCrew, bool WithBreakfast, bool WithLunch, bool WithDinner, bool WithLunchOrDinner,
            string VoucherAmount, bool WithShuttle, string Action)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                CultureInfo enCulture = new CultureInfo("en-US");
                string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";

                if (CheckInTime == "")
                {
                    CheckInTime = null;
                }

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateHotelBookingsPending");
                if (PendingID.Trim() != "0" && PendingID != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pPendingHotelIDBigInt", DbType.Int32, Convert.ToInt32(PendingID));
                }
                if (TransHotelID.Trim() != "0" && TransHotelID != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelIDBigInt", DbType.Int32, Convert.ToInt32(TransHotelID));
                }
                if (IdBigint.Trim() != "0" && IdBigint.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pIdBigint", DbType.Int32, Convert.ToInt32(IdBigint));
                }
                if (SeqNo.Trim() != "0" && SeqNo.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pSeqNoInt", DbType.Int32, Convert.ToInt32(SeqNo));
                }
                if (TravelReqID.Trim() != "0" && TravelReqID.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pTravelReqIdInt", DbType.Int32, Convert.ToInt32(TravelReqID));
                }
                if (RecordLoc.Trim() != "0" && RecordLoc.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRecordLocatorVarchar", DbType.String, RecordLoc);
                }
                if (RequestID.Trim() != "0" && RequestID.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pRequestIDInt", DbType.Int32, Convert.ToInt32(RequestID));
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorIdInt", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int32, BranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInDate", DbType.DateTime, DateTime.Parse(CheckInDate));
                SFDatebase.AddInParameter(SFDbCommand, "@pCheckInTime", DbType.Time, CheckInTime);
                SFDatebase.AddInParameter(SFDbCommand, "@pDuration", DbType.Int32, Duration);

                SFDatebase.AddInParameter(SFDbCommand, "@pHotelStatus", DbType.String, HotelStatus);
                SFDatebase.AddInParameter(SFDbCommand, "@pSfStatus", DbType.String, SfStatus);
                
                if (ModifiedByString.Trim() != "")
                {
                    SFDatebase.AddInParameter(SFDbCommand, "@pModifiedbyVarchar", DbType.String, ModifiedByString);
                    SFDatebase.AddInParameter(SFDbCommand, "@pDateModifiedDatetime", DbType.DateTime, DateTime.ParseExact(ModifiedDate, dtFormaString, enCulture));
                }
                SFDatebase.AddInParameter(SFDbCommand, "@pRemarks", DbType.String, Remarks);
                SFDatebase.AddInParameter(SFDbCommand, "@pBillToCrew", DbType.Boolean, BillToCrew);

                SFDatebase.AddInParameter(SFDbCommand, "@pWithBreakfast", DbType.Boolean, WithBreakfast);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunch", DbType.Boolean, WithLunch);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithDinner", DbType.Boolean, WithDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithLunchOrDinnerBit", DbType.Boolean, WithLunchOrDinner);
                SFDatebase.AddInParameter(SFDbCommand, "@pVoucherAmountMoney", DbType.String, VoucherAmount);
                SFDatebase.AddInParameter(SFDbCommand, "@pWithShuttle", DbType.Boolean, WithShuttle);

                //SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmountMoney", DbType.String, RoomAmount);
                //SFDatebase.AddInParameter(SFDbCommand, "@pRoomAmountCurrencyID", DbType.Int32, CurrencyID);
                //SFDatebase.AddInParameter(SFDbCommand, "@pWithTax", DbType.Boolean, WithTax);
                //SFDatebase.AddInParameter(SFDbCommand, "@pTaxAmountMoney", DbType.String, TaxAmount);

                SFDatebase.AddInParameter(SFDbCommand, "@pActionVarchar", DbType.String, Action);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Delete Hotel Booking 
        /// </summary>
        public static void DeleteHotelBooking(int HotelBookingID, string User, int SeqNo)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteHotelBookings");
                SFDatebase.AddInParameter(SFDbCommand, "pHotelBookingID", DbType.Int32, HotelBookingID);
                SFDatebase.AddInParameter(SFDbCommand, "pCreatedBy", DbType.String, User);
                SFDatebase.AddInParameter(SFDbCommand, "pSeqNo", DbType.Int32, SeqNo);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }

        /// <summary>
        /// Date Created:   26/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Hotel Booking not from Sabre
        /// </summary>
        public static void DeleteHotelBookingOther(int TransHotelIDBigInt, string DeletedByString)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteHotelBookingsOther");
                SFDatebase.AddInParameter(SFDbCommand, "@pTransHotelIDBigInt", DbType.Int32, TransHotelIDBigInt);
                SFDatebase.AddInParameter(SFDbCommand, "@pDeletedBy", DbType.String, DeletedByString);                

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created:   04/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Pending Hotel Booking
        /// </summary>
        public static void DeleteHotelBookingPending(int PendingHotelID, string DeletedByString)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspDeleteHotelBookingsPending");
                SFDatebase.AddInParameter(SFDbCommand, "@pPendingHotelIDBigInt", DbType.Int32, PendingHotelID);
                SFDatebase.AddInParameter(SFDbCommand, "@pModifiedByVarchar", DbType.String, DeletedByString);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }
        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Get number of rooms available by hotel name and hotel location 
        /// ----------------------------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader and DataTable
        /// </summary>
        public static DataTable GetNumberOfRoomsAvailableByHotelAndLocation(Int32 VendorID, Int32 RoomTypeID)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable RoomDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelNumberOfRoomsByVendorID");
                SFDatebase.AddInParameter(SFDbCommand, "pVendorID", DbType.Int32, VendorID);
                SFDatebase.AddInParameter(SFDbCommand, "pRoomTypeID", DbType.Int32, RoomTypeID);
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                RoomDataTable = new DataTable();
                RoomDataTable.Load(dataReader);
                return RoomDataTable;
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
                if (RoomDataTable != null)
                {
                    RoomDataTable.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:    25/08/2011
        /// Created By:      Josephine Gad
        /// (description)    Selecting Hotel Room Type Details by Vendor Branch
        /// -----------------------------------------------
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -----------------------------------------------
        /// </summary>
        //public static DataTable HotelRoomTypeGetDetailsByBranch(string BranchIDString)
        public static List<HotelBranchRoomType> HotelRoomTypeGetDetailsByBranch(string BranchIDString)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;            
            DataTable HotelDataTable = null;
            List<HotelBranchRoomType> list = null;
            try
            {
                BranchIDString = (BranchIDString == "" ? "0" : BranchIDString);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelRoomTypeByBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int16, Int16.Parse(BranchIDString));
                HotelDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];

                list = (from a in HotelDataTable.AsEnumerable()
                        select new HotelBranchRoomType
                        {
                            colRoomTypeID = GlobalCode.Field2TinyInt(a["colRoomTypeID"]),
                            colRoomNameVarchar =a.Field<string>("colRoomNameVarchar"),
                        }).ToList();
                return list;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Hotel Room Type Capacity and Rate Details by Branch
        /// -----------------------------------------------
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static DataTable HotelRoomTypeCapacityGetDetailsByBranch(string BranchID)
        {
            DataTable dt = null;
            DbCommand command = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectHotelRoomTypeCapacityByBranch");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int64, Int64.Parse(BranchID));
                dt = db.ExecuteDataSet(command).Tables[0];
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
                if (command != null)
                {
                    command.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created:   25/08/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Room Type Details 
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelRoomType()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelRoomType");

                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Selecting Hotel Meals
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelMealsGetByBranch(string BranchIDString, string MealTypeString, string MealTypeString2)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                BranchIDString = (BranchIDString == "" ? "0" : BranchIDString);

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectHotelMeals");
                SFDatebase.AddInParameter(SFDbCommand, "@pBranchIDInt", DbType.Int16, Int16.Parse(BranchIDString));
                SFDatebase.AddInParameter(SFDbCommand, "@pMealTypeIDInt", DbType.Int16, Int16.Parse(MealTypeString));
                SFDatebase.AddInParameter(SFDbCommand, "@pMealTypeIDInt2", DbType.Int16, Int16.Parse(MealTypeString2));
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals Type
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelMealsType()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectMealType");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals Preference
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelMealsPreference()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectMealPref");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals by meal type
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelMealsPreferenceByMealType(string MealType)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectMealPrefByMealType");
                SFDatebase.AddInParameter(SFDbCommand, "@colMealTypeIDInt", DbType.Int16, Int16.Parse(MealType));
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals type and preference
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelMealsPreferenceByMealTypePref(string MealType, string MealPref)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectMealPrefByMealTypePref");
                SFDatebase.AddInParameter(SFDbCommand, "@colMealTypeIDInt", DbType.Int16, Int16.Parse(MealType));
                SFDatebase.AddInParameter(SFDbCommand, "@colMealPrefIDInt", DbType.Int16, Int16.Parse(MealPref));
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>        
        /// Date Created:   05/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Selecting Hotel Meals 
        /// -----------------------------------------------
        /// </summary>
        public static DataTable HotelMeals()
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            IDataReader dataReader = null;
            DataTable HotelDataTable = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectMeal");
                dataReader = SFDatebase.ExecuteReader(SFDbCommand);
                HotelDataTable = new DataTable();
                HotelDataTable.Load(dataReader);
                return HotelDataTable;
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Hotel meal branch 
        /// </summary>
        public static Int32 InsertHotelMealBranch(string BranchID, string MealName, string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertMealBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@colBranchIDInt", DbType.Int32, Convert.ToInt32(BranchID));
                SFDatebase.AddInParameter(SFDbCommand, "@colMealName", DbType.String, MealName);
                SFDatebase.AddInParameter(SFDbCommand, "@Username", DbType.String, Username);

                SFDatebase.AddOutParameter(SFDbCommand, "@pID", DbType.Int32, 8);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();

                Int32 pID = Convert.ToInt32(SFDatebase.GetParameterValue(SFDbCommand, "@pID"));
                return pID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }                   
            }
        }

        /// <summary>
        /// Date Created: 15/07/2011
        /// Created By: Ryan Bautista
        /// (description) remove Hotel meal branch 
        /// </summary>
        public static void RemoveHotelMealBranch(string BranchID, string MealName, string Username)
        {
            Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand SFDbCommand = null;
            DataTable dt = new DataTable();
            DbConnection conn = SFDatebase.CreateConnection();
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateMealBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@colBranchIDInt", DbType.Int32, Convert.ToInt32(BranchID));
                SFDatebase.AddInParameter(SFDbCommand, "@colMealName", DbType.String, MealName);
                SFDatebase.AddInParameter(SFDbCommand, "@Username", DbType.String, Username);

                SFDatebase.ExecuteNonQuery(SFDbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
                if (SFDbCommand != null)
                {
                    SFDbCommand.Dispose();
                }
                if (conn != null)
                {
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }    
            }
        }

        /// <summary>                        
        /// Date Created:   19/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Event notification
        /// </summary>           
        public static DataTable HotelHasEvents()
        {
            Database EventDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbConnection connection = EventDatebase.CreateConnection();
            DbCommand EventDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                EventDbCommand = EventDatebase.GetStoredProcCommand("");                
                //SFDatebase.AddInParameter(SFDbCommand, "@pSFDateFrom", DbType.String, DateFromString);

                dataReader = EventDatebase.ExecuteReader(EventDbCommand);
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
                if (EventDbCommand != null)
                {
                    EventDbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   04/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Approve Pending hotel transaction
        /// </summary>
        /// <param name="PendingHotelID"></param>        
        /// <param name="ApprovedBy"></param>
        public static DataTable HotelApproveTransaction(string PendingHotelID, string ApprovedBy)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;
            
            //IDataReader dataReader = null;
            DataTable dt = new DataTable();
            
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                command = db.GetStoredProcCommand("uspApproveHotelBookingsPending");
                db.AddInParameter(command, "@pPendingHotelIDBigInt", DbType.Int64, Int64.Parse(PendingHotelID));
                db.AddInParameter(command, "@pApprovedBy", DbType.String, ApprovedBy);
                db.AddInParameter(command, "@pApprovedDatetime", DbType.DateTime, DateTime.Now);
                db.AddOutParameter(command, "@pPK", DbType.Int32, 8);
                db.AddOutParameter(command, "@pSeqNo", DbType.Int32, 8);
                
                db.ExecuteNonQuery(command, trans);
                trans.Commit();

                Int32 PK = Convert.ToInt32(db.GetParameterValue(command, "@pPK"));
                Int32 SeqNo = Convert.ToInt32(db.GetParameterValue(command, "@pSeqNo"));

                dt.Columns.Add("PK");
                dt.Columns.Add("SeqNum");

                dt.Rows.Add(PK, SeqNo);
                return dt;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   25/11/2011
        /// Created By:     Josephine Gad
        /// (description)  Get Hotel Room Override By Branch
        /// ---------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ---------------------------------------------------
        /// Date Modified: 16/02/2012
        /// Modified By:   Josephine Gad
        /// (description)  Change DataReader to List
        /// ---------------------------------------------------
        /// Date Modified: 30/May/2013
        /// Modified By:   Josephine Gad
        /// (description)   Add field IsWithSuttle
        ///                 Add IsMealBreakfast, IsMealLunch
        ///                 Add IsMealDinner, IsMealLunchDinner   
        /// ---------------------------------------------------
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="EffectiveDate"></param>
        /// <returns></returns>
        //public static IDataReader GetHotelRoomOverrideByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        public static List<HotelRoomBlocksDTO> GetHotelRoomOverrideByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        {
            DataTable dt = null;
            DbCommand command = null;
            List<HotelRoomBlocksDTO> hotelList = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectHotelRoomOverride");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int32, Int32.Parse(BranchID));
                db.AddInParameter(command, "@pRoomTypeID", DbType.Int16, Int16.Parse(RoomTypeID));
                if (EffectiveDate != "")
                {
                    db.AddInParameter(command, "@pEffectiveDate", DbType.Date, DateTime.Parse(EffectiveDate));
                }
                dt = db.ExecuteDataSet(command).Tables[0];

                hotelList = (from a in dt.AsEnumerable()
                              select new HotelRoomBlocksDTO
                              {
                                  BranchIDInt = a["colBranchIDInt"].ToString(),
                                  BranchName = a["BranchName"].ToString(),
                                  
                                  OverrideCurrentcyID = a["OverrideCurrentcyID"].ToString(),
                                  OverrideRate = a["OverrideRate"].ToString(),
                                  OverrideTaxPercent = a["OverrideTaxPercent"].ToString(),
                                  OverrideRoomBlocks = a["OverrideRoomBlocks"].ToString(),
                                  OverrideIsTaxInclusive = GlobalCode.Field2Bool(a["OverrideIsTaxInclusive"]),

                                  ContractRoomBlocks = a["ContractRoomBlocks"].ToString(),
                                  ContractCurrencyID = a["ContractCurrencyID"].ToString(),
                                  ContractRate = a["ContractRate"].ToString(),
                                  ContractTaxPercent = a["ContractTaxPercent"].ToString(),
                                  ContractIsTaxInclusive = GlobalCode.Field2Bool(a["ContractIsTaxInclusive"]),

                                  ContractCurrency = a["ContractCurrency"].ToString(),
                                  OverrideReservedRoom = a["ReservedRooms"].ToString(),
                                  IsWithOverflow = GlobalCode.Field2Bool(a["IsWithOverflow"]),

                                  ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                  IsWithSuttle = GlobalCode.Field2Bool(a["colWithShuttleBit"]),

                                  IsMealBreakfast = GlobalCode.Field2Bool(a["colBreakfastBit"]),
                                  IsMealLunch = GlobalCode.Field2Bool(a["colLunchBit"]),
                                  IsMealDinner = GlobalCode.Field2Bool(a["colDinnertBit"]),
                                  IsMealLunchDinner = GlobalCode.Field2Bool(a["colLunchOrDinnerBit"]),                                  

                              }).ToList();

                return hotelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (hotelList != null)
                {
                    hotelList = null;
                }
            }
        }
        /// <summary>
        /// Date Created:  27/11/2011
        /// Created By:    Josephine Gad
        /// (description)  Save Hotel Room Override by branch, room and date
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="EffectiveDate"></param>
        /// <param name="RatePerDayMoney"></param>
        /// <param name="Currency"></param>
        /// <param name="RoomRateTaxPercentage"></param>
        /// <param name="RoomRateTaxInclusive"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="RoomBlocksPerDayInt"></param>
        /// <param name="CreatedByVarchar"></param>
        public static DataTable SaveHotelRoomOverrideByBranch(string BranchID, DateTime EffectiveDate, string RatePerDayMoney,
            string Currency, string RoomRateTaxPercentage, bool RoomRateTaxInclusive, string RoomTypeID, string RoomBlocksPerDayInt,
            string CreatedByVarchar)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            DataTable dtOverride = new DataTable();

            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();

                

                command = db.GetStoredProcCommand("uspInsertHotelRoomOverride");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int64, GlobalCode.Field2Int(BranchID));
                db.AddInParameter(command, "@pEffectiveDate", DbType.DateTime, EffectiveDate);
                db.AddInParameter(command, "@pRatePerDayMoney", DbType.Double, GlobalCode.Field2Decimal(RatePerDayMoney));
                db.AddInParameter(command, "@pCurrencyIDInt", DbType.Int16, GlobalCode.Field2Int(Currency));
                db.AddInParameter(command, "@pRoomRateTaxPercentage", DbType.Double, GlobalCode.Field2Decimal(RoomRateTaxPercentage));
                db.AddInParameter(command, "@pRoomRateTaxInclusive", DbType.Boolean, RoomRateTaxInclusive);
                db.AddInParameter(command, "@pRoomTypeID", DbType.Int16, Int16.Parse(RoomTypeID));
                db.AddInParameter(command, "@pRoomBlocksPerDayInt", DbType.Int16, Int16.Parse(RoomBlocksPerDayInt));

                db.AddInParameter(command, "@pCreatedByVarchar", DbType.String, CreatedByVarchar);
                db.AddOutParameter(command, "@HotelRoomID", DbType.Int32, 8);
                db.AddOutParameter(command, "@ReturnType", DbType.Int32, 8);

                db.ExecuteNonQuery(command, trans);
                trans.Commit();

                Int32 HotelRoomID = Convert.ToInt32(db.GetParameterValue(command, "@HotelRoomID"));
                Int32 ReturnType = Convert.ToInt32(db.GetParameterValue(command, "@ReturnType"));

                dtOverride.Columns.Add("dtHotelRoomID");
                dtOverride.Columns.Add("dtReturnType");

                dtOverride.Rows.Add(HotelRoomID, ReturnType);
                return dtOverride;   
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }              
            }
        }
        /// <summary>
        /// Date Created:   16/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get department and stripes of hotel branch
        /// ------------------------------------------------------------
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        /// ------------------------------------------------------------
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change void to List<HotelBranchDeptStripe>
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static List<HotelBranchDeptStripe> GetHotelBranchDeptStripes(string BranchID)
        {
            DataTable dt = null;
            DbCommand command = null;
            List<HotelBranchDeptStripe> list = null;
            try 
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectHotelBranchDeptStripe");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int16, Int32.Parse(BranchID));
                dt = db.ExecuteDataSet(command).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new HotelBranchDeptStripe
                        {
                            BranchDeptStripeID = GlobalCode.Field2Int(a["BranchDeptStripeID"]),
                            DeptID =  GlobalCode.Field2TinyInt(a["DeptID"]),
                            DeptName = a.Field<string>("DeptName"),
                            Stripes = GlobalCode.Field2Decimal(a["Stripes"]),
                            StripeName = a.Field<string>("StripeName")
                        }).ToList();
                //HotelBranchDeptStripe.DeptStripeList = list;
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:  23/12/2011
        /// Created By:    Josephine Gad
        /// (description)  Get rank exceptions of hotel branch
        /// -----------------------------------------------------
        /// Date Modified: 21/03/2012
        /// Modified By:   Josephine Gad
        /// (description)  Change DataTable to List
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static List<HotelRankException> GetHotelBranchRankException(string BranchID)
        {
            DataTable dt = null;
            DbCommand command = null;            
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectHotelBranchRankException");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int16, Int32.Parse(BranchID));
                dt = db.ExecuteDataSet(command).Tables[0];
                List<HotelRankException> list = new List<HotelRankException>();
                list = (from a in dt.AsEnumerable()
                        select new HotelRankException {
                            BranchRankExceptID = GlobalCode.Field2Int(a["BranchRankExceptID"]),
                            BranchID = GlobalCode.Field2Int(a["BranchID"]),
                            RankID = GlobalCode.Field2Int(a["RankID"]),
                            RankName = a.Field<string>("RankName")
                            //iOrder = a.Field<string>("iOrder")
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/12/2011
        /// Description: automatically book seafarers
        /// </summary>
        /// <param name="trID"></param>
        /// <param name="mrID"></param>
        /// <param name="VendorId"></param>
        /// <param name="BranchId"></param>
        /// <param name="roomTypeId"></param>
        /// <param name="CheckInDate"></param>
        /// <param name="UserId"></param>
        /// <param name="Status"></param>
        /// <param name="CityId"></param>
        /// <param name="CountryId"></param>
        /// <param name="BfastBit"></param>
        /// <param name="LunchBit"></param>
        /// <param name="DinnerBit"></param>
        /// <param name="LunchOrDinnerBit"></param>
        /// <param name="withShuttle"></param>
        /// <param name="VoucherAmount"></param>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static Boolean AutomaticBooking(Int32 trID, Int32 mrID, Int32 VendorId, Int32 BranchId, Int32 roomTypeId,
            DateTime CheckInDate, String UserId, String Status, Int32 CityId, Int32 CountryId, Boolean BfastBit, Boolean LunchBit,
            Boolean DinnerBit, Boolean LunchOrDinnerBit, Boolean withShuttle, String VoucherAmount, Int32 ContractId)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;
            DbConnection dbConnection = db.CreateConnection();
            dbConnection.Open();
            DbTransaction dbTransaction = dbConnection.BeginTransaction();
            Boolean AddOverride = false;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspAutomaticHotelBooking");
                db.AddInParameter(dbCommand, "@pTravelRequestId", DbType.Int32, trID);
                db.AddInParameter(dbCommand, "@pManualRequestId", DbType.Int32, mrID);
                db.AddInParameter(dbCommand, "@pVendorId", DbType.Int32, VendorId);
                db.AddInParameter(dbCommand, "@pBranchId", DbType.Int32, BranchId);
                db.AddInParameter(dbCommand, "@pRoomType", DbType.Int32, roomTypeId);
                db.AddInParameter(dbCommand, "@pCheckInDate", DbType.Date, CheckInDate);
                db.AddInParameter(dbCommand, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCommand, "@pSFStatus", DbType.String, Status);
                db.AddInParameter(dbCommand, "@pCityId", DbType.Int32, CityId);
                db.AddInParameter(dbCommand, "@pCountryId", DbType.Int32, CountryId);
                db.AddInParameter(dbCommand, "@pBfastBit", DbType.Boolean, BfastBit);
                db.AddInParameter(dbCommand, "@pLunchBit", DbType.Boolean, LunchBit);
                db.AddInParameter(dbCommand, "@pDinnerBit", DbType.Boolean, DinnerBit);
                db.AddInParameter(dbCommand, "@pLunchOrDinner", DbType.Boolean, LunchOrDinnerBit);
                db.AddInParameter(dbCommand, "@pWithShuttle", DbType.Boolean, withShuttle);
                db.AddInParameter(dbCommand, "@pContractId", DbType.Int32, ContractId);
                db.AddInParameter(dbCommand, "@pVoucherAmount", DbType.String, VoucherAmount);
                db.AddOutParameter(dbCommand, "@pisNewOverride", DbType.Boolean, 50);
                db.ExecuteNonQuery(dbCommand, dbTransaction);

                AddOverride = Boolean.Parse(db.GetParameterValue(dbCommand, "@pisNewOverride").ToString());
                return AddOverride;
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:    10/02/2012
        /// Created By:      Josephine Gad
        /// (description)    Get Hotel Emergency Room Blocks
        /// ---------------------------------------------------       
        /// Date Modified:   16/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Replace IDataReader with List
        /// ---------------------------------------------------  
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="EffectiveDate"></param>
        /// <returns></returns>
        //public static IDataReader GetHotelRoomEmergencyByBranch(string BranchID, string RoomTypeID, string EffectiveDate)
        public static List<HotelRoomBlocksEmergencyDTO> GetHotelRoomEmergencyByBranch(string BranchID, string RoomTypeID, string EffectiveDate)    
        {
            List<HotelRoomBlocksEmergencyDTO> list = null;
            DbCommand command = null;
            DataTable dt = null;
            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                command = db.GetStoredProcCommand("uspSelectHotelRoomEmergency");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int32, Int32.Parse(BranchID));
                db.AddInParameter(command, "@pRoomTypeID", DbType.Int16, Int16.Parse(RoomTypeID));
                if (EffectiveDate != "")
                {
                    db.AddInParameter(command, "@pEffectiveDate", DbType.Date, DateTime.Parse(EffectiveDate));
                }
                dt = db.ExecuteDataSet(command).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new HotelRoomBlocksEmergencyDTO {
                            BranchIDInt = a["colBranchIDInt"].ToString(),
                            BranchName = a["BranchName"].ToString(),
                            Currency = a["EmergencyCurrentcyID"].ToString(),
                            Rate = a["EmergencyRate"].ToString(),
                            Tax = a["EmergencyTaxPercent"].ToString(),
                            RoomBlocks = a["EmergencyRoomBlocks"].ToString(),
                            IsTaxInclusive = GlobalCode.Field2Bool(a["EmergencyTaxInclusive"])
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:  10/02/2012
        /// Created By:    Josephine Gad
        /// (description)  Save Hotel Emergency Room by branch, room and date
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="EffectiveDate"></param>
        /// <param name="RatePerDayMoney"></param>
        /// <param name="Currency"></param>
        /// <param name="RoomRateTaxPercentage"></param>
        /// <param name="RoomRateTaxInclusive"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="RoomBlocksPerDayInt"></param>
        /// <param name="CreatedByVarchar"></param>
        public static DataTable SaveHotelRoomEmergencyByBranch(string BranchID, DateTime EffectiveDate, string RatePerDayMoney,
            string Currency, string RoomRateTaxPercentage, bool RoomRateTaxInclusive, string RoomTypeID, string RoomBlocksPerDayInt,
            string CreatedByVarchar)
        {
            DbCommand command = null;
            DbConnection conn = null;
            DbTransaction trans = null;

            DataTable dtEmergency = new DataTable();

            try
            {
                Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
                conn = db.CreateConnection();
                conn.Open();
                trans = conn.BeginTransaction();                                

                command = db.GetStoredProcCommand("uspInsertHotelRoomEmergency");
                db.AddInParameter(command, "@pBranchIDInt", DbType.Int64, Int64.Parse(BranchID));
                db.AddInParameter(command, "@pEffectiveDate", DbType.DateTime, EffectiveDate);
                db.AddInParameter(command, "@pRatePerDayMoney", DbType.Double, GlobalCode.Field2Decimal(RatePerDayMoney));
                db.AddInParameter(command, "@pCurrencyIDInt", DbType.Int16, Int16.Parse(Currency));
                db.AddInParameter(command, "@pRoomRateTaxPercentage", DbType.Double, GlobalCode.Field2Decimal(RoomRateTaxPercentage));
                db.AddInParameter(command, "@pRoomRateTaxInclusive", DbType.Boolean, RoomRateTaxInclusive);
                db.AddInParameter(command, "@pRoomTypeID", DbType.Int16, Int16.Parse(RoomTypeID));
                db.AddInParameter(command, "@pRoomBlocksPerDayInt", DbType.Int16, Int16.Parse(RoomBlocksPerDayInt));                

                db.AddInParameter(command, "@pCreatedByVarchar", DbType.String, CreatedByVarchar);
                db.AddOutParameter(command, "@HotelRoomID", DbType.Int32, 8);
                db.AddOutParameter(command, "@ReturnType", DbType.Int32, 8);

                db.ExecuteNonQuery(command, trans);
                trans.Commit();

                Int32 HotelRoomID = Convert.ToInt32(db.GetParameterValue(command, "@HotelRoomID"));
                Int32 ReturnType = Convert.ToInt32(db.GetParameterValue(command, "@ReturnType"));

                dtEmergency.Columns.Add("dtHotelRoomID");
                dtEmergency.Columns.Add("dtReturnType");

                dtEmergency.Rows.Add(HotelRoomID, ReturnType);
                return dtEmergency;              
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 21/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Validate country code
        /// </summary>  
        public static Boolean countrycodeInInsertExist(String CountryCode, Int32 CountryID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspValidateCountrycode");
                db.AddInParameter(dbCommand, "@pCountryCode", DbType.String, CountryCode);
                db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32, CountryID);
                
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 03/03/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Added validation for emergency room block(s) versus the total no. of emergency booking(s)
        /// </summary>  
        public static IDataReader GetEmergencyTotalBookings(String Date, Int32 BranchID, Int32 RoomTypeID)
        {
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader drEmergencyTotalBookings = null;
            
            try
            {
                dbCommand = db.GetStoredProcCommand("uspValidateEmergencyRoomBlock");
                db.AddInParameter(dbCommand, "@pDate", DbType.Date, GlobalCode.Field2DateTime(Date));
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, BranchID);
                db.AddInParameter(dbCommand, "@pRoomTypeID", DbType.Int32, RoomTypeID);

                drEmergencyTotalBookings = db.ExecuteReader(dbCommand);
                return drEmergencyTotalBookings;
            }
            catch (Exception ex)
            {
                if (drEmergencyTotalBookings != null)
                {
                    drEmergencyTotalBookings.Close();
                    drEmergencyTotalBookings.Dispose();
                }
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }                
                //if (drEmergencyTotalBookings != null)
                //{
                //    drEmergencyTotalBookings.Close();
                //    drEmergencyTotalBookings.Dispose();
                //}
                //if (dbConnection != null)
                //{
                //    dbConnection.Close();
                //    dbConnection.Dispose();
                //}
            }
        }

        //public void GetHotelVendorListforNonTurn() {
        //    List<HotelVendorNonTurnPortList> listHotel = new List<HotelVendorNonTurnPortList>();


        //    Database SFDatebase= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;
        //    IDataReader dataReader = null;
        //    DataTable HotelVendorDataTable = null;
        //    DataSet ds = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetAllHotelVendorName");
        //        ds = SFDatebase.ExecuteDataSet(SFDbCommand);
        //        HotelVendorDataTable = ds.Tables[0];

        //        listHotel = (from a in HotelVendorDataTable.AsEnumerable()
        //                     select new HotelVendorNonTurnPortList {
        //                         BranchID = GlobalCode.Field2Int(a["colBranchIDInt"]),
        //                         VendorID = GlobalCode.Field2Int(a["colVendorIDInt"]),
        //                         VendorName = a.Field<string>("colVendorBranchNameVarchar")
        //                     }).ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (SFDbCommand != null)
        //        {
        //            SFDbCommand.Dispose();
        //        }
        //        if (HotelVendorDataTable != null)
        //        {
        //            HotelVendorDataTable.Dispose();
        //        }
        //    }
        //}

        #endregion
    }
}

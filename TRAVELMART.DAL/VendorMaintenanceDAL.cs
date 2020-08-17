using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using TRAVELMART.Common;
using System.Web;

namespace TRAVELMART.DAL
{
    public class VendorMaintenanceDAL
    {
        /// <summary>            
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vehicle vendor
        /// -----------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close DbConnection
        /// -----------------------------------
        /// Date Modified: 08/08/2011
        /// Modified By: Ryan Bautsita
        /// (description) Add contact person column        
        /// ====================================
        /// Date Modified:  24/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Delete parameter vendorAccredited
        /// </summary>
        public static Int32 vendorMaintenanceInsert(string vendorCode, string vendorName, string vendorType,
            string vendorAddress, Int32 city, Int32 country, string contactno,
            string createdby, string vendorPrimaryId, string ContactPerson)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dt = new DataTable();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertVendorMaintenance");

                db.AddInParameter(dbCommand, "@pcolVendorCodeVarchar", DbType.String, vendorCode);
                db.AddInParameter(dbCommand, "@pcolVendorNameVarchar", DbType.String, vendorName);
                db.AddInParameter(dbCommand, "@pcolVendorTypeVarchar", DbType.String, vendorType);                
                db.AddInParameter(dbCommand, "@pcolVendorAddressVarchar", DbType.String, vendorAddress);
                db.AddInParameter(dbCommand, "@pcolCityIDInt", DbType.Int32, city);
                db.AddInParameter(dbCommand, "@pcolCountryIDInt", DbType.Int32, country);
                db.AddInParameter(dbCommand, "@pcolVendorContactNoVarchar", DbType.String, contactno);
                //db.AddInParameter(dbCommand, "@pcolIsAccreditedBit", DbType.Boolean, vendorAccredited);
                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.String, vendorPrimaryId);
                db.AddInParameter(dbCommand, "@pcolVendorContactPersonVarchar", DbType.String, ContactPerson);
                db.AddOutParameter(dbCommand, "@pVendorID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 VendorID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pVendorID"));
                return VendorID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally 
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created:   29/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Insert vendor branch            
        /// =================================
        /// Date Modified:  14/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add WithShuttle parameter
        /// =================================
        /// Date Modified:  13/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add IsOn and IsOff parameter
        /// =================================
        /// Date Modified:  21/May/2015
        /// Modified By:    Josephine Monteza
        /// (description)   Add sVendorID
        /// =================================
        /// </summary>
       

        public static Int32 vendorBranchMaintenanceInsert(string vendorName,
            string vendorAddress, Int32 city, Int32 country, string contactno,
            string createdby, string vendorPrimaryId, string ContactPerson, Boolean IsPortAgent, string BranchID, Boolean Rating,
            Boolean Officer, string branchCode, string EmailTo, string EmailCc, bool IsOn, bool IsOff, 
            string faxNo, string website, string InstructionON, string InstructionOFF) //string Email
        {            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dt = new DataTable();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertVendorBranchMaintenance");

                db.AddInParameter(dbCommand, "@pcolVendorNameVarchar", DbType.String, vendorName);
                db.AddInParameter(dbCommand, "@pcolVendorAddressVarchar", DbType.String, vendorAddress);
                db.AddInParameter(dbCommand, "@pcolCityIDInt", DbType.Int32, city);
                db.AddInParameter(dbCommand, "@pcolCountryIDInt", DbType.Int32, country);
                db.AddInParameter(dbCommand, "@pcolVendorContactNoVarchar", DbType.String, contactno);
                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.String, vendorPrimaryId);
                db.AddInParameter(dbCommand, "@pcolVendorContactPersonVarchar", DbType.String, ContactPerson);
                db.AddInParameter(dbCommand, "@colFranchiseBit", DbType.Boolean, IsPortAgent);
                db.AddInParameter(dbCommand, "@pcolBranchID", DbType.String, BranchID);
                db.AddInParameter(dbCommand, "@pcolRating", DbType.Boolean, Rating);
                db.AddInParameter(dbCommand, "@pcolOfficer", DbType.Boolean, Officer);
                db.AddInParameter(dbCommand, "@pcolBranchCode", DbType.String, branchCode);
                //db.AddInParameter(dbCommand, "@pcolEmail", DbType.String, Email);                
                db.AddInParameter(dbCommand, "@pcolEmailTo", DbType.String, EmailTo);
                db.AddInParameter(dbCommand, "@pcolEmailCc", DbType.String, EmailCc);
                //db.AddInParameter(dbCommand, "@pcolWithShuttleBit", DbType.Boolean, WithShuttle);
                db.AddInParameter(dbCommand, "@pcolOnBit", DbType.Boolean, IsOn);
                db.AddInParameter(dbCommand, "@pcolOffBit", DbType.Boolean, IsOff);
                db.AddOutParameter(dbCommand, "@pBranchID", DbType.Int32, 8);
                db.AddInParameter(dbCommand, "@pcolFaxNoVarchar", DbType.String, faxNo);
                db.AddInParameter(dbCommand, "@pColWebsiteVarchar", DbType.String, website);

                db.AddInParameter(dbCommand, "@pInstructionOn", DbType.String, InstructionON);
                db.AddInParameter(dbCommand, "@pInstructionOff", DbType.String, InstructionOFF);


                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                Int32 pBranchID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pBranchID"));
                return pBranchID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   10/Nov/2017
        /// Created By:     JMonteza
        /// (description)   Update vendor Instruction
        /// </summary>
        /// <param name="iBranchID"></param>
        /// <param name="InstructionON"></param>
        /// <param name="InstructionOFF"></param>
        /// <param name="strLogDescription"></param>
        /// <param name="strFunction"></param>
        /// <param name="strPageName"></param>
        /// <param name="DateGMT"></param>
        /// <param name="CreatedDate"></param>
        /// <param name="CreatedBy"></param>
        public static void vendorBranchMaintenanceUpdate(int iBranchID, string InstructionON, string InstructionOFF,
            String strLogDescription, String strFunction, String strPageName,
            DateTime DateGMT, DateTime CreatedDate, String CreatedBy) 
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

                dbCommand = db.GetStoredProcCommand("uspUpdateVendorBranch");

                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, iBranchID);
                db.AddInParameter(dbCommand, "@pInstructionOn", DbType.String, InstructionON);
                db.AddInParameter(dbCommand, "@pInstructionOff", DbType.String, InstructionOFF);

                db.AddInParameter(dbCommand, "@pLogDescriptionVarchar", DbType.String, strLogDescription);
                db.AddInParameter(dbCommand, "@pFunctionVarchar", DbType.String, strFunction);
                db.AddInParameter(dbCommand, "@pPageNameVarchar", DbType.String, strPageName);
                db.AddInParameter(dbCommand, "@pTimeZoneVarchar", DbType.String, strTimeZone);
                db.AddInParameter(dbCommand, "@pDateCreatedGMT", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime, CreatedDate);
                db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, CreatedBy);   


                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        ///<summary>
        ///Date  Created: 27/06/2014
        ///Created By:    Michael Brian C. Evangelista
        ///Description:   Get GetServiceProviderAirportbyBrand
        /// </summary>
        public static DataTable GetServiceProviderAirportbyBrand(string vendorId)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {

                dbCommand = db.GetStoredProcCommand("uspGetAllAirPortsbyBrand"); //uspGetListVehicleCompany
                db.AddInParameter(dbCommand, "pcolPortAgentVendorIDInt", DbType.String, vendorId);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
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
       ///<summary>
       ///Date Created 07/02/2014
       ///Created By: Michael Brian C. Evangelista
        ///Description: Add GetServiceProviderbyVendor
       /// </summary>
        public static DataTable GetServiceProviderbyVendor(string vendorId) {
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;

            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetAllAirPortsbyVendor"); //uspGetListVehicleCompany
                db.AddInParameter(dbCommand, "pcolPortAgentVendorIDInt", DbType.String, vendorId);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { 
            
            
            }


        
        }
        /// <summary>
        /// Date Created:   20/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Insert airport hotel branch
        /// </summary>
        /// <param name="airportID"></param>
        /// <param name="branchID"></param>       
        public static void SaveAirportHotelBranch(int airportID, int hotelbranchID, string userString)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSaveAirportHotelBranch");
                SFDatebase.AddInParameter(SFDbCommand, "@pAirportID", DbType.Int32, airportID);
                SFDatebase.AddInParameter(SFDbCommand, "@pHotelBranchID", DbType.Int32, hotelbranchID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserVarchar", DbType.String, userString);

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
        /// Date Created: 29/07/2011
        /// Created By: Ryan Bautista
        /// (description) Insert vendor branch voucher           
        /// </summary>
        public static Int32 InsertHotelBranchVoucher(string createdby, string BranchID, decimal Stripes, decimal VAmount, int DayNo)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dt = new DataTable();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertHotelBranchVoucher");

                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolBranchID", DbType.String, BranchID);
                db.AddInParameter(dbCommand, "@pcolStripesDecimal", DbType.Decimal, Stripes);
                db.AddInParameter(dbCommand, "@pcolAmountMoney", DbType.Decimal, VAmount);
                db.AddInParameter(dbCommand, "@pcolDayNoTinyInt", DbType.Int16, DayNo);
                db.AddOutParameter(dbCommand, "@pVoucherID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 VoucherID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pVoucherID"));
                return VoucherID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 29/07/2011
        /// Created By: Ryan Bautista
        /// (description) Insert Room type on hotel branch        
        /// </summary>
        public static Int32 InsertHotelBranchRoomType(string createdby, Int32 BranchID, string RoomType)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dt = new DataTable();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();                       

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertHotelBranchRoomType");

                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, createdby);
                db.AddInParameter(dbCommand, "@pcolBranchID", DbType.String, BranchID);
                db.AddInParameter(dbCommand, "@pcolRoomType", DbType.String, RoomType);
                db.AddOutParameter(dbCommand, "@pRoomTypeID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 RoomTypeID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pRoomTypeID"));
                return RoomTypeID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                 if (dbCommand != null)
                {
                    dbCommand.Dispose();                    
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        ///// <summary>            
        ///// Date Created:   17/11/2011
        ///// Created By:     Josephine Gad
        ///// (description)   Insert Hotel Room with capacity
        ///// </summary>
        //public static void InsertHotelBranchRoomCapacity(string BranchID, DateTime StartDate, DateTime EndDate,
        //    string RatePerDay, string CurrencyID, string RoomRateTaxPercentage, bool RoomRateTaxInclusive,
        //    string RoomTypeID, string NumberOfUnits, string Mon, string Tue, string Wed, string Thu,
        //    string Fri, string Sat, string Sun, string UserName
        //    )
        //{
        //    Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbCommand dbCommand = null;
            
        //    DbConnection connection = db.CreateConnection();
        //    connection.Open();
        //    DbTransaction trans = connection.BeginTransaction();

        //    try
        //    {
        //        dbCommand = db.GetStoredProcCommand("uspInsertHotelRooms");

        //        db.AddInParameter(dbCommand, "@pBranchIDInt", DbType.Int64, Int64.Parse(BranchID));
        //        db.AddInParameter(dbCommand, "@pStartDate", DbType.Date, StartDate);
        //        db.AddInParameter(dbCommand, "@pEndDate", DbType.Date, EndDate);
        //        db.AddInParameter(dbCommand, "@pRatePerDayMoney", DbType.Double, RatePerDay);
        //        db.AddInParameter(dbCommand, "@pCurrencyIDInt", DbType.Int16, Int16.Parse(CurrencyID));
        //        db.AddInParameter(dbCommand, "@pRoomRateTaxPercentage", DbType.Decimal, Decimal.Parse(RoomRateTaxPercentage));
        //        db.AddInParameter(dbCommand, "@pRoomRateTaxInclusive", DbType.Boolean, RoomRateTaxInclusive);
        //        db.AddInParameter(dbCommand, "@pRoomTypeID", DbType.Int32, Int32.Parse(RoomTypeID));

        //        db.AddInParameter(dbCommand, "@pNumberOfUnitsInt", DbType.Int16, Int16.Parse(NumberOfUnits));
        //        db.AddInParameter(dbCommand, "@pMonInt", DbType.Int16, Int16.Parse(Mon));
        //        db.AddInParameter(dbCommand, "@pTuesInt", DbType.Int16, Int16.Parse(Tue));
        //        db.AddInParameter(dbCommand, "@pWedInt", DbType.Int16, Int16.Parse(Wed));
        //        db.AddInParameter(dbCommand, "@pThursInt", DbType.Int16, Int16.Parse(Thu));
        //        db.AddInParameter(dbCommand, "@pFriInt", DbType.Int16, Int16.Parse(Fri));
        //        db.AddInParameter(dbCommand, "@pSatInt", DbType.Int16, Int16.Parse(Sat));
        //        db.AddInParameter(dbCommand, "@pSunInt", DbType.Int16, Int16.Parse(Sun));
        //        db.AddInParameter(dbCommand, "@pDateCreatedDate", DbType.DateTime,DateTime.Now);
        //        db.AddInParameter(dbCommand, "@pCreatedByVarchar", DbType.String, UserName);

        //        db.ExecuteNonQuery(dbCommand, trans);
        //        trans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //        {
        //            connection.Close();
        //            connection.Dispose();
        //        }
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();                    
        //        }
        //        if (trans != null)
        //        {
        //            trans.Dispose();                    
        //        }
        //    }
        //}
        /// <summary>            
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete Hotel Room with capacity
        /// </summary>
        public static void DeleteHotelBranchRoomCapacity(string HotelRoomID, string UserName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteHotelRooms");

                db.AddInParameter(dbCommand, "@pHotelBookingID", DbType.Int64, Int64.Parse(HotelRoomID));   
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserName);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle vendor maintenance information
        /// -----------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Josephine Gad
        /// (description) Close IDataReader, DataTable and DbConnection  
        /// -------------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary> 
        public static IDataReader vendorMaintenanceInformation(Int32 vendorPrimaryId)
        {
            Database VendorTransactionDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand VMDbCommand = null;
            IDataReader dataReader = null;
            try
            {
                VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVendorInformation");
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolVendorIdInt", DbType.Int32, vendorPrimaryId);                
                dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
                return dataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VMDbCommand != null)
                {
                    VMDbCommand.Dispose();
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
        /// (description) Get vendor branch maintenance information
        /// ------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>            
        //public static IDataReader vendorBranchMaintenanceInformation(Int32 vendorPrimaryId)
        //{
        //    Database VendorTransactionDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
        //    DbConnection connection = VendorTransactionDatebase.CreateConnection();
        //    DbCommand VMDbCommand = null;
        //    IDataReader dataReader = null;
        //    try
        //    {
        //        VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVendorBranchInformation");
        //        VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolVendorIdInt", DbType.Int32, vendorPrimaryId);
        //        dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
        //        return dataReader;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (VMDbCommand != null)
        //        {
        //            VMDbCommand.Dispose();
        //        }
        //        if (connection != null)
        //        {
        //            connection.Close();
        //            connection.Dispose();
        //        }
        //    }
        //} 
        /// Date Created: 01/08/2011
        /// Created By: Ryan Bautista
        /// (description) Get vendor branch maintenance information
        /// ------------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ------------------------------------------------------
        /// Date Modified: 24/Aug/2016
        /// Modified By:   Josephine Monteza
        /// (description)  Use class instead of DataReader 
        ///                List<HotelBranchDetails> list and  List<IMSVendor> listIMS
        /// ------------------------------------------------------
        public static void vendorBranchMaintenanceInformation(int HotelID)
        {
            List<HotelBranchDetails> list = new List<HotelBranchDetails>();
            List<IMSVendor> listIMS = new List<IMSVendor>();
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand com = null;
            DataSet ds = null;
            DataTable dtHotel = null;
            DataTable dtIMSVendor = null;

            try 
            {
                com = db.GetStoredProcCommand("uspGetVendorBranchInformation");
                db.AddInParameter(com, "@pcolVendorIdInt", DbType.Int32, HotelID);
                ds = db.ExecuteDataSet(com);
                if (ds.Tables.Count > 0)
                {
                    dtHotel = ds.Tables[0];
                    dtIMSVendor = ds.Tables[1];

                    if (dtHotel.Rows.Count > 0)
                    {
                        list = (from a in dtHotel.AsEnumerable()
                                select new HotelBranchDetails
                                {
                                    HotelName = a.Field<string>("colVendorBranchNameVarchar"),
                                    CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
                                    CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                    ContractNo = a.Field<string>("colContactNoVarchar"),

                                    VendorId = GlobalCode.Field2Int(a["colVendorIdInt"]),
                                    HotelId = GlobalCode.Field2Int(a["colBranchIDInt"]),
                                    ContactPerson = a.Field<string>("colContactPersonVarchar"),
                                    Address = a.Field<string>("colAddressVarchar"),
                                    IsFranchise = GlobalCode.Field2Bool(a["colFranchiseBit"]),

                                    BranchCode = a.Field<string>("colBranchCodeVarchar"),
                                    EmailTo = a.Field<string>("colEmailToVarchar"),
                                    EmailCc = a.Field<string>("colEmailCcVarchar"),
                                    CityName = a.Field<string>("colCityNameVarchar"),

                                    OnBit = GlobalCode.Field2Bool(a["colOnBit"]),
                                    OffBit = GlobalCode.Field2Bool(a["colOffBit"]),

                                    FaxNo = a.Field<string>("colFaxNoVarchar"),
                                    Website = a.Field<string>("colWebsiteVarchar"),

                                    InstructionOn = a.Field<string>("colInstructionOn"),
                                    InstructionOff = a.Field<string>("colInstructionOff")

                                }).ToList();
                    }

                    if (dtIMSVendor.Rows.Count > 0)
                    {
                        listIMS = (from a in dtIMSVendor.AsEnumerable()
                                select new IMSVendor
                                {
                                    iVendorID = GlobalCode.Field2Int(a["colVendorNumber"]),
                                    sVendorName = a.Field<string>("colVendorName"),
                                    sVendorNameWithId = a.Field<string>("VendorNameWithID"),
                                }).ToList();
                    }
                }
                HttpContext.Current.Session["HotelMaintenance_HotelDetails"] = list;
                HttpContext.Current.Session["HotelMaintenance_IMSVendor"] = listIMS;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dtHotel != null)
                {
                    dtHotel.Dispose();
                }
                if (dtIMSVendor != null)
                {
                    dtIMSVendor.Dispose();
                }
            }
         //   return list;
        }

        /// <summary>            
        /// Date Created: 08/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor branch maintenance information     
        /// ---------------------------------------------------
        /// Date Modified:  27/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to IDataReader
        /// </summary> 
        public static IDataReader vehicleVendorBranchMaintenanceInformation(Int32 branchId)
        {
            Database VendorTransactionDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand VMDbCommand = null;
            IDataReader dataReader = null;            
            try
            {
                VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVehicleVendorBranchInformation");                
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolBranchIdInt", DbType.Int32, branchId);
                dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
                return dataReader;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VMDbCommand != null)
                {
                    VMDbCommand.Dispose();
                }
                //if (dataReader != null)
                //{
                //    dataReader.Close();
                //    dataReader.Dispose();
                //}               
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle type branch information                 
        /// </summary> 
        public static DataTable vehicleTypeBranchInfoLoad(Int32 vehicleId)
        {
            Database VendorTransactionDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand VMDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVehicleTypeBranchInformation");
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolVehicleIdBigint", DbType.Int32, vehicleId);                
                dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
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
                if (VMDbCommand != null)
                {
                    VMDbCommand.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
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
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle branch type list
        /// </summary> 
        public static DataTable GetVehicleTypeBranchList(Int32 branchId)
        {
            Database VendorTransactionDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection connection = VendorTransactionDatebase.CreateConnection();
            DbCommand VMDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                VMDbCommand = VendorTransactionDatebase.GetStoredProcCommand("uspGetVehicleTypeBranch");
                VendorTransactionDatebase.AddInParameter(VMDbCommand, "@pcolBranchIdInt", DbType.Int32, branchId);
                dataReader = VendorTransactionDatebase.ExecuteReader(VMDbCommand);
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
                if (VMDbCommand != null)
                {
                    VMDbCommand.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
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
        /// Date Created: 06/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert vehicle branch          
        /// </summary>
        public static void vehicleInsertUpdateVendorBranch(String branchName, String branchCode, String vendorAddress, Int32 city, Int32 country, String contactNo,
                                                      String strUserName, Int32 vendorId, String contactPerson, Boolean IsFranchise, String branchID, Int16 VehicleType)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dt = new DataTable();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertVendorVehicleBranchMaintenance");

                db.AddInParameter(dbCommand, "@pcolVendorNameVarchar", DbType.String, branchName);
                db.AddInParameter(dbCommand, "@pcolBranchCodeVarchar", DbType.String, branchCode);
                db.AddInParameter(dbCommand, "@pcolVendorAddressVarchar", DbType.String, vendorAddress);
                db.AddInParameter(dbCommand, "@pcolCityIDInt", DbType.Int32, city);
                db.AddInParameter(dbCommand, "@pcolCountryIDInt", DbType.Int32, country);
                db.AddInParameter(dbCommand, "@pcolVendorContactNoVarchar", DbType.String, contactNo);
                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, strUserName);
                db.AddInParameter(dbCommand, "@pcolVendorIdInt", DbType.Int32, vendorId);
                db.AddInParameter(dbCommand, "@pcolVendorContactPersonVarchar", DbType.String, contactPerson);
                db.AddInParameter(dbCommand, "@colFranchiseBit", DbType.Boolean, IsFranchise);
                db.AddInParameter(dbCommand, "@pcolBranchID", DbType.String, branchID);
                db.AddInParameter(dbCommand, "@pcolVehicleTypeTinyInt", DbType.Int16, VehicleType);
                //db.AddOutParameter(dbCommand, "@pBranchID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
                //Int32 pBranchID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@pBranchID"));
                //return pBranchID;                
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 06/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert/update vehicle type branch
        /// </summary>
        public static Int32 vehicleInsertUpdateVehicleTypeBranch(Int32 vehicleId, Int32 vehicleType, String Name, Int32 branchId, Int32 Capacity, String User)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            //DataTable dt = new DataTable();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertUpdateVehicleTypeBranch");

                db.AddInParameter(dbCommand, "@pcolVehicleIdBigint", DbType.Int32, vehicleId);
                db.AddInParameter(dbCommand, "@pcolVehicleTypeIdInt", DbType.Int32, vehicleType);
                db.AddInParameter(dbCommand, "@pcolVehicleNameVarchar", DbType.String, Name);
                db.AddInParameter(dbCommand, "@pcolBranchIDInt", DbType.Int32, branchId);
                db.AddInParameter(dbCommand, "@pcolVehicleCapacityInt", DbType.Int32, Capacity);
                db.AddInParameter(dbCommand, "@pUser", DbType.String, User);
                db.AddOutParameter(dbCommand, "@VehicleID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 pVehicleID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@VehicleID"));
                return pVehicleID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Insert/update vehicle type branch
        /// </summary>
        public static void vehicleInsertUpdateBranch(Int32 branchID, Int32 vehicleID, Int32 vehicleTypeID, String Name, Int32 Capacity, String userName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertBranchVehicle");

                db.AddInParameter(dbCommand, "@pcolVehicleIdBigint", DbType.Int32, vehicleID);
                db.AddInParameter(dbCommand, "@pcolVehicleTypeIdInt", DbType.Int32, vehicleTypeID);
                db.AddInParameter(dbCommand, "@pcolVehicleNameVarchar", DbType.String, Name);
                db.AddInParameter(dbCommand, "@pcolBranchIDInt", DbType.Int32, branchID);
                db.AddInParameter(dbCommand, "@pcolVehicleCapacityInt", DbType.Int32, Capacity);
                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, userName);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>                
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting vendor branch details          
        /// </summary>
        public static DataTable vehicleGetBranchName(Int32 branchId)
        {
            Database branchDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand branchDbCommand = null;
            DataTable branchDataTable = null;
            IDataReader dataReader = null;
            try
            {
                branchDbCommand = branchDatebase.GetStoredProcCommand("uspGetListVehicleBranchName");
                branchDatebase.AddInParameter(branchDbCommand, "@pcolVendorIDInt", DbType.Int32, branchId);
                dataReader = branchDatebase.ExecuteReader(branchDbCommand);
                branchDataTable = new DataTable();
                branchDataTable.Load(dataReader);
                return branchDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (branchDbCommand != null)
                {
                    branchDbCommand.Dispose();
                }
                if (branchDataTable != null)
                {
                    branchDataTable.Dispose();
                }
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting country list
        /// </summary>
        public static DataTable countryList()
        {
            Database countryDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand countryDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                countryDbCommand = countryDatebase.GetStoredProcCommand("uspSelectCountryLists");

                dataReader = countryDatebase.ExecuteReader(countryDbCommand);
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
                if (countryDbCommand != null)
                {
                    countryDbCommand.Dispose();
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
            }
        }

        /// <summary>                        
        /// Date Created: 07/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Selecting city list by country id           
        /// </summary> 
        public static DataTable cityListByCountry(Int32 countryID)
        {
            Database cityDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand cityDbCommand = null;
            IDataReader dataReader = null;
            DataTable dt = null;
            try
            {
                cityDbCommand = cityDatebase.GetStoredProcCommand("uspSelectCityListByCountryID");
                cityDatebase.AddInParameter(cityDbCommand, "@pcolCountryIDInt", DbType.Int32, countryID);
                dataReader = cityDatebase.ExecuteReader(cityDbCommand);
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
                if (cityDbCommand != null)
                {
                    cityDbCommand.Dispose();
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
            }
        }

        /// <summary>            
        /// Date Created:   08/09/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Get list of vehicle type               
        /// </summary>
        public static DataTable vehicleGetTypeList()
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVehicleTypeList");                
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
        /// Date Created: 13/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle branch                 
        /// </summary>
        public static DataTable vehicleGetBranch()
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetListBranchVehicle");
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
        /// Date Created: 13/09/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vehicle capacity by vehicle type id                 
        /// </summary>
        public static DataTable vehicleGetCapacity(Int32 VehicleTypeID)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbConnection dbConnection = db.CreateConnection();
            DbCommand dbCommand = null;
            IDataReader dr = null;
            DataTable dt = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetVehicleCapacity");
                db.AddInParameter(dbCommand, "@pcolVehicleIDInt", DbType.Int32, VehicleTypeID);
                dr = db.ExecuteReader(dbCommand);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
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
        /// Date Created:   17/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get branch details by branchID
        /// --------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static IDataReader GetBranchDetails(string BranchID)
        {
            IDataReader dr = null;
            DbCommand command = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectVendorBranchByBranchID");
                db.AddInParameter(command, "@pBranchID", DbType.Int32, Int32.Parse(BranchID));
                dr = db.ExecuteReader(command);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(command != null)
                {
                    command.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:       17/10/2011
        /// Created By:         Ryan Bautista
        /// (description)       Get branch details by branchID
        /// -----------------------------------------------------
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Replace DataTable with List
        /// -----------------------------------------------------
        /// Date Modified:      17/09/2012
        /// Modified By:        Josephine Gad
        /// (description)       Add VoucherID
        /// </summary>  
        /// <param name="BranchID"></param>
        /// <returns></returns>
        //public static DataTable GetHotelBranchVoucherByBranchID(Int32 BranchID)
        public static List<HotelBranchVoucherList> GetHotelBranchVoucherByBranchID(string BranchID)
        {
            DataTable dt = null;
            DbCommand command = null;
            List<HotelBranchVoucherList> list = null;
            try
            {
                Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
                command = db.GetStoredProcCommand("uspSelectHotelBranchVoucherByBranchID");
                db.AddInParameter(command, "@pBranchID", DbType.Int32, GlobalCode.Field2Int(BranchID));
                dt = db.ExecuteDataSet(command).Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new HotelBranchVoucherList {
                            VoucherID = GlobalCode.Field2Int(a["colVoucherIDInt"]),
                            Stripe = GlobalCode.Field2Decimal(a["Stripe"]),
                            Amount = GlobalCode.Field2Decimal(a["Amount"]),
                            BranchID = GlobalCode.Field2Int(a["colBranchIDInt"]),
                            DayNo = GlobalCode.Field2TinyInt(a["DayNo"])                            
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
        /// Date Created:   10/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Remove Hotel branch voucher
        /// --------------------------------------------
        /// Date Modified:  22/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add DayNo
        /// --------------------------------------------
        /// Date Modified:  17/09/2012
        /// Modified By:    Josephine Gad
        /// (description)   Use VoucherID in updating table
        ///                 Do not return value    
        /// --------------------------------------------
        /// </summary>
        //public static Int32 RemoveHotelBranchVoucherByID(Int32 branchID, decimal stripes, String userName, string DayNo)
        public static void RemoveHotelBranchVoucherByID(String userName, int VoucherID)    
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspRemoveHotelBranchVoucher");

                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, userName);
                //db.AddInParameter(dbCommand, "@pcolBranchID", DbType.Int32, branchID);
                //db.AddInParameter(dbCommand, "@pcolStripesDecimal", DbType.Decimal, stripes);
                //db.AddInParameter(dbCommand, "@pcolDayNoTinyInt", DbType.Int16, int.Parse(DayNo));
                db.AddInParameter(dbCommand, "@pVoucherID", DbType.Int32, VoucherID);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                //Int32 VoucherID = GlobalCode.Field2Int(db.GetParameterValue(dbCommand, "@pVoucherID"));
                //return VoucherID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>            
        /// Date Created: 10/09/2011
        /// Created By: Ryan Bautista
        /// (description) Remove Hotel branch voucher
        /// </summary>
        public static Int32 RemoveHotelBranchRoomByID(Int32 branchID, string RoomType, String userName)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspRemoveHotelBranchRoomType");

                db.AddInParameter(dbCommand, "@pcolCreatedByVarchar", DbType.String, userName);
                db.AddInParameter(dbCommand, "@pcolBranchID", DbType.Int32, branchID);
                db.AddInParameter(dbCommand, "@pcolRoomType", DbType.String, RoomType);
                db.AddOutParameter(dbCommand, "@pBranchRoomID", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 BranchRoomID = GlobalCode.Field2Int(db.GetParameterValue(dbCommand, "@pBranchRoomID"));
                return BranchRoomID;

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();   
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   21/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert vendor branch Department Stripe           
        /// </summary>
        public static Int32 InsertHotelHotelBranchDeptStripe(string DeptStripeID, string BranchID, string DepartmentID,
            string Stripes, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertHotelBranchDeptStripe");

                db.AddInParameter(dbCommand, "@pBranchDeptStripeID", DbType.Int32, Int32.Parse(DeptStripeID));
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, Int32.Parse(BranchID));
                db.AddInParameter(dbCommand, "@pDepartmentID", DbType.Int32, Int32.Parse(DepartmentID));
                db.AddInParameter(dbCommand, "@pStripes", DbType.Double, double.Parse(Stripes));
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@pBranchDeptStripeIDInt", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 BranchDeptStripeID =GlobalCode.Field2Int(db.GetParameterValue(dbCommand, "@pBranchDeptStripeIDInt"));
                return BranchDeptStripeID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   21/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete vendor branch Department Stripe           
        /// </summary>
        public static void DeleteHotelHotelBranchDeptStripe(string DeptStripeID, string DeletedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteHotelBranchDeptStripe");

                db.AddInParameter(dbCommand, "@pBranchDeptStripeID", DbType.Int32, Int32.Parse(DeptStripeID));
                db.AddInParameter(dbCommand, "@pDeletedBy", DbType.String, DeletedBy);                

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Insert vendor branch Rank Exception      
        /// </summary>
        public static Int32 InsertHotelHotelBranchRankException(string BranchRankExceptionID, 
            string BranchID, string RankID, string CreatedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspInsertHotelBranchRankException");

                db.AddInParameter(dbCommand, "@pBranchRankExceptionID", DbType.Int32, Int32.Parse(BranchRankExceptionID));
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, Int32.Parse(BranchID));                
                db.AddInParameter(dbCommand, "@pRankID", DbType.Int32, Int32.Parse(RankID));
                db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);
                db.AddOutParameter(dbCommand, "@pBranchRankExceptionIDInt", DbType.Int32, 8);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();

                Int32 pBranchRankExceptionID =GlobalCode.Field2Int(db.GetParameterValue(dbCommand, "@pBranchRankExceptionID"));
                    return pBranchRankExceptionID;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }
        /// <summary>            
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Vendol Type
        /// </summary>
        public static void DeleteHotelHotelBranchRankException(string BranchRankExceptionID, string DeletedBy)
        {
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCommand = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();

            try
            {
                dbCommand = db.GetStoredProcCommand("uspDeleteHotelBranchRankException");

                db.AddInParameter(dbCommand, "@pBranchRankExceptionID", DbType.Int32, GlobalCode.Field2Int(BranchRankExceptionID));
                db.AddInParameter(dbCommand, "@pDeletedBy", DbType.String, DeletedBy);

                db.ExecuteNonQuery(dbCommand, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
            }
        }


        /// <summary>            
        /// Date Created:   23/12/2011
        /// Created By:     Muhallidin Wali
        /// (description)   Insert vendor branch Rank Exception      
        /// </summary>
        public static DataTable getVendorType(Int16 loadType)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            DataTable dt = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectVendorType");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, loadType);

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
        /// Date Created:   02/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle Vendor Details, Country List and City List
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsGetByID(Int32 iVendorID, Int16 iLoadType)
        {
            List<CountryList> listCountry = new List<CountryList>();
            List<CityList> listCity = new List<CityList>();
            List<VendorVehicleDetails> listVehicleDetails = new List<VendorVehicleDetails>();
            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<VendorVehicleType> listVendorVehicleType = new List<VendorVehicleType>();
            List<VehiclePlate> listPlate = new List<VehiclePlate>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtVehicle = null;
            DataTable dtCountry = null;
            DataTable dtCity = null;
            DataTable dtCityFilter = null;
            DataTable dtVendorVehicleType = null;
            DataTable dtVehicleType = null;
            DataTable dtPlate = null;


            HttpContext.Current.Session["CountryList"] = listCountry;
            HttpContext.Current.Session["CityList"] = listCountry;
            HttpContext.Current.Session["VehicleVendorDetails"] = listVehicleDetails;
            HttpContext.Current.Session["VehicleVendorCityFilter"] = "";
            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["VendorVehicleType"] = listVendorVehicleType;
            HttpContext.Current.Session["VendorVehiclePlate"] = listPlate;

            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsGetByID");
                db.AddInParameter(dbCom, "@pVehicleVendorIDInt", DbType.String, iVendorID);
                db.AddInParameter(dbCom, "@pLoadType", DbType.String, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[0] != null)
                {
                    dtVehicle = dSet.Tables[0];
                    listVehicleDetails = (from a in dtVehicle.AsEnumerable()
                                          select new VendorVehicleDetails { 
                                             VehicleID = GlobalCode.Field2Int(a["colVehicleVendorIDInt"]),
                                             VendorName = GlobalCode.Field2String(a["colVehicleVendorNameVarchar"]),
                                             
                                             CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                             CountryName = GlobalCode.Field2String(a["CountryName"]),

                                             CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                             CityName = GlobalCode.Field2String(a["CityName"]),

                                             ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                                             FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                                             ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                                             Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                                             EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                                             EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                                             Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
                                             VendorIMS_ID = GlobalCode.Field2String(a["colVendorIMS_IDVarchar"]),
                                          }).ToList();
                }
                if (dSet.Tables[1] != null)
                {
                    dtCountry = dSet.Tables[1];
                    listCountry = (from a in dtCountry.AsEnumerable()
                                   select new CountryList {
                                       CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                       CountryName = GlobalCode.Field2String(a["colCountryNameVarchar"]),
                                   }).ToList();
                }
                if (dSet.Tables[2] != null)
                {
                    dtCity = dSet.Tables[2];
                    listCity = (from a in dtCity.AsEnumerable()
                                   select new CityList
                                   {
                                       CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
                                       CityName = GlobalCode.Field2String(a["CityCodeName"]),
                                   }).ToList();
                }
                if (dSet.Tables[3] != null)
                {
                    dtCityFilter = dSet.Tables[3];
                } 
                if (dSet.Tables[4] != null)
                {
                    dtVehicleType = dSet.Tables[4];
                    listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                      select new VehicleType {
                                          VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                          VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])
                                      }).ToList();
                }
                if (dSet.Tables[5] != null)
                {
                    dtVendorVehicleType = dSet.Tables[5];
                    listVendorVehicleType = (from a in dtVendorVehicleType.AsEnumerable()
                                      select new VendorVehicleType {
                                          VehicleVendorID = GlobalCode.Field2Int(a["VehicleVendorID"]),
                                          VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                          VehicleType = GlobalCode.Field2String(a["VehicleType"])
                                      }).ToList();
                }
                if (dSet.Tables[6] != null)
                {
                    dtPlate = dSet.Tables[6];
                    listPlate = (from a in dtPlate.AsEnumerable()
                                             select new VehiclePlate
                                             {

                                                 VehiclePlateID = GlobalCode.Field2Int(a["VehiclePlateID"]),
                                                 VehiclePlateName = GlobalCode.Field2String(a["VehiclePlateName"]),

                                                 VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                 VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"]),
                                                 VehicleColor = GlobalCode.Field2String(a["VehicleColor"]),

                                                 VehicleColorName = GlobalCode.Field2String(a["ColorName"]),

                                                 VehicleMakeID = GlobalCode.Field2Int(a["VehicleMakeID"]),
                                                 VehicleMakeName = GlobalCode.Field2String(a["VehicleMakeName"]),
                                                 VehicleBrandID = GlobalCode.Field2Int(a["VehicleBrandID"]),
                                                 VehicleBrandName = GlobalCode.Field2String(a["VehicleBrandName"]),
                                                 VehicleDetailID = GlobalCode.Field2Int(a["VehicleDetailID"]),
                                             }).ToList();
                }

                List<VehicleMaker> VihMake = new List<VehicleMaker>();

                if (dSet.Tables[7] != null)
                {

                    VihMake = (from a in dSet.Tables[7].AsEnumerable()
                               select new VehicleMaker
                                             {
                                                  VehicleMakeID = GlobalCode.Field2Int( a["VehicleMakeID"]),
                                                  VehicleMakeName = GlobalCode.Field2String(a["VehicleMakeName"]),
                                                  VehicleBrandID = GlobalCode.Field2Int(a["VehicleBrandID"]),
                                                  VehicleMakeBrandID = GlobalCode.Field2String(a["VehicleBrandID"]) + '-' + GlobalCode.Field2String(a["VehicleMakeID"]) + '-' + GlobalCode.Field2String(a["VehicleTypeID"])
                                             }).ToList();
                }

                List<VehicleBrand> vehBrand = new List<VehicleBrand>();
                if (dSet.Tables[8] != null)
                {

                    vehBrand = (from a in dSet.Tables[8].AsEnumerable()
                               select new VehicleBrand
                               {
                                   VehicleBrandID = GlobalCode.Field2Int(a["VehicleBrandID"]),
                                   VehicleBrandName = GlobalCode.Field2String(a["VehicleBrandName"])
                               }).ToList();
                }

                HttpContext.Current.Session["CountryList"] = listCountry;
                HttpContext.Current.Session["CityList"] = listCity;
                HttpContext.Current.Session["VehicleVendorDetails"] = listVehicleDetails;
                HttpContext.Current.Session["VehicleVendorCityFilter"] = GlobalCode.Field2String(dtCityFilter.Rows[0][0]);
                HttpContext.Current.Session["VehicleType"] = listVehicleType;
                HttpContext.Current.Session["VendorVehicleType"] = listVendorVehicleType;
                HttpContext.Current.Session["VendorVehiclePlate"] = listPlate;
                HttpContext.Current.Session["VehicleMake"] = VihMake;
                HttpContext.Current.Session["VehicleBrand"] = vehBrand;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }               
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtVehicle != null)
                {
                    dtVehicle.Dispose();
                }
                if (dtCountry != null)
                {
                    dtCountry.Dispose();
                }
                if (dtCity != null)
                {
                    dtCity.Dispose();
                }
                if (dtCityFilter != null)
                {
                    dtCityFilter.Dispose();
                }
                if (dtVendorVehicleType != null)
                {
                    dtVendorVehicleType.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtPlate != null)
                {
                    dtPlate.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   08/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle Type of Vendor 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsTypeGet(Int32 iVendorID)
        {
            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<VendorVehicleType> listVendorVehicleType = new List<VendorVehicleType>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtVehicleType = null;
            DataTable dtVendorVehicleType = null;

            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["VendorVehicleType"] = listVendorVehicleType;
            
            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsTypeGet");
                db.AddInParameter(dbCom, "@pVehicleVendorIDInt", DbType.String, iVendorID);                
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[0] != null)
                {
                    dtVehicleType = dSet.Tables[0];
                    listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                       select new VehicleType
                                       {
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])
                                       }).ToList();
                }
                if (dSet.Tables[1] != null)
                {
                    dtVendorVehicleType = dSet.Tables[1];
                    listVendorVehicleType = (from a in dtVendorVehicleType.AsEnumerable()
                                       select new VendorVehicleType
                                       {
                                           VehicleVendorID = GlobalCode.Field2Int(a["VehicleVendorID"]),
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleType = GlobalCode.Field2String(a["VehicleType"])
                                       }).ToList();
                }

                HttpContext.Current.Session["VehicleType"] = listVehicleType;
                HttpContext.Current.Session["VendorVehicleType"] = listVendorVehicleType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }              
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtVendorVehicleType != null)
                {
                    dtVendorVehicleType.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   05/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Save Vehicle Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsSave( Int32 iVehicleVendorID, string sVehicleVendorName, Int32 iCountryID,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress, 
            string sEmailCc, string sEmailTo, string sWebsite, string sVendorID,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dt, DataTable dtPlateNo)
        {
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsSave");

                SqlParameter param = new SqlParameter("@pTableVar", dt);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                dbCom.Parameters.Add(param);

                param = new SqlParameter("@pTablePlateNo", dtPlateNo);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                dbCom.Parameters.Add(param);


                db.AddInParameter(dbCom, "@pVehicleVendorIDInt", DbType.Int64, iVehicleVendorID);
                db.AddInParameter(dbCom, "@pVehicleVendorNameVarchar", DbType.String, sVehicleVendorName);
                db.AddInParameter(dbCom, "@pCountryIDInt", DbType.Int32, iCountryID);
                db.AddInParameter(dbCom, "@pCityIDInt", DbType.Int32, iCityID);

                db.AddInParameter(dbCom, "@pContactNoVarchar", DbType.String, sContactNo);
                db.AddInParameter(dbCom, "@pFaxNoVarchar", DbType.String, sFaxNo);
                db.AddInParameter(dbCom, "@pContactPersonVarchar", DbType.String, sContactPerson);
                db.AddInParameter(dbCom, "@pAddressVarchar", DbType.String, sAddress);
                db.AddInParameter(dbCom, "@pEmailCcVarchar", DbType.String, sEmailCc);
                db.AddInParameter(dbCom, "@pEmailToVarchar", DbType.String, sEmailTo);
                db.AddInParameter(dbCom, "@pWebsiteVarchar", DbType.String, sWebsite);
                db.AddInParameter(dbCom, "@pVendorIMS_IDVarchar", DbType.String, sVendorID);


                db.AddInParameter(dbCom, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCom, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCom, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCom, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCom, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCom, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCom, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.ExecuteNonQuery(dbCom, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtPlateNo != null)
                {
                    dtPlateNo.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   12/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport of Vendor Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsAirportGet(Int32 iContractID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            List<Airport> listAirport = new List<Airport>();
            List<Airport> listAirportNotExist = new List<Airport>();
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtExists = null;
            DataTable dtNotExists = null;
           
            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsGetAirport");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pFilterByInt", DbType.Int16, iFilterBy);
                db.AddInParameter(dbCom, "@pAirportFilter", DbType.String, sFilter);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                    HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;

                    if (dSet.Tables[0] != null)
                    {
                        dtExists = dSet.Tables[0];
                        listAirport = (from a in dtExists.AsEnumerable()
                                       select new Airport
                                       {
                                           AirportSeaportID = GlobalCode.Field2Int(a["ContractAirID"]),
                                           AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                           AirportName = GlobalCode.Field2String(a["Airport"])

                                       }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtNotExists = dSet.Tables[1];
                        listAirportNotExist = (from a in dtNotExists.AsEnumerable()
                                               select new Airport
                                                 {
                                                     AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                     AirportName = GlobalCode.Field2String(a["Airport"])
                                                 }).ToList();
                    }

                    HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                    HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                        
                        if (dSet.Tables[0] != null)
                        {
                            dtExists = dSet.Tables[0];
                            listAirport = (from a in dtExists.AsEnumerable()
                                           select new Airport
                                           {
                                               AirportSeaportID = GlobalCode.Field2Int(a["ContractAirID"]),
                                               AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                               AirportName = GlobalCode.Field2String(a["Airport"])

                                           }).ToList();
                        }
                        HttpContext.Current.Session["VendorAirportExists"] = listAirport;
                    }
                    else
                    {                        
                        HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;

                        if (dSet.Tables[0] != null)
                        {
                            dtNotExists = dSet.Tables[0];
                            listAirportNotExist = (from a in dtNotExists.AsEnumerable()
                                                   select new Airport
                                                   {
                                                       AirportID = GlobalCode.Field2Int(a["AirportID"]),
                                                       AirportName = GlobalCode.Field2String(a["Airport"])
                                                   }).ToList();
                        }
                        HttpContext.Current.Session["VendorAirportNOTExists"] = listAirportNotExist;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtExists != null)
                {
                    dtExists.Dispose();
                }
                if (dtNotExists != null)
                {
                    dtNotExists.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Seaport of Vendor Contract
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsSeaportGet(Int32 iContractID, Int16 iFilterBy,
            string sFilter, bool isViewExists, Int16 iLoadType)
        {
            List<Seaport> listSeaport = new List<Seaport>();
            List<Seaport> listSeaportNotExist = new List<Seaport>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtExists = null;
            DataTable dtNotExists = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsGetSeaport");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pFilterByInt", DbType.Int16, iFilterBy);
                db.AddInParameter(dbCom, "@pSeaportFilter", DbType.String, sFilter);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;

                    if (dSet.Tables[0] != null)
                    {
                        dtExists = dSet.Tables[0];
                        listSeaport = (from a in dtExists.AsEnumerable()
                                       select new Seaport
                                       {
                                           ID = GlobalCode.Field2Int(a["ContractSeaID"]),
                                           SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                           SeaportName = GlobalCode.Field2String(a["Seaport"])

                                       }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtNotExists = dSet.Tables[1];
                        listSeaportNotExist = (from a in dtNotExists.AsEnumerable()
                                               select new Seaport
                                               {
                                                   SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                   SeaportName = GlobalCode.Field2String(a["Seaport"])
                                               }).ToList();
                    }

                    HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;

                        if (dSet.Tables[0] != null)
                        {
                            dtExists = dSet.Tables[0];
                            listSeaport = (from a in dtExists.AsEnumerable()
                                           select new Seaport
                                           {
                                               ID = GlobalCode.Field2Int(a["ContractSeaID"]),
                                               SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                               SeaportName = GlobalCode.Field2String(a["Seaport"])

                                           }).ToList();
                        }
                        HttpContext.Current.Session["VendorSeaportExists"] = listSeaport;
                    }
                    else
                    {
                        HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;

                        if (dSet.Tables[0] != null)
                        {
                            dtNotExists = dSet.Tables[0];
                            listSeaportNotExist = (from a in dtNotExists.AsEnumerable()
                                                   select new Seaport
                                                   {
                                                       SeaportID = GlobalCode.Field2Int(a["SeaportID"]),
                                                       SeaportName = GlobalCode.Field2String(a["Seaport"])
                                                   }).ToList();
                        }
                        HttpContext.Current.Session["VendorSeaportNOTExists"] = listSeaportNotExist;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtExists != null)
                {
                    dtExists.Dispose();
                }
                if (dtNotExists != null)
                {
                    dtNotExists.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   14/Aug/2013
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle type of company
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsVehicleTypeGet(Int32 iContractID, Int32 iVehicleVendorID,
            bool isViewExists, Int16 iLoadType)
        {
            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtVehicleType = null;
            DataTable dtVehicleTypeCapacity = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsGetVehicleType");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pVehicleVendorIDInt", DbType.Int64, iVehicleVendorID);                
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VehicleType"] = listVehicleType;
                    HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;

                    if (dSet.Tables[0] != null)
                    {
                        dtVehicleType = dSet.Tables[0];
                        listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                           select new VehicleType
                                       {
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])

                                       }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtVehicleTypeCapacity = dSet.Tables[1];
                        listVehicleTypeCapacity = (from a in dtVehicleTypeCapacity.AsEnumerable()
                                                   select new ContractVendorVehicleTypeCapacity
                                               {
                                                   ContractVehicleCapacityIDInt = GlobalCode.Field2Int(a["ContractVehicleCapacityID"]),
                                                   ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                   VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                   VehicleType = GlobalCode.Field2String(a["VehicleTypeName"]),
                                                   MinCapacity = GlobalCode.Field2Int(a["MinCapacity"]),
                                                   MaxCapacity = GlobalCode.Field2Int(a["MaxCapacity"]),
                                               }).ToList();
                    }
                    HttpContext.Current.Session["VehicleType"] = listVehicleType;
                    HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;                   
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;

                        if (dSet.Tables[0] != null)
                        {
                            dtVehicleTypeCapacity = dSet.Tables[0];
                            listVehicleTypeCapacity = (from a in dtVehicleTypeCapacity.AsEnumerable()
                                                       select new ContractVendorVehicleTypeCapacity
                                           {
                                               ContractVehicleCapacityIDInt = GlobalCode.Field2Int(a["ContractVehicleCapacityID"]),
                                               ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                               VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                               VehicleType = GlobalCode.Field2String(a["VehicleTypeName"]),
                                               MinCapacity = GlobalCode.Field2Int(a["MinCapacity"]),
                                               MaxCapacity = GlobalCode.Field2Int(a["MaxCapacity"]),
                                           }).ToList();
                        }
                        HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;
                    }
                    else
                    {
                        HttpContext.Current.Session["VehicleType"] = listVehicleType;

                        if (dSet.Tables[0] != null)
                        {
                            dtVehicleType = dSet.Tables[0];
                            listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                           select new VehicleType
                                           {
                                               VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                               VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])
                                           }).ToList();
                        }
                        HttpContext.Current.Session["VehicleType"] = listVehicleType;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtVehicleTypeCapacity != null)
                {
                    dtVehicleTypeCapacity.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get vehicle Route
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void VehicleVendorsRouteGet()
        {
            List<VehicleRoute> listRoute = new List<VehicleRoute>();
            HttpContext.Current.Session["VehicleRoute"] = listRoute;

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtRoute = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsGetRoute");
                dtRoute = db.ExecuteDataSet(dbCom).Tables[0];
                
                listRoute = (from a in dtRoute.AsEnumerable()
                             select new VehicleRoute
                             {
                                 RouteID = GlobalCode.Field2Int(a["colRouteIDInt"]),
                                 RouteDesc = a.Field<string>("colRouteNameVarchar")
                             }).ToList();

                HttpContext.Current.Session["VehicleRoute"] = listRoute;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }                
            }
        }
        /// <summary>
        /// Date Created:   12/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider details
        /// ------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// ------------------------------------------
        /// Date Modified: 05/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Rename GetPortAgent to GetPortAgentByID
        ///                Change IDataReader to void
        /// ------------------------------------------  
        /// </summary>        
        public static void GetPortAgentByID(int iPortAgentID, Int16 iLoadType)
        {
            List<CountryList> listCountry = new List<CountryList>();
            List<CityList> listCity = new List<CityList>();
            List<VendorPortAgentDetails> listPortAgentDetails = new List<VendorPortAgentDetails>();
            List<AirportDTO> listAirportPortAgent = new List<AirportDTO>();
            List<AirportDTO> listAirportNotInPortAgent = new List<AirportDTO>();

            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<PortAgentVehicleType> listPortAgentVehicleType = new List<PortAgentVehicleType>();
            //List<BrandList> listBrand = new List<BrandList>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtPortAgent = null;
            DataTable dtCountry = null;
            DataTable dtCity = null;
            DataTable dtCityFilter = null;
            DataTable dtAirport = null;
            DataTable dtAirportNotExist = null;

            DataTable dtVehicleType = null;
            DataTable dtPortAgentVehicleType = null;
            //DataTable dtBrand = null;


            HttpContext.Current.Session["CountryList"] = listCountry;
            HttpContext.Current.Session["CityList"] = listCity;
            HttpContext.Current.Session["PortAgentVendorDetails"] = listPortAgentDetails;
            HttpContext.Current.Session["PortAgentVendorCityFilter"] = "";
            HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
            HttpContext.Current.Session["PortAgentAirportNotExist"] = listAirportNotInPortAgent;
            
            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["PortAgentVehicleType"] = listPortAgentVehicleType;
            //HttpContext.Current.Session["PortAgentBrand"] = listBrand;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVendorGetByID");
                db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.String, iPortAgentID);
                db.AddInParameter(dbCom, "@pLoadType", DbType.String, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[0] != null)
                {
                    dtPortAgent = dSet.Tables[0];
                    listPortAgentDetails = (from a in dtPortAgent.AsEnumerable()
                                            select new VendorPortAgentDetails
                                            {
                                                PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                                PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                                CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                                CountryName = GlobalCode.Field2String(a["CountryName"]),

                                                CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                                CityName = GlobalCode.Field2String(a["CityName"]),

                                                ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                                                FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                                                ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                                                Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                                                EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                                                EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                                                Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
                                                VendorIMS_ID = GlobalCode.Field2String(a["colVendorIMS_IDVarchar"]),
                                            }).ToList();
                }
                if (dSet.Tables[1] != null)
                {
                    dtCountry = dSet.Tables[1];
                    listCountry = (from a in dtCountry.AsEnumerable()
                                   select new CountryList
                                   {
                                       CountryId = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                       CountryName = GlobalCode.Field2String(a["colCountryNameVarchar"]),
                                   }).ToList();
                }
                if (dSet.Tables[2] != null)
                {
                    dtCity = dSet.Tables[2];
                    listCity = (from a in dtCity.AsEnumerable()
                                select new CityList
                                {
                                    CityId = GlobalCode.Field2Int(a["colCityIDInt"]),
                                    CityName = GlobalCode.Field2String(a["CityCodeName"]),
                                }).ToList();
                }
                if (dSet.Tables[3] != null)
                {
                    dtCityFilter = dSet.Tables[3];
                }
                if (dSet.Tables[4] != null)
                {
                    dtAirport = dSet.Tables[4];
                    listAirportPortAgent = (from a in dtAirport.AsEnumerable()
                                            select new AirportDTO
                                            {
                                                AirportIDString = GlobalCode.Field2String(a["AirportCode"]),
                                                AirportNameString = GlobalCode.Field2String(a["AirportName"])
                                            }).ToList();
                }
                if (dSet.Tables[5] != null)
                {
                    dtAirportNotExist = dSet.Tables[5];
                    listAirportNotInPortAgent = (from a in dtAirportNotExist.AsEnumerable()
                                                 select new AirportDTO
                                                 {
                                                     AirportIDString = GlobalCode.Field2String(a["AirportCode"]),
                                                     AirportNameString = GlobalCode.Field2String(a["AirportName"])
                                                 }).ToList();
                }

                if (dSet.Tables[6] != null)
                {
                    dtVehicleType = dSet.Tables[6];
                    listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                       select new VehicleType
                                       {
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])
                                       }).ToList();
                }
                if (dSet.Tables[7] != null)
                {
                    dtPortAgentVehicleType = dSet.Tables[7];
                    listPortAgentVehicleType = (from a in dtPortAgentVehicleType.AsEnumerable()
                                             select new PortAgentVehicleType
                                             {
                                                 PortAgentVendorID = GlobalCode.Field2Int(a["PortAgentVendorID"]),
                                                 VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                 VehicleType = GlobalCode.Field2String(a["VehicleType"])
                                             }).ToList();
                }
                //if (dSet.Tables[8] != null)
                //{
                //    dtBrand = dSet.Tables[8];
                //    listBrand = (from a in dtBrand.AsEnumerable()
                //                                select new BrandList
                //                                {
                //                                    BrandID = GlobalCode.Field2Int(a["colBrandIdInt"]),
                //                                    BrandName = GlobalCode.Field2String(a["Brand"])
                //                                }).ToList();
                //}
                HttpContext.Current.Session["CountryList"] = listCountry;
                HttpContext.Current.Session["CityList"] = listCity;
                HttpContext.Current.Session["PortAgentVendorDetails"] = listPortAgentDetails;
                HttpContext.Current.Session["PortAgentVendorCityFilter"] = GlobalCode.Field2String(dtCityFilter.Rows[0][0]);
                HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
                HttpContext.Current.Session["PortAgentAirportNotExist"] = listAirportNotInPortAgent;

                HttpContext.Current.Session["VehicleType"] = listVehicleType;
                HttpContext.Current.Session["PortAgentVehicleType"] = listPortAgentVehicleType;
                //HttpContext.Current.Session["PortAgentBrand"] = listBrand;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtCountry != null)
                {
                    dtCountry.Dispose();
                }
                if (dtCity != null)
                {
                    dtCity.Dispose();
                }
                if (dtCityFilter != null)
                {
                    dtCityFilter.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtAirportNotExist != null)
                {
                    dtAirportNotExist.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtPortAgentVehicleType != null)
                {
                    dtPortAgentVehicleType.Dispose();
                }
                //if (dtBrand != null)
                //{
                //    dtBrand.Dispose();
                //}
            }
        }
        /// <summary>
        /// Date Created:   05/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Airport of Service Provider
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentAirport(Int32 iVendorID)
        {
            List<AirportDTO> listAirportPortAgent = new List<AirportDTO>();
            List<AirportDTO> listAirportNotInPortAgent = new List<AirportDTO>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtAirport = null;
            DataTable dtAirportNotExist = null;

            HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
            HttpContext.Current.Session["PortAgentAirportNotExist"] = listAirportNotInPortAgent;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVendorGetAirport");
                db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.String, iVendorID);
                dSet = db.ExecuteDataSet(dbCom);
                if (dSet.Tables[0] != null)
                {
                    dtAirport = dSet.Tables[0];
                    listAirportPortAgent = (from a in dtAirport.AsEnumerable()
                                            select new AirportDTO
                                            {
                                                AirportIDString = GlobalCode.Field2String(a["AirportCode"]),
                                                AirportNameString = GlobalCode.Field2String(a["AirportName"])
                                            }).ToList();
                }
                if (dSet.Tables[1] != null)
                {
                    dtAirportNotExist = dSet.Tables[1];
                    listAirportNotInPortAgent = (from a in dtAirportNotExist.AsEnumerable()
                                                 select new AirportDTO
                                                 {
                                                     AirportIDString = GlobalCode.Field2String(a["AirportCode"]),
                                                     AirportNameString = GlobalCode.Field2String(a["AirportName"])
                                                 }).ToList();
                }

                HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
                HttpContext.Current.Session["PortAgentAirportNotExist"] = listAirportNotInPortAgent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtAirportNotExist != null)
                {
                    dtAirportNotExist.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   12/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Vehicle Type of Service Provider 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void GetPortAgentVehicleType(Int32 iVendorID)
        {
            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<PortAgentVehicleType> listPortAgentVehicleType = new List<PortAgentVehicleType>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtVehicleType = null;
            DataTable dtPortAgentVehicleType = null;

            HttpContext.Current.Session["VehicleType"] = listVehicleType;
            HttpContext.Current.Session["PortAgentVehicleType"] = listPortAgentVehicleType;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVehicleTypeGet");
                db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.String, iVendorID);
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[0] != null)
                {
                    dtVehicleType = dSet.Tables[0];
                    listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                       select new VehicleType
                                       {
                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])
                                       }).ToList();
                }
                if (dSet.Tables[1] != null)
                {
                    dtPortAgentVehicleType = dSet.Tables[1];
                    listPortAgentVehicleType = (from a in dtPortAgentVehicleType.AsEnumerable()
                                             select new PortAgentVehicleType
                                             {
                                                 PortAgentVendorID = GlobalCode.Field2Int(a["PortAgentVendorID"]),
                                                 VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                 VehicleType = GlobalCode.Field2String(a["VehicleType"])
                                             }).ToList();
                }

                HttpContext.Current.Session["VehicleType"] = listVehicleType;
                HttpContext.Current.Session["VendorVehicleType"] = listPortAgentVehicleType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtPortAgentVehicleType != null)
                {
                    dtPortAgentVehicleType.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   06/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Save Service Provider Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void PortAgentVendorsSave(Int32 iPortAgentID, string sPortAgentName, Int32 iCountryID,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
            string sEmailCc, string sEmailTo, string sWebsite, string sVendorID,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dtAirport, DataTable dtVehicleType)
        {
            string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction trans = connection.BeginTransaction();
            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVendorSave");

                SqlParameter param = new SqlParameter("@pTableVar", dtAirport);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                dbCom.Parameters.Add(param);

                param = new SqlParameter("@pTableVehicleType", dtVehicleType);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.TblTempUserVessel";
                dbCom.Parameters.Add(param);

                db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.Int64, iPortAgentID);
                db.AddInParameter(dbCom, "@pPortAgentVendorNameVarchar", DbType.String, sPortAgentName);
                db.AddInParameter(dbCom, "@pCountryIDInt", DbType.Int32, iCountryID);
                db.AddInParameter(dbCom, "@pCityIDInt", DbType.Int32, iCityID);

                db.AddInParameter(dbCom, "@pContactNoVarchar", DbType.String, sContactNo);
                db.AddInParameter(dbCom, "@pFaxNoVarchar", DbType.String, sFaxNo);
                db.AddInParameter(dbCom, "@pContactPersonVarchar", DbType.String, sContactPerson);
                db.AddInParameter(dbCom, "@pAddressVarchar", DbType.String, sAddress);
                db.AddInParameter(dbCom, "@pEmailCcVarchar", DbType.String, sEmailCc);
                db.AddInParameter(dbCom, "@pEmailToVarchar", DbType.String, sEmailTo);
                db.AddInParameter(dbCom, "@pWebsiteVarchar", DbType.String, sWebsite);
                db.AddInParameter(dbCom, "@pVendorIMS_IDVarchar", DbType.String, sVendorID);

                db.AddInParameter(dbCom, "@pUserId", DbType.String, UserId);
                db.AddInParameter(dbCom, "@pDescription", DbType.String, strLogDescription);
                db.AddInParameter(dbCom, "@pFunction", DbType.String, strFunction);
                db.AddInParameter(dbCom, "@pFileName", DbType.String, strPageName);
                db.AddInParameter(dbCom, "@pTimezone", DbType.String, strTimeZone);
                db.AddInParameter(dbCom, "@pGMTDATE", DbType.DateTime, DateGMT);
                db.AddInParameter(dbCom, "@pCreateDate", DbType.DateTime, CreatedDate);

                db.ExecuteNonQuery(dbCom, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (trans != null)
                {
                    trans.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Plate No. of Vendor 
        /// ---------------------------------------------------------------     
        /// </summary>
        public static List<VehiclePlate> VehicleVendorsPlateNoGet(Int32 iVendorID)
        {
            List<VehiclePlate> list = new List<VehiclePlate>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dt = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspVehicleVendorsTypeGet");
                db.AddInParameter(dbCom, "@pVehicleVendorIDInt", DbType.String, iVendorID);
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[2] != null)
                {
                    dt = dSet.Tables[2];
                    list = (from a in dt.AsEnumerable()
                            select new VehiclePlate
                                       {



                                           VehiclePlateID = GlobalCode.Field2Int(a["VehiclePlateID"]),
                                           VehiclePlateName = GlobalCode.Field2String(a["VehiclePlateName"]),

                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                           VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"]),
                                           VehicleColor = GlobalCode.Field2String(a["VehicleColor"]),

                                           VehicleColorName = GlobalCode.Field2String(a["ColorName"]),

                                           VehicleMakeID = GlobalCode.Field2Int(a["VehicleMakeID"]),
                                           VehicleMakeName = GlobalCode.Field2String(a["VehicleMakeName"]),
                                           VehicleBrandID = GlobalCode.Field2Int(a["VehicleBrandID"]),
                                           VehicleBrandName = GlobalCode.Field2String(a["VehicleBrandName"]),
                                           VehicleDetailID = GlobalCode.Field2Int(a["VehicleDetailID"]),

                                       }).ToList();
                }
                return list;                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }               
            }
        }
        /// <summary>
        /// Date Created:   28/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider Vehicle Type
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void PortAgentVehicleTypeGet(Int32 iContractID, Int32 iPortAgentID,
            bool isViewExists, Int16 iLoadType)
        {
            List<VehicleType> listVehicleType = new List<VehicleType>();
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();

            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtVehicleType = null;
            DataTable dtVehicleTypeCapacity = null;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentGetVehicleType");
                db.AddInParameter(dbCom, "@pContractIdInt", DbType.Int64, iContractID);
                db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.Int64, iPortAgentID);
                db.AddInParameter(dbCom, "@pIsViewExists", DbType.Boolean, isViewExists);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (iLoadType == 0)
                {
                    HttpContext.Current.Session["VehicleType"] = listVehicleType;
                    HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;

                    if (dSet.Tables[0] != null)
                    {
                        dtVehicleType = dSet.Tables[0];
                        listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                           select new VehicleType
                                           {
                                               VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                               VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])

                                           }).ToList();
                    }
                    if (dSet.Tables[1] != null)
                    {
                        dtVehicleTypeCapacity = dSet.Tables[1];
                        listVehicleTypeCapacity = (from a in dtVehicleTypeCapacity.AsEnumerable()
                                                   select new ContractVendorVehicleTypeCapacity
                                                   {
                                                       ContractVehicleCapacityIDInt = GlobalCode.Field2Int(a["ContractVehicleCapacityID"]),
                                                       ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                       VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                       VehicleType = GlobalCode.Field2String(a["VehicleTypeName"]),
                                                       MinCapacity = GlobalCode.Field2Int(a["MinCapacity"]),
                                                       MaxCapacity = GlobalCode.Field2Int(a["MaxCapacity"]),
                                                   }).ToList();
                    }
                    HttpContext.Current.Session["VehicleType"] = listVehicleType;
                    HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;
                }
                else
                {
                    if (isViewExists)
                    {
                        HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;

                        if (dSet.Tables[0] != null)
                        {
                            dtVehicleTypeCapacity = dSet.Tables[0];
                            listVehicleTypeCapacity = (from a in dtVehicleTypeCapacity.AsEnumerable()
                                                       select new ContractVendorVehicleTypeCapacity
                                                       {
                                                           ContractVehicleCapacityIDInt = GlobalCode.Field2Int(a["ContractVehicleCapacityID"]),
                                                           ContractID = GlobalCode.Field2Int(a["ContractID"]),
                                                           VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                           VehicleType = GlobalCode.Field2String(a["VehicleTypeName"]),
                                                           MinCapacity = GlobalCode.Field2Int(a["MinCapacity"]),
                                                           MaxCapacity = GlobalCode.Field2Int(a["MaxCapacity"]),
                                                       }).ToList();
                        }
                        HttpContext.Current.Session["ContractVendorVehicleTypeCapacity"] = listVehicleTypeCapacity;
                    }
                    else
                    {
                        HttpContext.Current.Session["VehicleType"] = listVehicleType;

                        if (dSet.Tables[0] != null)
                        {
                            dtVehicleType = dSet.Tables[0];
                            listVehicleType = (from a in dtVehicleType.AsEnumerable()
                                               select new VehicleType
                                               {
                                                   VehicleTypeID = GlobalCode.Field2Int(a["VehicleTypeID"]),
                                                   VehicleTypeName = GlobalCode.Field2String(a["VehicleTypeName"])
                                               }).ToList();
                        }
                        HttpContext.Current.Session["VehicleType"] = listVehicleType;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtVehicleType != null)
                {
                    dtVehicleType.Dispose();
                }
                if (dtVehicleTypeCapacity != null)
                {
                    dtVehicleTypeCapacity.Dispose();
                }
            }
        }
        /// ------------------------------------------
        /// Date Created:  04/Mar/2014
        /// Created By:    Josephine Gad
        /// (description)  Get Service Provider details, Airport and Brand
        /// ------------------------------------------
        /// </summary>        
        public static void GetPortAgentAirportBrand(int iPortAgentID, Int16 iLoadType)
        {
            List<VendorPortAgentDetails> listPortAgentDetails = new List<VendorPortAgentDetails>();
            List<AirportDTO> listAirportPortAgent = new List<AirportDTO>();
            
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand dbCom = null;
            DataSet dSet = null;
            DataTable dtPortAgent = null;
            DataTable dtAirport = null;

            HttpContext.Current.Session["PortAgentVendorDetails"] = listPortAgentDetails;
            HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;

            try
            {
                dbCom = db.GetStoredProcCommand("uspPortAgentVendorGetAirportBrand");
                db.AddInParameter(dbCom, "@pPortAgentVendorIDInt", DbType.Int64, iPortAgentID);
                db.AddInParameter(dbCom, "@pLoadType", DbType.Int16, iLoadType);
                dSet = db.ExecuteDataSet(dbCom);

                if (dSet.Tables[0] != null)
                {
                    dtPortAgent = dSet.Tables[0];
                    listPortAgentDetails = (from a in dtPortAgent.AsEnumerable()
                                            select new VendorPortAgentDetails
                                            {
                                                PortAgentID = GlobalCode.Field2Int(a["colPortAgentVendorIDInt"]),
                                                PortAgentName = GlobalCode.Field2String(a["colPortAgentVendorNameVarchar"]),

                                                CountryID = GlobalCode.Field2Int(a["colCountryIDInt"]),
                                                CountryName = GlobalCode.Field2String(a["CountryName"]),

                                                CityID = GlobalCode.Field2Int(a["colCityIDInt"]),
                                                CityName = GlobalCode.Field2String(a["CityName"]),

                                                ContactNo = GlobalCode.Field2String(a["colContactNoVarchar"]),
                                                FaxNo = GlobalCode.Field2String(a["colFaxNoVarchar"]),
                                                ContactPerson = GlobalCode.Field2String(a["colContactPersonVarchar"]),
                                                Address = GlobalCode.Field2String(a["colAddressVarchar"]),
                                                EmailTo = GlobalCode.Field2String(a["colEmailToVarchar"]),
                                                EmailCC = GlobalCode.Field2String(a["colEmailCcVarchar"]),
                                                Website = GlobalCode.Field2String(a["colWebsiteVarchar"]),
                                            }).ToList();
                }
               
                if (dSet.Tables[1] != null)
                {
                    dtAirport = dSet.Tables[1];
                    listAirportPortAgent = (from a in dtAirport.AsEnumerable()
                                            select new AirportDTO
                                            {
                                                AirportIDString = GlobalCode.Field2String(a["AirportID"]),
                                                AirportCodeString = GlobalCode.Field2String(a["AirportCode"]),
                                                AirportNameString = GlobalCode.Field2String(a["AirportName"])
                                            }).ToList();
                }
               
                HttpContext.Current.Session["PortAgentVendorDetails"] = listPortAgentDetails;
                HttpContext.Current.Session["PortAgentAirport"] = listAirportPortAgent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCom != null)
                {
                    dbCom.Dispose();
                }
                if (dSet != null)
                {
                    dSet.Dispose();
                }
                if (dtPortAgent != null)
                {
                    dtPortAgent.Dispose();
                }
                if (dtAirport != null)
                {
                    dtAirport.Dispose();
                }               
            }
        }
        /// ------------------------------------------
        /// Date Created:  16/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  get Luggage UOM
        /// ------------------------------------------
        /// </summary>        
        public List<LuggageUOM> GetLuggageUOM()
        {
            List<LuggageUOM> list = new List<LuggageUOM>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand com = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {               
                com = db.GetStoredProcCommand("uspGetUOMLuggage");
                ds = db.ExecuteDataSet(com);

                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                                  select new LuggageUOM
                                  {
                                      LuggageUOMId = GlobalCode.Field2Int(a["colUOMIdIint"]),
                                      LuggageUOMName = GlobalCode.Field2String(a["colUOMName"])
                                  }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
                if (ds!= null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }

            }
        }
        /// ------------------------------------------
        /// Date Created:  16/Apr/2014
        /// Created By:    Josephine Gad
        /// (description)  get Luggage UOM
        /// ------------------------------------------
        /// </summary>        
        public List<SafeguardUOM> GetSafeguardUOM()
        {
            List<SafeguardUOM> list = new List<SafeguardUOM>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand com = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {              
                com = db.GetStoredProcCommand("uspGetUOMSafeguard");
                ds = db.ExecuteDataSet(com);

                dt = ds.Tables[0];

                list = (from a in dt.AsEnumerable()
                        select new SafeguardUOM
                        {
                            SafeguardUOMId = GlobalCode.Field2Int(a["colUOMIdIint"]),
                            SafeguardUOMName = GlobalCode.Field2String(a["colUOMName"])
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }

            }
        }

        public List<ColorCodes> GetColor()
        {
            List<ColorCodes> list = new List<ColorCodes>();
            Database db = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand com = null;
            try
            {
                com = db.GetStoredProcCommand("uspGetColorName");
                DataTable dt = db.ExecuteDataSet(com).Tables[0];
                list = (from a in dt.AsEnumerable()
                        select new ColorCodes
                        {
                            ColorCode = GlobalCode.Field2String(a["ColorCode"]),
                            ColorName = GlobalCode.Field2String(a["ColorName"])
                        }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
            }
        }


    }
}

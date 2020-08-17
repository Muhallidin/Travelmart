using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace TRAVELMART.DAL
{
    public class IMSDAL
    {
        private List<InvoiceHeader> InvoiceHeader = new List<InvoiceHeader>();
        private List<Vendor> Vendor = new List<Vendor>();
        private IMSClassList IMSClass = new  IMSClassList ();

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        /// <summary>
        /// Created By:    Muha Wali
        /// Date Modified: Sept 2016
        /// (description)  
        /// ================================================
        /// Modified By:    Josephine Monteza
        /// Date Modified:  08/Sept/2016
        /// (description)   Change index to name of column when retrieving InvoiceException
        ///                 Add field DateCreated in the result
        /// ================================================
        /// </summary>        
        public List<InvoiceHeader > GetInvoice(string vendorNo, string from, string to, int StatusID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                InvoiceHeader = new List<InvoiceHeader>();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetInvoicesToBill");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, 0);
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorNum", DbType.String, vendorNo);
                SFDatebase.AddInParameter(SFDbCommand, "@pFromDate", DbType.DateTime, GlobalCode.Field2DateTime(from));
                SFDatebase.AddInParameter(SFDbCommand, "@pToDate", DbType.DateTime, GlobalCode.Field2DateTime(to));
                SFDatebase.AddInParameter(SFDbCommand, "@pStatusID", DbType.Int32, StatusID);

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);

                InvoiceHeader = (from n in ds.Tables[0].AsEnumerable()
                                 //where a["colVendorNumber"].ToString() == n["colVendorNumber"].ToString()
                                 select new InvoiceHeader
                                 {

                                     InvoiceNumID = n["colInvoiceNumIDInt"].ToString(),
                                     VendorNumID = n["colVendorNumber"].ToString(),
                                     InvoiceNumber = n["colInvoiceNumber"].ToString(),
                                     InvoiceDate = GlobalCode.Field2DateTime(n["colInvoiceDate"]),
                                     InvoiceTotal = n["colInvoiceTotal"].ToString(),
                                     PortNumber = n["colPortNumber"].ToString(),
                                     ShipNumber = n["colShipNumber"].ToString(),
                                     BusinessUnitCode = n["colBusinessUnitCode"].ToString(),
                                     CreatedByVarchar = n["colCreatedByVarchar"].ToString(),
                                     Port = n["PortName"].ToString(),
                                     Ship = n["VesselName"].ToString(),
                                     ExceptionID = GlobalCode.Field2TinyInt(n["ExceptionID"]),
                                     Exception = n["InvoiceStatus"].ToString(),
                                     Image = n["Image"].ToString(),
                                     InvoiceDetail = (from e in ds.Tables[1].AsEnumerable()
                                                      where n["colInvoiceNumIDInt"].ToString() == e["colInvoiceNumIDInt"].ToString()
                                                      select new InvoiceDetail
                                                      {

                                                          InvoiceNumID = e["colInvoiceNumIDInt"].ToString(),
                                                          InvoiceDetailID = e["colInvoiceDetailIDInt"].ToString(),
                                                          ExpenseTypeCode = e["colExpenseTypeCodeVarchar"].ToString(),
                                                          Quantity = e["colQuantity"].ToString(),
                                                          UnitCost = e["colUnitCost"].ToString(),
                                                          CurrencyCode = e["colCurrencyCode"].ToString(),
                                                          TotalCost = e["colTotalCost"].ToString(),
                                                          Comment = e["colComment"].ToString(),
                                                          EmployeeNumber = e["colEmployeeNumber"].ToString(),
                                                          CrewServiceStartDate = GlobalCode.Field2DateTime(e["colCrewServiceStartDate"]),
                                                          CrewServiceEndDate = GlobalCode.Field2DateTime(e["colCrewServiceEndDate"]),
                                                          UnitofMeasureType = e["colUnitofMeasureType"].ToString(),
                                                          TripNumber = e["colTripNumber"].ToString(),
                                                          CreatedByVarchar = e["colCreatedByVarchar"].ToString(),
                                                          CreatedDateTime = e["colCreatedDateTime"].ToString(),
                                                          InvoiceNumber = n["colInvoiceNumber"].ToString(),
                                                          Exclude = GlobalCode.Field2Bool(e["colIsExcludeBit"]),
                                                          Backcolor = e["BackColor"].ToString(),
                                                          IsExcetion = GlobalCode.Field2TinyInt(e["ExceptionID"])

                                                      }).ToList(),
                                     InvoiceException = (from i in ds.Tables[2].AsEnumerable()
                                                         where i["colInvoiceNumber"].ToString() == n["colInvoiceNumber"].ToString()
                                                         select new InvoiceException
                                                         {
                                                             InvoiceExceptionID = i["colInvoiceExceptionIDInt"].ToString(),
                                                             Success = i["Success"].ToString(),
                                                             Message = i["colMessageVarchar"].ToString(),
                                                             InvoiceNumber = i["colInvoiceNumber"].ToString(),
                                                             VendorNumber = i["colVendorNumber"].ToString(),
                                                             DateCreated = i["DateCreated"].ToString(),
                                                             InvoiceExceptionDetail = (from a in ds.Tables[3].AsEnumerable()
                                                                                       where i[0].ToString() == a[1].ToString()
                                                                                       select new InvoiceExceptionDetail
                                                                                       {
                                                                                           InvoiceExceptionID = n[0].ToString(),
                                                                                           Success = n[1].ToString(),
                                                                                           Message = n[2].ToString(),
                                                                                           InvoiceNumber = n[3].ToString(),
                                                                                           VendorNumber = n[4].ToString(),
                                                                                           ErrorMessage = a["colErrorMessageVarchar"].ToString(),
                                                                                           Type = a["colTypeVarchar"].ToString(),
                                                                                           Owner = a["colErrorTypeOwnerVarchar"].ToString(),

                                                                                       }).ToList()
                                                         }).ToList()


                                 }).ToList();

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
            return InvoiceHeader;
        }

        public IMSClassList GetIMSClassList(short loadType, int vendorNo)
        {

           Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
           DbCommand SFDbCommand = null;
           try
           {
               IMSClass = new  IMSClassList();

               SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetIMSVendor");
               SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, 0);
               SFDatebase.AddInParameter(SFDbCommand, "@pBranchID", DbType.Int32, vendorNo);

               ds = SFDatebase.ExecuteDataSet(SFDbCommand);
               IMSClass.Vendor = (from a in ds.Tables[0].AsEnumerable()
                         select new Vendor
                         {
                             VendorNumber = a["colVendorNumber"].ToString(),
                             VendorNumID = a["colVendorNumIDInt"].ToString(),
                             BranchID = GlobalCode.Field2Int(a["BranchID"].ToString()),
                             BranchName = a["Branch"].ToString(),
                             UserID = a["colCreatedByVarchar"].ToString(),
                             createdDate = a["colCreatedDateTime"].ToString(),
                         }).ToList();

               IMSClass.InvoiceException =  (from a in ds.Tables[1].AsEnumerable()
                                             select new InvoiceExceptionType
                                             {
                                               Exception = a["ExceptionType"].ToString(),
                                               ExceptionID = GlobalCode.Field2TinyInt(  a["ExceptionTypeID"]),

                                            }).ToList();

               IMSClass.Port  = (from a in ds.Tables[2].AsEnumerable()
                                            select new Port 
                                            {
                                                PortId = GlobalCode.Field2Int( a["PortId"]),
                                                PortName = a["PortName"].ToString(),
                                                IMSPortNumber = GlobalCode.Field2Int(a["IMSPortNumber"]),
                                            }).ToList();

               return IMSClass;

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
        /// Modified by:      Josephine Monteza
        /// Date Modified:    08/Sept/2016 
        /// Description:      Add Audit Trail parameters
        /// ======================================
        /// </summary>        
        public short GetExcludeInvoiceDetail(int InvoiceDetailID, string sUser, string strLogDescription, string strFunction, string strPageName)
        {

           Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
           DbCommand SFDbCommand = null;
           try
           {

               string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString(); 
               DateTime dateNow = CommonFunctions.GetCurrentDateTime();
               DateTime DateGMT = CommonFunctions.GetDateTimeGMT(dateNow);

               SFDbCommand = SFDatebase.GetStoredProcCommand("uspExcludeInvoiceDetail");
               SFDatebase.AddInParameter(SFDbCommand, "@InvoiceDetailID", DbType.Int32, InvoiceDetailID);
               SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, sUser);
               SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, strLogDescription);
               SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, strFunction);
               SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, strPageName);
               SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, strTimeZone);
               SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, DateGMT);

               return GlobalCode.Field2TinyInt(SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0].Rows[0][0].ToString());
                
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




        public List<InvoiceHeader> GetSingleInvoicesToBill(string VendorNum, string InvoiceDate, ref string Token, ref string validation)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            try
            {

                InvoiceHeader = new List<InvoiceHeader>();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetSingleInvoicesToBill");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, 0);
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceNumber", DbType.String, VendorNum);
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceDate", DbType.DateTime, GlobalCode.Field2DateTime(InvoiceDate));

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                InvoiceHeader = (from n in ds.Tables[0].AsEnumerable()
                                 select new InvoiceHeader
                                 {

                                     InvoiceNumID = n["colInvoiceNumIDInt"].ToString(),
                                     VendorNumber = n["colVendorNumber"].ToString(),
                                     InvoiceNumber = n["colInvoiceNumber"].ToString(),
                                     InvoiceDate = GlobalCode.Field2DateTime( n["colInvoiceDate"]),
                                     InvoiceTotal = n["colInvoiceTotal"].ToString(),
                                     PortNumber = n["colPortNumber"].ToString(),
                                     ShipNumber = n["colShipNumber"].ToString(),
                                     BusinessUnitCode = n["colBusinessUnitCode"].ToString(),
                                     CreatedByVarchar = n["colCreatedByVarchar"].ToString(),
                                     Port = n["PortName"].ToString(),
                                     Ship = n["VesselName"].ToString(),

                                     InvoiceDetail = (from e in ds.Tables[1].AsEnumerable()
                                                      where n["colVendorNumber"].ToString() == e["colVendorNumber"].ToString()
                                                          && n["colInvoiceNumIDInt"].ToString() == e["colInvoiceNumIDInt"].ToString()
                                                      select new InvoiceDetail
                                                      {

                                                          InvoiceNumID = e["colInvoiceDetailIDInt"].ToString(),
                                                          InvoiceDetailID = e["colInvoiceNumIDInt"].ToString(),
                                                          ExpenseTypeCode = e["colExpenseTypeCodeVarchar"].ToString(),
                                                          Quantity = e["colQuantity"].ToString(),
                                                          UnitCost = e["colUnitCost"].ToString(),
                                                          CurrencyCode = e["colCurrencyCode"].ToString(),
                                                          TotalCost = e["colTotalCost"].ToString(),
                                                          Comment = e["colComment"].ToString(),
                                                          EmployeeNumber = e["colEmployeeNumber"].ToString(),
                                                          //CrewServiceStartDate = String.Format("{0:yyyy-MM-dd}", e["colCrewServiceStartDate"]),//e["colCrewServiceStartDate"].ToString(),
                                                          //CrewServiceEndDate = String.Format("{0:yyyy-MM-dd}", e["colCrewServiceEndDate"]),// e["colCrewServiceEndDate"].ToString(),

                                                          CrewServiceStartDate =GlobalCode.Field2DateTime( e["colCrewServiceStartDate"]),
                                                          CrewServiceEndDate = GlobalCode.Field2DateTime( e["colCrewServiceEndDate"]),

                                                          UnitofMeasureType = e["colUnitofMeasureType"].ToString(),
                                                          TripNumber = e["colTripNumber"].ToString(),
                                                          CreatedByVarchar = e["colCreatedByVarchar"].ToString(),
                                                          CreatedDateTime = e["colCreatedDateTime"].ToString(),
                                                          
                                                      }).ToList()

                                 }).ToList();

               
                Token = ds.Tables[2].Rows[0]["colAccesTokenVarchar"].ToString();
                validation = ds.Tables[3].Rows[0]["InvoiceStatus"].ToString(); ;


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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return InvoiceHeader;
        }



        public string GetBillSingleInvoices(string VendorNum, string InvoiceDate, ref string Token)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;

            try
            {

                InvoiceHeader = new List<InvoiceHeader>();

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspGetBillSingleInvoices");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, 0);
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceNumber", DbType.String, VendorNum);
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceDate", DbType.DateTime, GlobalCode.Field2DateTime(InvoiceDate));

                ds = SFDatebase.ExecuteDataSet(SFDbCommand);
                 



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
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

            return "";
        }

        /// ======================================
        /// Author:       Muha Wali
        /// Date Created: Sept 2016
        /// Description:  
        /// ======================================
        /// Modified by:      Josephine Monteza
        /// Date Modified:    08/Sept/2016 
        /// Description:      Close DataTable for optimization
        /// ======================================
        public void ErrorInvoice(string invoice, DataTable exception, DataTable execeptionDetail, string user)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {

                SFDbCommand = SFDatebase.GetStoredProcCommand("uspInsertException");
                SFDatebase.AddInParameter(SFDbCommand, "@InvoiceNumber", DbType.String, invoice);
                SFDatebase.AddInParameter(SFDbCommand, "@userID", DbType.String, user);

                SqlParameter param = new SqlParameter("@pInvoiceException", exception);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SFDbCommand.Parameters.Add(param);

                SqlParameter param1 = new SqlParameter("@pInvoiceExceptionDetail", execeptionDetail);
                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.Structured;
                SFDbCommand.Parameters.Add(param1);

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
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (exception != null)
                {
                    exception.Dispose();
                }
                if (execeptionDetail != null)
                {
                    execeptionDetail.Dispose();
                }
            }
        }

        /// <summary>
        /// Modified By:    Josephine Monteza
        /// Date Modified:  08/Sep/2016
        /// Descrption:     Add parameter for Audit Trail
        /// -----------------------------------------------------------------   
        /// </summary>
        public short GetSubmittedInvoice(short LoadType, int VendorNumber, string InvoiceNumber, string sPath, string sUser
                ,string Description,string Function,string FileName,string Timezone,DateTime GMTDATE)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspSubmittedInvoice");
                SFDatebase.AddInParameter(SFDbCommand, "@pLoadType", DbType.Int16, LoadType);
                SFDatebase.AddInParameter(SFDbCommand, "@pVendorNumber", DbType.Int32, VendorNumber);
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceNumber", DbType.String, InvoiceNumber);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserIDVarchar", DbType.String, sUser);


                SFDatebase.AddInParameter(SFDbCommand, "@pDescription", DbType.String, Description);
                SFDatebase.AddInParameter(SFDbCommand, "@pFunction", DbType.String, Function);
                SFDatebase.AddInParameter(SFDbCommand, "@pFileName", DbType.String, FileName);
                SFDatebase.AddInParameter(SFDbCommand, "@pTimezone", DbType.String, Timezone);
                SFDatebase.AddInParameter(SFDbCommand, "@pGMTDATE", DbType.DateTime, GMTDATE);		




                return GlobalCode.Field2TinyInt(SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0].Rows[0][0].ToString());
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

        public short GetUpdateInvoiceHotelPort(string InvoiceNumber, string PortNumber, string UserID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateInvoiceHotelPort");
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceNumber", DbType.String, InvoiceNumber);
                SFDatebase.AddInParameter(SFDbCommand, "@pPortNumber", DbType.String, PortNumber);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                return GlobalCode.Field2TinyInt(SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0].Rows[0][0].ToString());
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

        public short GetUpdateInvoiceDetail(int UpdateID, string InvoiceDetailID, string UpdateValue, string UserID)
        {
            Database SFDatebase = ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase()
            DbCommand SFDbCommand = null;
            try
            {
                SFDbCommand = SFDatebase.GetStoredProcCommand("uspUpdateInvoiceDetail");
                SFDatebase.AddInParameter(SFDbCommand, "@pUpdateID", DbType.Int32, UpdateID);
                SFDatebase.AddInParameter(SFDbCommand, "@pInvoiceDetailID", DbType.Int32, InvoiceDetailID);
                SFDatebase.AddInParameter(SFDbCommand, "@pUpdateValue", DbType.String, UpdateValue);
                SFDatebase.AddInParameter(SFDbCommand, "@pUserID", DbType.String, UserID);
                return GlobalCode.Field2TinyInt(SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0].Rows[0][0].ToString());
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
    }
}

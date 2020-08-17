using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.Common;
using TRAVELMART.DAL;
using System.Data;

namespace TRAVELMART.BLL
{
    public class IMSBLL
    {
        public List<InvoiceHeader> GetInvoice(string vendorNo, string from, string to,int StatusID)
        {
            try
            {
                IMSDAL DAL = new IMSDAL();
                return DAL.GetInvoice(vendorNo, from, to, StatusID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IMSClassList GetIMSClassList(short loadType, Int32 vendorNo)
        {
            try
            {
                IMSDAL DAL = new IMSDAL();
                return DAL.GetIMSClassList(loadType, vendorNo);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public short GetExcludeInvoiceDetail(int InvoiceDetailID, string sUser, string strLogDescription, string strFunction, string sPath)
        {
            try
            {
                IMSDAL DAL = new IMSDAL();
                return DAL.GetExcludeInvoiceDetail(InvoiceDetailID, sUser, strLogDescription, strFunction, sPath);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  Invoices  GetSingleInvoicesToBill(string VendorNum, string InvoiceDate)
        {
            try
            {
                string Token = "";
                string InvoiceStatus = "";
                
                Invoices invoice = new Invoices();


                IMS IMS = new IMS();
                IMSDAL DAL = new IMSDAL();
                List<InvoiceHeader> InvoiceHeader = DAL.GetSingleInvoicesToBill(VendorNum, InvoiceDate, ref Token, ref InvoiceStatus);

                string XMLInvoice = IMS.CreateInvoicesSingle(InvoiceHeader);
                invoice.Invoice = XMLInvoice;
                invoice.Token = Token;
                invoice.InvoiceStatus = InvoiceStatus;
                return invoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Invoices GetBillSingleInvoices(string VendorNum, string InvoiceDate)
        {
            try
            {
                string Token = "";
                string InvoiceStatus = "";
                Invoices invoice = new Invoices();

                IMS IMS = new IMS();
                IMSDAL DAL = new IMSDAL();
                List<InvoiceHeader> InvoiceHeader = DAL.GetSingleInvoicesToBill(VendorNum, InvoiceDate, ref Token, ref InvoiceStatus);

                string XMLInvoice = IMS.CreateInvoicesSingle(InvoiceHeader);
                invoice.Invoice = XMLInvoice;
                invoice.Token = Token;
                invoice.InvoiceStatus = InvoiceStatus;

                return invoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Modified by:      Josephine Monteza
        /// Date Modified:    08/Sept/2016 
        /// Description:      Close DataTable for optimization
        /// ======================================
        public void ErrorInvoice(string invoice, DataTable exception, DataTable execeptionDetail, string user)
        {
            try
            {
                IMSDAL DAL = new IMSDAL();
                DAL.ErrorInvoice(invoice, exception, execeptionDetail, user);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
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

        public short GetSubmittedInvoice(short LoadType, int VendorNumber, string InvoiceNumber, string sPath, string sUser
                , string Description, string Function, string FileName, string Timezone, DateTime GMTDATE)
        {
            try
            {
                IMSDAL DAL = new IMSDAL();
                return DAL.GetSubmittedInvoice(LoadType, VendorNumber, InvoiceNumber, sPath, sUser, Description, Function ,FileName, Timezone , GMTDATE);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public short GetUpdateInvoiceHotelPort(string InvoiceNumber, string PortNumber, string UserID)
        {
            try
            {
                IMSDAL DAL = new IMSDAL();
                return DAL.GetUpdateInvoiceHotelPort(InvoiceNumber, PortNumber, UserID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public short GetUpdateInvoiceDetail(int UpdateID, string InvoiceDetailID, string UpdateValue, string UserID)
         {
             try
             {
                 IMSDAL DAL = new IMSDAL();
                 return DAL.GetUpdateInvoiceDetail(UpdateID, InvoiceDetailID, UpdateValue, UserID);

             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
    }
}

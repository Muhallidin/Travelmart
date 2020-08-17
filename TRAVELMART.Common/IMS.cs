using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace TRAVELMART.Common
{


    public class IMS
    {
        public string CreateInvoicesSingle(List<InvoiceHeader> InvoiceHeader)
        {


            string result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            if (InvoiceHeader.Count > 0)
            {

                for (var h = 0; h < InvoiceHeader.Count; h++)
                {

                    var sum = InvoiceHeader[h].InvoiceDetail.Sum(e => GlobalCode.Field2Double(e.TotalCost));

                    result += "<Invoice>";
                    result += "<InvoiceHeader>";
                    result += "<InvoiceNumber>" + InvoiceHeader[h].InvoiceNumber + "</InvoiceNumber>";
                    result += "<InvoiceDate>" + InvoiceHeader[h].InvoiceDate + "</InvoiceDate>";
                    result += "<InvoiceTotal>" + string.Format("{0:0.0000}", sum) + "</InvoiceTotal>";
                    result += "<PortNumber>" + InvoiceHeader[h].PortNumber + "</PortNumber>";
                    result += "<ShipNumber>" + InvoiceHeader[h].ShipNumber + "</ShipNumber>";
                    result += "<BusinessUnitCode>" + InvoiceHeader[h].BusinessUnitCode + "</BusinessUnitCode>";
                    result += "<VendorNumber>" + InvoiceHeader[0].VendorNumber + "</VendorNumber>";
                    result += "</InvoiceHeader>";
                    if (InvoiceHeader[h].InvoiceDetail.Count > 0)
                    {

                        var detail = InvoiceHeader[h].InvoiceDetail;
                        result += "<InvoiceDetails>";

                        for (var d = 0; d < detail.Count; d++)
                        {

                            result += "<InvoiceDetail>";
                            result += "<ExpenseTypeCode>" + detail[d].ExpenseTypeCode + "</ExpenseTypeCode>";
                            result += "<Quantity>" + detail[d].Quantity + "</Quantity>";
                            result += "<UnitCost>" + string.Format("{0:0.0000}", detail[d].UnitCost) + "</UnitCost>";
                            result += "<CurrencyCode>" + detail[d].CurrencyCode + "</CurrencyCode>";
                            result += "<TotalCost>" + string.Format("{0:0.0000}", detail[d].TotalCost) + "</TotalCost>";
                            result += "<Comment>" + detail[d].Comment + "</Comment>";
                            result += "<EmployeeNumber>" + detail[d].EmployeeNumber + "</EmployeeNumber>";
                            result += "<CrewServiceStartDate>" + detail[d].CrewServiceStartDate + "</CrewServiceStartDate>";
                            result += "<CrewServiceEndDate>" + detail[d].CrewServiceStartDate + "</CrewServiceEndDate>";
                            result += "<UnitOfMeasureType>" + detail[d].UnitofMeasureType + "</UnitOfMeasureType>";
                            result += "<TripNumber>" + detail[d].TripNumber + "</TripNumber>";
                            result += "</InvoiceDetail>";

                        }
                        result += "</InvoiceDetails>";
                    }
                    result += "</Invoice>";
                }
            }

            return result;
        }


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/11/2013
        /// Description:  convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                //Type t = GetCoreType(prop.PropertyType);
                //tb.Columns.Add(prop.Name, t);
                tb.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }


        /// <summary>
        /// Author:       Muhallidin G Wali
        /// Date Created: 04/11/2013
        /// Description:  convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public DataTable ExceptionDataTable(JsonInvoiceException items)
        {

            // Here we create a DataTable with four columns.
            DataTable table = new DataTable("InvoiceException");
            table.Columns.Add("Success", typeof(string));
            table.Columns.Add("Message", typeof(string));
            table.Columns.Add("InvoiceNumber", typeof(string));
            table.Columns.Add("VendorNumber", typeof(string));

            // Here we add five DataRows.
            table.Rows.Add(items.Success, items.Message, items.InvoiceNumber, items.VendorNumber);

            return table;

        }
    }


     
    public class Vendor
    {

        public string VendorNumID { get; set; }
        public string VendorNumber { get; set; }
        public int? BranchID { get; set; }
        public string BranchName { get; set; }
        public string UserID { get; set; }
        public string createdDate { get; set; }
        public List<InvoiceHeader> InvoiceHeader { get; set; }

    }


    public class InvoiceHeader : InvoiceExceptionType
    {

        public string InvoiceNumID { get; set; }
        public string VendorNumID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceTotal { get; set; }
        public string PortNumber { get; set; }
        public string Port { get; set; }
        public string ShipNumber { get; set; }
        public string Ship { get; set; }
        public string BusinessUnitCode { get; set; }
        public string CreatedByVarchar { get; set; }
        public string CreatedDateTime { get; set; }
       
        public string VendorNumber { get; set; }
           
        public string Image { get; set; }
        public List<InvoiceDetail> InvoiceDetail { get; set; }
        public List<InvoiceException> InvoiceException { get; set; }

    }

    public class InvoiceDetail
    {

        public string InvoiceDetailID { get; set; }
        public string ExpenseTypeCode { get; set; }
        public string InvoiceNumID { get; set; }
        public string InvoiceNumber { get; set; }

        public string Quantity { get; set; }
        public string UnitCost { get; set; }
        public string CurrencyCode { get; set; }
        public string TotalCost { get; set; }
        public string Comment { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime CrewServiceStartDate { get; set; }
        public DateTime CrewServiceEndDate { get; set; }
        public string UnitofMeasureType { get; set; }
        public string TripNumber { get; set; }
        public string CreatedByVarchar { get; set; }
        public string CreatedDateTime { get; set; }
        public bool Exclude { get; set; }
        public string  Backcolor { get; set; }
        public short IsExcetion { get; set; }


    }

    /// <summary>
    /// Created By:    Muha Wali
    /// Date Modified: Sept 2016
    /// (description)  
    /// ================================================
    /// Modified By:    Josephine Monteza
    /// Date Modified:  08/Sept/2016
    /// (description)   Add DateCreated column
    /// ================================================
    /// </summary>
    public class InvoiceException
    {
        
        public string InvoiceExceptionID { get; set; }
        public string Success { get; set; }
        public string Message { get; set; }
        public string InvoiceNumber { get; set; }
        public string VendorNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated { get; set; }

        public List<InvoiceExceptionDetail> InvoiceExceptionDetail { get; set; }

    }

    public class InvoiceExceptionDetail : InvoiceException
    {

        public string ErrorMessage { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
    }

    public class JsonInvoiceException
    {

        public string Success { get; set; }
        public string Message { get; set; }
        public string InvoiceNumber { get; set; }
        public string VendorNumber { get; set; }
        public List<JsonInvoiceExceptionDetail> Errors { get; set; }

    }
    
    public class JsonInvoiceExceptionDetail
    {  

        public string ErrorMessage { get; set; }
        public string Type { get; set; }
        
    }

    public class Invoices 
    {
        public string Invoice { get; set; }
        public string Token { get; set; }
        public string InvoiceStatus { get; set; }
    }
     
    public class InvoiceExceptionType  
    {
        public short ExceptionID { get; set; }
        public string Exception { get; set; }
    }

    public class IMSClassList {
        public List<InvoiceExceptionType> InvoiceException { get; set; }
        public List<Vendor> Vendor { get; set; }
        public List<Port> Port { get; set; }
    }

    public class Port
    {
        public int PortId { get; set; }
        public int IMSPortNumber { get; set; }
        public string PortName { get; set; }
    }



}

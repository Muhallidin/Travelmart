using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using System.Reflection;
using System.IO;


namespace TRAVELMART.IMS
{
    public partial class IMS : System.Web.UI.Page
    {



        private AsyncTaskDelegate _IMSDeligate;

        // Create delegate. 
        protected delegate void AsyncTaskDelegate();

        IMSBLL BLL = new IMSBLL();
        List<InvoiceHeader> InvoiceHeader = new List<InvoiceHeader>();

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                
                GlobalCode gc = new GlobalCode();
                string userID = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldUser.Value = userID;
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                uoHiddenFieldBranchID.Value = GlobalCode.Field2String(Session["UserBranchID"]);
                     
                txtFromDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
                txtToDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");

            }

            if (Session["CurrentDate"] != null && Session["CurrentDate"].ToString() == "")
            {
                Session["CurrentDate"] = DateTime.Now.Date;
            }

            //ListView1.DataSource = null;
            //ListView1.DataBind();
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                List<InvoiceHeader> InvoiceHeader = new List<InvoiceHeader>();
                ListView1.DataSource = null;
                ListView1.DataBind();

                PageAsyncTask TaskIMS = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskIMS);
            }


        }

        
        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _IMSDeligate = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _IMSDeligate.BeginInvoke(cb, extraData);
            return result;
        }

        public void OnEndExceptions(IAsyncResult ar)
        {
            _IMSDeligate.EndInvoke(ar);
            GetVendor();
        }


        

        void GetVendor() {
            try
            {

                     //VendorNumber { get; set; }
                     //BranchID { get; set; }
                     // BranchName { get; set; }

                IMSClassList IMSClass = new IMSClassList();
                IMSClass = BLL.GetIMSClassList(0, 0);

                lstVendor.DataSource = null;
                lstVendor.Items.Clear();

                lstVendor.DataSource = IMSClass.Vendor;
                lstVendor.DataTextField = "BranchName";
                lstVendor.DataValueField = "BranchID";
                lstVendor.DataBind();

                if (uoHiddenFieldRole.Value != "Hotel Vendor")
                {

                    lstVendor.Rows = lstVendor.Items.Count;
                    lstVendor.SelectedIndex = 0;
                }
                else {


                    lstVendor.Rows = 1;
                    lstVendor.SelectedIndex = GlobalCode.GetselectedIndex(lstVendor, uoHiddenFieldBranchID.Value);

                    lblIMSTitle.Text = "IMS - " + lstVendor.SelectedItem.Text;  

                }
              

                //uoHiddenFieldBranchID.Value


                cboStatus.DataSource = null;
                cboStatus.Items.Clear();

                cboStatus.DataSource = IMSClass.InvoiceException;
                cboStatus.DataTextField = "Exception";
                cboStatus.DataValueField = "ExceptionID";
                cboStatus.DataBind();

                lstSeaport.DataSource = null;
                lstSeaport.Items.Clear();

                lstSeaport.DataSource = IMSClass.Port;
                lstSeaport.DataTextField = "PortName";
                lstSeaport.DataValueField = "IMSPortNumber";
                lstSeaport.DataBind();
                lstSeaport.Rows = 10;
 
                


            }
            catch { 
            
            
            }


        }


        protected void cbostatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnHidden_click(null, null);
            }
            catch {
                throw; 
            }
        }

        protected void btnHidden_click(object sender, EventArgs e)
        {
            try
            {
                uoHiddenFieldPath.Value = Path.GetFileName(Request.UrlReferrer.AbsolutePath);
               
                
                string Vendor = lstVendor.SelectedValue.ToString(); //((ListBox)sender).SelectedValue.ToString();

                string from = txtFromDate.Text;
                string to = txtToDate.Text;


                int StatusID = GlobalCode.Field2Int(cboStatus.SelectedItem.Value); 


               InvoiceHeader = new List<InvoiceHeader>();

               InvoiceHeader = BLL.GetInvoice(Vendor, from, to, StatusID);

               if (InvoiceHeader.Count > 0)
               {
                   ListView1.DataSource = InvoiceHeader;
                   ListView1.DataBind();
               }
               else
               {
                   ListView1.DataSource = null;
                   ListView1.DataBind();
               }
              
                 
            }
            catch(Exception ex) {
                throw ex;
            }
           
        }




        //==============================
        // Web Method region
        //==============================

        ///// <summary>
        ///// Author:         Muhallidin G Wali
        ///// Date Created:   30/Aug/2016
        ///// Descrption:     Exclude Invoice
        ///// -----------------------------------------------------------------            
        ///// <returns></returns>
        //[WebMethod]
        //public static short ExcludeInvoiceDetail(int InvoiceDetailID)
        //{
        //    IMSBLL IMS = new IMSBLL();
        //    try
        //    {
        //        return IMS.GetExcludeInvoiceDetail(InvoiceDetailID);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //}


        ///// <summary>
        ///// Author:         Muhallidin G Wali
        ///// Date Created:   30/Aug/2016
        ///// Descrption:     Exclude Invoice
        ///// -----------------------------------------------------------------            
        ///// <returns></returns>
        //[WebMethod]
        //public static Invoices GetInvoices(string InvNumber, string InvDate)
        //{
        //    IMSBLL IMS = new IMSBLL();
        //    Invoices invoice = new Invoices();
        //    try
        //    {
        //        return IMS.GetSingleInvoicesToBill(InvNumber, InvDate);
        //    }
        //    catch
        //    {
        //        return invoice;
        //    }

        //}



        ///// <summary>
        ///// Author:         Muhallidin G Wali
        ///// Date Created:   30/Aug/2016
        ///// Descrption:     Exclude Invoice
        ///// -----------------------------------------------------------------            
        ///// <returns></returns>
        //[WebMethod]
        //public static Invoices GetBillSingleInvoices(string InvNumber, string InvDate)
        //{
        //    IMSBLL IMS = new IMSBLL();
        //    Invoices invoice = new Invoices();
        //    try
        //    {
        //        return IMS.GetSingleInvoicesToBill(InvNumber, InvDate);
        //    }
        //    catch
        //    {
        //        return invoice;
        //    }

        //}



        ///// <summary>
        ///// Author:         Muhallidin G Wali
        ///// Date Created:   30/Aug/2016
        ///// Descrption:     Exclude Invoice
        ///// -----------------------------------------------------------------            
        ///// <returns></returns>
        //[WebMethod]
        //public static string ErrorInvoices(string Content, string InvoiceNumber, string userID)
        //{
        //    try
        //    {
        //        JavaScriptSerializer jc = new JavaScriptSerializer();
        //        DataTable exception = new DataTable();
        //        DataTable execeptionDetail = new DataTable();
        //        JsonInvoiceException JsonInvoiceException = new JsonInvoiceException();

        //        JsonInvoiceException = jc.Deserialize<JsonInvoiceException>(Content);

        //        if (JsonInvoiceException.InvoiceNumber == "" || JsonInvoiceException.InvoiceNumber == null)
        //        {
        //            JsonInvoiceException.InvoiceNumber = InvoiceNumber;
        //        }

        //        Common.IMS ims = new Common.IMS();
        //        IMSBLL BLL = new IMSBLL();

        //        execeptionDetail = ims.getDataTable(JsonInvoiceException.Errors);
        //        exception = ims.ExceptionDataTable(JsonInvoiceException);


        //        BLL.ErrorInvoice(InvoiceNumber, exception, execeptionDetail, userID);
        //        return "Exception Inserted";

        //    }
        //    catch {
        //        return "Exception Not Inserted";
        //    }
            
        //}


        ///// <summary>
        ///// Author:         Muhallidin G Wali
        ///// Date Created:   30/Aug/2016
        ///// Descrption:     Exclude Invoice
        ///// -----------------------------------------------------------------            
        ///// <returns></returns>
        //[WebMethod]
        //public static int SaveSummittedInvoice(string VendorNumber, string  InvoiceNumber)
        //{
        //    IMSBLL IMS = new IMSBLL();
        //    Invoices invoice = new Invoices();
        //    try
        //    {
        //        return IMS.GetSubmittedInvoice(0, GlobalCode.Field2Int(VendorNumber), InvoiceNumber);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //}




         
    }
}

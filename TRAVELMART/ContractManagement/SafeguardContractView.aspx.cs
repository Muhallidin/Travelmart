using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Reflection;


namespace TRAVELMART.ContractManagement
{
    public partial class SafeguardContractView : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //SFStatus.Visible = false;
            if (Request.QueryString["cId"] == null && Request.QueryString["bId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["cId"] != "undefined")
                    {
                        uoHiddenFieldContractId.Value = Request.QueryString["cId"];
                    }
                    else
                    {
                        uoHiddenFieldContractId.Value = "0";
                    }

                    if (Request.QueryString["bId"] != null)
                    {
                        uoHiddenFieldBranchId.Value = Request.QueryString["bId"];
                    }
                    VehicleContractViewLogAuditTrail();
                    vendorSafeguardGetContractDetail();
                    ViewAttachment();
                }
            }
        }

        protected void uoGridViewVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            uoGridViewVehicle.PageIndex = e.NewPageIndex;
            vendorSafeguardGetContractDetail();
        }

        /// Date Modified:  01/08/2012
        /// Modified By:    Jefferson Bermundo
        /// description:    get Attachment List
        /// </summary>
        /// <param name="cId">Contract Id</param>
        static List<ContractSafeguardAttachment> _contractSafeguardAttachments = new List<ContractSafeguardAttachment>(); 

        private void ViewAttachment()
        {
            _contractSafeguardAttachments = SafeguardBLL.GetSafeguardContractAttachment(Convert.ToInt32(uoHiddenFieldContractId.Value));
            uoGridViewHotelContractAttachment.DataSource = _contractSafeguardAttachments;
            uoGridViewHotelContractAttachment.DataBind();

        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor vehicle contract detail     
        /// </summary>
        public void vendorSafeguardGetContractDetail()
        {
            DataTable dt = null;
            DataTable dtLV = null;
            DataTable dtSR = null;

            try
            {
                if (uoHiddenFieldContractId.Value == "")
                {
                    uoHiddenFieldContractId.Value = "0";
                }
                SafeguardBLL.GetVendorSafeguardContractByContractID(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value, 0);
                List<ContractSafeguardDetails> listDetails = new List<ContractSafeguardDetails>();
                listDetails = (List<ContractSafeguardDetails>)Session["SafeguardVendorDetails"];

                dt = getDataTable(listDetails);

                if (dt.Rows.Count > 0)
                {
                    ucLabelContractStatus.Text = dt.Rows[0]["ContractStatus"].ToString();
                    //uoTextBoxVendorCode.Text = dt.Rows[0]["BranchID"].ToString();
                    uotextboxRCCLRep.Text = dt.Rows[0]["RCCLPersonnel"].ToString();
                    uotextboxVendorRep.Text = dt.Rows[0]["VendorPersonnel"].ToString();
                    uoTextBoxTelNo.Text = dt.Rows[0]["ContactNo"].ToString();
                    uoTextBoxEmailTo.Text = dt.Rows[0]["EmailTo"].ToString();
                    uoTextBoxEmailCc.Text = dt.Rows[0]["EmailCc"].ToString();
                    uoTextBoxContractTitle.Text = dt.Rows[0]["ContractName"].ToString();

                    string strContractStartDate = (dt.Rows[0]["ContractDateStart"].ToString().Length > 0)
                        ? String.Format("{0:MM/dd/yyyy HH:mm}", Convert.ToDateTime(dt.Rows[0]["ContractDateStart"].ToString()))
                        : "";
                    string strContractEndDate = (dt.Rows[0]["ContractDateEnd"].ToString().Length > 0)
                        ? String.Format("{0:MM/dd/yyyy HH:mm}", Convert.ToDateTime(dt.Rows[0]["ContractDateEnd"].ToString()))
                        : "";
                    uoTextBoxContractStartDate.Text = strContractStartDate;
                    uoTextBoxContractEndDate.Text = strContractEndDate;

                    uoTextBoxRemarks.Text = dt.Rows[0]["Remarks"].ToString();

                    List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
                    list = GetContractSafeguardDetailsList();

                    uoGridViewVehicle.DataSource = getDataTable(list); ;
                    uoGridViewVehicle.DataBind();

                }
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
                if (dtLV != null)
                {
                    dtLV.Dispose();
                }
                if (dtSR != null)
                {
                    dtSR.Dispose();
                }
            }
        }

        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
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
        /// Author:         Josephine Gad
        /// Date Created:   15/Aug/2013
        /// Description:    get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   
        /// </summary> 
        private List<ContractServiceDetailsAmt> GetContractSafeguardDetailsList()
        {
            List<ContractServiceDetailsAmt> list = new List<ContractServiceDetailsAmt>();
            if (Session["ContractServiceDetailsAmt"] != null)
            {
                list = (List<ContractServiceDetailsAmt>)Session["ContractServiceDetailsAmt"];
            }
            return list;
        }


        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void VehicleContractViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Safeguard contract viewed.";
            strFunction = "SafeguardContractViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, GlobalCode.Field2String(Session["VehiclePath"]),
                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        /// -------------------------------------------
        /// Date Modified:  16/06/2012
        /// Modified By:    Josephine Gad
        /// (description)   Use try and catch 
        /// </summary>        
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            LinkButton obj = (LinkButton)sender;
            int attachmentId = Convert.ToInt32(obj.CommandArgument);
            byte[] bytes = (byte[])_contractSafeguardAttachments.AsEnumerable().Where(
                    data => data.AttachmentId == attachmentId).Select(data => data.uploadedFile).FirstOrDefault();

            string fPath = System.IO.Path.GetTempPath();
            string fName = fPath + System.IO.Path.GetTempFileName();
            Response.Buffer = true;
            Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if ((Request.Browser.Browser.ToLower() == "ie") && (Request.Browser.MajorVersion < 9))
            {
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.Cache.SetMaxAge(TimeSpan.FromMilliseconds(1));
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);//IE set to not cache
                Response.Cache.SetNoStore();//Firefox/Chrome not to cache
                Response.Cache.SetExpires(DateTime.UtcNow); //for safe measure expire it immediately
            }


            Response.ContentType = _contractSafeguardAttachments.AsEnumerable().Where(
                    data => data.AttachmentId == attachmentId).Select(data => data.FileType).FirstOrDefault().Trim(); ;
            Response.AddHeader("content-length", bytes.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=" + obj.Text);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        #endregion
    }
}
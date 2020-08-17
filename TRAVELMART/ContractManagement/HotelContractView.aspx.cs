using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class HotelContractView : System.Web.UI.Page
    {
        static List<ContractHotelAttachment> _contractHotelAttachments = new List<ContractHotelAttachment>(); // add for contract attachment -JEFF

        #region Events
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// -------------------------------------------
        /// Date Modified:  07/05/2013
        /// Modified By:    Marco Abejar
        /// (description)   Set overflow and exception visibility for crew assist (jquery)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["cId"] == null && Request.QueryString["vId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                //SFStatus.Visible = false;
                if (!IsPostBack)
                {
                    if (Request.QueryString["cId"] != "undefined")
                    {
                        uoHiddenFieldContractID.Value = Request.QueryString["cId"];
                    }
                    else
                    {
                        uoHiddenFieldContractID.Value = "0";
                    }

                    if (Request.QueryString["vId"] != null)
                    {
                        uoHiddenFieldVendorID.Value = Request.QueryString["vId"];
                    }
                    HotelContractViewLogAuditTrail();
                    GetVendorHotelContract();
                    ViewAttachment();
                    uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                }

            }
        }

        #endregion

        #region Functions
        /// <summary>
        /// Date Modified:  17/04/2012
        /// Modified By:    Gabriel Oquialda
        /// (description)   Validation if contract has attachment
        /// -------------------------------------------------------
        /// Date Modified:  01/08/2012
        /// Modified By:    Jefferson Bermundo
        /// description:    get Attachment List
        /// </summary>
        /// <param name="cId">Contract Id</param>
        private void ViewAttachment()
        {
            //    IDataReader dr = BLL.ContractBLL.GetContractAttachment(uoHiddenFieldContractID.Value, uoHiddenFieldVendorID.Value);
            //    if (!dr.Read())
            //    {
            //LinkButton1.Text = "(No attachment.)";
            //LinkButton1.Enabled = false;
            _contractHotelAttachments = GetHotelAttachments(Convert.ToInt32(uoHiddenFieldContractID.Value));
            uoGridViewHotelContractAttachment.DataSource = _contractHotelAttachments;
            uoGridViewHotelContractAttachment.DataBind();
            //}
        }

        /// <summary>
        /// Date Modified:   13/09/2011
        /// Modified By:     Josephine Gad
        /// (description)    Use vendor id or contract id to view contract details        
        /// </summary>
        /// <param name="cId">Contract Id</param>
        private void GetVendorHotelContract()
        {
            DataTable dt = null;
            try
            {
                if (uoHiddenFieldContractID.Value == "")
                {
                    uoHiddenFieldContractID.Value = "0";
                }
                dt = BLL.ContractBLL.GetVendorHotelContractByContractID(uoHiddenFieldContractID.Value, uoHiddenFieldVendorID.Value); // select contract by contract ID
                //}
                //else
                //{
                //dt = BLL.ContractBLL.GetVendorHotelBranchContractActiveByBranchID(uoHiddenFieldVendorID.Value);
                //}
                if (dt.Rows.Count > 0)
                {
                    ucLabelContractStatus.Text = dt.Rows[0]["colContractStatusVarchar"].ToString();

                    uoTextBoxVendorCode.Text = dt.Rows[0]["colVendorCodeVarchar"].ToString();
                    uoTextBoxVendorName.Text = dt.Rows[0]["colVendorBranchNameVarchar"].ToString();
                    uotextboxContractTitle.Text = dt.Rows[0]["colContractNameVarchar"].ToString();
                    uotextboxRemarks.Text = dt.Rows[0]["colRemarksVarchar"].ToString();
                    uotextboxRCCLRep.Text = dt.Rows[0]["colRCCLPersonnel"].ToString();
                    uotextboxVendorRep.Text = dt.Rows[0]["colVendorPersonnel"].ToString();
                    uoTextBoxTelNo.Text = dt.Rows[0]["colContactNoVarchar"].ToString();
                    uoTextBoxEmailTo.Text = dt.Rows[0]["colEmailVarchar"].ToString();
                    uoTextBoxEmailCc.Text = dt.Rows[0]["colEmailCcVarchar"].ToString();
                    uoCheckBoxShuttle.Checked = Convert.ToBoolean(dt.Rows[0]["colWithShuttleBit"].ToString());
                    uoCheckBoxBreakfast.Checked = Convert.ToBoolean(dt.Rows[0]["colBreakfastBit"].ToString());
                    uoCheckBoxLunch.Checked = Convert.ToBoolean(dt.Rows[0]["colLunchBit"].ToString());
                    uoCheckBoxDinner.Checked = Convert.ToBoolean(dt.Rows[0]["colDinnertBit"].ToString());
                    uoCheckBoxLunchDinner.Checked = Convert.ToBoolean(dt.Rows[0]["colLunchOrDinnerBit"].ToString());
                    uoTextBoxFaxNumber.Text = dt.Rows[0]["colFaxNoVarchar"].ToString();
                    
                    uoTextBoxWebsite.Text = dt.Rows[0]["colWebsiteVarchar"].ToString();
                    string dtStart = (dt.Rows[0]["colContractDateStartedDate"].ToString().Length > 0)
                        ? dt.Rows[0]["colContractDateStartedDate"].ToString()
                        : "";
                    string dtEnd = (dt.Rows[0]["colContractDateEndDate"].ToString().Length > 0)
                       ? dt.Rows[0]["colContractDateEndDate"].ToString()
                       : "";
                    uotextboxStartDate.Text = dtStart;
                    uotextboxEndDate.Text = dtEnd;
                    uoTextBoxMeal.Text = dt.Rows[0]["colMealRateMoney"].ToString();
                    uoTextBoxMealTax.Text = dt.Rows[0]["colMealRateTaxDecimal"].ToString();
                    uoCheckBoxMealTaxInclusive.Checked = Convert.ToBoolean(dt.Rows[0]["colMealRateTaxInclusiveBit"].ToString());

                    uotextboxVendorRepEmailAdd.Text = dt.Rows[0]["colVendorRepEmailAddVarchar"].ToString();
                    uotextboxRCCLRepEmailAdd.Text = dt.Rows[0]["colRCCLRepEmailAddVarchar"].ToString();
                    uotextboxVendorRepContactNo.Text = dt.Rows[0]["colVendorRepContactNoVarchar"].ToString();
                    uotextboxRCCLRepContactNo.Text = dt.Rows[0]["colRCCLRepContactNoVarchar"].ToString();

                    uoTextBoxCurrency.Text = dt.Rows[0]["Currency"].ToString();
                    uoTextBoxHotelTimeZone.Text = dt.Rows[0]["colHotelTimeZoneIDVarchar"].ToString();
                    uoGridViewDays.DataSource = dt;
                    uoGridViewDays.DataBind();

                    if (GlobalCode.Field2Int(dt.Rows[0]["colCancellationTermsInt"]) > 0)
                    {
                        uoTextBoxCancellationHours.Text = GlobalCode.Field2Int(dt.Rows[0]["colCancellationTermsInt"]).ToString();
                    }
                    else
                    {
                        uoTextBoxCancellationHours.Text = "";
                    }
                    if (GlobalCode.Field2String(dt.Rows[0]["colCutOffTime"]) != "")
                    {
                        uoTextBoxCutoffTime.Text = string.Format("{0:HH:mm}", GlobalCode.Field2DateTime(dt.Rows[0]["colCutOffTime"]));
                    }
                    else
                    {
                        uoTextBoxCutoffTime.Text = "";
                    }
                    uoTextBoxDefaultDBLRate.Text = GlobalCode.Field2Decimal(dt.Rows[0]["colRoomAmountDblFloat"]).ToString("N2");
                    uoTextBoxDefaultSGLRate.Text = GlobalCode.Field2Decimal(dt.Rows[0]["colRoomAmountSglFloat"]).ToString("N2");
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
            }

        }

        protected void uoGridViewDay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            uoGridViewDays.PageIndex = e.NewPageIndex;
            GetVendorHotelContract();
        }

        /// <summary>
        /// Date Modified:  17/04/2012
        /// Modified By:    Gabriel Oquialda
        /// (description)   Open or Save attachment
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
            byte[] bytes = (byte[])_contractHotelAttachments.AsEnumerable().Where(
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


            Response.ContentType = _contractHotelAttachments.AsEnumerable().Where(
                    data => data.AttachmentId == attachmentId).Select(data => data.FileType).FirstOrDefault().Trim(); ;
            Response.AddHeader("content-length", bytes.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=" + obj.Text);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        //private void ClosePage()
        //{
        //    string sScript = "<script language='javascript'>";
        //    sScript += " parent.$.fancybox.close(); ";
        //    sScript += "</script>";

        //    ScriptManager.RegisterClientScriptBlock(uobtnSave, this.GetType(), "scr", sScript, false);
        //}

        //private void AlertMessage(string s)
        //{
        //    /// <summary>
        //    /// Date Created: 08/07/2011
        //    /// Created By: Marco Abejar
        //    /// (description) Show pop up message            
        //    /// </summary>

        //    string sScript = "<script language='JavaScript'>";
        //    sScript += "alert('" + s + "');";
        //    sScript += "</script>";
        //    ScriptManager.RegisterClientScriptBlock(uobtnSave, this.GetType(), "scr", sScript, false);
        //}

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelContractViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Hotel contract viewed.";
            strFunction = "HotelContractViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, GlobalCode.Field2String(Session["HotelPath"]),
                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        protected List<ContractHotelAttachment> GetHotelAttachments(int contractId)
        {
            return ContractBLL.GetHotelContractAttachment(contractId);
        }
        #endregion


    }
}

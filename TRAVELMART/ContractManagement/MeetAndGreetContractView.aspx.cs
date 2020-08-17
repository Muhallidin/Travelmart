using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class MeetAndGreetContractView : System.Web.UI.Page
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
                    vendorVehicleGetContractDetail();
                    ViewAttachment();
                }
            }            
        }

        protected void uoGridViewVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            uoGridViewVehicle.PageIndex = e.NewPageIndex;
            vendorVehicleGetContractDetail();
        }

        protected void uoGridViewLuggageVan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            uoGridViewLuggageVan.PageIndex = e.NewPageIndex;
            vendorVehicleGetContractDetail();
        }

        protected void uoGridViewServiceRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            uoGridViewServiceRate.PageIndex = e.NewPageIndex;
            vendorVehicleGetContractDetail();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            IDataReader dr = null;
            try
            {
               dr = BLL.ContractBLL.GetVehicleContractAttachment(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value);

                Byte[] bytes = (Byte[])dr["colContractAttachmentVarbinary"];

                Response.Buffer = true;

                Response.Charset = "";

                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.ContentType = dr["colFileTypeVarchar"].ToString();

                //Response.AddHeader("content-disposition", "attachment;filename="

                //+ dt.Rows[0]["filename"].ToString());

                Response.BinaryWrite(bytes);

                Response.Flush();

                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }           
        }

        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor vehicle contract detail     
        /// </summary>
        public void vendorVehicleGetContractDetail()
        {
            DataTable dt = null;
            DataTable dtLV = null;
            DataTable dtSR = null;

            try
            {                
                uoGridViewLuggageVan.DataBind();
                uoGridViewServiceRate.DataBind();
                if (uoHiddenFieldContractId.Value == "")
                {
                    uoHiddenFieldContractId.Value = "0";
                }//Request.QueryString["cId"], Request.QueryString["bId"]

                //dt = BLL.ContractBLL.GetVendorVehicleContractByContractID(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value, 0);
                
                if (dt.Rows.Count > 0)
                {
                    ucLabelContractStatus.Text = dt.Rows[0]["colContractStatusVarchar"].ToString();
                    uoTextBoxVendorCode.Text = dt.Rows[0]["colVendorCodeVarchar"].ToString();
                    uoTextBoxVehicleName.Text = dt.Rows[0]["colVendorNameVarchar"].ToString();
                    //uoTextBoxCountry.Text = dt.Rows[0]["COUNTRY"].ToString();
                    //uoTextBoxCity.Text = dt.Rows[0]["CITY"].ToString();
                    //uoTextBoxVehicleBranch.Text = dt.Rows[0]["BRANCH"].ToString();
                    uoTextBoxContractTitle.Text = dt.Rows[0]["colContractNameVarchar"].ToString();

                    string strContractStartDate = (dt.Rows[0]["colContractDateStartedDate"].ToString().Length > 0)
                        ? String.Format("{0:MM/dd/yyyy HH:mm}", Convert.ToDateTime(dt.Rows[0]["colContractDateStartedDate"].ToString()))
                        : "";
                    string strContractEndDate = (dt.Rows[0]["colContractDateEndDate"].ToString().Length > 0)
                        ? String.Format("{0:MM/dd/yyyy HH:mm}", Convert.ToDateTime(dt.Rows[0]["colContractDateEndDate"].ToString()))
                        : "";
                    uoTextBoxContractStartDate.Text = strContractStartDate;
                    uoTextBoxContractEndDate.Text = strContractEndDate;

                    uoTextBoxRemarks.Text = dt.Rows[0]["colRemarksVarchar"].ToString();

                    uoGridViewVehicle.DataSource = dt;
                    uoGridViewVehicle.DataBind();

                    //uoGridViewLuggageVan.DataSource = dt;
                    //uoGridViewLuggageVan.DataBind();

                    //uoGridViewServiceRate.DataSource = dt;
                    //uoGridViewServiceRate.DataBind();

                    //Loads luggage van data
                    dtLV = BLL.ContractBLL.GetVendorVehicleContractWithLuggageVanByContractID(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value);

                    if (dtLV.Rows.Count > 0)
                    {
                        uoGridViewLuggageVan.DataSource = dtLV;
                        uoGridViewLuggageVan.DataBind();
                    }

                    //Loads service rate data
                    dtSR = BLL.ContractBLL.GetVendorVehicleContractWithServiceRateByContractID(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value);

                    if (dtSR.Rows.Count > 0)
                    {
                        uoGridViewServiceRate.DataSource = dtSR;
                        uoGridViewServiceRate.DataBind();
                    }                    
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

        private void ViewAttachment()
        {
            IDataReader dr = null;
            try
            {
                dr = BLL.ContractBLL.GetVehicleContractAttachment(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value);
                if (dr.Read())
                {
                    LinkButton1.Text = "(No attachment.)";
                    LinkButton1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
           
        }

        //void createDataTable_GridViewDataSource(string Type, string Capacity, string StartDate, string EndDate, string Currency, string Rate)
        //{
        //    DataTable dt = new DataTable();
        //    //dt.Columns.Add("VehicleID");
        //    dt.Columns.Add("Type");
        //    dt.Columns.Add("Capacity");
        //    dt.Columns.Add("StartDate");
        //    dt.Columns.Add("EndDate");
        //    //dt.Columns.Add("CurrencyID");
        //    dt.Columns.Add("Currency");
        //    dt.Columns.Add("Rate");


        //    DataRow dr;
        //    dr = dt.NewRow();
        //    dt.Rows.Add(this.createDataRow_GridViewDataSource(dr, Type, Capacity, StartDate, EndDate, Currency, Rate));

        //    ViewState["Table"] = dt;
        //    this.uoGridViewVehicle.DataSource = dt;
        //}

        //DataRow createDataRow_GridViewDataSource(DataRow dr, string Type, string Capacity, string StartDate, string EndDate, string Currency, string Rate)
        //{
        //    //dr["VehicleID"] = VehicleID;
        //    dr["Type"] = Type;
        //    dr["Capacity"] = Capacity;
        //    dr["StartDate"] = StartDate;
        //    dr["EndDate"] = EndDate;
        //    //dr["CurrencyID"] = CurrencyID;
        //    dr["Currency"] = Currency;
        //    dr["Rate"] = Rate;

        //    return dr;
        //}

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void VehicleContractViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Meet and greet contract viewed.";
            strFunction = "MeetAndGreetContractViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, GlobalCode.Field2String(Session["VehiclePath"]),
                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion       
    }
}
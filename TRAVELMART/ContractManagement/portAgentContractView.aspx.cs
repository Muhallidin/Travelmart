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
    public partial class portAgentContractView : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/Mar/2014
        /// Description:    Regenerate the page same as the Vehicle Contract Viewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    ContractViewLogAuditTrail();
                    PortAgentGetContractDetail();
                }
            }
        }
       
        protected void ucLabelAttached_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            LinkButton obj = (LinkButton)sender;
            string sFilename = obj.CommandArgument;

            List<ContractPortAgentAttachment> list = new List<ContractPortAgentAttachment>();
            list = GetAttachmentList();

            byte[] bytes = (byte[])list.AsEnumerable().Where(
                    data => data.FileName == sFilename).Select(data => data.uploadedFile).FirstOrDefault();

            string fPath = System.IO.Path.GetTempPath();
            string fName = fPath + System.IO.Path.GetTempFileName();

            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = list.AsEnumerable().Where(
                    data => data.FileName == sFilename).Select(data => data.FileType).FirstOrDefault().Trim(); ;

            Response.AddHeader("content-length", bytes.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=" + obj.Text);

            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created:   06/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Contract Details
        /// </summary>
        private void PortAgentGetContractDetail()
        {
            if (GlobalCode.Field2Int(uoHiddenFieldContractId.Value) > 0)
            {
                ContractBLL.GetPortAgentContractByContractID(uoHiddenFieldContractId.Value, uoHiddenFieldBranchId.Value, 0);

                List<ContractPortAgentDetails> list = new List<ContractPortAgentDetails>();
                list = (List<ContractPortAgentDetails>)Session["ContractPortAgentDetails"];
                
                if (list.Count > 0)
                {
                    ucLabelContractStatus.Text = list[0].ContractStatus;
                    uoTextBoxContractTitle.Text = list[0].ContractName;
                    uoTextBoxPortAgentName.Text = list[0].PortAgentName;

                    uoTextBoxCountry.Text = list[0].CountryName;
                    uoTextBoxCity.Text = list[0].CityName;

                    uoTextBoxRemarks.Text = list[0].Remarks;

                    uoCheckBoxAirportToHotel.Checked = list[0].IsAirportToHotel;
                    uoCheckBoxHotelToShip.Checked = list[0].IsHotelToShip;

                    if (list[0].ContractDateStart != null)
                    {
                        uoTextBoxContractStartDate.Text = list[0].ContractDateStart.Value.ToString("MM/dd/yyyy");
                    }
                    if (list[0].ContractDateEnd != null)
                    {
                        uoTextBoxContractEndDate.Text = list[0].ContractDateEnd.Value.ToString("MM/dd/yyyy");
                    }
                    uoTextBoxCurrency.Text = list[0].CurrencyName;

                    uoCheckBoxListBrand.Items[0].Selected = list[0].IsRCI;
                    uoCheckBoxListBrand.Items[1].Selected = list[0].IsAZA;
                    uoCheckBoxListBrand.Items[2].Selected = list[0].IsCEL;
                    uoCheckBoxListBrand.Items[3].Selected = list[0].IsPUL;
                    uoCheckBoxListBrand.Items[4].Selected = list[0].IsSKS;
                }

                BindAirportListView();
                BindSeaportListView();
                BindVehicleTypeCapacityListView();
                BindContractVehicleDetailsList();
                BindContractHotelDetailsList();
                BindAttachmentListView();
            }
        }
        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void ContractViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Vehicle contract viewed.";
            strFunction = "VehicleContractViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, GlobalCode.Field2String(Session["VehiclePath"]),
                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// <summary>
        /// Date Created:   06/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport in ListView
        ///                 List of Airport in Contract
        /// </summary>        
        private void BindAirportListView()
        {
            List<Airport> list = new List<Airport>();
            uoListViewAirport.DataSource = null;
            
            if (Session["VendorAirportExists"] != null)
            {
                list = (List<Airport>)Session["VendorAirportExists"];
                uoListViewAirport.DataSource = list;
            }
            uoListViewAirport.DataBind();
        }
        /// <summary>
        /// Date Created:   06/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Seaport in ListView
        ///                 List of Seaport in Contract
        /// </summary>        
        private void BindSeaportListView()
        {
            List<Seaport> list = new List<Seaport>();
            uoListViewSeaport.DataSource = null;

            if (Session["VendorSeaportExists"] != null)
            {
                list = (List<Seaport>)Session["VendorSeaportExists"];
                uoListViewSeaport.DataSource = list;
            }

            uoListViewSeaport.DataBind();
        }
        /// <summary>
        /// Date Created:   06/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Vehicle Type Capacity in ListView
        /// </summary>
        private void BindVehicleTypeCapacityListView()
        {
            List<ContractVendorVehicleTypeCapacity> listVehicleTypeCapacity = new List<ContractVendorVehicleTypeCapacity>();

            if (Session["ContractVendorVehicleTypeCapacity"] != null)
            {
                listVehicleTypeCapacity = (List<ContractVendorVehicleTypeCapacity>)Session["ContractVendorVehicleTypeCapacity"];
            }

            uoListViewVehicleTypeCapacity.DataSource = listVehicleTypeCapacity;
            uoListViewVehicleTypeCapacity.DataBind();
        }
        /// <summary>
        /// Date Created:   01/Oct/2013
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport in ListView
        ///                 List of Airport in Contract
        /// </summary>        
        private void BindContractVehicleDetailsList()
        {
            List<ContractPortAgentDetailsAmt> list = new List<ContractPortAgentDetailsAmt>();
            uoGridViewPortAgent.DataSource = null;

            if (Session["ContractPortAgentDetailsAmt"] != null)
            {
                list = (List<ContractPortAgentDetailsAmt>)Session["ContractPortAgentDetailsAmt"];
                uoGridViewPortAgent.DataSource = list;
            }

            uoGridViewPortAgent.Columns[0].Visible = true;
            uoGridViewPortAgent.Columns[1].Visible = true;
            uoGridViewPortAgent.Columns[2].Visible = true;
            uoGridViewPortAgent.DataBind();
            uoGridViewPortAgent.Columns[0].Visible = false;
            uoGridViewPortAgent.Columns[1].Visible = false;
            uoGridViewPortAgent.Columns[2].Visible = false;
        }
        /// <summary>
        /// Date Created:   06/Feb/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Attachments
        /// </summary>        
        private void BindAttachmentListView()
        {
            List<ContractPortAgentAttachment> list = new List<ContractPortAgentAttachment>();
            list = GetAttachmentList();

            uoListViewAttachment.DataSource = list;
            uoListViewAttachment.DataBind();
        }
        /// <summary>
        /// Date Created:   26/Sep/2013
        /// Created By:     Josephine Gad
        /// (description)   Get Attachment List from Session or ListView
        /// </summary> 
        private List<ContractPortAgentAttachment> GetAttachmentList()
        {
            List<ContractPortAgentAttachment> list = new List<ContractPortAgentAttachment>();
            if (Session["ContractPortAgentAttachment"] != null)
            {
                list = (List<ContractPortAgentAttachment>)Session["ContractPortAgentAttachment"];
            }
            else
            {
                ContractPortAgentAttachment itemList;
                //HiddenField uoHiddenFieldID;
                HiddenField uoLabelVehicleTypeName;
                foreach (ListViewItem item in uoListViewAttachment.Items)
                {
                    itemList = new ContractPortAgentAttachment();
                    //uoHiddenFieldID = (HiddenField)item.FindControl("uoHiddenFieldID");
                    uoLabelVehicleTypeName = (HiddenField)item.FindControl("uoLabelVehicleTypeName");

                    itemList.FileName = uoLabelVehicleTypeName.Value;
                    list.Add(itemList);
                }
            }
            return list;
        }
        /// <summary>
        /// Date Created:   01/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Hotel rate
        /// </summary>        
        private void BindContractHotelDetailsList()
        {
            List<ContractPortAgentDetailsAmtHotel> list = new List<ContractPortAgentDetailsAmtHotel>();

            if (Session["ContractPortAgentDetailsAmtHotel"] != null)
            {
                list = (List<ContractPortAgentDetailsAmtHotel>)Session["ContractPortAgentDetailsAmtHotel"];
                if (list.Count > 0)
                {
                    uoCheckBoxIsRateByPercent.Checked = list[0].IsRateByPercentBit;
                    uoTextBoxCostPlus.Text = list[0].RoomCostPercent.ToString(); ;

                    uoTextBoxTaxHotel.Text = list[0].RoomRateTaxPercentage.ToString();
                    uoCheckBoxIsTaxInclusive.Checked = list[0].IsTaxInclusive;

                    uoTextBoxSingleRoomRate.Text = list[0].RoomSingleRate.ToString();
                    uoTextBoxDoubleRoomRate.Text = list[0].RoomDoubleRate.ToString();

                    uoTextBoxMealStandard.Text = list[0].MealStandardDecimal.ToString();
                    uoTextBoxMealIncreased.Text = list[0].MealIncreasedDecimal.ToString();

                    uoTextBoxSurchargeSingle.Text = list[0].SurchargeSingle.ToString();
                    uoTextBoxSurchargeDouble.Text = list[0].SurchargeDouble.ToString();
                }
                else
                {
                    uoCheckBoxIsRateByPercent.Checked = false;
                    uoTextBoxCostPlus.Text = "";

                    uoTextBoxTaxHotel.Text = "";
                    uoCheckBoxIsTaxInclusive.Checked = false;

                    uoTextBoxSingleRoomRate.Text = "";
                    uoTextBoxDoubleRoomRate.Text = "";

                    uoTextBoxMealStandard.Text = "";
                    uoTextBoxMealIncreased.Text = "";

                    uoTextBoxSurchargeSingle.Text = "";
                    uoTextBoxSurchargeDouble.Text = "";

                }
            }
        }
        #endregion
    }
}

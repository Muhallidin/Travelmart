using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class PortEditor : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["sfid"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                uoHiddenFieldPortTransID.Value = (Request.QueryString["pId"] == "" ? "0" : Request.QueryString["pId"]);
                uoHiddenFieldTravelRequestID.Value = (Request.QueryString["trID"] == "" ? "0" : Request.QueryString["trID"]);
                //uoHiddenFieldRecordLocator.Value = Request.QueryString["recloc"].ToString();
                uoHiddenFieldManualRequestID.Value = (Request.QueryString["manualReqID"] == "" ? "0" : Request.QueryString["manualReqID"]);

                uoHiddenFieldSeafarerID.Value = Request.QueryString["sfid"].Trim();
                uoHiddenFieldStatus.Value = Request.QueryString["st"];                
                
                GetSeafarerInfo(uoHiddenFieldSeafarerID.Value);
                Session["strPrevPage"] = Request.RawUrl;
            }            
        }
        protected void uobuttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //string PortAgentTransId = (Request.QueryString["pId"] == null) ? "" : Request.QueryString["pId"].ToString();
                //string SFRecLoc = (Request.QueryString["recloc"] == null) ? "" : Request.QueryString["recloc"].ToString();
                string PortAgentTransId = uoHiddenFieldPortTransID.Value;
                string TravelReqId = uoHiddenFieldTravelRequestID.Value;
                //string RecordLocator = uoHiddenFieldRecordLocator.Value;
                string ManualRequestID = uoHiddenFieldManualRequestID.Value;

                string sfID = uoHiddenFieldSeafarerID.Value.Trim();
                string PortAgentID = "1";
                string SFStatus = Request.QueryString["st"].ToString();                                

                string PortTransStatus = uoDropdownListPortTravelStatus.SelectedValue;
                string PortTransDate = uoTextBoxDropOffDate.Text;
                
                string PortID = uoHiddenFieldPortID.Value;
                
                if (uoHiddenFieldPortID.Value.Length == 0)
                {
                    PortID = uoDropDownListPort.SelectedValue;
                }
                bool transCompleted = PortBLL.InsertUpdatePortStatus(PortAgentTransId, TravelReqId,  
                    ManualRequestID, sfID, PortAgentID, PortID, PortTransStatus, 
                    GlobalCode.Field2String(Session["UserName"]), SFStatus, PortTransDate, uoHiddenFieldContractId.Value);
                if (transCompleted && (ViewState["Status"].ToString() != PortTransStatus || ViewState["date"].ToString()!= PortTransDate))
                {
                    string status = null;
                    string Contract = null;
                    if (ViewState["Status"].ToString() != "0")
                    {
                        status = ViewState["Status"].ToString();
                    }
                    if (uoHiddenFieldContractId.Value != "")
                    {
                        Contract = uoHiddenFieldContractId.Value;
                    }
                    //PortBLL.InsertPortStatusHistory(PortAgentTransId, TravelReqId, ManualRequestID, sfID,
                    //    PortAgentID, PortID, status, Session["UserName"].ToString(), SFStatus, 
                    //    ViewState["date"].ToString(), Contract);
                }
                //PortBLL.InsertSFPortDetails(sfID, PortTransStatus, PortTransDate, SFStatus, PortID, SFPID, SFRecLoc);
                OpenParentPage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created:   08/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Get and show seafarer personal and port transaction details            
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// -----------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private void GetSeafarerInfo(string sfCode)
        {           
            IDataReader dtSFInfo = null;
            try
            {
                string sfPID = null;
                string sfStatus = null;

                sfPID = uoHiddenFieldPortTransID.Value;
                sfStatus = uoHiddenFieldStatus.Value.Trim();
                dtSFInfo = PortBLL.GetSFPortTransDetails(sfCode, sfPID, sfStatus, 
                    uoHiddenFieldTravelRequestID.Value, uoHiddenFieldManualRequestID.Value);
                if (dtSFInfo.Read())
                {
                    uotextboxSFName.Text = dtSFInfo["Name"].ToString();
                    uotextboxSFNationality.Text = dtSFInfo["Nationality"].ToString();
                    uotextboxSFRank.Text = dtSFInfo["Rank"].ToString();
                    uotextboxSFVessel.Text = dtSFInfo["Vessel"].ToString();
                    uoTextboxSFPort.Text = dtSFInfo["Port"].ToString();
                    uoHiddenFieldPortID.Value = dtSFInfo["PortId"].ToString();
                    if (dtSFInfo["colContractIdInt"] == null)
                    {
                        uoHiddenFieldContractId.Value = null;                        
                    }
                    else if (dtSFInfo["colContractIdInt"].ToString() == "")
                    {
                        uoHiddenFieldContractId.Value = null;
                    }
                    else
                    {
                        uoHiddenFieldContractId.Value = dtSFInfo["colContractIdInt"].ToString();
                    }
                    //show hide portdropdownlist
                    if (uoHiddenFieldPortID.Value.Length > 0)
                    {
                        uoTextboxSFPort.Visible = true;
                        uoDropDownListPort.Visible = false;
                        uoRequiredFieldValidatorPort.Enabled = false;
                    }
                    else
                    {
                        uoTextboxSFPort.Visible = false;
                        uoDropDownListPort.Visible = true;
                        uoRequiredFieldValidatorPort.Enabled = true;
                        GetPortList();
                    }
                    uoRequiredFieldValidatorPort.Enabled = uoDropDownListPort.Visible = uoHiddenFieldPortID.Value.Length == 0;
                    string strStatus = dtSFInfo["Status"].ToString();
                    if (strStatus.Length > 0)
                    {
                        uoDropdownListPortTravelStatus.SelectedValue = Convert.ToString(uoDropdownListPortTravelStatus.Items.FindByText(strStatus).Value);
                        
                    }
                    ViewState["Status"] = uoDropdownListPortTravelStatus.SelectedValue;

                    DateTime dtTrans = (dtSFInfo["TransDate"].ToString().Length > 0)
                        ? Convert.ToDateTime(dtSFInfo["TransDate"].ToString())
                        : DateTime.Now;                    
                    string TransDate = String.Format("{0:MM/dd/yyyy HH:mm}", dtTrans);
                    uoTextBoxDropOffDate.Text = TransDate;
                    ViewState["date"] = uoTextBoxDropOffDate.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSFInfo != null)
                {
                    dtSFInfo.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Get Port List            
        /// </summary>
        private void GetPortList()
        {                        
            DataTable PortListDataTable = null;
            try
            {
                PortListDataTable = PortBLL.GetPortList();
                uoDropDownListPort.Items.Clear();
                ListItem item = new ListItem("--Select Port--", "");
                uoDropDownListPort.Items.Add(item);
                uoDropDownListPort.DataSource = PortListDataTable.DefaultView;
                uoDropDownListPort.DataBind();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
            finally
            {
                if (PortListDataTable != null)
                {
                    PortListDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page            
        /// </summary>
        private void OpenParentPage()
        {                        
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupPort\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uobuttonSave, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created: 08/07/2011
        /// Created By: Marco Abejar
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {           
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uobuttonSave, this.GetType(), "scr", sScript, false);           
        }
        #endregion
    }
}

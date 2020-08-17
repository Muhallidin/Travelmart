using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.IO;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentContractApproval : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 24/10/2011
        /// ----------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                uoHiddenFieldUserId.Value = GlobalCode.Field2String(Session["UserName"]);
                Session["PortPath"] = Path.GetFileName(Request.Path);
                //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;
                LoadApprovalList();
            }
        }

        protected void uoContractApprovalListPager_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadApprovalList();
            }
        }

        protected void uoContractApprovalList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }

        protected void uoContractApprovalList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SortByCTitle")
            { uoHiddenFieldSortParam.Value = e.CommandArgument.ToString(); }
            else if (e.CommandName == "SortByPortAgent")
            { uoHiddenFieldSortParam.Value = e.CommandArgument.ToString(); }
            else if (e.CommandName == "Select")
            {
                uoHiddenFieldContractId.Value = e.CommandArgument.ToString();
                ApproveContract(GlobalCode.Field2Int(uoHiddenFieldContractId.Value));
            }
        }
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            uoContractApprovalList.DataBind();
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Date Created: 24/10/2011
        /// Created By: Charlene Remotigue
        /// Description: Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
           // Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", sScript, false);
        }
        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 24/10/2011
        /// Description: Load all Service Provider Contract that needs approval
        /// </summary>
        private void LoadApprovalList()
        {
            uoContractApprovalList.Items.Clear();
            uoContractApprovalList.DataSource = null;
            uoContractApprovalList.DataSourceID = "ObjectDataSource1";
        }

        /// <summary>
        /// Date Created:   07/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Approve Vehicle Contract 
        /// </summary>
        private void ApproveContract(int index)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                strLogDescription = "Service Provider pending contract approved.";
                strFunction = "ApproveContract";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                BLL.ContractBLL.UpdatePortAgentContractStatus(index, uoHiddenFieldUserId.Value,
                    strLogDescription, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate));
                uoContractApprovalList.DataBind();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }

        }
        #endregion
             
        //protected void ObjectDataSource1_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;            

        //    if (e.ReturnValue.ToString() == "0")
        //    {
        //        AlertMessage("Contract successfully approved.");

        //        //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
        //        strLogDescription = "Port contract approved.";
        //        strFunction = "ObjectDataSource1_Updated";

        //        DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(uoHiddenFieldContractId.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //                                              CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        //    }
        //    else
        //    {
        //        AlertMessage("Cannot approve due to a current active contract.");
        //    }
        //}
    }
}

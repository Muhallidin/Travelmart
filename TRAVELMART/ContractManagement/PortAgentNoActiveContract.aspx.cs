using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentNoContract : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 25/10/2011
        /// ---------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("../Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {
                
                LoadContractList();
                UpdatePanel1.Update();
            }
        }
        protected void uoPortAgentContractListPager_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadContractList();
            }
        }

        protected void uoPortAgentContractList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SortByAgentName")
            { 
                uoHiddenFieldSortParam.Value = e.CommandArgument.ToString(); 
            }
            else if (e.CommandName == "SortByCompanyName")
            { 
                uoHiddenFieldSortParam.Value = e.CommandArgument.ToString(); 
            }

        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 25/10/2011
        /// Description: Load all Service Providers with no active contracts
        /// </summary>
        private void LoadContractList()
        {
            uoPortAgentContractList.Items.Clear();
            uoPortAgentContractList.DataSource = null;
            uoPortAgentContractList.DataSourceID = "ObjectDataSource1";
        }
        #endregion

        

       

    }
}

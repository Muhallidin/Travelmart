using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class EventsView_old : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();

        #region EVENTS
        /// <summary>
        /// Date Created:   12/10/2011
        /// Created By:     Charlene Remotigue
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if ((GlobalCode.Field2String(Session["Region"]) != "" && (GlobalCode.Field2String(Session["Region"])) != "0") ||
                    ((GlobalCode.Field2String(Session["Country"])) != "" && (GlobalCode.Field2String(Session["Country"])) != "0") ||
                    ((GlobalCode.Field2String(Session["Hotel"])) != "" && (GlobalCode.Field2String(Session["Hotel"])) != "0"))
                {
                    uoButtonViewAll.Enabled = true;
                }
                else
                {
                    uoButtonViewAll.Enabled = false;
                }
                uoHiddenFieldViewAll.Value = "false";
                Session["strPrevPage"] = Request.RawUrl;
                uoHyperLinkEventsAdd.HRef = "~/Maintenance/EventsAdd.aspx?eId=0";
                SetDefaults();                
            }            
        }

        protected void uoButtonViewAll_Click(object sender, EventArgs e)
        {
            uoButtonViewAll.Enabled = false;
            uoHiddenFieldViewAll.Value = "true";
            uoHIddenFieldCountryId.Value = "0";
            uoHiddenFieldBranchId.Value = "0";
            uoHiddenFieldRegionId.Value = "0";
            BindEvents();
        }

        protected void uoEventList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldEventId.Value = e.CommandArgument.ToString();
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
           
        }

        protected void uoEventList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion

        #region METHODS
        protected void SetDefaults()
        {
            uoHiddenFieldDateTo.Value = GlobalCode.Field2String(Session["DateTo"]);
            uoHiddenFieldDateFrom.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHIddenFieldCountryId.Value = GlobalCode.Field2String(Session["Country"]);
            uoHiddenFieldBranchId.Value = GlobalCode.Field2String(Session["Hotel"]);
            uoHiddenFieldRegionId.Value = GlobalCode.Field2String(Session["Region"]);
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            BindEvents();
        }
        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: loads events to listview
        /// </summary>
        private void BindEvents()
        {            

            int Region = 0;
            int Country = 0;
            int BranchId = 0;

            if((GlobalCode.Field2String(Session["Region"])) != "")
            {
                Region = GlobalCode.Field2Int(uoHiddenFieldRegionId.Value);
            }
            if((GlobalCode.Field2String(Session["Country"])) != "")
            {
                Country = GlobalCode.Field2Int(uoHIddenFieldCountryId.Value);
            }
            if((GlobalCode.Field2String(Session["Hotel"])) != "")
            {
                BranchId = GlobalCode.Field2Int(uoHiddenFieldBranchId.Value);
            }

            masterBLL.EventsViewLoadEvents(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2DateTime(uoHiddenFieldDateFrom.Value),
                GlobalCode.Field2DateTime(uoHiddenFieldDateTo.Value), Region, Country, BranchId, 0, 0, 1);

            ObjectDataSource1.TypeName = "TRAVELMART.Common.Maintenance";
            ObjectDataSource1.SelectCountMethod = "GetEventCount";
            ObjectDataSource1.SelectMethod = "GetEvents";

            uoEventList.DataSourceID = ObjectDataSource1.UniqueID;
        }

        
        #endregion
       

        
    }
}

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

namespace TRAVELMART
{
   
    public partial class SailMasterView : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region EVENTS
        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldViewAll.Value = "false";
                uoButtonViewAll.Enabled = true;               
                 Session["strPrevPage"]  = Request.RawUrl;
                uoTextBoxSearch.Text = "";
            }
        }

        protected void uoVesselListPager_PreRender(object sender, EventArgs e)
        {
            BindVessel();
        }

        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (!e.ExecutingSelectCount)
            {
                e.Arguments.MaximumRows = uoVesselListPager.PageSize;

            }
        }

        protected void uoButtonViewAll_Click(object sender, EventArgs e)
        {
            uoButtonViewAll.Enabled = false;
            uoHiddenFieldViewAll.Value = "true";

            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "View all button for sail master ship view clicked.";
            strFunction = "uoButtonViewAll_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion


        #region METHODS
        private void BindVessel()
        {
            //int maximumRows = masterBLL.SailMasterMaintenanceVesselSearchCount(uoTextBoxSearch.Text);
            //ObjectDataSource1.DataBind();
            uoVesselList.DataSource = null;
            uoVesselList.DataSourceID = "ObjectDataSource1";
            
        }
        #endregion

        

       

    }
}

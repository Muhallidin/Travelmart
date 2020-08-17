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

namespace TRAVELMART.Maintenance
{
    public partial class SailMaterAdd : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// -----------------------------------
        /// Modified by: Charlene Remotigue
        /// Date Modified: 26/10/2011
        /// Description: added checking for port specialist
        /// -------------------------------------------
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SailMasterViewLogAuditTrail();
                uoHiddenFieldVesselID.Value = Request.QueryString["vId"];
                uoHiddenFieldVesselName.Value = Request.QueryString["vName"];
                Session["strPrevPage"] = Request.RawUrl;
                uoHyperLinkSailMasterAdd.HRef = "~/Maintenance/SailMasterAdd.aspx?vId=" + uoHiddenFieldVesselID.Value + "&vName=" + uoHiddenFieldVesselName.Value + "&sId=0";
                //set visibililty
                if (GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RolePortSpecialist)
                {
                    uoHyperLinkSailMasterAdd.Visible = false;
                }
            }
        }

        protected void uoSailMasterListPager_PreRender(object sender, EventArgs e)
        {
            Session["ItineraryCode"] = uoTextBoxSearch.Text.Trim();

            uoSailMasterList.DataSource = null;
            uoSailMasterList.DataSourceID = "ObjectDataSource1";
        }

        protected void uoSailMasterList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                uoHiddenFieldSailMasterId.Value = e.CommandArgument.ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                string strLogDescription;
                string strFunction;

                //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                strLogDescription = "Sail master information deleted. (flagged as inactive)";
                strFunction = "uoSailMasterList_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(uoHiddenFieldSailMasterId.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
        }

        protected void uoSailMasterList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            Session["ItineraryCode"] = uoTextBoxSearch.Text.Trim();

            uoSailMasterList.DataSource = null;
            uoSailMasterList.DataSourceID = "ObjectDataSource1";

            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "View button for sail master view clicked.";
            strFunction = "uoButtonView_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void SailMasterViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Ship sail master list viewed.";
            strFunction = "SailMasterViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
    }
}

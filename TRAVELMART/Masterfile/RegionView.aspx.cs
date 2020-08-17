using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Web.Security;

namespace TRAVELMART
{
    public partial class RegionView : System.Web.UI.Page
    {
        #region EVENTS

        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Charlene Remotigue
        /// ------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>        
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                uoHyperLinkRegionAdd.HRef = "RegionAdd2.aspx?vmId=0&vmName=";
                BindRegion();
            }
            if (uoHiddenFieldIsDelete.Value == "1")
            {
                DeleteRegion();
            }
            uoHiddenFieldIsDelete.Value = "0";
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// </summary>
        
        protected void uoRegionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                uoHiddenFieldRegionId.Value = e.CommandArgument.ToString();
                DeleteRegion();
            }
        }

        protected void uoRegionList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoRegionListPager_PreRender(object sender, EventArgs e)
        {
            //BindRegion();
          
        }


        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// </summary>
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            uoHiddenFieldRegion.Value = uoTextSearchParam.Text;
           
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 09/05/2012
        /// Description: initialize session values
        /// </summary>
        protected void InitializeValues()
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
            }
            MembershipUser sUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (sUser == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!sUser.IsOnline)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                Session["UserRole"] = UserRolePrimary;
            }

            Session["strPrevPage"] = Request.RawUrl;
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: binds all regions to listview
        /// </summary>
        private void BindRegion()
        {
            try
            {                
                uoRegionList.Items.Clear();
                uoRegionList.DataSource = null;
                uoRegionList.DataSourceID = "ObjectDataSource1";
                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// Date Created: 28/02/2013
        /// Created By:   Gabriel Oquialda
        /// (description) Set region list groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string RegionAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Region";
            string GroupValueString = "colRegionNameVarchar";

            string RegionIdString = "colRegionIDInt";
            string RegionNameString = "colRegionNameVarchar";

            string RegionIdValue = Eval(RegionIdString).ToString();
            string RegionNameValue = Eval(RegionNameString).ToString();

            //string EditString = "<td class=\"group\" colspan=\"1\"><a runat=\"server\" class=\"RegionLink\" id=\"uoAEditRegion\" href=\"RegionAdd.aspx?vmId=" + RegionIdValue + "\"&vmName=" + RegionNameValue + "\">Edit</a></td>";
            string EditString = "<td class=\"group\" colspan=\"1\"><a runat=\"server\" class=\"RegionLink\" id=\"uoAEditRegion\" href=\"RegionAdd2.aspx?vmId=" + RegionIdValue + "&vmName=" + RegionNameValue + "\">Edit</a></td>";
            string DeleteString = "<td class=\"group\" colspan=\"1\"><a runat=\"server\" id=\"uoIsDelete\"  href=\"#\" OnClick=\"return DeleteRegion('" + RegionIdValue + "');\">Delete</a></td>";            
                                    
            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                return string.Format("<tr><td class=\"group\" colspan=\"1\">{0}: <strong>{1}</strong></td>{2}{3}</tr>", GroupTextString, currentDataFieldValue, EditString, DeleteString);
                //return string.Format("<tr><td class=\"group\" colspan=\"1\">{0}: <strong>{1}</strong></td><td class=\"group\" colspan=\"1\"><a runat=\"server\" class=\"RegionLink\" id=\"uoAEditRegion\" href=\"RegionAdd.aspx?vmId=" + RegionIdValue + "\"&vmName=" + RegionNameValue + "\">Edit</a></td><td class=\"group\" colspan=\"1\"><asp:LinkButton ID=\"uoLinkButtonDelete\" runat=\"server\" CommandArgument=" + RegionIdValue + "\" Text=\"Delete\" CommandName=\"Delete\" OnClientClick=\"return confirmDelete();\"></asp:LinkButton></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        private void DeleteRegion()
        {
            string strLogDescription;
            string strFunction;
                      
            RegionBLL.DeleteRegion(GlobalCode.Field2Int(uoHiddenFieldRegionId.Value), uoHiddenFieldUser.Value);

            //Insert log audit trail (Gabriel Oquialda - 28/02/2012)
            strLogDescription = "Region deleted. (flagged as inactive)";
            strFunction = "uoRegionList_ItemCommand";

            DateTime currentDate = CommonFunctions.GetCurrentDateTime();
            BLL.AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(uoHiddenFieldRegionId.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);

        }
        #endregion
    }
}

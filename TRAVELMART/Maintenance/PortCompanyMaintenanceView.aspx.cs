using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.IO;

namespace TRAVELMART
{
    public partial class PortCompanyMaintenanceView : System.Web.UI.Page
    {

        #region Events
        /// <summary>
        /// Date Modified: 12/10/2011
        /// Modified by: Charlene Remotigue
        /// -----------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date Modified: 26/10/2011
        /// Description: added checking for port specialist
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                SFStatus.Visible = false;
                 Session["strPrevPage"]  = Request.RawUrl;
                //BindRegion();
                GetPortList(uoTextBoxSearchParam.Text);
                uoHyperLinkPortCompanyAdd.HRef = "~/Maintenance/PortCompanyMaintenance.aspx?cmId=0";

                //set visibililty
                if (GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RolePortSpecialist)
                {
                    uoPortCompanySearch.Visible = false;
                    Button1.Visible = false;
                }
            }            
            
            if (uoHiddenFieldPopupPort.Value == "1")
            {
                GetPortList(uoTextBoxSearchParam.Text);
            }
            uoHiddenFieldPopupPort.Value = "0";
        }
        protected void uoPortCompanyList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                MaintenanceViewBLL.DeletePort(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Port vendor deleted. (flagged as inactive)";
                strFunction = "uoPortCompanyList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetPortList(uoTextBoxSearchParam.Text);
            }
        }
        protected void uoPortCompanyList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uoPortCompanyListPager_PreRender(object sender, EventArgs e)
        {
            GetPortList(uoTextBoxSearchParam.Text);

            //string strLogDescription;
            //string strFunction;

            ////Insert log audit trail (Gabriel Oquialda - 18/11/2011)
            //strLogDescription = "Search button for port vendor view clicked.";
            //strFunction = "uoPortCompanyListPager_PreRender";

            //DateTime currentDate = CommonFunctions.GetCurrentDateTime();

            //BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetPortList(uoTextBoxSearchParam.Text);
        //}    
        #endregion


        #region Functions 
       
        private void GetPortList(string strPortName)
        {
            /// <summary>
            /// Date Created: 04/08/2011
            /// Created By: Marco Abejar
            /// (description) Get the list of ports            
            /// </summary>

            uoPortCompanyList.DataSource = MaintenanceViewBLL.GetPortCompanyList(strPortName, GlobalCode.Field2String(Session["UserName"]),  Session["Region"] .ToString());
            uoPortCompanyList.DataBind();
        }

        protected string FormatUSContactNo(object oUSContactNo)
        {
            /// <summary>
            /// Date Created: 03/08/2011
            /// Created By: Gabriel Oquialda
            /// (description) Contact number US format            
            /// </summary>

            String strUSContactNo = (String)oUSContactNo;

            if (strUSContactNo != "")
            {
                string strFormat;
                strFormat = string.Format("({0}) {1}-{2}",
                    strUSContactNo.Substring(0, 3),
                    strUSContactNo.Substring(3, 3),
                    strUSContactNo.Substring(6));
                return strFormat;
            }
            else
            {
                return "";
            }
        }

        //private void BindRegion()
        //{
        //    /// <summary>
        //    /// Date Created: 19/08/2011
        //    /// Created By: Marco Abejar
        //    /// (description) Bind assigned region           
        //    /// </summary>

        //    DataTable RegionDataTable = null;
        //    try
        //    {
        //        RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
        //        uoDropDownListRegion.Items.Clear();
        //        ListItem item = new ListItem("--Select Region--", "0");
        //        uoDropDownListRegion.Items.Add(item);
        //        uoDropDownListRegion.DataSource = RegionDataTable;
        //        uoDropDownListRegion.DataTextField = "colMapNameVarchar";
        //        uoDropDownListRegion.DataValueField = "colMapIDInt";
        //        uoDropDownListRegion.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (RegionDataTable != null)
        //        {
        //            RegionDataTable.Dispose();
        //        }
        //    }
        //}    

        #endregion      
    }
}

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Web.Security;

namespace TRAVELMART
{
    public partial class HotelMaintenanceView : System.Web.UI.Page
    {

        #region Events
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date Modified: 27/10/2011
        /// Description: added checking for hotel vendor
        /// ----------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  27/10/2011
        /// Description:    Change MUser.GetUserRole() to User.IsInRole
        /// ----------------------------------------------
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeValues();
               //BindRegion();
                GetHotelVendorListByUser(uoTextBoxSearchParam.Text);              
                uoHyperLinkHotelAdd.HRef = "~/Maintenance/HotelMaintenance.aspx?vmId=0&vmType=HO";
                //Session["strPrevPage"] = Request.RawUrl;

                if (User.IsInRole(TravelMartVariable.RoleAdministrator) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAssist) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAdmin) ||
                    User.IsInRole(TravelMartVariable.RolePortSpecialist) ||
                    User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
                    )
                {
                    uoBtnHotelAdd.Visible = true;
                }
                else
                {
                    uoBtnHotelAdd.Visible = false;
                    uoHiddenFieldVendor.Value = "false";
                }
            }
            //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //SFStatus.Visible = false;           
        }

        protected void uoHotelVendorList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;            
            uoHiddenFieldOrderBy.Value = e.CommandName;
            if (e.CommandName == "Delete")
            {   int index = Convert.ToInt32(e.CommandArgument);

                MaintenanceViewBLL.DeleteHotelVendor(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Hotel vendor deleted. (flagged as inactive)";
                strFunction = "uoHotelVendorList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                GetHotelVendorListByUser(uoTextBoxSearchParam.Text);
            }
        }
        protected void uoHotelVendorList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {
        }
        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {
            GetHotelVendorListByUser(uoTextBoxSearchParam.Text);
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetHotelVendorListByUser(uoTextBoxSearchParam.Text);
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Functions
        /// <summary>
        /// Author:Charlene Remotigue
        /// Date Created: 29/03/2012
        /// Description: Initialize session values
        /// </summary>
        /// <returns></returns>
        protected void InitializeValues()
        {
            string sUserName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }

            Session["strPrevPage"] = Request.RawUrl;

            if (Session["UserRole"] == null)
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }
        }
        //private void GetHotelVendorList(string strHotelName)
        //{
        //    /// <summary>
        //    /// Date Created: 04/08/2011
        //    /// Created By: Marco Abejar
        //    /// (description) Get the list of hotel vendors            
        //    /// </summary>
        //    //GlobalCode.Field2String(Session["Region"]), 
        //    // GlobalCode.Field2String(Session["Country"]),
        //    //GlobalCode.Field2String(Session["City"]),
        //    //GlobalCode.Field2String(Session["Port"]), 
        //    //GlobalCode.Field2String(Session["Hotel"]);
        //    uoHotelVendorList.DataSource = MaintenanceViewBLL.GetHotelVendorList(strHotelName, GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue);
        //    uoHotelVendorList.DataBind();
        //}
        /// <summary>
        /// Date Created:   04/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get the list of hotel vendors     
        /// --------------------------------------------------
        /// Date Modified:  27/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Validate if user role is not empty
        /// /// --------------------------------------------------
        /// Date Modified:  18/03/2013
        /// Modified By:    Marco Abejar
        /// (description)   Add sorting
        /// </summary>
        private void GetHotelVendorListByUser(string strHotelName)
        {            
            //GlobalCode.Field2String(Session["Region"]), 
            // GlobalCode.Field2String(Session["Country"]),
            //GlobalCode.Field2String(Session["City"]),
            //GlobalCode.Field2String(Session["Port"]), 
            //GlobalCode.Field2String(Session["Hotel"]);

            DataTable dt = MaintenanceViewBLL.GetHotelVendorListByUser(strHotelName, GlobalCode.Field2String(Session["UserName"]),
                                            GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["UserVendor"]), GlobalCode.Field2String(Session["UserRole"]));
            DataView dtView = dt.DefaultView;
            if (dt.Rows.Count > 0 && uoHiddenFieldOrderBy.Value != "0")
            {               
                dtView.Sort = uoHiddenFieldOrderBy.Value;
            }
            uoHotelVendorList.DataSource = dtView.ToTable();
            uoHotelVendorList.DataBind();
        }
        /// <summary>
        /// Date Created: 03/08/2011
        /// Created By:   Gabriel Oquialda
        /// (description) Contact number US format            
        /// </summary>
        protected string FormatUSContactNo(object oUSContactNo)
        {         
            String strUSContactNo = (String)oUSContactNo;
            if (strUSContactNo != "")
            {
                if (strUSContactNo.Length < 10)
                {
                    strUSContactNo = strUSContactNo.Insert(0, "0000000000");
                    strUSContactNo.Remove(0, strUSContactNo.Length - 10);
                    string strFormat;
                    strFormat = string.Format("({0}) {1}-{2}",
                        strUSContactNo.Substring(0, 3),
                        strUSContactNo.Substring(3, 3),
                        strUSContactNo.Substring(6));
                    return strFormat;
                }
                else
                {
                    string strFormat;
                    strFormat = string.Format("({0}) {1}-{2}",
                        strUSContactNo.Substring(0, 3),
                        strUSContactNo.Substring(3, 3),
                        strUSContactNo.Substring(6));
                    return strFormat;
                }
            }
            else
            {
                return "";
            }            
        }
        /// <summary>
        /// Date Created: 19/08/2011
        /// Created By: Marco Abejar
        /// (description) Bind assigned region           
        /// </summary>
        private void BindRegion()
        {         
            DataTable RegionDataTable = null;
            try
            {
                RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListRegion.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListRegion.Items.Add(item);
                uoDropDownListRegion.DataSource = RegionDataTable;
                uoDropDownListRegion.DataTextField = "colMapNameVarchar";
                uoDropDownListRegion.DataValueField = "colMapIDInt";
                uoDropDownListRegion.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }   
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;

namespace TRAVELMART.Maintenance
{
    public partial class HotelMaintenanceBranchView : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 29/03/2012
        /// Description: transfer previous page to new master page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                BindRegion();
                BindBranch();
            }
        }

        protected void uoBtnHotelAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("HotelMaintenanceBranch.aspx?st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&vmId=0&vmType=HO&dt=" + Request.QueryString["dt"]);
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            if (uoDropDownListRegion.SelectedValue == "0")
            {
                BindCountry();
                uoDropDownListCountry.SelectedValue = "0";
                BindAirport();
                //BindCity();
                uoDropDownListAirport.SelectedValue = "0";

                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["Airport"] = "0";
            }
            BindBranch();

            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Search button for hotel branch view clicked.";
            strFunction = "uoButtonSearch_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        protected void uoHotelVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SortByBranchName")
            {
                uoHiddenFieldSortByBranch.Value = "1";
                uoHiddenFieldSortByPriority.Value = "2";
            }
            else if (e.CommandName == "SortByPriority")
            {
                uoHiddenFieldSortByBranch.Value = "2";
                uoHiddenFieldSortByPriority.Value = "1";
            }
            if (e.CommandName != "")
            {
                uoHiddenFieldOrderBy.Value = e.CommandName;
                BindBranch();
            }
        }

        protected void uoHotelVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {
            BindBranch();
        }

        protected void uoButtonViewBranch_Click(object sender, EventArgs e)
        {
            uoDropDownListRegion.SelectedValue = uoHiddenFieldRegion.Value;
            BindCountry();
            uoDropDownListCountry.SelectedValue = uoHiddenFieldCountry.Value;
            uoTextBoxSearchAirport.Text = uoHiddenFieldAirportName.Value;
            BindAirport();
            //BindCity();
            if (uoDropDownListAirport.Items.FindByValue(uoHiddenFieldAirport.Value) == null)
            {
                AlertMessage("Select Airport.");
            }
            else
            {
                uoDropDownListAirport.SelectedValue = uoHiddenFieldAirport.Value;
                Session["Region"] = uoDropDownListRegion.SelectedValue;
                Session["Country"] = uoDropDownListCountry.SelectedValue;
                Session["Airport"] = uoDropDownListAirport.SelectedValue;
                BindBranch();
            }
            HotelBranchViewLogAuditTrail();
        }

        //protected void uoButtonSavePriority_Click(object sender, EventArgs e)
        //{
        //    SavePriority();
        //    BindBranch();
        //    AlertMessage("Save successfully.");
        //}

        protected void uoHotelVendorList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                TextBox uoTextBoxPriority = (TextBox)e.Item.FindControl("uoTextBoxPriority");
                HiddenField uoHiddenFieldBranchID = (HiddenField)e.Item.FindControl("uoHiddenFieldBranchID");
                string scr = "validatePriority('" + uoHiddenFieldBranchID.Value + "', '" + uoTextBoxPriority.ClientID + "')";
                uoTextBoxPriority.Attributes.Add("onblur", scr);
            }
        }
        #endregion

        #region METHODS
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
            Session["HotelPath"] = Path.GetFileName(Request.Path);
        }

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
                uoDropDownListRegion.DataTextField = "colRegionNameVarchar";
                uoDropDownListRegion.DataValueField = "colRegionIDInt";
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

        private void BindBranch()
        {
            GetHotelVendorBranchList(uoTextBoxSearchParam.Text);
            HtmlControl EditTH = (HtmlControl)uoHotelVendorList.FindControl("EditTH");
            HtmlControl ContractListTH = (HtmlControl)uoHotelVendorList.FindControl("ContractListTH");
            HtmlControl ContractTH = (HtmlControl)uoHotelVendorList.FindControl("ContractTH");
            HtmlControl PriorityTH = (HtmlControl)uoHotelVendorList.FindControl("PriorityTH");

            if (User.IsInRole(TravelMartVariable.RoleAdministrator)
            || User.IsInRole(TravelMartVariable.RoleCrewAssist)
            || User.IsInRole(TravelMartVariable.RoleCrewAdmin)
            || User.IsInRole(TravelMartVariable.RolePortSpecialist)
            || User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
            || User.IsInRole(TravelMartVariable.RoleContractManager)
            || User.IsInRole(TravelMartVariable.RoleHotelVendor))
            {
                uoBtnHotelAdd.Visible = true;
            }
            else
            {
                uoBtnHotelAdd.Visible = false;
                uoHiddenFieldVendor.Value = "false";
                uoHiddenFieldVendorClass.Value = "hideElement";

                if (EditTH != null)
                {
                    EditTH.Style.Add("display", "none");
                }
            }

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }
            string UserRole = GlobalCode.Field2String(Session["UserRole"]); ;

            if (UserRole == TravelMartVariable.RoleAdministrator
            || UserRole == TravelMartVariable.RoleCrewAssist
            || UserRole == TravelMartVariable.RoleCrewAdmin
            || UserRole == TravelMartVariable.RoleHotelSpecialist
            || UserRole == TravelMartVariable.RoleContractManager)
            {
                uoHiddenFieldViewContract.Value = "true";
            }
            else
            {
                uoHiddenFieldViewContract.Value = "false";
                uoHiddenFieldViewContractClass.Value = "hideElement";

                if (ContractListTH != null)
                {
                    ContractListTH.Style.Add("display", "none");
                }
            }
            //setup contract column
            if (User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RoleContractManager))
            {
                uoHiddenFieldEditAddContract.Value = "true";

                //if (ContractTH != null)
                //{
                //    ContractTH.Style.Add("display", "inline");
                //}
            }
            else
            {
                uoHiddenFieldEditAddContract.Value = "false";
                uoHiddenFieldEditAddContractClass.Value = "hideElement";

                if (ContractTH != null)
                {
                    ContractTH.Style.Add("display", "none");
                }
            }
            //setup priority column
            if ((User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RoleHotelSpecialist)) &&
                uoDropDownListAirport.SelectedValue != "0" && uoTextBoxSearchParam.Text.Trim() == ""
                && uoDropDownListAirport.SelectedValue != "" &&
                uoHotelVendorList.Items.Count > 0
                )
            {
                uoHiddenFieldViewPriority.Value = "true";
                //uoButtonSavePriority.Visible = true;

                if (PriorityTH != null)
                {
                    PriorityTH.Visible = true;
                    // PriorityTH.Style.Add("display", "inline");
                }
            }
            else
            {
                //uoButtonSavePriority.Visible = false;
                uoHiddenFieldViewPriority.Value = "false";

                if (PriorityTH != null)
                {
                    //                    PriorityTH.Style.Add("display", "none");
                    PriorityTH.Visible = false;
                }
            }
        }

        private void GetHotelVendorBranchList(string strHotelName)
        {
            DataTable dt = null;
            try
            {
                if (Session["UserRole"] == null)
                {
                    Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                }
                dt = MaintenanceViewBLL.GetHotelVendorBranchListByUser(strHotelName,
                                                    GlobalCode.Field2String(Session["UserName"]),
                                                    GlobalCode.Field2String(Session["Region"]),
                                                    GlobalCode.Field2String(Session["Country"]),
                                                    GlobalCode.Field2String(Session["Airport"]),
                                                    GlobalCode.Field2String(Session["Port"]),
                                                    "0",
                                                    GlobalCode.Field2String(Session["UserRole"]),
                                                    uoHiddenFieldSortByBranch.Value,
                                                    uoHiddenFieldSortByPriority.Value
                                                    );
                //var results = from myRow in dt.AsEnumerable()
                //              where myRow.Field<int>(uoHiddenFieldOrderBy.Value) == 1
                //              select myRow;
                //uoHotelVendorList.DataSource = results;
                DataView dtview = dt.DefaultView;
                uoHiddenFieldOrderBy.Value = (uoHiddenFieldOrderBy.Value == "SortByBranchName") ? "colVendorBranchNameVarchar" : uoHiddenFieldOrderBy.Value;
                dtview.Sort = uoHiddenFieldOrderBy.Value;
                uoHotelVendorList.DataSource = dtview.ToTable();
                uoHotelVendorList.DataSource = dt;
                uoHotelVendorList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        protected string FormatUSContactNo(object oUSContactNo)
        {
            /// <summary>
            /// Date Created: 03/08/2011
            /// Created By:   Gabriel Oquialda
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

        private void BindCountry()
        {
            DataTable dt = null;
            try
            {
                dt = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, "");
                uoDropDownListCountry.Items.Clear();
                uoDropDownListCountry.DataSource = dt;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private void BindAirport()
        {
            DataTable dt = null;
            try
            {
                dt = AirBLL.GetAirportByCountry(uoDropDownListCountry.SelectedValue, uoDropDownListRegion.SelectedValue, uoTextBoxSearchAirport.Text, "0");
                uoDropDownListAirport.Items.Clear();
                uoDropDownListAirport.DataSource = dt;
                uoDropDownListAirport.DataTextField = "colAirportNameVarchar";
                uoDropDownListAirport.DataValueField = "colAirportIDInt";
                uoDropDownListAirport.DataBind();
                uoDropDownListAirport.Items.Insert(0, new ListItem("--Select Airport--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private void SavePriority()
        {
            //foreach (ListViewItem item in uoHotelVendorList.Items)
            //{
            //    string strLogDescription;
            //    string strFunction;

            //    HiddenField uoHiddenFieldBranchID = (HiddenField)item.FindControl("uoHiddenFieldBranchID");
            //    TextBox uoTextBoxPriority = (TextBox)item.FindControl("uoTextBoxPriority");

            //    if (uoHiddenFieldBranchID != null)
            //    {
            //        Int32 AirportHotelID = MaintenanceViewBLL.SaveHotelPriority(uoDropDownListAirport.SelectedValue, uoHiddenFieldBranchID.Value, uoTextBoxPriority.Text, GlobalCode.Field2String(Session["UserName"]));

            //        //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
            //        strLogDescription = "Hotel branch priority updated.";
            //        strFunction = "SavePriority";

            //        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

            //        BLL.AuditTrailBLL.InsertLogAuditTrail(AirportHotelID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            //    }
            //}
        }

        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "scr", sScript, false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }

        protected void HotelBranchViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "View branch button for hotel airport view clicked.";
            strFunction = "HotelBranchViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

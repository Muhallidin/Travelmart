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
using System.Web.UI.HtmlControls;

namespace TRAVELMART.Maintenance
{
    public partial class HotelAirportView : System.Web.UI.Page
    {
        #region "Events"
        /// -------------------------------------------
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldUserRole.Value = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                Session["strPrevPage"] = Request.RawUrl;
                Session["HotelPath"] = Path.GetFileName(Request.Path);
                BindRegion();
                //BindCountry();
                BindAirport();
                BindHotel();
            }
            //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;
            if (uoHiddenFieldPopupAdd.Value == "1")
            {
                uoDropDownListRegion.SelectedValue = uoHiddenFieldRegion.Value;
                //BindCountry();
                //uoDropDownListCountry.SelectedValue = uoHiddenFieldCountry.Value;
                uoTextBoxSearchAirport.Text = uoHiddenFieldAirportName.Value;                
                BindAirport();

                BindHotel();
                uoHotelVendorList.DataBind();
            }
            uoHiddenFieldPopupAdd.Value = "0";
        }
        protected void uoButtonFilterAirport_Click(object sender, EventArgs e)
        {

        }
        protected void uoButtonViewBranch_Click(object sender, EventArgs e)
        {
            uoDropDownListRegion.SelectedValue = uoHiddenFieldRegion.Value;
            //BindCountry();
            //uoDropDownListCountry.SelectedValue = uoHiddenFieldCountry.Value;
            uoTextBoxSearchAirport.Text = uoHiddenFieldAirportName.Value;
            //uoHiddenFieldAirport.Value = uoDropDownListAirport.SelectedValue;
            BindAirport();
            //BindHotel();
            uoHotelVendorList.DataBind();
            HotelAirportViewLogAuditTrail();
        }
        protected void uoObjectDataSourceHotel_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["strHotelName"] = "";
            e.InputParameters["strUser"] = uoHiddenFieldUser.Value; 
            
            e.InputParameters["Region"] = uoDropDownListRegion.SelectedValue;
            //e.InputParameters["Country"] = uoDropDownListCountry.SelectedValue;
            e.InputParameters["Country"] = uoHiddenFieldCountry.Value;
            e.InputParameters["Airport"] = uoDropDownListAirport.SelectedValue;
            e.InputParameters["Port"] = "0";
            e.InputParameters["Hotel"] = Session["Hotel"].ToString();            

            e.InputParameters["UserRole"] = uoHiddenFieldUserRole.Value;
            e.InputParameters["LoadType"] = uoHiddenFieldLoadType.Value;

            e.InputParameters["SortBy"] = uoHiddenFieldSortBy.Value;
            e.InputParameters["iRoomType"] = uoDropDownListRoomType.SelectedValue;
        }
        protected void uoHotelVendorList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName;
            
            if (e.CommandName == "Delete")
            {
                string[] AirportHotelBranch = e.CommandArgument.ToString().Split("::".ToCharArray());

                DeleteHotelInAirport(AirportHotelBranch[0], AirportHotelBranch[2]);
                BindHotel();                
            }
            if (e.CommandName == "SortByPriority")
            {
                uoHiddenFieldSortBy.Value = "SortByPriority";
                BindHotel();  
            }
            if (e.CommandName == "SortByBranchName")
            {
                uoHiddenFieldSortBy.Value = "SortByBranchName";
                BindHotel();
            }
        }
        protected void uoObjectDataSourceHotel_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
        {

        }
        protected void uoHotelVendorList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        protected void uoHotelVendorListPager_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoButtonSavePriority_Click(object sender, EventArgs e)
        {
            SavePriority();
            BindHotel();
            AlertMessage("Save successfully.");
        }
        protected void uoHotelVendorList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HtmlControl PriorityTR = (HtmlControl)e.Item.FindControl("PriorityTR");
                TextBox uoTextBoxPriority = (TextBox)e.Item.FindControl("uoTextBoxPriority");
                 if ((User.IsInRole(TravelMartVariable.RoleAdministrator)
                         || User.IsInRole(TravelMartVariable.RoleCrewAssist)
                         || User.IsInRole(TravelMartVariable.RoleHotelSpecialist)) &&
                         uoDropDownListAirport.SelectedValue != "0" && uoTextBoxSearchAirport.Text.Trim() == ""
                         && uoDropDownListAirport.SelectedValue != "")
                {
                    HiddenField uoHiddenFieldBranchID = (HiddenField)e.Item.FindControl("uoHiddenFieldBranchID");
                    string scr = "validatePriority('" + uoHiddenFieldBranchID.Value + "', '" + uoTextBoxPriority.ClientID + "')";
                    uoTextBoxPriority.Attributes.Add("onblur", scr);
                }
                else
                {
                    PriorityTR.Attributes.Add("class", "hideElement");
                    uoTextBoxPriority.Visible = false;
                }
            }
        }
        /// <summary>
        /// Date Created:   13/03/2012
        /// Created By:     Josephine gad
        /// (description)   set the control settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoHotelVendorList_DataBound(object sender, EventArgs e)
        {
             ListView list = sender as ListView;
             if (list != null)
             {
                 HtmlControl PriorityTH = (HtmlControl)list.FindControl("PriorityTH");
                
                 if (list.Items.Count > 0)
                 {                     
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
                     }

                     //setup priority column
                     if ((User.IsInRole(TravelMartVariable.RoleAdministrator)
                         || User.IsInRole(TravelMartVariable.RoleCrewAssist)
                         || User.IsInRole(TravelMartVariable.RoleHotelSpecialist)) &&
                         uoDropDownListAirport.SelectedValue != "0" && uoTextBoxSearchAirport.Text.Trim() == ""
                         && uoDropDownListAirport.SelectedValue != "" &&
                         uoHotelVendorList.Items.Count > 0
                         )
                     {
                         uoHiddenFieldViewPriority.Value = "true";
                         uoButtonSavePriority.Visible = true;

                         if (PriorityTH != null)
                         {
                             PriorityTH.Style.Add("display", "inline");
                         }                         
                     }
                     else
                     {
                         uoButtonSavePriority.Visible = false;
                         uoHiddenFieldViewPriority.Value = "false";

                         if (PriorityTH != null)
                         {
                             PriorityTH.Style.Add("display", "none");
                         }
                     }
                 }
                 else
                 {
                     uoButtonSavePriority.Visible = false;
                     uoHiddenFieldViewPriority.Value = "false";
                     if (PriorityTH != null)
                     {
                         PriorityTH.Style.Add("display", "none");
                     }
                 }                 
             }
        }       
        #endregion
        
        #region "Functions"
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
                ListItem item = new ListItem("--SELECT REGION--", "0");
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
        /// <summary>
        /// Date Created:   27/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Bind Country
        /// </summary>
        //private void BindCountry()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, "");
        //        uoDropDownListCountry.Items.Clear();
        //        uoDropDownListCountry.DataSource = dt;
        //        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
        //        uoDropDownListCountry.DataValueField = "colCountryIDInt";
        //        uoDropDownListCountry.DataBind();
        //        uoDropDownListCountry.Items.Insert(0, new ListItem("--SELECT COUNTRY--", "0"));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   19/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Bind Airport
        /// -----------------------------------
        /// Date Modified:   07/Feb/2014
        /// Modified By:     Josephine Gad
        /// (description)    Change DataSource from dt to loop to add airport code
                /// </summary>
        private void BindAirport()
        {
            DataTable dt = null;
            try
            {
                dt = AirBLL.GetAirportByCountry("0", uoDropDownListRegion.SelectedValue, uoTextBoxSearchAirport.Text, "0");
                uoDropDownListAirport.Items.Clear();
                //uoDropDownListAirport.DataSource = dt;
                //uoDropDownListAirport.DataTextField = "colAirportNameVarchar";
                //uoDropDownListAirport.DataValueField = "colAirportIDInt";
                //uoDropDownListAirport.DataBind();
                ListItem item;
                string sValue;
                string sText;
                uoDropDownListAirport.Items.Insert(0, new ListItem("--SELECT AIRPORT--", "0"));

                int iTotal = dt.Rows.Count;
                for (int i = 0; i < iTotal; i++)
                {
                    sText = GlobalCode.Field2String(dt.Rows[i]["colAirportCodeVarchar"]).Trim() + "-" +
                            GlobalCode.Field2String(dt.Rows[i]["colAirportNameVarchar"]).Trim();
                    sValue = GlobalCode.Field2String(dt.Rows[i]["colAirportIDInt"]);
                    item = new ListItem(sText, sValue);
                    uoDropDownListAirport.Items.Add(item);
                }

                if (uoDropDownListAirport.Items.FindByValue(uoHiddenFieldAirport.Value) != null)
                {
                    uoDropDownListAirport.SelectedValue = uoHiddenFieldAirport.Value;
                }
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
        /// <summary>
        /// Date Created:   24/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Branch List by User, Region, Country, Airport
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void BindHotel()
        {
            try
            {
                uoHotelVendorList.DataSource = null;
                uoHotelVendorList.DataSourceID = "uoObjectDataSourceHotel";
                uoHotelVendorList.DataBind();

                
               // uoHotelVendorList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelAirportViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "View branch button for hotel airport view clicked.";
            strFunction = "HotelAirportViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// <summary>
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Remove Airport in Seaport
        /// </summary>
        /// <param name="sAirport"></param>
        /// <param name="sBranch"></param>
        private void DeleteHotelInAirport(string sAirport, string sBranch)
        { 
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Delete button from list clicked.";
            strFunction = "DeleteHotelInAirport";

            MaintenanceViewBLL.RemoveHotelInAirport(uoHiddenFieldUser.Value, sAirport, sBranch, strLogDescription,
                strFunction, Path.GetFileName(Request.Path));
        }
        /// <summary>
        /// Date Created:      28/12/2011
        /// Created By:        Josephine Gad
        /// (description)      Save Priority
        /// </summary>
        private void SavePriority()
        {
            foreach (ListViewItem item in uoHotelVendorList.Items)
            {
                string strLogDescription;
                string strFunction;

                HiddenField uoHiddenFieldBranchID = (HiddenField)item.FindControl("uoHiddenFieldBranchID");
                TextBox uoTextBoxPriority = (TextBox)item.FindControl("uoTextBoxPriority");

                if (uoHiddenFieldBranchID != null)
                {
                    Int32 AirportHotelID = MaintenanceViewBLL.SaveHotelPriority(uoDropDownListAirport.SelectedValue, uoHiddenFieldBranchID.Value, 
                        uoTextBoxPriority.Text, uoHiddenFieldUser.Value, uoDropDownListRoomType.SelectedValue);

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Hotel branch priority updated.";
                    strFunction = "SavePriority";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(AirportHotelID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
            }
        }
        /// <summary>
        /// Date Created:      28/12/2011
        /// Created By:        Josephine Gad
        /// (description)      Show popup message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "scr", sScript, false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        #endregion                  
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.Office.Interop;

using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Text;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class ManifestSearchView : System.Web.UI.Page
    {
        #region "Events"

        private SeafarerTravelBLL BLL = new SeafarerTravelBLL();
        ManifestBLL manifestBLL = new ManifestBLL();
        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                    to avoid error in date conversion
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = GlobalCode.Field2String(Session["UserName"]);
            if (userName == "")
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {                
                if (Request.QueryString["dt"] != null)
                {
                    if (Request.QueryString["dt"].ToString() != "")
                    {                        
                        uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                        uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                    }                    
                }
                Session["strPrevPage"] = Request.RawUrl;

                

                string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(userName);
                string userRole = UserRolePrimary;
               
                uoHiddenFieldRole.Value = userRole;
                uoHiddenFieldUser.Value = userName;
                uoHiddenFieldUserId.Value = userName;
                uoHiddenFieldUserRole.Value = userRole;

                uoHiddenFieldSeafarerID.Value = Session["strSeafarerID"].ToString();
                uoHiddenFieldSeafarerLN.Value = Session["strSeafarerLN"].ToString();
                uoHiddenFieldSeafarerFN.Value = Session["strSeafarerFN"].ToString();
                uoHiddenFieldRecLoc.Value = Session["strRecLoc"].ToString();

                uoHiddenFieldVesselCode.Value = Session["strVesselCode"].ToString();
                uoHiddenFieldVesselName.Value = Session["strVesselName"].ToString();

                SetDefaultValues();
                //GetVessel();
                //GetNationality();
                //GetGender();
                //GetRank();

                if (User.IsInRole(TravelMartVariable.RoleAdministrator)
                    || User.IsInRole(TravelMartVariable.RoleCrewAssist)
                    || User.IsInRole(TravelMartVariable.RoleCrewAdmin)
                    || User.IsInRole(TravelMartVariable.RolePortSpecialist)
                    || User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                {
                    uoButtonRequest.Visible = true;
                }
                else
                {
                    uoButtonRequest.Visible = false;
                }

                if (userRole == TravelMartVariable.RoleHotelVendor || userRole == TravelMartVariable.RoleVehicleVendor)
                {
                    uoTRVessel.Visible = false;
                }
                else
                {
                    uoTRVessel.Visible = true;
                }
               
                HtmlControl uoRowDateTo = (HtmlControl)Master.FindControl("uoRowDateTo");
                uoRowDateTo.Visible = false;
            }
            if (uoHiddenFieldManifest.Value == "1")
            {
                GetTravelManifestWithCount();
            }
            uoHiddenFieldManifest.Value = "0";

        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoHiddenFieldLoadType.Value = "2";

            uoObjectDataSourceManifestSearchView.SelectParameters["LoadType"].DefaultValue = uoHiddenFieldLoadType.Value;
            //uoObjectDataSourceManifestSearchView.SelectParameters["FromDate"].DefaultValue = uoHiddenFieldDate.Value;
            //uoObjectDataSourceManifestSearchView.SelectParameters["ToDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["CurrentDate"].DefaultValue = uoHiddenFieldDate.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["UserID"].DefaultValue = uoHiddenFieldUser.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["Role"].DefaultValue = uoHiddenFieldUserRole.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["OrderBy"].DefaultValue = uoHiddenFieldOrderBy.Value;
                        
            uoObjectDataSourceManifestSearchView.SelectParameters["SeafarerID"].DefaultValue = uoHiddenFieldSeafarerID.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["SeafarerLN"].DefaultValue = uoHiddenFieldSeafarerLN.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["SeafarerFN"].DefaultValue = uoHiddenFieldSeafarerFN.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["RecordLocator"].DefaultValue = uoHiddenFieldRecLoc.Value;

            uoObjectDataSourceManifestSearchView.SelectParameters["VesselCode"].DefaultValue = uoHiddenFieldVesselCode.Value;
            uoObjectDataSourceManifestSearchView.SelectParameters["VesselName"].DefaultValue = uoHiddenFieldVesselName.Value;
                        
            uoObjectDataSourceManifestSearchView.SelectParameters["RegionID"].DefaultValue = GlobalCode.Field2String(Session["Region"]);
            uoObjectDataSourceManifestSearchView.SelectParameters["CountryID"].DefaultValue = GlobalCode.Field2String(Session["Country"]);
            uoObjectDataSourceManifestSearchView.SelectParameters["CityID"].DefaultValue = GlobalCode.Field2String(Session["City"]);
            uoObjectDataSourceManifestSearchView.SelectParameters["PortID"].DefaultValue = GlobalCode.Field2String(Session["Port"]);
            uoObjectDataSourceManifestSearchView.SelectParameters["HotelID"].DefaultValue = GlobalCode.Field2String(Session["Hotel"]);

            GetTravelManifestWithCount();
        }
        protected void uoListViewManifest_PreRender(object sender, EventArgs e)
        {           
        }
        protected void uoListViewManifest_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName.ToString();
            if (e.CommandName != "")
            {
                GetTravelManifestWithCount();
            }
        }
        protected void uoObjectDataSourceManifestSearchView_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["LoadType"] = GlobalCode.Field2String(uoHiddenFieldLoadType.Value);
            e.InputParameters["CurrentDate"] = uoHiddenFieldDate.Value;
            e.InputParameters["UserID"] = uoHiddenFieldUser.Value;
            e.InputParameters["Role"] = uoHiddenFieldUserRole.Value;
            e.InputParameters["OrderBy"] = uoHiddenFieldOrderBy.Value;
            
            e.InputParameters["SeafarerID"] = uoHiddenFieldSeafarerID.Value;
            e.InputParameters["SeafarerLN"] = uoHiddenFieldSeafarerLN.Value;
            e.InputParameters["SeafarerFN"] = uoHiddenFieldSeafarerFN.Value;
            e.InputParameters["RecordLocator"] = uoHiddenFieldRecLoc.Value;

            e.InputParameters["VesselCode"] = uoHiddenFieldVesselCode.Value;
            e.InputParameters["VesselName"] = uoHiddenFieldVesselName.Value;

            e.InputParameters["RegionID"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["CountryID"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["CityID"] = GlobalCode.Field2String(Session["City"]);
            e.InputParameters["PortID"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["HotelID"] = GlobalCode.Field2String(Session["Hotel"]);
        }

        protected void uoLinkButtonReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Report.aspx?rpt=Manifest");
        }

        protected void uoButtonSaveBarcode_Click(object sender, EventArgs e)
        {
        }
      
        public override void VerifyRenderingInServerForm(Control control)
        {
            //Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }
        /// <summary>
        /// Date Created:     27/03/2012
        /// Created By:       Josephine Gad
        /// (description)     Set Remarks URL        
        /// ========================================================        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoListViewManifest_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField uoHiddenFieldRemarksURL = (HiddenField)e.Item.FindControl("uoHiddenFieldRemarksURL");
                HiddenField uoHiddenFieldRemarksParameter = (HiddenField)e.Item.FindControl("uoHiddenFieldRemarksParameter");
                HyperLink uoHyperLinkRemarks = (HyperLink)e.Item.FindControl("uoHyperLinkRemarks");
                Label uoLabelRemarks = (Label)e.Item.FindControl("uoLabelRemarks");
                string sURL = "";
                if (uoHiddenFieldRemarksURL.Value == "2")
                {
                    sURL = "Hotel/HotelOverflowBooking3.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldRemarksParameter.Value;
                }
                else if (uoHiddenFieldRemarksURL.Value == "3")
                {
                    sURL = "Hotel/HotelExceptionBookings.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldRemarksParameter.Value;
                }
                else if (uoHiddenFieldRemarksURL.Value == "4")
                {
                    sURL = "NoTravelRequest2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldRemarksParameter.Value;
                }
                if (sURL == "")
                {
                    uoLabelRemarks.Visible = true;
                    uoHyperLinkRemarks.Visible = false;
                }
                else
                {
                    uoLabelRemarks.Visible = false;
                    uoHyperLinkRemarks.Visible = true;

                    uoHyperLinkRemarks.NavigateUrl = sURL;
                }
            }
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set default values of global variables
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void SetDefaultValues()
        {           
            if (GlobalCode.Field2String(Session["Region"]) == "")
            {
                Session["strPendingFilter"] = "0";
                Session["Region"] = "0";
                Session["Country"] = "0";
                Session["City"] = "0";
                Session["Port"] = "0";
                Session["Hotel"] = "0";
                Session["Vehicle"] = "0";
            }            
        }

        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list
        /// ---------------------------------------------------------------------------
        /// </summary>
        //private void GetVessel()
        //{
        //    DataTable VesselDataTable = null;
        //    try
        //    {
        //        VesselDataTable = VesselBLL.GetVessel(uoHiddenFieldUser.Value, uoHiddenFieldDate.Value,
        //            uoHiddenFieldDate.Value, GlobalCode.Field2String(Session["Region"]),  GlobalCode.Field2String(Session["Country"]),
        //            GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value);
        //        uoDropDownListVessel.Items.Clear();
        //        ListItem item = new ListItem("--Select Vessel--", "0");
        //        uoDropDownListVessel.Items.Add(item);
        //        uoDropDownListVessel.DataSource = VesselDataTable;
        //        uoDropDownListVessel.DataTextField = "VesselName";
        //        uoDropDownListVessel.DataValueField = "VesselID";
        //        uoDropDownListVessel.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (VesselDataTable != null)
        //        {
        //            VesselDataTable.Dispose();
        //        }
        //    }
        //}
        
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Manifest List  with count      
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetTravelManifestWithCount()
        {
            try
            {

                uoListViewManifest.DataSource = null;
                uoListViewManifest.DataSourceID = "uoObjectDataSourceManifestSearchView";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of nationality
        /// </summary>
        //private void GetNationality()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = MasterfileBLL.GetReference("Nationality");
        //        ListItem item = new ListItem("--Select Nationality--", "0");
        //        uoDropDownListNationality.Items.Clear();
        //        uoDropDownListNationality.Items.Add(item);
        //        uoDropDownListNationality.DataSource = dt;
        //        uoDropDownListNationality.DataTextField = "RefName";
        //        uoDropDownListNationality.DataValueField = "RefID";
        //        uoDropDownListNationality.DataBind();
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
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of gender
        /// </summary>
        //private void GetGender()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = MasterfileBLL.GetReference("Gender");
        //        ListItem item = new ListItem("--Select Gender--", "0");
        //        uoDropDownListGender.Items.Clear();
        //        uoDropDownListGender.Items.Add(item);
        //        uoDropDownListGender.DataSource = dt;
        //        uoDropDownListGender.DataTextField = "RefName";
        //        uoDropDownListGender.DataValueField = "RefID";
        //        uoDropDownListGender.DataBind();
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
        /// Date Created:   13/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of rank
        /// </summary>
        //private void GetRank()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = SeafarerTravelBLL.GetRankByVessel(uoDropDownListVessel.SelectedValue);
        //        ListItem item = new ListItem("--Select Rank--", "0");
        //        uoDropDownListRank.Items.Clear();
        //        uoDropDownListRank.Items.Add(item);
        //        uoDropDownListRank.DataSource = dt;
        //        uoDropDownListRank.DataTextField = "RankName";
        //        uoDropDownListRank.DataValueField = "RankID";
        //        uoDropDownListRank.DataBind();
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
        /// Date Created:   29/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get the directory of image status            
        /// <summary>
        protected string GetStatusImage(object StatusObj)
        {
            string ImageString = "~/Images/";
            string StatusString = StatusObj.ToString();
            StatusString = StatusString.ToLower();

            switch (StatusString)
            {
                case "ok":
                    ImageString += "positive.png";
                    break;
                case "notok":
                    ImageString += "positive-red.png";
                    break;
                case "none":
                    ImageString += "neutral.png";
                    break;
                default:
                    ImageString += "neutral.png";
                    break;
            }
            return ImageString;
        }

        /// <summary>
        /// Date Created:   09/01/2012
        /// Created By:     Muhallidin G Wali
        /// (description)   
        protected short getDefualtLoadType()
        {
            //Get the data field value of interest for this row            
            short LoadType;

            switch (GlobalCode.Field2String(Session["UserName"]))
            {
                case "Vehicle Vendor":
                case "Hotel Vendor":
                    LoadType = 0;
                    break;
                default:
                    LoadType = 1;
                    break;

            }

            return LoadType;
        }        

        /// <summary>
        /// Date Created:   08/02/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Set manifest search view groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string ManifestSearchViewAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Ship";
            string GroupValueString = "Vessel";

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
                if (Eval("IsWithSail").ToString() == "True")
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"16\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
                else
                {
                    return string.Format("<tr><td class=\"groupRed\" colspan=\"16\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }

        /// <summary>
        /// Date Created:   24/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Show/Hide Scan button
        /// </summary>
        /// <param name="TravelReqID"></param>
        /// <param name="ManualReqID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        protected bool ShowScanBtn(object TravelReqID, object ManualReqID)
        {
            bool bReturn = false;
            bReturn = SeafarerTravelBLL.IsLOEScanned(TravelReqID.ToString(), ManualReqID.ToString(), uoHiddenFieldUser.Value);
            return bReturn;
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   03/02/2012
        /// Description:    Set default values
        /// </summary>
        /// <param name="LoadType"></param>
        protected void BindManifest(Int16 LoadType)
        {            
            //manifestBLL.LoadAllManifestSearchViewTables(LoadType,
            //    Convert.ToDateTime(uoHiddenFieldDate.Value),
            //    uoHiddenFieldUser.Value,
            //    uoHiddenFieldUserRole.Value,
            //    uoHiddenFieldOrderBy.Value,
            //    0,
            //    20, 
            //    uoHiddenFieldSeafarerID.Value,
            //    uoHiddenFieldSeafarerLN.Value,
            //    uoHiddenFieldSeafarerFN.Value,
            //    uoHiddenFieldRecLoc.Value,
            //    uoHiddenFieldVesselCode.Value,
            //    uoHiddenFieldVesselName.Value,
            //    GlobalCode.Field2Int(GlobalCode.Field2String(Session["Region"])),
            //    GlobalCode.Field2Int(GlobalCode.Field2String(Session["Country"])),
            //    GlobalCode.Field2Int(GlobalCode.Field2String(Session["City"])),
            //    GlobalCode.Field2Int(GlobalCode.Field2String(Session["Port"])),
            //    GlobalCode.Field2Int(GlobalCode.Field2String(Session["Hotel"]))
            //    );
            //uoObjectDataSourceManifestSearchView.TypeName = "TRAVELMART.Common.ManifestSearchViewDTO";
            //uoObjectDataSourceManifestSearchView.SelectCountMethod = "GetManifestSearchViewListCount";
            //uoObjectDataSourceManifestSearchView.SelectMethod = "GetManifestSearchViewList";

            //uoListViewManifest.DataSourceID = uoObjectDataSourceManifestSearchView.UniqueID;
        }
        #endregion        
    }    
}

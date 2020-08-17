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
    public partial class HotelAirportBrandView : System.Web.UI.Page
    {
        #region EVENTS
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   02/Jul/2014
        /// Description:    Airport-Brand Assignment of Hotel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                uoHiddenFieldClassHideVisible.Value = "hideElement";
                uoHiddenFieldEditVisible.Value = "hideElement";
                uoHiddenFieldContractListVisible.Value = "hideElement";


                if (User.IsInRole(TravelMartVariable.RoleAdministrator))
                {
                    uoHiddenFieldClassHideVisible.Value = "";
                    uoHiddenFieldEditVisible.Value = "";
                    uoHiddenFieldContractListVisible.Value = "";
                }
                else if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                {
                    uoHiddenFieldContractListVisible.Value = "";
                }
                else if (User.IsInRole(TravelMartVariable.RoleHotelVendor))
                {
                    uoHiddenFieldEditVisible.Value = "";
                }
                
                BindPage();
              
            }
            uoListViewHeader.DataSource = null;
            uoListViewHeader.DataBind();

            if (uoHiddenFieldPopup.Value == "1")
            {
                uoHiddenFieldPopup.Value = "0";
            }
        }
        protected void uoButtonSavePriority_Click(object sender, EventArgs e)
        {
            SavePriority();
        }
        //protected void uoBtnHotelAdd_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("HotelMaintenanceBranch.aspx?st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&vmId=0&vmType=HO&dt=" + Request.QueryString["dt"]);
        //}

        //protected void uoButtonSearch_Click(object sender, EventArgs e)
        //{
        //    if (uoDropDownListRegion.SelectedValue == "0")
        //    {
        //        //BindCountry();
        //        //uoDropDownListCountry.SelectedValue = "0";
        //        //BindAirport();
        //        //BindCity();
        //        uoDropDownListAirport.SelectedValue = "0";

        //        Session["Region"] = "0";
        //        Session["Country"] = "0";
        //        Session["Airport"] = "0";
        //    }
        //    BindBranch();

        //    string strLogDescription;
        //    string strFunction;

        //    //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
        //    strLogDescription = "Search button for hotel branch view clicked.";
        //    strFunction = "uoButtonSearch_Click";

        //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

        //    AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        //}
        protected void uoListViewHeader_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldOrderBy.Value = e.CommandName;
            Session.Remove("HotelAirportBrand_Hotel");
            BindBranch();
        }
        protected void uoListViewHotel_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //if (e.CommandName == "SortByBranchName")
            //{
            //    uoHiddenFieldSortByBranch.Value = "1";
            //    uoHiddenFieldSortByPriority.Value = "2";
            //}
            //else if (e.CommandName == "SortByPriority")
            //{
            //    uoHiddenFieldSortByBranch.Value = "2";
            //    uoHiddenFieldSortByPriority.Value = "1";
            //}
            //if (e.CommandName != "")
            //{
            //    uoHiddenFieldOrderBy.Value = e.CommandName;
            //    BindBranch();
            //}
        }

        protected void uoListViewHotel_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoListViewHotelPager_PreRender(object sender, EventArgs e)
        {

            //BindBranch();
        }
        protected void uoButtonViewBranch_Click(object sender, EventArgs e)
        {
            Session.Remove("HotelAirportBrand_Hotel");
            //uoDropDownListAirport.SelectedValue = uoHiddenFieldAirport.Value;
            Session["Brand"] = uoDropDownListBrand.SelectedValue;
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["Airport"] = uoDropDownListAirport.SelectedValue;
            Session["RoomType"] = uoDropDownListRoom.SelectedValue;
            BindBranch();
        }

        //protected void uoButtonSavePriority_Click(object sender, EventArgs e)
        //{
        //    SavePriority();
        //    BindBranch();
        //    AlertMessage("Save successfully.");
        //}

        protected void uoListViewHotel_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                TextBox uoTextBoxPriority = (TextBox)e.Item.FindControl("uoTextBoxPriority");
                if (uoTextBoxPriority.Visible == true)
                {
                    HiddenField uoHiddenFieldBranchID = (HiddenField)e.Item.FindControl("uoHiddenFieldBranchID");
                    string scr = "validatePriority('" + uoHiddenFieldBranchID.Value + "', '" + uoTextBoxPriority.ClientID + "')";
                    uoTextBoxPriority.Attributes.Add("onblur", scr);
                }
            }
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAirport(1);
        }

        protected void uoButtonFilterAirport_Click(object sender, EventArgs e)
        {
            BindAirport(1);
        }
        protected void uoListViewHotel_DataBound(object sender, EventArgs e)
        {
            //HtmlControl TDEdit = (HtmlControl)uoListViewHotel.FindControl("TDEdit");
            //HtmlControl TDEditAssignment = (HtmlControl)uoListViewHotel.FindControl("TDEditAssignment");
            //HtmlControl TdAdd = (HtmlControl)uoListViewHotel.FindControl("TdAdd");

            //if (TDEdit != null)
            //{
            //    if (User.IsInRole(TravelMartVariable.RoleAdministrator))
            //    {
            //        TDEdit.Visible = true;
            //        TDEditAssignment.Visible = true;
            //        TdAdd.Visible = true;
            //    }
            //    else
            //    {
            //        TDEdit.Visible = false;
            //        TDEditAssignment.Visible = false;
            //        TdAdd.Visible = false;
            //    }
            //}
        }

        protected void uoListViewHotel_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            Session.Remove("HotelAirportBrand_Hotel");
        }
        protected void uoListViewHeader_DataBound(object sender, EventArgs e)
        {


        }
        protected void uoListViewHeader_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }
        protected void uoListViewHeader_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.EmptyItem)
            {
                int iAirport = GlobalCode.Field2Int(uoDropDownListAirport.SelectedValue);
                int iBrand = GlobalCode.Field2Int(uoDropDownListBrand.SelectedValue);
                int iRoom = GlobalCode.Field2Int(uoDropDownListRoom.SelectedValue);

                HtmlControl PriorityTH = (HtmlControl)e.Item.FindControl("PriorityTH");

                HtmlControl THEditHotel = (HtmlControl)e.Item.FindControl("THEditHotel");
                HtmlControl THEditAssignment = (HtmlControl)e.Item.FindControl("THEditAssignment");
                HtmlControl THAdd = (HtmlControl)e.Item.FindControl("THAdd");
                HtmlControl THContractList = (HtmlControl)e.Item.FindControl("THContractList");

                if (THEditHotel != null)
                {
                    if (User.IsInRole(TravelMartVariable.RoleAdministrator))
                    {
                        THEditHotel.Visible = true;
                        THEditAssignment.Visible = true;
                        THAdd.Visible = true;
                        THContractList.Visible = true;
                    }
                    else if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                    {
                        THEditHotel.Visible = false;
                        THEditAssignment.Visible = false;
                        THAdd.Visible = false;
                        THContractList.Visible = true;
                    }
                    else if (User.IsInRole(TravelMartVariable.RoleHotelVendor))
                    {
                        THEditHotel.Visible = true;
                        THEditAssignment.Visible = false;
                        THAdd.Visible = false;
                        THContractList.Visible = false;
                    }
                    else
                    {
                        THEditHotel.Visible = false;
                        THEditAssignment.Visible = false;
                        THAdd.Visible = false;
                        THContractList.Visible = false;
                    }
                }

                if (PriorityTH != null)
                {

                    if (iAirport > 0 &&
                       iBrand > 0 &&
                       iRoom > 0 && User.IsInRole(TravelMartVariable.RoleAdministrator))
                    {
                        PriorityTH.Visible = true;
                        uoButtonSavePriority.Visible = true;
                    }
                    else
                    {
                        PriorityTH.Visible = false;
                        uoButtonSavePriority.Visible = false;
                    }
                }
            }
        }
        protected void uoBtnHotelAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("HotelMaintenanceBranch.aspx?st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&vmId=0&vmType=HO&dt=" + Request.QueryString["dt"]);
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
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }

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


        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "scr", sScript, false);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }


        /// <summary>
        /// Date Created:   01/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind the details inside this page
        /// </summary>
        private void BindPage()
        {
            int iRegionID = GlobalCode.Field2Int(Session["Region"]);
            int iAirportID = GlobalCode.Field2Int(uoDropDownListAirport.SelectedValue);
            Int16 iRoomTypeID = GlobalCode.Field2TinyInt(uoDropDownListRoom.SelectedValue);
            int iBrandID = GlobalCode.Field2Int(uoDropDownListBrand.SelectedValue);


            MaintenanceViewBLL BLL = new MaintenanceViewBLL();
            BLL.GetHotelVendorBranchListByBrand(iRegionID, iAirportID, uoTextBoxAirport.Text.Trim(),
                iRoomTypeID, iBrandID, uoTextBoxSearchParam.Text.Trim(),
                uoHiddenFieldOrderBy.Value, 0, uoHiddenFieldUser.Value, 0, uoListViewHotelPager.PageSize);

            BindRegion();
            BindBrand();
            BindAirport(0);
            BindRoomType();

            //BindBranch();            
            uoListViewHotel.DataSourceID = "uoObjectDataSourceHotel";

        }
        private void BindBranch()
        {
            uoListViewHotel.DataSource = null;
            uoListViewHotel.DataBind();
        }
        /// <summary>
        /// Date Created:   02/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Refresh Branch List based from filter
        /// </summary>
        public List<VendorHotelList> GetBranch(int iRegionID, int iAirportID, string sAirportName,
            Int16 iRoomTypeID, int iBrandID, string sBranchFilter, string sOrderBy,
            Int16 iLoadType, string sUserID, int iStartRow, int iMaxRow)
        {

            if (Session["HotelAirportBrand_Hotel"] == null)
            {
                sAirportName = GlobalCode.Field2String(sAirportName);
                sBranchFilter = GlobalCode.Field2String(sBranchFilter);

                MaintenanceViewBLL BLL = new MaintenanceViewBLL();
                BLL.GetHotelVendorBranchListByBrand(iRegionID, iAirportID, sAirportName.Trim(),
                    iRoomTypeID, iBrandID, sBranchFilter.Trim(),
                    sOrderBy, iLoadType, sUserID, iStartRow, iMaxRow);
            }

            List<VendorHotelList> listHotel = new List<VendorHotelList>();

            listHotel = (List<VendorHotelList>)Session["HotelAirportBrand_Hotel"];
            return listHotel;
        }
        /// <summary>
        /// Date Created:   02/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Get count of hotels
        /// </summary>
        public int GetBranchCount(int iRegionID, int iAirportID, string sAirportName,
          Int16 iRoomTypeID, int iBrandID, string sBranchFilter, string sOrderBy,
          Int16 iLoadType, string sUserID)
        {
            int iCount = 0;
            iCount = GlobalCode.Field2Int(Session["HotelAirportBrand_HotelCount"]);
            return iCount;
        }
        /// <summary>
        /// Date Created:   01/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Region
        /// </summary>
        private void BindRegion()
        {
            List<RegionList> listRegion = new List<RegionList>();
            if (Session["HotelAirportBrand_Region"] != null)
            {
                listRegion = (List<RegionList>)Session["HotelAirportBrand_Region"];
            }
            uoDropDownListRegion.Items.Clear();
            ListItem item = new ListItem("--Select Region--", "0");
            uoDropDownListRegion.Items.Add(item);

            item = new ListItem("--No Region--", "-1");
            uoDropDownListRegion.Items.Add(item);

            item = new ListItem("--With Region--", "-2");
            uoDropDownListRegion.Items.Add(item);

            uoDropDownListRegion.DataSource = listRegion;
            uoDropDownListRegion.DataTextField = "RegionName";
            uoDropDownListRegion.DataValueField = "RegionId";
            uoDropDownListRegion.DataBind();

            string sRegion = GlobalCode.Field2String(Session["Region"]);
            if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
            {
                uoDropDownListRegion.SelectedValue = sRegion;
            }
        }
        /// <summary>
        /// Date Created:   01/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Brand
        /// </summary>
        private void BindBrand()
        {
            List<BrandList> listBrand = new List<BrandList>();
            if (Session["HotelAirportBrand_Brand"] != null)
            {
                listBrand = (List<BrandList>)Session["HotelAirportBrand_Brand"];
            }
            uoDropDownListBrand.Items.Clear();
            ListItem item = new ListItem("--Select Brand--", "0");
            uoDropDownListBrand.Items.Add(item);

            uoDropDownListBrand.DataSource = listBrand;
            uoDropDownListBrand.DataTextField = "BrandName";
            uoDropDownListBrand.DataValueField = "BrandID";
            uoDropDownListBrand.DataBind();

            string sBrand = GlobalCode.Field2String(Session["Brand"]);
            if (uoDropDownListBrand.Items.FindByValue(sBrand) != null)
            {
                uoDropDownListBrand.SelectedValue = sBrand;
            }
        }
        /// <summary>
        /// Date Created:   01/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Airport
        /// </summary>
        private void BindAirport(Int16 iLoadType)
        {
            List<AirportDTO> listAirport = new List<AirportDTO>();
            if (iLoadType == 0)
            {
                if (Session["HotelAirportBrand_Airport"] != null)
                {
                    listAirport = (List<AirportDTO>)Session["HotelAirportBrand_Airport"];
                }
            }
            else
            {
                string sRegion = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue).ToString();
                listAirport = AirBLL.GetAirportByRegion(sRegion, uoTextBoxAirport.Text.Trim());
            }
            uoDropDownListAirport.Items.Clear();
            ListItem item = new ListItem("--Select Airport--", "0");
            uoDropDownListAirport.Items.Add(item);

            uoDropDownListAirport.DataSource = listAirport;
            uoDropDownListAirport.DataTextField = "AirportNameString";
            uoDropDownListAirport.DataValueField = "AirportIDString";
            uoDropDownListAirport.DataBind();

            string sAirport = GlobalCode.Field2String(Session["Airport"]);
            if (uoDropDownListAirport.Items.FindByValue(sAirport) != null)
            {
                uoDropDownListAirport.SelectedValue = sAirport;
            }
        }
        /// <summary>
        /// Date Created:   02/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Bind Room Type
        /// </summary>
        private void BindRoomType()
        {
            List<RoomType> listRoom = new List<RoomType>();
            if (Session["HotelAirportBrand_Room"] != null)
            {
                listRoom = (List<RoomType>)Session["HotelAirportBrand_Room"];
            }
            uoDropDownListRoom.Items.Clear();
            ListItem item = new ListItem("--Select Room Type--", "0");
            uoDropDownListRoom.Items.Add(item);

            uoDropDownListRoom.DataSource = listRoom;
            uoDropDownListRoom.DataTextField = "RoomName";
            uoDropDownListRoom.DataValueField = "RoomID";
            uoDropDownListRoom.DataBind();

            string sRoom = GlobalCode.Field2String(Session["RoomType"]);
            if (uoDropDownListRoom.Items.FindByValue(sRoom) != null)
            {
                uoDropDownListRoom.SelectedValue = sRoom;
            }
        }
        /// <summary>
        /// Date Created:   03/Jul/2014
        /// Created By:     Josephine Gad
        /// (description)   Save Priority
        /// </summary>
        private void SavePriority()
        {

            DataTable dt = null;
            try
            {
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();
                dt = new DataTable();

                DataColumn col = new DataColumn("colBrandAirHotelIDInt", typeof(int));
                dt.Columns.Add(col);

                col = new DataColumn("colBranchIDInt", typeof(int));
                dt.Columns.Add(col);

                col = new DataColumn("colBrandIdInt", typeof(int));
                dt.Columns.Add(col);

                col = new DataColumn("colAirportIDInt", typeof(int));
                dt.Columns.Add(col);

                col = new DataColumn("colPriorityTinyint", typeof(Int16));
                dt.Columns.Add(col);

                col = new DataColumn("colRoomIDInt", typeof(Int16));
                dt.Columns.Add(col);

                DataRow r;
                int iTotalRow = uoListViewHotel.Items.Count;

                HiddenField hBrandAirportHoteID;
                TextBox tPriority;

                //for (int i = 0; i < iTotalRow; i++ )
                foreach (ListViewItem item in uoListViewHotel.Items)
                {
                    hBrandAirportHoteID = (HiddenField)item.FindControl("uoHiddenFieldBrandAirportHoteID");
                    tPriority = (TextBox)item.FindControl("uoTextBoxPriority");

                    r = dt.NewRow();
                    r["colBrandAirHotelIDInt"] = GlobalCode.Field2Int(hBrandAirportHoteID.Value);
                    r["colPriorityTinyint"] = GlobalCode.Field2TinyInt(tPriority.Text);

                    dt.Rows.Add(r);
                }

                MaintenanceViewBLL.BrandAirportHotelSavePriority(uoHiddenFieldUser.Value,
                    "Save Brand Airport Hotel Priority", "SavePriority", Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, dt);

                AlertMessage("Priority successfully saved!");

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
        #endregion




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.Security;

namespace TRAVELMART
{
    public partial class CityView : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region EVENTS
        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// ------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  07/05/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   move page to new master page
        ///                 move session initialization to new function
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            if (!IsPostBack)
            {                
                uoHyperLinkCityAdd.HRef = "CityAdd.aspx?vcid=0";                
            }
            if (uoHiddenFieldPopupCity.Value == "1")
            {
                BindCity();
                uoHiddenFieldPopupCity.Value = "0";
            }
        }

       
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            uoHiddenFieldCity.Value = uoTextBoxSearchParam.Text;
        }

        protected void uoCityList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                uoHiddenFieldCityId.Value= e.CommandArgument.ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            }
        }

        protected void uoCityList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoCityListPager_PreRender(object sender, EventArgs e)
        {
            BindCity();

            //if (uoTextBoxSearchParam.Text != "")
            //{
            //    BindCity(sender);
              
            //}
            //else if( GlobalCode.Field2String(Session["Country"]) != "0")
            //{
            //    BindCity(uoButtonSearch);
            //    uoTextBoxSearchParam.Text = "";
            //}
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/05/2012
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
        }
        
        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: binds city to listview
        /// </summary>

        private void BindCity()//object sender
        {
            try
            {
                uoHiddenFieldCountryString.Value = GlobalCode.Field2String(Session["Country"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;

                uoCityList.Items.Clear();                
                uoCityList.DataSource = null;
                uoCityList.DataSourceID = "ObjectDataSource1";
                UpdatePanel4.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region DELETED
        ///// <summary>
        ///// Date Created: 03/10/2011
        ///// Created By: Charlene Remotigue
        ///// Description: binds all regions to dropdownlist
        ///// </summary>
        //private void BindRegion()
        //{
        //    DataTable RegionDataTable = null;
        //    try
        //    {
        //        RegionDataTable = MasterfileBLL.regionViewMasterfileSearch("");
        //        uoDropDownListRegion.Items.Clear();
        //        ListItem item = new ListItem("--Select Region--", "0");
        //        uoDropDownListRegion.Items.Add(item);
        //        uoDropDownListRegion.DataSource = RegionDataTable;
        //        uoDropDownListRegion.DataTextField = "colMapNameVarchar";
        //        uoDropDownListRegion.DataValueField = "colMapIDInt";
        //        uoDropDownListRegion.DataBind();

        //        item = new ListItem("--Select Country--", "0");
        //        uoDropDownListCountry.Items.Add(item);
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

        ///// <summary>
        ///// Date Created: 03/10/2011
        ///// Created By: Charlene Remotigue
        ///// Descrition: binds country to dropdownlist
        ///// </summary>

        //private void BindCountry()
        //{
        //    DataTable CountryDataTable = null;
        //    try
        //    {
        //        CountryDataTable = MasterfileBLL.GetSearchCountryListByRegion(uoDropDownListRegion.SelectedValue, "");
        //        uoDropDownListCountry.Items.Clear();
        //        ListItem item = new ListItem("--Select Country--", "0");
        //        uoDropDownListCountry.Items.Add(item);
        //        uoDropDownListCountry.DataSource = CountryDataTable;
        //        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
        //        uoDropDownListCountry.DataValueField = "colCountryIDInt";
        //        uoDropDownListCountry.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (CountryDataTable != null)
        //        {
        //            CountryDataTable.Dispose();
        //        }
        //    }
        //}
        #endregion
    }
}

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
using System.Web.Security;

namespace TRAVELMART
{
    public partial class RestrictedNationalityMaster : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region Events

        /// <summary>
        /// Date Created:   19/10/2012
        /// Created By:     Josephine Gad
        /// (description)   Restricted Nationalities maintenance
        /// ------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();

            //if(uoHiddenFieldPopupCountry.Value=="1")
            if (!IsPostBack)
            {
                BindCountry();
                uoNationalityList.DataSource = null;
                uoNationalityList.DataBind();

            }
        }
        //protected void uoButtonSearch_Click(object sender, EventArgs e)
        //{
        //    uoHiddenFieldSearch.Value = uoTextBoxSearchParam.Text;
        //}

        protected void uoNationalityList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (e.CommandName == "Delete")
            {
                int iID = Convert.ToInt32(e.CommandArgument);

                strLogDescription = "Restricted Nationality Deleted.";
                strFunction = "uoNationalityList_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();


                CountryBLL.DeleteRestrictedNationality(iID, strLogDescription, uoHiddenFieldUser.Value,
                    strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(dateNow), CommonFunctions.GetCurrentDateTime());

                //BLL.AuditTrailBLL.InsertLogAuditTrail(nationalityID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
                BindRestrictedNationality();
            }
        }

        protected void uoNationalityList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoNationalityListPager_PreRender(object sender, EventArgs e)
        {
            //BindCountry();
        }
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRestrictedNationality();
        }
        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
             string strLogDescription = "Restricted Nationality Added";
            string strFunction = "uoButtonAdd_Click";
            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            CountryBLL.SaveRestrictedNationality(GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
               GlobalCode.Field2Int(uoDropDownListNationality.SelectedValue), strLogDescription, uoHiddenFieldUser.Value,
               strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(dateNow), CommonFunctions.GetCurrentDateTime());

            BindRestrictedNationality();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/10/2012
        /// Description:    initialize session values
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
            uoHiddenFieldUser.Value = GlobalCode.Field2String( Session["UserName"]);
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/10/2012
        /// Descrition:     binds country to listview
        /// </summary>

        private void BindCountry()
        {
            try
            {
                List<Country> list = new List<Country>();
                
                if (list.Count == 0)
                {
                    CountryBLL.GetCountryList();
                }
                list = CountryGenericClass.CountryList;
                uoDropDownListCountry.Items.Clear();
                uoDropDownListCountry.DataSource = list;
                uoDropDownListCountry.DataTextField = "CountryName";
                uoDropDownListCountry.DataValueField = "CountryID";
                uoDropDownListCountry.DataBind();

                uoDropDownListCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));

                ////uoHiddenFieldRegionId.Value = GlobalCode.Field2String(Session["Region"]);
                //uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;

                //uoNationalityList.Items.Clear();
                //uoNationalityList.DataSource = null;
                //uoNationalityList.DataSourceID = "ObjectDataSource1";
                //UpdatePanel2.Update();
                ////uoTextBoxSearchParam.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   19/10/2012
        /// Descrition:     binds country to listview
        /// </summary>
        private void BindRestrictedNationality()
        {
            CountryBLL.GetRestrictedNationalityList(true, true, GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue));
            List<Nationality> restricted = (List<Nationality>)Session["NationalityGenericClass_RestNationalityList"];
            List<Nationality> nonRestricted = (List<Nationality>)Session["NationalityGenericClass_NonRestNationalityList"];

            uoDropDownListNationality.Items.Clear();
            uoDropDownListNationality.DataSource = nonRestricted;
            uoDropDownListNationality.DataTextField = "NationalityName";
            uoDropDownListNationality.DataValueField = "NationalityID";
            uoDropDownListNationality.DataBind();
            uoDropDownListNationality.Items.Insert(0, new ListItem("--Select Nationality--", "0"));

            uoButtonAdd.Visible = false;
            if (nonRestricted.Count > 0)
            {
                uoButtonAdd.Visible = true;            
            }

            uoNationalityList.DataSource = restricted;
            uoNationalityList.DataBind();
        }
        #endregion

        
    }
}

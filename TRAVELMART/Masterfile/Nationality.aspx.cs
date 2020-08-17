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
    public partial class Nationality : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region Events

        /// <summary>
        /// Date Created:   25/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Nationality page
        /// ------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();

            if (!IsPostBack)
            {
                BindNationality();
            }
        }       
        protected void uoNationalityList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            uoHiddenFieldOrderBy.Value = e.CommandName.ToString();
            if (e.CommandName != "")
            {
                BindNationality();
            }
            
            //string strLogDescription;
            //string strFunction;

            //if (e.CommandName == "Delete")
            //{
            //    int iID = Convert.ToInt32(e.CommandArgument);

            //    strLogDescription = "Restricted Nationality Deleted.";
            //    strFunction = "uoNationalityList_ItemCommand";

            //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();


            //    CountryBLL.DeleteRestrictedNationality(iID, strLogDescription, uoHiddenFieldUser.Value,
            //        strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(dateNow), CommonFunctions.GetCurrentDateTime());

            //    //BLL.AuditTrailBLL.InsertLogAuditTrail(nationalityID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
            //    //                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
            //    BindNationality();
            //}
        }

        protected void uoNationalityList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

        protected void uoNationalityListPager_PreRender(object sender, EventArgs e)
        {
            //BindCountry();
        }
        //protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindNationality();
        //}
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            // string strLogDescription = "Restricted Nationality Added";
            //string strFunction = "uoButtonAdd_Click";
            //DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            //CountryBLL.SaveRestrictedNationality(GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue),
            //   GlobalCode.Field2Int(uoDropDownListNationality.SelectedValue), strLogDescription, uoHiddenFieldUser.Value,
            //   strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(dateNow), CommonFunctions.GetCurrentDateTime());

            BindNationality();
        }
        protected void uoObjectDataSourceNationality_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["sFilter"] = uoTextBoxNationality.Text.Trim();            
            e.InputParameters["sSortedBy"] = uoHiddenFieldOrderBy.Value;
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
        /// Author:         Josephine Monteza
        /// Date Created:   25/Nov/2014
        /// Descrition:     binds country to listview
        /// </summary>
        private void BindNationality()
        {
            //CountryBLL.GetRestrictedNationalityList(true, true, GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue));
            //List<Nationality> restricted = (List<Nationality>)Session["NationalityGenericClass_RestNationalityList"];
            //List<Nationality> nonRestricted = (List<Nationality>)Session["NationalityGenericClass_NonRestNationalityList"];

            //uoDropDownListNationality.Items.Clear();
            //uoDropDownListNationality.DataSource = nonRestricted;
            //uoDropDownListNationality.DataTextField = "NationalityName";
            //uoDropDownListNationality.DataValueField = "NationalityID";
            //uoDropDownListNationality.DataBind();
            //uoDropDownListNationality.Items.Insert(0, new ListItem("--Select Nationality--", "0"));

            //uoButtonAdd.Visible = false;
            //if (nonRestricted.Count > 0)
            //{
            //    uoButtonAdd.Visible = true;            
            //}

            //uoNationalityList.DataSource = restricted;
            //uoNationalityList.DataBind();

            uoNationalityList.DataSource = null;
            uoNationalityList.DataSourceID = "uoObjectDataSourceNationality";
        }
        #endregion

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART.Maintenance
{
    public partial class ImmigrationCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            string userID = (Request.QueryString["uID"] != null) ? Request.QueryString["uID"] : "";

            if (!IsPostBack)
            {
                LoadImmigrationCompany(userID, true);
            }
        }

        void ClearObject()
        { 
        
            txtCompany.Text = "";
            txtEmailAdd.Text = "";
            txtPhoneNo.Text = "";
            txtAddress.Text = "";
            cboCity.SelectedIndex = -1;
            cboCountry.SelectedIndex = -1;

        }

        
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
            Session["strPrevPage"] = Request.RawUrl;

        }


        void LoadImmigrationCompany(string userid, bool isPostBack)
        {
            MaintenanceViewBLL BLL = new MaintenanceViewBLL();
            List<ImmigrationCompanyGenericClass > ImmigrationCom = new List<ImmigrationCompanyGenericClass>();
            ImmigrationCom = BLL.GetImmigationCompany(userid);
             
            uoListviewImmigrationCompany.DataSource = ImmigrationCom[0].ImmigrationCompany ;
            uoListviewImmigrationCompany.DataBind();


            if (isPostBack == true)
            { 
                cboCountry.Items.Clear();
                cboCountry.Items.Add(new ListItem("--Select Country--", "0"));

                cboCountry.DataSource = ImmigrationCom[0].Country.OrderBy(n => n.CountryName) ;
              
                cboCountry.DataTextField = "CountryName";
                cboCountry.DataValueField = "CountryID";
                cboCountry.DataBind();


                Session["Country"] = ImmigrationCom[0].Country;

                cboCity.Items.Clear();
                cboCity.Items.Add(new ListItem("--Select City--", "0"));
            
            }
           
             
        }


        protected void uoListviewImmigrationCompany_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        { 
        }
        protected void uoListviewImmigrationCompany_ItemEditing(object sender,  ListViewEditEventArgs e)
        {
        }

        protected void uoListviewImmigrationCompany_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (String.Equals(e.CommandName, "delete"))
            {
                MaintenanceViewBLL BLL = new MaintenanceViewBLL();
                List<ImmigrationCompany> ImmigrationCom = new List<ImmigrationCompany>();

                var list = BLL.DeleteImmigationCompany(GlobalCode.Field2Int(uoHiddenFieldImmigrationCompanyID.Value), (Request.QueryString["uID"] != null) ? Request.QueryString["uID"] : "");

                uoListviewImmigrationCompany.DataSource = list;
                uoListviewImmigrationCompany.DataBind();
            }
            else if (String.Equals(e.CommandName, "edit"))
            {
                Label lblCompanyName = (Label)e.Item.FindControl("lblCompanyName");  
                Label lblEmailAdd = (Label)e.Item.FindControl("lblEmailAdd");  
                Label lblContact = (Label)e.Item.FindControl("lblContact");  
                Label lblAddress = (Label)e.Item.FindControl("lblAddress");

                HiddenField HFImmigrationCompanyID = (HiddenField)e.Item.FindControl("HFImmigrationCompanyID");
                HiddenField HFCountryID = (HiddenField)e.Item.FindControl("HFCountryID");
                HiddenField HFCityID = (HiddenField)e.Item.FindControl("HFCityID");


                txtCompany.Text = lblCompanyName.Text;
                txtAddress.Text = lblAddress.Text;
                txtEmailAdd.Text = lblEmailAdd.Text;
                lblAddress.Text = lblAddress.Text;
                uoHiddenFieldImmigrationCompanyID.Value = HFImmigrationCompanyID.Value;



                cboCountry.SelectedIndex = GlobalCode.GetselectedIndexValue(cboCountry, HFCountryID.Value);


                List<Country> country = (List<Country>)Session["Country"];
                List<Country> City = country.Where(n => n.CountryID == GlobalCode.Field2Int(HFCountryID.Value)).OrderBy(a => a.CountryName).ToList();


                cboCity.Items.Clear();
                cboCity.Items.Add(new ListItem("--Select City--", "0"));


                cboCity.DataSource = City[0].City;
                cboCity.DataTextField = "CityName";
                cboCity.DataValueField = "CityId";
                cboCity.DataBind();

                cboCity.SelectedIndex = GlobalCode.GetselectedIndexValue(cboCity, HFCityID.Value);


 
            }


        }

        protected void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCountry.SelectedIndex > 0)
            {
                List<Country> country = (List<Country>)Session["Country"];

                 List<Country>  City = country.Where(n => n.CountryID == GlobalCode.Field2Int(cboCountry.SelectedValue)).OrderBy(a => a.CountryName ).ToList();


                cboCity.Items.Clear();

                cboCity.Items.Add(new ListItem("--Select City--", "0"));
           

                cboCity.DataSource = City[0].City;
                cboCity.DataTextField = "CityName";
                cboCity.DataValueField = "CityId";
                cboCity.DataBind();
            
            
            }
             

          
        
        }

        protected void btnSave_click(object sender, EventArgs e)
        {

            MaintenanceViewBLL BLL = new MaintenanceViewBLL();
            List<ImmigrationCompany> ImmigrationCom = new List<ImmigrationCompany>();
            var list = BLL.SaveImmigationCompany(GlobalCode.Field2Int(uoHiddenFieldImmigrationCompanyID.Value), GlobalCode.Field2Int(cboCountry.SelectedValue), GlobalCode.Field2Int(cboCity.SelectedValue), txtCompany.Text.ToString(), txtAddress.Text.ToString(), txtEmailAdd.Text.ToString(), txtPhoneNo.Text.ToString(), (Request.QueryString["uID"] != null) ? Request.QueryString["uID"] : "");
            uoListviewImmigrationCompany.DataSource = list;
            uoListviewImmigrationCompany.DataBind();
            
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   21/Jan/2015
        /// Descrption:     Check checkin date to cancel request.
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static List<CityList> GetCityList(string CountryID)
        {
            try
            {
             return   GetGenericCityLis(CountryID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private static List<CityList> GetGenericCityLis(string CountryID)
        {

            List<Country> country = (List<Country>)HttpContext.Current.Session["Country"];
            List<Country> City = country.Where(n => n.CountryID == GlobalCode.Field2Int(CountryID)).OrderBy(a => a.CountryName).ToList();
            ImmigrationCompany t = new ImmigrationCompany();
            return City[0].City;
        }

    }
}

using System;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

namespace TRAVELMART
{
    public partial class PortCompanyMaintenance : System.Web.UI.Page
    {
        #region Events
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
                if (Request.QueryString["cmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                PortVendorLogAuditTrail();
                CompanyCountryLoad();
                CompanyInfoLoad();
            }
        }

        /// <summary>
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (description) Save new port company            
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            Int32 PortAgentCompanyID = PortBLL.AddNewPortCompany(Convert.ToInt32(Request.QueryString["cmId"].ToString()), uoTextBoxCompanyName.Text, uoTextBoxAddress.Text, Convert.ToInt32(uoDropDownListCountry.SelectedValue),
                uoTextBoxPerson.Text, uoTextBoxContactNo.Text, GlobalCode.Field2String(Session["UserName"]));

            if (Request.QueryString["cmId"] == "0")
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Port vendor added.";
                strFunction = "uoButtonSave_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(PortAgentCompanyID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Port vendor updated.";
                strFunction = "uoButtonSave_Click";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(PortAgentCompanyID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            OpenParentPage();

        }

        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 24/08/2011
        /// Created By: Marco Abejar
        /// (decription) Loads company information     
        /// ------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private void CompanyInfoLoad()
        {
            if (Request.QueryString["cmId"].ToString() != "0")
            {
                IDataReader dr = PortBLL.GetPortCompanyToEdit(Int32.Parse(Request.QueryString["cmId"].ToString()));
                if (dr.Read())
                {
                    uoTextBoxCompanyName.Text = dr["company"].ToString();
                    uoTextBoxAddress.Text = dr["address"].ToString();
                    uoDropDownListCountry.SelectedValue = dr["colCountryIDInt"].ToString();
                    uoTextBoxContactNo.Text = dr["colMainContactNumberVarchar"].ToString();
                    uoTextBoxPerson.Text = dr["colContactPerson"].ToString();
                }
            }
        }

        private void CompanyCountryLoad()
        {
            /// <summary>
            /// Date Created: 29/07/2011
            /// Created By: Gabriel Oquialda
            /// (description) Loads vendor city to dropdownlist            
            /// </summary>

            DataTable dt = new DataTable();
            dt = CountryBLL.CountryList();
            if (dt.Rows.Count > 0)
            {
                uoDropDownListCountry.DataSource = dt;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("- Select Country -", ""));
            }
            else
            {
                uoDropDownListCountry.DataBind();
            }
        }

        private void OpenParentPage()
        {
            /// <summary>
            /// Date Created: 19/07/2011
            /// Created By: Josephine Gad
            /// (description) Close this page and update parent page
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupPort\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        private void AlertMessage(string s)
        {
            /// <summary>
            /// Date Created: 18/07/2011
            /// Created By: Marco Abejar
            /// (description) Show pop up message            
            /// </summary>

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void PortVendorLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["cmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for port vendor editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for port vendor editor clicked.";
            }

            strFunction = "PortVendorLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

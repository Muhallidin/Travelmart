using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;

namespace TRAVELMART
{
    public partial class CreateUserAccount : System.Web.UI.Page
    {      
        #region Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session and .parse to globalcode
        protected void Page_Load(object sender, EventArgs e)
        {
            uotextboxLName.Focus();
            if (!IsPostBack)
            {
                if (MUser.GetLDAPLoginIsON() == "1")
                {
                    uoHiddenFieldIsLDAPOn.Value = "1";
                }
                

                UserAccountLogAuditTrail();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);


                GetUserInfo();
                GetUserRoles();


                if (uoHiddenFieldIsLDAPOn.Value == "1")
                {
                    uorfvPWD.Visible = false;
                    uorfvPWD1.Visible = false;

                    uotextboxPWD.Visible = false;                    
                    uotextboxPWD1.Visible = false;

                    uclabelPWD.Visible = false;
                    uclabelPWD1.Visible = false;

                    ucLabelPassword.Visible = false;

                }
                else
                {
                    uorfvPWD.Visible = uorfvPWD1.Visible = (Request.QueryString["uId"] != null) ? false : true;
                    uotextboxPWD.Visible = uotextboxPWD1.Visible = (Request.QueryString["uId"] != null) ? false : true;
                    uclabelPWD.Visible = uclabelPWD1.Visible = (Request.QueryString["uId"] != null) ? false : true;
                    ucLabelPassword.Visible = uclabelPWD.Visible;                   
                }

                uohiddenuid.Value = (Request.QueryString["uId"] != null) ? Request.QueryString["uId"] : "";
                uoHiddenFieldOldUserName.Value = uotextboxUName.Text;
                uoHiddenFieldEmailOld.Value = uotextboxEmail.Text;
            }
            if (uoHiddenFieldUser.Value == "")
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        /// <summary>        
        /// Date Created:  12/12/2011
        /// Created By:    Muhallidin G Wali
        /// description    Chech if input text are number                  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        Int16 CheckNumberTinyInt(TextBox sender)
        {
            try
            {
                if (sender.Text.ToString() == "")
                    return 5;
                else
                    return GlobalCode.Field2TinyInt(sender.Text.Trim());
            }
            catch
            {
                return 5;
            }
        }

        /// <summary>
        /// Date Created:  15/Jul/2013
        /// Created By:    Josephine Gad
        /// description    Add UserAccountBLL.SaveUserRegion(strUsername) to add all regions
        ///                for the ff. role: Admin, Crew Assist, Hotel Specialist, Finance
        /// </summary>        
        protected void uobuttonSave_Click(object sender, EventArgs e)
        {
            //UploadImage();
            
            //return;
            try
            {
                string sMsg = "";
                string strLName = uotextboxLName.Text.Trim();
                string strFName = uotextboxFName.Text.Trim();
                string strEmail = uotextboxEmail.Text.Trim();
                string strUsername = uotextboxUName.Text.Trim();
                string strPWD = uotextboxPWD.Text.Trim();
                string strPWD1 = uotextboxPWD1.Text.Trim();
                //string sURL = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "") + Request.ApplicationPath;
                string sURL = ConfigurationSettings.AppSettings["TM-URL"].ToString(); 
                string sToEmail = strEmail;                
                string strAltEmail = "";
                bool IsLDAPOn = false;

                if (uoHiddenFieldIsLDAPOn.Value == "1")
                {
                    IsLDAPOn = true;
                }

                if (chkAlternateEmail.Checked)
                {
                    strAltEmail = txtAlternateEmail.Text.Trim();
                    sToEmail = strAltEmail;
                }
                
                string strLogDescription;
                string strFunction;

                if (strPWD.Equals(strPWD1))
                {
                    CheckBox roleCheckBox;
                    RadioButton rolePrimary;
                    DropDownList branchDropDown;
                    RequiredFieldValidator branchValidator;
                    HiddenField branchHidden;
                    Label LabelRoleName;

                    if (uohiddenuid.Value.Length == 0)
                    {
                        strPWD = DateTime.Now.ToLongTimeString().Replace(" ", "").Replace(":", "") + strUsername;
                        strPWD = strPWD + "@aZ1";
                    }
                    string strMessage = UserAccountBLL.CreateUserAccount(strLName, strFName, strEmail, strUsername, strPWD,
                        uohiddenuid.Value, uotextboxNoOfDays.Text, chkAlternateEmail.Checked, strAltEmail, 
                        uoHiddenFieldUser.Value, uoTextBoxContactNo.Text);
                    
                    if (uohiddenuid.Value == "")
                    {
                        uohiddenuid.Value = UserAccountBLL.GetUserID(strUsername);
                    }


                    if (strMessage.Equals("New user has been created successfully.") || strMessage.Equals("User account has been updated successfully."))
                    {
                        if (uohiddenuid.Value.Trim() != "")
                        {
                            foreach (GridViewRow r in uoGridViewRole.Rows)
                            {
                                roleCheckBox = (CheckBox)r.FindControl("uoCheckBoxRole");
                                rolePrimary = (RadioButton)r.FindControl("uoRadioButtonPrimary");
                                branchDropDown = (DropDownList)r.FindControl("uoDropDownListBranch");
                                branchValidator = (RequiredFieldValidator)r.FindControl("uoRequiredFieldValidatorBranch");
                                branchHidden = (HiddenField)r.FindControl("uoHiddenFieldBranch");
                                LabelRoleName = (Label)r.FindControl("uoLabelRoleName");

                                string Role = LabelRoleName.Text.Trim();
                                if (roleCheckBox.Checked)
                                {
                                    UserAccountBLL.InsertUpdateUserInRole(strUsername, Role, branchDropDown.SelectedValue, uoHiddenFieldUser.Value);
                                    UserAccountBLL.UpdatePrimaryRole(uohiddenuid.Value, Role, rolePrimary.Checked);
                                }
                                else
                                {
                                    UserAccountBLL.DeleteUserRole(strUsername, Role, uoHiddenFieldUser.Value);
                                }
                            }
                        }

                        if (strMessage.Equals("New user has been created successfully."))
                        {

                            if (IsLDAPOn)
                            {
                                sMsg = MUser.AddEditUserFromLDAP(strUsername, strFName, strLName, strEmail, strPWD, "Add", strEmail);

                                if (!IsImmigrationWithAlternateEmail(strUsername))
                                {
                                    //if (sMsg.Contains("User Add with Other App"))
                                    //{
                                    //    UserAccountBLL.EmailNewUser(strLName, strFName, strUsername, sToEmail, sURL, false, IsLDAPOn);
                                    //}
                                    //else if (sMsg.Contains("User Add New User"))
                                    //{
                                    //    UserAccountBLL.EmailNewUser(strLName, strFName, strUsername, sToEmail, sURL, true, IsLDAPOn);
                                    //}
                                    if (sMsg.Contains("User Add"))
                                    {
                                        MUser.EmailUserLDAP(strUsername, "msg001");
                                    }
                                }
                                else 
                                {
                                    if (strAltEmail.Trim() != "" && chkAlternateEmail.Checked)
                                    {
                                        UpdateImmigrationIsMigrated(strUsername, strPWD);
                                    }
                                }
                                strMessage = strMessage + " LDAP:" + sMsg;


                                

                            }

                            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                            strLogDescription = "User account added.";
                            strFunction = "uobuttonSave_Click";

                            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
                        }
                        else if (strMessage.Equals("User account has been updated successfully."))
                        {
                            if (IsLDAPOn)
                            {
                                sMsg = MUser.AddEditUserFromLDAP(strUsername, strFName, strLName, uoHiddenFieldEmailOld.Value.Trim(), 
                                    strPWD, "Edit", strEmail);

                                if (!IsImmigrationWithAlternateEmail(strUsername))
                                {
                                    //if (sMsg.Contains("User Add with Other App"))
                                    //{
                                    //    UserAccountBLL.EmailNewUser(strLName, strFName, strUsername, sToEmail, sURL, false, IsLDAPOn);
                                    //}
                                    //else if (sMsg.Contains("User Add New User"))
                                    //{
                                    //    UserAccountBLL.EmailNewUser(strLName, strFName, strUsername, sToEmail, sURL, true, IsLDAPOn);
                                    //}

                                    if (sMsg.Contains("User Add"))
                                    {
                                        MUser.EmailUserLDAP(strUsername, "msg001");
                                    }
                                }
                                else
                                {
                                    if (strAltEmail.Trim() != "" && chkAlternateEmail.Checked)
                                    {
                                        UpdateImmigrationIsMigrated(strUsername, strPWD);
                                    }  
                                }
                                strMessage = strMessage + " LDAP:" + sMsg;

                                                                                           
                            }

                            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)

                            strLogDescription = "User account updated.";
                            strFunction = "uobuttonSave_Click";

                            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
                        }

                        //update the immigration table in LDAP
                        UserAccountBLL.LDAPImmigrationUpdate(strUsername, strEmail, strAltEmail, false);

                        UserAccountBLL.SaveUserRegion(strUsername);

                        //DateTime dt = new DateTime(1753, 1, 1);
                        MembershipUser mUser = Membership.GetUser(strUsername);
                        mUser.LastActivityDate = DateTime.Now.AddDays(-150);
                        Membership.UpdateUser(mUser);
                        OpenParentPage(strMessage);
                    }
                    else
                    {
                        AlertMessage(strMessage);
                    }
                }
                else
                {
                    AlertMessage("Unidentical password");
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        protected void uodropdownlistRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetBranchVisibility();           

        }
        /// <summary>        
        /// Date Created:   25/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Selected role settings                   
        ///-------------------------------------------------
        /// Date Modified:  05/Mar/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add Service Provider Link
        ///-------------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoCheckBoxRole_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            GridViewRow Row = (GridViewRow)chkBox.NamingContainer;
            RadioButton primaryRadioBtn = (RadioButton)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoRadioButtonPrimary");
            DropDownList branchDropDown = (DropDownList)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoDropDownListBranch");
            RequiredFieldValidator branchValidator = (RequiredFieldValidator)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoRequiredFieldValidatorBranch");
            LinkButton vesselAirportSeaportLink = (LinkButton)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoLinkButtonVessel");
            LinkButton portAgentLink = (LinkButton)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoLinkButtonPortAgent");

            Label LabelRoleName = (Label)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoLabelRoleName");
            //LinkButton vehicleVendorLink = (LinkButton)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoLinkButtonVehicleVendor");
            portAgentLink.Visible = false;
            //vehicleVendorLink.Visible = false;

            Button AddCompany = (Button)uoGridViewRole.Rows[Row.RowIndex].FindControl("uoButtonAddCompany");
            string RoleID = uoGridViewRole.Rows[Row.RowIndex].Cells[0].Text;
            string SelectedRole = LabelRoleName.Text;

            if (chkBox != null)
            {



                if (SelectedRole == TravelMartVariable.RoleAdministrator)
                {
                    if (chkBox.Checked)
                    {
                        primaryRadioBtn.Checked = true;
                        primaryRadioBtn.Visible = true;
                        uoTextBoxPrimaryRole.Text = RoleID;
                        EnableDisableGvControls(SelectedRole, false);
                    }
                    else
                    {
                        primaryRadioBtn.Checked = false;
                        primaryRadioBtn.Visible = false;

                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                        EnableDisableGvControls(SelectedRole, true);
                    }
                }
                else if (SelectedRole == TravelMartVariable.RoleCrewAdmin)
                {
                    branchDropDown.Visible = false;
                    branchValidator.Enabled = false;
                    if (chkBox.Checked)
                    {
                        vesselAirportSeaportLink.Text = "Vessel";
                        primaryRadioBtn.Visible = true;
                        vesselAirportSeaportLink.Visible = true;
                        string script = "return openVessel('" + uotextboxUName.Text + "'); ";
                        vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        vesselAirportSeaportLink.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
                else if (SelectedRole == TravelMartVariable.RoleMeetGreet)
                {
                    branchDropDown.Visible = false;
                    branchValidator.Enabled = false;
                    if (chkBox.Checked)
                    {
                        vesselAirportSeaportLink.Text = "Airport";
                        primaryRadioBtn.Visible = true;
                        vesselAirportSeaportLink.Visible = true;
                        string script = "return openAirport('" + uotextboxUName.Text + "'); ";
                        vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        vesselAirportSeaportLink.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
                else if (SelectedRole == TravelMartVariable.RolePortSpecialist)
                {
                    //branchDropDown.Visible = true;
                    branchValidator.Enabled = true;
                    if (chkBox.Checked)
                    {
                        vesselAirportSeaportLink.Text = "Seaport";
                        primaryRadioBtn.Visible = true;
                        portAgentLink.Visible = true;
                        vesselAirportSeaportLink.Visible = true;

                        string script = "return openPortAgent('" + uotextboxUName.Text + "'); ";
                        portAgentLink.Attributes.Add("OnClick", script);

                        script = "return openSeaport('" + uotextboxUName.Text + "'); ";
                        vesselAirportSeaportLink.Attributes.Add("OnClick", script);

                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        vesselAirportSeaportLink.Visible = false;
                        portAgentLink.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
                else if (SelectedRole == TravelMartVariable.RoleHotelVendor
                    //|| SelectedRole == TravelMartVariable.RoleVehicleVendor 
                    )
                {
                    vesselAirportSeaportLink.Visible = false;
                    if (chkBox.Checked)
                    {
                        branchDropDown.Visible = true;
                        branchValidator.Enabled = true;
                        primaryRadioBtn.Visible = true;
                    }
                    else
                    {
                        branchDropDown.Visible = false;
                        branchValidator.Enabled = false;
                        primaryRadioBtn.Checked = false;
                        primaryRadioBtn.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }



                else if (SelectedRole == TravelMartVariable.RoleImmigration)
                {
                    if (chkBox.Checked)
                    {
                        AddCompany.Visible = true;
                        branchDropDown.Visible = true;
                        branchValidator.Enabled = true;
                        primaryRadioBtn.Visible = true;
                    }
                    else
                    {
                        AddCompany.Visible = false;
                        branchDropDown.Visible = false;
                        branchValidator.Enabled = false;
                        primaryRadioBtn.Checked = false;
                        primaryRadioBtn.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }



                else if (SelectedRole == TravelMartVariable.RoleVehicleVendor)
                {
                    branchDropDown.Visible = false;
                    branchValidator.Enabled = false;
                    if (chkBox.Checked)
                    {
                        vesselAirportSeaportLink.Text = "Vehicle Vendor";
                        primaryRadioBtn.Visible = true;
                        vesselAirportSeaportLink.Visible = true;
                        string script = "return openVehicleVendor('" + uotextboxUName.Text + "'); ";
                        vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        vesselAirportSeaportLink.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
                else if (SelectedRole == TravelMartVariable.RoleDriver)
                {
                    branchDropDown.Visible = false;
                    branchValidator.Enabled = false;
                    if (chkBox.Checked)
                    {
                        vesselAirportSeaportLink.Text = "Vendor";
                        primaryRadioBtn.Visible = true;
                        vesselAirportSeaportLink.Visible = true;
                        string script = "return openDriver('" + uotextboxUName.Text + "'); ";
                        vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        vesselAirportSeaportLink.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
                else if (SelectedRole == TravelMartVariable.RoleGreeter)
                {
                    branchDropDown.Visible = false;
                    branchValidator.Enabled = false;
                    if (chkBox.Checked)
                    {
                        vesselAirportSeaportLink.Text = "Vendor";
                        primaryRadioBtn.Visible = true;
                        vesselAirportSeaportLink.Visible = true;
                        string script = "return openGreeter('" + uotextboxUName.Text + "'); ";
                        vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        vesselAirportSeaportLink.Visible = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
                else
                {
                    vesselAirportSeaportLink.Visible = false;
                    if (chkBox.Checked)
                    {
                        primaryRadioBtn.Visible = true;
                    }
                    else
                    {
                        primaryRadioBtn.Visible = false;
                        primaryRadioBtn.Checked = false;
                        if (uoTextBoxPrimaryRole.Text == RoleID)
                        {
                            uoTextBoxPrimaryRole.Text = "";
                        }
                    }
                }
            }
            EnableDisableByRoleAccess();
        }
        #endregion

        #region Functions

        private void BindBranch(DropDownList DDL, string RoleString, RequiredFieldValidator Validator)
        {

            DataTable BranchDataTable = null;

            try
            {
                DDL.Items.Clear();
                ListItem item = new ListItem("--SELECT BRANCH--", "0");
                DDL.Items.Add(item);


                if (RoleString == TravelMartVariable.RoleVehicleVendor)
                {
                    //BranchDataTable = MaintenanceViewBLL.GetVehicleVendorBranchList("", GlobalCode.Field2String(Session["UserName"]), "0", "0", "0", "0", "0", "0");
                    BranchDataTable = VehicleBLL.vehicleGetBranchAll();
                    DDL.DataSource = BranchDataTable;
                    DDL.DataTextField = "BranchName";
                    DDL.DataValueField = "BranchID";
                    Validator.ErrorMessage = "Vehicle branch required.";
                }
                else if (RoleString == TravelMartVariable.RoleHotelVendor)
                {
                    BranchDataTable = MaintenanceViewBLL.GetHotelVendorBranchList("", uoHiddenFieldUser.Value, "0", "0", "0", "0", "0", "0");
                    DDL.DataSource = BranchDataTable;

                    DDL.DataTextField = "HOTEL";
                    DDL.DataValueField = "colBranchIDInt";
                    Validator.ErrorMessage = "Hotel branch required.";
                }
                else if (RoleString == TravelMartVariable.RolePortSpecialist)
                {
                    List<VendorPortAgentList> list = MaintenanceViewBLL.GetPortAgentList("", "", "", "", 0, 0, 0, 1000);

                    DDL.Items.Clear();
                    DDL.Items.Add(new ListItem("--Select Service Provider--", "0"));

                    if (list.Count > 0)
                    {
                        DDL.DataSource = list;
                        DDL.DataTextField = "PortAgentName";
                        DDL.DataValueField = "PortAgentID";
                    }
                    Validator.ErrorMessage = "Service Provider required.";
                }

                else if (RoleString == TravelMartVariable.RoleImmigration)
                {
                    MaintenanceViewBLL BLL = new MaintenanceViewBLL();
                    List<ImmigrationCompanyGenericClass> list = BLL.GetImmigationCompany(uotextboxUName.Text);

                    DDL.Items.Clear();
                    DDL.Items.Add(new ListItem("--Select Immigration company--", "0"));

                    if (list.Count > 0)
                    {
                        DDL.DataSource = list[0].ImmigrationCompany;
                        DDL.DataTextField = "Company";
                        DDL.DataValueField = "ImmigrationCompanyID";
                    }
                    Validator.ErrorMessage = "Immigration company required.";
                }


                DDL.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (BranchDataTable != null)
                {
                    BranchDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   18/08/2011
        /// Created By:     Marco Abejar
        /// (description)   Get user roles           
        /// ---------------------------------------
        /// Date Modified:  25/08/2011
        /// Modified By:    Josephine gad
        /// (description)   Change GetUserRoles to GetRolesByUser
        ///                 Change the dropdown branch to gridview for multiple role selection
        ///-------------------------------------------------
        /// Date Modified:  05/Mar/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add Service Provider Link
        ///-------------------------------------------------
        /// Date Modified:  21/May/2014
        /// Modified By:    Josephine Gad
        /// (description)   Activate Vehicle Vendor link if user is Vehicle Vendor
        ///                 Hide the branch drop down list if Vehicle Vendor
        ///-------------------------------------------------
        /// </summary>
        /// 
        private void GetUserRoles()
        {
            //uodropdownlistRole.DataSource = UserAccountBLL.GetUserRoles();
            //uodropdownlistRole.DataBind();
            //CommonFunctions.ChangeToUpperCase(uodropdownlistRole);
            DataTable dt = null;
            try
            {
                dt = UserAccountBLL.GetRolesByUser(Request.QueryString["uId"], uoHiddenFieldUser.Value);
                if (dt.Rows.Count > 0)
                {
                    //uoCheckBoxListRole.DataSource = dt;
                    //uoCheckBoxListRole.DataTextField = "ROLE";
                    //uoCheckBoxListRole.DataValueField = "RID";
                    //uoCheckBoxListRole.DataBind();

                    uoGridViewRole.DataSource = dt;
                    uoGridViewRole.Columns[0].Visible = true;
                    uoGridViewRole.DataBind();
                    uoGridViewRole.Columns[0].Visible = false;

                    CheckBox roleCheckBox;
                    RadioButton rolePrimary;
                    DropDownList branchDropDown;
                    RequiredFieldValidator branchValidator;
                    HiddenField branchHidden;
                    LinkButton vesselAirportSeaportLink;
                    LinkButton portAgentLink;
                    Button AddCompany;

                    Label LabelRoleName;

                    uoTextBoxPrimaryRole.Text = "";

                    short lastTabIndex = 7;
                    foreach (GridViewRow r in uoGridViewRole.Rows)
                    {
                        roleCheckBox = (CheckBox)r.FindControl("uoCheckBoxRole");
                        rolePrimary = (RadioButton)r.FindControl("uoRadioButtonPrimary");
                        branchDropDown = (DropDownList)r.FindControl("uoDropDownListBranch");
                        branchValidator = (RequiredFieldValidator)r.FindControl("uoRequiredFieldValidatorBranch");
                        branchHidden = (HiddenField)r.FindControl("uoHiddenFieldBranch");
                        vesselAirportSeaportLink = (LinkButton)r.FindControl("uoLinkButtonVessel");
                        portAgentLink = (LinkButton)r.FindControl("uoLinkButtonPortAgent");
                        portAgentLink.Visible = false;

                        AddCompany = (Button)r.FindControl("uoButtonAddCompany");
                        LabelRoleName = (Label)r.FindControl("uoLabelRoleName");

                        AddCompany.Visible = false;

                        branchValidator.ControlToValidate = branchDropDown.ID;

                        lastTabIndex += 1;
                        roleCheckBox.TabIndex = lastTabIndex;
                        lastTabIndex += 1;
                        branchDropDown.TabIndex = lastTabIndex;

                        if (LabelRoleName.Text == TravelMartVariable.RoleHotelVendor
                            )
                        {

                            vesselAirportSeaportLink.Visible = false;
                            BindBranch(branchDropDown, LabelRoleName.Text, branchValidator);
                            if (roleCheckBox.Checked)
                            {
                                branchDropDown.Visible = true;
                                branchValidator.Enabled = true;
                                branchDropDown.SelectedValue = branchHidden.Value;

                            }
                            else
                            {
                                branchDropDown.Visible = false;
                                branchValidator.Enabled = false;
                                rolePrimary.Checked = false;

                            }
                        }
                        else if (LabelRoleName.Text == TravelMartVariable.RoleVehicleVendor)
                        {
                            branchDropDown.Visible = false;
                            branchValidator.Enabled = false;

                            if (roleCheckBox.Checked)
                            {
                                vesselAirportSeaportLink.Text = "Vehicle Vendor";
                                vesselAirportSeaportLink.Visible = true;
                                string script = "return openVehicleVendor('" + uotextboxUName.Text + "');";
                                vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                            }
                            else
                            {
                                vesselAirportSeaportLink.Visible = false;
                            }
                        }
                        else if (LabelRoleName.Text == TravelMartVariable.RoleCrewAdmin)
                        {
                            branchDropDown.Visible = false;
                            branchValidator.Enabled = false;

                            if (roleCheckBox.Checked)
                            {
                                vesselAirportSeaportLink.Text = "Vessel";
                                vesselAirportSeaportLink.Visible = true;
                                string script = "return openVessel('" + uotextboxUName.Text + "');";
                                vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                            }
                            else
                            {
                                vesselAirportSeaportLink.Visible = false;
                            }
                        }
                        else if (LabelRoleName.Text == TravelMartVariable.RoleMeetGreet)
                        {
                            branchDropDown.Visible = false;
                            branchValidator.Enabled = false;

                            if (roleCheckBox.Checked)
                            {
                                vesselAirportSeaportLink.Text = "Airport";
                                vesselAirportSeaportLink.Visible = true;
                                string script = "return openAirport('" + uotextboxUName.Text + "');";
                                vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                            }
                            else
                            {
                                vesselAirportSeaportLink.Visible = false;
                            }
                        }
                        else if (LabelRoleName.Text == TravelMartVariable.RolePortSpecialist)
                        {
                            branchDropDown.Visible = false;


                            if (roleCheckBox.Checked)
                            {
                                vesselAirportSeaportLink.Text = "Seaport";
                                vesselAirportSeaportLink.Visible = true;
                                string script = "return openSeaport('" + uotextboxUName.Text + "');";
                                vesselAirportSeaportLink.Attributes.Add("OnClick", script);


                                portAgentLink.Visible = true;
                                script = "return openPortAgent('" + uotextboxUName.Text + "'); ";
                                portAgentLink.Attributes.Add("OnClick", script);
                            }
                            else
                            {
                                vesselAirportSeaportLink.Visible = false;
                                portAgentLink.Visible = false;
                            }
                        }


                        else if (LabelRoleName.Text == TravelMartVariable.RoleImmigration)
                        {

                            vesselAirportSeaportLink.Visible = false;
                            BindBranch(branchDropDown, LabelRoleName.Text, branchValidator);
                            if (roleCheckBox.Checked)
                            {
                                AddCompany.Visible = true;
                                branchDropDown.Visible = true;
                                branchValidator.Enabled = true;
                                branchDropDown.SelectedValue = branchHidden.Value;

                            }
                            else
                            {
                                branchDropDown.Visible = false;
                                branchValidator.Enabled = false;
                                rolePrimary.Checked = false;

                            }


                            //branchDropDown.Visible = false;


                            //if (roleCheckBox.Checked)
                            //{
                            //    vesselAirportSeaportLink.Text = "Seaport";
                            //    vesselAirportSeaportLink.Visible = true;
                            //    string script = "return openSeaport('" + uotextboxUName.Text + "');";
                            //    vesselAirportSeaportLink.Attributes.Add("OnClick", script);


                            //    portAgentLink.Visible = true;
                            //    script = "return openPortAgent('" + uotextboxUName.Text + "'); ";
                            //    portAgentLink.Attributes.Add("OnClick", script);
                            //}
                            //else
                            //{
                            //    vesselAirportSeaportLink.Visible = false;
                            //    portAgentLink.Visible = false;
                            //}
                        }

                        else if (LabelRoleName.Text == TravelMartVariable.RoleDriver)
                        {
                            branchDropDown.Visible = false;
                            branchValidator.Enabled = false;

                            if (roleCheckBox.Checked)
                            {
                                vesselAirportSeaportLink.Text = "Vendor";
                                vesselAirportSeaportLink.Visible = true;
                                string script = "return openDriver('" + uotextboxUName.Text + "');";
                                vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                            }
                            else
                            {
                                vesselAirportSeaportLink.Visible = false;
                            }
                        }
                        else if (LabelRoleName.Text == TravelMartVariable.RoleGreeter)
                        {
                            branchDropDown.Visible = false;
                            branchValidator.Enabled = false;

                            if (roleCheckBox.Checked)
                            {
                                vesselAirportSeaportLink.Text = "Vendor";
                                vesselAirportSeaportLink.Visible = true;
                                string script = "return openGreeter('" + uotextboxUName.Text + "');";
                                vesselAirportSeaportLink.Attributes.Add("OnClick", script);
                            }
                            else
                            {
                                vesselAirportSeaportLink.Visible = false;
                            }
                        }

                        else
                        {
                            branchDropDown.Visible = false;
                            branchValidator.Enabled = false;
                            vesselAirportSeaportLink.Visible = false;
                            portAgentLink.Visible = false;
                        }

                        if (roleCheckBox.Checked)
                        {
                            rolePrimary.Visible = true;
                            if (rolePrimary.Checked)
                            {
                                if (uoTextBoxPrimaryRole.Text == "")
                                {
                                    uoTextBoxPrimaryRole.Text = r.Cells[0].Text;
                                }
                            }
                            if (LabelRoleName.Text == TravelMartVariable.RoleAdministrator)
                            {
                                EnableDisableGvControls(LabelRoleName.Text, false);
                            }
                        }
                        else
                        {
                            rolePrimary.Visible = false;
                            //uoTextBoxPrimaryRole.Text = "";
                        }
                    }
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
        /// Date Created: 18/08/2011
        /// Created By: Marco Abejar
        /// (description) Get user info    
        /// ----------------------------------
        /// Date Modified:  28/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to SqlDataReader
        /// ----------------------------------
        /// Date Modified:  28/07/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add column colDaysNoTinyint    
        /// </summary>
        private void GetUserInfo()
        {
            SqlDataReader dr = null;
            try
            {
                if (Request.QueryString["uId"] != null)
                {
                    lblTitle.Text = "Edit User";

                    dr = UserAccountBLL.GetUserInfo(Request.QueryString["uId"]);
                    txtAlternateEmail.Enabled = false;
                    if (dr.Read())
                    {
                        uotextboxLName.Text = dr["lname"].ToString();
                        uotextboxFName.Text = dr["fname"].ToString();
                        uotextboxEmail.Text = dr["email"].ToString();
                        uotextboxUName.Text = dr["uname"].ToString();
                        uotextboxPWD.Text = dr["pwd"].ToString();
                        uotextboxNoOfDays.Text = dr["colDaysNoTinyint"].ToString();
                        txtAlternateEmail.Text = dr["AlternateEmail"].ToString();
                        if (dr["AlternateEmail"].ToString() != "")
                        {
                            chkAlternateEmail.Checked = true;

                        }
                        txtAlternateEmail.Enabled = chkAlternateEmail.Checked;
                        uoTextBoxContactNo.Text = GlobalCode.Field2String(dr["colContactNo"]);

                        //if (dtUser.Rows[0]["role"].ToString().Length > 0)
                        //{
                        //    //if()
                        //    //uodropdownlistRole.SelectedItem.Text = dtUser.Rows[0]["role"].ToString();//Convert.ToString(uodropdownlistRole.Items.FindByText(dtUser.Rows[0]["role"].ToString()).Value);
                        //    //uodropdownlistRole.Items.FindByText(dtUser.Rows[0]["role"].ToString().ToUpper()).Selected = true;
                        //    //uodropdownlistRole.SelectedValue = dtUser.Rows[0]["RoleId"].ToString();
                        //}

                        uotextboxUName.Enabled = false;
                    }

                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   19/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Close this page and update parent page            
        /// --------------------------------------------------
        /// Date Modified:  18/04/2012
        /// Modified By:    Josephine Gad        
        /// (description)   change ctl00_ContentPlaceHolder1_uoHiddenFieldPopupUser to ctl00_NaviPlaceHolder_uoHiddenFieldPopupUser for the new master page
        /// </summary>

        private void OpenParentPage(string s)
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupUser\").val(\"1\"); ";

            sScript += " parent.$.fancybox.close(); ";
            if (s != "")
            {
                s = s.Replace("'", " ");
                sScript += " alert('" + s + "'); ";
            }
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uobuttonSave, this.GetType(), "scr", sScript, false);
        }

        /// <summary>
        /// Date Created:   08/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Show pop up message            
        /// ------------------------------------
        /// Date Modified:  02/01/20111
        /// Modified By:    Josephine Gad
        /// (description)   Change ClientScript to ScriptManager
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            //Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created:   26/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Enable/disabled checkbox, radiobuttons
        /// </summary>
        /// <param name="Role"></param>
        /// <param name="IsEnable"></param>
        private void EnableDisableGvControls(string Role, bool IsEnable)
        {
            if (!IsEnable)
            {
                foreach (GridViewRow r in uoGridViewRole.Rows)
                {
                    DropDownList ddlRole = (DropDownList)r.FindControl("uoDropDownListBranch");
                    Label LabelRoleName = (Label)r.FindControl("uoLabelRoleName"); 

                    ddlRole.Visible = false;

                    if (LabelRoleName.Text != Role)
                    {
                        CheckBox chkOther = (CheckBox)r.FindControl("uoCheckBoxRole");
                        chkOther.Checked = false;
                        chkOther.Enabled = false;

                        RadioButton rdoPrimary = (RadioButton)r.FindControl("uoRadioButtonPrimary");
                        rdoPrimary.Checked = false;
                        rdoPrimary.Visible = false;
                    }
                }
            }
            else
            {
                foreach (GridViewRow r in uoGridViewRole.Rows)
                {
                    CheckBox chkOther = (CheckBox)r.FindControl("uoCheckBoxRole");
                    RadioButton rdoPrimary = (RadioButton)r.FindControl("uoRadioButtonPrimary");
                    DropDownList ddlRole = (DropDownList)r.FindControl("uoDropDownListBranch");
                    chkOther.Enabled = true;
                    ddlRole.Visible = false;
                    rdoPrimary.Checked = false;
                    rdoPrimary.Visible = false;
                    //uoTextBoxPrimaryRole.Text = "";
                }
            }
        }
        /// <summary>
        /// Date Created:   22/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Enable/disabled checkbox, radiobuttons by Role Access of User
        /// </summary>
        private void EnableDisableByRoleAccess()
        {
            foreach (GridViewRow r in uoGridViewRole.Rows)
            {
                HiddenField hIsWithAccess = (HiddenField)r.FindControl("uoHiddenFieldIsWithAccess");
                CheckBox chkOther = (CheckBox)r.FindControl("uoCheckBoxRole");
                RadioButton rdoPrimary = (RadioButton)r.FindControl("uoRadioButtonPrimary");
                bool IsWithAccess = GlobalCode.Field2Bool(hIsWithAccess.Value);
                if (!IsWithAccess)
                {
                    chkOther.Enabled = false;
                    rdoPrimary.Enabled = false;
                }
            }
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void UserAccountLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["uId"] != null)
            {
                strLogDescription = "Edit linkbutton for user account editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for user account editor clicked.";
            }

            strFunction = "UserAccountLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// <summary>
        /// Date Created:   16/Nov/2015
        /// Created By:     Josephine Monteza
        /// (description)   Update Immigration password to tag as is_migrated = 1
        /// </summary>        
        private void UpdateImmigrationIsMigrated(string sUsername, string sPassword)
        {
            if(Roles.IsUserInRole(sUsername, TravelMartVariable.RoleImmigration))
            {
                sPassword = sPassword + "123";
                MUser.ChangePasswordInLDAP(sUsername, sPassword);
            }
        }
        private bool IsImmigrationWithAlternateEmail(string sUsername)
        {
            bool bReturn = false;
            if (Roles.IsUserInRole(sUsername, TravelMartVariable.RoleImmigration))
            {
                if (chkAlternateEmail.Checked)
                {
                    if (txtAlternateEmail.Text.Trim() != "")
                    {
                        bReturn = true;
                    }
                }
            }
            return bReturn;
        }
     
        private void UploadImage()
        {
            var FileExtension = Path.GetExtension(FileUploadImage.PostedFile.FileName).Substring(1);
            string sAllowedExtension = ConfigurationManager.AppSettings["ImageExtension"].ToString();
            string [] sAllowedFileExtensions = sAllowedExtension.Split(",".ToCharArray());

            if (FileUploadImage.HasFile)
            {
                if (sAllowedFileExtensions != null)
                {
                    if (sAllowedFileExtensions.Count() > 0)
                    {

                        //If extension name of file is correct
                        if (sAllowedFileExtensions.Contains(FileExtension))
                        {
                            //string sFileName = uotextboxUName.Text + "" + FileExtension;
                            //FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/Temporary/") + sFileName);

                            //imgAvatar.Src = ("~/Temporary/" + sFileName);

                            string sAPI_Header = ConfigurationManager.AppSettings["MediaHeader"].ToString();
                            string sAPI_Header_Verify = ConfigurationManager.AppSettings["MediaHeaderVerification"].ToString();

                            string sToken = ConfigurationManager.AppSettings["MediaToken"].ToString();
                            string sAPI_URL = ConfigurationManager.AppSettings["MediaURL"].ToString();


                            byte img;
                            string sAPI = sAPI_URL + "/avatars/jde/" + uotextboxUName.Text;

                            WebRequest wRequest = WebRequest.Create(sAPI);
                            wRequest.Method = "HEAD";
                            wRequest.Headers.Add(sAPI_Header, sToken);

                            bool bIsExists = false;
                            bIsExists = IsImageExists(wRequest, uotextboxUName.Text);

                            string sAvatarPath = Path.GetFullPath(FileUploadImage.FileName);
                            if (!bIsExists)
                            {

                                NameValueCollection nvc = new NameValueCollection();
                                nvc.Add("ID", uotextboxUName.Text);
                                //nvc.Add("btn-submit-photo", "Upload");
                             
                                AddAvatar(sAPI, sAvatarPath, "file", "image/jpeg", nvc, sAPI_Header, sToken);

                            }
                            else
                            { 
                                
                            }
                        }
                    }
                }
            }
                      
        }


        private void AddAvatar(string url, string file, string paramName, string contentType, NameValueCollection nvc, 
            string sAPI_Header, string sToken)
        {
            //log.Debug(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            wr.Headers.Add(sAPI_Header, sToken);

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

        
            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
        
        private bool IsImageExists(WebRequest wRequest, string sAPI)
        {
            bool bReturn = false;
            string sAPI_Header_Verify = ConfigurationManager.AppSettings["MediaHeaderVerification"].ToString();
            WebResponse wResponse = null;
            try
            {
                wResponse = wRequest.GetResponse();
                bReturn = GlobalCode.Field2Bool(wResponse.Headers[sAPI_Header_Verify]);
                return bReturn;
            }
            catch (WebException we)
            {
                using (HttpWebResponse errorResponse = we.Response as HttpWebResponse)
                {
                    //Same with if( (int)errorResponse.StatusCode == 404)
                    if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        //Avatar Not Found!   
                    }
                    else
                    {
                        //Log error
                    }
                }
                return bReturn;
            }
            finally
            {
                if (wResponse != null)
                {
                    wResponse.Close();
                }

            }
        }
        private void BindPhoto()
        { 
            
        }
        #endregion
    }
}
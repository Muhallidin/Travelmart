using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.HtmlControls;

using System.IO;

namespace TRAVELMART
{
    public partial class RequestView_old : System.Web.UI.Page
    {
        #region "Events"
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
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //use uspSelectHotelVehicleRequest
            if (MUser.GetUserName() == "")
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                Session["RequestPath"] = Path.GetFileName(Request.Path);
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString();
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                //uoHiddenFieldDateRange.Value = UserAccountBLL.GetUserDateRange(uoHiddenFieldUser.Value).ToString();

                if (GlobalCode.Field2String(Session["UserRole"]) == "")
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    Session["UserRole"]= UserRolePrimary;                    
                    GetBranchInfo();
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);

                if (uoHiddenFieldRole.Value  == TravelMartVariable.RoleHotelVendor ||
                    uoHiddenFieldRole.Value  ==TravelMartVariable.RoleVehicleVendor ||
                    uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    if (GlobalCode.Field2String(Session["UserCountry"]) == "0" || GlobalCode.Field2String(Session["UserCountry"]) == "")
                    {
                        GetBranchInfo();
                    }
                }
                GetVessel();
                GetNationality();
                GetGender();
                GetRank();
                GetRequest();
                Session["strPrevPage"] = Request.RawUrl;
            }
            if (uoHiddenFieldPopEditor.Value != "0")
            {
//                if (uoButtonViewAll.Visible == true)
                if( uoButtonViewAll.Enabled == false)
                {
                    GetRequestALL();
                }
                else
                {
                    GetRequest();
                }
            }
            uoHiddenFieldPopEditor.Value = "0";
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            uoButtonViewAll.Enabled = true;
            GetRequest();
        }
        protected void uoButtonViewAll_Click(object sender, EventArgs e)
        {
            uoButtonViewAll.Enabled = false;
            GetRequestALL();
        }
        protected void uoListViewRequest_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetRequest();
            }
        }
        protected void uoButtonApproved_Click(object sender, EventArgs e)
        {
            ApproveRequest();
        }
        #endregion

        #region "Procedures"
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetVessel()
        {            
            //DataTable VesselDataTable = null;
            List<VesselDTO> vesselList = null;
            try
            {
                string sDateTo = GlobalCode.Field2String(Session["DateTo"]);
                if (sDateTo == "")
                {
                    sDateTo = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value)).ToString("MM/dd/yyyy");                    
                }
                vesselList = VesselBLL.GetVesselList(uoHiddenFieldUser.Value, uoHiddenFieldDate.Value,
                    sDateTo, GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), uoHiddenFieldRole.Value, false);
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Ship--", "0");
                uoDropDownListVessel.Items.Add(item);

                var listVessel = (from a in vesselList
                                  select new
                                  {
                                      VesselName = a.VesselNameString,
                                      VesselID = a.VesselIDString
                                  }).ToList();
                uoDropDownListVessel.DataSource = listVessel;
                uoDropDownListVessel.DataTextField = "VesselName";
                uoDropDownListVessel.DataValueField = "VesselID";                
                uoDropDownListVessel.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (vesselList != null)
                {
                    vesselList = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of nationality
        /// </summary>
        private void GetNationality()
        {
            DataTable dt = null;
            try
            {
                dt = MasterfileBLL.GetReference("Nationality");
                ListItem item = new ListItem("--Select Nationality--", "0");
                uoDropDownListNationality.Items.Clear();
                uoDropDownListNationality.Items.Add(item);
                uoDropDownListNationality.DataSource = dt;
                uoDropDownListNationality.DataTextField = "RefName";
                uoDropDownListNationality.DataValueField = "RefID";
                uoDropDownListNationality.DataBind();
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
        /// Date Created:   03/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of gender
        /// </summary>
        private void GetGender()
        {
            DataTable dt = null;
            try
            {
                dt = MasterfileBLL.GetReference("Gender");
                ListItem item = new ListItem("--Select Gender--", "0");
                uoDropDownListGender.Items.Clear();
                uoDropDownListGender.Items.Add(item);
                uoDropDownListGender.DataSource = dt;
                uoDropDownListGender.DataTextField = "RefName";
                uoDropDownListGender.DataValueField = "RefID";
                uoDropDownListGender.DataBind();
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
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetRankByVessel(uoDropDownListVessel.SelectedValue);
                ListItem item = new ListItem("--Select Rank--", "0");
                uoDropDownListRank.Items.Clear();
                uoDropDownListRank.Items.Add(item);
                uoDropDownListRank.DataSource = dt;
                uoDropDownListRank.DataTextField = "RankName";
                uoDropDownListRank.DataValueField = "RankID";
                uoDropDownListRank.DataBind();
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
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Manifest List        
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetRequest()
        {            
            DataTable dt = null;
            DataView dv = null;
            try
            {
                string sDateTo = GlobalCode.Field2String(Session["DateTo"]);
                if (sDateTo == "")
                {
                    sDateTo = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value)).ToString("MM/dd/yyyy");
                }
                dt = RequestBLL.GetRequest(uoHiddenFieldDate.Value, sDateTo,
                    uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), uoDropDownListStatus.SelectedValue, 
                    uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                    GlobalCode.Field2String(Session["Port"]), GlobalCode.Field2String(Session["Hotel"]), GlobalCode.Field2String(Session["Vehicle"]), 
                    uoDropDownListVessel.SelectedValue, uoDropDownListNationality.SelectedValue,
                    uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue, "", uoHiddenFieldRole.Value);
                dv = dt.DefaultView;

                dv.Sort = uoDropDownListGroupBy.SelectedValue + ", Name ";

                uoListViewRequest.DataSource = dv;
                uoListViewRequest.DataBind();

                HtmlControl VesselHeader = (HtmlControl)uoListViewRequest.FindControl("ucTHVessel");
                HtmlControl HotelHeader = (HtmlControl)uoListViewRequest.FindControl("ucTHHotel");
                if (VesselHeader != null)
                {
                    if (uoDropDownListGroupBy.SelectedValue == "VesselName")
                    {
                        VesselHeader.Visible = false;
                        HotelHeader.Visible = true;
                    }
                    else if (uoDropDownListGroupBy.SelectedValue == "HotelName")
                    {
                        VesselHeader.Visible = true;
                        HotelHeader.Visible = false;
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
                if (dv != null)
                {
                    dv.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Manifest All List        
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetRequestALL()
        {
            DataTable dt = null;
            DataView dv = null;
            try
            {
                dt = RequestBLL.GetRequest("", "",
                    uoHiddenFieldUser.Value, "0", "0",
                    "0", "",
                    "1", "",
                    "0", "0", "0",
                    "0", "0",
                    "0", "0", "2", uoHiddenFieldRole.Value);
                dv = dt.DefaultView;

                dv.Sort = uoDropDownListGroupBy.SelectedValue + ", Name ";

                uoListViewRequest.DataSource = dv;
                uoListViewRequest.DataBind();

                HtmlControl VesselHeader = (HtmlControl)uoListViewRequest.FindControl("ucTHVessel");
                HtmlControl HotelHeader = (HtmlControl)uoListViewRequest.FindControl("ucTHHotel");
                if (VesselHeader != null)
                {
                    if (uoDropDownListGroupBy.SelectedValue == "VesselName")
                    {
                        VesselHeader.Visible = false;
                        HotelHeader.Visible = true;
                    }
                    else if (uoDropDownListGroupBy.SelectedValue == "HotelName")
                    {
                        VesselHeader.Visible = true;
                        HotelHeader.Visible = false;
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
                if (dv != null)
                {
                    dv.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Approve/disapprove selected request
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void ApproveRequest()
        {
            try
            {
                string strLogDescription;
                string strFunction;

                string ApproveByString = uoHiddenFieldUser.Value;
                foreach (ListViewItem item in uoListViewRequest.Items)
                {                    
                    CheckBox CheckSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    bool IsApprovedBool = CheckSelect.Checked;

                    if (IsApprovedBool)
                    {
                        HiddenField HiddenID = (HiddenField)item.FindControl("uoHiddenFieldIdBigInt");
                        string RequestIDString = HiddenID.Value;
                        
                        Label E1ID = (Label)item.FindControl("uoLabelE1ID");
                        Label OnOffdate = (Label)item.FindControl("uoLabelOnOffDate");
                        Label Name = (Label)item.FindControl("uoLabelName");
                        HiddenField CountryID = (HiddenField)item.FindControl("uoHiddenFieldCountry");
                        HiddenField BranchID = (HiddenField)item.FindControl("uoHiddenFieldBranch");
                        

                        RequestBLL.ApprovedRequest(RequestIDString, IsApprovedBool, ApproveByString);

                        //Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                        strLogDescription = "Hotel/Vehicle manual request approved.";
                        strFunction = "ApproveRequest";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(RequestIDString), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);

                        string sMsg = "Manual Hotel/Vehicle Request of seafarer <b>" + Name.Text + "</b> with E1 ID <b>" + E1ID.Text + "</b> has been approved. <br/><br/>";
                        sMsg += "Date (On/Off): " + OnOffdate.Text + "";
                        sMsg += "<br/><br/>Thank you.";
                        sMsg += "<br/><br/><i>Auto generated email.</i>";

                        SendEmail("Travelmart: Manual Hotel/Vehicle Request Approval", sMsg, CountryID.Value, BranchID.Value);
                    } 
                }
                AlertMessage("Approved!");                

                GetRequest();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
                throw ex;                
            }            
        }
        /// <summary>
        /// Date Created:   26/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message
        /// ---------------------------------
        /// </summary>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";            
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonApproved, this.GetType(), "scr", sScript, false);
        }
        string lastDataFieldValue = null;
        protected string RequestViewAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = uoDropDownListGroupBy.SelectedItem.Text;
            string GroupValueString = uoDropDownListGroupBy.SelectedValue;

            string currentDataFieldValue = Eval(uoDropDownListGroupBy.SelectedValue).ToString();
            //if (GroupTextString.Contains("Date"))
            //{
            //    currentDataFieldValue = string.Format("{0:dd-MMM-yyyy}", Eval(GroupValueString));
            //}

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
                return string.Format("<tr><td class=\"group\" colspan=\"12\">{0}: <strong>{1}</strong></td></tr>", uoDropDownListGroupBy.SelectedItem.Text, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        protected string HideVessel()
        {
            if (uoDropDownListGroupBy.SelectedValue == "VesselName")
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        protected string HideHotel()
        {
            if (uoDropDownListGroupBy.SelectedValue == "HotelName")
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Date Created:   20/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get user branch details if user is vendor or Service Provider
        /// -------------------------------------------------
        /// Date Modified:   27/11/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to IDataReader
        /// </summary>
        private void GetBranchInfo()
        {
            IDataReader dr = null;
            try
            {
                dr = UserAccountBLL.GetUserBranchDetails(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value);
                if (dr.Read())
                {
                    Session["UserVendor"] = dr["VendorID"].ToString();
                    Session["UserBranchID"] = dr["BranchID"].ToString();
                    Session["UserCountry"] = dr["CountryID"].ToString();
                    Session["UserCity"] = dr["CityID"].ToString();
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
        /// Date Created:   21/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Send email
        /// </summary>
        private void SendEmail(string sSubject, string sMessage, string sCountry, string sBranchID)
        {
            string sBody;
            DataTable dt = null;
            try
            {
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email 24*7
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleCrewAssist, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + "Admin" + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel specialist of the country affected
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelSpecialist, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleHotelSpecialist + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel vendor
                if (sBranchID != "")
                {
                    dt = new DataTable();
                    dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelVendor, sBranchID, sCountry);
                    foreach (DataRow r in dt.Rows)
                    {
                        sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                        sBody += "Dear " + TravelMartVariable.RoleHotelVendor + ", <br/><br/> " + sMessage;
                        sBody += "</TD></TR></TABLE>";

                        CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
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
        #endregion                       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART.Vehicle
{
    public partial class VehicleViewPending : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            } 
            if (!IsPostBack)
            {
                if (Session["UserRole"] == null)
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    Session["UserRole"] = UserRolePrimary;
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                SetDefaultValues();
                GetVessel();                              
                GetNationality();
                GetGender();
                GetRank();
               
                Session["strPrevPage"] = Request.RawUrl;
            }
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            GetSFVehicleTravelDetails();            
        }

        protected void uoListViewVehicle_PreRender(object sender, EventArgs e)
        {
            GetSFVehicleTravelDetails();
        }

        protected void uoObjectDataSourceVehicle_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFrom"] = GlobalCode.Field2String(Session["DateFrom"]);
            e.InputParameters["DateTo"] = GlobalCode.Field2String(Session["DateTo"]);
            e.InputParameters["UserID"] = uoHiddenFieldUser.Value;
            e.InputParameters["FilterByDate"] = GlobalCode.Field2String(Session["strPendingFilter"]);
            e.InputParameters["RegionID"] = GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["CountryID"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["CityID"] = GlobalCode.Field2String(Session["City"]);
            e.InputParameters["Status"] = uoDropDownListStatus.SelectedValue;
            e.InputParameters["FilterByNameID"] = uoDropDownListFilterBy.SelectedValue;
            e.InputParameters["FilterNameID"] = uoTextBoxFilter.Text.Trim();
            e.InputParameters["PortID"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["VesselID"] = uoDropDownListVessel.SelectedValue;
            e.InputParameters["Nationality"] = uoDropDownListNationality.SelectedValue;
            e.InputParameters["Gender"] = uoDropDownListGender.SelectedValue;
            e.InputParameters["Rank"] = uoDropDownListRank.SelectedValue;
            e.InputParameters["Role"] = uoHiddenFieldRole.Value;

            e.InputParameters["ByVessel"] = Int16.Parse(uoHiddenFieldByVessel.Value);
            e.InputParameters["ByName"] = Int16.Parse(uoHiddenFieldByName.Value);
            e.InputParameters["ByE1ID"] = Int16.Parse(uoHiddenFieldByE1ID.Value);
            e.InputParameters["ByDateOnOff"] = Int16.Parse(uoHiddenFieldByDateOnOff.Value);
            e.InputParameters["ByStatus"] = Int16.Parse(uoHiddenFieldByStatus.Value);
            e.InputParameters["ByPort"] = Int16.Parse(uoHiddenFieldByPort.Value);
            e.InputParameters["ByRank"] = Int16.Parse(uoHiddenFieldByRank.Value);   
        }
        //protected void uoListViewVehicle_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    string pendingID = e.CommandArgument.ToString();
        //    if (e.CommandName == "Approve")
        //    {
        //        VehicleBLL.vehicleApproveTransaction(pendingID, Session["UserName"].ToString());
        //        GetSFVehicleTravelDetails();
        //    }
        //}
        protected void uoButtonApproved_Click(object sender, EventArgs e)
        {
            ApproveRequest();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list        
        /// </summary>
        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                VesselDataTable = VesselBLL.GetVessel(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["DateFrom"]),
                    GlobalCode.Field2String(Session["DateTo"]), GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), GlobalCode.Field2String(Session["UserRole"]));
                uoDropDownListVessel.Items.Clear();
                ListItem item = new ListItem("--Select Vessel--", "0");
                uoDropDownListVessel.Items.Add(item);
                uoDropDownListVessel.DataSource = VesselDataTable;
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
                if (VesselDataTable != null)
                {
                    VesselDataTable.Dispose();
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

        /// <summary>
        /// Date Created:   28/09/2011
        /// Created By:     Ryan Bautista
        /// (description)   Get list of rank
        /// </summary>
        private void GetRank()
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetRankByVessel("0");
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
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Set default values of global variables        
        /// </summary>
        private void SetDefaultValues()
        {
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
            if (GlobalCode.Field2String(Session["DateFrom"]) == "")
            {
                Session["DateFrom"] = currentDate;
                Session["DateTo"] = currentDate;
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
        /// Date Created: 03/10/2011
        /// Created By: Josephine Gad
        /// (description) Date and time format        
        /// </summary>
        protected string DateFormat(object oDatetime)
        {
            Int32 DateTimeInt32 = (Int32)oDatetime;
            if (DateTimeInt32 == 1)
            {
                return "{0:dd-MMM-yyyy HHmm}";
            }
            else
            {
                return "{0:dd-MMM-yyyy}";
            }
        }

        /// <summary>
        /// Date Created:   14/10/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Set vehicle view  groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string VehicleAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Vessel";
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
                return string.Format("<tr><td class=\"group\" colspan=\"10\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get pending vehicle boooking using datasource
        /// </summary>
        private void GetSFVehicleTravelDetails()
        {
            try
            {
                uoListViewVehicle.DataSource = null;
                uoListViewVehicle.DataSourceID = "uoObjectDataSourceVehicle";
            }
            catch (Exception ex)
            {
                throw ex;
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
            IDataReader dr = null;
            DataTable dTable = null;
            try
            {
                string strLogDescription;
                string strFunction; 

                string ApproveByString = GlobalCode.Field2String(Session["UserName"]);;
                foreach (ListViewItem item in uoListViewVehicle.Items)
                {
                    CheckBox CheckSelect = (CheckBox)item.FindControl("uoCheckBoxSelect");
                    bool IsApprovedBool = CheckSelect.Checked;

                    if (IsApprovedBool)
                    {
                        HiddenField HiddenID = (HiddenField)item.FindControl("uoHiddenFieldIdBigInt");
                        HiddenField BranchID = (HiddenField)item.FindControl("uoHiddenFieldBranchIDInt");
                        Label VehiclelName = (Label)item.FindControl("uoLabelVehicleName");
                        Label E1ID = (Label)item.FindControl("uoLabelE1ID");
                        Label Pickupdate = (Label)item.FindControl("uoLabelPickupDate");
                        LinkButton Name = (LinkButton)item.FindControl("SeafarerLinkButton");
                        string pendingID = HiddenID.Value;

                        dTable = VehicleBLL.vehicleApproveTransaction(pendingID, ApproveByString);
                        dr = VendorMaintenanceBLL.vehicleVendorBranchMaintenanceInformation(Int32.Parse(BranchID.Value));
                        if (dr.Read())
                        {
                            string sMsg = "Booking of seafarer <b>" + Name.Text + "</b> with E1 ID <b>" + E1ID.Text + "</b> has been approved. <br/><br/>";
                            sMsg += "Vehicle Branch: " + VehiclelName.Text + "<br/>";
                            sMsg += "Pickup Date: " + Pickupdate.Text + "";
                            sMsg += "<br/><br/>Thank you.";
                            sMsg += "<br/><br/><i>Auto generated email.</i>";

                            SendEmail("Travelmart: Pending Vehicle Booking Approval", sMsg, dr["colCountryIDInt"].ToString(), BranchID.Value);
                        }

                        //Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                        strLogDescription = "Vehicle pending transaction approved.";
                        strFunction = "ApproveRequest";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dTable.Rows[0]["PK"].ToString()), dTable.Rows[0]["SeqNum"].ToString(), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }
                }
                AlertMessage("Approved!");                

                GetSFVehicleTravelDetails();                                
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
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
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleVehicleSpecialist, "0", sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleVehicleSpecialist + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
                }
                //Email Hotel vendor
                dt = new DataTable();
                dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleVehicleVendor, sBranchID, sCountry);
                foreach (DataRow r in dt.Rows)
                {
                    sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                    sBody += "Dear " + TravelMartVariable.RoleVehicleVendor + ", <br/><br/> " + sMessage;
                    sBody += "</TD></TR></TABLE>";

                    CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
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

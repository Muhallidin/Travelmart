using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART
{
    public partial class HotelDashboard : System.Web.UI.Page
    {
        #region DECLARATIONS
        DashboardBLL dbBLL = new DashboardBLL();
        string lastHotelBranch = null;
        string lastDate = null;
        string lastStatus = null;
        string Role = string.Empty;
        #endregion

        #region EVENTS
        /// <summary>
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
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                ViewState["InvalidRequest"] = "";
                 Session["strPrevPage"] = Request.RawUrl;
                Session["ViewLeftMenu"] = "0";
                Role = GlobalCode.Field2String(Session["UserRole"]);;
                //uoHiddenFieldRole.Value = Role;
                DateTime dFrom = DateTime.Parse(Request.QueryString["dt"].ToString());
                DateTime dTo = dFrom;
                uoHiddenFieldStartDate.Value = dFrom.ToString("MM/dd/yyyy");
                uoHiddenFieldEndDate.Value = dTo.ToString("MM/dd/yyyy");

                Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");
                uclabelStatus.Text = "Based on Checkin Date: " + DateTime.Parse(uoHiddenFieldStartDate.Value).ToString("dd-MMM-yyyy");

                uoLabelBranchName.Text = Request.QueryString["branchName"].ToString();

                if (TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserRole"]))
                {                   
                    uoHiddenFieldUserBrand.Value = GlobalCode.Field2String(Session["UserVendor"]);
                    uoHiddenFieldUserBranch.Value = GlobalCode.Field2String(Session["UserBranchID"]);

                    LoadDashboardDetails();
                    LoadSeafarers();
                    uoHyperLinkAdd.Visible = false;
                    uoPanelPending.Visible = false;
                    uoButtonApproved.Visible = false;
                    PendingDiv.Visible = false;
                   
                }
                else 
                {
                    uoHiddenFieldUserBranch.Value = Request.QueryString["branchId"];
                    uoHiddenFieldUserBrand.Value = Request.QueryString["brandId"];
                    uoHiddenFieldUserRegion.Value = GlobalCode.Field2String(Session["Region"]);

                    LoadDashboardDetails();
                    LoadSeafarers();
                    getBranchMatrix();
                    //GetSFHotelTravelDetails(); //get automatic booking
                    //PendingDetailsDiv.Visible = false;
                    
                    uoHyperLinkAdd.HRef = "~/Hotel/HotelRoomOverride.aspx?bId=0&hrId=0&rId=" + uoHiddenFieldUserRegion.Value;
                    
                }
                uoHyperLinkNoBooking.NavigateUrl = "~/Hotel/HotelViewNoTransaction.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "&dt=" + Request.QueryString["dt"].ToString() + "&branchId=" + Request.QueryString["branchId"].ToString() + "&branchName=" + Request.QueryString["BranchName"].ToString();
                EventNotification();                
            }
        }

        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            //e.InputParameters["UserId"] = GlobalCode.Field2String(Session["UserName"]);
            //e.InputParameters["BranchId"] = Int32.Parse(uoHiddenFieldUserBranch.Value);
            //e.InputParameters["colDate"] = DateTime.Parse(uoHiddenFieldStartDate.Value);
        }

        protected void uoButtonApproved_Click(object sender, EventArgs e)
        {
            ApproveRequest();
        }

        protected void uoHotelListPager_PreRender(object sender, EventArgs e)
        {
            //GetSFHotelTravelDetails();
        }
        #endregion

        #region METHODS
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
        /// Date Modified:      23/01/2012
        /// Modified By:        Josephine Gad
        /// (description)       Add Approval for pending hotel transaction (manual bookings)
        /// </summary>        
        private void ApproveRequest()
        {
            IDataReader dr = null;
            DataTable dTable = null;            
            try
            {
                string strLogDescription;
                string strFunction;

                string ApproveByString = GlobalCode.Field2String(Session["UserName"]);
                string InvalidTravelReq = "";

                foreach (ListViewItem item in uoHotelList.Items)
                {
                    CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                    bool IsApprovedBool = CheckSelect.Checked;

                    HiddenField uoHiddenFieldIsManual = (HiddenField)item.FindControl("uoHiddenFieldIsManual");
                    HiddenField uoPendingId = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");

                    
                    if (IsApprovedBool)
                    {
                        if (uoHiddenFieldIsManual.Value == "Yes")
                        {
                            dTable = HotelBLL.HotelApproveTransaction(uoPendingId.Value, GlobalCode.Field2String(Session["UserName"]));
                        }
                        else
                        {
                            String UserId = GlobalCode.Field2String(Session["UserName"]);
                            Label Duration = (Label)item.FindControl("uoLblDuration");
                            HiddenField TravelReqId = (HiddenField)item.FindControl("hfTRID");
                            HiddenField ManualReqId = (HiddenField)item.FindControl("hfMRID");
                            Label RecLoc = (Label)item.FindControl("uoLblRecLoc");
                            HiddenField VendorId = (HiddenField)item.FindControl("hfVendor");
                            //HiddenField HiddenID = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");
                            HiddenField BranchID = (HiddenField)item.FindControl("hfBranch");
                            HiddenField RoomTypeId = (HiddenField)item.FindControl("hfRoom");
                            HiddenField CheckIndate = (HiddenField)item.FindControl("hfCheckIn");
                            HiddenField CheckOutDate = (HiddenField)item.FindControl("hfCheckOut");
                            //HiddenField HotelStatus= (HiddenField)item.FindControl("hfHotelStatus");
                            HiddenField SFStatus = (HiddenField)item.FindControl("hfSFStatus");
                            HiddenField CityId = (HiddenField)item.FindControl("hfCity");
                            HiddenField CountryId = (HiddenField)item.FindControl("hfCountry");
                            HiddenField ContractId = (HiddenField)item.FindControl("hfContractID");
                            Label Voucher = (Label)item.FindControl("uoLblVoucher");
                            HiddenField DataSource = (HiddenField)item.FindControl("hfDatasource");
                            HiddenField PendingId = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");
                            HiddenField HotelName = (HiddenField)item.FindControl("hfHotelName");
                            Label E1ID = (Label)item.FindControl("uoLblE1ID");
                            HiddenField Name = (HiddenField)item.FindControl("hfName");

                            Boolean valid = true;
                            if (PendingId.Value == "0")
                            {
                               // valid = AutomaticBookingBLL.ApproveBookings(Int32.Parse(TravelReqId.Value), RecLoc.Text, Int32.Parse(VendorId.Value),
                               //Int32.Parse(BranchID.Value), Int32.Parse(RoomTypeId.Value), DateTime.Parse(CheckIndate.Value), DateTime.Parse(CheckOutDate.Value),
                               //SFStatus.Value, Int32.Parse(CityId.Value), Int32.Parse(CountryId.Value), Voucher.Text, Int32.Parse(ContractId.Value),
                               //UserId);
                            }
                            else
                            {
                                HotelBLL.HotelApproveTransaction(PendingId.Value, UserId);
                            }

                            if (!valid)
                            {
                                InvalidTravelReq = InvalidTravelReq + TravelReqId.Value + ",";
                            }
                            else
                            {
                                //dr = VendorMaintenanceBLL.vehicleVendorBranchMaintenanceInformation(Int32.Parse(BranchID.Value));
                                //if (dr.Read())
                                //{
                                string sMsg = "Booking of seafarer <b>" + Name.Value + "</b> with E1 ID <b>" + E1ID.Text + "</b> has been approved. <br/><br/>";
                                sMsg += "Hotel Branch: " + HotelName.Value + "<br/>";
                                sMsg += "Checkin Date: " + CheckIndate.Value + "";
                                sMsg += "<br/><br/>Thank you.";
                                sMsg += "<br/><br/><i>Auto generated email.</i>";

                                SendEmail("Travelmart: Pending Hotel Booking Approval", sMsg, CountryId.Value, BranchID.Value);
                                //}
                            }

                            //dTable = HotelBLL.HotelApproveTransaction(pendingID, ApproveByString);
                            

                            //Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                            strLogDescription = "Hotel pending transaction approved.";
                            strFunction = "ApproveRequest";

                            DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dTable.Rows[0]["PK"].ToString()), dTable.Rows[0]["SeqNum"].ToString(), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                            //BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                            //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                            //uoHotelList.Items.Remove((ListViewDataItem)item);
                        }
                    }
                }
                if (InvalidTravelReq != "")
                {
                    AlertMessage("Some bookings were not approved. Please check the room allocations of the highlighed transactions.");
                    ViewState["InvalidRequest"] = InvalidTravelReq.TrimEnd(',').ToString();
                }
                else
                {
                    AlertMessage("Approved");
                }
               
                LoadDashboardDetails();
                LoadSeafarers();
                getBranchMatrix();
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
                if (dTable != null)
                {
                    dTable.Dispose();
                }
            }
        }

        string lastDataFieldValue = null;
        protected string HotelAddGroup()
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
                return string.Format("<tr><td class=\"group\" colspan=\"28\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }                      

        protected bool IsEventExists(object BranchID, object Date)
        {
            //DateTime DateEvent = DateTime.Parse(Date.ToString());
            IDataReader dr = HotelBLL.GetEventNotification(Int32.Parse(BranchID.ToString()), 0, Date.ToString());
            try
            {
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
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
                    dr.Dispose();
                }
            }
        }

        protected void LoadDashboardDetails()
        {
            DataTable dt = null;
            try
            {
                dt = dbBLL.HotelDashboardbyDate(Int32.Parse(uoHiddenFieldUserBranch.Value),
                    Int32.Parse(uoHiddenFieldUserBrand.Value), Int32.Parse(uoHiddenFieldUserRegion.Value), DateTime.Parse(uoHiddenFieldStartDate.Value),
                    DateTime.Parse(uoHiddenFieldEndDate.Value));
                if (dt.Rows.Count == 0)
                {
                    dt = dbBLL.HotelDashboardStatfromContract(Int32.Parse(uoHiddenFieldUserBranch.Value),
                        Int32.Parse(uoHiddenFieldUserBrand.Value),
                        DateTime.Parse(uoHiddenFieldStartDate.Value));
                }
                uoHotelDashboardDetails.Items.Clear();
                uoHotelDashboardDetails.DataSource = dt; 
                uoHotelDashboardDetails.DataBind();
                //ViewState["dt"] = dt;
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

        protected void LoadSeafarers()
        {
            uoDashBoardListDetails.Items.Clear();
            uoDashBoardListDetails.DataSource = null;
            uoDashBoardListDetails.DataSourceID = "ObjectDataSource1";
        }

        protected string SetStatus()
        {
            string StatusTitle = "Status :";
            string currentStatus = Eval("colHotelStatusVarchar").ToString();

            if (currentStatus.Length == 0)
            {
                currentStatus = "";
            }

            if (currentStatus == "Unused")
            {
                currentStatus = "Arriving";
            }

            if (lastStatus != currentStatus)
            {
                lastStatus = currentStatus;
                return string.Format("<tr><td class=\"group\" colspan=\"15\">{0}: <strong>{1}</strong></td></tr> ", StatusTitle, currentStatus);
            }
            else
            {
                return string.Empty;
            }
        }

        protected string addDashBoardGroup()
        {
            if (TravelMartVariable.RoleHotelVendor != GlobalCode.Field2String(Session["UserRole"]))
            {
                string BranchTitle = "Hotel Name :";

                string currentHotelBranch = Eval("colBranchName").ToString();

                //GlobalCode.Field2String(Session["UserVendor"]) = Eval("colBrandId").ToString();
                //GlobalCode.Field2String(Session["UserBranchID"]) = Eval("colBranchId").ToString();

                if (currentHotelBranch.Length == 0)
                {
                    currentHotelBranch = "";
                }

                if (lastHotelBranch != currentHotelBranch)
                {
                    lastHotelBranch = currentHotelBranch;
                    lastDate = null;
                    return string.Format("<tr><td class=\"group\" colspan=\"11\">{0}: <strong>{1}</strong></td></tr> ", BranchTitle, currentHotelBranch);
                }
                else
                {
                    
                    return string.Empty;
                }

               

            }
            else
            {
                return string.Empty;
            }
        }

        protected string setDate()
        {
            string currentDate = Eval("CDate").ToString();

            //GlobalCode.Field2String(Session["UserVendor"]) = Eval("colBrandId").ToString();
            //GlobalCode.Field2String(Session["UserBranchID"]) = Eval("colBranchId").ToString();

            if (currentDate.Length == 0)
            {
                currentDate = "";
            }

            if (lastDate != currentDate)
            {
                lastDate = currentDate;
                DateTime dt = DateTime.Parse(currentDate);
               
                return String.Format("{0:dd-MMM-yyyy (dddd)}", dt);
            }
            else
            {
                return "";
            }
        }

        protected bool SetVisibility()
        {
            if (GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RoleHotelSpecialist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Date Created:   04/01/2011
        /// Created By:     Josephine Gad
        /// (description)   Check if Hotel has Event
        /// </summary>
        private void EventNotification()
        {
            IDataReader dr = null;
            try
            {
                //DateTime dtDate = CommonFunctions.ConvertDateByFormat(uoHiddenFieldStartDate.Value);
                dr = HotelBLL.GetEventNotification(int.Parse(uoHiddenFieldUserBranch.Value), 0, uoHiddenFieldStartDate.Value);
                if (dr.Read())
                {
                    uoLinkButtonEvents.Visible = true;

                    string scriptEventString = "return OpenEventsList('" + uoHiddenFieldUserBranch.Value
                        + "', '" + 0 + "', '" + uoHiddenFieldStartDate.Value + "');";
                    uoLinkButtonEvents.Attributes.Add("OnClick", scriptEventString);      
                }
                else
                {
                    uoLinkButtonEvents.Visible = false;
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
        /// Author:         Charlene Remotigue
        /// Date Created:   18/01/2012
        /// Description:    get branch matrix
        /// </summary>
        protected void getBranchMatrix()
        {
            DataTable dt = null;
            try
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                AutomaticBookingBLL.SaveTempAutomaticBookings(DateTime.Parse(uoHiddenFieldStartDate.Value),
                       Int32.Parse(uoHiddenFieldUserBranch.Value), uoHiddenFieldUser.Value);

                GetSFHotelTravelDetails();
               
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
        /// Author: Charlene Remotigue
        /// Date Created: 19/01/2012
        /// description: get seafarer pending list
        /// </summary>
        private void GetSFHotelTravelDetails()
        {
            try
            {
                uoHotelList.Items.Clear();
                uoHotelList.DataSource = null;
                uoHotelList.DataSourceID = "ObjectDataSource2";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidateData()
        {
            Boolean Invalid = false;

            if (ViewState["InvalidRequest"].ToString().Contains(Eval("TravelRequestId").ToString()))
            {
                Invalid = true;
            }
            else
            {
                Invalid = false;
            }

            if (Invalid)
            {
                return string.Format("background-color: #FFCC66; border: thin solid #000000; font-weight: bold");
            }
            else
            {
                return string.Format("");
            }
        }
        #endregion       

        protected void btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("HotelDashBoard2.aspx?ufn=" + Request.QueryString["ufn"] + "&branchId=" + Request.QueryString["branchId"] +
                "&branchName=" + Request.QueryString["branchName"]);
        }
    }
}

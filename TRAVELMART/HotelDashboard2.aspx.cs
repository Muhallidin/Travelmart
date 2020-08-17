using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{   
    public partial class HotelDashboard2 : System.Web.UI.Page
    {
        #region DECLARATIONS
        public string lastDate = null;
        public string lastStatus = null;
        DashboardBLL BLL = new DashboardBLL();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            if (!IsPostBack)
            {
                //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                strLogDescription = "Hotel dashboard information viewed. (Date)";
                strFunction = "Page_Load";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                SetDefaults(0);
            }
        }

        protected void uoButtonApprove_Click(object sender, EventArgs e)
        {
            BookSeafarer();
        }

        
        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/02/2012
        /// Description: Book Seafarer to a hotel
        /// </summary>
        protected void BookSeafarer()
        {
            string sfName = "";
            string sfId = "";
            string sfStripe = "";
            string SFStatus = "";
            string recLoc = "";
            string trId = "";
            string mReqId = "";
            foreach (ListViewItem item in uoHotelList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    HiddenField SeafarerName = (HiddenField)item.FindControl("hfName");
                    Label SeafarerId = (Label)item.FindControl("uoLblE1ID");
                    Label Stripes = (Label)item.FindControl("uoLblStripe");
                    HiddenField Status = (HiddenField)item.FindControl("hfSFStatus");
                    Label RLoc = (Label)item.FindControl("uoLblRecLoc");
                    HiddenField tReq = (HiddenField)item.FindControl("hfTRID");
                    
                    sfName = sfName + SeafarerName.Value + "|";
                    sfId = sfId + SeafarerId.Text + "|";
                    sfStripe = sfStripe + Stripes.Text + "|";
                    SFStatus += Status.Value + '|';
                    recLoc += RLoc.Text + "|";
                    trId += tReq.Value + "|";
                    mReqId += "0" + "|";
                }
            }

            sfName = sfName.TrimEnd('|');
            sfId = sfId.TrimEnd('|');
            sfStripe = sfStripe.TrimEnd('|');
            SFStatus = SFStatus.TrimEnd('|');
            recLoc = recLoc.TrimEnd('|');
            trId = trId.TrimEnd('|');
            mReqId = mReqId.TrimEnd('|');

            string sscript = "OpenRequestEditor('" + sfId + "','" + sfName + "','"
                + sfStripe + "','" + SFStatus + "','" + recLoc + "','" + trId + "','" 
                + Request.QueryString["branchId"] + "','" + mReqId + "')";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenRequestEditor", sscript, true);
        }

        protected void SetDefaults(Int16 LoadType)
        {
            ViewState["InvalidRequest"] = "";
             Session["strPrevPage"]  = Request.RawUrl;
             Session["ViewLeftMenu"]  = "0";

            uoHiddenFieldStartDate.Value = Request.QueryString["dt"].ToString();
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            //uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
            Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");
            uclabelStatus.Text = "Based on Checkin Date: " + DateTime.Parse(uoHiddenFieldStartDate.Value).ToString("dd-MMM-yyyy");
            uoHiddenFieldBranchId.Value = Request.QueryString["branchId"];
            uoLabelBranchName.Text = Request.QueryString["branchName"];
            //BLL.LoadAllHotelDashboard2Tables(DateTime.Parse(uoHiddenFieldStartDate.Value), Int32.Parse(uoHiddenFieldBranchId.Value),
            //    LoadType, uoHiddenFieldUser.Value, 0, 20);

            GetEvenCount();
            LoadStatusDashboard();
            LoadConfirmedBookings();

            if (TravelMartVariable.RoleHotelVendor == uoHiddenFieldUser.Value)
            {
                PendingDiv.Visible = false;
                uoPanelPending.Visible = false;
            }
            else
            {
                LoadPendingBookings();
            }
        }

        private void ApproveRequest()
        {
            IDataReader dr = null;
            DataTable dTable = null;
            try
            {
                string strFunction = "ApproveRequest";
                

                string ApproveByString = GlobalCode.Field2String(Session["UserName"]);
                string InvalidTravelReq = "";

                foreach (ListViewItem item in uoHotelList.Items)
                {
                    CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                    bool IsApprovedBool = CheckSelect.Checked;

                    HiddenField uoHiddenFieldIsManual = (HiddenField)item.FindControl("uoHiddenFieldIsManual");
                    HiddenField uoPendingId = (HiddenField)item.FindControl("uoHiddenFieldColIdBigInt");
                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

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
                               valid = AutomaticBookingBLL.ApproveBookings(Int32.Parse(TravelReqId.Value), RecLoc.Text, Int32.Parse(VendorId.Value),
                               Int32.Parse(BranchID.Value), Int32.Parse(RoomTypeId.Value), DateTime.Parse(CheckIndate.Value), DateTime.Parse(CheckOutDate.Value),
                               SFStatus.Value, Int32.Parse(CityId.Value), Int32.Parse(CountryId.Value), Voucher.Text, Int32.Parse(ContractId.Value),
                               UserId, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);
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

                                //SendEmail("Travelmart: Pending Hotel Booking Approval", sMsg, CountryId.Value, BranchID.Value);
                                //}
                            }

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

                SetDefaults(3);
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

        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uoButtonApprove, this.GetType(), "scr", sScript, false);
        }
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

        protected void GetEvenCount()
        {
            //if (HotelDashboardClass.EventCount > 0)
            //{
            //    uoLinkButtonEvents.Visible = true;

            //    string scriptEventString = "return OpenEventsList('" + uoHiddenFieldBranchId.Value
            //            + "', '" + 0 + "', '" + uoHiddenFieldStartDate.Value + "');";
            //    uoLinkButtonEvents.Attributes.Add("OnClick", scriptEventString);  
            //}
            //else
            //{
            //    uoLinkButtonEvents.Visible = false;
            //}
        }

        protected void LoadStatusDashboard()
        {
            //uoHotelDashboardDetails.DataSource = HotelDashboardClass.HotelRoomBlocks;
            //uoHotelDashboardDetails.DataBind();
        }

        protected void LoadConfirmedBookings()
        {
            ObjectDataSource1.TypeName = "TRAVELMART.Common.HotelDashboardClass";
            ObjectDataSource1.SelectCountMethod = "GetConfirmBookingCount";
            ObjectDataSource1.SelectMethod = "GetConfirmBooking";

            uoDashboardListDetails.DataSourceID = ObjectDataSource1.UniqueID;
            uoUpdatePanelDetails.Update();
        }

        protected void LoadPendingBookings()
        {
            ObjectDataSource2.TypeName = "TRAVELMART.Common.HotelDashboardClass";
            ObjectDataSource2.SelectCountMethod = "GetPendingBookingCount";
            ObjectDataSource2.SelectMethod = "GetPendingBooking";

            uoHotelList.DataSourceID = ObjectDataSource2.UniqueID;
            //uoPanelPending.Update();
        }
        #endregion

        #region LISTVIEW GROUPINGS AND VALIDATIONS
        public string setDate()
        {
            string currentDate = Eval("Date").ToString();

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

        protected string SetStatus()
        {
            string StatusTitle = "Status :";
            string currentStatus = Eval("HotelStatus").ToString();

            if (currentStatus.Length == 0)
            {
                currentStatus = "";
            }

            if (lastStatus != currentStatus)
            {
                lastStatus = currentStatus;
                return string.Format("<tr><td class=\"group\" colspan=\"16\">{0} <strong>{1}</strong></td></tr> ", StatusTitle, currentStatus);
            }
            else
            {
                return string.Empty;
            }
        }

        public string ValidateRequest()
        {
            Boolean Invalid = false;

            if (ViewState["InvalidRequest"].ToString().Contains(Eval("TravelReqId").ToString()))
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

       
    }
}

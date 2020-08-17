using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls; 
using System.Web.Security;
 
namespace TRAVELMART
{
    public partial class SuperUserView : System.Web.UI.Page
    {
        #region Declarations

        public String OnDate;
        public String OffDate;
        public FinanceBLL fBLL = new FinanceBLL();
        #endregion


        #region Events
        /// <summary>
        /// Date Created:   08/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Refresh confirmed hotel bookings list when it has been updated
        /// ---------------------------------------------------------------------------
        /// Date Modified:  14/02/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Change editor for hotel
        /// --------------------------------------
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================  
        /// Date Modified: 11/04/2012
        /// Modified By:   Charlene Remotigue
        /// (description)  added initialize values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        protected void Page_Load(object sender, EventArgs e)
        {
            string sUserRole = InitializeValues();
            if ((sUserRole == TravelMartVariable.RoleFinance) || (sUserRole == TravelMartVariable.RoleAdministrator) ||
                (sUserRole == TravelMartVariable.RoleCrewAssist))
            {
                uoHyperLinkReimbursementAdd.Visible = true;
            }
            else
            {
                uoHyperLinkReimbursementAdd.Visible = false;
            }
            
            Session["strSFCode"] = Request.QueryString["sfId"];
            Session["strRecordLocator"] = Request.QueryString["recloc"];
            Session["strSFStatus"] = Request.QueryString["st"];

            Session["strTravelLocatorID"] = Request.QueryString["ID"];                        
            Session["TravelRequestID"] = Request.QueryString["trID"];
            Session["ManualRequestID"] = Request.QueryString["manualReqID"];


            
            if (uoHiddenFieldPopEditor.Value == "1")
            {
                GetSFHotelDetails();
                uoHiddenFieldPopEditor.Value = "0";
            }
            if (uoHiddenFieldPopupRemarks.Value == "1")
            {
               //get remarks by identity

                if (TravelMartVariable.RoleHotelVendor == sUserRole)
                {
                    GetRemarks(2);
                }
                else
                {
                    GetRemarks(4);
                }
                RemarksView(false, false);
                uoHiddenFieldPopupRemarks.Value = "0";
            }
            if (!IsPostBack)
            {

                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldIDBigint.Value = Request.QueryString["ID"];
                uoHiddenFieldTRID.Value = GlobalCode.Field2String(Request.QueryString["trID"]);

                uoHdRecLoc.Value = GlobalCode.Field2String(Session["strRecordLocator"]);
                uoHdSfStatus.Value = GlobalCode.Field2String(Session["strSFStatus"]);

                if (Session["UserRole"] == null)
                {
                    Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                    GetBranchInfo();
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                //uoHiddenFieldRoleBranchID.Value = GlobalCode.Field2String(Session["UserBranchID"]);

                if (GlobalCode.Field2String(Session["strSFStatus"]) == "ON")
                {
                    uclabelStatus.Text = "ON";
                }
                else
                {
                    uclabelStatus.Text = "OFF";
                }

                int E1TravelReqID = 0;
                uoHiddenFieldIsExistInTR.Value = "1";
                if (Request.QueryString["e1TR"] != null)
                {
                    E1TravelReqID = GlobalCode.Field2Int(Request.QueryString["e1TR"]);
                    if (E1TravelReqID == 0)
                    {
                        uoHiddenFieldIsExistInTR.Value = "0";
                    }
                }
                
                SuperUserViewLogAuditTrail();

                GetSFInfo();
                GetSFAirTravelInfo();
                GetSFVehicleTravelInfo();
                GetSFHotelDetails();
                GetSFPortTravelDetails();
                GetRoleSettings();
                LoadReimbursement(); //Added cremotigue 27/10/11
                LoadOtherInfo();//Added cremotigue 27/10/11

                //GetSFVehicleTravelPending();
                //GetSFHotelPending(); temporarily removed -gelo 10/02/2012

                if (uclabelStatus.Text == "ON")
                {
                    Session["strOnOffDate"] = OnDate;
                }
                else if (uclabelStatus.Text == "OFF")
                {
                    Session["strOnOffDate"] = OffDate;
                }

                 //Session["strPrevPage"] = Request.UrlReferrer.ToString();

                //uoHyperLinkHotelAdd.HRef = "~/Hotel/HotelEditor.aspx?sfId=" + Session["strSFCode"].ToString() + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + GlobalCode.Field2String(Session["strRecordLocatorID"]) + "&Add=" + "1" + "&manualReqID=" +  GlobalCode.Field2String(Session["ManualRequestID"]) + "&trID=" + GlobalCode.Field2String(Session["TravelRequestID"]) + "&Date=" + TravelMartVariable.strOnOffDate;                
                uoHyperLinkVehicleAdd.HRef = "~/Vehicle/VehicleEditor.aspx?sfId=" + GlobalCode.Field2String(Session["strSFCode"]) + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + GlobalCode.Field2String(Session["strRecordLocatorID"]) + "&manualReqID=" + GlobalCode.Field2String(Session["ManualRequestID"]) + "&trID=" + uoHiddenFieldTRID.Value;//+ "&SN=" + TravelMartVariable.strSFSeqNo;
                uoHyperLinkAirAdd.HRef = "~/Air/AirEditor.aspx?sfId=" + Session["strSFCode"].ToString() + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + GlobalCode.Field2String(Session["strRecordLocatorID"]) + "&Add=1";// +"&SN=" + TravelMartVariable.strSFSeqNo;
                uoHyperLinkReimbursementAdd.HRef = "~/Finance/ReimbursementAdd.aspx?rId=0&mReqId=" + Request.QueryString["manualReqId"] + "&tReqId=" + Request.QueryString["trID"] + "&sfId=" + Request.QueryString["sfId"] + "&cId=" + ViewState["CurrencyId"] + "&cName=" + ViewState["CurrencyName"];


                if (User.IsInRole(TravelMartVariable.RoleAdministrator) ||
                    User.IsInRole(TravelMartVariable.RoleHotelSpecialist) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAssist) ||
                    User.IsInRole(TravelMartVariable.RoleCrewAdmin))
                {
                    ucTableRemarks.Visible = true;
                }
                else
                {
                    ucTableRemarks.Visible = false;
                }

                if (TravelMartVariable.RoleHotelVendor == uoHiddenFieldRole.Value)
                {
                    GetRemarks(2);
                }
                else
                {
                    GetRemarks(3);
                }
                RemarksView(true, false);
                uoHyperLinkRemarks.NavigateUrl = "Remarks.aspx?trID=" + Request.QueryString["trID"];
            }

            if (uoHiddenFieldPopupAir.Value == "1")
            {
                GetSFAirTravelInfo();
            }
            if (uoHiddenFieldPopupPort.Value == "1")
            {
                GetSFPortTravelDetails();
            }
            if (uoHiddenFieldPopupHotel.Value == "1")
            {
                GetSFHotelDetails();
            }
            if (uoHiddenFieldPopupHotelPending.Value == "1")
            {
                GetSFHotelDetails();
                //GetSFHotelPending(); temporarily removed -gelo 10/02/2012
            }
            if (uoHiddenFieldPopupVehicle.Value == "1")
            {
                GetSFVehicleTravelInfo();
            }
            //if (uoHiddenFieldPopupVehiclePending.Value == "1")
            //{
            //    GetSFVehicleTravelPending();
            //}
            uoHiddenFieldPopupAir.Value = "0";
            uoHiddenFieldPopupPort.Value = "0";
            uoHiddenFieldPopupHotel.Value = "0";
            uoHiddenFieldPopupHotelPending.Value = "0";
            uoHiddenFieldPopupVehicle.Value = "0";
            uoHiddenFieldPopupVehiclePending.Value = "0";
           

        }
        /// Date Modified:  3/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change the URL to GlobalCode.Field2String(Session["strPrevPage"])
        /// ========================================================  
        protected void uobuttonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(uoHiddenPrevPage.Value);
        }

        protected void uogridviewPortInfo_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                PortBLL.DeletePortTransaction(index, uoHiddenFieldUser.Value);

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Port transaction deleted. (flagged as inactive)";
                strFunction = "uogridviewPortInfo_RowCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

                GetSFPortTravelDetails();
            }
        }
        protected void uogridviewPortInfo_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

        }

        protected void uoListviewVehicleTravelInfo_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            string info = e.CommandArgument.ToString();

            string[] arg = new string[2];
            char[] splitter = { ';' };
            arg = info.Split(splitter);
            string IDBigInt = arg[0];
            string SeqNo = arg[1];
            string TransVehicleIDBigInt = arg[2];
            if (e.CommandName == "Delete")
            {
                if (Convert.ToInt32(IDBigInt) == 0)
                {
                    VehicleBLL.vehicleDeleteBookingOther(Convert.ToInt32(TransVehicleIDBigInt), uoHiddenFieldUser.Value);

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Vehicle transaction deleted - Non Sabre feed. (flagged as inactive)";
                    strFunction = "uoListviewVehicleTravelInfo_ItemCommand";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(TransVehicleIDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
                }
                else
                {
                    VehicleBLL.vehicleDeleteBooking(Int32.Parse(IDBigInt), Convert.ToInt16(SeqNo), uoHiddenFieldUser.Value);

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Vehicle transaction deleted - Sabre feed. (flagged as inactive)";
                    strFunction = "uoListviewVehicleTravelInfo_ItemCommand";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Int32.Parse(IDBigInt), SeqNo, strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
                }

                GetSFVehicleTravelInfo();
            }
        }
        protected void uoListviewVehicleTravelInfo_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uoListviewVehicleTravelPending_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            string pendingID = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                VehicleBLL.vehicleDeleteBookingPending(pendingID, uoHiddenFieldUser.Value);

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Pending vehicle transaction deleted. (flagged as inactive)";
                strFunction = "uoListviewVehicleTravelPending_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

                //GetSFVehicleTravelPending();
            }
            else if (e.CommandName == "Approve")
            {
                VehicleBLL.vehicleApproveTransaction(pendingID, uoHiddenFieldUser.Value);

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Pending vehicle transaction approved.";
                strFunction = "uoListviewVehicleTravelPending_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);

                GetSFVehicleTravelInfo();
                //GetSFVehicleTravelPending();
            }
        }
        protected void uoListviewVehicleTravelPending_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  27/07/2012
        /// Description:    Add hidden field hfIdBigint and hfSeqNo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uolistviewHotelInfo_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;
            SuperViewBLL svbll = new SuperViewBLL();
            string info = e.CommandArgument.ToString();

            string[] arg = new string[2];
            char[] splitter = { ';' };
            arg = info.Split(splitter);
            string IDBigInt = arg[0];
            string SeqNo = arg[1];
            string TransHotelIDBigInt = arg[2];

            if (e.CommandName == "Delete")
            {


                //if (Convert.ToInt32(IDBigInt) == 0)
                //{
                //    HotelBLL.DeleteHotelBookingOther(Convert.ToInt32(TransHotelIDBigInt), GlobalCode.Field2String(Session["UserName"]));

                //    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                //    strLogDescription = "Hotel transaction deleted - Non Sabre feed. (flagged as inactive)";
                //    strFunction = "uolistviewHotelInfo_ItemCommand";

                //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(TransHotelIDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                //}
                //else
                //{
                //    HotelBLL.DeleteHotelBooking(Convert.ToInt32(IDBigInt), GlobalCode.Field2String(Session["UserName"]), Convert.ToInt32(SeqNo));

                //    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                //    strLogDescription = "Hotel transaction deleted - Sabre feed. (flagged as inactive)";
                //    strFunction = "uolistviewHotelInfo_ItemCommand";

                //    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                //    BLL.AuditTrailBLL.InsertLogAuditTrail(Int32.Parse(IDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                //                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                //}
                //GetSFHotelDetails();

                 //Messagebox.show("Are you sure");
                 //Page.RegisterClientScriptBlock("mes", "<script language='javascript'>alert('Are you sure')</script>");

                 //string script = "alert('New Password is Created');\n";
                 //script += "var f='" + Session["flag"].ToString() + "';\n";
                 //script += "if (f == 'L')\n";
                 //script += "location.href='loginpage.aspx';\n";

                 //Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", script, true);

                //long SeafarerID = GlobalCode.Field2Long(Request.QueryString["sfId"]);
                //long TRID = GlobalCode.Field2Long(Request.QueryString["trID"]);
                //DataTable dt = svbll.GetCancelHotelTransactionOther(GlobalCode.Field2Long(TransHotelIDBigInt), TRID, SeafarerID);

                //uolistviewHotelInfo.DataSource = dt;
                //uolistviewHotelInfo.DataBind();

            }
           
            else
            {
                HiddenField hfIdBigint = (HiddenField)e.Item.FindControl("hfIdBigint");
                HiddenField hfSeqNo = (HiddenField)e.Item.FindControl("hfSeqNo");
                EditSeafarer(uclabelE1ID.Text, TransHotelIDBigInt, hfIdBigint.Value, hfSeqNo.Value);
            }
        }

        protected void uolistviewHotelInfo_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uolistviewHotelPending_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            string pendingID = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                HotelBLL.DeleteHotelBookingPending(int.Parse(pendingID), uoHiddenFieldUser.Value);

                //Insert log audit trail (Gabriel Oquialda - 23/11/2011)
                strLogDescription = "Pending hotel transaction deleted. (flagged as inactive)";
                strFunction = "uolistviewHotelPending_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);

                //GetSFHotelPending(); temporarily removed -gelo 10/02/2012
            }
            else if (e.CommandName == "Approve")
            {
                DataTable dTable = null;
                try
                {
                    dTable = HotelBLL.HotelApproveTransaction(pendingID, uoHiddenFieldUser.Value);

                    //Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                    strLogDescription = "Pending hotel transaction approved.";
                    strFunction = "uolistviewHotelPending_ItemCommand";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dTable.Rows[0]["PK"].ToString()), dTable.Rows[0]["SeqNum"].ToString(), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, uoHiddenFieldUser.Value);

                    GetSFHotelDetails();
                    //GetSFHotelPending(); temporarily removed -gelo 10/02/2012
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dTable != null)
                    {
                        dTable.Dispose();
                    }
                }
            }
        }
        protected void uolistviewHotelPending_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        protected void uoReimbursementListPager_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LoadReimbursement();
            }
        }

        protected void uoReimbursementList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            uoHiddenFieldReimbursementId.Value = e.CommandArgument.ToString();
        }

        protected void uoBtnAdd_Click(object sender, EventArgs e)
        {
            BookSeafarer();
        }

        protected void uolistviewHotelInfo_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        protected void uoButtonNew_Click(object sender, EventArgs e)
        {
            RemarksView(false, true);

            uoHiddenFieldLatestRemarksID.Value = "0";
            uoTextBoxRemarks.Text = "";
            uoLabelRemarksBy.Text = uoHiddenFieldUser.Value;
            uoLabelRemarksDate.Text = String.Format("{0:dd-MMM-yyyy}", DateTime.Now);
        }
        protected void uoButtonEdit_Click(object sender, EventArgs e)
        {
            RemarksView(false, false);            
        }
        protected void uoButtonCancel_Click(object sender, EventArgs e)
        {
            //uoLabelRemarks.Visible = true;
            //uoTextBoxRemarks.Visible = false;
            
            //uoButtonEdit.Visible = true;
            //uoButtonNew.Visible = true;

            //uoButtonCancel.Visible = false;
            //uoButtonSave.Visible = false;
            RemarksView(true, false);

            if (TravelMartVariable.RoleHotelVendor == uoHiddenFieldRole.Value)
            {
                GetRemarks(2);
            }
            else
            {
                GetRemarks(3);
            }
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            try
            {                
                if (uoTextBoxRemarks.Text.Trim() == "")
                {
                    AlertMessage("No Remarks!");
                }
                else
                {
                    int iRemarksID = GlobalCode.Field2Int(uoHiddenFieldLatestRemarksID.Value);

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                    DateTime GMTDate = CommonFunctions.GetDateTimeGMT(currentDate);
                    string sPageName = Path.GetFileName(Request.Path);

                    TravelRequestBLL BLL = new TravelRequestBLL();

                    if (iRemarksID == 0)
                    {
                        iRemarksID = BLL.InsertTravelRequestRemarks(uoHiddenFieldRole.Value, uoHiddenFieldTRID.Value, uoTextBoxRemarks.Text,
                            uoLabelRemarksBy.Text, "uoButtonSave_Click", sPageName, GMTDate, currentDate,
                            GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value));
                        uoHiddenFieldLatestRemarksID.Value = GlobalCode.Field2String(iRemarksID);
                    }
                    else
                    {
                        BLL.UpdateTravelRequestRemarks(iRemarksID, uoHiddenFieldTRID.Value, uoTextBoxRemarks.Text,
                            uoHiddenFieldUser.Value, "uoButtonSave_Click", sPageName, GMTDate, currentDate);
                    }
                }
                //uoLabelRemarks.Text = uoTextBoxRemarks.Text;    

                if (TravelMartVariable.RoleHotelVendor == uoHiddenFieldRole.Value)
                {
                    GetRemarks(2);
                }
                else
                {
                    GetRemarks(3);
                }
                RemarksView(true, false);
            }
            catch (Exception ex)
            { 
                AlertMessage(ex.Message);    
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 11/04/2012
        /// Description: initialize session values
        /// </summary>
        /// <returns></returns>
        protected string InitializeValues()
        {
            string sUserRole = GlobalCode.Field2String(Session["UserRole"]);
            if (Request.QueryString["sfId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));
            if (muser == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!muser.IsOnline)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            if(!IsPostBack)
            {
                uoHiddenPrevPage.Value = GlobalCode.Field2String(Session["strPrevPage"]);
            }
            Session["strPrevPage"] = Request.RawUrl;
            return sUserRole;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/10/2011
        /// Description: load seafarer reimbursementList
        /// </summary>
        private void LoadReimbursement()
        {
            try
            {
                uoReimbursementList.Items.Clear();
                uoReimbursementList.DataSource = null;
                uoReimbursementList.DataSourceID = "ObjectDataSourceReimbursement";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 25/10/2011
        /// Description: Load all users that has tagged seafarer as scanned
        /// --------------------------------------
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// </summary>
        private void LoadOtherInfo()
        {
            DataTable OtherInfoDataTable = null;
            try
            {
                //bool ViewInTR = true;
                //ViewInTR = (uoHiddenFieldIsExistInTR.Value == "1" ? true : false);
                int TRID = 0;
                int mId = GlobalCode.Field2Int(Request.QueryString["manualReqID"]);
                //if (mId > 0)
                //{
                //    ViewInTR = false;
                //}
                //if (ViewInTR)
                //{
                //    TRID = GlobalCode.Field2Int(Request.QueryString["trID"]);
                //}
                TRID = GlobalCode.Field2Int(Request.QueryString["trID"]);
                OtherInfoDataTable = SeafarerBLL.GetSeafarerOtherInfo(Request.QueryString["manualReqID"], TRID.ToString());
                uoOtherList.Items.Clear();
                uoOtherList.DataSource = OtherInfoDataTable;
                uoOtherList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>        
        /// Date Created: 19/07/2011
        /// Created By: Marco Abejar
        /// (description) Show seafarer info
        /// ---------------------------------
        /// Date Modified: 14/02/2012
        /// Modified By:   Charlene Remotigue
        /// (description)  get stripe
        /// ---------------------------------
        /// Date Modified: 27/03/2012
        /// Modified By:   Jhosephine Gad
        /// (description)  Add Reason Code
        /// ---------------------------------
        /// </summary>
        private void GetSFInfo()
        {
            IDataReader dtSFInfo = null;
            try
            {
                dtSFInfo = GetSfInfoDataTable();
                if (dtSFInfo.Read())
                {
                    uclabelBrand.Text = dtSFInfo["Brand"].ToString();
                    uclabelName.Text = dtSFInfo["Name"].ToString();
                    uclabelNationality.Text = dtSFInfo["Nationality"].ToString();
                    uclabelRank.Text = dtSFInfo["Rank"].ToString();
                    uclabelVessel.Text = dtSFInfo["Vessel"].ToString();
                    //uclabelCivilStatus.Text = dtSFInfo["CivilStatus"].ToString();

                    ucLabelSignOn.Text = (dtSFInfo["SIGNINGON"].ToString() != "" ? DateTime.Parse(dtSFInfo["SIGNINGON"].ToString()).ToString("dd-MMM-yyyy") : null);

                    ucLabelSignOff.Text = (dtSFInfo["SIGNINGOFF"].ToString() != "" ? DateTime.Parse(dtSFInfo["SIGNINGOFF"].ToString()).ToString("dd-MMM-yyyy") : null);

                    uclabelE1ID.Text = dtSFInfo["colSeafarerIdInt"].ToString();
                    uclabelGender.Text = dtSFInfo["Gender"].ToString();

                    uoHiddenFieldDateOn.Value = dtSFInfo["SIGNINGON"].ToString();
                    uoHiddenFieldDateOff.Value = dtSFInfo["SIGNINGOFF"].ToString();

                    DateTime OnsigningDate = (dtSFInfo["SIGNINGON"].ToString().Length > 0)
                       ? Convert.ToDateTime(dtSFInfo["SIGNINGON"].ToString())
                       : DateTime.Now;
                    string TransONDate = String.Format("{0:MM/dd/yyyy}", OnsigningDate);
                    OnDate = TransONDate;

                    DateTime OffsigningDate = (dtSFInfo["SIGNINGOFF"].ToString().Length > 0)
                            ? Convert.ToDateTime(dtSFInfo["SIGNINGOFF"].ToString())
                            : DateTime.Now;
                    string TransOFFDate = String.Format("{0:MM/dd/yyyy}", OffsigningDate);
                    OffDate = TransOFFDate;

                    ViewState["Stripe"] = dtSFInfo["STRIPES"].ToString();
                    uclabelReason.Text = dtSFInfo["ReasonCode"].ToString();
                    //ucLabelPort.Text = dtSFInfo["PORT"].ToString();

                    //ucLabelOrigin.Text = dtSFInfo["colOriginVarchar"].ToString();
                    //ucLabelDestination.Text = dtSFInfo["colDestinationVarchar"].ToString();

                    string sOriDes = GlobalCode.Field2String(dtSFInfo["colOriginVarchar"]);
                    sOriDes += (sOriDes == "" ? GlobalCode.Field2String(dtSFInfo["colDestinationVarchar"]) : " / " + GlobalCode.Field2String(dtSFInfo["colDestinationVarchar"]));
                    ucLabelOriginDestination.Text = sOriDes;
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
            finally
            {
                if (dtSFInfo != null)
                {
                    dtSFInfo.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Show Seafarer Information
        /// ----------------------------------------------------  
        /// Date Modified: 04/08/2011
        /// Modified By: Marco Abejar
        /// (description) Remove filter for status and itenerary
        /// ----------------------------------------------------  
        /// Date Modified:  23/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// --------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// --------------------------------------
        /// Date Modified: 02/12/2011
        /// Modified By:   Josephine Gad
        /// (description)  Cannot close IDataReader here because function is use in GetSFInfo
        /// --------------------------------------
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// </summary>
        private IDataReader GetSfInfoDataTable()
        {
            IDataReader dtSFInfo = null;
            try
            {
                bool ViewInTR = true;
                ViewInTR = (uoHiddenFieldIsExistInTR.Value == "1" ? true : false);
                int mId = GlobalCode.Field2Int(Request.QueryString["manualReqID"]);
                if (mId > 0)
                {
                    ViewInTR = false;
                }
                dtSFInfo = SeafarerBLL.SeafarerGetDetails(Session["strSFCode"].ToString(),
                    uoHiddenFieldTRID.Value, GlobalCode.Field2String(Session["ManualRequestID"]), ViewInTR);
                return dtSFInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (dtSFInfo != null)
            //    {
            //        dtSFInfo.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Marco Abejar
        /// (description) Show seafarer air travel list
        /// -------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// -------------------------------------------
        /// Date Modified:  31/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change GetSFAirTravelDetails to GetSFAirTravelDetailsAll
        /// --------------------------------------
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// </summary>
        private void GetSFAirTravelInfo()
        {
            DataTable dtSFAirDetails = null;
            try
            {
                //bool ViewInTR = true;
                //ViewInTR = (uoHiddenFieldIsExistInTR.Value == "1" ? true : false);
                //if (ViewInTR)
                //{
                //    dtSFAirDetails = SuperViewBLL.GetSFAirTravelDetailsAll(GlobalCode.Field2String(Session["TravelRequestID"]), Session["strSFStatus"].ToString());
                //}
                dtSFAirDetails = SuperViewBLL.GetSFAirTravelDetailsAll(uoHiddenFieldTRID.Value, Session["strSFStatus"].ToString());
                uoListViewAirTravelInfo.DataSource = dtSFAirDetails;
                uoListViewAirTravelInfo.DataBind();

                HtmlControl EditTH = (HtmlControl)uoListViewAirTravelInfo.FindControl("EditTH");
                HtmlControl SelectTH = (HtmlControl)uoListViewAirTravelInfo.FindControl("SelectTH");

                if (SelectTH != null)
                {
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator) &&
                        !User.IsInRole(TravelMartVariable.RoleCrewAssist) &&
                        !User.IsInRole(TravelMartVariable.RoleAirSpecialist) &&
                        !User.IsInRole(TravelMartVariable.RoleCrewAssist) &&
                        !User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                    {
                        // HtmlControl SelectTH = (HtmlControl)uoListViewAirTravelInfo.FindControl("SelectTH");
                        SelectTH.Visible = false;
                        HiddenFieldPriority.Value = "false";
                    }
                    else
                    {
                        SelectTH.Visible = true;
                        HiddenFieldPriority.Value = "true";
                    }
                }


                if (EditTH != null)
                {
                    //if (!User.IsInRole("administrator") && !User.IsInRole("Air Specialist"))
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator) && 
                        !User.IsInRole(TravelMartVariable.RoleCrewAssist) && 
                        !User.IsInRole(TravelMartVariable.RoleAirSpecialist) && 
                        !User.IsInRole(TravelMartVariable.RoleCrewAssist))
                    {
                        EditTH.Style.Add("display", "none");
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSFAirDetails != null)
                {
                    dtSFAirDetails.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   26/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Show seafarer vehicle travel list
        /// -------------------------------------------
        /// Date Modified:  02/09/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change GetSFVehicleTravelDetails to GetSFVehicleTravelDetailsAll
        /// --------------------------------------
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// </summary>
        private void GetSFVehicleTravelInfo()
        {
            DataTable dtSFVehicleDetails = null;
            try
            {
                uoHyperLinkVehicleAdd.Visible = false;
                //bool ViewInTR = true;
                //ViewInTR = (uoHiddenFieldIsExistInTR.Value == "1" ? true : false);
                //if (ViewInTR)
                //{
                //    dtSFVehicleDetails = SuperViewBLL.GetSFVehicleTravelDetailsAll(GlobalCode.Field2String(Session["TravelRequestID"]), Session["strSFStatus"].ToString());
                //}
                dtSFVehicleDetails = SuperViewBLL.GetSFVehicleTravelDetailsAll(uoHiddenFieldTRID.Value, Session["strSFStatus"].ToString());
                uoListviewVehicleTravelInfo.DataSource = dtSFVehicleDetails;
                uoListviewVehicleTravelInfo.DataBind();

                HtmlControl EditTH = (HtmlControl)uoListviewVehicleTravelInfo.FindControl("EditTH");
                //HtmlControl DeleteTH = (HtmlControl)uoListviewVehicleTravelInfo.FindControl("DeleteTH");

                if (EditTH != null)
                {
                    //if (!User.IsInRole("administrator") && !User.IsInRole("vehiclespecialist"))
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                         
                        && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist) 
                        && !User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                    {
                        EditTH.Style.Add("display", "none");
                        
                        ////DeleteTH.Style.Add("display", "none");
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                        //DeleteTH.Style.Add("display", "display");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSFVehicleDetails != null)
                {
                    dtSFVehicleDetails.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   02/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Show seafarer pending vehicle travel list
        /// -------------------------------------------      
        /// </summary>
        //private void GetSFVehicleTravelPending()
        //{
        //    DataTable dtSFVehicleDetails = null;
        //    try
        //    {
        //        int TRID = 0;
        //        int mId = GlobalCode.Field2Int(Request.QueryString["manualReqID"]);
                
        //        TRID = GlobalCode.Field2Int(Request.QueryString["trID"]);
        //        dtSFVehicleDetails = SuperViewBLL.GetSFVehicleTravelDetailsPending(TRID.ToString(),  GlobalCode.Field2String(Session["ManualRequestID"]));
        //        uoListviewVehicleTravelPending.DataSource = dtSFVehicleDetails;
        //        uoListviewVehicleTravelPending.DataBind();

        //        HtmlControl EditTH = (HtmlControl)uoListviewVehicleTravelPending.FindControl("EditTH");
        //        //HtmlControl DeleteTH = (HtmlControl)uoListviewVehicleTravelPending.FindControl("DeleteTH");
        //        HtmlControl ApproveTH = (HtmlControl)uoListviewVehicleTravelPending.FindControl("ApproveTH");

        //        if (EditTH != null)
        //        {
        //            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                         
        //                && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist) 
        //                && !User.IsInRole(TravelMartVariable.RoleVehicleVendor))
        //            {
        //                EditTH.Style.Add("display", "none");
        //                //DeleteTH.Style.Add("display", "none");
        //            }
        //            else
        //            {
        //                EditTH.Style.Add("display", "display");
        //                //DeleteTH.Style.Add("display", "display");
        //            }

        //            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                         
        //                && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist))
        //            {
        //                ApproveTH.Style.Add("display", "none");
        //            }
        //            else
        //            {
        //                ApproveTH.Style.Add("display", "display");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dtSFVehicleDetails != null)
        //        {
        //            dtSFVehicleDetails.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   26/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Show seafarer hotel transaction list
        /// ---------------------------------------------------------
        /// Date Created:   26/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Change GetSFHotelDetails to GetSFHotelDetailsAll
        /// --------------------------------------
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// </summary>
        private void GetSFHotelDetails()
        {
            DataTable dtSFHotelDetails = null;
            try
            {
                bool ViewInTR = true;
                ViewInTR = (uoHiddenFieldIsExistInTR.Value == "1" ? true : false);
                int TRID = 0;
                int mId = GlobalCode.Field2Int(Request.QueryString["manualReqID"]);
                if (mId > 0)
                {
                    ViewInTR = false;
                }
                //if (ViewInTR)
                //{
                //    TRID = GlobalCode.Field2Int(Request.QueryString["trID"]);
                //}
                TRID = GlobalCode.Field2Int(Request.QueryString["trID"]);
                dtSFHotelDetails = SuperViewBLL.GetSFHotelDetailsAll(TRID.ToString(),  GlobalCode.Field2String(Session["ManualRequestID"]), Session["strSFStatus"].ToString());

                uolistviewHotelInfo.DataSource = dtSFHotelDetails;
                uolistviewHotelInfo.DataBind();

                HtmlControl EditTH = (HtmlControl)uolistviewHotelInfo.FindControl("EditTH");
                //HtmlControl CancelTH = (HtmlControl)uolistviewHotelInfo.FindControl("CancelTH");
                ////HtmlControl DeleteTH = (HtmlControl)uolistviewHotelInfo.FindControl("DeleteTH");

                if (EditTH != null)
                {
                    //if (!User.IsInRole("administrator") && !User.IsInRole("hotelspecialist"))
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                         
                        && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist) )
                    {
                        EditTH.Style.Add("display", "none");
                        //CancelTH.Style.Add("display", "none");
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                        //if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                        //{
                        //    CancelTH.Style.Add("display", "display");
                        //}
                        //else {    
                        //    CancelTH.Style.Add("display", "none");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSFHotelDetails != null)
                {
                    dtSFHotelDetails.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pending hotel transaction list
        /// ---------------------------------------------------------       
        /// </summary>
        //private void GetSFHotelPending()
        //{
        //    DataTable dtSFHotelDetails = null;
        //    try
        //    {
        //        dtSFHotelDetails = SuperViewBLL.GetSFHotelTravelDetailsPending(GlobalCode.Field2String(Session["TravelRequestID"]),  GlobalCode.Field2String(Session["ManualRequestID"]));
        //        uolistviewHotelPending.DataSource = dtSFHotelDetails;
        //        uolistviewHotelPending.DataBind();

        //        HtmlControl EditTH = (HtmlControl)uolistviewHotelPending.FindControl("EditTH");
        //        HtmlControl DeleteTH = (HtmlControl)uolistviewHotelPending.FindControl("DeleteTH");
        //        HtmlControl ApproveTH = (HtmlControl)uolistviewHotelPending.FindControl("ApproveTH");

        //        if (EditTH != null)
        //        {
        //            if (!User.IsInRole(TravelMartVariable.RoleAdministrator) && !User.IsInRole(TravelMartVariable.Role24x7) && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist) && !User.IsInRole(TravelMartVariable.RoleHotelVendor))
        //            {
        //                EditTH.Style.Add("display", "none");
        //                DeleteTH.Style.Add("display", "none");
        //            }
        //            else
        //            {
        //                EditTH.Style.Add("display", "display");
        //                DeleteTH.Style.Add("display", "display");
        //            }
        //            if (!User.IsInRole(TravelMartVariable.RoleAdministrator) && !User.IsInRole(TravelMartVariable.Role24x7) && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
        //            {
        //                ApproveTH.Style.Add("display", "none");
        //            }
        //            else
        //            {
        //                ApproveTH.Style.Add("display", "display");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dtSFHotelDetails != null)
        //        {
        //            dtSFHotelDetails.Dispose();
        //        }
        //    }
        //} 

        /// <summary>
        /// Date Created:   26/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Show seafarer port arrival detail
        /// ------------------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// ------------------------------------------------------
        /// Date Modified:  18/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change GetSFPortTravelDetails to GetSFPortTravelDetailsByTravelReqID
        /// ------------------------------------------------------
        /// Date Modified:  27/10/2011
        /// Modified By:    Charlene Remotigue
        /// (description)   get port currency
        ///  ------------------------------------------------------
        /// Date Modified:  08/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add validation if dtSFPortDetails has data to avoid error
        ///  ------------------------------------------------------        
        /// Date Modified:  13/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   add ViewInTR to check if record exists in Travel Request
        /// </summary>
        private void GetSFPortTravelDetails()
        {
            DataTable dtSFPortDetails = null;
            try
            {
                //bool ViewInTR = true;
                //ViewInTR = (uoHiddenFieldIsExistInTR.Value == "1" ? true : false);
                int TRID = GlobalCode.Field2Int(Request.QueryString["trID"]);
                int mId = GlobalCode.Field2Int(Request.QueryString["manualReqID"]);
                if (mId > 0)
                {
                  //  ViewInTR = false;
                    TRID = 0;
                }                             
                dtSFPortDetails = SuperViewBLL.GetSFPortTravelDetailsByTravelReqID(TRID.ToString(),mId.ToString(), Session["strSFStatus"].ToString());

                uogridviewPortInfo.DataSource = dtSFPortDetails;
                uogridviewPortInfo.DataBind();

                if (dtSFPortDetails != null)
                {
                    if (dtSFPortDetails.Rows.Count > 0)
                    {
                        ViewState["CurrencyId"] = dtSFPortDetails.Rows[0]["CURRENCYID"].ToString();
                        ViewState["CurrencyName"] = dtSFPortDetails.Rows[0]["CURRENCYNAME"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dtSFPortDetails != null)
                {
                    dtSFPortDetails.Dispose();
                }
            }
        }
        /// <summary>        
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Show/Hide controls based from role
        /// ======================================================
        /// Date Modifid:   20/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change roles to global variable
        /// ======================================================
        /// Date Modifid:   28/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change setting to be used for multiple role
        /// </summary>
        private void GetRoleSettings()
        {
            LinkSettingsMultipleRole();
            //if (User.IsInRole(TravelMartVariable.RoleAdministrator) || User.IsInRole(TravelMartVariable.Role24x7))
            //{
            //    LinkSettings(true, true, true);
            //}
            //else if (User.IsInRole(TravelMartVariable.RoleVehicleSpecialist) || User.IsInRole(TravelMartVariable.RoleVehicleVendor))
            //{
            //    LinkSettings(true, false, false);
            //}
            //else if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist) || User.IsInRole(TravelMartVariable.RoleHotelVendor))
            //{
            //    LinkSettings(false, true, false);
            //}
            //else if (User.IsInRole(TravelMartVariable.RolePortSpecialist))
            //{
            //    LinkSettings(false, false, true);
            //}
            //else 
            //{
            //    LinkSettings(false, false, false);
            //}
        }
        /// <summary>        
        /// Date Created:   19/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Show/Hide COntrols
        /// </summary>
        private void LinkSettings(bool ViewVehicleControls, bool ViewHotelControls, bool ViewPortControls)
        {
            if (ViewVehicleControls)
            {
                uoButtonVehicleAdd.Style.Add("display", "inline");
            }
            else
            {
                uoButtonVehicleAdd.Style.Add("display", "none");
            }
            if (ViewHotelControls)
            {
                uoBtnAdd.Style.Add("display", "inline");
            }
            else
            {
                uoBtnAdd.Style.Add("display", "none");
            }
            //if (ViewPortControls)
            //{
                uogridviewPortInfo.Columns[5].Visible = true;
                uogridviewPortInfo.DataBind();
            //}
            //else
            //{
            //    uogridviewPortInfo.Columns[5].Visible = false;
            //    uogridviewPortInfo.DataBind();
            //}
        }
        /// <summary>        
        /// Date Created:   19/07/2011
        /// Created By:     Josephine Gad
        /// (description)   setup control settings for multiple role
        /// </summary>
        private void LinkSettingsMultipleRole()
        {
            if (User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RoleVehicleSpecialist))
            {
                uoButtonVehicleAdd.Style.Add("display", "inline");
            }
            else
            {
                uoButtonVehicleAdd.Style.Add("display", "none");
            }
            if (User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
            {
                uoBtnAdd.Style.Add("display", "inline");
            }
            else
            {
                uoBtnAdd.Style.Add("display", "none");
            }
            if (User.IsInRole(TravelMartVariable.RoleAdministrator))
            {
                uogridviewPortInfo.Columns[5].Visible = true;
                uogridviewPortInfo.DataBind();
            }
            else
            {
                uogridviewPortInfo.Columns[5].Visible = false;
                uogridviewPortInfo.DataBind();
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Datestring formating            
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

        protected bool hideshow()
        {
            return User.IsInRole(TravelMartVariable.RoleHotelSpecialist);
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Hide Vehicle            
        /// </summary>
        protected string HideVehicle()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)
                && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist)
                && !User.IsInRole(TravelMartVariable.RoleVehicleVendor))
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Hide Pending Vehicle
        /// </summary>
        protected string HideVehiclePending()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)
                && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist)
                )
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Hide Hotel            
        /// </summary>
        protected string HideHotel()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)
                && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
            {
                return "hideElement";
            }
            else
            {

                return "";
            }
        }

        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Muhallidin G wali
        /// (description) Hide Cancel button Hotel            
        /// </summary>
        protected string HideCancelHotel()
        {
            if (User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
            {
                return "";
            }
            else
            {
                return "hideElement";
            }
        }

        /// <summary>
        /// Date Created:   03/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Hide Pending Hotel
        /// </summary>
        protected string HideHotelPending()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)
                && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
                && !User.IsInRole(TravelMartVariable.RoleCrewAssist)
                )
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Hide Air            
        /// </summary>
        protected string HideAir()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)
                && !User.IsInRole(TravelMartVariable.RoleAirSpecialist)
                && !User.IsInRole(TravelMartVariable.RoleCrewAssist))
            {
                return "hideElement";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Date Created: 26/07/2011
        /// Created By: Josephine Gad
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            s = s.Replace("'", "\"");
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "scr", sScript);
        }
        /// <summary>
        /// Date Created: 08/08/2011
        /// Created By: Josephine Gad
        /// (description) Change the backgroung color of old record
        /// </summary>
        protected string InactiveRow(object IsActive)
        {
            Boolean IsActiveBool = (bool)IsActive;
            if (IsActiveBool)
            {
                return "";
            }
            else
            {
                return "inactive";
            }
        }
        /// <summary>
        /// Date Created:   08/08/2011
        /// Created By:     Josephine Gad
        /// (description)   Change the backgroung color of old record
        /// ==============================================
        /// Date Modified:  20/10/2011
        /// Modified By:    Josephine Gad
        /// (description)   add branch id parameter
        /// </summary>
        protected bool InactiveControl(object IsActive, object BranchID, object Type)
        {
            Boolean IsActiveBool = (bool)IsActive;
            if (IsActiveBool)
            {
                if (Type.ToString() == "HO")
                {
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist) 
                    {
                        return false;
                    }
                    else if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                    {
                        if (uoHiddenFieldRoleBranchID.Value == BranchID.ToString())
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else if (Type.ToString() == "VE")
                {
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
                    {
                        if (uoHiddenFieldRoleBranchID.Value == BranchID.ToString())
                        {
                            return false;
                        }
                        else
                        {
                            return true;

                        }
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Date Created: 20/10/2011
        /// Created By: Gabriel Oquialda
        /// (description) Hotel event notification
        /// </summary>
        protected string NotifyEvent()
        {
            GetSFInfo(); //gets the seafarer details (i.e. onsigning or offsigning date for event notification)
            string oDate = string.Empty;
            if (uclabelStatus.Text == "ON")
            {
                oDate = OnDate;
            }
            else if (uclabelStatus.Text == "OFF")
            {
                oDate = OffDate;
            }
            return oDate;
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
            if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist ||
               uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor ||
               uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            {
                IDataReader dr = null;
                try
                {
                    dr = UserAccountBLL.GetUserBranchDetails(uoHiddenFieldUser.Value, uoHiddenFieldRole.Value);
                    if (dr.Read())
                    {
                        uoHiddenFieldRoleBranchID.Value = dr["BranchID"].ToString().ToUpper();
                        Session["UserRoleKey"] = dr["RoleID"].ToString();
                        Session["UserBranchID"] = dr["BranchID"].ToString();
                        Session["UserCountry"] = dr["CountryID"].ToString();
                        Session["UserCity"] = dr["CityID"].ToString();
                        Session["UserVendor"] = dr["VendorID"].ToString();
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
        }
        /// <summary>
        /// Date Created:   28/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Event notification
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        protected bool IsEventExists(object BranchID)
        {
            string sOn = uoHiddenFieldDateOn.Value;
            string sOff = uoHiddenFieldDateOff.Value;
            string sDateEvent = "";
            sDateEvent = (uoHdSfStatus.Value == "ON" ? sOn : sOff);

            IDataReader dr = HotelBLL.GetEventNotification(Int32.Parse(BranchID.ToString()), 0, sDateEvent);
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
        /// <summary>
        /// Date Created:   14/Jun/2013
        /// Created By:     Josephine Gad
        /// (description)   Add OpenEmergencyRequestEditor if Crew Assist
        /// </summary>
        protected void BookSeafarer()
        {
            string sfId = GlobalCode.Field2String(Session["strSFCode"]);
            string sfName = uclabelName.Text;
            string sfStripe = ViewState["Stripe"].ToString();
            string SFStatus = Request.QueryString["st"];
            string recLoc = uoHdRecLoc.Value;
            string trId = uoHiddenFieldTRID.Value;
            string mReqId =  GlobalCode.Field2String(Session["ManualRequestID"]);
            int idBgint = 0;
            int seqNo = 0;

            for (int i = 0; i < uoListViewAirTravelInfo.Items.Count; i++)
            {
                CheckBox uoCheckBoxSelect = (CheckBox)uoListViewAirTravelInfo.Items[i].FindControl("uoCheckBoxSelect");
                if (uoCheckBoxSelect.Checked)
                {
                    Label uoLabelSeqNo = (Label)uoListViewAirTravelInfo.Items[i].FindControl("uoLabelSeqNo");
                    HiddenField uoHiddenFieldIDBigint = (HiddenField)uoListViewAirTravelInfo.Items[i].FindControl("uoHiddenFieldIDBigint");
                    Label uoLabelRecLoc = (Label)uoListViewAirTravelInfo.Items[i].FindControl("uoLabelRecLoc");

                    idBgint = GlobalCode.Field2Int(uoHiddenFieldIDBigint.Value);
                    seqNo = GlobalCode.Field2Int(uoLabelSeqNo.Text);
                    recLoc = uoLabelRecLoc.Text;
                }
            }
            
            //uoHyperLinkHotelAdd.HRef = "~/Hotel/HotelEditor.aspx?sfId=" + Session["strSFCode"].ToString() + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + GlobalCode.Field2String(Session["strRecordLocatorID"]) + "&Add=" + "1" + "&manualReqID=" +  GlobalCode.Field2String(Session["ManualRequestID"]) + "&trID=" + GlobalCode.Field2String(Session["TravelRequestID"]) + "&Date=" + TravelMartVariable.strOnOffDate;                
            string sscript = "";

            if (uoHiddenFieldRole.Value == TravelMartVariable.RoleCrewAssist)
            {
                sscript = "OpenEmergencyRequestEditor('" + sfId + "','" + trId + "','" + mReqId + "', '1')";
            }
            else
            {
                sscript = "OpenRequestEditor('" + sfId + "','" + sfName + "','"
                     + sfStripe + "','" + SFStatus + "','" + recLoc + "','" + trId + "','"
                     + mReqId + "', " + idBgint + "," + seqNo + ")";
            }

            ScriptManager.RegisterClientScriptBlock(uoBtnAdd, this.GetType(), "OpenRequestEditor", sscript, true);
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenRequestEditor", sscript, true);
        }

        protected void EditSeafarer(string SeafarerId, string HotelTransId, string strIdBigint, string strSeqNo)
        {
            string sfId = GlobalCode.Field2String(Session["strSFCode"]);
            string sfName = uclabelName.Text;
            string sfStripe = ViewState["Stripe"].ToString();
            string SFStatus = Request.QueryString["st"];
            string recLoc = uoHdRecLoc.Value;
            string trId = uoHiddenFieldTRID.Value;
            string mReqId =  GlobalCode.Field2String(Session["ManualRequestID"]);
            string sIdBigint = GlobalCode.Field2Int(strIdBigint).ToString();
            string sSeqNo = GlobalCode.Field2Int(strSeqNo).ToString();
                
            //uoHyperLinkHotelAdd.HRef = "~/Hotel/HotelEditor.aspx?sfId=" + Session["strSFCode"].ToString() + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + GlobalCode.Field2String(Session["strRecordLocatorID"]) + "&Add=" + "1" + "&manualReqID=" +  GlobalCode.Field2String(Session["ManualRequestID"]) + "&trID=" + GlobalCode.Field2String(Session["TravelRequestID"]) + "&Date=" + TravelMartVariable.strOnOffDate;                
            string sscript = "OpenRequestEditor2('" + sfId + "','" + sfName + "','"
                + sfStripe + "','" + SFStatus + "','" + recLoc + "','" + trId + "','"
                + mReqId + "','" + HotelTransId + "','" + sIdBigint + "','" + sSeqNo + "')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "OpenRequestEditor", sscript, true);

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenRequestEditor", sscript, true);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void SuperUserViewLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Seafarer itinerary information viewed.";
            strFunction = "SuperUserViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(uoHiddenFieldTRID.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        /// <summary>
        /// Date Created:   05/07/2012
        /// Created By:     Josephine Gad
        /// (description)   Set remarks for control
        /// </summary>
        /// <param name="IsTrue"></param>
        private void RemarksView(bool IsTrue, bool IsNew)
        {
            if (uoHiddenFieldUser.Value == uoLabelRemarksBy.Text)
            {
                uoButtonEdit.Visible = IsTrue;
                uoTextBoxRemarks.ReadOnly = IsTrue;                
            }
            else if (IsNew == true)
            {
                uoButtonEdit.Visible = false;
                uoTextBoxRemarks.ReadOnly = false;
            }
            else
            {
                uoButtonEdit.Visible = false;
                uoTextBoxRemarks.ReadOnly = true;
            }

            uoButtonNew.Visible = IsTrue;

            uoButtonCancel.Visible = !IsTrue;
            uoButtonSave.Visible = !IsTrue;

            if (uoTextBoxRemarks.ReadOnly == true)
            {
                uoTextBoxRemarks.CssClass = "ReadOnly";
            }
            else
            {
                uoTextBoxRemarks.CssClass = "";
            }
        }
        /// <summary>
        /// Date Created:   05/07/2012
        /// Created By:     Josephine Gad
        /// (description)   Get the remarks for this TR
        /// </summary>
        /// <param name="LoadType"></param>
        private void GetRemarks(Int16 LoadType)
        {
            try
            {
                TravelRequestBLL BLL = new TravelRequestBLL();
                List<TravelRequestRemarks> list = new List<TravelRequestRemarks>();

                list = BLL.GetTravelRequestRemarks(LoadType, GlobalCode.Field2Int(uoHiddenFieldLatestRemarksID.Value),
                    GlobalCode.Field2Int(uoHiddenFieldTRID.Value), uoHiddenFieldUser.Value).ToList();
                if (list.Count > 0)
                {
                    uoLabelRemarksBy.Text = list[0].CreatedBy;
                    uoLabelRemarksDate.Text = String.Format("{0:dd-MMM-yyyy}", list[0].CreatedDate);
                    uoTextBoxRemarks.Text = list[0].Remarks;
                    uoHiddenFieldLatestRemarksID.Value = GlobalCode.Field2String(list[0].RemarkIDBigInt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion        
    }
}

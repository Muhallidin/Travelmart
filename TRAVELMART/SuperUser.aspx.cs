using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.IO;

namespace TRAVELMART
{
    public partial class SuperUser : System.Web.UI.Page
    {
        #region Declarations

        public String OnDate;
        public String OffDate;
        public ListItem item;
        #endregion

        #region Events
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
            if (Request.QueryString["sfId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if ((Session["UserRole"] == TravelMartVariable.RoleFinance) || (Session["UserRole"] == TravelMartVariable.RoleAdministrator) ||
                (Session["UserRole"] == TravelMartVariable.RoleCrewAssist))
            {
                uoHyperLinkReimbursementAdd.Visible = true;
            }
            else
            {
                uoHyperLinkReimbursementAdd.Visible = false;
            }

            SuperUserLogAuditTrail();

            Session["strSFCode"] = Request.QueryString["sfId"];
            Session["strRecordLocator"] = Request.QueryString["recloc"];
            Session["strSFStatus"] = Request.QueryString["st"];

            Session["strTravelLocatorID"] = Request.QueryString["ID"];            
            Session["TravelRequestID"] = Request.QueryString["trID"];
            if (Request.QueryString["manualReqID"] == "")
            {
                Session["ManualRequestID"] = "0";
            }
            else
            {
                Session["ManualRequestID"] = Request.QueryString["manualReqID"];
            }
            //if (Request.QueryString["trID"] == "")
            //{
            //    GlobalCode.Field2String(Session["TravelRequestID"]) = "0";
            //}
            //else
            //{
            //    GlobalCode.Field2String(Session["TravelRequestID"]) = Request.QueryString["trID"];
            //}
            uoHdRecLoc.Value = Session["strRecordLocator"].ToString();
            uoHdSfStatus.Value = Session["strSFStatus"].ToString();
            if (!IsPostBack)
            {
                Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                SFStatus.Visible = false;

                if (Session["UserRole"] == "")
                {
                    Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    GetBranchInfo();
                }

                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;
                uoHiddenFieldRoleBranchID.Value = Session["UserBranchID"].ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                if (Session["strSFStatus"] == "ON")
                {
                    uclabelStatus.Text = "Onsigning";
                }
                else
                {
                    uclabelStatus.Text = "Offsigning";
                }

                if (Session["strRecordLocator"] != "")
                {
                    ucLabelrecloc.Text = Session["strRecordLocator"].ToString();
                    ucLabelRecordLocator.Visible = true;
                }
                else
                {
                    ucLabelRecordLocator.Visible = false;
                    ucLabelrecloc.Text = "";
                }
                                
                GetSFInfo();
                GetSFAirTravelInfo();
                GetSFVehicleTravelInfo();
                GetSFHotelDetails();
                GetSFPortTravelDetails();
                GetRoleSettings();
                LoadReimbursement(); //Added cremotigue 27/10/11
                LoadOtherInfo();//Added cremotigue 27/10/11
                //uoUpdatePanelReimbursement.Update();

                GetSFVehicleTravelPending();
                //GetSFHotelPending(); temporarily removed -gelo 10/02/2012

                if (uclabelStatus.Text == "Onsigning")
                {
                    Session["strOnOffDate"] = OnDate;
                }
                else if (uclabelStatus.Text == "Offsigning")
                {
                    Session["strOnOffDate"] = OffDate;
                }

                Session["strPrevPage"] = Request.UrlReferrer.ToString();
                uoHyperLinkVehicleAdd.HRef = "~/Vehicle/VehicleEditor.aspx?sfId=" + Session["strSFCode"].ToString() + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + Session["strTravelLocatorID"].ToString() + "&manualReqID=" + GlobalCode.Field2String(Session["ManualRequestID"]) + "&trID=" +  GlobalCode.Field2String(Session["TravelRequestID"]);//+ "&SN=" + TravelMartVariable.strSFSeqNo;
                uoHyperLinkAirAdd.HRef = "~/Air/AirEditor.aspx?sfId=" + Session["strSFCode"].ToString() + "&st=" + Request.QueryString["st"] + "&recloc=" + uoHdRecLoc.Value + "&ID=" + Session["strTravelLocatorID"].ToString() + "&Add=1";// +"&SN=" + TravelMartVariable.strSFSeqNo;
                //Added cremotigue 27/10/11
                uoHyperLinkReimbursementAdd.HRef = "~/Finance/ReimbursementAdd.aspx?rId=0&mReqId=" + Request.QueryString["manualReqId"] + "&tReqId=" + Request.QueryString["trID"] + "&sfId=" + Request.QueryString["sfId"] + "&cId=" + ViewState["CurrencyId"] + "&cName=" + ViewState["CurrencyName"];
                
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
                //GetSFHotelPending(); temporarily removed -gelo 10/02/2012
            }
            if (uoHiddenFieldPopupVehicle.Value == "1")
            {
                GetSFVehicleTravelInfo();
            }
            if (uoHiddenFieldPopupVehiclePending.Value == "1")
            {
                GetSFVehicleTravelPending();
            }
            uoHiddenFieldPopupAir.Value = "0";
            uoHiddenFieldPopupPort.Value = "0";
            uoHiddenFieldPopupHotel.Value = "0";
            uoHiddenFieldPopupHotelPending.Value = "0";
            uoHiddenFieldPopupVehicle.Value = "0";
            uoHiddenFieldPopupVehiclePending.Value = "0";
            
        }
        protected void uobuttonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect( Session["strPrevPage"].ToString());
        }        

        protected void uogridviewPortInfo_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Delete")
            {
                PortBLL.DeletePortTransaction(index, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Port transaction deleted. (flagged as inactive)";
                strFunction = "uogridviewPortInfo_RowCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               


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
                    VehicleBLL.vehicleDeleteBookingOther(Convert.ToInt32(TransVehicleIDBigInt), GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Vehicle transaction deleted - Non Sabre feed. (flagged as inactive)";
                    strFunction = "uoListviewVehicleTravelInfo_ItemCommand";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(TransVehicleIDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               
                }
                else
                {
                    VehicleBLL.vehicleDeleteBooking(Int32.Parse(IDBigInt), Convert.ToInt16(SeqNo), GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Vehicle transaction deleted - Sabre feed. (flagged as inactive)";
                    strFunction = "uoListviewVehicleTravelInfo_ItemCommand";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Int32.Parse(IDBigInt), SeqNo, strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               
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
                VehicleBLL.vehicleDeleteBookingPending(pendingID, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Pending vehicle transaction deleted. (flagged as inactive)";
                strFunction = "uoListviewVehicleTravelPending_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               

                GetSFVehicleTravelPending();
            }
            else if (e.CommandName == "Approve")
            {
                VehicleBLL.vehicleApproveTransaction(pendingID, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                strLogDescription = "Pending vehicle transaction approved.";
                strFunction = "uoListviewVehicleTravelPending_ItemCommand";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               

                GetSFVehicleTravelInfo();
                GetSFVehicleTravelPending();
            }
        }
        protected void uoListviewVehicleTravelPending_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        protected void uolistviewHotelInfo_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            string info = e.CommandArgument.ToString();

            string[] arg = new string[2];
            char[] splitter = { ';' };
            arg = info.Split(splitter);
            string IDBigInt = arg[0];
            string SeqNo = arg[1];
            string TransHotelIDBigInt = arg[2];

            if (e.CommandName == "Delete")
            {
                if (Convert.ToInt32(IDBigInt) == 0)
                {
                    HotelBLL.DeleteHotelBookingOther(Convert.ToInt32(TransHotelIDBigInt), GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Hotel transaction deleted - Non Sabre feed. (flagged as inactive)";
                    strFunction = "uolistviewHotelInfo_ItemCommand";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(TransHotelIDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               
                }
                else
                {
                    HotelBLL.DeleteHotelBooking(Convert.ToInt32(IDBigInt), GlobalCode.Field2String(Session["UserName"]), Convert.ToInt32(SeqNo));

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Hotel transaction deleted - Sabre feed. (flagged as inactive)";
                    strFunction = "uolistviewHotelInfo_ItemCommand";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Int32.Parse(IDBigInt), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               
                }
                GetSFHotelDetails();
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
                HotelBLL.DeleteHotelBookingPending(int.Parse(pendingID), GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 23/11/2011)
                strLogDescription = "Hotel pending transaction deleted. (flagged as inactive)";
                strFunction = "uolistviewHotelPending_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                //GetSFHotelPending(); temporarily removed -gelo 10/02/2012
            }
            else if (e.CommandName == "Approve")
            {
                DataTable dTable = null;
                try
                {
                    dTable = HotelBLL.HotelApproveTransaction(pendingID, GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 23/11/2011)
                    strLogDescription = "Hotel pending transaction approved.";
                    strFunction = "uolistviewHotelPending_ItemCommand";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dTable.Rows[0]["PK"].ToString()), dTable.Rows[0]["SeqNum"].ToString(), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

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
                
        #endregion

        #region Functions 
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
        /// </summary>
        private void LoadOtherInfo()
        {
            DataTable OtherInfoDataTable = null;
            try
            {
                OtherInfoDataTable = SeafarerBLL.GetSeafarerOtherInfo(GlobalCode.Field2String(Session["ManualRequestID"]), GlobalCode.Field2String(Session["TravelRequestID"]));
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
                    uclabelCivilStatus.Text = dtSFInfo["CivilStatus"].ToString();
                    
                    ucLabelSignOn.Text = (dtSFInfo["SIGNINGON"].ToString() != "" ? DateTime.Parse(dtSFInfo["SIGNINGON"].ToString()).ToString("dd-MMM-yyyy") : null);
                                       
                    ucLabelSignOff.Text = (dtSFInfo["SIGNINGOFF"].ToString() != "" ? DateTime.Parse(dtSFInfo["SIGNINGOFF"].ToString()).ToString("dd-MMM-yyyy") : null);
                    
                    uoHiddenFieldDateOn.Value = dtSFInfo["SIGNINGON"].ToString();
                    uoHiddenFieldDateOff.Value = dtSFInfo["SIGNINGOFF"].ToString();

                    uclabelE1ID.Text = dtSFInfo["colSeafarerIdInt"].ToString();
                    uclabelGender.Text = dtSFInfo["Gender"].ToString();

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
        /// ----------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        private IDataReader GetSfInfoDataTable()
        {                       
            IDataReader dr = null;
            try
            {
                string E1TravelReqID = "0";
                if (Request.QueryString["e1TR"] != null)
                {
                    E1TravelReqID = Request.QueryString["e1TR"];
                }
                dr = SeafarerBLL.SeafarerGetDetails(Session["strSFCode"].ToString(),
                     GlobalCode.Field2String(Session["TravelRequestID"]), GlobalCode.Field2String(Session["ManualRequestID"]), true);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
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
        /// </summary>
        private void GetSFAirTravelInfo()
        {
            //DateTime OnsigningDate;
            //DateTime OffsigningDate;

            DataTable dtSFAirDetails = null;
            try
            {
                dtSFAirDetails = SuperViewBLL.GetSFAirTravelDetails(Session["strSFCode"].ToString(), Session["strSFStatus"].ToString(), Session["strRecordLocator"].ToString(), Session["TravelRequestID"].ToString());
                
                uoListViewAirTravelInfo.DataSource = dtSFAirDetails;
                uoListViewAirTravelInfo.DataBind();

                HtmlControl EditTH = (HtmlControl)uoListViewAirTravelInfo.FindControl("EditTH");

                if (EditTH != null)
                {
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator) && !User.IsInRole(TravelMartVariable.RoleCrewAssist) && !User.IsInRole(TravelMartVariable.RoleAirSpecialist))
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
        /// Date Created:   10/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Show seafarer vehicle travel list
        /// -------------------------------------------      
        /// </summary>
        private void GetSFVehicleTravelInfo()
        {           
            DataTable dtSFVehicleDetails = null;
            try
            {
                dtSFVehicleDetails = SuperViewBLL.GetSFVehicleTravelDetailsByID(GlobalCode.Field2String(Session["TravelRequestID"]), Session["strSFCode"].ToString(), 
                    Session["strSFStatus"].ToString(), Session["strRecordLocator"].ToString(),  GlobalCode.Field2String(Session["ManualRequestID"]));
                uoListviewVehicleTravelInfo.DataSource = dtSFVehicleDetails;
                uoListviewVehicleTravelInfo.DataBind();

                HtmlControl EditTH = (HtmlControl)uoListviewVehicleTravelInfo.FindControl("EditTH");
                HtmlControl DeleteTH = (HtmlControl)uoListviewVehicleTravelInfo.FindControl("DeleteTH");

                if (EditTH != null)
                {
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator) 
                        && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist) 
                        && !User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                    {
                        EditTH.Style.Add("display", "none");
                        DeleteTH.Style.Add("display", "none");
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                        DeleteTH.Style.Add("display", "display");
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
        private void GetSFVehicleTravelPending()
        {
            DataTable dtSFVehicleDetails = null;
            try
            {
                dtSFVehicleDetails = SuperViewBLL.GetSFVehicleTravelDetailsPending(GlobalCode.Field2String(Session["TravelRequestID"]),  GlobalCode.Field2String(Session["ManualRequestID"]));
                uoListviewVehicleTravelPending.DataSource = dtSFVehicleDetails;
                uoListviewVehicleTravelPending.DataBind();

                HtmlControl EditTH = (HtmlControl)uoListviewVehicleTravelPending.FindControl("EditTH");
                HtmlControl DeleteTH = (HtmlControl)uoListviewVehicleTravelPending.FindControl("DeleteTH");
                HtmlControl ApproveTH = (HtmlControl)uoListviewVehicleTravelPending.FindControl("ApproveTH");

                if (EditTH != null)
                {
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator) 
                        && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist) 
                        && !User.IsInRole(TravelMartVariable.RoleVehicleVendor))
                    {
                        EditTH.Style.Add("display", "none");
                        DeleteTH.Style.Add("display", "none");                        
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                        DeleteTH.Style.Add("display", "display");                        
                    }

                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                         
                        && !User.IsInRole(TravelMartVariable.RoleVehicleSpecialist))
                    {
                        ApproveTH.Style.Add("display", "none");
                    }
                    else
                    {
                        ApproveTH.Style.Add("display", "display");
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
        /// Date Created:   10/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Show seafarer hotel transaction list
        /// ---------------------------------------------------------       
        /// </summary>
        private void GetSFHotelDetails()
        {            
            DataTable dtSFHotelDetails = null;
            try
            {
                dtSFHotelDetails = SuperViewBLL.GetSFHotelDetailsByID(GlobalCode.Field2String(Session["TravelRequestID"]),
                    Session["strSFCode"].ToString(), Session["strSFStatus"].ToString(), Session["strRecordLocator"].ToString(),
                     GlobalCode.Field2String(Session["ManualRequestID"]));
                uolistviewHotelInfo.DataSource = dtSFHotelDetails;
                uolistviewHotelInfo.DataBind();

                HtmlControl EditTH = (HtmlControl)uolistviewHotelInfo.FindControl("EditTH");
                HtmlControl DeleteTH = (HtmlControl)uolistviewHotelInfo.FindControl("DeleteTH");

                if (EditTH != null)
                {
                    if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                         
                        && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist) 
                        && !User.IsInRole(TravelMartVariable.RoleHotelVendor))
                    {
                        EditTH.Style.Add("display", "none");
                        DeleteTH.Style.Add("display", "none");
                    }
                    else
                    {
                        EditTH.Style.Add("display", "display");
                        DeleteTH.Style.Add("display", "display");
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
        /// Date Modified:  27/10/2011
        /// Modified By:    Charlene Remotigue
        /// (description)   get port currency
        ///  ------------------------------------------------------
        /// </summary>
        private void GetSFPortTravelDetails()
        {            
            DataTable dtSFPortDetails = null;
            try
            {                
                dtSFPortDetails = SuperViewBLL.GetSFPortTravelDetails(GlobalCode.Field2String(Session["TravelRequestID"]), 
                    Session["strRecordLocator"].ToString(),  GlobalCode.Field2String(Session["ManualRequestID"]), 
                    Session["strSFStatus"].ToString());
                uogridviewPortInfo.DataSource = dtSFPortDetails;
                uogridviewPortInfo.DataBind();

                ViewState["CurrencyId"] = dtSFPortDetails.Rows[0]["CURRENCYID"].ToString();
                ViewState["CurrencyName"] = dtSFPortDetails.Rows[0]["CURRENCYNAME"].ToString();
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
        /// Date Created:   19/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Show/Hide controls based from role
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
            //if (User.IsInRole(TravelMartVariable.RoleAdministrator) || User.IsInRole(TravelMartVariable.Role24x7)
            //    || User.IsInRole(TravelMartVariable.RolePortSpecialist))
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
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Show/Hide COntrols
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
            if (ViewPortControls)
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
        /// Date Created:   19/07/2011
        /// Created By:     Josephine Gad
        /// (description)   setup control settings for multiple role
        /// </summary>
        private void LinkSettingsMultipleRole()
        {
            if (User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RoleVehicleSpecialist)
                || User.IsInRole(TravelMartVariable.RoleVehicleVendor))
            {
                uoButtonVehicleAdd.Style.Add("display", "inline");
            }
            else
            {
                uoButtonVehicleAdd.Style.Add("display", "none");
            }
            if (User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
                || User.IsInRole(TravelMartVariable.RoleHotelVendor))
            {
                uoBtnAdd.Style.Add("display", "inline");
            }
            else
            {
                uoBtnAdd.Style.Add("display", "none");
            }
            if (User.IsInRole(TravelMartVariable.RoleAdministrator)                
                || User.IsInRole(TravelMartVariable.RolePortSpecialist))
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
                && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist) 
                && !User.IsInRole(TravelMartVariable.RoleHotelVendor))
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
        /// (description)   Hide Pending Hotel
        /// </summary>
        protected string HideHotelPending()
        {
            if (!User.IsInRole(TravelMartVariable.RoleAdministrator)                
                && !User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
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
                && !User.IsInRole(TravelMartVariable.RoleAirSpecialist))
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
            //if(IsActive.ToString() == "1")          
            Boolean IsActiveBool = (bool)IsActive;
            if (IsActiveBool)
            {
                return "";
            }
            else
            {
                return "inactive";
            }
            //if (ID.ToString() != GlobalCode.Field2String(Session["strTravelLocatorID"]))
            //{
            //    return "inactive";
            //}
            //else
            //{
            //    return "";
            //}
        }
        /// <summary>
        /// Date Created: 08/08/2011
        /// Created By: Josephine Gad
        /// (description) Change the backgroung color of old record
        /// </summary>
        protected bool InactiveControl(object IsActive, object BranchID, object Type)
        {
            Boolean IsActiveBool = (bool)IsActive;
            if (IsActiveBool)
            {
                if (Type.ToString() == "HO")
                {
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
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
            if (uclabelStatus.Text == "Onsigning")
            {
                oDate = OnDate;
            }
            else if (uclabelStatus.Text == "Offsigning")
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
                    dr = UserAccountBLL.GetUserBranchDetails(GlobalCode.Field2String(Session["UserName"]), uoHiddenFieldRole.Value);
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
        /// Date Created:   28/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Event notification
        protected bool IsEventExists(object BranchID)
        {
            string sDateEvent = (uoHdSfStatus.Value == "ON" ? uoHiddenFieldDateOn.Value : uoHiddenFieldDateOff.Value);
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

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void SuperUserLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Seafarer itinerary information viewed.";
            strFunction = "SuperUserLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion               

       
    }
}

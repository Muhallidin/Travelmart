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

namespace TRAVELMART.Hotel
{
    public partial class HotelViewNoTransaction : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        string Role = string.Empty;
        #region EVENTS
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
                Session["ViewLeftMenu"] = "0";
                Role = GlobalCode.Field2String(Session["UserRole"]);
                uoHiddenFieldRole.Value = Role;

                DateTime dFrom = DateTime.Parse(Request.QueryString["dt"].ToString());
                DateTime dTo = dFrom;

                uoHiddenFieldStartDate.Value = dFrom.ToString("MM/dd/yyyy");
                uoHiddenFieldEndDate.Value = dTo.ToString("MM/dd/yyyy");

                Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");

                if (TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserRole"]))
                {
                    //uoHiddenFieldUserBrand.Value = GlobalCode.Field2String(Session["UserVendor"]);
                    uoHiddenFieldUserBranch.Value = GlobalCode.Field2String(Session["UserBranchID"]);

                    //LoadDashboardDetails();
                    //LoadSeafarers();
                    //uoHyperLinkAdd.Visible = false;
                    uoPanelPending.Visible = false;
                    uoButtonApproved.Visible = false;
                    //PendingDiv.Visible = false;

                }
                else
                {
                    uoHiddenFieldUserBranch.Value = Request.QueryString["branchId"];
                    //uoHiddenFieldUserBrand.Value = Request.QueryString["brandId"];
                    //uoHiddenFieldUserRegion.Value = GlobalCode.Field2String(Session["Region"]);
                    uoLabelBranchName.Text = "(" + Request.QueryString["branchName"].ToString() + ")";
                    GetSFHotelTravelNoDetails();
                    //PendingDetailsDiv.Visible = false;

                    //uoHyperLinkAdd.HRef = "~/Hotel/HotelRoomOverride.aspx?bId=0&hrId=0&rId=" + uoHiddenFieldUserRegion.Value;

                }
                EventNotification();
            }
         }



        private void GetSFHotelTravelNoDetails()
        {
            try
            {
                uoHotelList.Items.Clear();
                uoHotelList.DataSource = null;
                uoHotelList.DataSourceID = "ObjectDataSource1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        protected void ObjectDataSource2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["DateFrom"] = uoHiddenFieldStartDate.Value;
            e.InputParameters["DateTo"] =  uoHiddenFieldStartDate.Value;

            e.InputParameters["UserID"] = GlobalCode.Field2String(Session["UserName"]);
            e.InputParameters["FilterByDate"] = GlobalCode.Field2String(Session["strPendingFilter"]);
            e.InputParameters["RegionID"] =GlobalCode.Field2String(Session["Region"]);
            e.InputParameters["CountryID"] = GlobalCode.Field2String(Session["Country"]);
            e.InputParameters["CityID"] = GlobalCode.Field2String(Session["City"]);
            e.InputParameters["Status"] = "";
            e.InputParameters["FilterByNameID"] = "1";
            e.InputParameters["FilterNameID"] = "";
            e.InputParameters["PortID"] = GlobalCode.Field2String(Session["Port"]);
            e.InputParameters["VesselID"] = "0";
            e.InputParameters["Nationality"] = "0";
            e.InputParameters["Gender"] = "0";
            e.InputParameters["Rank"] = "0";
            e.InputParameters["Role"] = uoHiddenFieldRole.Value;

            e.InputParameters["ByVessel"] = 1;
            e.InputParameters["ByName"] = 2;
            e.InputParameters["ByE1ID"] = 0;
            e.InputParameters["ByDateOnOff"] = 0;
            e.InputParameters["ByStatus"] = 0;
            e.InputParameters["ByPort"] = 0;
            e.InputParameters["ByRank"] = 0;
            e.InputParameters["BranchId"] = GlobalCode.Field2Int(uoHiddenFieldUserBranch.Value);
        }


        protected void uoButtonApproved_Click(object sender, EventArgs e)
        {
            //ApproveRequest();
        }

        protected void uoHotelListPager_PreRender(object sender, EventArgs e)
        {
            //GetSFHotelTravelDetails();
        }
        #endregion

        #region FUNCTIONS
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
                return string.Format("<tr><td class=\"group\" colspan=\"11\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
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
        /// Date Created:   04/01/2011
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

                string ApproveByString = GlobalCode.Field2String(Session["UserName"]);
                foreach (ListViewItem item in uoHotelList.Items)
                {
                    CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                    bool IsApprovedBool = CheckSelect.Checked;

                    if (IsApprovedBool)
                    {
                        //HiddenField HiddenID = (HiddenField)item.FindControl("uoHiddenFieldIdBigInt");
                        //HiddenField BranchID = (HiddenField)item.FindControl("uoHiddenFieldBranchIDInt");
                        //Label HotelName = (Label)item.FindControl("uoLabelHotelName");
                        //Label E1ID = (Label)item.FindControl("uoLabelE1ID");
                        //Label CheckIndate = (Label)item.FindControl("uoLabelCheckinDate");
                        //LinkButton Name = (LinkButton)item.FindControl("SeafarerLinkButton");

                        //string pendingID = HiddenID.Value;

                        //dTable = HotelBLL.HotelApproveTransaction(pendingID, ApproveByString);
                        //dr = VendorMaintenanceBLL.vehicleVendorBranchMaintenanceInformation(Int32.Parse(BranchID.Value));
                        //if (dr.Read())
                        //{
                        //    string sMsg = "Booking of seafarer <b>" + Name.Text + "</b> with E1 ID <b>" + E1ID.Text + "</b> has been approved. <br/><br/>";
                        //    sMsg += "Hotel Branch: " + HotelName.Text + "<br/>";
                        //    sMsg += "Checkin Date: " + CheckIndate.Text + "";
                        //    sMsg += "<br/><br/>Thank you.";
                        //    sMsg += "<br/><br/><i>Auto generated email.</i>";

                        //    SendEmail("Travelmart: Pending Hotel Booking Approval", sMsg, dr["colCountryIDInt"].ToString(), BranchID.Value);
                        //}

                        ////Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                        //strLogDescription = "Hotel pending transaction approved.";
                        //strFunction = "ApproveRequest";

                        //DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        //BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(dTable.Rows[0]["PK"].ToString()), dTable.Rows[0]["SeqNum"].ToString(), strLogDescription, strFunction, Path.GetFileName(Request.Path),
                        //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                        ////BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pendingID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                        ////                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }
                }
                AlertMessage("Approved!");

                GetSFHotelTravelNoDetails();
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
        #endregion
    }
}

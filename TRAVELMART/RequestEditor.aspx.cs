using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.IO;

namespace TRAVELMART
{
    public partial class RequestEditor : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Date Modified:  16/03/2012
        /// Modified By:    Charlene Remotigue
        /// (description)   Use Global Code for parsing and casting         
        /// -------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleVehicleVendor)
            //{
            //    if (GlobalCode.Field2String(Session["UserCountry"]) == "0" || GlobalCode.Field2String(Session["UserCountry"]) == "")
            //    {
            //        GetBranchInfo();
            //    }
            //}
            if (!IsPostBack)
            {
                RequestLogAuditTrail();
                uoHiddenFieldRequestID.Value = Request.QueryString["id"].ToString();

                if (GlobalCode.Field2String(Session["UserRole"])=="")
                {
                    string UserRolePrimary = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
                    Session["UserRole"] = UserRolePrimary;
                    GetBranchInfo();
                }
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
                //if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                //{
                //    if (GlobalCode.Field2String(Session["UserCountry"]) == "0" || GlobalCode.Field2String(Session["UserCountry"]) == "")
                //    {
                //        GetBranchInfo();
                //    }
                //}
                //uoHiddenFieldRole.Value = MUser.GetUserRole();

                GetSeafarer();
                if (uoHiddenFieldRequestID.Value != "0")
                {
                    GetRequestDetails();
                }
            }
        }

        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            GetSeafarer();
        }
        protected void uoDropDownListSeafarer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSeafarerDetails(uoDropDownListSeafarer.SelectedValue);
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveRequest();
            CloseThisPage();
        }
        protected void uoDropDownListVessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSeafarerDetails(uoDropDownListSeafarer.SelectedValue);
            GetVessel(uoHiddenFieldVesselID.Value);
            GetVesselDetails();
            GetPortAgent();
            //uoTextBoxFilterCity.Text = uoTextBoxCity.Text;
            //            GetCity();
            GetHotelBranch();
        }
        protected void uoButtonVieAirport_Click(object sender, EventArgs e)
        {
            //            GetCity();
            GetHotelBranch();
        }
        //protected void uoDropDownListAirport_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetHotelBranch();
        //}

        #endregion

        #region "Procedures"

        #region "Binding Objects"
        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Seafarer list
        /// ---------------------------------------------
        /// Date Modified:   17/12/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change Datatable to List
        /// ---------------------------------------------
        /// </summary>
        private void GetSeafarer()
        {
            List<SeafarerListDTO> list = null;
            try
            {
                list = SeafarerBLL.GetSeafarerListByFilter(uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim());
                ListItem item = new ListItem("--SELECT SEAFARER--", "0");
                uoDropDownListSeafarer.Items.Clear();
                uoDropDownListSeafarer.Items.Add(item);

                var listSeafarer = (from a in list
                                    select new { 
                                    Name = a.Name,
                                    SFID = a.SFID
                                    }).ToList();

                uoDropDownListSeafarer.DataSource = listSeafarer;
                uoDropDownListSeafarer.DataTextField = "Name";
                uoDropDownListSeafarer.DataValueField = "SFID";
                uoDropDownListSeafarer.DataBind();

                if (listSeafarer.Count == 1)
                {
                    uoDropDownListSeafarer.SelectedIndex = 1;
                    
                }
                else
                {
                    ClearSeafererDetails();
                    if (GlobalCode.Field2String(Request.QueryString["sfId"]) != "")
                    {
                        uoDropDownListSeafarer.SelectedValue = GlobalCode.Field2String(Request.QueryString["sfId"]);
                        GetSeafarerDetails(uoDropDownListSeafarer.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }
            //DataTable dt = null;
            //try
            //{
            //    dt = SeafarerBLL.GetSeafarerByFilter(uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim());
            //    ListItem item = new ListItem("--SELECT SEAFARER--", "0");
            //    uoDropDownListSeafarer.Items.Clear();
            //    uoDropDownListSeafarer.Items.Add(item);
            //    uoDropDownListSeafarer.DataSource = dt;
            //    uoDropDownListSeafarer.DataTextField = "Name";
            //    uoDropDownListSeafarer.DataValueField = "SFID";
            //    uoDropDownListSeafarer.DataBind();

            //    if (dt.Rows.Count == 1)
            //    {
            //        uoDropDownListSeafarer.SelectedIndex = 1;
            //        GetSeafarerDetails(uoDropDownListSeafarer.SelectedValue);
            //    }
            //    else
            //    {
            //        ClearSeafererDetails();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Seafarer details by E1 ID
        /// ---------------------------------------------
        /// Date Modified:   17/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------
        /// </summary>
        /// <param name="E1ID"></param>
        private void GetSeafarerDetails(string E1ID)
        {
            List<SeafarerDTO> list = null;
            try
            {
                list = SeafarerBLL.GetSeafarerByFilter("2", E1ID);
                if (list.Count > 0)
                {
                    uoTextBoxNationality.Text = list[0].Nationality;//dr["Nationality"].ToString();
                    uoTextBoxGender.Text = list[0].Gender;//dr["Gender"].ToString();
                    uoTextBoxRank.Text = list[0].Rank;//dr["Rank"].ToString();
                    uoTextBoxCostCenter.Text = list[0].CostCenter;//dr["CostCenter"].ToString();

                    uoHiddenFieldNationalityID.Value = list[0].NationalityID;//dr["NationalityID"].ToString();
                    uoHiddenFieldRankID.Value = list[0].RankID;//dr["RankID"].ToString();
                    uoHiddenFieldCostCenterID.Value = list[0].CostCenterID;//dr["CostCenterID"].ToString();
                }
                else
                {
                    ClearSeafererDetails();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:    04/10/2011
        /// Created By:      Josephine Gad
        /// (description)    Get Vessel list
        /// ---------------------------------------------
        /// Date Modified:   14/02/2011
        /// Modified By:     Josephine Gad
        /// (description)    Use class VesselDTOList.VesselList instead of calling function VesselBLL.GetVessel again
        /// ---------------------------------------------
        /// </summary>
        /// <param name="vesselID"></param>
        private void GetVessel(string vesselID)
        {
            List<VesselDTO> vesselList = null;
            try
            {
                //VesselDTOList.VesselList
                uoDropDownListVessel.Items.Clear();
                uoDropDownListVessel.Items.Add(new ListItem("--SELECT SHIP--", "0"));

                if (VesselDTOList.VesselList != null)
                {
                    vesselList = VesselDTOList.VesselList;
                }
                else
                {
                    vesselList = VesselBLL.GetVesselList(GlobalCode.Field2String(Session["UserName"]), uoTextBoxDate.Text,
                        uoTextBoxDate.Text, "0", "0", "0", "0", uoHiddenFieldRole.Value, false);
                }

                var listVessel = (from a in vesselList
                                  select new
                                  {
                                      VesselName = a.VesselNameString,
                                      VesselID = a.VesselIDString
                                  }
                                ).ToList();
                uoDropDownListVessel.DataSource = listVessel;
                uoDropDownListVessel.DataTextField = "VesselName";
                uoDropDownListVessel.DataValueField = "VesselID";
                uoDropDownListVessel.DataBind();

                uoDropDownListVessel.SelectedValue = vesselID;
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
        /// Date Created:   05/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel details by Vessel ID
        /// ---------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        public void GetVesselDetails()
        {            
            IDataReader dr = null;
            try
            {
                dr = VesselBLL.GetVesselPortDetails(uoDropDownListVessel.SelectedValue,
                    uoTextBoxDate.Text, uoTextBoxDate.Text);
                if (dr.Read())
                {
                    uoTextBoxPort.Text = dr["PortName"].ToString();
                    //uoTextBoxCity.Text = dr["CityName"].ToString();
                    uoTextBoxCountry.Text = dr["CountryName"].ToString();

                    uoHiddenFieldPortID.Value = dr["PortID"].ToString();
                   // uoHiddenFieldCityID.Value = dr["CityID"].ToString();
                    uoHiddenFieldCountryID.Value = dr["CountryID"].ToString();
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
        /// Date Created:   07/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get City List by Country
        /// ---------------------------------------------
        /// </summary>
        //private void GetCity()
        //{
        //    DataTable dt = null;
        //    try 
        //    {
        //        dt = CityBLL.GetCityByCountry(uoHiddenFieldCountryID.Value, uoTextBoxFilterCity.Text.Trim(), "0");
        //        ListItem item = new ListItem("--SELECT CITY--", "0");
        //        uoDropDownListCity.Items.Clear();
        //        uoDropDownListCity.Items.Add(item);
        //        if (dt.Rows.Count > 0)
        //        {
        //            uoDropDownListCity.DataSource = dt;                    
        //        }
        //        uoDropDownListCity.DataBind();

        //        if (dt.Rows.Count == 1)
        //        {
        //            uoDropDownListCity.SelectedIndex = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}
        /// <summary>       
        /// Date Created:   05/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Hotel List By City
        /// ---------------------------------------------
        /// Date Modified:   14/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------
        /// </summary>
        private void GetHotelBranch()
        {
            List<HotelDTO> hotelList = null;
            try
            {
                hotelList = HotelBLL.GetHotelBranchByRegionPortCountry(GlobalCode.Field2String(Session["UserName"]), "0", "0", "0", "0");
                //HotelBLL.GetHotelBranchByCity("", uoDropDownListCity.SelectedValue);
                var listHotel = (from a in hotelList
                                 select new
                                 {
                                     HotelID = a.HotelIDString,
                                     HotelName = a.HotelNameString
                                 }).ToList();

                uoDropDownListHotel.Items.Clear();
                ListItem item = new ListItem("--SELECT HOTEL--", "0");
                uoDropDownListHotel.Items.Add(item);

                uoDropDownListHotel.DataSource = listHotel;
                uoDropDownListHotel.DataTextField = "HotelName";
                uoDropDownListHotel.DataValueField = "HotelID";
                uoDropDownListHotel.DataBind();

                if (hotelList.Count == 1)
                {
                    uoDropDownListHotel.SelectedIndex = 1;
                }
                else if (hotelList.Count > 0)
                {
                    if (GlobalCode.Field2String(Session["Hotel"]) != "")
                    {
                        if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                        {
                            uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
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
                if (hotelList != null)
                {
                    hotelList = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   05/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider list by Port ID
        /// ---------------------------------------------
        /// </summary>
        /// <param name="vesselID"></param>
        private void GetPortAgent()
        {
            List<PortAgentDTO> portAgentList = null;
            try
            {
                if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    portAgentList = PortBLL.GetPortAgentByPortID(uoHiddenFieldPortID.Value, GlobalCode.Field2String(Session["UserBranchID"]));

                }
                else
                {
                    portAgentList = PortBLL.GetPortAgentByPortID(uoHiddenFieldPortID.Value, "0");
                }
                var listPortAgent = (from a in portAgentList
                                     select new
                                     {
                                         PortAgenName = a.PortAgentName,
                                         PortAgentID = a.PortAgentID
                                     }).ToList();

                uoDropDownListPortAgent.Items.Clear();
                uoDropDownListPortAgent.Items.Add(new ListItem("--SELECT Service Provider--", "0"));
                uoDropDownListPortAgent.Items.Add(new ListItem("Other", "1"));

                uoDropDownListPortAgent.DataSource = listPortAgent;
                uoDropDownListPortAgent.DataTextField = "PortAgenName";
                uoDropDownListPortAgent.DataValueField = "PortAgentID";
                uoDropDownListPortAgent.DataBind();

                if (listPortAgent.Count == 1)
                {
                    uoDropDownListPortAgent.SelectedIndex = 1;
                }
                else if (listPortAgent.Count > 0)
                {
                    if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                    {
                        if (uoDropDownListPortAgent.Items.FindByValue(GlobalCode.Field2String(Session["UserBranchID"])) != null)
                        {
                            uoDropDownListPortAgent.SelectedValue = GlobalCode.Field2String(Session["UserBranchID"]);
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
                if (portAgentList != null)
                {
                    portAgentList = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Clear seafarer's details
        /// ---------------------------------------------
        /// </summary>
        private void ClearSeafererDetails()
        {
            uoTextBoxNationality.Text = "";
            uoTextBoxGender.Text = "";
            uoTextBoxRank.Text = "";
            uoTextBoxCostCenter.Text = "";

            uoHiddenFieldNationalityID.Value = "";
            uoHiddenFieldRankID.Value = "";
            uoHiddenFieldCostCenterID.Value = "";
        }
        /// <summary>
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)  Get Request details by ID
        /// ---------------------------------------------
        /// </summary>
        private void GetRequestDetails()
        {
            IDataReader r = null;
            try
            {
                r = RequestBLL.GetRequestDetailsByID(uoHiddenFieldRequestID.Value);
                if (r.Read())
                {
                    uoDropDownListSeafarer.SelectedValue = r["SfID"].ToString();
                    GetSeafarerDetails(r["SfID"].ToString());

                    uoTextBoxDate.Text = DateTime.Parse(r["OnOffDate"].ToString()).ToString("MM/dd/yyyy");
                    uoRadioButtonListOnOff.SelectedValue = r["Status"].ToString().ToUpper();
                    GetVessel(r["VesselID"].ToString());
                    GetVesselDetails();
                    GetPortAgent();
                    uoDropDownListPortAgent.SelectedValue = r["PortAgentID"].ToString();

                    uoCheckBoxHotel.Checked = (bool)r["IsNeedHotel"];

                    //                    GetHotelDetails(r["BranchID"].ToString());
                    //                  uoTextBoxFilterCity.Text = uoTextBoxCity.Text;
                    //                    GetCity();
                    //uoDropDownListCity.SelectedValue = uoHiddenFieldHotelCity.Value;
                    GetHotelBranch();
                    if (uoCheckBoxHotel.Checked)
                    {
                        uoHiddenFieldHotelID.Value = r["BranchID"].ToString();
                        uoDropDownListHotel.SelectedValue = r["BranchID"].ToString();
                        uoTextBoxCheckinDate.Text = DateTime.Parse(r["CheckinDate"].ToString()).ToString("MM/dd/yyyy");
                        uoTextBoxCheckoutDate.Text = DateTime.Parse(r["CheckoutDate"].ToString()).ToString("MM/dd/yyyy");
                    }

                    uoCheckBoxTransportation.Checked = (bool)r["IsNeedTranspo"];
                    if (uoCheckBoxTransportation.Checked)
                    {
                        uoDropDownListOrigin.SelectedValue = r["Origin"].ToString();
                        if (r["Origin"].ToString().ToLower() == "other")
                        {
                            uoTextBoxOrigin.Text = r["OriginRemarks"].ToString();
                        }
                        uoDropDownListDestination.SelectedValue = r["Destination"].ToString();
                        if (r["Destination"].ToString().ToLower() == "other")
                        {
                            uoTextBoxDestination.Text = r["DestinationRemarks"].ToString();
                        }
                        uoTextBoxPickupdate.Text = DateTime.Parse(r["PickupDate"].ToString()).ToString("MM/dd/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (r != null)
                {
                    r.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   17/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel details by BranchID
        /// ----------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// <param name="BranchID"></param>
        private void GetHotelDetails(string BranchID)
        {
            //BranchID = (BranchID == "" ? "0" : BranchID);
            ////uoTextBoxFilterCity.Text = "";
            //uoTextBoxFilterAirport.Text = "";
            //IDataReader dr = null;
            //try
            //{
            //    dr = VendorMaintenanceBLL.GetBranchDetails(BranchID);
            //    if (dr.Read())
            //    {
            //        uoHiddenFieldHotelCity.Value = dr["CityID"].ToString();
            //        //uoTextBoxFilterCity.Text = dr["BranchCity"].ToString();
            //        //uoTextBoxFilterAirport
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dr != null)
            //    {
            //        dr.Dispose();
            //    }
            //}
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
                dr = UserAccountBLL.GetUserBranchDetails(MUser.GetUserName(), uoHiddenFieldRole.Value);
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
        /// Date Created:   05/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Close this page
        /// ----------------------------------------------------
        /// </summary>
        private void CloseThisPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " self.close (); ";
            if (uoHiddenFieldRequestID.Value != "0")
            {
                sScript += " window.opener.RefreshPageFromPopup(); ";
            }
            else if (Request.QueryString["r"] != null)
            {
                sScript += " window.opener.RefreshPageFromPopup(); ";
            }
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
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
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        #endregion


        #region "Insert/Update/Delete"
        /// <summary>
        /// Date Created:   06/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Saving manual request for hotel/vehicle
        /// ----------------------------------------------------
        /// </summary>
        private void SaveRequest()
        {
            string strLogDescription;
            string strFunction;

            if (uoHiddenFieldRequestID.Value == "0")
            {

                Int32 pRequestID = RequestBLL.RequestInsert("0", uoDropDownListSeafarer.SelectedValue, uoHiddenFieldVesselID.Value,
                    uoTextBoxDate.Text, uoRadioButtonListOnOff.SelectedValue, uoCheckBoxHotel.Checked,
                    uoHiddenFieldHotelID.Value, uoTextBoxCheckinDate.Text, uoTextBoxCheckoutDate.Text,
                    uoCheckBoxTransportation.Checked, uoDropDownListOrigin.SelectedValue, uoTextBoxOrigin.Text,
                    uoDropDownListDestination.SelectedValue, uoTextBoxDestination.Text, uoTextBoxPickupdate.Text,
                    uoHiddenFieldPortID.Value, "", uoDropDownListPortAgent.SelectedValue,
                    uoHiddenFieldCostCenterID.Value, uoHiddenFieldRankID.Value, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Hotel/Vehicle manual request added.";
                strFunction = "SaveRequest";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(pRequestID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
                RequestBLL.RequestUpdate(uoHiddenFieldRequestID.Value, uoDropDownListSeafarer.SelectedValue, uoDropDownListVessel.SelectedValue,
                    uoTextBoxDate.Text, uoRadioButtonListOnOff.SelectedValue, uoCheckBoxHotel.Checked,
                    uoDropDownListHotel.SelectedValue, uoTextBoxCheckinDate.Text, uoTextBoxCheckoutDate.Text,
                    uoCheckBoxTransportation.Checked, uoDropDownListOrigin.SelectedValue, uoTextBoxOrigin.Text,
                    uoDropDownListDestination.SelectedValue, uoTextBoxDestination.Text, uoTextBoxPickupdate.Text,
                    uoHiddenFieldPortID.Value, "", uoDropDownListPortAgent.SelectedValue,
                    uoHiddenFieldCostCenterID.Value, uoHiddenFieldRankID.Value, GlobalCode.Field2String(Session["UserName"]));

                //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                strLogDescription = "Hotel/Vehicle manual request updated.";
                strFunction = "SaveRequest";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(uoHiddenFieldRequestID.Value), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }            
        }

        #endregion

        private void BindAirport()
        {
            
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void RequestLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["id"] != null)
            {
                strLogDescription = "View linkbutton for request editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for request editor clicked.";
            }

            strFunction = "RequestLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, GlobalCode.Field2String(Session["RequestPath"]),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion       
    }
}

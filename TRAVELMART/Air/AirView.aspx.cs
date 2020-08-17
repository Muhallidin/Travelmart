using System;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace TRAVELMART
{
    public partial class AirView : System.Web.UI.Page
    {
        #region Events
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  15/08/2012
        /// Modified By:    Josephine Gad
        /// (description)   Get uoHiddenFieldDateRange.Value from UserAccountList
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");

                List<UserAccountList> listUser = GetUserAccountList(uoHiddenFieldUser.Value);
                uoHiddenFieldDateRange.Value = GlobalCode.Field2String(listUser[0].iDayNo);//UserAccountBLL.GetUserDateRange(uoHiddenFieldUser.Value).ToString();
               
                uoHiddenFieldDateTo.Value =GlobalCode.Field2String( Session["DateTo"]);
                if (uoHiddenFieldDateTo.Value == "")
                {
                    uoHiddenFieldDateTo.Value = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value)).ToString("MM/dd/yyyy");
                }
                Session["ViewHotel"] = "0";
                Image uoImageHotel = (Image)Master.FindControl("uoImageHotel");
                uoImageHotel.Style.Add("display", "none");
                HtmlControl spanHotel = (HtmlControl)Master.FindControl("spanHotel");
                spanHotel.Style.Add("display", "none");
                DropDownList uoDropDownListHotel = (DropDownList)Master.FindControl("uoDropDownListHotel");
                uoDropDownListHotel.Style.Add("display", "none");
                
                GetVessel();
                GetNationality();
                GetGender();
                GetRank();
                GetSFAirTravelDetails();
                Session["strPrevPage"] = Request.RawUrl;
            }
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetSFAirTravelDetails();
        }
        //protected void uoDropDownListMapRef_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetSFAirTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), TravelMartVariable.strAirStatusFilter);
        //}
        protected void uoListViewAirTravelInfo_PreRender(object sender, EventArgs e)
        {

        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
            GetSFAirTravelDetails();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 08/07/2011
        /// Created By: Marco Abejar
        /// (description) Get seafarer air travel info
        /// ----------------------------------------------------------------
        /// Date Created: 22/07/2011
        /// Modifed By: Josephine Gad
        /// (description) uogridviewAirTravelInfo to uoListViewAirTravelInfo
        /// ----------------------------------------------------------------
        /// Date Created: 02/08/2011
        /// Modifed By: Josephine Gad
        /// (description) Add parameter seafarer name        
        /// ----------------------------------------------------------------
        /// Date Modified: 05/08/2011
        /// Modifed By: Josephine Gad
        /// (description) Add Date Range parameter
        /// </summary>
        private void GetSFAirTravelDetails()
        {           
            DataTable AirDataTable = null;
            try
            {
                //string DateFromString = (uoHiddenFieldDateFrom.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateFrom.Value);
                //string DateToString = (uoHiddenFieldDateTo.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateTo.Value);

                //AirDataTable = SeafarerTravelBLL.GetSFAirTravelDetails(strSFStatus, strFlightDateRange, strAirStatusFilter, uoTextBoxName.Text,
                //    DateFromString, DateToString, GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["Region"]), uoHiddenFieldFilterBy.Value);
                AirDataTable = SeafarerTravelBLL.GetSFAirTravelDetails(GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(), uoHiddenFieldDateTo.Value,
                    GlobalCode.Field2String(Session["strAirStatusFilter"]), GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["strPendingFilter"]),
                    GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]), GlobalCode.Field2String(Session["City"]),
                    uoDropDownListStatus.SelectedValue, uoDropDownListFilterBy.SelectedValue, uoTextBoxFilter.Text.Trim(),
                    GlobalCode.Field2String(Session["Port"]), uoDropDownListVessel.SelectedValue, uoDropDownListNationality.SelectedValue,
                    uoDropDownListGender.SelectedValue, uoDropDownListRank.SelectedValue);
                   
                uoListViewAirTravelInfo.DataSource = AirDataTable;
                uoListViewAirTravelInfo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (AirDataTable != null)
                {
                    AirDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   30/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetVessel()
        {
            DataTable VesselDataTable = null;
            try
            {
                string UserName = GlobalCode.Field2String(Session["UserName"]);
                string UserRole = GlobalCode.Field2String(Session["UserRole"]);
                if (UserRole == "")
                {
                    UserRole = UserAccountBLL.GetUserPrimaryRole(UserName);
                }
                VesselDataTable = VesselBLL.GetVessel(UserName, GlobalCode.Field2DateTime((uoHiddenFieldDate.Value)).ToString(),
                    uoHiddenFieldDateTo.Value, GlobalCode.Field2String(Session["Region"]), GlobalCode.Field2String(Session["Country"]),
                    GlobalCode.Field2String(Session["City"]), GlobalCode.Field2String(Session["Port"]), UserRole);
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
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Get user details using session
        /// </summary>
        /// <returns></returns>
        private List<UserAccountList> GetUserAccountList(string sUserName)
        {
            List<UserAccountList> list = new List<UserAccountList>();

            if (Session["UserAccountList"] != null)
            {
                list = (List<UserAccountList>)Session["UserAccountList"];
            }
            else
            {
                list = UserAccountBLL.GetUserInfoListByName("sUserName");
                Session["UserAccountList"] = list;
            }
            return list;
        }
        //private void BindMapRef()
        //{
        //    DataTable MapRefDataTable = null;
        //    try
        //    {
        //        MapRefDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
        //        uoDropDownListMapRef.Items.Clear();
        //        ListItem item = new ListItem("--Select Region--", "0");
        //        uoDropDownListMapRef.Items.Add(item);
        //        uoDropDownListMapRef.DataSource = MapRefDataTable;
        //        uoDropDownListMapRef.DataTextField = "colMapNameVarchar";
        //        uoDropDownListMapRef.DataValueField = "colMapIDInt";
        //        uoDropDownListMapRef.DataBind();

        //        //CommonFunctions.ChangeToUpperCase(uoDropDownListMapRef);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (MapRefDataTable != null)
        //        {
        //            MapRefDataTable.Dispose();
        //        }
        //    }
        //}
        #endregion 
    }
}

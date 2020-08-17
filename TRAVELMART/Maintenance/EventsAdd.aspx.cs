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

namespace TRAVELMART.Maintenance
{
    public partial class EventsAdd : System.Web.UI.Page
    {
        private MasterfileBLL masterBLL = new MasterfileBLL();
        #region EVENTS
        /// <summary>
        /// Date Created:   12/10/2011
        /// Created By:     Charlene Remotigue
        /// -------------------------------------------
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["eId"] != "0")
                {
                    uoTextBoxEventName.Text = Request.QueryString["eName"].ToString();
                    LoadDetails();
                    UpdatePanel1.Update();
                }
                else
                {
                    SetDefault();
                    BindRegion();
                }

            }
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            uoDropDownListLetters.SelectedValue = "0";
            BindCity();
            uoTextBoxCity.Text = "";
            
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountry();
        }

        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLetters();
        }

        protected void uoDropDownListLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoTextBoxCity.Text = "";
            BindCity();
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindHotel();
        }

        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            Int32 pEventID = MasterfileBLL.EventsAddMaintenanceSave(Request.QueryString["eId"].ToString(), uoTextBoxEventName.Text,
                 Convert.ToDateTime(uoTextBoxFrom.Text), Convert.ToDateTime(uoTextBoxTo.Text), uoDropDownListHotel.SelectedValue,
                 uoDropDownListCity.SelectedValue, GlobalCode.Field2String(Session["UserName"]), uoTextBoxRemarks.Text);

            string sMessage;
            sMessage = "Hotel Name: <b>" + uoDropDownListHotel.SelectedItem.Text + "</b>";
            sMessage += "<br/>";
            sMessage += "Country: " + uoDropDownListCountry.SelectedItem.Text;
            sMessage += "<br/>";
            sMessage += "City: " + uoDropDownListCity.SelectedItem.Text;
            sMessage += "<br/>";
            sMessage += "Event Name: " + uoTextBoxEventName.Text;
            sMessage += "<br/>";
            sMessage += "Date From: " + Convert.ToDateTime(uoTextBoxFrom.Text).ToString("dd-MMM-yyyy");
            sMessage += "<br/>";
            sMessage += "Date To: " + Convert.ToDateTime(uoTextBoxTo.Text).ToString("dd-MMM-yyyy");
            sMessage += "<br/>";
            if (uoTextBoxRemarks.Text.Trim() != "")
            {
                sMessage += "Remarks: " + uoTextBoxRemarks.Text;
            }

            if (Request.QueryString["eId"] == "0" || Request.QueryString["eId"] == null)
            {

               // HotelSendEmail("Travelmart: New event added", "There is new added event. <br/><br/>" + sMessage);
                
                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Event added.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(pEventID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
            else
            {
//                HotelSendEmail("Travelmart: Event updated", "There is an updated event. <br/><br/>" + sMessage);

                //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                strLogDescription = "Event updated.";
                strFunction = "uoButtonSave_Click";

                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(pEventID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }

            OpenParentPage();
        }
        #endregion

        #region METHODS   
        /// <summary>
        /// Created By: Charlene Remotigue
        /// Date Created: 14/10/11
        /// Description: Sets default values
        /// </summary>
        private void SetDefault()
        {
            uoTextBoxFrom.Text = DateTime.Now.ToShortDateString();
            uoTextBoxTo.Text = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: load event details if edit
        /// </summary>
        private void LoadDetails()
        {
            IDataReader dr = null;
            try
            {
                dr = masterBLL.EventsAddMaintenanceLoadDetails(Request.QueryString["eId"]);
                if (dr.Read())
                {
                    DateTime dateFrom = Convert.ToDateTime(dr["colEventDateFromDate"]);
                    DateTime dateTo = Convert.ToDateTime(dr["colEventDateToDate"]);
                    uoTextBoxFrom.Text = dateFrom.ToShortDateString();
                    uoTextBoxTo.Text = dateTo.ToShortDateString();
                    BindRegion();
                    uoDropDownListRegion.SelectedValue = dr["colMapIDInt"].ToString();
                    BindCountry();
                    uoDropDownListCountry.SelectedValue = dr["colCountryIDInt"].ToString();
                    BindLetters();
                    uoDropDownListLetters.SelectedValue = dr["colCityNameVarchar"].ToString().Remove(1);
                    BindCity();
                    uoDropDownListCity.SelectedValue = dr["colCityIdInt"].ToString();
                    BindHotel();
                    uoDropDownListHotel.SelectedValue = dr["colBranchIdInt"].ToString();
                    uoTextBoxRemarks.Text = dr["colRemarksText"].ToString();
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
        /// Created By: Charlene Remotigue
        /// Date Created: 12/10/11
        /// Description: Binds regions to dropdownlist
        /// ---------------------------------------------------------------------------
        /// Date Modified : 26/01/2012
        /// Modified By:    Gelo Oquialda
        /// (description)   change MapRefrence to Region
        /// </summary>
        /// 
        private void BindRegion()
        {
            DataTable RegionDataTable = null;
            try
            {
                //RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListRegion.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListRegion.Items.Add(item);

                uoDropDownListRegion.DataSource = RegionDataTable;
                uoDropDownListRegion.DataTextField = "colRegionNameVarchar";
                uoDropDownListRegion.DataValueField = "colRegionIDInt";
                uoDropDownListRegion.DataBind();

                UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RegionDataTable != null)
                {
                    RegionDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds country to dropdownlist
        /// </summary>
        private void BindCountry()
        {
            DataTable CountryDataTable = null;
            try
            {
                ViewState["cityQueryCount"] = 0;
                CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, "");
                uoDropDownListCountry.Items.Clear();
                ListItem item = new ListItem("--Select Country--", "0");
                uoDropDownListCountry.Items.Add(item);
                uoDropDownListCountry.DataSource = CountryDataTable;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CountryDataTable != null)
                {
                    CountryDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// Description: populates dropdownlist with a-z
        /// </summary>
        private void BindLetters()
        {

            int cInt = 65;
            char cLetter = 'A';
            uoDropDownListLetters.Items.Clear();
            ListItem item = new ListItem("--", "0");
            uoDropDownListLetters.Items.Add(item);
            item = new ListItem("+", "+");
            uoDropDownListLetters.Items.Add(item);
            while (cLetter <= 'Z')
            {
                item = new ListItem(cLetter.ToString(), cLetter.ToString());
                uoDropDownListLetters.Items.Add(item);
                cInt += 1;
                cLetter = Convert.ToChar(cInt);
            }
        }

        /// <summary>
        /// Date Created: 11/10/2011
        /// Created By: Charlene Remotigue
        /// Description: binds city to uodropdownlistcity
        /// </summary>
        private void BindCity()
        {
            DataTable CityDataTable = null;

            try
            {

                CityDataTable = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxCity.Text, uoDropDownListLetters.SelectedValue);
                uoDropDownListCity.Items.Clear();
                ListItem item = new ListItem("--Select City--", "0");

                uoDropDownListCity.Items.Add(item);
                uoDropDownListCity.DataSource = CityDataTable;
                uoDropDownListCity.DataTextField = "colCityNameVarchar";
                uoDropDownListCity.DataValueField = "colCityIDInt";
                uoDropDownListCity.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CityDataTable != null)
                {
                    CityDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   12/10/2011
        /// Created By:     Charlene Remotigue
        /// Description     Get hotel list
        /// ---------------------------------------------
        /// Date Modified:   14/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------
        /// </summary>
        private void BindHotel()
        {

            List<HotelDTO> hotelList = HotelBLL.GetHotelBranchByCity("", uoDropDownListCity.SelectedValue);
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
                if (Session["Hotel"] != null)
                {
                    if (uoDropDownListHotel.Items.FindByValue(Session["Hotel"].ToString()) != null)
                    {
                        uoDropDownListHotel.SelectedValue = Session["Hotel"].ToString();
                    }
                }
            }            
        }

        /// <summary>
        /// Date Created: 12/10/2011
        /// Created By: Charlene Remotigue
        /// (description) Close this page and update parent page            
        /// </summary>
        private void OpenParentPage()
        {           

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupEvent\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Date Created:   21/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Send email
        /// </summary>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        //private void HotelSendEmail(string sSubject, string sMessage)
        //{
        //    string sBody;
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleAdministrator, "0", uoDropDownListCountry.SelectedValue);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
        //            sBody += "Dear " + TravelMartVariable.RoleAdministrator + ", <br/><br/> " + sMessage;
        //            sBody += "<br/><br/>Thank you.";
        //            sBody += "<br/><br/><i>Auto generated email.</i>";
        //            sBody += "</TD></TR></TABLE>";

        //            CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
        //        }
        //        //Email 24*7
        //        dt = UserAccountBLL.GetUserEmail(TravelMartVariable.Role24x7, "0", uoDropDownListCountry.SelectedValue);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
        //            sBody += "Dear " + "Admin" + ", <br/><br/> " + sMessage;
        //            sBody += "<br/><br/>Thank you.";
        //            sBody += "<br/><br/><i>Auto generated email.</i>";
        //            sBody += "</TD></TR></TABLE>";

        //            CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
        //        }
        //        //Email Hotel specialist of the country affected
        //        dt = new DataTable();
        //        dt = UserAccountBLL.GetUserEmail(TravelMartVariable.RoleHotelSpecialist, "0", uoDropDownListCountry.SelectedValue);
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
        //            sBody += "Dear " + TravelMartVariable.RoleHotelSpecialist + ", <br/><br/> " + sMessage;
        //            sBody += "<br/><br/>Thank you.";
        //            sBody += "<br/><br/><i>Auto generated email.</i>";
        //            sBody += "</TD></TR></TABLE>";

        //            CommonFunctions.SendEmail("", r["Email"].ToString(), sSubject, sBody);
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
        #endregion
    }
}

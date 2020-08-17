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
    public partial class HotelAirportAdd : System.Web.UI.Page
    {
        #region "Events"
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// Date Modified:  07/Feb/2013
        /// Modified By:    Josephine Gad
        /// (description)   Add roomID Query String
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HotelAirportLogAuditTrail();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRoomType.Value = GlobalCode.Field2TinyInt(Request.QueryString["roomID"]).ToString();

                vendorCountryLoad();
                vendorCityLoad(0);
                if (Request.QueryString["aID"] != null)
                {
                    uoHiddenFieldAirportID.Value = Request.QueryString["aID"].ToString();
                    //uoHiddenFieldRegionID.Value = Request.QueryString["rID"].ToString();
                    //uoTextBoxAirport.Text = Request.QueryString["aName"].ToString();

                    airportCodeNameLoad();
                }
                BindHotel();
            }
        }      
        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 0)
            {
                vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
            }
            //BindHotel();
        }

        protected void uoButtonFilterCity_Click(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 0)
            {
                vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));                
            }
            //BindHotel();
        }
        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindHotel();
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                if (uoHiddenFieldAirportID.Value != null || uoHiddenFieldAirportID.Value != "")
                {
                    
                    strLogDescription = "Airport hotel assignment added.";
                    strFunction = "uoButtonSave_Click";

                    Int32 AirportHotelID = AirBLL.InsertAirportHotel(Convert.ToInt32(uoHiddenFieldAirportID.Value), Convert.ToInt32(uoDropDownListHotel.SelectedValue),
                                              GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2TinyInt(uoHiddenFieldRoomType.Value),
                                              strLogDescription, strFunction, Path.GetFileName(Request.Path));
                    
                    //DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                    //BLL.AuditTrailBLL.InsertLogAuditTrail(AirportHotelID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                    //                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));

                    OpenParentPage();
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        #endregion
      

        #region "Functions"   
        /// <summary>
        /// Date Created: 27/01/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Loads airport code with airport name
        /// </summary>
        private void airportCodeNameLoad()
        {
            IDataReader dr = null;

            try
            {
                Int32 AirportID = Convert.ToInt32(uoHiddenFieldAirportID.Value);

                dr = AirBLL.GetAirportCodeName(AirportID);

                if (dr.Read())
                {
                    this.uoTextBoxAirport.Text = dr["Airport"].ToString();
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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist            
        /// </summary>
        private void vendorCountryLoad()
        {
            DataTable dt = new DataTable();
            dt = CountryBLL.CountryList();
            if (dt.Rows.Count > 0)
            {
                uoDropDownListCountry.DataSource = dt;
                uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountry.DataValueField = "colCountryIDInt";
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("--Select a Country--", "0"));
                //uoDropDownListCountry.SelectedValue = dt.Rows[0]["colCountryIDInt"].ToString();
            }
            else
            {                
                uoDropDownListCountry.DataBind();
                uoDropDownListCountry.Items.Insert(0, new ListItem("--Select a Country--", "0"));
            }
        }   
        /// <summary>
        /// Date Created:   29/07/2011
        /// Created By:     Gabriel Oquialda
        /// (description)   Loads vendor city to dropdownlist            
        /// ===============================================
        /// Date Modified:  24/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace CityListbyCountry with GetCityByCountry
        /// </summary>
        private void vendorCityLoad(int vendorCountryId)
        {
            DataTable dt = null;
            try
            {
                //dt = CityBLL.CityListbyCountry(vendorCountryId);
                dt = CityBLL.GetCityByCountry(vendorCountryId.ToString(), uoTextBoxCityName.Text.Trim(), "0");
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataSource = dt;
                    uoDropDownListCity.DataTextField = "colCityNameVarchar";
                    uoDropDownListCity.DataValueField = "colCityIDInt";
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select City--", "0"));
                }
                else
                {
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select City--", "0"));
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
        /// <summary>
        /// Date Created:   24/01/2012
        /// Created By:     Josephine Gad
        /// (description)   Loads Hotel Branch by city
        /// ===============================================
        /// Date Modified:  06/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   change DataTable to List
        /// ===============================================
        /// </summary>
        private void BindHotel()
        {
            //DataTable dt = null;
            try
            {
                string sCity = uoDropDownListCity.SelectedValue;
                sCity = (sCity.Trim() == "" ? "0" : sCity);

                List<HotelAirportDTO> list = HotelBLL.GetHotelBranchAll(sCity, GlobalCode.Field2Int(uoHiddenFieldAirportID.Value), 
                    false, GlobalCode.Field2TinyInt(uoHiddenFieldRoomType.Value));
                if (list.Count > 0)
                {
                    uoDropDownListHotel.DataSource = list;
                    uoDropDownListHotel.DataTextField = "BranchName";
                    uoDropDownListHotel.DataValueField = "BranchID";
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel Branch--", "0"));
                }
                else
                {
                    uoDropDownListHotel.DataBind();
                    uoDropDownListHotel.Items.Insert(0, new ListItem("--Select Hotel Branch--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='Javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupAdd\").val(\"1\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldRegion\").val(\"" + Request.QueryString["rID"] + "\"); ";
            //sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCountry\").val(\"" + Request.QueryString["cID"] + "\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldAirport\").val(\"" + Request.QueryString["aID"] + "\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelAirportLogAuditTrail()
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Add button for hotel airport editor clicked.";
            strFunction = "HotelAirportLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion
    }
}

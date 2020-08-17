using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.Hotel
{
    public partial class HotelRoomOverride : System.Web.UI.Page
    {
        #region DECLARATION
        public string PageTitle = string.Empty;
        public string Hotel = string.Empty;
        public string cBlocks = string.Empty;
        public string RoomType = string.Empty;
        public string EffectiveDate = string.Empty;
        DashboardBLL dbBLL = new DashboardBLL();
        #endregion

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
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                OpenParentPage();
            }

            if (Request.QueryString["hrId"] == "0")
            {
                PageTitle = "Add Hotel Room Blocks";
                //uoTextBoxEffectiveDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                //uoTextBoxEffectiveDate.Text = uoHiddenFieldEffectiveDate.Value;
            }
            else
            {
                PageTitle = "Edit Hotel Room Blocks";
            }

            if (!IsPostBack)
            {
                SetDefaults();
            }
        }

        protected void uoDropDownListCountryPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldCountryId.Value = uoDropDownListCountryPerRegion.SelectedValue;
            BindLetters();
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHotel();
        }

        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            string HotelBranch = string.Empty;
            int addedBlocks = 0;
            if(Request.QueryString["hrId"] != "0")
            {
                if(Int32.Parse(uoHiddenFieldLastRoomBlocks.Value) > Int32.Parse(uoTextBoxNumberOfRooms.Text))
                {
                    addedBlocks = Int32.Parse(uoHiddenFieldLastRoomBlocks.Value) - Int32.Parse(uoTextBoxNumberOfRooms.Text);
                }
                else
                {
                    addedBlocks = Int32.Parse(uoTextBoxNumberOfRooms.Text) - Int32.Parse(uoHiddenFieldLastRoomBlocks.Value);
                }
            }
            if (TravelMartVariable.RoleHotelSpecialist == GlobalCode.Field2String(Session["UserName"]))
            {
                HotelBranch = uoHiddenFieldBranchId.Value;
            }
            else
            {
                HotelBranch = GlobalCode.Field2String(Session["UserBranchID"]);
            }

            int withErrors = dbBLL.SaveHotelOverride(Request.QueryString["hrId"], HotelBranch, uoTextBoxEffectiveDate.Text, uoTextBoxRate.Text,
                uoHiddenFieldCurrencyId.Value, uoTextBoxRateTax.Text, uoCheckBoxTaxBit.Checked, uoDropdownListRoom.SelectedValue,
                uoTextBoxNumberOfRooms.Text, addedBlocks, GlobalCode.Field2String(Session["UserName"]));

            if (withErrors > 0)
            {
                AlertMessage("Record already exists.");
            }
            else
            {
                AlertMessage("Successfully saved.");
                OpenParentPage();
            }
            
        }

        protected void uoDropDownListLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCity();
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            LoadCity();
        }

        protected void uoDropDownListHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            uoHiddenFieldBranchId.Value = uoDropDownListHotel.SelectedValue;
//            LoadRooms();
            LoadCurrency();
        }

        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCountry();
        }
        #endregion

        #region METHODS
        private void OpenParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\").val(\"1\"); ";
            sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        protected void LoadRegion()
        {
            DataTable RegionDataTable = null;
            try
            {
                RegionDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListRegion.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListRegion.Items.Add(item);
                uoDropDownListRegion.DataSource = RegionDataTable;
                uoDropDownListRegion.DataTextField = "colMapNameVarchar";
                uoDropDownListRegion.DataValueField = "colMapIDInt";
                uoDropDownListRegion.DataBind();

                if (GlobalCode.Field2String(Session["Region"]) != "")
                {
                    if (uoDropDownListRegion.Items.FindByValue(GlobalCode.Field2String(Session["Region"])) != null)
                    {
                        uoDropDownListRegion.SelectedValue = GlobalCode.Field2String(Session["Region"]);
                    }
                }
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

        protected void LoadCountry()
        {
            DataTable CountryDataTable = null;
            try
            {
                CountryDataTable = CountryBLL.CountryListByRegion(uoDropDownListRegion.SelectedValue, "");
                //                CountryDataTable = CountryBLL.CountryListByRegion(GlobalCode.Field2String(Session["Region"]), uoHiddenFieldCountryName.Value);
                uoDropDownListCountryPerRegion.Items.Clear();
                ListItem item = new ListItem("--Select Country--", "0");
                uoDropDownListCountryPerRegion.Items.Add(item);
                uoDropDownListCountryPerRegion.DataSource = CountryDataTable;
                uoDropDownListCountryPerRegion.DataTextField = "colCountryNameVarchar";
                uoDropDownListCountryPerRegion.DataValueField = "colCountryIDInt";
                uoDropDownListCountryPerRegion.DataBind();

                if (GlobalCode.Field2String(Session["Country"]) != "")
                {
                    if (uoDropDownListCountryPerRegion.Items.FindByValue(GlobalCode.Field2String(Session["Country"])) != null)
                    {
                        uoDropDownListCountryPerRegion.SelectedValue = GlobalCode.Field2String(Session["Country"]);
                    }
                }
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

        protected void LoadCity()
        {
            DataTable CityDataTable = null;

            try
            {

                CityDataTable = CityBLL.GetCityByCountry(uoDropDownListCountryPerRegion.SelectedValue, uoTextBoxCity.Text, 
                    uoDropDownListLetters.SelectedValue);
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
        /// Date Modified:   14/02/2011
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        protected void LoadHotel()
        {
            try
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
                else if(hotelList.Count > 0)
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
            //DataTable HotelDataTable = null;
            //try
            //{
            //    HotelDataTable = HotelBLL.GetHotelBranchByCity(GlobalCode.Field2String(Session["UserName"]), uoDropDownListCity.SelectedValue);
            //    uoDropDownListHotel.Items.Clear();
            //    ListItem item = new ListItem("--Select Hotel--", "0");
            //    uoDropDownListHotel.Items.Add(item);
            //    uoDropDownListHotel.DataSource = HotelDataTable;
            //    uoDropDownListHotel.DataTextField = "BranchName";
            //    uoDropDownListHotel.DataValueField = "BranchID";
            //    uoDropDownListHotel.DataBind();

            //    if (GlobalCode.Field2String(Session["Hotel"]) != "")
            //    {
            //        if (uoDropDownListHotel.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
            //        {
            //            uoDropDownListHotel.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (HotelDataTable != null)
            //    {
            //        HotelDataTable.Dispose();
            //    }
            //}
        }        
        //protected void LoadRooms()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = HotelBLL.HotelRoomTypeGetDetailsByBranch(uoHiddenFieldBranchId.Value);

        //        uoDropdownListRoom.Items.Clear();
        //        if (dt.Rows.Count > 0)
        //        {
        //            uoDropdownListRoom.DataSource = dt;
        //            uoDropdownListRoom.DataTextField = "colRoomNameVarchar";
        //            uoDropdownListRoom.DataValueField = "colRoomTypeID";

        //        }
        //        uoDropdownListRoom.Items.Insert(0, new ListItem("--Select Room Type--", "0"));
        //        uoDropdownListRoom.DataBind();

        //        if (dt.Rows.Count == 1)
        //        {
        //            uoDropdownListRoom.SelectedIndex = 1;
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

        protected void LoadCurrency()
        {
            
            IDataReader dr = null;
            try
            {
                if (uoHiddenFieldCountryId.Value == "")
                {
                    uoHiddenFieldCountryId.Value = "0";
                }

                dr = ContractBLL.GetCurrencyByCountry(uoHiddenFieldCountryId.Value);
                if (dr.Read())
                {
                    uoTextBoxCurrency.Text = dr["colCurrencyNameVarchar"].ToString();
                    uoHiddenFieldCurrencyId.Value = dr["colCurrencyIDInt"].ToString();
                }
                else
                {
                    uoTextBoxCurrency.Text = "No available currency.";
                }
                uoTextBoxCurrency.ReadOnly = true;
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

        protected void LoadDetails()
        {
            IDataReader dr = null;
            try
            {
                dr = dbBLL.getOverrideDetails(uoHiddenFieldBranchId.Value, GlobalCode.Field2String(Request.QueryString["hrId"]), uoDropdownListRoom.SelectedValue);
                if (dr.Read())
                {
                    Hotel = dr["colVendorBranchNameVarchar"].ToString();
                    uoHiddenFieldCountryId.Value = dr["colCountryIDInt"].ToString();
                    if (Request.QueryString["hrId"] != "0")
                    {
                        DateTime dt = GlobalCode.Field2DateTime(dr["colEffectiveDate"]);
                        EffectiveDate = String.Format("{0:mm/dd/yyyy}", dr["colEffectiveDate"]) + " (" +
                            dt.Day + ")";
                        uoHiddenFieldEffectiveDate.Value = String.Format("{0:mm/dd/yyyy}", dr["colEffectiveDate"]);
                        RoomType = dr["colRoomNameVarchar"].ToString();
                        uoTextBoxRate.Text = dr["colRatePerDayMoney"].ToString().Remove(dr["colRatePerDayMoney"].ToString().Length - 2);
                        uoTextBoxRateTax.Text = (GlobalCode.Field2Decimal(dr["colRoomRateTaxPercentage"].ToString()) * 100).ToString();
                        uoCheckBoxTaxBit.Checked = GlobalCode.Field2Bool(dr["colRoomRateTaxInclusive"].ToString());
                        uoTextBoxNumberOfRooms.Text = dr["colRoomBlocksPerDayInt"].ToString();
                    }
                    else
                    {
                       
                        //TREditrType.Visible = false;
                    }
                    
                    
                    
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

        protected void SetDefaults()
        {
            if (TravelMartVariable.RoleHotelSpecialist == GlobalCode.Field2String(Session["UserName"]))
            {
                if (Request.QueryString["brId"] != "" && Request.QueryString["brId"] != null)
                {
                    DateTime dt = GlobalCode.Field2DateTime(Request.QueryString["date"]);
                    //set visibility
                    uoPanelHotelBranch.Visible = false;
                    
                    //set enabled
                    uoTextBoxEffectiveDate.Enabled = false;
                    ImageButton1.Enabled = false;
                    uoDropdownListRoom.Enabled = false;
                    //set default values
                    uoHiddenFieldBranchId.Value = Request.QueryString["brId"];
                    uoTextBoxEffectiveDate.Text = dt.ToShortDateString();
//                    LoadRooms();
                    uoDropdownListRoom.SelectedValue = String.Format("{0:MM/dd/yyyy}",Request.QueryString["rType"]);
                    LoadDetails();                   
                    LoadCurrency();
                    
                    cBlocks = (string)(GlobalCode.Field2Int(Request.QueryString["tBlocks"].ToString()) -
                                                GlobalCode.Field2Int(Request.QueryString["oBlocks"].ToString())).ToString();
                    
                }
                else
                {
                    uoPanelHotelBranch.Visible = true;
                    uoEditPanel.Visible = false;
                }
               
            }
            else
            {
                uoPanelHotelBranch.Visible = false;
                uoEditPanel.Visible = false;
//                LoadRooms();
                uoDropdownListRoom.SelectedValue = Request.QueryString["rType"];
                LoadDetails();
                LoadCurrency();
            }
        }
        #endregion

       

    }
}

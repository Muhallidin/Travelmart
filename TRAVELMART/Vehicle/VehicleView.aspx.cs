using System;
using System.Data;
using System.Collections;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;

namespace TRAVELMART
{
    public partial class VehicleView : System.Web.UI.Page
    {

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["p"] == null || Request.QueryString["st"] == null || Request.QueryString["dt"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["p"] == "" || Request.QueryString["st"] == "" || Request.QueryString["dt"] == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Request.QueryString["from"] != null)
            {
                uoHiddenFieldDateFrom.Value = Request.QueryString["from"];
            }
            if (Request.QueryString["to"] != null)
            {
                uoHiddenFieldDateTo.Value = Request.QueryString["to"];
            }
            if (Request.QueryString["fBy"] != null)
            {
                uoHiddenFieldFilterBy.Value = Request.QueryString["fBy"];
            }  
            if (!IsPostBack)
            {
                BindMapRef();
                Session["strSFStatus"] = Request.QueryString["st"];
                Session["strSFFlightDateRange"] = Request.QueryString["dt"];
                GetSFVehicleTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));

                uoHyperLinkSeafarer.HRef = "~/Vehicle/AddSeafarer.aspx";
            }

            if (uoHiddenFieldSeafarer.Value == "1")
            {
                Session["strSFStatus"] = Request.QueryString["st"];
                Session["strSFFlightDateRange"] = Request.QueryString["dt"];
                GetSFVehicleTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
            }           
            uoHiddenFieldSeafarer.Value = "0";
        }
        protected void uoButtonSeafarerAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Vehicle/AddSeafarer.aspx");
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetSFVehicleTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
        }
        protected void uoDropDownListMapRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSFVehicleTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
        }
        protected void uolistviewVehicleTravelInfoPager_PreRender(object sender, EventArgs e)
        {

        }  
        #endregion

        #region Functions        
        private void GetSFVehicleTravelDetails(string strSFStatus, string strFlightDateRange)
        {
            /// <summary>        
            /// Date Created: 14/07/2011
            /// Created By: Marco Abejar
            /// (description) Selecting list of vehicle travel details
            /// ------------------------------------------------------
            /// Date Modified: 27/07/2011
            /// Modified By: Josephine Gad
            /// (description) Make header ONSIGNING/OFFSIGNING dynamic
            /// ------------------------------------------------------
            /// Date Modified: 08/05/2011
            /// Modifed By: Josephine Gad
            /// (description) Add Date Range parameter, Close DataTable 
            /// </summary>
            /// 
            DataTable VehicleDataTable = null;
            try
            {
                string DateFromString = (uoHiddenFieldDateFrom.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateFrom.Value);
                string DateToString = (uoHiddenFieldDateTo.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateTo.Value);

                VehicleDataTable = SeafarerTravelBLL.GetSFVehicleTravelDetails(strSFStatus, strFlightDateRange, uoTextBoxName.Text,
                    DateFromString, DateToString, GlobalCode.Field2String(Session["UserName"]), uoDropDownListMapRef.SelectedValue, uoHiddenFieldFilterBy.Value);
                uolistviewVehicleTravelInfo.DataSource = VehicleDataTable;                          
                uolistviewVehicleTravelInfo.DataBind();

                Label ucLabelONOFF = (Label)uolistviewVehicleTravelInfo.FindControl("ucLabelONOFF");
                if (ucLabelONOFF != null)
                {
                    if (Session["strSFStatus"] == "ON")
                    {
                        ucLabelONOFF.Text = "Onsigning";
                    }
                    else
                    {
                        ucLabelONOFF.Text = "Offsigning";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (VehicleDataTable != null)
                {
                    VehicleDataTable.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Date and time format        
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
        private void BindMapRef()
        {
            DataTable MapRefDataTable = null;
            try
            {
                MapRefDataTable = CountryBLL.MapListByUser(GlobalCode.Field2String(Session["UserName"]));
                uoDropDownListMapRef.Items.Clear();
                ListItem item = new ListItem("--Select Region--", "0");
                uoDropDownListMapRef.Items.Add(item);
                uoDropDownListMapRef.DataSource = MapRefDataTable;
                uoDropDownListMapRef.DataTextField = "colMapNameVarchar";
                uoDropDownListMapRef.DataValueField = "colMapIDInt";
                uoDropDownListMapRef.DataBind();

                //CommonFunctions.ChangeToUpperCase(uoDropDownListMapRef);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (MapRefDataTable != null)
                {
                    MapRefDataTable.Dispose();
                }
            }
        }
        #endregion

        
    }
}

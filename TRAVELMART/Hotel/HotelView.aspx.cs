using System;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.WebControls;
using System.Linq;

namespace TRAVELMART
{
    public partial class HotelView : System.Web.UI.Page
    {
        #region Event 
        /// <summary>       
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Load all basic hotel details
        /// --------------------------------------------------------
        /// Date Modified: 07/27/2011
        /// Modified By: Gabriel Oquialda
        /// (description) Added code on Page_Load for re-loading of 
        ///               hotel view details after adding a seafarer  
        /// --------------------------------------------------------
        ///  Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
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
                GetSFHotelTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));

                uoHyperLinkSeafarer.HRef = "~/Hotel/AddSeafarer.aspx";
            }

            /// Added code 07/27/2011.
            if (uoHiddenFieldSeafarer.Value == "1")
            {
                Session["strSFStatus"] = Request.QueryString["st"];
                Session["strSFFlightDateRange"] = Request.QueryString["dt"];
                GetSFHotelTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
            }
            uoHiddenFieldSeafarer.Value = "0";
            
        }

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Add new seafarer        
        /// </summary>
        protected void uoButtonSeafarerAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Hotel/AddSeafarer.aspx");
        }

        /// <summary>
        /// Date Created:   25/07/2011
        /// Created By:     Ryan Bautista
        /// Description:    Search Seafarer        
        /// -------------------------------------
        /// Date Modified:   08/08/2011
        /// Modified By:     Josephine Gad
        /// Description:    Change GetSFHotelTravelListSearch to GetSFHotelTravelDetails
        /// </summary>
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            Session["strSFStatus"] = Request.QueryString["st"];
            Session["strSFFlightDateRange"] = Request.QueryString["dt"];
            //GetSFHotelTravelListSearch(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]), uoTextBoxName.Text);            
            GetSFHotelTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
        }
        protected void uoDropDownListMapRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSFHotelTravelDetails(GlobalCode.Field2String(Session["strSFStatus"]), GlobalCode.Field2String(Session["strSFFlightDateRange"]));
        }

        protected void uolistviewHotelInfoPager_PreRender(object sender, EventArgs e)
        {

        }   
        #endregion

        #region Function            
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
        /// <summary>        
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Load all hotel travel details.
        /// ------------------------------------------------------
        /// Date Modified: 27/07/2011
        /// Modified By: Josephine Gad
        /// (description) Make header ONSIGNING/OFFSIGNING dynamic        
        /// ------------------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// </summary>
        private void GetSFHotelTravelDetails(string strSFStatus, string strFlightDateRange)
        {
            DataTable HotelDataTable = null;
            try
            {
                string DateFromString = (uoHiddenFieldDateFrom.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateFrom.Value);
                string DateToString = (uoHiddenFieldDateTo.Value == "" ? DateTime.Now.ToShortDateString() : uoHiddenFieldDateTo.Value);

                HotelDataTable = SeafarerTravelBLL.GetSFHotelTravelList(strSFStatus, strFlightDateRange,
                     DateFromString, DateToString, uoTextBoxName.Text, GlobalCode.Field2String(Session["UserName"]), uoDropDownListMapRef.SelectedValue,
                     uoHiddenFieldFilterBy.Value);
                uolistviewHotelInfo.DataSource = HotelDataTable;
                uolistviewHotelInfo.DataBind();

                Label ucLabelONOFF = (Label)uolistviewHotelInfo.FindControl("ucLabelONOFF");
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
                if (HotelDataTable != null)
                {
                    HotelDataTable.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   25/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Search hotel travel details      
        /// ------------------------------------------------------
        /// Date Modified:  09/08/2011
        /// Modified By:    Josephine Gad
        /// (description)   Close DataTable
        /// </summary>
        private void GetSFHotelTravelListSearch(string strSFStatus, string strFlightDateRange, string SFname)
        {
            DataTable dt = null;
            try
            {
                dt = SeafarerTravelBLL.GetSFHotelTravelListSearch(strSFStatus, strFlightDateRange, SFname);
                uolistviewHotelInfo.DataSource = dt;
                uolistviewHotelInfo.DataBind();
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Format date        
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
        #endregion       
    }
}

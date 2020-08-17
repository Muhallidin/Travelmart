using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentContractHotelView : System.Web.UI.Page
    {
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
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadValues();
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 03/11/2011
        /// Description: loads values on edit
        /// ---------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        protected void LoadValues()
        {
            IDataReader dr = null;
            try
            {
                dr = null;//ContractBLL.LoadPortContractHotelDetails(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        uoLabelBrand.Text = dr["colVendorNameVarchar"].ToString();
                        uoLabelBranch.Text = dr["colVendorBranchNameVarchar"].ToString();
                        uoLabelMealRate.Text = dr["colServiceRateMoney"].ToString().Remove(dr["colServiceRateMoney"].ToString().Length - 2);
                        uoLabelMealTax.Text = (Int32.Parse(dr["colMealRateTaxDecimal"].ToString()) * 100).ToString();
                        uoCheckBoxTaxInclusive.Checked = bool.Parse(dr["colMealTaxInclusiveBit"].ToString());
                        uoCheckBoxBreakFast.Checked = bool.Parse(dr["colBreakfastBit"].ToString());
                        uoCheckBoxLunch.Checked = bool.Parse(dr["colLunchBit"].ToString());
                        uoCHeckBoxDinner.Checked = bool.Parse(dr["colDinnerBit"].ToString());
                        uoCheckBoxLunchOrDinner.Checked = bool.Parse(dr["colLunchOrDinnerBit"].ToString());
                        uoCheckBoxWithShuttle.Checked = bool.Parse(dr["colWithShuttleBit"].ToString());
                    }
                }
                LoadRooms();
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
        /// Author: Charlene Remotigue
        /// Date Created: 09/11/2011
        /// Description: load added rooms
        /// </summary>
        protected void LoadRooms()
        {
            DataTable roomsDataTable = null;//ContractBLL.LoadPortContractHotelRooms(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
            uoRoomsList.Items.Clear();
            uoRoomsList.DataSource = roomsDataTable;
            uoRoomsList.DataBind();

            if (roomsDataTable.Rows.Count == 0)
            {
                uoTRRooms.Visible = false;
            }
            else
            {
                uoTRRooms.Visible = true;
            }
        }
        #endregion
    }
}

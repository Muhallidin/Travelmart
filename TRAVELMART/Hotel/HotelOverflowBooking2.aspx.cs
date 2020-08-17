using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace TRAVELMART.Hotel
{
    public partial class HotelOverflowBooking2 : System.Web.UI.Page
    {
        #region DECLARATIONS
        OverFlowBookingBLL OverflowBLL = new OverFlowBookingBLL();
        
        #endregion

        #region EVENTS
        /// <summary>
        /// Author:             Charlene Remotigue
        /// Date Created:       09/02/2012
        /// ===================================
        /// Date Modified:     14/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Hide "Date To" in master page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;
                SetDefaults();

                HtmlControl uoRowDateTo = (HtmlControl)Master.FindControl("uoRowDateTo");
                uoRowDateTo.Visible = false;
            }

            if (this.IsPostBack && uoHiddenFieldPopEditor.Value=="1")
            {
                uoHiddenFieldPopEditor.Value = "0";
                SetDefaults(); 
            }
        }

        protected void uoButtonApprove_Click(object sender, EventArgs e)
        {
            string sfName = "";
            string sfId = "";
            string sfStripe = "";
            string SFStatus = "";
            string recLoc = "";
            string trId = "";
            string mReqId = "";
            foreach (ListViewItem item in uoOverflowList.Items)
            {
                CheckBox CheckSelect = (CheckBox)item.FindControl("uoSelectCheckBox");
                bool IsApprovedBool = CheckSelect.Checked;

                if (IsApprovedBool)
                {
                    Label SeafarerName = (Label)item.FindControl("uoLblSeafarerName");
                    Label SeafarerId = (Label)item.FindControl("uoLblSeafarerID");
                    Label Stripes = (Label)item.FindControl("uoLblStripe");
                    Label Status = (Label)item.FindControl("uoLblStatus");
                    Label RLoc = (Label)item.FindControl("uoLblRecLoc");
                    HiddenField tReq = (HiddenField)item.FindControl("hfReqId");

                    sfName = sfName + SeafarerName.Text + "|";
                    sfId = sfId + SeafarerId.Text + "|";
                    sfStripe = sfStripe + Stripes.Text + "|";
                    SFStatus += Status.Text + '|';
                    recLoc += RLoc.Text + "|";
                    trId += tReq.Value + "|";
                    mReqId += "0" + "|";
                }
            }

            sfName = sfName.TrimEnd('|');
            sfId = sfId.TrimEnd('|');
            sfStripe = sfStripe.TrimEnd('|');
            SFStatus = SFStatus.TrimEnd('|');
            recLoc = recLoc.TrimEnd('|');
            trId = trId.TrimEnd('|');
            mReqId = mReqId.TrimEnd('|');

            string sscript = "OpenRequestEditor('" + sfId + "','" + sfName + "','" 
                + sfStripe + "','" + SFStatus + "','" + recLoc + "','" + trId + "','" + mReqId + "')";
            uoHiddenFieldSFId.Value = sfId;

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenRequestEditor", sscript, true);
        }
        #endregion

        #region METHODS
        protected void SetDefaults()
        {
            try
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldStartDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                uoHiddenFieldFilter.Value = GlobalCode.Field2String(Session["strPendingFilter"]);

                OverflowBLL.LoadAllOverflowBooking(DateTime.Parse(uoHiddenFieldStartDate.Value), uoHiddenFieldUser.Value, 0,
                    Int32.Parse(uoHiddenFieldFilter.Value), 0, 20);

                ObjectDataSource1.TypeName = "TRAVELMART.Common.HotelOverflowBooking";
                ObjectDataSource1.SelectCountMethod = "GetOverflowBookingCount";
                ObjectDataSource1.SelectMethod = "GetOverflowBooking";

                uoOverflowList.DataSourceID = ObjectDataSource1.UniqueID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ValidateData()
        {
            Boolean Invalid = Boolean.Parse(Eval("Invalid").ToString());

            if (Invalid)
            {
                return string.Format("background-color: #FFCC66; border: thin solid #000000; font-weight: bold");
            }
            else
            {
                return string.Format("");
            }
        }

        #endregion

        
    }
}

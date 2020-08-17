using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART.Finance
{
    public partial class ReimbursementView : System.Web.UI.Page
    {
        #region DECLARATION
        FinanceBLL fBLL = new FinanceBLL();
        #endregion

        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 28/10/2011
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDetails();
            }
        }
        #endregion

        #region METHODS
        private void LoadDetails()
        {
            uoHiddenFieldCurrencyId.Value = Request.QueryString["cId"];
            uoLabelCurrency.Text = Request.QueryString["cName"];
            using (IDataReader dr = fBLL.LoadSeafarerReimbursementDetails(Request.QueryString["sfId"], Request.QueryString["rId"]))
            {
                if (dr.Read())
                {
                    uoLabelAmount.Text = dr["colAmountMoney"].ToString().Substring(0, (dr["colAmountMoney"].ToString().Length - 2));
                    uoLabelReimbursementName.Text = dr["colReimbursementNameVarchar"].ToString();
                    uoTextBoxRemarks.Text = dr["colRemarksVarchar"].ToString();
                }

            }
        }
        #endregion
    }
}

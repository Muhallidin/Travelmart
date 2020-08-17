using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;

namespace TRAVELMART.ContractManagement
{
    public partial class PortAgentContractOthersView : System.Web.UI.Page
    {
        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDetails();
            }
        }
        #endregion

        #region METHODS
        protected void LoadDetails()
        {
            IDataReader dr = null;
            try
            {
                dr = ContractBLL.LoadPortAgentContractOthers(Request.QueryString["ServiceId"]);
                if (dr.Read())
                {
                    uoTextBoxRemarks.Text = dr["colRemarksVarchar"].ToString();
                    uoLabelRate.Text = dr["colServiceRateMoney"].ToString().Remove(dr["colServiceRateMoney"].ToString().Length - 2);
                    uoLabelCurrency.Text = dr["colCurrencyNameVarchar"].ToString();
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
        #endregion
    }
}

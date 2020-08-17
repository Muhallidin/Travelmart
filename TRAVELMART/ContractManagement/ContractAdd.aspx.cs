using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TRAVELMART.ContractManagement
{
    public partial class ContractAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uobuttonSave_Click(object sender, EventArgs e)
        {
            string strTitle = uotextboxContractTitle.Text.Trim();
            string strCategory = uodropdownCategory.SelectedValue;
            string strVendor = uodropdownVendor.SelectedValue;
            string strCost = uotextboxCost.Text.Trim();
            string strStartDate = uotextboxStartDate.Text.Trim();
            string strEndDate = uotextboxEndDate.Text.Trim();
        }
    }
}

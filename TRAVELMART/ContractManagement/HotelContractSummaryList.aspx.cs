using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART.ContractManagement
{
    public partial class HotelContractSummaryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            SFStatus.Visible = false;
            if (!IsPostBack)
            {

            }
        }
    }
}

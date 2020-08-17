using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class PortContractListView : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            GetContractList();
            uoHyperLinkPortAdd.HRef = "~/ContractManagement/PortContractAdd.aspx";
        }

        protected void uoPortContractList_PreRender(object sender, EventArgs e)
        {        
        }

        #endregion

        #region Function

        private void GetContractList()
        {
            //DataTable dt = BLL.ContractBLL.GetPortContractList(uoTextBoxPortCompany.Text, GlobalCode.Field2String(Session["UserName"]));

            //if (dt.Rows.Count > 0)
            //    uoPortContractList.DataSource = dt;
            //uoPortContractList.DataBind();
        }

        #endregion

    }
}

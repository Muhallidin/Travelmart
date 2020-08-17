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
    public partial class PortAgentContractMedicalView : System.Web.UI.Page
    {
        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMedicalAccomodations();
                //LoadMedicalTransfers();
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created:10/11/2011
        /// Description: Load Medical Accomodation details
        /// ------------------------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        protected void LoadMedicalAccomodations()
        {
            IDataReader dr = null;
            try
            {
                //dr = ContractBLL.LoadPortAgentMedicalServices(Request.QueryString["ServiceId"]);
               if(dr != null)
               {
                   if (dr.Read())
                   {
                       uoLabelDays.Text = dr["colAccomodationDays"].ToString();
                       uoLabelRate.Text = dr["colServiceRateMoney"].ToString().Remove(dr["colServiceRateMoney"].ToString().Length - 2);
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

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 10/11/2011
        /// Description: load Service Provider medical specifications
        /// </summary>
        //protected void LoadMedicalTransfers()
        //{
        //    DataTable transferDataTable = ContractBLL.LoadPortAgentMedicalSpecifications(Request.QueryString["ServiceId"], GlobalCode.Field2String(Session["UserName"]));
        //    uoTransferList.Items.Clear();
        //    uoTransferList.DataSource = transferDataTable;
        //    uoTransferList.DataBind();
        //}
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;

namespace TRAVELMART
{
    public partial class Seafarer : System.Web.UI.Page
    {
        #region DECLARATIONS
        SeafarerBLL SFBll = new SeafarerBLL();
        #endregion

        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SeafarerViewDetails();
            }

        }
        #endregion

        #region FUNCTIONS
        private void SeafarerViewDetails()
        {
            DataTable SFDataTable = null;
            try
            {
               // SFDataTable = SFBll.SeafarerGetDetails();
                //uoListViewTravel.DataSource = SFDataTable;
                //uoListViewTravel.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SFDataTable != null)
                {
                    SFDataTable.Dispose();
                }
            }
        }        
        #endregion    
    }
}

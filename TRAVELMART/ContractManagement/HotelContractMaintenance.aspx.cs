using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;

namespace TRAVELMART.ContractManagement
{

    public partial class HotelContractMaintenance : System.Web.UI.Page
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["vmId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
                if (!IsPostBack)
                {
                    uohiddenvID.Value = Request.QueryString["vmId"].ToString().Trim();
                    GetVendorHotelContract(Request.QueryString["vmId"]);
                }

        }

        protected void uobtnSave_Click(object sender, EventArgs e)
        {
            //AddSaveHotelContract(uohiddenvID.Value,
            //    uoTextBoxVendorName.Text,
            //    uotextboxContractTitle.Text,
            //    uotextboxRemarks.Text,
            //    uotextboxStartDate.Text,
            //    uotextboxEndDate.Text,
            //    uotextboxRCCLRep.Text,
            //    uotextboxVendorRep.Text);
        }

        protected void uobtnAddRoom_Click(object sender, EventArgs e)
        {

        }
        protected void uobtnAddMeal_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region Functions
        /// <summary>
        /// Date Created: 19/08/2011
        /// Created By: Marco Abejar
        /// (description) Get Vendor Contract       
        /// -----------------------------------
        /// Date Modified: 28/11/2011
        /// Modified By:   Charlene Remotigue
        /// (description)  optimization (use datareader instead of datatable
        /// </summary>
        /// 
        private void GetVendorHotelContract(string vmId)
        {
           

            IDataReader dr = null;

            try
            {
                dr = BLL.ContractBLL.GetVendorHotelContract(vmId);
                if (dr.Read())
                {
                    uoTextBoxVendorCode.Text = dr["colVendorCodeVarchar"].ToString();
                    uoTextBoxVendorName.Text = dr["colVendorNameVarchar"].ToString();
                    uotextboxContractTitle.Text = dr["colContractNameVarchar"].ToString();
                    uotextboxRemarks.Text = dr["colRemarksVarchar"].ToString();
                    uotextboxRCCLRep.Text = dr["colRCCLPersonnel"].ToString();
                    uotextboxVendorRep.Text = dr["colVendorPersonnel"].ToString();

                    string dtStart = (dr["colContractDateStartedDate"].ToString().Length > 0)
                        ? String.Format("{0:MM/dd/yyyy HH:mm}", Convert.ToDateTime(dr["colContractDateStartedDate"].ToString()))
                        : "";
                    string dtEnd = (dr["colContractDateEndDate"].ToString().Length > 0)
                       ? String.Format("{0:MM/dd/yyyy HH:mm}", Convert.ToDateTime(dr["colContractDateEndDate"].ToString()))
                       : "";
                    uotextboxStartDate.Text = dtStart;
                    uotextboxEndDate.Text = dtEnd;

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

        private void AddSaveHotelContract(string vmID, Int32 VendorID, string vContract, string Remarks, string dtStart, string dtEnd, string RCCLRep, string vRep)
        {
            /// <summary>
            /// Date Created: 19/08/2011
            /// Created By: Marco Abejar
            /// (description) Add / save hotel contract          
            /// </summary>
            /// 

            //BLL.ContractBLL.AddSaveHotelContract(vmID, VendorID, vContract, Remarks, dtStart, dtEnd, RCCLRep, vRep);
            ClosePage();
        }


        private void ClosePage()
        {
            /// <summary>
            /// Date Created: 19/08/2011
            /// Created By: Marco Abejar
            /// (description) Close page       
            /// </summary>

            string sScript = "<script language='javascript'>";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uobtnSave, this.GetType(), "scr", sScript, false);
        
        }
        private void AlertMessage(string s)
        {
            /// <summary>
            /// Date Created: 08/07/2011
            /// Created By: Marco Abejar
            /// (description) Show pop up message            
            /// </summary>

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(uobtnSave, this.GetType(), "scr", sScript, false);
        }

        #endregion

    }


}

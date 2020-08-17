using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class Remarks : System.Web.UI.Page
    {
        TravelRequestBLL BLL = new TravelRequestBLL();

        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRemarksList();
            }
        }
        protected void uoListViewRemarks_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField hdfRemarksID = (HiddenField)uoListViewRemarks.Items[e.ItemIndex].FindControl("uoHiddenFieldRemarksID");
            DeleteRemarks(GlobalCode.Field2Int(hdfRemarksID.Value));            
        }
        protected void uoListViewRemarks_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            HiddenField hdfRemarksID = (HiddenField)uoListViewRemarks.Items[e.NewSelectedIndex].FindControl("uoHiddenFieldRemarksID");

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupRemarks\").val(\"1\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldLatestRemarksID\").val(\"" + hdfRemarksID.Value + "\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            //ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
            ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        #endregion
        #region "Functios"
        /// <summary>
        // Author:          Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Delete Travel Request Remarks
        /// -------------------------------------------------------
        /// </summary>
        /// <param name="RemarksID"></param>
        private void DeleteRemarks(int RemarksID)
        {
            try
            { 
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                DateTime GMTDate = CommonFunctions.GetDateTimeGMT(currentDate);
                string sPageName = Path.GetFileName(Request.Path);

                BLL.DeleteTravelRequestRemarks(0, RemarksID, GlobalCode.Field2String(Request.QueryString["trID"]),
                    GlobalCode.Field2String(Session["UserName"]), "DeleteRemarks", sPageName, GMTDate, currentDate);
                BindRemarksList();
                AlertMessage("Record successfully deleted.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Get Travel Request Remarks
        /// -------------------------------------------------------
        /// </summary>
        private List<TravelRequestRemarks> GetRemarksList(Int16 LoadType)
        {          
            List<TravelRequestRemarks> list = new List<TravelRequestRemarks>();

            list = BLL.GetTravelRequestRemarks(LoadType, 0, GlobalCode.Field2Int(Request.QueryString["trID"]),
               GlobalCode.Field2String(Session["UserName"])).ToList();
            return list;
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   05/07/2012
        /// Descrption:     Get Travel Request Remarks
        /// -------------------------------------------------------
        /// </summary>
        private void BindRemarksList()
        {
            List<TravelRequestRemarks> list = new List<TravelRequestRemarks>();
            if (TravelMartVariable.RoleHotelVendor == Session["UserRole"].ToString())
            {
                list = GetRemarksList(2);
            }
            else
            {
                list = GetRemarksList(1);
            }
            

            uoListViewRemarks.DataSource = list;
            uoListViewRemarks.DataBind();
        }
        protected bool IsVisible(object CreatedBy)
        {
            string sCreatedBy = GlobalCode.Field2String(CreatedBy);
            if (sCreatedBy == GlobalCode.Field2String(Session["UserName"]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Date Created:   26/07/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            s = s.Replace("'", "\"");
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "scr", sScript);
        }
        #endregion
    }
}

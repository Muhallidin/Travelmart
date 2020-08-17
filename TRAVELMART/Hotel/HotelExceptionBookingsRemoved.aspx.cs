using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART
{
    public partial class HotelExceptionBookingsRemoved : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Page for the removed records from Exception List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetRemovedExceptions();
            }
            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        protected void uoButtonReturn_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in uoExceptionList.Items)
            {
                CheckBox uoSelectCheckBox = (CheckBox)item.FindControl("uoSelectCheckBox");
                if (uoSelectCheckBox.Checked)
                {
                    HiddenField hfExID = (HiddenField)item.FindControl("hfExID");
                    PageMethods.ExceptionAddRemoveFromList(hfExID.Value, "false", "");
                }
            }
            ClosePage("Exception List successfully updated!");
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    set alternate color
        /// </summary>
        /// <returns></returns> 
        string lastDataFieldValue = null;
        string lastClass = "alternateBg";
        public string OverflowChangeRowColor()
        {

            string currentDataFieldValue = Eval("SeafarerId").ToString();
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                if (lastClass == "")
                {
                    lastClass = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
                else
                {
                    lastClass = "";
                    return "<tr>";
                }
            }
            else
            {
                if (lastClass == "")
                {
                    lastClass = "";
                    return "<tr>";
                }
                else
                {
                    lastClass = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Bind uoExceptionList
        /// ------------------------------------------
        /// </summary>
        protected void GetRemovedExceptions()
        {
            try
            {
                List<ExceptionBooking> list = new List<ExceptionBooking>();
                list = GetHotelExceptionList();
                uoExceptionList.DataSource = list;
                uoExceptionList.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    Get records removed from Exception List
        /// </summary>
        /// <returns></returns>
        private List<ExceptionBooking> GetHotelExceptionList()
        {
            try
            {
                List<ExceptionBooking> list = new List<ExceptionBooking>();
                int iRegionID = GlobalCode.Field2Int(Session["Region"]);
                int iPortID = GlobalCode.Field2Int(Session["Port"]);

                if (Session["HotelTransactionExceptionExceptionBookingRemoved"] != null)
                {
                    list = (List<ExceptionBooking>)Session["HotelTransactionExceptionExceptionBookingRemoved"];
                }
                else
                {
                    list = ExceptionBLL.ExceptionGetRemovedList(GlobalCode.Field2DateTime(Session["DateFrom"]), 
                        GlobalCode.Field2String(Session["UserName"]), 0, iRegionID, iPortID);
                    Session["HotelTransactionExceptionExceptionBookingRemovedRemoved"] = list;
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   01/10/2012
        /// Description:    close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage(string s)
        {

            string sScript = "<script language='JavaScript'>";
            //sScript += "var msg = '" + s + "';";
            //sScript += "alert( msg );";

            sScript += " window.parent.$(\"#ctl00_BodyPlaceHolder_uoHiddenFieldRemoveFromList\").val(\"1\"); ";
            sScript += " window.parent.RefreshPageFromPopupViewRemove(); ";
            sScript += " self.close(); ";
            
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonReturn, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        #endregion

        
    }
}

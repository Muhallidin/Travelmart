using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;

namespace TRAVELMART
{
    public partial class HotelOverflowBookingsRemoved : System.Web.UI.Page
    {
        #region "Events"
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   07/Aug/2014
        /// Description:    Page for the removed records from Overflow List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session.Remove("HotelOverflowBookingsRemovedList");
                GetRemovedOverflow();
            }
            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        protected void uoButtonReturn_Click(object sender, EventArgs e)
        {
            string sOverflowID = "";
            foreach(ListViewItem item in uoListViewOverflow.Items)
            {
                CheckBox uoSelectCheckBox = (CheckBox)item.FindControl("uoSelectCheckBox");
                if (uoSelectCheckBox.Checked)
                {
                    HiddenField hfExID = (HiddenField)item.FindControl("hfExID");
                    sOverflowID = sOverflowID + ";" + hfExID.Value;                  
                }               
            }
            PageMethods.OverflowAddRemoveFromList(sOverflowID, "false", "", Path.GetFileName(Request.Path));
            ClosePage("Overflow List successfully updated!");
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
        /// Description:    Bind uoListViewOverflow
        /// ------------------------------------------
        /// </summary>
        protected void GetRemovedOverflow()
        {
            try
            {
                List<OverflowBooking2> list = new List<OverflowBooking2>();
                list = GetHotelOverflowList();
                uoListViewOverflow.DataSource = list;
                uoListViewOverflow.DataBind();
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
        private List<OverflowBooking2> GetHotelOverflowList()
        {
            try
            {
                List<OverflowBooking2> list = new List<OverflowBooking2>();
                int iRegionID = GlobalCode.Field2Int(Session["Region"]);
                int iPortID = GlobalCode.Field2Int(Session["Port"]);

                if (Session["HotelOverflowBookingsRemovedList"] != null)
                {
                    list = (List<OverflowBooking2>)Session["HotelOverflowBookingsRemovedList"];
                }
                else
                {
                    list = OverFlowBookingBLL.OverflowGetRemovedList(GlobalCode.Field2DateTime(Session["DateFrom"]), 
                        GlobalCode.Field2String(Session["UserName"]), 0, iRegionID, iPortID);
                    Session["HotelOverflowBookingsRemovedList"] = list;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Xml.Linq;
using PDF = iTextSharp;




namespace TRAVELMART
{
    public partial class CrewAssistPopUp : System.Web.UI.Page
    {

        CrewAssistBLL SF = new CrewAssistBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uoHiddenFieldCrewAssistRequestID.Value = GlobalCode.Field2Int(Request.QueryString["RId"]).ToString();

              
            }
            if (Session["HotelEmailTo"] == null || Session["HotelEmailTo"].ToString() == string.Empty)
            {
                uoHiddenFieldEmailVendor.Value = string.Empty;
                uoTextBoxEmailAdd.Visible = true;
                Label1.Visible = true;
            }
            else
            {
                uoHiddenFieldEmailVendor.Value = Session["HotelEmailTo"].ToString();
                uoTextBoxEmailAdd.Visible = false;
                Label1.Visible = false;
            }
        }


         /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonSend_click(object sender, EventArgs e)
        {
            //SendRequest();
            int id = GlobalCode.Field2Int(Request.QueryString["ufn"]);
            string Val = GlobalCode.Field2String(Request.QueryString["Em"]);
            long PortID = GlobalCode.Field2Long(Request.QueryString["porID"]);

            CrewAssistBLL CA = new CrewAssistBLL();
            CA.TblEmail(id, Val, Val, PortID, uoHiddenFieldUser.Value);

            OpenParent();


        }
        

        private void OpenParent()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupHotel\").val(\"1\"); ";

            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(ButtonSend, this.GetType(), "scr", sScript, false);

        }





        /// <summary>
        /// Date Created:   14/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";

            sScript += "alert( msg );";
            sScript += "</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "scr", sScript, false);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }







    }
}

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
    public partial class CrewAssistConfirmation : System.Web.UI.Page
    {

        CrewAssistBLL SF = new CrewAssistBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TextBoxConfirmrate.Text = Request.QueryString["CRate"].ToString();
                uoHiddenFieldEmployeeID.Value = Request.QueryString["SfID"].ToString();
                uoHiddenFieldCheckinDate.Value = GlobalCode.Field2String(Request.QueryString["cDte"]).ToString();
                uoHiddenFieldCheckOutDate.Value = GlobalCode.Field2String(Request.QueryString["EDte"]).ToString();
                
                uoHiddenFieldIDBigint.Value = GlobalCode.Field2String(Request.QueryString["IDbint"]).ToString();
                uoHiddenFieldSeqNo.Value = GlobalCode.Field2String(Request.QueryString["sNo"]).ToString();
                uoHiddenFieldTravelRequestID.Value = GlobalCode.Field2String(Request.QueryString["trReqID"]).ToString();
                uoHiddenFieldBranchID.Value = GlobalCode.Field2String(Request.QueryString["bID"]).ToString();
                uoHiddenFieldHotelTransID.Value = GlobalCode.Field2String(Request.QueryString["TrHID"]).ToString();

            }
          
        }


         /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonSend_click(object sender, EventArgs e)
        {

             
            CrewAssistBLL CA = new CrewAssistBLL();
            string res = CA.GetHotelTransactionOverFlow(GlobalCode.Field2Long( uoHiddenFieldEmployeeID.Value)
                    , GlobalCode.Field2DateTime(uoHiddenFieldCheckinDate.Value)
                    , GlobalCode.Field2DateTime(uoHiddenFieldCheckOutDate.Value)
                    , GlobalCode.Field2Long(uoHiddenFieldIDBigint.Value)
                    , GlobalCode.Field2TinyInt(uoHiddenFieldSeqNo.Value)
                    , GlobalCode.Field2Long(uoHiddenFieldTravelRequestID.Value)
                    , GlobalCode.Field2Int(uoHiddenFieldBranchID.Value)
                    ,""
                    , GlobalCode.Field2Int(uoHiddenFieldHotelTransID.Value));

            if (res == "Record has hotel request already on this date!")
            {
                OpenParentExistHotelRequest(TextBoxWhoConfirm.Text.ToString(), TextBoxConfirmrate.Text.ToString(), res);
            }
            else
            { 
                 OpenParentSend(TextBoxWhoConfirm.Text.ToString(), TextBoxConfirmrate.Text.ToString());
            }

        }
        

        private void OpenParentExit()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCrewHotelCancelPopup\").val(\"2\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCrewHotelCancelPopup\").val(\"2\"); ";

            //sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldPopupHotel\").val(\"1\"); ";

            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(ButtonSend, this.GetType(), "scr", sScript, false);

        }

       
        private void OpenParentSend(string WhoConfirm, string ConfirmRate)
        {

            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCrewHotelCancelPopup\").val(\"1\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldHotelConfirmBy\").val(\"" + WhoConfirm + "\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoTextBoxComfirmRate\").val(\"" + ConfirmRate + "\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_HiddenFieldHideCenter\").val(\"1\"); ";
            
            
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(ButtonSend, this.GetType(), "scr", sScript, false);

        }


        private void OpenParentExistHotelRequest(string WhoConfirm, string ConfirmRate, string msg)
        {

          
            string sScript = "<script language='javascript'>";
           
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCrewHotelCancelPopup\").val(\"1\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldHotelConfirmBy\").val(\"" + WhoConfirm + "\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoTextBoxComfirmRate\").val(\"" + ConfirmRate + "\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldSaveHotel\").val(\"0\"); ";

            //string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + msg + " '; ";

            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";

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
            
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);

        }







    }
}

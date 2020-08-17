using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Data;
using System.IO;

namespace TRAVELMART
{
    public partial class CrewAssistCancelHotel : System.Web.UI.Page
    {


        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 08/06/2013
        /// Description: pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["hId"] != null) uoHiddenFieldHotelID.Value = Request.QueryString["hId"].ToString();
                if (Request.QueryString["hIdb"] != null) uoHiddenFieldHIDBigint.Value = Request.QueryString["hIdb"].ToString();
                if (Request.QueryString["hSno"] != null) uoHiddenFieldSeqNo.Value = Request.QueryString["hSno"].ToString();
                if (Request.QueryString["hTID"] != null) uoHiddenFieldHTravelRequestID.Value = Request.QueryString["hTID"].ToString();
                if (Request.QueryString["eIsV"] != null) uoHiddenFieldIsVehicle.Value = Request.QueryString["eIsV"].ToString();
              
                if (Request.QueryString["hN"] != null) uoLabelHotel.Text = Request.QueryString["hN"].ToString();
                if (Request.QueryString["eadd"] != null) txtEmailTo.Text = Request.QueryString["eadd"].ToString();


                if (uoHiddenFieldIsVehicle.Value == "1")
                {
                    if (Request.QueryString["Vty"] != null) uoHiddenFieldVendorType.Value = Request.QueryString["Vty"].ToString();

                    uoLabelTitle.Text = "Cancel Transporation Booking";
                    Label1.Text = "Vehicle";
                    btnSaveCancelHotel.Text = "Cancel Transporation Booking & Email";
                }
                else
                {
                    if (Request.QueryString["Hty"] != null) uoHiddenFieldVendorType.Value = Request.QueryString["Hty"].ToString();

                    uoLabelTitle.Text = "Cancel Hotel Booking";
                    Label1.Text = "Hotel";
                    btnSaveCancelHotel.Text = "Cancel Hotel Booking & Email";
                }

                string userID = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldUser.Value = userID;

            }
        }

        protected void btnSaveCancelHotel_click(object sender, EventArgs e)
        {

            try
            {
                CrewAssistBLL BLL = new CrewAssistBLL();

                if (uoHiddenFieldIsVehicle.Value == "1")
                {
                    //AlertMessage("Request for hotel Transportation has been sent successfully.");  
                    List<SeafarerDetailHeader> _SDetailList = new List<SeafarerDetailHeader>();
                    Session["SeafarerDetailList"] = null;
                    //_SDetailList = BLL.GetCrewAssistCancelTransportationRequest(GlobalCode.Field2TinyInt(uoHiddenFieldVendorType.Value == "PA" ? 1 : 0), GlobalCode.Field2Long(uoHiddenFieldHotelID.Value)

                    _SDetailList = BLL.GetCrewAssistCancelTransportationRequest(GlobalCode.Field2TinyInt(GlobalCode.Field2String( uoHiddenFieldVendorType.Value) == "True" ? 1 : 0), GlobalCode.Field2Long(uoHiddenFieldHotelID.Value)
                            , GlobalCode.Field2Long(uoHiddenFieldHTravelRequestID.Value)
                            , GlobalCode.Field2Long(uoHiddenFieldHIDBigint.Value)
                            , GlobalCode.Field2Int(uoHiddenFieldSeqNo.Value)
                            , GlobalCode.Field2String(uoHiddenFieldUser.Value)
                            , GlobalCode.Field2String(txtEmailTo.Text)
                            , GlobalCode.Field2String(txtEmailCC.Text)
                            , GlobalCode.Field2String(uoTextBoxConfirmedBy.Text)
                            , GlobalCode.Field2String(uoTextBoxComment.Text)
                            , GlobalCode.Field2DateTime(uoHiddenFieldCurrentDate.Value)
                            , GlobalCode.Field2String(uoHiddenFieldTimeZone.Value)
                            , GlobalCode.Field2String(uoHiddenFieldCurrentDate.Value));
                    Session["SeafarerDetailList"] = _SDetailList;

                }
                else
                {
                    List<SeafarerDetailHeader> _SDetailList = new List<SeafarerDetailHeader>();
                    Session["SeafarerDetailList"] = null;
                    _SDetailList = BLL.GetCancelCrewAssistHotelRequest(GlobalCode.Field2TinyInt(uoHiddenFieldVendorType.Value == "PA" ? 1 : 0), GlobalCode.Field2Long(uoHiddenFieldHotelID.Value)
                            , GlobalCode.Field2String(uoHiddenFieldUser.Value)
                            , GlobalCode.Field2String(txtEmailTo.Text)
                            , GlobalCode.Field2String(txtEmailCC.Text)
                            , GlobalCode.Field2String(uoTextBoxConfirmedBy.Text)
                            , GlobalCode.Field2String(uoTextBoxComment.Text)
                            , GlobalCode.Field2DateTime(uoHiddenFieldCurrentDate.Value)
                            , GlobalCode.Field2String(uoHiddenFieldTimeZone.Value)
                            , GlobalCode.Field2String(uoHiddenFieldCurrentDate.Value) );
                    Session["SeafarerDetailList"] = _SDetailList;
                    //AlertMessage("Request for hotel cancellation has been sent successfully.");
                }

                ClosePage();
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }


            
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   24/Apr/2014
        /// Description:    close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage()
        {
            string sScript = "<script language='JavaScript'>";


            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCrewHotelCancelPopup\").val(\"1\"); ";
            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldCancelHotel\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(btnSaveCancelHotel, this.GetType(), "scr", sScript, false);


            //if (uoHiddenFieldIsVehicle.Value == "1")
            //{
            //    AlertMessage("Request for hotel Transportation has been sent successfully.");
            //}
            //else
            //{
            //    AlertMessage("Request for hotel cancellation has been sent successfully.");
            //}
            
            
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
    }
}

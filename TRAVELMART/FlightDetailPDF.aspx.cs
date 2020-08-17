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
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class FlightDetailPDF : System.Web.UI.Page
    {

        // Create delegate. 
        protected delegate void AsyncTaskDelegate();
        private AsyncTaskDelegate _dlgtSeafarer;

        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 01/15/2014
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

                if (Request.QueryString["SFID"] != null) uoHiddenFieldSeafaredID.Value = Request.QueryString["SFID"].ToString();

                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);

            }
        }

        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }

        public void OnEndExceptions(IAsyncResult ar)
        {

            _dlgtSeafarer.EndInvoke(ar);

            AirDetail();
        }

        public void AirDetail()
        {
            try
            {

                VehicleManifestBLL bll = new VehicleManifestBLL();

                List<FlightHotelDetailPDF> list = new List<FlightHotelDetailPDF>();

                //list = bll.GetFlightHotelDetailPDF(0, GlobalCode.Field2Long(uoHiddenFieldSeafaredID.Value));

                //List<AirDetailPDF> lstAir = new List<AirDetailPDF>();
                //lstAir = list[0].AirDetailPDF;

                //lstviewAirDetail.DataSource = null;

                //lstviewAirDetail.DataSource = lstAir;
                //lstviewAirDetail.DataBind();

                //List<HotelDetailPDF> lstHotel = new List<HotelDetailPDF>();
                //lstHotel = list[0].HotelDetailPDF;
                
                //if (lstHotel.Count > 0)
                //{

                //    HotelDetailPDF n = lstHotel[0];

                //    lblCheckInDate.Text = GlobalCode.Field2String(n.CheckInDate);
                //    lblHotel.Text  = GlobalCode.Field2String(n.Chain);
                //    lblHotelAddress.Text = GlobalCode.Field2String(n.Location); 
                //    lblNoOfDay.Text = GlobalCode.Field2String(n.NoOfDays);
                //    lblRoomType.Text = GlobalCode.Field2String(n.RoomType);
                //    lblContactPerson.Text = GlobalCode.Field2String(n.ContactPerson);
                //    lblContactNumber.Text = GlobalCode.Field2String(n.ContactNumber);
                //    lblEmail.Text = GlobalCode.Field2String(n.Email);

                //}
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }

    }
}

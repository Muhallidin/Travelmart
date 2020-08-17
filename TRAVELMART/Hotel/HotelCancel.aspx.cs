using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TRAVELMART.Common;
using System.Web.Security;
using TRAVELMART.BLL;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;
using System.ComponentModel;

namespace TRAVELMART.Hotel
{
    public partial class HotelCancel : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string sUserName = "";
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                sUserName = MUser.GetUserName();
                Session["UserName"] = sUserName;
            }

            MembershipUser muser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (muser == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                if (muser.IsOnline == false)
                {
                    Response.Redirect("~/Login.aspx", false);
                }
            }
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);
            }
        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["strPrevPage"] = Request.RawUrl;
                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);

            }
        }

        // Create delegate. 
        private AsyncTaskDelegate _dlgtSeafarer;
        protected delegate void AsyncTaskDelegate();
        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  13/May/2014
        /// Description:    Add CountSummary to get name of Hotel to uoLabelHotel      
        ///                 Add EmailRecipient to get vendor's email address
        /// </summary>        
        public void OnEndExceptions(IAsyncResult ar)
        {
            uoListViewHotelCancel.DataSource = null;
            uoListViewHotelCancel.DataBind();
            uoHiddenFieldTravelReq.Value = "";
            List<HotelManifest> lst = new List<HotelManifest>();
            lst = (List<HotelManifest>)Session["ConfirmManifest_TentativeManifest"];

            List<HotelManifest> listConfirm = new List<HotelManifest>();
            listConfirm = (List<HotelManifest>)Session["ConfirmManifest_ConfirmedManifest"];

            List<HotelManifest> listResult = new List<HotelManifest>();                
            string vID = GlobalCode.Field2String(Request.QueryString["VID"]);
            
            List<HotelDashboardList> CountSummary = new List<HotelDashboardList>();
            if (Session["ConfirmManifest_CountSummary"] != null)
            {
                CountSummary = (List<HotelDashboardList>)Session["ConfirmManifest_CountSummary"];
            }

            List<EmailRecipient> EmailRecipient = new List<EmailRecipient>();
            if (Session["ConfirmManifest_EmailRecipient"] != null)
            {
                EmailRecipient = (List<EmailRecipient>)Session["ConfirmManifest_EmailRecipient"];
            }

            char[] s = { ',' };
            string strTravelID = "";
            if (vID.Length > 0)
            {
                strTravelID = vID.Substring(0, vID.Length - 1).Replace("|", ",").ToString();

                long[] TravelID = tolongarray(strTravelID, s);


                uoHiddenFieldTravelReq.Value = strTravelID.ToString();

                if(lst != null)
                {
                    var query = (from item in lst
                            where TravelID.Contains(GlobalCode.Field2Long(item.HotelTransID)) 
                            select item).ToList();
                    listResult.AddRange(query);
                }
                if (listConfirm != null)
                {
                    var queryConfirm = (from item in listConfirm
                                 where TravelID.Contains(GlobalCode.Field2Long(item.HotelTransID))
                                 select item).ToList();
                    listResult.AddRange(queryConfirm);
                }
                
                uoListViewHotelCancel.DataSource = listResult;
                uoListViewHotelCancel.DataBind();

                if (CountSummary.Count > 0)
                {
                    uoLabelHotel.Text = CountSummary[0].HotelBranchName.Trim();
                }
                if (EmailRecipient.Count > 0)
                {
                    txtEmailTo.Text = EmailRecipient[0].EmailTo.Trim();
                }
            }

        }

        private long[] tolongarray(string strTravelID, char[] p)
        {
           
            string[] sa = strTravelID.Split(p);
            long[] ia = new long[sa.Length];
            for (int i = 0; i < ia.Length; ++i)
            {
                long j;
                string s = sa[i];
                if (long.TryParse(s, out j))
                {
                    ia[i] = j;
                }
            }
            return ia;

        }


        /// <summary>
        /// Date Created:  23/Apr/2014
        /// Created By:    Muhallidin G Wali
        /// (description)  cancel hotel booking and insert into a table (TblHotelTransactionOtherCancel)
        /// ==========================================================
        /// Date Modified:  24/Apr/2014
        /// Modified By:    Josephine Gad
        /// (description)   Add ClosePage to close this page and refresh the parent page
        ///                 Change Session["UserName"].ToString() to uoHiddenFieldUser.Value
        ///                 Add parameter fot Audit Trail
        /// ==========================================================
        /// </summary>    
        protected void btnSaveCancelHotel_Click(object sender, EventArgs e)
        {
            try
            {
                ManifestBLL bll = new ManifestBLL();
                string strLogDescription = "Cancel Hotel in Primary Vendor";
                DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                bll.CancelHotelBooking(uoHiddenFieldTravelReq.Value, uoHiddenFieldUser.Value , 
                    txtEmailTo.Text.ToString(), txtEmailCC.Text.ToString(),uoTextBoxConfirmedBy.Text.ToString(),
                    uoTextBoxComment.Text.ToString(),DateTime.Now , true, uoHiddenFieldRole.Value,
                    strLogDescription, "btnSaveCancelHotel_Click",
                    Path.GetFileName(Request.UrlReferrer.AbsolutePath), CommonFunctions.GetDateTimeGMT(dateNow));
                
                ClosePage();
            }
            catch(Exception ex)
            {
                AlertMessage(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Description:    close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage()
        {
            string sScript = "<script language='JavaScript'>";


            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldHotelCancelPopup\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(btnSaveCancelHotel, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
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
    }
}

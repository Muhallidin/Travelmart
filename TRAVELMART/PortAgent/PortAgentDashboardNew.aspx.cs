using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class PortAgentDashboardNew : System.Web.UI.Page
    {
        PortAgentBLL BLL = new PortAgentBLL();


        private AsyncTaskDelegate _ComboListDeligate;
        private AsyncTaskDelegate _PortAgentDeligate;

        protected delegate void AsyncTaskDelegate();

        


        #region "Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("Login.aspx");
            }


            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
            uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);



            Session["strPrevPage"] = Request.RawUrl;
            if (!IsPostBack)
            {



            

                
                LinkButton uoLinkExport = (LinkButton)Master.FindControl("uoLinkExport");
                uoLinkExport.Visible = false;

                HtmlControl ucSpanExportALL = (HtmlControl)Master.FindControl("ucSpanExportALL");
                ucSpanExportALL.Visible = false;

                LinkButton uoLinkButtonViewHotelDashboardByWeek = (LinkButton)Master.FindControl("uoLinkButtonViewHotelDashboardByWeek");
                HtmlControl ucSpanViewWeek = (HtmlControl)Master.FindControl("ucSpanViewWeek");

                uoLinkButtonViewHotelDashboardByWeek.Visible = false;
                ucSpanViewWeek.Visible = false;

              
            }
            
        }



        /// <summary>
        /// Disables the link button.
        /// </summary>
        /// <param name="linkButton">The LinkButton to be disabled.</param>
        public static void DisableLinkButton(LinkButton linkButton)
        {
            linkButton.Attributes.Remove("href");
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
            linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";

            if (linkButton.Enabled != false)
            {
                linkButton.Enabled = false;
            }

            if (linkButton.OnClientClick != null)
            {
                linkButton.OnClientClick = null;
            }



            //if (User.IsInRole(TravelMartVariable.RolePortSpecialist))
            //{
            //    LinkButton lbnConfirmedConfirmed = (LinkButton)uoListViewHotelCount.FindControl("lbnConfirmedConfirmed");
            //    LinkButton lbnPendingHotel = (LinkButton)uoListViewHotelCount.FindControl("lbnPendingHotel");
            //    LinkButton lbnRequestHotel = (LinkButton)uoListViewHotelCount.FindControl("lbnRequestHotel");

            //    if (lbnConfirmedConfirmed != null) lbnConfirmedConfirmed.Enabled = false;
            //    if (lbnPendingHotel != null) lbnPendingHotel.Enabled = false;
            //    if (lbnRequestHotel != null) lbnRequestHotel.Enabled = true;



            //}
            //else
            //{
            //    LinkButton lbnConfirmedConfirmed = (LinkButton)uoListViewHotelCount.FindControl("lbnConfirmedConfirmed");
            //    LinkButton lbnPendingHotel = (LinkButton)uoListViewHotelCount.FindControl("lbnPendingHotel");

            //    LinkButton lbnRequestHotel = (LinkButton)uoListViewHotelCount.FindControl("lbnRequestHotel");

            //    if (lbnConfirmedConfirmed != null) lbnConfirmedConfirmed.Enabled = true;
            //    if (lbnPendingHotel != null) lbnPendingHotel.Enabled = true;
            //    if (lbnRequestHotel != null) lbnRequestHotel.Enabled = false;


            //}
          



        }


        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lbnConfirmedConfirmed = null;
            LinkButton lbnPendingHotel = null;
            LinkButton lbnRequestHotel = null;

            LinkButton lbnConfirmedTrans = null;
            LinkButton lbnPendingTrans = null;
            LinkButton lbnRequestTrans = null;
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                lbnConfirmedConfirmed  = (LinkButton)e.Item.FindControl("lbnConfirmedConfirmed");
                lbnPendingHotel   = (LinkButton)e.Item.FindControl("lbnPendingHotel");
                lbnRequestHotel   = (LinkButton)e.Item.FindControl("lbnRequestHotel");

                lbnConfirmedTrans = (LinkButton)e.Item.FindControl("lbnConfirmedTrans");
                lbnPendingTrans = (LinkButton)e.Item.FindControl("lbnPendingTrans");
                lbnRequestTrans = (LinkButton)e.Item.FindControl("lbnRequestTrans");
            }

            if ( uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
            {
                if (lbnConfirmedConfirmed != null)
                { 
                    lbnConfirmedConfirmed.Attributes.Remove("href");
                    lbnConfirmedConfirmed.Enabled = false;
                    lbnConfirmedConfirmed.OnClientClick = null;
                }

                if (lbnPendingHotel != null)
                {
                    lbnPendingHotel.Attributes.Remove("href");
                    lbnPendingHotel.Enabled = false;
                    lbnPendingHotel.OnClientClick = null;
                } 
                //if (lbnRequestHotel != null) lbnRequestHotel.Enabled = true;

                if (lbnConfirmedTrans != null)
                {
                    lbnConfirmedTrans.Attributes.Remove("href");
                    lbnConfirmedTrans.Enabled = false;
                    lbnConfirmedTrans.OnClientClick = null;
                }

                if (lbnPendingTrans != null)
                {
                    lbnPendingTrans.Attributes.Remove("href");
                    lbnPendingTrans.Enabled = false;
                    lbnPendingTrans.OnClientClick = null;
                }
                //if (lbnRequestTrans != null) lbnRequestHotel.Enabled = true;




            }
            else
            {
                //if (lbnConfirmedConfirmed != null) lbnConfirmedConfirmed.Enabled = true;
                //if (lbnPendingHotel != null) lbnPendingHotel.Enabled = true;
                if (lbnRequestHotel != null)
                {
                    lbnRequestHotel.Attributes.Remove("href");
                    lbnRequestHotel.Enabled = false;
                    lbnRequestHotel.OnClientClick = null;
                }
                if (lbnRequestTrans != null)
                {
                    lbnRequestTrans.Attributes.Remove("href");
                    lbnRequestTrans.Enabled = false;
                    lbnRequestTrans.OnClientClick = null;
                }
            }
        }


        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            uoHiddenFieldRequestDate.Value = Request.QueryString["dt"];
            if (!IsPostBack)
            {
                List<PortAgentHotelVehicle> PortAgentHotelVehicle = new List<PortAgentHotelVehicle>();

                uoListViewHotelCount.DataSource = PortAgentHotelVehicle;
                uoListViewHotelCount.DataBind();

 
                
                PageAsyncTask TaskCombo = new PageAsyncTask(OnBeginComboExceptions, OnEndComboExceptions, null, "Async2", true);
                Page.RegisterAsyncTask(TaskCombo);

              }
            else
            {
                

                //HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

                //if (uoHiddenFieldPopupCalendar.Value == "1" || (sChangeDate == "1"))
                //{
                //    //uoHiddenFieldPopupCalendar.Value = "0";
                //    string sURL = "PortAgentDashboardNew.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];
                //    Context.RewritePath(sURL);
                //}


                string sChangeDate = "";
                if (Request.QueryString["chDate"] != null)
                {
                    sChangeDate = Request.QueryString["chDate"];
                }

                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
                if (uoHiddenFieldPopupCalendar.Value == "1" || (Session["DateFrom"] == null))
                {
                   uoHiddenFieldRequestDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                }

                if (Session["DateFrom"] != null)
                {
                    uoHiddenFieldRequestDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString("MM/dd/yyyy");
                }
                else
                {
                    uoHiddenFieldRequestDate.Value = Session["dt"].ToString();
                }

                //uoHiddenFieldPopupCalendar.Value = "0";
                string sURL = "PortAgentDashboardNew.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldRequestDate.Value;
                Context.RewritePath(sURL);

            }


            PageAsyncTask TaskIMS = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
            Page.RegisterAsyncTask(TaskIMS);

            uoHyperLinkDashboard.NavigateUrl = "/Hotel/HotelNonTurnPorts2.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"];

        }


        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            
            _PortAgentDeligate = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _PortAgentDeligate.BeginInvoke(cb, extraData);
            return result;

        }

        public void OnEndExceptions(IAsyncResult ar)
        {

            _PortAgentDeligate.EndInvoke(ar);
            GetPortAgentHotelVehicle(0, uoHiddenFieldPortCode.Value
                ,GlobalCode.Field2Int(uoDropDownListSeaport.SelectedValue)
                ,GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue)
                ,GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value)
                , GlobalCode.Field2Int(uoDropDownListDays.SelectedValue));
        }

        public void GetPortAgentHotelVehicle(short loadType, string PortCode, int PortID, int PortAgentID, DateTime RequestDate, int Days)
        {
            try
            {

                List<PortAgentHotelVehicle> PortAgentHotelVehicle = new List<PortAgentHotelVehicle>();
                PortAgentHotelVehicle = BLL.GetNonTurnporHotelVehicleDashboard(0, PortCode, PortID, PortAgentID, RequestDate, GlobalCode.Field2String(Session["UserName"]), Days);

                uoListViewHotelCount.DataSource = PortAgentHotelVehicle;
                uoListViewHotelCount.DataBind();

            }
            catch
            {
                throw;
            }

        }



        public IAsyncResult OnBeginComboExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {

            _ComboListDeligate = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _ComboListDeligate.BeginInvoke(cb, extraData);
            return result;

        }

        public void OnEndComboExceptions(IAsyncResult ar)
        {

            _ComboListDeligate.EndInvoke(ar);

            SeaportPortagentDaysList SPDays = new SeaportPortagentDaysList();

            SPDays = BLL.GetSeaportPortagentDays(GlobalCode.Field2String(Session["UserName"]));

            uoDropDownListPortAgent.DataSource = null;
            uoDropDownListPortAgent.Items.Clear();

            uoDropDownListPortAgent.DataSource = SPDays.Portagent;
            uoDropDownListPortAgent.DataTextField = "BranchName";
            uoDropDownListPortAgent.DataValueField = "BranchID";
            uoDropDownListPortAgent.DataBind();

            uoDropDownListSeaport.DataSource = null;
            uoDropDownListSeaport.Items.Clear();

            uoDropDownListSeaport.DataSource = SPDays.Seaport;
            uoDropDownListSeaport.DataTextField = "PortName";
            uoDropDownListSeaport.DataValueField = "PortId";
            uoDropDownListSeaport.DataBind();

            uoDropDownListDays.DataSource = null;
            uoDropDownListDays.Items.Clear();

            uoDropDownListDays.DataSource = SPDays.DaysList;
            uoDropDownListDays.DataTextField = "Days";
            uoDropDownListDays.DataValueField = "Days";
            uoDropDownListDays.DataBind();

        }



        protected void uoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            //GetPortAgentHotelVehicle();

            DropDownList combo = (DropDownList)sender;
            switch (combo.ID) {
                case "uoDropDownListSeaport":
                    BindPortAgent();
                    break;
                
            }

        }

        	
         

        /// -------------------------------------------------------------------
        /// Author:         Josephine Gad
        /// Date Created:   05/Mar/2014
        /// Descrption:     Bind Service Provider in DropDownList
        /// -------------------------------------------------------------------
        private void BindPortAgent()
        {
            List<PortAgentDTO> list = new List<PortAgentDTO>();

            uoDropDownListPortAgent.Items.Clear();
            uoDropDownListPortAgent.Items.Add(new ListItem("--Select Service Provider--", "0"));
            list = BLL.GetPortAgentListByPortId(GlobalCode.Field2Int(uoDropDownListSeaport.SelectedValue),
                  uoHiddenFieldUser.Value, uoHiddenFieldRole.Value);
 
            uoDropDownListPortAgent.DataSource = list;
            uoDropDownListPortAgent.DataTextField = "PortAgentName";
            uoDropDownListPortAgent.DataValueField = "PortAgentID";
            uoDropDownListPortAgent.SelectedValue = "0";
            uoDropDownListPortAgent.DataBind();

            if (list.Count > 0)
            {
                uoDropDownListPortAgent.SelectedIndex = 1;
            }
 
            //string sPortAgentID = GlobalCode.Field2Int(Session["PortAgentID"]).ToString();
            //if (uoDropDownListPortAgent.Items.FindByValue(sPortAgentID) != null)
            //{
            //    uoDropDownListPortAgent.SelectedValue = sPortAgentID;
            //}
                
        }



        void GetPortAgentHotelVehicle()
        {
             
            GetPortAgentHotelVehicle(0, uoHiddenFieldPortCode.Value
              ,GlobalCode.Field2Int(uoDropDownListSeaport.SelectedValue)
              ,GlobalCode.Field2Int(uoDropDownListPortAgent.SelectedValue)
              ,GlobalCode.Field2DateTime(uoHiddenFieldRequestDate.Value)
              ,GlobalCode.Field2Int(uoDropDownListDays.SelectedValue) );


        }


        #endregion    

    }
} 
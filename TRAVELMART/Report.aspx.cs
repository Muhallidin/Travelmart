using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["rpt"] != "" && Request.QueryString["rpt"] != null)
                //{
                //    LoadReport(Convert.ToBoolean(Request.QueryString["p"].ToString()));
                //}
                //else
                //{
                //    LoadReport(false);
                //}
                LoadReport(true);
                PanelReport.Visible = true;
                //ButtonView.Text = "View " + Request.QueryString["rpt"].ToString();
                //ButtonPrint.Text = "Print " + Request.QueryString["rpt"].ToString();
            }
        }

        protected void LoadReport(Boolean ShowButton)
        {
            ArrayList arrParam = new ArrayList();
            ArrayList arrValue = new ArrayList();
            string ReportName = string.Empty;

            //ReportsBLL bll = new ReportsBLL();
            ReportViewer1.ShowPageNavigationControls = false;

            //string a = Request.QueryString["rpt"].ToString();

            switch (Request.QueryString["rpt"].ToString())
            {
                case "Manifest":
                    ReportName = "/test/" + "Manifest";
                    arrParam.Add("pSFDateFrom");
                    arrValue.Add(GlobalCode.Field2String(Session["DateFrom"]));
                    arrParam.Add("pSFDateTo");
                    arrValue.Add(Session["Date"].ToString());
                    arrParam.Add("pUserIDVarchar");
                    arrValue.Add(GlobalCode.Field2String(Session["UserName"]));
                    //arrParam.Add("pFilterByDate");
                    //arrValue.Add(Session["strPendingFilter"]);
                    //arrParam.Add("pRegionIDInt");
                    //arrValue.Add(GlobalCode.Field2String(Session["Region"]));
                    //arrParam.Add("pCountryIDInt");
                    //arrValue.Add( GlobalCode.Field2String(Session["Country"]));
                    //arrParam.Add("pCityIDInt");
                    //arrValue.Add(GlobalCode.Field2String(Session["City"]));
                    //arrParam.Add("pStatus");
                    //arrValue.Add("''");
                    //arrParam.Add("pFilterByNameID");
                    //arrValue.Add("1");
                    //arrParam.Add("pFilterNameID");
                    //arrValue.Add("''");
                    //arrParam.Add("pPortIDInt");
                    //arrValue.Add(GlobalCode.Field2String(Session["Port"]));
                    //arrParam.Add("pHotelIDInt");
                    //arrValue.Add(GlobalCode.Field2String(Session["Hotel"]));
                    //arrParam.Add("pVehicleIDInt");
                    //arrValue.Add("0");
                    //arrParam.Add("pVesselIDInt");
                    //arrValue.Add("0");
                    //arrParam.Add("pNationality");
                    //arrValue.Add("0");
                    //arrParam.Add("pGender");
                    //arrValue.Add("0");
                    //arrParam.Add("pRank");
                    //arrValue.Add("0");

                    //arrParam.Add("pStartRow");
                    //arrValue.Add("0");
                    //arrParam.Add("pMaxRow");
                    //arrValue.Add("10");

                    //arrParam.Add("pByVessel");
                    //arrValue.Add("1");
                    //arrParam.Add("pByName");
                    //arrValue.Add("2");
                    //arrParam.Add("pByRecLoc");
                    //arrValue.Add("0");
                    //arrParam.Add("pByE1ID");
                    //arrValue.Add("0");
                    //arrParam.Add("pByDateOnOff");
                    //arrValue.Add("0");
                    //arrParam.Add("pByDateArrDep");
                    //arrValue.Add("0");
                    //arrParam.Add("pByStatus");
                    //arrValue.Add("0");
                    //arrParam.Add("pByBrand");
                    //arrValue.Add("0");

                    //arrParam.Add("pByPort");
                    //arrValue.Add("0");
                    //arrParam.Add("pByRank");
                    //arrValue.Add("0");
                    //arrParam.Add("pByAirStatus");
                    //arrValue.Add("0");
                    //arrParam.Add("pByHotelStatus");
                    //arrValue.Add("0");
                    //arrParam.Add("pByVehicleStatus");
                    //arrValue.Add("0");

                    arrParam.Add("pRole");
                    arrValue.Add(MUser.GetUserRole());
                    break;

                //default:
                //    ReportName = "/test/NoReport";
                //    break;

            }

            ReportParameter[] parm = new ReportParameter[arrParam.Count];

            try
            {
                ReportViewer1.ShowCredentialPrompts = false;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer1.ServerReport.Timeout = 900000;
                //ReportViewer1.ServerReport.ReportServerCredentials = new ReportCredentials("PTCGRP\\ctl_rbautista", "password1", "FMBR02.ptcgrp.domain\\SQLSERVER2008"); 
                ReportViewer1.ServerReport.ReportServerUrl = new System.Uri("http://10.0.7.142:8080/ReportServer"); 
                ReportViewer1.ServerReport.ReportPath = ReportName;
                for (int i = 0; i < arrParam.Count; i++)
                {
                    parm[i] = new ReportParameter(arrParam[i].ToString(), arrValue[i].ToString());
                }
                if (arrParam.Count > 0)
                {
                    ReportViewer1.ServerReport.SetParameters(parm);
                }

                ReportViewer1.Height = 900;
                ReportViewer1.Width = 820;
                ReportViewer1.ShowParameterPrompts = false;
                ReportViewer1.ShowDocumentMapButton = false;
                ReportViewer1.ShowFindControls = false;
                ReportViewer1.ShowRefreshButton = true;
                ReportViewer1.ShowExportControls = ShowButton;
                ReportViewer1.ShowPrintButton = ShowButton;
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception)
            {
                //Response.Redirect("Report.aspx?SfCode=" + Request.QueryString["SfCode"].ToString() + "&transno=" + Request.QueryString["transno"].ToString() + "&rpt=" + Request.QueryString["rpt"].ToString() + "&p=true");
            }
        }

        protected void ButtonRefreshPrint_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {

        }

        protected void ReportViewer1_ReportError(object sender, ReportErrorEventArgs e)
        {

        }

    }
}

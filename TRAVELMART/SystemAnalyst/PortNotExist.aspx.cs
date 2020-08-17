using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Collections.Generic;

namespace TRAVELMART.SystemAnalyst
{
    public partial class PortNotExist : System.Web.UI.Page
    {
        #region "Event"
        protected void Page_Load(object sender, EventArgs e)
        {

            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
            uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

            if (!IsPostBack)
            {
                Session["strPrevPage"] = Request.RawUrl;

                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;

                BindPortList();
                GetPortNotInTM();
            }
            else
            {
                if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                }
                else
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"];
                }
            }

            if (uoHiddenFieldPopupCalendar.Value == "1")
            {
                BindPortList();
                GetPortNotInTM();
            }

            ListView1.DataSource = null;
            ListView1.DataBind();
        }
        protected void uoDropDownListPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPort.SelectedValue;
            GetPortNotInTM();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// Created By:     Josephine Gad
        /// Date Created:   04/Apr/2013
        /// description     Load Port List 
        /// </summary>
        private void BindPortList()
        {
            List<PortNotExistList> list = new List<PortNotExistList>();
            try
            {
                PortBLL.GetPortForNotInTM("", "", GlobalCode.Field2DateTime(uoHiddenFieldDate.Value));

                list = (List<PortNotExistList>)Session["PortNotExistList"];

                uoDropDownListPort.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListPort.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPort.DataSource = list;
                    uoDropDownListPort.DataTextField = "PortName";
                    uoDropDownListPort.DataValueField = "PortCode";
                    uoDropDownListPort.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListPort.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListPort.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
        }
        private void GetPortNotInTM()
        {
            List<NonTurnPortsList> list = new List<NonTurnPortsList>();
            
            AnalystExceptionBLL.GetNonTurnPortNotInTM(GlobalCode.Field2DateTime(uoHiddenFieldDate.Value),
                uoHiddenFieldUser.Value, GlobalCode.Field2String(Session["Port"]), uoHiddenFieldSortBy.Value);

            list = (List<NonTurnPortsList>)Session["PortNotExistExceptionList"];
        
            uoListViewNonTurnPort.DataSource = list;
            uoListViewNonTurnPort.DataBind();

        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   04/Apr/2013
        /// Description:    pop up alert message
        /// </summary>
        /// <param name="s"></param>
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

        string lastDateFieldValue2 = null;
        string lastClassColor = "alternateBg";
        protected string DashboardChangeRowColor()
        {
            string RowTextString = Eval("IDBigInt").ToString();
            string currentDataFieldValue = RowTextString;
            //See if there's been a change in value
            if (lastDateFieldValue2 != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDateFieldValue2 = currentDataFieldValue;
                if (lastClassColor == "")
                {
                    lastClassColor = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
                else
                {
                    lastClassColor = "";
                    return "<tr>";
                }
            }
            else
            {
                if (lastClassColor == "")
                {
                    lastClassColor = "";
                    return "<tr>";
                }
                else
                {
                    lastClassColor = "alternateBg";
                    return "<tr class=\"alternateBg\">";
                }
            }
        }
        #endregion
    }
}

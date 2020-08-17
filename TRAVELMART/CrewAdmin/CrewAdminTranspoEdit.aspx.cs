using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Data;
using System.Reflection;
using System.IO;

namespace TRAVELMART
{
    public partial class CrewAdminTranspoEdit : System.Web.UI.Page
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
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                GetVehicleToEdit();
                BindTranspo();
            }
            ListView1.DataSource = null;
            ListView1.DataBind();
           
        }       
        protected void uoButtonEmail_Click(object sender, EventArgs e)
        {
            EditVehicle();
            ClosePage ("Email successfully sent!");
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

            string currentDataFieldValue = Eval("E1ID").ToString();
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
        /// Date Created:   26/Dec/2013
        /// Description:    Bind uoListViewTranspo
        /// ------------------------------------------
        /// </summary>
        protected void BindTranspo()
        {
            try
            {
                List<CrewAdminTransport> list = new List<CrewAdminTransport>();
                list = GetCrewAdminTranspoListAdded();
                uoListViewTranspo.DataSource = list;
                uoListViewTranspo.DataBind();

                List<VehicleRoute> routeList = new List<VehicleRoute>();
                routeList = GetVehicleRoute();

                uoDropDownListFrom.Items.Clear();
                uoDropDownListTo.Items.Clear();

                uoDropDownListFrom.Items.Add(new ListItem("--Select Route From--", "0"));
                uoDropDownListTo.Items.Add(new ListItem("--Select Route To--", "0"));

                if (routeList.Count > 0)
                {
                    uoDropDownListFrom.DataSource = routeList;
                    uoDropDownListFrom.DataTextField = "RouteDesc";
                    uoDropDownListFrom.DataValueField = "RouteID";

                    uoDropDownListTo.DataSource = routeList;
                    uoDropDownListTo.DataTextField = "RouteDesc";
                    uoDropDownListTo.DataValueField = "RouteID";
                }

                uoDropDownListFrom.DataBind();
                uoDropDownListTo.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   22/Dec/2013
        /// Description:    Get added records
        /// </summary>
        /// <returns></returns>
        private List<CrewAdminTransport> GetCrewAdminTranspoListAdded()
        {
            try
            {
                List<CrewAdminTransport> list = new List<CrewAdminTransport>();
                if (Session["CrewAdminTranspo_EditedList"] != null)
                {
                    list = (List<CrewAdminTransport>)Session["CrewAdminTranspo_EditedList"];
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
        /// Date Created:   27/Dec/2013
        /// Description:    Get vehicle route
        /// </summary>
        /// <returns></returns>
        private List<VehicleRoute> GetVehicleRoute()
        {
            try
            {
                List<VehicleRoute> list = new List<VehicleRoute>();
                if (Session["CrewAdminTranspo_RouteList"] != null)
                {
                    list = (List<VehicleRoute>)Session["CrewAdminTranspo_RouteList"];
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
        /// Date Created:   12/27/2012
        /// Description:    close pop up page
        /// </summary>
        /// <param name="s"></param>
        private void ClosePage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            //sScript += "var msg = '" + s + "';";
            //sScript += "alert( msg );";

            sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldTransportation\").val(\"2\"); ";
            sScript += " parent.$.fancybox.close(); ";
            //sScript += " window.parent.RefreshPageFromPopupVehicle(); ";
            sScript += " self.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   26/Dec/2013
        /// Description:    Alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {

            string sScript = "<script language='JavaScript'>";
            sScript += "var msg = '" + s + "';";
            sScript += "alert( msg );";

            //sScript += " window.parent.$(\"#ctl00_NaviPlaceHolder_uoHiddenFieldTransportation\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";            
            //sScript += " window.parent.RefreshPageFromPopupVehicle(); ";
            sScript += " self.close(); ";

            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonEmail, this.GetType(), "scr", sScript, false);
            //ClientScript.RegisterClientScriptBlock(typeof(string), "scr", sScript);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   Dec/26/2013
        /// Description:    Bind List and Add/Cancel Transportation
        /// </summary>
        private void GetVehicleToEdit()
        {
            DataTable dt = null;
            try
            {
                if (GlobalCode.Field2String(Request.QueryString["Edit"]) == "")
                {
                    AlertMessage("No record to process!");
                }
                else
                {
                    string[] sAddCancelArr = Request.QueryString["Edit"].Split(",".ToCharArray());
                    string[] sIDBigintArr = Request.QueryString["RecLoc"].Split(",".ToCharArray());
                    string[] sIDTReqArr = Request.QueryString["TReqID"].Split(",".ToCharArray());
                    string[] sIDTrans = Request.QueryString["TransID"].Split(",".ToCharArray());

                    int i = 0;
                    int iTotal = sAddCancelArr.Count();
                    List<CrewAdminTransportAddCancel> list = new List<CrewAdminTransportAddCancel>();
                    CrewAdminTransportAddCancel item = new CrewAdminTransportAddCancel();

                    for (i = 0; i < iTotal; i++)
                    {
                        item = new CrewAdminTransportAddCancel();
                        item.AddCancel = GlobalCode.Field2String(sAddCancelArr[i]);
                        item.IDBigint = GlobalCode.Field2Int(sIDBigintArr[i]);
                        item.TReqID = GlobalCode.Field2Int(sIDTReqArr[i]);
                        item.TransID = GlobalCode.Field2Int(sIDTrans[i]);
                        list.Add(item);
                    }
                    dt = getDataTable(list);

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                    CrewAdminBLL.GetVehicleToEdit(dt, uoHiddenFieldUser.Value, "Edit Transport in Crew Admin Page",
                        "GetVehicleToEdit", Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);

                    List<VehicleVendorEmail> listEmail = new List<VehicleVendorEmail>();
                    if (Session["CrewAdminTranspo_VehicleEmailList"] != null)
                    {
                        listEmail = (List<VehicleVendorEmail>)Session["CrewAdminTranspo_VehicleEmailList"];
                    }
                    if (listEmail.Count > 0)
                    {
//                        uoTextBoxEmailAdd.Text = listEmail[0].VehicleEmailTo;
                        uoTextBoxVehicleVendor.Text = listEmail[0].VehicleName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        private DataTable getDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        public static Type GetCoreType(Type t)
        {
            return t;           
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   27/Dec/2013
        /// Description:    Edit Vehicle 
        /// </summary>
        private void EditVehicle()
        {
            DateTime currentDate = CommonFunctions.GetCurrentDateTime();
            CrewAdminBLL.EditVehicleManifest(uoTextBoxComment.Text, uoTextBoxPickupDate.Text, uoTextBoxPickupTime.Text,
                GlobalCode.Field2TinyInt(uoDropDownListFrom.SelectedValue), GlobalCode.Field2TinyInt(uoDropDownListTo.SelectedValue),
                uoHiddenFieldOtherFrom.Value, uoHiddenFieldOtherTo.Value,
                uoHiddenFieldUser.Value, uoTextBoxEmailAdd.Text, uoTextBoxCopy.Text, "",
                "Edit Transport in Crew Admin Page",
                "EditVehicle", Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now);

        }
        #endregion       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    public partial class HotelDashboardRoomType : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Date Modified:     02/02/2012
        /// Modified By:       Josephine Gad
        /// (description)      Do not use GlobalCode.Field2String(Session["DateFrom"]) and use uoHiddenFieldDate from Request.QueryString["dt"]
        ///                    to avoid error in date conversion
        /// -------------------------------------------
        /// Date Modified:  15/08/2012
        /// Modified By:    Josephine Gad
        /// (description)   Get uoHiddenFieldDateRange.Value from UserAccountList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            if (!IsPostBack)
            {
                uoHiddenFieldDate.Value = Request.QueryString["dt"].ToString(); //gelo
                uoHiddenFieldDate.Value = uoHiddenFieldDate.Value.Replace("_", "/");
                uoHiddenFieldRole.Value = GlobalCode.Field2String(Session["UserRole"]);;

                List<UserAccountList> listUser = GetUserAccountList(GlobalCode.Field2String(Session["UserName"]));
                uoHiddenFieldDateRange.Value = GlobalCode.Field2String(listUser[0].iDayNo);//UserAccountBLL.GetUserDateRange(GlobalCode.Field2String(Session["UserName"])).ToString();
                
                uoTextBoxSearch.Text =  GlobalCode.Field2String(Session["HotelNameToSearch"]) ;
                                
                //if ( Session["strPrevPage"]  == "")
                //{
                //    if (Session["postBack"] == null)
                //    {
                //        Session["postBack"] = "1";
                //    }
                //    else
                //    {
                //        Session["postBack"] = "0";
                //    }
                //}
                //else
                //{
                //    Session["postBack"] = null;
                //}

                if (uoHiddenFieldRole.Value == TravelMartVariable.RoleHotelVendor)
                {
                    TRSearch.Visible = false;
                }

                 Session["strPrevPage"] = Request.RawUrl; //gelo                
                Label uclabelStatus = (Label)Master.FindControl("uclabelStatus");                
                uclabelStatus.Visible = false;

                HtmlControl uoRowDateTo = (HtmlControl)Master.FindControl("uoRowDateTo");
                uoRowDateTo.Visible = false;

                BindColumnName();                
            }

            // Session["strPrevPage"]  = Request.RawUrl;

            if (uoHiddenFieldPopupHotel.Value == "1" || TravelMartVariable.RoleHotelVendor == GlobalCode.Field2String(Session["UserRole"]))
            {
                GetDashboard();
            }
            uoHiddenFieldPopupHotel.Value = "0";
        }

        protected void uoDataPagerDashboard_PreRender(object sender, EventArgs e)
        {
           
        }

        protected void uoObjectDataSourceDashboard_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["RegionID"] =  Session["Region"] .ToString();
            e.InputParameters["CountryID"] =  Session["Country"] .ToString();
            e.InputParameters["CityID"] = Session["City"] .ToString();
            
            e.InputParameters["UserName"] = GlobalCode.Field2String(Session["UserName"]);
            e.InputParameters["UserRole"] = GlobalCode.Field2String(Session["UserRole"]);;
            e.InputParameters["BranchID"] =  Session["Hotel"] .ToString();
            e.InputParameters["DateFrom"] = uoHiddenFieldDate.Value;
            e.InputParameters["DateTo"] = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).AddDays(double.Parse(uoHiddenFieldDateRange.Value)).ToString("MM/dd/yyyy");
            if (GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RoleHotelVendor)
            {
                e.InputParameters["BranchName"] = "";
            }
            else
            {
                e.InputParameters["BranchName"] = uoTextBoxSearch.Text.Trim();
            }
        }
        protected void uoButtonView_Click(object sender, EventArgs e)
        {
             Session["Hotel"] = null;
             Session["HotelNameToSearch"]  = uoTextBoxSearch.Text.Trim();            
            GetDashboard();
        }
        protected void uoListViewDashboard_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRoom")
            {
                Response.Redirect("HotelDashboard.aspx?ufn=" + Request.QueryString["ufn"].ToString());
            }
        }
        #endregion
       
      
        #region Functions
        /// <summary>
        /// Date Created:   24/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set Column Name for room types
        /// <summary>
        private void BindColumnName()
        {

            Label uoLabelRoomType1 = (Label)uoListViewDashboard.FindControl("uoLabelRoomType1");
            Label uoLabelRoomType2 = (Label)uoListViewDashboard.FindControl("uoLabelRoomType2");
            //Label uoLabelRoomType3 = (Label)uoListViewDashboard.FindControl("uoLabelRoomType3");
            //Label uoLabelRoomType4 = (Label)uoListViewDashboard.FindControl("uoLabelRoomType4");
            //Label uoLabelRoomType5 = (Label)uoListViewDashboard.FindControl("uoLabelRoomType5");
            if (uoLabelRoomType1 != null)
            {
                DataTable dt = null;
                try
                {
                    dt = HotelBLL.HotelRoomTypeGetDetails();
                    if (dt.Rows.Count > 0)
                    {
                        int i = 1;
                        foreach (DataRow r in dt.Rows)
                        {
                            if (i == 1)
                            {
                                uoLabelRoomType1.Text = r["colRoomNameVarchar"].ToString();
                            }
                            else if (i == 2)
                            {
                                uoLabelRoomType2.Text = r["colRoomNameVarchar"].ToString();
                            }
                            else if (i > 2)
                            {
                                break;
                            }
                            //else if (i == 3)
                            //{
                            //    uoLabelRoomType3.Text = r["colRoomNameVarchar"].ToString();
                            //}
                            //else if (i == 4)
                            //{
                            //    uoLabelRoomType4.Text = r["colRoomNameVarchar"].ToString();
                            //}
                            //else if (i == 5)
                            //{
                            //    uoLabelRoomType5.Text = r["colRoomNameVarchar"].ToString();
                            //}
                            i++;
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
        }
        //private void GetHotelList()
        //{
        //    DataTable dt = null;
        //    try 
        //    {
        //        ListItem item = new ListItem("Select Hotel", "0");
        //        uoDropDownListHotel.Items.Clear();
        //        uoDropDownListHotel.Items.Add(item);

        //        dt = HotelBLL.GetHotelBranchByCity(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["City"]));

        //        if (dt.Rows.Count > 0)
        //        {
        //            uoDropDownListHotel.DataSource = dt;
        //            uoDropDownListHotel.DataTextField = "BranchName";
        //            uoDropDownListHotel.DataValueField = "BranchID";
        //        }
        //        uoDropDownListHotel.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   24/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Bind Dashboard
        /// ---------------------------------------------------------------------------
        /// </summary>
        private void GetDashboard()
        {
            try
            {
                uoListViewDashboard.DataSource = null;
                uoListViewDashboard.DataSourceID = "uoObjectDataSourceDashboard";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
        /// <summary>
        /// Date Created:   24/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Set Dashboard groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string DashboardAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel Branch";
            string GroupValueString = "BranchName";

            string currentDataFieldValue = Eval(GroupValueString).ToString();

            //Specify name to display if dataFieldValue is a database NULL
            if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
            {
                currentDataFieldValue = "";
            }
            //See if there's been a change in value
            if (lastDataFieldValue != currentDataFieldValue)
            {
                //There's been a change! Record the change and emit the table row
                lastDataFieldValue = currentDataFieldValue;
                //return string.Format("<tr><td class=\"group\" colspan=\"17\">{0}: <strong><a class=\"groupLink\" href=\"Maintenance/HotelMaintenanceBranch.aspx?vmId=" + Eval("BranchID") + "&ufn=" + Request.QueryString["ufn"].ToString() + "\">{1}<a/></strong></td></tr>", GroupTextString, currentDataFieldValue);
                if (Eval("IsWithContract").ToString() == "True")
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"17\">{0}: <strong><a class=\"groupLink\" href=\"#\" onclick='return OpenContract(\"" + Eval("BranchID") + "\")'\">{1}<a/></strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
                else
                {
                    return string.Format("<tr><td class=\"group\" colspan=\"17\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   15/08/2012
        /// Description:    Get user details using session
        /// </summary>
        /// <returns></returns>
        private List<UserAccountList> GetUserAccountList(string sUserName)
        {
            List<UserAccountList> list = new List<UserAccountList>();

            if (Session["UserAccountList"] != null)
            {
                list = (List<UserAccountList>)Session["UserAccountList"];
            }
            else
            {
                list = UserAccountBLL.GetUserInfoListByName("sUserName");
                Session["UserAccountList"] = list;
            }
            return list;
        }
        #endregion

        protected void uoListViewDashboard_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton uoLinkButtonEvents = (LinkButton)e.Item.FindControl("uoLinkButtonEvents");
                HiddenField uoHiddenFieldBranchID = (HiddenField)e.Item.FindControl("uoHiddenFieldBranchID");

                string[] sBranchArr = uoHiddenFieldBranchID.Value.Split("_".ToCharArray());
                string sBranch = sBranchArr[0].ToString();
                string sCity = sBranchArr[1].ToString();
                string sDate = sBranchArr[2].ToString();

                string scriptEventString = "return OpenEventsList('" + sBranch + "', '" + sCity + "', '" + sDate + "');";
                uoLinkButtonEvents.Attributes.Add("OnClick", scriptEventString);            
            }
        }

        ///// <summary>
        ///// Date Created: 21/01/2012
        ///// Created By: Gabriel Oquialda
        ///// (description) String double format            
        ///// </summary>
        //protected string FormatDouble(object oDouble)
        //{
        //    String strDouble = (String)oDouble;

        //    if (strDouble != "")
        //    {
        //        string strFormat;
        //        strFormat = string.Format(String.Format("{0:0.##}", strDouble));
        //        return strFormat;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }
}

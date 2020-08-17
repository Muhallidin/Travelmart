using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace TRAVELMART.ContractManagement
{
    public partial class HotelContractListView : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Modified by:    Charlene Remotigue
        /// Date Modified:  27/10/2011
        /// Description:    added checking for hotel vendor
        /// ------------------------------------------------
        /// Modified by:    Josephine Gad
        /// Date Modified:  27/10/2011
        /// Description:    validate if UserRole is not null
        /// ------------------------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //SFStatus.Visible = false;
            if (!IsPostBack)
            {
                
                //uoTextBoxHotel.Text = Request.QueryString["hbName"];
                HotelContractListViewLogAuditTrail();
                GetContractList(); 
                //set visibility
                
                if (GlobalCode.Field2String(Session["UserRole"]) == TravelMartVariable.RoleHotelVendor)
                {
                    uoHiddenFieldVendor.Value = "false";                    
                }

                ShowHideAmendInactiveContractControls();                                
            } 
        }

        protected void uoHotelContractList_PreRender(object sender, EventArgs e)
        {
            GetContractList(); 
        }

        protected void uoBtnHotelAdd_Click1(object sender, EventArgs e)
        {
            Response.Redirect("HotelContractAdd.aspx?st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }

        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetContractList();
        }

        protected void upDropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetContractList();
        }

        #region uoHotelContractList_ItemCommand
        protected void uoHotelContractList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Select")
            {
                BLL.ContractBLL.UpdateContractFlag(index, GlobalCode.Field2String(Session["UserName"]));
                GetContractList();

                //Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                strLogDescription = "Hotel contract is flagged inactive.";
                strFunction = "uoHotelContractList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(index, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
            }
        }
        #endregion

        #region uoHotelContractList_SelectedIndexChanging
        protected void uoHotelContractList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }
        #endregion

        #region uoHotelContractList_ItemDeleting
        protected void uoHotelContractList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        #endregion

        //protected void ListView1_ItemCreated(object sender, ListViewItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListViewItemType.EmptyItem)
        //    {
        //        HtmlControl AmendTh = (HtmlControl)e.Item.FindControl("AmendTh");
        //        HtmlControl DeleteTh = (HtmlControl)e.Item.FindControl("DeleteTh");
        //        if (AmendTh != null)
        //        {
        //            if (User.IsInRole(TravelMartVariable.RoleAdministrator))
        //            {
        //                AmendTh.Visible = true;
        //                DeleteTh.Visible = true;
        //            }
        //            else
        //            {
        //                AmendTh.Visible = false;
        //                DeleteTh.Visible = false;
        //            }
        //        }
        //    }
        //}

        protected void uoHotelContractList_DataBound(object sender, EventArgs e)
        {
            HtmlControl AmendTh = (HtmlControl)uoHotelContractList.FindControl("AmendTh");
            HtmlControl DeleteTh = (HtmlControl)uoHotelContractList.FindControl("DeleteTh");
            if (AmendTh != null)
            {
                if (User.IsInRole(TravelMartVariable.RoleAdministrator))
                {
                    AmendTh.Visible = true;
                    DeleteTh.Visible = true;
                }
                else
                {
                    AmendTh.Visible = false;
                    DeleteTh.Visible = false;
                }
            }
        }
        #endregion

        #region Function

        private void GetContractList()
        {
            DataView dv = null;
            DataTable dt = null;
            try
            {
                //DataTable dt = BLL.ContractBLL.GetVendorHotelContractList(uoTextBoxHotel.Text, Session["UserName"].ToString());
               dt  = BLL.ContractBLL.GetVendorHotelBranchContractByBranchID(Request.QueryString["vmId"]);

                if (dt.Rows.Count > 0)
                {
                    ucLabelHotel.Text = dt.Rows[0]["colVendorBranchNameVarchar"].ToString() + " Contract List";
                    dv = (DataView)dt.DefaultView;
                    dv.Sort = upDropDownListSort.SelectedValue + " desc";
                    uoHotelContractList.DataSource = dv;
                    uoHotelContractList.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dv != null)
                {
                    dv.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created: 08/08/2011
        /// Created By: Josephine Gad
        /// (description) Change the backgroung color of old record
        /// </summary>
        protected bool InactiveControl(object IsActive)
        {
            //if (IsActive.ToString() == "1")
             Boolean IsActiveBool = (bool)IsActive;
            if(IsActiveBool)
            {
                if (!GlobalCode.Field2Bool(uoHiddenFieldInactiveContract.Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Date Created:   20/07/2011
        /// Created By:     Ryan bautista
        /// (description)   Set Contract list groupings
        /// <summary>
        string lastDataFieldValue = null;
        protected string ContractAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Active";
            string GroupValueString = "colIsActiveBit";

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
                return string.Format("<tr><td class=\"group\" colspan=\"15\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, currentDataFieldValue);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }  

        /// <summary>
        /// Date Modified:      11/01/2012
        /// Modified By:        Gelo Oquialda
        /// (description)       Show amend and inactive contract controls when role is Administrator, Contract Manager, and 24x7
        /// </summary>
        private void ShowHideAmendInactiveContractControls()
        {
            HtmlControl AmendTh = (HtmlControl)uoHotelContractList.FindControl("AmendTh");
            HtmlControl DeleteTh = (HtmlControl)uoHotelContractList.FindControl("DeleteTh");

            //GlobalCode.Field2String(Session["strPrevPage"]) = Request.RawUrl;

            if (User.IsInRole(TravelMartVariable.RoleAdministrator) || User.IsInRole(TravelMartVariable.RoleContractManager) )
            {
                uoHiddenFieldAmendContract.Value = "true";
                uoHiddenFieldInactiveContract.Value = "true";
                uoHiddenFieldAmendContractClass.Value = "thVal";
                uoHiddenFieldInactiveContractClass.Value = "thVal";
                uoHiddenFieldAmendContractClassTD.Value = "";
                uoHiddenFieldInactiveContractClassTD.Value = "";
                uoHiddenFieldAlign.Value = "left";
            }
            else
            {
                uoHiddenFieldAmendContract.Value = "false";
                uoHiddenFieldInactiveContract.Value = "false";
                uoHiddenFieldAmendContractClass.Value = "hideElement";
                uoHiddenFieldInactiveContractClass.Value = "hideElement";
                uoHiddenFieldAmendContractClassTD.Value = "hideElement";
                uoHiddenFieldInactiveContractClassTD.Value = "hideElement";
                uoHiddenFieldAlign.Value = "center";
            }
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelContractListViewLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";
            
            strLogDescription = "Hotel contract list viewed.";
            strFunction = "HotelContractListViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 27/04/2012
        /// Description: set session values
        /// </summary>
        protected void InitializeValues()
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Session["UserName"] = User.Identity.Name;
            }

            MembershipUser sUser = Membership.GetUser(GlobalCode.Field2String(Session["UserName"]));

            if (sUser == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!sUser.IsOnline)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            Session["strPrevPage"] = Request.RawUrl;
            Session["HotelPath"] = Path.GetFileName(Request.Path);

            if (GlobalCode.Field2String(Session["UserRole"]) == "")
            {
                Session["UserRole"] = UserAccountBLL.GetUserPrimaryRole(GlobalCode.Field2String(Session["UserName"]));
            }

            ShowHideAmendInactiveContractControls();

            //ListView1.DataSource = null;
            //ListView1.DataBind();

            //if (User.IsInRole(TravelMartVariable.RoleAdministrator) || User.IsInRole(TravelMartVariable.RoleContractManager))
            //{
            //    ListView1.DataSource = null;
            //    ListView1.DataBind();
            //}
            //else
            //{ 
            //    ListView2.DataSource = null;
            //    ListView2.DataBind();
            //}

            
        }
        #endregion

      

        

    }
}

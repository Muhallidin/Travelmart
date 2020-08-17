using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.Common;
using System.IO;
using TRAVELMART.BLL;

namespace TRAVELMART.ContractManagement
{
    public partial class MeetAndGreetContractListView : System.Web.UI.Page
    {
        #region Events
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 16/03/2012
        /// Description: Change TravelMartVariable to Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalCode.Field2String(Session["UserName"]) == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            Session["strPrevPage"] = Request.RawUrl;
            
            if (!IsPostBack)
            {
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                Session["VehiclePath"] = Path.GetFileName(Request.Path);
                uoTextBoxVehicle.Text = Request.QueryString["vbName"];
                VehicleContractListViewLogAuditTrail();
                GetVendorMeetGreetContractList();
                //uoHyperLinkVehicleAdd.HRef = "~/ContractManagement/VehicleContractAdd.aspx";                
            }            
        }

        protected void uoVehicleContractListPager_PreRender(object sender, EventArgs e)
        {
            GetVendorMeetGreetContractList();
        }

        protected void uoVehicleContractList_PreRender(object sender, EventArgs e)
        {
            GetVendorMeetGreetContractList();
        }
        
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            GetVendorMeetGreetContractList();
        }

        protected void uoBtnVehicleAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ContractManagement/MeetAndGreetContractAdd.aspx?st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"]);
            //Response.Redirect("~/ContractManagement/VehicleContractAdd.aspx");
        }

        protected void upDropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetVendorMeetGreetContractList();
        }

        protected void uoVehicleContractList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Select")
            {
                //Insert log audit trail (Gabriel Oquialda - 15/11/2011)
                strLogDescription = "Meet and Greet contract is flagged inactive.";
                strFunction = "uoVehicleContractList_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                MeetAndGreetBLL.UpdateMeetGreetContractFlag(index, uoHiddenFieldUser.Value,
                    strLogDescription, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate));
                GetVendorMeetGreetContractList();               
            }
        }
        protected void uoVehicleContractList_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            
        }     
        //protected void uoVehicleContractList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        //{

        //}
        #endregion

        #region Functions
        /// <summary>
        /// Date Created: 18/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Get vendor vehicle contract List<ContractVehicle>    
        /// ---------------------------------------------------
        /// Date Modified:  16/Aug/2013
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to void and put it in Session
        /// </summary>  
        /// </summary>  
        public void GetVendorMeetGreetContractList()
        {

            List<ContractMeetAndGreet> list = new List<ContractMeetAndGreet>();
            list = MeetAndGreetBLL.GetVendorMeetAndGreetBranchContractByBranchID(GlobalCode.Field2String(Request.QueryString["vmId"]));

            if (list.Count > 0)
            {
                ucLabelVehicle.Text = list[0].VendorName + " Contract List";
            }
            else
            {
                ucLabelVehicle.Text = "Contract List";
            }

            uoVehicleContractList.DataSource = list;
            uoVehicleContractList.DataBind();
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
            if (IsActiveBool)
            {
                return false;
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
            string GroupValueString = "IsActive";

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

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void VehicleContractListViewLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            strLogDescription = "Vehicle contract list viewed.";
            strFunction = "VehicleContractListViewLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, uoHiddenFieldUser.Value);
        }
        #endregion      

      
    }
}

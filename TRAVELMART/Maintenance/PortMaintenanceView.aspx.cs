using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace TRAVELMART
{
    public partial class PortMaintenanceView : System.Web.UI.Page
    {

        #region Events
        /// <summary>
        /// Modified by: Charlene Remotigue
        /// Date Modified: 26/10/2011
        /// Description: added checking for port specialist
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
                //SFStatus.Visible = false;

                //BindCountry();                           
                uoHyperLinkPortAdd.HRef = "~/Maintenance/PortMaintenance.aspx?vmId=0";
                 Session["strPrevPage"]  = Request.RawUrl;
                //set visibililty
                if (GlobalCode.Field2String(Session["UserRole"])== TravelMartVariable.RolePortSpecialist)
                {
                    //uoCountrySearch.Visible = false;
                    uoSeaportSearch.Visible = false;
                    uoHyperLinkPortAdd.Visible = false;
                }
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldRole.Value = UserAccountBLL.GetUserPrimaryRole(uoHiddenFieldUser.Value);
                Session["UserRole"] = uoHiddenFieldRole.Value;
                Session["UserName"]= uoHiddenFieldUser.Value;
                               
                uoHiddenFieldRegionID.Value = GlobalCode.Field2Int(GlobalCode.Field2String(Session["Region"])).ToString();
                if (uoHiddenFieldRole.Value == TravelMartVariable.RolePortSpecialist)
                {
                    uoHiddenFieldPortAgentID.Value = GlobalCode.Field2Int(GlobalCode.Field2String(Session["UserBranchID"]) == "" ? "0" : GlobalCode.Field2String(Session["UserBranchID"])).ToString();
                }
                GetPortList();
                BindRegion();
                //BindSeaport(0);
            }            
            
            if (uoHiddenFieldPopupPort.Value == "1")
            {
                GetPortList();
            }
            uoHiddenFieldPopupPort.Value = "0";
        }       
        /// <summary>
        /// Date Created: 06/08/2014
        /// Modified By:Michael Brian C. Evangelista
        /// Description: Added for Both Activate and Deactivate Seaport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoPortList_ItemCommand(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            string strLogDescription;
            string strFunction;
            int seaportIDtoActivate;

            if (e.CommandName == "Remove")
            {
                strLogDescription = "Seaport to be flagged inactive.";
                strFunction = "uoSeaport_ItemCommand";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                seaportIDtoActivate = Convert.ToInt32(uoHiddenFieldActivationID.Value);
                
                BLL.ContractBLL.SeaportInActivate(uoHiddenFieldUser.Value, seaportIDtoActivate, strLogDescription, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate));

                GetPortListUpdate();

            }
            if (e.CommandName == "Select")
            {
                strLogDescription = "Seaport to be flagged active.";
                strFunction = "uoSeaport_ItemCommand";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                seaportIDtoActivate = Convert.ToInt32(uoHiddenFieldActivationID.Value);
                BLL.ContractBLL.SeaportActivate(uoHiddenFieldUser.Value, seaportIDtoActivate, strLogDescription, strFunction, Path.GetFileName(Request.Path), CommonFunctions.GetDateTimeGMT(currentDate));
                GetPortListUpdate();
            
            }
        }
        protected void uoPortList_ItemDeleting(object sender, System.Web.UI.WebControls.ListViewDeleteEventArgs e)
        {

        }
        protected void uoPortListPager_PreRender(object sender, EventArgs e)
        {
           // GetPortList();
        }
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            uoHiddenFieldLoadType.Value = "0";
            uoPortList.DataBind();
            //GetPortList();
 
        }    
        protected void uoObjectDataSourceVehicle_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["SeaportName"] = uoTextBoxSearchParam.Text;
        }
        //protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        //{
           
        //    //BindSeaport(1);
        //}
        protected void uoButtonSearch_Click(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            //Session["SeaportID"] = uoDropDownListSeaport.SelectedValue;
            uoHiddenFieldLoadType.Value = "0";
            uoPortList.DataBind();

        }
        #endregion


        #region Functions 
       
        /// <summary>
        /// Date Created: 04/08/2011
        /// Created By: Marco Abejar
        /// (description) Get the list of ports            
        /// </summary>
        /// <summary>
        /// Date Modified:05/08/2014
        /// Created By: Michael Brian C. Evangelista
        /// Description: Replaced Datasource components
        /// </summary>
        private void GetPortList()
        {
            //Int32 CountryID = Convert.ToInt32(uoDropDownListCountry.SelectedValue);
            //uoPortList.DataSource = MaintenanceViewBLL.GetPortList(strPortName, CountryID, GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2String(Session["Region"]));
            //uoPortList.DataBind();
            uoPortList.DataSourceID = "uoObjectDataSourceVehicle";
            uoPortList.DataBind();
            //uoPortList.DataSourceID = "ObjectDataSource_uoPortList";
        }
        /// <summary>
        /// Date Created: 05/08/2014
        /// Created By: Michael Evangelista
        /// Description: For activation and deactivation of seaport
        /// </summary>
        private void GetPortListUpdate()
        {
            GetPortList();
            BindRegion();
            //BindSeaport(0);     

        }
        
        /// <summary>
        /// Date Created: 21/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Bind country
        /// </summary>
        //private void BindCountry()
        //{
        //    DataTable CountryDataTable = null;
        //    try
        //    {
        //        CountryDataTable = CountryBLL.CountryList();
        //        uoDropDownListCountry.Items.Clear();
        //        ListItem item = new ListItem("--SELECT COUNTRY--", "0");
        //        uoDropDownListCountry.Items.Add(item);
        //        uoDropDownListCountry.DataSource = CountryDataTable;
        //        uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
        //        uoDropDownListCountry.DataValueField = "colCountryIDInt";
        //        uoDropDownListCountry.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (CountryDataTable != null)
        //        {
        //            CountryDataTable.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Date Created:   07/03/2012
        /// Created By:     Josephine Gad
        /// (description)   Set Airport Groupings by Seaport
        /// ------------------------------------------------
        /// Date Edited:   19/07/2012
        /// Edited By:     JTemporary Removal of delete in seaport
        /// <summary>
        string lastDataFieldValue = null;
        protected string PortAirportAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Seaport";
            string GroupValueString = "PortName";

            string currentDataFieldValue = Eval("PortCode").ToString() + " - " + Eval(GroupValueString).ToString();

            //string EditString = "<td class=\"group\" colspan=\"1\"><a runat=\"server\" class=\"PortLink\" id=\"uoAEditPort\" href=\"~/Maintenance/PortMaintenance.aspx?vmId=" + Eval("PORTID") + "\">Edit</a></td>";
            //string DeleteString = "<td class=\"group\" colspan=\"1\"><a runat=\"server\" id=\"uoIsDelete\"  href=\"#\" OnClick=\"return DeletePort('" + Eval("PORTID") + "');\">Delete</a></td>";            
            string EditString = "<a runat=\"server\" class=\"PortLink\" id=\"uoAEditPort\" href=\"PortMaintenance.aspx?vmId=" + Eval("PORTID") + "\">Edit</a>";
            //string DeleteString = "<a runat=\"server\" id=\"uoIsDelete\"  href=\"#\" OnClick=\"return DeletePort('" + Eval("PORTID") + "');\">Delete</a>";            
            

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
                //return string.Format("<tr><td class=\"group\" colspan=\"1\">{0}: <strong>{1}</strong></td>{2}{3}</tr>", GroupTextString, currentDataFieldValue, EditString, DeleteString);
                //return string.Format("<tr><td class=\"group\" colspan=\"3\"><table class=\"group2\"><tr><td class=\"title\" >{0}:<strong>{1}</strong></td><td>{2}</td><td>{3}</td></tr></table><td/></tr>", GroupTextString, currentDataFieldValue, EditString, DeleteString);
                return string.Format("<tr><td class=\"group\" colspan=\"3\"><table class=\"group2\"><tr><td class=\"title\" >{0}:<strong>{1}</strong></td><td class=\"title\">{2}</td></tr></table><td/></tr>", GroupTextString, currentDataFieldValue, EditString);
                //return string.Format("<tr><td class=\"group\" colspan=\"3\">{0}: <strong>{1}</strong>{2}{3}</td></tr>", GroupTextString, currentDataFieldValue, EditString, DeleteString);
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>
        ///  Date Created: 05/08/2014
        ///  Created By: Michael Brian C. Evangelista
        ///  Description: Bind Region
        /// </summary>
        private void BindRegion()
        {
            List<RegionList> listRegion = new List<RegionList>();
            if (Session["SeaportRegion"] != null)
            {
                listRegion = (List<RegionList>)Session["SeaportRegion"];
            }
            uoDropDownListRegion.Items.Clear();
            ListItem item = new ListItem("--Select Region--", "0");
            uoDropDownListRegion.Items.Add(item);

            item = new ListItem("--No Region--", "-1");
            uoDropDownListRegion.Items.Add(item);

            item = new ListItem("--With Region--", "-2");
            uoDropDownListRegion.Items.Add(item);

            uoDropDownListRegion.DataSource = listRegion;
            uoDropDownListRegion.DataTextField = "RegionName";
            uoDropDownListRegion.DataValueField = "RegionId";
            uoDropDownListRegion.DataBind();

            string sRegion = GlobalCode.Field2String(Session["Region"]);
            if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
            {
                uoDropDownListRegion.SelectedValue = sRegion;
            }
        }

        private void BindSeaport(Int16 iLoadType)
        {
            List<SeaportDTO> list = new List<SeaportDTO>();

            if (iLoadType == 0)
            {
                if (Session["SeaportListAll"] != null)
                {
                    list = (List<SeaportDTO>)Session["SeaportListAll"];
                }
            }
            else
            {
                List<PortList> listSeaport = new List<PortList>();

                string sRegion = GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue).ToString();
                listSeaport = PortBLL.GetPortListByRegion(uoHiddenFieldUser.Value, uoDropDownListRegion.SelectedValue, "", "");

                list = (from a in listSeaport
                        select new SeaportDTO
                        {
                            SeaportIDString = a.PortId.ToString(),
                            SeaportNameString = a.PortName

                        }).ToList();
            }
            //uoDropDownListSeaport.Items.Clear();
            ListItem item = new ListItem("--Select Seaport--", "0");
           // uoDropDownListSeaport.Items.Add(item);

           // uoDropDownListSeaport.DataSource = list;
           // uoDropDownListSeaport.DataTextField = "SeaportNameString";
          //  uoDropDownListSeaport.DataValueField = "SeaportIDString";
          //  uoDropDownListSeaport.DataBind();

            string sSeaport = GlobalCode.Field2String(Session["SeaportID"]);
            //if (uoDropDownListSeaport.Items.FindByValue(sSeaport) != null)
            //{
            //    uoDropDownListSeaport.SelectedValue = sSeaport;
            //}
        }
        #endregion      
    }
}

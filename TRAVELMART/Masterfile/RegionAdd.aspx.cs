using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;

namespace TRAVELMART
{
    public partial class RegionAdd : System.Web.UI.Page
    {
        #region EVENTS

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// </summary>
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                RegionLogAuditTrail();
                if (Request.QueryString["vmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }                
                Session["RegionId"] = Request.QueryString["vmId"];
                Session["RegionName"] = Request.QueryString["vmName"];

                uoHiddenFieldNew.Value = "1";

                RegionSeaport.RegionSeaportList = null;
                GetSeaport();

                if (Session["RegionName"].ToString() != "")
                {
                    SetRegionDetails();
                }
            }
        }

        /// <summary>
        /// Description: Save Region
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// </summary>
       
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                int rID = Convert.ToInt32(Session["RegionId"].ToString());
                string rName = Session["RegionName"].ToString();
                if (rName == "")
                {
                    Int32 RegionID = MasterfileBLL.MasterfileRegionInsert(uoTextBoxRegionName.Text, GlobalCode.Field2String(Session["UserName"]));

                    foreach (GridViewRow r in uoGridViewSeaport.Rows)
                    {
                        if (r.Cells[0].Text == "0")
                        {
                            Int32 RegionSeaportID = RegionBLL.InsertRegionSeaport("0", GlobalCode.Field2String(RegionID), r.Cells[2].Text,
                                GlobalCode.Field2String(Session["UserName"]));
                            
                            if (RegionSeaportID != 0)
                            {
                                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                                strLogDescription = "Region seaport added.";
                                strFunction = "uoButtonSave_Click";
                                
                                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                                BLL.AuditTrailBLL.InsertLogAuditTrail(RegionSeaportID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                    CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                            }
                        }
                    }

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Region added.";
                    strFunction = "uoButtonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(RegionID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               

                    OpenParentPage();
                }
                else
                {
                    MasterfileBLL.MasterfileRegionUpdate(rID, uoTextBoxRegionName.Text, GlobalCode.Field2String(Session["UserName"]));

                    foreach (GridViewRow r in uoGridViewSeaport.Rows)
                    {
                        if (r.Cells[0].Text == "0")
                        {
                            Int32 RegionSeaportID = RegionBLL.InsertRegionSeaport("0", GlobalCode.Field2String(rID), r.Cells[2].Text,
                                GlobalCode.Field2String(Session["UserName"]));
                            
                            if (RegionSeaportID != 0)
                            {
                                //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                                strLogDescription = "Region seaport added.";
                                strFunction = "uoButtonSave_Click";
                                
                                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                                BLL.AuditTrailBLL.InsertLogAuditTrail(RegionSeaportID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                    CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                            }
                        }
                    }

                    //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                    strLogDescription = "Region updated.";
                    strFunction = "uoButtonSave_Click";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(rID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));               


                    OpenParentPage();
                }
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message);
            }
           
        }

        protected void uoButtonAddSeaport_Click(object sender, EventArgs e)
        {
            CreateDatatableRegionSeaport();           
        }

        /// <summary>
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description)                             
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoGridViewSeaport_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteDataTableRegionSeaport();
        }       
        #endregion


        #region METHODS
        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        
        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: set region details            
        /// </summary>
        protected void SetRegionDetails()
        {            
            if (Request.QueryString["vmId"].ToString() != "")
            {
                uoTextBoxRegionName.Text = Session["RegionName"].ToString();

                bool IsNew = false;
                if (uoHiddenFieldNew.Value == "1")
                {
                    IsNew = true;
                }                
                BindRegionSeaport(IsNew);
            }
            else
            {
                RegionSeaport.RegionSeaportList = new List<RegionSeaport>();                
            }            
        }

        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: Close this page and update parent page
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='Javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupRegion\").val(\"1\"); ";
            //sScript += " window.parent.history.go(0);";
            sScript += " parent.$.fancybox.close(); ";
          
            sScript += "</script>";
            
            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(),"scr", sScript,false);
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void RegionLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"] != "0")
            {
                strLogDescription = "Edit linkbutton for region editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for region editor clicked.";
            }

            strFunction = "RegionLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        /// Date Created:  27/02/2012
        /// Created By:    Gabriel Oquialda
        /// (description)  Get seaport list
        /// -------------------------------
        /// </summary>
        private void GetSeaport()
        {
            List<Seaport> list = null;
            try
            {
                int RegionID = GlobalCode.Field2Int(Request.QueryString["vmId"]);
                if (RegionID == 0)
                {
                    list = RegionBLL.GetSeaport(RegionID, true);
                }
                else
                {
                    list = RegionBLL.GetSeaport(RegionID, false);
                }
                uoDropDownListSeaport.Items.Clear();
                if (list.Count > 0)
                {
                    uoDropDownListSeaport.DataSource = list;
                    uoDropDownListSeaport.DataTextField = "SeaportName";
                    uoDropDownListSeaport.DataValueField = "SeaportID";
                    uoDropDownListSeaport.DataBind();
                }
                uoDropDownListSeaport.Items.Insert(0, new ListItem("--SELECT SEAPORT--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }
        }

        /// <summary>
        /// Date Modified: 27/02/2012
        /// Modified By:   Gabriel Oquialda
        /// (description)  Create data table for seaport
        /// </summary>
        private void CreateDatatableRegionSeaport()
        {
            List<RegionSeaport> RegionSeaportList = null;
            RegionSeaport RegionSeaportItem = null;

            try
            {                
                if (RegionSeaport.RegionSeaportList == null)
                {                    
                    RegionSeaportList = new List<RegionSeaport>();

                    for (int i = 0; i < uoGridViewSeaport.Rows.Count; i++)
                    {
                        RegionSeaportItem = new RegionSeaport();

                        RegionSeaportItem.RegionSeaportID = GlobalCode.Field2Int(uoGridViewSeaport.Rows[i].Cells[0].Text);
                        RegionSeaportItem.RegionID = GlobalCode.Field2TinyInt(uoGridViewSeaport.Rows[i].Cells[1].Text);
                        RegionSeaportItem.SeaportID = GlobalCode.Field2TinyInt(uoGridViewSeaport.Rows[i].Cells[2].Text);
                        RegionSeaportItem.SeaportName = uoGridViewSeaport.Rows[i].Cells[3].Text;

                        RegionSeaportList.Add(RegionSeaportItem);
                    }
                }
                else
                {
                    RegionSeaportList = RegionSeaport.RegionSeaportList;
                }

                RegionSeaportItem = new RegionSeaport();
                RegionSeaportItem.RegionSeaportID = 0;
                RegionSeaportItem.RegionID = GlobalCode.Field2TinyInt(Session["RegionId"].ToString());
                RegionSeaportItem.SeaportID = GlobalCode.Field2TinyInt(uoDropDownListSeaport.SelectedValue);
                RegionSeaportItem.SeaportName = uoDropDownListSeaport.SelectedItem.Text;                
                RegionSeaportList.Add(RegionSeaportItem);

                RegionSeaport.RegionSeaportList = RegionSeaportList;
                BindRegionSeaport(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            }
        }

        /// <summary>
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Bind uoGridViewSeaport
        /// </summary>
        private void BindRegionSeaport(bool IsNew)
        {
            try
            {
                List<RegionSeaport> list = new List<RegionSeaport>();
                if (IsNew || RegionSeaport.RegionSeaportList == null)
                {
                    list = RegionBLL.GetRegionSeaport(GlobalCode.Field2Int(Session["RegionId"]).ToString(), "0", "");
                }
                uoGridViewSeaport.DataSource = list;
                uoGridViewSeaport.Columns[0].Visible = true;
                uoGridViewSeaport.Columns[1].Visible = true;   
                uoGridViewSeaport.Columns[2].Visible = true;   
                uoGridViewSeaport.DataBind();
                uoGridViewSeaport.Columns[0].Visible = false;
                uoGridViewSeaport.Columns[1].Visible = false;
                uoGridViewSeaport.Columns[2].Visible = false;   
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Created: 27/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Remove row of region seaport
        /// --------------------------------------------
        /// </summary>
        private void DeleteDataTableRegionSeaport()
        {
            string strLogDescription;
            string strFunction;

            List<RegionSeaport> RegionSeaportList = null;
            RegionSeaport RegionSeaportItem = null;
            try
            {                
                if (RegionSeaport.RegionSeaportList == null)
                {
                    RegionSeaportList = new List<RegionSeaport>();

                    for (int i = 0; i < uoGridViewSeaport.Rows.Count; i++)
                    {
                        RegionSeaportItem = new RegionSeaport();

                        RegionSeaportItem.RegionSeaportID = GlobalCode.Field2Int(uoGridViewSeaport.Rows[i].Cells[0].Text);
                        RegionSeaportItem.RegionID = GlobalCode.Field2TinyInt(uoGridViewSeaport.Rows[i].Cells[1].Text);
                        RegionSeaportItem.SeaportID = GlobalCode.Field2TinyInt(uoGridViewSeaport.Rows[i].Cells[2].Text);
                        RegionSeaportItem.SeaportName = uoGridViewSeaport.Rows[i].Cells[3].Text;

                        RegionSeaportList.Add(RegionSeaportItem);
                    }
                }
                else
                {
                    RegionSeaportList = RegionSeaport.RegionSeaportList;
                }
                int iIndex = uoGridViewSeaport.SelectedIndex;
                int RegionSeaportID = GlobalCode.Field2Int(uoGridViewSeaport.Rows[iIndex].Cells[0].Text);
                Int16 RegionID = GlobalCode.Field2TinyInt(uoGridViewSeaport.Rows[iIndex].Cells[1].Text);
                Int16 SeaportID = GlobalCode.Field2TinyInt(uoGridViewSeaport.Rows[iIndex].Cells[2].Text);
                string SeaportName = GlobalCode.Field2String(uoGridViewSeaport.Rows[iIndex].Cells[3].Text);

                if (RegionSeaportID != 0)
                {
                    RegionBLL.DeleteRegionSeaport(RegionSeaportID.ToString(), GlobalCode.Field2String(Session["UserName"]));

                    //Insert log audit trail (Gabriel Oquialda - 28/02/2012)
                    strLogDescription = "Region seaport deleted. (flagged as inactive)";
                    strFunction = "DeleteDataTableRegionSeaport";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                    BLL.AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(RegionSeaportID.ToString()), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                        CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }

                RegionSeaportList.RemoveAll(a => a.SeaportID == SeaportID && a.SeaportName == SeaportName);
                BindRegionSeaport(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RegionSeaportList != null)
                {
                    RegionSeaportList = null;
                }
                if (RegionSeaportItem != null)
                {
                    RegionSeaportItem = null;
                }
            }
        }
        #endregion
    }
}

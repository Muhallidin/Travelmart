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
    public partial class StripesRoomType : System.Web.UI.Page
    {
        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStripesRoomTypes();
                BindStripes();
                BindRoomTypes();
            }
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveStripeRoom();
            BindStripesRoomTypes();

            //string sScript = "<script language='javascript'>";
            //sScript += " $.fancybox.close(); ";
            //sScript += "</script>";
            //ClientScript.RegisterClientScriptBlock(GetType(), "scr", sScript, false);
        }
        protected void uoStripesRoomList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            
        }
        protected void uoStripesRoomList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DeleteStripeRoom(e.CommandArgument.ToString());
                BindStripesRoomTypes();
            }
        }
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// Date Created:    26/12/2011
        /// Created By:      Josephine Gad
        /// (description)    Get list of stripes
        /// -------------------------------------------
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to Stripe.StripeList
        /// -------------------------------------------
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Do not use Stripe.StripeList 
        /// -------------------------------------------
        /// </summary>
        private void BindStripes()
        {
            List<Stripe> list = null;
            try
            {
                //if (Stripe.StripeList == null)
                //{
                  list =  MaintenanceViewBLL.GetStripes();
                //}
                uoDropDownListStripes.Items.Clear();

                if (list.Count > 0)
                {
                    uoDropDownListStripes.DataTextField = "StripeName";
                    uoDropDownListStripes.DataValueField = "Stripes";
                    uoDropDownListStripes.DataSource = list;
                }                
                uoDropDownListStripes.DataBind();
                uoDropDownListStripes.Items.Insert(0, new ListItem("--Select Stripe--", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (list != null)
            //    {
            //        list = null;
            //    }
            //}
        }
        /// <summary>
        /// Date Created:   26/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of stripes and room types
        /// -------------------------------------------
        /// </summary>
        private void BindStripesRoomTypes()
        {
            DataTable dt = null;
            try
            {
                dt = MaintenanceViewBLL.GetStripesRoomType();
                uoStripesRoomList.DataSource = dt;
                uoStripesRoomList.DataBind();
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
        /// <summary>
        /// Date Created:   26/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get list of room types
        /// -------------------------------------------
        /// </summary>
        private void BindRoomTypes()
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelRoomType();
                uoDropDownListRoom.Items.Clear();

                if (dt.Rows.Count > 0)
                {
                    uoDropDownListRoom.DataTextField = "colRoomNameVarchar";
                    uoDropDownListRoom.DataValueField = "colRoomTypeID";
                    uoDropDownListRoom.DataSource = dt;
                }               
                uoDropDownListRoom.DataBind();
                uoDropDownListRoom.Items.Insert(0, new ListItem("--Select Room Type--", "0"));
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
        /// <summary>
        /// Date Created:   26/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Save stripes and room types
        /// -------------------------------------------
        /// </summary>
        private void SaveStripeRoom()
        {
            DataTable dtStripeRoom = null;

            if (uoHiddenFieldStripe.Value != "")
            {
                dtStripeRoom = MaintenanceViewBLL.SaveStripeRoomType("0", uoHiddenFieldStripe.Value, uoHiddenFieldRoomID.Value,
                    DateTime.Parse(uoHiddenFieldEffectiveDate.Value), uoHiddenFieldContractLength.Value, GlobalCode.Field2String(Session["UserName"]));

                uoDropDownListStripes.SelectedValue = "";
                uoDropDownListRoom.SelectedValue = "0";

                if (GlobalCode.Field2Int(dtStripeRoom.Rows[0]["dtReturnType"]) == 0)
                {
                    string strLogDescription;
                    string strFunction;

                    //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
                    strLogDescription = "Hotel stripe and room type added.";
                    strFunction = "SaveStripeRoom";

                    DateTime dateNow = CommonFunctions.GetCurrentDateTime();

                    AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(dtStripeRoom.Rows[0]["dtStripeRoomID"]), "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                          CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
                else
                {
                    AlertMessage("Hotel stripe and room type with same effective date already exists.");
                }
            }                        
        }
        /// <summary>
        /// Date Created:   26/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Delete stripes and room types
        /// -------------------------------------------
        /// </summary>
        /// <param name="StripeRoomID"></param>
        private void DeleteStripeRoom(string StripeRoomID)
        {
            MaintenanceViewBLL.DeleteStripeRoomType(StripeRoomID, GlobalCode.Field2String(Session["UserName"]));

            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Hotel stripe and room type deleted. (flagged as inactive)";
            strFunction = "SaveStripeRoom";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(Convert.ToInt32(StripeRoomID), "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        #endregion


        protected void uoButtonAdd_Click(object sender, EventArgs e)
        {
            string strLogDescription;
            string strFunction;

            //Insert log audit trail (Gabriel Oquialda - 16/02/2012)
            strLogDescription = "Add button for stripe room type editor clicked.";
            strFunction = "uoButtonAdd_Click";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }

        /// <summary>
        /// Date Created: 23/02/2012
        /// Created By:   Gabriel Oquialda
        /// (description) Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
    }
}

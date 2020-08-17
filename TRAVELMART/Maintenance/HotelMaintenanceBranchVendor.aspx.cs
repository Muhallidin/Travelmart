using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Configuration;

namespace TRAVELMART
{
    public partial class HotelMaintenanceBranchVendor : System.Web.UI.Page
    {
        #region EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserBranchID"] == null)
                {
                    Response.Redirect("../Login.aspx", true);
                }
                else
                {
                    uoHiddenFieldBranchID.Value = GlobalCode.Field2Int(Session["UserBranchID"]).ToString();
                    uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);

                    lblHotelName.Text = GlobalCode.Field2String(Session["BranchName"]);
                    VendorBranchInfoLoad();                 
                    HotelVendorBranchLogAuditTrail();
                }
            }
        }
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            SaveHotel();
            //VendorBranchInfoLoad();
        }
        protected void uoButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]));
        }

        protected void btnSaveImageLogo_Click(object sender, EventArgs e)
        {
            if (FileUploadImageLogo.HasFile)
            {
                SavePhoto("logo");
            }
            else
            {
                AlertMessage("No Logo image file specified!", true);
            }
        }

        protected void btnSaveImageShuttle_Click(object sender, EventArgs e)
        {
            if (FileUploadImageShuttle.HasFile)
            {
                //Get the file size in KB
                decimal dSize = Convert.ToDecimal(String.Format("{0:0.00}", FileUploadImageShuttle.FileBytes.Length / 1024.0));
                
                HttpRuntimeSection section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                decimal dMaxSize = Convert.ToDecimal(section.MaxRequestLength);
                if (dMaxSize < dSize)
                { 
                    string sMsg = "Allowable maximum size is set to " + dMaxSize.ToString().Trim() + " KB.";
                    AlertMessage(sMsg, true);
                }
                else
                {
                    SavePhoto("shuttle");
                }
            }
            else
            {
                AlertMessage("No Shuttle image file specified!", true);
            }
        }
       
        #endregion

        #region FUNCTIONS

        private void SaveHotel()
        {
            try
            {
                Int32 iBranchID = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
                VendorMaintenanceBLL.vendorBranchMaintenanceUpdate(iBranchID, uoTextBoxInstructionOn.Text,
                    uoTextBoxInstructionOff.Text, "Update Hotel Instruction", "SaveHotel", Path.GetFileName(Request.Path),
                    CommonFunctions.GetDateTimeGMT(CommonFunctions.GetCurrentDateTime()), DateTime.Now, uoHiddenFieldUser.Value);
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message, true);
            }

            
        }
        /// Date Created:   10/Nov/2017
        /// Created By:     JGM
        /// (description)   Audit trail logs
        protected void HotelVendorBranchLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";
                       
            strLogDescription = "View Hotel Vendor Maintenance";
            strFunction = "HotelVendorBranchLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        /// <summary>
        /// Date Created:   10/Nov/2017
        /// Created By:     JGM
        /// (description)   Load Hotel Info of user
        /// </summary>
        private void VendorBranchInfoLoad()
        {
            List<HotelBranchDetails> listHotel = new List<HotelBranchDetails>();

            int iHotelId = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
            if (iHotelId > 0)
            {
                VendorMaintenanceBLL.vendorBranchMaintenanceInformation(iHotelId);
                listHotel = (List<HotelBranchDetails>)Session["HotelMaintenance_HotelDetails"];
                if (listHotel.Count > 0)
                {
                    uoTextBoxInstructionOn.Text = listHotel[0].InstructionOn;
                    uoTextBoxInstructionOff.Text = listHotel[0].InstructionOff;
                }
            }
        }
        /// Date Modified:  10/Nov/2017
        /// Modified By:    JMonteza
        /// (description)   Replace ', \n and \r with empty string
        /// </summary>
        private void AlertMessage(string s, bool IsScripManager)
        {
            string sScript = "<script language='JavaScript'>";

            s = s.Replace("'", "");
            s = s.Replace("\n", "");
            s = s.Replace("\r", " ");

            sScript += "alert('" + s + "');";
            sScript += "</script>";
            if (IsScripManager)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "script", sScript, false);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
            }
        }
        /// <summary>
        /// Date Modified:  03/Jan/2018
        /// Modified By:    JMonteza
        /// (description)   Save photo in Directory and call TM-API to save it in Panda
        /// </summary>
        /// <returns></returns>
        private bool SavePhoto(string sEntityType)
        {
            bool bReturn = false;
            try
            {
                string sFileExtension = "";
                string sLocation = "";
                string sIDName = "";

                if (sEntityType == "shuttle")
                {
                    sFileExtension = Path.GetExtension(FileUploadImageShuttle.FileName);
                    sLocation = Server.MapPath("~/FileUploaded/HotelShuttle/");
                    sIDName = "HotelID";
                }
                else if (sEntityType == "logo")
                {
                    sFileExtension = Path.GetExtension(FileUploadImageLogo.FileName);
                    sLocation = Server.MapPath("~/FileUploaded/HotelLogo/");
                    sIDName = "HotelID";
                }
                sFileExtension = sFileExtension.Replace(".", "");

                string[] sImageExtension = ConfigurationManager.AppSettings["ImageExtension"].ToString().Split(",".ToCharArray());
                if (sImageExtension.Contains(sFileExtension.ToLower().Trim()))
                {                    
                    string sBranchID = uoHiddenFieldBranchID.Value;

                    DirectoryInfo dir = new DirectoryInfo(sLocation);
                    FileInfo[] files = dir.GetFiles(sBranchID + ".*");
                    if (files.Length > 0)
                    {
                        //File exists
                        foreach (FileInfo file in files)
                        {
                            file.Delete();
                        }
                    }
                    //else
                    //{
                    //    //File does not exist
                    //}
                    
                    //save photo in directory
                    sLocation += sBranchID + "." + sFileExtension.ToLower();
                    if (sEntityType == "shuttle")
                    {
                        FileUploadImageShuttle.SaveAs(sLocation);
                    }
                    else if (sEntityType == "logo")
                    {
                        FileUploadImageLogo.SaveAs(sLocation);
                        sEntityType = "hotel";
                    }


                    bReturn = WSConnection.SaveToMediaServer(sIDName, sBranchID, sFileExtension.ToLower(), sEntityType);
                    bReturn = true;
                }
                else
                {
                    AlertMessage("Invalid extension file", true);
                }
                return bReturn;
            }
            catch (Exception ex)
            {
                AlertMessage(ex.Message, true);
                return bReturn;
            }
        }

        
        #endregion

       

       
    }
}

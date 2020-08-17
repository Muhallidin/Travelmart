using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace TRAVELMART.ContractManagement
{
    public partial class HotelContractAdd : System.Web.UI.Page
    {
        #region DECLARATION

        private string _FileName;
        private string _FileType;
        private Int32 _FileSize = 0;
        private Byte[] _FileData;
        private DateTime _Now = DateTime.Now;
        static List<ContractHotelAttachment> _contractHotelAttachments = new List<ContractHotelAttachment>(); // add for contract attachment -JEFF

        #endregion

        #region EVENTS
        /// <summary>
        /// Date Created:   23/08/2011
        /// Created By:     Ryan Bautista
        /// (description)   Load all details      
        /// </summary>
        /// 


        protected void Page_Load(object sender, EventArgs e)
        {
            //Label SFStatus = (Label)Master.FindControl("uclabelStatus");
            //SFStatus.Visible = false;
            if (!IsPostBack)
            {
                Session.Remove("ContractAdd_HotelDetails");

                LoadHotelTimeZone();
                HotelContractLogAuditTrail();
                HotelLoad();
                //HotelBranchLoad();
                HotelBranchLoadByVendorID();
                //ChangeToUpperCase(uoDropDownListVendor);
                CountryListByVendorID();
                //ChangeToUpperCase(uoDropDownCountry);
                CityListByVendorID();
                //ChangeToUpperCase(uoDropDownListCity);
                //HotelRoomTypeLoad();
                //ChangeToUpperCase(uoDropDownListRoomType);
                CurrencyLoad();
                //ChangeToUpperCase(uoDropDownListCurrency);
                ViewState["Add"] = "False";
                ////ViewState["AddBranch"] = "False";
                _contractHotelAttachments = new List<ContractHotelAttachment>(); // reset the list to new list
                uoGridViewHotelContractAttachment.DataSource = _contractHotelAttachments;
                uoGridViewHotelContractAttachment.DataBind();

                uoLabelCurrency.Text = "";
                ContractLoadByBranch();


               

                //TimeZoneInfo timeZoneInfo;
                //timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                //datatime = TimeZoneInfo.ConvertTime(currentTime, timeZoneInfo);

                 //foreach (TimeZoneInfo timeZone in tzi)
                //{
                //    if (timeZone.Id == tzID
                //        || timeZone.DaylightName == tzID
                //        || timeZone.DisplayName == tzID
                //        )
                //    {
                //        datatime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentTime, timezone, timeZone.Id); //,  timeZone. .GetUtcOffset(currentTime);
                //    }
                //}

                //System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> timeZone = TimeZoneInfo.GetSystemTimeZones();
                
               

               

            }
        }

        void LoadHotelTimeZone()
        {

            var timeZone = (from n in TimeZoneInfo.GetSystemTimeZones().AsEnumerable()
                            select new
                            {
                                TimezonID = n.Id,
                                DisplayName = n.DisplayName,
                                StandardName = n.StandardName,
                                DaylightName = n.DaylightName,
                            }).ToList();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--Select Timezone--";
            cboHotelTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones();
            cboHotelTimeZone.DataTextField = "DisplayName";
            cboHotelTimeZone.DataValueField = "Id";
            cboHotelTimeZone.DataBind();

            cboHotelTimeZone.Items.Insert(0, item);
            cboHotelTimeZone.SelectedIndex = 0;
        }

        protected void uoBtnAdd_Click(object sender, EventArgs e)
        {
            if (uoDropDownlistCurrency.SelectedValue != "0")
            {
                uoLabelCurrency.Text = uoDropDownlistCurrency.SelectedItem.Text;
            }
            else
            {
                uoLabelCurrency.Text = "";
            }

            DateTime dtFrom = GlobalCode.Field2DateTime(uoTxtFrom.Text);
            DateTime dtTo = GlobalCode.Field2DateTime(uoTxtTo.Text);
                      
            if (dtTo < dtFrom)
            {
                AlertMessage("Invalid Date To!", true);
            }
            else if (!IsDateWithinContract(GlobalCode.Field2DateTime(uoTxtFrom.Text)))
            {
               AlertMessage("Date From is outside the contract period!", true);
            }
            else if (!IsDateWithinContract(GlobalCode.Field2DateTime(uoTxtTo.Text)))
            {
                AlertMessage("Date To is outside the contract period!", true);
            }
            else
            {
                if (uoBtnAdd.Text == "Add")
                {
                    //AddNewRowToGrid();
                    AddNewRow();
                    uoBtnAdd.Focus();
                }
                else
                {
                    UpdateRowToGrid();
                    uoBtnAdd.Text = "Add";
                }

                uoBtnAdd.Enabled = false;
            }
        }

                /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 06/06/2012
        /// Description: remove listview item
        /// --------------------------------
        /// Modified by:    Josephine Monteza
        /// Date Modified:  01/June/2016
        /// Description:    Use DeleteEditGrid function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //string Deleted = GlobalCode.Field2String(ViewState["DeletedItems"]);
           

            string[] sCommandArray = e.CommandArgument.ToString().Split("-".ToCharArray());
            string sCommand = "";
            string sRowNo = "0";
            if (sCommandArray.Count() > 0)
            {
                sCommand = sCommandArray[0];
                sRowNo = sCommandArray[1];
            }
            if (sCommand == "Delete")
            {
                uoBtnAdd.Text = "Add";
            }
            else if (sCommand == "Edit")
            {
                uoBtnAdd.Text = "Update";
            }
            DeleteEditGrid(sCommand, sRowNo);
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 06/06/2012
        /// Description: remove listview item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ////protected void uoListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        ////{
        //    DataTable dtCurrentTable = null;
        //    try
        //    {
        //        string Deleted = GlobalCode.Field2String(ViewState["DeletedItems"]);
        //        string DetailId = "";
        //        string DetailId2 = "";
        //        if (ViewState["CurrentTable"] != null)
        //        {
        //            dtCurrentTable = (DataTable)ViewState["CurrentTable"];

        //            //var selectedItem = (from a in dtCurrentTable.AsEnumerable()
        //            //                        where a["RowNumber"].Equals(e.CommandArgument)
        //            //                    select a
        //            //                        ).ToList();

        //            if (e.CommandArgument.ToString().Substring(0, 3) == "A- ")
        //            {

        //                DataRow[] row = dtCurrentTable.Select("RowNumber = " + e.CommandArgument.ToString().Remove(0, 3));
        //                DetailId = row[0]["DetailId"].ToString();
        //                DetailId2 = row[0]["DetailId2"].ToString();
        //                //DetailId = dtCurrentTable.Rows[Convert.ToInt32(e.CommandArgument)]["DetailId"].ToString();
        //                //DetailId2 = dtCurrentTable.Rows[Convert.ToInt32(e.CommandArgument)]["DetailId2"].ToString();


        //                if (DetailId != "0")
        //                {
        //                    Deleted = Deleted + "," + DetailId;
        //                }
        //                if (DetailId2 != "0")
        //                {
        //                    Deleted = Deleted + "," + DetailId2;
        //                }

        //                //dtCurrentTable.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument) - 1);
        //                dtCurrentTable.Rows.Remove(row[0]);
        //                ViewState["CurrentTable"] = dtCurrentTable;

        //                uoListView.DataSource = dtCurrentTable;
        //                uoListView.DataBind();

        //                ViewState["DeletedItems"] = Deleted;

        //                uoBtnAdd.Text = "Add";

        //            }
        //            else
        //            {

        //                DataRow[] drCurrentRow = dtCurrentTable.Select("RowNumber = " + e.CommandArgument.ToString().Remove(0, 3));
                      
        //                uoTxtFrom.Text = GlobalCode.Field2DateTime(drCurrentRow[0]["DateFrom"]).ToString();

        //                uoTxtTo.Text = GlobalCode.Field2DateTime(drCurrentRow[0]["DateTo"] ).ToString();
        //                uoTxtSun.Text= GlobalCode.Field2String(drCurrentRow[0]["Sun"]);
        //                uoMon.Text = GlobalCode.Field2String(drCurrentRow[0]["Mon"] );
        //                uoTue.Text = GlobalCode.Field2String(drCurrentRow[0]["Tue"]);
        //                uoWed.Text = GlobalCode.Field2String(drCurrentRow[0]["Wed"]);
        //                uoThu.Text = GlobalCode.Field2String(drCurrentRow[0]["Thu"]);
        //                uoFri.Text = GlobalCode.Field2String(drCurrentRow[0]["Fri"] );
        //                uoSat.Text = GlobalCode.Field2String(drCurrentRow[0]["Sat"] );

        //                uoTxtSun2.Text = GlobalCode.Field2String(drCurrentRow[0]["Sun2"]);
        //                uoMon2.Text = GlobalCode.Field2String(drCurrentRow[0]["Mon2"] );
        //                uoTue2.Text = GlobalCode.Field2String(drCurrentRow[0]["Tue2"] );
        //                uoWed2.Text = GlobalCode.Field2String(drCurrentRow[0]["Wed2"] );
        //                uoThu2.Text = GlobalCode.Field2String(drCurrentRow[0]["Thu2"] );
        //                uoFri2.Text = GlobalCode.Field2String(drCurrentRow[0]["Fri2"] );
        //                uoSat2.Text = GlobalCode.Field2String(drCurrentRow[0]["Sat2"] );
        //                uotextboxRoomRate.Text = GlobalCode.Field2Decimal(drCurrentRow[0]["SingleRate"]).ToString("N2");
        //                uotextboxRoomRate2.Text = GlobalCode.Field2Decimal(drCurrentRow[0]["DoubleRate"]).ToString("N2");
        //                uoRoomTax.Text = GlobalCode.Field2String(drCurrentRow[0]["Tax"]);

        //                uoChkRoomTaxInc.Checked= GlobalCode.Field2Bool(drCurrentRow[0]["TaxInclusive"] );
        //                uoDropDownlistCurrency.SelectedIndex = GlobalCode.GetselectedIndex(uoDropDownlistCurrency, GlobalCode.Field2Int(drCurrentRow[0]["CurrencyId"]));
        //                uoHiddenFieldRowNo.Value = e.CommandArgument.ToString().Remove(0, 3);

        //                uoBtnAdd.Text = "Update";

        //                //drCurrentRow["Currency"] = uoDropDownlistCurrency.SelectedItem.Text;
        //                // = GlobalCode.Field2Int(uoDropDownlistCurrency.SelectedValue);

        //                //dtCurrentTable.Rows.Add(drCurrentRow);
        //                //ViewState["CurrentTable"] = dtCurrentTable;

        //                //uoListView.DataSource = dtCurrentTable;
        //                //uoListView.DataBind();

        //                //if (DetailId != "0")
        //                //{
        //                //    Deleted = Deleted + "," + DetailId;
        //                //}
        //                //if (DetailId2 != "0")
        //                //{
        //                //    Deleted = Deleted + "," + DetailId2;
        //                //}

        //                //dtCurrentTable.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument) - 1);
        //                //dtCurrentTable.Rows.Remove(row[0]);
        //                //ViewState["CurrentTable"] = dtCurrentTable;

        //                //uoListView.DataSource = dtCurrentTable;
        //                //uoListView.DataBind();

        //                //ViewState["DeletedItems"] = Deleted;

        //            }
        //        }
        //        uoBtnAdd.Focus();
        //        uoBtnAdd.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dtCurrentTable != null)
        //        {
        //            dtCurrentTable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Date Created: 23/08/2011
        /// Created By:   Ryan Bautista
        /// (description) Save/Update Hotel contract  
        /// -----------------------------------------
        /// Modofied By: CHarlene Remotigue
        /// Date Modified: 06/06/2012
        /// Description: move saving to a different class
        /// </summary>
        protected void uobtnSave_Click(object sender, EventArgs e)
        {
            Int32 pID = 0;
            string strLogDescription;
            string strFunction;
            object sqlTransaction;

            pID = SaveContract(out sqlTransaction);

            if (pID > 0)
            {
                //Save images
                SaveHotelAttachments(pID, sqlTransaction);

                SaveGridItems(pID);
            }


            //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
            strLogDescription = "Hotel contract added.";
            strFunction = "uobtnSave_Click";

            DateTime currentDate = CommonFunctions.GetCurrentDateTime();

            BLL.AuditTrailBLL.InsertLogAuditTrail(pID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                  CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, MUser.GetUserName());

            Session.Remove("ContractAdd_HotelDetails");
            Response.Redirect("HotelContractListView.aspx?vmId=" + Request.QueryString["vmId"] + "&cID=" + Request.QueryString["cID"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
        }

        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) select country and city after selecting a vendor branch
        /// </summary>
        protected void uoDropDownListVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountryListByVendorID();
            CityListByVendorID();
            ChangeToUpperCase(uoDropDownCountry);
        }

        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) select city after select vendor branch 
        /// </summary>
        protected void uoDropDownCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            CityListByVendorID();
            ChangeToUpperCase(uoDropDownListCity);
        }

        protected void uoCheckBoxLunchDinner_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxLunchDinner.Checked)
            {
                uoCheckBoxLunchDinner.Checked = true;
                uoCheckBoxLunch.Checked = false;
                uoCheckBoxDinner.Checked = false;
            }
        }

        protected void uoCheckBoxLunch_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxLunch.Checked)
            {
                uoCheckBoxLunch.Checked = true;
                uoCheckBoxLunchDinner.Checked = false;
            }
        }

        protected void uoCheckBoxDinner_CheckedChanged(object sender, EventArgs e)
        {
            if (uoCheckBoxDinner.Checked)
            {
                uoCheckBoxDinner.Checked = true;
                uoCheckBoxLunchDinner.Checked = false;
            }
        }

        /// <summary>
        /// Date Created: 14/9/2011
        /// Created By: Ryan Bautista
        /// (description) select vendor branch, country and city after selecting a vendor 
        /// </summary>
        protected void uoDropDownListVendorMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelBranchLoadByVendorID();
            CountryListByVendorID();
            CityListByVendorID();
            ChangeToUpperCase(uoDropDownCountry);
        }


        /// <summary>
        /// Date Modified:      24/07/2012
        /// Modified by:        Jefferson Bermundo
        /// Description:        Create a file upload checker for uploading file greater than 10MB
        /// -------------------------------------------------------------------------------------
        /// Date Modified:      27/07/2012
        /// Modified by:        Jefferson Bermundo
        /// Description:        Create a handle for multiple Attachment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoButtonUpload_Click(object sender, EventArgs e)
        {
            if (uoFileUploadContract.PostedFile == null || string.IsNullOrEmpty(uoFileUploadContract.PostedFile.FileName) || uoFileUploadContract.PostedFile.InputStream == null)
            {
                Label1.Text = "<br />Error - unable to upload file. Please try again.<br />";
                uoFileUploadContract.Focus();
            }
            else
            {
                ContractHotelAttachment chaModel = new ContractHotelAttachment();
                int fileSize = uoFileUploadContract.PostedFile.ContentLength;
                if (fileSize > (10000 * 1024)) //10000 means 10MB
                {
                    Label1.Text = "Filesize is too large. Maximum file size permitted is 10MB";
                    uoFileUploadContract.Focus();
                    return;
                }
                //Label1.Visible = false;
                byte[] imageBytes = new byte[uoFileUploadContract.PostedFile.InputStream.Length + 1];
                uoFileUploadContract.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                uoHiddenFieldFileName.Value = uoFileUploadContract.FileName;
                uoHiddenFieldFileType.Value = uoFileUploadContract.PostedFile.ContentType;
                
                //uoFileUploadContract.Visible = false;
                //ucLabelAttached.Visible = true;
                
                //ucLabelAttached.Text = uoHiddenFieldFileName.Value;
                
                ViewState["imageByte"] = imageBytes;
                //uoLinkAttach.Visible = false;
                //uoLinkButtonRemove.Visible = true;
                uobtnSave.Focus();
                
                chaModel.FileName = uoFileUploadContract.FileName;
                chaModel.FileType = uoFileUploadContract.PostedFile.ContentType;
                chaModel.UploadedDate = DateTime.Now;
                chaModel.uploadedFile = imageBytes;
                _contractHotelAttachments.Add(chaModel);

                uoGridViewHotelContractAttachment.DataSource = _contractHotelAttachments;
                uoGridViewHotelContractAttachment.DataBind();

                //_FileSize = imageBytes.Length;
                //uoHiddenFieldFileData.Value = System.Text.Encoding.ASCII.GetString(imageBytes);

                //ucLabelAttached.Text = "Attached: " + uoFileUploadContract.FileName + " (" + imageBytes.Length + " KB)";
                //ucLabelAttached.Visible = true;
                //uoLinkButtonRemove.Visible = true;

                //ContractBLL.InsertAttachHotelContract(uoFileUploadContract.FileName, imageBytes, imageBytes.Length, uoFileUploadContract.PostedFile.ContentType);
            }
        }


        protected void uoLinkButtonRemove_Click(object sender, EventArgs e)
        {            
            uoHiddenFieldFileName.Value = "";
            uoHiddenFieldFileType.Value = "";
            uoFileUploadContract.Visible = true;
            //ucLabelAttached.Visible = false;
            //ucLabelAttached.Text = uoHiddenFieldFileName.Value;
            ViewState["imageByte"] = null;
            uoLinkAttach.Visible = true;
            //uoLinkButtonRemove.Visible = false;
            uobtnSave.Focus();
        }
        protected void ucLabelAttached_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            LinkButton obj = (LinkButton)sender;
            int attachmentId = Convert.ToInt32(obj.CommandArgument);
            byte[] bytes = (byte[])_contractHotelAttachments.AsEnumerable().Where(
                    data => data.AttachmentId == attachmentId).Select(data => data.uploadedFile).FirstOrDefault();


            string fPath = System.IO.Path.GetTempPath();
            string fName = fPath + System.IO.Path.GetTempFileName();

            //Byte[] bytes = (Byte[])ViewState["imageByte"];
            //if (File.Exists(fName))
            //{
            //    File.Delete(fName);
            //}
            //File.WriteAllBytes(fName, bytes);

            //string strScript = "CloseModal('" + fName + "');";
            //ScriptManager.RegisterStartupScript(ucLabelAttached, this.GetType(), "CloseModal", strScript, true);

            Response.Buffer = true;

            Response.Charset = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = _contractHotelAttachments.AsEnumerable().Where(
                    data => data.AttachmentId == attachmentId).Select(data => data.FileType).FirstOrDefault().Trim(); ;

            Response.AddHeader("content-length", bytes.Length.ToString());
            Response.AddHeader("content-disposition", "attachment; filename=" + obj.Text);

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();

        }
        protected void uoGridViewHotelContractAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int i = Convert.ToInt32(e.CommandArgument);
            }
        }

        protected void uoGridViewHotelContractAttachment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int categoryID = e.RowIndex;
            _contractHotelAttachments.RemoveAt(categoryID);
            uoGridViewHotelContractAttachment.DataSource = _contractHotelAttachments;
            uoGridViewHotelContractAttachment.DataBind();
        }

        protected void uoGridViewHotelContractAttachment_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // loop all data rows
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        LinkButton button = control as LinkButton;
                        if (button != null && button.CommandName == "Delete")
                            button.OnClientClick = "return confirmDelete();";
                    }
                }
            }
        }
        #endregion

        #region METHODS
        
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 07/06/2012
        /// Description: add the new row to data table
        /// ===================================================
        /// Modified by:    Josephine Monteza
        /// Date Modified:  01/June/2016
        /// Description:    Change DataTable to List
        ///                 Change ViewState["CurrentTable"] to Session["ContractAdd_HotelDetails"]
        /// </summary>
        private void AddNewRow()
        {
            try
            {
                string sMsg = "";
                List<ContractHotelDetails> list = new List<ContractHotelDetails>();
                if (Session["ContractAdd_HotelDetails"] != null)
                {
                    list = (List<ContractHotelDetails>)Session["ContractAdd_HotelDetails"];
                }

                var fromList = from a in list
                               where GlobalCode.Field2DateTime(uoTxtFrom.Text) >= a.DateFrom &&
                                     GlobalCode.Field2DateTime(uoTxtFrom.Text) <= a.DateTo
                               select a;

                if (fromList.Count() > 0)
                {
                    sMsg = "Date From already included!";
                }

                var toList = from a in list
                             where GlobalCode.Field2DateTime(uoTxtTo.Text) >= a.DateFrom &&
                                   GlobalCode.Field2DateTime(uoTxtTo.Text) <= a.DateTo
                             select a;
              
                if (toList.Count() > 0)
                {
                    if (sMsg == "")
                    {
                        sMsg = "Date To already included!";
                    }
                    else
                    {
                        sMsg = "Date From and To already included!";
                    }
                }             

                if (sMsg == "")
                {
                    ContractHotelDetails item = new ContractHotelDetails();
                    int max = 0;

                    if (list.Count > 0)
                    {
                        max = list.Max(x => x.RowNumber);
                    }

                    item.RowNumber = max + 1;
                    item.DetailId = 0;
                    item.DetailId2 = 0;
                    item.DateFrom = GlobalCode.Field2DateTime(uoTxtFrom.Text);
                    item.DateTo = GlobalCode.Field2DateTime(uoTxtTo.Text);
                    item.Sun = GlobalCode.Field2Int(uoTxtSun.Text);
                    item.Mon = GlobalCode.Field2Int(uoMon.Text);
                    item.Tue = GlobalCode.Field2Int(uoTue.Text);
                    item.Wed = GlobalCode.Field2Int(uoWed.Text);
                    item.Thu = GlobalCode.Field2Int(uoThu.Text);

                    item.Fri = GlobalCode.Field2Int(uoFri.Text);
                    item.Sat = GlobalCode.Field2Int(uoSat.Text);

                    item.Sun2 = GlobalCode.Field2Int(uoTxtSun2.Text);
                    item.Mon2 = GlobalCode.Field2Int(uoMon2.Text);
                    item.Tue2 = GlobalCode.Field2Int(uoTue2.Text);
                    item.Wed2 = GlobalCode.Field2Int(uoWed2.Text);
                    item.Thu2 = GlobalCode.Field2Int(uoThu2.Text);
                    item.Fri2 = GlobalCode.Field2Int(uoFri2.Text);
                    item.Sat2 = GlobalCode.Field2Int(uoSat2.Text);
                    item.SingleRate = GlobalCode.Field2Decimal(uotextboxRoomRate.Text);
                    item.DoubleRate = GlobalCode.Field2Decimal(uotextboxRoomRate2.Text);
                    item.Tax = GlobalCode.Field2Decimal(uoRoomTax.Text);
                    item.TaxInclusive = GlobalCode.Field2Bool(uoChkRoomTaxInc.Checked);
                    //item.Currency = uoDropDownlistCurrency.SelectedItem.Text;
                    //item.CurrencyId = GlobalCode.Field2Int(uoDropDownlistCurrency.SelectedValue);
                    list.Add(item);

                    list = list.OrderBy(a => a.DateFrom).ToList();
                    Session["ContractAdd_HotelDetails"] = list;
                    
                    
                    uoListView.DataSource = list;
                    uoListView.DataBind();
                 
                }
                else
                {
                    AlertMessage(sMsg, true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modified By:    Josephine Monteza
        /// Date Modified:   02/June/2016
        /// Description:    Changed ViewState["CurrentTable"] to Session["ContractAdd_HotelDetails"]
        /// </summary>
        private void UpdateRowToGrid()
        {
            try
            {
                string sMsg = "";
                List<ContractHotelDetails> listContractHotelDetails = new List<ContractHotelDetails>();
                ContractHotelDetails listToAdd = new ContractHotelDetails();

                if (Session["ContractAdd_HotelDetails"] != null)
                {
                    listContractHotelDetails = (List<ContractHotelDetails>)Session["ContractAdd_HotelDetails"];
                }

                if (listContractHotelDetails.Count > 0)
                {
                    var fromList = from a in listContractHotelDetails
                                   where GlobalCode.Field2DateTime(uoTxtFrom.Text) >= a.DateFrom &&
                                         GlobalCode.Field2DateTime(uoTxtFrom.Text) <= a.DateTo &&
                                         a.RowNumber != GlobalCode.Field2Int(uoHiddenFieldRowNo.Value)
                                   select a;

                    if (fromList.Count() > 0)
                    {
                        sMsg = "Date From already included!";
                    }

                    var toList = from a in listContractHotelDetails
                                 where GlobalCode.Field2DateTime(uoTxtTo.Text) >= a.DateFrom &&
                                       GlobalCode.Field2DateTime(uoTxtTo.Text) <= a.DateTo &&
                                       a.RowNumber != GlobalCode.Field2Int(uoHiddenFieldRowNo.Value)
                                 select a;


                    if (toList.Count() > 0)
                    {
                        if (sMsg == "")
                        {
                            sMsg = "Date To already included!";
                        }
                        else
                        {
                            sMsg = "Date From and To already included!";
                        }
                    }
                   

                    if (sMsg == "")
                    {
                        listToAdd.RowNumber = GlobalCode.Field2Int(uoHiddenFieldRowNo.Value);

                        listToAdd.DateFrom = GlobalCode.Field2DateTime(uoTxtFrom.Text);
                        listToAdd.DateTo = GlobalCode.Field2DateTime(uoTxtTo.Text);

                        listToAdd.Sun = GlobalCode.Field2Int(uoTxtSun.Text);
                        listToAdd.Mon = GlobalCode.Field2Int(uoMon.Text);
                        listToAdd.Tue = GlobalCode.Field2Int(uoTue.Text);
                        listToAdd.Wed = GlobalCode.Field2Int(uoWed.Text);
                        listToAdd.Thu = GlobalCode.Field2Int(uoThu.Text);
                        listToAdd.Fri = GlobalCode.Field2Int(uoFri.Text);
                        listToAdd.Sat = GlobalCode.Field2Int(uoSat.Text);


                        listToAdd.Sun2 = GlobalCode.Field2Int(uoTxtSun2.Text);
                        listToAdd.Mon2 = GlobalCode.Field2Int(uoMon2.Text);
                        listToAdd.Tue2 = GlobalCode.Field2Int(uoTue2.Text);
                        listToAdd.Wed2 = GlobalCode.Field2Int(uoWed2.Text);
                        listToAdd.Thu2 = GlobalCode.Field2Int(uoThu2.Text);
                        listToAdd.Fri2 = GlobalCode.Field2Int(uoFri2.Text);
                        listToAdd.Sat2 = GlobalCode.Field2Int(uoSat2.Text);

                        listToAdd.SingleRate = GlobalCode.Field2Decimal(uotextboxRoomRate.Text);
                        listToAdd.DoubleRate = GlobalCode.Field2Decimal(uotextboxRoomRate2.Text);

                        listToAdd.Tax = GlobalCode.Field2Decimal(uoRoomTax.Text);
                        listToAdd.TaxInclusive = GlobalCode.Field2Bool(uoChkRoomTaxInc.Checked);
                        //listToAdd.Currency = uoDropDownlistCurrency.SelectedItem.Text;
                        //listToAdd.CurrencyId = GlobalCode.Field2Int(uoDropDownlistCurrency.SelectedValue);

                        listContractHotelDetails.RemoveAll(a => a.RowNumber == GlobalCode.Field2Int(uoHiddenFieldRowNo.Value));

                        listContractHotelDetails.Insert(listContractHotelDetails.Count, listToAdd);
                        listContractHotelDetails = listContractHotelDetails.OrderBy(a => a.DateFrom).ToList();

                        uoListView.DataSource = listContractHotelDetails;
                        uoListView.DataBind();

                        Session["ContractAdd_HotelDetails"] = listContractHotelDetails;
                        uoHiddenFieldRowNo.Value = "0";
                    }
                    else
                    {
                        AlertMessage(sMsg, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Created By:     Josephine Monteza
        /// Date Created:   02/June/2016
        /// Description:    Delete/Update Row in Hotel Details
        /// </summary>
        private void DeleteEditGrid(string sCommand, string sRowNumber)
        {
            try
            {
                List<ContractHotelDetails> listContractHotelDetails = new List<ContractHotelDetails>();
                if (Session["ContractAdd_HotelDetails"] != null)
                {
                    listContractHotelDetails = (List<ContractHotelDetails>)Session["ContractAdd_HotelDetails"];
                }
                if (sCommand == "Delete")
                {
                    listContractHotelDetails.RemoveAll(a => a.RowNumber == GlobalCode.Field2Int(sRowNumber));
                    listContractHotelDetails = listContractHotelDetails.OrderBy(a => a.DateFrom).ToList();
                    Session["ContractAdd_HotelDetails"] = listContractHotelDetails;
                    uoListView.DataSource = listContractHotelDetails;
                    uoListView.DataBind();
                }
                else if (sCommand == "Edit")
                {
                    var selectedRow = listContractHotelDetails.Where(a => a.RowNumber == GlobalCode.Field2Int(sRowNumber)).ToList();

                    //DataRow[] drCurrentRow = dtCurrentTable.Select("RowNumber = " + e.CommandArgument.ToString().Remove(0, 3));

                    uoTxtFrom.Text = selectedRow[0].DateFrom.ToString();//GlobalCode.Field2DateTime(drCurrentRow[0]["DateFrom"]).ToString();
                    uoTxtTo.Text = selectedRow[0].DateTo.ToString();

                    uoTxtSun.Text = selectedRow[0].Sun.ToString();
                    uoMon.Text = selectedRow[0].Mon.ToString();
                    uoTue.Text = selectedRow[0].Tue.ToString();
                    uoWed.Text = selectedRow[0].Wed.ToString();
                    uoThu.Text = selectedRow[0].Thu.ToString();
                    uoFri.Text = selectedRow[0].Fri.ToString();
                    uoSat.Text = selectedRow[0].Sat.ToString();

                    uoTxtSun2.Text = selectedRow[0].Sun2.ToString();
                    uoMon2.Text = selectedRow[0].Mon2.ToString();
                    uoTue2.Text = selectedRow[0].Tue2.ToString();
                    uoWed2.Text = selectedRow[0].Wed2.ToString();
                    uoThu2.Text = selectedRow[0].Thu2.ToString();
                    uoFri2.Text = selectedRow[0].Fri2.ToString();
                    uoSat2.Text = selectedRow[0].Sat2.ToString();

                    uotextboxRoomRate.Text = selectedRow[0].SingleRate.ToString("N2");
                    uotextboxRoomRate2.Text = selectedRow[0].DoubleRate.ToString("N2");
                    uoRoomTax.Text = selectedRow[0].Tax.ToString();

                    uoChkRoomTaxInc.Checked = selectedRow[0].TaxInclusive;
                    //uoDropDownlistCurrency.SelectedValue = selectedRow[0].CurrencyId.ToString();
                    uoHiddenFieldRowNo.Value = sRowNumber;

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 06/06/2012
        /// Description: save contract header
        /// </summary>
        /// <returns></returns>
        protected int SaveContract(out object sqlTransaction)   
        {
            Int32 pID = 0;

            if (uoTextBoxMealRate.Text == "")
            {
                uoTextBoxMealRate.Text = "0";
            }
            if (uoTextBoxMealRateTax.Text == "")
            {
                uoTextBoxMealRateTax.Text = "0";
            }
            Byte[] imageBytes = null;
            string Getdate="";

           
            pID = AddSaveHotelContract(
                
                GlobalCode.Field2Int(uoDropDownListVendorMain.SelectedValue),
                GlobalCode.Field2Int(uoDropDownListVendor.SelectedValue),
                uotextboxContractTitle.Text,
                uotextboxRemarks.Text,
                uotextboxStartDate.Text,
                uotextboxEndDate.Text,
                uotextboxRCCLRep.Text,
                uotextboxVendorRep.Text,
                uoTextBoxRCCLDateAccepted.Text,
                uoTextBoxVendorDateAccepted.Text,

                
                MUser.GetUserName(),
                uoTextBoxMealRate.Text,
                uoTextBoxMealRateTax.Text,
                uoCheckBoxMealRateTaxInclusive.Checked,
                uoCheckBoxBreakfast.Checked,
                uoCheckBoxLunch.Checked,
                uoCheckBoxDinner.Checked,
                uoCheckBoxLunchDinner.Checked,
                uoCheckBoxShuttle.Checked,

                uoHiddenFieldFileName.Value,
                uoHiddenFieldFileType.Value,
                //System.Text.Encoding.ASCII.GetBytes(uoHiddenFieldFileData.Value),
                imageBytes,
                Getdate,
                uoHiddenFieldContractID.Value,
                out sqlTransaction,
                uotextboxVendorRepContactNo.Text.ToString(),
                uotextboxRCCLRepContactNo.Text.ToString(),
                uotextboxVendorRepEmailAdd.Text.ToString(),
                uotextboxRCCLRepEmailAdd.Text.ToString(),
                GlobalCode.Field2Int(uoTextBoxCancellationHours.Text),
                uoTextBoxCutoffTime.Text, cboHotelTimeZone.SelectedValue.ToString(),
                GlobalCode.Field2Int(uoDropDownlistCurrency.SelectedValue),
                GlobalCode.Field2Float(uoTextBoxDefaultDBLRate.Text),
                GlobalCode.Field2Float(uoTextBoxDefaultSGLRate.Text)
                );


            return pID;
        }

        /// <summary>
        /// Modified By: Charlene Remotigue
        /// Date Modified: 06/06/2012
        /// Description: save contract details (by bulk)
        /// ==========================================================
        /// Modified By:    Josephine Monteza
        /// Date Modified:  02/June/2016
        /// Description:    Changed ViewState["CurrentTable"] to Session["ContractAdd_HotelDetails"]
        ///                 Used Class instead of DataTable
        /// </summary>
        /// <param name="pID"></param>
        private void SaveGridItems(Int32 pID)
        {
            DataTable dt = null;
            try
            {
                Int32 ContractDetailID = 0;
                //string DeletedItems = GlobalCode.Field2String(ViewState["DeletedItems"]);

                List<ContractHotelDetails> listContractHotelDetails = new List<ContractHotelDetails>();
                if (Session["ContractAdd_HotelDetails"] != null)
                {
                    listContractHotelDetails = (List<ContractHotelDetails>)Session["ContractAdd_HotelDetails"];

                    //dt = new DataTable();
                    //dt = (DataTable)ViewState["CurrentTable"];
                    dt = getDataTable(listContractHotelDetails);
                    
                    //string RoomRate = uotextboxRoomRate.Text;
                    //string RoomRate2 = uotextboxRoomRate2.Text;
                    //int Currency = GlobalCode.Field2Int(uoDropDownlistCurrency.SelectedValue);
                    //bool TaxInclusive = uoChkRoomTaxInc.Checked;
                    //string Tax = uoRoomTax.Text;

                    if (dt.Rows.Count > 0)
                    {
                        ContractDetailID = ContractBLL.AddSaveHotelDetailContract(pID, "", MUser.GetUserName(), dt);
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

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select all currency  
        /// </summary>
        private void CurrencyLoad()
        {
            DataTable dt = null;
            try
            {
                dt = ContractBLL.CurrencyLoad();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownlistCurrency.DataSource = dt;
                    uoDropDownlistCurrency.DataTextField = "colCurrencyNameVarchar"; // "colCurrencyCodeVarchar";
                    uoDropDownlistCurrency.DataValueField = "colCurrencyIDInt";
                    uoDropDownlistCurrency.DataBind();
                }
                else
                {
                    uoDropDownlistCurrency.DataBind();
                }
                uoDropDownlistCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select country list by vendor ID   
        /// </summary>
        private void CountryListByVendorID()
        {
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListVendor.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVendor.SelectedValue);
                }

                dt = HotelBLL.CountryByVendorBranchID(VendorID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownCountry.DataSource = dt;
                    uoDropDownCountry.DataTextField = "colCountryNameVarchar";
                    uoDropDownCountry.DataValueField = "colCountryIDInt";
                    uoDropDownCountry.DataBind();
                    uoDropDownCountry.SelectedValue = dt.Rows[0]["colCountryIDInt"].ToString();
                    uoDropDownCountry.Enabled = false;
                }
                else
                {
                    uoDropDownCountry.DataBind();
                    uoDropDownCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));
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

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select city list by vendor ID   
        /// </summary>

        private void CityListByVendorID()
        {
            DataTable dt = null;
            try
            {
                int CountryID = 0;
                if (uoDropDownCountry.SelectedValue != "0")
                {
                    CountryID = Convert.ToInt32(uoDropDownCountry.SelectedValue);
                }
                int VendorID = 0;
                if (uoDropDownListVendor.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVendor.SelectedValue);
                }

                dt = HotelBLL.CityByVendorBranchIDCountryID(VendorID, CountryID);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataSource = dt;
                    uoDropDownListCity.DataTextField = "colCityNameVarchar";
                    uoDropDownListCity.DataValueField = "colCityIDInt";
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.SelectedValue = dt.Rows[0]["colCityIDInt"].ToString();
                    uoDropDownListCity.Enabled = false;
                }
                else
                {
                    uoDropDownListCity.DataBind();
                    uoDropDownListCity.Items.Insert(0, new ListItem("--Select City--", "0"));
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

        /// <summary>
        /// Date Created: 19/08/2011
        /// Created By: Ryan Bautista
        /// (description) Add / save hotel contract          
        /// --------------------------------------
        /// Date Modified:  14/June/2016
        /// Modified By:    Josephine Monteza
        /// Description:    Added parameter BranchID, removed parameter CountryID and CityID
        /// </summary>
        /// 
        private Int32 AddSaveHotelContract(Int32 VendorID, Int32 BranchID, string vContract, string Remarks, string dtStart, string dtEnd, string RCCLRep,
                                            string vRep, string dtRCCLAccepted, string dtVendorAccepted, string Username,
                                            string MealRate, string MealRateTax, bool TaxInclusive, bool Breakfast, bool Lunch, bool Dinner,
                                            bool LunchDinner, bool Shuttle, string Filename, string FileType, byte[] FileData, string DateUploaded,
                                            string ContractID, out object sqlTransaction,
                                            string VendorRepContactNo, string RCCLRepContactNo,
                                            string endorRepEmailAdd, string RCCLRepEmailAdd,
                                            int iCancelationTerms, string sCutoffTime, string HotelTimeZoneID,
                                            int CurrencyIDInt, float RoomRateDbl, float RoomRateSgl)
        {

            return BLL.ContractBLL.AddSaveHotelContract(VendorID, BranchID, vContract, Remarks, dtStart, dtEnd, RCCLRep, vRep, dtRCCLAccepted, dtVendorAccepted,
                                                    Username, MealRate, MealRateTax, TaxInclusive, Breakfast, Lunch, Dinner, LunchDinner,
                                                    Shuttle, Filename, FileType, FileData, DateUploaded, ContractID, out sqlTransaction,
                                                    VendorRepContactNo, RCCLRepContactNo,
                                                    endorRepEmailAdd, RCCLRepEmailAdd,
                                                    iCancelationTerms, sCutoffTime, HotelTimeZoneID, 
                                                    CurrencyIDInt, RoomRateDbl, RoomRateSgl);
        }

        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Format data to uppercase        
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }

        ///// <summary>
        ///// Date Created: 23/08/2011
        ///// Created By: Ryan Bautista
        ///// (description) Select all hotel details by region 
        ///// </summary>
        private void HotelLoad()
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelVendorGetDetailsByRegion(MUser.GetUserName());
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendorMain.DataSource = dt;
                    uoDropDownListVendorMain.DataTextField = "colVendorNameVarchar";
                    uoDropDownListVendorMain.DataValueField = "colVendorIdInt";
                    uoDropDownListVendorMain.DataBind();
                    if (Request.QueryString["hvID"] != null)
                    {
                        uoDropDownListVendorMain.SelectedValue = Request.QueryString["hvID"];
                        uoDropDownListVendorMain.Enabled = false;
                    }
                }
                else
                {
                    uoDropDownListVendorMain.DataBind();
                }
                uoDropDownListVendorMain.Items.Insert(0, new ListItem("--Select Hotel--", "0"));
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
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Select all hotel branch details  
        /// </summary>
        private void HotelBranchLoad()
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelBranchList();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendor.DataSource = dt;
                    uoDropDownListVendor.DataTextField = "colVendorBranchNameVarchar";
                    uoDropDownListVendor.DataValueField = "colBranchIDInt";
                    uoDropDownListVendor.DataBind();

                }
                else
                {
                    uoDropDownListVendor.DataBind();
                }
                uoDropDownListVendor.Items.Insert(0, new ListItem("--Select Hotel Branch--", "0"));
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
        /// Date Created:   23/08/2011
        /// Created By:     Ryan Bautista
        /// (description)   Select all hotel branch details  
        /// ----------------------------------------------
        /// Date Modified:  26/04/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add branch name as default value of uotextboxContractTitle
        /// </summary>
        private void HotelBranchLoadByVendorID()
        {
            DataTable dt = null;
            try
            {
                int VendorID = 0;
                if (uoDropDownListVendorMain.SelectedValue != "0")
                {
                    VendorID = Convert.ToInt32(uoDropDownListVendorMain.SelectedValue);
                }
                //dt = HotelBLL.HotelBranchList();
                dt = HotelBLL.HotelBranchListByVendorID(VendorID, MUser.GetUserName());
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendor.DataSource = dt;
                    uoDropDownListVendor.DataTextField = "colVendorBranchNameVarchar";
                    uoDropDownListVendor.DataValueField = "colBranchIDInt";
                    uoDropDownListVendor.DataBind();
                    if (Request.QueryString["vmId"] != null)
                    {
                        uoDropDownListVendor.SelectedValue = Request.QueryString["vmId"];
                        uoDropDownListVendor.Enabled = false;
                    }
                    else
                    {
                        uoDropDownListVendor.SelectedValue = dt.Rows[0]["colBranchIDInt"].ToString();
                    }
                    if (uotextboxContractTitle.Text == "")
                    {
                        uotextboxContractTitle.Text = uoDropDownListVendor.SelectedItem.Text;
                    }
                }
                else
                {
                    uoDropDownListVendor.DataBind();
                    uoDropDownListVendor.Items.Insert(0, new ListItem("--Select Hotel Branch--", "0"));
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

        /// <summary>
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Select contract by branch
        /// -----------------------------------------
        /// Modified By: Charlene Remotigue
        /// Date Modified: 07/06/2012
        /// Description: add getting of contract details on separate datatable
        /// -----------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  13/06/2012
        /// Description:    add validation if colContractAttachmentVarbinary is NULL
        /// </summary>
        private void ContractLoadByBranch()
        {
            DataTable dt = null;
            DataTable dt2 = null;
            try
            {
                //dt = HotelBLL.HotelVendorGetDetailsByRegion(MUser.GetUserName());
                if (Request.QueryString["vmId"] != "" && Request.QueryString["cID"] != null)
                {
                    dt = BLL.ContractBLL.GetVendorHotelContract(Request.QueryString["cID"], Request.QueryString["vmId"], out dt2);
                    if (dt.Rows.Count > 0)
                    {
                        //uoHiddenFieldBranchID.Value = GlobalCode.Field2Int(Request.QueryString["vmId"]).ToString();
                        uoHiddenFieldContractID.Value = dt.Rows[0]["colContractIdInt"].ToString();
                        uotextboxContractTitle.Text = dt.Rows[0]["colContractNameVarchar"].ToString();
                        uotextboxRemarks.Text = dt.Rows[0]["colRemarksVarchar"].ToString();
                        uotextboxRCCLRep.Text = dt.Rows[0]["colRCCLPersonnel"].ToString();
                        uotextboxVendorRep.Text = dt.Rows[0]["colVendorPersonnel"].ToString();
                        uoCheckBoxShuttle.Checked = Convert.ToBoolean(dt.Rows[0]["colWithShuttleBit"].ToString());
                        uoCheckBoxBreakfast.Checked = Convert.ToBoolean(dt.Rows[0]["colBreakfastBit"].ToString());
                        uoCheckBoxLunch.Checked = Convert.ToBoolean(dt.Rows[0]["colLunchBit"].ToString());
                        uoCheckBoxDinner.Checked = Convert.ToBoolean(dt.Rows[0]["colDinnertBit"].ToString());
                        uoCheckBoxLunchDinner.Checked = Convert.ToBoolean(dt.Rows[0]["colLunchOrDinnerBit"].ToString());

                        string dtStart = (dt.Rows[0]["colContractDateStartedDate"].ToString().Length > 0)
                            ? dt.Rows[0]["colContractDateStartedDate"].ToString() : "";
                            
                        string dtEnd = (dt.Rows[0]["colContractDateEndDate"].ToString().Length > 0)
                           ? dt.Rows[0]["colContractDateEndDate"].ToString() : "";
                          
                        uotextboxStartDate.Text = Convert.ToDateTime(dtStart).ToShortDateString();
                        uotextboxEndDate.Text = Convert.ToDateTime(dtEnd).ToShortDateString();

                        string dtRCCLAccepted = (dt.Rows[0]["colRCCLAcceptedDate"].ToString().Length > 0)
                            ? Convert.ToDateTime(dt.Rows[0]["colRCCLAcceptedDate"].ToString()).ToShortDateString()
                            : "";
                        string dtVendorAccepted = (dt.Rows[0]["colVendorAcceptedDate"].ToString().Length > 0)
                           ? Convert.ToDateTime(dt.Rows[0]["colVendorAcceptedDate"].ToString()).ToShortDateString()
                           : "";

                        uoTextBoxRCCLDateAccepted.Text = dtRCCLAccepted;
                        uoTextBoxVendorDateAccepted.Text = dtVendorAccepted;

                        uoTextBoxMealRate.Text = GlobalCode.Field2Double(dt.Rows[0]["colMealRateMoney"]).ToString("0,000.00");//dt.Rows[0]["colMealRateMoney"].ToString();
                        uoTextBoxMealRateTax.Text = GlobalCode.Field2Double(dt.Rows[0]["colMealRateTaxDecimal"]).ToString("000.00");//dt.Rows[0]["colMealRateTaxDecimal"].ToString();
                        uoCheckBoxMealRateTaxInclusive.Checked = Convert.ToBoolean(dt.Rows[0]["colMealRateTaxInclusiveBit"].ToString());

                        uotextboxVendorRepEmailAdd.Text = dt.Rows[0]["colVendorRepEmailAddVarchar"].ToString();
                        uotextboxRCCLRepEmailAdd.Text = dt.Rows[0]["colRCCLRepEmailAddVarchar"].ToString();
                        uotextboxVendorRepContactNo.Text = dt.Rows[0]["colVendorRepContactNoVarchar"].ToString();
                        uotextboxRCCLRepContactNo.Text = dt.Rows[0]["colRCCLRepContactNoVarchar"].ToString();
                       
                        if (GlobalCode.Field2String(dt.Rows[0]["colContractAttachmentVarbinary"]) != "")
                        {
                            ViewState["imageByte"] = (byte[])dt.Rows[0]["colContractAttachmentVarbinary"];
                        }
                        else
                        {
                            ViewState["imageByte"] = null;
                        }

                        if (GlobalCode.Field2Int(dt.Rows[0]["colCancellationTermsInt"]) > 0)
                        {
                            uoTextBoxCancellationHours.Text = GlobalCode.Field2Int(dt.Rows[0]["colCancellationTermsInt"]).ToString();
                        }
                        else
                        {
                            uoTextBoxCancellationHours.Text = "";
                        }

                        if (GlobalCode.Field2String(dt.Rows[0]["colCutOffTime"]) != "")
                        {
                            uoTextBoxCutoffTime.Text  = string.Format("{0:HH:mm}", GlobalCode.Field2DateTime(dt.Rows[0]["colCutOffTime"]));
                        }
                        else
                        {
                            uoTextBoxCutoffTime.Text = "";
                        }

                        cboHotelTimeZone.SelectedIndex = GlobalCode.GetselectedIndexValue(cboHotelTimeZone, dt.Rows[0]["colHotelTimeZoneIDVarchar"].ToString());


                        ViewState["Add"] = true;

                        string sCurrency = GlobalCode.Field2Int(dt.Rows[0]["colCurrencyIDInt"]).ToString();
                        if (uoDropDownlistCurrency.Items.FindByValue(sCurrency) != null)
                        {
                            uoDropDownlistCurrency.SelectedValue = sCurrency;
                            uoLabelCurrency.Text = uoDropDownlistCurrency.SelectedItem.Text;
                        }

                        uoTextBoxDefaultDBLRate.Text = GlobalCode.Field2Decimal(dt.Rows[0]["colRoomAmountDblFloat"]).ToString("N2");
                        uoTextBoxDefaultSGLRate.Text = GlobalCode.Field2Decimal(dt.Rows[0]["colRoomAmountSglFloat"]).ToString("N2");
                    }
                }
                if (Request.QueryString["cID"] != null)
                {
                    //ViewState["CurrentTable"] = dt2;
                    _contractHotelAttachments = GetHotelAttachments(Convert.ToInt32(Request.QueryString["cID"].ToString()));
                    uoGridViewHotelContractAttachment.DataSource = _contractHotelAttachments;
                    uoGridViewHotelContractAttachment.DataBind();

                    List<ContractHotelDetails>  listContractHotelDetails = new List<ContractHotelDetails>();
                    if (Session["ContractAdd_HotelDetails"] != null)
                    {
                        listContractHotelDetails = (List<ContractHotelDetails>)Session["ContractAdd_HotelDetails"];
                    }
                    uoListView.DataSource = listContractHotelDetails;
                    uoListView.DataBind();
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
                if (dt2 != null)
                {
                    dt2.Dispose();
                }
            }
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelContractLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"] != "" && Request.QueryString["cID"] != null)
            {
                strLogDescription = "Ammend linkbutton for hotel contract editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for hotel contract editor clicked.";
            }

            strFunction = "HotelContractLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, MUser.GetUserName());
        }
       
       
       
        /// <summary>
        /// Created By:     Jefferson Bermundo
        /// Date Created:   07/31/2012
        /// Description:    Saving for Multiple Attachments
        /// <para>pId : Contract Id</para>
        /// </summary>
        protected void SaveHotelAttachments(int pId, object sqlTransaction)
        {
            int i = 0;
            foreach (var iList in _contractHotelAttachments)
            {
                i++;
                ContractBLL.AddSaveHotelContractAttachments(pId, iList.FileName, iList.FileType, iList.uploadedFile,
                    iList.UploadedDate, sqlTransaction, i == _contractHotelAttachments.Count ? true: false);
            }
            if (i == 0)
            {
                ContractBLL.AddSaveHotelContractAttachments(0, "", "", null, DateTime.Now, sqlTransaction, true);
            }
        }

        protected List<ContractHotelAttachment> GetHotelAttachments(int contractId)
        {
           return ContractBLL.GetHotelContractAttachment(contractId);
        }

        /// <summary>
        /// Created By:     Jefferson Bermundo 
        /// Created Date:   24/07/2012
        /// Description:    Set ViewState to session temporary.
        ///                 Enable to handle more than 4MB files
        ///                 during upload.
        /// </summary>
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                //return base.PageStatePersister;
                return new SessionPageStatePersister(this);
            }
        }
                    
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   02/Jun/2016
        /// Description:    convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   02/Jun/2016
        /// Description:    get item type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Type GetCoreType(Type t)
        {
            if (t != null)
            {
                return t;
            }
            else
            {
                return t;
            }
        }
        /// Date Created:  31/May/2016
        /// Created By:    Josephine Monteza
        /// (description)  Popup message
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
        /// Date Created:  24/Jun/2016
        /// Created By:    Josephine Monteza
        /// (description)  Validate date if within the contract period
        /// </summary>
        /// <returns></returns>
        private bool IsDateWithinContract(DateTime dtToValidate)
        {
            bool bReturn = false;
            DateTime dtContractFrom = GlobalCode.Field2DateTime(uotextboxStartDate);
            DateTime dtContractTo = GlobalCode.Field2DateTime(uotextboxEndDate);

            if (dtToValidate >= dtContractFrom  &&
                dtToValidate <= dtContractTo)
            {
                bReturn = true;
            }

            return bReturn;
        }
        #endregion
    }
}

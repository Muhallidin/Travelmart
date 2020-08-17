using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

using System.Threading;

using System.IO;
using System.ComponentModel;
using System.Configuration;

namespace TRAVELMART.Maintenance
{
    public partial class HotelMaintenanceBranch : System.Web.UI.Page
    {
        #region Delegate
        private delegate void ProcessPageOnLoaded();

        #endregion

        #region EVENT
        /// Date Modified:  15/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {                
                if (Request.QueryString["vmId"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                uoHiddenFieldVendorIdInt.Value = Request.QueryString["vmId"];

                HotelVendorBranchLogAuditTrail();

                //GlobalCode.Field2String(Session["Region"]), TravelMartVariable.TreadUserID
                VendorLoad();
                uoHiddenFieldNew.Value = "1";                
                
                ProcessVendorBranchInfoLoad();
                //Thread.CurrentThread.Name = "TreadMain"; 
                //Thread TreadMain = new Thread(new ThreadStart(ProcessVendorBranchInfoLoad));
                //TreadMain.Start();

                //HotelGetRoomTypeForCapacity();
                //CurrencyLoad();

            }
            uoHiddenFieldNew.Value = "0";
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
                SavePhoto("shuttle");
            }
            else
            {
                AlertMessage("No Shuttle image file specified!", true);
            }
        }

        /// <summary>
        /// =================================
        /// Date Created:   29/11/2011
        /// Created By:     Muhallidin G Wali
        /// Description:                             
        /// =================================
        /// Date Modified:   14/12/2011
        /// Modified By:     Josephine Gad
        /// Description:     Disable threading for now    
        ///                  Delete MealName function
        ///                  Add BindDropdownStripeInRank
        /// =================================
        /// </summary>
        #region > Thread Call <
        void ProcessVendorBranchInfoLoad()
        {
            try
            {
                vendorCountryLoad();
                vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                //AirportLoad(GlobalCode.Field2String(Session["Region"]), TravelMartVariable.TreadUserID);

                //MealName();
                //ViewState["Add"] = "False";
                //ViewState["AddRoom"] = "False";
                HotelGetDepartment(true);
                HotelGetRoomType(true);
                BindDropdownStripeInRank(true);
                
                VendorBranchInfoLoad();
                BindDropDownRank(true);
                //Thread.CurrentThread.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region uoDropDownListVendor_SelectedIndexChanged
        protected void uoDropDownListVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion


        protected void uoDropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uoDropDownListCountry.SelectedIndex > 1)
            {
                vendorCityLoad(Convert.ToInt32(uoDropDownListCountry.SelectedValue));
                ChangeToUpperCase(uoDropDownListCity);
            }
        }

        protected void uoDropDownListCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Date Created:   29/07/2011
        /// Created By:     Ryan Bautista
        /// Description:    Save vendor branch                          
        /// -----------------------------------
        /// Date Modified:   17/11/2011
        /// Modified By:     Josephine Gad
        /// Description:     Add Error Logs
        /// -----------------------------------
        /// Date Modified:   21/12/2011
        /// Modified By:     Josephine Gad
        /// Description:     Add saving for HotelBranchDeptStripe
        /// -----------------------------------
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// Description:     Change HotelBranchVoucherList.VoucherList to Session["VoucherList"]
        /// Description:     Change HotelBranchRoomType.RoomTypeList to Session["RoomTypeList"]
        /// -----------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Jefferson Bermundo
        /// (description)   Add saving for colFaxNoVarchar and colWebsiteVarchar
        /// </summary>
        protected void uoButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strLogDescription;
                string strFunction;

                decimal Stripes = 0;
                decimal Amount = 0;
                Int16 DayNo = 1;
                Int32 BranchIDint = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);

                string RoomType;
                Int16 RoomTypID;

                Int32 VoucherID = 0;
                Int32 RoomTypeID = 0;

                //if (HotelBranchVoucherList.VoucherList == null)
                if (Session["VoucherList"] == null)
                {
                    List<HotelBranchVoucherList> voucherList = new List<HotelBranchVoucherList>();
                    HotelBranchVoucherList voucherItem = new HotelBranchVoucherList();
                    for (int i = 0; i <= uoGridViewVoucher.Rows.Count; i++)
                    {
                        Stripes = GlobalCode.Field2Decimal(uoGridViewVoucher.Rows[i].Cells[1].Text);
                        Amount = GlobalCode.Field2Decimal(uoGridViewVoucher.Rows[i].Cells[3].Text);
                        DayNo = GlobalCode.Field2TinyInt(uoGridViewVoucher.Rows[i].Cells[2].Text);

                        voucherItem.Stripe = Stripes;
                        voucherItem.Amount = Amount;
                        voucherItem.DayNo = DayNo;
                        voucherItem.BranchID = BranchIDint;

                        voucherList.Add(voucherItem);
                    }
                    //HotelBranchVoucherList.VoucherList = voucherList;
                    Session["VoucherList"] = voucherList;
                }
                //if (HotelBranchRoomType.RoomTypeList == null)
                if (Session["RoomTypeList"] == null)
                {
                    List<HotelBranchRoomType> roomList = new List<HotelBranchRoomType>();
                    HotelBranchRoomType roomItem = new HotelBranchRoomType();
                    for (int i = 0; i <= uoGridViewRoom.Rows.Count; i++)
                    {
                        RoomTypID = GlobalCode.Field2TinyInt(uoGridViewRoom.Rows[i].Cells[0].Text);
                        RoomType = uoGridViewRoom.Rows[i].Cells[1].Text;

                        roomItem.colRoomTypeID = RoomTypID;
                        roomItem.colRoomNameVarchar = RoomType;

                        roomList.Add(roomItem);
                    }
                    //HotelBranchRoomType.RoomTypeList = roomList;
                    Session["RoomTypeList"] = roomList;
                }
                Int32 BranchID = VendorMaintenanceBLL.vendorBranchMaintenanceInsert(uoTextBoxVendorName.Text.ToUpper(),
                uoTextBoxVendorAddress.Text, Convert.ToInt32(uoDropDownListCity.SelectedValue), Convert.ToInt32(uoDropDownListCountry.SelectedValue),
                uoTextBoxContactNo.Text, GlobalCode.Field2String(Session["UserName"]), uoDropDownListVendor.SelectedValue,
                uoTextBoxPerson.Text, uoCheckBoxIsFranchise.Checked, uoHiddenFieldVendorIdInt.Value,
                uoCheckBoxRating.Checked, uoCheckBoxOfficer.Checked, uoDropDownListIMSVendor.SelectedValue,
                uoTextBoxEmailTo.Text, uoTextBoxEmailCc.Text, uoCheckBoxON.Checked, uoCheckBoxOff.Checked
                ,uoTextBoxFaxNo.Text, uoTextBoxWebsite.Text, uoTextBoxInstructionOn.Text, uoTextBoxInstructionOff.Text); //Convert.ToInt32(uoDropDownListAirport.SelectedValue)

                BranchIDint = BranchID;
                uoHiddenFieldBranchID.Value = BranchID.ToString();

                decimal lStripes = 0;
                decimal lAmount = 0;
                int lDayNo = 1;

                //Save Voucher                 
                List<HotelBranchVoucherList> listVoucher = (List<HotelBranchVoucherList>)Session["VoucherList"];//HotelBranchVoucherList.VoucherList;
                if (listVoucher != null)
                {
                    if (listVoucher.Count > 0)
                    {
                        for (int i = 0; i < listVoucher.Count; i++) //Save the values one by one
                        {
                            lStripes = listVoucher[i].Stripe;
                            lAmount = listVoucher[i].Amount;
                            lDayNo = listVoucher[i].DayNo;
                            VoucherID = VendorMaintenanceBLL.InsertHotelBranchVoucher(GlobalCode.Field2String(Session["UserName"]), Convert.ToString(BranchIDint), lStripes, lAmount, lDayNo);

                            if (VoucherID != 0)
                            {
                                //Insert log audit trail (Gabriel Oquialda - 19/03/2012)
                                strLogDescription = "Hotel vendor branch voucher added.";
                                strFunction = "uoButtonSave_Click";

                                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                                BLL.AuditTrailBLL.InsertLogAuditTrail(VoucherID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                            }
                        }                        
                    }
                }

                string lRoom;

                //Save Room Types
                List<HotelBranchRoomType> listRoomType = (List<HotelBranchRoomType>)Session["RoomTypeList"];//HotelBranchRoomType.RoomTypeList;
                if (listRoomType != null)
                {
                    if (listRoomType.Count > 0)
                    {
                        for (int i = 0; i < listRoomType.Count; i++) //Save the values one by one
                        {
                            lRoom = listRoomType[i].colRoomNameVarchar;
                            RoomTypeID = VendorMaintenanceBLL.InsertHotelBranchRoomType(GlobalCode.Field2String(Session["UserName"]), BranchIDint, lRoom);

                            if(RoomTypeID != 0)
                            {
                                //Insert log audit trail (Gabriel Oquialda - 19/03/2012)
                                strLogDescription = "Hotel vendor branch room type added.";
                                strFunction = "uoButtonSave_Click";

                                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                                BLL.AuditTrailBLL.InsertLogAuditTrail(RoomTypeID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                            }
                        }
                    }
                }

                //Save Dept Stripes
                foreach (GridViewRow r in uoGridViewDepartment.Rows)
                {
                    if (r.Cells[0].Text == "0")
                    {
                        Int32 BranchDeptStripeID = VendorMaintenanceBLL.InsertHotelHotelBranchDeptStripe("0", BranchID.ToString(), r.Cells[1].Text,
                            r.Cells[3].Text, GlobalCode.Field2String(Session["UserName"]));

                        if (BranchDeptStripeID != 0)
                        {
                            //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                            strLogDescription = "Hotel vendor branch department and stripe added.";
                            strFunction = "uoButtonSave_Click";

                            DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(BranchDeptStripeID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                        }
                    }
                }                
                //Save RankException
                foreach (GridViewRow r in uoGridViewRanks.Rows)
                {
                    if (r.Cells[0].Text == "0")
                    {
                        Int32 pBranchRankExceptionID = VendorMaintenanceBLL.InsertHotelHotelBranchRankException("0", 
                            BranchID.ToString(), r.Cells[1].Text, GlobalCode.Field2String(Session["UserName"]));

                        if (pBranchRankExceptionID != 0)
                        {
                            //Insert log audit trail (Gabriel Oquialda - 14/02/2012)
                            strLogDescription = "Hotel vendor branch rank exception added.";
                            strFunction = "uoButtonSave_Click";

                            DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                            BLL.AuditTrailBLL.InsertLogAuditTrail(pBranchRankExceptionID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                                  CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                        }
                    }
                }
                if (Request.QueryString["vmId"] == "0" || Request.QueryString["vmId"] == null)
                {
                    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                    strLogDescription = "Hotel vendor branch added.";
                    strFunction = "uoButtonSave_Click";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(BranchID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
                else
                {
                    //Insert log audit trail (Gabriel Oquialda - 18/11/2011)
                    strLogDescription = "Hotel vendor branch updated.";
                    strFunction = "uoButtonSave_Click";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(BranchID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
                
                uoHiddenFieldBranchID.Value = BranchID.ToString();

                //HotelSaveRoomTypeCapacity();
                VendorBranchInfoLoad();
                // OpenParentPage();
                AlertMessage("Save successfully.", true);

                Response.Redirect("HotelAirportBrandView.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]);
            }
            catch (Exception ex)
            {
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                ErrorBLL.InsertError(ex.Message, ex.StackTrace.ToString(), Request.Url.AbsolutePath,
                    currentDate, CommonFunctions.GetDateTimeGMT(currentDate), GlobalCode.Field2String(Session["UserName"]));

                AlertMessage(ex.Message, true);
            }
        }

        protected void uoButtonVoucher_Click(object sender, EventArgs e)
        {
            //uoButtonSaveMeal.Enabled = true;
            CreateDatatable();
            uoTextBoxStripes.Text = "";
            uoTextBoxAmount.Text = "";
            uoTextBoxDayNo.Text = "";
        }

        //#region uoButtonSave_Click Meal
        //protected void uoButtonSaveMeal_Click(object sender, EventArgs e)
        //{
        //    string strLogDescription;
        //    string strFunction;

        //    if (uoHiddenFieldBranchID.Value != "")
        //    {
        //        DataTable dt = new DataTable(); ;
        //        dt = (DataTable)ViewState["Table"];
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < uoGridViewMeal.Rows.Count; i++) //Save the values one by one
        //            {
        //                string Meal = dt.Rows[i]["Meal"].ToString();

        //                Int32 pID = 0;

        //                pID = HotelBLL.InsertHotelMealBranch(uoHiddenFieldBranchID.Value, Meal, GlobalCode.Field2String(Session["UserName"]));

        //                Insert log audit trail (Gabriel Oquialda - 17/11/2011)
        //                strLogDescription = "Hotel branch meal added.";
        //                strFunction = "uoButtonSaveMeal_Click";

        //                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

        //                BLL.AuditTrailBLL.InsertLogAuditTrail(pID, strLogDescription, strFunction, Path.GetFileName(Request.Path),
        //                                                      CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        //            }
        //        }
        //    }
        //    AlertMessage("Save successfully.");
        //}
        //#endregion

        /// <summary>
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change DataTable to List<HotelBranchVoucherList>      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoGridViewVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<HotelBranchVoucherList> list = null;
            List<HotelBranchVoucherList> listToRemove = null;
            try
            {
                string strLogDescription;
                string strFunction;

                //GridViewRow row = uoGridViewVoucher.SelectedRow;
                string stripe = uoGridViewVoucher.SelectedDataKey.Value.ToString();
                string DayNo = uoGridViewVoucher.Rows[uoGridViewVoucher.SelectedIndex].Cells[2].Text;
                int VoucherID = GlobalCode.Field2Int(uoGridViewVoucher.Rows[uoGridViewVoucher.SelectedIndex].Cells[0].Text);

                if (uoHiddenFieldBranchID.Value != "")
                {
                    //VendorMaintenanceBLL.RemoveHotelBranchVoucherByID(uoHiddenFieldBranchID.Value, stripe, GlobalCode.Field2String(Session["UserName"]), DayNo);
                    VendorMaintenanceBLL.RemoveHotelBranchVoucherByID(GlobalCode.Field2String(Session["UserName"]), VoucherID);

                    strLogDescription = "Hotel branch voucher deleted. (flagged as inactive)";
                    strFunction = "uoGridViewVoucher_SelectedIndexChanged";

                    DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                    BLL.AuditTrailBLL.InsertLogAuditTrail(VoucherID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                          CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                }
                list = (List<HotelBranchVoucherList>)Session["VoucherList"];//HotelBranchVoucherList.VoucherList;               
                //listToRemove = list.Where(a => a.Stripe == GlobalCode.Field2Decimal(stripe) && a.DayNo == GlobalCode.Field2TinyInt(DayNo)).ToList();
                //list.RemoveAll(x => listToRemove.Contains(x));
                if (VoucherID == 0)
                {
                    list.RemoveAll(a => a.Stripe == GlobalCode.Field2Decimal(stripe) && a.DayNo == GlobalCode.Field2TinyInt(DayNo));
                }
                else
                {
                    list.RemoveAll(a => a.VoucherID == VoucherID);
                }

                //HotelBranchVoucherList.VoucherList = list;
                Session["VoucherList"] = list;
                uoGridViewVoucher.DataSource = list;
                uoGridViewVoucher.Columns[0].Visible = true;
                uoGridViewVoucher.DataBind();
                uoGridViewVoucher.Columns[0].Visible = false;
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
                if (listToRemove != null)
                {
                    listToRemove = null;
                }
            }
        }
        /// <summary>
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Add try and catch and dispose DataTable
        ///                     Add the deleted room type to the drop down list
        /// ---------------------------------------------------------------------                    
        /// Date Modified:      24/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change DataTable to List, delete ViewState          
        /// ---------------------------------------------------------------------                    
        /// Date Modified:      31/05/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change HotelBranchRoomType.RoomTypeList to Session["RoomTypeList"]
        ///                     Change HotelBranchRoomTypeNotExist.RoomTypeList to Session["HotelBranchRoomTypeNotExist"]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoGridViewRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<HotelBranchRoomType> RoomTypeList = null;
            List<HotelBranchRoomTypeNotExist> RoomTypeNotExistList = null;
            try
            {
                //RoomTypeList = HotelBranchRoomType.RoomTypeList;
                //RoomTypeNotExistList = HotelBranchRoomTypeNotExist.RoomTypeList;

                RoomTypeList = (List<HotelBranchRoomType>)Session["RoomTypeList"];
                RoomTypeNotExistList = (List<HotelBranchRoomTypeNotExist>)Session["HotelBranchRoomTypeNotExist"];

                string strLogDescription;
                string strFunction;

                string RoomType = uoGridViewRoom.SelectedDataKey.Value.ToString();
                Int32 BranchRoomID = 0;
                if (uoHiddenFieldBranchID.Value != "")
                {
                    BranchRoomID = VendorMaintenanceBLL.RemoveHotelBranchRoomByID(uoHiddenFieldBranchID.Value, RoomType, GlobalCode.Field2String(Session["UserName"]));
                    if (BranchRoomID != 0)
                    {
                        //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                        strLogDescription = "Hotel branch room deleted. (flagged as inactive)";
                        strFunction = "uoGridViewRoom_SelectedIndexChanged";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(BranchRoomID, "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }                                      
                }
                //ListItem ItemToAddInDDL = new ListItem(RoomType, BranchRoomID.ToString());
                //uoDropDownListRoom.Items.Insert(1, ItemToAddInDDL);

                //dt = new DataTable();
                //dt = (DataTable)ViewState["TableRoom"];
                //dt.Rows.RemoveAt(this.uoGridViewRoom.SelectedIndex);
                HotelBranchRoomTypeNotExist item = new HotelBranchRoomTypeNotExist();
                item.colRoomTypeID = GlobalCode.Field2TinyInt(uoGridViewRoom.Rows[uoGridViewRoom.SelectedIndex].Cells[0].Text);
                item.colRoomNameVarchar = RoomType;

                RoomTypeNotExistList.Add(item);
                RoomTypeList.RemoveAll(a => a.colRoomNameVarchar == RoomType);

                //HotelBranchRoomType.RoomTypeList = RoomTypeList;
                //HotelBranchRoomTypeNotExist.RoomTypeList = RoomTypeNotExistList;
                Session["RoomTypeList"] = RoomTypeList;
                Session["HotelBranchRoomTypeNotExist"] = RoomTypeNotExistList;

                uoGridViewRoom.DataSource = RoomTypeList;
                uoGridViewRoom.Columns[0].Visible = true;
                uoGridViewRoom.DataBind();
                uoGridViewRoom.Columns[0].Visible = false;

                HotelGetRoomType(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoomTypeList != null)
                {
                    RoomTypeList = null;
                }
                if (RoomTypeNotExistList != null)
                {
                    RoomTypeNotExistList = null;
                }
            }
        }

        //#region uoButtonAdd_Click Meal
        //protected void uoButtonAdd_Click(object sender, EventArgs e)
        //{

        //}
        //#endregion


        protected void uoButtonAddRoom_Click(object sender, EventArgs e)
        {
            CreateDatatableRoom();
            //HotelGetRoomTypeForCapacity();
        }

        protected void uoButtonViewCity_Click(object sender, EventArgs e)
        {
            vendorCityLoad(int.Parse(uoDropDownListCountry.SelectedValue));
        }       
        protected void uoButtonSaveRank_Click(object sender, EventArgs e)
        {

        }     
        protected void uoGridViewDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteDataTableDeptStripes();
            BindDropdownStripe();
        }       
        protected void uoButtonSaveDeptStripe_Click(object sender, EventArgs e)
        {
            CreateDatatableDeptStripes();
            BindDropdownStripe();
        }
        protected void uoButtonSaveRanks_Click(object sender, EventArgs e)
        {
            CreateDatatableRankException();
            BindDropDownRank(false);
        }
        protected void uoDropDownListDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropdownStripe();
        }
        protected void uoButtonFilterRank_Click(object sender, EventArgs e)
        {
            BindDropDownRank(true);
        }        
        protected void uoGridViewRanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteDataTableRankException();
        }
        protected void uoButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(GlobalCode.Field2String(Session["strPrevPage"]));
        }       
        #endregion

        #region Functions
       
        #endregion
        /// <summary>
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Add try and catch and dispose DataTable
        ///                     Delete the added room type from the drop down list
        ///--------------------------------------------------------------                     
        /// Date Modified:      24/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change DataTable to List, delete ViewState  
        ///                     Delete other functions like _Create_DataRow_GridViewDataSourceRoom
        /// ---------------------------------------------------------------------                    
        /// Date Modified:      31/05/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change HotelBranchRoomType.RoomTypeList to Session["RoomTypeList"]
        ///                     Change HotelBranchRoomTypeNotExist.RoomTypeList to Session["HotelBranchRoomTypeNotExist"]
        /// </summary>
        private void CreateDatatableRoom()
        {
            List<HotelBranchRoomType> RoomTypeList = null;
            List<HotelBranchRoomTypeNotExist> RoomTypeNotExistList = null;
            try
            {
                 //RoomTypeList = HotelBranchRoomType.RoomTypeList;
                 //RoomTypeNotExistList = HotelBranchRoomTypeNotExist.RoomTypeList;
                RoomTypeList = (List<HotelBranchRoomType>)Session["RoomTypeList"];
                RoomTypeNotExistList = (List<HotelBranchRoomTypeNotExist>)Session["HotelBranchRoomTypeNotExist"];

                 HotelBranchRoomType item = new HotelBranchRoomType();
                 item.colRoomTypeID = GlobalCode.Field2TinyInt(uoDropDownListRoom.SelectedValue);
                 item.colRoomNameVarchar = uoDropDownListRoom.SelectedItem.Text;
                 RoomTypeList.Add(item);
                
                 //Remove selected room type from drop down list
                 RoomTypeNotExistList.RemoveAll(a => a.colRoomTypeID == GlobalCode.Field2TinyInt(uoDropDownListRoom.SelectedValue) 
                     && a.colRoomNameVarchar == uoDropDownListRoom.SelectedItem.Text);

                 //HotelBranchRoomType.RoomTypeList = RoomTypeList;
                 //HotelBranchRoomTypeNotExist.RoomTypeList = RoomTypeNotExistList;
                 Session["RoomTypeList"] = RoomTypeList;
                 Session["HotelBranchRoomTypeNotExist"] = RoomTypeNotExistList;

                 uoGridViewRoom.DataSource = RoomTypeList;
                 uoGridViewRoom.Columns[0].Visible = true;
                 uoGridViewRoom.DataBind();
                 uoGridViewRoom.Columns[0].Visible = false;

                 HotelGetRoomType(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RoomTypeList != null)
                {
                    RoomTypeList = null;
                }
                if (RoomTypeNotExistList != null)
                {
                    RoomTypeNotExistList = null;
                }
            }

            //DataTable dt = null;
            //try
            //{
            //    if (ViewState["AddRoom"].ToString() == "True")
            //    {
            //        dt = new DataTable();
            //        dt = (DataTable)ViewState["TableRoom"];
            //        //int i_TotalRows = dt.Rows.Count;

            //        DataRow dr;
            //        dr = dt.NewRow();
            //        dt.Rows.Add(this._Create_DataRow_GridViewDataSourceRoom(dr, uoDropDownListRoom.SelectedItem.Text, uoDropDownListRoom.SelectedValue));
            //        uoGridViewRoom.DataSource = dt;
            //        uoGridViewRoom.DataBind();
            //        ViewState["TableRoom"] = dt;                            
            //    }
            //    else
            //    {
            //        _Create_DataTable_GridViewDataSourceRoom(uoDropDownListRoom.SelectedItem.Text, uoDropDownListRoom.SelectedValue);
            //        ViewState["AddRoom"] = true;
            //        uoGridViewRoom.Enabled = true;
            //        uoGridViewRoom.DataBind();
            //    }
            //    ListItem ItemToRemove = new ListItem(uoDropDownListRoom.SelectedItem.Text, uoDropDownListRoom.SelectedValue);
            //    uoDropDownListRoom.Items.Remove(ItemToRemove);           
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Add try and catch and dispose DataTable
        /// </summary>
        /// <param name="RoomType"></param>
        /// <param name="RoomID"></param>
        //void _Create_DataTable_GridViewDataSourceRoom(string RoomType, string RoomID)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = new DataTable();
        //        dt.Columns.Add("colRoomNameVarchar");
        //        dt.Columns.Add("colRoomTypeID");
        //        dt.Columns.Add("colIsActiveBit");
        //        dt.Columns.Add("Delete");

        //        DataRow dr;
        //        dr = dt.NewRow();
        //        dt.Rows.Add(this._Create_DataRow_GridViewDataSourceRoom(dr, RoomType, RoomID));

        //        ViewState["TableRoom"] = dt;
        //        this.uoGridViewRoom.DataSource = dt;
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

        //DataRow _Create_DataRow_GridViewDataSourceRoom(DataRow dr, string RoomType, string RoomID)
        //{
        //    dr["colRoomNameVarchar"] = RoomType;
        //    dr["colRoomTypeID"] = RoomID;
        //    dr["colIsActiveBit"] = true;
        //    return dr;
        //}
        /// <summary>
        /// Date Modified:      23/02/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:      31/05/2012
        /// Modified By:        Josephine Gad
        /// (description)       Change HotelBranchVoucherList.VoucherList to Session["VoucherList"]
        /// </summary>
        private void CreateDatatable()
        {
            List<HotelBranchVoucherList> list = null;
            try
            {
                list = (List<HotelBranchVoucherList>)Session["VoucherList"];//HotelBranchVoucherList.VoucherList;
                //if (list == null)
                //{
                //    list = new List<HotelBranchVoucherList>();
                //}
                if (list.Count > 0)
                {
                    var voucher = (from a in list
                                   where a.Stripe.Equals(GlobalCode.Field2Decimal(uoTextBoxStripes.Text))
                                    && a.DayNo.Equals(GlobalCode.Field2TinyInt(uoTextBoxDayNo.Text))
                                   select new
                                   {
                                       Stripe = a.Stripe,
                                       DayNo = a.DayNo,
                                       Amount = a.Amount,
                                   }).ToList();
                    if (voucher.Count > 0)
                    {
                        AlertMessage("Stripe and Day No. already exist!", true);
                    }
                    else
                    {

                        HotelBranchVoucherList item = new HotelBranchVoucherList();
                        item.Stripe = GlobalCode.Field2Decimal(uoTextBoxStripes.Text);
                        item.Amount = GlobalCode.Field2Decimal(uoTextBoxAmount.Text);
                        item.BranchID = GlobalCode.Field2TinyInt(uoHiddenFieldBranchID.Value);
                        item.DayNo = GlobalCode.Field2TinyInt(uoTextBoxDayNo.Text);

                        list.Add(item);

                        //HotelBranchVoucherList.VoucherList = list;
                        Session["VoucherList"] = list;
                        uoGridViewVoucher.DataSource = list;
                        uoGridViewVoucher.Columns[0].Visible = true;
                        uoGridViewVoucher.DataBind();
                        uoGridViewVoucher.Columns[0].Visible = false;
                    }
                }
                else
                {

                    HotelBranchVoucherList item = new HotelBranchVoucherList();
                    item.Stripe = GlobalCode.Field2Decimal(uoTextBoxStripes.Text);
                    item.Amount = GlobalCode.Field2Decimal(uoTextBoxAmount.Text);
                    item.BranchID = GlobalCode.Field2TinyInt(uoHiddenFieldBranchID.Value);
                    item.DayNo = GlobalCode.Field2TinyInt(uoTextBoxDayNo.Text);

                    list.Add(item);

                    //HotelBranchVoucherList.VoucherList = list;
                    Session["VoucherList"] = list;
                    uoGridViewVoucher.DataSource = list;
                    uoGridViewVoucher.Columns[0].Visible = true;
                    uoGridViewVoucher.DataBind();
                    uoGridViewVoucher.Columns[0].Visible = false;
                }
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
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) Select All Vendnor  
        /// </summary>
        private void VendorLoad()
        {
            DataTable dt = null;
            try
            {
                dt = HotelBLL.HotelVendorGetDetails();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListVendor.DataSource = dt;
                    uoDropDownListVendor.DataTextField = "colVendorNameVarchar";
                    uoDropDownListVendor.DataValueField = "colVendorIdInt";
                }
                uoDropDownListVendor.DataBind();
                uoDropDownListVendor.Items.Insert(0, new ListItem("--Select a Hotel Brand--", "0"));
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
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist            
        /// --------------------------------------------------
        /// Date Modified:  14/12/2011
        /// Modified By:    Josephine Gad
        /// (description)   Get city by country (so it wont load all city)
        /// </summary>
        private void vendorCityLoad(int vendorCountryId)
        {
            DataTable dt = null;
            try
            {
                dt = CityBLL.GetCityByCountry(uoDropDownListCountry.SelectedValue, uoTextBoxFilterCity.Text.Trim(), "0");
                ListItem item = new ListItem("--SELECT CITY--", "0");
                uoDropDownListCity.Items.Clear();
                uoDropDownListCity.Items.Add(item);
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCity.DataTextField = "colCityNameVarchar";
                    uoDropDownListCity.DataValueField = "colCityIDInt";
                    uoDropDownListCity.DataSource = dt;
                }
                uoDropDownListCity.DataBind();

                if (dt.Rows.Count == 1)
                {
                    uoDropDownListCity.SelectedIndex = 1;
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
        #region vendorCountryLoad
        /// <summary>
        /// Date Created: 29/07/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor city to dropdownlist            
        /// </summary>
        private void vendorCountryLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = CountryBLL.CountryList();
                if (dt.Rows.Count > 0)
                {
                    uoDropDownListCountry.DataSource = dt;
                    uoDropDownListCountry.DataTextField = "colCountryNameVarchar";
                    uoDropDownListCountry.DataValueField = "colCountryIDInt";
                    uoDropDownListCountry.DataBind();
                    uoDropDownListCountry.Items.Insert(0, new ListItem("--Select a Country--", "0"));
                }
                else
                {
                    uoDropDownListCountry.DataBind();
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
        #endregion

        #region VendorBranchInfoLoad
        /// <summary>
        /// Date Created: 01/08/2011
        /// Created By: Gabriel Oquialda
        /// (description) Loads vendor information    
        /// -----------------------------------------
        /// Date Modified:  17/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Add try and catch and finally to close DataTable
        /// -----------------------------------------
        /// Date Modified:  24/01/2012
        /// Modified By:    Josephine Gad
        /// (description)   Disable uoTextBoxBranchCode if there is value
        /// -----------------------------------------
        /// Date Modified:  23/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change dt to VoucherList, change dtRoom to RoomTypeList
        /// -----------------------------------------
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add session:  Session["VoucherList"]; Session["RoomTypeList"];
        /// (description)   Session["DeptStripeList"]; Session["HotelBranchRankException"];
        /// -----------------------------------------
        /// Date Modified:  15/03/2013
        /// Modified By:    Jefferson Bermundo
        /// (description)   Display value of Fax Number and Website
        /// </summary>
        private void VendorBranchInfoLoad()
        {
            //IDataReader drVendorInfo = null;
            List<HotelBranchDetails> listHotel = new List<HotelBranchDetails>();
            List<IMSVendor> listIMSVendor = new List<IMSVendor>();

            List<HotelBranchVoucherList> VoucherList = new List<HotelBranchVoucherList>();
            List<HotelBranchRoomType> RoomTypeList = new List<HotelBranchRoomType>();              
            //DataTable dtRoomCapacity = null;
            try
            {
                //uoGridViewVoucher.DataBind();
                uoGridViewRoom.DataBind();
                if (Request.QueryString["vmId"].ToString() != "")
                {
                    VendorMaintenanceBLL.vendorBranchMaintenanceInformation(Int32.Parse(Request.QueryString["vmId"].ToString()));                    
                    //drVendorInfo = VendorMaintenanceBLL.vendorBranchMaintenanceInformation(Int32.Parse(Request.QueryString["vmId"].ToString()));
                    //if (drVendorInfo.Read())
                    listHotel = (List<HotelBranchDetails>)Session["HotelMaintenance_HotelDetails"];
                    listIMSVendor = (List<IMSVendor>)Session["HotelMaintenance_IMSVendor"];

                    BindIMSHotel(listIMSVendor);

                    if(listHotel.Count> 0)
                    {
                        uoHiddenFieldBranchID.Value = GlobalCode.Field2String(listHotel[0].HotelId);//drVendorInfo["colBranchIDInt"].ToString();

                        uoDropDownListVendor.Enabled = false;
                        uoDropDownListVendor.SelectedValue = GlobalCode.Field2String(listHotel[0].VendorId); //drVendorInfo["colVendorIdInt"].ToString();
                        uoTextBoxVendorName.Text = listHotel[0].HotelName; //drVendorInfo["colVendorBranchNameVarchar"].ToString();

                        uoTextBoxVendorAddress.Text = listHotel[0].Address;//drVendorInfo["colAddressVarchar"].ToString();

                        uoDropDownListCountry.SelectedValue = GlobalCode.Field2String(listHotel[0].CountryId);//drVendorInfo["colCountryIDInt"].ToString();
                        uoTextBoxFilterCity.Text = listHotel[0].CityName;//drVendorInfo["colCityNameVarchar"].ToString();
                        
                        int iCountryID = GlobalCode.Field2Int(uoDropDownListCountry.SelectedValue);
                        if (iCountryID > 1)
                        {
                            vendorCityLoad(iCountryID);
                        }
                        uoDropDownListCity.SelectedValue = GlobalCode.Field2String(listHotel[0].CityId);// drVendorInfo["colCityIDInt"].ToString();
                        uoTextBoxContactNo.Text = listHotel[0].ContractNo;//drVendorInfo["colContactNoVarchar"].ToString();
                        uoTextBoxPerson.Text = listHotel[0].ContactPerson;//drVendorInfo["colContactPersonVarchar"].ToString();
                        uoCheckBoxIsFranchise.Checked = GlobalCode.Field2Bool(listHotel[0].IsFranchise);//Convert.ToBoolean(drVendorInfo["colFranchiseBit"].ToString());
                        //uoCheckBoxRating.Checked = Convert.ToBoolean(drVendorInfo["colForRatingsBit"].ToString());
                        //uoCheckBoxOfficer.Checked = Convert.ToBoolean(drVendorInfo["colForOfficerBit"].ToString());
                        uoTextBoxFaxNo.Text = listHotel[0].FaxNo;//drVendorInfo["colFaxNoVarchar"].ToString();
                        uoTextBoxWebsite.Text = listHotel[0].Website;//drVendorInfo["colWebsiteVarchar"].ToString();

                        uoTextBoxBranchCode.Text = listHotel[0].BranchCode; //drVendorInfo["colBranchCodeVarchar"].ToString();

                        uoTextBoxInstructionOn.Text = listHotel[0].InstructionOn;
                        uoTextBoxInstructionOff.Text = listHotel[0].InstructionOff;


                        string sIMSVendorID = GlobalCode.Field2Int(uoTextBoxBranchCode.Text).ToString();

                        if (uoDropDownListIMSVendor.Items.FindByValue(sIMSVendorID) != null)
                        {
                            uoDropDownListIMSVendor.SelectedValue = sIMSVendorID;
                        }

                        uoTextBoxEmailTo.Text = listHotel[0].EmailTo;//drVendorInfo["colEmailToVarchar"].ToString();
                        uoTextBoxEmailCc.Text = listHotel[0].EmailCc;//drVendorInfo["colEmailCcVarchar"].ToString();

                        uoCheckBoxON.Checked = GlobalCode.Field2Bool(listHotel[0].OnBit); //(bool)drVendorInfo["colOnBit"];
                        uoCheckBoxOff.Checked = GlobalCode.Field2Bool(listHotel[0].OffBit);//(bool)drVendorInfo["colOffBit"];

                        ChangeToUpperCase(uoDropDownListCountry);
                        ChangeToUpperCase(uoDropDownListCity);

                        //Hotel voucher info                        
                        VoucherList = VendorMaintenanceBLL.GetHotelBranchVoucherByBranchID(uoHiddenFieldBranchID.Value);
                        
                        //HotelBranchVoucherList.VoucherList = VoucherList;
                        Session["VoucherList"] = VoucherList;
                        uoGridViewVoucher.DataSource = VoucherList;
                        uoGridViewVoucher.Columns[0].Visible = true;
                        uoGridViewVoucher.DataBind();
                        uoGridViewVoucher.Columns[0].Visible = false;


                        //Hotel room type info                      
                        RoomTypeList = HotelBLL.HotelRoomTypeGetDetailsByBranch(uoHiddenFieldBranchID.Value);
                        //HotelBranchRoomType.RoomTypeList = RoomTypeList;
                        Session["RoomTypeList"] = RoomTypeList;
                        uoGridViewRoom.DataSource = RoomTypeList;
                        uoGridViewRoom.Columns[0].Visible = true;
                        uoGridViewRoom.DataBind();
                        uoGridViewRoom.Columns[0].Visible = false;

                        bool IsNew = false;
                        if (uoHiddenFieldNew.Value == "1")
                        {
                            IsNew = true;
                        }

                        BindDeptStripe(IsNew);
                        BindRankException(IsNew);
                    }
                    else 
                    {
                        //HotelBranchVoucherList.VoucherList = new List<HotelBranchVoucherList>();
                        //HotelBranchRoomType.RoomTypeList = new List<HotelBranchRoomType>();
                        //HotelBranchDeptStripe.DeptStripeList = new List<HotelBranchDeptStripe>();
                        Session["VoucherList"] = VoucherList;
                        Session["RoomTypeList"] = RoomTypeList;
                        Session["DeptStripeList"] = new List<HotelBranchDeptStripe>();
                        Session["RankExceptionList"] = new List<HotelRankException>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (drVendorInfo != null)
                //{
                //    drVendorInfo.Close();
                //    drVendorInfo.Dispose();
                //}
                if (VoucherList != null)
                {
                    VoucherList = null;
                }
                if (RoomTypeList != null)
                {
                    RoomTypeList = null;
                }
                //if (dtRoomCapacity != null)
                //{
                //    dtRoomCapacity.Dispose();
                //}
            }
        }
        #endregion

        #region ChangeToUpperCase
        /// <summary>
        /// Date Created: 25/07/2011
        /// Created By: Ryan Bautista
        /// (description) format data to uppercase
        /// --------------------------------------------------------
        /// </summary>
        private void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }
        #endregion

        #region OpenParentPage
        /// <summary>
        /// Date Created: 19/07/2011
        /// Created By: Josephine Gad
        /// (description) Close this page and update parent page
        /// -------------------------------------------------------------------------------
        /// Date Modified: 02/08/2011
        /// Modified By: Gabriel Oquialda
        /// (description) Change script "#ctl00_ContentPlaceHolder1_uoHiddenFieldSeafarer\"
        ///               to "#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\"            
        /// </summary>
        private void OpenParentPage()
        {
            string sScript = "<script language='javascript'>";
            sScript += " window.parent.$(\"#ctl00_ContentPlaceHolder1_uoHiddenFieldPopupHotel\").val(\"1\"); ";
            sScript += " parent.$.fancybox.close(); ";
            sScript += "</script>";

            ScriptManager.RegisterClientScriptBlock(uoButtonSave, this.GetType(), "scr", sScript, false);
        }
        #endregion

        #region AlertMessage
        /// <summary>
        /// Date Created:   18/07/2011
        /// Created By:     Marco Abejar
        /// (description)   Show pop up message            
        /// -----------------------------------
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Add IsScripManager paramater for the DIV with UpdatePanel
        /// -----------------------------------
        /// Date Modified:  27/06/2012
        /// Modified By:    Josephine Gad
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
        #endregion


        /// <summary>
        /// Date Created:   25/07/2011
        /// Created By:     Ryan Bautista
        /// (description)   Select all hotel room type   
        /// -------------------------------------------
        /// Date Modified:  17/11/2011
        /// Modified By:    Josephine Gad
        /// (description)   Change HotelRoomType to HotelRoomTypeGetNotExist
        /// -------------------------------------------
        /// Date Modified:  24/02/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List<HotelBranchRoomTypeNotExist>
        /// -------------------------------------------
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Use session
        /// </summary>
        private void HotelGetRoomType(bool IsFirstLoad)
        {
            List<HotelBranchRoomTypeNotExist> list = null;
            try
            {
                if (IsFirstLoad)
                {
                    Session["HotelBranchRoomTypeNotExist"] = null;
                    Session["HotelBranchRoomTypeNotExist"] = HotelBLL.HotelRoomTypeGetNotExist(uoHiddenFieldVendorIdInt.Value);
                    //HotelBranchRoomTypeNotExist.RoomTypeList = list;
                }
                else
                {
                    if (Session["HotelBranchRoomTypeNotExist"] == null)
                    {
                        Session["HotelBranchRoomTypeNotExist"] = HotelBLL.HotelRoomTypeGetNotExist(uoHiddenFieldVendorIdInt.Value);
                    }
                }

                list = (List<HotelBranchRoomTypeNotExist>)Session["HotelBranchRoomTypeNotExist"];
                uoDropDownListRoom.Items.Clear();
                if (list.Count > 0)
                {                    
                    uoDropDownListRoom.DataSource = list;
                    uoDropDownListRoom.DataTextField = "colRoomNameVarchar";
                    uoDropDownListRoom.DataValueField = "colRoomTypeID";                    
                    uoDropDownListRoom.DataBind();                    
                }
                uoDropDownListRoom.Items.Insert(0, new ListItem("--Select a Room Type--", "0"));
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
        /// Date Created:    15/12/2011
        /// Created By:      Josephine Gad
        /// (description)    Select all department
        /// -------------------------------------------
        /// Date Modified:   24/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add Session["DepartmentList"] and parameter IsFirstLoad
        /// -------------------------------------------
        /// </summary>
        private void HotelGetDepartment(bool IsFirstLoad)
        {
            List<Department> list = null;
            
            if (IsFirstLoad)
            {
                Session["DepartmentList"] = null;
                Session["DepartmentList"] = MaintenanceViewBLL.GetDepartment();
            }
            else
            {
                if (Session["DepartmentList"] == null)
                {
                    Session["DepartmentList"] = MaintenanceViewBLL.GetDepartment();
                }                
            }
            try
            {
                list = (List<Department>)Session["DepartmentList"];          
                uoDropDownListDepartment.Items.Clear();
                if (list.Count > 0)
                {
                    uoDropDownListDepartment.DataSource = list;
                    uoDropDownListDepartment.DataTextField = "DeptName";
                    uoDropDownListDepartment.DataValueField = "DeptID";
                    uoDropDownListDepartment.DataBind();                    
                }
                uoDropDownListDepartment.Items.Insert(0, new ListItem("--Select Department--", "0"));
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
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add Session["DeptStripeList"] and parameter IsNew
        /// -------------------------------------------
        /// </summary>
        /// <param name="IsNew"></param>
        private void BindDeptStripe(bool IsNew)
        {
            List<HotelBranchDeptStripe> list = null;
            try
            {
                if (IsNew)
                {
                    Session["DeptStripeList"] = null;
                    Session["DeptStripeList"] = HotelBLL.GetHotelBranchDeptStripes(uoHiddenFieldBranchID.Value);
                }
                else
                {
                    if (Session["DeptStripeList"] == null)
                    {
                        Session["DeptStripeList"] = HotelBLL.GetHotelBranchDeptStripes(uoHiddenFieldBranchID.Value);
                    }
                }
                //if (IsNew || HotelBranchDeptStripe.DeptStripeList == null)
                //{
                //    HotelBLL.GetHotelBranchDeptStripes(uoHiddenFieldBranchID.Value);
                //}

                list = (List<HotelBranchDeptStripe>)Session["DeptStripeList"];
                uoGridViewDepartment.DataSource = list;//HotelBranchDeptStripe.DeptStripeList;
                uoGridViewDepartment.Columns[0].Visible = true;
                uoGridViewDepartment.Columns[1].Visible = true;
                uoGridViewDepartment.Columns[5].Visible = true;
                uoGridViewDepartment.DataBind();
                uoGridViewDepartment.Columns[0].Visible = false;
                uoGridViewDepartment.Columns[1].Visible = false;
                uoGridViewDepartment.Columns[5].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// Date Created:   16/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Add New Row
        /// -------------------------------------------
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change Stripe.StripeList to Session["StripeList"]
        /// (description)    Change HotelBranchDeptStripe.DeptStripeList to Session["DeptStripeList"]
        /// -------------------------------------------
        /// </summary>
        private void CreateDatatableDeptStripes()
        {
            List<HotelBranchDeptStripeNotExist> stripeNotExistList = null;
            List<HotelBranchDeptStripe> stripeList = null;
            List<Stripe> allStripeList = null;
            HotelBranchDeptStripe stripeItem = null;
            try
            {
                 //all stripes
                //if (Stripe.StripeList == null)
                if (Session["StripeList"] == null)
                {
                    Session["StripeList"] = MaintenanceViewBLL.GetStripes();
                }
                allStripeList = (List<Stripe>)Session["StripeList"]; //Stripe.StripeList;

                //dept and stripes in branch
                //if (HotelBranchDeptStripe.DeptStripeList == null)
                if (Session["DeptStripeList"] == null)
                {
                    //HotelBLL.GetHotelBranchDeptStripes(uoHiddenFieldBranchID.Value);
                    stripeList = new List<HotelBranchDeptStripe>();
                    
                    for (int i = 0; i < uoGridViewDepartment.Rows.Count; i++)
                    {
                        stripeItem = new HotelBranchDeptStripe();

                        stripeItem.BranchDeptStripeID = GlobalCode.Field2Int(uoGridViewDepartment.Rows[i].Cells[0].Text);
                        stripeItem.Stripes = GlobalCode.Field2Decimal(uoGridViewDepartment.Rows[i].Cells[3].Text);
                        stripeItem.StripeName = uoGridViewDepartment.Rows[i].Cells[5].Text;
                        stripeItem.DeptID = GlobalCode.Field2TinyInt(uoGridViewDepartment.Rows[i].Cells[1].Text);
                        stripeItem.DeptName = uoGridViewDepartment.Rows[i].Cells[2].Text;

                        stripeList.Add(stripeItem);
                    }
                }
                else
                {
                    stripeList = (List<HotelBranchDeptStripe>)Session["DeptStripeList"] ;//HotelBranchDeptStripe.DeptStripeList;
                }

                stripeItem = new HotelBranchDeptStripe();
                stripeItem.BranchDeptStripeID = 0;
                stripeItem.Stripes = GlobalCode.Field2Decimal(uoDropDownListStripes.SelectedValue);
                stripeItem.StripeName = uoDropDownListStripes.SelectedItem.Text;
                stripeItem.DeptID = GlobalCode.Field2TinyInt(uoDropDownListDepartment.SelectedValue);
                stripeItem.DeptName = uoDropDownListDepartment.SelectedItem.Text;
                stripeList.Add(stripeItem);
                
                //HotelBranchDeptStripe.DeptStripeList = stripeList;
                Session["DeptStripeList"] = stripeList;
                BindDeptStripe(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stripeNotExistList != null)
                {
                    stripeNotExistList = null;
                }
                if (stripeList != null)
                {
                    stripeList = null;
                }
                if (allStripeList != null)
                {
                    allStripeList = null;
                }
                if (stripeItem != null)
                {
                    stripeItem = null;
                }
            }           
        }
        /// <summary>
        /// Date Created:   16/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Remove Row of DeptStripe
        /// -------------------------------------------        
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change Stripe.StripeList to Session["StripeList"]
        /// (description)    Change HotelBranchDeptStripe.DeptStripeList to Session["DeptStripeList"]
        /// -------------------------------------------
        /// </summary>
        private void DeleteDataTableDeptStripes()
        {
            List<HotelBranchDeptStripeNotExist> stripeNotExistList = null;
            List<HotelBranchDeptStripe> stripeList = null;
            List<Stripe> allStripeList = null;
            HotelBranchDeptStripe stripeItem = null;
            try
            {
                //all stripes
                //if (Stripe.StripeList == null)
                //{
                //    MaintenanceViewBLL.GetStripes();
                //}
                if (Session["StripeList"] == null)
                {
                    Session["StripeList"] = MaintenanceViewBLL.GetStripes();
                }
                allStripeList = (List<Stripe>)Session["StripeList"];

                //dept and stripes in branch
                //if (HotelBranchDeptStripe.DeptStripeList == null)
                if (Session["DeptStripeList"] == null)
                {
                    //HotelBLL.GetHotelBranchDeptStripes(uoHiddenFieldBranchID.Value);
                    stripeList = new List<HotelBranchDeptStripe>();

                    for (int i = 0; i < uoGridViewDepartment.Rows.Count; i++)
                    {
                        stripeItem = new HotelBranchDeptStripe();

                        stripeItem.BranchDeptStripeID = GlobalCode.Field2Int(uoGridViewDepartment.Rows[i].Cells[0].Text);
                        stripeItem.Stripes = GlobalCode.Field2Decimal(uoGridViewDepartment.Rows[i].Cells[3].Text);
                        stripeItem.StripeName = uoGridViewDepartment.Rows[i].Cells[5].Text;
                        stripeItem.DeptID = GlobalCode.Field2TinyInt(uoGridViewDepartment.Rows[i].Cells[1].Text);
                        stripeItem.DeptName = uoGridViewDepartment.Rows[i].Cells[2].Text;

                        stripeList.Add(stripeItem);
                    }
                }
                else
                {
                    stripeList = (List<HotelBranchDeptStripe>)Session["DeptStripeList"];//HotelBranchDeptStripe.DeptStripeList;
                }
                int iIndex = uoGridViewDepartment.SelectedIndex;
                int BranchDeptStripeID = GlobalCode.Field2Int(uoGridViewDepartment.Rows[iIndex].Cells[0].Text);
                Int16 DeptID = GlobalCode.Field2TinyInt(uoGridViewDepartment.Rows[iIndex].Cells[1].Text);
                decimal Stripes = GlobalCode.Field2Decimal(uoGridViewDepartment.Rows[iIndex].Cells[3].Text);

                if (BranchDeptStripeID != 0)
                {
                    VendorMaintenanceBLL.DeleteHotelHotelBranchDeptStripe(BranchDeptStripeID.ToString(), GlobalCode.Field2String(Session["UserName"]));
                }

                stripeList.RemoveAll(a => a.DeptID == DeptID && a.Stripes== Stripes);
                BindDeptStripe(false);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stripeNotExistList != null)
                {
                    stripeNotExistList = null;
                }
                if (stripeList != null)
                {
                    stripeList = null;
                }
                if (allStripeList != null)
                {
                    allStripeList = null;
                }
                if (stripeItem != null)
                {
                    stripeItem = null;
                }
            }            
        }
        /// <summary>
        /// Date Created:   20/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get stripes not in grid
        /// -------------------------------------------        
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change Stripe.StripeList to Session["StripeList"]
        /// (description)    Change HotelBranchDeptStripe.DeptStripeList to Session["DeptStripeList"]
        /// -------------------------------------------
        /// </summary>
        private void BindDropdownStripe()
        {
            List<HotelBranchDeptStripeNotExist> stripeNotExistList = null;
            List<HotelBranchDeptStripe> stripeList = null;
            List<Stripe> allStripeList = null;
            try
            {
                //all stripes
                //if (Stripe.StripeList == null)
                //{
                //    MaintenanceViewBLL.GetStripes();
                //}
                if (Session["StripeList"] == null)
                {
                    Session["StripeList"] = MaintenanceViewBLL.GetStripes();
                }
                allStripeList = (List<Stripe>)Session["StripeList"];

                //dept and stripes in branch
                if (Session["DeptStripeList"] == null)
                {
                    Session["DeptStripeList"] = HotelBLL.GetHotelBranchDeptStripes(uoHiddenFieldBranchID.Value);
                }
                stripeList = (List<HotelBranchDeptStripe>)Session["DeptStripeList"];//HotelBranchDeptStripe.DeptStripeList;

                //stripes not in dept and branch
                //if (HotelBranchDeptStripeNotExist.DeptStripeList == null)
                //{
                stripeNotExistList = (from a in allStripeList.Where(all => !stripeList.Exists(s => all.Stripes == s.Stripes
                                          && s.DeptID == GlobalCode.Field2Decimal(uoDropDownListDepartment.SelectedValue)))
                                      select new HotelBranchDeptStripeNotExist
                                      {
                                          Stripes = a.Stripes,
                                          StripeName = a.StripeName
                                      }).ToList();
                //HotelBranchDeptStripeNotExist.DeptStripeList = stripeNotExistList;
                //}               
                uoDropDownListStripes.Items.Clear();
                uoDropDownListStripes.DataSource = stripeNotExistList;//HotelBranchDeptStripeNotExist.DeptStripeList;
                uoDropDownListStripes.DataValueField = "Stripes";
                uoDropDownListStripes.DataTextField = "StripeName";
                uoDropDownListStripes.DataBind();
                uoDropDownListStripes.Items.Insert(0, new ListItem("--Select Stripes--", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stripeNotExistList != null)
                {
                    stripeNotExistList = null;
                }
                if (stripeList != null)
                {
                    stripeList = null;
                }
                if (allStripeList != null)
                {
                    allStripeList = null;
                }
            }           
        }              
        /// <summary>
        /// Date Created:    22/12/2011
        /// Created By:      Josephine Gad
        /// (description)    Get stripes list to be used in Rank filter
        /// -------------------------------------------
        /// Date Modified:   24/12/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:   31/05/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add Session["StripeList"] and parameter IsFirstLoad
        /// -------------------------------------------
        /// </summary>
        private void BindDropdownStripeInRank(bool IsFirstLoad)
        {
            List<Stripe> list = null;

            try
            {
                if (IsFirstLoad)
                {
                    Session["StripeList"] = null;
                    Session["StripeList"] = MaintenanceViewBLL.GetStripes();
                }
                else
                {
                    if (Session["StripeList"] == null)
                    {
                        Session["StripeList"] = MaintenanceViewBLL.GetStripes();
                    }
                }
                list = (List<Stripe>)Session["StripeList"];
                uoDropDownListStripesRanks.Items.Clear();
                if (list.Count > 0)
                {
                    uoDropDownListStripesRanks.DataSource = list;//Stripe.StripeList;
                    uoDropDownListStripesRanks.DataTextField = "StripeName";
                    uoDropDownListStripesRanks.DataValueField = "Stripes";
                }
                uoDropDownListStripesRanks.DataBind();
                uoDropDownListStripesRanks.Items.Insert(0, new ListItem("--All Stripes--","0"));
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
        /// Date Created:   22/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Rank list by Stripes
        /// -------------------------------------------
        /// Date Created:   31/05/2012
        /// Created By:     Josephine Gad
        /// (description)   Use Session["RankExceptionList"] instead of HotelRankException.RankExceptionList
        /// -------------------------------------------
        /// </summary>
        private void BindDropDownRank(bool IsNew)
        {            
            DataTable dt = null;
            List<HotelRankExceptionNotExist> list = new List<HotelRankExceptionNotExist>();
            List<HotelRankExceptionNotExist> listFiltered = new List<HotelRankExceptionNotExist>();
            List<HotelRankException> Ranklist = new List<HotelRankException>();
            try
            {
                uoDropDownListRanks.Items.Clear();
                if (Session["RankExceptionList"] != null)
                {
                    Ranklist = (List<HotelRankException>)Session["RankExceptionList"];//HotelRankException.RankExceptionList;
                }
                if (IsNew)
                {
                    dt = MaintenanceViewBLL.GetRanks(uoDropDownListStripesRanks.SelectedValue, uoTextBoxFilterRank.Text.Trim());
                    list = (from a in dt.AsEnumerable()
                            select new HotelRankExceptionNotExist
                            {
                                BranchRankExceptID = 0,
                                BranchID = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value),
                                RankID = GlobalCode.Field2Int(a["RankID"]),
                                RankName = a["RankName"].ToString()
                            }).ToList();
                   
                }
                else
                {
                    if (Session["RankExceptionListInDropDown"] != null)
                    {
                        list = (List<HotelRankExceptionNotExist>)Session["RankExceptionListInDropDown"];
                    }
                }
                if (Ranklist != null)
                {
                    listFiltered = (from a in list.Where(all => !Ranklist.Exists(s => all.RankID == s.RankID))
                                    orderby a.RankName
                                    select new HotelRankExceptionNotExist
                                    {
                                        BranchRankExceptID = a.BranchRankExceptID,
                                        BranchID = a.BranchID,
                                        RankID = a.RankID,
                                        RankName = a.RankName
                                    }).ToList();
                }
                else
                {
                    listFiltered = list;
                }

                //HotelRankExceptionNotExist.RankExceptionList = listFiltered.ToList();
                Session["RankExceptionListInDropDown"] = listFiltered;
                if (listFiltered.Count > 0)
                {
                    uoDropDownListRanks.DataSource = listFiltered;
                    uoDropDownListRanks.DataTextField = "RankName";
                    uoDropDownListRanks.DataValueField = "RankID";
                }
                uoDropDownListRanks.DataBind();
                uoDropDownListRanks.Items.Insert(0, new ListItem("--Select Ranks--", "0"));
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
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Select Rank Exception for this branch
        /// -------------------------------------------        
        /// Date Modified:  21/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        ///                 Change Cache to Session
        /// </summary>
        private List<HotelRankException> GetDataTableRankException(bool IsNew)
        {
            List<HotelRankException> list = new List<HotelRankException>();
            try
            {
                if (IsNew)
                {
                    Session.Remove("RankExceptionList");
                    Session["RankExceptionList"] = HotelBLL.GetHotelBranchRankException(uoHiddenFieldBranchID.Value);
                    //list = HotelBLL.GetHotelBranchRankException(uoHiddenFieldBranchID.Value);
                    //HotelRankException.RankExceptionList = list;                    
                    //dt.PrimaryKey = new DataColumn[] { dt.Columns["iOrder"] };
                    //Store HotelBranchRankException DataTable in Cache for 5 minutes
                    //Cache.Insert("HotelBranchRankException", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                    //return list;
                }
                else
                {
                    if (Session["RankExceptionList"] == null)
                    {
                        Session["RankExceptionList"] = HotelBLL.GetHotelBranchRankException(uoHiddenFieldBranchID.Value);
                    }
                    //else
                    //{
                    //    list = HotelBLL.GetHotelBranchRankException(uoHiddenFieldBranchID.Value);
                    //    //HotelRankException.RankExceptionList = list;
                    //    Session["HotelBranchRankException"] = list;
                    //    //dt.PrimaryKey = new DataColumn[] { dt.Columns["iOrder"] };
                    //    //Store HotelBranchRankException DataTable in Cache for 5 minutes
                    //    //Cache.Insert("HotelBranchRankException", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                    //}                    
                }
                list = (List<HotelRankException>)Session["RankExceptionList"];
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        /// <summary>
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Bind uoGridViewRanks
        /// -------------------------------------------        
        /// Date Modified:  21/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        ///                 Change Cache to Session
        /// </summary>
        private void BindRankException(bool IsNew)
        {
            List<HotelRankException> list = new List<HotelRankException>();
            try
            {
                list = GetDataTableRankException(IsNew);
                //if (list.Count > 0)
                //{
                uoGridViewRanks.DataSource = list;
                uoGridViewRanks.Columns[0].Visible = true;
                uoGridViewRanks.Columns[1].Visible = true;
                uoGridViewRanks.DataBind();
                uoGridViewRanks.Columns[0].Visible = false;
                uoGridViewRanks.Columns[1].Visible = false;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        /// <summary>
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Add New Row in Rank Exception
        /// -------------------------------------------        
        /// Date Modified:  21/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List
        ///                 Change Cache to Session        
        /// -------------------------------------------        
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change HotelRankException.RankExceptionList = Session["RankExceptionList"]
        /// (description)   Change HotelRankExceptionNotExist.RankExceptionList = Session["RankExceptionListInDropDown"]         
        /// </summary>
        private void CreateDatatableRankException()
        {
            List<HotelRankException> RankList = null;
            List<HotelRankExceptionNotExist> RankNotExistList = null;
            try
            {
                //RankList = HotelRankException.RankExceptionList;
                //RankNotExistList = HotelRankExceptionNotExist.RankExceptionList;

                RankList = (List<HotelRankException>)Session["RankExceptionList"];
                RankNotExistList = (List<HotelRankExceptionNotExist>)Session["RankExceptionListInDropDown"];

                HotelRankException item = new HotelRankException();
                item.BranchRankExceptID = 0;
                item.BranchID = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
                item.RankID = GlobalCode.Field2Int(uoDropDownListRanks.SelectedValue);
                item.RankName = uoDropDownListRanks.SelectedItem.Text;
                RankList.Add(item);

                //Remove selected room type from drop down list
                RankNotExistList.RemoveAll(a => a.RankID == GlobalCode.Field2TinyInt(uoDropDownListRanks.SelectedValue));

                //HotelRankException.RankExceptionList = RankList;
                //HotelRankExceptionNotExist.RankExceptionList = RankNotExistList;

                Session["RankExceptionList"] = RankList;
                Session["RankExceptionListInDropDown"] = RankNotExistList;

                uoGridViewRanks.DataSource = RankList;
                uoGridViewRanks.Columns[0].Visible = true;
                uoGridViewRanks.Columns[1].Visible = true;
                uoGridViewRanks.DataBind();
                uoGridViewRanks.Columns[0].Visible = false;
                uoGridViewRanks.Columns[1].Visible = false;

                GetDataTableRankException(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //List<HotelRankException> list = new List<HotelRankException>();
            //try
            //{
            //    list = GetDataTableRankException(false);
            //    //DataRow dr = dt.NewRow();
            //    //dr["BranchRankExceptID"] = "0";
            //    //dr["BranchID"] = uoHiddenFieldBranchID.Value;
            //    //dr["RankID"] = uoDropDownListRanks.SelectedValue;
            //    //dr["RankName"] = uoDropDownListRanks.SelectedItem.Text;
            //    //dr["iOrder"] = uoHiddenFieldBranchID.Value + "_" + uoDropDownListRanks.SelectedValue;
            //    //DataRow drExists = dt.Rows.Find(uoHiddenFieldBranchID.Value + "_" + uoDropDownListRanks.SelectedValue);
            //    //if (drExists == null)
            //    //{
            //    //    dt.Rows.Add(dr);

            //    //    dt.DefaultView.Sort = "RankName";

            //    //    Cache.Insert("HotelBranchRankException", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));

            //    //    uoGridViewRanks.DataSource = dt;

            //    //    uoGridViewRanks.Columns[0].Visible = true;
            //    //    uoGridViewRanks.Columns[1].Visible = true;
            //    //    uoGridViewRanks.DataBind();
            //    //    uoGridViewRanks.Columns[0].Visible = false;
            //    //    uoGridViewRanks.Columns[1].Visible = false;
            //    //}
            //    uoDropDownListRanks.SelectedValue = "0";
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}           
        }
        /// <summary>
        /// Date Created:   23/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Remove Row of Rank Exception
        /// -------------------------------------------        
        /// Date Modified:  21/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change DataTable to List     
        /// -------------------------------------------        
        /// Date Modified:  31/05/2012
        /// Modified By:    Josephine Gad
        /// (description)   Change HotelRankException.RankExceptionList = Session["RankExceptionList"]
        /// (description)   Change HotelRankExceptionNotExist.RankExceptionList = Session["RankExceptionListInDropDown"]    
        /// </summary>
        private void DeleteDataTableRankException()
        {
            List<HotelRankException> RankList = null;
            List<HotelRankExceptionNotExist> RankNotExistList = null;
            try
            {
                //RankList = HotelRankException.RankExceptionList;
                //RankNotExistList = HotelRankExceptionNotExist.RankExceptionList;
                RankList = (List<HotelRankException>)Session["RankExceptionList"];
                RankNotExistList = (List<HotelRankExceptionNotExist>)Session["RankExceptionListInDropDown"];

                string strLogDescription;
                string strFunction;

                string ExceptionID = uoGridViewRanks.SelectedRow.Cells[0].Text;//.SelectedDataKey.Value.ToString();
                if (uoHiddenFieldBranchID.Value != "")
                {
                    if (ExceptionID != "0" && ExceptionID != "")
                    {
                        VendorMaintenanceBLL.DeleteHotelHotelBranchRankException(ExceptionID, GlobalCode.Field2String(Session["UserName"]));

                        //Insert log audit trail (Gabriel Oquialda - 17/11/2011)
                        strLogDescription = "Hotel rank exception deleted. (flagged as inactive)";
                        strFunction = "uoGridViewRanks_SelectedIndexChanged-DeleteDataTableRankException";

                        DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                        BLL.AuditTrailBLL.InsertLogAuditTrail(GlobalCode.Field2Int(ExceptionID), "", strLogDescription, strFunction, Path.GetFileName(Request.Path),
                                                              CommonFunctions.GetDateTimeGMT(currentDate), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
                    }
                }
                HotelRankExceptionNotExist item = new HotelRankExceptionNotExist();
                item.BranchRankExceptID = GlobalCode.Field2Int(ExceptionID);
                item.BranchID = GlobalCode.Field2Int(uoHiddenFieldBranchID.Value);
                item.RankID = GlobalCode.Field2Int(uoGridViewRanks.SelectedRow.Cells[1].Text);
                item.RankName = uoGridViewRanks.SelectedRow.Cells[2].Text;
                RankNotExistList.Add(item);

                RankList.RemoveAll(a => a.RankID == GlobalCode.Field2Int(uoGridViewRanks.SelectedRow.Cells[1].Text));

                //HotelRankException.RankExceptionList = RankList;
                //HotelRankExceptionNotExist.RankExceptionList = RankNotExistList;
                Session["RankExceptionList"] = RankList;
                Session["RankExceptionListInDropDown"] = RankNotExistList;

                uoGridViewRanks.DataSource = RankList;
                uoGridViewRanks.Columns[0].Visible = true;
                uoGridViewRanks.Columns[1].Visible = true;
                uoGridViewRanks.DataBind();
                uoGridViewRanks.Columns[0].Visible = false;
                uoGridViewRanks.Columns[1].Visible = false;

                BindDropDownRank(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
                //DataTable dt = null;
                //try
                //{
                //    string iOrder = uoGridViewRanks.SelectedDataKey.Value.ToString();

                //    dt = GetDataTableRankException(false);
                //    DataRow dr = dt.Rows.Find(iOrder);

                //    VendorMaintenanceBLL.DeleteHotelHotelBranchRankException(dr["BranchRankExceptID"].ToString(), GlobalCode.Field2String(Session["UserName"]));

                //    dt.Rows.Remove(dr);

                //    Cache.Insert("HotelBranchRankException", dt, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(5));
                //    uoGridViewRanks.DataSource = dt;
                //    uoGridViewRanks.Columns[0].Visible = true;
                //    uoGridViewRanks.Columns[1].Visible = true;
                //    uoGridViewRanks.DataBind();
                //    uoGridViewRanks.Columns[0].Visible = false;
                //    uoGridViewRanks.Columns[1].Visible = false;
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //finally
                //{
                //    if (dt != null)
                //    {
                //        dt.Dispose();
                //    }
                //}            
        }

        /// Date Created:   17/02/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Audit trail logs
        protected void HotelVendorBranchLogAuditTrail()
        {
            string strLogDescription = "";
            string strFunction = "";

            if (Request.QueryString["vmId"].ToString() != "0")
            {
                strLogDescription = "Edit linkbutton for hotel vendor branch editor clicked.";
            }
            else
            {
                strLogDescription = "Add button for hotel vendor branch editor clicked.";
            }

            strFunction = "HotelVendorBranchLogAuditTrail";

            DateTime dateNow = CommonFunctions.GetCurrentDateTime();

            AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, Path.GetFileName(Request.UrlReferrer.AbsolutePath),
                                                  CommonFunctions.GetDateTimeGMT(dateNow), DateTime.Now, GlobalCode.Field2String(Session["UserName"]));
        }
        
        private void BindIMSHotel(List<IMSVendor> listIMSVendor)
        {
            uoDropDownListIMSVendor.Items.Clear();
            uoDropDownListIMSVendor.Items.Add(new ListItem("--Select IMS Vendor--","0"));

            //List<IMSVendor> list = new List<IMSVendor>();

            if (listIMSVendor.Count > 0)
            {
                uoDropDownListIMSVendor.DataSource = listIMSVendor;
                uoDropDownListIMSVendor.DataTextField = "sVendorNameWithId";
                uoDropDownListIMSVendor.DataValueField = "iVendorID";
            }
            uoDropDownListIMSVendor.DataBind();
        }

        /// <summary>
        /// Date Modified:  04/Jan/2018
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
       
    }
}

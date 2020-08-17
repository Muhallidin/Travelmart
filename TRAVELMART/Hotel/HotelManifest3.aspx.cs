using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using PDF = iTextSharp;
using System.IO;
using System.Text;

namespace TRAVELMART.Hotel
{
    public partial class HotelManifest3 : System.Web.UI.Page
    {
        LockedManifestBLL lockedBLL = new LockedManifestBLL();
        string strEmailTo;
        string strEmailCc;

        #region EVENTS
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// ------------------------------
        /// Modified by: Charlene Remotigue
        /// Date MOdified: 15/03/2012
        /// Description: Change TravelMartVariable to Session
        /// ------------------------------
        /// Modified by:    Josephine Gad
        /// Date MOdified:  09/07/2012
        /// Description:    remove uoBtnView_Click(sender, e); use Request.QueryString["chDate"] to refresh the page list if needed
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
                uoHiddenFieldDate.Value = Request.QueryString["dt"];
                Session["DateFrom"] = uoHiddenFieldDate.Value;
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                LoadValues();
                LoadEmailAddress(uoHiddenBranch.Value);
                uoHyperLinkSendEmail.HRef = "~/Hotel/HotelEmailManifest.aspx?bId=" + uoHiddenBranch.Value;

                uoListLockedManifest.DataSource = null;
                uoListLockedManifest.DataBind();

                if (Request.QueryString["chDate"] != null)
                {
                    if ((GlobalCode.Field2String(Session["Hotel"]) != "" || GlobalCode.Field2String(Session["Hotel"]) != "0") &&
                        (GlobalCode.Field2String(Session["ManifestHrs"]) != "" || GlobalCode.Field2String(Session["ManifestHrs"]) != "0"))
                    {
                        SetLockedManifestDetails();
                        if (GlobalCode.Field2Int(Session["LockedManifestClass_LockedManifestCount"]) > 0)
                        {
                            uoLblManifestHeader.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Locked By: " + Session["LockedManifestClass_LockedManifestLockedBy"].ToString();
                            uoLblManifestHeader.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Date: " + Session["LockedManifestClass_LockedManifestLockedDate"].ToString();
                        }
                    }
                }
            }
            else
            {
                uoHiddenFieldLoadType.Value = "1";
                if (Session["DateFrom"] != null && GlobalCode.Field2String(Session["DateFrom"]) != "")
                {
                    uoHiddenFieldDate.Value = GlobalCode.Field2String(Session["DateFrom"]);
                }
                else
                {
                    uoHiddenFieldDate.Value = Request.QueryString["dt"];
                }
                HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");
                if (uoHiddenFieldPopupCalendar.Value == "1" ) 
                {
                    SetLockedManifestDetails();
                    if (GlobalCode.Field2Int(Session["LockedManifestClass_LockedManifestCount"]) > 0)
                    {
                        uoLblManifestHeader.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Locked By: " + Session["LockedManifestClass_LockedManifestLockedBy"].ToString();
                        uoLblManifestHeader.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Date: " + Session["LockedManifestClass_LockedManifestLockedDate"].ToString();
                    }
                }
            }
            
            if (uoHiddenFieldPopupSendEmail.Value == "1")
            {
                strEmailTo = (string)(Session["EmailTo"]);
                strEmailCc = (string)(Session["EmailCc"]);

                uoBtnSendEmail_Click(sender, e);
            }
            uoHiddenFieldPopupSendEmail.Value = "0";
        }

        protected void uoBtnView_Click(object sender, EventArgs e)
        {
            Session.Remove("TentativeManifestCalendarDashboard");
            Session["Hotel"] = uoDropDownListBranch.SelectedValue;
            Session["ManifestHrs"] = uoDropDownListManifest.SelectedValue;
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            uoDropDownListCompare.SelectedValue = "-1";
            SetLockedManifestDetails();
            if (GlobalCode.Field2Int(Session["LockedManifestClass_LockedManifestCount"]) > 0)
            {
                uoLblManifestHeader.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Locked By: " + Session["LockedManifestClass_LockedManifestLockedBy"].ToString();
                uoLblManifestHeader.Text += "&nbsp;&nbsp;&nbsp;&nbsp;Date: " + Session["LockedManifestClass_LockedManifestLockedDate"].ToString();
            }
        }
        protected void uoButtonBack_Click(object sender, EventArgs e)
        {
            string URLString = "HotelLockedManifest.aspx?ufn=" + Request.QueryString["ufn"] + "&dt=" + uoHiddenFieldDate.Value;
            Response.Redirect(URLString);
        }
        protected void uoBtnCompare_Click(object sender, EventArgs e)
        {
            uoDropDownListCompare.SelectedValue = uoHIddenFieldCompareType.Value;
            SetManifestDifferenceDetails();
        }
        protected void uoBtnSearch_Click(object sender, EventArgs e)
        {
            SearchLockedManifest();
        }
        protected void uoBtnSendEmail_Click(object sender, EventArgs e)
        {
            getEmail();
            CreateEmail();
            AlertMessage("E-mail sent.");
        }

        /// <summary>
        /// Author:       Gabriel Oquialda
        /// Date Created: 17/04/2012
        /// Description:  Export hotel locked manifest list 
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestEmail to Session
        /// ---------------------------------------------------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoBtnExportList_Click(object sender, EventArgs e)
        {
            getEmail();
            List<LockedManifestEmail> list = new List<LockedManifestEmail>();
            list = (List<LockedManifestEmail>)Session["LockedManifestClass_LockedManifestEmail"];
           
            if (list.Count > 0)
            {
                ExportHotelLockedManifest();
            }
            else
            {
                AlertMessage("There are no locked manifest to export.");
            }
        }

        /// <summary>
        /// Created by:     Jefferson Bermundo
        /// Modified Date:  07/16/2012
        /// Description:    Bind Port Based on Region, 
        ///                 clear the Hotel session and Port Session Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoDropDownListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Region"] = uoDropDownListRegion.SelectedValue;
            Session["Hotel"] = "";
            Session["HotelNameToSearch"] = "";
            Session.Remove("Port"); // remove the current selected Port 05/07/2012
            BindPortList(false);
            GetHotelFilter();
        }

        /// <summary>
        /// Created by:     Jefferson Bermundo
        /// Modified Date:  07/16/2012
        /// Description:    Bind Hotel Based on Port,
        ///                 Remove Hotel Session Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void uoDropDownListPortPerRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Port"] = uoDropDownListPortPerRegion.SelectedValue;
            GetHotelFilter();
        }
        #endregion

        #region METHODS

        #region Values
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load default values
        /// ------------------------------------
        /// Modified By:    Josephine gad
        /// Date Modified:  04/10/2012
        /// Description:    User BindRegionList to bind Region List
        /// ------------------------------------
        /// </summary>
        protected void LoadValues()
        {
            lockedBLL.LoadLockedManifestPage(GlobalCode.Field2String(Session["UserName"]), GlobalCode.Field2DateTime(Session["DateFrom"]),
                GlobalCode.Field2Int(Session["Region"]));
            LoadManifestType();
            LoadNationality();
            LoadVessel();
            LoadRank();
            SetDefaultValues();

            BindRegionList();
            BindPortList(true);
        }

        /// <summary>
        /// Date Created:   27/03/2012
        /// Created By:     Gabriel Oquialda
        /// (description)   Load e-mail address
        /// </summary>
        protected void LoadEmailAddress(string BranchId)
        {
            IDataReader dr = null;
            try
            {
                dr = EmailBLL.LoadEmailAddress(BranchId);

                if (dr.Read())
                {
                    strEmailTo = dr["colEmailToVarchar"].ToString();
                    strEmailCc = dr["colEmailCcVarchar"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest type
        /// ------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.ManifestType to Session["LockedManifestClass_ManifestType"]
        /// </summary>
        protected void LoadManifestType()
        {
            ListItem item = new ListItem("--Select Manifest Type--", "0");
            uoDropDownListManifest.Items.Clear();
            uoDropDownListManifest.Items.Add(item);
            uoDropDownListManifest.DataSource = (List<ManifestClass>)Session["LockedManifestClass_ManifestType"];//LockedManifestClass.ManifestType;
            uoDropDownListManifest.DataTextField = "ManifestName";
            uoDropDownListManifest.DataValueField = "ManifestType";
            uoDropDownListManifest.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load vessel type
        /// -----------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.Vessel to Session["LockedManifestClass_Vessel"]
        /// </summary>
        protected void LoadVessel()
        {
            ListItem item = new ListItem("--Select Ship--", "0");
            uoDropDownListVessel.Items.Clear();
            uoDropDownListVessel.Items.Add(item);
            uoDropDownListVessel.DataSource = (List<Vessel>)Session["LockedManifestClass_Vessel"];//LockedManifestClass.Vessel;
            uoDropDownListVessel.DataTextField = "VesselName";
            uoDropDownListVessel.DataValueField = "VesselId";
            uoDropDownListVessel.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load Nationality
        /// -----------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.SFNationality to Session["LockedManifestClass_SFNationality"]
        /// </summary>
        protected void LoadNationality()
        {
            ListItem item = new ListItem("--Select Nationality--", "");
            uoDropDownListNationality.Items.Add(item);
            uoDropDownListNationality.DataSource = (List<SFNationality>)Session["LockedManifestClass_SFNationality"];//LockedManifestClass.SFNationality;
            uoDropDownListNationality.DataTextField = "Nationality";
            uoDropDownListNationality.DataValueField = "Nationality";
            uoDropDownListNationality.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load Rank
        /// -----------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.SFRank to Session["LockedManifestClass_SFRank"]
        /// </summary>
        protected void LoadRank()
        {
            ListItem item = new ListItem("--Select Rank--", "");
            uoDropDownListRank.Items.Add(item);
            uoDropDownListRank.DataSource = (List<SFRank>)Session["LockedManifestClass_SFRank"];//LockedManifestClass.SFRank;
            uoDropDownListRank.DataTextField = "RankName";
            uoDropDownListRank.DataValueField = "RankName";
            uoDropDownListRank.DataBind();
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: set default values
        /// ------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  03/10/2012
        /// Description:    Set default value of hotel if -1 is selected at first load
        /// </summary>
        protected void SetDefaultValues()
        {
            GetHotelFilter();
            if (Session["Hotel"] != null && GlobalCode.Field2String(Session["Hotel"]) != ""
                    && GlobalCode.Field2String(Session["Hotel"]) != "0")
            {
                uoDropDownListBranch.SelectedValue = GlobalCode.Field2Int(Session["Hotel"]).ToString();
                uoDropDownListManifest.SelectedValue = GlobalCode.Field2Int(Session["ManifestHrs"]).ToString();
                uoHiddenBranch.Value = GlobalCode.Field2Int(Session["Hotel"]).ToString();
                uoHiddenFieldManifest.Value = GlobalCode.Field2Int(Session["ManifestHrs"]).ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString();
                uoChkBoxPDF.Enabled = true;
                //EventArgs e = new EventArgs();
                //uoBtnView_Click(uoBtnView, e);                
            }
            else if (uoDropDownListBranch.SelectedValue != "0")
            {
                Session["Hotel"] = GlobalCode.Field2Int(uoDropDownListBranch.SelectedValue);
                Session["ManifestHrs"] = uoDropDownListManifest.SelectedValue;
                uoHiddenBranch.Value = GlobalCode.Field2Int(Session["Hotel"]).ToString();
                uoHiddenFieldManifest.Value = GlobalCode.Field2Int(Session["ManifestHrs"]).ToString();
                uoHiddenFieldUser.Value = GlobalCode.Field2String(Session["UserName"]);
                uoHiddenFieldDate.Value = GlobalCode.Field2DateTime(Session["DateFrom"]).ToString();
                uoChkBoxPDF.Enabled = true;
                
                //EventArgs e = new EventArgs();
                //uoBtnView_Click(uoBtnView, e);                
            }
            else
            {
                uoChkBoxPDF.Enabled = false;
            }
            uoDropDownListGender.SelectedIndex = 0;
            uoDropDownListNationality.SelectedIndex = 0;
            uoDropDownListRank.SelectedIndex = 0;
            uoDropDownListStatus.SelectedIndex = 0;
            uoDropDownListVessel.SelectedIndex = 0;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest type
        /// ---------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.ManifestType to List<ManifestClass> list, Session["LockedManifestClass_ManifestType"]
        /// </summary>
        protected void LoadCompareType()
        {
            ListItem item = new ListItem("--Select Manifest Type--", "-1");

            List<ManifestClass> list = new List<ManifestClass>();
            if (Session["LockedManifestClass_ManifestType"] != null)
            {
                list = (List<ManifestClass>)Session["LockedManifestClass_ManifestType"];
            }
            else
            {
                ManifestClass listItem = new ManifestClass();
                for (int x = 1; x < uoDropDownListManifest.Items.Count; x++)
                {
                    listItem.ManifestName = uoDropDownListManifest.Items[x].Text;
                    listItem.ManifestType = GlobalCode.Field2Int(uoDropDownListManifest.Items[x].Value);
                    list.Add(listItem);
                }
            }
            var CompareManifest = (from a in list//LockedManifestClass.ManifestType
                                   where a.ManifestType != Int32.Parse(uoDropDownListManifest.SelectedValue)
                                   select new
                                   {
                                       CompareId = a.ManifestType,
                                       CompareName = a.ManifestName,
                                   }).ToList();

            uoDropDownListCompare.Items.Clear();
            uoDropDownListCompare.Items.Add(item);
            item = new ListItem("Current Transactions", "0");
            uoDropDownListCompare.Items.Add(item);
            uoDropDownListCompare.DataSource = CompareManifest;
            uoDropDownListCompare.DataTextField = "CompareName";
            uoDropDownListCompare.DataValueField = "CompareId";
            uoDropDownListCompare.DataBind();
        }
        #endregion

        #region Locked
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load locked manifest details
        /// ----------------------------------------------
        /// Modified By:     Josephine Gad
        /// Date Modified:   17/05/2012
        /// Description:     remove lockedBLL.LoadLockedManifest2 and use BindLockedManifest to reload uoListLockedManifest
        ///                  use uoHiddenFieldLoadType
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  27/09/2012
        /// Description:    Set the default value of compared manifest if no data selected in compared type
        /// </summary>
        protected void SetLockedManifestDetails()
        {
            uoHiddenBranch.Value = GlobalCode.Field2Int(Session["Hotel"]).ToString();

            if (uoDropDownListBranch.SelectedValue != "0")
            {
                uoDropDownListCompare.Visible = true;
                uoBtnCompare.Visible = true;
                uoLblCompare.Visible = true;
            }
            else
            {
                uoDropDownListCompare.Visible = false;
                uoBtnCompare.Visible = false;
                uoLblCompare.Visible = false;
            }

            uoLblManifestHeader.Text="";
            if (uoDropDownListBranch.SelectedValue == "-1")
            {
                uoDropDownListPortPerRegion.SelectedValue = "0";
                Session["Port"] = "0";
                uoDropDownListPortPerRegion.Enabled = false;

                uoDropDownListCompare.Visible = false;
                uoBtnCompare.Visible = false;
                uoLblCompare.Visible = false;
            }
            else
            {
                uoDropDownListPortPerRegion.Enabled = true;
            }

            if (uoDropDownListManifest.SelectedValue != "0" && uoDropDownListManifest.SelectedValue != "-1")
            {
                uoLblManifestHeader.Text = uoDropDownListManifest.SelectedItem.Text + " for "
                    + String.Format("{0:dd-MMM-yyyy}", GlobalCode.Field2DateTime(Session["DateFrom"]));
            }
            LoadCompareType();

            string sfName = "";
            int sfId = 0;
            if (uoDropDownListFilter.SelectedValue == "0")
            {
                sfName = uoTextBoxFilter.Text;
            }
            else
            {
                if (uoTextBoxFilter.Text != "")
                {
                    sfId = GlobalCode.Field2Int(uoTextBoxFilter.Text);
                }
            }
            uoHiddenFieldLoadType.Value = "0";
            BindLockedManifest();

            if (GlobalCode.Field2Int(Session["LockedManifestClass_LockedManifestCount"]) > 0)
            {
                uoChkBoxPDF.Enabled = true;
                uoBtnSendEmail.Enabled = true;
                uoHyperLinkSendEmail.Attributes.Remove("disabled");
                uoChkBoxSendDiff.Enabled = false;
            }
            else
            {
                uoChkBoxPDF.Enabled = false;
                uoBtnSendEmail.Enabled = false;
                uoHyperLinkSendEmail.Attributes.Add("disabled", "disabled");
                uoChkBoxSendDiff.Enabled = false;
            }

            if (uoDropDownListCompare.SelectedValue == "-1")
            {
                uoDifferenceList.DataSourceID = "";
                uoDifferenceList.DataSource = null;
                uoDifferenceList.DataBind();
                uoLblCompareHeader.Text = "";

                uoDifferenceList.Visible = false;
                uoDifferenceListPager.Visible = false;
            }
        }
        /// Author:         Josephine Gad
        /// Date Created:   17/05/2012
        /// Description:    use DataSourceID to reload uoListLockedManifest
        private void BindLockedManifest()
        {
            //uoListLockedManifest.DataSource = null;
            uoListLockedManifest.DataSourceID = "ObjectDataSource1";
            uoListLockedManifest.DataBind();
        }
        #endregion

        #region Difference
        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 24/02/2012
        /// Description: set manifest difference details
        /// --------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestDifferenceWithRows to Session["LockedManifestClass_LockedManifestDifferenceWithRows"]
        ///                 Change LockedManifestClass.LockedManifestDifferenceWithRowsCount to Session["LockedManifestClass_LockedManifestDifferenceWithRowsCount"]
        ///                 Delete LockedManifestClass.LockedManifestDifference because it has no use
        /// --------------------------------------------
        /// </summary>
        protected void SetManifestDifferenceDetails()
        {
            //LockedManifestClass.LockedManifestDifference = null;
            //LockedManifestClass.LockedManifestDifferenceWithRows = null;
            //LockedManifestClass.LockedManifestDifferenceWithRowsCount = null;

            Session.Remove("LockedManifestClass_LockedManifestDifferenceWithRows");
            Session.Remove("LockedManifestClass_LockedManifestDifferenceWithRowsCount");

            uoLblCompareHeader.Text = uoDropDownListManifest.SelectedItem.Text + "(" +
                    uoLblManifestHeader.Text.Replace(uoDropDownListManifest.SelectedItem.Text + " for " +
                    String.Format("{0:dd-MMM-yyyy}", GlobalCode.Field2DateTime(Session["DateFrom"]))
                    + "&nbsp;&nbsp;&nbsp;&nbsp;", "") +
                    ") vs. " + uoDropDownListCompare.SelectedItem.Text;

            string lockedBy = "", lockedDate = "";
            lockedBLL.LoadManifestDifference(uoHiddenFieldUser.Value, 0, DateTime.Parse(uoHiddenFieldDate.Value),
                Int32.Parse(uoDropDownListManifest.SelectedValue), Int32.Parse(uoDropDownListCompare.SelectedValue),
                Int32.Parse(uoDropDownListBranch.SelectedValue),ref lockedBy, ref lockedDate);
            if (!string.IsNullOrEmpty(lockedBy) || !string.IsNullOrEmpty(lockedDate))
            {
                uoLblCompareHeader.Text += "(Locked By:" + lockedBy + "&nbsp;&nbsp;&nbsp;&nbsp;Date: " + lockedDate + ")";
            }
            uoDifferenceList.Visible = true;
            uoDifferenceListPager.Visible = true;

            uoDifferenceList.DataSourceID = ObjectDataSource2.UniqueID;
            if (GlobalCode.Field2Int(Session["LockedManifestClass_LockedManifestDifferenceWithRowsCount"]) > 0)
            {
                uoChkBoxSendDiff.Enabled = true;
            }
            else
            {
                uoChkBoxSendDiff.Enabled = false;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest difference list
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modoified: 04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestDifferenceWithRows to Session
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public List<LockedManifestDifference> GetDifferenceClass(int startRowIndex, int maximumRows)
        {
            List<LockedManifestDifferenceWithRows> list = new List<LockedManifestDifferenceWithRows>();
            list = (List<LockedManifestDifferenceWithRows>)Session["LockedManifestClass_LockedManifestDifferenceWithRows"];

            int lastRow = startRowIndex + maximumRows;
            var locked = (from a in list//LockedManifestClass.LockedManifestDifferenceWithRows
                          where a.RowNum >= startRowIndex + 1 &&
                          a.RowNum <= lastRow
                          select new LockedManifestDifference
                          {
                              IdBigInt = a.IdBigInt,
                              E1TravelReqId = a.E1TravelReqId,
                              TravelReqId = a.TravelReqId,
                              ManualReqId = a.ManualReqId,
                              Couple = a.Couple,
                              Gender = a.Gender,
                              Nationality = a.Nationality,
                              CostCenter = a.CostCenter,
                              CheckIn = a.CheckedIn,
                              CheckOut = a.CheckedOut,
                              LastName = a.LastName,
                              FirstName = a.FirstName,

                              EmployeeId = a.EmployeeId,
                              Ship = a.Ship,
                              HotelRequest = a.HotelRequest,
                              SingleDouble = a.RoomType,
                              Title = a.Title,
                              HotelCity = a.HotelCity,
                              HotelNights = a.HotelNights,
                              FromCity = a.FromCity,
                              ToCity = a.ToCity,
                              AL = a.Airline,
                              RecordLocator = a.RecordLocator,
                              PassportNo = a.PassportNo,
                              IssuedDate = a.IssuedDate,
                              PassportExpiration = a.PassportExpiration,
                              DeptDate = a.DeptDate,
                              ArvlDate = a.ArvlDate,
                              DeptCity = a.DeptCity,
                              ArvlCity = a.ArvlCity,
                              Carrier = a.Carrier,
                              FlightNo = a.FlightNo,
                              OnOffDate = a.OnOffDate,
                              Voucher = a.Voucher,
                              TravelDate = a.TravelDate,
                              Reason = a.Reason,
                              Status = a.Status,
                              isDeleted = a.isDeleted,

                              QueryRemarks = a.QueryRemarks,
                              QueryRemarksID = a.QueryRemarksID
                          }).ToList();

            return locked;
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 23/02/2012
        /// Description: load manifest difference count
        /// ---------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modoified: 04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestDifferenceWithRowsCount to Session
        /// </summary>
        /// <returns></returns>
        public int GetDifferenceClassCount()
        {
            return GlobalCode.Field2Int(Session["LockedManifestClass_LockedManifestDifferenceWithRowsCount"]);// LockedManifestClass.LockedManifestDifferenceWithRowsCount;
        }

        public string SetValue()
        {
            bool isDeleted = Convert.ToBoolean(Eval("isDeleted"));

            if (isDeleted)
            {
                return "text-decoration:line-through; color:Red";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region SEARCH
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  17/05/2012
        /// Description:    comment the LoadLockedManifestPaging2 and call function BindLockedManifest
        /// </summary>
        protected void SearchLockedManifest()
        {
            string sfName = "";
            int sfId = 0;
            if (uoDropDownListFilter.SelectedValue == "0")
            {
                sfName = uoTextBoxFilter.Text;
            }
            else
            {
                if (uoTextBoxFilter.Text != "")
                {
                    sfId = GlobalCode.Field2Int(uoTextBoxFilter.Text);
                }
            }
            uoHiddenFieldName.Value = sfName;
            uoHiddenFieldId.Value = sfId.ToString();
            uoHiddenFieldGender.Value = uoDropDownListGender.SelectedValue;
            uoHiddenFieldNationality.Value = uoDropDownListNationality.SelectedValue;
            uoHiddenFieldRank.Value = uoDropDownListRank.SelectedValue;
            uoHiddenFieldStatus.Value = uoDropDownListStatus.SelectedValue;
            uoHiddenFieldVessel.Value = uoDropDownListVessel.SelectedValue;

            BindLockedManifest();

            //lockedBLL.LoadLockedManifestPaging2(uoHiddenFieldUser.Value, 1, DateTime.Parse(uoHiddenFieldDate.Value), Int32.Parse(uoHiddenFieldManifest.Value),
            //     Int32.Parse(uoHiddenBranch.Value), Int32.Parse(uoHiddenFieldVessel.Value), uoHiddenFieldRank.Value, sfName, uoHiddenFieldNationality.Value, sfId, uoHiddenFieldGender.Value, uoHiddenFieldStatus.Value,
            //     uoListLockedManifestPager.StartRowIndex, 25);

            ////uoListLockedManifest.Items.Clear();
            //ObjectDataSource1.TypeName = "TRAVELMART.Common.LockedManifestClass";
            //ObjectDataSource1.SelectCountMethod = "GetLockedManifestCount";
            //ObjectDataSource1.SelectMethod = "GetLockedManifest";
            //uoListLockedManifest.DataSourceID = ObjectDataSource1.UniqueID;
            //Session["Hotel"] = uoHiddenBranch.Value;
            //Session["ManifestHrs"] = uoHiddenFieldManifest.Value;
        }
        #endregion

        #region EMAIL
        protected void getEmail()
        {
            int LoadType = 0;
            if (uoChkBoxSendDiff.Checked)
            {
                LoadType = 1;
            }

            lockedBLL.LoadManifestEmailList(uoHiddenFieldUser.Value, LoadType, GlobalCode.Field2Int(uoDropDownListManifest.SelectedValue),
                GlobalCode.Field2Int(uoHiddenBranch.Value), GlobalCode.Field2Int(uoDropDownListCompare.SelectedValue), 
                GlobalCode.Field2DateTime(Session["DateFrom"]),
                GlobalCode.Field2Int(uoDropDownListRegion.SelectedValue));
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Modified Date:  22/07/2012
        /// Description:    Add FileNameEmail. Selected Manifest and comparison is now in single excel
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestEmail to list from Session
        ///                 Change LockedManifestClass.ManifestDifferenceEmail to listDiff Session
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  24/Jan/2013
        /// Description:    Add Manifest Date in File Name and Subject of email
        /// ---------------------------------------------------------
        /// </summary>
        protected void CreateEmail()
        {
            string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
            string sDateOnly = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM dd, yyyy");
            string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM_dd_yyy");

            string FilePath = Server.MapPath("~/Extract/HotelManifest/");
            string FileName = FilePath + "HotelManifest_" + sDateManifest + '_' + sDate + ".xls";
            string FileNameWithDiff = FilePath + "HotelManifestWithDiff_" + sDateManifest + '_' + sDate + ".xls";
            string FileNameDiff = "";
            string PDFFileName = FilePath + "HotelManifest_" + sDateManifest + '_' + sDate + ".pdf";
            string PDFFileNameDiff = "";

            string FileNameEmail;
            
            DataTable locked = new DataTable();
            DataTable diff = new DataTable();
            try
            {
                List<LockedManifestEmail> list = new List<LockedManifestEmail>();
                if (Session["LockedManifestClass_LockedManifestEmail"] != null)
                {
                    list = (List<LockedManifestEmail>)Session["LockedManifestClass_LockedManifestEmail"];
                }

                List<ManifestDifferenceEmail> listDiff = new List<ManifestDifferenceEmail>();
                if (Session["LockedManifestClass_ManifestDifferenceEmail"] != null)
                {
                    listDiff = (List<ManifestDifferenceEmail>)Session["LockedManifestClass_ManifestDifferenceEmail"];
                }

                locked = getDataTable(list);
                if (uoChkBoxPDF.Checked)
                {
                    CreatePDF(sDateOnly, PDFFileName, locked);
                    if (uoChkBoxSendDiff.Checked)
                    {
                        PDFFileNameDiff = FilePath + "HotelManifestDifference_" + sDateManifest + '_' + sDate + ".pdf";
                        diff = getDataTable(listDiff);
                        CreatePDF(sDateOnly, PDFFileNameDiff, diff);

                        //strFileDiffName = FilePath + PDFFileNameDiff;
                        //OpenExcelFile(PDFFileName, strFileDiffName);
                    }


                    //ConvertExcelToPDF(FilePath, sDate, PDFFileName, dt);
                    string sMsg = "Please find attached " + uoDropDownListBranch.SelectedItem.Text + " for " +
                        sDateOnly + ".<br/><br/>Thank you.";

                    string sSubject = "Travelmart: Hotel Manifest " + uoDropDownListBranch.SelectedItem.Text + 
                        " " + sDateOnly;

                    ManifestSendEmail(sSubject, sMsg, PDFFileName, PDFFileNameDiff, strEmailTo, strEmailCc, //LockedManifestClass.HotelEmail[0].EmailAddress
                        (PDFFileName + ";" + PDFFileNameDiff).TrimEnd(';'), uoDropDownListBranch.SelectedItem.Text);

                    //strFileName = FilePath + PDFFileName;
                    //OpenExcelFile(PDFFileName, strFileName);
                }
                else
                {
                    if (uoChkBoxSendDiff.Checked)
                    {
                        //FileNameDiff = FilePath + "HotelManifestDifference_" + sDate + ".xls";
                        FileNameEmail = FileNameWithDiff;
                        diff = getDataTable(listDiff);
                        //CreateExcel(diff, FileNameDiff);
                        CreateExcel(locked, FileNameWithDiff, diff);
                    }
                    else
                    {
                        FileNameEmail = FileName;
                        CreateExcel(locked, FileName, null);
                    }
                    string sMsg = "Please find attached " + uoDropDownListManifest.SelectedItem.Text + " for " +
                        sDateOnly + ".<br/><br/>Thank you.";
                    
                    string sSubject = "Travelmart: Hotel Manifest " + uoDropDownListBranch.SelectedItem.Text +
                        " " + sDateOnly;

                    ManifestSendEmail(sSubject, sMsg, FileNameEmail, FileNameDiff, strEmailTo, strEmailCc, //LockedManifestClass.HotelEmail[0].EmailAddress,
                        (FileNameEmail + ";").TrimEnd(';'), uoDropDownListBranch.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                AlertMessage("E-mail sending failed.");
            }
            finally
            {
                if (locked != null)
                {
                    locked.Dispose();
                }
                if (diff != null)
                {
                    diff.Dispose();
                }
            }
        }

        /// <summary>
        /// Date Created:   14/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Show pop up message            
        /// </summary>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            
            sScript += "alert( msg );";
            sScript += "</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "scr", sScript, false);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        /// <summary>        
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create PDF file based from Datatable      
        /// </summary>
        /// <param name="sDateOnly">Date with correct format to be included in PDF file</param>
        /// <param name="PDFFileName">PDF filename to be used</param>
        /// <param name="dt">Datatable to convert</param>
        private void CreatePDF(string sDateOnly, string PDFFileName, DataTable dt)
        {
            //HttpResponse Response = HttpContext.Current.Response;

            //Response.Clear();            

            //Response.ContentType = "application/octet-stream";            
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + PDFFileName);

            // step 1: creation of a document-object            
            PDF.text.Document document = new PDF.text.Document(PDF.text.PageSize.A2.Rotate(), 5, 5, 50, 10);

            // step 2: we create a writer that listens to the document            
            PDF.text.pdf.PdfWriter.GetInstance(document, new FileStream(PDFFileName, FileMode.Create));

            //set some header stuff
            //document.AddTitle(name);
            //document.AddSubject("Table of " + name);
            //document.AddCreator("This Application");
            //document.AddAuthor("Me");

            // we Add a Header that will show up on PAGE 1
            //Phrase phr = new Phrase(""); //empty phrase for page numbering
            //HeaderFooter footer = new HeaderFooter(phr, true);
            //document.Footer = footer;

            // step 3: we open the document
            document.Open();

            // step 4: we add content to the document
            CreatePages(document, dt, sDateOnly);

            // step 5: we close the document
            document.Close();

            //memoryStream.Position = 0;
        }
        /// <summary>
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create PDF pages
        /// </summary>
        /// <param name="document"></param>
        /// <param name="dt"></param>
        /// <param name="sDateOnly"></param>
        public void CreatePages(PDF.text.Document document, DataTable dt, string sDateOnly)
        {
            if (dt.Rows.Count > 0)
            {
                bool first = true;
                if (first)
                    first = false;
                else
                    document.NewPage();

                int iColCount = 33;

                document.Add(FormatHeaderPhrase(dt.Rows[0]["HotelBranch"].ToString() + ": " + uoDropDownListManifest.SelectedItem.Text + " (" + sDateOnly + ")"));
                iTextSharp.text.pdf.PdfPTable pdfTable = new iTextSharp.text.pdf.PdfPTable(dt.Columns.Count - 1); //-2
                pdfTable.DefaultCell.Padding = 1;
                pdfTable.WidthPercentage = 100; // percentage
                pdfTable.DefaultCell.BorderWidth = 1;
                //pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_CENTER;
                pdfTable.DefaultCell.HorizontalAlignment = PDF.text.Element.ALIGN_LEFT;

                int iCol = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    if (iCol <= iColCount)
                    {
                        pdfTable.AddCell(FormatHeaderTable(column.ColumnName));
                    }
                    iCol++;
                }

                pdfTable.HeaderRows = 1;  // this is the end of the table header
                pdfTable.DefaultCell.BorderWidth = 1;

                PDF.text.BaseColor altRow = new PDF.text.BaseColor(242, 242, 242);

                string Gender;
                string RoomType;

                double Total = 0;
                double TotalMale = 0;
                double TotalFemale = 0;
                double TotalMaleSingle = 0;
                double TotalMaleDouble = 0;
                double TotalFemaleSingle = 0;
                double TotalFemaleDouble = 0;

                Int32 MaleSingle = 0;
                Int32 MaleDouble = 0;
                Int32 FemaleSingle = 0;
                Int32 FemaleDouble = 0;

                Int32 MaleSDTotal = 0;
                Int32 FemaleSDTotal = 0;

                int i = 0;
                int iRow = 0;

                //Row
                foreach (DataRow row in dt.Rows)
                {

                    i++;
                    if (i % 2 == 1)
                    {
                        pdfTable.DefaultCell.BackgroundColor = altRow;
                    }
                    //Each column of row
                    iCol = 1;
                    foreach (object cell in row.ItemArray)
                    {
                        if (iCol <= iColCount)
                        {    //assume toString produces valid output
                            pdfTable.AddCell(FormatPhrase(cell.ToString()));
                        }
                        iCol++;
                    }
                    if (i % 2 == 1)
                    {
                        pdfTable.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                    }

                    Gender = dt.Rows[iRow]["Gender"].ToString();
                    RoomType = dt.Rows[iRow]["SingleDouble"].ToString();
                    // Gender count					
                    if (Gender == "M" || Gender == "Male")
                    {
                        if (RoomType == "1")
                        {
                            TotalMaleSingle = TotalMaleSingle + 1;
                            MaleSingle = MaleSingle + 1;
                        }
                        else
                        {
                            TotalMaleDouble = TotalMaleDouble + .5;
                            MaleDouble = MaleDouble + 1;
                        }
                        TotalMale = TotalMaleSingle + TotalMaleDouble;
                    }
                    else
                    {
                        if (RoomType == "1")
                        {
                            TotalFemaleSingle = TotalFemaleSingle + 1;
                            FemaleSingle = FemaleSingle + 1;
                        }
                        else
                        {
                            TotalFemaleDouble = TotalFemaleDouble + .5;
                            FemaleDouble = FemaleDouble + 1;
                        }
                        TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                    }
                    //Total = Total + .5;
                    iRow++;
                }
                MaleSDTotal = MaleSingle + MaleDouble;
                FemaleSDTotal = FemaleSingle + FemaleDouble;
                Total = TotalMale + TotalFemale;

                //Border and background settings
                pdfTable.DefaultCell.Border = 0;
                pdfTable.DefaultCell.BackgroundColor = PDF.text.BaseColor.WHITE;
                pdfTable.DefaultCell.BorderColor = PDF.text.BaseColor.WHITE;
                //Blank Row
                iTextSharp.text.pdf.PdfPCell BlankRow = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(""));
                BlankRow.Colspan = iColCount;
                BlankRow.Border = 0;
                pdfTable.AddCell(BlankRow);

                //Male Room                               
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {

                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Room:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Room:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //lblSummary.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        //pdfTable.AddCell(FormatPhrase(TotalMale.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(TotalMale.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }

                //Female Room:               
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Room:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Room:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(TotalFemale.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(TotalFemale.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }

                //Total Room:               
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Total Room:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Total Room:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(Total.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(Total.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                pdfTable.AddCell(BlankRow);

                //Booked from prev Date:               
                double BookedInPrevDate = LockedManifestClass.BookedInPrevDateCount;
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Booked from Prev Date:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Booked from Prev Date:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(Total.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(BookedInPrevDate.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Total for the day:               
                Total = Total + BookedInPrevDate;
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Total Room for the Day:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Total Room for the Day:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(Total.ToString("0.#")));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(Total.ToString("0.#")));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                pdfTable.AddCell(BlankRow);

                //Male Single:               
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Single:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Single:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(MaleSingle.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(MaleSingle.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Male Double:               
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Double:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Double:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(MaleDouble.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(MaleDouble.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Male Single/Double Total:
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Single/Double Total:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Male Single/Double Total:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(MaleSDTotal.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(MaleSDTotal.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                pdfTable.AddCell(BlankRow);
                //Female Single:
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Single:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Single:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(FemaleSingle.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(FemaleSingle.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Female Double:
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Double:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Double:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(FemaleDouble.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(FemaleDouble.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                //Female Single/Double Total:
                for (int x = 1; x <= 33; x++) //32
                {
                    if (x == 10)
                    {
                        iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell();
                        //lblSummary.Colspan = 3;
                        lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Single/Double Total:"));
                        //iTextSharp.text.pdf.PdfPCell lblSummary = new iTextSharp.text.pdf.PdfPCell(FormatPhrase("Female Single/Double Total:"));                        
                        lblSummary.Border = 0;
                        pdfTable.AddCell(lblSummary);
                    }
                    else if (x == 11)
                    {
                        //pdfTable.AddCell(FormatPhrase(FemaleSDTotal.ToString()));
                        iTextSharp.text.pdf.PdfPCell valSummay = new iTextSharp.text.pdf.PdfPCell(FormatPhrase(FemaleSDTotal.ToString()));
                        valSummay.HorizontalAlignment = PDF.text.Element.ALIGN_RIGHT;
                        valSummay.Border = 0;
                        pdfTable.AddCell(valSummay);
                    }
                    else
                    {
                        pdfTable.AddCell(FormatPhrase(""));
                    }
                }
                document.Add(pdfTable);
            }
        }
        /// <summary>
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Format the phrase. Apply font and size here.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static PDF.text.Phrase FormatHeaderTable(string value)
        {
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6, PDF.text.Font.BOLD));
        }
        private static PDF.text.Phrase FormatPhrase(string value)
        {
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6));
        }
        private static PDF.text.Phrase FormatHeaderPhrase(string value)
        {
            return new PDF.text.Phrase(value, PDF.text.FontFactory.GetFont(PDF.text.FontFactory.HELVETICA, 6, PDF.text.Font.BOLD, new PDF.text.BaseColor(0, 0, 255)));
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
            if (t != null)
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        private void ManifestSendEmail(string sSubject, string sMessage, string attachment1, string attachment2,
            string EmailVendor, string EmailCc, string file, string Hotel)
        {
            string sBody;
            DataTable dt = null;
            try
            {

                sBody = "<TABLE style=\"font-family: Tahoma, Arial; font-size: 14px; width:100%\"><TR><TD>";
                sBody += "Dear " + Hotel + ", <br/><br/> " + sMessage;
                sBody += "</TR></TD></TABLE>";

                if (EmailVendor != "")
                {
                    string attachment = attachment1 + ";" + attachment2;
                    CommonFunctions.SendEmailWithAttachment("", EmailVendor, EmailCc, sSubject, sBody, attachment.TrimEnd(';'));
                }

                //Insert Email logs
                //CommonFunctions.InsertEmailLog(EmailVendor, EmailCc, "travelmart.ptc@gmail.com", sSubject, file, DateTime.Now, uoHiddenFieldUser.Value);

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
        /// Date Created:   01/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Create one Excel-XML-Document with SpreadsheetML from a DataTable
        /// </summary>
        /// <param name="dataSource">Datable which would be exported in Excel</param>
        /// <param name="fileName">Name of exported file</param>
        ///-------------------------------------------
        /// Modified By:    Jefferson S. Bermundo
        /// Date Modified:  16/07/2012
        /// Description:    Change Write Setting for EmployeeId to Number format,
        ///                 add comparison to check if the current column is an 
        ///                 EmployeeId and change format to number
        ///-------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  18/07/2012
        /// Description:    Add another sheet for comparison manifest
        ///-------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  06/Aug/2013
        /// Description:    validate cost center if numeric or not
        ///                 Add style S65  to align all rows to Left
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strFileName"></param>
        public void CreateExcel(DataTable dtSource, string strFileName, DataTable dtCompare)
        {
            // Create XMLWriter
            using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
            {
                int iColCount = 29;
                //Format the output file for reading easier
                xtwWriter.Formatting = Formatting.Indented;

                // <?xml version="1.0"?>
                xtwWriter.WriteStartDocument();

                // <?mso-application progid="Excel.Sheet"?>
                xtwWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

                // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
                xtwWriter.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

                //Write definition of namespace
                xtwWriter.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
                xtwWriter.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
                xtwWriter.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
                xtwWriter.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

                // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                xtwWriter.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

                // Write document properties
                xtwWriter.WriteElementString("Author", "Travelmart");
                xtwWriter.WriteElementString("LastAuthor", Environment.UserName);
                xtwWriter.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
                xtwWriter.WriteElementString("Company", "RCCL");
                xtwWriter.WriteElementString("Version", "1");

                // </DocumentProperties>
                xtwWriter.WriteEndElement();

                // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                xtwWriter.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

                // Write settings of workbook
                xtwWriter.WriteElementString("WindowHeight", "13170");
                xtwWriter.WriteElementString("WindowWidth", "17580");
                xtwWriter.WriteElementString("WindowTopX", "120");
                xtwWriter.WriteElementString("WindowTopY", "60");
                xtwWriter.WriteElementString("ProtectStructure", "False");
                xtwWriter.WriteElementString("ProtectWindows", "False");

                // </ExcelWorkbook>
                xtwWriter.WriteEndElement();

                // <Styles>
                xtwWriter.WriteStartElement("Styles");

                // <Style ss:ID="Default" ss:Name="Normal">
                xtwWriter.WriteStartElement("Style");
                xtwWriter.WriteAttributeString("ss", "ID", null, "Default");
                xtwWriter.WriteAttributeString("ss", "Name", null, "Normal");

                // <Alignment ss:Vertical="Bottom"/>
                xtwWriter.WriteStartElement("Alignment");
                xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                xtwWriter.WriteEndElement();

                // Write null on the other properties
                xtwWriter.WriteElementString("Borders", null);
                xtwWriter.WriteElementString("Font", null);
                xtwWriter.WriteElementString("Interior", null);
                xtwWriter.WriteElementString("NumberFormat", null);
                xtwWriter.WriteElementString("Protection", null);
                // </Style>
                xtwWriter.WriteEndElement();

                //Style for header
                xtwWriter.WriteStartElement("Style");
                //<Style ss:ID="s62">
                xtwWriter.WriteAttributeString("ss", "ID", null, "s62");
                xtwWriter.WriteStartElement("Font");
                // <Font ss:Bold="1"/>
                xtwWriter.WriteAttributeString("ss", "Bold", null, "1");
                //end of font
                xtwWriter.WriteEndElement();
                //End Style for header
                xtwWriter.WriteEndElement();


                //Style for total summary numbers
                xtwWriter.WriteStartElement("Style");
                //<Style ss:ID="s64">
                xtwWriter.WriteAttributeString("ss", "ID", null, "s64");
                xtwWriter.WriteStartElement("Alignment");
                xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Right");
                xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                xtwWriter.WriteEndElement();
                //End Style for header
                xtwWriter.WriteEndElement();


                //Style for Rows
                xtwWriter.WriteStartElement("Style");
                //<Style ss:ID="s64">
                xtwWriter.WriteAttributeString("ss", "ID", null, "s65");
                xtwWriter.WriteStartElement("Alignment");
                xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Left");
                xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
                xtwWriter.WriteEndElement();
                //End Style for Rows
                xtwWriter.WriteEndElement();


                // </Styles>
                xtwWriter.WriteEndElement();

                // <Worksheet ss:Name="xxx">
                xtwWriter.WriteStartElement("Worksheet");

                int iHotelLength;
                iHotelLength = dtSource.Rows[0]["HotelBranch"].ToString().Length;
                if (iHotelLength > 25)
                {
                    iHotelLength = 25;
                }
                xtwWriter.WriteAttributeString("ss", "Name", null, dtSource.Rows[0]["HotelBranch"].ToString().Substring(0,iHotelLength));

                // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                xtwWriter.WriteStartElement("Table");

                int iRow = dtSource.Rows.Count + 16;

                xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
                xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

                xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

                //Variables for computation
                string Gender;
                string RoomType;

                double Total = 0;
                double TotalMale = 0;
                double TotalFemale = 0;
                double TotalMaleSingle = 0;
                double TotalMaleDouble = 0;
                double TotalFemaleSingle = 0;
                double TotalFemaleDouble = 0;

                Int32 MaleSingle = 0;
                Int32 MaleDouble = 0;
                Int32 FemaleSingle = 0;
                Int32 FemaleDouble = 0;

                Int32 MaleSDTotal = 0;
                Int32 FemaleSDTotal = 0;

                //Header
                xtwWriter.WriteStartElement("Row");
                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
                int i = 1;
                foreach (DataColumn Header in dtSource.Columns)
                {
                    if (i <= iColCount)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        // xxx
                        xtwWriter.WriteStartElement("Data");

                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                       
                        // Write content of cell
                        xtwWriter.WriteValue(Header.ColumnName);
                        xtwWriter.WriteEndElement();
                        xtwWriter.WriteEndElement();
                    }
                    i++;
                }
                xtwWriter.WriteEndElement();


                // Run through all rows of data source
                foreach (DataRow row in dtSource.Rows)
                {
                    // <Row>
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s65");

                    i = 1;
                    // Run through all cell of current rows
                    foreach (object cellValue in row.ItemArray)
                    {
                        if (i <= iColCount)
                        {
                            // <Cell>
                            xtwWriter.WriteStartElement("Cell");

                            // <Data ss:Type="String">xxx</Data>
                            xtwWriter.WriteStartElement("Data");

                            if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                dtSource.Columns[i - 1].Caption.ToUpper() == "HOTELNIGHTS" ||
                                dtSource.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                dtSource.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                            {
                                xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                            }
                            else if (dtSource.Columns[i - 1].Caption.ToUpper() == "COSTCENTER")
                            {
                                if (GlobalCode.Field2Int(cellValue) > 0)
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                else
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                }
                            }
                            else
                            {
                                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            }
                            // Write content of cell
                            xtwWriter.WriteValue(cellValue);
                            //if (cellValue.ToString() != "")
                            //{
                            //    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            //    // Write content of cell
                            //    xtwWriter.WriteValue(cellValue);
                            //}
                            //else 
                            //{
                            //    xtwWriter.WriteAttributeString("Type", "");
                            //    // Write content of cell
                            //    xtwWriter.WriteValue("");
                            //}

                            // </Data>
                            xtwWriter.WriteEndElement();

                            // </Cell>
                            xtwWriter.WriteEndElement();
                        }
                        i++;
                    }
                    // </Row>
                    xtwWriter.WriteEndElement();

                    //Computation
                    Gender = row["Gender"].ToString();
                    RoomType = row["SingleDouble"].ToString();
                    // Gender count					
                    if (Gender == "M" || Gender == "Male")
                    {
                        if (RoomType == "1")
                        {
                            TotalMaleSingle = TotalMaleSingle + 1;
                            MaleSingle = MaleSingle + 1;
                        }
                        else
                        {
                            TotalMaleDouble = TotalMaleDouble + .5;
                            MaleDouble = MaleDouble + 1;
                        }
                        TotalMale = TotalMaleSingle + TotalMaleDouble;
                    }
                    else
                    {
                        if (RoomType == "1")
                        {
                            TotalFemaleSingle = TotalFemaleSingle + 1;
                            FemaleSingle = FemaleSingle + 1;
                        }
                        else
                        {
                            TotalFemaleDouble = TotalFemaleDouble + .5;
                            FemaleDouble = FemaleDouble + 1;
                        }
                        TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
                    }
                    //Total = Total + .5;
                }
                MaleSDTotal = MaleSingle + MaleDouble;
                FemaleSDTotal = FemaleSingle + FemaleDouble;
                Total = TotalMale + TotalFemale;

                //TotalSummary

                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    xtwWriter.WriteValue("");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();


                //Row for Male Room:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Room:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                        xtwWriter.WriteValue(TotalMale.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Male Room:
                xtwWriter.WriteEndElement();


                //Row for Female Room:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Room:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                        xtwWriter.WriteValue(TotalFemale.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Room:
                xtwWriter.WriteEndElement();

                //Row for Total Room:                
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Total Room:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(Total.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Total Room:
                xtwWriter.WriteEndElement();

                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    xtwWriter.WriteValue("");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();

                //Row for Booked from Prev Date
                double BookedInPrevDate = LockedManifestClass.BookedInPrevDateCount;
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Booked from Prev Date:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(BookedInPrevDate.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Booked in Prev Date
                xtwWriter.WriteEndElement();

                //Row for Total for the day
                Total = Total + BookedInPrevDate;
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Total Room for the Day:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(Total.ToString("0.#"));
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row  for Total for the day
                xtwWriter.WriteEndElement();


                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    xtwWriter.WriteValue("");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();


                //Male Single:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Single:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(MaleSingle.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //Male Single:
                xtwWriter.WriteEndElement();

                //Male Double:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Double:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(MaleDouble.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Male Double:
                xtwWriter.WriteEndElement();


                //Male Single/Double Total:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Male Single/Double Total:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(MaleSDTotal.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Male Single/Double Total:
                xtwWriter.WriteEndElement();


                //Row for blank space
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    xtwWriter.WriteStartElement("Cell");
                    xtwWriter.WriteStartElement("Data");
                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    xtwWriter.WriteValue("");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for blank space
                xtwWriter.WriteEndElement();


                //Female Single:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Single:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(FemaleSingle.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Single:
                xtwWriter.WriteEndElement();


                //Female Double:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Double:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(FemaleDouble.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Double:
                xtwWriter.WriteEndElement();


                //Female Single/Double Total:
                xtwWriter.WriteStartElement("Row");
                for (int x = 1; x <= iColCount; x++)
                {
                    if (x == 10)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("Female Single/Double Total:");
                    }
                    else if (x == 11)
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");

                        xtwWriter.WriteValue(FemaleSDTotal.ToString());
                    }
                    else
                    {
                        xtwWriter.WriteStartElement("Cell");
                        xtwWriter.WriteStartElement("Data");
                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");

                        xtwWriter.WriteValue("");
                    }
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();
                }
                //End Row for Female Single/Double Total:
                xtwWriter.WriteEndElement();               
                // </Table>
                xtwWriter.WriteEndElement();
                             
                // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                // Write settings of page
                xtwWriter.WriteStartElement("PageSetup");
                xtwWriter.WriteStartElement("Header");
                xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                xtwWriter.WriteEndElement();
                xtwWriter.WriteStartElement("Footer");
                xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                xtwWriter.WriteEndElement();
                xtwWriter.WriteStartElement("PageMargins");
                xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                xtwWriter.WriteEndElement();
                xtwWriter.WriteEndElement();

                // <Selected/>
                xtwWriter.WriteElementString("Selected", null);

                // <Panes>
                xtwWriter.WriteStartElement("Panes");

                // <Pane>
                xtwWriter.WriteStartElement("Pane");

                // Write settings of active field
                xtwWriter.WriteElementString("Number", "1");
                xtwWriter.WriteElementString("ActiveRow", "1");
                xtwWriter.WriteElementString("ActiveCol", "1");

                // </Pane>
                xtwWriter.WriteEndElement();

                // </Panes>
                xtwWriter.WriteEndElement();

                // <ProtectObjects>False</ProtectObjects>
                xtwWriter.WriteElementString("ProtectObjects", "False");

                // <ProtectScenarios>False</ProtectScenarios>
                xtwWriter.WriteElementString("ProtectScenarios", "False");

                // </WorksheetOptions>
                xtwWriter.WriteEndElement();

                // </Worksheet>
                xtwWriter.WriteEndElement();
                
             #region COMPARED SHEET
                //START OF COMPARED SHEET
                if (uoChkBoxSendDiff.Checked == true && dtCompare != null
                    && uoDropDownListManifest.SelectedValue != "0"
                    && uoDropDownListCompare.SelectedValue != "-1")
                {
                    int iCompare = 1;
                    int iColCountCompare = 30;
                    int iColCountCompareExpand = 31;
                    int iRowCompare = dtCompare.Rows.Count + 16;
                    string sSheetName = uoDropDownListManifest.SelectedItem.Text.Split("".ToCharArray())[0] + "vs"
                                        + uoDropDownListCompare.SelectedItem.Text.Split("".ToCharArray())[0] + "_Difference";


                    // <Worksheet ss:Name="xxx">
                    xtwWriter.WriteStartElement("Worksheet");
                    xtwWriter.WriteAttributeString("ss", "Name", null, sSheetName);

                    // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
                    xtwWriter.WriteStartElement("Table");

                    xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCountCompareExpand.ToString());
                    xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRowCompare.ToString());

                    xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
                    xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
                    xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

                    //Header
                    xtwWriter.WriteStartElement("Row");
                    xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");

                    foreach (DataColumn Header in dtCompare.Columns)
                    {
                        if (iCompare <= iColCountCompare)
                        {
                            xtwWriter.WriteStartElement("Cell");
                            // xxx
                            xtwWriter.WriteStartElement("Data");
                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                            // Write content of cell
                            xtwWriter.WriteValue(Header.ColumnName);
                            xtwWriter.WriteEndElement();
                            xtwWriter.WriteEndElement();
                        }
                        iCompare++;
                    }
                    ////header for QueryRemarks
                    //xtwWriter.WriteStartElement("Cell");
                    //// xxx
                    //xtwWriter.WriteStartElement("Data");
                    //xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                    //// Write content of cell
                    //xtwWriter.WriteValue("Comparison Remarks");
                    //xtwWriter.WriteEndElement();
                    //xtwWriter.WriteEndElement();

                    xtwWriter.WriteEndElement();
                    

                    // Run through all rows of data source
                    foreach (DataRow row in dtCompare.Rows)
                    {
                        // <Row>
                        xtwWriter.WriteStartElement("Row");

                        i = 1;
                        // Run through all cell of current rows
                        foreach (object cellValue in row.ItemArray)
                        {
                            if (i <= iColCountCompare) //36 is the query remarks column
                            {
                                // <Cell>
                                xtwWriter.WriteStartElement("Cell");

                                // <Data ss:Type="String">xxx</Data>
                                xtwWriter.WriteStartElement("Data");

                                if (dtCompare.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID" ||
                                    dtCompare.Columns[i - 1].Caption.ToUpper() == "HOTELNIGHTS" ||
                                    dtCompare.Columns[i - 1].Caption.ToUpper() == "SINGLEDOUBLE" ||
                                    dtCompare.Columns[i - 1].Caption.ToUpper() == "VOUCHER")
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
                                }
                                else
                                {
                                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                }
                                // Write content of cell
                                xtwWriter.WriteValue(cellValue);
                                //if (cellValue.ToString() != "")
                                //{
                                //    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
                                //    // Write content of cell
                                //    xtwWriter.WriteValue(cellValue);
                                //}
                                //else 
                                //{
                                //    xtwWriter.WriteAttributeString("Type", "");
                                //    // Write content of cell
                                //    xtwWriter.WriteValue("");
                                //}

                                // </Data>
                                xtwWriter.WriteEndElement();

                                // </Cell>
                                xtwWriter.WriteEndElement();
                            }
                            i++;                            
                        }
                        // </Row>
                        xtwWriter.WriteEndElement();
                    }

                    // </Table>
                    xtwWriter.WriteEndElement();
                    // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                    xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

                    // Write settings of page
                    xtwWriter.WriteStartElement("PageSetup");
                    xtwWriter.WriteStartElement("Header");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("Footer");
                    xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteStartElement("PageMargins");
                    xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
                    xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
                    xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
                    xtwWriter.WriteEndElement();
                    xtwWriter.WriteEndElement();

                    // <Selected/>
                    xtwWriter.WriteElementString("Selected", null);

                    // <Panes>
                    xtwWriter.WriteStartElement("Panes");

                    // <Pane>
                    xtwWriter.WriteStartElement("Pane");

                    // Write settings of active field
                    xtwWriter.WriteElementString("Number", "1");
                    xtwWriter.WriteElementString("ActiveRow", "1");
                    xtwWriter.WriteElementString("ActiveCol", "1");

                    // </Pane>
                    xtwWriter.WriteEndElement();

                    // </Panes>
                    xtwWriter.WriteEndElement();

                    // <ProtectObjects>False</ProtectObjects>
                    xtwWriter.WriteElementString("ProtectObjects", "False");

                    // <ProtectScenarios>False</ProtectScenarios>
                    xtwWriter.WriteElementString("ProtectScenarios", "False");

                    // </WorksheetOptions>
                    xtwWriter.WriteEndElement();

                    // </Worksheet>
                    xtwWriter.WriteEndElement();
                }
                //END OF COMPARED SHEET
            #endregion

                // </Workbook>
                xtwWriter.WriteEndElement();

                // Write file on hard disk
                xtwWriter.Flush();
                xtwWriter.Close();

                //FileInfo FileName = new FileInfo(strFileName);
                //FileStream fs = new FileStream(FileName.FullName, FileMode.Create);                
                //fs.Close();
            }
        }      
        //bkup of create excel old format (19/07/2012)
        //public void CreateExcel(DataTable dtSource, string strFileName, DataTable dtCompare)
        //{
        //    // Create XMLWriter
        //    using (XmlTextWriter xtwWriter = new XmlTextWriter(strFileName, Encoding.UTF8))
        //    {
        //        int iColCount = 34;
        //        //Format the output file for reading easier
        //        xtwWriter.Formatting = Formatting.Indented;

        //        // <?xml version="1.0"?>
        //        xtwWriter.WriteStartDocument();

        //        // <?mso-application progid="Excel.Sheet"?>
        //        xtwWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");

        //        // <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet >"
        //        xtwWriter.WriteStartElement("Workbook", "urn:schemas-microsoft-com:office:spreadsheet");

        //        //Write definition of namespace
        //        xtwWriter.WriteAttributeString("xmlns", "o", null, "urn:schemas-microsoft-com:office:office");
        //        xtwWriter.WriteAttributeString("xmlns", "x", null, "urn:schemas-microsoft-com:office:excel");
        //        xtwWriter.WriteAttributeString("xmlns", "ss", null, "urn:schemas-microsoft-com:office:spreadsheet");
        //        xtwWriter.WriteAttributeString("xmlns", "html", null, "http://www.w3.org/TR/REC-html40");

        //        // <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
        //        xtwWriter.WriteStartElement("DocumentProperties", "urn:schemas-microsoft-com:office:office");

        //        // Write document properties
        //        xtwWriter.WriteElementString("Author", "Travelmart");
        //        xtwWriter.WriteElementString("LastAuthor", Environment.UserName);
        //        xtwWriter.WriteElementString("Created", DateTime.Now.ToString("u") + "Z");
        //        xtwWriter.WriteElementString("Company", "RCCL");
        //        xtwWriter.WriteElementString("Version", "1");

        //        // </DocumentProperties>
        //        xtwWriter.WriteEndElement();

        //        // <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
        //        xtwWriter.WriteStartElement("ExcelWorkbook", "urn:schemas-microsoft-com:office:excel");

        //        // Write settings of workbook
        //        xtwWriter.WriteElementString("WindowHeight", "13170");
        //        xtwWriter.WriteElementString("WindowWidth", "17580");
        //        xtwWriter.WriteElementString("WindowTopX", "120");
        //        xtwWriter.WriteElementString("WindowTopY", "60");
        //        xtwWriter.WriteElementString("ProtectStructure", "False");
        //        xtwWriter.WriteElementString("ProtectWindows", "False");

        //        // </ExcelWorkbook>
        //        xtwWriter.WriteEndElement();

        //        // <Styles>
        //        xtwWriter.WriteStartElement("Styles");

        //        // <Style ss:ID="Default" ss:Name="Normal">
        //        xtwWriter.WriteStartElement("Style");
        //        xtwWriter.WriteAttributeString("ss", "ID", null, "Default");
        //        xtwWriter.WriteAttributeString("ss", "Name", null, "Normal");

        //        // <Alignment ss:Vertical="Bottom"/>
        //        xtwWriter.WriteStartElement("Alignment");
        //        xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
        //        xtwWriter.WriteEndElement();

        //        // Write null on the other properties
        //        xtwWriter.WriteElementString("Borders", null);
        //        xtwWriter.WriteElementString("Font", null);
        //        xtwWriter.WriteElementString("Interior", null);
        //        xtwWriter.WriteElementString("NumberFormat", null);
        //        xtwWriter.WriteElementString("Protection", null);
        //        // </Style>
        //        xtwWriter.WriteEndElement();

        //        //Style for header
        //        xtwWriter.WriteStartElement("Style");
        //        //<Style ss:ID="s62">
        //        xtwWriter.WriteAttributeString("ss", "ID", null, "s62");
        //        xtwWriter.WriteStartElement("Font");
        //        // <Font ss:Bold="1"/>
        //        xtwWriter.WriteAttributeString("ss", "Bold", null, "1");
        //        //end of font
        //        xtwWriter.WriteEndElement();
        //        //End Style for header
        //        xtwWriter.WriteEndElement();


        //        //Style for total summary numbers
        //        xtwWriter.WriteStartElement("Style");
        //        //<Style ss:ID="s64">
        //        xtwWriter.WriteAttributeString("ss", "ID", null, "s64");
        //        xtwWriter.WriteStartElement("Alignment");
        //        xtwWriter.WriteAttributeString("ss", "Horizontal", null, "Right");
        //        xtwWriter.WriteAttributeString("ss", "Vertical", null, "Bottom");
        //        xtwWriter.WriteEndElement();
        //        //End Style for header
        //        xtwWriter.WriteEndElement();


        //        // </Styles>
        //        xtwWriter.WriteEndElement();

        //        // <Worksheet ss:Name="xxx">
        //        xtwWriter.WriteStartElement("Worksheet");
        //        xtwWriter.WriteAttributeString("ss", "Name", null, dtSource.Rows[0]["HotelBranch"].ToString());

        //        // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
        //        xtwWriter.WriteStartElement("Table");

        //        int iRow = dtSource.Rows.Count + 16;

        //        xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCount.ToString());
        //        xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRow.ToString());

        //        xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
        //        xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
        //        xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

        //        //Variables for computation
        //        string Gender;
        //        string RoomType;

        //        double Total = 0;
        //        double TotalMale = 0;
        //        double TotalFemale = 0;
        //        double TotalMaleSingle = 0;
        //        double TotalMaleDouble = 0;
        //        double TotalFemaleSingle = 0;
        //        double TotalFemaleDouble = 0;

        //        Int32 MaleSingle = 0;
        //        Int32 MaleDouble = 0;
        //        Int32 FemaleSingle = 0;
        //        Int32 FemaleDouble = 0;

        //        Int32 MaleSDTotal = 0;
        //        Int32 FemaleSDTotal = 0;

        //        //Header
        //        xtwWriter.WriteStartElement("Row");
        //        xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");
        //        int i = 1;
        //        foreach (DataColumn Header in dtSource.Columns)
        //        {
        //            if (i <= iColCount)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                // xxx
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                // Write content of cell
        //                xtwWriter.WriteValue(Header.ColumnName);
        //                xtwWriter.WriteEndElement();
        //                xtwWriter.WriteEndElement();
        //            }
        //            i++;
        //        }
        //        xtwWriter.WriteEndElement();


        //        // Run through all rows of data source
        //        foreach (DataRow row in dtSource.Rows)
        //        {
        //            // <Row>
        //            xtwWriter.WriteStartElement("Row");

        //            i = 1;
        //            // Run through all cell of current rows
        //            foreach (object cellValue in row.ItemArray)
        //            {
        //                if (i <= iColCount)
        //                {
        //                    // <Cell>
        //                    xtwWriter.WriteStartElement("Cell");

        //                    // <Data ss:Type="String">xxx</Data>
        //                    xtwWriter.WriteStartElement("Data");

        //                    if (dtSource.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID")
        //                    {
        //                        xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
        //                    }
        //                    else
        //                    {
        //                        xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                    }
        //                    // Write content of cell
        //                    xtwWriter.WriteValue(cellValue);
        //                    //if (cellValue.ToString() != "")
        //                    //{
        //                    //    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                    //    // Write content of cell
        //                    //    xtwWriter.WriteValue(cellValue);
        //                    //}
        //                    //else 
        //                    //{
        //                    //    xtwWriter.WriteAttributeString("Type", "");
        //                    //    // Write content of cell
        //                    //    xtwWriter.WriteValue("");
        //                    //}

        //                    // </Data>
        //                    xtwWriter.WriteEndElement();

        //                    // </Cell>
        //                    xtwWriter.WriteEndElement();
        //                }
        //                i++;
        //            }
        //            // </Row>
        //            xtwWriter.WriteEndElement();

        //            //Computation
        //            Gender = row["Gender"].ToString();
        //            RoomType = row["SingleDouble"].ToString();
        //            // Gender count					
        //            if (Gender == "M" || Gender == "Male")
        //            {
        //                if (RoomType == "1")
        //                {
        //                    TotalMaleSingle = TotalMaleSingle + 1;
        //                    MaleSingle = MaleSingle + 1;
        //                }
        //                else
        //                {
        //                    TotalMaleDouble = TotalMaleDouble + .5;
        //                    MaleDouble = MaleDouble + 1;
        //                }
        //                TotalMale = TotalMaleSingle + TotalMaleDouble;
        //            }
        //            else
        //            {
        //                if (RoomType == "1")
        //                {
        //                    TotalFemaleSingle = TotalFemaleSingle + 1;
        //                    FemaleSingle = FemaleSingle + 1;
        //                }
        //                else
        //                {
        //                    TotalFemaleDouble = TotalFemaleDouble + .5;
        //                    FemaleDouble = FemaleDouble + 1;
        //                }
        //                TotalFemale = TotalFemaleSingle + TotalFemaleDouble;
        //            }
        //            //Total = Total + .5;
        //        }
        //        MaleSDTotal = MaleSingle + MaleDouble;
        //        FemaleSDTotal = FemaleSingle + FemaleDouble;
        //        Total = TotalMale + TotalFemale;

        //        //TotalSummary

        //        //Row for blank space
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            xtwWriter.WriteStartElement("Cell");
        //            xtwWriter.WriteStartElement("Data");
        //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //            xtwWriter.WriteValue("");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for blank space
        //        xtwWriter.WriteEndElement();


        //        //Row for Male Room:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Male Room:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                xtwWriter.WriteValue(TotalMale.ToString("0.#"));
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Male Room:
        //        xtwWriter.WriteEndElement();


        //        //Row for Female Room:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Female Room:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                xtwWriter.WriteValue(TotalFemale.ToString("0.#"));
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Female Room:
        //        xtwWriter.WriteEndElement();

        //        //Row for Total Room:                
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Total Room:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(Total.ToString("0.#"));
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Total Room:
        //        xtwWriter.WriteEndElement();

        //        //Row for blank space
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            xtwWriter.WriteStartElement("Cell");
        //            xtwWriter.WriteStartElement("Data");
        //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //            xtwWriter.WriteValue("");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for blank space
        //        xtwWriter.WriteEndElement();

        //        //Row for Booked from Prev Date
        //        double BookedInPrevDate = LockedManifestClass.BookedInPrevDateCount;
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Booked from Prev Date:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(BookedInPrevDate.ToString("0.#"));
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Booked in Prev Date
        //        xtwWriter.WriteEndElement();

        //        //Row for Total for the day
        //        Total = Total + BookedInPrevDate;
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Total Room for the Day:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(Total.ToString("0.#"));
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row  for Total for the day
        //        xtwWriter.WriteEndElement();


        //        //Row for blank space
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            xtwWriter.WriteStartElement("Cell");
        //            xtwWriter.WriteStartElement("Data");
        //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //            xtwWriter.WriteValue("");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for blank space
        //        xtwWriter.WriteEndElement();


        //        //Male Single:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Male Single:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(MaleSingle.ToString());
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //Male Single:
        //        xtwWriter.WriteEndElement();

        //        //Male Double:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Male Double:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(MaleDouble.ToString());
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Male Double:
        //        xtwWriter.WriteEndElement();


        //        //Male Single/Double Total:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Male Single/Double Total:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(MaleSDTotal.ToString());
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Male Single/Double Total:
        //        xtwWriter.WriteEndElement();


        //        //Row for blank space
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            xtwWriter.WriteStartElement("Cell");
        //            xtwWriter.WriteStartElement("Data");
        //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //            xtwWriter.WriteValue("");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for blank space
        //        xtwWriter.WriteEndElement();


        //        //Female Single:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Female Single:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(FemaleSingle.ToString());
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Female Single:
        //        xtwWriter.WriteEndElement();


        //        //Female Double:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Female Double:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(FemaleDouble.ToString());
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Female Double:
        //        xtwWriter.WriteEndElement();


        //        //Female Single/Double Total:
        //        xtwWriter.WriteStartElement("Row");
        //        for (int x = 1; x <= iColCount; x++)
        //        {
        //            if (x == 10)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("Female Single/Double Total:");
        //            }
        //            else if (x == 11)
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteAttributeString("ss", "StyleID", null, "s64");

        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue(FemaleSDTotal.ToString());
        //            }
        //            else
        //            {
        //                xtwWriter.WriteStartElement("Cell");
        //                xtwWriter.WriteStartElement("Data");
        //                xtwWriter.WriteAttributeString("ss", "Type", null, "String");

        //                xtwWriter.WriteValue("");
        //            }
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();
        //        }
        //        //End Row for Female Single/Double Total:
        //        xtwWriter.WriteEndElement();
        //        // </Table>
        //        xtwWriter.WriteEndElement();

        //        // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
        //        xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

        //        // Write settings of page
        //        xtwWriter.WriteStartElement("PageSetup");
        //        xtwWriter.WriteStartElement("Header");
        //        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
        //        xtwWriter.WriteEndElement();
        //        xtwWriter.WriteStartElement("Footer");
        //        xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
        //        xtwWriter.WriteEndElement();
        //        xtwWriter.WriteStartElement("PageMargins");
        //        xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
        //        xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
        //        xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
        //        xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
        //        xtwWriter.WriteEndElement();
        //        xtwWriter.WriteEndElement();

        //        // <Selected/>
        //        xtwWriter.WriteElementString("Selected", null);

        //        // <Panes>
        //        xtwWriter.WriteStartElement("Panes");

        //        // <Pane>
        //        xtwWriter.WriteStartElement("Pane");

        //        // Write settings of active field
        //        xtwWriter.WriteElementString("Number", "1");
        //        xtwWriter.WriteElementString("ActiveRow", "1");
        //        xtwWriter.WriteElementString("ActiveCol", "1");

        //        // </Pane>
        //        xtwWriter.WriteEndElement();

        //        // </Panes>
        //        xtwWriter.WriteEndElement();

        //        // <ProtectObjects>False</ProtectObjects>
        //        xtwWriter.WriteElementString("ProtectObjects", "False");

        //        // <ProtectScenarios>False</ProtectScenarios>
        //        xtwWriter.WriteElementString("ProtectScenarios", "False");

        //        // </WorksheetOptions>
        //        xtwWriter.WriteEndElement();

        //        // </Worksheet>
        //        xtwWriter.WriteEndElement();

        //        #region COMPARED SHEET
        //        //START OF COMPARED SHEET
        //        if (uoChkBoxSendDiff.Checked == true && dtCompare != null
        //            && uoDropDownListManifest.SelectedValue != "0"
        //            && uoDropDownListCompare.SelectedValue != "-1")
        //        {
        //            int iCompare = 1;
        //            int iColCountCompare = 34;
        //            int iColCountCompareExpand = 35;
        //            int iRowCompare = dtCompare.Rows.Count + 16;
        //            string sSheetName = uoDropDownListManifest.SelectedItem.Text.Split("".ToCharArray())[0] + "vs"
        //                                + uoDropDownListCompare.SelectedItem.Text.Split("".ToCharArray())[0] + "Difference";


        //            // <Worksheet ss:Name="xxx">
        //            xtwWriter.WriteStartElement("Worksheet");
        //            xtwWriter.WriteAttributeString("ss", "Name", null, sSheetName);

        //            // <Table ss:ExpandedColumnCount="2" ss:ExpandedRowCount="3" x:FullColumns="1" x:FullRows="1" ss:DefaultColumnWidth="60">
        //            xtwWriter.WriteStartElement("Table");

        //            xtwWriter.WriteAttributeString("ss", "ExpandedColumnCount", null, iColCountCompareExpand.ToString());
        //            xtwWriter.WriteAttributeString("ss", "ExpandedRowCount", null, iRowCompare.ToString());

        //            xtwWriter.WriteAttributeString("x", "FullColumns", null, "1");
        //            xtwWriter.WriteAttributeString("x", "FullRows", null, "1");
        //            xtwWriter.WriteAttributeString("ss", "DefaultColumnWidth", null, "60");

        //            //Header
        //            xtwWriter.WriteStartElement("Row");
        //            xtwWriter.WriteAttributeString("ss", "StyleID", null, "s62");

        //            foreach (DataColumn Header in dtCompare.Columns)
        //            {
        //                if (iCompare <= iColCountCompare)
        //                {
        //                    xtwWriter.WriteStartElement("Cell");
        //                    // xxx
        //                    xtwWriter.WriteStartElement("Data");
        //                    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                    // Write content of cell
        //                    xtwWriter.WriteValue(Header.ColumnName);
        //                    xtwWriter.WriteEndElement();
        //                    xtwWriter.WriteEndElement();
        //                }
        //                iCompare++;
        //            }
        //            //header for QueryRemarks
        //            xtwWriter.WriteStartElement("Cell");
        //            // xxx
        //            xtwWriter.WriteStartElement("Data");
        //            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //            // Write content of cell
        //            xtwWriter.WriteValue("Comparison Remarks");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();

        //            xtwWriter.WriteEndElement();


        //            // Run through all rows of data source
        //            foreach (DataRow row in dtCompare.Rows)
        //            {
        //                // <Row>
        //                xtwWriter.WriteStartElement("Row");

        //                i = 1;
        //                // Run through all cell of current rows
        //                foreach (object cellValue in row.ItemArray)
        //                {
        //                    if (i <= iColCountCompare || dtCompare.Columns[i - 1].Caption.ToUpper() == "QUERYREMARKS") //36 is the query remarks column
        //                    {
        //                        // <Cell>
        //                        xtwWriter.WriteStartElement("Cell");

        //                        // <Data ss:Type="String">xxx</Data>
        //                        xtwWriter.WriteStartElement("Data");

        //                        if (dtCompare.Columns[i - 1].Caption.ToUpper() == "EMPLOYEEID")
        //                        {
        //                            xtwWriter.WriteAttributeString("ss", "Type", null, "Number");
        //                        }
        //                        else
        //                        {
        //                            xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                        }
        //                        // Write content of cell
        //                        xtwWriter.WriteValue(cellValue);
        //                        //if (cellValue.ToString() != "")
        //                        //{
        //                        //    xtwWriter.WriteAttributeString("ss", "Type", null, "String");
        //                        //    // Write content of cell
        //                        //    xtwWriter.WriteValue(cellValue);
        //                        //}
        //                        //else 
        //                        //{
        //                        //    xtwWriter.WriteAttributeString("Type", "");
        //                        //    // Write content of cell
        //                        //    xtwWriter.WriteValue("");
        //                        //}

        //                        // </Data>
        //                        xtwWriter.WriteEndElement();

        //                        // </Cell>
        //                        xtwWriter.WriteEndElement();
        //                    }
        //                    i++;
        //                }
        //                // </Row>
        //                xtwWriter.WriteEndElement();
        //            }

        //            // </Table>
        //            xtwWriter.WriteEndElement();
        //            // <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
        //            xtwWriter.WriteStartElement("WorksheetOptions", "urn:schemas-microsoft-com:office:excel");

        //            // Write settings of page
        //            xtwWriter.WriteStartElement("PageSetup");
        //            xtwWriter.WriteStartElement("Header");
        //            xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteStartElement("Footer");
        //            xtwWriter.WriteAttributeString("x", "Margin", null, "0.4921259845");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteStartElement("PageMargins");
        //            xtwWriter.WriteAttributeString("x", "Bottom", null, "0.984251969");
        //            xtwWriter.WriteAttributeString("x", "Left", null, "0.78740157499999996");
        //            xtwWriter.WriteAttributeString("x", "Right", null, "0.78740157499999996");
        //            xtwWriter.WriteAttributeString("x", "Top", null, "0.984251969");
        //            xtwWriter.WriteEndElement();
        //            xtwWriter.WriteEndElement();

        //            // <Selected/>
        //            xtwWriter.WriteElementString("Selected", null);

        //            // <Panes>
        //            xtwWriter.WriteStartElement("Panes");

        //            // <Pane>
        //            xtwWriter.WriteStartElement("Pane");

        //            // Write settings of active field
        //            xtwWriter.WriteElementString("Number", "1");
        //            xtwWriter.WriteElementString("ActiveRow", "1");
        //            xtwWriter.WriteElementString("ActiveCol", "1");

        //            // </Pane>
        //            xtwWriter.WriteEndElement();

        //            // </Panes>
        //            xtwWriter.WriteEndElement();

        //            // <ProtectObjects>False</ProtectObjects>
        //            xtwWriter.WriteElementString("ProtectObjects", "False");

        //            // <ProtectScenarios>False</ProtectScenarios>
        //            xtwWriter.WriteElementString("ProtectScenarios", "False");

        //            // </WorksheetOptions>
        //            xtwWriter.WriteEndElement();

        //            // </Worksheet>
        //            xtwWriter.WriteEndElement();
        //        }
        //        //END OF COMPARED SHEET
        //        #endregion

        //        // </Workbook>
        //        xtwWriter.WriteEndElement();

        //        // Write file on hard disk
        //        xtwWriter.Flush();
        //        xtwWriter.Close();

        //        //FileInfo FileName = new FileInfo(strFileName);
        //        //FileStream fs = new FileStream(FileName.FullName, FileMode.Create);                
        //        //fs.Close();
        //    }
        //}      
        #endregion

        /// <summary>
        /// Author:       Gabriel Oquialda
        /// Date Created: 17/04/2012
        /// Description:  Get hotel locked manifest to be exported
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  11/07/2012
        /// Description:    use using in DataTable
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  18/07/2012
        /// Description:    use try and catch to close DataTable
        ///                 Add LockedManifestDT , remove unnecessary columns, re-order columns
        ///                 Add sFinalFileName and DifferenceManifestDT to include manifest with comparison
        ///                 Add sFinalDirectory
        /// ---------------------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  04/10/2012
        /// Description:    Change LockedManifestClass.LockedManifestEmail to Session
        /// ---------------------------------------------------------
        /// </summary>
        private void ExportHotelLockedManifest()
        {
            DataTable LockedManifestDT = null;
            DataTable DifferenceManifestDT = null;
           
            try
            {
                List<LockedManifestEmail> list = new List<LockedManifestEmail>();
                if (Session["LockedManifestClass_LockedManifestEmail"] != null)
                {
                    list = (List<LockedManifestEmail>)Session["LockedManifestClass_LockedManifestEmail"];
                }
                var e = (from a in list//LockedManifestClass.LockedManifestEmail
                         select new
                         {
                             HotelCity = GlobalCode.Field2String(a.HotelCity),
                             CheckIn = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckIn)),
                             CheckOut = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.CheckOut)),
                             HotelNights = a.HotelNights,
                             ReasonCode =  GlobalCode.Field2String(a.ReasonCode),
                             LastName = GlobalCode.Field2String(a.LastName),
                             FirstName = GlobalCode.Field2String(a.FirstName),

                             EmployeeId = a.EmployeeId.ToString(),
                             Gender = GlobalCode.Field2String(a.Gender),
                             SingleDouble = GlobalCode.Field2String((a.SingleDouble)),
                             Couple = GlobalCode.Field2String(a.Couple),
                             Title = GlobalCode.Field2String(a.Title),
                             Ship = GlobalCode.Field2String(a.Ship),
                             
                             CostCenter = GlobalCode.Field2String(a.CostCenter),
                             Nationality = GlobalCode.Field2String(a.Nationality),                                                          
                             HotelRequest = GlobalCode.Field2String(a.HotelRequest),
                             RecordLocator = GlobalCode.Field2String(a.RecordLocator),

                             DeptCity = GlobalCode.Field2String(a.DeptCity),
                             ArvlCity = GlobalCode.Field2String(a.ArvlCity),
                             DeptDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.DeptDate)),
                             ArvlDate = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.ArvlDate)),
                             DeptTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.DeptTime)),
                             ArvlTime = GlobalCode.Field2String(string.Format("{0:hh:mm:ss}", a.ArvlTime)),
                             
                             Carrier = GlobalCode.Field2String(a.Carrier),
                             FlightNo = GlobalCode.Field2String(a.FlightNo),
                             Voucher = GlobalCode.Field2String(a.Voucher),

                             PassportNo = GlobalCode.Field2String(a.PassportNo),
                             IssuedDate = GlobalCode.Field2String(a.IssuedDate),
                             PassportExpiration = GlobalCode.Field2String(string.Format("{0:dd-MMM-yyyy}", a.PassportExpiration)),

                             HotelBranch = GlobalCode.Field2String(a.HotelBranch),
                                                          
                         }).ToList();

                LockedManifestDT = getDataTable(e);

                string sDate = DateTime.Now.ToString(TravelMartVariable.DateTimeFormatFileExtension);
                string sDateManifest = GlobalCode.Field2DateTime(uoHiddenFieldDate.Value).ToString("MMM_dd_yyy");

                string FilePath = Server.MapPath("~/Extract/HotelManifest/");
                string FileName = "HotelManifest_" + sDateManifest + '_' + sDate + ".xls";
                string FileNameWithDiff = "HotelManifestWithDiff_" + sDateManifest + '_' + sDate + ".xls";

                string FileNameDir = FilePath + FileName;//"HotelManifest_" + sDate + ".xls";
                string FileNameWithDiffDir = FilePath + FileNameWithDiff; // "HotelManifestWithDiff_" + sDate + ".xls";
              
                string sFinalFileName = FileName;
                string sFinalDirectory = FileNameDir;

                List<ManifestDifferenceEmail> listDiff = new List<ManifestDifferenceEmail>();
                if (Session["LockedManifestClass_ManifestDifferenceEmail"] != null)
                {
                    listDiff = (List<ManifestDifferenceEmail>)Session["LockedManifestClass_ManifestDifferenceEmail"];
                }

                if (uoChkBoxSendDiff.Checked == true)
                {
                    sFinalFileName = FileNameWithDiff;
                    sFinalDirectory = FileNameWithDiffDir;
                    DifferenceManifestDT = getDataTable(listDiff);
                }

                CreateFile(LockedManifestDT, sFinalFileName, sFinalDirectory, DifferenceManifestDT);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (LockedManifestDT != null)
                {
                    LockedManifestDT.Dispose();
                }
                if (DifferenceManifestDT != null)
                {
                    DifferenceManifestDT.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:       Gabriel Oquialda
        /// Date Created: 17/04/2012
        /// Description:  Create the file to be exported
        /// ====================================================
        /// Modified By:    Josephine Gad
        /// Date Modified:  18/07/2012
        /// Description:    Add dtCompare, strFileName
        ///                 sFinalDirectory to include manifest with comparison
        /// </summary>
        /// <param name="dt"></param>
        protected void CreateFile(DataTable dt, string strFileName,string sFinalDirectory, DataTable dtCompare)
        {
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            CreateExcel(dt, sFinalDirectory, dtCompare);
            OpenExcelFile(strFileName, strFileName);
        }

        /// <summary>
        /// Modified By:    Gabriel Oquialda
        /// Date Modified:  17/04/2012
        /// Description:    open file via excel
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="filePath"></param>
        public void OpenExcelFile(string strFileName, string filePath)
        {
            string strScript = "CloseModal('../Extract/HotelManifest/" + strFileName + "');";
            ScriptManager.RegisterStartupScript(uoBtnExportList, this.GetType(), "CloseModal", strScript, true);
        }
        /// -------------------------------------------
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// -------------------------------------------
        /// Date Modified:   04/10/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change LockedManifestClass.PortList to Session["LockedManifestClass_PortList"]
        /// -------------------------------------------
        /// </summary>
        private void BindPortList(bool IsFirstLoad)
        {
            List<PortList> list = new List<PortList>();
            try
            {
                if (IsFirstLoad)
                {
                    list = (List<PortList>)Session["LockedManifestClass_PortList"]; //LockedManifestClass.PortList;
                }
                else
                {
                    list = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(Session["UserName"]), uoDropDownListRegion.SelectedValue, "0", "");
                }

                uoDropDownListPortPerRegion.Items.Clear();
                ListItem item = new ListItem("--SELECT PORT--", "0");
                uoDropDownListPortPerRegion.Items.Add(item);
                if (list.Count > 0)
                {
                    uoDropDownListPortPerRegion.DataSource = list;
                    uoDropDownListPortPerRegion.DataTextField = "PORTName";
                    uoDropDownListPortPerRegion.DataValueField = "PORTID";
                    uoDropDownListPortPerRegion.DataBind();

                    if (GlobalCode.Field2String(Session["Port"]) != "")
                    {
                        if (uoDropDownListPortPerRegion.Items.FindByValue(GlobalCode.Field2String(Session["Port"])) != null)
                        {
                            uoDropDownListPortPerRegion.SelectedValue = GlobalCode.Field2String(Session["Port"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Modified By:    Josephine Gad
        /// Date Modified:  09/07/2012
        /// Description:    Change DataTable to List
        /// ----------------------------------------------
        /// Modified By:    Josephine Gad
        /// Date Modified:  02/10/2012
        /// Description:    Add option "Select ALL Hotel" ,"-1" if there is selected Region
        /// ----------------------------------------------
        /// </summary>
        private void GetHotelFilter()
        {
            //DataTable dt = null;
            List<HotelDTO> list = new List<HotelDTO>();
            try
            {
                list = HotelBLL.GetHotelBranchByRegionPortCountry(uoHiddenFieldUser.Value, Session["Region"].ToString(),
                    Session["Port"] == null ? "0" : Session["Port"].ToString(), "0", "0");

                int iRowCount = list.Count;
                if (iRowCount == 1)
                {
                    Session["Hotel"] = list[0].HotelIDString;//dt.Rows[0]["BranchID"].ToString();
                }
                if (iRowCount > 0)
                {
                    uoDropDownListBranch.Items.Clear();
                    uoDropDownListBranch.DataSource = list;
                    uoDropDownListBranch.DataTextField = "HotelNameString";
                    uoDropDownListBranch.DataValueField = "HotelIDString";
                    uoDropDownListBranch.DataBind();
                    uoDropDownListBranch.Items.Insert(0, new ListItem("--Select Hotel--", "0"));

                    uoDropDownListBranch.SelectedValue = "0";

                    if (GlobalCode.Field2Int(Session["Region"]) > 0)
                    {
                        if (uoDropDownListBranch.Items.FindByValue("-1") == null)
                        {
                            uoDropDownListBranch.Items.Insert(1, new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                    else
                    {
                        if (uoDropDownListBranch.Items.FindByValue("-1") != null)
                        {
                            uoDropDownListBranch.Items.Remove(new ListItem("--Select ALL Hotel--", "-1"));
                        }
                    }
                }
                if (uoDropDownListBranch.Items.FindByValue(GlobalCode.Field2String(Session["Hotel"])) != null)
                {
                    uoDropDownListBranch.SelectedValue = GlobalCode.Field2String(Session["Hotel"]);
                    LoadEmailAddress(uoHiddenBranch.Value);
                    uoHyperLinkSendEmail.HRef = "~/Hotel/HotelEmailManifest.aspx?bId=" + uoHiddenBranch.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        string lastDataFieldValue = null;
        protected string HotelAddGroup()
        {
            //Get the data field value of interest for this row            
            string GroupTextString = "Hotel"; //"Check In";
            string GroupValueString = "HotelBranch"; //"colTimeSpanStartDate";

            if (Eval(GroupValueString) != null)
            {
                string currentDataFieldValue = Eval(GroupValueString).ToString();

                //Specify name to display if dataFieldValue is a database NULL
                if (currentDataFieldValue.Length == 0 || currentDataFieldValue == null)
                {
                    currentDataFieldValue = "";
                }
                //See if there's been a change in value
                if (lastDataFieldValue != currentDataFieldValue) //Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy")
                {
                    //There's been a change! Record the change and emit the table row
                    lastDataFieldValue = currentDataFieldValue; //Convert.ToDateTime(currentDataFieldValue).ToString("dd-MMM-yyyy")
                    return string.Format("<tr><td class=\"group\" colspan=\"34\">{0}: <strong>{1}</strong></td></tr>", GroupTextString, lastDataFieldValue);
                }
                else
                {
                    //No change, return an empty string
                    return string.Empty;
                }
            }
            else
            {
                //No change, return an empty string
                return string.Empty;
            }
        }
        /// <summary>
        /// Author:             Josephine Gad
        /// Date Created:       22/05/2012
        /// Description:        Bind Region List
        /// ------------------------------------
        /// </summary>
        private void BindRegionList()
        {
            List<RegionList> list = new List<RegionList>();
            try
            {
                if (Session["HotelDashboardDTO_RegionList"] != null)
                {
                    list = (List<RegionList>)Session["HotelDashboardDTO_RegionList"];
                }
                else
                {
                    list = CountryBLL.RegionListByUser(uoHiddenFieldUser.Value);
                    Session["HotelDashboardDTO_RegionList"] = list;
                }
                if (list.Count > 0)
                {
                    uoDropDownListRegion.Items.Clear();
                    uoDropDownListRegion.DataSource = list;
                    uoDropDownListRegion.DataTextField = "RegionName";
                    uoDropDownListRegion.DataValueField = "RegionId";
                    uoDropDownListRegion.DataBind();
                }
                uoDropDownListRegion.Items.Insert(0, new ListItem("--Select Region--", "0"));

                string sRegion = GlobalCode.Field2String(Session["Region"]);
                if (sRegion != "")
                {
                    if (uoDropDownListRegion.Items.FindByValue(sRegion) != null)
                    {
                        uoDropDownListRegion.SelectedValue = sRegion;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

       
    }
}

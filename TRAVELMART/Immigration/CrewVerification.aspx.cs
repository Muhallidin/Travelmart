using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TRAVELMART.Common;
using TRAVELMART.BLL;

using System.Text;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Xml;
using System.Globalization;

using MGW.QRCode.Codec;
using System.Configuration;

namespace TRAVELMART.Immigration
{
    public partial class CrewVerification : System.Web.UI.Page
    {


        /// <summary>
        /// Author: Muhallidin G Wali
        /// Date Created: 08/06/2013
        /// Description: pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = ' " + s + " '; ";
            //sScript += "alert('" + s + "');";
            sScript += "alert( msg );";
            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }


        CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);

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
           
        }

        private AsyncTaskDelegate _dlgtSeafarer;
        // Create delegate. 
        protected delegate void AsyncTaskDelegate();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeValues();
            string userName = GlobalCode.Field2String(Session["UserName"]);
            uoHDFuserID.Value = GlobalCode.Field2String(Session["UserName"]);
            HiddenField uoHiddenFieldPopupCalendar = (HiddenField)Master.FindControl("uoHiddenFieldPopupCalendar");

            if (!IsPostBack)
            {
                ClearGridCrewVerification();
            }

            tblUCDate.Text = DateTime.Now.ToString("MM/dd/yyyy") + " [" + DateTime.Now.ToString("hh:mm tt") + "]";    
  
        }

        private void ClearGridCrewVerification()
        {
            try
            {

                txtTelephone.Text = "";
                txtUniqeID.Text = "";  
                txtPassportNo.Text = "";  
                txtNationality.Text = "";  
                txtLastName.Text = ""; 
                txtFirstName.Text = ""; 
                txtLOEControlNo.Text = ""; 
                txtExpiration.Text = "";  
                txtDateOffBirth.Text = ""; 
                txtOtherComment.Text = "";
                txtEmailAdd.Text = ""; 
                //txtPosition.Text = "";
                //txtBrand.Text = "";
                //txtJoindate.Text = "";
                //txtJoinShip.Text = "";
                //txtJoinPort.Text = "";
                //txtJoinCity.Text = "";
                //txtPosition.Text = ""; 

                txtLOEControlNo.BackColor = System.Drawing.Color.White;
                


                btnApproved.Enabled = false;
                btnDecline.Enabled = false; 

                rdbFraudulentDoc.Checked = false;
                rdbOther.Checked =  false;
                rdbPriorConDep.Checked = false;
                rdbPriorIIssue.Checked = false; 
                chkNew.Checked = false; 
                uoHDFCrewVericationID.Value = "";
                uoHiddenFieldOldOther.Value = ""; 

                uoListviewAir.DataSource = null;
                uoListviewAir.DataBind();

                uoListViewHotelBook.DataSource = null;
                uoListViewHotelBook.DataBind();

                uoListViewTransportation.DataSource = null;
                uoListViewTransportation.DataBind();

                List<CrewImmigration> immigration = new List<CrewImmigration>();

                uoListViewCrewverification.DataSource = immigration;
                uoListViewCrewverification.DataBind();

                uoDataPagerCrewVerification.DataBind();

                uoDataPagerCrewVerification.SetPageProperties(0, 1, true);

                uoListViewRecentEmployment.DataSource = null;
                uoListViewRecentEmployment.DataBind();

                uoImageCM.ImageUrl = "~/Images/no-profile-image.jpg";
                
                
                uoHiddenFieldAppDecMessage.Value = "";

                tblUCDate.Text = DateTime.Now.ToString("MM/dd/yyyy") + " [" + DateTime.Now.ToString("hh:mm tt") + "]";
                Session["immigration"] = null;

            }
            catch (Exception ex)
            {
                throw ex; 
            }
            
        }

        protected void btnClear_click(object sender, EventArgs e)
        {
            ClearGridCrewVerification();
            txtUniqeID.Text = ""; 
        
        }

        protected string GetRequestColor()
        {

            var ColorCode = Eval("ColorCode");
            var ForCode = Eval("ForeColor");

            if (ColorCode.ToString() != "" && ForCode.ToString() != "")
            {
                ColorCode = ColorCode + "; color:" + ForCode + ";\"";

                //return ColorCode = ColorCode + ";" + "";
                return "<tr style=\" background-color:" + ColorCode + ">";
            }
            else if (ColorCode.ToString() != "" && ForCode.ToString() == "")
            {
                ColorCode = ColorCode + ";\" ";

                //return ColorCode = ColorCode + ";" + "";
                return "<tr style=\" background-color:" + ColorCode + ">";
            }
            else
            {
                //return "<tr style=\" background-color:" + ColorCode + ">";
                return "<tr>";
            }


        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            try
            {
                string LoeNo = "";
                string IDNumber = "";

                ImmigrationBLL BLL = new ImmigrationBLL();
                LoeNo = txtLOEControlNo.Text;  
                IDNumber = txtUniqeID.Text;

                if (IDNumber == "" && LoeNo == "")
                {
                    AlertMessage("Please enter Unique ID or LOE Control No to search crew verification.");
                    return; 
                }
                ClearGridCrewVerification();

                List<CrewImmigration> Immigration = new List<CrewImmigration>();
                Immigration = BLL.CrewImmigration(0, GlobalCode.Field2Long(IDNumber), 0, LoeNo, "");
                
                Session["immigration"] = Immigration;

                if (Immigration.Count == 0)
                {
                    if (GlobalCode.Field2Long(IDNumber) > 0 && LoeNo == "")

                    {
                        txtUniqeID.Text = IDNumber.ToString(); 
                        AlertMessage("Unique ID does not exist in the system.");
                        txtUniqeID.Text = ""; 
                    }
                    
                    else if (IDNumber == "" && LoeNo != "")
                    {
                        txtLOEControlNo.Text = LoeNo;
                        AlertMessage("This LOE is either invalid or does not exists in the system, please try search by Unique ID..");
                        txtLOEControlNo.Text ="";
                    }
                    else if (GlobalCode.Field2Long(IDNumber) > 0 && LoeNo != "")
                    {
                        txtLOEControlNo.Text = LoeNo;
                        txtUniqeID.Text = IDNumber.ToString();  
                        AlertMessage("This Unique ID and LOE combination does not exist in our system, please try seaching by Unique ID only");
                        txtLOEControlNo.Text = "";
                        txtUniqeID.Text = "";  
                    
                    }
                    return; 
                }

                crewverification(Immigration , 0);

                PageAsyncTask TaskPort1 = new PageAsyncTask(OnBeginExceptions, OnEndExceptions, null, "Async1", true);
                Page.RegisterAsyncTask(TaskPort1);

            }
            catch (Exception ex)
            { throw ex; }
        
        }

        
        public void ExecuteAsyncTaskExceptions() { }
        public IAsyncResult OnBeginExceptions(object sender, EventArgs e, AsyncCallback cb, object extraData)
        {
            _dlgtSeafarer = new AsyncTaskDelegate(ExecuteAsyncTaskExceptions);
            IAsyncResult result = _dlgtSeafarer.BeginInvoke(cb, extraData);
            return result;
        }
        public void OnEndExceptions(IAsyncResult ar)
        {



            List<SeafarerImage> SeafarerImage = new List<SeafarerImage>(); 
            QRCodeEncoder rq = new QRCodeEncoder();

            Bitmap myImage;
            List<CrewImmigration> Immigration = new List<CrewImmigration>();

            Immigration = (List<CrewImmigration>)Session["immigration"];
            for (var i = 0; Immigration.Count > i; i++)
            {


                myImage = rq.Encode(txtUniqeID.Text + "+" + Immigration[i].LOEControlNumber + "+" + Immigration[i].Joindate.Value.ToString("MMMM")   + " " + Immigration[i].Joindate.Value.Day.ToString() + " " + Immigration[i].Joindate.Value.Year.ToString()    + "+" + Immigration[i].Vessel + "+" + Immigration[i].Seaport);


                SeafarerImage.Add(new SeafarerImage
                {
                    Image = GlobalCode.Field2BitmapByte(myImage),
                    ImageType = "jpg",
                    LOENumber = Immigration[i].LOEControlNumber,
                    SeaparerID = GlobalCode.Field2Long(txtUniqeID.Text),
                    LogInUser = GlobalCode.Field2String(Session["UserName"])
                });
            }



            ImmigrationBLL BLL = new ImmigrationBLL();
            BLL.InsertQRCode(SeafarerImage);


                //foreach (ListViewItem item in Immigration.Items)
                //{
                //    txtJoindate = (TextBox)item.FindControl("txtJoindate");
                //    txtJoinShip = (TextBox)item.FindControl("txtJoinShip");
                //    txtJoinPort = (TextBox)item.FindControl("txtJoinPort");
                //    HPLOENumber = (HiddenField)item.FindControl("uoHiddenFieldControlNo");

                //    myImage = rq.Encode(txtUniqeID.Text + "+" + HPLOENumber.Value + "+" + txtJoindate.Text + "+" + txtJoinShip.Text + "+" + txtJoinPort.Text);


                //    SeafarerImage.Add(new SeafarerImage
                //    {
                //        Image = GlobalCode.Field2BitmapByte(myImage),
                //        ImageType = "jpg",
                //        LOENumber = HPLOENumber.Value,
                //        SeaparerID = GlobalCode.Field2Long(txtUniqeID.Text)
                //    });

                //}

         


            //if (immigration[0].SeafarerImage.Count > 0)
            //{
            //    uoImageCM.ImageUrl = GlobalCode.Field2PictureImage(GlobalCode.Field2BitmapByte(myImage), "jpg");
            //}

        }
         


        protected void uoListViewCrewverification_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //set current page startindex, max rows and rebind to false
            uoDataPagerCrewVerification.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            //rebind List View
            List<CrewImmigration> immigration = new List<CrewImmigration>();

            immigration = (List<CrewImmigration>)Session["immigration"];
            if (immigration != null)
            {
                crewverification(immigration, e.StartRowIndex);
            }
            else
            {
                ClearGridCrewVerification();
            
            }
        }

        bool IsImage(object imge)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }

        }


        void crewverification(List<CrewImmigration> immigration, int RowIndex)
        {

           
            tblUCDate.Text = DateTime.Now.ToString("MM/dd/yyyy") + " [" + DateTime.Now.ToString("hh:mm tt") + "]";

            if (immigration.Count > 0)
            {
                txtUniqeID.Text = immigration[RowIndex].SeaparerID.ToString();
                txtTelephone.Text = immigration[RowIndex].ContactNo;

                txtPassportNo.Text = immigration[RowIndex].PassportNo;
                txtNationality.Text = immigration[RowIndex].Nationality;
                txtLastName.Text = immigration[RowIndex].LastName;
                txtFirstName.Text = immigration[RowIndex].FirstName;
                txtLOEControlNo.Text = immigration[RowIndex].LOEControlNumber;
                txtExpiration.Text = immigration[RowIndex].PassportExpiredate;
                txtDateOffBirth.Text = immigration[0].DateOfBirth == null ? "" : GlobalCode.Field2DateTime(immigration[0].DateOfBirth).ToString("MM/dd/yyyy");

                txtOtherComment.Text = immigration[RowIndex].OtherDetail;
                uoHiddenFieldOldOther.Value = txtOtherComment.Text;
                txtEmailAdd.Text = immigration[RowIndex].EmailAdd;

                //txtBrand.Text = immigration[0].Brand;
                //txtJoindate.Text = immigration[0].SignOnDate.ToString();
                //txtJoinShip.Text = immigration[0].Vessel;
                //txtJoinPort.Text = immigration[0].Seaport;
                //txtJoinCity.Text = immigration[0].Seaport;
                //txtPosition.Text = immigration[0].Rank;
                //txtPosition.Text = immigration[0].Rank;

                if (txtLOEControlNo.Text == "")
                {
                    txtLOEControlNo.BackColor = System.Drawing.Color.Red;
                    txtLOEControlNo.ToolTip = "No LOE Available";

                    immigration[0].SignOnDate = null;
                    immigration[0].DateHired = null;
                }
                else
                {

                    txtLOEControlNo.BackColor = System.Drawing.Color.White;
                    txtLOEControlNo.ToolTip = immigration[RowIndex].LOEControlNumber;

                    //btnApproved.Enabled = true;
                    //btnDecline.Enabled = true; 

                }

                uoListViewCrewverification.DataSource = immigration;
                uoListViewCrewverification.DataBind();

                rdbFraudulentDoc.Checked = GlobalCode.Field2Bool(immigration[RowIndex].IsFraudulentDoc);
                rdbOther.Checked = GlobalCode.Field2Bool(immigration[RowIndex].IsOther);
                rdbPriorConDep.Checked = GlobalCode.Field2Bool(immigration[RowIndex].IsPriorConDep);
                rdbPriorIIssue.Checked = GlobalCode.Field2Bool(immigration[RowIndex].IsPriorImmigIssues);
                chkNew.Checked = GlobalCode.Field2Bool(immigration[RowIndex].NewHire);
                chkReHire.Checked = GlobalCode.Field2Bool(immigration[RowIndex].NewHire) == true ? false : true;
                uoHDFCrewVericationID.Value = immigration[RowIndex].CrewVericationID.ToString();

                //uoHiddenFieldServerdate.Value = immigration[RowIndex].ProcessDate.ToString();  

                if (immigration[RowIndex].CrewVericationID > 0)
                {
                    if (immigration[RowIndex].IsApproved == true)
                    {
                        uoHiddenFieldAppDecMessage.Value = "This immigration entry was approved by " + immigration[RowIndex].UserName + " on " + GlobalCode.Field2TimeZoneTime(GlobalCode.Field2DateTime(immigration[RowIndex].ProcessDate), uoHiddenFieldTimeZoneID.Value).ToString() + " are you sure you would like to ";

                        tblUCDate.Text = DateTime.Now.ToString("MM/dd/yyyy") + " [" + DateTime.Now.ToString("hh:mm tt") + "]" + "       Assignment Status : Approved   ";
                    }
                    else
                    {

                        uoHiddenFieldAppDecMessage.Value = "This immigration entry was declined by " + immigration[RowIndex].UserName + " on " + GlobalCode.Field2TimeZoneTime(GlobalCode.Field2DateTime(immigration[RowIndex].ProcessDate), uoHiddenFieldTimeZoneID.Value).ToString() + " are you sure you would like to ";
                        tblUCDate.Text = DateTime.Now.ToString("MM/dd/yyyy") + " [" + DateTime.Now.ToString("hh:mm tt") + "]" + "       Assignment Status : Declined   ";

                    }
                }
                else
                {
                    tblUCDate.Text = DateTime.Now.ToString("MM/dd/yyyy") + " [" + DateTime.Now.ToString("hh:mm tt") + "]";
                }

                //if (HiddenFieldIsImage.Value == "0") 
                //{
                //    if (immigration[0].SeafarerImage.Count > 0)
                //    {
                //        uoImageCM.ImageUrl = GlobalCode.Field2PictureImage(immigration[0].SeafarerImage[0].Image, immigration[0].SeafarerImage[0].ImageType);
                //    }
                //    else {
                //        uoImageCM.ImageUrl = "~/Images/no-profile-image.jpg";
                //    }
                //}
                //else
                //{
                //    uoImageCM.ImageUrl = HiddenFieldCMImage.Value;
                //}

                string URL = ConfigurationManager.AppSettings["MediaURL"];
                string Token = ConfigurationManager.AppSettings["MediaToken"];

                VehicleImageFile img = new VehicleImageFile();
                GlobalCode k = new GlobalCode();
                img = k.GetPhoto(URL + "/avatars/jde/" + txtUniqeID.Text.ToString() + "?at=" + Token);


                if (img.Image == null)
                {
                    if (GlobalCode.Field2Long(immigration[0].CtracDetail.user_id) > 0)
                    {
                        VehicleImageFile cimg = new VehicleImageFile();
                        cimg = k.GetPhoto(URL + "/avatars/ctrac/" + GlobalCode.Field2String(immigration[0].CtracDetail.user_id) + "?at=" + Token);
                        if (cimg.Image != null)
                        {
                            uoImageCM.ImageUrl = "data:image/*;base64," + cimg.Image;
                        }
                        else
                        {
                            if (immigration[0].SeafarerImage.Count > 0)
                            {
                                uoImageCM.ImageUrl = GlobalCode.Field2PictureImage(immigration[0].SeafarerImage[0].Image, immigration[0].SeafarerImage[0].ImageType);
                            }
                            else
                            {
                                uoImageCM.ImageUrl = "~/Images/no-profile-image.jpg";
                            }
                        }
                    }
                    else {
                        if (immigration[0].SeafarerImage.Count > 0)
                        {
                            uoImageCM.ImageUrl = GlobalCode.Field2PictureImage(immigration[0].SeafarerImage[0].Image, immigration[0].SeafarerImage[0].ImageType);
                        }
                        else
                        {
                            uoImageCM.ImageUrl = "~/Images/no-profile-image.jpg";
                        }
                    }

                }
                else
                {
                    uoImageCM.ImageUrl = "data:image/*;base64," + img.Image;
                }

                if (immigration[0].ImmigrationAirTransaction.Count > 0)
                {
                    uoListviewAir.DataSource = immigration[0].ImmigrationAirTransaction;
                    uoListviewAir.DataBind();
                }
                else
                {
                    uoListviewAir.DataSource = null;
                    uoListviewAir.DataBind();
                }

                if (immigration[0].ImmigrationHotelBooking.Count > 0)
                {
                    uoListViewHotelBook.DataSource = immigration[0].ImmigrationHotelBooking;
                    uoListViewHotelBook.DataBind();
                }
                else
                {
                    uoListViewHotelBook.DataSource = null;
                    uoListViewHotelBook.DataBind();
                }

                if (immigration[0].ImmigrationTransportion.Count > 0)
                {
                    uoListViewTransportation.DataSource = immigration[0].ImmigrationTransportion;
                    uoListViewTransportation.DataBind();
                }
                else
                {
                    uoListViewTransportation.DataSource = null;
                    uoListViewTransportation.DataBind();
                }

                if (immigration[0].ImmigrationTransportion.Count > 0)
                {
                    uoListViewTransportation.DataSource = immigration[0].ImmigrationTransportion;
                    uoListViewTransportation.DataBind();
                }
                else
                {
                    uoListViewTransportation.DataSource = null;
                    uoListViewTransportation.DataBind();
                }


                if (immigration[0].ImmigrationEmploymentHistory.Count > 0)
                {
                    uoListViewRecentEmployment.DataSource = immigration[0].ImmigrationEmploymentHistory;
                    uoListViewRecentEmployment.DataBind();
                }
                else
                {
                    uoListViewRecentEmployment.DataSource = null;
                    uoListViewRecentEmployment.DataBind();
                }

                if (immigration[0].Parent.Count > 0)
                {
                    uoListViewParent.DataSource = immigration[0].Parent;
                    uoListViewParent.DataBind();
                }
                else
                {
                    uoListViewParent.DataSource = null;
                    uoListViewParent.DataBind();
                }


            }
            else {
                uoImageCM.ImageUrl = "~/Images/no-profile-image.jpg";
            
            }
        }


        protected void btnApproved_click(object sender, EventArgs e)
        {
            try
            {
                ImmigrationBLL BLL = new ImmigrationBLL();

                if (uoListViewCrewverification.Items.Count == 0)
                {
                    AlertMessage("Crew member has no upcomming assigment, Please try other crew member! ");
                    return;
                }

                bool IsApproved = true;
                Button ApproveButton = (Button)sender;

                DateTime curDate  = new DateTime();
                
                //curDate = GlobalCode.Field2DateTime("2015-05-05 16:11:28.680");
                //DateTime T = GlobalCode.Field2TimeZoneTime(GlobalCode.Field2DateTime("2015-05-05 16:11:28.680") , );


                if (ApproveButton.ID == "btnApproved") {
                    IsApproved = true;
                    rdbFraudulentDoc.Checked = false;
                    rdbPriorIIssue.Checked = false;
                    rdbPriorConDep.Checked = false;
                    rdbOther.Checked = false;
                    txtOtherComment.Text = "";
                }
                else { 
                    IsApproved = false;
                }
                    

                HiddenField uoCrewVericationID = null ;
                HiddenField uoLOEControlNumber = null;

                HiddenField uoHDFVesselID = null;
                HiddenField uoHDFRankID = null;

                TextBox txtJoindate = null;
                TextBox txtJoinPort = null;
                TextBox txtJoinCity = null;

                foreach (ListViewItem item in uoListViewCrewverification.Items)
                {
                    uoLOEControlNumber = (HiddenField)item.FindControl("uoHiddenFieldControlNo");
                    uoCrewVericationID = (HiddenField)item.FindControl("uoHiddenFieldCrewVericationID");

                    txtJoindate = (TextBox)item.FindControl("txtJoindate");
                    txtJoinPort = (TextBox)item.FindControl("txtJoinPort");
                    txtJoinCity = (TextBox)item.FindControl("txtJoinCity");


                    uoHDFRankID = (HiddenField)item.FindControl("uoHDFRankID");
                    uoHDFVesselID = (HiddenField)item.FindControl("uoHDFVesselID");
                
                }




                List<CrewImmigration> Immigration = new List<CrewImmigration>();
                if (rdbOther.Checked == false)
                    txtOtherComment.Text = "";

                Immigration.Add(new CrewImmigration
                {
                    CrewVericationID = GlobalCode.Field2Long(uoCrewVericationID.Value),
                    SeaparerID = GlobalCode.Field2Long(txtUniqeID.Text),

                    TravelReqID  = GlobalCode.Field2Long(uoHDFCrewVericationID.Value),
                    RecordLocator  = GlobalCode.Field2String(uoHDFRecordLocator.Value),
                    ItinerarySeqNo = GlobalCode.Field2Int(uoHDFItinerarySeqID.Value),
                    LOEControlNumber = GlobalCode.Field2String(txtLOEControlNo.Text ),

                    VesselID = GlobalCode.Field2Int(uoHDFVesselID.Value),
                    RankID = GlobalCode.Field2Int(uoHDFRankID.Value),

                    Joindate = GlobalCode.Field2DateTime(txtJoindate.Text),
                    JoinPort = GlobalCode.Field2String(txtJoinPort.Text),
                    JoinCity = GlobalCode.Field2String(txtJoinCity.Text),

                    Reason = 0, 
                    IsFraudulentDoc  = GlobalCode.Field2Bool(rdbFraudulentDoc.Checked  ),
                    IsPriorImmigIssues = GlobalCode.Field2Bool(rdbPriorIIssue.Checked),
                    IsPriorConDep = GlobalCode.Field2Bool(rdbPriorConDep.Checked),
                    IsOther = GlobalCode.Field2Bool(rdbOther.Checked),
                    IsApproved = GlobalCode.Field2Bool(IsApproved),
                    OtherDetail  = GlobalCode.Field2String(txtOtherComment.Text ),
                    UserID = GlobalCode.Field2String(uoHDFuserID.Value),

                });
                Immigration = BLL.InsertCrewImmigration(Immigration);
                
                Session["immigration"] = Immigration;

                int row = 0;

                if (Immigration.Count > 1)
                {
                    foreach (CrewImmigration item in Immigration)
                    {
                        if (item.CrewVericationID == GlobalCode.Field2Long(uoCrewVericationID.Value))
                        {
                            row = row + 1;
                        }
                    }
                
                }

                crewverification(Immigration, row);

            }
            catch (Exception ex)
            { throw ex; }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class ServiceRequestEmail : System.Web.UI.Page
    {
        ServiceRequestBLL BLL = new ServiceRequestBLL();
        CrewAssistBLL CrewBLL = new CrewAssistBLL();
        CrewAssist crewAssistPage = new CrewAssist();

        #region "Event"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["hrID"] != null)
                {
                    uoHiddenFieldHotelRequestID.Value = Request.QueryString["hrID"].ToString();
                }
                if (Request.QueryString["trID"] != null)
                {
                    uoHiddenFieldTravelRequestID.Value = Request.QueryString["trID"].ToString();
                }
                if (Request.QueryString["SfID"] != null)
                {
                    uoTextBoxEmployeeID.Text = Request.QueryString["SfID"].ToString();
                }
                if (Request.QueryString["name"] != null)
                {
                    uoTextBoxName.Text = Request.QueryString["name"].ToString();
                }
                if (Request.QueryString["as"] != null)
                {
                    uoHiddenFieldSeqNo.Value = Request.QueryString["as"].ToString();
                }
                if (Request.QueryString["trp"] != null)
                {
                    uoHiddenFieldVehicleRequestID.Value = Request.QueryString["trp"].ToString();
                }
                if (Request.QueryString["MAid"] != null)
                {
                    uoHiddenFieldMeetGreetRequestID.Value = Request.QueryString["MAid"].ToString();
                }
                if (Request.QueryString["PAid"] != null)
                {
                    uoHiddenFieldPortAgentRequestID.Value = Request.QueryString["PAid"].ToString();
                }
                int iHotelReqID = GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value);
                int iVehicleReqID = GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value);
                int iMeetGreetReqID = GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value);
                int iPortAgentReqID = GlobalCode.Field2Int(uoHiddenFieldPortAgentRequestID.Value);


                if (iHotelReqID == 0)
                {
                    uoCheckBoxHotelRequest.Enabled = false;
                    uoTextBoxEmail.ReadOnly = true;
                    uoTextBoxEmail.CssClass = "ReadOnly";
                }
                if (iVehicleReqID == 0)
                {
                    uoCheckBoxVehicleRequest.Enabled = false;
                    uoTextBoxEmailVehicle.ReadOnly = true;
                    uoTextBoxEmailVehicle.CssClass = "ReadOnly";
                } 
                if (iPortAgentReqID == 0)
                {
                    uoCheckBoxPortAgentRequest.Enabled = false;
                    uoTextBoxEmailPortAgent.ReadOnly = true;
                    uoTextBoxEmailPortAgent.CssClass = "ReadOnly";
                }
                if (iMeetGreetReqID == 0)
                {
                    uoCheckBoxMeetGreetRequest.Enabled = false;
                    uoTextBoxEmailMeetGreet.ReadOnly = true;
                    uoTextBoxEmailMeetGreet.CssClass = "ReadOnly";
                }
                BindEmail(0);
            }
        }
        protected void uoButtonSend_Click(object sender, EventArgs e)
        {
            SendEmailButton();
        }
        #endregion 

        #region "Fucntions"
        /// <summary>
        /// Date Modified: 22/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get email values 
        /// -------------------------------------
        /// Date Modified: 25/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add vehicleTransactionList to get the Vehicle Transaction ID for email use
        /// </summary>
        private void BindEmail(Int16 iLoadType)
        {
            List<ServiceRequestEmailList> list = new List<ServiceRequestEmailList>();
            List<CopyEmail> copyMailList = new List<CopyEmail>();
            List<CrewAssistTranspo> vehicleTransactionList = new List<CrewAssistTranspo>();

            BLL.GetServiceRequestEmail(GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value),
                GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value),
                GlobalCode.Field2Int(uoHiddenFieldPortAgentRequestID.Value),
                GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value),iLoadType);

            if (iLoadType == 0)
            {
                if (Session["ServiceRequestEmail_EmailList"] != null)
                {
                    list = (List<ServiceRequestEmailList>)Session["ServiceRequestEmail_EmailList"];
                }
                if (Session["ServiceRequestEmail_CopyMailList"] != null)
                {
                    copyMailList = (List<CopyEmail>)Session["ServiceRequestEmail_CopyMailList"];
                }

                if (Session["ServiceRequestEmail_VehicleEmailDetails"] != null)
                {
                    vehicleTransactionList = (List<CrewAssistTranspo>)Session["ServiceRequestEmail_VehicleEmailDetails"];
                    if (vehicleTransactionList.Count > 0)
                    {
                        uoHiddenFieldVehicleTransID.Value = GlobalCode.Field2String(vehicleTransactionList[0].VehicleTransID);
                    }
                }
            }
            
            if (list.Count > 0)
            {
                uoTextBoxEmail.Text = GlobalCode.Field2String(list[0].HotelEmailTo);
                uoTextBoxEmailVehicle.Text = GlobalCode.Field2String(list[0].VehicleEmailTo);
                uoTextBoxEmailPortAgent.Text = GlobalCode.Field2String(list[0].PortAgentEmailTo);
                uoTextBoxEmailMeetGreet.Text = GlobalCode.Field2String(list[0].MeetGreetEmailTo);
                
                uoHiddenFieldHotelID.Value = list[0].HotelID.ToString();
                uoHiddenFieldVehicleID.Value = list[0].VehicleID.ToString();
                uoHiddenFieldPortAgentID.Value = list[0].PortAgentID.ToString();
                uoHiddenFieldMeetGreetID.Value = list[0].MeetGreetID.ToString();

                uoLabelHotelName.Text = list[0].HotelName;
                uoLabelVehicleName.Text = list[0].VehicleName;
                uoLabelPortAgentName.Text = list[0].PortAgentName;
                uoLabelMeetGreet.Text = list[0].MeetGreetName;

                uoHiddenFieldCopyShip.Value = list[0].VesselEmailTo;
            }

            if (copyMailList.Count > 0)
            {
                uoHiddenFieldCrewAssist.Value = "";
                uoHiddenFieldEmailHotel.Value = "";
                uoHiddenFieldScheduler.Value = "";

                var emailCrewAssist = (from a in copyMailList
                                    where a.EmailName == "Crew Assist"
                                    select new CopyEmail
                                     {
                                         Email = a.Email,
                                         EmailName = a.EmailName,
                                         EmailType = a.EmailType,
                                     }).ToList();
                if (emailCrewAssist.Count > 0)
                {
                    uoHiddenFieldCrewAssist.Value = emailCrewAssist[0].Email;
                }
                
                var emailHotelRccl = (from a in copyMailList
                                      where a.EmailName == "Hotel"
                                       select new CopyEmail
                                       {
                                           Email = a.Email,
                                           EmailName = a.EmailName,
                                           EmailType = a.EmailType,
                                       }).ToList();
                if (emailHotelRccl.Count > 0)
                {
                    uoHiddenFieldEmailHotel.Value = emailHotelRccl[0].Email;
                }

                var emailScheduler = (from a in copyMailList
                                      where a.EmailName == "Scheduler"
                                      select new CopyEmail
                                      {
                                          Email = a.Email,
                                          EmailName = a.EmailName,
                                          EmailType = a.EmailType,
                                      }).ToList();
                if (emailScheduler.Count > 0)
                {
                    uoHiddenFieldScheduler.Value = emailScheduler[0].Email;
                }
            }
        }
        /// <summary>
        /// Date Modified: 22/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get email details of Hotel 
        /// </summary>
        //private List<CrewAssistEmailDetail> GetHotelDetails()
        //{
        //    List<CrewAssistEmailDetail> hotelEmail = new List<CrewAssistEmailDetail>();

        //    if (Session["ServiceRequestEmail_EmailList"] != null)
        //    {
        //        hotelEmail = (List<CrewAssistEmailDetail>)Session["ServiceRequestEmail_HotelEmailDetails"];
        //    }
        //    else
        //    {
        //        BLL.GetServiceRequestEmail(GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value),
        //          GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value),
        //          GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value), 1);

        //        if (Session["ServiceRequestEmail_EmailList"] != null)
        //        {
        //            hotelEmail = (List<CrewAssistEmailDetail>)Session["ServiceRequestEmail_HotelEmailDetails"];
        //        }
        //    }
        //    return hotelEmail;
        //}
        /// <summary>
        /// Date Modified: 22/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get email details of Hotel 
        /// </summary>
        //private List<CrewAssistTranspo> GetVehicleDetails()
        //{
        //    List<CrewAssistTranspo> list = new List<CrewAssistTranspo>();

        //    if (Session["ServiceRequestEmail_VehicleEmailDetails"] != null)
        //    {
        //        list = (List<CrewAssistTranspo>)Session["ServiceRequestEmail_VehicleEmailDetails"];
        //    }
        //    else
        //    {
        //        BLL.GetServiceRequestEmail(GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value),
        //          GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value),
        //          GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value), 1);

        //        if (Session["ServiceRequestEmail_VehicleEmailDetails"] != null)
        //        {
        //            list = (List<CrewAssistTranspo>)Session["ServiceRequestEmail_VehicleEmailDetails"];
        //        }
        //    }
        //    return list;
        //}
        /// <summary>
        /// Date Modified: 24/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get email details of Hotel 
        /// </summary>
        //private List<CrewAssistMeetAndGreet> GetMeetGreetDetails()
        //{
        //    List<CrewAssistMeetAndGreet> list = new List<CrewAssistMeetAndGreet>();

        //    if (Session["ServiceRequestEmail_MeetGreetEmailDetails"] != null)
        //    {
        //        list = (List<CrewAssistMeetAndGreet>)Session["ServiceRequestEmail_MeetGreetEmailDetails"];
        //    }
        //    else
        //    {
        //        BLL.GetServiceRequestEmail(GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value),
        //          GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value),
        //          GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value), 1);

        //        if (Session["ServiceRequestEmail_MeetGreetEmailDetails"] != null)
        //        {
        //            list = (List<CrewAssistMeetAndGreet>)Session["ServiceRequestEmail_MeetGreetEmailDetails"];
        //        }
        //    }
        //    return list;
        //}

        /// <summary>
        /// Date Modified: 23/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Set the control property
        /// </summary>
        private void SendEmailButton()
        { 
            uoTextBoxEmail.Text = uoHiddenFieldEmailHotelVendor.Value;
            uoTextBoxEmailVehicle.Text = uoHiddenFieldEmailVehicle.Value;
            uoTextBoxEmailMeetGreet.Text = uoHiddenFieldEmailMeetGreet.Value;
            uoTextBoxEmailOther.Text = uoHiddenFieldEmailOther.Value;

            if (GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value) > 0 ||
                GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value) > 0 ||
                GlobalCode.Field2Int(uoHiddenFieldPortAgentRequestID.Value) > 0 ||
                GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value) > 0 
                )
            {
                SendEmail();                
            }

            //Hotel Settings
            if (uoCheckBoxHotelRequest.Checked)
            {
                uoTextBoxEmail.ReadOnly = false;
                uoTextBoxEmail.Attributes.Remove("class");
            }
            else
            {
                uoTextBoxEmail.ReadOnly = true;
                uoTextBoxEmail.Attributes.Add("class", "ReadOnly");
            }
            //Vehicle Settings
            if (uoCheckBoxVehicleRequest.Checked)
            {
                uoTextBoxEmailVehicle.ReadOnly = false;
                uoTextBoxEmailVehicle.Attributes.Remove("class");
            }
            else
            {
                uoTextBoxEmailVehicle.ReadOnly = true;
                uoTextBoxEmailVehicle.Attributes.Add("class", "ReadOnly");
            }
            //Service Provider Settings
            if (uoCheckBoxPortAgentRequest.Checked)
            {
                uoTextBoxEmailPortAgent.ReadOnly = false;
                uoTextBoxEmailPortAgent.Attributes.Remove("class");
            }
            else
            {
                uoTextBoxEmailPortAgent.ReadOnly = true;
                uoTextBoxEmailPortAgent.Attributes.Add("class", "ReadOnly");
            }
            //Meet & Greet Settings
            if (uoCheckBoxMeetGreetRequest.Checked)
            {
                uoTextBoxEmailMeetGreet.ReadOnly = false;
                uoTextBoxEmailMeetGreet.Attributes.Remove("class");
            }
            else
            {
                uoTextBoxEmailMeetGreet.ReadOnly = true;
                uoTextBoxEmailMeetGreet.Attributes.Add("class", "ReadOnly");
            }
        }
        /// <summary>
        /// Date Modified: 23/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Email Hotel Request
        /// </summary>
        private void SendEmail()
        {
            try
            {
                //List<CrewAssistEmailDetail> hotelEmailList = new List<CrewAssistEmailDetail>();
                //List<CrewAssistTranspo> vehicleEmailList = new List<CrewAssistTranspo>();
                //List<CrewAssistMeetAndGreet> meetGreetEmailList = new List<CrewAssistMeetAndGreet>();

                //hotelEmailList = GetHotelDetails();
                //vehicleEmailList = GetVehicleDetails();
                //meetGreetEmailList = GetMeetGreetDetails();

                string cssEmail = "";

                if (CheckBoxCopycrewassist.Checked == true && uoHiddenFieldCrewAssist.Value != string.Empty)
                {
                    cssEmail = uoHiddenFieldCrewAssist.Value.ToString() + ", ";
                }
                if (CheckBoxCopycrewhotels.Checked == true && uoHiddenFieldEmailHotel.Value != string.Empty)
                {
                    cssEmail += uoHiddenFieldEmailHotel.Value.ToString() + ", ";
                }
                if (CheckBoxCopyShip.Checked == true && uoHiddenFieldCopyShip.Value != string.Empty)
                {
                    cssEmail += uoHiddenFieldCopyShip.Value.ToString() + ", ";
                }
                if (CheckBoxScheduler.Checked == true && uoHiddenFieldScheduler.Value != string.Empty)
                {
                    cssEmail += uoHiddenFieldScheduler.Value.ToString() + ", ";
                }
                if (uoTextBoxEmailOther.Text != string.Empty)
                {
                    cssEmail += uoTextBoxEmailOther.Text.ToString();
                }

                string[] separators = { ",", ";", " " };
                string ccrecipient = "";
                string[] ccmail = cssEmail.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in ccmail)
                {
                    ccrecipient += word + ", ";
                }

                if (ccrecipient != "")
                {
                    ccrecipient = ccrecipient.Substring(0, ccrecipient.Length - 2).ToString();
                }

                string sHotelRecipient = "";
                string sVehicleRecipient = "";
                string sPortAgentRecipient = "";
                string sMeetGreetRecipient = "";


                if (uoCheckBoxHotelRequest.Checked && uoTextBoxEmail.Text.Trim() != "")
                {
                    string sToEmailHotel = uoTextBoxEmail.Text.Trim();
                    string[] sToEmailHotelArr = sToEmailHotel.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in sToEmailHotelArr)
                    {
                        if (sHotelRecipient != "")
                        {
                            sHotelRecipient += ", ";
                        }
                        sHotelRecipient += word;
                    }
                }

                if (uoCheckBoxVehicleRequest.Checked && uoTextBoxEmailVehicle.Text.Trim() != "")
                {
                    string sToEmailVehicle = uoTextBoxEmailVehicle.Text.Trim();
                    string[] sToEmailVehicleArr = sToEmailVehicle.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in sToEmailVehicleArr)
                    {
                        if (sVehicleRecipient != "")
                        {
                            sVehicleRecipient += ", ";
                        }
                        sVehicleRecipient += word;
                    }
                }

                if (uoCheckBoxPortAgentRequest.Checked && uoTextBoxEmailPortAgent.Text.Trim() != "")
                {
                    string sToEmailPortAgent = uoTextBoxEmailPortAgent.Text.Trim();
                    string[] sToEmailPortAgentArr = sToEmailPortAgent.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in sToEmailPortAgentArr)
                    {
                        if (sPortAgentRecipient != "")
                        {
                            sPortAgentRecipient += ", ";
                        }
                        sPortAgentRecipient += word;
                    }
                }

                if (uoCheckBoxMeetGreetRequest.Checked && uoTextBoxEmailMeetGreet.Text.Trim() != "")
                {
                    string sToEmailMeetGreet = uoTextBoxEmailMeetGreet.Text.Trim();
                    string[] sToEmailMeetGreetArr = sToEmailMeetGreet.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in sToEmailMeetGreetArr)
                    {
                        if (sMeetGreetRecipient != "")
                        {
                            sMeetGreetRecipient += ", ";
                        }
                        sMeetGreetRecipient += word;
                    }
                }

                string sAlert = "";
                int iHotelReqID = GlobalCode.Field2Int(uoHiddenFieldHotelRequestID.Value);
                int iVehicleTransID = GlobalCode.Field2Int(uoHiddenFieldVehicleTransID.Value);
                int iPortAgentReqID = GlobalCode.Field2Int(uoHiddenFieldPortAgentRequestID.Value);
                int iMeetGreetReqID = GlobalCode.Field2Int(uoHiddenFieldMeetGreetRequestID.Value);

                if (uoCheckBoxHotelRequest.Checked && sHotelRecipient != "")
                {
                    SendEmailHotel(iHotelReqID, sHotelRecipient, ccrecipient, "" );
                    sAlert = " 'Hotel Request email sent!' ";
                }
                if (uoCheckBoxVehicleRequest.Checked && sVehicleRecipient != "")
                {
                    SendEmailTransport(iVehicleTransID, sVehicleRecipient, ccrecipient, "");
                    if (sAlert != "")
                    {
                        sAlert += " + '\\nVehicle Request email sent!' ";
                    }
                    else
                    {
                        sAlert = " 'Vehicle Request email sent!' ";
                    }
                }
                if (uoCheckBoxPortAgentRequest.Checked && sPortAgentRecipient != "")
                {
                    SendEmailPortAgent(iPortAgentReqID, sPortAgentRecipient, ccrecipient, "");
                    if (sAlert != "")
                    {
                        sAlert += " + '\\nPort Agent Service Request email sent!' ";
                    }
                    else
                    {
                        sAlert = " 'Port Agent Service Request email sent!' ";
                    }
                }
                if (uoCheckBoxMeetGreetRequest.Checked && sMeetGreetRecipient != "")
                {
                    SendEmailMeetGreet(iMeetGreetReqID, sMeetGreetRecipient, ccrecipient, "");
                    if (sAlert != "")
                    {
                        sAlert += " + '\\nMeet & Greet Request email sent!' ";
                    }
                    else
                    {
                        sAlert = " 'Meet & Greet Request email sent!' ";
                    }
                }
                //if (hotelEmailList.Count > 0 && uoCheckBoxHotelRequest.Checked && sHotelRecipient != "")
                //{
                //    crewAssistPage.SendEmail("Hotel Request", hotelEmailList, "", sHotelRecipient, ccrecipient, "Hotel", hotelEmailList[0].Confirmedbyhotelvendor);
                //    sAlert = " 'Hotel Request email sent!' ";
                //}
                //if (vehicleEmailList.Count > 0 && uoCheckBoxVehicleRequest.Checked && sVehicleRecipient != "")
                //{
                //    //crewAssistPage.SendTransEmail("Vehicle Request", vehicleEmailList, "", sVehicleRecipient, ccrecipient,"");
                //    int iTransVendorID = GlobalCode.Field2Int(uoHiddenFieldVehicleRequestID.Value);
                //    SendEmailTransport(iTransVendorID, sVehicleRecipient, ccrecipient, "", vehicleEmailList[0].ConfirmBy);
                //    if (sAlert != "")
                //    {
                //        sAlert += " + '\\nVehicle Request email sent!' ";
                //    }
                //    else
                //    {
                //        sAlert = " 'Vehicle Request email sent!' ";
                //    }
                //}
                //if (uoCheckBoxPortAgentRequest.Checked && sMeetGreetRecipient != "")
                //{
                //    int iTransPortAgentID = GlobalCode.Field2Int(uoHiddenFieldPortAgentRequestID.Value);
                //    SendEmailPortAgent(iTransPortAgentID, sPortAgentRecipient, ccrecipient, "");
                //    if (sAlert != "")
                //    {
                //        sAlert += " + '\\nPort Agent Service Request email sent!' ";
                //    }
                //    else
                //    {
                //        sAlert = " 'Port Agent Service Request email sent!' ";
                //    }
                //}
                //if (meetGreetEmailList.Count > 0 && uoCheckBoxMeetGreetRequest.Checked && sMeetGreetRecipient != "")
                //{
                //    crewAssistPage.SendMeetAndGreetEmail("Meet & Greet Request", meetGreetEmailList, "", sMeetGreetRecipient, ccrecipient);
                //    if (sAlert != "")
                //    {
                //        sAlert += " + '\\nMeet & Greet Request email sent!' ";
                //    }
                //    else
                //    {
                //        sAlert = " 'Meet & Greet Request email sent!' ";
                //    }
                //}
                AlertMessage(sAlert);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s = s.Replace("'", "");
                s = s.Replace("\"", "");

                AlertMessage(s);
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Oct/2013
        /// Description:    pop up alert message
        /// </summary>
        /// <param name="s"></param>
        private void AlertMessage(string s)
        {
           // s = s.Replace("'", "");
            //s = s.Replace("\"", "");

            string sScript = "<script language='JavaScript'>";
            sScript += " var msg = " + s + " ; ";
            sScript += "alert( msg );";            
            sScript += " parent.$.fancybox.close(); ";
            //sScript += " self.close(); ";

            sScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scr", sScript, false);
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   18/Nov/2013
        /// Description:    Send Email for Hotel Service Request
        /// </summary>        
        private void SendEmailHotel(int  iRequestID, string Recipient, string CCopy, string BlindCopy)
        {
            string msg = "";
            msg = CrewBLL.SendEmailHotel(iRequestID, "", Recipient, CCopy, BlindCopy);
            if (msg != "" && Recipient.Length > 0 && uoCheckBoxVehicleRequest.Checked == true)
            {
                crewAssistPage.SendEmail("RCL Confirmation – Hotel Accommodations Request ", msg, "", Recipient, CCopy, "Hotel", "");                
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   18/Nov/2013
        /// Description:    Send Email for Vehicle Service Request
        /// </summary>        
        private void SendEmailTransport(int TransVendorID, string Recipient, string CCopy, string BlindCopy)
        {
            string msg = "";
            msg = CrewBLL.SendEmailTransport(TransVendorID, "", Recipient, CCopy, BlindCopy);
            if (msg != "" && Recipient.Length > 0 && uoCheckBoxVehicleRequest.Checked == true)
            {
                crewAssistPage.SendTransEmail("RCL Confirmation – Transportation Request ", msg, "", Recipient, CCopy, "");
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   18/Nov/2013
        /// Description:    Send Email for Port Agent Service Request
        /// </summary>        
        private void SendEmailPortAgent(int TransPortAgentID, string Recipient, string CCopy, string BlindCopy)
        {
            string msg = "";
            msg = CrewBLL.SendEmailPortAgent(TransPortAgentID, "", Recipient, CCopy, BlindCopy);
            if (msg != "" && Recipient.Length > 0 && uoCheckBoxVehicleRequest.Checked == true)
            {
                crewAssistPage.SendPortAgenEmail("RCL Confirmation – Port Agent Service Request ", msg, "", Recipient, CCopy);
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Nov/2013
        /// Description:    Send Email for Meet & Greet Service Request
        /// </summary>        
        private void SendEmailMeetGreet(int MeetGreetReqID, string Recipient, string CCopy, string BlindCopy)
        {
            //string msg = "";
            //msg = CrewBLL.SendEmailPortAgent(MeetGreetReqID, "", Recipient, CCopy, BlindCopy);
            //if (msg != "" && Recipient.Length > 0 && uoCheckBoxVehicleRequest.Checked == true)
            //{
            //    crewAssistPage.SendPortAgenEmail("RCL Confirmation – Port Agent Service Request ", msg, "", Recipient, CCopy);
            //}
        }
        #endregion
    }
}
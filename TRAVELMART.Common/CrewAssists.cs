using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mime;
using System.Globalization;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.IO;
using System.Security.Cryptography;


namespace TRAVELMART.Common
{

    //public class CrewAssistEmail
    //{
    //    /// <summary>

    //    /// </summary>
    //    /// <param name="sFrom"></param>
    //    /// <param name="sTo"></param>
    //    /// <param name="sSubject"></param>
    //    /// <param name="sMessage"></param>
    //    public void SendEmail(string sFrom, string sTo, string CC, string sSubject, string sMessage)
    //    {
    //        try
    //        {

    //            string sPassphrase = "Great Vacations Begin With Great Employees!";
    //            string sUser = DecryptString(ConfigurationSettings.AppSettings["TravelmartMailUser"].ToString(), sPassphrase);
    //            string sPwd = DecryptString(ConfigurationSettings.AppSettings["TravelmartMailPassword"].ToString(), sPassphrase);

    //            string sEmail = ConfigurationSettings.AppSettings["RCCLSupportEmail"].ToString();

    //            if (sTo != "")
    //            {
    //                string sServer = ConfigurationSettings.AppSettings["TravelmartMailServer"].ToString();
    //                int iPort = GlobalCode.Field2Int(ConfigurationSettings.AppSettings["TravelmartMailPort"]);

    //                sFrom = sEmail;

    //                //MailMessage mM = new MailMessage();
    //                MailMessage msg = new MailMessage();
    //                msg.From = new MailAddress(sFrom, "CrewAssist@rccl.com");

    //                msg.To.Add(sTo);
    //                msg.ReplyTo = new MailAddress("CrewAssist@rccl.com");

    //                if (CC != "") msg.CC.Add(CC);

    //                msg.Subject = sSubject;
    //                msg.IsBodyHtml = true;

    //                msg.Body += sMessage.ToString();
    //                msg.Priority = MailPriority.High;

    //                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(sServer, iPort);
    //                client.UseDefaultCredentials = false;
    //                client.Credentials = new System.Net.NetworkCredential(sUser, sPwd);
    //                client.Port = iPort;
    //                client.Host = sServer;
    //                client.EnableSsl = true;
    //                //object userstate = msg;
    //                client.Send(msg);



    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //}
    //public class CrewAssistGenericClass
    //{
    //    public List<CrewAssitPortList> CrewAssitPortList { get; set; }
    //    public List<CrewAssitExpendTypeList> CrewAssitExpendTypeList { get; set; }
    //    public List<CrewAssistHotelList> CrewAssistHotelList { get; set; }
    //    public List<CrewAssistCurrency> CrewAssistCurrency { get; set; }
    //    public List<CrewAssistRout> CrewAssistRout { get; set; }
    //    public List<VehicleVendor> VehicleVendor { get; set; }
    //    public List<CrewAssistNationality> CrewAssistNationality { get; set; }
    //    public List<CrewAssistMeetAndGreetVendor> CrewAssistMeetAndGreetVendor { get; set; }
    //    public List<CrewAssistVendorPortAgent> CrewAssistVendorPortAgent { get; set; }

    //    public List<CrewAssistSafeguardVendor> CrewAssistSafeguardVendor { get; set; }


    //    public List<CrewAssitPortList> CrewAssistAirport { get; set; }

    //}


    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load Rout
    ///// </summary>
    ///// 
    //[Serializable]
    //public class CrewAssistNationality
    //{

    //    public int NatioalityID { get; set; }
    //    public string NationalityCode { get; set; }
    //    public string Nationality { get; set; }
    //}


    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load Rout
    ///// </summary>
    ///// 
    //[Serializable]
    //public class CrewAssistRout
    //{
    //    public int RoutId { get; set; }
    //    public string RoutName { get; set; }
    //}

    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load user port
    ///// </summary>
    ///// 
    //[Serializable]
    //public class CrewAssitPortList
    //{
    //    public int PortId { get; set; }
    //    public string PortName { get; set; }
    //    public string PortCode { get; set; }
    //}


    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load user port
    ///// </summary>
    ///// 
    //[Serializable]
    //public class CrewAssitPortListClass
    //{

    //    public List<CrewAssitPortList> SeaportList { get; set; }
    //    public List<CrewAssitPortList> AirportList { get; set; }

    //}


    ///// <summary>
    ///// Author:         Muhallidin G Wali
    ///// Date Created:   08/02/2013
    ///// Description:    Load user port
    ///// </summary>
    ///// 
    //public class CrewAssitExpendTypeList
    //{
    //    public int ExpendTypeID { get; set; }
    //    public string ExpendType { get; set; }
    //    public bool? IsSelected { get; set; }
    //}

    ///// <summary>
    ///// Date Created:   08/02/2013
    ///// Created By:     Muhallidin G wali
    ///// (description)   Set Hotel class 
    ///// </summary>
    ///// 
    //[Serializable]
    //public class CrewAssistHotelList
    //{
    //    public int HotelID { get; set; }
    //    public string HotelName { get; set; }
    //    public string Portcode { get; set; }
    //    public bool? IsPortAgent { get; set; }
    //}

    ///// <summary>
    ///// Date Created:   08/02/2013
    ///// Created By:     Muhallidin G wali
    ///// (description)   Set Hotel class 
    ///// </summary>
    ///// 
    //public class CrewAssistCurrency
    //{
    //    public int? CurrencyID { get; set; }
    //    public string CurrencyCode { get; set; }
    //    public string CurrencyName { get; set; }
    //}


    [Serializable]
    public class CrewMemberInformation
    {

        public long? SeafarerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string NationalityCode { get; set; }
        public string Nationality { get; set; }
        public int? NationalityID { get; set; }


        public List<CrewAssisRemark> Remark { get; set; }
        public List<CrewSchedule> CrewSchedule { get; set; }
        public List<HotelTransactionMedical> HotelTransactionMedical { get; set; }
        public List<VehicleTransactionMedical> VehicleTransactionMedical { get; set; }

    }


    [Serializable]
    public class CrewSchedule
    {
        
        public long? TravelRequestID { get; set; }
        public long? IDBigint { get; set; }
        public int? SeqNo { get; set; }

        public string RecordLocator { get; set; }

        public int? VesselID { get; set; }
        public string Vessel { get; set; }
        public string VesselCode { get; set; }
        public string Status { get; set; }

        public DateTime?  SignOnOffDate { get; set; }

        public int? PortID { get; set; }
        public string Port { get; set; }
        public string PortCode { get; set; }

        public int? RankID { get; set; }
        public string RankCode { get; set; }
        public string Rank { get; set; }

        public int? BrandID { get; set; }
        public string Brand { get; set; }

        public string ReasonCode { get; set; }
        public int? CostcenterID { get; set; }   
        public string Costcenter { get; set; }   

        public string LOEStatus { get; set; }
        public DateTime? LOEDate { get; set; }
        public string LOEImmigrationOfficer { get; set; }
        public string LOEImmigrationPlace { get; set; }
        public string LOEReason { get; set; }
        public bool? IsConfirm { get; set; }

    }



    [Serializable]
    public class CrewAssistCMAirTransaction
    {
        public long TravelReqId { get; set; }
        public string RecordLocator { get; set; }
        public int ItinerarySeqNo { get; set; }
        public long IdBigint { get; set; }
        public int SeqNo { get; set; }

        public string AirlineCode { get; set; }

        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string DepartureAirportLocationCode { get; set; }
        public string ArrivalAirportLocationCode { get; set; }



        public string SeatNo { get; set; }
        public string TicketNo { get; set; }
        public string AirStatus { get; set; }
        public bool IsBilledToCrew { get; set; }
        public string ActionCode { get; set; }
        public int OrderNo { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }

        public string FligthNo { get; set; }
  
 
        public bool? IsSelected { get; set; }
    } 

     
    /// <summary>
    /// Date Created:  26/08/2014
    /// Created By:    Muhallidin G Wali
    /// (description) 
    /// </summary>
    [Serializable]
    public class CrewAssistCMTransaction
    {


        public List<CrewAssistHotelBooking> CrewAssistHotelBooking
        {
            get;
            set;
        }


        public List<CrewAssistTranspo> CrewAssistTranspo
        {
            get;
            set;
        }

        public List<CrewAssistMeetAndGreet> CrewAssistMeetAndGreet
        {
            get;
            set;
        }

        public List<CrewAssistPortAgentRequest> CrewAssistPortAgentRequest
        {
            get;
            set;
        }

        public List<CrewAssistSafeguardRequest> CrewAssistSafeguardRequest
        {
            get;
            set;
        }

        

        public List<CopyEmail> CopyEmail
        {
            get;
            set;
        }


        public List<CrewAssisRemark> HotelRemark
        {
            get;
            set;
        }




        public List<HotelRequestCompanion> HotelRequestCompanion
        {
            get;
            set;
        }

        public List<CrewAssistCMAirTransaction> CrewAssistAirTransaction
        {
            get;
            set;
        }
        public List<CrewAssistGenericVendor> CrewAssistGenericVendor
        {
            get;
            set;
        }
    }



    public class CrewAssistGenericVendor 
    {
        public List<CrewAssistHotelList> listHotel { get; set; }
        public List<VehicleVendor> listVehicle { get; set; }
        public List<PortAgentDTO> listPortAgent { get; set; }
        public List<MeetAndGreetList> listMeetGreet { get; set; }
        public List<VendorSafeguardList> listSafeguard { get; set; }
        
    }




}

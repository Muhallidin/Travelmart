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

    

   public class CrewAssistEmail
   {
       /// <summary>

       /// </summary>
       /// <param name="sFrom"></param>
       /// <param name="sTo"></param>
       /// <param name="sSubject"></param>
       /// <param name="sMessage"></param>
       public void SendEmail(string sFrom, string sTo,string CC, string sSubject, string sMessage)
       {
           try
           {

               string sPassphrase = "Great Vacations Begin With Great Employees!";
               string sUser = DecryptString(ConfigurationSettings.AppSettings["TravelmartMailUser"].ToString(), sPassphrase);
               string sPwd = DecryptString(ConfigurationSettings.AppSettings["TravelmartMailPassword"].ToString(), sPassphrase);

               string sEmail = ConfigurationSettings.AppSettings["RCCLSupportEmail"].ToString();

               if (sTo != "")
               {
                   string sServer = ConfigurationSettings.AppSettings["TravelmartMailServer"].ToString();
                   int iPort = GlobalCode.Field2Int(ConfigurationSettings.AppSettings["TravelmartMailPort"]);

                   sFrom = sEmail;

                   //MailMessage mM = new MailMessage();
                   MailMessage msg = new MailMessage();
                   msg.From = new MailAddress(sFrom, "CrewAssist@rccl.com");
                  
                   msg.To.Add(sTo);
                   msg.ReplyTo = new MailAddress("CrewAssist@rccl.com"); 
                   
                   if (CC != "") msg.CC.Add(CC);
                   
                   msg.Subject = sSubject;
                   msg.IsBodyHtml = true;

                   msg.Body += sMessage.ToString();
                   msg.Priority = MailPriority.High;

                   System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(sServer, iPort);
                   client.UseDefaultCredentials = false;
                   client.Credentials = new System.Net.NetworkCredential(sUser, sPwd);
                   client.Port = iPort;
                   client.Host = sServer;
                   client.EnableSsl = true;
                   //object userstate = msg;
                   client.Send(msg);



                   //MailAddress from = new MailAddress(sFrom, "Muhallidin Wali Message");
                  
                   //MailMessage message = new System.Net.Mail.MailMessage();
                   //message.From = new MailAddress(sFrom);
                   //message.To.Add(sTo);
                   //message.ReplyTo = new MailAddress("CrewAssist@rccl.com"); 

                   //message.Subject = sSubject;
                   //message.Body = sMessage.ToString();
                   //SmtpClient clienttest = new System.Net.Mail.SmtpClient(sServer, iPort);
                   //clienttest.Credentials = new System.Net.NetworkCredential(sUser, sPwd);

                   //MailAddress emailTo = new MailAddress(sTo);

                   //MailMessage emailMsg = new MailMessage(emailFrom, emailTo);
                   //using (MailMessage emailMsg = new MailMessage(emailFrom, emailTo))
                   //{
                   //    if (CC.Length > 0)
                   //    {
                   //        emailMsg.CC.Add(CC);
                   //    }
                   //    emailMsg.To.Add(new System.Net.Mail.MailAddress(sTo));
                   //    emailMsg.Subject = sSubject;
                   //    emailMsg.IsBodyHtml = true;
                   //    emailMsg.Body = sMessage;
                   //    SmtpClient emailClient = new SmtpClient(sServer, iPort);
                   //    emailClient.Credentials = new System.Net.NetworkCredential(sUser, sPwd);
                   //    emailClient.EnableSsl = true;
                   //    emailClient.Send(emailMsg);
                   //}

               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }




       public string DecryptString(string NetworkUser, string Passphrase)
       {
           byte[] Results;
           System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

           // Step 1. We hash the passphrase using MD5
           // We use the MD5 hash generator as the result is a 128 bit byte array
           // which is a valid length for the TripleDES encoder we use below

           MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
           byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

           // Step 2. Create a new TripleDESCryptoServiceProvider object
           TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

           // Step 3. Setup the decoder
           TDESAlgorithm.Key = TDESKey;
           TDESAlgorithm.Mode = CipherMode.ECB;
           TDESAlgorithm.Padding = PaddingMode.PKCS7;

           // Step 4. Convert the input string to a byte[]
           byte[] DataToDecrypt = Convert.FromBase64String(NetworkUser);

           // Step 5. Attempt to decrypt the string
           try
           {
               ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
               Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
           }
           finally
           {
               // Clear the TripleDes and Hashprovider services of any sensitive information
               TDESAlgorithm.Clear();
               HashProvider.Clear();
           }

           // Step 6. Return the decrypted string in UTF8 format
           return UTF8.GetString(Results);
       }


   }

   [Serializable]
   public class SeafarerList
   {
       public long? SeafarerID { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Name { get; set; }

       public List<CrewAssisRemark> Remark { get; set; }

   }


   [Serializable]
   public class SeafarerDetailHeader
   {
        public long TravelRequetID { get; set; }
        public long SeafarerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? GenderID { get; set; }
        public int? NationalityID { get; set; }
        public string NationalityCode { get; set; }
        public string Nationality { get; set; }
        public string Status { get; set; }
        public int? VesselID { get; set; }
        public string VesselCode { get; set; }
        public string Vessel { get; set; } 
        public DateTime? RequestDate  { get; set; }
        public int? PortID { get; set; }
        public string PortCode { get; set; }
        public string PortName { get; set; }
        public int? RankID { get; set; }
        public string RankCode { get; set; }
        public string RankName { get; set; }
        public int? CostCenterID { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }

        public string HotelComments { get; set; }

        public int?  BrandID { get; set; }
        public string BrandCode { get; set; }
        public string Brand { get; set; }
        public string ReasonCode { get; set; }



        public long? CrewAssistRequestID { get; set; }
        public string CrewAssistRequestNo { get; set; }

        public long? ReqVehicleID { get; set; }
        public long? ReqMeetAndGreetID { get; set; }
        public long? ReqPortAgentID { get; set; }

        public List<CrewAssistAirTransaction> CrewAssistAirTransaction
        {
            get;
            set;
        }

        public List<SeafarerDetailList> SeafarerDetailList
        {
            get;
            set;
        }

        public List<CrewAssistShipEmail> CrewAssistShipEmail
        {
            get;
            set;
        }


        public List<CopyEmail> CopyEmail
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

        public List<CrewAssistHotelBooking> CrewAssistHotelBooking
        {
            get;
            set;
        }
       
        public List<CrewAssisRemark> Remark 
        { 
            get; set; 
        }

   }



   [Serializable]
   public class SeafarerDetailList
   {
       public long TravelRequetID { get; set; }
       public long SeafarerID { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Gender { get; set; }
       public int? GenderID { get; set; }
       public int? NationalityID { get; set; }
       public string NationalityCode { get; set; }
       public string Nationality { get; set; }
       public string Status { get; set; }
       public int? VesselID { get; set; }
       public string VesselCode { get; set; }
       public string Vessel { get; set; }
       public DateTime? RequestDate { get; set; }
       public int? PortID { get; set; }
       public string PortCode { get; set; }
       public string PortName { get; set; }
       public int? RankID { get; set; }
       public string RankCode { get; set; }
       public string RankName { get; set; }
       public int? CostCenterID { get; set; }
       public string CostCenterCode { get; set; }
       public string CostCenterName { get; set; }
       public int? BrandID { get; set; }
       public string BrandCode { get; set; }
       public string Brand { get; set; }
       public long? CrewAssistRequestID { get; set; }
       public string CrewAssistRequestNo { get; set; }
       public string ReasonCode { get; set; }

       public long IDBigint { get; set; }
       public string RecordLocator { get; set; }
       public long? ReqVehicleID { get; set; }
       public long? ReqMeetAndGreetID { get; set; }
       public long? ReqPortAgentID { get; set; }
       public long? ReqSafeguardID { get; set; }
       public bool? IsDeleted { get; set; }
       public DateTime? OnOffDate { get; set; }
       public bool? IsSelected { get; set; }

       public string LOEStatus { get; set; }




        public DateTime? LOEDate { get; set; }
        public string LOEImmigrationOfficer { get; set; }
        public string LOEImmigrationPlace { get; set; }
        public string LOEReason { get; set; }

        public bool? IsConfirm { get; set; }



        public string ContractStatus { get; set; }
        public string AirStatus { get; set; }
        public string BookingStatus { get; set; }
        public string AppointmentStatus { get; set; }

   }

   [Serializable]
   public class CrewAssistAirTransaction
   {
        public long HotelRequestAirDetailID { get; set; }
        public long HotelRequestID { get; set; }
        public long TravelReqId {get; set;}
        public string RecordLocator {get; set;}
        public int ItinerarySeqNo {get; set;}
        public long IdBigint  {get; set;}
        public int SeqNo {get; set;}

        public string AirlineCode { get; set; }

        public DateTime ArrivalDateTime  {get; set;}
        public DateTime DepartureDateTime {get; set;}
        public string DepartureAirportLocationCode  {get; set;}
        public string ArrivalAirportLocationCode {get; set;}

        

        public string SeatNo {get; set;}
        public string TicketNo {get; set;}
        public string AirStatus {get; set;}
        public bool IsBilledToCrew {get; set;}
        public string ActionCode {get; set;}
        public int OrderNo { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }

        public string FligthNo { get; set; }

        public int VendorID { get; set; }
        public int BranchID { get; set; }
        public int RoomTypeID { get; set; }
        public DateTime TimeSpanStartDate { get; set; }
        public DateTime TimeSpanStartTime { get; set; }
        public DateTime TimeSpanEndDate { get; set; }
        public DateTime TimeSpanEndTime { get; set; }

        public int Duration { get; set; }
        public string HotelCity { get; set; }

        public long? ReqVehicleID { get; set; }
        public long? ReqMeetAndGreetID { get; set; }
        public long? ReqPortAgentID { get; set; }

        public bool? IsSelected { get; set; }
   } 




   public class CrewAssistGenericClass
   {
       public List<CrewAssitPortList> CrewAssitPortList { get; set; }
       public List<CrewAssitExpendTypeList> CrewAssitExpendTypeList { get; set; }
       public List<CrewAssistHotelList> CrewAssistHotelList { get; set; }
       public List<CrewAssistCurrency> CrewAssistCurrency { get; set; }
       public List<CrewAssistRout> CrewAssistRout { get; set; }
       public List<VehicleVendor> VehicleVendor { get; set; }
       public List<CrewAssistNationality> CrewAssistNationality { get; set; }
       public List<CrewAssistMeetAndGreetVendor> CrewAssistMeetAndGreetVendor { get; set; }
       public List<CrewAssistVendorPortAgent> CrewAssistVendorPortAgent { get; set; }

       public List<CrewAssistSafeguardVendor> CrewAssistSafeguardVendor { get; set; }
       

       public List<CrewAssitPortList> CrewAssistAirport { get; set; }
       public List<CRRemarkType> RemarkType { get; set; }
       public List<CRRemarkType> RemarkStatus { get; set; }
       public List<CRRemarkType> RemarkRequestor { get; set; }

   }

   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    Load Rout
   /// </summary>
   /// 
   [Serializable]
   public class CrewAssistNationality
   {

       public int NatioalityID { get; set; }
       public string NationalityCode { get; set; }
       public string Nationality { get; set; }
   }
    
    
   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    RemarkType
   /// </summary>
   /// 
   [Serializable]
   public class CRRemarkType
   {

       public int RemarkTypeID { get; set; }
       public string RemarkType { get; set; }
       public List<RemarkTypeDetail> RemarkTypeDetail { get; set; }

   }


   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    RemarkType
   /// </summary>
   /// 
   [Serializable]
   public class RemarkTypeDetail
   {

       public int RemarkTypeDetID { get; set; }
       public string RemarkTypeDet { get; set; }
   }

   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    Load Rout
   /// </summary>
   /// 
   [Serializable]
   public class CrewAssistRout
   {
       public int RoutId { get; set; }
       public string RoutName { get; set; }
   }

   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    Load user port
   /// </summary>
   /// 
   [Serializable]
   public class CrewAssitPortList
   {
       public int PortId { get; set; }
       public string PortName { get; set; }
       public string PortCode { get; set; }
   }


   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    Load user port
   /// </summary>
   /// 
   [Serializable]
   public class CrewAssitPortListClass
   {

     public List<CrewAssitPortList> SeaportList { get; set; }
     public List<CrewAssitPortList> AirportList { get; set; }

   }


   /// <summary>
   /// Author:         Muhallidin G Wali
   /// Date Created:   08/02/2013
   /// Description:    Load user port
   /// </summary>
   /// 
   public class CrewAssitExpendTypeList
   {
       public int ExpendTypeID { get; set; }
       public string ExpendType { get; set; }
       public bool? IsSelected { get; set; }
   }

   /// <summary>
   /// Date Created:   08/02/2013
   /// Created By:     Muhallidin G wali
   /// (description)   Set Hotel class 
   /// </summary>
   /// 
   [Serializable]
   public class CrewAssistHotelList
   {
       public int HotelID { get; set; }
       public string HotelName { get; set; }
       public string Portcode { get; set; }
       public bool? IsPortAgent { get; set; }
   }

/// <summary>
   /// Date Created:   08/02/2013
   /// Created By:     Muhallidin G wali
   /// (description)   Set Hotel class 
   /// </summary>
   /// 
   public class CrewAssistCurrency
   {
       public int? CurrencyID { get; set; }
       public string CurrencyCode { get; set; }
       public string CurrencyName { get; set; }
   }
    /// <summary>
   /// Date Modiefied:  25/Nov/2013
   /// Modified By:     Josephine Gad
   /// (description)    Add RoomTypeID
    /// </summary>
   public class CrewAssistHotelInformation
   {

        public string VendorBranchName { get; set; }
        public int? CityID { get; set; }
        public string CityCode { get; set; }
        public int CountryID { get; set; }		   
        public string ContactNo { get; set; }
        public int? VendorId { get; set; }
        public int? BranchID { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string CityName { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }

        public string MealVoucher { get; set; }
        public string ContractedRate   { get; set; }
        public string ConfirmRate { get; set; }

        public string ContractDateStarted { get; set; }
        public string ContractDateEnd { get; set; }

        public string ContractRoomRateTaxPercentage { get; set; } 

        public bool IsLunch { get; set; }
        public bool IsDinner { get; set; }
        public bool IsBreakfast { get; set; }
        public bool IsWithShuttle { get; set; }
        
        public int RoomTypeID { get; set; }
        public int? CurrencyID { get; set; }
        public List<ATTEMail> ATTEMail { get; set; }
 }


   [Serializable]
   public class ATTEMail {
       public string BranchID   { get; set; }
       public string BranchCode { get; set; }
       public string BranchName { get; set; }
       public string Email      { get; set; }

   
   
   }


   [Serializable]
   public class crewassistrequest : RequestSource 

   { 
   
   
           public long  RequestID  { get; set; }
	        public long SeafarerIDInt  { get; set; }
	        public string LastNameVarchar  { get; set; }
	        public string FirstNameVarchar  { get; set; }
	        public string Gender  { get; set; }
	        public int? RegionIDInt  { get; set; }
	        public int? PortIDInt  { get; set; }
	        public int? AirportIDInt  { get; set; }
        	
	        public int? HotelIDInt  { get; set; }

	        public DateTime? CheckinDate  { get; set; }
	        public DateTime? CheckoutDate  { get; set; }
	        public int? NoNitesInt  { get; set; }
	        public int? RoomTypeID  { get; set; }
            public string RoomType { get; set; }

	        public bool MealBreakfastBit  { get; set; }
	        public bool MealLunchBit  { get; set; }
	        public bool MealDinnerBit  { get; set; }
	        public bool MealLunchDinnerBit  { get; set; }
	        public bool WithShuttleBit	 { get; set; }
        	
	        public int? RankIDInt  { get; set; }
	        public int? VesselInt  { get; set; }
	        public int? CostCenterInt  { get; set; }
	        public string CommentsVarchar  { get; set; }
	        public string SfStatus	 { get; set; }
	        public string TimeIn  { get; set; }
	        public string TimeOut  { get; set; }
	        public string RoomAmount  { get; set; }
	        public bool? RoomRateTaxInclusive  { get; set; }
	        public double? RoomRateTaxPercentage  { get; set; }
        	
	        public string UserID  { get; set; }
            public long? TravelReqID { get; set; }
	        public int? Currency  { get; set; }
        	
	        public string Description  { get; set; }
	        public string Function  { get; set; }
	        public string FileName  { get; set; }
	        public string Timezone  { get; set; }
	        public DateTime? GMTDATE  { get; set; }
            public DateTime? CreateDate { get; set; }

            public bool? isAir { get; set; }
            public int? SeqNo { get; set; }
            public long? IDBigInt { get; set; }
            public string RecordLocator { get; set; }

            public double? MealVoucherMoney    { get; set; }
            public double? ConfirmRateMoney    { get; set; }
            public double? ContractedRateMoney { get; set; }
            public string HotelEmail { get; set; }

            public string HotelComment { get; set; }
            public long TransVehicleIDBigint { get; set; }
          
            public string ReasonCode { get; set; }
            public bool? IsMedical { get; set; }
   }

   [Serializable]
   public class CrewAssistEmailDetail 
   {

	    public string SeafarerID  { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string GenderDiscription { get; set; }
		public string BrandCode { get; set; }
		public string RankName { get; set; }
		public string SFStatus  { get; set; }
		public string Nationality { get; set; }
		public string VesselName { get; set; }
		public int    VesselId { get; set; }
		public string CostCenterCode { get; set; }
		public string RoomDesc { get; set; }
		public string SharingWith  { get; set; }
		public string TimeSpanStartDate { get; set; }
		public string TimeSpanEndDate  { get; set; }
		public string TimeSpanStartTime  { get; set; }
		public string TimeSpanEndTime  { get; set; }
        public string NoOfNite { get; set; }
		public string Mealvoucheramount  { get; set; }
        public string Roomrate { get; set; }
		public string Confirmedbyhotelvendor  {  get; set; }
        public string ConfirmedbyRCCL { get; set; }
        public string VendorBranch { get; set; }
        public string HotelLocation { get; set; }
        public string Comment { get; set; }


        public string TravelRequestID { get; set; }
        public string RecordLocator { get; set; }

        public List<CrewAssistEmailAirDetail> CrewAssistEmailAirDetail { get; set; }

   }


   [Serializable]
   public class CrewAssistEmailAirDetail
   {
       public string AirDetail { get; set; }
   }

   [Serializable]
   public class CrewAssistShipEmail
   {
       public string VesselCode { get; set; }
       public string VesselName { get; set; }
       public string Email { get; set; }
   }

   [Serializable]
   public class CopyEmail 
   {
       public int  EmailType { get; set; }
       public string Email     { get; set; }
       public string EmailName { get; set; }
   }


   [Serializable]
   public class VehicleVendor
   {
        
       public int?   ContractID { get; set; }
       public int?   VehicleID { get; set; }
       public string Vehicle { get; set; }
       public int?   PortCodeID  { get; set; }
       public string PortCode  { get; set; }

       public bool?  IsAirport { get; set; }
       public string Email { get; set; }
       public string VenConfirm { get; set; }
       public string Address { get; set; }
       public string Telephone { get; set; }
       public bool?  IsPortAgent { get; set; }

       public List<CrewAssisVehicleCost> VehicleCost { get; set; }

   }


   [Serializable]
   public class CrewassistSeafarerDetail
   {

      public long?  SeaparerID { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string RankName { get; set; }
      public string CostCenter { get; set; }
      public string NationalityCode { get; set; }
      public string NationalityName { get; set; }
      public string Ship { get; set; }

      public string Gender { get; set; }

   }

    /// <summary>
   /// Date Modified: 25/Nov/2013
   /// Modified By:   Josephine Gad
   /// (description)  Add VehicleTransID for Transaction ID of TblVehicleTransaction
    /// </summary>
    [Serializable]
    public class CrewAssistTranspo : CrewassistSeafarerDetail
    {

        public Int64      VehicleTransID          { get; set; }
        public long?      ReqVehicleIDBigint		{ get; set; }
        public string     ReqVehicleVarchar		{ get; set; }
        public long?      IdBigint				{ get; set; }
        public long?      TravelReqIDInt			{ get; set; }
        public short?     SeqNoInt                { get; set; }
        public string     RecordLocatorVarchar	    { get; set; }
        public long?      VehicleVendorIDInt		    { get; set; }
        public string     VehicleVendor           { get; set; }
        public string     VehiclePlateNoVarchar	    { get; set; }
        public DateTime?  PickUpDate				{ get; set; }
        public DateTime?  PickUpTime				{ get; set; }
        public DateTime?  DropOffDate			    { get; set; }
        public DateTime?  DropOffTime			    { get; set; }
        public string     ConfirmationNoVarchar	    { get; set; }
        public string     VehicleStatusVarchar	    { get; set; }
        public int?       VehicleTypeIdInt		    { get; set; }
        public string     SFStatus				    { get; set; }
        public int?       RouteIDFromInt			    { get; set; }
        public int?       RouteIDToInt			    { get; set; }
        public string     FromVarchar			        { get; set; }
        public string     ToVarchar			        { get; set; }
        public string     UserID				        { get; set; }
        public string     Comment                     { get; set; }
        public string     TransSender                 { get; set; }

        public string     Email { get; set; }
        public bool?      IsAir { get; set; }

        public string RouteFrom { get; set; }
        public string RouteTo { get; set; }

        public int? HotelID { get; set; }

        public string ConfirmBy { get; set; }

        public string Address { get; set; }
        public string Telephone { get; set; }
        public bool? IsPortAgent { get; set; }
        public bool? IsMedical { get; set; }
        public double? ConfirmRate { get; set; }
        public string TranspoCost { get; set; }
        public short? ReqSourceID { get; set; }

        public int?   StatusID { get; set; }
        public string ColorCode { get; set; }
        public string ForeColor { get; set; }


        public List<CrewAssisVehicleCost> VehicleContract
        {
            get;
            set;
        }
        public List<CrewAssisRemark> VehicleRemark
        {
            get;
            set;
        }

   }

   [Serializable]
   public class MeetAndGreetGenericClass 
   {
       public List<CrewAssistMeetAndGreetVendor> CrewAssistMeetAndGreet { get; set; }
       public List<ComboGenericClass> ComboGenericClass { get; set; } 
   }

   [Serializable]
   public class CrewAssistMeetAndGreetVendor 
   {
       public int?    MeetAndGreetVendorID { get; set; }
       public string  MeetAndGreetVendor { get; set; }
       public int?    CityID { get; set; }
       public string  ContactNo { get; set; }
       public string  FaxNo { get; set; }
       public string  ContactPerson { get; set; }
       public string  Address { get; set; }
       public string  EmailCc { get; set; }
       public string  EmailTo { get; set; }
       public string  Website { get; set; }

       public string  FlightInfo { get; set; }
       public DateTime ServiceDate { get; set; }
       public int? AirportCodeID { get; set; }
       public string AirportCode { get; set; }
       public string  Rate { get; set; }


   }

   [Serializable]
   public class ComboGenericClass
   {
       public int? ID { get; set; }
       public string Name { get; set; }
       public string NameCode { get; set; }
   }


  

   [Serializable]
   public class CrewAssistMeetAndGreet : CrewassistSeafarerDetail
   {

       public long? ReqMeetAndGreetID  { get; set; }
       public long? IdBigint  { get; set; }
       public long? TravelReqID  { get; set; }
       public int? SeqNo  { get; set; }
       public string RecordLocator  { get; set; }
       public int? MeetAndGreetVendorID  { get; set; }
       public string MeetAndGreetVendor { get; set; }
       public string ConfirmationNo  { get; set; }
       public string MeetAndGreetStatus  { get; set; }
       public int? AirportID  { get; set; }
       public string AirportCode { get; set; }
       public string FligthNo  { get; set; }

       public DateTime? ArrTime  { get; set; }
       public DateTime? DeptTime { get; set; }


       public DateTime? ServiceDate  { get; set; }
       public double? Rate  { get; set; }
       public string SFStatus { get; set; }
       public string Comment { get; set; }
       public string UserID { get; set; }
       public string Email { get; set; }

       public bool? IsAir { get; set; }
       public string ConfirmBy { get; set; }
   }
   
   /// <summary>
   /// Date Created:    22/10/2013
   /// Created By:      Muhallidin G Wali
   /// (description)    Get Service Provider list
    /// </summary>
   [Serializable]  
   public class CrewAssistVendorPortAgent
   { 
        public int? PortAgentVendorID { get; set; }
        public string PortAgentVendorName { get; set; }
        public int? CityID { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailCc { get; set; }
        public string EmailTo { get; set; }
        public string Website { get; set; }
        
       public int? PortCodeID { get; set; }
        public string PortCode { get; set; }
        
        public int? AirportID { get; set; }
        public string AirportCode { get; set; }

        public string Comment { get; set; }
        public bool? IsAir { get; set; }

        public List<CrewAssisVehicleCost> PortAgentVehicle { get; set; }
        public List<CrewAssisPortAgentHotelCost> PortAgentHotel { get; set; }
   }

   //public class CrewAssisVehicleCost
   //{
   //    public long? TravelRequestID { get; set; }
   //    public long? IDBigInt { get; set; }
   //    public string PortCode { get; set; }
   //    public string CurrencyCode { get; set; }
   //    public string Route { get; set; }

   //    public string VehicleTypeName { get; set; }
   //    public int? VehicleTypeID { get; set; }
   //    public string UnitOfMeasure { get; set; }
   //    public string Cost { get; set; }
   //    public string Capacity { get; set; }
   //    public string VacantSeat { get; set; }

   //}


    /// <summary>
    /// Date Created:    22/10/2013
    /// Created By:      Muhallidin G Wali
   /// (description)    Get Service Provider list
    /// </summary>
   [Serializable]
   public class CrewAssistPortAgentRequest : CrewAssistVendorPortAgent
   { 

       public long? ReqPortAgentID  { get; set; }
       public long? IdBigint { get; set; }
       public long? TravelReqID { get; set; }
       public int? SeqNo { get; set; }
       public string RecordLocator { get; set; }
       public string FligthNo  { get; set; }
       public DateTime? ServiceDatetime { get; set; }
       public string SFStatus  { get; set; }

       public bool? IsMAG { get; set; }
       public bool? IsTrans { get; set; }
       public bool? IsHotel { get; set; }
       public bool? IsLuggage { get; set; }
       public bool? IsSafeguard { get; set; }
       public bool? IsVisa { get; set; }
       public bool? IsOther { get; set; }
       public string UserID  { get; set; }

       public string ConfirmBy { get; set; }



     public bool? IsPhoneCard { get; set; }
     public double? PhoneCard { get; set; }
     public bool? IsLaundry { get; set; }
     public double? Laundry { get; set; }
     public bool? IsGiftCard { get; set; }
     public double? GiftCard { get; set; }









       public List<VehicleTransactionPortAgent> VehicleTransactionPortAgent
       {
           get;
           set;
       }


       public List<CrewAssisVehicleCost> PortAgentVehicle
       {
           get;
           set;
       }


       public List<HotelTransactionPortAgent> HotelTransactionPortAgent
       {
           get;
           set;
       }

      
   }


   /// <summary>
   /// Date Created:    22/10/2013
   /// Created By:      Muhallidin G Wali
   /// (description)    Get Safeguard vendor detail
   /// </summary>
   [Serializable]
   public class CrewAssistSafeguardVendor : CrewAssitPortList
   { 

        public int? SafeguardVendorID { get; set; }
        public string SafeguardName { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string EmailCc { get; set; }
        public string EmailTo { get; set; }
        public string Website { get; set; }


        public List<CrewAssistSafeguardServiceType> CrewAssistSafeguardServiceType { get; set; }
   }



  /// <summary>
   /// Date Created:    22/10/2013
   /// Created By:      Muhallidin G Wali
   /// (description)    Get Service Type Safeguard
   /// </summary>
   [Serializable]
   public class CrewAssistSafeguardServiceType 
   {
        public string ServiceDisplay { get; set; }
        public int TypeID { get; set; }
        public string RateAmount { get; set; }

        public int contractID { get; set; }
        public int ContractServiceTypeID { get; set; }

   }


   // <summary>
   /// Date Created:    22/10/2013
   /// Created By:      Muhallidin G Wali
   /// (description)    Get Service Type Safeguard
   /// </summary>
   [Serializable]
   public class CrewAssistSafeguardRequest : CrewAssistSafeguardVendor
   {
        public long ReqSafeguardID { get; set; }
		public string ReqSafeguard { get; set; }
		public long IdBigint { get; set; }
		public long TravelReqID { get; set; }
		public int SeqNo { get; set; }
		public string RecordLocator { get; set; }
        //public long SafeguardVendorID { get; set; }
		public int TypeID { get; set; }
        public string Type { get; set; }
		public int ContractId { get; set; }
		public int ContractServiceTypeID { get; set; }
        public string ContractServiceType { get; set; }
		public string SFStatus { get; set; }
		public string Comments { get; set; }
		public bool IsAirBit { get; set; }
		public DateTime TransactionDate { get; set; }
		public DateTime TransactionTime { get; set; }
        public string SGRate { get; set; }
        public string UserID { get; set; }
        public string ConfirmBy { get; set; }
   }


   /// <summary>
   /// Date Created:    22/10/2013
   /// Created By:      Muhallidin G Wali
   /// (description)    Get Hotel transaction Other
   /// </summary>
   [Serializable]
   public class CrewAssistHotelBooking : crewassistrequest
   { 
   
       public long?  TransHotelID  { get; set; }
       public String Branch { get; set; }
       public string ReserveUnderName { get; set; }
       public string ConfirmationNo { get; set; }
       public string HotelStatus { get; set; }
       public string Remarks { get; set; }
       public Decimal? VoucherAmount { get; set; }
       public long? ContractID { get; set; }
       public string ApprovedBy { get; set; }
       public DateTime? ApprovedDate { get; set; }
       public string ContractFrom { get; set; }
       public string RemarksForAudit { get; set; }
       public string HotelCity { get; set; }
       public double? RoomCountFloat { get; set; }
       public bool? IsPortAgent { get; set; }
       public bool? IsApproved { get; set; }
       public int? StatusID { get; set; }
       public string ColorCode { get; set; }
       public string ForeColor { get; set; }
       public string ConfirmBy { get; set; }
       public bool? IsAutoAirportToHotel { get; set; }
       public bool? IsAutoHotelToShip { get; set; }
       public string EmailAdd { get; set; }

       public string ContractDateStarted { get; set; }
       public string ContractDateEnd { get; set; }

       public string Address { get; set; }
       public string ContactNo { get; set; }


       public bool? CanCancel { get; set; }
       public string CancelMessage { get; set; }

       public int? CancellationTermsInt { get; set; }
       public string HotelTimeZoneID { get; set; }
       public DateTime? CutOffTime { get; set; }

       public bool? IsConfirmed { get; set;  } 



       public List<CrewAssisRemark> HotelRemark { get; set; }

   }

   /// <summary>
   /// Date Created:  26/08/2014
   /// Created By:    Muhallidin G Wali
   /// (description) 
   /// </summary>
   [Serializable]
   public class CrewAssistTransaction
   {
       public long? TransHotelOtherID { get; set; }
       public long? TransTransapotationID { get; set; }
       public long? TransMeetAndGreetID { get; set; }
       public long? TransPortAgentID { get; set; }
       public long? TransSafeguardID { get; set; }

       public long? ReqHotelOtherID { get; set; }
       public long? ReqTransapotati { get; set; }
       public long? ReqMeetAndGreet { get; set; }
       public long? ReqPortAgentID  { get; set; }
       public long? ReqSafeguardID  { get; set; }


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

       public List<CrewAssistHotelBooking> CrewAssistHotelBooking
       {
           get;
           set;
       }

       public List<CrewAssistTranspo> CrewAssistTranspoApprove
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


   }

   [Serializable]
   public class CrewAssisRemark {

       public long?     TravelRequestID { get; set; }
       public long?     SeafarerID { get; set; }
       public long?     RemarkID { get; set; }
       public string    Remark { get; set; }
       public string    RemarkBy { get; set; }
       public DateTime? RemarkDate { get; set; }
       public string    Visible { get; set; }

       public short?    ReqResourceID { get; set; }
       public string    Resource { get; set; }

       public int?    RemarkTypeID { get; set; }
       public string    RemarkType { get; set; }

       public short? RemarkStatusID { get; set; }
       public string RemarkStatus { get; set; }

       public string    SummaryOfCall { get; set; }

       public int RemarkRequestorID { get; set; }
       public string  RemarkRequestor { get; set; }

       public DateTime? TransactionDate { get; set; }
       public DateTime? TransactionTime { get; set; } 

       public long?     IDBigInt { get; set; }
       public string    RecordLocator { get; set; }
       public string UserID { get; set; }
       public string Role { get; set; }
       public string PortCode { get; set; }
       public bool? IR { get; set; }


   }


   /// <summary>
   /// Date Created:  26/08/2014
   /// Created By:    Muhallidin G Wali
   /// (description) 
   /// </summary>
   [Serializable]
    public class CrewAssisVehicleCost
    {
       public long?  TravelRequestID   { get; set; }
       public long?  IDBigInt        { get; set; }
       public string PortCode { get; set; }
       public string CurrencyCode { get; set; }
       public string Route { get; set; }

       public string VehicleTypeName { get; set; }
       public int? VehicleTypeID { get; set; }
       public string UnitOfMeasure { get; set; }
       public string TranspoCost { get; set; }
       public string Capacity { get; set; }
       public string VacantSeat { get; set; }

    }

    public class CrewAssisPortAgentHotelCost
    {
                
        public int? PortAgentVendorID { get; set; }
        public string PortAgentVendorName { get; set; }
        public string PortCode { get; set; }
        public string CurrencyCode { get; set; }
        public bool? IsRateByPercentBit { get; set; }
        public decimal? RoomCostPercent { get; set; }
        public decimal? RoomDoubleRate { get; set; }
        public decimal? RoomSingleRate { get; set; }
        public decimal? MealStandardDecimal { get; set; }
        public decimal? MealIncreasedDecimal { get; set; }
        public decimal? SurchargeSingle { get; set; }
        public decimal? SurchargeDouble { get; set; }


        public decimal? ContractRate { get; set; }
        public decimal? ConfirmRate { get; set; }
        public decimal? MealVoucher { get; set; }



        public int? ContractID { get; set; }

    }

    /// <summary>
    /// Date Created:  26/08/2014
    /// Created By:    Muhallidin G Wali
    /// (description) 
    /// </summary>
    [Serializable]
    public class VehicleTransactionPortAgent
    {
        public long? VehicleTransID { get; set; }
        public long? ReqVehicleIDBigint { get; set; } 
        public long? IdBigint  { get; set; } 
        public long?  TravelReqIDInt  { get; set; } 
        public string  RecordLocator  { get; set; }
        public long? PortAgentVendorID { get; set; } 
        public string VehiclePlateNo  { get; set; } 
        public DateTime?  PickUpDate  { get; set; } 
        public DateTime? PickUpTime  { get; set; }
        public DateTime? DropOffDate  { get; set; } 
        public DateTime? DropOffTime  { get; set; } 
        public string ConfirmationNo  { get; set; } 
        public string VehicleStatus  { get; set; } 
        public int? VehicleTypeID  { get; set; } 
        public int? RouteIDFromInt  { get; set; } 
        public int? RouteIDToInt  { get; set; } 
        public string FromVarchar  { get; set; } 
        public string ToVarchar  { get; set; } 
        public string RemarksForAudit  { get; set; } 
        public int? HotelID  { get; set; }
        public int? SeqNo  { get; set; }
        public int? DriverID  { get; set; } 
        public DateTime? VehicleDispatchTime  { get; set; } 

        public string RouteFrom  { get; set; } 
        public string RouteTo  { get; set; }

        public long? VehicleUnset  { get; set; }
        public string VehicleUnsetBy  { get; set; } 
        public DateTime? VehicleUnsetDate  { get; set; } 
        public string ConfirmBy  { get; set; } 
        public string Comments { get; set; }
        public string VehicleVendorName { get; set; }
        public double? TranspoCost { get; set; } 

    }
    /// <summary>
    /// Date Created:  26/08/2014
    /// Created By:    Muhallidin G Wali
    /// (description) 
    /// </summary>
    [Serializable]
    public class HotelTransactionPortAgent
    {

        public long? HotelTransID { get; set; }
	    public long? TravelReqID  { get; set; }  
	    public long? IdBigint  { get; set; }  
	    public string RecordLocator  { get; set; }  
	    public int? SeqNo  { get; set; }  
	    public long? PortAgentVendorID  { get; set; }  
	    public int? RoomTypeID  { get; set; }  
	    public string ReserveUnderName  { get; set; }  
	    public DateTime? TimeSpanStartDate  { get; set; }  
	    public DateTime? TimeSpanStartTime  { get; set; }  
	    public DateTime? TimeSpanEndDate  { get; set; }  
	    public DateTime? TimeSpanEndTime  { get; set; }  
	    public int? TimeSpanDurationInt  { get; set; }  

	    public string ConfirmationNo  { get; set; }  
	    public string HotelStatus  { get; set; }  
	    public bool? IsBilledToCrew  { get; set; }  
	    public bool? Breakfast  { get; set; }  
	    public bool? Lunch  { get; set; }  
	    public bool? Dinner  { get; set; }  

	    public bool? LunchOrDinner  { get; set; }  
	    public int? BreakfastID  { get; set; }
        public int? LunchID { get; set; }  
	    public int? DinnerID  { get; set; }  
	    public int? LunchOrDinnerID  { get; set; }  

	    public double? VoucherAmount  { get; set; }  

	    public long? ContractID  { get; set; }  
	    public string ApprovedBy  { get; set; }  
	    public DateTime? ApprovedDate  { get; set; } 
 
	    public string ContractFrom  { get; set; }  
	    public string RemarksForAudit  { get; set; }  
	    public string HotelCity  { get; set; }  
	    public decimal? RoomCount  { get; set; }  
	    public string HotelName  { get; set; }

        public string ContractRate { get; set; }
        public string ConfirmRate { get; set; }  
        public bool? IsAir { get; set; } 
        public string EmailTo { get; set; } 
        public string Comment { get; set; } 
        public int? Currency { get; set; }
        public string ConfirmBy { get; set; }
        public short? ReqSource { get; set; } 

        public bool? IsAutoAirportToHotel { get; set; }
        public bool? IsAutoHotelToShip { get; set; }
        public bool? IsMedical { get; set; } 

    }



    public class SaveCrewAssistrequest
    {

        public List<InsertCrewAssistHotelRequest> InsertCrewAssistHotelRequest
        {
            get;
            set;
        }

        public List<InsertCrewAssistTransRequest> InsertCrewAssistTransRequest
        {
            get;
            set;
        }
    
    }





    public class InsertCrewAssistHotelRequest
    { 
        public string pRequestNo { get; set; }
        public int? pSeafarerIDInt { get; set; }
        public string pLastNameVarchar { get; set; }
        public string pFirstNameVarchar { get; set; }
        public int? pGender { get; set; }
        public int? pRegionIDInt { get; set; }
        public int? pPortIDInt { get; set; }
        public int? pAirportIDInt { get; set; }
        public int? pHotelIDInt { get; set; }
        public DateTime? pCheckinDate { get; set; }
        public DateTime? pCheckoutDate { get; set; }
        public int? pNoNitesInt { get; set; }
        public string pRoomTypeId { get; set; }
        public bool? pMealBreakfastBit { get; set; }
        public bool? pMealLunchBit { get; set; }
        public bool? pMealDinnerBit  { get; set; }
        public bool? pMealLunchDinnerBit { get; set; }
        public bool? pWithShuttleBit { get; set; }
        public int? pRankIDInt { get; set; }
        public int? pVesselInt { get; set; }
        public int? pCostCenterInt { get; set; }
        public int? pCommentsVarchar { get; set; }
        public string pSfStatus { get; set; }
        public string pTimeIn { get; set; }
        public string pTimeOut { get; set; }
        public string pRoomAmount { get; set; }
        public bool? pRoomRateTaxInclusive { get; set; }
        public double pRoomRateTaxPercentage { get; set; }
        public string pUserID { get; set; }
        public string pTrID { get; set; }
        public string pCurrency { get; set; }
        public string pDescription { get; set; }
        public string pFunction { get; set; }
        public string pFileName { get; set; }
        public string pTimezone { get; set; }
        public DateTime? pGMTDATE { get; set; }
        public string pCreateDate { get; set; }
        public bool? pIsAirBit { get; set; }
        public int? pSequentNoInt { get; set; }
        public long pIDBigInt { get; set; }
        public int? pBrandIDInt { get; set; }
        public double? pMealVoucherMoney { get; set; }
        public double? pConfirmRateMoney { get; set; }
        public double? pContractedRateMoney { get; set; }
        public string pEmailTO { get; set; }
        public string pHotelCity { get; set; }
        public short? pReqSource { get; set; }
    }

    public class InsertCrewAssistTransRequest
    { 
    
        public long? pReqVehicleID { get; set; }
        public long? pTransVehicleID { get; set; }
        public long? pIdBigint { get; set; }
        public long? pTravelReqID { get; set; }
        public short? pSeqNo { get; set; }
        public string pRecordLocator { get; set; }
        public int? pVehicleVendorID { get; set; }
        public string pVehiclePlateNo { get; set; }
        public DateTime? pPickUpDate { get; set; }
        public DateTime? pPickUpTime { get; set; }
        public DateTime? pDropOffDate { get; set; }
        public DateTime? pDropOffTime { get; set; }
        public string pConfirmationNo { get; set; }
        public string pVehicleStatus { get; set; }
        public int? pVehicleTypeId { get; set; }
        public string pSFStatus { get; set; }
        public int? pRouteIDFromInt { get; set; }
        public int? pRouteIDToInt { get; set; }
        public string pFrom { get; set; }
        public string pTo { get; set; }
        public string pUserID { get; set; }
        public string pComment { get; set; }
        public bool? pIsAir { get; set; }
        public string pRouteFrom { get; set; }
        public string pRouteTo { get; set; }
        public int? pHotelID { get; set; }
        public string pConfirmBy { get; set; }
        public string pIsPortAgent { get; set; }
        public string pEmailTo { get; set; }
        public string pConfirmRate { get; set; }
        public short? pReqSourceID { get; set; }
    }

    /// <summary>
    /// Date Created:  01/09/2014
    /// Created By:    Muhallidin G Wali
    /// (description) 
    /// </summary>
    [Serializable]
    public class HotelRequestCompanion
    {

        public long? DETAILID { get; set; }
        public long? REQUESTID { get; set; }
        public long? TRANSPOID { get; set; }

        public long? TRAVELREQID { get; set; }
        public long? IDBIGINT { get; set; }
        public string RECORDLOCATOR { get; set; }
        public int? SEQNO { get; set; }

        public int? GENDERID { get; set; }
        public string GENDER { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string RELATIONSHIP { get; set; }
        public string UserID { get; set; }
        public bool? IsMedical { get; set; }
        public bool? IsPortAgent { get; set; }

      

    }

    public class Medical { 
    
        public long? SeafarerID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Ship { get; set; }
        public string Brand { get; set; }

        public string  Position { get; set; }
        public string Status { get; set; }
        public string ReasonCode { get; set; }
        public string OnOffDate { get; set; }

        public string Port { get; set; }
        public string LOEStatus { get; set; }


        public long? TravelRequetID { get; set; }
        public long? IDBigint { get; set; }
        public int? SeqNo { get; set; }
        public int? PortID { get; set; }
        public long? RequestID { get; set; }
        public long? TransHotelID { get; set; }


        public string LOEDate { get; set; }
        public string LOEImmigrationOfficer { get; set; }
        public string LOEImmigrationPlace { get; set; }
    
    }
    
        
}

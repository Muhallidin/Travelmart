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
    public class CommonFunctions
    {
        /// <summary>
        /// Date Created:   09/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Format dropdownlist data to uppercase        
        /// </summary>   
        public static void ChangeToUpperCase(DropDownList ddl)
        {
            foreach (ListItem item in ddl.Items)
            {
                item.Text = item.Text.ToUpper();
            }
        }
        /// <summary>
        /// Date Created:   29/09/2011
        /// Created By:     Josephine Gad
        /// (description)   Clear all variables       
        /// </summary>   
        public static void ClearVariables()
        {
//            TravelMartVariable.strCreateMessge = string.Empty;
//            TravelMartVariable.strPrevPage = String.Empty;
//            TravelMartVariable.strSFID = String.Empty;
//            TravelMartVariable.strSFStatus = string.Empty;
//            TravelMartVariable.strSFFlightDateRange = string.Empty;
//            TravelMartVariable.strSFSeqNo = string.Empty;
//            TravelMartVariable.strPendingFilter = string.Empty;
//            TravelMartVariable.strAirStatusFilter = string.Empty;
//            TravelMartVariable.strTravelLocatorID = string.Empty;  
//            TravelMartVariable.strRecordLocator = string.Empty;

//            TravelMartVariable.UserRoleString = string.Empty;
//            TravelMartVariable.UserRoleKeyString = string.Empty;
//            GlobalCode.Field2String(Session["UserVendor"]) = "0";
//            GlobalCode.Field2String(Session["UserBranchID"]) = "0";
//            GlobalCode.Field2String(Session["UserCountry"]) = "0";
//            GlobalCode.Field2String(Session["UserCity"]) = "0";
        
//            TravelMartVariable.SelectedRoleKeyString = string.Empty;
//            TravelMartVariable.SelectedRoleNameString = string.Empty;

//            GlobalCode.Field2String(Session["TravelRequestID"]) = string.Empty;
            
//            TravelMartVariable.RegionString = string.Empty;
//            TravelMartVariable.CountryString = string.Empty;
//            TravelMartVariable.CityString = string.Empty;

//            TravelMartVariable.DateFromString = "";
//            TravelMartVariable.DateToString = "";

//            TravelMartVariable.PortString = "";
//            TravelMartVariable.HotelString = "";
//            TravelMartVariable.VehicleString = "";

//            TravelMartVariable.ViewRegion = "1";
//            TravelMartVariable.ViewCountry = "1";
//            TravelMartVariable.ViewCity = "1";
//            TravelMartVariable.ViewHotel = "1";
//            TravelMartVariable.ViewPort = "1";
//            TravelMartVariable.ViewLegend = "1";
//            TravelMartVariable.ViewFilter = "0";
//            TravelMartVariable.ViewDashboard = "0";
////            TravelMartVariable.ViewLinkMenu = "0";
//            TravelMartVariable.ViewLeftMenu = "0";
//            TravelMartVariable.ViewDashboard2 = "0";
//            TravelMartVariable.ViewDateRange = "1";
//            TravelMartVariable.ViewFilter2 = "1";
//            TravelMartVariable.ManifestHrs = "0";            
        }
        /// <summary>
        /// Date Created:   10/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Send Email
        /// --------------------------------------
        /// Date Modified:   06/Dec/2012
        /// Created By:      Josephine Gad
        /// (description)    Add security to email address by using encrypt/decrypt
        ///                  Get the email credentials from web config
        ///                  Use also the SHRSS Spport email from web config
        /// --------------------------------------
        /// </summary>
        /// <param name="sFrom"></param>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        public static void SendEmail(string sFrom, string sTo, string sSubject, string sMessage)
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
                    MailAddress emailFrom = new MailAddress(sFrom);
                    MailAddress emailTo = new MailAddress(sTo);
                    //MailMessage emailMsg = new MailMessage(emailFrom, emailTo);
                    using (MailMessage emailMsg = new MailMessage(emailFrom, emailTo))
                    {
                        emailMsg.Subject = sSubject;
                        emailMsg.IsBodyHtml = true;
                        emailMsg.Body = sMessage;
                        SmtpClient emailClient = new SmtpClient(sServer, iPort);
                        emailClient.Credentials = new System.Net.NetworkCredential(sUser, sPwd);
                        emailClient.EnableSsl = true;
                        emailClient.Send(emailMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:  14/11/2011
        /// Created By:    Josephine Gad
        /// (description)  Send Email with attachment
        /// --------------------------------------------------------------------------------------------------------------
        /// Date Modified: 24/02/2012
        /// Modified By:   Charlene Remotigue
        /// (description)  Send multiple attachments
        /// --------------------------------------------------------------------------------------------------------------
        /// Date Modified: 27/03/2012
        /// Modified By:   Gabriel Oquialda
        /// (description)  Send multiple recipient and cc
        /// --------------------------------------------------------------------------------------------------------------
        /// Date Modified: 29/03/2012
        /// Modified By:   Gabriel Oquialda
        /// (description)  Added configuration settings/appsettings key for smtp network username and password (encrypted)
        ///                Added md5 decryption for encrypted smtp network username and password        
        /// --------------------------------------
        /// Date Modified:   06/Dec/2012
        /// Created By:      Josephine Gad
        /// (description)    Get the email credentials from web config
        /// --------------------------------------
        /// </summary>
        /// <param name="sFrom"></param>
        /// <param name="sTo"></param>
        /// <param name="sCc"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        /// <param name="attachment"></param>
        public static void SendEmailWithAttachment(string sFrom, string sTo, string sCc, string sSubject, string sMessage, string attachment)
        {            
            char[] charsToTrim = { ';' };
            string sTos = sTo.TrimEnd(charsToTrim);
            string sCcs = sCc.TrimEnd(charsToTrim);

            string[] attachments = attachment.Split(';');
            string[] recipients = sTos.Split(';');
            string[] carboncopy;

            try
            {
                // Encryption (for debugging purpose only)
                //string EncryptedNetworkUser = EncryptString(TravelmartNetworkUser, TravelmartPassphrase);
                //string EncryptedNetworkPass = EncryptString(TravelmartNetworkPass, TravelmartPassphrase);

                // Decryption
                //string DecryptedNetworkUser = DecryptString(TravelmartNetworkUser, TravelmartPassphrase);
                //string DecryptedNetworkPass = DecryptString(TravelmartNetworkPass, TravelmartPassphrase);

                if (sTo != "")
                {                    
                    string sPassphrase = "Great Vacations Begin With Great Employees!";
                    string sUser = DecryptString(ConfigurationSettings.AppSettings["TravelmartMailUser"].ToString(), sPassphrase);
                    string sPwd = DecryptString(ConfigurationSettings.AppSettings["TravelmartMailPassword"].ToString(), sPassphrase);

                    string sEmail = ConfigurationSettings.AppSettings["RCCLSupportEmail"].ToString();
                    string sEmailDisplay = ConfigurationSettings.AppSettings["RCCLSupportEmailDisplay"].ToString();

                    string sServer = ConfigurationSettings.AppSettings["TravelmartMailServer"].ToString();
                    int iPort = GlobalCode.Field2Int(ConfigurationSettings.AppSettings["TravelmartMailPort"]);

                    bool isEnableSSL = GlobalCode.Field2Bool(ConfigurationSettings.AppSettings["EnableSSL"]);

                    sFrom = sEmail;

                    MailMessage emailMsg = new MailMessage();

                    emailMsg.From = new MailAddress(sFrom, sEmailDisplay);

                    for (int i = 0; i < recipients.Length; i++)
                    {
                        MailAddress emailTo = new MailAddress(recipients[i]);
                        emailMsg.To.Add(emailTo);
                    }

                    if (sCcs != "")
                    {
                        carboncopy = sCcs.Split(';');

                        for (int i = 0; i < carboncopy.Length; i++)
                        {
                            MailAddress emailCc = new MailAddress(carboncopy[i]);
                            emailMsg.CC.Add(emailCc);
                        }
                    }

                    for (int i = 0; i < attachments.Length; i++)
                    {
                        Attachment data = new Attachment(attachments[i], MediaTypeNames.Application.Octet);
                        emailMsg.Attachments.Add(data);
                    }

                    emailMsg.Subject = sSubject;
                    emailMsg.IsBodyHtml = true;
                    emailMsg.Body = sMessage;

                    //details from web config
                    //SmtpClient emailClient = new SmtpClient();
                    //emailClient.Host = "mail.ptc.com.ph";
                    //emailClient.Host = ConfigurationSettings.AppSettings["TravelmartMailServer"].ToString();

                    SmtpClient emailClient = new SmtpClient(sServer, iPort);
                    emailClient.Credentials = new System.Net.NetworkCredential(sUser, sPwd);
                    emailClient.EnableSsl = isEnableSSL;
                    emailClient.Send(emailMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Encryption and Decryption
        #region MD5 Encryption
        /// <summary>
        /// Date Created:  29/03/2012
        /// Created By:    Gabriel Oquialda
        /// (description)  MD5 Encryption function
        /// </summary>   
        /// <param name="NetworkUser"></param>
        /// <param name="Passphrase"></param>
        public static string EncryptString(string NetworkUser, string Passphrase)
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

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(NetworkUser);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }
        #endregion
        #region MD5 Decryption
        /// <summary>
        /// Date Created:  29/03/2012
        /// Created By:    Gabriel Oquialda
        /// (description)  MD5 Decryption function
        /// </summary>   
        /// <param name="NetworkUser"></param>
        /// <param name="Passphrase"></param>
        public static string DecryptString(string NetworkUser, string Passphrase)
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
        #endregion        
        #endregion

        public static DateTime GetCurrentDateTime()
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";
            DateTime currentDate;
            currentDate = DateTime.ParseExact(DateTime.Now.ToString(dtFormaString), dtFormaString, enCulture);
            return currentDate;
        }        
        public static DateTime GetDateTimeGMT(DateTime dDate)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";
            DateTime currentDate = dDate.ToUniversalTime();
            currentDate = DateTime.ParseExact(currentDate.ToString(dtFormaString), dtFormaString, enCulture);
            return currentDate;
        }
        public static DateTime ConvertDateTimeByFormat(string sDateTime)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            string dtFormaString = "MM/dd/yyyy HH:mm:ss:fff";
            DateTime dDate;
            dDate = DateTime.ParseExact(sDateTime, dtFormaString, enCulture);
            return dDate;
        }
        /// <summary>
        /// Date Created:   17/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Convert Date based from dafault format
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public static DateTime ConvertDateByFormat(string sDate)
        {
            CultureInfo enCulture = new CultureInfo(TravelMartVariable.UserCultureInfo);
            
            //string dtFormaString = "MM/dd/yyyy";
            DateTime dDate;
            dDate = DateTime.Parse(sDate, enCulture);
            return dDate;
        }

        /// <summary>            
        /// Date Created: 15/11/2011
        /// Created By: Ryan Bautista
        /// (description) Insert email log 
        /// </summary>
        //public static void InsertEmailLog(String To, String strCc, String From, String Subject, String FileName, DateTime SentDate, String CreatedBy) 
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = null;

        //    DbConnection connection = db.CreateConnection();
        //    connection.Open();
        //    DbTransaction trans = connection.BeginTransaction();

        //    try
        //    {                
        //        string strTimeZone = TimeZone.CurrentTimeZone.StandardName.ToString();
        //        //string strFileName = string.Empty;

        //        //if (FileNamePDF != null || FileNamePDF != "")
        //        //{
        //        //    strFileName = FileNameXLS + "and" + FileNamePDF;
        //        //}
        //        //else
        //        //{
        //        //    strFileName = FileNameXLS;
        //        //}

        //        dbCommand = db.GetStoredProcCommand("uspInsertEmailSent");

        //        db.AddInParameter(dbCommand, "@pTo", DbType.String, To);
        //        db.AddInParameter(dbCommand, "@pCc", DbType.String, strCc);
        //        db.AddInParameter(dbCommand, "@pFrom", DbType.String, From);                
        //        db.AddInParameter(dbCommand, "@pSubject", DbType.String, Subject);
        //        db.AddInParameter(dbCommand, "@pFileName", DbType.String, FileName);
        //        db.AddInParameter(dbCommand, "@pSentDate", DbType.DateTime, SentDate);
        //        db.AddInParameter(dbCommand, "@pCreatedBy", DbType.String, CreatedBy);

        //        db.ExecuteNonQuery(dbCommand, trans);
        //        trans.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //        {
        //            connection.Close();
        //            connection.Dispose();
        //        }
        //        if (dbCommand != null)
        //        {
        //            dbCommand.Dispose();
        //        }
        //    }
        //}

        /// <summary>            
        /// Date Created: 23/08/2011
        /// Created By: Ryan Bautista
        /// (description) Selecting all email sent
        /// </summary>  
        //public static DataTable GetEmailSent()
        //{
        //    Database SFDatebase = DatabaseFactory.CreateDatabase();
        //    DbCommand SFDbCommand = null;
        //    DataTable SFDataTable = null;
        //    try
        //    {
        //        SFDbCommand = SFDatebase.GetStoredProcCommand("uspSelectEmailSent");
        //        SFDataTable = SFDatebase.ExecuteDataSet(SFDbCommand).Tables[0];
        //        return SFDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (SFDbCommand != null)
        //        {
        //            SFDbCommand.Dispose();
        //        }
        //        if (SFDataTable != null)
        //        {
        //            SFDataTable.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 14/02/2012
        /// Description: Send Async email
        /// </summary>
        /// <param name="sFrom"></param>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        /// <param name="sCC"></param>
        //public void SendApprovalEmail(string sFrom, string sSubject, string sMessage, string Role)
        //{
        //    //string[] sendTo = sTo.ToString();
           
        //    try
        //    {
        //        var SendTo = (from a in HotelBookings.HotelEmail
        //                      where a.RoleName == Role
        //                      select new
        //                      {
        //                          a.EmailAddress,
        //                      }).ToList();

        //        var SendCC = (from a in HotelBookings.HotelEmail
        //                      where a.RoleName != Role
        //                      select new
        //                      {
        //                          a.EmailAddress,
        //                      }).ToList();

        //        MailMessage mail = new MailMessage();
        //        sFrom = "travelmart.ptc@gmail.com";
        //        mail.From = new MailAddress(sFrom);
        //        for (int i = 0; i < SendTo.Count; i++)
        //        {
        //            mail.To.Add(new MailAddress(SendTo[i].EmailAddress));
        //        }
        //        for (int i = 0; i < SendCC.Count; i++)
        //        {
        //            mail.CC.Add(new MailAddress(SendCC[i].EmailAddress));
        //        }

        //        mail.IsBodyHtml = true;
        //        mail.Subject = sSubject;
        //        mail.Body = sMessage;
                

        //        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //        smtpClient.Credentials = new System.Net.NetworkCredential("travelmart.ptc", "password@ptc");
        //        smtpClient.EnableSsl = true;
        //        Object state = mail;

        //        smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);

        //        try
        //        {
        //            smtpClient.SendAsync(mail, state);
        //        }
        //        catch (Exception ex)
        //        {
        //            mail.Dispose();
        //            smtpClient = null;                    
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
           
        //}

        //public void SendAsyncEmail(string sTo, string sSubject, string sMessage)
        //{
        //    //string[] sendTo = sTo.ToString();

        //    try
        //    {
        //        string sFrom="";
        //        MailMessage mail = new MailMessage();
        //        sFrom = "travelmart.ptc@gmail.com";
        //        mail.From = new MailAddress(sFrom);
        //        mail.To.Add(new MailAddress(sTo));

        //        mail.IsBodyHtml = true;
        //        mail.Subject = sSubject;
        //        mail.Body = sMessage;


        //        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //        smtpClient.Credentials = new System.Net.NetworkCredential("travelmart.ptc", "password@ptc");
        //        smtpClient.EnableSsl = true;
        //        Object state = mail;

        //        smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);

        //        try
        //        {
        //            smtpClient.SendAsync(mail, state);
        //        }
        //        catch (Exception ex)
        //        {
        //            mail.Dispose();
        //            smtpClient = null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{

        //    MailMessage mail = e.UserState as MailMessage;

        //    if (!e.Cancelled && e.Error != null)
        //    {
        //        //System.Diagnostics.Trace.
        //        //message.Text = "Mail sent successfully";
        //        //Console.WriteLine("error");
        //    }

        //    mail.Dispose();
        //}
        
    }
}

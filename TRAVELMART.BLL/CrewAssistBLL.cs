using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Web;
//using System.Web.Services;
using System.Collections;



namespace TRAVELMART.BLL
{
    public class CrewAssistBLL
    {

        public List<SeafarerList> SeafarerList(short loadtype, string Seafarer, string userID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SeafarerList(loadtype, Seafarer, userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SeafarerDetailHeader> SeafarerDetailList(short loadtype, long SeafarerID, string UserID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SeafarerDetailTable(loadtype, SeafarerID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Date Created:   19/01/2012
        /// Created By:     Muhallidin G Wali
        /// description   Get hotel , Seaport and ExpendType List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssistGenericClass> GetGetHotelPortExpendTypeList(string userString, string regionString, string portString)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetGetHotelPortExpendTypeList(userString, regionString, portString);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Date Created:   19/01/2012
        /// Created By:     Muhallidin G Wali
        /// description   Get hotel , Seaport and ExpendType List
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssistHotelList> GetGetHotelPortExpendTypeList(short lodType, string userString, string regionString, string portString)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetGetHotelPortExpendTypeList(lodType, userString, regionString, portString);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CrewAssistHotelInformation> CrewAssistHotelInformation(int TravelRequestID, int BranchID,
            string PortCode, string Arrcode, DateTime RequestDate,long EmployeeID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.CrewAssistHotelInformation(TravelRequestID, BranchID, PortCode, Arrcode, RequestDate, EmployeeID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Save SF Hotel Request
        /// -------------------------------------------
        /// Date Modified: 30/May/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add Shuttle, MealLunchDinner, Tax Bit and Tax Percent
        ///                Add fields for audit trail use 
        /// -------------------------------------------
        /// </summary>
        public string SeafarerSaveRequest(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
            double ContractedRate, string EmailTO, string HotelCity, short ReqSource,
            string ContactName, string ContactNo, string Recipient,
            string CCEmail, string BlindCopy, double RateConfirm, bool IsMedical, long TransHotelID, List<HotelRequestCompanion> HRC)
            
        {
            
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SeafarerSaveRequest(RequestNo, SfID, LastName, FirstName, Gender, RegionID,
                    PortID, AirportID, HotelID, CheckinDate, CheckoutDate, NoNites, RoomType,
                    MealBreakfast, MealLunch, MealDinner, MealLunchDinner, WithShuttle,
                    RankID, VesselInt, CostCenter, Comments, SfStatus, TimeIn, TimeOut,
                    RoomAmount, IsRoomTax, RoomTaxPercent, UserID, TrID, Currency,
                    strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, IsAir, SequentNo, IDBig,
                    BrandID, MealVoucher, ConfirmRate, ContractedRate, EmailTO, HotelCity, ReqSource,
                    ContactName, ContactNo, Recipient, CCEmail, BlindCopy, RateConfirm, IsMedical,TransHotelID, HRC);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //public void SendHotelTransactionOtherRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName)
        //{
        //    try
        //    {
        //        CrewAssistDAL CA = new CrewAssistDAL();
        //        CA. SendHotelTransactionOtherRequest(RequestID, UserID, ContactName, ContactNo, Description,  Function,  FileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}


        /// <summary>
        // =============================================
        // Author:			Muhallididn G Wali
        // Create date:		04/02/2013
        // Description:		Submit SF Hotel Request. Booked and add in TblHotelTransactionOther
        // =============================================
        /// </summary>
        public List<CrewAssistEmailDetail> SendHotelTransactionOtherRequest(string RequestID, string UserID, string ContactName, string ContactNo,
            string Description, string Function, string FileName)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendHotelTransactionOtherRequest(RequestID, UserID, ContactName, ContactNo, Description, Function, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        // =============================================
        // Author:			Muhallididn G Wali
        // Create date:		04/02/2013
        // Description:		Submit SF Hotel Request. Booked and add in TblHotelTransactionOther
        // =============================================
        /// </summary>
        public string SendHotelTransactionOtherRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName
            , string Reciever, string Recipient, string BlindCopy, double ConfirmRate)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendHotelTransactionOtherRequest(RequestID, UserID, ContactName, ContactNo, Description, Function, FileName, Reciever, Recipient, BlindCopy, ConfirmRate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        // =============================================
        // Author:			Muhallididn G Wali
        // Create date:		04/02/2013
        // Description:		Submit SF Hotel Request. Booked and add in TblHotelTransactionOther
        // =============================================
        /// </summary>
        public string SendHotelTransactionPortAgentRequest(long HotelTransID, long RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName
            , string Reciever, string Recipient, string BlindCopy)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendHotelTransactionPortAgentRequest(HotelTransID, RequestID, UserID, ContactName, ContactNo, Description, Function, FileName, Reciever, Recipient, BlindCopy);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Modified: 27/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request
        /// </summary>
        public void SeafarerSaveComapnionRequest(string RequestDetailID, string RequestID, string LastName, string FirstName, string Relationship, string Gender, string UserID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                CA.SeafarerSaveComapnionRequest( RequestDetailID, RequestID, LastName, FirstName, Relationship, Gender, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public void SeafarerSaveComapnionRequest(ref DataTable dt, string RequestDetailID, string RequestID
                , string LastName, string FirstName, string Relationship
                , string Gender, string UserID, long TravelRequestID, long IDBignt, int SeqNoInt)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                CA.SeafarerSaveComapnionRequest(ref dt, RequestDetailID, RequestID, LastName, FirstName, Relationship, Gender, UserID, TravelRequestID , IDBignt, SeqNoInt);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<crewassistrequest> GetHotelRequest(Int16 LoadType, long RequestID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetHotelRequest(LoadType, RequestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// Author:		    Muhallidin G Wali
        /// Create date:    09/26/2013
        /// Description:	Add Copy Email 
        /// </summary>

        public void TblEmail(int EmailTypeID, string EmailType, string Email, long EmailFromID, string UserID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                CA.TblEmail(EmailTypeID, EmailType, Email, EmailFromID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public List<CopyEmail> GetTblEmail(Int16 LoadType, Int16 EmailTypeID, long EmailHotelID, long EmailCrewAssistID, long EmailSchedulerID, long EmailShipID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetTblEmail(LoadType, EmailTypeID, EmailHotelID, EmailCrewAssistID, EmailSchedulerID, EmailShipID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Hotel, Port, ExpendType
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<VehicleVendor> GetVendorVehicleDetail(short LoadType, long VehicleVendorID, string PortCode)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetVendorVehicleDetail(LoadType, VehicleVendorID, PortCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save SF Companion Hotel Request 
        /// </summary>
        public List<CrewAssistTranspo> SendVehicleTransactionRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendVehicleTransactionRequest(RequestID, UserID, ContactName, ContactNo, Description, Function, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string SendVehicleTransactionRequest(string RequestID, string UserID, string ContactName, string ContactNo, string Description, string Function, string FileName
            , string Reciever, string Recipient, string BlindCopy, bool IsPortAgent)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendVehicleTransactionRequest(RequestID, UserID, ContactName, ContactNo, Description, Function, FileName, Reciever, Recipient, BlindCopy, IsPortAgent);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist transportation Request
        /// </summary>
        public string SaveVehicleRequest(List<CrewAssistTranspo> list)
        {
            CrewAssistDAL CA = new CrewAssistDAL();
            return CA.SaveVehicleRequest(list);
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Crew assist transportation Request
        /// </summary>
        public string SaveTransportationeReques(List<CrewAssistTranspo> list)
        {
            CrewAssistDAL CA = new CrewAssistDAL();
            return CA.SaveTransportationeReques(list);
        }


        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist transportation Request
        /// </summary>
        public string InsertVehicleRequest(List<CrewAssistTranspo> list)
        {
            CrewAssistDAL CA = new CrewAssistDAL();
            return CA.InsertVehicleRequest(list);
        }

        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Hotel, Port, ExpendType
        /// ----------------------------------------------
        /// </summary>\\\\\\\\
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssistTranspo> getVehicleRequest(short LoadType, long VehicleVendorID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.getVehicleRequest(LoadType, VehicleVendorID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Created:    26/09/2011
        /// Created By:      Muhallidin G Wali
        /// (description)    Gett Visa nationality
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public string GetNationalityVisa(short LoadType, int NationalityIDInt, int VisitVisaIDInt)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetNationalityVisa(LoadType, NationalityIDInt, VisitVisaIDInt);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        /// <summary>
        /// Date Created:    17/10/2013
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Airport, Seaport
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssitPortList> GetPort(short LoadType, string userString)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetPort(LoadType, userString);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Date Created:    17/10/2013
        /// Created By:      Muhallidin G Wali
        /// (description)    Select Airport, Seaport
        /// ----------------------------------------------
        /// </summary>
        /// <param name="userString"></param>
        /// <param name="cityString"></param>
        /// <returns></returns>
        public List<CrewAssitPortListClass> GetPort(string userString)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetPort(userString);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Get the Crew assist Meet and greet 
        /// </summary>
        public List<MeetAndGreetGenericClass> GetMeetAndGreetAirport(short LoadType, int MeetAndGreetID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetMeetAndGreetAirport(LoadType, MeetAndGreetID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SaveMeetAndGreetRequest(List<CrewAssistMeetAndGreet> list)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SaveMeetAndGreetRequest(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Create:    22/10/2013
        /// Create By:      Muhallidin G Wali
        /// (description)  	get port agent detail
        /// </summary>
        public List<CrewAssistVendorPortAgent> GetVendorPortAgent(short LoadType, long PortAgentVendorID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetVendorPortAgent(LoadType, PortAgentVendorID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SavePortAgentRequest(List<CrewAssistPortAgentRequest> list
                , List<VehicleTransactionPortAgent> VehicleTranPortAgent, List<HotelTransactionPortAgent> HotelTranPortAgent)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SavePortAgentRequest(list, VehicleTranPortAgent, HotelTranPortAgent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public List<CrewAssistMeetAndGreet> SendMeetAndGreetTransactionRequest(long MeetAndGreetID, string UserID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendMeetAndGreetTransactionRequest(MeetAndGreetID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Port agent Request to table PortAgentTransaction
        /// </summary>
        public List<CrewAssistPortAgentRequest> SendPortAgentTransactionRequest(long MeetAndGreetID, string UserID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendPortAgentTransactionRequest(MeetAndGreetID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Date Created: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Save the Port agent Request to table PortAgentTransaction
        /// </summary>
        public string SendPortAgentTransactionRequest(long MeetAndGreetID, string UserID
             , string Reciever, string Recipient, string BlindCopy)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SendPortAgentTransactionRequest(MeetAndGreetID, UserID, Reciever, Recipient, BlindCopy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Marco Abejar
        /// (description)  Save SF Hotel Request
        /// -------------------------------------------
        /// Date Modified: 30/May/2013
        /// Modified By:   Josephine Gad
        /// (description)  Add Shuttle, MealLunchDinner, Tax Bit and Tax Percent
        ///                Add fields for audit trail use 
        /// -------------------------------------------
        /// </summary>
        public string SeafarerSaveRequestHotelOverflow(string RequestNo, string SfID, string LastName, string FirstName, string Gender, string RegionID,
            string PortID, string AirportID, string HotelID, string CheckinDate, string CheckoutDate, string NoNites, string RoomType,
            bool MealBreakfast, bool MealLunch, bool MealDinner, bool MealLunchDinner, bool WithShuttle,
            string RankID, string VesselInt, string CostCenter, string Comments, string SfStatus, string TimeIn, string TimeOut,
            string RoomAmount, bool IsRoomTax, string RoomTaxPercent, string UserID, string TrID, string Currency,
            string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            bool IsAir, int SequentNo, long IDBig, int BrandID, double MealVoucher, double ConfirmRate,
            double ContractedRate, string EmailTO, string HotelCity)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SeafarerSaveRequestHotelOverflow(RequestNo, SfID, LastName, FirstName, Gender, RegionID,
                    PortID, AirportID, HotelID, CheckinDate, CheckoutDate, NoNites, RoomType,
                    MealBreakfast, MealLunch, MealDinner, MealLunchDinner, WithShuttle,
                    RankID, VesselInt, CostCenter, Comments, SfStatus, TimeIn, TimeOut,
                    RoomAmount, IsRoomTax, RoomTaxPercent, UserID, TrID, Currency,
                    strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, IsAir, SequentNo, IDBig
                    , BrandID, MealVoucher, ConfirmRate, ContractedRate, EmailTO, HotelCity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Date Created: 28/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Select Vendor Safeguard
        /// </summary>
        public List<CrewAssistSafeguardVendor> GetVendorSafeguard(short Loadtype, long SafeguardVendorIDInt)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetVendorSafeguard(Loadtype, SafeguardVendorIDInt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Inse
        /// </summary>
        public string SaveSafeguardRequest(List<CrewAssistSafeguardRequest> list)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.SaveSafeguardRequest(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Get Transportation Rout
        /// </summary>
        public List<ComboGenericClass> GetTransportationRoute(string LoadType, long TransportID, long TravelRequestID, long RecLocID, int SeqNo, string PortCode, int PortID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetTransportationRoute(LoadType, TransportID, TravelRequestID, RecLocID, SeqNo, PortCode, PortID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Get Transportation Rout
        /// </summary>
        public List<CrewAssistGenericClass> GetComboGeneric(short LoadType, string PortCode, string ArrCode, int PortID, string storeproc)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetComboGeneric(LoadType, PortCode, ArrCode, PortID, storeproc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CrewAssistTransaction> GetCrewTransaction(short LoadType, long SeafarerID,
            long TravelRequestID, long IDBigInt, long SeqNo, DateTime Startdate, string DepCode,
             string ArrCode, bool IsAir, string UserID)
        {
            try
            {
                CrewAssistDAL CA = new CrewAssistDAL();
                return CA.GetCrewTransaction(LoadType, SeafarerID, TravelRequestID, IDBigInt, SeqNo, Startdate, DepCode, ArrCode, IsAir, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Date Created:   16/Nov/2013
        /// Created By:    Josephine Gad
        /// (description)   Get Vendors of Port Selected
        /// 
        /// </summary>
        public void GetVendors(Int16 iLoadTpe, string sUsername, int iSeaPortID, int iAirPortID)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            DAL.GetVendors(iLoadTpe, sUsername, iSeaPortID, iAirPortID);
        }
        /// <summary>
        /// Date Modified: 18/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Send Email for Hotel Request
        /// </summary>
        public string SendEmailHotel(Int32 iRequestID, string sUserID, string sReceiver, string sRecipient, string sBlindCopy)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.SendEmailHotel(iRequestID, sUserID, sReceiver, sRecipient, sBlindCopy);
        }
        /// <summary>
        /// Date Modified: 17/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Send Email for Transportation Request
        /// </summary>
        public string SendEmailTransport(Int32 iTransactionID, string sUserID, string sReceiver, string sRecipient, string sBlindCopy)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.SendEmailTransport(iTransactionID, sUserID, sReceiver, sRecipient, sBlindCopy);
        }
        /// <summary>
        /// Date Modified: 18/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Send Email for Hotel Request
        /// </summary>
        public string SendEmailPortAgent(Int32 iPortAgentReqID, string sUserID, string sReceiver, string sRecipient, string sBlindCopy)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.SendEmailPortAgent(iPortAgentReqID, sUserID, sReceiver, sRecipient, sBlindCopy);
        }


        public List<CrewAssisRemark> InsertCrewAssistRemark(long RemarkID, long TravelReqIdInt
                                , string RamarksVarchar, string UserID, long IDBigInt, string Role, short ReqSource
                                , long SeafarerID, int RemarkTypeID, short RemarkStatusID, string SummaryCall
                                , int RemarkRequestorID, DateTime TransactionDate, DateTime TransactionTime
                                , string PortCode, bool IR)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.InsertCrewAssistRemark(RemarkID, TravelReqIdInt, RamarksVarchar, UserID, IDBigInt, Role, ReqSource, SeafarerID, RemarkTypeID, RemarkStatusID, SummaryCall, RemarkRequestorID, TransactionDate, TransactionTime, PortCode, IR);
        }

        public List<CrewAssisRemark> DeleteCrewAssistRemarks(long RemarkID, long TravelReqIdInt,
                                string RamarksVarchar, string UserID)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.DeleteCrewAssistRemarks(RemarkID, TravelReqIdInt, RamarksVarchar, UserID);
        }

        public List<CrewAssisVehicleCost> GetCrewAssisVehicleCost(int VehicleVendorID, long TravelReqID, int VehicleTypeID, string PortCode, string UserID)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.GetCrewAssisVehicleCost(VehicleVendorID, TravelReqID, VehicleTypeID, PortCode, UserID);
        }

        public List<CrewAssistHotelInformation> GetPortAgentHotelVendor(short LoadType, long PortAgentVendorID, long TravelRequestID, long IDBigInt, string PortCode)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.GetPortAgentHotelVendor(LoadType, PortAgentVendorID, TravelRequestID, IDBigInt, PortCode);
        }

        // <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public string SavePortAgentHotelRequest(List<HotelTransactionPortAgent> HotelTranPortAgent, string UserID, List<HotelRequestCompanion> HRC)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.SavePortAgentHotelRequest(HotelTranPortAgent, UserID, HRC);
        }

        public string SaveCancelPortAgentHotelRequest(short LoadType, long TransHotelID, string UserID, string Reciever
            , string Recipient, string BlindCopy, string comment)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.SaveCancelPortAgentHotelRequest(LoadType, TransHotelID, UserID, Reciever, Recipient, BlindCopy, comment);
        }

        /// <summary>
        /// Date Modified: 10/10/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Insert the Crew assist Meet and greet Request
        /// </summary>
        public List<CrewAssisVehicleCost> GetCrewAssistTransportationCost(short LoadType, int VehicleVendorID, long TravelReqID, int VehicleTypeID, int FromID, int ToID, string PortCode, string UserID)
        {
            CrewAssistDAL DAL = new CrewAssistDAL();
            return DAL.GetCrewAssistTransportationCost(LoadType, VehicleVendorID, TravelReqID, VehicleTypeID, FromID, ToID, PortCode, UserID);

        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    13/Mar/2014
        /// Description:    Get Vehicle Route
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public string GetVehicleRoute(short LoadType, long TravelReqID, long IDBigInt, int SeqNo, string Status, string Route, string PortCode, int RouteID)
        {
            try
            {


                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.GetVehicleRoute(LoadType, TravelReqID, IDBigInt, SeqNo, Status, Route, PortCode, RouteID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CrewAssistTranspo> DeleteTransportationRequest(short LoadType, long SeafarerID, long TranslID, long TransactionID
                , long IDBigint, long TravelReqID, int SeqNo, string UserID, string Sender, string Recipient, string BlindCopy, string Comment)
        {
            try
            {

                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.DeleteTransportationRequest(LoadType, SeafarerID, TranslID, TransactionID, IDBigint
                            , TravelReqID, SeqNo, UserID, Sender, Recipient, BlindCopy, Comment);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


         /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    20/Jun/2014
        /// Description:    Cancel Hotel booking
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public List<SeafarerDetailHeader> GetCancelCrewAssistHotelRequest(short LoadType, long HotelTransID, string UserID
                                        ,string Email, string CCEmail, string Confirmby, string Comment
                                        ,DateTime CancelDate, string Timezone, string GMTDATE)
        {
            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.GetCancelCrewAssistHotelRequest(LoadType, HotelTransID, UserID, Email, CCEmail, Confirmby, Comment, CancelDate, Timezone, GMTDATE);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    20/Jun/2014
        /// Description:    Cancel Transpotation booking
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public List<SeafarerDetailHeader> GetCrewAssistCancelTransportationRequest(short LoadType, long VehicleTransID, long TravelRequestID
                                    , long RecLocID, int SeqNo, string UserID, string Email, string CCEmail, string Confirmby
                                    , string Comment, DateTime CancelDate, string Timezone, string GMTDATE)
        {

            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.GetCrewAssistCancelTransportationRequest(LoadType,VehicleTransID, TravelRequestID, RecLocID, SeqNo, UserID, Email, CCEmail, Confirmby, Comment, CancelDate, Timezone, GMTDATE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// DateCreated:    20/Jun/2014
        /// Description:    check overflow Hotel
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        public string GetHotelTransactionOverFlow(long SeafarerID, DateTime CheckInDate, DateTime CheckoutDate, long IDBigint, short SeqNo, long TravelReqId, int BranchID, string PortCode, long TransHotelID)
        {
            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.GetHotelTransactionOverFlow(SeafarerID, CheckInDate, CheckoutDate, IDBigint, SeqNo, TravelReqId, BranchID, PortCode, TransHotelID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SeafarerDetailHeader> GetAllPastTravelRequest(short loadtype, long SeafarerID, string UserID)
        {
            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.GetAllPastTravelRequest(loadtype, SeafarerID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Modified: 26/03/2013
        /// Modified By:   Muhallidin G Wali
        /// (description)  Send SF Companion Hotel Request 
        /// </summary>
        public void SendCompanionHotelTransaction(long RequestIDInt, string UserID, string Reciever, string Recipient, string BlindCopy, bool IsOverFlowRequest)
        {
            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                DAL.SendCompanionHotelTransaction(RequestIDInt, UserID, Reciever, Recipient, BlindCopy, IsOverFlowRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        /// <summary>
        /// Created By:    Muhallidin G Wali
        /// Date Modified: 10/24/2014
        /// (description)  Insert Air Status.
        /// </summary>
        public List<SeafarerDetailHeader> InsertAirTransactionStatus(long TravelRequestID, long IdBigint, int SeqNo, int StatusID, string OldStatus, string UserID)
        {
            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                return  DAL.InsertAirTransactionStatus(TravelRequestID, IdBigint, SeqNo, StatusID,OldStatus, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   05/Aug/2016
        /// Descrption:     Get vendor contracted amount
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        //[WebMethod]
        public  ArrayList GetVendorContractAmount(short LT, int BID, int RID, DateTime cdate)
        {
            try
            {
                CrewAssistDAL DAL = new CrewAssistDAL();
                return DAL.GetVendorContractAmount(LT, BID, RID, cdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

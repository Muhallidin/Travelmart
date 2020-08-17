using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class AirBLL
    {
        public static IDataReader GetSFAirTravelDetailsById(string AirID, string SeqNo)
        {
            IDataReader dt = null;
            try
            {
                dt = AirDAL.GetSFAirTravelDetailsById(AirID, SeqNo);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public static IDataReader GetSFAirTravelDetailsOtherById(string AirIdBigint)
        {
            IDataReader dt = null;
            try
            {
                dt = AirDAL.GetSFAirTravelDetailsOtherById(AirIdBigint);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public static void UpdateAirBooking(string AirIdBigint,string SeqNo, string AirStatusString, bool IsBillToCrew,
            string RemarksString, string ModifiedByString)
        {
            AirDAL.UpdateAirBooking(AirIdBigint, SeqNo, AirStatusString, IsBillToCrew, RemarksString, ModifiedByString);
        }
        public static void InsertAirBookingOther(string TravelReqIDString, string FlightNoString,
            string ArrivalDateString, string DepartureDateString, string DepartureLoc, string ArrivalLoc,
            string AirlineString, string TicketNoString, string AirStatusString, string CreatedByString,
            string StatusString, string RemarksString, bool IsBillToCrew)
        {
            AirDAL.InsertAirBookingOther(TravelReqIDString, FlightNoString, ArrivalDateString,
                DepartureDateString, DepartureLoc, ArrivalLoc, AirlineString, TicketNoString, AirStatusString, CreatedByString,
                StatusString, RemarksString, IsBillToCrew);
        }
        public static void UpdateAirBookingOther(string AirIDString, string FlightNoString,
            string ArrivalDateString, string DepartureDateString, string DepartureLoc, string ArrivalLoc,
            string AirlineString, string TicketNoString, string AirStatusString, string ModifiedByString,
            string StatusString, string RemarksString, bool IsBillToCrew)
        {
            AirDAL.UpdateAirBookingOther(AirIDString, FlightNoString,
            ArrivalDateString, DepartureDateString,  DepartureLoc,  ArrivalLoc,
            AirlineString, TicketNoString, AirStatusString,  ModifiedByString,
            StatusString, RemarksString,  IsBillToCrew);        
        }
        public static DataTable GetAirline()
        {
            DataTable dt = null;
            try
            {
                dt = AirDAL.GetAirline();
                return dt;
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
        /// Date Created:   19/01/2012
        /// Created By:     Gelo Oquialda
        /// (description)   Get Airport List By country ID
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static DataTable GetAirportByCountry(string CountryID, string RegionID, string AirportName, string AirportInitial)
        {
            DataTable dt = null;
            try
            {
                dt = AirDAL.GetAirportByCountry(CountryID, RegionID, AirportName, AirportInitial);
                return dt;
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
        /// Date Created:   04/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Airport List By Region
        /// ----------------------------------------------
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public static List<AirportDTO> GetAirportByRegion(string RegionID, string AiportName)
        {
            return AirDAL.GetAirportByRegion(RegionID, AiportName);
        }
        /// <summary>
        /// Date Created: 20/01/2012
        /// Created By: Gelo Oquialda
        /// (description) Load airport list by region
        /// </summary>
        //public static DataTable AirportLoad(string regionString, string userstring)
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        dt = AirDAL.AirportLoad(regionString, userstring);
        //        return dt;
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
        /// <summary>
        /// Date Created: 27/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Loads airport code with airport name
        /// </summary>
        public static IDataReader GetAirportCodeName(int AirportID)
        {
            IDataReader dr = null;
            try
            {
                dr = AirDAL.GetAirportCodeName(AirportID);
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Date Created: 31/01/2012
        /// Created By:   Gelo Oquialda
        /// (description) Insert airport hotel assignment
        /// </summary>
        public static Int32 InsertAirportHotel(Int32 AirportID, Int32 BranchID, String User, Int16 iRoomType,
            string sLogDescription, string sFunction, string sPageName)
        {
            try
            {
                Int32 AirportHotelID = 0;
                AirDAL.InsertAirportHotel(AirportID, BranchID, User, iRoomType, sLogDescription, sFunction, sPageName);
                return AirportHotelID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }       
    }
}

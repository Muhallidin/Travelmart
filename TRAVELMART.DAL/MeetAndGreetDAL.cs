using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.Common;
using System.Web;
using TRAVELMART.DAL;
using System.Data;

namespace TRAVELMART.DAL
{
    public class MeetAndGreetDAL
    {
        public static void MeetAndGreetVendorsGetByID(int iVendorID, short iLoadType)
        {
            throw new NotImplementedException();
        }

        public static List<MeetAndGreetList> MeetAndGreetVendorsGet(string sVehicleVendorName, string sOrderyBy, int iStartRow, int iMaxRow)
        {

            List<MeetAndGreetList> list = new List<MeetAndGreetList>();

            return list;
        }

        public static void MeetAndGreetSave(Int32 iVehicleVendorID, string sVehicleVendorName,
           Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
           string sEmailCc, string sEmailTo, string sWebsite,
           string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
           DataTable dt, DataTable dtPlateNo)
        {
            throw new NotImplementedException();
        }

        public static void MeetandgreetVendorsGetByID(int iVendorID, short iLoadType)
        {
            throw new NotImplementedException();
        }

        public static void MeetandGreetVendorsGet(string sVehicleVendorName, string sOrderyBy, int iStartRow, int iMaxRow)
        {
            throw new NotImplementedException();
        }

        public static void MeetAndGreetVendorsSave(int iVehicleVendorID, string sVehicleVendorName, int iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress, string sEmailCc, string sEmailTo, string sWebsite, string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate, DataTable dt, DataTable dtPlateNo)
        {
            throw new NotImplementedException();
        }
    }
}

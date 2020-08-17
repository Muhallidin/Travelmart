using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TRAVELMART.DAL;
using System.Data;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class MeetAndGreetBLL
    {
        public static void MeetAndGreetVendorsGetByID(Int32 iVendorID, Int16 iLoadType)
        {
            //MeetAndGreetDAL.MeetandgreetVendorsGetByID(iVendorID, iLoadType);
        }

        public static List<MeetAndGreetList> MeetAndGreetVendorsGet(string sVehicleVendorName, string sOrderyBy, int iStartRow, int iMaxRow)
        {
            List<MeetAndGreetList> list = new List<MeetAndGreetList>();
            //MeetAndGreetDAL.MeetandGreetVendorsGet(sVehicleVendorName, sOrderyBy, iStartRow, iMaxRow);

            //list = (List<MeetAndGreetList>)HttpContext.Current.Session["MeetAndGreetList"];
            return list;
        }

        public static int MeetAndGreetVendorsGetCount(string sVehicleVendorName, string sOrderyBy)
        {
            return GlobalCode.Field2Int(HttpContext.Current.Session["MeetAndGreetCount"]);
        }

        /// <summary>
        /// Date Created:   13/Nov/2013
        /// Created By:     Jefferson Bermundo
        /// (description)   Save Meet and Greet Vendor Vendor
        /// ---------------------------------------------------------------     
        /// </summary>
        public static void MeetAndGreetSave(Int32 iVehicleVendorID, string sVehicleVendorName,
            Int32 iCityID, string sContactNo, string sFaxNo, string sContactPerson, string sAddress,
            string sEmailCc, string sEmailTo, string sWebsite,
            string UserId, string strLogDescription, string strFunction, string strPageName, DateTime DateGMT, DateTime CreatedDate,
            DataTable dt, DataTable dtPlateNo)
        {
            //MeetAndGreetDAL.MeetAndGreetVendorsSave(iVehicleVendorID, sVehicleVendorName,
            //iCityID, sContactNo, sFaxNo, sContactPerson, sAddress, sEmailCc, sEmailTo, sWebsite,
            //UserId, strLogDescription, strFunction, strPageName, DateGMT, CreatedDate, dt, dtPlateNo);
        }

        public static void MeetAndGreetInsertContract(int iContractID, int iVehicleVendorID,
            string sContractName, string sRemarks, string sDateStart,
            string sDateEnd, string sRCCLPerconnel, string sRCCLDateAccepted,
            string sVendorPersonnel, string sVendorDateAccepted, int iCurrency,
            string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtAttachment)
        {
            //MeetAndGreetDAL.MeetAndGreetInsertContract(iContractID, iVehicleVendorID,
            //sContractName, sRemarks, sDateStart, sDateEnd, sRCCLPerconnel, sRCCLDateAccepted,
            //sVendorPersonnel, sVendorDateAccepted, iCurrency, sUserName, sDescription, sFunction, sFilename,
            //sGMTDate,dtAttachment);
        }

        public static List<ContractMeetAndGreet> GetVendorMeetAndGreetBranchContractByBranchID(string VehicleBranchID)
        {
            //return MeetAndGreetDAL.GetVendorMeetAndGreetBranchContractByBranchID(VehicleBranchID);
             List<ContractMeetAndGreet> list = new List<ContractMeetAndGreet>();
             return list;
        }

        public static void UpdateMeetGreetContractFlag(Int32 ContractID, string Username,
            string sDescription, string sFunction, string sFilename, DateTime GMTDate)
        {
            //MeetAndGreetDAL.UpdateMeetGreetContractFlag(ContractID, Username, sDescription, sFunction, sFilename, GMTDate);
        }

        public static void GetVendorMeetAndGreetContractByContractID(string contractId, string branchId, Int16 iLoadType)
        {
            //MeetAndGreetDAL.GetVendorMeetAndGreetContractByContractID(contractId,  branchId, iLoadType);
        }
    }
}

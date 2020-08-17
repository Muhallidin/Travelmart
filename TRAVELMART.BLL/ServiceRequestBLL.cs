using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;
using System.Data;

namespace TRAVELMART.BLL
{
    public class ServiceRequestBLL
    {
        ServiceRequestDAL DAL = new ServiceRequestDAL();

        /// <summary>
        /// Date Modified: 17/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get Service Request List
        /// </summary>
        public void GetServiceRequestList(DateTime dDate, string sUser, int iStartRow, int iMaxRow,
            Int16 iLoad, string sOrderBy, Int16 iViewFilter, Int16 iViewActive, Int16 iViewBooked,
             Int16 iFilterType, Int64 iEmployeeID, string sCrewAssistUser)
        {
            DAL.GetServiceRequestList(dDate, sUser, iStartRow, iMaxRow, iLoad, sOrderBy,
                iViewFilter, iViewActive, iViewBooked, iFilterType, iEmployeeID, sCrewAssistUser);
        }
         /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   25/Oct/2013
        /// Descrption:     Cancel or Activate Service Request
        /// =============================================================
        /// </summary>
        public void CancelActivateServiceRequest(string sUserName, string sDescription, string sFunction, string sFilename,
            string sGMTDate, DataTable dtServiceRequest)
        {
            DAL.CancelActivateServiceRequest(sUserName, sDescription, sFunction, sFilename, sGMTDate, dtServiceRequest);
        }

        /// <summary>
        /// Date Modified: 22/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  GetServiceRequestEmail
        /// </summary>
        public void GetServiceRequestEmail(Int32 iHotelRequestID, Int32 iVehicleRequestID,
            Int32 iPortAgentRequestID, Int32 iMeetGreetRequestID, Int16 iLoadType)
        {
            //CrewAssistDAL DAL = new CrewAssistDAL();
            DAL.GetServiceRequestEmail(iHotelRequestID, iVehicleRequestID, iPortAgentRequestID, iMeetGreetRequestID, iLoadType);
        }
         /// <summary>
        /// Date Modified: 30/Oct/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get Service Request List for Export Use
        /// </summary>
        public DataTable GetServiceRequestExport(string sUser)
        {
            return DAL.GetServiceRequestExport(sUser);
        }
         /// <summary>
        /// Date Modified: 05/Nov/2013
        /// Modified By:   Josephine Gad
        /// (description)  Get Service Request List for Export Use
        /// </summary>
        /// <returns></returns>
        public List<CrewAssistUsers> GetCrewAssist()
        {
            return DAL.GetCrewAssist();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRAVELMART.DAL;
using TRAVELMART.Common;

namespace TRAVELMART.BLL
{
    public class MedicalBLL
    {
        public IMSClassList GetMedicalClassList(short LoadType, int BranchID)
        {
            try
            {
                MedicalDAL DAL = new MedicalDAL();
                return DAL.GetMedicalClassList(LoadType, BranchID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<CrewMemberInformation> GetHotelTransactionMedical(short loadType, long SeafarerID, string UserID)
        {
            try
            {
                MedicalDAL CA = new MedicalDAL();
                return CA.GetHotelTransactionMedical(loadType, SeafarerID, UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HotelTransactionMedical> InsertHotelTransactionMedical(List<HotelTransactionMedical> Medical)
        {

            try
            {
                MedicalDAL DAL = new MedicalDAL();
                return DAL.InsertHotelTransactionMedical(Medical);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VehicleTransactionMedical> InsertVehicleTransactionMedical(List<VehicleTransactionMedical> Medical)
        {
            try
            {
                MedicalDAL DAL = new MedicalDAL();
                return DAL.InsertVehicleTransactionMedical(Medical);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

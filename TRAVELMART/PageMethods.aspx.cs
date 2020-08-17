using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.Services;
using TRAVELMART.Common;
using TRAVELMART.BLL;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Web.Security;
using System.Collections;
using System.Web.Script.Serialization;
using System.Net;
using System.Drawing;
using System.Text;
//using TRAVELMART.BLL.CrewAssistBLL;

namespace TRAVELMART
{
    public partial class PageMethods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get vessel list based from user and date
        /// ========================================================        
        /// Date Modified:  15/03/2012
        /// Modified By:    Muhallidin G Wali
        /// (description)   Replace Global Variable with Session          
        /// ========================================================        
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// ========================================================
        /// </summary>
        /// <returns></returns>
        /// 
        [WebMethod]
        public static List<VesselDTO> ManualRequestGetVessel(string DateFrom, string DateTo)
        {
            List<VesselDTO> list = null;
            try
            {

                list = VesselBLL.GetVesselList(HttpContext.Current.User.Identity.Name.ToString(), DateFrom, DateTo, "0", "0", "0", "0", GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]), false);
                //dt = VesselBLL.GetVessel(MUser.GetUserName(), DateFrom, DateTo, "0", "0", "0", "0", MUser.GetUserRole());                
                //if (dt.Rows.Count > 0)
                //{
                //    VesselDTO VesselList;
                //    foreach (DataRow r in dt.Rows)
                //    {
                //        VesselList = new VesselDTO();
                //        VesselList.VesselIDString = r["VesselID"].ToString();
                //        VesselList.VesselNameString = r["VesselName"].ToString();
                //        //VesselList.PortIDString = r["PortID"].ToString();
                //        list.Add(VesselList);
                //    }
                //}
                VesselDTOList.VesselList = list;
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   17/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Get vessel details
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<VesselDetailsDTO> ManualRequestGetVesselDetails(string VesselID, string DateFrom)
        {
            List<VesselDetailsDTO> list = null;
            IDataReader dr = null;
            try
            {
                list = new List<VesselDetailsDTO>();
                dr = VesselBLL.GetVesselPortDetails(VesselID, DateFrom, DateFrom);
                if (dr.Read())
                {
                    VesselDetailsDTO VesselList;

                    VesselList = new VesselDetailsDTO();
                    VesselList.PortName = dr["PortName"].ToString();
                    VesselList.CountryName = dr["CountryName"].ToString();
                    VesselList.PortID = dr["PortID"].ToString();
                    VesselList.CountryID = dr["CountryID"].ToString();
                    list.Add(VesselList);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (list != null)
                {
                    list = null;
                }
            }
        }
        /// <summary>
        /// Date Created:   17/02/2012
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider list by port id
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<PortAgentDTO> ManualRequestGetPortAgentList(string PortID, string BranchID)
        {
            try
            {
                return PortBLL.GetPortAgentByPortID(PortID, BranchID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        ///Date Created: 09/03/2014
        ///Created By: Michael Brian C. Evangelista
        ///Description: Tag to Vehicle
        ///---------------------------------------------
        ///Date Modified:   06/Jan/2015
        ///Modified By:     Josephine Monteza
        ///Description:     Add parameter colVehicleVendorIDInt, sDescription, sFunction, sFileName
        /// </summary>
        [WebMethod]
        public static string TagtoVehicle(Int32 colIDBigint, Int32 colTravelReqIDInt, string colRecordLocatorVarchar,
            Int32 colSeafarerIdInt, string colOnOff, int colVehicleVendorIDInt, Int32 colPortAgentVendorIDInt, string colSFStatus, string UserId,
            string sDescription, string sFunction, string sFileName)
        {
            DataTable dt = null;
            try
            {

                VehicleBLL.TagtoVehicle(colIDBigint, colTravelReqIDInt, colRecordLocatorVarchar, colSeafarerIdInt, colOnOff,
                    colVehicleVendorIDInt, colPortAgentVendorIDInt, colSFStatus, UserId, sDescription, "TagtoVehicle", sFileName);
                return "";
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
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get country list based from CountryID
        /// </summary>
        /// <param name="CountryID"></param>
        /// <param name="CountryName"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<CountryDTO> GetCountry(string RegionID, string CountryName)
        {
            DataTable dt = null;
            try
            {
                dt = CountryBLL.CountryListByRegion(RegionID, CountryName);
                List<CountryDTO> list = new List<CountryDTO>();

                if (dt.Rows.Count > 0)
                {
                    CountryDTO CountryItem;
                    foreach (DataRow r in dt.Rows)
                    {
                        CountryItem = new CountryDTO();
                        CountryItem.CountryIDString = r["colCountryIDInt"].ToString();
                        CountryItem.CountryNameString = r["colCountryNameVarchar"].ToString();
                        list.Add(CountryItem);
                    }
                }
                return list;
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
        /// Date Created:   04/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get city list based from CountryID
        /// </summary>
        /// <param name="CountryID"></param>
        /// <param name="CityName"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<CityDTO> GetCity(string CountryID, string CityName)
        {
            DataTable dt = null;
            try
            {
                dt = CityBLL.GetCityByCountry(CountryID, CityName, "0");
                List<CityDTO> list = new List<CityDTO>();

                if (dt.Rows.Count > 0)
                {
                    CityDTO CityItem;
                    foreach (DataRow r in dt.Rows)
                    {
                        CityItem = new CityDTO();
                        CityItem.CityIDString = r["colCityIDInt"].ToString();
                        CityItem.CityNameString = r["colCityNameVarchar"].ToString().Replace("'", "");
                        list.Add(CityItem);
                    }
                }
                return list;
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
        /// (description)   Get airport list based from CountryID
        /// </summary>
        /// <param name="CountryID"></param>
        /// <param name="AirportName"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<AirportDTO> GetAirport(string CountryID, string RegionID, string AirportName)
        {
            DataTable dt = null;
            try
            {
                dt = AirBLL.GetAirportByCountry(CountryID, RegionID, AirportName, "0");
                List<AirportDTO> list = new List<AirportDTO>();

                if (dt.Rows.Count > 0)
                {
                    AirportDTO AirportItem;
                    foreach (DataRow r in dt.Rows)
                    {
                        AirportItem = new AirportDTO();
                        AirportItem.AirportIDString = r["colAirportIDInt"].ToString();
                        AirportItem.AirportNameString = r["colAirportNameVarchar"].ToString().Replace("'", "");
                        AirportItem.AirportCodeString = r["colAirportCodeVarchar"].ToString();
                        list.Add(AirportItem);
                    }
                }
                return list;
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
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get airport list
        /// </summary>
        [WebMethod]
        public static List<AirportDTO> GetAirportList(bool IsSearchByCode, string sFilter)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            list = MasterfileBLL.GetAirportList(IsSearchByCode, sFilter);
            return list;
        }
        /// <summary>
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get seaport list
        /// </summary>
        [WebMethod]
        public static List<SeaportDTO> GetSeaportList(bool IsSearchByCode, string sFilter)
        {
            List<SeaportDTO> list = new List<SeaportDTO>();
            list = MasterfileBLL.GetSeaportList(IsSearchByCode, sFilter);
            return list;
        }
        /// <summary>
        /// Date Created:   15/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get airport list of vehicle vendor
        /// </summary>
        [WebMethod]
        public static List<AirportDTO> GetVehicleVendorAirportList(Int32 iVehicleVendor, Int16 iLoadType)
        {
            List<AirportDTO> list = new List<AirportDTO>();
            list = VehicleBLL.GetVehicleVendorAirportList(iVehicleVendor, iLoadType);
            return list;
        }
        /// <summary>
        /// Date Created:   14/Nov/2013
        /// Created By:     Josephine Gad
        /// (description)   Get seaport list
        /// </summary>
        [WebMethod]
        public static List<SeaportDTO> GetVehicleVendorSeaportList(Int32 iVehicleVendor, Int16 iLoadType)
        {
            List<SeaportDTO> list = new List<SeaportDTO>();
            list = VehicleBLL.GetVehicleVendorSeaportList(iVehicleVendor, iLoadType);
            return list;
        }
        /// <summary>
        /// Date Created:   07/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get port list based from CityID     
        /// -----------------------------------------------
        /// Date Modified: 18/01/2012
        /// Modified By: Gelo Oquialda
        /// (description) Get port list based from RegionID and/or CountryID
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------        
        /// Date Modified:   06/07/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// </summary>
        /// <param name="RegionID"></param>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<PortDTO> GetPort(string RegionID, string CountryID)
        {

            try
            {
                List<PortList> listPort = new List<PortList>();
                List<PortDTO> list = new List<PortDTO>();
                //dt = PortBLL.GetPortListByCity(MUser.GetUserName(), CityID);
                listPort = PortBLL.GetPortListByRegionCountry(GlobalCode.Field2String(HttpContext.Current.Session["UserName"]), RegionID, CountryID, "");

                if (listPort.Count > 0)
                {
                    PortDTO PortItem;
                    for (int i = 0; i < listPort.Count; i++)
                    {
                        PortItem = new PortDTO();
                        PortItem.PortIDString = GlobalCode.Field2String(listPort[i].PortId);//["PORTID"].ToString();
                        PortItem.PortNameString = listPort[i].PortName;// r["PORT"].ToString();
                        list.Add(PortItem);
                    }
                    //foreach (DataRow r in dt.Rows)
                    //{
                    //    PortItem = new PortDTO();
                    //    PortItem.PortIDString = r["PORTID"].ToString();
                    //    PortItem.PortNameString = r["PORT"].ToString();
                    //    list.Add(PortItem);
                    //}
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   07/10/2011
        /// Created By:     Josephine Gad
        /// (description)   Get hotel list based from CityID
        /// ----------------------------------------------------------------
        /// Date Modified:  18/01/2012
        /// Modified By:    Gelo Oquialda
        /// (description)   Get hotel list based from RegionID and/or PortID and/or CountryID
        /// ----------------------------------------------------------------
        /// Date Modified:   15/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Remove Datatable
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        /// <param name="RegionID"></param>
        /// <param name="PortID"></param>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<HotelDTO> GetHotel(string RegionID, string PortID, string CountryID)
        {
            List<HotelDTO> list = null;
            try
            {
                list = HotelBLL.GetHotelBranchByRegionPortCountry(GlobalCode.Field2String(HttpContext.Current.Session["UserName"]),
                    RegionID, PortID, CountryID, "0");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (list != null)
                {
                    list = null;
                }
            }

            //DataTable dt = null;
            //try
            //{
            //    List<HotelDTO> list = new List<HotelDTO>();
            //    //dt = HotelBLL.GetHotelBranchByCity(MUser.GetUserName(), CityID);
            //    dt = HotelBLL.GetHotelBranchByRegionPortCountry(MUser.GetUserName(), RegionID, PortID, CountryID, "0");

            //    if (dt.Rows.Count > 0)
            //    {
            //        HotelDTO HotelItem;
            //        foreach (DataRow r in dt.Rows)
            //        {
            //            HotelItem = new HotelDTO();
            //            HotelItem.HotelIDString = r["BranchID"].ToString();
            //            HotelItem.HotelNameString = r["BranchName"].ToString();
            //            list.Add(HotelItem);
            //        }
            //    }
            //    return list;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //    }
            //}
        }
        /// <summary>
        /// Date Created:   29/11/2011
        /// Created By:     Josephine Gad
        /// (description)   Get Home Manifest Dashboard Count
        ///==================================================
        /// Date Created:   16/12/2011
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Home Manifest Dashboard Count
        ///==================================================        
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        ///==================================================
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<HomeDashboardDTO> GetHomeDashboard(string DateFrom, string DateTo, string pRole)
        {
            IDataReader dr = null;
            try
            {
                string sDateFrom = DateFrom.Replace("_", "/");
                //string sDateTo = DateTo.Replace("_", "/");
                DateTime vCurDate = DateTime.Parse(sDateFrom);

                List<HomeDashboardDTO> list = new List<HomeDashboardDTO>();

                //dr = dr = SeafarerTravelBLL.GetTravelManifestDashboard(sDateFrom, sDateTo, MUser.GetUserName(), "1",
                // sRegion, sCountry, sCity, 
                // "0", "1", "",
                // "0", sHotel, "0", "0", "0",
                // "0", "0", pRole);


                dr = SeafarerTravelBLL.GetTravelRequestDashboard(1, vCurDate, GlobalCode.Field2String(HttpContext.Current.Session["UserName"]),
                    GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]));

                while (dr.Read())
                {
                    HomeDashboardDTO DashBoardItem;
                    DashBoardItem = new HomeDashboardDTO();
                    DashBoardItem.Onsigning = dr["SIGNON"].ToString();
                    DashBoardItem.Offsigning = dr["SIGNOFF"].ToString();
                    list.Add(DashBoardItem);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
        }
        /// <summary>
        /// Date Created:   27/10/2011
        /// Created By:     Charlene Remotigue
        /// (description)   Tag Seafarer As Scanned
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        [WebMethod]
        public static String TagAsScanned(string e1Id, string tReqId, string mReqId,
            string UserId, string URole, string retValue)
        {
            DataTable dt = null;
            try
            {
                string strLogDescription;
                string strFunction;

                retValue = SeafarerTravelBLL.uspTagSeafarerAsScanned(Int32.Parse(retValue),
                    Int32.Parse(e1Id), Int32.Parse(mReqId), Int32.Parse(tReqId), UserId, URole).ToString();

                //Insert log audit trail (Gabriel Oquialda - 02/16/2012)
                strLogDescription = "Seafarer tagged as scanned.";
                strFunction = "TagAsScanned";

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();

                BLL.AuditTrailBLL.InsertLogAuditTrail(0, "", strLogDescription, strFunction, "PageMethods.aspx",
                                                      CommonFunctions.GetDateTimeGMT(currentDate), currentDate,
                                                      GlobalCode.Field2String(HttpContext.Current.Session["UserName"]));

                return retValue;
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
        /// Date Created:   09/12/2011
        /// Created By:     Josephine Gad
        /// (description)   Get seafarer details
        /// ---------------------------------------------
        /// Date Modified:   17/02/2012
        /// Modified By:     Josephine Gad
        /// (description)    Change DataTable to List
        /// ---------------------------------------------
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<SeafarerDTO> GetSeafarer(string E1ID)
        {
            return SeafarerBLL.GetSeafarerByFilter("2", E1ID);
        }

        [WebMethod]
        public static void DeletePortContractHotel()
        {
            try
            {
                //ContractBLL.DeleteContractPortAgentHotel("0", MUser.GetUserName(), "Hotel details deleted when contract creation is cancelled.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// Date Created: 12/22/2011
        /// Description: get voucher amount
        /// </summary>
        /// <param name="SeafarerId"></param>
        /// <param name="BranchId"></param>
        /// <param name="Days"></param>
        /// <returns></returns>
        [WebMethod]
        public static String GetVoucherAmount(string sStripe, string BranchId, string Days)
        {
            IDataReader dr = null;
            String amount = "0";
            try
            {
                decimal dStripe = GlobalCode.Field2Decimal(sStripe);
                dr = SeafarerBLL.VoucherGetDetails(dStripe, BranchId, Days);
                if (dr.Read())
                {
                    amount = Double.Parse(dr["colAmountMoney"].ToString()).ToString("#,##0.00");
                }
                return amount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author: Charlene Remotigue
        /// DateCreated: 28/12/2011
        /// Description: load hotels by user
        /// -------------------------------------------
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<HotelDTO> LoadHotelsByUser()
        {
            DataTable dt = null;
            try
            {
                List<HotelDTO> list = new List<HotelDTO>();
                dt = HotelManifestBLL.GetHotelBranchListByUser("", GlobalCode.Field2String(HttpContext.Current.Session["UserName"]),
                    GlobalCode.Field2String(HttpContext.Current.Session["UserRole"]), "");

                if (dt.Rows.Count > 0)
                {
                    HotelDTO HotelItem;
                    foreach (DataRow r in dt.Rows)
                    {
                        HotelItem = new HotelDTO();
                        HotelItem.HotelIDString = r["BranchID"].ToString();
                        HotelItem.HotelNameString = r["BranchName"].ToString();
                        list.Add(HotelItem);
                    }
                }
                return list;
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

        [WebMethod]
        public static int GetEventCount(int BranchId, DateTime Date)
        {
            int Count = 0;
            try
            {
                Count = HotelBookingsBLL.GetEventCount(Date, BranchId);
                return Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// DateCreated:    16/02/2012
        /// Description:    Get hotel details and room blocks for the specific room type
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="EffectiveDate"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<HotelRoomBlocksDTO> GetHotelRoomBlocks(string BranchID, string RoomTypeID, string EffectiveDate)
        {
            try
            {
                return HotelBLL.GetHotelRoomOverrideByBranch(BranchID, RoomTypeID, EffectiveDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// DateCreated:    16/02/2012
        /// Description:    Get hotel details and emergency room blocks for the specific room type
        /// </summary>
        /// <param name="BranchID"></param>
        /// <param name="RoomTypeID"></param>
        /// <param name="EffectiveDate"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<HotelRoomBlocksEmergencyDTO> GetHotelRoomBlocksEmergency(string BranchID, string RoomTypeID, string EffectiveDate)
        {
            try
            {
                return HotelBLL.GetHotelRoomEmergencyByBranch(BranchID, RoomTypeID, EffectiveDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>        
        /// Date Modified:  16/03/2012
        /// Modified By:    Josephine Gad
        /// (description)   Replace Global Variable with Session          
        /// -------------------------------------------
        /// </summary>
        [WebMethod]
        public static void ClearLogin()
        {
            try
            {
                MembershipUser mUser = Membership.GetUser(GlobalCode.Field2String(HttpContext.Current.Session["UserName"]));
                mUser.LastActivityDate = DateTime.Now.AddMinutes(-15);
                Membership.UpdateUser(mUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// DateCreated:    01/10/2012
        /// Description:    Add or remove record from Exception List
        /// ----------------------------------------------------------
        /// Modiofied By:   Josephine Gad
        /// Date Modiofied: 05/Mar/2013
        /// Description:    Add sComment Parameter
        /// </summary>
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        [WebMethod]
        public static string ExceptionAddRemoveFromList(string sExceptionID, string sIsRemove, string sComment)
        {
            try
            {
                Int64 ExceptionID = GlobalCode.Field2Int(sExceptionID);
                bool bIsRemove = GlobalCode.Field2Bool(sIsRemove);
                string sUser = GlobalCode.Field2String(HttpContext.Current.Session["UserName"]);
                string strFunction = "ExceptionAddRemoveFromList";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strLogDescription;

                if (bIsRemove == true)
                {
                    strLogDescription = "Remove record from Exception List";
                }
                else
                {
                    strLogDescription = "Add record to Exception List";
                }

                ExceptionBLL.ExceptionAddRemoveFromList(ExceptionID, bIsRemove, sUser, strLogDescription, strFunction,
                   "PageMethods.aspx", CommonFunctions.GetDateTimeGMT(currentDate), currentDate, sComment);

                return "Successfull!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// DateCreated:    06/Aug/2014
        /// Description:    Add or remove record from Exception List
        /// ----------------------------------------------------------        
        /// </summary>
        [WebMethod]
        public static string OverflowAddRemoveFromList(string sOverflowId, string sIsRemove, string sComment, string sPage)
        {
            DataTable dt = null;
            try
            {
                //Int64 ExceptionID = GlobalCode.Field2Int(sExceptionID);
                bool bIsRemove = GlobalCode.Field2Bool(sIsRemove);
                string sUser = GlobalCode.Field2String(HttpContext.Current.Session["UserName"]);
                string strFunction = "OverflowAddRemoveFromList";
                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                string strLogDescription;

                if (bIsRemove == true)
                {
                    strLogDescription = "Remove record from Overflow List";
                }
                else
                {
                    strLogDescription = "Add record to Overflow List";
                }


                dt = new DataTable();
                DataColumn col = new DataColumn("iOverflowID", typeof(Int64));
                dt.Columns.Add(col);

                DataRow row;
                string[] sOverflowArray = sOverflowId.Split(";".ToCharArray());

                for (int i = 0; i < sOverflowArray.Count(); i++)
                {
                    if (sOverflowArray[i] != "")
                    {
                        row = dt.NewRow();
                        row["iOverflowID"] = GlobalCode.Field2Int(sOverflowArray[i]);
                        dt.Rows.Add(row);
                    }
                }


                OverFlowBookingBLL.OverflowAddRemoveFromList(dt, bIsRemove, sUser, strLogDescription, strFunction,
                   sPage, CommonFunctions.GetDateTimeGMT(currentDate), currentDate, sComment);

                return "Successfull!";
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
        /// Author:         Muhallidin G Wali
        /// DateCreated:    13/Mar/2014
        /// Description:    Get Vehicle Route
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        [WebMethod]
        public static string GetVehicleRoute(string LoadType, string TravelReqID, string IDBigInt, string SeqNo
            , string Status, string PortCode, string RouteID)
        {
            try
            {
                CrewAssistBLL BLL = new CrewAssistBLL();
                return BLL.GetVehicleRoute(GlobalCode.Field2TinyInt(LoadType),
                        GlobalCode.Field2Long(TravelReqID), GlobalCode.Field2Long(IDBigInt)
                        , GlobalCode.Field2Int(SeqNo), Status, "", PortCode, GlobalCode.Field2Int(RouteID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:   26/Mar/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Remarks of transaction
        /// </summary>
        /// <param name="CountryID"></param>
        /// <param name="CountryName"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<TransactionRemarks> GetTransactionRemarks(string sTRid, string sRecloc, string sExpenseType)
        {
            try
            {
                PortAgentBLL BLL = new PortAgentBLL();
                return BLL.GetTransactionRemarks(GlobalCode.Field2Int(sTRid), sRecloc, GlobalCode.Field2TinyInt(sExpenseType));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Date Created:   11/Apr/2014
        /// Created By:     Josephine Gad
        /// (description)   Get Service Provider Vehicle Contract Amt
        /// </summary>
        [WebMethod]
        public static List<PortAgentVehicleContractAmt> GetPortAgentVehicleContractAmt(string sPortAgentID, string sVehicleTypeID)
        {
            try
            {
                PortAgentBLL BLL = new PortAgentBLL();
                return BLL.GetPortAgentVehicleContractAmt(GlobalCode.Field2Int(sPortAgentID), GlobalCode.Field2TinyInt(sVehicleTypeID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Descrption:     Save record to tblTempCrewAdminTransportationAddCancel to be able to tag ALL OFF to "Need Vehicle"
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        /// 
        [WebMethod]
        public static void TagCrewAsNeedVehicleAll(string UserID, bool IsNeedTransport)
        {
            try
            {
                CrewAdminBLL BLL = new CrewAdminBLL();
                BLL.TagCrewAsNeedVehicle(UserID, IsNeedTransport);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   24/Apr/2014
        /// Descrption:     Save record to tblTempCrewAdminTransportationAddCancel to be able to tag single OFF to "Need Vehicle"
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        /// 
        [WebMethod]
        public static void TagCrewAsNeedVehicleSingle(string UserID, bool IsNeedTransport, Int64 iTravelReqID, Int64 iIDBigint)
        {
            try
            {
                //bool IsNeedTransport = GlobalCode.Field2Bool(sIsNeedTransport);
                CrewAdminBLL BLL = new CrewAdminBLL();
                BLL.TagCrewAsNeedVehicleSingle(UserID, IsNeedTransport, iTravelReqID, iIDBigint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/May/2014
        /// Descrption:     Save record to tblTempCrewAdminPrintItinerary to be able to tag all crew to show itinerary in PDF
        /// <returns></returns>
        /// 
        [WebMethod]
        public static void TagCrewAsSelectedToPrintItinerary(string UserID, bool IsSelected)
        {
            try
            {
                CrewAdminBLL BLL = new CrewAdminBLL();
                BLL.TagCrewAsSelectedToPrintItinerary(UserID, IsSelected);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   29/May/2014
        /// Descrption:     Save record to tblTempCrewAdminPrintItinerary to be able to tag seafarer to show itinerary in PDF
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        /// 
        [WebMethod]
        public static void TagCrewAsSelectedToPrintItinerarySingle(string UserID, bool IsSelected, Int64 iTravelReqID, Int64 iIDBigint)
        {
            try
            {
                CrewAdminBLL BLL = new CrewAdminBLL();
                BLL.TagCrewAsSelectedToPrintItinerarySingle(UserID, IsSelected, iTravelReqID, iIDBigint);
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
        [WebMethod]
        public static string GetHotelTransactionOverFlow(long SeafarerID, DateTime CheckInDate, DateTime CheckOutDate, long IDBigint, short SeqNo, long TravelReqId, int BranchID, string PortCode)
        {
            try
            {
                CrewAssistBLL BLL = new CrewAssistBLL();
                return BLL.GetHotelTransactionOverFlow(SeafarerID, CheckInDate, CheckOutDate, IDBigint, SeqNo, TravelReqId, BranchID, PortCode, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// DateCreated:    25/Nov/2014
        /// Description:    Update OKTB of Nationality
        /// ----------------------------------------------------------
        /// <param name="sExceptionID"></param>
        /// <param name="bIsRemove"></param>
        [WebMethod]
        public static void UpdateNationalityOkTB(string UserId, int iNationalityID, bool IsOKTB, string strPageName)
        {
            try
            {
                DateTime CreatedDate = CommonFunctions.GetCurrentDateTime();

                MasterfileBLL BLL = new MasterfileBLL();
                string strLogDescription = "Updated Nationality - OK to Brazil";
                string strFunction = "UpdateNationalityOkTB";
                DateTime DateGMT = CommonFunctions.GetDateTimeGMT(CreatedDate);

                if (!IsOKTB)
                {
                    strLogDescription = "Updated Nationality - Not OK to Brazil";
                }

                BLL.UpdateNationalityOkTB(UserId, iNationalityID, IsOKTB,
                    strLogDescription, strFunction, strPageName, DateGMT, CreatedDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Date Created:   12/Dec/2015
        /// Created By:     Muhallidin G Wali
        /// (description)   Get Active User vendor
        /// --------------------------------------- 
        /// </summary>   
        [WebMethod]
        public static int GetActiveUserVendor(string userID)
        {
            int n = 0;
//            UserVendorBLL ss = new UserVendorBLL();
            n = UserVendorBLL.GetActiveUserVendor(userID);
            return n;
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save record to TblTempVehicleTransactionToConfirm to be able to tag seafarer to confirm
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static void SaveVehicleManifestToConfirm(string UserID, bool IsSelected, Int64 iTravelReqID, Int64 iIDBigint)
        {
            try
            {
                VehicleManifestBLL BLL = new VehicleManifestBLL();
                BLL.SaveVehicleManifestToConfirm(UserID, IsSelected, iTravelReqID, iIDBigint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   21/Jan/2015
        /// Descrption:     Save All record to TblTempVehicleTransactionToConfirm to be able to tag seafarer to confirm
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static void SaveVehicleManifestToConfirmAll(string UserID, bool IsSelected)
        {
            try
            {
                VehicleManifestBLL BLL = new VehicleManifestBLL();
                BLL.SaveVehicleManifestToConfirmAll(UserID, IsSelected);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   26/Jan/2015
        /// Descrption:     Get User List
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static List<UserList> GetUserList(string sUserID, bool bIsForCrewAssistRemarks)
        {
            try
            {
                List<UserList> list = new List<UserList>();
                list = ReportBLL.GetUserList(sUserID, bIsForCrewAssistRemarks);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   26/Jan/2015
        /// Descrption:     Get Employee List: 1=Name 2=ID
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static List<SeafarerListDTO> GetEmployeeList(string sFilterBy, string sFilterValue)
        {
            List<SeafarerListDTO> list = new List<SeafarerListDTO>();
            list = SeafarerBLL.GetSeafarerListByFilter(sFilterBy, sFilterValue);
            return list;
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   30/Jul/2015
        /// Descrption:     Get Employee List: 1=Name 2=ID
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static List<HotelWithAction> ValidateHotelForecast(string iBranchID, string sDate)
        {
            List<HotelWithAction> list = new List<HotelWithAction>();
           
            //HotelForecastBLL BLL = new HotelForecastBLL();
            list = HotelForecastBLL.ValidateHotelForecast(GlobalCode.Field2Int(iBranchID), sDate);
            return list;
        } 
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   29/sept/2015
        /// Descrption:     Get hotel list using the city of given hotel
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static List<HotelDTO> GetHotelBranchByCityOfHotel(string sBranchID)
        {
            List<HotelDTO> list = new List<HotelDTO>();

            Int64 iBranchID = GlobalCode.Field2Long(sBranchID);
            list = HotelBLL.GetHotelBranchByCityOfHotel(iBranchID);
            return list;
        }
        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   30/Mar/2016
        /// Descrption:     Get hotel start and end contract date
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static List<BranchList> GetHotelVendorDetails(Int32 iBranchID)
        {
            List<BranchList> list = new List<BranchList>();
            TimeSpan dayDiff;
            double iDayDiff = 0;


            ManifestBLL BLL = new ManifestBLL();
            list = BLL.GetHotelVendorDetails(iBranchID);

            if (list.Count() > 0)
            {
                DateTime dtContractDateEnd;
                dtContractDateEnd = GlobalCode.Field2DateTime(list[0].ContractDateEnd);

                DateTime dtNow = GlobalCode.Field2DateTime((DateTime.Now.ToString("mmm-dd-yyyyy")));
                dayDiff = dtContractDateEnd - dtNow;
                iDayDiff = dayDiff.TotalDays;
                list[0].NoOfDaysExpiry = iDayDiff;
            }
            return list;
        }

        [WebMethod]
        /// <summary>
        /// Date Created:   17/Oct/2017
        /// Created By:     Josephine Monteza
        /// (description)   Get Vendor list of user
        /// </summary>
        /// <param name="sLoginName"></param>
        /// <param name="sUsername"></param>
        /// <returns></returns>
        public static bool IsUserWithVendor(string sLogName, string sUsername, string sRoleToCheck)
        {
            bool bIsWithVendor = false;
            List<UserVendorList> list = new List<UserVendorList>();
            list = UserVendorBLL.UserVendorGet(sLogName, sUsername, sRoleToCheck);

            if (list.Count > 0)
            { 
                bIsWithVendor = true;
            }

            return bIsWithVendor;
        }

        
        /// <summary>
        /// Date Created:   04/Jan/2018
        /// Created By:     Muhallidin G Wali
        /// (description)  Get Vehicle Photo
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [WebMethod]
        public static VehicleImageFile GetPhoto(string URL)
        {

            VehicleImageFile img = new VehicleImageFile();
            try
            { 
                ////string url =  "http://10.80.0.49:7001/vendors/transportation/vehicle/" + ID + "?at=8c193d19e210e3f5705f95c21cd60f614db1bc26"  ;
                StringBuilder _sb = new StringBuilder();
                Byte[] _byte = GetImage(URL);
                _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
                img.Image = _sb.ToString();
            }
            catch (Exception exp)
            {
                img.Image = null;
            }
            return img;
        }


       
        /// <summary>
        /// Date Created:   04/Jan/2018
        /// Created By:     Muhallidin G Wali
        /// (description)  Get Vehicle Photo
        /// ------------------------------------------
        /// Date Modified:  05/Jan/2018
        /// Created By:     JMonteza
        /// (description)   Close stream and response in finally block
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static byte[] GetImage(string url)
        {
             Stream stream = null;
            byte[] buf;
            HttpWebResponse response = null;

             try
             {
                 WebProxy myProxy = new WebProxy();
                 HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                 response = (HttpWebResponse)req.GetResponse();
                 stream = response.GetResponseStream();

                 using (BinaryReader br = new BinaryReader(stream))
                 {
                     int len = (int)(response.ContentLength);
                     buf = br.ReadBytes(len);
                     br.Close();
                 }                
             }
             catch (Exception exp)
             {
                 buf = null;
                 throw exp;
             }
             finally
             {
                 stream.Close();
                 response.Close();
             }

             return (buf);
        }
 

        [WebMethod]
        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   27/Nov/2017
        /// Description:    Delete selected Hotel Exception
        /// </summary>
        /// <param name="Excption"></param>
        /// <returns></returns>
        public static short DeleteHotelException(string Excption, string UserID, string Comment)
        {

            HotelBookingsBLL hbll = new HotelBookingsBLL();
            return hbll.DeleteHotelException(Excption, UserID, Comment);
        }



        #region "Crew Assist"
        /// <summary>
        /// Author:         Muha Wali
        /// Date Created:   08/2016
        /// </summary>
        /// <param name="cHID"></param>
        /// <returns></returns>
        private static int GetNotConfirm(string cHID)
        {
            try
            {
                List<SeafarerDetailHeader> _SDetailList = new List<SeafarerDetailHeader>();
                BLL.CrewAssistBLL n = new BLL.CrewAssistBLL();
                HttpContext.Current.Session["SeafarerDetailList"] = null;
                _SDetailList = n.GetCancelCrewAssistHotelRequest(2, GlobalCode.Field2Long(cHID)
                        , ""
                        , ""
                        , ""
                        , ""
                        , ""
                        , DateTime.Now
                        , ""
                        , "");
                HttpContext.Current.Session["SeafarerDetailList"] = _SDetailList;
                return 3;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
       /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   21/Jan/2015
        /// Descrption:     Check checkin date to cancel request.
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static int CheckCancellationCheckinDate(string cTHrs, string cTZ, string cCOT, string cCUD, string cIsC, string cHID)
        {
            try
            {

                if (cIsC == "False")
                {
                   GetNotConfirm(cHID);
                   return 2;
                }
                DateTime TimeZoneDate = GlobalCode.Field2TimeZoneTime(DateTime.Now , cTZ);
                DateTime COT = GlobalCode.Field2DateTime(cCUD);
 

                // Difference in days, hours, and minutes.
                TimeSpan ts = GlobalCode.Field2DateTime(cCOT) - TimeZoneDate;
                // Difference in days.
                double differenceInDays = ts.TotalHours ;

                if (differenceInDays <= GlobalCode.Field2Double(cTHrs))
                    return 0;
                else
                    return 1;

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
        [WebMethod]
        public static ArrayList GetVendorContractAmount(short LT, int BID, int RID, DateTime cdate)
        {
            try
            {
                CrewAssistBLL  BLL = new CrewAssistBLL();
                return BLL.GetVendorContractAmount(LT, BID, RID, cdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region "IMS"

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------   
        /// Author:         Josephine Monteza
        /// Date Created:   08/Sep/2016
        /// Descrption:     Add column for Audit Trail
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static short ExcludeInvoiceDetail(int InvoiceDetailID, string sPath, string sUser)
        {
            IMSBLL IMS = new IMSBLL();
            try
            {
                string strLogDescription = "Exclude/Incldue invoice detail";
                string strFunction = "ExcludeInvoiceDetail";

                return IMS.GetExcludeInvoiceDetail(InvoiceDetailID, sUser, strLogDescription, strFunction, sPath);                                               
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static Invoices GetInvoices(string InvNumber, string InvDate)
        {
            IMSBLL IMS = new IMSBLL();
            Invoices invoice = new Invoices();
            try
            {
                invoice = IMS.GetSingleInvoicesToBill(InvNumber, InvDate);
                return invoice;
            }
            catch
            {
                return invoice;
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static Invoices GetBillSingleInvoices(string InvNumber, string InvDate)
        {
            IMSBLL IMS = new IMSBLL();
            Invoices invoice = new Invoices();
            try
            {
                return IMS.GetSingleInvoicesToBill(InvNumber, InvDate);
            }
            catch
            {
                return invoice;
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------
        /// Modified by:      Josephine Monteza
        /// Date Modified:    08/Sept/2016 
        /// Description:      Close DataTable for optimization
        /// =================================================================
        /// <returns></returns>
        [WebMethod]
        public static string ErrorInvoices(string Content, string InvoiceNumber, string userID)
        {

            DataTable exception = null;
            DataTable execeptionDetail = null;

            try
            {
                JavaScriptSerializer jc = new JavaScriptSerializer();
                exception = new DataTable();
                execeptionDetail = new DataTable();
                JsonInvoiceException JsonInvoiceException = new JsonInvoiceException();

                JsonInvoiceException = jc.Deserialize<JsonInvoiceException>(Content);

                if (JsonInvoiceException.InvoiceNumber == "" || JsonInvoiceException.InvoiceNumber == null)
                {
                    JsonInvoiceException.InvoiceNumber = InvoiceNumber;

                }

                Common.IMS ims = new Common.IMS();
                IMSBLL BLL = new IMSBLL();

                execeptionDetail = ims.getDataTable(JsonInvoiceException.Errors);
                exception = ims.ExceptionDataTable(JsonInvoiceException);

                BLL.ErrorInvoice(InvoiceNumber, exception, execeptionDetail, userID);
                return "Exception Inserted";

            }
            catch
            {
                return "Exception Not Inserted";
            }
            finally
            {
                if (exception != null)
                {
                    exception.Dispose();
                }
                if (execeptionDetail != null)
                {
                    execeptionDetail.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------      
        /// Modified By:    Josephine Monteza
        /// Date Modified:  08/Sep/2016
        /// Descrption:     Add column for Audit Trail
        /// -----------------------------------------------------------------    
        /// <returns></returns>
        [WebMethod]
        public static int SaveSummittedInvoice(string VendorNumber, string InvoiceNumber, string sPath, string sUser
             , string Desc, string Func, string FName, string TZone, string GDATE)
        {
            IMSBLL IMS = new IMSBLL();
            Invoices invoice = new Invoices();
            try
            {
                return IMS.GetSubmittedInvoice(0, GlobalCode.Field2Int(VendorNumber), InvoiceNumber, sPath, sUser, Desc , Func, FName , TZone, GlobalCode.Field2DateTime(GDATE));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static short UpdateInvoiceHotelPort(string InvoiceNumber, string PortNumber, string UserID)
        {
            IMSBLL IMS = new IMSBLL();
            try
            {
                return IMS.GetUpdateInvoiceHotelPort(InvoiceNumber, PortNumber, UserID);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Author:         Muhallidin G Wali
        /// Date Created:   30/Aug/2016
        /// Descrption:     Exclude Invoice
        /// -----------------------------------------------------------------            
        /// <returns></returns>
        [WebMethod]
        public static short GetUpdateInvoiceDetail(int UpdateID, string InvoiceDetailID, string UpdateValue, string UserID)
        {
            try
            {
                IMSBLL IMS = new IMSBLL();
                return IMS.GetUpdateInvoiceDetail(UpdateID, InvoiceDetailID, UpdateValue, UserID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author:         Josephine Monteza
        /// Date Created:   08/Sept/2016
        /// Descrption:     Log Audit Trail
        /// -----------------------------------------------------------------       
        /// </summary>
        [WebMethod]
        public static void LogAuditTrail(string sID, string strLogDescription, string strFunction, string sPath, string sUser)
        {

            DateTime currentDate = CommonFunctions.GetCurrentDateTime();
            Int32 iID = GlobalCode.Field2Int(sID);

            BLL.AuditTrailBLL.InsertLogAuditTrail(iID, "", strLogDescription, strFunction, sPath,
                                                  CommonFunctions.GetDateTimeGMT(currentDate), currentDate,
                                                  sUser);

        }
        #endregion
    }
}

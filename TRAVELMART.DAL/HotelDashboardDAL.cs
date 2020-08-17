using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using TRAVELMART.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace TRAVELMART.DAL
{
    public class HotelDashboardDAL
    {
        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/02/2012
        /// Descrption:     send hotel dashboard queries (list and count) to list
        /// </summary>
        /// <param name="iRegionID"></param>
        /// <param name="iCountryID"></param>
        /// <param name="iCityID"></param>
        /// <param name="sUserName"></param>
        /// <param name="sRole"></param>
        /// <param name="iBranchID"></param>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="sBranchName"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <returns></returns>
        public List<HotelDashboardDTOGenericClass> LoadAllHotelDashboardTables(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
            Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
            string sBranchName, int StartRow, int MaxRow)
        {
            List<HotelDashboardDTOGenericClass> HotelDashboardTables = new List<HotelDashboardDTOGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            //Int32 ExceptionCount = 0;
            Int32 OverflowCount = 0;
            //Int32 NoTravelRequestCount = 0;
            Int32 ArrDeptSameOnOffDateCount = 0;

            DataTable dt = null;
            DataTable dtOverflow = null;
            DataSet ds = null;
            DataTable dtExceptionNoTravelRequest = null;
            try
            {
                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardRoomTypeFromSummary");
                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, iRegionID);
                db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32, iCountryID);
                db.AddInParameter(dbCommand, "@pCityID", DbType.Int32, iCityID);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPortID);

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, iBranchID);

                db.AddInParameter(dbCommand, "@pFrom", DbType.DateTime, dFrom);
                db.AddInParameter(dbCommand, "@pTo", DbType.DateTime, dTo);

                db.AddInParameter(dbCommand, "@pBranchName", DbType.String, sBranchName);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType); 

                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                //if(iLoadType == 0)
                //{
                    //ExceptionCount =  Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
                    //OverflowCount =  Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
                    //NoTravelRequestCount = Int32.Parse(ds.Tables[5].Rows[0][0].ToString());
                  
                    //dtOverflow = ds.Tables[3];
                    //HotelDashboardClass.PendingBooking = (from c in dtOverflow.AsEnumerable()
                    //                                      select new OverflowBooking
                    //                                      {
                    //                                          CoupleId = c.Field<int?>("colCoupleIdInt"),
                    //                                          Gender = c.Field<string>("colGenderVarchar"),
                    //                                          Nationality = c.Field<string>("colNationalityVarchar"),
                    //                                          CostCenter = c.Field<string>("coLCostCenterVarchar"),
                    //                                          CheckInDate = GlobalCode.Field2DateTime(c["colCheckInDateTime"]),
                    //                                          CheckOutDate = GlobalCode.Field2DateTime(c["colCheckOutDatetime"]),

                    //                                          TravelReqId = GlobalCode.Field2Int(c["colTravelRequestIdInt"]),
                    //                                          SFStatus = c.Field<string>("colStatusVarchar"),
                    //                                          Name = c.Field<string>("colNameVarchar"),
                    //                                          SeafarerId = GlobalCode.Field2Int(c["colSeafarerIdInt"]),
                    //                                          VesselName = c.Field<string>("colVesselNameVarchar"),
                    //                                          RoomName = c.Field<string>("colRoomNameVarchar"),
                    //                                          RankName = c.Field<string>("colRankNameVarchar"),
                    //                                          CityId = GlobalCode.Field2Int(c["colCityIdInt"]),
                    //                                          CountryId = GlobalCode.Field2Int(c["colCountryIdInt"]),
                    //                                          HotelCity = c.Field<string>("colHotelCityVarchar"),
                    //                                          HotelNites = GlobalCode.Field2Int(c["colHotelNitesInt"]),
                    //                                          FromCity = c.Field<string>("colFromCityVarchar"),
                    //                                          ToCity = c.Field<string>("colToCityVarchar"),
                    //                                          RecordLocator = c.Field<string>("colRecordLocatorVarchar"),
                    //                                          Carrier = c.Field<string>("colCarrierVarchar"),
                    //                                          DepartureDate = GlobalCode.Field2DateTime(c["colDepartureDateTime"]),
                    //                                          ArrivalDate = GlobalCode.Field2DateTime(c["colArrivalDatetime"]),
                    //                                          FlightNo = c.Field<string>("colFlightNoVarchar"),
                    //                                          OnOffDate = GlobalCode.Field2DateTime(c["colOnOffDate"]),
                    //                                          Voucher = c["colVoucherMoney"].ToString(),
                    //                                          ReasonCode = c.Field<string>("colReasonCodeVarchar"),
                    //                                          Stripe = c.Field<decimal?>("colStripesDecimal"),
                    //                                          VendorId = GlobalCode.Field2Int(c["colVendorIdInt"]),
                    //                                          BranchId = GlobalCode.Field2Int(c["colBranchIdInt"]),
                    //                                          RoomTypeId = GlobalCode.Field2Int(c["colRoomTypeIdInt"]),
                    //                                          PortId = GlobalCode.Field2Int(c["colPortIdInt"]),
                    //                                          VesselId = GlobalCode.Field2Int(c["colVesselIdInt"]),
                    //                                          EnabledBit = GlobalCode.Field2Bool(c["isEnabled"]),
                    //                                      }).ToList();
                    //HotelDashboardClass.PendingBookingCount = OverflowCount;

                    //dtExceptionNoTravelRequest = ds.Tables[4];
                    //HotelDashboardDTO.HotelExceptionNoTravelRequestList = (from d in dtExceptionNoTravelRequest.AsEnumerable()
                    //                                                       select new HotelExceptionNoTravelRequestList
                    //                                                       {    
                    //                                                           colDate = GlobalCode.Field2DateTime(d["colDate"]),
                    //                                                           ExceptionCount = GlobalCode.Field2Int(d["ExceptionCount"]),
                    //                                                           NoTravelCount = GlobalCode.Field2Int(d["NoTravelCount"]),
                    //                                                           ArrDeptSameOnOffDateCount = GlobalCode.Field2Int(d["ArrDepSameDateCount"])
                    //                                                       }).ToList();
                //}

                HotelDashboardTables.Add(new HotelDashboardDTOGenericClass()
                {
                    HotelDashboardList = (from a in dt.AsEnumerable()
                                    select new HotelDashboardList
                                    {
                                        RowNo = GlobalCode.Field2Int(a["RowNo"]),

                                        BranchID = GlobalCode.Field2Int(a["BranchID"]),
                                        BrandID = GlobalCode.Field2Int(a["BrandID"]),

                                        RoomTypeID = GlobalCode.Field2TinyInt(a["RoomTypeID"]),
                                        HotelBranchName = a["HotelBranchName"].ToString(),

                                        colDate = GlobalCode.Field2DateTime(a["colDate"]),
                                        colDateName = a["colDateName"].ToString(),

                                        RoomType = a["RoomType"].ToString(),
                                        ReservedCrew = GlobalCode.Field2Int(a["ReservedCrew"]),
                                        OverflowCrew = GlobalCode.Field2Int(a["OverflowCrew"]),
                                        //TotalCrew = GlobalCode.Field2Int(a["TotalCrew"]),

                                        //ReservedRoom = GlobalCode.Field2Decimal(a["ReservedRoom"]),
                                        //TotalRoomBlocks = GlobalCode.Field2Int(a["TotalRoomBlocks"]),

                                        AvailableRoomBlocks = GlobalCode.Field2Decimal(a["AvailableRoomBlocks"]),                                        

                                        //EmergencyRoomBlocks = GlobalCode.Field2Int(a["EmergencyRoomBlocks"]),
                                        //AvailableEmergencyRoomBlocks = GlobalCode.Field2Decimal(a["AvailableEmergencyRoomBlocks"]),

                                        IsWithEvent = GlobalCode.Field2Bool(a["IsWithEvent"]),
                                        IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"])
                                        
                                    }).ToList(),
                    HotelDashboardListCount = maxRows,
                    //HotelExceptionNoTravelRequestList = (from d in dtExceptionNoTravelRequest.AsEnumerable()
                    //                                                       select new HotelExceptionNoTravelRequestList
                    //                                                       {    
                    //                                                           colDate = GlobalCode.Field2DateTime(d["colDate"]),
                    //                                                           ExceptionCount = GlobalCode.Field2Int(d["ExceptionCount"]),
                    //                                                           NoTravelCount = GlobalCode.Field2Int(d["NoTravelCount"]),
                    //                                                           ArrDeptSameOnOffDateCount = GlobalCode.Field2Int(d["ArrDepSameDateCount"])
                    //                                                       }).ToList(),
                    //HotelOverflowCount = OverflowCount,
                    //HotelExceptionCount = ExceptionCount,
                    //NoTravelRequestCount = NoTravelRequestCount
                });

                if (iLoadType == 0)
                {
                    dtExceptionNoTravelRequest = ds.Tables[2];

                    HotelDashboardTables.Add(new HotelDashboardDTOGenericClass()
                    {

                        HotelExceptionNoTravelRequestList = (from d in dtExceptionNoTravelRequest.AsEnumerable()
                                                             select new HotelExceptionNoTravelRequestList
                                                             {
                                                                 colDate = GlobalCode.Field2DateTime(d["colDate"]),
                                                                 ExceptionCount = GlobalCode.Field2Int(d["ExceptionCount"]),
                                                                 NoTravelCount = GlobalCode.Field2Int(d["NoTravelCount"]),
                                                                 ArrDeptSameOnOffDateCount = GlobalCode.Field2Int(d["ArrDepSameDateCount"])
                                                             }).ToList(),
                    });
                } 
                return HotelDashboardTables;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtOverflow != null)
                {
                    dtOverflow.Dispose();
                }
            }
        }

        /// <summary>
        /// Author:         Josephine Gad
        /// Date Created:   09/02/2012
        /// Descrption:     send hotel dashboard queries (list and count) to list
        /// -----------------------------------------------------------------------
        /// Modfied by:     Charlene Remotigue
        /// Date Modified:  07/03/2012
        /// Description:    added the ff parameters:
        ///                 SingleAvailableContractRooms
        ///                 DoubleAvailableContractRooms
        ///                 SingleAvailableOverrideRooms
        ///                 DoubleAvailableOverrideRooms
        ///                 isAccredited
        /// -------------------------------------------------------------------------------------
        /// Modfied by:     Gabriel Oquialda
        /// Date Modified:  13/03/2012
        /// Description:    This is a modified 'LoadAllHotelDashboardTables' copy for new screens 
        /// -------------------------------------------------------------------------------------
        /// Modfied by:     Josephine Gad
        /// Date Modified:  30/03/2012
        /// Description:    Add count for TR with Arrival/Departure same with On/Off Date
        /// -------------------------------------------------------------------------------------
        /// Modfied by:     Josephine Gad
        /// Date Modified:  22/05/2012
        /// Description:    Add Region List
        /// </summary>
        /// <param name="iRegionID"></param>
        /// <param name="iCountryID"></param>
        /// <param name="iCityID"></param>
        /// <param name="sUserName"></param>
        /// <param name="sRole"></param>
        /// <param name="iBranchID"></param>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="sBranchName"></param>
        /// <param name="StartRow"></param>
        /// <param name="MaxRow"></param>
        /// <returns></returns>
        public List<HotelDashboardDTOGenericClass> LoadAllHotelDashboardTables2(Int16 iLoadType, Int32 iRegionID, Int32 iCountryID,
            Int32 iCityID, Int32 iPortID, string sUserName, string sRole, Int32 iBranchID, DateTime dFrom, DateTime dTo,
            string sBranchName, int StartRow, int MaxRow)
        {
            List<HotelDashboardDTOGenericClass> HotelDashboardTables = new List<HotelDashboardDTOGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            Int32 maxRows = 0;
            Int32 ExceptionCount = 0;
            Int32 OverflowCount = 0;
            Int32 NoTravelRequestCount = 0;
            Int32 ArrDeptSameOnOffDateCount = 0;
            Int32 NoHotelContract = 0;
            Int32 RestrictedNationalityCount = 0;


            DataTable dtRegion = null;
            DataTable dt = null;
            DataSet ds = null;
            try
            {
               
                dbCommand = db.GetStoredProcCommand("uspGetHotelDashboardRoomTypeFromSummary_PROTOTYPE2");
                db.AddInParameter(dbCommand, "@pRegionID", DbType.Int32, iRegionID);
                db.AddInParameter(dbCommand, "@pCountryID", DbType.Int32, iCountryID);
                db.AddInParameter(dbCommand, "@pCityID", DbType.Int32, iCityID);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, iPortID);

                db.AddInParameter(dbCommand, "@pUserName", DbType.String, sUserName);
                db.AddInParameter(dbCommand, "@pRole", DbType.String, sRole);
                db.AddInParameter(dbCommand, "@pBranchID", DbType.Int32, iBranchID);

                db.AddInParameter(dbCommand, "@pFrom", DbType.DateTime, dFrom);
                db.AddInParameter(dbCommand, "@pTo", DbType.DateTime, dTo);

                db.AddInParameter(dbCommand, "@pBranchName", DbType.String, sBranchName);

                db.AddInParameter(dbCommand, "@pStartRow", DbType.Int32, StartRow);
                db.AddInParameter(dbCommand, "@pMaxRow", DbType.Int32, MaxRow);

                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, iLoadType);

                dbCommand.CommandTimeout = 0;
                ds = db.ExecuteDataSet(dbCommand);
                dt = ds.Tables[1];
                maxRows = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                if (iLoadType == 0 || iLoadType == 1)
                {
                    ExceptionCount = Int32.Parse(ds.Tables[2].Rows[0][0].ToString());
                    OverflowCount = Int32.Parse(ds.Tables[3].Rows[0][0].ToString());
                    NoTravelRequestCount = Int32.Parse(ds.Tables[4].Rows[0][0].ToString());
                    ArrDeptSameOnOffDateCount = Int32.Parse(ds.Tables[5].Rows[0][0].ToString());
                    RestrictedNationalityCount = Int32.Parse(ds.Tables[6].Rows[0][0].ToString());
                    NoHotelContract = Int32.Parse(ds.Tables[7].Rows[0][0].ToString());

                }

                HotelDashboardTables.Add(new HotelDashboardDTOGenericClass()
                {
                    HotelDashboardList = (from a in dt.AsEnumerable()
                                          select new HotelDashboardList
                                          {
                                              RowNo = GlobalCode.Field2Int(a["RowNo"]),

                                              CountryId = GlobalCode.Field2Int(a["colCountryIdInt"]),
                                              CityIdInt = GlobalCode.Field2Int(a["colCityIdInt"]),

                                              BranchID = GlobalCode.Field2Int(a["BranchID"]),
                                              BrandID = GlobalCode.Field2Int(a["BrandID"]),

                                              //RoomTypeID = GlobalCode.Field2TinyInt(a["RoomTypeID"]),
                                              HotelBranchName = a["HotelBranchName"].ToString(),

                                              colDate = GlobalCode.Field2DateTime(a["colDate"]),
                                              colDateName = a["colDateName"].ToString(),

                                              //RoomType = a["RoomType"].ToString(),
                                              //ReservedCrew = GlobalCode.Field2Int(a["ReservedCrew"]),
                                              //OverflowCrew = GlobalCode.Field2Int(a["OverflowCrew"]),
                                              //TotalCrew = GlobalCode.Field2Int(a["TotalCrew"]),

                                              ReservedRoom = GlobalCode.Field2Decimal(a["ReservedRoom"]),
                                              //TotalRoomBlocks = GlobalCode.Field2Int(a["TotalRoomBlocks"]),

                                              //AvailableRoomBlocks = GlobalCode.Field2Decimal(a["AvailableRoomBlocks"]),
                                              //EmergencyRoomBlocks = GlobalCode.Field2Int(a["EmergencyRoomBlocks"]),
                                              //AvailableEmergencyRoomBlocks = GlobalCode.Field2Decimal(a["AvailableEmergencyRoomBlocks"]),

                                              TotalSingleAvailableRoom = GlobalCode.Field2Decimal(a["TotalSingleAvailableRooms"]),
                                              TotalSingleRoomBlock = GlobalCode.Field2Decimal(a["TotalSingleBookings"]),
                                              TotalDoubleRoomBlock = GlobalCode.Field2Decimal(a["TotalDoubleBookings"]),
                                              TotalDoubleAvailableRoom = GlobalCode.Field2Decimal(a["TotalDoubleAvailableRooms"]),

                                              IsWithEvent = GlobalCode.Field2Bool(a["IsWithEvent"]),
                                              //IsAccredited = GlobalCode.Field2Bool(a["IsAccredited"]),
                                              IsWithContract = GlobalCode.Field2Bool(a["IsWithContract"]),
                                              //TotalSingleRoomBlock = GlobalCode.Field2Int(a["TotalSingleRoomBlock"]),
                                              //TotalDoubleRoomBlock = GlobalCode.Field2Int(a["TotalDoubleRoomBlock"]),
                                              //TotalSingleAvailableRoom = GlobalCode.Field2Int(a["TotalSingleAvailableRoom"]),
                                              //TotalDoubleAvailableRoom = GlobalCode.Field2Decimal(a["TotalDoubleAvailableRoom"]),
                                              //SingleAvailableContractRooms = GlobalCode.Field2Int(a["SingleAvailableContractRooms"]),
                                              //DoubleAvailableContractRooms = GlobalCode.Field2Decimal(a["DoubleAvailableContractRooms"]),
                                              //SingleAvailableOverrideRooms = GlobalCode.Field2Int(a["SingleAvailableOverrideRooms"]),
                                              //DoubleAvailableOverrideRooms = GlobalCode.Field2Decimal(a["DoubleAvailableOverrideRooms"]),
                                              ContractId = GlobalCode.Field2Int(a["ContractId"]),

                                          }).ToList(),
                    HotelDashboardListCount = maxRows,
                    HotelExceptionCount = ExceptionCount,
                    HotelOverflowCount = OverflowCount,
                    NoTravelRequestCount = NoTravelRequestCount,
                    ArrDeptSameOnOffDateCount = ArrDeptSameOnOffDateCount,
                    NoContractCount = NoHotelContract,
                    RestrictedNationalityCount = RestrictedNationalityCount,
                   
                });
                if (iLoadType == 0)
                {
                    dtRegion = ds.Tables[9];
                    HotelDashboardTables.Add(new HotelDashboardDTOGenericClass() {
                        RegionList = (from a in dtRegion.AsEnumerable()
                                      select new RegionList
                                      {
                                          RegionId = GlobalCode.Field2Int(a["colRegionIDInt"]),
                                          RegionName = a["colRegionNameVarchar"].ToString()
                                      }
                            ).ToList()
                    });

                    TMSettings.E1CHLastProcessedDate = GlobalCode.Field2DateTime(ds.Tables[10].Rows[0][0].ToString());
                }

                return HotelDashboardTables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (dtRegion != null)
                {
                    dtRegion.Dispose();
                }
            }
        }


        public List<HotelDashBoardPAGenericClass> GetNotTurnPort(short LoadType, int PortID, string UserID, DateTime Dates)
        {
            List<HotelDashboardDTOGenericClass> HotelDashboardTables = new List<HotelDashboardDTOGenericClass>();
            Database db= ConnectionSetting.GetConnection(); //  DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = null;

            DataSet ds = null;
            try {

                List<HotelDashBoardPAGenericClass> HotelDashBoard = new List<HotelDashBoardPAGenericClass>();

                dbCommand = db.GetStoredProcCommand("upGetNotTurnPort");
                db.AddInParameter(dbCommand, "@pLoadType", DbType.Int16, LoadType);
                db.AddInParameter(dbCommand, "@pPortID", DbType.Int32, PortID);
                db.AddInParameter(dbCommand, "@pUserID", DbType.String, UserID);
                db.AddInParameter(dbCommand, "@pDate", DbType.DateTime, Dates);

                ds = db.ExecuteDataSet(dbCommand);

                if (LoadType == 0)
                { 
                    HotelDashBoard.Add(new HotelDashBoardPAGenericClass
                    {
                        PortList = (from a in ds.Tables[0].AsEnumerable()
                                    select new PortList
                                    {
                                        PortId = GlobalCode.Field2Int(a["colPortIdInt"]),
                                        PortName = a["colPortNameVarchar"].ToString()
                                    }).ToList(),

                        HotelDashBoardPortAgentClass = (from a in ds.Tables[1].AsEnumerable()
                                                        select new HotelDashBoardPortAgentClass
                                                        {
                                                            PortAgentID = GlobalCode.Field2Int(a["PortAgentVendorID"]),
                                                            PortAgentName = a["PortAgentVendorName"].ToString(),

                                                            TotalDoubleRoomBlock = GlobalCode.Field2Int(a["TotalDoubleRoomBlock"]),
                                                            TotalSingleRoomBlock = GlobalCode.Field2Int(a["TotalSingleRoomBlock"]),
                                                            ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                                            DashBoardDate = GlobalCode.Field2DateTime(Dates),
                                                        }).ToList(),
                    });
                }
                else if (LoadType == 1)
                {

                    HotelDashBoard.Add(new HotelDashBoardPAGenericClass
                    {
                        HotelDashBoardPortAgentClass = (from a in ds.Tables[0].AsEnumerable()
                                                        select new HotelDashBoardPortAgentClass
                                                        {
                                                            PortAgentID = GlobalCode.Field2Int(a["PortAgentVendorID"]),
                                                            PortAgentName = a["PortAgentVendorName"].ToString(),
                                                            TotalDoubleRoomBlock = GlobalCode.Field2Int(a["TotalDoubleRoomBlock"]),
                                                            TotalSingleRoomBlock = GlobalCode.Field2Int(a["TotalSingleRoomBlock"]),
                                                            ContractID = GlobalCode.Field2Int(a["colContractIdInt"]),
                                                            DashBoardDate = GlobalCode.Field2DateTime(Dates),
                                                        }).ToList(),
                    });

                }

                return HotelDashBoard;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                }
                if (ds != null)
                {
                    ds.Dispose();
                }
                
            }

        }



    }
}

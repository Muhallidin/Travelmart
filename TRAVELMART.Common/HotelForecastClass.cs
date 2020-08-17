using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   29/Jan/2015
    /// Created By:     Josephine Monteza
    /// (description)   Hotel Forecast for Approval 
    /// </summary>
    /// 
    [Serializable]
    public class HotelForecastForApprovalList
    {
        public Int64 colBranchIDInt { get; set; }        
        public DateTime colDate { get; set; }
       
        //public int Confirmed_DBL { get; set; }
        //public int Overflow_DBL { get; set; }

        //public int Confirmed_SGL { get; set; }
        //public int Overflow_SGL { get; set; }

        public int Forecast_DBL { get; set; }
        public int Forecast_SGL { get; set; }

        public int Forecast_DBL_Adj { get; set; }
        public int Forecast_SGL_Adj { get; set; }

        public int RoomBlock_DBL { get; set; }
        public int RoomBlock_SGL { get; set; }

        public int RoomBlock_DBL_Total { get; set; }
        public int RoomBlock_SGL_Total { get; set; }

        public float TMBooked_DBL { get; set; }
        public float TMBooked_SGL { get; set; }

        public int ToBeAdded_DBL { get; set; }
        public int ToBeAdded_SGL { get; set; }

        public bool IsEnable { get; set; }

        public int Forecast_DBL_Old { get; set; }
        public int Forecast_SGL_Old { get; set; }

        public int ToBeAdded_DBL_Suggested { get; set; }
        public int ToBeAdded_SGL_Suggested { get; set; }

        public string Remarks { get; set; }
       
        public int ApprovedDBL { get; set; }
        public int ApprovedSGL { get; set; }
        public string ActionDone { get; set; }

        public bool IsLinkToRequestVisibleDBL { get; set; }
        public bool IsLinkToRequestVisibleSGL { get; set; }

        public bool IsNeededHotelVisibleDBL { get; set; }
        public bool IsNeededHotelVisibleSGL { get; set; }

        public int RoomToDropDBL { get; set; }
        public int RoomToDropSGL { get; set; }

        public string RoomToDropColorDBL { get; set; }
        public string RoomToDropColorSGL { get; set; }

        public float RatePerDayMoneySGL { get; set; }
        public float RatePerDayMoneyDBL { get; set; }
        public int CurrencyID { get; set; }
        public float RoomRateTaxPercentage { get; set; }
        public bool RoomRateIsTaxInclusive { get; set; }

        public bool IsRoomToDropVisibleToVendorBDL { get; set; }
        public bool IsRoomToDropVisibleToVendorSGL { get; set; }

        public bool IsRCCLApprovalVisible { get; set; }
        public string MessageToVendor { get; set; }

        public string CurrencyName { get; set; }
    }

   
    /// <summary>
    /// Date Created:   04/May/2015
    /// Created By:     Josephine Monteza
    /// (description)   Currency of hotel
    /// </summary>
    /// 
    [Serializable]
    public class HotelForecastCurrency
    {
        public int CurrencyID { get; set; }
        public string CurrencyName { get; set; }

        public decimal RateMoney { get; set; }
        public bool IsTaxInclusive { get; set; }
        public decimal Tax { get; set; }
        public Int16 RoomTypeID { get; set; }
    }
    /// <summary>
    /// Date Created:   30/Jul/2015
    /// Created By:     Josephine Monteza
    /// (description)   Date class
    /// </summary>
    [Serializable]
    public class HotelWithAction
    {
        public string colDate { get; set; }
    }
}

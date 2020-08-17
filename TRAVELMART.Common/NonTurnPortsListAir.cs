using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class NonTurnPortsListAir
    {
        
        public long IdBig {get;set;}
        public int  SeqNo {get;set;}
        public  Int16 AirMilesFlown {get;set;}
        public string ElapsedTime {get;set;}
        public string ResBookDesigCode {get;set;}
        public string FlightNo {get;set;}
        public DateTime ArrivalDateTime {get;set;}
        public DateTime DepartureDateTime {get;set;}
        public string DepartureAirportLocationCode {get;set;}
        public string DepartureAirportTerminal {get;set;}
        public string ArrivalAirportLocationCode {get;set;}
        public string ArrivalAirportTerminal {get;set;}
        public string MarketingAirlineCode {get;set;}
        public string SeatLocation {get;set;}
        public string SeatNo {get;set;}
        public string AirlineRef {get;set;}
        public string TicketNo {get;set;}
        public string AirStatus {get;set;}
        public string SFStatus {get;set;}
        public string Remarks {get;set;}
        public string ActionCode {get;set;}
        public string AirEquipType {get;set;}
        public Int16 OrderNo { get; set; }

    }
}

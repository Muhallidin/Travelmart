using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   08/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor Type
    /// </summary>
    [Serializable]
    public class VendorVehicleType
    {
        public int VehicleVendorID { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }        
    }
    /// <summary>
    /// Date Created:   08/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor Type
    /// </summary>
    [Serializable]
    public class VehicleType
    {
        public int VehicleTypeID { get; set; }
        public string VehicleTypeName { get; set; }
    }

    /// <summary>
    /// Date Created:   10/Aug/2013
    /// Created By:     Marco Abejar
    /// (description)  Plate Number
    /// ------------------------------------
    /// Date Modified:      07/Nov/2013
    /// Modified By:        Josephine Gad
    /// (description)       Add VehicleTypeID
    /// </summary>
    [Serializable]
    public class VehiclePlate : VehicleMaker
    {
        public int VehiclePlateID { get; set; }
        public string VehiclePlateName { get; set; }

        public int VehicleTypeID { get; set; }
        public string VehicleTypeName { get; set; }

        public string VehicleColor { get; set; }
        public string VehicleColorName { get; set; }
        public int VehicleDetailID { get; set; }



    }

    /// <summary>
    /// Date Created:   14/Aug/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Vehicle Vendor Type in Contract
    /// </summary>
    [Serializable]
    public class ContractVendorVehicleTypeCapacity
    {
        public int ContractVehicleCapacityIDInt { get; set; }
        public int ContractID { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public int? MinCapacity { get; set; }
        public int? MaxCapacity { get; set; }
    }
    /// <summary>
    /// Date Created:   30/Sep/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Route
    /// ---------------------------------
    /// Date Created:   3/Oct/2013
    /// Created By:     Marco Abejar
    /// (description)   Modify Route Variables
    /// </summary>
    [Serializable]
    public class VehicleRoute
    {
        public int RouteID { get; set; }
        public string RouteDesc { get; set; }
    }
    /// ---------------------------------
    /// Date Created:   08/Oct/2013
    /// Created By:     Josephine Gad
    /// (description)   Class Vehicle Vendor
    /// ---------------------------------
    [Serializable]
    public class VehicleVendorList
    {
        public int VehicleVendorID { get; set; }
        public string VehicleVendorName { get; set; }
    }


    /// <summary>
    /// Date Created:   12/Nov/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Service Provider Vendor Type
    /// </summary>
    [Serializable]
    public class PortAgentVehicleType
    {
        public int PortAgentVendorID { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
    }

    /// <summary>
    /// Date Created:   12/Nov/2013
    /// Created By:     Josephine Gad
    /// (description)   Class for Service Provider Vendor Type
    /// </summary>
    [Serializable]
    public class VehicleMaker : VehicleBrand
    {
        public int VehicleMakeID { get; set; }
        public string VehicleMakeName { get; set; }
        public string VehicleMakeBrandID { get; set; }
    }


    /// <summary>
    /// Date Created:   18/Oct/2017
    /// Created By:     Muhallidin G Wlai
    /// (description)   Class for Service Provider Vendor Type
    /// </summary>
    [Serializable]
    public class VehicleBrand
    {
        public int VehicleBrandID { get; set; }
        public string VehicleBrandName { get; set; }
    }


    
    /// <summary>
    /// Created By:     Muhallidin G Wali
    /// Date Created:   10/JAN/2014
    /// (description)   Get Air and hotel Detail for PDF print out
    /// </summary>
    [Serializable]
    public class FlightHotelDetailPDF
    {
        public List<AirDetailPDF> AirDetailPDF { get; set; }
        public List<HotelDetailPDF> HotelDetailPDF { get; set; }
        public string Destination { get; set; }
    
    }



    /// <summary>
    /// Created By:     Muhallidin G Wali
    /// Date Created:   10/JAN/2014
    /// (description)   Get Flight Detail for PDF print out
    /// </summary>
    [Serializable]
    public class AirDetailPDF
    { 
        public string TravelDate { get; set; }
        public string Carrier { get; set; }
        public string FlightNo { get; set; }


        public string DepartureCode { get; set; }
        public string ArrivalCode   { get; set; }

        public string DeparturePort { get; set; }
        public string ArrivalPort   { get; set; }


        public string FromTo { get; set; }

        public string DepartureDateTime { get; set; }
        public string ArrivalDateTime { get; set; }

        public string RecordLocator { get; set; }
        public string FlightStatus { get; set; }
        public string Status { get; set; }


        public string Seat { get; set; }
        public string MileFlown { get; set; }
        public string Class { get; set; }
        public string Meals { get; set; }
        public string Aircraft { get; set; }
        public string Duration { get; set; }

        public string PassengerName { get; set; }

    }


    /// <summary>
    /// Created By:     Muhallidin G Wali
    /// Date Created:   10/JAN/2014
    /// (description)   Get Flight Detail for PDF print out
    /// </summary>
    [Serializable]
    public class HotelDetailPDF
    {
        
        public string CheckInDate { get; set; }
        public string Chain { get; set; }
        public string Location { get; set; }
        public string Recordlocator { get; set; }
        public string RoomType { get; set; }
        public string NoOfDays { get; set; }
        public string Status { get; set; }
        public string ConfirmationNo { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
    }

    /// Created By:     Josephine Gad
    /// Date Created:   11/Feb/2014
    /// (description)   
    [Serializable]
    public class VehicleCountList
    {
        public int OnOffDate { get; set; }
        public string Status { get; set; }        
    }

    
    public class ColorCodes
    {
        public string ColorCode { get; set; }
        public string ColorName { get; set; } 
    }

    public class DriverGreeterVehHotelServProv
    {

        public List<DriverGreeter> Driver { get; set; }
        public List<DriverGreeter> Greeter { get; set; }
        public List<VehicleHotelServiceProvider> VehHotelSerProv { get; set; }
        public List<DriverTransaction> VehicleManifestList { get; set; } 




    }

    public class DriverGreeter
    {

        //private string  ID = "";
        //private string  fName = "";
        //private string  lName = "";
        //public void DriverDriver()

        public string ID { get; set; }
        public string FullName { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }

    }

    public class VehicleHotelServiceProvider
    {
        public int VehicleHotelServProvID { get; set;}
        public int VendorID { get; set;}
        public int VehicleDetailID { get; set;}
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }

        public string PlateNumber { get; set; }
        public string VehicleColor { get; set;}
        public string VehicleColorName { get; set;}
        public int VehicleBrandID { get; set;}
        public string VehicleBrand { get; set; }
        public int VehicleMakeId { get; set;}
        public string VehicleMakeName { get; set;}
        public string Capacity { get; set; }

    }


    public class DriverTransaction : VehicleManifestList
    { 
        private string _ConfirmTransID;
        private string _DriverUserID;
        private string _GreeterUserID;
        
		private int _VendorTypeID;
		private int _VendorID;
		private DateTime _PickupDate;
		private DateTime _PickupTime;
		private string _ParkingLocation;
		private float? _ParkingLatitude;
		private float? _ParkingLongitude;
		private string _PickupLocation;
		private float? _PickupLatitude;
		private float? _PickupLongitude;
		private string _DropOffLocation;
		private float? _DropOffLatitude;
		private float? _DropOffLongitude;
        private string _UserID;
		private int? _VehicleDetailID;
        private long? _DriverRequestID;

        private int? _RoutFrom;	
        private int? _RoutTo ;
        private string _From;
        private string _To;

        public DriverTransaction() { }
        public DriverTransaction(string confirmTransID,string driverUserID, int vendorTypeID, int vendorID, DateTime pickupdate, DateTime pickuptime,
            string parkinglocation, float parkingLatitude,  float parkingLongitude, string pickupLocation,  float pickupLatitude, float pickupLongitude, 
            string dropOffLocation, float dropOffLatitude, float dropOffLongitude, string userID, int vehicleDetailID,  long driverRequestID)
        { 
            this._ConfirmTransID = confirmTransID;
            this._DriverUserID = driverUserID;
            this._VendorTypeID = vendorTypeID;
            this._VendorID = vendorID;
            this._PickupDate = pickupdate;
            this._PickupTime = pickuptime;
            this._ParkingLocation = parkinglocation;
            this._ParkingLatitude = parkingLatitude;
            this._ParkingLongitude = parkingLongitude;
            this._PickupLocation = pickupLocation;
            this._PickupLatitude = pickupLatitude;
            this._PickupLongitude = pickupLongitude;
            this._DropOffLocation = dropOffLocation;
            this._DropOffLatitude = dropOffLatitude;
            this._DropOffLongitude = dropOffLongitude;
            this._UserID = userID;
            this._VehicleDetailID = vehicleDetailID;
            this._DriverRequestID = driverRequestID;
        
        }

        public string ConfirmTransID {
            get{
                return _ConfirmTransID;
            }
            set {
                _ConfirmTransID =  value;
            }
        
        }

        public string DriverUserID
        {
            get{
                return _DriverUserID;
            }
            set {
                _DriverUserID = value;
            }
        
        }
        public string GreeterUserID
        {
            get
            {
                return _GreeterUserID;
            }
            set
            {
                _GreeterUserID = value;
            }

        }
        public int VendorTypeID
        {
            get{
                return _VendorTypeID;
            }
            set {
                _VendorTypeID =  value;
            }
        
        }

        public int VendorID
        {
            get{
                return _VendorID;
            }
            set {
                _VendorID =  value;
            }
        
        }

        public DateTime PickupDate
        {
            get{
                return _PickupDate;
            }
            set {
                _PickupDate =  value;
            }
        }


        public DateTime PickupTime
        {
            get{
                return _PickupTime;
            }
            set {
                _PickupTime =  value;
            }
        }

        public string ParkingLocation
        {
            get{
                return _ParkingLocation;
            }
            set {

                

                _ParkingLocation =  value;
            }
        }

        public float? ParkingLatitude
        { 
            get{
                return _ParkingLatitude;
            }
            set {
                _ParkingLatitude = value;
            }
        }

        public float? ParkingLongitude
        { 
            get{
                return _ParkingLongitude;
            }
            set {
                _ParkingLongitude =  value;
            }
        }

        public string PickupLocation
        { 
            get{
                return _PickupLocation;
            }
            set {
                _PickupLocation =  value;
            }
        }

        public float? PickupLatitude
        { 
            get{
                return _PickupLatitude;
            }
            set {
                _PickupLatitude =  value;
            }
        }

        public float? PickupLongitude
        { 
            get{
                return _PickupLongitude;
            }
            set {
                _PickupLongitude =  value;
            }
        }

        public string DropOffLocation
        {
            get{
                return _DropOffLocation;
            }
            set{
                _DropOffLocation = value;
            }
        
        }

        public float? DropOffLatitude
        {
            get{
                return _ParkingLatitude;
            }
            set {
                _DropOffLatitude = value;
            }
        }

        public float? DropOffLongitude
        {
            get{
                return _DropOffLongitude;
            }
            set{
                _DropOffLongitude = value;
            }
        }

        public string UserID
        {
            get{
                return _UserID;
            }
            set{
                _UserID = value;
            }
        }

        public int? VehicleDetailID
        {
            get{
                return _VehicleDetailID;
            }
            set{
                _VehicleDetailID = value;
            }
            
        }

        public long? DriverRequestID
        {
            get {
                return _DriverRequestID;
            }
            set {
                _DriverRequestID = value;
            }
        }


        public int? RoutFrom {
            get {
                return _RoutFrom; 
            }
            set {
                _RoutFrom = value;
            }
        
        }

        public int? RoutTo
        {
            get {
                return _RoutTo;
            }
            set {
                _RoutTo = value;
            }
        }



        public string From {
            get {
                return _From;
            }
            set {
                _From = value;
            }
        
        }

        public string To {
            get {
                return _To;
            }
            set {
                _To = value;
            }
        }
    
    
    
    }

















}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class VendorMaintenanceVariables
    {
        private static string _vendorPrimaryIdString = string.Empty;
        private static string _vendorTypeString = string.Empty;

        public static string vendorPrimaryIdString
        {
            get
            {
                return _vendorPrimaryIdString;
            }
            set
            {
                _vendorPrimaryIdString = value;
            }
        }

        public static string vendorTypeString
        {
            get
            {
                return _vendorTypeString;
            }
            set
            {
                _vendorTypeString = value;
            }
        }
    }



    public class VehicleImageFile
    {

        public string Image { get; set; }

    }



}

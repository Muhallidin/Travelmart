using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TRAVELMART.Common
{
    public class ManifestSearchFilterVariables
    {
        private static string _strSeafarerID = string.Empty;
        private static string _strSeafarerLN = string.Empty;
        private static string _strSeafarerFN = string.Empty;
        private static string _strRecLoc = string.Empty;
        
        private static string _strVesselCode = string.Empty;
        private static string _strVesselName = string.Empty;

        public static string strSeafarerID
        {
            get
            {
                return _strSeafarerID;
            }
            set
            {
                _strSeafarerID = value;
            }
        }

        public static string strSeafarerLN
        {
            get
            {
                return _strSeafarerLN;
            }
            set
            {
                _strSeafarerLN = value;
            }
        }

        public static string strSeafarerFN
        {
            get
            {
                return _strSeafarerFN;
            }
            set
            {
                _strSeafarerFN = value;
            }
        }

        public static string strRecLoc
        {
            get
            {
                return _strRecLoc;
            }
            set
            {
                _strRecLoc = value;
            }
        }

        public static string strVesselCode
        {
            get
            {
                return _strVesselCode;
            }
            set
            {
                _strVesselCode = value;
            }
        }

        public static string strVesselName
        {
            get
            {
                return _strVesselName;
            }
            set
            {
                _strVesselName = value;
            }
        }
    }
}

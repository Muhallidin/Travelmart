using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    public class MasterfileVariables
    {
        private static string _RegionIdString = string.Empty;
        private static string _RegionNameString = string.Empty;
        private static string _CountryIdString = string.Empty;
        private static string _CountryNameString = string.Empty;
        private static string _CountryCode = string.Empty;
        private static string _CityIdString = string.Empty;
        private static string _CityNameString = string.Empty;
        private static string _CityCodeString = string.Empty;
        private static string _PortAgentIdString = string.Empty;
        private static string _PortAgentNameString = string.Empty;
        private static string _VesselIdString = string.Empty;
        private static string _VesselNameString = string.Empty;
        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Description: gets and sets region id
        /// </summary>
        public static string RegionIdString
        {
            get
            { 
                return _RegionIdString; 
            }
            set
            { 
                _RegionIdString = value; 
            }
        }
        /// <summary>
        /// Date Created: 30/09/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets region name
        /// </summary>
        public static string RegionNameString
        {
            get
            { 
                return _RegionNameString;
            }
            set
            { 
                _RegionNameString = value; 
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets country id
        /// </summary>
        public static string CountryIdString
        {
            get
            { 
                return _CountryIdString; 
            }
            set
            {
                _CountryIdString = value; 
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets country name
        /// </summary>
        public static string CountryNameString
        {
            get
            { 
                return _CountryNameString; 
            }
            set
            { 
                _CountryNameString = value; 
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets country code
        /// </summary>
        public static string CountryCodeString
        {
            get
            { 
                return _CountryCode;
            }
            set
            { 
                _CountryCode = value; 
            }
        }


        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets city id
        /// </summary>
        public static string CityIdString
        {
            get
            { 
                return _CityIdString; 
            }
            set
            { 
                _CityIdString = value;
            }
        }


        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets city name
        /// </summary>
        public static string CityNameString
        {
            get
            { 
                return _CityNameString;
            }
            set
            { 
                _CityNameString = value;
            }
        }

        /// <summary>
        /// Date Created: 03/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets city name
        /// </summary>
        public static string CityCodeString
        {
            get
            { 
                return _CityCodeString; 
            }
            set
            { 
                _CityCodeString = value; 
            }
        }

        /// <summary>
        /// Date Created: 04/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets Service Provider id
        /// </summary>

        public static string PortAgentIdString
        {
            get
            { 
                return _PortAgentIdString; 
            }
            set
            {
                _PortAgentIdString = value; 
            }
        }

        /// <summary>
        /// Date Created: 04/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets Service Provider name
        /// </summary>

        public static string PortAgentNameString
        {
            get
            { 
                return _PortAgentNameString; 
            }
            set
            { 
                _PortAgentNameString = value; 
            }
        }

        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets vessel id
        /// </summary>

        public static string VesselIdString
        {
            get
            { 
                return _VesselIdString; 
            }
            set
            { 
                _VesselIdString = value; 
            }
        }

        /// <summary>
        /// Date Created: 10/10/2011
        /// Created By: Charlene Remotigue
        /// Descrition: gets and sets vessel name
        /// </summary>

        public static string VesselNameString
        {
            get
            { 
                return _VesselNameString; 
            }
            set
            { 
                _VesselNameString = value; 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    [Serializable]
    public class Vessel
    {
        public int? VesselId { get; set; }
        public string VesselName { get; set; }
        public string VesselCode { get; set; }
    }
}

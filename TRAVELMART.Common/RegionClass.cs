using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRAVELMART.Common
{
    /// <summary>
    /// Date Created:   27/02/2012
    /// Created By:     Gabriel Oquialda
    /// (description)   Class for seaport
    /// -----------------------------------
    /// Date Modified:      14/08/2013
    /// Created By:         Josephine Gad
    /// (description)       Add ID field for other purpose like ContractSeaID in Vehicle Contract
    /// -----------------------------------
    /// </summary>
    /// 
    [Serializable]
    public class Seaport
    {
        public int? ID { get; set; }
        public int SeaportID { get; set; }
        public string SeaportName { get; set; }

        public static List<Seaport> SeaportList { get; set; }
    }

    ///<summary>
    ///Date Created: 07/08/2014
    ///Created By: Michael Brian C. Evangelista
    ///Description: class for Seaport Activation
    ///</summary>
    ///
    [Serializable]
    public class SeaportActivation
    {
        public int? ID { get; set; }
        public int SeaportID { get; set; }
        public string SeaportName { get; set; }
        public string SeaportCode { get; set; }
        public bool SeaportActivated { get; set; }

        public static List<SeaportActivation> SeaportList { get; set; }    
    }

    /// <summary>
    /// Date Created: 27/02/2012
    /// Created By:   Gabriel Oquialda
    /// (description) Class for region seaport
    /// ------------------------------------------
    /// Date Modified:  07/05/2012
    /// Modified By:    Josephine Gad
    /// (description)   Add CountryID
    /// </summary>
    /// 
    [Serializable]
    public class RegionSeaport
    {
        public Int64 RegionSeaportID { get; set; }
        public int RegionID { get; set; }
        public int SeaportID { get; set; }
        public string SeaportName { get; set; }
        public int CountryID { get; set; }

        public static List<RegionSeaport> RegionSeaportList { get; set; }
    }
    /// <summary>
    /// Date Created:   04/04/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for seaport not exist in region
    /// </summary>    
    [Serializable]
    public class RegionSeaportNotExists
    {
        public Int64 RegionSeaportID { get; set; }
        public int RegionID { get; set; }
        public int SeaportID { get; set; }        
        public string SeaportName { get; set; }
        public int CountryID { get; set; }
                
        public static List<RegionSeaportNotExists> RegionSeaportToAdd { get; set; }
    }

    /// <summary>
    /// Date Created:   04/04/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Continent
    /// </summary>
    /// 
    [Serializable]
    public class Continent
    {
        public int ContinentID { get; set; }
        public string ContinentName { get; set; }

        public static List<Continent> ContinentList { get; set; }
    }

    public class RegionGenericClass
    {
        public List<Continent> ContinentList { get; set; }
        public List<RegionSeaport> RegionSeaportList { get; set; }
    }

    public class RegionClass
    {
        public static List<Continent> ContinentList { get; set; }
        public static List<RegionSeaport> RegionSeaportList { get; set; }
    }
}


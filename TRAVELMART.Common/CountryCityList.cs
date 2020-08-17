using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TRAVELMART.Common
{

    /// <summary>
    /// Date Created:   04/05/2012
    /// Created By:     Josephine Gad
    /// (description)   Class for Country
    /// </summary>
    [Serializable]
    public class Country
    {

        public int? CountryID { get; set; }
        public string CountryName { get; set; }

        public List<CityList> City { get; set; }

        //public static List<Country> CountryList { get; set; }
    }
    /// <summary>
    /// Date Created:   19/10/2012
    /// Created By:     Josephine Gad
    /// (description)   set the generic class of country
    /// </summary>
    public class CountryGenericClass
    {
        public static List<Country> CountryList { get; set; }
    }
    [Serializable]
    public class Nationality
    {
        public int? RestrictedID { get; set; }
        public int? NationalityID { get; set; }
        public string NationalityCode { get; set; }
        public string NationalityName { get; set; }
    }
    public class NationalityGenericClass
    {
        public List<Nationality> NonRestNationalityList { get; set; }
        
        public List<Nationality> RestNationalityList { get; set; }
        public int RestNationalityCount { get; set; }
    }
}

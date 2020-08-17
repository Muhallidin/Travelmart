using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Web.Services;
using TRAVELMART.Common;
using TRAVELMART.BLL;


namespace TRAVELMART.WebService
{
    public partial class EPortalLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Date Created:   10/Nov/2014
        /// Created By:     Josephine Monteza
        /// (description)   Check if user is authenticated in TM
        /// --------------------------------------- 
        /// </summary>       
        [WebMethod]
        public static bool IsAuthenticated(string sUserIdentifier, string sAppID)
        {
            return EPortalBLL.IsUserExists(sUserIdentifier, sAppID);
        }

        //public class Service : IHttpHandler
        //{ 
        
        //}
    }
}

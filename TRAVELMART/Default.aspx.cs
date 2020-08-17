using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using TRAVELMART.Common;
namespace TRAVELMART
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = ConfigurationSettings.AppSettings["UnderMaintenance"].ToString();
            if (GlobalCode.Field2String(Request.QueryString["IsTest"]) == "1")
            {
                Response.Redirect("Login.aspx");
            }
          
            if (str == "0")
            {
                Response.Redirect("Login.aspx");
            }
            else
            { 
                Response.Redirect("UnderMaintenance.html");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TRAVELMART.Common;
using TRAVELMART.BLL;

namespace TRAVELMART
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {            
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Date Modified:   15/11/2011
        /// Modified By:     Josehine Gad
        /// (description)    Capture error and insert in Exception Table
        /// ===================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {            
            //Server.Transfer("/Message.aspx");
            //Response.Redirect("~/Message.aspx");

            if (Server.GetLastError() != null)
            { 
                Exception objErr = Server.GetLastError().GetBaseException();

                DateTime currentDate = CommonFunctions.GetCurrentDateTime();
                ErrorBLL.InsertError(objErr.Message, objErr.StackTrace.ToString(), Request.Url.AbsolutePath,
                    currentDate, CommonFunctions.GetDateTimeGMT(currentDate), User.Identity.Name);

                Exception objError = Server.GetLastError().GetBaseException();
                HttpContext.Current.Application.Remove("lastException");
                HttpContext.Current.Application.Add("lastException", objError);
                Response.Redirect("~/Message.aspx?aspxerrorpath=" + Request.Url.AbsolutePath);
            
            }
        
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Response.Redirect("~/Message.aspx");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
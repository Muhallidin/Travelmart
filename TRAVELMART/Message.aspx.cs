using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class Message : System.Web.UI.Page
    {
        /// <summary>
        /// Date Modified:   15/11/2011
        /// Modified By:     Josehine Gad
        /// (description)    Set the error message
        /// ===================================
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ////// Get the exception object.
            ////Exception exc = Server.GetLastError().GetBaseException();
            ////errorMessage.Text = exc.Message;
            //// ========================================================        
            //// Date Modified:  15/03/2012
            //// Modified By:    Muhallidin G Wali
            //// (description)   Replace Global Variable with Session          
            //// ========================================================        

            if (!IsPostBack)
            {
                if (Session["strPrevPage"] != null && GlobalCode.Field2String(Session["strPrevPage"]).Trim() != "")
                {
                    uoHyperLinkBack.NavigateUrl = GlobalCode.Field2String(Session["strPrevPage"]);
                    uoRowPreviosPage.Visible = true;
                }
                else
                {
                    uoRowPreviosPage.Visible = false;
                }

                Exception objErr = (Exception)HttpContext.Current.Application.Get("lastException");

                errorMessage.Text = objErr.Message;
                uoLabelErrorIn.Text = Request.QueryString["aspxerrorpath"];
                uoLabelStackTrace.Text = objErr.StackTrace;

                Server.ClearError();
            }
        }
    }
}

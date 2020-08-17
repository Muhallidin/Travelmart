using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;     
using TRAVELMART.Common;
using System.Collections.Generic;
using TRAVELMART.BLL;
using System.Collections.Specialized;
using System.Text;

namespace TRAVELMART
{
    public partial class LoginRemoteWeb : System.Web.UI.Page
    {
        NameValueCollection coll;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Request.Form to be sent by values from android

            int loop1;

            coll = Request.Form;

            String[] arr1 = coll.AllKeys;
            //myDiv.InnerHtml = Request.QueryString["username"] + Request.QueryString["password"];
            String sUser = Request.QueryString["username"];
            String sPass = Request.QueryString["password"];

            //Call the Membership Provider and use the data to check whether username and password is correct

            bool b = Membership.ValidateUser(sUser, sPass);
            //myDiv.InnerHtml = b.ToString();
            // if username and password is correct, return the following data:

            if (b == true)
            {
                List<UserAccountList> userAccount = UserAccountBLL.GetUserInfoListByName(sUser);
                List<UserPrimaryDetails> userDetails = (List<UserPrimaryDetails>)Session["UserPrimaryDetails"];

               
                var vRole = (from a in userAccount
                             where a.bIsPrimary == true
                             select new
                             {
                                 sRole = a.sRole
                             }).ToList();

                String userRole = vRole[0].sRole;

                String[] userId = userDetails.Select(y => y.sFirstName).ToArray();
                StringBuilder builder = new StringBuilder();
				
				    builder.Append(userId[0]).Append("|");

                    builder.Append(userRole).Append("|");
                

                string result = builder.ToString();
                Response.Write(result);
                //myDiv.InnerHtml = result;
            }
            else
            {
                Response.Write("error");
            }
            //return data or return error
		
        }
    }
}

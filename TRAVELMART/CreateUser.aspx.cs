using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using TRAVELMART.BLL;


namespace TRAVELMART
{
    public partial class CreateUser : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            uotextboxLName.Focus();
        }
        protected void uobuttonSave_Click(object sender, EventArgs e)
        {
            string strLName = uotextboxLName.Text.Trim();
            string strFName = uotextboxFName.Text.Trim();
            string strEmail = uotextboxEmail.Text.Trim();
            string strUsername = uotextboxUName.Text.Trim();
            string strPWD = uotextboxPWD.Text.Trim();
            string strPWD1 = uotextboxPWD1.Text.Trim();
            string strRoleID = uodropdownlistRole.SelectedValue;
            string strRoleName = uodropdownlistRole.SelectedItem.Text;

            if (strPWD.Equals(strPWD1))
            {
                //string strMessage = UserAccountBLL.CreateUserAccount(strLName, strFName, strEmail, strUsername, strPWD, strRoleID, strRoleName, "");
                string strMessage = "";
                AlertMessage(strMessage);
                if (strMessage.Equals("New user has been created successfully."))
                    Response.Redirect("~/Login.aspx");
            }
            else
            {
                AlertMessage("Unidentical password");
            }
        }
        #endregion

        #region Functions
        private void AlertMessage(string s)
        {
            /// <summary>
            /// Date Created: 08/07/2011
            /// Created By: Marco Abejar
            /// (description) Show pop up message            
            /// </summary>

            string sScript = "<script language='JavaScript'>";
            sScript += "alert('" + s + "');";
            sScript += "</script>";
            Page.ClientScript.RegisterClientScriptBlock(typeof(string), "script", sScript);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;

namespace TRAVELMART
{
    public partial class TravelmartMenuLinks : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindMenuLink();
        }
        private void BindMenuLink()
        {
            string MenuString = "";

            if (Cache["UserMenuLeft"] == null)
            {
                MenuString = "<div id=\"vertmenu\"> ";
                MenuString += " <ul> ";

                string userName = GlobalCode.Field2String(Session["UserName"]);
                string userRole = UserAccountBLL.GetUserPrimaryRole(userName);

                bool IsPendingExists = false;
                IsPendingExists = RequestBLL.IsPendingRequestExists(userName, userRole);
                if (IsPendingExists)
                {
                    MenuString += " <li><a href=\"../RequestView.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Pending Request</a></li> ";
                }
                if (userRole == TravelMartVariable.RoleAdministrator || Page.User.IsInRole(TravelMartVariable.RoleHotelSpecialist))
                {
                    MenuString += " <li><a href=\"../Hotel/HotelViewPending.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Pending Hotel Booking</a></li> ";
                }
                if (userRole == TravelMartVariable.RoleAdministrator || Page.User.IsInRole(TravelMartVariable.RoleHotelSpecialist)
                    || Page.User.IsInRole(TravelMartVariable.RoleVehicleSpecialist))
                {
                    MenuString += " <li><a href=\"../Vehicle/VehicleViewPending.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Pending Vehicle Booking</a></li> ";
                }

                if (userRole == TravelMartVariable.RoleAdministrator || userRole == TravelMartVariable.RoleContractManager)
                {
                    MenuString += " <li><a href=\"../Maintenance/HotelContractApproval.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Pending Hotel Contract</a></li> ";
                    MenuString += " <li><a href=\"../ContractManagement/HotelNoActiveContractList.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Hotel No Active Contract</a></li> ";

                    MenuString += " <li><a href=\"../Maintenance/VehicleContractApproval.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Pending Vehicle Contract</a></li> ";
                    MenuString += " <li><a href=\"../ContractManagement/VehicleNoActiveContractList.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Vehicle No Active Contract</a></li> ";

                    MenuString += " <li><a href=\"../ContractManagement/PortAgentContractApproval.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Pending Service Provider Contract</a></li> ";
                    MenuString += " <li><a href=\"../ContractManagement/PortAgentNoActiveContract.aspx?ufn=" + Request.QueryString["ufn"].ToString() + "\">Service Provider No Active Contract</a></li> ";
                    MenuString += " </ul> ";
                    MenuString += " </div> ";
                }
                //Store menu string in Cache for 2 minutes
                Cache.Insert("UserMenuLeft", MenuString, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(120));
            }
            MenuString = Cache["UserMenuLeft"].ToString();
            ucLiteralMenu.Text = MenuString;
        }
    }
}
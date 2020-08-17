using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TRAVELMART.BLL;
using TRAVELMART.Common;
using System.Web.Security;
using System.Data;

using System.IO;
using System.Web.UI.HtmlControls;

namespace TRAVELMART
{
    
    public partial class TravelmartMenu : System.Web.UI.UserControl
    {
        #region "Events"    
        protected void Page_Load(object sender, EventArgs e)
        {
            BindMenu();            
        }
        #endregion
        
        #region "Procedures"       
        /// <summary>
        /// Date Created:    27/10/2011
        /// Created By:      Josephine Gad
        /// (description)    Get users menu by UserName
        /// ----------------------------------------------------------------
        /// Date Modified:   16/08/2012
        /// Modified By:     Josephine Gad
        /// (description)    Add each role's home page
        /// ----------------------------------------------------------------
        /// Date Modified:   16/08/2012
        /// Modified By:     Jefferson Bermundo
        /// (description)    Add Homepage for HotelVendor
        /// ----------------------------------------------------------------
        /// Date Modified:   25/Apr/2013
        /// Modified By:     Josephine Gad
        /// (description)    Change Hotel Vendor's Page to HotelConfirmManifest.aspx Page
        /// ----------------------------------------------------------------
        /// Date Modified:  7/May/2013
        /// Modified By:    Marco Abejar
        /// (description)   Change Crew Assist Page to HotelDashboardRoomType5.aspx Page
        /// /// ----------------------------------------------------------------
        /// </summary>
        /// 
        private void BindMenuByUser()
        {            
            string URLString = "";            
            string MenuString = "";            
            string UserName = GlobalCode.Field2String(Session["UserName"]); //MUser.GetUserName();
            string UserRole = GlobalCode.Field2String(Session["UserRole"]);;//MUser.GetUserRole();
            
            if (Session["UserMenu"] == null)
            {
                if (Request.QueryString["ufn"] != null)
                {
                    //GlobalCode.Field2String(Session["DateFrom"]) = Request.QueryString["dt"];
                    Session["DateFrom"] = Request.QueryString["dt"];
                    
                    //URLString = "?ufn=" + Request.QueryString["ufn"];    
                    //URLString = "?ufn" + GlobalCode.Field2String(Session["strPrevPage"]);
                    //URLString = "?ufn=" + Request.QueryString["ufn"] + "&dt=" + GlobalCode.Field2DateTime(GlobalCode.Field2String(Session["DateFrom"])).ToString(); //gelo

                    URLString = "?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]; //gelo
                }
                MenuString = "<ul id =\"nav\">";
                
                //if (UserRole != TravelMartVariable.RoleHotelSpecialist && UserRole != TravelMartVariable.RoleHotelVendor && UserRole != "")
                //{
                //    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Manifest.aspx" + URLString + "\">Home</a></li>";
                //}
                if (UserRole == TravelMartVariable.RoleHotelSpecialist)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../HotelDashboardRoomType5.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleMeetGreet || UserRole == TravelMartVariable.RolePortSpecialist)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../MeetAndGreet/MeetAndGreet.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleCrewAdmin)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../CrewAdmin/CrewAdmin.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleHotelVendor)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Hotel/HotelConfirmManifest.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleSystemAnalyst)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../SystemAnalyst/ExceptionPNR.aspx" + URLString + "\">Home</a></li>";
                }
                else
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Manifest.aspx" + URLString + "\">Home</a></li>";
                }

                DataTable MenuDataTable = null;
                DataTable SubMenuDataTable = null;
                DataTable SubMenuDataTable2 = null;
                string PageUrlString = "";
                string SubMenuString = "";
                string SubMenuString2 = "";
                try
                {                   
                    MenuDataTable = UserRightsBLL.GetMenuByUser(UserName);
                    if (MenuDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow MenuRow in MenuDataTable.Rows)
                        {
                            PageUrlString = MenuRow["colPageNameVarchar"].ToString();
                            PageUrlString = (PageUrlString == "" ? "#" : "../" + PageUrlString + URLString);

                            MenuString += "<li><a href=\"" + PageUrlString + "\">" + MenuRow["colDisplayNameVarchar"].ToString() + "</a>";
                            SubMenuDataTable = UserRightsBLL.GetSubMenuByUser(UserName, MenuRow["colPageIDInt"].ToString());
                            if (SubMenuDataTable.Rows.Count > 0)
                            {
                                SubMenuString = "<ul>";
                                foreach (DataRow SubMenuRow in SubMenuDataTable.Rows)
                                {
                                    PageUrlString = SubMenuRow["colPageNameVarchar"].ToString();
                                    PageUrlString = (PageUrlString == "" ? "#" : "../" + PageUrlString + URLString);

                                    SubMenuString += "<li><a href=\"" + PageUrlString + "\">" + SubMenuRow["colDisplayNameVarchar"].ToString() + "</a>";
                                    SubMenuDataTable2 = UserRightsBLL.GetSubMenuByUser(UserName, SubMenuRow["colPageIDInt"].ToString());
                                    if (SubMenuDataTable2.Rows.Count > 0)
                                    {
                                        SubMenuString2 = "<ul>";
                                        foreach (DataRow SubMenuRow2 in SubMenuDataTable2.Rows)
                                        {
                                            PageUrlString = SubMenuRow2["colPageNameVarchar"].ToString();
                                            PageUrlString = (PageUrlString == "" ? "#" : "../" + PageUrlString + URLString);
                                            SubMenuString2 += "<li><a href=\"" + PageUrlString + "\">" + SubMenuRow2["colDisplayNameVarchar"].ToString() + "</a></li>";
                                        }
                                        SubMenuString2 += "</ul>";
                                        SubMenuString += SubMenuString2;
                                    }
                                    SubMenuString += "</li>";

                                }
                                SubMenuString += "</ul>";
                                MenuString += SubMenuString;
                            }
                            MenuString += "</li>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (MenuDataTable != null)
                    {
                        MenuDataTable.Dispose();
                    }
                    if (SubMenuDataTable != null)
                    {
                        SubMenuDataTable.Dispose();
                    }
                    if (SubMenuDataTable2 != null)
                    {
                        SubMenuDataTable2.Dispose();
                    }
                }

                if (UserRole != TravelMartVariable.RoleImmigration)
                { 
                    MenuString += "<li><a runat=\"server\" id=\"uoASearch\" href=\"../ManifestSearchFilter.aspx" + URLString + "\">Search</a></li>";
                }
               
                MenuString += "</ul>";

                Session["UserMenu"] = MenuString;
                //Store menu string in Cache for 10 minutes
                //Cache.Insert("UserMenu", MenuString, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
            }
            if (Session["CurrentDate"] == null)
            {
                Session["CurrentDate"] = Request.QueryString["dt"].ToString();
                //Store menu string in Cache for 10 minutes
                //Cache.Insert("CurrentDate", Request.QueryString["dt"].ToString(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
            }
            if (Session["UserMenu"] != null && Session["CurrentDate"] != null)
            {
                string sMenu = Session["UserMenu"].ToString();
                string sDate = Session["CurrentDate"].ToString();
                string sDateNew = Request.QueryString["dt"].ToString();
                //sDate = sDate.Replace("_", "/");
                if (!sDateNew.Contains("-"))
                {
                    sDateNew = sDateNew.Replace("_", "/");
                    sMenu = sMenu.Replace(sDate, sDateNew);

                    Session["UserMenu"] = sMenu;
                    Session["CurrentDate"] = sDateNew;
                    //Store menu string in Cache for 10 minutes
                    //Cache.Insert("UserMenu", sMenu, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
                    //Store menu selected date in Cache for 10 minutes
                    //Cache.Insert("CurrentDate", sDateNew, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
                }
            }
            MenuString = GlobalCode.Field2String(Session["UserMenu"]);
            ucLiteralMenu.Text = MenuString;
        }
        /// <summary>
        /// Date Created:    27/Nov/2012
        /// Created By:      Josephine Gad
        /// (description)    Get users menu by UserName using session
        /// ----------------------------------------------------------------
        /// Date Modified:   25/Apr/2013
        /// Modified By:     Josephine Gad
        /// (description)    Change Hotel Vendor's Page to HotelConfirmManifest.aspx Page
        /// ----------------------------------------------------------------
        /// Date Modified:  7/May/2013
        /// Modified By:    Marco Abejar
        /// (description)   Change Crew Assist Page to HotelDashboardRoomType5.aspx Page
        ///----------------------------------------------     
        /// Date Modified:  07/Mar/2014
        /// Modified By:    Josephine Gad
        /// (description)   Change class of menu if Service Provider 
        ///----------------------------------------------   
        /// </summary>
        private void BindMenu()
        {
            string URLString = "";
            string MenuString = "";
            string UserName = GlobalCode.Field2String(Session["UserName"]); //MUser.GetUserName();
            string UserRole = GlobalCode.Field2String(Session["UserRole"]); ;//MUser.GetUserRole();

            if (Session["UserMenu"] == null)
            {
                if (Request.QueryString["ufn"] != null)
                {
                    Session["DateFrom"] = Request.QueryString["dt"];
                    URLString = "?ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]; //gelo
                }
                MenuString = "<ul id =\"nav\">";

                if (UserRole == TravelMartVariable.RoleAdministrator)
                {                    
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../HotelDashboardRoomType5.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleHotelSpecialist)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../HotelDashboardRoomType5.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RolePortSpecialist)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../PortAgent/PortAgentDashboard.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleMeetGreet)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../MeetAndGreet/MeetAndGreet.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleCrewAdmin)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../CrewAdmin/CrewAdmin.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleHotelVendor)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Hotel/HotelConfirmManifest.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleSystemAnalyst)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../SystemAnalyst/ExceptionPNR.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleCrewAssist || UserRole == TravelMartVariable.RoleCrewAssistTeamLead)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../CrewAssist/CrewAssist.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleVehicleVendor)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Vehicle/VehicleManifestByVendor.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleFinance || UserRole == TravelMartVariable.RoleCrewMedical)
                {                    
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../ManifestSearchFilterPage.aspx" + URLString + "\">Home</a></li>";
                }
                else if (UserRole == TravelMartVariable.RoleImmigration)
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Immigration/CrewVerification.aspx" + URLString + "\">Home</a></li>";
                }
                else
                {
                    MenuString += "<li><a runat=\"server\"  id=\"uoAHome\" href=\"../Manifest.aspx" + URLString + "\">Home</a></li>";
                }

                string PageUrlString = "";
                string SubMenuString = "";
                string SubMenuString2 = "";
                try
                {
                    List<UserMenus> menuList = new List<UserMenus>();
                    List<UserSubMenus> subMenuList = new List<UserSubMenus>();

                    if (Session["UserMenuList"] != null)
                    {
                        menuList = (List<UserMenus>)Session["UserMenuList"];
                        subMenuList = (List<UserSubMenus>)Session["UserSubMenuList"];
                    }
                    else
                    {
                        menuList = UserRightsBLL.GetMenuListByUser(UserName);
                        subMenuList = (List<UserSubMenus>)Session["UserSubMenuList"];
                    }
                    if (menuList.Count > 0)
                    {
                        int menuMaxCount = menuList.Count;

                        for (int i = 0; i < menuMaxCount; i++)
                        {
                            PageUrlString = menuList[i].PageName;//MenuRow["colPageNameVarchar"].ToString();
                            PageUrlString = (PageUrlString == "" ? "#" : "../" + PageUrlString + URLString);

                            MenuString += "<li><a href=\"" + PageUrlString + "\">" + menuList[i].DisplayName + "</a>";

                            List<UserSubMenus> subMenuList1stLevel = new List<UserSubMenus>();
                            subMenuList1stLevel = (from a in subMenuList
                                                   where a.ParentIDInt == menuList[i].PageIDInt
                                                   select new UserSubMenus
                                                   {
                                                       ParentIDInt = a.ParentIDInt,
                                                       PageIDInt = a.PageIDInt,
                                                       ModuleName = a.ModuleName,
                                                       PageName = a.PageName,
                                                       DisplayName = a.DisplayName,
                                                       Sequence = a.Sequence
                                                   }).ToList();
                            subMenuList1stLevel = subMenuList1stLevel.OrderBy(a => a.Sequence).ToList();
                            
                            if(subMenuList1stLevel.Count > 0)
                            {
                                SubMenuString = "<ul>";
                                for (int i1st= 0; i1st < subMenuList1stLevel.Count; i1st++)
                                {
                                    PageUrlString = subMenuList1stLevel[i1st].PageName;
                                    PageUrlString = (PageUrlString == "" ? "#" : "../" + PageUrlString + URLString);

                                    SubMenuString += "<li><a href=\"" + PageUrlString + "\">" + subMenuList1stLevel[i1st].DisplayName + "</a>";

                                     List<UserSubMenus> subMenuList2ndLevel = new List<UserSubMenus>();
                                     subMenuList2ndLevel = (from a in subMenuList
                                                            where a.ParentIDInt == subMenuList1stLevel[i1st].PageIDInt
                                                            select new UserSubMenus
                                                            {
                                                                ParentIDInt = a.ParentIDInt,
                                                                PageIDInt = a.PageIDInt,
                                                                ModuleName = a.ModuleName,
                                                                PageName = a.PageName,
                                                                DisplayName = a.DisplayName,
                                                                Sequence = a.Sequence
                                                            }).ToList();
                                     subMenuList2ndLevel = subMenuList2ndLevel.OrderBy(a => a.Sequence).ToList();

                                     if (subMenuList2ndLevel.Count > 0)
                                     {
                                        SubMenuString2 = "<ul>";
                                        for (int i2nd = 0; i2nd < subMenuList2ndLevel.Count; i2nd++)
                                        {
                                            PageUrlString = subMenuList2ndLevel[i2nd].PageName;
                                            PageUrlString = (PageUrlString == "" ? "#" : "../" + PageUrlString + URLString);
                                            SubMenuString2 += "<li><a href=\"" + PageUrlString + "\">" +subMenuList2ndLevel[i2nd].DisplayName + "</a></li>";
                                        }
                                        SubMenuString2 += "</ul>";
                                        SubMenuString += SubMenuString2;
                                    }
                                    SubMenuString += "</li>";

                                }
                                SubMenuString += "</ul>";
                                MenuString += SubMenuString;
                            }
                            MenuString += "</li>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                if ((UserRole != TravelMartVariable.RoleFinance) && (UserRole != TravelMartVariable.RoleCrewMedical) && (UserRole != TravelMartVariable.RoleImmigration ))
                {
                    MenuString += "<li><a runat=\"server\" id=\"uoASearch\" href=\"../ManifestSearchFilter.aspx" + URLString + "\">Search</a></li>";
                }
                
                MenuString += "</ul>";

                Session["UserMenu"] = MenuString;
                //Store menu string in Cache for 10 minutes
                //Cache.Insert("UserMenu", MenuString, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
            }
            if (Session["CurrentDate"] == null)
            {
                Session["CurrentDate"] = Request.QueryString["dt"].ToString();
                //Store menu string in Cache for 10 minutes
                //Cache.Insert("CurrentDate", Request.QueryString["dt"].ToString(), null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
            }
            if (Session["UserMenu"] != null && Session["CurrentDate"] != null)
            {
                string sMenu = Session["UserMenu"].ToString();
                string sDate = Session["CurrentDate"].ToString();

                if (sDate == "")
                {
                    sDate = GlobalCode.Field2Date(DateTime.Now);
                }
                string sDateNew = Request.QueryString["dt"].ToString();
                //sDate = sDate.Replace("_", "/");
                if (!sDateNew.Contains("-"))
                {
                    sDateNew = sDateNew.Replace("_", "/");
                    sMenu = sMenu.Replace(sDate, sDateNew);

                    Session["UserMenu"] = sMenu;
                    Session["CurrentDate"] = sDateNew;
                    //Store menu string in Cache for 10 minutes
                    //Cache.Insert("UserMenu", sMenu, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
                    //Store menu selected date in Cache for 10 minutes
                    //Cache.Insert("CurrentDate", sDateNew, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10));
                }
            }
            MenuString = GlobalCode.Field2String(Session["UserMenu"]);
            ucLiteralMenu.Text = MenuString;
        }   
        #endregion
    }       
}